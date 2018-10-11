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

namespace GeometryGym.Ifc
{
	public partial class Pset_ActionRequest : IfcPropertySet
	{
		public string RequestSourceLabel { set { AddProperty(new IfcPropertySingleValue(mDatabase, "RequestSourceLabel", new IfcLabel(value))); } }
		public IfcPerson RequestSourceName { set { AddProperty(new IfcPropertyReferenceValue("RequestSourceName", value )); } }
		public string RequestComments { set { AddProperty(new IfcPropertySingleValue(mDatabase, "RequestComments", new IfcText(value))); } }
		public Pset_ActionRequest(IfcActionRequest instance) : base(instance) { }
	}
	public partial class Pset_ActorCommon : IfcPropertySet
	{
		public double NumberOfActors { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NumberOfActors", new IfcCountMeasure(value))); } }
		public string Category { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Category", new IfcLabel(value))); } }
		public string SkillLevel { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SkillLevel", new IfcLabel(value))); } }
		public Pset_ActorCommon(IfcActor instance) : base(instance) { }
	}
	public partial class Pset_ActuatorPHistory : IfcPropertySet
	{
		public Pset_ActuatorPHistory(IfcActuator instance) : base(instance.mDatabase, "Pset_ActuatorPHistory") { Description = instance.Name; DefinesOccurrence.RelatedObjects.Add(instance); }
	}
	public partial class Pset_ActuatorTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public PEnum_FailPosition FailPosition { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "FailPosition", new IfcLabel(value.ToString()))); } }
		public bool ManualOverride { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ManualOverride", new IfcBoolean(value))); } }
		public PEnum_ActuatorApplication Application { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Application", new IfcLabel(value.ToString()))); } }
		public Pset_ActuatorTypeCommon(IfcActuator instance) : base(instance) { }
		public Pset_ActuatorTypeCommon(IfcActuatorType type) : base(type) { }
	}
	public partial class Pset_ActuatorTypeElectricActuator : IfcPropertySet
	{
		public double ActuatorInputPower { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ActuatorInputPower", new IfcPowerMeasure(value))); } }
		public PEnum_ElectricActuatorType ElectricActuatorType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ElectricActuatorType", new IfcLabel(value.ToString()))); } }
		public Pset_ActuatorTypeElectricActuator(IfcActuator instance) : base(instance.mDatabase, "Pset_ActuatorTypeElectricActuator") { Description = instance.Name; DefinesOccurrence.RelatedObjects.Add(instance); }
		public Pset_ActuatorTypeElectricActuator(IfcActuatorType type) : base(type) { }
	}
	public partial class Pset_ActuatorTypeHydraulicActuator : IfcPropertySet
	{
		public double InputPressure { set { AddProperty(new IfcPropertySingleValue(mDatabase, "InputPressure", new IfcPressureMeasure(value))); } }
		public double InputFlowrate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "InputFlowrate", new IfcVolumetricFlowRateMeasure(value))); } }
		public Pset_ActuatorTypeHydraulicActuator(IfcActuator instance) : base(instance) { }
		public Pset_ActuatorTypeHydraulicActuator(IfcActuatorType type) : base(type) { }
	}
	public partial class Pset_ActuatorTypeLinearActuation : IfcPropertySet
	{
		public double Force { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Force", new IfcForceMeasure(value))); } }
		public double Stroke { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Stroke", new IfcLengthMeasure(value))); } }
		public Pset_ActuatorTypeLinearActuation(IfcActuator instance) : base(instance) { }
		public Pset_ActuatorTypeLinearActuation(IfcActuatorType type) : base(type) { }
	}
	public partial class Pset_ActuatorTypePneumaticActuator : IfcPropertySet
	{
		public double InputPressure { set { AddProperty(new IfcPropertySingleValue(mDatabase, "InputPressure", new IfcPressureMeasure(value))); } }
		public double InputFlowrate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "InputFlowrate", new IfcVolumetricFlowRateMeasure(value))); } }
		public Pset_ActuatorTypePneumaticActuator(IfcActuator instance) : base(instance) { }
		public Pset_ActuatorTypePneumaticActuator(IfcActuatorType type) : base(type) { }
	}
	public partial class Pset_ActuatorTypeRotationalActuation : IfcPropertySet
	{
		public double Torque { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Torque", new IfcTorqueMeasure(value))); } }
		public double RangeAngle { set { AddProperty(new IfcPropertySingleValue(mDatabase, "RangeAngle", new IfcPlaneAngleMeasure(value))); } }
		public Pset_ActuatorTypeRotationalActuation(IfcActuator instance) : base(instance) { }
		public Pset_ActuatorTypeRotationalActuation(IfcActuatorType type) : base(type) { }
	}
	public partial class Pset_AirSideSystemInformation : IfcPropertySet
	{
		public string Name { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Name", new IfcLabel(value))); } }
		public new string Description { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Description", new IfcLabel(value))); } }
		public PEnum_AirSideSystemType AirSideSystemType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AirSideSystemType", new IfcLabel(value.ToString()))); } }
		public PEnum_AirSideSystemDistributionType AirSideSystemDistributionType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AirSideSystemDistributionType", new IfcLabel(value.ToString()))); } }
		public double TotalAirflow { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TotalAirflow", new IfcVolumetricFlowRateMeasure(value))); } }
		public double EnergyGainTotal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "EnergyGainTotal", new IfcPowerMeasure(value))); } }
		public double AirflowSensible { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AirflowSensible", new IfcVolumetricFlowRateMeasure(value))); } }
		public double EnergyGainSensible { set { AddProperty(new IfcPropertySingleValue(mDatabase, "EnergyGainSensible", new IfcPowerMeasure(value))); } }
		public double EnergyLoss { set { AddProperty(new IfcPropertySingleValue(mDatabase, "EnergyLoss", new IfcPowerMeasure(value))); } }
		public double LightingDiversity { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LightingDiversity", new IfcPositiveRatioMeasure(value))); } }
		public double InfiltrationDiversitySummer { set { AddProperty(new IfcPropertySingleValue(mDatabase, "InfiltrationDiversitySummer", new IfcPositiveRatioMeasure(value))); } }
		public double InfiltrationDiversityWinter { set { AddProperty(new IfcPropertySingleValue(mDatabase, "InfiltrationDiversityWinter", new IfcPositiveRatioMeasure(value))); } }
		public double ApplianceDiversity { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ApplianceDiversity", new IfcPositiveRatioMeasure(value))); } }
		public double LoadSafetyFactor { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LoadSafetyFactor", new IfcPositiveRatioMeasure(value))); } }
		public double HeatingTemperatureDelta { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HeatingTemperatureDelta", new IfcThermodynamicTemperatureMeasure(value))); } }
		public double CoolingTemperatureDelta { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CoolingTemperatureDelta", new IfcThermodynamicTemperatureMeasure(value))); } }
		public double Ventilation { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Ventilation", new IfcVolumetricFlowRateMeasure(value))); } }
		public double FanPower { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FanPower", new IfcPowerMeasure(value))); } }
		public Pset_AirSideSystemInformation(IfcSpace instance) : base(instance) { }
		public Pset_AirSideSystemInformation(IfcSpaceType type) : base(type) { }
		public Pset_AirSideSystemInformation(IfcSpatialZone instance) : base(instance) { }
		public Pset_AirSideSystemInformation(IfcSpatialZoneType type) : base(type) { }
		public Pset_AirSideSystemInformation(IfcZone instance) : base(instance) { }
	}
	//Pset_AirTerminalBoxPHistory
	public partial class Pset_AirTerminalBoxTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public IfcPropertyBoundedValue<IfcVolumetricFlowRateMeasure> AirFlowrateRange { set { value.Name = "AirFlowrateRange"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcPressureMeasure> AirPressureRange { set { value.Name = "AirPressureRange"; addProperty(value); } }
		public double NominalAirFlowRate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalAirFlowRate", new IfcVolumetricFlowRateMeasure(value))); } }
		public PEnum_AirTerminalBoxArrangementType ArrangementType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ArrangementType", new IfcLabel(value.ToString()))); } }
		public PEnum_AirTerminalBoxArrangementType ReheatType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ReheatType", new IfcLabel(value.ToString()))); } }
		public bool HasSoundAttenuator { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HasSoundAttenuator", new IfcBoolean(value))); } }
		public bool HasReturnAir { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HasReturnAir", new IfcBoolean(value))); } }
		public bool HasFan { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HasFan", new IfcBoolean(value))); } }
		public double NominalInletAirPressure { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalInletAirPressure", new IfcPressureMeasure(value))); } }
		public double NominalDamperDiameter { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalDamperDiameter", new IfcPositiveLengthMeasure(value))); } }
		public double HousingThickness { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HousingThickness", new IfcLengthMeasure(value))); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> OperationTemperatureRange { set { value.Name = "OperationTemperatureRange"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcPositiveRatioMeasure> ReturnAirFractionRange { set { value.Name = "ReturnAirFractionRange"; addProperty(value); } }
		public Pset_AirTerminalBoxTypeCommon(IfcAirTerminalBox instance) : base(instance) { }
		public Pset_AirTerminalBoxTypeCommon(IfcAirTerminalBoxType type) : base(type) { }
	}
	public partial class Pset_AirTerminalOccurrence : IfcPropertySet
	{
		public PEnum_AirTerminalAirflowType AirflowType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AirflowType", new IfcLabel(value.ToString()))); } }
		public double AirFlowRate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AirFlowRate", new IfcVolumetricFlowRateMeasure(value))); } }
		public PEnum_AirTerminalLocation Location { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Location", new IfcLabel(value.ToString()))); } }
		public Pset_AirTerminalOccurrence(IfcAirTerminal instance) : base(instance) { }
		public Pset_AirTerminalOccurrence(IfcAirTerminalType type) : base(type) { }
	}
	//Pset_AirTerminalPHistory
	public partial class Pset_AirTerminalTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public PEnum_AirTerminalShape Shape { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Shape", new IfcLabel(value.ToString()))); } }
		public PEnum_AirTerminalFaceType FaceType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "FaceType", new IfcLabel(value.ToString()))); } }
		public double SlotWidth { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SlotWidth", new IfcPositiveLengthMeasure(value))); } }
		public double SlotLength { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SlotLength", new IfcPositiveLengthMeasure(value))); } }
		public int NumberOfSlots { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NumberOfSlots", new IfcInteger(value))); } }
		public PEnum_AirTerminalFlowPattern FlowPattern { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "FlowPattern", new IfcLabel(value.ToString()))); } }
		public IfcPropertyBoundedValue<IfcVolumetricFlowRateMeasure> AirFlowrateRange { set { value.Name = "AirFlowrateRange"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> TemperatureRange { set { value.Name = "TemperatureRange"; addProperty(value); } }
		public PEnum_AirTerminalDischargeDirection DischargeDirection { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "DischargeDirection", new IfcLabel(value.ToString()))); } }
		public double ThrowLength { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThrowLength", new IfcLengthMeasure(value))); } }
		public double AirDiffusionPerformanceIndex { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AirDiffusionPerformanceIndex", new IfcReal(value))); } }
		public PEnum_AirTerminalFinishType FinishType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "FinishType", new IfcLabel(value.ToString()))); } }
		public string FinishColor { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FinishColor", new IfcLabel(value))); } }
		public PEnum_AirTerminalMountingType MountingType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "MountingType", new IfcLabel(value.ToString()))); } }
		public PEnum_AirTerminalCoreType CoreType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "CoreType", new IfcLabel(value.ToString()))); } }
		public double CoreSetHorizontal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CoreSetHorizontal", new IfcPlaneAngleMeasure(value))); } }
		public double CoreSetVertical { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CoreSetVertical", new IfcPlaneAngleMeasure(value))); } }
		public bool HasIntegralControl { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HasIntegralControl", new IfcBoolean(value))); } }
		public PEnum_AirTerminalFlowControlType FlowControlType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "FlowControlType", new IfcLabel(value.ToString()))); } }
		public bool HasSoundAttenuator { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HasSoundAttenuator", new IfcBoolean(value))); } }
		public bool HasThermalInsulation { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HasThermalInsulation", new IfcBoolean(value))); } }
		public double NeckArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NeckArea", new IfcAreaMeasure(value))); } }
		public double EffectiveArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "EffectiveArea", new IfcAreaMeasure(value))); } }
		public IfcPropertyTableValue<IfcVolumetricFlowRateMeasure, IfcPositiveRatioMeasure> AirFlowrateVersusFlowControlElement { set { value.Name = "AirFlowrateVersusFlowControlElement"; addProperty(value); } }
		public Pset_AirTerminalTypeCommon(IfcAirTerminal instance) : base(instance) { }
		public Pset_AirTerminalTypeCommon(IfcAirTerminalType type) : base(type) { }
	}
	//Pset_AirToAirHeatRecoveryPHistory
	public partial class Pset_AirToAirHeatRecoveryTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public PEnum_HeatTransferType HeatTransferTypeEnum { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "HeatTransferTypeEnum", new IfcLabel(value.ToString()))); } }
		public bool HasDefrost { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HasDefrost", new IfcBoolean(value))); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> OperationalTemperatureRange { set { value.Name = "OperationalTemperatureRange"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcVolumetricFlowRateMeasure> PrimaryAirflowRateRange { set { value.Name = "PrimaryAirflowRateRange"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcPressureMeasure> SecondaryAirflowRateRange { set { value.Name = "SecondaryAirflowRateRange"; addProperty(value); } }
		public Pset_AirToAirHeatRecoveryTypeCommon(IfcAirToAirHeatRecovery instance) : base(instance) { }
		public Pset_AirToAirHeatRecoveryTypeCommon(IfcAirToAirHeatRecoveryType type) : base(type) { }
	}
	//Pset_AlarmPHistory
	public partial class Pset_AlarmTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public string Condition { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "Condition", new IfcLabel(value))); } }
		public Pset_AlarmTypeCommon(IfcAlarm instance) : base(instance) { }
		public Pset_AlarmTypeCommon(IfcAlarmType type) : base(type) { }
	}
	public partial class Pset_AnnotationContourLine : IfcPropertySet
	{
		public double ContourValue { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ContourValue", new IfcLengthMeasure(value))); } }
		public Pset_AnnotationContourLine(IfcAnnotation instance) : base(instance) { }
	}
	public partial class Pset_AnnotationLineOfSight : IfcPropertySet
	{
		public double SetbackDistance { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SetbackDistance", new IfcPositiveLengthMeasure(value))); } }
		public double VisibleAngleLeft { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VisibleAngleLeft", new IfcPositivePlaneAngleMeasure(value))); } }
		public double VisibleAngleRight { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VisibleAngleRight", new IfcPositivePlaneAngleMeasure(value))); } }
		public double RoadVisibleDistanceLeft { set { AddProperty(new IfcPropertySingleValue(mDatabase, "RoadVisibleDistanceLeft", new IfcPositiveLengthMeasure(value))); } }
		public double RoadVisibleDistanceRight { set { AddProperty(new IfcPropertySingleValue(mDatabase, "RoadVisibleDistanceRight", new IfcPositiveLengthMeasure(value))); } }
		public Pset_AnnotationLineOfSight(IfcAnnotation instance) : base(instance) { }
	}
	public partial class Pset_AnnotationSurveyArea : IfcPropertySet
	{
		public PEnum_AcquisitionMethod AcquisitionMethod { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AcquisitionMethod", new IfcLabel(value.ToString()))); } }
		public double AccuracyQualityObtained { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AccuracyQualityObtained", new IfcRatioMeasure(value))); } }
		public double AccuracyQualityExpected { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AccuracyQualityExpected", new IfcRatioMeasure(value))); } }
		public Pset_AnnotationSurveyArea(IfcAnnotation instance) : base(instance) { }
	}
	public partial class Pset_Asset : IfcPropertySet
	{
		public PEnum_AssetAccountingType AssetAccountingType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AssetAccountingType", new IfcLabel(value.ToString()))); } }
		public PEnum_AssetTaxType AssetTaxType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AssetTaxType", new IfcLabel(value.ToString()))); } }
		public PEnum_AssetAccountingType AssetInsuranceType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AssetInsuranceType", new IfcLabel(value.ToString()))); } }
		public Pset_Asset(IfcAsset instance) : base(instance) { }
	}
	// Pset_AudioVisualAppliancePHistory
	public partial class Pset_AudioVisualApplianceTypeAmplifier : IfcPropertySet
	{
		public PEnum_AssetAccountingType AmplifierType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AmplifierType", new IfcLabel(value.ToString()))); } }
		public IfcPropertyTableValue<IfcFrequencyMeasure, IfcSoundPowerMeasure> AudioAmplification { set { value.Name = "AudioAmplification"; addProperty(value); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> AudioMode { set { value.Name = "AudioMode"; addProperty(value); } }
		public Pset_AudioVisualApplianceTypeAmplifier(IfcAudioVisualAppliance instance) : base(instance) { }
		public Pset_AudioVisualApplianceTypeAmplifier(IfcAudioVisualApplianceType type) : base(type) { }
	}
	public partial class Pset_AudioVisualApplianceTypeCamera : IfcPropertySet
	{
		public PEnum_AssetAccountingType CameraType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "CameraType", new IfcLabel(value.ToString()))); } }
		public bool IsOutdoors { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsOutdoors", new IfcBoolean(value))); } }
		public int VideoResolutionWidth { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VideoResolutionWidth", new IfcInteger(value))); } }
		public int VideoResolutionHeight { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VideoResolutionHeight", new IfcInteger(value))); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> VideoResolutionMode { set { value.Name = "VideoResolutionMode"; addProperty(value); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcTimeMeasure> VideoCaptureInterval { set { value.Name = "VideoCaptureInterval"; addProperty(value); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> PanTiltZoomPreset { set { value.Name = "PanTiltZoomPreset"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcLengthMeasure> PanHorizontal { set { value.Name = "PanHorizontal"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcLengthMeasure> PanVertical { set { value.Name = "PanVertical"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcPlaneAngleMeasure> TiltHorizontal { set { value.Name = "TiltHorizontal"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcPlaneAngleMeasure> TiltVertical { set { value.Name = "TiltVertical"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcPositiveLengthMeasure> Zoom { set { value.Name = "Zoom"; addProperty(value); } }
		public Pset_AudioVisualApplianceTypeCamera(IfcAudioVisualAppliance instance) : base(instance) { }
		public Pset_AudioVisualApplianceTypeCamera(IfcAudioVisualApplianceType type) : base(type) { }
	}
	public partial class Pset_AudioVisualApplianceTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> MediaSource { set { value.Name = "MediaSource"; addProperty(value); } }
		public IfcPropertyTableValue<IfcInteger, IfcSoundPowerMeasure> AudioVolume { set { value.Name = "AudioVolume"; addProperty(value); } }
		public Pset_AudioVisualApplianceTypeCommon(IfcAudioVisualAppliance instance) : base(instance) { }
		public Pset_AudioVisualApplianceTypeCommon(IfcAudioVisualApplianceType type) : base(type) { }
	}
	public partial class Pset_AudioVisualApplianceTypeDisplay : IfcPropertySet
	{
		public PEnum_AudioVisualDisplayType DisplayType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "DisplayType", new IfcLabel(value.ToString()))); } }
		public double NominalSize { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalSize", new IfcPositiveLengthMeasure(value))); } }
		public double DisplayWidth { set { AddProperty(new IfcPropertySingleValue(mDatabase, "DisplayWidth", new IfcPositiveLengthMeasure(value))); } }
		public double DisplayHeight { set { AddProperty(new IfcPropertySingleValue(mDatabase, "DisplayHeight", new IfcPositiveLengthMeasure(value))); } }
		public double Brightness { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Brightness", new IfcIlluminanceMeasure(value))); } }
		public double ContrastRatio { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ContrastRatio", new IfcPositiveRatioMeasure(value))); } }
		public double RefreshRate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "RefreshRate", new IfcFrequencyMeasure(value))); } }
		public PEnum_AudioVisualDisplayTouchScreen TouchScreen { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "TouchScreen", new IfcLabel(value.ToString()))); } }
		public int VideoResolutionWidth { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VideoResolutionWidth", new IfcInteger(value))); } }
		public int VideoResolutionHeight { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VideoResolutionHeight", new IfcInteger(value))); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> VideoResolutionMode { set { value.Name = "VideoResolutionMode"; addProperty(value); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> VideoScaleMode { set { value.Name = "VideoScaleMode"; addProperty(value); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcTimeMeasure> VideoCaptionMode { set { value.Name = "VideoCaptionMode"; addProperty(value); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcTimeMeasure> AudioMode { set { value.Name = "AudioMode"; addProperty(value); } }
		public Pset_AudioVisualApplianceTypeDisplay(IfcAudioVisualAppliance instance) : base(instance) { }
		public Pset_AudioVisualApplianceTypeDisplay(IfcAudioVisualApplianceType type) : base(type) { }
	}
	public partial class Pset_AudioVisualApplianceTypePlayer : IfcPropertySet
	{
		public PEnum_AudioVisualPlayerType PlayerType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "PlayerType", new IfcLabel(value.ToString()))); } }
		public bool PlayerMediaEject { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PlayerMediaEject", new IfcBoolean(value))); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> PlayerMediaFormat { set { value.Name = "PlayerMediaFormat"; addProperty(value); } }
		public Pset_AudioVisualApplianceTypePlayer(IfcAudioVisualAppliance instance) : base(instance) { }
		public Pset_AudioVisualApplianceTypePlayer(IfcAudioVisualApplianceType type) : base(type) { }
	}
	public partial class Pset_AudioVisualApplianceTypeProjector : IfcPropertySet
	{
		public PEnum_AudioVisualProjectorType ProjectorType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "PlayerType", new IfcLabel(value.ToString()))); } }
		public int VideoResolutionWidth { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VideoResolutionWidth", new IfcInteger(value))); } }
		public int VideoResolutionHeight { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VideoResolutionHeight", new IfcInteger(value))); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> VideoResolutionMode { set { value.Name = "VideoResolutionMode"; addProperty(value); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> VideoScaleMode { set { value.Name = "VideoScaleMode"; addProperty(value); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcTimeMeasure> VideoCaptionMode { set { value.Name = "VideoCaptionMode"; addProperty(value); } }
		public Pset_AudioVisualApplianceTypeProjector(IfcAudioVisualAppliance instance) : base(instance) { }
		public Pset_AudioVisualApplianceTypeProjector(IfcAudioVisualApplianceType type) : base(type) { }
	}
	public partial class Pset_AudioVisualApplianceTypeReceiver : IfcPropertySet
	{
		public PEnum_AudioVisualReceiverType ReceiverType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ReceiverType", new IfcLabel(value.ToString()))); } }
		public IfcPropertyTableValue<IfcFrequencyMeasure, IfcRatioMeasure> AudioAmplification { set { value.Name = "AudioAmplification"; addProperty(value); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> AudioMode { set { value.Name = "AudioMode"; addProperty(value); } }
		public Pset_AudioVisualApplianceTypeReceiver(IfcAudioVisualAppliance instance) : base(instance) { }
		public Pset_AudioVisualApplianceTypeReceiver(IfcAudioVisualApplianceType type) : base(type) { }
	}
	public partial class Pset_AudioVisualApplianceTypeSpeaker : IfcPropertySet
	{
		public PEnum_AudioVisualSpeakerType SpeakerType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "SpeakerType", new IfcLabel(value.ToString()))); } }
		public PEnum_AudioVisualSpeakerMounting SpeakerMounting { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "SpeakerMounting", new IfcLabel(value.ToString()))); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcPositiveLengthMeasure> SpeakerDriverSize { set { value.Name = "SpeakerDriverSize"; addProperty(value); } }
		public IfcPropertyTableValue<IfcFrequencyMeasure, IfcSoundPowerMeasure> FrequencyResponse { set { value.Name = "FrequencyResponse"; addProperty(value); } }
		public double Impedence { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Impedence", new IfcFrequencyMeasure(value))); } }
		public Pset_AudioVisualApplianceTypeSpeaker(IfcAudioVisualAppliance instance) : base(instance) { }
		public Pset_AudioVisualApplianceTypeSpeaker(IfcAudioVisualApplianceType type) : base(type) { }
	}
	public partial class Pset_AudioVisualApplianceTypeTuner : IfcPropertySet
	{
		public PEnum_AudioVisualTunerType TunerType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "TunerType", new IfcLabel(value.ToString()))); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> TunerMode { set { value.Name = "TunerMode"; addProperty(value); } }
		public IfcPropertyTableValue<IfcIdentifier, IfcLabel> TunerChannel { set { value.Name = "TunerChannel"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcFrequencyMeasure> TunerFrequency { set { value.Name = "TunerFrequency"; addProperty(value); } }
		public Pset_AudioVisualApplianceTypeTuner(IfcAudioVisualAppliance instance) : base(instance) { }
		public Pset_AudioVisualApplianceTypeTuner(IfcAudioVisualApplianceType type) : base(type) { }
	}
	public partial class Pset_BeamCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public double Span { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Span", new IfcPositiveLengthMeasure(value))); } }
		public double Slope { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Slope", new IfcPlaneAngleMeasure(value))); } }
		public double Roll { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Roll", new IfcPlaneAngleMeasure(value))); } }
		public bool IsExternal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value))); } }
		public double ThermalTransmittance { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThermalTransmittance", new IfcThermalTransmittanceMeasure(value))); } }
		public bool LoadBearing { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LoadBearing", new IfcBoolean(value))); } }
		public string FireRating { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "FireRating", new IfcLabel(value))); } }
		public Pset_BeamCommon(IfcBeam instance) : base(instance) { }
		public Pset_BeamCommon(IfcBeamType type) : base(type) { }
	}
	//  Pset_BoilerPHistory
	public partial class Pset_BoilerTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public double PressureRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PressureRating", new IfcPressureMeasure(value))); } }
		public PEnum_BoilerOperatingMode OperatingMode { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "OperatingMode", new IfcLabel(value.ToString()))); } }
		public double HeatTransferSurfaceArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HeatTransferSurfaceArea", new IfcAreaMeasure(value))); } }
		public double NominalPartLoadRatio { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalPartLoadRatio", new IfcReal(value))); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> WaterInletTemperatureRange { set { value.Name = "WaterInletTemperatureRange"; addProperty(value); } }
		public double WaterStorageCapacity { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WaterStorageCapacity", new IfcVolumeMeasure(value))); } }
		public bool IsWaterStorageHeater { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsWaterStorageHeater", new IfcBoolean(value))); } }
		public IfcPropertyTableValue<IfcPositiveRatioMeasure, IfcNormalisedRatioMeasure> PartialLoadEfficiencyCurves { set { value.Name = "PartialLoadEfficiencyCurves"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> OutletTemperatureRange { set { value.Name = "OutletTemperatureRange"; addProperty(value); } }
		public double NominalEnergyConsumption { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalEnergyConsumption", new IfcPowerMeasure(value))); } }
		public PEnum_EnergySource EnergySource { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "EnergySource", new IfcLabel(value.ToString()))); } }
		public Pset_BoilerTypeCommon(IfcBoiler instance) : base(instance) { }
		public Pset_BoilerTypeCommon(IfcBoilerType type) : base(type) { }
	}
	public partial class Pset_BoilerTypeSteam : IfcPropertySet
	{
		public string MaximumOutletPressure { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "MaximumOutletPressure", new IfcLabel(value))); } }
		public IfcPropertyTableValue<IfcThermodynamicTemperatureMeasure, IfcNormalisedRatioMeasure> NominalEfficiency { set { value.Name = "NominalEfficiency"; addProperty(value); } }
		public IfcPropertyTableValue<IfcThermodynamicTemperatureMeasure, IfcEnergyMeasure> HeatOutput { set { value.Name = "HeatOutput"; addProperty(value); } }
		public Pset_BoilerTypeSteam(IfcBoiler instance) : base(instance) { }
		public Pset_BoilerTypeSteam(IfcBoilerType type) : base(type) { }
	}
	public partial class Pset_BoilerTypeWater : IfcPropertySet
	{
		public IfcPropertyTableValue<IfcThermodynamicTemperatureMeasure, IfcNormalisedRatioMeasure> NominalEfficiency { set { value.Name = "NominalEfficiency"; addProperty(value); } }
		public IfcPropertyTableValue<IfcThermodynamicTemperatureMeasure, IfcEnergyMeasure> HeatOutput { set { value.Name = "HeatOutput"; addProperty(value); } }
		public Pset_BoilerTypeWater(IfcBoiler instance) : base(instance) { }
		public Pset_BoilerTypeWater(IfcBoilerType type) : base(type) { }
	}
	public partial class Pset_BuildingCommon : IfcPropertySet
	{
		public string Reference { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public string BuildingID { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "BuildingID", new IfcIdentifier(value))); } }
		public bool IsPermanentID { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsPermanentID", new IfcBoolean(value))); } }
		public string ConstructionMethod { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "ConstructionMethod", new IfcLabel(value))); } }
		public string FireProtectionClass { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "FireProtectionClass", new IfcLabel(value))); } }
		public bool SprinklerProtection { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SprinklerProtection", new IfcBoolean(value))); } }
		public bool SprinklerProtectionAutomatic { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SprinklerProtectionAutomatic", new IfcBoolean(value))); } }
		public string OccupancyType { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "OccupancyType", new IfcLabel(value))); } }
		public double GrossPlannedArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "GrossPlannedArea", new IfcAreaMeasure(value))); } }
		public double NetPlannedArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NetPlannedArea", new IfcAreaMeasure(value))); } }
		public int NumberOfStoreys { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NumberOfStoreys", new IfcInteger(value))); } }
		public string YearOfConstruction { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "YearOfConstruction", new IfcLabel(value))); } }
		public string YearOfLastRefurbishment { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "YearOfLastRefurbishment", new IfcLabel(value))); } }
		public IfcLogicalEnum IsLandmarked { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsLandmarked", new IfcLogical(value))); } }
		public Pset_BuildingCommon(IfcBuilding instance) : base(instance) { }
	}
	public partial class Pset_BuildingElementProxyCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public bool IsExternal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value))); } }
		public double ThermalTransmittance { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThermalTransmittance", new IfcThermalTransmittanceMeasure(value))); } }
		public bool LoadBearing { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LoadBearing", new IfcBoolean(value))); } }
		public string FireRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FireRating", new IfcLabel(value))); } }
		public Pset_BuildingElementProxyCommon(IfcBuildingElementProxy instance) : base(instance) { }
		public Pset_BuildingElementProxyCommon(IfcBuildingElementProxyType type) : base(type) { }
	}
	public partial class Pset_BuildingElementProxyProvisionForVoid : IfcPropertySet
	{
		public string Shape { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Shape", new IfcLabel(value))); } }
		public double Width { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Width", new IfcPositiveLengthMeasure(value))); } }
		public double Height { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Height", new IfcPositiveLengthMeasure(value))); } }
		public double Diameter { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Diameter", new IfcPositiveLengthMeasure(value))); } }
		public double Depth { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Depth", new IfcPositiveLengthMeasure(value))); } }
		public string System { set { AddProperty(new IfcPropertySingleValue(mDatabase, "System", new IfcLabel(value))); } }
		public Pset_BuildingElementProxyProvisionForVoid(IfcBuildingElementProxy instance) : base(instance) { }
		public Pset_BuildingElementProxyProvisionForVoid(IfcBuildingElementProxyType type) : base(type) { }
	}
	public partial class Pset_BuildingStoreyCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public bool EntranceLevel { set { AddProperty(new IfcPropertySingleValue(mDatabase, "EntranceLevel", new IfcBoolean(value))); } }
		public IfcLogicalEnum AboveGround { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AboveGround", new IfcLogical(value))); } }
		public bool SprinklerProtection { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SprinklerProtection", new IfcBoolean(value))); } }
		public bool SprinklerProtectionAutomatic { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SprinklerProtectionAutomatic", new IfcBoolean(value))); } }
		public double LoadBearingCapacity { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LoadBearingCapacity", new IfcPlanarForceMeasure(value))); } }
		public double GrossPlannedArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "GrossPlannedArea", new IfcAreaMeasure(value))); } }
		public double NetPlannedArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NetPlannedArea", new IfcAreaMeasure(value))); } }
		public Pset_BuildingStoreyCommon(IfcBuildingStorey instance) : base(instance) { }
	}
	public partial class Pset_BuildingSystemCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public Pset_BuildingSystemCommon(IfcBuildingSystem instance) : base(instance) { }
	}
	public partial class Pset_BuildingUse : IfcPropertySet
	{
		public string MarketCategory { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MarketCategory", new IfcLabel(value))); } }
		public string MarketSubCategory { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MarketSubCategory", new IfcLabel(value))); } }
		public string PlanningControlStatus { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PlanningControlStatus", new IfcLabel(value))); } }
		public string NarrativeText { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NarrativeText", new IfcText(value))); } }
		public double VacancyRateInCategoryNow { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VacancyRateInCategoryNow", new IfcPositiveRatioMeasure(value))); } }
		public IfcPropertyListValue<IfcLabel> TenureModesAvailableNow { set { value.Name = "TenureModesAvailableNow"; addProperty(value); } }
		public IfcPropertyListValue<IfcLabel> MarketSubCategoriesAvailableNow { set { value.Name = "MarketSubCategoriesAvailableNow"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcMonetaryMeasure> RentalRatesInCategoryNow { set { value.Name = "RentalRatesInCategoryNow"; addProperty(value); } }
		public double VacancyRateInCategoryFuture { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VacancyRateInCategoryFuture", new IfcPositiveRatioMeasure(value))); } }
		public IfcPropertyListValue<IfcLabel> TenureModesAvailableFuture { set { value.Name = "TenureModesAvailableFuture"; addProperty(value); } }
		public IfcPropertyListValue<IfcLabel> MarketSubCategoriesAvailableFuture { set { value.Name = "MarketSubCategoriesAvailableFuture"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcMonetaryMeasure> RentalRatesInCategoryFuture { set { value.Name = "RentalRatesInCategoryFuture"; addProperty(value); } }
		public Pset_BuildingUse(IfcBuilding instance) : base(instance) { }
	}
	public partial class Pset_BuildingUseAdjacent : IfcPropertySet
	{
		public string MarketCategory { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MarketCategory", new IfcLabel(value))); } }
		public string MarketSubCategory { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MarketSubCategory", new IfcLabel(value))); } }
		public string PlanningControlStatus { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PlanningControlStatus", new IfcLabel(value))); } }
		public string NarrativeText { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NarrativeText", new IfcText(value))); } }
		public Pset_BuildingUseAdjacent(IfcBuilding instance) : base(instance) { }
	}
	public partial class Pset_BurnerTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public PEnum_EnergySource EnergySource { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "EnergySource", new IfcLabel(value.ToString()))); } }
		public Pset_BurnerTypeCommon(IfcBurner instance) : base(instance) { }
		public Pset_BurnerTypeCommon(IfcBurnerType type) : base(type) { }
	}
	public partial class Pset_CableCarrierFittingTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public Pset_CableCarrierFittingTypeCommon(IfcCableCarrierFitting instance) : base(instance) { }
		public Pset_CableCarrierFittingTypeCommon(IfcCableCarrierFittingType type) : base(type) { }
	}
	public partial class Pset_CableCarrierSegmentTypeCableLadderSegment : IfcPropertySet
	{
		public double NominalWidth { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalWidth", new IfcPositiveLengthMeasure(value))); } }
		public double NominalHeight { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalHeight", new IfcPositiveLengthMeasure(value))); } }
		public string LadderConfiguration { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LadderConfiguration", new IfcText(value))); } }
		public Pset_CableCarrierSegmentTypeCableLadderSegment(IfcCableCarrierSegment instance) : base(instance) { }
		public Pset_CableCarrierSegmentTypeCableLadderSegment(IfcCableCarrierSegmentType type) : base(type) { }
	}
	//Pset_CableCarrierSegmentTypeCableTraySegment
	//Pset_CableCarrierSegmentTypeCableTrunkingSegment
	//Pset_CableCarrierSegmentTypeCommon
	//Pset_CableCarrierSegmentTypeConduitSegment
	//Pset_CableFittingTypeCommon
	//Pset_CableSegmentOccurrence
	//Pset_CableSegmentTypeBusBarSegment
	//Pset_CableSegmentTypeCableSegment
	//Pset_CableSegmentTypeCommon
	//Pset_CableSegmentTypeConductorSegment
	//Pset_CableSegmentTypeCoreSegment
	//Pset_ChillerPHistory
	//Pset_ChillerTypeCommon
	//Pset_ChimneyCommon
	//Pset_CivilElementCommon
	//Pset_CoilOccurrence
	//Pset_CoilPHistory
	//Pset_CoilTypeCommon
	//Pset_CoilTypeHydronic
	public partial class Pset_ColumnCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public double Slope { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Slope", new IfcPlaneAngleMeasure(value))); } }
		public double Roll { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Roll", new IfcPlaneAngleMeasure(value))); } }
		public bool IsExternal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value))); } }
		public double ThermalTransmittance { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThermalTransmittance", new IfcThermalTransmittanceMeasure(value))); } }
		public bool LoadBearing { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LoadBearing", new IfcBoolean(value))); } }
		public string FireRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FireRating", new IfcLabel(value))); } }
		public Pset_ColumnCommon(IfcColumn instance) : base(instance) { }
		public Pset_ColumnCommon(IfcColumnType type) : base(type) { }
	}
	//Pset_CommunicationsAppliancePHistory
	//Pset_CommunicationsApplianceTypeCommon
	//Pset_CompressorPHistory
	//Pset_CompressorTypeCommon
	public partial class Pset_ConcreteElementGeneral : IfcPropertySet
	{
		public string ConstructionMethod { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "ConstructionMethod", new IfcLabel(value))); } }
		public string StructuralClass { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "StructuralClass", new IfcLabel(value))); } }
		public string StrengthClass { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "StrengthClass", new IfcLabel(value))); } }
		public string ExposureClass { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "ExposureClass", new IfcLabel(value))); } }
		public double ReinforcementVolumeRatio { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ReinforcementVolumeRatio", new IfcMassDensityMeasure(value))); } }
		public double ReinforcementAreaRatio { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ReinforcementAreaRatio", new IfcAreaDensityMeasure(value))); } }
		public string DimensionalAccuracyClass { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "DimensionalAccuracyClass", new IfcLabel(value))); } }
		public string ConstructionToleranceClass { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "ConstructionToleranceClass", new IfcLabel(value))); } }
		public double ConcreteCover { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ConcreteCover", new IfcPositiveLengthMeasure(value))); } }
		public double ConcreteCoverAtMainBars { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ConcreteCoverAtMainBars", new IfcPositiveLengthMeasure(value))); } }
		public double ConcreteCoverAtLinks { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ConcreteCoverAtLinks", new IfcPositiveLengthMeasure(value))); } }
		public string ReinforcementStrengthClass { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "ReinforcementStrengthClass", new IfcLabel(value))); } }
		public Pset_ConcreteElementGeneral(IfcBeam instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcBeamType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcBuildingElementProxy instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcBuildingElementProxyType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcChimney instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcChimneyType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcColumn instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcColumnType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcFooting instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcFootingType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcMember instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcMemberType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcPile instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcPileType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcPlate instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcPlateType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcRailing instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcRailingType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcRamp instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcRampType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcRampFlight instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcRampFlightType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcRoof instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcRoofType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcSlab instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcSlabType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcStair instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcStairType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcStairFlight instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcStairFlightType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcWall instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcWallType type) : base(type) { }
		public Pset_ConcreteElementGeneral(IfcCivilElement instance) : base(instance) { }
		public Pset_ConcreteElementGeneral(IfcCivilElementType type) : base(type) { }
	}
	//Pset_CondenserPHistory
	//Pset_CondenserTypeCommon
	public partial class Pset_Condition : IfcPropertySet
	{
		public DateTime AssessmentDate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AssessmentDate", new IfcDate(value))); } }
		public string AssessmentCondition { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AssessmentCondition", new IfcLabel(value))); } }
		public string AssessmentDescription { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AssessmentDescription", new IfcText(value))); } }
		public Pset_Condition(IfcElement instance) : base(instance) { }
	}
	//Pset_ConstructionResource
	//Pset_ControllerPHistory
	//Pset_ControllerTypeCommon
	//Pset_ControllerTypeFloating
	//Pset_ControllerTypeMultiPosition
	//Pset_ControllerTypeProgrammable
	//Pset_ControllerTypeProportional
	//Pset_ControllerTypeTwoPosition
	//Pset_CooledBeamPHistory
	//Pset_CooledBeamPHistoryActive
	//Pset_CooledBeamTypeActive
	//Pset_CooledBeamTypeCommon
	//Pset_CoolingTowerPHistory
	//Pset_CoolingTowerTypeCommon
	//Pset_CoveringCeiling
	//Pset_CoveringCommon
	//Pset_CoveringFlooring
	//Pset_CurtainWallCommon
	//Pset_DamperOccurrence
	//Pset_DamperPHistory
	//Pset_DamperTypeCommon
	//Pset_DamperTypeControlDamper
	//Pset_DamperTypeFireDamper
	//Pset_DamperTypeFireSmokeDamper
	//Pset_DamperTypeSmokeDamper
	//Pset_DiscreteAccessoryColumnShoe
	//Pset_DiscreteAccessoryCornerFixingPlate
	//Pset_DiscreteAccessoryDiagonalTrussConnector
	//Pset_DiscreteAccessoryEdgeFixingPlate
	//Pset_DiscreteAccessoryFixingSocket
	//Pset_DiscreteAccessoryLadderTrussConnector
	//Pset_DiscreteAccessoryStandardFixingPlate
	//Pset_DiscreteAccessoryWireLoop
	//Pset_DistributionChamberElementCommon
	//Pset_DistributionChamberElementTypeFormedDuct
	//Pset_DistributionChamberElementTypeInspectionChamber
	//Pset_DistributionChamberElementTypeInspectionPit
	//Pset_DistributionChamberElementTypeManhole
	//Pset_DistributionChamberElementTypeMeterChamber
	//Pset_DistributionChamberElementTypeSump
	//Pset_DistributionChamberElementTypeTrench
	//Pset_DistributionChamberElementTypeValveChamber
	public partial class Pset_DistributionPortCommon : IfcPropertySet
	{
		public int PortNumber { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PortNumber", new IfcInteger(value))); } }
		public string ColorCode { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ColorCode", new IfcLabel(value))); } }
		public Pset_DistributionPortCommon(IfcDistributionPort instance) : base(instance) { }
	}
	//Pset_DistributionPortPHistoryCable
	//Pset_DistributionPortPHistoryDuct
	//Pset_DistributionPortPHistoryPipe
	public partial class Pset_DistributionPortTypeCable : IfcPropertySet
	{
		public PEnum_DistributionPortElectricalType ConnectionType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ConnectionType", new IfcLabel(value.ToString()))); } }
		public string ConnectionSubType { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ConnectionSubType", new IfcLabel(value))); } }
		public PEnum_DistributionPortGender ConnectionGender { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ConnectionGender", new IfcLabel(value.ToString()))); } }
		public PEnum_ConductorFunctionEnum ConductorFunction { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ConductorFunction", new IfcLabel(value.ToString()))); } }
		public double CurrentContent3rdHarmonic { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CurrentContent3rdHarmonic", new IfcPositiveRatioMeasure(value))); } }
		public IfcPropertyBoundedValue<IfcElectricCurrentMeasure> Current { set { value.Name = "Current"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcElectricVoltageMeasure> Voltage { set { value.Name = "Voltage"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcPowerMeasure> Power { set { value.Name = "Power"; addProperty(value); } }
		public IfcPropertyListValue<IfcIdentifier> Protocols { set { value.Name = "Protocols"; addProperty(value); } }
		public Pset_DistributionPortTypeCable(IfcDistributionPort instance) : base(instance) { }
	}
	public partial class Pset_DistributionPortTypeDuct : IfcPropertySet
	{
		public PEnum_DuctConnectionType ConnectionType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ConnectionType", new IfcLabel(value.ToString()))); } }
		public string ConnectionSubType { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "ConnectionSubType", new IfcLabel(value))); } }
		public double NominalWidth { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalWidth", new IfcPositiveLengthMeasure(value))); } }
		public double NominalHeight { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalHeight", new IfcPositiveLengthMeasure(value))); } }
		public double NominalThickness { set {  AddProperty(new IfcPropertySingleValue(mDatabase, "NominalThickness", new IfcPositiveLengthMeasure(value))); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> DryBulbTemperature { set { value.Name = "DryBulbTemperature"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> WetBulbTemperature { set { value.Name = "WetBulbTemperature"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcVolumetricFlowRateMeasure> VolumetricFlowRate { set { value.Name = "VolumetricFlowRate"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcLinearVelocityMeasure> Velocity { set { value.Name = "Velocity"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcPressureMeasure> Pressure { set { value.Name = "Pressure"; addProperty(value); } }
		public Pset_DistributionPortTypeDuct(IfcDistributionPort instance) : base(instance) { }
	}
	public partial class Pset_DistributionPortTypePipe : IfcPropertySet
	{
		public PEnum_PipeEndStyleTreatment ConnectionType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ConnectionType", new IfcLabel(value.ToString()))); } }
		public string ConnectionSubType { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ConnectionSubType", new IfcLabel(value))); } }
		public double NominalDiameter { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalDiameter", new IfcPositiveLengthMeasure(value))); } }
		public double InnerDiameter { set { AddProperty(new IfcPropertySingleValue(mDatabase, "InnerDiameter", new IfcPositiveLengthMeasure(value))); } }
		public double OuterDiameter { set { AddProperty(new IfcPropertySingleValue(mDatabase, "OuterDiameter", new IfcPositiveLengthMeasure(value))); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> Temperature { set { value.Name = "Temperature"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcVolumetricFlowRateMeasure> VolumetricFlowRate { set { value.Name = "VolumetricFlowRate"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcMassFlowRateMeasure> MassFlowRate { set { value.Name = "MassFlowRate"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcLinearVelocityMeasure> Velocity { set { value.Name = "Velocity"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcPressureMeasure> Pressure { set { value.Name = "Pressure"; addProperty(value); } }
		public Pset_DistributionPortTypePipe(IfcDistributionPort instance) : base(instance) { }
	}
	//Pset_DistributionSystemCommon
	//Pset_DistributionSystemTypeElectrical
	//Pset_DistributionSystemTypeVentilation
	//Pset_DoorCommon
	//Pset_DoorWindowGlazingType
	//Pset_DuctFittingOccurrence
	//Pset_DuctFittingPHistory
	//Pset_DuctFittingTypeCommon
	//Pset_DuctSegmentOccurrence
	//Pset_DuctSegmentPHistory
	//Pset_DuctSegmentTypeCommon
	//Pset_DuctSilencerPHistory
	//Pset_DuctSilencerTypeCommon
	//Pset_ElectricalDeviceCommon
	//Pset_ElectricAppliancePHistory
	//Pset_ElectricApplianceTypeCommon
	//Pset_ElectricApplianceTypeDishwasher
	//Pset_ElectricApplianceTypeElectricCooker
	//Pset_ElectricDistributionBoardOccurrence
	//Pset_ElectricDistributionBoardTypeCommon
	//Pset_ElectricFlowStorageDeviceTypeCommon
	//Pset_ElectricGeneratorTypeCommon
	//Pset_ElectricMotorTypeCommon
	//Pset_ElectricTimeControlTypeCommon
	//Pset_ElementAssemblyCommon
	public partial class Pset_ElementComponentCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public PEnum_ElementComponentDeliveryType DeliveryType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "DeliveryType", new IfcLabel(value.ToString()))); } }
		public PEnum_ElementComponentCorrosionTreatment IfcElementComponent { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "DeliveryType", new IfcLabel(value.ToString()))); } }
		public Pset_ElementComponentCommon(IfcElementComponent instance) : base(instance) { }
		public Pset_ElementComponentCommon(IfcElementComponentType type) : base(type) { }
	}
	//Pset_EngineTypeCommon
	public partial class Pset_EnvironmentalImpactIndicators : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public string FunctionalUnitReference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FunctionalUnitReference", new IfcLabel(value))); } }
		public string Unit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Unit", new IfcText(value))); } }
		public PEnum_LifeCyclePhase LifeCyclePhase { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "LifeCyclePhase", new IfcLabel(value.ToString()))); } }
		public double ExpectedServiceLife { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ExpectedServiceLife", new IfcTimeMeasure(value))); } }
		public double TotalPrimaryEnergyConsumptionPerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TotalPrimaryEnergyConsumptionPerUnit", new IfcEnergyMeasure(value))); } }
		public double WaterConsumptionPerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WaterConsumptionPerUnit", new IfcVolumeMeasure(value))); } }
		public double HazardousWastePerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HazardousWastePerUnit", new IfcMassMeasure(value))); } }
		public double NonHazardousWastePerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NonHazardousWastePerUnit", new IfcMassMeasure(value))); } }
		public double ClimateChangePerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ClimateChangePerUnit", new IfcMassMeasure(value))); } }
		public double AtmosphericAcidificationPerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AtmosphericAcidificationPerUnit", new IfcMassMeasure(value))); } }
		public double RenewableEnergyConsumptionPerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "RenewableEnergyConsumptionPerUnit", new IfcEnergyMeasure(value))); } }
		public double NonRenewableEnergyConsumptionPerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NonRenewableEnergyConsumptionPerUnit", new IfcEnergyMeasure(value))); } }
		public double ResourceDepletionPerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ResourceDepletionPerUnit", new IfcMassMeasure(value))); } }
		public double InertWastePerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "InertWastePerUnit", new IfcMassMeasure(value))); } }
		public double RadioactiveWastePerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "RadioactiveWastePerUnit", new IfcMassMeasure(value))); } }
		public double StratosphericOzoneLayerDestructionPerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "StratosphericOzoneLayerDestructionPerUnit", new IfcMassMeasure(value))); } }
		public double PhotochemicalOzoneFormationPerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PhotochemicalOzoneFormationPerUnit", new IfcMassMeasure(value))); } }
		public double EutrophicationPerUnit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "EutrophicationPerUnit", new IfcMassMeasure(value))); } }
		public Pset_EnvironmentalImpactIndicators(IfcElement instance) : base(instance) { }
	}
	public partial class Pset_EnvironmentalImpactValues : IfcPropertySet
	{
		public double TotalPrimaryEnergyConsumption { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TotalPrimaryEnergyConsumption", new IfcEnergyMeasure(value))); } }
		public double WaterConsumption { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WaterConsumption", new IfcVolumeMeasure(value))); } }
		public double HazardousWaste{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "HazardousWaste", new IfcMassMeasure(value))); } }
		public double NonHazardousWaste{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "NonHazardousWaste", new IfcMassMeasure(value))); } }
		public double ClimateChange{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "ClimateChange", new IfcMassMeasure(value))); } }
		public double AtmosphericAcidification{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "AtmosphericAcidification", new IfcMassMeasure(value))); } }
		public double RenewableEnergyConsumption{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "RenewableEnergyConsumption", new IfcEnergyMeasure(value))); } }
		public double NonRenewableEnergyConsumption{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "NonRenewableEnergyConsumption", new IfcEnergyMeasure(value))); } }
		public double ResourceDepletion{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "ResourceDepletion", new IfcMassMeasure(value))); } }
		public double InertWaste{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "InertWaste", new IfcMassMeasure(value))); } }
		public double RadioactiveWaste{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "RadioactiveWaste", new IfcMassMeasure(value))); } }
		public double StratosphericOzoneLayerDestruction{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "StratosphericOzoneLayerDestruction", new IfcMassMeasure(value))); } }
		public double PhotochemicalOzoneFormation{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "PhotochemicalOzoneFormation", new IfcMassMeasure(value))); } }
		public double Eutrophication{ set { AddProperty(new IfcPropertySingleValue(mDatabase, "Eutrophication", new IfcMassMeasure(value))); } }
		public IfcDuration LeadInTime { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LeadInTime", value)); } }
		public IfcDuration Duration { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Duration", value)); } }
		public IfcDuration LeadOutTime { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LeadOutTime", value)); } }
		public Pset_EnvironmentalImpactValues(IfcElement instance) : base(instance) { }
	}
	//Pset_EvaporativeCoolerPHistory
	//Pset_EvaporativeCoolerTypeCommon
	//Pset_EvaporatorPHistory
	//Pset_EvaporatorTypeCommon
	//Pset_FanCentrifugal
	//Pset_FanOccurrence
	//Pset_FanPHistory
	//Pset_FanTypeCommon
	//Pset_FastenerWeld
	//Pset_FilterPHistory
	//Pset_FilterTypeAirParticleFilter
	//Pset_FilterTypeCommon
	//Pset_FilterTypeCompressedAirFilter
	//Pset_FilterTypeWaterFilter
	//Pset_FireSuppressionTerminalTypeBreechingInlet
	//Pset_FireSuppressionTerminalTypeCommon
	//Pset_FireSuppressionTerminalTypeFireHydrant
	//Pset_FireSuppressionTerminalTypeHoseReel
	//Pset_FireSuppressionTerminalTypeSprinkler
	//Pset_FlowInstrumentPHistory
	//Pset_FlowInstrumentTypeCommon
	//Pset_FlowInstrumentTypePressureGauge
	//Pset_FlowInstrumentTypeThermometer
	//Pset_FlowMeterOccurrence
	//Pset_FlowMeterTypeCommon
	//Pset_FlowMeterTypeEnergyMeter
	//Pset_FlowMeterTypeGasMeter
	//Pset_FlowMeterTypeOilMeter
	//Pset_FlowMeterTypeWaterMeter
	//Pset_FootingCommon
	//Pset_FurnitureTypeChair
	//Pset_FurnitureTypeCommon
	//Pset_FurnitureTypeDesk
	//Pset_FurnitureTypeFileCabinet
	//Pset_FurnitureTypeTable
	//Pset_HeatExchangerTypeCommon
	//Pset_HeatExchangerTypePlate
	//Pset_HumidifierPHistory
	//Pset_HumidifierTypeCommon
	//Pset_InterceptorTypeCommon
	//Pset_JunctionBoxTypeCommon
	//Pset_LampTypeCommon
	public partial class Pset_LandRegistration : IfcPropertySet
	{
		public string LandID { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LandID", new IfcIdentifier(value))); } }
		public bool IsPermanentID { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsPermanentID", new IfcBoolean(value))); } }
		public string LandTitleID { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LandTitleID", new IfcIdentifier(value))); } }
		public Pset_LandRegistration(IfcSite instance) : base(instance) { }
	}
	public partial class Pset_LightFixtureTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public int NumberOfSources { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NumberOfSources", new IfcInteger(value))); } }
		public PEnum_LightFixtureMountingType LightFixtureMountingType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "LightFixtureMountingType", new IfcLabel(value.ToString()))); } }
		public PEnum_LightFixturePlacingType LightFixturePlacingType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "LightFixturePlacingType", new IfcLabel(value.ToString()))); } }
		public double MaintenanceFactor { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MaintenanceFactor", new IfcReal(value))); } }
		public double MaximumPlenumSensibleLoad { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MaximumPlenumSensibleLoad", new IfcPowerMeasure(value))); } }
		public double MaximumSpaceSensibleLoad { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MaximumSpaceSensibleLoad", new IfcPowerMeasure(value))); } }
		public double SensibleLoadToRadiant { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SensibleLoadToRadiant", new IfcPositiveRatioMeasure(value))); } }
		public Pset_LightFixtureTypeCommon(IfcLightFixture instance) : base(instance) { }
		public Pset_LightFixtureTypeCommon(IfcLightFixtureType type) : base(type) { }
	}
	//Pset_LightFixtureTypeSecurityLighting
	public partial class Pset_ManufacturerOccurrence : IfcPropertySet
	{
		public IfcDate AcquisitionDate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AcquisitionDate", value)); } }
		public string BarCode { set { AddProperty(new IfcPropertySingleValue(mDatabase, "BarCode", new IfcIdentifier(value))); } }
		public string SerialNumber { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SerialNumber", new IfcIdentifier(value))); } }
		public string BatchReference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "BatchReference", new IfcIdentifier(value))); } }
		public PEnum_AssemblyPlace AssemblyPlace { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AssemblyPlace", new IfcLabel(value.ToString()))); } }
		public Pset_ManufacturerOccurrence(IfcElement instance) : base(instance) { }
		public Pset_ManufacturerOccurrence(IfcElementType type) : base(type) { }
	}
	public partial class Pset_ManufacturerTypeInformation : IfcPropertySet
	{
		public string GlobalTradeItemNumber { set { AddProperty(new IfcPropertySingleValue(mDatabase, "GlobalTradeItemNumber", new IfcIdentifier(value))); } }
		public string ArticleNumber { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ArticleNumber", new IfcIdentifier(value))); } }
		public string ModelReference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ModelReference", new IfcIdentifier(value))); } }
		public string ModelLabel { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ModelLabel", new IfcLabel(value))); } }
		public string Manufacturer { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Manufacturer", new IfcLabel(value))); } }
		public string ProductionYear { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ProductionYear", new IfcLabel(value))); } }
		public PEnum_AssemblyPlace AssemblyPlace { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AssemblyPlace", new IfcLabel(value.ToString()))); } }
		public Pset_ManufacturerTypeInformation(IfcElement instance) : base(instance) { }
		public Pset_ManufacturerTypeInformation(IfcElementType type) : base(type) { }
	}
	//Pset_MaterialCombustion
	public partial class Pset_MaterialCommon : IfcMaterialProperties
	{
		public double MolecularWeight { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MolecularWeight", new IfcMolecularWeightMeasure(value))); } }
		public double Porosity { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Porosity", new IfcNormalisedRatioMeasure(value))); } }
		public double MassDensity { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MassDensity", new IfcMassDensityMeasure(value))); } }
		public Pset_MaterialCommon(IfcMaterialDefinition material) : base(material) { }
	}
	public partial class Pset_MaterialConcrete : IfcMaterialProperties
	{
		public double CompressiveStrength { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CompressiveStrength", new IfcPressureMeasure(value))); } }
		public double MaxAggregateSize { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MaxAggregateSize", new IfcPositiveLengthMeasure(value))); } }
		public string AdmixturesDescription { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AdmixturesDescription", new IfcText(value))); } }
		public string Workability { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Workability", new IfcText(value))); } }
		public string WaterImpermeability { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WaterImpermeability", new IfcText(value))); } }
		public double ProtectivePoreRatio { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ProtectivePoreRatio", new IfcPositiveRatioMeasure(value))); } }
		public Pset_MaterialConcrete(IfcMaterialDefinition material) : base(material) { }
	}
	//Pset_MaterialEnergy
	//Pset_MaterialFuel
	//Pset_MaterialHygroscopic
	public partial class Pset_MaterialMechanical : IfcMaterialProperties
	{
		public double DynamicViscosity { set { AddProperty(new IfcPropertySingleValue(mDatabase, "DynamicViscosity", new IfcDynamicViscosityMeasure(value))); } }
		public double YoungModulus { set { AddProperty(new IfcPropertySingleValue(mDatabase, "YoungModulus", new IfcModulusOfElasticityMeasure(value))); } }
		public double ShearModulus { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShearModulus", new IfcModulusOfElasticityMeasure(value))); } }
		public double PoissonRatio { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PoissonRatio", new IfcPositiveRatioMeasure(value))); } }
		public double ThermalExpansionCoefficient { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThermalExpansionCoefficient", new IfcThermalExpansionCoefficientMeasure(value))); } }
		public Pset_MaterialMechanical(IfcMaterialDefinition material) : base(material) { }
	}
	//Pset_MaterialOptical
	public partial class Pset_MaterialSteel : IfcMaterialProperties
	{
		public double YieldStress { set { AddProperty(new IfcPropertySingleValue(mDatabase, "YieldStress", new IfcPressureMeasure(value))); } }
		public double UltimateStress { set { AddProperty(new IfcPropertySingleValue(mDatabase, "UltimateStress", new IfcPressureMeasure(value))); } }
		public double UltimateStrain { set { AddProperty(new IfcPropertySingleValue(mDatabase, "UltimateStrain", new IfcPositiveRatioMeasure(value))); } }
		public double HardeningModule { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HardeningModule", new IfcModulusOfElasticityMeasure(value))); } }
		public double ProportionalStress { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ProportionalStress", new IfcPressureMeasure(value))); } }
		public double PlasticStrain { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PlasticStrain", new IfcPositiveRatioMeasure(value))); } }
		public IfcPropertyTableValue<IfcNormalisedRatioMeasure, IfcNormalisedRatioMeasure> Relaxations { set { value.Name = "Relaxations"; AddProperty(value); } }
		public Pset_MaterialSteel(IfcMaterialDefinition material) : base(material) {}
	}
	//Pset_MaterialThermal
	//Pset_MaterialWater
	//Pset_MaterialWood
	public partial class Pset_MaterialWood : IfcMaterialProperties
	{
		public string Species { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Species", new IfcLabel(value))); } }
		public string StrengthGrade { set { AddProperty(new IfcPropertySingleValue(mDatabase, "StrengthGrade", new IfcLabel(value))); } }
		public string AppearanceGrade { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AppearanceGrade", new IfcLabel(value))); } }
		public string Layup { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Layup", new IfcLabel(value))); } }
		public int Layers { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Layers", new IfcInteger(value))); } }
		public int Plies { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Plies", new IfcInteger(value))); } }
		public double MoistureContent { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MoistureContent", new IfcPositiveRatioMeasure(value))); } }
		public double DimensionalChangeCoefficient { set { AddProperty(new IfcPropertySingleValue(mDatabase, "DimensionalChangeCoefficient", new IfcPositiveRatioMeasure(value))); } }
		public double ThicknessSwelling { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThicknessSwelling	", new IfcPositiveRatioMeasure(value))); } }
		public Pset_MaterialWood(IfcMaterialDefinition material) : base(material) { }
	}
	public partial class Pset_MaterialWoodBasedBeam : IfcMaterialProperties
	{
		public string ApplicableStructuralDesignMethod { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ApplicableStructuralDesignMethod", new IfcLabel(value))); } }
		public CP_MaterialMechanicalBeam InPlane { set { value.UsageName = "InPlane"; AddProperty(value); } }
		public CP_MaterialMechanicalBeam InPlaneNegative { set { value.UsageName = "InPlaneNegative"; AddProperty(value); } }
		public CP_MaterialMechanicalBeam OutOfPlane { set { value.UsageName = "OutOfPlane"; AddProperty(value); } }
		public Pset_MaterialWoodBasedBeam(IfcMaterialDefinition material) : base(material) { }
	}
	//Pset_MaterialWoodBasedPanel
	//Pset_MechanicalFastenerAnchorBolt
	//Pset_MechanicalFastenerBolt
	//Pset_MedicalDeviceTypeCommon
	public partial class Pset_MemberCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public double Span { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Span", new IfcPositiveLengthMeasure(value))); } }
		public double Slope { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Slope", new IfcPlaneAngleMeasure(value))); } }
		public double Roll { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Roll", new IfcPlaneAngleMeasure(value))); } }
		public bool IsExternal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value))); } }
		public double ThermalTransmittance { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThermalTransmittance", new IfcThermalTransmittanceMeasure(value))); } }
		public bool LoadBearing { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LoadBearing", new IfcBoolean(value))); } }
		public string FireRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FireRating", new IfcLabel(value))); } }
		public Pset_MemberCommon(IfcMember instance) : base(instance) { }
		public Pset_MemberCommon(IfcMemberType type) : base(type) { }
	}
	//Pset_MotorConnectionTypeCommon
	//Pset_OpeningElementCommon
	//Pset_OutletTypeCommon
	//Pset_OutsideDesignCriteria
	//Pset_PackingInstructions
	//Pset_Permit
	//Pset_PileCommon
	//Pset_PipeConnectionFlanged
	public partial class Pset_PipeFittingOccurrence : IfcPropertySet
	{
		public double InteriorRoughnessCoefficient { set { AddProperty(new IfcPropertySingleValue(mDatabase, "InteriorRoughnessCoefficient", new IfcPositiveLengthMeasure(value))); } }
		public string Color { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Color", new IfcLabel(value))); } }
		public Pset_PipeFittingOccurrence(IfcPipeFitting instance) : base(instance) { }
	}
	//Pset_PipeFittingPHistory
	public partial class Pset_PipeFittingTypeBend : IfcPropertySet
	{
		public double BendAngle { set { AddProperty(new IfcPropertySingleValue(mDatabase, "BendAngle", new IfcPositivePlaneAngleMeasure(value))); } }
		public double BendRadius { set { AddProperty(new IfcPropertySingleValue(mDatabase, "BendRadius", new IfcPositiveLengthMeasure(value))); } }
		public Pset_PipeFittingTypeBend(IfcPipeFitting instance) : base(instance) { }
		public Pset_PipeFittingTypeBend(IfcPipeFittingType type) : base(type) { }
	}
	public partial class Pset_PipeFittingTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public double PressureClass { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PressureClass", new IfcPressureMeasure(value))); } }
		public IfcPropertyBoundedValue<IfcPressureMeasure> PressureRange { set { value.Name = "PressureRange"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> TemperatureRange { set { value.Name = "TemperatureRange"; addProperty(value); } }
		public Pset_PipeFittingTypeCommon(IfcPipeFitting instance) : base(instance) { }
		public Pset_PipeFittingTypeCommon(IfcPipeFittingType type) : base(type) { }
	}
	public partial class Pset_PipeFittingTypeJunction : IfcPropertySet
	{
		public PEnum_PipeFittingJunctionType JunctionType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "JunctionType", new IfcLabel(value.ToString()))); } }
		public double JunctionLeftAngle { set { AddProperty(new IfcPropertySingleValue(mDatabase, "JunctionLeftAngle", new IfcPositivePlaneAngleMeasure(value))); } }
		public double JunctionLeftRadius { set { AddProperty(new IfcPropertySingleValue(mDatabase, "JunctionLeftRadius", new IfcPositiveLengthMeasure(value))); } }
		public double JunctionRightAngle { set { AddProperty(new IfcPropertySingleValue(mDatabase, "JunctionRightAngle", new IfcPositivePlaneAngleMeasure(value))); } }
		public double JunctionRightRadius { set { AddProperty(new IfcPropertySingleValue(mDatabase, "JunctionRightRadius", new IfcPositiveLengthMeasure(value))); } }
		public Pset_PipeFittingTypeJunction(IfcPipeFitting instance) : base(instance) { }
		public Pset_PipeFittingTypeJunction(IfcPipeFittingType type) : base(type) { }
	}
	//Pset_PipeSegmentOccurrence
	//Pset_PipeSegmentPHistory
	//Pset_PipeSegmentTypeCommon
	//Pset_PipeSegmentTypeCulvert
	//Pset_PipeSegmentTypeGutter
	//Pset_PlateCommon
	public partial class Pset_PrecastConcreteElementFabrication : IfcPropertySet
	{
		public string TypeDesignator { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TypeDesignator", new IfcLabel(value))); } }
		public string ProductionLotId { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ProductionLotId", new IfcIdentifier(value))); } }
		public string SerialNumber { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SerialNumber", new IfcIdentifier(value))); } }
		public string PieceMark { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PieceMark", new IfcLabel(value))); } }
		public string AsBuiltLocationNumber { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AsBuiltLocationNumber", new IfcLabel(value))); } }
		public DateTime ActualProductionDate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ActualProductionDate", new IfcDateTime(value))); } }
		public DateTime ActualErectionDate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ActualErectionDate", new IfcDateTime(value))); } }
		public Pset_PrecastConcreteElementFabrication(IfcBeam instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcBeamType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcBuildingElementProxy instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcBuildingElementProxyType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcChimney instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcChimneyType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcColumn instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcColumnType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcFooting instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcFootingType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcMember instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcMemberType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcPile instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcPileType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcPlate instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcPlateType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcRamp instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcRampType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcRampFlight instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcRampFlightType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcRoof instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcRoofType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcSlab instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcSlabType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcStair instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcStairType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcStairFlight instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcStairFlightType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcWall instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcWallType type) : base(type) { }
		public Pset_PrecastConcreteElementFabrication(IfcCivilElement instance) : base(instance) { }
		public Pset_PrecastConcreteElementFabrication(IfcCivilElementType type) : base(type) { }
	}
	public partial class Pset_PrecastConcreteElementGeneral : IfcPropertySet
	{
		public string TypeDesignator { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TypeDesignator", new IfcLabel(value))); } }
		public double CornerChamfer { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CornerChamfer", new IfcPositiveLengthMeasure(value))); } }
		public string ManufacturingToleranceClass { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ManufacturingToleranceClass", new IfcLabel(value))); } }
		public double FormStrippingStrength { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FormStrippingStrength", new IfcPressureMeasure(value))); } }
		public double LiftingStrength { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LiftingStrength", new IfcPressureMeasure(value))); } }
		public double ReleaseStrength { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ReleaseStrength", new IfcPressureMeasure(value))); } }
		public double MinimumAllowableSupportLength { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MinimumAllowableSupportLength", new IfcPositiveLengthMeasure(value))); } }
		public double InitialTension { set { AddProperty(new IfcPropertySingleValue(mDatabase, "InitialTension", new IfcPressureMeasure(value))); } }
		public double TendonRelaxation { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TendonRelaxation", new IfcPositiveRatioMeasure(value))); } }
		public double TransportationStrength { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TransportationStrength", new IfcPressureMeasure(value))); } }
		public string SupportDuringTransportDescription { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SupportDuringTransportDescription", new IfcText(value))); } }
		public IfcExternalReference SupportDuringTransportDocReference { set { AddProperty(new IfcPropertyReferenceValue("SupportDuringTransportDocReference", value)); } }
		public string HollowCorePlugging { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HollowCorePlugging", new IfcLabel(value))); } }
		public double CamberAtMidspan { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CamberAtMidspan", new IfcRatioMeasure(value))); } }
		public double BatterAtStart { set { AddProperty(new IfcPropertySingleValue(mDatabase, "BatterAtStart", new IfcPlaneAngleMeasure(value))); } }
		public double BatterAtEnd { set { AddProperty(new IfcPropertySingleValue(mDatabase, "BatterAtEnd", new IfcPlaneAngleMeasure(value))); } }
		public double Twisting { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Twisting", new IfcPlaneAngleMeasure(value))); } }
		public double Shortening { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Shortening", new IfcRatioMeasure(value))); } }
		public string PieceMark { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PieceMark", new IfcLabel(value))); } }
		public string DesignLocationNumber { set { AddProperty(new IfcPropertySingleValue(mDatabase, "DesignLocationNumber", new IfcLabel(value))); } }
		public Pset_PrecastConcreteElementGeneral(IfcBeam instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcBeamType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcBuildingElementProxy instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcBuildingElementProxyType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcChimney instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcChimneyType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcColumn instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcColumnType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcFooting instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcFootingType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcMember instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcMemberType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcPile instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcPileType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcPlate instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcPlateType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcRamp instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcRampType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcRampFlight instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcRampFlightType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcRoof instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcRoofType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcSlab instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcSlabType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcStair instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcStairType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcStairFlight instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcStairFlightType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcWall instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcWallType type) : base(type) { }
		public Pset_PrecastConcreteElementGeneral(IfcCivilElement instance) : base(instance) { }
		public Pset_PrecastConcreteElementGeneral(IfcCivilElementType type) : base(type) { }
	}
	//Pset_PrecastSlab
	//Pset_ProfileArbitraryDoubleT
	//Pset_ProfileArbitraryHollowCore
	public partial class Pset_ProfileMechanical : IfcProfileProperties
	{
		public double MassPerLength { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MassPerLength", new IfcMassPerLengthMeasure(value))); } }
		public double CrossSectionArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CrossSectionArea", new IfcAreaMeasure(value))); } }
		public double Perimeter { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Perimeter", new IfcPositiveLengthMeasure(value))); } }
		public double MinimumPlateThickness { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MinimumPlateThickness", new IfcPositiveLengthMeasure(value))); } }
		public double MaximumPlateThickness { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MaximumPlateThickness", new IfcPositiveLengthMeasure(value))); } }
		public double CentreOfGravityInX { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CentreOfGravityInX", new IfcLengthMeasure(value))); } }
		public double CentreOfGravityInY { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CentreOfGravityInY", new IfcLengthMeasure(value))); } }
		public double ShearCentreZ { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShearCentreZ", new IfcLengthMeasure(value))); } }
		public double ShearCentreY { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShearCentreY", new IfcLengthMeasure(value))); } }
		public double MomentOfInertiaY { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MomentOfInertiaY", new IfcMomentOfInertiaMeasure(value))); } }
		public double MomentOfInertiaZ { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MomentOfInertiaZ", new IfcMomentOfInertiaMeasure(value))); } }
		public double MomentOfInertiaYZ { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MomentOfInertiaYZ", new IfcMomentOfInertiaMeasure(value))); } }
		public double TorsionalConstantX { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TorsionalConstantX", new IfcMomentOfInertiaMeasure(value))); } }
		public double WarpingConstant { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WarpingConstant", new IfcWarpingConstantMeasure(value))); } }
		public double ShearDeformationAreaZ { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShearDeformationAreaZ", new IfcAreaMeasure(value))); } }
		public double ShearDeformationAreaY { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShearDeformationAreaY", new IfcAreaMeasure(value))); } }
		public double MaximumSectionModulusY { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MaximumSectionModulusY", new IfcSectionModulusMeasure(value))); } }
		public double MinimumSectionModulusY { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MinimumSectionModulusY", new IfcSectionModulusMeasure(value))); } }
		public double MaximumSectionModulusZ { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MaximumSectionModulusZ", new IfcSectionModulusMeasure(value))); } }
		public double MinimumSectionModulusZ { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MinimumSectionModulusZ", new IfcSectionModulusMeasure(value))); } }
		public double TorsionalSectionModulus { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TorsionalSectionModulus", new IfcSectionModulusMeasure(value))); } }
		public double ShearAreaZ { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShearAreaZ", new IfcAreaMeasure(value))); } }
		public double ShearAreaY { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShearAreaY", new IfcAreaMeasure(value))); } }
		public double PlasticShapeFactorY { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PlasticShapeFactorY", new IfcPositiveRatioMeasure(value))); } }
		public double PlasticShapeFactorZ { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PlasticShapeFactorZ", new IfcPositiveRatioMeasure(value))); } }
		public Pset_ProfileMechanical(IfcProfileDef profileDef) : base(profileDef) { Name = this.GetType().Name; Description = profileDef.Name; }
	}
	//Pset_ProjectOrderChangeOrder
	//Pset_ProjectOrderMaintenanceWorkOrder
	//Pset_ProjectOrderMoveOrder
	//Pset_ProjectOrderPurchaseOrder
	//Pset_ProjectOrderWorkOrder
	public partial class Pset_PropertyAgreement : IfcPropertySet
	{
		public PEnum_PropertyAgreementType AgreementType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AgreementType", new IfcLabel(value.ToString()))); } }
		public string Identifier { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Identifier", new IfcIdentifier(value))); } }
		public string Version { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Version", new IfcLabel(value))); } }
		public DateTime VersionDate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VersionDate", new IfcDate(value))); } }
		public string PropertyName { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PropertyName", new IfcLabel(value))); } }
		public DateTime CommencementDate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CommencementDate", new IfcDate(value))); } }
		public DateTime TerminationDate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TerminationDate", new IfcDate(value))); } }
		public IfcDuration Duration { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Duration", value)); } }
		public string Options { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Options", new IfcText(value))); } }
		public string ConditionCommencement { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ConditionCommencement", new IfcText(value))); } }
		public string Restrictions { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Restrictions", new IfcText(value))); } }
		public string ConditionTermination { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ConditionTermination", new IfcText(value))); } }
		public Pset_PropertyAgreement(IfcSpatialStructureElement instance) : base(instance) { }
	}
	//Pset_ProtectiveDeviceBreakerUnitI2TCurve
	//Pset_ProtectiveDeviceBreakerUnitI2TFuseCurve
	//Pset_ProtectiveDeviceBreakerUnitIPICurve
	//Pset_ProtectiveDeviceBreakerUnitTypeMCB
	//Pset_ProtectiveDeviceBreakerUnitTypeMotorProtection
	//Pset_ProtectiveDeviceOccurrence
	//Pset_ProtectiveDeviceTrippingCurve
	//Pset_ProtectiveDeviceTrippingFunctionGCurve
	//Pset_ProtectiveDeviceTrippingFunctionICurve
	//Pset_ProtectiveDeviceTrippingFunctionLCurve
	//Pset_ProtectiveDeviceTrippingFunctionSCurve
	//Pset_ProtectiveDeviceTrippingUnitCurrentAdjustment
	//Pset_ProtectiveDeviceTrippingUnitTimeAdjustment
	//Pset_ProtectiveDeviceTrippingUnitTypeCommon
	//Pset_ProtectiveDeviceTrippingUnitTypeElectroMagnetic
	//Pset_ProtectiveDeviceTrippingUnitTypeElectronic
	//Pset_ProtectiveDeviceTrippingUnitTypeResidualCurrent
	//Pset_ProtectiveDeviceTrippingUnitTypeThermal
	//Pset_ProtectiveDeviceTypeCircuitBreaker
	//Pset_ProtectiveDeviceTypeCommon
	//Pset_ProtectiveDeviceTypeEarthLeakageCircuitBreaker
	//Pset_ProtectiveDeviceTypeFuseDisconnector
	//Pset_ProtectiveDeviceTypeResidualCurrentCircuitBreaker
	//Pset_ProtectiveDeviceTypeResidualCurrentSwitch
	//Pset_ProtectiveDeviceTypeVaristor
	public partial class Pset_PumpOccurrence : IfcPropertySet
	{
		public double ImpellerDiameter { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ImpellerDiameter", new IfcPositiveLengthMeasure(value))); } }
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_PumpBaseType BaseType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "BaseType", new IfcLabel(value.ToString()))); } }
		public PEnum_PumpDriveConnectionType DriveConnectionType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "DriveConnectionType", new IfcLabel(value.ToString()))); } }
		public Pset_PumpOccurrence(IfcPump instance) : base(instance) { }
	}
	//Pset_PumpPHistory
	public partial class Pset_PumpTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public IfcPropertyBoundedValue<IfcMassFlowRateMeasure> FlowRateRange { set { value.Name = "FlowRateRange"; addProperty(value); } }
		public IfcPropertyBoundedValue<IfcPressureMeasure> FlowResistanceRange { set { value.Name = "FlowResistanceRange"; addProperty(value); } }
		public double ConnectionSize { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ConnectionSize", new IfcPositiveLengthMeasure(value))); } }
		public IfcPropertyBoundedValue<IfcThermodynamicTemperatureMeasure> TemperatureRange { set { value.Name = "TemperatureRange"; addProperty(value); } }
		public double NetPositiveSuctionHead { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NetPositiveSuctionHead", new IfcPressureMeasure(value))); } }
		public double NominalRotationSpeed { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalRotationSpeed", new IfcRotationalFrequencyMeasure(value))); } }
		public Pset_PumpTypeCommon(IfcPump instance) : base(instance) { }
		public Pset_PumpTypeCommon(IfcPumpType type) : base(type) { }
	}
   //Pset_RailingCommon
   //Pset_RampCommon
   //Pset_RampFlightCommon
   //Pset_ReinforcementBarCountOfIndependentFooting
   public partial class Pset_ReinforcementBarPitchOfBeam : IfcPropertySet
   {
      public new string Description { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Description", new IfcText(value))); } }
      public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
      public double StirrupBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "StirrupBarPitch", new IfcPositiveLengthMeasure(value))); } }
      public double SpacingBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SpacingBarPitch", new IfcPositiveLengthMeasure(value))); } }
      public Pset_ReinforcementBarPitchOfBeam(IfcBeam instance) : base(instance) { }
      public Pset_ReinforcementBarPitchOfBeam(IfcBeamType type) : base(type) { }
   }
   public partial class Pset_ReinforcementBarPitchOfColumn : IfcPropertySet
	{
		public new string Description { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Description", new IfcText(value))); } }
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_ReinforcementBarType ReinforcementBarType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ReinforcementBarType", new IfcLabel(value.ToString()))); } }
		public double HoopBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HoopBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double XDirectionTieHoopBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "XDirectionTieHoopBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double XDirectionTieHoopCount { set { AddProperty(new IfcPropertySingleValue(mDatabase, "XDirectionTieHoopCount", new IfcPositiveLengthMeasure(value))); } }
		public double YDirectionTieHoopBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "YDirectionTieHoopBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double YDirectionTieHoopCount { set { AddProperty(new IfcPropertySingleValue(mDatabase, "YDirectionTieHoopCount", new IfcPositiveLengthMeasure(value))); } }
		public Pset_ReinforcementBarPitchOfColumn(IfcColumn instance) : base(instance) { }
		public Pset_ReinforcementBarPitchOfColumn(IfcColumnType type) : base(type) { }
	}
	public partial class Pset_ReinforcementBarPitchOfContinuousFooting : IfcPropertySet
	{
		public new string Description { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Description", new IfcText(value))); } }
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public double CrossingUpperBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CrossingUpperBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double CrossingLowerBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CrossingLowerBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public Pset_ReinforcementBarPitchOfContinuousFooting(IfcFooting instance) : base(instance) { }
		public Pset_ReinforcementBarPitchOfContinuousFooting(IfcFootingType type) : base(type) { }
	}
	public partial class Pset_ReinforcementBarPitchOfSlab : IfcPropertySet
	{
		public new string Description { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Description", new IfcText(value))); } }
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public double LongOutsideTopBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LongOutsideTopBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double LongInsideCenterTopBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LongInsideCenterTopBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double LongInsideEndTopBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LongInsideEndTopBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double ShortOutsideTopBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShortOutsideTopBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double ShortInsideCenterTopBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShortInsideCenterTopBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double ShortInsideEndTopBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShortInsideEndTopBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double LongOutsideLowerBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LongOutsideLowerBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double LongInsideCenterLowerBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LongInsideCenterLowerBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double LongInsideEndLowerBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LongInsideEndLowerBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double ShortOutsideLowerBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShortOutsideLowerBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double ShortInsideCenterLowerBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShortInsideCenterLowerBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double ShortInsideEndLowerBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ShortInsideEndLowerBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public Pset_ReinforcementBarPitchOfSlab(IfcSlab instance) : base(instance) { }
		public Pset_ReinforcementBarPitchOfSlab(IfcSlabType type) : base(type) { }
	}
	public partial class Pset_ReinforcementBarPitchOfWall : IfcPropertySet
	{
		public new string Description { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Description", new IfcText(value))); } }
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_ReinforcementBarAllocationType BarAllocationType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "BarAllocationType", new IfcLabel(value.ToString()))); } }
		public double VerticalBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "VerticalBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double HorizontalBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HorizontalBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public double SpacingBarPitch { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SpacingBarPitch", new IfcPositiveLengthMeasure(value))); } }
		public Pset_ReinforcementBarPitchOfWall(IfcWall instance) : base(instance) { }
		public Pset_ReinforcementBarPitchOfWall(IfcWallType type) : base(type) { }
	}
	//Pset_Risk
	//Pset_RoofCommon
	//Pset_SanitaryTerminalTypeBath
	//Pset_SanitaryTerminalTypeBidet
	//Pset_SanitaryTerminalTypeCistern
	//Pset_SanitaryTerminalTypeCommon
	//Pset_SanitaryTerminalTypeSanitaryFountain
	//Pset_SanitaryTerminalTypeShower
	//Pset_SanitaryTerminalTypeSink
	//Pset_SanitaryTerminalTypeToiletPan
	//Pset_SanitaryTerminalTypeUrinal
	//Pset_SanitaryTerminalTypeWashHandBasin
	//Pset_SensorPHistory
	//Pset_SensorTypeCO2Sensor
	//Pset_SensorTypeCommon
	//Pset_SensorTypeConductanceSensor
	//Pset_SensorTypeContactSensor
	//Pset_SensorTypeFireSensor
	//Pset_SensorTypeFlowSensor
	//Pset_SensorTypeFrostSensor
	//Pset_SensorTypeGasSensor
	//Pset_SensorTypeHeatSensor
	//Pset_SensorTypeHumiditySensor
	//Pset_SensorTypeIdentifierSensor
	//Pset_SensorTypeIonConcentrationSensor
	//Pset_SensorTypeLevelSensor
	//Pset_SensorTypeLightSensor
	//Pset_SensorTypeMoistureSensor
	//Pset_SensorTypeMovementSensor
	//Pset_SensorTypePHSensor
	//Pset_SensorTypePressureSensor
	//Pset_SensorTypeRadiationSensor
	//Pset_SensorTypeRadioactivitySensor
	//Pset_SensorTypeSmokeSensor
	//Pset_SensorTypeSoundSensor
	//Pset_SensorTypeTemperatureSensor
	//Pset_SensorTypeWindSensor
	public partial class Pset_ServiceLife : IfcPropertySet
	{
		public IfcPropertyBoundedValue<IfcDuration> ServiceLifeDuration { set { value.Name = "ServiceLifeDuration"; addProperty(value); } }
		public IfcDuration MeanTimeBetweenFailure { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MeanTimeBetweenFailure", value)); } }
		public Pset_ServiceLife(IfcElement instance) : base(instance) { }
		public Pset_ServiceLife(IfcElementType type) : base(type) { }
	}
	//Pset_ServiceLifeFactors
	//Pset_ShadingDeviceCommon
	//Pset_ShadingDevicePHistory
	public partial class Pset_SiteCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public double BuildableArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "BuildableArea", new IfcAreaMeasure(value))); } }
		public double SiteCoverageRatio { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SiteCoverageRatio", new IfcPositiveRatioMeasure(value))); } }
		public double FloorAreaRatio { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FloorAreaRatio", new IfcPositiveRatioMeasure(value))); } }
		public double BuildingHeightLimit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "BuildingHeightLimit", new IfcPositiveLengthMeasure(value))); } }
		public double TotalArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TotalArea", new IfcAreaMeasure(value))); } }
		public Pset_SiteCommon(IfcSite instance) : base(instance) { }
	}
	public partial class Pset_SlabCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public string AcousticRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AcousticRating", new IfcLabel(value))); } }
		public string FireRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FireRating", new IfcLabel(value))); } }
		public bool Combustible { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Combustible", new IfcBoolean(value))); } }
		public string SurfaceSpreadOfFlame { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SurfaceSpreadOfFlame", new IfcLabel(value))); } }
		public double ThermalTransmittance { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThermalTransmittance", new IfcThermalTransmittanceMeasure(value))); } }
		public bool IsExternal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value))); } }
		public bool LoadBearing { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LoadBearing", new IfcBoolean(value))); } }
		public bool Compartmentation { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Compartmentation", new IfcBoolean(value))); } }
		public double PitchAngle { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PitchAngle", new IfcPlaneAngleMeasure(value))); } }
		public Pset_SlabCommon(IfcSlab instance) : base(instance) { }
		public Pset_SlabCommon(IfcSlabType type) : base(type) { }
	}
	//Pset_SolarDeviceTypeCommon
	//Pset_SoundAttenuation
	//Pset_SoundGeneration
	public partial class Pset_SpaceCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public bool IsExternal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value))); } }
		public double GrossPlannedArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "GrossPlannedArea", new IfcAreaMeasure(value))); } }
		public double NetPlannedArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NetPlannedArea", new IfcAreaMeasure(value))); } }
		public bool PubliclyAccessible { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PubliclyAccessible", new IfcBoolean(value))); } }
		public bool HandicapAccessible { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HandicapAccessible", new IfcBoolean(value))); } }
		public Pset_SpaceCommon(IfcSpace instance) : base(instance) { }
		public Pset_SpaceCommon(IfcSpaceType type) : base(type) { }
	}
	//Pset_SpaceCoveringRequirements
	//Pset_SpaceFireSafetyRequirements
	//Pset_SpaceHeaterPHistory
	public partial class Pset_SpaceHeaterTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public PEnum_SpaceHeaterPlacementType PlacementType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "PlacementType", new IfcLabel(value.ToString()))); } }
		public PEnum_SpaceHeaterTemperatureClassification TemperatureClassification { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "TemperatureClassification", new IfcLabel(value.ToString()))); } }
		public PEnum_SpaceHeaterHeatTransferDimension HeatTransferDimension { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "HeatTransferDimension", new IfcLabel(value.ToString()))); } }
		public PEnum_SpaceHeaterHeatTransferMedium HeatTransferMedium { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "HeatTransferMedium", new IfcLabel(value.ToString()))); } }
		public PEnum_EnergySource EnergySource { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "EnergySource", new IfcLabel(value.ToString()))); } }
		public double BodyMass { set { AddProperty(new IfcPropertySingleValue(mDatabase, "BodyMass", new IfcMassMeasure(value))); } }
		public double ThermalMassHeatCapacity { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThermalMassHeatCapacity", new IfcReal(value))); } }
		public double OutputCapacity { set { AddProperty(new IfcPropertySingleValue(mDatabase, "OutputCapacity", new IfcPowerMeasure(value))); } }
		public double ThermalEfficiency { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThermalEfficiency", new IfcNormalisedRatioMeasure(value))); } }
		public int NumberOfPanels { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NumberOfPanels", new IfcInteger(value))); } }
		public int NumberOfSections { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NumberOfSections", new IfcInteger(value))); } }
		public Pset_SpaceHeaterTypeCommon(IfcSpaceHeater instance) : base(instance) { }
		public Pset_SpaceHeaterTypeCommon(IfcSpaceHeaterType type) : base(type) { }
	}
	public partial class Pset_SpaceHeaterTypeConvector : IfcPropertySet
	{
		public PEnum_SpaceHeaterConvectorType ConvectorType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ConvectorType", new IfcLabel(value.ToString()))); } }
		public Pset_SpaceHeaterTypeConvector(IfcSpaceHeater instance) : base(instance) { }
		public Pset_SpaceHeaterTypeConvector(IfcSpaceHeaterType spaceHeaterType) : base(spaceHeaterType) { }
	}
	public partial class Pset_SpaceHeaterTypeRadiator : IfcPropertySet
	{
		public PEnum_SpaceHeaterRadiatorType RadiatorType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "RadiatorType", new IfcLabel(value.ToString()))); } }
		public double TubingLength { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TubingLength", new IfcPositiveLengthMeasure(value))); } }
		public double WaterContent { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WaterContent", new IfcMassMeasure(value))); } }
		public Pset_SpaceHeaterTypeRadiator(IfcSpaceHeater instance) : base(instance) { }
		public Pset_SpaceHeaterTypeRadiator(IfcSpaceHeaterType type) : base(type) { }
	}
	//Pset_SpaceLightingRequirements
	public partial class Pset_SpaceOccupancyRequirements : IfcPropertySet
	{
		public string OccupancyType { set { AddProperty(new IfcPropertySingleValue(mDatabase, "OccupancyType", new IfcLabel(value))); } }
		public double OccupancyNumber { set { AddProperty(new IfcPropertySingleValue(mDatabase, "OccupancyNumber", new IfcCountMeasure(value))); } }
		public double OccupancyNumberPeak { set { AddProperty(new IfcPropertySingleValue(mDatabase, "OccupancyNumberPeak", new IfcCountMeasure(value))); } }
		public double OccupancyTimePerDay { set { AddProperty(new IfcPropertySingleValue(mDatabase, "OccupancyTimePerDay", new IfcTimeMeasure(value))); } }
		public double AreaPerOccupant { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AreaPerOccupant", new IfcAreaMeasure(value))); } }
		public double MinimumHeadroom { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MinimumHeadroom", new IfcLengthMeasure(value))); } }
		public bool IsOutlookDesirable { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsOutlookDesirable", new IfcBoolean(value))); } }
		public Pset_SpaceOccupancyRequirements(IfcSpace instance) : base(instance) { }
		public Pset_SpaceOccupancyRequirements(IfcSpaceType type) : base(type) { }
		public Pset_SpaceOccupancyRequirements(IfcSpatialZone instance) : base(instance) { }
		public Pset_SpaceOccupancyRequirements(IfcSpatialZoneType type) : base(type) { }
		public Pset_SpaceOccupancyRequirements(IfcZone instance) : base(instance) { }
	}
	//Pset_SpaceParking
	//Pset_SpaceThermalDesign
	//Pset_SpaceThermalLoad
	//Pset_SpaceThermalLoadPHistory
	//Pset_SpaceThermalPHistory
	//Pset_SpaceThermalRequirements
	//Pset_SpatialZoneCommon
	//Pset_StackTerminalTypeCommon
	//Pset_StairCommon
	//Pset_StairFlightCommon
	//Pset_StructuralSurfaceMemberVaryingThickness
	//Pset_SwitchingDeviceTypeCommon
	//Pset_SwitchingDeviceTypeContactor
	//Pset_SwitchingDeviceTypeDimmerSwitch
	//Pset_SwitchingDeviceTypeEmergencyStop
	//Pset_SwitchingDeviceTypeKeypad
	//Pset_SwitchingDeviceTypeMomentarySwitch
	//Pset_SwitchingDeviceTypePHistory
	//Pset_SwitchingDeviceTypeSelectorSwitch
	//Pset_SwitchingDeviceTypeStarter
	//Pset_SwitchingDeviceTypeSwitchDisconnector
	//Pset_SwitchingDeviceTypeToggleSwitch
	//Pset_SystemFurnitureElementTypeCommon
	//Pset_SystemFurnitureElementTypePanel
	//Pset_SystemFurnitureElementTypeWorkSurface
	//Pset_TankOccurrence
	//Pset_TankTypeCommon
	//Pset_TankTypeExpansion
	//Pset_TankTypePreformed
	//Pset_TankTypePressureVessel
	//Pset_TankTypeSectional
	//Pset_ThermalLoadAggregate
	//Pset_ThermalLoadDesignCriteria
	//Pset_TransformerTypeCommon
	//Pset_TransportElementCommon
	//Pset_TransportElementElevator
	//Pset_TubeBundleTypeCommon
	//Pset_TubeBundleTypeFinned
	//Pset_UnitaryControlElementPHistory
	//Pset_UnitaryControlElementTypeCommon
	//Pset_UnitaryControlElementTypeIndicatorPanel
	//Pset_UnitaryControlElementTypeThermostat
	//Pset_UnitaryEquipmentTypeAirConditioningUnit
	public partial class Pset_UnitaryEquipmentTypeAirHandler : IfcPropertySet
	{
		public PEnum_AirHandlerConstruction AirHandlerConstruction { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AirHandlerConstruction", new IfcLabel(value.ToString()))); } }
		public PEnum_AirHandlerFanCoilArrangement AirHandlerFanCoilArrangement { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AirHandlerFanCoilArrangement", new IfcLabel(value.ToString()))); } }
		public bool DualDeck { set { AddProperty(new IfcPropertySingleValue(mDatabase, "DualDeck", new IfcBoolean(value))); } }
		public Pset_UnitaryEquipmentTypeAirHandler(IfcUnitaryEquipment instance) : base(instance) { }
		public Pset_UnitaryEquipmentTypeAirHandler(IfcUnitaryEquipmentType type) : base(type) { }
	}
	public partial class Pset_UnitaryEquipmentTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public Pset_UnitaryEquipmentTypeCommon(IfcUnitaryEquipment instance) : base(instance) { }
		public Pset_UnitaryEquipmentTypeCommon(IfcUnitaryEquipmentType type) : base(type) { }
	}
	//Pset_UtilityConsumptionPHistory
	//Pset_ValvePHistory
	//Pset_ValveTypeAirRelease
	public partial class Pset_ValveTypeCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_ValvePattern ValvePattern { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ValvePattern", new IfcLabel(value.ToString()))); } }
		public PEnum_ValveOperation ValveOperation { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ValveOperation", new IfcLabel(value.ToString()))); } }
		public PEnum_ValveMechanism ValveMechanism { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "ValveMechanism", new IfcLabel(value.ToString()))); } }
		public double Size { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Size", new IfcPositiveLengthMeasure(value))); } }
		public double TestPressure { set { AddProperty(new IfcPropertySingleValue(mDatabase, "TestPressure", new IfcPressureMeasure(value))); } }
		public double WorkingPressure { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WorkingPressure", new IfcPressureMeasure(value))); } }
		public double FlowCoefficient { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FlowCoefficient", new IfcReal(value))); } }
		public double CloseOffRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CloseOffRating", new IfcPressureMeasure(value))); } }
		public Pset_ValveTypeCommon(IfcValve instance) : base(instance) { }
		public Pset_ValveTypeCommon(IfcValveType type) : base(type) { }
	}
	//Pset_ValveTypeDrawOffCock
	//Pset_ValveTypeFaucet
	//Pset_ValveTypeFlushing
	//Pset_ValveTypeGasTap
	//Pset_ValveTypeIsolating
	//Pset_ValveTypeMixing
	//Pset_ValveTypePressureReducing
	//Pset_ValveTypePressureRelief
	//Pset_VibrationIsolatorTypeCommon
	public partial class Pset_WallCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public string AcousticRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AcousticRating", new IfcLabel(value))); } }
		public string FireRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FireRating", new IfcLabel(value))); } }
		public bool Combustible { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Combustible", new IfcBoolean(value))); } }
		public string SurfaceSpreadOfFlame { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SurfaceSpreadOfFlame", new IfcLabel(value))); } }
		public double ThermalTransmittance { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThermalTransmittance", new IfcThermalTransmittanceMeasure(value))); } }
		public bool IsExternal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value))); } }
		public bool ExtendToStructure { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ExtendToStructure", new IfcBoolean(value))); } }
		public bool LoadBearing { set { AddProperty(new IfcPropertySingleValue(mDatabase, "LoadBearing", new IfcBoolean(value))); } }
		public bool Compartmentation { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Compartmentation", new IfcBoolean(value))); } }
		public Pset_WallCommon(IfcWall instance) : base(instance) { }
		public Pset_WallCommon(IfcWallType type) : base(type) { }
	}
	public partial class Pset_Warranty : IfcPropertySet
	{
		public string WarrantyIdentifier { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WarrantyIdentifier", new IfcIdentifier(value))); } }
		public DateTime WarrantyStartDate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WarrantyStartDate", new IfcDate(value))); } }
		public DateTime WarrantyEndDate { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WarrantyEndDate", new IfcDate(value))); } }
		public bool IsExtendedWarranty { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsExtendedWarranty", new IfcBoolean(value))); } }
		public IfcDuration WarrantyPeriod { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WarrantyPeriod", value)); } }
		public string WarrantyContent { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WarrantyContent", new IfcText(value))); } }
		public string PointOfContact { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PointOfContact", new IfcLabel(value))); } }
		public string Exclusions { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Exclusions", new IfcText(value))); } }
		public Pset_Warranty(IfcElement instance) : base(instance) { }
	}
	//Pset_WasteTerminalTypeCommon
	//Pset_WasteTerminalTypeFloorTrap
	//Pset_WasteTerminalTypeFloorWaste
	//Pset_WasteTerminalTypeGullySump
	//Pset_WasteTerminalTypeGullyTrap
	//Pset_WasteTerminalTypeRoofDrain
	//Pset_WasteTerminalTypeWasteDisposalUnit
	//Pset_WasteTerminalTypeWasteTrap
	public partial class Pset_WindowCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public PEnum_Status Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public string AcousticRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AcousticRating", new IfcLabel(value))); } }
		public string FireRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FireRating", new IfcLabel(value))); } }
		public string SecurityRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SecurityRating", new IfcLabel(value))); } }
		public bool IsExternal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value))); } }
		public double Infiltration { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Infiltration", new IfcVolumetricFlowRateMeasure(value))); } }
		public double ThermalTransmittance { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ThermalTransmittance", new IfcThermalTransmittanceMeasure(value))); } }
		public double GlazingAreaFraction { set { AddProperty(new IfcPropertySingleValue(mDatabase, "GlazingAreaFraction", new IfcPositiveRatioMeasure(value))); } }
		public bool HasSillExternal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HasSillExternal", new IfcBoolean(value))); } }
		public bool HasSillInternal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HasSillInternal", new IfcBoolean(value))); } }
		public bool HasDrive { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HasDrive", new IfcBoolean(value))); } }
		public bool SmokeStop { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SmokeStop", new IfcBoolean(value))); } }
		public bool FireExit { set { AddProperty(new IfcPropertySingleValue(mDatabase, "FireExit", new IfcBoolean(value))); } }
		public string WaterTightnessRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WaterTightnessRating", new IfcLabel(value))); } }
		public string MechanicalLoadRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "MechanicalLoadRating", new IfcLabel(value))); } }
		public string WindLoadRating { set { AddProperty(new IfcPropertySingleValue(mDatabase, "WindLoadRating", new IfcLabel(value))); } }
		public Pset_WindowCommon(IfcWindow instance) : base(instance) { }
		public Pset_WindowCommon(IfcWindowType type) : base(type) { }
	}
	//Pset_WorkControlCommon
	public partial class Pset_ZoneCommon : IfcPropertySet
	{
		public string Reference { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Reference", new IfcIdentifier(value))); } }
		public bool IsExternal { set { AddProperty(new IfcPropertySingleValue(mDatabase, "IsExternal", new IfcBoolean(value))); } }
		public double GrossPlannedArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "GrossPlannedArea", new IfcAreaMeasure(value))); } }
		public double NetPlannedArea { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NetPlannedArea", new IfcAreaMeasure(value))); } }
		public bool PubliclyAccessible { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PubliclyAccessible", new IfcBoolean(value))); } }
		public bool HandicapAccessible { set { AddProperty(new IfcPropertySingleValue(mDatabase, "HandicapAccessible", new IfcBoolean(value))); } }
		public Pset_ZoneCommon(IfcZone instance) : base(instance) { }
	}

	public class CP_MaterialMechanicalBeam : IfcComplexProperty
	{
		public double YoungModulus { set { addProperty(new IfcPropertySingleValue(mDatabase, "YoungModulus", new IfcModulusOfElasticityMeasure(value))); } }
		public double YoungModulusMin { set { addProperty(new IfcPropertySingleValue(mDatabase, "YoungModulusMin", new IfcModulusOfElasticityMeasure(value))); } }
		public double YoungModulusPerp { set { addProperty(new IfcPropertySingleValue(mDatabase, "YoungModulusPerp", new IfcModulusOfElasticityMeasure(value))); } }
		public double YoungModulusPerpMin { set { addProperty(new IfcPropertySingleValue(mDatabase, "YoungModulusPerpMin", new IfcModulusOfElasticityMeasure(value))); } }
		public double ShearModulus { set { addProperty(new IfcPropertySingleValue(mDatabase, "ShearModulus", new IfcModulusOfElasticityMeasure(value))); } }
		public double ShearModulusMin { set { addProperty(new IfcPropertySingleValue(mDatabase, "ShearModulusMin", new IfcModulusOfElasticityMeasure(value))); } }
		public double BendingStrength { set { addProperty(new IfcPropertySingleValue(mDatabase, "BendingStrength", new IfcPressureMeasure(value))); } }
		public double TensileStrength { set { addProperty(new IfcPropertySingleValue(mDatabase, "TensileStrength", new IfcPressureMeasure(value))); } }
		public double TensileStrengthPerp { set { addProperty(new IfcPropertySingleValue(mDatabase, "TensileStrengthPerp", new IfcPressureMeasure(value))); } }
		public double CompStrength { set { addProperty(new IfcPropertySingleValue(mDatabase, "CompStrength", new IfcPressureMeasure(value))); } }
		public double CompStrengthPerp { set { addProperty(new IfcPropertySingleValue(mDatabase, "CompStrengthPerp", new IfcPressureMeasure(value))); } }
		public double RaisedCompStrengthPerp { set { addProperty(new IfcPropertySingleValue(mDatabase, "RaisedCompStrengthPerp", new IfcPressureMeasure(value))); } }
		public double ShearStrength { set { addProperty(new IfcPropertySingleValue(mDatabase, "ShearStrength", new IfcPressureMeasure(value))); } }
		public double TorsionalStrength { set { addProperty(new IfcPropertySingleValue(mDatabase, "TorsionalStrength", new IfcPressureMeasure(value))); } }
		public double ReferenceDepth { set { addProperty(new IfcPropertySingleValue(mDatabase, "ReferenceDepth", new IfcPositiveLengthMeasure(value))); } }
		public IfcPropertyTableValue<IfcPositiveRatioMeasure, IfcPositiveRatioMeasure> InstabilityFactors { set { value.Name = "InstabilityFactors"; addProperty(value); } }
		public CP_MaterialMechanicalBeam(DatabaseIfc db, string usageName) : base(db, "CP_MaterialMechanicalBeam", usageName) { }
	}


	public partial class COBie_ActionRequest : IfcPropertySet
	{
		public PEnum_ActionRequestType Type { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Type", new IfcLabel(value.ToString()))); } }
		public PEnum_ActionRequestRisk Risk { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Risk", new IfcLabel(value.ToString()))); } }
		public PEnum_ActionRequestChance Chance { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Chance", new IfcLabel(value.ToString()))); } }
		public PEnum_ActionRequestImpact Impact { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Impact", new IfcLabel(value.ToString()))); } }
		public COBie_ActionRequest(IfcActionRequest instance) : base(instance) { }
	}
	public partial class COBie_ConstructionProductResource : IfcPropertySet
	{
		public PEnum_ConstructionProductResourceCategory Category { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Category", new IfcLabel(value.ToString()))); } }
		public COBie_ConstructionProductResource(IfcConstructionProductResource instance) : base(instance) { }
	}
	public partial class COBie_ConstructionProductResourceType : IfcPropertySet
	{
		public PEnum_ConstructionProductResourceTypeCategory Category { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Category", new IfcLabel(value.ToString()))); } }
		public string SetNumber { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SetNumber", new IfcLabel(value))); } }
		public string PartNumber { set { AddProperty(new IfcPropertySingleValue(mDatabase, "PartNumber", new IfcLabel(value))); } }
		public COBie_ConstructionProductResourceType(IfcConstructionProductResourceType instance) : base(instance) { }
	}
	public partial class COBie_ElementType : IfcPropertySet
	{
		public PEnum_AssetType AssetType { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "AssetType", new IfcLabel(value.ToString()))); } }
		public double ReplacementCost { set { AddProperty(new IfcPropertySingleValue(mDatabase, "ReplacementCost", new IfcMonetaryMeasure(value))); } }
		public double NominalLength { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalLength", new IfcPositiveLengthMeasure(value))); } }
		public double NominalWidth { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalWidth", new IfcPositiveLengthMeasure(value))); } }
		public double NominalHeight { set { AddProperty(new IfcPropertySingleValue(mDatabase, "NominalHeight", new IfcPositiveLengthMeasure(value))); } }
		public string Shape { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Shape", new IfcLabel(value))); } }
		public string Size { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Size", new IfcLabel(value))); } }
		public string Color { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Color", new IfcLabel(value))); } }
		public string Finish { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Finish", new IfcLabel(value))); } }
		public string Features { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Features", new IfcText(value))); } }
		public string AccessibilityPerformance { set { AddProperty(new IfcPropertySingleValue(mDatabase, "AccessibilityPerformance", new IfcLabel(value))); } }
		public string CodePerformance { set { AddProperty(new IfcPropertySingleValue(mDatabase, "CodePerformance", new IfcLabel(value))); } }
		public string SustainabilityPerformance { set { AddProperty(new IfcPropertySingleValue(mDatabase, "SustainabilityPerformance", new IfcLabel(value))); } }
		public string Grade { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Grade", new IfcLabel(value))); } }
		public string Material { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Material", new IfcLabel(value))); } }
		public string Constituents { set { AddProperty(new IfcPropertySingleValue(mDatabase, "Constituents", new IfcText(value))); } }
		public COBie_ElementType(IfcElementType type) : base(type) { }
	}
	public partial class COBie_Task : IfcPropertySet
	{
		public PEnum_TaskCategory Category { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Category", new IfcLabel(value.ToString()))); } }
		public PEnum_TaskStatus Status { set { AddProperty(new IfcPropertyEnumeratedValue(mDatabase, "Status", new IfcLabel(value.ToString()))); } }
		public COBie_Task(IfcTask instance) : base(instance) { }
	}
}
