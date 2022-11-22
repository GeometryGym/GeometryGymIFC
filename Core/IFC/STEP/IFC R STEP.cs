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
	public partial class IfcRailwayPart
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4X3 && release >= ReleaseVersion.IFC4X3_RC1)
				return base.BuildStringSTEP(release);
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcRailwayPartTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4X3_RC1 || release >= ReleaseVersion.IFC4X3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
				{
					if (Enum.TryParse<IfcRailwayPartTypeEnum>(s.Replace(".", ""), true, out IfcRailwayPartTypeEnum partType))
						PredefinedType = partType;
				}
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
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mWeightsData.Select(x=> ParserSTEP.DoubleToString(x))) + ")";
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + formatLength(mWallThickness) + (double.IsNaN(mInnerFilletRadius) || mInnerFilletRadius < mDatabase.Tolerance ? ",$," : "," + formatLength(mInnerFilletRadius) + ",") + (double.IsNaN(mOuterFilletRadius) || mOuterFilletRadius < mDatabase.Tolerance ? "$" : formatLength(mOuterFilletRadius)); }
	}
	public partial class IfcRectangleProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + formatLength(mXDim) + "," + formatLength(mYDim); }
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mBasisSurface.StepId + "," + ParserSTEP.DoubleToString(mU1) + "," + ParserSTEP.DoubleToString(mV1) + "," + ParserSTEP.DoubleToString(mU2) + "," + ParserSTEP.DoubleToString(mV2) + "," + ParserSTEP.BoolToString(mUsense) + "," + ParserSTEP.BoolToString(mVsense); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mBasisSurface = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPlane;
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
			return "." + mRecurrenceType.ToString() + (mDayComponent.Count == 0 ? ".,$," : ".,(" + string.Join(",", mDayComponent) + "),") +
				(mWeekdayComponent.Count == 0 ? "$," : "(" + string.Join(",", mWeekdayComponent) + "),") +
				(mMonthComponent.Count == 0 ? "$," : "(" + string.Join(",", mMonthComponent) + "),") +
				 (mInterval == int.MinValue ? "$" : mInterval.ToString()) + (mPosition == int.MinValue ? ",$" : "," + mPosition) +
				 (mOccurrences == int.MinValue ? ",$" :"," + mOccurrences) + 
				 (mTimePeriods.Count == 0 ? ",$" : string.Join(",", mTimePeriods.Select(x=>"#" + x.StepId)) + ")");
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
			mTimePeriods.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcTimePeriod));
		}
	}
	public partial class IfcReference
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mDatabase.Release < ReleaseVersion.IFC4)
				return "";
			return (string.IsNullOrEmpty(mTypeIdentifier) ? "$" : "'" + ParserSTEP.Encode(mTypeIdentifier) + "'") +
				(string.IsNullOrEmpty(mAttributeIdentifier) ? ",$," : ",'" + ParserSTEP.Encode(mAttributeIdentifier) + "',") +
				(string.IsNullOrEmpty(mInstanceName) ? "$," : "'" + ParserSTEP.Encode(mInstanceName) + "',") +
				(mListPositions.Count == 0 ? "$," : "(" + string.Join(",", mListPositions) + "),") + (mInnerReference == null ? "$" : "#" + mInnerReference.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mTypeIdentifier = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mAttributeIdentifier = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mInstanceName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("("))
				mListPositions.AddRange(ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => int.Parse(x)));
			mInnerReference = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcReference;
		}
	}
	public partial class IfcReferent
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			(mPredefinedType == IfcReferentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".") +
			(release < ReleaseVersion.IFC4X3 ? "," + ParserSTEP.DoubleOptionalToString(mRestartDistance) : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcReferentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			if(release < ReleaseVersion.IFC4X3)
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
			return ParserSTEP.DoubleToString(mTotalCrossSectionArea) + ",'" + ParserSTEP.Encode(mSteelGrade) + "'," + (mBarSurface == IfcReinforcingBarSurfaceEnum.NOTDEFINED ? "$," : "." + mBarSurface.ToString() + ".,") +
				ParserSTEP.DoubleOptionalToString(mEffectiveDepth) + "," + ParserSTEP.DoubleOptionalToString(mNominalBarDiameter) + "," + ParserSTEP.DoubleOptionalToString(mBarCount);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mTotalCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mSteelGrade = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
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
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mDefinitionType) ? ",$,(" : ",'" + ParserSTEP.Encode(mDefinitionType) + "',(")
				 + string.Join(",", mReinforcementSectionDefinitions.Select(x=>"#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDefinitionType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			ReinforcementSectionDefinitions.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcSectionReinforcementProperties));
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
			return base.BuildStringSTEP(release) + ",." + mPredefinedType + ".," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + "," +
				ParserSTEP.DoubleOptionalToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mBarLength) +
				(mBarSurface == IfcReinforcingBarSurfaceEnum.NOTDEFINED ? ",$," : ",." + mBarSurface.ToString() + ".,") +
				(string.IsNullOrEmpty(mBendingShapeCode) ? "$," : "'" + ParserSTEP.Encode(mBendingShapeCode) + "',") +
				(mBendingParameters.Count == 0 ? "$" : "(" + string.Join(",", mBendingParameters) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcReinforcingBarTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mBarLength = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcReinforcingBarSurfaceEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mBarSurface);
			mBendingShapeCode = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mBendingParameters.AddRange(ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).Select(x=>ParserIfc.parseValue(x) as IfcBendingParameterSelect));
		}
	}
	public abstract partial class IfcReinforcingElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mSteelGrade) ? ",$" : ",'" + ParserSTEP.Encode(mSteelGrade) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSteelGrade = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
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
			return base.BuildStringSTEP(release) + ",." + mPredefinedType + ".," + ParserSTEP.DoubleOptionalToString(mMeshLength) + "," +
				ParserSTEP.DoubleOptionalToString(mMeshWidth) + "," + ParserSTEP.DoubleToString(mLongitudinalBarNominalDiameter) + "," +
				ParserSTEP.DoubleToString(mTransverseBarNominalDiameter) + "," + ParserSTEP.DoubleToString(mLongitudinalBarCrossSectionArea) + "," +
				ParserSTEP.DoubleToString(mTransverseBarCrossSectionArea) + "," + ParserSTEP.DoubleToString(mLongitudinalBarSpacing) + "," +
				ParserSTEP.DoubleToString(mTransverseBarSpacing) + (string.IsNullOrEmpty(mBendingShapeCode) ? ",$," : ",'" + ParserSTEP.Encode(mBendingShapeCode) + "',") +
				(mBendingParameters.Count == 0 ? "$" : "(" + string.Join(",", mBendingParameters) + ")");
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
			mBendingShapeCode = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mBendingParameters.AddRange(ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).Select(x => ParserIfc.parseValue(x) as IfcBendingParameterSelect));
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
			return base.BuildStringSTEP(release) + ",#" + mRelatingObject.StepId + ",(" + string.Join(",", RelatedObjects.OrderBy(x=>x.StepId).Select(x => "#" + x.StepId)) + ")";
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
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", RelatedObjects.ConvertAll(x => "#" + x.StepId)) + ")," +
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
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" :
				base.BuildStringSTEP(release) + (mTimeForTask == null ? ",$" : ",#" + mTimeForTask.StepId)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			TimeForTask = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcScheduleTimeControl;
		}
	}
	public partial class IfcRelAssignsToActor
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP(release) + ",#" + RelatingActor.StepId + "," + ParserSTEP.ObjToLinkString(mRelatingActor)); }
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP(release) + ",#" + mRelatingGroup.StepId); }
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
			return base.BuildStringSTEP(release) + ",#" + mRelatingProcess.StepId + (mQuantityInProcess == null ? ",$" : ",#" + mQuantityInProcess.StepId); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingProcess = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProcessSelect;
			mQuantityInProcess = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMeasureWithUnit;
		}
	}
	public partial class IfcRelAssignsToProduct
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP(release) + ",#" + mRelatingProduct.StepId); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingProduct = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProductSelect;
		}
	}
	public partial class IfcRelAssignsToResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : 
				base.BuildStringSTEP(release) + ",#" + mRelatingResource.StepId); }
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
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mRelatedObjects.ConvertAll(x => "#" + x.StepId)) + ")";
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
	public partial class IfcRelAssociatesApproval
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release);
			return string.IsNullOrEmpty(str) ? "" : str + ",#" + RelatingApproval.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingApproval = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcApproval;
		}
	}
	public partial class IfcRelAssociatesClassification
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release);
			return string.IsNullOrEmpty(str) ? "" : str + ",#" + RelatingClassification.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingClassification = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcClassificationSelect;
		}
	}
	public partial class IfcRelAssociatesConstraint
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (RelatingConstraint == null ? "" : base.BuildStringSTEP(release) +
				(string.IsNullOrEmpty(mIntent) ? ",$," : ",'" + ParserSTEP.Encode(mIntent) + "',") + "#" + mRelatingConstraint.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIntent = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			RelatingConstraint = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcConstraint;
		}
	}
	public partial class IfcRelAssociatesDocument
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mRelatingDocument.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingDocument = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDocumentSelect;
		}
	}
	public partial class IfcRelAssociatesLibrary
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mRelatingLibrary.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingLibrary = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcLibrarySelect;
		}
	}
	public partial class IfcRelAssociatesMaterial
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string baseString = base.BuildStringSTEP(release);
			if (string.IsNullOrEmpty(baseString) || (release < ReleaseVersion.IFC4 && string.IsNullOrEmpty(RelatingMaterial.ToString())))
				return "";
			return baseString + ",#" + mRelatingMaterial.StepId;
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
			return base.BuildStringSTEP(release) + ",#" + mRelatingProfileProperties.StepId + 
				(mProfileSectionLocation == null ? ",$" : ",#" + mProfileSectionLocation.StepId) +
				(mProfileOrientation is BaseClassIfc o ? ",#" + o.StepId : (mProfileOrientation == null ? ",$" : mProfileOrientation.ToString()));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingProfileProperties = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileProperties;
			ProfileSectionLocation = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcShapeAspect;
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '#')
				mProfileOrientation = dictionary[ParserSTEP.ParseLink(s)] as IfcOrientationSelect;
			else
				mProfileOrientation = ParserIfc.parseValue(s) as IfcOrientationSelect; 
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mRelatingElement == null || mRelatedElement == null ? "" : base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(mConnectionGeometry) + ",#" + mRelatingElement.StepId + ",#" + mRelatedElement.StepId); }
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
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + ",#" + mRelatingPort.StepId + ",#" + mRelatedPort.StepId +
				(mRealizingElement == null ? ",$" : "," + mRealizingElement.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingPort = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPort;
			RelatedPort = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPort;
			RealizingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
		}
	}
	public partial class IfcRelConnectsPortToElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{ 
			return base.BuildStringSTEP(release) + ",#" + mRelatingPort.StepId + ",#" + mRelatedElement.StepId; 
		}
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
			return base.BuildStringSTEP(release) + ",#" + mRelatingStructuralMember.StepId + ",#" + mRelatedStructuralConnection.StepId +
				(mAppliedCondition == null ? ",$" : ",#" + mAppliedCondition.StepId) + (mAdditionalConditions == null ? ",$," : ",#" + mAppliedCondition.StepId + ",") +
				ParserSTEP.DoubleOptionalToString(mSupportedLength) + (mConditionCoordinateSystem == null ? ",$" : ",#" + mConditionCoordinateSystem.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingStructuralMember = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcStructuralMember;
			RelatedStructuralConnection = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcStructuralConnection;
			AppliedCondition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcBoundaryCondition;
			AdditionalConditions = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcStructuralConnectionCondition;
			SupportedLength = ParserSTEP.StripDouble(str, ref pos, len);
			ConditionCoordinateSystem = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement3D;
		}
	}
	public partial class IfcRelConnectsWithEccentricity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mConnectionConstraint.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mConnectionConstraint = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcConnectionGeometry;
		}
	}
	public partial class IfcRelConnectsWithRealizingElements
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRealizingElements.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mRealizingElements.Select(x => "#" + x.StepId)) + (string.IsNullOrEmpty(mConnectionType) ? "),$" : "),'" + ParserSTEP.Encode(mConnectionType) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RealizingElements.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcElement));
			mConnectionType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcRelContainedInSpatialStructure
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRelatedElements.Count <= 0)
				return "";
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mRelatedElements.OrderBy(x=>x.StepId).Select(x => x.StepId.ToString())) + "),#" + mRelatingStructure.StepId;
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
			return base.BuildStringSTEP(release) + ",#" + mRelatedSpace.StepId + ",(" + string.Join(",", RelatedCoverings.ConvertAll(x => "#" + x.StepId)) + ")";
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
			return base.BuildStringSTEP(release) + ",#" + mRelatingContext.StepId + ",(" + string.Join(",", mRelatedDefinitions.ConvertAll(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingContext = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcContext;
			RelatedDefinitions.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcDefinitionSelect));
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
			return base.BuildStringSTEP(release) + ",(" + String.Join(",", mRelatedPropertySets.Select(x => "#" + x.StepId)) + "),#" + mRelatingTemplate.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRelatedPropertySets.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcPropertySetDefinition));
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
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mRelatedObjects.ConvertAll(x => "#"+ x.StepId)) + "),#" + mRelatingType.StepId;
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mRelatingOpeningElement.StepId + ",#" + mRelatedBuildingElement.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingOpeningElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcOpeningElement;
			RelatedBuildingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
		}
	}
	public partial class IfcRelFlowControlElements
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mRelatingPort.StepId + ",#" + mRelatedElement.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRelatingPort = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPort;
			mRelatedElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
		}
	}
	public partial class IfcRelInterferesElements
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mRelatingElement.StepId + ",#" + mRelatedElement.StepId +
				(mInterferenceGeometry == null ? ",$" : ",#" + mInterferenceGeometry.StepId) + 
				(string.IsNullOrEmpty( mInterferenceType) ? ",$," : ",'" + ParserSTEP.Encode(mInterferenceType) + "',") + 
				ParserIfc.LogicalToString(mImpliedOrder) +
				(release > ReleaseVersion.IFC4X3_RC3 ? "," + ParserSTEP.ObjToLinkString(mInterferenceSpace) : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcInterferenceSelect;
			RelatedElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcInterferenceSelect;
			InterferenceGeometry =dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcConnectionGeometry;
			mInterferenceType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mImpliedOrder = ParserIfc.StripLogical(str, ref pos, len);
			if (release > ReleaseVersion.IFC4X3_RC3)
				mInterferenceSpace = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSpatialZone;
		}
	}
	public partial class IfcRelNests
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mRelatedObjects.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",#" + mRelatingObject.StepId + ",(" + string.Join(",", mRelatedObjects.Select(x => "#" + x.StepId)) + ")";
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
			return str + ",(" + String.Join(",", mOverridingProperties.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mOverridingProperties.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcProperty));
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mRelatingElement.StepId + ",#" + mRelatedFeatureElement.StepId; }
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
				mSequenceType + (release < ReleaseVersion.IFC4 ? "." : (string.IsNullOrEmpty(mUserDefinedSequenceType) ? ".,$" : ".,'" + ParserSTEP.Encode(mUserDefinedSequenceType) + "'"));
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
			if (release > ReleaseVersion.IFC2x3)
				mUserDefinedSequenceType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
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
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return ((release > ReleaseVersion.IFC2x3 && mRelatedBuildingElement == null) || mRelatingSpace == null ? "" : 
				base.BuildStringSTEP(release) + ",#" + mRelatingSpace.StepId + ",#" + mRelatedBuildingElement.StepId + 
				(mConnectionGeometry == null ? ",$" : ",#" + mConnectionGeometry.StepId) + ",." + mPhysicalOrVirtualBoundary.ToString() + ".,." +
				mInternalOrExternalBoundary.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingSpace = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSpaceBoundarySelect;
			RelatedBuildingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcElement;
			mConnectionGeometry = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcConnectionGeometry;
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mParentBoundary.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			ParentBoundary = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcRelSpaceBoundary1stLevel;
		}
	}
	public partial class IfcRelSpaceBoundary2ndLevel
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mCorrespondingBoundary.StepId; }
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
			return (mContextOfItems == null ? "$" : "#" + mContextOfItems.StepId) +
				(string.IsNullOrEmpty(mRepresentationIdentifier) ? ",$," : ",'" + ParserSTEP.Encode(mRepresentationIdentifier) + "',") +
				(string.IsNullOrEmpty(mRepresentationType) ? "$,(" : "'" + ParserSTEP.Encode(mRepresentationType) + "',(") +
				string.Join(",", mItems.ConvertAll(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			ContextOfItems = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcRepresentationContext;
			mRepresentationIdentifier = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mRepresentationType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			Items.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as RepresentationItem));
		}
	}
	public abstract partial class IfcRepresentationContext
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (string.IsNullOrEmpty(mContextIdentifier) ? "$," : "'" + ParserSTEP.Encode(mContextIdentifier) + "',") + (string.IsNullOrEmpty(mContextType) ? "$" : "'" + ParserSTEP.Encode(mContextType) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mContextIdentifier = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mContextType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
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
			return base.BuildStringSTEP(release) + ",#" + mRelatingConstraint.StepId + ",(" + 
				string.Join(",", mRelatedResourceObjects.Select(x=>"#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingConstraint = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcConstraint;
			RelatedResourceObjects.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcResourceObjectSelect));
		}
	}

	public abstract partial class IfcResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release > ReleaseVersion.IFC2x3 ? (string.IsNullOrEmpty(mIdentification) ? ",$," : ",'" + ParserSTEP.Encode(mIdentification) + "',") + (string.IsNullOrEmpty(mLongDescription) ? "$" : "'" + ParserSTEP.Encode(mLongDescription) + "'") : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release > ReleaseVersion.IFC2x3)
			{
				mIdentification = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mLongDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			}
		}
	}
	public abstract partial class IfcResourceLevelRelationship
	{ 
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return (mDatabase.Release < ReleaseVersion.IFC4 ? "" : 
				(string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$" : "'" + ParserSTEP.Encode(mDescription) + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			if (release > ReleaseVersion.IFC2x3)
			{
				mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			}
		}
	}
	public partial class IfcResourceTime
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mScheduleWork == "$" ? ",$," : ",'" + mScheduleWork + "',") + ParserSTEP.DoubleOptionalToString(mScheduleUsage) + "," + IfcDateTime.STEPAttribute(mScheduleStart) + "," +
				IfcDateTime.STEPAttribute(mScheduleFinish) + (mScheduleContour == "$" ? ",$," : ",'" + mScheduleContour + "',") + (mLevelingDelay == null ? "$," : "'" + mLevelingDelay.ValueString + "',") + ParserSTEP.BoolToString(mIsOverAllocated) + "," +
				IfcDateTime.STEPAttribute(mStatusTime) + "," + (mActualWork == null ? "$," : "'" + mActualWork.ValueString + "',") + ParserSTEP.DoubleOptionalToString(mActualUsage) + "," + IfcDateTime.STEPAttribute(mActualStart) + "," +
				IfcDateTime.STEPAttribute(mActualFinish) + (mRemainingWork == null ? ",$," : ",'" + mRemainingWork.ValueString + "',") + ParserSTEP.DoubleOptionalToString(mRemainingUsage) + "," + ParserSTEP.DoubleOptionalToString(mCompletion);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);

			mScheduleWork = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mScheduleUsage = ParserSTEP.StripDouble(str, ref pos, len);
			mScheduleStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mScheduleFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mScheduleContour = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripString(str, ref pos, len);
			if (s != "$")
				mLevelingDelay = IfcDuration.Convert(s);
			mIsOverAllocated = ParserSTEP.StripBool(str, ref pos, len);
			mStatusTime = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			s = ParserSTEP.StripString(str, ref pos, len);
			if (s != "$")
				mActualWork = IfcDuration.Convert(s);
			mActualUsage = ParserSTEP.StripDouble(str, ref pos, len);
			mActualStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mActualFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			s = ParserSTEP.StripString(str, ref pos, len);
			if (s != "$")
				mRemainingWork = IfcDuration.Convert(s);
			mRemainingUsage = ParserSTEP.StripDouble(str, ref pos, len);
			mCompletion = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRevolvedAreaSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mAxis.StepId + "," + ParserSTEP.DoubleToString(mAngle); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxis = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis1Placement;
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("IfcPlaneAngleMeasure(", true, System.Globalization.CultureInfo.CurrentCulture))
				mAngle = ParserSTEP.ParseDouble(s.Substring(21, str.Length - 22));
			else
				mAngle = ParserSTEP.ParseDouble(s);
		}
	}
	public partial class IfcRevolvedAreaSolidTapered
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mEndSweptArea.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mEndSweptArea = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
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
	public partial class IfcRoadPart
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4X3 && release >= ReleaseVersion.IFC4X3_RC1)
				return base.BuildStringSTEP(release);
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcRoadPartTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4X3_RC1 || release >= ReleaseVersion.IFC4X3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
				{
					if (Enum.TryParse<IfcRoadPartTypeEnum>(s.Replace(".", ""), true, out IfcRoadPartTypeEnum partType))
						PredefinedType = partType;
				}
			}
		}
	}
	public partial class IfcRoof
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + 
				(release > ReleaseVersion.IFC2x3 && mPredefinedType == IfcRoofTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}

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
			string globalId = ParserSTEP.StripString(str, ref pos, len);
			if (string.Compare(GlobalId, globalId, true) != 0)
				GlobalId = globalId;
			mOwnerHistory = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcOwnerHistory;
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "'" + mGlobalId + (mOwnerHistory == null ? "',$" : "',#" + mOwnerHistory.StepId) + 
				(string.IsNullOrEmpty(mName) ? ",$," :  ",'" + ParserSTEP.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$" : "'" + ParserSTEP.Encode(mDescription) + "'");
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
