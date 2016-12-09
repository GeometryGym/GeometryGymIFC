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
using GeometryGym.STEP;


namespace GeometryGym.Ifc
{
	public partial class IfcAlignment : IfcPositioningElement //IFC4.1
	{
		internal IfcAlignmentTypeEnum mPredefinedType;// : OPTIONAL IfcAlignmentTypeEnum;
		internal int mHorizontal = 0;// : OPTIONAL IfcAlignment2DHorizontal;
		internal int mVertical = 0;// : OPTIONAL IfcAlignment2DVertical;
		internal string mLinearRefMethod = "$";// : OPTIONAL IfcLabel;

		public IfcAlignment2DHorizontal Horizontal { get { return mDatabase[mHorizontal] as IfcAlignment2DHorizontal; } }
		public IfcAlignment2DVertical Vertical { get { return mDatabase[mVertical] as IfcAlignment2DVertical; } }
		public IfcAlignmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public string LinearRefMethod { get { return (mLinearRefMethod == "$" ? "" : ParserIfc.Decode(mLinearRefMethod)); } set { mLinearRefMethod = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		internal IfcAlignment() : base() { }
		internal IfcAlignment(DatabaseIfc db, IfcAlignment a) : base(db, a) { mPredefinedType = a.mPredefinedType; mLinearRefMethod = a.mLinearRefMethod; }
		internal static void parseFields(IfcAlignment a, List<string> arrFields, ref int ipos) 
		{ 
			IfcPositioningElement.parseFields(a, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if(str != "$")
				a.mPredefinedType = (IfcAlignmentTypeEnum)Enum.Parse(typeof(IfcAlignmentTypeEnum), str.Replace(".", ""));
			a.mHorizontal = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mVertical = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mLinearRefMethod = arrFields[ipos++].Replace("'", "");
		}
		internal static IfcAlignment Parse(string strDef) { IfcAlignment a = new IfcAlignment(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildStringSTEP() 
		{ 
			return base.BuildStringSTEP() + (mPredefinedType== IfcAlignmentTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") + 
				ParserSTEP.LinkToString(mHorizontal) + "," + ParserSTEP.LinkToString(mVertical) + "," +	( mLinearRefMethod == "$" ? "$" : "'" + mLinearRefMethod + "'");
		}
	}
	public partial class IfcAlignment2DHorizontal : BaseClassIfc //IFC4.1
	{
		internal double mStartDistAlong = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal List<int> mSegments = new List<int>();// : LIST [1:?] OF IfcAlignment2DHorizontalSegment;

		public double StartDistAlong { get { return mStartDistAlong; } set { mStartDistAlong = value; } }
		public List<IfcAlignment2DHorizontalSegment> Segments { get { return mSegments.ConvertAll(x => mDatabase[x] as IfcAlignment2DHorizontalSegment); } set { mSegments = value.ConvertAll(x => x.mIndex); } }

		internal IfcAlignment2DHorizontal() : base() { }
		internal IfcAlignment2DHorizontal(IfcAlignment2DHorizontal a) : base() { mStartDistAlong = a.mStartDistAlong; mSegments = new List<int>(a.mSegments.ToArray()); }
		internal static void parseFields(IfcAlignment2DHorizontal a, List<string> arrFields, ref int ipos)
		{
			a.mStartDistAlong = ParserSTEP.ParseDouble(arrFields[ipos++]);
			a.mSegments = ParserSTEP.SplitListLinks(arrFields[ipos++]); 
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mStartDistAlong) + "," + ParserSTEP.LinkToString(mSegments[0]);
			for (int icounter = 1; icounter < mSegments.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSegments[icounter]);
			return str + ")";
		}
		internal static IfcAlignment2DHorizontal Parse(string strDef) { IfcAlignment2DHorizontal a = new IfcAlignment2DHorizontal(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
	}
	public partial class IfcAlignment2DHorizontalSegment : IfcAlignment2DSegment //IFC4.1
	{
		internal int mCurveGeometry;// : IfcCurveSegment2D;
		public IfcCurveSegment2D CurveGeometry { get { return mDatabase[mCurveGeometry] as IfcCurveSegment2D; } }

		internal IfcAlignment2DHorizontalSegment() : base() { }
		internal IfcAlignment2DHorizontalSegment(IfcAlignment2DHorizontalSegment p) : base(p) { mCurveGeometry = p.mCurveGeometry; }
		internal IfcAlignment2DHorizontalSegment(bool tangential, string startTag, string endTag, IfcCurveSegment2D seg) : base(seg.mDatabase, tangential, startTag, endTag) { mCurveGeometry = seg.mIndex; }	
		internal static void parseFields(IfcAlignment2DHorizontalSegment s, List<string> arrFields, ref int ipos)
		{
			IfcAlignment2DSegment.parseFields(s, arrFields, ref ipos);
			s.mCurveGeometry = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		internal static IfcAlignment2DHorizontalSegment Parse(string strDef) { IfcAlignment2DHorizontalSegment a = new IfcAlignment2DHorizontalSegment(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mCurveGeometry); }
	}
	public abstract partial class IfcAlignment2DSegment : BaseClassIfc //IFC4.1
	{
		private IfcLogicalEnum mTangentialContinuity = IfcLogicalEnum.UNKNOWN;// : OPTIONAL IfcBoolean;
		private string mStartTag = "$";// : OPTIONAL IfcLabel;
		private string mEndTag = "$";// : OPTIONAL IfcLabel;
	
		public bool TangentialContinuity { get { return mTangentialContinuity== IfcLogicalEnum.TRUE; } set { mTangentialContinuity = (value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); } } 	
		public string StartTag { get { return (mStartTag == "$" ? "" : ParserIfc.Decode(mStartTag)); } set { mStartTag = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string EndTag { get { return (mEndTag == "$" ? "" : ParserIfc.Decode(mEndTag)); } set { mEndTag = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcAlignment2DSegment() : base() { }
		protected IfcAlignment2DSegment(IfcAlignment2DSegment s) : base() { mTangentialContinuity = s.mTangentialContinuity; mStartTag = s.mStartTag; mEndTag = s.mEndTag; }
		protected IfcAlignment2DSegment(DatabaseIfc m, bool tangential, string startTag, string endTag) : base(m) { mTangentialContinuity = (tangential ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); StartTag = startTag; EndTag = endTag; }	
		protected static void parseFields(IfcAlignment2DSegment a, List<string> arrFields, ref int ipos) 
		{
			a.mTangentialContinuity = ParserIfc.ParseIFCLogical(arrFields[ipos++]); 
			a.mStartTag = arrFields[ipos++].Replace("'", "");
			a.mEndTag = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mTangentialContinuity == IfcLogicalEnum.UNKNOWN ? ",$," : (mTangentialContinuity == IfcLogicalEnum.TRUE ? ",.T.," : ",.F.")) +
				(mStartTag == "$" ? "$," : "'" + mStartTag + "',") + (mEndTag == "$" ? "$" : "'" + mEndTag + "'");
		}
	}
	public partial class IfcAlignment2DVerSegCircularArc : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		private bool mIsConvex;// : IfcBoolean;

		public double Radius { get { return mRadius; } set { mRadius = value; } }
		public bool IsConvex { get { return mIsConvex; } set { mIsConvex = value; } }

		internal IfcAlignment2DVerSegCircularArc() : base() { }
		internal IfcAlignment2DVerSegCircularArc(IfcAlignment2DVerSegCircularArc p) : base(p) { mRadius = p.mRadius; mIsConvex = p.mIsConvex; }
		internal IfcAlignment2DVerSegCircularArc(DatabaseIfc m, bool tangential, string startTag, string endTag, double startDist, double horizontalLength, double startHeight, double startGradient, double radius, bool isConvex)
			: base(m, tangential, startTag, endTag, startDist, horizontalLength, startHeight, startGradient)
		{
			mRadius = radius;
			mIsConvex = isConvex;
		}
		internal static void parseFields(IfcAlignment2DVerSegCircularArc c, List<string> arrFields, ref int ipos)
		{
			IfcAlignment2DVerticalSegment.parseFields(c, arrFields, ref ipos);
			c.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mIsConvex = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		internal static IfcAlignment2DVerSegCircularArc Parse(string strDef) { IfcAlignment2DVerSegCircularArc c = new IfcAlignment2DVerSegCircularArc(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mRadius) + "," + ParserSTEP.BoolToString(mIsConvex); }

	}
	public partial class IfcAlignment2DVerSegLine : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		internal IfcAlignment2DVerSegLine() : base() { }
		internal IfcAlignment2DVerSegLine(IfcAlignment2DVerSegLine s) : base(s) { }
		internal IfcAlignment2DVerSegLine(DatabaseIfc m, bool tangential, string startTag, string endTag, double startDist, double horizontalLength, double startHeight, double startGradient)
			: base(m, tangential, startTag, endTag, startDist, horizontalLength, startHeight, startGradient) { }
		internal static void parseFields(IfcAlignment2DVerSegLine c, List<string> arrFields, ref int ipos) { IfcAlignment2DVerticalSegment.parseFields(c, arrFields, ref ipos); }
		internal static IfcAlignment2DVerSegLine Parse(string strDef) { IfcAlignment2DVerSegLine c = new IfcAlignment2DVerSegLine(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
	}
	public partial class IfcAlignment2DVerSegParabolicArc : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		private double mParabolaConstant;// : IfcPositiveLengthMeasure;
		private bool mIsConvex;// : IfcBoolean;

		public double ParabolaConstant { get { return mParabolaConstant; } set { mParabolaConstant = value; } }
		public bool IsConvex { get { return mIsConvex; } set { mIsConvex = value; } }

		internal IfcAlignment2DVerSegParabolicArc() : base() { }
		internal IfcAlignment2DVerSegParabolicArc(IfcAlignment2DVerSegParabolicArc p) : base(p) { mParabolaConstant = p.mParabolaConstant; mIsConvex = p.mIsConvex; }
		internal IfcAlignment2DVerSegParabolicArc(DatabaseIfc m, bool tangential, string startTag, string endTag, double startDist, double horizontalLength, double startHeight, double startGradient, double radius, bool isConvex)
			: base(m, tangential, startTag, endTag, startDist, horizontalLength, startHeight, startGradient)
		{
			mParabolaConstant = radius;
			mIsConvex = isConvex;
		}
		internal static void parseFields(IfcAlignment2DVerSegParabolicArc c, List<string> arrFields, ref int ipos)
		{
			IfcAlignment2DVerticalSegment.parseFields(c, arrFields, ref ipos);
			c.mParabolaConstant = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mIsConvex = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		internal static IfcAlignment2DVerSegParabolicArc Parse(string strDef) { IfcAlignment2DVerSegParabolicArc c = new IfcAlignment2DVerSegParabolicArc(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mParabolaConstant) + "," + ParserSTEP.BoolToString(mIsConvex); }
	}
	public partial class IfcAlignment2DVertical : BaseClassIfc //IFC4.1
	{
		internal List<int> mSegments = new List<int>();// : LIST [1:?] OF IfcAlignment2DVerticalSegment;
		public List<IfcAlignment2DVerticalSegment> Segments { get { return mSegments.ConvertAll(x => mDatabase[x] as IfcAlignment2DVerticalSegment); } set { mSegments = value.ConvertAll(x => x.mIndex); } }
		internal IfcAlignment2DVertical() : base() { }
		internal IfcAlignment2DVertical(IfcAlignment2DVertical o) : base() { mSegments = new List<int>(o.mSegments.ToArray()); }
		internal static void parseFields(IfcAlignment2DVertical a, List<string> arrFields, ref int ipos) { a.mSegments = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ","  + ParserSTEP.LinkToString(mSegments[0]);
			for (int icounter = 1; icounter < mSegments.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSegments[icounter]);
			return str + ")";
		}
		internal static IfcAlignment2DVertical Parse(string strDef) { IfcAlignment2DVertical a = new IfcAlignment2DVertical(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
	}
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
		protected IfcAlignment2DVerticalSegment(IfcAlignment2DVerticalSegment s) : base(s) { mStartDistAlong = s.mStartDistAlong; mHorizontalLength = s.mHorizontalLength; mStartHeight = s.mStartHeight; mStartGradient = s.mStartGradient; }
		protected IfcAlignment2DVerticalSegment(DatabaseIfc m, bool tangential, string startTag, string endTag, double startDist, double horizontalLength,double startHeight, double startGradient) : base(m, tangential, startTag, endTag) {  }
		protected static void parseFields(IfcAlignment2DVerticalSegment s, List<string> arrFields, ref int ipos)
		{
			IfcAlignment2DSegment.parseFields(s, arrFields, ref ipos);
			s.mStartDistAlong = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mHorizontalLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mStartHeight = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mStartGradient = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mStartDistAlong) + "," + ParserSTEP.DoubleToString(mHorizontalLength) + "," + ParserSTEP.DoubleToString(mStartHeight) + "," + ParserSTEP.DoubleToString(mStartGradient); }
	}
	
	public partial class IfcCircularArcSegment2D : IfcCurveSegment2D  //IFC4.1
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		private bool mIsCCW;// : IfcBoolean;

		public double Radius { get { return mRadius; } set { mRadius = value; } }
		public bool IsCCW { get { return mIsCCW; } set { mIsCCW = value; } }

		internal IfcCircularArcSegment2D() : base() { }
		internal IfcCircularArcSegment2D(DatabaseIfc db, IfcCircularArcSegment2D s) : base(db,s) { mRadius = s.mRadius; mIsCCW = s.mIsCCW; }
		internal IfcCircularArcSegment2D(IfcCartesianPoint start, double startDirection, double length, double radius, bool isCCW)
			: base(start, startDirection, length)
		{
			mRadius = radius;
			mIsCCW = isCCW;
		}
		internal static IfcCircularArcSegment2D Parse(string str) { IfcCircularArcSegment2D c = new IfcCircularArcSegment2D(); int pos = 0; c.Parse(str, ref pos, str.Length); return c; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mIsCCW = ParserSTEP.StripBool(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mRadius) + "," + ParserSTEP.BoolToString(mIsCCW); }
	}
	public partial class IfcClothoidalArcSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		private double mStartRadius;// : IfcPositiveLengthMeasure;
		private bool mIsCCW;// : IfcBoolean;
		private bool mIsEntry;// : IfcBoolean;
		private double mClothoidConstant;// : IfcReal;

		public double StartRadius { get { return mStartRadius; } set { mStartRadius = value; } }
		public bool IsCCW { get { return mIsCCW; } set { mIsCCW = value; } }
		public bool IsEntry { get { return mIsEntry; } set { mIsEntry = value; } }
		public double ClothoidConstant { get { return mClothoidConstant; } set { mClothoidConstant = value; } }

		internal IfcClothoidalArcSegment2D() : base() { }
		internal IfcClothoidalArcSegment2D(DatabaseIfc db, IfcClothoidalArcSegment2D s) : base(db,s) { mStartRadius = s.mStartRadius; mIsCCW = s.mIsCCW; mIsEntry = s.mIsEntry; mClothoidConstant = s.mClothoidConstant; }
		internal IfcClothoidalArcSegment2D(IfcCartesianPoint start, double startDirection, double length, double radius, bool isCCW, bool isEntry, double clothoidConstant)
			: base(start, startDirection, length)
		{
			mStartRadius = radius;
			mIsCCW = isCCW;
			mIsEntry = isEntry;
			mClothoidConstant = clothoidConstant;
		}
		internal static IfcClothoidalArcSegment2D Parse(string str) { IfcClothoidalArcSegment2D c = new IfcClothoidalArcSegment2D(); int pos = 0; c.Parse(str, ref pos, str.Length); return c; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mStartRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mIsCCW = ParserSTEP.StripBool(str, ref pos, len);
			mIsEntry = ParserSTEP.StripBool(str, ref pos, len);
			mClothoidConstant = ParserSTEP.StripDouble(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mStartRadius) + "," + ParserSTEP.BoolToString(mIsCCW) + "," + ParserSTEP.BoolToString(mIsEntry) + "," + ParserSTEP.DoubleToString(mClothoidConstant); }
	}
	
	public abstract partial class IfcCurveSegment2D : IfcBoundedCurve
	{
		private int mStartPoint;// : IfcCartesianPoint;
		private double mStartDirection;// : IfcPlaneAngleMeasure;
		private double mSegmentLength;// : IfcPositiveLengthMeasure;

		internal IfcCartesianPoint StartPoint { get { return mDatabase[mStartPoint] as IfcCartesianPoint; } set { mStartPoint = value.mIndex; } }

		protected IfcCurveSegment2D() : base() { }
		protected IfcCurveSegment2D(DatabaseIfc db, IfcCurveSegment2D p) : base(db,p) { StartPoint = db.Factory.Duplicate( p.StartPoint) as IfcCartesianPoint; mStartDirection = p.mStartDirection; mSegmentLength = p.mSegmentLength; }
		protected IfcCurveSegment2D(IfcCartesianPoint start, double startDirection, double length)
			: base(start.mDatabase)
		{
			mStartDirection = startDirection;
			mSegmentLength = length;
		}

		protected virtual void Parse(string str, ref int pos, int len)
		{
			mStartPoint = ParserSTEP.StripLink(str, ref pos, len);
			mStartDirection = ParserSTEP.StripDouble(str, ref pos, len);
			mSegmentLength = ParserSTEP.StripDouble(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",#" + mStartPoint + "," + ParserSTEP.DoubleToString(mStartDirection) + "," + ParserSTEP.DoubleToString(mSegmentLength); }
	}
	
	public partial class IfcLineSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		internal IfcLineSegment2D() : base() { }
		internal IfcLineSegment2D(DatabaseIfc db, IfcLineSegment2D s) : base(db, s) { }
		internal IfcLineSegment2D(IfcCartesianPoint start, double startDirection, double length)
			: base(start, startDirection, length) { }
		internal static IfcLineSegment2D Parse(string str) { IfcLineSegment2D c = new IfcLineSegment2D(); int pos = 0, len = str.Length; c.Parse(str, ref pos, len); return c; }
	}
	
	public abstract partial class IfcPositioningElement : IfcProduct //IFC4.1
	{
		protected IfcPositioningElement() : base() { }
		protected IfcPositioningElement(DatabaseIfc db, IfcPositioningElement e) : base(db,e,false) { }
		protected static void parseFields(IfcPositioningElement p, List<string> arrFields, ref int ipos) { IfcProduct.parseFields(p, arrFields, ref ipos); }
	}
}
