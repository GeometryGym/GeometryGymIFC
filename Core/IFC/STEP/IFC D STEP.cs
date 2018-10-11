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
	public partial class IfcDamper : IfcFlowController //IFC4
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
	public partial class IfcDamperType : IfcFlowControllerType
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
	public partial class IfcDateAndTime : BaseClassIfc, IfcDateTimeSelect // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mDateComponent) + "," + ParserSTEP.LinkToString(mTimeComponent); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mDateComponent = ParserSTEP.StripLink(str, ref pos, len);
			mTimeComponent = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	//ENTITY IfcDefinedSymbol  // DEPRECEATED IFC4
	public partial class IfcDerivedProfileDef : IfcProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mContainerProfile) + "," + ParserSTEP.LinkToString(mOperator) + (mLabel == "$" ? ",$" : ",'" + mLabel + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mContainerProfile = ParserSTEP.StripLink(str, ref pos, len);
			mOperator = ParserSTEP.StripLink(str, ref pos, len);
			mLabel = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcDerivedUnit : BaseClassIfc, IfcUnit
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.LinkToString(mElements[0]);
			for (int icounter = 1; icounter < mElements.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mElements[icounter]);
			return str + "),." + mUnitType.ToString() + (mUserDefinedType == "$" ? ".,$" : ".,'" + mUserDefinedType + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mElements = ParserSTEP.StripListLink(str, ref pos, len);
			Enum.TryParse<IfcDerivedUnitEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mUnitType);
			mUserDefinedType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcDerivedUnitElement : BaseClassIfc
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mUnit) + "," + ParserSTEP.IntToString(mExponent); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mUnit = ParserSTEP.StripLink(str, ref pos, len);
			mExponent = ParserSTEP.StripInt(str, ref pos, len);
		}
	}
	public partial class IfcDimensionalExponents : BaseClassIfc
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + mLengthExponent + "," + mMassExponent + "," + mTimeExponent + "," + mElectricCurrentExponent + "," + mThermodynamicTemperatureExponent + "," + mAmountOfSubstanceExponent + "," + mLuminousIntensityExponent; }
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
	public partial class IfcDimensionCurve : IfcAnnotationCurveOccurrence // DEPRECEATED IFC4
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
	public partial class IfcDimensionCurveTerminator : IfcTerminatorSymbol // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mRole.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcDimensionExtentUsage>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mRole);
		}
	}
	public partial class IfcDirection : IfcGeometricRepresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{ 
			return base.BuildStringSTEP(release) + ",(" + ParserSTEP.DoubleToString(RoundRatio(mDirectionRatioX)) + "," +
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
	public partial class IfcDiscreteAccessory : IfcElementComponent
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
	public partial class IfcDiscreteAccessoryType : IfcElementComponentType
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
	public partial class IfcDistanceExpression : IfcGeometricRepresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(DistanceAlong) + "," + ParserSTEP.DoubleOptionalToString(OffsetLateral) + "," +
				ParserSTEP.DoubleOptionalToString(OffsetVertical) + "," + ParserSTEP.DoubleOptionalToString(mOffsetLongitudinal) + "," + ParserSTEP.BoolToString(AlongHorizontal);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			DistanceAlong = ParserSTEP.StripDouble(str, ref pos, len);
			OffsetLateral = ParserSTEP.StripDouble(str, ref pos, len);
			OffsetVertical = ParserSTEP.StripDouble(str, ref pos, len);
			OffsetLongitudinal = ParserSTEP.StripDouble(str, ref pos, len);
			AlongHorizontal = ParserSTEP.StripBool(str, ref pos, len); 
		}
	}
	public partial class IfcDistributionChamberElement : IfcDistributionFlowElement
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
	public partial class IfcDistributionChamberElementType : IfcDistributionFlowElementType
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
	public partial class IfcDistributionControlElement : IfcDistributionElement // SUPERTYPE OF(ONEOF(IfcActuator, IfcAlarm, IfcController,
	{ 
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mControlElementId == "$" ? ",$" : ",'" + mControlElementId + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mControlElementId = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcDistributionPort : IfcPort
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
	public partial class IfcDistributionSystem : IfcSystem //SUPERTYPE OF(IfcDistributionCircuit) IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mLongName == "$" ? ",$,." : ",'" + mLongName + "',.") + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mLongName = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcDistributionSystemEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcDocumentElectronicFormat : BaseClassIfc // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mFileExtension == "$" ? ",$," : ",'" + mFileExtension + ",',") + (mMimeContentType == "$" ? "$," : "'" + mMimeContentType + "',") + (mMimeSubtype == "$" ? "$" : "'" + mMimeSubtype + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mFileExtension = ParserSTEP.StripString(str, ref pos, len);
			mMimeContentType = ParserSTEP.StripString(str, ref pos, len);
			mMimeSubtype = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcDocumentInformation : IfcExternalInformation, IfcDocumentSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
			{
				return "";//to be implemented
			}
			return base.BuildStringSTEP(release) + ",'" + ParserIfc.Encode(mIdentification) + "','" + ParserIfc.Encode(mName) +
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
				mDocumentReferences = ParserSTEP.StripListLink(str, ref pos, len);
			else
				mLocation = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mPurpose = ParserSTEP.StripString(str, ref pos, len);
			mIntendedUse = ParserSTEP.StripString(str, ref pos, len);
			mScope = ParserSTEP.StripString(str, ref pos, len);
			mRevision = ParserSTEP.StripString(str, ref pos, len);
			mDocumentOwner = ParserSTEP.StripLink(str, ref pos, len);
			mEditors = ParserSTEP.StripListLink(str, ref pos, len);
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
	public partial class IfcDocumentInformationRelationship : BaseClassIfc
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingDocument) + ",(" + ParserSTEP.LinkToString(mRelatedDocuments[0]);
			for (int icounter = 0; icounter < mRelatedDocuments.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedDocuments[icounter]);
			return str + (mRelationshipType == "$" ? "),$" : "),'" + mRelationshipType + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mRelatingDocument = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedDocuments = ParserSTEP.StripListLink(str, ref pos, len);
			mRelationshipType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcDocumentReference : IfcExternalReference, IfcDocumentSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mDescription == "$" ? ",$," : ",'" + mDescription + "',") + ParserSTEP.LinkToString(mReferencedDocument)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				mDescription = ParserSTEP.StripString(str, ref pos, len);
				mReferencedDocument = ParserSTEP.StripLink(str, ref pos, len);
			}
		}
	}
	public partial class IfcDoor : IfcBuildingElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mOverallHeight) + "," + ParserSTEP.DoubleOptionalToString(mOverallWidth)
				+ (release < ReleaseVersion.IFC4 ? "" : ",." + mPredefinedType.ToString() + ".,." + mOperationType.ToString() + (mUserDefinedOperationType == "$" ? ".,$" : ".,'" + mUserDefinedOperationType + "'"));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mOverallHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mOverallWidth = ParserSTEP.StripDouble(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					PredefinedType = (IfcDoorTypeEnum)Enum.Parse(typeof(IfcDoorTypeEnum), s.Substring(1, s.Length - 2));
				s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					mOperationType = (IfcDoorTypeOperationEnum)Enum.Parse(typeof(IfcDoorTypeOperationEnum), s.Substring(1, s.Length - 2));
				try
				{
					mUserDefinedOperationType = ParserSTEP.StripString(str, ref pos, len);
				}
				catch (Exception) { }
			}
		}
	}
	public partial class IfcDoorLiningProperties : IfcPreDefinedPropertySet // IFC2x3 IfcPropertySetDefinition
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
	public partial class IfcDoorPanelProperties : IfcPreDefinedPropertySet //IFC2x3 IfcPropertySetDefinition
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
	public partial class IfcDoorStyle : IfcTypeProduct //IFC2x3 
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
	public partial class IfcDoorType : IfcBuildingElementType //IFC2x3 IfcDoorStyle
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? ",." : ",." + mPredefinedType.ToString() + ".,.") + mOperationType.ToString() + (release < ReleaseVersion.IFC4 ? ".,.NOTDEFINED" : "") + ".," + ParserSTEP.BoolToString(mParameterTakesPrecedence) + (release < ReleaseVersion.IFC4 ? "," + ParserSTEP.BoolToString(false) : (mUserDefinedOperationType == "$" ? ",$" : ",'" + mUserDefinedOperationType + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcDoorTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			Enum.TryParse<IfcDoorTypeOperationEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mOperationType);
			mParameterTakesPrecedence = ParserSTEP.StripBool(str, ref pos, len);
			mUserDefinedOperationType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcDraughtingCallout : IfcGeometricRepresentationItem // DEPRECEATED IFC4 SUPERTYPE OF (ONEOF (IfcDimensionCurveDirectedCallout ,IfcStructuredDimensionCallout))
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = ",(" + ParserSTEP.LinkToString(mContents[0]);
			for (int icounter = 1; icounter < mContents.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mContents[icounter]);
			return base.BuildStringSTEP(release) + str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mContents = ParserSTEP.StripListLink(str, ref pos, len); }
	}
	public partial class IfcDraughtingCalloutRelationship : BaseClassIfc // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mName == "$" ? ",$," : ",'" + mName + "',") + 
				(mDescription == "$" ? "$," : "'" + mDescription +"',") + 
				ParserSTEP.LinkToString(mRelatingDraughtingCallout) + "," + ParserSTEP.LinkToString(mRelatedDraughtingCallout);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mRelatingDraughtingCallout = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedDraughtingCallout = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcDuctFitting : IfcFlowFitting //IFC4
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
	public partial class IfcDuctFittingType : IfcFlowFittingType
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
	public partial class IfcDuctSegment : IfcFlowSegment //IFC4
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
	public partial class IfcDuctSegmentType : IfcFlowSegmentType
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
	public partial class IfcDuctSilencer : IfcFlowTreatmentDevice //IFC4  
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
	public partial class IfcDuctSilencerType : IfcFlowTreatmentDeviceType
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
