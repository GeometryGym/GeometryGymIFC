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
	public partial class IfcCableCarrierFitting : IfcFlowFitting //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcCableCarrierFittingTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCableCarrierFittingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcCableCarrierFittingType : IfcFlowFittingType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCableCarrierFittingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCableCarrierSegment : IfcFlowSegment //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcCableCarrierSegmentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCableCarrierSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcCableCarrierSegmentType : IfcFlowSegmentType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCableCarrierSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCableFitting : IfcFlowFitting //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcCableFittingTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCableFittingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcCableFittingType : IfcFlowFittingType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCableFittingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCableSegment : IfcFlowSegment //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCableSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcCableSegmentType : IfcFlowSegmentType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCableSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCaissonFoundation : IfcDeepFoundation
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
			(mPredefinedType == IfcCaissonFoundationTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCaissonFoundationTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCaissonFoundationType : IfcDeepFoundationType
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
			",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCaissonFoundationTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCalendarDate : BaseClassIfc, IfcDateTimeSelect //DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + mDayComponent + "," + mMonthComponent + "," + mYearComponent; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mDayComponent = ParserSTEP.StripInt(str, ref pos, len);
			mMonthComponent = ParserSTEP.StripInt(str, ref pos, len);
			mYearComponent = ParserSTEP.StripInt(str, ref pos, len);
		}
	}
	public partial class IfcCartesianPoint : IfcPoint
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			Tuple<double, double, double> coordinates = SerializeCoordinates;
			string result = base.BuildStringSTEP(release) + ",(" + ParserSTEP.DoubleToString(coordinates.Item1) + "," + ParserSTEP.DoubleToString(coordinates.Item2);
			if (double.IsNaN(coordinates.Item3))
				return result + ")";
			return result + "," + ParserSTEP.DoubleToString(coordinates.Item3) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			string s = str.Trim();
			if (s[0] == '(')
			{
				string[] fields = s.Substring(1, s.Length - 2).Split(",".ToCharArray());
				if (fields != null && fields.Length > 0)
				{
					mCoordinateX = ParserSTEP.ParseDouble(fields[0]);
					if (double.IsNaN(mCoordinateX))
						mCoordinateX = 0;
					if (fields.Length > 1)
					{
						mCoordinateY = ParserSTEP.ParseDouble(fields[1]);
						if (fields.Length > 2)
							mCoordinateZ = ParserSTEP.ParseDouble(fields[2]);
					}
				}
			}
		}
	}
	public partial class IfcCartesianPointList2D : IfcCartesianPointList //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mCoordList.Select(x => "(" + formatLength(x[0]) + "," + formatLength(x[1]) + ")")) + ")" +
				(release < ReleaseVersion.IFC4X1 ? "" : (mTagList == null || mTagList.Count == 0 ? ",$" : ",(" + string.Join(",", mTagList.Select(x=>"'" + ParserIfc.Encode(x) + "'")) + ")" ));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			if(release < ReleaseVersion.IFC4X1)
				mCoordList = ParserSTEP.SplitListDoubleTuple(str); 
			else
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				mCoordList = ParserSTEP.SplitListDoubleTuple(s); 
				s = ParserSTEP.StripField(str, ref pos, len);
				if (!string.IsNullOrEmpty(s) && s != "$")
				{
					List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
					for (int icounter = 0; icounter < lst.Count; icounter++)
					{
						string field = lst[icounter];
						if (field.Length > 2)
							mTagList.Add(ParserIfc.Decode(field.Substring(1, field.Length - 2)));
					}
				}
			}
		}
	}
	public partial class IfcCartesianPointList3D : IfcCartesianPointList //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mCoordList.Select(x => "(" + formatLength(x[0]) + "," + formatLength(x[1]) + "," + formatLength(x[2]) + ")")) + ")" + 
				(release < ReleaseVersion.IFC4X1 ? "" : (mTagList == null || mTagList.Count == 0 ? ",$" : ",(" + string.Join(",", mTagList.Select(x => "'" + ParserIfc.Encode(x) + "'")) + ")")); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			if (release < ReleaseVersion.IFC4X1)
				mCoordList = ParserSTEP.SplitListDoubleTriple(str);
			else
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				mCoordList = ParserSTEP.SplitListDoubleTriple(s);
				s = ParserSTEP.StripField(str, ref pos, len);
				if (!string.IsNullOrEmpty(s) && s != "$")
				{
					List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
					for (int icounter = 0; icounter < lst.Count; icounter++)
					{
						string field = lst[icounter];
						if (field.Length > 2)
							mTagList.Add(ParserIfc.Decode(field.Substring(1, field.Length - 2)));
					}
				}
			}
		}
	}
	public abstract partial class IfcCartesianTransformationOperator : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCartesianTransformationOperator2D ,IfcCartesianTransformationOperator3D))*/
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mAxis1) + "," + ParserSTEP.LinkToString(mAxis2) + "," +
				ParserSTEP.LinkToString(mLocalOrigin) + "," + ParserSTEP.DoubleOptionalToString(mScale);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mAxis1 = ParserSTEP.StripLink(str, ref pos, len);
			mAxis2 = ParserSTEP.StripLink(str, ref pos, len);
			mLocalOrigin = ParserSTEP.StripLink(str, ref pos, len);
			Scale = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCartesianTransformationOperator2DnonUniform : IfcCartesianTransformationOperator2D
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mScale2); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Scale2 = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCartesianTransformationOperator3D : IfcCartesianTransformationOperator //SUPERTYPE OF(IfcCartesianTransformationOperator3DnonUniform)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mAxis3); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxis3 = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcCartesianTransformationOperator3DnonUniform : IfcCartesianTransformationOperator3D
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mScale2) + "," + ParserSTEP.DoubleOptionalToString(mScale3); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Scale2 = ParserSTEP.StripDouble(str, ref pos, len);
			Scale3 = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCenterLineProfileDef : IfcArbitraryOpenProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mThickness); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mThickness = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcChamferEdgeFeature : IfcEdgeFeature //DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mWidth) + "," + ParserSTEP.DoubleOptionalToString(mHeight); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mHeight = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcChiller : IfcEnergyConversionDevice //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcChillerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcChillerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcChillerType : IfcEnergyConversionDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcChillerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcChimney : IfcBuiltElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcChimneyTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcChimneyTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcChimneyType : IfcBuiltElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcChimneyTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCircle : IfcConic
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + formatLength(mRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCircleHollowProfileDef : IfcCircleProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mWallThickness < mDatabase.Tolerance ? "" : "," + formatLength(mWallThickness)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mWallThickness = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCircleProfileDef : IfcParameterizedProfileDef //SUPERTYPE OF(IfcCircleHollowProfileDef)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + formatLength(mRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCircularArcSegment2D : IfcCurveSegment2D  //IFC4.1
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + formatLength(mRadius) + "," + ParserSTEP.BoolToString(mIsCCW); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Radius = ParserSTEP.StripDouble(str, ref pos, len);
			mIsCCW = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcClassification : IfcExternalInformation, IfcClassificationReferenceSelect, IfcClassificationSelect //	SUBTYPE OF IfcExternalInformation;
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string tokens = "$";
			if (mReferenceTokens.Count > 0)
				tokens = "('" + string.Join("','", mReferenceTokens) + "')";
			bool older = mDatabase.Release <= ReleaseVersion.IFC2x3;
			string result = base.BuildStringSTEP(release) + (mSource == "$" ? (older ? ",'Unknown'," : ",$,") : ",'" + mSource + "',") +
				(mEdition == "$" ? (older ? "'Unknown'," : "$,") : "'" + mEdition + "',") + (older ? ParserSTEP.LinkToString(mEditionDateSS) : IfcDate.STEPAttribute(mEditionDate)) +
				",'" + mName + (older ? "'" : (mDescription == "$" ? "',$," : "','" + mDescription + "',") + (mLocation == "$" ? "$," : "'" + mLocation + "',") + tokens);
			return result;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mSource = ParserSTEP.StripString(str, ref pos, len);
			mEdition = ParserSTEP.StripString(str, ref pos, len);
			if (release <= ReleaseVersion.IFC2x3)
			{
				mEditionDateSS = ParserSTEP.StripLink(str, ref pos, len);
				mName = ParserSTEP.StripString(str, ref pos, len);
			}
			else
			{
				mEditionDate = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
				mName = ParserSTEP.StripString(str, ref pos, len);
				mDescription = ParserSTEP.StripString(str, ref pos, len);
				mLocation = ParserSTEP.StripString(str, ref pos, len);
				mReferenceTokens = ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len));
			}
		}
	}
	public partial class IfcClassificationItem : BaseClassIfc //DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mNotation) + "," + ParserSTEP.LinkToString(mItemOf) + "," + mTitle; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mNotation = ParserSTEP.StripLink(str, ref pos, len);
			mItemOf = ParserSTEP.StripLink(str, ref pos, len);
			mTitle = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcClassificationItemRelationship : BaseClassIfc //DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + mSource + "," + mEdition + "," + ParserSTEP.LinkToString(mEditionDate) + "," + mName; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mSource = ParserSTEP.StripString(str, ref pos, len);
			mEdition = ParserSTEP.StripString(str, ref pos, len);
			mEditionDate = ParserSTEP.StripLink(str, ref pos, len);
			mName = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcClassificationNotation : BaseClassIfc, IfcClassificationNotationSelect //DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(";
			if (mNotationFacets.Count > 0)
			{
				str += ParserSTEP.LinkToString(mNotationFacets[0]);
				for (int icounter = 1; icounter < mNotationFacets.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mNotationFacets[icounter]);
			}
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mNotationFacets = ParserSTEP.StripListLink(str, ref pos, len); }
	}
	public partial class IfcClassificationNotationFacet : BaseClassIfc  //DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + mNotationValue; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mNotationValue = ParserSTEP.StripString(str, ref pos, len); }
	}
	public partial class IfcClassificationReference : IfcExternalReference, IfcClassificationReferenceSelect, IfcClassificationSelect, IfcClassificationNotationSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			if (release < ReleaseVersion.IFC4)
				return base.BuildStringSTEP(release) + (ReferencedSource is IfcClassification ? "," + ParserSTEP.ObjToLinkString(ReferencedSource) : ",$");
			return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(ReferencedSource) + 
				(mDescription == "$" ? ",$" : ",'" + mDescription + "'") + (mSort == "$" ? ",$" : ",'" + mSort + "'"); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			ReferencedSource = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcClassificationReferenceSelect;
			if (release > ReleaseVersion.IFC2x3)
			{
				mDescription = ParserSTEP.StripString(str, ref pos, len);
				mSort = ParserSTEP.StripString(str, ref pos, len);
			}
		}
	}
	public partial class IfcClothoid
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mClothoidConstant);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			ClothoidConstant = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCoil : IfcEnergyConversionDevice //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcCoilTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCoilTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcCoilType : IfcEnergyConversionDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCoilTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcColourRgb : IfcColourSpecification, IfcColourOrFactor
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mRed) + "," + ParserSTEP.DoubleToString(mGreen) + "," + ParserSTEP.DoubleToString(mBlue); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRed = ParserSTEP.StripDouble(str, ref pos, len);
			mGreen = ParserSTEP.StripDouble(str, ref pos, len);
			mBlue = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcColourRgbList : IfcPresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			double[] t = mColourList[0];
			string result = base.BuildStringSTEP(release) + ",((" + ParserSTEP.DoubleToString(t[0]) + "," + ParserSTEP.DoubleToString(t[1]) + "," + ParserSTEP.DoubleToString(t[2]);
			for (int icounter = 1; icounter < mColourList.Length; icounter++)
			{
				t = mColourList[icounter];
				result += "),(" + ParserSTEP.DoubleToString(t[0]) + "," + ParserSTEP.DoubleToString(t[1]) + "," + ParserSTEP.DoubleToString(t[2]);
			}

			return result + "))";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mColourList = ParserSTEP.SplitListDoubleTriple(str); }
	}
	public abstract partial class IfcColourSpecification : IfcPresentationItem, IfcColour //	ABSTRACT SUPERTYPE OF(IfcColourRgb)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mName == "$" ? ",$" : ",'" + mName + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcColumn : IfcBuiltElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcColumnTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					mPredefinedType = (IfcColumnTypeEnum)Enum.Parse(typeof(IfcColumnTypeEnum), s.Substring(1, s.Length - 2));
			}
		}
	}
	public partial class IfcColumnType : IfcBuiltElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				mPredefinedType = (IfcColumnTypeEnum)Enum.Parse(typeof(IfcColumnTypeEnum), s.Substring(1, s.Length - 2));
		}
	}
	public partial class IfcComplexProperty : IfcProperty
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",'" + ParserIfc.Encode(mUsageName) + 
				(mHasProperties.Values.Count == 0 ? "',()" : "',(#" + string.Join(",#", mHasProperties.Values.Select(x=>x.StepId)) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mUsageName = ParserSTEP.StripString(str, ref pos, len);
			foreach(int i in ParserSTEP.StripListLink(str, ref pos, len))
			{
				IfcProperty property = dictionary[i] as IfcProperty;
				if (property != null)
					addProperty(property);
			}
		}
	}
	public partial class IfcComplexPropertyTemplate : IfcPropertyTemplate, NamedObjectIfc
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return "";
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mUsageName) ? ",$," : ",'" + ParserIfc.Encode(mUsageName) + "',") + (mTemplateType == IfcComplexPropertyTemplateTypeEnum.NOTDEFINED ? ",$," : ",." + mTemplateType + ".,") +
				(mHasPropertyTemplates.Count == 0 ? "$" : "(#" + string.Join(",#", mHasPropertyTemplates.Values.Select(x => x.Index)) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mUsageName = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("."))
				Enum.TryParse<IfcComplexPropertyTemplateTypeEnum>(field.Replace(".", ""), true, out mTemplateType);
			foreach (IfcPropertyTemplate propertyTemplate in ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcPropertyTemplate))
				AddPropertyTemplate(propertyTemplate);
		}
	}
	public partial class IfcCompositeCurve : IfcBoundedCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", Segments.ConvertAll(x=>"#" + x.Index)) + ")," + ParserIfc.LogicalToString(mSelfIntersect);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			if(!(release == ReleaseVersion.IFC4X3_RC2 && (this is IfcGradientCurve || this is IfcSegmentedReferenceCurve)))
				Segments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcSegment));
			mSelfIntersect = ParserIfc.StripLogical(str, ref pos, len);
		}
	}
	public partial class IfcCompositeCurveOnSurface : IfcCompositeCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mBasisSurface; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mBasisSurface = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcCompositeCurveSegment : IfcSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.BoolToString(mSameSense) + ",#" + mParentCurve.StepId; }

		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSameSense = ParserSTEP.StripBool(str, ref pos, len);
			mParentCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
		}
	}
	public partial class IfcCompositeProfileDef : IfcProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(#" + String.Join(",#", mProfiles.Select(x=>x.StepId)) + (mLabel == "$" ? "),$" : "),'" + mLabel + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mProfiles.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcProfileDef));
			mLabel = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcCompressor : IfcFlowMovingDevice //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcCompressorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCompressorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcCompressorType : IfcFlowMovingDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCompressorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCommunicationsAppliance : IfcFlowTerminal //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcCommunicationsApplianceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCommunicationsApplianceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcCommunicationsApplianceType : IfcFlowTerminalType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCommunicationsApplianceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCondenser : IfcEnergyConversionDevice //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcCondenserTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCondenserTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcCondenserType : IfcEnergyConversionDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCondenserTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcConditionCriterion : IfcControl //DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mCriterion) + "," + ParserSTEP.LinkToString(mCriterionDateTime); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mCriterion = ParserSTEP.StripLink(str, ref pos, len);
			mCriterionDateTime = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public abstract partial class IfcConic : IfcCurve /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCircle ,IfcEllipse))*/
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mPosition); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mPosition = ParserSTEP.StripLink(str, ref pos, len); }
	}
	public partial class IfcConnectedFaceSet : IfcTopologicalRepresentationItem //SUPERTYPE OF (ONEOF (IfcClosedShell ,IfcOpenShell))
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mCfsFaces.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", CfsFaces.ConvertAll(x => x.Index)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { CfsFaces.AddRange(ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)).ConvertAll(x => dictionary[x] as IfcFace)); }
	}
	public partial class IfcConnectionCurveGeometry : IfcConnectionGeometry
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mCurveOnRelatingElement) + "," + ParserSTEP.LinkToString(mCurveOnRelatedElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mCurveOnRelatingElement = ParserSTEP.StripLink(str, ref pos, len);
			mCurveOnRelatedElement = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcConnectionPointEccentricity : IfcConnectionPointGeometry
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mEccentricityInX) + "," + ParserSTEP.DoubleOptionalToString(mEccentricityInY) + "," + ParserSTEP.DoubleOptionalToString(mEccentricityInZ); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mEccentricityInX = ParserSTEP.StripDouble(str, ref pos, len);
			mEccentricityInY = ParserSTEP.StripDouble(str, ref pos, len);
			mEccentricityInZ = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcConnectionPointGeometry : IfcConnectionGeometry
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mPointOnRelatingElement) + "," + ParserSTEP.LinkToString(mPointOnRelatedElement); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mPointOnRelatingElement = ParserSTEP.StripLink(str, ref pos, len);
			mPointOnRelatedElement = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	//ENTITY IfcConnectionPortGeometry  // DEPRECATED IFC4
	public partial class IfcConnectionSurfaceGeometry : IfcConnectionGeometry
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mSurfaceOnRelatingElement) + "," + ParserSTEP.LinkToString(mSurfaceOnRelatedElement); }

		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mSurfaceOnRelatingElement = ParserSTEP.StripLink(str, ref pos, len);
			mSurfaceOnRelatedElement = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcConnectionVolumeGeometry : IfcConnectionGeometry
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
			",#" + mVolumeOnRelatingElement.StepId +
			(mVolumeOnRelatedElement == null ? ",$" : ",#" + mVolumeOnRelatedElement.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			VolumeOnRelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSolidOrShell;
			VolumeOnRelatedElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSolidOrShell;
		}
	}
	public abstract partial class IfcConstraint : BaseClassIfc, IfcResourceObjectSelect //IFC4Change ABSTRACT SUPERTYPE OF(ONEOF(IfcMetric, IfcObjective));
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mName + (mDescription == "$" ? "',$,." : "','" + mDescription + "',.") + mConstraintGrade.ToString() + (mConstraintSource == "$" ? ".,$," : ".,'" + mConstraintSource + "',") + ParserSTEP.LinkToString(mCreatingActor) + "," + (release < ReleaseVersion.IFC4 ? ParserSTEP.LinkToString(mSSCreationTime) : (mCreationTime == "$" ? "$" : "'" + mCreationTime + "'")) + (mUserDefinedGrade == "$" ? ",$" : ",'" + mUserDefinedGrade + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			Enum.TryParse<IfcConstraintEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mConstraintGrade);
			mConstraintSource = ParserSTEP.StripString(str, ref pos, len);
			mCreatingActor = ParserSTEP.StripLink(str, ref pos, len);
			if (release < ReleaseVersion.IFC4)
				mSSCreationTime = ParserSTEP.StripLink(str, ref pos, len);
			else
				mCreationTime = ParserSTEP.StripString(str, ref pos, len);
			mUserDefinedGrade = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	//ENTITY IfcConstraintAggregationRelationship; // DEPRECATED IFC4
	//ENTITY IfcConstraintclassificationRelationship; // DEPRECATED IFC4
	//ENTITY IfcConstraintRelationship; // DEPRECATED IFC4
	//ENTITY IfcConstructionResource
	public partial class IfcConstructionEquipmentResource : IfcConstructionResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mDatabase.Release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcConstructionEquipmentResourceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcConstructionEquipmentResourceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcConstructionEquipmentResourceType : IfcConstructionResourceType //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcConstructionEquipmentResourceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcConstructionMaterialResource : IfcConstructionResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			//if ifc2x3
			//else
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcConstructionMaterialResourceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
			{

			}
			else
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcConstructionMaterialResourceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcConstructionMaterialResourceType : IfcConstructionResourceType //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcConstructionMaterialResourceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcConstructionProductResource : IfcConstructionResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcConstructionProductResourceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcConstructionProductResourceType : IfcConstructionResourceType //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcConstructionProductResourceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcConstructionResource : IfcResource //ABSTRACT SUPERTYPE OF (ONEOF(IfcConstructionEquipmentResource, IfcConstructionMaterialResource, IfcConstructionProductResource, IfcCrewResource, IfcLaborResource, IfcSubContractResource))
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mIdentification) ? ",$," : ",'" + ParserIfc.Encode(mIdentification) + "',") +
					(string.IsNullOrEmpty(mResourceGroup) ? "$," : "'" + ParserIfc.Encode(mResourceGroup) + "',") + ParserSTEP.ObjToLinkString(mBaseQuantitySS);
			return base.BuildStringSTEP(release) + (mUsage == 0 ? ",$," : ",#" + mUsage + ",") + ParserSTEP.ListLinksToString(mBaseCosts) + "," + ParserSTEP.ObjToLinkString(mBaseQuantity);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);

			if (release < ReleaseVersion.IFC4)
			{
				mIdentification = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
				mResourceGroup = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
				mBaseQuantitySS = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMeasureWithUnit;
			}
			else
			{
				mUsage = ParserSTEP.StripLink(str, ref pos, len);
				mBaseCosts = ParserSTEP.StripListLink(str, ref pos, len);
				mBaseQuantity = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPhysicalQuantity;
			}
		}
	}
	public abstract partial class IfcConstructionResourceType : IfcTypeResource //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + "," + ParserSTEP.ListLinksToString(mBaseCosts) + (mBaseQuantity == 0 ? ",$" : ",#" + mBaseQuantity)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mBaseCosts = ParserSTEP.StripListLink(str, ref pos, len);
			mBaseQuantity = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public abstract partial class IfcContext : IfcObjectDefinition//(IfcProject, IfcProjectLibrary)
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mObjectType == "$" ? ",$" : ",'" + mObjectType + "'") + (mLongName == "$" ? ",$" : ",'" + mLongName + "'") + (mPhase == "$" ? ",$" : ",'" + mPhase + "'") +
				(mRepresentationContexts.Count == 0 ? ",$," : ",(#" + string.Join(",#", mRepresentationContexts.ConvertAll(x => x.Index)) + "),") + (mUnitsInContext == 0 ? "$" : ParserSTEP.LinkToString(mUnitsInContext));

		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mObjectType = ParserSTEP.StripString(str, ref pos, len);
			mLongName = ParserSTEP.StripString(str, ref pos, len);
			mPhase = ParserSTEP.StripString(str, ref pos, len);
			RepresentationContexts.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcRepresentationContext));
			mUnitsInContext = ParserSTEP.StripLink(str, ref pos, len);

			if (mDatabase.mContext == null || !(this is IfcProjectLibrary))
				mDatabase.mContext = this;
		}
	}
	public partial class IfcContextDependentUnit : IfcNamedUnit
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",'" + ParserIfc.Encode(mName) + "'";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Name = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public abstract partial class IfcControl : IfcObject //ABSTRACT SUPERTYPE OF (ONEOF (IfcActionRequest ,IfcConditionCriterion ,IfcCostItem ,IfcCostSchedule,IfcEquipmentStandard ,IfcFurnitureStandard
	{ //  ,IfcPerformanceHistory ,IfcPermit ,IfcProjectOrder ,IfcProjectOrderRecord ,IfcScheduleTimeControl ,IfcServiceLife ,IfcSpaceProgram ,IfcTimeSeriesSchedule,IfcWorkControl))
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mIdentification == "$" ? ",$" : ",'" + mIdentification + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
				mIdentification = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcController : IfcDistributionControlElement //IFC4  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcControllerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcControllerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcControllerType : IfcDistributionControlElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcControllerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcConversionBasedUnit : IfcNamedUnit, IfcResourceObjectSelect //	SUPERTYPE OF(IfcConversionBasedUnitWithOffset)
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mName + "'," + ParserSTEP.LinkToString(mConversionFactor); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mName = ParserSTEP.StripString(str, ref pos, len);
			mConversionFactor = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcConversionBasedUnitWithOffset : IfcConversionBasedUnit //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mConversionOffset); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mConversionOffset = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcConveyorSegment : IfcFlowSegment
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mPredefinedType == IfcConveyorSegmentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcConveyorSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcConveyorSegmentType : IfcFlowSegmentType
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcConveyorSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCooledBeam : IfcEnergyConversionDevice //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcCooledBeamTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCooledBeamTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCooledBeamType : IfcEnergyConversionDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCooledBeamTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCoolingTower : IfcEnergyConversionDevice //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcCoolingTowerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCoolingTowerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCoolingTowerType : IfcEnergyConversionDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCoolingTowerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCoordinatedUniversalTimeOffset : BaseClassIfc //DEPRECATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + mHourOffset + "," + mMinuteOffset + ",." + mSense.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mHourOffset = ParserSTEP.StripInt(str, ref pos, len);
			mMinuteOffset = ParserSTEP.StripInt(str, ref pos, len);
			Enum.TryParse<IfcAheadOrBehind>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mSense);
		}
	}
	public abstract partial class IfcCoordinateOperation : BaseClassIfc // IFC4 	ABSTRACT SUPERTYPE OF(IfcMapConversion);
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mSourceCRS.StepId + ",#" + mTargetCRS.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			SourceCRS = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCoordinateReferenceSystemSelect;
			TargetCRS = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCoordinateReferenceSystem;
		}
	}
	public abstract partial class IfcCoordinateReferenceSystem : BaseClassIfc, IfcCoordinateReferenceSystemSelect  // IFC4 	ABSTRACT SUPERTYPE OF(IfcProjectedCRS);
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,'" : "'" + mDescription + "','") +
				mGeodeticDatum + (mVerticalDatum == "$" ? "',$" : "','" + mVerticalDatum + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mGeodeticDatum = ParserSTEP.StripString(str, ref pos, len);
			mVerticalDatum = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcCosine
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ParserSTEP.DoubleToString(mCosineTerm) + "," + ParserSTEP.DoubleToString(mConstant);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			CosineTerm = ParserSTEP.StripDouble(str, ref pos, len);
			Constant = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCostItem : IfcControl
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string s = base.BuildStringSTEP(release);
			if (release != ReleaseVersion.IFC2x3)
			{
				s += ",." + mPredefinedType.ToString();
				if (mCostValues.Count == 0)
					s += ".,$,";
				else
				{
					s += ".,(" + ParserSTEP.LinkToString(mCostValues[0]);
					for (int icounter = 1; icounter < mCostValues.Count; icounter++)
						s += "," + ParserSTEP.LinkToString(mCostValues[icounter]);
					s += "),";
				}
				if (mCostQuantities.Count == 0)
					s += "$";
				else
				{
					s += "(" + ParserSTEP.LinkToString(mCostQuantities[0]);
					for (int icounter = 1; icounter < mCostQuantities.Count; icounter++)
						s += "," + ParserSTEP.LinkToString(mCostQuantities[icounter]);
					s += ")";
				}
			}
			return s;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCostItemTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
				mCostValues = ParserSTEP.StripListLink(str, ref pos, len);
				mCostQuantities = ParserSTEP.StripListLink(str, ref pos, len);
			}
		}
	}
	public partial class IfcCostSchedule : IfcControl
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
			{
				string str = base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mSubmittedBy) + "," + ParserSTEP.LinkToString(mPreparedBy) + "," + ParserSTEP.LinkToString(mSubmittedOnSS) + "," + mStatus;
				if (mTargetUsers.Count > 0)
				{
					str += ",(" + ParserSTEP.LinkToString(mTargetUsers[0]);
					for (int icounter = 1; icounter < mTargetUsers.Count; icounter++)
						str += "," + ParserSTEP.LinkToString(mTargetUsers[icounter]);
					str += "),";
				}
				else
					str += ",$,";
				return str + ParserSTEP.LinkToString(mUpdateDateSS) + (mIdentification == "$" ? ",$,." : ",'" + mIdentification + "',.") + mPredefinedType.ToString() + ".";
			}
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + (mStatus == "$" ? ".,$," : ".,'" + mStatus + "',") + mSubmittedOn + "," + mUpdateDate;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
			{
				mSubmittedBy = ParserSTEP.StripLink(str, ref pos, len);
				mPreparedBy = ParserSTEP.StripLink(str, ref pos, len);
				mSubmittedOnSS = ParserSTEP.StripLink(str, ref pos, len);
				mStatus = ParserSTEP.StripString(str, ref pos, len);
				mTargetUsers = ParserSTEP.StripListLink(str, ref pos, len);
				mUpdateDateSS = ParserSTEP.StripLink(str, ref pos, len);
				mIdentification = ParserSTEP.StripString(str, ref pos, len);
				Enum.TryParse<IfcCostScheduleTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
			}
			else
			{
				Enum.TryParse<IfcCostScheduleTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
				mStatus = ParserSTEP.StripString(str, ref pos, len);
				mSubmittedOn = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
				mUpdateDate = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			}
		}
	}
	public partial class IfcCostValue : IfcAppliedValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? (mCategory == "$" ? ",$," : ",'" + mCategory + "',") + (mCondition == "$" ? "$" : "'" + mCondition + "'") : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
			{
				mCategory = ParserSTEP.StripString(str, ref pos, len);
				mCondition = ParserSTEP.StripString(str, ref pos, len);
			}
		}
	}
	public partial class IfcCourse : IfcBuiltElement
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mPredefinedType == IfcCourseTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCourseTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCourseType : IfcBuiltElementType
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCourseTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCovering : IfcBuiltElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + (mPredefinedType == IfcCoveringTypeEnum.NOTDEFINED ? "$" : "." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCoveringTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcCoveringType : IfcBuiltElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCoveringTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCraneRailAShapeProfileDef : IfcParameterizedProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mOverallHeight) + "," + ParserSTEP.DoubleToString(mBaseWidth2) + "," + ParserSTEP.DoubleOptionalToString(mRadius) + "," + ParserSTEP.DoubleToString(mHeadWidth) + "," + ParserSTEP.DoubleToString(mHeadDepth2) + "," + ParserSTEP.DoubleToString(mHeadDepth3) + "," + ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mBaseDepth1) + "," + ParserSTEP.DoubleToString(mBaseDepth2) + "," + ParserSTEP.DoubleToString(mBaseDepth3) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mOverallHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mBaseWidth2 = ParserSTEP.StripDouble(str, ref pos, len);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mHeadWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mHeadDepth2 = ParserSTEP.StripDouble(str, ref pos, len);
			mHeadDepth3 = ParserSTEP.StripDouble(str, ref pos, len);
			mWebThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mBaseWidth4 = ParserSTEP.StripDouble(str, ref pos, len);
			mBaseDepth1 = ParserSTEP.StripDouble(str, ref pos, len);
			mBaseDepth2 = ParserSTEP.StripDouble(str, ref pos, len);
			mBaseDepth3 = ParserSTEP.StripDouble(str, ref pos, len);
			mCentreOfGravityInY = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCraneRailFShapeProfileDef : IfcParameterizedProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mOverallHeight) + "," + ParserSTEP.DoubleToString(mHeadWidth) + "," + ParserSTEP.DoubleOptionalToString(mRadius) + "," + ParserSTEP.DoubleToString(mHeadDepth2) + "," + ParserSTEP.DoubleToString(mHeadDepth3) + "," + ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mBaseDepth1) + "," + ParserSTEP.DoubleToString(mBaseDepth2) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mOverallHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mHeadWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mHeadDepth2 = ParserSTEP.StripDouble(str, ref pos, len);
			mHeadDepth3 = ParserSTEP.StripDouble(str, ref pos, len);
			mWebThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mBaseDepth1 = ParserSTEP.StripDouble(str, ref pos, len);
			mBaseDepth2 = ParserSTEP.StripDouble(str, ref pos, len);
			mCentreOfGravityInY = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCrewResource : IfcConstructionResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCrewResourceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcCrewResourceType : IfcConstructionResourceType //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCrewResourceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcCsgPrimitive3D : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBlock ,IfcRectangularPyramid ,IfcRightCircularCone ,IfcRightCircularCylinder ,IfcSphere))*/
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mPosition); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mPosition = ParserSTEP.StripLink(str, ref pos, len); }
	}
	public partial class IfcCsgSolid : IfcSolidModel
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mTreeRootExpression); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mTreeRootExpression = ParserSTEP.StripLink(str, ref pos, len); }
	}
	public partial class IfcCShapeProfileDef : IfcParameterizedProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mDepth) + "," + ParserSTEP.DoubleToString(mWidth) + "," + ParserSTEP.DoubleToString(mWallThickness) + "," + ParserSTEP.DoubleToString(mGirth) + "," + ParserSTEP.DoubleOptionalToString(mInternalFilletRadius) + (release < ReleaseVersion.IFC4 ? "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInX) : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mWallThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mGirth = ParserSTEP.StripDouble(str, ref pos, len);
			mInternalFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
			if (release < ReleaseVersion.IFC4)
				mCentreOfGravityInX = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCurrencyRelationship : IfcResourceLevelRelationship
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +
			",#" + mRelatingMonetaryUnit.StepId +
			",#" + mRelatedMonetaryUnit.StepId + "," +
			ParserSTEP.DoubleOptionalToString(mExchangeRate) + "," +
			IfcDateTime.STEPAttribute(mRateDateTime) +
			(mRateSource == null ? ",$" : ",#" + mRateSource.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingMonetaryUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMonetaryUnit;
			RelatedMonetaryUnit = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMonetaryUnit;
			ExchangeRate = ParserSTEP.StripDouble(str, ref pos, len);
			RateDateTime = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
			RateSource = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcLibraryInformation;
		}
	}
	public partial class IfcCurtainWall : IfcBuiltElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? base.BuildStringSTEP(release) : base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcCurtainWallTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcCurtainWallType : IfcBuiltElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCurtainWallTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCurveBoundedPlane : IfcBoundedSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = ",(";
			if (mInnerBoundaries.Count > 0)
			{
				str += "#" + mInnerBoundaries[0];
				for (int icounter = 1; icounter < mInnerBoundaries.Count; icounter++)
					str += ",#" + mInnerBoundaries[icounter];
			}
			return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.LinkToString(mOuterBoundary) + (mInnerBoundaries.Count > 0 || release < ReleaseVersion.IFC4 ? str + ")" : ",$");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mBasisSurface = ParserSTEP.StripLink(str, ref pos, len);
			mOuterBoundary = ParserSTEP.StripLink(str, ref pos, len);
			mInnerBoundaries = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcCurveBoundedSurface : IfcBoundedSurface //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.ListLinksToString(mBoundaries) + (mImplicitOuter ? ",.T." : ",.F."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mBasisSurface = ParserSTEP.StripLink(str, ref pos, len);
			mBoundaries = ParserSTEP.StripListLink(str, ref pos, len);
			mImplicitOuter = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcCurveSegment : IfcSegment
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",#" + mPlacement.StepId + "," + mSegmentStart.ToString() + "," +
				mSegmentLength.ToString() + ",#" + mParentCurve.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Placement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPlacement;
			SegmentStart = ParserIfc.parseMeasureValue(ParserSTEP.StripField(str, ref pos, len)) as IfcCurveMeasureSelect;
			SegmentLength = ParserIfc.parseMeasureValue(ParserSTEP.StripField(str, ref pos, len)) as IfcCurveMeasureSelect;
			ParentCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
		}
	}
	public abstract partial class IfcCurveSegment2D : IfcBoundedCurve
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",#" + mStartPoint.mIndex + 
				"," + ParserSTEP.DoubleToString(mStartDirection) + "," + formatLength(mSegmentLength); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mStartPoint = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianPoint;
			mStartDirection = ParserSTEP.StripDouble(str, ref pos, len);
			mSegmentLength = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCurveStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mCurveFont) + (mCurveWidth == null ? ",$," : "," + mCurveWidth.ToString() + ",") + ParserSTEP.LinkToString(mCurveColour) + (release != ReleaseVersion.IFC2x3 ? (mModelOrDraughting == IfcLogicalEnum.UNKNOWN ? ",$" : ","+ ParserIfc.LogicalToString(mModelOrDraughting)) :"") ; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mCurveFont = ParserSTEP.StripLink(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if(str != "$")
				mCurveWidth = ParserIfc.parseValue(str) as IfcSizeSelect;
			mCurveColour = ParserSTEP.StripLink(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
				mModelOrDraughting = ParserIfc.StripLogical(str, ref pos, len);
		}
	}
	public partial class IfcCurveStyleFont : IfcPresentationItem, IfcCurveStyleFontSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mName == "$" ? ",$,(" : ",'" + mName + "',(") +
				string.Join(",", mPatternList.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mPatternList.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcCurveStyleFontPattern));
		}
	}
	public partial class IfcCurveStyleFontAndScaling : IfcPresentationItem, IfcCurveFontOrScaledCurveFontSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return ",'" + mName + "'," + ParserSTEP.LinkToString(mCurveFont) + "," + mCurveFontScaling.ToString(); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mCurveFont = ParserSTEP.StripLink(str, ref pos, len);
			mCurveFontScaling = new IfcPositiveRatioMeasure(ParserSTEP.StripField(str, ref pos, len));
		}
	}
	public partial class IfcCurveStyleFontPattern : IfcPresentationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mVisibleSegmentLength) + "," + ParserSTEP.DoubleToString(mInvisibleSegmentLength); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mVisibleSegmentLength = ParserSTEP.StripDouble(str, ref pos, len);
			mInvisibleSegmentLength = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCylindricalSurface : IfcElementarySurface //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return  base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
}
