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
		protected override string BuildStringSTEP()
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
			if (mDatabase.mRelease != ReleaseVersion.IFC2x3)
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
			return base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + s;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mRows = ParserSTEP.StripListLink(str, ref pos, len);
			mColumns = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcTableColumn : BaseClassIfc
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + (mIdentifier == "$" ? ",$," : ",'" + mIdentifier + "',") + (mName == "$" ? "$," : "'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserSTEP.LinkToString(mUnit) + "," + ParserSTEP.LinkToString(mReferencePath)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
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
		protected override string BuildStringSTEP()
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
			return base.BuildStringSTEP() + s + ParserSTEP.BoolToString(mIsHeading);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcTankTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTankTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcTankType : IfcFlowStorageDeviceType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTankTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcTask : IfcProcess //SUPERTYPE OF (ONEOF(IfcMove,IfcOrderAction) both depreceated IFC4) 
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ",'" + mIdentification + "'" : "") + "," + mStatus + "," + mWorkMethod + "," + ParserSTEP.BoolToString(mIsMilestone) + "," + mPriority.ToString() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.LinkToString(mTaskTime) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			if (release == ReleaseVersion.IFC2x3)
				mIdentification = ParserSTEP.StripString(str, ref pos, len);
			mStatus = ParserSTEP.StripString(str, ref pos, len);
			mWorkMethod = ParserSTEP.StripString(str, ref pos, len);
			mIsMilestone = ParserSTEP.StripBool(str, ref pos, len);
			mPriority = ParserSTEP.StripInt(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				mTaskTime = ParserSTEP.StripLink(str, ref pos, len);
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcTaskTypeEnum>(s.Replace(".", ""), out mPredefinedType);
			}
		}
	}
	public partial class IfcTaskTime : IfcSchedulingTime //IFC4
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",." + mDurationType + (mScheduleDuration == "$" ? ".,$," : ".,'" + mScheduleDuration + "',") + IfcDateTime.formatSTEP(mScheduleStart) + "," +
				IfcDateTime.formatSTEP(mScheduleFinish) + "," + IfcDateTime.formatSTEP(mEarlyStart) + "," + IfcDateTime.formatSTEP(mEarlyFinish) + "," + IfcDateTime.formatSTEP(mLateStart) + "," +
				IfcDateTime.formatSTEP(mLateFinish) + (mFreeFloat == "$" ? ",$," : ",'" + mFreeFloat + "',") + (mTotalFloat == "$" ? "$," : "'" + mTotalFloat + "',") + ParserSTEP.BoolToString(mIsCritical) + "," +
				IfcDateTime.formatSTEP(mStatusTime) + "," + (mActualDuration == "$" ? "$," : "'" + mActualDuration + "',") + IfcDateTime.formatSTEP(mActualStart) + "," + IfcDateTime.formatSTEP(mActualFinish) + "," +
				(mRemainingTime == "$" ? "$," : "'" + mRemainingTime + "',") + ParserSTEP.DoubleOptionalToString(mCompletion);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTaskDurationEnum>(s.Replace(".", ""), out mDurationType);
			mScheduleDuration = ParserSTEP.StripString(str, ref pos, len);
			mScheduleStart = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mScheduleFinish = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mEarlyStart = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mEarlyFinish = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mLateStart = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mLateFinish = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mFreeFloat = ParserSTEP.StripString(str, ref pos, len);
			mTotalFloat = ParserSTEP.StripString(str, ref pos, len);
			mIsCritical = ParserSTEP.StripBool(str, ref pos, len);
			mStatusTime = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mActualDuration = ParserSTEP.StripString(str, ref pos, len);
			mActualStart = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mActualFinish = IfcDateTime.parseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mRemainingTime = ParserSTEP.StripString(str, ref pos, len);
			mCompletion = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTaskType : IfcTypeProcess //IFC4
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + (mWorkMethod == "$" ? ".,$" : (".,'" + mWorkMethod + "'"))); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			Enum.TryParse<IfcTaskTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mPredefinedType);
			mWorkMethod = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcTelecomAddress : IfcAddress
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP();
			if (mTelephoneNumbers.Count == 0)
				str += ",$,";
			else
			{
				str += ",('" + mTelephoneNumbers[0];
				for (int icounter = 1; icounter < mTelephoneNumbers.Count; icounter++)
					str += "','" + mTelephoneNumbers[icounter];
				str += "'),";
			}
			if (mFacsimileNumbers.Count == 0)
				str += "$,";
			else
			{
				str += "('" + mFacsimileNumbers[0];
				for (int icounter = 1; icounter < mFacsimileNumbers.Count; icounter++)
					str += "','" + mFacsimileNumbers[icounter];
				str += "'),";
			}

			str += mPagerNumber;
			if (mElectronicMailAddresses.Count == 0)
				str += ",$,";
			else
			{
				str += ",('" + mElectronicMailAddresses[0];
				for (int icounter = 1; icounter < mElectronicMailAddresses.Count; icounter++)
					str += "','" + mElectronicMailAddresses[icounter];
				str += "'),";
			}
			str += mWWWHomePageURL;
			if (mDatabase.mRelease != ReleaseVersion.IFC2x3)
			{
				if (mMessagingIDs.Count == 0)
					str += ",$";
				else
				{
					str += ",('" + mMessagingIDs[0];
					for (int icounter = 1; icounter < mMessagingIDs.Count; icounter++)
						str += "','" + mMessagingIDs[icounter];
					str += "')";
				}
			}
			return str;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
					mTelephoneNumbers.Add(lst[icounter]);
			}
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
					mFacsimileNumbers.Add(lst[icounter]);
			}
			mPagerNumber = ParserSTEP.StripString(str, ref pos, len);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
					mElectronicMailAddresses.Add(lst[icounter]);
			}
			mWWWHomePageURL = ParserSTEP.StripString(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				s = ParserSTEP.StripField(str, ref pos, len);
				if (!s.StartsWith("$"))
					mMessagingIDs = ParserSTEP.SplitListStrings(str.Substring(1, str.Length - 2));
			}
		}
	}
	public partial class IfcTendon : IfcReinforcingElement
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease != ReleaseVersion.IFC2x3 && mPredefinedType == IfcTendonTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") + ParserSTEP.DoubleToString(mNominalDiameter) + "," +
				ParserSTEP.DoubleToString(mCrossSectionArea) + "," + ParserSTEP.DoubleToString(mTensionForce) + "," +
				ParserSTEP.DoubleToString(mPreStress) + "," + ParserSTEP.DoubleToString(mFrictionCoefficient) + "," +
				ParserSTEP.DoubleToString(mAnchorageSlip) + "," + ParserSTEP.DoubleToString(mMinCurvatureRadius);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTendonTypeEnum>(s.Replace(".", ""), out mPredefinedType);
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcTendonAnchorTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTendonAnchorTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	//IfcTendonAnchorType
	public partial class IfcTendonType : IfcReinforcingElementType  //IFC4
	{
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP();
			result += ",." + mPredefinedType + ".," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + ",";
			result += ParserSTEP.DoubleOptionalToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mSheathDiameter);
			return result;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			Enum.TryParse<IfcTendonTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mPredefinedType);
			mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
			mSheathDiameter = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTerminatorSymbol : IfcAnnotationSymbolOccurrence // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",#" + mAnnotatedCurve; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mAnnotatedCurve = ParserSTEP.StripLink(str, ref pos, len); }
	}
	public abstract partial class IfcTessellatedFaceSet : IfcTessellatedItem, IfcBooleanOperand //ABSTRACT SUPERTYPE OF(IfcTriangulatedFaceSet, IfcPolygonalFaceSet )
	{
		protected override string BuildStringSTEP() { return  base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mCoordinates); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mCoordinates = ParserSTEP.StripLink(str, ref pos, len); }
	}
	public partial class IfcTextLiteral : IfcGeometricRepresentationItem //SUPERTYPE OF	(IfcTextLiteralWithExtent)
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mLiteral + "'," + ParserSTEP.LinkToString(mPlacement) + ",." + mPath.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mLiteral = ParserSTEP.StripField(str, ref pos, len);
			mPlacement = ParserSTEP.StripLink(str, ref pos, len);
			Enum.TryParse<IfcTextPath>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mPath);
		}
	}
	public partial class IfcTextLiteralWithExtent : IfcTextLiteral
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mExtent) + ",'" + mBoxAlignment + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mExtent = ParserSTEP.StripLink(str, ref pos, len);
			mBoxAlignment = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcTextStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mTextCharacterAppearance) + "," + ParserSTEP.LinkToString(mTextStyle) + "," + ParserSTEP.LinkToString(mTextFontStyle) + (mDatabase.mRelease != ReleaseVersion.IFC2x3 ? "," + ParserSTEP.BoolToString(mModelOrDraughting) : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mTextCharacterAppearance = ParserSTEP.StripLink(str, ref pos, len);
			mTextStyle = ParserSTEP.StripLink(str, ref pos, len);
			mTextFontStyle = ParserSTEP.StripLink(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
				mModelOrDraughting = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcTextStyleFontModel : IfcPreDefinedTextFont
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP();
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mColour) + "," + ParserSTEP.LinkToString(mBackgroundColour); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mColour = ParserSTEP.StripLink(str, ref pos, len);
			mBackgroundColour = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcTextStyleTextModel : IfcPresentationItem
	{
		//protected override string BuildString() { return (mModel.mOutputEssential ? "" : base.BuildString() + "," + IFCModel.mSTP.STPLinkToString(mDiffuseTransmissionColour) + "," + IFCModel.mSTP.STPLinkToString(mDiffuseReflectionColour) + "," + IFCModel.mSTP.STPLinkToString(mTransmissionColour) + "," + IFCModel.mSTP.STPLinkToString(mReflectanceColour)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
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
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + ",(#" + mMaps[0];
			for (int icounter = 1; icounter < mMaps.Count; icounter++)
				result += ",#" + mMaps[icounter];
			return result + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mMaps = ParserSTEP.StripListLink(str, ref pos, len); }
	}
	//ENTITY IfcTextureCoordinateGenerator
	//ENTITY IfcTextureMap
	//ENTITY IfcTextureVertex;
	public partial class IfcTextureVertexList : IfcPresentationItem
	{
		protected override string BuildStringSTEP()
		{
			Tuple<double, double> pair = mTexCoordsList[0];
			string result = base.BuildStringSTEP() + ",((" + ParserSTEP.DoubleToString(pair.Item1) + "," + ParserSTEP.DoubleToString(pair.Item2);
			for (int icounter = 1; icounter < mTexCoordsList.Length; icounter++)
			{
				pair = mTexCoordsList[icounter];
				result += "),(" + ParserSTEP.DoubleToString(pair.Item1) + "," + ParserSTEP.DoubleToString(pair.Item2);
			}

			return result + "))";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mTexCoordsList = ParserSTEP.SplitListDoubleTuple(ParserSTEP.StripField(str, ref pos, len)); }
	}
	public partial class IfcThermalMaterialProperties : IfcMaterialPropertiesSuperseded // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mSpecificHeatCapacity) + "," + ParserSTEP.DoubleOptionalToString(mBoilingPoint) + "," + ParserSTEP.DoubleOptionalToString(mFreezingPoint) + "," + ParserSTEP.DoubleOptionalToString(mThermalConductivity); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mSpecificHeatCapacity = ParserSTEP.StripDouble(str, ref pos, len);
			mBoilingPoint = ParserSTEP.StripDouble(str, ref pos, len);
			mFreezingPoint = ParserSTEP.StripDouble(str, ref pos, len);
			mThermalConductivity = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTimePeriod : BaseClassIfc // IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mStart + "','" + mFinish + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mStart = ParserSTEP.StripString(str, ref pos, len);
			mFinish = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public abstract partial class IfcTimeSeries : BaseClassIfc, IfcMetricValueSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcIrregularTimeSeries,IfcRegularTimeSeries));
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mName + "','" + mDescription + "'," + ParserSTEP.LinkToString(mStartTime) + "," + ParserSTEP.LinkToString(mEndTime) + ",." + mTimeSeriesDataType.ToString() + ".,." + mDataOrigin.ToString() + (mUserDefinedDataOrigin == "$" ? ".,$," : ".,'" + mUserDefinedDataOrigin + "',") + ParserSTEP.LinkToString(mUnit); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mStartTime = ParserSTEP.StripLink(str, ref pos, len);
			mEndTime = ParserSTEP.StripLink(str, ref pos, len);
			Enum.TryParse<IfcTimeSeriesDataTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mTimeSeriesDataType);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDataOriginEnum>(s.Replace(".", ""), out mDataOrigin);
			mUserDefinedDataOrigin = ParserSTEP.StripString(str, ref pos, len);
			mUnit = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	//ENTITY IfcTimeSeriesReferenceRelationship; // DEPRECEATED IFC4
	//ENTITY IfcTimeSeriesSchedule // DEPRECEATED IFC4
	//ENTITY IfcTimeSeriesValue;  
	public partial class IfcToroidalSurface : IfcElementarySurface //IFC4.2
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mMajorRadius) + "," + ParserSTEP.DoubleToString(mMinorRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mMajorRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mMinorRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTransformer : IfcEnergyConversionDevice //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcTransformerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTransformerTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcTransformerType : IfcEnergyConversionDeviceType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTransformerTypeEnum>(s.Replace(".", ""), out mPredefinedType);
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
			if (version == ReleaseVersion.IFC2x3)
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mPredefinedType == IfcTransportElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".") + (mDatabase.Release == ReleaseVersion.IFC2x3 ? "," + ParserSTEP.DoubleOptionalToString(mCapacityByWeight) + "," + ParserSTEP.DoubleOptionalToString(mCapacityByNumber) : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTransportElementTypeEnum>(s.Replace(".", ""), out mPredefinedType);
			if(release == ReleaseVersion.IFC2x3)
			{
				mCapacityByWeight = ParserSTEP.StripDouble(str, ref pos, len);
				mCapacityByNumber = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
	public partial class IfcTransportElementType : IfcElementType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTransportElementTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcTrapeziumProfileDef : IfcParameterizedProfileDef
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mBottomXDim) + "," + ParserSTEP.DoubleToString(mTopXDim) + "," + ParserSTEP.DoubleToString(mYDim) + "," + ParserSTEP.DoubleToString(mTopXOffset); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mBottomXDim = ParserSTEP.StripDouble(str, ref pos, len);
			mTopXDim = ParserSTEP.StripDouble(str, ref pos, len);
			mYDim = ParserSTEP.StripDouble(str, ref pos, len);
			mTopXOffset = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTriangulatedFaceSet : IfcTessellatedFaceSet
	{
		protected override string BuildStringSTEP()
		{
			StringBuilder sb = new StringBuilder();
			if (mNormals.Length == 0)
				sb.Append( ",$,");
			else
			{
				Tuple<double, double, double> normal = mNormals[0];
				sb.Append( ",((" + ParserSTEP.DoubleToString(normal.Item1) + "," + ParserSTEP.DoubleToString(normal.Item2) + "," + ParserSTEP.DoubleToString(normal.Item3) + ")");
				for (int icounter = 1; icounter < mNormals.Length; icounter++)
				{
					normal = mNormals[icounter];
					sb.Append( ",(" + ParserSTEP.DoubleToString(normal.Item1) + "," + ParserSTEP.DoubleToString(normal.Item2) + "," + ParserSTEP.DoubleToString(normal.Item3) + ")");
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
			if (mPnIndex.Count == 0)
				sb.Append(",$");
			else
			{
				sb.Append(",(" + mPnIndex[0]);
				for (int icounter = 1; icounter < mPnIndex.Count; icounter++)
					sb.Append("," + mPnIndex[icounter]);
				sb.Append(")");
			}
			return base.BuildStringSTEP() + sb.ToString();
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("("))
				mNormals = ParserSTEP.SplitListDoubleTriple(field);
			mClosed = ParserIfc.StripLogical(str, ref pos, len);
			field = ParserSTEP.StripField(str, ref pos, len);
			mCoordIndex = ParserSTEP.SplitListSTPIntTriple(field);
			field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("("))
				mNormalIndex = ParserSTEP.SplitListSTPIntTriple(field);
			try
			{
				if (pos < len)
					mPnIndex = ParserSTEP.StripListInt(str, ref pos, len);
			}
			catch (Exception) { }
		}
	}
	public partial class IfcTrimmedCurve : IfcBoundedCurve
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBasisCurve) + "," + mTrim1.ToString() + "," + mTrim2.ToString() + "," + ParserSTEP.BoolToString(mSenseAgreement) + ",." + mMasterRepresentation.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mBasisCurve = ParserSTEP.StripLink(str, ref pos, len);
			mTrim1 = IfcTrimmingSelect.Parse(ParserSTEP.StripField(str, ref pos, len));
			mTrim2 = IfcTrimmingSelect.Parse(ParserSTEP.StripField(str, ref pos, len));
			mSenseAgreement = ParserSTEP.StripBool(str, ref pos, len);
			Enum.TryParse<IfcTrimmingPreference>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mMasterRepresentation);
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
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mDepth) + "," + ParserSTEP.DoubleToString(mFlangeWidth) + "," +
				ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mFlangeThickness) + "," +
				ParserSTEP.DoubleOptionalToString(mFilletRadius) + "," + ParserSTEP.DoubleOptionalToString(mFlangeEdgeRadius) + "," +
				ParserSTEP.DoubleOptionalToString(mWebEdgeRadius) + "," + ParserSTEP.DoubleOptionalToString(mWebSlope) + "," +
				ParserSTEP.DoubleOptionalToString(mFlangeSlope) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInX) : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mWebThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeEdgeRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mWebEdgeRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mWebSlope = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeSlope = ParserSTEP.StripDouble(str, ref pos, len);
			if (release == ReleaseVersion.IFC2x3)
				mCentreOfGravityInX = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcTubeBundle : IfcEnergyConversionDevice //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcTubeBundleTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTubeBundleTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcTubeBundleType : IfcEnergyConversionDeviceType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTubeBundleTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcTwoDirectionRepeatFactor : IfcOneDirectionRepeatFactor // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mSecondRepeatFactor); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mSecondRepeatFactor = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcTypeObject : IfcObjectDefinition //(IfcTypeProcess, IfcTypeProduct, IfcTypeResource) IFC4 ABSTRACT 
	{
		protected override string BuildStringSTEP()
		{
			string psetlist = "";
			if (mHasPropertySets.Count > 0)
			{
				int icounter = 0;
				ReadOnlyCollection<IfcPropertySetDefinition> psets = HasPropertySets;
				for(icounter = 0; icounter < psets.Count; icounter++ )
				{
					if (psets[icounter].isEmpty)
						continue;
					psetlist = "#" + psets[icounter].mIndex;
					break;
				}
				for (icounter++; icounter < psets.Count; icounter++)
				{
					if (!psets[icounter].isEmpty)
						psetlist += ",#" + psets[icounter].mIndex;
				}
			}
			return base.BuildStringSTEP() + (mApplicableOccurrence == "$" ? ",$," : ",'" + mApplicableOccurrence + "',") +(string.IsNullOrEmpty(psetlist) ? "$" : "(" + psetlist + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mApplicableOccurrence = ParserSTEP.StripString(str, ref pos, len);
			mHasPropertySets = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			ReadOnlyCollection<IfcPropertySetDefinition> psets = HasPropertySets;
			foreach(IfcPropertySetDefinition pset in psets)
				pset.mDefinesType.Add(this);
		}
	}
	public abstract partial class IfcTypeProcess : IfcTypeObject //ABSTRACT SUPERTYPE OF(ONEOF(IfcEventType, IfcProcedureType, IfcTaskType))
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',") + (mLongDescription == "$" ? "$," : "'" + mLongDescription + "',") + (mProcessType == "$" ? "$" : "'" + mProcessType + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mIdentification = ParserSTEP.StripString(str, ref pos, len);
			mLongDescription = ParserSTEP.StripString(str, ref pos, len);
			mProcessType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcTypeProduct : IfcTypeObject, IfcProductSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcDoorStyle ,IfcElementType ,IfcSpatialElementType ,IfcWindowStyle)) 
	{ 
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",";
			if (mRepresentationMaps.Count > 0)
			{
				str += "(" + ParserSTEP.LinkToString(mRepresentationMaps[0]);
				for (int icounter = 1; icounter < mRepresentationMaps.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRepresentationMaps[icounter]);
				str += ")";
			}
			else
				str += "$";
			return str + (mTag == "$" ? ",$" : ",'" + mTag + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRepresentationMaps = ParserSTEP.StripListLink(str, ref pos, len);
			mTag = ParserSTEP.StripString(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			ReadOnlyCollection<IfcRepresentationMap> repMaps = RepresentationMaps;
			foreach(IfcRepresentationMap repMap in repMaps)
				repMap.mRepresents.Add(this);
		}
	}
	public abstract partial class IfcTypeResource : IfcTypeObject //ABSTRACT SUPERTYPE OF(IfcConstructionResourceType)
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',") + (mLongDescription == "$" ? "$," : "'" + mLongDescription + "',") + (mResourceType == "$" ? "$" : "'" + mResourceType + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mIdentification = ParserSTEP.StripString(str, ref pos, len);
			mLongDescription = ParserSTEP.StripString(str, ref pos, len);
			mResourceType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
}
