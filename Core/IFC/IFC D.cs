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
	public partial class IfcDamper : IfcFlowController //IFC4
	{
		internal IfcDamperTypeEnum mPredefinedType = IfcDamperTypeEnum.NOTDEFINED;// OPTIONAL : IfcDamperTypeEnum;
		public IfcDamperTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcDamper() : base() { }
		internal IfcDamper(DatabaseIfc db, IfcDamper d, IfcOwnerHistory ownerHistory, bool downStream) : base(db, d, ownerHistory, downStream) { mPredefinedType = d.mPredefinedType; }
		public IfcDamper(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcDamperType : IfcFlowControllerType
	{
		internal IfcDamperTypeEnum mPredefinedType = IfcDamperTypeEnum.NOTDEFINED;// : IfcDamperTypeEnum;
		public IfcDamperTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcDamperType() : base() { }
		internal IfcDamperType(DatabaseIfc db, IfcDamperType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcDamperType(DatabaseIfc m, string name, IfcDamperTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDateAndTime : BaseClassIfc, IfcDateTimeSelect // DEPRECEATED IFC4
	{
		internal int mDateComponent;// : IfcCalendarDate;
		internal int mTimeComponent;// : IfcLocalTime;
		public IfcCalendarDate DateComponent { get { return mDatabase[mDateComponent] as IfcCalendarDate; } set { mDateComponent = value.Index; } }
		public IfcLocalTime TimeComponent { get { return mDatabase[mTimeComponent] as IfcLocalTime; } set { mTimeComponent = value.Index; } }

		internal IfcDateAndTime(IfcDateAndTime v) : base() { mDateComponent = v.mDateComponent; mTimeComponent = v.mTimeComponent; }
		internal IfcDateAndTime() : base() { }
		public IfcDateAndTime(IfcCalendarDate d, IfcLocalTime t) : base(d.mDatabase) { mDateComponent = d.mIndex; mTimeComponent = t.mIndex; }
		public DateTime DateTime
		{
			get
			{
				IfcCalendarDate cd = mDatabase[mDateComponent] as IfcCalendarDate;
				IfcLocalTime lt = mDatabase[mTimeComponent] as IfcLocalTime;
				return new DateTime(cd.mYearComponent, cd.mMonthComponent, cd.mDayComponent, lt.mHourComponent, lt.mMinuteComponent, (int)lt.mSecondComponent);
			}
		}
	}
	
	public interface IfcDateTimeSelect : IBaseClassIfc { DateTime DateTime { get; } } // IFC2x3 IfcCalenderDate, IfcDateAndTime, IfcLocalTime 
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcDefinedSymbol  // DEPRECEATED IFC4
	public interface IfcDefinitionSelect : IBaseClassIfc // IFC4 SELECT ( IfcObjectDefinition,  IfcPropertyDefinition);
	{
		IfcRelDeclares HasContext { get; set; }
		SET<IfcRelAssociates> HasAssociations { get; }
		List<T> Extract<T>() where T : IBaseClassIfc;
	}
	[Serializable]
	public partial class IfcDerivedProfileDef : IfcProfileDef
	{
		private int mContainerProfile;// : IfcProfileDef;
		private int mOperator;// : IfcCartesianTransformationOperator2D;
		internal string mLabel = "$";// : OPTIONAL IfcLabel;

		public IfcProfileDef ContainerProfile { get { return mDatabase[mContainerProfile] as IfcProfileDef; } set { mContainerProfile = value.mIndex; } }
		public IfcCartesianTransformationOperator2D Operator { get { return mDatabase[mOperator] as IfcCartesianTransformationOperator2D; } set { mOperator = value.mIndex; } }
		public string Label { get { return (mLabel == "$" ? "" : ParserIfc.Decode(mLabel)); } set { mLabel = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcDerivedProfileDef() : base() { }
		internal IfcDerivedProfileDef(DatabaseIfc db, IfcDerivedProfileDef p) : base(db, p)
		{
			ContainerProfile = db.Factory.Duplicate(p.ContainerProfile) as IfcProfileDef;
			Operator = db.Factory.Duplicate(p.Operator) as IfcCartesianTransformationOperator2D;
			mLabel = p.mLabel;
		}
		public IfcDerivedProfileDef(IfcProfileDef container, IfcCartesianTransformationOperator2D op, string name) : base(container.mDatabase, name) { ContainerProfile = container; Operator = op; }
	}
	[Serializable]
	public partial class IfcDerivedUnit : BaseClassIfc, IfcUnit
	{
		private List<int> mElements = new List<int>();// : SET [1:?] OF IfcDerivedUnitElement;
		private IfcDerivedUnitEnum mUnitType;// : IfcDerivedUnitEnum;
		private string mUserDefinedType = "$";// : OPTIONAL IfcLabel;

		public List<IfcDerivedUnitElement> Elements { get { return mElements.ConvertAll(x => mDatabase[x] as IfcDerivedUnitElement); } set { mElements = value.ConvertAll(x => x.mIndex); } }
		public IfcDerivedUnitEnum UnitType { get { return mUnitType; } set { mUnitType = value; } }
		public string UserDefinedType { get { return (mUserDefinedType == "$" ? "" : ParserIfc.Decode(mUserDefinedType)); } set { mUserDefinedType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcDerivedUnit() : base() { }
		internal IfcDerivedUnit(DatabaseIfc db, IfcDerivedUnit u) : base(db) { Elements = u.Elements.ConvertAll(x=>db.Factory.Duplicate(x) as IfcDerivedUnitElement); mUnitType = u.mUnitType; mUserDefinedType = u.mUserDefinedType; }
		public IfcDerivedUnit(IfcDerivedUnitElement element, IfcDerivedUnitEnum type) : base(element.mDatabase) { mElements.Add(element.mIndex); mUnitType = type;  }
		public IfcDerivedUnit(IEnumerable<IfcDerivedUnitElement> elements, IfcDerivedUnitEnum type) : base(elements.First().mDatabase) { foreach(IfcDerivedUnitElement e in elements) mElements.Add(e.mIndex); mUnitType = type; }
		public IfcDerivedUnit(IEnumerable<IfcDerivedUnitElement> elements, string userDefinedType) : base(elements.First().mDatabase) { foreach(IfcDerivedUnitElement e in elements) mElements.Add(e.mIndex); UserDefinedType = userDefinedType; mUnitType = IfcDerivedUnitEnum.USERDEFINED; }
		public IfcDerivedUnit(IfcDerivedUnitElement due1, IfcDerivedUnitElement due2, IfcDerivedUnitEnum type) : base(due1.mDatabase) { mElements.Add(due1.mIndex); mElements.Add(due2.mIndex); mUnitType = type;  }
		public IfcDerivedUnit(IfcDerivedUnitElement due1, IfcDerivedUnitElement due2, IfcDerivedUnitElement due3, IfcDerivedUnitEnum type) : base(due1.mDatabase) { mElements.Add(due1.mIndex); mElements.Add(due2.mIndex); mElements.Add(due3.mIndex); mUnitType = type;  }
		
		public double SIFactor
		{
			get
			{
				List<IfcDerivedUnitElement> elements = Elements;
				double result = 1;
				foreach (IfcDerivedUnitElement due in elements)
					result *= Math.Pow(due.Unit.SIFactor, due.Exponent);
				return result;
			}
		}
	}
	[Serializable]
	public partial class IfcDerivedUnitElement : BaseClassIfc
	{
		private int mUnit;// : IfcNamedUnit;
		private int mExponent;// : INTEGER;

		public IfcNamedUnit Unit { get { return mDatabase[mUnit] as IfcNamedUnit; } set { mUnit = value.mIndex; } }
		public int Exponent { get { return mExponent; } set { mExponent = value; } } 

		internal IfcDerivedUnitElement() : base() { }
		internal IfcDerivedUnitElement(DatabaseIfc db, IfcDerivedUnitElement e) : base(db) { Unit = db.Factory.Duplicate(e.Unit) as IfcNamedUnit; mExponent = e.mExponent; }
		public IfcDerivedUnitElement(IfcNamedUnit u, int exponent) : base(u.mDatabase) { mUnit = u.mIndex; mExponent = exponent; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDiameterDimension : IfcDimensionCurveDirectedCallout // DEPRECEATED IFC4
	{
		internal IfcDiameterDimension() : base() { }
		//internal IfcDiameterDimension(IfcDiameterDimension el) : base(el) { }
	}
	[Serializable]
	public partial class IfcDimensionalExponents : BaseClassIfc
	{
		internal int mLengthExponent, mMassExponent,mTimeExponent, mElectricCurrentExponent, mThermodynamicTemperatureExponent, mAmountOfSubstanceExponent, mLuminousIntensityExponent;// : INTEGER;
		public int LengthExponent { get { return mLengthExponent; } set { mLengthExponent = value; } }
		public int MassExponent { get { return mMassExponent; } set { mMassExponent = value; } }
		public int TimeExponent { get { return mTimeExponent; } set { mTimeExponent = value; } }
		public int ElectricCurrentExponent { get { return mElectricCurrentExponent; } set { mElectricCurrentExponent = value; } }
		public int ThermodynamicTemperatureExponent { get { return mThermodynamicTemperatureExponent; } set { mThermodynamicTemperatureExponent = value; } }
		public int AmountOfSubstanceExponent { get { return mAmountOfSubstanceExponent; } set { mAmountOfSubstanceExponent = value; } }
		public int LuminousIntensityExponent { get { return mLuminousIntensityExponent; } set { mLuminousIntensityExponent = value; } }
		internal IfcDimensionalExponents() : base() { }
		internal IfcDimensionalExponents(DatabaseIfc db, IfcDimensionalExponents e) : base(db,e)
		{
			mLengthExponent = e.mLengthExponent;
			mMassExponent = e.mMassExponent;
			mTimeExponent = e.mTimeExponent;
			mElectricCurrentExponent = e.mElectricCurrentExponent;
			mThermodynamicTemperatureExponent = e.mThermodynamicTemperatureExponent;
			mAmountOfSubstanceExponent = e.mAmountOfSubstanceExponent;
			mLuminousIntensityExponent = e.mLuminousIntensityExponent;
		}
		public IfcDimensionalExponents(DatabaseIfc db, int len, int mass, int time, int elecCurr, int themrmo, int amountSubs, int luminous) : base(db)
		{
			mLengthExponent = len;
			mMassExponent = mass;
			mTimeExponent = time;
			mElectricCurrentExponent = elecCurr;
			mThermodynamicTemperatureExponent = themrmo;
			mAmountOfSubstanceExponent = amountSubs;
			mLuminousIntensityExponent = luminous;
		}
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDimensionCalloutRelationship : IfcDraughtingCalloutRelationship // DEPRECEATED IFC4
	{
		internal IfcDimensionCalloutRelationship() : base() { }
		//internal IfcDimensionCalloutRelationship(DatabaseIfc db, IfcDimensionCalloutRelationship r) : base(db,r) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDimensionCurve : IfcAnnotationCurveOccurrence // DEPRECEATED IFC4
	{
		internal List<int> mAnnotatedBySymbols = new List<int>();// SET [0:2] OF IfcTerminatorSymbol FOR AnnotatedCurve; 
		internal IfcDimensionCurve() : base() { }
		//internal IfcDimensionCurve(DatabaseIfc db, IfcDimensionCurve p) : base(p) { mAnnotatedBySymbols = new List<int>(p.mAnnotatedBySymbols.ToArray()); }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDimensionCurveDirectedCallout : IfcDraughtingCallout // DEPRECEATED IFC4 SUPERTYPE OF (ONEOF (IfcAngularDimension ,IfcDiameterDimension ,IfcLinearDimension ,IfcRadiusDimension))
	{
		internal IfcDimensionCurveDirectedCallout() : base() { }
	//	internal IfcDimensionCurveDirectedCallout(DatabaseIfc db, IfcDimensionCurveDirectedCallout c) : base(db,c) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDimensionCurveTerminator : IfcTerminatorSymbol // DEPRECEATED IFC4
	{
		internal IfcDimensionExtentUsage mRole;// : IfcDimensionExtentUsage;
		internal IfcDimensionCurveTerminator() : base() { }
	//	internal IfcDimensionCurveTerminator(IfcDimensionCurveTerminator i) : base(i) { mRole = i.mRole; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDimensionPair : IfcDraughtingCalloutRelationship // DEPRECEATED IFC4
	{
		internal IfcDimensionPair() : base() { }
		//internal IfcDimensionPair(IfcDimensionPair i) : base((IfcDraughtingCalloutRelationship)i) { }
	}
	[Serializable]
	public partial class IfcDirection : IfcGeometricRepresentationItem
	{
		[NonSerialized] private double mDirectionRatioX = 0, mDirectionRatioY = 0, mDirectionRatioZ = double.NaN; // DirectionRatios : LIST [2:3] OF IfcReal;

		public double DirectionRatioX { get { return mDirectionRatioX; } set { mDirectionRatioX = value; } }
		public double DirectionRatioY { get { return mDirectionRatioY; } set { mDirectionRatioY = value; } }
		public double DirectionRatioZ { get { return double.IsNaN(mDirectionRatioZ) ? 0 : mDirectionRatioZ; } set { mDirectionRatioZ = value; } }

		public LIST<double> DirectionRatios
		{
			get { return (is2D ? new LIST<double>() { mDirectionRatioX, mDirectionRatioY } : new LIST<double>() { mDirectionRatioX, mDirectionRatioY, mDirectionRatioZ }); }
			set { mDirectionRatioX = value[0]; mDirectionRatioY = value[1]; if(value.Count() > 2) mDirectionRatioZ = value[2]; }
		}

		internal IfcDirection() : base() { }
		internal IfcDirection(DatabaseIfc db, IfcDirection d) : base(db,d) { mDirectionRatioX = d.mDirectionRatioX; mDirectionRatioY = d.mDirectionRatioY; mDirectionRatioZ = d.mDirectionRatioZ; }
		public IfcDirection(DatabaseIfc db, double x, double y) : base(db) { double length = Math.Sqrt(x * x + y * y); DirectionRatioX = x / length; DirectionRatioY = y / length; DirectionRatioZ = double.NaN; }
		public IfcDirection(DatabaseIfc db, double x, double y, double z) : base(db) { double length = Math.Sqrt(x * x + y * y +z * z); DirectionRatioX = x / length; DirectionRatioY = y / length; DirectionRatioZ = z / length; }

		private double RoundRatio(double ratio) { return Math.Round(ratio, 8); }

		internal bool is2D { get { return double.IsNaN(mDirectionRatioZ); } }
		internal bool isXAxis { get { double tol = 1e-6; return ((Math.Abs(mDirectionRatioX - 1) < tol) && (double.IsNaN(mDirectionRatioY) || Math.Abs(mDirectionRatioY) < tol) && (double.IsNaN(mDirectionRatioZ) || Math.Abs(mDirectionRatioZ) < tol)); } }
		internal bool isYAxis { get { double tol = 1e-6; return ((double.IsNaN(mDirectionRatioX) || Math.Abs(mDirectionRatioX) < tol) && Math.Abs(mDirectionRatioY - 1) < tol && (double.IsNaN(mDirectionRatioZ) || Math.Abs(mDirectionRatioZ) < tol)); } }
		internal bool isZAxis { get { double tol = 1e-6; return ((double.IsNaN(mDirectionRatioX) || Math.Abs(mDirectionRatioX) < tol) && (double.IsNaN(mDirectionRatioY) || Math.Abs(mDirectionRatioY) < tol) && (Math.Abs(mDirectionRatioZ - 1) < tol)); } }
		internal bool isXAxisNegative { get { double tol = 1e-6; return ((Math.Abs(mDirectionRatioX + 1) < tol) && (double.IsNaN(mDirectionRatioY) || Math.Abs(mDirectionRatioY) < tol) && (double.IsNaN(mDirectionRatioZ) || Math.Abs(mDirectionRatioZ) < tol)); } }
		internal bool isYAxisNegative { get { double tol = 1e-6; return ((double.IsNaN(mDirectionRatioX) || Math.Abs(mDirectionRatioX) < tol) && Math.Abs(mDirectionRatioY + 1) < tol && (double.IsNaN(mDirectionRatioZ) || Math.Abs(mDirectionRatioZ) < tol)); } }
		internal bool isZAxisNegative { get { double tol = 1e-6; return ((double.IsNaN(mDirectionRatioX) || Math.Abs(mDirectionRatioX) < tol) && (double.IsNaN(mDirectionRatioY) || Math.Abs(mDirectionRatioY) < tol) && (Math.Abs(mDirectionRatioZ + 1) < tol)); } }
	}
	[Serializable]
	public partial class IfcDiscreteAccessory : IfcElementComponent
	{
		internal IfcDiscreteAccessoryTypeEnum mPredefinedType = IfcDiscreteAccessoryTypeEnum.NOTDEFINED;// : OPTIONAL IfcDiscreteAccessoryTypeEnum;
		public IfcDiscreteAccessoryTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcDiscreteAccessory() : base() { }
		internal IfcDiscreteAccessory(DatabaseIfc db, IfcDiscreteAccessory a, IfcOwnerHistory ownerHistory, bool downStream) : base(db, a, ownerHistory, downStream) { mPredefinedType = a.mPredefinedType; }
		public IfcDiscreteAccessory(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		public IfcDiscreteAccessory(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public partial class IfcDiscreteAccessoryType : IfcElementComponentType
	{
		internal IfcDiscreteAccessoryTypeEnum mPredefinedType = IfcDiscreteAccessoryTypeEnum.NOTDEFINED;//:	OPTIONAL IfcDiscreteAccessoryTypeEnum; IFC4	
		public IfcDiscreteAccessoryTypeEnum PredefinedType
		{
			get { return mPredefinedType; }
			set
			{
				mPredefinedType = value;
				if (mDatabase.Release < ReleaseVersion.IFC4 && string.IsNullOrEmpty(ElementType))
					ElementType = value.ToString();

			}
		}

		internal IfcDiscreteAccessoryType() : base() { }
		internal IfcDiscreteAccessoryType(DatabaseIfc db, IfcDiscreteAccessoryType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcDiscreteAccessoryType(DatabaseIfc db, string name, IfcDiscreteAccessoryTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcDistanceExpression : IfcGeometricRepresentationItem
	{
		internal double mDistanceAlong;// : IfcLengthMeasure;
		internal double mOffsetLateral = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal double mOffsetVertical = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal double mOffsetLongitudinal = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal bool mAlongHorizontal = false; // IfcBoolean

		public double DistanceAlong { get { return mDistanceAlong; } set { mDistanceAlong = value; } }
		public double OffsetLateral { get { return mOffsetLateral; } set { mOffsetLateral = value; } }
		public double OffsetVertical { get { return mOffsetVertical; } set { mOffsetVertical = value; } }
		public double OffsetLongitudinal { get { return mOffsetLongitudinal; } set { mOffsetLongitudinal = value; } }
		public bool AlongHorizontal { get { return mAlongHorizontal; } set { mAlongHorizontal = value; } } 

		internal IfcDistanceExpression() : base() { }
		internal IfcDistanceExpression(DatabaseIfc db, IfcDistanceExpression e) : base(db, e)
		{
			DistanceAlong = e.DistanceAlong;
			OffsetLateral = e.OffsetLateral;
			OffsetVertical = e.OffsetVertical;
			OffsetLongitudinal = e.OffsetLongitudinal;
			AlongHorizontal = e.AlongHorizontal;
		}
		public IfcDistanceExpression(DatabaseIfc db, double distanceAlong) : base(db) { DistanceAlong = distanceAlong; }
	}
	[Serializable]
	public partial class IfcDistributionChamberElement : IfcDistributionFlowElement
	{
		internal IfcDistributionChamberElementTypeEnum mPredefinedType = IfcDistributionChamberElementTypeEnum.NOTDEFINED;//	:	OPTIONAL IfcDistributionChamberElementTypeEnum;
		public IfcDistributionChamberElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcDistributionChamberElement() : base() { }
		internal IfcDistributionChamberElement(DatabaseIfc db, IfcDistributionChamberElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { mPredefinedType = e.mPredefinedType; }
		public IfcDistributionChamberElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcDistributionChamberElementType : IfcDistributionFlowElementType
	{
		internal IfcDistributionChamberElementTypeEnum mPredefinedType = IfcDistributionChamberElementTypeEnum.NOTDEFINED;
		public IfcDistributionChamberElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcDistributionChamberElementType() : base() { }
		internal IfcDistributionChamberElementType(DatabaseIfc db, IfcDistributionChamberElementType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcDistributionChamberElementType(DatabaseIfc db, string name, IfcDistributionChamberElementTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcDistributionCircuit : IfcDistributionSystem
	{
		internal IfcDistributionCircuit() : base() { }
		internal IfcDistributionCircuit(DatabaseIfc db, IfcDistributionCircuit c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { }
		public IfcDistributionCircuit(IfcSpatialElement bldg, string name,IfcDistributionSystemEnum type) : base(bldg, name, type) { }
	} 
	[Serializable]
	public partial class IfcDistributionControlElement : IfcDistributionElement // SUPERTYPE OF(ONEOF(IfcActuator, IfcAlarm, IfcController,
	{ // IfcFlowInstrument, IfcProtectiveDeviceTrippingUnit, IfcSensor, IfcUnitaryControlElement)) //"IFCDISTRIBUTIONCONTROLELEMENT"
		internal override string KeyWord { get { return mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcDistributionControlElement" : base.KeyWord; } }

		internal string mControlElementId = "$";// : OPTIONAL IfcIdentifier;
		public string ControlElementId { get { return (mControlElementId == "$" ? "" : ParserIfc.Decode(mControlElementId)); } set { mControlElementId = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcDistributionControlElement() : base() { }
		internal IfcDistributionControlElement(DatabaseIfc db, IfcDistributionControlElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { }
		public IfcDistributionControlElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductRepresentation r, IfcDistributionSystem system) : base(host,p,r, system) { }
	}
	[Serializable]
	public abstract partial class IfcDistributionControlElementType : IfcDistributionElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcActuatorType ,IfcAlarmType ,IfcControllerType ,IfcFlowInstrumentType ,IfcSensorType))
	{
		protected IfcDistributionControlElementType() : base() { }
		protected IfcDistributionControlElementType(DatabaseIfc db) : base(db) { }
		protected IfcDistributionControlElementType(DatabaseIfc db, IfcDistributionControlElementType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcDistributionElement : IfcElement //SUPERTYPE OF (ONEOF (IfcDistributionControlElement ,IfcDistributionFlowElement))
	{
		internal IfcDistributionElement() : base() { }
		protected IfcDistributionElement(IfcDistributionElement basis) : base(basis) { }
		protected IfcDistributionElement(DatabaseIfc db, IfcDistributionElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { }
		public IfcDistributionElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		public IfcDistributionElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductRepresentation r, IfcDistributionSystem system) : this(host,p,r) { if (system != null) system.AddRelated(this); }
		
		public SET<IfcRelConnectsPortToElement> HasPorts { get { return mHasPorts; } }
		
	}
	[Serializable]
	public partial class IfcDistributionElementType : IfcElementType //SUPERTYPE OF(ONEOF(IfcDistributionControlElementType, IfcDistributionFlowElementType))
	{
		internal IfcDistributionElementType() : base() { }
		internal IfcDistributionElementType(IfcDistributionElementType basis) : base(basis) { }
		protected IfcDistributionElementType(DatabaseIfc db) : base(db) { }
		protected IfcDistributionElementType(DatabaseIfc db, IfcDistributionElementType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcDistributionFlowElement : IfcDistributionElement //SUPERTYPE OF (ONEOF (IfcDistributionChamberElement ,IfcEnergyConversionDevice ,
	{ 	//IfcFlowController ,IfcFlowFitting ,IfcFlowMovingDevice,IfcFlowSegment ,IfcFlowStorageDevice ,IfcFlowTerminal ,IfcFlowTreatmentDevice))
		internal override string KeyWord { get { return mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcDistributionFlowElement" : base.KeyWord; } }

		//INVERSE 	HasControlElements : SET [0:1] OF IfcRelFlowControlElements FOR RelatingFlowElement;
		//GG
		internal IfcDistributionPort mSourcePort, mSinkPort;

		internal IfcDistributionFlowElement() : base() { }
		protected IfcDistributionFlowElement(IfcDistributionFlowElement basis) : base(basis) { mSourcePort = basis.mSourcePort; mSinkPort = basis.mSinkPort;  }
		internal IfcDistributionFlowElement(DatabaseIfc db, IfcDistributionFlowElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { }
		public IfcDistributionFlowElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcDistributionFlowElementType : IfcDistributionElementType //IfcDistributionChamberElementType, IfcEnergyConversionDeviceType, IfcFlowControllerType,
	{ // IfcFlowFittingType, IfcFlowMovingDeviceType, IfcFlowSegmentType, IfcFlowStorageDeviceType, IfcFlowTerminalType, IfcFlowTreatmentDeviceType))
		protected IfcDistributionFlowElementType() : base() { }
		protected IfcDistributionFlowElementType(IfcDistributionFlowElementType basis) : base(basis) { }
		protected IfcDistributionFlowElementType(DatabaseIfc db) : base(db) { }
		protected IfcDistributionFlowElementType(DatabaseIfc db, IfcDistributionFlowElementType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcDistributionPort : IfcPort
	{
		internal IfcFlowDirectionEnum mFlowDirection = IfcFlowDirectionEnum.NOTDEFINED; //:	OPTIONAL IfcFlowDirectionEnum;
		internal IfcDistributionPortTypeEnum mPredefinedType = IfcDistributionPortTypeEnum.NOTDEFINED; // IFC4 : OPTIONAL IfcDistributionPortTypeEnum;
		internal IfcDistributionSystemEnum mSystemType = IfcDistributionSystemEnum.NOTDEFINED;// IFC4 : OPTIONAL IfcDistributionSystemEnum;

		public IfcFlowDirectionEnum FlowDirection { get { return mFlowDirection; } set { mFlowDirection = value; } }
		public IfcDistributionPortTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcDistributionSystemEnum SystemType { get { return mSystemType; } set { mSystemType = value; } }

		internal IfcDistributionPort() : base() { }
		internal IfcDistributionPort(DatabaseIfc db, IfcDistributionPort p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mFlowDirection = p.mFlowDirection; mPredefinedType = p.mPredefinedType; mSystemType = p.mSystemType; }
		public IfcDistributionPort(IfcElement host) : base(host) { }
		public IfcDistributionPort(IfcElementType host) : base(host) { }
		public IfcDistributionPort(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcDistributionSystem : IfcSystem //SUPERTYPE OF(IfcDistributionCircuit) IFC4
	{
		internal string mLongName = "$"; // 	OPTIONAL IfcLabel
		internal IfcDistributionSystemEnum mPredefinedType = IfcDistributionSystemEnum.NOTDEFINED;// : OPTIONAL IfcDistributionSystemEnum

		public string LongName { get { return (mLongName == "$" ? "" : ParserIfc.Decode(mLongName)); } set { mLongName = (string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value)); } }
		public IfcDistributionSystemEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcDistributionSystem() : base() { }
		internal IfcDistributionSystem(DatabaseIfc db, IfcDistributionSystem s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { mLongName = s.mLongName; mPredefinedType = s.mPredefinedType; }
		public IfcDistributionSystem(IfcSpatialElement bldg, string name, IfcDistributionSystemEnum type) : base(bldg, name) { mPredefinedType = type; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDocumentElectronicFormat : BaseClassIfc // DEPRECEATED IFC4
	{
		internal string mFileExtension = "$";//  OPTIONAL IfcLabel;
		internal string mMimeContentType = "$";//  OPTIONAL IfcLabel;
		internal string mMimeSubtype = "$";//  OPTIONAL IfcLabel;
		internal IfcDocumentElectronicFormat() : base() { }
		//internal IfcDocumentElectronicFormat(IfcDocumentElectronicFormat i) : base() { mFileExtension = i.mFileExtension; mMimeContentType = i.mMimeContentType; mMimeSubtype = i.mMimeSubtype; }
	}
	[Serializable]
	public partial class IfcDocumentInformation : IfcExternalInformation, IfcDocumentSelect, NamedObjectIfc
	{
		internal string mIdentification;// : IfcIdentifier;
		internal string mName;// :  IfcLabel;
		internal string mDescription = "";// : OPTIONAL IfcText;
		internal List<int> mDocumentReferences = new List<int>(); // ifc2x3 : OPTIONAL SET [1:?] OF IfcDocumentReference;
		internal string mLocation = "";// : IFC4	OPTIONAL IfcURIReference;
		internal string mPurpose = "", mIntendedUse = "", mScope = "";// : OPTIONAL IfcText;
		internal string mRevision = "";// : OPTIONAL IfcLabel;
		internal int mDocumentOwner;// : OPTIONAL IfcActorSelect;
		internal List<int> mEditors = new List<int>();// : OPTIONAL SET [1:?] OF IfcActorSelect;
		internal DateTime mCreationTime = DateTime.MinValue, mLastRevisionTime = DateTime.MinValue;// : OPTIONAL IFC4 IfcDateTime;
		internal string mElectronicFormat = "";// IFC4	 :	OPTIONAL IfcIdentifier; IFC4
		internal int mSSElectronicFormat;// IFC2x3 : OPTIONAL IfcDocumentElectronicFormat;
		internal DateTime mValidFrom = DateTime.MinValue, mValidUntil = DateTime.MinValue;// : OPTIONAL Ifc2x3 IfcCalendarDate; IFC4 IfcDate
		internal int mSSValidFrom = 0, mSSVAlidUntil = 0;
		internal IfcDocumentConfidentialityEnum mConfidentiality = IfcDocumentConfidentialityEnum.NOTDEFINED;// : OPTIONAL IfcDocumentConfidentialityEnum;
		internal IfcDocumentStatusEnum mStatus = IfcDocumentStatusEnum.NOTDEFINED;// : OPTIONAL IfcDocumentStatusEnum; 
																				  //INVERSE
		internal List<IfcRelAssociatesDocument> mDocumentInfoForObjects = new List<IfcRelAssociatesDocument>();//	 :	SET OF IfcRelAssociatesDocument FOR RelatingDocument;
		internal List<IfcDocumentReference> mHasDocumentReferences = new List<IfcDocumentReference>();//	 :	SET OF IfcDocumentReference FOR ReferencedDocument;
		internal List<IfcDocumentInformationRelationship> mIsPointedTo = new List<IfcDocumentInformationRelationship>();//	 :	SET OF IfcDocumentInformationRelationship FOR RelatedDocuments;
		internal List<IfcDocumentInformationRelationship> mIsPointer = new List<IfcDocumentInformationRelationship>();//	 :	SET [0:1] OF IfcDocumentInformationRelationship FOR RelatingDocument;

		public string Identification { get { return mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Name { get { return ParserIfc.Decode(mName); } set { mName = ParserIfc.Encode(value); } }
		public string Description { get { return mDescription == "$" ? "" : ParserIfc.Decode(mDescription); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		[Obsolete("DEPRECEATED IFC4", false)]
		public ReadOnlyCollection<IfcDocumentReference> DocumentReferences { get { return new ReadOnlyCollection<IfcDocumentReference>(mDocumentReferences.ConvertAll(x => mDatabase[x] as IfcDocumentReference)); } }
		public string Location { get { return mLocation == "$" ? "" : ParserIfc.Decode(mLocation); } set { mLocation = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Purpose { get { return mPurpose == "$" ? "" : ParserIfc.Decode(mPurpose); } set { mPurpose = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string IntendedUse { get { return mIntendedUse == "$" ? "" : ParserIfc.Decode(mIntendedUse); } set { mIntendedUse = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Scope { get { return mScope == "$" ? "" : ParserIfc.Decode(mScope); } set { mScope = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Revision { get { return mRevision == "$" ? "" : ParserIfc.Decode(mRevision); } set { mRevision = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcActorSelect DocumentOwner { get { return mDatabase[mDocumentOwner] as IfcActorSelect; } set { mDocumentOwner = (value == null ? 0 : value.Index); } }
		public ReadOnlyCollection<IfcActorSelect> Editors { get { return new ReadOnlyCollection<IfcActorSelect>(mEditors.ConvertAll(x => mDatabase[x] as IfcActorSelect)); } }
		public DateTime CreationTime { get { return mCreationTime; } set { mCreationTime = value; } }
		public DateTime LastRevisionTime { get { return mLastRevisionTime; } set { mLastRevisionTime = value; } } 
		public string ElectronicFormat { get { return mElectronicFormat == "$" ? "" : ParserIfc.Decode(mElectronicFormat); } set { mElectronicFormat = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public DateTime ValidFrom { get { return mValidFrom; } set { mValidFrom = value; } }
		public DateTime ValidUntil { get { return mValidUntil; } set { mValidUntil = value; } }
		public IfcDocumentConfidentialityEnum Confidentiality { get { return mConfidentiality; } set { mConfidentiality = value; } } 
		public IfcDocumentStatusEnum Status { get { return mStatus; } set { mStatus = value; } }

		public ReadOnlyCollection<IfcRelAssociatesDocument> Associates { get { return new ReadOnlyCollection<IfcRelAssociatesDocument>( mDocumentInfoForObjects); } }

		internal IfcDocumentInformation() : base() { }
		internal IfcDocumentInformation(DatabaseIfc db, IfcDocumentInformation i) : base(db,i)
		{
			mIdentification = i.mIdentification;
			mName = i.mName;
			mDescription = i.mDescription;
			i.DocumentReferences.ToList().ForEach(x => addReference( db.Factory.Duplicate(x) as IfcDocumentReference));
			mPurpose = i.mPurpose;
			mIntendedUse = i.mIntendedUse;
			mScope = i.mScope;
			mRevision = i.mRevision;
			mDocumentOwner = i.mDocumentOwner;
			i.mEditors.ForEach(x=> addEditor( db.Factory.Duplicate(i.mDatabase[x]) as IfcActorSelect));
			mCreationTime = i.mCreationTime;
			mLastRevisionTime = i.mLastRevisionTime;
			mElectronicFormat = i.mElectronicFormat;
			if(i.mSSElectronicFormat > 0)
				mSSElectronicFormat = db.Factory.Duplicate(i.mDatabase[i.mSSElectronicFormat]).mIndex;
			//if(i.mValidFrom > 0)
			//	ValidFrom = db.Factory.Duplicate( i.ValidFrom) as IfcCalendarDate;
			//if(i.mValidUntil > 0)
			//	ValidUntil = db.Factory.Duplicate( i.ValidUntil) as IfcCalendarDate;
#warning todo
			mConfidentiality = i.mConfidentiality;
			mStatus = i.mStatus;
		}
		public IfcDocumentInformation(DatabaseIfc db, string identification, string name) : base(db)
		{
			Identification = identification;
			Name = name;
		}
		public void Associate(IfcRelAssociatesDocument associates) { mDocumentInfoForObjects.Add(associates); }
		internal void addReference(IfcDocumentReference reference) { mDocumentReferences.Add(reference.mIndex); }
		internal void addEditor(IfcActorSelect editor) { mEditors.Add(editor.Index); }
	}
	[Serializable]
	public partial class IfcDocumentInformationRelationship : BaseClassIfc
	{
		internal int mRelatingDocument; //: IfcDocumentInformation
		internal List<int> mRelatedDocuments = new List<int>();// : SET [1:?] OF IfcDocumentInformation;
		internal string mRelationshipType = "$";// : OPTIONAL IfcLabel;
		internal IfcDocumentInformationRelationship() : base() { }
	//	internal IfcDocumentInformationRelationship(IfcDocumentInformationRelationship v) : base() { mRelatingDocument = v.mRelatingDocument; mRelatedDocuments = new List<int>(v.mRelatedDocuments.ToArray()); mRelationshipType = v.mRelationshipType; }
	}
	[Serializable]
	public partial class IfcDocumentReference : IfcExternalReference, IfcDocumentSelect
	{
		internal string mDescription = "$";// IFC4	 :	OPTIONAL IfcText;
		internal int mReferencedDocument = 0;// IFC	 :	OPTIONAL IfcDocumentInformation;
		//INVERSE
		internal List<IfcRelAssociatesDocument> mDocumentRefForObjects = new List<IfcRelAssociatesDocument>();//	 :	SET OF IfcRelAssociatesDocument FOR RelatingDocument;

		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcDocumentInformation ReferencedDocument { get { return mDatabase[mReferencedDocument] as IfcDocumentInformation; } set { mReferencedDocument = (value == null ? 0 : value.mIndex); } }
		public ReadOnlyCollection<IfcRelAssociatesDocument> Associates { get { return new ReadOnlyCollection<IfcRelAssociatesDocument>( mDocumentRefForObjects); } }

		internal IfcDocumentReference() : base() { }
		internal IfcDocumentReference(DatabaseIfc db, IfcDocumentReference r) : base(db,r) { mDescription = r.mDescription; if(r.mReferencedDocument > 0) ReferencedDocument = db.Factory.Duplicate(r.ReferencedDocument) as IfcDocumentInformation;  }
		public IfcDocumentReference(DatabaseIfc db) : base(db) { }

		internal void associate(IfcDefinitionSelect d) { if (mDocumentRefForObjects.Count == 0) { new IfcRelAssociatesDocument(this); } mDocumentRefForObjects[0].RelatedObjects.Add(d); }
		public void Associate(IfcRelAssociatesDocument associates) { mDocumentRefForObjects.Add(associates); }
	}
	public interface IfcDocumentSelect : NamedObjectIfc //IFC4 SELECT (	IfcDocumentReference, IfcDocumentInformation);
	{
		ReadOnlyCollection<IfcRelAssociatesDocument> Associates { get; }
		void Associate(IfcRelAssociatesDocument associates);
	}
	[Serializable]
	public partial class IfcDoor : IfcBuildingElement
	{
		internal double mOverallHeight = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mOverallWidth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcDoorTypeEnum mPredefinedType = IfcDoorTypeEnum.NOTDEFINED;//: OPTIONAL IfcDoorTypeEnum; //IFC4 
		internal IfcDoorTypeOperationEnum mOperationType = IfcDoorTypeOperationEnum.NOTDEFINED;// : OPTIONAL IfcDoorTypeOperationEnum; //IFC4
		internal string mUserDefinedOperationType = "$";//	 :	OPTIONAL IfcLabel;

		public double OverallHeight { get { return mOverallHeight; } set { mOverallHeight = (value > 0 ? value : double.NaN); } }
		public double OverallWidth { get { return mOverallWidth; } set { mOverallWidth = (value > 0 ? value : double.NaN); } }
		public IfcDoorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcDoorTypeOperationEnum OperationType { get { return mOperationType; } set { mOperationType = value; } }
		public string UserDefinedOperationType { get { return (mUserDefinedOperationType == "$" ? "" : ParserIfc.Decode(mUserDefinedOperationType)); } set { mUserDefinedOperationType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcDoor() : base() { }
		internal IfcDoor(DatabaseIfc db, IfcDoor d, IfcOwnerHistory ownerHistory, bool downStream) : base(db, d, ownerHistory, downStream) { mOverallHeight = d.mOverallHeight; mOverallWidth = d.mOverallWidth; mPredefinedType = d.mPredefinedType; mOperationType = d.mOperationType; mUserDefinedOperationType = d.mUserDefinedOperationType; }
		public IfcDoor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcDoorLiningProperties : IfcPreDefinedPropertySet // IFC2x3 IfcPropertySetDefinition
	{
		internal double mLiningDepth, mLiningThickness, mThresholdDepth, mThresholdThickness, mTransomThickness;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mTransomOffset = double.NaN, mLiningOffset = double.NaN, mThresholdOffset = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal double mCasingThickness = double.NaN, mCasingDepth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		private int mShapeAspectStyle;// : OPTIONAL IfcShapeAspect;  // DEPRECEATED IFC4
		internal double mLiningToPanelOffsetX = double.NaN, mLiningToPanelOffsetY = double.NaN;//	 :	OPTIONAL IfcLengthMeasure;  IFC4

		public IfcShapeAspect ShapeAspectStyle { get { return mDatabase[mShapeAspectStyle] as IfcShapeAspect; } set { mShapeAspectStyle = (value == null ? 0 : value.mIndex); } }
		
		internal IfcDoorLiningProperties() : base() { }
		internal IfcDoorLiningProperties(DatabaseIfc db, IfcDoorLiningProperties p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream)
		{
			mLiningDepth = p.mLiningDepth;
			mLiningThickness = p.mLiningThickness;
			mThresholdDepth = p.mThresholdDepth;
			mThresholdThickness = p.mThresholdThickness;
			mTransomThickness = p.mTransomThickness;
			mTransomOffset = p.mTransomOffset;
			mLiningOffset = p.mLiningOffset;
			mThresholdOffset = p.mThresholdOffset;
			mCasingThickness = p.mCasingThickness;
			mCasingDepth = p.mCasingDepth;
			if (p.mShapeAspectStyle > 0)
				ShapeAspectStyle = db.Factory.Duplicate(p.ShapeAspectStyle) as IfcShapeAspect;
			mLiningToPanelOffsetX = p.mLiningToPanelOffsetX;
			mLiningToPanelOffsetY = p.mLiningToPanelOffsetY;
		}
	}
	[Serializable]
	public partial class IfcDoorPanelProperties : IfcPreDefinedPropertySet //IFC2x3 IfcPropertySetDefinition
	{
		internal double mPanelDepth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcDoorPanelOperationEnum mOperationType;// : IfcDoorPanelOperationEnum;
		internal double mPanelWidth = double.NaN;// : OPTIONAL IfcNormalisedRatioMeasure;
		internal IfcDoorPanelPositionEnum mPanelPosition;// :IfcDoorPanelPositionEnum;
		private int mShapeAspectStyle;// : OPTIONAL IfcShapeAspect;  // DEPRECEATED IFC4

		public double PanelDepth { get { return mPanelDepth; } set { mPanelDepth = value; } }
		public IfcDoorPanelOperationEnum OperationType { get { return mOperationType; } set { mOperationType = value; } }
		public double PanelWidth { get { return mPanelWidth; } set { mPanelWidth = value; } }
		public IfcDoorPanelPositionEnum PanelPosition { get { return mPanelPosition; } set { mPanelPosition = value; } }

		internal IfcDoorPanelProperties() : base() { }
		[Obsolete("DEPRECEATED IFC4", false)]
		public IfcShapeAspect ShapeAspectStyle { get { return mDatabase[mShapeAspectStyle] as IfcShapeAspect; } set { mShapeAspectStyle = (value == null ? 0 : value.mIndex); } }
		internal IfcDoorPanelProperties(DatabaseIfc db, IfcDoorPanelProperties p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream)
		{
			mPanelDepth = p.mPanelDepth;
			mOperationType = p.mOperationType;
			mPanelWidth = p.mPanelWidth;
			mPanelPosition = p.mPanelPosition;
			if (p.mShapeAspectStyle > 0)
				ShapeAspectStyle = db.Factory.Duplicate(p.ShapeAspectStyle) as IfcShapeAspect;
		}
	}
	[Serializable]
	public partial class IfcDoorStandardCase : IfcDoor
	{
		internal override string KeyWord { get { return "IfcDoor"; } }
		internal IfcDoorStandardCase() : base() { }
		internal IfcDoorStandardCase(DatabaseIfc db, IfcDoorStandardCase d, IfcOwnerHistory ownerHistory, bool downStream) : base(db, d, ownerHistory, downStream) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDoorStyle : IfcTypeProduct //IFC2x3 
	{
		internal IfcDoorTypeOperationEnum mOperationType = IfcDoorTypeOperationEnum.NOTDEFINED;// : IfcDoorStyleOperationEnum;
		internal IfcDoorStyleConstructionEnum mConstructionType = IfcDoorStyleConstructionEnum.NOTDEFINED;// : IfcDoorStyleConstructionEnum; //IFC2x3
		internal bool mParameterTakesPrecedence = false;// : BOOLEAN; 
		internal bool mSizeable = false;// : BOOLEAN;  //IFC2x3
		internal IfcDoorStyle() : base() { }
		internal IfcDoorStyle(DatabaseIfc db, IfcDoorStyle s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { mOperationType = s.mOperationType; mConstructionType = s.mConstructionType; mParameterTakesPrecedence = s.mParameterTakesPrecedence; mSizeable = s.mSizeable; }
	}
	[Serializable]
	public partial class IfcDoorType : IfcBuildingElementType //IFC2x3 IfcDoorStyle
	{
		internal override string KeyWord { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcDoorStyle" : base.KeyWord); } }

		internal IfcDoorTypeEnum mPredefinedType = IfcDoorTypeEnum.NOTDEFINED;
		internal IfcDoorTypeOperationEnum mOperationType;// : IfcDoorStyleOperationEnum; 
		internal bool mParameterTakesPrecedence = false;// : BOOLEAN;  
		internal string mUserDefinedOperationType = "$";//	 :	OPTIONAL IfcLabel;

		public IfcDoorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public string UserDefinedOperationType { get { return (mUserDefinedOperationType == "$" ? "" : ParserIfc.Decode(mUserDefinedOperationType)); } set { mUserDefinedOperationType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcDoorType() : base() { }
		internal IfcDoorType(DatabaseIfc db, IfcDoorType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; mOperationType = t.mOperationType; mParameterTakesPrecedence = t.mParameterTakesPrecedence; mUserDefinedOperationType = t.mUserDefinedOperationType; }
		public IfcDoorType(DatabaseIfc m, string name, IfcDoorTypeEnum type) : this(m, name, type, IfcDoorTypeOperationEnum.NOTDEFINED, false) { }
		internal IfcDoorType(DatabaseIfc m, string name, IfcDoorTypeEnum type, IfcDoorTypeOperationEnum operation, IfcDoorLiningProperties lp, List<IfcDoorPanelProperties> pps)
			: base(m)
		{
			Name = name;
			if (lp != null) mHasPropertySets.Add(lp);
			if (pps != null && pps.Count > 0) mHasPropertySets.AddRange(pps);
			mPredefinedType = type;
			mOperationType = operation;
			mParameterTakesPrecedence = true;
			 
		}
		internal IfcDoorType(DatabaseIfc m, string name, IfcDoorTypeEnum type, IfcDoorTypeOperationEnum operation, bool parameterTakesPrecendence)
			: base(m)
		{
			Name = name;
			mPredefinedType = type;
			mOperationType = operation;
			mParameterTakesPrecedence = parameterTakesPrecendence;
		}
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDraughtingCallout : IfcGeometricRepresentationItem // DEPRECEATED IFC4 SUPERTYPE OF (ONEOF (IfcDimensionCurveDirectedCallout ,IfcStructuredDimensionCallout))
	{
		internal List<int> mContents = new List<int>(); //: SET [1:?] OF IfcDraughtingCalloutElement 
		internal IfcDraughtingCallout() : base() { }
		//internal IfcDraughtingCallout(IfcDraughtingCallout el) : base(el) { mContents = new List<int>(el.mContents.ToArray()); }
	}
	public interface IfcDraughtingCalloutElement : IBaseClassIfc { } //SELECT (IfcAnnotationCurveOccurrence ,IfcAnnotationTextOccurrence ,IfcAnnotationSymbolOccurrence);
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDraughtingCalloutRelationship : BaseClassIfc // DEPRECEATED IFC4
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal int mRelatingDraughtingCallout;// : IfcDraughtingCallout;
		internal int mRelatedDraughtingCallout;// : IfcDraughtingCallout;
		internal IfcDraughtingCalloutRelationship() : base() { }
	//	internal IfcDraughtingCalloutRelationship(IfcDraughtingCalloutRelationship o) : base() { mName = o.mName; mDescription = o.mDescription; mRelatingDraughtingCallout = o.mRelatingDraughtingCallout; mRelatedDraughtingCallout = o.mRelatedDraughtingCallout; }
	}
	[Serializable]
	public partial class IfcDraughtingPreDefinedColour : IfcPreDefinedColour
	{
		internal IfcDraughtingPreDefinedColour() : base() { }
	//	internal IfcDraughtingPreDefinedColour(IfcDraughtingPreDefinedColour i) : base(i) { }
	}
	[Serializable]
	public partial class IfcDraughtingPreDefinedCurveFont : IfcPreDefinedCurveFont
	{
		internal IfcDraughtingPreDefinedCurveFont() : base() { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcDraughtingPreDefinedTextFont : IfcPreDefinedTextFont // DEPRECEATED IFC4
	{
		internal IfcDraughtingPreDefinedTextFont() : base() { }
	}
	[Serializable]
	public partial class IfcDuctFitting : IfcFlowFitting //IFC4
	{
		internal IfcDuctFittingTypeEnum mPredefinedType = IfcDuctFittingTypeEnum.NOTDEFINED;// OPTIONAL : IfcDuctFittingTypeEnum;
		public IfcDuctFittingTypeEnum Predefined { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcDuctFitting() : base() { }
		internal IfcDuctFitting(DatabaseIfc db, IfcDuctFitting f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcDuctFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcDuctFittingType : IfcFlowFittingType
	{
		internal IfcDuctFittingTypeEnum mPredefinedType = IfcDuctFittingTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum;
		public IfcDuctFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcDuctFittingType() : base() { }
		internal IfcDuctFittingType(DatabaseIfc db, IfcDuctFittingType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcDuctFittingType(DatabaseIfc m, string name, IfcDuctFittingTypeEnum t) : base(m) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcDuctSegment : IfcFlowSegment //IFC4
	{
		internal IfcDuctSegmentTypeEnum mPredefinedType = IfcDuctSegmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcDuctSegmentTypeEnum;
		public IfcDuctSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcDuctSegment() : base() { }
		internal IfcDuctSegment(DatabaseIfc db, IfcDuctSegment s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { mPredefinedType = s.mPredefinedType; }
		public IfcDuctSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcDuctSegmentType : IfcFlowSegmentType
	{
		internal IfcDuctSegmentTypeEnum mPredefinedType = IfcDuctSegmentTypeEnum.NOTDEFINED;// : IfcDuctSegmentTypeEnum;
		public IfcDuctSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcDuctSegmentType() : base() { }
		internal IfcDuctSegmentType(DatabaseIfc db, IfcDuctSegmentType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcDuctSegmentType(DatabaseIfc db, string name, IfcDuctSegmentTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcDuctSilencer : IfcFlowTreatmentDevice //IFC4  
	{
		internal IfcDuctSilencerTypeEnum mPredefinedType = IfcDuctSilencerTypeEnum.NOTDEFINED;
		public IfcDuctSilencerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcDuctSilencer() : base() { }
		internal IfcDuctSilencer(DatabaseIfc db, IfcDuctSilencer s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { mPredefinedType = s.mPredefinedType; }
		public IfcDuctSilencer(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcDuctSilencerType : IfcFlowTreatmentDeviceType
	{
		internal IfcDuctSilencerTypeEnum mPredefinedType = IfcDuctSilencerTypeEnum.NOTDEFINED;// : IfcDuctSilencerTypeEnum;
		public IfcDuctSilencerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcDuctSilencerType() : base() { }
		internal IfcDuctSilencerType(DatabaseIfc db, IfcDuctSilencerType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcDuctSilencerType(DatabaseIfc db, string name, IfcDuctSilencerTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
}
