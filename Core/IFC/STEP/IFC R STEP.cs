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
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class IfcRail
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcRailTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRailTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcRailway
	{
		public override string StepClassName { get { if (mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4X3_RC1) return "IfcFacility"; return base.StepClassName; } }
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4X3_RC3 ? "" : (mPredefinedType == IfcRailwayTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release > ReleaseVersion.IFC4X3_RC2)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcRailwayTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcRailType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRailTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcRailing
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRailingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcRailingType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRailingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcRamp
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRampTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcRampFlight
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? base.BuildStringSTEP(release) : base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcRampFlightTypeEnum>(s.Substring(1, s.Length - 2), out mPredefinedType);
			}
		}
	}
	public partial class IfcRampFlightType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcRampFlightTypeEnum>(s.Substring(1, s.Length - 2), out mPredefinedType);
		}
	}
	public partial class IfcRampType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRampTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcRationalBezierCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.DoubleToString(mWeightsData[0]);
			for (int icounter = 1; icounter < mWeightsData.Count; icounter++)
				str += "," + ParserSTEP.DoubleToString(mWeightsData[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> arrNodes = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			for (int icounter = 0; icounter < arrNodes.Count; icounter++)
				mWeightsData.Add(ParserSTEP.ParseDouble(arrNodes[icounter]));
		}

	}
	public partial class IfcRationalBSplineCurveWithKnots
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.DoubleToString(mWeightsData[0]);
			for (int icounter = 1; icounter < mWeightsData.Count; icounter++)
				str += "," + ParserSTEP.DoubleToString(mWeightsData[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mWeightsData = ParserSTEP.StripListDouble(str, ref pos, len);
		}
	}
	public partial class IfcRationalBSplineSurfaceWithKnots
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			List<double> wts = mWeightsData[0];
			string str = base.BuildStringSTEP(release) + ",((" +
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			foreach (var weights in ParserSTEP.StripListListDouble(str, ref pos, len))
				mWeightsData.Add(weights);
		}
	}
	public partial class IfcRectangleHollowProfileDef
	{
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mWallThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mInnerFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mOuterFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mWallThickness) + (double.IsNaN(mInnerFilletRadius) || mInnerFilletRadius < mDatabase.Tolerance ? ",$," : "," + ParserSTEP.DoubleOptionalToString(mInnerFilletRadius) + ",") + (double.IsNaN(mOuterFilletRadius) || mOuterFilletRadius < mDatabase.Tolerance ? "$" : ParserSTEP.DoubleOptionalToString(mOuterFilletRadius)); }
	}
	public partial class IfcRectangleProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mXDim) + "," + ParserSTEP.DoubleToString(mYDim); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mXDim = ParserSTEP.StripDouble(str, ref pos, len);
			mYDim = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRectangularPyramid
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mXLength) + "," + ParserSTEP.DoubleToString(mYLength) + "," + ParserSTEP.DoubleToString(mHeight); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mXLength = ParserSTEP.StripDouble(str, ref pos, len);
			mYLength = ParserSTEP.StripDouble(str, ref pos, len);
			mHeight = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRectangularTrimmedSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.DoubleToString(mU1) + "," + ParserSTEP.DoubleToString(mV1) + "," + ParserSTEP.DoubleToString(mU2) + "," + ParserSTEP.DoubleToString(mV2) + "," + ParserSTEP.BoolToString(mUsense) + "," + ParserSTEP.BoolToString(mVsense); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mBasisSurface = ParserSTEP.StripLink(str, ref pos, len);
			mU1 = ParserSTEP.StripDouble(str, ref pos, len);
			mU2 = ParserSTEP.StripDouble(str, ref pos, len);
			mV1 = ParserSTEP.StripDouble(str, ref pos, len);
			mV2 = ParserSTEP.StripDouble(str, ref pos, len);
			mUsense = ParserSTEP.StripBool(str, ref pos, len);
			mVsense = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcRecurrencePattern
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = "." + mRecurrenceType.ToString();
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Enum.TryParse<IfcRecurrenceTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mRecurrenceType);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("("))
				mDayComponent = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => int.Parse(x));
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("("))
				mWeekdayComponent = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => int.Parse(x));
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("("))
				mMonthComponent = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => int.Parse(x));
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				mPosition = int.Parse(s);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				mInterval = int.Parse(s);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				mOccurrences = int.Parse(s);
			mTimePeriods = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcReference
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mDatabase.Release < ReleaseVersion.IFC4)
				return "";
			string str = (mTypeIdentifier == "$" ? "$" : "'" + mTypeIdentifier + "'") + (mAttributeIdentifier == "$" ? ",$," : ",'" + mAttributeIdentifier + "',") +
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mTypeIdentifier = ParserSTEP.StripString(str, ref pos, len);
			mAttributeIdentifier = ParserSTEP.StripString(str, ref pos, len);
			mInstanceName = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("("))
				mListPositions.AddRange(ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => int.Parse(x)));
			mInnerReference = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcReferent
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			(mPredefinedType == IfcReferentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".") + "," +
			ParserSTEP.DoubleOptionalToString(mRestartDistance);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcReferentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			RestartDistance = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRegularTimeSeries
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," +
			ParserSTEP.DoubleOptionalToString(mTimeStep) +
			",(#" + string.Join(",#", mValues.ConvertAll(x => x.StepId.ToString())) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			TimeStep = ParserSTEP.StripDouble(str, ref pos, len);
			Values.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcTimeSeriesValue));
		}
	}
	public partial class IfcReinforcedSoil
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcReinforcedSoilTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcReinforcedSoilTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcReinforcementBarProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return ParserSTEP.DoubleToString(mTotalCrossSectionArea) + ",'" + mSteelGrade + "'," + (mBarSurface == IfcReinforcingBarSurfaceEnum.NOTDEFINED ? "$," : "." + mBarSurface.ToString() + ".,") +
				ParserSTEP.DoubleOptionalToString(mEffectiveDepth) + "," + ParserSTEP.DoubleOptionalToString(mNominalBarDiameter) + "," + ParserSTEP.DoubleOptionalToString(mBarCount);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mTotalCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mSteelGrade = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcReinforcingBarSurfaceEnum>(s.Replace(".", ""), true, out mBarSurface);
			mEffectiveDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mNominalBarDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mBarCount = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcReinforcementDefinitionProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + (mDefinitionType == "$" ? ",$,(#" : ",'" + mDefinitionType + "',(#") + mReinforcementSectionDefinitions[0];
			for (int icounter = 1; icounter < mReinforcementSectionDefinitions.Count; icounter++)
				result += ",#" + mReinforcementSectionDefinitions;
			return result + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDefinitionType = ParserSTEP.StripString(str, ref pos, len);
			mReinforcementSectionDefinitions = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcReinforcingBar
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + "," + (release < ReleaseVersion.IFC4 ? ParserSTEP.DoubleToString(mNominalDiameter) + "," + ParserSTEP.DoubleToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mBarLength) + ",." + mPredefinedType.ToString() + ".," :
				ParserSTEP.DoubleOptionalToString(mNominalDiameter) + "," + ParserSTEP.DoubleOptionalToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mBarLength) + (mPredefinedType == IfcReinforcingBarTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,"));
			return result + (mBarSurface == IfcReinforcingBarSurfaceEnum.NOTDEFINED ? "$" : "." + mBarSurface.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mBarLength = ParserSTEP.StripDouble(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcReinforcingBarTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (str.StartsWith("."))
				Enum.TryParse<IfcReinforcingBarSurfaceEnum>(s.Replace(".", ""), true, out mBarSurface);

		}
	}
	public partial class IfcReinforcingBarType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release);
			result += ",." + mPredefinedType + ".," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + ",";
			result += ParserSTEP.DoubleOptionalToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mBarLength);
			result += (mBarSurface == IfcReinforcingBarSurfaceEnum.NOTDEFINED ? ",$," : ",." + mBarSurface.ToString() + ".,") + (mBendingShapeCode == "$" ? "$," : "'" + mBendingShapeCode + "',");
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcReinforcingBarTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mBarLength = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcReinforcingBarSurfaceEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mBarSurface);
			mBendingShapeCode = ParserSTEP.StripString(str, ref pos, len);
			//t.mBendingParameters = 
			ParserSTEP.StripField(str, ref pos, len);
		}
	}
	public abstract partial class IfcReinforcingElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mSteelGrade == "$" ? ",$" : ",'" + mSteelGrade + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSteelGrade = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcReinforcingMesh
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mMeshLength) + "," + ParserSTEP.DoubleOptionalToString(mMeshWidth) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarNominalDiameter) + "," + ParserSTEP.DoubleToString(mTransverseBarNominalDiameter) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarCrossSectionArea) + "," + ParserSTEP.DoubleToString(mTransverseBarCrossSectionArea) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarSpacing) + "," + ParserSTEP.DoubleToString(mTransverseBarSpacing) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcReinforcingMeshTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + "."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mMeshLength = ParserSTEP.StripDouble(str, ref pos, len);
			mMeshWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mTransverseBarNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mLongitudinalBarCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mTransverseBarCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mLongitudinalBarSpacing = ParserSTEP.StripDouble(str, ref pos, len);
			mTransverseBarSpacing = ParserSTEP.StripDouble(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcReinforcingMeshTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcReinforcingMeshType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mDatabase.Release < ReleaseVersion.IFC4)
				return "";
			string result = base.BuildStringSTEP(release) + ",." + mPredefinedType + ".," + ParserSTEP.DoubleOptionalToString(mMeshLength) + "," +
				ParserSTEP.DoubleOptionalToString(mMeshWidth) + "," + ParserSTEP.DoubleToString(mLongitudinalBarNominalDiameter) + "," +
				ParserSTEP.DoubleToString(mTransverseBarNominalDiameter) + "," + ParserSTEP.DoubleToString(mLongitudinalBarCrossSectionArea) + "," +
				ParserSTEP.DoubleToString(mTransverseBarCrossSectionArea) + "," + ParserSTEP.DoubleToString(mLongitudinalBarSpacing) + "," +
				ParserSTEP.DoubleToString(mTransverseBarSpacing) + (mBendingShapeCode == "$" ? ",$," : ",'" + mBendingShapeCode + "',");
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcReinforcingMeshTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mMeshLength = ParserSTEP.StripDouble(str, ref pos, len);
			mMeshWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mTransverseBarNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mLongitudinalBarCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mTransverseBarCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mLongitudinalBarSpacing = ParserSTEP.StripDouble(str, ref pos, len);
			mTransverseBarSpacing = ParserSTEP.StripDouble(str, ref pos, len);
			mBendingShapeCode = ParserSTEP.StripString(str, ref pos, len);
			// parse bending
		}
	}
	public partial class IfcRelAdheresToElement 
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + ",#" + RelatingElement.StepId + ",(" + string.Join(",", RelatedSurfaceFeatures.Select(x=>"#" +x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
			RelatedSurfaceFeatures.AddRange(ParserSTEP.StripListLink(str,ref pos, len).Select(x=> dictionary[x] as IfcSurfaceFeature));
		}
	}
	public partial class IfcRelAggregates
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRelatedObjects.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",#" + mRelatingObject.StepId + ",(#" + string.Join(",#", RelatedObjects.OrderBy(x=>x.StepId).Select(x => x.Index)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingObject = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcObjectDefinition;
			RelatedObjects.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcObjectDefinition));
		}
	}
	public abstract partial class IfcRelAssigns
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", RelatedObjects.ConvertAll(x => x.Index)) + ")," +
				(mDatabase.Release < ReleaseVersion.IFC4 && mRelatedObjectsType != IfcObjectTypeEnum.NOTDEFINED ? "." + mRelatedObjectsType + "." : "$");

		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatedObjects.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcObjectDefinition));
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (!Enum.TryParse<IfcObjectTypeEnum>(field.Replace(".", ""), true, out mRelatedObjectsType))
				mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;
		}

	}
	public partial class IfcRelAssignsTasks
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mTimeForTask)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			TimeForTask = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcScheduleTimeControl;
		}
	}
	public partial class IfcRelAssignsToActor
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP(release) + ",#" + RelatingActor.Index + "," + ParserSTEP.ObjToLinkString(mRelatingActor)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingActor = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActor;
			ActingRole = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorRole;
		}

	}
	public partial class IfcRelAssignsToControl
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP(release) + ",#" + mRelatingControl.StepId); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingControl = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcControl;
		}
	}
	public partial class IfcRelAssignsToGroup
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP(release) + ",#" + mRelatingGroup.Index); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingGroup = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcGroup;
		}
	}
	public partial class IfcRelAssignsToGroupByFactor
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP(release) + (mDatabase.Release < ReleaseVersion.IFC4 ? "" : "," + ParserSTEP.DoubleToString(mFactor))); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mFactor = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRelAssignsToProcess
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",#" + mRelatingProcess + (mQuantityInProcess == null ? ",$" : ",#" + mQuantityInProcess.Index); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingProcess = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProcessSelect;
			mQuantityInProcess = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMeasureWithUnit;
		}
	}
	public partial class IfcRelAssignsToProduct
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP(release) + ",#" + mRelatingProduct.Index); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingProduct = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProductSelect;
		}
	}
	public partial class IfcRelAssignsToResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingResource)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingResource = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcResourceSelect;
		}
	}
	public abstract partial class IfcRelAssociates
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRelatedObjects.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mRelatedObjects.ConvertAll(x => x.Index)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			foreach (int id in ParserSTEP.StripListLink(str, ref pos, len))
			{
				BaseClassIfc obj = null;
				if (dictionary.TryGetValue(id, out obj))
				{
					IfcDefinitionSelect definitionSelect = obj as IfcDefinitionSelect;
					if (definitionSelect != null)
						RelatedObjects.Add(definitionSelect);
				}
			}
		}
	}
	public partial class IfcRelAssociatesClassification
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release);
			return string.IsNullOrEmpty(str) ? "" : str + ",#" + RelatingClassification.Index;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingClassification = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcClassificationSelect;
		}
	}
	public partial class IfcRelAssociatesConstraint
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (RelatingConstraint == null ? "" : base.BuildStringSTEP(release) + (mIntent == "$" ? ",$," : ",'" + mIntent + "',") + ParserSTEP.LinkToString(mRelatingConstraint)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIntent = ParserSTEP.StripString(str, ref pos, len);
			RelatingConstraint = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcConstraint;
		}
	}
	public partial class IfcRelAssociatesDocument
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingDocument); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRelatingDocument = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcRelAssociatesLibrary
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mRelatingLibrary.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRelatingLibrary = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcLibrarySelect;
		}
	}
	public partial class IfcRelAssociatesMaterial
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string baseString = base.BuildStringSTEP(release);
			if (string.IsNullOrEmpty(baseString) || (release < ReleaseVersion.IFC4 && string.IsNullOrEmpty(RelatingMaterial.ToString())))
				return "";
			return baseString + ",#" + mRelatingMaterial;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingMaterial = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterialSelect;
		}
	}
	public partial class IfcRelAssociatesProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mRelatingProfileDef.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingProfileDef = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
		}
	}
	public partial class IfcRelAssociatesProfileProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release > ReleaseVersion.IFC2x3 || mRelatedObjects.Count == 0)
				return "";
			string str = base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingProfileProperties) + "," +
				ParserSTEP.LinkToString(mProfileSectionLocation) + ",";
			if (double.IsNaN(mProfileOrientationValue))
			{
				if (mProfileOrientation == 0)
					return str + "$";
				return str + ",IFCPLANEANGLEMEASURE(" + ParserSTEP.DoubleToString(mProfileOrientation) + ")";
			}
			return str + ParserSTEP.LinkToString((int)mProfileOrientation);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRelatingProfileProperties = ParserSTEP.StripLink(str, ref pos, len);
			mProfileSectionLocation = ParserSTEP.StripLink(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("IfcPlaneAngleMeasure(", true, System.Globalization.CultureInfo.CurrentCulture))
			{
				mProfileOrientationValue = ParserSTEP.ParseDouble(s.Substring(21, s.Length - 22));
			}
			else
				mProfileOrientation = ParserSTEP.ParseLink(s);
		}
	}
	public partial class IfcRelaxation
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return ParserSTEP.DoubleToString(mRelaxationValue) + "," + ParserSTEP.DoubleToString(mInitialStress); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mRelaxationValue = ParserSTEP.StripDouble(str, ref pos, len);
			mInitialStress = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRelConnectsElements
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mRelatingElement == null || mRelatedElement == null ? "" : base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(mConnectionGeometry) + ",#" + mRelatingElement.Index + ",#" + mRelatedElement.Index); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			ConnectionGeometry = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcConnectionGeometry;
			RelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
			mRelatedElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
		}
	}
	public partial class IfcRelConnectsPathElements
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (RelatingElement == null || RelatedElement == null)
				return "";
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mRelatingPriorities.ConvertAll(x => ParserSTEP.DoubleToString(x))) + "),(" +
				string.Join(",", mRelatedPriorities.ConvertAll(x => ParserSTEP.DoubleToString(x))) + "),." + mRelatedConnectionType.ToString() + ".,." + mRelatingConnectionType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRelatingPriorities = ParserSTEP.StripListDouble(str, ref pos, len);
			mRelatedPriorities = ParserSTEP.StripListDouble(str, ref pos, len);
			Enum.TryParse<IfcConnectionTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mRelatedConnectionType);
			Enum.TryParse<IfcConnectionTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mRelatingConnectionType);
		}
	}
	public partial class IfcRelConnectsPorts
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedPort) + "," + ParserSTEP.LinkToString(mRealizingElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingPort = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPort;
			RelatedPort = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPort;
			mRealizingElement = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcRelConnectsPortToElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingPort = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPort;
			RelatedElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
		}

	}
	public partial class IfcRelConnectsStructuralActivity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mRelatingElement.StepId + ",#" + mRelatedStructuralActivity.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcStructuralActivityAssignmentSelect;
			RelatedStructuralActivity = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcStructuralActivity;
		}
	}
	public partial class IfcRelConnectsStructuralElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mRelatingElement.StepId + ",#" + mRelatedStructuralMember.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
			RelatedStructuralMember = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcStructuralMember;
		}
	}
	public partial class IfcRelConnectsStructuralMember
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingStructuralMember) + "," +
				ParserSTEP.LinkToString(mRelatedStructuralConnection) + "," + ParserSTEP.LinkToString(mAppliedCondition) + "," +
				ParserSTEP.LinkToString(mAdditionalConditions) + "," + ParserSTEP.DoubleOptionalToString(mSupportedLength) + "," +
				ParserSTEP.LinkToString(mConditionCoordinateSystem);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingStructuralMember = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcStructuralMember;
			RelatedStructuralConnection = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcStructuralConnection;
			mAppliedCondition = ParserSTEP.StripLink(str, ref pos, len);
			mAdditionalConditions = ParserSTEP.StripLink(str, ref pos, len);
			mSupportedLength = ParserSTEP.StripDouble(str, ref pos, len);
			mConditionCoordinateSystem = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcRelConnectsWithEccentricity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mConnectionConstraint); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mConnectionConstraint = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcRelConnectsWithRealizingElements
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRealizingElements.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mRealizingElements.Select(x => x.Index)) + (mConnectionType == "$" ? "),$" : "),'" + mConnectionType + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RealizingElements.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcElement));
			mConnectionType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcRelContainedInSpatialStructure
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRelatedElements.Count <= 0)
				return "";
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mRelatedElements.OrderBy(x=>x.StepId).Select(x => x.StepId.ToString())) + "),#" + mRelatingStructure.mIndex;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			foreach (int id in ParserSTEP.StripListLink(str, ref pos, len))
			{
				try
				{
					RelatedElements.Add(dictionary[id] as IfcProduct);
				}
				catch (Exception x) { mDatabase.logParseError("XXX Error in line #" + StepId + " " + StepClassName + " " + x.Message); }
			}
			RelatingStructure = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSpatialElement;
		}
	}
	public partial class IfcRelCoversBldgElements
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRelatedCoverings.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",#" + mRelatingBuildingElement.StepId + ",(#" +
				string.Join(",#", mRelatedCoverings.Select(x => x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingBuildingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
			foreach (IfcCovering covering in ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcCovering))
				addCovering(covering);
		}
	}
	public partial class IfcRelCoversSpaces
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRelatedCoverings.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",#" + mRelatedSpace.Index + ",(#" + string.Join(",#", RelatedCoverings.ConvertAll(x => x.Index)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatedSpace = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSpace;
			RelatedCoverings.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcCovering));
		}
	}
	public partial class IfcRelDeclares
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4 || mRelatingContext == null || mRelatedDefinitions.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",#" + mRelatingContext.mIndex + ",(#" + string.Join(",#", mRelatedDefinitions.ConvertAll(x => x.Index)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingContext = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcContext;
			RelatedDefinitions.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcDefinitionSelect));
		}
	}
	public partial class IfcRelDefinesByObject
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRelatedObjects.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mRelatedObjects.Select(x => "#" + x.StepId)) + "),#" + mRelatingObject.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatedObjects.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcObject));
			RelatingObject = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcObject;
		}
	}
	public partial class IfcRelDefinesByProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release > ReleaseVersion.IFC2x3 && RelatingPropertyDefinition.Count > 1)
			{
				return base.BuildStringSTEP(release) + ",(" + string.Join(",", mRelatedObjects.ConvertAll(x => "#" + x.StepId)) + "),(" +
					string.Join(",", mRelatingPropertyDefinition.Select(x => "#" + x.StepId)) + ")";
			}
			else if (RelatingPropertyDefinition.Count == 0)
				return "";
			IfcPropertySetDefinition pset = RelatingPropertyDefinition.First();
			if (mRelatedObjects.Count == 0 || pset == null || pset.isEmpty)
				return "";
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mRelatedObjects.ConvertAll(x => "#" + x.StepId)) + "),#" + pset.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatedObjects.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcObjectDefinition));
			string field = ParserSTEP.StripField(str, ref pos, len).Trim();
			if (field[0] == '(')
				RelatingPropertyDefinition.AddRange(ParserSTEP.SplitListLinks(field).Select(x=>dictionary[x] as IfcPropertySetDefinition));
			else
				RelatingPropertyDefinition.Add(dictionary[ParserSTEP.ParseLink(field)] as IfcPropertySetDefinition);
		}
	}
	public partial class IfcRelDefinesByTemplate
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mDatabase.Release < ReleaseVersion.IFC4 || mRelatedPropertySets.Count == 0)
				return "";
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.LinkToString(mRelatedPropertySets[0]);
			for (int icounter = 1; icounter < mRelatedPropertySets.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedPropertySets[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingTemplate);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			foreach (IfcPropertySetDefinition pset in ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcPropertySetDefinition))
				AddRelated(pset);
			RelatingTemplate = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPropertySetTemplate;
		}
	}
	public partial class IfcRelDefinesByType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRelatedObjects.Count == 0)
				return "";
			IfcTypeObject to = RelatingType;
			if (to == null || string.IsNullOrEmpty(to.ToString()))
				return "";
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mRelatedObjects.ConvertAll(x => x.Index)) + "),#" + mRelatingType.Index;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatedObjects.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcObject));
			RelatingType = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcTypeObject;
		}
	}
	public partial class IfcRelFillsElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingOpeningElement) + "," + ParserSTEP.LinkToString(mRelatedBuildingElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingOpeningElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcOpeningElement;
			RelatedBuildingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
		}
	}
	public partial class IfcRelFlowControlElements
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRelatingPort = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedElement = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcRelInterferesElements
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedElement) + "," +
				ParserSTEP.LinkToString(mInterferenceGeometry) + (release > ReleaseVersion.IFC4X3_RC3 ? "," + ParserSTEP.ObjToLinkString(mInterferenceSpace) :"") + 
				(mInterferenceType == "$" ? ",$," : ",'" + mInterferenceType + "',") + ParserIfc.LogicalToString(mImpliedOrder);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcInterferenceSelect;
			RelatedElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcInterferenceSelect;
			mInterferenceGeometry = ParserSTEP.StripLink(str, ref pos, len);
			if (release > ReleaseVersion.IFC4X3_RC3)
				mInterferenceSpace = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSpatialZone;
			mInterferenceType = ParserSTEP.StripString(str, ref pos, len);
			mImpliedOrder = ParserIfc.StripLogical(str, ref pos, len);
		}
	}
	public partial class IfcRelNests
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRelatedObjects.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",#" + mRelatingObject.Index + ",(#" + string.Join(",#", mRelatedObjects.Select(x => x.Index)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingObject = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcObjectDefinition;
			RelatedObjects.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcObjectDefinition));
		}
	}
	public partial class IfcRelOverridesProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release);
			if (string.IsNullOrEmpty(str))
				return "";
			str += ",(#" + mOverridingProperties[0];
			for (int icounter = 1; icounter < mOverridingProperties.Count; icounter++)
				str += ",#" + mOverridingProperties[icounter];
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mOverridingProperties = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcRelPositions
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			",#" + mRelatingPositioningElement.StepId +
			",(#" + string.Join(",#", mRelatedProducts.ConvertAll(x => x.StepId.ToString())) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingPositioningElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPositioningElement;
			RelatedProducts.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcProduct));
		}
	}
	public partial class IfcRelProjectsElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedFeatureElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
			RelatedFeatureElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcFeatureElementAddition;
		}
	}
	public partial class IfcRelReferencedInSpatialStructure
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if ((release < ReleaseVersion.IFC4 && RelatingStructure as IfcSpatialStructureElement == null) || mRelatedElements.Count <= 0)
				return "";
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mRelatedElements.Select(x => "#" + x.StepId)) + "),#" + mRelatingStructure.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatedElements.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcSpatialReferenceSelect));
			RelatingStructure = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSpatialElement;
		}
	}
	public partial class IfcRelSequence
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mRelatingProcess.StepId + ",#" + mRelatedProcess.StepId + "," +
				(release < ReleaseVersion.IFC4 ? ParserSTEP.DoubleToString(mTimeLagSS) : ParserSTEP.ObjToLinkString(mTimeLag)) + ",." +
				mSequenceType + (release < ReleaseVersion.IFC4 ? "." : (mUserDefinedSequenceType == "$" ? ".,$" : ".,'" + mUserDefinedSequenceType + "'"));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingProcess = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProcess;
			RelatedProcess = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProcess;
			if (release < ReleaseVersion.IFC4)
				mTimeLagSS = ParserSTEP.StripDouble(str, ref pos, len);
			else
				mTimeLag = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcLagTime;
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				Enum.TryParse<IfcSequenceEnum>(s.Replace(".", ""), true, out mSequenceType);
			if (release != ReleaseVersion.IFC2x3)
				mUserDefinedSequenceType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcRelServicesBuildings
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRelatedBuildings.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",#" + mRelatingSystem.StepId + ",(" +
				string.Join(",", mRelatedBuildings.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingSystem = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSystem;
			foreach (IfcSpatialElement spatial in ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcSpatialElement))
				addRelated(spatial);
		}
	}
	public partial class IfcRelSpaceBoundary
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return ((release > ReleaseVersion.IFC2x3 && mRelatedBuildingElement == null) || mRelatingSpace == 0 ? "" : base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingSpace) + "," + ParserSTEP.ObjToLinkString(mRelatedBuildingElement) + "," + ParserSTEP.LinkToString(mConnectionGeometry) + ",." + mPhysicalOrVirtualBoundary.ToString() + ".,." + mInternalOrExternalBoundary.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingSpace = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSpaceBoundarySelect;
			RelatedBuildingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
			mConnectionGeometry = ParserSTEP.StripLink(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				Enum.TryParse<IfcPhysicalOrVirtualEnum>(s.Replace(".", ""), true, out mPhysicalOrVirtualBoundary);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				Enum.TryParse<IfcInternalOrExternalEnum>(s.Replace(".", ""), true, out mInternalOrExternalBoundary);
		}
	}
	public partial class IfcRelSpaceBoundary1stLevel
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mParentBoundary); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			ParentBoundary = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcRelSpaceBoundary1stLevel;
		}
	}
	public partial class IfcRelSpaceBoundary2ndLevel
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mCorrespondingBoundary); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			CorrespondingBoundary = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcRelSpaceBoundary2ndLevel;
		}
	}
	public partial class IfcRelVoidsElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + RelatingBuildingElement.StepId + ",#" + RelatedOpeningElement.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingBuildingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
			RelatedOpeningElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcFeatureElementSubtraction;
		}
	}
	public partial class IfcReparametrisedCompositeCurveSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + mParamLength.ToString(); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mParamLength = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRepresentation<RepresentationItem>
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = (mContextOfItems == null ? "$" : "#" + mContextOfItems.Index) +
				(string.IsNullOrEmpty(mRepresentationIdentifier) ? ",$," : ",'" + ParserIfc.Encode(mRepresentationIdentifier) + "',") +
				(string.IsNullOrEmpty(mRepresentationType) ? "$,(" : "'" + ParserIfc.Encode(mRepresentationType) + "',(");
			if (mItems.Count > 0)
				str += "#" + string.Join(",#", mItems.ConvertAll(x => x.mIndex));
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			ContextOfItems = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcRepresentationContext;
			mRepresentationIdentifier = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mRepresentationType = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			Items.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as RepresentationItem));
		}
	}
	public abstract partial class IfcRepresentationContext
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mContextIdentifier == "$" ? "$," : "'" + mContextIdentifier + "',") + (mContextType == "$" ? "$" : "'" + mContextType + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mContextIdentifier = ParserSTEP.StripString(str, ref pos, len);
			mContextType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcRepresentationMap
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mMappingOrigin.StepId + ",#" + mMappedRepresentation.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			MappingOrigin = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement;
			MappedRepresentation = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcShapeModel;
		}
	}
	public partial class IfcResourceApprovalRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			",(#" + string.Join(",#", mRelatedResourceObjects.ConvertAll(x => x.StepId.ToString())) + ")" +
			",#" + mRelatingApproval.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatedResourceObjects.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcResourceObjectSelect));
			RelatingApproval = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcApproval;
		}
	}
	public partial class IfcResourceConstraintRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingConstraint) + ",(#" + mRelatedResourceObjects[0];
			for (int icounter = 1; icounter < mRelatedResourceObjects.Count; icounter++)
				result += ",#" + mRelatedResourceObjects[icounter];
			return result + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingConstraint = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcConstraint;
			foreach (IfcResourceObjectSelect resource in ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcResourceObjectSelect))
				addRelated(resource);
		}
	}

	public abstract partial class IfcResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release > ReleaseVersion.IFC2x3 ? (string.IsNullOrEmpty(mIdentification) ? ",$," : ",'" + ParserIfc.Encode(mIdentification) + "',") + (string.IsNullOrEmpty(mLongDescription) ? "$" : "'" + ParserIfc.Encode(mLongDescription) + "'") : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release > ReleaseVersion.IFC2x3)
			{
				mIdentification = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
				mLongDescription = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			}
		}
	}
	public abstract partial class IfcResourceLevelRelationship
	{ 
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mDatabase.Release < ReleaseVersion.IFC4 ? "" : (mName == "$" ? "$," : "'" + mName + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			if (release != ReleaseVersion.IFC2x3)
			{
				mName = ParserSTEP.StripString(str, ref pos, len);
				mDescription = ParserSTEP.StripString(str, ref pos, len);
			}
		}
	}
	public partial class IfcResourceTime
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mScheduleWork == "$" ? ",$," : ",'" + mScheduleWork + "',") + ParserSTEP.DoubleOptionalToString(mScheduleUsage) + "," + IfcDateTime.STEPAttribute(mScheduleStart) + "," +
				IfcDateTime.STEPAttribute(mScheduleFinish) + (mScheduleContour == "$" ? ",$," : ",'" + mScheduleContour + "',") + (mLevelingDelay == "$" ? "$," : "'" + mLevelingDelay + "',") + ParserSTEP.BoolToString(mIsOverAllocated) + "," +
				IfcDateTime.STEPAttribute(mStatusTime) + "," + (mActualWork == "$" ? "$," : "'" + mActualWork + "',") + ParserSTEP.DoubleOptionalToString(mActualUsage) + "," + IfcDateTime.STEPAttribute(mActualStart) + "," +
				IfcDateTime.STEPAttribute(mActualFinish) + (mRemainingWork == "$" ? ",$," : ",'" + mRemainingWork + "',") + ParserSTEP.DoubleOptionalToString(mRemainingUsage) + "," + ParserSTEP.DoubleOptionalToString(mCompletion);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRemainingWork = ParserSTEP.StripString(str, ref pos, len);
			mScheduleUsage = ParserSTEP.StripDouble(str, ref pos, len);
			mScheduleStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mScheduleFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mScheduleContour = ParserSTEP.StripString(str, ref pos, len);
			mLevelingDelay = ParserSTEP.StripString(str, ref pos, len);
			mIsOverAllocated = ParserSTEP.StripBool(str, ref pos, len);
			mStatusTime = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mActualWork = ParserSTEP.StripString(str, ref pos, len);
			mActualUsage = ParserSTEP.StripDouble(str, ref pos, len);
			mActualStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mActualFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mRemainingWork = ParserSTEP.StripString(str, ref pos, len);
			mRemainingUsage = ParserSTEP.StripDouble(str, ref pos, len);
			mCompletion = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRevolvedAreaSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mAxis) + "," + ParserSTEP.DoubleToString(mAngle); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxis = ParserSTEP.StripLink(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("IfcPlaneAngleMeasure(", true, System.Globalization.CultureInfo.CurrentCulture))
				mAngle = ParserSTEP.ParseDouble(s.Substring(21, str.Length - 22));
			else
				mAngle = ParserSTEP.ParseDouble(s);
		}
	}
	public partial class IfcRevolvedAreaSolidTapered
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mEndSweptArea); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mEndSweptArea = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcRibPlateProfileProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { string str = base.BuildStringSTEP(release); return (string.IsNullOrEmpty(str) || release != ReleaseVersion.IFC2x3 ? "" : str + "," + ParserSTEP.DoubleOptionalToString(mThickness) + "," + ParserSTEP.DoubleOptionalToString(mRibHeight) + "," + ParserSTEP.DoubleOptionalToString(mRibWidth) + "," + ParserSTEP.DoubleOptionalToString(mRibSpacing) + ",." + mDirection.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mRibHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mRibWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mRibSpacing = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcRibPlateDirectionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mDirection);
		}
	}
	public partial class IfcRightCircularCone
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mHeight) + "," + ParserSTEP.DoubleToString(mBottomRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mBottomRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRightCircularCylinder
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mHeight) + "," + ParserSTEP.DoubleToString(mRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRoad
	{
		public override string StepClassName { get { if (mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4X3_RC1) return "IfcFacility"; return base.StepClassName; } }
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4X3_RC3 ? "" : (mPredefinedType == IfcRoadTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release > ReleaseVersion.IFC4X3_RC2)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcRoadTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcRoof
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mPredefinedType == IfcRoofTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRoofTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcRoofType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRoofTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcRoot
	{
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			GlobalId = ParserSTEP.StripString(str, ref pos, len);
			mOwnerHistory = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcOwnerHistory;
			mName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "'" + mGlobalId + (mOwnerHistory == null ? "',$" : "',#" + mOwnerHistory.mIndex) + 
				(string.IsNullOrEmpty(mName) ? ",$," :  ",'" + ParserIfc.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$" : "'" + ParserIfc.Encode(mDescription) + "'");
		}
	}
	public partial class IfcRotationalStiffnessSelect
	{
		internal static IfcRotationalStiffnessSelect Parse(string str, ReleaseVersion version)
		{
			if (str.StartsWith("IFCBOOL"))
				return new IfcRotationalStiffnessSelect(((IfcBoolean)ParserIfc.parseSimpleValue(str)).Boolean);
			if (str.StartsWith("IFCROT"))
				return new IfcRotationalStiffnessSelect((IfcRotationalStiffnessMeasure)ParserIfc.parseDerivedMeasureValue(str));
			if (str.StartsWith("."))
				return new IfcRotationalStiffnessSelect(ParserSTEP.ParseBool(str));
			double d = ParserSTEP.ParseDouble(str), tol = 1e-9;
			if (version < ReleaseVersion.IFC4)
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
	public partial class IfcRoundedRectangleProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mRoundingRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRoundingRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
}
