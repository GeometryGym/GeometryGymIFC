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

using Newtonsoft.Json.Linq;

namespace GeometryGym.Ifc
{
	public partial class IfcBeam : IfcBuildingElement
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcBeamTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcBeamTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcBeamType : IfcBuildingElementType
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcBeamTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcBeamTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcBlock : IfcCsgPrimitive3D
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["XLength"] = XLength.ToString();
			obj["YLength"] = YLength.ToString();
			obj["ZLength"] = ZLength.ToString();
		}
	}
	public partial class IfcBoiler : IfcEnergyConversionDevice
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcBoilerTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcBoilerTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcBoilerType : IfcEnergyConversionDeviceType
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcBoilerTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcBoilerTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcBooleanResult : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Operator", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcBooleanOperator>(token.Value<string>(), true, out mOperator);
			JObject jobj = obj.GetValue("FirstOperand", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				FirstOperand = mDatabase.parseJObject<IfcBooleanOperand>(jobj);
			jobj = obj.GetValue("SecondOperand", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				SecondOperand = mDatabase.parseJObject<IfcBooleanOperand>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Operator"] = mOperator.ToString();
			obj["FirstOperand"] = mDatabase[mFirstOperand].getJson(this, options);
			obj["SecondOperand"] = mDatabase[mSecondOperand].getJson(this, options);
		}
	}
	public abstract partial class IfcBoundaryCondition : BaseClassIfc //ABSTRACT SUPERTYPE OF (ONEOF (IfcBoundaryEdgeCondition ,IfcBoundaryFaceCondition ,IfcBoundaryNodeCondition));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);

			JToken token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "Name", Name);
		}
	}
	public partial class IfcBoundaryNodeCondition : IfcBoundaryCondition
	{

		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("TranslationalStiffnessX", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				mTranslationalStiffnessX = IfcTranslationalStiffnessSelect.parseJObject(jobj);
			else
			{
				jobj = obj.GetValue("LinearStiffnessX", StringComparison.InvariantCultureIgnoreCase) as JObject;
				if (jobj != null)
					mTranslationalStiffnessX = IfcTranslationalStiffnessSelect.Parse(jobj.Value<double>().ToString(), mDatabase.Release);
			}
			jobj = obj.GetValue("TranslationalStiffnessY", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				mTranslationalStiffnessY = IfcTranslationalStiffnessSelect.parseJObject(jobj);
			else
			{
				jobj = obj.GetValue("LinearStiffnessY", StringComparison.InvariantCultureIgnoreCase) as JObject;
				if (jobj != null)
					mTranslationalStiffnessY = IfcTranslationalStiffnessSelect.Parse(jobj.Value<double>().ToString(), mDatabase.Release);
			}
			jobj = obj.GetValue("TranslationalStiffnessZ", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				mTranslationalStiffnessZ = IfcTranslationalStiffnessSelect.parseJObject(jobj);
			else
			{
				jobj = obj.GetValue("LinearStiffnessZ", StringComparison.InvariantCultureIgnoreCase) as JObject;
				if (jobj != null)
					mTranslationalStiffnessZ = IfcTranslationalStiffnessSelect.Parse(jobj.Value<double>().ToString(), mDatabase.Release);
			}
			jobj = obj.GetValue("RotationalStiffnessX", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				mRotationalStiffnessX = IfcRotationalStiffnessSelect.parseJObject(jobj);
			jobj = obj.GetValue("RotationalStiffnessY", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				mRotationalStiffnessY = IfcRotationalStiffnessSelect.parseJObject(jobj);
			jobj = obj.GetValue("RotationalStiffnessZ", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				mRotationalStiffnessZ = IfcRotationalStiffnessSelect.parseJObject(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
					obj["TranslationalStiffnessX"] = mTranslationalStiffnessX.getJObject();
				if (mTranslationalStiffnessY != null)
					obj["TranslationalStiffnessY"] = mTranslationalStiffnessY.getJObject();
				if (mTranslationalStiffnessZ != null)
					obj["TranslationalStiffnessZ"] = mTranslationalStiffnessZ.getJObject();
				if (mRotationalStiffnessX != null)
					obj["RotationalStiffnessX"] = mRotationalStiffnessX.getJObject();
				if (mRotationalStiffnessY != null)
					obj["RotationalStiffnessY"] = mRotationalStiffnessY.getJObject();
				if (mTranslationalStiffnessZ != null)
					obj["RotationalStiffnessZ"] = mRotationalStiffnessZ.getJObject();
			}
		}
	}
	public abstract partial class IfcBSplineCurve : IfcBoundedCurve //SUPERTYPE OF(IfcBSplineCurveWithKnots)
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Degree"] = Degree;
			JArray array = new JArray();
			foreach (IfcCartesianPoint point in ControlPointsList)
				array.Add(point.getJson(this, options));
			obj["ControlPointsList"] = array;
			obj["CurveForm"] = CurveForm.ToString();
			obj["ClosedCurve"] = ClosedCurve.ToString();
			obj["SelfIntersect"] = SelfIntersect.ToString();
		}
	}
	public partial class IfcBSplineCurveWithKnots : IfcBSplineCurve
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JArray array = new JArray();
			foreach (int i in mMultiplicities)
				array.Add(i);
			obj["Multiplicities"] = array;
			array = new JArray();
			foreach (int i in mKnots)
				array.Add(i);
			obj["Knots"] = array;
			obj["KnotSpec"] = mKnotSpec.ToString();
		}
	}
	public partial class IfcBuilding : IfcFacility
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("BuildingAddress", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				BuildingAddress = mDatabase.parseJObject<IfcPostalAddress>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mBuildingAddress != null) 
				obj["BuildingAddress"] = BuildingAddress.getJson(this, options);
		}
	}
	public partial class IfcBuildingStorey : IfcFacilityPart
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Elevation", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Elevation = token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mElevation))
				obj["Elevation"] = Elevation.ToString();
		}
	}
}
