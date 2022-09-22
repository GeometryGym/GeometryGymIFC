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
	public partial class IfcGeneralProfileProperties : IfcProfileProperties //DELETED IFC4  SUPERTYPE OF	(IfcStructuralProfileProperties)
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PhysicalWeight"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mPhysicalWeight);
			node = obj["Perimeter"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mPerimeter);
			node = obj["MinimumPlateThickness"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mMinimumPlateThickness);
			node = obj["MaximumPlateThickness"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mMaximumPlateThickness);
			node = obj["CrossSectionArea"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mCrossSectionArea);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
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
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcGeographicElementTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcGeometricRepresentationContext
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["CoordinateSpaceDimension"];
			if (node != null)
				CoordinateSpaceDimension = node.GetValue<int>();
			node = obj["Precision"];
			if (node != null)
				Precision = node.GetValue<double>();
			WorldCoordinateSystem = mDatabase.ParseJsonObject<IfcAxis2Placement>(obj["WorldCoordinateSystem"] as JsonObject);
			TrueNorth = mDatabase.ParseJsonObject<IfcDirection>(obj["TrueNorth"] as JsonObject);

			List<IfcGeometricRepresentationSubContext> subs = mDatabase.extractJsonArray<IfcGeometricRepresentationSubContext>(obj["HasSubContexts"] as JsonArray);
			foreach (IfcGeometricRepresentationSubContext sub in subs)
				sub.ParentContext = this;

			foreach (IfcShapeModel shape in mDatabase.extractJsonArray<IfcShapeModel>(obj["RepresentationsInContext"] as JsonArray))
				mRepresentationsInContext.Add(shape);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (this as IfcGeometricRepresentationSubContext == null)
			{
				if (mCoordinateSpaceDimension > 0)
					obj["CoordinateSpaceDimension"] = CoordinateSpaceDimension;
				if (mPrecision > 0)
					obj["Precision"] = Precision;
				obj["WorldCoordinateSystem"] = mDatabase[mWorldCoordinateSystem.StepId].getJson(this, options);
				if (mTrueNorth != null)
					obj["TrueNorth"] = TrueNorth.getJson(this, options);
			}

			JsonArray arr = new JsonArray();
			if (!(host is IfcGeometricRepresentationSubContext))
			{
				foreach (IfcGeometricRepresentationSubContext sub in HasSubContexts)
				{
					if (sub.StepId != host.StepId)
						arr.Add(sub.getJson(this, options));
				}
			}
			if (arr.Count > 0)
				obj["HasSubContexts"] = arr;
			if (mHasCoordinateOperation != null)
				obj["HasCoordinateOperation"] = mHasCoordinateOperation.getJson(this, options);

			JsonArray reps = new JsonArray();
			foreach (IfcRepresentation<IfcRepresentationItem> r in RepresentationsInContext)
			{
				if (r.mOfProductRepresentation.Count == 0 && r.mRepresentationMap == null)
					reps.Add(r.getJson(this, options));
			}
			if (reps.Count > 0)
				obj["RepresentationsInContext"] = reps;
		}
	}
	public partial class IfcGeometricRepresentationSubContext : IfcGeometricRepresentationContext
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["TargetScale"];
			if (node != null)
				TargetScale = node.GetValue<double>();
			node = obj["TargetView"];
			if (node != null)
				Enum.TryParse<IfcGeometricProjectionEnum>(node.GetValue<string>(), out mTargetView);
			node = obj["UserDefinedTargetView"];
			if (node != null)
				UserDefinedTargetView = node.GetValue<string>();
			JsonObject jobj = obj["ParentContext"] as JsonObject;
			if (jobj != null)
				ParentContext = mDatabase.ParseJsonObject<IfcGeometricRepresentationContext>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			if (options.Style == SetJsonOptions.JsonStyle.Repository && string.IsNullOrEmpty(mGlobalId))
			{
				setGlobalId(ParserIfc.EncodeGuid(Guid.NewGuid()));
				options.Encountered.Add(mGlobalId);
			}
			base.setJSON(obj, host, options);
			obj["ParentContext"] = ParentContext.getJson(this, options);
			if (!double.IsNaN(mTargetScale))
				obj["TargetScale"] = mTargetScale.ToString();
			obj["TargetView"] = TargetView.ToString();
			setAttribute(obj, "UserDefinedTargetView", UserDefinedTargetView);
		}
	}
	public partial class IfcGeometricSet : IfcGeometricRepresentationItem //SUPERTYPE OF(IfcGeometricCurveSet)
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mElements.AddRange(mDatabase.extractJsonArray<IfcGeometricSetSelect>(obj["Elements"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			foreach (IfcGeometricSetSelect gs in mElements)
				array.Add(gs.getJson(this, options));
			obj["Elements"] = array;
		}
	}
	public partial class IfcGradientCurve
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["BaseCurve"] = BaseCurve.getJson(this, options);
			if (EndPoint != null)
				obj["EndPoint"] = EndPoint.getJson(this, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["BaseCurve"] as JsonObject;
			if (jobj != null)
				BaseCurve = mDatabase.ParseJsonObject<IfcBoundedCurve>(jobj);
			jobj = obj["EndPoint"] as JsonObject;
			if (jobj != null)
				EndPoint = mDatabase.ParseJsonObject<IfcPlacement>(jobj);
		}
	}
	public partial class IfcGrid : IfcPositioningElement
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			UAxes.AddRange(mDatabase.extractJsonArray<IfcGridAxis>(obj["UAxes"] as JsonArray));
			VAxes.AddRange(mDatabase.extractJsonArray<IfcGridAxis>(obj["VAxes"] as JsonArray));
			WAxes.AddRange(mDatabase.extractJsonArray<IfcGridAxis>(obj["WAxes"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "UAxes", UAxes, this, options);
			createArray(obj, "VAxes", VAxes, this, options);
			createArray(obj, "WAxes", WAxes, this, options);
		}
	}
	public partial class IfcGridAxis : BaseClassIfc
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			AxisTag = extractString(obj["AxisTag"]);
			JsonObject jobj = obj["AxisCurve"] as JsonObject;
			if (jobj != null)
				AxisCurve = mDatabase.ParseJsonObject<IfcCurve>(jobj);
			var node = obj["SameSense"];
			if (node != null)
				SameSense = node.GetValue<bool>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "AxisTag", AxisTag);
			obj["AxisCurve"] = AxisCurve.getJson(this, options);
			obj["SameSense"] = SameSense;
		}
	}
	public partial class IfcGroup : IfcObject //SUPERTYPE OF (ONEOF (IfcAsset ,IfcCondition ,IfcInventory ,IfcStructuralLoadGroup ,IfcStructuralResultGroup ,IfcSystem ,IfcZone))
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			foreach (IfcRelAssignsToGroup rag in mDatabase.extractJsonArray<IfcRelAssignsToGroup>(obj["IsGroupedBy"] as JsonArray))
				rag.RelatingGroup = this;
			foreach (IfcRelReferencedInSpatialStructure rss in mDatabase.extractJsonArray<IfcRelReferencedInSpatialStructure>(obj["ReferencedInStructures"] as JsonArray))
				rss.RelatedElements.Add(this);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			foreach (IfcRelAssignsToGroup rag in mIsGroupedBy)
				array.Add(rag.getJson(this, options));
			obj["IsGroupedBy"] = array;
		}
	}
}
#endif
