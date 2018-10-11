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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;

using GeometryGym.STEP;


namespace GeometryGym.Ifc
{
	public class VersionAddedAttribute : Attribute
	{
		public ReleaseVersion Release { get; set; }
		public VersionAddedAttribute(ReleaseVersion release)
		{
			Release = release;
		}
	}

	[Serializable()]
	public abstract partial class BaseClassIfc : STEPEntity, IBaseClassIfc
	{
		internal string mGlobalId = ""; // :	IfcGloballyUniqueId;

		[NonSerialized] internal DatabaseIfc mDatabase = null;

		public DatabaseIfc Database { get { return mDatabase; } private set { mDatabase = value; } }

#if (NOIFCJSON)
		public BaseClassIfc() : base() { }// this(new DatabaseIfc(false, ModelView.Ifc4NotAssigned)) { }
#else
		public BaseClassIfc() : base() { } // this(new DatabaseIfc(false, ModelView.Ifc4NotAssigned) { Format = FormatIfcSerialization.JSON }) { }
#endif
		protected BaseClassIfc(BaseClassIfc basis) : base()
		{
			basis.ReplaceDatabase(this);
		}
		protected BaseClassIfc(DatabaseIfc db, BaseClassIfc e) : base() { mGlobalId = e.mGlobalId; db.appendObject(this); db.Factory.mDuplicateMapping.AddObject(e, mIndex);  }
		protected BaseClassIfc(DatabaseIfc db) : base()
		{
			if(db != null)
				db.appendObject(this);
		}

		protected virtual void parseFields(List<string> arrFields, ref int ipos) { }
		internal virtual void postParseRelate() { }

		public List<T> Extract<T>() where T : IBaseClassIfc
		{
			List<T> extracted = Extract<T>(typeof(T));
			HashSet<string> rootIds = new HashSet<string>();
			List<T> result = new List<T>(extracted.Count);
			foreach(T t in extracted)
			{
				if(t is IfcRoot)
				{
					IfcRoot root = (IfcRoot)(IBaseClassIfc)t;
					string globalId = root.GlobalId;
					if (rootIds.Contains(globalId))
						continue;
					rootIds.Add(globalId);
				}
				result.Add(t);
			}
			return result;
		}
		protected virtual List<T> Extract<T>(Type type) where T : IBaseClassIfc
		{
			List<T> result = new List<T>();
			if (this is T)
				result.Add((T)(IBaseClassIfc)this);
			return result;
		}

		public override string ToString()
		{
#if (!NOIFCJSON)
			if (mDatabase == null || mDatabase.Format == FormatIfcSerialization.JSON)
				return getJson(null, new BaseClassIfc.SetJsonOptions()).ToString();
#endif
			if(mDatabase != null)
			{
				if (mDatabase.Format == FormatIfcSerialization.XML)
					return GetXML(new System.Xml.XmlDocument(), "", null, new Dictionary<int, System.Xml.XmlElement>()).OuterXml;
			}
			return base.ToString();
		}

		internal virtual void changeSchema(ReleaseVersion schema) { }
		protected void ReplaceDatabase(BaseClassIfc revised)
		{
			mDatabase[revised.mIndex] = null;
			revised.mIndex = mIndex;
			mDatabase[mIndex] = revised;
		}
		public virtual bool Dispose(bool children)
		{
			if (mDatabase == null)
				return true;
			mDatabase[mIndex] = null;
			return true;
		}
		internal virtual List<IBaseClassIfc> retrieveReference(IfcReference reference) { return (reference.InnerReference != null ? null : new List<IBaseClassIfc>() { }); }

		internal static Type GetType(string className)
		{
			Type type = null;
			string name = className;
			string[] fields = className.Split(".".ToCharArray());
			if (fields != null && fields.Length > 0)
				name = fields[0];
			if (!mTypes.TryGetValue(name, out type))
			{
				type = Type.GetType("GeometryGym.Ifc." + name, false, true);
				if (type != null)
					mTypes[name] = type;
			}
			return type;	
		}
		public static BaseClassIfc Construct(string className)
		{
			ConstructorInfo constructor = null;
			if (!mConstructors.TryGetValue(className, out constructor))
			{
				Type type = GetType(className);
				if (type == null)
					return null;
				constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { }, null);
				if (constructor == null)
				{
					if (string.Compare(className, "IfcParameterizedProfileDef", true) == 0)
						return Construct("IfcProfileDef");
					if (string.Compare(className, "IfcBuildingElement", true) == 0)
						return Construct("IfcBuildingElementProxy");
				}
				mConstructors.TryAdd(className, constructor);
			}
			return constructor.Invoke(new object[] { }) as BaseClassIfc;
		}
		internal static BaseClassIfc LineParser(string className, string str, ReleaseVersion release, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			BaseClassIfc result = Construct(className);	
			if(result == null)
				return null;
			int pos = 0;
			result.parse(str, ref pos, release, str.Length, dictionary);
			return result;
		}

		internal virtual bool isDuplicate(BaseClassIfc e) { return true; }

		internal class RepositoryAttributes
		{
			internal DateTime Created { get; set; } = DateTime.MinValue;
			internal DateTime Modified { get; set; } = DateTime.MinValue;

			internal RepositoryAttributes() { }
			internal RepositoryAttributes(DateTime created, DateTime modified) { Created = created; Modified = modified; }
		}

		internal void setFolderAttributes(string folderPath)
		{
			IfcRoot root = this as IfcRoot;
			IfcOwnerHistory ownerHistory = (root == null ? null : root.OwnerHistory);
			if (ownerHistory == null)
				return;
			setFolderAttributes(folderPath, new RepositoryAttributes(ownerHistory.CreationDate, ownerHistory.LastModifiedDate));
		}
		internal void setFolderAttributes(string folderPath, RepositoryAttributes attributes)
		{
			try
			{
				DateTime created = attributes.Created, modified = attributes.Modified;
				if (created != DateTime.MinValue && created < Directory.GetCreationTime(folderPath))
				{
					Directory.SetCreationTime(folderPath, created);
					Directory.SetLastWriteTime(folderPath, created);
				}
				if (modified != DateTime.MinValue && modified > Directory.GetLastWriteTime(folderPath))
					Directory.SetLastWriteTime(folderPath, modified);
			}
			catch (Exception) { }
		}

		internal void setFileAttributes(string filePath)
		{
			IfcRoot root = this as IfcRoot;
			IfcOwnerHistory ownerHistory = (root == null ? null : root.OwnerHistory);
			if (ownerHistory == null)
				return;
			setFileAttributes(filePath, new RepositoryAttributes(ownerHistory.CreationDate, ownerHistory.LastModifiedDate));
		}
		internal void setFileAttributes(string filePath, RepositoryAttributes attributes)
		{
			try
			{
				DateTime created = attributes.Created, modified = attributes.Modified;
				if (created != DateTime.MinValue && created < File.GetCreationTime(filePath))
				{
					File.SetCreationTime(filePath, created);
					File.SetLastWriteTime(filePath, created);
				}
				if (modified != DateTime.MinValue && modified > File.GetLastWriteTime(filePath))
					File.SetLastWriteTime(filePath, modified);
			}
			catch (Exception) { }
			
		}
	}
	public partial interface IBaseClassIfc : ISTEPEntity
	{
		DatabaseIfc Database { get; }
	}
	public interface NamedObjectIfc : IBaseClassIfc { string Name { get; } } // GG interface
}
