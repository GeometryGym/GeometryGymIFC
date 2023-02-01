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
		public string UrlReference { get { return mUrlReference; } set { mUrlReference = value; } }

		internal IfcImageTexture() : base() { }
		internal IfcImageTexture(DatabaseIfc db, IfcImageTexture t) : base(db, t) { mUrlReference = t.mUrlReference; }
		public IfcImageTexture(DatabaseIfc db, bool repeatS, bool repeatT, string urlReference) : base(db, repeatS, repeatT) { UrlReference = urlReference; }
	}
	[Serializable]
	public partial class IfcImpactProtectionDevice : IfcElementComponent
	{
		private IfcImpactProtectionDeviceTypeEnum mPredefinedType = IfcImpactProtectionDeviceTypeEnum.NOTDEFINED; //: OPTIONAL IfcImpactProtectionDeviceTypeEnum;
		public IfcImpactProtectionDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcImpactProtectionDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcImpactProtectionDevice() : base() { }
		public IfcImpactProtectionDevice(DatabaseIfc db) : base(db) { }
		public IfcImpactProtectionDevice(DatabaseIfc db, IfcImpactProtectionDevice impactProtectionDevice, DuplicateOptions options) : base(db, impactProtectionDevice, options) { PredefinedType = impactProtectionDevice.PredefinedType; }
		public IfcImpactProtectionDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcImpactProtectionDeviceType : IfcElementComponentType
	{
		private IfcImpactProtectionDeviceTypeEnum mPredefinedType = IfcImpactProtectionDeviceTypeEnum.NOTDEFINED; //: IfcImpactProtectionDeviceTypeEnum;
		public IfcImpactProtectionDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcImpactProtectionDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcImpactProtectionDeviceType() : base() { }
		public IfcImpactProtectionDeviceType(DatabaseIfc db, IfcImpactProtectionDeviceType impactProtectionDeviceType, DuplicateOptions options) : base(db, impactProtectionDeviceType, options) { PredefinedType = impactProtectionDeviceType.PredefinedType; }
		public IfcImpactProtectionDeviceType(DatabaseIfc db, string name, IfcImpactProtectionDeviceTypeEnum predefinedType)
			: base(db, name) { PredefinedType = predefinedType; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcImprovedGround : IfcEarthworksElement
	{
		private IfcImprovedGroundTypeEnum mPredefinedType = IfcImprovedGroundTypeEnum.NOTDEFINED; //: IfcImprovedGroundTypeEnum
		public IfcImprovedGroundTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcImprovedGroundTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X4_DRAFT : mDatabase.Release); } }

		public IfcImprovedGround() : base() { }
		public IfcImprovedGround(DatabaseIfc db) : base(db) { }
		public IfcImprovedGround(DatabaseIfc db, IfcImprovedGround ground, DuplicateOptions options) : base(db, ground, options) { PredefinedType = ground.PredefinedType; }
		public IfcImprovedGround(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcIndexedColourMap : IfcPresentationItem
	{
		internal IfcTessellatedFaceSet mMappedTo;// : IfcTessellatedFaceSet; 
		internal double mOpacity = double.NaN;// : OPTIONAL IfcNormalisedRatioMeasure;
		internal IfcColourRgbList mColours;// : IfcColourRgbList; 
		internal List<int> mColourIndex = new List<int>();// : LIST [1:?] OF IfcPositiveInteger;

		public IfcTessellatedFaceSet MappedTo { get { return mMappedTo; } set { mMappedTo = value; value.HasColours = this; } }
		public double Opacity { get { return mOpacity; } set { mOpacity = value; } }
		public IfcColourRgbList Colours { get { return mColours; } set { mColours = value; } }

		internal IfcIndexedColourMap() : base() { }
		internal IfcIndexedColourMap(DatabaseIfc db, IfcIndexedColourMap m) : base(db, m) { MappedTo = db.Factory.Duplicate(m.MappedTo) as IfcTessellatedFaceSet; Colours = db.Factory.Duplicate(m.Colours) as IfcColourRgbList; mColourIndex.AddRange(m.mColourIndex); }
		public IfcIndexedColourMap(IfcTessellatedFaceSet fs, IfcColourRgbList colours, IEnumerable<int> colourindex)
			: base(fs.mDatabase) { MappedTo = fs; mColours = colours; mColourIndex.AddRange(colourindex); }
	}
	[Serializable]
	public partial class IfcIndexedPolyCurve : IfcBoundedCurve 
	{
		private IfcCartesianPointList mPoints; // IfcCartesianPointList
		internal LIST<IfcSegmentIndexSelect> mSegments = new LIST<IfcSegmentIndexSelect>();// OPTIONAL LIST [1:?] OF IfcSegmentIndexSelect;
		internal IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// IfcLogical

		public IfcCartesianPointList Points { get { return mPoints; } set { mPoints = value; } }
		public LIST<IfcSegmentIndexSelect> Segments { get { return mSegments; } }
		public bool SelfIntersect { get { return mSelfIntersect == IfcLogicalEnum.TRUE; } set { mSelfIntersect = (value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); } }

		internal IfcIndexedPolyCurve() : base() { }
		internal IfcIndexedPolyCurve(DatabaseIfc db, IfcIndexedPolyCurve c, DuplicateOptions options) : base(db, c, options) { Points = db.Factory.Duplicate(c.Points) as IfcCartesianPointList; mSegments.AddRange(c.mSegments); mSelfIntersect = c.mSelfIntersect; }
		public IfcIndexedPolyCurve(IfcCartesianPointList pl) : base(pl.mDatabase) { Points = pl; }
		public IfcIndexedPolyCurve(IfcCartesianPointList pl, IEnumerable<IfcSegmentIndexSelect> segments) 
			: this(pl) { mSegments.AddRange(segments); }
		public IfcIndexedPolyCurve(IfcCartesianPointList pl, params IfcSegmentIndexSelect[] segments) : this(pl)
		{
			mSegments.AddRange(segments);
		}
	}
	[Serializable]
	public partial class IfcIndexedPolygonalFace : IfcTessellatedItem //SUPERTYPE OF (ONEOF (IfcIndexedPolygonalFaceWithVoids))
	{
		internal List<int> mCoordIndex = new List<int>();// : LIST [3:?] OF IfcPositiveInteger;
		 //INVERSE
		internal SET<IfcPolygonalFaceSet> mToFaceSet = new SET<IfcPolygonalFaceSet>();
		internal IfcTextureCoordinateIndices mHasTexCoords = null;// : SET[0:1] OF IfcTextureCoordinateIndices FOR TexCoordsOf;
		public List<int> CoordIndex { get { return mCoordIndex; } }
		public SET<IfcPolygonalFaceSet> ToFaceSet { get { return mToFaceSet; } }
		public IfcTextureCoordinateIndices HasTexCoords { get { return mHasTexCoords; } set { mHasTexCoords = value; } }

		internal IfcIndexedPolygonalFace() : base() { }
		internal IfcIndexedPolygonalFace(DatabaseIfc db, IfcIndexedPolygonalFace f, DuplicateOptions options) : base(db, f, options) { mCoordIndex.AddRange(f.mCoordIndex); }
		public IfcIndexedPolygonalFace(DatabaseIfc db, IEnumerable<int> coords) : base(db) { mCoordIndex = coords.ToList(); }
		public IfcIndexedPolygonalFace(DatabaseIfc db, int c1, int c2, int c3) : base(db) { mCoordIndex.Add(c1); mCoordIndex.Add(c2); mCoordIndex.Add(c3); }
		public IfcIndexedPolygonalFace(DatabaseIfc db, int c1, int c2, int c3, int c4) : this(db, c1, c2, c3) { mCoordIndex.Add(c4); }
	}
	[Serializable]
	public partial class IfcIndexedPolygonalFaceWithVoids : IfcIndexedPolygonalFace
	{
		internal List<List<int>> mInnerCoordIndices = new List<List<int>>();// : List[1:?] LIST [3:?] OF IfcPositiveInteger;
		public List<List<int>> InnerCoordIndices { get { return mInnerCoordIndices; }  }
		internal IfcIndexedPolygonalFaceWithVoids() : base() { }
		internal IfcIndexedPolygonalFaceWithVoids(DatabaseIfc db, IfcIndexedPolygonalFaceWithVoids f, DuplicateOptions options) : base(db, f, options) { mInnerCoordIndices.AddRange(f.mInnerCoordIndices); }
		public IfcIndexedPolygonalFaceWithVoids(DatabaseIfc db, IEnumerable<int> coords, IEnumerable<int> inner) 
			: base(db, coords) { mInnerCoordIndices.Add(inner.ToList()); }
		public IfcIndexedPolygonalFaceWithVoids(DatabaseIfc db, IEnumerable<int> coords, IEnumerable<List<int>> inners) 
			: base(db, coords) { mInnerCoordIndices.AddRange(inners); }
	}
	[Serializable]
	public partial class IfcIndexedPolygonalTextureMap : IfcIndexedTextureMap
	{
		private LIST<IfcTextureCoordinateIndices> mTexCoordIndices = new LIST<IfcTextureCoordinateIndices>();// : SET [1:?] OF IfcTextureCoordinateIndices;
		public LIST<IfcTextureCoordinateIndices> TexCoordIndices { get { return mTexCoordIndices; } set { mTexCoordIndices = value; } }

		internal IfcIndexedPolygonalTextureMap() : base() { }
		internal IfcIndexedPolygonalTextureMap(DatabaseIfc db) : base(db) { }
		internal IfcIndexedPolygonalTextureMap(DatabaseIfc db, IfcIndexedPolygonalTextureMap m) : base(db, m)
		{
			foreach (IfcTextureCoordinateIndices i in m.mTexCoordIndices)
				TexCoordIndices.Add(db.Factory.Duplicate(i) as IfcTextureCoordinateIndices);
		}
	}
	[Serializable]
	public abstract partial class IfcIndexedTextureMap : IfcTextureCoordinate // ABSTRACT SUPERTYPE OF(IfcIndexedPolygonalTextureMap, IfcIndexedTriangleTextureMap)
	{
		internal IfcTessellatedFaceSet mMappedTo;// : IfcTessellatedFaceSet;
		internal IfcTextureVertexList mTexCoords;// : IfcTextureVertexList;

		public IfcTessellatedFaceSet MappedTo { get { return mMappedTo; } set { mMappedTo = value; mMappedTo.mHasTextures.Add(this); } }
		public IfcTextureVertexList TexCoords { get { return mTexCoords; } set { mTexCoords = value; } }

		protected IfcIndexedTextureMap() : base() { }
		protected IfcIndexedTextureMap(DatabaseIfc db) : base(db) { }
		protected IfcIndexedTextureMap(DatabaseIfc db, IfcIndexedTextureMap m) : base(db, m) { MappedTo = db.Factory.Duplicate(m.MappedTo) as IfcTessellatedFaceSet; TexCoords = db.Factory.Duplicate(m.TexCoords) as IfcTextureVertexList; }
	}
	[Serializable]
	public partial class IfcIndexedTriangleTextureMap : IfcIndexedTextureMap
	{
		internal List<Tuple<int, int, int>> mTexCoordList = new List<Tuple<int, int, int>>();// : OPTIONAL LIST [1:?] OF LIST [3:3] OF IfcPositiveInteger;

		internal IfcIndexedTriangleTextureMap() : base() { }
		internal IfcIndexedTriangleTextureMap(DatabaseIfc db) : base(db) { }
		internal IfcIndexedTriangleTextureMap(DatabaseIfc db, IfcIndexedTriangleTextureMap m) : base(db, m) { mTexCoordList = m.mTexCoordList; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcIntegerVoxelData : IfcVoxelData
	{
		internal int[] mValues = new int[0];// :	ARRAY [1:?] OF IfcInteger;
		internal IfcUnit mUnit = null;// :	OPTIONAL IfcUnit;

		public int[] Values { get { return mValues; } set { mValues = value; } }
		public IfcUnit Unit { get { return mUnit; } set { mUnit = value; } }

		internal IfcIntegerVoxelData() : base() { }
		internal IfcIntegerVoxelData(DatabaseIfc db, IfcIntegerVoxelData d, DuplicateOptions options) : base(db, d, options) { mValues = d.mValues; if (d.Unit != null) Unit = db.Factory.Duplicate(d.Unit); }
		public IfcIntegerVoxelData(IfcProduct host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, int[] values)
			: base(host, placement, representation) { Values = values; }
	}
	[Serializable]
	public partial class IfcInterceptor : IfcFlowTreatmentDevice //IFC4  
	{
		private IfcInterceptorTypeEnum mPredefinedType = IfcInterceptorTypeEnum.NOTDEFINED;
		public IfcInterceptorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcInterceptorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcInterceptor() : base() { }
		internal IfcInterceptor(DatabaseIfc db, IfcInterceptor i, DuplicateOptions options) : base(db, i, options) { PredefinedType = i.PredefinedType; }
		public IfcInterceptor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcInterceptorType : IfcFlowTreatmentDeviceType
	{
		private IfcInterceptorTypeEnum mPredefinedType = IfcInterceptorTypeEnum.NOTDEFINED;// : IfcInterceptorTypeEnum;
		public IfcInterceptorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcInterceptorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcInterceptorType() : base() { }
		internal IfcInterceptorType(DatabaseIfc db, IfcInterceptorType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcInterceptorType(DatabaseIfc db, string name, IfcInterceptorTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	public interface IfcInterferenceSelect : IBaseClassIfc // SELECT(IfcSpatialElement, IfcElement);
	{
		SET<IfcRelInterferesElements> IsInterferedByElements { get; } 
		SET<IfcRelInterferesElements> InterferesElements { get; } 
	}
	[Serializable]
	public partial class IfcIntersectionCurve : IfcSurfaceCurve //IFC4 Add2
	{
		internal IfcIntersectionCurve() : base() { }
		internal IfcIntersectionCurve(DatabaseIfc db, IfcIntersectionCurve c, DuplicateOptions options) : base(db, c, options) { }
		internal IfcIntersectionCurve(IfcCurve curve3D, IfcPcurve p1, IfcPcurve p2, IfcPreferredSurfaceCurveRepresentation cr) : base(curve3D, p1, p2, cr) { }
	}
	[Serializable]
	public partial class IfcInventory : IfcGroup
	{
		internal IfcInventoryTypeEnum mPredefinedType;// : IfcInventoryTypeEnum;
		internal IfcActorSelect mJurisdiction;// : IfcActorSelect;
		internal SET<IfcPerson> mResponsiblePersons = new SET<IfcPerson>();// : SET [1:?] OF IfcPerson;
		internal DateTime mLastUpdateDate = DateTime.MinValue;// : IfcDate
		internal IfcCalendarDate mLastUpdateDateSS;// : IfcCalendarDate;
		internal IfcCostValue mCurrentValue;// : OPTIONAL IfcCostValue;
		internal IfcCostValue mOriginalValue;// : OPTIONAL IfcCostValue;

		public IfcInventoryTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcInventoryTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcInventory() : base() { }
		internal IfcInventory(DatabaseIfc db, IfcInventory i, DuplicateOptions options) : base(db, i, options)
		{
			PredefinedType = i.PredefinedType;
			mJurisdiction = db.Factory.Duplicate(i.mJurisdiction, options);
			mResponsiblePersons.AddRange(i.mResponsiblePersons.Select(x=>db.Factory.Duplicate(x, options)));
			mLastUpdateDate = i.mLastUpdateDate;
			if (mLastUpdateDateSS != null)
				mLastUpdateDateSS = db.Factory.Duplicate(i.mLastUpdateDateSS, options);
			if(i.mCurrentValue != null)
				mCurrentValue = db.Factory.Duplicate(i.mCurrentValue, options);
			if(i.mOriginalValue != null)
				mOriginalValue = db.Factory.Duplicate(i.mOriginalValue, options);
		}
		internal IfcInventory(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Serializable]
	public partial class IfcIrregularTimeSeries : IfcTimeSeries
	{
		private LIST<IfcIrregularTimeSeriesValue> mValues = new LIST<IfcIrregularTimeSeriesValue>(); //: LIST[1:?] OF IfcIrregularTimeSeriesValue;
		public LIST<IfcIrregularTimeSeriesValue> Values { get { return mValues; } set { mValues = value; } }

		public IfcIrregularTimeSeries() : base() { }
		public IfcIrregularTimeSeries(string name, IfcDateTimeSelect startTime, IfcDateTimeSelect endTime, IfcTimeSeriesDataTypeEnum timeSeriesDataType, IfcDataOriginEnum dataOrigin, IEnumerable<IfcIrregularTimeSeriesValue> values)
			: base(name, startTime, endTime, timeSeriesDataType, dataOrigin)
		{
			Values.AddRange(values);
		}
	}
	[Serializable]
	public partial class IfcIrregularTimeSeriesValue : BaseClassIfc
	{
		private DateTime mTimeStamp = DateTime.MinValue; //: IfcDateTime;
		private LIST<IfcValue> mListValues = new LIST<IfcValue>(); //: LIST[1:?] OF IfcValue;

		public DateTime TimeStamp { get { return mTimeStamp; } set { mTimeStamp = value; } }
		public LIST<IfcValue> ListValues { get { return mListValues; } set { mListValues = value; } }

		public IfcIrregularTimeSeriesValue() : base() { }
		public IfcIrregularTimeSeriesValue(DatabaseIfc db, DateTime timeStamp, IEnumerable<IfcValue> listValues)
			: base(db)
		{
			TimeStamp = timeStamp;
			ListValues.AddRange(listValues);
		}
	}
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
		internal IfcIShapeProfileDef(DatabaseIfc db, IfcIShapeProfileDef p, DuplicateOptions options) : base(db, p, options) { mOverallWidth = p.mOverallWidth; mOverallDepth = p.mOverallDepth; mWebThickness = p.mWebThickness; mFlangeThickness = p.mFlangeThickness; mFilletRadius = p.mFilletRadius; mFlangeEdgeRadius = p.mFlangeEdgeRadius; mFlangeSlope = p.mFlangeSlope; }
		public IfcIShapeProfileDef(DatabaseIfc db, string name, double overallWidth, double overallDepth, double webThickness, double flangeThickness)
			: base(db,name) {  mOverallWidth = overallWidth; mOverallDepth = overallDepth; mWebThickness = webThickness; mFlangeThickness = flangeThickness; }

		internal override double ProfileDepth { get { return OverallDepth; } }
		internal override double ProfileWidth { get { return OverallWidth; } }
	} 
}
