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
	public partial class IfcAlignment : IfcPositioningElement //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, List<BaseClassIfc> prime)
		{
			base.setJSON(obj, host, prime);
			if (mPredefinedType != IfcAlignmentTypeEnum.NOTDEFINED)
				mJsonObject["PredefinedType"] = mPredefinedType.ToString();
			IfcAlignment2DHorizontal horizontal = Horizontal;
			if (horizontal != null)
				mJsonObject["Horizontal"] = horizontal.getJson(this, prime);
			IfcAlignment2DVertical vertical = Vertical;
			if (vertical != null)
				mJsonObject["Vertical"] = vertical.getJson(this, prime);
			string str = LinearRefMethod;
			if (!string.IsNullOrEmpty(str))
				mJsonObject["LinearRefMethod"] = str;
		}
	}
	public partial class IfcAlignment2DHorizontal : BaseClassIfc //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, List<BaseClassIfc> prime)
		{
			base.setJSON(obj, host, prime);
			if(!double.IsNaN(mStartDistAlong))
				mJsonObject["StartDistAlong"] = mStartDistAlong;
			JArray array = new JArray();
			foreach (IfcAlignment2DHorizontalSegment seg in Segments)
				array.Add(seg.getJson(this,prime));
			mJsonObject["Segments"] = array;
		}
	}
	public partial class IfcAlignment2DHorizontalSegment : IfcAlignment2DSegment //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, List<BaseClassIfc> prime)
		{
			base.setJSON(obj, host, prime);
			mJsonObject["CurveGeometry"] = mDatabase[mCurveGeometry].getJson(this, prime);
		}
	}
	public abstract partial class IfcAlignment2DSegment : BaseClassIfc //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, List<BaseClassIfc> prime)
		{
			base.setJSON(obj, host, prime);
			if (mTangentialContinuity != IfcLogicalEnum.UNKNOWN)
				mJsonObject["TangentialContinuity"] = TangentialContinuity;
			string str = StartTag;
			if (!string.IsNullOrEmpty(str))
				mJsonObject["StartTag"] = str;
			str = EndTag;
			if (!string.IsNullOrEmpty(str))
				mJsonObject["EndTag"] = str;
		}
	}
	public partial class IfcAlignment2DVerSegCircularArc : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, List<BaseClassIfc> prime)
		{
			base.setJSON(obj, host, prime);
			mJsonObject["Radius"] = Radius;
			mJsonObject["IsConvex"] = IsConvex;
		}
	}
	public partial class IfcAlignment2DVerSegParabolicArc : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, List<BaseClassIfc> prime)
		{
			base.setJSON(obj, host, prime);
			mJsonObject["ParabolaConstant"] = ParabolaConstant;
			mJsonObject["IsConvex"] = IsConvex;
		}
	}
	public partial class IfcAlignment2DVertical : BaseClassIfc //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, List<BaseClassIfc> prime)
		{
			base.setJSON(obj, host, prime);
			JArray array = new JArray();
			foreach (IfcAlignment2DVerticalSegment seg in Segments)
				array.Add(seg.getJson(this, prime));
			mJsonObject["Segments"] = array;
		}
	}
	public abstract partial class IfcAlignment2DVerticalSegment : IfcAlignment2DSegment //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, List<BaseClassIfc> prime)
		{
			base.setJSON(obj, host, prime);
			mJsonObject["StartDistAlong"] = StartDistAlong;
			mJsonObject["HorizontalLength"] = HorizontalLength;
			mJsonObject["StartHeight"] = StartHeight;
			mJsonObject["StartGradient"] = StartGradient;
		}
	}

	public partial class IfcCircularArcSegment2D : IfcCurveSegment2D  //IFC4.1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, List<BaseClassIfc> prime)
		{
			base.setJSON(obj, host, prime);
			mJsonObject["Radius"] = Radius;
			mJsonObject["IsCCW"] = IsCCW;
		}
	}
	public partial class IfcClothoidalArcSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, List<BaseClassIfc> prime)
		{
			base.setJSON(obj, host, prime);
			mJsonObject["StartRadius"] = StartRadius;
			mJsonObject["IsCCW"] = IsCCW;
			mJsonObject["IsEntry"] = IsEntry;
			mJsonObject["ClothoidConstant"] = ClothoidConstant;
		}
	}
	public abstract partial class IfcCurveSegment2D : IfcBoundedCurve
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, List<BaseClassIfc> prime)
		{
			base.setJSON(obj, host, prime);
			mJsonObject["StartPoint"] = mDatabase[mStartPoint].getJson(this, prime);
			mJsonObject["StartDirection"] = mStartDirection;
			mJsonObject["SegmentLength"] = mSegmentLength;
		}
	}
	
}
