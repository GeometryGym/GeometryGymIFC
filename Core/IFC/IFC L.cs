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

using GeometryGym.STEP;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace GeometryGym.Ifc
{
	[Serializable]
	public partial class IfcLaborResource : IfcConstructionResource
	{
		internal IfcLaborResourceTypeEnum mPredefinedType = IfcLaborResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcLaborResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLaborResource() : base() { }
		internal IfcLaborResource(DatabaseIfc db, IfcLaborResource r, DuplicateOptions options) : base(db, r, options) { mPredefinedType = r.mPredefinedType; }
		public IfcLaborResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcLaborResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcLaborResourceTypeEnum mPredefinedType = IfcLaborResourceTypeEnum.NOTDEFINED;
		public IfcLaborResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLaborResourceType() : base() { }
		internal IfcLaborResourceType(DatabaseIfc db, IfcLaborResourceType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcLaborResourceType(DatabaseIfc db, string name, IfcLaborResourceTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcLagTime : IfcSchedulingTime //IFC4
	{
		internal IfcTimeOrRatioSelect mLagValue;//	IfcTimeOrRatioSelect
		internal IfcTaskDurationEnum mDurationType = IfcTaskDurationEnum.NOTDEFINED;//	IfcTaskDurationEnum; 
		internal IfcLagTime() : base() { }
		//internal IfcLagTime(IfcLagTime i) : base(i) { mLagValue = i.mLagValue; mDurationType = i.mDurationType; }
		internal IfcLagTime(DatabaseIfc db, IfcTimeOrRatioSelect lag, IfcTaskDurationEnum nature) : base(db) { mLagValue = lag; mDurationType = nature; }
		internal TimeSpan getLag() { return new TimeSpan(0, 0, (int)getSecondsDuration()); }
		internal double getSecondsDuration() { IfcDuration d = mLagValue as IfcDuration; return (d == null ? 0 : d.ToSeconds()); }
	}
	[Serializable]
	public partial class IfcLamp : IfcFlowTerminal //IFC4
	{
		internal IfcLampTypeEnum mPredefinedType = IfcLampTypeEnum.NOTDEFINED;// OPTIONAL : IfcLampTypeEnum;
		public IfcLampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLamp() : base() { }
		internal IfcLamp(DatabaseIfc db, IfcLamp l, DuplicateOptions options) : base(db, l, options) { mPredefinedType = l.mPredefinedType; }
		public IfcLamp(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcLampType : IfcFlowTerminalType
	{
		internal IfcLampTypeEnum mPredefinedType = IfcLampTypeEnum.NOTDEFINED;// : IfcLampTypeEnum; 
		public IfcLampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLampType() : base() { }
		internal IfcLampType(DatabaseIfc db, IfcLampType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcLampType(DatabaseIfc m, string name, IfcLampTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	public interface IfcLayeredItem : IBaseClassIfc // SELECT(IfcRepresentationItem, IfcRepresentation);
	{
		IfcPresentationLayerAssignment LayerAssignment { get; set; }
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
		private SET<IfcLibraryReference> mLibraryReference = new SET<IfcLibraryReference>();// IFC2x3 : 	OPTIONAL SET[1:?] OF IfcLibraryReference;
																							//INVERSE
		internal SET<IfcRelAssociatesLibrary> mLibraryRefForObjects = new SET<IfcRelAssociatesLibrary>();//IFC4 :	SET [0:?] OF IfcRelAssociatesLibrary FOR RelatingLibrary;
		internal SET<IfcLibraryReference> mHasLibraryReferences = new SET<IfcLibraryReference>();//	:	SET OF IfcLibraryReference FOR ReferencedLibrary;

		public string Name { get { return ParserIfc.Decode(mName); } set { mName = (string.IsNullOrEmpty(value) ? "UNKNOWN" : ParserIfc.Encode(value)); } }
		public string Version { get { return (mVersion == "$" ? "" : ParserIfc.Decode(mVersion)); } set { mVersion = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcActorSelect Publisher { get { return mDatabase[mPublisher] as IfcActorSelect; } set { mPublisher = (value == null ? 0 : value.Index); } }
		public string Location { get { return (mLocation == "$" ? "" : ParserIfc.Decode(mLocation)); } set { mLocation = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcLibraryInformation() : base() { }
		internal IfcLibraryInformation(DatabaseIfc db, IfcLibraryInformation i) : base(db, i) { mName = i.mName; mVersion = i.mVersion; if (i.mPublisher > 0) Publisher = db.Factory.Duplicate(i.mDatabase[i.mPublisher]) as IfcActorSelect; mVersionDate = i.mVersionDate; mLocation = i.mLocation; mDescription = i.mDescription; }
		public IfcLibraryInformation(DatabaseIfc db, string name) : base(db) { Name = name; }

		protected override void initialize()
		{
			base.initialize();

			mLibraryReference.CollectionChanged += mLibraryReference_CollectionChanged;
		}
		private void mLibraryReference_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcLibraryReference libraryReference in e.NewItems)
					libraryReference.ReferencedLibrary = this;
			}
			if (e.OldItems != null)
			{
				foreach (IfcLibraryReference libraryReference in e.NewItems)
				{
					if (libraryReference.ReferencedLibrary == this)
						libraryReference.ReferencedLibrary = this;
				}
			}
		}
	}
	[Serializable]
	public partial class IfcLibraryReference : IfcExternalReference, IfcLibrarySelect
	{
		internal string mDescription = ""; //IFC4	 :	OPTIONAL IfcText;
		internal string mLanguage = "$"; //IFC4	 :	OPTIONAL IfcLanguageId;
		internal IfcLibraryInformation mReferencedLibrary; //	 :	OPTIONAL IfcLibraryInformation; ifc2x3 INVERSE ReferenceIntoLibrary
														   //INVERSE
		internal List<IfcRelAssociatesLibrary> mLibraryRefForObjects = new List<IfcRelAssociatesLibrary>();//IFC4 :	SET [0:?] OF IfcRelAssociatesLibrary FOR RelatingLibrary;

		public string Description { get { return mDescription; } set { mDescription = value; } }
		public string Language { get { return (mLanguage == "$" ? "" : ParserIfc.Decode(mLanguage)); } set { mLanguage = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcLibraryInformation ReferencedLibrary { get { return mReferencedLibrary; } set { mReferencedLibrary = value; if (value != null) value.mHasLibraryReferences.Add(this); } }

		internal IfcLibraryReference() : base() { }
		internal IfcLibraryReference(DatabaseIfc db, IfcLibraryReference r) : base(db, r) { mDescription = r.mDescription; mLanguage = r.mLanguage; ReferencedLibrary = db.Factory.Duplicate(r.ReferencedLibrary) as IfcLibraryInformation; }
		public IfcLibraryReference(DatabaseIfc db) : base(db) { }
		public IfcLibraryReference(IfcLibraryInformation referenced) : base(referenced.mDatabase) { ReferencedLibrary = referenced; }
	}
	public interface IfcLibrarySelect : NamedObjectIfc //SELECT ( IfcLibraryReference,  IfcLibraryInformation);
	{
		//IfcRelAssociatesLibrary Associates { get; }
		//string Name { get; }
	}
	[Serializable]
	public partial class IfcLightDistributionData : BaseClassIfc
	{
		private double mMainPlaneAngle = 0; //: IfcPlaneAngleMeasure;
		private LIST<double> mSecondaryPlaneAngle = new LIST<double>(); //: LIST[1:?] OF IfcPlaneAngleMeasure;
		private LIST<double> mLuminousIntensity = new LIST<double>(); //: LIST[1:?] OF IfcLuminousIntensityDistributionMeasure;

		public double MainPlaneAngle { get { return mMainPlaneAngle; } set { mMainPlaneAngle = value; } }
		public LIST<double> SecondaryPlaneAngle { get { return mSecondaryPlaneAngle; } set { mSecondaryPlaneAngle = value; } }
		public LIST<double> LuminousIntensity { get { return mLuminousIntensity; } set { mLuminousIntensity = value; } }

		public IfcLightDistributionData() : base() { }
		public IfcLightDistributionData(DatabaseIfc db, double mainPlaneAngle, IEnumerable<double> secondaryPlaneAngle, IEnumerable<double> luminousIntensity)
			: base(db)
		{
			MainPlaneAngle = mainPlaneAngle;
			SecondaryPlaneAngle.AddRange(secondaryPlaneAngle);
			LuminousIntensity.AddRange(luminousIntensity);
		}
	}
	public interface IfcLightDistributionDataSourceSelect : IBaseClassIfc { } //SELECT(IfcExternalReference,IfcLightIntensityDistribution);
	[Serializable]
	public partial class IfcLightFixture : IfcFlowTerminal
	{
		internal IfcLightFixtureTypeEnum mPredefinedType = IfcLightFixtureTypeEnum.NOTDEFINED;// : OPTIONAL IfcLightFixtureTypeEnum; 
		public IfcLightFixtureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLightFixture() : base() { }
		internal IfcLightFixture(DatabaseIfc db, IfcLightFixture f, DuplicateOptions options) : base(db, f, options) { mPredefinedType = f.mPredefinedType; }
		public IfcLightFixture(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcLightFixtureType : IfcFlowTerminalType
	{
		internal IfcLightFixtureTypeEnum mPredefinedType = IfcLightFixtureTypeEnum.NOTDEFINED;// : IfcLightFixtureTypeEnum; 
		public IfcLightFixtureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLightFixtureType() : base() { }
		internal IfcLightFixtureType(DatabaseIfc db, IfcLightFixtureType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcLightFixtureType(DatabaseIfc m, string name, IfcLightFixtureTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcLightIntensityDistribution : BaseClassIfc, IfcLightDistributionDataSourceSelect
	{
		private IfcLightDistributionCurveEnum mLightDistributionCurve = IfcLightDistributionCurveEnum.NOTDEFINED; //: IfcLightDistributionCurveEnum;
		private LIST<IfcLightDistributionData> mDistributionData = new LIST<IfcLightDistributionData>(); //: LIST[1:?] OF IfcLightDistributionData;

		public IfcLightDistributionCurveEnum LightDistributionCurve { get { return mLightDistributionCurve; } set { mLightDistributionCurve = value; } }
		public LIST<IfcLightDistributionData> DistributionData { get { return mDistributionData; } set { mDistributionData = value; } }

		public IfcLightIntensityDistribution() : base() { }
		public IfcLightIntensityDistribution(IfcLightDistributionCurveEnum lightDistributionCurve, IEnumerable<IfcLightDistributionData> distributionData)
			: base(distributionData.First().Database)
		{
			LightDistributionCurve = lightDistributionCurve;
			DistributionData.AddRange(distributionData);
		}
	}
	[Serializable]
	public abstract partial class IfcLightSource : IfcGeometricRepresentationItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcLightSourceAmbient ,IfcLightSourceDirectional ,IfcLightSourceGoniometric ,IfcLightSourcePositional))
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal int mLightColour;// : IfcColourRgb;
		internal double mAmbientIntensity;// : OPTIONAL IfcNormalisedRatioMeasure;
		internal double mIntensity;// : OPTIONAL IfcNormalisedRatioMeasure; 
		protected IfcLightSource() : base() { }
		protected IfcLightSource(DatabaseIfc db, IfcLightSource l, DuplicateOptions options) : base(db, l, options) { mName = l.mName; mLightColour = l.mLightColour; mAmbientIntensity = l.mAmbientIntensity; mIntensity = l.mIntensity; }
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
	public partial class IfcLightSourceSpot : IfcLightSourcePositional
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
		internal IfcLine(DatabaseIfc db, IfcLine l, DuplicateOptions options) : base(db, l, options) { Pnt = db.Factory.Duplicate(l.Pnt) as IfcCartesianPoint; Dir = db.Factory.Duplicate(l.Dir) as IfcVector; }
		public IfcLine(IfcCartesianPoint point, IfcVector dir) : base(point.mDatabase) { Pnt = point; Dir = dir; }
	}
	[Obsolete("DEPRECATED IFC4X3", false)]
	public partial interface IfcLinearAxisSelect : IBaseClassIfc { } // SELECT(IfcLinearAxisWithInclination, IfcCurve);
	[Serializable]
	public partial class IfcLinearAxisWithInclination : IfcGeometricRepresentationItem, IfcLinearAxisSelect
	{
		private IfcCurve mDirectrix = null; //: IfcCurve;
		private IfcAxisLateralInclination mInclinating = null; //: IfcAxisLateralInclination;

		public IfcCurve Directrix { get { return mDirectrix; } set { mDirectrix = value; } }
		public IfcAxisLateralInclination Inclinating { get { return mInclinating; } set { mInclinating = value; } }

		public IfcLinearAxisWithInclination() : base() { }
		public IfcLinearAxisWithInclination(DatabaseIfc db, IfcCurve directrix, IfcAxisLateralInclination inclinating)
			: base(db)
		{
			Directrix = directrix;
			Inclinating = inclinating;
		}


	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcLinearDimension : IfcDimensionCurveDirectedCallout // DEPRECATED IFC4
	{
		internal IfcLinearDimension() : base() { }
		//internal IfcLinearDimension(IfcAngularDimension el) : base((IfcDimensionCurveDirectedCallout)el) { }
	}
	[Serializable]
	public abstract partial class IfcLinearElement : IfcProduct
	{
		protected IfcLinearElement() : base() { }
		protected IfcLinearElement(DatabaseIfc db) : base(db) { }
		protected IfcLinearElement(DatabaseIfc db, IfcLinearElement linearElement, DuplicateOptions options)
		: base(db, linearElement, options) { }
		protected IfcLinearElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	public partial class IfcLinearPlacement : IfcObjectPlacement
	{
		[Obsolete("DEPRECATED IFC4x3", false)]
		private IfcCurve mPlacementMeasuredAlong = null; //: IfcCurve;
		[Obsolete("DEPRECATED IFC4x3", false)]
		private IfcPointByDistanceExpression mDistance = null; //: IfcDistanceExpression;
		[Obsolete("DEPRECATED IFC4x3", false)]
		private IfcOrientationExpression mOrientation = null; //: OPTIONAL IfcOrientationExpression;
		private IfcAxis2PlacementLinear mRelativePlacement; //: IfcLinearAxis2Placement;
		private IfcAxis2Placement3D mCartesianPosition = null; //: OPTIONAL IfcAxis2Placement3D;

		[Obsolete("DEPRECATED IFC4x3", false)]
		public IfcCurve PlacementMeasuredAlong { get { return mPlacementMeasuredAlong; } set { mPlacementMeasuredAlong = value; } }
		[Obsolete("DEPRECATED IFC4x3", false)]
		public IfcPointByDistanceExpression Distance { get { return mDistance; } set { mDistance = value; } }
		[Obsolete("DEPRECATED IFC4x3", false)]
		public IfcOrientationExpression Orientation { get { return mOrientation; } set { mOrientation = value; } }
		public IfcAxis2PlacementLinear RelativePlacement { get { return mRelativePlacement; } set { mRelativePlacement = value; } }
		public IfcAxis2Placement3D CartesianPosition { get { return mCartesianPosition; } set { mCartesianPosition = value; } }

		public IfcLinearPlacement() : base() { }
		internal IfcLinearPlacement(DatabaseIfc db, IfcLinearPlacement linearPlacement) : base(db, linearPlacement)
		{
			PlacementMeasuredAlong = db.Factory.Duplicate(linearPlacement.PlacementMeasuredAlong) as IfcCurve;
			Distance = db.Factory.Duplicate(linearPlacement.Distance) as IfcPointByDistanceExpression;
			if (linearPlacement.Orientation != null)
				Orientation = db.Factory.Duplicate(linearPlacement.Orientation) as IfcOrientationExpression;
			if (linearPlacement.mRelativePlacement != null)
				RelativePlacement = db.Factory.Duplicate(linearPlacement.RelativePlacement) as IfcAxis2PlacementLinear;
			if (linearPlacement.CartesianPosition != null)
				CartesianPosition = db.Factory.Duplicate(linearPlacement.CartesianPosition) as IfcAxis2Placement3D;
		}
		public IfcLinearPlacement(IfcAxis2PlacementLinear p) : base(p.Database) { RelativePlacement = p; }
		public IfcLinearPlacement(IfcObjectPlacement refPlacement, IfcAxis2PlacementLinear p) : base(refPlacement) { RelativePlacement = p; }
		[Obsolete("DEPRECATED IFC4x3", false)]
		public IfcLinearPlacement(IfcCurve placementMeasuredAlong, IfcPointByDistanceExpression distance)
			: base(placementMeasuredAlong.Database)
		{
			PlacementMeasuredAlong = placementMeasuredAlong;
			Distance = distance;
		}
	}
	[Serializable]
	public partial class IfcLinearPositioningElement : IfcPositioningElement //IFC4.1
	{
		[Obsolete("DEPRECATED IFC4X3", false)]
		private IfcLinearAxisSelect mAxis;// : IfcCurve;
		[Obsolete("DEPRECATED IFC4X3", false)]
		public IfcCurve Axis
		{
			get { return mAxis as IfcCurve; }
			set
			{
				mAxis = value;
				IfcBoundedCurve boundedCurve = value as IfcBoundedCurve;
				if (boundedCurve != null)
					boundedCurve.mPositioningElement = this;
			}
		}

		protected IfcLinearPositioningElement() : base() { }
		protected IfcLinearPositioningElement(IfcSpatialStructureElement host) : base(host) { }
		[Obsolete("DEPRECATED IFC4X3", false)]
		public IfcLinearPositioningElement(IfcSpatialStructureElement host, IfcCurve axis) : base(host) { Axis = axis; }
		protected IfcLinearPositioningElement(DatabaseIfc db, IfcLinearPositioningElement e, DuplicateOptions options) : base(db, e, options) { Axis = db.Factory.Duplicate(e.Axis) as IfcCurve; }
	}
	[Serializable]
	public partial class IfcLinearSpanPlacement : IfcLinearPlacement
	{
		private double mSpan = 0; //: IfcPositiveLengthMeasure;
		public double Span { get { return mSpan; } set { mSpan = value; } }

		public IfcLinearSpanPlacement() : base() { }
		internal IfcLinearSpanPlacement(DatabaseIfc db, IfcLinearSpanPlacement linearSpanPlacement) : base(db, linearSpanPlacement)
		{
			Span = linearSpanPlacement.Span;
		}
		public IfcLinearSpanPlacement(IfcCurve placementMeasuredAlong, IfcPointByDistanceExpression distance, double span)
			: base(placementMeasuredAlong, distance) { Span = span; }
	}
	[Serializable]
	public partial class IfcLineIndex : List<int>, IfcSegmentIndexSelect
	{
		public IfcLineIndex(int a, int b) { base.Add(a); base.Add(b); }
		public IfcLineIndex(IEnumerable<int> indices) { base.AddRange(indices); }
		public override string ToString()
		{
			return "IFCLINEINDEX((" + string.Join(",", this) + "))";
		}
	}
	[Obsolete("DEPRECATED IFC4X3", false)]
	[Serializable]
	public partial class IfcLineSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		internal IfcLineSegment2D() : base() { }
		internal IfcLineSegment2D(DatabaseIfc db, IfcLineSegment2D s, DuplicateOptions options) : base(db, s, options) { }
		public IfcLineSegment2D(IfcCartesianPoint start, double startDirection, double length)
			: base(start, startDirection, length) { }
	}
	[Serializable]
	public partial class IfcLiquidTerminal : IfcFlowTerminal
	{
		private IfcLiquidTerminalTypeEnum mPredefinedType = IfcLiquidTerminalTypeEnum.NOTDEFINED; //: OPTIONAL IfcLiquidTerminalTypeEnum;
		public IfcLiquidTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcLiquidTerminal() : base() { }
		public IfcLiquidTerminal(DatabaseIfc db) : base(db) { }
		public IfcLiquidTerminal(DatabaseIfc db, IfcLiquidTerminal liquidTerminal, DuplicateOptions options) : base(db, liquidTerminal, options) { PredefinedType = liquidTerminal.PredefinedType; }
		public IfcLiquidTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcLiquidTerminalType : IfcFlowTerminalType
	{
		private IfcLiquidTerminalTypeEnum mPredefinedType = IfcLiquidTerminalTypeEnum.NOTDEFINED; //: IfcLiquidTerminalTypeEnum;
		public IfcLiquidTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcLiquidTerminalType() : base() { }
		public IfcLiquidTerminalType(DatabaseIfc db, IfcLiquidTerminalType liquidTerminalType, DuplicateOptions options) : base(db, liquidTerminalType, options) { PredefinedType = liquidTerminalType.PredefinedType; }
		public IfcLiquidTerminalType(DatabaseIfc db, string name, IfcLiquidTerminalTypeEnum predefinedType) : base(db)
		{
			Name = name;
			PredefinedType = predefinedType;
		}
	}
	[Serializable]
	public partial class IfcLocalPlacement : IfcObjectPlacement
	{
		private IfcAxis2Placement mRelativePlacement = null;// : IfcAxis2Placement;
		public IfcAxis2Placement RelativePlacement
		{
			get { return mRelativePlacement; }
			set { mRelativePlacement = value; }
		}

		internal IfcLocalPlacement() : base() { }
		internal IfcLocalPlacement(DatabaseIfc db, IfcLocalPlacement p) : base(db, p)
		{
			if (p.PlacementRelTo != null)
				PlacementRelTo = db.Factory.Duplicate(p.PlacementRelTo) as IfcObjectPlacement;
			RelativePlacement = db.Factory.Duplicate(p.mDatabase[p.mRelativePlacement.Index]) as IfcAxis2Placement;
		}
		public IfcLocalPlacement(IfcAxis2Placement placement) : base(placement.Database) { RelativePlacement = placement; }
		public IfcLocalPlacement(IfcObjectPlacement relativeTo, IfcAxis2Placement placement) : this(placement)
		{
			if (relativeTo != null)
				PlacementRelTo = relativeTo;
		}

		internal override bool isXYPlane(double tol)
		{
			IfcLocalPlacement placement = PlacementRelTo as IfcLocalPlacement;
			if (RelativePlacement.IsXYPlane(tol) && (placement == null || placement.isXYPlane(tol)))
				return true;
			return false;
		}
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcLocalTime : BaseClassIfc, IfcDateTimeSelect // DEPRECATED IFC4
	{
		internal int mHourComponent;// : IfcHourInDay;
		internal int mMinuteComponent;// : OPTIONAL IfcMinuteInHour;
		internal double mSecondComponent;// : OPTIONAL IfcSecondInMinute;
		internal int mZone;// OPTIONAL IfcCoordinatedUniversalTimeOffset;
		internal int mDaylightSavingOffset;// : OPTIONAL IfcDaylightSavingHour; 

		public IfcCoordinatedUniversalTimeOffset Zone { get { return mDatabase[mZone] as IfcCoordinatedUniversalTimeOffset; } set { mZone = (value == null ? 0 : value.mIndex); } }
		public int DaylightSavingOffset { get { return mDaylightSavingOffset; } set { mDaylightSavingOffset = value; } }
		internal IfcLocalTime() : base() { }
		internal IfcLocalTime(DatabaseIfc db, IfcLocalTime t) : base(db, t)
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
		//INVERSE GG
		internal IfcFaceBound mLoopOf = null;

		protected IfcLoop() : base() { }
		protected IfcLoop(DatabaseIfc db) : base(db) { }
		protected IfcLoop(DatabaseIfc db, IfcLoop l, DuplicateOptions options) : base(db, l, options) { }
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
		internal IfcLShapeProfileDef(DatabaseIfc db, IfcLShapeProfileDef p, DuplicateOptions options) : base(db, p, options)
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
			: base(db, name)
		{
			mDepth = depth;
			mWidth = width;
			mThickness = thickness;
		}
	}
}
