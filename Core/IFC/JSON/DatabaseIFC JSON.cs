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
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Globalization;
using System.Threading;

#if (NET || !NOIFCJSON)
#if (NEWTONSOFT)
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonObject = Newtonsoft.Json.Linq.JObject;
using JsonArray = Newtonsoft.Json.Linq.JArray;
#else
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
#endif

namespace GeometryGym.Ifc
{
	public partial class DatabaseIfc
	{
		public void ReadJSONFile(string filename) { FileName = filename; ReadJSONFile(new StreamReader(filename)); }
		public void ReadJSONFile(TextReader reader)
		{
			mRelease = ReleaseVersion.IFC2x3;
			bool ownerHistory = mFactory.Options.GenerateOwnerHistory;
			mFactory.Options.GenerateOwnerHistory = false;
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

			Release = ReleaseVersion.IFC4;
			string str = reader.ReadToEnd();

#if (NEWTONSOFT)
			JsonObject jsonObject = JsonObject.Parse(str);
#else
			JsonObject jsonObject = JsonObject.Parse(str) as JsonObject;
#endif
			ReadJSON(jsonObject);

			if (mContext != null)
				mContext.initializeUnitsAndScales();

			Thread.CurrentThread.CurrentCulture = current;
			postImport();
			Factory.Options.GenerateOwnerHistory = ownerHistory;
		}

		internal int mNextUnassigned = 0;

