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
using System.Linq;

#if (NET || !NOIFCJSON)
#if (NEWTONSOFT)
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonObject = Newtonsoft.Json.Linq.JObject;
using JsonArray = Newtonsoft.Json.Linq.JArray;
using JsonNode = Newtonsoft.Json.Linq.JToken;
#else
using System.Text.Json.Nodes;
#endif

using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial interface IBaseClassIfc { JsonObject getJson(BaseClassIfc host, BaseClassIfc.SetJsonOptions options); }
	public partial class BaseClassIfc : STEPEntity, IBaseClassIfc
	{
		public class SetJsonOptions
		{
			public enum JsonStyle { Explicit, Repository, Default };
			public string ProjectFolder = "";
			public HashSet<string> Encountered = new HashSet<string>();
			public JsonStyle Style = JsonStyle.Default;
			public bool Local = false;  //Convert to nesting
			public bool SerializeOwnerHistory = true, SerializeAllGlobalIds = true;
			internal int LengthDigitCount = 4;
			internal RepositoryAttributes RepositoryAttributes = new RepositoryAttributes();
			internal ReleaseVersion Version = ReleaseVersion.IFC4X3_RC3;

			public SetJsonOptions() { }

			internal SetJsonOptions(SetJsonOptions options)
			{
				ProjectFolder = options.ProjectFolder;
				Encountered = options.Encountered;
				Style = options.Style;
				RepositoryAttributes = options.RepositoryAttributes;
				LengthDigitCount = options.LengthDigitCount;
				Version = options.Version;
				SerializeAllGlobalIds = options.SerializeAllGlobalIds;
				SerializeOwnerHistory = options.SerializeOwnerHistory;
			}
			internal SetJsonOptions(BaseClassIfc obj) { adopt(obj); }
			internal SetJsonOptions(SetJsonOptions options, BaseClassIfc obj) : this(options) { adopt(obj); }
			private void adopt(BaseClassIfc obj)
			{ 
				IfcRoot root = obj as IfcRoot;
				if(root != null)
				{
					IfcOwnerHistory ownerHistory = root.OwnerHistory;
					if (ownerHistory != null)
						RepositoryAttributes = new RepositoryAttributes(ownerHistory.CreationDate, ownerHistory.LastModifiedDate);
				}
			}
		}
		internal virtual void parseJsonObject(JsonObject obj)
		{
			
		}
		internal string removeFileNameIllegal(string filename)
		{
			return ParserIfc.Encode(filename).Replace("*", "x").Replace(".", "_").Replace("\"", "").Replace("/", "_").Replace("\\", "_").Replace("[", "(").Replace("]", ")").Replace(":", "-").Replace(";", "-").Replace("|", "-").Replace("=", "-").Replace(",", "_").Replace("<", "(").Replace(">", ")");
		}
		internal string RepositoryName { get { return removeFileNameIllegal(genRepositoryName); } }
		protected string RepositoryNameStub { get { return mGlobalId; } }
		protected virtual string genRepositoryName
		{
			get
			{
				NamedObjectIfc named = this as NamedObjectIfc;
				string name = (named == null ? "" : named.Name);
				return (string.IsNullOrEmpty(name) ? "" : name + " ") + RepositoryNameStub;
			}
		}
		
		private JsonObject writeRepositoryCommon(JsonObject obj, SetJsonOptions options) { return writeRepositoryCommon(obj, options, ""); }
		private JsonObject writeRepositoryCommon(JsonObject obj, SetJsonOptions options, string folderName)
		{
			if (string.IsNullOrEmpty(options.ProjectFolder))
				return obj;
			if (string.IsNullOrEmpty(mGlobalId))
				obj["id"] = mGlobalId = ParserIfc.EncodeGuid(Guid.NewGuid());
			options.Encountered.Add(mGlobalId);
			string folder = Path.Combine(options.ProjectFolder, string.IsNullOrEmpty(folderName) ?  StepClassName.Substring(3) : folderName);
			Directory.CreateDirectory(folder);
			string path = Path.Combine(folder, RepositoryName + ".ifc.json");
			StreamWriter streamWriter = new StreamWriter(path);

			streamWriter.Write(obj.ToString());
			streamWriter.Close();
			setFileAttributes(path, options.RepositoryAttributes);
			setFolderAttributes(folder, options.RepositoryAttributes);
			JsonObject	result = new JsonObject();
			result["href"] = mGlobalId;
				return result;
		}
		internal bool isCommon
		{
			get
			{
				if (this is IfcApplication ||
					this is IfcOrganization || this is IfcOwnerHistory ||
					this is IfcPerson || this is IfcPersonAndOrganization || this is IfcPresentationStyleAssignment || this is IfcPresentationLayerAssignment)
					return true;
				
				IfcProfileDef profile = this as IfcProfileDef;
				if (profile != null && !string.IsNullOrEmpty(profile.Name))
					return true;

				//IfcPropertySet pset = this as IfcPropertySet;
					//	if (rdp != null && rdp.RelatedObjects.Count > 0)
				//		return true;
				//}
				return false;	
			}
		}
		public JsonObject getJson(BaseClassIfc host, SetJsonOptions options)
		{
			bool common = isCommon;
			JsonObject obj = new JsonObject();
			
			if (!string.IsNullOrEmpty(mGlobalId))
			{
				if (options.Encountered.Contains(mGlobalId))
				{
					obj["href"] = mGlobalId;
					return obj;
				}
				else
					options.Encountered.Add(mGlobalId);
			}
			obj["type"] = StepClassName;
			if (common || (options.SerializeAllGlobalIds && !string.IsNullOrEmpty(mGlobalId)) || this is IfcGeometricRepresentationContext || this is NamedObjectIfc)
			{
				if (string.IsNullOrEmpty(mGlobalId))
				{
					mGlobalId = ParserIfc.EncodeGuid(Guid.NewGuid());
					options.Encountered.Add(mGlobalId);
				}
				if(options.SerializeAllGlobalIds && !string.IsNullOrEmpty(mGlobalId))
					obj["id"] = mGlobalId;
			}
			
			setJSON(obj, host, options);
			if (options.Style == SetJsonOptions.JsonStyle.Repository)
			{
				if (common)
					return writeRepositoryCommon(obj, options, "");
				else
				{
					IfcRelationship relationship = this as IfcRelationship;
					if (relationship != null)
					{
						if (this is IfcRelAssociates || this is IfcRelDefinesByType)
							return this.writeRepositoryCommon(obj, options);
						else
						{
							IfcRelDefinesByProperties properties = this as IfcRelDefinesByProperties;
							if (properties != null && properties.RelatedObjects.Count > 1)
								return this.writeRepositoryCommon(obj, options);
						}
					}
				}
			}
			return obj;
		}
		protected virtual void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			if(options.SerializeAllGlobalIds && !string.IsNullOrEmpty(mGlobalId) && this as IfcRoot == null)
				obj["id"] = mGlobalId;
		}
		protected T extractObject<T>(JsonObject obj) where T : IBaseClassIfc
		{
			return (obj == null ? default(T) : mDatabase.ParseJsonObject<T>(obj));
		}
		protected string extractString(JsonObject obj, string key)
		{
			var node = obj[key];
			return node == null ? "" : node.GetValue<string>();
		}
		protected DateTime extractDateTime(JsonObject obj, string key)
		{
			string str = extractString(obj, key);
			if (string.IsNullOrEmpty(str))
				return DateTime.MinValue;
			return DateTime.Parse(str);
		}
