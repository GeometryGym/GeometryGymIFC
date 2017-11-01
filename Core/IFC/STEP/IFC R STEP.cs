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
	public partial class IfcRailing : IfcBuildingElement
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRailingTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcRailingType : IfcBuildingElementType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRailingTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcRamp : IfcBuildingElement
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRampTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcRampFlight : IfcBuildingElement
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? base.BuildStringSTEP() : base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcRampFlightTypeEnum>(s.Substring(1, s.Length - 2), out mPredefinedType);
			}
		}
	}
	public partial class IfcRampFlightType : IfcBuildingElementType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcRampFlightTypeEnum>(s.Substring(1, s.Length - 2), out mPredefinedType);
		}
	}
	public partial class IfcRampType : IfcBuildingElementType //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRampTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcRationalBezierCurve : IfcBezierCurve // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.DoubleToString(mWeightsData[0]);
			for (int icounter = 1; icounter < mWeightsData.Count; icounter++)
				str += "," + ParserSTEP.DoubleToString(mWeightsData[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> arrNodes = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			for (int icounter = 0; icounter < arrNodes.Count; icounter++)
				mWeightsData.Add(ParserSTEP.ParseDouble(arrNodes[icounter]));
		}
		
	}
	public partial class IfcRationalBSplineCurveWithKnots : IfcBSplineCurveWithKnots
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.DoubleToString(mWeightsData[0]);
			for (int icounter = 1; icounter < mWeightsData.Count; icounter++)
				str += "," + ParserSTEP.DoubleToString(mWeightsData[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mWeightsData = ParserSTEP.StripListDouble(str, ref pos, len);
		}
	}
	public partial class IfcRationalBSplineSurfaceWithKnots : IfcBSplineSurfaceWithKnots
	{
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mWeightsData = ParserSTEP.StripListListDouble(str, ref pos, len);
		}
	}
	public partial class IfcRectangleHollowProfileDef : IfcRectangleProfileDef
	{
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mWallThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mInnerFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mOuterFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRectangleProfileDef : IfcParameterizedProfileDef //	SUPERTYPE OF(ONEOF(IfcRectangleHollowProfileDef, IfcRoundedRectangleProfileDef))
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mXDim) + "," + ParserSTEP.DoubleToString(mYDim); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mXDim = ParserSTEP.StripDouble(str, ref pos, len);
			mYDim = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRectangularPyramid : IfcCsgPrimitive3D
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mXLength) + "," + ParserSTEP.DoubleToString(mYLength) + "," + ParserSTEP.DoubleToString(mHeight); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mXLength = ParserSTEP.StripDouble(str, ref pos, len);
			mYLength = ParserSTEP.StripDouble(str, ref pos, len);
			mHeight = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRectangularTrimmedSurface : IfcBoundedSurface
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.DoubleToString(mU1) + "," + ParserSTEP.DoubleToString(mV1) + "," + ParserSTEP.DoubleToString(mU2) + "," + ParserSTEP.DoubleToString(mV2) + "," + ParserSTEP.BoolToString(mUsense) + "," + ParserSTEP.BoolToString(mVsense); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
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
	public partial class IfcRecurrencePattern : BaseClassIfc // IFC4
	{
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			Enum.TryParse<IfcRecurrenceTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mRecurrenceType);
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
	public partial class IfcReference : BaseClassIfc, IfcMetricValueSelect, IfcAppliedValueSelect // IFC4
	{
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mTypeIdentifier = ParserSTEP.StripString(str, ref pos, len);
			mAttributeIdentifier = ParserSTEP.StripString(str, ref pos, len);
			mInstanceName = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("("))
				mListPositions = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => int.Parse(x));
			mInnerReference = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcReferencesValueDocument; // DEPRECEATED IFC4
	//ENTITY IfcRegularTimeSeries
	public partial class IfcReinforcementBarProperties : IfcPreDefinedProperties
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mTotalCrossSectionArea) + ",'" + mSteelGrade + "'," + ( mBarSurface == IfcReinforcingBarSurfaceEnum.NOTDEFINED ? "$," : "." + mBarSurface.ToString() + ".,") +
				ParserSTEP.DoubleOptionalToString(mEffectiveDepth) + "," + ParserSTEP.DoubleOptionalToString(mNominalBarDiameter) + "," + ParserSTEP.DoubleOptionalToString(mBarCount);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mTotalCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mSteelGrade = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcReinforcingBarSurfaceEnum>(s.Replace(".", ""), out mBarSurface);
			mEffectiveDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mNominalBarDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mBarCount = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcReinforcementDefinitionProperties : IfcPreDefinedPropertySet //IFC2x3 IfcPropertySetDefinition
	{
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + (mDefinitionType == "$" ? ",$,(#" : ",'" + mDefinitionType + "',(#") + mReinforcementSectionDefinitions[0];
			for (int icounter = 1; icounter < mReinforcementSectionDefinitions.Count; icounter++)
				result += ",#" + mReinforcementSectionDefinitions;
			return result + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mDefinitionType = ParserSTEP.StripString(str, ref pos, len);
			mReinforcementSectionDefinitions = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcReinforcingBar : IfcReinforcingElement
	{
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + "," + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ParserSTEP.DoubleToString(mNominalDiameter) + "," + ParserSTEP.DoubleToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mBarLength) + ",." + mPredefinedType.ToString() + ".," :
				ParserSTEP.DoubleOptionalToString(mNominalDiameter) + "," + ParserSTEP.DoubleOptionalToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mBarLength) + (mPredefinedType == IfcReinforcingBarTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,"));
			return result + (mBarSurface == IfcReinforcingBarSurfaceEnum.NOTDEFINED ? "$" : "." + mBarSurface.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mBarLength = ParserSTEP.StripDouble(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcReinforcingBarTypeEnum>(s.Replace(".", ""), out mPredefinedType);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (str.StartsWith("."))
				Enum.TryParse<IfcReinforcingBarSurfaceEnum>(s.Replace(".", ""), out mBarSurface);
		}
	}
	public partial class IfcReinforcingBarType : IfcReinforcingElementType  //IFC4
	{
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP();
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			Enum.TryParse<IfcReinforcingBarTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mPredefinedType);
			mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mBarLength = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcReinforcingBarSurfaceEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mBarSurface);
			mBendingShapeCode = ParserSTEP.StripString(str, ref pos, len);
			//t.mBendingParameters = 
			ParserSTEP.StripField(str, ref pos, len);
		}
	}
	public abstract partial class IfcReinforcingElement : IfcElementComponent //	ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcingBar, IfcReinforcingMesh, IfcTendon, IfcTendonAnchor))
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mSteelGrade == "$" ? ",$" : ",'" + mSteelGrade + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mSteelGrade = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcReinforcingMesh : IfcReinforcingElement
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mMeshLength) + "," + ParserSTEP.DoubleOptionalToString(mMeshWidth) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarNominalDiameter) + "," + ParserSTEP.DoubleToString(mTransverseBarNominalDiameter) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarCrossSectionArea) + "," + ParserSTEP.DoubleToString(mTransverseBarCrossSectionArea) + "," +
				 ParserSTEP.DoubleToString(mLongitudinalBarSpacing) + "," + ParserSTEP.DoubleToString(mTransverseBarSpacing) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcReinforcingMeshTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + "."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
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
					Enum.TryParse<IfcReinforcingMeshTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mPredefinedType);
			}
		}
	}
	public partial class IfcReinforcingMeshType : IfcReinforcingElementType
	{
		protected override string BuildStringSTEP()
		{
			if (mDatabase.Release == ReleaseVersion.IFC2x3)
				return "";
			string result = base.BuildStringSTEP() + ",." + mPredefinedType + ".," + ParserSTEP.DoubleOptionalToString(mMeshLength) + "," + 
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcReinforcingMeshTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mPredefinedType);
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
	public partial class IfcRelAggregates : IfcRelDecomposes
	{
		protected override string BuildStringSTEP()
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
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingObject) + ",(" + str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingObject = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingObject.mIsDecomposedBy.Add(this);
			ReadOnlyCollection<IfcObjectDefinition> ods = RelatedObjects;
			for (int icounter = 0; icounter < ods.Count; icounter++)
			{
				if (ods[icounter] != null)
					ods[icounter].mDecomposes = this;
			}
		}
	}
	public abstract partial class IfcRelAssigns : IfcRelationship //	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelAssignsToActor, IfcRelAssignsToControl, IfcRelAssignsToGroup, IfcRelAssignsToProcess, IfcRelAssignsToProduct, IfcRelAssignsToResource))
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			if (mRelatedObjects.Count > 200)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(str);
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					sb.Append(",#" + mRelatedObjects[icounter]);
				sb.Append(")," + (mDatabase.Release == ReleaseVersion.IFC2x3 && mRelatedObjectsType != IfcObjectTypeEnum.NOTDEFINED ? "." + mRelatedObjectsType + "." : "$"));
				return sb.ToString();
			}
			else
			{
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					str += ",#" + mRelatedObjects[icounter];
				return str + ")," + (mDatabase.Release == ReleaseVersion.IFC2x3 && mRelatedObjectsType != IfcObjectTypeEnum.NOTDEFINED ? "." + mRelatedObjectsType + "." : "$");
			}
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos, len);
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (!Enum.TryParse<IfcObjectTypeEnum>(field.Replace(".",""),true, out mRelatedObjectsType))
				mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			ReadOnlyCollection<IfcObjectDefinition> ods = RelatedObjects;
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
		}
	}
	public partial class IfcRelAssignsTasks : IfcRelAssignsToControl // IFC4 depreceated
	{
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mTimeForTask)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mTimeForTask = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcScheduleTimeControl t = TimeForTask;
			if (t != null)
				t.mScheduleTimeControlAssigned = this;
		}
	}
	public partial class IfcRelAssignsToActor : IfcRelAssigns
	{
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingActor)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingActor = ParserSTEP.StripLink(str, ref pos, len); mActingRole = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcActor c = RelatingActor;
			if (c != null)
				c.mIsActingUpon.Add(this);
		}
	}
	public partial class IfcRelAssignsToControl : IfcRelAssigns
	{
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingControl)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingControl = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcControl c = RelatingControl;
			if (c != null)
				c.mControls.Add(this);
		}
	}
	public partial class IfcRelAssignsToGroup : IfcRelAssigns 	//SUPERTYPE OF(IfcRelAssignsToGroupByFactor)
	{
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingGroup)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingGroup = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcGroup g = RelatingGroup;
			if (g != null)
				g.mIsGroupedBy.Add(this);
		}
	}
	public partial class IfcRelAssignsToGroupByFactor : IfcRelAssignsToGroup //IFC4
	{
		protected override string BuildStringSTEP() { return (mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + (mDatabase.Release == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.DoubleToString(mFactor))); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mFactor = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRelAssignsToProcess : IfcRelAssigns
	{
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingProcess)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingProcess = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcProcess p = RelatingProcess;
			if (p != null)
				p.mHasAssignments.Add(this);
		}
	}
	public partial class IfcRelAssignsToProduct : IfcRelAssigns
	{
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingProduct)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingProduct = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcProductSelect p = RelatingProduct;
			if (p != null)
				p.Assign(this);
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelAssignsToProjectOrder // DEPRECEATED IFC4 
	public partial class IfcRelAssignsToResource : IfcRelAssigns
	{
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingResource)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingResource = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcResource r = RelatingResource;
			if (r != null)
				r.mResourceOf.Add(this);
		}
	}
	public abstract partial class IfcRelAssociates : IfcRelationship   //ABSTRACT SUPERTYPE OF (ONEOF(IfcRelAssociatesApproval,IfcRelAssociatesclassification,IfcRelAssociatesConstraint,IfcRelAssociatesDocument,IfcRelAssociatesLibrary,IfcRelAssociatesMaterial))
	{
		protected override string BuildStringSTEP()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(#" + mRelatedObjects[0];
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			ReadOnlyCollection<IfcDefinitionSelect> objects = RelatedObjects;
			for (int icounter = 0; icounter < objects.Count; icounter++)
			{
				IfcDefinitionSelect r = objects[icounter];
				if (r != null)
					r.Associate(this);
			}
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelAssociatesAppliedValue // DEPRECEATED IFC4
	//ENTITY IfcRelAssociatesApproval
	public partial class IfcRelAssociatesClassification : IfcRelAssociates
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingClassification); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingClassification = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingClassification.ClassifyObjects(this);
		}
	}
	public partial class IfcRelAssociatesConstraint : IfcRelAssociates
	{
		protected override string BuildStringSTEP() { return (RelatingConstraint == null ? "" : base.BuildStringSTEP() + (mIntent == "$" ? ",$," : ",'" + mIntent + "',") + ParserSTEP.LinkToString(mRelatingConstraint)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mIntent = ParserSTEP.StripString(str, ref pos, len);
			mRelatingConstraint = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingConstraint.mConstraintForObjects.Add(this);
		}
	}
	public partial class IfcRelAssociatesDocument : IfcRelAssociates
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingDocument); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingDocument = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcRelAssociatesLibrary : IfcRelAssociates
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingLibrary); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingLibrary = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcRelAssociatesMaterial : IfcRelAssociates
	{
		protected override string BuildStringSTEP()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3 && string.IsNullOrEmpty(RelatingMaterial.ToString()))
				return "";
			return base.BuildStringSTEP() + ",#" + mRelatingMaterial;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingMaterial = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcMaterialSelect ms = RelatingMaterial;
			if (ms != null)
				ms.Associates = this;
		}
	}
	public partial class IfcRelAssociatesProfileProperties : IfcRelAssociates //IFC4 DELETED Replaced by IfcRelAssociatesMaterial together with material-profile sets
	{
		protected override string BuildStringSTEP()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingProfileProperties) + "," +
				ParserSTEP.LinkToString(mProfileSectionLocation) + ",";
			if (double.IsNaN(mProfileOrientationValue))
			{
				if (mProfileOrientation == 0)
					return str + "$";
				return str + ",IFCPLANEANGLEMEASURE(" + ParserSTEP.DoubleToString(mProfileOrientation) + ")";
			}
			return str + ParserSTEP.LinkToString((int)mProfileOrientation);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
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
	public partial class IfcRelaxation : BaseClassIfc// DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mRelaxationValue) + "," + ParserSTEP.DoubleToString(mInitialStress); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mRelaxationValue = ParserSTEP.StripDouble(str, ref pos, len);
			mInitialStress = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRelConnectsElements : IfcRelConnects //	SUPERTYPE OF(ONEOF(IfcRelConnectsPathElements, IfcRelConnectsWithRealizingElements))
	{
		protected override string BuildStringSTEP() { return (mRelatingElement == 0 || mRelatedElement == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mConnectionGeometry) + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedElement)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mConnectionGeometry = ParserSTEP.StripLink(str, ref pos, len);
			mRelatingElement = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedElement = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcElement relating = RelatingElement, related = RelatedElement;
			if (relating != null)
				relating.mConnectedTo.Add(this);
			if (related != null)
				related.mConnectedFrom.Add(this);
		}
	}
	public partial class IfcRelConnectsPathElements : IfcRelConnectsElements
	{
		protected override string BuildStringSTEP()
		{
			if (mRelatingElement == 0 || mRelatedElement == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(";
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingPriorities = ParserSTEP.StripListLink(str, ref pos, len);
			mRelatedPriorities = ParserSTEP.StripListLink(str, ref pos, len);
			Enum.TryParse<IfcConnectionTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mRelatedConnectionType);
			Enum.TryParse<IfcConnectionTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mRelatingConnectionType);
		}
	}
	public partial class IfcRelConnectsPorts : IfcRelConnects
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedPort) + "," + ParserSTEP.LinkToString(mRealizingElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingPort = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedPort = ParserSTEP.StripLink(str, ref pos, len);
			mRealizingElement = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingPort.mConnectedFrom = this;
			RelatedPort.mConnectedTo = this;
		}
	}
	public partial class IfcRelConnectsPortToElement : IfcRelConnects
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingPort = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedElement = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingPort.mContainedIn = this;
			RelatedElement.mHasPorts.Add(this);
		}
	}
	public partial class IfcRelConnectsStructuralActivity : IfcRelConnects
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedStructuralActivity); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingElement = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedStructuralActivity = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatedStructuralActivity.AssignedToStructuralItem = this;
			IfcStructuralActivityAssignmentSelect saa = RelatingElement;
			saa.AssignStructuralActivity(this);
		}
	}
	public partial class IfcRelConnectsStructuralElement : IfcRelConnects //DELETED IFC4 Replaced by IfcRelAssignsToProduct
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedStructuralMember); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingElement = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedStructuralMember = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcElement element = RelatingElement;
			if (element != null)
				element.mHasStructuralMember.Add(this);
			IfcStructuralMember member = RelatedStructuralMember;
			if(member != null)
				member.mStructuralMemberForGG = this;
		}
	}
	public partial class IfcRelConnectsStructuralMember : IfcRelConnects
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingStructuralMember) + "," + ParserSTEP.LinkToString(mRelatedStructuralConnection) + "," + ParserSTEP.LinkToString(mAppliedCondition) + "," + ParserSTEP.LinkToString(mAdditionalConditions) + "," + ParserSTEP.DoubleToString(mSupportedLength) + "," + ParserSTEP.LinkToString(mConditionCoordinateSystem); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingStructuralMember = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedStructuralConnection = ParserSTEP.StripLink(str, ref pos, len);
			mAppliedCondition = ParserSTEP.StripLink(str, ref pos, len);
			mAdditionalConditions = ParserSTEP.StripLink(str, ref pos, len);
			mSupportedLength = ParserSTEP.StripDouble(str, ref pos, len);
			mConditionCoordinateSystem = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcStructuralMember m = RelatingStructuralMember;
			if (m != null)
				m.mConnectedBy.Add(this);
			IfcStructuralConnection c = mDatabase[mRelatedStructuralConnection] as IfcStructuralConnection;
			if (c != null)
				c.mConnectsStructuralMembers.Add(this);
		}
	}
	public partial class IfcRelConnectsWithEccentricity : IfcRelConnectsStructuralMember
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mConnectionConstraint); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mConnectionConstraint = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcRelConnectsWithRealizingElements : IfcRelConnectsElements
	{
		protected override string BuildStringSTEP()
		{
			if (mRealizingElements.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRealizingElements[0]);
			for (int icounter = 1; icounter < mRealizingElements.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRealizingElements[icounter]);
			return str + (mConnectionType == "$" ? "),$" : "),'" + mConnectionType + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRealizingElements = ParserSTEP.StripListLink(str, ref pos, len);
			mConnectionType = ParserSTEP.StripString(str, ref pos, len); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			for (int icounter = 0; icounter < mRealizingElements.Count; icounter++)
			{
				IfcElement e = mDatabase[mRealizingElements[icounter]] as IfcElement;
				if (e != null)
					e.mIsConnectionRealization.Add(this);
			}
		}
	}
	public partial class IfcRelContainedInSpatialStructure : IfcRelConnects
	{
		protected override string BuildStringSTEP()
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
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
					{
						sb.Append(",(#" + mRelatedElements[0]);
						break;
					}
				}
				for (icounter++; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
						sb.Append(",#" + mRelatedElements[icounter]);
				}
				list = sb.ToString();
			}
			else
			{
				for (icounter = 0; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
					{
						list = ",(#" + mRelatedElements[0];
						break;
					}

				}
				for (icounter++; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
						list += ",#" + mRelatedElements[icounter];
				}
			}
			if (string.IsNullOrEmpty(list))
				return "";
			return base.BuildStringSTEP() + list + "),#" + mRelatingStructure;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatedElements = ParserSTEP.StripListLink(str, ref pos, len);
			mRelatingStructure = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcSpatialElement se = RelatingStructure;
			if (se != null)
				se.mContainsElements.Add(this);
			//foreach(int i in mRelatedElements)
			//{
			//	IfcProduct p = mDatabase[i] as IfcProduct;
			//	if (p != null)
			//		addRelated(p);
			//		//RelatedElements.Add(p);
			//}
			foreach (IfcProduct p in RelatedElements)
				relate(p);
		}
	}
	public partial class IfcRelCoversBldgElements : IfcRelConnects //IFC4 DEPRECATION  The relationship IfcRelCoversBldgElements shall not be used anymore, use IfcRelAggregates instead.
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingBuildingElement) + ",(";
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatedCoverings = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcElement e = RelatingBuildingElement;
			if (e != null)
				e.mHasCoverings.Add(this);
			ReadOnlyCollection<IfcCovering> coverings = RelatedCoverings;
			for (int icounter = 0; icounter < coverings.Count; icounter++)
				coverings[icounter].mCoversElements = this;
		}
	}
	public partial class IfcRelCoversSpaces : IfcRelConnects //IFC4 DEPRECATION  The relationship IfcRelCoversSpace shall not be used anymore, use IfcRelContainedInSpatialStructure instead.
	{
		protected override string BuildStringSTEP()
		{
			if (mRelatedCoverings.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",#" + mRelatedSpace + ",(#" + mRelatedCoverings[0];
			for (int icounter = 1; icounter < mRelatedCoverings.Count; icounter++)
				str += ",#" + mRelatedCoverings[icounter];
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatedSpace = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedCoverings = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatedSpace.mHasCoverings.Add(this);
			for (int icounter = 0; icounter < mRelatedCoverings.Count; icounter++)
			{
				IfcCovering cov = mDatabase[mRelatedCoverings[icounter]] as IfcCovering;
				if (cov != null)
					cov.mCoversSpaces = this;
			}
		}
	}
	public partial class IfcRelDeclares : IfcRelationship //IFC4
	{
		protected override string BuildStringSTEP()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mRelatingContext == 0 || mRelatedDefinitions.Count == 0)
				return "";
			string str = ",(" + ParserSTEP.LinkToString(mRelatedDefinitions[0]);
			for (int icounter = 1; icounter < mRelatedDefinitions.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedDefinitions[icounter]);
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingContext) + str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingContext = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedDefinitions = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingContext.mDeclares.Add(this);
			for (int icounter = 0; icounter < mRelatedDefinitions.Count; icounter++)
				(mDatabase[mRelatedDefinitions[icounter]] as IfcDefinitionSelect).HasContext = this;
		}
	}
	public partial class IfcRelDefinesByObject : IfcRelDefines
	{
		protected override string BuildStringSTEP()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingObject);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos, len);
			mRelatingObject = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mRelatingObject > 0)
			{
				IfcObject ot = mDatabase[mRelatingObject] as IfcObject;
				if (ot != null)
					ot.mIsDeclaredBy = this;
			}
			ReadOnlyCollection<IfcObject> objects = RelatedObjects;
			for (int icounter = 0; icounter < objects.Count; icounter++)
			{
				IfcObject o = objects[icounter];
				if (o != null)
					o.mDeclares.Add(this);
			}
		}
	}
	public partial class IfcRelDefinesByProperties : IfcRelDefines
	{
		protected override string BuildStringSTEP()
		{
			IfcPropertySetDefinition pset = RelatingPropertyDefinition;
			if (mRelatedObjects.Count == 0 || pset == null || pset.isEmpty)
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingPropertyDefinition);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos, len);
			mRelatingPropertyDefinition = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingPropertyDefinition.DefinesOccurrence = this;
			ReadOnlyCollection<IfcObjectDefinition> related = RelatedObjects;
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
	public partial class IfcRelDefinesByTemplate : IfcRelDefines //IFC4
	{
		protected override string BuildStringSTEP()
		{
			if (mDatabase.Release == ReleaseVersion.IFC2x3 || mRelatedPropertySets.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedPropertySets[0]);
			for (int icounter = 1; icounter < mRelatedPropertySets.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedPropertySets[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingTemplate);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatedPropertySets = ParserSTEP.StripListLink(str, ref pos, len);
			mRelatingTemplate = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mRelatingTemplate > 0)
			{
				IfcPropertySetTemplate rt = RelatingTemplate;
				if (rt != null)
					rt.mDefines.Add(this);
			}
			ReadOnlyCollection<IfcPropertySetDefinition> psets = RelatedPropertySets;
			for (int icounter = 0; icounter < psets.Count; icounter++)
			{ 
				IfcPropertySetDefinition pset = psets[icounter];
				if (pset != null)
					pset.mIsDefinedBy.Add(this);
			}
		}
	}
	public partial class IfcRelDefinesByType : IfcRelDefines
	{
		protected override string BuildStringSTEP()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			IfcTypeObject to = RelatingType;
			if (to == null || string.IsNullOrEmpty(to.ToString()))
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingType);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos, len);
			mRelatingType = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mRelatingType > 0)
			{
				IfcTypeObject ot = RelatingType;
				if (ot != null)
					ot.mObjectTypeOf = this;
			}
			for (int icounter = 0; icounter < mRelatedObjects.Count; icounter++)
			{
				IfcObject o = mDatabase[mRelatedObjects[icounter]] as IfcObject;
				if (o != null)
					o.IsTypedBy = this;
			}
		}
	}
	public partial class IfcRelFillsElement : IfcRelConnects
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingOpeningElement) + "," + ParserSTEP.LinkToString(mRelatedBuildingElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingOpeningElement = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedBuildingElement = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatedBuildingElement.mFillsVoids.Add(this);
			RelatingOpeningElement.mHasFillings.Add(this);
		}
	}
	public partial class IfcRelFlowControlElements : IfcRelConnects
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingPort = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedElement = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelInteractionRequirements  // DEPRECEATED IFC4
	public partial class IfcRelInterferesElements : IfcRelConnects
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedElement) + "," + 
				ParserSTEP.LinkToString(mInterferenceGeometry) + (mInterferenceType == "$" ? ",$," : ",'" + mInterferenceType + "',") + ParserIfc.LogicalToString(mImpliedOrder);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingElement = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedElement = ParserSTEP.StripLink(str, ref pos, len);
			mInterferenceGeometry = ParserSTEP.StripLink(str, ref pos, len);
			mInterferenceType = ParserSTEP.StripString(str, ref pos, len);
			mImpliedOrder = ParserIfc.StripLogical(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingElement.mInterferesElements.Add(this);
			RelatedElement.mIsInterferedByElements.Add(this);
		}
	}
	public partial class IfcRelNests : IfcRelDecomposes
	{
		protected override string BuildStringSTEP()
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
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingObject) + ",(" + str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingObject = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcObjectDefinition relating = RelatingObject;
			relating.relateNested(this);
			ReadOnlyCollection<IfcObjectDefinition> ods = RelatedObjects;
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
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelOccupiesSpaces // DEPRECEATED IFC4
	public partial class IfcRelOverridesProperties : IfcRelDefinesByProperties // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP();
			if (string.IsNullOrEmpty(str))
				return "";
			str += ",(#" + mOverridingProperties[0];
			for (int icounter = 1; icounter < mOverridingProperties.Count; icounter++)
				str += ",#" + mOverridingProperties[icounter];
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mOverridingProperties = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcRelProjectsElement : IfcRelDecomposes // IFC2x3 IfcRelDecomposes
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedFeatureElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingElement = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedFeatureElement = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingElement.mHasProjections.Add(this);
			RelatedFeatureElement.mProjectsElements.Add(this);
		}
	}
	public partial class IfcRelReferencedInSpatialStructure : IfcRelConnects
	{
		protected override string BuildStringSTEP()
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
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
					{
						sb.Append(",(#" + mRelatedElements[0]);
						break;
					}
				}
				for (icounter++; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
						sb.Append(",#" + mRelatedElements[icounter]);
				}
				list = sb.ToString();
			}
			else
			{
				for (icounter = 0; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
					{
						list = ",(#" + mRelatedElements[0];
						break;
					}

				}
				for (icounter++; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
						list += ",#" + mRelatedElements[icounter];
				}
			}
			return base.BuildStringSTEP() + list + "),#" + mRelatingStructure;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatedElements = ParserSTEP.StripListLink(str, ref pos, len);
			mRelatingStructure = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcSpatialElement se = RelatingStructure;
			if (se != null)
				se.mReferencesElements.Add(this);
			ReadOnlyCollection<IfcProduct> products = RelatedElements;
			for (int icounter = 0; icounter < products.Count; icounter++)
				relate(products[icounter] as IfcProduct);
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelSchedulesCostItems // DEPRECEATED IFC4 
	public partial class IfcRelSequence : IfcRelConnects
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingProcess) + "," + ParserSTEP.LinkToString(mRelatedProcess) + "," + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ParserSTEP.DoubleToString(mTimeLag) :
				ParserSTEP.LinkToString((int)mTimeLag)) + ",." + mSequenceType + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "." : (mUserDefinedSequenceType == "$" ? ".,$" : ".,'" + mUserDefinedSequenceType + "'"));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingProcess = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedProcess = ParserSTEP.StripLink(str, ref pos, len);
			if (release == ReleaseVersion.IFC2x3)
				mTimeLag = ParserSTEP.StripDouble(str, ref pos, len);
			else
				mTimeLag = ParserSTEP.StripLink(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				Enum.TryParse<IfcSequenceEnum>(s.Replace(".", ""), out mSequenceType);
			if (release != ReleaseVersion.IFC2x3)
				mUserDefinedSequenceType = ParserSTEP.StripString(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingProcess.mIsPredecessorTo.Add(this);
			RelatedProcess.mIsSuccessorFrom.Add(this);
		}
	}
	public partial class IfcRelServicesBuildings : IfcRelConnects
	{
		protected override string BuildStringSTEP()
		{
			if (mRelatedBuildings.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingSystem) + ",(" + ParserSTEP.LinkToString(mRelatedBuildings[0]);
			for (int icounter = 1; icounter < mRelatedBuildings.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedBuildings[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingSystem = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedBuildings = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingSystem.mServicesBuildings = this;
			for (int icounter = 0; icounter < mRelatedBuildings.Count; icounter++)
			{
				IfcSpatialStructureElement se = mDatabase[mRelatedBuildings[icounter]] as IfcSpatialStructureElement;
				if (se != null)
					se.mServicedBySystems.Add(this);
			}
		}
	}
	public partial class IfcRelSpaceBoundary : IfcRelConnects
	{
		protected override string BuildStringSTEP() { return (mRelatedBuildingElement == 0 || mRelatingSpace == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingSpace) + "," + ParserSTEP.LinkToString(mRelatedBuildingElement) + "," + ParserSTEP.LinkToString(mConnectionGeometry) + ",." + mPhysicalOrVirtualBoundary.ToString() + ".,." + mInternalOrExternalBoundary.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingSpace = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedBuildingElement = ParserSTEP.StripLink(str, ref pos, len);
			mConnectionGeometry = ParserSTEP.StripLink(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				Enum.TryParse<IfcPhysicalOrVirtualEnum>(s.Replace(".", ""), out mPhysicalOrVirtualBoundary);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				Enum.TryParse<IfcInternalOrExternalEnum>(s.Replace(".", ""), out mInternalOrExternalBoundary);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingSpace.AddBoundary(this);
			IfcElement e = RelatedBuildingElement;
			if (e != null)
				e.mProvidesBoundaries.Add(this);
		}
	}
	public partial class IfcRelSpaceBoundary1stLevel : IfcRelSpaceBoundary
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mParentBoundary); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mParentBoundary = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcRelSpaceBoundary1stLevel s = mDatabase[mParentBoundary] as IfcRelSpaceBoundary1stLevel;
			s.mInnerBoundaries.Add(this);
		}
	}
	public partial class IfcRelSpaceBoundary2ndLevel : IfcRelSpaceBoundary1stLevel
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mCorrespondingBoundary); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mCorrespondingBoundary = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			CorrespondingBoundary.mCorresponds.Add(this);
		}
	}
	public partial class IfcRelVoidsElement : IfcRelDecomposes // Ifc2x3 IfcRelConnects
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingBuildingElement) + "," + ParserSTEP.LinkToString(mRelatedOpeningElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingBuildingElement = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedOpeningElement = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcElement elem = RelatingBuildingElement;
			if (elem != null)
				elem.mHasOpenings.Add(this);
			IfcFeatureElementSubtraction es = RelatedOpeningElement;
			if (es != null)
				es.mVoidsElement = this;
		}
	}
	public partial class IfcReparametrisedCompositeCurveSegment : IfcCompositeCurveSegment
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mParamLength.ToString(); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mParamLength = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRepresentation : BaseClassIfc, IfcLayeredItem // Abstract IFC4 ,SUPERTYPE OF (ONEOF(IfcShapeModel,IfcStyleModel));
	{
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mContextOfItems = ParserSTEP.StripLink(str, ref pos, len);
			mRepresentationIdentifier = ParserSTEP.StripString(str, ref pos, len);
			mRepresentationType = ParserSTEP.StripString(str, ref pos, len);
			mItems = ParserSTEP.StripListLink(str, ref pos, len);
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
	}
	public abstract partial class IfcRepresentationContext : BaseClassIfc //ABSTRACT SUPERTYPE OF(IfcGeometricRepresentationContext);
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mContextIdentifier == "$" ? ",$," : ",'" + mContextIdentifier + "',") + (mContextType == "$" ? "$" : "'" + mContextType + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mContextIdentifier = ParserSTEP.StripString(str, ref pos, len);
			mContextType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcRepresentationMap : BaseClassIfc, IfcProductRepresentationSelect
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mMappingOrigin) + "," + ParserSTEP.LinkToString(mMappedRepresentation); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mMappingOrigin = ParserSTEP.StripLink(str, ref pos, len);
			mMappedRepresentation = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcResourceConstraintRelationship : IfcResourceLevelRelationship  // IfcPropertyConstraintRelationship; // DEPRECEATED IFC4 renamed
	{
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingConstraint) + ",(#" + mRelatedResourceObjects[0];
			for (int icounter = 1; icounter < mRelatedResourceObjects.Count; icounter++)
				result += ",#" + mRelatedResourceObjects[icounter];
			return result + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingConstraint = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedResourceObjects = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingConstraint.mPropertiesForConstraint.Add(this);
			foreach(IfcResourceObjectSelect related in RelatedResourceObjects)
				related.AddConstraintRelationShip(this);
		}
	}
	public abstract partial class IfcResourceLevelRelationship : BaseClassIfc //IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcApprovalRelationship,
	{ // IfcCurrencyRelationship, IfcDocumentInformationRelationship, IfcExternalReferenceRelationship, IfcMaterialRelationship, IfcOrganizationRelationship, IfcPropertyDependencyRelationship, IfcResourceApprovalRelationship, IfcResourceConstraintRelationship));
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.Release == ReleaseVersion.IFC2x3 ? "" : (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			if (release != ReleaseVersion.IFC2x3)
			{
				mName = ParserSTEP.StripString(str, ref pos, len);
				mDescription = ParserSTEP.StripString(str, ref pos, len);
			}
		}
	}
	public partial class IfcResourceTime : IfcSchedulingTime //IFC4
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mScheduleWork == "$" ? ",$," : ",'" + mScheduleWork + "',") + ParserSTEP.DoubleOptionalToString(mScheduleUsage) + "," + IfcDateTime.formatSTEP(mScheduleStart) + "," +
				IfcDateTime.formatSTEP(mScheduleFinish) + (mScheduleContour == "$" ? ",$," : ",'" + mScheduleContour + "',") + (mLevelingDelay == "$" ? "$," : "'" + mLevelingDelay + "',") + ParserSTEP.BoolToString(mIsOverAllocated) + "," +
				IfcDateTime.formatSTEP(mStatusTime) + "," + (mActualWork == "$" ? "$," : "'" + mActualWork + "',") + ParserSTEP.DoubleOptionalToString(mActualUsage) + "," + IfcDateTime.formatSTEP(mActualStart) + "," +
				IfcDateTime.formatSTEP(mActualFinish) + (mRemainingWork == "$" ? ",$," : ",'" + mRemainingWork + "',") + ParserSTEP.DoubleOptionalToString(mRemainingUsage) + "," + ParserSTEP.DoubleOptionalToString(mCompletion);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRemainingWork = ParserSTEP.StripString(str, ref pos, len);
			mScheduleUsage = ParserSTEP.StripDouble(str, ref pos, len);
			mScheduleStart = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mScheduleFinish = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mScheduleContour = ParserSTEP.StripString(str, ref pos, len);
			mLevelingDelay = ParserSTEP.StripString(str, ref pos, len);
			mIsOverAllocated = ParserSTEP.StripBool(str, ref pos, len);
			mStatusTime = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mActualWork = ParserSTEP.StripString(str, ref pos, len);
			mActualUsage = ParserSTEP.StripDouble(str, ref pos, len);
			mActualStart = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mActualFinish = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mRemainingWork = ParserSTEP.StripString(str, ref pos, len);
			mRemainingUsage = ParserSTEP.StripDouble(str, ref pos, len);
			mCompletion = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRevolvedAreaSolid : IfcSweptAreaSolid // SUPERTYPE OF(IfcRevolvedAreaSolidTapered)
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mAxis) + "," + ParserSTEP.DoubleToString(mAngle); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mAxis = ParserSTEP.StripLink(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("IfcPlaneAngleMeasure(", true, System.Globalization.CultureInfo.CurrentCulture))
				mAngle = ParserSTEP.ParseDouble(s.Substring(21, str.Length - 22));
			else
				mAngle = ParserSTEP.ParseDouble(s);
		}
	}
	//ENTITY IfcRevolvedAreaSolidTapered
	public partial class IfcRibPlateProfileProperties : IfcProfileProperties // DEPRECEATED IFC4
	{
#warning building step
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mRibHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mRibWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mRibSpacing = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcRibPlateDirectionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mDirection);
		}
	}
	public partial class IfcRightCircularCone : IfcCsgPrimitive3D
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mHeight) + "," + ParserSTEP.DoubleToString(mBottomRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mBottomRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRightCircularCylinder : IfcCsgPrimitive3D
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mHeight) + "," + ParserSTEP.DoubleToString(mRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcRoof : IfcBuildingElement
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRoofTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcRoofType : IfcBuildingElementType //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcRoofTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public abstract partial class IfcRoot : BaseClassIfc//ABSTRACT SUPERTYPE OF (ONEOF (IfcObjectDefinition ,IfcPropertyDefinition ,IfcRelationship));
	{
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			GlobalId = ParserSTEP.StripString(str, ref pos, len);
			mOwnerHistory = ParserSTEP.StripLink(str, ref pos, len);
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mGlobalId + (mOwnerHistory == 0 ? "',$" : "',#" + mOwnerHistory) + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'"); }
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mRoundingRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRoundingRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
}
