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
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcUndergroundExcavation : IfcExcavation
	{
		private IfcUndergroundExcavationTypeEnum mPredefinedType = IfcUndergroundExcavationTypeEnum.NOTDEFINED; //: OPTIONAL IfcUndergroundExcavationTypeEnum;
		public IfcUndergroundExcavationTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcUndergroundExcavationTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcUndergroundExcavation() : base() { }
		public IfcUndergroundExcavation(DatabaseIfc db) : base(db) { }
		public IfcUndergroundExcavation(DatabaseIfc db, IfcUndergroundExcavation excavation, DuplicateOptions options) : base(db, excavation, options) { PredefinedType = excavation.PredefinedType; }
		public IfcUndergroundExcavation(IfcElement host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	public partial interface IfcUnit : IBaseClassIfc { } // = SELECT(IfcDerivedUnit, IfcNamedUnit, IfcMonetaryUnit);
	[Serializable]
	public partial class IfcUnitaryControlElement : IfcDistributionControlElement //IFC4  
	{
		private IfcUnitaryControlElementTypeEnum mPredefinedType = IfcUnitaryControlElementTypeEnum.NOTDEFINED;
		public IfcUnitaryControlElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcUnitaryControlElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcUnitaryControlElement() : base() { }
		internal IfcUnitaryControlElement(DatabaseIfc db, IfcUnitaryControlElement e, DuplicateOptions options) : base(db, e, options) { PredefinedType = e.PredefinedType; }
		public IfcUnitaryControlElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductDefinitionShape r, IfcDistributionSystem system) : base(host,p,r, system) { }
	}
	[Serializable]
	public partial class IfcUnitaryControlElementType : IfcDistributionControlElementType
	{
		private IfcUnitaryControlElementTypeEnum mPredefinedType = IfcUnitaryControlElementTypeEnum.NOTDEFINED;// : IfcUnitaryControlElementTypeEnum; 
		public IfcUnitaryControlElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcUnitaryControlElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcUnitaryControlElementType() : base() { }
		internal IfcUnitaryControlElementType(DatabaseIfc db, IfcUnitaryControlElementType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcUnitaryControlElementType(DatabaseIfc db, string name, IfcUnitaryControlElementTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcUnitaryEquipment : IfcEnergyConversionDevice //IFC4
	{
		private IfcUnitaryEquipmentTypeEnum mPredefinedType = IfcUnitaryEquipmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcUnitaryEquipmentTypeEnum;
		public IfcUnitaryEquipmentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcUnitaryEquipmentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcUnitaryEquipment() : base() { }
		internal IfcUnitaryEquipment(DatabaseIfc db, IfcUnitaryEquipment e, DuplicateOptions options) : base(db, e, options) { PredefinedType = e.PredefinedType; }
		public IfcUnitaryEquipment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcUnitaryEquipmentType : IfcEnergyConversionDeviceType
	{
		private IfcUnitaryEquipmentTypeEnum mPredefinedType = IfcUnitaryEquipmentTypeEnum.NOTDEFINED;// : IfcUnitaryEquipmentTypeEnum; 
		public IfcUnitaryEquipmentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcUnitaryEquipmentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcUnitaryEquipmentType() : base() { }
		internal IfcUnitaryEquipmentType(DatabaseIfc db, IfcUnitaryEquipmentType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcUnitaryEquipmentType(DatabaseIfc db, string name, IfcUnitaryEquipmentTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcUnitAssignment : BaseClassIfc
	{
		public enum Length { Metre, Centimetre, Millimetre, Foot, Inch, USSurveyFoot };

		private SET<IfcUnit> mUnits = new SET<IfcUnit>();// : SET [1:?] OF IfcUnit; 
		public SET<IfcUnit> Units { get { return mUnits; } }

		internal IfcUnitAssignment() : base() { }
		internal IfcUnitAssignment(DatabaseIfc db, IfcUnitAssignment u, DuplicateOptions options) : base(db) { Units.AddRange(u.Units.Select(x => db.Factory.Duplicate(x, options))); }
		public IfcUnitAssignment(DatabaseIfc db) : base(db) { }
		public IfcUnitAssignment(DatabaseIfc db, Length length) : base(db) { SetUnits(length); }
		public IfcUnitAssignment(params IfcUnit[] units) : base(units.First().Database) { Units.AddRange(units); }
		public IfcUnitAssignment(IEnumerable<IfcUnit> units) : base(units.First().Database) { Units.AddRange(units); }

		internal static double scaleSI(Length length)
		{
			if (length == Length.Millimetre)
				return 0.001;
			else if (length == Length.Centimetre)
				return 0.01;
			if (length == Length.Inch)
				return 0.0254;
			if (length == Length.Foot)
				return FeetToMetre;
			if (length == Length.USSurveyFoot)
				return 12000.0 / 3937.0;

			return 1;
		}
		public IfcUnitAssignment SetUnits(Length length)
		{
			Units.Add(mDatabase.Factory.LengthUnit(length));
			double scale = scaleSI(length);
			if (length == Length.Inch || length == Length.Foot || length == Length.USSurveyFoot)
			{
				Units.Add(mDatabase.Factory.ConversionUnit(IfcConversionBasedUnit.CommonUnitName.square_foot));
				Units.Add(mDatabase.Factory.ConversionUnit(IfcConversionBasedUnit.CommonUnitName.cubic_foot));
				//farenheit
			}
			SetUnits();
			return this;
		}
		public static double FeetToMetre = 0.3048;
		internal void SetUnits()
		{
			if (this[IfcUnitEnum.AREAUNIT] == null)
				Units.Add(mDatabase.Factory.SIArea);
			if (this[IfcUnitEnum.VOLUMEUNIT] == null)
				Units.Add(mDatabase.Factory.SIVolume);
			if (this[IfcUnitEnum.PLANEANGLEUNIT] == null)
			{
				if (mDatabase.Factory.Options.AngleUnitsInRadians)
					Units.Add(new IfcSIUnit(mDatabase, IfcUnitEnum.PLANEANGLEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.RADIAN));
				else
					Units.Add(mDatabase.Factory.ConversionUnit(IfcConversionBasedUnit.CommonUnitName.degree));
			}
			if (this[IfcUnitEnum.TIMEUNIT] == null)
			{
				IfcSIUnit seconds = new IfcSIUnit(mDatabase, IfcUnitEnum.TIMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SECOND);
				if (mDatabase.mTimeInDays)
				{
					IfcMeasureWithUnit mu = new IfcMeasureWithUnit(new IfcTimeMeasure(60 * 60 * 24), seconds);
					Units.Add(new IfcConversionBasedUnit(IfcUnitEnum.TIMEUNIT, "DAY", mu));
				}
				else
					Units.Add(seconds);
			}
		}
		internal bool mStructuralSet = false;
		internal void setStructuralUnits()
		{
			if (mStructuralSet)
				return;
			mStructuralSet = true;
			DatabaseIfc db = mDatabase;
			
			IfcNamedUnit fu = this[IfcUnitEnum.FORCEUNIT];
			if (fu == null)
			{
				fu = new IfcSIUnit(db, IfcUnitEnum.FORCEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.NEWTON);
				Units.Add(fu);
			}
			IfcSIUnit lengthSI = db.Factory.SILength, volumeSI = db.Factory.SIVolume;
			if (this[IfcDerivedUnitEnum.TORQUEUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, 1), IfcDerivedUnitEnum.TORQUEUNIT));
			if (this[IfcDerivedUnitEnum.LINEARFORCEUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, -1), IfcDerivedUnitEnum.LINEARFORCEUNIT));
			if (this[IfcDerivedUnitEnum.LINEARMOMENTUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), IfcDerivedUnitEnum.LINEARMOMENTUNIT));
			if (this[IfcDerivedUnitEnum.PLANARFORCEUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, -2), IfcDerivedUnitEnum.PLANARFORCEUNIT));
			if (this[IfcDerivedUnitEnum.MODULUSOFELASTICITYUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, -2), IfcDerivedUnitEnum.MODULUSOFELASTICITYUNIT));

			IfcNamedUnit time = this[IfcUnitEnum.TIMEUNIT];
			if (time == null || Math.Abs(time.SIFactor() - 1) < mDatabase.Tolerance)
				time = new IfcSIUnit(mDatabase, IfcUnitEnum.TIMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SECOND);
			if(this[IfcDerivedUnitEnum.ACCELERATIONUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(lengthSI, 1), new IfcDerivedUnitElement(time, -2), IfcDerivedUnitEnum.ACCELERATIONUNIT));
			if(this[IfcUnitEnum.PRESSUREUNIT] == null)
				Units.Add(new IfcSIUnit(db, IfcUnitEnum.PRESSUREUNIT, IfcSIPrefix.NONE, IfcSIUnitName.PASCAL));
			if (this[IfcDerivedUnitEnum.SECTIONMODULUSUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(lengthSI, 3), IfcDerivedUnitEnum.SECTIONMODULUSUNIT));
			if(this[IfcDerivedUnitEnum.MOMENTOFINERTIAUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(lengthSI, 4), IfcDerivedUnitEnum.MOMENTOFINERTIAUNIT));
			IfcSIUnit massu = this[IfcUnitEnum.MASSUNIT] as IfcSIUnit;
			if (massu == null)
			{
				massu = new IfcSIUnit(db, IfcUnitEnum.MASSUNIT, IfcSIPrefix.KILO, IfcSIUnitName.GRAM);
				Units.Add(massu);
			}
			if (this[IfcDerivedUnitEnum.MASSDENSITYUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(massu, 1), new IfcDerivedUnitElement(volumeSI, -1), IfcDerivedUnitEnum.MASSDENSITYUNIT));
			IfcSIUnit kelvin = this[IfcUnitEnum.THERMODYNAMICTEMPERATUREUNIT] as IfcSIUnit;
			if (kelvin == null)
			{
				kelvin = new IfcSIUnit(db, IfcUnitEnum.THERMODYNAMICTEMPERATUREUNIT, IfcSIPrefix.NONE, IfcSIUnitName.KELVIN);
				Units.Add(kelvin);
			}
			if (this[IfcDerivedUnitEnum.THERMALEXPANSIONCOEFFICIENTUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(kelvin, -1), IfcDerivedUnitEnum.THERMALEXPANSIONCOEFFICIENTUNIT));
			if(this[IfcDerivedUnitEnum.LINEARSTIFFNESSUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, -1), IfcDerivedUnitEnum.LINEARSTIFFNESSUNIT));

			IfcNamedUnit radians = this[IfcUnitEnum.PLANEANGLEUNIT];
			if (radians == null || Math.Abs(radians.SIFactor() - 1) < mDatabase.Tolerance)
				radians = new IfcSIUnit(mDatabase, IfcUnitEnum.PLANEANGLEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.RADIAN);
			if(this[IfcDerivedUnitEnum.ROTATIONALSTIFFNESSUNIT] == null)
				Units.Add(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, 1), new IfcDerivedUnitElement(radians, -1), IfcDerivedUnitEnum.ROTATIONALSTIFFNESSUNIT));
		}

		public IfcNamedUnit this[IfcUnitEnum unit]
		{
			get
			{
				foreach (IfcUnit u in Units)
				{
					IfcNamedUnit nu = u as IfcNamedUnit;
					if (nu != null && nu.UnitType == unit)
						return nu;
				}
				return null;
			}
		}
		public IfcDerivedUnit this[IfcDerivedUnitEnum unit]
		{
			get
			{
				foreach (IfcUnit u in Units)
				{
					IfcDerivedUnit du = u as IfcDerivedUnit;
					if (du != null && du.UnitType == unit)
						return du;
				}
				return null;
			}
		}
		
		internal void Replace(IfcNamedUnit unit)
		{
			IfcNamedUnit existing = mUnits.OfType<IfcNamedUnit>().Where(x => x.UnitType == unit.UnitType).FirstOrDefault();
			if (existing != null)
				mUnits.Remove(existing);
			Units.Add(unit);
		}
		
		public double ScaleSI(IfcUnitEnum unitType)
		{
			IfcNamedUnit namedUnit = this[unitType];
			if(namedUnit != null)
			{
				IfcSIUnit siUnit = namedUnit as IfcSIUnit;
				if(siUnit != null)
					return siUnit.SIFactor();
				IfcConversionBasedUnit conversionBasedUnit = namedUnit as IfcConversionBasedUnit;
				if(conversionBasedUnit != null)
					return conversionBasedUnit.SIFactor();
			}
			return 1;
		}
		public double ScaleSI(IfcDerivedUnitEnum unitType)
		{
			IfcDerivedUnit derivedUnit = this[unitType];
			if(derivedUnit != null)
				return derivedUnit.SIFactor();
			return 1;
		}
	}
	[Serializable]
	public partial class IfcUShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mDepth, mFlangeWidth, mWebThickness, mFlangeThickness;// : IfcPositiveLengthMeasure;
		internal double mFilletRadius = double.NaN, mEdgeRadius = double.NaN, mFlangeSlope = double.NaN;// : OPTIONAL IfcPlaneAngleMeasure;
		internal double mCentreOfGravityInX = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure // DELETED IFC4 	Superseded by respective attribute of IfcStructuralProfileProperties 

		public double Depth { get { return mDepth; } set { mDepth = value; } }
		public double FlangeWidth { get { return mFlangeWidth; } set { mFlangeWidth = value; } }
		public double WebThickness { get { return mWebThickness; } set { mWebThickness = value; } }
		public double FlangeThickness { get { return mFlangeThickness; } set { mFlangeThickness = value; } }
		public double FilletRadius { get { return mFilletRadius; } set { mFilletRadius = value; } }
		public double EdgeRadius { get { return mEdgeRadius; } set { mEdgeRadius = value; } }
		public double FlangeSlope { get { return mFlangeSlope; } set { mFlangeSlope = value; } }

		internal IfcUShapeProfileDef() : base() { }
		internal IfcUShapeProfileDef(DatabaseIfc db, IfcUShapeProfileDef p, DuplicateOptions options) : base(db, p, options)
		{
			mDepth = p.mDepth;
			mFlangeWidth = p.mFlangeWidth;
			mWebThickness = p.mWebThickness;
			mFlangeThickness = p.mFlangeThickness;
			mFilletRadius = p.mFilletRadius;
			mEdgeRadius = p.mEdgeRadius;
			mFlangeSlope = p.mFlangeSlope;
			mCentreOfGravityInX = p.mCentreOfGravityInX;
		}
		public IfcUShapeProfileDef(DatabaseIfc db, string name, double depth, double flangeWidth, double webThickness, double flangeThickness)
			: base(db,name) { mDepth = depth; mFlangeWidth = flangeWidth; mWebThickness = webThickness; mFlangeThickness = flangeThickness;  }
	}
}
