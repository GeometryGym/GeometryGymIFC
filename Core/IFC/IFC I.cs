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
	public partial class IfcImageTexture : IfcSurfaceTexture
	{
		internal string mUrlReference;// : IfcIdentifier; 
		public string UrlReference { get { return ParserIfc.Decode(mUrlReference); } set { mUrlReference = ParserIfc.Encode(value); } }

		internal IfcImageTexture() : base() { }
		internal IfcImageTexture(DatabaseIfc db, IfcImageTexture t) : base(db, t) { mUrlReference = t.mUrlReference; }
		public IfcImageTexture(DatabaseIfc db, bool repeatS, bool repeatT, string urlReference) : base(db, repeatS, repeatT) { UrlReference = urlReference; }
		internal static IfcImageTexture Parse(string strDef, ReleaseVersion schema) { IfcImageTexture t = new IfcImageTexture(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return t; }
		internal static void parseFields(IfcImageTexture t, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			try
			{
				IfcSurfaceTexture.parseFields(t, arrFields, ref ipos, schema);
				t.mUrlReference = arrFields[ipos++].Replace("'", "");
			}
			catch (Exception) { }
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mUrlReference + "'"; }
	}
	public partial class IfcIndexedColourMap : IfcPresentationItem
	{
		internal int mMappedTo;// : IfcTessellatedFaceSet; 
							   // Overrides : OPTIONAL IfcStrippedOptional; 
		internal int mColours;// : IfcColourRgbList; 
		internal List<int> mColourIndex = new List<int>();// : LIST [1:?] OF IfcPositiveInteger;

		public IfcTessellatedFaceSet MappedTo { get { return mDatabase[mMappedTo] as IfcTessellatedFaceSet; } set { mMappedTo = value.mIndex; } }
		public IfcColourRgbList Colours { get { return mDatabase[mColours] as IfcColourRgbList; } set { mColours = value.mIndex; } }

		internal IfcIndexedColourMap() : base() { }
		internal IfcIndexedColourMap(DatabaseIfc db, IfcIndexedColourMap m) : base(db, m) { MappedTo = db.Factory.Duplicate(m.MappedTo) as IfcTessellatedFaceSet; Colours = db.Factory.Duplicate(m.Colours) as IfcColourRgbList; mColourIndex.AddRange(m.mColourIndex); }
		public IfcIndexedColourMap(IfcTessellatedFaceSet fs, IfcColourRgbList colours, IEnumerable<int> colourindex)
			: base(fs.mDatabase) { mMappedTo = fs.mIndex; mColours = colours.mIndex; mColourIndex.AddRange(colourindex); }
		protected override void parseFields(List<string> arrFields, ref int ipos)
		{
			base.parseFields(arrFields, ref ipos);
			mMappedTo = ParserSTEP.ParseLink(arrFields[ipos++]);
			ipos++; // Overrides : OPTIONAL IfcStrippedOptional;
			mColours = ParserSTEP.ParseLink(arrFields[ipos++]);
			mColourIndex = ParserSTEP.SplitListSTPIntegers(arrFields[ipos++]);
		}
		internal static IfcIndexedColourMap Parse(string strDef) { IfcIndexedColourMap s = new IfcIndexedColourMap(); int ipos = 0; s.parseFields(ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mMappedTo) + ",$," + ParserSTEP.LinkToString(mColours) + ",(" + mColourIndex[0];
			for (int icounter = 1; icounter < mColourIndex.Count; icounter++)
				result += "," + mColourIndex[icounter];
			return result + ")";
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			MappedTo.mHasColours = this;
		}
	}
	public partial class IfcIndexedPolyCurve : IfcBoundedCurve
	{
		private int mPoints; // IfcCartesianPointList
		internal List<IfcSegmentIndexSelect> mSegments = new List<IfcSegmentIndexSelect>();// OPTIONAL LIST [1:?] OF IfcSegmentIndexSelect;
		internal IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// Optional IfcBoolean

		public IfcCartesianPointList Points { get { return mDatabase[mPoints] as IfcCartesianPointList; } set { mPoints = value.mIndex; } }
		public ReadOnlyCollection<IfcSegmentIndexSelect> Segments { get { return new ReadOnlyCollection<IfcSegmentIndexSelect>( mSegments); } }
		public bool SelfIntersect { get { return mSelfIntersect == IfcLogicalEnum.TRUE; } set { mSelfIntersect = (value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); } }

		internal IfcIndexedPolyCurve() : base() { }
		internal IfcIndexedPolyCurve(DatabaseIfc db, IfcIndexedPolyCurve c) : base(db, c) { Points = db.Factory.Duplicate(c.Points) as IfcCartesianPointList; mSegments.AddRange(c.mSegments); mSelfIntersect = c.mSelfIntersect; }
		public IfcIndexedPolyCurve(IfcCartesianPointList pl) : base(pl.mDatabase) { Points = pl; }
		public IfcIndexedPolyCurve(IfcCartesianPointList pl, List<IfcSegmentIndexSelect> segs) : this(pl) { mSegments = segs; }
		internal static IfcIndexedPolyCurve Parse(string str)
		{
			IfcIndexedPolyCurve c = new IfcIndexedPolyCurve();
			int pos = 0, len = str.Length;
			c.mPoints = ParserSTEP.StripLink(str, ref pos, len);
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field != "$")
			{
				List<string> strs = ParserSTEP.SplitLineFields(field.Substring(1, field.Length - 2));
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
			field = ParserSTEP.StripField(str, ref pos, len);
			if (field[0] == '.')
				c.mSelfIntersect = field[1] == 'T' ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE;
			return c;
		}
		protected override string BuildStringSTEP()
		{

			string str = base.BuildStringSTEP() + ",#" + mPoints;
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
			return base.BuildStringSTEP() + str;
		}

		internal void addSegment(IfcSegmentIndexSelect segment) { mSegments.Add(segment); }
		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			if (schema == ReleaseVersion.IFC2x3)
			{
				IfcCartesianPointList cpl = Points;
				IfcCartesianPointList2D cpl2d = cpl as IfcCartesianPointList2D;
				if (cpl2d != null)
				{
					IfcBoundedCurve bc = IfcBoundedCurve.Generate(mDatabase, cpl2d.mCoordList.ToList(), Segments.ToList());
					int index = bc.mIndex;
					mDatabase[mIndex] = bc;
					mDatabase[index] = null;
					mDatabase[cpl.mIndex] = null;
				}
				else
				{
					throw new Exception("Not Implemented");
				}
			}
		}
	}
	public partial class IfcIndexedPolygonalFace : IfcTessellatedItem
	{
		internal List<int> mCoordIndex = new List<int>();// : LIST [3:?] OF IfcPositiveInteger;
														 //INVERSE
		internal IfcPolygonalFaceSet mToFaceSet = null;

		public ReadOnlyCollection<int> CoordIndex { get { return new ReadOnlyCollection<int>( mCoordIndex); } }
		public IfcPolygonalFaceSet ToFaceSet { get { return mToFaceSet; } set { mToFaceSet = value; } }

		internal IfcIndexedPolygonalFace() : base() { }
		internal IfcIndexedPolygonalFace(DatabaseIfc db, IfcIndexedPolygonalFace f) : base(db, f) { mCoordIndex.AddRange(f.mCoordIndex); }
		public IfcIndexedPolygonalFace(DatabaseIfc db, IEnumerable<int> coords) : base(db) { mCoordIndex = coords.ToList(); }
		protected override void parseFields(List<string> arrFields, ref int ipos)
		{
			base.parseFields(arrFields, ref ipos);
			mCoordIndex = ParserSTEP.SplitListSTPIntegers(arrFields[ipos++]);
		}
		internal static IfcIndexedPolygonalFace Parse(string strDef) { IfcIndexedPolygonalFace s = new IfcIndexedPolygonalFace(); int ipos = 0; s.parseFields(ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + ",(" + mCoordIndex[0];
			for (int icounter = 1; icounter < mCoordIndex.Count; icounter++)
				result += "," + mCoordIndex[icounter];
			return result + ")";
		}

		public void AddCoordIndex(int index) { mCoordIndex.Add(index); }
	}
	public partial class IfcIndexedPolygonalFaceWithVoids : IfcIndexedPolygonalFace
	{
		internal List<List<int>> mInnerCoordIndices = new List<List<int>>();// : List[1:?] LIST [3:?] OF IfcPositiveInteger;
		public ReadOnlyCollection<ReadOnlyCollection<int>> InnerCoordIndices { get { return new ReadOnlyCollection<ReadOnlyCollection<int>>( mInnerCoordIndices.ConvertAll(x=> new ReadOnlyCollection<int>(x))); }  }
		internal IfcIndexedPolygonalFaceWithVoids() : base() { }
		internal IfcIndexedPolygonalFaceWithVoids(DatabaseIfc db, IfcIndexedPolygonalFaceWithVoids f) : base(db, f) { mInnerCoordIndices.AddRange(f.mInnerCoordIndices); }
		public IfcIndexedPolygonalFaceWithVoids(DatabaseIfc db, IEnumerable<int> coords, IEnumerable<IEnumerable<int>> inners) 
			: base(db, coords) { mInnerCoordIndices = inners.ToList().ConvertAll(x=>x.ToList()); }
		protected override void parseFields(List<string> arrFields, ref int ipos)
		{
			base.parseFields(arrFields, ref ipos);
			string str = arrFields[ipos++];
			List<string> fields = ParserSTEP.SplitLineFields(str.Substring(1, str.Length - 2));
			mInnerCoordIndices = fields.ConvertAll(x => ParserSTEP.SplitListSTPIntegers(x));
		}
		internal new static IfcIndexedPolygonalFaceWithVoids Parse(string strDef) { IfcIndexedPolygonalFaceWithVoids s = new IfcIndexedPolygonalFaceWithVoids(); int ipos = 0; s.parseFields(ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
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
			return base.BuildStringSTEP() + ",((" + result + "))";
		}
	}
	public abstract partial class IfcIndexedTextureMap : IfcTextureCoordinate // ABSTRACT SUPERTYPE OF(IfcIndexedTriangleTextureMap)
	{
		internal int mMappedTo = 0;// : IfcTessellatedFaceSet;
		internal int mTexCoords = 0;// : IfcTextureVertexList;

		public IfcTessellatedFaceSet MappedTo { get { return mDatabase[mMappedTo] as IfcTessellatedFaceSet; } set { mMappedTo = value.mIndex; } }
		public IfcTextureVertexList TexCoords { get { return mDatabase[mTexCoords] as IfcTextureVertexList; } set { mTexCoords = value.mIndex; } }

		protected IfcIndexedTextureMap() : base() { }
		protected IfcIndexedTextureMap(DatabaseIfc db, IfcIndexedTextureMap m) : base(db, m) { MappedTo = db.Factory.Duplicate(m.MappedTo) as IfcTessellatedFaceSet; TexCoords = db.Factory.Duplicate(m.TexCoords) as IfcTextureVertexList; }
		//internal IfcIndexedTextureMap(IfcTessellatedFaceSet mappedTo, ifctext) : this(pl, nll, selfIntersect, new List<int>()) { }
		//internal IfcIndexedTextureMap(IfcCartesianPointList pl, List<IfcSegmentIndexSelect> segs, IfcLogicalEnum selfIntersect) : this(pl, segs, selfIntersect, new List<int>()) { }
		protected override void parseFields(List<string> arrFields, ref int ipos)
		{
			base.parseFields(arrFields, ref ipos);
			mMappedTo = ParserSTEP.ParseLink(arrFields[ipos++]);
			mTexCoords = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",#" + mMappedTo + ",#" + mTexCoords; }

		internal override void postParseRelate()
		{
			base.postParseRelate();
			MappedTo.mHasTextures.Add(this);
		}
	}
	public partial class IfcIndexedTriangleTextureMap : IfcIndexedTextureMap
	{
		internal Tuple<int, int, int>[] mTexCoordList = new Tuple<int, int, int>[0];// : OPTIONAL LIST [1:?] OF LIST [3:3] OF IfcPositiveInteger;

		internal IfcIndexedTriangleTextureMap() : base() { }
		internal IfcIndexedTriangleTextureMap(DatabaseIfc db, IfcIndexedTriangleTextureMap m) : base(db, m) { mTexCoordList = m.mTexCoordList; }
		//public IfcIndexedTriangleTextureMap(DatabaseIfc m, IEnumerable<Tuple<int, int,int>> coords) : base(m) { mTexCoordList = coords.ToArray(); }

		internal static IfcIndexedTriangleTextureMap Parse(string str) { IfcIndexedTriangleTextureMap m = new IfcIndexedTriangleTextureMap(); int pos = 0; m.parseFields(ParserSTEP.SplitLineFields(str), ref pos); return m; }
		protected override void parseFields(List<string> arrFields, ref int ipos) { base.parseFields(arrFields, ref ipos); mTexCoordList = ParserSTEP.SplitListSTPIntTriple(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			if (mTexCoordList.Length == 0)
				return base.BuildStringSTEP() + ",$";
			Tuple<int, int, int> triple = mTexCoordList[0];
			string result = base.BuildStringSTEP() + ",((" + triple.Item1 + "," + triple.Item2 + "," + triple.Item3;
			for (int icounter = 1; icounter < mTexCoordList.Length; icounter++)
			{
				triple = mTexCoordList[icounter];
				result += "),(" + triple.Item1 + "," + triple.Item2 + "," + triple.Item3;
			}

			return result + "))";
		}
	}
	public partial class IfcInterceptor : IfcFlowTreatmentDevice //IFC4  
	{
		internal IfcInterceptorTypeEnum mPredefinedType = IfcInterceptorTypeEnum.NOTDEFINED;
		public IfcInterceptorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcInterceptor() : base() { }
		internal IfcInterceptor(DatabaseIfc db, IfcInterceptor i) : base(db, i) { mPredefinedType = i.mPredefinedType; }
		public IfcInterceptor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcInterceptor a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcInterceptorTypeEnum)Enum.Parse(typeof(IfcInterceptorTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcInterceptor Parse(string strDef) { IfcInterceptor d = new IfcInterceptor(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcInterceptorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcInterceptorType : IfcFlowTreatmentDeviceType
	{
		internal IfcInterceptorTypeEnum mPredefinedType = IfcInterceptorTypeEnum.NOTDEFINED;// : IfcInterceptorTypeEnum;
		public IfcInterceptorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcInterceptorType() : base() { }
		internal IfcInterceptorType(DatabaseIfc db, IfcInterceptorType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal static void parseFields(IfcInterceptorType t, List<string> arrFields, ref int ipos) { IfcFlowTreatmentDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcInterceptorTypeEnum)Enum.Parse(typeof(IfcInterceptorTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcInterceptorType Parse(string strDef) { IfcInterceptorType t = new IfcInterceptorType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcIntersectionCurve : IfcSurfaceCurve //IFC4 Add2
	{
		internal IfcIntersectionCurve() : base() { }
		internal IfcIntersectionCurve(DatabaseIfc db, IfcIntersectionCurve c) : base(db, c) { }
		internal IfcIntersectionCurve(IfcCurve curve, IfcPCurve p1, IfcPCurve p2, IfcPreferredSurfaceCurveRepresentation cr) : base(curve,p1,p2,cr) { }
		internal new static IfcIntersectionCurve Parse(string str) { IfcIntersectionCurve c = new IfcIntersectionCurve(); int pos = 0; c.Parse(str, ref pos, str.Length); return c; }
	}
	public partial class IfcInventory : IfcGroup
	{
		internal IfcInventoryTypeEnum mInventoryType;// : IfcInventoryTypeEnum;
		internal int mJurisdiction;// : IfcActorSelect;
		internal List<int> mResponsiblePersons = new List<int>();// : SET [1:?] OF IfcPerson;
		internal int mLastUpdateDate;// : IfcCalendarDate;
		internal int mCurrentValue;// : OPTIONAL IfcCostValue;
		internal int mOriginalValue;// : OPTIONAL IfcCostValue;
		internal IfcInventory() : base() { }
		internal IfcInventory(DatabaseIfc db, IfcInventory i, bool downStream) : base(db, i, downStream)
		{
#warning todo
			//mInventoryType = p.mInventoryType;
			//mJurisdiction = p.mJurisdiction;
			//mResponsiblePersons = new List<int>(p.mResponsiblePersons.ToArray());
			//mLastUpdateDate = p.mLastUpdateDate;
			//mCurrentValue = p.mCurrentValue;
			//mOriginalValue = p.mOriginalValue;
		}
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
	public partial class IfcIShapeProfileDef : IfcParameterizedProfileDef // Ifc2x3 SUPERTYPE OF	(IfcAsymmetricIShapeProfileDef) 
	{
		internal double mOverallWidth, mOverallDepth, mWebThickness, mFlangeThickness;// : IfcPositiveLengthMeasure;
		internal double mFilletRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mFlangeEdgeRadius = double.NaN;// : OPTIONAL IfcNonNegativeLengthMeasure; //IFC4
		internal double mFlangeSlope = double.NaN;// : OPTIONAL IfcPlaneAngleMeasure; //IFC4

		public double OverallWidth { get { return mOverallWidth; } set { mOverallWidth = value; } }
		public double OverallDepth { get { return mOverallDepth; } set { mOverallDepth = value; } }
		public double WebThickness { get { return mWebThickness; } set { mWebThickness = value; } } 
		public double FlangeThickness { get { return mFlangeThickness; } set { mFlangeThickness = value; } } 
		public double FilletRadius { get { return mFilletRadius; } set { mFilletRadius = value; } } 
		public double FlangeEdgeRadius { get { return mFlangeEdgeRadius; } set { mFlangeEdgeRadius = value; } }
		public double FlangeSlope { get { return mFlangeSlope; } set { mFlangeSlope = value; } }

		internal IfcIShapeProfileDef() : base() { }
		internal IfcIShapeProfileDef(DatabaseIfc db, IfcIShapeProfileDef p) : base(db,p) { mOverallWidth = p.mOverallWidth; mOverallDepth = p.mOverallDepth; mWebThickness = p.mWebThickness; mFlangeThickness = p.mFlangeThickness; mFilletRadius = p.mFilletRadius; mFlangeEdgeRadius = p.mFlangeEdgeRadius; mFlangeSlope = p.mFlangeSlope; }
		public IfcIShapeProfileDef(DatabaseIfc m, string name, double overallDepth, double overalWidth, double webThickness, double flangeThickness, double filletRadius)
			: base(m,name) { mOverallDepth = overallDepth; mOverallWidth = overalWidth; mWebThickness = webThickness; mFlangeThickness = flangeThickness; mFilletRadius = filletRadius; }

		internal static IfcIShapeProfileDef Parse(string strDef, ReleaseVersion schema) { IfcIShapeProfileDef p = new IfcIShapeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcIShapeProfileDef p, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos);
			p.mOverallWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mOverallDepth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mWebThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFlangeThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema != ReleaseVersion.IFC2x3) 
			{
				p.mFlangeEdgeRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mFlangeSlope = ParserSTEP.ParseDouble(arrFields[ipos++]);
			}
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mOverallWidth) + "," + ParserSTEP.DoubleToString(mOverallDepth) + "," + ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mFlangeThickness) + "," + ParserSTEP.DoubleOptionalToString(mFilletRadius)
				+ (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.DoubleOptionalToString(mFlangeEdgeRadius) + "," + ParserSTEP.DoubleOptionalToString(mFlangeSlope));
		}

		internal override double ProfileDepth { get { return OverallDepth; } }
		internal override double ProfileWidth { get { return OverallWidth; } }
	} 
}
