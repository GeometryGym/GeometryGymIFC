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

namespace GeometryGym.Ifc
{
	public class IfcRadiusDimension : IfcDimensionCurveDirectedCallout // DEPRECEATED IFC4
	{
		internal IfcRadiusDimension() : base() { }
		internal IfcRadiusDimension(IfcRadiusDimension d) : base((IfcDimensionCurveDirectedCallout)d) { }
		internal new static IfcRadiusDimension Parse(string strDef) { IfcRadiusDimension d = new IfcRadiusDimension(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		internal static void parseFields(IfcRadiusDimension d, List<string> arrFields, ref int ipos) { IfcDimensionCurveDirectedCallout.parseFields(d, arrFields, ref ipos); }
	}
	public partial class IfcRailing : IfcBuildingElement
	{
		internal IfcRailingTypeEnum mPredefinedType = IfcRailingTypeEnum.NOTDEFINED;// : OPTIONAL IfcRailingTypeEnum
		public IfcRailingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRailing() : base() { }
		internal IfcRailing(IfcRailing r) : base(r) { mPredefinedType = r.mPredefinedType; }
		public IfcRailing(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }

		internal static IfcRailing Parse(string strDef) { IfcRailing r = new IfcRailing(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
		internal static void parseFields(IfcRailing r, List<string> arrFields, ref int ipos)
		{
			IfcBuildingElement.parseFields(r, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str != "$")
				r.mPredefinedType = (IfcRailingTypeEnum)Enum.Parse(typeof(IfcRailingTypeEnum), str.Replace(".", ""));
		}
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcRailingType : IfcBuildingElementType
	{
		internal IfcRailingTypeEnum mPredefinedType = IfcRailingTypeEnum.NOTDEFINED;
		public IfcRailingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcRailingType() : base() { }
		internal IfcRailingType(IfcRailingType b) : base(b) { mPredefinedType = b.mPredefinedType; }
		public IfcRailingType(DatabaseIfc m, string name, IfcRailingTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcRailingType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcRailingTypeEnum)Enum.Parse(typeof(IfcRailingTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcRailingType Parse(string strDef) { IfcRailingType t = new IfcRailingType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcRamp : IfcBuildingElement
	{
		internal IfcRampTypeEnum mPredefinedType = IfcRampTypeEnum.NOTDEFINED;// OPTIONAL : IfcRampTypeEnum
		public IfcRampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRamp() : base() { }
		internal IfcRamp(IfcRamp r) : base(r) { mPredefinedType = r.mPredefinedType; }
		public IfcRamp(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }

		internal static IfcRamp Parse(string strDef) { IfcRamp r = new IfcRamp(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
		internal static void parseFields(IfcRamp r, List<string> arrFields, ref int ipos)
		{
			IfcBuildingElement.parseFields(r, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				r.mPredefinedType = (IfcRampTypeEnum)Enum.Parse(typeof(IfcRampTypeEnum), str.Substring(1, str.Length - 2));
		}
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcRampFlight : IfcBuildingElement
	{
		internal IfcRampFlightTypeEnum mPredefinedType = IfcRampFlightTypeEnum.NOTDEFINED;// OPTIONAL : IfcRampTypeEnum
		public IfcRampFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRampFlight() : base() { }
		internal IfcRampFlight(IfcRampFlight f) : base(f) { mPredefinedType = f.mPredefinedType; }
		public IfcRampFlight(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }

		internal static IfcRampFlight Parse(string strDef, Schema schema) { IfcRampFlight f = new IfcRampFlight(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return f; }
		internal static void parseFields(IfcRampFlight f, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcBuildingElement.parseFields(f, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					f.mPredefinedType = (IfcRampFlightTypeEnum)Enum.Parse(typeof(IfcRampFlightTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? base.BuildString() : base.BuildString() + ",." + mPredefinedType.ToString() + "."); }
	}
	public class IfcRampFlightType : IfcBuildingElementType
	{
		internal IfcRampFlightTypeEnum mPredefinedType = IfcRampFlightTypeEnum.NOTDEFINED;
		public IfcRampFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcRampFlightType() : base() { }
		internal IfcRampFlightType(IfcRampFlightType b) : base(b) { mPredefinedType = b.mPredefinedType; }
		public IfcRampFlightType(DatabaseIfc m, string name, IfcRampFlightTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcRampFlightType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcRampFlightTypeEnum)Enum.Parse(typeof(IfcRampFlightTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcRampFlightType Parse(string strDef) { IfcRampFlightType t = new IfcRampFlightType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcRampType : IfcBuildingElementType //IFC4
	{
		internal IfcRampTypeEnum mPredefinedType = IfcRampTypeEnum.NOTDEFINED;
		public IfcRampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcRampType() : base() { }
		internal IfcRampType(IfcRampType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcRampType(DatabaseIfc m, string name, IfcRampTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcRampType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcRampTypeEnum)Enum.Parse(typeof(IfcRampTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcRampType Parse(string strDef) { IfcRampType t = new IfcRampType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcRationalBezierCurve : IfcBezierCurve // DEPRECEATED IFC4
	{
		internal List<double> mWeightsData = new List<double>();// : LIST [2:?] OF REAL;	
		internal IfcRationalBezierCurve() : base() { }
		internal IfcRationalBezierCurve(IfcRationalBezierCurve pl) : base(pl) { mWeightsData = new List<double>(pl.mWeightsData.ToArray()); }
		internal static void parseFields(IfcRationalBezierCurve c, List<string> arrFields, ref int ipos)
		{
			IfcBezierCurve.parseFields(c, arrFields, ref ipos);
			string s = arrFields[ipos++];
			List<string> arrNodes = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			for (int icounter = 0; icounter < arrNodes.Count; icounter++)
				c.mWeightsData.Add(ParserSTEP.ParseDouble(arrNodes[icounter]));
		}
		internal new static IfcRationalBezierCurve Parse(string strDef) { IfcRationalBezierCurve c = new IfcRationalBezierCurve(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.DoubleToString(mWeightsData[0]);
			for (int icounter = 1; icounter < mWeightsData.Count; icounter++)
				str += "," + ParserSTEP.DoubleToString(mWeightsData[icounter]);
			return str + ")";
		}
	}
	public partial class IfcRationalBSplineCurveWithKnots : IfcBSplineCurveWithKnots
	{
		internal List<double> mWeightsData = new List<double>();// : LIST [2:?] OF REAL;	
		internal IfcRationalBSplineCurveWithKnots() : base() { }
		internal IfcRationalBSplineCurveWithKnots(IfcRationalBSplineCurveWithKnots c) : base(c) { mWeightsData = new List<double>(c.mWeightsData.ToArray()); }

		internal static void parseFields(IfcRationalBSplineCurveWithKnots c, List<string> arrFields, ref int ipos)
		{
			IfcBSplineCurveWithKnots.parseFields(c, arrFields, ref ipos);
			string s = arrFields[ipos++];
			List<string> arrNodes = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			for (int icounter = 0; icounter < arrNodes.Count; icounter++)
				c.mWeightsData.Add(ParserSTEP.ParseDouble(arrNodes[icounter]));
		}
		internal new static IfcRationalBSplineCurveWithKnots Parse(string strDef) { IfcRationalBSplineCurveWithKnots c = new IfcRationalBSplineCurveWithKnots(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.DoubleToString(mWeightsData[0]);
			for (int icounter = 1; icounter < mWeightsData.Count; icounter++)
				str += "," + ParserSTEP.DoubleToString(mWeightsData[icounter]);
			return str + ")";
		}
	}
	public partial class IfcRationalBSplineSurfaceWithKnots : IfcBSplineSurfaceWithKnots
	{
		internal List<List<double>> mWeightsData = new List<List<double>>();// : LIST [2:?] OF REAL;	
		internal IfcRationalBSplineSurfaceWithKnots() : base() { }
		internal IfcRationalBSplineSurfaceWithKnots(IfcRationalBSplineSurfaceWithKnots s) : base(s)
		{
			for (int icounter = 0; icounter < s.mWeightsData.Count; icounter++)
				mWeightsData.Add(new List<double>(s.mWeightsData[icounter].ToArray()));
		}
		internal static void parseFields(IfcRationalBSplineSurfaceWithKnots s, List<string> arrFields, ref int ipos)
		{
			IfcBSplineSurfaceWithKnots.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			List<string> arrLists = ParserSTEP.SplitLineFields(str.Substring(1, str.Length - 2));
			int ilast = arrLists.Count;
			for (int icounter = 0; icounter < ilast; icounter++)
			{
				List<double> weights = new List<double>();
				List<string> arrweights = ParserSTEP.SplitLineFields(arrLists[icounter].Substring(1, arrLists[icounter].Length - 2));
				for (int jcounter = 0; jcounter < arrweights.Count; jcounter++)
					weights.Add(ParserSTEP.ParseDouble(arrweights[jcounter]));
				s.mWeightsData.Add(weights);
			}
		}
		internal new static IfcRationalBSplineSurfaceWithKnots Parse(string strDef) { IfcRationalBSplineSurfaceWithKnots s = new IfcRationalBSplineSurfaceWithKnots(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			List<double> wts = mWeightsData[0];
			string str = base.BuildString() + ",((" +
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
		internal double mInnerFilletRadius;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mOuterFilletRadius;// : OPTIONAL IfcPositiveLengthMeasure; 
		internal IfcRectangleHollowProfileDef() : base() { }
		internal IfcRectangleHollowProfileDef(IfcRectangleHollowProfileDef c) : base(c) { mWallThickness = c.mWallThickness; mInnerFilletRadius = c.mInnerFilletRadius; mOuterFilletRadius = c.mOuterFilletRadius; }
		public IfcRectangleHollowProfileDef(DatabaseIfc m, string name, double depth, double width, double wallThickness, double outerFilletRadius, double innerFilletRadius)
			: base(m, name, depth, width) { mWallThickness = wallThickness; mOuterFilletRadius = outerFilletRadius; mInnerFilletRadius = innerFilletRadius; }
		internal static void parseFields(IfcRectangleHollowProfileDef p, List<string> arrFields, ref int ipos) { IfcRectangleProfileDef.parseFields(p, arrFields, ref ipos); p.mWallThickness = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mInnerFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mOuterFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mWallThickness) + "," + ParserSTEP.DoubleToString(mInnerFilletRadius) + "," + ParserSTEP.DoubleToString(mOuterFilletRadius); }
		internal new static IfcRectangleHollowProfileDef Parse(string strDef) { IfcRectangleHollowProfileDef p = new IfcRectangleHollowProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
	}
	public partial class IfcRectangleProfileDef : IfcParameterizedProfileDef
	{
		internal double mXDim, mYDim;// : IfcPositiveLengthMeasure; 
		internal IfcRectangleProfileDef() : base() { }
		internal IfcRectangleProfileDef(IfcRectangleProfileDef p) : base(p) { mXDim = p.mXDim; mYDim = p.mYDim; }
		public IfcRectangleProfileDef(DatabaseIfc m, string name, double depth, double width) : base(m) { Name = name; mXDim = width; mYDim = depth; }
		internal static void parseFields(IfcRectangleProfileDef p, List<string> arrFields, ref int ipos) { IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos); p.mXDim = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mYDim = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mXDim) + "," + ParserSTEP.DoubleToString(mYDim); }
		internal new static IfcRectangleProfileDef Parse(string strDef) { IfcRectangleProfileDef p = new IfcRectangleProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
	}
	public partial class IfcRectangularPyramid : IfcCsgPrimitive3D
	{
		internal double mXLength, mYLength, mHeight;// : IfcPositiveLengthMeasure;
		internal IfcRectangularPyramid() : base() { }
		internal IfcRectangularPyramid(IfcRectangularPyramid pl) : base(pl) { mXLength = pl.mXLength; mYLength = pl.mYLength; mHeight = pl.mHeight; }
		internal static void parseFields(IfcRectangularPyramid p, List<string> arrFields, ref int ipos) { IfcCsgPrimitive3D.parseFields(p, arrFields, ref ipos); p.mXLength = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mYLength = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mHeight = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcRectangularPyramid Parse(string strDef) { IfcRectangularPyramid p = new IfcRectangularPyramid(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mXLength) + "," + ParserSTEP.DoubleToString(mYLength) + "," + ParserSTEP.DoubleToString(mHeight); }
	}
	public class IfcRectangularTrimmedSurface : IfcBoundedSurface
	{
		internal int mBasisSurface;// : IfcPlane;
		internal double mU1, mV1, mU2, mV2;// : IfcParameterValue;
		internal bool mUsense, mVsense;// : BOOLEAN; 
		internal IfcRectangularTrimmedSurface() : base() { }
		internal IfcRectangularTrimmedSurface(IfcRectangularTrimmedSurface s) : base(s)
		{
			mBasisSurface = s.mBasisSurface;
			mU1 = s.mU1;
			mU2 = s.mU2;
			mV1 = s.mV1;
			mV2 = s.mV2;
			mUsense = s.mUsense;
			mVsense = s.mVsense;
		}
		internal static void parseFields(IfcRectangularTrimmedSurface s, List<string> arrFields, ref int ipos)
		{
			IfcBoundedSurface.parseFields(s, arrFields, ref ipos);
			s.mBasisSurface = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mU1 = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mU2 = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mV1 = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mV2 = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mUsense = ParserSTEP.ParseBool(arrFields[ipos++]);
			s.mVsense = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		internal static IfcRectangularTrimmedSurface Parse(string strDef) { IfcRectangularTrimmedSurface s = new IfcRectangularTrimmedSurface(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.DoubleToString(mU1) + "," + ParserSTEP.DoubleToString(mV1) + "," + ParserSTEP.DoubleToString(mU2) + "," + ParserSTEP.DoubleToString(mV2) + "," + ParserSTEP.BoolToString(mUsense) + "," + ParserSTEP.BoolToString(mVsense); }
	}
	public class IfcRecurrencePattern : BaseClassIfc // IFC4
	{
		internal IfcRecurrenceTypeEnum mRecurrenceType = IfcRecurrenceTypeEnum.WEEKLY; //:	IfcRecurrenceTypeEnum;
		internal List<int> mDayComponent = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcDayInMonthNumber;
		internal List<int> mWeekdayComponent = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcDayInWeekNumber;
		internal List<int> mMonthComponent = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcMonthInYearNumber;
		internal int mPosition = 0;//	 :	OPTIONAL IfcInteger;
		internal int mInterval = 0;//	 :	OPTIONAL IfcInteger;
		internal int mOccurrences = 0;//	 :	OPTIONAL IfcInteger;
		internal List<int> mTimePeriods = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcTimePeriod;
		internal IfcRecurrencePattern() : base() { }
		internal IfcRecurrencePattern(IfcRecurrencePattern p) : base()
		{
			mRecurrenceType = p.mRecurrenceType;
			mDayComponent.AddRange(p.mDayComponent);
			mWeekdayComponent.AddRange(p.mWeekdayComponent);
			mMonthComponent.AddRange(p.mMonthComponent);
			mPosition = p.mPosition;
			mInterval = p.mInterval;
			mOccurrences = p.mOccurrences;
			mTimePeriods.AddRange(p.mTimePeriods);
		}
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
		protected override string BuildString()
		{
			string str = base.BuildString() + ",." + mRecurrenceType.ToString();
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
		internal string mTypeIdentifier = "$"; //:		OPTIONAL IfcIdentifier;
		internal string mAttributeIdentifier = "$"; //:		OPTIONAL IfcIdentifier;
		internal string mInstanceName = "$"; //:		OPTIONAL IfcLabel;
		internal List<int> mListPositions = new List<int>();//	 :	OPTIONAL LIST [1:?] OF INTEGER;
		private int mInnerReference = 0;//	 :	OPTIONAL IfcReference;

		public string TypeIdentifier { get { return (mTypeIdentifier == "$" ? "" : ParserIfc.Decode(mTypeIdentifier)); } set { mTypeIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string AttributeIdentifier { get { return (mAttributeIdentifier == "$" ? "" : ParserIfc.Decode(mAttributeIdentifier)); } set { mAttributeIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string InstanceName { get { return (mInstanceName == "$" ? "" : ParserIfc.Decode(mInstanceName)); } set { mInstanceName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public List<int> ListPositions { get { return mListPositions; } set { mListPositions = (value == null ? new List<int>() : value); } }
		public IfcReference InnerReference { get { return mDatabase.mIfcObjects[mInnerReference] as IfcReference; } set { mInnerReference = (value == null ? 0 : value.mIndex); } }

		internal IfcReference() : base() { }
		internal IfcReference(IfcReference r) : base()
		{
			mTypeIdentifier = r.mTypeIdentifier;
			mAttributeIdentifier = r.mAttributeIdentifier;
			mInstanceName = r.mInstanceName;
			mListPositions.AddRange(r.mListPositions);
			mInnerReference = r.mInnerReference;
		}
		public IfcReference(DatabaseIfc db, string typeId, string attributeId, string instanceName) : base(db)
		{
			TypeIdentifier = typeId;
			AttributeIdentifier = attributeId;
			InstanceName = instanceName;
		}
		public IfcReference(DatabaseIfc db, string typeId, string attributeId, string instanceName, IfcReference inner)
			: this(db,typeId,attributeId,instanceName) { InnerReference = inner; }
		public IfcReference(DatabaseIfc db, string typeId, string attributeId, string instanceName, int position, IfcReference inner)
			: this(db, typeId, attributeId, instanceName, inner) { mListPositions.Add(position); }
		public IfcReference(DatabaseIfc db, string typeId, string attributeId, string instanceName, List<int> positions, IfcReference inner)
			: this(db, typeId, attributeId, instanceName, inner) { mListPositions.AddRange(positions); }

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
		protected override string BuildString()
		{
			string str = base.BuildString() + (mTypeIdentifier == "$" ? ",$" : ",'" + mTypeIdentifier + "'") + (mAttributeIdentifier == "$" ? ",$," : ",'" + mAttributeIdentifier + "',") +
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
	}
	//ENTITY IfcReferencesValueDocument; // DEPRECEATED IFC4
	//ENTITY IfcRegularTimeSeries
	public class IfcReinforcementBarProperties : IfcPreDefinedProperties
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
		protected override string BuildString()
		{
			return base.BuildString() + "," + ParserSTEP.DoubleToString(mTotalCrossSectionArea) + ",'" + mSteelGrade + "',." + mBarSurface.ToString() + ".," +
				ParserSTEP.DoubleOptionalToString(mEffectiveDepth) + "," + ParserSTEP.DoubleOptionalToString(mNominalBarDiameter) + "," + ParserSTEP.IntOptionalToString(mBarCount);
		}
	}
	public class IfcReinforcementDefinitionProperties : IfcPreDefinedPropertySet //IFC2x3 IfcPropertySetDefinition
	{
		internal string mDefinitionType = "$";// 	:	OPTIONAL IfcLabel; 
		List<int> mReinforcementSectionDefinitions = new List<int>();// :	LIST [1:?] OF IfcSectionReinforcementProperties;
		internal IfcReinforcementDefinitionProperties() : base() { }
		internal IfcReinforcementDefinitionProperties(IfcReinforcementDefinitionProperties p) : base(p)
		{
			mDefinitionType = p.mDefinitionType;
			mReinforcementSectionDefinitions = new List<int>(p.mReinforcementSectionDefinitions);
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
		protected override string BuildString()
		{
			string result = base.BuildString() + (mDefinitionType == "$" ? ",$,(#" : ",'" + mDefinitionType + "',(#") + mReinforcementSectionDefinitions[0];
			for (int icounter = 1; icounter < mReinforcementSectionDefinitions.Count; icounter++)
				result += ",#" + mReinforcementSectionDefinitions;
			return result + ")";
		}
	}
	public partial class IfcReinforcingBar : IfcReinforcingElement
	{
		private double mNominalDiameter;// : IfcPositiveLengthMeasure; 	IFC4 OPTIONAL
		internal double mCrossSectionArea;// : IfcAreaMeasure; IFC4 OPTIONAL
		internal double mBarLength;// : OPTIONAL IfcPositiveLengthMeasure;

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
		internal IfcReinforcingBarTypeEnum mPredefinedType = IfcReinforcingBarTypeEnum.NOTDEFINED;// : IfcReinforcingBarRoleEnum; IFC4 OPTIONAL
		internal IfcReinforcingBarSurfaceEnum mBarSurface = IfcReinforcingBarSurfaceEnum.NONE;// //: OPTIONAL IfcReinforcingBarSurfaceEnum; 
		public IfcReinforcingBarTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcReinforcingBar() : base() { }
		internal IfcReinforcingBar(IfcReinforcingBar b) : base(b)
		{
			mNominalDiameter = b.mNominalDiameter;
			mCrossSectionArea = b.mCrossSectionArea;
			mBarLength = b.mBarLength;
			mPredefinedType = b.mPredefinedType;
			mBarSurface = b.mBarSurface;
		}
		internal IfcReinforcingBar(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

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
		protected override string BuildString()
		{
			string result = base.BuildString() + "," + (mDatabase.mSchema == Schema.IFC2x3 ? ParserSTEP.DoubleToString(mNominalDiameter) + "," + ParserSTEP.DoubleToString(mCrossSectionArea) + "," + ParserSTEP.DoubleToString(mBarLength) + ",." + mPredefinedType.ToString() + ".," :
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
		internal IfcReinforcingBarType(IfcReinforcingBarType t)
			: base(t)
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
		protected override string BuildString()
		{
			string result = base.BuildString();
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
	public abstract class IfcReinforcingElement : IfcElementComponent //	ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcingBar, IfcReinforcingMesh, IfcTendon, IfcTendonAnchor))
	{
		internal new enum SubTypes { IfcReinforcingBar, IfcReinforcingMesh, IfcTendon, IfcTendonAnchor }
		private string mSteelGrade = "$";// : OPTIONAL IfcLabel; //IFC4 Depreceated 

		public string SteelGrade { get { return (mSteelGrade == "$" ? "" : ParserIfc.Decode(mSteelGrade)); } set { mSteelGrade = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcReinforcingElement() : base() { }
		protected IfcReinforcingElement(IfcReinforcingElement e) : base(e) { mSteelGrade = e.mSteelGrade; }
		internal IfcReinforcingElement(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		protected static void parseFields(IfcReinforcingElement e, List<string> arrFields, ref int ipos)
		{
			IfcElementComponent.parseFields(e, arrFields, ref ipos);
			//if(UtilityIFC.mIFCModelData.mSchema == Schema.IFC2x3)
			e.mSteelGrade = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildString() { return base.BuildString() + (mSteelGrade == "$" ? ",$" : ",'" + mSteelGrade + "'"); }
	}
	public abstract class IfcReinforcingElementType : IfcElementComponentType //IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcingBarType, IfcReinforcingMeshType, IfcTendonAnchorType, IfcTendonType))
	{
		protected IfcReinforcingElementType() : base() { }
		protected IfcReinforcingElementType(IfcReinforcingElementType t) : base(t) { }
		protected IfcReinforcingElementType(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcReinforcingElementType t, List<string> arrFields, ref int ipos) { IfcElementComponentType.parseFields(t, arrFields, ref ipos); }
	}
	public class IfcReinforcingMesh : IfcReinforcingElement
	{
		internal double mMeshLength;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mMeshWidth;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mLongitudinalBarNominalDiameter;// : IfcPositiveLengthMeasure;
		internal double mTransverseBarNominalDiameter;// : IfcPositiveLengthMeasure;
		internal double mLongitudinalBarCrossSectionArea;// : IfcPositiveLengthMeasure;
		internal double mTransverseBarCrossSectionArea;// : IfcAreaMeasure;
		internal double mLongitudinalBarSpacing;// : IfcPositiveLengthMeasure;
		internal double mTransverseBarSpacing;// : IfcPositiveLengthMeasure; 
		internal IfcReinforcingMeshTypeEnum mPredefinedType = IfcReinforcingMeshTypeEnum.NOTDEFINED; //	:	OPTIONAL IfcReinforcingMeshTypeEnum;
		public IfcReinforcingMeshTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcReinforcingMesh() : base() { }
		internal IfcReinforcingMesh(IfcReinforcingMesh m) : base(m)
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
		internal IfcReinforcingMesh(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static IfcReinforcingMesh Parse(string strDef, Schema schema) { IfcReinforcingMesh m = new IfcReinforcingMesh(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return m; }
		internal static void parseFields(IfcReinforcingMesh c, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcReinforcingElement.parseFields(c, arrFields, ref ipos);
			c.mMeshLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mMeshWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mTransverseBarNominalDiameter = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mLongitudinalBarCrossSectionArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mTransverseBarCrossSectionArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mLongitudinalBarSpacing = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mTransverseBarSpacing = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					c.mPredefinedType = (IfcReinforcingMeshTypeEnum)Enum.Parse(typeof(IfcReinforcingMeshTypeEnum), str.Replace(".", ""));
			}
		}
		protected override string BuildString()
		{
			return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mMeshLength) + "," + ParserSTEP.DoubleOptionalToString(mMeshWidth) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarNominalDiameter) + "," + ParserSTEP.DoubleToString(mTransverseBarNominalDiameter) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarCrossSectionArea) + "," + ParserSTEP.DoubleToString(mTransverseBarCrossSectionArea) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarSpacing) + "," + ParserSTEP.DoubleToString(mTransverseBarSpacing) + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcReinforcingMeshTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + "."));
		}
	}
	//IfcReinforcingMeshType
	public partial class IfcRelAggregates : IfcRelDecomposes
	{
		internal int mRelatingObject;// : IfcObjectDefinition IFC4 IfcObject
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObjectDefinition; 

		public IfcObjectDefinition RelatingObject { get { return mDatabase.mIfcObjects[mRelatingObject] as IfcObjectDefinition; } }
		public List<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcObjectDefinition); } }

		internal IfcRelAggregates() : base() { }
		internal IfcRelAggregates(IfcRelAggregates a) : base(a) { mRelatingObject = a.mRelatingObject; mRelatedObjects = new List<int>(a.mRelatedObjects.ToArray()); }
		internal IfcRelAggregates(IfcObjectDefinition relObject) : base(relObject.mDatabase)
		{
			mRelatingObject = relObject.mIndex;
			relObject.mIsDecomposedBy.Add(this);
		}
		internal IfcRelAggregates(IfcObjectDefinition relObject, IfcObjectDefinition relatedObject) : this(relObject, new List<IfcObjectDefinition>() { relatedObject }) { }
		internal IfcRelAggregates(IfcObjectDefinition relObject, List<IfcObjectDefinition> relatedObjects) : this(relObject)
		{
			for (int icounter = 0; icounter < relatedObjects.Count; icounter++)
			{
				mRelatedObjects.Add(relatedObjects[icounter].mIndex);
				relatedObjects[icounter].mDecomposes = this;
			}
		}
		internal IfcRelAggregates(DatabaseIfc m, string container, string part, IfcObjectDefinition relObject)
			: this(relObject) { Name = container + " Container"; Description = container + " Container for " + part + "s"; }
		internal IfcRelAggregates(DatabaseIfc m, string container, string part, IfcObjectDefinition relObject, IfcObjectDefinition relatedObject)
			: this(relObject, relatedObject) { Name = container + " Container"; Description = container + " Container for " + part + "s"; }
		internal IfcRelAggregates(string container, string part, IfcObjectDefinition relObject, List<IfcObjectDefinition> relatedObjects)
			: this(relObject, relatedObjects) { Name = container + " Container"; Description = container + " Container for " + part + "s"; }
		internal static IfcRelAggregates Parse(string strDef) { IfcRelAggregates a = new IfcRelAggregates(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAggregates a, List<string> arrFields, ref int ipos) { IfcRelDecomposes.parseFields(a, arrFields, ref ipos); a.mRelatingObject = ParserSTEP.ParseLink(arrFields[ipos++]); a.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = "";
			if (mRelatedObjects.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRelatedObjects[0]);
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			}
			else
				return "";
			return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingObject) + ",(" + str + ")";
		}

		internal override void relate()
		{
			RelatingObject.mIsDecomposedBy.Add(this);
			List<IfcObjectDefinition> ods = RelatedObjects;
			for (int icounter = 0; icounter < ods.Count; icounter++)
			{
				if (ods[icounter] != null)
					ods[icounter].mDecomposes = this;
			}
		}
		internal bool addObject(IfcObjectDefinition o)
		{
			if (mRelatedObjects.Contains(o.mIndex))
				return false;
			mRelatedObjects.Add(o.mIndex);
			o.mDecomposes = this;
			return true;
		}
	}
	public abstract partial class IfcRelAssigns : IfcRelationship
	{
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObjectDefinition;
		//internal IfcObjectTypeEnum mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;// : OPTIONAL IfcObjectTypeEnum; IFC4 CHANGE  The attribute is deprecated and shall no longer be used. A NIL value should always be assigned.
		public List<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcObjectDefinition); } }

		protected IfcRelAssigns() : base() { }
		protected IfcRelAssigns(IfcRelAssigns a) : base(a) { mRelatedObjects = new List<int>(a.mRelatedObjects.ToArray()); }
		protected IfcRelAssigns(DatabaseIfc m) : base(m) { }
		protected IfcRelAssigns(IfcObjectDefinition related) : this(new List<IfcObjectDefinition>() { related }) { }
		protected IfcRelAssigns(List<IfcObjectDefinition> relObjects) : base(relObjects[0].mDatabase)
		{
			for (int icounter = 0; icounter < relObjects.Count; icounter++)
			{
				mRelatedObjects.Add(relObjects[icounter].mIndex);
				relObjects[icounter].mHasAssignments.Add(this);
			}
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			if (mRelatedObjects.Count > 200)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(str);
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					sb.Append(",#" + mRelatedObjects[icounter]);
				sb.Append("),$");
				return sb.ToString();
			}
			else
			{
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					str += ",#" + mRelatedObjects[icounter];
				return str + "),$";
			}
		}
		protected static void parseFields(IfcRelAssigns c, List<string> arrFields, ref int ipos)
		{
			IfcRelationship.parseFields(c, arrFields, ref ipos);
			c.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			ipos++; //string str = arrFields[ipos++]; //if(!str.Contains("$")) c.mRelatedObjectsType = (IfcObjectTypeEnum) Enum.Parse(typeof(IfcObjectTypeEnum),str.Replace(".",""));  
		}
		protected static void parseString(IfcRelAssigns a, string str, ref int pos)
		{
			a.parseString(str, ref pos);
			a.mRelatedObjects = ParserSTEP.StripListLink(str, ref pos);
			ParserSTEP.StripField(str, ref pos);
		}

		internal void assign(IfcObjectDefinition o) { mRelatedObjects.Add(o.mIndex); o.mHasAssignments.Add(this); }

		internal override void relate()
		{
			List<IfcObjectDefinition> ods = RelatedObjects;
			for (int icounter = 0; icounter < ods.Count; icounter++)
			{
				try
				{
					IfcObjectDefinition o = ods[icounter];
					if (o != null)
						o.mHasAssignments.Add(this);
				}
				catch (Exception) { }
			}
			base.relate();
		}
	}
	public partial class IfcRelAssignsTasks : IfcRelAssignsToControl // IFC4 depreceated
	{
		internal int mTimeForTask;// :  	OPTIONAL IfcScheduleTimeControl; 

		public IfcScheduleTimeControl TimeForTask { get { return mDatabase.mIfcObjects[mTimeForTask] as IfcScheduleTimeControl; } }
		internal IfcWorkControl WorkControl { get { return mDatabase.mIfcObjects[mRelatingControl] as IfcWorkControl; } }
		internal List<IfcTask> Tasks { get { return RelatedObjects.ConvertAll(x => x as IfcTask); } }

		internal IfcRelAssignsTasks() : base() { }
		internal IfcRelAssignsTasks(IfcRelAssignsTasks a) : base(a) { mTimeForTask = a.mTimeForTask; }
		internal IfcRelAssignsTasks(IfcWorkControl relControl, IfcScheduleTimeControl timeControl)
			: base(relControl) { if (timeControl != null) { mTimeForTask = timeControl.mIndex; timeControl.mScheduleTimeControlAssigned = this; } }
		internal IfcRelAssignsTasks(IfcWorkControl relControl, IfcTask relObject, IfcScheduleTimeControl timeControl)
			: base(relControl, relObject) { if (timeControl != null) { mTimeForTask = timeControl.mIndex; timeControl.mScheduleTimeControlAssigned = this; } }
		internal IfcRelAssignsTasks(IfcWorkControl relControl, List<IfcTask> relObjects, IfcScheduleTimeControl timeControl)
			: base(relControl, relObjects.ConvertAll(x => (IfcObjectDefinition)x)) { if (timeControl != null) { mTimeForTask = timeControl.mIndex; timeControl.mScheduleTimeControlAssigned = this; } }
		internal new static IfcRelAssignsTasks Parse(string strDef) { IfcRelAssignsTasks a = new IfcRelAssignsTasks(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsTasks c, List<string> arrFields, ref int ipos) { IfcRelAssignsToControl.parseFields(c, arrFields, ref ipos); c.mTimeForTask = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mTimeForTask)); }
		internal override void relate()
		{
			IfcScheduleTimeControl t = mDatabase.mIfcObjects[mTimeForTask] as IfcScheduleTimeControl;
			if (t != null)
				t.mScheduleTimeControlAssigned = this;
			base.relate();
		}
	}
	public partial class IfcRelAssignsToActor : IfcRelAssigns
	{
		internal int mRelatingActor;// : IfcActor; 
		internal int mActingRole;//	 :	OPTIONAL IfcActorRole;

		public IfcActor RelatingActor { get { return mDatabase.mIfcObjects[mRelatingActor] as IfcActor; } }

		internal IfcRelAssignsToActor() : base() { }
		internal IfcRelAssignsToActor(IfcRelAssignsToActor a) : base(a) { mRelatingActor = a.mRelatingActor; mActingRole = a.mActingRole; }
		internal IfcRelAssignsToActor(IfcActor relActor, IfcActorRole r) : base(relActor.mDatabase) { mRelatingActor = relActor.mIndex; if (r != null) mActingRole = r.mIndex; }
		internal IfcRelAssignsToActor(IfcActor relActor, IfcObjectDefinition relObject, IfcActorRole r) : base(relObject) { mRelatingActor = relActor.mIndex; if (r != null) mActingRole = r.mIndex; }
		internal IfcRelAssignsToActor(IfcActor relActor, List<IfcObjectDefinition> relObjects, IfcActorRole r) : base(relObjects) { mRelatingActor = relActor.mIndex; if (r != null) mActingRole = r.mIndex; }
		internal static IfcRelAssignsToActor Parse(string strDef) { IfcRelAssignsToActor a = new IfcRelAssignsToActor(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsToActor c, List<string> arrFields, ref int ipos) { IfcRelAssigns.parseFields(c, arrFields, ref ipos); c.mRelatingActor = ParserSTEP.ParseLink(arrFields[ipos++]); c.mActingRole = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingActor)); }
		internal override void relate()
		{
			IfcActor c = mDatabase.mIfcObjects[mRelatingActor] as IfcActor;
			if (c != null)
				c.mIsActingUpon.Add(this);
			base.relate();
		}
	}
	public class IfcRelAssignsToControl : IfcRelAssigns
	{
		internal int mRelatingControl;// : IfcControl; 
		internal IfcControl RelatingControl { get { return mDatabase.mIfcObjects[mRelatingControl] as IfcControl; } }

		internal IfcRelAssignsToControl() : base() { }
		internal IfcRelAssignsToControl(IfcRelAssignsToControl a) : base(a) { mRelatingControl = a.mRelatingControl; }
		internal IfcRelAssignsToControl(IfcControl relControl) : base(relControl.mDatabase) { mRelatingControl = relControl.mIndex; relControl.mControls.Add(this); }
		internal IfcRelAssignsToControl(IfcControl relControl, IfcObjectDefinition relObject) : base(relObject) { mRelatingControl = relControl.mIndex; relControl.mControls.Add(this); }
		internal IfcRelAssignsToControl(IfcControl relControl, List<IfcObjectDefinition> relObjects) : base(relObjects) { mRelatingControl = relControl.mIndex; relControl.mControls.Add(this); }
		internal static IfcRelAssignsToControl Parse(string strDef) { IfcRelAssignsToControl a = new IfcRelAssignsToControl(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsToControl c, List<string> arrFields, ref int ipos) { IfcRelAssigns.parseFields(c, arrFields, ref ipos); c.mRelatingControl = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingControl)); }
		internal override void relate()
		{
			IfcControl c = mDatabase.mIfcObjects[mRelatingControl] as IfcControl;
			if (c != null)
				c.mControls.Add(this);
			base.relate();
		}
	}
	public partial class IfcRelAssignsToGroup : IfcRelAssigns 	//SUPERTYPE OF(IfcRelAssignsToGroupByFactor)
	{
		private int mRelatingGroup;// : IfcGroup; 
		public IfcGroup RelatingGroup { get { return mDatabase.mIfcObjects[mRelatingGroup] as IfcGroup; } }

		internal IfcRelAssignsToGroup() : base() { }
		internal IfcRelAssignsToGroup(IfcRelAssignsToGroup a) : base(a) { mRelatingGroup = a.mRelatingGroup; }
		internal IfcRelAssignsToGroup(IfcGroup relgroup) : base(relgroup.mDatabase) { mRelatingGroup = relgroup.mIndex; relgroup.mIsGroupedBy.Add(this); }
		internal IfcRelAssignsToGroup(IfcGroup relgroup, List<IfcObjectDefinition> relObjects) : base(relObjects) { mRelatingGroup = relgroup.mIndex; }
		internal static IfcRelAssignsToGroup Parse(string strDef)
		{
			IfcRelAssignsToGroup a = new IfcRelAssignsToGroup();
			int pos = 0;
			parseString(a, strDef, ref pos);
			return a;
		}
		protected static void parseString(IfcRelAssignsToGroup a, string str, ref int pos)
		{
			IfcRelAssigns.parseString(a, str, ref pos);
			a.mRelatingGroup = ParserSTEP.StripLink(str, ref pos);
		}
		protected override string BuildString() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingGroup)); }
		internal override void relate()
		{
			IfcGroup g = RelatingGroup;
			if (g != null)
				g.mIsGroupedBy.Add(this);
			base.relate();
		}
	}
	public class IfcRelAssignsToGroupByFactor : IfcRelAssignsToGroup //IFC4
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 ? "IFCRELASSIGNSTOGROUP" : base.KeyWord); } }
		internal double mFactor = 1;//	 :	IfcRatioMeasure;
		internal IfcRelAssignsToGroupByFactor() : base() { }
		internal IfcRelAssignsToGroupByFactor(IfcRelAssignsToGroupByFactor a) : base(a) { mFactor = a.mFactor; }
		internal IfcRelAssignsToGroupByFactor(IfcGroup relgroup, double factor) : base(relgroup) { mFactor = factor; }
		internal IfcRelAssignsToGroupByFactor(IfcGroup relgroup, List<IfcObjectDefinition> relObjects, double factor) : base(relgroup, relObjects) { mFactor = factor; }
		internal new static IfcRelAssignsToGroup Parse(string strDef)
		{
			IfcRelAssignsToGroupByFactor a = new IfcRelAssignsToGroupByFactor();
			int pos = 0;
			IfcRelAssignsToGroup.parseString(a, strDef, ref pos);
			a.mFactor = ParserSTEP.StripDouble(strDef, ref pos);
			return a;
		}
		protected override string BuildString() { return (mRelatedObjects.Count == 0 ? "" : base.BuildString() + (mDatabase.ModelView == ModelView.Ifc2x3Coordination ? "" : "," + ParserSTEP.DoubleToString(mFactor))); }
	}
	public class IfcRelAssignsToProcess : IfcRelAssigns
	{
		internal int mRelatingProcess;// : IfcProcess; 
		internal IfcRelAssignsToProcess() : base() { }
		internal IfcRelAssignsToProcess(IfcRelAssignsToProcess a) : base(a) { mRelatingProcess = a.mRelatingProcess; }
		internal IfcRelAssignsToProcess(IfcProcess relProcess)
			: base(relProcess.mDatabase) { mRelatingProcess = relProcess.mIndex; }
		internal IfcRelAssignsToProcess(IfcProcess relProcess, IfcObjectDefinition relObject)
			: base(relObject) { mRelatingProcess = relProcess.mIndex; }
		internal IfcRelAssignsToProcess(IfcProcess relProcess, List<IfcObjectDefinition> relObjects)
			: base(relObjects) { mRelatingProcess = relProcess.mIndex; }
		internal static IfcRelAssignsToProcess Parse(string strDef) { IfcRelAssignsToProcess a = new IfcRelAssignsToProcess(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsToProcess c, List<string> arrFields, ref int ipos) { IfcRelAssigns.parseFields(c, arrFields, ref ipos); c.mRelatingProcess = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingProcess)); }
		internal override void relate()
		{
			IfcProcess p = mDatabase.mIfcObjects[mRelatingProcess] as IfcProcess;
			if (p != null)
				p.mHasAssignments.Add(this);
			base.relate();
		}
	}
	public partial class IfcRelAssignsToProduct : IfcRelAssigns
	{
		private int mRelatingProduct;// : IFC4	IfcProductSelect; : IfcProduct; 

		public IfcProductSelect RelatingProduct { get { return mDatabase.mIfcObjects[mRelatingProduct] as IfcProductSelect; } }

		internal IfcRelAssignsToProduct() : base() { }
		internal IfcRelAssignsToProduct(IfcRelAssignsToProduct a) : base(a) { mRelatingProduct = a.mRelatingProduct; }
		internal IfcRelAssignsToProduct(IfcProductSelect relProduct)
			: base(relProduct.Database) { mRelatingProduct = relProduct.Index; relProduct.ReferencedBy.Add(this); ; }
		public IfcRelAssignsToProduct(IfcObjectDefinition relObject, IfcProductSelect relProduct)
			: base(relObject) { mRelatingProduct = relProduct.Index; relProduct.ReferencedBy.Add(this); }
		public IfcRelAssignsToProduct(List<IfcObjectDefinition> relObjects, IfcProductSelect relProduct)
			: base(relObjects) { mRelatingProduct = relProduct.Index; relProduct.ReferencedBy.Add(this); }
		internal static IfcRelAssignsToProduct Parse(string strDef) { IfcRelAssignsToProduct a = new IfcRelAssignsToProduct(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsToProduct c, List<string> arrFields, ref int ipos) { IfcRelAssigns.parseFields(c, arrFields, ref ipos); c.mRelatingProduct = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingProduct)); }
		internal override void relate()
		{
			IfcProductSelect p = RelatingProduct;
			if (p != null)
				p.ReferencedBy.Add(this);
			base.relate();
		}
	}
	//ENTITY IfcRelAssignsToProjectOrder // DEPRECEATED IFC4 
	public partial class IfcRelAssignsToResource : IfcRelAssigns
	{
		internal int mRelatingResource;// : IfcResource; 
		internal IfcRelAssignsToResource() : base() { }
		internal IfcRelAssignsToResource(IfcRelAssignsToResource i) : base(i) { mRelatingResource = i.mRelatingResource; }
		internal IfcRelAssignsToResource(IfcResource relResource) : base(relResource.mDatabase) { mRelatingResource = relResource.mIndex; }
		internal IfcRelAssignsToResource(IfcResource relResource, IfcObjectDefinition relObject) : base(relObject) { mRelatingResource = relResource.mIndex; }
		internal static IfcRelAssignsToResource Parse(string strDef) { IfcRelAssignsToResource a = new IfcRelAssignsToResource(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsToResource c, List<string> arrFields, ref int ipos) { IfcRelAssigns.parseFields(c, arrFields, ref ipos); c.mRelatingResource = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingResource)); }
		internal override void relate()
		{
			IfcResource r = mDatabase.mIfcObjects[mRelatingResource] as IfcResource;
			if (r != null)
				r.mResourceOf.Add(this);
			base.relate();
		}
	}
	public abstract partial class IfcRelAssociates : IfcRelationship   //ABSTRACT SUPERTYPE OF (ONEOF(IfcRelAssociatesApproval,IfcRelAssociatesclassification,IfcRelAssociatesConstraint,IfcRelAssociatesDocument,IfcRelAssociatesLibrary,IfcRelAssociatesMaterial))
	{
		internal int mID = 0;
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcDefinitionSelect IFC2x3 IfcRoot; 

		List<IfcDefinitionSelect> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcDefinitionSelect); } }

		protected IfcRelAssociates() : base() { }
		protected IfcRelAssociates(IfcRelAssociates i) : base(i) { mRelatedObjects = new List<int>(i.mRelatedObjects.ToArray()); }
		internal IfcRelAssociates(DatabaseIfc m) : base(m) { }
		internal IfcRelAssociates(IfcDefinitionSelect related) : base(related.Model) { mRelatedObjects.Add(related.Index); }
		protected override string BuildString()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			string str = base.BuildString() + ",(#" + mRelatedObjects[0];
			if (mRelatedObjects.Count > 200)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(str);
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					sb.Append(",#" + mRelatedObjects[icounter]);
				sb.Append(")");
				return sb.ToString();
			}
			else
			{
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					str += ",#" + mRelatedObjects[icounter];
				return str + ")";
			}
		}
		protected static void parseFields(IfcRelAssociates a, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(a, arrFields, ref ipos); a.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override void parseString(string str, ref int pos)
		{
			base.parseString(str, ref pos);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos);
		}
		internal void addAssociation(IfcDefinitionSelect obj) { mRelatedObjects.Add(obj.Index); obj.HasAssociations.Add(this); }
		internal void addAssociation(List<IfcDefinitionSelect> objs)
		{
			for (int icounter = 0; icounter < objs.Count; icounter++)
			{
				IfcDefinitionSelect obj = objs[icounter];
				mRelatedObjects.Add(obj.Index);
				obj.HasAssociations.Add(this);
			}
		}
		internal void unassign(IfcDefinitionSelect d) { mRelatedObjects.Remove(d.Index); d.HasAssociations.Remove(this); }
		internal void unassign(List<IfcDefinitionSelect> ds)
		{
			for (int icounter = 0; icounter < ds.Count; icounter++)
			{
				IfcDefinitionSelect d = ds[icounter];
				mRelatedObjects.Remove(d.Index);
				d.HasAssociations.Remove(this);
			}
		}
		public override string ToString() { return (mRelatedObjects.Count == 0 ? "" : base.ToString()); }
		internal override void relate()
		{
			for (int icounter = 0; icounter < mRelatedObjects.Count; icounter++)
			{
				IfcDefinitionSelect r = mDatabase.mIfcObjects[mRelatedObjects[icounter]] as IfcDefinitionSelect;
				if (r != null)
					r.HasAssociations.Add(this);
			}
		}
	}
	//ENTITY IfcRelAssociatesAppliedValue // DEPRECEATED IFC4
	//ENTITY IfcRelAssociatesApproval
	public partial class IfcRelAssociatesClassification : IfcRelAssociates
	{
		internal int mRelatingClassification;// : IfcClassificationSelect; IFC2x3  	IfcClassificationNotationSelect
		public IfcClassificationSelect RelatingClassification { get { return mDatabase.mIfcObjects[mRelatingClassification] as IfcClassificationSelect; } }

		internal IfcRelAssociatesClassification() : base() { }
		internal IfcRelAssociatesClassification(IfcRelAssociatesClassification a) : base(a) { mRelatingClassification = a.mRelatingClassification; }
		internal IfcRelAssociatesClassification(IfcClassificationSelect cl) : base(cl.Database) { mRelatingClassification = cl.Index; }
		internal static IfcRelAssociatesClassification Parse(string strDef) { IfcRelAssociatesClassification a = new IfcRelAssociatesClassification(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssociatesClassification a, List<string> arrFields, ref int ipos) { IfcRelAssociates.parseFields(a, arrFields, ref ipos); a.mRelatingClassification = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingClassification); }
		internal override void relate()
		{
			IfcClassificationSelect cs = mDatabase.mIfcObjects[mRelatingClassification] as IfcClassificationSelect;
			cs.ClassificationForObjects.Add(this);
			base.relate();
		}
	}
	public partial class IfcRelAssociatesConstraint : IfcRelAssociates
	{
		internal string mIntent = "$";// :	OPTIONAL IfcLabel;
		private int mRelatingConstraint;// : IfcConstraint

		internal IfcConstraint RelatingConstraint { get { return mDatabase.mIfcObjects[mRelatingConstraint] as IfcConstraint; } }
		internal IfcRelAssociatesConstraint() : base() { }
		internal IfcRelAssociatesConstraint(IfcRelAssociatesConstraint a) : base(a) { mRelatingConstraint = a.mRelatingConstraint; }
		internal IfcRelAssociatesConstraint(string intent, IfcConstraint c)
			: base(c.mDatabase) { if (!string.IsNullOrEmpty(intent)) mIntent = intent.Replace("'", ""); mRelatingConstraint = c.Index; }
		public IfcRelAssociatesConstraint(IfcDefinitionSelect related, string intent, IfcConstraint c)
			: base(related) { if (!string.IsNullOrEmpty(intent)) mIntent = intent.Replace("'", ""); mRelatingConstraint = c.Index; }

		internal static IfcRelAssociatesConstraint Parse(string strDef) { IfcRelAssociatesConstraint a = new IfcRelAssociatesConstraint(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssociatesConstraint a, List<string> arrFields, ref int ipos) { IfcRelAssociates.parseFields(a, arrFields, ref ipos); a.mIntent = arrFields[ipos++].Replace("'", ""); a.mRelatingConstraint = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + (mIntent == "$" ? ",$," : ",'" + mIntent + "',") + ParserSTEP.LinkToString(mRelatingConstraint); }
		internal override void relate()
		{
			IfcConstraint c = mDatabase.mIfcObjects[mRelatingConstraint] as IfcConstraint;
			//c.classificationForObjects.Add(this);
			base.relate();
		}
	}
	public partial class IfcRelAssociatesDocument : IfcRelAssociates
	{
		internal int mRelatingDocument;// : IfcDocumentSelect; 
		internal IfcRelAssociatesDocument() : base() { }
		internal IfcRelAssociatesDocument(IfcRelAssociatesDocument i) : base(i) { mRelatingDocument = i.mRelatingDocument; }
		public IfcRelAssociatesDocument(IfcDefinitionSelect related, IfcDocumentSelect document) : base(related) { mRelatingDocument = document.Index; }
		internal IfcRelAssociatesDocument(IfcDocumentSelect document) : base(document.Database) { Name = "DocAssoc"; Description = "Document Associates"; mRelatingDocument = document.Index; document.Associates.Add(this); }
		internal static IfcRelAssociatesDocument Parse(string strDef) { IfcRelAssociatesDocument a = new IfcRelAssociatesDocument(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssociatesDocument a, List<string> arrFields, ref int ipos) { IfcRelAssociates.parseFields(a, arrFields, ref ipos); a.mRelatingDocument = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingDocument); }
		internal IfcDocumentSelect getDocument() { return mDatabase.mIfcObjects[mRelatingDocument] as IfcDocumentSelect; }
	}
	public class IfcRelAssociatesLibrary : IfcRelAssociates
	{
		internal string mMatName = "";
		internal int mRelatingLibrary;// : IfcLibrarySelect; 
		internal IfcRelAssociatesLibrary() : base() { }
		internal IfcRelAssociatesLibrary(IfcRelAssociatesLibrary i) : base(i) { mRelatingLibrary = i.mRelatingLibrary; }
		internal IfcRelAssociatesLibrary(DatabaseIfc m, IfcLibrarySelect lib) : base(m) { Name = "LibAssoc"; Description = "Library Associates"; mRelatingLibrary = lib.Index; }
		internal static IfcRelAssociatesLibrary Parse(string strDef) { IfcRelAssociatesLibrary a = new IfcRelAssociatesLibrary(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssociatesLibrary a, List<string> arrFields, ref int ipos) { IfcRelAssociates.parseFields(a, arrFields, ref ipos); a.mRelatingLibrary = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingLibrary); }
	}
	public partial class IfcRelAssociatesMaterial : IfcRelAssociates
	{
		private int mRelatingMaterial;// : IfcMaterialSelect; 

		internal IfcMaterialSelect RelatingMaterial { get { return mDatabase.mIfcObjects[mRelatingMaterial] as IfcMaterialSelect; } }

		internal IfcRelAssociatesMaterial() : base() { }
		internal IfcRelAssociatesMaterial(IfcRelAssociatesMaterial i) : base(i) { mRelatingMaterial = i.mRelatingMaterial; }
		internal IfcRelAssociatesMaterial(IfcMaterialSelect material) : base(material.Database) { Name = "MatAssoc"; Description = "Material Associates"; mRelatingMaterial = material.Index; material.Associates = this; }
		internal static IfcRelAssociatesMaterial Parse(string strDef)
		{
			IfcRelAssociatesMaterial a = new IfcRelAssociatesMaterial();
			int pos = 0;
			a.parseString(strDef, ref pos);
			a.mRelatingMaterial = ParserSTEP.StripLink(strDef, ref pos);
			return a;
		}
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 && string.IsNullOrEmpty(RelatingMaterial.ToString()) ? "" : base.BuildString() + ",#" + mRelatingMaterial); }
		internal override void relate()
		{
			base.relate();
			IfcMaterialSelect ms = mDatabase.mIfcObjects[mRelatingMaterial] as IfcMaterialSelect;
			if (ms != null)
				ms.Associates = this;
		}
	}
	public partial class IfcRelAssociatesProfileProperties : IfcRelAssociates //IFC4 DELETED Replaced by IfcRelAssociatesMaterial together with material-profile sets
	{
		private int mRelatingProfileProperties;// : IfcProfileProperties;
		internal int mProfileSectionLocation;// : OPTIONAL IfcShapeAspect;
		internal double mProfileOrientation;// : OPTIONAL IfcOrientationSelect; //TYPE IfcOrientationSelect = SELECT(IfcPlaneAngleMeasure,IfcDirection);
		private bool mAngle = true;

		internal IfcProfileProperties RelatingProfileProperties { get { return mDatabase.mIfcObjects[mRelatingProfileProperties] as IfcProfileProperties; } }

		internal IfcRelAssociatesProfileProperties() : base() { }
		internal IfcRelAssociatesProfileProperties(IfcRelAssociatesProfileProperties i) : base(i) { mRelatingProfileProperties = i.mRelatingProfileProperties; mProfileSectionLocation = i.mProfileSectionLocation; mProfileOrientation = i.mProfileOrientation; }
		internal IfcRelAssociatesProfileProperties(IfcProfileProperties pp) : base(pp.mDatabase) { if (pp.mDatabase.mSchema != Schema.IFC2x3) throw new Exception(KeyWord + " Deleted in IFC4"); mRelatingProfileProperties = pp.mIndex; }
		internal static IfcRelAssociatesProfileProperties Parse(string strDef) { IfcRelAssociatesProfileProperties a = new IfcRelAssociatesProfileProperties(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssociatesProfileProperties a, List<string> arrFields, ref int ipos)
		{
			IfcRelAssociates.parseFields(a, arrFields, ref ipos);
			a.mRelatingProfileProperties = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mProfileSectionLocation = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (arrFields[ipos].StartsWith("IfcPlaneAngleMeasure(", true, System.Globalization.CultureInfo.CurrentCulture))
			{
				string str = arrFields[ipos++];
				a.mProfileOrientation = ParserSTEP.ParseDouble(str.Substring(21, str.Length - 22));
			}
			else
			{
				a.mAngle = false;
				a.mProfileOrientation = ParserSTEP.ParseLink(arrFields[ipos++]);
			}
		}
		protected override string BuildString()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			string str = base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingProfileProperties) + "," +
				ParserSTEP.LinkToString(mProfileSectionLocation) + ",";
			if (mAngle)
			{
				if (mProfileOrientation == 0)
					return str + "$";
				return str + ",IFCPLANEANGLEMEASURE(" + ParserSTEP.DoubleToString(mProfileOrientation) + ")";
			}
			return str + ParserSTEP.LinkToString((int)mProfileOrientation);
		}
	}
	public abstract class IfcRelationship : IfcRoot  //ABSTRACT SUPERTYPE OF (ONEOF (IfcRelAssigns ,IfcRelAssociates ,IfcRelConnects ,IfcRelDecomposes ,IfcRelDefines))
	{
		protected IfcRelationship() : base() { }
		protected IfcRelationship(IfcRelationship r) : base(r) { }
		internal IfcRelationship(DatabaseIfc m) : base(m) {  }
		protected static void parseFields(IfcRelationship r, List<string> arrFields, ref int ipos) { IfcRoot.parseFields(r, arrFields, ref ipos); }
		internal virtual void relate() { }
	}
	/*internal class IfcRelaxation : IfcEntity // DEPRECEATED IFC4
	{
		public override string getKW { get { return mKW; } }
		internal new static string mKW = strCrypto.Decrypt("sSev2y4jvspcTXVmK7+oEg=="); //// IFCRELAXATION
		internal int mRelaxationValue;// : IfcNormalisedRatioMeasure;
		internal int mInitialStress;// : IfcNormalisedRatioMeasure; 
		internal IfcRelaxation(IfcRelaxation p) : base() { mRelaxationValue = p.mRelaxationValue; mInitialStress = p.mInitialStress; }
		internal IfcRelaxation() : base() { }
	 	internal new static IfcRelaxation Parse(string strDef) { int ipos = 0; return parseFields(IFCModel.mParser.splitLineFields(strDef), ref ipos); }
		internal new static IfcRelaxation parseFields(List<string> arrFields, ref int ipos)
		{
			IfcRelaxation rm = new IfcRelaxation();
			rm.mRelaxationValue = IFCModel.mParser.parseSTPLink(arrFields[ipos++]);
			rm.mInitialStress = IFCModel.mParser.parseSTPLink(arrFields[ipos++]);
			return rm;
		}
		protected override string BuildString() { return base.BuildString() + "," + IFCModel.mParser.STPLinkToString(mRelaxationValue) + "," + IFCModel.mParser.STPLinkToString(mInitialStress); }
	}*/
	public abstract class IfcRelConnects : IfcRelationship //ABSTRACT SUPERTYPE OF (ONEOF (IfcRelConnectsElements ,IfcRelConnectsPortToElement ,IfcRelConnectsPorts ,IfcRelConnectsStructuralActivity ,IfcRelConnectsStructuralMember
	{  //,IfcRelContainedInSpatialStructure ,IfcRelCoversBldgElements ,IfcRelCoversSpaces ,IfcRelFillsElement ,IfcRelFlowControlElements ,IfcRelInterferesElements ,IfcRelReferencedInSpatialStructure ,IfcRelSequence ,IfcRelServicesBuildings ,IfcRelSpaceBoundary))
		protected IfcRelConnects() : base() { }
		protected IfcRelConnects(IfcRelConnects i) : base(i) { }
		internal IfcRelConnects(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcRelConnects i, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(i, arrFields, ref ipos); }
	}
	public class IfcRelConnectsElements : IfcRelConnects
	{
		internal int mConnectionGeometry;// : OPTIONAL IfcConnectionGeometry;
		internal int mRelatingElement;// : IfcElement;
		internal int mRelatedElement;// : IfcElement; 

		internal IfcConnectionGeometry ConnectionGeometry { get { return mDatabase.mIfcObjects[mConnectionGeometry] as IfcConnectionGeometry; } set { mConnectionGeometry = (value == null ? 0 : value.mIndex); } }
		internal IfcElement RelatingElement { get { return mDatabase.mIfcObjects[mRelatingElement] as IfcElement; } }
		internal IfcElement RelatedElement { get { return mDatabase.mIfcObjects[mRelatedElement] as IfcElement; } }

		internal IfcRelConnectsElements() : base() { }
		internal IfcRelConnectsElements(IfcRelConnectsElements i) : base(i) { mConnectionGeometry = i.mConnectionGeometry; mRelatingElement = i.mRelatingElement; mRelatedElement = i.mRelatedElement; }
		internal IfcRelConnectsElements(IfcElement relating, IfcElement related)
			: base(relating.mDatabase)
		{
			mRelatingElement = relating.mIndex;
			relating.mConnectedFrom.Add(this);
			mRelatedElement = related.mIndex;
			related.mConnectedTo.Add(this);
		}
		internal IfcRelConnectsElements(IfcConnectionGeometry cg, IfcElement relating, IfcElement related) : this(relating, related) { mConnectionGeometry = cg.mIndex; }
		internal static IfcRelConnectsElements Parse(string strDef) { IfcRelConnectsElements i = new IfcRelConnectsElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsElements i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mConnectionGeometry = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return (mRelatingElement == 0 || mRelatedElement == 0 ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mConnectionGeometry) + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedElement)); }
		internal override void relate()
		{
			IfcElement rg = mDatabase.mIfcObjects[mRelatingElement] as IfcElement, rd = mDatabase.mIfcObjects[mRelatedElement] as IfcElement;
			if (rg != null)
				rg.mConnectedFrom.Add(this);
			if (rd != null)
				rd.mConnectedTo.Add(this);
			base.relate();
		}
		internal IfcElement getConnected(IfcElement e) { return mDatabase.mIfcObjects[(mRelatingElement == e.mIndex ? mRelatedElement : mRelatingElement)] as IfcElement; }
	}
	public class IfcRelConnectsPathElements : IfcRelConnectsElements
	{
		internal List<int> mRelatingPriorities = new List<int>();// : LIST [0:?] OF INTEGER;
		internal List<int> mRelatedPriorities = new List<int>();// : LIST [0:?] OF INTEGER;
		internal IfcConnectionTypeEnum mRelatedConnectionType = IfcConnectionTypeEnum.NOTDEFINED;// : IfcConnectionTypeEnum;
		internal IfcConnectionTypeEnum mRelatingConnectionType = IfcConnectionTypeEnum.NOTDEFINED;// : IfcConnectionTypeEnum; 
		internal IfcRelConnectsPathElements() : base() { }
		internal IfcRelConnectsPathElements(IfcRelConnectsPathElements i)
			: base(i)
		{
			mRelatingPriorities.AddRange(i.mRelatingPriorities);
			mRelatedPriorities.AddRange(i.mRelatedPriorities);
			mRelatedConnectionType = i.mRelatedConnectionType;
			mRelatingConnectionType = i.mRelatingConnectionType;
		}
		internal new static IfcRelConnectsPathElements Parse(string strDef) { IfcRelConnectsPathElements i = new IfcRelConnectsPathElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsPathElements i, List<string> arrFields, ref int ipos)
		{
			IfcRelConnectsElements.parseFields(i, arrFields, ref ipos);
			i.mRelatingPriorities = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			i.mRelatedPriorities = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			i.mRelatedConnectionType = (IfcConnectionTypeEnum)Enum.Parse(typeof(IfcConnectionTypeEnum), arrFields[ipos++].Replace(".", ""));
			i.mRelatingConnectionType = (IfcConnectionTypeEnum)Enum.Parse(typeof(IfcConnectionTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildString()
		{
			if (mRelatingElement == 0 || mRelatedElement == 0)
				return "";
			string str = base.BuildString() + ",(";
			if (mRelatingPriorities.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRelatingPriorities[0]);
				for (int icounter = 1; icounter < mRelatingPriorities.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRelatingPriorities[icounter]);
			}
			str += "),(";
			if (mRelatedPriorities.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRelatedPriorities[0]);
				for (int icounter = 1; icounter < mRelatedPriorities.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRelatedPriorities[icounter]);
			}
			return str + "),." + mRelatedConnectionType.ToString() + ".,." + mRelatingConnectionType.ToString() + ".";
		}
	}
	public class IfcRelConnectsPortToElement : IfcRelConnects
	{
		internal int mRelatingPort;// : IfcPort;
		internal int mRelatedElement;// : IfcElement; 

		internal IfcPort RelatingPort { get { return mDatabase.mIfcObjects[mRelatingPort] as IfcPort; } }
		internal IfcElement RelatedElement { get { return mDatabase.mIfcObjects[mRelatedElement] as IfcElement; } }

		internal IfcRelConnectsPortToElement() : base() { }
		internal IfcRelConnectsPortToElement(IfcRelConnectsPortToElement c) : base(c) { mRelatingPort = c.mRelatingPort; mRelatedElement = c.mRelatedElement; }
		internal IfcRelConnectsPortToElement(IfcPort p, IfcElement e) : base(p.mDatabase)
		{
			mRelatingPort = p.mIndex;
			p.mContainedIn = this;
			mRelatedElement = e.mIndex;
			e.mHasPorts.Add(this);
		}
		internal static IfcRelConnectsPortToElement Parse(string strDef) { IfcRelConnectsPortToElement i = new IfcRelConnectsPortToElement(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsPortToElement i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingPort = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedElement); }
		internal override void relate() { RelatingPort.mContainedIn = this; RelatedElement.mHasPorts.Add(this); }
	}
	public class IfcRelConnectsPorts : IfcRelConnects
	{
		internal int mRelatingPort;// : IfcPort;
		internal int mRelatedPort;// : IfcPort;
		internal int mRealizingElement;// : OPTIONAL IfcElement; 

		internal IfcPort RelatingPort { get { return mDatabase.mIfcObjects[mRelatingPort] as IfcPort; } }
		internal IfcPort RelatedPort { get { return mDatabase.mIfcObjects[mRelatedPort] as IfcPort; } }
		internal IfcElement RealizingElement { get { return mDatabase.mIfcObjects[mRealizingElement] as IfcElement; } }

		internal IfcRelConnectsPorts() : base() { }
		internal IfcRelConnectsPorts(IfcRelConnectsPorts c) : base(c) { mRelatingPort = c.mRelatingPort; mRelatedPort = c.mRelatedPort; mRealizingElement = c.mRealizingElement; }
		internal IfcRelConnectsPorts(IfcPort relatingPort, IfcPort relatedPort, IfcElement realizingElement)
			: base(relatingPort.mDatabase) { mRelatingPort = relatingPort.mIndex; mRelatedPort = relatedPort.mIndex; if (realizingElement != null) mRealizingElement = realizingElement.mIndex; }
		internal static IfcRelConnectsPorts Parse(string strDef) { IfcRelConnectsPorts i = new IfcRelConnectsPorts(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsPorts i, List<string> arrFields, ref int ipos)
		{
			IfcRelConnects.parseFields(i, arrFields, ref ipos);
			i.mRelatingPort = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mRelatedPort = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mRealizingElement = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedPort) + "," + ParserSTEP.LinkToString(mRealizingElement); }
		internal override void relate()
		{
			IfcPort rg = RelatingPort, rd = RelatedPort;
			rg.mConnectedFrom = this;
			rd.mConnectedTo = this;
		}
		internal IfcPort getOtherPort(IfcPort p) { return (mRelatedPort == p.mIndex ? mDatabase.mIfcObjects[mRelatingPort] as IfcPort : mDatabase.mIfcObjects[mRelatedPort] as IfcPort); }
	}
	public class IfcRelConnectsStructuralActivity : IfcRelConnects
	{
		private int mRelatingElement;// : IfcStructuralActivityAssignmentSelect; SELECT(IfcStructuralItem,IfcElement);
		private int mRelatedStructuralActivity;// : IfcStructuralActivity; 

		internal IfcStructuralActivityAssignmentSelect RelatingElement { get { return mDatabase.mIfcObjects[mRelatingElement] as IfcStructuralActivityAssignmentSelect; } }
		internal IfcStructuralActivity RelatedStructuralActivity { get { return mDatabase.mIfcObjects[mRelatedStructuralActivity] as IfcStructuralActivity; } }

		internal IfcRelConnectsStructuralActivity() : base() { }
		internal IfcRelConnectsStructuralActivity(IfcRelConnectsStructuralActivity c) : base(c) { mRelatingElement = c.mRelatingElement; mRelatedStructuralActivity = c.mRelatedStructuralActivity; }
		internal IfcRelConnectsStructuralActivity(IfcStructuralActivityAssignmentSelect item, IfcStructuralActivity a)
			: base(a.mDatabase) { mRelatingElement = item.Index; mRelatedStructuralActivity = a.mIndex; }
		internal static IfcRelConnectsStructuralActivity Parse(string strDef) { IfcRelConnectsStructuralActivity i = new IfcRelConnectsStructuralActivity(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsStructuralActivity i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedStructuralActivity = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedStructuralActivity); }
		internal override void relate()
		{
			IfcStructuralActivity sa = RelatedStructuralActivity;
			sa.AssignedToStructuralItem = this;
			IfcStructuralActivityAssignmentSelect saa = RelatingElement;
			IfcStructuralItem si = saa as IfcStructuralItem;
			if (si != null)
				si.mAssignedStructuralActivity.Add(this);
			else
			{
				IfcElement element = saa as IfcElement;
				if (element != null)
					element.mAssignedStructuralActivity.Add(this);
			}
		}
	}
	public partial class IfcRelConnectsStructuralElement : IfcRelConnects //DELETED IFC4 Replaced by IfcRelAssignsToProduct
	{
		internal int mRelatingElement;// : IfcElement;
		internal int mRelatedStructuralMember;// : IfcStructuralMember; 

		public IfcElement RelatingElement { get { return mDatabase.mIfcObjects[mRelatingElement] as IfcElement; } }
		public IfcStructuralMember RelatedStructuralMember { get { return mDatabase.mIfcObjects[mRelatedStructuralMember] as IfcStructuralMember; } }

		internal IfcRelConnectsStructuralElement() : base() { }
		internal IfcRelConnectsStructuralElement(IfcRelConnectsStructuralElement c) : base(c) { mRelatingElement = c.mRelatingElement; mRelatedStructuralMember = c.mRelatedStructuralMember; }
		internal IfcRelConnectsStructuralElement(IfcElement elem, IfcStructuralMember memb)
			: base(elem.mDatabase) { if (elem.mDatabase.mSchema != Schema.IFC2x3) throw new Exception(KeyWord + " Deleted IFC4!"); mRelatingElement = elem.mIndex; mRelatedStructuralMember = memb.mIndex; }
		internal static IfcRelConnectsStructuralElement Parse(string strDef) { IfcRelConnectsStructuralElement i = new IfcRelConnectsStructuralElement(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsStructuralElement i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedStructuralMember = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedStructuralMember); }
		internal override void relate() { IfcElement element = RelatingElement; if (element != null) element.mHasStructuralMemberSS.Add(this); (mDatabase.mIfcObjects[mRelatedStructuralMember] as IfcStructuralMember).mStructuralMemberForGG = this; }
	}
	public partial class IfcRelConnectsStructuralMember : IfcRelConnects
	{
		internal int mRelatingStructuralMember;// : IfcStructuralMember;
		internal int mRelatedStructuralConnection;// : IfcStructuralConnection;
		internal int mAppliedCondition;// : OPTIONAL IfcBoundaryCondition;
		internal int mAdditionalConditions;// : OPTIONAL IfcStructuralConnectionCondition;
		private double mSupportedLength;// : OPTIONAL IfcLengthMeasure;
		internal int mConditionCoordinateSystem; // : OPTIONAL IfcAxis2Placement3D; 

		public IfcStructuralMember RelatingStructuralMember { get { return mDatabase.mIfcObjects[mRelatingStructuralMember] as IfcStructuralMember; } }
		public IfcStructuralConnection RelatedStructuralConnection { get { return mDatabase.mIfcObjects[mRelatedStructuralConnection] as IfcStructuralConnection; } }
		public IfcBoundaryCondition AppliedCondition { get { return mDatabase.mIfcObjects[mAppliedCondition] as IfcBoundaryCondition; } set { mAppliedCondition = (value == null ? 0 : value.mIndex); } }
		public IfcStructuralConnectionCondition AdditionalConditions { get { return mDatabase.mIfcObjects[mAdditionalConditions] as IfcStructuralConnectionCondition; } set { mAdditionalConditions = (value == null ? 0 : value.mIndex); } }
		public double SupportedLength { get { return mSupportedLength; } set { mSupportedLength = value; } }
		public IfcAxis2Placement3D ConditionCoordinateSystem { get { return mDatabase.mIfcObjects[mConditionCoordinateSystem] as IfcAxis2Placement3D; } set { mConditionCoordinateSystem = (value == null ? 0 : value.mIndex); } }

		internal IfcRelConnectsStructuralMember() : base() { }
		internal IfcRelConnectsStructuralMember(IfcRelConnectsStructuralMember i) : base(i) { mRelatingStructuralMember = i.mRelatingStructuralMember; mRelatedStructuralConnection = i.mRelatedStructuralConnection; mAppliedCondition = i.mAppliedCondition; mAdditionalConditions = i.mAdditionalConditions; mSupportedLength = i.mSupportedLength; mConditionCoordinateSystem = i.mConditionCoordinateSystem; }
		internal IfcRelConnectsStructuralMember(IfcStructuralMember member, IfcStructuralConnection connection)
			: base(member.mDatabase)
		{
			mRelatingStructuralMember = member.mIndex;
			member.mConnectedBy.Add(this);
			mRelatedStructuralConnection = connection.mIndex;
			connection.mConnectsStructuralMembers.Add(this);
		}

		internal static IfcRelConnectsStructuralMember Parse(string strDef) { IfcRelConnectsStructuralMember i = new IfcRelConnectsStructuralMember(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsStructuralMember i, List<string> arrFields, ref int ipos)
		{
			IfcRelConnects.parseFields(i, arrFields, ref ipos);
			i.mRelatingStructuralMember = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mRelatedStructuralConnection = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mAppliedCondition = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mAdditionalConditions = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mSupportedLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			i.mConditionCoordinateSystem = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingStructuralMember) + "," + ParserSTEP.LinkToString(mRelatedStructuralConnection) + "," + ParserSTEP.LinkToString(mAppliedCondition) + "," + ParserSTEP.LinkToString(mAdditionalConditions) + "," + ParserSTEP.DoubleToString(mSupportedLength) + "," + ParserSTEP.LinkToString(mConditionCoordinateSystem); }
		internal override void relate()
		{
			IfcStructuralMember m = mDatabase.mIfcObjects[mRelatingStructuralMember] as IfcStructuralMember;
			if (m != null)
				m.mConnectedBy.Add(this);
			IfcStructuralConnection c = mDatabase.mIfcObjects[mRelatedStructuralConnection] as IfcStructuralConnection;
			if (c != null)
				c.mConnectsStructuralMembers.Add(this);
		}
	}
	public class IfcRelConnectsWithEccentricity : IfcRelConnectsStructuralMember
	{
		internal int mConnectionConstraint;// : IfcConnectionGeometry
		internal IfcConnectionGeometry ConnectionConstraint { get { return mDatabase.mIfcObjects[mConnectionConstraint] as IfcConnectionGeometry; } set { mConnectionConstraint = value.mIndex; } }

		internal IfcRelConnectsWithEccentricity() : base() { }
		internal IfcRelConnectsWithEccentricity(IfcRelConnectsWithEccentricity c) : base(c) { mConnectionConstraint = c.mConnectionConstraint; }
		internal IfcRelConnectsWithEccentricity(IfcStructuralMember memb, IfcStructuralConnection connection, IfcConnectionGeometry cg)
			: base(memb, connection) { mConnectionConstraint = cg.mIndex; }
		internal new static IfcRelConnectsWithEccentricity Parse(string strDef) { IfcRelConnectsWithEccentricity i = new IfcRelConnectsWithEccentricity(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsWithEccentricity i, List<string> arrFields, ref int ipos) { IfcRelConnectsStructuralMember.parseFields(i, arrFields, ref ipos); i.mConnectionConstraint = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mConnectionConstraint); }
	}
	public class IfcRelConnectsWithRealizingElements : IfcRelConnectsElements
	{
		internal List<int> mRealizingElements = new List<int>();// :	SET [1:?] OF IfcElement;
		internal string mConnectionType = "$";// : :	OPTIONAL IfcLabel; 
		internal IfcRelConnectsWithRealizingElements() : base() { }
		internal IfcRelConnectsWithRealizingElements(IfcRelConnectsWithRealizingElements c) : base(c) { mConnectionGeometry = c.mConnectionGeometry; mRelatingElement = c.mRelatingElement; mRelatedElement = c.mRelatedElement; }
		internal IfcRelConnectsWithRealizingElements(IfcConnectionGeometry cg, IfcElement relating, IfcElement related, IfcElement realizing, string connection)
			: base(relating, related)
		{
			ConnectionGeometry = cg;
			mRealizingElements.Add(realizing.mIndex);
			if (!string.IsNullOrEmpty(connection))
				mConnectionType = connection.Replace("'", "");
		}
		internal new static IfcRelConnectsWithRealizingElements Parse(string strDef) { IfcRelConnectsWithRealizingElements i = new IfcRelConnectsWithRealizingElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsWithRealizingElements i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRealizingElements = ParserSTEP.SplitListLinks(arrFields[ipos++]); i.mConnectionType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString()
		{
			if (mRealizingElements.Count == 0)
				return "";
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mRealizingElements[0]);
			for (int icounter = 1; icounter < mRealizingElements.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRealizingElements[icounter]);
			return str + (mConnectionType == "$" ? "),$" : "),'" + mConnectionType + "'");
		}
		internal override void relate()
		{
			for (int icounter = 0; icounter < mRealizingElements.Count; icounter++)
			{
				IfcElement e = mDatabase.mIfcObjects[mRealizingElements[icounter]] as IfcElement;
				if (e != null)
					e.mIsConnectionRealization.Add(this);
			}
			base.relate();
		}
	}
	public partial class IfcRelContainedInSpatialStructure : IfcRelConnects
	{
		internal List<int> mRelatedElements = new List<int>();// : SET [1:?] OF IfcProduct;
		private int mRelatingStructure;//  IfcSpatialElement 

		public List<IfcProduct> RelatedElements { get { return (mRelatedElements.Count == 0 ? new List<IfcProduct>() : mRelatedElements.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcProduct)); } }
		public IfcSpatialElement RelatingStructure { get { return mDatabase.mIfcObjects[mRelatingStructure] as IfcSpatialElement; } }

		internal IfcRelContainedInSpatialStructure() : base() { }
		internal IfcRelContainedInSpatialStructure(IfcRelContainedInSpatialStructure i) : base(i) { mRelatedElements = new List<int>(i.mRelatedElements.ToArray()); mRelatingStructure = i.mRelatingStructure; }
		internal IfcRelContainedInSpatialStructure(IfcSpatialElement e) : base(e.mDatabase)
		{
			string containerName = "";
			if (e as IfcBuilding != null)
				containerName = "Building";
			else if (e as IfcBuildingStorey != null)
				containerName = "BuildingStorey";
			else if (e as IfcExternalSpatialElement != null)
				containerName = "ExternalSpatialElement";
			else if (e as IfcSite != null)
				containerName = "Site";
			else if (e as IfcSpace != null)
				containerName = "Space";
			Name = containerName;
			Description = containerName + " Container for Elements";
			mRelatingStructure = e.mIndex;
			e.mContainsElements.Add(this);
		}
		protected override string BuildString()
		{
			if (mRelatedElements.Count <= 0)
				return "";
			string list = "";
			int icounter;
			if (mRelatedElements.Count > 100)
			{
				StringBuilder sb = new StringBuilder();
				for (icounter = 0; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase.mIfcObjects[mRelatedElements[icounter]].ToString()))
					{
						sb.Append(",(#" + mRelatedElements[0]);
						break;
					}
				}
				for (icounter++; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase.mIfcObjects[mRelatedElements[icounter]].ToString()))
						sb.Append(",#" + mRelatedElements[icounter]);
				}
				list = sb.ToString();
			}
			else
			{
				for (icounter = 0; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase.mIfcObjects[mRelatedElements[icounter]].ToString()))
					{
						list = ",(#" + mRelatedElements[0];
						break;
					}

				}
				for (icounter++; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase.mIfcObjects[mRelatedElements[icounter]].ToString()))
						list += ",#" + mRelatedElements[icounter];
				}
			}
			return base.BuildString() + list + "),#" + mRelatingStructure;
		}
		internal static IfcRelContainedInSpatialStructure Parse(string strDef)
		{
			IfcRelContainedInSpatialStructure c = new IfcRelContainedInSpatialStructure();
			int pos = 0;
			c.parseString(strDef, ref pos);
			c.mRelatedElements = ParserSTEP.StripListLink(strDef, ref pos);
			c.mRelatingStructure = ParserSTEP.StripLink(strDef, ref pos);
			return c;
		}
		internal override void relate()
		{
			BaseClassIfc ent = mDatabase.mIfcObjects[mRelatingStructure];
			IfcSpatialElement se = ent as IfcSpatialElement;
			if (se != null)
				se.mContainsElements.Add(this);//.relateStructure(contains);
			for (int icounter = 0; icounter < mRelatedElements.Count; icounter++)
			{
				IfcProduct p = mDatabase.mIfcObjects[mRelatedElements[icounter]] as IfcProduct;
				IfcElement e = p as IfcElement;
				if (e != null)
					e.mContainedInStructure = this;
				else
				{
					IfcGrid g = ent as IfcGrid;
					if (g != null)
						g.mContainedInStructure = this;
				}
			}
		}
		internal List<IfcBuildingElement> getBuildingElements()
		{
			List<IfcProduct> ps = RelatedElements;
			List<IfcBuildingElement> result = new List<IfcBuildingElement>(ps.Count);
			for (int icounter = 0; icounter < ps.Count; icounter++)
			{
				IfcBuildingElement be = ps[icounter] as IfcBuildingElement;
				if (be != null)
					result.Add(be);
			}
			return result;
		}
		internal void removeObject(IfcElement e)
		{
			e.mContainedInStructure = null;
			mRelatedElements.Remove(e.mIndex);
		}
	}
	public partial class IfcRelCoversBldgElements : IfcRelConnects //IFC4 DEPRECATION  The relationship IfcRelCoversBldgElements shall not be used anymore, use IfcRelAggregates instead.
	{
		internal int mRelatingBuildingElement;// :	IfcElement;  
		private List<int> mRelatedCoverings = new List<int>();// : SET [1:?] OF IfcCovering;

		public IfcElement RelatingBuildingElement { get { return mDatabase.mIfcObjects[mRelatingBuildingElement] as IfcElement; } }
		public List<IfcCovering> RelatedCoverings { get { return mRelatedCoverings.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcCovering); } }

		internal IfcRelCoversBldgElements() : base() { }
		internal IfcRelCoversBldgElements(IfcRelCoversBldgElements c) : base(c) { mRelatingBuildingElement = c.mRelatingBuildingElement; mRelatedCoverings = new List<int>(c.mRelatedCoverings.ToArray()); }
		internal IfcRelCoversBldgElements(IfcElement e, IfcCovering covering) : base(e.mDatabase)
		{
			mRelatingBuildingElement = e.mIndex;
			e.mHasCoverings.Add(this);
			if (covering != null)
			{
				mRelatedCoverings.Add(covering.mIndex);
				covering.mCoversElements = this;
			}
		}
		internal IfcRelCoversBldgElements(IfcElement e, List<IfcCovering> coverings) : base(e.mDatabase)
		{
			mRelatingBuildingElement = e.mIndex;
			e.mHasCoverings.Add(this);
			for (int icounter = 0; icounter < coverings.Count; icounter++)
			{
				mRelatedCoverings.Add(coverings[icounter].mIndex);
				coverings[icounter].mCoversElements = this;
			}
		}
		internal static IfcRelCoversBldgElements Parse(string strDef) { IfcRelCoversBldgElements i = new IfcRelCoversBldgElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelCoversBldgElements i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingBuildingElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedCoverings = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingBuildingElement) + ",(";
			if (mRelatedCoverings.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRelatedCoverings[0]);
				for (int icounter = 1; icounter < mRelatedCoverings.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRelatedCoverings[icounter]);
			}
			else
				return "";
			return str + ")";
		}
		internal override void relate()
		{
			IfcBuildingElement be = mDatabase.mIfcObjects[mRelatingBuildingElement] as IfcBuildingElement;
			if (be != null)
				be.mHasCoverings.Add(this);
			for (int icounter = 0; icounter < mRelatedCoverings.Count; icounter++)
			{
				IfcCovering cov = mDatabase.mIfcObjects[mRelatedCoverings[icounter]] as IfcCovering;
				if (cov != null)
					cov.mCoversElements = this;
			}
		}
		internal void Remove(IfcCovering c) { mRelatedCoverings.Remove(c.mIndex); c.mHasCoverings.Remove(this); }
		internal void addCovering(IfcCovering c) { c.mCoversElements = this; mRelatedCoverings.Add(c.mIndex); }
	}
	public partial class IfcRelCoversSpaces : IfcRelConnects //IFC4 DEPRECATION  The relationship IfcRelCoversSpace shall not be used anymore, use IfcRelContainedInSpatialStructure instead.
	{
		internal int mRelatedSpace;// : IfcSpace;
		internal List<int> mRelatedCoverings = new List<int>();// SET [1:?] OF IfcCovering; 

		internal IfcSpace RelatedSpace { get { return mDatabase.mIfcObjects[mRelatedSpace] as IfcSpace; } }
		internal List<IfcCovering> RelatedCoverings { get { return mRelatedCoverings.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcCovering); } }

		internal IfcRelCoversSpaces() : base() { }
		internal IfcRelCoversSpaces(IfcRelCoversSpaces c) : base(c) { mRelatedSpace = c.mRelatedSpace; mRelatedCoverings = new List<int>(c.mRelatedCoverings.ToArray()); }
		internal IfcRelCoversSpaces(IfcSpace s, IfcCovering covering) : base(s.mDatabase)
		{
			mRelatedSpace = s.mIndex;
			s.mHasCoverings.Add(this);
			if (covering != null)
			{
				mRelatedCoverings.Add(covering.mIndex);
				covering.mCoversSpaces = this;
			}
		}
		internal static IfcRelCoversSpaces Parse(string strDef) { IfcRelCoversSpaces i = new IfcRelCoversSpaces(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelCoversSpaces i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatedSpace = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedCoverings = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			if (mRelatedCoverings.Count == 0)
				return "";
			string str = base.BuildString() + ",#" + mRelatedSpace + ",(#" + mRelatedCoverings[0];
			for (int icounter = 1; icounter < mRelatedCoverings.Count; icounter++)
				str += ",#" + mRelatedCoverings[icounter];
			return str + ")";
		}
	
		internal void addCovering(IfcCovering c) { c.mCoversSpaces = this; mRelatedCoverings.Add(c.mIndex); }
		internal override void relate()
		{
			IfcSpace s = (IfcSpace)mDatabase.mIfcObjects[mRelatedSpace];
			s.mHasCoverings.Add(this);
			for (int icounter = 0; icounter < mRelatedCoverings.Count; icounter++)
			{
				IfcCovering cov = mDatabase.mIfcObjects[mRelatedCoverings[icounter]] as IfcCovering;
				if (cov != null)
					cov.mCoversSpaces = this;
			}
		}
	}
	public partial class IfcRelDeclares : IfcRelationship //IFC4
	{
		private int mRelatingContext;// : 	IfcContext;
		private List<int> mRelatedDefinitions = new List<int>();// :	SET [1:?] OF IfcDefinitionSelect; 

		internal IfcContext RelatingContext { get { return mDatabase.mIfcObjects[mRelatingContext] as IfcContext; } }
		public List<IfcDefinitionSelect> RelatedDefinitions { get { return mRelatedDefinitions.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcDefinitionSelect); } }

		internal IfcRelDeclares() : base() { }
		internal IfcRelDeclares(IfcRelDeclares d) : base(d) { mRelatingContext = d.mRelatingContext; mRelatedDefinitions.AddRange(d.mRelatedDefinitions); }
		internal IfcRelDeclares(IfcContext c) : base(c.mDatabase) { mRelatingContext = c.mIndex; c.mDeclares.Add(this); }
		public IfcRelDeclares(IfcContext c, IfcDefinitionSelect def) : this(c, new List<IfcDefinitionSelect>() { def }) { }
		internal IfcRelDeclares(IfcContext c, List<IfcDefinitionSelect> defs) : this(c)
		{
			for (int icounter = 0; icounter < defs.Count; icounter++)
			{
				mRelatedDefinitions.Add(defs[icounter].Index);
				defs[icounter].HasContext = this;
			}
		}
		internal void AddRelated(IfcDefinitionSelect d) { mRelatedDefinitions.Add(d.Index); d.HasContext = this; }
		internal void RemoveRelated(IfcDefinitionSelect d) { mRelatedDefinitions.Remove(d.Index); d.HasContext = null; }
		internal static IfcRelDeclares Parse(string strDef) { IfcRelDeclares i = new IfcRelDeclares(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelDeclares i, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(i, arrFields, ref ipos); i.mRelatingContext = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedDefinitions = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			if (mDatabase.mSchema == Schema.IFC2x3 || mRelatingContext == 0 || mRelatedDefinitions.Count == 0)
				return "";
			string str = ",(" + ParserSTEP.LinkToString(mRelatedDefinitions[0]);
			for (int icounter = 1; icounter < mRelatedDefinitions.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedDefinitions[icounter]);
			return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingContext) + str + ")";
		}
		internal override void relate()
		{
			RelatingContext.mDeclares.Add(this);
			for (int icounter = 0; icounter < mRelatedDefinitions.Count; icounter++)
				(mDatabase.mIfcObjects[mRelatedDefinitions[icounter]] as IfcDefinitionSelect).HasContext = this;
			base.relate();
		}
	}
	public abstract class IfcRelDecomposes : IfcRelationship //ABSTACT  SUPERTYPE OF (ONEOF (IfcRelAggregates ,IfcRelNests ,IfcRelProjectsElement ,IfcRelVoidsElement))
	{
		protected IfcRelDecomposes() : base() { }
		protected IfcRelDecomposes(IfcRelDecomposes d) : base(d) { }
		internal IfcRelDecomposes(DatabaseIfc m) : base(m) { }
		internal static void parseFields(IfcRelDecomposes d, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(d, arrFields, ref ipos); }
	}
	public abstract partial class IfcRelDefines : IfcRelationship // ABSTRACT SUPERTYPE OF (ONEOF(IfcRelDefinesByProperties,IfcRelDefinesByType))
	{
		protected IfcRelDefines() : base() { }
		protected IfcRelDefines(IfcRelDefines d) : base(d) { }
		protected IfcRelDefines(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcRelDefines r, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(r, arrFields, ref ipos); }
	}
	public partial class IfcRelDefinesByObject : IfcRelDefines
	{
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObject;
		internal int mRelatingObject;// : IfcObject  

