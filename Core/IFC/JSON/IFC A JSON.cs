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
	public partial class IfcAirTerminal
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcAirTerminalTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcAirTerminalTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcAirTerminalType
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcAirTerminalTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcAlignment
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcAlignmentTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcAlignmentTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcAlignment2DHorizontal
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["StartDistAlong"];
			if (node != null)
				StartDistAlong = node.GetValue<double>();
			Segments.AddRange(mDatabase.extractJsonArray<IfcAlignment2DHorizontalSegment>(obj["Segments"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if ((mDatabase != null && mStartDistAlong > mDatabase.Tolerance) || mStartDistAlong > 1e-5)
				obj["StartDistAlong"] = StartDistAlong;
			createArray(obj, "Segments", Segments, this, options);
		}
	}
	public partial class IfcAlignment2DHorizontalSegment 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["CurveGeometry"];
			if (node != null)
				CurveGeometry = mDatabase.ParseJsonObject<IfcCurveSegment2D>(node as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["CurveGeometry"] = CurveGeometry.getJson(this, options);
		}
	}
	public abstract partial class IfcAlignment2DSegment
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["TangentialContinuity"];
			if (node != null)
				TangentialContinuity = node.GetValue<bool>();
			node = obj["StartTag"];
			if (node != null)
				StartTag = node.GetValue<string>();
			node = obj["EndTag"];
			if (node != null)
				EndTag = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mTangentialContinuity != IfcLogicalEnum.UNKNOWN)
				obj["TangentialContinuity"] = TangentialContinuity;
			setAttribute(obj, "StartTag", StartTag);
			setAttribute(obj, "EndTag", EndTag);
		}
	}
	public partial class IfcAlignment2DVerSegCircularArc
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Radius"] = Radius;
			obj["IsConvex"] = IsConvex;
		}
	}
	public partial class IfcAlignment2DVerSegParabolicArc
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ParabolaConstant"] = ParabolaConstant;
			obj["IsConvex"] = IsConvex;
		}
	}
	public partial class IfcAlignment2DVertical
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "Segments", Segments, this, options);
		}
	}
	public abstract partial class IfcAlignment2DVerticalSegment
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["StartDistAlong"] = StartDistAlong;
			obj["HorizontalLength"] = HorizontalLength;
			obj["StartHeight"] = StartHeight;
			obj["StartGradient"] = StartGradient;
		}
	}
	public partial class IfcAlignmentCant
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["RailHeadDistance"];
			if (node != null)
				mRailHeadDistance = node.GetValue<double>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RailHeadDistance"] = mRailHeadDistance.ToString();
		}
	
	}
	public partial class IfcAlignmentCantSegment
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["StartDistAlong"];
			if (node != null)
				mStartDistAlong = node.GetValue<double>();
			node = obj["HorizontalLength"];
			if (node != null)
				mHorizontalLength = node.GetValue<double>();
			node = obj["StartCantLeft"];
			if (node != null)
				mStartCantLeft = node.GetValue<double>();
			node = obj["EndCantLeft"];
			if (node != null)
				mEndCantLeft = node.GetValue<double>();
			node = obj["StartCantRight"];
			if (node != null)
				mStartCantRight = node.GetValue<double>();
			node = obj["EndCantRight"];
			if (node != null)
				mEndCantRight = node.GetValue<double>();
			node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcAlignmentCantSegmentTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["StartDistAlong"] = mStartDistAlong.ToString();
			obj["HorizontalLength"] = mHorizontalLength.ToString();
			obj["StartCantLeft"] = mStartCantLeft.ToString();
			if (!double.IsNaN(mEndCantLeft))
				obj["EndCantLeft"] = mEndCantLeft.ToString();
			obj["StartCantRight"] = mStartCantRight.ToString();
			if (!double.IsNaN(mEndCantRight))
				obj["EndCantRight"] = mEndCantRight.ToString();
			obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcAlignmentCurve
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Horizontal = mDatabase.ParseJsonObject<IfcAlignment2DHorizontal>(obj["Horizontal"] as JsonObject);
			Vertical = mDatabase.ParseJsonObject<IfcAlignment2DVertical>(obj["Vertical"] as JsonObject);
			Tag = extractString(obj, "Tag");
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mHorizontal != null)
				obj["Horizontal"] = Horizontal.getJson(this, options);
			if (mVertical != null)
				obj["Vertical"] = Vertical.getJson(this, options);
			setAttribute(obj, "Tag", Tag);
		}
	}
	public partial class IfcAlignmentHorizontal
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mStartDistAlong))
				obj["StartDistAlong"] = mStartDistAlong.ToString();
			createArray(obj, "Segments", HorizontalSegments, this, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["StartDistAlong"];
			if (node != null)
				mStartDistAlong = node.GetValue<double>();
			mHorizontalSegments.AddRange(mDatabase.extractJsonArray<IfcAlignmentHorizontalSegment>(obj["Segments"] as JsonArray));
		}
	}
	public partial class IfcAlignmentHorizontalSegment
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["StartPoint"] = StartPoint.getJson(this, options);
			obj["StartDirection"] = StartDirection;
			obj["StartRadiusOfCurvature"] = mStartRadiusOfCurvature.ToString();
			obj["EndRadiusOfCurvature"] = mEndRadiusOfCurvature.ToString();
			obj["SegmentLength"] = mSegmentLength.ToString();
			if (!double.IsNaN(mGravityCenterLineHeight))
				obj["GravityCenterLineHeight"] = mGravityCenterLineHeight.ToString();
			obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			StartPoint = mDatabase.ParseJsonObject<IfcCartesianPoint>(obj["StartPoint"] as JsonObject);
			var node = obj["StartDirection"];
			if (node != null)
			StartDirection = node.GetValue<double>();
			node = obj["StartRadiusOfCurvature"];
			if (node != null)
				StartRadiusOfCurvature = node.GetValue<double>();
			node = obj["EndRadiusOfCurvature"];
			if (node != null)
				EndRadiusOfCurvature = node.GetValue<double>();
			node = obj["SegmentLength"];
			if (node != null)
				mSegmentLength = node.GetValue<double>();
			node = obj["GravityCenterLineHeight"];
			if (node != null)
				mGravityCenterLineHeight = node.GetValue<double>();
			node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcAlignmentHorizontalSegmentTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcAlignmentParameterSegment
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			StartTag = extractString(obj, "StartTag");
			EndTag = extractString(obj, "EndTag");
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!string.IsNullOrEmpty(StartTag))
				obj["StartTag"] = StartTag;
			if (!string.IsNullOrEmpty(EndTag))
				obj["EndTag"] = EndTag;
		}
	}
	public partial class IfcAlignmentSegment
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["GeometricParameters"] = DesignParameters.getJson(this, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["GeometricParameters"] as JsonObject;
			if (jobj != null)
				DesignParameters = mDatabase.ParseJsonObject<IfcAlignmentParameterSegment>(jobj);
		}
	}
	public partial class IfcAlignmentVertical
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mDatabase.Release == ReleaseVersion.IFC4X3_RC2)
				createArray(obj, "Segments", VerticalSegments, this, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mVerticalSegments.AddRange(mDatabase.extractJsonArray<IfcAlignmentVerticalSegment>(obj["Segments"] as JsonArray));
		}
	}
	public partial class IfcAlignmentVerticalSegment
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["StartDistAlong"] = mStartDistAlong.ToString();
			obj["HorizontalLength"] = mHorizontalLength.ToString();
			obj["StartHeight"] = mStartHeight.ToString();
			obj["StartGradient"] = mStartGradient.ToString();
			obj["EndGradient"] = mStartGradient.ToString();
			if(!double.IsNaN(mRadiusOfCurvature))
				obj["RadiusOfCurvature"] = mRadiusOfCurvature.ToString();
			obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["StartDistAlong"];
			if (node != null)
				mStartDistAlong = node.GetValue<double>();
			node = obj["HorizontalLength"];
			if (node != null)
				mHorizontalLength = node.GetValue<double>();
			node = obj["StartHeight"];
			if (node != null)
				mStartHeight = node.GetValue<double>();
			node = obj["StartGradient"];
			if (node != null)
				mStartGradient = node.GetValue<double>();
			node = obj["EndGradient"];
			if (node != null)
				mEndGradient = node.GetValue<double>();
			node = obj["RadiusOfCurvature"];
			if (node != null)
				mRadiusOfCurvature = node.GetValue<double>();
			node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcAlignmentVerticalSegmentTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcAnnotation
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcAnnotationTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcAnnotationTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcAnnotationFillArea
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			OuterBoundary = mDatabase.ParseJsonObject<IfcCurve>(obj["OuterBoundary"] as JsonObject);
			InnerBoundaries.AddRange(mDatabase.extractJsonArray<IfcCurve>(obj["InnerBoundaries"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["OuterBoundary"] = OuterBoundary.getJson(this, options);
			createArray(obj, "InnerBoundaries", InnerBoundaries, this, options);
		}
	}
	public partial class IfcApplication
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			ApplicationDeveloper = mDatabase.ParseJsonObject<IfcOrganization>(obj["ApplicationDeveloper"] as JsonObject);
			Version = extractString(obj, "Version");
			ApplicationFullName = extractString(obj, "ApplicationFullName");
			ApplicationIdentifier = extractString(obj, "ApplicationIdentifier");
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			
			obj["ApplicationDeveloper"] = ApplicationDeveloper.getJson(this, options);
			obj["Version"] = Version;
			obj["ApplicationFullName"] = ApplicationFullName;
			obj["ApplicationIdentifier"] = ApplicationIdentifier;
		}
	}
	public partial class IfcAppliedValue
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);

			Name = extractString(obj, "Name");
			Description = extractString(obj, "Description");
			AppliedValue = mDatabase.ParseJsonObject<IfcAppliedValueSelect>(obj["AppliedValue"] as JsonObject);
			UnitBasis = mDatabase.ParseJsonObject<IfcMeasureWithUnit>(obj["UnitBasis"] as JsonObject);

			Components.AddRange(mDatabase.extractJsonArray<IfcAppliedValue>(obj["Components"] as JsonArray));


			List <IfcExternalReferenceRelationship> ers = mDatabase.extractJsonArray<IfcExternalReferenceRelationship>(obj["HasExternalReference"] as JsonArray);
			foreach (var r in ers)
				r.RelatedResourceObjects.Add(this);
			List<IfcResourceConstraintRelationship> crs = mDatabase.extractJsonArray<IfcResourceConstraintRelationship>(obj["HasConstraintRelationships"] as JsonArray);
			foreach (var r in crs)
				r.RelatedResourceObjects.Add(this);
			//todo
			Category = extractString(obj, "Category");
			Condition = extractString(obj, "Condition");
			var node = obj["ArithmeticOperator"];
			if (node != null)
				Enum.TryParse<IfcArithmeticOperatorEnum>(node.GetValue<string>(), out mArithmeticOperator);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			if (mAppliedValue != null)
			{
				IfcValue value = mAppliedValue as IfcValue;
				if(value != null)
					obj["AppliedValue"] = DatabaseIfc.extract(value);
				else
					obj["AppliedValue"] = mAppliedValue.getJson(this, options);
			}
			if (mUnitBasis != null)
				obj["UnitBasis"] = UnitBasis.getJson(this, options);
			//todo
			setAttribute(obj, "Category", Category);
			setAttribute(obj, "Condition", Condition);
			if (mArithmeticOperator != IfcArithmeticOperatorEnum.NONE)
				obj["ArithmeticOperator"] = ArithmeticOperator.ToString();
			createArray(obj, "Components", Components, this, options);
			createArray(obj, "HasExternalReference", HasExternalReference, this, options);
			var constraintRelationships = HasConstraintRelationships.Where(x => x.StepId != host.StepId);
			if (constraintRelationships.Count() > 0)
				createArray(obj, "HasConstraintRelationships", constraintRelationships, this, options);
		}
	}
	public partial class IfcArbitraryClosedProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			OuterCurve = mDatabase.ParseJsonObject<IfcBoundedCurve>(obj["OuterCurve"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["OuterCurve"] = OuterCurve.getJson(this, options);
		}
	}
	public partial class IfcAsymmetricIShapeProfileDef 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["BottomFlangeWidth"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mBottomFlangeWidth);
			node = obj["OverallDepth"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mOverallDepth);
			node = obj["WebThickness"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mWebThickness);
			node = obj["BottomFlangeThickness"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mBottomFlangeThickness);
			node = obj["BottomFlangeFilletRadius"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mBottomFlangeFilletRadius);
			node = obj["TopFlangeWidth"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mTopFlangeWidth);
			node = obj["TopFlangeThickness"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mTopFlangeThickness);
			node = obj["TopFlangeFilletRadius"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mTopFlangeFilletRadius);
			node = obj["BottomFlangeEdgeRadius"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mBottomFlangeEdgeRadius);
			node = obj["BottomFlangeSlope"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mBottomFlangeSlope);
			node = obj["TopFlangeEdgeRadius"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mTopFlangeEdgeRadius);
			node = obj["TopFlangeSlope"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mTopFlangeSlope);
			node = obj["CentreOfGravityInY"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mCentreOfGravityInY);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["BottomFlangeWidth"] = formatLength(mBottomFlangeWidth);
			obj["OverallDepth"] = formatLength(mOverallDepth);
			obj["WebThickness"] = formatLength(mWebThickness);
			obj["BottomFlangeThickness"] = formatLength(mBottomFlangeThickness);
			if (!double.IsNaN(mBottomFlangeFilletRadius) && mBottomFlangeFilletRadius > 0)
				obj["BottomFlangeFilletRadius"] = formatLength(mBottomFlangeFilletRadius);
			obj["TopFlangeWidth"] = formatLength(mTopFlangeWidth);
			if (!double.IsNaN(mTopFlangeThickness) && mTopFlangeThickness > 0)
				obj["TopFlangeThickness"] = formatLength(mTopFlangeThickness);
			if (!double.IsNaN(mTopFlangeFilletRadius) && mTopFlangeFilletRadius > 0)
				obj["TopFlangeFilletRadius"] = formatLength(mTopFlangeFilletRadius);
			if (!double.IsNaN(mBottomFlangeEdgeRadius) && mBottomFlangeEdgeRadius > 0)
				obj["BottomFlangeEdgeRadius"] = formatLength(mBottomFlangeEdgeRadius);
			if (!double.IsNaN(mBottomFlangeSlope) && mBottomFlangeSlope > 0)
				obj["BottomFlangeSlope"] = mBottomFlangeSlope;
			if (!double.IsNaN(mTopFlangeEdgeRadius) && mTopFlangeEdgeRadius > 0)
				obj["TopFlangeEdgeRadius"] = formatLength(mTopFlangeEdgeRadius);
			if (!double.IsNaN(mTopFlangeSlope) && mTopFlangeSlope > 0)
				obj["TopFlangeSlope"] = mTopFlangeSlope;
			if (mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4)
			{
				if (!double.IsNaN(mCentreOfGravityInY) && mCentreOfGravityInY > 0)
					obj["CentreOfGravityInY"] = formatLength(mCentreOfGravityInY);
			}
		}
	}
	public partial class IfcAxis1Placement
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Axis = mDatabase.ParseJsonObject<IfcDirection>(obj["Axis"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			IfcDirection axis = Axis;
			if (axis != null)
				obj["Axis"] = axis.getJson(this, options);
		}
	}
	public partial class IfcAxis2Placement2D
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			RefDirection = mDatabase.ParseJsonObject<IfcDirection>(obj["RefDirection"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			IfcDirection refDirection = RefDirection;
			if (refDirection != null)
				obj["RefDirection"] = refDirection.getJson(this, options);
		}
	}
	public partial class IfcAxis2Placement3D
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Axis = mDatabase.ParseJsonObject<IfcDirection>(obj["Axis"] as JsonObject);
			RefDirection = mDatabase.ParseJsonObject<IfcDirection>(obj["RefDirection"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			IfcDirection axis = Axis, refDirection = RefDirection;

			if (axis == null)
			{
				if(refDirection != null)
				{
					obj["Axis"] = mDatabase.Factory.ZAxis.getJson(this, options);
					obj["RefDirection"] = refDirection.getJson(this, options);
				}
			}
			else if (refDirection == null)
			{
				obj["Axis"] = axis.getJson(this, options);
				obj["RefDirection"] = mDatabase.Factory.XAxis.getJson(this, options);
			}
			else if(!axis.isZAxis || !refDirection.isXAxis)
			{
				obj["Axis"] = axis.getJson(this, options);
				obj["RefDirection"] = refDirection.getJson(this, options);
			}
		}
	}
	public partial class IfcAxis2PlacementLinear
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (Axis != null)
				obj["Axis"] = Axis.getJson(this, options);
			if (RefDirection != null)
				obj["RefDirection"] = RefDirection.getJson(this, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Axis = mDatabase.ParseJsonObject<IfcDirection>(obj["Axis"] as JsonObject);
			RefDirection = mDatabase.ParseJsonObject<IfcDirection>(obj["RefDirection"] as JsonObject);
		}
	}
}
#endif
