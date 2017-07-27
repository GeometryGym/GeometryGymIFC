// MIT License
// Copyright (c) 2016 Geometry Gym Pty Ltd

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
// and associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial 
// portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
// LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;

using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class BaseClassIfc : STEPEntity, IBaseClassIfc
	{
		public virtual string Name { get { return ""; } set { } }
		internal DatabaseIfc mDatabase = null;

		public DatabaseIfc Database { get { return mDatabase; } }

		public BaseClassIfc() : base() { }
		protected BaseClassIfc(BaseClassIfc basis)
		{
			basis.ReplaceDatabase(this);
		}
		protected BaseClassIfc(DatabaseIfc db, BaseClassIfc e) { db.appendObject(this); db.Factory.mDuplicateMapping.Add(e.mIndex, mIndex); }
		internal BaseClassIfc(int record, string kw, string line) : base(record,kw,line) { }
		protected BaseClassIfc(DatabaseIfc db) { if(db != null) db.appendObject(this); }

		protected virtual void parseFields(List<string> arrFields, ref int ipos) { }
		internal virtual void postParseRelate() { }

		public List<T> Extract<T>() where T :IBaseClassIfc { return Extract<T>(typeof(T)); }
		protected virtual List<T> Extract<T>(Type type) where T : IBaseClassIfc
		{
			List<T> result = new List<T>();
			if (this is T)
				result.Add((T)(IBaseClassIfc)this);
			return result;
		}

		internal virtual void changeSchema(ReleaseVersion schema) { }
		protected void ReplaceDatabase(BaseClassIfc revised)
		{
			mDatabase[revised.mIndex] = null;
			revised.mIndex = mIndex;
			mDatabase[mIndex] = revised;
		}
		public virtual bool Destruct(bool children)
		{
			if (mDatabase == null)
				return true;
			mDatabase[mIndex] = null;
			return true;
		}
		internal virtual List<IBaseClassIfc> retrieveReference(IfcReference reference) { return (reference.InnerReference != null ? null : new List<IBaseClassIfc>() { }); }


		private static Type[] argSet1 = new Type[] { typeof(string), typeof(ReleaseVersion) }, argSet2 = new Type[] { typeof(string) };
		internal static BaseClassIfc LineParser(string keyword, string str, ReleaseVersion schema)
		{
			MethodInfo parser = null;			
			if(mConstructorsSchema.TryGetValue(keyword,out parser))
				return parser.Invoke(null, new object[] { str, schema }) as BaseClassIfc;
			if(mConstructorsNoSchema.TryGetValue(keyword,out parser))
				return parser.Invoke(null, new object[] { str }) as BaseClassIfc;
			Type type = null;
			if (!mTypes.TryGetValue(keyword, out type))
			{
				type = Type.GetType("GeometryGym.Ifc." + keyword, false, true);
				if (type != null)
					mTypes.TryAdd(keyword, type);
			}
			if (type != null)
			{
				parser = type.GetMethod("Parse", BindingFlags.Static | BindingFlags.NonPublic, null, CallingConventions.Any, argSet1, null);
				if (parser != null)
				{
					mConstructorsSchema.TryAdd(keyword, parser);
					return parser.Invoke(null, new object[] { str, schema }) as BaseClassIfc;
				}
				parser = type.GetMethod("Parse", BindingFlags.Static | BindingFlags.NonPublic, null, CallingConventions.Any, argSet2, null);
				if (parser != null)
				{
					mConstructorsNoSchema.TryAdd(keyword, parser);
					return parser.Invoke(null, new object[] { str }) as BaseClassIfc;
				}
			}
			if (string.Compare(keyword, "IfcParameterizedProfileDef", true) == 0)
				return LineParser("IfcProfileDef", str, schema);
			return null;
		}
	}
	public interface IBaseClassIfc { int Index { get; } string Name { get; set; } DatabaseIfc Database { get; } }

}
