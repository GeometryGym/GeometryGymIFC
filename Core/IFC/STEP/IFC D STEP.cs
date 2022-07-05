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
	public partial class IfcDateAndTime
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mDateComponent.StepId + ",#" + mTimeComponent.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mDateComponent = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCalendarDate;
			mTimeComponent = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcLocalTime;
		}
	}
	public partial class IfcDerivedProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + ",#" + mParentProfile.StepId + (mOperator == null ? ",*" : ",#" + mOperator.StepId) +
				(string.IsNullOrEmpty(mLabel) ? ",$" : ",'" + ParserIfc.Encode(mLabel) + "'"); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mParentProfile = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
			mOperator = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianTransformationOperator2D;
			mLabel = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDerivedUnit
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mElements.Select(x=> "#" +x.StepId)) +
				"),." + mUnitType.ToString() + (mUserDefinedType == "$" ? ".,$" : ".,'" + mUserDefinedType + "'") +
				(release >= ReleaseVersion.IFC4X3 ? (string.IsNullOrEmpty(mName) ? ",$" : ",'" + ParserIfc.Encode(mName) + "'") : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Elements.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcDerivedUnitElement));
			Enum.TryParse<IfcDerivedUnitEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mUnitType);
			mUserDefinedType = ParserSTEP.StripString(str, ref pos, len);
			if(release >= ReleaseVersion.IFC4X3)
				mName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
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
			string str = base.BuildStringSTEP(release) + ",(";
			if (mAnnotatedBySymbols.Count > 0)
			{
				str += ParserSTEP.LinkToString(mAnnotatedBySymbols[0]);
				for (int icounter = 1; icounter < mAnnotatedBySymbols.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mAnnotatedBySymbols[icounter]);
			}
			return str + "}";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mAnnotatedBySymbols = ParserSTEP.StripListLink(str, ref pos, len);
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
			return "(" + ParserSTEP.DoubleToString(RoundRatio(mDirectionRatioX)) + "," +
				ParserSTEP.DoubleToString(RoundRatio(mDirectionRatioY)) + (double.IsNaN(mDirectionRatioZ) ? "" : "," + ParserSTEP.DoubleToString(RoundRatio(mDirectionRatioZ))) + ")";
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
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? (string.IsNullOrEmpty(mControlElementId) ? ",$" : ",'" + ParserIfc.Encode(mControlElementId) + "'") : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if(release < ReleaseVersion.IFC4)
				mControlElementId = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDistributionPort
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mFlowDirection.ToString() + (release < ReleaseVersion.IFC4 ? "." : ".,." + mPredefinedType + ".,." + mSystemType + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFlowDirectionEnum>(s.Replace(".", ""), true, out mFlowDirection);
			if (release != ReleaseVersion.IFC2x3)
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
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (string.IsNullOrEmpty(mLongName) ? ",$,." : ",'" + ParserIfc.Encode(mLongName) + "',.") + mPredefinedType.ToString() + "."); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mLongName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDistributionSystemEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDocumentElectronicFormat
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return (string.IsNullOrEmpty(mFileExtension) ? "$," : "'" + ParserIfc.Encode(mFileExtension) + ",',") + 
				(string.IsNullOrEmpty(mMimeContentType) ? "$," : "'" + ParserIfc.Encode(mMimeContentType) + "',") + 
				(string.IsNullOrEmpty(mMimeSubtype) ? "$" : "'" + ParserIfc.Encode(mMimeSubtype) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mFileExtension = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mMimeContentType = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mMimeSubtype = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDocumentInformation
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
			{
				return "";//to be implemented
			}
			return "'" + ParserIfc.Encode(mIdentification) + "','" + ParserIfc.Encode(mName) +
				(string.IsNullOrEmpty(mDescription) ? "',$," :  "','" + ParserIfc.Encode(mDescription) + "',") + 
				(release < ReleaseVersion.IFC4 ? (mDocumentReferences.Count == 0 ? "$," : "(#" + string.Join(",#", mDocumentReferences) + "),") : 
				(string.IsNullOrEmpty(mLocation) ? "$," : "'" + ParserIfc.Encode(mLocation) + "',")) +
				(string.IsNullOrEmpty(mPurpose) ? "$," : "'" + ParserIfc.Encode(mPurpose) + "',") + (string.IsNullOrEmpty(mIntendedUse) ? "$," : "'" + ParserIfc.Encode(mIntendedUse) + "',") + 
				(string.IsNullOrEmpty(mScope) ? "$," : "'" + ParserIfc.Encode(mScope) + "',") +  
				(string.IsNullOrEmpty(mRevision) ? "$," : "'" + ParserIfc.Encode(mRevision) + "',") + ParserSTEP.LinkToString(mDocumentOwner) + 
				(mEditors.Count == 0 ? ",$," : ",(#" + string.Join(",#", mEditors) + "),") + IfcDateTime.STEPAttribute(mCreationTime) + "," + IfcDateTime.STEPAttribute(mLastRevisionTime) + "," + 
				(release < ReleaseVersion.IFC4 ? ParserSTEP.LinkToString(mSSElectronicFormat) : (string.IsNullOrEmpty(mElectronicFormat) ? "$" : "'" + ParserIfc.Encode(mElectronicFormat) + "'")) +
				(release < ReleaseVersion.IFC4 ? ",$,$" : "," + IfcDate.STEPAttribute(mValidFrom) + "," + IfcDate.STEPAttribute(mValidUntil)) +
				(mConfidentiality == IfcDocumentConfidentialityEnum.NOTDEFINED ? ",$," : ",." + mConfidentiality.ToString() + ".,") + (mStatus == IfcDocumentStatusEnum.NOTDEFINED ? "$" : "." + mStatus.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mIdentification = ParserIfc.Decode( ParserSTEP.StripString(str, ref pos, len));
			mName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			if (release < ReleaseVersion.IFC4)
				DocumentReferences.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcDocumentReference));
			else
				mLocation = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mPurpose = ParserSTEP.StripString(str, ref pos, len);
			mIntendedUse = ParserSTEP.StripString(str, ref pos, len);
			mScope = ParserSTEP.StripString(str, ref pos, len);
			mRevision = ParserSTEP.StripString(str, ref pos, len);
			mDocumentOwner = ParserSTEP.StripLink(str, ref pos, len);
			Editors.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcActorSelect));
			mCreationTime = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mLastRevisionTime = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			if (release < ReleaseVersion.IFC4)
				mSSElectronicFormat = ParserSTEP.StripLink(str, ref pos, len);
			else
				mElectronicFormat = ParserSTEP.StripString(str, ref pos, len);
			mValidFrom = IfcDate.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			mValidUntil = IfcDate.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
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
				(string.IsNullOrEmpty( mRelationshipType) ? "),$" : "),'" + ParserIfc.Encode(mRelationshipType) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			RelatingDocument = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDocumentInformation;
			RelatedDocuments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcDocumentInformation));
			RelationshipType = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDocumentReference
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (string.IsNullOrEmpty(mDescription) ? ",$," : ",'" + ParserIfc.Encode(mDescription) + "',") +
				(mReferencedDocument == null ? ",$" : ",#" + mReferencedDocument.StepId)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				mDescription = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
				mReferencedDocument = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDocumentInformation;
			}
		}
	}
	public partial class IfcDoor
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mOverallHeight) + "," + ParserSTEP.DoubleOptionalToString(mOverallWidth)
				+ (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcDoorTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") + 
				(mOperationType == IfcDoorTypeOperationEnum.NOTDEFINED ? "$," : "." + SerializeDoorTypeOperation(mOperationType, release) + ".,") + 
				(string.IsNullOrEmpty(mUserDefinedOperationType) ? "$" : ",'" + ParserIfc.Encode(mUserDefinedOperationType) + "'"));
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
					mUserDefinedOperationType = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
				}
				catch (Exception) { }
			}
		}
	}
	public partial class IfcDoorLiningProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mLiningDepth) + "," + ParserSTEP.DoubleOptionalToString(mLiningThickness) + "," + ParserSTEP.DoubleOptionalToString(mThresholdDepth) + "," + ParserSTEP.DoubleOptionalToString(mThresholdThickness) + "," + ParserSTEP.DoubleOptionalToString(mTransomThickness) + "," + ParserSTEP.DoubleOptionalToString(mTransomOffset) + "," + ParserSTEP.DoubleOptionalToString(mLiningOffset) + "," +
				ParserSTEP.DoubleOptionalToString(mThresholdOffset) + "," + ParserSTEP.DoubleOptionalToString(mCasingThickness) + "," + ParserSTEP.DoubleOptionalToString(mCasingDepth) + "," + ParserSTEP.LinkToString(mShapeAspectStyle) + (release < ReleaseVersion.IFC4 ? "" : "," + ParserSTEP.DoubleOptionalToString(mLiningToPanelOffsetX) + "," + ParserSTEP.DoubleOptionalToString(mLiningToPanelOffsetY));
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
			mShapeAspectStyle = ParserSTEP.StripLink(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				mLiningToPanelOffsetX = ParserSTEP.StripDouble(str, ref pos, len);
				mLiningToPanelOffsetY = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
	public partial class IfcDoorPanelProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mPanelDepth) + ",." + mOperationType.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mPanelWidth) + ",." + mPanelPosition.ToString() + ".," + ParserSTEP.LinkToString(mShapeAspectStyle); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPanelDepth = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcDoorPanelOperationEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mOperationType);
			mPanelWidth = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcDoorPanelPositionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPanelPosition);
			mShapeAspectStyle = ParserSTEP.StripLink(str,ref pos,len);
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
				ParserSTEP.BoolToString(false) : (string.IsNullOrEmpty(mUserDefinedOperationType) ? ",$" : ",'" + ParserIfc.Encode(mUserDefinedOperationType) + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcDoorTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mOperationType = IfcDoor.ParseDoorTypeOperation(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""));
			mParameterTakesPrecedence = ParserSTEP.StripBool(str, ref pos, len);
			mUserDefinedOperationType = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcDraughtingCallout
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mContents.Select(x => "#" + x)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mContents = ParserSTEP.StripListLink(str, ref pos, len); }
	}
	public partial class IfcDraughtingCalloutRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (string.IsNullOrEmpty(mName) ? "$," : "'" + ParserIfc.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$," : "'" + ParserIfc.Encode(mDescription) + "',") + 
				ParserSTEP.LinkToString(mRelatingDraughtingCallout) + "," + ParserSTEP.LinkToString(mRelatedDraughtingCallout);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mRelatingDraughtingCallout = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedDraughtingCallout = ParserSTEP.StripLink(str, ref pos, len);
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
