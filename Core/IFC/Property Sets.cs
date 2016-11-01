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
		public Pset_AirTerminalTypeCommon(IfcAirTerminal airTerminal) : base(airTerminal.mDatabase, "Pset_AirTerminalTypeCommon") { Description = airTerminal.Name; DefinesOccurrence.Assign(airTerminal); }
		public Pset_AirTerminalTypeCommon(IfcAirTerminalType airTerminalType) : base(airTerminalType.mDatabase, "Pset_AirTerminalTypeCommon") { Description = airTerminalType.Name; airTerminalType.AddPropertySet(this); }
	}
	public partial class Pset_AirTerminalOccurrence : IfcPropertySet
	{
		public PEnum_AirTerminalAirflowType AirflowType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "AirflowType", new IfcLabel(value.ToString())).mIndex); } }
		public double AirFlowRate { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "AirFlowRate", new IfcVolumetricFlowRateMeasure(value)).mIndex); } }
		public PEnum_AirTerminalLocation Location { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Location", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_AirTerminalOccurrence(IfcAirTerminal valve) : base(valve.mDatabase, "Pset_AirTerminalOccurrence") { Description = valve.Name; DefinesOccurrence.Assign(valve); }
		public Pset_AirTerminalOccurrence(IfcAirTerminalType valveType) : base(valveType.mDatabase, "Pset_AirTerminalOccurrence") { Description = valveType.Name; valveType.AddPropertySet(this); }
	}
	public partial class Pset_BoilerTypeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public double PressureRating { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "PressureRating", new IfcPressureMeasure(value)).mIndex); } }
		public PEnum_BoilerOperatingMode OperatingMode { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "OperatingMode", new IfcLabel(value.ToString())).mIndex); } }
		public double HeatTransferSurfaceArea { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "HeatTransferSurfaceArea", new IfcAreaMeasure(value)).mIndex); } }
		public double NominalPartLoadRatio { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalPartLoadRatio", new IfcReal(value)).mIndex); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> WaterInletTemperatureRange { set { value.Name = "WaterInletTemperatureRange"; mHasProperties.Add(value.mIndex); } }
		public double WaterStorageCapacity { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "WaterStorageCapacity", new IfcVolumeMeasure(value)).mIndex); } }
		public bool IsWaterStorageHeater { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "IsWaterStorageHeater", new IfcBoolean(value)).mIndex); } }
		public IfcPropertyTableValue<IfcPositiveRatioMeasure, IfcNormalisedRatioMeasure> PartialLoadEfficiencyCurves { set { value.Name = "PartialLoadEfficiencyCurves"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> OutletTemperatureRange { set { value.Name = "OutletTemperatureRange"; mHasProperties.Add(value.mIndex); } }
		public double NominalEnergyConsumption { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalEnergyConsumption", new IfcPowerMeasure(value)).mIndex); } }
		public PEnum_EnergySource EnergySource { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "EnergySource", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_BoilerTypeCommon(IfcBoiler boiler) : base(boiler.mDatabase, "Pset_BoilerTypeCommon") { Description = boiler.Name; DefinesOccurrence.Assign(boiler); }
		public Pset_BoilerTypeCommon(IfcBoilerType boilerType) : base(boilerType.mDatabase, "Pset_BoilerTypeCommon") { Description = boilerType.Name; boilerType.AddPropertySet(this); }
	}
	public partial class Pset_BoilerTypeSteam : IfcPropertySet
	{
		public string MaximumOutletPressure { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "MaximumOutletPressure", new IfcLabel(value)).mIndex); } }
		public IfcPropertyTableValue<IfcThermodynamicTemperatureMeasure, IfcNormalisedRatioMeasure> NominalEfficiency { set { value.Name = "NominalEfficiency"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyTableValue<IfcThermodynamicTemperatureMeasure, IfcEnergyMeasure> HeatOutput { set { value.Name = "HeatOutput"; mHasProperties.Add(value.mIndex); } }
		public Pset_BoilerTypeSteam(IfcBoiler boiler) : base(boiler.mDatabase, "Pset_BoilerTypeWater") { Description = boiler.Name; DefinesOccurrence.Assign(boiler); }
		public Pset_BoilerTypeSteam(IfcBoilerType boilerType) : base(boilerType.mDatabase, "Pset_BoilerTypeWater") { Description = boilerType.Name; boilerType.AddPropertySet(this); }
	}
	public partial class Pset_BoilerTypeWater : IfcPropertySet
	{
		public IfcPropertyTableValue<IfcThermodynamicTemperatureMeasure, IfcNormalisedRatioMeasure> NominalEfficiency { set { value.Name = "NominalEfficiency"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyTableValue<IfcThermodynamicTemperatureMeasure, IfcEnergyMeasure> HeatOutput { set { value.Name = "HeatOutput"; mHasProperties.Add(value.mIndex); } }
		public Pset_BoilerTypeWater(IfcBoiler boiler) : base(boiler.mDatabase, "Pset_BoilerTypeWater") { Description = boiler.Name; DefinesOccurrence.Assign(boiler); }
		public Pset_BoilerTypeWater(IfcBoilerType boilerType) : base(boilerType.mDatabase, "Pset_BoilerTypeWater") { Description = boilerType.Name; boilerType.AddPropertySet(this); }
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
		public Pset_BuildingCommon(IfcBuilding building) : base(building.mDatabase, "Pset_BuildingCommon") { Description = building.Name; DefinesOccurrence.Assign(building);  }
	}
	public partial class Pset_typeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_EnergySource EnergySource { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "EnergySource", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_typeCommon(IfcBurner instance) : base(instance.mDatabase, "Pset_typeCommon") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_typeCommon(IfcBurnerType type) : base(type.mDatabase, "Pset_typeCommon") { Description = type.Name; type.AddPropertySet(this); }
	}
	public partial class Pset_ConcreteElementGeneral : IfcPropertySet
	{
		public string ConstructionMethod { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConstructionMethod", new IfcLabel(value)).mIndex); } }
		public string StructuralClass { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "StructuralClass", new IfcLabel(value)).mIndex); } }
		public string StrengthClass { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "StrengthClass", new IfcLabel(value)).mIndex); } }
		public string ExposureClass { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ExposureClass", new IfcLabel(value)).mIndex); } }
		public double ReinforcementVolumeRatio { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ReinforcementVolumeRatio", new IfcMassDensityMeasure(value)).mIndex); } }
		public double ReinforcementAreaRatio { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ReinforcementAreaRatio", new IfcAreaDensityMeasure(value)).mIndex); } }
		public string DimensionalAccuracyClass { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "DimensionalAccuracyClass", new IfcLabel(value)).mIndex); } }
		public string ConstructionToleranceClass { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConstructionToleranceClass", new IfcLabel(value)).mIndex); } }
		public double ConcreteCover { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConcreteCover", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double ConcreteCoverAtMainBars { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConcreteCoverAtMainBars", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double ConcreteCoverAtLinks { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConcreteCoverAtLinks", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public string ReinforcementStrengthClass { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ReinforcementStrengthClass", new IfcLabel(value)).mIndex); } }
		public Pset_ConcreteElementGeneral(IfcBeam instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcBeamType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcBuildingElementProxy instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcBuildingElementProxyType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcChimney instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcChimneyType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcColumn instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcColumnType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcFooting instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcFootingType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcMember instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcMemberType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcPile instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcPileType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcPlate instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcPlateType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcRailing instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcRailingType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcRamp instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcRampType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcRampFlight instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcRampFlightType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcRoof instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcRoofType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcSlab instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcSlabType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcStair instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcStairType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcStairFlight instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcStairFlightType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcWall instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcWallType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_ConcreteElementGeneral(IfcCivilElement instance) : base(instance.mDatabase, "Pset_ConcreteElementGeneral") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Pset_ConcreteElementGeneral(IfcCivilElementType type) : base(type.mDatabase, "Pset_ConcreteElementGeneral") { Description = type.Name; type.AddPropertySet(this); }
	}
	
	public partial class Pset_DistributionPortCommon : IfcPropertySet
	{
		public int PortNumber { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "PortNumber", new IfcInteger(value)).mIndex); } }
		public string ColorCode { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ColorCode", new IfcLabel(value)).mIndex); } }
		public Pset_DistributionPortCommon(IfcDistributionPort port) : base(port.mDatabase, "Pset_DistributionPortCommon") { Description = port.Name; DefinesOccurrence.Assign(port); }
	}

	public partial class Pset_DistributionPortTypeCable : IfcPropertySet
	{
		public PEnum_DistributionPortElectricalType ConnectionType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "ConnectionType", new IfcLabel(value.ToString())).mIndex); } }
		public string ConnectionSubType { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConnectionSubType", new IfcLabel(value)).mIndex); } }
		public PEnum_DistributionPortGender ConnectionGender { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "ConnectionGender", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_ConductorFunctionEnum ConductorFunction { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "ConductorFunction", new IfcLabel(value.ToString())).mIndex); } }
		public double CurrentContent3rdHarmonic { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "CurrentContent3rdHarmonic", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public IfcPropertyBoundedValue<IfcElectricCurrentMeasure> Current { set { value.Name = "Current"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcElectricVoltageMeasure> Voltage { set { value.Name = "Voltage"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcPowerMeasure> Power { set { value.Name = "Power"; mHasProperties.Add(value.mIndex); } }
		//public IfcPropertyListValue<IfcIdentifier> Protocols { set { value.Name = "Protocols"; mHasProperties.Add(value.mIndex); } }
		public Pset_DistributionPortTypeCable(IfcDistributionPort port) : base(port.mDatabase, "Pset_DistributionPortTypeCable") { DefinesOccurrence.Assign(port); }
	}
	public partial class Pset_DistributionPortTypeDuct : IfcPropertySet
	{
		public PEnum_DuctConnectionType ConnectionType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "ConnectionType", new IfcLabel(value.ToString())).mIndex); } }
		public string ConnectionSubType { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConnectionSubType", new IfcLabel(value)).mIndex); } }
		public double NominalWidth { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalWidth", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double NominalHeight { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalHeight", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double NominalThickness { set {  mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalThickness", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> DryBulbTemperature { set { value.Name = "DryBulbTemperature"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> WetBulbTemperature { set { value.Name = "WetBulbTemperature"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcVolumetricFlowRateMeasure> VolumetricFlowRate { set { value.Name = "VolumetricFlowRate"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcLinearVelocityMeasure> Velocity { set { value.Name = "Velocity"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcPressureMeasure> Pressure { set { value.Name = "Pressure"; mHasProperties.Add(value.mIndex); } }
		public Pset_DistributionPortTypeDuct(IfcDistributionPort port) : base(port.mDatabase, "Pset_DistributionPortTypeDuct") { DefinesOccurrence.Assign(port); }
	}
	public partial class Pset_DistributionPortTypePipe : IfcPropertySet
	{
		public PEnum_PipeEndStyleTreatment ConnectionType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "ConnectionType", new IfcLabel(value.ToString())).mIndex); } }
		public string ConnectionSubType { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConnectionSubType", new IfcLabel(value)).mIndex); } }
		public double NominalDiameter { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalDiameter", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double InnerDiameter { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "InnerDiameter", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double OuterDiameter { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "OuterDiameter", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> Temperature { set { value.Name = "Temperature"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcVolumetricFlowRateMeasure> VolumetricFlowRate { set { value.Name = "VolumetricFlowRate"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcMassFlowRateMeasure> MassFlowRate { set { value.Name = "MassFlowRate"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcLinearVelocityMeasure> Velocity { set { value.Name = "Velocity"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcPressureMeasure> Pressure { set { value.Name = "Pressure"; mHasProperties.Add(value.mIndex); } }
		public Pset_DistributionPortTypePipe(IfcDistributionPort port) : base(port.mDatabase, "Pset_DistributionPortTypePipe") { DefinesOccurrence.Assign(port); }
	}

	public partial class Pset_LandRegistration : IfcPropertySet
	{
		public string LandID { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "LandID", new IfcIdentifier(value)).mIndex); } }
		public bool IsPermanentID { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "IsPermanentID", new IfcBoolean(value)).mIndex); } }
		public string LandTitleID { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "LandTitleID", new IfcIdentifier(value)).mIndex); } }
		public Pset_LandRegistration(IfcSite instance) : base(instance.mDatabase, "Pset_LandRegistration") { DefinesOccurrence.Assign(instance); }
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
		public Pset_LightFixtureTypeCommon(IfcLightFixture lightFixture) : base(lightFixture.mDatabase, "Pset_LightFixtureTypeCommon") { Description = lightFixture.Name; DefinesOccurrence.Assign(lightFixture); }
		public Pset_LightFixtureTypeCommon(IfcLightFixtureType lightFixtureType) : base(lightFixtureType.mDatabase, "Pset_LightFixtureTypeCommon") { Description = lightFixtureType.Name; lightFixtureType.AddPropertySet(this); }
	}
	public partial class Pset_ManufacturerOccurrence : IfcPropertySet
	{
		//public IfcDate AcquisitionDate { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "AcquisitionDate", value; } }
		public string BarCode { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "BarCode", new IfcIdentifier(value)).mIndex); } }
		public string ModelReference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ModelReference", new IfcIdentifier(value)).mIndex); } }
		public string SerialNumber { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "SerialNumber", new IfcIdentifier(value)).mIndex); } }
		public string BatchReference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "BatchReference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_AssemblyPlace AssemblyPlace { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "AssemblyPlace", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_ManufacturerOccurrence(IfcElement element) : base(element.mDatabase, "Pset_ManufacturerOccurrence") { DefinesOccurrence.Assign(element); }
		public Pset_ManufacturerOccurrence(IfcElementType type) : base(type.mDatabase, "Pset_ManufacturerOccurrence") { Description = type.Name; type.AddPropertySet(this); }
	}
	public partial class Pset_ManufacturerTypeInformation : IfcPropertySet
	{
		public string GlobalTradeItemNumber { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "GlobalTradeItemNumber", new IfcIdentifier(value)).mIndex); } }
		public string ArticleNumber { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ArticleNumber", new IfcIdentifier(value)).mIndex); } }
		public string ModelReference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ModelReference", new IfcIdentifier(value)).mIndex); } }
		public string ModelLabel { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ModelLabel", new IfcLabel(value)).mIndex); } }
		public string Manufacturer { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Manufacturer", new IfcLabel(value)).mIndex); } }
		public string ProductionYear { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ProductionYear", new IfcLabel(value)).mIndex); } }
		public PEnum_AssemblyPlace AssemblyPlace { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "AssemblyPlace", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_ManufacturerTypeInformation(IfcElement element) : base(element.mDatabase, "Pset_ManufacturerTypeInformation") { DefinesOccurrence.Assign(element); }
		public Pset_ManufacturerTypeInformation(IfcElementType type) : base(type.mDatabase, "Pset_ManufacturerTypeInformation") { Description = type.Name; type.AddPropertySet(this); }
	}
	public partial class Pset_MaterialCommon : IfcMaterialProperties
	{
		public double MolecularWeight { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MolecularWeight", new IfcMolecularWeightMeasure(value)).mIndex); } }
		public double Porosity { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "Porosity", new IfcNormalisedRatioMeasure(value)).mIndex); } }
		public double MassDensity { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MassDensity", new IfcMassDensityMeasure(value)).mIndex); } }
		public Pset_MaterialCommon(IfcMaterialDefinition material) : base( "Pset_MaterialCommon",material) { Description = material.Name; }
	}
	public partial class Pset_MaterialConcrete : IfcMaterialProperties
	{
		public double CompressiveStrength { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "CompressiveStrength", new IfcPressureMeasure(value)).mIndex); } }
		public double MaxAggregateSize { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MaxAggregateSize", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public string AdmixturesDescription { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "AdmixturesDescription", new IfcText(value)).mIndex); } }
		public string Workability { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "Workability", new IfcText(value)).mIndex); } }
		public string WaterImpermeability { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "WaterImpermeability", new IfcText(value)).mIndex); } }
		public double ProtectivePoreRatio { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "ProtectivePoreRatio", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public Pset_MaterialConcrete(IfcMaterialDefinition material) : base("Pset_MaterialConcrete", material) { Description = material.Name; }
	}
	public partial class Pset_MaterialMechanical : IfcMaterialProperties
	{
		public double DynamicViscosity { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "DynamicViscosity", new IfcDynamicViscosityMeasure(value)).mIndex); } }
		public double YoungModulus { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "YoungModulus", new IfcModulusOfElasticityMeasure(value)).mIndex); } }
		public double ShearModulus { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearModulus", new IfcModulusOfElasticityMeasure(value)).mIndex); } }
		public double PoissonRatio { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "PoissonRatio", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public double ThermalExpansionCoefficient { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "ThermalExpansionCoefficient", new IfcThermalExpansionCoefficientMeasure(value)).mIndex); } }
		public Pset_MaterialMechanical(IfcMaterialDefinition material) : base("Pset_MaterialMechanical",material) { Description = material.Name; }
	}
	public partial class Pset_MaterialSteel : IfcMaterialProperties
	{
		public double YieldStress { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "YieldStress", new IfcPressureMeasure(value)).mIndex); } }
		public double UltimateStress { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "UltimateStress", new IfcPressureMeasure(value)).mIndex); } }
		public double UltimateStrain { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "UltimateStrain", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public double HardeningModule { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "HardeningModule", new IfcModulusOfElasticityMeasure(value)).mIndex); } }
		public double ProportionalStress { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "ProportionalStress", new IfcPressureMeasure(value)).mIndex); } }
		public double PlasticStrain { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "PlasticStrain", new IfcPositiveRatioMeasure(value)).mIndex); } }
		//public double Relaxations { set { if (value > 0) mProperties.Add(new ifctableValue IfcPropertySingleValue(mDatabase, "Relaxations", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public Pset_MaterialSteel(IfcMaterialDefinition material) : base("Pset_MaterialSteel", material) { Description = material.Name; }
	}
	public partial class Pset_PipeFittingOccurrence : IfcPropertySet
	{
		public double InteriorRoughnessCoefficient { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "InteriorRoughnessCoefficient", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public string Color { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Color", new IfcLabel(value)).mIndex); } }
		public Pset_PipeFittingOccurrence(IfcPipeFitting pipeFitting) : base(pipeFitting.mDatabase, "Pset_PipeFittingOccurrence") { Description = pipeFitting.Name; DefinesOccurrence.Assign(pipeFitting); }
	}
	public partial class Pset_PipeFittingTypeBend : IfcPropertySet
	{
		public double BendAngle { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "BendAngle", new IfcPositivePlaneAngleMeasure(value)).mIndex); } }
		public double BendRadius { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "BendRadius", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public Pset_PipeFittingTypeBend(IfcPipeFitting pipeFitting) : base(pipeFitting.mDatabase, "Pset_PipeFittingTypeBend") { Description = pipeFitting.Name; DefinesOccurrence.Assign(pipeFitting); }
		public Pset_PipeFittingTypeBend(IfcPipeFittingType pipeFittingType) : base(pipeFittingType.mDatabase, "Pset_PipeFittingTypeBend") { Description = pipeFittingType.Name; pipeFittingType.AddPropertySet(this); }
	}
	public partial class Pset_PipeFittingTypeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public double PressureClass { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "PressureClass", new IfcPressureMeasure(value)).mIndex); } }
		public IfcPropertyBoundedValue<IfcPressureMeasure> PressureRange { set { value.Name = "PressureRange"; mHasProperties.Add(value.mIndex); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> TemperatureRange { set { value.Name = "TemperatureRange"; mHasProperties.Add(value.mIndex); } }
		public Pset_PipeFittingTypeCommon(IfcPipeFitting pipeFitting) : base(pipeFitting.mDatabase, "Pset_PipeFittingTypeCommon") { Description = pipeFitting.Name; DefinesOccurrence.Assign(pipeFitting); }
		public Pset_PipeFittingTypeCommon(IfcPipeFittingType pipeFittingType) : base(pipeFittingType.mDatabase, "Pset_PipeFittingTypeCommon") { Description = pipeFittingType.Name; pipeFittingType.AddPropertySet(this); }
	}
	public partial class Pset_PipeFittingTypeJunction : IfcPropertySet
	{
		public PEnum_PipeFittingJunctionType JunctionType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "JunctionType", new IfcLabel(value.ToString())).mIndex); } }
		public double JunctionLeftAngle { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "JunctionLeftAngle", new IfcPositivePlaneAngleMeasure(value)).mIndex); } }
		public double JunctionLeftRadius { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "JunctionLeftRadius", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double JunctionRightAngle { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "JunctionRightAngle", new IfcPositivePlaneAngleMeasure(value)).mIndex); } }
		public double JunctionRightRadius { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "JunctionRightRadius", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public Pset_PipeFittingTypeJunction(IfcPipeFitting pipeFitting) : base(pipeFitting.mDatabase, "Pset_PipeFittingTypeJunction") { Description = pipeFitting.Name; DefinesOccurrence.Assign(pipeFitting); }
		public Pset_PipeFittingTypeJunction(IfcPipeFittingType pipeFittingType) : base(pipeFittingType.mDatabase, "Pset_PipeFittingTypeJunction") { Description = pipeFittingType.Name; pipeFittingType.AddPropertySet(this); }
	}
	public partial class Pset_ProfileMechanical : IfcProfileProperties
	{
		public double MassPerLength { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MassPerLength", "", new IfcMassPerLengthMeasure(value)).mIndex); } }
		public double CrossSectionArea { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "CrossSectionArea", "", new IfcAreaMeasure(value)).mIndex); } }
		public double Perimeter { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "Perimeter", "", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double MinimumPlateThickness { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MinimumPlateThickness", "", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double MaximumPlateThickness { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MaximumPlateThickness", "", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double CentreOfGravityInX { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "CentreOfGravityInX", "", new IfcLengthMeasure(value)).mIndex); } }
		public double CentreOfGravityInY { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "CentreOfGravityInY", "", new IfcLengthMeasure(value)).mIndex); } }
		public double ShearCentreZ { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearCentreZ", "", new IfcLengthMeasure(value)).mIndex); } }
		public double ShearCentreY { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearCentreY", "", new IfcLengthMeasure(value)).mIndex); } }
		public double MomentOfInertiaY { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MomentOfInertiaY", "", new IfcMomentOfInertiaMeasure(value)).mIndex); } }
		public double MomentOfInertiaZ { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MomentOfInertiaZ", "", new IfcMomentOfInertiaMeasure(value)).mIndex); } }
		public double MomentOfInertiaYZ { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MomentOfInertiaYZ", "", new IfcMomentOfInertiaMeasure(value)).mIndex); } }
		public double TorsionalConstantX { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "TorsionalConstantX", "", new IfcMomentOfInertiaMeasure(value)).mIndex); } }
		public double WarpingConstant { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "WarpingConstant", "", new IfcWarpingConstantMeasure(value)).mIndex); } }
		public double ShearDeformationAreaZ { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearDeformationAreaZ", "", new IfcAreaMeasure(value)).mIndex); } }
		public double ShearDeformationAreaY { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearDeformationAreaY", "", new IfcAreaMeasure(value)).mIndex); } }
		public double MaximumSectionModulusY { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MaximumSectionModulusY", "", new IfcSectionModulusMeasure(value)).mIndex); } }
		public double MinimumSectionModulusY { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MinimumSectionModulusY", "", new IfcSectionModulusMeasure(value)).mIndex); } }
		public double MaximumSectionModulusZ { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MaximumSectionModulusZ", "", new IfcSectionModulusMeasure(value)).mIndex); } }
		public double MinimumSectionModulusZ { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "MinimumSectionModulusZ", "", new IfcSectionModulusMeasure(value)).mIndex); } }
		public double TorsionalSectionModulus { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "TorsionalSectionModulus", "", new IfcSectionModulusMeasure(value)).mIndex); } }
		public double ShearAreaZ { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearAreaZ", "", new IfcAreaMeasure(value)).mIndex); } }
		public double ShearAreaY { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "ShearAreaY", "", new IfcAreaMeasure(value)).mIndex); } }
		public double PlasticShapeFactorY { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "PlasticShapeFactorY", "", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public double PlasticShapeFactorZ { set { mProperties.Add(new IfcPropertySingleValue(mDatabase, "PlasticShapeFactorZ", "", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public Pset_ProfileMechanical(IfcProfileDef profileDef) : base("Pset_ProfileMechanical", profileDef) { Description = profileDef.Name; }
	}
	public partial class Pset_PropertyAgreement : IfcPropertySet
	{
		public PEnum_PropertyAgreementType AgreementType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "AgreementType", new IfcLabel(value.ToString())).mIndex); } }
		public string Identifier { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Identifier", new IfcIdentifier(value)).mIndex); } }
		public string Version { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Version", new IfcLabel(value)).mIndex); } }
		//public DateTime VersionDate { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "VersionDate", new IfcDate(value)).mIndex); } }
		public string PropertyName { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "PropertyName", new IfcLabel(value)).mIndex); } }
		//public DateTime CommencementDate { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "CommencementDate", new IfcDate(value)).mIndex); } }
		//public DateTime TerminationDate { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "TerminationDate", new IfcDate(value)).mIndex); } }
		//public IfcDuration Duration { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Duration", value).mIndex); } }
		public string Options { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Options", new IfcText(value)).mIndex); } }
		public string ConditionCommencement { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConditionCommencement", new IfcText(value)).mIndex); } }
		public string Restrictions { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Restrictions", new IfcText(value)).mIndex); } }
		public string ConditionTermination { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConditionTermination", new IfcText(value)).mIndex); } }
		public Pset_PropertyAgreement(IfcSpatialStructureElement instance) : base(instance.mDatabase, "Pset_PropertyAgreement") { DefinesOccurrence.Assign(instance); }
	}
	public partial class Pset_PumpOccurrence : IfcPropertySet
	{
		public double ImpellerDiameter { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ImpellerDiameter", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_PumpBaseType BaseType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "BaseType", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_PumpDriveConnectionType DriveConnectionType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "DriveConnectionType", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_PumpOccurrence(IfcPump pump) : base(pump.mDatabase, "Pset_PumpOccurrence") { Description = pump.Name; DefinesOccurrence.Assign(pump); }
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
		public Pset_PumpTypeCommon(IfcPump pump) : base(pump.mDatabase, "Pset_PumpTypeCommon") { Description = pump.Name; DefinesOccurrence.Assign(pump); }
		public Pset_PumpTypeCommon(IfcPumpType pumpType) : base(pumpType.mDatabase, "Pset_PumpTypeCommon") { Description = pumpType.Name; pumpType.AddPropertySet(this); }
	}
	public partial class Pset_SiteCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public double BuildableArea { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "BuildableArea", new IfcAreaMeasure(value)).mIndex); } }
		public double SiteCoverageRatio { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "SiteCoverageRatio", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public double FloorAreaRatio { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "FloorAreaRatio", new IfcPositiveRatioMeasure(value)).mIndex); } }
		public double BuildingHeightLimit { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "BuildingHeightLimit", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double TotalArea { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "TotalArea", new IfcAreaMeasure(value)).mIndex); } }
		public Pset_SiteCommon(IfcSite instance) : base(instance.mDatabase, "Pset_SiteCommon") { DefinesOccurrence.Assign(instance); }
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
		public Pset_SlabCommon(IfcSlab instance) : base(instance.mDatabase, "Pset_SlabCommon") { DefinesOccurrence.Assign(instance); }
		public Pset_SlabCommon(IfcSlabType type) : base(type.mDatabase, "Pset_SlabCommon") { Description = type.Name; type.AddPropertySet(this); }
	}
	public partial class Pset_SpaceCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public bool IsExternal { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value)).mIndex); } }
		public double GrossPlannedArea { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "GrossPlannedArea", new IfcAreaMeasure(value)).mIndex); } }
		public double NetPlannedArea { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NetPlannedArea", new IfcAreaMeasure(value)).mIndex); } }
		public bool PubliclyAccessible { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "PubliclyAccessible", new IfcBoolean(value)).mIndex); } }
		public bool HandicapAccessible { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "HandicapAccessible", new IfcBoolean(value)).mIndex); } }
		public Pset_SpaceCommon(IfcSpace instance) : base(instance.mDatabase, "Pset_SpaceCommon") { DefinesOccurrence.Assign(instance); }
		public Pset_SpaceCommon(IfcSpaceType type) : base(type.mDatabase, "Pset_SpaceCommon") { Description = type.Name; type.AddPropertySet(this); }
	}
	public partial class Pset_SpaceHeaterTypeConvector : IfcPropertySet
	{
		public PEnum_SpaceHeaterConvectorType ConvectorType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "ConvectorType", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_SpaceHeaterTypeConvector(IfcSpaceHeater spaceHeater) : base(spaceHeater.mDatabase, "Pset_SpaceHeaterTypeConvector") { Description = spaceHeater.Name; DefinesOccurrence.Assign(spaceHeater); }
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
		public Pset_SpaceHeaterTypeCommon(IfcSpaceHeater spaceHeater) : base(spaceHeater.mDatabase, "Pset_SpaceHeaterTypeCommon") { Description = spaceHeater.Name; DefinesOccurrence.Assign(spaceHeater); }
		public Pset_SpaceHeaterTypeCommon(IfcSpaceHeaterType spaceHeaterType) : base(spaceHeaterType.mDatabase, "Pset_SpaceHeaterTypeCommon") { Description = spaceHeaterType.Name; spaceHeaterType.AddPropertySet(this); }
	}
	public partial class Pset_SpaceHeaterTypeRadiator : IfcPropertySet
	{
		public PEnum_SpaceHeaterRadiatorType RadiatorType { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "RadiatorType", new IfcLabel(value.ToString())).mIndex); } }
		public double TubingLength { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "TubingLength", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double WaterContent { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "WaterContent", new IfcMassMeasure(value)).mIndex); } }
		public Pset_SpaceHeaterTypeRadiator(IfcSpaceHeater spaceHeater) : base(spaceHeater.mDatabase, "Pset_SpaceHeaterTypeRadiator") { Description = spaceHeater.Name; DefinesOccurrence.Assign(spaceHeater); }
		public Pset_SpaceHeaterTypeRadiator(IfcSpaceHeaterType spaceHeaterType) : base(spaceHeaterType.mDatabase, "Pset_SpaceHeaterTypeRadiator") { Description = spaceHeaterType.Name; spaceHeaterType.AddPropertySet(this); }
	}
	public partial class Pset_SpaceOccupancyRequirements : IfcPropertySet
	{
		public string OccupancyType { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "OccupancyType", new IfcLabel(value)).mIndex); } }
		public double OccupancyNumber { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "OccupancyNumber", new IfcCountMeasure(value)).mIndex); } }
		public double OccupancyNumberPeak { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "OccupancyNumberPeak", new IfcCountMeasure(value)).mIndex); } }
		public double OccupancyTimePerDay { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "OccupancyTimePerDay", new IfcTimeMeasure(value)).mIndex); } }
		public double AreaPerOccupant { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "AreaPerOccupant", new IfcAreaMeasure(value)).mIndex); } }
		public double MinimumHeadroom { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "MinimumHeadroom", new IfcLengthMeasure(value)).mIndex); } }
		public bool IsOutlookDesirable { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "IsOutlookDesirable", new IfcBoolean(value)).mIndex); } }
		public Pset_SpaceOccupancyRequirements(IfcSpace instance) : base(instance.mDatabase, "Pset_SpaceOccupancyRequirements") { DefinesOccurrence.Assign(instance); }
		public Pset_SpaceOccupancyRequirements(IfcSpaceType type) : base(type.mDatabase, "Pset_SpaceOccupancyRequirements") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_SpaceOccupancyRequirements(IfcSpatialZone instance) : base(instance.mDatabase, "Pset_SpaceOccupancyRequirements") { DefinesOccurrence.Assign(instance); }
		public Pset_SpaceOccupancyRequirements(IfcSpatialZoneType type) : base(type.mDatabase, "Pset_SpaceOccupancyRequirements") { Description = type.Name; type.AddPropertySet(this); }
		public Pset_SpaceOccupancyRequirements(IfcZone instance) : base(instance.mDatabase, "Pset_SpaceOccupancyRequirements") { DefinesOccurrence.Assign(instance); }
	}
	public partial class Pset_UnitaryEquipmentTypeAirHandler : IfcPropertySet
	{
		public PEnum_AirHandlerConstruction AirHandlerConstruction { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "AirHandlerConstruction", new IfcLabel(value.ToString())).mIndex); } }
		public PEnum_AirHandlerFanCoilArrangement AirHandlerFanCoilArrangement { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "AirHandlerFanCoilArrangement", new IfcLabel(value.ToString())).mIndex); } }
		public bool DualDeck { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "DualDeck", new IfcBoolean(value)).mIndex); } }
		public Pset_UnitaryEquipmentTypeAirHandler(IfcUnitaryEquipment unitaryEquipment) : base(unitaryEquipment.mDatabase, "Pset_UnitaryEquipmentTypeAirHandler") { Description = unitaryEquipment.Name; DefinesOccurrence.Assign(unitaryEquipment); }
		public Pset_UnitaryEquipmentTypeAirHandler(IfcUnitaryEquipmentType unitaryEquipmentType) : base(unitaryEquipmentType.mDatabase, "Pset_UnitaryEquipmentTypeAirHandler") { Description = unitaryEquipmentType.Name; unitaryEquipmentType.AddPropertySet(this); }
	}
	public partial class Pset_UnitaryEquipmentTypeCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public PEnum_ElementStatus Status { set { mHasProperties.Add(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString())).mIndex); } }
		public Pset_UnitaryEquipmentTypeCommon(IfcUnitaryEquipment unitaryEquipment) : base(unitaryEquipment.mDatabase, "Pset_UnitaryEquipmentTypeCommonr") { Description = unitaryEquipment.Name; DefinesOccurrence.Assign(unitaryEquipment); }
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
		public Pset_ValveTypeCommon(IfcValve valve) : base(valve.mDatabase, "Pset_ValveTypeCommon") { Description = valve.Name; DefinesOccurrence.Assign(valve); }
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
		public Pset_WallCommon(IfcWall wall) : base(wall.mDatabase, "Pset_WallCommon") {  DefinesOccurrence.Assign(wall); }
		public Pset_WallCommon(IfcWallType wallType) : base(wallType.mDatabase, "Pset_WallCommon") { Description = wallType.Name;  wallType.AddPropertySet(this); }
	}
	public partial class Pset_ZoneCommon : IfcPropertySet
	{
		public string Reference { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value)).mIndex); } }
		public bool IsExternal { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value)).mIndex); } }
		public double GrossPlannedArea { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "GrossPlannedArea", new IfcAreaMeasure(value)).mIndex); } }
		public double NetPlannedArea { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NetPlannedArea", new IfcAreaMeasure(value)).mIndex); } }
		public bool PubliclyAccessible { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "PubliclyAccessible", new IfcBoolean(value)).mIndex); } }
		public bool HandicapAccessible { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "HandicapAccessible", new IfcBoolean(value)).mIndex); } }
		public Pset_ZoneCommon(IfcZone instance) : base(instance.mDatabase, "Pset_ZoneCommon") { DefinesOccurrence.Assign(instance); }
	}
}
