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
	public partial class IfcWall : IfcBuildingElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcWallTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcWallTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcWallType : IfcBuildingElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcWallTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcWarpingStiffnessSelect
	{
		public override string ToString() { return (mFixed ? ParserSTEP.BoolToString(mFixed) : ParserSTEP.DoubleToString(mStiffness)); }
		internal static IfcWarpingStiffnessSelect Parse(string str) { if (str.StartsWith(".")) return new IfcWarpingStiffnessSelect(ParserSTEP.ParseBool(str)); return new IfcWarpingStiffnessSelect(ParserSTEP.ParseDouble(str)); }
	}
	public partial class IfcWasteTerminal : IfcFlowTerminal //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcWasteTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcWasteTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcWasteTerminalType : IfcFlowTerminalType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcWasteTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcWaterProperties : IfcMaterialProperties // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.BoolToString(mIsPotable) + "," + ParserSTEP.DoubleOptionalToString(mHardness) + "," + ParserSTEP.DoubleOptionalToString(mAlkalinityConcentration) + "," + ParserSTEP.DoubleOptionalToString(mAcidityConcentration) + "," + ParserSTEP.DoubleOptionalToString(mImpuritiesContent) + "," + ParserSTEP.DoubleOptionalToString(mPHLevel) + "," + ParserSTEP.DoubleOptionalToString(mDissolvedSolidsContent); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIsPotable = ParserSTEP.StripBool(str, ref pos, len);
			mHardness = ParserSTEP.StripDouble(str, ref pos, len);
			mAlkalinityConcentration = ParserSTEP.StripDouble(str, ref pos, len);
			mAcidityConcentration = ParserSTEP.StripDouble(str, ref pos, len);
			mImpuritiesContent = ParserSTEP.StripDouble(str, ref pos, len);
			mPHLevel = ParserSTEP.StripDouble(str, ref pos, len);
			mDissolvedSolidsContent = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcWindow : IfcBuildingElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mOverallHeight) + "," + ParserSTEP.DoubleOptionalToString(mOverallWidth) + (release < ReleaseVersion.IFC4 ? "" : ",." + mPredefinedType + ".,." + mPartitioningType + (mUserDefinedPartitioningType == "$" ? ".,$" : ".,'" + mUserDefinedPartitioningType + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mOverallHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mOverallWidth = ParserSTEP.StripDouble(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcWindowTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
				s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcWindowTypePartitioningEnum>(s.Replace(".", ""), true, out mPartitioningType);
				mUserDefinedPartitioningType = ParserSTEP.StripString(str, ref pos, len);
			}
		}
	}
	public partial class IfcWindowLiningProperties : IfcPreDefinedPropertySet //IFC2x3 : IfcPropertySetDefinition
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mLiningDepth) + "," + ParserSTEP.DoubleOptionalToString(mLiningThickness) + "," + ParserSTEP.DoubleOptionalToString(mTransomThickness) + "," + ParserSTEP.DoubleOptionalToString(mMullionThickness)
				+ "," + ParserSTEP.DoubleOptionalToString(mFirstTransomOffset) + "," + ParserSTEP.DoubleOptionalToString(mSecondTransomOffset) + "," + ParserSTEP.DoubleOptionalToString(mFirstMullionOffset) + "," + ParserSTEP.DoubleOptionalToString(mSecondMullionOffset) + "," +
				ParserSTEP.LinkToString(mShapeAspectStyle) + (release < ReleaseVersion.IFC4 ? "" : "," + ParserSTEP.DoubleOptionalToString(mLiningOffset) + "," + ParserSTEP.DoubleOptionalToString(mLiningToPanelOffsetX) + "," + ParserSTEP.DoubleOptionalToString(mLiningToPanelOffsetY));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mLiningDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mLiningThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mTransomThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mMullionThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mFirstTransomOffset = ParserSTEP.StripDouble(str, ref pos, len);
			mSecondTransomOffset = ParserSTEP.StripDouble(str, ref pos, len);
			mFirstMullionOffset = ParserSTEP.StripDouble(str, ref pos, len);
			mSecondMullionOffset = ParserSTEP.StripDouble(str, ref pos, len);
			mShapeAspectStyle = ParserSTEP.StripLink(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				mLiningOffset = ParserSTEP.StripDouble(str, ref pos, len);
				mLiningToPanelOffsetX = ParserSTEP.StripDouble(str, ref pos, len);
				mLiningToPanelOffsetY = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
	public partial class IfcWindowPanelProperties : IfcPreDefinedPropertySet //IFC2x3: IfcPropertySetDefinition
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mOperationType.ToString() + ".,." + mPanelPosition.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mFrameDepth) + "," + ParserSTEP.DoubleOptionalToString(mFrameThickness) + "," + ParserSTEP.LinkToString(mShapeAspectStyle); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcWindowPanelOperationEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mOperationType);
			Enum.TryParse<IfcWindowPanelPositionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPanelPosition);
			mFrameDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mFrameThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mShapeAspectStyle = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcWindowStyle : IfcTypeProduct // IFC2x3
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mConstructionType.ToString() + ".,." + mOperationType.ToString() + ".," + ParserSTEP.BoolToString(mParameterTakesPrecedence) + "," + ParserSTEP.BoolToString(mSizeable); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcWindowStyleConstructionEnum>(s.Replace(".", ""), true, out mConstructionType);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcWindowStyleOperationEnum>(s.Replace(".", ""), true, out mOperationType);
			mParameterTakesPrecedence = ParserSTEP.StripBool(str, ref pos, len);
			mSizeable = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcWindowType : IfcBuildingElementType //IFCWindowStyle IFC2x3
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (release < ReleaseVersion.IFC4 ? base.BuildStringSTEP(release) + ",.NOTDEFINED.,.NOTDEFINED.," + ParserSTEP.BoolToString(mParameterTakesPrecedence) + "," + ParserSTEP.BoolToString(false) :
				base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".,." + mPartitioningType.ToString() + ".," + ParserSTEP.BoolToString(mParameterTakesPrecedence) + (mUserDefinedPartitioningType == "$" ? ",$" : ",'" + mUserDefinedPartitioningType + "'"));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcWindowTypeEnum>(ParserSTEP.StripField(str,ref pos, len).Replace(".", ""), true, out mPredefinedType);
			Enum.TryParse<IfcWindowTypePartitioningEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPartitioningType);
			mParameterTakesPrecedence = ParserSTEP.StripBool(str, ref pos, len);
			mUserDefinedPartitioningType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcWorkCalendar : IfcControl //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = "";
			if (mWorkingTimes.Count > 0)
			{
				str += ",(" + ParserSTEP.LinkToString(mWorkingTimes[0]);
				for (int icounter = 1; icounter < mWorkingTimes.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mWorkingTimes[icounter]);
				str += "),";
			}
			else
				str += ",$,";
			if (mExceptionTimes.Count > 0)
			{
				str += "(" + ParserSTEP.LinkToString(mExceptionTimes[0]);
				for (int icounter = 1; icounter < mExceptionTimes.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mExceptionTimes[icounter]);
				str += "),.";
			}
			else
				str += "$,.";
			return base.BuildStringSTEP(release) + str + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mWorkingTimes = ParserSTEP.StripListLink(str, ref pos, len);
			mExceptionTimes = ParserSTEP.StripListLink(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcWorkCalendarTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcWorkControl : IfcControl //ABSTRACT SUPERTYPE OF(ONEOF(IfcWorkPlan, IfcWorkSchedule))
	{
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
			{
				mIdentification = ParserSTEP.StripString(str, ref pos, len);
				mSSCreationDate = ParserSTEP.StripLink(str, ref pos, len);
				Creators.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcPerson));
				mPurpose = ParserSTEP.StripString(str, ref pos, len);
				mSSDuration = ParserSTEP.StripDouble(str, ref pos, len);
				mSSTotalFloat = ParserSTEP.StripDouble(str, ref pos, len);
				mSSStartTime = ParserSTEP.StripLink(str, ref pos, len);
				mSSFinishTime = ParserSTEP.StripLink(str, ref pos, len);
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcWorkControlTypeEnum>(s.Replace(".", ""), true, out mWorkControlType);
				mUserDefinedControlType = ParserSTEP.StripString(str, ref pos, len);
			}
			else
			{
				mCreationDate = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
				Creators.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcPerson));
				mPurpose = ParserSTEP.StripString(str, ref pos, len);
				mDuration = ParserSTEP.StripString(str, ref pos, len);
				mTotalFloat = ParserSTEP.StripString(str, ref pos, len);
				mStartTime = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
				mFinishTime = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			}
		}
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + (release < ReleaseVersion.IFC4 ? "'" + mIdentification + "'," + ParserSTEP.LinkToString(mSSCreationDate) : IfcDateTime.STEPAttribute(mCreationDate)) +
				(mCreators.Count > 0 ? ",(#" + string.Join(",#", Creators.ConvertAll(x=>x.Index)) + ")," : ",$,") +
				(release < ReleaseVersion.IFC4 ? (mPurpose == "$" ? "$," : "'" + mPurpose + "',") + ParserSTEP.DoubleOptionalToString(mSSDuration) + "," + ParserSTEP.DoubleOptionalToString(mSSTotalFloat) + "," +
					ParserSTEP.LinkToString(mSSStartTime) + "," + ParserSTEP.LinkToString(mSSFinishTime) + ",." + mWorkControlType.ToString() + (mUserDefinedControlType == "$" ? ".,$" : ".,'" + mUserDefinedControlType + "'") :
				(mPurpose == "$" ? "$," : "'" + mPurpose + "',") + mDuration + "," + mTotalFloat + "," + IfcDateTime.STEPAttribute(mStartTime) + "," + IfcDateTime.STEPAttribute(mFinishTime));
		}
	}
	public partial class IfcWorkPlan : IfcWorkControl
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcWorkPlanTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcWorkSchedule : IfcWorkControl
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcWorkScheduleTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcWorkTime : IfcSchedulingTime //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRecurrencePattern) + ","+ IfcDate.STEPAttribute(mStart) + "," + IfcDate.STEPAttribute(mFinish)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRecurrencePattern = ParserSTEP.StripLink(str, ref pos, len);
			mStart = IfcDate.ParseSTEP(ParserSTEP.StripString(str, ref pos, len));
			mFinish = IfcDate.ParseSTEP(ParserSTEP.StripString(str, ref pos, len));
		}
	}
}