		public List<IBaseClassIfc> ReadJSON(JsonObject ifcFile)
		{
			List<IBaseClassIfc> result = new List<IBaseClassIfc>();
			var node = ifcFile["HEADER"];
			node = ifcFile["DATA"];
			JsonArray array = node as JsonArray;
			if (array != null)
				result = extractJsonArray<IBaseClassIfc>(array);
			else
			{
				IBaseClassIfc obj = ParseJsonObject<IBaseClassIfc>(ifcFile);
				if (obj != null)
					result.Add(obj);
			}
			return result;
		}
		internal List<T> extractJsonArray<T>(JsonArray array) where T : IBaseClassIfc 
		{
			List<T> result = new List<T>();
			if (array == null)
				return result;
			foreach (var node in array)
			{
				JsonObject obj = node as JsonObject;
				if (obj != null)
				{
					T entity = ParseJsonObject<T>(obj);
					if (entity != null)
						result.Add(entity);
				}
				else
				{
					logParseError("Unrecognized node");
				}
			}
			return result;
		}
		public T ParseJsonObject<T>(JsonObject obj) where T : IBaseClassIfc
		{
			if (obj == null)
				return default(T);

			Type type = typeof(T);
			
			string hrefId = "";
			BaseClassIfc result = null;
			var node = obj["href"];
			if (node != null)
			{
				hrefId = node.GetValue<string>();
				if (mDictionary.TryGetValue(hrefId, out result) && obj.Count == 1)
					return (T)(IBaseClassIfc)result;
			}
			if(string.IsNullOrEmpty(hrefId))
			{
				node = obj["id"];
				if (node != null)
				{
					hrefId = node.GetValue<string>();
					mDictionary.TryGetValue(hrefId, out result);
				}
			}

			if (result == null)
			{
				if (type.IsAbstract)
				{
#if (NEWTONSOFT)
					var jtoken = (JProperty)obj.First;
					Type valueType = BaseClassIfc.GetType(jtoken.Name);
					string key = jtoken.Name;
					string value = jtoken.Value.ToString();
				
#else
					var pair = obj.First();
					Type valueType = BaseClassIfc.GetType(pair.Key);
					string key = pair.Key;
					string value = pair.Value.ToString();

#endif
					if (valueType != null && valueType.IsSubclassOf(typeof(IfcValue)))
					{
						IBaseClassIfc val = ParserIfc.extractValue(key, value) as IBaseClassIfc;
						if (val != null)
							return (T)val;
						return default(T);
					}
				}


				node = obj["type"];
				if (node != null)
				{
					Type nominatedType = BaseClassIfc.GetType(node.GetValue<string>());
					if (nominatedType != null)
						type = nominatedType;
				}
				if (string.IsNullOrEmpty(hrefId) || !mDictionary.TryGetValue(hrefId, out result))
				{
					ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
	null, Type.EmptyTypes, null);
					if (constructor != null)
					{
						bool common = false;
						result = constructor.Invoke(new object[] { }) as BaseClassIfc;
						if (result != null)
						{
							result.mDatabase = this;
							if(!string.IsNullOrEmpty(hrefId))
								mDictionary[hrefId] = result;

							IfcCartesianPoint point = result as IfcCartesianPoint;
							if (point != null)
							{
								point.parseJsonObject(obj);
								if (point.isOrigin(Tolerance))
								{
									if (point.is2D)
										result = Factory.Origin2d;
									else
										result = Factory.Origin;
									common = true;
								}
							}
							else
							{
								IfcDirection direction = result as IfcDirection;
								if (direction != null)
								{
									direction.parseJsonObject(obj);
									if (!direction.is2D)
									{
										common = true;
										if (direction.isXAxis)
											result = Factory.XAxis;
										else if (direction.isYAxis)
											result = Factory.YAxis;
										else if (direction.isZAxis)
											result = Factory.ZAxis;
										else if (direction.isXAxisNegative)
											result = Factory.XAxisNegative;
										else if (direction.isYAxisNegative)
											result = Factory.YAxisNegative;
										else if (direction.isZAxisNegative)
											result = Factory.ZAxisNegative;
										else
											common = false;
									}
								}
								else
								{
									IfcAxis2Placement3D placement = result as IfcAxis2Placement3D;
									if (placement != null)
									{
										placement.parseJsonObject(obj);
										if (placement.IsXYPlane(Tolerance))
										{
											result = Factory.XYPlanePlacement;
											common = true;
										}
									}
								}
							}
							node = obj["id"];
							if (node != null)
							{
								hrefId = node.GetValue<string>();
								if (!string.IsNullOrEmpty(hrefId))
								{
									if (!(result is IfcRoot))
										result.setGlobalId(hrefId);
									mDictionary.TryAdd(hrefId, result);
								}
							}

							if (common)
								return (T)(IBaseClassIfc)result;

							int index = NextBlank();
							this[index] = result;
						}
					}
				}

			}
			if(result == null)
				return default(T);
			result.parseJsonObject(obj);
			parseBespoke(result, obj);
			IfcRoot root = result as IfcRoot;
			if (root != null)
				mDictionary[root.GlobalId] = root;
			return (T)(IBaseClassIfc)result;
		}

