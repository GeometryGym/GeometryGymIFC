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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

using Newtonsoft.Json.Linq;

namespace GeometryGym.Ifc
{
	[Serializable]
	public partial class IfcAlignment : IfcLinearPositioningElement //IFC4.1
	{
		internal IfcAlignmentTypeEnum mPredefinedType = IfcAlignmentTypeEnum.NOTDEFINED;// : OPTIONAL IfcAlignmentTypeEnum;

		public IfcAlignmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcAlignment() : base() { }
		public IfcAlignment(IfcSite host, IfcCurve axis) : base(host, axis) { }

		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mPredefinedType == IfcAlignmentTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAlignmentTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}

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
	[Serializable]
	public partial class IfcAlignment2DHorizontal : IfcGeometricRepresentationItem //IFC4.1
	{
		internal double mStartDistAlong = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal LIST<IfcAlignment2DHorizontalSegment> mSegments = new LIST<IfcAlignment2DHorizontalSegment>();// : LIST [1:?] OF IfcAlignment2DHorizontalSegment;
													   //INVERSE
													   //ToAlignmentCurve : SET[1:?] OF IfcAlignmentCurve FOR Horizontal;
		public double StartDistAlong { get { return mStartDistAlong; } set { mStartDistAlong = value; } }
		public LIST<IfcAlignment2DHorizontalSegment> Segments { get { return mSegments; } set { mSegments = value; } }

