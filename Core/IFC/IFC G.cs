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
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcGasTerminalType : IfcFlowTerminalType // DEPRECATED IFC4
	{
		internal IfcGasTerminalTypeEnum mPredefinedType = IfcGasTerminalTypeEnum.NOTDEFINED;// : IfcGasTerminalBoxTypeEnum;
		public IfcGasTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcGasTerminalType() : base() { }
		internal IfcGasTerminalType(DatabaseIfc db, IfcGasTerminalType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcGasTerminalType(DatabaseIfc m, string name, IfcGasTerminalTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcGeneralMaterialProperties : IfcMaterialProperties // DEPRECATED IFC4
	{
		internal double mMolecularWeight = double.NaN; //: OPTIONAL IfcMolecularWeightMeasure;
		internal double mPorosity = double.NaN; //: OPTIONAL IfcNormalisedRatioMeasure;
		internal double mMassDensity = double.NaN;//OPTIONAL IfcMassDensityMeasure

		public double MolecularWeight { get { return mMolecularWeight; } set { mMolecularWeight = value; } }
		public double Porosity { get { return mPorosity; } set { mPorosity = value; } }
		public double MassDensity { get { return mMassDensity; } set { mMassDensity = value; } } 

		internal IfcGeneralMaterialProperties() : base() { }
		internal IfcGeneralMaterialProperties(DatabaseIfc db, IfcGeneralMaterialProperties p, DuplicateOptions options) : base(db, p, options) { mMolecularWeight = p.mMolecularWeight; mPorosity = p.mPorosity; mMassDensity = p.mMassDensity; }
		public IfcGeneralMaterialProperties(IfcMaterial material) : base(material) { }
	}
	[Obsolete("DELETED IFC4", false)]
	[Serializable]
	public partial class IfcGeneralProfileProperties : IfcProfileProperties //DELETED IFC4  SUPERTYPE OF	(IfcStructuralProfileProperties)
	{ 
		internal double mPhysicalWeight = double.NaN;// : OPTIONAL IfcMassPerLengthMeasure;
		internal double mPerimeter = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mMinimumPlateThickness = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mMaximumPlateThickness = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mCrossSectionArea = double.NaN;// : OPTIONAL IfcAreaMeasure;

		public double PhysicalWeight { get { return mPhysicalWeight; } set { mPhysicalWeight = value; } }
		public double Perimeter { get { return mPerimeter; } set { mPerimeter = value; } }
		public double MinimumPlateThickness { get { return mMinimumPlateThickness; } set { mMinimumPlateThickness = value; } }
		public double MaximumPlateThickness { get { return mMaximumPlateThickness; } set { mMaximumPlateThickness = value; } }
		public double CrossSectionArea { get { return mCrossSectionArea; } set { mCrossSectionArea = value; } }

		internal IfcGeneralProfileProperties() : base() { }
		internal IfcGeneralProfileProperties(DatabaseIfc db, IfcGeneralProfileProperties p, DuplicateOptions options) : base(db, p, options) { mPhysicalWeight = p.mPhysicalWeight; mPerimeter = p.mPerimeter; mMinimumPlateThickness = p.mMinimumPlateThickness; mMaximumPlateThickness = p.mMaximumPlateThickness; mCrossSectionArea = p.mCrossSectionArea; }
		public IfcGeneralProfileProperties(IfcProfileDef p) : base(p) { }
	}
	[Serializable]
	public partial class IfcGeographicElement : IfcElement  //IFC4
	{
		internal IfcGeographicElementTypeEnum mPredefinedType = IfcGeographicElementTypeEnum.NOTDEFINED;// OPTIONAL IfcGeographicElementTypeEnum; 
		public IfcGeographicElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcGeographicElement() : base() { }
		internal IfcGeographicElement(DatabaseIfc db) : base(db) { }
		internal IfcGeographicElement(DatabaseIfc db, IfcGeographicElement e, DuplicateOptions options) : base(db, e, options) { mPredefinedType = e.mPredefinedType; }
		public IfcGeographicElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { if (mDatabase.mRelease < ReleaseVersion.IFC4) throw new Exception(StepClassName + " only supported in IFC4!"); }
	}
	public partial class IfcGeographicElementType : IfcElementType //IFC4
	{
		internal IfcGeographicElementTypeEnum mPredefinedType = IfcGeographicElementTypeEnum.NOTDEFINED;// IfcGeographicElementTypeEnum; 
		public IfcGeographicElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcGeographicElementType() : base() { }
		internal IfcGeographicElementType(DatabaseIfc db, IfcGeographicElementType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcGeographicElementType(DatabaseIfc m, string name, IfcGeographicElementTypeEnum type) : base(m) { Name = name; mPredefinedType = type; if (m.mRelease < ReleaseVersion.IFC4) throw new Exception(StepClassName + " only supported in IFC4!"); }
	}
	[Serializable]
	public partial class IfcGeometricCurveSet : IfcGeometricSet
	{
		internal IfcGeometricCurveSet() : base() { }
		internal IfcGeometricCurveSet(DatabaseIfc db, IfcGeometricCurveSet s, DuplicateOptions options) : base(db, s, options) { }
		public IfcGeometricCurveSet(IfcGeometricSetSelect element) : base(element) { if(element is IfcSurface) throw new Exception("XXX Error, IfcSurface cannot be added to IfcGeometricCurveSet " + mIndex); }
		public IfcGeometricCurveSet(IEnumerable<IfcGeometricSetSelect> set) : base(set)
		{
			foreach(IfcGeometricSetSelect item in set)
			{
				if(item is IfcSurface)
					throw new Exception("XXX Error, IfcSurface cannot be added to IfcGeometricCurveSet " + mIndex);
			}
		}
	}
	[Serializable]
	public partial class IfcGeometricRepresentationContext : IfcRepresentationContext, IfcCoordinateReferenceSystemSelect // SUPERTYPE OF(IfcGeometricRepresentationSubContext)
	{
		public enum GeometricContextIdentifier { Model, Plan, NotDefined };

		internal int mCoordinateSpaceDimension;// : IfcDimensionCount;
		internal double mPrecision = 1e-8;// : OPTIONAL REAL;
		internal IfcAxis2Placement mWorldCoordinateSystem = null;// : IfcAxis2Placement;
		internal IfcDirection mTrueNorth = null;// : OPTIONAL IfcDirection; 
		//INVERSE
		internal List<IfcGeometricRepresentationSubContext> mHasSubContexts = new List<IfcGeometricRepresentationSubContext>();//	 :	SET OF IfcGeometricRepresentationSubContext FOR ParentContext;
		private IfcCoordinateOperation mHasCoordinateOperation = null; //IFC4

		public int CoordinateSpaceDimension { get { return mCoordinateSpaceDimension; } set { mCoordinateSpaceDimension = value; } }
		public double Precision { get { return mPrecision; } set { mPrecision = value; } }
		public IfcAxis2Placement WorldCoordinateSystem { get { return mWorldCoordinateSystem; } set { mWorldCoordinateSystem = value; } }
		public IfcDirection TrueNorth 
		{ 
			get { return mTrueNorth; }  
			set { mTrueNorth = value; if (value != null) value.DirectionRatioZ = double.NaN; } 
		}
		public List<IfcGeometricRepresentationSubContext> HasSubContexts { get { return mHasSubContexts; } }
		public IfcCoordinateOperation HasCoordinateOperation { get { return mHasCoordinateOperation; } set { mHasCoordinateOperation = value; if(value.mSourceCRS != this) value.SourceCRS = this; } }
		
		internal IfcGeometricRepresentationContext() : base() { }
		protected IfcGeometricRepresentationContext(DatabaseIfc db) : base(db) { }
		internal IfcGeometricRepresentationContext(DatabaseIfc db, IfcGeometricRepresentationContext c) : base(db, c)
		{
			mCoordinateSpaceDimension = c.mCoordinateSpaceDimension;
			mPrecision = c.mPrecision;
			if(c.mWorldCoordinateSystem != null)
				WorldCoordinateSystem = db.Factory.Duplicate(c.mDatabase[c.mWorldCoordinateSystem.Index]) as IfcAxis2Placement;
			if (c.mTrueNorth != null)
				TrueNorth = db.Factory.Duplicate(c.TrueNorth) as IfcDirection;

			foreach (IfcGeometricRepresentationSubContext sc in mHasSubContexts)
				db.Factory.Duplicate(sc);
		}
		internal IfcGeometricRepresentationContext(DatabaseIfc db, int SpaceDimension, double precision) : base(db)
		{
			if (db.Context != null)
				db.Context.RepresentationContexts.Add(this);
			mCoordinateSpaceDimension = SpaceDimension;
			mPrecision = Math.Max(1e-8, precision);
			WorldCoordinateSystem = new IfcAxis2Placement3D(new IfcCartesianPoint(db,0,0,0));
			TrueNorth = new IfcDirection(mDatabase, 0, 1);
		}
		public IfcGeometricRepresentationContext(int coordinateSpaceDimension, IfcAxis2Placement worldCoordinateSystem) : base(worldCoordinateSystem.Database) { mCoordinateSpaceDimension = coordinateSpaceDimension; WorldCoordinateSystem = worldCoordinateSystem; }
	}
	[Serializable]
	public abstract partial class IfcGeometricRepresentationItem : IfcRepresentationItem 
	{
		//ABSTRACT SUPERTYPE OF (ONEOF (IfcAnnotationFillArea, IfcBooleanResult, IfcBoundingBox, IfcCartesianTransformationOperator, 
		//		IfcCompositeCurveSegment, IfcCsgPrimitive3D, IfcCurve, IfcDirection,IfcFaceBasedSurfaceModel, IfcFillAreaStyleHatching, 
		//		IfcFillAreaStyleTiles, IfcGeometricSet, IfcHalfSpaceSolid, IfcLightSource, IfcPlacement, IfcPlanarExtent, IfcPoint,
		//		IfcSectionedSpine, IfcShellBasedSurfaceModel, IfcSolidModel, IfcSurface, IfcTextLiteral, IfcVector))
		// IFC2x3 IfcAnnotationSurface, IfcDefinedSymbol, IfcDraughtingCallout, IfcFillAreaStyleTileSymbolWithStyle, IfcOneDirectionRepeatFactor
		// IFC4 IfcCartesianPointList, IfcTessellatedItem
		protected IfcGeometricRepresentationItem() : base() { }
		protected IfcGeometricRepresentationItem(DatabaseIfc db) : base(db) { }
		protected IfcGeometricRepresentationItem(DatabaseIfc db, IfcGeometricRepresentationItem i, DuplicateOptions options) : base(db, i, options) { }
	}
	[Serializable]
	public partial class IfcGeometricRepresentationSubContext : IfcGeometricRepresentationContext
	{
		public enum SubContextIdentifier { Axis, Body, BoundingBox, FootPrint, PlanSymbol3d, PlanSymbol2d, Reference, Profile, Row, Outline };//, PVI };// Surface };

		internal IfcGeometricRepresentationContext mParentContext;// : IfcGeometricRepresentationContext;
		internal double mTargetScale = double.NaN;// : OPTIONAL IfcPositiveRatioMeasure;
		private IfcGeometricProjectionEnum mTargetView;// : IfcGeometricProjectionEnum;
		internal string mUserDefinedTargetView = "$";// : OPTIONAL IfcLabel;

		public IfcGeometricRepresentationContext ParentContext { get { return mParentContext; } set { mParentContext = value; if(value != null) value.mHasSubContexts.Add(this); } }
		public double TargetScale { get { return mTargetScale; } set { mTargetScale = value; } }
		public IfcGeometricProjectionEnum TargetView { get { return mTargetView; } set { mTargetView = value; } }
		public string UserDefinedTargetView { get { return (mUserDefinedTargetView == "$" ? "" : ParserIfc.Decode(mUserDefinedTargetView)); } set { mUserDefinedTargetView = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcGeometricRepresentationSubContext() : base() { }
		internal IfcGeometricRepresentationSubContext(DatabaseIfc db, IfcGeometricRepresentationSubContext s) : base(db, s)
		{
			ParentContext = db.Factory.Duplicate(s.ParentContext) as IfcGeometricRepresentationContext;

			mTargetScale = s.mTargetScale;
			mTargetView = s.mTargetView;
			mUserDefinedTargetView = s.mUserDefinedTargetView;
		}
		public IfcGeometricRepresentationSubContext(IfcGeometricRepresentationContext container, IfcGeometricProjectionEnum view)
			: base(container.mDatabase)
		{
			ParentContext = container;
			mContextType = container.mContextType;
			mTargetView = view;
		}
	}
	[Serializable]
	public partial class IfcGeometricSet : IfcGeometricRepresentationItem //SUPERTYPE OF(IfcGeometricCurveSet)
	{
		private SET<IfcGeometricSetSelect> mElements = new SET<IfcGeometricSetSelect>(); //SET [1:?] OF IfcGeometricSetSelect; 
		public SET<IfcGeometricSetSelect> Elements { get { return mElements; } set { mElements = value; } }

		internal IfcGeometricSet() : base() { }
		internal IfcGeometricSet(DatabaseIfc db, IfcGeometricSet s, DuplicateOptions options) : base(db, s, options) { mElements.AddRange(s.mElements.ConvertAll(x=>db.Factory.Duplicate(s.mDatabase[x.Index]) as IfcGeometricSetSelect)); }
		public IfcGeometricSet(IfcGeometricSetSelect element) : base(element.Database) { mElements.Add(element); }
		public IfcGeometricSet(IEnumerable<IfcGeometricSetSelect> set) : base(set.First().Database) { mElements.AddRange(set); }
	}
	public partial interface IfcGeometricSetSelect : IBaseClassIfc { } //SELECT ( IfcPoint, IfcCurve,  IfcSurface);
	[Serializable]
	public partial class IfcGeomodel : IfcGeotechnicalAssembly
	{
		public IfcGeomodel() : base() { }
		public IfcGeomodel(DatabaseIfc db) : base(db) { }
		public IfcGeomodel(DatabaseIfc db, IfcGeomodel geomodel, DuplicateOptions options) : base(db, geomodel, options) {  }
		public IfcGeomodel(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcGeoslice : IfcGeotechnicalAssembly
	{
		public IfcGeoslice() : base() { }
		public IfcGeoslice(DatabaseIfc db) : base(db) { }
		public IfcGeoslice(DatabaseIfc db, IfcGeoslice geoslice, DuplicateOptions options) : base(db, geoslice, options) { }
		public IfcGeoslice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcGeotechnicalAssembly : IfcGeotechnicalElement
	{
		protected IfcGeotechnicalAssembly() : base() { }
		protected IfcGeotechnicalAssembly(DatabaseIfc db) : base(db) { }
		protected IfcGeotechnicalAssembly(DatabaseIfc db, IfcGeotechnicalAssembly geotechnicalAssembly, DuplicateOptions options) : base(db, geotechnicalAssembly, options) { }
		protected IfcGeotechnicalAssembly(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcGeotechnicalElement : IfcElement
	{
		protected IfcGeotechnicalElement() : base() { }
		protected IfcGeotechnicalElement(DatabaseIfc db) : base(db) { }
		protected IfcGeotechnicalElement(DatabaseIfc db, IfcGeotechnicalElement geotechnicalElement, DuplicateOptions options) : base(db, geotechnicalElement, options) { }
		protected IfcGeotechnicalElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcGeotechnicalStratum : IfcGeotechnicalElement
	{
		protected IfcGeotechnicalStratum() : base() { }
		protected IfcGeotechnicalStratum(DatabaseIfc db) : base(db) { }
		protected IfcGeotechnicalStratum(DatabaseIfc db, IfcGeotechnicalStratum geotechnicalStratum, DuplicateOptions options) : base(db, geotechnicalStratum, options) { }
		protected IfcGeotechnicalStratum(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation)
			: base(host.Database)
		{
			host.AddNested(this);
			ObjectPlacement = placement;
			Representation = representation;
		}
	}
	[Serializable]
	public partial class IfcGradientCurve : IfcCompositeCurve 
	{
		private IfcBoundedCurve mBaseCurve = null; //: IfcBoundedCurve;
		private IfcPlacement mEndPoint = null; //: OPTIONAL IfcPlacement;

		public IfcBoundedCurve BaseCurve { get { return mBaseCurve; } set { mBaseCurve = value; } }
		public IfcPlacement EndPoint { get { return mEndPoint; } set { mEndPoint = value; } }

		public IfcGradientCurve() : base() { }
		internal IfcGradientCurve(DatabaseIfc db, IfcGradientCurve c, DuplicateOptions options) : base(db, c, options)
		{
			mBaseCurve = db.Factory.Duplicate(c.mBaseCurve) as IfcBoundedCurve;
			mEndPoint = db.Factory.Duplicate(c.mEndPoint) as IfcPlacement;
		}
		public IfcGradientCurve(IfcBoundedCurve baseCurve, IEnumerable<IfcCurveSegment> segments)
			: base(segments) { BaseCurve = baseCurve; }
	}
	[Serializable]
	public partial class IfcGrid : IfcPositioningElement
	{
		private LIST<IfcGridAxis> mUAxes = new LIST<IfcGridAxis>();// : LIST [1:?] OF UNIQUE IfcGridAxis;
		private LIST<IfcGridAxis> mVAxes = new LIST<IfcGridAxis>();// : LIST [1:?] OF UNIQUE IfcGridAxis;
		private LIST<IfcGridAxis> mWAxes = new LIST<IfcGridAxis>();// : OPTIONAL LIST [1:?] OF UNIQUE IfcGridAxis;
		internal IfcGridTypeEnum mPredefinedType = IfcGridTypeEnum.NOTDEFINED;// :OPTIONAL IfcGridTypeEnum; //IFC4 CHANGE  New attribute.
		//INVERSE
		public IfcGridTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public LIST<IfcGridAxis> UAxes { get { return mUAxes; } set { mUAxes.Clear(); if (value != null) { mUAxes.CollectionChanged -= mUAxes_CollectionChanged; mUAxes = value; mUAxes.CollectionChanged += mUAxes_CollectionChanged; } } }
		public LIST<IfcGridAxis> VAxes { get { return mVAxes; } set { mVAxes.Clear(); if (value != null) { mVAxes.CollectionChanged -= mVAxes_CollectionChanged; mVAxes = value; mVAxes.CollectionChanged += mVAxes_CollectionChanged; } } }
		public LIST<IfcGridAxis> WAxes { get { return mWAxes; } set { mWAxes.Clear(); if (value != null) { mWAxes.CollectionChanged -= mWAxes_CollectionChanged; mWAxes = value; mWAxes.CollectionChanged += mWAxes_CollectionChanged; } } }

		internal IfcGrid() : base() { }
		internal IfcGrid(DatabaseIfc db, IfcGrid g, DuplicateOptions options) : base(db, g, options)
		{
			UAxes.AddRange(g.UAxes.ConvertAll(x => db.Factory.Duplicate(x) as IfcGridAxis));
			VAxes.AddRange(g.VAxes.ConvertAll(x => db.Factory.Duplicate(x) as IfcGridAxis));
			WAxes.AddRange(g.WAxes.ConvertAll(x => db.Factory.Duplicate(x) as IfcGridAxis));
			mPredefinedType = g.mPredefinedType;
		}
		public IfcGrid(IfcSpatialElement host, IfcAxis2Placement3D placement, List<IfcGridAxis> uAxes, List<IfcGridAxis> vAxes) 
			: base(new IfcLocalPlacement(host.ObjectPlacement, placement), getRepresentation(uAxes,vAxes, null))
		{
			host.addGrid(this);
			UAxes.AddRange(uAxes);
			VAxes.AddRange(vAxes);
			
		}
		public IfcGrid(IfcSpatialElement host, IfcAxis2Placement3D placement, List<IfcGridAxis> uAxes, List<IfcGridAxis> vAxes, List<IfcGridAxis> wAxes) 
			:this(host,placement, uAxes,vAxes) { WAxes.AddRange(wAxes); }

		protected override void initialize()
		{
			base.initialize();

			mUAxes.CollectionChanged += mUAxes_CollectionChanged;
			mVAxes.CollectionChanged += mVAxes_CollectionChanged;
			mWAxes.CollectionChanged += mWAxes_CollectionChanged;
		}
		private void mUAxes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcGridAxis a in e.NewItems)
				{
					a.mPartOfU = this;
					setShapeRep(a);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcGridAxis a in e.OldItems)
				{
					removeExistingFromShapeRep(a);
					a.mPartOfU = null;
				}
			}
		}
		private void mVAxes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcGridAxis a in e.NewItems)
				{
					a.mPartOfV = this;
					setShapeRep(a);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcGridAxis a in e.OldItems)
				{
					removeExistingFromShapeRep(a);
					a.mPartOfV = null;
				}
			}
		}
		private void mWAxes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcGridAxis a in e.NewItems)
				{
					a.mPartOfW = this;
					setShapeRep(a);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcGridAxis a in e.OldItems)
				{
					removeExistingFromShapeRep(a);
					a.mPartOfW = null;
				}
			}
		}
		private void setShapeRep(IfcGridAxis a) { setShapeRep(new List<IfcGridAxis>() { a }); }
		private void setShapeRep(List<IfcGridAxis> axis)
		{
			if (axis == null || axis.Count == 0)
				return;
			IfcProductDefinitionShape pds = Representation as IfcProductDefinitionShape;
			if (pds == null)
			{
				List<IfcGeometricSetSelect> set = new List<IfcGeometricSetSelect>();
				foreach(IfcGridAxis a in axis)
				{
					IfcCurve c = a.AxisCurve;
					if (c != null)
						set.Add(c);
				}
				if(set.Count > 0)
					Representation = new IfcProductDefinitionShape(new IfcShapeRepresentation(mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.FootPrint), new IfcGeometricCurveSet(set), ShapeRepresentationType.GeometricCurveSet));
			}
			else
			{
				foreach (IfcShapeModel sm in pds.Representations)
				{
					IfcShapeRepresentation sr = sm as IfcShapeRepresentation;
					if (sr != null)
					{
						foreach (IfcRepresentationItem gri in sr.Items)
						{
							IfcGeometricCurveSet curveSet = gri as IfcGeometricCurveSet;
							if (curveSet != null)
							{
								foreach(IfcGridAxis a in axis)
								{
									IfcCurve c = a.AxisCurve;
									if (c != null && !curveSet.Elements.Contains(c))
										curveSet.Elements.Add(c);	
								}
								return;
							}
						}
					}
				}
			}
			
		}

		private static IfcProductDefinitionShape getRepresentation(List<IfcGridAxis> uAxes, List<IfcGridAxis> vAxes, List<IfcGridAxis> wAxes)
		{
			List<IfcGeometricSetSelect> set = new List<IfcGeometricSetSelect>();
			if (uAxes != null)
			{
				foreach (IfcGridAxis axis in uAxes)
					set.Add(axis.AxisCurve);
			}
			if (vAxes != null)
			{
				foreach (IfcGridAxis axis in vAxes)
					set.Add(axis.AxisCurve);
			}
			if (wAxes != null)
			{
				foreach (IfcGridAxis axis in wAxes)
					set.Add(axis.AxisCurve);
			}
			return new IfcProductDefinitionShape(new IfcShapeRepresentation(new IfcGeometricCurveSet(set)));
		}
		private void removeExistingFromShapeRep(IfcGridAxis a) { removeExistingFromShapeRep(new List<IfcGridAxis>() { a }); }
		private void removeExistingFromShapeRep(List<IfcGridAxis> axis)
		{
			if (axis == null || axis.Count == 0)
				return;
			IfcProductDefinitionShape pds = Representation as IfcProductDefinitionShape;
			if (pds != null)
			{
				foreach (IfcShapeModel sm in pds.Representations)
				{
					IfcShapeRepresentation sr = sm as IfcShapeRepresentation;
					if (sr != null)
					{
						foreach (IfcRepresentationItem gri in sr.Items)
						{
							IfcGeometricCurveSet curveSet = gri as IfcGeometricCurveSet;
							if (curveSet != null)
								axis.ForEach(x=>curveSet.Elements.Remove(x.AxisCurve));
						}
					}
				}
			}
		}
	}
	[Serializable]
	public partial class IfcGridAxis : BaseClassIfc, NamedObjectIfc
	{
		private string mAxisTag = "$";// : OPTIONAL IfcLabel;
		internal bool mSameSense;// : IfcBoolean;

		//INVERSE
		internal IfcGrid mPartOfW = null;//	:	SET [0:1] OF IfcGrid FOR WAxes;
		internal IfcGrid mPartOfV = null;//	:	SET [0:1] OF IfcGrid FOR VAxes;
		internal IfcGrid mPartOfU = null;//	:	SET [0:1] OF IfcGrid FOR UAxes;
		internal List<IfcVirtualGridIntersection> mHasIntersections = new List<IfcVirtualGridIntersection>();//:	SET OF IfcVirtualGridIntersection FOR IntersectingAxes;

		public string Name { get { return AxisTag; } set { AxisTag = value; } }
		public string AxisTag { get { return mAxisTag == "$" ? "" : ParserIfc.Decode(mAxisTag); } set { mAxisTag = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcCurve AxisCurve { get; set; }
		public bool SameSense { get { return mSameSense; } set { mSameSense = value; } }

		internal IfcGridAxis() : base() { }
		internal IfcGridAxis(DatabaseIfc db, IfcGridAxis a) : base(db) { mAxisTag = a.mAxisTag; AxisCurve = db.Factory.Duplicate(a.AxisCurve) as IfcCurve; mSameSense = a.mSameSense; }
		public IfcGridAxis(string tag, IfcCurve axis, bool sameSense) : base(axis.mDatabase) { if (!string.IsNullOrEmpty(tag)) mAxisTag = tag.Replace("'", ""); AxisCurve = axis; mSameSense = sameSense; }
	}
	[Serializable]
	public partial class IfcGridPlacement : IfcObjectPlacement
	{
		internal IfcVirtualGridIntersection mPlacementLocation;// : IfcVirtualGridIntersection ;
		internal IfcGridPlacementDirectionSelect mPlacementRefDirection;// : OPTIONAL IfcVirtualGridIntersection; IFC4x3 IfcGridPlacementDirectionSelect

		public IfcVirtualGridIntersection PlacementLocation { get { return mPlacementLocation; } set { mPlacementLocation = value; } }
		public IfcGridPlacementDirectionSelect PlacementRefDirection { get { return mPlacementRefDirection; } set { mPlacementRefDirection = value; } }

		internal IfcGridPlacement() : base() { }
		internal IfcGridPlacement(DatabaseIfc db, IfcGridPlacement p) : base(db, p)
		{
			PlacementLocation = db.Factory.Duplicate(p.PlacementLocation) as IfcVirtualGridIntersection;
			if (p.mPlacementRefDirection != null)
				PlacementRefDirection = db.Factory.Duplicate(p.PlacementRefDirection) as IfcGridPlacementDirectionSelect;
		}
	}
	public interface IfcGridPlacementDirectionSelect : IBaseClassIfc { } // SELECT(IfcVirtualGridIntersection, IfcDirection);
	[Serializable]
	public partial class IfcGroup : IfcObject, IfcSpatialReferenceSelect //SUPERTYPE OF (ONEOF (IfcAsset ,IfcCondition ,IfcInventory ,IfcStructuralLoadGroup ,IfcStructuralResultGroup ,IfcSystem ,IfcZone))
	{
		//INVERSE
		internal SET<IfcRelAssignsToGroup> mIsGroupedBy = new SET<IfcRelAssignsToGroup>();// IFC4 SET : IfcRelAssignsToGroup FOR RelatingGroup;
		internal SET<IfcRelReferencedInSpatialStructure> mReferencedInStructures = new SET<IfcRelReferencedInSpatialStructure>();//  : 	SET OF IfcRelReferencedInSpatialStructure FOR RelatedElements;
		public SET<IfcRelAssignsToGroup> IsGroupedBy { get { return mIsGroupedBy; } }
		public SET<IfcRelReferencedInSpatialStructure> ReferencedInStructures { get { return mReferencedInStructures; } }

		internal IfcGroup() : base() { }
		internal IfcGroup(DatabaseIfc db, IfcGroup g, DuplicateOptions options) : base(db, g, options)
		{
			if(options.DuplicateDownstream)
			{
				foreach (IfcRelAssignsToGroup rags in g.mIsGroupedBy)
					db.Factory.Duplicate(rags, options);
			}
		}
		public IfcGroup(DatabaseIfc db, string name) : base(db) { Name = name; }
		public IfcGroup(IfcSpatialElement spatial, string name) : base(spatial.Database) 
		{
			Name = name;
			if (!(this is IfcZone))
			{
				if (spatial.mDatabase.Release <= ReleaseVersion.IFC4X3_RC1)
				{
					IfcSystem system = this as IfcSystem;
					if (system != null)
					{
						new IfcRelServicesBuildings(system, spatial) { Name = name };
					}
				}
				else
					spatial.ReferenceElement(this);
			}
		}
		public IfcGroup(List<IfcObjectDefinition> ods) : base(ods[0].mDatabase) { mIsGroupedBy.Add(new IfcRelAssignsToGroup(ods, this)); }

		public void AddRelated(IfcObjectDefinition related)
		{
			if (mIsGroupedBy.Count == 0)
			{
				new IfcRelAssignsToGroup(related, this);
			}
			else
				mIsGroupedBy.First().RelatedObjects.Add(related);
		}

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcRelAssignsToGroup rags in mIsGroupedBy)
			{
				foreach (IfcObjectDefinition d in rags.RelatedObjects)
					result.AddRange(d.Extract<T>());
			}
			return result;
		}
	}
}
