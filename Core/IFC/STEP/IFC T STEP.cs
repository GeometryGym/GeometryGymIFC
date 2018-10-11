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
	public partial class IfcTable : BaseClassIfc, IfcMetricValueSelect, IfcObjectReferenceSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string s = "";
			if (mRows.Count == 0)
				s = "$";
			else
			{
				s = "(" + ParserSTEP.LinkToString(mRows[0]);
				for (int icounter = 1; icounter < mRows.Count; icounter++)
					s += "," + ParserSTEP.LinkToString(mRows[icounter]);
				s += ")";
			}
			if (release != ReleaseVersion.IFC2x3)
			{
				if (mColumns.Count == 0)
					s += ",$";
				else
				{
					s += ",(" + ParserSTEP.LinkToString(mColumns[0]);
					for (int icounter = 1; icounter < mColumns.Count; icounter++)
						s += "," + ParserSTEP.LinkToString(mColumns[icounter]);
					s += ")";
				}
			}
			return base.BuildStringSTEP(release) + (mName == "$" ? ",$," : ",'" + mName + "',") + s;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mRows = ParserSTEP.StripListLink(str, ref pos, len);
			mColumns = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcTableColumn : BaseClassIfc
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + (mIdentifier == "$" ? ",$," : ",'" + mIdentifier + "',") + (mName == "$" ? "$," : "'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserSTEP.LinkToString(mUnit) + "," + ParserSTEP.LinkToString(mReferencePath)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mIdentifier = ParserSTEP.StripString(str, ref pos, len);
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mUnit = ParserSTEP.StripLink(str, ref pos, len);
			mReferencePath = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcTableRow : BaseClassIfc
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string s = "";
			if (mRowCells.Count == 0)
				s = ",$,";
			else
			{
				s = ",(" + mRowCells[0].ToString();
				for (int icounter = 1; icounter < mRowCells.Count; icounter++)
					s += "," + mRowCells[icounter].ToString();
				s += "),";
			}
			return base.BuildStringSTEP(release) + s + ParserSTEP.BoolToString(mIsHeading);
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
	public partial class IfcTank : IfcFlowStorageDevice //IFC4
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
	public partial class IfcTankType : IfcFlowStorageDeviceType
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
	public partial class IfcTask : IfcProcess //SUPERTYPE OF (ONEOF(IfcMove,IfcOrderAction) both depreceated IFC4) 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? ",'" + ParserIfc.Encode(mIdentification) + "'" : "") +
				(string.IsNullOrEmpty(mStatus) ? ",$," : ",'" + ParserIfc.Encode(mStatus) + "',") + (string.IsNullOrEmpty(mWorkMethod) ? "$," : ",'" + ParserIfc.Encode(mWorkMethod) + "',") +
				ParserSTEP.BoolToString(mIsMilestone) + "," + mPriority + (release < ReleaseVersion.IFC4 ? "" : "," + ParserSTEP.LinkToString(mTaskTime) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
				mIdentification = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mStatus = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mWorkMethod = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mIsMilestone = ParserSTEP.StripBool(str, ref pos, len);
			mPriority = ParserSTEP.StripInt(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				mTaskTime = ParserSTEP.StripLink(str, ref pos, len);
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcTaskTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcTaskTime : IfcSchedulingTime //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release <= ReleaseVersion.IFC2x3)
				return "";
			return base.BuildStringSTEP(release) + ",." + mDurationType + (mScheduleDuration == "$" ? ".,$," : ".,'" + mScheduleDuration + "',") + IfcDateTime.STEPAttribute(mScheduleStart) + "," +
				IfcDateTime.STEPAttribute(mScheduleFinish) + "," + IfcDateTime.STEPAttribute(mEarlyStart) + "," + IfcDateTime.STEPAttribute(mEarlyFinish) + "," + IfcDateTime.STEPAttribute(mLateStart) + "," +
				IfcDateTime.STEPAttribute(mLateFinish) + (mFreeFloat == "$" ? ",$," : ",'" + mFreeFloat + "',") + (mTotalFloat == "$" ? "$," : "'" + mTotalFloat + "',") + ParserSTEP.BoolToString(mIsCritical) + "," +
				IfcDateTime.STEPAttribute(mStatusTime) + "," + (mActualDuration == "$" ? "$," : "'" + mActualDuration + "',") + IfcDateTime.STEPAttribute(mActualStart) + "," + IfcDateTime.STEPAttribute(mActualFinish) + "," +
				(mRemainingTime == "$" ? "$," : "'" + mRemainingTime + "',") + ParserSTEP.DoubleOptionalToString(mCompletion);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTaskDurationEnum>(s.Replace(".", ""), true, out mDurationType);
			mScheduleDuration = ParserSTEP.StripString(str, ref pos, len);
			mScheduleStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mScheduleFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mEarlyStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mEarlyFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mLateStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mLateFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mFreeFloat = ParserSTEP.StripString(str, ref pos, len);
			mTotalFloat = ParserSTEP.StripString(str, ref pos, len);
			mIsCritical = ParserSTEP.StripBool(str, ref pos, len);
			mStatusTime = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mActualDuration = ParserSTEP.StripString(str, ref pos, len);
			mActualStart = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mActualFinish = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mRemainingTime = ParserSTEP.StripString(str, ref pos, len);
			mCompletion = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTaskType : IfcTypeProcess //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + (mWorkMethod == "$" ? ".,$" : (".,'" + mWorkMethod + "'"))); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcTaskTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mWorkMethod = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcTelecomAddress : IfcAddress
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mTelephoneNumbers.Count == 0 ? ",$" : ",('" + string.Join("','", mTelephoneNumbers.Select(x => ParserIfc.Encode(x))) + "')") +
				(mFacsimileNumbers.Count == 0 ? ",$" : ",('" + string.Join("','", mFacsimileNumbers.Select(x => ParserIfc.Encode(x))) + "')") +
				(string.IsNullOrEmpty(mPagerNumber) ? ",$" : ",'" + ParserIfc.Encode(mPagerNumber) + "'") +
				(mElectronicMailAddresses.Count == 0 ? ",$" : ",('" + string.Join("','", mElectronicMailAddresses.Select(x => ParserIfc.Encode(x))) + "')") +
				(string.IsNullOrEmpty(mWWWHomePageURL) ? ",$" : ",'" + ParserIfc.Encode(mWWWHomePageURL) + "'") +
				(release <= ReleaseVersion.IFC4 ? "" : (mMessagingIDs.Count == 0 ? ",$" : ",('" + string.Join("','", mMessagingIDs.Select(x => ParserIfc.Encode(x))) + "')"));
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
						mTelephoneNumbers.Add(ParserIfc.Decode(field.Substring(1, field.Length - 2)));
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
						mFacsimileNumbers.Add(ParserIfc.Decode(field.Substring(1, field.Length - 2)));
				}
			}
			mPagerNumber = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
				{
					string field = lst[icounter];
					if (field.Length > 2)
						mElectronicMailAddresses.Add(ParserIfc.Decode(field.Substring(1, field.Length - 2)));
				}
			}
			mWWWHomePageURL = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
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
							mMessagingIDs.Add(ParserIfc.Decode(field.Substring(1, field.Length - 2)));
					}
				}
			}
		}
	}
	public partial class IfcTendon : IfcReinforcingElement
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
	public partial class IfcTendonAnchor : IfcReinforcingElement
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
	//IfcTendonAnchorType
	public partial class IfcTendonType : IfcReinforcingElementType  //IFC4
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
	public partial class IfcTerminatorSymbol : IfcAnnotationSymbolOccurrence // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mAnnotatedCurve; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mAnnotatedCurve = ParserSTEP.StripLink(str, ref pos, len); }
	}
	public abstract partial class IfcTessellatedFaceSet : IfcTessellatedItem, IfcBooleanOperand //ABSTRACT SUPERTYPE OF(IfcTriangulatedFaceSet, IfcPolygonalFaceSet )
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mCoordinates); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mCoordinates = ParserSTEP.StripLink(str, ref pos, len); }
	}
	public partial class IfcTextLiteral : IfcGeometricRepresentationItem //SUPERTYPE OF	(IfcTextLiteralWithExtent)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + ParserIfc.Encode(mLiteral) + "'," + ParserSTEP.LinkToString(mPlacement) + ",." + mPath.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mLiteral = ParserIfc.Decode(ParserSTEP.StripField(str, ref pos, len));
			mPlacement = ParserSTEP.StripLink(str, ref pos, len);
			Enum.TryParse<IfcTextPath>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPath);
		}
	}
	public partial class IfcTextLiteralWithExtent : IfcTextLiteral
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mExtent) + ",'" + mBoxAlignment + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mExtent = ParserSTEP.StripLink(str, ref pos, len);
			mBoxAlignment = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcTextStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mTextCharacterAppearance) + "," + ParserSTEP.LinkToString(mTextStyle) + "," + ParserSTEP.LinkToString(mTextFontStyle) + (release != ReleaseVersion.IFC2x3 ? "," + ParserSTEP.BoolToString(mModelOrDraughting) : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mTextCharacterAppearance = ParserSTEP.StripLink(str, ref pos, len);
			mTextStyle = ParserSTEP.StripLink(str, ref pos, len);
			mTextFontStyle = ParserSTEP.StripLink(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
				mModelOrDraughting = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcTextStyleFontModel : IfcPreDefinedTextFont
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release);
			if (mFontFamily.Count > 0)
			{
				str += ",(" + mFontFamily[0];
				for (int icounter = 1; icounter < mFontFamily.Count; icounter++)
					str += "," + mFontFamily[icounter];
				str += "),";
			}
			else
				str += ",$,";
			return str + (mFontStyle == "$" ? "$," : "'" + mFontStyle + "',") + (mFontVariant == "$" ? "$," : "'" + mFontVariant + "',") + (mFontWeight == "$" ? "$," : "'" + mFontWeight + "',") + mFontSize;
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
			mFontStyle = ParserSTEP.StripString(str, ref pos, len);
			mFontVariant = ParserSTEP.StripString(str, ref pos, len);
			mFontWeight = ParserSTEP.StripString(str, ref pos, len);
			mFontSize = ParserSTEP.StripField(str, ref pos, len);
		}
	}
	public partial class IfcTextStyleForDefinedFont : BaseClassIfc
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mColour) + "," + ParserSTEP.LinkToString(mBackgroundColour); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mColour = ParserSTEP.StripLink(str, ref pos, len);
			mBackgroundColour = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcTextStyleTextModel : IfcPresentationItem
	{
		//protected override string BuildString() { return (mModel.mOutputEssential ? "" : base.BuildString() + "," + IFCModel.mSTP.STPLinkToString(mDiffuseTransmissionColour) + "," + IFCModel.mSTP.STPLinkToString(mDiffuseReflectionColour) + "," + IFCModel.mSTP.STPLinkToString(mTransmissionColour) + "," + IFCModel.mSTP.STPLinkToString(mReflectanceColour)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			//mDiffuseTransmissionColour = ParserSTEP.StripLink(str, ref pos, len);
			//mDiffuseReflectionColour = ParserSTEP.StripLink(str, ref pos, len);
			//mTransmissionColour = ParserSTEP.StripLink(str, ref pos, len);
			//mReflectanceColour = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	//ENTITY IfcTextStyleWithBoxCharacteristics; // DEPRECEATED IFC4
	public abstract partial class IfcTextureCoordinate : IfcPresentationItem  //ABSTRACT SUPERTYPE OF(ONEOF(IfcIndexedTextureMap, IfcTextureCoordinateGenerator, IfcTextureMap))
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + ",(#" + mMaps[0];
			for (int icounter = 1; icounter < mMaps.Count; icounter++)
				result += ",#" + mMaps[icounter];
			return result + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mMaps = ParserSTEP.StripListLink(str, ref pos, len); }
	}
	//ENTITY IfcTextureCoordinateGenerator
	//ENTITY IfcTextureMap
	//ENTITY IfcTextureVertex;
	public partial class IfcTextureVertexList : IfcPresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			double[] pair = mTexCoordsList[0];
			string result = base.BuildStringSTEP(release) + ",((" + ParserSTEP.DoubleToString(pair[0]) + "," + ParserSTEP.DoubleToString(pair[1]);
			for (int icounter = 1; icounter < mTexCoordsList.Length; icounter++)
			{
				pair = mTexCoordsList[icounter];
				result += "),(" + ParserSTEP.DoubleToString(pair[0]) + "," + ParserSTEP.DoubleToString(pair[1]);
			}
			return result + "))";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mTexCoordsList = ParserSTEP.SplitListDoubleTuple(ParserSTEP.StripField(str, ref pos, len)); }
	}
	public partial class IfcThermalMaterialProperties : IfcMaterialProperties // DEPRECEATED IFC4
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
	public partial class IfcTimePeriod : BaseClassIfc // IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mStart + "','" + mFinish + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mStart = ParserSTEP.StripString(str, ref pos, len);
			mFinish = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public abstract partial class IfcTimeSeries : BaseClassIfc, IfcMetricValueSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcIrregularTimeSeries,IfcRegularTimeSeries));
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mName + "','" + mDescription + "'," + ParserSTEP.LinkToString(mStartTime) + "," + ParserSTEP.LinkToString(mEndTime) + ",." + mTimeSeriesDataType.ToString() + ".,." + mDataOrigin.ToString() + (mUserDefinedDataOrigin == "$" ? ".,$," : ".,'" + mUserDefinedDataOrigin + "',") + ParserSTEP.LinkToString(mUnit); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mStartTime = ParserSTEP.StripLink(str, ref pos, len);
			mEndTime = ParserSTEP.StripLink(str, ref pos, len);
			Enum.TryParse<IfcTimeSeriesDataTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mTimeSeriesDataType);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDataOriginEnum>(s.Replace(".", ""), true, out mDataOrigin);
			mUserDefinedDataOrigin = ParserSTEP.StripString(str, ref pos, len);
			mUnit = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	//ENTITY IfcTimeSeriesReferenceRelationship; // DEPRECEATED IFC4
	//ENTITY IfcTimeSeriesSchedule // DEPRECEATED IFC4
	//ENTITY IfcTimeSeriesValue;  
	public partial class IfcToroidalSurface : IfcElementarySurface //IFC4.2
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mMajorRadius) + "," + ParserSTEP.DoubleToString(mMinorRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mMajorRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mMinorRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTransformer : IfcEnergyConversionDevice //IFC4
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
	public partial class IfcTransformerType : IfcEnergyConversionDeviceType
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
	public partial class IfcTransitionCurveSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mStartRadius) + "," + ParserSTEP.DoubleOptionalToString(mEndRadius) + "," +
				ParserSTEP.BoolToString(mIsStartRadiusCCW) + "," + ParserSTEP.BoolToString(mIsEndRadiusCCW) + ",." + mTransitionCurveType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mStartRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mEndRadius = ParserSTEP.StripDouble(str, ref pos, len);
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
	public partial class IfcTransportElement : IfcElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mPredefinedType == IfcTransportElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".") + (mDatabase.Release < ReleaseVersion.IFC4 ? "," + ParserSTEP.DoubleOptionalToString(mCapacityByWeight) + "," + ParserSTEP.DoubleOptionalToString(mCapacityByNumber) : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTransportElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			if(release < ReleaseVersion.IFC4)
			{
				mCapacityByWeight = ParserSTEP.StripDouble(str, ref pos, len);
				mCapacityByNumber = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
	public partial class IfcTransportElementType : IfcElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTransportElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcTrapeziumProfileDef : IfcParameterizedProfileDef
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
	public partial class IfcTriangulatedFaceSet : IfcTessellatedFaceSet
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			StringBuilder sb = new StringBuilder();
			if (mNormals.Length == 0)
				sb.Append( ",$,");
			else
			{
				double[] normal = mNormals[0];
				sb.Append( ",((" + ParserSTEP.DoubleToString(normal[0]) + "," + ParserSTEP.DoubleToString(normal[1]) + "," + ParserSTEP.DoubleToString(normal[2]) + ")");
				for (int icounter = 1; icounter < mNormals.Length; icounter++)
				{
					normal = mNormals[icounter];
					sb.Append( ",(" + ParserSTEP.DoubleToString(normal[0]) + "," + ParserSTEP.DoubleToString(normal[1]) + "," + ParserSTEP.DoubleToString(normal[2]) + ")");
				}
				sb.Append("),");
			}
			sb.Append( mClosed == IfcLogicalEnum.UNKNOWN ? "$" : ParserSTEP.BoolToString(Closed));
			Tuple<int, int, int> p = mCoordIndex[0];
			sb.Append(",((" + p.Item1 + "," + p.Item2 + "," + p.Item3);
			for (int icounter = 1; icounter < mCoordIndex.Length; icounter++)
			{
				p = mCoordIndex[icounter];
				sb.Append("),(" + p.Item1 + "," + p.Item2 + "," + p.Item3);
			}
			if (mDatabase != null && mDatabase.Release <= ReleaseVersion.IFC4A1)
			{
				if (mNormalIndex.Length == 0)
					sb.Append(")),$");
				else
				{
					p = mNormalIndex[0];
					sb.Append(")),((" + p.Item1 + "," + p.Item2 + "," + p.Item3);
					for (int icounter = 1; icounter < mNormalIndex.Length; icounter++)
					{
						p = mNormalIndex[icounter];
						sb.Append("),(" + p.Item1 + "," + p.Item2 + "," + p.Item3);
					}
					sb.Append("))");
				}
			}
			else
			{
				if (mPnIndex.Count == 0)
					sb.Append(")),$");
				else
				{
					sb.Append(")),(" + mPnIndex[0]);
					for (int icounter = 1; icounter < mPnIndex.Count; icounter++)
						sb.Append("," + mPnIndex[icounter]);
					sb.Append(")");
				}
			}
			return base.BuildStringSTEP(release) + sb.ToString();
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("("))
				mNormals = ParserSTEP.SplitListDoubleTriple(field);
			mClosed = ParserIfc.StripLogical(str, ref pos, len);
			field = ParserSTEP.StripField(str, ref pos, len);
			mCoordIndex = ParserSTEP.SplitListSTPIntTriple(field);
			if (release <= ReleaseVersion.IFC4A1)
			{
				field = ParserSTEP.StripField(str, ref pos, len);
				if (field.StartsWith("("))
					mNormalIndex = ParserSTEP.SplitListSTPIntTriple(field);
			}
			try
			{
				if (pos < len)
					mPnIndex = ParserSTEP.StripListInt(str, ref pos, len);
			}
			catch (Exception) { }
		}
	}
	public partial class IfcTriangulatedIrregularNetwork : IfcTriangulatedFaceSet
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mFlags.ConvertAll(x=>x.ToString())) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPnIndex = ParserSTEP.StripListInt(str, ref pos, len);
		}
	}
	public partial class IfcTrimmedCurve : IfcBoundedCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mBasisCurve.Index + "," + mTrim1.ToString() + "," + mTrim2.ToString() + "," + ParserSTEP.BoolToString(mSenseAgreement) + ",." + mMasterRepresentation.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mBasisCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			mTrim1 = IfcTrimmingSelect.Parse(ParserSTEP.StripField(str, ref pos, len));
			mTrim2 = IfcTrimmingSelect.Parse(ParserSTEP.StripField(str, ref pos, len));
			mSenseAgreement = ParserSTEP.StripBool(str, ref pos, len);
			Enum.TryParse<IfcTrimmingPreference>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mMasterRepresentation);
		}
	}
	public partial class IfcTrimmingSelect
	{
		internal static IfcTrimmingSelect Parse(string str)
		{
			IfcTrimmingSelect ts = new IfcTrimmingSelect();
			ts.mIfcParameterValue = double.NaN;
			int i = 0;
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
				ts.mIfcCartesianPoint = ParserSTEP.ParseLink(ls);
				if (c == ',')
				{
					if (str.Substring(i + 1).StartsWith("IFCPARAMETERVALUE(", true, System.Globalization.CultureInfo.CurrentCulture))
					{
						i += 19;
						string pv = "";
						while (str[i] != ')')
						{
							pv += str[i++];
						}
						ts.mIfcParameterValue = ParserSTEP.ParseDouble(pv);
					}
				}
			}
			else
			{
				if (str.Substring(i).StartsWith("IFCPARAMETERVALUE(", true, System.Globalization.CultureInfo.CurrentCulture))
				{
					i += 18;
					string pv = "";
					while (str[i] != ')')
					{
						pv += str[i++];
					}
					ts.mIfcParameterValue = ParserSTEP.ParseDouble(pv);
				}
				if (++i < str.Length)
				{
					if (str[i++] == ',')
					{
						ts.mIfcCartesianPoint = ParserSTEP.ParseLink(str.Substring(i, str.Length - i - 1));
					}
				}
			}
			return ts;
		}
		public override string ToString()
		{
			string str = "(";
			if (!double.IsNaN(mIfcParameterValue))
			{
				str += "IFCPARAMETERVALUE(" + ParserSTEP.DoubleToString(mIfcParameterValue) + ")";
				if (mIfcCartesianPoint > 0)
					str += "," + ParserSTEP.LinkToString(mIfcCartesianPoint);
				return str + ")";
			}
			else
				return str + ParserSTEP.LinkToString(mIfcCartesianPoint) + ")";
		}
	}
	public partial class IfcTShapeProfileDef : IfcParameterizedProfileDef
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
	public partial class IfcTubeBundle : IfcEnergyConversionDevice //IFC4
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
	public partial class IfcTubeBundleType : IfcEnergyConversionDeviceType
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
	public partial class IfcTwoDirectionRepeatFactor : IfcOneDirectionRepeatFactor // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mSecondRepeatFactor); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSecondRepeatFactor = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcTypeObject : IfcObjectDefinition //(IfcTypeProcess, IfcTypeProduct, IfcTypeResource) IFC4 ABSTRACT 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			List<IfcPropertySetDefinition> psets = HasPropertySets.Where(x => !x.isEmpty).ToList();
			return base.BuildStringSTEP(release) + (mApplicableOccurrence == "$" ? ",$," : ",'" + mApplicableOccurrence + "',") +(psets.Count == 0 ? "$" : "(#" + string.Join(",#" , psets.ConvertAll(x=>x.mIndex)) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mApplicableOccurrence = ParserSTEP.StripString(str, ref pos, len);
			mHasPropertySets.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=> dictionary[x] as IfcPropertySetDefinition));
		}
	}
	public abstract partial class IfcTypeProcess : IfcTypeObject //ABSTRACT SUPERTYPE OF(ONEOF(IfcEventType, IfcProcedureType, IfcTaskType))
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',") + (mLongDescription == "$" ? "$," : "'" + mLongDescription + "',") + (mProcessType == "$" ? "$" : "'" + mProcessType + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIdentification = ParserSTEP.StripString(str, ref pos, len);
			mLongDescription = ParserSTEP.StripString(str, ref pos, len);
			mProcessType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcTypeProduct : IfcTypeObject, IfcProductSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcDoorStyle ,IfcElementType ,IfcSpatialElementType ,IfcWindowStyle)) 
	{ 
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mRepresentationMaps.Count == 0 ? ",$," : ",(#" + string.Join(",#", mRepresentationMaps.ConvertAll(x=>x.mIndex)) + "),") +
				(mTag == "$" ? "$" : "'" + mTag + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RepresentationMaps.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcRepresentationMap));
			mTag = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public abstract partial class IfcTypeResource : IfcTypeObject //ABSTRACT SUPERTYPE OF(IfcConstructionResourceType)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',") + (mLongDescription == "$" ? "$," : "'" + mLongDescription + "',") + (mResourceType == "$" ? "$" : "'" + mResourceType + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIdentification = ParserSTEP.StripString(str, ref pos, len);
			mLongDescription = ParserSTEP.StripString(str, ref pos, len);
			mResourceType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
}
