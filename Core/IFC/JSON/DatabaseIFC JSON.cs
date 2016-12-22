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
		internal void ReadJSONFile(string filename) { ReadJSONFile(new StreamReader(filename)); }
		internal void ReadJSONFile(StreamReader reader)
		{
			Release = ReleaseVersion.IFC4;
			ReadJSON(JObject.Parse(reader.ReadToEnd()));
			
		}

		internal int mNextUnassigned = 0;

		internal void ReadJSON(JObject ifcFile)
		{ 
			JToken token = ifcFile.First;
			token = ifcFile["HEADER"];
			token = ifcFile["DATA"];
			JArray array = token as JArray;
			if (array != null)
				extractJArray<IBaseClassIfc>(array);	
			else
			{
				parseJObject<IBaseClassIfc>(ifcFile);	
			}
			if(mContext != null)
				mContext.initializeUnitsAndScales();
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
			JToken token = obj.GetValue("href", StringComparison.InvariantCultureIgnoreCase);
			if(token != null)
			{
				int index = token.Value<int>();
				try
				{
					return (T)(IBaseClassIfc)this[index];
				}
				catch(Exception) { }
				return default(T);
			}
			Type type = null;
			token = obj.GetValue("type", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
			{
				string keyword = token.Value<string>();
				type = Type.GetType("GeometryGym.Ifc." + keyword, false, true);
			}
			if (token == null)
				type = typeof(T);
			if (type != null)
			{
				ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
null, Type.EmptyTypes, null);
				if (constructor != null)
				{ 
					BaseClassIfc entity = constructor.Invoke(new object[] { }) as BaseClassIfc;
					if (entity != null)
					{
						token = obj.GetValue("id", StringComparison.InvariantCultureIgnoreCase);
						int index = 0;// (int) (this.mIfcObjects.Count * 1.2); 
						if (token != null)
						{
							int i = token.Value<int>();
							if (this[i] == null)
								index = i;
							// TODO merge if existing equivalent
						}
						if(index == 0)
						{
							if(mNextUnassigned == 0)
								mNextUnassigned = Math.Max(mIfcObjects.Count * 2, 1000);
							index = mNextUnassigned++;
						}
						this[index] = entity;
						entity.parseJObject(obj);
						parseBespoke(entity, obj);	
						return (T)(IBaseClassIfc) entity;
					}
				}
				
			}
			return default(T);
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

			IfcContext context = this.mContext;
			JObject jcontext = context.getJson(null, new HashSet<int>()); //null);//

			JArray data = new JArray();
			data.Add(jcontext);
			ifcFile["DATA"] = data;
			if (!string.IsNullOrEmpty(filename))
			{
				StreamWriter sw = new StreamWriter(filename);
				sw.Write(ifcFile.ToString());
				sw.Close();
			}
			Thread.CurrentThread.CurrentUICulture = current;
			return ifcFile;
			
		}
		
		internal static JObject extract(IfcValue value)
		{
			JObject result = new JObject();
			result[value.GetType().Name] = value.Value.ToString();
			return result;
		}
		internal static IfcValue ParseValue(JObject obj)
		{
			JProperty token = (JProperty) obj.First;
			return ParserIfc.extractValue(token.Name, token.Value.ToString());
		}
	}

}

 