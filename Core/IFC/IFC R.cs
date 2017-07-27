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
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcRadiusDimension : IfcDimensionCurveDirectedCallout // DEPRECEATED IFC4
	{
		internal IfcRadiusDimension() : base() { }
		internal new static IfcRadiusDimension Parse(string str) { IfcRadiusDimension d = new IfcRadiusDimension(); int pos = 0; d.Parse(str,ref pos, str.Length); return d; }
	}
	public partial class IfcRailing : IfcBuildingElement
	{
		internal IfcRailingTypeEnum mPredefinedType = IfcRailingTypeEnum.NOTDEFINED;// : OPTIONAL IfcRailingTypeEnum
		public IfcRailingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRailing() : base() { }
		internal IfcRailing(DatabaseIfc db, IfcRailing r, bool downStream) : base(db, r, downStream) { mPredefinedType = r.mPredefinedType; }
		public IfcRailing(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcRailing Parse(string str) { IfcRailing r = new IfcRailing(); int pos = 0; r.Parse(str, ref pos, str.Length); return r; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRailingTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcRailingType : IfcBuildingElementType
	{
		internal IfcRailingTypeEnum mPredefinedType = IfcRailingTypeEnum.NOTDEFINED;
		public IfcRailingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRailingType() : base() { }
		internal IfcRailingType(DatabaseIfc db, IfcRailingType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcRailingType(DatabaseIfc m, string name, IfcRailingTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcRailingType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcRailingTypeEnum)Enum.Parse(typeof(IfcRailingTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcRailingType Parse(string strDef) { IfcRailingType t = new IfcRailingType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcRamp : IfcBuildingElement
	{
		internal IfcRampTypeEnum mPredefinedType = IfcRampTypeEnum.NOTDEFINED;// OPTIONAL : IfcRampTypeEnum
		public IfcRampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRamp() : base() { }
		internal IfcRamp(DatabaseIfc db, IfcRamp r, bool downStream) : base(db, r, downStream) { mPredefinedType = r.mPredefinedType; }
		public IfcRamp(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcRamp Parse(string str) { IfcRamp r = new IfcRamp(); int pos = 0; r.Parse(str, ref pos, str.Length); return r; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRampTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcRampFlight : IfcBuildingElement
	{
		internal IfcRampFlightTypeEnum mPredefinedType = IfcRampFlightTypeEnum.NOTDEFINED;// OPTIONAL : IfcRampTypeEnum
		public IfcRampFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRampFlight() : base() { }
		internal IfcRampFlight(DatabaseIfc db, IfcRampFlight f, bool downStream) : base(db, f, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcRampFlight(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcRampFlight Parse(string str, ReleaseVersion schema) { IfcRampFlight f = new IfcRampFlight(); int pos = 0; f.Parse(str, ref pos, str.Length, schema); return f; }
		protected void Parse(string str, ref int pos, int len, ReleaseVersion schema)
		{
			base.Parse(str, ref pos, len);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcRampFlightTypeEnum>(s.Substring(1, s.Length - 2), out mPredefinedType);
			}
		}
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? base.BuildStringSTEP() : base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcRampFlightType : IfcBuildingElementType
	{
		internal IfcRampFlightTypeEnum mPredefinedType = IfcRampFlightTypeEnum.NOTDEFINED;
		public IfcRampFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRampFlightType() : base() { }
		internal IfcRampFlightType(DatabaseIfc db, IfcRampFlightType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcRampFlightType(DatabaseIfc m, string name, IfcRampFlightTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcRampFlightType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcRampFlightTypeEnum)Enum.Parse(typeof(IfcRampFlightTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcRampFlightType Parse(string strDef) { IfcRampFlightType t = new IfcRampFlightType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcRampType : IfcBuildingElementType //IFC4
	{
		internal IfcRampTypeEnum mPredefinedType = IfcRampTypeEnum.NOTDEFINED;
		public IfcRampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRampType() : base() { }
		internal IfcRampType(DatabaseIfc db, IfcRampType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcRampType(DatabaseIfc m, string name, IfcRampTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcRampType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcRampTypeEnum)Enum.Parse(typeof(IfcRampTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcRampType Parse(string strDef) { IfcRampType t = new IfcRampType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcRationalBezierCurve : IfcBezierCurve // DEPRECEATED IFC4
	{
		internal List<double> mWeightsData = new List<double>();// : LIST [2:?] OF REAL;	
		internal IfcRationalBezierCurve() : base() { }
		internal IfcRationalBezierCurve(DatabaseIfc db, IfcRationalBezierCurve c) : base(db, c) { mWeightsData.AddRange(c.mWeightsData); }
		internal new static IfcRationalBezierCurve Parse(string str) { IfcRationalBezierCurve c = new IfcRationalBezierCurve(); int pos = 0; c.Parse(str, ref pos, str.Length); return c; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> arrNodes = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			for (int icounter = 0; icounter < arrNodes.Count; icounter++)
				mWeightsData.Add(ParserSTEP.ParseDouble(arrNodes[icounter]));
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.DoubleToString(mWeightsData[0]);
			for (int icounter = 1; icounter < mWeightsData.Count; icounter++)
				str += "," + ParserSTEP.DoubleToString(mWeightsData[icounter]);
			return str + ")";
		}
	}
	public partial class IfcRationalBSplineCurveWithKnots : IfcBSplineCurveWithKnots
	{
		internal List<double> mWeightsData = new List<double>();// : LIST [2:?] OF REAL;	
		internal IfcRationalBSplineCurveWithKnots() : base() { }
		internal IfcRationalBSplineCurveWithKnots(DatabaseIfc db, IfcRationalBSplineCurveWithKnots c) : base(db, c) { mWeightsData.AddRange(c.mWeightsData); }

		internal new static IfcRationalBSplineCurveWithKnots Parse(string str)
		{
			IfcRationalBSplineCurveWithKnots c = new IfcRationalBSplineCurveWithKnots();
			int pos = 0, len = str.Length;
			c.Parse(str, ref pos, len);
			c.mWeightsData = ParserSTEP.StripListDouble(str, ref pos, len);
			return c;
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.DoubleToString(mWeightsData[0]);
			for (int icounter = 1; icounter < mWeightsData.Count; icounter++)
				str += "," + ParserSTEP.DoubleToString(mWeightsData[icounter]);
			return str + ")";
		}
	}
	public partial class IfcRationalBSplineSurfaceWithKnots : IfcBSplineSurfaceWithKnots
	{
		internal List<List<double>> mWeightsData = new List<List<double>>();// : LIST [2:?] OF REAL;	
		internal IfcRationalBSplineSurfaceWithKnots() : base() { }
		internal IfcRationalBSplineSurfaceWithKnots(DatabaseIfc db, IfcRationalBSplineSurfaceWithKnots s) : base(db,s)
		{
			for (int icounter = 0; icounter < s.mWeightsData.Count; icounter++)
				mWeightsData.Add(new List<double>(s.mWeightsData[icounter].ToArray()));
		}
		
		internal new static IfcRationalBSplineSurfaceWithKnots Parse(string str)
		{
			IfcRationalBSplineSurfaceWithKnots r = new IfcRationalBSplineSurfaceWithKnots();
			int pos = 0, len = str.Length;
			r.Parse(str, ref pos, len);
			r.mWeightsData = ParserSTEP.StripListListDouble(str, ref pos, len);
			return r;
		}
		protected override string BuildStringSTEP()
		{
			List<double> wts = mWeightsData[0];
			string str = base.BuildStringSTEP() + ",((" +
				ParserSTEP.DoubleToString(wts[0]);
			for (int jcounter = 1; jcounter < wts.Count; jcounter++)
				str += "," + ParserSTEP.DoubleToString(wts[jcounter]);
			str += ")";
			for (int icounter = 1; icounter < mWeightsData.Count; icounter++)
			{
				wts = mWeightsData[icounter];
				str += ",(" + ParserSTEP.DoubleToString(wts[0]);
				for (int jcounter = 1; jcounter < wts.Count; jcounter++)
					str += "," + ParserSTEP.DoubleToString(wts[jcounter]);
				str += ")";
			}
			return str + ")";
		}
	}
	public partial class IfcRectangleHollowProfileDef : IfcRectangleProfileDef
	{
		internal double mWallThickness;// : IfcPositiveLengthMeasure;
		internal double mInnerFilletRadius = double.NaN, mOuterFilletRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		public double WallThickness { get { return mWallThickness; } set { mWallThickness = value; } }
		public double InnerFilletRadius { get { return mInnerFilletRadius; } set { mInnerFilletRadius = value; } }
		public double OuterFilletRadius { get { return mOuterFilletRadius; } set { mOuterFilletRadius = value; } }

		internal IfcRectangleHollowProfileDef() : base() { }
		internal IfcRectangleHollowProfileDef(DatabaseIfc db, IfcRectangleHollowProfileDef p) : base(db, p) { mWallThickness = p.mWallThickness; mInnerFilletRadius = p.mInnerFilletRadius; mOuterFilletRadius = p.mOuterFilletRadius; }
		public IfcRectangleHollowProfileDef(DatabaseIfc m, string name, double depth, double width, double wallThickness, double outerFilletRadius, double innerFilletRadius)
			: base(m, name, depth, width) { mWallThickness = wallThickness; mOuterFilletRadius = outerFilletRadius; mInnerFilletRadius = innerFilletRadius; }
		internal static void parseFields(IfcRectangleHollowProfileDef p, List<string> arrFields, ref int ipos) { IfcRectangleProfileDef.parseFields(p, arrFields, ref ipos); p.mWallThickness = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mInnerFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mOuterFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mWallThickness) + (double.IsNaN(mInnerFilletRadius) || mInnerFilletRadius < mDatabase.Tolerance ? ",$," : "," + ParserSTEP.DoubleOptionalToString(mInnerFilletRadius) + ",") + (double.IsNaN(mOuterFilletRadius) || mOuterFilletRadius < mDatabase.Tolerance ? "$" : ParserSTEP.DoubleOptionalToString(mOuterFilletRadius)); }
		internal new static IfcRectangleHollowProfileDef Parse(string strDef) { IfcRectangleHollowProfileDef p = new IfcRectangleHollowProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
	}
	public partial class IfcRectangleProfileDef : IfcParameterizedProfileDef //	SUPERTYPE OF(ONEOF(IfcRectangleHollowProfileDef, IfcRoundedRectangleProfileDef))
	{
		internal double mXDim, mYDim;// : IfcPositiveLengthMeasure; 
		public double XDim { get { return mXDim; } set { mXDim = value; } }
		public double YDim { get { return mYDim; } set { mYDim = value; } }

		internal IfcRectangleProfileDef() : base() { }
		internal IfcRectangleProfileDef(DatabaseIfc db, IfcRectangleProfileDef p) : base(db, p) { mXDim = p.mXDim; mYDim = p.mYDim; }
		public IfcRectangleProfileDef(DatabaseIfc db, string name, double xDim, double yDim) : base(db,name) { mXDim = xDim; mYDim = yDim; }
		internal static void parseFields(IfcRectangleProfileDef p, List<string> arrFields, ref int ipos) { IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos); p.mXDim = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mYDim = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mXDim) + "," + ParserSTEP.DoubleToString(mYDim); }
		internal new static IfcRectangleProfileDef Parse(string strDef) { IfcRectangleProfileDef p = new IfcRectangleProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }

		internal override double ProfileDepth { get { return YDim; } }
		internal override double ProfileWidth { get { return XDim; } }
	}
	public partial class IfcRectangularPyramid : IfcCsgPrimitive3D
	{
		internal double mXLength, mYLength, mHeight;// : IfcPositiveLengthMeasure;
		public double XLength { get { return mXLength; } set { mXLength = value; } }
		public double YLength { get { return mYLength; } set { mYLength = value; } }
		public double Height { get { return mHeight; } set { mHeight = value; } }

		internal IfcRectangularPyramid() : base() { }
		internal IfcRectangularPyramid(DatabaseIfc db, IfcRectangularPyramid p) : base(db,p) { mXLength = p.mXLength; mYLength = p.mYLength; mHeight = p.mHeight; }
		internal static IfcRectangularPyramid Parse(string str) { IfcRectangularPyramid p = new IfcRectangularPyramid(); int pos = 0; p.Parse(str, ref pos, str.Length); return p; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mXLength = ParserSTEP.StripDouble(str, ref pos, len);
			mYLength = ParserSTEP.StripDouble(str, ref pos, len);
			mHeight = ParserSTEP.StripDouble(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mXLength) + "," + ParserSTEP.DoubleToString(mYLength) + "," + ParserSTEP.DoubleToString(mHeight); }
	}
	public partial class IfcRectangularTrimmedSurface : IfcBoundedSurface
	{
		internal int mBasisSurface;// : IfcPlane;
		internal double mU1, mV1, mU2, mV2;// : IfcParameterValue;
		internal bool mUsense, mVsense;// : BOOLEAN; 

		public IfcPlane BasisSurface { get { return mDatabase[mBasisSurface] as IfcPlane; } set { mBasisSurface = value.mIndex; } }

		internal IfcRectangularTrimmedSurface() : base() { }
		internal IfcRectangularTrimmedSurface(DatabaseIfc db, IfcRectangularTrimmedSurface s) : base(db,s)
		{
			BasisSurface = db.Factory.Duplicate(s.BasisSurface) as IfcPlane;
			mU1 = s.mU1;
			mU2 = s.mU2;
			mV1 = s.mV1;
			mV2 = s.mV2;
			mUsense = s.mUsense;
			mVsense = s.mVsense;
		}
		internal static IfcRectangularTrimmedSurface Parse(string str)
		{
			IfcRectangularTrimmedSurface s = new IfcRectangularTrimmedSurface();
			int pos = 0, len = str.Length;
			s.mBasisSurface = ParserSTEP.StripLink(str, ref pos, len);
			s.mU1 = ParserSTEP.StripDouble(str, ref pos, len);
			s.mU2 = ParserSTEP.StripDouble(str, ref pos, len);
			s.mV1 = ParserSTEP.StripDouble(str, ref pos, len);
			s.mV2 = ParserSTEP.StripDouble(str, ref pos, len);
			s.mUsense = ParserSTEP.StripBool(str, ref pos, len);
			s.mVsense = ParserSTEP.StripBool(str, ref pos, len);
			return s;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.DoubleToString(mU1) + "," + ParserSTEP.DoubleToString(mV1) + "," + ParserSTEP.DoubleToString(mU2) + "," + ParserSTEP.DoubleToString(mV2) + "," + ParserSTEP.BoolToString(mUsense) + "," + ParserSTEP.BoolToString(mVsense); }
	}
	public partial class IfcRecurrencePattern : BaseClassIfc // IFC4
	{
		internal IfcRecurrenceTypeEnum mRecurrenceType = IfcRecurrenceTypeEnum.WEEKLY; //:	IfcRecurrenceTypeEnum;
		internal List<int> mDayComponent = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcDayInMonthNumber;
		internal List<int> mWeekdayComponent = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcDayInWeekNumber;
		internal List<int> mMonthComponent = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcMonthInYearNumber;
		internal int mPosition = 0, mInterval = 0, mOccurrences = 0;//	 :	OPTIONAL IfcInteger;
		internal List<int> mTimePeriods = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcTimePeriod;
		internal IfcRecurrencePattern() : base() { }
		//internal IfcRecurrencePattern(IfcRecurrencePattern p) : base()
		//{
		//	mRecurrenceType = p.mRecurrenceType;
		//	mDayComponent.AddRange(p.mDayComponent);
		//	mWeekdayComponent.AddRange(p.mWeekdayComponent);
		//	mMonthComponent.AddRange(p.mMonthComponent);
		//	mPosition = p.mPosition;
		//	mInterval = p.mInterval;
		//	mOccurrences = p.mOccurrences;
		//	mTimePeriods.AddRange(p.mTimePeriods);
		//}
		internal IfcRecurrencePattern(DatabaseIfc m, IfcRecurrenceTypeEnum type, List<int> days, List<int> weekdays, List<int> months, int position, int interval, int occurences, List<IfcTimePeriod> periods)
			: base(m)
		{
			mRecurrenceType = type;
			if (days != null)
				mDayComponent.AddRange(days);
			if (weekdays != null)
				mWeekdayComponent.AddRange(weekdays);
			if (months != null)
				mMonthComponent.AddRange(months);
			mPosition = position;
			mInterval = interval;
			mOccurrences = occurences;
			if (periods != null)
				mTimePeriods = periods.ConvertAll(x => x.mIndex);
		}
		internal static IfcRecurrencePattern Parse(string strDef) { IfcRecurrencePattern m = new IfcRecurrencePattern(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcRecurrencePattern m, List<string> arrFields, ref int ipos)
		{
			m.mRecurrenceType = (IfcRecurrenceTypeEnum)Enum.Parse(typeof(IfcRecurrenceTypeEnum), arrFields[ipos++].Replace(".", ""));
			string s = arrFields[ipos++];
			if (s.StartsWith("("))
				m.mDayComponent = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => int.Parse(x));
			s = arrFields[ipos++];
			if (s.StartsWith("("))
				m.mWeekdayComponent = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => int.Parse(x));
			s = arrFields[ipos++];
			if (s.StartsWith("("))
				m.mMonthComponent = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => int.Parse(x));
			s = arrFields[ipos++];
			if (s != "$")
				m.mPosition = int.Parse(s);
			s = arrFields[ipos++];
			if (s != "$")
				m.mInterval = int.Parse(s);
			s = arrFields[ipos++];
			if (s != "$")
				m.mOccurrences = int.Parse(s);
			m.mTimePeriods = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",." + mRecurrenceType.ToString();
			if (mDayComponent.Count == 0)
				str += ".,$,";
			else
			{
				str += ".,(" + mDayComponent[0];
				for (int icounter = 1; icounter < mDayComponent.Count; icounter++)
					str += "," + mDayComponent[icounter];
				str += "),";
			}
			if (mWeekdayComponent.Count == 0)
				str += "$,";
			else
			{
				str += "(" + mWeekdayComponent[0];
				for (int icounter = 1; icounter < mWeekdayComponent.Count; icounter++)
					str += "," + mWeekdayComponent[icounter];
				str += "),";
			}
			if (mMonthComponent.Count == 0)
				str += "$,";
			else
			{
				str += "(" + mMonthComponent[0];
				for (int icounter = 1; icounter < mMonthComponent.Count; icounter++)
					str += "," + mMonthComponent[icounter];
				str += "),";
			}
			str += mInterval + "," + mPosition + "," + mOccurrences;
			if (mTimePeriods.Count == 0)
				str += ",$";
			else
			{
				str += ",(" + ParserSTEP.LinkToString(mTimePeriods[0]);
				for (int icounter = 1; icounter < mTimePeriods.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mTimePeriods[icounter]);
				str += ")";
			}
			return str;
		}
	}
	public partial class IfcReference : BaseClassIfc, IfcMetricValueSelect, IfcAppliedValueSelect // IFC4
	{
		internal string mTypeIdentifier = "$", mAttributeIdentifier = "$"; //:	OPTIONAL IfcIdentifier;
		internal string mInstanceName = "$"; //:OPTIONAL IfcLabel;
		internal List<int> mListPositions = new List<int>();//	 :	OPTIONAL LIST [1:?] OF INTEGER;
		private int mInnerReference = 0;//	 :	OPTIONAL IfcReference;

		public string TypeIdentifier { get { return (mTypeIdentifier == "$" ? "" : ParserIfc.Decode(mTypeIdentifier)); } set { mTypeIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string AttributeIdentifier { get { return (mAttributeIdentifier == "$" ? "" : ParserIfc.Decode(mAttributeIdentifier)); } set { mAttributeIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string InstanceName { get { return (mInstanceName == "$" ? "" : ParserIfc.Decode(mInstanceName)); } set { mInstanceName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<int> ListPositions { get { return new ReadOnlyCollection<int>( mListPositions); } }
		public IfcReference InnerReference { get { return mDatabase[mInnerReference] as IfcReference; } set { mInnerReference = (value == null ? 0 : value.mIndex); } }

		internal IfcReference() : base() { }
		internal IfcReference(DatabaseIfc db, IfcReference r) : base(db,r)
		{
			mTypeIdentifier = r.mTypeIdentifier;
			mAttributeIdentifier = r.mAttributeIdentifier;
			mInstanceName = r.mInstanceName;
			mListPositions.AddRange(r.mListPositions);
			InnerReference = db.Factory.Duplicate( r.InnerReference) as IfcReference;
		}
		public IfcReference(DatabaseIfc db) : base(db) { }	
		public IfcReference(DatabaseIfc db, string typeId, string attributeId, string instanceName) : base(db)
		{
			TypeIdentifier = typeId;
			AttributeIdentifier = attributeId;
			InstanceName = instanceName;
		}
		public IfcReference(string typeId, string attributeId, string instanceName, IfcReference inner)
			: this(inner.mDatabase,typeId,attributeId,instanceName) { InnerReference = inner;  }
		public IfcReference(string typeId, string attributeId, string instanceName, int position, IfcReference inner)
			: this(typeId, attributeId, instanceName, inner) { mListPositions.Add(position); }
		public IfcReference(string typeId, string attributeId, string instanceName, List<int> positions, IfcReference inner)
			: this(typeId, attributeId, instanceName, inner) { mListPositions.AddRange(positions); }

		internal static IfcReference Parse(string strDef) { IfcReference m = new IfcReference(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcReference m, List<string> arrFields, ref int ipos)
		{
			m.mTypeIdentifier = arrFields[ipos++].Replace("'", "");
			m.mAttributeIdentifier = arrFields[ipos++].Replace("'", "");
			m.mInstanceName = arrFields[ipos++].Replace("'", "");
			string s = arrFields[ipos++];
			if (s.StartsWith("("))
				m.mListPositions = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => int.Parse(x));
			m.mInnerReference = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			if (mDatabase.Release == ReleaseVersion.IFC2x3)
				return "";
			string str = base.BuildStringSTEP() + (mTypeIdentifier == "$" ? ",$" : ",'" + mTypeIdentifier + "'") + (mAttributeIdentifier == "$" ? ",$," : ",'" + mAttributeIdentifier + "',") +
				(mInstanceName == "$" ? "$," : "'" + mInstanceName + "',");
			if (mListPositions.Count == 0)
				str += "$,";
			else
			{
				str += "(" + mListPositions[0];
				for (int icounter = 1; icounter < mListPositions.Count; icounter++)
					str += "," + mListPositions[icounter];
				str += "),";
			}
			return str + ParserSTEP.LinkToString(mInnerReference);
		}
		private static string stripReference(string referenceDescription)
		{
			string reference = "";
			int length = referenceDescription.Length;
			for(int icounter = 0; icounter < length; icounter++)
			{
				char c = referenceDescription[icounter];
				if (c == '.')
					return reference;
				reference += c;
				if(c == '\'')
				{
					for(++icounter;icounter<length; icounter++)
					{
						c = referenceDescription[icounter];
						reference += c;
						if(c == '\'')
						{
							if (icounter + 1 == length) 
								break;
							if (referenceDescription[icounter + 1] == '\'')
							{
								reference += '\'';
								icounter++;
							}
							else
								break;
						}
					}
				}
			}
			return reference;
		}
		public static IfcReference ParseDescription(DatabaseIfc db, string referenceDescription)
		{
			// Example description 
			//RepresentationMaps.MappedRepresentation['Body'].Items[*].StyledByItem.Styles\IfcSurfaceStyle.Styles\IfcSurfaceStyleWithTextures.Textures\IfcImageTexture.UrlReference
			string reference = stripReference(referenceDescription);
			IfcReference innerReference = null;
			if (reference.Length + 1 < referenceDescription.Length)
				innerReference = ParseDescription(db, referenceDescription.Substring(reference.Length + 1));
			int i = reference.IndexOf('\\');
			string attributeId = "",instanceName = "";
			if(i > 0)	
			{
				if(innerReference != null)
					innerReference.TypeIdentifier = reference.Substring(i + 1, reference.Length - i - 1);
				reference = reference.Substring(0, i);
			}
			List<int> positions = new List<int>();
			if (reference[reference.Length-1] == ']')
			{
				i = reference.IndexOf('[');
				attributeId = reference.Substring(0, i);
				reference = reference.Substring(i+1, reference.Length - i - 2);
				if (reference != "*")
				{
					if (!reference.Contains('\'') && reference.Contains(' '))
					{
						string[] fields = reference.Split(" ".ToCharArray());
						foreach (string s in fields)
						{
							if (int.TryParse(s, out i))
								positions.Add(i);
						}
					}
					else
					{
						if (int.TryParse(reference, out i))
							positions.Add(i);
						else
							instanceName = reference.Replace("'", "");
					}
				}
			}
			else
				attributeId = reference;
			IfcReference result = new IfcReference(db);
			result.AttributeIdentifier = attributeId;
			result.InstanceName = instanceName;
			result.mListPositions.AddRange(positions);
			result.InnerReference = innerReference;
			return result;
		}
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcReferencesValueDocument; // DEPRECEATED IFC4
	//ENTITY IfcRegularTimeSeries
	public partial class IfcReinforcementBarProperties : IfcPreDefinedProperties
	{
		internal double mTotalCrossSectionArea = 0;//:	IfcAreaMeasure;
		internal string mSteelGrade = "";//	:	IfcLabel;
		internal IfcReinforcingBarSurfaceEnum mBarSurface = IfcReinforcingBarSurfaceEnum.NONE;//	:	OPTIONAL IfcReinforcingBarSurfaceEnum;
		internal double mEffectiveDepth = 0;//:	OPTIONAL IfcLengthMeasure;
		internal double mNominalBarDiameter = 0;//	:	OPTIONAL IfcPositiveLengthMeasure;
		internal int mBarCount = 0;//	:	OPTIONAL IfcCountMeasure; 
		internal IfcReinforcementBarProperties() : base() { }
		internal IfcReinforcementBarProperties(IfcReinforcementBarProperties p) : base()
		{
			mTotalCrossSectionArea = p.mTotalCrossSectionArea;
			mSteelGrade = p.mSteelGrade;
			mBarSurface = p.mBarSurface;
			mEffectiveDepth = p.mEffectiveDepth;
			mNominalBarDiameter = p.mNominalBarDiameter;
			mBarCount = p.mBarCount;
		}
		internal static IfcReinforcementBarProperties Parse(string strDef) { IfcReinforcementBarProperties p = new IfcReinforcementBarProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcReinforcementBarProperties p, List<string> arrFields, ref int ipos)
		{
			p.mTotalCrossSectionArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mSteelGrade = arrFields[ipos++].Replace("'", "");
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				p.mBarSurface = (IfcReinforcingBarSurfaceEnum)Enum.Parse(typeof(IfcReinforcingBarSurfaceEnum), arrFields[ipos++].Replace(".", ""));
			p.mEffectiveDepth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mNominalBarDiameter = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mBarCount = ParserSTEP.ParseInt(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mTotalCrossSectionArea) + ",'" + mSteelGrade + "',." + mBarSurface.ToString() + ".," +
				ParserSTEP.DoubleOptionalToString(mEffectiveDepth) + "," + ParserSTEP.DoubleOptionalToString(mNominalBarDiameter) + "," + ParserSTEP.IntOptionalToString(mBarCount);
		}
	}
	public partial class IfcReinforcementDefinitionProperties : IfcPreDefinedPropertySet //IFC2x3 IfcPropertySetDefinition
	{
		internal string mDefinitionType = "$";// 	:	OPTIONAL IfcLabel; 
		internal List<int> mReinforcementSectionDefinitions = new List<int>();// :	LIST [1:?] OF IfcSectionReinforcementProperties;

		public ReadOnlyCollection<IfcSectionReinforcementProperties> ReinforcementSectionDefinitions { get { return new ReadOnlyCollection<IfcSectionReinforcementProperties>( mReinforcementSectionDefinitions.ConvertAll(x=>mDatabase[x] as IfcSectionReinforcementProperties)); } }
		internal IfcReinforcementDefinitionProperties() : base() { }
		internal IfcReinforcementDefinitionProperties(DatabaseIfc db, IfcReinforcementDefinitionProperties p) : base(db, p)
		{
			mDefinitionType = p.mDefinitionType;
			p.ReinforcementSectionDefinitions.ToList().ForEach(x => addSection( db.Factory.Duplicate(x) as IfcSectionReinforcementProperties));
		}
		internal IfcReinforcementDefinitionProperties(string name, string type, List<IfcSectionReinforcementProperties> sectProps)
			: base(sectProps[0].mDatabase, name) { if (!string.IsNullOrEmpty(type)) mDefinitionType = type; mReinforcementSectionDefinitions = sectProps.ConvertAll(x => x.mIndex); }
		internal static IfcReinforcementDefinitionProperties Parse(string strDef) { IfcReinforcementDefinitionProperties p = new IfcReinforcementDefinitionProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcReinforcementDefinitionProperties p, List<string> arrFields, ref int ipos)
		{
			IfcPropertySetDefinition.parseFields(p, arrFields, ref ipos);
			p.mDefinitionType = arrFields[ipos++].Replace("'", "");
			p.mReinforcementSectionDefinitions = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + (mDefinitionType == "$" ? ",$,(#" : ",'" + mDefinitionType + "',(#") + mReinforcementSectionDefinitions[0];
			for (int icounter = 1; icounter < mReinforcementSectionDefinitions.Count; icounter++)
				result += ",#" + mReinforcementSectionDefinitions;
			return result + ")";
		}
		internal void addSection(IfcSectionReinforcementProperties section) { mReinforcementSectionDefinitions.Add(section.mIndex); }
	}
	public partial class IfcReinforcingBar : IfcReinforcingElement
	{
		private double mNominalDiameter = double.NaN;// : IfcPositiveLengthMeasure; 	IFC4 OPTIONAL
		internal double mCrossSectionArea = double.NaN;// : IfcAreaMeasure; IFC4 OPTIONAL
		internal double mBarLength = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcReinforcingBarTypeEnum mPredefinedType = IfcReinforcingBarTypeEnum.NOTDEFINED;// : IfcReinforcingBarRoleEnum; IFC4 OPTIONAL
		internal IfcReinforcingBarSurfaceEnum mBarSurface = IfcReinforcingBarSurfaceEnum.NONE;// //: OPTIONAL IfcReinforcingBarSurfaceEnum; 

		public double NominalDiameter
		{
			get
			{
				if (mNominalDiameter > mDatabase.Tolerance)
					return mNominalDiameter;
				IfcReinforcingBarType t = RelatingType as IfcReinforcingBarType;
				return (t != null ? t.NominalDiameter : 0);
			}
			set { mNominalDiameter = value; }
		}
		public IfcReinforcingBarTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcReinforcingBar() : base() { }
		internal IfcReinforcingBar(DatabaseIfc db, IfcReinforcingBar b) : base(db, b)
		{
			mNominalDiameter = b.mNominalDiameter;
			mCrossSectionArea = b.mCrossSectionArea;
			mBarLength = b.mBarLength;
			mPredefinedType = b.mPredefinedType;
			mBarSurface = b.mBarSurface;
		}
		public IfcReinforcingBar(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcReinforcingBar Parse(string strDef) { IfcReinforcingBar b = new IfcReinforcingBar(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcReinforcingBar c, List<string> arrFields, ref int ipos)
		{
			IfcReinforcingElement.parseFields(c, arrFields, ref ipos);
			c.mNominalDiameter = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mCrossSectionArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mBarLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			string str = arrFields[ipos++];
			if (str.StartsWith("."))
				c.mPredefinedType = (IfcReinforcingBarTypeEnum)Enum.Parse(typeof(IfcReinforcingBarTypeEnum), str.Replace(".", ""));
			str = arrFields[ipos++];
			if (str.StartsWith("."))
				c.mBarSurface = (IfcReinforcingBarSurfaceEnum)Enum.Parse(typeof(IfcReinforcingBarSurfaceEnum), str.Replace(".", ""));
		}
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + "," + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ParserSTEP.DoubleToString(mNominalDiameter) + "," + ParserSTEP.DoubleToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mBarLength) + ",." + mPredefinedType.ToString() + ".," :
				ParserSTEP.DoubleOptionalToString(mNominalDiameter) + "," + ParserSTEP.DoubleOptionalToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mBarLength) + (mPredefinedType == IfcReinforcingBarTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,"));
			return result + (mBarSurface == IfcReinforcingBarSurfaceEnum.NONE ? "$" : "." + mBarSurface.ToString() + ".");
		}
	}
	public partial class IfcReinforcingBarType : IfcReinforcingElementType  //IFC4
	{
		internal IfcReinforcingBarTypeEnum mPredefinedType = IfcReinforcingBarTypeEnum.NOTDEFINED;// : IfcFastenerTypeEnum; //IFC4
		private double mNominalDiameter;// : IfcPositiveLengthMeasure; 	IFC4 OPTIONAL
		internal double mCrossSectionArea;// : IfcAreaMeasure; IFC4 OPTIONAL
		internal double mBarLength;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcReinforcingBarSurfaceEnum mBarSurface = IfcReinforcingBarSurfaceEnum.NONE;// //: OPTIONAL IfcReinforcingBarSurfaceEnum; 
		internal string mBendingShapeCode = "$";//	:	OPTIONAL IfcLabel;
		internal List<IfcBendingParameterSelect> mBendingParameters = new List<IfcBendingParameterSelect>();//	:	OPTIONAL LIST [1:?] OF IfcBendingParameterSelect;

		public IfcReinforcingBarTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public double NominalDiameter { get { return mNominalDiameter; } set { mNominalDiameter = value; } }

		internal IfcReinforcingBarType() : base() { }
		internal IfcReinforcingBarType(DatabaseIfc db, IfcReinforcingBarType t) : base(db, t)
		{
			mPredefinedType = t.mPredefinedType;
			mNominalDiameter = t.mNominalDiameter;
			mCrossSectionArea = t.mCrossSectionArea;
			mBarLength = t.mBarLength;
			mBarSurface = t.mBarSurface;
			mBendingShapeCode = t.mBendingShapeCode;
			mBendingParameters.AddRange(t.mBendingParameters);
		}
		 
		public IfcReinforcingBarType(DatabaseIfc m, string name, IfcReinforcingBarTypeEnum type, double diameter, double area, double length, IfcReinforcingBarSurfaceEnum surface, string shapecode, List<IfcBendingParameterSelect> bends)
			: base(m)
		{
			Name = name;
			mPredefinedType = type;
			mNominalDiameter = diameter;
			mCrossSectionArea = area;
			mBarLength = length;
			mBarSurface = surface;
			if (!string.IsNullOrEmpty(shapecode))
				mBendingShapeCode = shapecode;
			if (bends != null && bends.Count > 0)
				mBendingParameters.AddRange(bends);
		}
		internal new static IfcReinforcingBarType Parse(string strDef) { int ipos = 0; IfcReinforcingBarType t = new IfcReinforcingBarType(); parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal static void parseFields(IfcReinforcingBarType t, List<string> arrFields, ref int ipos)
		{
			IfcReinforcingElementType.parseFields(t, arrFields, ref ipos);
			t.mPredefinedType = (IfcReinforcingBarTypeEnum)Enum.Parse(typeof(IfcReinforcingBarTypeEnum), arrFields[ipos++].Replace(".", ""));
			t.mNominalDiameter = ParserSTEP.ParseDouble(arrFields[ipos++]);
			t.mCrossSectionArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			t.mBarLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			t.mBarSurface = (IfcReinforcingBarSurfaceEnum)Enum.Parse(typeof(IfcReinforcingBarSurfaceEnum), arrFields[ipos++].Replace(".", ""));
			t.mBendingShapeCode = arrFields[ipos++];
			//t.mBendingParameters = 
			ipos++;
		}
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP();
			result += ",." + mPredefinedType + ".," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + ",";
			result += ParserSTEP.DoubleOptionalToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mBarLength);
			result += ",." + mBarSurface.ToString() + (mBendingShapeCode == "$" ? ".,$," : ".,'" + mBendingShapeCode + "',");
			if (mBendingParameters.Count == 0)
				result += "$";
			else
			{
				result += "(" + mBendingParameters[0].ToString();
				for (int icounter = 1; icounter < mBendingParameters.Count; icounter++)
					result += "," + mBendingParameters[icounter].ToString();
				result += ")";
			}
			return result;
		}
	}
	public abstract partial class IfcReinforcingElement : IfcElementComponent //	ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcingBar, IfcReinforcingMesh, IfcTendon, IfcTendonAnchor))
	{
		private string mSteelGrade = "$";// : OPTIONAL IfcLabel; //IFC4 Depreceated 
		[Obsolete("DEPRECEATED IFC4", false)]
		public string SteelGrade { get { return (mSteelGrade == "$" ? "" : ParserIfc.Decode(mSteelGrade)); } set { mSteelGrade = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcReinforcingElement() : base() { }
		protected IfcReinforcingElement(DatabaseIfc db, IfcReinforcingElement e) : base(db, e) { mSteelGrade = e.mSteelGrade; }
		public IfcReinforcingElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		protected static void parseFields(IfcReinforcingElement e, List<string> arrFields, ref int ipos)
		{
			IfcElementComponent.parseFields(e, arrFields, ref ipos);
			e.mSteelGrade = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mSteelGrade == "$" ? ",$" : ",'" + mSteelGrade + "'"); }
	}
	public abstract partial class IfcReinforcingElementType : IfcElementComponentType //IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcingBarType, IfcReinforcingMeshType, IfcTendonAnchorType, IfcTendonType))
	{
		protected IfcReinforcingElementType() : base() { }
		protected IfcReinforcingElementType(DatabaseIfc db) : base(db) { }
		protected IfcReinforcingElementType(DatabaseIfc db, IfcReinforcingElementType t) : base(db, t) { }
		protected static void parseFields(IfcReinforcingElementType t, List<string> arrFields, ref int ipos) { IfcElementComponentType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcReinforcingMesh : IfcReinforcingElement
	{
		internal double mMeshLength, mMeshWidth;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mLongitudinalBarNominalDiameter, mTransverseBarNominalDiameter, mLongitudinalBarCrossSectionArea;// : IfcPositiveLengthMeasure;
		internal double mTransverseBarCrossSectionArea;// : IfcAreaMeasure;
		internal double mLongitudinalBarSpacing, mTransverseBarSpacing;// : IfcPositiveLengthMeasure;
		internal IfcReinforcingMeshTypeEnum mPredefinedType = IfcReinforcingMeshTypeEnum.NOTDEFINED; //	:	OPTIONAL IfcReinforcingMeshTypeEnum;

		public IfcReinforcingMeshTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcReinforcingMesh() : base() { }
		internal IfcReinforcingMesh(DatabaseIfc db, IfcReinforcingMesh m) : base(db, m)
		{
			mMeshLength = m.mMeshLength;
			mMeshWidth = m.mMeshWidth;
			mLongitudinalBarNominalDiameter = m.mLongitudinalBarNominalDiameter;
			mTransverseBarNominalDiameter = m.mTransverseBarNominalDiameter;
			mLongitudinalBarCrossSectionArea = m.mLongitudinalBarCrossSectionArea;
			mTransverseBarCrossSectionArea = m.mTransverseBarCrossSectionArea;
			mLongitudinalBarSpacing = m.mLongitudinalBarSpacing;
			mTransverseBarSpacing = m.mTransverseBarSpacing;
			mPredefinedType = m.mPredefinedType;
		}
		public IfcReinforcingMesh(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static IfcReinforcingMesh Parse(string strDef, ReleaseVersion schema) { IfcReinforcingMesh m = new IfcReinforcingMesh(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return m; }
		internal static void parseFields(IfcReinforcingMesh c, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcReinforcingElement.parseFields(c, arrFields, ref ipos);
			c.mMeshLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mMeshWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mTransverseBarNominalDiameter = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mLongitudinalBarCrossSectionArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mTransverseBarCrossSectionArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mLongitudinalBarSpacing = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mTransverseBarSpacing = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					c.mPredefinedType = (IfcReinforcingMeshTypeEnum)Enum.Parse(typeof(IfcReinforcingMeshTypeEnum), str.Replace(".", ""));
			}
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mMeshLength) + "," + ParserSTEP.DoubleOptionalToString(mMeshWidth) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarNominalDiameter) + "," + ParserSTEP.DoubleToString(mTransverseBarNominalDiameter) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarCrossSectionArea) + "," + ParserSTEP.DoubleToString(mTransverseBarCrossSectionArea) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarSpacing) + "," + ParserSTEP.DoubleToString(mTransverseBarSpacing) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcReinforcingMeshTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + "."));
		}
	}
	//IfcReinforcingMeshType
	[Obsolete("DEPRECEATED IFC4", false)]
	public class IfcRelaxation : BaseClassIfc// DEPRECEATED IFC4
	{
		internal double mRelaxationValue;// : IfcNormalisedRatioMeasure;
		internal double mInitialStress;// : IfcNormalisedRatioMeasure; 

		public double RelaxationValue { get { return mRelaxationValue; } set { mRelaxationValue = value; } }
		public double InitialStress { get { return mInitialStress; } set { mInitialStress = value; } }

		internal IfcRelaxation() : base() { }
		internal IfcRelaxation(DatabaseIfc db, IfcRelaxation p) : base(db) { mRelaxationValue = p.mRelaxationValue; mInitialStress = p.mInitialStress; }
	 	internal static IfcRelaxation Parse(string strDef) { IfcRelaxation relaxation = new IfcRelaxation(); int ipos = 0;  parseFields(relaxation,ParserSTEP.SplitLineFields(strDef), ref ipos); return relaxation; }
		internal static void parseFields(IfcRelaxation relaxation, List<string> arrFields, ref int ipos)
		{
			relaxation.mRelaxationValue = ParserSTEP.ParseDouble(arrFields[ipos++]);
			relaxation.mInitialStress = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mRelaxationValue) + "," + ParserSTEP.DoubleToString(mInitialStress); }
	}
	public partial class IfcReparametrisedCompositeCurveSegment : IfcCompositeCurveSegment
	{
		private double mParamLength;// : IfcParameterValue

		public double ParamLength { get { return mParamLength; } set { mParamLength = value; } }

		internal IfcReparametrisedCompositeCurveSegment() : base() { }
		internal IfcReparametrisedCompositeCurveSegment(DatabaseIfc db, IfcReparametrisedCompositeCurveSegment s) : base(db, s) { mParamLength = s.mParamLength; }
		public IfcReparametrisedCompositeCurveSegment(IfcTransitionCode tc, bool sense, IfcBoundedCurve bc, double paramLength) : base(tc, sense,bc) { mParamLength = paramLength; }
		internal new static IfcReparametrisedCompositeCurveSegment Parse(string str) { IfcReparametrisedCompositeCurveSegment s = new IfcReparametrisedCompositeCurveSegment(); int pos = 0, len = str.Length; s.parseString(str, ref pos, len); return s; }
		protected override void parseString(string str, ref int pos, int len)
		{
			base.parseString(str, ref pos, len);
			mParamLength = ParserSTEP.StripDouble(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mParamLength.ToString(); }
	}
	public partial class IfcRepresentation : BaseClassIfc, IfcLayeredItem // Abstract IFC4 ,SUPERTYPE OF (ONEOF(IfcShapeModel,IfcStyleModel));
	{
		private int mContextOfItems;// : IfcRepresentationContext;
		internal string mRepresentationIdentifier = "$";//  : OPTIONAL IfcLabel; //RepresentationIdentifier: Name of the representation, e.g. 'Body' for 3D shape, 'FootPrint' for 2D ground view, 'Axis' for reference axis, 		
		internal string mRepresentationType = "$";//  : OPTIONAL IfcLabel;
		internal List<int> mItems = new List<int>();//  : SET [1:?] OF IfcRepresentationItem; 
		//INVERSE 
		internal IfcRepresentationMap mRepresentationMap = null;//	 : 	SET [0:1] OF IfcRepresentationMap FOR MappedRepresentation;
		internal List<IfcPresentationLayerAssignment> mLayerAssignments = new List<IfcPresentationLayerAssignment>();// new List<>();//	IFC4 change : 	SET OF IfcPresentationLayerAssignment FOR AssignedItems;
		internal List<IfcProductRepresentation> mOfProductRepresentation = new List<IfcProductRepresentation>();/// IFC4 change	 : 	SET [0:n] OF IfcProductRepresentation FOR Representations;

		public IfcRepresentationContext ContextOfItems
		{
			get { return mDatabase[mContextOfItems] as IfcRepresentationContext; }
			set
			{
				mContextOfItems = value.mIndex;
				value.mRepresentationsInContext.Add(this);
			}
		}
		public string RepresentationIdentifier { get { return mRepresentationIdentifier == "$" ? "" : mRepresentationIdentifier; } set { mRepresentationIdentifier = (string.IsNullOrEmpty(value) ? "$" : value); } }
		public string RepresentationType { get { return mRepresentationType == "$" ? "" : mRepresentationType; } set { mRepresentationType = string.IsNullOrEmpty(value) ? "$" : value; } }
		public ReadOnlyCollection<IfcRepresentationItem> Items { get { return new ReadOnlyCollection<IfcRepresentationItem>(mItems.ConvertAll(x => mDatabase[x] as IfcRepresentationItem)); } }
		public ReadOnlyCollection<IfcPresentationLayerAssignment> LayerAssignments { get { return new ReadOnlyCollection<IfcPresentationLayerAssignment>( mLayerAssignments); } }
		public ReadOnlyCollection<IfcProductRepresentation> OfProductRepresentation { get { return new ReadOnlyCollection<IfcProductRepresentation>( mOfProductRepresentation); } }

		protected IfcRepresentation() : base() { }
		protected IfcRepresentation(DatabaseIfc db, IfcRepresentation r) : base(db)
		{
			ContextOfItems = db.Factory.Duplicate(r.ContextOfItems) as IfcRepresentationContext;

			mRepresentationIdentifier = r.mRepresentationIdentifier;
			mRepresentationType = r.mRepresentationType;
			r.Items.ToList().ForEach(x => addItem( db.Factory.Duplicate(x) as IfcRepresentationItem));
		}
		protected IfcRepresentation(IfcRepresentationContext context, string identifier, string repType) : base(context.mDatabase)
		{
			ContextOfItems = context;
			RepresentationIdentifier = identifier;
			RepresentationType = repType;
		}
		protected IfcRepresentation(IfcRepresentationContext context, IfcRepresentationItem ri) : this(context,"","") { mItems.Add(ri.mIndex); ri.mRepresents.Add(this); }
		protected IfcRepresentation(IfcRepresentationContext context, IfcRepresentationItem ri, string identifier, string repType)
			: this(context, identifier, repType) { mItems.Add(ri.mIndex); ri.mRepresents.Add(this); }
		protected IfcRepresentation(IfcRepresentationContext context, List<IfcRepresentationItem> reps, string identifier, string repType)
			: this(context, identifier, repType)
		{
			for(int icounter = 0; icounter < reps.Count;icounter++)
			{
				IfcRepresentationItem ri = reps[icounter];
				mItems.Add(ri.mIndex);
				ri.mRepresents.Add(this);
			}
		}

		internal static IfcRepresentation Parse(string str) { IfcRepresentation r = new IfcRepresentation(); int pos = 0; r.parseString(str, ref pos, str.Length); return r; }
		internal void parseString(string str, ref int pos, int len)
		{
			mContextOfItems = ParserSTEP.StripLink(str, ref pos, len);
			mRepresentationIdentifier = ParserSTEP.StripString(str, ref pos, len);
			mRepresentationType = ParserSTEP.StripString(str, ref pos, len);
			mItems = ParserSTEP.StripListLink(str, ref pos, len);
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mContextOfItems) + (mRepresentationIdentifier == "$" ? ",$," : ",'" + mRepresentationIdentifier + "',") + (mRepresentationType == "$" ? "$,(" : "'" + mRepresentationType + "',(");
			if (mItems.Count > 0)
			{
				str += ParserSTEP.LinkToString(mItems[0]);
				for (int icounter = 1; icounter < mItems.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mItems[icounter]);
			}
			return str + ")";
		}

		internal void addItem(IfcRepresentationItem item) { mItems.Add(item.mIndex); item.mRepresents.Add(this); }
		internal void setItems(IEnumerable<IfcRepresentationItem> items) { mItems.Clear(); foreach (IfcRepresentationItem item in items) addItem(item);  }
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcRepresentationItem item in Items)
				result.AddRange(item.Extract<T>());
			return result;
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcRepresentationContext rc = ContextOfItems;
			if (rc != null)
				rc.mRepresentationsInContext.Add(this);
			foreach(IfcRepresentationItem item in Items)
			{
				if(item != null)
					item.mRepresents.Add(this);
			}
		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			foreach(IfcRepresentationItem item in Items)
				item.changeSchema(schema);
		}
		public void AssignLayer(IfcPresentationLayerAssignment layer) { mLayerAssignments.Add(layer); }
	}
	public abstract partial class IfcRepresentationContext : BaseClassIfc //ABSTRACT SUPERTYPE OF(IfcGeometricRepresentationContext);
	{
		internal string mContextIdentifier = "$";// : OPTIONAL IfcLabel;
		internal string mContextType = "$";// : OPTIONAL IfcLabel; 
		//INVERSE
		internal List<IfcRepresentation> mRepresentationsInContext = new List<IfcRepresentation>();// :	SET OF IfcRepresentation FOR ContextOfItems;

		public string ContextIdentifier { get { return (mContextIdentifier == "$" ? "" : ParserIfc.Decode(mContextIdentifier)); } set { mContextIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string ContextType { get { return (mContextType == "$" ? "" : ParserIfc.Decode(mContextType)); } set { mContextType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcRepresentation> RepresentationsInContext { get { return new ReadOnlyCollection<IfcRepresentation>( mRepresentationsInContext); } }

		protected bool mActive = true;
		internal bool Active { get { return mActive; } set { mActive = value; } }
		internal bool suggestActive()
		{
			if (string.Compare(mContextIdentifier, "Axis", true) == 0)
				return false;
			if (string.Compare(mContextIdentifier, "Box", true) == 0)
				return false;
			if (string.Compare(mContextIdentifier, "Annotation", true) == 0)
				return false;
			if (string.Compare(mContextType, "Plan", true) == 0)
				return false;
			return mActive;
		}

		protected IfcRepresentationContext() : base() { }
		protected IfcRepresentationContext(DatabaseIfc db) : base(db) { }
		protected IfcRepresentationContext(DatabaseIfc db, IfcRepresentationContext c) : base(db,c) { mContextIdentifier = c.mContextIdentifier; mContextType = c.mContextType; }
		internal static void parseFields(IfcRepresentationContext c, List<string> arrFields, ref int ipos) { c.mContextIdentifier = arrFields[ipos++].Replace("'", ""); c.mContextType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mContextIdentifier == "$" ? ",$," : ",'" + mContextIdentifier + "',") + (mContextType == "$" ? "$" : "'" + mContextType + "'"); }
	}
	public abstract partial class IfcRepresentationItem : BaseClassIfc, IfcLayeredItem /*(IfcGeometricRepresentationItem,IfcMappedItem,IfcStyledItem,IfcTopologicalRepresentationItem));*/
	{ //INVERSE
		internal List<IfcPresentationLayerAssignment> mLayerAssignments = new List<IfcPresentationLayerAssignment>();// null;
		internal IfcStyledItem mStyledByItem = null;// : SET [0:1] OF IfcStyledItem FOR Item; 

		internal List<IfcRepresentation> mRepresents = new List<IfcRepresentation>();

		public ReadOnlyCollection<IfcPresentationLayerAssignment> LayerAssignments { get { return new ReadOnlyCollection<IfcPresentationLayerAssignment>( mLayerAssignments); } }
		
		protected IfcRepresentationItem() : base() { }
		protected IfcRepresentationItem(DatabaseIfc db, IfcRepresentationItem i) : base(db, i)
		{
			if (i.mStyledByItem != null)
			{
				IfcStyledItem si = db.Factory.Duplicate(i.mStyledByItem) as IfcStyledItem;
				si.Item = this;
			}
		}
		protected IfcRepresentationItem(DatabaseIfc db) : base(db) { }

		public void AssignLayer(IfcPresentationLayerAssignment layer) { mLayerAssignments.Add(layer); }
	}
	public partial class IfcRepresentationMap : BaseClassIfc, IfcProductRepresentationSelect
	{
		internal int mMappingOrigin;// : IfcAxis2Placement;
		internal int mMappedRepresentation;// : IfcRepresentation;
		//INVERSE
		internal List<IfcShapeAspect> mHasShapeAspects = new List<IfcShapeAspect>();//	:	SET [0:?] OF IfcShapeAspect FOR PartOfProductDefinitionShape;
		internal List<IfcMappedItem> mMapUsage = new List<IfcMappedItem>();//: 	SET OF IfcMappedItem FOR MappingSource;

		public IfcAxis2Placement MappingOrigin { get { return mDatabase[mMappingOrigin] as IfcAxis2Placement; } set { mMappingOrigin = value.Index; } }
		public IfcRepresentation MappedRepresentation { get { return mDatabase[mMappedRepresentation] as IfcRepresentation; } set { mMappedRepresentation = value.mIndex; } }
		public ReadOnlyCollection<IfcShapeAspect> HasShapeAspects { get { return new ReadOnlyCollection<IfcShapeAspect>( mHasShapeAspects); } }
		public ReadOnlyCollection<IfcMappedItem> MapUsage { get { return new ReadOnlyCollection<IfcMappedItem>( mMapUsage); } }

		internal List<IfcTypeProduct> mRepresents = new List<IfcTypeProduct>();// GG

		internal IfcRepresentationMap() : base() { }
		internal IfcRepresentationMap(DatabaseIfc db, IfcRepresentationMap m) : base(db,m) { MappingOrigin = db.Factory.Duplicate(m.mDatabase[m.mMappingOrigin]) as IfcAxis2Placement; MappedRepresentation = db.Factory.Duplicate(m.MappedRepresentation) as IfcRepresentation; }
		public IfcRepresentationMap(IfcGeometricRepresentationItem item) : base(item.mDatabase) { MappingOrigin = item.mDatabase.Factory.XYPlanePlacement; MappedRepresentation = new IfcShapeRepresentation(new List<IfcRepresentationItem>() { item }); }
		public IfcRepresentationMap(IfcTopologicalRepresentationItem item) : base(item.mDatabase) { MappingOrigin = item.mDatabase.Factory.XYPlanePlacement; MappedRepresentation = new IfcShapeRepresentation(new List<IfcRepresentationItem>() { item }); }
		public IfcRepresentationMap(IfcAxis2Placement placement, IfcShapeRepresentation representation) : base(representation.mDatabase) { mMappingOrigin = placement.Index; mMappedRepresentation = representation.mIndex; }
		public IfcRepresentationMap(IfcAxis2Placement placement, IfcTopologyRepresentation representation) : base(representation.mDatabase) { mMappingOrigin = placement.Index; mMappedRepresentation = representation.mIndex; }

		internal static IfcRepresentationMap Parse(string strDef) { IfcRepresentationMap m = new IfcRepresentationMap(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcRepresentationMap rm, List<string> arrFields, ref int ipos) { rm.mMappingOrigin = ParserSTEP.ParseLink(arrFields[ipos++]); rm.mMappedRepresentation = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mMappingOrigin) + "," + ParserSTEP.LinkToString(mMappedRepresentation); }
		public void AddShapeAspect(IfcShapeAspect aspect) { mHasShapeAspects.Add(aspect); }

		internal override void changeSchema(ReleaseVersion schema)
		{
			MappedRepresentation.changeSchema(schema);
			foreach (IfcShapeAspect aspect in mHasShapeAspects)
			{
				IfcShapeAspect sa = mDatabase[aspect.mIndex] as IfcShapeAspect;
				if (sa != null)
					mDatabase[aspect.mIndex] = null;
			
			}  
			base.changeSchema(schema);
		}
	}
	public abstract partial class IfcResource : IfcObject //ABSTRACT SUPERTYPE OF (ONEOF (IfcConstructionResource))
	{	//INVERSE 
		internal List<IfcRelAssignsToResource> mResourceOf = new List<IfcRelAssignsToResource>();// : SET [0:?] OF IfcRelAssignsToResource FOR RelatingResource; 
		protected IfcResource() : base() { } 
		protected IfcResource(DatabaseIfc db, IfcResource r) : base(db,r,false) { }
		protected IfcResource(DatabaseIfc db) : base(db) { }
		protected static void parseFields(IfcResource r, List<string> arrFields, ref int ipos) { IfcObject.parseFields(r, arrFields, ref ipos); }
	}
	public partial class IfcResourceConstraintRelationship : IfcResourceLevelRelationship  // IfcPropertyConstraintRelationship; // DEPRECEATED IFC4 renamed
	{
		internal int mRelatingConstraint;// :	IfcConstraint;
		internal List<int> mRelatedResourceObjects = new List<int>();// :	SET [1:?] OF IfcResourceObjectSelect;

		public IfcConstraint RelatingConstraint { get { return mDatabase[mRelatingConstraint] as IfcConstraint; } set { mRelatingConstraint = value.mIndex; if (!value.mPropertiesForConstraint.Contains(this)) value.mPropertiesForConstraint.Add(this); } }
		public ReadOnlyCollection<IfcResourceObjectSelect> RelatedResourceObjects { get { return new ReadOnlyCollection<IfcResourceObjectSelect>( mRelatedResourceObjects.ConvertAll(x => mDatabase[x] as IfcResourceObjectSelect)); } }

		internal IfcResourceConstraintRelationship() : base() { }
		internal IfcResourceConstraintRelationship(DatabaseIfc db, IfcResourceConstraintRelationship r) : base(db,r) { RelatingConstraint = db.Factory.Duplicate(r.RelatingConstraint) as IfcConstraint; r.mRelatedResourceObjects.ForEach(x => addRelated( db.Factory.Duplicate(r.mDatabase[x]) as IfcResourceObjectSelect)); }
		public IfcResourceConstraintRelationship(IfcConstraint constraint, IfcResourceObjectSelect related) : this( constraint, new List<IfcResourceObjectSelect>() { related }) { }
		public IfcResourceConstraintRelationship(IfcConstraint constraint, List<IfcResourceObjectSelect> related) : base(constraint.mDatabase)
		{
			RelatingConstraint = constraint;
			related.ForEach(x => addRelated(x));
		}
		internal static IfcResourceConstraintRelationship Parse(string strDef, ReleaseVersion schema) { IfcResourceConstraintRelationship a = new IfcResourceConstraintRelationship(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcResourceConstraintRelationship a, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcResourceLevelRelationship.parseFields(a, arrFields, ref ipos,schema); a.mRelatingConstraint = ParserSTEP.ParseLink(arrFields[ipos++]); a.mRelatedResourceObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingConstraint) + ",(#" + mRelatedResourceObjects[0];
			for (int icounter = 1; icounter < mRelatedResourceObjects.Count; icounter++)
				result += ",#" + mRelatedResourceObjects[icounter];
			return result + ")";
		}
		internal void addRelated(IfcResourceObjectSelect r)
		{
			r.AddConstraintRelationShip(this);
			mRelatedResourceObjects.Add(r.Index);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingConstraint.mPropertiesForConstraint.Add(this);
			foreach(IfcResourceObjectSelect related in RelatedResourceObjects)
				related.AddConstraintRelationShip(this);
		}

		public override bool Destruct(bool children)
		{
			for (int icounter = 0; icounter < mRelatedResourceObjects.Count; icounter++)
			{
				BaseClassIfc bc = mDatabase[mRelatedResourceObjects[icounter]];
				if (bc != null)
					bc.Destruct(children);
			}
			return base.Destruct(children);
		}
	}
	public abstract partial class IfcResourceLevelRelationship : BaseClassIfc //IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcApprovalRelationship,
	{ // IfcCurrencyRelationship, IfcDocumentInformationRelationship, IfcExternalReferenceRelationship, IfcMaterialRelationship, IfcOrganizationRelationship, IfcPropertyDependencyRelationship, IfcResourceApprovalRelationship, IfcResourceConstraintRelationship));
		internal string mName = "$";// : OPTIONAL IfcLabel
		internal string mDescription = "$";// : OPTIONAL IfcText; 
		//INVERSE
		//mHasExternalReference

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcResourceLevelRelationship() : base() { }
		protected IfcResourceLevelRelationship(DatabaseIfc db, IfcResourceLevelRelationship r) : base(db,r) { mDescription = r.mDescription; mName = r.mName; }
		protected IfcResourceLevelRelationship(DatabaseIfc db) : base(db) { }
		internal static void parseFields(IfcResourceLevelRelationship a, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			if (schema != ReleaseVersion.IFC2x3)
			{
				a.mName = arrFields[ipos++].Replace("'", "");
				a.mDescription = arrFields[ipos++].Replace("'", "");
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'"); }
	}
	public partial interface IfcResourceObjectSelect : IBaseClassIfc //IFC4 SELECT (	IfcPropertyAbstraction, IfcPhysicalQuantity, IfcAppliedValue, 
	{   //IfcContextDependentUnit, IfcConversionBasedUnit, IfcProfileDef, IfcActorRole, IfcApproval, IfcConstraint, IfcTimeSeries, IfcMaterialDefinition, IfcPerson, IfcPersonAndOrganization, IfcOrganization, IfcExternalReference, IfcExternalInformation););
		void AddExternalReferenceRelationship(IfcExternalReferenceRelationship referenceRelationship);
		void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship);
	}
	public partial class IfcResourceTime : IfcSchedulingTime //IFC4
	{
		internal string mScheduleWork = "$";//	 :	OPTIONAL IfcDuration;
		internal double mScheduleUsage = double.NaN; //:	OPTIONAL IfcPositiveRatioMeasure;
		internal DateTime mScheduleStart = DateTime.MinValue, mScheduleFinish = DateTime.MinValue;//:	OPTIONAL IfcDateTime;
		internal string mScheduleContour = "$";//:	OPTIONAL IfcLabel;
		internal string mLevelingDelay = "$";//	 :	OPTIONAL IfcDuration;
		internal bool mIsOverAllocated = false;//	 :	OPTIONAL BOOLEAN;
		internal DateTime mStatusTime = DateTime.MinValue;//:	OPTIONAL IfcDateTime;
		internal string mActualWork = "$";//	 :	OPTIONAL IfcDuration; 
		internal double mActualUsage = double.NaN; //:	OPTIONAL IfcPositiveRatioMeasure; 
		internal DateTime mActualStart = DateTime.MinValue, mActualFinish = DateTime.MinValue;//	 :	OPTIONAL IfcDateTime;
		internal string mRemainingWork = "$";//	 :	OPTIONAL IfcDuration;
		internal double mRemainingUsage = double.NaN, mCompletion = double.NaN;//	 :	OPTIONAL IfcPositiveRatioMeasure; 

		public string ScheduleWork { get { return (mScheduleWork == "$" ? "" : ParserIfc.Decode(mScheduleWork)); } set { mScheduleWork = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public double ScheduleUsage { get { return mScheduleUsage; } set { mScheduleUsage = value; } }
		public DateTime ScheduleStart { get { return mScheduleStart; } set { mScheduleStart = value; } }
		public DateTime ScheduleFinish { get { return mScheduleFinish; } set { mScheduleFinish = value; } }
		public string ScheduleContour { get { return (mScheduleContour == "$" ? "" : ParserIfc.Decode(mScheduleContour)); } set { mScheduleContour = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcDuration LevelingDelay { get { return null; } set { mLevelingDelay = (value == null ? "$" : value.ToString()); } }
		public bool IsOverAllocated { get { return mIsOverAllocated; } set { mIsOverAllocated = value; } }
		public DateTime StatusTime { get { return mStatusTime; } set { mStatusTime = value; } }
		public IfcDuration ActualWork { get { return null; } set { mActualWork = (value == null ? "$" : value.ToString()); } }
		public double ActualUsage { get { return mActualUsage; } set { mActualUsage = value; } }
		public DateTime ActualStart { get { return mActualStart; } set { mActualStart = value; } }
		public DateTime ActualFinish { get { return mActualFinish; } set { mActualFinish = value; } }
		public IfcDuration RemainingWork { get { return null; } set { mRemainingWork = (value == null ? "$" : value.ToString()); } }
		public double RemainingUsage { get { return mRemainingUsage; } set { mRemainingUsage = value; } }
		public double Completion { get { return mCompletion; } set { mCompletion = value; } }

		internal IfcResourceTime() : base() { }
		internal IfcResourceTime(DatabaseIfc db, IfcResourceTime t) : base(db,t)
		{
			mScheduleWork = t.mScheduleWork; mScheduleUsage = t.mScheduleUsage; mScheduleStart = t.mScheduleStart; mScheduleFinish = t.mScheduleFinish; mScheduleContour = t.mScheduleContour;
			mLevelingDelay = t.mLevelingDelay; mIsOverAllocated = t.mIsOverAllocated; mStatusTime = t.mStatusTime; mActualWork = t.mActualWork; mActualUsage = t.mActualUsage;
			mActualStart = t.mActualStart; mActualFinish = t.mActualFinish; mRemainingWork = t.mRemainingWork; mRemainingUsage = t.mRemainingUsage; mCompletion = t.mCompletion;
		}
		internal IfcResourceTime(DatabaseIfc db) : base(db) { }
		
		internal static IfcResourceTime Parse(string strDef) { IfcResourceTime s = new IfcResourceTime(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcResourceTime s, List<string> arrFields, ref int ipos)
		{
			IfcSchedulingTime.parseFields(s, arrFields, ref ipos);
			s.mRemainingWork = arrFields[ipos++].Replace("'", "");
			s.mScheduleUsage = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mScheduleStart = IfcDateTime.parseSTEP(arrFields[ipos++]);
			s.mScheduleFinish = IfcDateTime.parseSTEP(arrFields[ipos++]);
			s.mScheduleContour = arrFields[ipos++].Replace("'", "");
			s.mLevelingDelay = arrFields[ipos++].Replace("'", "");
			s.mIsOverAllocated = ParserSTEP.ParseBool(arrFields[ipos++]);
			s.mStatusTime = IfcDateTime.parseSTEP(arrFields[ipos++]);
			s.mActualWork = arrFields[ipos++].Replace("'", "");
			s.mActualUsage = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mActualStart = IfcDateTime.parseSTEP(arrFields[ipos++]);
			s.mActualFinish = IfcDateTime.parseSTEP(arrFields[ipos++]);
			s.mRemainingWork = arrFields[ipos++].Replace("'", "");
			s.mRemainingUsage = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mCompletion = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mScheduleWork == "$" ? ",$," : ",'" + mScheduleWork + "',") + ParserSTEP.DoubleOptionalToString(mScheduleUsage) + "," + IfcDateTime.formatSTEP(mScheduleStart) + "," +
				IfcDateTime.formatSTEP(mScheduleFinish) + (mScheduleContour == "$" ? ",$," : ",'" + mScheduleContour + "',") + (mLevelingDelay == "$" ? "$," : "'" + mLevelingDelay + "',") + ParserSTEP.BoolToString(mIsOverAllocated) + "," +
				IfcDateTime.formatSTEP(mStatusTime) + "," + (mActualWork == "$" ? "$," : "'" + mActualWork + "',") + ParserSTEP.DoubleOptionalToString(mActualUsage) + "," + IfcDateTime.formatSTEP(mActualStart) + "," +
				IfcDateTime.formatSTEP(mActualFinish) + (mRemainingWork == "$" ? ",$," : ",'" + mRemainingWork + "',") + ParserSTEP.DoubleOptionalToString(mRemainingUsage) + "," + ParserSTEP.DoubleOptionalToString(mCompletion);
		}
	}
	public partial class IfcRevolvedAreaSolid : IfcSweptAreaSolid // SUPERTYPE OF(IfcRevolvedAreaSolidTapered)
	{
		internal int mAxis;//: IfcAxis1Placement
		internal double mAngle;// : IfcPlaneAngleMeasure;

		public IfcAxis1Placement Axis { get { return mDatabase[mAxis] as IfcAxis1Placement; } set { mAxis = value.mIndex; } }
		public double Angle { get { return mAngle; } set { mAngle = value; } }

		internal IfcRevolvedAreaSolid() : base() { }
		internal IfcRevolvedAreaSolid(DatabaseIfc db, IfcRevolvedAreaSolid s) : base(db,s) { Axis = db.Factory.Duplicate(s.Axis) as IfcAxis1Placement; mAngle = s.mAngle; }
		public IfcRevolvedAreaSolid(IfcProfileDef profile, IfcAxis2Placement3D position, IfcAxis1Placement axis, double angle) : base(profile, position) { Axis = axis; mAngle = angle; }
		internal static IfcRevolvedAreaSolid Parse(string str) { IfcRevolvedAreaSolid r = new IfcRevolvedAreaSolid(); int pos = 0; r.Parse(str, ref pos, str.Length); return r; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mAxis = ParserSTEP.StripLink(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("IfcPlaneAngleMeasure(", true, System.Globalization.CultureInfo.CurrentCulture))
				mAngle = ParserSTEP.ParseDouble(s.Substring(21, str.Length - 22));
			else
				mAngle = ParserSTEP.ParseDouble(s);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mAxis) + "," + ParserSTEP.DoubleToString(mAngle); }
	}
	//ENTITY IfcRevolvedAreaSolidTapered
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcRibPlateProfileProperties : IfcProfileProperties // DEPRECEATED IFC4
	{ 
		internal double mThickness, mRibHeight, mRibWidth, mRibSpacing;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcRibPlateDirectionEnum mDirection;// : IfcRibPlateDirectionEnum;*/
		internal IfcRibPlateProfileProperties() : base() { }
		internal IfcRibPlateProfileProperties(DatabaseIfc db, IfcRibPlateProfileProperties p) : base(db, p)
		{
			mThickness = p.mThickness;
			mRibHeight = p.mRibHeight;
			mRibWidth = p.mRibWidth;
			mRibSpacing = p.mRibSpacing;
			mDirection = p.mDirection;
		}
		internal new static IfcRibPlateProfileProperties Parse(string strDef,ReleaseVersion schema) { IfcRibPlateProfileProperties p = new IfcRibPlateProfileProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcRibPlateProfileProperties p, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcProfileProperties.parseFields(p, arrFields, ref ipos,schema);
			p.mThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mRibHeight = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mRibWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mRibSpacing = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mDirection = (IfcRibPlateDirectionEnum)Enum.Parse(typeof(IfcRibPlateDirectionEnum), arrFields[ipos++].Replace(".", ""));
		}
	}
	public partial class IfcRightCircularCone : IfcCsgPrimitive3D
	{
		internal double mHeight, mBottomRadius;// : IfcPositiveLengthMeasure;
		internal IfcRightCircularCone() : base() { }
		internal IfcRightCircularCone(DatabaseIfc db, IfcRightCircularCone c) : base(db,c) { mHeight = c.mHeight; mBottomRadius = c.mBottomRadius; }
		internal static IfcRightCircularCone Parse(string str) { IfcRightCircularCone c = new IfcRightCircularCone(); int pos = 0; c.Parse(str, ref pos, str.Length); return c; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mBottomRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mHeight) + "," + ParserSTEP.DoubleToString(mBottomRadius); }
	}
	public partial class IfcRightCircularCylinder : IfcCsgPrimitive3D
	{
		internal double mHeight, mRadius;// : IfcPositiveLengthMeasure;
		internal IfcRightCircularCylinder() : base() { }
		internal IfcRightCircularCylinder(DatabaseIfc db, IfcRightCircularCylinder c) : base(db,c) { mHeight = c.mHeight; mRadius = c.mRadius; }

		internal static IfcRightCircularCylinder Parse(string str) { IfcRightCircularCylinder c = new IfcRightCircularCylinder(); int pos = 0; c.Parse(str, ref pos, str.Length); return c; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mHeight) + "," + ParserSTEP.DoubleToString(mRadius); }
	}
	public partial class IfcRoof : IfcBuildingElement
	{
		internal IfcRoofTypeEnum mPredefinedType = IfcRoofTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcRoofTypeEnum PredefinedType { get { return mPredefinedType; } }

		internal IfcRoof() : base() { }
		internal IfcRoof(DatabaseIfc db, IfcRoof r, bool downStream) : base(db,r, downStream) { mPredefinedType = r.mPredefinedType; }
		public IfcRoof(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcRoof Parse(string str) { IfcRoof r = new IfcRoof(); int pos = 0; r.Parse(str, ref pos, str.Length); return r; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRoofTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcRoofType : IfcBuildingElementType //IFC4
	{
		internal IfcRoofTypeEnum mPredefinedType = IfcRoofTypeEnum.NOTDEFINED;
		public IfcRoofTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRoofType() : base() { }
		internal IfcRoofType(DatabaseIfc db, IfcRoofType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcRoofType(DatabaseIfc m, string name, IfcRoofTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcRoofType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcRoofTypeEnum)Enum.Parse(typeof(IfcRoofTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcRoofType Parse(string strDef) { IfcRoofType t = new IfcRoofType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract partial class IfcRoot : BaseClassIfc//ABSTRACT SUPERTYPE OF (ONEOF (IfcObjectDefinition ,IfcPropertyDefinition ,IfcRelationship));
	{
		private string mGlobalId; // :	IfcGloballyUniqueId;
		private int mOwnerHistory;// : IfcOwnerHistory  IFC4  OPTIONAL;
		private string mName = "$"; //: OPTIONAL IfcLabel;
		private string mDescription = "$"; //: OPTIONAL IfcText; 

		public string GlobalId
		{
			get { return mGlobalId; }
			set
			{
				if (ParserIfc.DecodeGlobalID(value) != Guid.Empty && !mDatabase.mGlobalIDs.Contains(value))
				{
					mDatabase.mGlobalIDs.Remove(mGlobalId);
					mGlobalId = value;
					mDatabase.mGlobalIDs.Add(value);
				}
			}
		}
		public Guid Guid
		{
			get { return ParserIfc.DecodeGlobalID(mGlobalId); }
			set
			{
				string id = ParserIfc.EncodeGuid(value == Guid.Empty ? Guid.NewGuid() : value);
				if (!mDatabase.mGlobalIDs.Contains(id))
				{
					mDatabase.mGlobalIDs.Remove(mGlobalId);
					GlobalId = id;
					mDatabase.mGlobalIDs.Add(id);
				}
			}
		}
		public IfcOwnerHistory OwnerHistory
		{
			get { return mDatabase[mOwnerHistory] as IfcOwnerHistory; }
			set { mOwnerHistory = (value == null ? 0 : value.mIndex); }
		}
		public override string Name
		{
			get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); }
			set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); }
		} 
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcRoot() : base() { mGlobalId = ParserIfc.EncodeGuid(Guid.NewGuid()); }
		protected IfcRoot(IfcRoot basis) : base(basis) { GlobalId = basis.mGlobalId; mOwnerHistory = basis.mOwnerHistory; mName = basis.mName; mDescription = basis.mDescription; }
		protected IfcRoot(DatabaseIfc db) : base(db)
		{
			mGlobalId = ParserIfc.EncodeGuid(Guid.NewGuid());
			//m.mGlobalIDs.Add(mGlobalId);
			if (db.Release == ReleaseVersion.IFC2x3 || (db.mModelView != ModelView.Ifc4Reference && db.Factory.Options.GenerateOwnerHistory))
				OwnerHistory = db.Factory.OwnerHistory(IfcChangeActionEnum.ADDED);
		}
		protected IfcRoot(DatabaseIfc db, IfcRoot r) : base(db,r)
		{
			GlobalId = r.GlobalId;
			if (r.mOwnerHistory > 0)
				OwnerHistory = db.Factory.Duplicate(r.OwnerHistory) as IfcOwnerHistory;
			mName = r.mName;
			mDescription = r.mDescription;
		}
		protected static void parseFields(IfcRoot root, List<string> arrFields, ref int ipos)
		{
			root.mGlobalId = ParserSTEP.ParseString(arrFields[ipos++]);
			root.mOwnerHistory = ParserSTEP.ParseLink(arrFields[ipos++]);
			root.Name = ParserSTEP.ParseString(arrFields[ipos++]);
			root.Description = ParserSTEP.ParseString(arrFields[ipos++]);
		}
		protected virtual void Parse(string str, ref int pos, int len)
		{
			mGlobalId = ParserSTEP.StripString(str, ref pos, len);
			mOwnerHistory = ParserSTEP.StripLink(str, ref pos, len);
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mGlobalId + (mOwnerHistory == 0 ? "',$" : "',#" + mOwnerHistory) + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'"); }

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			if (schema == ReleaseVersion.IFC2x3)
			{
				if (mOwnerHistory == 0)
					OwnerHistory = mDatabase.Factory.OwnerHistory(IfcChangeActionEnum.NOCHANGE);
			}
		}
	}
	public partial class IfcRotationalStiffnessSelect
	{
		internal bool mRigid = false;
		internal IfcRotationalStiffnessMeasure mStiffness = null;

		public bool Rigid { get { return mRigid; } }
		public IfcRotationalStiffnessMeasure Stiffness { get { return mStiffness; } }

		internal IfcRotationalStiffnessSelect(IfcRotationalStiffnessMeasure stiff) { mStiffness = stiff; }
		public IfcRotationalStiffnessSelect(bool fix) { mRigid = fix; }
		public IfcRotationalStiffnessSelect(double stiff) { mStiffness = new IfcRotationalStiffnessMeasure(stiff); }
		internal static IfcRotationalStiffnessSelect Parse(string str, ReleaseVersion version)
		{
			if (str.StartsWith("IFCBOOL"))
				return new IfcRotationalStiffnessSelect(((IfcBoolean)ParserIfc.parseSimpleValue(str)).mValue);
			if (str.StartsWith("IFCROT"))
				return new IfcRotationalStiffnessSelect((IfcRotationalStiffnessMeasure)ParserIfc.parseDerivedMeasureValue(str));
			if (str.StartsWith("."))
				return new IfcRotationalStiffnessSelect(ParserSTEP.ParseBool(str));
			double d = ParserSTEP.ParseDouble(str), tol = 1e-9;
			if (version == ReleaseVersion.IFC2x3)
			{
				if (Math.Abs(d + 1) < tol)
					return new IfcRotationalStiffnessSelect(true) { mStiffness = new IfcRotationalStiffnessMeasure(-1) };
				if (Math.Abs(d) < tol)
					return new IfcRotationalStiffnessSelect(false) { mStiffness = new IfcRotationalStiffnessMeasure(0) };
			}
			return new IfcRotationalStiffnessSelect(new IfcRotationalStiffnessMeasure(d));
		}
		public override string ToString() { return (mStiffness == null ? "IFCBOOLEAN(" + ParserSTEP.BoolToString(mRigid) + ")" : mStiffness.ToString()); }
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRoundedEdgeFeature // DEPRECEATED IFC4
	public partial class IfcRoundedRectangleProfileDef : IfcRectangleProfileDef
	{
		internal double mRoundingRadius;// : IfcPositiveLengthMeasure; 
		public double RoundingRadius { get { return mRoundingRadius; } set { mRoundingRadius = value; } }
		internal IfcRoundedRectangleProfileDef() : base() { }
		internal IfcRoundedRectangleProfileDef(DatabaseIfc db, IfcRoundedRectangleProfileDef p) : base(db, p) { mRoundingRadius = p.mRoundingRadius; }
		public IfcRoundedRectangleProfileDef(DatabaseIfc db, string name, double xDim, double yDim, double roundingRadius) : base(db, name, xDim, yDim) { mRoundingRadius = roundingRadius; }
		internal static void parseFields(IfcRoundedRectangleProfileDef p, List<string> arrFields, ref int ipos) { IfcRectangleProfileDef.parseFields(p, arrFields, ref ipos); p.mRoundingRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal new static IfcRoundedRectangleProfileDef Parse(string strDef) { IfcRoundedRectangleProfileDef p = new IfcRoundedRectangleProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mRoundingRadius); }
	}
}
