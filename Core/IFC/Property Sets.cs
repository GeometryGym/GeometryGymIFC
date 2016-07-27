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
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class Pset_AirTerminalTypeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_AirTerminalShape Shape { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Shape", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_AirTerminalFaceType FaceType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "FaceType", new IfcLabel(value.ToString())).mIndex); } }
		public double SlotWidth { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "SlotWidth", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double SlotLength { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "SlotLength", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public int NumberOfSlots { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NumberOfSlots", new IfcInteger(value)).mIndex); } }
		public PEnum_AirTerminalFlowPattern FlowPattern { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "FlowPattern", new IfcLabel(value.ToString())).mIndex); } }
		public IfcPropertyBoundedValue<IfcVolumetricFlowRateMeasure> AirFlowrateRange { set { value.Name = "AirFlowrateRange"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> TemperatureRange { set { value.Name = "TemperatureRange"; mHasProperties.Add(value.mIndex); } }
		public PEnum_AirTerminalDischargeDirection DischargeDirection { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "DischargeDirection", new IfcLabel(value.ToString())).mIndex); } }
		public double ThrowLength { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ThrowLength", new IfcLengthMeasure(value)).mIndex); } }
		public double AirDiffusionPerformanceIndex { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "AirDiffusionPerformanceIndex", new IfcReal(value)).mIndex); } }
		public PEnum_AirTerminalFinishType FinishType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "FinishType", new IfcLabel(value.ToString())).mIndex); } }
		public string FinishColor { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "FinishColor", new IfcLabel(value)).mIndex); } }
		public PEnum_AirTerminalMountingType MountingType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "MountingType", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_AirTerminalCoreType CoreType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "CoreType", new IfcLabel(value.ToString())).mIndex); } }
		public double CoreSetHorizontal { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "CoreSetHorizontal", new IfcPlaneAngleMeasure(value)).mIndex); } }
		public double CoreSetVertical { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "CoreSetVertical", new IfcPlaneAngleMeasure(value)).mIndex); } }
		public bool HasIntegralControl { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "HasIntegralControl", new IfcBoolean(value)).mIndex); } }
		public PEnum_AirTerminalFlowControlType FlowControlType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "FlowControlType", new IfcLabel(value.ToString())).mIndex); } }
		public bool HasSoundAttenuator { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "HasSoundAttenuator", new IfcBoolean(value)).mIndex); } }
		public bool HasThermalInsulation { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "HasThermalInsulation", new IfcBoolean(value)).mIndex); } }
		public double NeckArea { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NeckArea", new IfcAreaMeasure(value)).mIndex); } }
		public double EffectiveArea { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "EffectiveArea", new IfcAreaMeasure(value)).mIndex); } }
		public IfcPropertyTableValue<IfcVolumetricFlowRateMeasure, IfcPositiveRatioMeasure> AirFlowrateVersusFlowControlElement { set { value.Name = "AirFlowrateVersusFlowControlElement"; mHasProperties.Add(value.mIndex); } }
		public Pset_AirTerminalTypeCommon(IfcAirTerminal airTerminal) : base(airTerminal.mDatabase, "Pset_AirTerminalTypeCommon") { Description = airTerminal.Name; DefinesOccurrence.assign(airTerminal); }
		public Pset_AirTerminalTypeCommon(IfcAirTerminalType airTerminalType) : base(airTerminalType.mDatabase, "Pset_AirTerminalTypeCommon") { Description = airTerminalType.Name; airTerminalType.AddPropertySet(this); }
	}
	public partial class Pset_AirTerminalOccurrence : IfcPropertySet
	{
		public PEnum_AirTerminalAirflowType AirflowType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "AirflowType", new IfcLabel(value.ToString())).mIndex); } }
		public double AirFlowRate { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "AirFlowRate", new IfcVolumetricFlowRateMeasure(value)).mIndex); } }
		public PEnum_AirTerminalLocation Location { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Location", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_AirTerminalOccurrence(IfcAirTerminal valve) : base(valve.mDatabase, "Pset_AirTerminalOccurrence") { Description = valve.Name; DefinesOccurrence.assign(valve); }
		public Pset_AirTerminalOccurrence(IfcAirTerminalType valveType) : base(valveType.mDatabase, "Pset_AirTerminalOccurrence") { Description = valveType.Name; valveType.AddPropertySet(this); }
	}

	public partial class Pset_BuildingCommon : IfcPropertySet
	{
		public string Reference { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public string BuildingID { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "BuildingID", new IfcIdentifier(value)).mIndex); } }
		public bool IsPermanentID { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "IsPermanentID", new IfcBoolean(value)).mIndex); } }
		public string ConstructionMethod { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConstructionMethod", new IfcLabel(value)).mIndex); } }
		public string FireProtectionClass { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "FireProtectionClass", new IfcLabel(value)).mIndex); } }
		public bool SprinklerProtection { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "SprinklerProtection", new IfcBoolean(value)).mIndex); } }
		public bool SprinklerProtectionAutomatic { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "SprinklerProtectionAutomatic", new IfcBoolean(value)).mIndex); } }
		public string OccupancyType { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "OccupancyType", new IfcLabel(value)).mIndex); } }
		public double GrossPlannedArea { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "GrossPlannedArea", new IfcAreaMeasure(value)).mIndex); } }
		public double NetPlannedArea { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NetPlannedArea", new IfcAreaMeasure(value)).mIndex); } }
		public int NumberOfStoreys { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NumberOfStoreys", new IfcInteger(value)).mIndex); } }
		public string YearOfConstruction { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "YearOfConstruction", new IfcLabel(value)).mIndex); } }
		public string YearOfLastRefurbishment { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "YearOfLastRefurbishment", new IfcLabel(value)).mIndex); } }
		public bool IsLandmarked { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "IsLandmarked", new IfcLogical(value)).mIndex); } }
		public Pset_BuildingCommon(IfcBuilding building) : base(building.mDatabase, "Pset_BuildingCommon") { Description = building.Name; DefinesOccurrence.assign(building);  }
	}
	public partial class Pset_BurnerTypeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_EnergySource EnergySource { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "EnergySource", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_BurnerTypeCommon(IfcBurner burner) : base(burner.mDatabase, "Pset_BurnerTypeCommon") { Description = burner.Name; DefinesOccurrence.assign(burner); }
		public Pset_BurnerTypeCommon(IfcBurnerType burnerType) : base(burnerType.mDatabase, "Pset_BurnerTypeCommon") { Description = burnerType.Name; burnerType.AddPropertySet(this); }
	}
	public partial class Pset_DistributionPortTypeDuct : IfcPropertySet
	{
		public PEnum_DuctConnectionType ConnectionType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "ConnectionType", new IfcLabel(value.ToString())).mIndex); } }
		public string ConnectionSubType { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConnectionSubType", new IfcLabel(value)).mIndex); } }
		public double NominalWidth { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalWidth", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double NominalHeight { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalHeight", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double NominalThickness { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalThickness", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double DryBulbTemperature { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "DryBulbTemperature", new IfcThermodynamicTemperatureMeasure(value)).mIndex); } }
		public double WetBulbTemperature { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "WetBulbTemperature", new IfcThermodynamicTemperatureMeasure(value)).mIndex); } }
		public double VolumetricFlowRate { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "VolumetricFlowRate", new IfcVolumetricFlowRateMeasure(value)).mIndex); } }
		public double Velocity { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Velocity", new IfcLinearVelocityMeasure(value)).mIndex); } }
		public double Pressure { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Pressure", new IfcPressureMeasure(value)).mIndex); } }
		public Pset_DistributionPortTypeDuct(DatabaseIfc db) : base(db, "Pset_DistributionPortTypeDuct") { }
	}
	public partial class Pset_LightFixtureTypeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public int NumberOfSources { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NumberOfSources", new IfcInteger(value)).mIndex); } }
		public PEnum_LightFixtureMountingType LightFixtureMountingType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "LightFixtureMountingType", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_LightFixturePlacingType LightFixturePlacingType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "LightFixturePlacingType", new IfcLabel(value.ToString())).mIndex); } }
		public double MaintenanceFactor { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "MaintenanceFactor", new IfcReal(value)).mIndex); } }
		public double MaximumPlenumSensibleLoad { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "MaximumPlenumSensibleLoad", new IfcPowerMeasure(value)).mIndex); } }
		public double MaximumSpaceSensibleLoad { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "MaximumSpaceSensibleLoad", new IfcPowerMeasure(value)).mIndex); } }
		public double SensibleLoadToRadiant { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "SensibleLoadToRadiant", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public Pset_LightFixtureTypeCommon(IfcLightFixture lightFixture) : base(lightFixture.mDatabase, "Pset_LightFixtureTypeCommon") { Description = lightFixture.Name; DefinesOccurrence.assign(lightFixture); }
		public Pset_LightFixtureTypeCommon(IfcLightFixtureType lightFixtureType) : base(lightFixtureType.mDatabase, "Pset_LightFixtureTypeCommon") { Description = lightFixtureType.Name; lightFixtureType.AddPropertySet(this); }
	}
	public partial class Pset_MaterialCommon : IfcMaterialProperties
	{
		public double MolecularWeight { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MolecularWeight", new IfcMolecularWeightMeasure(value)).mIndex); } }
		public double Porosity { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "Porosity", new IfcNormalisedRatioMeasure(value)).mIndex); } }
		public double MassDensity { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MassDensity", new IfcMassDensityMeasure(value)).mIndex); } }
		public Pset_MaterialCommon(IfcMaterialDefinition material) : base( "Pset_MaterialCommon",material) { Description = material.Name; }
	}
	public partial class Pset_MaterialMechanical : IfcMaterialProperties
	{
		public double DynamicViscosity { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "DynamicViscosity", new IfcDynamicViscosityMeasure(value)).mIndex); } }
		public double YoungModulus { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "YoungModulus", new IfcModulusOfElasticityMeasure(value)).mIndex); } }
		public double ShearModulus { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearModulus", new IfcModulusOfElasticityMeasure(value)).mIndex); } }
		public double PoissonRatio { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "PoissonRatio", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public double ThermalExpansionCoefficient { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "ThermalExpansionCoefficient", new IfcThermalExpansionCoefficientMeasure(value)).mIndex); } }
		public Pset_MaterialMechanical(IfcMaterialDefinition material) : base("Pset_MaterialMechanical",material) { Description = material.Name; }
	}
	public partial class Pset_MaterialSteel : IfcMaterialProperties
	{
		public double YieldStress { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "YieldStress", new IfcPressureMeasure(value)).mIndex); } }
		public double UltimateStress { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "UltimateStress", new IfcPressureMeasure(value)).mIndex); } }
		public double UltimateStrain { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "UltimateStrain", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public double HardeningModule { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "HardeningModule", new IfcModulusOfElasticityMeasure(value)).mIndex); } }
		public double ProportionalStress { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "ProportionalStress", new IfcPressureMeasure(value)).mIndex); } }
		public double PlasticStrain { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "PlasticStrain", new IfcPositiveRatioMeasure(value)).mIndex); } }
		//public double Relaxations { set { if (value > 0) mExtendedProperties.Add(new ifctableValue IfcPropertySingleValue(mDatabase, "Relaxations", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public Pset_MaterialSteel(IfcMaterialDefinition material) : base("Pset_MaterialSteel", material) { Description = material.Name; }
	}
	public partial class Pset_PipeFittingOccurrence : IfcPropertySet
	{
		public double InteriorRoughnessCoefficient { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "InteriorRoughnessCoefficient", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public string Color { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Color", new IfcLabel(value)).mIndex); } }
		public Pset_PipeFittingOccurrence(IfcPipeFitting pipeFitting) : base(pipeFitting.mDatabase, "Pset_PipeFittingOccurrence") { Description = pipeFitting.Name; DefinesOccurrence.assign(pipeFitting); }
	}
	public partial class Pset_PipeFittingTypeBend : IfcPropertySet
	{
		public double BendAngle { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "BendAngle", new IfcPositivePlaneAngleMeasure(value)).mIndex); } }
		public double BendRadius { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "BendRadius", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public Pset_PipeFittingTypeBend(IfcPipeFitting pipeFitting) : base(pipeFitting.mDatabase, "Pset_PipeFittingTypeBend") { Description = pipeFitting.Name; DefinesOccurrence.assign(pipeFitting); }
		public Pset_PipeFittingTypeBend(IfcPipeFittingType pipeFittingType) : base(pipeFittingType.mDatabase, "Pset_PipeFittingTypeBend") { Description = pipeFittingType.Name; pipeFittingType.AddPropertySet(this); }
	}
	public partial class Pset_PipeFittingTypeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public double PressureClass { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "PressureClass", new IfcPressureMeasure(value)).mIndex); } }
		public IfcPropertyBoundedValue<IfcPressureMeasure> PressureRange { set { value.Name = "PressureRange"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> TemperatureRange { set { value.Name = "TemperatureRange"; mHasProperties.Add(value.mIndex); } }
		public Pset_PipeFittingTypeCommon(IfcPipeFitting pipeFitting) : base(pipeFitting.mDatabase, "Pset_PipeFittingTypeCommon") { Description = pipeFitting.Name; DefinesOccurrence.assign(pipeFitting); }
		public Pset_PipeFittingTypeCommon(IfcPipeFittingType pipeFittingType) : base(pipeFittingType.mDatabase, "Pset_PipeFittingTypeCommon") { Description = pipeFittingType.Name; pipeFittingType.AddPropertySet(this); }
	}
	public partial class Pset_PipeFittingTypeJunction : IfcPropertySet
	{
		public PEnum_PipeFittingJunctionType JunctionType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "JunctionType", new IfcLabel(value.ToString())).mIndex); } }
		public double JunctionLeftAngle { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "JunctionLeftAngle", new IfcPositivePlaneAngleMeasure(value)).mIndex); } }
		public double JunctionLeftRadius { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "JunctionLeftRadius", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double JunctionRightAngle { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "JunctionRightAngle", new IfcPositivePlaneAngleMeasure(value)).mIndex); } }
		public double JunctionRightRadius { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "JunctionRightRadius", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public Pset_PipeFittingTypeJunction(IfcPipeFitting pipeFitting) : base(pipeFitting.mDatabase, "Pset_PipeFittingTypeJunction") { Description = pipeFitting.Name; DefinesOccurrence.assign(pipeFitting); }
		public Pset_PipeFittingTypeJunction(IfcPipeFittingType pipeFittingType) : base(pipeFittingType.mDatabase, "Pset_PipeFittingTypeJunction") { Description = pipeFittingType.Name; pipeFittingType.AddPropertySet(this); }
	}
	public partial class Pset_ProfileMechanical : IfcProfileProperties
	{
		public double MassPerLength { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MassPerLength", "", new IfcMassPerLengthMeasure(value)).mIndex); } }
		public double CrossSectionArea { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "CrossSectionArea", "", new IfcAreaMeasure(value)).mIndex); } }
		public double Perimeter { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "Perimeter", "", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double MinimumPlateThickness { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MinimumPlateThickness", "", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double MaximumPlateThickness { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MaximumPlateThickness", "", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double CentreOfGravityInX { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "CentreOfGravityInX", "", new IfcLengthMeasure(value)).mIndex); } }
		public double CentreOfGravityInY { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "CentreOfGravityInY", "", new IfcLengthMeasure(value)).mIndex); } }
		public double ShearCentreZ { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearCentreZ", "", new IfcLengthMeasure(value)).mIndex); } }
		public double ShearCentreY { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearCentreY", "", new IfcLengthMeasure(value)).mIndex); } }
		public double MomentOfInertiaY { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MomentOfInertiaY", "", new IfcMomentOfInertiaMeasure(value)).mIndex); } }
		public double MomentOfInertiaZ { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MomentOfInertiaZ", "", new IfcMomentOfInertiaMeasure(value)).mIndex); } }
		public double MomentOfInertiaYZ { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MomentOfInertiaYZ", "", new IfcMomentOfInertiaMeasure(value)).mIndex); } }
		public double TorsionalConstantX { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "TorsionalConstantX", "", new IfcMomentOfInertiaMeasure(value)).mIndex); } }
		public double WarpingConstant { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "WarpingConstant", "", new IfcWarpingConstantMeasure(value)).mIndex); } }
		public double ShearDeformationAreaZ { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearDeformationAreaZ", "", new IfcAreaMeasure(value)).mIndex); } }
		public double ShearDeformationAreaY { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearDeformationAreaY", "", new IfcAreaMeasure(value)).mIndex); } }
		public double MaximumSectionModulusY { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MaximumSectionModulusY", "", new IfcSectionModulusMeasure(value)).mIndex); } }
		public double MinimumSectionModulusY { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MinimumSectionModulusY", "", new IfcSectionModulusMeasure(value)).mIndex); } }
		public double MaximumSectionModulusZ { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MaximumSectionModulusZ", "", new IfcSectionModulusMeasure(value)).mIndex); } }
		public double MinimumSectionModulusZ { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "MinimumSectionModulusZ", "", new IfcSectionModulusMeasure(value)).mIndex); } }
		public double TorsionalSectionModulus { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "TorsionalSectionModulus", "", new IfcSectionModulusMeasure(value)).mIndex); } }
		public double ShearAreaZ { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearAreaZ", "", new IfcAreaMeasure(value)).mIndex); } }
		public double ShearAreaY { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearAreaY", "", new IfcAreaMeasure(value)).mIndex); } }
		public double PlasticShapeFactorY { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "PlasticShapeFactorY", "", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public double PlasticShapeFactorZ { set { mExtendedProperties.Add(new IfcPropertySingleValue(mDatabase, "PlasticShapeFactorZ", "", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public Pset_ProfileMechanical(IfcProfileDef profileDef) : base("Pset_ProfileMechanical", profileDef) { Description = profileDef.Name; }
	}
	public partial class Pset_PumpOccurrence : IfcPropertySet
	{
		public double ImpellerDiameter { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ImpellerDiameter", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_PumpBaseType BaseType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "BaseType", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_PumpDriveConnectionType DriveConnectionType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "DriveConnectionType", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_PumpOccurrence(IfcPump pump) : base(pump.mDatabase, "Pset_PumpOccurrence") { Description = pump.Name; DefinesOccurrence.assign(pump); }
	}
	public partial class Pset_PumpTypeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public IfcPropertyBoundedValue<IfcMassFlowRateMeasure> FlowRateRange { set { value.Name = "FlowRateRange"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcPressureMeasure> FlowResistanceRange { set { value.Name = "FlowResistanceRange"; mHasProperties.Add(value.mIndex); } }
		public double ConnectionSize { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConnectionSize", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> TemperatureRange { set { value.Name = "TemperatureRange"; mHasProperties.Add(value.mIndex); } }
		public double NetPositiveSuctionHead { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NetPositiveSuctionHead", new IfcPressureMeasure(value)).mIndex); } }
		public double NominalRotationSpeed { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalRotationSpeed", new IfcRotationalFrequencyMeasure(value)).mIndex); } }
		public Pset_PumpTypeCommon(IfcPump pump) : base(pump.mDatabase, "Pset_PumpTypeCommon") { Description = pump.Name; DefinesOccurrence.assign(pump); }
		public Pset_PumpTypeCommon(IfcPumpType pumpType) : base(pumpType.mDatabase, "Pset_PumpTypeCommon") { Description = pumpType.Name; pumpType.AddPropertySet(this); }
	}
	public partial class Pset_SlabCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public string AcousticRating { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "AcousticRating", new IfcLabel(value)).mIndex); } }
		public string FireRating { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "FireRating", new IfcLabel(value)).mIndex); } }
		public bool Combustible { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Combustible", new IfcBoolean(value)).mIndex); } }
		public string SurfaceSpreadOfFlame { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "SurfaceSpreadOfFlame", new IfcLabel(value)).mIndex); } }
		public double ThermalTransmittance { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ThermalTransmittance", new IfcThermalTransmittanceMeasure(value)).mIndex); } }
		public bool IsExternal { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value)).mIndex); } }
		public bool LoadBearing { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "LoadBearing", new IfcBoolean(value)).mIndex); } }
		public bool Compartmentation { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Compartmentation", new IfcBoolean(value)).mIndex); } }
		public double PitchAngle { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "PitchAngle", new IfcPlaneAngleMeasure(value)).mIndex); } }
		public Pset_SlabCommon(IfcSlab slab) : base(slab.mDatabase, "Pset_SlabCommon") { DefinesOccurrence.assign(slab); }
		public Pset_SlabCommon(IfcSlabType slabType) : base(slabType.mDatabase, "Pset_SlabCommon") { Description = slabType.Name; slabType.AddPropertySet(this); }
	}
	public partial class Pset_SpaceHeaterTypeConvector : IfcPropertySet
	{
		public PEnum_SpaceHeaterConvectorType ConvectorType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "ConvectorType", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_SpaceHeaterTypeConvector(IfcSpaceHeater spaceHeater) : base(spaceHeater.mDatabase, "Pset_SpaceHeaterTypeConvector") { Description = spaceHeater.Name; DefinesOccurrence.assign(spaceHeater); }
		public Pset_SpaceHeaterTypeConvector(IfcSpaceHeaterType spaceHeaterType) : base(spaceHeaterType.mDatabase, "Pset_SpaceHeaterTypeConvector") { Description = spaceHeaterType.Name; spaceHeaterType.AddPropertySet(this); }
	}
	public partial class Pset_SpaceHeaterTypeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_SpaceHeaterPlacementType PlacementType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "PlacementType", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_SpaceHeaterTemperatureClassification TemperatureClassification { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "TemperatureClassification", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_SpaceHeaterHeatTransferDimension HeatTransferDimension { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "HeatTransferDimension", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_SpaceHeaterHeatTransferMedium HeatTransferMedium { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "HeatTransferMedium", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_EnergySource EnergySource { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "EnergySource", new IfcLabel(value.ToString())).mIndex); } }
		public double BodyMass { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "BodyMass", new IfcMassMeasure(value)).mIndex); } }
		public double ThermalMassHeatCapacity { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ThermalMassHeatCapacity", new IfcReal(value)).mIndex); } }
		public double OutputCapacity { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "OutputCapacity", new IfcPowerMeasure(value)).mIndex); } }
		public double ThermalEfficiency { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ThermalEfficiency", new IfcNormalisedRatioMeasure(value)).mIndex); } }
		public int NumberOfPanels { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NumberOfPanels", new IfcInteger(value)).mIndex); } }
		public int NumberOfSections { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NumberOfSections", new IfcInteger(value)).mIndex); } }
		public Pset_SpaceHeaterTypeCommon(IfcSpaceHeater spaceHeater) : base(spaceHeater.mDatabase, "Pset_SpaceHeaterTypeCommon") { Description = spaceHeater.Name; DefinesOccurrence.assign(spaceHeater); }
		public Pset_SpaceHeaterTypeCommon(IfcSpaceHeaterType spaceHeaterType) : base(spaceHeaterType.mDatabase, "Pset_SpaceHeaterTypeCommon") { Description = spaceHeaterType.Name; spaceHeaterType.AddPropertySet(this); }
	}
	public partial class Pset_SpaceHeaterTypeRadiator : IfcPropertySet
	{
		public PEnum_SpaceHeaterRadiatorType RadiatorType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "RadiatorType", new IfcLabel(value.ToString())).mIndex); } }
		public double TubingLength { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "TubingLength", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double WaterContent { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "WaterContent", new IfcMassMeasure(value)).mIndex); } }
		public Pset_SpaceHeaterTypeRadiator(IfcSpaceHeater spaceHeater) : base(spaceHeater.mDatabase, "Pset_SpaceHeaterTypeRadiator") { Description = spaceHeater.Name; DefinesOccurrence.assign(spaceHeater); }
		public Pset_SpaceHeaterTypeRadiator(IfcSpaceHeaterType spaceHeaterType) : base(spaceHeaterType.mDatabase, "Pset_SpaceHeaterTypeRadiator") { Description = spaceHeaterType.Name; spaceHeaterType.AddPropertySet(this); }
	}
	public partial class Pset_UnitaryEquipmentTypeAirHandler : IfcPropertySet
	{
		public PEnum_AirHandlerConstruction AirHandlerConstruction { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "AirHandlerConstruction", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_AirHandlerFanCoilArrangement AirHandlerFanCoilArrangement { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "AirHandlerFanCoilArrangement", new IfcLabel(value.ToString())).mIndex); } }
		public bool DualDeck { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "DualDeck", new IfcBoolean(value)).mIndex); } }
		public Pset_UnitaryEquipmentTypeAirHandler(IfcUnitaryEquipment unitaryEquipment) : base(unitaryEquipment.mDatabase, "Pset_UnitaryEquipmentTypeAirHandler") { Description = unitaryEquipment.Name; DefinesOccurrence.assign(unitaryEquipment); }
		public Pset_UnitaryEquipmentTypeAirHandler(IfcUnitaryEquipmentType unitaryEquipmentType) : base(unitaryEquipmentType.mDatabase, "Pset_UnitaryEquipmentTypeAirHandler") { Description = unitaryEquipmentType.Name; unitaryEquipmentType.AddPropertySet(this); }
	}
	public partial class Pset_UnitaryEquipmentTypeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_UnitaryEquipmentTypeCommon(IfcUnitaryEquipment unitaryEquipment) : base(unitaryEquipment.mDatabase, "Pset_UnitaryEquipmentTypeCommonr") { Description = unitaryEquipment.Name; DefinesOccurrence.assign(unitaryEquipment); }
		public Pset_UnitaryEquipmentTypeCommon(IfcUnitaryEquipmentType unitaryEquipmentType) : base(unitaryEquipmentType.mDatabase, "Pset_UnitaryEquipmentTypeCommon") { Description = unitaryEquipmentType.Name; unitaryEquipmentType.AddPropertySet(this); }
	}
	public partial class Pset_ValveTypeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ValvePattern ValvePattern { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "ValvePattern", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_ValveOperation ValveOperation { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "ValveOperation", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_ValveMechanism ValveMechanism { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "ValveMechanism", new IfcLabel(value.ToString())).mIndex); } }
		public double Size { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Size", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double TestPressure { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "TestPressure", new IfcPressureMeasure(value)).mIndex); } }
		public double WorkingPressure { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "WorkingPressure", new IfcPressureMeasure(value)).mIndex); } }
		public double FlowCoefficient { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "FlowCoefficient", new IfcReal(value)).mIndex); } }
		public double CloseOffRating { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "CloseOffRating", new IfcPressureMeasure(value)).mIndex); } }
		public Pset_ValveTypeCommon(IfcValve valve) : base(valve.mDatabase, "Pset_ValveTypeCommon") { Description = valve.Name; DefinesOccurrence.assign(valve); }
		public Pset_ValveTypeCommon(IfcValveType valveType) : base(valveType.mDatabase, "Pset_ValveTypeCommon") { Description = valveType.Name; valveType.AddPropertySet(this); }
	}
	public partial class Pset_WallCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public string AcousticRating { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "AcousticRating", new IfcLabel(value)).mIndex); } }
		public string FireRating { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "FireRating", new IfcLabel(value)).mIndex); } }
		public bool Combustible { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Combustible", new IfcBoolean(value)).mIndex); } }
		public string SurfaceSpreadOfFlame { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "SurfaceSpreadOfFlame", new IfcLabel(value)).mIndex); } }
		public double ThermalTransmittance { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ThermalTransmittance", new IfcThermalTransmittanceMeasure(value)).mIndex); } }
		public bool IsExternal { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value)).mIndex); } }
		public bool ExtendToStructure { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ExtendToStructure", new IfcBoolean(value)).mIndex); } }
		public bool LoadBearing { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "LoadBearing", new IfcBoolean(value)).mIndex); } }
		public bool Compartmentation { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Compartmentation", new IfcBoolean(value)).mIndex); } }
		public Pset_WallCommon(IfcWall wall) : base(wall.mDatabase, "Pset_WallCommon") {  DefinesOccurrence.assign(wall); }
		public Pset_WallCommon(IfcWallType wallType) : base(wallType.mDatabase, "Pset_WallCommon") { Description = wallType.Name;  wallType.AddPropertySet(this); }
	}

}
