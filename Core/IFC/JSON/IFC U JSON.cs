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

#if (NET || !NOIFCJSON)
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
	public partial class IfcUnitaryEquipment
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcUnitaryEquipmentTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcUnitaryEquipmentTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcUnitaryEquipmentType 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcUnitaryEquipmentTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcUnitaryEquipmentTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcUnitAssignment
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Units.AddRange(mDatabase.extractJsonArray<IfcUnit>(obj["Units"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			foreach (IfcUnit unit in mUnits)
				array.Add(mDatabase[unit.StepId].getJson(this, options));
			obj["Units"] = array;
		}
	}
	public partial class IfcUShapeProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mDepth = extractDouble(obj["Depth"]);
			mFlangeWidth = extractDouble(obj["FlangeWidth"]);
			mWebThickness = extractDouble(obj["WebThickness"]);
			mFlangeThickness = extractDouble(obj["FlangeThickness"]);
			mFilletRadius = extractDouble(obj["FilletRadius"]);
			mEdgeRadius = extractDouble(obj["EdgeRadius"]);
			mFlangeSlope =	extractDouble(obj["FlangeSlope"]);
			mCentreOfGravityInX = extractDouble(obj["mCentreOfGravityInX"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Depth"] = formatLength(mDepth);
			obj["FlangeWidth"] = formatLength(mFlangeWidth);
			obj["WebThickness"] = formatLength(mWebThickness);
			obj["FlangeThickness"] = formatLength(mFlangeThickness);
			if (!double.IsNaN(mFilletRadius))
				obj["FilletRadius"] = formatLength(mFilletRadius);
			if (!double.IsNaN(mEdgeRadius))
				obj["EdgeRadius"] = formatLength(mEdgeRadius);
			if (!double.IsNaN(mFlangeSlope))
				obj["FlangeSlope"] = mFlangeSlope;
			if(options.Version < ReleaseVersion.IFC4 && !double.IsNaN(mCentreOfGravityInX))
				obj["mCentreOfGravityInX"] = formatLength(mCentreOfGravityInX);
		}
	}
}
#endif
