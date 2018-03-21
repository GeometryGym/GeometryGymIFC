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
//using System.Drawing;

using Newtonsoft.Json.Linq;

namespace GeometryGym.Ifc
{
	public partial class IfcGeneralProfileProperties : IfcProfileProperties //DELETED IFC4  SUPERTYPE OF	(IfcStructuralProfileProperties)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PhysicalWeight", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mPhysicalWeight);
			token = obj.GetValue("Perimeter", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mPerimeter);
			token = obj.GetValue("MinimumPlateThickness", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mMinimumPlateThickness);
			token = obj.GetValue("MaximumPlateThickness", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mMaximumPlateThickness);
			token = obj.GetValue("CrossSectionArea", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mCrossSectionArea);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mPhysicalWeight))
				obj["PhysicalWeight"] = mPhysicalWeight;
			if (!double.IsNaN(mPerimeter))
				obj["Perimeter"] = mPerimeter;
			if (!double.IsNaN(mMinimumPlateThickness))
				obj["MinimumPlateThickness"] = mMinimumPlateThickness;
			if (!double.IsNaN(mMaximumPlateThickness))
				obj["MaximumPlateThickness"] = mMaximumPlateThickness;
			if (!double.IsNaN(mCrossSectionArea))
				obj["CrossSectionArea"] = mCrossSectionArea;
		}
	}
	public partial class IfcGeographicElement : IfcElement  //IFC4
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcGeographicElementTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcGeometricRepresentationContext : IfcRepresentationContext, IfcCoordinateReferenceSystemSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("CoordinateSpaceDimension", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				CoordinateSpaceDimension = token.Value<int>();
			token = obj.GetValue("Precision", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Precision = token.Value<double>();
			WorldCoordinateSystem = mDatabase.parseJObject<IfcAxis2Placement>(obj.GetValue("WorldCoordinateSystem", StringComparison.InvariantCultureIgnoreCase) as JObject);
			TrueNorth = mDatabase.parseJObject<IfcDirection>(obj.GetValue("TrueNorth", StringComparison.InvariantCultureIgnoreCase) as JObject);

			List<IfcGeometricRepresentationSubContext> subs = mDatabase.extractJArray<IfcGeometricRepresentationSubContext>(obj.GetValue("HasSubContexts", StringComparison.InvariantCultureIgnoreCase) as JArray);
			foreach (IfcGeometricRepresentationSubContext sub in subs)
				sub.ContainerContext = this;
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (this as IfcGeometricRepresentationSubContext == null)
			{
				if (mCoordinateSpaceDimension > 0)
					obj["CoordinateSpaceDimension"] = CoordinateSpaceDimension;
				if (mPrecision > 0)
					obj["Precision"] = Precision;
				obj["WorldCoordinateSystem"] = mDatabase[mWorldCoordinateSystem.Index].getJson(this, options);
				if (mTrueNorth != null)
					obj["TrueNorth"] = TrueNorth.getJson(this, options);
			}

			JArray arr = new JArray();
			foreach (IfcGeometricRepresentationSubContext sub in HasSubContexts)
			{
				if (sub.mIndex != host.mIndex)
					arr.Add(sub.getJson(this, options));
			}
			if (arr.Count > 0)
				obj["HasSubContexts"] = arr;

			if (mHasCoordinateOperation != null)
				obj["HasCoordinateOperation"] = mHasCoordinateOperation.getJson(this, options);
		}
	}
	public partial class IfcGeometricRepresentationSubContext : IfcGeometricRepresentationContext
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("TargetScale", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				TargetScale = token.Value<double>();
			token = obj.GetValue("TargetView", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcGeometricProjectionEnum>(token.Value<string>(), out mTargetView);
			token = obj.GetValue("UserDefinedTargetView", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				UserDefinedTargetView = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			if (options.Style == SetJsonOptions.JsonStyle.Repository && string.IsNullOrEmpty(mGlobalId))
			{
				mGlobalId = ParserIfc.EncodeGuid(Guid.NewGuid());
				options.Encountered.Add(mGlobalId);
			}
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mTargetScale))
				obj["TargetScale"] = mTargetScale.ToString();
			obj["TargetView"] = TargetView.ToString();
			setAttribute(obj, "UserDefinedTargetView", UserDefinedTargetView);
		}
	}
	public partial class IfcGeometricSet : IfcGeometricRepresentationItem //SUPERTYPE OF(IfcGeometricCurveSet)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mElements.AddRange(mDatabase.extractJArray<IfcGeometricSetSelect>(obj.GetValue("Elements", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JArray array = new JArray();
			foreach (IfcGeometricSetSelect gs in mElements)
				array.Add(gs.getJson(this, options));
			obj["Elements"] = array;
		}
	}

	public partial class IfcGrid : IfcProduct
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["UAxes"] = new JArray(UAxes.ConvertAll(x => x.getJson(this, options)));
			obj["VAxes"] = new JArray(VAxes.ConvertAll(x => x.getJson(this, options)));
			if(WAxes.Count > 0)
				obj["WAxes"] = new JArray(WAxes.ConvertAll(x => x.getJson(this, options)));
		}
	}
	public partial class IfcGridAxis : BaseClassIfc
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "AxisTag", AxisTag);
			obj["AxisCurve"] = AxisCurve.getJson(this, options);
			obj["SameSense"] = SameSense;
		}
	}
	public partial class IfcGroup : IfcObject //SUPERTYPE OF (ONEOF (IfcAsset ,IfcCondition ,IfcInventory ,IfcStructuralLoadGroup ,IfcStructuralResultGroup ,IfcSystem ,IfcZone))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			foreach (IfcRelAssignsToGroup rag in mDatabase.extractJArray<IfcRelAssignsToGroup>(obj.GetValue("IsGroupedBy", StringComparison.InvariantCultureIgnoreCase) as JArray))
				rag.RelatingGroup = this;
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JArray array = new JArray();
			foreach (IfcRelAssignsToGroup rag in mIsGroupedBy)
				array.Add(rag.getJson(this, options));
			obj["IsGroupedBy"] = array;
		}
	}
}
