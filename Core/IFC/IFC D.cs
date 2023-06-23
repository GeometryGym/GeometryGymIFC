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
	[Serializable]
	public partial class IfcDamper : IfcFlowController //IFC4
	{
		private IfcDamperTypeEnum mPredefinedType = IfcDamperTypeEnum.NOTDEFINED;// OPTIONAL : IfcDamperTypeEnum;
		public IfcDamperTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDamperTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDamper() : base() { }
		internal IfcDamper(DatabaseIfc db, IfcDamper d, DuplicateOptions options) : base(db, d, options) { PredefinedType = d.PredefinedType; }
		public IfcDamper(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcDamperType : IfcFlowControllerType
	{
		private IfcDamperTypeEnum mPredefinedType = IfcDamperTypeEnum.NOTDEFINED;// : IfcDamperTypeEnum;
		public IfcDamperTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDamperTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDamperType() : base() { }
		internal IfcDamperType(DatabaseIfc db, IfcDamperType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcDamperType(DatabaseIfc db, string name, IfcDamperTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcDatasetInformation : IfcDocumentInformation, IfcDatasetSelect
	{ 
		private string mSchemaReference = "";//  :	OPTIONAL IfcURIReference; 
		//INVERSE  
		private SET<IfcRelAssociatesDataset> mDatasetInfoForObjects = new SET<IfcRelAssociatesDataset>(); //SET [0:?] OF IfcRelAssociatesDataset FOR RelatingDataset;	
		internal SET<IfcDatasetReference> mHasDatasetReferences = new SET<IfcDatasetReference>(); // SET [0:?] OF IfcDatasetReference FOR ReferencedDataset;

		public string SchemaReference { get { return mSchemaReference; } set { mSchemaReference = value; } }
		public SET<IfcRelAssociatesDataset> DatasetInfoForObjects { get { return mDatasetInfoForObjects; } }
		public SET<IfcDatasetReference> HasDatasetReferences { get { return mHasDatasetReferences; } }

		internal IfcDatasetInformation() : base() { }
		protected IfcDatasetInformation(DatabaseIfc db, IfcDatasetInformation r)
			: base(db, r) { mSchemaReference = r.mSchemaReference; }
		public IfcDatasetInformation(DatabaseIfc db, string identification, string name) : base(db, identification, name) { }
	
		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcDatasetInformation dataset = e as IfcDatasetInformation;
			if (dataset == null || !base.isDuplicate(e, tol))
				return false;

			if (string.Compare(SchemaReference, dataset.SchemaReference, false) != 0)
				return false;
			return true;
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcDatasetReference : IfcExternalReference, IfcDatasetSelect
	{  
		private string mDescription = "";//  :	OPTIONAL IfcText
		private IfcDatasetInformation mReferencedDataset;// :	OPTIONAL  IfcDatasetInformation
		private string mFilter = "";//  :OPTIONAL IfcText
		//INVERSE  
		private SET<IfcRelAssociatesDataset> mDatasetRefForObjects = new SET<IfcRelAssociatesDataset>(); //SET [0:?] OF IfcRelAssociatesDataset FOR RelatingDataset;	

		public string Description { get { return mDescription; } set { mDescription = value; } }
		public IfcDatasetInformation ReferencedDataset { get { return mReferencedDataset; } set { mReferencedDataset = value; } }
		public string Filter { get { return mFilter; } set { mFilter = value; } }
		public SET<IfcRelAssociatesDataset> DatasetRefForObjects { get { return mDatasetRefForObjects; } }

		internal IfcDatasetReference() : base() { }
		internal IfcDatasetReference(DatabaseIfc db, IfcDatasetReference r, DuplicateOptions options)
			: base(db, r, options) { mDescription = r.mDescription; mReferencedDataset = db.Factory.Duplicate(r.ReferencedDataset, options); mFilter = r.mFilter; }
		 public IfcDatasetReference(IfcDatasetInformation referencedDataset) : base(referencedDataset.Database) { ReferencedDataset = referencedDataset; }
		 public IfcDatasetReference(DatabaseIfc db, string name) : base(db) { Name = name; }
		
		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcDatasetReference datasetReference = e as IfcDatasetReference;
			if (datasetReference == null || !base.isDuplicate(e, tol))
				return false;
			if (!ReferencedDataset.isDuplicate(datasetReference.ReferencedDataset, tol))
				return false;
			if (string.Compare(Description, datasetReference.Description, false) != 0)
				return false;
			if (string.Compare(Identification, datasetReference.Identification, false) != 0)
				return false;
			return true;
		}
	}
	public interface IfcDatasetSelect : NamedObjectIfc { }
	[Obsolete("DELETED IFC4", false)]
	[Serializable]
	public partial class IfcDateAndTime : BaseClassIfc, IfcDateTimeSelect // DELETED IFC4
	{
		internal IfcCalendarDate mDateComponent;// : IfcCalendarDate;
		internal IfcLocalTime mTimeComponent;// : IfcLocalTime;
		public IfcCalendarDate DateComponent { get { return mDateComponent; } set { mDateComponent = value; } }
		public IfcLocalTime TimeComponent { get { return mTimeComponent; } set { mTimeComponent = value; } }

		internal IfcDateAndTime(IfcDateAndTime v) : base() { mDateComponent = v.mDateComponent; mTimeComponent = v.mTimeComponent; }
		internal IfcDateAndTime() : base() { }
		public IfcDateAndTime(IfcCalendarDate d, IfcLocalTime t) : base(d.mDatabase) { mDateComponent = d; mTimeComponent = t; }
		public IfcDateAndTime(DatabaseIfc db, DateTime dateTime) : base(db)
		{
			mDateComponent = new IfcCalendarDate(db, dateTime);
			mTimeComponent = new IfcLocalTime(db, dateTime);
		}
		public DateTime DateTime
		{
			get
			{
				IfcCalendarDate cd = mDateComponent;
				IfcLocalTime lt = mTimeComponent;
				int seconds = 0, milliSeconds = 0;
				if (!double.IsNaN(lt.mSecondComponent))
				{
					seconds = (int)Math.Floor(lt.mSecondComponent);
					milliSeconds = Math.Min((int)Math.Round((lt.mSecondComponent - seconds) * 1000,0), 999);
				}
				return new DateTime(cd.mYearComponent, cd.mMonthComponent, cd.mDayComponent, lt.mHourComponent, lt.mMinuteComponent, seconds, milliSeconds);
			}
		}
	}
	public interface IfcDateTimeSelect : IBaseClassIfc { DateTime DateTime { get; } } // IFC2x3 IfcCalenderDate, IfcDateAndTime, IfcLocalTime 
	[Serializable, VersionAdded(ReleaseVersion.IFC4X2)]
	public partial class IfcDeepFoundation : IfcBuiltElement
	{
		public IfcDeepFoundation() : base() { }
		public IfcDeepFoundation(DatabaseIfc db) : base(db) { }
		public IfcDeepFoundation(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		internal IfcDeepFoundation(DatabaseIfc db, IfcDeepFoundation f, DuplicateOptions options) : base(db, f, options) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X2)]
	public partial class IfcDeepFoundationType : IfcBuiltElementType
	{
		public IfcDeepFoundationType() : base() { }
		public IfcDeepFoundationType(DatabaseIfc db) : base(db) { }
		public IfcDeepFoundationType(DatabaseIfc db, string name) : base(db, name) { }
		internal IfcDeepFoundationType(DatabaseIfc db, IfcDeepFoundationType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcDefinedSymbol : IfcGeometricRepresentationItem
	{
		private IfcDefinedSymbolSelect mDefinition;// : IfcDefinedSymbolSelect;
		private IfcCartesianTransformationOperator2D mTarget;// : IfcCartesianTransformationOperator2D;

		public IfcDefinedSymbolSelect Definition { get { return mDefinition; } set { mDefinition = value; } }
		public IfcCartesianTransformationOperator2D Target { get { return mTarget; } set { mTarget = value; } }

		internal IfcDefinedSymbol() : base() { }
		internal IfcDefinedSymbol(DatabaseIfc db, IfcDefinedSymbol s, DuplicateOptions options) : base(db, s, options)
		{
			mDefinition = db.Factory.Duplicate(s.mDefinition) as IfcDefinedSymbolSelect; 
			mTarget = db.Factory.Duplicate(s.mTarget) as IfcCartesianTransformationOperator2D;
		}
		public IfcDefinedSymbol(IfcDefinedSymbolSelect definition, IfcCartesianTransformationOperator2D target) 
			: base(definition.Database)
		{
			mDefinition = definition;
			mTarget = target;
		}

	}
	public interface IfcDefinedSymbolSelect : NamedObjectIfc { }
	public interface IfcDefinitionSelect : IBaseClassIfc // IFC4 SELECT ( IfcObjectDefinition,  IfcPropertyDefinition);
	{
		IfcRelDeclares HasContext { get; set; }
		SET<IfcRelAssociates> HasAssociations { get; }
		List<T> Extract<T>() where T : IBaseClassIfc;
	}
	[Serializable]
	public partial class IfcDerivedProfileDef : IfcProfileDef
	{
		private IfcProfileDef mParentProfile;// : IfcProfileDef;
		private IfcCartesianTransformationOperator2D mOperator;// : IfcCartesianTransformationOperator2D;
		internal string mLabel = "";// : OPTIONAL IfcLabel;

		public IfcProfileDef ParentProfile { get { return mParentProfile; } set { mParentProfile = value; } }
		public IfcCartesianTransformationOperator2D Operator { get { return mOperator; } set { mOperator = value; } }
		public string Label { get { return mLabel; } set { mLabel = value; } }

		internal IfcDerivedProfileDef() : base() { }
		internal IfcDerivedProfileDef(DatabaseIfc db, IfcDerivedProfileDef p, DuplicateOptions options) : base(db, p, options)
		{
			ParentProfile = db.Factory.Duplicate(p.ParentProfile, options) as IfcProfileDef;
			Operator = db.Factory.Duplicate(p.Operator, options) as IfcCartesianTransformationOperator2D;
			mLabel = p.mLabel;
		}
		public IfcDerivedProfileDef(IfcProfileDef container, IfcCartesianTransformationOperator2D op, string name) : base(container.mDatabase, name) { ParentProfile = container; Operator = op; }
	}
	[Serializable]
	public partial class IfcDerivedUnit : BaseClassIfc, IfcUnit, NamedObjectIfc
	{
		private SET<IfcDerivedUnitElement> mElements = new SET<IfcDerivedUnitElement>();// : SET [1:?] OF IfcDerivedUnitElement;
		private IfcDerivedUnitEnum mUnitType;// : IfcDerivedUnitEnum;
		private string mUserDefinedType = "";// : OPTIONAL IfcLabel;
		private string mName = ""; //: OPTIONAL IfcLabel;

		public SET<IfcDerivedUnitElement> Elements { get { return mElements; } }
		public IfcDerivedUnitEnum UnitType { get { return mUnitType; } set { mUnitType = value; } }
		public string UserDefinedType { get { return mUserDefinedType; } set { mUserDefinedType = value; } }
		public string Name { get { return mName; } set { mName = value; } }

		internal IfcDerivedUnit() : base() { }
		internal IfcDerivedUnit(DatabaseIfc db, IfcDerivedUnit u, DuplicateOptions options) : base(db) 
		{ 
			Elements.AddRange(u.Elements.ConvertAll(x=>db.Factory.Duplicate(x, options))); 
			mUnitType = u.mUnitType; 
			mUserDefinedType = u.mUserDefinedType; 
		}
		public IfcDerivedUnit(IfcDerivedUnitElement element, IfcDerivedUnitEnum type) : base(element.mDatabase) { Elements.Add(element); mUnitType = type;  }
		public IfcDerivedUnit(IfcDerivedUnitEnum type, params IfcDerivedUnitElement[] elements) : base(elements.First().Database) { Elements.AddRange(elements); mUnitType = type;  }
		public IfcDerivedUnit(IEnumerable<IfcDerivedUnitElement> elements, IfcDerivedUnitEnum type) : base(elements.First().mDatabase) { Elements.AddRange(elements); mUnitType = type; }
		public IfcDerivedUnit(IEnumerable<IfcDerivedUnitElement> elements, string userDefinedType) : base(elements.First().mDatabase) { Elements.AddRange(elements); UserDefinedType = userDefinedType; mUnitType = IfcDerivedUnitEnum.USERDEFINED; }
		public IfcDerivedUnit(IfcDerivedUnitElement due1, IfcDerivedUnitElement due2, IfcDerivedUnitEnum type) : base(due1.mDatabase) { Elements.Add(due1); Elements.Add(due2); mUnitType = type;  }
		public IfcDerivedUnit(IfcDerivedUnitElement due1, IfcDerivedUnitElement due2, IfcDerivedUnitElement due3, IfcDerivedUnitEnum type) : base(due1.mDatabase) { Elements.Add(due1); Elements.Add(due2); Elements.Add(due3); mUnitType = type;  }
		
		public double SIFactor
		{
			get
			{
				SET<IfcDerivedUnitElement> elements = Elements;
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
		private IfcNamedUnit mUnit;// : IfcNamedUnit;
		private int mExponent;// : IfcInteger;

		public IfcNamedUnit Unit { get { return mUnit; } set { mUnit = value; } }
		public int Exponent { get { return mExponent; } set { mExponent = value; } } 

		internal IfcDerivedUnitElement() : base() { }
		internal IfcDerivedUnitElement(DatabaseIfc db, IfcDerivedUnitElement e, DuplicateOptions options) : base(db) { Unit = db.Factory.Duplicate(e.Unit, options); mExponent = e.mExponent; }
		public IfcDerivedUnitElement(IfcNamedUnit u, int exponent) : base(u.mDatabase) { Unit = u; mExponent = exponent; }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcDiameterDimension : IfcDimensionCurveDirectedCallout 
	{
		internal IfcDiameterDimension() : base() { }
		public IfcDiameterDimension(IfcDraughtingCalloutElement content) : base(content) { }
		public IfcDiameterDimension(IEnumerable<IfcDraughtingCalloutElement> contents) : base(contents) { }
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
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcDimensionCalloutRelationship : IfcDraughtingCalloutRelationship
	{
		internal IfcDimensionCalloutRelationship() : base() { }
		public IfcDimensionCalloutRelationship(IfcDraughtingCallout relatingDraughtingCallout, IfcDraughtingCallout relatedDraughtingCallout)
		: base(relatingDraughtingCallout, relatedDraughtingCallout) { }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcDimensionCurve : IfcAnnotationCurveOccurrence
	{
		internal SET<IfcTerminatorSymbol> mAnnotatedBySymbols = new SET<IfcTerminatorSymbol>();// SET [0:2] OF IfcTerminatorSymbol FOR AnnotatedCurve; 
		public SET<IfcTerminatorSymbol> AnnotatedBySymbols { get { return mAnnotatedBySymbols; } }
		internal IfcDimensionCurve() : base() { }
		public IfcDimensionCurve(IfcPresentationStyleAssignment style, IfcTerminatorSymbol symbol) 
			: base(style) { mAnnotatedBySymbols.Add(symbol); }
		public IfcDimensionCurve(IfcPresentationStyleAssignment style, IfcTerminatorSymbol symbol1, IfcTerminatorSymbol symbol2) 
			: this(style, symbol1) { mAnnotatedBySymbols.Add(symbol2); }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcDimensionCurveDirectedCallout : IfcDraughtingCallout // SUPERTYPE OF (ONEOF (IfcAngularDimension ,IfcDiameterDimension ,IfcLinearDimension ,IfcRadiusDimension))
	{
		internal IfcDimensionCurveDirectedCallout() : base() { }
		internal IfcDimensionCurveDirectedCallout(DatabaseIfc db, IfcDimensionCurveDirectedCallout c, DuplicateOptions options) : base(db, c, options) { }
		public IfcDimensionCurveDirectedCallout(IfcDraughtingCalloutElement content) : base(content) { }
		public IfcDimensionCurveDirectedCallout(IEnumerable<IfcDraughtingCalloutElement> contents) : base(contents) { }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcDimensionCurveTerminator : IfcTerminatorSymbol 
	{
		internal IfcDimensionExtentUsage mRole;// : IfcDimensionExtentUsage;
		public IfcDimensionExtentUsage Role { get { return mRole; } set { mRole = value; } }
		internal IfcDimensionCurveTerminator() : base() { }
		public IfcDimensionCurveTerminator(IfcPresentationStyleAssignment style, IfcAnnotationCurveOccurrence curve, IfcDimensionExtentUsage role) 
			: base(style, curve) { mRole = role;  }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcDimensionPair : IfcDraughtingCalloutRelationship
	{
		internal IfcDimensionPair() : base() { }
		public IfcDimensionPair(IfcDraughtingCallout relatingDraughtingCallout, IfcDraughtingCallout relatedDraughtingCallout)
		: base(relatingDraughtingCallout, relatedDraughtingCallout) { }
	}
	[Serializable]
	public partial class IfcDirection : IfcGeometricRepresentationItem, IfcGridPlacementDirectionSelect, IfcOrientationSelect, IfcVectorOrDirection
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
		internal IfcDirection(DatabaseIfc db, IfcDirection d, DuplicateOptions options) : base(db, d, options) { mDirectionRatioX = d.mDirectionRatioX; mDirectionRatioY = d.mDirectionRatioY; mDirectionRatioZ = d.mDirectionRatioZ; }
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
	public abstract partial class IfcDirectrixCurveSweptAreaSolid : IfcSweptAreaSolid //ABSTRACT SUPERTYPE OF(ONEOF(IfcFixedReferenceSweptAreaSolid, IfcSurfaceCurveSweptAreaSolid))
	{
		private IfcCurve mDirectrix = null; //: IfcCurve;
		private IfcCurveMeasureSelect mStartParam = null; //: OPTIONAL IfcCurveMeasureSelect IFC4 IfcParameterValue;
		private IfcCurveMeasureSelect mEndParam = null; //: OPTIONAL IfcCurveMeasureSelect IFC4 IfcParameterValue;

		public IfcCurve Directrix { get { return mDirectrix; } set { mDirectrix = value; } }
		public IfcCurveMeasureSelect StartParam { get { return mStartParam; } set { mStartParam = value; } }
		public IfcCurveMeasureSelect EndParam { get { return mEndParam; } set { mEndParam = value; } }

		protected IfcDirectrixCurveSweptAreaSolid() : base() { }
		protected IfcDirectrixCurveSweptAreaSolid(DatabaseIfc db, IfcDirectrixCurveSweptAreaSolid s, DuplicateOptions options) : base(db, s, options)
		{
			Directrix = db.Factory.Duplicate(s.Directrix, options) as IfcCurve;
			mStartParam = s.mStartParam;
			mEndParam = s.mEndParam;
		}
		protected IfcDirectrixCurveSweptAreaSolid(IfcProfileDef sweptArea, IfcCurve directrix)
			: base(sweptArea) { Directrix = directrix; }
	}
	[Serializable]
	public partial class IfcDirectrixDerivedReferenceSweptAreaSolid : IfcFixedReferenceSweptAreaSolid //IFC4.3
	{
		internal IfcDirectrixDerivedReferenceSweptAreaSolid() : base() { }
		internal IfcDirectrixDerivedReferenceSweptAreaSolid(DatabaseIfc db, IfcFixedReferenceSweptAreaSolid s, DuplicateOptions options) 
			: base(db, s, options) { }
		public IfcDirectrixDerivedReferenceSweptAreaSolid(IfcProfileDef sweptArea, IfcCurve directrix, IfcDirection reference)
			: base(sweptArea, directrix, reference) { }
	}
	[Serializable]
	public partial class IfcDiscreteAccessory : IfcElementComponent
	{
		private IfcDiscreteAccessoryTypeEnum mPredefinedType = IfcDiscreteAccessoryTypeEnum.NOTDEFINED;// : OPTIONAL IfcDiscreteAccessoryTypeEnum;
		public IfcDiscreteAccessoryTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDiscreteAccessoryTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDiscreteAccessory() : base() { }
		internal IfcDiscreteAccessory(DatabaseIfc db, IfcDiscreteAccessory a, DuplicateOptions options) : base(db, a, options) { PredefinedType = a.PredefinedType; }
		public IfcDiscreteAccessory(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		public IfcDiscreteAccessory(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public partial class IfcDiscreteAccessoryType : IfcElementComponentType
	{
		private IfcDiscreteAccessoryTypeEnum mPredefinedType = IfcDiscreteAccessoryTypeEnum.NOTDEFINED;//:	OPTIONAL IfcDiscreteAccessoryTypeEnum; IFC4	
		public IfcDiscreteAccessoryTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDiscreteAccessoryTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDiscreteAccessoryType() : base() { }
		internal IfcDiscreteAccessoryType(DatabaseIfc db, IfcDiscreteAccessoryType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcDiscreteAccessoryType(DatabaseIfc db, string name, IfcDiscreteAccessoryTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcDistributionBoard : IfcFlowController
	{
		private IfcDistributionBoardTypeEnum mPredefinedType = IfcDistributionBoardTypeEnum.NOTDEFINED; //: OPTIONAL IfcDistributionBoardTypeEnum;
		public IfcDistributionBoardTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDistributionBoardTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcDistributionBoard() : base() { }
		public IfcDistributionBoard(DatabaseIfc db) : base(db) { }
		public IfcDistributionBoard(DatabaseIfc db, IfcDistributionBoard distributionBoard, DuplicateOptions options) : base(db, distributionBoard, options) { PredefinedType = distributionBoard.PredefinedType; }
		public IfcDistributionBoard(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcDistributionBoardType : IfcFlowControllerType
	{
		private IfcDistributionBoardTypeEnum mPredefinedType = IfcDistributionBoardTypeEnum.NOTDEFINED; //: IfcDistributionBoardTypeEnum;
		public IfcDistributionBoardTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDistributionBoardTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcDistributionBoardType() : base() { }
		public IfcDistributionBoardType(DatabaseIfc db, IfcDistributionBoardType distributionBoardType, DuplicateOptions options) : base(db, distributionBoardType, options) { PredefinedType = distributionBoardType.PredefinedType; }
		public IfcDistributionBoardType(DatabaseIfc db, string name, IfcDistributionBoardTypeEnum predefinedType)
			: base(db) { Name = name; PredefinedType = predefinedType; }
	}
	[Serializable]
	public partial class IfcDistributionChamberElement : IfcDistributionFlowElement
	{
		private IfcDistributionChamberElementTypeEnum mPredefinedType = IfcDistributionChamberElementTypeEnum.NOTDEFINED;//	:	OPTIONAL IfcDistributionChamberElementTypeEnum;
		public IfcDistributionChamberElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDistributionChamberElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDistributionChamberElement() : base() { }
		internal IfcDistributionChamberElement(DatabaseIfc db, IfcDistributionChamberElement e, DuplicateOptions options) : base(db, e, options) { PredefinedType = e.PredefinedType; }
		public IfcDistributionChamberElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcDistributionChamberElementType : IfcDistributionFlowElementType
	{
		private IfcDistributionChamberElementTypeEnum mPredefinedType = IfcDistributionChamberElementTypeEnum.NOTDEFINED;
		public IfcDistributionChamberElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDistributionChamberElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDistributionChamberElementType() : base() { }
		internal IfcDistributionChamberElementType(DatabaseIfc db, IfcDistributionChamberElementType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcDistributionChamberElementType(DatabaseIfc db, string name, IfcDistributionChamberElementTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcDistributionCircuit : IfcDistributionSystem
	{
		internal IfcDistributionCircuit() : base() { }
		internal IfcDistributionCircuit(DatabaseIfc db, IfcDistributionCircuit c, DuplicateOptions options) : base(db, c, options) { }
		public IfcDistributionCircuit(IfcSpatialElement bldg, string name,IfcDistributionSystemEnum type) : base(bldg, name, type) { }
	} 
	[Serializable]
	public partial class IfcDistributionControlElement : IfcDistributionElement // SUPERTYPE OF(ONEOF(IfcActuator, IfcAlarm, IfcController,
	{ // IfcFlowInstrument, IfcProtectiveDeviceTrippingUnit, IfcSensor, IfcUnitaryControlElement)) //"IFCDISTRIBUTIONCONTROLELEMENT"
		public override string StepClassName { get { return mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcDistributionControlElement" : base.StepClassName; } }

		private string mControlElementId = "";// : OPTIONAL IfcIdentifier;
		[Obsolete("DEPRECATED IFC4", false)]
		public string ControlElementId { get { return mControlElementId; } set { mControlElementId = value; } }

		internal IfcDistributionControlElement() : base() { }
		internal IfcDistributionControlElement(DatabaseIfc db, IfcDistributionControlElement e, DuplicateOptions options) : base(db, e, options) { }
		public IfcDistributionControlElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductDefinitionShape r, IfcDistributionSystem system) : base(host, p, r, system) { }
	}
	[Serializable]
	public abstract partial class IfcDistributionControlElementType : IfcDistributionElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcActuatorType ,IfcAlarmType ,IfcControllerType ,IfcFlowInstrumentType ,IfcSensorType))
	{
		protected IfcDistributionControlElementType() : base() { }
		protected IfcDistributionControlElementType(DatabaseIfc db) : base(db) { }
		protected IfcDistributionControlElementType(DatabaseIfc db, IfcDistributionControlElementType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcDistributionElement : IfcElement //SUPERTYPE OF (ONEOF (IfcDistributionControlElement ,IfcDistributionFlowElement))
	{
		public SET<IfcRelConnectsPortToElement> HasPorts { get { return mHasPorts; } }

		internal IfcDistributionElement() : base() { }
		internal IfcDistributionElement(DatabaseIfc db) : base(db) { }
		protected IfcDistributionElement(DatabaseIfc db, IfcDistributionElement e, DuplicateOptions options) : base(db, e, options) { }
		public IfcDistributionElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		public IfcDistributionElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductDefinitionShape r, IfcDistributionSystem system) : this(host,p,r) { if (system != null) system.AddRelated(this); }
	}
	[Serializable]
	public partial class IfcDistributionElementType : IfcElementType //SUPERTYPE OF(ONEOF(IfcDistributionControlElementType, IfcDistributionFlowElementType))
	{
		internal IfcDistributionElementType() : base() { }
		public IfcDistributionElementType(DatabaseIfc db) : base(db) { }
		protected IfcDistributionElementType(DatabaseIfc db, IfcDistributionElementType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcDistributionFlowElement : IfcDistributionElement //SUPERTYPE OF (ONEOF (IfcDistributionChamberElement ,IfcEnergyConversionDevice ,
	{ 	//IfcFlowController ,IfcFlowFitting ,IfcFlowMovingDevice,IfcFlowSegment ,IfcFlowStorageDevice ,IfcFlowTerminal ,IfcFlowTreatmentDevice))
		public override string StepClassName { get { return mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcDistributionFlowElement" : base.StepClassName; } }

		//INVERSE 	HasControlElements : SET [0:1] OF IfcRelFlowControlElements FOR RelatingFlowElement;
		//GG
		internal IfcDistributionPort mSourcePort, mSinkPort;

		internal IfcDistributionFlowElement() : base() { }
		internal IfcDistributionFlowElement(DatabaseIfc db) : base(db) { }
		internal IfcDistributionFlowElement(DatabaseIfc db, IfcDistributionFlowElement e, DuplicateOptions options) : base(db, e, options) { }
		public IfcDistributionFlowElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcDistributionFlowElementType : IfcDistributionElementType //IfcDistributionChamberElementType, IfcEnergyConversionDeviceType, IfcFlowControllerType,
	{ // IfcFlowFittingType, IfcFlowMovingDeviceType, IfcFlowSegmentType, IfcFlowStorageDeviceType, IfcFlowTerminalType, IfcFlowTreatmentDeviceType))
		protected IfcDistributionFlowElementType() : base() { }
		protected IfcDistributionFlowElementType(DatabaseIfc db) : base(db) { }
		protected IfcDistributionFlowElementType(DatabaseIfc db, IfcDistributionFlowElementType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcDistributionPort : IfcPort
	{
		internal IfcFlowDirectionEnum mFlowDirection = IfcFlowDirectionEnum.NOTDEFINED; //:	OPTIONAL IfcFlowDirectionEnum;
		private IfcDistributionPortTypeEnum mPredefinedType = IfcDistributionPortTypeEnum.NOTDEFINED; // IFC4 : OPTIONAL IfcDistributionPortTypeEnum;
		internal IfcDistributionSystemEnum mSystemType = IfcDistributionSystemEnum.NOTDEFINED;// IFC4 : OPTIONAL IfcDistributionSystemEnum;

		public IfcFlowDirectionEnum FlowDirection { get { return mFlowDirection; } set { mFlowDirection = value; } }
		public IfcDistributionPortTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDistributionPortTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public IfcDistributionSystemEnum SystemType { get { return mSystemType; } set { mSystemType = value; } }

		internal IfcDistributionPort() : base() { }
		internal IfcDistributionPort(DatabaseIfc db, IfcDistributionPort p, DuplicateOptions options) : base(db, p, options) { mFlowDirection = p.mFlowDirection; PredefinedType = p.PredefinedType; mSystemType = p.mSystemType; }
		public IfcDistributionPort(IfcElement host) : base(host) { }
		public IfcDistributionPort(IfcElementType host) : base(host) { }
		public IfcDistributionPort(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcDistributionSystem : IfcSystem //SUPERTYPE OF(IfcDistributionCircuit) IFC4
	{
		public override string StepClassName { get { return (mDatabase != null && mDatabase.Release <= ReleaseVersion.IFC2x3 ? "IfcSystem" : base.StepClassName); } }
		internal string mLongName = ""; // 	OPTIONAL IfcLabel
		private IfcDistributionSystemEnum mPredefinedType = IfcDistributionSystemEnum.NOTDEFINED;// : OPTIONAL IfcDistributionSystemEnum

		public string LongName { get { return mLongName; } set { mLongName = value; } }
		public IfcDistributionSystemEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDistributionSystemEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDistributionSystem() : base() { }
		internal IfcDistributionSystem(DatabaseIfc db, IfcDistributionSystem s, DuplicateOptions options) : base(db, s, options) { mLongName = s.mLongName; PredefinedType = s.PredefinedType; }
		public IfcDistributionSystem(IfcSpatialElement bldg, string name, IfcDistributionSystemEnum type) : base(bldg, name) { PredefinedType = type; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcDocumentElectronicFormat : BaseClassIfc // DEPRECATED IFC4
	{
		internal string mFileExtension = "";//  OPTIONAL IfcLabel;
		internal string mMimeContentType = "";//  OPTIONAL IfcLabel;
		internal string mMimeSubtype = "";//  OPTIONAL IfcLabel;
		public string FileExtension { get { return mFileExtension; } set { mFileExtension = value; } }
		public string MimeContentType { get { return mMimeContentType; } set { mMimeContentType = value; } }
		public string MimeSubtype { get { return mMimeSubtype; } set { mMimeSubtype = value; } }
		internal IfcDocumentElectronicFormat() : base() { }
		public IfcDocumentElectronicFormat(DatabaseIfc db) : base(db) { }
		public IfcDocumentElectronicFormat(DatabaseIfc db, IfcDocumentElectronicFormat f, DuplicateOptions options)
			:base(db, f)
		{
			FileExtension = f.FileExtension;
			MimeContentType = f.MimeContentType;
			MimeSubtype = f.MimeSubtype;
		}
	}
	[Serializable]
	public partial class IfcDocumentInformation : IfcExternalInformation, IfcDocumentSelect, NamedObjectIfc
	{
		internal string mIdentification;// : IfcIdentifier;
		internal string mName;// :  IfcLabel;
		internal string mDescription = "";// : OPTIONAL IfcText;
		internal SET<IfcDocumentReference> mDocumentReferences = new SET<IfcDocumentReference>(); // ifc2x3 : OPTIONAL SET [1:?] OF IfcDocumentReference;
		internal string mLocation = "";// : IFC4	OPTIONAL IfcURIReference;
		internal string mPurpose = "", mIntendedUse = "", mScope = "";// : OPTIONAL IfcText;
		internal string mRevision = "";// : OPTIONAL IfcLabel;
		internal IfcActorSelect mDocumentOwner;// : OPTIONAL IfcActorSelect;
		internal SET<IfcActorSelect> mEditors = new SET<IfcActorSelect>();// : OPTIONAL SET [1:?] OF IfcActorSelect;
		internal DateTime mCreationTime = DateTime.MinValue, mLastRevisionTime = DateTime.MinValue;// : OPTIONAL IFC4 IfcDateTime;
		internal IfcDateAndTime mSSCreationTime = null, mSSLastRevisionTime = null;
		internal string mElectronicFormat = "";// IFC4	 :	OPTIONAL IfcIdentifier; IFC4
		internal IfcDocumentElectronicFormat mSSElectronicFormat;// IFC2x3 : OPTIONAL IfcDocumentElectronicFormat;
		internal DateTime mValidFrom = DateTime.MinValue, mValidUntil = DateTime.MinValue;// : OPTIONAL Ifc2x3 IfcCalendarDate; IFC4 IfcDate
		internal IfcCalendarDate mSSValidFrom = null, mSSValidUntil = null;
		internal IfcDocumentConfidentialityEnum mConfidentiality = IfcDocumentConfidentialityEnum.NOTDEFINED;// : OPTIONAL IfcDocumentConfidentialityEnum;
		internal IfcDocumentStatusEnum mStatus = IfcDocumentStatusEnum.NOTDEFINED;// : OPTIONAL IfcDocumentStatusEnum; 
		//INVERSE
		internal SET<IfcRelAssociatesDocument> mDocumentInfoForObjects = new SET<IfcRelAssociatesDocument>();//	 :	SET OF IfcRelAssociatesDocument FOR RelatingDocument;
		internal SET<IfcDocumentReference> mHasDocumentReferences = new SET<IfcDocumentReference>();//	 :	SET OF IfcDocumentReference FOR ReferencedDocument;
		internal SET<IfcDocumentInformationRelationship> mIsPointedTo = new SET<IfcDocumentInformationRelationship>();//	 :	SET OF IfcDocumentInformationRelationship FOR RelatedDocuments;
		internal SET<IfcDocumentInformationRelationship> mIsPointer = new SET<IfcDocumentInformationRelationship>();//	 :	SET [0:1] OF IfcDocumentInformationRelationship FOR RelatingDocument;

		public string Identification { get { return mIdentification; } set { mIdentification = value; } }
		public string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		[Obsolete("DEPRECATED IFC4", false)]
		public SET<IfcDocumentReference> DocumentReferences { get { return mDocumentReferences; } }
		public string Location { get { return mLocation; } set { mLocation = value; } }
		public string Purpose { get { return mPurpose; } set { mPurpose = value; } }
		public string IntendedUse { get { return mIntendedUse; } set { mIntendedUse = value; } }
		public string Scope { get { return mScope; } set { mScope = value; } }
		public string Revision { get { return mRevision; } set { mRevision = value; } }
		public IfcActorSelect DocumentOwner { get { return mDocumentOwner; } set { mDocumentOwner = value; } }
		public SET<IfcActorSelect> Editors { get { return mEditors; } }
		public DateTime CreationTime { get { return mCreationTime; } set { mCreationTime = value; } }
		public DateTime LastRevisionTime { get { return mLastRevisionTime; } set { mLastRevisionTime = value; } } 
		public string ElectronicFormat { get { return mElectronicFormat; } set { mElectronicFormat = value; } }
		public DateTime ValidFrom { get { return mValidFrom; } set { mValidFrom = value; } }
		public DateTime ValidUntil { get { return mValidUntil; } set { mValidUntil = value; } }
		public IfcDocumentConfidentialityEnum Confidentiality { get { return mConfidentiality; } set { mConfidentiality = value; } } 
		public IfcDocumentStatusEnum Status { get { return mStatus; } set { mStatus = value; } }

		public SET<IfcRelAssociatesDocument> DocumentInfoForObjects { get { return mDocumentInfoForObjects; } }
		public SET<IfcDocumentReference> HasDocumentReferences { get { return mHasDocumentReferences; } }
		public SET<IfcDocumentInformationRelationship> IsPointedTo { get { return mIsPointedTo; } }
		public SET<IfcDocumentInformationRelationship> IsPointer { get { return mIsPointer; } }

		public SET<IfcRelAssociatesDocument> DocumentForObjects { get { return mDocumentInfoForObjects; } }

		internal IfcDocumentInformation() : base() { }
		internal IfcDocumentInformation(DatabaseIfc db, IfcDocumentInformation i) : base(db, i)
		{
			mIdentification = i.mIdentification;
			mName = i.mName;
			mDescription = i.mDescription;
			DocumentReferences.AddRange(i.DocumentReferences.Select(x=> db.Factory.Duplicate(x)));
			mPurpose = i.mPurpose;
			mIntendedUse = i.mIntendedUse;
			mScope = i.mScope;
			mRevision = i.mRevision;
			mDocumentOwner = i.mDocumentOwner;
			Editors.AddRange(i.Editors.Select(x=> db.Factory.Duplicate(x) as IfcActorSelect));
			mCreationTime = i.mCreationTime;
			mLastRevisionTime = i.mLastRevisionTime;
			mElectronicFormat = i.mElectronicFormat;
			if(i.mSSElectronicFormat != null)
				mSSElectronicFormat = db.Factory.Duplicate(i.mSSElectronicFormat);
			ValidFrom = i.ValidFrom;
			ValidUntil = i.ValidUntil;
			mConfidentiality = i.mConfidentiality;
			mStatus = i.mStatus;
		}
		public IfcDocumentInformation(DatabaseIfc db, string identification, string name) : base(db)
		{
			Identification = identification;
			Name = name;
		}
	}
	[Serializable]
	public partial class IfcDocumentInformationRelationship : IfcResourceLevelRelationship
	{
		private IfcDocumentInformation mRelatingDocument; //: IfcDocumentInformation
		private SET<IfcDocumentInformation> mRelatedDocuments = new SET<IfcDocumentInformation>();// : SET [1:?] OF IfcDocumentInformation;
		private string mRelationshipType = "";// : OPTIONAL IfcLabel;
		public IfcDocumentInformation RelatingDocument { get { return mRelatingDocument; } set { mRelatingDocument = value; } }
		public SET<IfcDocumentInformation> RelatedDocuments { get { return mRelatedDocuments; } }
		public string RelationshipType { get { return mRelationshipType; } set { mRelationshipType = value; } }
		internal IfcDocumentInformationRelationship() : base() { }
	}
	[Serializable]
	public partial class IfcDocumentReference : IfcExternalReference, IfcDocumentSelect
	{
		internal string mDescription = "";// IFC4	 :	OPTIONAL IfcText;
		internal IfcDocumentInformation mReferencedDocument = null;// IFC	 :	OPTIONAL IfcDocumentInformation;
		//INVERSE
		internal SET<IfcRelAssociatesDocument> mDocumentRefForObjects = new SET<IfcRelAssociatesDocument>();//	 :	SET OF IfcRelAssociatesDocument FOR RelatingDocument;

		public string Description { get { return mDescription; } set { mDescription = value; } }
		public IfcDocumentInformation ReferencedDocument { get { return mReferencedDocument; } set { mReferencedDocument = value; } }
		public SET<IfcRelAssociatesDocument> DocumentRefForObjects { get { return mDocumentRefForObjects; } }

		public SET<IfcRelAssociatesDocument> DocumentForObjects { get { return mDocumentRefForObjects; } }

		internal IfcDocumentReference() : base() { }
		internal IfcDocumentReference(DatabaseIfc db, IfcDocumentReference r, DuplicateOptions options) : base(db, r, options) 
		{
			mDescription = r.mDescription; 
			if(r.mReferencedDocument != null)
				ReferencedDocument = db.Factory.Duplicate(r.ReferencedDocument, options); 
		}
		public IfcDocumentReference(DatabaseIfc db) : base(db) { }

		internal void associate(IfcDefinitionSelect d) { if (mDocumentRefForObjects.Count == 0) { new IfcRelAssociatesDocument(this); } mDocumentRefForObjects.First().RelatedObjects.Add(d); }
	}
	public interface IfcDocumentSelect : NamedObjectIfc //IFC4 SELECT (	IfcDocumentReference, IfcDocumentInformation);
	{
		SET<IfcRelAssociatesDocument> DocumentForObjects { get; }
	}
	[Serializable]
	public partial class IfcDoor : IfcBuiltElement
	{
		internal double mOverallHeight = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mOverallWidth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		private IfcDoorTypeEnum mPredefinedType = IfcDoorTypeEnum.NOTDEFINED;//: OPTIONAL IfcDoorTypeEnum; //IFC4 
		internal IfcDoorTypeOperationEnum mOperationType = IfcDoorTypeOperationEnum.NOTDEFINED;// : OPTIONAL IfcDoorTypeOperationEnum; //IFC4
		internal string mUserDefinedOperationType = "";//	 :	OPTIONAL IfcLabel;

		public double OverallHeight { get { return mOverallHeight; } set { mOverallHeight = (value > 0 ? value : double.NaN); } }
		public double OverallWidth { get { return mOverallWidth; } set { mOverallWidth = (value > 0 ? value : double.NaN); } }
		public IfcDoorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDoorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public IfcDoorTypeOperationEnum OperationType { get { return mOperationType; } set { mOperationType = value; } }
		public string UserDefinedOperationType { get { return mUserDefinedOperationType; } set { mUserDefinedOperationType = value; } }

		internal IfcDoor() : base() { }
		internal IfcDoor(DatabaseIfc db, IfcDoor d, DuplicateOptions options) : base(db, d, options) { mOverallHeight = d.mOverallHeight; mOverallWidth = d.mOverallWidth; PredefinedType = d.PredefinedType; mOperationType = d.mOperationType; mUserDefinedOperationType = d.mUserDefinedOperationType; }
		public IfcDoor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }

		internal static IfcDoorTypeOperationEnum ParseDoorTypeOperation(string constant)
		{
			IfcDoorTypeOperationEnum result = IfcDoorTypeOperationEnum.NOTDEFINED;
			if (Enum.TryParse(constant, out result))
				return result;
			if (Enum.TryParse(constant.Replace("PANEL", "DOOR"), out result))
				return result;
			return IfcDoorTypeOperationEnum.NOTDEFINED;
		}
		internal static string SerializeDoorTypeOperation(IfcDoorTypeOperationEnum operation, ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4X3_RC3)
				return operation.ToString().Replace("PANEL", "DOOR");
			return operation.ToString();
		}
	}
	[Serializable]
	public partial class IfcDoorLiningProperties : IfcPreDefinedPropertySet // IFC2x3 IfcPropertySetDefinition
	{
		internal double mLiningDepth, mLiningThickness, mThresholdDepth, mThresholdThickness, mTransomThickness;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mTransomOffset = double.NaN, mLiningOffset = double.NaN, mThresholdOffset = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal double mCasingThickness = double.NaN, mCasingDepth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		private IfcShapeAspect mShapeAspectStyle;// : OPTIONAL IfcShapeAspect;  // DEPRECATED IFC4
		internal double mLiningToPanelOffsetX = double.NaN, mLiningToPanelOffsetY = double.NaN;//	 :	OPTIONAL IfcLengthMeasure;  IFC4

		public IfcShapeAspect ShapeAspectStyle { get { return mShapeAspectStyle; } set { mShapeAspectStyle = value; } }
		
		internal IfcDoorLiningProperties() : base() { }
		internal IfcDoorLiningProperties(DatabaseIfc db, IfcDoorLiningProperties p, DuplicateOptions options) : base(db, p, options)
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
			if (p.mShapeAspectStyle != null)
				ShapeAspectStyle = db.Factory.Duplicate(p.ShapeAspectStyle);
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
		private IfcShapeAspect mShapeAspectStyle;// : OPTIONAL IfcShapeAspect;  // DEPRECATED IFC4

		public double PanelDepth { get { return mPanelDepth; } set { mPanelDepth = value; } }
		public IfcDoorPanelOperationEnum OperationType { get { return mOperationType; } set { mOperationType = value; } }
		public double PanelWidth { get { return mPanelWidth; } set { mPanelWidth = value; } }
		public IfcDoorPanelPositionEnum PanelPosition { get { return mPanelPosition; } set { mPanelPosition = value; } }

		internal IfcDoorPanelProperties() : base() { }
		[Obsolete("DEPRECATED IFC4", false)]
		public IfcShapeAspect ShapeAspectStyle { get { return mShapeAspectStyle; } set { mShapeAspectStyle = value; } }
		internal IfcDoorPanelProperties(DatabaseIfc db, IfcDoorPanelProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mPanelDepth = p.mPanelDepth;
			mOperationType = p.mOperationType;
			mPanelWidth = p.mPanelWidth;
			mPanelPosition = p.mPanelPosition;
			if (p.mShapeAspectStyle != null)
				ShapeAspectStyle = db.Factory.Duplicate(p.ShapeAspectStyle);
		}
	}
	[Serializable]
	public partial class IfcDoorStandardCase : IfcDoor
	{
		public override string StepClassName { get { return "IfcDoor"; } }
		internal IfcDoorStandardCase() : base() { }
		internal IfcDoorStandardCase(DatabaseIfc db, IfcDoorStandardCase d, DuplicateOptions options) : base(db, d, options) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcDoorStyle : IfcTypeProduct //IFC2x3 
	{
		internal IfcDoorTypeOperationEnum mOperationType = IfcDoorTypeOperationEnum.NOTDEFINED;// : IfcDoorStyleOperationEnum;
		internal IfcDoorStyleConstructionEnum mConstructionType = IfcDoorStyleConstructionEnum.NOTDEFINED;// : IfcDoorStyleConstructionEnum; //IFC2x3
		internal bool mParameterTakesPrecedence = false;// : BOOLEAN; 
		internal bool mSizeable = false;// : BOOLEAN;  //IFC2x3

		public IfcDoorTypeOperationEnum OperationType { get { return mOperationType; } set { mOperationType = value; } }
		public IfcDoorStyleConstructionEnum ConstructionType { get { return mConstructionType; } set { mConstructionType = value; } }

		internal IfcDoorStyle() : base() { }
		internal IfcDoorStyle(DatabaseIfc db, IfcDoorStyle s, DuplicateOptions options) : base(db, s, options) { mOperationType = s.mOperationType; mConstructionType = s.mConstructionType; mParameterTakesPrecedence = s.mParameterTakesPrecedence; mSizeable = s.mSizeable; }
	}
	[Serializable]
	public partial class IfcDoorType : IfcBuiltElementType //IFC2x3 IfcDoorStyle
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcDoorStyle" : base.StepClassName); } }

		private IfcDoorTypeEnum mPredefinedType = IfcDoorTypeEnum.NOTDEFINED;
		internal IfcDoorTypeOperationEnum mOperationType;// : IfcDoorStyleOperationEnum; 
		internal bool mParameterTakesPrecedence = false;// : BOOLEAN;  
		internal string mUserDefinedOperationType = "";//	 :	OPTIONAL IfcLabel;

		public IfcDoorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDoorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public string UserDefinedOperationType { get { return mUserDefinedOperationType; } set { mUserDefinedOperationType = value; } }

		internal IfcDoorType() : base() { }
		internal IfcDoorType(DatabaseIfc db, IfcDoorType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; mOperationType = t.mOperationType; mParameterTakesPrecedence = t.mParameterTakesPrecedence; mUserDefinedOperationType = t.mUserDefinedOperationType; }
		public IfcDoorType(DatabaseIfc db, string name, IfcDoorTypeEnum type) : base(db, name) { mPredefinedType = type; }
		internal IfcDoorType(DatabaseIfc db, string name, IfcDoorTypeEnum type, IfcDoorTypeOperationEnum operation, IfcDoorLiningProperties lp, List<IfcDoorPanelProperties> pps)
			: this(db, name, type)
		{
			if (lp != null) mHasPropertySets.Add(lp);
			if (pps != null && pps.Count > 0) mHasPropertySets.AddRange(pps);
			mOperationType = operation;
			mParameterTakesPrecedence = true;
			 
		}
		internal IfcDoorType(DatabaseIfc db, string name, IfcDoorTypeEnum type, IfcDoorTypeOperationEnum operation, bool parameterTakesPrecendence)
			: this(db, name, type)
		{
			mOperationType = operation;
			mParameterTakesPrecedence = parameterTakesPrecendence;
		}
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcDraughtingCallout : IfcGeometricRepresentationItem // DEPRECATED IFC4 SUPERTYPE OF (ONEOF (IfcDimensionCurveDirectedCallout ,IfcStructuredDimensionCallout))
	{
		internal SET<IfcDraughtingCalloutElement> mContents = new SET<IfcDraughtingCalloutElement>(); //: SET [1:?] OF IfcDraughtingCalloutElement 
		public SET<IfcDraughtingCalloutElement> Contents { get { return mContents; } }
		internal IfcDraughtingCallout() : base() { }
		internal IfcDraughtingCallout(DatabaseIfc db, IfcDraughtingCallout el, DuplicateOptions options) : base(db, el, options) 
		{
			mContents.AddRange(el.mContents.Select(x => db.Factory.Duplicate(x)));
		}
		public IfcDraughtingCallout(IfcDraughtingCalloutElement content) : base(content.Database) { mContents.Add(content); }
		public IfcDraughtingCallout(IEnumerable<IfcDraughtingCalloutElement> contents) : base(contents.First().Database) {  }
	} 
	public interface IfcDraughtingCalloutElement : IBaseClassIfc { } //SELECT (IfcAnnotationCurveOccurrence ,IfcAnnotationTextOccurrence ,IfcAnnotationSymbolOccurrence);
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcDraughtingCalloutRelationship : BaseClassIfc, NamedObjectIfc
	{
		internal string mName = "";// : OPTIONAL IfcLabel;
		internal string mDescription = "";// : OPTIONAL IfcText;
		internal IfcDraughtingCallout mRelatingDraughtingCallout = null;// : IfcDraughtingCallout;
		internal IfcDraughtingCallout mRelatedDraughtingCallout = null;// : IfcDraughtingCallout;

		public string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		internal IfcDraughtingCalloutRelationship() : base() { }

		public IfcDraughtingCalloutRelationship(IfcDraughtingCallout relatingDraughtingCallout, IfcDraughtingCallout relatedDraughtingCallout)
			: base(relatedDraughtingCallout.Database)
		{
			mRelatingDraughtingCallout = relatingDraughtingCallout;
			mRelatedDraughtingCallout = relatedDraughtingCallout;
		}
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
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcDraughtingPreDefinedTextFont : IfcPreDefinedTextFont // DEPRECATED IFC4
	{
		internal IfcDraughtingPreDefinedTextFont() : base() { }
	}
	[Serializable]
	public partial class IfcDuctFitting : IfcFlowFitting //IFC4
	{
		private IfcDuctFittingTypeEnum mPredefinedType = IfcDuctFittingTypeEnum.NOTDEFINED;// OPTIONAL : IfcDuctFittingTypeEnum;
		public IfcDuctFittingTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDuctFittingTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDuctFitting() : base() { }
		internal IfcDuctFitting(DatabaseIfc db, IfcDuctFitting f, DuplicateOptions options) : base(db, f, options) { PredefinedType = f.PredefinedType; }
		public IfcDuctFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcDuctFittingType : IfcFlowFittingType
	{
		private IfcDuctFittingTypeEnum mPredefinedType = IfcDuctFittingTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum;
		public IfcDuctFittingTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDuctFittingTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDuctFittingType() : base() { }
		internal IfcDuctFittingType(DatabaseIfc db, IfcDuctFittingType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcDuctFittingType(DatabaseIfc db, string name, IfcDuctFittingTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcDuctSegment : IfcFlowSegment //IFC4
	{
		private IfcDuctSegmentTypeEnum mPredefinedType = IfcDuctSegmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcDuctSegmentTypeEnum;
		public IfcDuctSegmentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDuctSegmentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDuctSegment() : base() { }
		internal IfcDuctSegment(DatabaseIfc db, IfcDuctSegment s, DuplicateOptions options) : base(db, s, options) { PredefinedType = s.PredefinedType; }
		public IfcDuctSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcDuctSegmentType : IfcFlowSegmentType
	{
		private IfcDuctSegmentTypeEnum mPredefinedType = IfcDuctSegmentTypeEnum.NOTDEFINED;// : IfcDuctSegmentTypeEnum;
		public IfcDuctSegmentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDuctSegmentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDuctSegmentType() : base() { }
		internal IfcDuctSegmentType(DatabaseIfc db, IfcDuctSegmentType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcDuctSegmentType(DatabaseIfc db, string name, IfcDuctSegmentTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcDuctSilencer : IfcFlowTreatmentDevice //IFC4  
	{
		private IfcDuctSilencerTypeEnum mPredefinedType = IfcDuctSilencerTypeEnum.NOTDEFINED;
		public IfcDuctSilencerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDuctSilencerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcDuctSilencer() : base() { }
		internal IfcDuctSilencer(DatabaseIfc db, IfcDuctSilencer s, DuplicateOptions options) : base(db, s, options) { PredefinedType = s.PredefinedType; }
		public IfcDuctSilencer(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcDuctSilencerType : IfcFlowTreatmentDeviceType
	{
		private IfcDuctSilencerTypeEnum mPredefinedType = IfcDuctSilencerTypeEnum.NOTDEFINED;// : IfcDuctSilencerTypeEnum;
		public IfcDuctSilencerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcDuctSilencerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		
		internal IfcDuctSilencerType() : base() { }
		internal IfcDuctSilencerType(DatabaseIfc db, IfcDuctSilencerType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcDuctSilencerType(DatabaseIfc db, string name, IfcDuctSilencerTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
}
