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
using System.Collections.Specialized;
using System.Reflection;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcGasTerminalType : IfcFlowTerminalType // DEPRECATED IFC4
	{
		private IfcGasTerminalTypeEnum mPredefinedType = IfcGasTerminalTypeEnum.NOTDEFINED;// : IfcGasTerminalBoxTypeEnum;
		public IfcGasTerminalTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcGasTerminalTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcGasTerminalType() : base() { }
		internal IfcGasTerminalType(DatabaseIfc db, IfcGasTerminalType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcGasTerminalType(DatabaseIfc db, string name, IfcGasTerminalTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
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
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3_ADD2)]
	public partial class IfcGeographicCRS : IfcCoordinateReferenceSystem 
	{
		internal string mPrimeMeridian = "";// : OPTIONAL IfcIdentifier 
		internal IfcNamedUnit mAngleUnit = null;// :	OPTIONAL IfcNamedUnit;
		internal IfcNamedUnit mHeightUnit = null;// :	OPTIONAL IfcNamedUnit;

		public string PrimeMeridian { get { return mPrimeMeridian; } set { mPrimeMeridian = value; } }
		public IfcNamedUnit AngleUnit { get { return mAngleUnit; } set { mAngleUnit = value; } }
		public IfcNamedUnit HeightUnit { get { return mHeightUnit; } set { mHeightUnit = value; } }

		internal IfcGeographicCRS() : base() { }
		internal IfcGeographicCRS(DatabaseIfc db, IfcGeographicCRS p) : base(db, p) 
		{ 
			mPrimeMeridian = p.mPrimeMeridian; 
			if (p.AngleUnit != null) 
				AngleUnit = db.Factory.Duplicate(p.AngleUnit);
			if (p.HeightUnit != null) 
				AngleUnit = db.Factory.Duplicate(p.HeightUnit); 
		}
		public IfcGeographicCRS(DatabaseIfc db, string name) : base(db) { Name = name; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4)]
	public partial class IfcGeographicElement : IfcElement  //IFC4
	{
		private IfcGeographicElementTypeEnum mPredefinedType = IfcGeographicElementTypeEnum.NOTDEFINED;// OPTIONAL IfcGeographicElementTypeEnum; 
		public IfcGeographicElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcGeographicElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcGeographicElement() : base() { }
		internal IfcGeographicElement(DatabaseIfc db) : base(db) { }
		internal IfcGeographicElement(DatabaseIfc db, IfcGeographicElement e, DuplicateOptions options) : base(db, e, options) { PredefinedType = e.PredefinedType; }
		public IfcGeographicElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) {  }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4)]
	public partial class IfcGeographicElementType : IfcElementType //IFC4
	{
		private IfcGeographicElementTypeEnum mPredefinedType = IfcGeographicElementTypeEnum.NOTDEFINED;// IfcGeographicElementTypeEnum; 
		public IfcGeographicElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcGeographicElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcGeographicElementType() : base() { }
		internal IfcGeographicElementType(DatabaseIfc db, IfcGeographicElementType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcGeographicElementType(DatabaseIfc db, string name, IfcGeographicElementTypeEnum type) : base(db) { Name = name; PredefinedType = type;  }
	}
	[Serializable]
	public partial class IfcGeometricCurveSet : IfcGeometricSet
	{
		internal IfcGeometricCurveSet() : base() { }
		internal IfcGeometricCurveSet(DatabaseIfc db, IfcGeometricCurveSet s, DuplicateOptions options) : base(db, s, options) { }
		public IfcGeometricCurveSet(IfcGeometricSetSelect element) : base(element) { if(element is IfcSurface) throw new Exception("XXX Error, IfcSurface cannot be added to IfcGeometricCurveSet " + StepId); }
		public IfcGeometricCurveSet(IEnumerable<IfcGeometricSetSelect> set) : base(set)
		{
			foreach(IfcGeometricSetSelect item in set)
			{
				if(item is IfcSurface)
					throw new Exception("XXX Error, IfcSurface cannot be added to IfcGeometricCurveSet " + StepId);
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
		[NonSerialized] internal SET<IfcShapeModel> mRepresentationsInContext = new SET<IfcShapeModel>();// :	SET OF IfcRepresentation FOR ContextOfItems;
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
		public SET<IfcShapeModel> RepresentationsInContext { get { return mRepresentationsInContext; } }
		public List<IfcGeometricRepresentationSubContext> HasSubContexts { get { return mHasSubContexts; } }
		public IfcCoordinateOperation HasCoordinateOperation { get { return mHasCoordinateOperation; } set { mHasCoordinateOperation = value; if(value != null && value.mSourceCRS != this) value.SourceCRS = this; } }


		internal IfcGeometricRepresentationContext() : base() { }
		protected IfcGeometricRepresentationContext(DatabaseIfc db) : base(db) { }
		internal IfcGeometricRepresentationContext(DatabaseIfc db, IfcGeometricRepresentationContext c, DuplicateOptions options) : base(db, c, options)
		{
			mCoordinateSpaceDimension = c.mCoordinateSpaceDimension;
			mPrecision = c.mPrecision;
			if(c.mWorldCoordinateSystem != null)
				WorldCoordinateSystem = db.Factory.Duplicate(c.mWorldCoordinateSystem, options);
			if (c.mTrueNorth != null)
				TrueNorth = db.Factory.Duplicate(c.TrueNorth, options);

			if (options.DuplicateDownstream)
			{
				foreach (IfcGeometricRepresentationSubContext sc in c.mHasSubContexts)
					db.Factory.Duplicate(sc, options);
			}

			if (c.mHasCoordinateOperation != null)
				db.Factory.Duplicate(c.mHasCoordinateOperation, options);

			IfcGeometricRepresentationSubContext subContext = this as IfcGeometricRepresentationSubContext;
			if (subContext == null)
			{
				IfcGeometricRepresentationContext.GeometricContextIdentifier id = IfcGeometricRepresentationContext.GeometricContextIdentifier.Model;
				if (Enum.TryParse<IfcGeometricRepresentationContext.GeometricContextIdentifier>(ContextType, out id))
				{
					db.Factory.mContexts[id] = this;
				}
				if (db.Context != null)
					db.Context.RepresentationContexts.Add(this);
			}
		}
		internal IfcGeometricRepresentationContext(DatabaseIfc db, int spaceDimension, double precision) : base(db)
		{
			if (db.Context != null)
				db.Context.RepresentationContexts.Add(this);
			mCoordinateSpaceDimension = spaceDimension;
			mPrecision = Math.Max(1e-8, precision);
			if(spaceDimension == 2)
				WorldCoordinateSystem = new IfcAxis2Placement2D(new IfcCartesianPoint(db, 0, 0));
			else
				WorldCoordinateSystem = new IfcAxis2Placement3D(new IfcCartesianPoint(db, 0, 0, 0));
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
		internal T representationOf<T>() where T : IfcProduct
		{
			IfcShapeModel shapeModel = Represents.FirstOrDefault();
			if (shapeModel != null)
			{
				var ofRepresentation = shapeModel.OfProductRepresentation.SelectMany(x => x.ShapeOfProduct);
				return ofRepresentation.OfType<T>().FirstOrDefault();
			}
			return null;
		}
	}
	[Serializable]
	public partial class IfcGeometricRepresentationSubContext : IfcGeometricRepresentationContext
	{
		public enum SubContextIdentifier { Annotation, Axis, Body, Body_Fallback, Box, Clearance, CoG, FootPrint, Lighting, PlanSymbol3d, PlanSymbol2d, Reference, Profile, Row, Outline, Surface };

		internal IfcGeometricRepresentationContext mParentContext;// : IfcGeometricRepresentationContext;
		internal double mTargetScale = double.NaN;// : OPTIONAL IfcPositiveRatioMeasure;
		private IfcGeometricProjectionEnum mTargetView;// : IfcGeometricProjectionEnum;
		internal string mUserDefinedTargetView = "";// : OPTIONAL IfcLabel;

		public IfcGeometricRepresentationContext ParentContext { get { return mParentContext; } set { mParentContext = value; if(value != null) value.mHasSubContexts.Add(this); } }
		public double TargetScale { get { return mTargetScale; } set { mTargetScale = value; } }
		public IfcGeometricProjectionEnum TargetView { get { return mTargetView; } set { mTargetView = value; } }
		public string UserDefinedTargetView { get { return mUserDefinedTargetView; } set { mUserDefinedTargetView = value; } }

		internal IfcGeometricRepresentationSubContext() : base() { }
		internal IfcGeometricRepresentationSubContext(DatabaseIfc db, IfcGeometricRepresentationSubContext s, DuplicateOptions options) : base(db, s, options)
		{
			IfcGeometricRepresentationContext parent = s.ParentContext;
			if (parent != null)
			{
				IfcGeometricRepresentationContext.GeometricContextIdentifier contextId = IfcGeometricRepresentationContext.GeometricContextIdentifier.Model;
				if (Enum.TryParse<IfcGeometricRepresentationSubContext.GeometricContextIdentifier>(s.ContextType, out contextId))
				{
					IfcGeometricRepresentationContext existing = null;
					if (db.Factory.mContexts.TryGetValue(contextId, out existing))
						ParentContext = existing;
				}
			}
			if(ParentContext == null)
				ParentContext = db.Factory.Duplicate(s.ParentContext, options) as IfcGeometricRepresentationContext;

			mTargetScale = s.mTargetScale;
			mTargetView = s.mTargetView;
			mUserDefinedTargetView = s.mUserDefinedTargetView;
			IfcGeometricRepresentationSubContext.SubContextIdentifier id = IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis;
			if (Enum.TryParse<IfcGeometricRepresentationSubContext.SubContextIdentifier>(ContextIdentifier, out id))
				db.Factory.mSubContexts[id] = this;
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
		public SET<IfcGeometricSetSelect> Elements { get { return mElements; } }

		internal IfcGeometricSet() : base() { }
		internal IfcGeometricSet(DatabaseIfc db, IfcGeometricSet s, DuplicateOptions options) : base(db, s, options) 
		{
			mElements.AddRange(s.mElements.ConvertAll(x=>db.Factory.Duplicate(x, options)));
		}
		public IfcGeometricSet(IfcGeometricSetSelect element) : base(element.Database) { mElements.Add(element); }
		public IfcGeometricSet(IEnumerable<IfcGeometricSetSelect> set) : base(set.First().Database) { mElements.AddRange(set); }
	}
	public partial interface IfcGeometricSetSelect : IBaseClassIfc { } //SELECT ( IfcPoint, IfcCurve,  IfcSurface);
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcGeomodel : IfcGeotechnicalAssembly
	{
		public IfcGeomodel() : base() { }
		public IfcGeomodel(DatabaseIfc db) : base(db) { }
		public IfcGeomodel(DatabaseIfc db, IfcGeomodel geomodel, DuplicateOptions options) : base(db, geomodel, options) {  }
		public IfcGeomodel(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public abstract partial class IfcGeoScienceElement : IfcElement
	{
		protected IfcGeoScienceElement() : base() { }
		protected IfcGeoScienceElement(DatabaseIfc db) : base(db) { }
		protected IfcGeoScienceElement(DatabaseIfc db, IfcGeoScienceElement geotechnicalElement, DuplicateOptions options) : base(db, geotechnicalElement, options) { }
		protected IfcGeoScienceElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcGeoScienceFeature : IfcGeoScienceElement
	{
		private IfcGeoScienceFeatureTypeEnum mPredefinedType = IfcGeoScienceFeatureTypeEnum.NOTDEFINED; //: OPTIONAL IfcBoreholeTypeEnum;
		public IfcGeoScienceFeatureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcGeoScienceFeatureTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X4_DRAFT : mDatabase.Release); } }

		public IfcGeoScienceFeature() : base() { }
		public IfcGeoScienceFeature(DatabaseIfc db) : base(db) { }
		public IfcGeoScienceFeature(DatabaseIfc db, IfcGeoScienceFeature feature, DuplicateOptions options) : base(db, feature, options) { PredefinedType = feature.PredefinedType; }
		public IfcGeoScienceFeature(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	public partial class IfcGeoScienceModel : IfcGeoScienceElement
	{
		private IfcGeoScienceModelTypeEnum mPredefinedType = IfcGeoScienceModelTypeEnum.NOTDEFINED; //: OPTIONAL IfcBoreholeTypeEnum;
		public IfcGeoScienceModelTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcGeoScienceModelTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X4_DRAFT : mDatabase.Release); } }

		public IfcGeoScienceModel() : base() { }
		public IfcGeoScienceModel(DatabaseIfc db) : base(db) { }
		public IfcGeoScienceModel(DatabaseIfc db, IfcGeoScienceModel model, DuplicateOptions options) : base(db, model, options) { PredefinedType = model.PredefinedType; }
		public IfcGeoScienceModel(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcGeoScienceObservation : IfcObservation 
	{
		private IfcGeoScienceObservationTypeEnum mPredefinedType = IfcGeoScienceObservationTypeEnum.NOTDEFINED;// OPTIONAL : IfcOutletTypeEnum;
		public IfcGeoScienceObservationTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcGeoScienceObservationTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcGeoScienceObservation() : base() { }
		internal IfcGeoScienceObservation(DatabaseIfc db, IfcGeoScienceObservation o, DuplicateOptions options) : base(db, o, options) { PredefinedType = o.PredefinedType; }
		public IfcGeoScienceObservation(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcGeotechTypicalSection : IfcLinearZone
	{
		private IfcGeotechTypicalSectionTypeEnum mPredefinedType = IfcGeotechTypicalSectionTypeEnum.NOTDEFINED;// OPTIONAL : IfcOutletTypeEnum;
		public IfcGeotechTypicalSectionTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcGeotechTypicalSectionTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X4_DRAFT : mDatabase.Release); } }

		internal IfcGeotechTypicalSection() : base() { }
		internal IfcGeotechTypicalSection(DatabaseIfc db, IfcGeotechTypicalSection o, DuplicateOptions options) : base(db, o, options) { PredefinedType = o.PredefinedType; }
		public IfcGeotechTypicalSection(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcGeoslice : IfcGeotechnicalAssembly
	{
		public IfcGeoslice() : base() { }
		public IfcGeoslice(DatabaseIfc db) : base(db) { }
		public IfcGeoslice(DatabaseIfc db, IfcGeoslice geoslice, DuplicateOptions options) : base(db, geoslice, options) { }
		public IfcGeoslice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public abstract partial class IfcGeotechnicalAssembly : IfcGeotechnicalElement
	{
		protected IfcGeotechnicalAssembly() : base() { }
		protected IfcGeotechnicalAssembly(DatabaseIfc db) : base(db) { }
		protected IfcGeotechnicalAssembly(DatabaseIfc db, IfcGeotechnicalAssembly geotechnicalAssembly, DuplicateOptions options) : base(db, geotechnicalAssembly, options) { }
		protected IfcGeotechnicalAssembly(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public abstract partial class IfcGeotechnicalElement : IfcElement
	{
		protected IfcGeotechnicalElement() : base() { }
		protected IfcGeotechnicalElement(DatabaseIfc db) : base(db) { }
		protected IfcGeotechnicalElement(DatabaseIfc db, IfcGeotechnicalElement geotechnicalElement, DuplicateOptions options) : base(db, geotechnicalElement, options) { }
		protected IfcGeotechnicalElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcGeotechnicalStratum : IfcGeotechnicalElement
	{
		public override string StepClassName 
		{ 
			get 
			{
				if(mDatabase == null)
				return base.StepClassName; 
				ReleaseVersion release = mDatabase.Release;
				if (release < ReleaseVersion.IFC4X3)
				{
					if (PredefinedType == IfcGeotechnicalStratumTypeEnum.SOLID)
						return "IfcSolidStratum";
					if (PredefinedType == IfcGeotechnicalStratumTypeEnum.WATER)
						return "IfcWaterStratum";
					if (PredefinedType == IfcGeotechnicalStratumTypeEnum.VOID)
						return "IfcVoidStratum";
				}
				return base.StepClassName; 
			}
		}

		private IfcGeotechnicalStratumTypeEnum mPredefinedType = IfcGeotechnicalStratumTypeEnum.NOTDEFINED;// IfcGeotechnicalStratumTypeEnum; 
		public IfcGeotechnicalStratumTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcGeotechnicalStratumTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcGeotechnicalStratum() : base() { }
		public IfcGeotechnicalStratum(DatabaseIfc db) : base(db) { }
		public IfcGeotechnicalStratum(DatabaseIfc db, IfcGeotechnicalStratum geotechnicalStratum, DuplicateOptions options) : base(db, geotechnicalStratum, options) { }
		public IfcGeotechnicalStratum(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation)
			: base(host.Database)
		{
			IfcSpatialElement spatialElement = host as IfcSpatialElement;
			if (spatialElement != null)
				spatialElement.AddElement(this);
			else
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
			IfcLinearElement linearElement = c.representationOf<IfcLinearElement>();
			if (linearElement != null)
				db.Factory.Duplicate(linearElement, new DuplicateOptions(options) { DuplicateHost = true });
			else
			{
				IfcPositioningElement positioningElement = c.representationOf<IfcPositioningElement>();
				if(positioningElement != null)
					db.Factory.Duplicate(positioningElement, new DuplicateOptions(options) { DuplicateHost = true });
			}
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
		private IfcGridTypeEnum mPredefinedType = IfcGridTypeEnum.NOTDEFINED;// :OPTIONAL IfcGridTypeEnum; //IFC4 CHANGE  New attribute.
		//INVERSE
		public IfcGridTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcGridTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public LIST<IfcGridAxis> UAxes { get { return mUAxes; } }
		public LIST<IfcGridAxis> VAxes { get { return mVAxes; } }
		public LIST<IfcGridAxis> WAxes { get { return mWAxes; } }

		internal IfcGrid() : base() { }
		internal IfcGrid(DatabaseIfc db, IfcGrid g, DuplicateOptions options) : base(db, g, options)
		{
			UAxes.AddRange(g.UAxes.Select(x => db.Factory.Duplicate(x, options)));
			VAxes.AddRange(g.VAxes.Select(x => db.Factory.Duplicate(x, options)));
			WAxes.AddRange(g.WAxes.Select(x => db.Factory.Duplicate(x, options)));
			PredefinedType = g.PredefinedType;
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
		private string mAxisTag = "";// : OPTIONAL IfcLabel;
		internal bool mSameSense;// : IfcBoolean;

		//INVERSE
		internal IfcGrid mPartOfW = null;//	:	SET [0:1] OF IfcGrid FOR WAxes;
		internal IfcGrid mPartOfV = null;//	:	SET [0:1] OF IfcGrid FOR VAxes;
		internal IfcGrid mPartOfU = null;//	:	SET [0:1] OF IfcGrid FOR UAxes;
		internal List<IfcVirtualGridIntersection> mHasIntersections = new List<IfcVirtualGridIntersection>();//:	SET OF IfcVirtualGridIntersection FOR IntersectingAxes;

		public string Name { get { return AxisTag; } set { AxisTag = value; } }
		public string AxisTag { get { return mAxisTag; } set { mAxisTag = value; } }
		public IfcCurve AxisCurve { get; set; }
		public bool SameSense { get { return mSameSense; } set { mSameSense = value; } }

		internal IfcGridAxis() : base() { }
		internal IfcGridAxis(DatabaseIfc db, IfcGridAxis a, DuplicateOptions options) : base(db) 
		{
			mAxisTag = a.mAxisTag;
			AxisCurve = db.Factory.Duplicate(a.AxisCurve, options);
			mSameSense = a.mSameSense; 
		}
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
		internal IfcGridPlacement(DatabaseIfc db, IfcGridPlacement p, DuplicateOptions options) : base(db, p, options)
		{
			PlacementLocation = db.Factory.Duplicate(p.PlacementLocation, options);
			if (p.mPlacementRefDirection != null)
				PlacementRefDirection = db.Factory.Duplicate(p.PlacementRefDirection, options);
		}

		internal override bool isXYPlaneWorker(double tol)
		{
			return false;
		}
	}
	public interface IfcGridPlacementDirectionSelect : IBaseClassIfc { } // SELECT(IfcVirtualGridIntersection, IfcDirection);
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcGroundReinforcementElement : IfcBuiltElement
	{
		private IfcGroundReinforcementElementTypeEnum mPredefinedType = IfcGroundReinforcementElementTypeEnum.NOTDEFINED;//: IfcGroundReinforcementElementTypeEnum; 
		public IfcGroundReinforcementElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcGroundReinforcementElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X4_DRAFT : mDatabase.Release); } }

		internal IfcGroundReinforcementElement() : base() { }
		internal IfcGroundReinforcementElement(DatabaseIfc db, IfcGroundReinforcementElement e, DuplicateOptions options) : base(db, e, options) { PredefinedType = e.PredefinedType; }
		public IfcGroundReinforcementElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcGroundReinforcementElementType : IfcBuiltElementType 
	{
		private IfcGroundReinforcementElementTypeEnum mPredefinedType = IfcGroundReinforcementElementTypeEnum.NOTDEFINED;// IfcGroundReinforcementElementTypeEnum; 
		public IfcGroundReinforcementElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcGroundReinforcementElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X4_DRAFT : mDatabase.Release); } }

		internal IfcGroundReinforcementElementType() : base() { }
		internal IfcGroundReinforcementElementType(DatabaseIfc db, IfcGroundReinforcementElementType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcGroundReinforcementElementType(DatabaseIfc db, string name, IfcGroundReinforcementElementTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
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
		public IfcGroup(IEnumerable<IfcObjectDefinition> ods) : base(ods.First().mDatabase) { mIsGroupedBy.Add(new IfcRelAssignsToGroup(ods, this)); }

		public void AddRelated(IfcObjectDefinition related)
		{
			if (mIsGroupedBy.Count == 0)
			{
				new IfcRelAssignsToGroup(related, this);
			}
			else
				mIsGroupedBy.First().RelatedObjects.Add(related);
		}
		public void AddRelated(IEnumerable<IfcObjectDefinition> related)
		{
			if (mIsGroupedBy.Count == 0)
			{
				new IfcRelAssignsToGroup(related, this);
			}
			else
				mIsGroupedBy.First().RelatedObjects.AddRange(related);
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
