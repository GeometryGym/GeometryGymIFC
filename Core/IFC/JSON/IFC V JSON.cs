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

#if (!NOIFCJSON)
#if (NEWTONSOFT)
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonObject = Newtonsoft.Json.Linq.JObject;
using JsonArray = Newtonsoft.Json.Linq.JArray;
#else
using System.Text.Json.Nodes;
#endif

namespace GeometryGym.Ifc
{
	public abstract partial class IfcValue : IfcMetricValueSelect //SELECT(IfcMeasureValue,IfcSimpleValue,IfcDerivedMeasureValue); stpentity parse method
	{
		public JsonObject getJson(BaseClassIfc host, BaseClassIfc.SetJsonOptions options)
		{
			return DatabaseIfc.extract(this);
		}
	}
	public partial class IfcValveType : IfcFlowControllerType
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcValveTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcValveTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcVector
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Orientation"] = Orientation.getJson(this, options);
			obj["Magnitude"] = Magnitude;
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Pnt"] as JsonObject;
			if (jobj != null)
				Orientation = mDatabase.ParseJsonObject<IfcDirection>(jobj);
			var node = obj["Depth"];
			if (node != null)
				mMagnitude = node.GetValue<double>();
		}
	}
	public partial class IfcVertexPoint : IfcVertex, IfcPointOrVertexPoint
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject rp = obj["VertexGeometry"] as JsonObject;
			if (rp != null)
				VertexGeometry = mDatabase.ParseJsonObject<IfcPoint>(rp);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
				obj["VertexGeometry"] = VertexGeometry.getJson(this, options);
		}
	}
}
#endif
