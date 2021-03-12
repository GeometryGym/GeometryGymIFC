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
	public partial class IfcSanitaryTerminal : IfcFlowTerminal
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
	public partial class IfcSanitaryTerminalType : IfcFlowTerminalType
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
	public partial class IfcScheduleTimeControl : IfcControl // DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mActualStart) + "," + ParserSTEP.LinkToString(mEarlyStart) + "," + ParserSTEP.LinkToString(mLateStart) + "," +
				ParserSTEP.LinkToString(mScheduleStart) + "," + ParserSTEP.LinkToString(mActualFinish) + "," + ParserSTEP.LinkToString(mEarlyFinish) + "," + ParserSTEP.LinkToString(mLateFinish) + "," + ParserSTEP.LinkToString(mScheduleFinish) + "," +
				ParserSTEP.DoubleOptionalToString(mScheduleDuration) + "," + ParserSTEP.DoubleOptionalToString(mActualDuration) + "," + ParserSTEP.DoubleOptionalToString(mRemainingTime) + "," + ParserSTEP.DoubleOptionalToString(mFreeFloat) + "," + ParserSTEP.DoubleOptionalToString(mTotalFloat) + "," + ParserSTEP.BoolToString(mIsCritical) + "," + ParserSTEP.LinkToString(mStatusTime) + "," +
				ParserSTEP.DoubleOptionalToString(mStartFloat) + "," + ParserSTEP.DoubleOptionalToString(mFinishFloat) + "," + ParserSTEP.DoubleOptionalToString(mCompletion); //(mScheduleTimeControlAssigned == null ? "" :
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mActualStart = ParserSTEP.StripLink(str, ref pos, len);
			mEarlyStart = ParserSTEP.StripLink(str, ref pos, len);
			mLateStart = ParserSTEP.StripLink(str, ref pos, len);
			mScheduleStart = ParserSTEP.StripLink(str, ref pos, len);
			mActualFinish = ParserSTEP.StripLink(str, ref pos, len);
			mEarlyFinish = ParserSTEP.StripLink(str, ref pos, len);
			mLateFinish = ParserSTEP.StripLink(str, ref pos, len);
			mScheduleFinish = ParserSTEP.StripLink(str, ref pos, len);
			mScheduleDuration = ParserSTEP.StripDouble(str, ref pos, len);
			mActualDuration = ParserSTEP.StripDouble(str, ref pos, len);
			mRemainingTime = ParserSTEP.StripDouble(str, ref pos, len);
			mFreeFloat = ParserSTEP.StripDouble(str, ref pos, len);
			mTotalFloat = ParserSTEP.StripDouble(str, ref pos, len);
			mIsCritical = ParserSTEP.StripBool(str, ref pos, len);
			mStatusTime = ParserSTEP.StripLink(str, ref pos, len);
			mStartFloat = ParserSTEP.StripDouble(str, ref pos, len);
			mFinishFloat = ParserSTEP.StripDouble(str, ref pos, len);
			mCompletion = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public abstract partial class IfcSchedulingTime : BaseClassIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcEventTime, IfcLagTime, IfcResourceTime, IfcTaskTime, IfcWorkTime));
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mName == "$" ? ",$,." : ",'" + mName + "',.") + mDataOrigin.ToString() + (mUserDefinedDataOrigin == "$" ? ".,$" : ".,'" + mUserDefinedDataOrigin + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDataOriginEnum>(s.Replace(".", ""), true, out mDataOrigin);
			mUserDefinedDataOrigin = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcSectionedSolid : IfcSolidModel
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + Directrix.Index + ",(#" +
				string.Join(",#", CrossSections.ConvertAll(x => x.Index)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Directrix = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			CrossSections.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcProfileDef));
		}
	}
	public partial class IfcSectionedSolidHorizontal : IfcSectionedSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4X3_RC3 ? ",(#" + string.Join(",#", mCrossSectionPositions_OBSOLETE.ConvertAll(x => x.Index)) : 
				",(" + string.Join(",", CrossSectionPositions.Select(x=>x.ToString()))) + (mFixedAxisVertical ? "),.T." : "),$");	
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4X3_RC3)
				mCrossSectionPositions_OBSOLETE.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcPointByDistanceExpression));
			else
			{
				string field = ParserSTEP.StripField(str, ref pos, len);
				//CrossSectionPositions.AddRange()
			}
			FixedAxisVertical = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcSectionedSpine : IfcGeometricRepresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + SpineCurve.Index + ",(#" + 
				string.Join(",#", CrossSections.ConvertAll(x => x.Index)) + "),(#" + string.Join(",#", mCrossSectionPositions.ConvertAll(x=>x.mIndex)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			SpineCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCompositeCurve;
			CrossSections.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcProfileDef));
			CrossSectionPositions.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcAxis2Placement3D));
		}
	}
	public partial class IfcSectionedSurface : IfcSurface
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",#" + mDirectrix.StepId +
			",(#" + string.Join(",#", mCrossSectionPositions.ConvertAll(x => x.StepId.ToString())) + ")" +
			",(#" + string.Join(",#", mCrossSections.ConvertAll(x => x.StepId.ToString())) + ")" + "," +
			(mFixedAxisVertical ? ".T." : ".F");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Directrix = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			CrossSectionPositions.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcPointByDistanceExpression));
			CrossSections.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcProfileDef));
			FixedAxisVertical = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcSectionProperties : IfcPreDefinedProperties // IFC2x3 STPEntity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mSectionType.ToString() + ".," + ParserSTEP.LinkToString(mStartProfile) + "," + ParserSTEP.LinkToString(mEndProfile); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Enum.TryParse<IfcSectionTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mSectionType);
			mStartProfile = ParserSTEP.StripLink(str, ref pos, len);
			mEndProfile = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcSectionReinforcementProperties : IfcPreDefinedProperties // IFC2x3 STPEntity
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mLongitudinalStartPosition) + "," + ParserSTEP.DoubleToString(mLongitudinalEndPosition) + "," +
			ParserSTEP.DoubleOptionalToString(mTransversePosition) + ",." + mReinforcementRole.ToString() + ".," + ParserSTEP.LinkToString(mSectionDefinition) + ",(" + ParserSTEP.LinkToString(mCrossSectionReinforcementDefinitions[0]);
			for (int icounter = 1; icounter < mCrossSectionReinforcementDefinitions.Count; icounter++)
				result += ",#" + mCrossSectionReinforcementDefinitions;
			return result + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mLongitudinalStartPosition = ParserSTEP.StripDouble(str, ref pos, len);
			mLongitudinalEndPosition = ParserSTEP.StripDouble(str, ref pos, len);
			mTransversePosition = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcReinforcingBarRoleEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mReinforcementRole);
			mSectionDefinition = ParserSTEP.StripLink(str, ref pos, len);
			mCrossSectionReinforcementDefinitions = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public abstract partial class IfcSegment : IfcGeometricRepresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",." + mTransition.ToString() + ".";
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
	public partial class IfcSensor : IfcDistributionControlElement //IFC4  
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
	public partial class IfcSensorType : IfcDistributionControlElementType
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

	//ENTITY IfcServiceLife // DEPRECATED IFC4
	//ENTITY IfcServiceLifeFactor // DEPRECATED IFC4
	public partial class IfcShadingDevice : IfcBuiltElement
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
	public partial class IfcShadingDeviceType : IfcBuiltElementType
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
	public partial class IfcShapeAspect : BaseClassIfc
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", ShapeRepresentations.Select(x=>x.StepId)) + 
				(mName == "$" ? "),$," : "),'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + 
				ParserIfc.LogicalToString(mProductDefinitional) + "," + ParserSTEP.ObjToLinkString(mPartOfProductDefinitionShape);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			foreach (IfcShapeModel shapeModel in ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcShapeModel))
				addRepresentation(shapeModel);
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mProductDefinitional = ParserIfc.StripLogical(str,ref pos, len);
			PartOfProductDefinitionShape = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProductRepresentationSelect;
		}
	}
	public partial class IfcShellBasedSurfaceModel : IfcGeometricRepresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mSbsmBoundary.Select(x=>"#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{ 
			SbsmBoundary.AddRange( ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)).Select(x=>dictionary[x] as IfcShell));
		}
	}
	public partial class IfcSign : IfcElementComponent
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
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
	public partial class IfcSignal : IfcFlowTerminal
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
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
	public partial class IfcSignalType : IfcFlowTerminalType
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
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
	public partial class IfcSignType : IfcElementComponentType
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
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

	public partial class IfcSimplePropertyTemplate : IfcPropertyTemplate
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mDatabase.Release < ReleaseVersion.IFC4)
				return "";
			return base.BuildStringSTEP(release) + (mTemplateType == IfcSimplePropertyTemplateTypeEnum.NOTDEFINED ? ",$," : ",." + mTemplateType.ToString() + ".,") + 
				(mPrimaryMeasureType == "$" ? "$," : "'" + mPrimaryMeasureType + "',") + (mSecondaryMeasureType == "$" ? "$," : "'" + mSecondaryMeasureType + "',") + 
				ParserSTEP.LinkToString(mEnumerators) + "," + ParserSTEP.LinkToString(mPrimaryUnit) + "," + ParserSTEP.LinkToString(mSecondaryUnit) + "," +
				(mExpression == "$" ? "$," : "'" + mExpression + "',") + (mAccessState == IfcStateEnum.NOTDEFINED ? "$" : "." + mAccessState.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSimplePropertyTemplateTypeEnum>(s.Replace(".", ""), true, out mTemplateType);
			mPrimaryMeasureType = ParserSTEP.StripString(str, ref pos, len); ;
			mSecondaryMeasureType = ParserSTEP.StripString(str, ref pos, len); ;
			mEnumerators = ParserSTEP.StripLink(str, ref pos, len);
			mPrimaryUnit = ParserSTEP.StripLink(str, ref pos, len);
			mSecondaryUnit = ParserSTEP.StripLink(str, ref pos, len);
			mExpression = ParserSTEP.StripString(str, ref pos, len);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcStateEnum>(s.Replace(".", ""), true, out mAccessState);
		}
	}
	public partial class IfcSine
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ParserSTEP.DoubleToString(mSineTerm) + "," + ParserSTEP.DoubleToString(mConstant);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			SineTerm = ParserSTEP.StripDouble(str, ref pos, len);
			Constant = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSite : IfcSpatialStructureElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mRefLatitude != null ? "," + mRefLatitude.ToSTEP() + "," : ",$,") + (mRefLongitude != null ?  mRefLongitude.ToSTEP() + "," : "$,") + ParserSTEP.DoubleOptionalToString(mRefElevation) +(mLandTitleNumber== "$" ? ",$," : ",'" + mLandTitleNumber + "',") + ParserSTEP.LinkToString(mSiteAddress); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRefLatitude = IfcCompoundPlaneAngleMeasure.Parse(ParserSTEP.StripField(str, ref pos, len));
			mRefLongitude = IfcCompoundPlaneAngleMeasure.Parse(ParserSTEP.StripField(str, ref pos, len));
			mRefElevation = ParserSTEP.StripDouble(str, ref pos, len);
			mLandTitleNumber = ParserSTEP.StripString(str, ref pos, len);
			mSiteAddress = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcSIUnit : IfcNamedUnit
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
	public partial class IfcSlab : IfcBuiltElement
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
	public partial class IfcSlabType : IfcBuiltElementType
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
	public partial class IfcSlippageConnectionCondition : IfcStructuralConnectionCondition
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," +
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
	public partial class IfcSolarDevice : IfcEnergyConversionDevice //IFC4
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
	public partial class IfcSolarDeviceType : IfcEnergyConversionDeviceType
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
	public partial class IfcSoundProperties : IfcPropertySetDefinition // DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + "," + ParserSTEP.BoolToString(mIsAttenuating) + ",." + mSoundScale.ToString() + ".,(" + ParserSTEP.LinkToString(mSoundValues[0]);
			for (int icounter = 1; icounter < mSoundValues.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSoundValues[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIsAttenuating = ParserSTEP.StripBool(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSoundScaleEnum>(s.Replace(".", ""), true, out mSoundScale);
			mSoundValues = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcSoundValue : IfcPropertySetDefinition // DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mSoundLevelTimeSeries) + "," + ParserSTEP.DoubleToString(mFrequency) + "," + ParserSTEP.DoubleOptionalToString(mSoundLevelSingleValue); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSoundLevelTimeSeries = ParserSTEP.StripLink(str, ref pos, len);
			mFrequency = ParserSTEP.StripDouble(str, ref pos, len);
			mSoundLevelSingleValue = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSpace : IfcSpatialStructureElement, IfcSpaceBoundarySelect
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
	public partial class IfcSpaceHeater : IfcFlowTerminal //IFC4
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
	public partial class IfcSpaceHeaterType : IfcFlowTerminalType
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
	public partial class IfcSpaceProgram : IfcControl // DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mSpaceProgramIdentifier + "'," + ParserSTEP.DoubleOptionalToString(mMaxRequiredArea) + "," + ParserSTEP.DoubleOptionalToString(mMinRequiredArea) + "," + ParserSTEP.LinkToString(mRequestedLocation) + "," + ParserSTEP.DoubleToString(mStandardRequiredArea); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSpaceProgramIdentifier = ParserSTEP.StripField(str, ref pos, len); 
			mMaxRequiredArea = ParserSTEP.StripDouble(str, ref pos, len);
			mMinRequiredArea = ParserSTEP.StripDouble(str, ref pos, len);
			mRequestedLocation = ParserSTEP.StripLink(str, ref pos, len);
			mStandardRequiredArea = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	//ENTITY IfcSpaceThermalLoadProperties // DEPRECATED IFC4
	public partial class IfcSpaceType : IfcSpatialStructureElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcSpaceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcSpatialElement : IfcProduct //ABSTRACT SUPERTYPE OF (ONEOF (IfcExternalSpatialStructureElement ,IfcSpatialStructureElement ,IfcSpatialZone))
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + (mLongName == "$" ? "$" : "'" + mLongName + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			LongName = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public abstract partial class IfcSpatialElementType : IfcTypeProduct //IFC4 ABSTRACT SUPERTYPE OF(IfcSpaceType)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mElementType == "$" ? ",$" : ",'" + mElementType + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mElementType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public abstract partial class IfcSpatialStructureElement : IfcSpatialElement /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBuilding ,IfcBuildingStorey ,IfcSite ,IfcSpace, IfcCivilStructureElement))*/
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
	public partial class IfcSpatialZone : IfcSpatialElement  //IFC4
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
	public partial class IfcSpatialZoneType : IfcSpatialElementType  //IFC4
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
	public partial class IfcSphere : IfcCsgPrimitive3D
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSphericalSurface : IfcElementarySurface //IFC4.2
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
			return base.BuildStringSTEP(release) + ",#" + mPosition.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Position = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement;
		}
	}
	public partial class IfcStackTerminal : IfcFlowTerminal //IFC4
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
	public partial class IfcStackTerminalType : IfcFlowTerminalType
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
	public partial class IfcStair : IfcBuiltElement
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
	public partial class IfcStairFlight : IfcBuiltElement
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
	public partial class IfcStairFlightType : IfcBuiltElementType
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
	public partial class IfcStairType : IfcBuiltElementType
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
	public abstract partial class IfcStructuralAction : IfcStructuralActivity // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveAction, IfcStructuralPointAction, IfcStructuralSurfaceAction))
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mDestabilizingLoad == IfcLogicalEnum.UNKNOWN ? (mDatabase.Release < ReleaseVersion.IFC4 ? "," + ParserSTEP.BoolToString(false) : ",$") : "," + ParserIfc.LogicalToString(mDestabilizingLoad)) + (release < ReleaseVersion.IFC4 ? "," + ParserSTEP.LinkToString(mCausedBy) : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDestabilizingLoad = ParserIfc.StripLogical(str, ref pos, len);
			if (release < ReleaseVersion.IFC4)
				mCausedBy = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public abstract partial class IfcStructuralActivity : IfcProduct //ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralAction, IfcStructuralReaction))
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mAppliedLoad) + ",." + mGlobalOrLocal.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAppliedLoad = ParserSTEP.StripLink(str, ref pos, len);
			Enum.TryParse<IfcGlobalOrLocalEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mGlobalOrLocal);
		}
	}
	public partial class IfcStructuralAnalysisModel : IfcSystem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".," + ParserSTEP.LinkToString(mOrientationOf2DPlane) + 
				(mLoadedBy.Count == 0 ?	",$," : ",(#" + string.Join(",#", mLoadedBy) + "),") + (mHasResults.Count == 0 ? "$" : "(#" + string.Join(",#", mHasResults) + ")") +	
				(release > ReleaseVersion.IFC2x3 ? "," + ParserSTEP.ObjToLinkString(mSharedPlacement) : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcAnalysisModelTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mOrientationOf2DPlane = ParserSTEP.StripLink(str, ref pos, len);
			mLoadedBy = ParserSTEP.StripListLink(str, ref pos, len);
			mHasResults = ParserSTEP.StripListLink(str, ref pos, len);
			if(release > ReleaseVersion.IFC2x3)
				SharedPlacement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcObjectPlacement;
		}
	}
	public abstract partial class IfcStructuralConnection : IfcStructuralItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveConnection ,IfcStructuralPointConnection ,IfcStructuralSurfaceConnection))
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mAppliedCondition); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAppliedCondition = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public abstract partial class IfcStructuralConnectionCondition : BaseClassIfc //ABSTRACT SUPERTYPE OF (ONEOF (IfcFailureConnectionCondition ,IfcSlippageConnectionCondition));
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mName == "$" ? ",$" : ",'" + mName + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mName = ParserSTEP.StripString(str, ref pos, len); }
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
	public partial class IfcStructuralCurveMember : IfcStructuralMember
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + (release < ReleaseVersion.IFC4 ? "." : ".," + ParserSTEP.LinkToString(mAxis)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			Enum.TryParse<IfcStructuralCurveMemberTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			if (release != ReleaseVersion.IFC2x3)
				mAxis = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcStructuralLinearActionVarying : IfcStructuralLinearAction // DELETED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mVaryingAppliedLoadLocation) + ",("
				+ ParserSTEP.LinkToString(mSubsequentAppliedLoads[0]);
			for (int icounter = 1; icounter < mSubsequentAppliedLoads.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSubsequentAppliedLoads[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mVaryingAppliedLoadLocation = ParserSTEP.StripLink(str, ref pos, len);
			mSubsequentAppliedLoads = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public abstract partial class IfcStructuralLoad : BaseClassIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralLoadConfiguration, IfcStructuralLoadOrResult));
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mName == "$" ? ",$" : ",'" + mName + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mName = ParserSTEP.StripString(str, ref pos, len); }
	}
	public partial class IfcStructuralLoadConfiguration : IfcStructuralLoad //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string s = ",$";
			if (mLocations.Count > 0)
			{
				s = ",((" + ParserSTEP.DoubleToString(mLocations[0][0]) + (mLocations[0].Count > 1 ? "," + ParserSTEP.DoubleToString(mLocations[0][1]) : "");
				for (int icounter = 1; icounter < mLocations.Count; icounter++)
					s += "),(" + ParserSTEP.DoubleToString(mLocations[icounter][0]) + (mLocations[icounter].Count > 1 ? "," + ParserSTEP.DoubleToString(mLocations[icounter][1]) : "");
				s += "))";
			}
			return base.BuildStringSTEP(release) + "," + ParserSTEP.ListLinksToString(mValues) + s;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mValues = ParserSTEP.StripListLink(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> fields = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				char[] delim = ",".ToCharArray();
				for (int icounter = 0; icounter < fields.Count; icounter++)
				{
					List<double> list = new List<double>(2);

					string[] ss = fields[icounter].Substring(1, fields[icounter].Length - 2).Split(delim);
					list.Add(ParserSTEP.ParseDouble(ss[0]));
					if (ss.Length > 1)
						list.Add(ParserSTEP.ParseDouble(ss[1]));
					mLocations.Add(list);
				}
			}
		}
	}
	public partial class IfcStructuralLoadCase : IfcStructuralLoadGroup //IFC4
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
	public partial class IfcStructuralLoadGroup : IfcGroup
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".,." + mActionType.ToString() + ".,." + mActionSource.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mCoefficient) + (mPurpose == "$" ? ",$" : ",'" + mPurpose + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcLoadGroupTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcStructuralLoadLinearForce : IfcStructuralLoadStatic
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
	public partial class IfcStructuralLoadPlanarForce : IfcStructuralLoadStatic
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
	public partial class IfcStructuralLoadSingleDisplacement : IfcStructuralLoadStatic
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
	public partial class IfcStructuralLoadSingleDisplacementDistortion : IfcStructuralLoadSingleDisplacement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mDistortion); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDistortion = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralLoadSingleForce : IfcStructuralLoadStatic
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
	public partial class IfcStructuralLoadSingleForceWarping : IfcStructuralLoadSingleForce
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mWarpingMoment); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mWarpingMoment = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralLoadTemperature : IfcStructuralLoadStatic
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
	// IfcStructuralPlanarActionVarying : IfcStructuralPlanarAction //DELETED IFC4
	public partial class IfcStructuralPointConnection : IfcStructuralConnection
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mConditionCoordinateSystem == 0 ? ",$" : ",#" + mConditionCoordinateSystem)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if(release > ReleaseVersion.IFC2x3)
				mConditionCoordinateSystem = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcStructuralProfileProperties : IfcGeneralProfileProperties //IFC4 DELETED Entity replaced by IfcProfileProperties
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
	public partial class IfcStructuralResultGroup : IfcGroup
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
	public partial class IfcStructuralSteelProfileProperties : IfcStructuralProfileProperties //IFC4 DELETED Entity replaced by IfcProfileProperties
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
	public partial class IfcStructuralSurfaceAction : IfcStructuralAction //IFC4 SUPERTYPE OF(IfcStructuralPlanarAction)
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
	public partial class IfcStructuralSurfaceMember : IfcStructuralMember
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mThickness); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcStructuralSurfaceMemberTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mThickness = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcStructuralSurfaceMemberVarying : IfcStructuralSurfaceMember
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.DoubleToString(mSubsequentThickness[0]);
			for (int icounter = 1; icounter < mSubsequentThickness.Count; icounter++)
				str += "," + ParserSTEP.DoubleToString(mSubsequentThickness[icounter]);
			return str + ")" + "," + ParserSTEP.LinkToString(mVaryingThicknessLocation);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSubsequentThickness = ParserSTEP.StripListDouble(str, ref pos, len);
			mVaryingThicknessLocation = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcStructuralSurfaceReaction : IfcStructuralReaction
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
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
	public partial class IfcStyledItem : IfcRepresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(mItem) + ",(" +
				string.Join(",", mStyles.Select(x => "#" + x.StepId)) + (mName == "$" ? "),$" : "),'" + mName + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Item = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcRepresentationItem;
			foreach (IfcStyleAssignmentSelect style in ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcStyleAssignmentSelect))
				addStyle(style);
			mName = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcSubContractResource : IfcConstructionResource
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
	public partial class IfcSubContractResourceType : IfcConstructionResourceType //IFC4
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
	public partial class IfcSubedge : IfcEdge
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mParentEdge); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mParentEdge = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcSurfaceCurve : IfcCurve //IFC4 Add2
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			string geometry = mAssociatedGeometry.Count == 0 ? ",$,." : ",(" + string.Join(",", mAssociatedGeometry.Select(x => "#" + x.StepId)) + "),.";
			return base.BuildStringSTEP(release) + ",#" + mCurve3D.StepId + geometry + mMasterRepresentation.ToString() + "."; 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Curve3D = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			AssociatedGeometry.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcPcurve));
			Enum.TryParse<IfcPreferredSurfaceCurveRepresentation>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mMasterRepresentation);
		}
	}
	public partial class IfcSurfaceCurveSweptAreaSolid : IfcDirectrixCurveSweptAreaSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mReferenceSurface;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mReferenceSurface = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcSurfaceFeature : IfcFeatureElement
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
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
	public partial class IfcSurfaceOfLinearExtrusion : IfcSweptSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mExtrudedDirection) + "," + ParserSTEP.DoubleToString(mDepth); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mExtrudedDirection = ParserSTEP.StripLink(str, ref pos, len);
			mDepth = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSurfaceOfRevolution : IfcSweptSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mAxisPosition); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxisPosition = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcSurfaceReinforcementArea : IfcStructuralLoadOrResult
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
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
	public partial class IfcSurfaceStyle : IfcPresentationStyle, IfcPresentationStyleSelect
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
	public partial class IfcSurfaceStyleLighting : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mDiffuseTransmissionColour) + "," + ParserSTEP.LinkToString(mDiffuseReflectionColour) + "," + ParserSTEP.LinkToString(mTransmissionColour) + "," + ParserSTEP.LinkToString(mReflectanceColour); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mDiffuseTransmissionColour = ParserSTEP.StripLink(str, ref pos, len);
			mDiffuseReflectionColour = ParserSTEP.StripLink(str, ref pos, len);
			mTransmissionColour = ParserSTEP.StripLink(str, ref pos, len);
			mReflectanceColour = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcSurfaceStyleRefraction : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return  base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mRefractionIndex) + "," + ParserSTEP.DoubleOptionalToString(mDispersionFactor); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mRefractionIndex = ParserSTEP.StripDouble(str, ref pos, len);
			mDispersionFactor = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSurfaceStyleRendering : IfcSurfaceStyleShading
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
	public partial class IfcSurfaceStyleShading : IfcPresentationItem, IfcSurfaceStyleElementSelect //SUPERTYPE OF(IfcSurfaceStyleRendering)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			IfcSurfaceStyleRendering rendering = this as IfcSurfaceStyleRendering;
			return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mSurfaceColour) + 
				(rendering != null || release > ReleaseVersion.IFC2x3 ? "," + ParserSTEP.DoubleOptionalToString(mTransparency) : ""); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) 
		{
			mSurfaceColour = ParserSTEP.StripLink(str, ref pos, len); 
			IfcSurfaceStyleRendering rendering = this as IfcSurfaceStyleRendering;
			if(rendering != null || release > ReleaseVersion.IFC2x3)
				mTransparency = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSurfaceStyleWithTextures : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mTextures.Select(x=>x.StepId)) + ")"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mTextures.AddRange( ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcSurfaceTexture)); }
	}
	public abstract partial class IfcSurfaceTexture : IfcPresentationItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcBlobTexture ,IfcImageTexture ,IfcPixelTexture));
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + "," + ParserSTEP.BoolToString(mRepeatS) + "," + ParserSTEP.BoolToString(mRepeatT);
			if (release < ReleaseVersion.IFC4)
			{
				IfcSurfaceTextureEnum texture = IfcSurfaceTextureEnum.NOTDEFINED;
				if (!Enum.TryParse<IfcSurfaceTextureEnum>(mMode,true, out texture))
					texture = IfcSurfaceTextureEnum.NOTDEFINED;
				result += ",." + texture.ToString() + ".,"+ ParserSTEP.LinkToString(mTextureTransform);
			}
			else
			{
				result += (mMode == "$" ? ",$," : ",'" + mMode + "',") + ParserSTEP.LinkToString(mTextureTransform);
				if (mParameter.Count == 0)
					result += ",$";
				else
				{
					result += ",('" + mParameter[0];
					for (int icounter = 1; icounter < mParameter.Count; icounter++)
						result += "','" + mParameter[icounter];
					result += "')";
				}
			}
			return result;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mRepeatS = ParserSTEP.StripBool(str, ref pos, len);
			mRepeatT = ParserSTEP.StripBool(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			mMode = (release < ReleaseVersion.IFC4 ?  s.Replace(".", "") : s.Replace("'", ""));
			mTextureTransform = ParserSTEP.StripLink(str, ref pos, len);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				mParameter = ParserSTEP.SplitListStrings(s.Substring(1, s.Length - 2));
		}
	}
	public abstract partial class IfcSweptAreaSolid : IfcSolidModel  /*ABSTRACT SUPERTYPE OF (ONEOF (IfcExtrudedAreaSolid, IfcFixedReferenceSweptAreaSolid ,IfcRevolvedAreaSolid ,IfcSurfaceCurveSweptAreaSolid))*/
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mSweptArea.StepId + (mPosition == null ? ",$" : ",#" + mPosition.StepId); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			SweptArea = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
			Position = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement3D;
		}
	}
	public partial class IfcSweptDiskSolid : IfcSolidModel
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mDirectrix) + "," + ParserSTEP.DoubleToString(mRadius) + "," + ParserSTEP.DoubleOptionalToString(mInnerRadius) + "," + (release < ReleaseVersion.IFC4 ? ParserSTEP.DoubleToString(mStartParam) + "," + ParserSTEP.DoubleToString(mEndParam) : ParserSTEP.DoubleOptionalToString(mStartParam) + "," + ParserSTEP.DoubleOptionalToString(mEndParam)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mDirectrix = ParserSTEP.StripLink(str, ref pos, len);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mInnerRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mStartParam = ParserSTEP.StripDouble(str, ref pos, len);
			mEndParam = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcSweptDiskSolidPolygonal : IfcSweptDiskSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mFilletRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public abstract partial class IfcSweptSurface : IfcSurface /*	ABSTRACT SUPERTYPE OF (ONEOF (IfcSurfaceOfLinearExtrusion ,IfcSurfaceOfRevolution))*/
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mSweptCurve) + "," + ParserSTEP.LinkToString(mPosition); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mSweptCurve = ParserSTEP.StripLink(str, ref pos, len);
			mPosition = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcSwitchingDevice : IfcFlowController //IFC4
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
	public partial class IfcSwitchingDeviceType : IfcFlowControllerType
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
	//ENTITY IfcSymbolStyle // DEPRECATED IFC4
	public partial class IfcSystemFurnitureElement : IfcFurnishingElement //IFC4
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
	public partial class IfcSystemFurnitureElementType : IfcFurnishingElementType
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
