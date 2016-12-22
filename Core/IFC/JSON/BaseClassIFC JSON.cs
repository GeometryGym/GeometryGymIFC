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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class BaseClassIfc : STEPEntity, IBaseClassIfc
	{
		internal virtual void parseJObject(JObject obj) { }
		//internal JObject obj = null;
		internal JObject getJson(BaseClassIfc host, HashSet<int> processed)
		{
			if(processed.Contains(mIndex))
			{
				JObject jobj = new JObject();
				jobj["href"] = mIndex;
				return jobj;
			}

			JObject obj = new JObject();
			processed.Add(mIndex);
			obj["type"] = KeyWord; 
			setJSON(obj, host, processed);
			return obj;
		}
		protected virtual void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			obj["id"] = mIndex;
		}
		protected T extractObject<T>(JObject obj) where T : IBaseClassIfc
		{
			return (obj == null ? default(T) : mDatabase.parseJObject<T>(obj));
		}
		protected string extractString(JToken token) { return (token == null ? "" : token.Value<string>()); }
		protected void setAttribute(JObject obj, string attribute, string value)
		{
			if (!string.IsNullOrEmpty(value))
				obj[attribute] = value;
		}
		protected void createArray(JObject obj,string name, IEnumerable<IBaseClassIfc> objects, BaseClassIfc host, HashSet<int> processed)
		{
			if (objects.Count() == 0)
				return;
			obj[name] = new JArray( objects.ToList().ConvertAll(x => mDatabase[x.Index].getJson(host, processed)));
		}
		
	}
}
