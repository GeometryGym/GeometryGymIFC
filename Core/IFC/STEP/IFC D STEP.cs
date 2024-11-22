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
	public partial class IfcDamper
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcDamperTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDamperTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDamperType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDamperTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDatasetInformation
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4X4_DRAFT)
				return "";
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mSchemaReference) ? ",$" : ",'" + ParserSTEP.Encode(mSchemaReference) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSchemaReference = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDatasetReference
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4X4_DRAFT)
				return "";
			return base.BuildStringSTEP(release) +
				(string.IsNullOrEmpty(mDescription) ? ",$," : ",'" + ParserSTEP.Encode(mDescription) + "',") +
				(mReferencedDataset == null ? "$" : "#" + mReferencedDataset.StepId) +
				(string.IsNullOrEmpty(mFilter) ? ",$" : ",'" + ParserSTEP.Encode(mFilter) + "'"); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mReferencedDataset = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDatasetInformation;
			mFilter = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDateAndTime
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mDateComponent.StepId + ",#" + mTimeComponent.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mDateComponent = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCalendarDate;
			mTimeComponent = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcLocalTime;
		}
	}
	public partial class IfcDefinedSymbol
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mDefinition.StepId + ",#" + mTarget.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mDefinition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDefinedSymbolSelect;
			mTarget = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianTransformationOperator2D;
		}
	}
	public partial class IfcDerivedProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + ",#" + mParentProfile.StepId + (mOperator == null ? ",*" : ",#" + mOperator.StepId) +
				(string.IsNullOrEmpty(mLabel) ? ",$" : ",'" + ParserSTEP.Encode(mLabel) + "'"); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mParentProfile = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
			mOperator = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianTransformationOperator2D;
			mLabel = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDerivedUnit
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mElements.Select(x=> "#" +x.StepId)) +
				"),." + mUnitType.ToString() + (string.IsNullOrEmpty(mUserDefinedType) ? ".,$" : ".,'" + ParserSTEP.Encode(mUserDefinedType) + "'") +
				(release >= ReleaseVersion.IFC4X3 ? (string.IsNullOrEmpty(mName) ? ",$" : ",'" + ParserSTEP.Encode(mName) + "'") : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Elements.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcDerivedUnitElement));
			Enum.TryParse<IfcDerivedUnitEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mUnitType);
			mUserDefinedType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			if(release >= ReleaseVersion.IFC4X3)
				mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDerivedUnitElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mUnit.StepId + "," + ParserSTEP.IntToString(mExponent); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Unit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcNamedUnit;
			mExponent = ParserSTEP.StripInt(str, ref pos, len);
		}
	}
	public partial class IfcDimensionalExponents
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return mLengthExponent + "," + mMassExponent + "," + mTimeExponent + "," + mElectricCurrentExponent + "," + mThermodynamicTemperatureExponent + "," + mAmountOfSubstanceExponent + "," + mLuminousIntensityExponent; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mLengthExponent = ParserSTEP.StripInt(str, ref pos, len);
			mMassExponent = ParserSTEP.StripInt(str, ref pos, len);
			mTimeExponent = ParserSTEP.StripInt(str, ref pos, len);
			mElectricCurrentExponent = ParserSTEP.StripInt(str, ref pos, len);
			mThermodynamicTemperatureExponent = ParserSTEP.StripInt(str, ref pos, len);
			mAmountOfSubstanceExponent = ParserSTEP.StripInt(str, ref pos, len);
			mLuminousIntensityExponent = ParserSTEP.StripInt(str, ref pos, len);
		}
	}
	public partial class IfcDimensionCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + String.Join(",", mAnnotatedBySymbols.Select(x=>"#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mAnnotatedBySymbols.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcTerminatorSymbol));
		}
	}
	public partial class IfcDimensionCurveTerminator
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mRole.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcDimensionExtentUsage>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mRole);
		}
	}
	public partial class IfcDirection
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{ 
			return "(" + FormatRatio(mDirectionRatioX) + "," +
				(FormatRatio(mDirectionRatioY) + (double.IsNaN(mDirectionRatioZ) ? "" : "," + FormatRatio(mDirectionRatioZ))) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			string s = str.Trim();
			if (s[0] == '(')
			{
				string[] fields = s.Substring(1, s.Length - 2).Split(",".ToCharArray());
				if (fields != null && fields.Length > 0)
				{
					mDirectionRatioX = ParserSTEP.ParseDouble(fields[0]);
					if (fields.Length > 1)
					{
						mDirectionRatioY = ParserSTEP.ParseDouble(fields[1]);
						if (fields.Length > 2)
							mDirectionRatioZ = ParserSTEP.ParseDouble(fields[2]);
					}
				}
			}
		}
	}
	public abstract partial class IfcDirectrixCurveSweptAreaSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string parameters = "$,$";
			if (release < ReleaseVersion.IFC4X3_RC2)
			{
				IfcParameterValue startParameter = mStartParam as IfcParameterValue, endParameter = mEndParam as IfcParameterValue;
				parameters = (startParameter == null ? "$," : ParserSTEP.DoubleToString(startParameter.Measure) + ",") +
					(endParameter == null ? "$" : ParserSTEP.DoubleToString(endParameter.Measure));
			}
			else
				parameters = (mStartParam == null ? "$," : mStartParam.ToString() + ",") + (mEndParam == null ? "$" : mEndParam.ToString());

			return base.BuildStringSTEP(release) + ",#" + mDirectrix.StepId + "," + parameters;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Directrix = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("I"))
				mStartParam = ParserIfc.parseMeasureValue(field) as IfcCurveMeasureSelect;
			else
			{
				double d = ParserSTEP.ParseDouble(field);
				if (!double.IsNaN(d))
					mStartParam = new IfcParameterValue(d);
			}
			field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("I"))
				mEndParam = ParserIfc.parseMeasureValue(field) as IfcCurveMeasureSelect;
			else
			{
				double d = ParserSTEP.ParseDouble(field);
				if (!double.IsNaN(d))
					mEndParam = new IfcParameterValue(d);
			}
		}
	}
	public partial class IfcDiscreteAccessory
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcDiscreteAccessoryTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcDiscreteAccessoryTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcDiscreteAccessoryType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcDiscreteAccessoryTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDiscreteAccessoryTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDistributionBoard
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			(mPredefinedType == IfcDistributionBoardTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDistributionBoardTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDistributionBoardType
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
				Enum.TryParse<IfcDistributionBoardTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDistributionChamberElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcDistributionChamberElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcDistributionChamberElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcDistributionChamberElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDistributionChamberElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDistributionControlElement
	{ 
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? (string.IsNullOrEmpty(mControlElementId) ? ",$" : ",'" + ParserSTEP.Encode(mControlElementId) + "'") : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if(release < ReleaseVersion.IFC4)
				mControlElementId = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDistributionPort
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (mFlowDirection == IfcFlowDirectionEnum.NOTDEFINED ? ",$" : ",." + mFlowDirection.ToString() + ".") +
				(release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcDistributionPortTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".") + 
				(mSystemType == IfcDistributionSystemEnum.NOTDEFINED ? ",$" : ",." + mSystemType + "."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFlowDirectionEnum>(s.Replace(".", ""), true, out mFlowDirection);
			if (release > ReleaseVersion.IFC2x3)
			{
				s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcDistributionPortTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
				s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcDistributionSystemEnum>(s.Replace(".", ""), true, out mSystemType);
			}
		}
	}
	public partial class IfcDistributionSystem
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (string.IsNullOrEmpty(mLongName) ? ",$,." : ",'" + ParserSTEP.Encode(mLongName) + "',.") + mPredefinedType.ToString() + "."); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mLongName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDistributionSystemEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDocumentElectronicFormat
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return (string.IsNullOrEmpty(mFileExtension) ? "$," : "'" + ParserSTEP.Encode(mFileExtension) + ",',") + 
				(string.IsNullOrEmpty(mMimeContentType) ? "$," : "'" + ParserSTEP.Encode(mMimeContentType) + "',") + 
				(string.IsNullOrEmpty(mMimeSubtype) ? "$" : "'" + ParserSTEP.Encode(mMimeSubtype) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mFileExtension = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mMimeContentType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mMimeSubtype = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDocumentInformation
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
			{
				return "'" + ParserSTEP.Encode(mIdentification) + "','" + ParserSTEP.Encode(mName) +
					(string.IsNullOrEmpty(mDescription) ? "',$," : "','" + ParserSTEP.Encode(mDescription) + "',") +
					(mDocumentReferences.Count == 0 ? "$," : "(#" + string.Join(",#", mDocumentReferences) + "),") +
					(string.IsNullOrEmpty(mPurpose) ? "$," : "'" + ParserSTEP.Encode(mPurpose) + "',") + 
					(string.IsNullOrEmpty(mIntendedUse) ? "$," : "'" + ParserSTEP.Encode(mIntendedUse) + "',") +
					(string.IsNullOrEmpty(mScope) ? "$," : "'" + ParserSTEP.Encode(mScope) + "',") +
					(string.IsNullOrEmpty(mRevision) ? "$," : "'" + ParserSTEP.Encode(mRevision) + "',") +
					(mDocumentOwner == null ? "$" : "#" + mDocumentOwner.StepId) +
					(mEditors.Count == 0 ? ",$," : ",(" + string.Join(",", mEditors.Select(x => "#" + x.StepId) + "),")) +
					ParserSTEP.ObjToLinkString(mSSCreationTime) + "," + ParserSTEP.ObjToLinkString(mSSLastRevisionTime) + "," +
					ParserSTEP.ObjToLinkString(mSSElectronicFormat) + "," + ParserSTEP.ObjToLinkString(mSSValidFrom) + "," + 
					ParserSTEP.ObjToLinkString(mSSValidUntil) + "," + 
					(mConfidentiality == IfcDocumentConfidentialityEnum.NOTDEFINED ? ",$," : ",." + mConfidentiality.ToString() + ".,") + 
					(mStatus == IfcDocumentStatusEnum.NOTDEFINED ? "$" : "." + mStatus.ToString() + ".");
			}
			return "'" + ParserSTEP.Encode(mIdentification) + "','" + ParserSTEP.Encode(mName) +
				(string.IsNullOrEmpty(mDescription) ? "',$," : "','" + ParserSTEP.Encode(mDescription) + "',") + 
				(string.IsNullOrEmpty(mLocation) ? "$," : "'" + ParserSTEP.Encode(mLocation) + "',") +
				(string.IsNullOrEmpty(mPurpose) ? "$," : "'" + ParserSTEP.Encode(mPurpose) + "',") +
				(string.IsNullOrEmpty(mIntendedUse) ? "$," : "'" + ParserSTEP.Encode(mIntendedUse) + "',") +
				(string.IsNullOrEmpty(mScope) ? "$," : "'" + ParserSTEP.Encode(mScope) + "',") +
				(string.IsNullOrEmpty(mRevision) ? "$," : "'" + ParserSTEP.Encode(mRevision) + "',") +
				(mDocumentOwner == null ? "$" : "#" + mDocumentOwner.StepId) + 
				(mEditors.Count == 0 ? ",$," : ",(" + string.Join(",", mEditors.Select(x=>"#" + x.StepId)) + "),") + IfcDateTime.STEPAttribute(mCreationTime) + "," + IfcDateTime.STEPAttribute(mLastRevisionTime) + "," + 
				(string.IsNullOrEmpty(mElectronicFormat) ? "$" : "'" + ParserSTEP.Encode(mElectronicFormat) + "'") + "," +
				IfcDate.STEPAttribute(mValidFrom) + "," + IfcDate.STEPAttribute(mValidUntil) +
				(mConfidentiality == IfcDocumentConfidentialityEnum.NOTDEFINED ? ",$," : ",." + mConfidentiality.ToString() + ".,") + 
				(mStatus == IfcDocumentStatusEnum.NOTDEFINED ? "$" : "." + mStatus.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mIdentification = ParserSTEP.Decode( ParserSTEP.StripString(str, ref pos, len));
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			if (release < ReleaseVersion.IFC4)
				DocumentReferences.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcDocumentReference));
			else
				mLocation = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mPurpose = ParserSTEP.StripString(str, ref pos, len);
			mIntendedUse = ParserSTEP.StripString(str, ref pos, len);
			mScope = ParserSTEP.StripString(str, ref pos, len);
			mRevision = ParserSTEP.StripString(str, ref pos, len);
			mDocumentOwner = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorSelect;
			Editors.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcActorSelect));
			if (release < ReleaseVersion.IFC4)
			{
				mSSCreationTime = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateAndTime;
				mSSLastRevisionTime = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateAndTime;
				mSSElectronicFormat = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDocumentElectronicFormat;
				mSSValidFrom = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCalendarDate;
				mSSValidUntil = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCalendarDate;
			}
			else
			{
				mCreationTime = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
				mLastRevisionTime = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
				mElectronicFormat = ParserSTEP.StripString(str, ref pos, len);
				mValidFrom = IfcDate.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
				mValidUntil = IfcDate.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			}
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcDocumentConfidentialityEnum>(s.Replace(".", ""), true, out mConfidentiality);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcDocumentStatusEnum>(s.Replace(".", ""), true, out mStatus);
		}
	}
	public partial class IfcDocumentInformationRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mRelatingDocument.StepId + ",(" + 
				string.Join(",", mRelatedDocuments.Select(x=>"#" + x.StepId)) + 
				(string.IsNullOrEmpty( mRelationshipType) ? "),$" : "),'" + ParserSTEP.Encode(mRelationshipType) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			RelatingDocument = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDocumentInformation;
			RelatedDocuments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcDocumentInformation));
			RelationshipType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDocumentReference
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (string.IsNullOrEmpty(mDescription) ? ",$," : ",'" + ParserSTEP.Encode(mDescription) + "',") +
				(mReferencedDocument == null ? "$" : "#" + mReferencedDocument.StepId)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mReferencedDocument = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDocumentInformation;
			}
		}
	}
	public partial class IfcDoor
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + formatLength(mOverallHeight) + "," + formatLength(mOverallWidth) + 
				(release < ReleaseVersion.IFC4 ? "" : 
				(mPredefinedType == IfcDoorTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") + 
				(mOperationType == IfcDoorTypeOperationEnum.NOTDEFINED ? "$," : "." + SerializeDoorTypeOperation(mOperationType, release) + ".,") + 
				(string.IsNullOrEmpty(mUserDefinedOperationType) ? "$" : "'" + ParserSTEP.Encode(mUserDefinedOperationType) + "'"));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mOverallHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mOverallWidth = ParserSTEP.StripDouble(str, ref pos, len);
			if (release > ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					PredefinedType = (IfcDoorTypeEnum)Enum.Parse(typeof(IfcDoorTypeEnum), s.Substring(1, s.Length - 2));
				s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					mOperationType = ParseDoorTypeOperation(s.Substring(1, s.Length - 2));
				try
				{
					mUserDefinedOperationType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				}
				catch (Exception) { }
			}
		}
	}
	public partial class IfcDoorLiningProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mLiningDepth) + "," + 
				ParserSTEP.DoubleOptionalToString(mLiningThickness) + "," + ParserSTEP.DoubleOptionalToString(mThresholdDepth) + "," + 
				ParserSTEP.DoubleOptionalToString(mThresholdThickness) + "," + ParserSTEP.DoubleOptionalToString(mTransomThickness) + "," + 
				ParserSTEP.DoubleOptionalToString(mTransomOffset) + "," + ParserSTEP.DoubleOptionalToString(mLiningOffset) + "," +
				ParserSTEP.DoubleOptionalToString(mThresholdOffset) + "," + ParserSTEP.DoubleOptionalToString(mCasingThickness) + "," +
				ParserSTEP.DoubleOptionalToString(mCasingDepth) + (mShapeAspectStyle == null ? ",$" : ",#" + mShapeAspectStyle) + 
				(release < ReleaseVersion.IFC4 ? "" : "," + ParserSTEP.DoubleOptionalToString(mLiningToPanelOffsetX) + "," + ParserSTEP.DoubleOptionalToString(mLiningToPanelOffsetY));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mLiningDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mLiningThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mThresholdDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mThresholdThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mTransomThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mTransomOffset = ParserSTEP.StripDouble(str, ref pos, len);
			mLiningOffset = ParserSTEP.StripDouble(str, ref pos, len);
			mThresholdOffset = ParserSTEP.StripDouble(str, ref pos, len);
			mCasingThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mCasingDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mShapeAspectStyle = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcShapeAspect;
			if (release != ReleaseVersion.IFC2x3)
			{
				mLiningToPanelOffsetX = ParserSTEP.StripDouble(str, ref pos, len);
				mLiningToPanelOffsetY = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
	public partial class IfcDoorPanelProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{ 
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mPanelDepth) + ",." + mOperationType.ToString() + ".," +
				ParserSTEP.DoubleOptionalToString(mPanelWidth) + ",." + mPanelPosition.ToString() + (mShapeAspectStyle == null ? ".,$" : ".,#" + mShapeAspectStyle.StepId); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPanelDepth = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcDoorPanelOperationEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mOperationType);
			mPanelWidth = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcDoorPanelPositionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPanelPosition);
			mShapeAspectStyle = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcShapeAspect;
		}
	}
	public partial class IfcDoorStyle 
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mOperationType.ToString() + ".,." + mConstructionType.ToString() + ".," + ParserSTEP.BoolToString(mParameterTakesPrecedence) + "," + ParserSTEP.BoolToString(mSizeable); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcDoorTypeOperationEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mOperationType);
			Enum.TryParse<IfcDoorStyleConstructionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mConstructionType);
			mParameterTakesPrecedence = ParserSTEP.StripBool(str, ref pos, len);
			mSizeable = ParserSTEP.StripBool(str,ref pos, len);
		}
	}
	public partial class IfcDoorType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) +
				(release < ReleaseVersion.IFC4 ? ",." : ",." + mPredefinedType.ToString() + ".,.") + 
				IfcDoor.SerializeDoorTypeOperation(mOperationType, release) + (release < ReleaseVersion.IFC4 ? ".,.NOTDEFINED" : "") + ".," +
				ParserSTEP.BoolToString(mParameterTakesPrecedence) + (release < ReleaseVersion.IFC4 ? "," +
				ParserSTEP.BoolToString(false) : (string.IsNullOrEmpty(mUserDefinedOperationType) ? ",$" : ",'" + ParserSTEP.Encode(mUserDefinedOperationType) + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcDoorTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mOperationType = IfcDoor.ParseDoorTypeOperation(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""));
			mParameterTakesPrecedence = ParserSTEP.StripBool(str, ref pos, len);
			mUserDefinedOperationType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDraughtingCallout
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mContents.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mContents.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcDraughtingCalloutElement)); }
	}
	public partial class IfcDraughtingCalloutRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$,#" : "'" + ParserSTEP.Encode(mDescription) + "',#") + 
				mRelatingDraughtingCallout.StepId + ",#" + mRelatedDraughtingCallout.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mRelatingDraughtingCallout = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as	IfcDraughtingCallout;
			mRelatedDraughtingCallout = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDraughtingCallout;
		}
	}
	public partial class IfcDuctFitting
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcDuctFittingTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDuctFittingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDuctFittingType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDuctFittingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDuctSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcDuctSegmentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDuctSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDuctSegmentType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDuctSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDuctSilencer  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcDuctSilencerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDuctSilencerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDuctSilencerType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDuctSilencerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	
}