		internal List<IfcObject> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcObject); } }
		internal IfcObject RelatingObject { get { return mDatabase.mIfcObjects[mRelatingObject] as IfcObject; } }

		internal IfcRelDefinesByObject() : base() { }
		internal IfcRelDefinesByObject(IfcRelDefinesByObject d) : base(d) { mRelatedObjects = new List<int>(d.mRelatedObjects.ToArray()); mRelatingObject = d.mRelatingObject; }
		internal IfcRelDefinesByObject(IfcObject relObj) : base(relObj.mDatabase) { mRelatingObject = relObj.mIndex; relObj.mIsDeclaredBy = this; }
		protected override string BuildString()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingObject);
		}
		internal static void parseFields(IfcRelDefinesByObject t, List<string> arrFields, ref int ipos) { IfcRelDefines.parseFields(t, arrFields, ref ipos); t.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); t.mRelatingObject = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcRelDefinesByObject Parse(string strDef) { IfcRelDefinesByObject t = new IfcRelDefinesByObject(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal override void relate()
		{
			if (mRelatingObject > 0)
			{
				IfcObject ot = mDatabase.mIfcObjects[mRelatingObject] as IfcObject;
				if (ot != null)
					ot.mIsDeclaredBy = this;
			}
			for (int icounter = 0; icounter < mRelatedObjects.Count; icounter++)
			{
				IfcObject o = mDatabase.mIfcObjects[mRelatedObjects[icounter]] as IfcObject;
				if (o != null)
					o.mDeclares.Add(this);
			}
		}
		internal void assign(IfcObject obj) { mRelatedObjects.Add(obj.mIndex); obj.mIsDeclaredBy = this; }
	}
	public partial class IfcRelDefinesByProperties : IfcRelDefines
	{
		private List<int> mRelatedObjects = new List<int>();// IFC4 change	SET [1:1] OF IfcObjectDefinition; ifc2x3 : SET [1:?] OF IfcObject;  
		private int mRelatingPropertyDefinition;// : IfcPropertySetDefinition; 

		public List<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcObjectDefinition); } }
		public IfcPropertySetDefinition RelatingPropertyDefinition { get { return mDatabase.mIfcObjects[mRelatingPropertyDefinition] as IfcPropertySetDefinition; } }

		internal IfcRelDefinesByProperties() : base() { }
		internal IfcRelDefinesByProperties(IfcRelDefinesByProperties d) : base(d) { mRelatedObjects = new List<int>(d.mRelatedObjects.ToArray()); mRelatingPropertyDefinition = d.mRelatingPropertyDefinition; }
		internal IfcRelDefinesByProperties(IfcPropertySetDefinition ifcproperty) : base(ifcproperty.mDatabase) { mRelatingPropertyDefinition = ifcproperty.mIndex; }
		public IfcRelDefinesByProperties(IfcObjectDefinitionSelect od, IfcPropertySetDefinition ifcproperty) : this(new List<IfcObjectDefinitionSelect>() { od }, ifcproperty) { }
		public IfcRelDefinesByProperties(List<IfcObjectDefinitionSelect> objs, IfcPropertySetDefinition ifcproperty) : this(ifcproperty)
		{
			for (int icounter = 0; icounter < objs.Count; icounter++)
			{
				mRelatedObjects.Add(objs[icounter].Index);
				objs[icounter].IsDefinedBy.Add(this);
			}
		}
		private IfcRelDefinesByProperties(DatabaseIfc m) : base(m)
		{
			Name = "NameRelDefinesByProperties";
			Description = "DescriptionRelDefinesByProperties";
		}
		protected override string BuildString()
		{
			if (mRelatedObjects.Count == 0 || RelatingPropertyDefinition == null || string.IsNullOrEmpty(RelatingPropertyDefinition.ToString()))
				return "";
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingPropertyDefinition);
		}
		internal static IfcRelDefinesByProperties Parse(string str)
		{
			IfcRelDefinesByProperties d = new IfcRelDefinesByProperties();
			int pos = 0;
			d.parseString(str, ref pos);
			d.mRelatedObjects = ParserSTEP.StripListLink(str, ref pos);// splitListSTPLinks(arrFields[ipos++]);
			d.mRelatingPropertyDefinition = ParserSTEP.StripLink(str, ref pos);//.parseSTPLink(arrFields[ipos++]);
			return d;
		}

		internal void assign(IfcObjectDefinitionSelect od)
		{
			mRelatedObjects.Add(od.Index);
			od.IsDefinedBy.Add(this);
		}
		internal void assign(List<IfcObjectDefinitionSelect> objs)
		{
			for (int icounter = 0; icounter < objs.Count; icounter++)
			{
				mRelatedObjects.Add(objs[icounter].Index);
				objs[icounter].IsDefinedBy.Add(this);
			}
		}
		internal void unassign(List<IfcObjectDefinitionSelect> objs)
		{
			for (int icounter = 0; icounter < objs.Count; icounter++)
			{
				mRelatedObjects.Remove(objs[icounter].Index);
				objs[icounter].IsDefinedBy.Remove(this);
			}
		}
		internal void RemoveRelated(IfcObjectDefinitionSelect od)
		{
			mRelatedObjects.Remove(od.Index);
			od.IsDefinedBy.Remove(this);
		}
		internal override void relate()
		{
			RelatingPropertyDefinition.DefinesOccurrence = this;
			List<IfcObjectDefinition> related = RelatedObjects;
			for (int icounter = 0; icounter < related.Count; icounter++)
			{
				IfcObject o = related[icounter] as IfcObject;
				if (o != null)
					o.mIsDefinedBy.Add(this);
				else
				{
					IfcContext context = related[icounter] as IfcContext;
					if (context != null)
						context.mIsDefinedBy.Add(this);
				}
			}
		}
	}
	public partial class IfcRelDefinesByType : IfcRelDefines
	{
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObject;
		private int mRelatingType;// : IfcTypeObject  

		internal IfcTypeObject RelatingType { get { return mDatabase.mIfcObjects[mRelatingType] as IfcTypeObject; } }
		internal List<IfcObject> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcObject); } }

		internal IfcRelDefinesByType() : base() { }
		internal IfcRelDefinesByType(IfcRelDefinesByType d) : base(d) { mRelatedObjects = new List<int>(d.mRelatedObjects.ToArray()); mRelatingType = d.mRelatingType; }
		internal IfcRelDefinesByType(IfcTypeObject relType) : base(relType.mDatabase) { mRelatingType = relType.mIndex; relType.mObjectTypeOf = this; }
		internal IfcRelDefinesByType(IfcObject related, IfcTypeObject relating) : this(relating) { mRelatedObjects.Add(related.mIndex); }
		protected override string BuildString()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			IfcTypeObject to = RelatingType;
			if (to == null || string.IsNullOrEmpty(to.ToString()))
				return "";
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingType);
		}
		internal static void parseFields(IfcRelDefinesByType t, List<string> arrFields, ref int ipos) { IfcRelDefines.parseFields(t, arrFields, ref ipos); t.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); t.mRelatingType = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcRelDefinesByType Parse(string strDef) { IfcRelDefinesByType t = new IfcRelDefinesByType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal override void relate()
		{
			if (mRelatingType > 0)
			{
				IfcTypeObject ot = mDatabase.mIfcObjects[mRelatingType] as IfcTypeObject;
				if (ot != null)
					ot.mObjectTypeOf = this;
			}
			for (int icounter = 0; icounter < mRelatedObjects.Count; icounter++)
			{
				IfcObject o = mDatabase.mIfcObjects[mRelatedObjects[icounter]] as IfcObject;
				if (o != null)
					o.IsTypedBy = this;
			}
		}
		internal void assignObj(IfcObject obj)
		{
			mRelatedObjects.Add(obj.mIndex);
			if (obj.IsTypedBy != null)
				obj.IsTypedBy.mRelatedObjects.Remove(obj.mIndex);
			obj.IsTypedBy = this;
		}
	}
	public class IfcRelFillsElement : IfcRelConnects
	{
		private int mRelatingOpeningElement;// : IfcOpeningElement;
		private int mRelatedBuildingElement;// :OPTIONAL IfcElement; 

		internal IfcOpeningElement RelatingOpeningElement { get { return mDatabase.mIfcObjects[mRelatingOpeningElement] as IfcOpeningElement; } }
		internal IfcElement RelatedBuildingElement { get { return mDatabase.mIfcObjects[mRelatedBuildingElement] as IfcElement; } }

		internal IfcRelFillsElement() : base() { }
		internal IfcRelFillsElement(IfcRelFillsElement f) : base(f) { mRelatingOpeningElement = f.mRelatingOpeningElement; mRelatedBuildingElement = f.mRelatedBuildingElement; }
		internal IfcRelFillsElement(IfcOpeningElement oe, IfcElement e) : base(oe.mDatabase) { mRelatingOpeningElement = oe.mIndex; mRelatedBuildingElement = e.mIndex; }
		internal static IfcRelFillsElement Parse(string strDef) { IfcRelFillsElement i = new IfcRelFillsElement(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelFillsElement i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingOpeningElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedBuildingElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingOpeningElement) + "," + ParserSTEP.LinkToString(mRelatedBuildingElement); }
		internal override void relate() { RelatedBuildingElement.mFillsVoids.Add(this); RelatingOpeningElement.mHasFillings.Add(this); base.relate(); }
	}
	public class IfcRelFlowControlElements : IfcRelConnects
	{
		internal int mRelatingPort;// : IfcPort;
		internal int mRelatedElement;// : IfcElement; 
		internal IfcRelFlowControlElements() : base() { }
		internal IfcRelFlowControlElements(IfcRelFlowControlElements i) : base(i) { mRelatingPort = i.mRelatingPort; mRelatedElement = i.mRelatedElement; }
		internal static IfcRelFlowControlElements Parse(string strDef) { IfcRelFlowControlElements i = new IfcRelFlowControlElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelFlowControlElements i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingPort = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedElement); }
	}
	//ENTITY IfcRelInteractionRequirements  // DEPRECEATED IFC4
	public partial class IfcRelNests : IfcRelDecomposes
	{
		internal int mRelatingObject;// : IfcObjectDefinition 
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObjectDefinition; 

		internal IfcObjectDefinition RelatingObject { get { return mDatabase.mIfcObjects[mRelatingObject] as IfcObjectDefinition; } }
		internal List<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcObjectDefinition); } }

		internal IfcRelNests() : base() { }
		internal IfcRelNests(IfcRelNests n) : base(n) { mRelatingObject = n.mRelatingObject; mRelatedObjects = new List<int>(n.mRelatedObjects.ToArray()); }
		public IfcRelNests(IfcObjectDefinition relatingObject) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject.mIndex;
			relatingObject.mIsNestedBy.Add(this);
		}
		internal IfcRelNests(IfcObjectDefinition relatingObject, IfcObjectDefinition relatedObject) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject.mIndex;
			mRelatedObjects.Add(relatedObject.mIndex);
			relatingObject.mIsNestedBy.Add(this);
			relatedObject.mNests = this;
		}
		internal IfcRelNests(IfcObjectDefinition relatingObject, IfcObjectDefinition ro, IfcObjectDefinition ro2) : this(relatingObject, ro) { mRelatedObjects.Add(ro2.mIndex); ro2.mNests = this; ; }
		internal IfcRelNests(IfcObjectDefinition relatingObject, List<IfcObjectDefinition> relatedObjects) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject.mIndex;
			relatingObject.mIsNestedBy.Add(this);
			for (int icounter = 0; icounter < relatedObjects.Count; icounter++)
			{
				mRelatedObjects.Add(relatedObjects[icounter].mIndex);
				relatedObjects[icounter].mNests = this;
			}
		}
		internal static IfcRelNests Parse(string strDef) { IfcRelNests a = new IfcRelNests(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelNests a, List<string> arrFields, ref int ipos) { IfcRelDecomposes.parseFields(a, arrFields, ref ipos); a.mRelatingObject = ParserSTEP.ParseLink(arrFields[ipos++]); a.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = "";
			if (mRelatedObjects.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRelatedObjects[0]);
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			}
			else
				return "";
			return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingObject) + ",(" + str + ")";
		}
		internal override void relate()
		{
			IfcObjectDefinition relating = RelatingObject;
			relating.relateNested(this);
			List<IfcObjectDefinition> ods = RelatedObjects;
			foreach (IfcObjectDefinition od in ods)
			{
				if (od == null)
					continue;
				od.mNests = this;
				IfcDistributionPort dp = od as IfcDistributionPort;
				if (dp != null)
				{
					IfcFlowSegment fs = relating as IfcFlowSegment;
					if (fs != null)
					{
						if (dp.mFlowDirection == IfcFlowDirectionEnum.SOURCE)
							fs.mSourcePort = dp;
						else if (dp.mFlowDirection == IfcFlowDirectionEnum.SINK)
							fs.mSinkPort = dp;
					}
				}
			}
		}
		internal void addObject(IfcObjectDefinition o)
		{
			o.mNests = this;
			if (!mRelatedObjects.Contains(o.mIndex))
				mRelatedObjects.Add(o.mIndex);
		}
	}
	//ENTITY IfcRelOccupiesSpaces // DEPRECEATED IFC4
	//ENTITY IfcRelOverridesProperties // DEPRECEATED IFC4
	public class IfcRelProjectsElement : IfcRelDecomposes // IFC2x3 IfcRelDecomposes
	{
		internal int mRelatingElement;// : IfcElement; 
		internal int mRelatedFeatureElement;// : IfcFeatureElementAddition

		internal IfcElement RelatingElement { get { return mDatabase.mIfcObjects[mRelatingElement] as IfcElement; } }
		internal IfcFeatureElementAddition RelatedFeatureElement { get { return mDatabase.mIfcObjects[mRelatedFeatureElement] as IfcFeatureElementAddition; } }

		protected IfcRelProjectsElement() : base() { }
		protected IfcRelProjectsElement(IfcRelProjectsElement p) : base(p) { mRelatingElement = p.mRelatingElement; mRelatedFeatureElement = p.mRelatedFeatureElement; }
		protected IfcRelProjectsElement(IfcElement e, IfcFeatureElementAddition a) : base(e.mDatabase) { mRelatingElement = e.mIndex; mRelatedFeatureElement = a.mIndex; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedFeatureElement); }
		protected static void parseFields(IfcRelProjectsElement c, List<string> arrFields, ref int ipos) { IfcRelDecomposes.parseFields(c, arrFields, ref ipos); c.mRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); c.mRelatedFeatureElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal override void relate()
		{
			base.relate();
			RelatingElement.mHasProjections.Add(this);
			RelatedFeatureElement.mProjectsElements.Add(this);
		}
	}
	//ENTITY IfcRelReferencedInSpatialStructure
	//ENTITY IfcRelSchedulesCostItems // DEPRECEATED IFC4 
	public class IfcRelSequence : IfcRelConnects
	{
		internal int mRelatingProcess;// : IfcProcess;
		internal int mRelatedProcess;//  IfcProcess;
		internal double mTimeLag;// : OPTIONAL IfcLagTime; IFC2x3 	IfcTimeMeasure
		internal IfcSequenceEnum mSequenceType = IfcSequenceEnum.NOTDEFINED;//	 :	OPTIONAL IfcSequenceEnum;
		internal string mUserDefinedSequenceType = "$";//	 :	OPTIONAL IfcLabel; 
		internal IfcRelSequence() : base() { }
		internal IfcRelSequence(IfcRelSequence s) : base(s) { mRelatingProcess = s.mRelatingProcess; mRelatedProcess = s.mRelatedProcess; mTimeLag = s.mTimeLag; mSequenceType = s.mSequenceType; mUserDefinedSequenceType = s.mUserDefinedSequenceType; }
		internal IfcRelSequence(IfcProcess rg, IfcProcess rd, IfcLagTime lag, IfcSequenceEnum st, string userSeqType) : base(rg.mDatabase)
		{
			mRelatingProcess = rg.mIndex;
			mRelatedProcess = rd.mIndex;
			if (lag != null)
				mTimeLag = (mDatabase.mSchema == Schema.IFC2x3 ? (int)lag.getSecondsDuration() : lag.mIndex);
			mSequenceType = st;
			if (!string.IsNullOrEmpty(userSeqType))
				mUserDefinedSequenceType = userSeqType.Replace("'", "");
		}
		internal static IfcRelSequence Parse(string strDef,Schema schema) { IfcRelSequence i = new IfcRelSequence(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return i; }
		internal static void parseFields(IfcRelSequence i, List<string> arrFields, ref int ipos,Schema schema)
		{
			IfcRelConnects.parseFields(i, arrFields, ref ipos);
			i.mRelatingProcess = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mRelatedProcess = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (schema == Schema.IFC2x3)
				i.mTimeLag = ParserSTEP.ParseDouble(arrFields[ipos++]);
			else
				i.mTimeLag = ParserSTEP.ParseLink(arrFields[ipos++]);
			string s = arrFields[ipos++];
			if (s != "$")
				i.mSequenceType = (IfcSequenceEnum)Enum.Parse(typeof(IfcSequenceEnum), s.Replace(".", ""));
			if (schema != Schema.IFC2x3)
				i.mUserDefinedSequenceType = arrFields[ipos++];
		}
		protected override string BuildString()
		{
			return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingProcess) + "," + ParserSTEP.LinkToString(mRelatedProcess) + "," + (mDatabase.mSchema == Schema.IFC2x3 ? ParserSTEP.DoubleToString(mTimeLag) :
				ParserSTEP.LinkToString((int)mTimeLag)) + ",." + mSequenceType + (mDatabase.mSchema == Schema.IFC2x3 ? "." : (mUserDefinedSequenceType == "$" ? ".,$" : ".,'" + mUserDefinedSequenceType + "'"));
		}
		internal override void relate()
		{
			IfcProcess p = mDatabase.mIfcObjects[mRelatingProcess] as IfcProcess, s = mDatabase.mIfcObjects[mRelatedProcess] as IfcProcess;
			p.mIsPredecessorTo.Add(this);
			s.mIsSuccessorFrom.Add(this);
		}
		internal IfcProcess getPredecessor() { return mDatabase.mIfcObjects[mRelatingProcess] as IfcProcess; }
		internal IfcProcess getSuccessor() { return mDatabase.mIfcObjects[mRelatedProcess] as IfcProcess; }
		internal TimeSpan getLag()
		{
			if (mDatabase.mSchema == Schema.IFC2x3) return new TimeSpan(0, 0, (int)mTimeLag);
			IfcLagTime lt = mDatabase.mIfcObjects[(int)mTimeLag] as IfcLagTime;
			return (lt == null ? new TimeSpan(0, 0, 0) : lt.getLag());
		}
	}
	public partial class IfcRelServicesBuildings : IfcRelConnects
	{
		internal int mRelatingSystem;// : IfcSystem;
		internal List<int> mRelatedBuildings = new List<int>();// : SET [1:?] OF IfcSpatialElement  ;

		public IfcSystem RelatingSystem { get { return mDatabase.mIfcObjects[mRelatingSystem] as IfcSystem; } set { mRelatingSystem = value.mIndex; } }
		public List<IfcSpatialElement> RelatedBuildings { get { return mRelatedBuildings.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcSpatialElement);  } }

		internal IfcRelServicesBuildings() : base() { }
		internal IfcRelServicesBuildings(IfcRelServicesBuildings s) : base(s) { mRelatingSystem = s.mRelatingSystem; mRelatedBuildings = new List<int>(s.mRelatedBuildings.ToArray()); }
		internal IfcRelServicesBuildings(IfcSystem sys, IfcSpatialElement se)
			: base(sys.mDatabase) { mRelatingSystem = sys.mIndex; mRelatedBuildings.Add(se.mIndex); se.mServicedBySystems.Add(this); }
		internal static IfcRelServicesBuildings Parse(string strDef) { IfcRelServicesBuildings i = new IfcRelServicesBuildings(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelServicesBuildings i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingSystem = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedBuildings = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			if (mRelatedBuildings.Count == 0)
				return "";
			string str = base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingSystem) + ",(" + ParserSTEP.LinkToString(mRelatedBuildings[0]);
			for (int icounter = 1; icounter < mRelatedBuildings.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedBuildings[icounter]);
			return str + ")";
		}
		internal override void relate()
		{
			IfcSystem sys = (IfcSystem)mDatabase.mIfcObjects[mRelatingSystem];
			sys.mServicesBuildings = this;
			for (int icounter = 0; icounter < mRelatedBuildings.Count; icounter++)
			{
				IfcSpatialStructureElement se = mDatabase.mIfcObjects[mRelatedBuildings[icounter]] as IfcSpatialStructureElement;
				if (se != null)
					se.mServicedBySystems.Add(this);
			}
		}
	}
	public partial class IfcRelSpaceBoundary : IfcRelConnects
	{
		internal int mRelatingSpace;// :	IfcSpaceBoundarySelect; : IfcSpace;
		internal int mRelatedBuildingElement;// :OPTIONAL IfcElement;
		internal int mConnectionGeometry;// : OPTIONAL IfcConnectionGeometry;
		internal IfcPhysicalOrVirtualEnum mPhysicalOrVirtualBoundary = IfcPhysicalOrVirtualEnum.NOTDEFINED;// : IfcPhysicalOrVirtualEnum;
		internal IfcInternalOrExternalEnum mInternalOrExternalBoundary = IfcInternalOrExternalEnum.NOTDEFINED;// : IfcInternalOrExternalEnum; 

		internal IfcSpaceBoundarySelect RelatingSpace { get { return mDatabase.mIfcObjects[mRelatingSpace] as IfcSpaceBoundarySelect; } }
		internal IfcRelSpaceBoundary() : base() { }
		internal IfcRelSpaceBoundary(IfcRelSpaceBoundary p) : base(p) { mRelatingSpace = p.mRelatingSpace; mRelatedBuildingElement = p.mRelatedBuildingElement; mConnectionGeometry = p.mConnectionGeometry; mPhysicalOrVirtualBoundary = p.mPhysicalOrVirtualBoundary; mInternalOrExternalBoundary = p.mInternalOrExternalBoundary; }
		internal IfcRelSpaceBoundary(IfcSpaceBoundarySelect s, IfcElement e, IfcConnectionGeometry g, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern) : base(s.Database)
		{
			mRelatingSpace = s.Index;
			s.BoundedBy.Add(this);
			mRelatedBuildingElement = e.mIndex;
			if (g != null)
				mConnectionGeometry = g.mIndex;
			mPhysicalOrVirtualBoundary = virt;
			mInternalOrExternalBoundary = intern;
		}
		internal static IfcRelSpaceBoundary Parse(string strDef) { IfcRelSpaceBoundary i = new IfcRelSpaceBoundary(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelSpaceBoundary i, List<string> arrFields, ref int ipos)
		{
			IfcRelConnects.parseFields(i, arrFields, ref ipos);
			i.mRelatingSpace = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mRelatedBuildingElement = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mConnectionGeometry = ParserSTEP.ParseLink(arrFields[ipos++]);
			string s = arrFields[ipos++];
			if (s != "$")
				i.mPhysicalOrVirtualBoundary = (IfcPhysicalOrVirtualEnum)Enum.Parse(typeof(IfcPhysicalOrVirtualEnum), s.Replace(".", ""));
			s = arrFields[ipos++];
			if (s != "$")
				i.mInternalOrExternalBoundary = (IfcInternalOrExternalEnum)Enum.Parse(typeof(IfcInternalOrExternalEnum), s.Replace(".", ""));
		}
		protected override string BuildString() { return (mRelatedBuildingElement == 0 || mRelatingSpace == 0 ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingSpace) + "," + ParserSTEP.LinkToString(mRelatedBuildingElement) + "," + ParserSTEP.LinkToString(mConnectionGeometry) + ",." + mPhysicalOrVirtualBoundary.ToString() + ".,." + mInternalOrExternalBoundary.ToString() + "."); }
		internal override void relate()
		{
			base.relate();
			IfcSpaceBoundarySelect s = RelatingSpace;
			s.BoundedBy.Add(this);
			IfcElement e = mDatabase.mIfcObjects[mRelatedBuildingElement] as IfcElement;
			if (e != null)
				e.mProvidesBoundaries.Add(this);
		}
	}
	public class IfcRelSpaceBoundary1stLevel : IfcRelSpaceBoundary
	{
		internal int mParentBoundary;// :	IfcRelSpaceBoundary1stLevel;
		//INVERSE	
		internal List<IfcRelSpaceBoundary1stLevel> mInnerBoundaries = new List<IfcRelSpaceBoundary1stLevel>();//	:	SET OF IfcRelSpaceBoundary1stLevel FOR ParentBoundary;

		internal IfcRelSpaceBoundary1stLevel() : base() { }
		internal IfcRelSpaceBoundary1stLevel(IfcRelSpaceBoundary1stLevel p) : base(p) { mParentBoundary = p.mParentBoundary; }
		internal IfcRelSpaceBoundary1stLevel(IfcSpaceBoundarySelect s, IfcElement e, IfcConnectionGeometry g, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern, IfcRelSpaceBoundary1stLevel parent)
			: base(s, e, g, virt, intern) { mParentBoundary = parent.mIndex; }
		internal static new IfcRelSpaceBoundary1stLevel Parse(string strDef) { IfcRelSpaceBoundary1stLevel i = new IfcRelSpaceBoundary1stLevel(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelSpaceBoundary1stLevel i, List<string> arrFields, ref int ipos)
		{
			IfcRelSpaceBoundary.parseFields(i, arrFields, ref ipos);
			i.mParentBoundary = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mParentBoundary); }
		internal override void relate()
		{
			base.relate();
			IfcRelSpaceBoundary1stLevel s = mDatabase.mIfcObjects[mParentBoundary] as IfcRelSpaceBoundary1stLevel;
			s.mInnerBoundaries.Add(this);
		}
	}
	public partial class IfcRelSpaceBoundary2ndLevel : IfcRelSpaceBoundary1stLevel
	{
		internal int mCorrespondingBoundary;// :	IfcRelSpaceBoundary2ndLevel;
		//INVERSE	
		internal List<IfcRelSpaceBoundary2ndLevel> mCorresponds = new List<IfcRelSpaceBoundary2ndLevel>();//	:	SET OF IfcRelSpaceBoundary1stLevel FOR ParentBoundary;

		internal IfcRelSpaceBoundary2ndLevel() : base() { }
		internal IfcRelSpaceBoundary2ndLevel(IfcRelSpaceBoundary2ndLevel p) : base(p) { mCorrespondingBoundary = p.mCorrespondingBoundary; }
		internal IfcRelSpaceBoundary2ndLevel(IfcSpaceBoundarySelect s, IfcElement e, IfcConnectionGeometry g, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern, IfcRelSpaceBoundary1stLevel parent, IfcRelSpaceBoundary2ndLevel corresponding)
			: base(s, e, g, virt, intern, parent) { if (corresponding != null) mCorrespondingBoundary = corresponding.mIndex; }
		internal static new IfcRelSpaceBoundary2ndLevel Parse(string strDef) { IfcRelSpaceBoundary2ndLevel i = new IfcRelSpaceBoundary2ndLevel(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelSpaceBoundary2ndLevel i, List<string> arrFields, ref int ipos)
		{
			IfcRelSpaceBoundary1stLevel.parseFields(i, arrFields, ref ipos);
			i.mCorrespondingBoundary = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mCorrespondingBoundary); }
		internal override void relate()
		{
			base.relate();
			IfcRelSpaceBoundary2ndLevel s = mDatabase.mIfcObjects[mCorrespondingBoundary] as IfcRelSpaceBoundary2ndLevel;
			s.mCorresponds.Add(this);
		}
	}
	public partial class IfcRelVoidsElement : IfcRelDecomposes // Ifc2x3 IfcRelConnects
	{
		private int mRelatingBuildingElement;// : IfcElement;
		private int mRelatedOpeningElement;// : IfcFeatureElementSubtraction; 

		internal IfcElement RelatingBuildingElement { get { return mDatabase.mIfcObjects[mRelatingBuildingElement] as IfcElement; } }
		internal IfcFeatureElementSubtraction RelatedOpeningElement { get { return mDatabase.mIfcObjects[mRelatedOpeningElement] as IfcFeatureElementSubtraction; } }

		internal IfcRelVoidsElement() : base() { }
		internal IfcRelVoidsElement(IfcRelVoidsElement v) : base(v) { mRelatingBuildingElement = v.mRelatingBuildingElement; mRelatedOpeningElement = v.mRelatedOpeningElement; }
		public IfcRelVoidsElement(IfcElement host, IfcFeatureElementSubtraction fes)
			: base(host.mDatabase) { mRelatingBuildingElement = host.mIndex; host.mHasOpenings.Add(this); mRelatedOpeningElement = fes.mIndex; fes.mVoidsElement = this; }
		internal static IfcRelVoidsElement Parse(string strDef) { IfcRelVoidsElement i = new IfcRelVoidsElement(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelVoidsElement i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingBuildingElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedOpeningElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingBuildingElement) + "," + ParserSTEP.LinkToString(mRelatedOpeningElement); }
		internal override void relate()
		{
			IfcElement elem = RelatingBuildingElement;
			if (elem != null)
				elem.mHasOpenings.Add(this);
			IfcFeatureElementSubtraction es = RelatedOpeningElement;
			if (es != null)
				es.mVoidsElement = this;
		}
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
		private List<IfcProductRepresentation> mOfProductRepresentation = new List<IfcProductRepresentation>();/// IFC4 change	 : 	SET [0:n] OF IfcProductRepresentation FOR Representations;

		public IfcRepresentationContext ContextOfItems { get { return mDatabase.mIfcObjects[mContextOfItems] as IfcRepresentationContext; } }
		public string RepresentationIdentifier { get { return mRepresentationIdentifier == "$" ? "" : mRepresentationIdentifier; } set { mRepresentationIdentifier = (string.IsNullOrEmpty(value) ? "$" : value); } }
		public string RepresentationType { get { return mRepresentationType == "$" ? "" : mRepresentationType; } set { mRepresentationType = string.IsNullOrEmpty(value) ? "$" : value; } }
		public List<IfcRepresentationItem> Items { get { return mItems.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcRepresentationItem); } }
		public List<IfcProductRepresentation> OfProductRepresentation { get { return mOfProductRepresentation; } }
		public List<IfcPresentationLayerAssignment> LayerAssignments { get { return mLayerAssignments; } }

		protected IfcRepresentation() : base() { }
		protected IfcRepresentation(IfcRepresentation r) : base() { mContextOfItems = r.mContextOfItems; mRepresentationIdentifier = r.mRepresentationIdentifier; mRepresentationType = r.mRepresentationType; mItems = new List<int>(r.mItems.ToArray()); }
		protected IfcRepresentation(DatabaseIfc m, string identifier, string repType) : base(m)
		{
			RepresentationIdentifier = identifier.Replace("'", "");
			RepresentationType = repType.Replace("'", "");
			if (m.mGeomRepContxt != null)
				mContextOfItems = m.mGeomRepContxt.mIndex;
			if (string.Compare(identifier, "Axis", true) == 0 && m.mGeoRepSubContxtAxis != null)
				mContextOfItems = m.mGeoRepSubContxtAxis.mIndex;
			else if (m.mGeoRepSubContxtBody != null)
				mContextOfItems = m.mGeoRepSubContxtBody.mIndex;
		}
		protected IfcRepresentation(IfcRepresentationItem ri) : this(ri.mDatabase,"","") { mItems.Add(ri.mIndex); }
		protected IfcRepresentation(IfcRepresentationItem ri, string identifier, string repType)
			: this(ri.mDatabase, identifier, repType) { mItems.Add(ri.mIndex); }
		protected IfcRepresentation(List<IfcRepresentationItem> reps, string identifier, string repType)
			: this(reps[0].mDatabase, identifier, repType) { mItems = reps.ConvertAll(x => x.mIndex); }

		internal static IfcRepresentation Parse(string str)
		{
			IfcRepresentation r = new IfcRepresentation();
			int pos = 0;
			parseString(r, str, ref pos);
			return r;
		}
		protected static void parseString(IfcRepresentation r, string str, ref int pos)
		{
			r.mContextOfItems = ParserSTEP.StripLink(str, ref pos);
			r.mRepresentationIdentifier = ParserSTEP.StripString(str, ref pos);
			r.mRepresentationType = ParserSTEP.StripString(str, ref pos);
			r.mItems = ParserSTEP.StripListLink(str, ref pos);
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + ParserSTEP.LinkToString(mContextOfItems) + (mRepresentationIdentifier == "$" ? ",$," : ",'" + mRepresentationIdentifier + "',") + (mRepresentationType == "$" ? "$,(" : "'" + mRepresentationType + "',(");
			if (mItems.Count > 0)
			{
				str += ParserSTEP.LinkToString(mItems[0]);
				for (int icounter = 1; icounter < mItems.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mItems[icounter]);
			}
			return str + ")";
		}
		internal void relate()
		{
			IfcRepresentationContext rc = ContextOfItems;
			if (rc != null)
				rc.RepresentationsInContext.Add(this);
			List<IfcRepresentationItem> items = Items;
			for (int icounter = 0; icounter < items.Count; icounter++)
				items[icounter].mRepresents.Add(this);
		}
	}
	public abstract partial class IfcRepresentationContext : BaseClassIfc
	{
		internal string mContextIdentifier = "$";// : OPTIONAL IfcLabel;
		internal string mContextType = "$";// : OPTIONAL IfcLabel; 
		//INVERSE
		private List<IfcRepresentation> mRepresentationsInContext = new List<IfcRepresentation>();// :	SET OF IfcRepresentation FOR ContextOfItems;

		internal string ContextIdentifier { get { return (mContextIdentifier == "$" ? "" : ParserIfc.Decode(mContextIdentifier)); } set { mContextIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		internal string ContextType { get { return (mContextType == "$" ? "" : ParserIfc.Decode(mContextType)); } set { mContextType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		internal List<IfcRepresentation> RepresentationsInContext { get { return mRepresentationsInContext; } }

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
		protected IfcRepresentationContext(IfcRepresentationContext p) : base() { mContextIdentifier = p.mContextIdentifier; mContextType = p.mContextType; }
		protected IfcRepresentationContext(DatabaseIfc m) : base(m) { }
		internal static void parseFields(IfcRepresentationContext c, List<string> arrFields, ref int ipos) { c.mContextIdentifier = arrFields[ipos++].Replace("'", ""); c.mContextType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + (mContextIdentifier == "$" ? ",$," : ",'" + mContextIdentifier + "',") + (mContextType == "$" ? "$" : "'" + mContextType + "'"); }
	}
	public abstract partial class IfcRepresentationItem : BaseClassIfc, IfcLayeredItem /*(IfcGeometricRepresentationItem,IfcMappedItem,IfcStyledItem,IfcTopologicalRepresentationItem));*/
	{ //INVERSE
		internal List<IfcPresentationLayerAssignment> mLayerAssignments = new List<IfcPresentationLayerAssignment>();// null;
		internal IfcStyledItem mStyledByItem = null;// : SET [0:1] OF IfcStyledItem FOR Item; 

		internal List<IfcRepresentation> mRepresents = new List<IfcRepresentation>();

		public List<IfcPresentationLayerAssignment> LayerAssignments { get { return mLayerAssignments; } }

		protected IfcRepresentationItem() : base() { }
		protected IfcRepresentationItem(IfcRepresentationItem p) : base() { }
		protected IfcRepresentationItem(DatabaseIfc m) : base(m) { }
		
		protected static void parseFields(IfcRepresentationItem i, List<string> arrFields, ref int ipos) { }
		protected virtual void Parse(string str, ref int ipos) { }
	}
	public partial class IfcRepresentationMap : BaseClassIfc, IfcProductRepresentationSelect
	{
		internal int mMappingOrigin;// : IfcAxis2Placement;
		internal int mMappedRepresentation;// : IfcRepresentation;
		//INVERSE
		internal List<IfcShapeAspect> mHasShapeAspects = new List<IfcShapeAspect>();//	:	SET [0:?] OF IfcShapeAspect FOR PartOfProductDefinitionShape;
		internal List<IfcMappedItem> mMapUsage = new List<IfcMappedItem>();//: 	SET OF IfcMappedItem FOR MappingSource;

		public IfcAxis2Placement MappingOrigin { get { return mDatabase.mIfcObjects[mMappingOrigin] as IfcAxis2Placement; } set { mMappingOrigin = value.Index; } }
		public IfcRepresentation MappedRepresentation { get { return mDatabase.mIfcObjects[mMappedRepresentation] as IfcRepresentation; } set { mMappedRepresentation = value.mIndex; } }
		public List<IfcShapeAspect> HasShapeAspects { get { return mHasShapeAspects; } }

		internal List<IfcTypeProduct> mTypeProducts = new List<IfcTypeProduct>();// GG

		internal IfcRepresentationMap() : base() { }
		internal IfcRepresentationMap(IfcRepresentationMap p) : base() { mMappingOrigin = p.mMappingOrigin; mMappedRepresentation = p.mMappedRepresentation; }
		public IfcRepresentationMap(IfcRepresentationItem item) : base(item.mDatabase) { mMappingOrigin = new IfcAxis2Placement3D(item.mDatabase).mIndex; MappedRepresentation = new IfcShapeRepresentation(new List<IfcRepresentationItem>() { item }); }
		public IfcRepresentationMap(IfcAxis2Placement placement, IfcRepresentation representation) : base(representation.mDatabase) { mMappingOrigin = placement.Index; mMappedRepresentation = representation.mIndex; }

		internal static IfcRepresentationMap Parse(string strDef) { IfcRepresentationMap m = new IfcRepresentationMap(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcRepresentationMap rm, List<string> arrFields, ref int ipos) { rm.mMappingOrigin = ParserSTEP.ParseLink(arrFields[ipos++]); rm.mMappedRepresentation = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mMappingOrigin) + "," + ParserSTEP.LinkToString(mMappedRepresentation); }

	}
	public abstract class IfcResource : IfcObject //ABSTRACT SUPERTYPE OF (ONEOF (IfcConstructionResource))
	{	//INVERSE 
		internal List<IfcRelAssignsToResource> mResourceOf = new List<IfcRelAssignsToResource>();// : SET [0:?] OF IfcRelAssignsToResource FOR RelatingResource; 
		protected IfcResource() : base() { }
		protected IfcResource(IfcResource o) : base(o) { }
		protected IfcResource(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcResource r, List<string> arrFields, ref int ipos) { IfcObject.parseFields(r, arrFields, ref ipos); }
	}
	public partial class IfcResourceConstraintRelationship : IfcResourceLevelRelationship  // IfcPropertyConstraintRelationship; // DEPRECEATED IFC4 renamed
	{
		internal int mRelatingConstraint;// :	IfcConstraint;
		internal List<int> mRelatedResourceObjects = new List<int>();// :	SET [1:?] OF IfcResourceObjectSelect;

		internal IfcConstraint RelatingConstraint { get { return mDatabase.mIfcObjects[mRelatingConstraint] as IfcConstraint; } }
		internal List<IfcResourceObjectSelect> RelatedResourceObjects { get { return mRelatedResourceObjects.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcResourceObjectSelect); } }
		internal IfcResourceConstraintRelationship() : base() { }
		internal IfcResourceConstraintRelationship(IfcResourceConstraintRelationship o) : base(o) { mRelatingConstraint = o.mRelatingConstraint; }
		public IfcResourceConstraintRelationship(string name, string description, IfcConstraint constraint, IfcResourceObjectSelect related) : this(name, description, constraint, new List<IfcResourceObjectSelect>() { related }) { }
		public IfcResourceConstraintRelationship(string name, string description, IfcConstraint constraint, List<IfcResourceObjectSelect> related) : base(constraint.mDatabase, name, description) { mRelatingConstraint = constraint.mIndex; mRelatedResourceObjects = related.ConvertAll(x => x.Index); }
		internal static IfcResourceConstraintRelationship Parse(string strDef, Schema schema) { IfcResourceConstraintRelationship a = new IfcResourceConstraintRelationship(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcResourceConstraintRelationship a, List<string> arrFields, ref int ipos, Schema schema) { IfcResourceLevelRelationship.parseFields(a, arrFields, ref ipos,schema); a.mRelatingConstraint = ParserSTEP.ParseLink(arrFields[ipos++]); a.mRelatedResourceObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string result = base.BuildString() + "," + ParserSTEP.LinkToString(mRelatingConstraint) + ",(#" + mRelatedResourceObjects[0];
			for (int icounter = 1; icounter < mRelatedResourceObjects.Count; icounter++)
				result += ",#" + mRelatedResourceObjects[icounter];
			return result + ")";
		}
		internal void relate()
		{
			RelatingConstraint.mPropertiesForConstraint.Add(this);
			List<IfcResourceObjectSelect> related = RelatedResourceObjects;
			for (int icounter = 0; icounter < related.Count; icounter++)
				related[icounter].HasConstraintRelationships.Add(this);
		}
	}
	public abstract class IfcResourceLevelRelationship : BaseClassIfc //IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcApprovalRelationship,
	{ // IfcCurrencyRelationship, IfcDocumentInformationRelationship, IfcExternalReferenceRelationship, IfcMaterialRelationship, IfcOrganizationRelationship, IfcPropertyDependencyRelationship, IfcResourceApprovalRelationship, IfcResourceConstraintRelationship));
		internal string mName = "$";// : OPTIONAL IfcLabel
		internal string mDescription = "$";// : OPTIONAL IfcText; 
		//INVERSE
		//mHasExternalReference

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcResourceLevelRelationship() : base() { }
		protected IfcResourceLevelRelationship(IfcResourceLevelRelationship r) : base() { mDescription = r.mDescription; mName = r.mName; }
		protected IfcResourceLevelRelationship(DatabaseIfc m, string name, string description) : base(m) { Name = name; Description = description; }
		internal static void parseFields(IfcResourceLevelRelationship a, List<string> arrFields, ref int ipos,Schema schema)
		{
			if (schema != Schema.IFC2x3)
			{
				a.mName = arrFields[ipos++].Replace("'", "");
				a.mDescription = arrFields[ipos++].Replace("'", "");
			}
		}
		protected override string BuildString() { return base.BuildString() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'"); }
	}
	public partial interface IfcResourceObjectSelect : IfcInterface //IFC4 SELECT (	IfcPropertyAbstraction, IfcPhysicalQuantity, IfcAppliedValue, 
	{	//IfcContextDependentUnit, IfcConversionBasedUnit, IfcProfileDef, IfcActorRole, IfcApproval, IfcConstraint, IfcTimeSeries, IfcMaterialDefinition, IfcPerson, IfcPersonAndOrganization, IfcOrganization, IfcExternalReference, IfcExternalInformation););
		List<IfcExternalReferenceRelationship> HasExternalReferences { get; }
		List<IfcResourceConstraintRelationship> HasConstraintRelationships { get; } //gg
	}
	public class IfcResourceTime : IfcSchedulingTime //IFC4
	{
		internal string mScheduleWork = "$";//	 :	OPTIONAL IfcDuration;
		internal double mScheduleUsage; //:	OPTIONAL IfcPositiveRatioMeasure;
		internal string mScheduleStart = "$", mScheduleFinish = "$";//:	OPTIONAL IfcDateTime;
		internal string mScheduleContour = "$";//:	OPTIONAL IfcLabel;
		internal string mLevelingDelay = "$";//	 :	OPTIONAL IfcDuration;
		internal bool mIsOverAllocated = false;//	 :	OPTIONAL BOOLEAN;
		internal string mStatusTime = "$";//:	OPTIONAL IfcDateTime;
		internal string mActualWork = "$";//	 :	OPTIONAL IfcDuration; 
		internal double mActualUsage; //:	OPTIONAL IfcPositiveRatioMeasure; 
		internal string mActualStart = "$", mActualFinish = "$";//	 :	OPTIONAL IfcDateTime;
		internal string mRemainingWork = "$";//	 :	OPTIONAL IfcDuration;
		internal double mRemainingUsage, mCompletion;//	 :	OPTIONAL IfcPositiveRatioMeasure; 
		internal IfcResourceTime() : base() { }
		internal IfcResourceTime(IfcResourceTime t) : base(t)
		{
			mScheduleWork = t.mScheduleWork; mScheduleUsage = t.mScheduleUsage; mScheduleStart = t.mScheduleStart; mScheduleFinish = t.mScheduleFinish; mScheduleContour = t.mScheduleContour;
			mLevelingDelay = t.mLevelingDelay; mIsOverAllocated = t.mIsOverAllocated; mStatusTime = t.mStatusTime; mActualWork = t.mActualWork; mActualUsage = t.mActualUsage;
			mActualStart = t.mActualStart; mActualFinish = t.mActualFinish; mRemainingWork = t.mRemainingWork; mRemainingUsage = t.mRemainingUsage; mCompletion = t.mCompletion;

		}
		internal IfcResourceTime(DatabaseIfc m, string name, IfcDataOriginEnum orig, string userOrigin, IfcDuration schedWork, double schedUsage, DateTime schedStart,
			DateTime schedFinish, string schedContour, IfcDuration levelingDelay, bool isOverAllocated, DateTime statusTime, IfcDuration actualWork, double actualUsage,
			DateTime actualStart, DateTime actualFinish, IfcDuration remainingWork, double remainingUsage, double fractionComplete)
			: base(m, name, orig, userOrigin)
		{
			if (schedWork != null)
				mScheduleWork = schedWork.ToString();
			mScheduleUsage = schedUsage;
			if (schedStart != DateTime.MinValue)
				mScheduleStart = IfcDateTime.Convert(schedStart);
			if (schedFinish != DateTime.MinValue)
				mScheduleFinish = IfcDateTime.Convert(schedFinish);
			if (!string.IsNullOrEmpty(schedContour))
				mScheduleContour = schedContour.Replace("'", "");
			if (levelingDelay != null)
				mLevelingDelay = levelingDelay.ToString();
			mIsOverAllocated = isOverAllocated;
			if (statusTime != DateTime.MinValue)
				mStatusTime = IfcDateTime.Convert(statusTime);
			if (actualWork != null)
				mActualWork = actualWork.ToString();
			mActualUsage = actualUsage;
			if (actualStart != DateTime.MinValue)
				mActualStart = IfcDateTime.Convert(actualStart);
			if (actualFinish != DateTime.MinValue)
				mActualFinish = IfcDateTime.Convert(actualFinish);
			if (remainingWork != null)
				mRemainingWork = remainingWork.ToString();
			mRemainingUsage = remainingUsage;
			mCompletion = fractionComplete;
		}
		internal static IfcResourceTime Parse(string strDef) { IfcResourceTime s = new IfcResourceTime(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcResourceTime s, List<string> arrFields, ref int ipos)
		{
			IfcSchedulingTime.parseFields(s, arrFields, ref ipos);
			s.mRemainingWork = arrFields[ipos++].Replace("'", "");
			s.mScheduleUsage = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mScheduleStart = arrFields[ipos++].Replace("'", "");
			s.mScheduleFinish = arrFields[ipos++].Replace("'", "");
			s.mScheduleContour = arrFields[ipos++].Replace("'", "");
			s.mLevelingDelay = arrFields[ipos++].Replace("'", "");
			s.mIsOverAllocated = ParserSTEP.ParseBool(arrFields[ipos++]);
			s.mStatusTime = arrFields[ipos++].Replace("'", "");
			s.mActualWork = arrFields[ipos++].Replace("'", "");
			s.mActualUsage = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mActualStart = arrFields[ipos++].Replace("'", "");
			s.mActualFinish = arrFields[ipos++].Replace("'", "");
			s.mRemainingWork = arrFields[ipos++].Replace("'", "");
			s.mRemainingUsage = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mCompletion = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildString()
		{
			return base.BuildString() + (mScheduleWork == "$" ? ",$," : ",'" + mScheduleWork + "',") + ParserSTEP.DoubleOptionalToString(mScheduleUsage) + (mScheduleStart == "$" ? ",$," : ",'" + mScheduleStart + "',") +
				(mScheduleFinish == "$" ? "$," : "'" + mScheduleFinish + "',") + (mScheduleContour == "$" ? "$," : "'" + mScheduleContour + "',") + (mLevelingDelay == "$" ? "$," : "'" + mLevelingDelay + "',") + ParserSTEP.BoolToString(mIsOverAllocated) +
				(mStatusTime == "$" ? ",$," : ",'" + mStatusTime + "',") + (mActualWork == "$" ? "$," : "'" + mActualWork + "',") + ParserSTEP.DoubleOptionalToString(mActualUsage) + (mActualStart == "$" ? ",$," : ",'" + mActualStart + "',") +
				(mActualFinish == "$" ? "$," : "'" + mActualFinish + "',") + (mRemainingWork == "$" ? "$," : "'" + mRemainingWork + "',") + ParserSTEP.DoubleOptionalToString(mRemainingUsage) + "," + ParserSTEP.DoubleOptionalToString(mCompletion);
		}
	}
	public partial class IfcRevolvedAreaSolid : IfcSweptAreaSolid
	{
		internal int mAxis;//: IfcAxis1Placement
		internal double mAngle;// : IfcPlaneAngleMeasure;

		public IfcAxis1Placement Axis { get { return mDatabase.mIfcObjects[mAxis] as IfcAxis1Placement; } set { mAxis = value.mIndex; } }
		public double Angle { get { return mAngle; } set { mAngle = value; } }

		internal IfcRevolvedAreaSolid() : base() { }
		internal IfcRevolvedAreaSolid(IfcRevolvedAreaSolid s) : base(s) { mAxis = s.mAxis; mAngle = s.mAngle; }
		public IfcRevolvedAreaSolid(IfcProfileDef profile, IfcAxis2Placement3D pl, IfcAxis1Placement axis, double angle) : base(profile, pl) { Axis = axis; mAngle = angle; }

		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mAxis) + "," + ParserSTEP.DoubleToString(mAngle); }
		internal static void parseFields(IfcRevolvedAreaSolid r, List<string> arrFields, ref int ipos)
		{
			IfcSweptAreaSolid.parseFields(r, arrFields, ref ipos);
			r.mAxis = ParserSTEP.ParseLink(arrFields[ipos++]);
			string str = arrFields[ipos];
			if (arrFields[ipos].StartsWith("IfcPlaneAngleMeasure(", true, System.Globalization.CultureInfo.CurrentCulture))
				r.mAngle = ParserSTEP.ParseDouble(str.Substring(21, str.Length - 22));
			else
				r.mAngle = ParserSTEP.ParseDouble(str);
		}
		internal static IfcRevolvedAreaSolid Parse(string strDef) { IfcRevolvedAreaSolid r = new IfcRevolvedAreaSolid(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
	}
	//ENTITY IfcRevolvedAreaSolidTapered
	public class IfcRibPlateProfileProperties : IfcProfileProperties // DEPRECEATED IFC4
	{ 
		internal double mThickness, mRibHeight, mRibWidth, mRibSpacing;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcRibPlateDirectionEnum mDirection;// : IfcRibPlateDirectionEnum;*/
		internal IfcRibPlateProfileProperties() : base() { }
		internal IfcRibPlateProfileProperties(IfcRibPlateProfileProperties p) : base(p)
		{
			mThickness = p.mThickness;
			mRibHeight = p.mRibHeight;
			mRibWidth = p.mRibWidth;
			mRibSpacing = p.mRibSpacing;
			mDirection = p.mDirection;
		}
		internal new static IfcRibPlateProfileProperties Parse(string strDef,Schema schema) { IfcRibPlateProfileProperties p = new IfcRibPlateProfileProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcRibPlateProfileProperties p, List<string> arrFields, ref int ipos,Schema schema)
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
		internal double mHeight;// : IfcPositiveLengthMeasure;
		internal double mBottomRadius;// : IfcPositiveLengthMeasure;	
		internal IfcRightCircularCone() : base() { }
		internal IfcRightCircularCone(IfcRightCircularCone c) : base(c) { mHeight = c.mHeight; mBottomRadius = c.mBottomRadius; }
		internal static void parseFields(IfcRightCircularCone c, List<string> arrFields, ref int ipos) { IfcCsgPrimitive3D.parseFields(c, arrFields, ref ipos); c.mHeight = ParserSTEP.ParseDouble(arrFields[ipos++]); c.mBottomRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcRightCircularCone Parse(string strDef) { IfcRightCircularCone c = new IfcRightCircularCone(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mHeight) + "," + ParserSTEP.DoubleToString(mBottomRadius); }
	}
	public partial class IfcRightCircularCylinder : IfcCsgPrimitive3D
	{
		internal double mHeight;// : IfcPositiveLengthMeasure;
		internal double mRadius;// : IfcPositiveLengthMeasure;	
		internal IfcRightCircularCylinder() : base() { }
		internal IfcRightCircularCylinder(IfcRightCircularCylinder c) : base(c) { mHeight = c.mHeight; mRadius = c.mRadius; }

		internal static void parseFields(IfcRightCircularCylinder c, List<string> arrFields, ref int ipos) { IfcCsgPrimitive3D.parseFields(c, arrFields, ref ipos); c.mHeight = ParserSTEP.ParseDouble(arrFields[ipos++]); c.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcRightCircularCylinder Parse(string strDef) { IfcRightCircularCylinder c = new IfcRightCircularCylinder(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mHeight) + "," + ParserSTEP.DoubleToString(mRadius); }
	}
	public partial class IfcRoof : IfcBuildingElement
	{
		internal IfcRoofTypeEnum mPredefinedType = IfcRoofTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcRoofTypeEnum PredefinedType { get { return mPredefinedType; } }

		internal IfcRoof() : base() { }
		internal IfcRoof(IfcRoof r) : base(r) { mPredefinedType = r.mPredefinedType; }
		public IfcRoof(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcRoof Parse(string strDef) { IfcRoof r = new IfcRoof(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
		internal static void parseFields(IfcRoof r, List<string> arrFields, ref int ipos)
		{
			IfcBuildingElement.parseFields(r, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				r.mPredefinedType = (IfcRoofTypeEnum)Enum.Parse(typeof(IfcRoofTypeEnum), str.Substring(1, str.Length - 2));
		}
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcRoofType : IfcBuildingElementType //IFC4
	{
		internal IfcRoofTypeEnum mPredefinedType = IfcRoofTypeEnum.NOTDEFINED;
		public IfcRoofTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcRoofType() : base() { }
		internal IfcRoofType(IfcRoofType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcRoofType(DatabaseIfc m, string name, IfcRoofTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcRoofType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcRoofTypeEnum)Enum.Parse(typeof(IfcRoofTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcRoofType Parse(string strDef) { IfcRoofType t = new IfcRoofType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract partial class IfcRoot : BaseClassIfc//ABSTRACT SUPERTYPE OF (ONEOF (IfcObjectDefinition ,IfcPropertyDefinition ,IfcRelationship));
	{
		private string mGlobalId; // :	IfcGloballyUniqueId;
		private int mOwnerHistory;// : IFC4  OPTIONAL IfcOwnerHistory;
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
		internal IfcOwnerHistory OwnerHistory { get { return mDatabase.mIfcObjects[mOwnerHistory] as IfcOwnerHistory; } set { mOwnerHistory = (value == null ? 0 : value.mIndex); } }
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } } 
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcRoot() : base() { mGlobalId = ParserIfc.EncodeGuid(Guid.NewGuid()); }
		protected IfcRoot(IfcRoot r) : base(r) { mGlobalId = r.mGlobalId; mOwnerHistory = r.mOwnerHistory; mName = r.mName; mDescription = r.mDescription; }
		protected IfcRoot(DatabaseIfc m) : base(m)
		{
			mGlobalId = ParserIfc.EncodeGuid(Guid.NewGuid());
			//m.mGlobalIDs.Add(mGlobalId);
			if (m.mModelView != ModelView.Ifc4Reference)
				OwnerHistory = m.OwnerHistory(IfcChangeActionEnum.ADDED);
		}
		protected static void parseFields(IfcRoot root, List<string> arrFields, ref int ipos)
		{
			root.mGlobalId = arrFields[ipos++].Replace("'", "");
			root.mOwnerHistory = ParserSTEP.ParseLink(arrFields[ipos++]);
			root.mName = arrFields[ipos++].Replace("'", "");
			root.mDescription = arrFields[ipos++].Replace("'", "");
		}
		protected virtual void parseString(string str, ref int pos)
		{
			mGlobalId = ParserSTEP.StripString(str, ref pos);
			mOwnerHistory = ParserSTEP.StripLink(str, ref pos);
			mName = ParserSTEP.StripString(str, ref pos);
			mDescription = ParserSTEP.StripString(str, ref pos);
		}
		protected override string BuildString() { return base.BuildString() + ",'" + mGlobalId + (mOwnerHistory == 0 ? "',$" : "',#" + mOwnerHistory) + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'"); }
	}
	public class IfcRotationalStiffnessSelect
	{
		internal bool mRigid = false;
		internal IfcRotationalStiffnessMeasure mStiffness = null;
		public IfcRotationalStiffnessSelect(bool fix) { mRigid = fix; }
		internal IfcRotationalStiffnessSelect(double stiff) { mStiffness = new IfcRotationalStiffnessMeasure(stiff); }
		internal IfcRotationalStiffnessSelect(IfcRotationalStiffnessMeasure stiff) { mStiffness = stiff; }
		internal static IfcRotationalStiffnessSelect Parse(string str, Schema schema)
		{
			if (str.StartsWith("IFCBOOL"))
				return new IfcRotationalStiffnessSelect(((IfcBoolean)ParserIfc.parseSimpleValue(str)).mValue);
			if (str.StartsWith("IFCROT"))
				return new IfcRotationalStiffnessSelect((IfcRotationalStiffnessMeasure)ParserIfc.parseDerivedMeasureValue(str));
			if (str.StartsWith("."))
				return new IfcRotationalStiffnessSelect(ParserSTEP.ParseBool(str));
			double d = ParserSTEP.ParseDouble(str), tol = 1e-9;
			if (schema == Schema.IFC2x3)
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
	//ENTITY IfcRoundedEdgeFeature // DEPRECEATED IFC4
	public partial class IfcRoundedRectangleProfileDef : IfcRectangleProfileDef
	{
		internal double mRoundingRadius;// : IfcPositiveLengthMeasure; 
		internal IfcRoundedRectangleProfileDef() : base() { }
		internal IfcRoundedRectangleProfileDef(IfcRoundedRectangleProfileDef c) : base(c) { mRoundingRadius = c.mRoundingRadius; }
		public IfcRoundedRectangleProfileDef(DatabaseIfc m, string name, double h, double b, double r) : base(m, name, h, b) { mRoundingRadius = r; }
		internal static void parseFields(IfcRoundedRectangleProfileDef p, List<string> arrFields, ref int ipos) { IfcRectangleProfileDef.parseFields(p, arrFields, ref ipos); p.mRoundingRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal new static IfcRoundedRectangleProfileDef Parse(string strDef) { IfcRoundedRectangleProfileDef p = new IfcRoundedRectangleProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mRoundingRadius); }
	}
}