		partial void parseBespoke(BaseClassIfc entity, JsonObject JsonObject);
		public JsonObject JSON() { return ToJSON(""); }
		public JsonObject ToJSON(string filename)
		{
			BaseClassIfc.SetJsonOptions options = new BaseClassIfc.SetJsonOptions() { };
			return ToJSON(filename, options);
		}
		public JsonObject ToJSON(string fileName, BaseClassIfc.SetJsonOptions options)
		{ 
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

			options.Version = mRelease;
			options.LengthDigitCount = mLengthDigits;

			JsonObject ifcFile = new JsonObject();
			JsonObject header = new JsonObject();
			JsonObject fileDescription = new JsonObject();
			fileDescription["description"] = "ViewDefinition[" + viewDefinition + "]";
			fileDescription["implementation_level"] = "2;1";
			header["FILE_DESCRIPTION"] = fileDescription;
			JsonObject jsonObject = new JsonObject();
			string strFileName = fileName;
			if(!WriteFullFilePath)
				strFileName = System.IO.Path.GetFileName(fileName);

			jsonObject["name"] = strFileName;
			DateTime now = DateTime.Now;
			jsonObject["time_stamp"] = now.Year + "-" + (now.Month < 10 ? "0" : "") + now.Month + "-" + (now.Day < 10 ? "0" : "") + now.Day + "T" + (now.Hour < 10 ? "0" : "") + now.Hour + ":" + (now.Minute < 10 ? "0" : "") + now.Minute + ":" + (now.Second < 10 ? "0" : "") + now.Second;
			jsonObject["author"] = System.Environment.UserName;
			jsonObject["organization"] = IfcOrganization.Organization;
			jsonObject["preprocessor_version"] = mFactory.ToolkitName;
			jsonObject["originating_system"] = Factory.ApplicationFullName;
			jsonObject["authorization"] = "None";
			header["FILE_NAME"] = jsonObject;
			JsonObject fileSchema = new JsonObject();
			fileSchema["schema_identifiers"] = mRelease == ReleaseVersion.IFC2x3 ? "IFC2X3" : "IFC4";
			header["FILE_SCHEMA"] = fileSchema;
			ifcFile["HEADER"] = header;

			JsonArray data = new JsonArray();
			IfcContext context = this.mContext;
			if (context != null)
			{
				JsonObject jcontext = context.getJson(null, options); 
				data.Add(jcontext);
			}
			if(context == null || (context.mIsDecomposedBy.Count == 0 && context.Declares.Count == 0))
			{
				foreach (BaseClassIfc e in this)
				{
					if (!string.IsNullOrEmpty(e.mGlobalId))
					{
						if (!options.Encountered.Contains(e.mGlobalId))
							data.Add(e.getJson(null, options));
					}
					else
					{
						NamedObjectIfc named = e as NamedObjectIfc;
						if (named != null)
						{
							data.Add(named.getJson(null, options));
						}
					}
				}
			}
			ifcFile["DATA"] = data;
			if (!string.IsNullOrEmpty(fileName))
			{
#if (NEWTONSOFT)
				StreamWriter sw = new StreamWriter(fileName);
#if (DEBUG)
				sw.Write(ifcFile.ToString());
#else
				sw.Write(ifcFile.ToString(Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonConverter[0]));
#endif
				sw.Close();
#else
				JsonSerializerOptions jsonoptions = new JsonSerializerOptions();
				jsonoptions.WriteIndented = true;
				string jsonString = JsonSerializer.Serialize(ifcFile, jsonoptions);
				File.WriteAllText(fileName, jsonString);
#endif
			}
			Thread.CurrentThread.CurrentUICulture = current;
			return ifcFile;
			
		}
		
		internal static JsonObject extract(IfcValue value)
		{
			JsonObject result = new JsonObject();
			result[value.GetType().Name] = value.ValueString;
			return result;
		}
		internal static IfcValue ParseValue(JsonObject obj)
		{
#if (NEWTONSOFT)
			JProperty token = (JProperty) obj.First;
			return ParserIfc.extractValue(token.Name, token.Value.ToString());
#else
			var token = obj.First();
			return ParserIfc.extractValue(token.Key, token.Value.ToString());
#endif
		}
	}

	public static partial class JsonIFCExtensions
	{
#if (NEWTONSOFT)
		public static T GetValue<T>(this JToken node)
		{
			return node.Value<T>();
		}
		public static void StripToEssentialIFC(this JToken containerToken)
		{
			HashSet<string> toStrip = new HashSet<string>();
			toStrip.Add("globalid");
			stripEssential(containerToken, toStrip);
		}
		private static void stripEssential(JToken containerToken, HashSet<string> toStrip)
		{
			if (containerToken.Type == JTokenType.Object)
			{
				List<JProperty> children = containerToken.Children<JProperty>().ToList();
				foreach (JProperty child in children)
				{
					if (toStrip.Contains(child.Name.ToLower()))
					{
						child.Remove();
					}
					stripEssential(child.Value, toStrip);
				}
			}
			else if (containerToken.Type == JTokenType.Array)
			{
				foreach (JToken child in containerToken.Children())
				{
					stripEssential(child, toStrip);
				}
			}
		}
#endif
	}
}
#endif