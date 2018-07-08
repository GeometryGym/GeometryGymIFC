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
	public partial class IfcActionRequest : IfcControl
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release == ReleaseVersion.IFC2x3 ? ",'" + mIdentification + "'" : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIdentification = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcActor : IfcObject // SUPERTYPE OF(IfcOccupant)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mTheActor.Index; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mTheActor = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorSelect;
		}
	}
	public partial class IfcActorRole : BaseClassIfc, IfcResourceObjectSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + (release == ReleaseVersion.IFC2x3 && mRole == IfcRoleEnum.COMMISSIONINGENGINEER ? "COMISSIONINGENGINEER" : mRole.ToString()) + (mUserDefinedRole == "$" ? ".,$," : ".,'" + mUserDefinedRole + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				mRole = (string.Compare(s, "COMISSIONINGENGINEER", true) == 0 ? IfcRoleEnum.COMMISSIONINGENGINEER : (IfcRoleEnum)Enum.Parse(typeof(IfcRoleEnum), s.Replace(".", "")));
			mUserDefinedRole = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcActuator : IfcDistributionControlElement //IFC4  
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcActuatorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcActuatorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcActuatorType : IfcDistributionControlElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcActuatorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcAddress : BaseClassIfc, IfcObjectReferenceSelect   //ABSTRACT SUPERTYPE OF(ONEOF(IfcPostalAddress, IfcTelecomAddress));
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mPurpose == IfcAddressTypeEnum.NOTDEFINED ? ",$," : ",." + mPurpose.ToString() + ".,") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + (mUserDefinedPurpose == "$" ? "$" : "'" + mUserDefinedPurpose + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAddressTypeEnum>(s.Replace(".", ""), true, out mPurpose);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mUserDefinedPurpose = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcAdvancedBrep : IfcManifoldSolidBrep // IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP(release)); }
	}
	public partial class IfcAdvancedBrepWithVoids : IfcAdvancedBrep
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mVoids.ConvertAll(x=>x.Index)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mVoids.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcClosedShell));
		}
	}
	public partial class IfcAdvancedFace : IfcFaceSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP(release)); }
	}
	public partial class IfcAirTerminal : IfcFlowTerminal //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcAirTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcAirTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcAirTerminalBox : IfcFlowController //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcAirTerminalBoxTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcAirTerminalBoxTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcAirTerminalBoxType : IfcFlowControllerType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAirTerminalBoxTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAirTerminalType : IfcFlowTerminalType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAirTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAirToAirHeatRecovery : IfcEnergyConversionDevice //IFC4  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mPredefinedType == IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcAirToAirHeatRecoveryTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcAirToAirHeatRecoveryType : IfcEnergyConversionDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAirToAirHeatRecoveryTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAlarm : IfcDistributionControlElement //IFC4  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcAlarmTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcAlarmTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcAlarmType : IfcDistributionControlElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAlarmTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAnnotationFillArea : IfcGeometricRepresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mOuterBoundary + (mInnerBoundaries.Count == 0 ? ",$" : ",(#" + string.Join(",#", mInnerBoundaries.ConvertAll(x=>x.mIndex)) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mOuterBoundary = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			mInnerBoundaries.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=> dictionary[x] as IfcCurve));
		}
	}
	public partial class IfcAnnotationFillAreaOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mFillStyleTarget + ",." + mGlobalOrLocal.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mFillStyleTarget = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPoint;
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				Enum.TryParse<IfcGlobalOrLocalEnum>(s.Replace(".", ""), true, out mGlobalOrLocal);
		}
	}
	public partial class IfcAnnotationSurface : IfcGeometricRepresentationItem //DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mItem + "," + ParserSTEP.ObjToLinkString(mTextureCoordinates); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mItem = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcGeometricRepresentationItem;
			mTextureCoordinates = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcTextureCoordinate;
		}
	}
	public partial class IfcApplication : BaseClassIfc
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mApplicationDeveloper.Index + ",'" + mVersion + "','" +
				(string.IsNullOrEmpty(mApplicationFullName) ? mDatabase.Factory.ApplicationFullName : mApplicationFullName) + "','" +
				(string.IsNullOrEmpty(mApplicationIdentifier) ? mDatabase.Factory.ApplicationIdentifier : mApplicationIdentifier) + "'";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			ApplicationDeveloper = dictionary[ParserSTEP.StripLink(str,ref pos, len)] as IfcOrganization;
			mVersion = ParserSTEP.StripString(str, ref pos, len);
			mApplicationFullName = ParserSTEP.StripString(str, ref pos, len);
			mApplicationIdentifier = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcAppliedValue : BaseClassIfc, IfcMetricValueSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect// SUPERTYPE OF(IfcCostValue);
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',");
			result += (mAppliedValueValue != null ? mAppliedValueValue.ToString() : ParserSTEP.LinkToString(mAppliedValueIndex)) + "," + ParserSTEP.LinkToString(mUnitBasis) + ",";
			result += IfcDate.STEPAttribute(mApplicableDate) + "," + IfcDate.STEPAttribute(mFixedUntilDate);
			if (release < ReleaseVersion.IFC4)
				return result;
			string str = "$";
			if (mComponents.Count > 0)
			{
				str = "(" + ParserSTEP.LinkToString(mComponents[0]);
				for (int icounter = 1; icounter < mComponents.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mComponents[icounter]);
				str += ")";
			}
			result += (mCategory == "$" ? ",$," : ",'" + mCategory + "',") + (mCondition == "$" ? "$," : "'" + mCondition + "',");
			return result + (mArithmeticOperator == IfcArithmeticOperatorEnum.NONE ? "$," : "." + mArithmeticOperator.ToString() + ".,") + str;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			mAppliedValueValue = ParserIfc.parseValue(s);
			if (mAppliedValueValue == null)
				mAppliedValueIndex = ParserSTEP.ParseLink(s);
			mUnitBasis = ParserSTEP.StripLink(str, ref pos, len);
			mApplicableDate = IfcDate.ParseSTEP(ParserSTEP.StripString(str, ref pos, len));
			mFixedUntilDate = IfcDate.ParseSTEP(ParserSTEP.StripString(str, ref pos, len));
			if (release != ReleaseVersion.IFC2x3)
			{
				mCategory = ParserSTEP.StripString(str, ref pos, len);
				mCondition = ParserSTEP.StripString(str, ref pos, len);
				s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcArithmeticOperatorEnum>(s.Replace(".", ""), true, out mArithmeticOperator);
				mComponents = ParserSTEP.StripListLink(str, ref pos, len);
			}
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			foreach (IfcAppliedValue v in Components)
				v.mComponentFor.Add(this);
		}
	}
	public partial class IfcAppliedValueRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mComponentOfTotal) + ",(" +
				ParserSTEP.LinkToString(mComponents[0]);
			for (int icounter = 1; icounter < mComponents.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mComponents[icounter]);
			return str + "),." + mArithmeticOperator.ToString() + ".," + mName + "," + mDescription;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mComponentOfTotal = ParserSTEP.StripLink(str, ref pos, len);
			mComponents = ParserSTEP.StripListLink(str, ref pos, len);
			Enum.TryParse<IfcArithmeticOperatorEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mArithmeticOperator);
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcApproval : BaseClassIfc, IfcResourceObjectSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release)  + "," + mDescription + "," + ParserSTEP.LinkToString(mApprovalDateTime) + "," +  mApprovalStatus + "," + mApprovalLevel + "," + mApprovalQualifier + "," + mName + "," + mIdentifier;  }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mApprovalDateTime = ParserSTEP.StripLink(str, ref pos, len);
			mApprovalStatus = ParserSTEP.StripString(str, ref pos, len);
			mApprovalLevel = ParserSTEP.StripString(str, ref pos, len);
			mApprovalQualifier = ParserSTEP.StripString(str, ref pos, len);
			mName = ParserSTEP.StripString(str, ref pos, len);
			mIdentifier = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcApprovalActorRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mActor) + "," + ParserSTEP.LinkToString(mApproval) + "," + ParserSTEP.LinkToString(mRole);  }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mActor = ParserSTEP.StripLink(str, ref pos, len);
			mApproval = ParserSTEP.StripLink(str, ref pos, len);
			mRole = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcApprovalPropertyRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.LinkToString(mApprovedProperties[0]);
			for(int icounter = 1; icounter < mApprovedProperties.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mApprovedProperties[icounter]);
			str += ")," + ParserSTEP.LinkToString(mApproval);
			return str;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mApprovedProperties = ParserSTEP.StripListLink(str, ref pos, len);
			mApproval = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcApprovalRelationship : IfcResourceLevelRelationship //IFC4Change
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatedApproval) + "," + ParserSTEP.LinkToString(mRelatingApproval) + (release == ReleaseVersion.IFC2x3 ? (mDescription == "$" ? ",$,'" : ",'" + mDescription + "','") +  mName  + "'": ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRelatedApproval = ParserSTEP.StripLink(str, ref pos, len); 
			mRelatingApproval = ParserSTEP.StripLink(str, ref pos, len);
			if (release == ReleaseVersion.IFC2x3)
			{
				mDescription = ParserSTEP.StripString(str, ref pos, len);
				mName = ParserSTEP.StripString(str, ref pos, len);
			}
		}
	}
	public partial class IfcArbitraryClosedProfileDef : IfcProfileDef //SUPERTYPE OF(IfcArbitraryProfileDefWithVoids)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mOuterCurve); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mOuterCurve = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcArbitraryOpenProfileDef : IfcProfileDef //	SUPERTYPE OF(IfcCenterLineProfileDef)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mCurve); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mCurve = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcArbitraryProfileDefWithVoids : IfcArbitraryClosedProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mInnerCurves.Count == 0)
				return base.BuildStringSTEP(release);
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.LinkToString(mInnerCurves[0]);
			for (int icounter = 1; icounter < mInnerCurves.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mInnerCurves[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mInnerCurves = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcAsset : IfcGroup
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +",'" + mAssetID + "'," + ParserSTEP.LinkToString(mOriginalValue) + "," +ParserSTEP.LinkToString(mCurrentValue) + "," + 
				ParserSTEP.LinkToString(mTotalReplacementCost) + "," +ParserSTEP.LinkToString(mOwner) + "," +
				ParserSTEP.LinkToString(mUser) + "," +ParserSTEP.LinkToString(mResponsiblePerson) + "," +
				(mDatabase.Release == ReleaseVersion.IFC2x3 ? ParserSTEP.LinkToString(mIncorporationDateSS) : mIncorporationDate ) + "," +ParserSTEP.LinkToString(mDepreciatedValue);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAssetID = ParserSTEP.StripString(str, ref pos, len);
			mOriginalValue = ParserSTEP.StripLink(str, ref pos, len);
			mCurrentValue = ParserSTEP.StripLink(str, ref pos, len);
			mTotalReplacementCost = ParserSTEP.StripLink(str, ref pos, len);
			mOwner = ParserSTEP.StripLink(str, ref pos, len);
			mUser = ParserSTEP.StripLink(str, ref pos, len);
			mResponsiblePerson = ParserSTEP.StripLink(str, ref pos, len);
			if (release == ReleaseVersion.IFC2x3)
				mIncorporationDateSS = ParserSTEP.StripLink(str, ref pos, len);
			else
				mIncorporationDate = ParserSTEP.StripString(str, ref pos, len);
			mDepreciatedValue = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcAsymmetricIShapeProfileDef : IfcParameterizedProfileDef // Ifc2x3 IfcIShapeProfileDef 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mDatabase.Release == ReleaseVersion.IFC2x3)
			{
				return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mBottomFlangeWidth) + "," + ParserSTEP.DoubleToString(mOverallDepth) + "," + 
					ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mBottomFlangeThickness) + "," + ParserSTEP.DoubleOptionalToString(mBottomFlangeFilletRadius) + "," +
					ParserSTEP.DoubleToString(mTopFlangeWidth) + "," + ParserSTEP.DoubleOptionalToString(mTopFlangeThickness) + "," +
					ParserSTEP.DoubleOptionalToString(mTopFlangeFilletRadius) +  "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY);
			}
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mBottomFlangeWidth) + "," + ParserSTEP.DoubleToString(mOverallDepth) + "," +
					ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mBottomFlangeThickness) + "," + ParserSTEP.DoubleOptionalToString(mBottomFlangeFilletRadius) + "," +
				ParserSTEP.DoubleToString(mTopFlangeWidth) + "," + ParserSTEP.DoubleOptionalToString(mTopFlangeThickness) + "," +
				ParserSTEP.DoubleOptionalToString(mTopFlangeFilletRadius) + "," + ParserSTEP.DoubleOptionalToString(mBottomFlangeEdgeRadius) + "," +
				ParserSTEP.DoubleOptionalToString(mBottomFlangeSlope) + "," + ParserSTEP.DoubleOptionalToString(mTopFlangeEdgeRadius) + "," +
				ParserSTEP.DoubleOptionalToString(mTopFlangeSlope);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release == ReleaseVersion.IFC2x3)
			{
				mBottomFlangeWidth = ParserSTEP.StripDouble(str, ref pos, len);
				mOverallDepth = ParserSTEP.StripDouble(str, ref pos, len);
				mWebThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeWidth = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mCentreOfGravityInY = ParserSTEP.StripDouble(str, ref pos, len);
			}
			else
			{
				mBottomFlangeWidth = ParserSTEP.StripDouble(str, ref pos, len);
				mOverallDepth = ParserSTEP.StripDouble(str, ref pos, len);
				mWebThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeWidth = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeEdgeRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeSlope = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeEdgeRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeSlope = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
	public partial class IfcAudioVisualAppliance : IfcFlowTerminal //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release == ReleaseVersion.IFC2x3 ? "" : ( mPredefinedType == IfcAudioVisualApplianceTypeEnum.NOTDEFINED  ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcAudioVisualApplianceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcAudioVisualApplianceType : IfcFlowTerminalType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAudioVisualApplianceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAxis1Placement : IfcPlacement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mAxis); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxis = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcAxis2Placement2D : IfcPlacement, IfcAxis2Placement
	{ 
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(mRefDirection); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRefDirection = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
		}
	}
	public partial class IfcAxis2Placement3D : IfcPlacement, IfcAxis2Placement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(mAxis) + "," + ParserSTEP.ObjToLinkString(mRefDirection); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxis = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
			mRefDirection = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
		}
	}
}
