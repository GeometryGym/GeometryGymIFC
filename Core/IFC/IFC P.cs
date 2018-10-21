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
		protected IfcParameterizedProfileDef(DatabaseIfc db, IfcParameterizedProfileDef p) : base(db, p)
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
		internal override void changeSchema(ReleaseVersion schema)
		{
			IfcAxis2Placement2D position = Position;
			if (schema < ReleaseVersion.IFC4)
			{
				if (position == null)
					Position = mDatabase.Factory.Origin2dPlace;
			}
			else if (position != null && position.IsXYPlane)
				Position = null;
		}
	}
	[Serializable]
	public partial class IfcPath : IfcTopologicalRepresentationItem
	{
		internal List<int> mEdgeList = new List<int>();// : SET [1:?] OF IfcOrientedEdge;
		public ReadOnlyCollection<IfcOrientedEdge> EdgeList { get { return new ReadOnlyCollection<IfcOrientedEdge>(mEdgeList.ConvertAll(x => mDatabase[x] as IfcOrientedEdge)); } }

		internal IfcPath() : base() { }
		internal IfcPath(DatabaseIfc db, IfcPath p) : base(db, p) { p.EdgeList.ToList().ForEach(x => addEdge(db.Factory.Duplicate(x) as IfcOrientedEdge)); }
		public IfcPath(IfcOrientedEdge edge) : base(edge.mDatabase) { mEdgeList.Add(edge.mIndex); }
		public IfcPath(List<IfcOrientedEdge> edges) : base(edges[0].mDatabase) { edges.ForEach(x => addEdge(x)); }
		
		internal void addEdge(IfcOrientedEdge edge) { mEdgeList.Add(edge.mIndex); }
	}
	[Serializable]
	public partial class IfcPCurve : IfcCurve
	{
		internal int mBasisSurface;// :	IfcSurface;
		internal int mReferenceCurve;// :	IfcCurve; 

		public IfcSurface BasisSurface { get { return mDatabase[mBasisSurface] as IfcSurface; } set { mBasisSurface = value.mIndex; } }
		public IfcCurve ReferenceCurve { get { return mDatabase[mReferenceCurve] as IfcCurve; } set { mReferenceCurve = value.mIndex; } }

		internal IfcPCurve() : base() { }
		internal IfcPCurve(DatabaseIfc db, IfcPCurve c) : base(db, c) { BasisSurface = db.Factory.Duplicate(c.BasisSurface) as IfcSurface; ReferenceCurve = db.Factory.Duplicate(c.ReferenceCurve) as IfcCurve; }
		public IfcPCurve(IfcSurface basisSurface, IfcCurve referenceCurve) : base(basisSurface.mDatabase) { BasisSurface = basisSurface; ReferenceCurve = referenceCurve; }
	}
	[Serializable]
	public partial class IfcPerformanceHistory : IfcControl
	{
		internal string mLifeCyclePhase;// : IfcLabel; 
		internal IfcPerformanceHistory() : base() { }
		internal IfcPerformanceHistory(DatabaseIfc db, IfcPerformanceHistory h, IfcOwnerHistory ownerHistory, bool downStream) : base(db, h, ownerHistory, downStream) { mLifeCyclePhase = h.mLifeCyclePhase; }
	}
	//ENTITY IfcPermeableCoveringProperties : IfcPreDefinedPropertySet //IFC2x3 
	[Serializable]
	public partial class IfcPermit : IfcControl
	{
		internal string mPermitID;// : IfcIdentifier; 
		internal IfcPermit() : base() { }
		internal IfcPermit(DatabaseIfc db, IfcPermit p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mPermitID = p.mPermitID; }
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
		private SET<IfcExternalReferenceRelationship> mHasExternalReferences = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string FamilyName { get { return (mFamilyName == "$" ? "" : ParserIfc.Decode(mFamilyName)); } set { mFamilyName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string GivenName { get { return (mGivenName == "$" ? "" : ParserIfc.Decode(mGivenName)); } set { mGivenName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<string> MiddleNames { get { return new ReadOnlyCollection<string>(mMiddleNames.ConvertAll(x => ParserIfc.Decode(x))); } }
		public ReadOnlyCollection<string> PrefixTitles { get { return new ReadOnlyCollection<string>(mPrefixTitles.ConvertAll(x => ParserIfc.Decode(x))); } }
		public ReadOnlyCollection<string> SuffixTitles { get { return new ReadOnlyCollection<string>(mSuffixTitles.ConvertAll(x => ParserIfc.Decode(x))); } }
		public LIST<IfcActorRole> Roles { get { return mRoles; } }
		public LIST<IfcAddress> Addresses { get { return mAddresses; } }
		public SET<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } set { mHasExternalReferences.Clear();  if (value != null) { mHasExternalReferences.CollectionChanged -= mHasExternalReferences_CollectionChanged; mHasExternalReferences = value; mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged; } } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>( mHasConstraintRelationships); } }

		public string Name { get { return (string.IsNullOrEmpty(GivenName) ? FamilyName : GivenName + (string.IsNullOrEmpty(FamilyName) ? "" : " " + FamilyName)); } }

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

			mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged;
		}
		private void mHasExternalReferences_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
		private SET<IfcExternalReferenceRelationship> mHasExternalReferences = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public IfcPerson ThePerson { get { return mThePerson; } set { mThePerson = value; } }
		public IfcOrganization TheOrganization { get { return mTheOrganization; } set { mTheOrganization = value; } }
		public LIST<IfcActorRole> Roles { get { return mRoles; } set { mRoles = value; } }

		public SET<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } set { mHasExternalReferences.Clear();  if (value != null) { mHasExternalReferences.CollectionChanged -= mHasExternalReferences_CollectionChanged; mHasExternalReferences = value; mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged; } } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>(mHasConstraintRelationships); } }

		public string Name { get { return TheOrganization.Name + " " + ThePerson.Name; } }

		internal IfcPersonAndOrganization() : base() { }
		internal IfcPersonAndOrganization(DatabaseIfc db) : base(db) { }
		internal IfcPersonAndOrganization(DatabaseIfc db, IfcPersonAndOrganization p) : base(db, p) { ThePerson = db.Factory.Duplicate(p.ThePerson) as IfcPerson; TheOrganization = db.Factory.Duplicate(p.TheOrganization) as IfcOrganization; Roles.AddRange(p.Roles.ConvertAll(x=> db.Factory.Duplicate(x) as IfcActorRole)); }
		public IfcPersonAndOrganization(IfcPerson person, IfcOrganization organization) : base(person.mDatabase) { ThePerson = person; TheOrganization = organization; }
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged;
		}
		private void mHasExternalReferences_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
	//ENTITY IfcPhysicalComplexQuantity
	[Serializable]
	public abstract partial class IfcPhysicalQuantity : BaseClassIfc, IfcResourceObjectSelect, NamedObjectIfc  //ABSTRACT SUPERTYPE OF(ONEOF(IfcPhysicalComplexQuantity, IfcPhysicalSimpleQuantity));
	{
		internal string mName = "NoName";// : IfcLabel;
		internal string mDescription = "$"; // : OPTIONAL IfcText;
											//INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReferences = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		//PartOfComplex : SET[0:1] OF IfcPhysicalComplexQuantity FOR HasQuantities;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public string Name
		{
			get { return ParserIfc.Decode(mName); }
			set { mName = (string.IsNullOrEmpty(value) ? "NoName" : ParserIfc.Encode(value)); }
		}
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public SET<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } set { mHasExternalReferences.Clear();  if (value != null) { mHasExternalReferences.CollectionChanged -= mHasExternalReferences_CollectionChanged; mHasExternalReferences = value; mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged; } } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		protected IfcPhysicalQuantity() : base() { }
		protected IfcPhysicalQuantity(DatabaseIfc db, IfcPhysicalQuantity q) : base(db, q) { mName = q.mName; mDescription = q.mDescription; }
		protected IfcPhysicalQuantity(DatabaseIfc db, string name) : base(db) { Name = name; }
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged;
		}
		private void mHasExternalReferences_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
	public partial class IfcPile : IfcBuildingElement
	{
		internal IfcPileTypeEnum mPredefinedType = IfcPileTypeEnum.NOTDEFINED;// OPTIONAL : IfcPileTypeEnum;
		internal IfcPileConstructionEnum mConstructionType = IfcPileConstructionEnum.NOTDEFINED;// : OPTIONAL IfcPileConstructionEnum; IFC4 	Deprecated.

		public IfcPileTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		[Obsolete("DEPRECEATED IFC4", false)]
		public IfcPileConstructionEnum ConstructionType { get { return mConstructionType; } set { mConstructionType = value; } }

		internal IfcPile() : base() { }
		internal IfcPile(DatabaseIfc db, IfcPile p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mPredefinedType = p.mPredefinedType; mConstructionType = p.mConstructionType; }
		public IfcPile(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcPileType : IfcBuildingElementType
	{
		internal IfcPileTypeEnum mPredefinedType = IfcPileTypeEnum.NOTDEFINED;
		public IfcPileTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPileType() : base() { }
		internal IfcPileType(DatabaseIfc db, IfcPileType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcPileType(DatabaseIfc m, string name, IfcPileTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcPileType(string name, IfcMaterialProfileSet mps, IfcPileTypeEnum type) : base(mps.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = mps; }
	}
	[Serializable]
	public partial class IfcPipeFitting : IfcFlowFitting //IFC4
	{
		internal IfcPipeFittingTypeEnum mPredefinedType = IfcPipeFittingTypeEnum.NOTDEFINED;    // :	OPTIONAL IfcPipeFittingTypeEnum;
		public IfcPipeFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeFitting() : base() { }
		internal IfcPipeFitting(DatabaseIfc db, IfcPipeFitting f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcPipeFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcPipeFittingType : IfcFlowFittingType
	{
		internal IfcPipeFittingTypeEnum mPredefinedType = IfcPipeFittingTypeEnum.NOTDEFINED;// : IfcPipeFittingTypeEnum; 
		public IfcPipeFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeFittingType() : base() { }
		internal IfcPipeFittingType(DatabaseIfc db, IfcPipeFittingType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
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
		internal IfcPipeSegment(DatabaseIfc db, IfcPipeSegment s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { mPredefinedType = s.mPredefinedType; }
		public IfcPipeSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcPipeSegmentType : IfcFlowSegmentType
	{
		internal IfcPipeSegmentTypeEnum mPredefinedType = IfcPipeSegmentTypeEnum.NOTDEFINED;// : IfcPipeSegmentTypeEnum; 
		public IfcPipeSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeSegmentType() : base() { }
		internal IfcPipeSegmentType(DatabaseIfc db, IfcPipeSegmentType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
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
		private IfcCartesianPoint mLocation;// : IfcCartesianPoint;
		public IfcCartesianPoint Location { get { return mLocation; } set { mLocation = value; } }

		protected IfcPlacement() : base() { }
		protected IfcPlacement(DatabaseIfc db) : base(db) { Location = db.Factory.Origin; }
		protected IfcPlacement(IfcCartesianPoint location) : base(location.mDatabase) { Location = location; }
		protected IfcPlacement(DatabaseIfc db, IfcPlacement p) : base(db, p) { Location = db.Factory.Duplicate(p.Location) as IfcCartesianPoint; }

		public virtual bool IsXYPlane { get { return Location.isOrigin; } }
	}
	[Serializable]
	public partial class IfcPlanarBox : IfcPlanarExtent
	{
		internal int mPlacement;// : IfcAxis2Placement; 
		public IfcAxis2Placement Placement { get { return mDatabase[mPlacement] as IfcAxis2Placement; } set { mPlacement = value.Index; } }

		internal IfcPlanarBox() : base() { }
		internal IfcPlanarBox(DatabaseIfc db, IfcPlanarBox b) : base(db, b) { Placement = db.Factory.Duplicate(b.mDatabase[b.mPlacement]) as IfcAxis2Placement; }
	}
	[Serializable]
	public partial class IfcPlanarExtent : IfcGeometricRepresentationItem
	{
		internal double mSizeInX;// : IfcLengthMeasure;
		internal double mSizeInY;// : IfcLengthMeasure; 
		
		public double SizeInX { get { return mSizeInX; } set { mSizeInX = value; } }
		public double SizeInY { get { return mSizeInY; } set { mSizeInY = value; } }
		internal IfcPlanarExtent() : base() { }
		internal IfcPlanarExtent(DatabaseIfc db, IfcPlanarExtent p) : base(db, p) { mSizeInX = p.mSizeInX; mSizeInY = p.mSizeInY; }
	}
	[Serializable]
	public partial class IfcPlane : IfcElementarySurface
	{
		internal IfcPlane() : base() { }
		internal IfcPlane(DatabaseIfc db, IfcPlane p) : base(db, p) { }
		public IfcPlane(IfcAxis2Placement3D placement) : base(placement) { }
	}
	[Serializable]
	public partial class IfcPlate : IfcBuildingElement
	{
		internal IfcPlateTypeEnum mPredefinedType = IfcPlateTypeEnum.NOTDEFINED;//: OPTIONAL IfcPlateTypeEnum;
		public IfcPlateTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPlate() : base() { }
		internal IfcPlate(DatabaseIfc db, IfcPlate p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mPredefinedType = p.mPredefinedType; }
		public IfcPlate(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		public IfcPlate(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public partial class IfcPlateStandardCase : IfcPlate //IFC4
	{
		internal override string KeyWord { get { return "IfcPlate"; } }
		internal IfcPlateStandardCase() : base() { }
		internal IfcPlateStandardCase(DatabaseIfc db, IfcPlateStandardCase p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcPlateType : IfcBuildingElementType
	{
		internal IfcPlateTypeEnum mPredefinedType = IfcPlateTypeEnum.NOTDEFINED;
		public IfcPlateTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPlateType() : base() { }
		internal IfcPlateType(DatabaseIfc db, IfcPlateType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t,ownerHistory,downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcPlateType(DatabaseIfc m, string name, IfcPlateTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcPlateType(string name, IfcMaterialLayerSet mls, IfcPlateTypeEnum type) : this(mls.mDatabase, name, type) { MaterialSelect = mls; }
	}
	[Serializable]
	public abstract partial class IfcPoint : IfcGeometricRepresentationItem, IfcGeometricSetSelect, IfcPointOrVertexPoint /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCartesianPoint ,IfcPointOnCurve ,IfcPointOnSurface))*/
	{
		protected IfcPoint() : base() { }
		protected IfcPoint(DatabaseIfc db) : base(db) { }
		protected IfcPoint(DatabaseIfc db, IfcPoint p) : base(db, p) { }
	}
	[Serializable]
	public partial class IfcPointOnCurve : IfcPoint
	{
		internal int mBasisCurve;// : IfcCurve;
		internal double mPointParameter;// : IfcParameterValue; 

		public IfcCurve BasisCurve { get { return mDatabase[mBasisCurve] as IfcCurve; } set { mBasisCurve = value.mIndex; } }
		public double PointParameter { get { return mPointParameter; } set { mPointParameter = value; } }

		internal IfcPointOnCurve() : base() { }
		internal IfcPointOnCurve(DatabaseIfc db, IfcPointOnCurve p) : base(db, p)
		{
			BasisCurve = db.Factory.Duplicate(p.BasisCurve) as IfcCurve;
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
		internal IfcPointOnSurface(DatabaseIfc db, IfcPointOnSurface p) : base(db, p)
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
		internal IfcPolygonalBoundedHalfSpace(DatabaseIfc db, IfcPolygonalBoundedHalfSpace s) : base(db, s) { Position = db.Factory.Duplicate(s.Position) as IfcAxis2Placement3D; PolygonalBoundary = db.Factory.Duplicate(s.PolygonalBoundary) as IfcBoundedCurve; }
	}
	[Serializable]
	public partial class IfcPolygonalFaceSet : IfcTessellatedFaceSet //IFC4A2
	{
		internal bool mClosed; // 	OPTIONAL BOOLEAN;
		internal List<int> mFaces = new List<int>(); // : SET [1:?] OF IfcIndexedPolygonalFace;
		internal List<int> mPnIndex = new List<int>(); // : OPTIONAL LIST [1:?] OF IfcPositiveInteger;

		public bool Closed { get { return mClosed; } set { mClosed = value; } }
		public ReadOnlyCollection<IfcIndexedPolygonalFace> Faces { get { return new ReadOnlyCollection<IfcIndexedPolygonalFace>(mFaces.ConvertAll(x => mDatabase[x] as IfcIndexedPolygonalFace)); } }
		public ReadOnlyCollection<int> PnIndex { get { return new ReadOnlyCollection<int>(mPnIndex); } }

		internal IfcPolygonalFaceSet() : base() { }
		internal IfcPolygonalFaceSet(DatabaseIfc db, IfcPolygonalFaceSet s) : base(db, s) { s.Faces.ToList().ForEach(x => addFace(db.Factory.Duplicate(x) as IfcIndexedPolygonalFace)); }
		public IfcPolygonalFaceSet(IfcCartesianPointList3D pl, bool closed, IEnumerable<IfcIndexedPolygonalFace> faces) : base(pl) { mClosed = closed; foreach (IfcIndexedPolygonalFace face in faces) addFace(face); }
		
		internal void addFace(IfcIndexedPolygonalFace face) { mFaces.Add(face.mIndex); }
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
		internal IfcPolyline(DatabaseIfc db, IfcPolyline p) : base(db, p) { Points.AddRange(p.Points.ConvertAll(x => db.Factory.Duplicate(x) as IfcCartesianPoint)); }
		public IfcPolyline(IfcCartesianPoint start, IfcCartesianPoint end) : base(start.mDatabase) { mPoints.Add(start); mPoints.Add(end); }
		public IfcPolyline(IEnumerable<IfcCartesianPoint> pts) : base(pts.First().mDatabase) { Points.AddRange(pts); }
		public IfcPolyline(DatabaseIfc db, IEnumerable<Tuple<double, double>> points) : base(db) { Points.AddRange(points.Select(x=> new IfcCartesianPoint(db, x.Item1, x.Item2))); }
		public IfcPolyline(DatabaseIfc db, List<Tuple<double, double, double>> points) : base(db) { Points.AddRange(points.ConvertAll(x => new IfcCartesianPoint(db, x.Item1, x.Item2, x.Item3))); }
		
		internal override void changeSchema(ReleaseVersion schema)
		{
			if (schema != ReleaseVersion.IFC2x3)
			{
				if (mPoints.Count > 2)
				{
					List<Tuple<double, double>> pts = new List<Tuple<double, double>>(mPoints.Count);

					foreach (IfcCartesianPoint cp in Points)
					{
						if (!cp.is2D)
						{
							pts.Clear();
							break;
						}
						LIST<double> p = cp.Coordinates;
						pts.Add(new Tuple<double, double>(p[0], p.Count > 1 ? p[1] : 0));
					}
					IfcIndexedPolyCurve ipc = pts.Count > 0 ? new IfcIndexedPolyCurve(new IfcCartesianPointList2D(mDatabase, pts)) : new IfcIndexedPolyCurve(new IfcCartesianPointList3D(mDatabase, Points.ToList().ConvertAll(x => x.Tuple())));
					ReplaceDatabase(ipc);
					return;

				}
			}
			else
				base.changeSchema(schema);
		}
	}
	[Serializable]
	public partial class IfcPolyloop : IfcLoop
	{
		internal LIST<IfcCartesianPoint> mPolygon = new LIST<IfcCartesianPoint> ();// : LIST [3:?] OF UNIQUE IfcCartesianPoint;
		public LIST<IfcCartesianPoint> Polygon { get { return mPolygon; } set { mPolygon = value; } }

		internal IfcPolyloop() : base() { }
		internal IfcPolyloop(DatabaseIfc db, IfcPolyloop l) : base(db, l) { mPolygon.AddRange(l.Polygon.ConvertAll(x=> db.Factory.Duplicate(x) as IfcCartesianPoint)); }
		public IfcPolyloop(IEnumerable<IfcCartesianPoint> polygon) : base(polygon.First().mDatabase) { mPolygon.AddRange(polygon); }
		public IfcPolyloop(IfcCartesianPoint cp1, IfcCartesianPoint cp2, IfcCartesianPoint cp3) : base(cp1.mDatabase) { mPolygon.Add(cp1); mPolygon.Add(cp2); mPolygon.Add(cp3); }

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcCartesianPoint p in Polygon)
				result.AddRange(p.Extract<T>());
			return result;
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
		protected IfcPort(DatabaseIfc db, IfcPort p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { }
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
					e.IsNestedBy[0].addRelated(this);
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
					t.IsNestedBy[0].addRelated(this);
			}
		}

		internal IfcElement getElement()
		{
			if (mDatabase.mRelease <= ReleaseVersion.IFC2x3)
			{
			}
			else
			{
				IfcRelNests nests = Nests;
				if (nests != null)
					return nests.RelatingObject as IfcElement;
			}
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
	{
		[NonSerialized] internal IfcRelContainedInSpatialStructure mContainedInStructure = null;
		public IfcRelContainedInSpatialStructure ContainedinStructure { get { return mContainedInStructure; } }
		protected IfcPositioningElement() : base() { }
		protected IfcPositioningElement(IfcSite host) : base(host.Database) { host.AddElement(this); }
		protected IfcPositioningElement(DatabaseIfc db, IfcPositioningElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream)
		{
			if (e.mContainedInStructure != null)
				(db.Factory.Duplicate(e.mContainedInStructure, false) as IfcRelContainedInSpatialStructure).RelatedElements.Add(this);
		}
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
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcPreDefinedDimensionSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
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
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcPreDefinedPointMarkerSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
	{
		internal IfcPreDefinedPointMarkerSymbol() : base() { }
		internal IfcPreDefinedPointMarkerSymbol(DatabaseIfc db, IfcPreDefinedPointMarkerSymbol s) : base(db, s) { }
	}
	[Serializable]
	public abstract partial class IfcPreDefinedProperties : IfcPropertyAbstraction // IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcementBarProperties, IfcSectionProperties, IfcSectionReinforcementProperties))
	{
		protected IfcPreDefinedProperties() : base() { }
		protected IfcPreDefinedProperties(DatabaseIfc db) : base(db) { }
		protected IfcPreDefinedProperties(DatabaseIfc db, IfcPreDefinedProperties p) : base(db, p) { }
	}
	[Serializable]
	public abstract partial class IfcPreDefinedPropertySet : IfcPropertySetDefinition // IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcDoorLiningProperties,  
	{ //IfcDoorPanelProperties, IfcPermeableCoveringProperties, IfcReinforcementDefinitionProperties, IfcWindowLiningProperties, IfcWindowPanelProperties))
		protected IfcPreDefinedPropertySet() : base() { }
		protected IfcPreDefinedPropertySet(DatabaseIfc db, IfcPreDefinedPropertySet p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { }
		protected IfcPreDefinedPropertySet(DatabaseIfc m, string name) : base(m, name) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public abstract partial class IfcPreDefinedSymbol : IfcPreDefinedItem // DEPRECEATED IFC4
	{
		protected IfcPreDefinedSymbol() : base() { }
		protected IfcPreDefinedSymbol(DatabaseIfc db, IfcPreDefinedSymbol s) : base(db, s) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcPreDefinedTerminatorSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
	{
		internal IfcPreDefinedTerminatorSymbol() : base() { }
		internal IfcPreDefinedTerminatorSymbol(DatabaseIfc db, IfcPreDefinedTerminatorSymbol s) : base(db, s) { }
	}
	[Serializable]
	public abstract partial class IfcPreDefinedTextFont : IfcPreDefinedItem
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
		internal IfcPresentationLayerAssignment(DatabaseIfc db, IfcPresentationLayerAssignment a, bool downstream) : base(db, a)
		{
			mName = a.mName;
			mDescription = a.mDescription;
			if (downstream)
				mAssignedItems.AddRange(a.AssignedItems.ConvertAll(x => db.Factory.Duplicate(x as BaseClassIfc, true) as IfcLayeredItem));
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
						if(!i.LayerAssignments.Contains(this))
							i.LayerAssignments.Add(this);
					}
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcLayeredItem i in e.OldItems)
				{
					if (i != null)
						i.LayerAssignments.Remove(this);
				}
			}
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
		internal IfcPresentationLayerWithStyle(DatabaseIfc db, IfcPresentationLayerWithStyle l, bool downstream) : base(db, l, downstream) { mLayerOn = l.mLayerOn; mLayerFrozen = l.mLayerFrozen; mLayerBlocked = l.mLayerBlocked; l.LayerStyles.ToList().ForEach(x => addLayerStyle(db.Factory.Duplicate(x) as IfcPresentationStyle)); }

		internal IfcPresentationLayerWithStyle(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPresentationLayerWithStyle(string name, IfcLayeredItem item, IfcPresentationStyle style) : base(name, item) { mLayerStyles.Add(style.mIndex); }
		public IfcPresentationLayerWithStyle(string name, IfcLayeredItem item, List<IfcPresentationStyle> styles) : base(name, item) { styles.ForEach(x => addLayerStyle(x)); }
		public IfcPresentationLayerWithStyle(string name, List<IfcLayeredItem> items, List<IfcPresentationStyle> styles) : base(name, items) { styles.ForEach(x => addLayerStyle(x)); }
		public IfcPresentationLayerWithStyle(string name, List<IfcLayeredItem> items, IfcPresentationStyle style) : base(name, items) { mLayerStyles.Add(style.mIndex); }
	
		internal void addLayerStyle(IfcPresentationStyle style) { mLayerStyles.Add(style.mIndex); }
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
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcPresentationStyleAssignment : BaseClassIfc, IfcStyleAssignmentSelect //DEPRECEATED IFC4
	{
		internal List<int> mStyles = new List<int>();// : SET [1:?] OF IfcPresentationStyleSelect; 
													 //INVERSE
		internal List<IfcStyledItem> mStyledItems = new List<IfcStyledItem>();

		public ReadOnlyCollection<IfcPresentationStyleSelect> Styles { get { return new ReadOnlyCollection<IfcPresentationStyleSelect>(mStyles.ConvertAll(x => mDatabase[x] as IfcPresentationStyleSelect)); } }
		public ReadOnlyCollection<IfcStyledItem> StyledItems { get { return new ReadOnlyCollection<IfcStyledItem>(mStyledItems); } }

		internal IfcPresentationStyleAssignment() : base() { }
		internal IfcPresentationStyleAssignment(DatabaseIfc db, IfcPresentationStyleAssignment s) : base(db, s) { s.mStyles.ToList().ForEach(x => addStyle(db.Factory.Duplicate(s.mDatabase[x]) as IfcPresentationStyleSelect)); }
		public IfcPresentationStyleAssignment(IfcPresentationStyle style) : base(style.mDatabase) { mStyles.Add(style.Index); }
		public IfcPresentationStyleAssignment(List<IfcPresentationStyle> styles) : base(styles[0].mDatabase) { mStyles = styles.ConvertAll(x => x.Index); }
		
		internal void addStyle(IfcPresentationStyleSelect style) { mStyles.Add(style.Index); }
		public void associateItem(IfcStyledItem item) { mStyledItems.Add(item); }
	}
	public interface IfcPresentationStyleSelect : IBaseClassIfc { } //DEPRECEATED IFC4 TYPE  = SELECT(IfcNullStyle, IfcCurveStyle, IfcSymbolStyle, IfcFillAreaStyle, IfcTextStyle, IfcSurfaceStyle);
	[Serializable]
	public partial class IfcProcedure : IfcProcess
	{
		internal IfcProcedureTypeEnum mProcedureType;// : IfcProcedureTypeEnum;
		internal string mUserDefinedProcedureType = "$";// : OPTIONAL IfcLabel;
		internal IfcProcedure() : base() { }
		internal IfcProcedure(DatabaseIfc db, IfcProcedure p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mProcedureType = p.mProcedureType; mUserDefinedProcedureType = p.mUserDefinedProcedureType; }
	}
	[Serializable]
	public partial class IfcProcedureType : IfcTypeProcess //IFC4
	{
		internal IfcProcedureTypeEnum mPredefinedType = IfcProcedureTypeEnum.NOTDEFINED;// : IfcProcedureTypeEnum; 
		public IfcProcedureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProcedureType() : base() { }
		internal IfcProcedureType(DatabaseIfc db, IfcProcedureType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcProcedureType(DatabaseIfc m, string name, IfcProcedureTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public abstract partial class IfcProcess : IfcObject // ABSTRACT SUPERTYPE OF (ONEOF (IfcProcedure ,IfcTask))
	{
		internal string mIdentification = "";// :OPTIONAL IfcIdentifier;
		internal string mLongDescription = "";//: OPTIONAL IfcText; 
		//INVERSE
		internal List<IfcRelSequence> mIsPredecessorTo = new List<IfcRelSequence>();// : SET [0:?] OF IfcRelSequence FOR RelatingProcess; 
		internal List<IfcRelSequence> mIsSuccessorFrom = new List<IfcRelSequence>();// : SET [0:?] OF IfcRelSequence FOR RelatedProcess;
		internal List<IfcRelAssignsToProcess> mOperatesOn = new List<IfcRelAssignsToProcess>();// : SET [0:?] OF IfcRelAssignsToProcess FOR RelatingProcess;

		public string Identification { get { return mIdentification; } set { mIdentification = value; } }
		public string LongDescription { get { return mLongDescription; } set { mLongDescription = value; } }

		protected IfcProcess() : base() { }
		protected IfcProcess(DatabaseIfc db, IfcProcess p, IfcOwnerHistory ownerHistory, bool downStream ) : base(db, p, ownerHistory, downStream) { mIdentification = p.mIdentification; mLongDescription = p.mLongDescription; }
		protected IfcProcess(DatabaseIfc db) : base(db)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.Ifc2x3NotAssigned)
				throw new Exception("Invalid Model View for IfcProcess : " + db.ModelView.ToString());
		}
		public bool AddOperatesOn(IfcObjectDefinition related)
		{
			if (mOperatesOn.Count == 0)
				new IfcRelAssignsToProcess(this, related);
			else if (!mOperatesOn[0].mRelatedObjects.Contains(related))
				mOperatesOn[0].RelatedObjects.Add(related);
			else
				return false;
			return true;
		}
	}
	[Serializable]
	public abstract partial class IfcProduct : IfcObject, IfcProductSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcAnnotation ,IfcElement ,IfcGrid ,IfcPort ,IfcProxy ,IfcSpatialElement ,IfcStructuralActivity ,IfcStructuralItem))
	{
		private IfcObjectPlacement mPlacement = null; //: OPTIONAL IfcObjectPlacement;
		private IfcProductRepresentation mRepresentation =  null; //: OPTIONAL IfcProductRepresentation 
		//INVERSE
		[NonSerialized] internal SET<IfcRelAssignsToProduct> mReferencedBy = new SET<IfcRelAssignsToProduct>();//	 :	SET OF IfcRelAssignsToProduct FOR RelatingProduct;

		//Specified on IfcElement
		internal List<IfcRelReferencedInSpatialStructure> mReferencedInStructures = new List<IfcRelReferencedInSpatialStructure>();//  : 	SET OF IfcRelReferencedInSpatialStructure FOR RelatedElements;

		public IfcObjectPlacement Placement
		{
			get { return mPlacement; }
			set
			{
				if (mPlacement != null)
					mPlacement.mPlacesObject.Remove(this);
				mPlacement = value;
				if (value != null)
					value.mPlacesObject.Add(this);
			}
		}
		public IfcProductRepresentation Representation
		{
			get { return mRepresentation; }
			set
			{
				IfcProductDefinitionShape pds = mRepresentation as IfcProductDefinitionShape;
				if (pds != null)
					pds.mShapeOfProduct.Remove(this);
				mRepresentation = value;
				if (value != null)
				{
					pds = value as IfcProductDefinitionShape;
					if (pds != null)
					{
						pds.mShapeOfProduct.Add(this);
						if (mPlacement == null)
						{
							IfcElement element = this as IfcElement;
							if (element == null)
								Placement = new IfcLocalPlacement(mDatabase.Factory.XYPlanePlacement);
							else
							{
								IfcProduct product = element.getContainer();
								if (product == null)
									Placement = new IfcLocalPlacement(mDatabase.Factory.XYPlanePlacement);
								else
									Placement = new IfcLocalPlacement(product.Placement, mDatabase.Factory.XYPlanePlacement);
							}
						}
					}
				}
			}
		}
		public SET<IfcRelAssignsToProduct> ReferencedBy { get { return mReferencedBy; } }

		internal IfcObjectPlacement mContainerCommonPlacement = null; //GeometryGym common Placement reference for aggregated items

		protected IfcProduct() : base() { }
		protected IfcProduct(IfcProduct basis) : base(basis) { mPlacement = basis.mPlacement; Representation = basis.Representation; mReferencedBy = basis.mReferencedBy; }
		protected IfcProduct(IfcProductRepresentation rep) : base(rep.mDatabase) { Representation = rep; }
		protected IfcProduct(IfcObjectPlacement placement) : base(placement.mDatabase) { Placement = placement; }
		protected IfcProduct(IfcObjectPlacement placement, IfcProductRepresentation rep) : base(placement == null ? rep.mDatabase : placement.mDatabase)
		{
			Placement = placement;
			Representation = rep;
		}
		protected IfcProduct(DatabaseIfc db) : base(db) { }
		protected IfcProduct(DatabaseIfc db, IfcProduct p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream)
		{
			if (p.mPlacement != null)
				Placement = db.Factory.Duplicate(p.Placement) as IfcObjectPlacement;
			if (p.mRepresentation != null)
				Representation = db.Factory.Duplicate(p.Representation) as IfcProductRepresentation;
			foreach (IfcRelAssignsToProduct rap in p.mReferencedBy)
			{
				IfcRelAssignsToProduct rp = db.Factory.Duplicate(rap, ownerHistory, false) as IfcRelAssignsToProduct;
				foreach (IfcObjectDefinition od in rap.RelatedObjects)
					rp.RelatedObjects.Add(db.Factory.Duplicate(od, ownerHistory, false) as IfcObjectDefinition);
				rp.RelatingProduct = this;
			}
		}
		protected IfcProduct(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host.mDatabase)
		{
			IfcElement el = this as IfcElement;
			IfcProduct product = host as IfcProduct;
			if (el != null && product != null)
				product.AddElement(el);
			else
				host.AddAggregated(this);
			Placement = p;
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
		public bool AddElement(IfcProduct product)
		{
			product.detachFromHost();
			return addProduct(product);
		}
		internal virtual void detachFromHost()
		{
			if (mDecomposes != null)
				mDecomposes.removeObject(this);
		}
		protected virtual bool addProduct(IfcProduct product)
		{
			if (mIsDecomposedBy.Count > 0)
				return mIsDecomposedBy[0].addObject(product);
			new IfcRelAggregates(this, product);
			return true;
		}
		
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			if (mRepresentation != null && !typeof(IfcRoot).IsAssignableFrom(type))
				result.AddRange(mRepresentation.Extract<T>());
			return result;
		}
		internal override void changeSchema(ReleaseVersion schema)
		{
			IfcProductRepresentation rep = Representation;
			if (rep != null)
				rep.changeSchema(schema);

			base.changeSchema(schema);
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcProductsOfCombustionProperties	 // DEPRECEATED IFC4
	[Serializable]
	public partial class IfcProductDefinitionShape : IfcProductRepresentation, IfcProductRepresentationSelect
	{
		private LIST<IfcShapeModel> mShapeRepresentations = new LIST<IfcShapeModel>(); 
		//INVERSE
		internal List<IfcProduct> mShapeOfProduct = new List<IfcProduct>();
		internal List<IfcShapeAspect> mHasShapeAspects = new List<IfcShapeAspect>();

		public ReadOnlyCollection<IfcProduct> ShapeOfProduct { get { return new ReadOnlyCollection<IfcProduct>(mShapeOfProduct); } }
		public ReadOnlyCollection<IfcShapeAspect> HasShapeAspects { get { return new ReadOnlyCollection<IfcShapeAspect>(mHasShapeAspects); } }

		public new LIST<IfcShapeModel> Representations { get { return mShapeRepresentations; } set { base.Representations.Clear(); mShapeRepresentations.Clear(); if (value != null) { mShapeRepresentations.CollectionChanged -= mShapeRepresentations_CollectionChanged;  mShapeRepresentations = value; base.Representations.AddRange(value); mShapeRepresentations.CollectionChanged += mShapeRepresentations_CollectionChanged;  } } }

		internal IfcProductDefinitionShape() : base() { }
		public IfcProductDefinitionShape(IfcShapeModel rep) : base(rep.Database) { mShapeRepresentations.Add(rep); }
		public IfcProductDefinitionShape(IEnumerable<IfcShapeModel> reps) : base(reps.First().Database) { Representations.AddRange(reps); } 
		internal IfcProductDefinitionShape(DatabaseIfc db, IfcProductDefinitionShape s) : base(db, s) { }
		protected override void initialize()
		{
			base.initialize();
			mShapeRepresentations.CollectionChanged += mShapeRepresentations_CollectionChanged;
		}
		private void mShapeRepresentations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcShapeModel r in e.NewItems)
				{
					if(!base.Representations.Contains(r))
						base.Representations.Add(r);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcShapeModel r in e.OldItems)
					base.Representations.Remove(r);
			}
		}
		protected override void addRepresentation(IfcRepresentation r)
		{
			IfcShapeModel shapeModel = r as IfcShapeModel;
			if (shapeModel != null && !mShapeRepresentations.Contains(shapeModel))
				mShapeRepresentations.Add(shapeModel);
		}
		protected override void removeRepresentation(IfcRepresentation r)
		{
			IfcShapeModel shapeModel = r as IfcShapeModel;
			if(shapeModel != null)
				mShapeRepresentations.Remove(shapeModel);
		}
		public void AddShapeAspect(IfcShapeAspect aspect) { mHasShapeAspects.Add(aspect); }
	}
	[Serializable]
	public partial class IfcProductRepresentation : BaseClassIfc, NamedObjectIfc //IFC4 Abstract (IfcMaterialDefinitionRepresentation ,IfcProductDefinitionShape)); //IFC4 Abstract
	{
		private string mName = "$";// : OPTIONAL IfcLabel;
		private string mDescription = "$";// : OPTIONAL IfcText;
		[NonSerialized] private LIST<IfcRepresentation> mRepresentations = new LIST<IfcRepresentation>();// : LIST [1:?] OF IfcRepresentation; 

		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public LIST<IfcRepresentation> Representations
		{
			get { return mRepresentations; }
			private set { mRepresentations.Clear(); if(value != null) { mRepresentations.CollectionChanged -= mRepresentations_CollectionChanged; mRepresentations = value; mRepresentations.CollectionChanged += mRepresentations_CollectionChanged; } }
		}

		internal IfcProductRepresentation() : base() { }
		protected IfcProductRepresentation(DatabaseIfc db) : base(db) { }
		[Obsolete("DEPRECEATED IFC4", false)]
		public IfcProductRepresentation(IfcRepresentation r) : base(r.mDatabase) { mRepresentations.Add(r); }
		[Obsolete("DEPRECEATED IFC4", false)]
		public IfcProductRepresentation(List<IfcRepresentation> reps) : base(reps[0].mDatabase) { Representations.AddRange(reps); }
		internal IfcProductRepresentation(DatabaseIfc db, IfcProductRepresentation r) : base(db, r)
		{
			mName = r.mName;
			mDescription = r.mDescription;
			Representations.AddRange(r.Representations.ToList().ConvertAll(x => db.Factory.Duplicate(x) as IfcRepresentation));
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
				foreach (IfcRepresentation r in e.NewItems)
				{
					if (r != null)
					{
						r.mOfProductRepresentation.Add(this);
						addRepresentation(r);
					}
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRepresentation r in e.OldItems)
				{
					if(r != null)
					{
						r.mOfProductRepresentation.Remove(this);
						removeRepresentation(r);
					}
				}
			}
		}
		protected virtual void addRepresentation(IfcRepresentation r) { } 
		protected virtual void removeRepresentation(IfcRepresentation r) { } 
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcRepresentation r in Representations)
				result.AddRange(r.Extract<T>());
			return result;

		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			foreach (IfcRepresentation representation in Representations)
				representation.changeSchema(schema);
			base.changeSchema(schema);
		}
	}
	public interface IfcProductRepresentationSelect : IBaseClassIfc { ReadOnlyCollection<IfcShapeAspect> HasShapeAspects { get; } void AddShapeAspect(IfcShapeAspect aspect); }// 	IfcProductDefinitionShape,	IfcRepresentationMap);
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
		private SET<IfcExternalReferenceRelationship> mHasExternalReferences = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal SET<IfcProfileProperties> mHasProperties = new SET<IfcProfileProperties>();
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg
		internal List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		public IfcProfileTypeEnum ProfileType { get { return mProfileType; } set { mProfileType = value; } }
		public string ProfileName { get { return mProfileName == "$" ? "" : ParserIfc.Decode(mProfileName); } set { mProfileName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Name { get { return ProfileName; } set { ProfileName = value; } }
		public SET<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } set { mHasExternalReferences.Clear();  if (value != null) { mHasExternalReferences.CollectionChanged -= mHasExternalReferences_CollectionChanged; mHasExternalReferences = value; mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged; } } }
		public SET<IfcProfileProperties> HasProperties { get { return mHasProperties; }  set { mHasProperties = value; } }

		protected IfcProfileDef() : base() { }
		protected IfcProfileDef(DatabaseIfc db, IfcProfileDef p) : base(db, p)
		{
			mProfileType = p.mProfileType;
			mProfileName = p.mProfileName;
			foreach (IfcProfileProperties pp in p.mHasProperties)
				(db.Factory.Duplicate(pp) as IfcProfileProperties).ProfileDefinition = this;
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

			mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged;
		}
		private void mHasExternalReferences_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
		internal override string KeyWord { get { return mDatabase.Release < ReleaseVersion.IFC4 ? base.KeyWord : "IfcProfileProperties"; } }
		//[Obsolete("DEPRECEATED IFC4", false)]
		//internal string mProfileName = "$";// : OPTIONAL IfcLabel; DELETED IFC4
		private int mProfileDefinition;// : OPTIONAL IfcProfileDef; 
		public IfcProfileDef ProfileDefinition
		{
			get { return mDatabase[mProfileDefinition] as IfcProfileDef; }
			set
			{
				if (value == null)
					mProfileDefinition = 0;
				else
				{
					mProfileDefinition = value.mIndex;
					value.mHasProperties.Add(this);
				}
			}
		}

		internal IfcRelAssociatesProfileProperties mAssociates = null; //GeometryGym attribute

		internal IfcProfileProperties() : base() { }
		internal IfcProfileProperties(DatabaseIfc db, IfcProfileProperties p) : base(db, p) { ProfileDefinition = db.Factory.Duplicate(p.ProfileDefinition) as IfcProfileDef; }
		internal IfcProfileProperties(IfcProfileDef p) : base(p == null ? null : p.mDatabase)
		{
			if (p != null)
			{
				ProfileDefinition = p;
				if (mDatabase.mRelease < ReleaseVersion.IFC4)
					mAssociates = new IfcRelAssociatesProfileProperties(this) { Name = p.ProfileName };
			}
		}
		internal IfcProfileProperties(List<IfcProperty> props, IfcProfileDef p) : base(props)
		{
			mProfileDefinition = p.mIndex;
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
		internal IfcProject(DatabaseIfc db, IfcProject p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { }
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
		private IfcProject(string projectNumber, IfcUnitAssignment units) : base(projectNumber, units)
		{
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECT";
		}
		private IfcProject(DatabaseIfc db, string projectNumber, IfcUnitAssignment.Length length) : base(db, projectNumber, length)
		{
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECT";
		}

		public IfcSpatialElement RootElement { get { return (mIsDecomposedBy.Count == 0 ? null : mIsDecomposedBy[0].RelatedObjects[0] as IfcSpatialElement); } }
		internal IfcSite getSite() { return (mIsDecomposedBy.Count == 0 ? null : mIsDecomposedBy[0].RelatedObjects[0] as IfcSite); }
		public IfcSite UppermostSite { get { return getSite(); } }
		public IfcBuilding UppermostBuilding
		{
			get
			{
				if (mIsDecomposedBy.Count == 0)
					return null;
				BaseClassIfc ent = mDatabase[mIsDecomposedBy[0].mRelatedObjects[0]];
				IfcBuilding result = ent as IfcBuilding;
				if (result != null)
					return result;
				IfcSite s = ent as IfcSite;
				if (s != null)
				{
					List<IfcBuilding> bs = s.getBuildings();
					if (bs.Count > 0)
						return bs[0];
				}
				return null;
			}
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
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcProjectionCurve // DEPRECEATED IFC4
	[Serializable]
	public partial class IfcProjectionElement : IfcFeatureElementAddition
	{
		internal IfcProjectionElementTypeEnum mPredefinedType = IfcProjectionElementTypeEnum.NOTDEFINED;// :	OPTIONAL IfcProjectionElementTypeEnum; //IFC4
		public IfcProjectionElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		//INVERSE
		internal IfcProjectionElement() : base() { }
		internal IfcProjectionElement(DatabaseIfc db, IfcProjectionElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream){ mPredefinedType = e.mPredefinedType; }
	}
	[Serializable]
	public partial class IfcProjectLibrary : IfcContext
	{
		internal IfcProjectLibrary() : base() { }
		internal IfcProjectLibrary(DatabaseIfc db, IfcProjectLibrary l, IfcOwnerHistory ownerHistory, bool downStream) : base(db, l, ownerHistory, downStream) { }
		public IfcProjectLibrary(DatabaseIfc m, string name) : base(m, name)
		{
			if (m.ModelView == ModelView.Ifc4Reference || m.ModelView == ModelView.Ifc2x3Coordination)
				throw new Exception("Invalid Model View for IfcProjectLibrary : " + m.ModelView.ToString());
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECTLIBRARY";
		}
		public IfcProjectLibrary(DatabaseIfc m, string name, IfcUnitAssignment.Length length)
			: base(m, name) { UnitsInContext = new IfcUnitAssignment(mDatabase).SetUnits(length); }

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			if (schema < ReleaseVersion.IFC4)
			{
				IfcBuilding building = new IfcBuilding(mDatabase, Name);
				IfcProject project = new IfcProject(building, Name) { UnitsInContext = UnitsInContext };
				List<IfcTypeObject> types = DeclaredTypes;
				for (int icounter = 0; icounter < types.Count; icounter++)
				{
					IfcTypeObject tp = types[icounter];
					IfcTypeProduct typeP = tp as IfcTypeProduct;
					if (typeP != null)
					{
						if (typeP.mRepresentationMaps.Count > 0)
							typeP.genMappedItemElement(building, new IfcAxis2Placement3D(mDatabase.Factory.Origin));
					}

					tp.changeSchema(schema);
					List<IfcPropertySetDefinition> psets = tp.HasPropertySets.ToList();
					foreach (IfcPropertySetDefinition pset in psets)
					{
						if (pset != null && pset.IsInstancePropertySet)
							tp.mHasPropertySets.Remove(pset);
					}
				}
				mDatabase[mIndex] = null;
				return;
			}
		}
	}
	[Serializable]
	public partial class IfcProjectOrder : IfcControl
	{
		//internal string mID;// : IfcIdentifier; IFC4 relocated 
		internal IfcProjectOrderTypeEnum mPredefinedType = IfcProjectOrderTypeEnum.NOTDEFINED;// : IfcProjectOrderTypeEnum;
		internal string mStatus = "$";// : OPTIONAL IfcLabel; 
		internal string mLongDescription = "$"; //	 :	OPTIONAL IfcText;
		internal IfcProjectOrder() : base() { }
		internal IfcProjectOrder(DatabaseIfc db, IfcProjectOrder o, IfcOwnerHistory ownerHistory, bool downStream) : base(db, o, ownerHistory, downStream) { mPredefinedType = o.mPredefinedType; mStatus = o.mStatus; mLongDescription = o.mLongDescription; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcProjectOrderRecord : IfcControl // DEPRECEATED IFC4
	{
		internal List<int> mRecords = new List<int>();// : LIST [1:?] OF UNIQUE IfcRelAssignsToProjectOrder;
		internal IfcProjectOrderRecordTypeEnum mPredefinedType = IfcProjectOrderRecordTypeEnum.NOTDEFINED;// : IfcProjectOrderRecordTypeEnum; 
															   //public List<ifcrelassignstopr>
		internal IfcProjectOrderRecord() : base() { }
		internal IfcProjectOrderRecord(DatabaseIfc db, IfcProjectOrderRecord r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { }// Records = r.Records mPredefinedType = i.mPredefinedType; }
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
		protected IfcProperty(DatabaseIfc db, IfcProperty p) : base(db, p) { mName = p.mName; mDescription = p.mDescription; }
		protected IfcProperty(DatabaseIfc db, string name) : base(db) { Name = name; }

		internal override bool isDuplicate(BaseClassIfc e)
		{
			IfcProperty p = e as IfcProperty;
			if (p == null || string.Compare(mName, p.mName) != 0 || string.Compare(mDescription, p.mDescription) != 0)
				return false;
			return base.isDuplicate(e);
		}

		public override bool Dispose(bool children)
		{
			if (mPartOfPset.Count > 0)
				return false;
			if (mPartOfComplex.Count > 0)
				return false;
			return base.Dispose(children);
		}
	}
	[Serializable]
	public abstract partial class IfcPropertyAbstraction : BaseClassIfc, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcExtendedProperties ,IfcPreDefinedProperties ,IfcProperty ,IfcPropertyEnumeration));
	{ //INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReferences = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public SET<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } set { mHasExternalReferences.Clear();  if (value != null) { mHasExternalReferences.CollectionChanged -= mHasExternalReferences_CollectionChanged; mHasExternalReferences = value; mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged; } } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>(mHasConstraintRelationships); } }
		protected IfcPropertyAbstraction() : base() { }
		protected IfcPropertyAbstraction(DatabaseIfc db) : base(db) { }
		protected IfcPropertyAbstraction(DatabaseIfc db, IfcPropertyAbstraction p) : base(db, p) { }
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged;
		}
		private void mHasExternalReferences_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
		public IfcValue LowerBoundValue { get { return mUpperBoundValue; } set { mUpperBoundValue = value; } }
		public IfcUnit Unit { get { return mDatabase[mUnit] as IfcUnit; } set { mUnit = (value == null ? 0 : value.Index); } }
		public IfcValue SetPointValue { get { return mSetPointValue; } set { mSetPointValue = value; } }

		internal IfcPropertyBoundedValue() : base() { }
		internal IfcPropertyBoundedValue(DatabaseIfc db, IfcPropertyBoundedValue p) : base(db, p)
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
		internal override string KeyWord { get { return "IfcPropertyBoundedValue"; } }
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
		protected IfcPropertyDefinition(DatabaseIfc db, IfcPropertyDefinition p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory)
		{
			foreach (IfcRelAssociates associates in mHasAssociations)
			{
				IfcRelAssociates dup = db.Factory.Duplicate(associates) as IfcRelAssociates;
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
		internal IfcPropertyEnumeration(DatabaseIfc db, IfcPropertyEnumeration p) : base(db, p)
		{
			mName = p.mName;
			mEnumerationValues.AddRange(p.mEnumerationValues);
			if (p.mUnit > 0)
				Unit = db.Factory.Duplicate(p.mDatabase[p.mUnit]) as IfcUnit;
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
		internal IfcPropertyListValue(DatabaseIfc db, IfcPropertyListValue p) : base(db, p)
		{
			mNominalValue = p.mNominalValue;
			if (p.mUnit > 0)
				Unit = db.Factory.Duplicate(p.mDatabase[p.mUnit]) as IfcUnit;
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
		internal IfcPropertyReferenceValue(DatabaseIfc db, IfcPropertyReferenceValue p) : base(db, p)
		{
			mUsageName = p.mUsageName;
#warning todo
			//if(p.mPropertyReference > 0)
			//	PropertyReference = db.Factory.Duplicate( p.PropertyReference) as ;
		}
		public IfcPropertyReferenceValue(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPropertyReferenceValue(string name, IfcObjectReferenceSelect obj) : base(obj.Database, name) { PropertyReference = obj; }
	}
	[Serializable]
	public partial class IfcPropertySet : IfcPropertySetDefinition
	{
		internal override string KeyWord { get { return "IfcPropertySet"; } }
		private Dictionary<string,IfcProperty> mHasProperties = new Dictionary<string, IfcProperty>();// : SET [1:?] OF IfcProperty;
		private List<int> mPropertyIndices = new List<int>();

		public ReadOnlyDictionary<string,IfcProperty> HasProperties { get { return new ReadOnlyDictionary<string, IfcProperty>( mHasProperties); } }

		protected override void initialize()
		{
			base.initialize();
			mHasProperties = new Dictionary<string, IfcProperty>();
			mPropertyIndices = new List<int>();
		}

		internal IfcPropertySet() : base() { }
		protected IfcPropertySet(IfcObjectDefinition obj) : base(obj.mDatabase,"") { Name = this.GetType().Name; DefinesOccurrence.RelatedObjects.Add(obj); }
		protected IfcPropertySet(IfcTypeObject type) : base(type.mDatabase,"") { Name = this.GetType().Name; type.HasPropertySets.Add(this); }
		internal IfcPropertySet(DatabaseIfc db, IfcPropertySet s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { s.mPropertyIndices.ForEach(x => addProperty( db.Factory.Duplicate(s.mDatabase[x]) as IfcProperty)); }
		public IfcPropertySet(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPropertySet(IfcObjectDefinition relatedObject, string name) : base(relatedObject, name) { }
		public IfcPropertySet(string name, IfcProperty prop) : base(prop.mDatabase, name) { addProperty(prop); }
		public IfcPropertySet(string name, IEnumerable<IfcProperty> props) : base(props.First().mDatabase, name) { foreach(IfcProperty p in props) addProperty(p);  }
		public IfcPropertySet(IfcObjectDefinition relatedObject, string name, IfcProperty prop) : base(relatedObject, name) { addProperty(prop); }
		public IfcPropertySet(IfcObjectDefinition relatedObject, string name, IEnumerable<IfcProperty> props) : base(relatedObject, name) { foreach(IfcProperty p in props) addProperty(p);  }
		

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcProperty p in mHasProperties.Values)
				result.AddRange(p.Extract<T>());
			return result;
		}
		internal override bool isEmpty { get { return mHasProperties.Count == 0; } }
		public IfcPropertyBoundedValue AddProperty(IfcPropertyBoundedValue property) { addProperty(property); return property; }
		public IfcPropertyEnumeratedValue AddProperty(IfcPropertyEnumeratedValue property) { addProperty(property); return property; }
		public IfcPropertyReferenceValue AddProperty(IfcPropertyReferenceValue property) { addProperty(property); return property; }
		public IfcPropertySingleValue AddProperty(IfcPropertySingleValue property) { addProperty(property); return property; }
		public IfcPropertyTableValue AddProperty(IfcPropertyTableValue property) { addProperty(property); return property; }
		internal void addProperty(IfcProperty property)
		{
			if (property == null)
				return;
			IfcProperty existing = null;
			if (mHasProperties.TryGetValue(property.Name, out existing))
			{
				if (property.isDuplicate(existing))
					return;
			}
			mHasProperties[property.Name] = property;
			property.mPartOfPset.Add(this);
			if(!mPropertyIndices.Contains(property.mIndex))
				mPropertyIndices.Add(property.mIndex);
		}
		public void RemoveProperty(IfcProperty property)
		{
			if (property != null)
			{
				mHasProperties.Remove(property.Name);
				mPropertyIndices.Remove(property.mIndex);
				property.mPartOfPset.Remove(this);
			}
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
				IfcProperty existing = this[name];
				if (existing != null)
					mPropertyIndices.Remove(existing.Index);
				mHasProperties[name] = value;
				mPropertyIndices.Add(value.Index);
			}
		}
		public void SetProperties(IEnumerable<IfcProperty> properties)
		{
			mHasProperties.Clear();
			mPropertyIndices.Clear();
			foreach (IfcProperty property in properties)
				addProperty(property);
		}
		public override bool Dispose(bool children)
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
			return base.Dispose(children);
		}
		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			foreach (IfcProperty property in HasProperties.Values)
				property.changeSchema(schema);
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
					else
					{
						foreach (int i in r.mListPositions)
							result.Add(mDatabase[mPropertyIndices[i - 1]] as IfcProperty);
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
				else
				{
					foreach (int i in r.mListPositions)
						result.AddRange(mDatabase[mPropertyIndices[i - 1]].retrieveReference(ir));
				}
				return result;
			}
			return base.retrieveReference(r);
		}
	}
	[Serializable]
	public abstract partial class IfcPropertySetDefinition : IfcPropertyDefinition //ABSTRACT SUPERTYPE OF (ONEOF (IfcElementQuantity,IfcEnergyProperties ,
	{ // IfcFluidFlowProperties,IfcPropertySet, IfcServiceLifeFactor, IfcSoundProperties ,IfcSoundValue ,IfcSpaceThermalLoadProperties ))
		//INVERSE
		internal SET<IfcTypeObject> mDefinesType = new SET<IfcTypeObject>();// :	SET OF IfcTypeObject FOR HasPropertySets; IFC4change
		internal List<IfcRelDefinesByTemplate> mIsDefinedBy = new List<IfcRelDefinesByTemplate>();//IsDefinedBy	 :	SET OF IfcRelDefinesByTemplate FOR RelatedPropertySets;
		private IfcRelDefinesByProperties mDefinesOccurrence = null; //:	SET [0:1] OF IfcRelDefinesByProperties FOR RelatingPropertyDefinition;
		
		public SET<IfcTypeObject> DefinesType { get { return mDefinesType; } }
		public ReadOnlyCollection<IfcRelDefinesByTemplate> IsDefinedBy { get { return new ReadOnlyCollection<IfcRelDefinesByTemplate>(mIsDefinedBy); } }
		public IfcRelDefinesByProperties DefinesOccurrence { get { if (mDefinesOccurrence == null) mDefinesOccurrence = new IfcRelDefinesByProperties(this) { Name = Name }; return mDefinesOccurrence; } set { mDefinesOccurrence = value; } }

		protected IfcPropertySetDefinition() : base() { }
		protected IfcPropertySetDefinition(DatabaseIfc db, IfcPropertySetDefinition s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { }
		protected IfcPropertySetDefinition(DatabaseIfc m, string name) : base(m) { Name = name; }
		protected IfcPropertySetDefinition(IfcObjectDefinition relatedObject, string name) : base(relatedObject.mDatabase) { RelateObjectDefinition(relatedObject); Name = name; }

		public void RelateObjectDefinition(IfcObjectDefinition relatedObject)
		{
			IfcTypeObject to = relatedObject as IfcTypeObject;
			if (to != null)
				to.HasPropertySets.Add(this);
			else
			{
				if (mDefinesOccurrence == null)
					mDefinesOccurrence = new IfcRelDefinesByProperties(relatedObject, this);
				else
					mDefinesOccurrence.RelatedObjects.Add(relatedObject);
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
		private List<int> mHasPropertyTemplateIndices = new List<int>(1);//
		private Dictionary<string, IfcPropertyTemplate> mHasPropertyTemplates = new Dictionary<string, IfcPropertyTemplate>();//  : SET [1:?] OF IfcPropertyTemplate;

		//INVERSE
		internal List<IfcRelDefinesByTemplate> mDefines = new List<IfcRelDefinesByTemplate>();//	:	SET OF IfcRelDefinesByTemplate FOR RelatingTemplate;

		private class ApplicableType
		{
			private Type mType = null;
			private string mPredefined = "";
			private HashSet<Type> mApplicable = new HashSet<Type>();
			internal ApplicableType(Type type) { mType = type; mApplicable.Add(type); }
			internal ApplicableType(Type type, string predefined) :this(type) { mPredefined = predefined; }

			internal bool isApplicable(Type type, string predefined)
			{
				if (mApplicable.Contains(type))
					return (string.IsNullOrEmpty(mPredefined) || string.Compare(predefined,mPredefined,true) == 0);
				if(type.IsSubclassOf(mType))
				{
					mApplicable.Add(type);
					return (string.IsNullOrEmpty(mPredefined) || string.Compare(predefined,mPredefined,true) == 0);
				}
				return false;
			}
			
		}
		private List<ApplicableType> mApplicableTypes = null;

		public IfcPropertySetTemplateTypeEnum TemplateType { get { return mTemplateType; } set { mTemplateType = value; } }
		public string ApplicableEntity
		{
			get { return (mApplicableEntity == "$" ? "" : ParserIfc.Decode(mApplicableEntity)); }
			set
			{
				mApplicableEntity = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value));
				mApplicableTypes = null;
			}
		}
		public ReadOnlyDictionary<string, IfcPropertyTemplate> HasPropertyTemplates { get { return new ReadOnlyDictionary<string, IfcPropertyTemplate>(mHasPropertyTemplates); } }
		public ReadOnlyCollection<IfcRelDefinesByTemplate> Defines => new ReadOnlyCollection<IfcRelDefinesByTemplate>(mDefines);

		protected override void initialize()
		{
			base.initialize();
			mHasPropertyTemplates = new Dictionary<string, IfcPropertyTemplate>();
			mHasPropertyTemplateIndices = new List<int>();
		}

		internal IfcPropertySetTemplate() : base() { }
		public IfcPropertySetTemplate(DatabaseIfc db, string name) : base(db,name) { }
		public IfcPropertySetTemplate(DatabaseIfc db, IfcPropertySetTemplate p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream)
		{
			mTemplateType = p.mTemplateType;
			mApplicableEntity = p.mApplicableEntity;
			if(downStream)
				p.HasPropertyTemplates.Values.ToList().ForEach(x => AddPropertyTemplate(db.Factory.Duplicate(x) as IfcPropertyTemplate));
		}
		public IfcPropertySetTemplate(string name, IfcPropertyTemplate propertyTemplate) : this(propertyTemplate.mDatabase, name) { AddPropertyTemplate(propertyTemplate); }
		public IfcPropertySetTemplate(string name, IEnumerable<IfcPropertyTemplate> propertyTemplates) : base(propertyTemplates.First().mDatabase, name) { foreach(IfcPropertyTemplate propertyTemplate in propertyTemplates) AddPropertyTemplate(propertyTemplate); }
		
		public void AddPropertyTemplate(IfcPropertyTemplate template) { mHasPropertyTemplateIndices.Add(template.mIndex); mHasPropertyTemplates.Add(template.Name, template); template.mPartOfPsetTemplate.Add(this); }
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
		public void Remove(string templateName) { IfcPropertyTemplate template = this[templateName]; if (template != null) { template.mPartOfPsetTemplate.Remove(this); mHasPropertyTemplateIndices.Remove(template.Index); mHasPropertyTemplates.Remove(templateName); } }
		public bool IsApplicable(IfcObjectDefinition obj) { return IsApplicable(obj.GetType(), obj.Particular); }
		public bool IsApplicable(Type type,string predefined)
		{
			if (mApplicableTypes == null)
				fillApplicableTypes();
			foreach (ApplicableType t in mApplicableTypes)
			{
				if (t.isApplicable(type, predefined))
					return true;
			}
			return false;
		}
		private void fillApplicableTypes()
		{
			mApplicableTypes = new List<ApplicableType>();
			string[] fields = mApplicableEntity.Split(",".ToCharArray());
			foreach(string str in fields)
			{
				if (string.IsNullOrEmpty(str))
					continue;
				string[] pair = str.Split("/".ToCharArray());
				string typename = (pair == null ? str : pair[0]), predefined = pair == null || pair.Length < 2 ? "" : pair[0];
				Type type = Type.GetType("GeometryGym.Ifc." + typename);
				if (type == null)
					continue;	
				mApplicableTypes.Add( new ApplicableType(type,predefined));
			}

		}
	}
	[Serializable]
	public partial class IfcPropertySingleValue : IfcSimpleProperty
	{
		private IfcValue mNominalValue;// : OPTIONAL IfcValue; 
		private int mUnit;// : OPTIONAL IfcUnit; 

		public IfcValue NominalValue { get { return mNominalValue; } set { mNominalValue = value; } }
		public IfcUnit Unit { get { return mDatabase[mUnit] as IfcUnit; } set { mUnit = (value == null ? 0 : value.Index); } }

		internal string mVal;
		internal IfcPropertySingleValue() : base() { }
		internal IfcPropertySingleValue(DatabaseIfc db, IfcPropertySingleValue s) : base(db, s)
		{
			mNominalValue = s.mNominalValue;
			if (s.mUnit > 0)
				Unit = db.Factory.Duplicate( s.mDatabase[s.mUnit]) as IfcUnit;
		}
		public IfcPropertySingleValue(DatabaseIfc m, string name, IfcValue value) : base(m, name) { mNominalValue = value; }
		public IfcPropertySingleValue(DatabaseIfc m, string name, bool value) : this(m, name, new IfcBoolean(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, int value) : this(m, name, new IfcInteger(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, double value) : this(m, name, new IfcReal(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, string value) : this(m, name, new IfcLabel(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, IfcValue val, IfcUnit unit) : this(m, name, val) {  Unit = unit; }

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			if(schema <= ReleaseVersion.IFC4)
			{
				IfcValue value = NominalValue;
				IfcURIReference uri = value as IfcURIReference;
				if (uri != null)
					NominalValue = new IfcLabel(uri.URI.ToString());
				else
				{
					
				}
			}
		}
		internal override bool isDuplicate(BaseClassIfc e)
		{
			IfcPropertySingleValue psv = e as IfcPropertySingleValue;
			if (psv == null || psv.mUnit != mUnit)
				return false;
			if (base.isDuplicate(e))
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
		internal IfcPropertyTableValue(DatabaseIfc db, IfcPropertyTableValue p) : base(db, p)
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
		internal int mDefiningUnit;// : :	OPTIONAL IfcUnit;   
		internal int mDefinedUnit;// : :	OPTIONAL IfcUnit;
		internal IfcCurveInterpolationEnum mCurveInterpolation = IfcCurveInterpolationEnum.NOTDEFINED;// : :	OPTIONAL IfcCurveInterpolationEnum; 

		internal IfcPropertyTableValue() : base() { }
		internal IfcPropertyTableValue(DatabaseIfc db, IfcPropertyTableValue p) : base(db, p)
		{
#warning todo
			//			mDefiningValues.AddRange(p.DefiningValues);
			//		DefinedValues.AddRange(p.DefinedValues);//.ToArray()); mExpression = p.mExpression; mDefiningUnit = p.mDefiningUnit; mDefinedUnit = p.mDefinedUnit; mCurveInterpolation = p.mCurveInterpolation; 
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
		protected IfcPropertyTemplate(DatabaseIfc db, IfcPropertyTemplate t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
		protected IfcPropertyTemplate(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Serializable]
	public abstract partial class IfcPropertyTemplateDefinition : IfcPropertyDefinition // ABSTRACT SUPERTYPE OF(ONEOF(IfcPropertySetTemplate, IfcPropertyTemplate))
	{ 
	 	protected IfcPropertyTemplateDefinition() : base() { }
		protected IfcPropertyTemplateDefinition(DatabaseIfc db, IfcPropertyTemplateDefinition p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { }
		protected IfcPropertyTemplateDefinition(DatabaseIfc m, string name) : base(m) { Name = name; }
	}
	[Serializable]
	public partial class IfcProtectiveDevice : IfcFlowController //IFC4
	{
		internal IfcProtectiveDeviceTypeEnum mPredefinedType = IfcProtectiveDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcProtectiveDeviceTypeEnum;
		public IfcProtectiveDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDevice() : base() { }
		internal IfcProtectiveDevice(DatabaseIfc db, IfcProtectiveDevice d, IfcOwnerHistory ownerHistory, bool downStream) : base(db, d, ownerHistory, downStream) { mPredefinedType = d.mPredefinedType; }
		public IfcProtectiveDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcProtectiveDeviceTrippingUnit : IfcDistributionControlElement //IFC4  
	{
		internal IfcProtectiveDeviceTrippingUnitTypeEnum mPredefinedType = IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED;
		public IfcProtectiveDeviceTrippingUnitTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceTrippingUnit() : base() { }
		internal IfcProtectiveDeviceTrippingUnit(DatabaseIfc db, IfcProtectiveDeviceTrippingUnit u, IfcOwnerHistory ownerHistory, bool downStream) : base(db,u, ownerHistory, downStream) { mPredefinedType = u.mPredefinedType; }
		public IfcProtectiveDeviceTrippingUnit(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcProtectiveDeviceTrippingUnitType : IfcDistributionControlElementType
	{
		internal IfcProtectiveDeviceTrippingUnitTypeEnum mPredefinedType = IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED;// : IfcProtectiveDeviceTrippingUnitTypeEnum;
		public IfcProtectiveDeviceTrippingUnitTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceTrippingUnitType() : base() { }
		internal IfcProtectiveDeviceTrippingUnitType(DatabaseIfc db, IfcProtectiveDeviceTrippingUnitType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcProtectiveDeviceTrippingUnitType(DatabaseIfc m, string name, IfcProtectiveDeviceTrippingUnitTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcProtectiveDeviceType : IfcFlowControllerType
	{
		internal IfcProtectiveDeviceTypeEnum mPredefinedType = IfcProtectiveDeviceTypeEnum.NOTDEFINED;// : IfcProtectiveDeviceTypeEnum; 
		public IfcProtectiveDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceType() : base() { }
		internal IfcProtectiveDeviceType(DatabaseIfc db, IfcProtectiveDeviceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcProtectiveDeviceType(DatabaseIfc m, string name, IfcProtectiveDeviceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcProxy : IfcProduct
	{
		internal IfcObjectTypeEnum mProxyType;// : IfcObjectTypeEnum;
		internal string mTag = "$";// : OPTIONAL IfcLabel;
		internal IfcProxy() : base() { }
		internal IfcProxy(DatabaseIfc db, IfcProxy p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mProxyType = p.mProxyType; mTag = p.mTag; }
	}
	[Serializable]
	public partial class IfcPump : IfcFlowMovingDevice //IFC4
	{
		internal IfcPumpTypeEnum mPredefinedType = IfcPumpTypeEnum.NOTDEFINED;// OPTIONAL : IfcPumpTypeEnum;
		public IfcPumpTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPump() : base() { }
		internal IfcPump(DatabaseIfc db, IfcPump p, IfcOwnerHistory ownerHistory, bool downStream) : base(db,p, ownerHistory, downStream) { mPredefinedType = p.mPredefinedType; }
		public IfcPump(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcPumpType : IfcFlowMovingDeviceType
	{
		internal IfcPumpTypeEnum mPredefinedType = IfcPumpTypeEnum.NOTDEFINED;// : IfcPumpTypeEnum; 
		public IfcPumpTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPumpType() : base() { }
		internal IfcPumpType(DatabaseIfc db, IfcPumpType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcPumpType(DatabaseIfc m, string name, IfcPumpTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
}
