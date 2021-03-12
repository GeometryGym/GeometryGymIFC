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

using Newtonsoft.Json.Linq;

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
			ReadJSON(JObject.Parse(str));

			if (mContext != null)
				mContext.initializeUnitsAndScales();

			Thread.CurrentThread.CurrentCulture = current;
			postImport();
			Factory.Options.GenerateOwnerHistory = ownerHistory;
		}

		internal int mNextUnassigned = 0;

		public List<IBaseClassIfc> ReadJSON(JObject ifcFile)
		{
			List<IBaseClassIfc> result = new List<IBaseClassIfc>();
			JToken token = ifcFile.First;
			token = ifcFile["HEADER"];
			token = ifcFile["DATA"];
			JArray array = token as JArray;
			if (array != null)
				result = extractJArray<IBaseClassIfc>(array);
			else
			{
				IBaseClassIfc obj = ParseJObject<IBaseClassIfc>(ifcFile);
				if (obj != null)
					result.Add(obj);
			}
			return result;
		}
		internal List<T> extractJArray<T>(JArray array) where T : IBaseClassIfc 
		{
			List<T> result = new List<T>();
			if (array == null)
				return result;
			foreach (JToken token in array)
			{
				JObject obj = token as JObject;
				if (obj != null)
				{
					T entity = ParseJObject<T>(obj);
					if (entity != null)
						result.Add(entity);
				}
				else
				{
					logParseError("Unrecognized token");
				}
			}
			return result;
		}
		public T ParseJObject<T>(JObject obj) where T : IBaseClassIfc
		{
			if (obj == null)
				return default(T);

			Type type = typeof(T);
			
			BaseClassIfc result = null;
			JToken token = obj.GetValue("href", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
			{
				string hrefId = token.Value<string>();
				if(mDictionary.TryGetValue(hrefId, out result) && obj.Count == 1)
					return (T)(IBaseClassIfc)result;
			}
			if (result == null)
			{
				if (type.IsAbstract)
				{
					JProperty jtoken = (JProperty)obj.First;
					Type valueType = BaseClassIfc.GetType(jtoken.Name);
					if (valueType != null && valueType.IsSubclassOf(typeof(IfcValue)))
					{
						IBaseClassIfc val = ParserIfc.extractValue(jtoken.Name, jtoken.Value.ToString()) as IBaseClassIfc;
						if (val != null)
							return (T)val;
						return default(T);
					}
				}


				token = obj.GetValue("type", StringComparison.InvariantCultureIgnoreCase);
				if (token != null)
				{
					Type nominatedType = BaseClassIfc.GetType(token.Value<string>());
					if (nominatedType != null)
						type = nominatedType;
				}
				string hrefId = "";
				token = obj.GetValue("id", StringComparison.InvariantCultureIgnoreCase);
				if (token != null)
					hrefId = token.Value<string>();
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

							IfcCartesianPoint point = result as IfcCartesianPoint;
							if (point != null)
							{
								point.parseJObject(obj);
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
									direction.parseJObject(obj);
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
										placement.parseJObject(obj);
										if (placement.IsXYPlane(Tolerance))
										{
											result = Factory.XYPlanePlacement;
											common = true;
										}
									}
								}
							}
							token = obj.GetValue("id", StringComparison.InvariantCultureIgnoreCase);
							if (!string.IsNullOrEmpty(hrefId))
							{
								if (!(result is IfcRoot))
									result.setGlobalId(hrefId);
								mDictionary.TryAdd(hrefId, result);
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
			result.parseJObject(obj);
			parseBespoke(result, obj);
			IfcRoot root = result as IfcRoot;
			if (root != null)
				mDictionary[root.GlobalId] = root;
			return (T)(IBaseClassIfc)result;
		}

		partial void parseBespoke(BaseClassIfc entity, JObject jObject);
		public JObject JSON() { return ToJSON(""); }
		public JObject ToJSON(string filename)
		{
			BaseClassIfc.SetJsonOptions options = new BaseClassIfc.SetJsonOptions() { };
			return ToJSON(filename, options);
		}
		public JObject ToJSON(string filename, BaseClassIfc.SetJsonOptions options)
		{ 
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

			options.Version = mRelease;
			options.LengthDigitCount = mLengthDigits;

			JObject ifcFile = new JObject();
			JObject header = new JObject();
			JObject fileDescription = new JObject();
			fileDescription["description"] = "ViewDefinition[" + viewDefinition + "]";
			fileDescription["implementation_level"] = "2;1";
			header["FILE_DESCRIPTION"] = fileDescription;
			JObject fileName = new JObject();
			fileName["name"] = filename;
			DateTime now = DateTime.Now;
			fileName["time_stamp"] = now.Year + "-" + (now.Month < 10 ? "0" : "") + now.Month + "-" + (now.Day < 10 ? "0" : "") + now.Day + "T" + (now.Hour < 10 ? "0" : "") + now.Hour + ":" + (now.Minute < 10 ? "0" : "") + now.Minute + ":" + (now.Second < 10 ? "0" : "") + now.Second;
			fileName["author"] = System.Environment.UserName;
			fileName["organization"] = IfcOrganization.Organization;
			fileName["preprocessor_version"] = mFactory.ToolkitName;
			fileName["originating_system"] = Factory.ApplicationFullName;
			fileName["authorization"] = "None";
			header["FILE_NAME"] = fileName;
			JObject fileSchema = new JObject();
			fileSchema["schema_identifiers"] = mRelease == ReleaseVersion.IFC2x3 ? "IFC2X3" : "IFC4";
			header["FILE_SCHEMA"] = fileSchema;
			ifcFile["HEADER"] = header;

			JArray data = new JArray();
			IfcContext context = this.mContext;
			if (context != null)
			{
				JObject jcontext = context.getJson(null, options); //null);//
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
			if (!string.IsNullOrEmpty(filename))
			{
				StreamWriter sw = new StreamWriter(filename);
#if(DEBUG)
				sw.Write(ifcFile.ToString());
#else
				sw.Write(ifcFile.ToString(Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonConverter[0]));
#endif
				sw.Close();
			}
			Thread.CurrentThread.CurrentUICulture = current;
			return ifcFile;
			
		}
		
		internal static JObject extract(IfcValue value)
		{
			JObject result = new JObject();
			result[value.GetType().Name] = value.ValueString;
			return result;
		}
		internal static IfcValue ParseValue(JObject obj)
		{
			JProperty token = (JProperty) obj.First;
			return ParserIfc.extractValue(token.Name, token.Value.ToString());
		}
	}

	public static class JsonIFCExtensions
	{
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
	}
}

 