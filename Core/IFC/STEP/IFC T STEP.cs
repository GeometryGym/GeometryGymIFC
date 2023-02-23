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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class IfcTable
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string s = (string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") + (mRows.Count == 0 ? "$" : "(" + string.Join(",", mRows.Select(x => "#" + x.StepId)) + ")");
			if (release != ReleaseVersion.IFC2x3)
				s += (mColumns.Count == 0 ? ",$" : ",(" + string.Join(",", mColumns.Select(x=>"#" + x.StepId)) + ")");
			return s;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mRows.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcTableRow));
			mColumns.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcTableColumn));
		}
	}
	public partial class IfcTableColumn
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return (release < ReleaseVersion.IFC4 ? "" : (string.IsNullOrEmpty(mIdentifier) ? "$," : "'" + ParserSTEP.Encode(mIdentifier) + "',") +
				(string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$," : "'" + ParserSTEP.Encode(mDescription) + "',") +
				(mUnit == null ? "$" : "#" + mUnit.StepId) + (mReferencePath == null ? ",$" : ",#" + mReferencePath.StepId));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mIdentifier = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
			mReferencePath = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcReference;
		}
	}
	public partial class IfcTableRow
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string s = "";
			if (mRowCells.Count == 0)
				s = "$,";
			else
			{
				s = "(" + mRowCells[0].ToString();
				for (int icounter = 1; icounter < mRowCells.Count; icounter++)
					s += "," + mRowCells[icounter].ToString();
				s += "),";
			}
			return (mRowCells.Count == 0 ? "$," : "(" + string.Join(",", mRowCells) + "),") + ParserSTEP.BoolToString(mIsHeading);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> ss = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < ss.Count; icounter++)
				{
					IfcValue v = ParserIfc.parseValue(ss[icounter]);
					if (v != null)
						mRowCells.Add(v);
				}
			}
			mIsHeading = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcTank
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcTankTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTankTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTankType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTankTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTask 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? ",'" + ParserSTEP.Encode(mIdentification) + "'" : "") +
				(string.IsNullOrEmpty(mStatus) ? ",$," : ",'" + ParserSTEP.Encode(mStatus) + "',") + (string.IsNullOrEmpty(mWorkMethod) ? "$," : ",'" +
				ParserSTEP.Encode(mWorkMethod) + "',") + ParserSTEP.BoolToString(mIsMilestone) + (mPriority == int.MinValue ? ",$" : "," + mPriority) + 
				(release < ReleaseVersion.IFC4 ? "" : "," + ParserSTEP.ObjToLinkString(mTaskTime) + (mPredefinedType == IfcTaskTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
				mIdentification = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mStatus = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mWorkMethod = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mIsMilestone = ParserSTEP.StripBool(str, ref pos, len);
			mPriority = ParserSTEP.StripInt(str, ref pos, len);
			if (release > ReleaseVersion.IFC2x3)
			{
				mTaskTime = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcTaskTime;
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcTaskTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcTaskTime
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release <= ReleaseVersion.IFC2x3)
				return "";
			return base.BuildStringSTEP(release) + (mDurationType == IfcTaskDurationEnum.NOTDEFINED ? ",$," : ",." + mDurationType + ".,") + (mScheduleDuration == null ? "$," : "'" + mScheduleDuration.ValueString + "',") + IfcDateTime.STEPAttribute(mScheduleStart) + "," +
				IfcDateTime.STEPAttribute(mScheduleFinish) + "," + IfcDateTime.STEPAttribute(mEarlyStart) + "," + IfcDateTime.STEPAttribute(mEarlyFinish) + "," + IfcDateTime.STEPAttribute(mLateStart) + "," +
				IfcDateTime.STEPAttribute(mLateFinish) + (mFreeFloat == null ? ",$," : ",'" + mFreeFloat.ValueString + "',") + (mTotalFloat == null ? "$," : "'" + mTotalFloat.ValueString + "',") + ParserSTEP.BoolToString(mIsCritical) + "," +
				IfcDateTime.STEPAttribute(mStatusTime) + "," + (mActualDuration == null ? "$," : "'" + mActualDuration.ValueString + "',") + IfcDateTime.STEPAttribute(mActualStart) + "," + IfcDateTime.STEPAttribute(mActualFinish) + "," +
				(mRemainingTime == null ? "$," : "'" + mRemainingTime.ValueString + "',") + ParserSTEP.DoubleOptionalToString(mCompletion);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTaskDurationEnum>(s.Replace(".", ""), true, out mDurationType);
			s = ParserSTEP.StripString(str, ref pos, len);
			if(s != "$")
				mScheduleDuration = IfcDuration.Convert(s);
			mScheduleStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mScheduleFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mEarlyStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mEarlyFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mLateStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mLateFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			s = ParserSTEP.StripString(str, ref pos, len);
			if (s != "$")
				mFreeFloat = IfcDuration.Convert(s);
			s = ParserSTEP.StripString(str, ref pos, len);
			if (s != "$")
				mTotalFloat = IfcDuration.Convert(s);
			mIsCritical = ParserSTEP.StripBool(str, ref pos, len);
			mStatusTime = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			s = ParserSTEP.StripString(str, ref pos, len);
			if (s != "$")
				mActualDuration = IfcDuration.Convert(s);
			mActualStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mActualFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			s = ParserSTEP.StripString(str, ref pos, len);
			if (s != "$")
				mRemainingTime = IfcDuration.Convert(s);
			mCompletion = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTaskTimeRecurring 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			",#" + mRecurrence.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Recurrence = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcRecurrencePattern;
		}
	}
	public partial class IfcTaskType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + (string.IsNullOrEmpty(mWorkMethod) ? ".,$" : (".,'" + ParserSTEP.Encode(mWorkMethod) + "'"))); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcTaskTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mWorkMethod = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcTelecomAddress
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mTelephoneNumbers.Count == 0 ? ",$" : ",('" + string.Join("','", mTelephoneNumbers.Select(x => ParserSTEP.Encode(x))) + "')") +
				(mFacsimileNumbers.Count == 0 ? ",$" : ",('" + string.Join("','", mFacsimileNumbers.Select(x => ParserSTEP.Encode(x))) + "')") +
				(string.IsNullOrEmpty(mPagerNumber) ? ",$" : ",'" + ParserSTEP.Encode(mPagerNumber) + "'") +
				(mElectronicMailAddresses.Count == 0 ? ",$" : ",('" + string.Join("','", mElectronicMailAddresses.Select(x => ParserSTEP.Encode(x))) + "')") +
				(string.IsNullOrEmpty(mWWWHomePageURL) ? ",$" : ",'" + ParserSTEP.Encode(mWWWHomePageURL) + "'") +
				(release < ReleaseVersion.IFC4 ? "" : (mMessagingIDs.Count == 0 ? ",$" : ",('" + string.Join("','", mMessagingIDs.Select(x => ParserSTEP.Encode(x))) + "')"));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
				{
					string field = lst[icounter];
					if (field.Length > 2)
						mTelephoneNumbers.Add(ParserSTEP.Decode(field.Substring(1, field.Length - 2)));
				}
			}
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
				{
					string field = lst[icounter];
					if (field.Length > 2)
						mFacsimileNumbers.Add(ParserSTEP.Decode(field.Substring(1, field.Length - 2)));
				}
			}
			mPagerNumber = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
				{
					string field = lst[icounter];
					if (field.Length > 2)
						mElectronicMailAddresses.Add(ParserSTEP.Decode(field.Substring(1, field.Length - 2)));
				}
			}
			mWWWHomePageURL = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			if (release > ReleaseVersion.IFC2x3)
			{
				s = ParserSTEP.StripField(str, ref pos, len);
				if (!s.StartsWith("$"))
				{
					List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
					for (int icounter = 0; icounter < lst.Count; icounter++)
					{
						string field = lst[icounter];
						if (field.Length > 2)
							mMessagingIDs.Add(ParserSTEP.Decode(field.Substring(1, field.Length - 2)));
					}
				}
			}
		}
	}
	public partial class IfcTendon
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release != ReleaseVersion.IFC2x3 && mPredefinedType == IfcTendonTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") + ParserSTEP.DoubleToString(mNominalDiameter) + "," +
				ParserSTEP.DoubleToString(mCrossSectionArea) + "," + ParserSTEP.DoubleToString(mTensionForce) + "," +
				ParserSTEP.DoubleToString(mPreStress) + "," + ParserSTEP.DoubleToString(mFrictionCoefficient) + "," +
				ParserSTEP.DoubleToString(mAnchorageSlip) + "," + ParserSTEP.DoubleToString(mMinCurvatureRadius);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTendonTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mTensionForce = ParserSTEP.StripDouble(str, ref pos, len);
			mPreStress = ParserSTEP.StripDouble(str, ref pos, len);
			mFrictionCoefficient = ParserSTEP.StripDouble(str, ref pos, len);
			mAnchorageSlip = ParserSTEP.StripDouble(str, ref pos, len);
			mMinCurvatureRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTendonAnchor
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcTendonAnchorTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTendonAnchorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTendonAnchorType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTendonAnchorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTendonConduit
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
				Enum.TryParse<IfcTendonConduitTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTendonConduitType
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
				Enum.TryParse<IfcTendonConduitTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTendonType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release);
			result += ",." + mPredefinedType + ".," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + ",";
			result += ParserSTEP.DoubleOptionalToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mSheathDiameter);
			return result;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcTendonTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mSheathDiameter = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTerminatorSymbol
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mAnnotatedCurve.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mAnnotatedCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAnnotationCurveOccurrence;
		}
	}
	public abstract partial class IfcTessellatedFaceSet
	{
		protected virtual void WriteStepLineWorker(TextWriter textWriter, ReleaseVersion release)
		{
			textWriter.Write("#" + mCoordinates.StepId);
			if(release == ReleaseVersion.IFC4X3)
				textWriter.Write(mClosed == IfcLogicalEnum.UNKNOWN ? ",$" : "," + ParserSTEP.BoolToString(Closed));
		}
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return "#" + mCoordinates.StepId +
				(release == ReleaseVersion.IFC4X3 ? (mClosed == IfcLogicalEnum.UNKNOWN ? ",$" : "," + ParserSTEP.BoolToString(Closed)) : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) 
		{
			mCoordinates = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianPointList;
			if(release == ReleaseVersion.IFC4X3)
				mClosed = ParserIfc.StripLogical(str, ref pos, len);
		}
	}
	public partial class IfcTextLiteral
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return "'" + ParserSTEP.Encode(mLiteral) + "',#" + mPlacement.StepId + ",." + mPath.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mLiteral = ParserSTEP.Decode(ParserSTEP.StripField(str, ref pos, len));
			mPlacement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement;
			Enum.TryParse<IfcTextPath>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPath);
		}
	}
	public partial class IfcTextLiteralWithExtent
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + ",#" + mExtent.StepId + ",'" + mBoxAlignment.ToString().ToLower().Replace("_","-") + "'";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mExtent = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPlanarExtent;
			Enum.TryParse<IfcBoxAlignment>(ParserSTEP.StripString(str, ref pos, len).Replace("-","_"), true, out mBoxAlignment);
		}
	}
	public partial class IfcTextStyle
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{ 
			return base.BuildStringSTEP(release) + (mTextCharacterAppearance == null ? ",$" : ",#" + mTextCharacterAppearance) +
				(mTextStyle == null ? ",$" : ",#" + mTextStyle.StepId) + (mTextStyle == null ? ",$" : ",#" + mTextStyle.StepId) + "," +
				(mTextFontStyle == null ? ",$" : ",#" + mTextFontStyle.StepId) + 
				(release > ReleaseVersion.IFC2x3 ? "," + ParserSTEP.BoolToString(mModelOrDraughting) : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mTextCharacterAppearance = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCharacterStyleSelect;
			mTextStyle = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcTextStyleSelect;
			mTextFontStyle = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcTextFontSelect;
			if (release > ReleaseVersion.IFC2x3)
				mModelOrDraughting = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcTextStyleFontModel
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mFontFamily.Count == 0 ? ",$," : ",(" + string.Join(",", mFontFamily.Select(x=>ParserSTEP.Encode(x)) + "),")) +
				(string.IsNullOrEmpty(mFontStyle) ? "$," : "'" + ParserSTEP.Encode(mFontStyle) + "',") + 
				(string.IsNullOrEmpty(mFontVariant) ? "$," : "'" + ParserSTEP.Encode(mFontVariant) + "',") + 
				(string.IsNullOrEmpty(mFontWeight) ? "$," : "'" + ParserSTEP.Encode(mFontWeight) + "',") + mFontSize.ToString();
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
					mFontFamily.Add(lst[icounter]);
			}
			mFontStyle = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mFontVariant = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mFontWeight = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mFontSize = ParserIfc.parseSimpleValue(ParserSTEP.StripField(str, ref pos, len)) as IfcSizeSelect;
		}
	}
	public partial class IfcTextStyleForDefinedFont
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "#" + mColour.StepId + (mBackgroundColour == null ? ",$" : ",#" + mBackgroundColour.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mColour = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcColour;
			mBackgroundColour = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcColour;
		}
	}
	public partial class IfcTextStyleTextModel
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (mTextIndent == null ? ",$" : "," + mTextIndent.ToString()) +
			(string.IsNullOrEmpty(mTextAlign) ? ",$" : ",'" + ParserSTEP.Encode(mTextAlign) + "'") +
			(string.IsNullOrEmpty(mTextDecoration) ? ",$" : ",'" + ParserSTEP.Encode(mTextDecoration) + "'") +
			(mLetterSpacing == null ? ",$" : "," + mLetterSpacing.ToString()) +
			(mWordSpacing == null ? ",$" : ",#" + mWordSpacing.ToString()) +
			(string.IsNullOrEmpty(mTextTransform) ? ",$" : ",'" + ParserSTEP.Encode(mTextTransform) + "'") +
			(mLineHeight == null ? ",$" : ",#" + mLineHeight.ToString());
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			TextIndent = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSizeSelect;
			TextAlign = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			TextDecoration = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			LetterSpacing = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSizeSelect;
			WordSpacing = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSizeSelect;
			TextTransform = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			LineHeight = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSizeSelect;
		}
	}
	public partial class IfcTextStyleWithBoxCharacteristics
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return ParserSTEP.DoubleOptionalToString(mBoxHeight) + "," +
				ParserSTEP.DoubleOptionalToString(mBoxWidth) + "," + ParserSTEP.DoubleOptionalToString(mBoxSlantAngle) + "," +
				ParserSTEP.DoubleOptionalToString(mBoxRotateAngle) + "," + mCharacterSpacing.ToString();
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mBoxHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mBoxWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mBoxSlantAngle = ParserSTEP.StripDouble(str, ref pos, len);
			mBoxRotateAngle = ParserSTEP.StripDouble(str, ref pos, len);
			mCharacterSpacing = ParserIfc.parseValue(ParserSTEP.StripField(str, ref pos, len)) as IfcSizeSelect;	
		}
	}
	public abstract partial class IfcTextureCoordinate
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return "(" + string.Join(",", mMaps.Select(x => "#" + x.StepId)) + ")"; 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) 
		{ 
			mMaps.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcSurfaceTexture));
		}
	}
	public partial class IfcTextureCoordinateIndices
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mTexCoordIndex.Select(x=>x.ToString())) + "),#" + mTexCoordsOf.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			TexCoordIndex.AddRange(ParserSTEP.StripListInt(str, ref pos, len));
			TexCoordsOf = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcIndexedPolygonalFace;
		}
	}
	public partial class IfcTextureCoordinateIndicesWithVoids
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" +
				string.Join(",", mInnerTexCoordIndices.ConvertAll(x => "(" + string.Join(",", x) + ")")) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			InnerTexCoordIndices.AddRange(ParserSTEP.StripListListInt(str, ref pos, len));
		}
	}
	public partial class IfcTextureCoordinateGenerator
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",'" + ParserSTEP.Encode(mMode) + "'" + ",(" + string.Join(",", mParameter.ConvertAll(x => formatLength(x))) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Mode = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			Parameter.AddRange(ParserSTEP.StripListDouble(str, ref pos, len));
		}
	}
	public partial class IfcTextureMap
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mVertices.ConvertAll(x => "#" + x.StepId.ToString())) + 
				"),#" + mMappedTo.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Vertices.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcTextureVertex));
			MappedTo = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcFace;
		}
	}
	public partial class IfcTextureVertex
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mCoordinates.ConvertAll(x => formatLength(x))) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Coordinates.AddRange(ParserSTEP.StripListDouble(str, ref pos, len));
		}
	}
	public partial class IfcTextureVertexList
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mTexCoordsList.Select(x => "(" + ParserSTEP.DoubleToString(x.Item1) + "," + ParserSTEP.DoubleToString(x.Item2) + ")")) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) 
		{ 
			mTexCoordsList = ParserSTEP.SplitListDoubleTuple(ParserSTEP.StripField(str, ref pos, len));
		}
	}
	public partial class IfcThermalMaterialProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mSpecificHeatCapacity) + "," + ParserSTEP.DoubleOptionalToString(mBoilingPoint) + "," + ParserSTEP.DoubleOptionalToString(mFreezingPoint) + "," + ParserSTEP.DoubleOptionalToString(mThermalConductivity); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSpecificHeatCapacity = ParserSTEP.StripDouble(str, ref pos, len);
			mBoilingPoint = ParserSTEP.StripDouble(str, ref pos, len);
			mFreezingPoint = ParserSTEP.StripDouble(str, ref pos, len);
			mThermalConductivity = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcThirdOrderPolynomialSpiral
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ParserSTEP.DoubleToString(mQubicTerm) + "," +
			ParserSTEP.DoubleOptionalToString(mQuadraticTerm) + "," + ParserSTEP.DoubleOptionalToString(mLinearTerm) + "," + 
			ParserSTEP.DoubleOptionalToString(mConstantTerm);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			QubicTerm = ParserSTEP.StripDouble(str, ref pos, len);
			QuadraticTerm = ParserSTEP.StripDouble(str, ref pos, len);
			LinearTerm = ParserSTEP.StripDouble(str, ref pos, len);
			ConstantTerm = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTimePeriod
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return IfcDateTime.STEPAttribute(mStart) + "," + IfcDateTime.STEPAttribute(mFinish); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
		}
	}
	public abstract partial class IfcTimeSeries
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return "'" + ParserSTEP.Encode(mName) + (string.IsNullOrEmpty(mDescription) ? "',$," : "','" + ParserSTEP.Encode(mDescription) + "',") +
				(mStartTime == null ? "$" : "#" + mStartTime.StepId) + (mEndTime == null ? ",$" : ",#" + mEndTime.StepId) + ",." + 
				mTimeSeriesDataType.ToString() + ".,." + mDataOrigin.ToString() + (string.IsNullOrEmpty(mUserDefinedDataOrigin) ? ".,$," : ".,'" + ParserSTEP.Encode(mUserDefinedDataOrigin) + "',") + 
				(mUnit == null ? "$" : "#" + mUnit.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mStartTime = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			mEndTime = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			Enum.TryParse<IfcTimeSeriesDataTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mTimeSeriesDataType);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDataOriginEnum>(s.Replace(".", ""), true, out mDataOrigin);
			mUserDefinedDataOrigin = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
		}
	}
	public partial class IfcTimeSeriesValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(#" + string.Join(",#", mListValues.ConvertAll(x => x.StepId.ToString())) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> ss = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < ss.Count; icounter++)
				{
					IfcValue v = ParserIfc.parseValue(ss[icounter]);
					if (v != null)
						mListValues.Add(v);
				}
			}
		}
	}
	public partial class IfcToroidalSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mMajorRadius) + "," + ParserSTEP.DoubleToString(mMinorRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mMajorRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mMinorRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTrackElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcTrackElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTrackElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTrackElementType
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
				Enum.TryParse<IfcTrackElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTransformer
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcTransformerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTransformerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTransformerType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTransformerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTransitionCurveSegment2D
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + StepOptionalLengthString(mStartRadius) + "," + StepOptionalLengthString(mEndRadius) + "," +
				ParserSTEP.BoolToString(mIsStartRadiusCCW) + "," + ParserSTEP.BoolToString(mIsEndRadiusCCW) + ",." + mTransitionCurveType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			StartRadius = ParserSTEP.StripDouble(str, ref pos, len);
			EndRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mIsStartRadiusCCW = ParserSTEP.StripBool(str, ref pos, len);
			mIsEndRadiusCCW = ParserSTEP.StripBool(str, ref pos, len);
			Enum.TryParse<IfcTransitionCurveType>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mTransitionCurveType);
		}
	}
	public partial class IfcTranslationalStiffnessSelect
	{
		public override string ToString() { return (mStiffness == null ? "IFCBOOLEAN(" + ParserSTEP.BoolToString(mRigid) + ")" : mStiffness.ToString()); }
		internal static IfcTranslationalStiffnessSelect Parse(string str,ReleaseVersion version)
		{
			if (str == "$")
				return null;
			if (str.StartsWith("IFCBOOL"))
				return new IfcTranslationalStiffnessSelect(((IfcBoolean)ParserIfc.parseSimpleValue(str)).Boolean);
			if (str.StartsWith("IFCLIN"))
				return new IfcTranslationalStiffnessSelect((IfcLinearStiffnessMeasure)ParserIfc.parseDerivedMeasureValue(str));
			if (str.StartsWith("."))
				return new IfcTranslationalStiffnessSelect(ParserSTEP.ParseBool(str));
			double d = ParserSTEP.ParseDouble(str), tol = 1e-9;
			if (version < ReleaseVersion.IFC4)
			{
				if (Math.Abs(d + 1) < tol)
					return new IfcTranslationalStiffnessSelect(true) { mStiffness = new IfcLinearStiffnessMeasure(-1) };
				if (Math.Abs(d) < tol)
					return new IfcTranslationalStiffnessSelect(false) { mStiffness = new IfcLinearStiffnessMeasure(0) };
			}
			return new IfcTranslationalStiffnessSelect(new IfcLinearStiffnessMeasure(d));
		}
	}
	public partial class IfcTransportElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) +
				(mPredefinedType == IfcTransportElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
			return result + (release < ReleaseVersion.IFC4 ? "," + ParserSTEP.DoubleOptionalToString(mCapacityByWeight) + "," + ParserSTEP.DoubleOptionalToString(mCapacityByNumber) : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if(s.StartsWith("."))
				Enum.TryParse<IfcTransportElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			if(release < ReleaseVersion.IFC4)
			{
				mCapacityByWeight = ParserSTEP.StripDouble(str, ref pos, len);
				mCapacityByNumber = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
	public partial class IfcTransportElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + mPredefinedType.ToString();
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if(s.StartsWith("."))
				Enum.TryParse<IfcTransportElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTrapeziumProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mBottomXDim) + "," + ParserSTEP.DoubleToString(mTopXDim) + "," + ParserSTEP.DoubleToString(mYDim) + "," + ParserSTEP.DoubleToString(mTopXOffset); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mBottomXDim = ParserSTEP.StripDouble(str, ref pos, len);
			mTopXDim = ParserSTEP.StripDouble(str, ref pos, len);
			mYDim = ParserSTEP.StripDouble(str, ref pos, len);
			mTopXOffset = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTriangulatedFaceSet
	{
		internal override void WriteStepLine(TextWriter textWriter, ReleaseVersion release)
		{
			WriteStepLineWorkerPrefix(textWriter);
			WriteStepLineWorker(textWriter, release);
			WriteStepLineWorkerSuffix(textWriter);
		}
		protected override void WriteStepLineWorker(TextWriter textWriter, ReleaseVersion release)
		{
			base.WriteStepLineWorker(textWriter, release);
			if (mNormals.Count == 0)
				textWriter.Write(",$");
			else
				textWriter.Write(",(" + string.Join(",", mNormals.Select(x => "(" + ParserSTEP.DoubleToString(x.Item1) + "," + ParserSTEP.DoubleToString(x.Item2) + "," + ParserSTEP.DoubleToString(x.Item3) + ")")) + ")");
			if (release != ReleaseVersion.IFC4X3)
				textWriter.Write(mClosed == IfcLogicalEnum.UNKNOWN ? ",$" : "," + ParserSTEP.BoolToString(Closed));

			textWriter.Write(",((");
			var first = mCoordIndex.First();
			textWriter.Write(first.Item1);
			textWriter.Write(",");
			textWriter.Write(first.Item2);
			textWriter.Write(",");
			textWriter.Write(first.Item3);
			foreach (var face in mCoordIndex.Skip(1))
			{
				textWriter.Write("),(");
				textWriter.Write(face.Item1);
				textWriter.Write(",");
				textWriter.Write(face.Item2);
				textWriter.Write(",");
				textWriter.Write(face.Item3);
			}
			if (mDatabase != null && mDatabase.Release <= ReleaseVersion.IFC4A1)
			{
				if (mNormalIndex.Count == 0)
					textWriter.Write(")),$");
				else
					textWriter.Write(")),(" + string.Join(",", mNormalIndex.Select(x => "(" + x.Item1 + "," + x.Item2 + "," + x.Item3 + ")")) + ")");
			}
			else
			{
				if (mPnIndex.Count == 0)
					textWriter.Write(")),$");
				else
					textWriter.Write(")),(" + string.Join(",", mPnIndex) + ")");
			}
		}
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			StringBuilder sb = new StringBuilder();
			if (mNormals.Count == 0)
				sb.Append(",$");
			else
				sb.Append(",(" + string.Join(",", mNormals.Select(x => "(" + ParserSTEP.DoubleToString(x.Item1) + "," + ParserSTEP.DoubleToString(x.Item2) + "," + ParserSTEP.DoubleToString(x.Item3) + ")")) + ")");
			if(release != ReleaseVersion.IFC4X3)
				sb.Append( mClosed == IfcLogicalEnum.UNKNOWN ? ",$" : "," + ParserSTEP.BoolToString(Closed));


			sb.Append(",((");
			var first = mCoordIndex.First();
			sb.Append(first.Item1);
			sb.Append(",");
			sb.Append(first.Item2);
			sb.Append(",");
			sb.Append(first.Item3);
			foreach(var face in mCoordIndex.Skip(1))
			{
				sb.Append("),(");
				sb.Append(face.Item1);
				sb.Append(",");
				sb.Append(face.Item2);
				sb.Append(",");
				sb.Append(face.Item3);
			}
			if (mDatabase != null && mDatabase.Release <= ReleaseVersion.IFC4A1)
			{
				if (mNormalIndex.Count == 0)
					sb.Append(")),$");
				else
					sb.Append(")),(" + string.Join(",", mNormalIndex.Select(x => "(" + x.Item1 + "," + x.Item2 + "," + x.Item3 + ")")) + ")");
			}
			else
			{
				if (mPnIndex.Count == 0)
					sb.Append(")),$");
				else
					sb.Append(")),(" + string.Join(",", mPnIndex) + ")");
			}
			return base.BuildStringSTEP(release) + sb.ToString();
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("("))
				mNormals = ParserSTEP.SplitListDoubleTriple(field);
			if(release != ReleaseVersion.IFC4X3)
				mClosed = ParserIfc.StripLogical(str, ref pos, len);
			mCoordIndex.AddRange(ParserSTEP.StripListSTPIntTriple(str, ref pos, len));
			if (release <= ReleaseVersion.IFC4A1)
			{
				mNormalIndex.AddRange(ParserSTEP.StripListSTPIntTriple(str, ref pos, len));
			}
			try
			{
				if (pos < len)
					mPnIndex.AddRange(ParserSTEP.StripListInt(str, ref pos, len));
			}
			catch (Exception) { }
		}
	}
	public partial class IfcTriangulatedIrregularNetwork
	{
		protected override void WriteStepLineWorker(TextWriter textWriter, ReleaseVersion release)
		{
			base.WriteStepLineWorker(textWriter, release);
			if(release >= ReleaseVersion.IFC4X1)
				textWriter.Write(",(" + string.Join(",", mFlags.ConvertAll(x => x.ToString())) + ")");
		}
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release >= ReleaseVersion.IFC4X1 ? ",(" + string.Join(",", mFlags.ConvertAll(x=>x.ToString())) + ")" : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mFlags.AddRange(ParserSTEP.StripListInt(str, ref pos, len));
		}
	}
	public partial class IfcTrimmedCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mBasisCurve.StepId + "," + mTrim1.ToString() + "," + mTrim2.ToString() + "," + ParserSTEP.BoolToString(mSenseAgreement) + ",." + mMasterRepresentation.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mBasisCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			mTrim1 = IfcTrimmingSelect.Parse(dictionary, ParserSTEP.StripField(str, ref pos, len));
			mTrim2 = IfcTrimmingSelect.Parse(dictionary, ParserSTEP.StripField(str, ref pos, len));
			mSenseAgreement = ParserSTEP.StripBool(str, ref pos, len);
			Enum.TryParse<IfcTrimmingPreference>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mMasterRepresentation);
		}
	}
	public partial class IfcTrimmingSelect
	{
		internal static IfcTrimmingSelect Parse(ConcurrentDictionary<int, BaseClassIfc> dictionary, string str)
		{
			IfcTrimmingSelect ts = new IfcTrimmingSelect();
			ts.ParameterValue = double.NaN;
			int i = 0;
			while (str[i] == ' ')
				i++;
			if (str[i] == '(')
				i++;
			char c = str[i];
			if (c == '#')
			{
				string ls = "#";
				i++;
				while (i < str.Length)
				{
					c = str[i];
					if (c == ',' || c == ')')
						break;
					ls += c;
					i++;
				}
				ts.CartesianPoint = dictionary[ParserSTEP.ParseLink(ls)] as IfcCartesianPoint;
				if (c == ',')
				{
					i++;
					while (str[i] == ' ')
						i++;
					if (str.Substring(i).StartsWith("IFCPARAMETERVALUE", true, System.Globalization.CultureInfo.CurrentCulture))
					{
						i += 17;
						while (str[i] == ' ')
							i++;
						if(str[i] == '(')
							i++;
						string pv = "";
						while (str[i] != ')')
							pv += str[i++];
						ts.mParameterValue = ParserSTEP.ParseDouble(pv);
					}
				}
			}
			else
			{
				if (str.Substring(i).StartsWith("IFCPARAMETERVALUE", true, System.Globalization.CultureInfo.CurrentCulture))
				{
					i += 17;
					while (str[i] == ' ')
						i++;
					if (str[i] == '(')
						i++;
					string pv = "";
					while (str[i] != ')')
						pv += str[i++];
					ts.ParameterValue = ParserSTEP.ParseDouble(pv);
				}
				if (++i < str.Length)
				{
					while (str[i] == ' ')
						i++;
					if (str[i++] == ',')
					{
						ts.CartesianPoint = dictionary[ParserSTEP.ParseLink(str.Substring(i, str.Length - i - 1))] as IfcCartesianPoint;
					}
				}
			}
			return ts;
		}
		public override string ToString()
		{
			string str = "(";
			if (!double.IsNaN(mParameterValue))
			{
				str += "IFCPARAMETERVALUE(" + ParserSTEP.DoubleToString(mParameterValue) + ")";
				if (mCartesianPoint != null)
					str += ",#" + mCartesianPoint.StepId;
				return str + ")";
			}
			else
				return str + "#" + mCartesianPoint.StepId + ")";
		}
	}
	public partial class IfcTShapeProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mDepth) + "," + ParserSTEP.DoubleToString(mFlangeWidth) + "," +
				ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mFlangeThickness) + "," +
				ParserSTEP.DoubleOptionalToString(mFilletRadius) + "," + ParserSTEP.DoubleOptionalToString(mFlangeEdgeRadius) + "," +
				ParserSTEP.DoubleOptionalToString(mWebEdgeRadius) + "," + ParserSTEP.DoubleOptionalToString(mWebSlope) + "," +
				ParserSTEP.DoubleOptionalToString(mFlangeSlope) + (release < ReleaseVersion.IFC4 ? "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInX) : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mWebThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeEdgeRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mWebEdgeRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mWebSlope = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeSlope = ParserSTEP.StripDouble(str, ref pos, len);
			if (release < ReleaseVersion.IFC4)
				mCentreOfGravityInX = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTubeBundle
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcTubeBundleTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTubeBundleTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTubeBundleType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTubeBundleTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTunnel
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
				Enum.TryParse<IfcTunnelTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTunnelPart
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
			{
				if (Enum.TryParse<IfcTunnelPartTypeEnum>(s.Replace(".", ""), true, out IfcTunnelPartTypeEnum partType))
					PredefinedType = partType;
			}
		}
	}
	public partial class IfcTwoDirectionRepeatFactor
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mSecondRepeatFactor.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSecondRepeatFactor = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcVector;
		}
	}
	public partial class IfcTypeObject
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			List<IfcPropertySetDefinition> psets = HasPropertySets.Where(x => !x.isEmpty).ToList();
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mApplicableOccurrence) ? ",$," : ",'" + mApplicableOccurrence + "',") +
				(psets.Count == 0 ? "$" : "(" + string.Join("," , psets.ConvertAll(x=>"#" + x.StepId)) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mApplicableOccurrence = ParserSTEP.StripString(str, ref pos, len);
			mHasPropertySets.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=> dictionary[x] as IfcPropertySetDefinition));
		}
	}
	public abstract partial class IfcTypeProcess
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mIdentification) ? ",$," : ",'" + ParserSTEP.Encode(mIdentification) + "',") + 
				(string.IsNullOrEmpty(mLongDescription) ? "$," : "'" + ParserSTEP.Encode(mLongDescription) + "',") + 
				(string.IsNullOrEmpty(mProcessType) ? "$" : "'" + ParserSTEP.Encode(mProcessType) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIdentification = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mLongDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mProcessType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcTypeProduct 
	{ 
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mRepresentationMaps.Count == 0 ? ",$," : ",(" + 
				string.Join(",", mRepresentationMaps.ConvertAll(x=> "#" + x.StepId)) + "),") + (string.IsNullOrEmpty(mTag) ? "$" : "'" + ParserSTEP.Encode(mTag) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RepresentationMaps.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcRepresentationMap));
			mTag = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public abstract partial class IfcTypeResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mIdentification) ? ",$," : ",'" + ParserSTEP.Encode(mIdentification) + "',") + 
				(string.IsNullOrEmpty(mLongDescription) ? "$," : "'" + ParserSTEP.Encode(mLongDescription) + "',") + 
				(string.IsNullOrEmpty(mResourceType) ? "$" : "'" + ParserSTEP.Encode(mResourceType) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIdentification = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mLongDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mResourceType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
}
