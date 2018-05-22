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
using System.Drawing;
using GeometryGym.STEP;

using Newtonsoft.Json.Linq;

namespace GeometryGym.Ifc
{
	public partial class IfcAlignment : IfcLinearPositioningElement //IFC4.1
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcAlignmentTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcAlignmentTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcAlignment2DHorizontal : IfcGeometricRepresentationItem //IFC4.1
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("StartDistAlong", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				StartDistAlong = token.Value<double>();
			Segments.AddRange(mDatabase.extractJArray<IfcAlignment2DHorizontalSegment>(obj.GetValue("Segments", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if ((mDatabase != null && mStartDistAlong > mDatabase.Tolerance) || mStartDistAlong > 1e-5)
				obj["StartDistAlong"] = StartDistAlong;
			obj["Segments"] = new JArray(Segments.ConvertAll(x => x.getJson(this, options)));
		}
	}
	public partial class IfcAlignment2DHorizontalSegment : IfcAlignment2DSegment //IFC4.1
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("CurveGeometry", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				CurveGeometry = mDatabase.parseJObject<IfcCurveSegment2D>(token as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["CurveGeometry"] = CurveGeometry.getJson(this, options);
		}
	}
	public abstract partial class IfcAlignment2DSegment : BaseClassIfc //IFC4.1
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("TangentialContinuity", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				TangentialContinuity = token.Value<bool>();
			token = obj.GetValue("StartTag", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				StartTag = token.Value<string>();
			token = obj.GetValue("EndTag", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				EndTag = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mTangentialContinuity != IfcLogicalEnum.UNKNOWN)
				obj["TangentialContinuity"] = TangentialContinuity;
			setAttribute(obj, "StartTag", StartTag);
			setAttribute(obj, "EndTag", EndTag);
		}
	}
	public partial class IfcAlignment2DVerSegCircularArc : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Radius"] = Radius;
			obj["IsConvex"] = IsConvex;
		}
	}
	public partial class IfcAlignment2DVerSegParabolicArc : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ParabolaConstant"] = ParabolaConstant;
			obj["IsConvex"] = IsConvex;
		}
	}
	public partial class IfcAlignment2DVertical : IfcGeometricRepresentationItem //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JArray array = new JArray();
			foreach (IfcAlignment2DVerticalSegment seg in Segments)
				array.Add(seg.getJson(this, options));
			obj["Segments"] = array;
		}
	}
	public abstract partial class IfcAlignment2DVerticalSegment : IfcAlignment2DSegment //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["StartDistAlong"] = StartDistAlong;
			obj["HorizontalLength"] = HorizontalLength;
			obj["StartHeight"] = StartHeight;
			obj["StartGradient"] = StartGradient;
		}
	}

	public partial class IfcAlignmentCurve : IfcBoundedCurve //IFC4.1
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Horizontal", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Horizontal = mDatabase.parseJObject<IfcAlignment2DHorizontal>(token as JObject);
			token = obj.GetValue("Vertical", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Vertical = mDatabase.parseJObject<IfcAlignment2DVertical>(token as JObject);
			token = obj.GetValue("Tag", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Tag = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mHorizontal != null)
				obj["Horizontal"] = Horizontal.getJson(this, options);
			if (mVertical != null)
				obj["Vertical"] = Vertical.getJson(this, options);
			setAttribute(obj, "Tag", Tag);
		}
	}
	public partial class IfcCircularArcSegment2D : IfcCurveSegment2D  //IFC4.1
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Radius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Radius = token.Value<double>();
			token = obj.GetValue("IsCCW", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				IsCCW = token.Value<bool>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Radius"] = Radius;
			obj["IsCCW"] = IsCCW;
		}
	}
	public abstract partial class IfcCurveSegment2D : IfcBoundedCurve
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("StartPoint", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				StartPoint = mDatabase.parseJObject<IfcCartesianPoint>(token as JObject);
			token = obj.GetValue("StartDirection", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				StartDirection = token.Value<double>();
			token = obj.GetValue("SegmentLength", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				SegmentLength = token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["StartPoint"] = StartPoint.getJson(this, options);
			obj["StartDirection"] = StartDirection;
			obj["SegmentLength"] = SegmentLength;
		}
	}
	public abstract partial class IfcLinearPositioningElement : IfcPositioningElement //IFC4.1
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Axis", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Axis = mDatabase.parseJObject<IfcCurve>(token as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Axis"] = Axis.getJson(this, options);
		}
	}

	public partial class IfcTransitionCurveSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("StartRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				StartRadius = token.Value<double>();
			token = obj.GetValue("EndRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				EndRadius = token.Value<double>();
			token = obj.GetValue("IsStartRadiusCCW", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				IsStartRadiusCCW = token.Value<bool>();
			token = obj.GetValue("IsEndRadiusCCW", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				IsEndRadiusCCW = token.Value<bool>();
			token = obj.GetValue("TransitionCurveType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcTransitionCurveType>(token.Value<string>(), true, out mTransitionCurveType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["StartRadius"] = StartRadius;
			obj["EndRadius"] = EndRadius;
			obj["IsStartRadiusCCW"] = IsStartRadiusCCW;
			obj["IsEndRadiusCCW"] = IsEndRadiusCCW;
			obj["TransitionCurveType"] = mTransitionCurveType.ToString();
		}
	}
}
