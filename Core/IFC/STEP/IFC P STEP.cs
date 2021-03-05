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
	public abstract partial class IfcParameterizedProfileDef : IfcProfileDef //ABSTRACT SUPERTYPE OF (ONEOF (IfcAsymmetricIShapeProfileDef , IfcCShapeProfileDef ,IfcCircleProfileDef ,IfcCraneRailAShapeProfileDef ,IfcCraneRailFShapeProfileDef ,
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(mPosition); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPosition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement2D;
		}
	}
	public partial class IfcPath : IfcTopologicalRepresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(";
			if (mEdgeList.Count > 0)
				str += ParserSTEP.LinkToString(mEdgeList[0]);
			for (int icounter = 1; icounter < mEdgeList.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mEdgeList[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mEdgeList = ParserSTEP.StripListLink(str, ref pos, len); }
	}
	public partial class IfcPcurve : IfcCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mBasisSurface.StepId + ",#" + mReferenceCurve.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			BasisSurface = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSurface;
			ReferenceCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
		}
	}
	public partial class IfcPavement : IfcBuiltElement
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mFlexible == IfcLogicalEnum.UNKNOWN ? ",$" : (mFlexible == IfcLogicalEnum.TRUE ? ",.T." : ",.F."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field[0] == '.')
				mFlexible = field[1] == 'T' ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE;
		}
	}
	public partial class IfcPavementType : IfcBuiltElementType
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mFlexible ? ",.T." : ",.F."); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Flexible = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcPerformanceHistory : IfcControl
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string predefined = (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcPerformanceHistoryTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
			return base.BuildStringSTEP(release) + ",'" + ParserIfc.Encode(mLifeCyclePhase) + "'" + predefined;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mLifeCyclePhase = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			if(release > ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (!string.IsNullOrEmpty(s) && s[0] == '.')
					Enum.TryParse<IfcPerformanceHistoryTypeEnum>(s.Replace(".", ""), out mPredefinedType);
			}
		}
	}
	public partial class IfcPermeableCoveringProperties : IfcPreDefinedPropertySet
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
			",." + mOperationType.ToString() + "." +
			",." + mPanelPosition.ToString() + "." + "," +
			ParserSTEP.DoubleOptionalToString(mFrameDepth) + "," +
			ParserSTEP.DoubleOptionalToString(mFrameThickness) +
			(mShapeAspectStyle == null ? ",$" : ",#" + mShapeAspectStyle.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPermeableCoveringOperationEnum>(s.Replace(".", ""), true, out mOperationType);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcWindowPanelPositionEnum>(s.Replace(".", ""), true, out mPanelPosition);
			FrameDepth = ParserSTEP.StripDouble(str, ref pos, len);
			FrameThickness = ParserSTEP.StripDouble(str, ref pos, len);
			ShapeAspectStyle = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcShapeAspect;
		}
	}
	public partial class IfcPermit : IfcControl
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mPermitID + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPermitID = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcPerson : BaseClassIfc, IfcActorSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',");
			if (mFamilyName == "$" && mGivenName == "$")
				str += (mIdentification == "$" ? "'Unknown',$," : "'" + mIdentification + "',$,");
			else
				str += (mFamilyName == "$" ? "$," : "'" + mFamilyName + "',") + (mGivenName == "$" ? "$," : "'" + mGivenName + "',");
			str += (mMiddleNames.Count == 0 ? "$," : "(" + string.Join(",", mMiddleNames.Select(x=> "'" + x + "'")) + "),");
			str += (mPrefixTitles.Count == 0 ? "$," : "(" + string.Join(",", mPrefixTitles.Select(x=>"'" + x + "'")) + "),");
			str += (mSuffixTitles.Count == 0 ? "$," : "(" + string.Join(",", mSuffixTitles.Select(x=>"'" + x + "'")) + "),");
			str += (Roles.Count == 0 ? "$," : "(#" + string.Join(",#", Roles.ConvertAll(x=>x.Index.ToString())) + "),");
			return str + (Addresses.Count == 0 ? "$" : "(#" + string.Join(",#", Addresses.ConvertAll(x=>x.Index.ToString())) + ")");
			
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mIdentification = ParserSTEP.StripString(str, ref pos, len);
			mFamilyName = ParserSTEP.StripString(str, ref pos, len);
			mGivenName = ParserSTEP.StripString(str, ref pos, len);
			mMiddleNames = ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).ConvertAll(x => x.Replace("'", ""));
			mPrefixTitles = ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).ConvertAll(x => x.Replace("'", ""));
			mSuffixTitles = ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).ConvertAll(x => x.Replace("'", ""));
			mRoles = new LIST<IfcActorRole>(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=> dictionary[x] as IfcActorRole));
			mAddresses = new LIST<IfcAddress>(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=> dictionary[x] as IfcAddress));
		}
	}
	public partial class IfcPersonAndOrganization : BaseClassIfc, IfcActorSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mThePerson.Index + ",#" + mTheOrganization.Index + (mRoles.Count == 0 ? ",$" : ",(#" + string.Join(",#", mRoles.ConvertAll(x => x.Index.ToString())) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mThePerson = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPerson;
			mTheOrganization = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcOrganization;
			mRoles = new LIST<IfcActorRole>(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcActorRole));
		}
	}
	public partial class IfcPhysicalComplexQuantity : IfcPhysicalQuantity
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
			",(#" + string.Join(",#", mHasQuantities.ConvertAll(x => x.StepId.ToString())) + ")" +
			",'" + ParserIfc.Encode(mDiscrimination) + "'" +
			(string.IsNullOrEmpty(mQuality) ? ",$" : ",'" + ParserIfc.Encode(mQuality) + "'") +
			(string.IsNullOrEmpty(mUsage) ? ",$" : ",'" + ParserIfc.Encode(mUsage) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			HasQuantities.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcPhysicalQuantity));
			Discrimination = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			Quality = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			Usage = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public abstract partial class IfcPhysicalQuantity : BaseClassIfc, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF(ONEOF(IfcPhysicalComplexQuantity, IfcPhysicalSimpleQuantity));
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mName + (mDescription == "$" ? "',$" : "','" + mDescription + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public abstract partial class IfcPhysicalSimpleQuantity : IfcPhysicalQuantity //ABSTRACT SUPERTYPE OF (ONEOF (IfcQuantityArea ,IfcQuantityCount ,IfcQuantityLength ,IfcQuantityTime ,IfcQuantityVolume ,IfcQuantityWeight))
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mUnit); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mUnit = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPile : IfcDeepFoundation
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release != ReleaseVersion.IFC2x3 && mPredefinedType == IfcPileTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") +
				(mConstructionType == IfcPileConstructionEnum.NOTDEFINED ? "$" : "." + mConstructionType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				mPredefinedType = (IfcPileTypeEnum)Enum.Parse(typeof(IfcPileTypeEnum), s.Replace(".", ""));
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				mConstructionType = (IfcPileConstructionEnum)Enum.Parse(typeof(IfcPileConstructionEnum), s.Replace(".", ""));
		}
	}
	public partial class IfcPileType : IfcDeepFoundationType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPileTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcPipeFitting : IfcFlowFitting //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcPipeFittingTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPipeFittingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcPipeFittingType : IfcFlowFittingType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPipeFittingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcPipeSegment : IfcFlowSegment //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcPipeSegmentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPipeSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcPipeSegmentType : IfcFlowSegmentType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPipeSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcPixelTexture : IfcSurfaceTexture
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + mWidth + "," + mHeight + "," + mColourComponents + ",(" + string.Join(",", mPixel.ConvertAll(x => "'" + x + "'")) + ")"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mWidth = ParserSTEP.StripInt(str, ref pos, len);
			mHeight = ParserSTEP.StripInt(str, ref pos, len);
			mColourComponents = ParserSTEP.StripInt(str, ref pos, len);
			mPixel = ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len));
		}
	}
	public abstract partial class IfcPlacement : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcAxis1Placement ,IfcAxis2Placement2D ,IfcAxis2Placement3D))*/
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mLocation.mIndex; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mLocation = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPoint; }
	}
	public partial class IfcPlanarBox : IfcPlanarExtent
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mPlacement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPlacement = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPlanarExtent : IfcGeometricRepresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mSizeInX) + "," + ParserSTEP.DoubleToString(mSizeInY); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			SizeInX = ParserSTEP.StripDouble(str, ref pos, len);
			SizeInY = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcPlate : IfcBuiltElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? base.BuildStringSTEP(release) : base.BuildStringSTEP(release) + (mPredefinedType == IfcPlateTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcPlateTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcPlateType : IfcBuiltElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPlateTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcPointByDistanceExpression : IfcPoint
	{
		public override string StepClassName { get { return (mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4X3_RC2 ? "IFCDISTANCEEXPRESSION" : base.StepClassName); } }
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + (release < ReleaseVersion.IFC4X3_RC3 ? formatLength((mDistanceAlong as IfcNonNegativeLengthMeasure).Measure) :  mDistanceAlong.ToString()) +
				(double.IsNaN(mOffsetLateral) ? ",$" : "," + formatLength(OffsetLateral)) +
				(double.IsNaN(mOffsetVertical) ? ",$" : "," + formatLength(OffsetVertical)) +
				(double.IsNaN(mOffsetLongitudinal) ? ",$" : "," + formatLength(mOffsetLongitudinal)) +
				(release < ReleaseVersion.IFC4X3_RC2 ? "," + ParserSTEP.BoolToString(mAlongHorizontal) : ",#" + mBasisCurve.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			if (release < ReleaseVersion.IFC4X3_RC2)
				DistanceAlong = new IfcNonNegativeLengthMeasure(ParserSTEP.StripDouble(str, ref pos, len));
			else
				DistanceAlong = ParserIfc.parseMeasureValue(ParserSTEP.StripField(str, ref pos, len)) as IfcCurveMeasureSelect;
			OffsetLateral = ParserSTEP.StripDouble(str, ref pos, len);
			OffsetVertical = ParserSTEP.StripDouble(str, ref pos, len);
			OffsetLongitudinal = ParserSTEP.StripDouble(str, ref pos, len);
			if(release < ReleaseVersion.IFC4X3_RC2)
				mAlongHorizontal = ParserSTEP.StripBool(str, ref pos, len);
			else
				mBasisCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
		}
	}
	public partial class IfcPointOnCurve : IfcPoint
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mBasisCurve) + "," + ParserSTEP.DoubleToString(mPointParameter); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mBasisCurve = ParserSTEP.StripLink(str, ref pos, len);
			mPointParameter = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcPointOnSurface : IfcPoint
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.DoubleToString(mPointParameterU) + "," + ParserSTEP.DoubleToString(mPointParameterV); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mBasisSurface = ParserSTEP.StripLink(str, ref pos, len);
			mPointParameterU = ParserSTEP.StripDouble(str, ref pos, len);
			mPointParameterV = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcPolygonalBoundedHalfSpace : IfcHalfSpaceSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mPosition) + "," + ParserSTEP.LinkToString(mPolygonalBoundary); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPosition = ParserSTEP.StripLink(str, ref pos, len);
			mPolygonalBoundary = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPolygonalFaceSet : IfcTessellatedFaceSet //IFC4A2
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mClosed == IfcLogicalEnum.UNKNOWN ? ",$" : "," + ParserIfc.LogicalToString(mClosed)) + 
				",(#" + string.Join(",#", mFaces.Select(x => x.StepId)) + (mPnIndex.Count == 0 ? "),$" : "),(" + string.Join(",", mPnIndex) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mClosed = ParserIfc.StripLogical(str, ref pos, len);
			mFaces.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=> dictionary[x] as IfcIndexedPolygonalFace));
			mPnIndex.AddRange(ParserSTEP.StripListInt(str, ref pos, len));
		}
	}
	public partial class IfcPolyline : IfcBoundedCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", Points.ConvertAll(x => x.Index.ToString())) + ")"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { Points.AddRange(ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)).ConvertAll(x=>dictionary[x] as IfcCartesianPoint)); }
	}
	public partial class IfcPolyLoop : IfcLoop
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mPolygon.ConvertAll(x=>x.mIndex)) + ")" ; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mPolygon.AddRange(ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)).ConvertAll(x => dictionary[x] as IfcCartesianPoint)); }
	}
	public partial class IfcPolynomialCurve : IfcCurve
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
			",#" + mPosition.StepId +
			(mCoefficientsX.Count ==0 ? ",$" :	",(" + string.Join(",", mCoefficientsX.ConvertAll(x => ParserSTEP.DoubleExponentialString(x))) + ")") +
			(mCoefficientsY.Count == 0 ? ",$" : ",(" + string.Join(",", mCoefficientsY.ConvertAll(x => ParserSTEP.DoubleExponentialString(x))) + ")") +
			(mCoefficientsZ.Count ==0 ? ",$" : ",(" + string.Join(",", mCoefficientsY.ConvertAll(x => ParserSTEP.DoubleExponentialString(x))) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Position = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPlacement;
			CoefficientsX.AddRange(ParserSTEP.StripListDouble(str, ref pos, len));
			CoefficientsY.AddRange(ParserSTEP.StripListDouble(str, ref pos, len));
			CoefficientsZ.AddRange(ParserSTEP.StripListDouble(str, ref pos, len));
		}
	}
	public partial class IfcPostalAddress : IfcAddress
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mInternalLocation == "$" ? ",$" : ",'" + mInternalLocation + "'") +
				(mAddressLines.Count == 0 ? ",$" : ",('" + string.Join("','", mAddressLines.Select(x => ParserIfc.Encode(x))) + "')")
				+ (mPostalBox == "$" ? ",$" : ",'" + mPostalBox + "'") + (mTown == "$" ? ",$" : ",'" + mTown + "'") + (mRegion == "$" ? ",$" : ",'" + mRegion + "'") + (mPostalCode == "$" ? ",$" : ",'" + mPostalCode + "'") + (mCountry == "$" ? ",$" : ",'" + mCountry + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mInternalLocation = ParserSTEP.StripString(str, ref pos, len);
			if (string.IsNullOrEmpty(mInternalLocation))
				mInternalLocation = "$";
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
				{
					string field = lst[icounter];
					if (field.Length > 2)
						mAddressLines.Add(ParserIfc.Decode(field.Substring(1, field.Length - 2)));
				}
			}
			mPostalBox = ParserSTEP.StripString(str, ref pos, len);
			if (string.IsNullOrEmpty(mPostalBox))
				mPostalBox = "$";
			mTown = ParserSTEP.StripString(str, ref pos, len);
			if (string.IsNullOrEmpty(mTown))
				mTown = "$";
			mRegion = ParserSTEP.StripString(str, ref pos, len);
			if (string.IsNullOrEmpty(mRegion))
				mRegion = "$";
			mPostalCode = ParserSTEP.StripString(str, ref pos, len);
			if (string.IsNullOrEmpty(mPostalCode))
				mPostalCode = "$";
			mCountry = ParserSTEP.StripString(str, ref pos, len);
			if (string.IsNullOrEmpty(mCountry))
				mCountry = "$";
		}
	}
	public abstract partial class IfcPreDefinedItem : IfcPresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mName + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mName = ParserSTEP.StripString(str, ref pos, len); }
	}
	public partial class IfcPresentationLayerAssignment : BaseClassIfc //SUPERTYPE OF	(IfcPresentationLayerWithStyle);
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mAssignedItems.Count < 1)
				return "";
			return base.BuildStringSTEP(release) + ",'" + mName + (mDescription == "$" ? "',$,(#" : "','" + mDescription + "',(#") + 
				string.Join(",#", mAssignedItems.ConvertAll(x=>x.Index)) + (mIdentifier == "$" ? "),$" : "),'" + mIdentifier + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			AssignedItems.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcLayeredItem));
			mIdentifier = ParserSTEP.StripString(str, ref pos, len);
		}
		
	}
	public partial class IfcPresentationLayerWithStyle : IfcPresentationLayerAssignment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mAssignedItems.Count < 1 || mLayerStyles.Count == 0)
				return "";
			string str = base.BuildStringSTEP(release) + "," + ParserIfc.LogicalToString(mLayerOn) + "," +
				ParserIfc.LogicalToString(mLayerFrozen) + "," + ParserIfc.LogicalToString(mLayerBlocked);
			str += ",(" + ParserSTEP.LinkToString(mLayerStyles[0]);
			for (int icounter = 1; icounter < mLayerStyles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mLayerStyles[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mLayerOn = ParserIfc.StripLogical(str, ref pos, len);
			mLayerFrozen = ParserIfc.StripLogical(str, ref pos, len);
			mLayerBlocked = ParserIfc.StripLogical(str, ref pos, len);
			mLayerStyles = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public abstract partial class IfcPresentationStyle : BaseClassIfc, IfcStyleAssignmentSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcCurveStyle,IfcFillAreaStyle,IfcSurfaceStyle,IfcSymbolStyle,IfcTextStyle));
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mName == "$" ? ",$" : ",'" + mName + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mName = ParserSTEP.StripString(str, ref pos, len); }
	}
	public partial class IfcPresentationStyleAssignment : BaseClassIfc, IfcStyleAssignmentSelect //DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.LinkToString(mStyles[0]);
			for (int icounter = 1; icounter < mStyles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mStyles[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mStyles = ParserSTEP.StripListLink(str, ref pos, len); }
	}
	public partial class IfcProcedure : IfcProcess
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? ",'" + ParserIfc.Encode(mIdentification) + "'" : "") + ",." +
				mPredefinedType.ToString() + (release < ReleaseVersion.IFC4 ? mUserDefinedProcedureType == "$" ? ".,$" : ".,'" + mUserDefinedProcedureType + "'" : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if(release < ReleaseVersion.IFC4)
				mIdentification = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));

			Enum.TryParse<IfcProcedureTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			if(release < ReleaseVersion.IFC4)
				mUserDefinedProcedureType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcProcedureType : IfcTypeProcess //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcProcedureTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcProcess : IfcObject // ABSTRACT SUPERTYPE OF (ONEOF (IfcProcedure ,IfcTask))
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release > ReleaseVersion.IFC2x3 ? (string.IsNullOrEmpty(mIdentification) ? ",$," : ",'" + ParserIfc.Encode(mIdentification) + "',") + 
				(string.IsNullOrEmpty(mLongDescription) ? "$" : "'" + ParserIfc.Encode(mLongDescription) + "'") : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release > ReleaseVersion.IFC2x3)
			{
				mIdentification = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
				mLongDescription = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			}
		}
	}
	public abstract partial class IfcProduct : IfcObject, IfcProductSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcAnnotation ,IfcElement ,IfcGrid ,IfcPort ,IfcProxy ,IfcSpatialElement ,IfcStructuralActivity ,IfcStructuralItem))
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(ObjectPlacement) + "," + ParserSTEP.ObjToLinkString(mRepresentation); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			ObjectPlacement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcObjectPlacement;
			Representation = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProductDefinitionShape;
		}
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcProductsOfCombustionProperties	 // DEPRECATED IFC4
	public partial class IfcProductRepresentation<Representation, RepresentationItem> : BaseClassIfc //(IfcMaterialDefinitionRepresentation ,IfcProductDefinitionShape)); //IFC4 Abstract
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,(" : "'" + mDescription + "',(");
			if (mRepresentations.Count > 0)
				result += "#" + string.Join(",#", mRepresentations.ConvertAll(x => x.mIndex.ToString()));
			return result + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			Representations.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as Representation));
		}
	}
	public partial class IfcProfileDef : BaseClassIfc, IfcResourceObjectSelect // SUPERTYPE OF (ONEOF (IfcArbitraryClosedProfileDef ,IfcArbitraryOpenProfileDef
	{  //,IfcCompositeProfileDef ,IfcDerivedProfileDef ,IfcParameterizedProfileDef));  IFC2x3 abstract 
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mProfileType.ToString() + (mProfileName == "$" ? ".,$" : ".,'" + mProfileName + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProfileTypeEnum>(s.Replace(".", ""), true, out mProfileType);
			mProfileName = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcProfileProperties : IfcExtendedProperties //IFC4 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			IfcProfileDef profileDefinition = ProfileDefinition;
			if (profileDefinition == null)
				return "";
			if(release < ReleaseVersion.IFC4)
				return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mName) ? ",$,#" : ",'" + ParserIfc.Encode(mName) + "',#") + profileDefinition.Index;
			
			return base.BuildStringSTEP(release) + ",#" + profileDefinition.Index;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if(release < ReleaseVersion.IFC4)
				mName = ParserSTEP.StripString(str, ref pos, len);
			ProfileDefinition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
		}
	}
	public partial class IfcProjectedCRS : IfcCoordinateReferenceSystem //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mMapProjection == "$" ? ",$," : ",'" + mMapProjection + "',") +
				(mMapZone == "$" ? "$," : "'" + mMapZone + "',") + ParserSTEP.LinkToString(mMapUnit);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mMapProjection = ParserSTEP.StripString(str, ref pos, len);
			mMapZone = ParserSTEP.StripString(str, ref pos, len);
			mMapUnit = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	//ENTITY IfcProjectionCurve // DEPRECATED IFC4
	public partial class IfcProjectionElement : IfcFeatureElementAddition
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcProjectionElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcProjectionElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcProjectOrder : IfcControl
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? ",'" + mIdentification + "',." : ",.") + mPredefinedType.ToString() + (mStatus == "$" ? ".,$" : ".," + mStatus + "'") + (release < ReleaseVersion.IFC4 ? "" : (mLongDescription == "$" ? ",$" : ",'" + mLongDescription + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
				mIdentification = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProjectOrderTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			mStatus = ParserSTEP.StripString(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
				mLongDescription = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcProjectOrderRecord : IfcControl // DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.LinkToString(mRecords[0]);
			for (int icounter = 1; icounter < mRecords.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRecords[icounter]);
			return str + "),." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRecords = ParserSTEP.StripListLink(str, ref pos, len);
			Enum.TryParse<IfcProjectOrderRecordTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcProperty : IfcPropertyAbstraction  //ABSTRACT SUPERTYPE OF (ONEOF(IfcComplexProperty,IfcSimpleProperty));
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mName + (mDescription == "$" ? "',$" : "','" + mDescription + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcPropertyBoundedValue : IfcSimpleProperty
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mUpperBoundValue == null ? ",$," : "," + mUpperBoundValue.ToString() + ",") + (mLowerBoundValue == null ? "$," : mLowerBoundValue.ToString() + ",") + ParserSTEP.LinkToString(mUnit) + (release < ReleaseVersion.IFC4 ? "" : (mSetPointValue == null ? ",$" : "," + mSetPointValue.ToString())); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				mUpperBoundValue = ParserIfc.parseValue(s);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				mLowerBoundValue = ParserIfc.parseValue(s);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				mUnit = ParserSTEP.ParseLink(s);
			if (release != ReleaseVersion.IFC2x3)
			{
				s = ParserSTEP.StripField(str, ref pos, len);
				if (s != "$")
					mSetPointValue = ParserIfc.parseValue(s);
			}
		}
	}
	public partial class IfcPropertyBoundedValue<T> : IfcSimpleProperty where T : IfcValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mUpperBoundValue == null ? ",$," : "," + mUpperBoundValue.ToString() + ",") + (mLowerBoundValue == null ? "$," : mLowerBoundValue.ToString() + ",") + ParserSTEP.LinkToString(mUnit) + (release < ReleaseVersion.IFC4 ? "" : (mSetPointValue == null ? ",$" : "," + mSetPointValue.ToString())); }
	}
	public partial class IfcPropertyDependencyRelationship : IfcResourceLevelRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#"+ mDependingProperty + ",#" + mDependantProperty + (mExpression == "$" ? ",$" : ",'" + mExpression + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDependingProperty = ParserSTEP.StripLink(str, ref pos, len);
			mDependantProperty = ParserSTEP.StripLink(str, ref pos, len);
			mExpression = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcPropertyEnumeratedValue : IfcSimpleProperty
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + ",(" + mEnumerationValues[0].ToString();
			for (int icounter = 1; icounter < mEnumerationValues.Count; icounter++)
				result += "," + mEnumerationValues[icounter].ToString();
			return result + ")," + ParserSTEP.LinkToString(mEnumerationReference);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> fields = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			mEnumerationValues.AddRange(fields.Select(x => ParserIfc.parseValue(x)));
			mEnumerationReference = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPropertyEnumeration : IfcPropertyAbstraction
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string s = base.BuildStringSTEP(release) + ",'" + mName + "',(" + mEnumerationValues[0].ToString();
			for (int icounter = 1; icounter < mEnumerationValues.Count; icounter++)
				s += "," + mEnumerationValues[icounter].ToString();
			return s + ")," + ParserSTEP.LinkToString(mUnit);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			mEnumerationValues = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => ParserIfc.parseValue(x));
			mUnit = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPropertyListValue : IfcSimpleProperty
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mNominalValue == null || mNominalValue.Count == 0 ? ",$," : ",(" + string.Join(",", mNominalValue.Select(x=>x.ToString())) + "),") + (mUnit == 0 ? "$" : "#" + mUnit);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> values = ParserSTEP.SplitLineFields(str.Substring(1, str.Length - 2));
			for (int icounter = 0; icounter < values.Count; icounter++)
			{
				IfcValue value = ParserIfc.parseValue(values[icounter]);
				if (value != null)
					mNominalValue.Add(value);
			}
			mUnit = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPropertyListValue<T> : IfcSimpleProperty where T : IfcValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release);
			if (mNominalValue == null)
				result += ",$,";
			else
			{
				result += ",(" + mNominalValue[0].ToString();
				for (int icounter = 1; icounter < mNominalValue.Count; icounter++)
					result += "," + mNominalValue[icounter].ToString();
				result += "),";
			}
			return result + (mUnit == 0 ? "$" : "#" + mUnit);
		}
	}
	public partial class IfcPropertyReferenceValue : IfcSimpleProperty
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mUsageName == "$" ? ",$," : ",'" + mUsageName + "',") + (mPropertyReference == 0 ? "$" : "#" + mPropertyReference); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mUsageName = ParserSTEP.StripString(str, ref pos, len);
			mPropertyReference = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPropertySet : IfcPropertySetDefinition
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mHasProperties.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mHasProperties.Values.Select(x=>x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			foreach (IfcProperty property in ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcProperty))
				addProperty(property);
		}
	}
	public partial class IfcPropertySetTemplate : IfcPropertyTemplateDefinition
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4 || mHasPropertyTemplates.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + (mTemplateType == IfcPropertySetTemplateTypeEnum.NOTDEFINED ? ",$," : ",." + mTemplateType + ".,") +
				(mApplicableEntity == "$" ? "$,(#" : "'" + mApplicableEntity + "',(#") + string.Join(",#", mHasPropertyTemplates.Values.Select(x => x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("."))
				Enum.TryParse<IfcPropertySetTemplateTypeEnum>(field.Replace(".", ""), true, out mTemplateType);
			mApplicableEntity = ParserSTEP.StripString(str, ref pos, len);
			foreach (IfcPropertyTemplate property in ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcPropertyTemplate))
				AddPropertyTemplate(property);	
		}
	}
	public partial class IfcPropertySingleValue : IfcSimpleProperty
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + (mNominalValue == null ? (string.IsNullOrEmpty(mVal) ? "$" :  mVal) : mNominalValue.ToString()) + "," + ParserSTEP.ObjToLinkString(mUnit); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$" && !string.IsNullOrEmpty(s))
			{
				mNominalValue = ParserIfc.parseValue(s);
				if (mNominalValue == null)
					mVal = s;
			}
			mUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
		}
	}
	public partial class IfcPropertyTableValue<T,U> : IfcSimpleProperty where T : IfcValue where U :IfcValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + (mDefiningValues.Count > 0 ? ",(" + mDefiningValues[0].ToString() : ",$,");
			for (int icounter = 1; icounter < mDefiningValues.Count; icounter++)
				result += "," + mDefiningValues[icounter].ToString();
			result += (mDefiningValues.Count > 0 ? ")," : "") + (mDefinedValues.Count > 0 ? "(" + mDefinedValues[0].ToString() : "$,");
			for (int icounter = 1; icounter < mDefinedValues.Count; icounter++)
				result += "," + mDefinedValues[icounter].ToString();
			return result + (mDefinedValues.Count > 0 ? ")," : "") + mExpression + "," + ParserSTEP.LinkToString(mDefiningUnit) + "," + ParserSTEP.LinkToString(mDefinedUnit) + ",." + mCurveInterpolation.ToString() + ".";
		}
	}
	public partial class IfcPropertyTableValue : IfcSimpleProperty
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + (mDefiningValues.Count > 0 ? ",(" + mDefiningValues[0].ToString() : ",$,");
			for (int icounter = 1; icounter < mDefiningValues.Count; icounter++)
				result += "," + mDefiningValues[icounter].ToString();
			result += (mDefiningValues.Count > 0 ? ")," : "") + (mDefinedValues.Count > 0 ? "(" + mDefinedValues[0].ToString() : "$,");
			for (int icounter = 1; icounter < mDefinedValues.Count; icounter++)
				result += "," + mDefinedValues[icounter].ToString();
			return result + (mDefinedValues.Count > 0 ? ")" : "") + (mExpression == "$" ? ",$," : ",'" + mExpression + "',") + ParserSTEP.ObjToLinkString(mDefiningUnit) + "," + ParserSTEP.ObjToLinkString(mDefinedUnit) + ",." + mCurveInterpolation.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] != '$')
			{
				List<string> ss = ParserSTEP.SplitLineFields(s);
				for (int icounter = 0; icounter < ss.Count; icounter++)
				{
					IfcValue v = ParserIfc.parseValue(ss[icounter]);
					if (v != null)
						mDefiningValues.Add(v);
				}
			}
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] != '$')
			{
				List<string> ss = ParserSTEP.SplitLineFields(s);
				for (int icounter = 0; icounter < ss.Count; icounter++)
				{
					IfcValue v = ParserIfc.parseValue(ss[icounter]);
					if (v != null)
						mDefinedValues.Add(v);
				}
			}
			mExpression = ParserSTEP.StripString(str, ref pos, len);
			mDefiningUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
			mDefinedUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] != '$')
				Enum.TryParse<IfcCurveInterpolationEnum>(s.Replace(".", ""), true, out mCurveInterpolation);
		}
	}
	public partial class IfcProtectiveDevice : IfcFlowController //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcProtectiveDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProtectiveDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcProtectiveDeviceTrippingUnit : IfcDistributionControlElement //IFC4  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProtectiveDeviceTrippingUnitTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcProtectiveDeviceTrippingUnitType : IfcDistributionControlElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProtectiveDeviceTrippingUnitTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcProtectiveDeviceType : IfcFlowControllerType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProtectiveDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcProxy : IfcProduct
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mProxyType.ToString() + ".," + (mTag == "$" ? "$" : "'" + mTag + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcObjectTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mProxyType);
			mTag = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcPump : IfcFlowMovingDevice //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcPumpTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPumpTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcPumpType : IfcFlowMovingDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPumpTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
}
