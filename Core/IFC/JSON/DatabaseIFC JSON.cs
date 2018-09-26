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
		internal void ReadJSONFile(string filename) { FileName = filename; ReadJSONFile(new StreamReader(filename)); }
		internal void ReadJSONFile(TextReader reader)
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
				IBaseClassIfc obj = parseJObject<IBaseClassIfc>(ifcFile);
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
					T entity = parseJObject<T>(obj);
					if (entity != null)
						result.Add(entity);
				}
				else
				{
					logError("Unrecognized token");
				}
			}
			return result;
		}
		internal T parseJObject<T>(JObject obj) where T : IBaseClassIfc
		{
			if (obj == null)
				return default(T);
			BaseClassIfc result = null;
			JToken token = obj.GetValue("href", StringComparison.InvariantCultureIgnoreCase);
			if(token != null)
			{
				if (token.Type == JTokenType.Integer)
				{
					int index = token.Value<int>();
					result = this[index];
				}
				else if (token.Type == JTokenType.String)
				{
					mDictionary.TryGetValue(token.Value<string>(), out result);
				}
				if (result != null && obj.Count == 1)
					return (T)(IBaseClassIfc)result;
			}
			if (result == null)
			{
				Type type = null;
				token = obj.GetValue("type", StringComparison.InvariantCultureIgnoreCase);
				if (token != null)
				{
					string keyword = token.Value<string>();
					type = Type.GetType("GeometryGym.Ifc." + keyword, false, true);
				}
				if (token == null)
					type = typeof(T);
				if (type != null && !type.IsAbstract)
				{
					ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
	null, Type.EmptyTypes, null);
					if (constructor != null)
					{
						result  = constructor.Invoke(new object[] { }) as BaseClassIfc;
						if (result != null)
						{
							result.mDatabase = this;
							token = obj.GetValue("id", StringComparison.InvariantCultureIgnoreCase);
							int index = 0;// (int) (this.mIfcObjects.Count * 1.2); 
							if (token != null)
							{
								if (token.Type == JTokenType.Integer)
								{
									try
									{
										int i = token.Value<int>();
										if (this[i] == null)
											index = i;
									}
									catch (Exception) { }
									// TODO merge if existing equivalent
								}
								else if(token.Type == JTokenType.String)
								{
									result.mGlobalId = token.Value<string>();
									mDictionary.TryAdd(result.mGlobalId, result);
								}
							}
							IfcCartesianPoint point = result as IfcCartesianPoint;
							IfcDirection direction = result as IfcDirection;
							IfcAxis2Placement3D placement = result as IfcAxis2Placement3D;
							if (index == 0)
							{
								if (point != null)
								{
									point.parseJObject(obj);
									if (point.isOrigin)
									{
										if (point.is2D)
											return (T)(IBaseClassIfc)Factory.Origin2d;
										return (T)(IBaseClassIfc)Factory.Origin;
									}
								}
								else
								{
									if (direction != null)
									{
										direction.parseJObject(obj);
										if (!direction.is2D)
										{
											if (direction.isXAxis)
												return (T)(IBaseClassIfc)Factory.XAxis;
											if (direction.isYAxis)
												return (T)(IBaseClassIfc)Factory.YAxis;
											if (direction.isZAxis)
												return (T)(IBaseClassIfc)Factory.ZAxis;
											if (direction.isXAxisNegative)
												return (T)(IBaseClassIfc)Factory.XAxisNegative;
											if (direction.isYAxisNegative)
												return (T)(IBaseClassIfc)Factory.YAxisNegative;
											if (direction.isZAxisNegative)
												return (T)(IBaseClassIfc)Factory.ZAxisNegative;
										}
									}
									if (placement != null)
									{
										placement.parseJObject(obj);
										if (placement.IsXYPlane)
											return (T)(IBaseClassIfc)Factory.XYPlanePlacement;
									}
								}
							
								if (mNextUnassigned == 0)
									mNextUnassigned = Math.Max(LastKey * 2, 1000);
								index = mNextUnassigned++;
								this[index] = result;

								if (point != null || direction != null || placement != null)
									return (T)(IBaseClassIfc)result;
							}
							else
								this[index] = result;
							
						}
					}
				}
			}
			if(result == null)
				return default(T);
			result.parseJObject(obj);
			parseBespoke(result, obj);
			return (T)(IBaseClassIfc)result;
		}

		partial void parseBespoke(BaseClassIfc entity, JObject jObject);
		public JObject JSON { get { return ToJSON(""); } }
		public JObject ToJSON(string filename)
		{
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");			

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
			BaseClassIfc.SetJsonOptions options = new BaseClassIfc.SetJsonOptions() { LengthDigitCount = mLengthDigits, Version = this.Release };
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
				sw.Write(ifcFile.ToString(Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonConverter[0]));
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

 