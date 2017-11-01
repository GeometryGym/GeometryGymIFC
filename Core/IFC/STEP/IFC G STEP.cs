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
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class IfcGasTerminalType : IfcFlowTerminalType // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcGasTerminalTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcGeneralMaterialProperties : IfcMaterialPropertiesSuperseded // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mMolecularWeight) + "," + ParserSTEP.DoubleOptionalToString(mPorosity) + "," + ParserSTEP.DoubleOptionalToString(mMassDensity); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mMolecularWeight = ParserSTEP.StripDouble(str, ref pos, len);
			mPorosity = ParserSTEP.StripDouble(str, ref pos, len);
			mMassDensity = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcGeneralProfileProperties : IfcProfileProperties //DELETED IFC4  SUPERTYPE OF	(IfcStructuralProfileProperties)
	{ 
		protected override string BuildStringSTEP() { string str = base.BuildStringSTEP(); return (string.IsNullOrEmpty(str) || mDatabase.mRelease != ReleaseVersion.IFC2x3 ? "" : str + "," + ParserSTEP.DoubleOptionalToString(mPhysicalWeight) + "," + ParserSTEP.DoubleOptionalToString(mPerimeter) + "," + ParserSTEP.DoubleOptionalToString(mMinimumPlateThickness) + "," + ParserSTEP.DoubleOptionalToString(mMaximumPlateThickness) + "," + ParserSTEP.DoubleOptionalToString(mCrossSectionArea)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mPhysicalWeight = ParserSTEP.StripDouble(str, ref pos, len);
			mPerimeter = ParserSTEP.StripDouble(str, ref pos, len);
			mMinimumPlateThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mMaximumPlateThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcGeographicElement : IfcElement  //IFC4
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + ",." + mPredefinedType + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcGeographicElementTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcGeographicElementType : IfcElementType //IFC4
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcGeographicElementTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcGeometricRepresentationContext : IfcRepresentationContext, IfcCoordinateReferenceSystemSelect
	{
		protected override string BuildStringSTEP()
		{
			if (this as IfcGeometricRepresentationSubContext != null)
				return base.BuildStringSTEP() + ",*,*,*,*";

			return base.BuildStringSTEP() + "," + (mCoordinateSpaceDimension == 0 ? "*" : mCoordinateSpaceDimension.ToString()) + "," + (mPrecision == 0 ? "*" : ParserSTEP.DoubleOptionalToString(mPrecision)) + "," + ParserSTEP.LinkToString(mWorldCoordinateSystem) + "," + ParserSTEP.LinkToString(mTrueNorth);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mCoordinateSpaceDimension = ParserSTEP.StripInt(str, ref pos, len);
			mPrecision = ParserSTEP.StripDouble(str, ref pos, len);
			mWorldCoordinateSystem = ParserSTEP.StripLink(str, ref pos, len);
			mTrueNorth = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcGeometricRepresentationSubContext : IfcGeometricRepresentationContext
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mContainerContext) + (double.IsNaN(mTargetScale) || mTargetScale <=0 ? ",$,." : "," + ParserSTEP.DoubleOptionalToString(mTargetScale) + ",.") + 
				mTargetView.ToString() + (mUserDefinedTargetView == "$" ?  ".,$" : ".,'" + mUserDefinedTargetView + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mContainerContext = ParserSTEP.StripLink(str, ref pos, len);
			mTargetScale = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcGeometricProjectionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mTargetView);
			mUserDefinedTargetView = ParserSTEP.StripString(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			ContainerContext.HasSubContexts.Add(this);
		}
	}
	public partial class IfcGeometricSet : IfcGeometricRepresentationItem //SUPERTYPE OF(IfcGeometricCurveSet)
	{
		protected override string BuildStringSTEP()
		{
			if (mElements.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mElements[0]);
			for (int icounter = 1; icounter < mElements.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mElements[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mElements = ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)); }
	}
	public partial class IfcGrid : IfcProduct
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(";
			if (mUAxes.Count > 0)
			{
				str += "#" + mUAxes[0];
				for (int icounter = 1; icounter < mUAxes.Count; icounter++)
					str += ",#" + mUAxes[icounter];
			}
			str += "),(";
			if (mVAxes.Count > 0)
			{
				str += "#" + mVAxes[0];
				for (int icounter = 1; icounter < mVAxes.Count; icounter++)
					str += ",#" + mVAxes[icounter];
			}
			str += "),";
			if (mWAxes.Count > 0)
			{
				str += "(#" + mWAxes[0];
				for (int icounter = 1; icounter < mWAxes.Count; icounter++)
					str += ",#" + mWAxes[icounter];
				str += ")";
			}
			else
				str += "$";
			return str + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcGridTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mUAxes = ParserSTEP.StripListLink(str, ref pos, len);
			mVAxes = ParserSTEP.StripListLink(str, ref pos, len);
			mWAxes = ParserSTEP.StripListLink(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcGridTypeEnum>(s.Replace(".", ""), out mPredefinedType);
			}
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			foreach (IfcGridAxis a in UAxes)
				a.mPartOfU = this;
			foreach (IfcGridAxis a in VAxes)
				a.mPartOfV = this;
			foreach (IfcGridAxis a in WAxes)
				a.mPartOfW = this;
		}
	}
	public partial class IfcGridAxis : BaseClassIfc
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mAxisTag == "$" ? ",$," : ",'" + mAxisTag + "',") + ParserSTEP.LinkToString(mAxisCurve) + "," + ParserSTEP.BoolToString(mSameSense); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mAxisTag = ParserSTEP.StripString(str, ref pos, len);
			mAxisCurve = ParserSTEP.StripLink(str, ref pos, len);
			mSameSense = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcGridPlacement : IfcObjectPlacement
	{
		protected override string BuildStringSTEP() { return (mPlacesObject.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPlacementLocation) + "," + ParserSTEP.LinkToString(mPlacementRefDirection)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mPlacementLocation = ParserSTEP.StripLink(str, ref pos, len);
			mPlacementRefDirection = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
}
