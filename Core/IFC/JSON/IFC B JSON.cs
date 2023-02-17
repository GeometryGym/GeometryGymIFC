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
	public partial class IfcBeam
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcBeamTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcBeamTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcBeamType
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcBeamTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcBeamTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcBlock
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["XLength"] = XLength;
			obj["YLength"] = YLength;
			obj["ZLength"] = ZLength;
		}
	}
	public partial class IfcBoiler
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcBoilerTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcBoilerTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcBoilerType
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcBoilerTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcBoilerTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcBooleanResult
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Operator"];
			if (node != null)
				Enum.TryParse<IfcBooleanOperator>(node.GetValue<string>(), true, out mOperator);
			FirstOperand = mDatabase.ParseJsonObject<IfcBooleanOperand>(obj["FirstOperand"] as JsonObject);
			SecondOperand = mDatabase.ParseJsonObject<IfcBooleanOperand>(obj["SecondOperand"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Operator"] = mOperator.ToString();
			obj["FirstOperand"] = mFirstOperand.getJson(this, options);
			obj["SecondOperand"] = mSecondOperand.getJson(this, options);
		}
	}
	public partial class IfcBoundaryCondition 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Name = extractString(obj, "Name");
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "Name", Name);
		}
	}
	public partial class IfcBoundaryNodeCondition : IfcBoundaryCondition
	{

		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var jobj = obj["TranslationalStiffnessX"] as JsonObject;
			if (jobj != null)
				mTranslationalStiffnessX = IfcTranslationalStiffnessSelect.parseJsonObject(jobj);
			else
			{
				jobj = obj["LinearStiffnessX"] as JsonObject;
				if (jobj != null)
					mTranslationalStiffnessX = IfcTranslationalStiffnessSelect.Parse(jobj.GetValue<double>().ToString(), mDatabase.Release);
			}
			jobj = obj["TranslationalStiffnessY"] as JsonObject;
			if (jobj != null)
				mTranslationalStiffnessY = IfcTranslationalStiffnessSelect.parseJsonObject(jobj);
			else
			{
				jobj = obj["LinearStiffnessY"] as JsonObject;
				if (jobj != null)
					mTranslationalStiffnessY = IfcTranslationalStiffnessSelect.Parse(jobj.GetValue<double>().ToString(), mDatabase.Release);
			}
			jobj = obj["TranslationalStiffnessZ"] as JsonObject;
			if (jobj != null)
				mTranslationalStiffnessZ = IfcTranslationalStiffnessSelect.parseJsonObject(jobj);
			else
			{
				jobj = obj["LinearStiffnessZ"] as JsonObject;
				if (jobj != null)
					mTranslationalStiffnessZ = IfcTranslationalStiffnessSelect.Parse(jobj.GetValue<double>().ToString(), mDatabase.Release);
			}
			jobj = obj["RotationalStiffnessX"] as JsonObject;
			if (jobj != null)
				mRotationalStiffnessX = IfcRotationalStiffnessSelect.parseJsonObject(jobj);
			jobj = obj["RotationalStiffnessY"] as JsonObject;
			if (jobj != null)
				mRotationalStiffnessY = IfcRotationalStiffnessSelect.parseJsonObject(jobj);
			jobj = obj["RotationalStiffnessZ"] as JsonObject;
			if (jobj != null)
				mRotationalStiffnessZ = IfcRotationalStiffnessSelect.parseJsonObject(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mDatabase.Release < ReleaseVersion.IFC4)
			{
				if (mTranslationalStiffnessX != null)
					obj["LinearStiffnessX"] = (mTranslationalStiffnessX.mStiffness == null ? (mTranslationalStiffnessX.Rigid ? -1 : 0) : mTranslationalStiffnessX.mStiffness.Measure);
				if (mTranslationalStiffnessY != null)
					obj["LinearStiffnessY"] = (mTranslationalStiffnessY.mStiffness == null ? (mTranslationalStiffnessY.Rigid ? -1 : 0) : mTranslationalStiffnessX.mStiffness.Measure);
				if (mTranslationalStiffnessZ != null)
					obj["LinearStiffnessZ"] = (mTranslationalStiffnessZ.mStiffness == null ? (mTranslationalStiffnessZ.Rigid ? -1 : 0) : mTranslationalStiffnessX.mStiffness.Measure);
				if (mRotationalStiffnessX != null)
					obj["RotationalStiffnessX"] = (mRotationalStiffnessX.mStiffness == null ? (mRotationalStiffnessX.Rigid ? -1 : 0) : mRotationalStiffnessX.mStiffness.Measure);
				if (mRotationalStiffnessY != null)
					obj["RotationalStiffnessY"] = (mRotationalStiffnessY.mStiffness == null ? (mRotationalStiffnessY.Rigid ? -1 : 0) : mRotationalStiffnessY.mStiffness.Measure);
				if (mTranslationalStiffnessZ != null)
					obj["RotationalStiffnessZ"] = (mRotationalStiffnessZ.mStiffness == null ? (mRotationalStiffnessZ.Rigid ? -1 : 0) : mRotationalStiffnessZ.mStiffness.Measure);
			}
			else
			{
				if (mTranslationalStiffnessX != null)
					obj["TranslationalStiffnessX"] = mTranslationalStiffnessX.getJsonObject();
				if (mTranslationalStiffnessY != null)
					obj["TranslationalStiffnessY"] = mTranslationalStiffnessY.getJsonObject();
				if (mTranslationalStiffnessZ != null)
					obj["TranslationalStiffnessZ"] = mTranslationalStiffnessZ.getJsonObject();
				if (mRotationalStiffnessX != null)
					obj["RotationalStiffnessX"] = mRotationalStiffnessX.getJsonObject();
				if (mRotationalStiffnessY != null)
					obj["RotationalStiffnessY"] = mRotationalStiffnessY.getJsonObject();
				if (mTranslationalStiffnessZ != null)
					obj["RotationalStiffnessZ"] = mRotationalStiffnessZ.getJsonObject();
			}
		}
	}
	public partial class IfcBSplineCurve
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Degree"] = Degree;
			JsonArray array = new JsonArray();
			foreach (IfcCartesianPoint point in ControlPointsList)
				array.Add(point.getJson(this, options));
			obj["ControlPointsList"] = array;
			obj["CurveForm"] = CurveForm.ToString();
			if(ClosedCurve != IfcLogicalEnum.UNKNOWN)
				obj["ClosedCurve"] = ClosedCurve == IfcLogicalEnum.TRUE;
			if (SelfIntersect != IfcLogicalEnum.UNKNOWN)
				obj["SelfIntersect"] = SelfIntersect == IfcLogicalEnum.TRUE;
		}
	}
	public partial class IfcBSplineCurveWithKnots
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			foreach (int i in mKnotMultiplicities)
				array.Add(i);
			obj["KnotMultiplicities"] = array;
			array = new JsonArray();
			foreach (int i in mKnots)
				array.Add(i);
			obj["Knots"] = array;
			obj["KnotSpec"] = mKnotSpec.ToString();
		}
	}
	public partial class IfcBSplineSurface
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["UDegree"] = UDegree;
			obj["VDegree"] = VDegree;
			JsonArray array = new JsonArray();
			foreach (var points in ControlPointsList)
			{
				JsonArray sub = new JsonArray();
				foreach (var point in points)
					sub.Add(point.getJson(this, options));
				array.Add(sub);
			}
			obj["ControlPointsList"] = array;
			obj["SurfaceForm"] = SurfaceForm.ToString();
			if(UClosed != IfcLogicalEnum.UNKNOWN)
				obj["UClosed"] = UClosed == IfcLogicalEnum.TRUE;
			if(VClosed != IfcLogicalEnum.UNKNOWN)
				obj["VClosed"] = VClosed == IfcLogicalEnum.TRUE;
			if (SelfIntersect != IfcLogicalEnum.UNKNOWN)
				obj["SelfIntersect"] = SelfIntersect == IfcLogicalEnum.TRUE;
		}
	}
	public partial class IfcBSplineSurfaceWithKnots
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			foreach (int i in mUMultiplicities)
				array.Add(i);
			obj["UMultiplicities"] = array;
			array = new JsonArray();
			foreach (int i in mVMultiplicities)
				array.Add(i);
			obj["VMultiplicities"] = array;
			array = new JsonArray();
			foreach (int i in mUKnots)
				array.Add(i);
			obj["UKnots"] = array;
			array = new JsonArray();
			foreach (int i in mVKnots)
				array.Add(i);
			obj["VKnots"] = array;
			obj["KnotSpec"] = mKnotSpec.ToString();
		}
	}
	public partial class IfcBuilding 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mElevationOfRefHeight = extractDouble(obj["ElevationOfRefHeight"]);
			mElevationOfTerrain = extractDouble(obj["ElevationOfTerrain"]);
			JsonObject jobj = obj["BuildingAddress"] as JsonObject;
			if (jobj != null)
				BuildingAddress = mDatabase.ParseJsonObject<IfcPostalAddress>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mElevationOfRefHeight))
				obj["ElevationOfRefHeight"] = ElevationOfRefHeight;
			if (!double.IsNaN(mElevationOfTerrain))
				obj["ElevationOfTerrain"] = ElevationOfTerrain;
			if (mBuildingAddress != null) 
				obj["BuildingAddress"] = BuildingAddress.getJson(this, options);
		}
	}
	public partial class IfcBuildingStorey
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Elevation"];
			if (node != null)
				Elevation = node.GetValue<double>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mElevation))
				obj["Elevation"] = Elevation;
		}
	}
}
#endif
