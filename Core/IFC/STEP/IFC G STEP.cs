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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcGasTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcGeneralMaterialProperties : IfcMaterialProperties // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mMolecularWeight) + "," + ParserSTEP.DoubleOptionalToString(mPorosity) + "," + ParserSTEP.DoubleOptionalToString(mMassDensity); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mMolecularWeight = ParserSTEP.StripDouble(str, ref pos, len);
			mPorosity = ParserSTEP.StripDouble(str, ref pos, len);
			mMassDensity = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcGeneralProfileProperties : IfcProfileProperties //DELETED IFC4  SUPERTYPE OF	(IfcStructuralProfileProperties)
	{ 
		protected override string BuildStringSTEP(ReleaseVersion release) { string str = base.BuildStringSTEP(release); return (string.IsNullOrEmpty(str) || release != ReleaseVersion.IFC2x3 ? "" : str + "," + ParserSTEP.DoubleOptionalToString(mPhysicalWeight) + "," + ParserSTEP.DoubleOptionalToString(mPerimeter) + "," + ParserSTEP.DoubleOptionalToString(mMinimumPlateThickness) + "," + ParserSTEP.DoubleOptionalToString(mMaximumPlateThickness) + "," + ParserSTEP.DoubleOptionalToString(mCrossSectionArea)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPhysicalWeight = ParserSTEP.StripDouble(str, ref pos, len);
			mPerimeter = ParserSTEP.StripDouble(str, ref pos, len);
			mMinimumPlateThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mMaximumPlateThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mCrossSectionArea = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcGeographicElement : IfcElement  //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + ",." + mPredefinedType + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcGeographicElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcGeographicElementType : IfcElementType //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcGeographicElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcGeometricRepresentationContext : IfcRepresentationContext, IfcCoordinateReferenceSystemSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (this as IfcGeometricRepresentationSubContext != null)
				return base.BuildStringSTEP(release) + ",*,*,*,*";

			return base.BuildStringSTEP(release) + "," + (mCoordinateSpaceDimension == 0 ? "*" : mCoordinateSpaceDimension.ToString()) + "," + (mPrecision == 0 ? "*" : ParserSTEP.DoubleOptionalToString(mPrecision)) + ",#" + mWorldCoordinateSystem.Index + "," + ParserSTEP.ObjToLinkString(mTrueNorth);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mCoordinateSpaceDimension = ParserSTEP.StripInt(str, ref pos, len);
			mPrecision = ParserSTEP.StripDouble(str, ref pos, len);
			WorldCoordinateSystem = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement;
			TrueNorth = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
		}
	}
	public partial class IfcGeometricRepresentationSubContext : IfcGeometricRepresentationContext
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mContainerContext.Index + (double.IsNaN(mTargetScale) || mTargetScale <=0 ? ",$,." : "," + ParserSTEP.DoubleOptionalToString(mTargetScale) + ",.") + 
				mTargetView.ToString() + (mUserDefinedTargetView == "$" ?  ".,$" : ".,'" + mUserDefinedTargetView + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mContainerContext = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcGeometricRepresentationContext;
			mTargetScale = ParserSTEP.StripDouble(str, ref pos, len);
			Enum.TryParse<IfcGeometricProjectionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mTargetView);
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
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (mElements.Count == 0 ? "" : base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mElements.ConvertAll(x => x.Index)) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Elements = new SET<IfcGeometricSetSelect>(ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)).ConvertAll(x => dictionary[x] as IfcGeometricSetSelect));
		}
	}
	public partial class IfcGrid : IfcProduct
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mUAxes.ConvertAll(x => x.Index.ToString()));
			str += "),(#" + string.Join(",#", mVAxes.ConvertAll(x => x.Index.ToString()));
			str += (mWAxes.Count == 0 ? "),$" : "),#" + string.Join(",#", mWAxes.ConvertAll(x=>x.Index.ToString())));
			return str + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcGridTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			UAxes.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcGridAxis));
			VAxes.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcGridAxis));
			WAxes.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcGridAxis));
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcGridTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcGridAxis : BaseClassIfc
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mAxisTag == "$" ? ",$," : ",'" + mAxisTag + "',#") + AxisCurve.Index.ToString() + "," + ParserSTEP.BoolToString(mSameSense); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mAxisTag = ParserSTEP.StripString(str, ref pos, len);
			AxisCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			mSameSense = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcGridPlacement : IfcObjectPlacement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mPlacesObject.Count == 0 ? "" : base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mPlacementLocation) + "," + ParserSTEP.LinkToString(mPlacementRefDirection)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mPlacementLocation = ParserSTEP.StripLink(str, ref pos, len);
			mPlacementRefDirection = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
}
