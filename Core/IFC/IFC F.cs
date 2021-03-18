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
using System.Collections.Specialized;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public partial class IfcFace : IfcTopologicalRepresentationItem //	SUPERTYPE OF(IfcFaceSurface)
	{
		private SET<IfcFaceBound> mBounds = new SET<IfcFaceBound>();// : SET [1:?] OF IfcFaceBound;
		public SET<IfcFaceBound> Bounds { get { return mBounds; } set { mBounds.Clear(); if (value != null) mBounds = value; } }

		internal IfcFace() : base() { }
		internal IfcFace(DatabaseIfc db, IfcFace f, DuplicateOptions options) : base(db, f, options) { Bounds.AddRange(f.Bounds.ConvertAll(x=>db.Factory.Duplicate(x) as IfcFaceBound)); }
		public IfcFace(IfcFaceOuterBound outer) : base(outer.mDatabase) { mBounds.Add(outer); }
		public IfcFace(IfcFaceOuterBound outer, IfcFaceBound inner) : this(outer) { mBounds.Add(inner); }
		public IfcFace(List<IfcFaceBound> bounds) : base(bounds[0].mDatabase) { mBounds.AddRange(bounds); }

		protected override void initialize()
		{
			base.initialize();
			mBounds.CollectionChanged += mBounds_CollectionChanged;
		}
		private void mBounds_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcFaceBound bound in e.NewItems)
				{
					bound.mBoundOf = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcFaceBound bound in e.OldItems)
				{
					if (bound.mBoundOf == this)
						bound.mBoundOf = null;
				}
			}
		}

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach(IfcFaceBound b in Bounds)
				result.AddRange(b.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public partial class IfcFaceBasedSurfaceModel : IfcGeometricRepresentationItem, IfcSurfaceOrFaceSurface
	{
		private SET<IfcConnectedFaceSet> mFbsmFaces = new SET<IfcConnectedFaceSet>();// : SET [1:?] OF IfcConnectedFaceSet;
		public SET<IfcConnectedFaceSet> FbsmFaces { get { return mFbsmFaces; } }

		internal IfcFaceBasedSurfaceModel() : base() { }
		internal IfcFaceBasedSurfaceModel(DatabaseIfc db, IfcFaceBasedSurfaceModel s, DuplicateOptions options) : base(db, s, options) { FbsmFaces.AddRange(s.FbsmFaces.Select(x => db.Factory.Duplicate(x) as IfcConnectedFaceSet)); }
		public IfcFaceBasedSurfaceModel(IfcConnectedFaceSet faceSet) : base(faceSet.mDatabase) { mFbsmFaces.Add(faceSet); }
		public IfcFaceBasedSurfaceModel(IEnumerable<IfcConnectedFaceSet> faceSets) : base(faceSets.First().mDatabase) { mFbsmFaces.AddRange(faceSets); }
	}
	[Serializable]
	public partial class IfcFaceBound : IfcTopologicalRepresentationItem //SUPERTYPE OF (ONEOF (IfcFaceOuterBound))
	{
		private IfcLoop mBound;// : IfcLoop;
		internal bool mOrientation = true;// : BOOLEAN;
		//INVERSE GG
		internal IfcFace mBoundOf = null;

		public IfcLoop Bound { get { return mBound; } set { if (mBound != null) mBoundOf = null; mBound = value; if (value != null) mBound.mLoopOf = this; } }
		public bool Orientation { get { return mOrientation; } set { mOrientation = value; } }

		internal IfcFaceBound() : base() { }
		internal IfcFaceBound(DatabaseIfc db, IfcFaceBound b, DuplicateOptions options) : base(db, b, options) { Bound = db.Factory.Duplicate(b.Bound) as IfcLoop; mOrientation = b.mOrientation; }
		public IfcFaceBound(IfcLoop l, bool orientation) : base(l.mDatabase) { Bound = l; mOrientation = orientation; }
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			result.AddRange(Bound.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public partial class IfcFaceOuterBound : IfcFaceBound
	{
		internal IfcFaceOuterBound() : base() { }
		internal IfcFaceOuterBound(DatabaseIfc db, IfcFaceOuterBound b, DuplicateOptions options) : base(db, b, options) { }
		public IfcFaceOuterBound(IfcLoop l, bool orientation) : base(l, orientation) { }
	}
	[Serializable]
	public partial class IfcFaceSurface : IfcFace, IfcSurfaceOrFaceSurface //SUPERTYPE OF(IfcAdvancedFace)
	{
		internal IfcSurface mFaceSurface;// : IfcSurface;
		internal bool mSameSense = true;// : BOOLEAN;

		public IfcSurface FaceSurface { get { return mFaceSurface; } set { if (mFaceSurface != null) mFaceSurface.OfFaces.Remove(this); mFaceSurface = value; if (value != null) value.OfFaces.Add(this); } }
		public bool SameSense { get { return mSameSense; } set { mSameSense = value; } }

		internal IfcFaceSurface() : base() { } 
		internal IfcFaceSurface(DatabaseIfc db, IfcFaceSurface s, DuplicateOptions options) : base(db, s, options) { FaceSurface = db.Factory.Duplicate( s.FaceSurface) as IfcSurface; mSameSense = s.mSameSense; }
		public IfcFaceSurface(IfcFaceOuterBound bound, IfcSurface srf, bool sameSense) : base(bound) { FaceSurface = srf; mSameSense = sameSense; }
		public IfcFaceSurface(IfcFaceOuterBound outer, IfcFaceBound inner, IfcSurface srf, bool sameSense) : base(outer, inner) { FaceSurface = srf; mSameSense = sameSense; }
		public IfcFaceSurface(List<IfcFaceBound> bounds, IfcSurface srf, bool sameSense) : base(bounds) { FaceSurface = srf; mSameSense = sameSense; }
	}
	[Serializable]
	public partial class IfcFacetedBrep : IfcManifoldSolidBrep
	{
		internal IfcFacetedBrep() : base() { }
		internal IfcFacetedBrep(DatabaseIfc db, IfcFacetedBrep b, DuplicateOptions options) : base(db, b, options) { }
		public IfcFacetedBrep(IfcClosedShell s) : base(s) { }
	}
	[Serializable]
	public partial class IfcFacetedBrepWithVoids : IfcFacetedBrep
	{
		internal List<int> mVoids = new List<int>();// : SET [1:?] OF IfcClosedShell
		public ReadOnlyCollection<IfcClosedShell> Voids { get { return new ReadOnlyCollection<IfcClosedShell>( mVoids.ConvertAll(x => mDatabase[x] as IfcClosedShell)); } }

		internal IfcFacetedBrepWithVoids() : base() { }
		internal IfcFacetedBrepWithVoids(DatabaseIfc db, IfcFacetedBrepWithVoids b, DuplicateOptions options) : base(db, b, options) { b.Voids.ToList().ForEach(x=>addVoid( db.Factory.Duplicate(x) as IfcClosedShell)); }
		public IfcFacetedBrepWithVoids(IfcClosedShell s, IEnumerable<IfcClosedShell> voids) : base(s) { mVoids.AddRange(voids.Select(x=>x.Index)); }
		
		internal void addVoid(IfcClosedShell shell) { mVoids.Add(shell.mIndex); }
	}
	[Serializable]
	public partial class IfcFacility : IfcSpatialStructureElement //IFC4x2 //SUPERTYPE OF(IfcBridge, IfcBuilding)
	{
		internal IfcFacility() : base() { }
		public IfcFacility(DatabaseIfc db) : base(db.Factory.RootPlacement) { }
		internal IfcFacility(DatabaseIfc db, IfcFacility f, DuplicateOptions options) : base(db, f, options) { }
		public IfcFacility(DatabaseIfc db, string name) : base(db.Factory.RootPlacement) { Name = name; }
		public IfcFacility(IfcSpatialStructureElement host, string name) : base(host, name) { }
		internal IfcFacility(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		public IfcFacility(IfcFacility host, string name, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { Name = name; }
	}
	[Serializable]
	public partial class IfcFacilityPart : IfcSpatialStructureElement //IFC4x2 //SUPERTYPE OF(IfcBridgePart)
	{
		private IfcFacilityPartTypeSelect mPredefinedType = new IfcFacilityPartTypeSelect(IfcFacilityPartCommonTypeEnum.NOTDEFINED);// : IfcFacilityPartTypeSelect;
		private IfcFacilityUsageEnum mUsageType = IfcFacilityUsageEnum.NOTDEFINED;// : IfcFacilityUsageEnum;

		public IfcFacilityPartTypeSelect PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcFacilityUsageEnum UsageType { get { return mUsageType; } set { mUsageType = value; } }

		public IfcFacilityPart() : base() { }
		internal IfcFacilityPart(DatabaseIfc db) : base(db) { }
		internal IfcFacilityPart(DatabaseIfc db, IfcFacilityPart f, DuplicateOptions options) : base(db, f, options) { }
		internal IfcFacilityPart(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		public IfcFacilityPart(IfcFacility host, string name, IfcFacilityPartTypeSelect predefined, IfcFacilityUsageEnum usage) : base(host, name) { mPredefinedType = predefined; mUsageType = usage; }
		public IfcFacilityPart(IfcFacilityPart host, string name, IfcFacilityPartTypeSelect predefined, IfcFacilityUsageEnum usage) : base(host, name) { mPredefinedType = predefined; mUsageType = usage; }
		public IfcFacilityPart(IfcFacility host, string name, IfcObjectPlacement p, IfcProductDefinitionShape r, IfcFacilityPartTypeSelect predefined, IfcFacilityUsageEnum usage) : base(host, p, r) { Name = name; mPredefinedType = predefined; mUsageType = usage; }
	}
	[Serializable]
	public partial class IfcFailureConnectionCondition : IfcStructuralConnectionCondition
	{
		private double mTensionFailureX = double.NaN; //: OPTIONAL IfcForceMeasure;
		private double mTensionFailureY = double.NaN; //: OPTIONAL IfcForceMeasure;
		private double mTensionFailureZ = double.NaN; //: OPTIONAL IfcForceMeasure;
		private double mCompressionFailureX = double.NaN; //: OPTIONAL IfcForceMeasure;
		private double mCompressionFailureY = double.NaN; //: OPTIONAL IfcForceMeasure;
		private double mCompressionFailureZ = double.NaN; //: OPTIONAL IfcForceMeasure;

		public double TensionFailureX { get { return mTensionFailureX; } set { mTensionFailureX = value; } }
		public double TensionFailureY { get { return mTensionFailureY; } set { mTensionFailureY = value; } }
		public double TensionFailureZ { get { return mTensionFailureZ; } set { mTensionFailureZ = value; } }
		public double CompressionFailureX { get { return mCompressionFailureX; } set { mCompressionFailureX = value; } }
		public double CompressionFailureY { get { return mCompressionFailureY; } set { mCompressionFailureY = value; } }
		public double CompressionFailureZ { get { return mCompressionFailureZ; } set { mCompressionFailureZ = value; } }

		public IfcFailureConnectionCondition() : base() { }
		public IfcFailureConnectionCondition(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcFan : IfcFlowMovingDevice //IFC4
	{
		internal IfcFanTypeEnum mPredefinedType = IfcFanTypeEnum.NOTDEFINED;// OPTIONAL : IfcFanTypeEnum;
		public IfcFanTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFan() : base() { }
		internal IfcFan(DatabaseIfc db, IfcFan f, DuplicateOptions options) : base(db, f, options) { mPredefinedType = f.mPredefinedType; }
		public IfcFan(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcFanType : IfcFlowMovingDeviceType
	{
		internal IfcFanTypeEnum mPredefinedType = IfcFanTypeEnum.NOTDEFINED;// : IfcFanTypeEnum;
		public IfcFanTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFanType() : base() { }
		internal IfcFanType(DatabaseIfc db, IfcFanType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcFanType(DatabaseIfc db, string name, IfcFanTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcFastener : IfcElementComponent
	{
		internal IfcFastenerTypeEnum mPredefinedType = IfcFastenerTypeEnum.NOTDEFINED;// : IfcFastenerTypeEnum; //IFC4
		public IfcFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFastener() : base() { }
		internal IfcFastener(DatabaseIfc db, IfcFastener f, DuplicateOptions options) : base(db, f, options) { mPredefinedType = f.mPredefinedType; }
		public IfcFastener(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcFastenerType : IfcElementComponentType
	{
		internal IfcFastenerTypeEnum mPredefinedType = IfcFastenerTypeEnum.NOTDEFINED;// : IfcFastenerTypeEnum; //IFC4
		public IfcFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFastenerType() : base() { }
		internal IfcFastenerType(DatabaseIfc db, IfcFastenerType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcFastenerType(DatabaseIfc m, string name, IfcFastenerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcFeatureElement : IfcElement //	ABSTRACT SUPERTYPE OF(ONEOF(IfcFeatureElementAddition, IfcFeatureElementSubtraction, IfcSurfaceFeature))
	{
		protected IfcFeatureElement() : base() { }
		protected IfcFeatureElement(DatabaseIfc db) : base(db) {  }
		protected IfcFeatureElement(DatabaseIfc db, IfcFeatureElement e, DuplicateOptions options) : base(db, e, options) { }
		protected IfcFeatureElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcFeatureElementAddition : IfcFeatureElement //ABSTRACT SUPERTYPE OF(IfcProjectionElement)
	{	//INVERSE
		internal List<IfcRelProjectsElement> mProjectsElements = new List<IfcRelProjectsElement>();
		public ReadOnlyCollection<IfcRelProjectsElement> ProjectsElements { get { return new ReadOnlyCollection<IfcRelProjectsElement>( mProjectsElements); } }

		protected IfcFeatureElementAddition() : base() { }
		protected IfcFeatureElementAddition(DatabaseIfc db, IfcFeatureElementAddition e, DuplicateOptions options) : base(db, e, options){ }
	}
	[Serializable]
	public abstract partial class IfcFeatureElementSubtraction : IfcFeatureElement //ABSTRACT SUPERTYPE OF (ONEOF (IfcOpeningElement ,IfcVoidingFeature)) 
	{ //INVERSE
		internal IfcRelVoidsElement mVoidsElement = null;
		public IfcRelVoidsElement VoidsElement { get { return mVoidsElement; } set { mVoidsElement = value; } }

		protected IfcFeatureElementSubtraction() : base() { }
		protected IfcFeatureElementSubtraction(DatabaseIfc db, IfcFeatureElementSubtraction e, DuplicateOptions options) 
			: base(db, e, options)
		{
			IfcRelVoidsElement relVoidsElement = e.VoidsElement;
			
			VoidsElement = db.Factory.Duplicate(relVoidsElement, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcRelVoidsElement;
			VoidsElement.RelatingBuildingElement = db.Factory.Duplicate(relVoidsElement.RelatingBuildingElement, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcElement;
			VoidsElement.RelatedOpeningElement = this;
		}
		protected IfcFeatureElementSubtraction(DatabaseIfc db) : base(db) {  }
		protected IfcFeatureElementSubtraction(IfcElement host, IfcProductDefinitionShape rep) : base(host.mDatabase)
		{
			new IfcRelVoidsElement(host, this);
			Representation = rep;
			ObjectPlacement = new IfcLocalPlacement(host.ObjectPlacement, mDatabase.Factory.XYPlanePlacement);	
		}
		protected IfcFeatureElementSubtraction(IfcElement host, IfcObjectPlacement placement, IfcProductDefinitionShape representation)
			: base(host.Database) 
		{
			new IfcRelVoidsElement(host, this);
			ObjectPlacement = placement;
			Representation = representation;
		}
	}
	[Serializable]
	public partial class IfcFillAreaStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal SET<IfcFillStyleSelect> mFillStyles = new SET<IfcFillStyleSelect>();// : SET [1:?] OF IfcFillStyleSelect;
		internal bool mModelorDraughting = true;//	:	OPTIONAL BOOLEAN;
		public SET<IfcFillStyleSelect> FillStyles { get { return mFillStyles; } }// : SET [1:?] OF IfcFillStyleSelect;
		public bool ModelorDraughting { get { return mModelorDraughting; } set { mModelorDraughting = value; } }

		internal IfcFillAreaStyle() : base() { }
		//internal IfcFillAreaStyle(IfcFillAreaStyle i) : base(i) { mFillStyles = new List<int>(i.mFillStyles.ToArray()); }
		public IfcFillAreaStyle(IfcFillStyleSelect style) : base(style.Database) { mFillStyles.Add(style); }
		public IfcFillAreaStyle(IEnumerable<IfcFillStyleSelect> styles) : base(styles.First().Database) { mFillStyles.AddRange(styles); }
		internal IfcFillAreaStyle(DatabaseIfc db, IfcFillAreaStyle fillAreaStyle) : base(db, fillAreaStyle) { FillStyles.AddRange(fillAreaStyle.FillStyles.Select(x => db.Factory.Duplicate(x) as IfcFillStyleSelect)); }
	}
	[Serializable]
	public partial class IfcFillAreaStyleHatching : IfcGeometricRepresentationItem, IfcFillStyleSelect
	{
		internal int mHatchLineAppearance;// : IfcCurveStyle;
		internal string mStartOfNextHatchLine;// : IfcHatchLineDistanceSelect; IfcOneDirectionRepeatFactor,IfcPositiveLengthMeasure
		internal int mPointOfReferenceHatchLine;// : OPTIONAL IfcCartesianPoint; //DEPRECATED IFC4
		internal int mPatternStart;// : OPTIONAL IfcCartesianPoint;
		internal double mHatchLineAngle;// : IfcPlaneAngleMeasure;

		public IfcCurveStyle HatchLineAppearance { get { return mDatabase[mHatchLineAppearance] as IfcCurveStyle; } set { mHatchLineAppearance = value.mIndex; } }
		public IfcCartesianPoint PatternStart { get { return mDatabase[mPatternStart] as IfcCartesianPoint; } set { mPatternStart = (value == null ? 0 : value.mIndex); } }

		internal IfcFillAreaStyleHatching() : base() { }
		internal IfcFillAreaStyleHatching(DatabaseIfc db, IfcFillAreaStyleHatching h, DuplicateOptions options) : base(db, h, options)
		{
			mHatchLineAppearance = db.Factory.Duplicate(h.HatchLineAppearance).mIndex;
			mStartOfNextHatchLine = h.mStartOfNextHatchLine;
			if (h.mPointOfReferenceHatchLine > 0)
				mPointOfReferenceHatchLine = db.Factory.Duplicate(h.mDatabase[h.mPointOfReferenceHatchLine]).mIndex;
			if (h.mPatternStart > 0)
				PatternStart = db.Factory.Duplicate(h.PatternStart) as IfcCartesianPoint;
			mHatchLineAngle = h.mHatchLineAngle;
		}
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcFillAreaStyleTileSymbolWithStyle // DEPRECATED IFC4
	[Serializable]
	public partial class IfcFillAreaStyleTiles : IfcGeometricRepresentationItem, IfcFillStyleSelect
	{
		private LIST<IfcVector> mTilingPattern = new LIST<IfcVector>(); //: LIST[2:2] OF IfcVector;
		private SET<IfcStyledItem> mTiles = new SET<IfcStyledItem>(); //: SET[1:?] OF IfcStyledItem;
		private double mTilingScale = 0; //: IfcPositiveRatioMeasure;

		public LIST<IfcVector> TilingPattern { get { return mTilingPattern; } set { mTilingPattern = value; } }
		public SET<IfcStyledItem> Tiles { get { return mTiles; } set { mTiles = value; } }
		public double TilingScale { get { return mTilingScale; } set { mTilingScale = value; } }

		public IfcFillAreaStyleTiles() : base() { }
		public IfcFillAreaStyleTiles(IEnumerable<IfcVector> tilingPattern, IEnumerable<IfcStyledItem> tiles, double tilingScale)
			: base(tilingPattern.First().Database)
		{
			TilingPattern.AddRange(tilingPattern);
			Tiles.AddRange(tiles);
			TilingScale = tilingScale;
		}
	}
	public interface IfcFillStyleSelect : IBaseClassIfc { } // SELECT ( IfcFillAreaStyleHatching, IfcFillAreaStyleTiles, IfcExternallyDefinedHatchStyle, IfcColour);
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcFilter : IfcFlowTreatmentDevice //IFC4  
	{
		internal IfcFilterTypeEnum mPredefinedType = IfcFilterTypeEnum.NOTDEFINED;
		public IfcFilterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFilter() : base() { }
		internal IfcFilter(DatabaseIfc db, IfcFilter f, DuplicateOptions options) : base(db, f, options) { mPredefinedType = f.mPredefinedType; }
		public IfcFilter(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcFilterType : IfcFlowTreatmentDeviceType
	{
		internal IfcFilterTypeEnum mPredefinedType = IfcFilterTypeEnum.NOTDEFINED;// : IfcFilterTypeEnum;
		public IfcFilterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFilterType() : base() { }
		internal IfcFilterType(DatabaseIfc db, IfcFilterType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcFilterType(DatabaseIfc db, string name, IfcFilterTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcFireSuppressionTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcFireSuppressionTerminalTypeEnum mPredefinedType = IfcFireSuppressionTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcFireSuppressinTerminalTypeEnum;
		public IfcFireSuppressionTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcFireSuppressionTerminal() : base() { }
		internal IfcFireSuppressionTerminal(DatabaseIfc db, IfcFireSuppressionTerminal t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcFireSuppressionTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcFireSuppressionTerminalType : IfcFlowTerminalType
	{
		internal IfcFireSuppressionTerminalTypeEnum mPredefinedType = IfcFireSuppressionTerminalTypeEnum.NOTDEFINED;// : IfcFireSuppressionTerminalTypeEnum;
		public IfcFireSuppressionTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFireSuppressionTerminalType() : base() { }
		internal IfcFireSuppressionTerminalType(DatabaseIfc db, IfcFireSuppressionTerminalType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcFireSuppressionTerminalType(DatabaseIfc m, string name, IfcFireSuppressionTerminalTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcFixedReferenceSweptAreaSolid : IfcDirectrixCurveSweptAreaSolid //IFC4
	{
		internal int mFixedReference;// : 	IfcDirection; 

		public IfcDirection FixedReference { get { return mDatabase[mFixedReference] as IfcDirection; } set { mFixedReference = value.mIndex; } }

		internal IfcFixedReferenceSweptAreaSolid() : base() { }
		internal IfcFixedReferenceSweptAreaSolid(DatabaseIfc db, IfcFixedReferenceSweptAreaSolid s, DuplicateOptions options) : base(db, s, options)
		{
			FixedReference = db.Factory.Duplicate(s.FixedReference) as IfcDirection;
		}
		public IfcFixedReferenceSweptAreaSolid(IfcProfileDef sweptArea, IfcCurve directrix, IfcDirection reference) 
			: base(sweptArea, directrix) { FixedReference = reference; }
	}
	[Serializable]
	public partial class IfcFlowController : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcAirTerminalBox, IfcDamper
	{ //, IfcElectricDistributionBoard, IfcElectricTimeControl, IfcFlowMeter, IfcProtectiveDevice, IfcSwitchingDevice, IfcValve))
		public override string StepClassName { get { return mDatabase.mRelease < ReleaseVersion.IFC4 && this as IfcElectricDistributionPoint == null ? "IfcFlowController" : base.StepClassName; } }

		internal IfcFlowController() : base() { }
		internal IfcFlowController(DatabaseIfc db) : base(db) { }
		internal IfcFlowController(DatabaseIfc db, IfcFlowController c, DuplicateOptions options) : base(db,c, options) { }
		public IfcFlowController(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowControllerType : IfcDistributionFlowElementType
	{
		protected IfcFlowControllerType() : base() { }
		protected IfcFlowControllerType(DatabaseIfc db) : base(db) { }
		protected IfcFlowControllerType(DatabaseIfc db, IfcFlowControllerType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcFlowFitting : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcCableCarrierFitting, IfcCableFitting, IfcDuctFitting, IfcJunctionBox, IfcPipeFitting))
	{
		public override string StepClassName { get { return mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowFitting" : base.StepClassName; } }

		internal IfcFlowFitting() : base() { }
		internal IfcFlowFitting(DatabaseIfc db, IfcFlowFitting f, DuplicateOptions options) : base(db,f, options) { }
		public IfcFlowFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowFittingType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcCableCarrierFittingType ,IfcDuctFittingType ,IfcJunctionBoxType ,IfcPipeFittingType))
	{
		protected IfcFlowFittingType() : base() { }
		protected IfcFlowFittingType(DatabaseIfc db) : base(db) { }
		protected IfcFlowFittingType(DatabaseIfc db, IfcFlowFittingType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcFlowInstrument : IfcDistributionControlElement //IFC4  
	{
		internal IfcFlowInstrumentTypeEnum mPredefinedType = IfcFlowInstrumentTypeEnum.NOTDEFINED;
		public IfcFlowInstrumentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowInstrument() : base() { }
		internal IfcFlowInstrument(DatabaseIfc db, IfcFlowInstrument i, DuplicateOptions options) : base(db, i, options) { mPredefinedType = i.mPredefinedType; }
		public IfcFlowInstrument(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcFlowInstrumentType : IfcDistributionControlElementType
	{
		internal IfcFlowInstrumentTypeEnum mPredefinedType = IfcFlowInstrumentTypeEnum.NOTDEFINED;// : IfcFlowInstrumentTypeEnum;
		public IfcFlowInstrumentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowInstrumentType() : base() { }
		internal IfcFlowInstrumentType(DatabaseIfc db, IfcFlowInstrumentType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcFlowInstrumentType(DatabaseIfc m, string name, IfcFlowInstrumentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcFlowMeter : IfcFlowController //IFC4
	{
		internal IfcFlowMeterTypeEnum mPredefinedType = IfcFlowMeterTypeEnum.NOTDEFINED;// OPTIONAL : IfcDamperTypeEnum;
		public IfcFlowMeterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowMeter() : base() { }
		internal IfcFlowMeter(DatabaseIfc db, IfcFlowMeter m, DuplicateOptions options) : base(db, m, options) { mPredefinedType = m.mPredefinedType; }
		public IfcFlowMeter(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcFlowMeterType : IfcFlowControllerType
	{
		internal IfcFlowMeterTypeEnum mPredefinedType = IfcFlowMeterTypeEnum.NOTDEFINED;// : IfcFlowMeterTypeEnum;
		public IfcFlowMeterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowMeterType() : base() { }
		internal IfcFlowMeterType(DatabaseIfc db, IfcFlowMeterType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcFlowMeterType(DatabaseIfc m, string name, IfcFlowMeterTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcFlowMovingDevice : IfcDistributionFlowElement //	SUPERTYPE OF(ONEOF(IfcCompressor, IfcFan, IfcPump))
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowMovingDevice" : base.StepClassName); } }

		internal IfcFlowMovingDevice() : base() { }
		internal IfcFlowMovingDevice(DatabaseIfc db, IfcFlowMovingDevice d, DuplicateOptions options) : base(db, d, options) { }
		public IfcFlowMovingDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowMovingDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF(ONEOF(IfcCompressorType, IfcFanType, IfcPumpType))
	{
		protected IfcFlowMovingDeviceType() : base() { }
		protected IfcFlowMovingDeviceType(DatabaseIfc db) : base(db) { }
		protected IfcFlowMovingDeviceType(DatabaseIfc db, IfcFlowMovingDeviceType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcFlowSegment : IfcDistributionFlowElement //	SUPERTYPE OF(ONEOF(IfcCableCarrierSegment, IfcCableSegment, IfcDuctSegment, IfcPipeSegment))
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowSegment" : base.StepClassName); } }

		internal IfcFlowSegment() : base() { }
		internal IfcFlowSegment(DatabaseIfc db) : base(db) { }
		internal IfcFlowSegment(DatabaseIfc db, IfcFlowSegment s, DuplicateOptions options) : base(db, s, options) { }
		public IfcFlowSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowSegmentType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcCableCarrierSegmentType ,IfcCableSegmentType ,IfcDuctSegmentType ,IfcPipeSegmentType))
	{
		protected IfcFlowSegmentType() : base() { }
		protected IfcFlowSegmentType(DatabaseIfc db) : base(db) { }
		protected IfcFlowSegmentType(DatabaseIfc db, IfcFlowSegmentType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcFlowStorageDevice : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcElectricFlowStorageDevice, IfcTank))
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowStorageDevice" : base.StepClassName); } }

		internal IfcFlowStorageDevice() : base() { }
		internal IfcFlowStorageDevice(DatabaseIfc db, IfcFlowStorageDevice d, DuplicateOptions options) : base(db,d, options) { }
		public IfcFlowStorageDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowStorageDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcElectricFlowStorageDeviceType ,IfcTankType))
	{
		protected IfcFlowStorageDeviceType() : base() { }
		protected IfcFlowStorageDeviceType(DatabaseIfc db) : base(db) { }
		protected IfcFlowStorageDeviceType(DatabaseIfc db, IfcFlowStorageDeviceType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcFlowTerminal : IfcDistributionFlowElement 	//SUPERTYPE OF(ONEOF(IfcAirTerminal, IfcAudioVisualAppliance, IfcCommunicationsAppliance, IfcElectricAppliance, IfcFireSuppressionTerminal, IfcLamp, IfcLightFixture, IfcMedicalDevice, IfcOutlet, IfcSanitaryTerminal, IfcSpaceHeater, IfcStackTerminal, IfcWasteTerminal))
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowTerminal" : base.StepClassName); } }

		internal IfcFlowTerminal() : base() { }
		internal IfcFlowTerminal(DatabaseIfc db) : base(db) { }
		protected IfcFlowTerminal(IfcFlowTerminal basis, bool replace) : base(basis, replace) { }
		internal IfcFlowTerminal(DatabaseIfc db, IfcFlowTerminal t, DuplicateOptions options) : base(db, t, options) { }
		public IfcFlowTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowTerminalType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcAirTerminalType ,
	{ // IfcElectricApplianceType ,IfcElectricHeaterType ,IfcFireSuppressionTerminalType,IfcLampType ,IfcLightFixtureType ,IfcOutletType ,IfcSanitaryTerminalType ,IfcStackTerminalType ,IfcWasteTerminalType)) 
		// IFC4 deleted ,IfcGasTerminalType 
		protected IfcFlowTerminalType() : base() { }
		protected IfcFlowTerminalType(IfcDistributionFlowElementType basis) : base(basis) { }
		protected IfcFlowTerminalType(DatabaseIfc db) : base(db) { }
		protected IfcFlowTerminalType(DatabaseIfc db, IfcFlowTerminalType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcFlowTreatmentDevice : IfcDistributionFlowElement // 	SUPERTYPE OF(ONEOF(IfcDuctSilencer, IfcFilter, IfcInterceptor))
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowTreatmentDevice" : base.StepClassName); } }

		internal IfcFlowTreatmentDevice() : base() { }
		internal IfcFlowTreatmentDevice(DatabaseIfc db) : base(db) { }
		internal IfcFlowTreatmentDevice(DatabaseIfc db, IfcFlowTreatmentDevice d, DuplicateOptions options) : base(db, d, options) { }
		public IfcFlowTreatmentDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowTreatmentDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF(ONEOF(IfcDuctSilencerType, IfcFilterType, IfcInterceptorType))
	{
		protected IfcFlowTreatmentDeviceType() : base() { }
		protected IfcFlowTreatmentDeviceType(DatabaseIfc db) : base(db) { }
		protected IfcFlowTreatmentDeviceType(DatabaseIfc db,  IfcFlowTreatmentDeviceType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcFluidFlowProperties : IfcPropertySetDefinition 
	{
		internal IfcPropertySourceEnum mPropertySource;// : IfcPropertySourceEnum;
		internal int mFlowConditionTimeSeries, mVelocityTimeSeries, mFlowrateTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal int mFluid;// : IfcMaterial;
		internal int mPressureTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal string mUserDefinedPropertySource = "$";// : OPTIONAL IfcLabel;
		internal double mTemperatureSingleValue = double.NaN, mWetBulbTemperatureSingleValue = double.NaN;// : OPTIONAL IfcThermodynamicTemperatureMeasure;
		internal int mWetBulbTemperatureTimeSeries, mTemperatureTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal double mFlowrateSingleValue = double.NaN;// : OPTIONAL IfcDerivedMeasureValue;
		internal double mFlowConditionSingleValue = double.NaN;// : OPTIONAL IfcPositiveRatioMeasure;
		internal double mVelocitySingleValue = double.NaN;// : OPTIONAL IfcLinearVelocityMeasure;
		internal double mPressureSingleValue = double.NaN;// : OPTIONAL IfcPressureMeasure;

		internal IfcFluidFlowProperties() : base() { }
		internal IfcFluidFlowProperties(DatabaseIfc db, IfcFluidFlowProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mPropertySource = p.mPropertySource;
			//if(p.mFlowConditionTimeSeries > 0)
			//	mFlowConditionTimeSeries = p.mFlowConditionTimeSeries;

			//mVelocityTimeSeries = p.mVelocityTimeSeries;
			//mFlowrateTimeSeries = p.mFlowrateTimeSeries;
			//mFluid = p.mFluid;
			//mPressureTimeSeries = p.mPressureTimeSeries;
			mUserDefinedPropertySource = p.mUserDefinedPropertySource;
			mTemperatureSingleValue = p.mTemperatureSingleValue;
			mWetBulbTemperatureSingleValue = p.mWetBulbTemperatureSingleValue;
			//mWetBulbTemperatureTimeSeries = p.mWetBulbTemperatureTimeSeries;
			//mTemperatureTimeSeries = p.mTemperatureTimeSeries;
			mFlowrateSingleValue = p.mFlowrateSingleValue;
			mFlowConditionSingleValue = p.mFlowConditionSingleValue;
			mVelocitySingleValue = p.mVelocitySingleValue;
			mPressureSingleValue = p.mPressureSingleValue;
		}
		public IfcFluidFlowProperties(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Serializable]
	public partial class IfcFooting : IfcBuiltElement
	{
		internal IfcFootingTypeEnum mPredefinedType = IfcFootingTypeEnum.NOTDEFINED;// OPTIONAL : IfcFootingTypeEnum;
		public IfcFootingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFooting() : base() { }
		internal IfcFooting(DatabaseIfc db, IfcFooting f, DuplicateOptions options) : base(db, f, options) { mPredefinedType = f.mPredefinedType; }
		public IfcFooting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcFootingType : IfcBuiltElementType
	{
		internal IfcFootingTypeEnum mPredefinedType = IfcFootingTypeEnum.NOTDEFINED;
		public IfcFootingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFootingType() : base() { }
		internal IfcFootingType(DatabaseIfc db, IfcFootingType t, DuplicateOptions options) : base(db,t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcFootingType(DatabaseIfc m, string name, IfcFootingTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcFootingType(string name, IfcMaterialProfileSet mps, IfcFootingTypeEnum type) : base(mps.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = mps; }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcFuelProperties
	[Serializable]
	public partial class IfcFurnishingElement : IfcElement // DEPRECATED IFC4 to make abstract SUPERTYPE OF(ONEOF(IfcFurniture, IfcSystemFurnitureElement))
	{
		internal IfcFurnishingElement() : base() { }
		internal IfcFurnishingElement(DatabaseIfc db, IfcFurnishingElement e, DuplicateOptions options) : base(db, e, options) { }
		public IfcFurnishingElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcFurnishingElementType : IfcElementType //IFC4 DEPRECATED //SUPERTYPE OF (ONEOF (IfcFurnitureType ,IfcSystemFurnitureElementType))
	{
		internal IfcFurnishingElementType() : base() { }
		internal IfcFurnishingElementType(DatabaseIfc db, IfcFurnishingElementType t, DuplicateOptions options) : base(db, t, options) { }
		public IfcFurnishingElementType(DatabaseIfc db, string name) : base(db) { Name = name; }
	}
	[Serializable]
	public partial class IfcFurniture : IfcFurnishingElement
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFurnishingElement" : base.StepClassName); } }

		internal IfcFurnitureTypeEnum mPredefinedType = IfcFurnitureTypeEnum.NOTDEFINED;//: OPTIONAL IfcFurnitureTypeEnum;
		public IfcFurnitureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFurniture() : base() { }
		internal IfcFurniture(DatabaseIfc db, IfcFurniture f, DuplicateOptions options) : base(db, f, options) { mPredefinedType = f.mPredefinedType; }
		public IfcFurniture(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcFurnitureStandard : IfcControl 
	{
		internal IfcFurnitureStandard() : base() { }
		internal IfcFurnitureStandard(DatabaseIfc db, IfcFurnitureStandard s, DuplicateOptions options) : base(db,s, options) { }
	}
	[Serializable]
	public partial class IfcFurnitureType : IfcFurnishingElementType
	{
		internal IfcAssemblyPlaceEnum mAssemblyPlace = IfcAssemblyPlaceEnum.NOTDEFINED;
		internal IfcFurnitureTypeEnum mPredefinedType = IfcFurnitureTypeEnum.NOTDEFINED; // IFC4 OPTIONAL
		internal IfcFurnitureType() : base() { }
		internal IfcFurnitureType(DatabaseIfc db, IfcFurnitureType t, DuplicateOptions options) : base(db, t, options) { mAssemblyPlace = t.mAssemblyPlace; mPredefinedType = t.mPredefinedType; }
		public IfcFurnitureType(DatabaseIfc db, string name, IfcFurnitureTypeEnum type) : base(db, name)
		{
			mPredefinedType = type;
			if (mDatabase.mRelease < ReleaseVersion.IFC4 && string.IsNullOrEmpty(ElementType) && type != IfcFurnitureTypeEnum.NOTDEFINED)
				ElementType = type.ToString();
		}
	}
}
