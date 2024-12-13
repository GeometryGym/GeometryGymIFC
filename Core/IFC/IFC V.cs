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
	[Serializable]
	public abstract partial class IfcValue : IfcMetricValueSelect, IfcAppliedValueSelect //SELECT(IfcMeasureValue,IfcSimpleValue,IfcDerivedMeasureValue); stpentity parse method
	{
		public abstract object Value { get; set; }
		public abstract Type ValueType { get; }
		public abstract string ValueString { get; }

		public int StepId { get { return 0; } }
		public string StepClassName { get { return this.GetType().Name; } }
		public int Index { get { return 0; } }
		public DatabaseIfc Database { get { return null; } }
	}  
	[Serializable]
	public partial class IfcValve : IfcFlowController //IFC4
	{
		private IfcValveTypeEnum mPredefinedType = IfcValveTypeEnum.NOTDEFINED;// OPTIONAL : IfcValveTypeEnum;
		public IfcValveTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcValveTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcValve() : base() { }
		internal IfcValve(DatabaseIfc db, IfcValve v, DuplicateOptions options) : base(db, v, options) { PredefinedType = v.PredefinedType; }
		public IfcValve(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcValveType : IfcFlowControllerType
	{
		private IfcValveTypeEnum mPredefinedType = IfcValveTypeEnum.NOTDEFINED;// : IfcValveTypeEnum; 
		public IfcValveTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcValveTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcValveType() : base() { }
		internal IfcValveType(DatabaseIfc db, IfcValveType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcValveType(DatabaseIfc db, string name, IfcValveTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcVector : IfcGeometricRepresentationItem, IfcHatchLineDistanceSelect, IfcVectorOrDirection
	{
		internal IfcDirection mOrientation; // : IfcDirection;
		internal double mMagnitude;// : IfcLengthMeasure; 

		public IfcDirection Orientation { get { return mOrientation; } set { mOrientation = value; } }
		public double Magnitude { get { return mMagnitude; } set { mMagnitude = value; } }

		internal IfcVector() : base() { }
		internal IfcVector(DatabaseIfc db, IfcVector v, DuplicateOptions options) : base(db, v, options) { Orientation = db.Factory.Duplicate(v.Orientation, options); mMagnitude = v.mMagnitude; }
		public IfcVector(IfcDirection orientation, double magnitude) : base(orientation.mDatabase) { Orientation = orientation; Magnitude = magnitude; }
	}
	public interface IfcVectorOrDirection : IBaseClassIfc { } // SELECT(IfcDirection, IfcVector);
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcVectorVoxelData : IfcVoxelData
	{
		internal IfcVector[] mValues = new IfcVector[0];// :	ARRAY [1:?] OF IfcVector;
		internal IfcUnit mUnit = null;// :	OPTIONAL IfcUnit;

		public IfcVector[] Values { get { return mValues; } set { mValues = value; } }
		public IfcUnit Unit { get { return mUnit; } set { mUnit = value; } }

		internal IfcVectorVoxelData() : base() { }
		internal IfcVectorVoxelData(DatabaseIfc db, IfcVectorVoxelData d, DuplicateOptions options) : base(db, d, options) { mValues = d.mValues; if (d.Unit != null) Unit = db.Factory.Duplicate(d.Unit); }
		public IfcVectorVoxelData(IfcProduct host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcVector[] values)
			: base(host, placement, representation) { Values = values; }
	}
	[Serializable]
	public partial class IfcVehicle : IfcTransportationDevice
	{
		private IfcVehicleTypeEnum mPredefinedType = IfcVehicleTypeEnum.NOTDEFINED;// : 	OPTIONAL IfcVehicleTypeEnum;
		public IfcVehicleTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcVehicleTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcVehicle() : base() { }
		internal IfcVehicle(DatabaseIfc db, IfcVehicle e, DuplicateOptions options) : base(db, e, options) { }
		public IfcVehicle(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcVehicleType : IfcTransportationDeviceType
	{
		private IfcVehicleTypeEnum mPredefinedType = IfcVehicleTypeEnum.NOTDEFINED;// IfcVehicleTypeEnum; 
		public IfcVehicleTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcVehicleTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcVehicleType() : base() { }
		internal IfcVehicleType(DatabaseIfc db, IfcVehicleType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcVehicleType(DatabaseIfc db, string name, IfcVehicleTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcVertex : IfcTopologicalRepresentationItem //SUPERTYPE OF(IfcVertexPoint)
	{
		//INVERSE
		internal List<IfcEdge> mAttachedEdges = new List<IfcEdge>(); // GG attribute

		internal IfcVertex() : base() { }
		internal IfcVertex(DatabaseIfc db, IfcVertex v, DuplicateOptions options) : base(db, v, options) { }
		public IfcVertex(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	[Obsolete("DEPRECATED IFC4", false)]
	public partial class IfcVertexBasedTextureMap : BaseClassIfc // DEPRECATED IFC4
	{
		internal LIST<IfcTextureVertex> mTextureVertices = new LIST<IfcTextureVertex>();// LIST [3:?] OF IfcTextureVertex;
		internal LIST<IfcCartesianPoint> mTexturePoints = new LIST<IfcCartesianPoint>();// LIST [3:?] OF IfcCartesianPoint; 

		internal IfcVertexBasedTextureMap() : base() { }
		internal IfcVertexBasedTextureMap(DatabaseIfc db, IfcVertexBasedTextureMap m) : base(db) 
		{
			mTextureVertices.AddRange(m.mTextureVertices.Select(x => db.Factory.Duplicate(x)));
			mTexturePoints.AddRange(m.mTexturePoints.Select(x => db.Factory.Duplicate(x)));
		}
	}
	[Serializable]
	public partial class IfcVertexLoop : IfcLoop
	{
		internal IfcVertex mLoopVertex;// : IfcVertex; 
		public IfcVertex LoopVertex { get { return mLoopVertex; } set { mLoopVertex = value; } }

		internal IfcVertexLoop() : base() { }
		internal IfcVertexLoop(DatabaseIfc db, IfcVertexLoop l, DuplicateOptions options) : base(db, l, options) { LoopVertex = db.Factory.Duplicate(l.LoopVertex) as IfcVertex; }

	}
	[Serializable]
	public partial class IfcVertexPoint : IfcVertex, IfcPointOrVertexPoint
	{
		private IfcPoint mVertexGeometry;// : IfcPoint; 
		public IfcPoint VertexGeometry { get { return mVertexGeometry; } set { mVertexGeometry = value; if (value != null) value.mOfVertex = this; } }
		
		internal IfcVertexPoint() : base() { }
		internal IfcVertexPoint(DatabaseIfc db, IfcVertexPoint v, DuplicateOptions options) : base(db, v, options) { VertexGeometry = db.Factory.Duplicate(v.VertexGeometry) as IfcPoint; }
		public IfcVertexPoint(IfcPoint p) : base(p.mDatabase) { VertexGeometry = p; }

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			result.AddRange(VertexGeometry.Extract<T>());
			return result;
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X2)]
	public partial class IfcVibrationDamper : IfcElementComponent
	{
		private IfcVibrationDamperTypeEnum mPredefinedType = IfcVibrationDamperTypeEnum.NOTDEFINED; //: OPTIONAL IfcVibrationDamperTypeEnum;
		public IfcVibrationDamperTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcVibrationDamperTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcVibrationDamper() : base() { }
		public IfcVibrationDamper(DatabaseIfc db) : base(db) { }
		public IfcVibrationDamper(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X2)]
	public partial class IfcVibrationDamperType : IfcElementComponentType
	{
		private IfcVibrationDamperTypeEnum mPredefinedType = IfcVibrationDamperTypeEnum.NOTDEFINED; //: IfcVibrationDamperTypeEnum;
		public IfcVibrationDamperTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcVibrationDamperTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcVibrationDamperType() : base() { }
		public IfcVibrationDamperType(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Serializable, Obsolete("DEPRECATED IFC4X4", false)]
	public partial class IfcVibrationIsolator : IfcElementComponent
	{
		private IfcVibrationIsolatorTypeEnum mPredefinedType = IfcVibrationIsolatorTypeEnum.NOTDEFINED;// : OPTIONAL IfcVibrationIsolatorTypeEnum;
		public IfcVibrationIsolatorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcVibrationIsolatorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcVibrationIsolator() : base() { }
		internal IfcVibrationIsolator(DatabaseIfc db, IfcVibrationIsolator i, DuplicateOptions options) : base(db, i, options) { PredefinedType = i.PredefinedType; }
		public IfcVibrationIsolator(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, Obsolete("DEPRECATED IFC4X4", false)]
	public partial class IfcVibrationIsolatorType : IfcElementComponentType
	{
		private IfcVibrationIsolatorTypeEnum mPredefinedType = IfcVibrationIsolatorTypeEnum.NOTDEFINED;// : IfcVibrationIsolatorTypeEnum
		public IfcVibrationIsolatorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcVibrationIsolatorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcVibrationIsolatorType() : base() { }
		internal IfcVibrationIsolatorType(DatabaseIfc db, IfcVibrationIsolatorType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcVibrationIsolatorType(DatabaseIfc db, string name, IfcVibrationIsolatorTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcVirtualElement : IfcElement
	{
		private IfcVirtualElementTypeEnum mPredefinedType = IfcVirtualElementTypeEnum.NOTDEFINED;// : IfcVirtualElementTypeEnum
		public IfcVirtualElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcVirtualElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcVirtualElement() : base() { }
		internal IfcVirtualElement(DatabaseIfc db, IfcVirtualElement e, DuplicateOptions options) : base(db, e, options) { }
		public IfcVirtualElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductDefinitionShape r) : base(host, p, r) { }
	}
	[Serializable]
	public partial class IfcVirtualGridIntersection : BaseClassIfc, IfcGridPlacementDirectionSelect
	{
		private Tuple<IfcGridAxis, IfcGridAxis> mIntersectingAxes = null;// : LIST [2:2] OF UNIQUE IfcGridAxis;
		private Tuple<double,double,double> mOffsetDistances = null;// : LIST [2:3] OF IfcLengthMeasure; 
		public Tuple<IfcGridAxis,IfcGridAxis> IntersectingAxes { get { return mIntersectingAxes; } set { mIntersectingAxes = value; } }
		internal IfcVirtualGridIntersection() : base() { }
		internal IfcVirtualGridIntersection(DatabaseIfc db, IfcVirtualGridIntersection i) : base(db, i) { Tuple<IfcGridAxis, IfcGridAxis> axes = i.IntersectingAxes; IntersectingAxes = new Tuple<IfcGridAxis,IfcGridAxis>(db.Factory.Duplicate(axes.Item1) as IfcGridAxis, db.Factory.Duplicate(axes.Item2) as IfcGridAxis); mOffsetDistances = i.mOffsetDistances; }
	}
	[Serializable]
	public partial class IfcVoidingFeature : IfcFeatureElementSubtraction //IFC4
	{
		private IfcVoidingFeatureTypeEnum mPredefinedType = IfcVoidingFeatureTypeEnum.NOTDEFINED;// :IfcVoidingFeatureTypeEnum;  
		public IfcVoidingFeatureTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcVoidingFeatureTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		
		internal IfcVoidingFeature() : base() { }
		internal IfcVoidingFeature(DatabaseIfc db, IfcVoidingFeature v, DuplicateOptions options) : base(db, v, options) { PredefinedType = v.PredefinedType; }
		public IfcVoidingFeature(IfcElement host, IfcObjectPlacement placement, IfcProductDefinitionShape rep) 
			: base(host, placement, rep) { }
	}
	[Obsolete("RELEASE CANDIDATE IFC4X3", false)]
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcVoidStratum : IfcGeotechnicalStratum
	{
		public override string StepClassName { get { return (mDatabase.mRelease >= ReleaseVersion.IFC4X3 ? "IfcGeotechnicalStratum" : base.StepClassName); } }

		internal IfcVoidStratum() : base() { PredefinedType = IfcGeotechnicalStratumTypeEnum.VOID; }
		internal IfcVoidStratum(DatabaseIfc db) : base(db) { PredefinedType = IfcGeotechnicalStratumTypeEnum.VOID; }
		internal IfcVoidStratum(DatabaseIfc db, IfcVoidStratum voidStratum, DuplicateOptions options)
			: base(db, voidStratum, options) { PredefinedType = IfcGeotechnicalStratumTypeEnum.VOID; }
		public IfcVoidStratum(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape shape) 
			: base(host, placement, shape) { PredefinedType = IfcGeotechnicalStratumTypeEnum.VOID; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public abstract partial class IfcVoxelData : IfcComplementaryData
	{
		private string mValueType = ""; //: OPTIONAL IfcLabel;
		public string ValueType { get { return mValueType; } set { mValueType = value; } }
		internal IfcVoxelData() : base() { }
		internal IfcVoxelData(DatabaseIfc db, IfcVoxelData o, DuplicateOptions options) : base(db, o, options) { mValueType = o.mValueType; }
		protected IfcVoxelData(IfcProduct host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) 
			: base(host.Database) { new IfcRelAssignsToProduct(this, host); }
	}
	[Serializable]
	public partial class IfcVoxelGrid : IfcSolidModel
	{
		internal double mVoxelSizeX, mVoxelSizeY = double.NaN, mVoxelSizeZ = double.NaN;// : IfcNonNegativeLengthMeasure;
		internal int mNumberOfVoxelsX, mNumberOfVoxelsY = int.MinValue, mNumberOfVoxelsZ = int.MinValue;// : OPTIONAL IfcInteger;
		internal List<bool> mVoxels = new List<bool>();

		public double VoxelSizeX { get { return mVoxelSizeX; } set { mVoxelSizeX = value; } }
		public double VoxelSizeY { get { return (double.IsNaN(mVoxelSizeY) ? mVoxelSizeX : mVoxelSizeY); } set { mVoxelSizeY = value; } }
		public double VoxelSizeZ { get { return (double.IsNaN(mVoxelSizeZ) ? mVoxelSizeX : mVoxelSizeZ); } set { mVoxelSizeZ = value; } }
		public int NumberOfVoxelsX { get { return mNumberOfVoxelsX; } set { mNumberOfVoxelsX = value; } }
		public int NumberOfVoxelsY { get { return (mNumberOfVoxelsY == int.MinValue ? mNumberOfVoxelsX : mNumberOfVoxelsY); } set { mNumberOfVoxelsY = value; } }
		public int NumberOfVoxelsZ { get { return (mNumberOfVoxelsZ == int.MinValue ? mNumberOfVoxelsX : mNumberOfVoxelsZ); } set { mNumberOfVoxelsZ = value; } }
		public List<bool> Voxels { get { return mVoxels; } }

		internal IfcVoxelGrid() : base() { }
		internal IfcVoxelGrid(DatabaseIfc db, IfcVoxelGrid g, DuplicateOptions options) : base(db, g, options)
		{
			mVoxelSizeX = g.mVoxelSizeX;
			mVoxelSizeY = g.mVoxelSizeY;
			mVoxelSizeZ = g.mVoxelSizeZ;
			mNumberOfVoxelsX = g.mNumberOfVoxelsX;
			mNumberOfVoxelsY = g.mNumberOfVoxelsY;
			mNumberOfVoxelsZ = g.mNumberOfVoxelsZ;
			mVoxels.AddRange(g.mVoxels);
		}
		public IfcVoxelGrid(DatabaseIfc db, double sizeX, double sizeY, double sizeZ, int numberX, int numberY, int numberZ, List<bool> voxels) 
			: base(db) 
		{
			mVoxelSizeX = sizeX;
			mVoxelSizeY = sizeY;
			mVoxelSizeZ = sizeZ;
			mNumberOfVoxelsX = numberX;
			mNumberOfVoxelsY = numberY;
			mNumberOfVoxelsZ = numberZ;
			mVoxels.AddRange(voxels);
		}
	}
	[Serializable]
	public abstract partial class IfcDerivedMeasureValue : IfcValue
	{
		internal double mValue;
		public override object Value { get { return mValue; } set { mValue = Convert.ToDouble(value); } }
		public override Type ValueType { get { return typeof(double); } }
		public double Measure { get { return mValue; } set { mValue = value; } }
		protected IfcDerivedMeasureValue() { }
		protected IfcDerivedMeasureValue(double value) { mValue = value; }
		public override string ToString() { return this.GetType().Name.ToUpper() + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public override string ValueString { get { return Value.ToString(); } }
	} //(IfcVolumetricFlowRateMeasure,IfcTimeStamp ,IfcThermalTransmittanceMeasure ,IfcThermalResistanceMeasure
	  //,IfcThermalAdmittanceMeasure ,IfcPressureMeasure ,IfcPowerMeasure ,IfcMassFlowRateMeasure ,IfcMassDensityMeasure ,IfcLinearVelocityMeasure
	  //,IfcKinematicViscosityMeasure ,IfcIntegerCountRateMeasure ,IfcHeatFluxDensityMeasure ,IfcFrequencyMeasure ,IfcEnergyMeasure ,IfcElectricVoltageMeasure
	  //,IfcDynamicViscosityMeasure ,IfcCompoundPlaneAngleMeasure ,IfcAngularVelocityMeasure ,IfcThermalConductivityMeasure ,IfcMolecularWeightMeasure
	  //,IfcVaporPermeabilityMeasure ,IfcMoistureDiffusivityMeasure ,IfcIsothermalMoistureCapacityMeasure ,IfcSpecificHeatCapacityMeasure ,IfcMonetaryMeasure
	  //,IfcMagneticFluxDensityMeasure ,IfcMagneticFluxMeasure ,IfcLuminousFluxMeasure ,IfcForceMeasure ,IfcInductanceMeasure ,IfcIlluminanceMeasure
	  //,IfcElectricResistanceMeasure ,IfcElectricConductanceMeasure ,IfcElectricChargeMeasure ,IfcDoseEquivalentMeasure ,IfcElectricCapacitanceMeasure
	  //,IfcAbsorbedDoseMeasure ,IfcRadioActivityMeasure ,IfcRotationalFrequencyMeasure ,IfcTorqueMeasure ,IfcAccelerationMeasure ,IfcLinearForceMeasure
	  //,IfcLinearStiffnessMeasure ,IfcModulusOfSubgradeReactionMeasure ,IfcModulusOfElasticityMeasure ,IfcMomentOfInertiaMeasure ,IfcPlanarForceMeasure
	  //,IfcRotationalStiffnessMeasure ,IfcShearModulusMeasure ,IfcLinearMomentMeasure ,IfcLuminousIntensityDistributionMeasure ,IfcCurvatureMeasure
	  //,IfcMassPerLengthMeasure ,IfcModulusOfLinearSubgradeReactionMeasure ,IfcModulusOfRotationalSubgradeReactionMeasure ,IfcRotationalMassMeasure
	  //,IfcSectionalAreaIntegralMeasure ,IfcSectionModulusMeasure ,IfcTemperatureGradientMeasure ,IfcThermalExpansionCoefficientMeasure ,IfcWarpingConstantMeasure
	  //,IfcWarpingMomentMeasure ,IfcSoundPowerMeasure ,IfcSoundPressureMeasure ,IfcHeatingValueMeasure,IfcPHMeasure,IfcIonConcentrationMeasure);

	[Serializable]
	public class IfcAbsorbedDoseMeasure : IfcDerivedMeasureValue { public IfcAbsorbedDoseMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcAccelerationMeasure : IfcDerivedMeasureValue { public IfcAccelerationMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcAngularVelocityMeasure : IfcDerivedMeasureValue { public IfcAngularVelocityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcAreaDensityMeasure : IfcDerivedMeasureValue { public IfcAreaDensityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcCompoundPlaneAngleMeasure //: IfcDerivedMeasureValue
	{
		internal int mDegrees = 0, mMinutes = 0, mSeconds = 0, mMicroSeconds = 0;
		public IfcCompoundPlaneAngleMeasure(double angleDegrees) 
		{
			double ang = Math.Abs(angleDegrees);
			int sign = angleDegrees < 0 ? -1 : 1;
			int degrees = (int)Math.Floor(ang);
			mDegrees = sign * degrees;
			double minutes = (ang - degrees) * 60;
			int iMinutes = (int)Math.Floor(minutes);
			mMinutes = sign * iMinutes;
			double seconds = (minutes - iMinutes) * 60;
			int iSeconds = (int)Math.Floor(seconds);
			mSeconds = sign * iSeconds;
			mMicroSeconds = (int)(sign * (seconds - iSeconds)  * 1e6);

		}
		public IfcCompoundPlaneAngleMeasure(int degrees, int minutes, int seconds, int microSeconds)
		{
			mDegrees = degrees;
			mMinutes = minutes;
			mSeconds = seconds;
			mMicroSeconds = microSeconds;
		}

		internal static IfcCompoundPlaneAngleMeasure Parse(string text)
		{
			if (string.IsNullOrEmpty(text) || text == "$")
				return null;
			double angle = 0;
			
			string str = text.Replace("(", "").Replace(")", "");
			string[] fields = str.Split(",".ToCharArray());
			if (fields.Length == 1 && double.TryParse(text, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out angle))
				return new IfcCompoundPlaneAngleMeasure(angle);
			if (fields.Length >= 3)
			{
				int degrees = 0, minutes = 0, seconds = 0, microSeconds = 0;
				if (int.TryParse(fields[0], out degrees) && int.TryParse(fields[1], out minutes) && int.TryParse(fields[2], out seconds) && (fields.Length == 3 || int.TryParse(fields[3], out microSeconds)))
					return new IfcCompoundPlaneAngleMeasure(degrees, minutes, seconds, microSeconds);
			}

			// handle 50°58'33"S.
			return null;
		}
		public string ToSTEP() { return "(" + mDegrees + "," + mMinutes + "," + mSeconds + (mMicroSeconds == 0 ? ")" : "," + mMicroSeconds + ")"); }
		public override string ToString()
		{
			return this.GetType().Name.ToUpper() + ToSTEP();
		}
		public double Angle()
		{
			double minutes = Math.Abs(mMinutes) / 60.0, seconds = Math.Abs(mSeconds) / 3600.0, microSeconds = Math.Abs(mMicroSeconds) / 3600000000.0;
			double compound = minutes + seconds + microSeconds;
			double multiplier = (mDegrees == 0 ? (mMinutes == 0 ? (mSeconds == 0 ? (mMicroSeconds > 0 ? 1 : -1) : (mSeconds > 0 ? 1 : -1)) : (mMinutes > 0 ? 1 : -1)) : (mDegrees > 0 ? 1 : -1));
			return mDegrees + multiplier  * compound;
		}
	}
	[Serializable]
	public class IfcCurvatureMeasure : IfcDerivedMeasureValue { public IfcCurvatureMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcDoseEquivalentMeasure : IfcDerivedMeasureValue { public IfcDoseEquivalentMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcDynamicViscosityMeasure : IfcDerivedMeasureValue { public IfcDynamicViscosityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcElectricCapacitanceMeasure : IfcDerivedMeasureValue { public IfcElectricCapacitanceMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcElectricChargeMeasure : IfcDerivedMeasureValue { public IfcElectricChargeMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcElectricConductanceMeasure : IfcDerivedMeasureValue { public IfcElectricConductanceMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcElectricResistanceMeasure : IfcDerivedMeasureValue { public IfcElectricResistanceMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcElectricVoltageMeasure : IfcDerivedMeasureValue { public IfcElectricVoltageMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcEnergyMeasure : IfcDerivedMeasureValue { public IfcEnergyMeasure(double value) : base(value) { } }
	public class IfcForceMeasure : IfcDerivedMeasureValue { public IfcForceMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcFrequencyMeasure : IfcDerivedMeasureValue { public IfcFrequencyMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcHeatFluxDensityMeasure : IfcDerivedMeasureValue { public IfcHeatFluxDensityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcHeatingValueMeasure : IfcDerivedMeasureValue { public IfcHeatingValueMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcIlluminanceMeasure : IfcDerivedMeasureValue { public IfcIlluminanceMeasure(double value) : base(value) { } } 
	[Serializable]
	public class IfcInductanceMeasure : IfcDerivedMeasureValue { public IfcInductanceMeasure(double value) : base(value) { } } 
	[Serializable]
	public class IfcIntegerCountRateMeasure : IfcDerivedMeasureValue
	{
		public IfcIntegerCountRateMeasure(double value) : base((int)value) { }
		public override string ToString() { return this.GetType().Name.ToUpper() + "(" + (int)mValue + ")"; }
	}
	[Serializable]
	public class IfcIonConcentrationMeasure : IfcDerivedMeasureValue { public IfcIonConcentrationMeasure(double value) : base(value) { } } 
	[Serializable]
	public class IfcIsothermalMoistureCapacityMeasure : IfcDerivedMeasureValue { public IfcIsothermalMoistureCapacityMeasure(double value) : base(value) { } } 
	[Serializable]
	public class IfcKinematicViscosityMeasure : IfcDerivedMeasureValue { public IfcKinematicViscosityMeasure(double value) : base(value) { } } 
	[Serializable]
	public class IfcLinearForceMeasure : IfcDerivedMeasureValue { public IfcLinearForceMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcLinearMomentMeasure : IfcDerivedMeasureValue { public IfcLinearMomentMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcLinearStiffnessMeasure : IfcDerivedMeasureValue 
	{
		public IfcLinearStiffnessMeasure() : base() { }
		public IfcLinearStiffnessMeasure(double value) : base(value) { }
	}
	[Serializable]
	public class IfcLinearVelocityMeasure : IfcDerivedMeasureValue { public IfcLinearVelocityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcLuminousFluxMeasure : IfcDerivedMeasureValue { public IfcLuminousFluxMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcLuminousIntensityDistributionMeasure : IfcDerivedMeasureValue { public IfcLuminousIntensityDistributionMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcLuminousMeasure : IfcDerivedMeasureValue { public IfcLuminousMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcMagneticFluxDensityMeasure : IfcDerivedMeasureValue { public IfcMagneticFluxDensityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcMagneticFluxMeasure : IfcDerivedMeasureValue { public IfcMagneticFluxMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcMassDensityMeasure : IfcDerivedMeasureValue { public IfcMassDensityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcMassFlowRateMeasure : IfcDerivedMeasureValue { public IfcMassFlowRateMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcMassPerLengthMeasure : IfcDerivedMeasureValue { public IfcMassPerLengthMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcModulusOfElasticityMeasure : IfcDerivedMeasureValue { public IfcModulusOfElasticityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcModulusOfSubgradeReactionMeasure : IfcDerivedMeasureValue 
	{
		public IfcModulusOfSubgradeReactionMeasure() { }
		public IfcModulusOfSubgradeReactionMeasure(double value) : base(value) { } 
	}
	[Serializable]
	public class IfcModulusOfLinearSubgradeReactionMeasure : IfcDerivedMeasureValue 
	{ 
		public IfcModulusOfLinearSubgradeReactionMeasure() { } 
		public IfcModulusOfLinearSubgradeReactionMeasure(double value) : base(value) { } 
	}
	[Serializable]
	public class IfcModulusOfRotationalSubgradeReactionMeasure : IfcDerivedMeasureValue 
	{ 
		public IfcModulusOfRotationalSubgradeReactionMeasure() { }
		public IfcModulusOfRotationalSubgradeReactionMeasure(double value) : base(value) { }
	}
	[Serializable]
	public class IfcMoistureDiffusivityMeasure : IfcDerivedMeasureValue { public IfcMoistureDiffusivityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcMolecularWeightMeasure : IfcDerivedMeasureValue { public IfcMolecularWeightMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcMomentOfInertiaMeasure : IfcDerivedMeasureValue { public IfcMomentOfInertiaMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcMonetaryMeasure : IfcDerivedMeasureValue { public IfcMonetaryMeasure(double value) : base(value) { } }//, IfcAppliedValueSelect
	[Serializable]
	public class IfcPHMeasure : IfcDerivedMeasureValue { public IfcPHMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcPlanarForceMeasure : IfcDerivedMeasureValue { public IfcPlanarForceMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcPowerMeasure : IfcDerivedMeasureValue { public IfcPowerMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcPressureMeasure : IfcDerivedMeasureValue { public IfcPressureMeasure(double value) : base(value) { } } 
	[Serializable]
	public class IfcRadioActivityMeasure : IfcDerivedMeasureValue { public IfcRadioActivityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcRotationalFrequencyMeasure : IfcDerivedMeasureValue { public IfcRotationalFrequencyMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcRotationalMassMeasure : IfcDerivedMeasureValue { public IfcRotationalMassMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcRotationalStiffnessMeasure : IfcDerivedMeasureValue { public IfcRotationalStiffnessMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcSectionalAreaIntegralMeasure : IfcDerivedMeasureValue { public IfcSectionalAreaIntegralMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcSectionModulusMeasure : IfcDerivedMeasureValue { public IfcSectionModulusMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcShearModulusMeasure : IfcDerivedMeasureValue { public IfcShearModulusMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcSoundPowerMeasure : IfcDerivedMeasureValue { public IfcSoundPowerMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcSoundPressureMeasure : IfcDerivedMeasureValue { public IfcSoundPressureMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcSpecificHeatCapacityMeasure : IfcDerivedMeasureValue { public IfcSpecificHeatCapacityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcTemperatureGradientMeasure : IfcDerivedMeasureValue { public IfcTemperatureGradientMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcThermalAdmittanceMeasure : IfcDerivedMeasureValue { public IfcThermalAdmittanceMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcThermalConductivityMeasure : IfcDerivedMeasureValue { public IfcThermalConductivityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcThermalExpansionCoefficientMeasure : IfcDerivedMeasureValue { public IfcThermalExpansionCoefficientMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcThermalTransmittanceMeasure : IfcDerivedMeasureValue { public IfcThermalTransmittanceMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcTorqueMeasure : IfcDerivedMeasureValue { public IfcTorqueMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcVaporPermeabilityMeasure : IfcDerivedMeasureValue { public IfcVaporPermeabilityMeasure(double value) : base(value) { } }// IfcMeasureValue
	[Serializable]
	public class IfcVolumetricFlowRateMeasure : IfcDerivedMeasureValue { public IfcVolumetricFlowRateMeasure(double value) : base(value) { } }// IfcMeasureValue
	[Serializable]
	public class IfcWarpingConstantMeasure : IfcDerivedMeasureValue { public IfcWarpingConstantMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcWarpingMomentMeasure : IfcDerivedMeasureValue { public IfcWarpingMomentMeasure(double value) : base(value) { } }

	[Serializable]
	public abstract partial class IfcMeasureValue : IfcValue //TYPE IfcMeasureValue = SELECT (IfcVolumeMeasure,IfcTimeMeasure ,IfcThermodynamicTemperatureMeasure ,IfcSolidAngleMeasure ,IfcPositiveRatioMeasure
	{ //,IfcRatioMeasure ,IfcPositivePlaneAngleMeasure ,IfcPlaneAngleMeasure ,IfcParameterValue ,IfcNumericMeasure ,IfcMassMeasure ,IfcPositiveLengthMeasure,IfcLengthMeasure ,IfcElectricCurrentMeasure ,
      //IfcDescriptiveMeasure ,IfcCountMeasure ,IfcContextDependentMeasure ,IfcAreaMeasure ,IfcAmountOfSubstanceMeasure ,IfcLuminousIntensityMeasure ,IfcNormalisedRatioMeasure ,IfcComplexNumber);
		internal double mValue;
		public override object Value { get { return mValue; } set { mValue = Convert.ToDouble(value); } }
		public override Type ValueType { get { return typeof(double); } }
		public double Measure { get { return mValue; } set { mValue = value; } }
		public string Format { get; set; } = "{0:0.0################}";
		protected IfcMeasureValue(double value) { mValue = value; }
		public override string ToString() { return this.GetType().Name.ToUpper() + "(" + ParserSTEP.DoubleToString(mValue, Format) + ")"; }
		public override string ValueString { get { return Value.ToString(); } }
	}
	[Serializable]
	public class IfcAreaMeasure : IfcMeasureValue { public IfcAreaMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcAmountOfSubstanceMeasure : IfcMeasureValue { public IfcAmountOfSubstanceMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcComplexNumber : IfcMeasureValue
	{
		internal double mImaginary = 0;
		public IfcComplexNumber(double real) : base(real) { }
		public IfcComplexNumber(double real, double imaginary) : base(real) { mImaginary = imaginary; }
		public override string ToString() { return this.GetType().Name + "((" + ParserSTEP.DoubleToString(mValue) + "," + ParserSTEP.DoubleToString(mImaginary) + "))"; }
	}
	[Serializable]
	public class IfcContextDependentMeasure : IfcMeasureValue { public IfcContextDependentMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcCountMeasure : IfcMeasureValue { public IfcCountMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcDescriptiveMeasure : IfcMeasureValue, IfcSizeSelect 
	{
		internal string mStringValue;
		public override object Value { get { return mStringValue; } }
		public IfcDescriptiveMeasure(string value) : base(0) { mStringValue = value; }
		public override string ToString() { return "IFCDESCRIPTIVEMEASURE(" + mValue + ")"; }
	}
	[Serializable]
	public class IfcElectricCurrentMeasure : IfcMeasureValue { public IfcElectricCurrentMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcLengthMeasure : IfcMeasureValue, IfcSizeSelect, IfcBendingParameterSelect, IfcCurveMeasureSelect 
	{
		public IfcLengthMeasure(double value) : base(value) { } 
	}
	[Serializable]
	public class IfcLuminousIntensityMeasure : IfcMeasureValue { public IfcLuminousIntensityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcMassMeasure : IfcMeasureValue { public IfcMassMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcNonNegativeLengthMeasure : IfcMeasureValue, IfcCurveMeasureSelect
	{
		public IfcNonNegativeLengthMeasure(double value) : base(value) { }
		internal IfcNonNegativeLengthMeasure(string str) : base(0)
		{
			int icounter = 0;
			for (; icounter < str.Length; icounter++)
			{
				Char c = str[icounter];
				if (char.IsDigit(c))
					break;
				if (c == '.')
					break;
			}
			if (!double.TryParse(str.Substring(icounter), System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out mValue))
				mValue = 0;
		}
	}
	[Serializable]
	public partial class IfcNormalisedRatioMeasure : IfcMeasureValue, IfcColourOrFactor { public IfcNormalisedRatioMeasure(double value) : base(Math.Min(1, Math.Max(0, value))) { }  }
	[Serializable]
	public class IfcNumericMeasure : IfcMeasureValue { public IfcNumericMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcParameterValue : IfcMeasureValue, IfcCurveMeasureSelect { public IfcParameterValue(double value) : base(value) { } }
	[Serializable]
	public class IfcPlaneAngleMeasure : IfcMeasureValue, IfcBendingParameterSelect, IfcOrientationSelect { public IfcPlaneAngleMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcPositivePlaneAngleMeasure : IfcMeasureValue { public IfcPositivePlaneAngleMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcPositiveLengthMeasure : IfcMeasureValue, IfcSizeSelect, IfcHatchLineDistanceSelect
	{
		public IfcPositiveLengthMeasure(double value) : base(value) { }
		internal IfcPositiveLengthMeasure(string str) : base(0)
		{
			int icounter = 0;
			for (; icounter < str.Length; icounter++)
			{
				Char c = str[icounter];
				if (char.IsDigit(c))
					break;
				if (c == '.')
					break;
			}
			if (!double.TryParse(str.Substring(icounter), System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out mValue))
				mValue = 0;
		}
	}
	[Serializable]
	public class IfcPositiveRatioMeasure : IfcMeasureValue
	{
		public IfcPositiveRatioMeasure(double value) : base(value) { }
		public IfcPositiveRatioMeasure(string str) : base(0)
		{
			int icounter = 0;
			for (; icounter < str.Length; icounter++)
			{
				Char c = str[icounter];
				if (char.IsDigit(c))
					break;
				if (c == '.')
					break;
			}
			if (!double.TryParse(str.Substring(icounter), System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out mValue))
				mValue = 0;
		}
	}
	[Serializable]
	public class IfcRatioMeasure : IfcMeasureValue, IfcTimeOrRatioSelect//, IfcAppliedValueSelect
	{
		public IfcRatioMeasure(double value) : base(value) { }
	}
	[Serializable]
	public class IfcSolidAngleMeasure : IfcMeasureValue { public IfcSolidAngleMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcThermodynamicTemperatureMeasure : IfcMeasureValue { public IfcThermodynamicTemperatureMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcTimeMeasure : IfcMeasureValue { public IfcTimeMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcVolumeMeasure : IfcMeasureValue { public IfcVolumeMeasure(double value) : base(value) { } }

	[Serializable]
	public abstract partial class IfcSimpleValue : IfcValue // = SELECT(IfcInteger,IfcReal,IfcBoolean,IfcIdentifier,IfcText,IfcLabel,IfcLogical,IfcBinary);
	{
		public override string ValueString { get { return Value.ToString(); } }
	}
	[Serializable]
	public partial class IfcBinary : IfcSimpleValue
	{
		public Byte[] Binary { get; set; }
		public override object Value { get { return Binary; } set { try { Binary = (Byte[])value; } catch (Exception) { }; } }
		public override Type ValueType { get { return typeof(long); } }
		public IfcBinary(Byte[] value) { Binary = value; }
		public override string ToString() { return "IFCBINARY(" + ParserSTEP.BinaryToString(Binary) + ")"; }
		public override string ValueString { get { return ParserSTEP.BinaryToString(Binary); } }
	}
	[Serializable]
	public partial class IfcBoolean : IfcSimpleValue
	{
		public bool Boolean { get; set; }
		public override object Value { get { return Boolean; } set { Boolean = Convert.ToBoolean(value); } }
		public override Type ValueType { get { return typeof(bool); } }
		public IfcBoolean(bool value) { Boolean = value; }
		public override string ToString() { return "IFCBOOLEAN(" + ParserSTEP.BoolToString(Boolean) + ")"; }
	}
	[Serializable]
	public partial class IfcDate : IfcSimpleValue
	{
		private DateTime mDate = DateTime.MinValue;
		public override object Value { get { return mDate; } set { mDate = Convert.ToDateTime(value); } }
		public override Type ValueType { get { return typeof(DateTime); } }
		public IfcDate(DateTime date) { mDate = date; }
		public override string ToString() { return "IFCDATE(" + (mDate == DateTime.MinValue ? "$)" : "'" + FormatSTEP(mDate) + "')"); }
		public static string FormatSTEP(DateTime date) { return date.Year + (date.Month < 10 ? "-0" : "-") + date.Month + (date.Day < 10 ? "-0" : "-") + date.Day; }
		internal static string STEPAttribute(DateTime dateTime) { return dateTime == DateTime.MinValue ? "$" : "'" + FormatSTEP(dateTime) + "'"; }
		internal static DateTime ParseSTEP(string date)
		{
			if (string.Compare(date, "$") == 0)
				return DateTime.MinValue;
			try
			{
				return DateTime.Parse(date);
			}
			catch(Exception) { }
			string str = date.Replace("'", "");
			string[] fields = str.Split("-".ToCharArray());
			return new DateTime(int.Parse(fields[0]), int.Parse(fields[1]), int.Parse(fields[2]));
		}
		public override string ValueString { get { return FormatSTEP(mDate); } }
	}
	[Serializable]
	public partial class IfcDateTime : IfcSimpleValue
	{
		private DateTime mDateTime = DateTime.MinValue;
		public override object Value { get { return mDateTime; } set { mDateTime = Convert.ToDateTime(value); } }
		public override Type ValueType { get { return typeof(DateTime); } }
		public IfcDateTime(DateTime datetime) { mDateTime = datetime; }
		public override string ToString() { return "IFCDATETIME(" + (mDateTime == DateTime.MinValue ? "$)" : "'" + FormatSTEP(mDateTime) + "')"); }
		public static string FormatSTEP(DateTime dateTime) { return dateTime.Year + (dateTime.Month < 10 ? "-0" : "-") + dateTime.Month + (dateTime.Day < 10 ? "-0" : "-") + dateTime.Day + (dateTime.Hour < 10 ? "T0" : "T") + dateTime.Hour + (dateTime.Minute < 10 ? ":0" : ":") + dateTime.Minute + (dateTime.Second < 10 ? ":0" : ":") + dateTime.Second; }
		internal static string STEPAttribute(DateTime dateTime) { return dateTime == DateTime.MinValue ? "$" : "'" + FormatSTEP(dateTime) + "'"; }
		public static DateTime ParseSTEP(string str)
		{
			string value = str.Replace("'", "");
			if (string.IsNullOrEmpty(value) || value == "$")
				return DateTime.MinValue;
			try
			{
				int year = int.Parse(value.Substring(0, 4)), month = int.Parse(value.Substring(5, 2)), day = int.Parse(value.Substring(8, 2));
				if (value.Contains("T"))
				{
					int hour = int.Parse(value.Substring(11, 2)), min = int.Parse(value.Substring(14, 2));
					double seconds = double.Parse(value.Substring(17, value.Length - 17), ParserSTEP.NumberFormat);
					return new DateTime(year, month, day, hour, min, (int)seconds);
				}
				return new DateTime(year, month, day);
			}
			catch (Exception) { }
			DateTime result = DateTime.MinValue;
			return (DateTime.TryParse(value, out result) ? result : DateTime.MinValue);
		}
		internal static IfcDateTimeSelect convertDateTimeSelect(DatabaseIfc db, DateTime date)
		{
			IfcCalendarDate cd = new IfcCalendarDate(db, date.Day, date.Month, date.Year);
			if (date.Hour + date.Minute + date.Second < db.Tolerance)
				return cd;
			return new IfcDateAndTime(cd, new IfcLocalTime(db, date.Hour, date.Minute, date.Second));
		}
		public override string ValueString { get { return FormatSTEP(mDateTime); } }
	}
	[Serializable]
	public partial class IfcDuration : IfcSimpleValue, IfcTimeOrRatioSelect
	{
		internal int mYears, mMonths, mDays, mHours, mMinutes;
		internal double mSeconds = 0;
		public int Years { get { return mYears; } set { mYears = value; } }
		public int Months { get { return mMonths; } set { mMonths = value; } }
		public int Days { get { return mDays; } set { mDays = value; } }
		public int Hours { get { return mHours; } set { mHours = value; } }
		public int Minutes { get { return mMinutes; } set { mMinutes = value; } }
		public double Seconds { get { return mSeconds; } set { mSeconds = value; } }

		public override object Value { get { return ToSeconds(); } set { fromSeconds(System.Convert.ToDouble(value)); } }
		public override Type ValueType { get { return typeof(double); } }

		public IfcDuration() { }
		public IfcDuration(TimeSpan timeSpan) { mDays = timeSpan.Days; mHours = timeSpan.Hours; mMinutes = timeSpan.Minutes; mSeconds = timeSpan.Seconds;  }
		public static string Convert(IfcDuration d) { return (d == null ? "$" : d.ToString()); }
		public static IfcDuration Convert(string s) 
		{
			IfcDuration duration = new IfcDuration();
			if (string.IsNullOrEmpty(s))
				return duration;
			if (s[0] != 'P')
			{
				if (int.TryParse(s, out int i))
					return new IfcDuration() { Days = i };
				return null;
			}
			int stringLength = s.Length;
			bool inTime = false;
			for(int icounter = 1; icounter < stringLength; icounter++)
			{
				char c = s[icounter];
				if (c == 'T')
				{
					inTime = true;
					continue;
				}
				string str = "";
				while(char.IsDigit(c) || c == '.')
				{
					str += c;
					c = s[++icounter];
				}
				
				if(!string.IsNullOrEmpty(str)) 
				{
					if(c== 'S')
					{
						double d = 0;
						if (double.TryParse(str, out d))
							duration.Seconds = d;
					}
					else
					{
						int i = 0;
						if(int.TryParse(str, out i))
						{
							if (c == 'Y')
								duration.Years = i;
							else if (c == 'M')
							{
								if (inTime)
									duration.Minutes = i;
								else
									duration.Months = i;
							}
							else if (c == 'D')
								duration.Days = i;
							else if (c == 'H')
								duration.Hours = i;
						}
					}
				}
			}
			return duration;
		} 
 
		public override string ValueString 
		{ 
			get 
			{
				string result = "P" + (mYears > 0 ? mYears + "Y" : "") + (mMonths > 0 ? mMonths + "M" : "") + (mDays > 0 ? mDays + "D" : "");
				if(mHours > 0 || mMinutes > 0 || mSeconds > 0)
					result += ("T" + (mHours > 0 ? mHours + "H" : "") + (mMinutes>0 ? mMinutes + "M" : "") + (mSeconds > 0 ? mSeconds.ToString(ParserSTEP.NumberFormat) + "S" : ""));
				if (result.Length == 1)
					return "PT0S";
				return result;
			} 
		}
		public override string ToString() { return "IFCDURATION('" + ValueString + "')"; }
		internal double ToSeconds() { return ((((((mYears * 365) + (mMonths * 30) + mDays) * 24) + mHours) * 60) + mMinutes) * 60 + mSeconds; }
		private void fromSeconds(double seconds) { mYears = mMonths = mDays = mHours = mMinutes = 0; mSeconds = seconds; }
	}
	[Serializable]
	public partial class IfcIdentifier : IfcSimpleValue
	{
		public string Identifier { get; set; }
		public override object Value { get { return Identifier; } set { Identifier = value.ToString(); } }
		public override Type ValueType { get { return typeof(string); } }
		public IfcIdentifier(string value) { Identifier = value; }
		public override string ToString() { return "IFCIDENTIFIER('" + ParserSTEP.Encode( Identifier) + "')"; }
	}
	[Serializable]
	public partial class IfcInteger : IfcSimpleValue
	{
		public long Magnitude { get; set; }
		public override object Value { get { return Magnitude; } set { Magnitude = Convert.ToInt64(value); } }
		public override Type ValueType { get { return typeof(long); } }
		public IfcInteger(long value) { Magnitude = value; }
		public override string ToString() { return "IFCINTEGER(" + Magnitude.ToString() + ")"; }
	}
	[Serializable]
	public partial class IfcLabel : IfcSimpleValue, IfcConditionCriterionSelect
	{
		public string Label { get; set; }
		public override object Value { get { return Label; } set { Label = value.ToString(); } }
		public override Type ValueType { get { return typeof(string); } }
		public IfcLabel(string value) { Label = value; }
		public override string ToString() { return "IFCLABEL('" + ParserSTEP.Encode(Label) + "')"; }
	}
	[Serializable]
	public partial class IfcLogical : IfcSimpleValue
	{
		public IfcLogicalEnum Logical { get; set; }
		public override object Value
		{
			get { return Logical; }
			set
			{
				IfcLogicalEnum logical = IfcLogicalEnum.UNKNOWN;
				if (!Enum.TryParse<IfcLogicalEnum>(value.ToString(), true, out logical))
				{
					bool boolean = Convert.ToBoolean(value);
					Logical = (boolean ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE);
				}
				else
					Logical = logical;
			}
		}
		public override Type ValueType { get { return typeof(IfcLogicalEnum); } }
		public IfcLogical(IfcLogicalEnum value) { Logical = value; }
		public IfcLogical(bool value) { Logical = value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE; }
		public override string ToString() { return "IFCLOGICAL(" + ParserIfc.LogicalToString(Logical) + ")"; }
	}
	[Serializable]
	public partial class IfcPositiveInteger : IfcSimpleValue
	{
		public int Magnitude { get; set; } 
		public override object Value { get { return Magnitude; } set { Magnitude = Convert.ToInt32(value); } }
		public override Type ValueType { get { return typeof(int); } }
		public IfcPositiveInteger(int value) { Magnitude = value; }
		public override string ToString() { return "IFCINTEGER(" + Magnitude.ToString() + ")"; }
	}
	[Serializable]
	public partial class IfcReal : IfcSimpleValue
	{
		public double Magnitude { get; set; }
		public override object Value { get { return Magnitude; } set { Magnitude = Convert.ToDouble(value); } }
		public override Type ValueType { get { return typeof(double); } }
		public string Format { get; set; } = "{0:0.0################}";
		public IfcReal(double value) { Magnitude = value; }
		public override string ToString() { return "IFCREAL(" + ParserSTEP.DoubleToString(Magnitude, Format) + ")"; }
	}
	[Serializable]
	public partial class IfcText : IfcSimpleValue
	{
		public string Text { get; set; }
		public override object Value { get { return Text; } set { Text = value.ToString(); } }
		public override Type ValueType { get { return typeof(string); } }
		public IfcText(string value) { Text = value; }
		public override string ToString() { return "IFCTEXT('" + ParserSTEP.Encode(Text) + "')"; }
	}
	[Serializable]
	public partial class IfcTime : IfcSimpleValue
	{
		internal DateTime mTime = DateTime.MinValue;
		public override string ToString() { return "IFCTIME(" + (mTime == DateTime.MinValue ? "$)" : "'" + FormatSTEP(mTime) + "')"); }
		public override object Value { get { return mTime; } set { mTime = Convert.ToDateTime(value); } }
		public override Type ValueType { get { return typeof(DateTime); } }
		public static string FormatSTEP(DateTime date) { return (date.Hour < 10 ? "T0" : "T") + date.Hour + (date.Minute < 10 ? ":0" : ":") + date.Minute + (date.Second < 10 ? ":0" : ":") + date.Second; }
		internal static DateTime parseSTEP(string str)
		{
			string value = str.Replace("'", "");
			if (string.IsNullOrEmpty(value) || value == "$")
				return DateTime.MinValue;
			try
			{
				DateTime min = DateTime.MinValue;
				int hour = int.Parse(value.Substring(1, 2)), minute = int.Parse(value.Substring(4, 2));
				double seconds = double.Parse(value.Substring(7, value.Length - 7), ParserSTEP.NumberFormat);
				return new DateTime(min.Year, min.Month, min.Day, hour, minute, (int)seconds);
			}
			catch (Exception) { }
			DateTime result = DateTime.MinValue;
			return (DateTime.TryParse(value, out result) ? result : DateTime.MinValue);
		}
		public override string ValueString { get { return FormatSTEP(mTime); } }
	}
	[Serializable]
	public class IfcTimeStamp : IfcSimpleValue
	{
		internal int mTimeStamp { get; set; }
		public override object Value { get { return mTimeStamp; } set { mTimeStamp = Convert.ToInt32(value); } }
		public override Type ValueType { get { return typeof(int); } }
		public IfcTimeStamp(int value) { mTimeStamp = value; }
		public override string ToString() { return "IFCTIMESTAMP" + "(" + mTimeStamp + ")"; }
	}
	[Serializable]
	public partial class IfcURIReference : IfcSimpleValue
	{
		internal string mValue = "";
		public Uri URI
		{
			get
			{
				if (Uri.IsWellFormedUriString(mValue, UriKind.RelativeOrAbsolute))
				{
					try { return new Uri(mValue); } catch (Exception) { }
				}
				return null;
			}
			set
			{
				mValue = value.OriginalString;
			}
		}
		public override object Value
		{
			get
			{
				Uri uri = URI;
				if (uri == null)
					return mValue;
				return uri;
			}
			set
			{
				Uri uri = value as Uri;
				if (uri != null)
					URI = uri;
				else
					mValue = value.ToString();
			}
		}
		public override Type ValueType { get { return typeof(Uri); } }
		public IfcURIReference(string value) { mValue = value; }
		public override string ToString() { return "IFCURIREFERENCE('" + ParserSTEP.Encode(mValue) + "')"; }
	}

	[Serializable]
	public partial class IfcSpecularExponent : IfcValue, IfcSpecularHighlightSelect
	{
		internal double mValue;
		public override object Value { get { return mValue; } set { mValue = Convert.ToInt32(value); } }
		public override Type ValueType { get { return typeof(double); } }
		public IfcSpecularExponent(double value) { mValue = value; }
		public override string ToString() { return "IFCSPECULAREXPONENT(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public override string ValueString { get { return Value.ToString(); } }
		public double SpecularExponent { get { return mValue; } }
	}
	[Serializable]
	public partial class IfcSpecularRoughness : IfcValue, IfcSpecularHighlightSelect
	{
		internal double mValue;
		public override object Value { get { return mValue; } set { mValue = Convert.ToInt32(value); } }
		public override Type ValueType { get { return typeof(double); } }
		public IfcSpecularRoughness(double value) { mValue = Math.Min(1, Math.Max(0, value)); }
		public override string ToString() { return "IFCSPECULARROUGHNESS(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public override string ValueString { get { return Value.ToString(); } }
		public double SpecularRoughness { get { return mValue; } }
	}
	public interface IfcSizeSelect { } //TYPE IfcSizeSelect = SELECT (IfcRatioMeasure ,IfcLengthMeasure ,IfcDescriptiveMeasure ,IfcPositiveLengthMeasure ,IfcNormalisedRatioMeasure ,IfcPositiveRatioMeasure);  
}
