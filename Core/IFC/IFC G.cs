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
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcGasTerminalType : IfcFlowTerminalType // DEPRECEATED IFC4
	{
		internal IfcGasTerminalTypeEnum mPredefinedType = IfcGasTerminalTypeEnum.NOTDEFINED;// : IfcGasTerminalBoxTypeEnum;
		public IfcGasTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcGasTerminalType() : base() { }
		internal IfcGasTerminalType(DatabaseIfc db, IfcGasTerminalType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		internal IfcGasTerminalType(DatabaseIfc m, string name, IfcGasTerminalTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcGeneralMaterialProperties : IfcMaterialPropertiesSuperseded // DEPRECEATED IFC4
	{
		internal double mMolecularWeight = double.NaN; //: OPTIONAL IfcMolecularWeightMeasure;
		internal double mPorosity = double.NaN; //: OPTIONAL IfcNormalisedRatioMeasure;
		internal double mMassDensity = double.NaN;//OPTIONAL IfcMassDensityMeasure

		public double MolecularWeight { get { return mMolecularWeight; } set { mMolecularWeight = value; } }
		public double Porosity { get { return mPorosity; } set { mPorosity = value; } }
		public double MassDensity { get { return mMassDensity; } set { mMassDensity = value; } } 

		internal IfcGeneralMaterialProperties() : base() { }
		internal IfcGeneralMaterialProperties(DatabaseIfc db, IfcGeneralMaterialProperties p) : base(db,p) { mMolecularWeight = p.mMolecularWeight; mPorosity = p.mPorosity; mMassDensity = p.mMassDensity; }
		public IfcGeneralMaterialProperties(IfcMaterial material) : base(material) { }
		internal IfcGeneralMaterialProperties(IfcMaterial mat, double molecularWeight, double porosity, double massDensity) : base(mat)
		{
			mMolecularWeight = molecularWeight;
			mPorosity = porosity;
			mMassDensity = massDensity;
		}
	}
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
		internal IfcGeneralProfileProperties(DatabaseIfc db, IfcGeneralProfileProperties p) : base(db, p) { mPhysicalWeight = p.mPhysicalWeight; mPerimeter = p.mPerimeter; mMinimumPlateThickness = p.mMinimumPlateThickness; mMaximumPlateThickness = p.mMaximumPlateThickness; mCrossSectionArea = p.mCrossSectionArea; }
		public IfcGeneralProfileProperties(string name, IfcProfileDef p) : base(name, p) { }
		internal IfcGeneralProfileProperties(string name, List<IfcProperty> props, IfcProfileDef p) : base(name, props, p) { }
	}
	public partial class IfcGeographicElement : IfcElement  //IFC4
	{
		internal IfcGeographicElementTypeEnum mPredefinedType = IfcGeographicElementTypeEnum.NOTDEFINED;// OPTIONAL IfcGeographicElementTypeEnum; 
		public IfcGeographicElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcGeographicElement() : base() { }
		internal IfcGeographicElement(DatabaseIfc db, IfcGeographicElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { mPredefinedType = e.mPredefinedType; }
		public IfcGeographicElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { if (mDatabase.mRelease == ReleaseVersion.IFC2x3) throw new Exception(KeyWord + " only supported in IFC4!"); }
	}
	public partial class IfcGeographicElementType : IfcElementType //IFC4
	{
		internal IfcGeographicElementTypeEnum mPredefinedType = IfcGeographicElementTypeEnum.NOTDEFINED;// IfcGeographicElementTypeEnum; 
		public IfcGeographicElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcGeographicElementType() : base() { }
		internal IfcGeographicElementType(DatabaseIfc db, IfcGeographicElementType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		internal IfcGeographicElementType(DatabaseIfc m, string name, IfcGeographicElementTypeEnum type) : base(m) { Name = name; mPredefinedType = type; if (m.mRelease == ReleaseVersion.IFC2x3) throw new Exception(KeyWord + " only supported in IFC4!"); }
	}
	public partial class IfcGeometricCurveSet : IfcGeometricSet
	{
		internal IfcGeometricCurveSet() : base() { }
		internal IfcGeometricCurveSet(DatabaseIfc db, IfcGeometricCurveSet s) : base(db,s) { }
		public IfcGeometricCurveSet(IfcGeometricSetSelect element) : base(element) { if(element is IfcSurface) throw new Exception("XXX Error, IfcSurface cannot be added to IfcGeometricCurveSet " + mIndex); }
		public IfcGeometricCurveSet(List<IfcGeometricSetSelect> set) : base(set)
		{
			foreach(IfcGeometricSetSelect item in set)
			{
				if(item is IfcSurface)
					throw new Exception("XXX Error, IfcSurface cannot be added to IfcGeometricCurveSet " + mIndex);
			}
		}
	}
	public partial class IfcGeometricRepresentationContext : IfcRepresentationContext, IfcCoordinateReferenceSystemSelect // SUPERTYPE OF(IfcGeometricRepresentationSubContext)
	{
		public enum GeometricContextIdentifier { Annotation, Model };

		internal int mCoordinateSpaceDimension;// : IfcDimensionCount;
		internal double mPrecision = 1e-8;// : OPTIONAL REAL;
		internal int mWorldCoordinateSystem;// : IfcAxis2Placement;
		internal int mTrueNorth;// : OPTIONAL IfcDirection; 
		//INVERSE
		internal List<IfcGeometricRepresentationSubContext> mHasSubContexts = new List<IfcGeometricRepresentationSubContext>();//	 :	SET OF IfcGeometricRepresentationSubContext FOR ParentContext;
		private IfcCoordinateOperation mHasCoordinateOperation = null; //IFC4

		public int CoordinateSpaceDimension { get { return mCoordinateSpaceDimension; } set { mCoordinateSpaceDimension = value; } }
		public double Precision { get { return mPrecision; } set { mPrecision = value; } }
		public IfcAxis2Placement WorldCoordinateSystem { get { return mDatabase[mWorldCoordinateSystem] as IfcAxis2Placement; } set { mWorldCoordinateSystem = (value == null ? 0 : value.Index); } }
		public IfcDirection TrueNorth 
		{ 
			get { return mDatabase[mTrueNorth] as IfcDirection; }  
			set 
			{
				if (value == null)
					mTrueNorth = 0;
				else
				{
					if (Math.Abs(value.DirectionRatioZ) > mDatabase.Tolerance)
						throw new Exception("True North direction must be 2 dimensional direction");
					mTrueNorth = value.mIndex; 
				}
			} 
		}
		public List<IfcGeometricRepresentationSubContext> HasSubContexts { get { return mHasSubContexts; } }
		public IfcCoordinateOperation HasCoordinateOperation { get { return mHasCoordinateOperation; } set { mHasCoordinateOperation = value; if(value.mSourceCRS != mIndex) value.SourceCRS = this; } }
		
		internal IfcGeometricRepresentationContext() : base() { }
		protected IfcGeometricRepresentationContext(DatabaseIfc db) : base(db) { }
		internal IfcGeometricRepresentationContext(DatabaseIfc db, IfcGeometricRepresentationContext c) : base(db, c)
		{
			mCoordinateSpaceDimension = c.mCoordinateSpaceDimension;
			mPrecision = c.mPrecision;
			if(c.mWorldCoordinateSystem > 0)
				WorldCoordinateSystem = db.Factory.Duplicate(c.mDatabase[ c.mWorldCoordinateSystem]) as IfcAxis2Placement;
			if (c.mTrueNorth > 0)
				TrueNorth = db.Factory.Duplicate(c.TrueNorth) as IfcDirection;

			foreach (IfcGeometricRepresentationSubContext sc in mHasSubContexts)
				db.Factory.Duplicate(sc);
		}
		internal IfcGeometricRepresentationContext(DatabaseIfc db, int SpaceDimension, double precision) : base(db)
		{
			if (db.Context != null)
				db.Context.addRepresentationContext(this);
			mCoordinateSpaceDimension = SpaceDimension;
			mPrecision = Math.Max(1e-8, precision);
			WorldCoordinateSystem = new IfcAxis2Placement3D(new IfcCartesianPoint(db,0,0,0));
			TrueNorth = new IfcDirection(mDatabase, 0, 1);
		}
		public IfcGeometricRepresentationContext(int coordinateSpaceDimension, IfcAxis2Placement worldCoordinateSystem) : base(worldCoordinateSystem.Database) { mCoordinateSpaceDimension = coordinateSpaceDimension; WorldCoordinateSystem = worldCoordinateSystem; }
	}
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
		protected IfcGeometricRepresentationItem(DatabaseIfc db, IfcGeometricRepresentationItem i) : base(db,i) { }
	}
	public partial class IfcGeometricRepresentationSubContext : IfcGeometricRepresentationContext
	{
		public enum SubContextIdentifier { Axis, Body, BoundingBox, FootPrint, PlanSymbol3d, PlanSymbol2d, Reference, Profile, Row };// Surface };

		internal int mContainerContext;// : IfcGeometricRepresentationContext;
		internal double mTargetScale = double.NaN;// : OPTIONAL IfcPositiveRatioMeasure;
		private IfcGeometricProjectionEnum mTargetView;// : IfcGeometricProjectionEnum;
		internal string mUserDefinedTargetView = "$";// : OPTIONAL IfcLabel;

		public IfcGeometricRepresentationContext ContainerContext { get { return mDatabase[mContainerContext] as IfcGeometricRepresentationContext; } set { mContainerContext = value.mIndex; value.mHasSubContexts.Add(this); } }
		public double TargetScale { get { return mTargetScale; } set { mTargetScale = value; } }
		public IfcGeometricProjectionEnum TargetView { get { return mTargetView; } set { mTargetView = value; } }
		public string UserDefinedTargetView { get { return (mUserDefinedTargetView == "$" ? "" : ParserIfc.Decode(mUserDefinedTargetView)); } set { mUserDefinedTargetView = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcGeometricRepresentationSubContext() : base() { }
		internal IfcGeometricRepresentationSubContext(DatabaseIfc db, IfcGeometricRepresentationSubContext s) : base(db, s)
		{
			ContainerContext = db.Factory.Duplicate(s.ContainerContext) as IfcGeometricRepresentationContext;

			mTargetScale = s.mTargetScale;
			mTargetView = s.mTargetView;
			mUserDefinedTargetView = s.mUserDefinedTargetView;
		}
		public IfcGeometricRepresentationSubContext(IfcGeometricRepresentationContext container, IfcGeometricProjectionEnum view)
			: base(container.mDatabase)
		{
			ContainerContext = container;
			mContextType = container.mContextType;
			mTargetView = view;
		}
	}
	public partial class IfcGeometricSet : IfcGeometricRepresentationItem //SUPERTYPE OF(IfcGeometricCurveSet)
	{
		private List<int> mElements = new List<int>(); //SET [1:?] OF IfcGeometricSetSelect; 
		public ReadOnlyCollection<IfcGeometricSetSelect> Elements { get { return new ReadOnlyCollection<IfcGeometricSetSelect>( mElements.ConvertAll(x => mDatabase[x] as IfcGeometricSetSelect)); } }

		internal IfcGeometricSet() : base() { }
		internal IfcGeometricSet(DatabaseIfc db, IfcGeometricSet s) : base(db,s) { s.mElements.ToList().ForEach(x=>addElement( db.Factory.Duplicate(s.mDatabase[x]) as IfcGeometricSetSelect)); }
		public IfcGeometricSet(IfcGeometricSetSelect element) : base(element.Database) { mElements.Add(element.Index); }
		public IfcGeometricSet(List<IfcGeometricSetSelect> set) : base(set[0].Database) { mElements = set.ConvertAll(x => x.Index); }

		internal void addElement(IfcGeometricSetSelect element)
		{
			if (!mElements.Contains(element.Index))
				mElements.Add(element.Index);
		}
		internal void removeElement(IfcGeometricSetSelect element)
		{
			if(element != null)
				mElements.Remove(element.Index);
		}
	}
	public partial interface IfcGeometricSetSelect : IBaseClassIfc { } //SELECT ( IfcPoint, IfcCurve,  IfcSurface);
	public partial class IfcGrid : IfcProduct
	{
		private List<int> mUAxes = new List<int>();// : LIST [1:?] OF UNIQUE IfcGridAxis;
		private List<int> mVAxes = new List<int>();// : LIST [1:?] OF UNIQUE IfcGridAxis;
		private List<int> mWAxes = new List<int>();// : OPTIONAL LIST [1:?] OF UNIQUE IfcGridAxis;
		internal IfcGridTypeEnum mPredefinedType = IfcGridTypeEnum.NOTDEFINED;// :OPTIONAL IfcGridTypeEnum; //IFC4 CHANGE  New attribute.
																			  //INVERSE
		internal IfcRelContainedInSpatialStructure mContainedInStructure = null;

		internal IfcGridTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public ReadOnlyCollection<IfcGridAxis> UAxes { get { return new ReadOnlyCollection<IfcGridAxis>( mUAxes.ConvertAll(x => mDatabase[x] as IfcGridAxis)); } }
		public ReadOnlyCollection<IfcGridAxis> VAxes { get { return new ReadOnlyCollection<IfcGridAxis>( mVAxes.ConvertAll(x => mDatabase[x] as IfcGridAxis)); } }
		public ReadOnlyCollection<IfcGridAxis> WAxes { get { return new ReadOnlyCollection<IfcGridAxis>( mWAxes.ConvertAll(x => mDatabase[x] as IfcGridAxis)); } }

		internal IfcGrid() : base() { }
		internal IfcGrid(DatabaseIfc db, IfcGrid g, IfcOwnerHistory ownerHistory, bool downStream) : base(db, g, ownerHistory, downStream)
		{
			g.UAxes.ToList().ForEach(x => AddUAxis( db.Factory.Duplicate(x) as IfcGridAxis));
			g.VAxes.ToList().ForEach(x => AddVAxis( db.Factory.Duplicate(x) as IfcGridAxis));
			g.WAxes.ToList().ForEach(x => AddWAxis( db.Factory.Duplicate(x) as IfcGridAxis));
			mPredefinedType = g.mPredefinedType;
		}
		public IfcGrid(IfcSpatialElement host, IfcAxis2Placement3D placement, List<IfcGridAxis> uAxes, List<IfcGridAxis> vAxes) 
			: base(new IfcLocalPlacement(host.Placement, placement), getRepresentation(uAxes,vAxes, null))
		{
			host.addGrid(this);
			if(uAxes != null)
				mUAxes = uAxes.ConvertAll(x=>x.mIndex);
			if (vAxes != null)
				mVAxes = vAxes.ConvertAll(x => x.mIndex);
			
		}
		public IfcGrid(IfcSpatialElement host, IfcAxis2Placement3D placement, List<IfcGridAxis> uAxes, List<IfcGridAxis> vAxes, List<IfcGridAxis> wAxes) 
			:this(host,placement, uAxes,vAxes) { wAxes.ForEach(x=>AddWAxis(x)); }

		internal void AddUAxis(IfcGridAxis a) { mUAxes.Add(a.mIndex); a.mPartOfU = this; setShapeRep(a); }
		internal void AddVAxis(IfcGridAxis a) { mVAxes.Add(a.mIndex); a.mPartOfV = this; setShapeRep(a); }
		internal void AddWAxis(IfcGridAxis a) { mWAxes.Add(a.mIndex); a.mPartOfW = this; setShapeRep(a); }
		internal void RemoveUAxis(IfcGridAxis a) { mUAxes.Remove(a.mIndex); a.mPartOfU = null; removeExistingFromShapeRep(a);  }
		internal void RemoveVAxis(IfcGridAxis a) { mVAxes.Remove(a.mIndex); a.mPartOfV = null; removeExistingFromShapeRep(a); }
		internal void RemoveWAxis(IfcGridAxis a) { mWAxes.Remove(a.mIndex); a.mPartOfW = null; removeExistingFromShapeRep(a); }

		private void setShapeRep(IfcGridAxis a) { setShapeRep(new List<IfcGridAxis>() { a }); }
		private void setShapeRep(List<IfcGridAxis> axis)
		{
			if (axis == null || axis.Count == 0)
				return;
			IfcProductDefinitionShape pds = Representation as IfcProductDefinitionShape;
			if (pds == null)
				Representation = new IfcProductDefinitionShape(new IfcShapeRepresentation(new IfcGeometricCurveSet(axis.ConvertAll(x => (IfcGeometricSetSelect)x.AxisCurve))));
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
								axis.ForEach(x => curveSet.addElement( (IfcGeometricSetSelect)x.AxisCurve));
								return;
							}
						}
					}
				}
			}
			
		}

		private static IfcProductRepresentation getRepresentation(List<IfcGridAxis> uAxes, List<IfcGridAxis> vAxes, List<IfcGridAxis> wAxes)
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
								axis.ForEach(x=>curveSet.removeElement((IfcGeometricSetSelect)x.AxisCurve));
						}
					}
				}
			}
		}

		//removeExistingFromShapeRep(UAxes);
		//mUAxes = value == null ? new List<int>() : value.ConvertAll(x => x.mIndex);
		//		setShapeRep(value);

		internal override void detachFromHost()
		{
			base.detachFromHost();
			if (mContainedInStructure != null)
			{
				mContainedInStructure.mRelatedElements.Remove(mIndex);
				mContainedInStructure = null;
			}
		}
	}
	public partial class IfcGridAxis : BaseClassIfc
	{
		private string mAxisTag = "$";// : OPTIONAL IfcLabel;
		private int mAxisCurve;// : IfcCurve;
		internal bool mSameSense;// : IfcBoolean;

		//INVERSE
		internal IfcGrid mPartOfW = null;//	:	SET [0:1] OF IfcGrid FOR WAxes;
		internal IfcGrid mPartOfV = null;//	:	SET [0:1] OF IfcGrid FOR VAxes;
		internal IfcGrid mPartOfU = null;//	:	SET [0:1] OF IfcGrid FOR UAxes;
		internal List<IfcVirtualGridIntersection> mHasIntersections = new List<IfcVirtualGridIntersection>();//:	SET OF IfcVirtualGridIntersection FOR IntersectingAxes;

		public override string Name { get { return AxisTag; } set { AxisTag = value; } }
		public string AxisTag { get { return mAxisTag == "$" ? "" : ParserIfc.Decode(mAxisTag); } set { mAxisTag = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcCurve AxisCurve { get { return mDatabase[mAxisCurve] as IfcCurve; } set { mAxisCurve = value.mIndex; } }

		internal IfcGridAxis() : base() { }
		internal IfcGridAxis(DatabaseIfc db, IfcGridAxis a) : base(db) { mAxisTag = a.mAxisTag; AxisCurve = db.Factory.Duplicate(a.AxisCurve) as IfcCurve; mSameSense = a.mSameSense; }
		public IfcGridAxis(string tag, IfcCurve axis, bool sameSense) : base(axis.Database) { if (!string.IsNullOrEmpty(tag)) mAxisTag = tag.Replace("'", ""); mAxisCurve = axis.mIndex; mSameSense = sameSense; }
	}
	public partial class IfcGridPlacement : IfcObjectPlacement
	{
		internal int mPlacementLocation;// : IfcVirtualGridIntersection ;
		internal int mPlacementRefDirection;// : OPTIONAL IfcVirtualGridIntersection;

		public IfcVirtualGridIntersection PlacementLocation { get { return mDatabase[mPlacementLocation] as IfcVirtualGridIntersection; } set { mPlacementLocation = value.mIndex; } }
		public IfcVirtualGridIntersection PlacementRefDirection { get { return mDatabase[mPlacementRefDirection] as IfcVirtualGridIntersection; } set { mPlacementRefDirection = (value == null ? 0 : value.mIndex); } }

		internal IfcGridPlacement() : base() { }
		internal IfcGridPlacement(DatabaseIfc db, IfcGridPlacement p) : base(db, p)
		{
			PlacementLocation = db.Factory.Duplicate(p.PlacementLocation) as IfcVirtualGridIntersection;
			if (p.mPlacementRefDirection > 0)
				PlacementRefDirection = db.Factory.Duplicate(p.PlacementRefDirection) as IfcVirtualGridIntersection;
		}
	}
	public partial class IfcGroup : IfcObject //SUPERTYPE OF (ONEOF (IfcAsset ,IfcCondition ,IfcInventory ,IfcStructuralLoadGroup ,IfcStructuralResultGroup ,IfcSystem ,IfcZone))
	{
		//INVERSE
		internal List<IfcRelAssignsToGroup> mIsGroupedBy = new List<IfcRelAssignsToGroup>();// IFC4 SET : IfcRelAssignsToGroup FOR RelatingGroup;
		public ReadOnlyCollection<IfcRelAssignsToGroup> IsGroupedBy { get { return new ReadOnlyCollection<IfcRelAssignsToGroup>( mIsGroupedBy); } }

		internal IfcGroup() : base() { }
		internal IfcGroup(DatabaseIfc db, IfcGroup g, IfcOwnerHistory ownerHistory, bool downStream) : base(db, g, ownerHistory, downStream) { }
		public IfcGroup(DatabaseIfc m, string name) : base(m) { Name = name; new IfcRelAssignsToGroup(this); }
		internal IfcGroup(List<IfcObjectDefinition> ods) : base(ods[0].mDatabase) { mIsGroupedBy.Add(new IfcRelAssignsToGroup(this, ods)); }

		internal void assign(IfcObjectDefinition o) { mIsGroupedBy[0].AddRelated(o); }
	}
}
