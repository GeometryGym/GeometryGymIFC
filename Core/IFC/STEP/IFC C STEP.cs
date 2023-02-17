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
	public partial class IfcCableCarrierFitting
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
	public partial class IfcCableCarrierFittingType
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
	public partial class IfcCableCarrierSegment
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
	public partial class IfcCableCarrierSegmentType
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
	public partial class IfcCableFitting
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
	public partial class IfcCableFittingType
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
	public partial class IfcCableSegment
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
	public partial class IfcCableSegmentType
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
	public partial class IfcCaissonFoundation
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
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
	public partial class IfcCaissonFoundationType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
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
	public partial class IfcCalendarDate
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return mDayComponent + "," + mMonthComponent + "," + mYearComponent; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mDayComponent = ParserSTEP.StripInt(str, ref pos, len);
			mMonthComponent = ParserSTEP.StripInt(str, ref pos, len);
			mYearComponent = ParserSTEP.StripInt(str, ref pos, len);
		}
	}
	public partial class IfcCartesianPoint
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			Tuple<double, double, double> coordinates = SerializeCoordinates;
			string result = "(" + ParserSTEP.DoubleToString(coordinates.Item1) + "," + ParserSTEP.DoubleToString(coordinates.Item2);
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
	public partial class IfcCartesianPointList2D
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mCoordList.Select(x => "(" + formatLength(x.Item1) + "," + formatLength(x.Item2) + ")")) + ")" +
				(release < ReleaseVersion.IFC4X1 ? "" : (mTagList == null || mTagList.Count == 0 ? ",$" : ",(" + string.Join(",", mTagList.Select(x=> (string.IsNullOrEmpty(x) ? "$" : "'" + ParserSTEP.Encode(x) + "'"))) + ")" ));
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
							mTagList.Add(ParserSTEP.Decode(field.Substring(1, field.Length - 2)));
						else
							mTagList.Add(null);
					}
				}
			}
		}
	}
	public partial class IfcCartesianPointList3D
	{
		internal override void WriteStepLine(TextWriter textWriter, ReleaseVersion release)
		{
			WriteStepLineWorkerPrefix(textWriter);

			var first = mCoordList[0];

			textWriter.Write("((");
			textWriter.Write(formatLength(first.Item1));
			textWriter.Write(",");
			textWriter.Write(formatLength(first.Item2));
			textWriter.Write(",");
			textWriter.Write(formatLength(first.Item3));

			foreach (var coord in mCoordList.Skip(1))
			{
				textWriter.Write("),(");
				textWriter.Write(formatLength(coord.Item1));
				textWriter.Write(",");
				textWriter.Write(formatLength(coord.Item2));
				textWriter.Write(",");
				textWriter.Write(formatLength(coord.Item3));
			}
			textWriter.Write("))");
			
			if(release >= ReleaseVersion.IFC4X1)
				textWriter.Write(mTagList == null || mTagList.Count == 0 ? ",$" : ",(" + string.Join(",", mTagList.Select(x => (string.IsNullOrEmpty(x) ? "$" : "'" + ParserSTEP.Encode(x) + "'"))) + ")");

			WriteStepLineWorkerSuffix(textWriter);
		}
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			StringBuilder stringBuilder = new StringBuilder();
			var first = mCoordList[0];
			stringBuilder.Append("((");
			stringBuilder.Append(formatLength(first.Item1));
			stringBuilder.Append(",");
			stringBuilder.Append(formatLength(first.Item2));
			stringBuilder.Append(",");
			stringBuilder.Append(formatLength(first.Item3));

			foreach (var coord in mCoordList.Skip(1))
			{
				stringBuilder.Append("),(");
				stringBuilder.Append(formatLength(coord.Item1));
				stringBuilder.Append(",");
				stringBuilder.Append(formatLength(coord.Item2));
				stringBuilder.Append(",");
				stringBuilder.Append(formatLength(coord.Item3));
			}
			stringBuilder.Append("))");
			return stringBuilder.ToString() + 
				(release < ReleaseVersion.IFC4X1 ? "" : (mTagList == null || mTagList.Count == 0 ? ",$" : ",(" + string.Join(",", mTagList.Select(x => (string.IsNullOrEmpty(x) ? "$" : "'" + ParserSTEP.Encode(x) + "'"))) + ")")); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			if (release < ReleaseVersion.IFC4X1)
				mCoordList.AddRange(ParserSTEP.SplitListDoubleTriple(str));
			else
			{
				mCoordList.AddRange(ParserSTEP.StripListTripleDouble(str, ref pos, len));
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (!string.IsNullOrEmpty(s) && s != "$")
				{
					List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
					for (int icounter = 0; icounter < lst.Count; icounter++)
					{
						string field = lst[icounter];
						if (field.Length > 2)
							mTagList.Add(ParserSTEP.Decode(field.Substring(1, field.Length - 2)));
						else
							mTagList.Add(null);
					}
				}
			}
		}
	}
	public abstract partial class IfcCartesianTransformationOperator
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (mAxis1 == null ? "$" : "#" + mAxis1.StepId) + (mAxis2 == null ? ",$" : ",#" + mAxis2.StepId) + ",#" + mLocalOrigin.StepId + "," + ParserSTEP.DoubleOptionalToString(mScale);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mAxis1 = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
			mAxis2 = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
			mLocalOrigin = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianPoint;
			Scale = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCartesianTransformationOperator2DnonUniform
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mScale2); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Scale2 = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCartesianTransformationOperator3D
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mAxis3 == null ? ",$" : ",#" + mAxis3.StepId); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxis3 = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
		}
	}
	public partial class IfcCartesianTransformationOperator3DnonUniform
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mScale2) + "," + ParserSTEP.DoubleOptionalToString(mScale3); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Scale2 = ParserSTEP.StripDouble(str, ref pos, len);
			Scale3 = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCenterLineProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mThickness); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mThickness = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcChamferEdgeFeature
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mWidth) + "," + ParserSTEP.DoubleOptionalToString(mHeight); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mHeight = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcChiller
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
	public partial class IfcChillerType
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
	public partial class IfcChimney
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
	public partial class IfcChimneyType
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
	public partial class IfcCircle
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + formatLength(mRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCircleHollowProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mWallThickness < mDatabase.Tolerance ? "" : "," + formatLength(mWallThickness)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mWallThickness = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCircleProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + formatLength(mRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCircularArcSegment2D
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + formatLength(mRadius) + "," + ParserSTEP.BoolToString(mIsCCW); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Radius = ParserSTEP.StripDouble(str, ref pos, len);
			mIsCCW = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcClassification
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string tokens = "$";
			if (mReferenceTokens.Count > 0)
				tokens = "('" + string.Join("','", mReferenceTokens) + "')";
			bool older = mDatabase.Release <= ReleaseVersion.IFC2x3;
			return (string.IsNullOrEmpty(mSource) ? (older ? "'Unknown'," : "$,") : "'" + ParserSTEP.Encode(mSource) + "',") +
				(string.IsNullOrEmpty(mEdition) ? (older ? "'Unknown'," : "$,") : "'" + ParserSTEP.Encode(mEdition) + "',") + 
				(older ? (mEditionDateSS == null ? "$" : "#" + mEditionDateSS.StepId) : IfcDate.STEPAttribute(mEditionDate)) + ",'" + 
				ParserSTEP.Encode(mName) + (older ? "'" : (string.IsNullOrEmpty(mDescription) ? "',$," : "','" + ParserSTEP.Encode(mDescription) + "',") + (string.IsNullOrEmpty(mSpecification) ? "$," : "'" + ParserSTEP.Encode(mSpecification) + "',") + tokens);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mSource = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mEdition = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			if (release <= ReleaseVersion.IFC2x3)
			{
				mEditionDateSS = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCalendarDate;
				mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			}
			else
			{
				mEditionDate = IfcDateTime.ParseSTEP(ParserSTEP.StripField(str, ref pos, len));
				mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mSpecification = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mReferenceTokens = ParserSTEP.SplitListStrings(ParserSTEP.StripField(str, ref pos, len));
			}
		}
	}
	public partial class IfcClassificationItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mNotation.StepId + ",#" + mItemOf.StepId + ",'" + ParserSTEP.Encode(mTitle) + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mNotation = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcClassificationNotationFacet;
			mItemOf = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcClassification;
			mTitle = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcClassificationItemRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return mSource + "," + mEdition + (mEditionDate == null ? ",$,'" : ",#" + mEditionDate.StepId + ",'") + ParserSTEP.Encode(mName) + "'"; 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mSource = ParserSTEP.StripString(str, ref pos, len);
			mEdition = ParserSTEP.StripString(str, ref pos, len);
			mEditionDate = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCalendarDate;
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcClassificationNotation
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mNotationFacets.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mNotationFacets.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcClassificationNotationFacet)); }
	}
	public partial class IfcClassificationNotationFacet
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "'" + ParserSTEP.Encode( mNotationValue) + "'"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mNotationValue = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len)); }
	}
	public partial class IfcClassificationReference
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			if (release < ReleaseVersion.IFC4)
				return base.BuildStringSTEP(release) + (ReferencedSource is IfcClassification ? "," + ParserSTEP.ObjToLinkString(ReferencedSource) : ",$");
			return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(ReferencedSource) + 
				(string.IsNullOrEmpty(mDescription) ? ",$" : ",'" + ParserSTEP.Encode(mDescription) + "'") + (string.IsNullOrEmpty(mSort) ? ",$" : ",'" + ParserSTEP.Encode(mSort) + "'"); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			ReferencedSource = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcClassificationReferenceSelect;
			if (release > ReleaseVersion.IFC2x3)
			{
				mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mSort = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			}
		}
	}
	public partial class IfcClothoid
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mClothoidConstant);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			ClothoidConstant = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCoil
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
	public partial class IfcCoilType
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
	public partial class IfcColourRgb
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
	public partial class IfcColourRgbList
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + 
				string.Join(",", mColourList.Select(x=> "(" + ParserSTEP.DoubleToString(x.Item1) + "," + ParserSTEP.DoubleToString(x.Item2) + "," + ParserSTEP.DoubleToString(x.Item3) + ")")) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) 
		{
			mColourList.AddRange(ParserSTEP.SplitListDoubleTriple(str));
		}
	}
	public abstract partial class IfcColourSpecification
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (string.IsNullOrEmpty(mName) ? "$" : "'" + ParserSTEP.Encode(mName) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcColumn
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcColumnTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcColumnTypeEnum>(s.Substring(1, s.Length - 2), out mPredefinedType);
			}
		}
	}
	public partial class IfcColumnType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcColumnTypeEnum>(s.Substring(1, s.Length - 2), out mPredefinedType);
		}
	}
	public partial class IfcComplexProperty
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",'" + ParserSTEP.Encode(mUsageName) + "',(" + 
				string.Join(",", mHasProperties.Values.OrderBy(x=>x.StepId).Select(x=>"#" + x.StepId)) + ")";
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
	public partial class IfcComplexPropertyTemplate
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return "";
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mUsageName) ? ",$," : ",'" + ParserSTEP.Encode(mUsageName) + "',") + (mTemplateType == IfcComplexPropertyTemplateTypeEnum.NOTDEFINED ? ",$," : ",." + mTemplateType + ".,") +
				(mHasPropertyTemplates.Count == 0 ? "$" : "(" + string.Join(",", mHasPropertyTemplates.Values.Select(x => "#" + x.StepId)) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mUsageName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("."))
				Enum.TryParse<IfcComplexPropertyTemplateTypeEnum>(field.Replace(".", ""), true, out mTemplateType);
			foreach (IfcPropertyTemplate propertyTemplate in ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcPropertyTemplate))
				AddPropertyTemplate(propertyTemplate);
		}
	}
	public partial class IfcCompositeCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", Segments.ConvertAll(x=>"#" + x.StepId)) + ")," + ParserIfc.LogicalToString(mSelfIntersect);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			if(!(release == ReleaseVersion.IFC4X3_RC2 && (this is IfcGradientCurve || this is IfcSegmentedReferenceCurve)))
				Segments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcSegment));
			mSelfIntersect = ParserIfc.StripLogical(str, ref pos, len);
		}
	}
	public partial class IfcCompositeCurveOnSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mBasisSurface.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mBasisSurface = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSurface;
		}
	}
	public partial class IfcCompositeCurveSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.BoolToString(mSameSense) + ",#" + mParentCurve.StepId; }

		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mSameSense = ParserSTEP.StripBool(str, ref pos, len);
			mParentCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
		}
	}
	public partial class IfcCompositeProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(#" + String.Join(",#", mProfiles.Select(x=>x.StepId)) + (string.IsNullOrEmpty(mLabel) ? "),$" : "),'" + ParserSTEP.Encode(mLabel) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mProfiles.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcProfileDef));
			mLabel = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcCompressor
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
	public partial class IfcCompressorType
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
	public partial class IfcCommunicationsAppliance
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
	public partial class IfcCommunicationsApplianceType
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
	public partial class IfcCondenser
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
	public partial class IfcCondenserType
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
	public partial class IfcConditionCriterion
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string criterion = "$";
			if (mCriterion is IfcLabel label)
				criterion = label.ToString();
			else if (mCriterion is BaseClassIfc o)
				criterion = "#" + o.StepId;
			return base.BuildStringSTEP(release) + "," + criterion + ",#" +  mCriterionDateTime.StepId; 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("#"))
				mCriterion = dictionary[int.Parse(s.Substring(1))] as IfcConditionCriterionSelect;
			else
				mCriterion = ParserIfc.parseMeasureValue(s) as IfcConditionCriterionSelect;
			mCriterionDateTime = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
		}
	}
	public abstract partial class IfcConic
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mPosition.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mPosition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement; }
	}
	public partial class IfcConnectedFaceSet
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mCfsFaces.Count == 0)
				return "";
			return "(" + string.Join(",", CfsFaces.ConvertAll(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) 
		{
			List<int> stepIds = ParserSTEP.StripListLink(str, ref pos, len);
			CfsFaces.AddRange(stepIds.ConvertAll(x => dictionary[x] as IfcFace)); }
	}
	public partial class IfcConnectionCurveGeometry
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return "#" + mCurveOnRelatingElement.StepId + (mCurveOnRelatedElement == null ? ",$" : ",#" + mCurveOnRelatedElement.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mCurveOnRelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurveOrEdgeCurve;
			mCurveOnRelatedElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurveOrEdgeCurve;
		}
	}
	public partial class IfcConnectionPointEccentricity
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
	public partial class IfcConnectionPointGeometry
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return "#" + mPointOnRelatingElement.StepId + (mPointOnRelatedElement == null ? ",$" : ",#" + mPointOnRelatedElement.StepId); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mPointOnRelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPointOrVertexPoint;
			mPointOnRelatedElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPointOrVertexPoint;
		}
	}
	public partial class IfcConnectionSurfaceGeometry
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mSurfaceOnRelatingElement.StepId + (mSurfaceOnRelatedElement == null ? ",$" : ",#" + mSurfaceOnRelatedElement.StepId); }

		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mSurfaceOnRelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSurfaceOrFaceSurface;
			mSurfaceOnRelatedElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSurfaceOrFaceSurface;
		}
	}
	public partial class IfcConnectionVolumeGeometry
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "#" + mVolumeOnRelatingElement.StepId +
			(mVolumeOnRelatedElement == null ? ",$" : ",#" + mVolumeOnRelatedElement.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			VolumeOnRelatingElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSolidOrShell;
			VolumeOnRelatedElement = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSolidOrShell;
		}
	}
	public abstract partial class IfcConstraint
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return "'" + ParserSTEP.Encode(mName) + (string.IsNullOrWhiteSpace(mDescription) ? "',$,." : "','" + ParserSTEP.Encode(mDescription) + 
				"',.") + mConstraintGrade.ToString() + (string.IsNullOrWhiteSpace(mConstraintSource) ? ".,$," : ".,'" + ParserSTEP.Encode(mConstraintSource) + "',") +
				(mCreatingActor == null ? "$," : "#" + mCreatingActor.StepId + ",") + 
				(release < ReleaseVersion.IFC4 ? (mSSCreationTime == null ? "$" : "#" + mSSCreationTime.StepId) : IfcDate.STEPAttribute(mCreationTime)) + 
				(string.IsNullOrEmpty(mUserDefinedGrade) ? ",$" : ",'" + ParserSTEP.Encode(mUserDefinedGrade) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Name = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			Description = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			Enum.TryParse<IfcConstraintEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mConstraintGrade);
			mConstraintSource = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mCreatingActor = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorSelect;
			if (release < ReleaseVersion.IFC4)
				mSSCreationTime = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			else
				mCreationTime = IfcDate.ParseSTEP(ParserSTEP.StripString(str, ref pos, len));
			mUserDefinedGrade = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcConstructionEquipmentResource
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
	public partial class IfcConstructionEquipmentResourceType
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
	public partial class IfcConstructionMaterialResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return base.BuildStringSTEP(release) + (mSuppliers.Count == 0 ? ",$" : ",(" + string.Join(",", mSuppliers.Select(x=> "#" + x.StepId)) + "),") +
					(mUsageRatio == null ? ",$" : "," + mUsageRatio.ToString());
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcConstructionMaterialResourceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
			{
				mSuppliers.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcActorSelect));
				UsageRatio = ParserIfc.parseMeasureValue(ParserSTEP.StripField(str, ref pos, len)) as IfcRatioMeasure;
			}
			else
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcConstructionMaterialResourceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcConstructionMaterialResourceType
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
	public partial class IfcConstructionProductResource
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
	public partial class IfcConstructionProductResourceType
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
	public abstract partial class IfcConstructionResource
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mIdentification) ? ",$," : ",'" + ParserSTEP.Encode(mIdentification) + "',") +
					(string.IsNullOrEmpty(mResourceGroup) ? "$," : "'" + ParserSTEP.Encode(mResourceGroup) + "',") + ParserSTEP.ObjToLinkString(mBaseQuantitySS);
			return base.BuildStringSTEP(release) + (mUsage == null ? ",$,(" : ",#" + mUsage.StepId + ",(") +
				String.Join(",", BaseCosts.Select(x=>"#" + x.StepId)) + (mBaseQuantity == null ? "),$" : "),#" + mBaseQuantity.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);

			if (release < ReleaseVersion.IFC4)
			{
				mIdentification = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mResourceGroup = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mBaseQuantitySS = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMeasureWithUnit;
			}
			else
			{
				mUsage = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcResourceTime;
				mBaseCosts.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcAppliedValue));
				mBaseQuantity = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPhysicalQuantity;
			}
		}
	}
	public abstract partial class IfcConstructionResourceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{ 
			return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) +
				(mBaseCosts.Count == 0 ? ",$" : ",(" + String.Join(",", mBaseCosts.Select(x=>"#" + x.StepId)) + ")") + 
				(mBaseQuantity == null ? ",$" : ",#" + mBaseQuantity.StepId));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mBaseCosts.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcAppliedValue));
			mBaseQuantity = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPhysicalQuantity;
		}
	}
	public abstract partial class IfcContext
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mObjectType) ? ",$" : ",'" + ParserSTEP.Encode(mObjectType) + "'") + 
				(string.IsNullOrEmpty(mLongName) ? ",$" : ",'" + ParserSTEP.Encode(mLongName) + "'") +
				(string.IsNullOrEmpty(mPhase) ? ",$" : ",'" + ParserSTEP.Encode(mPhase) + "'") +
				(mRepresentationContexts.Count == 0 ? ",$," : ",(" + string.Join(",", mRepresentationContexts.Select(x => "#" + x.StepId)) + "),") + 
				(mUnitsInContext == null ? "$" : "#" + mUnitsInContext.StepId);

		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mObjectType = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mLongName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mPhase = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			RepresentationContexts.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcRepresentationContext));
			mUnitsInContext = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnitAssignment;

			if (mDatabase.mContext == null || !(this is IfcProjectLibrary))
				mDatabase.mContext = this;
		}
	}
	public partial class IfcContextDependentUnit
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",'" + ParserSTEP.Encode(mName) + "'";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Name = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public abstract partial class IfcControl
	{ 
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (string.IsNullOrEmpty( mIdentification) ? ",$" : ",'" + ParserSTEP.Encode(mIdentification) + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release > ReleaseVersion.IFC2x3)
				mIdentification = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcController  
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
	public partial class IfcControllerType
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
	public partial class IfcConversionBasedUnit
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",'" + mName + "',#" + mConversionFactor.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mName = ParserSTEP.StripString(str, ref pos, len);
			mConversionFactor = dictionary[ParserSTEP.StripLink(str, ref pos, len)]  as IfcMeasureWithUnit;
		}
	}
	public partial class IfcConversionBasedUnitWithOffset
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mConversionOffset); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mConversionOffset = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcConveyorSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcConveyorSegmentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcConveyorSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcConveyorSegmentType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcConveyorSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCooledBeam
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
	public partial class IfcCooledBeamType
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
	public partial class IfcCoolingTower
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
	public partial class IfcCoolingTowerType
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
	public partial class IfcCoordinatedUniversalTimeOffset
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return mHourOffset + "," + mMinuteOffset + ",." + mSense.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mHourOffset = ParserSTEP.StripInt(str, ref pos, len);
			mMinuteOffset = ParserSTEP.StripInt(str, ref pos, len);
			Enum.TryParse<IfcAheadOrBehind>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mSense);
		}
	}
	public abstract partial class IfcCoordinateOperation
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mSourceCRS.StepId + ",#" + mTargetCRS.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			SourceCRS = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCoordinateReferenceSystemSelect;
			TargetCRS = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCoordinateReferenceSystem;
		}
	}
	public abstract partial class IfcCoordinateReferenceSystem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$" : "'" + ParserSTEP.Encode(mDescription) + "'") +
				(string.IsNullOrEmpty(mGeodeticDatum) ? ",$" : ",'" + ParserSTEP.Encode(mGeodeticDatum) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mGeodeticDatum = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcCosineSpiral
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ParserSTEP.DoubleToString(mCosineTerm) + "," + ParserSTEP.DoubleOptionalToString(mConstantTerm);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			CosineTerm = ParserSTEP.StripDouble(str, ref pos, len);
			ConstantTerm = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCostItem
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
	public partial class IfcCostSchedule
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
			{
				return base.BuildStringSTEP(release) + (mSubmittedBy == null ? ",$" : ",#" + mSubmittedBy.StepId) +
					(mPreparedBy == null ? ",$" : ",#" + mPreparedBy.StepId) + (mSubmittedOnSS == null ? ",$" : ",#" + mSubmittedOnSS.StepId) +
					(string.IsNullOrEmpty(mStatus) ? ",$" : ",'" + ParserSTEP.Encode(mStatus) + "'") + 
					(mTargetUsers.Count == 0 ? ",$" : ",(" + string.Join(",", mTargetUsers.Select(x=>"#" + x.StepId)) +")") +
					(mUpdateDateSS == null ? ",$" : ",#" + mUpdateDateSS.StepId) + (string.IsNullOrEmpty(mIdentification) ? ",$,." : ",'" + ParserSTEP.Encode( mIdentification) + "',.") + 
					mPredefinedType.ToString() + ".";
			}
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + (string.IsNullOrEmpty(mStatus) ? ".,$," : ".,'" + ParserSTEP.Encode(mStatus) + "',") + IfcDate.STEPAttribute( mSubmittedOn) + "," + IfcDate.STEPAttribute(mUpdateDate);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
			{
				mSubmittedBy = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorSelect;
				mPreparedBy = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorSelect;
				mSubmittedOnSS = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
				mStatus = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mTargetUsers.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcActorSelect));
				mUpdateDateSS = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
				mIdentification = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
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
	public partial class IfcCostValue
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
	public partial class IfcCourse
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcCourseTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCourseTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCourseType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcCourseTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcCovering
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
	public partial class IfcCoveringType	
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
	public partial class IfcCraneRailAShapeProfileDef
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
	public partial class IfcCraneRailFShapeProfileDef
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
	public partial class IfcCrewResource
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
	public partial class IfcCrewResourceType
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
	public abstract partial class IfcCsgPrimitive3D
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mPosition == null ? "$" : "#" + mPosition.StepId); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{ 
			mPosition = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAxis2Placement3D; 
		}
	}
	public partial class IfcCsgSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" +  mTreeRootExpression.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) 
		{ 
			mTreeRootExpression = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCsgSelect;
		}
	}
	public partial class IfcCShapeProfileDef
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
	public partial class IfcCurrencyRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
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
	public partial class IfcCurtainWall
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcCurtainWallTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); 
		}
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
	public partial class IfcCurtainWallType
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
	public partial class IfcCurveBoundedPlane
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "#" + mBasisSurface.StepId + ",#" + mOuterBoundary.StepId + 
				(mInnerBoundaries.Count > 0 || release < ReleaseVersion.IFC4 ? ",(" + string.Join(",", mInnerBoundaries.Select(x=> "#" + x.StepId)) + ")" : ",$");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			BasisSurface = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPlane;
			OuterBoundary = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			InnerBoundaries.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcCurve));
		}
	}
	public partial class IfcCurveBoundedSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mBasisSurface.StepId + ",(" + string.Join(",", mBoundaries.Select(x=>"#" + x.StepId)) + (mImplicitOuter ? "),.T." : "),.F."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mBasisSurface = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSurface;
			Boundaries.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcBoundaryCurve));
			mImplicitOuter = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcCurveSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mPlacement.StepId + "," + mSegmentStart.ToString() + "," +
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
	public abstract partial class IfcCurveSegment2D
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mStartPoint.StepId + 
				"," + ParserSTEP.DoubleToString(mStartDirection) + "," + formatLength(mSegmentLength); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mStartPoint = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianPoint;
			mStartDirection = ParserSTEP.StripDouble(str, ref pos, len);
			mSegmentLength = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCurveStyle
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (mCurveFont == null ? ",$" : ",#" + mCurveFont.StepId) +
				(mCurveWidth == null ? ",$," : "," + mCurveWidth.ToString() + ",") + 
				(mCurveColour == null ? "$" : "#" + mCurveColour.StepId) + 
				(release != ReleaseVersion.IFC2x3 ? (mModelOrDraughting == IfcLogicalEnum.UNKNOWN ? ",$" : ","+ ParserIfc.LogicalToString(mModelOrDraughting)) :"");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mCurveFont = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurveFontOrScaledCurveFontSelect;
			string s = ParserSTEP.StripField(str, ref pos, len);
			if(str != "$")
				mCurveWidth = ParserIfc.parseValue(str) as IfcSizeSelect;
			mCurveColour = dictionary[ ParserSTEP.StripLink(str, ref pos, len)] as IfcColour;
			if (release != ReleaseVersion.IFC2x3)
				mModelOrDraughting = ParserIfc.StripLogical(str, ref pos, len);
		}
	}
	public partial class IfcCurveStyleFont
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (string.IsNullOrEmpty(mName) ? "$,(" : "'" + ParserSTEP.Encode(mName) + "',(") +
				string.Join(",", mPatternList.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mPatternList.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcCurveStyleFontPattern));
		}
	}
	public partial class IfcCurveStyleFontAndScaling
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (string.IsNullOrEmpty(mName) ? "$,#" : "'" + ParserSTEP.Encode(mName) + "',#") + mCurveStyleFont.StepId + "," + mCurveFontScaling.ToString(); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mCurveStyleFont = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurveStyleFontSelect;
			mCurveFontScaling = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCurveStyleFontPattern
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return ParserSTEP.DoubleToString(mVisibleSegmentLength) + "," + ParserSTEP.DoubleToString(mInvisibleSegmentLength); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mVisibleSegmentLength = ParserSTEP.StripDouble(str, ref pos, len);
			mInvisibleSegmentLength = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcCylindricalSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return  base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mRadius); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
}
