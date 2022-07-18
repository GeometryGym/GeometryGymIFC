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
	public abstract partial class IfcParameterizedProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(mPosition); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPosition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement2D;
		}
	}
	public partial class IfcPath
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mEdgeList.Select(x=>"#" + x.StepId));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mEdgeList.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcOrientedEdge)); 
		}
	}
	public partial class IfcPcurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mBasisSurface.StepId + ",#" + mReferenceCurve.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			BasisSurface = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSurface;
			ReferenceCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
		}
	}
	public partial class IfcPavement
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcPavementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcPavementTypeEnum>(s.Substring(1, s.Length - 2), out mPredefinedType);
		}
	}
	public partial class IfcPavementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +  ",." + mPredefinedType.ToString() + "."; 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcPavementTypeEnum>(s.Substring(1, s.Length - 2), out mPredefinedType);
		}
	}
	public partial class IfcPerformanceHistory
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
	public partial class IfcPermeableCoveringProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
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
	public partial class IfcPermit
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mPermitID + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPermitID = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcPerson
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string id = Identification;
			if (string.IsNullOrEmpty(mIdentification) && string.IsNullOrEmpty(mGivenName) && string.IsNullOrEmpty(mFamilyName))
				id = "Unknown";
			return (string.IsNullOrEmpty(id) ? "$," : "'" + ParserIfc.Encode(id) + "',") + 
				(string.IsNullOrEmpty(mFamilyName) ? "$," : "'" + ParserIfc.Encode(mFamilyName) + "',") + 
				(string.IsNullOrEmpty(mGivenName) ? "$," : "'" + ParserIfc.Encode(mGivenName) + "',") +
				(mMiddleNames.Count == 0 ? "$," : "(" + string.Join(",", mMiddleNames.Select(x=> "'" + ParserIfc.Encode(x) + "'")) + "),") +
				(mPrefixTitles.Count == 0 ? "$," : "(" + string.Join(",", mPrefixTitles.Select(x=>"'" + ParserIfc.Encode(x) + "'")) + "),") +
				(mSuffixTitles.Count == 0 ? "$," : "(" + string.Join(",", mSuffixTitles.Select(x=>"'" + ParserIfc.Encode(x) + "'")) + "),") +
				(Roles.Count == 0 ? "$," : "(" + string.Join(",", Roles.ConvertAll(x=> "#" + x.StepId)) + "),") +
				(Addresses.Count == 0 ? "$" : "(" + string.Join(",", Addresses.ConvertAll(x=> "#" + x.StepId)) + ")");
			
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mIdentification = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mFamilyName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mGivenName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mMiddleNames.AddRange(ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).Select(x=>ParserIfc.Decode(x)));
			mPrefixTitles.AddRange(ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).Select(x => ParserIfc.Decode(x)));
			mSuffixTitles.AddRange(ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).Select(x => ParserIfc.Decode(x)));
			mRoles.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=> dictionary[x] as IfcActorRole));
			mAddresses.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=> dictionary[x] as IfcAddress));
		}
	}
	public partial class IfcPersonAndOrganization
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "#" + mThePerson.StepId + ",#" + mTheOrganization.StepId + (mRoles.Count == 0 ? ",$" : ",(" + string.Join(",", mRoles.ConvertAll(x => "#" + x.StepId)) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mThePerson = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPerson;
			mTheOrganization = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcOrganization;
			mRoles = new LIST<IfcActorRole>(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcActorRole));
		}
	}
	public partial class IfcPhysicalComplexQuantity
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
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
	public abstract partial class IfcPhysicalQuantity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "'" + ParserIfc.Encode(mName) + (string.IsNullOrEmpty(mDescription) ? "',$" : "','" + ParserIfc.Encode(mDescription) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public abstract partial class IfcPhysicalSimpleQuantity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mUnit == null ? ",$" : ",#" + mUnit.StepId); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcNamedUnit;
		}
	}
	public partial class IfcPile
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
	public partial class IfcPileType
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
	public partial class IfcPipeFitting
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
	public partial class IfcPipeFittingType
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
	public partial class IfcPipeSegment
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
	public partial class IfcPipeSegmentType
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
	public partial class IfcPixelTexture
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
	public abstract partial class IfcPlacement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mLocation.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mLocation = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPoint; }
	}
	public partial class IfcPlanarBox
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mPlacement.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPlacement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement;
		}
	}
	public partial class IfcPlanarExtent
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return ParserSTEP.DoubleToString(mSizeInX) + "," + ParserSTEP.DoubleToString(mSizeInY); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			SizeInX = ParserSTEP.StripDouble(str, ref pos, len);
			SizeInY = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcPlate
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
	public partial class IfcPlateType
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
	public partial class IfcPointByDistanceExpression
	{
		public override string StepClassName { get { return (mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4X3_RC2 ? "IFCDISTANCEEXPRESSION" : base.StepClassName); } }
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (release < ReleaseVersion.IFC4X3_RC3 ? formatLength((mDistanceAlong as IfcNonNegativeLengthMeasure).Measure) :  mDistanceAlong.ToString()) +
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
	public partial class IfcPointOnCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mBasisCurve.StepId + "," + ParserSTEP.DoubleToString(mPointParameter); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mBasisCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			mPointParameter = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcPointOnSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mBasisSurface.StepId + "," + ParserSTEP.DoubleToString(mPointParameterU) + "," + ParserSTEP.DoubleToString(mPointParameterV); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mBasisSurface = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSurface;
			mPointParameterU = ParserSTEP.StripDouble(str, ref pos, len);
			mPointParameterV = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcPolygonalBoundedHalfSpace
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mPosition.StepId + ",#" + mPolygonalBoundary.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mPosition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement3D;
			mPolygonalBoundary = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcBoundedCurve;
		}
	}
	public partial class IfcPolygonalFaceSet
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + 
				(release < ReleaseVersion.IFC4X3 ? (mClosed == IfcLogicalEnum.UNKNOWN ? ",$" : "," + ParserIfc.LogicalToString(mClosed)) : "") + 
				",(" + string.Join(",", mFaces.Select(x => "#" + x.StepId)) + (mPnIndex.Count == 0 ? "),$" : "),(" + string.Join(",", mPnIndex) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if(release < ReleaseVersion.IFC4X3)
				mClosed = ParserIfc.StripLogical(str, ref pos, len);
			mFaces.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=> dictionary[x] as IfcIndexedPolygonalFace));
			mPnIndex.AddRange(ParserSTEP.StripListInt(str, ref pos, len));
		}
	}
	public partial class IfcPolyline
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "(" + string.Join(",", Points.ConvertAll(x => "#" + x.StepId)) + ")"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { Points.AddRange(ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)).ConvertAll(x=>dictionary[x] as IfcCartesianPoint)); }
	}
	public partial class IfcPolyLoop
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "(" + string.Join(",", mPolygon.ConvertAll(x=> "#" + x.StepId)) + ")" ; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mPolygon.AddRange(ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)).ConvertAll(x => dictionary[x] as IfcCartesianPoint)); }
	}
	public partial class IfcPolynomialCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "#" + mPosition.StepId +
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
	public partial class IfcPostalAddress
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mInternalLocation) ? ",$" : ",'" + ParserIfc.Encode(mInternalLocation) + "'") +
				(mAddressLines.Count == 0 ? ",$" : ",('" + string.Join("','", mAddressLines.Select(x => ParserIfc.Encode(x))) + "')") +
				(string.IsNullOrEmpty(mPostalBox) ? ",$" : ",'" + ParserIfc.Encode(mPostalBox) + "'") + 
				(string.IsNullOrEmpty(mTown) ? ",$" : ",'" + ParserIfc.Encode(mTown) + "'") +
				(string.IsNullOrEmpty(mRegion) ? ",$" : ",'" + ParserIfc.Encode(mRegion) + "'") +
				(string.IsNullOrEmpty(mPostalCode) ? ",$" : ",'" + ParserIfc.Encode(mPostalCode) + "'") + 
				(string.IsNullOrEmpty(mCountry) ? ",$" : ",'" + ParserIfc.Encode(mCountry) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mInternalLocation = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mAddressLines.AddRange(ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len)).Select(x => ParserIfc.Decode(x)));
			mPostalBox = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mTown = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mRegion = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mPostalCode = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mCountry = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public abstract partial class IfcPreDefinedItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "'" + ParserIfc.Encode(mName)+ "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len)); 
		}
	}
	public partial class IfcPresentationLayerAssignment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mAssignedItems.Count < 1)
				return "";
			return "'" + ParserIfc.Encode(mName) + (string.IsNullOrEmpty(mDescription) ? "',$,(" : "','" + ParserIfc.Encode(mDescription) + "',(") + 
				string.Join(",", mAssignedItems.ConvertAll(x=> "#" + x.StepId)) + (string.IsNullOrEmpty(mIdentifier) ? "),$" : "),'" + ParserIfc.Encode(mIdentifier) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			AssignedItems.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcLayeredItem));
			mIdentifier = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcPresentationLayerWithStyle
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mAssignedItems.Count < 1 || mLayerStyles.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + "," + ParserIfc.LogicalToString(mLayerOn) + "," +
				ParserIfc.LogicalToString(mLayerFrozen) + "," + ParserIfc.LogicalToString(mLayerBlocked) + ",(" + 
				string.Join(",", mLayerStyles.Select(x=>"#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mLayerOn = ParserIfc.StripLogical(str, ref pos, len);
			mLayerFrozen = ParserIfc.StripLogical(str, ref pos, len);
			mLayerBlocked = ParserIfc.StripLogical(str, ref pos, len);
			LayerStyles.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcPresentationStyle));
		}
	}
	public abstract partial class IfcPresentationStyle
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (string.IsNullOrEmpty(mName) ? "$" : "'" + ParserIfc.Encode(mName) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len)); }
	}
	public partial class IfcPresentationStyleAssignment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mStyles.Select(x=>"#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{ 
			Styles.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcPresentationStyleSelect));
		}
	}
	public partial class IfcProcedure
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? ",'" + ParserIfc.Encode(mIdentification) + "'" : "") + ",." +
				mPredefinedType.ToString() + (release < ReleaseVersion.IFC4 ? string.IsNullOrEmpty(mUserDefinedProcedureType) ? ".,$" : ".,'" + ParserIfc.Encode(mUserDefinedProcedureType) + "'" : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if(release < ReleaseVersion.IFC4)
				mIdentification = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));

			Enum.TryParse<IfcProcedureTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			if(release < ReleaseVersion.IFC4)
				mUserDefinedProcedureType = ParserIfc.Encode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcProcedureType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcProcedureTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcProcess
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
	public abstract partial class IfcProduct
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(ObjectPlacement) + "," + ParserSTEP.ObjToLinkString(mRepresentation); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			ObjectPlacement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcObjectPlacement;
			Representation = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProductDefinitionShape;
		}
	}
	public partial class IfcProductRepresentation<Representation, RepresentationItem>
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (string.IsNullOrEmpty(mName) ? "$," : "'" + ParserIfc.Encode(mName) + "',") +
				(string.IsNullOrEmpty(mDescription) ? "$,(" : "'" + ParserIfc.Encode(mDescription) + "',(") +
				string.Join(",", mRepresentations.ConvertAll(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			Representations.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as Representation));
		}
	}
	public partial class IfcProfileDef 
	{  
		protected override string BuildStringSTEP(ReleaseVersion release)
		{ 
			return "." + mProfileType.ToString() + (string.IsNullOrEmpty(mProfileName) ? ".,$" : ".,'" + ParserIfc.Encode( mProfileName) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProfileTypeEnum>(s.Replace(".", ""), true, out mProfileType);
			ProfileName =  ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcProfileProperties 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			IfcProfileDef profileDefinition = ProfileDefinition;
			if (profileDefinition == null)
				return "";
			if(release < ReleaseVersion.IFC4)
				return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mName) ? "$,#" : "'" + ParserIfc.Encode(mName) + "',#") + profileDefinition.StepId;
			
			return base.BuildStringSTEP(release) + ",#" + profileDefinition.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if(release < ReleaseVersion.IFC4)
				Name = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			ProfileDefinition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
		}
	}
	public partial class IfcProjectedCRS
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return "";
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty( mMapProjection) ? ",$," : ",'" + ParserIfc.Encode( mMapProjection) + "',") +
				(string.IsNullOrEmpty( mMapZone) ? "$," : "'" + ParserIfc.Encode(mMapZone) + "',") + (mMapUnit == null ? "$" : "#" + mMapUnit.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mMapProjection = ParserIfc.Decode( ParserSTEP.StripString(str, ref pos, len));
			mMapZone = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mMapUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcNamedUnit;
		}
	}
	public partial class IfcProjectionElement
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
	public partial class IfcProjectOrder
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? ",'" + ParserIfc.Encode(mIdentification) + "',." : ",.") + 
				mPredefinedType.ToString() + (string.IsNullOrEmpty(mStatus) ? ".,$" : ".," + ParserIfc.Encode(mStatus) + "'") + 
				(release < ReleaseVersion.IFC4 ? "" : (string.IsNullOrEmpty(mLongDescription) ? ",$" : ",'" + ParserIfc.Encode(mLongDescription) + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
				mIdentification = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcProjectOrderTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			mStatus = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			if (release != ReleaseVersion.IFC2x3)
				mLongDescription = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcProjectOrderRecord
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mRecords.Select(x=>"#" + x.StepId)) + "),." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRecords.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcRelAssignsToProjectOrder));
			Enum.TryParse<IfcProjectOrderRecordTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcProperty
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "'" + ParserIfc.Encode(mName) + (string.IsNullOrEmpty(mSpecification) ? "',$" : "','" + ParserIfc.Encode(mSpecification) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mSpecification = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcPropertyBoundedValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return base.BuildStringSTEP(release) + (mUpperBoundValue == null ? ",$," : "," + mUpperBoundValue.ToString() + ",") +
				(mLowerBoundValue == null ? "$," : mLowerBoundValue.ToString() + ",") + (mUnit == null ? "$" : "#" + mUnit.StepId) +
				(release < ReleaseVersion.IFC4 ? "" : (mSetPointValue == null ? ",$" : "," + mSetPointValue.ToString())); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				mUpperBoundValue = ParserIfc.parseValue(s);
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
				mLowerBoundValue = ParserIfc.parseValue(s);
			mUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
			if (release > ReleaseVersion.IFC2x3)
			{
				s = ParserSTEP.StripField(str, ref pos, len);
				if (s != "$")
					mSetPointValue = ParserIfc.parseValue(s);
			}
		}
	}
	public partial class IfcPropertyBoundedValue<T> : IfcSimpleProperty where T : IfcValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return base.BuildStringSTEP(release) + (mUpperBoundValue == null ? ",$," : "," + mUpperBoundValue.ToString() + ",") +
				(mLowerBoundValue == null ? "$," : mLowerBoundValue.ToString() + ",") + (mUnit == null ? "$" : "#" + mUnit.StepId) +
				(release < ReleaseVersion.IFC4 ? "" : (mSetPointValue == null ? ",$" : "," + mSetPointValue.ToString())); }
	}
	public abstract partial class IfcPropertyConstraintRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return "#" + mRelatingConstraint.StepId + ",(" + String.Join(",", mRelatedProperties.Select(x=>"#" + x.StepId)) +
				(string.IsNullOrEmpty(mName) ? "),$," :  "),'" + ParserIfc.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$" : "'" + ParserIfc.Encode( mDescription) + "'"); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			RelatingConstraint = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcConstraint;
			RelatedProperties.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcProperty));
			Name = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			Description = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcPropertyDependencyRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{ 
			return base.BuildStringSTEP(release) + ",#" + mDependingProperty.StepId + ",#" + mDependantProperty.StepId + 
				(string.IsNullOrEmpty(mExpression) ? ",$" : ",'" + ParserIfc.Encode(mExpression) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDependingProperty = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProperty;
			mDependantProperty = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProperty;
			mExpression = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcPropertyEnumeratedValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + String.Join(",",  mEnumerationValues) + (mEnumerationReference == null ? "),$" : "),#" + mEnumerationReference.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> fields = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			mEnumerationValues.AddRange(fields.Select(x => ParserIfc.parseValue(x)));
			mEnumerationReference = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPropertyEnumeration;
		}
	}
	public partial class IfcPropertyEnumeration
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "'" + mName + "',(" + string.Join(",", mEnumerationValues.Select(x => x.ToString())) +
				(mUnit == null ? "),$" : "),#" + mUnit.ToString());
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			mEnumerationValues.AddRange(ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => ParserIfc.parseValue(x)));
			mUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
		}
	}
	public partial class IfcPropertyListValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mNominalValue == null || mNominalValue.Count == 0 ? ",$," : ",(" + string.Join(",", mNominalValue.Select(x=>x.ToString())) + "),") + (mUnit == null ? "$" : "#" + mUnit.StepId);
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
			mUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
		}
	}
	public partial class IfcPropertyListValue<T> : IfcSimpleProperty where T : IfcValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mNominalValue == null || mNominalValue.Count == 0 ? ",$," : ",(" + string.Join(",", mNominalValue.Select(x => x.ToString())) + "),") + (mUnit == null ? "$" : "#" + mUnit.StepId);
		}
	}
	public partial class IfcPropertyReferenceValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mUsageName) ? ",$," : ",'" + ParserIfc.Encode(mUsageName) + "',") +
				(mPropertyReference == null ? "$" : "#" + mPropertyReference.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mUsageName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mPropertyReference = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcObjectReferenceSelect;
		}
	}
	public partial class IfcPropertySet
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
	public partial class IfcPropertySetTemplate
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4 || mHasPropertyTemplates.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + (mTemplateType == IfcPropertySetTemplateTypeEnum.NOTDEFINED ? ",$," : ",." + mTemplateType + ".,") +
				(string.IsNullOrEmpty(mApplicableEntity) ? "$,(#" : "'" + ParserIfc.Encode(mApplicableEntity) + "',(#") + string.Join(",#", mHasPropertyTemplates.Values.Select(x => x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("."))
				Enum.TryParse<IfcPropertySetTemplateTypeEnum>(field.Replace(".", ""), true, out mTemplateType);
			mApplicableEntity = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			foreach (IfcPropertyTemplate property in ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcPropertyTemplate))
				AddPropertyTemplate(property);	
		}
	}
	public partial class IfcPropertySingleValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + (mNominalValue == null ? (string.IsNullOrEmpty(mVal) ? "$" :  mVal) : mNominalValue.ToString()) + "," + ParserSTEP.ObjToLinkString(mUnit); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$" && !string.IsNullOrEmpty(s))
			{
				try
				{
					mNominalValue = ParserIfc.parseValue(s);
				}
				catch { }
				if (mNominalValue == null)
				{
					mVal = s;
				}
			}
			mUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
		}
	}
	public partial class IfcPropertyTableValue<T,U> : IfcSimpleProperty where T : IfcValue where U :IfcValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mDefiningValues.Count > 0 ? ",(" + string.Join(",", mDefiningValues.Select(x=>x.ToString())) + ")," : ",$,") +
				(mDefinedValues.Count > 0 ? "(" + string.Join(",", mDefinedValues.Select(x=>x.ToString())) + ")," : "$,") +
				(string.IsNullOrEmpty(mExpression) ? "$," : "'" + ParserIfc.Encode(mExpression) + "',") + 
				(mDefiningUnit == null ? "$" : "#" + mDefiningUnit.StepId) + 
				(mDefinedUnit == null ? ",$,." : ",#,." + mDefinedUnit.StepId) + mCurveInterpolation.ToString() + ".";
		}
	}
	public partial class IfcPropertyTableValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mDefiningValues.Count > 0 ? ",(" + string.Join(",", mDefiningValues.Select(x => x.ToString())) + ")," : ",$,") +
				(mDefinedValues.Count > 0 ? "(" + string.Join(",", mDefinedValues.Select(x => x.ToString())) + ")," : "$,") +
				(string.IsNullOrEmpty(mExpression) ? "$," : "'" + ParserIfc.Encode(mExpression) + "',") +
				(mDefiningUnit == null ? "$" : "#" + mDefiningUnit.StepId) +
				(mDefinedUnit == null ? ",$,." : ",#,." + mDefinedUnit.StepId) + mCurveInterpolation.ToString() + ".";
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
			mExpression = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDefiningUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
			mDefinedUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] != '$')
				Enum.TryParse<IfcCurveInterpolationEnum>(s.Replace(".", ""), true, out mCurveInterpolation);
		}
	}
	public partial class IfcProtectiveDevice
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
	public partial class IfcProtectiveDeviceTrippingUnit  
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
	public partial class IfcProtectiveDeviceTrippingUnitType
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
	public partial class IfcProtectiveDeviceType
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
	public partial class IfcProxy
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mProxyType.ToString() + ".," + (string.IsNullOrEmpty(mTag) ? "$" : "'" + ParserIfc.Encode(mTag) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcObjectTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mProxyType);
			mTag = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcPump
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
	public partial class IfcPumpType
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