		internal IfcAlignment2DHorizontal() : base() { }
		internal IfcAlignment2DHorizontal(IEnumerable<IfcAlignment2DHorizontalSegment> segments) : base(segments.First().Database) { mSegments.AddRange(segments); }

		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mStartDistAlong) + ",(#" + string.Join(",#", mSegments.ConvertAll(x => x.Index)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mStartDistAlong = ParserSTEP.StripDouble(str, ref pos, len);
			mSegments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcAlignment2DHorizontalSegment));
		}

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
	[Serializable]
	public partial class IfcAlignment2DHorizontalSegment : IfcAlignment2DSegment //IFC4.1
	{
		internal IfcCurveSegment2D mCurveGeometry;// : IfcCurveSegment2D;
		public IfcCurveSegment2D CurveGeometry { get { return mCurveGeometry; } set { mCurveGeometry = value; } }

		internal IfcAlignment2DHorizontalSegment() : base() { }
		internal IfcAlignment2DHorizontalSegment(IfcCurveSegment2D seg) : base(seg.mDatabase) { CurveGeometry = seg; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",#" + mCurveGeometry.Index; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mCurveGeometry = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurveSegment2D;
		}

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
	[Serializable]
	public abstract partial class IfcAlignment2DSegment : BaseClassIfc //IFC4.1
	{
		private IfcLogicalEnum mTangentialContinuity = IfcLogicalEnum.UNKNOWN;// : OPTIONAL IfcBoolean;
		private string mStartTag = "$";// : OPTIONAL IfcLabel;
		private string mEndTag = "$";// : OPTIONAL IfcLabel;

		public bool TangentialContinuity { get { return mTangentialContinuity == IfcLogicalEnum.TRUE; } set { mTangentialContinuity = (value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); } }
		public string StartTag { get { return (mStartTag == "$" ? "" : ParserIfc.Decode(mStartTag)); } set { mStartTag = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string EndTag { get { return (mEndTag == "$" ? "" : ParserIfc.Decode(mEndTag)); } set { mEndTag = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcAlignment2DSegment() : base() { }
		protected IfcAlignment2DSegment(DatabaseIfc db) : base(db) { }

		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mTangentialContinuity == IfcLogicalEnum.UNKNOWN ? ",$," : (mTangentialContinuity == IfcLogicalEnum.TRUE ? ",.T.," : ",.F.,")) +
				(mStartTag == "$" ? "$," : "'" + mStartTag + "',") + (mEndTag == "$" ? "$" : "'" + mEndTag + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mTangentialContinuity = ParserIfc.StripLogical(str, ref pos, len);
			mStartTag = ParserSTEP.StripString(str, ref pos, len);
			mEndTag = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	[Serializable]
	public partial class IfcAlignment2DVerSegCircularArc : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		private bool mIsConvex;// : IfcBoolean;

		public double Radius { get { return mRadius; } set { mRadius = value; } }
		public bool IsConvex { get { return mIsConvex; } set { mIsConvex = value; } }

		internal IfcAlignment2DVerSegCircularArc() : base() { }
		internal IfcAlignment2DVerSegCircularArc(DatabaseIfc db, double startDist, double horizontalLength, double startHeight, double startGradient, double radius, bool isConvex)
			: base(db, startDist, horizontalLength, startHeight, startGradient)
		{
			mRadius = radius;
			mIsConvex = isConvex;
		}

		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mRadius) + "," + ParserSTEP.BoolToString(mIsConvex); }

		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mIsConvex = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	[Serializable]
	public partial class IfcAlignment2DVerSegLine : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		internal IfcAlignment2DVerSegLine() : base() { }
		internal IfcAlignment2DVerSegLine(DatabaseIfc db, double startDist, double horizontalLength, double startHeight, double startGradient)
			: base(db, startDist, horizontalLength, startHeight, startGradient) { }
	}
	[Serializable]
	public partial class IfcAlignment2DVerSegParabolicArc : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		private double mParabolaConstant;// : IfcPositiveLengthMeasure;
		private bool mIsConvex;// : IfcBoolean;

		public double ParabolaConstant { get { return mParabolaConstant; } set { mParabolaConstant = value; } }
		public bool IsConvex { get { return mIsConvex; } set { mIsConvex = value; } }

		internal IfcAlignment2DVerSegParabolicArc() : base() { }
		internal IfcAlignment2DVerSegParabolicArc(DatabaseIfc db, double startDist, double horizontalLength, double startHeight, double startGradient, double radius, bool isConvex)
			: base(db, startDist, horizontalLength, startHeight, startGradient)
		{
			mParabolaConstant = radius;
			mIsConvex = isConvex;
		}

		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mParabolaConstant) + "," + ParserSTEP.BoolToString(mIsConvex); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mParabolaConstant = ParserSTEP.StripDouble(str, ref pos, len);
			mIsConvex = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	[Serializable]
	public partial class IfcAlignment2DVertical : IfcGeometricRepresentationItem //IFC4.1
	{
		internal LIST<IfcAlignment2DVerticalSegment> mSegments = new LIST<IfcAlignment2DVerticalSegment>();// : LIST [1:?] OF IfcAlignment2DVerticalSegment;
		public LIST<IfcAlignment2DVerticalSegment> Segments { get { return mSegments; } set { mSegments = value; } }
		internal IfcAlignment2DVertical() : base() { }
		//internal IfcAlignment2DVertical(IfcAlignment2DVertical a) : base() { mSegments.Add(a); }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",(#" + string.Join(",#", mSegments.ConvertAll(x => x.Index)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mSegments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcAlignment2DVerticalSegment)); }
	}
	[Serializable]
	public abstract partial class IfcAlignment2DVerticalSegment : IfcAlignment2DSegment //IFC4.1
	{
		internal double mStartDistAlong;// : IfcLengthMeasure;
		internal double mHorizontalLength;// : IfcPositiveLengthMeasure;
		internal double mStartHeight;// : IfcLengthMeasure;
		internal double mStartGradient;// : IfcRatioMeasure; 

		public double StartDistAlong { get { return mStartDistAlong; } set { mStartDistAlong = value; } }
		public double HorizontalLength { get { return mHorizontalLength; } set { mHorizontalLength = value; } }
		public double StartHeight { get { return mStartHeight; } set { mStartHeight = value; } }
		public double StartGradient { get { return mStartGradient; } set { mStartGradient = value; } }

		protected IfcAlignment2DVerticalSegment() : base() { }
		protected IfcAlignment2DVerticalSegment(DatabaseIfc db, double startDist, double horizontalLength, double startHeight, double startGradient) : base(db)
		{
			mStartDistAlong = startDist;
			mHorizontalLength = horizontalLength;
			mStartHeight = startHeight;
			mStartGradient = startGradient;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mStartDistAlong) + "," + ParserSTEP.DoubleToString(mHorizontalLength) + "," + ParserSTEP.DoubleToString(mStartHeight) + "," + ParserSTEP.DoubleToString(mStartGradient); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mStartDistAlong = ParserSTEP.StripDouble(str, ref pos, len);
			mHorizontalLength = ParserSTEP.StripDouble(str, ref pos, len);
			mStartHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mStartGradient = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	[Serializable]
	public partial class IfcAlignmentCurve : IfcBoundedCurve //IFC4.1
	{
		internal IfcAlignment2DHorizontal mHorizontal = null;// : OPTIONAL IfcAlignment2DHorizontal;
		internal IfcAlignment2DVertical mVertical = null;// : OPTIONAL IfcAlignment2DVertical;
		internal string mTag = "$";// : OPTIONAL IfcLabel;

		public IfcAlignment2DHorizontal Horizontal { get { return mHorizontal; } set { mHorizontal = value; } }
		public IfcAlignment2DVertical Vertical { get { return mVertical; } set { mVertical = value; } }
		public string Tag { get { return (mTag == "$" ? "" : ParserIfc.Decode(mTag)); } set { mTag = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		internal IfcAlignmentCurve() : base() { }
		public IfcAlignmentCurve(DatabaseIfc db) : base(db) { }
		public IfcAlignmentCurve(IfcAlignment2DHorizontal horizontal) : base(horizontal.Database) { Horizontal = horizontal; }
		public IfcAlignmentCurve(IfcAlignment2DVertical vertical) : base(vertical.Database) { Vertical = vertical; }
		public IfcAlignmentCurve(IfcAlignment2DHorizontal horizontal, IfcAlignment2DVertical vertical) : this(horizontal) { Vertical = vertical; }

		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.ObjToLinkString(mHorizontal) + "," + ParserSTEP.ObjToLinkString(mVertical) + "," + (mTag == "$" ? "$" : "'" + mTag + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mHorizontal = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAlignment2DHorizontal;
			mVertical = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAlignment2DVertical;
			mTag = ParserSTEP.StripString(str, ref pos, len);
		}

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
			if(mHorizontal != null)
				obj["Horizontal"] = Horizontal.getJson(this, options);
			if(mVertical != null)
				obj["Vertical"] = Vertical.getJson(this, options);
			setAttribute(obj, "Tag", Tag);
		}
	}
	[Serializable]
	public partial class IfcCircularArcSegment2D : IfcCurveSegment2D  //IFC4.1
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		private bool mIsCCW;// : IfcBoolean;

		public double Radius { get { return mRadius; } set { mRadius = value; } }
		public bool IsCCW { get { return mIsCCW; } set { mIsCCW = value; } }

		internal IfcCircularArcSegment2D() : base() { }
		internal IfcCircularArcSegment2D(DatabaseIfc db, IfcCircularArcSegment2D s) : base(db, s) { mRadius = s.mRadius; mIsCCW = s.mIsCCW; }
		internal IfcCircularArcSegment2D(IfcCartesianPoint start, double startDirection, double length, double radius, bool isCCW)
			: base(start, startDirection, length)
		{
			mRadius = radius;
			mIsCCW = isCCW;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Radius = ParserSTEP.StripDouble(str, ref pos, len);
			mIsCCW = ParserSTEP.StripBool(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mRadius) + "," + ParserSTEP.BoolToString(mIsCCW); }

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
	[Serializable]
	public abstract partial class IfcCurveSegment2D : IfcBoundedCurve
	{
		private IfcCartesianPoint mStartPoint;// : IfcCartesianPoint;
		private double mStartDirection;// : IfcPlaneAngleMeasure;
		private double mSegmentLength;// : IfcPositiveLengthMeasure;

		public IfcCartesianPoint StartPoint { get { return mStartPoint; } set { mStartPoint = value; } }
		public double StartDirection { get { return mStartDirection; } set { mStartDirection = value; } }
		public double SegmentLength { get { return mSegmentLength; } set { mSegmentLength = value; } }

		protected IfcCurveSegment2D() : base() { }
		protected IfcCurveSegment2D(DatabaseIfc db, IfcCurveSegment2D p) : base(db, p) { StartPoint = db.Factory.Duplicate(p.StartPoint) as IfcCartesianPoint; mStartDirection = p.mStartDirection; mSegmentLength = p.mSegmentLength; }
		protected IfcCurveSegment2D(IfcCartesianPoint start, double startDirection, double length)
			: base(start.mDatabase)
		{
			mStartPoint = start;
			mStartDirection = startDirection;
			mSegmentLength = length;
		}

		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",#" + mStartPoint.mIndex + "," + ParserSTEP.DoubleToString(mStartDirection) + "," + ParserSTEP.DoubleToString(mSegmentLength); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mStartPoint = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianPoint;
			mStartDirection = ParserSTEP.StripDouble(str, ref pos, len);
			mSegmentLength = ParserSTEP.StripDouble(str, ref pos, len);
		}
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
	[Serializable]
	public abstract partial class IfcLinearPositioningElement : IfcPositioningElement //IFC4.1
	{
		private IfcCurve mAxis;// : IfcCurve;

		public IfcCurve Axis { get { return mAxis; } set { mAxis = value; } }

		protected IfcLinearPositioningElement() : base() { }
		protected IfcLinearPositioningElement(IfcSite host, IfcCurve axis) : base(host) { Axis = axis; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",#" + mAxis.Index; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxis = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
		}
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
	[Serializable]
	public partial class IfcLineSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		internal IfcLineSegment2D() : base() { }
		internal IfcLineSegment2D(DatabaseIfc db, IfcLineSegment2D s) : base(db, s) { }
		internal IfcLineSegment2D(IfcCartesianPoint start, double startDirection, double length)
			: base(start, startDirection, length) { }
	}
	[Serializable]
	public abstract partial class IfcPositioningElement : IfcProduct //IFC4.1
	{
		protected IfcPositioningElement() : base() { }
		protected IfcPositioningElement(IfcSite host) : base(host.Database) { host.AddElement(this); }
	}
	[Serializable]
	public partial class IfcTransitionCurveSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		private double mStartRadius = double.PositiveInfinity;// OPTIONAL IfcPositiveLengthMeasure;
		private double mEndRadius = double.PositiveInfinity;// OPTIONAL IfcPositiveLengthMeasure;
		private bool mIsStartRadiusCCW;// : IfcBoolean;
		private bool mIsEndRadiusCCW;// : IfcBoolean;
		private IfcTransitionCurveType mTransitionCurveType = IfcTransitionCurveType.BIQUADRATICPARABOLA;

		public double StartRadius { get { return mStartRadius; } set { mStartRadius = value; } }
		public double EndRadius { get { return mEndRadius; } set { mEndRadius = value; } }
		public bool IsStartRadiusCCW { get { return mIsStartRadiusCCW; } set { mIsStartRadiusCCW = value; } }
		public bool IsEndRadiusCCW { get { return mIsEndRadiusCCW; } set { mIsEndRadiusCCW = value; } }
		public IfcTransitionCurveType TransitionCurveType { get { return mTransitionCurveType; } set { mTransitionCurveType = value; } }

		internal IfcTransitionCurveSegment2D() : base() { }
		internal IfcTransitionCurveSegment2D(IfcCartesianPoint start, double startDirection, double length, double startRadius, double endRadius, bool isStartCCW, bool isEndCCW, IfcTransitionCurveType curveType)
			: base(start, startDirection, length)
		{
			mStartRadius = startRadius;
			mEndRadius = endRadius;
			mIsStartRadiusCCW = isStartCCW;
			mIsEndRadiusCCW = isEndCCW;
			mTransitionCurveType = curveType;
		}

		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mStartRadius) + "," + ParserSTEP.DoubleOptionalToString(mEndRadius) + "," +
				ParserSTEP.BoolToString(mIsStartRadiusCCW) + "," + ParserSTEP.BoolToString(mIsEndRadiusCCW) + ",." + mTransitionCurveType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mStartRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mEndRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mIsStartRadiusCCW = ParserSTEP.StripBool(str, ref pos, len);
			mIsEndRadiusCCW = ParserSTEP.StripBool(str, ref pos, len);
			Enum.TryParse<IfcTransitionCurveType>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mTransitionCurveType);
		}

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
