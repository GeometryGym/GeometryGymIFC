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
	public partial class IfcSanitaryTerminal
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcSanitaryTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcSanitaryTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcSanitaryTerminalType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSanitaryTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcScheduleTimeControl
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mActualStart == null ? ",$" : ",#" + mActualStart.StepId) +
				(mEarlyStart == null ? ",$" : ",#" + mEarlyStart.StepId) + (mLateStart == null ? ",$" : ",#" + mLateStart.StepId) +
				(mScheduleStart == null ? ",$" : ",#" + mScheduleStart.StepId) + (mActualFinish == null ? ",$" : ",#" + mActualFinish.StepId) +
				(mEarlyFinish == null ? ",$" : ",#" + mEarlyFinish.StepId) + (mLateFinish == null ? ",$" : ",#" + mLateFinish.StepId) +
				(mScheduleFinish == null ? ",$" : ",#" + mScheduleFinish) + ParserSTEP.DoubleOptionalToString(mScheduleDuration) + "," +
				ParserSTEP.DoubleOptionalToString(mActualDuration) + "," + ParserSTEP.DoubleOptionalToString(mRemainingTime) + "," +
				ParserSTEP.DoubleOptionalToString(mFreeFloat) + "," + ParserSTEP.DoubleOptionalToString(mTotalFloat) + "," +
				ParserSTEP.BoolToString(mIsCritical) + (mStatusTime == null ? ",$" : ",#" + mStatusTime.StepId) + "," +
				ParserSTEP.DoubleOptionalToString(mStartFloat) + "," + ParserSTEP.DoubleOptionalToString(mFinishFloat) + "," + 
				ParserSTEP.DoubleOptionalToString(mCompletion); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mActualStart = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			mEarlyStart = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			mLateStart = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			mScheduleStart = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			mActualFinish = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			mEarlyFinish = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			mLateFinish = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			mScheduleFinish = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			mScheduleDuration = ParserSTEP.StripDouble(str, ref pos, len);
			mActualDuration = ParserSTEP.StripDouble(str, ref pos, len);
			mRemainingTime = ParserSTEP.StripDouble(str, ref pos, len);
			mFreeFloat = ParserSTEP.StripDouble(str, ref pos, len);
			mTotalFloat = ParserSTEP.StripDouble(str, ref pos, len);
			mIsCritical = ParserSTEP.StripBool(str, ref pos, len);
			mStatusTime = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			mStartFloat = ParserSTEP.StripDouble(str, ref pos, len);
			mFinishFloat = ParserSTEP.StripDouble(str, ref pos, len);
			mCompletion = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public abstract partial class IfcSchedulingTime
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return (string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") + 
				(mDataOrigin == IfcDataOriginEnum.NOTDEFINED ? "$" : "." + mDataOrigin.ToString() + ".") + 
				(string.IsNullOrEmpty(mUserDefinedDataOrigin) ? ",$" : ",'" + ParserSTEP.Encode(mUserDefinedDataOrigin) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDataOriginEnum>(s.Replace(".", ""), true, out mDataOrigin);
			mUserDefinedDataOrigin = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcSecondOrderPolynomialSpiral
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ParserSTEP.DoubleToString(mQuadraticTerm) + "," +
			ParserSTEP.DoubleOptionalToString(mLinearTerm) + "," + ParserSTEP.DoubleOptionalToString(mConstantTerm);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			QuadraticTerm = ParserSTEP.StripDouble(str, ref pos, len);
			LinearTerm = ParserSTEP.StripDouble(str, ref pos, len);
			ConstantTerm = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSectionedSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "#" + Directrix.StepId + ",(" + string.Join(",", CrossSections.ConvertAll(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Directrix = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			CrossSections.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcProfileDef));
		}
	}
	public partial class IfcSectionedSolidHorizontal
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			string result = base.BuildStringSTEP(release);
			if (release < ReleaseVersion.IFC4X3_RC3)
				result += ",(" + string.Join(",", mCrossSectionPositions_OBSOLETE.ConvertAll(x => "#" + x.StepId));
			else if (release == ReleaseVersion.IFC4X3_RC3)
				result += ",(" + string.Join(",", mCrossSectionPositionMeasures_OBSOLETE.ConvertAll(x => "#" + x.ToString()));
			else
				result += ",(" + string.Join(",", CrossSectionPositions.Select(x => "#" + x.StepId));
			return result + (release < ReleaseVersion.IFC4X3 ? (mFixedAxisVertical ? "),.T." : "),.F.") : ")");	
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4X3_RC3)
				mCrossSectionPositions_OBSOLETE.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcPointByDistanceExpression));
			else if (release == ReleaseVersion.IFC4X3_RC3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				string[] fields = s.Substring(1, s.Length - 2).Split(",".ToCharArray());
				foreach (string field in fields)
				{
					IfcCurveMeasureSelect measure = ParserIfc.parseMeasureValue(field.Trim()) as IfcCurveMeasureSelect;
					if (measure != null)
						mCrossSectionPositionMeasures_OBSOLETE.Add(measure);
				}
			}
			else
				mCrossSectionPositions.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcAxis2PlacementLinear));
			if (release < ReleaseVersion.IFC4X3)
				FixedAxisVertical = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcSectionedSpine
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "#" + SpineCurve.StepId + ",(" + string.Join(",", CrossSections.ConvertAll(x => "#" + x.StepId)) + "),(" + 
				string.Join(",", mCrossSectionPositions.ConvertAll(x=> "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			SpineCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCompositeCurve;
			CrossSections.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcProfileDef));
			CrossSectionPositions.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcAxis2Placement3D));
		}
	}
	public partial class IfcSectionedSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = "#" + mDirectrix.StepId;
			if (release < ReleaseVersion.IFC4X3)
			{
				if(mCrossSectionPositions_OBSOLETE.Count > 0)
					result += ",(" + string.Join(",", mCrossSectionPositions_OBSOLETE.ConvertAll(x => "#" + x.StepId.ToString())) + ")";
				else
					result += ",(" + string.Join(",", mCrossSectionPositions.ConvertAll(x => "#" + x.Location.StepId.ToString())) + ")";
			}
			else
				result += ",(" + string.Join(",", mCrossSectionPositions.ConvertAll(x => "#" + x.StepId.ToString())) + ")";
			return result + ",(" + string.Join(",", mCrossSections.ConvertAll(x => "#" + x.StepId.ToString())) + ")" +
				(release < ReleaseVersion.IFC4X3 ? "," + (mFixedAxisVertical ? ".T." : ".F") : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Directrix = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			if(release < ReleaseVersion.IFC4X3)
				mCrossSectionPositions_OBSOLETE.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcPointByDistanceExpression));
			else
				CrossSectionPositions.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcAxis2PlacementLinear));
			CrossSections.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcProfileDef));
			if(release < ReleaseVersion.IFC4X3)
				FixedAxisVertical = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcSectionProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "." + mSectionType.ToString() + ".,#" + mStartProfile.StepId + ",#" + mEndProfile.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Enum.TryParse<IfcSectionTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mSectionType);
			mStartProfile = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
			mEndProfile = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
		}
	}
	public partial class IfcSectionReinforcementProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return ParserSTEP.DoubleToString(mLongitudinalStartPosition) + "," + ParserSTEP.DoubleToString(mLongitudinalEndPosition) + "," +
			ParserSTEP.DoubleOptionalToString(mTransversePosition) + ",." + mReinforcementRole.ToString() + ".,#" + mSectionDefinition.StepId +
			",(" + String.Join(",", mCrossSectionReinforcementDefinitions.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mLongitudinalStartPosition = ParserSTEP.StripDouble(str, ref pos, len);
			mLongitudinalEndPosition = ParserSTEP.StripDouble(str, ref pos, len);
			mTransversePosition = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcReinforcingBarRoleEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mReinforcementRole);
			mSectionDefinition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSectionProperties;
			mCrossSectionReinforcementDefinitions.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcReinforcementBarProperties));
		}
	}
	public abstract partial class IfcSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "." + mTransition.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTransitionCode>(s.Replace(".", ""), true, out mTransition);
		}
	}
	public partial class IfcSegmentedReferenceCurve 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (release < ReleaseVersion.IFC4X3_RC3 ? "" : base.BuildStringSTEP(release)) + ",#" + mBaseCurve.StepId + 
				(release == ReleaseVersion.IFC4X3_RC2 ? ",(#" + string.Join(",#", Segments.ConvertAll(x => x.StepId.ToString())) + ")" : "") +
				(mEndPoint == null ? ",$" : ",#" + mEndPoint.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			if(release != ReleaseVersion.IFC4X3_RC2)
				base.parse(str, ref pos, release, len, dictionary);
			BaseCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcBoundedCurve;
			if(release == ReleaseVersion.IFC4X3_RC2)
				Segments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcCurveSegment));
			EndPoint = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPlacement;
		}
	}
	public partial class IfcSensor  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcSensorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSensorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSensorType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSensorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}

	public partial class IfcSeventhOrderPolynomialSpiral
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ParserSTEP.DoubleToString(mSepticTerm) + "," + ParserSTEP.DoubleOptionalToString(mSexticTerm) + "," +
			ParserSTEP.DoubleOptionalToString(mQuinticTerm) + "," + ParserSTEP.DoubleOptionalToString(mQuarticTerm) + "," +
			ParserSTEP.DoubleOptionalToString(mQuadraticTerm) + "," + ParserSTEP.DoubleOptionalToString(mLinearTerm) + "," +
			ParserSTEP.DoubleOptionalToString(mConstantTerm);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			SepticTerm = ParserSTEP.StripDouble(str, ref pos, len);
			SexticTerm = ParserSTEP.StripDouble(str, ref pos, len);
			QuinticTerm = ParserSTEP.StripDouble(str, ref pos, len);
			QuarticTerm = ParserSTEP.StripDouble(str, ref pos, len);
			CubicTerm = ParserSTEP.StripDouble(str, ref pos, len);
			QuadraticTerm = ParserSTEP.StripDouble(str, ref pos, len);
			LinearTerm = ParserSTEP.StripDouble(str, ref pos, len);
			ConstantTerm = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcShadingDevice
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcShadingDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcShadingDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcShadingDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcShadingDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcShapeAspect
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(#" + string.Join(",#", ShapeRepresentations.Select(x=>x.StepId)) + 
				(string.IsNullOrEmpty(mName) ? "),$," : "),'" + ParserSTEP.Encode( mName) + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + 
				ParserIfc.LogicalToString(mProductDefinitional) + "," + ParserSTEP.ObjToLinkString(mPartOfProductDefinitionShape);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mShapeRepresentations.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcShapeModel));
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mProductDefinitional = ParserIfc.StripLogical(str,ref pos, len);
			PartOfProductDefinitionShape = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProductRepresentationSelect;
		}
	}
	public partial class IfcShellBasedSurfaceModel
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mSbsmBoundary.Select(x=>"#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{ 
			SbsmBoundary.AddRange( ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)).Select(x=>dictionary[x] as IfcShell));
		}
	}
	public partial class IfcSign
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			(mPredefinedType == IfcSignTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSignTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSignal
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			(mPredefinedType == IfcSignalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSignalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSignalType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSignalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSignType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSignTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}

	public partial class IfcSimplePropertyTemplate
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mDatabase.Release < ReleaseVersion.IFC4)
				return "";
			return base.BuildStringSTEP(release) + (mTemplateType == IfcSimplePropertyTemplateTypeEnum.NOTDEFINED ? ",$," : ",." + mTemplateType.ToString() + ".,") + 
				(string.IsNullOrEmpty(mPrimaryMeasureType) ? "$," : "'" + ParserSTEP.Encode(mPrimaryMeasureType) + "',") + 
				(string.IsNullOrEmpty(mSecondaryMeasureType) ? "$," : "'" + ParserSTEP.Encode(mSecondaryMeasureType) + "',") + 
				(mEnumerators == null ? "$" : "#" + mEnumerators.StepId) + (mPrimaryUnit == null ? ",$" : ",#" + mPrimaryUnit.StepId) +
				(mSecondaryUnit == null ? ",$" : ",#" + mSecondaryUnit.StepId) + "," +
				(string.IsNullOrEmpty(mExpression) ? "$," : "'" + ParserSTEP.Encode(mExpression) + "',") +
				(mAccessState == IfcStateEnum.NOTDEFINED ? "$" : "." + mAccessState.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSimplePropertyTemplateTypeEnum>(s.Replace(".", ""), true, out mTemplateType);
			mPrimaryMeasureType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mSecondaryMeasureType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mEnumerators = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPropertyEnumeration;
			mPrimaryUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
			mSecondaryUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
			mExpression = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcStateEnum>(s.Replace(".", ""), true, out mAccessState);
		}
	}
	public partial class IfcSineSpiral
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ParserSTEP.DoubleToString(mSineTerm) + "," + ParserSTEP.DoubleOptionalToString(mLinearTerm) + "," + ParserSTEP.DoubleOptionalToString(mConstantTerm);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			SineTerm = ParserSTEP.StripDouble(str, ref pos, len);
			LinearTerm = ParserSTEP.StripDouble(str, ref pos, len);
			ConstantTerm = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSite
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return base.BuildStringSTEP(release) + (mRefLatitude != null ? "," + mRefLatitude.ToSTEP() + "," : ",$,") +
				(mRefLongitude != null ?  mRefLongitude.ToSTEP() + "," : "$,") + ParserSTEP.DoubleOptionalToString(mRefElevation) +
				(string.IsNullOrEmpty(mLandTitleNumber) ? ",$," : ",'" + ParserSTEP.Encode(mLandTitleNumber) + "',") +
				(mSiteAddress == null ? "$" : "#" + mSiteAddress.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRefLatitude = IfcCompoundPlaneAngleMeasure.Parse(ParserSTEP.StripField(str, ref pos, len));
			mRefLongitude = IfcCompoundPlaneAngleMeasure.Parse(ParserSTEP.StripField(str, ref pos, len));
			mRefElevation = ParserSTEP.StripDouble(str, ref pos, len);
			mLandTitleNumber = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mSiteAddress = dictionary[ParserSTEP.StripLink(str, ref pos, len)]  as IfcPostalAddress;
		}
	}
	public partial class IfcSIUnit
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release);
			if (mPrefix == IfcSIPrefix.NONE)
				str += ",$,.";
			else
				str += ",." + mPrefix.ToString() + ".,.";
			return str + mName + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if(s.StartsWith("."))
				Enum.TryParse<IfcSIPrefix>(s.Replace(".", ""), true, out mPrefix);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSIUnitName>(s.Replace(".", ""), true, out mName);
		}
	}
	public partial class IfcSlab
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mPredefinedType == IfcSlabTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSlabTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSlabType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSlabTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSlippageConnectionCondition
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," +
			ParserSTEP.DoubleOptionalToString(mSlippageX) + "," +
			ParserSTEP.DoubleOptionalToString(mSlippageY) + "," +
			ParserSTEP.DoubleOptionalToString(mSlippageZ);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			SlippageX = ParserSTEP.StripDouble(str, ref pos, len);
			SlippageY = ParserSTEP.StripDouble(str, ref pos, len);
			SlippageZ = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSolarDevice
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcSolarDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSolarDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSolarDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSolarDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSoundProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.BoolToString(mIsAttenuating) + ",." + mSoundScale.ToString() + ".,(" +
				String.Join(",", mSoundValues.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIsAttenuating = ParserSTEP.StripBool(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSoundScaleEnum>(s.Replace(".", ""), true, out mSoundScale);
			mSoundValues.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcSoundValue));
		}
	}
	public partial class IfcSoundValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return base.BuildStringSTEP(release) + (mSoundLevelTimeSeries == null ? ",$" : ",#" + mSoundLevelTimeSeries.StepId) + "," + 
				ParserSTEP.DoubleToString(mFrequency) + "," + ParserSTEP.DoubleOptionalToString(mSoundLevelSingleValue);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSoundLevelTimeSeries = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcTimeSeries;
			mFrequency = ParserSTEP.StripDouble(str, ref pos, len);
			mSoundLevelSingleValue = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSpace
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 || mPredefinedType != IfcSpaceTypeEnum.NOTDEFINED ? ",." + mPredefinedType.ToString() + ".," : ",$,") + ParserSTEP.DoubleOptionalToString(mElevationWithFlooring); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcSpaceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			mElevationWithFlooring = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSpaceHeater
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSpaceHeaterTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSpaceHeaterType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSpaceHeaterTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSpaceProgram
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return base.BuildStringSTEP(release) + ",'" + mSpaceProgramIdentifier + "'," + ParserSTEP.DoubleOptionalToString(mMaxRequiredArea) + "," + 
				ParserSTEP.DoubleOptionalToString(mMinRequiredArea) + (mRequestedLocation == null ? ",$" : ",#" + mRequestedLocation.StepId) + "," +
				ParserSTEP.DoubleToString(mStandardRequiredArea);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSpaceProgramIdentifier = ParserSTEP.StripField(str, ref pos, len); 
			mMaxRequiredArea = ParserSTEP.StripDouble(str, ref pos, len);
			mMinRequiredArea = ParserSTEP.StripDouble(str, ref pos, len);
			mRequestedLocation = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSpatialStructureElement;
			mStandardRequiredArea = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSpaceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() +
				(release < ReleaseVersion.IFC4 ? "." : (string.IsNullOrEmpty(LongName) ? ".,$" : ".,'" + ParserSTEP.Encode(LongName) + "'"));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSpaceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			if(release > ReleaseVersion.IFC2x3)
				LongName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public abstract partial class IfcSpatialElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mLongName) ? ",$" : ",'" + ParserSTEP.Encode(mLongName) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			LongName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public abstract partial class IfcSpatialElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mElementType) ? ",$" : ",'" + ParserSTEP.Encode(mElementType) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mElementType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public abstract partial class IfcSpatialStructureElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mCompositionType == IfcElementCompositionEnum.NOTDEFINED ? (release < ReleaseVersion.IFC4 ? ",." + IfcElementCompositionEnum.ELEMENT.ToString() + "." : ",$") : ",." + mCompositionType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcElementCompositionEnum>(s.Replace(".", ""), true, out mCompositionType);
		}
	}
	public partial class IfcSpatialZone
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSpatialZoneTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSpatialZoneType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSpatialZoneTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSphere
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSphericalSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSpiral
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (mPosition == null ? "$" : "#" + mPosition.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Position = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement;
		}
	}
	public partial class IfcStackTerminal
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcStackTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcStackTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcStackTerminalType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcStackTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcStair
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcStairTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcStairFlight
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + "," + ParserSTEP.IntOptionalToString(mNumberOfRiser) + "," + ParserSTEP.IntOptionalToString(mNumberOfTreads) + "," +
				ParserSTEP.DoubleOptionalToString(mRiserHeight) + "," + ParserSTEP.DoubleOptionalToString(mTreadLength);
			return result + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcStairFlightTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mNumberOfRiser = ParserSTEP.StripInt(str, ref pos, len);
			mNumberOfTreads = ParserSTEP.StripInt(str, ref pos, len);
			mRiserHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mTreadLength = ParserSTEP.StripDouble(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcStairFlightTypeEnum>(s.Substring(1, s.Length - 2), out mPredefinedType);
			}
		}
	}
	public partial class IfcStairFlightType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcStairFlightTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcStairType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcStairTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcStructuralAction
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + 
				(mDestabilizingLoad == IfcLogicalEnum.UNKNOWN ? (mDatabase.Release < ReleaseVersion.IFC4 ? "," + ParserSTEP.BoolToString(false) : ",$") : "," + ParserIfc.LogicalToString(mDestabilizingLoad)) + 
				(release < ReleaseVersion.IFC4 ? (mCausedBy == null ? ",$" : ",#" + mCausedBy.StepId) : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDestabilizingLoad = ParserIfc.StripLogical(str, ref pos, len);
			if (release < ReleaseVersion.IFC4)
				mCausedBy = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcStructuralReaction;
		}
	}
	public abstract partial class IfcStructuralActivity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + ",#" + mAppliedLoad.StepId + ",." + mGlobalOrLocal.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAppliedLoad = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcStructuralLoad;
			Enum.TryParse<IfcGlobalOrLocalEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mGlobalOrLocal);
		}
	}
	public partial class IfcStructuralAnalysisModel
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".," +
				(mOrientationOf2DPlane == null ? "$" : "#" + mOrientationOf2DPlane.StepId) + 
				(mLoadedBy.Count == 0 ?	",$," : ",(" + string.Join(",", mLoadedBy.Select(x=> "#" + x.StepId)) + "),") +
				(mHasResults.Count == 0 ? "$" : "(" + string.Join(",", mHasResults.Select(x=>"#" + x.StepId)) + ")") +	
				(release > ReleaseVersion.IFC2x3 ? "," + ParserSTEP.ObjToLinkString(mSharedPlacement) : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcAnalysisModelTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mOrientationOf2DPlane = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement3D;
			foreach (IfcStructuralLoadGroup loadGroup in ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcStructuralLoadGroup))
				addLoadGroup(loadGroup);
			HasResults.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcStructuralResultGroup));
			if(release > ReleaseVersion.IFC2x3)
				SharedPlacement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcObjectPlacement;
		}
	}
	public abstract partial class IfcStructuralConnection
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return base.BuildStringSTEP(release) + (mAppliedCondition == null ? ",$" : ",#" + mAppliedCondition.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAppliedCondition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcBoundaryCondition;
		}
	}
	public abstract partial class IfcStructuralConnectionCondition
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (string.IsNullOrEmpty(mName) ? "$" : "'" + ParserSTEP.Encode(mName) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len)); }
	}
	public partial class IfcStructuralCurveAction : IfcStructuralAction // IFC4 SUPERTYPE OF(IfcStructuralLinearAction)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mProjectedOrTrue.ToString() + (release < ReleaseVersion.IFC4 ? "." : ".,." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProjectedOrTrueLengthEnum>(s.Replace(".", ""), true, out mProjectedOrTrue);
			if (release != ReleaseVersion.IFC2x3)
				Enum.TryParse<IfcStructuralCurveActivityTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcStructuralCurveConnection
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : ",#" + mAxisDirection.StepId); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxisDirection = dictionary[ ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
		}
	}
	public partial class IfcStructuralCurveMember
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + (release < ReleaseVersion.IFC4 ? "." : ".,#" + mAxis.StepId); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			Enum.TryParse<IfcStructuralCurveMemberTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			if (release != ReleaseVersion.IFC2x3)
				mAxis = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
		}
	}
	public partial class IfcStructuralLinearActionVarying
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mVaryingAppliedLoadLocation.StepId + ",("
				+ string.Join(",", mSubsequentAppliedLoads.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mVaryingAppliedLoadLocation = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcShapeAspect;
			mSubsequentAppliedLoads.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcStructuralLoad));
		}
	}
	public abstract partial class IfcStructuralLoad
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (string.IsNullOrEmpty(mName) ? "$" : "'" + ParserSTEP.Encode(mName) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len)); }
	}
	public partial class IfcStructuralLoadConfiguration : IfcStructuralLoad //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mValues.Select(x => "#" + x.StepId)) + ")" +
				(mLocations.Count == 0 ? ",$" : ",(" + 
				string.Join(",", mLocations.Select(x=> "(" + String.Join(",", x.Select(d=>ParserSTEP.DoubleToString(d))) + ")")) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mValues.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcStructuralLoadOrResult));
			mLocations.AddRange(ParserSTEP.StripListListDouble(str, ref pos, len));			
		}
	}
	public partial class IfcStructuralLoadCase
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mSelfWeightCoefficients != null ? ",(" + ParserSTEP.DoubleToString(mSelfWeightCoefficients.Item1) + "," + ParserSTEP.DoubleToString(mSelfWeightCoefficients.Item2) + "," + ParserSTEP.DoubleToString(mSelfWeightCoefficients.Item3) + ")" : ",$")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("("))
			{
				List<string> fields = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				mSelfWeightCoefficients = new Tuple<double,double,double>(double.Parse(fields[0], ParserSTEP.NumberFormat), double.Parse(fields[1], ParserSTEP.NumberFormat), double.Parse(fields[2], ParserSTEP.NumberFormat));
			}
		}
	}
	public partial class IfcStructuralLoadGroup
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".,." + mActionType.ToString() + ".,." + 
				mActionSource.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mCoefficient) + 
				(string.IsNullOrEmpty(mPurpose) ? ",$" : ",'" + ParserSTEP.Encode(mPurpose) + "'"); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcLoadGroupTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			Enum.TryParse<IfcActionTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mActionType);
			Enum.TryParse<IfcActionSourceTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mActionSource);
			mCoefficient = ParserSTEP.StripDouble(str, ref pos, len);
			mPurpose = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcStructuralLoadLinearForce
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mLinearForceX) + "," + ParserSTEP.DoubleOptionalToString(mLinearForceY) + "," + ParserSTEP.DoubleOptionalToString(mLinearForceZ) + "," + ParserSTEP.DoubleOptionalToString(mLinearMomentX) + "," + ParserSTEP.DoubleOptionalToString(mLinearMomentY) + "," + ParserSTEP.DoubleOptionalToString(mLinearMomentZ); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mLinearForceX = ParserSTEP.StripDouble(str, ref pos, len);
			mLinearForceY = ParserSTEP.StripDouble(str, ref pos, len);
			mLinearForceZ = ParserSTEP.StripDouble(str, ref pos, len);
			mLinearMomentX = ParserSTEP.StripDouble(str, ref pos, len);
			mLinearMomentY = ParserSTEP.StripDouble(str, ref pos, len);
			mLinearMomentZ = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralLoadPlanarForce
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mPlanarForceX) + "," + ParserSTEP.DoubleOptionalToString(mPlanarForceY) + "," + ParserSTEP.DoubleOptionalToString(mPlanarForceZ); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPlanarForceX = ParserSTEP.StripDouble(str, ref pos, len);
			mPlanarForceY = ParserSTEP.StripDouble(str, ref pos, len);
			mPlanarForceZ = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralLoadSingleDisplacement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mDisplacementX) + "," + ParserSTEP.DoubleOptionalToString(mDisplacementY) + "," + ParserSTEP.DoubleOptionalToString(mDisplacementZ) + "," + ParserSTEP.DoubleOptionalToString(mRotationalDisplacementRX) + "," + ParserSTEP.DoubleOptionalToString(mRotationalDisplacementRY) + "," + ParserSTEP.DoubleOptionalToString(mRotationalDisplacementRZ); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDisplacementX = ParserSTEP.StripDouble(str, ref pos, len);
			mDisplacementY = ParserSTEP.StripDouble(str, ref pos, len);
			mDisplacementZ = ParserSTEP.StripDouble(str, ref pos, len);
			mRotationalDisplacementRX = ParserSTEP.StripDouble(str, ref pos, len);
			mRotationalDisplacementRY = ParserSTEP.StripDouble(str, ref pos, len);
			mRotationalDisplacementRZ = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralLoadSingleDisplacementDistortion
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mDistortion); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDistortion = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralLoadSingleForce
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mForceX) + "," + ParserSTEP.DoubleOptionalToString(mForceY) + "," + ParserSTEP.DoubleOptionalToString(mForceZ) + "," + ParserSTEP.DoubleOptionalToString(mMomentX) + "," + ParserSTEP.DoubleOptionalToString(mMomentY) + "," + ParserSTEP.DoubleOptionalToString(mMomentZ); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mForceX = ParserSTEP.StripDouble(str, ref pos, len);
			mForceY = ParserSTEP.StripDouble(str, ref pos, len);
			mForceZ = ParserSTEP.StripDouble(str, ref pos, len);
			mMomentX = ParserSTEP.StripDouble(str, ref pos, len);
			mMomentY = ParserSTEP.StripDouble(str, ref pos, len);
			mMomentZ = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralLoadSingleForceWarping
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mWarpingMoment); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mWarpingMoment = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralLoadTemperature
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mDeltaT_Constant) + "," + ParserSTEP.DoubleOptionalToString(mDeltaT_Y) + "," + ParserSTEP.DoubleOptionalToString(mDeltaT_Z); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDeltaT_Constant = ParserSTEP.StripDouble(str, ref pos, len);
			mDeltaT_Y = ParserSTEP.StripDouble(str, ref pos, len);
			mDeltaT_Z = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralPointConnection
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + 
				(release < ReleaseVersion.IFC4 ? "" : (mConditionCoordinateSystem == null ? ",$" : ",#" + mConditionCoordinateSystem.StepId));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if(release > ReleaseVersion.IFC2x3)
				mConditionCoordinateSystem = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement3D;
		}
	}
	public partial class IfcStructuralProfileProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mTorsionalConstantX) + "," + ParserSTEP.DoubleOptionalToString(mMomentOfInertiaYZ) + "," + ParserSTEP.DoubleOptionalToString(mMomentOfInertiaY) + "," + ParserSTEP.DoubleOptionalToString(mMomentOfInertiaZ) + "," +
				ParserSTEP.DoubleOptionalToString(mWarpingConstant) + "," + ParserSTEP.DoubleOptionalToString(mShearCentreZ) + "," + ParserSTEP.DoubleOptionalToString(mShearCentreY) + "," + ParserSTEP.DoubleOptionalToString(mShearDeformationAreaZ) + "," +
				ParserSTEP.DoubleOptionalToString(mShearDeformationAreaY) + "," + ParserSTEP.DoubleOptionalToString(mMaximumSectionModulusY) + "," + ParserSTEP.DoubleOptionalToString(mMinimumSectionModulusY) + "," + ParserSTEP.DoubleOptionalToString(mMaximumSectionModulusZ) + "," +
				ParserSTEP.DoubleOptionalToString(mMinimumSectionModulusZ) + "," + ParserSTEP.DoubleOptionalToString(mTorsionalSectionModulus) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInX) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mTorsionalConstantX = ParserSTEP.StripDouble(str, ref pos, len);
			mMomentOfInertiaYZ = ParserSTEP.StripDouble(str, ref pos, len);
			mMomentOfInertiaY = ParserSTEP.StripDouble(str, ref pos, len);
			mMomentOfInertiaZ = ParserSTEP.StripDouble(str, ref pos, len);
			mWarpingConstant = ParserSTEP.StripDouble(str, ref pos, len);
			mShearCentreZ = ParserSTEP.StripDouble(str, ref pos, len);
			mShearCentreY = ParserSTEP.StripDouble(str, ref pos, len);
			mShearDeformationAreaZ = ParserSTEP.StripDouble(str, ref pos, len);
			mShearDeformationAreaY = ParserSTEP.StripDouble(str, ref pos, len);
			mMaximumSectionModulusY = ParserSTEP.StripDouble(str, ref pos, len);
			mMinimumSectionModulusY = ParserSTEP.StripDouble(str, ref pos, len);
			mMaximumSectionModulusZ = ParserSTEP.StripDouble(str, ref pos, len);
			mMinimumSectionModulusZ = ParserSTEP.StripDouble(str, ref pos, len);
			mTorsionalSectionModulus = ParserSTEP.StripDouble(str, ref pos, len);
			mCentreOfGravityInX = ParserSTEP.StripDouble(str, ref pos, len);
			mCentreOfGravityInY = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralResultGroup
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mTheoryType.ToString() + (mResultForLoadGroup == null ? ".,$" : ".,#" + mResultForLoadGroup.StepId) + "," + ParserSTEP.BoolToString(mIsLinear); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcAnalysisTheoryTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mTheoryType);
			ResultForLoadGroup = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcStructuralLoadGroup;
			mIsLinear = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcStructuralSteelProfileProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mShearAreaZ) + "," + ParserSTEP.DoubleOptionalToString(mShearAreaY) + "," + ParserSTEP.DoubleOptionalToString(mPlasticShapeFactorY) + "," + ParserSTEP.DoubleOptionalToString(mPlasticShapeFactorZ); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mShearAreaZ = ParserSTEP.StripDouble(str, ref pos, len); 
			mShearAreaY = ParserSTEP.StripDouble(str, ref pos, len); 
			mPlasticShapeFactorY = ParserSTEP.StripDouble(str, ref pos, len); 
			mPlasticShapeFactorZ = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralSurfaceAction
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mProjectedOrTrue.ToString() + (release < ReleaseVersion.IFC4 ? "." : ".,." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProjectedOrTrueLengthEnum>(s.Replace(".", ""), true, out mProjectedOrTrue);
			if (release != ReleaseVersion.IFC2x3)
				Enum.TryParse<IfcStructuralSurfaceActivityTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcStructuralSurfaceMember
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mThickness); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcStructuralSurfaceMemberTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mThickness = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralSurfaceMemberVarying
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release);
			if(release < ReleaseVersion.IFC4)
				str += ",(" + string.Join(",", mSubsequentThickness.Select(x=>ParserSTEP.DoubleToString(x))) + ")" + ",#" + mVaryingThicknessLocation.StepId;
			return str;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
			{
				mSubsequentThickness = ParserSTEP.StripListDouble(str, ref pos, len);
				mVaryingThicknessLocation = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcShapeAspect;
			}
		}
	}
	public partial class IfcStructuralSurfaceReaction
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcStructuralSurfaceActivityTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcStyledItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return ParserSTEP.ObjToLinkString(mItem) + ",(" +
				string.Join(",", mStyles.Select(x => "#" + x.StepId)) + (string.IsNullOrEmpty(mName) ? "),$" : "),'" + ParserSTEP.Encode(mName) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Item = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcRepresentationItem;
			foreach (IfcStyleAssignmentSelect style in ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcStyleAssignmentSelect))
				mStyles.Add(style);
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcSubContractResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcSubContractResourceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcSubContractResourceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mPredefinedType == IfcSubContractResourceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSubContractResourceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSubedge
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mParentEdge.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mParentEdge = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcEdge;
		}
	}
	public partial class IfcSurfaceCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			string geometry = mAssociatedGeometry.Count == 0 ? ",$,." : ",(" + string.Join(",", mAssociatedGeometry.Select(x => "#" + x.StepId)) + "),.";
			return "#" + mCurve3D.StepId + geometry + mMasterRepresentation.ToString() + "."; 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Curve3D = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			AssociatedGeometry.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcPcurve));
			Enum.TryParse<IfcPreferredSurfaceCurveRepresentation>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mMasterRepresentation);
		}
	}
	public partial class IfcSurfaceCurveSweptAreaSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mReferenceSurface.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mReferenceSurface = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSurface;
		}
	}
	public partial class IfcSurfaceFeature
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			(mPredefinedType == IfcSurfaceFeatureTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSurfaceFeatureTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSurfaceOfLinearExtrusion
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mExtrudedDirection.StepId + "," + ParserSTEP.DoubleToString(mDepth); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mExtrudedDirection = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
			mDepth = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSurfaceOfRevolution
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" +  mAxisPosition.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxisPosition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis1Placement;
		}
	}
	public partial class IfcSurfaceReinforcementArea
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			",(" + string.Join(",", mSurfaceReinforcement1.ConvertAll(x => formatLength(x))) + ")" +
			",(" + string.Join(",", mSurfaceReinforcement2.ConvertAll(x => formatLength(x))) + ")" + "," +
			ParserSTEP.DoubleOptionalToString(mShearReinforcement);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			SurfaceReinforcement1.AddRange(ParserSTEP.StripListDouble(str, ref pos, len));
			SurfaceReinforcement2.AddRange(ParserSTEP.StripListDouble(str, ref pos, len));
			ShearReinforcement = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSurfaceStyle
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",." + mSide.ToString() +
				(mStyles.Count > 0 ? ".,(#" + string.Join(",#", mStyles.Select(x=>x.StepId)) + ")" : ".,()");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSurfaceSide>(s.Replace(".", ""), true, out mSide);
			mStyles.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcSurfaceStyleElementSelect));
		}
	}
	public partial class IfcSurfaceStyleLighting
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return "#" + mDiffuseTransmissionColour.StepId + ",#" + mDiffuseReflectionColour.StepId + ",#" + 
				mTransmissionColour.StepId + ",#" + mReflectanceColour.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mDiffuseTransmissionColour = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcColourRgb;
			mDiffuseReflectionColour = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcColourRgb;
			mTransmissionColour = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcColourRgb;
			mReflectanceColour = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcColourRgb;
		}
	}
	public partial class IfcSurfaceStyleRefraction
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return ParserSTEP.DoubleOptionalToString(mRefractionIndex) + "," + ParserSTEP.DoubleOptionalToString(mDispersionFactor); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mRefractionIndex = ParserSTEP.StripDouble(str, ref pos, len);
			mDispersionFactor = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSurfaceStyleRendering
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserIfc.STEPString(mDiffuseColour) + "," + 
				ParserIfc.STEPString(mTransmissionColour) + "," + ParserIfc.STEPString(mDiffuseTransmissionColour) + "," +
				ParserIfc.STEPString(mReflectionColour) + "," + ParserIfc.STEPString(mSpecularColour) + 
				(mSpecularHighlight == null ? ",$" : "," + mSpecularHighlight.ToString()) + ",." + mReflectanceMethod.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDiffuseColour = ParserIfc.parseColourOrFactor(ParserSTEP.StripField(str, ref pos, len), dictionary);
			mTransmissionColour = ParserIfc.parseColourOrFactor(ParserSTEP.StripField(str, ref pos, len), dictionary);
			mDiffuseTransmissionColour = ParserIfc.parseColourOrFactor(ParserSTEP.StripField(str, ref pos, len), dictionary);
			mReflectionColour = ParserIfc.parseColourOrFactor(ParserSTEP.StripField(str, ref pos, len), dictionary);
			mSpecularColour = ParserIfc.parseColourOrFactor(ParserSTEP.StripField(str, ref pos, len), dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				if (s.StartsWith("IFCSPECULARROUGHNESS"))
					mSpecularHighlight = new IfcSpecularRoughness(double.Parse(s.Substring(21, s.Length - 22), ParserSTEP.NumberFormat));
				else
					mSpecularHighlight = new IfcSpecularExponent(double.Parse(s.Substring(20, s.Length - 21), ParserSTEP.NumberFormat));
			}
			s = ParserSTEP.StripField(str, ref pos, len);
			if(s.StartsWith("."))
				Enum.TryParse<IfcReflectanceMethodEnum>(s.Replace(".", ""), true, out mReflectanceMethod);
		}
	}
	public partial class IfcSurfaceStyleShading
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			IfcSurfaceStyleRendering rendering = this as IfcSurfaceStyleRendering;
			return "#"  + mSurfaceColour.StepId + 
				(rendering != null || release > ReleaseVersion.IFC2x3 ? "," + ParserSTEP.DoubleOptionalToString(mTransparency) : ""); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) 
		{
			mSurfaceColour = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcColourRgb; 
			IfcSurfaceStyleRendering rendering = this as IfcSurfaceStyleRendering;
			if(rendering != null || release > ReleaseVersion.IFC2x3)
				mTransparency = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSurfaceStyleWithTextures
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "(#" + string.Join(",#", mTextures.Select(x=>x.StepId)) + ")"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mTextures.AddRange( ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcSurfaceTexture)); }
	}
	public abstract partial class IfcSurfaceTexture
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = ParserSTEP.BoolToString(mRepeatS) + "," + ParserSTEP.BoolToString(mRepeatT);
			if (release < ReleaseVersion.IFC4)
			{
				IfcSurfaceTextureEnum texture = IfcSurfaceTextureEnum.NOTDEFINED;
				if (!Enum.TryParse<IfcSurfaceTextureEnum>(mMode,true, out texture))
					texture = IfcSurfaceTextureEnum.NOTDEFINED;
				result += ",." + texture.ToString() + (mTextureTransform == null ? ".,$" : ".,#" + mTextureTransform.StepId);
			}
			else
			{
				result += (string.IsNullOrEmpty(mMode) ? ",$," : ",'" + ParserSTEP.Encode(mMode) + "',") + (mTextureTransform == null ? "$" : "#" + mTextureTransform.StepId);
				result += (mParameter.Count == 0 ? ",$" : ",(" + string.Join(",", mParameter.Select(x => "'" + ParserSTEP.Encode(x) + "'")) + ")");
			}
			return result;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mRepeatS = ParserSTEP.StripBool(str, ref pos, len);
			mRepeatT = ParserSTEP.StripBool(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			mMode = (release < ReleaseVersion.IFC4 ?  s.Replace(".", "") : ParserSTEP.Decode(s.Replace("'", "")));
			TextureTransform = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianTransformationOperator2D;
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				mParameter.AddRange(ParserSTEP.SplitListStrings(s.Substring(1, s.Length - 2)).Select(x=>ParserSTEP.Decode(x)));
		}
	}
	public abstract partial class IfcSweptAreaSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mSweptArea.StepId + (mPosition == null ? ",$" : ",#" + mPosition.StepId); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			SweptArea = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
			Position = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement3D;
		}
	}
	public partial class IfcSweptDiskSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{ 
			return "#" + mDirectrix.StepId + "," + ParserSTEP.DoubleToString(mRadius) + "," + ParserSTEP.DoubleOptionalToString(mInnerRadius) + "," + 
				(release < ReleaseVersion.IFC4 ? ParserSTEP.DoubleToString(mStartParam) + "," + ParserSTEP.DoubleToString(mEndParam) : 
				ParserSTEP.DoubleOptionalToString(mStartParam) + "," + ParserSTEP.DoubleOptionalToString(mEndParam)); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mDirectrix = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mInnerRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mStartParam = ParserSTEP.StripDouble(str, ref pos, len);
			mEndParam = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSweptDiskSolidPolygonal
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mFilletRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public abstract partial class IfcSweptSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return "#" + mSweptCurve.StepId + (mPosition == null ? ",$" : ",#" + mPosition.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mSweptCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
			mPosition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement3D;
		}
	}
	public partial class IfcSwitchingDevice
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcSwitchingDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSwitchingDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSwitchingDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSwitchingDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSystemFurnitureElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSystemFurnitureElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcSystemFurnitureElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSystemFurnitureElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
}
