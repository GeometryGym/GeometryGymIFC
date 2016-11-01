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
		internal string mIFCString = "";
		public virtual string Name { get { return ""; } set { } }
		internal DatabaseIfc mDatabase = null;

		public DatabaseIfc Database { get { return mDatabase; } }

		internal BaseClassIfc() : base() { }
		protected BaseClassIfc(BaseClassIfc basis)
		{
			basis.ReplaceDatabase(this);
		}
		protected BaseClassIfc(DatabaseIfc db, BaseClassIfc e) { db.appendObject(this); db.Factory.mDuplicateMapping.Add(e.mIndex, mIndex); }
		internal BaseClassIfc(int record, string kw, string line) { mIndex = record; mIFCString = line; }
		protected BaseClassIfc(DatabaseIfc db) { db.appendObject(this); }

		protected virtual void parseFields(List<string> arrFields, ref int ipos) { }
		internal virtual void postParseRelate() { }

		public virtual List<T> Extract<T>() where T : IBaseClassIfc
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
	}
	public interface IBaseClassIfc { int Index { get; } string Name { get; set; } DatabaseIfc Database { get; } }

}
