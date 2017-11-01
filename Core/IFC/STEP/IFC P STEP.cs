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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPosition); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mPosition = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPath : IfcTopologicalRepresentationItem
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(";
			if (mEdgeList.Count > 0)
				str += ParserSTEP.LinkToString(mEdgeList[0]);
			for (int icounter = 1; icounter < mEdgeList.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mEdgeList[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mEdgeList = ParserSTEP.StripListLink(str, ref pos, len); }
	}
	public partial class IfcPCurve : IfcCurve
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.LinkToString(mReferenceCurve); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mBasisSurface = ParserSTEP.StripLink(str, ref pos, len);
			mReferenceCurve = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPerformanceHistory : IfcControl
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mLifeCyclePhase + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mLifeCyclePhase = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	//ENTITY IfcPermeableCoveringProperties : IfcPreDefinedPropertySet //IFC2x3 
	public partial class IfcPermit : IfcControl
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mPermitID + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mPermitID = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcPerson : BaseClassIfc, IfcActorSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',");
			if (mFamilyName == "$" && mGivenName == "$")
				str += (mIdentification == "$" ? "'Unknown',$," : "'" + mIdentification + "',$,");
			else
				str += (mFamilyName == "$" ? "$," : "'" + mFamilyName + "',") + (mGivenName == "$" ? "$," : "'" + mGivenName + "',");
			if (mMiddleNames.Count == 0)
				str += "$,";
			else
			{
				str += "('" + mMiddleNames[0];
				for (int icounter = 1; icounter < mMiddleNames.Count; icounter++)
					str += "','" + mMiddleNames[icounter];
				str += "'),";
			}
			if (mPrefixTitles.Count == 0)
				str += "$,";
			else
			{
				str += "('" + mPrefixTitles[0];
				for (int icounter = 1; icounter < mPrefixTitles.Count; icounter++)
					str += "','" + mPrefixTitles[icounter];
				str += "'),";
			}
			if (mSuffixTitles.Count == 0)
				str += "$,";
			else
			{
				str += "('" + mSuffixTitles[0];
				for (int icounter = 1; icounter < mSuffixTitles.Count; icounter++)
					str += "','" + mSuffixTitles[icounter];
				str += "'),";
			}
			if (mRoles.Count == 0)
				str += "$,";
			else
			{
				str += "(" + ParserSTEP.LinkToString(mRoles[0]);
				for (int icounter = 1; icounter < mRoles.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRoles[icounter]);
				str += "),";
			}
			if (mAddresses.Count == 0)
				str += "$";
			else
			{
				str += "(" + ParserSTEP.LinkToString(mAddresses[0]);
				for (int icounter = 1; icounter < mAddresses.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mAddresses[icounter]);
				str += ")";
			}
			return str;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mIdentification = ParserSTEP.StripString(str, ref pos, len);
			mFamilyName = ParserSTEP.StripString(str, ref pos, len);
			mGivenName = ParserSTEP.StripString(str, ref pos, len);
			mMiddleNames = ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).ConvertAll(x => x.Replace("'", ""));
			mPrefixTitles = ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).ConvertAll(x => x.Replace("'", ""));
			mSuffixTitles = ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).ConvertAll(x => x.Replace("'", ""));
			mRoles = ParserSTEP.StripListLink(str, ref pos, len);
			mAddresses = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcPersonAndOrganization : BaseClassIfc, IfcActorSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect
	{
		protected override string BuildStringSTEP()
		{
			string str;
			if (mRoles.Count == 0)
				str = ",$";
			else
			{
				str = ",(" + ParserSTEP.LinkToString(mRoles[0]);
				for (int icounter = 1; icounter < mRoles.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRoles[icounter]);
				str += ")";
			}
			return base.BuildStringSTEP() + ",#" + mThePerson + ",#" + mTheOrganization + str;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mThePerson = ParserSTEP.StripLink(str, ref pos, len);
			mTheOrganization = ParserSTEP.StripLink(str, ref pos, len);
			mRoles = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	//ENTITY IfcPhysicalComplexQuantity
	public abstract partial class IfcPhysicalQuantity : BaseClassIfc, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF(ONEOF(IfcPhysicalComplexQuantity, IfcPhysicalSimpleQuantity));
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mName + (mDescription == "$" ? "',$" : "','" + mDescription + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public abstract partial class IfcPhysicalSimpleQuantity : IfcPhysicalQuantity //ABSTRACT SUPERTYPE OF (ONEOF (IfcQuantityArea ,IfcQuantityCount ,IfcQuantityLength ,IfcQuantityTime ,IfcQuantityVolume ,IfcQuantityWeight))
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mUnit); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mUnit = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPile : IfcBuildingElement
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease != ReleaseVersion.IFC2x3 && mPredefinedType == IfcPileTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") +
				(mConstructionType == IfcPileConstructionEnum.NOTDEFINED ? "$" : "." + mConstructionType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				mPredefinedType = (IfcPileTypeEnum)Enum.Parse(typeof(IfcPileTypeEnum), s.Replace(".", ""));
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				mConstructionType = (IfcPileConstructionEnum)Enum.Parse(typeof(IfcPileConstructionEnum), s.Replace(".", ""));
		}
	}
	public partial class IfcPileType : IfcBuildingElementType
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPileTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcPipeFitting : IfcFlowFitting //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcPipeFittingTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPipeFittingTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcPipeFittingType : IfcFlowFittingType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPipeFittingTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcPipeSegment : IfcFlowSegment //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcPipeSegmentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPipeSegmentTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcPipeSegmentType : IfcFlowSegmentType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPipeSegmentTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcPixelTexture : IfcSurfaceTexture
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mWidth + "," + mHeight + "," + mColourComponents + ",(" + string.Join(",", mPixel.ConvertAll(x => "'" + x + "'")) + ")"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mWidth = ParserSTEP.StripInt(str, ref pos, len);
			mHeight = ParserSTEP.StripInt(str, ref pos, len);
			mColourComponents = ParserSTEP.StripInt(str, ref pos, len);
			mPixel = ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len));
		}
	}
	public abstract partial class IfcPlacement : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcAxis1Placement ,IfcAxis2Placement2D ,IfcAxis2Placement3D))*/
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mLocation); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mLocation = ParserSTEP.StripLink(str, ref pos, len); }
	}
	public partial class IfcPlanarBox : IfcPlanarExtent
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPlacement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mPlacement = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPlanarExtent : IfcGeometricRepresentationItem
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mSizeInX) + "," + ParserSTEP.DoubleToString(mSizeInY); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mSizeInX = ParserSTEP.StripDouble(str, ref pos, len);
			mSizeInY = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcPlate : IfcBuildingElement
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? base.BuildStringSTEP() : base.BuildStringSTEP() + (mPredefinedType == IfcPlateTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcPlateTypeEnum>(s.Replace(".", ""), out mPredefinedType);
			}
		}
	}
	public partial class IfcPlateType : IfcBuildingElementType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPlateTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcPointOnCurve : IfcPoint
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBasisCurve) + "," + ParserSTEP.DoubleToString(mPointParameter); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mBasisCurve = ParserSTEP.StripLink(str, ref pos, len);
			mPointParameter = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcPointOnSurface : IfcPoint
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.DoubleToString(mPointParameterU) + "," + ParserSTEP.DoubleToString(mPointParameterV); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mBasisSurface = ParserSTEP.StripLink(str, ref pos, len);
			mPointParameterU = ParserSTEP.StripDouble(str, ref pos, len);
			mPointParameterV = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcPolygonalBoundedHalfSpace : IfcHalfSpaceSolid
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPosition) + "," + ParserSTEP.LinkToString(mPolygonalBoundary); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mPosition = ParserSTEP.StripLink(str, ref pos, len);
			mPolygonalBoundary = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPolygonalFaceSet : IfcTessellatedFaceSet //IFC4A2
	{
		protected override string BuildStringSTEP()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("," + ParserSTEP.BoolToString(mClosed) + ",(#" + mFaces[0]);
			for (int icounter = 1; icounter < mFaces.Count; icounter++)
				sb.Append(",#" + mFaces[icounter]);
			if (mPnIndex.Count == 0)
				sb.Append("),$");
			else
			{
				sb.Append("),(" + mPnIndex[0]);
				for (int icounter = 1; icounter < mPnIndex.Count; icounter++)
					sb.Append("," + mPnIndex[icounter]);
				sb.Append(")");
			}

			return base.BuildStringSTEP() + sb.ToString();
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mClosed = ParserSTEP.StripBool(str, ref pos, len);
			mFaces = ParserSTEP.StripListLink(str, ref pos, len);
			mPnIndex = ParserSTEP.StripListInt(str, ref pos, len);
		}
	}
	public partial class IfcPolyline : IfcBoundedCurve
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(";
			if (mPoints.Count > 0)
				str += ParserSTEP.LinkToString(mPoints[0]);
			for (int icounter = 1; icounter < mPoints.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mPoints[icounter]);
			str += ")";
			return base.BuildStringSTEP() + str;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mPoints = ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)); }
	}
	public partial class IfcPolyloop : IfcLoop
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(";
			if (mPolygon.Count > 0)
				str += "#" + mPolygon[0];
			for (int icounter = 1; icounter < mPolygon.Count; icounter++)
				str += ",#" + mPolygon[icounter];
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mPolygon = ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)); }
	}
	public partial class IfcPostalAddress : IfcAddress
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + (mInternalLocation == "$" ? ",$," : ",'" + mInternalLocation + "',");
			if (mAddressLines.Count == 0)
				str += "$";
			else
			{
				str += "('" + mAddressLines[0];
				for (int icounter = 1; icounter < mAddressLines.Count; icounter++)
					str += "','" + mAddressLines[icounter];
				str += "')";
			}
			return str + (mPostalBox == "$" ? ",$" : ",'" + mPostalBox + "'") + (mTown == "$" ? ",$" : ",'" + mTown + "'") + (mRegion == "$" ? ",$" : ",'" + mRegion + "'") + (mPostalCode == "$" ? ",$" : ",'" + mPostalCode + "'") + (mCountry == "$" ? ",$" : ",'" + mCountry + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mInternalLocation = ParserSTEP.StripString(str, ref pos, len);
			if (string.IsNullOrEmpty(mInternalLocation))
				mInternalLocation = "$";
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
					mAddressLines.Add(lst[icounter].Replace("'", ""));
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mName + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mName = ParserSTEP.StripString(str, ref pos, len); }
	}
	public partial class IfcPresentationLayerAssignment : BaseClassIfc //SUPERTYPE OF	(IfcPresentationLayerWithStyle);
	{
		protected override string BuildStringSTEP()
		{
			if (mAssignedItems.Count < 1)
				return "";
			string str = base.BuildStringSTEP() + ",'" + mName + (mDescription == "$" ? "',$,(" : "'," + mDescription + ",(") + ParserSTEP.LinkToString(mAssignedItems[0]);
			if (mAssignedItems.Count > 100)
			{
				StringBuilder sb = new StringBuilder();
				for (int icounter = 1; icounter < mAssignedItems.Count; icounter++)
					sb.Append(",#" + mAssignedItems[icounter]);
				str += sb.ToString();
			}
			else
			{
				for (int icounter = 1; icounter < mAssignedItems.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mAssignedItems[icounter]);
			}
			return str + (mIdentifier == "$" ? "),$" : "),'" + mIdentifier + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mAssignedItems = ParserSTEP.StripListLink(str, ref pos, len);
			mIdentifier = ParserSTEP.StripString(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			foreach (IfcLayeredItem item in AssignedItems)
				item.AssignLayer(this);
		}
	}
	public partial class IfcPresentationLayerWithStyle : IfcPresentationLayerAssignment
	{
		protected override string BuildStringSTEP()
		{
			if (mAssignedItems.Count < 1 || mLayerStyles.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + "," + ParserIfc.LogicalToString(mLayerOn) + "," +
				ParserIfc.LogicalToString(mLayerFrozen) + "," + ParserIfc.LogicalToString(mLayerBlocked);
			str += ",(" + ParserSTEP.LinkToString(mLayerStyles[0]);
			for (int icounter = 1; icounter < mLayerStyles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mLayerStyles[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mLayerOn = ParserIfc.StripLogical(str, ref pos, len);
			mLayerFrozen = ParserIfc.StripLogical(str, ref pos, len);
			mLayerBlocked = ParserIfc.StripLogical(str, ref pos, len);
			mLayerStyles = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public abstract partial class IfcPresentationStyle : BaseClassIfc, IfcStyleAssignmentSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcCurveStyle,IfcFillAreaStyle,IfcSurfaceStyle,IfcSymbolStyle,IfcTextStyle));
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mName == "$" ? ",$" : ",'" + mName + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mName = ParserSTEP.StripString(str, ref pos, len); }
	}
	public partial class IfcPresentationStyleAssignment : BaseClassIfc, IfcStyleAssignmentSelect //DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mStyles[0]);
			for (int icounter = 1; icounter < mStyles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mStyles[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mStyles = ParserSTEP.StripListLink(str, ref pos, len); }
	}
	public partial class IfcProcedure : IfcProcess
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mProcedureID + "',." + mProcedureType.ToString() + (mUserDefinedProcedureType == "$" ? ".,$" : ".,'" + mUserDefinedProcedureType + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mProcedureID = ParserSTEP.StripString(str, ref pos, len);

			Enum.TryParse<IfcProcedureTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mProcedureType);
			mUserDefinedProcedureType = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcProcedureType : IfcTypeProcess //IFC4
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			Enum.TryParse<IfcProcedureTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mPredefinedType);
		}
	}
	public abstract partial class IfcProcess : IfcObject // ABSTRACT SUPERTYPE OF (ONEOF (IfcProcedure ,IfcTask))
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease != ReleaseVersion.IFC2x3 ? (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',") + (mLongDescription == "$" ? "$" : "'" + mLongDescription + "'") : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				mIdentification = ParserSTEP.StripString(str, ref pos, len);
				mLongDescription = ParserSTEP.StripString(str, ref pos, len);
			}
		}
	}
	public abstract partial class IfcProduct : IfcObject, IfcProductSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcAnnotation ,IfcElement ,IfcGrid ,IfcPort ,IfcProxy ,IfcSpatialElement ,IfcStructuralActivity ,IfcStructuralItem))
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPlacement) + "," + ParserSTEP.LinkToString(mRepresentation); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mPlacement = ParserSTEP.StripLink(str, ref pos, len);
			mRepresentation = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mPlacement > 0)
				Placement.mPlacesObject.Add(this);
			if (mRepresentation > 0)
			{
				IfcProductDefinitionShape pds = Representation as IfcProductDefinitionShape;
				if (pds != null)
					pds.mShapeOfProduct.Add(this);
			}
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcProductsOfCombustionProperties	 // DEPRECEATED IFC4
	public partial class IfcProductRepresentation : BaseClassIfc //(IfcMaterialDefinitionRepresentation ,IfcProductDefinitionShape)); //IFC4 Abstract
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,(" : "'" + mDescription + "',(");
			if (mRepresentations.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRepresentations[0]);
				for (int icounter = 1; icounter < mRepresentations.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRepresentations[icounter]);
			}
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mRepresentations = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			foreach (IfcRepresentation r in Representations)
			{
				if (r != null)
					r.mOfProductRepresentation.Add(this);
			}
		}
	}
	public partial class IfcProfileDef : BaseClassIfc, IfcResourceObjectSelect // SUPERTYPE OF (ONEOF (IfcArbitraryClosedProfileDef ,IfcArbitraryOpenProfileDef
	{  //,IfcCompositeProfileDef ,IfcDerivedProfileDef ,IfcParameterizedProfileDef));  IFC2x3 abstract 
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mProfileType.ToString() + (mProfileName == "$" ? ".,$" : ".,'" + mProfileName + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProfileTypeEnum>(s.Replace(".",""), out mProfileType);
			mProfileName = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcProfileProperties : IfcExtendedProperties //IFC2x3 Abstract : BaseClassIfc ABSTRACT SUPERTYPE OF	(ONEOF(IfcGeneralProfileProperties, IfcRibPlateProfileProperties));
	{
		protected override string BuildStringSTEP() { return (ProfileDefinition == null ? "" : base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? (mName == "$" ? ",$," : ",'" + mName + "',") : ",") + ParserSTEP.LinkToString(mProfileDefinition)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			if (release == ReleaseVersion.IFC2x3)
			{
				mName = ParserSTEP.StripString(str, ref pos, len);
				mProfileDefinition = ParserSTEP.StripLink(str, ref pos, len);
			}
			else
			{
				base.parse(str, ref pos, release, len);
				mProfileDefinition = ParserSTEP.StripLink(str, ref pos, len);
			}
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			ProfileDefinition.mHasProperties.Add(this);
		}
	}
	public partial class IfcProjectedCRS : IfcCoordinateReferenceSystem //IFC4
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mMapProjection == "$" ? ",$," : ",'" + mMapProjection + "',") +
				(mMapZone == "$" ? "$," : "'" + mMapZone + "',") + ParserSTEP.LinkToString(mMapUnit);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mMapProjection = ParserSTEP.StripString(str, ref pos, len);
			mMapZone = ParserSTEP.StripString(str, ref pos, len);
			mMapUnit = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	//ENTITY IfcProjectionCurve // DEPRECEATED IFC4
	public partial class IfcProjectionElement : IfcFeatureElementAddition
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcProjectionElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcProjectionElementTypeEnum>(s.Replace(".", ""), out mPredefinedType);
			}
		}
	}
	public partial class IfcProjectOrder : IfcControl
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ",'" + mIdentification + "',." : ",.") + mPredefinedType.ToString() + (mStatus == "$" ? ".,$" : ".," + mStatus + "'") + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mLongDescription == "$" ? ",$" : ",'" + mLongDescription + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			if (release == ReleaseVersion.IFC2x3)
				mIdentification = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProjectOrderTypeEnum>(s.Replace(".", ""), out mPredefinedType);
			mStatus = ParserSTEP.StripString(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
				mLongDescription = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcProjectOrderRecord : IfcControl // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRecords[0]);
			for (int icounter = 1; icounter < mRecords.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRecords[icounter]);
			return str + "),." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRecords = ParserSTEP.StripListLink(str, ref pos, len);
			Enum.TryParse<IfcProjectOrderRecordTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mPredefinedType);
		}
	}
	public abstract partial class IfcProperty : IfcPropertyAbstraction  //ABSTRACT SUPERTYPE OF (ONEOF(IfcComplexProperty,IfcSimpleProperty));
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mName + (mDescription == "$" ? "',$" : "','" + mDescription + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcPropertyBoundedValue : IfcSimpleProperty
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mUpperBoundValue == null ? ",$," : "," + mUpperBoundValue.ToString() + ",") + (mLowerBoundValue == null ? "$," : mLowerBoundValue.ToString() + ",") + ParserSTEP.LinkToString(mUnit) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mSetPointValue == null ? ",$" : "," + mSetPointValue.ToString())); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mUpperBoundValue == null ? ",$," : "," + mUpperBoundValue.ToString() + ",") + (mLowerBoundValue == null ? "$," : mLowerBoundValue.ToString() + ",") + ParserSTEP.LinkToString(mUnit) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mSetPointValue == null ? ",$" : "," + mSetPointValue.ToString())); }
	}
	public partial class IfcPropertyDependencyRelationship : IfcResourceLevelRelationship
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",#"+ mDependingProperty + ",#" + mDependantProperty + (mExpression == "$" ? ",$" : ",'" + mExpression + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mDependingProperty = ParserSTEP.StripLink(str, ref pos, len);
			mDependantProperty = ParserSTEP.StripLink(str, ref pos, len);
			mExpression = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcPropertyEnumeratedValue : IfcSimpleProperty
	{
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + ",(" + mEnumerationValues[0].ToString();
			for (int icounter = 1; icounter < mEnumerationValues.Count; icounter++)
				result += "," + mEnumerationValues[icounter].ToString();
			return result + ")," + ParserSTEP.LinkToString(mEnumerationReference);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> fields = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			mEnumerationValues = fields.ConvertAll(x => ParserIfc.parseValue(x));
			mEnumerationReference = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPropertyEnumeration : IfcPropertyAbstraction
	{
		protected override string BuildStringSTEP()
		{
			string s = base.BuildStringSTEP() + ",'" + mName + "',(" + mEnumerationValues[0].ToString();
			for (int icounter = 1; icounter < mEnumerationValues.Count; icounter++)
				s += "," + mEnumerationValues[icounter].ToString();
			return s + ")," + ParserSTEP.LinkToString(mUnit);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			mEnumerationValues = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => ParserIfc.parseValue(x));
			mUnit = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPropertyListValue : IfcSimpleProperty
	{
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP();
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
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
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
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP();
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mUsageName == "$" ? ",$," : ",'" + mUsageName + "',") + (mPropertyReference == 0 ? "$" : "#" + mPropertyReference); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mUsageName = ParserSTEP.StripString(str, ref pos, len);
			mPropertyReference = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPropertySet : IfcPropertySetDefinition
	{
		protected override string BuildStringSTEP()
		{
			if (mHasProperties.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(#" + mPropertyIndices[0];
			for (int icounter = 1; icounter < mPropertyIndices.Count; icounter++)
				str += ",#" + mPropertyIndices[icounter];
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mPropertyIndices = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			foreach(int i in mPropertyIndices)
			{
				IfcProperty p = mDatabase[i] as IfcProperty;
				if (p != null && !mHasProperties.ContainsKey(p.Name))
				{
					mHasProperties.Add(p.Name, p);
					p.mPartOfPset.Add(this);
				}	
			}
		}
	}
	public partial class IfcPropertySetTemplate : IfcPropertyTemplateDefinition
	{
		protected override string BuildStringSTEP()
		{
			if (mDatabase.Release == ReleaseVersion.IFC2x3 || mHasPropertyTemplates.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + (mTemplateType == IfcPropertySetTemplateTypeEnum.NOTDEFINED ? ",$," : ",." + mTemplateType + ".,") +
				(mApplicableEntity == "$" ? "$,(" : "'" + mApplicableEntity + "',(") + ParserSTEP.LinkToString(mHasPropertyTemplates[0]);
			for (int icounter = 1; icounter < mHasPropertyTemplates.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mHasPropertyTemplates[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("."))
				Enum.TryParse<IfcPropertySetTemplateTypeEnum>(field.Replace(".", ""), out mTemplateType);
			mApplicableEntity = ParserSTEP.StripField(str, ref pos, len).Replace("'", "");
			mHasPropertyTemplates = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcPropertySingleValue : IfcSimpleProperty
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + (mNominalValue == null ? (string.IsNullOrEmpty(mVal) ? "$" :  mVal) : mNominalValue.ToString()) + "," + ParserSTEP.LinkToString(mUnit); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$" && !string.IsNullOrEmpty(s))
			{
				mNominalValue = ParserIfc.parseValue(s);
				if (mNominalValue == null)
					mVal = s;
			}
			mUnit = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcPropertyTableValue<T,U> : IfcSimpleProperty where T : IfcValue where U :IfcValue
	{
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + (mDefiningValues.Count > 0 ? ",(" + mDefiningValues[0].ToString() : ",$,");
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
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + (mDefiningValues.Count > 0 ? ",(" + mDefiningValues[0].ToString() : ",$,");
			for (int icounter = 1; icounter < mDefiningValues.Count; icounter++)
				result += "," + mDefiningValues[icounter].ToString();
			result += (mDefiningValues.Count > 0 ? ")," : "") + (mDefinedValues.Count > 0 ? "(" + mDefinedValues[0].ToString() : "$,");
			for (int icounter = 1; icounter < mDefinedValues.Count; icounter++)
				result += "," + mDefinedValues[icounter].ToString();
			return result + (mDefinedValues.Count > 0 ? ")" : "") + (mExpression == "$" ? ",$," : ",'" + mExpression + "',") + ParserSTEP.LinkToString(mDefiningUnit) + "," + ParserSTEP.LinkToString(mDefinedUnit) + ",." + mCurveInterpolation.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
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
			mDefiningUnit = ParserSTEP.StripLink(str, ref pos, len);
			mDefinedUnit = ParserSTEP.StripLink(str, ref pos, len);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] != '$')
				Enum.TryParse<IfcCurveInterpolationEnum>(s.Replace(".",""), out mCurveInterpolation);
		}
	}
	public partial class IfcProtectiveDevice : IfcFlowController //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcProtectiveDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProtectiveDeviceTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcProtectiveDeviceTrippingUnit : IfcDistributionControlElement //IFC4  
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProtectiveDeviceTrippingUnitTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcProtectiveDeviceTrippingUnitType : IfcDistributionControlElementType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProtectiveDeviceTrippingUnitTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcProtectiveDeviceType : IfcFlowControllerType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProtectiveDeviceTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcProxy : IfcProduct
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mProxyType.ToString() + ".," + (mTag == "$" ? "$" : "'" + mTag + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			Enum.TryParse<IfcObjectTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mProxyType);
			mTag = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcPump : IfcFlowMovingDevice //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcPumpTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPumpTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcPumpType : IfcFlowMovingDeviceType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcPumpTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
}
