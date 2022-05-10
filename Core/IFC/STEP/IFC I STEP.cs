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
	public partial class IfcImageTexture
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mUrlReference + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mUrlReference = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcIndexedColourMap
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "#" + mMappedTo.StepId + "," + ParserSTEP.DoubleOptionalToString(mOpacity) + ",#" + 
				mColours.StepId + ",(" + string.Join(",", mColourIndex) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			MappedTo = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcTessellatedFaceSet;
			mOpacity = ParserSTEP.StripDouble(str, ref pos, len); // Overrides : OPTIONAL IfcStrippedOptional;
			mColours = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcColourRgbList;
			mColourIndex = ParserSTEP.SplitListSTPIntegers(ParserSTEP.StripField(str, ref pos, len));
		}
	}
	public partial class IfcIndexedPolyCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "#" + mPoints.Index + (mSegments.Count == 0 ? ",$," : ",(" + string.Join(",", mSegments) + "),") +
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
	public partial class IfcIndexedPolygonalFace
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mCoordIndex) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mCoordIndex = ParserSTEP.SplitListSTPIntegers(ParserSTEP.StripField(str, ref pos, len));
		}
	}
	public partial class IfcIndexedPolygonalFaceWithVoids
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mInnerCoordIndices.Select(x=> "(" + string.Join(",", x) + ")")) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> fields = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			mInnerCoordIndices = fields.ConvertAll(x => ParserSTEP.SplitListSTPIntegers(x));
		}
	}
	public abstract partial class IfcIndexedTextureMap
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mMappedTo + ",#" + mTexCoords; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			MappedTo = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcTessellatedFaceSet;
			TexCoords = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcTextureVertexList;
		}
	}
	public partial class IfcIndexedTriangleTextureMap
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mTexCoordList.Count == 0)
				return base.BuildStringSTEP(release) + ",$";
			return base.BuildStringSTEP(release) + ",(" + 
				string.Join(",", mTexCoordList.Select(x=> "(" + x.Item1 + "," + x.Item2 + "," + x.Item3 + ")")) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mTexCoordList.AddRange(ParserSTEP.SplitListSTPIntTriple(ParserSTEP.StripField(str,ref pos, len))); }
	}
	public partial class IfcInterceptor  
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
	public partial class IfcInterceptorType
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
	public partial class IfcInventory
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".,#" + mJurisdiction + ",(#" + mResponsiblePersons[0];
			for (int icounter = 1; icounter < mResponsiblePersons.Count; icounter++)
				result += ",#" + mResponsiblePersons[icounter];
			return result + "),#" + mLastUpdateDate + (mCurrentValue == 0 ? ",$" : ",#" + mCurrentValue) + (mOriginalValue == 0 ? ",$" : ",#" + mOriginalValue);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcInventoryTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			mJurisdiction = ParserSTEP.StripLink(str, ref pos, len);
			mResponsiblePersons = ParserSTEP.StripListLink(str, ref pos, len);
			mLastUpdateDate = ParserSTEP.StripLink(str, ref pos, len);
			mCurrentValue = ParserSTEP.StripLink(str, ref pos, len);
			mOriginalValue = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcImpactProtectionDevice
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mPredefinedType == null ? ",$" : mPredefinedType.ToString());
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			PredefinedType = IfcImpactProtectionDeviceTypeSelect.Parse(ParserSTEP.StripField(str, ref pos, len));
		}
	}
	public partial class IfcImpactProtectionDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mPredefinedType.ToString();
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			PredefinedType = IfcImpactProtectionDeviceTypeSelect.Parse(ParserSTEP.StripField(str, ref pos, len));
		}
	}
	public partial class IfcIrregularTimeSeries
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			",(#" + string.Join(",#", mValues.ConvertAll(x => x.StepId.ToString())) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Values.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcIrregularTimeSeriesValue));
		}
	}
	public partial class IfcIrregularTimeSeriesValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return IfcDateTime.STEPAttribute(mTimeStamp) + ",(#" + string.Join(",#", mListValues.ConvertAll(x => x.ToString())) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			TimeStamp = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				List<string> ss = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < ss.Count; icounter++)
				{
					IfcValue v = ParserIfc.parseValue(ss[icounter]);
					if (v != null)
						mListValues.Add(v);
				}
			}
		}
	}
	public partial class IfcIShapeProfileDef 
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
