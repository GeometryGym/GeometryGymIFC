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
	[Serializable]
	public partial class IfcImageTexture : IfcSurfaceTexture
	{
		internal string mUrlReference;// : IfcIdentifier; 
		public string UrlReference { get { return ParserIfc.Decode(mUrlReference); } set { mUrlReference = ParserIfc.Encode(value); } }

		internal IfcImageTexture() : base() { }
		internal IfcImageTexture(DatabaseIfc db, IfcImageTexture t) : base(db, t) { mUrlReference = t.mUrlReference; }
		public IfcImageTexture(DatabaseIfc db, bool repeatS, bool repeatT, string urlReference) : base(db, repeatS, repeatT) { UrlReference = urlReference; }
	}
	[Serializable]
	public partial class IfcIndexedColourMap : IfcPresentationItem
	{
		internal int mMappedTo;// : IfcTessellatedFaceSet; 
		internal double mOpacity = double.NaN;// : OPTIONAL IfcNormalisedRatioMeasure;
		internal int mColours;// : IfcColourRgbList; 
		internal List<int> mColourIndex = new List<int>();// : LIST [1:?] OF IfcPositiveInteger;

		public IfcTessellatedFaceSet MappedTo { get { return mDatabase[mMappedTo] as IfcTessellatedFaceSet; } set { mMappedTo = value.mIndex; } }
		public double Opacity { get { return mOpacity; } set { mOpacity = value; } }
		public IfcColourRgbList Colours { get { return mDatabase[mColours] as IfcColourRgbList; } set { mColours = value.mIndex; } }

		internal IfcIndexedColourMap() : base() { }
		internal IfcIndexedColourMap(DatabaseIfc db, IfcIndexedColourMap m) : base(db, m) { MappedTo = db.Factory.Duplicate(m.MappedTo) as IfcTessellatedFaceSet; Colours = db.Factory.Duplicate(m.Colours) as IfcColourRgbList; mColourIndex.AddRange(m.mColourIndex); }
		public IfcIndexedColourMap(IfcTessellatedFaceSet fs, IfcColourRgbList colours, IEnumerable<int> colourindex)
			: base(fs.mDatabase) { mMappedTo = fs.mIndex; mColours = colours.mIndex; mColourIndex.AddRange(colourindex); }
	}
	[Serializable]
	public partial class IfcIndexedPolyCurve : IfcBoundedCurve 
	{
		private IfcCartesianPointList mPoints; // IfcCartesianPointList
		internal List<IfcSegmentIndexSelect> mSegments = new List<IfcSegmentIndexSelect>();// OPTIONAL LIST [1:?] OF IfcSegmentIndexSelect;
		internal IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// Optional IfcBoolean

		public IfcCartesianPointList Points { get { return mPoints; } set { mPoints = value; } }
		public ReadOnlyCollection<IfcSegmentIndexSelect> Segments { get { return new ReadOnlyCollection<IfcSegmentIndexSelect>( mSegments); } }
		public bool SelfIntersect { get { return mSelfIntersect == IfcLogicalEnum.TRUE; } set { mSelfIntersect = (value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); } }

		internal IfcIndexedPolyCurve() : base() { }
		internal IfcIndexedPolyCurve(DatabaseIfc db, IfcIndexedPolyCurve c) : base(db, c) { Points = db.Factory.Duplicate(c.Points) as IfcCartesianPointList; mSegments.AddRange(c.mSegments); mSelfIntersect = c.mSelfIntersect; }
		public IfcIndexedPolyCurve(IfcCartesianPointList pl) : base(pl.mDatabase) { Points = pl; }
		public IfcIndexedPolyCurve(IfcCartesianPointList pl, List<IfcSegmentIndexSelect> segs) : this(pl) { mSegments = segs; }

		internal void addSegment(IfcSegmentIndexSelect segment) { mSegments.Add(segment); }
		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			if (schema < ReleaseVersion.IFC4)
			{
				IfcCartesianPointList cpl = Points;
				IfcCartesianPointList2D cpl2d = cpl as IfcCartesianPointList2D;
				if (cpl2d != null)
				{
					IfcBoundedCurve bc = IfcBoundedCurve.Generate(mDatabase, cpl2d.mCoordList.Select(x=>new Tuple<double,double>(x[0],x[1])), Segments.ToList());
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
	[Serializable]
	public partial class IfcIndexedPolygonalFace : IfcTessellatedItem //SUPERTYPE OF (ONEOF (IfcIndexedPolygonalFaceWithVoids))
	{
		internal List<int> mCoordIndex = new List<int>();// : LIST [3:?] OF IfcPositiveInteger;
		 //INVERSE
		internal IfcPolygonalFaceSet mToFaceSet = null;

		public ReadOnlyCollection<int> CoordIndex { get { return new ReadOnlyCollection<int>( mCoordIndex); } }
		public IfcPolygonalFaceSet ToFaceSet { get { return mToFaceSet; } set { mToFaceSet = value; } }

		internal IfcIndexedPolygonalFace() : base() { }
		internal IfcIndexedPolygonalFace(DatabaseIfc db, IfcIndexedPolygonalFace f) : base(db, f) { mCoordIndex.AddRange(f.mCoordIndex); }
		public IfcIndexedPolygonalFace(DatabaseIfc db, IEnumerable<int> coords) : base(db) { mCoordIndex = coords.ToList(); }

		public void AddCoordIndex(int index) { mCoordIndex.Add(index); }
	}
	[Serializable]
	public partial class IfcIndexedPolygonalFaceWithVoids : IfcIndexedPolygonalFace
	{
		internal List<List<int>> mInnerCoordIndices = new List<List<int>>();// : List[1:?] LIST [3:?] OF IfcPositiveInteger;
		public ReadOnlyCollection<ReadOnlyCollection<int>> InnerCoordIndices { get { return new ReadOnlyCollection<ReadOnlyCollection<int>>( mInnerCoordIndices.ConvertAll(x=> new ReadOnlyCollection<int>(x))); }  }
		internal IfcIndexedPolygonalFaceWithVoids() : base() { }
		internal IfcIndexedPolygonalFaceWithVoids(DatabaseIfc db, IfcIndexedPolygonalFaceWithVoids f) : base(db, f) { mInnerCoordIndices.AddRange(f.mInnerCoordIndices); }
		public IfcIndexedPolygonalFaceWithVoids(DatabaseIfc db, IEnumerable<int> coords, IEnumerable<IEnumerable<int>> inners) 
			: base(db, coords) { mInnerCoordIndices = inners.ToList().ConvertAll(x=>x.ToList()); }
	}
	[Serializable]
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
	}
	[Serializable]
	public partial class IfcIndexedTriangleTextureMap : IfcIndexedTextureMap
	{
		internal Tuple<int, int, int>[] mTexCoordList = new Tuple<int, int, int>[0];// : OPTIONAL LIST [1:?] OF LIST [3:3] OF IfcPositiveInteger;

		internal IfcIndexedTriangleTextureMap() : base() { }
		internal IfcIndexedTriangleTextureMap(DatabaseIfc db, IfcIndexedTriangleTextureMap m) : base(db, m) { mTexCoordList = m.mTexCoordList; }
		//public IfcIndexedTriangleTextureMap(DatabaseIfc m, IEnumerable<Tuple<int, int,int>> coords) : base(m) { mTexCoordList = coords.ToArray(); }
	}
	[Serializable]
	public partial class IfcInterceptor : IfcFlowTreatmentDevice //IFC4  
	{
		internal IfcInterceptorTypeEnum mPredefinedType = IfcInterceptorTypeEnum.NOTDEFINED;
		public IfcInterceptorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcInterceptor() : base() { }
		internal IfcInterceptor(DatabaseIfc db, IfcInterceptor i, IfcOwnerHistory ownerHistory, bool downStream) : base(db, i, ownerHistory, downStream) { mPredefinedType = i.mPredefinedType; }
		public IfcInterceptor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcInterceptorType : IfcFlowTreatmentDeviceType
	{
		internal IfcInterceptorTypeEnum mPredefinedType = IfcInterceptorTypeEnum.NOTDEFINED;// : IfcInterceptorTypeEnum;
		public IfcInterceptorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcInterceptorType() : base() { }
		internal IfcInterceptorType(DatabaseIfc db, IfcInterceptorType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcInterceptorType(DatabaseIfc db, string name, IfcInterceptorTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcIntersectionCurve : IfcSurfaceCurve //IFC4 Add2
	{
		internal IfcIntersectionCurve() : base() { }
		internal IfcIntersectionCurve(DatabaseIfc db, IfcIntersectionCurve c) : base(db, c) { }
		internal IfcIntersectionCurve(IfcCurve curve, IfcPCurve p1, IfcPCurve p2, IfcPreferredSurfaceCurveRepresentation cr) : base(curve,p1,p2,cr) { }
	}
	[Serializable]
	public partial class IfcInventory : IfcGroup
	{
		internal IfcInventoryTypeEnum mInventoryType;// : IfcInventoryTypeEnum;
		internal int mJurisdiction;// : IfcActorSelect;
		internal List<int> mResponsiblePersons = new List<int>();// : SET [1:?] OF IfcPerson;
		internal int mLastUpdateDate;// : IfcCalendarDate;
		internal int mCurrentValue;// : OPTIONAL IfcCostValue;
		internal int mOriginalValue;// : OPTIONAL IfcCostValue;
		internal IfcInventory() : base() { }
		internal IfcInventory(DatabaseIfc db, IfcInventory i, IfcOwnerHistory ownerHistory, bool downStream) : base(db, i, ownerHistory, downStream)
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
	}
	//ENTITY IfcIrregularTimeSeries
	//ENTITY IfcIrregularTimeSeriesValue;
	[Serializable]
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
		public IfcIShapeProfileDef(DatabaseIfc db, string name, double overallWidth, double overallDepth, double webThickness, double flangeThickness)
			: base(db,name) {  mOverallWidth = overallWidth; mOverallDepth = overallDepth; mWebThickness = webThickness; mFlangeThickness = flangeThickness; }

		internal override double ProfileDepth { get { return OverallDepth; } }
		internal override double ProfileWidth { get { return OverallWidth; } }
	} 
}
