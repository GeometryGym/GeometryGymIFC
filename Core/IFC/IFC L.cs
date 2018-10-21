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
	public partial class IfcLaborResource : IfcConstructionResource
	{
		internal IfcLaborResourceTypeEnum mPredefinedType = IfcLaborResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcLaborResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLaborResource() : base() { }
		internal IfcLaborResource(DatabaseIfc db, IfcLaborResource r, IfcOwnerHistory ownerHistory, bool downStream) : base(db,r, ownerHistory, downStream) { mPredefinedType = r.mPredefinedType; }
		public IfcLaborResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcLaborResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcLaborResourceTypeEnum mPredefinedType = IfcLaborResourceTypeEnum.NOTDEFINED;
		public IfcLaborResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLaborResourceType() : base() { }
		internal IfcLaborResourceType(DatabaseIfc db, IfcLaborResourceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcLaborResourceType(DatabaseIfc db, string name, IfcLaborResourceTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcLagTime : IfcSchedulingTime //IFC4
	{
		internal IfcTimeOrRatioSelect mLagValue;//	IfcTimeOrRatioSelect
		internal IfcTaskDurationEnum mDurationType = IfcTaskDurationEnum.NOTDEFINED;//	IfcTaskDurationEnum; 
		internal IfcLagTime() : base() { }
		//internal IfcLagTime(IfcLagTime i) : base(i) { mLagValue = i.mLagValue; mDurationType = i.mDurationType; }
		internal IfcLagTime(DatabaseIfc db,  IfcTimeOrRatioSelect lag, IfcTaskDurationEnum nature) : base(db) { mLagValue = lag; mDurationType = nature; }
		internal TimeSpan getLag() { return new TimeSpan(0, 0, (int)getSecondsDuration()); }
		internal double getSecondsDuration() { IfcDuration d = mLagValue as IfcDuration; return (d == null ? 0 : d.ToSeconds()); }
	}
	[Serializable]
	public partial class IfcLamp : IfcFlowTerminal //IFC4
	{
		internal IfcLampTypeEnum mPredefinedType = IfcLampTypeEnum.NOTDEFINED;// OPTIONAL : IfcLampTypeEnum;
		public IfcLampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLamp() : base() { }
		internal IfcLamp(DatabaseIfc db, IfcLamp l, IfcOwnerHistory ownerHistory, bool downStream) : base(db, l, ownerHistory, downStream) { mPredefinedType = l.mPredefinedType; }
		public IfcLamp(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcLampType : IfcFlowTerminalType
	{
		internal IfcLampTypeEnum mPredefinedType = IfcLampTypeEnum.NOTDEFINED;// : IfcLampTypeEnum; 
		public IfcLampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLampType() : base() { }
		internal IfcLampType(DatabaseIfc db, IfcLampType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcLampType(DatabaseIfc m, string name, IfcLampTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	public interface IfcLayeredItem : IBaseClassIfc // = SELECT(IfcRepresentationItem, IfcRepresentation);
	{
		SET<IfcPresentationLayerAssignment> LayerAssignments { get; }
	}
	[Serializable]
	public partial class IfcLibraryInformation : IfcExternalInformation, NamedObjectIfc
	{
		internal string mName;// :	IfcLabel;
		internal string mVersion = "$";//:	OPTIONAL IfcLabel;
		internal int mPublisher;//	 :	OPTIONAL IfcActorSelect;
		internal string mVersionDate = "$"; // :	OPTIONAL IfcDateTime;
		internal int mVersionDateSS = 0; // 
		internal string mLocation = "$";//	 :	OPTIONAL IfcURIReference; //IFC4 Added
		internal string mDescription = "$";//	 :	OPTIONAL IfcText; //IFC4 Added
		internal List<int> mLibraryReference = new List<int>();// IFC2x3 : 	OPTIONAL SET[1:?] OF IfcLibraryReference;
		//INVERSE
		internal List<IfcRelAssociatesLibrary> mLibraryRefForObjects = new List<IfcRelAssociatesLibrary>();//IFC4 :	SET [0:?] OF IfcRelAssociatesLibrary FOR RelatingLibrary;
		internal List<IfcLibraryReference> mHasLibraryReferences = new List<IfcLibraryReference>();//	:	SET OF IfcLibraryReference FOR ReferencedLibrary;

		public string Name { get { return ParserIfc.Decode(mName); } set { mName = (string.IsNullOrEmpty(value) ? "UNKNOWN" : ParserIfc.Encode(value)); } } 
		public string Version { get { return (mVersion == "$" ? "" : ParserIfc.Decode(mVersion)); } set { mVersion = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcActorSelect Publisher { get { return mDatabase[mPublisher] as IfcActorSelect; } set { mPublisher = (value == null ? 0 : value.Index); } }
		public string Location { get { return (mLocation == "$" ? "" : ParserIfc.Decode(mLocation)); } set { mLocation = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcLibraryInformation() : base() { }
		internal IfcLibraryInformation(DatabaseIfc db, IfcLibraryInformation i) : base(db,i) { mName = i.mName; mVersion = i.mVersion; if(i.mPublisher > 0) Publisher = db.Factory.Duplicate(i.mDatabase[ i.mPublisher]) as IfcActorSelect; mVersionDate = i.mVersionDate; mLocation = i.mLocation; mDescription = i.mDescription; }
		public IfcLibraryInformation(DatabaseIfc db, string name) : base(db) { Name = name; }
	}
	[Serializable]
	public partial class IfcLibraryReference : IfcExternalReference, IfcLibrarySelect
	{
		internal string mDescription = ""; //IFC4	 :	OPTIONAL IfcText;
		internal string mLanguage = "$"; //IFC4	 :	OPTIONAL IfcLanguageId;
		internal int mReferencedLibrary; //	 :	OPTIONAL IfcLibraryInformation; ifc2x3 INVERSE ReferenceIntoLibrary
		//INVERSE
		internal List<IfcRelAssociatesLibrary> mLibraryRefForObjects = new List<IfcRelAssociatesLibrary>();//IFC4 :	SET [0:?] OF IfcRelAssociatesLibrary FOR RelatingLibrary;

		public string Description { get { return mDescription; } set { mDescription = value; } }
		public string Language { get { return (mLanguage == "$" ? "" : ParserIfc.Decode(mLanguage)); } set { mLanguage = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcLibraryInformation ReferencedLibrary { get { return mDatabase[mReferencedLibrary] as IfcLibraryInformation; } set { mReferencedLibrary = (value == null ? 0 : value.mIndex); if (value != null && !value.mHasLibraryReferences.Contains(this)) value.mHasLibraryReferences.Add(this); } }

		internal IfcLibraryReference() : base() { }
		internal IfcLibraryReference(DatabaseIfc db, IfcLibraryReference r) : base(db,r) { mDescription = r.mDescription; mLanguage = r.mLanguage; ReferencedLibrary = db.Factory.Duplicate(r.ReferencedLibrary) as IfcLibraryInformation; }
		public IfcLibraryReference(DatabaseIfc db) : base(db) { }
		public IfcLibraryReference(IfcLibraryInformation referenced) : base(referenced.mDatabase) { ReferencedLibrary = referenced; }
	}
	public interface IfcLibrarySelect : NamedObjectIfc //SELECT ( IfcLibraryReference,  IfcLibraryInformation);
	{
		//IfcRelAssociatesLibrary Associates { get; }
		//string Name { get; }
	}
	//ENTITY IfcLightDistributionData;
	public interface IfcLightDistributionDataSourceSelect : IBaseClassIfc { } //SELECT(IfcExternalReference,IfcLightIntensityDistribution);
	[Serializable]
	public partial class IfcLightFixture : IfcFlowTerminal
	{
		internal IfcLightFixtureTypeEnum mPredefinedType = IfcLightFixtureTypeEnum.NOTDEFINED;// : OPTIONAL IfcLightFixtureTypeEnum; 
		public IfcLightFixtureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLightFixture() : base() { }
		internal IfcLightFixture(DatabaseIfc db, IfcLightFixture f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcLightFixture(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcLightFixtureType : IfcFlowTerminalType
	{
		internal IfcLightFixtureTypeEnum mPredefinedType = IfcLightFixtureTypeEnum.NOTDEFINED;// : IfcLightFixtureTypeEnum; 
		public IfcLightFixtureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLightFixtureType() : base() { }
		internal IfcLightFixtureType(DatabaseIfc db, IfcLightFixtureType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcLightFixtureType(DatabaseIfc m, string name, IfcLightFixtureTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	//ENTITY IfcLightIntensityDistribution ,IfcLightDistributionDataSourceSelect
	[Serializable]
	public abstract partial class IfcLightSource : IfcGeometricRepresentationItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcLightSourceAmbient ,IfcLightSourceDirectional ,IfcLightSourceGoniometric ,IfcLightSourcePositional))
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal int mLightColour;// : IfcColourRgb;
		internal double mAmbientIntensity;// : OPTIONAL IfcNormalisedRatioMeasure;
		internal double mIntensity;// : OPTIONAL IfcNormalisedRatioMeasure; 
		protected IfcLightSource() : base() { }
		protected IfcLightSource(DatabaseIfc db, IfcLightSource l) : base(db,l) { mName = l.mName; mLightColour = l.mLightColour; mAmbientIntensity = l.mAmbientIntensity; mIntensity = l.mIntensity; }
	}
	[Serializable]
	public partial class IfcLightSourceAmbient : IfcLightSource
	{
		internal IfcLightSourceAmbient() : base() { }
		//internal IfcLightSourceAmbient(IfcLightSourceAmbient el) : base((IfcLightSourceAmbient)el) { }
	}
	[Serializable]
	public partial class IfcLightSourceDirectional : IfcLightSource
	{
		internal int mOrientation;// : IfcDirection; 
		internal IfcLightSourceDirectional() : base() { }
		//internal IfcLightSourceDirectional(IfcLightSourceDirectional el) : base((IfcLightSource)el) { mOrientation = el.mOrientation; }
	}
	[Serializable]
	public partial class IfcLightSourceGoniometric : IfcLightSource
	{
		internal int mPosition;// : IfcAxis2Placement3D;
		internal int mColourAppearance;// : OPTIONAL IfcColourRgb;
		internal double mColourTemperature;// : IfcReal;
		internal double mLuminousFlux;// : IfcLuminousFluxMeasure;
		internal IfcLightEmissionSourceEnum mLightEmissionSource;// : IfcLightEmissionSourceEnum;
		internal int mLightDistributionDataSource; // IfcLightDistributionDataSourceSelect; 
		internal IfcLightSourceGoniometric() : base() { }
		//internal IfcLightSourceGoniometric(DatabaseIfc db, IfcLightSourceGoniometric el)
		//	: base(el)
		//{
		//	mPosition = el.mPosition;
		//	mColourAppearance = el.mColourAppearance;
		//	mColourTemperature = el.mColourTemperature;
		//	mLuminousFlux = el.mLuminousFlux;
		//	mLightEmissionSource = el.mLightEmissionSource;
		//	mLightDistributionDataSource = el.mLightDistributionDataSource;
		//}	
	}
	[Serializable]
	public partial class IfcLightSourcePositional : IfcLightSource
	{
		internal int mPosition;// : IfcCartesianPoint;
		internal double mRadius;// : IfcPositiveLengthMeasure;
		internal double mConstantAttenuation;// : IfcReal;
		internal double mDistanceAttenuation;// : IfcReal;
		internal double mQuadricAttenuation;// : IfcReal; 
		internal IfcLightSourcePositional() : base() { }
		//internal IfcLightSourcePositional(IfcLightSourcePositional el)
		//	: base((IfcLightSource)el)
		//{
		//	mPosition = el.mPosition;
		//	mRadius = el.mRadius;
		//	mConstantAttenuation = el.mConstantAttenuation;
		//	mDistanceAttenuation = el.mDistanceAttenuation;
		//	mQuadricAttenuation = el.mQuadricAttenuation;
		//}
	}
	[Serializable]
	public partial class IfcLightSourceSpot : IfcLightSource
	{
		internal int mOrientation;// : IfcDirection;
		internal double mConcentrationExponent;// :  IfcReal;
		internal double mSpreadAngle;// : IfcPositivePlaneAngleMeasure;
		internal double mBeamWidthAngle;// : IfcPositivePlaneAngleMeasure; 
		internal IfcLightSourceSpot() : base() { }
		//internal IfcLightSourceSpot(IfcLightSourceSpot el) : base(el) { mOrientation = el.mOrientation; mConcentrationExponent = el.mConcentrationExponent; mSpreadAngle = el.mSpreadAngle; mBeamWidthAngle = el.mBeamWidthAngle; }
	}
	[Serializable]
	public partial class IfcLine : IfcCurve
	{
		internal int mPnt;// : IfcCartesianPoint;
		internal int mDir;// : IfcVector; 

		public IfcCartesianPoint Pnt { get { return mDatabase[mPnt] as IfcCartesianPoint; } set { mPnt = value.mIndex; } }
		public IfcVector Dir { get { return mDatabase[mDir] as IfcVector; } set { mDir = value.mIndex; } }

		internal IfcLine() : base() { }
		internal IfcLine(DatabaseIfc db, IfcLine l) : base(db, l) { Pnt = db.Factory.Duplicate(l.Pnt) as IfcCartesianPoint; Dir = db.Factory.Duplicate(l.Dir) as IfcVector; }
		public IfcLine(IfcCartesianPoint point, IfcVector dir) : base(point.mDatabase) { Pnt = point; Dir = dir; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcLinearDimension : IfcDimensionCurveDirectedCallout // DEPRECEATED IFC4
	{
		internal IfcLinearDimension() : base() { }
		//internal IfcLinearDimension(IfcAngularDimension el) : base((IfcDimensionCurveDirectedCallout)el) { }
	}
	[Serializable]
	public abstract partial class IfcLinearPositioningElement : IfcPositioningElement //IFC4.1
	{
		private IfcCurve mAxis;// : IfcCurve;

		public IfcCurve Axis { get { return mAxis; } set { mAxis = value; } }

		protected IfcLinearPositioningElement() : base() { }
		protected IfcLinearPositioningElement(IfcSite host, IfcCurve axis) : base(host) { Axis = axis; }
		protected IfcLinearPositioningElement(DatabaseIfc db, IfcLinearPositioningElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { Axis = db.Factory.Duplicate(e.Axis) as IfcCurve; }
	}
	[Serializable]
	public partial class IfcLineIndex : IfcSegmentIndexSelect
	{
		internal List<int> mIndices = new List<int>();
		public IfcLineIndex(int a, int b) { mIndices.Add(a); mIndices.Add(b); }
		public IfcLineIndex(IEnumerable<int> indices) { mIndices.AddRange(indices); }
		public override string ToString()
		{
			string indices = "";
			for (int icounter = 1; icounter < mIndices.Count; icounter++)
				indices += "," + mIndices[icounter];
			return "IFCLINEINDEX((" + mIndices[0] + indices + "))";
		}
	}
	[Serializable]
	public partial class IfcLineSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		internal IfcLineSegment2D() : base() { }
		internal IfcLineSegment2D(DatabaseIfc db, IfcLineSegment2D s) : base(db, s) { }
		internal IfcLineSegment2D(IfcCartesianPoint start, double startDirection, double length)
			: base(start, startDirection, length) { }
	}
	[Serializable]
	public partial class IfcLocalPlacement : IfcObjectPlacement
	{
		private IfcObjectPlacement mPlacementRelTo = null;// : OPTIONAL IfcObjectPlacement;
		private IfcAxis2Placement mRelativePlacement = null;// : IfcAxis2Placement;

		private bool mCalculated = false;

		public IfcObjectPlacement PlacementRelTo
		{
			get { return mPlacementRelTo; }
			set
			{
				if (mPlacementRelTo != null)
					mPlacementRelTo.mReferencedByPlacements.Remove(this);
				mPlacementRelTo = value;
				if (value != null)
					value.mReferencedByPlacements.Add(this);
			}
		}
		public IfcAxis2Placement RelativePlacement
		{
			get { return mRelativePlacement; }
			set { mRelativePlacement = value; mCalculated = false; }
		}

		internal IfcLocalPlacement() : base() { }
		internal IfcLocalPlacement(DatabaseIfc db, IfcLocalPlacement p) : base(db, p)
		{
			if (p.mPlacementRelTo  != null)
				PlacementRelTo = db.Factory.Duplicate(p.PlacementRelTo) as IfcObjectPlacement;
			RelativePlacement = db.Factory.Duplicate(p.mDatabase[p.mRelativePlacement.Index]) as IfcAxis2Placement;
		}
		public IfcLocalPlacement(IfcAxis2Placement placement) : base(placement.Database) { RelativePlacement = placement; }
		public IfcLocalPlacement(IfcObjectPlacement relativeTo, IfcAxis2Placement placement) : this(placement)
		
		{
			if (relativeTo != null)
				PlacementRelTo = relativeTo;
		}
		
		internal override bool isXYPlane
		{
			get
			{
				IfcLocalPlacement placement = PlacementRelTo as IfcLocalPlacement;
				if (RelativePlacement.IsXYPlane && (placement == null || placement.isXYPlane))
					return true;
				return false;
			}
		}
		//internal override bool isWorldXY
		//{
		//	get
		//	{
		//		base.isWorldXY
		//		if (mIndex == mDatabase.Factory.WorldCoordinatePlacement.mIndex)
		//			return true;
		//		IfcLocalPlacement placement = PlacementRelTo as IfcLocalPlacement;
		//		return RelativePlacement.IsXYPlane && (placement != null && placement.isWorldXY);
		//	}
		//}
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcLocalTime : BaseClassIfc, IfcDateTimeSelect // DEPRECEATED IFC4
	{
		internal int mHourComponent;// : IfcHourInDay;
		internal int mMinuteComponent;// : OPTIONAL IfcMinuteInHour;
		internal double mSecondComponent;// : OPTIONAL IfcSecondInMinute;
		internal int mZone;// OPTIONAL IfcCoordinatedUniversalTimeOffset;
		internal int mDaylightSavingOffset;// : OPTIONAL IfcDaylightSavingHour; 

		public IfcCoordinatedUniversalTimeOffset Zone { get { return mDatabase[mZone] as IfcCoordinatedUniversalTimeOffset; } set { mZone = (value == null ? 0 : value.mIndex); } }
		public int DaylightSavingOffset { get { return mDaylightSavingOffset; } set { mDaylightSavingOffset = value; } }
		internal IfcLocalTime() : base() { }
		internal IfcLocalTime(DatabaseIfc db, IfcLocalTime t) : base(db,t)
		{
			mHourComponent = t.mHourComponent;
			mMinuteComponent = t.mMinuteComponent;
			mSecondComponent = t.mSecondComponent;
			mZone = t.mZone;
			mDaylightSavingOffset = t.mDaylightSavingOffset;
		}
		internal IfcLocalTime(DatabaseIfc m, int hour, int min, int sec) : base(m) { mHourComponent = hour; mMinuteComponent = min; mSecondComponent = sec; }
		public DateTime DateTime
		{
			get
			{
				return new DateTime(0, 0, 0, mHourComponent, mMinuteComponent, (int)mSecondComponent);
			}
		}
	}
	[Serializable]
	public abstract partial class IfcLoop : IfcTopologicalRepresentationItem /*SUPERTYPE OF (ONEOF (IfcEdgeLoop ,IfcPolyLoop ,IfcVertexLoop))*/
	{ 
		protected IfcLoop() : base() { }
		protected IfcLoop(DatabaseIfc db) : base(db) { }
		protected IfcLoop(DatabaseIfc db, IfcLoop l) : base(db,l) { }
	}
	[Serializable]
	public partial class IfcLShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mDepth, mWidth, mThickness;// : IfcPositiveLengthMeasure;
		internal double mFilletRadius = double.NaN, mEdgeRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mLegSlope = double.NaN;// : OPTIONAL IfcPlaneAngleMeasure;
		internal double mCentreOfGravityInX = double.NaN, mCentreOfGravityInY = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure 

		public double Depth { get { return mDepth; } set { mDepth = value; } }
		public double Width { get { return mWidth; } set { mWidth = value; } }
		public double Thickness { get { return mThickness; } set { mThickness = value; } }
		public double FilletRadius { get { return mFilletRadius; } set { mFilletRadius = value; } }
		public double EdgeRadius { get { return mEdgeRadius; } set { mEdgeRadius = value; } }
		public double LegSlope { get { return mLegSlope; } set { mLegSlope = value; } }
		public double CentreOfGravityInX { get { return mCentreOfGravityInX; } set { mCentreOfGravityInX = value; } }
		public double CentreOfGravityInY { get { return mCentreOfGravityInY; } set { mCentreOfGravityInY = value; } }

		internal IfcLShapeProfileDef() : base() { }
		internal IfcLShapeProfileDef(DatabaseIfc db, IfcLShapeProfileDef p) : base(db, p)
		{
			mDepth = p.mDepth;
			mWidth = p.mWidth;
			mThickness = p.mThickness;
			mFilletRadius = p.mFilletRadius;
			mEdgeRadius = p.mEdgeRadius;
			mLegSlope = p.mLegSlope;
			mCentreOfGravityInX = p.mCentreOfGravityInX;
			mCentreOfGravityInY = p.mCentreOfGravityInY;
		}
		public IfcLShapeProfileDef(DatabaseIfc db, string name, double depth, double width, double thickness)
			: base(db,name)
		{
			mDepth = depth;
			mWidth = width;
			mThickness = thickness;
		}
	}
}