#if (NEWTONSOFT)
		protected string extractString(JToken node) { return (node == null ? "" : node.Value<string>()); }
#else
		protected string extractString(JsonNode node) { return (node == null ? "" : node.GetValue<string>()); }
#endif
		protected double extractDouble(JsonNode node)
		{
			if (node == null)
				return double.NaN;
			try
			{
				return node.GetValue<double>();
			}
			catch { }
			try
			{
				string str = node.GetValue<string>();

				if (double.TryParse(str, out double d))
					return d;
			}
			catch { }

			return double.NaN;
		}
		protected int extractInt(JsonNode node, int defaultValue)
		{
			if (node == null)
				return defaultValue;
			try
			{
				return node.GetValue<int>();
			}
			catch { }
			try
			{
				string str = node.GetValue<string>();

				if (int.TryParse(str, out int i))
					return i;
			}
			catch { }

			return defaultValue;
		}
		protected bool extractBool(JsonNode node)
		{
			if (node == null)
				return false;
			try
			{
				return node.GetValue<bool>();
			}
			catch { }
			try
			{
				string str = node.GetValue<string>();

				if (bool.TryParse(str, out bool b))
					return b;
			}
			catch { }

			return false;
		}
		protected void setAttribute(JsonObject obj, string attribute, string value)
		{
			if (!string.IsNullOrEmpty(value))
				obj[attribute] = value;
		}
		protected void createArray(JsonObject obj, string name, IEnumerable<IBaseClassIfc> objects, BaseClassIfc host, SetJsonOptions options)
		{
			if (objects.Count() == 0)
				return;
			JsonArray array = new JsonArray();
			foreach(var o in objects.OfType<BaseClassIfc>())
			{
				var jsonObject = o.getJson(host, options);
				if(jsonObject != null)
					array.Append(jsonObject);
			}	
			obj[name] = array;
		}
		protected void createArray(JsonObject obj, string name, IEnumerable<string> strings)
		{
			JsonArray array = new JsonArray();
			foreach (string str in strings)
				array.Add(str);
			obj[name] = array;
		}
		protected void createArray(JsonObject obj, string name, IEnumerable<JsonObject> objects)
		{
			JsonArray array = new JsonArray();
			foreach (var o in objects)
				array.Add(o);
			obj[name] = array;
		}

		public static void setJSON(IfcColourOrFactor colourOrFactor, string name, JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			if (colourOrFactor == null)
				return;
			IfcNormalisedRatioMeasure normalisedRatioMeasure = colourOrFactor as IfcNormalisedRatioMeasure;
			if (normalisedRatioMeasure != null)
				obj[name] = DatabaseIfc.extract(normalisedRatioMeasure);
			else
				obj[name] = (colourOrFactor as BaseClassIfc).getJson(host, options);
		}
		 
	}
}
#endif
