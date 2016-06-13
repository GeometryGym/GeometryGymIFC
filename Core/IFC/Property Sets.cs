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
	public partial class Pset_DistributionPortTypeDuct : IfcPropertySet
	{
		public PEnum_DuctConnectionType ConnectionType { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "ConnectionType", new IfcLabel(value.ToString())).mIndex); } }
		public double NominalWidth { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalWidth", new IfcPositiveLengthMeasure(value)).mIndex); } }
		public double NominalHeight { set { mHasProperties.Add(new IfcPropertySingleValue(mDatabase, "NominalHeight", new IfcPositiveLengthMeasure(value)).mIndex); } }
		//if (!string.IsNullOrEmpty(connectionSubType))
		//	props.Add(new IfcPropertySingleValue(mDatabase, Pset_DistributionPortTypeDuct.mConnectionSubTypeLabel, "", new IfcLabel(connectionSubType)));
		//if (nominalWidth > tol)
		//	props.Add(new IfcPropertySingleValue(mDatabase, Pset_DistributionPortTypeDuct.mNominalWidthLabel, "", new IfcPositiveLengthMeasure(nominalWidth)));
		//if (nominalHeight > tol)
		//	props.Add(new IfcPropertySingleValue(mDatabase, Pset_DistributionPortTypeDuct.mNominalHeightLabel, "", new IfcPositiveLengthMeasure(nominalHeight)));
		//if (Math.Abs(dryBulbTemperature) > tol)
		//	props.Add(new IfcPropertySingleValue(mDatabase, Pset_DistributionPortTypeDuct.mDryBulbTemperatureLabel, "", new IfcThermodynamicTemperatureMeasure(dryBulbTemperature)));
		//if (Math.Abs(wetBulbTemperature) > tol)
		//	props.Add(new IfcPropertySingleValue(mDatabase, Pset_DistributionPortTypeDuct.mWetBulbTemperatureLabel, "", new IfcThermodynamicTemperatureMeasure(wetBulbTemperature)));
		//if (volumetricFlowRate > tol)
		//	props.Add(new IfcPropertySingleValue(mDatabase, Pset_DistributionPortTypeDuct.mVolumetricFlowRateLabel, "", new IfcVolumetricFlowRateMeasure(volumetricFlowRate)));
		//if (velocity > tol)
		//	props.Add(new IfcPropertySingleValue(mDatabase, Pset_DistributionPortTypeDuct.mVelocityLabel, "", new IfcLinearVelocityMeasure(velocity)));
		//if (pressure > tol)
		//	props.Add(new IfcPropertySingleValue(mDatabase, Pset_DistributionPortTypeDuct.mPressureLabel, "", new IfcPressureMeasure(pressure)));
		public Pset_DistributionPortTypeDuct(DatabaseIfc db) : base(db, "Pset_DistributionPortTypeDuct") { }
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
}
