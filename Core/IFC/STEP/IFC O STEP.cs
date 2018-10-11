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
	public abstract partial class IfcObject : IfcObjectDefinition //ABSTRACT SUPERTYPE OF (ONEOF (IfcActor ,IfcControl ,IfcGroup ,IfcProcess ,IfcProduct ,IfcProject ,IfcResource))
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mObjectType == "$" ? ",$" : ",'" + mObjectType + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mObjectType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcObjective : IfcConstraint
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release);
			if (mBenchmarkValues.Count > 0)
			{
				str += ",(" + ParserSTEP.LinkToString(mBenchmarkValues[0]);
				for (int icounter = 1; icounter < mBenchmarkValues.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mBenchmarkValues[icounter]);
				str += "),";
			}
			else
				str += ",$,";
			return str + (mLogicalAggregator != IfcLogicalOperatorEnum.NONE ? "." + mLogicalAggregator.ToString() + ".,." : "$,.") + mObjectiveQualifier + (mUserDefinedQualifier == "$" ? ".,$" : ".,'" + mUserDefinedQualifier + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mBenchmarkValues = ParserSTEP.StripListLink(str, ref pos, len);
			string s = ParserSTEP.StripString(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcLogicalOperatorEnum>(s.Replace(".", ""), true, out mLogicalAggregator);
			Enum.TryParse<IfcObjectiveEnum>(ParserSTEP.StripField( str, ref pos, len).Replace(".", ""), true, out mObjectiveQualifier);
			mUserDefinedQualifier = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcOccupant : IfcActor
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 || mPredefinedType != IfcOccupantTypeEnum.NOTDEFINED ? ",." + mPredefinedType.ToString() + "." : ",$"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcOccupantTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcOffsetCurve : IfcCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mBasisCurve; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mBasisCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
		}
	}
	//ENTITY IfcOffsetCurve2D
	//ENTITY IfcOffsetCurve3D
	public partial class IfcOffsetCurveByDistances : IfcOffsetCurve
	{
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			OffsetValues.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcDistanceExpression));
			mTag = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mOffsetValues.ConvertAll(x=>x.mIndex)) +
				(string.IsNullOrEmpty(mTag) ? "$" : "'" + ParserIfc.Encode(mTag) + "'");
		}
	}
	public partial class IfcOneDirectionRepeatFactor : IfcGeometricRepresentationItem // DEPRECEATED IFC4 SUPERTYPE OF	(IfcTwoDirectionRepeatFactor)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRepeatFactor); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mRepeatFactor = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcOpeningElement : IfcFeatureElementSubtraction //SUPERTYPE OF(IfcOpeningStandardCase)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcOpeningElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcOpeningElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	//ENTITY IfcOpticalMaterialProperties // DEPRECEATED IFC4
	//ENTITY IfcOrderAction // DEPRECEATED IFC4
	public partial class IfcOrganization : BaseClassIfc, IfcActorSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string name = mName;
			if(string.IsNullOrEmpty(name))
				name = mDatabase.Factory.ApplicationDeveloper;
			return base.BuildStringSTEP(release) + (mIdentification == "$" ? ",$,'" : ",'" + mIdentification + "','") + name 
				+ (mDescription == "$" ? "',$," : "','" + mDescription + "',") 
				+ (mRoles.Count == 0 ? "$," : "(#" + string.Join(",#", Roles.Select(x=>x.Index)) + "),")
				+ (mAddresses.Count == 0 ? "$" : "(#" + string.Join(",#", Addresses.Select(x=>x.Index)) + ")");

		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mIdentification = ParserSTEP.StripString(str, ref pos, len);
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			Roles.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcActorRole));
			mAddresses.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcAddress));
		}
	}
	//ENTITY IfcOrganizationRelationship; //optional name
	public partial class IfcOrientedEdge : IfcEdge
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mEdgeElement) + "," + ParserSTEP.BoolToString(mOrientation); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mEdgeElement = ParserSTEP.StripLink(str, ref pos, len);
			mOrientation = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcOutlet : IfcFlowTerminal //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcOutletTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcOutletTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcOutletType : IfcFlowTerminalType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcOutletTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcOwnerHistory : BaseClassIfc
	{ 
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",#" + mOwningUser.Index + ",#" + mOwningApplication.Index + ",";
			if (mState == IfcStateEnum.NOTDEFINED)
				str += "$";
			else
				str += "." + mState.ToString() + ".";
			return str + ",." + (release < ReleaseVersion.IFC4 && mChangeAction == IfcChangeActionEnum.NOTDEFINED ? IfcChangeActionEnum.NOCHANGE : mChangeAction).ToString() + ".," + ParserSTEP.IntOptionalToString(mLastModifiedDate) + ","
				+ ParserSTEP.ObjToLinkString(mLastModifyingUser) + "," + ParserSTEP.ObjToLinkString(mLastModifyingApplication) + "," + ParserSTEP.IntToString(mCreationDate);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			OwningUser = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPersonAndOrganization;
			OwningApplication = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcApplication;
			string s = ParserSTEP.StripField(str, ref pos, len);
			if(!Enum.TryParse<IfcStateEnum>(s.Replace(".", ""), true, out mState))
				mState = IfcStateEnum.NOTDEFINED;
			s = ParserSTEP.StripField(str, ref pos, len).Replace(".", "");
			if (s.EndsWith("ADDED"))
				mChangeAction = IfcChangeActionEnum.ADDED;
			if (s.EndsWith("DELETED"))
				mChangeAction = IfcChangeActionEnum.DELETED;
			else
				Enum.TryParse<IfcChangeActionEnum>(s.Replace(".", ""), true, out mChangeAction);
			mLastModifiedDate = ParserSTEP.StripInt(str, ref pos, len);
			LastModifyingUser = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPersonAndOrganization;
			LastModifyingApplication = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcApplication;
			mCreationDate = ParserSTEP.StripInt(str, ref pos, len);
		}
	}
}
