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
	public partial class IfcFace : IfcTopologicalRepresentationItem //	SUPERTYPE OF(IfcFaceSurface)
	{
		internal SET<IfcFaceBound> mBounds = new SET<IfcFaceBound>();// : SET [1:?] OF IfcFaceBound;
		public SET<IfcFaceBound> Bounds { get { return mBounds; } set { mBounds.Clear(); if (value != null) mBounds = value; } }

		internal IfcFace() : base() { }
		internal IfcFace(DatabaseIfc db, IfcFace f) : base(db,f) { Bounds.AddRange(f.Bounds.ConvertAll(x=>db.Factory.Duplicate(x) as IfcFaceBound)); }
		public IfcFace(IfcFaceOuterBound outer) : base(outer.mDatabase) { mBounds.Add(outer); }
		public IfcFace(IfcFaceOuterBound outer, IfcFaceBound inner) : this(outer) { mBounds.Add(inner); }
		public IfcFace(List<IfcFaceBound> bounds) : base(bounds[0].mDatabase) { mBounds.AddRange(bounds); }

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
		private List<int> mFbsmFaces = new List<int>();// : SET [1:?] OF IfcConnectedFaceSet;
		public ReadOnlyCollection<IfcConnectedFaceSet> FbsmFaces { get { return new ReadOnlyCollection<IfcConnectedFaceSet>( mFbsmFaces.ConvertAll(x =>mDatabase[x] as IfcConnectedFaceSet)); } }

		internal IfcFaceBasedSurfaceModel() : base() { }
		internal IfcFaceBasedSurfaceModel(DatabaseIfc db, IfcFaceBasedSurfaceModel s) : base(db,s) { s.FbsmFaces.ToList().ForEach(x => addFace( db.Factory.Duplicate(x) as IfcConnectedFaceSet)); }
		public IfcFaceBasedSurfaceModel(IfcConnectedFaceSet face) : base(face.mDatabase) { mFbsmFaces.Add(face.mIndex); }
		public IfcFaceBasedSurfaceModel(List<IfcConnectedFaceSet> faces) : base(faces[0].mDatabase) { faces.ForEach(x => addFace(x)); }

		internal void addFace(IfcConnectedFaceSet face) { mFbsmFaces.Add(face.mIndex); }
	}
	[Serializable]
	public partial class IfcFaceBound : IfcTopologicalRepresentationItem //SUPERTYPE OF (ONEOF (IfcFaceOuterBound))
	{
		internal int mBound;// : IfcLoop;
		internal bool mOrientation = true;// : BOOLEAN;

		public IfcLoop Bound { get { return mDatabase[mBound] as IfcLoop; } set { mBound = value.mIndex; } }
		public bool Orientation { get { return mOrientation; } set { mOrientation = value; } }

		internal IfcFaceBound() : base() { }
		internal IfcFaceBound(DatabaseIfc db, IfcFaceBound b) : base(db,b) { Bound = db.Factory.Duplicate(b.Bound) as IfcLoop; mOrientation = b.mOrientation; }
		public IfcFaceBound(IfcLoop l, bool orientation) : base(l.mDatabase) { mBound = l.mIndex; mOrientation = orientation; }
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
		internal IfcFaceOuterBound(DatabaseIfc db, IfcFaceOuterBound b) : base(db,b) { }
		public IfcFaceOuterBound(IfcLoop l, bool orientation) : base(l, orientation) { }
	}
	[Serializable]
	public partial class IfcFaceSurface : IfcFace, IfcSurfaceOrFaceSurface //SUPERTYPE OF(IfcAdvancedFace)
	{
		internal int mFaceSurface;// : IfcSurface;
		internal bool mSameSense = true;// : BOOLEAN;

		public IfcSurface FaceSurface { get { return mDatabase[mFaceSurface] as IfcSurface; } set { mFaceSurface = value.mIndex; } }
		public bool SameSense { get { return mSameSense; } set { mSameSense = value; } }

		internal IfcFaceSurface() : base() { } 
		internal IfcFaceSurface(DatabaseIfc db, IfcFaceSurface s) : base(db,s) { FaceSurface = db.Factory.Duplicate( s.FaceSurface) as IfcSurface; mSameSense = s.mSameSense; }
		public IfcFaceSurface(IfcFaceOuterBound bound, IfcSurface srf, bool sameSense) : base(bound) { mFaceSurface = srf.mIndex; mSameSense = sameSense; }
		public IfcFaceSurface(IfcFaceOuterBound outer, IfcFaceBound inner, IfcSurface srf, bool sameSense) : base(outer, inner) { mFaceSurface = srf.mIndex; mSameSense = sameSense; }
		public IfcFaceSurface(List<IfcFaceBound> bounds, IfcSurface srf, bool sameSense) : base(bounds) { mFaceSurface = srf.mIndex; mSameSense = sameSense; }
	}
	[Serializable]
	public partial class IfcFacetedBrep : IfcManifoldSolidBrep
	{
		internal IfcFacetedBrep() : base() { }
		internal IfcFacetedBrep(DatabaseIfc db, IfcFacetedBrep b) : base(db,b) { }
		public IfcFacetedBrep(IfcClosedShell s) : base(s) { }
	}
	[Serializable]
	public partial class IfcFacetedBrepWithVoids : IfcFacetedBrep
	{
		internal List<int> mVoids = new List<int>();// : SET [1:?] OF IfcClosedShell
		public ReadOnlyCollection<IfcClosedShell> Voids { get { return new ReadOnlyCollection<IfcClosedShell>( mVoids.ConvertAll(x => mDatabase[x] as IfcClosedShell)); } }

		internal IfcFacetedBrepWithVoids() : base() { }
		internal IfcFacetedBrepWithVoids(DatabaseIfc db, IfcFacetedBrepWithVoids b) : base(db,b) { b.Voids.ToList().ForEach(x=>addVoid( db.Factory.Duplicate(x) as IfcClosedShell)); }
		public IfcFacetedBrepWithVoids(IfcClosedShell s, IEnumerable<IfcClosedShell> voids) : base(s) { mVoids.AddRange(voids.Select(x=>x.Index)); }
		
		internal void addVoid(IfcClosedShell shell) { mVoids.Add(shell.mIndex); }
	}
	[Serializable]
	public partial class IfcFacility : IfcSpatialStructureElement //IFC4x2 //SUPERTYPE OF(IfcBridge, IfcBuilding)
	{
		internal override string KeyWord { get { if(mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4X2) return "IfcBuilding"; return base.KeyWord;  } }
		internal double mElevationOfRefHeight = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal double mElevationOfTerrain = double.NaN;// : OPTIONAL IfcLengthMeasure;

		public double ElevationOfRefHeight { get { return mElevationOfRefHeight; } set { mElevationOfRefHeight = value; } }
		public double ElevationOfTerrain { get { return mElevationOfTerrain; } set { mElevationOfTerrain = value; } }

		internal IfcFacility() : base() { }
		internal IfcFacility(DatabaseIfc db, IfcFacility f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream)
		{
			mElevationOfRefHeight = f.mElevationOfRefHeight;
			mElevationOfTerrain = f.mElevationOfTerrain;
		}
		public IfcFacility(DatabaseIfc db, string name) : base(db.Factory.RootPlacement) { Name = name; }
		public IfcFacility(IfcSpatialStructureElement host, string name) : base(host, name) { }
		public IfcFacility(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcFacilityPart : IfcSpatialStructureElement //IFC4x2 //SUPERTYPE OF(IfcBuildingStorey)
	{
		internal override string KeyWord { get { if(mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4X2) return "IfcBuildingStorey"; return base.KeyWord;  } }
		internal double mElevation = double.NaN;// : OPTIONAL IfcLengthMeasure; 
		public double Elevation
		{
			get { return mElevation; }
			set
			{
				mElevation = Math.Abs(value) < mDatabase.Tolerance ? 0 : value;
#if (RHINO)
				try
				{
					int i = Rhino.RhinoDoc.ActiveDoc.NamedConstructionPlanes.Find(Name);
					if (i >= 0)
						Rhino.RhinoDoc.ActiveDoc.NamedConstructionPlanes.Delete(i);
					Rhino.RhinoDoc.ActiveDoc.NamedConstructionPlanes.Add(Name, new Rhino.Geometry.Plane(new Rhino.Geometry.Point3d(0, 0, mElevation), Rhino.Geometry.Vector3d.XAxis, Rhino.Geometry.Vector3d.YAxis));
				}
				catch (Exception) { }
#endif
			}
		}

		public IfcFacilityPart() : base() { }
		internal IfcFacilityPart(DatabaseIfc db, IfcFacilityPart f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mElevation = f.mElevation; }
		public IfcFacilityPart(IfcFacility host, string name) : base(host, name) { }
		public IfcFacilityPart(IfcFacilityPart host, string name) : base(host, name) { }
		public IfcFacilityPart(IfcFacility host, string name, double elevation) 
			: base(host, name, new IfcLocalPlacement(host.Placement, new IfcAxis2Placement3D(new IfcCartesianPoint(host.mDatabase, 0, 0, elevation))))
		{
			Elevation = elevation;
		}
		public IfcFacilityPart(IfcFacilityPart host, string name, double elevation)
			: base(host, name, new IfcLocalPlacement(host.Placement, new IfcAxis2Placement3D(new IfcCartesianPoint(host.mDatabase, 0, 0, elevation))))
		{
			Elevation = elevation;
		}
		public IfcFacilityPart(IfcFacility host, string name, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { Name = name; }
		internal bool addStorey(IfcBuildingStorey s) { return base.AddAggregated(s); }
	}
	//ENTITY IfcFailureConnectionCondition
	[Serializable]
	public partial class IfcFan : IfcFlowMovingDevice //IFC4
	{
		internal IfcFanTypeEnum mPredefinedType = IfcFanTypeEnum.NOTDEFINED;// OPTIONAL : IfcFanTypeEnum;
		public IfcFanTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFan() : base() { }
		internal IfcFan(DatabaseIfc db, IfcFan f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcFan(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcFanType : IfcFlowMovingDeviceType
	{
		internal IfcFanTypeEnum mPredefinedType = IfcFanTypeEnum.NOTDEFINED;// : IfcFanTypeEnum;
		public IfcFanTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFanType() : base() { }
		internal IfcFanType(DatabaseIfc db, IfcFanType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcFanType(DatabaseIfc db, string name, IfcFanTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcFastener : IfcElementComponent
	{
		internal IfcFastenerTypeEnum mPredefinedType = IfcFastenerTypeEnum.NOTDEFINED;// : IfcFastenerTypeEnum; //IFC4
		public IfcFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFastener() : base() { }
		internal IfcFastener(DatabaseIfc db, IfcFastener f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcFastener(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcFastenerType : IfcElementComponentType
	{
		internal IfcFastenerTypeEnum mPredefinedType = IfcFastenerTypeEnum.NOTDEFINED;// : IfcFastenerTypeEnum; //IFC4
		public IfcFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFastenerType() : base() { }
		internal IfcFastenerType(DatabaseIfc db, IfcFastenerType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcFastenerType(DatabaseIfc m, string name, IfcFastenerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcFeatureElement : IfcElement //	ABSTRACT SUPERTYPE OF(ONEOF(IfcFeatureElementAddition, IfcFeatureElementSubtraction, IfcSurfaceFeature))
	{
		protected IfcFeatureElement() : base() { }
		protected IfcFeatureElement(DatabaseIfc db) : base(db) {  }
		protected IfcFeatureElement(DatabaseIfc db, IfcFeatureElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { }
		protected IfcFeatureElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcFeatureElementAddition : IfcFeatureElement //ABSTRACT SUPERTYPE OF(IfcProjectionElement)
	{	//INVERSE
		internal List<IfcRelProjectsElement> mProjectsElements = new List<IfcRelProjectsElement>();
		public ReadOnlyCollection<IfcRelProjectsElement> ProjectsElements { get { return new ReadOnlyCollection<IfcRelProjectsElement>( mProjectsElements); } }

		protected IfcFeatureElementAddition() : base() { }
		protected IfcFeatureElementAddition(DatabaseIfc db, IfcFeatureElementAddition e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream){ }
	}
	[Serializable]
	public abstract partial class IfcFeatureElementSubtraction : IfcFeatureElement //ABSTRACT SUPERTYPE OF (ONEOF (IfcOpeningElement ,IfcVoidingFeature)) 
	{ //INVERSE
		internal IfcRelVoidsElement mVoidsElement = null;
		public IfcRelVoidsElement VoidsElement { get { return mVoidsElement; } }

		protected IfcFeatureElementSubtraction() : base() { }
		protected IfcFeatureElementSubtraction(DatabaseIfc db, IfcFeatureElementSubtraction e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream){ }
		protected IfcFeatureElementSubtraction(DatabaseIfc db) : base(db) {  }
		protected IfcFeatureElementSubtraction(IfcElement host, IfcProductRepresentation rep) : base(host.mDatabase)
		{
			new IfcRelVoidsElement(host, this);
			Representation = rep;
			Placement = new IfcLocalPlacement(host.Placement, mDatabase.Factory.XYPlanePlacement);	
		}
	}
	[Serializable]
	public partial class IfcFillAreaStyle : IfcPresentationStyle
	{
		internal SET<IfcFillStyleSelect> mFillStyles = new SET<IfcFillStyleSelect>();// : SET [1:?] OF IfcFillStyleSelect;
	
		public SET<IfcFillStyleSelect> FillStyles { get { return mFillStyles; } }// : SET [1:?] OF IfcFillStyleSelect;
		internal IfcFillAreaStyle() : base() { }
		//internal IfcFillAreaStyle(IfcFillAreaStyle i) : base(i) { mFillStyles = new List<int>(i.mFillStyles.ToArray()); }
		public IfcFillAreaStyle(IfcFillStyleSelect style) : base(style.Database) { mFillStyles.Add(style); }
		public IfcFillAreaStyle(IEnumerable<IfcFillStyleSelect> styles) : base(styles.First().Database) { mFillStyles.AddRange(styles); }
	}
	[Serializable]
	public partial class IfcFillAreaStyleHatching : IfcGeometricRepresentationItem, IfcFillStyleSelect
	{
		internal int mHatchLineAppearance;// : IfcCurveStyle;
		internal string mStartOfNextHatchLine;// : IfcHatchLineDistanceSelect; IfcOneDirectionRepeatFactor,IfcPositiveLengthMeasure
		internal int mPointOfReferenceHatchLine;// : OPTIONAL IfcCartesianPoint; //DEPRECEATED IFC4
		internal int mPatternStart;// : OPTIONAL IfcCartesianPoint;
		internal double mHatchLineAngle;// : IfcPlaneAngleMeasure;

		public IfcCurveStyle HatchLineAppearance { get { return mDatabase[mHatchLineAppearance] as IfcCurveStyle; } set { mHatchLineAppearance = value.mIndex; } }
		public IfcCartesianPoint PatternStart { get { return mDatabase[mPatternStart] as IfcCartesianPoint; } set { mPatternStart = (value == null ? 0 : value.mIndex); } }

		internal IfcFillAreaStyleHatching() : base() { }
		internal IfcFillAreaStyleHatching(DatabaseIfc db, IfcFillAreaStyleHatching h) : base(db, h)
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
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcFillAreaStyleTileSymbolWithStyle // DEPRECEATED IFC4
	//ENTITY IfcFillAreaStyleTiles : , IfcFillStyleSelect 
	public interface IfcFillStyleSelect : IBaseClassIfc { } // SELECT ( IfcFillAreaStyleHatching, IfcFillAreaStyleTiles, IfcExternallyDefinedHatchStyle, IfcColour);
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcFilter : IfcFlowTreatmentDevice //IFC4  
	{
		internal IfcFilterTypeEnum mPredefinedType = IfcFilterTypeEnum.NOTDEFINED;
		public IfcFilterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFilter() : base() { }
		internal IfcFilter(DatabaseIfc db, IfcFilter f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcFilter(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcFilterType : IfcFlowTreatmentDeviceType
	{
		internal IfcFilterTypeEnum mPredefinedType = IfcFilterTypeEnum.NOTDEFINED;// : IfcFilterTypeEnum;
		public IfcFilterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFilterType() : base() { }
		internal IfcFilterType(DatabaseIfc db, IfcFilterType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcFilterType(DatabaseIfc db, string name, IfcFilterTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcFireSuppressionTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcFireSuppressionTerminalTypeEnum mPredefinedType = IfcFireSuppressionTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcFireSuppressinTerminalTypeEnum;
		public IfcFireSuppressionTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcFireSuppressionTerminal() : base() { }
		internal IfcFireSuppressionTerminal(DatabaseIfc db, IfcFireSuppressionTerminal t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcFireSuppressionTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcFireSuppressionTerminalType : IfcFlowTerminalType
	{
		internal IfcFireSuppressionTerminalTypeEnum mPredefinedType = IfcFireSuppressionTerminalTypeEnum.NOTDEFINED;// : IfcFireSuppressionTerminalTypeEnum;
		public IfcFireSuppressionTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFireSuppressionTerminalType() : base() { }
		internal IfcFireSuppressionTerminalType(DatabaseIfc db, IfcFireSuppressionTerminalType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcFireSuppressionTerminalType(DatabaseIfc m, string name, IfcFireSuppressionTerminalTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcFixedReferenceSweptAreaSolid : IfcSweptAreaSolid //IFC4
	{
		internal int mDirectrix; // : IfcCurve;
		internal double mStartParam = double.NaN;// : OPT IfcParameterValue;  
		internal double mEndParam = double.NaN;//: OPT IfcParameterValue; 
		internal int mFixedReference;// : 	IfcDirection; 

		public IfcCurve Directrix { get { return mDatabase[mDirectrix] as IfcCurve; } set { mDirectrix = value.mIndex; } }
		public IfcDirection FixedReference { get { return mDatabase[mFixedReference] as IfcDirection; } set { mFixedReference = value.mIndex; } }

		internal IfcFixedReferenceSweptAreaSolid() : base() { }
		internal IfcFixedReferenceSweptAreaSolid(DatabaseIfc db, IfcFixedReferenceSweptAreaSolid s) : base(db, s)
		{
			Directrix = db.Factory.Duplicate(s.Directrix) as IfcCurve;
			mStartParam = s.mStartParam;
			mEndParam = s.mEndParam;
			FixedReference = db.Factory.Duplicate(s.FixedReference) as IfcDirection;
		}
		public IfcFixedReferenceSweptAreaSolid(IfcProfileDef sweptArea, IfcCurve directrix, IfcDirection reference) : base(sweptArea)
		{
			Directrix = directrix;
			FixedReference = reference;
		}
	}
	[Serializable]
	public partial class IfcFlowController : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcAirTerminalBox, IfcDamper
	{ //, IfcElectricDistributionBoard, IfcElectricTimeControl, IfcFlowMeter, IfcProtectiveDevice, IfcSwitchingDevice, IfcValve))
		internal override string KeyWord { get { return mDatabase.mRelease < ReleaseVersion.IFC4 && this as IfcElectricDistributionPoint == null ? "IfcFlowController" : base.KeyWord; } }

		internal IfcFlowController() : base() { }
		internal IfcFlowController(DatabaseIfc db, IfcFlowController c, IfcOwnerHistory ownerHistory, bool downStream) : base(db,c, ownerHistory, downStream) { }
		public IfcFlowController(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowControllerType : IfcDistributionFlowElementType
	{
		protected IfcFlowControllerType() : base() { }
		protected IfcFlowControllerType(DatabaseIfc db) : base(db) { }
		protected IfcFlowControllerType(DatabaseIfc db, IfcFlowControllerType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcFlowFitting : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcCableCarrierFitting, IfcCableFitting, IfcDuctFitting, IfcJunctionBox, IfcPipeFitting))
	{
		internal override string KeyWord { get { return mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowFitting" : base.KeyWord; } }

		internal IfcFlowFitting() : base() { }
		internal IfcFlowFitting(DatabaseIfc db, IfcFlowFitting f, IfcOwnerHistory ownerHistory, bool downStream) : base(db,f, ownerHistory, downStream) { }
		public IfcFlowFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowFittingType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcCableCarrierFittingType ,IfcDuctFittingType ,IfcJunctionBoxType ,IfcPipeFittingType))
	{
		protected IfcFlowFittingType() : base() { }
		protected IfcFlowFittingType(DatabaseIfc db) : base(db) { }
		protected IfcFlowFittingType(DatabaseIfc db, IfcFlowFittingType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcFlowInstrument : IfcDistributionControlElement //IFC4  
	{
		internal IfcFlowInstrumentTypeEnum mPredefinedType = IfcFlowInstrumentTypeEnum.NOTDEFINED;
		public IfcFlowInstrumentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowInstrument() : base() { }
		internal IfcFlowInstrument(DatabaseIfc db, IfcFlowInstrument i, IfcOwnerHistory ownerHistory, bool downStream) : base(db, i, ownerHistory, downStream) { mPredefinedType = i.mPredefinedType; }
		public IfcFlowInstrument(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcFlowInstrumentType : IfcDistributionControlElementType
	{
		internal IfcFlowInstrumentTypeEnum mPredefinedType = IfcFlowInstrumentTypeEnum.NOTDEFINED;// : IfcFlowInstrumentTypeEnum;
		public IfcFlowInstrumentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowInstrumentType() : base() { }
		internal IfcFlowInstrumentType(DatabaseIfc db, IfcFlowInstrumentType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcFlowInstrumentType(DatabaseIfc m, string name, IfcFlowInstrumentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcFlowMeter : IfcFlowController //IFC4
	{
		internal IfcFlowMeterTypeEnum mPredefinedType = IfcFlowMeterTypeEnum.NOTDEFINED;// OPTIONAL : IfcDamperTypeEnum;
		public IfcFlowMeterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowMeter() : base() { }
		internal IfcFlowMeter(DatabaseIfc db, IfcFlowMeter m, IfcOwnerHistory ownerHistory, bool downStream) : base(db, m, ownerHistory, downStream) { mPredefinedType = m.mPredefinedType; }
		public IfcFlowMeter(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcFlowMeterType : IfcFlowControllerType
	{
		internal IfcFlowMeterTypeEnum mPredefinedType = IfcFlowMeterTypeEnum.NOTDEFINED;// : IfcFlowMeterTypeEnum;
		public IfcFlowMeterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowMeterType() : base() { }
		internal IfcFlowMeterType(DatabaseIfc db, IfcFlowMeterType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcFlowMeterType(DatabaseIfc m, string name, IfcFlowMeterTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcFlowMovingDevice : IfcDistributionFlowElement //	SUPERTYPE OF(ONEOF(IfcCompressor, IfcFan, IfcPump))
	{
		internal override string KeyWord { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowMovingDevice" : base.KeyWord); } }

		internal IfcFlowMovingDevice() : base() { }
		internal IfcFlowMovingDevice(DatabaseIfc db, IfcFlowMovingDevice d, IfcOwnerHistory ownerHistory, bool downStream) : base(db, d, ownerHistory ,downStream) { }
		public IfcFlowMovingDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowMovingDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF(ONEOF(IfcCompressorType, IfcFanType, IfcPumpType))
	{
		protected IfcFlowMovingDeviceType() : base() { }
		protected IfcFlowMovingDeviceType(DatabaseIfc db) : base(db) { }
		protected IfcFlowMovingDeviceType(DatabaseIfc db, IfcFlowMovingDeviceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcFlowSegment : IfcDistributionFlowElement //	SUPERTYPE OF(ONEOF(IfcCableCarrierSegment, IfcCableSegment, IfcDuctSegment, IfcPipeSegment))
	{
		internal override string KeyWord { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowSegment" : base.KeyWord); } }

		internal IfcFlowSegment() : base() { }
		internal IfcFlowSegment(DatabaseIfc db, IfcFlowSegment s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { }
		public IfcFlowSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowSegmentType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcCableCarrierSegmentType ,IfcCableSegmentType ,IfcDuctSegmentType ,IfcPipeSegmentType))
	{
		protected IfcFlowSegmentType() : base() { }
		protected IfcFlowSegmentType(DatabaseIfc db) : base(db) { }
		protected IfcFlowSegmentType(DatabaseIfc db, IfcFlowSegmentType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcFlowStorageDevice : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcElectricFlowStorageDevice, IfcTank))
	{
		internal override string KeyWord { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowStorageDevice" : base.KeyWord); } }

		internal IfcFlowStorageDevice() : base() { }
		internal IfcFlowStorageDevice(DatabaseIfc db, IfcFlowStorageDevice d, IfcOwnerHistory ownerHistory, bool downStream) : base(db,d, ownerHistory, downStream) { }
		public IfcFlowStorageDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowStorageDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcElectricFlowStorageDeviceType ,IfcTankType))
	{
		protected IfcFlowStorageDeviceType() : base() { }
		protected IfcFlowStorageDeviceType(DatabaseIfc db) : base(db) { }
		protected IfcFlowStorageDeviceType(DatabaseIfc db, IfcFlowStorageDeviceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcFlowTerminal : IfcDistributionFlowElement 	//SUPERTYPE OF(ONEOF(IfcAirTerminal, IfcAudioVisualAppliance, IfcCommunicationsAppliance, IfcElectricAppliance, IfcFireSuppressionTerminal, IfcLamp, IfcLightFixture, IfcMedicalDevice, IfcOutlet, IfcSanitaryTerminal, IfcSpaceHeater, IfcStackTerminal, IfcWasteTerminal))
	{
		internal override string KeyWord { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowTerminal" : base.KeyWord); } }

		internal IfcFlowTerminal() : base() { }
		protected IfcFlowTerminal(IfcFlowTerminal basis) : base(basis) { }
		internal IfcFlowTerminal(DatabaseIfc db, IfcFlowTerminal t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
		public IfcFlowTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowTerminalType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcAirTerminalType ,
	{ // IfcElectricApplianceType ,IfcElectricHeaterType ,IfcFireSuppressionTerminalType,IfcLampType ,IfcLightFixtureType ,IfcOutletType ,IfcSanitaryTerminalType ,IfcStackTerminalType ,IfcWasteTerminalType)) 
		// IFC4 deleted ,IfcGasTerminalType 
		protected IfcFlowTerminalType() : base() { }
		protected IfcFlowTerminalType(IfcDistributionFlowElementType basis) : base(basis) { }
		protected IfcFlowTerminalType(DatabaseIfc db) : base(db) { }
		protected IfcFlowTerminalType(DatabaseIfc db, IfcFlowTerminalType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcFlowTreatmentDevice : IfcDistributionFlowElement // 	SUPERTYPE OF(ONEOF(IfcDuctSilencer, IfcFilter, IfcInterceptor))
	{
		internal override string KeyWord { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowTreatmentDevice" : base.KeyWord); } }

		internal IfcFlowTreatmentDevice() : base() { }
		internal IfcFlowTreatmentDevice(DatabaseIfc db, IfcFlowTreatmentDevice d, IfcOwnerHistory ownerHistory, bool downStream) : base(db, d, ownerHistory, downStream) { }
		public IfcFlowTreatmentDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcFlowTreatmentDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF(ONEOF(IfcDuctSilencerType, IfcFilterType, IfcInterceptorType))
	{
		protected IfcFlowTreatmentDeviceType() : base() { }
		protected IfcFlowTreatmentDeviceType(DatabaseIfc db) : base(db) { }
		protected IfcFlowTreatmentDeviceType(DatabaseIfc db,  IfcFlowTreatmentDeviceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
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
		internal IfcFluidFlowProperties(DatabaseIfc db, IfcFluidFlowProperties p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream)
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
	public partial class IfcFooting : IfcBuildingElement
	{
		internal IfcFootingTypeEnum mPredefinedType = IfcFootingTypeEnum.NOTDEFINED;// OPTIONAL : IfcFootingTypeEnum;
		public IfcFootingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFooting() : base() { }
		internal IfcFooting(DatabaseIfc db, IfcFooting f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcFooting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcFootingType : IfcBuildingElementType
	{
		internal IfcFootingTypeEnum mPredefinedType = IfcFootingTypeEnum.NOTDEFINED;
		public IfcFootingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFootingType() : base() { }
		internal IfcFootingType(DatabaseIfc db, IfcFootingType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db,t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcFootingType(DatabaseIfc m, string name, IfcFootingTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcFootingType(string name, IfcMaterialProfileSet mps, IfcFootingTypeEnum type) : base(mps.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = mps; }
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcFuelProperties
	[Serializable]
	public partial class IfcFurnishingElement : IfcElement // DEPRECEATED IFC4 to make abstract SUPERTYPE OF(ONEOF(IfcFurniture, IfcSystemFurnitureElement))
	{
		internal IfcFurnishingElement() : base() { }
		internal IfcFurnishingElement(DatabaseIfc db, IfcFurnishingElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { }
		internal IfcFurnishingElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcFurnishingElementType : IfcElementType //IFC4 Depreceated //SUPERTYPE OF (ONEOF (IfcFurnitureType ,IfcSystemFurnitureElementType))
	{
		internal IfcFurnishingElementType() : base() { }
		internal IfcFurnishingElementType(DatabaseIfc db, IfcFurnishingElementType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
		public IfcFurnishingElementType(DatabaseIfc db, string name) : base(db) { Name = name; }
	}
	[Serializable]
	public partial class IfcFurniture : IfcFurnishingElement
	{
		internal override string KeyWord { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFurnishingElement" : base.KeyWord); } }
		internal IfcFurnitureTypeEnum mPredefinedType = IfcFurnitureTypeEnum.NOTDEFINED;//: OPTIONAL IfcFurnitureTypeEnum;
		internal IfcFurniture() : base() { }
		internal IfcFurniture(DatabaseIfc db, IfcFurniture f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcFurniture(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcFurnitureStandard : IfcControl 
	{
		internal IfcFurnitureStandard() : base() { }
		internal IfcFurnitureStandard(DatabaseIfc db, IfcFurnitureStandard s, IfcOwnerHistory ownerHistory, bool downStream) : base(db,s, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcFurnitureType : IfcFurnishingElementType
	{
		internal IfcAssemblyPlaceEnum mAssemblyPlace = IfcAssemblyPlaceEnum.NOTDEFINED;
		internal IfcFurnitureTypeEnum mPredefinedType = IfcFurnitureTypeEnum.NOTDEFINED; // IFC4 OPTIONAL
		internal IfcFurnitureType() : base() { }
		internal IfcFurnitureType(DatabaseIfc db, IfcFurnitureType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mAssemblyPlace = t.mAssemblyPlace; mPredefinedType = t.mPredefinedType; }
		public IfcFurnitureType(DatabaseIfc db, string name, IfcFurnitureTypeEnum type) : base(db, name)
		{
			mPredefinedType = type;
			if (mDatabase.mRelease < ReleaseVersion.IFC4 && string.IsNullOrEmpty(ElementType) && type != IfcFurnitureTypeEnum.NOTDEFINED)
				ElementType = type.ToString();
		}
	}
}
