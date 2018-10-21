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
	public abstract class IfcValue : IfcMetricValueSelect //SELECT(IfcMeasureValue,IfcSimpleValue,IfcDerivedMeasureValue); stpentity parse method
	{
		public abstract object Value { get; set; }
		public abstract Type ValueType { get; }
		public abstract string ValueString { get; }
	}  
	[Serializable]
	public partial class IfcValve : IfcFlowController //IFC4
	{
		internal IfcValveTypeEnum mPredefinedType = IfcValveTypeEnum.NOTDEFINED;// OPTIONAL : IfcValveTypeEnum;
		public IfcValveTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcValve() : base() { }
		internal IfcValve(DatabaseIfc db, IfcValve v, IfcOwnerHistory ownerHistory, bool downStream) : base(db, v, ownerHistory, downStream) { mPredefinedType = v.mPredefinedType; }
		public IfcValve(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcValveType : IfcFlowControllerType
	{
		internal IfcValveTypeEnum mPredefinedType = IfcValveTypeEnum.NOTDEFINED;// : IfcValveTypeEnum; 
		public IfcValveTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcValveType() : base() { }
		internal IfcValveType(DatabaseIfc db, IfcValveType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcValveType(DatabaseIfc m, string name, IfcValveTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcVector : IfcGeometricRepresentationItem
	{
		internal int mOrientation; // : IfcDirection;
		internal double mMagnitude;// : IfcLengthMeasure; 

		public IfcDirection Orientation { get { return mDatabase[mOrientation] as IfcDirection; } set { mOrientation = value.mIndex; } }
		public double Magnitude { get { return mMagnitude; } set { mMagnitude = value; } }

		internal IfcVector() : base() { }
		internal IfcVector(DatabaseIfc db, IfcVector v) : base(db,v) { Orientation = db.Factory.Duplicate( v.Orientation) as IfcDirection; mMagnitude = v.mMagnitude; }
		public IfcVector(IfcDirection orientation, double magnitude) : base(orientation.mDatabase) { Orientation = orientation; Magnitude = magnitude; }
	}
	[Serializable]
	public partial class IfcVertex : IfcTopologicalRepresentationItem //SUPERTYPE OF(IfcVertexPoint)
	{
		internal IfcVertex() : base() { }
		internal IfcVertex(DatabaseIfc db, IfcVertex v) : base(db,v) { }
		public IfcVertex(DatabaseIfc db) : base(db) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcVertexBasedTextureMap : BaseClassIfc // DEPRECEATED IFC4
	{
		internal List<int> mTextureVertices = new List<int>();// LIST [3:?] OF IfcTextureVertex;
		internal List<int> mTexturePoints = new List<int>();// LIST [3:?] OF IfcCartesianPoint; 

		internal IfcVertexBasedTextureMap() : base() { }
		internal IfcVertexBasedTextureMap(IfcVertexBasedTextureMap m) : base() { mTextureVertices = new List<int>(m.mTextureVertices.ToArray()); mTexturePoints = new List<int>(m.mTexturePoints.ToArray()); }
	}
	[Serializable]
	public partial class IfcVertexloop : IfcLoop
	{
		internal int mLoopVertex;// : IfcVertex; 
		public IfcVertex LoopVertex { get { return mDatabase[mLoopVertex] as IfcVertex; } set { mLoopVertex = value.mIndex; } }

		internal IfcVertexloop() : base() { }
		internal IfcVertexloop(DatabaseIfc db, IfcVertexloop l) : base(db,l) { LoopVertex = db.Factory.Duplicate(l.LoopVertex) as IfcVertex; }
	}
	[Serializable]
	public partial class IfcVertexPoint : IfcVertex, IfcPointOrVertexPoint
	{
		internal int mVertexGeometry;// : IfcPoint; 
		public IfcPoint VertexGeometry { get { return mDatabase[mVertexGeometry] as IfcPoint; } set { mVertexGeometry = value.mIndex; } }

		internal IfcVertexPoint() : base() { }
		internal IfcVertexPoint(DatabaseIfc db, IfcVertexPoint v) : base(db,v) { VertexGeometry = db.Factory.Duplicate(v.VertexGeometry) as IfcPoint; }
		public IfcVertexPoint(IfcPoint p) : base(p.mDatabase) { VertexGeometry = p; }
	}
	[Serializable]
	public partial class IfcVibrationIsolator : IfcElementComponent
	{
		internal IfcVibrationIsolatorTypeEnum mPredefinedType = IfcVibrationIsolatorTypeEnum.NOTDEFINED;// : OPTIONAL IfcVibrationIsolatorTypeEnum;
		public IfcVibrationIsolatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcVibrationIsolator() : base() { }
		internal IfcVibrationIsolator(DatabaseIfc db, IfcVibrationIsolator i, IfcOwnerHistory ownerHistory, bool downStream) : base(db, i, ownerHistory, downStream) { mPredefinedType = i.mPredefinedType; }
		public IfcVibrationIsolator(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcVibrationIsolatorType : IfcElementComponentType
	{
		internal IfcVibrationIsolatorTypeEnum mPredefinedType = IfcVibrationIsolatorTypeEnum.NOTDEFINED;// : IfcVibrationIsolatorTypeEnum
		public IfcVibrationIsolatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcVibrationIsolatorType() : base() { }
		internal IfcVibrationIsolatorType(DatabaseIfc db, IfcVibrationIsolatorType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcVibrationIsolatorType(DatabaseIfc m, string name, IfcVibrationIsolatorTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcVirtualElement : IfcElement
	{
		internal IfcVirtualElement() : base() { }
		internal IfcVirtualElement(DatabaseIfc db, IfcVirtualElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { }
		public IfcVirtualElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
	}
	[Serializable]
	public partial class IfcVirtualGridIntersection : BaseClassIfc
	{
		private Tuple<int,int> mIntersectingAxes = new Tuple<int,int>(0,0);// : LIST [2:2] OF UNIQUE IfcGridAxis;
		private Tuple<double,double,double> mOffsetDistances = null;// : LIST [2:3] OF IfcLengthMeasure; 
		public Tuple<IfcGridAxis,IfcGridAxis> IntersectingAxes { get { return new Tuple<IfcGridAxis, IfcGridAxis>(mDatabase[mIntersectingAxes.Item1] as IfcGridAxis, mDatabase[mIntersectingAxes.Item2] as IfcGridAxis); } set { mIntersectingAxes = new Tuple<int, int>(value.Item1.mIndex, value.Item2.mIndex); } }
		internal IfcVirtualGridIntersection() : base() { }
		internal IfcVirtualGridIntersection(DatabaseIfc db, IfcVirtualGridIntersection i) : base(db, i) { Tuple<IfcGridAxis, IfcGridAxis> axes = i.IntersectingAxes; IntersectingAxes = new Tuple<IfcGridAxis,IfcGridAxis>(db.Factory.Duplicate(axes.Item1) as IfcGridAxis, db.Factory.Duplicate(axes.Item2) as IfcGridAxis); mOffsetDistances = i.mOffsetDistances; }
	}
	[Serializable]
	public partial class IfcVoidingFeature : IfcFeatureElementSubtraction //IFC4
	{
		internal IfcVoidingFeatureTypeEnum mPredefinedType = IfcVoidingFeatureTypeEnum.NOTDEFINED;// :IfcVoidingFeatureTypeEnum;  
		public IfcVoidingFeatureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcVoidingFeature() : base() { }
		internal IfcVoidingFeature(DatabaseIfc db, IfcVoidingFeature v, IfcOwnerHistory ownerHistory, bool downStream) : base(db, v, ownerHistory, downStream) { mPredefinedType = v.mPredefinedType; }
		public IfcVoidingFeature(IfcElement host,IfcProductRepresentation rep,IfcVoidingFeatureTypeEnum type) : base(host,rep) { mPredefinedType = type; }
	}

	[Serializable]
	public abstract class IfcDerivedMeasureValue : IfcValue
	{
		internal double mValue;
		public override object Value { get { return mValue; } set { mValue = Convert.ToDouble(value); } }
		public override Type ValueType => typeof(double);
		public double Measure { get { return mValue; } set { mValue = value; } }
		protected IfcDerivedMeasureValue(double value) { mValue = value; }
		public override string ToString() { return this.GetType().Name.ToUpper() + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public override string ValueString => Value.ToString();
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
		public IfcCompoundPlaneAngleMeasure(double angleDegrees) //: base(angleDegrees)
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
		internal IfcCompoundPlaneAngleMeasure(int degrees, int minutes, int seconds, int microSeconds)
			//:base(degrees + minutes/60 + seconds / 60 / 60 + microSeconds / 60 / 60 / 1e-6)
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
			if (double.TryParse(text, out angle))
				return new IfcCompoundPlaneAngleMeasure(angle);
			string str = text.Replace("(", "").Replace(")", "");
			string[] fields = str.Split(",".ToCharArray());
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
		internal double computeAngle()
		{
			double compound = Math.Abs(mMinutes) / 60.0 + Math.Abs(mSeconds) / 3600.0 + Math.Abs(mMicroSeconds) / 3600 * 1e-6;
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
	public class IfcLinearStiffnessMeasure : IfcDerivedMeasureValue { public IfcLinearStiffnessMeasure(double value) : base(value) { } }
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
	public class IfcModulusOfSubgradeReactionMeasure : IfcDerivedMeasureValue { public IfcModulusOfSubgradeReactionMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcModulusOfRotationalSubgradeReactionMeasure : IfcDerivedMeasureValue { public IfcModulusOfRotationalSubgradeReactionMeasure(double value) : base(value) { } }
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
	public abstract class IfcMeasureValue : IfcValue //TYPE IfcMeasureValue = SELECT (IfcVolumeMeasure,IfcTimeMeasure ,IfcThermodynamicTemperatureMeasure ,IfcSolidAngleMeasure ,IfcPositiveRatioMeasure
	{ //,IfcRatioMeasure ,IfcPositivePlaneAngleMeasure ,IfcPlaneAngleMeasure ,IfcParameterValue ,IfcNumericMeasure ,IfcMassMeasure ,IfcPositiveLengthMeasure,IfcLengthMeasure ,IfcElectricCurrentMeasure ,
      //IfcDescriptiveMeasure ,IfcCountMeasure ,IfcContextDependentMeasure ,IfcAreaMeasure ,IfcAmountOfSubstanceMeasure ,IfcLuminousIntensityMeasure ,IfcNormalisedRatioMeasure ,IfcComplexNumber);
		internal double mValue;
		public override object Value { get { return mValue; } set { mValue = Convert.ToDouble(value); } }
		public override Type ValueType => typeof(double);
		public double Measure { get { return mValue; } set { mValue = value; } }
		protected IfcMeasureValue(double value) { mValue = value; }
		public override string ToString() { return this.GetType().Name.ToUpper() + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public override string ValueString => Value.ToString();
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
	public class IfcLengthMeasure : IfcMeasureValue, IfcSizeSelect, IfcBendingParameterSelect { public IfcLengthMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcLuminousIntensityMeasure : IfcMeasureValue { public IfcLuminousIntensityMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcMassMeasure : IfcMeasureValue { public IfcMassMeasure(double value) : base(value) { } }
	[Serializable]
	public partial class IfcNormalisedRatioMeasure : IfcMeasureValue, IfcColourOrFactor { public IfcNormalisedRatioMeasure(double value) : base(Math.Min(1, Math.Max(0, value))) { }  }
	[Serializable]
	public class IfcNumericMeasure : IfcMeasureValue { public IfcNumericMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcParameterValue : IfcMeasureValue { public IfcParameterValue(double value) : base(value) { } }
	[Serializable]
	public class IfcPlaneAngleMeasure : IfcMeasureValue, IfcBendingParameterSelect { public IfcPlaneAngleMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcPositivePlaneAngleMeasure : IfcMeasureValue { public IfcPositivePlaneAngleMeasure(double value) : base(value) { } }
	[Serializable]
	public class IfcPositiveLengthMeasure : IfcMeasureValue, IfcSizeSelect
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
			if (!double.TryParse(str.Substring(icounter), out mValue))
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
			if (!double.TryParse(str.Substring(icounter), out mValue))
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
	public abstract class IfcSimpleValue : IfcValue // = SELECT(IfcInteger,IfcReal,IfcBoolean,IfcIdentifier,IfcText,IfcLabel,IfcLogical,IfcBinary);
	{
		public override string ValueString { get { return Value.ToString(); } }
	}
	//IfcBinary
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
		internal static string FormatSTEP(DateTime date) { return date.Year + (date.Month < 10 ? "-0" : "-") + date.Month + (date.Day < 10 ? "-0" : "-") + date.Day; }
		internal static string STEPAttribute(DateTime dateTime) { return dateTime == DateTime.MinValue ? "$" : "'" + FormatSTEP(dateTime) + "'"; }
		internal static DateTime ParseSTEP(string date)
		{
			if (string.Compare(date, "$") == 0)
				return DateTime.MinValue;
			string str = date.Replace("'", "");
			string[] fields = str.Split("-".ToCharArray());
			return new DateTime(int.Parse(fields[0]), int.Parse(fields[1]), int.Parse(fields[2]));
		}
		public override string ValueString => FormatSTEP(mDate);
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
					double seconds = double.Parse(value.Substring(17, value.Length - 17));
					return new DateTime(year, month, day, hour, min, (int)seconds);
				}
				return new DateTime(year, month, day);
			}
			catch (Exception) { }
			DateTime result = DateTime.MinValue;
			return (DateTime.TryParse(value, out result) ? result : DateTime.MinValue);
		}
		internal static IfcDateTimeSelect convertDateTimeSelect(DatabaseIfc m, DateTime date)
		{
			IfcCalendarDate cd = new IfcCalendarDate(m, date.Day, date.Month, date.Year);
			if (date.Hour + date.Minute + date.Second < m.Tolerance)
				return cd;
			return new IfcDateAndTime(cd, new IfcLocalTime(m, date.Hour, date.Minute, date.Second));
		}
		public override string ValueString => FormatSTEP(mDateTime);
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
		public static string Convert(IfcDuration d) { return (d == null ? "$" : d.ToString()); }
		public static IfcDuration Convert(string s) { return null; }

		public override string ValueString => "P" + mYears + "Y" + mMonths + "M" + mDays + "DT" + mHours + "H" + mMinutes + "M" + mSeconds.ToString(ParserSTEP.NumberFormat) + "S";
		public override string ToString() { return "IFCDURATION('" + ValueString + "'"; }
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
		public override string ToString() { return "IFCIDENTIFIER('" + ParserIfc.Encode( Identifier) + "')"; }
	}
	[Serializable]
	public partial class IfcInteger : IfcSimpleValue
	{
		public int Magnitude { get; set; } = 0;
		public override object Value { get { return Magnitude; } set { Magnitude = Convert.ToInt32(value); } }
		public override Type ValueType { get { return typeof(int); } }
		public IfcInteger(int value) { Magnitude = value; }
		public override string ToString() { return "IFCINTEGER(" + Magnitude.ToString() + ")"; }
	}
	[Serializable]
	public partial class IfcLabel : IfcSimpleValue
	{
		public string Label { get; set; }
		public override object Value { get { return Label; } set { Label = value.ToString(); } }
		public override Type ValueType => typeof(string);
		public IfcLabel(string value) { Label = value; }
		public override string ToString() { return "IFCLABEL('" + ParserIfc.Encode(Label) + "')"; }
	}
	[Serializable]
	public partial class IfcLogical : IfcSimpleValue
	{
		internal IfcLogicalEnum mValue;
		public override object Value
		{
			get { return mValue; }
			set
			{
				if (!Enum.TryParse<IfcLogicalEnum>(value.ToString(), true, out mValue))
				{
					bool boolean = Convert.ToBoolean(value);
					mValue = (boolean ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE);
				}
			}
		}
		public override Type ValueType { get { return typeof(IfcLogicalEnum); } }
		public IfcLogical(IfcLogicalEnum value) { mValue = value; }
		public IfcLogical(bool value) { mValue = value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE; }
		public override string ToString() { return "IFCLOGICAL(" + ParserIfc.LogicalToString(mValue) + ")"; }
	}
	[Serializable]
	public partial class IfcPositiveInteger : IfcSimpleValue
	{
		public int Magnitude { get; set; } = 0;
		public override object Value { get { return Magnitude; } set { Magnitude = Convert.ToInt32(value); } }
		public override Type ValueType { get { return typeof(int); } }
		public IfcPositiveInteger(int value) { Magnitude = value; }
		public override string ToString() { return "IFCINTEGER(" + Magnitude.ToString() + ")"; }
	}
	[Serializable]
	public partial class IfcReal : IfcSimpleValue
	{
		public double Magnitude { get; set; }
		public override object Value { get { return Magnitude; } set { Magnitude = Convert.ToInt32(value); } }
		public override Type ValueType { get { return typeof(double); } }
		public IfcReal(double value) { Magnitude = value; }
		public override string ToString() { return "IFCREAL(" + ParserSTEP.DoubleToString(Magnitude) + ")"; }
	}
	[Serializable]
	public partial class IfcText : IfcSimpleValue
	{
		public string Text { get; set; }
		public override object Value { get { return Text; } set { Text = value.ToString(); } }
		public override Type ValueType { get { return typeof(string); } }
		public IfcText(string value) { Text = value; }
		public override string ToString() { return "IFCTEXT('" + ParserIfc.Encode(Text) + "')"; }
	}
	[Serializable]
	public partial class IfcTime : IfcSimpleValue
	{
		internal string mTime = "$";
		public override string ToString() { return mTime; }
		public override object Value { get { return parseSTEP(mTime); } set { mTime = formatSTEP(Convert.ToDateTime(value)); } }
		public override Type ValueType { get { return typeof(DateTime); } }
		internal static string formatSTEP(DateTime date) { return (date.Hour < 10 ? "T0" : "T") + date.Hour + (date.Minute < 10 ? ":0" : ":") + date.Minute + (date.Second < 10 ? ":0" : ":") + date.Second + "'"; }
		internal static DateTime parseSTEP(string str)
		{
			string value = str.Replace("'", "");
			if (string.IsNullOrEmpty(value) || value == "$")
				return DateTime.MinValue;
			try
			{
				DateTime min = DateTime.MinValue;
				int hour = int.Parse(value.Substring(1, 2)), minute = int.Parse(value.Substring(4, 2));
				double seconds = double.Parse(value.Substring(7, value.Length - 7));
				return new DateTime(min.Year, min.Month, min.Day, hour, minute, (int)seconds);
			}
			catch (Exception) { }
			DateTime result = DateTime.MinValue;
			return (DateTime.TryParse(value, out result) ? result : DateTime.MinValue);
		}
		internal static string convert(DateTime date) { return (date.Hour < 10 ? "T0" : "T") + date.Hour + (date.Minute < 10 ? "-0" : "-") + date.Minute + (date.Second < 10 ? "-0" : "-") + date.Second; }
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
		public override Type ValueType => typeof(Uri);
		public IfcURIReference(string value) { mValue = value; }
		public override string ToString() { return "IFCURIREFERENCE('" + ParserIfc.Encode(mValue) + "')"; }
	}

	[Serializable]
	public partial class IfcSpecularExponent : IfcValue, IfcSpecularHighlightSelect
	{
		internal double mValue;
		public override object Value { get { return mValue; } set { mValue = Convert.ToInt32(value); } }
		public override Type ValueType { get { return typeof(double); } }
		public IfcSpecularExponent(double value) { mValue = value; }
		public override string ToString() { return "IFCSPECULAREXPONENT(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public override string ValueString => Value.ToString();
	}
	[Serializable]
	public partial class IfcSpecularRoughness : IfcValue, IfcSpecularHighlightSelect
	{
		internal double mValue;
		public override object Value { get { return mValue; } set { mValue = Convert.ToInt32(value); } }
		public override Type ValueType { get { return typeof(double); } }
		public IfcSpecularRoughness(double value) { mValue = Math.Min(1, Math.Max(0, value)); }
		public override string ToString() { return "IFCSPECULARROUGHNESS(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public override string ValueString => Value.ToString();
	}
	public interface IfcSizeSelect { } //TYPE IfcSizeSelect = SELECT (IfcRatioMeasure ,IfcLengthMeasure ,IfcDescriptiveMeasure ,IfcPositiveLengthMeasure ,IfcNormalisedRatioMeasure ,IfcPositiveRatioMeasure);  
}
