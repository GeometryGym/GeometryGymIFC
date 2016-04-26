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
	public interface IfcValue { string getKW { get; } object Value { get; } }  //SELECT(IfcMeasureValue,IfcSimpleValue,IfcDerivedMeasureValue); stpentity parse method
	public class IfcValve : IfcFlowController //IFC4
	{
		internal IfcValveTypeEnum mPredefinedType = IfcValveTypeEnum.NOTDEFINED;// OPTIONAL : IfcValveTypeEnum;
		public IfcValveTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcValve() : base() { }
		internal IfcValve(IfcValve b) : base(b) { mPredefinedType = b.mPredefinedType; }
		internal IfcValve(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcValve s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcValveTypeEnum)Enum.Parse(typeof(IfcValveTypeEnum), str);
		}
		internal new static IfcValve Parse(string strDef) { IfcValve s = new IfcValve(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcValveTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcValveType : IfcFlowControllerType
	{
		internal IfcValveTypeEnum mPredefinedType = IfcValveTypeEnum.NOTDEFINED;// : IfcValveTypeEnum; 
		public IfcValveTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcValveType() : base() { }
		internal IfcValveType(IfcValveType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcValveType(DatabaseIfc m, string name, IfcValveTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcValveType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcValveTypeEnum)Enum.Parse(typeof(IfcValveTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcValveType Parse(string strDef) { IfcValveType t = new IfcValveType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcVector : IfcGeometricRepresentationItem
	{
		internal int mOrientation; // : IfcDirection;
		internal double mMagnitude;// : IfcLengthMeasure; 

		internal IfcDirection Orientation { get { return mDatabase.mIfcObjects[mOrientation] as IfcDirection; } set { mOrientation = value.mIndex; } }

		internal IfcVector() : base() { }
		internal IfcVector(IfcVector v) : base(v) { mOrientation = v.mOrientation; mMagnitude = v.mMagnitude; }
	
		internal static void parseFields(IfcVector v, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(v, arrFields, ref ipos); v.mOrientation = ParserSTEP.ParseLink(arrFields[ipos++]); v.mMagnitude = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcVector Parse(string strDef) { IfcVector v = new IfcVector(); int ipos = 0; parseFields(v, ParserSTEP.SplitLineFields(strDef), ref ipos); return v; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mOrientation) + "," + ParserSTEP.DoubleToString(mMagnitude); }
	}
	public partial class IfcVertex : IfcTopologicalRepresentationItem //SUPERTYPE OF(IfcVertexPoint)
	{
		internal IfcVertex() : base() { }
		internal IfcVertex(IfcVertex v) : base(v) { }
		internal IfcVertex(DatabaseIfc m) : base(m) { }
		internal static IfcVertex Parse(string strDef) { IfcVertex v = new IfcVertex(); int ipos = 0; parseFields(v, ParserSTEP.SplitLineFields(strDef), ref ipos); return v; }
		internal static void parseFields(IfcVertex v, List<string> arrFields, ref int ipos) { IfcTopologicalRepresentationItem.parseFields(v, arrFields, ref ipos); }
	}
	public class IfcVertexBasedTextureMap : BaseClassIfc // DEPRECEATED IFC4
	{
		internal List<int> mTextureVertices = new List<int>();// LIST [3:?] OF IfcTextureVertex;
		internal List<int> mTexturePoints = new List<int>();// LIST [3:?] OF IfcCartesianPoint; 
		internal IfcVertexBasedTextureMap() : base() { }
		internal IfcVertexBasedTextureMap(IfcVertexBasedTextureMap m) : base() { mTextureVertices = new List<int>(m.mTextureVertices.ToArray()); mTexturePoints = new List<int>(m.mTexturePoints.ToArray()); }
		internal static IfcVertexBasedTextureMap Parse(string strDef) { IfcVertexBasedTextureMap m = new IfcVertexBasedTextureMap(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcVertexBasedTextureMap m, List<string> arrFields, ref int ipos) { m.mTextureVertices = ParserSTEP.SplitListLinks(arrFields[ipos++]); m.mTexturePoints = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mTextureVertices[0]);
			for (int icounter = 1; icounter < mTextureVertices.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mTextureVertices[icounter]);
			str += "),(" + ParserSTEP.LinkToString(mTexturePoints[0]);
			for (int icounter = 1; icounter < mTexturePoints.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mTexturePoints[icounter]);
			return str;
		}
	}
	public class IfcVertexloop : IfcLoop
	{
		internal int mLoopVertex;// : IfcVertex; 
		internal IfcVertexloop() : base() { }
		internal IfcVertexloop(IfcVertexloop o) : base(o) { mLoopVertex = o.mLoopVertex; }
		internal new static IfcVertexloop Parse(string strDef) { IfcVertexloop l = new IfcVertexloop(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcVertexloop l, List<string> arrFields, ref int ipos) { IfcLoop.parseFields(l, arrFields, ref ipos); l.mLoopVertex = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mLoopVertex); }
	}
	public partial class IfcVertexPoint : IfcVertex, IfcPointOrVertexPoint
	{
		internal int mVertexGeometry;// : IfcPoint; 
		internal IfcPoint VertexGeometry { get { return mDatabase.mIfcObjects[mVertexGeometry] as IfcPoint; } }
		internal IfcVertexPoint() : base() { }
		internal IfcVertexPoint(IfcVertexPoint v) : base(v) { mVertexGeometry = v.mVertexGeometry; }
		public IfcVertexPoint(IfcPoint cp) : base(cp.mDatabase) { mVertexGeometry = cp.mIndex; }
		internal new static IfcVertexPoint Parse(string strDef) { IfcVertexPoint v = new IfcVertexPoint(); int ipos = 0; parseFields(v, ParserSTEP.SplitLineFields(strDef), ref ipos); return v; }
		internal static void parseFields(IfcVertexPoint v, List<string> arrFields, ref int ipos) { IfcVertex.parseFields(v, arrFields, ref ipos); v.mVertexGeometry = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mVertexGeometry); }
	}
	public partial class IfcVibrationIsolator : IfcElementComponent
	{
		internal IfcVibrationIsolatorTypeEnum mPredefinedType = IfcVibrationIsolatorTypeEnum.NOTDEFINED;// : OPTIONAL IfcVibrationIsolatorTypeEnum;
		public IfcVibrationIsolatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcVibrationIsolator() : base() { }
		internal IfcVibrationIsolator(IfcVibrationIsolator a) : base(a) { mPredefinedType = a.mPredefinedType; }
		public IfcVibrationIsolator(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }

		internal static IfcVibrationIsolator Parse(string strDef) { int ipos = 0; IfcVibrationIsolator a = new IfcVibrationIsolator(); parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcVibrationIsolator a, List<string> arrFields, ref int ipos)
		{
			IfcElementComponent.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcVibrationIsolatorTypeEnum)Enum.Parse(typeof(IfcVibrationIsolatorTypeEnum), s.Replace(".", ""));
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcVibrationIsolatorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
	}
	public class IfcVibrationIsolatorType : IfcElementComponentType
	{
		internal IfcVibrationIsolatorTypeEnum mPredefinedType = IfcVibrationIsolatorTypeEnum.NOTDEFINED;// : IfcVibrationIsolatorTypeEnum
		public IfcVibrationIsolatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcVibrationIsolatorType() : base() { }
		internal IfcVibrationIsolatorType(IfcVibrationIsolatorType be) : base(be) { mPredefinedType = be.mPredefinedType; }
		internal static void parseFields(IfcVibrationIsolatorType t, List<string> arrFields, ref int ipos) { IfcDiscreteAccessoryType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcVibrationIsolatorTypeEnum)Enum.Parse(typeof(IfcVibrationIsolatorTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcVibrationIsolatorType Parse(string strDef) { IfcVibrationIsolatorType t = new IfcVibrationIsolatorType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcVirtualElement : IfcElement
	{
		internal IfcVirtualElement() : base() { }
		internal IfcVirtualElement(IfcVirtualElement el) : base(el) { }
		internal static IfcProduct Parse(string strDef) { IfcVirtualElement e = new IfcVirtualElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		internal static void parseFields(IfcVirtualElement e, List<string> arrFields, ref int ipos) { IfcElement.parseFields(e, arrFields, ref ipos); }
	}
	public partial class IfcVirtualGridIntersection : BaseClassIfc
	{
		internal List<int> mIntersectingAxes = new List<int>(2);// : LIST [2:2] OF UNIQUE IfcGridAxis;
		internal Tuple<double,double,double> mOffsetDistances = null;// : LIST [2:3] OF IfcLengthMeasure; 
		internal IfcVirtualGridIntersection() : base() { }
		internal IfcVirtualGridIntersection(IfcVirtualGridIntersection p) : base() { mIntersectingAxes = new List<int>(p.mIntersectingAxes.ToArray()); mOffsetDistances = p.mOffsetDistances; }
		internal static IfcVirtualGridIntersection Parse(string strDef) { IfcVirtualGridIntersection i = new IfcVirtualGridIntersection(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcVirtualGridIntersection i, List<string> arrFields, ref int ipos)
		{
			i.mIntersectingAxes = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			List<string> lst = ParserSTEP.SplitLineFields(arrFields[ipos++]);
			i.mOffsetDistances = new Tuple<double,double,double>(ParserSTEP.ParseDouble(lst[0]), ParserSTEP.ParseDouble(lst[1]),(lst.Count > 2 ? ParserSTEP.ParseDouble(lst[2]) : double.NaN));
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mIntersectingAxes[0]) + "," +
				ParserSTEP.LinkToString(mIntersectingAxes[1]) + "),(";
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
		internal IfcVoidingFeature(IfcVoidingFeature od) : base(od) { mPredefinedType = od.mPredefinedType; }
		public IfcVoidingFeature(IfcElement host,IfcProductRepresentation rep,IfcVoidingFeatureTypeEnum type) : base(host,rep) { mPredefinedType = type; }
		
		internal static IfcVoidingFeature Parse(string strDef, Schema schema) { IfcVoidingFeature e = new IfcVoidingFeature(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return e; }
		internal static void parseFields(IfcVoidingFeature e, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcFeatureElementSubtraction.parseFields(e, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
				e.mPredefinedType = (IfcVoidingFeatureTypeEnum)Enum.Parse(typeof(IfcVoidingFeatureTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : ",." + mPredefinedType + "."); }
	}

	public interface IfcDerivedMeasureValue : IfcValue { double Measure { get; } } //(IfcVolumetricFlowRateMeasure,IfcTimeStamp ,IfcThermalTransmittanceMeasure ,IfcThermalResistanceMeasure
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

	public class IfcDynamicViscosityMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcDynamicViscosityMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCDYNAMICVISCOSITYMEASURE";
	}
	public class IfcForceMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcForceMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCFORCEMEASURE";
	}
	public class IfcLinearStiffnessMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcLinearStiffnessMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCLINEARSTIFFNESSMEASURE";
	}
	public class IfcLinearVelocityMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcLinearVelocityMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCLINEARVELOCITYMEASURE";
	}
	public class IfcMassDensityMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcMassDensityMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCMASSDENSITYMEASURE";
	}
	public struct IfcMassPerLengthMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcMassPerLengthMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCMASSPERLENGTHMEASURE";
	}
	public class IfcModulusOfElasticityMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcModulusOfElasticityMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCMODULUSOFELASTICITYMEASURE";
	}
	public struct IfcMolecularWeightMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcMolecularWeightMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCMOLECULARWEIGHTMEASURE";
	}
	public struct IfcMomentOfInertiaMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcMomentOfInertiaMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCMOMOENTOFINERTIAMEASURE";
	}
	public struct IfcMonetaryMeasure : IfcDerivedMeasureValue//, IfcAppliedValueSelect
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcMonetaryMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCMONETARYMEASURE";
	}
	public class IfcPowerMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		public IfcPowerMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCPOWERMEASURE";
	}
	public class IfcPressureMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		public IfcPressureMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCPRESSUREMEASURE";
	}
	public class IfcRotationalStiffnessMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcRotationalStiffnessMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCROTATIONALSTIFFNESSMEASURE";
	}
	public class IfcSectionModulusMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcSectionModulusMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCSECTIONMODULUSMEASURE";
	}
	public class IfcThermalExpansionCoefficientMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcThermalExpansionCoefficientMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCTHERMALEXPANSIONCOEFFICIENTMEASURE";
	}
	public class IfcThermalTransmittanceMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcThermalTransmittanceMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCTHERMALTRANSMITTANCEMEASURE";
	}
	public class IfcVolumetricFlowRateMeasure : IfcDerivedMeasureValue// IfcMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		public IfcVolumetricFlowRateMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCVOLUMETRICFLOWRATEMEASURE";
	}
	public class IfcWarpingConstantMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcWarpingConstantMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCWARPINGCONSTANTMEASURE";
	}
	public class IfcWarpingMomentMeasure : IfcDerivedMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcWarpingMomentMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCWARPINGMOMENTMEASURE";
	}

	public interface IfcMeasureValue : IfcValue { double Measure { get; } } //TYPE IfcMeasureValue = SELECT (IfcVolumeMeasure,IfcTimeMeasure ,IfcThermodynamicTemperatureMeasure ,IfcSolidAngleMeasure ,IfcPositiveRatioMeasure
	//,IfcRatioMeasure ,IfcPositivePlaneAngleMeasure ,IfcPlaneAngleMeasure ,IfcParameterValue ,IfcNumericMeasure ,IfcMassMeasure ,IfcPositiveLengthMeasure,IfcLengthMeasure ,IfcElectricCurrentMeasure ,
	//IfcDescriptiveMeasure ,IfcCountMeasure ,IfcContextDependentMeasure ,IfcAreaMeasure ,IfcAmountOfSubstanceMeasure ,IfcLuminousIntensityMeasure ,IfcNormalisedRatioMeasure ,IfcComplexNumber);
	public class IfcAreaMeasure : IfcMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcAreaMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCAREAMEASURE";
	}
	public class IfcCountMeasure : IfcMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcCountMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCCOUNTMEASURE";
	}
	public class IfcDescriptiveMeasure : IfcMeasureValue, IfcSizeSelect
	{
		internal string mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return 0; } }
		internal IfcDescriptiveMeasure(string value) { mValue = value; }
		public override string ToString() { return getKW + "(" + mValue + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCDESCRIPTIVEMEASURE";
	}
	public class IfcLengthMeasure : IfcMeasureValue, IfcSizeSelect, IfcBendingParameterSelect
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcLengthMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCLENGTHMEASURE";
	}
	public class IfcMassMeasure : IfcMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcMassMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCMASSMEASURE";
	}
	public class IfcNormalisedRatioMeasure : IfcMeasureValue, IfcColourOrFactor
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcNormalisedRatioMeasure(double value) { mValue = Math.Min(1, Math.Max(0, value)); }
		internal IfcNormalisedRatioMeasure(System.Drawing.Color col) : this(System.Drawing.Color.FromArgb(0, col.R, col.G, col.B).ToArgb() / 16581375.0) { }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCNORMALISEDRATIOMEASURE";
	}
	public class IfcPlaneAngleMeasure : IfcMeasureValue, IfcBendingParameterSelect
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcPlaneAngleMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCPLANEANGLEMEASURE";
	}
	public class IfcPositiveLengthMeasure : IfcMeasureValue, IfcSizeSelect
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		public IfcPositiveLengthMeasure(double value) { mValue = value; }
		internal IfcPositiveLengthMeasure(string str)
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
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCPOSITIVELENGTHMEASURE";
	}
	public class IfcPositiveRatioMeasure : IfcMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcPositiveRatioMeasure(double value) { mValue = value; }
		internal IfcPositiveRatioMeasure(string str)
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
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCPOSITIVERATIOMEASURE";
	}
	public class IfcRatioMeasure : IfcMeasureValue, IfcTimeOrRatioSelect//, IfcAppliedValueSelect
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcRatioMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string String { get { return ToString(); } }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCRATIOMEASURE";
	}
	public class IfcThermodynamicTemperatureMeasure : IfcMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcThermodynamicTemperatureMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCTHERMODYNAMICTEMPERATRUEMEASURE";
	}
	public class IfcTimeMeasure : IfcMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcTimeMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCTIMEMEASURE";
	}
	public class IfcVolumeMeasure : IfcMeasureValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		public double Measure { get { return mValue; } }
		internal IfcVolumeMeasure(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return mKW; } }
		internal static string mKW = "IFCVOLUMEMEASURE";
	}

	public interface IfcSimpleValue : IfcValue { }// = SELECT(IfcInteger,IfcReal,IfcBoolean,IfcIdentifier,IfcText,IfcLabel,IfcLogical);
	public class IfcBoolean : IfcSimpleValue
	{
		internal bool mValue;
		public object Value { get { return mValue; } }
		public IfcBoolean(bool value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.BoolToString(mValue) + ")"; }
		public string getKW { get { return "IFCBOOLEAN"; } }
	}
	public class IfcIdentifier : IfcSimpleValue
	{
		internal string mValue;
		public object Value { get { return mValue; } }
		internal IfcIdentifier(string value) { mValue = value.Replace("'", ""); }
		public override string ToString() { return getKW + "('" + mValue + "')"; }
		public string getKW { get { return "IFCIDENTIFIER"; } }
	}
	public class IfcInteger : IfcSimpleValue
	{
		internal int mValue;
		public object Value { get { return mValue; } }
		internal IfcInteger(int value) { mValue = value; }
		public override string ToString() { return getKW + "(" + mValue.ToString() + ")"; }
		public string getKW { get { return "IFCINTEGER"; } }
	}
	public class IfcLabel : IfcSimpleValue
	{
		internal string mValue;
		public object Value { get { return ParserIfc.Decode(mValue); } }
		public IfcLabel(string value) { mValue = string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value.Replace("'", "")); }
		public override string ToString() { return getKW + "('" + mValue + "')"; }
		public string getKW { get { return "IFCLABEL"; } }
	}
	public class IfcLogical : IfcSimpleValue
	{
		internal IfcLogicalEnum mValue;
		public object Value { get { return mValue.ToString(); } }
		internal IfcLogical(IfcLogicalEnum value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserIfc.LogicalToString(mValue) + ")"; }
		public string getKW { get { return "IFCLOGICAL"; } }
	}
	public class IfcReal : IfcSimpleValue
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		internal IfcReal(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return "IFCREAL"; } }
	}
	public class IfcSpecularExponent : IfcSimpleValue, IfcSpecularHighlightSelect
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		internal IfcSpecularExponent(double value) { mValue = value; }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return "IFCSPECULAREXPONENT"; } }
	}
	public class IfcSpecularRoughness : IfcSimpleValue, IfcSpecularHighlightSelect
	{
		internal double mValue;
		public object Value { get { return mValue; } }
		internal IfcSpecularRoughness(double value) { mValue = Math.Min(1, Math.Max(0, value)); }
		public override string ToString() { return getKW + "(" + ParserSTEP.DoubleToString(mValue) + ")"; }
		public string getKW { get { return "IFCSPECULARROUGHNESS"; } }
	}
	public class IfcText : IfcSimpleValue
	{
		internal string mValue;
		public object Value { get { return ParserIfc.Decode(mValue); } }
		internal IfcText(string value) { mValue = string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value.Replace("'", "")); }
		public override string ToString() { return getKW + "('" + mValue + "')"; }
		public string getKW { get { return "IFCTEXT"; } }
	}

	public interface IfcSizeSelect { } //TYPE IfcSizeSelect = SELECT (IfcRatioMeasure ,IfcLengthMeasure ,IfcDescriptiveMeasure ,IfcPositiveLengthMeasure ,IfcNormalisedRatioMeasure ,IfcPositiveRatioMeasure);  
}
