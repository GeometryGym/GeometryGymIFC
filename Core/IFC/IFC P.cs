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
using System.Collections.Specialized;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public abstract partial class IfcParameterizedProfileDef : IfcProfileDef //ABSTRACT SUPERTYPE OF (ONEOF (IfcAsymmetricIShapeProfileDef , IfcCShapeProfileDef ,IfcCircleProfileDef ,IfcCraneRailAShapeProfileDef ,IfcCraneRailFShapeProfileDef ,
	{//IfcEllipseProfileDef ,IfcIShapeProfileDef ,IfcLShapeProfileDef ,IfcRectangleProfileDef ,IfcTShapeProfileDef ,IfcTrapeziumProfileDef ,IfcUShapeProfileDef ,IfcZShapeProfileDef))*/
		internal IfcAxis2Placement2D mPosition = null;// : IfcAxis2Placement2D //IFC4  OPTIONAL

		public IfcAxis2Placement2D Position
		{
			get { return mPosition; }
			set
			{
				mPosition = value;
				if (value == null && mDatabase != null && mDatabase.Release <= ReleaseVersion.IFC2x3)
					mPosition = mDatabase.Factory.Origin2dPlace;
			}
		}

		protected IfcParameterizedProfileDef() : base() { }
		protected IfcParameterizedProfileDef(DatabaseIfc db, IfcParameterizedProfileDef p, DuplicateOptions options) : base(db, p, options)
		{
			if (p.mPosition != null)
				Position = db.Factory.Duplicate(p.Position) as IfcAxis2Placement2D;
		}
		protected IfcParameterizedProfileDef(DatabaseIfc m, string name) : base(m, name)
		{
			if (mDatabase != null)
			{
				if (mDatabase.mModelView == ModelView.Ifc4Reference)
					throw new Exception("Invalid Model View for IfcParameterizedProfileDef : " + m.ModelView.ToString());
				if (mDatabase.mRelease < ReleaseVersion.IFC4)
					Position = mDatabase.Factory.Origin2dPlace;
			}
		}
		
	}
	[Serializable]
	public partial class IfcPath : IfcTopologicalRepresentationItem
	{
		internal List<int> mEdgeList = new List<int>();// : SET [1:?] OF IfcOrientedEdge;
		public ReadOnlyCollection<IfcOrientedEdge> EdgeList { get { return new ReadOnlyCollection<IfcOrientedEdge>(mEdgeList.ConvertAll(x => mDatabase[x] as IfcOrientedEdge)); } }

		internal IfcPath() : base() { }
		internal IfcPath(DatabaseIfc db, IfcPath p, DuplicateOptions options) : base(db, p, options) { p.EdgeList.ToList().ForEach(x => addEdge(db.Factory.Duplicate(x) as IfcOrientedEdge)); }
		public IfcPath(IfcOrientedEdge edge) : base(edge.mDatabase) { mEdgeList.Add(edge.mIndex); }
		public IfcPath(List<IfcOrientedEdge> edges) : base(edges[0].mDatabase) { edges.ForEach(x => addEdge(x)); }
		
		internal void addEdge(IfcOrientedEdge edge) { mEdgeList.Add(edge.mIndex); }
	}
	[Serializable]
	public partial class IfcPavement : IfcBuiltElement
	{
		private IfcLogicalEnum mFlexible = IfcLogicalEnum.UNKNOWN; //: OPTIONAL IfcBoolean;
		public IfcLogicalEnum Flexible { get { return mFlexible; } set { mFlexible = value; } }

		public IfcPavement() : base() { }
		public IfcPavement(DatabaseIfc db) : base(db) { }
		public IfcPavement(DatabaseIfc db, IfcPavement pavement, DuplicateOptions options) : base(db, pavement, options) { Flexible = pavement.Flexible; }
		public IfcPavement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcPavementType : IfcBuiltElementType
	{
		private bool mFlexible = false; //: IfcBoolean;
		public bool Flexible { get { return mFlexible; } set { mFlexible = value; } }

		public IfcPavementType() : base() { }
		public IfcPavementType(DatabaseIfc db, IfcPavementType pavementType, DuplicateOptions options) : base(db, pavementType, options) { Flexible = pavementType.Flexible; }
		public IfcPavementType(DatabaseIfc db, string name, bool flexible) : base(db, name) { Flexible = flexible; }
	}
	[Serializable]
	public partial class IfcPcurve : IfcCurve, IfcCurveOnSurface
	{
		internal IfcSurface mBasisSurface;// :	IfcSurface;
		internal IfcCurve mReferenceCurve;// :	IfcCurve; 

		public IfcSurface BasisSurface { get { return mBasisSurface; } set { mBasisSurface = value; } }
		public IfcCurve ReferenceCurve { get { return mReferenceCurve; } set { mReferenceCurve = value; } }

		internal IfcPcurve() : base() { }
		internal IfcPcurve(DatabaseIfc db, IfcPcurve c, DuplicateOptions options) : base(db, c, options) { BasisSurface = db.Factory.Duplicate(c.BasisSurface) as IfcSurface; ReferenceCurve = db.Factory.Duplicate(c.ReferenceCurve) as IfcCurve; }
		public IfcPcurve(IfcSurface basisSurface, IfcCurve referenceCurve) : base(basisSurface.mDatabase) { BasisSurface = basisSurface; ReferenceCurve = referenceCurve; }
	}
	[Serializable]
	public partial class IfcPerformanceHistory : IfcControl
	{
		internal string mLifeCyclePhase;// : IfcLabel; 
		private IfcPerformanceHistoryTypeEnum mPredefinedType = IfcPerformanceHistoryTypeEnum.NOTDEFINED;// OPTIONAL : IfcPerformanceHistoryTypeEnum;
		public IfcPerformanceHistoryTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public string LifeCyclePhase { get { return mLifeCyclePhase; } set { mLifeCyclePhase = value; } }// : IfcLabel; 
		internal IfcPerformanceHistory() : base() { }
		internal IfcPerformanceHistory(DatabaseIfc db, IfcPerformanceHistory h, DuplicateOptions options) : base(db, h, options) { mLifeCyclePhase = h.mLifeCyclePhase; }
	}
	[Serializable]
	public partial class IfcPermeableCoveringProperties : IfcPreDefinedPropertySet
	{
		private IfcPermeableCoveringOperationEnum mOperationType = IfcPermeableCoveringOperationEnum.NOTDEFINED; //: IfcPermeableCoveringOperationEnum;
		private IfcWindowPanelPositionEnum mPanelPosition = IfcWindowPanelPositionEnum.NOTDEFINED; //: IfcWindowPanelPositionEnum;
		private double mFrameDepth = double.NaN; //: OPTIONAL IfcPositiveLengthMeasure;
		private double mFrameThickness = double.NaN; //: OPTIONAL IfcPositiveLengthMeasure;
		private IfcShapeAspect mShapeAspectStyle = null; //: OPTIONAL IfcShapeAspect;

		public IfcPermeableCoveringOperationEnum OperationType { get { return mOperationType; } set { mOperationType = value; } }
		public IfcWindowPanelPositionEnum PanelPosition { get { return mPanelPosition; } set { mPanelPosition = value; } }
		public double FrameDepth { get { return mFrameDepth; } set { mFrameDepth = value; } }
		public double FrameThickness { get { return mFrameThickness; } set { mFrameThickness = value; } }
		public IfcShapeAspect ShapeAspectStyle { get { return mShapeAspectStyle; } set { mShapeAspectStyle = value; } }

		public IfcPermeableCoveringProperties() : base() { }
		public IfcPermeableCoveringProperties(DatabaseIfc db, string name, IfcPermeableCoveringOperationEnum operationType, IfcWindowPanelPositionEnum panelPosition)
			: base(db, name)
		{
			OperationType = operationType;
			PanelPosition = panelPosition;
		}
	}
	[Serializable]
	public partial class IfcPermit : IfcControl
	{
		internal string mPermitID;// : IfcIdentifier; 
		internal IfcPermit() : base() { }
		internal IfcPermit(DatabaseIfc db, IfcPermit p, DuplicateOptions options) : base(db, p, options) { mPermitID = p.mPermitID; }
	}
	[Serializable]
	public partial class IfcPerson : BaseClassIfc, IfcActorSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect, NamedObjectIfc
	{
		private string mIdentification = "$";// : OPTIONAL IfcIdentifier;
		private string mFamilyName = "$", mGivenName = "$";// : OPTIONAL IfcLabel;
		private List<string> mMiddleNames = new List<string>(), mPrefixTitles = new List<string>(), mSuffixTitles = new List<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		private LIST<IfcActorRole> mRoles = new LIST<IfcActorRole>();// : OPTIONAL LIST [1:?] OF IfcActorRole;
		private LIST<IfcAddress> mAddresses = new LIST<IfcAddress>();//: OPTIONAL LIST [1:?] OF IfcAddress; 
																	 //INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string FamilyName { get { return (mFamilyName == "$" ? "" : ParserIfc.Decode(mFamilyName)); } set { mFamilyName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string GivenName { get { return (mGivenName == "$" ? "" : ParserIfc.Decode(mGivenName)); } set { mGivenName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<string> MiddleNames { get { return new ReadOnlyCollection<string>(mMiddleNames.ConvertAll(x => ParserIfc.Decode(x))); } }
		public ReadOnlyCollection<string> PrefixTitles { get { return new ReadOnlyCollection<string>(mPrefixTitles.ConvertAll(x => ParserIfc.Decode(x))); } }
		public ReadOnlyCollection<string> SuffixTitles { get { return new ReadOnlyCollection<string>(mSuffixTitles.ConvertAll(x => ParserIfc.Decode(x))); } }
		public LIST<IfcActorRole> Roles { get { return mRoles; } }
		public LIST<IfcAddress> Addresses { get { return mAddresses; } }
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } set { mHasExternalReference.Clear();  if (value != null) { mHasExternalReference.CollectionChanged -= mHasExternalReference_CollectionChanged; mHasExternalReference = value; mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged; } } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>( mHasConstraintRelationships); } }

		public string Name { get { return (string.IsNullOrEmpty(GivenName) ? (string.IsNullOrEmpty(FamilyName) ? Identification : FamilyName) : GivenName + (string.IsNullOrEmpty(FamilyName) ? "" : " " + FamilyName)); } }

		internal IfcPerson() : base() { }
		public IfcPerson(DatabaseIfc db) : base(db) { }
		internal IfcPerson(DatabaseIfc db, IfcPerson p) : base(db, p)
		{
			mIdentification = p.mIdentification;
			mFamilyName = p.mFamilyName;
			mGivenName = p.mGivenName;
			mMiddleNames.AddRange(p.mMiddleNames);
			mPrefixTitles.AddRange(p.mPrefixTitles);
			mSuffixTitles.AddRange(p.mSuffixTitles);
			Roles.AddRange(p.Roles.ConvertAll(x => db.Factory.Duplicate(x) as IfcActorRole));
			Addresses.AddRange(p.Addresses.ConvertAll(x => db.Factory.Duplicate(x) as IfcAddress));
		}
		internal IfcPerson(DatabaseIfc m, string id, string familyname, string givenName) : base(m)
		{
			Identification = id;
			FamilyName = familyname;
			GivenName = givenName;
		}
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged;
		}
		private void mHasExternalReference_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.NewItems)
				{
					if (!r.RelatedResourceObjects.Contains(this))
						r.RelatedResourceObjects.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.OldItems)
					r.RelatedResourceObjects.Remove(this);
			}
		}
		public void AddMiddleName(string name) { if (!string.IsNullOrEmpty(name)) mMiddleNames.Add(ParserIfc.Encode(name)); }
		public void AddPrefixTitle(string title) { if (!string.IsNullOrEmpty(title)) mPrefixTitles.Add(ParserIfc.Encode(title)); }
		public void AddSuffixTitle(string title) { if (!string.IsNullOrEmpty(title)) mSuffixTitles.Add(ParserIfc.Encode(title)); }
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	[Serializable]
	public partial class IfcPersonAndOrganization : BaseClassIfc, IfcActorSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect, NamedObjectIfc
	{
		private IfcPerson mThePerson = null;// : IfcPerson;
		private IfcOrganization mTheOrganization = null;// : IfcOrganization;
		private LIST<IfcActorRole> mRoles = new LIST<IfcActorRole>();// : OPTIONAL LIST [1:?] OF IfcActorRole;
																	 //INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public IfcPerson ThePerson { get { return mThePerson; } set { mThePerson = value; } }
		public IfcOrganization TheOrganization { get { return mTheOrganization; } set { mTheOrganization = value; } }
		public LIST<IfcActorRole> Roles { get { return mRoles; } set { mRoles = value; } }

		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } set { mHasExternalReference.Clear();  if (value != null) { mHasExternalReference.CollectionChanged -= mHasExternalReference_CollectionChanged; mHasExternalReference = value; mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged; } } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>(mHasConstraintRelationships); } }

		public string Name { get { return TheOrganization.Name + " " + ThePerson.Name; } }

		internal IfcPersonAndOrganization() : base() { }
		internal IfcPersonAndOrganization(DatabaseIfc db) : base(db) { }
		internal IfcPersonAndOrganization(DatabaseIfc db, IfcPersonAndOrganization p) : base(db, p) { ThePerson = db.Factory.Duplicate(p.ThePerson) as IfcPerson; TheOrganization = db.Factory.Duplicate(p.TheOrganization) as IfcOrganization; Roles.AddRange(p.Roles.ConvertAll(x=> db.Factory.Duplicate(x) as IfcActorRole)); }
		public IfcPersonAndOrganization(IfcPerson person, IfcOrganization organization) : base(person.mDatabase) { ThePerson = person; TheOrganization = organization; }
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged;
		}
		private void mHasExternalReference_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.NewItems)
				{
					if (!r.RelatedResourceObjects.Contains(this))
						r.RelatedResourceObjects.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.OldItems)
					r.RelatedResourceObjects.Remove(this);
			}
		}
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	[Serializable]
	public partial class IfcPhysicalComplexQuantity : IfcPhysicalQuantity
	{
		private SET<IfcPhysicalQuantity> mHasQuantities = new SET<IfcPhysicalQuantity>(); //: SET[1:?] OF IfcPhysicalQuantity;
		private string mDiscrimination = ""; //: IfcLabel;
		private string mQuality = ""; //: OPTIONAL IfcLabel;
		private string mUsage = ""; //: OPTIONAL IfcLabel;

		public SET<IfcPhysicalQuantity> HasQuantities { get { return mHasQuantities; } set { mHasQuantities = value; } }
		public string Discrimination { get { return mDiscrimination; } set { mDiscrimination = value; } }
		public string Quality { get { return mQuality; } set { mQuality = value; } }
		public string Usage { get { return mUsage; } set { mUsage = value; } }

		public IfcPhysicalComplexQuantity() : base() { }
		public IfcPhysicalComplexQuantity(string name, IEnumerable<IfcPhysicalQuantity> hasQuantities, string discrimination)
			: base(hasQuantities.First().Database, name)
		{
			HasQuantities.AddRange(hasQuantities);
			Discrimination = discrimination;
		}
	}
	[Serializable]
	public abstract partial class IfcPhysicalQuantity : BaseClassIfc, IfcResourceObjectSelect, NamedObjectIfc  //ABSTRACT SUPERTYPE OF(ONEOF(IfcPhysicalComplexQuantity, IfcPhysicalSimpleQuantity));
	{
		internal string mName = "NoName";// : IfcLabel;
		internal string mDescription = "$"; // : OPTIONAL IfcText;
											//INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		//PartOfComplex : SET[0:1] OF IfcPhysicalComplexQuantity FOR HasQuantities;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public string Name
		{
			get { return ParserIfc.Decode(mName); }
			set { mName = (string.IsNullOrEmpty(value) ? "NoName" : ParserIfc.Encode(value)); }
		}
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } set { mHasExternalReference.Clear();  if (value != null) { mHasExternalReference.CollectionChanged -= mHasExternalReference_CollectionChanged; mHasExternalReference = value; mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged; } } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		protected IfcPhysicalQuantity() : base() { }
		protected IfcPhysicalQuantity(DatabaseIfc db, IfcPhysicalQuantity q) : base(db, q) { mName = q.mName; mDescription = q.mDescription; }
		protected IfcPhysicalQuantity(DatabaseIfc db, string name) : base(db) { Name = name; }
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged;
		}
		private void mHasExternalReference_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.NewItems)
				{
					if (!r.RelatedResourceObjects.Contains(this))
						r.RelatedResourceObjects.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.OldItems)
					r.RelatedResourceObjects.Remove(this);
			}
		}
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	[Serializable]
	public abstract partial class IfcPhysicalSimpleQuantity : IfcPhysicalQuantity //ABSTRACT SUPERTYPE OF (ONEOF (IfcQuantityArea ,IfcQuantityCount ,IfcQuantityLength ,IfcQuantityTime ,IfcQuantityVolume ,IfcQuantityWeight))
	{
		internal int mUnit = 0;// : OPTIONAL IfcNamedUnit;	
		public IfcNamedUnit Unit { get { return mDatabase[mUnit] as IfcNamedUnit; } set { mUnit = (value == null ? 0 : value.mIndex); } }

		protected IfcPhysicalSimpleQuantity() : base() { }
		protected IfcPhysicalSimpleQuantity(DatabaseIfc db, IfcPhysicalSimpleQuantity q) : base(db, q) { if (q.mUnit > 0) Unit = db.Factory.Duplicate(q.Unit) as IfcNamedUnit; }
		protected IfcPhysicalSimpleQuantity(DatabaseIfc db, string name) : base(db, name) { }

		public abstract IfcMeasureValue MeasureValue { get; }
	}
	[Serializable]
	public partial class IfcPile : IfcDeepFoundation
	{
		internal IfcPileTypeEnum mPredefinedType = IfcPileTypeEnum.NOTDEFINED;// OPTIONAL : IfcPileTypeEnum;
		internal IfcPileConstructionEnum mConstructionType = IfcPileConstructionEnum.NOTDEFINED;// : OPTIONAL IfcPileConstructionEnum; IFC4 	Deprecated.

		public IfcPileTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		[Obsolete("DEPRECATED IFC4", false)]
		public IfcPileConstructionEnum ConstructionType { get { return mConstructionType; } set { mConstructionType = value; } }

		internal IfcPile() : base() { }
		internal IfcPile(DatabaseIfc db, IfcPile p, DuplicateOptions options) : base(db, p, options) { mPredefinedType = p.mPredefinedType; mConstructionType = p.mConstructionType; }
		public IfcPile(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcPileType : IfcDeepFoundationType
	{
		internal IfcPileTypeEnum mPredefinedType = IfcPileTypeEnum.NOTDEFINED;
		public IfcPileTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPileType() : base() { }
		internal IfcPileType(DatabaseIfc db, IfcPileType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcPileType(DatabaseIfc db, string name, IfcPileTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
		public IfcPileType(string name, IfcMaterialProfileSet mps, IfcPileTypeEnum type) : base(mps.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = mps; }
	}
	[Serializable]
	public partial class IfcPipeFitting : IfcFlowFitting //IFC4
	{
		internal IfcPipeFittingTypeEnum mPredefinedType = IfcPipeFittingTypeEnum.NOTDEFINED;    // :	OPTIONAL IfcPipeFittingTypeEnum;
		public IfcPipeFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeFitting() : base() { }
		internal IfcPipeFitting(DatabaseIfc db, IfcPipeFitting f, DuplicateOptions options) : base(db, f, options) { mPredefinedType = f.mPredefinedType; }
		public IfcPipeFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcPipeFittingType : IfcFlowFittingType
	{
		internal IfcPipeFittingTypeEnum mPredefinedType = IfcPipeFittingTypeEnum.NOTDEFINED;// : IfcPipeFittingTypeEnum; 
		public IfcPipeFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeFittingType() : base() { }
		internal IfcPipeFittingType(DatabaseIfc db, IfcPipeFittingType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcPipeFittingType(DatabaseIfc db, string name, IfcPipeFittingTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
		public IfcPipeFittingType(DatabaseIfc db, string name, double radius, double bendAngle) : base(db)
		{
			Name = name;
			Pset_PipeFittingTypeBend pset = new Pset_PipeFittingTypeBend(this) { BendRadius = radius, BendAngle = bendAngle };
			mPredefinedType = IfcPipeFittingTypeEnum.BEND;
		}
	}
	[Serializable]
	public partial class IfcPipeSegment : IfcFlowSegment //IFC4
	{
		internal IfcPipeSegmentTypeEnum mPredefinedType = IfcPipeSegmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcPipeSegmentTypeEnum;
		public IfcPipeSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeSegment() : base() { }
		internal IfcPipeSegment(DatabaseIfc db, IfcPipeSegment s, DuplicateOptions options) : base(db, s, options) { mPredefinedType = s.mPredefinedType; }
		public IfcPipeSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcPipeSegmentType : IfcFlowSegmentType
	{
		internal IfcPipeSegmentTypeEnum mPredefinedType = IfcPipeSegmentTypeEnum.NOTDEFINED;// : IfcPipeSegmentTypeEnum; 
		public IfcPipeSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeSegmentType() : base() { }
		internal IfcPipeSegmentType(DatabaseIfc db, IfcPipeSegmentType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcPipeSegmentType(DatabaseIfc m, string name, IfcPipeSegmentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcPixelTexture : IfcSurfaceTexture
	{
		internal int mWidth;// : IfcInteger;
		internal int mHeight;// : IfcInteger;
		internal int mColourComponents;// : IfcInteger;
		internal List<string> mPixel = new List<string>();// : LIST[1:?] OF IfcBinary;

		public int Width { get { return mWidth; } set { mWidth = value; } }
		public int Height { get { return mHeight; } set { mHeight = value; } }
		public int ColourComponents { get { return mColourComponents; } set { mColourComponents = value; } }

		internal IfcPixelTexture() : base() { }
		internal IfcPixelTexture(DatabaseIfc db, IfcPixelTexture t) : base(db, t) { mWidth = t.mWidth; mHeight = t.mHeight; mColourComponents = t.mColourComponents; mPixel.AddRange(t.mPixel); }
		public IfcPixelTexture(DatabaseIfc db, bool repeatS, bool repeatT, int width, int height, int colourComponents, List<string> pixel) : base(db, repeatS, repeatT) { mWidth = width; mHeight = height; mColourComponents = colourComponents; mPixel = pixel; }
	}
	[Serializable]
	public abstract partial class IfcPlacement : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcAxis1Placement ,IfcAxis2Placement2D ,IfcAxis2Placement3D))*/
	{
		protected IfcPoint mLocation;// : IfcCartesianPoint;  IfcPoint IFC4x3

		protected IfcPlacement() : base() { }
		protected IfcPlacement(DatabaseIfc db) : base(db) { }
		protected IfcPlacement(IfcPoint location) : base(location.Database) { mLocation = location; }
		protected IfcPlacement(DatabaseIfc db, IfcPlacement p, DuplicateOptions options) : base(db, p, options) { mLocation = db.Factory.Duplicate(p.mLocation) as IfcPoint; }

		public virtual bool IsXYPlane(double tol) { return mLocation.isOrigin(tol); } 
	}
	[Serializable]
	public partial class IfcPlanarBox : IfcPlanarExtent
	{
		internal int mPlacement;// : IfcAxis2Placement; 
		public IfcAxis2Placement Placement { get { return mDatabase[mPlacement] as IfcAxis2Placement; } set { mPlacement = value.Index; } }

		internal IfcPlanarBox() : base() { }
		internal IfcPlanarBox(DatabaseIfc db, IfcPlanarBox b, DuplicateOptions options) : base(db, b, options) { Placement = db.Factory.Duplicate(b.mDatabase[b.mPlacement]) as IfcAxis2Placement; }
	}
	[Serializable]
	public partial class IfcPlanarExtent : IfcGeometricRepresentationItem
	{
		internal double mSizeInX;// : IfcLengthMeasure;
		internal double mSizeInY;// : IfcLengthMeasure; 
		
		public double SizeInX { get { return mSizeInX; } set { mSizeInX = value; } }
		public double SizeInY { get { return mSizeInY; } set { mSizeInY = value; } }
		internal IfcPlanarExtent() : base() { }
		internal IfcPlanarExtent(DatabaseIfc db, IfcPlanarExtent p, DuplicateOptions options) : base(db, p, options) { mSizeInX = p.mSizeInX; mSizeInY = p.mSizeInY; }
	}
	[Serializable]
	public partial class IfcPlane : IfcElementarySurface
	{
		internal IfcPlane() : base() { }
		internal IfcPlane(DatabaseIfc db, IfcPlane p, DuplicateOptions options) : base(db, p, options) { }
		public IfcPlane(IfcAxis2Placement3D placement) : base(placement) { }
	}
	[Serializable]
	public partial class IfcPlant : IfcGeographicElement
	{
		public IfcPlant() : base() { }
		public IfcPlant(DatabaseIfc db) : base(db) { }
		public IfcPlant(DatabaseIfc db, IfcPlant plant, DuplicateOptions options) : base(db, plant, options) { PredefinedType = plant.PredefinedType; }
		public IfcPlant(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcPlate : IfcBuiltElement
	{
		internal IfcPlateTypeEnum mPredefinedType = IfcPlateTypeEnum.NOTDEFINED;//: OPTIONAL IfcPlateTypeEnum;
		public IfcPlateTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPlate() : base() { }
		internal IfcPlate(DatabaseIfc db, IfcPlate p, DuplicateOptions options) : base(db, p, options) { mPredefinedType = p.mPredefinedType; }
		public IfcPlate(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		public IfcPlate(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public partial class IfcPlateStandardCase : IfcPlate //IFC4
	{
		public override string StepClassName { get { return "IfcPlate"; } }
		internal IfcPlateStandardCase() : base() { }
		internal IfcPlateStandardCase(DatabaseIfc db, IfcPlateStandardCase p, DuplicateOptions options) : base(db, p, options) { }
	}
	[Serializable]
	public partial class IfcPlateType : IfcBuiltElementType
	{
		internal IfcPlateTypeEnum mPredefinedType = IfcPlateTypeEnum.NOTDEFINED;
		public IfcPlateTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPlateType() : base() { }
		internal IfcPlateType(DatabaseIfc db, IfcPlateType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcPlateType(DatabaseIfc m, string name, IfcPlateTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcPlateType(string name, IfcMaterialLayerSet mls, IfcPlateTypeEnum type) : this(mls.mDatabase, name, type) { MaterialSelect = mls; }
	}
	[Serializable]
	public abstract partial class IfcPoint : IfcGeometricRepresentationItem, IfcGeometricSetSelect, IfcPointOrVertexPoint /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCartesianPoint ,IfcPointOnCurve ,IfcPointOnSurface))*/
	{
		protected IfcPoint() : base() { }
		protected IfcPoint(DatabaseIfc db) : base(db) { }
		protected IfcPoint(DatabaseIfc db, IfcPoint p, DuplicateOptions options) : base(db, p, options) { }

		internal virtual bool isOrigin(double tol) 
		{ 
			return false; 
		} 


	}
	[Serializable]
	public partial class IfcPointByDistanceExpression : IfcPoint
	{
		internal IfcCurveMeasureSelect mDistanceAlong;// : IfcCurveMeasureSelect;
		internal double mOffsetLateral = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal double mOffsetVertical = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal double mOffsetLongitudinal = double.NaN;// : OPTIONAL IfcLengthMeasure;
		[Obsolete("DEPRECATED IFC4x3", false)]
		internal bool mAlongHorizontal = false; // IfcBoolean
		internal IfcCurve mBasisCurve = null;// : IfcCurve

		public IfcCurveMeasureSelect DistanceAlong { get { return mDistanceAlong; } set { mDistanceAlong = value; } }
		public double OffsetLateral { get { return mOffsetLateral; } set { mOffsetLateral = value; } }
		public double OffsetVertical { get { return mOffsetVertical; } set { mOffsetVertical = value; } }
		public double OffsetLongitudinal { get { return mOffsetLongitudinal; } set { mOffsetLongitudinal = value; } }
		public IfcCurve BasisCurve { get { return mBasisCurve; } set { mBasisCurve = value; } }

		internal IfcPointByDistanceExpression() : base() { }
		internal IfcPointByDistanceExpression(DatabaseIfc db, IfcPointByDistanceExpression e, DuplicateOptions options) : base(db, e, options)
		{
			DistanceAlong = e.DistanceAlong;
			OffsetLateral = e.OffsetLateral;
			OffsetVertical = e.OffsetVertical;
			OffsetLongitudinal = e.OffsetLongitudinal;
			BasisCurve = e.BasisCurve;
		}
		public IfcPointByDistanceExpression(IfcCurveMeasureSelect distanceAlong, IfcCurve basisCurve) : base(basisCurve.Database) { DistanceAlong = distanceAlong; BasisCurve = basisCurve; }
		public IfcPointByDistanceExpression(double nonNegativeLength, IfcCurve basisCurve) : base(basisCurve.Database) { DistanceAlong = new IfcNonNegativeLengthMeasure(nonNegativeLength); BasisCurve = basisCurve; }
		[Obsolete("DEPRECATED IFC4x3", false)]
		public IfcPointByDistanceExpression(DatabaseIfc db, double distanceAlong) : base(db) { DistanceAlong = new IfcNonNegativeLengthMeasure(distanceAlong); }


		internal IfcPointByDistanceExpression Duplicate()
		{
			return new IfcPointByDistanceExpression()
			{ DistanceAlong = DistanceAlong, OffsetLateral = OffsetLateral, OffsetVertical = OffsetVertical, OffsetLongitudinal = OffsetLongitudinal, BasisCurve = BasisCurve };
		}
	}
	[Serializable]
	public partial class IfcPointOnCurve : IfcPoint
	{
		internal int mBasisCurve;// : IfcCurve;
		internal double mPointParameter;// : IfcParameterValue; 

		public IfcCurve BasisCurve { get { return mDatabase[mBasisCurve] as IfcCurve; } set { mBasisCurve = value.mIndex; } }
		public double PointParameter { get { return mPointParameter; } set { mPointParameter = value; } }

		internal IfcPointOnCurve() : base() { }
		internal IfcPointOnCurve(DatabaseIfc db, IfcPointOnCurve p, DuplicateOptions options) : base(db, p, options)
		{
			BasisCurve = p.BasisCurve.Duplicate(db, options) as IfcCurve;
			mPointParameter = p.mPointParameter;
		}
		public IfcPointOnCurve(DatabaseIfc m, IfcCurve c, double p) : base(m) { mBasisCurve = c.mIndex; mPointParameter = p; }
	}
	[Serializable]
	public partial class IfcPointOnSurface : IfcPoint
	{
		internal int mBasisSurface;// : IfcSurface;
		internal double mPointParameterU, mPointParameterV;// : IfcParameterValue; 

		public IfcSurface BasisSurface { get { return mDatabase[mBasisSurface] as IfcSurface; } set { mBasisSurface = value.mIndex; } }

		internal IfcPointOnSurface() : base() { }
		internal IfcPointOnSurface(DatabaseIfc db, IfcPointOnSurface p, DuplicateOptions options) : base(db, p, options)
		{
			BasisSurface = db.Factory.Duplicate(p.BasisSurface) as IfcSurface;
			mPointParameterU = p.mPointParameterU;
			mPointParameterV = p.mPointParameterV;
		}
	}
	public partial interface IfcPointOrVertexPoint : IBaseClassIfc { }  // = SELECT ( IfcPoint, IfcVertexPoint);
	[Serializable]
	public partial class IfcPolygonalBoundedHalfSpace : IfcHalfSpaceSolid
	{
		internal int mPosition;// : IfcAxis2Placement3D;
		internal int mPolygonalBoundary;// : IfcBoundedCurve; 

		public IfcAxis2Placement3D Position { get { return mDatabase[mPosition] as IfcAxis2Placement3D; } set { mPosition = value.mIndex; } }
		public IfcBoundedCurve PolygonalBoundary { get { return mDatabase[mPolygonalBoundary] as IfcBoundedCurve; } set { mPolygonalBoundary = value.mIndex; } }

		internal IfcPolygonalBoundedHalfSpace() : base() { }
		internal IfcPolygonalBoundedHalfSpace(DatabaseIfc db, IfcPolygonalBoundedHalfSpace s, DuplicateOptions options) : base(db, s, options) { Position = db.Factory.Duplicate(s.Position) as IfcAxis2Placement3D; PolygonalBoundary = db.Factory.Duplicate(s.PolygonalBoundary) as IfcBoundedCurve; }
	}
	[Serializable]
	public partial class IfcPolygonalFaceSet : IfcTessellatedFaceSet //IFC4A2
	{
		internal IfcLogicalEnum mClosed = IfcLogicalEnum.UNKNOWN; // 	OPTIONAL BOOLEAN;
		internal LIST<IfcIndexedPolygonalFace> mFaces = new LIST<IfcIndexedPolygonalFace>(); // : LIST [1:?] OF IfcIndexedPolygonalFace;
		internal LIST<int> mPnIndex = new LIST<int>(); // : OPTIONAL LIST [1:?] OF IfcPositiveInteger;

		public bool Closed { get { return mClosed == IfcLogicalEnum.TRUE; } set { mClosed = value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE; } }
		public LIST<IfcIndexedPolygonalFace> Faces { get { return mFaces; } }
		public LIST<int> PnIndex { get { return mPnIndex; } }

		internal IfcPolygonalFaceSet() : base() { }
		internal IfcPolygonalFaceSet(DatabaseIfc db, IfcPolygonalFaceSet s, DuplicateOptions options) : base(db, s, options) { Faces.AddRange(s.Faces.Select(x => db.Factory.Duplicate(x) as IfcIndexedPolygonalFace)); }
		public IfcPolygonalFaceSet(IfcCartesianPointList pl, IEnumerable<IfcIndexedPolygonalFace> faces) : base(pl) { Faces.AddRange(faces); }
		public IfcPolygonalFaceSet(IfcCartesianPointList pl, params IfcIndexedPolygonalFace[] faces) : base(pl) { Faces.AddRange(faces); }
	}
	[Serializable]
	public partial class IfcPolyline : IfcBoundedCurve
	{
		private LIST<IfcCartesianPoint> mPoints = new LIST<IfcCartesianPoint>();// : LIST [2:?] OF IfcCartesianPoint;
		public LIST<IfcCartesianPoint> Points
		{
			get { return mPoints; }
			set { mPoints = value; }
		}

		internal IfcPolyline() : base() { }
		internal IfcPolyline(DatabaseIfc db, IfcPolyline p, DuplicateOptions options) : base(db, p, options) { Points.AddRange(p.Points.ConvertAll(x => db.Factory.Duplicate(x) as IfcCartesianPoint)); }
		public IfcPolyline(IfcCartesianPoint start, IfcCartesianPoint end) : base(start.mDatabase) { mPoints.Add(start); mPoints.Add(end); }
		public IfcPolyline(IEnumerable<IfcCartesianPoint> pts) : base(pts.First().mDatabase) { Points.AddRange(pts); }
		public IfcPolyline(DatabaseIfc db, IEnumerable<Tuple<double, double>> points) : base(db) { Points.AddRange(points.Select(x=> new IfcCartesianPoint(db, x.Item1, x.Item2))); }
		public IfcPolyline(DatabaseIfc db, List<Tuple<double, double, double>> points) : base(db) { Points.AddRange(points.ConvertAll(x => new IfcCartesianPoint(db, x.Item1, x.Item2, x.Item3))); }
	}
	[Serializable]
	public partial class IfcPolyLoop : IfcLoop
	{
		internal LIST<IfcCartesianPoint> mPolygon = new LIST<IfcCartesianPoint> ();// : LIST [3:?] OF UNIQUE IfcCartesianPoint;
		public LIST<IfcCartesianPoint> Polygon { get { return mPolygon; } set { mPolygon = value; } }

		internal IfcPolyLoop() : base() { }
		internal IfcPolyLoop(DatabaseIfc db, IfcPolyLoop l, DuplicateOptions options) : base(db, l, options) { mPolygon.AddRange(l.Polygon.ConvertAll(x=> db.Factory.Duplicate(x) as IfcCartesianPoint)); }
		public IfcPolyLoop(IEnumerable<IfcCartesianPoint> polygon) : base(polygon.First().mDatabase) { mPolygon.AddRange(polygon); }
		public IfcPolyLoop(IfcCartesianPoint cp1, IfcCartesianPoint cp2, IfcCartesianPoint cp3) : base(cp1.mDatabase) { mPolygon.Add(cp1); mPolygon.Add(cp2); mPolygon.Add(cp3); }
		public IfcPolyLoop(IfcCartesianPoint cp1, IfcCartesianPoint cp2, IfcCartesianPoint cp3, IfcCartesianPoint cp4) : this(cp1, cp2, cp3) { mPolygon.Add(cp4); }

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcCartesianPoint p in Polygon)
				result.AddRange(p.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public partial class IfcPolynomialCurve : IfcCurve
	{
		private IfcPlacement mPosition = null; //: IfcPlacement;
		private LIST<double> mCoefficientsX = new LIST<double>(); //: OPTIONAL  LIST[2:?] OF IfcReal;
		private LIST<double> mCoefficientsY = new LIST<double>(); //: OPTIONAL LIST[2:?] OF IfcReal;
		private LIST<double> mCoefficientsZ = new LIST<double>();//: OPTIONAL LIST[2:?] OF IfcReal;

		public IfcPlacement Position { get { return mPosition; } set { mPosition = value; } }
		public LIST<double> CoefficientsX { get { return mCoefficientsX; } set { mCoefficientsX = value; } }
		public LIST<double> CoefficientsY { get { return mCoefficientsY; } set { mCoefficientsY = value; } }
		public LIST<double> CoefficientsZ { get { return mCoefficientsY; } set { mCoefficientsY = value; } }

		public IfcPolynomialCurve() : base() { }
		internal IfcPolynomialCurve(DatabaseIfc db, IfcPolynomialCurve polynomial, DuplicateOptions options)
		: base(db, polynomial, options)
		{
			Position = db.Factory.Duplicate(polynomial.Position as BaseClassIfc, options) as IfcPlacement;
			CoefficientsX.AddRange(polynomial.mCoefficientsX);
			CoefficientsY.AddRange(polynomial.mCoefficientsY);
			CoefficientsZ.AddRange(polynomial.mCoefficientsZ);
		}
		public IfcPolynomialCurve(IfcPlacement position, IEnumerable<double> coefficientsX, IEnumerable<double> coefficientsY)
			: base(position.Database)
		{
			Position = position;
			CoefficientsX.AddRange(coefficientsX);
			CoefficientsY.AddRange(coefficientsY);
		}
	}
	[Serializable]
	public abstract partial class IfcPort : IfcProduct
	{   //INVERSE	
		internal IfcRelConnectsPortToElement mContainedIn = null;//	 :	SET [0:1] OF IfcRelConnectsPortToElement FOR RelatingPort;
		internal IfcRelConnectsPorts mConnectedFrom = null;//	 :	SET [0:1] OF IfcRelConnectsPorts FOR RelatedPort;
		internal IfcRelConnectsPorts mConnectedTo = null;//	 :	SET [0:1] OF IfcRelConnectsPorts FOR RelatingPort;

		public IfcRelConnectsPortToElement ContainedIn { get { return mContainedIn; } }
		public IfcRelConnectsPorts ConnectedFrom { get { return mConnectedFrom; } }
		public IfcRelConnectsPorts ConnectedTo { get { return mConnectedTo; } }

		protected IfcPort() : base() { }
		protected IfcPort(DatabaseIfc db, IfcPort p, DuplicateOptions options) : base(db, p, options) { }
		protected IfcPort(DatabaseIfc db) : base(db) { }
		protected IfcPort(IfcElement e) : base(e.mDatabase)
		{
			if (mDatabase.mRelease < ReleaseVersion.IFC4)
			{
				new IfcRelConnectsPortToElement(this, e);
			}
			else
			{
				if (e.IsNestedBy.Count() == 0)
					new IfcRelNests(e, this);
				else
					e.IsNestedBy.First().RelatedObjects.Add(this);
			}
		}
		protected IfcPort(IfcElementType t) : base(t.mDatabase)
		{
			if (mDatabase.mRelease < ReleaseVersion.IFC4)
			{
				t.AddAggregated(this);
			}
			else
			{
				if (t.IsNestedBy.Count() == 0)
				{
					new IfcRelNests(t, this);
				}
				else
					t.IsNestedBy.First().RelatedObjects.Add(this);
			}
		}

		internal IfcElement getElement()
		{
			IfcRelNests nests = Nests;
			if (nests != null)
			{
				IfcElement result = nests.RelatingObject as IfcElement;
				if (result != null)
					return result;
			}
			if(mContainedIn != null)
				return mContainedIn.RelatedElement;
			return null;
		}
	}
	[Serializable]
	public partial class IfcPostalAddress : IfcAddress
	{
		internal string mInternalLocation = "$";// : OPTIONAL IfcLabel;
		internal LIST<string> mAddressLines = new LIST<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		internal string mPostalBox = "$";// :OPTIONAL IfcLabel;
		internal string mTown = "$";// : OPTIONAL IfcLabel;
		internal string mRegion = "$";// : OPTIONAL IfcLabel;
		internal string mPostalCode = "$";// : OPTIONAL IfcLabel;
		internal string mCountry = "$";// : OPTIONAL IfcLabel; 

		public string InternalLocation { get { return (mInternalLocation == "$" ? "" : ParserIfc.Decode(mInternalLocation)); } set { mInternalLocation = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public LIST<string> AddressLines { get { return mAddressLines; } set { mAddressLines = value; } }
		public string PostalBox { get { return (mPostalBox == "$" ? "" : ParserIfc.Decode(mPostalBox)); } set { mPostalBox = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Town { get { return (mTown == "$" ? "" : ParserIfc.Decode(mTown)); } set { mTown = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Region { get { return (mRegion == "$" ? "" : ParserIfc.Decode(mRegion)); } set { mRegion = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string PostalCode { get { return (mPostalCode == "$" ? "" : ParserIfc.Decode(mPostalCode)); } set { mPostalCode = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Country { get { return (mCountry == "$" ? "" : ParserIfc.Decode(mCountry)); } set { mCountry = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcPostalAddress() : base() { }
		public IfcPostalAddress(DatabaseIfc db) : base(db) { }
		internal IfcPostalAddress(DatabaseIfc db, IfcPostalAddress a) : base(db, a)
		{
			mInternalLocation = a.mInternalLocation; mAddressLines = a.mAddressLines; mPostalBox = a.mPostalBox;
			mTown = a.mTown; mRegion = a.mRegion; mPostalCode = a.mPostalCode; mCountry = a.mCountry;
		}
	}
	[Serializable]
	public abstract partial class IfcPositioningElement : IfcProduct //IFC4.1
	{   // ABSTRACT SUPERTYPE OF(ONEOF(IfcGrid, IfcLinearPositioningElement, IfcReferent))
		//INVERSE
		private SET<IfcRelPositions> mPositions = new SET<IfcRelPositions>();//: SET[0:?] OF IfcRelPositions FOR RelatingPositioningElement; 
		public IfcRelContainedInSpatialStructure ContainedinStructure { get { return mContainedInStructure; } }
		public SET<IfcRelPositions> Positions { get { return mPositions; } } 

		protected IfcPositioningElement() : base() { }
		protected IfcPositioningElement(DatabaseIfc db) : base(db) { }
		protected IfcPositioningElement(IfcSpatialStructureElement host) : base(host.Database) { host.AddElement(this); }
		protected IfcPositioningElement(IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(placement, representation) { }
		protected IfcPositioningElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		protected IfcPositioningElement(DatabaseIfc db, IfcPositioningElement e, DuplicateOptions options) : base(db, e, options) { }
	}
	[Serializable]
	public abstract partial class IfcPreDefinedColour : IfcPreDefinedItem, IfcColour //	ABSTRACT SUPERTYPE OF(IfcDraughtingPreDefinedColour)
	{
		protected IfcPreDefinedColour() : base() { }
		protected IfcPreDefinedColour(DatabaseIfc db, IfcPreDefinedColour c) : base(db, c) { }
	}
	[Serializable]
	public abstract partial class IfcPreDefinedCurveFont : IfcPreDefinedItem, IfcCurveStyleFontSelect
	{
		protected IfcPreDefinedCurveFont() : base() { }
		protected IfcPreDefinedCurveFont(DatabaseIfc db, IfcPreDefinedCurveFont f) : base(db, f) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcPreDefinedDimensionSymbol : IfcPreDefinedSymbol // DEPRECATED IFC4
	{
		internal IfcPreDefinedDimensionSymbol() : base() { }
		internal IfcPreDefinedDimensionSymbol(DatabaseIfc db, IfcPreDefinedDimensionSymbol s) : base(db, s) { }
	}
	[Serializable]
	public abstract partial class IfcPreDefinedItem : IfcPresentationItem, NamedObjectIfc
	{
		internal string mName = "";//: IfcLabel; 
		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcPreDefinedItem() : base() { }
		protected IfcPreDefinedItem(DatabaseIfc db, IfcPreDefinedItem i) : base(db, i) { mName = i.mName; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcPreDefinedPointMarkerSymbol : IfcPreDefinedSymbol // DEPRECATED IFC4
	{
		internal IfcPreDefinedPointMarkerSymbol() : base() { }
		internal IfcPreDefinedPointMarkerSymbol(DatabaseIfc db, IfcPreDefinedPointMarkerSymbol s) : base(db, s) { }
	}
	[Serializable]
	public abstract partial class IfcPreDefinedProperties : IfcPropertyAbstraction // IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcementBarProperties, IfcSectionProperties, IfcSectionReinforcementProperties))
	{
		protected IfcPreDefinedProperties() : base() { }
		protected IfcPreDefinedProperties(DatabaseIfc db) : base(db) { }
		protected IfcPreDefinedProperties(DatabaseIfc db, IfcPreDefinedProperties p, DuplicateOptions options) : base(db, p, options) { }
	}
	[Serializable]
	public abstract partial class IfcPreDefinedPropertySet : IfcPropertySetDefinition // IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcDoorLiningProperties,  
	{ //IfcDoorPanelProperties, IfcPermeableCoveringProperties, IfcReinforcementDefinitionProperties, IfcWindowLiningProperties, IfcWindowPanelProperties))
		protected IfcPreDefinedPropertySet() : base() { }
		protected IfcPreDefinedPropertySet(DatabaseIfc db, IfcPreDefinedPropertySet p, DuplicateOptions options) : base(db, p, options) { }
		protected IfcPreDefinedPropertySet(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public abstract partial class IfcPreDefinedSymbol : IfcPreDefinedItem // DEPRECATED IFC4
	{
		protected IfcPreDefinedSymbol() : base() { }
		protected IfcPreDefinedSymbol(DatabaseIfc db, IfcPreDefinedSymbol s) : base(db, s) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcPreDefinedTerminatorSymbol : IfcPreDefinedSymbol // DEPRECATED IFC4
	{
		internal IfcPreDefinedTerminatorSymbol() : base() { }
		internal IfcPreDefinedTerminatorSymbol(DatabaseIfc db, IfcPreDefinedTerminatorSymbol s) : base(db, s) { }
	}
	[Serializable]
	public abstract partial class IfcPreDefinedTextFont : IfcPreDefinedItem, IfcTextFontSelect
	{
		protected IfcPreDefinedTextFont() : base() { }
		protected IfcPreDefinedTextFont(DatabaseIfc db, IfcPreDefinedTextFont f) : base(db, f) { }
	}
	[Serializable]
	public abstract partial class IfcPresentationItem : BaseClassIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcColourRgbList, IfcColourSpecification,
	{ // IfcCurveStyleFont, IfcCurveStyleFontAndScaling, IfcCurveStyleFontPattern, IfcIndexedColourMap, IfcPreDefinedItem, IfcSurfaceStyleLighting, IfcSurfaceStyleRefraction, IfcSurfaceStyleShading, IfcSurfaceStyleWithTextures, IfcSurfaceTexture, IfcTextStyleForDefinedFont, IfcTextStyleTextModel, IfcTextureCoordinate, IfcTextureVertex, IfcTextureVertexList));
		protected IfcPresentationItem() : base() { }
		protected IfcPresentationItem(DatabaseIfc db) : base(db) { }
		protected IfcPresentationItem(DatabaseIfc db, IfcPresentationItem i) : base(db, i) { }
	}
	[Serializable]
	public partial class IfcPresentationLayerAssignment : BaseClassIfc, NamedObjectIfc  //SUPERTYPE OF	(IfcPresentationLayerWithStyle);
	{
		private string mName = "$";// : IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal SET<IfcLayeredItem> mAssignedItems = new SET<IfcLayeredItem>();// : SET [1:?] OF IfcLayeredItem; 
		internal string mIdentifier = "$";// : OPTIONAL IfcIdentifier;

		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "Default Layer" : mName = ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public SET<IfcLayeredItem> AssignedItems { get { return mAssignedItems; } set { mAssignedItems.Clear(); if (value != null) { mAssignedItems.CollectionChanged -= mAssignedItems_CollectionChanged; mAssignedItems = value; value.CollectionChanged += mAssignedItems_CollectionChanged; } } }
		public string Identifier { get { return (mIdentifier == "$" ? "" : ParserIfc.Decode(mIdentifier)); } set { mIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcPresentationLayerAssignment() : base() { }
		internal IfcPresentationLayerAssignment(DatabaseIfc db, IfcPresentationLayerAssignment a, DuplicateOptions options) : base(db, a)
		{
			mName = a.mName;
			mDescription = a.mDescription;
			if (options.DuplicateDownstream)
				mAssignedItems.AddRange(a.AssignedItems.ConvertAll(x => db.Factory.Duplicate(x as BaseClassIfc, options) as IfcLayeredItem));
			mIdentifier = a.mIdentifier;
		}
		public IfcPresentationLayerAssignment(DatabaseIfc db, string name) : base(db) { Name = name; }
		public IfcPresentationLayerAssignment(string name, IfcLayeredItem item) : this(item.Database, name) { mAssignedItems.Add(item); }
		public IfcPresentationLayerAssignment(string name, List<IfcLayeredItem> items) : this(items[0].Database, name) { mAssignedItems.AddRange(items); }

		protected override void initialize()
		{
			base.initialize();
			mAssignedItems.CollectionChanged += mAssignedItems_CollectionChanged;
		}

		protected virtual void mAssignedItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcLayeredItem i in e.NewItems)
				{
					if (i != null)
					{
						i.LayerAssignment = this;
					}
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcLayeredItem i in e.OldItems)
				{
					if (i != null)
						i.LayerAssignment = null;
				}
			}
		}
		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcPresentationLayerAssignment a = e as IfcPresentationLayerAssignment;
			if (a == null || string.Compare(Name, a.Name) != 0 || string.Compare(Description, a.Description) != 0 || string.Compare(Identifier, a.Identifier) != 0)
				return false;
			return base.isDuplicate(e, tol);
		}
	}
	[Serializable]
	public partial class IfcPresentationLayerWithStyle : IfcPresentationLayerAssignment
	{
		internal IfcLogicalEnum mLayerOn = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		internal IfcLogicalEnum mLayerFrozen = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		internal IfcLogicalEnum mLayerBlocked = IfcLogicalEnum.UNKNOWN;// LOGICAL;
		internal List<int> mLayerStyles = new List<int>();// SET OF IfcPresentationStyleSelect; IFC4 IfcPresentationStyle

		public IfcLogicalEnum LayerOn { get { return mLayerOn; } set { mLayerOn = value; } }
		public IfcLogicalEnum LayerFrozen { get { return mLayerFrozen; } set { mLayerFrozen = value; } }
		public IfcLogicalEnum LayerBlocked { get { return mLayerBlocked; } set { mLayerBlocked = value; } }
		public ReadOnlyCollection<IfcPresentationStyle> LayerStyles { get { return new ReadOnlyCollection<IfcPresentationStyle>(mLayerStyles.ConvertAll(x => mDatabase[x] as IfcPresentationStyle)); } }

		internal IfcPresentationLayerWithStyle() : base() { }
		internal IfcPresentationLayerWithStyle(DatabaseIfc db, IfcPresentationLayerWithStyle l, DuplicateOptions options) : base(db, l, options) { mLayerOn = l.mLayerOn; mLayerFrozen = l.mLayerFrozen; mLayerBlocked = l.mLayerBlocked; l.LayerStyles.ToList().ForEach(x => addLayerStyle(db.Factory.Duplicate(x) as IfcPresentationStyle)); }

		internal IfcPresentationLayerWithStyle(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPresentationLayerWithStyle(string name, IfcLayeredItem item, IfcPresentationStyle style) : base(name, item) { mLayerStyles.Add(style.mIndex); }
		public IfcPresentationLayerWithStyle(string name, IfcLayeredItem item, List<IfcPresentationStyle> styles) : base(name, item) { styles.ForEach(x => addLayerStyle(x)); }
		public IfcPresentationLayerWithStyle(string name, List<IfcLayeredItem> items, List<IfcPresentationStyle> styles) : base(name, items) { styles.ForEach(x => addLayerStyle(x)); }
		public IfcPresentationLayerWithStyle(string name, List<IfcLayeredItem> items, IfcPresentationStyle style) : base(name, items) { mLayerStyles.Add(style.mIndex); }
	
		internal void addLayerStyle(IfcPresentationStyle style) { mLayerStyles.Add(style.mIndex); }

		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcPresentationLayerWithStyle a = e as IfcPresentationLayerWithStyle;
			if (a == null || mLayerOn != a.mLayerOn || mLayerFrozen != a.mLayerFrozen || mLayerBlocked != a.mLayerBlocked)
				return false;
			if (mLayerStyles.Count != a.mLayerStyles.Count)
				return false;
			List<IfcPresentationStyle> styles = a.LayerStyles.ToList();
			foreach (IfcPresentationStyle style in LayerStyles)
			{
				bool duplicated = false;
				foreach (IfcPresentationStyle s in styles)
				{
					if (style.isDuplicate(s, tol))
					{
						duplicated = true;
						styles.Remove(style);
					}
				}
				if (!duplicated)
					return false;
			}
			return base.isDuplicate(e, tol);
		}
	}
	[Serializable]
	public abstract partial class IfcPresentationStyle : BaseClassIfc, IfcStyleAssignmentSelect, NamedObjectIfc  //ABSTRACT SUPERTYPE OF (ONEOF(IfcCurveStyle,IfcFillAreaStyle,IfcSurfaceStyle,IfcSymbolStyle,IfcTextStyle));
	{
		private string mName = "$";// : OPTIONAL IfcLabel;		
		//INVERSE
		internal List<IfcStyledItem> mStyledItems = new List<IfcStyledItem>();

		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcStyledItem> StyledItems { get { return new ReadOnlyCollection<IfcStyledItem>(mStyledItems); } }

		protected IfcPresentationStyle() : base() { }
		protected IfcPresentationStyle(DatabaseIfc db) : base(db) { }
		protected IfcPresentationStyle(DatabaseIfc db, IfcPresentationStyle s) : base(db, s) { mName = s.mName; }
		protected IfcPresentationStyle(IfcPresentationStyle i) : base() { mName = i.mName; }

		public void associateItem(IfcStyledItem item) { mStyledItems.Add(item); }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcPresentationStyleAssignment : BaseClassIfc, IfcStyleAssignmentSelect //DEPRECATED IFC4
	{
		internal List<int> mStyles = new List<int>();// : SET [1:?] OF IfcPresentationStyleSelect; 
		//INVERSE
		internal List<IfcStyledItem> mStyledItems = new List<IfcStyledItem>();

		public ReadOnlyCollection<IfcPresentationStyleSelect> Styles { get { return new ReadOnlyCollection<IfcPresentationStyleSelect>(mStyles.ConvertAll(x => mDatabase[x] as IfcPresentationStyleSelect)); } }
		public ReadOnlyCollection<IfcStyledItem> StyledItems { get { return new ReadOnlyCollection<IfcStyledItem>(mStyledItems); } }

		internal IfcPresentationStyleAssignment() : base() { }
		internal IfcPresentationStyleAssignment(DatabaseIfc db, IfcPresentationStyleAssignment s) : base(db, s) { mStyles.AddRange(s.mStyles.Select(x=> db.Factory.Duplicate(s.mDatabase[x]).Index)); }
		public IfcPresentationStyleAssignment(IfcPresentationStyle style) : base(style.mDatabase) { mStyles.Add(style.Index); }
		public IfcPresentationStyleAssignment(IEnumerable<IfcPresentationStyle> styles) : base(styles.First().mDatabase) { mStyles.AddRange(styles.Select(x => x.StepId)); }
		
		internal void addStyle(IfcPresentationStyleSelect style) { mStyles.Add(style.Index); }
		public void associateItem(IfcStyledItem item) { mStyledItems.Add(item); }
	}
	public partial interface IfcPresentationStyleSelect : IBaseClassIfc { } //DEPRECATED IFC4 TYPE  = SELECT(IfcNullStyle, IfcCurveStyle, IfcSymbolStyle, IfcFillAreaStyle, IfcTextStyle, IfcSurfaceStyle);
	[Serializable]
	public partial class IfcProcedure : IfcProcess
	{
		internal IfcProcedureTypeEnum mPredefinedType;// : IfcProcedureTypeEnum;
		internal string mUserDefinedProcedureType = "$";// : OPTIONAL IfcLabel;
		
		public IfcProcedureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProcedure() : base() { }
		internal IfcProcedure(DatabaseIfc db, IfcProcedure p, DuplicateOptions options) : base(db, p, options) { mPredefinedType = p.mPredefinedType; mUserDefinedProcedureType = p.mUserDefinedProcedureType; }
	}
	[Serializable]
	public partial class IfcProcedureType : IfcTypeProcess //IFC4
	{
		internal IfcProcedureTypeEnum mPredefinedType = IfcProcedureTypeEnum.NOTDEFINED;// : IfcProcedureTypeEnum; 
		public IfcProcedureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProcedureType() : base() { }
		internal IfcProcedureType(DatabaseIfc db, IfcProcedureType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcProcedureType(DatabaseIfc m, string name, IfcProcedureTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public abstract partial class IfcProcess : IfcObject, IfcProcessSelect  // ABSTRACT SUPERTYPE OF (ONEOF (IfcProcedure ,IfcTask))
	{
		internal string mIdentification = "";// :OPTIONAL IfcIdentifier;
		internal string mLongDescription = "";//: OPTIONAL IfcText; 
		//INVERSE
		internal List<IfcRelSequence> mIsPredecessorTo = new List<IfcRelSequence>();// : SET [0:?] OF IfcRelSequence FOR RelatingProcess; 
		internal List<IfcRelSequence> mIsSuccessorFrom = new List<IfcRelSequence>();// : SET [0:?] OF IfcRelSequence FOR RelatedProcess;
		internal SET<IfcRelAssignsToProcess> mOperatesOn = new SET<IfcRelAssignsToProcess>();// : SET [0:?] OF IfcRelAssignsToProcess FOR RelatingProcess;

		public string Identification { get { return mIdentification; } set { mIdentification = value; } }
		public string LongDescription { get { return mLongDescription; } set { mLongDescription = value; } }
		public SET<IfcRelAssignsToProcess> OperatesOn { get { return mOperatesOn; } }

		protected IfcProcess() : base() { }
		protected IfcProcess(DatabaseIfc db, IfcProcess p, DuplicateOptions options ) : base(db, p, options)
		{
			mIdentification = p.mIdentification;
			mLongDescription = p.mLongDescription;
			if (options.DuplicateDownstream)
			{
				foreach (IfcRelAssignsToProcess rap in mOperatesOn)
					db.Factory.Duplicate(rap, options);
			}

		}
		protected IfcProcess(DatabaseIfc db) : base(db)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.Ifc2x3NotAssigned)
				throw new Exception("Invalid Model View for IfcProcess : " + db.ModelView.ToString());
		}
		protected IfcProcess(IfcProcess process) : base(process, false)
		{
			mIdentification = process.mIdentification;
			mLongDescription = process.mLongDescription;
		}
		public bool AddOperatesOn(IfcObjectDefinition related)
		{
			if (mOperatesOn.Count == 0)
				new IfcRelAssignsToProcess(this, related);
			else if (!mOperatesOn.First().mRelatedObjects.Contains(related))
				mOperatesOn.First().RelatedObjects.Add(related);
			else
				return false;
			return true;
		}
	}
	public interface IfcProcessSelect : IBaseClassIfc // SELECT(IfcProcess, IfcTypeProcess)c
	{
		SET<IfcRelAssignsToProcess> OperatesOn { get; }
	} 
	[Serializable]
	public abstract partial class IfcProduct : IfcObject, IfcProductSelect, IfcSpatialReferenceSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcAnnotation ,IfcElement ,IfcGrid ,IfcPort ,IfcProxy ,IfcSpatialElement ,IfcStructuralActivity ,IfcStructuralItem))
	{
		private IfcObjectPlacement mObjectPlacement = null; //: OPTIONAL IfcObjectPlacement;
		private IfcProductDefinitionShape mRepresentation = null; //: OPTIONAL IfcProductRepresentation 
		//INVERSE
		[NonSerialized] internal IfcRelContainedInSpatialStructure mContainedInStructure = null;
		[NonSerialized] internal SET<IfcRelAssignsToProduct> mReferencedBy = new SET<IfcRelAssignsToProduct>();// :	SET OF IfcRelAssignsToProduct FOR RelatingProduct;
		[NonSerialized] internal IfcRelPositions mPositionedRelativeTo = null; // : SET [0:1] OF IfcRelPositions FOR RelatedProducts;
		[NonSerialized] internal SET<IfcRelReferencedInSpatialStructure> mReferencedInStructures = new SET<IfcRelReferencedInSpatialStructure>();//  : 	SET OF IfcRelReferencedInSpatialStructure FOR RelatedElements;

		public IfcObjectPlacement ObjectPlacement
		{
			get { return mObjectPlacement; }
			set
			{
				if (mObjectPlacement != null)
					mObjectPlacement.mPlacesObject.Remove(this);
				mObjectPlacement = value;
				if (value != null)
					value.mPlacesObject.Add(this);
			}
		}
		public IfcProductDefinitionShape Representation
		{
			get { return mRepresentation; }
			set
			{
				if(mRepresentation != null)
					mRepresentation.mShapeOfProduct.Remove(this);
				mRepresentation = value;
				if (value != null)
				{
					mRepresentation.mShapeOfProduct.Add(this);
					if (mObjectPlacement == null)
					{
						IfcElement element = this as IfcElement;
						if (element == null)
							ObjectPlacement = new IfcLocalPlacement(mDatabase.Factory.XYPlanePlacement);
						else
						{
							IfcProduct product = element.getContainer();
							if (product == null)
								ObjectPlacement = new IfcLocalPlacement(mDatabase.Factory.XYPlanePlacement);
							else
							{
								if (mDatabase.mRelease > ReleaseVersion.IFC2x3)
								{
									if (product.mContainerCommonPlacement == null)
									{
										ObjectPlacement = mContainerCommonPlacement = new IfcLocalPlacement(ObjectPlacement, mDatabase.Factory.XYPlanePlacement);
										mContainerCommonPlacement.mContainerHost = product;
									}
									else
										ObjectPlacement = product.mContainerCommonPlacement;
								}
								else
									ObjectPlacement = new IfcLocalPlacement(product.ObjectPlacement, mDatabase.Factory.XYPlanePlacement);
							}
						}
					}
				}
			}
		}
		public SET<IfcRelAssignsToProduct> ReferencedBy { get { return mReferencedBy; } }
		public IfcRelPositions PositionedRelativeTo { get { return mPositionedRelativeTo; } set { mPositionedRelativeTo = value; } }
		public SET<IfcRelReferencedInSpatialStructure> ReferencedInStructures { get { return mReferencedInStructures; } }

		internal IfcObjectPlacement mContainerCommonPlacement = null; //GeometryGym common Placement reference for aggregated items

		protected IfcProduct() : base() { }
		protected IfcProduct(IfcProduct product, bool replace) : base(product, replace)
		{
			ObjectPlacement = product.ObjectPlacement;
			Representation = product.Representation;
			if(replace)
			{
				mReferencedBy = product.mReferencedBy;
				mContainedInStructure = product.mContainedInStructure;
			}
		}
		protected IfcProduct(IfcProductDefinitionShape rep) : base(rep.mDatabase) { Representation = rep; }
		protected IfcProduct(IfcObjectPlacement placement) : base(placement.mDatabase) { ObjectPlacement = placement; }
		protected IfcProduct(IfcObjectPlacement placement, IfcProductDefinitionShape rep) : base(placement == null ? rep.mDatabase : placement.mDatabase)
		{
			ObjectPlacement = placement;
			Representation = rep;
		}
		protected IfcProduct(DatabaseIfc db) : base(db) { }
		protected IfcProduct(DatabaseIfc db, IfcProduct p, DuplicateOptions options) : base(db, p, options)
		{
			if (p.mObjectPlacement != null)
				ObjectPlacement = p.mObjectPlacement.Duplicate(db);
			if (p.mRepresentation != null)
				Representation = db.Factory.Duplicate(p.Representation, options) as IfcProductDefinitionShape;
			foreach (IfcRelAssignsToProduct rap in p.mReferencedBy)
			{
				IfcRelAssignsToProduct rp = db.Factory.Duplicate(rap, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcRelAssignsToProduct;
				foreach (IfcObjectDefinition od in rap.RelatedObjects)
					rp.RelatedObjects.Add(db.Factory.Duplicate(od, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcObjectDefinition);
				rp.RelatingProduct = this;
			}
			if (options.DuplicateHost && p.mContainedInStructure != null)
			{
				IfcRelContainedInSpatialStructure rcss = db.Factory.Duplicate(p.mContainedInStructure, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcRelContainedInSpatialStructure;
				rcss.RelatedElements.Add(this);
			}
		}
		protected IfcProduct(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductDefinitionShape r) : base(host.mDatabase)
		{
			IfcElement el = this as IfcElement;
			IfcProduct product = host as IfcProduct;
			if (el != null && product != null)
				product.AddElement(el);
			else
				host.AddAggregated(this);
			ObjectPlacement = p;
			Representation = r;
		}

		protected override void initialize()
		{
			base.initialize();

			mReferencedBy.CollectionChanged += mReferencedBy_CollectionChanged;
		}
		private void mReferencedBy_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelAssignsToProduct r in e.NewItems)
				{
					if (r.RelatingProduct != this)
						r.RelatingProduct = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelAssignsToProduct r in e.OldItems)
				{
					if (r.RelatingProduct == this)
						r.RelatingProduct = null;
				}
			}
		}

		public void AddElement(IfcProduct product)
		{
			product.detachFromHost();
			IfcCovering c = product as IfcCovering;
			if (c != null)
			{
				IfcElement element = this as IfcElement;
				if (element != null)
				{
					if (element.mHasCoverings.Count == 0)
						element.mHasCoverings.Add(new IfcRelCoversBldgElements(element, c));
					else
						element.mHasCoverings.First().RelatedCoverings.Add(c);
					return;
				}
				IfcSpace space = this as IfcSpace;
				if(space != null)
				{
					if (space.mHasCoverings.Count == 0)
						space.mHasCoverings.Add(new IfcRelCoversSpaces(space, c));
					else
						space.mHasCoverings.First().RelatedCoverings.Add(c);
					return;
				}
			}
			IfcSpatialElement spatialElement = this as IfcSpatialElement;
			if (spatialElement != null)
			{
				if (spatialElement.mContainsElements.Count == 0)
				{
					new IfcRelContainedInSpatialStructure(product, spatialElement);
				}
				else
					spatialElement.ContainsElements.First().RelatedElements.Add(product);
				return;
			}
			if (mIsDecomposedBy.Count > 0)
				mIsDecomposedBy.First().RelatedObjects.Add(product);
			else
			{
				new IfcRelAggregates(this, product);
			}
		}
		internal void detachFromHost()
		{
			if (mDecomposes != null)
				mDecomposes.RelatedObjects.Remove(this);
			if (mContainedInStructure != null)
				mContainedInStructure.RelatedElements.Remove(this);
		}
		
		internal T FindHost<T>() where T : IfcProduct
		{
			T result = null;
			if(mDecomposes != null)
			{
				result = mDecomposes.RelatingObject as T;
				if (result != null)
					return result;
				IfcProduct host = mDecomposes.RelatingObject as IfcProduct;
				if (host != null)
					return host.FindHost<T>();
			}	
			else if(mNests != null)
			{
				result = mNests.RelatingObject as T;
				if (result != null)
					return result;
				IfcProduct host = mNests.RelatingObject as IfcProduct;
				if (host != null)
					return host.FindHost<T>();
			}
			else if(mContainedInStructure != null)
			{
				result = mContainedInStructure.RelatingStructure as T;
				if (result != null)
					return result;
				return mContainedInStructure.RelatingStructure.FindHost<T>();
			}
			return null;
		}
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			if (mRepresentation != null && !typeof(IfcRoot).IsAssignableFrom(type))
				result.AddRange(mRepresentation.Extract<T>());
			return result;
		}
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcProductsOfCombustionProperties	 // DEPRECATED IFC4
	[Serializable]
	public partial class IfcProductDefinitionShape : IfcProductRepresentation<IfcShapeModel, IfcRepresentationItem>, IfcProductRepresentationSelect
	{
		//INVERSE
		internal SET<IfcProduct> mShapeOfProduct = new SET<IfcProduct>();
		internal SET<IfcShapeAspect> mHasShapeAspects = new SET<IfcShapeAspect>();

		public SET<IfcProduct> ShapeOfProduct { get { return mShapeOfProduct; } }
		public SET<IfcShapeAspect> HasShapeAspects { get { return mHasShapeAspects; } }

		internal IfcProductDefinitionShape() : base() { }
		private IfcProductDefinitionShape(IfcProductDefinitionShape shape) : base(shape.Representations) 
		{ 
			Name = shape.Name;
			Description = shape.Description;
		}
		public IfcProductDefinitionShape(IfcShapeModel rep) : base(rep) { }
		public IfcProductDefinitionShape(IEnumerable<IfcShapeModel> reps) : base(reps) { } 
		internal IfcProductDefinitionShape(DatabaseIfc db, IfcProductDefinitionShape s, DuplicateOptions options) : base(db, s, options) { }
		internal IfcProductDefinitionShape Duplicate() { return new IfcProductDefinitionShape(this); }
	}
	[Serializable]
	public abstract partial class IfcProductRepresentation<Representation, RepresentationItem> : BaseClassIfc, NamedObjectIfc where Representation : IfcRepresentation<RepresentationItem> where RepresentationItem : IfcRepresentationItem //IFC4 Abstract (IfcMaterialDefinitionRepresentation ,IfcProductDefinitionShape)); //IFC4 Abstract
	{
		private string mName = "$";// : OPTIONAL IfcLabel;
		private string mDescription = "$";// : OPTIONAL IfcText;
		private LIST<Representation> mRepresentations = new LIST<Representation>();// : LIST [1:?] OF IfcRepresentation; 

		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public LIST<Representation> Representations
		{
			get { return mRepresentations; }
			private set { mRepresentations.Clear(); if(value != null) { mRepresentations.CollectionChanged -= mRepresentations_CollectionChanged; mRepresentations = value; mRepresentations.CollectionChanged += mRepresentations_CollectionChanged; } }
		}

		protected IfcProductRepresentation() : base() { }
		protected IfcProductRepresentation(DatabaseIfc db) : base(db) { }
		protected IfcProductRepresentation(Representation r) : base(r.mDatabase) { mRepresentations.Add(r); }
		protected IfcProductRepresentation(IEnumerable<Representation> reps) : base(reps.First().mDatabase) { Representations.AddRange(reps); }
		internal IfcProductRepresentation(DatabaseIfc db, IfcProductRepresentation<Representation, RepresentationItem> r, DuplicateOptions options) : base(db, r)
		{
			mName = r.mName;
			mDescription = r.mDescription;
			Representations.AddRange(r.Representations.ToList().ConvertAll(x => db.Factory.Duplicate(x, options) as Representation));
		}

		protected override void initialize()
		{
			base.initialize();
			mRepresentations.CollectionChanged += mRepresentations_CollectionChanged;
		}

		protected virtual void mRepresentations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (Representation r in e.NewItems)
				{
					if (r != null)
					{
						IfcProductDefinitionShape productDefinitionShape = this as IfcProductDefinitionShape;
						if(productDefinitionShape != null)
							r.mOfProductRepresentation.Add(productDefinitionShape);
					}
				}
			}
			if (e.OldItems != null)
			{
				foreach (Representation r in e.OldItems)
				{
					if(r != null)
					{
						IfcProductDefinitionShape productDefinitionShape = this as IfcProductDefinitionShape;
						if(productDefinitionShape != null)
							r.mOfProductRepresentation.Remove(productDefinitionShape);
					}
				}
			}
		}
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (Representation r in Representations)
				result.AddRange(r.Extract<T>());
			return result;
		}
	}
	public interface IfcProductRepresentationSelect : IBaseClassIfc { SET<IfcShapeAspect> HasShapeAspects { get; } }// 	IfcProductDefinitionShape,	IfcRepresentationMap);
	public interface IfcProductSelect : IBaseClassIfc //	IfcProduct, IfcTypeProduct);
	{
		SET<IfcRelAssignsToProduct> ReferencedBy { get; }
		string GlobalId { get; }
	}
	[Serializable]
	public partial class IfcProfileDef : BaseClassIfc, IfcResourceObjectSelect, NamedObjectIfc  // SUPERTYPE OF (ONEOF (IfcArbitraryClosedProfileDef ,IfcArbitraryOpenProfileDef
	{  //,IfcCompositeProfileDef ,IfcDerivedProfileDef ,IfcParameterizedProfileDef));  IFC2x3 abstract 
		internal IfcProfileTypeEnum mProfileType = IfcProfileTypeEnum.AREA;// : IfcProfileTypeEnum;
		private string mProfileName = "$";// : OPTIONAL IfcLabel; 
		//INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal SET<IfcProfileProperties> mHasProperties = new SET<IfcProfileProperties>();
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg
		internal List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		public IfcProfileTypeEnum ProfileType { get { return mProfileType; } set { mProfileType = value; } }
		public string ProfileName { get { return mProfileName == "$" ? "" : ParserIfc.Decode(mProfileName); } set { mProfileName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Name { get { return ProfileName; } set { ProfileName = value; } }
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } set { mHasExternalReference.Clear();  if (value != null) { mHasExternalReference.CollectionChanged -= mHasExternalReference_CollectionChanged; mHasExternalReference = value; mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged; } } }
		public SET<IfcProfileProperties> HasProperties { get { return mHasProperties; }  set { mHasProperties = value; } }

		protected IfcProfileDef() : base() { }
		protected IfcProfileDef(DatabaseIfc db, IfcProfileDef p, DuplicateOptions options) : base(db, p)
		{
			mProfileType = p.mProfileType;
			mProfileName = p.mProfileName;
			foreach (IfcProfileProperties pp in p.mHasProperties)
				(db.Factory.Duplicate(pp, options) as IfcProfileProperties).ProfileDefinition = this;

		}
		public IfcProfileDef(DatabaseIfc db, string name) : base(db)
		{
			ProfileName = name;
			if (db != null && db.mRelease < ReleaseVersion.IFC4)
			{
				new IfcGeneralProfileProperties(this);
			}
		}

		protected override void initialize()
		{
			base.initialize();

			mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged;
		}
		private void mHasExternalReference_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.NewItems)
				{
					if (!r.RelatedResourceObjects.Contains(this))
						r.RelatedResourceObjects.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.OldItems)
					r.RelatedResourceObjects.Remove(this);
			}
		}
		internal IfcAxis2Placement3D CalculateTransform(IfcCardinalPointReference ip)
		{
			double halfDepth = ProfileDepth / 2.0, halfWidth = ProfileWidth / 2.0;

			if (ip == IfcCardinalPointReference.MID)
				return null;
			if (ip == IfcCardinalPointReference.BOTLEFT)
				return new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase, halfWidth, halfDepth, 0));
			if (ip == IfcCardinalPointReference.BOTMID)
				return new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase, 0, halfDepth, 0));
			if (ip == IfcCardinalPointReference.BOTRIGHT)
				return new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase, -halfWidth, halfDepth, 0));
			if (ip == IfcCardinalPointReference.MIDLEFT)
				return new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase, halfWidth, 0, 0));
			if (ip == IfcCardinalPointReference.MIDRIGHT)
				return new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase, -halfWidth, 0, 0));
			if (ip == IfcCardinalPointReference.TOPLEFT)
				return new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase, halfWidth, -halfDepth, 0));
			if (ip == IfcCardinalPointReference.TOPMID)
				return new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase, 0, -halfDepth, 0));
			if (ip == IfcCardinalPointReference.TOPRIGHT)
				return new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase, -halfWidth, -halfDepth, 0));
			return null;
		}
		internal virtual double ProfileDepth { get { return 0; } }
		internal virtual double ProfileWidth { get { return 0; } }

		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	[Serializable]
	public partial class IfcProfileProperties : IfcExtendedProperties //IFC2x3 Abstract : BaseClassIfc ABSTRACT SUPERTYPE OF	(ONEOF(IfcGeneralProfileProperties, IfcRibPlateProfileProperties));
	{
		public override string StepClassName { get { return (mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4 ? base.StepClassName : "IFCPROFILEPROPERTIES"); } }
		//[Obsolete("DEPRECATED IFC4", false)]
		//internal string mProfileName = "$";// : OPTIONAL IfcLabel; DELETED IFC4
		private IfcProfileDef mProfileDefinition = null;// : OPTIONAL IfcProfileDef; 
		public IfcProfileDef ProfileDefinition
		{
			get { return mProfileDefinition; }
			set
			{
				if (mProfileDefinition != null)
					mProfileDefinition.mHasProperties.Remove(this);
				mProfileDefinition = value;
				if(mProfileDefinition != null)
					value.mHasProperties.Add(this);
			}
		}

		internal IfcRelAssociatesProfileProperties mAssociates = null; //GeometryGym attribute

		internal IfcProfileProperties() : base() { }
		internal IfcProfileProperties(DatabaseIfc db, IfcProfileProperties p, DuplicateOptions options) : base(db, p, options) { ProfileDefinition = db.Factory.Duplicate(p.ProfileDefinition) as IfcProfileDef; }
		internal IfcProfileProperties(IfcProfileDef p) : base(p.mDatabase)
		{
			ProfileDefinition = p;
			if (mDatabase != null && mDatabase.mRelease < ReleaseVersion.IFC4)
				mAssociates = new IfcRelAssociatesProfileProperties(this) { Name = p.ProfileName };
		}
		internal IfcProfileProperties(List<IfcProperty> props, IfcProfileDef p) : base(props)
		{
			ProfileDefinition = p;
			p.mHasProperties.Add(this);
			if (mDatabase.mRelease < ReleaseVersion.IFC4)
				mAssociates = new IfcRelAssociatesProfileProperties(this) { Name = p.ProfileName };
		}
	}
	[Serializable]
	public partial class IfcProject : IfcContext
	{
		internal string ProjectNumber { get { return Name; } set { Name = (string.IsNullOrEmpty(value) ? "UNKNOWN PROJECT" : value); } }

		internal IfcProject() : base() { }
		internal IfcProject(DatabaseIfc db, IfcProject p, DuplicateOptions options) : base(db, p, options) { }
		public IfcProject(IfcBuilding building, string projectNumber) : this(building.mDatabase, projectNumber) { new IfcRelAggregates(this, building); }
		public IfcProject(IfcSite site, string projectNumber) : this(site.mDatabase, projectNumber) { new IfcRelAggregates(this, site); }
		public IfcProject(IfcBuilding building, string projectNumber, IfcUnitAssignment units) : this(projectNumber, units) { new IfcRelAggregates(this, building); }
		public IfcProject(IfcBuilding building, string projectNumber, IfcUnitAssignment.Length length) : this(building.mDatabase, projectNumber, length) { new IfcRelAggregates(this, building); }
		public IfcProject(IfcFacility facility, string projectNumber, IfcUnitAssignment units) : this(projectNumber, units) { new IfcRelAggregates(this, facility); }
		public IfcProject(IfcFacility facility, string projectNumber, IfcUnitAssignment.Length length) : this(facility.mDatabase, projectNumber, length) { new IfcRelAggregates(this, facility); }
		public IfcProject(IfcSite site, string projectNumber, IfcUnitAssignment units) : this(projectNumber, units) { new IfcRelAggregates(this, site); }
		public IfcProject(IfcSite site, string projectNumber, IfcUnitAssignment.Length length) : this(site.mDatabase, projectNumber, length) { new IfcRelAggregates(this, site); }
		public IfcProject(IfcSpatialZone zone, string projectNumber, IfcUnitAssignment units) : this(projectNumber, units) { new IfcRelAggregates(this, zone); }
		public IfcProject(IfcSpatialZone zone, string projectNumber, IfcUnitAssignment.Length length) : this(zone.mDatabase, projectNumber, length) { new IfcRelAggregates(this, zone); }
		public IfcProject(DatabaseIfc db, string projectNumber) : base(db, projectNumber)
		{
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECT";
		}
		internal IfcProject(DatabaseIfc db, string projectNumber, IfcUnitAssignment.Length length) : base(db, projectNumber, length)
		{
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECT";
		}
		private IfcProject(string projectNumber, IfcUnitAssignment units) : base(projectNumber, units)
		{
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECT";
		}

		public IfcSpatialElement RootElement() { return (mIsDecomposedBy.Count == 0 ? null : mIsDecomposedBy.First().RelatedObjects.First() as IfcSpatialElement);  }
		internal IfcSite getSite() { return (mIsDecomposedBy.Count == 0 ? null : mIsDecomposedBy.First().RelatedObjects.First() as IfcSite); }
		public IfcSite UppermostSite() { return getSite(); }
		public IfcBuilding UppermostBuilding()
		{
			if (mIsDecomposedBy.Count == 0)
				return null;
			BaseClassIfc ent = mIsDecomposedBy.First().mRelatedObjects.First();
			IfcBuilding result = ent as IfcBuilding;
			if (result != null)
				return result;
			IfcSite s = ent as IfcSite;
			if (s != null)
				return s.IsDecomposedBy.SelectMany(x=>x.RelatedObjects).OfType<IfcBuilding>().FirstOrDefault();
			return null;
		}
	}
	[Serializable]
	public partial class IfcProjectedCRS : IfcCoordinateReferenceSystem //IFC4
	{
		internal string mMapProjection = "$";// :	OPTIONAL IfcIdentifier;
		internal string mMapZone = "$";// : OPTIONAL IfcIdentifier 
		internal int mMapUnit = 0;// :	OPTIONAL IfcNamedUnit;

		public string MapProjection { get { return (mMapProjection == "$" ? "" : ParserIfc.Decode(mMapProjection)); } set { mMapProjection = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string MapZone { get { return (mMapZone == "$" ? "" : ParserIfc.Decode(mMapZone)); } set { mMapZone = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcNamedUnit MapUnit { get { return mDatabase[mMapUnit] as IfcNamedUnit; } set { mMapUnit = (value == null ? 0 : value.mIndex); } }

		internal IfcProjectedCRS() : base() { }
		internal IfcProjectedCRS(DatabaseIfc db, IfcProjectedCRS p) : base(db, p) { mName = p.mName; mMapZone = p.mMapZone; if (p.mMapUnit > 0) MapUnit = db.Factory.Duplicate(p.MapUnit) as IfcNamedUnit; }
		public IfcProjectedCRS(DatabaseIfc db, string name) : base(db, name) { }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcProjectionCurve // DEPRECATED IFC4
	[Serializable]
	public partial class IfcProjectionElement : IfcFeatureElementAddition
	{
		internal IfcProjectionElementTypeEnum mPredefinedType = IfcProjectionElementTypeEnum.NOTDEFINED;// :	OPTIONAL IfcProjectionElementTypeEnum; //IFC4
		public IfcProjectionElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		//INVERSE
		internal IfcProjectionElement() : base() { }
		internal IfcProjectionElement(DatabaseIfc db, IfcProjectionElement e, DuplicateOptions options) : base(db, e, options){ mPredefinedType = e.mPredefinedType; }
	}
	[Serializable]
	public partial class IfcProjectLibrary : IfcContext
	{
		internal IfcProjectLibrary() : base() { }
		internal IfcProjectLibrary(DatabaseIfc db, IfcProjectLibrary l, DuplicateOptions options) : base(db, l, options) { }
		public IfcProjectLibrary(DatabaseIfc m, string name) : base(m, name)
		{
			if (m.ModelView == ModelView.Ifc4Reference || m.ModelView == ModelView.Ifc2x3Coordination)
				throw new Exception("Invalid Model View for IfcProjectLibrary : " + m.ModelView.ToString());
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECTLIBRARY";
		}
		public IfcProjectLibrary(DatabaseIfc m, string name, IfcUnitAssignment.Length length)
			: base(m, name) { UnitsInContext = new IfcUnitAssignment(mDatabase).SetUnits(length); }

	}
	[Serializable]
	public partial class IfcProjectOrder : IfcControl
	{
		//internal string mID;// : IfcIdentifier; IFC4 relocated 
		internal IfcProjectOrderTypeEnum mPredefinedType = IfcProjectOrderTypeEnum.NOTDEFINED;// : IfcProjectOrderTypeEnum;
		internal string mStatus = "$";// : OPTIONAL IfcLabel; 
		internal string mLongDescription = "$"; //	 :	OPTIONAL IfcText;
		internal IfcProjectOrder() : base() { }
		internal IfcProjectOrder(DatabaseIfc db, IfcProjectOrder o, DuplicateOptions options) : base(db, o, options) { mPredefinedType = o.mPredefinedType; mStatus = o.mStatus; mLongDescription = o.mLongDescription; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcProjectOrderRecord : IfcControl // DEPRECATED IFC4
	{
		internal List<int> mRecords = new List<int>();// : LIST [1:?] OF UNIQUE IfcRelAssignsToProjectOrder;
		internal IfcProjectOrderRecordTypeEnum mPredefinedType = IfcProjectOrderRecordTypeEnum.NOTDEFINED;// : IfcProjectOrderRecordTypeEnum; 
															   //public List<ifcrelassignstopr>
		internal IfcProjectOrderRecord() : base() { }
		internal IfcProjectOrderRecord(DatabaseIfc db, IfcProjectOrderRecord r, DuplicateOptions options) : base(db, r, options) { }// Records = r.Records mPredefinedType = i.mPredefinedType; }
	}
	[Serializable]
	public abstract partial class IfcProperty : IfcPropertyAbstraction, NamedObjectIfc  //ABSTRACT SUPERTYPE OF (ONEOF(IfcComplexProperty,IfcSimpleProperty));
	{
		internal string mName; //: IfcIdentifier;
		internal string mDescription = "$"; //: OPTIONAL IfcText;
		//INVERSE
		internal List<IfcPropertySet> mPartOfPset = new List<IfcPropertySet>();//	:	SET OF IfcPropertySet FOR HasProperties;
		internal List<IfcPropertyDependencyRelationship> mPropertyForDependance = new List<IfcPropertyDependencyRelationship>();//	:	SET OF IfcPropertyDependencyRelationship FOR DependingProperty;
		internal List<IfcPropertyDependencyRelationship> mPropertyDependsOn = new List<IfcPropertyDependencyRelationship>();//	:	SET OF IfcPropertyDependencyRelationship FOR DependantProperty;
		internal List<IfcComplexProperty> mPartOfComplex = new List<IfcComplexProperty>();//	:	SET OF IfcComplexProperty FOR HasProperties;

		public string Name { get { return ParserIfc.Decode(mName); } set { mName = string.IsNullOrEmpty(value) ? "Unknown" : ParserIfc.Encode(value); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcPropertySet> PartOfPset { get { return new ReadOnlyCollection<IfcPropertySet>(mPartOfPset); } }

		protected IfcProperty() : base() { }
		protected IfcProperty(IfcProperty property) : base(property) { Name = property.Name; Description = property.Description; }
		protected IfcProperty(DatabaseIfc db, IfcProperty p, DuplicateOptions options) : base(db, p, options) { mName = p.mName; mDescription = p.mDescription; }
		protected IfcProperty(DatabaseIfc db, string name) : base(db) { Name = name; }

		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcProperty p = e as IfcProperty;
			if (p == null || string.Compare(mName, p.mName) != 0 || string.Compare(mDescription, p.mDescription) != 0)
				return false;
			return base.isDuplicate(e, tol);
		}

		protected override bool DisposeWorker(bool children)
		{
			if (mPartOfPset.Count > 0)
				return false;
			if (mPartOfComplex.Count > 0)
				return false;
			return base.DisposeWorker(children);
		}
	}
	[Serializable]
	public abstract partial class IfcPropertyAbstraction : BaseClassIfc, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcExtendedProperties ,IfcPreDefinedProperties ,IfcProperty ,IfcPropertyEnumeration));
	{ //INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } set { mHasExternalReference.Clear();  if (value != null) { mHasExternalReference.CollectionChanged -= mHasExternalReference_CollectionChanged; mHasExternalReference = value; mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged; } } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>(mHasConstraintRelationships); } }
		protected IfcPropertyAbstraction() : base() { }
		protected IfcPropertyAbstraction(IfcPropertyAbstraction propertyAbstraction) : base(propertyAbstraction.Database) { }
		protected IfcPropertyAbstraction(DatabaseIfc db) : base(db) { }
		protected IfcPropertyAbstraction(DatabaseIfc db, IfcPropertyAbstraction p, DuplicateOptions options) : base(db, p) { }
		protected override void initialize()
		{
			base.initialize();
			mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged;
		}
		private void mHasExternalReference_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.NewItems)
				{
					if (!r.RelatedResourceObjects.Contains(this))
						r.RelatedResourceObjects.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.OldItems)
					r.RelatedResourceObjects.Remove(this);
			}
		}
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	[Serializable]
	public partial class IfcPropertyBoundedValue : IfcSimpleProperty
	{
		internal IfcValue mUpperBoundValue;// : OPTIONAL IfcValue;
		internal IfcValue mLowerBoundValue;// : OPTIONAL IfcValue;   
		internal int mUnit;// : OPTIONAL IfcUnit;
		internal IfcValue mSetPointValue;// 	OPTIONAL IfcValue; IFC4

		public IfcValue UpperBoundValue { get { return mUpperBoundValue; } set { mUpperBoundValue = value; } }
		public IfcValue LowerBoundValue { get { return mLowerBoundValue; } set { mLowerBoundValue = value; } }
		public IfcUnit Unit { get { return mDatabase[mUnit] as IfcUnit; } set { mUnit = (value == null ? 0 : value.Index); } }
		public IfcValue SetPointValue { get { return mSetPointValue; } set { mSetPointValue = value; } }

		internal IfcPropertyBoundedValue() : base() { }
		internal IfcPropertyBoundedValue(DatabaseIfc db, IfcPropertyBoundedValue p, DuplicateOptions options) : base(db, p, options)
		{
			mUpperBoundValue = p.mUpperBoundValue;
			mLowerBoundValue = p.mLowerBoundValue;
			if (p.mUnit > 0)
				Unit = mDatabase.Factory.Duplicate(p.mDatabase[p.mUnit]) as IfcUnit;
			mSetPointValue = p.mSetPointValue;
		}
		public IfcPropertyBoundedValue(DatabaseIfc db, string name) : base(db, name) {  }
	}
	[Serializable]
	public partial class IfcPropertyBoundedValue<T> : IfcSimpleProperty where T : IfcValue
	{
		public override string StepClassName { get { return "IfcPropertyBoundedValue"; } }
		internal T mUpperBoundValue;// : OPTIONAL IfcValue;
		internal T mLowerBoundValue;// : OPTIONAL IfcValue;   
		internal int mUnit;// : OPTIONAL IfcUnit;
		internal T mSetPointValue;// 	OPTIONAL IfcValue; IFC4

		public T UpperBoundValue { get { return mUpperBoundValue; } set { mUpperBoundValue = value; } }
		public T LowerBoundValue { get { return mUpperBoundValue; } set { mUpperBoundValue = value; } }
		public IfcUnit Unit { get { return mDatabase[mUnit] as IfcUnit; } set { mUnit = (value == null ? 0 : value.Index); } }
		public T SetPointValue { get { return mSetPointValue; } set { mSetPointValue = value; } }

		internal IfcPropertyBoundedValue() : base() { }
		public IfcPropertyBoundedValue(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Serializable]
	public abstract partial class IfcPropertyDefinition : IfcRoot, IfcDefinitionSelect //(IfcPropertySetDefinition, IfcPropertyTemplateDefinition)
	{ //INVERSE
		internal IfcRelDeclares mHasContext = null;// :	SET [0:1] OF IfcRelDeclares FOR RelatedDefinitions;
		internal SET<IfcRelAssociates> mHasAssociations = new SET<IfcRelAssociates>();//	 : 	SET OF IfcRelAssociates FOR RelatedObjects;

		public IfcRelDeclares HasContext { get { return mHasContext; } set { mHasContext = value; } }
		public SET<IfcRelAssociates> HasAssociations { get { return mHasAssociations; } }

		protected IfcPropertyDefinition() : base() { }
		internal IfcPropertyDefinition(DatabaseIfc db) : base(db) { }
		protected IfcPropertyDefinition(IfcPropertyDefinition propertyDefinition) : base(propertyDefinition, false) {  }
		protected IfcPropertyDefinition(DatabaseIfc db, IfcPropertyDefinition p, DuplicateOptions options) : base(db, p, options)
		{
			foreach (IfcRelAssociates associates in mHasAssociations)
			{
				IfcRelAssociates dup = db.Factory.Duplicate(associates, options) as IfcRelAssociates;
				dup.RelatedObjects.Add(this);
			}
		}
		protected override void initialize()
		{
			base.initialize();

			mHasAssociations.CollectionChanged += mHasAssociations_CollectionChanged;
		}
		private void mHasAssociations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelAssociates r in e.NewItems)
				{
					if (!r.RelatedObjects.Contains(this))
						r.RelatedObjects.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelAssociates r in e.OldItems)
				{
					r.RelatedObjects.Remove(this);
				}
			}
		}
	}
	[Serializable]
	public partial class IfcPropertyDependencyRelationship : IfcResourceLevelRelationship
	{
		internal int mDependingProperty;// : IfcProperty;
		internal int mDependantProperty;// : IfcProperty;   
		internal string mExpression = "$";// : OPTIONAL IfcText;

		public IfcProperty DependingProperty { get { return mDatabase[mDependingProperty] as IfcProperty; } set { mDependingProperty = value.mIndex; value.mPropertyDependsOn.Add(this); } }
		public IfcProperty DependantProperty { get { return mDatabase[mDependantProperty] as IfcProperty; } set { mDependantProperty = value.mIndex; value.mPropertyForDependance.Add(this); } }
		public string Expression { get { return (mExpression == "$" ? "" : ParserIfc.Decode(mExpression)); } set { mExpression = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcPropertyDependencyRelationship() : base() { }
		internal IfcPropertyDependencyRelationship(DatabaseIfc db, IfcPropertyDependencyRelationship p) : base(db, p)
		{
			DependingProperty = db.Factory.Duplicate(p.DependingProperty) as IfcProperty;
			DependantProperty = db.Factory.Duplicate(p.DependantProperty) as IfcProperty;
			mExpression = p.mExpression;
		}
		internal IfcPropertyDependencyRelationship(IfcProperty depending, IfcProperty dependant)
			: base(depending.mDatabase) { DependingProperty = depending; DependantProperty = dependant; }
	}
	[Serializable]
	public partial class IfcPropertyEnumeratedValue : IfcSimpleProperty
	{
		internal LIST<IfcValue> mEnumerationValues = new LIST<IfcValue>();// : LIST [1:?] OF IfcValue;
		internal int mEnumerationReference;// : OPTIONAL IfcPropertyEnumeration;   

		public LIST<IfcValue> EnumerationValues { get { return mEnumerationValues; } set { mEnumerationValues = value; } }
		public IfcPropertyEnumeration EnumerationReference { get { return mDatabase[mEnumerationReference] as IfcPropertyEnumeration; } set { mEnumerationReference = value == null ? 0 : value.mIndex; } }

		internal IfcPropertyEnumeratedValue() : base() { }
		public IfcPropertyEnumeratedValue(DatabaseIfc db, string name, IfcValue value) : base(db, name) { mEnumerationValues.Add(value); }
		public IfcPropertyEnumeratedValue(DatabaseIfc db, string name, IEnumerable<IfcValue> values) : base(db, name) { mEnumerationValues.AddRange(values); }
		public IfcPropertyEnumeratedValue(string name, IfcValue value, IfcPropertyEnumeration reference) : base(reference.mDatabase, name) { mEnumerationValues.Add(value); EnumerationReference = reference; }
		public IfcPropertyEnumeratedValue(string name, IEnumerable<IfcValue> values, IfcPropertyEnumeration reference) : base(reference.mDatabase, name) { mEnumerationValues.AddRange(values); EnumerationReference = reference; }
	}
	[Serializable]
	public partial class IfcPropertyEnumeration : IfcPropertyAbstraction, NamedObjectIfc
	{
		internal string mName;//	 :	IfcLabel;
		internal List<IfcValue> mEnumerationValues = new List<IfcValue>();// :	LIST [1:?] OF UNIQUE IfcValue
		internal int mUnit; //	 :	OPTIONAL IfcUnit;

		public string Name { get { return ParserIfc.Decode(mName); } set { mName = (string.IsNullOrEmpty(value) ? "Unknown" : ParserIfc.Encode(value)); } }
		public IfcUnit Unit { get { return mDatabase[mUnit] as IfcUnit; } set { mUnit = (value == null ? 0 : value.Index); } }

		internal IfcPropertyEnumeration() : base() { }
		internal IfcPropertyEnumeration(DatabaseIfc db, IfcPropertyEnumeration p, DuplicateOptions options) : base(db, p, options)
		{
			mName = p.mName;
			mEnumerationValues.AddRange(p.mEnumerationValues);
			if (p.mUnit > 0)
				Unit = db.Factory.Duplicate(p.mDatabase[p.mUnit], options) as IfcUnit;
		}
		public IfcPropertyEnumeration(DatabaseIfc db, string name, IEnumerable<IfcValue> values) : base(db) { Name = name;  mEnumerationValues.AddRange(values); }
	}
	[Serializable]
	public partial class IfcPropertyListValue : IfcSimpleProperty
	{
		private List<IfcValue> mNominalValue = new List<IfcValue>();// :	OPTIONAL LIST [1:?] OF IfcValue;
		private int mUnit;// : OPTIONAL IfcUnit; 

		public ReadOnlyCollection<IfcValue> NominalValue { get { return new ReadOnlyCollection<IfcValue>(mNominalValue); } }
		public IfcUnit Unit { get { return mDatabase[mUnit] as IfcUnit; } set { mUnit = value == null ? 0 : value.Index; } }

		internal IfcPropertyListValue() : base() { }
		internal IfcPropertyListValue(DatabaseIfc db, IfcPropertyListValue p, DuplicateOptions options) : base(db, p, options)
		{
			mNominalValue.AddRange(p.mNominalValue);
			if (p.mUnit > 0)
				Unit = db.Factory.Duplicate(p.mDatabase[p.mUnit], options) as IfcUnit;
		}
		public IfcPropertyListValue(DatabaseIfc db, string name, IEnumerable<IfcValue> values)
			: base(db, name) { mNominalValue.AddRange(values); }
	}
	[Serializable]
	public partial class IfcPropertyListValue<T> : IfcSimpleProperty where T : IfcValue
	{
		private List<T> mNominalValue = new List<T>();// :	OPTIONAL LIST [1:?] OF IfcValue;
		private int mUnit;// : OPTIONAL IfcUnit; 

		public ReadOnlyCollection<T> NominalValue { get { return new ReadOnlyCollection<T>(mNominalValue); } }
		public IfcUnit Unit { get { return mDatabase[mUnit] as IfcUnit; } set { mUnit = value == null ? 0 : value.Index; } }

		internal IfcPropertyListValue() : base() { }
		public IfcPropertyListValue(DatabaseIfc db, string name, List<T> values)
			: base(db, name) { mNominalValue.AddRange(values); }
	}
	[Serializable]
	public partial class IfcPropertyReferenceValue : IfcSimpleProperty
	{
		internal string mUsageName = "$";// 	 :	OPTIONAL IfcText;
		internal int mPropertyReference = 0;// 	 :	OPTIONAL IfcObjectReferenceSelect;

		public string UsageName { get { return (mUsageName == "$" ? "" : ParserIfc.Decode(mUsageName)); } set { mUsageName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcObjectReferenceSelect PropertyReference { get { return mDatabase[mPropertyReference] as IfcObjectReferenceSelect; } set { mPropertyReference = (value == null ? 0 : value.Index); } }

		internal IfcPropertyReferenceValue() : base() { }
		internal IfcPropertyReferenceValue(DatabaseIfc db, IfcPropertyReferenceValue p, DuplicateOptions options) : base(db, p, options)
		{
			mUsageName = p.mUsageName;
			if(p.mPropertyReference > 0)
				PropertyReference = db.Factory.Duplicate(p.PropertyReference as BaseClassIfc, options) as IfcObjectReferenceSelect;
		}
		public IfcPropertyReferenceValue(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPropertyReferenceValue(string name, IfcObjectReferenceSelect obj) : base(obj.Database, name) { PropertyReference = obj; }
	}
	[Serializable]
	public partial class IfcPropertySet : IfcPropertySetDefinition
	{
		public override string StepClassName { get { return "IfcPropertySet"; } }
		private Dictionary<string, IfcProperty> mHasProperties = new Dictionary<string, IfcProperty>();// : SET [1:?] OF IfcProperty;
		public Dictionary<string, IfcProperty> HasProperties { get { return mHasProperties; } }

		internal IfcPropertySet() : base() { }
		protected IfcPropertySet(IfcObjectDefinition obj) : base(obj.mDatabase,"") { Name = this.GetType().Name; new IfcRelDefinesByProperties(obj, this); }
		protected IfcPropertySet(IfcTypeObject type) : base(type.mDatabase,"") { Name = this.GetType().Name; type.HasPropertySets.Add(this); }
		public IfcPropertySet(IfcPropertySet pset) : base(pset)
		{
			foreach (IfcProperty property in pset.HasProperties.Values)
				HasProperties[property.Name] = property;
		}
		internal IfcPropertySet(DatabaseIfc db, IfcPropertySet s, DuplicateOptions options) : base(db, s, options)
		{
			foreach (IfcProperty p in s.HasProperties.Values)
			{
				if(!options.IgnoredPropertyNames.Contains(p.Name))
					addProperty(db.Factory.Duplicate(p), 1e-5);
			}
		}
		public IfcPropertySet(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPropertySet(IfcObjectDefinition relatedObject, string name) : base(relatedObject, name) { }
		public IfcPropertySet(string name, IfcProperty property) : base(property.mDatabase, name) { addProperty(property); }
		public IfcPropertySet(string name, params IfcProperty[] properties)
		: base(properties.First().Database, name) { foreach (IfcProperty p in properties) addProperty(p); }
		public IfcPropertySet(string name, IEnumerable<IfcProperty> properties) : base(properties.First().mDatabase, name)
		{
			foreach (IfcProperty p in properties)
				addProperty(p);
		}
		public IfcPropertySet(IfcObjectDefinition relatedObject, string name, IfcProperty prop) : base(relatedObject, name) { addProperty(prop); }
		public IfcPropertySet(IfcObjectDefinition relatedObject, string name, IEnumerable<IfcProperty> props) : base(relatedObject, name) { foreach(IfcProperty p in props) addProperty(p);  }
		public IfcPropertySet(IfcObjectDefinition relatedObject, string name, params IfcProperty[] properties)
			: base(relatedObject, name) { foreach (IfcProperty p in properties) addProperty(p); }
		public IfcPropertySet(IfcPropertySetTemplate template, IEnumerable<IfcProperty> properties) 
			: this(template.Name, properties) { Description = template.Description; }
		
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcProperty p in mHasProperties.Values)
				result.AddRange(p.Extract<T>());
			return result;
		}
		internal override bool isEmpty { get { return mHasProperties.Count == 0; } }
		public IfcPropertyBoundedValue AddProperty(IfcPropertyBoundedValue property, double tol) { addProperty(property, tol); return property; }
		public IfcPropertyEnumeratedValue AddProperty(IfcPropertyEnumeratedValue property) { addProperty(property, 1e-5); return property; }
		public IfcPropertyReferenceValue AddProperty(IfcPropertyReferenceValue property) { addProperty(property, 1e-5); return property; }
		public IfcPropertySingleValue AddProperty(IfcPropertySingleValue property) { addProperty(property, 1e-5); return property; }
		public IfcPropertySingleValue AddProperty(IfcPropertySingleValue property, double tol) { addProperty(property, tol); return property; }
		public IfcPropertyTableValue AddProperty(IfcPropertyTableValue property, double tol) { addProperty(property, tol); return property; }
		internal void addProperty(IfcProperty property, double tol)
		{
			if (property == null)
				return;
			IfcProperty existing = null;
			if (mHasProperties.TryGetValue(property.Name, out existing))
			{
				if (property.isDuplicate(existing, tol))
					return;
				existing.mPartOfPset.Remove(this);
			}
			mHasProperties[property.Name] = property;
			property.mPartOfPset.Add(this);
		}
		internal void addProperty(IfcProperty property)
		{
			if (property == null)
				return;
			mHasProperties[property.Name] = property;
			property.mPartOfPset.Add(this);
		}
		public void RemoveProperty(IfcProperty property)
		{
			if (property != null)
			{
				mHasProperties.Remove(property.Name);
				property.mPartOfPset.Remove(this);
			}
		}

		public IfcProperty FindProperty(string name)
		{
			IfcProperty result = this[name];
			if (result != null)
				return result;
			foreach(IfcComplexProperty complexProperty in mHasProperties.OfType<IfcComplexProperty>())
			{
				result = complexProperty.FindProperty(name);
				if (result != null)
					return result;
			}
			return null;
		}
		public IfcProperty this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
					return null;
				IfcProperty result = null;
				mHasProperties.TryGetValue(name, out result);
					return result;
			}
			set
			{
				if (string.IsNullOrEmpty(name))
					return;
				mHasProperties[name] = value;
			}
		}
		public void SetProperties(IEnumerable<IfcProperty> properties)
		{
			mHasProperties.Clear();
			foreach (IfcProperty property in properties)
				addProperty(property);
		}
		protected override bool DisposeWorker(bool children)
		{
			if (children)
			{
				foreach (IfcProperty p in HasProperties.Values)
				{
					p.mPartOfPset.Remove(this);
					if (children)
						p.Dispose(true);
				}
			}
			return base.DisposeWorker(children);
		}
		internal override List<IBaseClassIfc> retrieveReference(IfcReference r)
		{
			IfcReference ir = r.InnerReference;
			List<IBaseClassIfc> result = new List<IBaseClassIfc>();
			if (ir == null)
			{
				if (string.Compare(r.mAttributeIdentifier, "HasProperties", true) == 0)
				{
					if (r.mListPositions.Count == 0)
					{
						string name = r.InstanceName;

						if (!string.IsNullOrEmpty(name))
						{
							if (mHasProperties.ContainsKey(name))
								result.Add(mHasProperties[name]);
						}
						else
							result.AddRange(mHasProperties.Values);
					}
					return result;
				}
			}
			if (string.Compare(r.mAttributeIdentifier, "HasProperties", true) == 0)
			{
				if (r.mListPositions.Count == 0)
				{
					string name = r.InstanceName;

					if (string.IsNullOrEmpty(name))
					{
						foreach (IfcProperty p in mHasProperties.Values)
							result.AddRange(p.retrieveReference(r.InnerReference));
					}
					else
					{
						if(mHasProperties.ContainsKey(name))
							result.AddRange(mHasProperties[name].retrieveReference(r.InnerReference));
					}
				}
				
				return result;
			}
			return base.retrieveReference(r);
		}
	}
	[Serializable]
	public abstract partial class IfcPropertySetDefinition : IfcPropertyDefinition, IfcPropertySetDefinitionSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcElementQuantity,IfcEnergyProperties ,
	{ // IfcFluidFlowProperties,IfcPropertySet, IfcServiceLifeFactor, IfcSoundProperties ,IfcSoundValue ,IfcSpaceThermalLoadProperties ))
		//INVERSE
		internal SET<IfcTypeObject> mDefinesType = new SET<IfcTypeObject>();// :	SET OF IfcTypeObject FOR HasPropertySets; IFC4change
		internal List<IfcRelDefinesByTemplate> mIsDefinedBy = new List<IfcRelDefinesByTemplate>();//IsDefinedBy	 :	SET OF IfcRelDefinesByTemplate FOR RelatedPropertySets;
		private SET<IfcRelDefinesByProperties> mDefinesOccurrence = new SET<IfcRelDefinesByProperties>(); //:	SET [0:1] OF IfcRelDefinesByProperties FOR RelatingPropertyDefinition;
		
		public SET<IfcTypeObject> DefinesType { get { return mDefinesType; } }
		public ReadOnlyCollection<IfcRelDefinesByTemplate> IsDefinedBy { get { return new ReadOnlyCollection<IfcRelDefinesByTemplate>(mIsDefinedBy); } }
		public SET<IfcRelDefinesByProperties> DefinesOccurrence { get { return mDefinesOccurrence; } }

		protected IfcPropertySetDefinition() : base() { }
		protected IfcPropertySetDefinition(IfcPropertySetDefinition pset) : base(pset) { }
		protected IfcPropertySetDefinition(DatabaseIfc db, IfcPropertySetDefinition s, DuplicateOptions options) : base(db, s, options) { }
		protected IfcPropertySetDefinition(DatabaseIfc m, string name) : base(m) { Name = name; }
		protected IfcPropertySetDefinition(IfcObjectDefinition relatedObject, string name) : base(relatedObject.mDatabase) { RelateObjectDefinition(relatedObject); Name = name; }

		public void RelateObjectDefinition(IfcObjectDefinition relatedObject)
		{
			IfcTypeObject to = relatedObject as IfcTypeObject;
			if (to != null)
				to.HasPropertySets.Add(this);
			else
			{
				if (mDefinesOccurrence.Count == 0)
					new IfcRelDefinesByProperties(relatedObject, this);
				else
					mDefinesOccurrence.First().RelatedObjects.Add(relatedObject);
			}
		}

		internal bool IsInstancePropertySet
		{
			get
			{
				foreach (IfcRelDefinesByTemplate dbt in mIsDefinedBy)
				{
					if (dbt.RelatingTemplate.TemplateType == IfcPropertySetTemplateTypeEnum.PSET_OCCURRENCEDRIVEN)
						return true;
				}
				//Check context declared
				return false;
			}
		}
		internal virtual bool isEmpty { get { return false; } }
	}

	public interface IfcPropertySetDefinitionSelect : IBaseClassIfc { }// = SELECT ( IfcPropertySetDefinitionSet,  IfcPropertySetDefinition);
																	   //  IfcPropertySetDefinitionSet
	[Serializable]
	public partial class IfcPropertySetTemplate : IfcPropertyTemplateDefinition
	{
		internal IfcPropertySetTemplateTypeEnum mTemplateType = Ifc.IfcPropertySetTemplateTypeEnum.NOTDEFINED;//	:	OPTIONAL IfcPropertySetTemplateTypeEnum;
		private string mApplicableEntity = "$";//	:	OPTIONAL IfcIdentifier;
		private Dictionary<string, IfcPropertyTemplate> mHasPropertyTemplates = new Dictionary<string, IfcPropertyTemplate>();//  : SET [1:?] OF IfcPropertyTemplate;

		//INVERSE
		internal SET<IfcRelDefinesByTemplate> mDefines = new SET<IfcRelDefinesByTemplate>();//	:	SET OF IfcRelDefinesByTemplate FOR RelatingTemplate;

		private ApplicableFilter mApplicableFilter = null;

		public IfcPropertySetTemplateTypeEnum TemplateType { get { return mTemplateType; } set { mTemplateType = value; } }
		public string ApplicableEntity
		{
			get { return (mApplicableEntity == "$" ? "" : ParserIfc.Decode(mApplicableEntity)); }
			set
			{
				mApplicableEntity = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value));
				mApplicableFilter = null;
			}
		}
		public Dictionary<string, IfcPropertyTemplate> HasPropertyTemplates { get { return mHasPropertyTemplates; } }
		public SET<IfcRelDefinesByTemplate> Defines { get { return mDefines; } }

		protected override void initialize()
		{
			base.initialize();
			mHasPropertyTemplates = new Dictionary<string, IfcPropertyTemplate>();
		}

		internal IfcPropertySetTemplate() : base() { }
		public IfcPropertySetTemplate(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPropertySetTemplate(DatabaseIfc db, IfcPropertySetTemplate p, DuplicateOptions options) : base(db, p, options)
		{
			mTemplateType = p.mTemplateType;
			mApplicableEntity = p.mApplicableEntity;
			if(options.DuplicateDownstream)
				p.HasPropertyTemplates.Values.ToList().ForEach(x => AddPropertyTemplate(db.Factory.Duplicate(x, options) as IfcPropertyTemplate));
		}
		public IfcPropertySetTemplate(string name, IfcPropertyTemplate propertyTemplate) : this(propertyTemplate.mDatabase, name) { AddPropertyTemplate(propertyTemplate); }
		public IfcPropertySetTemplate(string name, IEnumerable<IfcPropertyTemplate> propertyTemplates) : base(propertyTemplates.First().mDatabase, name) { foreach(IfcPropertyTemplate propertyTemplate in propertyTemplates) AddPropertyTemplate(propertyTemplate); }
		
		public void AddPropertyTemplate(IfcPropertyTemplate template) { mHasPropertyTemplates[template.Name] = template; template.mPartOfPsetTemplate.Add(this); }
		public IfcPropertyTemplate this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
					return null;
				IfcPropertyTemplate result = null;
				mHasPropertyTemplates.TryGetValue(name, out result);
				return result;
			}
		}
		public void Remove(string templateName) { IfcPropertyTemplate template = this[templateName]; if (template != null) { template.mPartOfPsetTemplate.Remove(this);  mHasPropertyTemplates.Remove(templateName); } }
		public bool IsApplicable(IfcObjectDefinition obj)
		{
			if (mApplicableFilter == null)
				mApplicableFilter = new ApplicableFilter(ApplicableEntity);
			return mApplicableFilter.IsApplicable(obj);
		}
		public bool IsApplicable(Type typeIfc,string predefined)
		{
			if (mApplicableFilter == null)
				mApplicableFilter = new ApplicableFilter(ApplicableEntity);

			return mApplicableFilter.IsApplicable(typeIfc, predefined);
		}
		
	}
	[Serializable]
	public partial class IfcPropertySingleValue : IfcSimpleProperty
	{
		private IfcValue mNominalValue;// : OPTIONAL IfcValue; 
		private IfcUnit mUnit;// : OPTIONAL IfcUnit; 

		public IfcValue NominalValue { get { return mNominalValue; } set { mNominalValue = value; } }
		public IfcUnit Unit { get { return mUnit as IfcUnit; } set { mUnit = value; } }

		internal string mVal;
		internal IfcPropertySingleValue() : base() { }

		public IfcPropertySingleValue(IfcPropertySingleValue propertySingleValue) : base(propertySingleValue)
		{
			NominalValue = propertySingleValue.NominalValue;
			Unit = propertySingleValue.Unit;
		}
		internal IfcPropertySingleValue(DatabaseIfc db, IfcPropertySingleValue s, DuplicateOptions options) : base(db, s, options)
		{
			mNominalValue = s.mNominalValue;
			if (s.mUnit != null)
				Unit = db.Factory.Duplicate(s.mUnit as BaseClassIfc, options) as IfcUnit;
		}
		public IfcPropertySingleValue(DatabaseIfc m, string name, IfcValue value) : base(m, name) { mNominalValue = value; }
		public IfcPropertySingleValue(DatabaseIfc m, string name, bool value) : this(m, name, new IfcBoolean(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, int value) : this(m, name, new IfcInteger(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, double value) : this(m, name, new IfcReal(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, string value) : this(m, name, new IfcLabel(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, IfcValue val, IfcUnit unit) : this(m, name, val) {  Unit = unit; }

		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcPropertySingleValue psv = e as IfcPropertySingleValue;
			if (psv == null || psv.mUnit != mUnit)
				return false;
			if (base.isDuplicate(e, tol))
			{
				if (mNominalValue != null)
				{
					if (psv.mNominalValue != null)
					{
						if (mNominalValue.GetType() != psv.mNominalValue.GetType() || string.Compare(mNominalValue.Value.ToString(), psv.mNominalValue.Value.ToString()) != 0)
							return false;
					}
					else
						return false;
				}
				else if (string.Compare(mVal, psv.mVal) != 0)
					return false;
				return true;
			}
			return false;
		}

		public bool SetValueOrDuplicate(IfcValue val, IfcObjectDefinition obj)
		{
			IfcPropertySet pset = (mPartOfPset.Count == 1 ? mPartOfPset[0] : null);
			if(pset != null && mPartOfComplex.Count == 0)
			{
				int count = pset.DefinesOccurrence.SelectMany(x => x.RelatedObjects).Count() + pset.DefinesType.Count;
				if (count == 1)
				{
					NominalValue = val;
					return true;
				}
			}
			foreach(IfcPropertySet propertySet in mPartOfPset)
			{
				IfcRelDefinesByProperties defines = propertySet.DefinesOccurrence.Where(x => x.RelatedObjects.Contains(obj)).FirstOrDefault();
				if(defines != null)
				{
					IfcPropertySet duplicate = new IfcPropertySet(pset);
					duplicate[Name] = new IfcPropertySingleValue(this) { NominalValue = val };
					defines.RelatedObjects.Remove(obj);
					duplicate.RelateObjectDefinition(obj);
					return true;
				}
			}
			return false;

		}

		public int ValueStringCompare(string value, bool ignoreCase)
		{
			if (NominalValue == null)
				return -1;
			string valueString = NominalValue.ValueString;
			if (string.IsNullOrEmpty(valueString))
				return -1;
			return string.Compare(valueString, value, ignoreCase);
		}
		public bool ValueStringStartsWith(string value)
		{
			if (NominalValue == null)
				return false;
			string valueString = NominalValue.ValueString;
			if (string.IsNullOrEmpty(valueString))
				return false;
			return valueString.StartsWith(value);
		}
	}

	[Serializable]
	public partial class IfcPropertyTableValue<T,U> : IfcSimpleProperty where T : IfcValue where U :IfcValue
	{
		internal List <T> mDefiningValues = new List<T>();//	 :	OPTIONAL LIST [1:?] OF UNIQUE IfcValue;
		internal List<U> mDefinedValues = new List<U>();//	 :	OPTIONAL LIST [1:?] OF IfcValue;
		internal string mExpression = "$";// ::	OPTIONAL IfcText; 
		internal int mDefiningUnit;// : :	OPTIONAL IfcUnit;   
		internal int mDefinedUnit;// : :	OPTIONAL IfcUnit;
		internal IfcCurveInterpolationEnum mCurveInterpolation = IfcCurveInterpolationEnum.NOTDEFINED;// : :	OPTIONAL IfcCurveInterpolationEnum; 

		internal IfcPropertyTableValue() : base() { }
		internal IfcPropertyTableValue(DatabaseIfc db, IfcPropertyTableValue p, DuplicateOptions options) : base(db, p, options)
		{
#warning todo
			//			mDefiningValues.AddRange(p.DefiningValues);
			//		DefinedValues.AddRange(p.DefinedValues);//.ToArray()); mExpression = p.mExpression; mDefiningUnit = p.mDefiningUnit; mDefinedUnit = p.mDefinedUnit; mCurveInterpolation = p.mCurveInterpolation; 
		}
		public IfcPropertyTableValue(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Serializable]
	public partial class IfcPropertyTableValue : IfcSimpleProperty
	{
		internal List<IfcValue> mDefiningValues = new List<IfcValue>();//	 :	OPTIONAL LIST [1:?] OF UNIQUE IfcValue;
		internal List<IfcValue> mDefinedValues = new List<IfcValue>();//	 :	OPTIONAL LIST [1:?] OF IfcValue;
		internal string mExpression = "$";// ::	OPTIONAL IfcText; 
		internal IfcUnit mDefiningUnit = null;// : :	OPTIONAL IfcUnit;   
		internal IfcUnit mDefinedUnit = null;// : :	OPTIONAL IfcUnit;
		internal IfcCurveInterpolationEnum mCurveInterpolation = IfcCurveInterpolationEnum.NOTDEFINED;// : :	OPTIONAL IfcCurveInterpolationEnum; 

		internal IfcPropertyTableValue() : base() { }
		internal IfcPropertyTableValue(DatabaseIfc db, IfcPropertyTableValue p, DuplicateOptions options) : base(db, p, options)
		{
			mDefiningValues.AddRange(p.mDefiningValues);
			mDefinedValues.AddRange(p.mDefinedValues);
			mExpression = p.mExpression;
			mDefiningUnit = db.Factory.Duplicate(p.mDefiningUnit) as IfcUnit;
			mDefinedUnit = db.Factory.Duplicate(p.mDefinedUnit) as IfcUnit;
			//.ToArray()); mExpression = p.mExpression; mDefiningUnit = p.mDefiningUnit; mDefinedUnit = p.mDefinedUnit; mCurveInterpolation = p.mCurveInterpolation; 
		}
		public IfcPropertyTableValue(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Serializable]
	public abstract partial class IfcPropertyTemplate : IfcPropertyTemplateDefinition    //ABSTRACT SUPERTYPE OF(ONEOF(IfcComplexPropertyTemplate, IfcSimplePropertyTemplate))
	{   //INVERSE
		internal SET<IfcComplexPropertyTemplate> mPartOfComplexTemplate = new SET<IfcComplexPropertyTemplate>();//	:	SET OF IfcComplexPropertyTemplate FOR HasPropertyTemplates;
		internal SET<IfcPropertySetTemplate> mPartOfPsetTemplate = new SET<IfcPropertySetTemplate>();//	:	SET OF IfcPropertySetTemplate FOR HasPropertyTemplates;
		public SET<IfcComplexPropertyTemplate> PartOfComplexTemplate { get { return mPartOfComplexTemplate; } } 
		public SET<IfcPropertySetTemplate> PartOfPsetTemplate { get { return mPartOfPsetTemplate; } } 

		protected IfcPropertyTemplate() : base() { }
		protected IfcPropertyTemplate(DatabaseIfc db, IfcPropertyTemplate t, DuplicateOptions options) : base(db, t, options) { }
		protected IfcPropertyTemplate(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Serializable]
	public abstract partial class IfcPropertyTemplateDefinition : IfcPropertyDefinition // ABSTRACT SUPERTYPE OF(ONEOF(IfcPropertySetTemplate, IfcPropertyTemplate))
	{ 
	 	protected IfcPropertyTemplateDefinition() : base() { }
		protected IfcPropertyTemplateDefinition(DatabaseIfc db, IfcPropertyTemplateDefinition p, DuplicateOptions options) : base(db, p, options) { }
		protected IfcPropertyTemplateDefinition(DatabaseIfc m, string name) : base(m) { Name = name; }
	}
	[Serializable]
	public partial class IfcProtectiveDevice : IfcFlowController //IFC4
	{
		internal IfcProtectiveDeviceTypeEnum mPredefinedType = IfcProtectiveDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcProtectiveDeviceTypeEnum;
		public IfcProtectiveDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDevice() : base() { }
		internal IfcProtectiveDevice(DatabaseIfc db, IfcProtectiveDevice d, DuplicateOptions options) : base(db, d, options) { mPredefinedType = d.mPredefinedType; }
		public IfcProtectiveDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcProtectiveDeviceTrippingUnit : IfcDistributionControlElement //IFC4  
	{
		internal IfcProtectiveDeviceTrippingUnitTypeEnum mPredefinedType = IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED;
		public IfcProtectiveDeviceTrippingUnitTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceTrippingUnit() : base() { }
		internal IfcProtectiveDeviceTrippingUnit(DatabaseIfc db, IfcProtectiveDeviceTrippingUnit u, DuplicateOptions options) : base(db,u, options) { mPredefinedType = u.mPredefinedType; }
		public IfcProtectiveDeviceTrippingUnit(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcProtectiveDeviceTrippingUnitType : IfcDistributionControlElementType
	{
		internal IfcProtectiveDeviceTrippingUnitTypeEnum mPredefinedType = IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED;// : IfcProtectiveDeviceTrippingUnitTypeEnum;
		public IfcProtectiveDeviceTrippingUnitTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceTrippingUnitType() : base() { }
		internal IfcProtectiveDeviceTrippingUnitType(DatabaseIfc db, IfcProtectiveDeviceTrippingUnitType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcProtectiveDeviceTrippingUnitType(DatabaseIfc m, string name, IfcProtectiveDeviceTrippingUnitTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcProtectiveDeviceType : IfcFlowControllerType
	{
		internal IfcProtectiveDeviceTypeEnum mPredefinedType = IfcProtectiveDeviceTypeEnum.NOTDEFINED;// : IfcProtectiveDeviceTypeEnum; 
		public IfcProtectiveDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceType() : base() { }
		internal IfcProtectiveDeviceType(DatabaseIfc db, IfcProtectiveDeviceType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcProtectiveDeviceType(DatabaseIfc m, string name, IfcProtectiveDeviceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcProxy : IfcProduct
	{
		internal IfcObjectTypeEnum mProxyType;// : IfcObjectTypeEnum;
		internal string mTag = "$";// : OPTIONAL IfcLabel;

		public IfcObjectTypeEnum ProxyType { get { return mProxyType; } set { mProxyType = value; } }
		public string Tag { get { return mTag == "$" ? "" : ParserIfc.Decode(mTag); } set { mTag = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value); } }

		internal IfcProxy() : base() { }
		internal IfcProxy(DatabaseIfc db, IfcProxy p, DuplicateOptions options) : base(db, p, options)
		{
			mProxyType = p.mProxyType;
			mTag = p.mTag;

			if (p.mContainedInStructure != null)
			{
				IfcRelContainedInSpatialStructure rcss = db.Factory.Duplicate(p.mContainedInStructure, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcRelContainedInSpatialStructure;
				rcss.RelatedElements.Add(this);
			}
		}
	}
	[Serializable]
	public partial class IfcPump : IfcFlowMovingDevice //IFC4
	{
		internal IfcPumpTypeEnum mPredefinedType = IfcPumpTypeEnum.NOTDEFINED;// OPTIONAL : IfcPumpTypeEnum;
		public IfcPumpTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPump() : base() { }
		internal IfcPump(DatabaseIfc db, IfcPump p, DuplicateOptions options) : base(db,p, options) { mPredefinedType = p.mPredefinedType; }
		public IfcPump(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcPumpType : IfcFlowMovingDeviceType
	{
		internal IfcPumpTypeEnum mPredefinedType = IfcPumpTypeEnum.NOTDEFINED;// : IfcPumpTypeEnum; 
		public IfcPumpTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPumpType() : base() { }
		internal IfcPumpType(DatabaseIfc db, IfcPumpType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcPumpType(DatabaseIfc m, string name, IfcPumpTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
}
