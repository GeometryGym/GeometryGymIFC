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
	public abstract class IfcValue { public abstract object Value { get; } }  //SELECT(IfcMeasureValue,IfcSimpleValue,IfcDerivedMeasureValue); stpentity parse method
	public partial class IfcValve : IfcFlowController //IFC4
	{
		internal IfcValveTypeEnum mPredefinedType = IfcValveTypeEnum.NOTDEFINED;// OPTIONAL : IfcValveTypeEnum;
		public IfcValveTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcValve() : base() { }
		internal IfcValve(DatabaseIfc db, IfcValve v) : base(db, v) { mPredefinedType = v.mPredefinedType; }
		public IfcValve(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcValve s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcValveTypeEnum)Enum.Parse(typeof(IfcValveTypeEnum), str);
		}
		internal new static IfcValve Parse(string strDef) { IfcValve s = new IfcValve(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcValveTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcValveType : IfcFlowControllerType
	{
		internal IfcValveTypeEnum mPredefinedType = IfcValveTypeEnum.NOTDEFINED;// : IfcValveTypeEnum; 
		public IfcValveTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcValveType() : base() { }
		internal IfcValveType(DatabaseIfc db, IfcValveType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcValveType(DatabaseIfc m, string name, IfcValveTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcValveType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcValveTypeEnum)Enum.Parse(typeof(IfcValveTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcValveType Parse(string strDef) { IfcValveType t = new IfcValveType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcVector : IfcGeometricRepresentationItem
	{
		internal int mOrientation; // : IfcDirection;
		internal double mMagnitude;// : IfcLengthMeasure; 

		public IfcDirection Orientation { get { return mDatabase[mOrientation] as IfcDirection; } set { mOrientation = value.mIndex; } }
		public double Magnitude { get { return mMagnitude; } set { mMagnitude = value; } }

		internal IfcVector() : base() { }
		internal IfcVector(DatabaseIfc db, IfcVector v) : base(db,v) { Orientation = db.Factory.Duplicate( v.Orientation) as IfcDirection; mMagnitude = v.mMagnitude; }
		public IfcVector(IfcDirection orientation, double magnitude) : base(orientation.Database) { Orientation = orientation; Magnitude = magnitude; }
	
		internal static IfcVector Parse(string str)
		{
			IfcVector v = new IfcVector();
			int pos = 0, len = str.Length;
			v.mOrientation = ParserSTEP.StripLink(str, ref pos, len);
			v.mMagnitude = ParserSTEP.StripDouble(str, ref pos, len);
			return v;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mOrientation) + "," + ParserSTEP.DoubleToString(mMagnitude); }
	}
	public partial class IfcVertex : IfcTopologicalRepresentationItem //SUPERTYPE OF(IfcVertexPoint)
	{
		internal IfcVertex() : base() { }
		internal IfcVertex(DatabaseIfc db) : base(db) { }
		internal IfcVertex(DatabaseIfc db, IfcVertex v) : base(db,v) { }
		internal static IfcVertex Parse(string str) { return new IfcVertex(); }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcVertexBasedTextureMap : BaseClassIfc // DEPRECEATED IFC4
	{
		internal List<int> mTextureVertices = new List<int>();// LIST [3:?] OF IfcTextureVertex;
		internal List<int> mTexturePoints = new List<int>();// LIST [3:?] OF IfcCartesianPoint; 

		internal IfcVertexBasedTextureMap() : base() { }
		internal IfcVertexBasedTextureMap(IfcVertexBasedTextureMap m) : base() { mTextureVertices = new List<int>(m.mTextureVertices.ToArray()); mTexturePoints = new List<int>(m.mTexturePoints.ToArray()); }
		internal static IfcVertexBasedTextureMap Parse(string strDef) { IfcVertexBasedTextureMap m = new IfcVertexBasedTextureMap(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcVertexBasedTextureMap m, List<string> arrFields, ref int ipos) { m.mTextureVertices = ParserSTEP.SplitListLinks(arrFields[ipos++]); m.mTexturePoints = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mTextureVertices[0]);
			for (int icounter = 1; icounter < mTextureVertices.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mTextureVertices[icounter]);
			str += "),(" + ParserSTEP.LinkToString(mTexturePoints[0]);
			for (int icounter = 1; icounter < mTexturePoints.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mTexturePoints[icounter]);
			return str;
		}
	}
	public partial class IfcVertexloop : IfcLoop
	{
		internal int mLoopVertex;// : IfcVertex; 
		public IfcVertex LoopVertex { get { return mDatabase[mLoopVertex] as IfcVertex; } set { mLoopVertex = value.mIndex; } }

		internal IfcVertexloop() : base() { }
		internal IfcVertexloop(DatabaseIfc db, IfcVertexloop l) : base(db,l) { LoopVertex = db.Factory.Duplicate(l.LoopVertex) as IfcVertex; }
		internal static IfcVertexloop Parse(string str)
		{
			IfcVertexloop l = new IfcVertexloop();
			int pos = 0;
			l.mLoopVertex = ParserSTEP.StripLink(str, ref pos, str.Length);
			return l;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mLoopVertex); }
	}
	public partial class IfcVertexPoint : IfcVertex, IfcPointOrVertexPoint
	{
		internal int mVertexGeometry;// : IfcPoint; 
		public IfcPoint VertexGeometry { get { return mDatabase[mVertexGeometry] as IfcPoint; } set { mVertexGeometry = value.mIndex; } }

		internal IfcVertexPoint() : base() { }
		internal IfcVertexPoint(DatabaseIfc db, IfcVertexPoint v) : base(db,v) { VertexGeometry = db.Factory.Duplicate(v.VertexGeometry) as IfcPoint; }
		public IfcVertexPoint(IfcPoint p) : base(p.mDatabase) { VertexGeometry = p; }
		internal new static IfcVertexPoint Parse(string str) { IfcVertexPoint v = new IfcVertexPoint(); int pos = 0;v.mVertexGeometry = ParserSTEP.StripLink(str, ref pos, str.Length); return v; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mVertexGeometry); }
	}
	public partial class IfcVibrationIsolator : IfcElementComponent
	{
		internal IfcVibrationIsolatorTypeEnum mPredefinedType = IfcVibrationIsolatorTypeEnum.NOTDEFINED;// : OPTIONAL IfcVibrationIsolatorTypeEnum;
		public IfcVibrationIsolatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcVibrationIsolator() : base() { }
		internal IfcVibrationIsolator(DatabaseIfc db, IfcVibrationIsolator i) : base(db, i) { mPredefinedType = i.mPredefinedType; }
		public IfcVibrationIsolator(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcVibrationIsolator Parse(string strDef) { int ipos = 0; IfcVibrationIsolator a = new IfcVibrationIsolator(); parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcVibrationIsolator a, List<string> arrFields, ref int ipos)
		{
			IfcElementComponent.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcVibrationIsolatorTypeEnum)Enum.Parse(typeof(IfcVibrationIsolatorTypeEnum), s.Replace(".", ""));
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcVibrationIsolatorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
	}
	public partial class IfcVibrationIsolatorType : IfcElementComponentType
	{
		internal IfcVibrationIsolatorTypeEnum mPredefinedType = IfcVibrationIsolatorTypeEnum.NOTDEFINED;// : IfcVibrationIsolatorTypeEnum
		public IfcVibrationIsolatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcVibrationIsolatorType() : base() { }
		internal IfcVibrationIsolatorType(DatabaseIfc db, IfcVibrationIsolatorType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal static void parseFields(IfcVibrationIsolatorType t, List<string> arrFields, ref int ipos) { IfcDiscreteAccessoryType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcVibrationIsolatorTypeEnum)Enum.Parse(typeof(IfcVibrationIsolatorTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcVibrationIsolatorType Parse(string strDef) { IfcVibrationIsolatorType t = new IfcVibrationIsolatorType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcVirtualElement : IfcElement
	{
		internal IfcVirtualElement() : base() { }
		internal IfcVirtualElement(DatabaseIfc db, IfcVirtualElement e) : base(db, e,false) { }
		public IfcVirtualElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
		internal static IfcProduct Parse(string strDef) { IfcVirtualElement e = new IfcVirtualElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		internal static void parseFields(IfcVirtualElement e, List<string> arrFields, ref int ipos) { IfcElement.parseFields(e, arrFields, ref ipos); }
	}
	public partial class IfcVirtualGridIntersection : BaseClassIfc
	{
		private Tuple<int,int> mIntersectingAxes = new Tuple<int,int>(0,0);// : LIST [2:2] OF UNIQUE IfcGridAxis;
		private Tuple<double,double,double> mOffsetDistances = null;// : LIST [2:3] OF IfcLengthMeasure; 
		public Tuple<IfcGridAxis,IfcGridAxis> IntersectingAxes { get { return new Tuple<IfcGridAxis, IfcGridAxis>(mDatabase[mIntersectingAxes.Item1] as IfcGridAxis, mDatabase[mIntersectingAxes.Item2] as IfcGridAxis); } set { mIntersectingAxes = new Tuple<int, int>(value.Item1.mIndex, value.Item2.mIndex); } }
		internal IfcVirtualGridIntersection() : base() { }
		internal IfcVirtualGridIntersection(DatabaseIfc db, IfcVirtualGridIntersection i) : base(db, i) { Tuple<IfcGridAxis, IfcGridAxis> axes = i.IntersectingAxes; IntersectingAxes = new Tuple<IfcGridAxis,IfcGridAxis>(db.Factory.Duplicate(axes.Item1) as IfcGridAxis, db.Factory.Duplicate(axes.Item2) as IfcGridAxis); mOffsetDistances = i.mOffsetDistances; }
		internal static IfcVirtualGridIntersection Parse(string strDef) { IfcVirtualGridIntersection i = new IfcVirtualGridIntersection(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcVirtualGridIntersection i, List<string> arrFields, ref int ipos)
		{
			List<int> links = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			i.mIntersectingAxes = new Tuple<int, int>(links[0], links[1]);
			List<string> lst = ParserSTEP.SplitLineFields(arrFields[ipos++]);
			i.mOffsetDistances = new Tuple<double,double,double>(ParserSTEP.ParseDouble(lst[0]), ParserSTEP.ParseDouble(lst[1]),(lst.Count > 2 ? ParserSTEP.ParseDouble(lst[2]) : double.NaN));
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(#" + mIntersectingAxes.Item1 + ",#" + mIntersectingAxes.Item2 + "),(";
			str += ParserSTEP.DoubleToString(mOffsetDistances.Item1) + "," + ParserSTEP.DoubleToString(mOffsetDistances.Item2);
			if (!double.IsNaN(mOffsetDistances.Item3))
				str += "," + ParserSTEP.DoubleToString(mOffsetDistances.Item3);
			str += ")";
			return str;
		}
	}
	public partial class IfcVoidingFeature : IfcFeatureElementSubtraction //IFC4
	{
		internal IfcVoidingFeatureTypeEnum mPredefinedType = IfcVoidingFeatureTypeEnum.NOTDEFINED;// :IfcVoidingFeatureTypeEnum;  
		public IfcVoidingFeatureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcVoidingFeature() : base() { }
		internal IfcVoidingFeature(DatabaseIfc db, IfcVoidingFeature v) : base(db,v) { mPredefinedType = v.mPredefinedType; }
		public IfcVoidingFeature(IfcElement host,IfcProductRepresentation rep,IfcVoidingFeatureTypeEnum type) : base(host,rep) { mPredefinedType = type; }
		
		internal static IfcVoidingFeature Parse(string strDef, ReleaseVersion schema) { IfcVoidingFeature e = new IfcVoidingFeature(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return e; }
		internal static void parseFields(IfcVoidingFeature e, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcFeatureElementSubtraction.parseFields(e, arrFields, ref ipos);
			if (schema != ReleaseVersion.IFC2x3)
				e.mPredefinedType = (IfcVoidingFeatureTypeEnum)Enum.Parse(typeof(IfcVoidingFeatureTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : ",." + mPredefinedType + "."); }
	}

	public abstract class IfcDerivedMeasureValue : IfcValue
	{
		internal double mValue;
		public override object Value { get { return mValue; } }
		public double Measure { get { return mValue; } set { mValue = value; } }
		protected IfcDerivedMeasureValue(double value) { mValue = value; }
		public override string ToString() { return this.GetType().Name.ToUpper() + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
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

	public class IfcAbsorbedDoseMeasure : IfcDerivedMeasureValue { public IfcAbsorbedDoseMeasure(double value) : base(value) { } }
	public class IfcAccelerationMeasure : IfcDerivedMeasureValue { public IfcAccelerationMeasure(double value) : base(value) { } }
	public class IfcAngularVelocityMeasure : IfcDerivedMeasureValue { public IfcAngularVelocityMeasure(double value) : base(value) { } }
	public class IfcAreaDensityMeasure : IfcDerivedMeasureValue { public IfcAreaDensityMeasure(double value) : base(value) { } }
	public class IfcCompoundPlaneAngleMeasure //: IfcDerivedMeasureValue
	{
		internal int mDegrees = 0, mMinutes = 0, mSeconds = 0, mMicroSeconds = 0;
		public IfcCompoundPlaneAngleMeasure(double angleDegrees) //: base(angleDegrees)
		{
			double ang = Math.Abs(angleDegrees);
			int sign = angleDegrees < 0 ? -1 : 1;

			mDegrees = sign * (int)Math.Floor(ang);
			mMinutes = sign * (int)Math.Floor((ang - mDegrees) * 60.0);
			mSeconds = sign * (int)Math.Floor(((ang - mDegrees) * 60 - mMinutes) * 60);
			mMicroSeconds = sign * (int)Math.Floor((((ang - mDegrees) * 60 - mMinutes) * 60 - mSeconds) * 1e6);

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
	public class IfcCurvatureMeasure : IfcDerivedMeasureValue { public IfcCurvatureMeasure(double value) : base(value) { } }
	public class IfcDoseEquivalentMeasure : IfcDerivedMeasureValue { public IfcDoseEquivalentMeasure(double value) : base(value) { } }
	public class IfcDynamicViscosityMeasure : IfcDerivedMeasureValue { public IfcDynamicViscosityMeasure(double value) : base(value) { } }
	public class IfcElectricCapacitanceMeasure : IfcDerivedMeasureValue { public IfcElectricCapacitanceMeasure(double value) : base(value) { } }
	public class IfcElectricChargeMeasure : IfcDerivedMeasureValue { public IfcElectricChargeMeasure(double value) : base(value) { } }
	public class IfcElectricConductanceMeasure : IfcDerivedMeasureValue { public IfcElectricConductanceMeasure(double value) : base(value) { } }
	public class IfcElectricResistanceMeasure : IfcDerivedMeasureValue { public IfcElectricResistanceMeasure(double value) : base(value) { } }
	public class IfcElectricVoltageMeasure : IfcDerivedMeasureValue { public IfcElectricVoltageMeasure(double value) : base(value) { } }
	public class IfcEnergyMeasure : IfcDerivedMeasureValue { public IfcEnergyMeasure(double value) : base(value) { } }
	public class IfcForceMeasure : IfcDerivedMeasureValue { public IfcForceMeasure(double value) : base(value) { } }
	public class IfcFrequencyMeasure : IfcDerivedMeasureValue { public IfcFrequencyMeasure(double value) : base(value) { } }
	public class IfcHeatFluxDensityMeasure : IfcDerivedMeasureValue { public IfcHeatFluxDensityMeasure(double value) : base(value) { } }
	public class IfcHeatingValueMeasure : IfcDerivedMeasureValue { public IfcHeatingValueMeasure(double value) : base(value) { } }
	public class IfcIlluminanceMeasure : IfcDerivedMeasureValue { public IfcIlluminanceMeasure(double value) : base(value) { } } 
	public class IfcInductanceMeasure : IfcDerivedMeasureValue { public IfcInductanceMeasure(double value) : base(value) { } } 
	public class IfcIntegerCountRateMeasure : IfcDerivedMeasureValue
	{
		public IfcIntegerCountRateMeasure(double value) : base((int)value) { }
		public override string ToString() { return this.GetType().Name.ToUpper() + "(" + (int)mValue + ")"; }
	}
	public class IfcIonConcentrationMeasure : IfcDerivedMeasureValue { public IfcIonConcentrationMeasure(double value) : base(value) { } } 
	public class IfcIsothermalMoistureCapacityMeasure : IfcDerivedMeasureValue { public IfcIsothermalMoistureCapacityMeasure(double value) : base(value) { } } 
	public class IfcKinematicViscosityMeasure : IfcDerivedMeasureValue { public IfcKinematicViscosityMeasure(double value) : base(value) { } } 
	public class IfcLinearForceMeasure : IfcDerivedMeasureValue { public IfcLinearForceMeasure(double value) : base(value) { } }
	public class IfcLinearMomentMeasure : IfcDerivedMeasureValue { public IfcLinearMomentMeasure(double value) : base(value) { } }
	public class IfcLinearStiffnessMeasure : IfcDerivedMeasureValue { public IfcLinearStiffnessMeasure(double value) : base(value) { } }
	public class IfcLinearVelocityMeasure : IfcDerivedMeasureValue { public IfcLinearVelocityMeasure(double value) : base(value) { } }
	public class IfcLuminousFluxMeasure : IfcDerivedMeasureValue { public IfcLuminousFluxMeasure(double value) : base(value) { } }
	public class IfcLuminousIntensityDistributionMeasure : IfcDerivedMeasureValue { public IfcLuminousIntensityDistributionMeasure(double value) : base(value) { } }
	public class IfcLuminousMeasure : IfcDerivedMeasureValue { public IfcLuminousMeasure(double value) : base(value) { } }
	public class IfcMagneticFluxDensityMeasure : IfcDerivedMeasureValue { public IfcMagneticFluxDensityMeasure(double value) : base(value) { } }
	public class IfcMagneticFluxMeasure : IfcDerivedMeasureValue { public IfcMagneticFluxMeasure(double value) : base(value) { } }
	public class IfcMassDensityMeasure : IfcDerivedMeasureValue { public IfcMassDensityMeasure(double value) : base(value) { } }
	public class IfcMassFlowRateMeasure : IfcDerivedMeasureValue { public IfcMassFlowRateMeasure(double value) : base(value) { } }
	public class IfcMassPerLengthMeasure : IfcDerivedMeasureValue { public IfcMassPerLengthMeasure(double value) : base(value) { } }
	public class IfcModulusOfElasticityMeasure : IfcDerivedMeasureValue { public IfcModulusOfElasticityMeasure(double value) : base(value) { } }
	public class IfcModulusOfSubgradeReactionMeasure : IfcDerivedMeasureValue { public IfcModulusOfSubgradeReactionMeasure(double value) : base(value) { } }
	public class IfcModulusOfRotationalSubgradeReactionMeasure : IfcDerivedMeasureValue { public IfcModulusOfRotationalSubgradeReactionMeasure(double value) : base(value) { } }
	public class IfcMoistureDiffusivityMeasure : IfcDerivedMeasureValue { public IfcMoistureDiffusivityMeasure(double value) : base(value) { } }
	public class IfcMolecularWeightMeasure : IfcDerivedMeasureValue { public IfcMolecularWeightMeasure(double value) : base(value) { } }
	public class IfcMomentOfInertiaMeasure : IfcDerivedMeasureValue { public IfcMomentOfInertiaMeasure(double value) : base(value) { } }
	public class IfcMonetaryMeasure : IfcDerivedMeasureValue { public IfcMonetaryMeasure(double value) : base(value) { } }//, IfcAppliedValueSelect
	public class IfcPHMeasure : IfcDerivedMeasureValue { public IfcPHMeasure(double value) : base(value) { } }
	public class IfcPlanarForceMeasure : IfcDerivedMeasureValue { public IfcPlanarForceMeasure(double value) : base(value) { } }
	public class IfcPowerMeasure : IfcDerivedMeasureValue { public IfcPowerMeasure(double value) : base(value) { } }
	public class IfcPressureMeasure : IfcDerivedMeasureValue { public IfcPressureMeasure(double value) : base(value) { } } 
	public class IfcRadioActivityMeasure : IfcDerivedMeasureValue { public IfcRadioActivityMeasure(double value) : base(value) { } }
	public class IfcRotationalFrequencyMeasure : IfcDerivedMeasureValue { public IfcRotationalFrequencyMeasure(double value) : base(value) { } }
	public class IfcRotationalStiffnessMeasure : IfcDerivedMeasureValue { public IfcRotationalStiffnessMeasure(double value) : base(value) { } }
	public class IfcSectionalAreaIntegralMeasure : IfcDerivedMeasureValue { public IfcSectionalAreaIntegralMeasure(double value) : base(value) { } }
	public class IfcSectionModulusMeasure : IfcDerivedMeasureValue { public IfcSectionModulusMeasure(double value) : base(value) { } }
	public class IfcShearModulusMeasure : IfcDerivedMeasureValue { public IfcShearModulusMeasure(double value) : base(value) { } }
	public class IfcSoundPowerMeasure : IfcDerivedMeasureValue { public IfcSoundPowerMeasure(double value) : base(value) { } }
	public class IfcSoundPressureMeasure : IfcDerivedMeasureValue { public IfcSoundPressureMeasure(double value) : base(value) { } }
	public class IfcSpecificHeatCapacityMeasure : IfcDerivedMeasureValue { public IfcSpecificHeatCapacityMeasure(double value) : base(value) { } }
	public class IfcTemperatureGradientMeasure : IfcDerivedMeasureValue { public IfcTemperatureGradientMeasure(double value) : base(value) { } }
	public class IfcThermalAdmittanceMeasure : IfcDerivedMeasureValue { public IfcThermalAdmittanceMeasure(double value) : base(value) { } }
	public class IfcThermalConductivityMeasure : IfcDerivedMeasureValue { public IfcThermalConductivityMeasure(double value) : base(value) { } }
	public class IfcThermalExpansionCoefficientMeasure : IfcDerivedMeasureValue { public IfcThermalExpansionCoefficientMeasure(double value) : base(value) { } }
	public class IfcThermalTransmittanceMeasure : IfcDerivedMeasureValue { public IfcThermalTransmittanceMeasure(double value) : base(value) { } }
	public class IfcTimeStamp : IfcDerivedMeasureValue
	{
		public IfcTimeStamp(double value) : base((int)value) { }
		public override string ToString() { return this.GetType().Name.ToUpper() + "(" + (int)mValue + ")"; }
	}
	public class IfcTorqueMeasure : IfcDerivedMeasureValue { public IfcTorqueMeasure(double value) : base(value) { } }
	public class IfcVaporPermeabilityMeasure : IfcDerivedMeasureValue { public IfcVaporPermeabilityMeasure(double value) : base(value) { } }// IfcMeasureValue
	public class IfcVolumetricFlowRateMeasure : IfcDerivedMeasureValue { public IfcVolumetricFlowRateMeasure(double value) : base(value) { } }// IfcMeasureValue
	public class IfcWarpingConstantMeasure : IfcDerivedMeasureValue { public IfcWarpingConstantMeasure(double value) : base(value) { } }
	public class IfcWarpingMomentMeasure : IfcDerivedMeasureValue { public IfcWarpingMomentMeasure(double value) : base(value) { } }

	public abstract class IfcMeasureValue : IfcValue //TYPE IfcMeasureValue = SELECT (IfcVolumeMeasure,IfcTimeMeasure ,IfcThermodynamicTemperatureMeasure ,IfcSolidAngleMeasure ,IfcPositiveRatioMeasure
	{ //,IfcRatioMeasure ,IfcPositivePlaneAngleMeasure ,IfcPlaneAngleMeasure ,IfcParameterValue ,IfcNumericMeasure ,IfcMassMeasure ,IfcPositiveLengthMeasure,IfcLengthMeasure ,IfcElectricCurrentMeasure ,
      //IfcDescriptiveMeasure ,IfcCountMeasure ,IfcContextDependentMeasure ,IfcAreaMeasure ,IfcAmountOfSubstanceMeasure ,IfcLuminousIntensityMeasure ,IfcNormalisedRatioMeasure ,IfcComplexNumber);
		internal double mValue;
		public override object Value { get { return mValue; } }
		public double Measure { get { return mValue; } set { mValue = value; } }
		protected IfcMeasureValue(double value) { mValue = value; }
		public override string ToString() { return this.GetType().Name.ToUpper() + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
	}
	public class IfcAreaMeasure : IfcMeasureValue { public IfcAreaMeasure(double value) : base(value) { } }
	public class IfcAmountOfSubstanceMeasure : IfcMeasureValue { public IfcAmountOfSubstanceMeasure(double value) : base(value) { } }
	public class IfcComplexNumber : IfcMeasureValue
	{
		internal double mImaginary = 0;
		public IfcComplexNumber(double real) : base(real) { }
		public IfcComplexNumber(double real, double imaginary) : base(real) { mImaginary = imaginary; }
		public override string ToString() { return this.GetType().Name + "((" + ParserSTEP.DoubleToString(mValue) + "," + ParserSTEP.DoubleToString(mImaginary) + "))"; }
	}
	public class IfcContextDependentMeasure : IfcMeasureValue { public IfcContextDependentMeasure(double value) : base(value) { } }
	public class IfcCountMeasure : IfcMeasureValue { public IfcCountMeasure(double value) : base(value) { } }
	public class IfcDescriptiveMeasure : IfcMeasureValue, IfcSizeSelect 
	{
		internal string mStringValue;
		public override object Value { get { return mStringValue; } }
		public IfcDescriptiveMeasure(string value) : base(0) { mStringValue = value; }
		public override string ToString() { return "IFCDESCRIPTIVEMEASURE(" + mValue + ")"; }
	}
	public class IfcElectricCurrentMeasure : IfcMeasureValue { public IfcElectricCurrentMeasure(double value) : base(value) { } }
	public class IfcLengthMeasure : IfcMeasureValue, IfcSizeSelect, IfcBendingParameterSelect { public IfcLengthMeasure(double value) : base(value) { } }
	public class IfcLuminousIntensityMeasure : IfcMeasureValue { public IfcLuminousIntensityMeasure(double value) : base(value) { } }
	public class IfcMassMeasure : IfcMeasureValue { public IfcMassMeasure(double value) : base(value) { } }
	public partial class IfcNormalisedRatioMeasure : IfcMeasureValue, IfcColourOrFactor { public IfcNormalisedRatioMeasure(double value) : base(Math.Min(1, Math.Max(0, value))) { }  }
	public class IfcNumericMeasure : IfcMeasureValue { public IfcNumericMeasure(double value) : base(value) { } }
	public class IfcParameterValue : IfcMeasureValue { public IfcParameterValue(double value) : base(value) { } }
	public class IfcPlaneAngleMeasure : IfcMeasureValue, IfcBendingParameterSelect { public IfcPlaneAngleMeasure(double value) : base(value) { } }
	public class IfcPositivePlaneAngleMeasure : IfcMeasureValue { public IfcPositivePlaneAngleMeasure(double value) : base(value) { } }
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
	public class IfcRatioMeasure : IfcMeasureValue, IfcTimeOrRatioSelect//, IfcAppliedValueSelect
	{
		public IfcRatioMeasure(double value) : base(value) { }
		public string String { get { return ToString(); } }
	}
	public class IfcSolidAngleMeasure : IfcMeasureValue { public IfcSolidAngleMeasure(double value) : base(value) { } }
	public class IfcThermodynamicTemperatureMeasure : IfcMeasureValue { public IfcThermodynamicTemperatureMeasure(double value) : base(value) { } }
	public class IfcTimeMeasure : IfcMeasureValue { public IfcTimeMeasure(double value) : base(value) { } }
	public class IfcVolumeMeasure : IfcMeasureValue { public IfcVolumeMeasure(double value) : base(value) { } }

	public abstract class IfcSimpleValue : IfcValue { }// = SELECT(IfcInteger,IfcReal,IfcBoolean,IfcIdentifier,IfcText,IfcLabel,IfcLogical,IfcBinary);
	//IfcBinary
	public partial class IfcBoolean : IfcSimpleValue
	{
		internal bool mValue;
		public override object Value { get { return mValue; } }
		public IfcBoolean(bool value) { mValue = value; }
		public override string ToString() { return "IFCBOOLEAN(" + ParserSTEP.BoolToString(mValue) + ")"; }
	}
	public partial class IfcDate : IfcSimpleValue
	{
		internal string mDate = "$";
		public override object Value { get { return convert(mDate); } }
		internal IfcDate(DateTime datetime) { mDate = convert(datetime); }
		public override string ToString() { return "IFCDATE(" + mDate + ")"; }
		internal static string convert(DateTime date) { return "'" + date.Year + (date.Month < 10 ? "-0" : "-") + date.Month + (date.Day < 10 ? "-0" : "-") + date.Day + "'"; }
		internal static DateTime convert(string date) { return new DateTime(int.Parse(date.Substring(0, 4)), int.Parse(date.Substring(5, 2)), int.Parse(date.Substring(8, 2))); }
	}
	public partial class IfcDateTime : IfcSimpleValue
	{
		internal string mDateTime = "$";
		internal IfcDateTime(DateTime datetime) { mDateTime = formatSTEP(datetime); }
		public override string ToString() { return "IFCDATETIME(" + mDateTime + ")"; }
		internal static string formatSTEP(DateTime dateTime) { return (dateTime == DateTime.MinValue ? "$" : "'" + dateTime.Year + (dateTime.Month < 10 ? "-0" : "-") + dateTime.Month + (dateTime.Day < 10 ? "-0" : "-") + dateTime.Day + (dateTime.Hour < 10 ? "T0" : "T") + dateTime.Hour + (dateTime.Minute < 10 ? ":0" : ":") + dateTime.Minute + (dateTime.Second < 10 ? ":0" : ":") + dateTime.Second + "'"); }
		internal static DateTime parseSTEP(string str)
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
		public override object Value { get { return parseSTEP( mDateTime); } }
		internal static IfcDateTimeSelect convertDateTimeSelect(DatabaseIfc m, DateTime date)
		{
			IfcCalendarDate cd = new IfcCalendarDate(m, date.Day, date.Month, date.Year);
			if (date.Hour + date.Minute + date.Second < m.Tolerance)
				return cd;
			return new IfcDateAndTime(cd, new IfcLocalTime(m, date.Hour, date.Minute, date.Second));
		}
	}
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

		public IfcDuration() { }
		public static string Convert(IfcDuration d) { return (d == null ? "$" : d.ToString()); }
		public static IfcDuration Convert(string s) { return null; }

		public override string ToString() { return "'P" + mYears + "Y" + mMonths + "M" + mDays + "DT" + mHours + "H" + mMinutes + "M" + mSeconds.ToString(ParserSTEP.NumberFormat) + "S'"; }
		internal double ToSeconds() { return ((((((mYears * 365) + (mMonths * 30) + mDays) * 24) + mHours) * 60) + mMinutes) * 60 + mSeconds; }
		public override object Value { get { return ToSeconds(); } }
		public string String { get { return getKW + "(" + ToString() + ")"; } }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCDURATION";
	}
	public partial class IfcIdentifier : IfcSimpleValue
	{
		internal string mValue;
		public override object Value { get { return ParserIfc.Decode(mValue); } }
		public IfcIdentifier(string value) { mValue = ParserIfc.Encode( value); }
		public override string ToString() { return "IFCIDENTIFIER('" + mValue + "')"; }
	}
	public partial class IfcInteger : IfcSimpleValue
	{
		internal int mValue;
		public override object Value { get { return mValue; } }
		public IfcInteger(int value) { mValue = value; }
		public override string ToString() { return "IFCINTEGER(" + mValue.ToString() + ")"; }
	}
	public partial class IfcLabel : IfcSimpleValue
	{
		internal string mValue;
		public override object Value { get { return ParserIfc.Decode(mValue); } }
		public IfcLabel(string value) { mValue = string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value); }
		public override string ToString() { return "IFCLABEL('" + mValue + "')"; }
	}
	public partial class IfcLogical : IfcSimpleValue
	{
		internal IfcLogicalEnum mValue;
		public override object Value { get { return mValue.ToString(); } }
		public IfcLogical(IfcLogicalEnum value) { mValue = value; }
		public IfcLogical(bool value) { mValue = value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE; }
		public override string ToString() { return "IFCLOGICAL(" + ParserIfc.LogicalToString(mValue) + ")"; }
	}
	//IfcPositiveInteger
	public partial class IfcReal : IfcSimpleValue
	{
		internal double mValue;
		public override object Value { get { return mValue; } }
		public IfcReal(double value) { mValue = value; }
		public override string ToString() { return "IFCREAL(" + ParserSTEP.DoubleToString(mValue) + ")"; }
	}
	public partial class IfcText : IfcSimpleValue
	{
		internal string mValue;
		public override object Value { get { return ParserIfc.Decode(mValue); } }
		public IfcText(string value) { mValue = string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value); }
		public override string ToString() { return "IFCTEXT('" + mValue + "')"; }
	}
	public partial class IfcTime : IfcSimpleValue
	{
		internal string mTime = "$";
		public override string ToString() { return mTime; }
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
		public override object Value { get { return mTime; } }
		internal static string convert(DateTime date) { return (date.Hour < 10 ? "T0" : "T") + date.Hour + (date.Minute < 10 ? "-0" : "-") + date.Minute + (date.Second < 10 ? "-0" : "-") + date.Second; }
	}
	//IfcTimeStamp
	public partial class IfcURIReference : IfcSimpleValue
	{
		internal string mValue;
		public override object Value { get { return ParserIfc.Decode(mValue); } }
		public IfcURIReference(string value) { mValue = string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value); }
		public override string ToString() { return "IFCURIREFERENCE('" + mValue + "')"; }
	}

	public partial class IfcSpecularExponent : IfcValue, IfcSpecularHighlightSelect
	{
		internal double mValue;
		public override object Value { get { return mValue; } }
		public IfcSpecularExponent(double value) { mValue = value; }
		public override string ToString() { return "IFCSPECULAREXPONENT(" + ParserSTEP.DoubleToString(mValue) + ")"; }
	}
	public partial class IfcSpecularRoughness : IfcValue, IfcSpecularHighlightSelect
	{
		internal double mValue;
		public override object Value { get { return mValue; } }
		public IfcSpecularRoughness(double value) { mValue = Math.Min(1, Math.Max(0, value)); }
		public override string ToString() { return "IFCSPECULARROUGHNESS(" + ParserSTEP.DoubleToString(mValue) + ")"; }
	}
	public interface IfcSizeSelect { } //TYPE IfcSizeSelect = SELECT (IfcRatioMeasure ,IfcLengthMeasure ,IfcDescriptiveMeasure ,IfcPositiveLengthMeasure ,IfcNormalisedRatioMeasure ,IfcPositiveRatioMeasure);  
}
