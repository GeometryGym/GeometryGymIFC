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
	public partial class IfcImageTexture : IfcSurfaceTexture
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mUrlReference + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mUrlReference = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcIndexedColourMap : IfcPresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mMappedTo) + "," + ParserSTEP.DoubleOptionalToString(mOpacity) +"," + ParserSTEP.LinkToString(mColours) + ",(" + mColourIndex[0];
			for (int icounter = 1; icounter < mColourIndex.Count; icounter++)
				result += "," + mColourIndex[icounter];
			return result + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mMappedTo = ParserSTEP.StripLink(str, ref pos, len);
			mOpacity = ParserSTEP.StripDouble(str, ref pos, len); // Overrides : OPTIONAL IfcStrippedOptional;
			mColours = ParserSTEP.StripLink(str, ref pos, len);
			mColourIndex = ParserSTEP.SplitListSTPIntegers(ParserSTEP.StripField(str, ref pos, len));
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			MappedTo.mHasColours = this;
		}
	}
	public partial class IfcIndexedPolyCurve : IfcBoundedCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mPoints.Index + (mSegments.Count == 0 ? ",$," : ",(" + string.Join(",", mSegments) + "),") +
				(mSelfIntersect == IfcLogicalEnum.UNKNOWN ? "$" : (mSelfIntersect == IfcLogicalEnum.TRUE ? ".T." : ".F."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Points = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianPointList;
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field != "$")
			{
				List<string> strs = ParserSTEP.SplitLineFields(field.Substring(1, field.Length - 2));
				foreach (string s in strs)
				{
					if (s.ToUpper().StartsWith("IFCLINEINDEX"))
						mSegments.Add(new IfcLineIndex(ParserSTEP.SplitListSTPIntegers(s.Substring(13, s.Length - 14))));
					else
					{
						List<int> ints = ParserSTEP.SplitListSTPIntegers(s.Substring(12, s.Length - 13));
						mSegments.Add(new IfcArcIndex(ints[0], ints[1], ints[2]));
					}

				}
			}
			field = ParserSTEP.StripField(str, ref pos, len);
			if (field[0] == '.')
				mSelfIntersect = field[1] == 'T' ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE;
		}
	}
	public partial class IfcIndexedPolygonalFace : IfcTessellatedItem  //SUPERTYPE OF (ONEOF (IfcIndexedPolygonalFaceWithVoids))
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + ",(" + mCoordIndex[0];
			for (int icounter = 1; icounter < mCoordIndex.Count; icounter++)
				result += "," + mCoordIndex[icounter];
			return result + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mCoordIndex = ParserSTEP.SplitListSTPIntegers(ParserSTEP.StripField(str, ref pos, len));
		}
	}
	public partial class IfcIndexedPolygonalFaceWithVoids : IfcIndexedPolygonalFace
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			List<int> inner = mInnerCoordIndices[0];
			string result = inner[0].ToString();
			for (int icounter = 1; icounter < inner.Count; icounter++)
				result += "," + inner[icounter];
			for (int jcounter = 1; jcounter < mInnerCoordIndices.Count; jcounter++)
			{
				inner = mInnerCoordIndices[jcounter];
				result += "),(" + inner[0];
				for (int icounter = 1; icounter < inner.Count; icounter++)
					result += "," + inner[icounter];
			}
			return base.BuildStringSTEP(release) + ",((" + result + "))";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> fields = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			mInnerCoordIndices = fields.ConvertAll(x => ParserSTEP.SplitListSTPIntegers(x));
		}
	}
	public abstract partial class IfcIndexedTextureMap : IfcTextureCoordinate // ABSTRACT SUPERTYPE OF(IfcIndexedTriangleTextureMap)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mMappedTo + ",#" + mTexCoords; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mMappedTo = ParserSTEP.StripLink(str, ref pos, len);
			mTexCoords = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			MappedTo.mHasTextures.Add(this);
		}
	}
	public partial class IfcIndexedTriangleTextureMap : IfcIndexedTextureMap
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mTexCoordList.Length == 0)
				return base.BuildStringSTEP(release) + ",$";
			Tuple<int, int, int> triple = mTexCoordList[0];
			string result = base.BuildStringSTEP(release) + ",((" + triple.Item1 + "," + triple.Item2 + "," + triple.Item3;
			for (int icounter = 1; icounter < mTexCoordList.Length; icounter++)
			{
				triple = mTexCoordList[icounter];
				result += "),(" + triple.Item1 + "," + triple.Item2 + "," + triple.Item3;
			}

			return result + "))";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mTexCoordList = ParserSTEP.SplitListSTPIntTriple(ParserSTEP.StripField(str,ref pos, len)); }
	}
	public partial class IfcInterceptor : IfcFlowTreatmentDevice //IFC4  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcInterceptorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcInterceptorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcInterceptorType : IfcFlowTreatmentDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcInterceptorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcInventory : IfcGroup
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + ",." + mInventoryType + ".,#" + mJurisdiction + ",(#" + mResponsiblePersons[0];
			for (int icounter = 1; icounter < mResponsiblePersons.Count; icounter++)
				result += ",#" + mResponsiblePersons[icounter];
			return result + "),#" + mLastUpdateDate + (mCurrentValue == 0 ? ",$" : ",#" + mCurrentValue) + (mOriginalValue == 0 ? ",$" : ",#" + mOriginalValue);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcInventoryTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mInventoryType);
			mJurisdiction = ParserSTEP.StripLink(str, ref pos, len);
			mResponsiblePersons = ParserSTEP.StripListLink(str, ref pos, len);
			mLastUpdateDate = ParserSTEP.StripLink(str, ref pos, len);
			mCurrentValue = ParserSTEP.StripLink(str, ref pos, len);
			mOriginalValue = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	//ENTITY IfcIrregularTimeSeries
	//ENTITY IfcIrregularTimeSeriesValue;ductFittingTypeEnum;
	public partial class IfcIShapeProfileDef : IfcParameterizedProfileDef // Ifc2x3 SUPERTYPE OF	(IfcAsymmetricIShapeProfileDef) 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mOverallWidth) + "," + ParserSTEP.DoubleToString(mOverallDepth) + "," + ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mFlangeThickness) + "," + ParserSTEP.DoubleOptionalToString(mFilletRadius)
				+ (release <= ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.DoubleOptionalToString(mFlangeEdgeRadius) + "," + ParserSTEP.DoubleOptionalToString(mFlangeSlope));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mOverallWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mOverallDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mWebThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3) 
			{
				mFlangeEdgeRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mFlangeSlope = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	} 
}
