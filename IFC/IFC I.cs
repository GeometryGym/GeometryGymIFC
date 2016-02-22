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
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class IfcImageTexture : IfcSurfaceTexture
	{
		internal string mUrlReference;// : IfcIdentifier; 
		internal IfcImageTexture() : base() { }
		internal IfcImageTexture(IfcImageTexture i) : base((IfcSurfaceTexture)i) { mUrlReference = i.mUrlReference; }
		internal static IfcImageTexture Parse(string strDef, Schema schema) { IfcImageTexture t = new IfcImageTexture(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return t; }
		internal static void parseFields(IfcImageTexture t, List<string> arrFields, ref int ipos,Schema schema)
		{
			try
			{
				IfcSurfaceTexture.parseFields(t, arrFields, ref ipos,schema);
				t.mUrlReference = arrFields[ipos++].Replace("'", "");
			}
			catch (Exception) { }
		}
		protected override string BuildString() { return base.BuildString() + ",'" + mUrlReference + "'"; }
	}
	public class IfcIndexedColourMap : IfcPresentationItem
	{
		internal int mMappedTo;// : IfcTessellatedFaceSet; 
		// Overrides : OPTIONAL IfcStrippedOptional; 
		internal int mColours;// : IfcColourRgbList; 
		internal List<int> mColourIndex = new List<int>();// : LIST [1:?] OF IfcPositiveInteger;

		public IfcColourRgbList Colours { get { return mDatabase.mIfcObjects[mColours] as IfcColourRgbList; } }

		internal IfcIndexedColourMap() : base() { }
		internal IfcIndexedColourMap(IfcIndexedColourMap v) : base(v) { mMappedTo = v.mMappedTo; mColours = v.mColours; }
		public IfcIndexedColourMap(IfcTessellatedFaceSet fs, IfcColourRgbList colours, IEnumerable<int> colourindex)
			: base(fs.mDatabase) { mMappedTo = fs.mIndex; mColours = colours.mIndex; mColourIndex.AddRange(colourindex); }
		protected override void parseFields(List<string> arrFields, ref int ipos)
		{
			base.parseFields(arrFields, ref ipos);
			mMappedTo = ParserSTEP.ParseLink(arrFields[ipos++]);
			ipos++;
			mColours = ParserSTEP.ParseLink(arrFields[ipos++]);
			mColourIndex = ParserSTEP.SplitListSTPIntegers(arrFields[ipos++]);
		}
		internal static IfcIndexedColourMap Parse(string strDef) { IfcIndexedColourMap s = new IfcIndexedColourMap(); int ipos = 0; s.parseFields(ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential)
				return "";
			string result = base.BuildString() + "," + ParserSTEP.LinkToString(mMappedTo) + ",$," + ParserSTEP.LinkToString(mColours) + ",(" + mColourIndex[0];
			for (int icounter = 1; icounter < mColourIndex.Count; icounter++)
				result += "," + mColourIndex[icounter];
			return result + ")";
		}
		internal void relate() { (mDatabase.mIfcObjects[mMappedTo] as IfcTessellatedFaceSet).mHasColours = this; }
	}
	public partial class IfcIndexedPolyCurve : IfcBoundedCurve
	{
		private int mPoints; // IfcCartesianPointList
		internal List<IfcSegmentIndexSelect> mSegments = new List<IfcSegmentIndexSelect>();// OPTIONAL LIST [1:?] OF IfcSegmentIndexSelect;
		internal IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// Optional IfcBoolean

		public IfcCartesianPointList Points { get { return mDatabase.mIfcObjects[mPoints] as IfcCartesianPointList; } set { mPoints = value.mIndex; } }
		public List<IfcSegmentIndexSelect> Segments { get { return mSegments; } set { mSegments = new List<IfcSegmentIndexSelect>() { }; mSegments.AddRange(value); } }
		public bool SelfIntersect { get { return mSelfIntersect == IfcLogicalEnum.TRUE; } set { mSelfIntersect = (value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); }}

		internal IfcIndexedPolyCurve() : base() { }
		internal IfcIndexedPolyCurve(IfcIndexedPolyCurve p) : base(p) { mPoints = p.mPoints; mSegments.AddRange(p.mSegments); mSelfIntersect = p.mSelfIntersect; }
		internal IfcIndexedPolyCurve(IfcCartesianPointList pl) : base(pl.mDatabase) { Points = pl; }
		internal IfcIndexedPolyCurve(IfcCartesianPointList pl, List<IfcSegmentIndexSelect> segs) : this(pl) { Segments = segs; }
		internal static void parseFields(IfcIndexedPolyCurve c, List<string> arrFields, ref int ipos)
		{
			IfcBoundedCurve.parseFields(c, arrFields, ref ipos);
			c.mPoints = ParserSTEP.ParseLink(arrFields[ipos++]);
			string str = arrFields[ipos++];
			if (str != "$")
			{
				List<string> strs = ParserSTEP.SplitLineFields(str.Substring(1, str.Length - 2));
				foreach (string s in strs)
				{
					if (s.ToUpper().StartsWith("IFCLINEINDEX"))
						c.mSegments.Add(new IfcLineIndex(ParserSTEP.SplitListSTPIntegers(s.Substring(13, s.Length - 14))));
					else
					{
						List<int> ints = ParserSTEP.SplitListSTPIntegers(s.Substring(12, s.Length - 13));
						c.mSegments.Add(new IfcArcIndex(ints[0], ints[1], ints[2]));
					}

				}
			}
			str = arrFields[ipos++];
			if (str[0] == '.')
				c.mSelfIntersect = str[1] == 'T' ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE;
		}
		internal static IfcIndexedPolyCurve Parse(string strDef) { IfcIndexedPolyCurve c = new IfcIndexedPolyCurve(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString()
		{

			string str = base.BuildString() + ",#" + mPoints;
			if (mSegments.Count > 0)
			{
				str += ",(" + mSegments[0].ToString();
				for (int icounter = 1; icounter < mSegments.Count; icounter++)
					str += "," + mSegments[icounter].ToString();
				str += "),";
			}
			else
				str += ",$,";
			str += (mSelfIntersect == IfcLogicalEnum.UNKNOWN ? "$" : (mSelfIntersect == IfcLogicalEnum.TRUE ? ".T." : ".F."));
			return base.BuildString() + str;
		}
	}
	public abstract partial class IfcIndexedTextureMap : IfcTextureCoordinate // ABSTRACT SUPERTYPE OF(IfcIndexedTriangleTextureMap)
	{
		internal int mMappedTo = 0;// : IfcTessellatedFaceSet;
		internal int mTexCoords = 0;// : IfcTextureVertexList;

		public IfcTessellatedFaceSet MappedTo { get { return mDatabase.mIfcObjects[mMappedTo] as IfcTessellatedFaceSet; } }

		protected IfcIndexedTextureMap() : base() { }
		protected IfcIndexedTextureMap(IfcIndexedTextureMap m) : base(m) { mMappedTo = m.mMappedTo; mTexCoords = m.mTexCoords; }
		//internal IfcIndexedTextureMap(IfcTessellatedFaceSet mappedTo, ifctext) : this(pl, nll, selfIntersect, new List<int>()) { }
		//internal IfcIndexedTextureMap(IfcCartesianPointList pl, List<IfcSegmentIndexSelect> segs, IfcLogicalEnum selfIntersect) : this(pl, segs, selfIntersect, new List<int>()) { }
		protected override void parseFields(List<string> arrFields, ref int ipos)
		{
			base.parseFields(arrFields, ref ipos);
			mMappedTo = ParserSTEP.ParseLink(arrFields[ipos++]);
			mTexCoords = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + ",#" + mMappedTo + ",#" + mTexCoords; }

		internal void relate() { MappedTo.mHasTextures.Add(this); }
	} 
	public class IfcIndexedTriangleTextureMap : IfcIndexedTextureMap
	{
		internal Tuple<int, int, int>[] mTexCoordList = new Tuple<int, int, int>[0];// : OPTIONAL LIST [1:?] OF LIST [3:3] OF IfcPositiveInteger;

		internal IfcIndexedTriangleTextureMap() : base() { }
		internal IfcIndexedTriangleTextureMap(IfcIndexedTriangleTextureMap c) : base() { mTexCoordList = c.mTexCoordList; }
		//public IfcIndexedTriangleTextureMap(DatabaseIfc m, IEnumerable<Tuple<int, int,int>> coords) : base(m) { mTexCoordList = coords.ToArray(); }

		internal static IfcIndexedTriangleTextureMap Parse(string str) { IfcIndexedTriangleTextureMap m = new IfcIndexedTriangleTextureMap(); int pos = 0; m.parseFields(ParserSTEP.SplitLineFields(str), ref pos); return m; }
		protected override void parseFields(List<string> arrFields, ref int ipos) { base.parseFields(arrFields, ref ipos); mTexCoordList = ParserSTEP.SplitListSTPIntTriple(arrFields[ipos++]); }
		protected override string BuildString() 
		{
			if (mTexCoordList.Length == 0)
				return base.BuildString() + ",$";
			Tuple<int, int,int> triple = mTexCoordList[0];
			string result = base.BuildString() + ",((" + triple.Item1 + "," + triple.Item2 + "," + triple.Item3;
			for (int icounter = 1; icounter < mTexCoordList.Length; icounter++)
			{
				triple = mTexCoordList[icounter];
				result += "),(" + triple.Item1 + "," + triple.Item2 + "," + triple.Item3;
			}

			return result + "))";
		}
	} 
	public class IfcInterceptor : IfcFlowTreatmentDevice //IFC4  
	{
		internal IfcInterceptorTypeEnum mPredefinedType = IfcInterceptorTypeEnum.NOTDEFINED;
		public IfcInterceptorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcInterceptor(IfcInterceptor a) : base(a) { mPredefinedType = a.mPredefinedType; }
		internal IfcInterceptor() : base() { }
		internal IfcInterceptor(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcInterceptor a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcInterceptorTypeEnum)Enum.Parse(typeof(IfcInterceptorTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcInterceptor Parse(string strDef) { IfcInterceptor d = new IfcInterceptor(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcInterceptorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcInterceptorType : IfcFlowTreatmentDeviceType
	{
		internal IfcInterceptorTypeEnum mPredefinedType = IfcInterceptorTypeEnum.NOTDEFINED;// : IfcInterceptorTypeEnum;
		public IfcInterceptorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcInterceptorType() : base() { }
		internal IfcInterceptorType(IfcInterceptorType be) : base(be) { mPredefinedType = be.mPredefinedType; }
		internal static void parseFields(IfcInterceptorType t, List<string> arrFields, ref int ipos) { IfcFlowTreatmentDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcInterceptorTypeEnum)Enum.Parse(typeof(IfcInterceptorTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcInterceptorType Parse(string strDef) { IfcInterceptorType t = new IfcInterceptorType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcInventory : IfcGroup
	{
		internal IfcInventoryTypeEnum mInventoryType;// : IfcInventoryTypeEnum;
		internal int mJurisdiction;// : IfcActorSelect;
		internal List<int> mResponsiblePersons = new List<int>();// : SET [1:?] OF IfcPerson;
		internal int mLastUpdateDate;// : IfcCalendarDate;
		internal int mCurrentValue;// : OPTIONAL IfcCostValue;
		internal int mOriginalValue;// : OPTIONAL IfcCostValue;
		internal IfcInventory() : base() { }
		internal IfcInventory(IfcInventory p) : base(p) { mInventoryType = p.mInventoryType; mJurisdiction = p.mJurisdiction; mResponsiblePersons = new List<int>(p.mResponsiblePersons.ToArray()); mLastUpdateDate = p.mLastUpdateDate; mCurrentValue = p.mCurrentValue; mOriginalValue = p.mOriginalValue; }
		internal IfcInventory(DatabaseIfc m, string name) : base(m, name) { }
		internal new static IfcInventory Parse(string strDef) { IfcInventory i = new IfcInventory(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcInventory i, List<string> arrFields, ref int ipos)
		{
			IfcGroup.parseFields(i, arrFields, ref ipos);
			i.mInventoryType = (IfcInventoryTypeEnum)Enum.Parse(typeof(IfcInventoryTypeEnum), arrFields[ipos++].Replace(".", ""));
			i.mJurisdiction = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mResponsiblePersons = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			i.mLastUpdateDate = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mCurrentValue = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mOriginalValue = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
	}
	//ENTITY IfcIrregularTimeSeries
	//ENTITY IfcIrregularTimeSeriesValue;ductFittingTypeEnum;
	public partial class IfcIShapeProfileDef : IfcParameterizedProfileDef //Ifc2x3 SUBTYPE OF (	IfcParameterizedProfileDef);
	{
		internal double mOverallWidth, mOverallDepth, mWebThickness, mFlangeThickness;// : IfcPositiveLengthMeasure;
		internal double mFilletRadius;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mFlangeEdgeRadius;// : OPTIONAL IfcNonNegativeLengthMeasure;
		internal double mFlangeSlope;// : OPTIONAL IfcPlaneAngleMeasure; 

		public double FlangeEdgeRadius { get { return mFlangeEdgeRadius; } set { mFlangeEdgeRadius = value; } }
		public double FlangeSlope { get { return mFlangeSlope; } set { mFlangeSlope = value; } }

		internal IfcIShapeProfileDef() : base() { }
		internal IfcIShapeProfileDef(IfcIShapeProfileDef i) : base(i) { mOverallWidth = i.mOverallWidth; mOverallDepth = i.mOverallDepth; mWebThickness = i.mWebThickness; mFlangeThickness = i.mFlangeThickness; mFilletRadius = i.mFilletRadius; mFlangeEdgeRadius = i.mFlangeEdgeRadius; mFlangeSlope = i.mFlangeSlope; }
		public IfcIShapeProfileDef(DatabaseIfc m, string name, double overallDepth, double overalWidth, double webThickness, double flangeThickness, double filletRadius)
			: base(m) { Name = name; mOverallDepth = overallDepth; mOverallWidth = overalWidth; mWebThickness = webThickness; mFlangeThickness = flangeThickness; mFilletRadius = filletRadius; }
		internal static IfcIShapeProfileDef Parse(string strDef, Schema schema) { IfcIShapeProfileDef p = new IfcIShapeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcIShapeProfileDef p, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos);
			p.mOverallWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mOverallDepth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mWebThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFlangeThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema != Schema.IFC2x3) 
			{
				p.mFlangeEdgeRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mFlangeSlope = ParserSTEP.ParseDouble(arrFields[ipos++]);
			}
		}
		protected override string BuildString()
		{
			return base.BuildString() + "," + ParserSTEP.DoubleToString(mOverallWidth) + "," + ParserSTEP.DoubleToString(mOverallDepth) + "," + ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mFlangeThickness) + "," + ParserSTEP.DoubleToString(mFilletRadius)
				+ (mDatabase.mSchema == Schema.IFC2x3 ? "" : "," + ParserSTEP.DoubleOptionalToString(mFlangeEdgeRadius) + "," + ParserSTEP.DoubleOptionalToString(mFlangeSlope));
		}
	} 
}
