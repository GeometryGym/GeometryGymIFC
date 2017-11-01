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
	public partial interface IfcUnit : IBaseClassIfc { } // = SELECT(IfcDerivedUnit, IfcNamedUnit, IfcMonetaryUnit);
	public partial class IfcUnitaryControlElement : IfcDistributionControlElement //IFC4  
	{
		internal IfcUnitaryControlElementTypeEnum mPredefinedType = IfcUnitaryControlElementTypeEnum.NOTDEFINED;
		public IfcUnitaryControlElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcUnitaryControlElement() : base() { }
		internal IfcUnitaryControlElement(DatabaseIfc db, IfcUnitaryControlElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { mPredefinedType = e.mPredefinedType; }
		internal IfcUnitaryControlElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductRepresentation r, IfcDistributionSystem system) : base(host,p,r, system) { }
	}
	public partial class IfcUnitaryControlElementType : IfcDistributionControlElementType
	{
		internal IfcUnitaryControlElementTypeEnum mPredefinedType = IfcUnitaryControlElementTypeEnum.NOTDEFINED;// : IfcUnitaryControlElementTypeEnum; 
		public IfcUnitaryControlElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcUnitaryControlElementType() : base() { }
		internal IfcUnitaryControlElementType(DatabaseIfc db, IfcUnitaryControlElementType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		internal IfcUnitaryControlElementType(DatabaseIfc m, string name, IfcUnitaryControlElementTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	public partial class IfcUnitaryEquipment : IfcEnergyConversionDevice //IFC4
	{
		internal IfcUnitaryEquipmentTypeEnum mPredefinedType = IfcUnitaryEquipmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcUnitaryEquipmentTypeEnum;
		public IfcUnitaryEquipmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcUnitaryEquipment() : base() { }
		internal IfcUnitaryEquipment(DatabaseIfc db, IfcUnitaryEquipment e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { mPredefinedType = e.mPredefinedType; }
		public IfcUnitaryEquipment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	public partial class IfcUnitaryEquipmentType : IfcEnergyConversionDeviceType
	{
		internal IfcUnitaryEquipmentTypeEnum mPredefinedType = IfcUnitaryEquipmentTypeEnum.NOTDEFINED;// : IfcUnitaryEquipmentTypeEnum; 
		public IfcUnitaryEquipmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcUnitaryEquipmentType() : base() { }
		internal IfcUnitaryEquipmentType(DatabaseIfc db, IfcUnitaryEquipmentType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcUnitaryEquipmentType(DatabaseIfc m, string name, IfcUnitaryEquipmentTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	public partial class IfcUnitAssignment : BaseClassIfc
	{
		public enum Length { Metre, Centimetre, Millimetre, Foot, Inch };

		private List<int> mUnits = new List<int>();// : SET [1:?] OF IfcUnit; 
		public ReadOnlyCollection<IfcUnit> Units { get { return new ReadOnlyCollection<IfcUnit>( mUnits.ConvertAll(x => mDatabase[x] as IfcUnit)); } }

		internal IfcUnitAssignment() : base() { }
		internal IfcUnitAssignment(DatabaseIfc db, IfcUnitAssignment u) : base(db) { u.mUnits.ForEach(x => AddUnit( db.Factory.Duplicate(u.mDatabase[x]) as IfcUnit)); }
		public IfcUnitAssignment(DatabaseIfc db) : base(db) { }
		public IfcUnitAssignment(DatabaseIfc db, Length length) : base(db) { SetUnits(length); }
		public IfcUnitAssignment(IfcUnit unit) : base(unit.Database) { AddUnit(unit); }
		public IfcUnitAssignment(IEnumerable<IfcUnit> units) : base(units.First().Database) { foreach(IfcUnit u in units) AddUnit(u); }

		public IfcUnitAssignment SetUnits(Length length)
		{
			AddUnit(mDatabase.Factory.LengthUnit(length));
			if (length == Length.Millimetre)
				mDatabase.ScaleSI = 0.001;
			else if (length == Length.Centimetre)
				mDatabase.ScaleSI = 0.01;
			else if (length == Length.Inch)
				mDatabase.ScaleSI = 0.0254;
			else if (length == Length.Foot)
				mDatabase.ScaleSI = FeetToMetre;
			else
				mDatabase.ScaleSI = 1;
			SetUnits();
			return this;
		}
		public static double FeetToMetre = 0.3048;
		internal void SetUnits()
		{
			if (Find(IfcUnitEnum.AREAUNIT) == null)
				mUnits.Add(mDatabase.Factory.SIArea.mIndex);
			if (Find(IfcUnitEnum.VOLUMEUNIT) == null)
				mUnits.Add(mDatabase.Factory.SIVolume.mIndex);
			if (Find(IfcUnitEnum.PLANEANGLEUNIT) == null)
			{
				IfcSIUnit radians = new IfcSIUnit(mDatabase, IfcUnitEnum.PLANEANGLEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.RADIAN);
				if (mDatabase.Factory.Options.AngleUnitsInRadians)
					AddUnit(radians);
				else
					AddUnit(new IfcConversionBasedUnit(IfcUnitEnum.PLANEANGLEUNIT, "DEGREE", new IfcMeasureWithUnit(new IfcPlaneAngleMeasure(Math.PI / 180.0), radians)));
			}
			if (Find(IfcUnitEnum.TIMEUNIT) == null)
			{
				IfcSIUnit seconds = new IfcSIUnit(mDatabase, IfcUnitEnum.TIMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SECOND);
				if (mDatabase.mTimeInDays)
				{
					IfcMeasureWithUnit mu = new IfcMeasureWithUnit(new IfcTimeMeasure(60 * 60 * 24), seconds);
					AddUnit(new IfcConversionBasedUnit(IfcUnitEnum.TIMEUNIT, "DAY", mu));
				}
				else
					AddUnit(seconds);
			}
		}
		internal bool mStructuralSet = false;
		internal void setStructuralUnits()
		{
			if (mStructuralSet)
				return;
			mStructuralSet = true;
			DatabaseIfc m = mDatabase;
			
			IfcNamedUnit fu = Find(IfcUnitEnum.FORCEUNIT);
			if (fu == null)
			{
				fu = new IfcSIUnit(m, IfcUnitEnum.FORCEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.NEWTON);
				AddUnit(fu);
			}
			IfcSIUnit lengthSI = m.Factory.SILength, volumeSI = m.Factory.SIVolume;
			if (Find(IfcDerivedUnitEnum.TORQUEUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, 1), IfcDerivedUnitEnum.TORQUEUNIT));
			if (Find(IfcDerivedUnitEnum.LINEARFORCEUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, -1), IfcDerivedUnitEnum.LINEARFORCEUNIT));
			if (Find(IfcDerivedUnitEnum.LINEARMOMENTUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), IfcDerivedUnitEnum.LINEARMOMENTUNIT));
			if (Find(IfcDerivedUnitEnum.PLANARFORCEUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, -2), IfcDerivedUnitEnum.PLANARFORCEUNIT));
			if (Find(IfcDerivedUnitEnum.MODULUSOFELASTICITYUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, -2), IfcDerivedUnitEnum.MODULUSOFELASTICITYUNIT));

			IfcNamedUnit time = Find(IfcUnitEnum.TIMEUNIT);
			if (time == null || Math.Abs(time.SIFactor - 1) < mDatabase.Tolerance)
				time = new IfcSIUnit(mDatabase, IfcUnitEnum.TIMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SECOND);
			if(Find(IfcDerivedUnitEnum.ACCELERATIONUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(lengthSI, 1), new IfcDerivedUnitElement(time, -2), IfcDerivedUnitEnum.ACCELERATIONUNIT));
			if(Find(IfcUnitEnum.PRESSUREUNIT) == null)
				AddUnit(new IfcSIUnit(m, IfcUnitEnum.PRESSUREUNIT, IfcSIPrefix.NONE, IfcSIUnitName.PASCAL));
			if (Find(IfcDerivedUnitEnum.SECTIONMODULUSUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(lengthSI, 3), IfcDerivedUnitEnum.SECTIONMODULUSUNIT));
			if(Find(IfcDerivedUnitEnum.MOMENTOFINERTIAUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(lengthSI, 4), IfcDerivedUnitEnum.MOMENTOFINERTIAUNIT));
			IfcSIUnit massu = Find(IfcUnitEnum.MASSUNIT) as IfcSIUnit;
			if (massu == null)
			{
				massu = new IfcSIUnit(m, IfcUnitEnum.MASSUNIT, IfcSIPrefix.KILO, IfcSIUnitName.GRAM);
				AddUnit(massu);
			}
			if (Find(IfcDerivedUnitEnum.MASSDENSITYUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(massu, 1), new IfcDerivedUnitElement(volumeSI, -1), IfcDerivedUnitEnum.MASSDENSITYUNIT));
			IfcSIUnit kelvin = Find(IfcUnitEnum.THERMODYNAMICTEMPERATUREUNIT) as IfcSIUnit;
			if (kelvin == null)
			{
				kelvin = new IfcSIUnit(m, IfcUnitEnum.THERMODYNAMICTEMPERATUREUNIT, IfcSIPrefix.NONE, IfcSIUnitName.KELVIN);
				AddUnit(kelvin);
			}
			if (Find(IfcDerivedUnitEnum.THERMALEXPANSIONCOEFFICIENTUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(kelvin, -1), IfcDerivedUnitEnum.THERMALEXPANSIONCOEFFICIENTUNIT));
			if(Find(IfcDerivedUnitEnum.LINEARSTIFFNESSUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, -1), IfcDerivedUnitEnum.LINEARSTIFFNESSUNIT));

			IfcNamedUnit radians = Find(IfcUnitEnum.PLANEANGLEUNIT);
			if (radians == null || Math.Abs(radians.SIFactor - 1) < mDatabase.Tolerance)
				radians = new IfcSIUnit(mDatabase, IfcUnitEnum.PLANEANGLEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.RADIAN);
			if(Find(IfcDerivedUnitEnum.ROTATIONALSTIFFNESSUNIT) == null)
				AddUnit(new IfcDerivedUnit(new IfcDerivedUnitElement(fu, 1), new IfcDerivedUnitElement(lengthSI, 1), new IfcDerivedUnitElement(radians, -1), IfcDerivedUnitEnum.ROTATIONALSTIFFNESSUNIT));
		}
		public void AddUnit(IfcUnit u) { mUnits.Add(u.Index); }
		internal IfcNamedUnit Find(IfcUnitEnum unit)
		{
			foreach (IfcUnit u in Units)
			{
				IfcNamedUnit nu = u as IfcNamedUnit;
				if (nu != null && nu.UnitType == unit)
					return nu;
			}
			return null;
		}
		internal IfcDerivedUnit Find(IfcDerivedUnitEnum unit)
		{
			foreach (IfcUnit u in Units)
			{
				IfcDerivedUnit du = u as IfcDerivedUnit;
				if (du != null && du.UnitType == unit)
					return du;
			}
			return null;
		}
		internal void Replace(IfcConversionBasedUnit cbu)
		{
			for (int icounter = 0; icounter < mUnits.Count; icounter++)
			{
				IfcNamedUnit u = mDatabase[mUnits[icounter]] as IfcNamedUnit;
				if (u != null && u.UnitType == cbu.UnitType)
				{
					mUnits[icounter] = cbu.mIndex;
					return;
				}
			}
		}
		internal double LengthScaleSI
		{
			get
			{
#if(REVIT)
			double result = GGYM.Units.MetreToFeet;
#else
				double result = 1;
#endif
				foreach (IfcUnit u in Units)
				{
					IfcNamedUnit nu = u as IfcNamedUnit;
					if (nu != null)
					{
						if (nu.UnitType == IfcUnitEnum.LENGTHUNIT)
						{
							double d = nu.SIFactor;
							mDatabase.ScaleSI = d;
							result = d;
						}
						//if (nu.UnitType == IfcUnitEnum.PLANEANGLEUNIT)
						//{
							//double d = nu.getSIFactor();
						//	mDatabase.mPlaneAngleToRadians = d;
						//}
					}
					else
					{
						IfcDerivedUnit du = u as IfcDerivedUnit;
						if (du != null)
						{

						}
					}
				}
				return result;
			}
		}
		internal double getScaleSI(IfcUnitEnum unitType)
		{
			foreach (IfcUnit u in Units)
			{
				IfcNamedUnit nu = u as IfcNamedUnit;
				if (nu != null && nu.UnitType == unitType)
				{
					IfcSIUnit si = nu as IfcSIUnit;
					if (si != null)
						return si.SIFactor;
					IfcConversionBasedUnit cbu = nu as IfcConversionBasedUnit;
					if (cbu != null)
					{
						return cbu.SIFactor;
					}
				}
			}
			return 1;
		}
		internal double getScaleSI(IfcDerivedUnitEnum unitType)
		{
			foreach (IfcUnit u in Units)
			{
				IfcDerivedUnit du = u as IfcDerivedUnit;
				if (du != null)
				{

				}
			}
			return 1;
		}
	}
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
		internal IfcUShapeProfileDef(DatabaseIfc db, IfcUShapeProfileDef p) : base(db, p)
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
		public IfcUShapeProfileDef(DatabaseIfc m, string name, double depth, double flangeWidth, double webThickness, double flangeThickness)
			: base(m,name) { mDepth = depth; mFlangeWidth = flangeWidth; mWebThickness = webThickness; mFlangeThickness = flangeThickness;  }
	}
}
