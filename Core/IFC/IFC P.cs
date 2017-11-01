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
	public abstract partial class IfcParameterizedProfileDef : IfcProfileDef //ABSTRACT SUPERTYPE OF (ONEOF (IfcAsymmetricIShapeProfileDef , IfcCShapeProfileDef ,IfcCircleProfileDef ,IfcCraneRailAShapeProfileDef ,IfcCraneRailFShapeProfileDef ,
	{//IfcEllipseProfileDef ,IfcIShapeProfileDef ,IfcLShapeProfileDef ,IfcRectangleProfileDef ,IfcTShapeProfileDef ,IfcTrapeziumProfileDef ,IfcUShapeProfileDef ,IfcZShapeProfileDef))*/
		internal int mPosition;// : IfcAxis2Placement2D //IFC4  OPTIONAL

		public IfcAxis2Placement2D Position
		{
			get { return (mPosition > 0 ? mDatabase[mPosition] as IfcAxis2Placement2D : null); }
			set { mPosition = (value == null ? (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? mDatabase.Factory.Origin2dPlace.mIndex : 0) : value.mIndex); }
		}

		protected IfcParameterizedProfileDef() : base() { }
		protected IfcParameterizedProfileDef(DatabaseIfc db, IfcParameterizedProfileDef p) : base(db, p)
		{
			if (p.mPosition > 0)
				Position = db.Factory.Duplicate(p.Position) as IfcAxis2Placement2D;
		}
		protected IfcParameterizedProfileDef(DatabaseIfc m, string name) : base(m, name)
		{
			if (mDatabase != null)
			{
				if (mDatabase.mModelView == ModelView.Ifc4Reference)
					throw new Exception("Invalid Model View for IfcParameterizedProfileDef : " + m.ModelView.ToString());
				if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
					Position = mDatabase.Factory.Origin2dPlace;
			}
		}
		internal override void changeSchema(ReleaseVersion schema)
		{
			IfcAxis2Placement2D position = Position;
			if (schema == ReleaseVersion.IFC2x3)
			{
				if (position == null)
					Position = mDatabase.Factory.Origin2dPlace;
			}
			else if (position != null && position.IsXYPlane)
				Position = null;
		}
	}
	public partial class IfcPath : IfcTopologicalRepresentationItem
	{
		internal List<int> mEdgeList = new List<int>();// : SET [1:?] OF IfcOrientedEdge;
		public ReadOnlyCollection<IfcOrientedEdge> EdgeList { get { return new ReadOnlyCollection<IfcOrientedEdge>(mEdgeList.ConvertAll(x => mDatabase[x] as IfcOrientedEdge)); } }

		internal IfcPath() : base() { }
		internal IfcPath(IfcOrientedEdge edge) : base(edge.mDatabase) { mEdgeList.Add(edge.mIndex); }
		internal IfcPath(List<IfcOrientedEdge> edges) : base(edges[0].mDatabase) { edges.ForEach(x => addEdge(x)); }
		internal IfcPath(DatabaseIfc db, IfcPath p) : base(db, p) { p.EdgeList.ToList().ForEach(x => addEdge(db.Factory.Duplicate(x) as IfcOrientedEdge)); }
		
		internal void addEdge(IfcOrientedEdge edge) { mEdgeList.Add(edge.mIndex); }
	}
	public partial class IfcPCurve : IfcCurve
	{
		internal int mBasisSurface;// :	IfcSurface;
		internal int mReferenceCurve;// :	IfcCurve; 

		public IfcSurface BasisSurface { get { return mDatabase[mBasisSurface] as IfcSurface; } set { mBasisSurface = value.mIndex; } }
		public IfcCurve ReferenceCurve { get { return mDatabase[mReferenceCurve] as IfcCurve; } set { mReferenceCurve = value.mIndex; } }

		internal IfcPCurve() : base() { }
		internal IfcPCurve(DatabaseIfc db, IfcPCurve c) : base(db, c) { BasisSurface = db.Factory.Duplicate(c.BasisSurface) as IfcSurface; ReferenceCurve = db.Factory.Duplicate(c.ReferenceCurve) as IfcCurve; }
		public IfcPCurve(IfcSurface basisSurface, IfcCurve referenceCurve) : base(basisSurface.Database) { BasisSurface = basisSurface; ReferenceCurve = referenceCurve; }
	}
	public partial class IfcPerformanceHistory : IfcControl
	{
		internal string mLifeCyclePhase;// : IfcLabel; 
		internal IfcPerformanceHistory() : base() { }
		internal IfcPerformanceHistory(DatabaseIfc db, IfcPerformanceHistory h, IfcOwnerHistory ownerHistory, bool downStream) : base(db, h, ownerHistory, downStream) { mLifeCyclePhase = h.mLifeCyclePhase; }
	}
	//ENTITY IfcPermeableCoveringProperties : IfcPreDefinedPropertySet //IFC2x3 
	public partial class IfcPermit : IfcControl
	{
		internal string mPermitID;// : IfcIdentifier; 
		internal IfcPermit() : base() { }
		internal IfcPermit(DatabaseIfc db, IfcPermit p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mPermitID = p.mPermitID; }
	}
	public partial class IfcPerson : BaseClassIfc, IfcActorSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect
	{
		private string mIdentification = "$";// : OPTIONAL IfcIdentifier;
		private string mFamilyName = "$", mGivenName = "$";// : OPTIONAL IfcLabel;
		private List<string> mMiddleNames = new List<string>(), mPrefixTitles = new List<string>(), mSuffixTitles = new List<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		private List<int> mRoles = new List<int>();// : OPTIONAL LIST [1:?] OF IfcActorRole;
		private List<int> mAddresses = new List<int>();//: OPTIONAL LIST [1:?] OF IfcAddress; 
													   //INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string FamilyName { get { return (mFamilyName == "$" ? "" : ParserIfc.Decode(mFamilyName)); } set { mFamilyName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string GivenName { get { return (mGivenName == "$" ? "" : ParserIfc.Decode(mGivenName)); } set { mGivenName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<string> MiddleNames { get { return new ReadOnlyCollection<string>(mMiddleNames.ConvertAll(x => ParserIfc.Decode(x))); } }
		public ReadOnlyCollection<string> PrefixTitles { get { return new ReadOnlyCollection<string>(mPrefixTitles.ConvertAll(x => ParserIfc.Decode(x))); } }
		public ReadOnlyCollection<string> SuffixTitles { get { return new ReadOnlyCollection<string>(mSuffixTitles.ConvertAll(x => ParserIfc.Decode(x))); } }
		public ReadOnlyCollection<IfcActorRole> Roles { get { return new ReadOnlyCollection<IfcActorRole>( mRoles.ConvertAll(x => mDatabase[x] as IfcActorRole)); } }
		public ReadOnlyCollection<IfcAddress> Addresses { get { return new ReadOnlyCollection<IfcAddress>(mAddresses.ConvertAll(x => mDatabase[x] as IfcAddress)); } }
		public ReadOnlyCollection<IfcExternalReferenceRelationship> HasExternalReferences { get { return new ReadOnlyCollection<IfcExternalReferenceRelationship>( mHasExternalReferences); } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>( mHasConstraintRelationships); } }

		public override string Name { get => (string.IsNullOrEmpty(GivenName) ? FamilyName : GivenName + (string.IsNullOrEmpty(FamilyName) ? "" : " " + FamilyName)); }

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
			p.Roles.ToList().ForEach(x => AddRole( db.Factory.Duplicate(x) as IfcActorRole));
			p.Addresses.ToList().ForEach(x => AddAddress( db.Factory.Duplicate(x) as IfcAddress));
		}
		internal IfcPerson(DatabaseIfc m, string id, string familyname, string givenName) : base(m)
		{
			Identification = id;
			FamilyName = familyname;
			GivenName = givenName;
		}
		
		public void AddMiddleName(string name) { if (!string.IsNullOrEmpty(name)) mMiddleNames.Add(ParserIfc.Encode(name)); }
		public void AddPrefixTitle(string title) { if (!string.IsNullOrEmpty(title)) mPrefixTitles.Add(ParserIfc.Encode(title)); }
		public void AddSuffixTitle(string title) { if (!string.IsNullOrEmpty(title)) mSuffixTitles.Add(ParserIfc.Encode(title)); }
		public void AddRole(IfcActorRole role) { if (role != null) mRoles.Add(role.mIndex); }
		public void AddAddress(IfcAddress address) { if (address != null) mAddresses.Add(address.mIndex); }
		public void AddExternalReferenceRelationship(IfcExternalReferenceRelationship referenceRelationship) { mHasExternalReferences.Add(referenceRelationship); }
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	public partial class IfcPersonAndOrganization : BaseClassIfc, IfcActorSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect
	{
		internal int mThePerson;// : IfcPerson;
		internal int mTheOrganization;// : IfcOrganization;
		private List<int> mRoles = new List<int>();// : OPTIONAL LIST [1:?] OF IfcActorRole;
												   //INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public IfcPerson ThePerson { get { return mDatabase[mThePerson] as IfcPerson; } set { mThePerson = value.mIndex; } }
		public IfcOrganization TheOrganization { get { return mDatabase[mTheOrganization] as IfcOrganization; } set { mTheOrganization = value.mIndex; } }
		public ReadOnlyCollection<IfcActorRole> Roles { get { return new ReadOnlyCollection<IfcActorRole>(mRoles.ConvertAll(x => mDatabase[x] as IfcActorRole)); } }

		public ReadOnlyCollection<IfcExternalReferenceRelationship> HasExternalReferences { get { return new ReadOnlyCollection<IfcExternalReferenceRelationship>(mHasExternalReferences); } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>(mHasConstraintRelationships); } }

		public override string Name { get => TheOrganization.Name + " " + ThePerson.Name; }

		internal IfcPersonAndOrganization() : base() { }
		internal IfcPersonAndOrganization(DatabaseIfc db) : base(db) { }
		internal IfcPersonAndOrganization(DatabaseIfc db, IfcPersonAndOrganization p) : base(db, p) { ThePerson = db.Factory.Duplicate(p.ThePerson) as IfcPerson; TheOrganization = db.Factory.Duplicate(p.TheOrganization) as IfcOrganization; p.Roles.ToList().ForEach(x => AddRole( db.Factory.Duplicate(x) as IfcActorRole)); }
		public IfcPersonAndOrganization(IfcPerson person, IfcOrganization organization) : base(person.mDatabase) { ThePerson = person; TheOrganization = organization; }
		
		public void AddRole(IfcActorRole role) { if (role != null) mRoles.Add(role.mIndex); }
		public void AddExternalReferenceRelationship(IfcExternalReferenceRelationship referenceRelationship) { mHasExternalReferences.Add(referenceRelationship); }
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	//ENTITY IfcPhysicalComplexQuantity
	public abstract partial class IfcPhysicalQuantity : BaseClassIfc, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF(ONEOF(IfcPhysicalComplexQuantity, IfcPhysicalSimpleQuantity));
	{
		internal string mName = "NoName";// : IfcLabel;
		internal string mDescription = "$"; // : OPTIONAL IfcText;
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public override string Name
		{
			get { return ParserIfc.Decode(mName); }
			set { mName = (string.IsNullOrEmpty(value) ? "NoName" : ParserIfc.Encode(value)); }
		}
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		protected IfcPhysicalQuantity() : base() { }
		protected IfcPhysicalQuantity(DatabaseIfc db, IfcPhysicalQuantity q) : base(db, q) { mName = q.mName; mDescription = q.mDescription; }
		protected IfcPhysicalQuantity(DatabaseIfc db, string name) : base(db) { Name = name; }
		
		public void AddExternalReferenceRelationship(IfcExternalReferenceRelationship referenceRelationship) { mHasExternalReferences.Add(referenceRelationship); }
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	public abstract partial class IfcPhysicalSimpleQuantity : IfcPhysicalQuantity //ABSTRACT SUPERTYPE OF (ONEOF (IfcQuantityArea ,IfcQuantityCount ,IfcQuantityLength ,IfcQuantityTime ,IfcQuantityVolume ,IfcQuantityWeight))
	{
		internal int mUnit = 0;// : OPTIONAL IfcNamedUnit;	
		public IfcNamedUnit Unit { get { return mDatabase[mUnit] as IfcNamedUnit; } set { mUnit = (value == null ? 0 : value.mIndex); } }

		protected IfcPhysicalSimpleQuantity() : base() { }
		protected IfcPhysicalSimpleQuantity(DatabaseIfc db, IfcPhysicalSimpleQuantity q) : base(db, q) { if (q.mUnit > 0) Unit = db.Factory.Duplicate(q.Unit) as IfcNamedUnit; }
		protected IfcPhysicalSimpleQuantity(DatabaseIfc db, string name) : base(db, name) { }

		internal abstract IfcMeasureValue MeasureValue { get; }
	}
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
	public partial class IfcPileType : IfcBuildingElementType
	{
		internal IfcPileTypeEnum mPredefinedType = IfcPileTypeEnum.NOTDEFINED;
		public IfcPileTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPileType() : base() { }
		internal IfcPileType(DatabaseIfc db, IfcPileType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcPileType(DatabaseIfc m, string name, IfcPileTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal IfcPileType(string name, IfcMaterialProfileSet mps, IfcPileTypeEnum type) : base(mps.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = mps; }
	}
	public partial class IfcPipeFitting : IfcFlowFitting //IFC4
	{
		internal IfcPipeFittingTypeEnum mPredefinedType = IfcPipeFittingTypeEnum.NOTDEFINED;    // :	OPTIONAL IfcPipeFittingTypeEnum;
		public IfcPipeFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeFitting() : base() { }
		internal IfcPipeFitting(DatabaseIfc db, IfcPipeFitting f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcPipeFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
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
	public partial class IfcPipeSegment : IfcFlowSegment //IFC4
	{
		internal IfcPipeSegmentTypeEnum mPredefinedType = IfcPipeSegmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcPipeSegmentTypeEnum;
		public IfcPipeSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeSegment() : base() { }
		internal IfcPipeSegment(DatabaseIfc db, IfcPipeSegment s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { mPredefinedType = s.mPredefinedType; }
		public IfcPipeSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	public partial class IfcPipeSegmentType : IfcFlowSegmentType
	{
		internal IfcPipeSegmentTypeEnum mPredefinedType = IfcPipeSegmentTypeEnum.NOTDEFINED;// : IfcPipeSegmentTypeEnum; 
		public IfcPipeSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeSegmentType() : base() { }
		internal IfcPipeSegmentType(DatabaseIfc db, IfcPipeSegmentType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		internal IfcPipeSegmentType(DatabaseIfc m, string name, IfcPipeSegmentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
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
	public abstract partial class IfcPlacement : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcAxis1Placement ,IfcAxis2Placement2D ,IfcAxis2Placement3D))*/
	{
		private int mLocation;// : IfcCartesianPoint;
		public IfcCartesianPoint Location { get { return mDatabase[mLocation] as IfcCartesianPoint; } set { mLocation = value.mIndex; } }

		protected IfcPlacement() : base() { }
		protected IfcPlacement(DatabaseIfc db) : base(db) { Location = db.Factory.Origin; }
		protected IfcPlacement(IfcCartesianPoint location) : base(location.mDatabase) { Location = location; }
		protected IfcPlacement(DatabaseIfc db, IfcPlacement p) : base(db, p) { Location = db.Factory.Duplicate(p.Location) as IfcCartesianPoint; }

		public virtual bool IsXYPlane { get { return Location.isOrigin; } }
	}
	public partial class IfcPlanarBox : IfcPlanarExtent
	{
		internal int mPlacement;// : IfcAxis2Placement; 
		public IfcAxis2Placement Placement { get { return mDatabase[mPlacement] as IfcAxis2Placement; } set { mPlacement = value.Index; } }

		internal IfcPlanarBox() : base() { }
		internal IfcPlanarBox(DatabaseIfc db, IfcPlanarBox b) : base(db, b) { Placement = db.Factory.Duplicate(b.mDatabase[b.mPlacement]) as IfcAxis2Placement; }
	}
	public partial class IfcPlanarExtent : IfcGeometricRepresentationItem
	{
		internal double mSizeInX;// : IfcLengthMeasure;
		internal double mSizeInY;// : IfcLengthMeasure; 
		internal IfcPlanarExtent() : base() { }
		internal IfcPlanarExtent(DatabaseIfc db, IfcPlanarExtent p) : base(db, p) { mSizeInX = p.mSizeInX; mSizeInY = p.mSizeInY; }
	}
	public partial class IfcPlane : IfcElementarySurface
	{
		internal IfcPlane() : base() { }
		internal IfcPlane(DatabaseIfc db, IfcPlane p) : base(db, p) { }
		public IfcPlane(IfcAxis2Placement3D placement) : base(placement) { }
	}
	public partial class IfcPlate : IfcBuildingElement
	{
		internal IfcPlateTypeEnum mPredefinedType = IfcPlateTypeEnum.NOTDEFINED;//: OPTIONAL IfcPlateTypeEnum;
		public IfcPlateTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPlate() : base() { }
		internal IfcPlate(DatabaseIfc db, IfcPlate p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mPredefinedType = p.mPredefinedType; }
		public IfcPlate(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	public partial class IfcPlateStandardCase : IfcPlate //IFC4
	{
		public override string KeyWord { get { return "IfcPlate"; } }
		internal IfcPlateStandardCase() : base() { }
		internal IfcPlateStandardCase(DatabaseIfc db, IfcPlateStandardCase p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { }
	}
	public partial class IfcPlateType : IfcBuildingElementType
	{
		internal IfcPlateTypeEnum mPredefinedType = IfcPlateTypeEnum.NOTDEFINED;
		public IfcPlateTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPlateType() : base() { }
		internal IfcPlateType(DatabaseIfc db, IfcPlateType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t,ownerHistory,downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcPlateType(DatabaseIfc m, string name, IfcPlateTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal IfcPlateType(string name, IfcMaterialLayerSet mls, IfcPlateTypeEnum type) : this(mls.mDatabase, name, type) { MaterialSelect = mls; }
	}
	public abstract partial class IfcPoint : IfcGeometricRepresentationItem, IfcGeometricSetSelect, IfcPointOrVertexPoint /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCartesianPoint ,IfcPointOnCurve ,IfcPointOnSurface))*/
	{
		protected IfcPoint() : base() { }
		protected IfcPoint(DatabaseIfc db) : base(db) { }
		protected IfcPoint(DatabaseIfc db, IfcPoint p) : base(db, p) { }
	}
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
		internal IfcPointOnCurve(DatabaseIfc m, IfcCurve c, double p) : base(m) { mBasisCurve = c.mIndex; mPointParameter = p; }
	}
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
	public interface IfcPointOrVertexPoint : IBaseClassIfc { }  // = SELECT ( IfcPoint, IfcVertexPoint);
	public partial class IfcPolygonalBoundedHalfSpace : IfcHalfSpaceSolid
	{
		internal int mPosition;// : IfcAxis2Placement3D;
		internal int mPolygonalBoundary;// : IfcBoundedCurve; 

		public IfcAxis2Placement3D Position { get { return mDatabase[mPosition] as IfcAxis2Placement3D; } set { mPosition = value.mIndex; } }
		public IfcBoundedCurve PolygonalBoundary { get { return mDatabase[mPolygonalBoundary] as IfcBoundedCurve; } set { mPolygonalBoundary = value.mIndex; } }

		internal IfcPolygonalBoundedHalfSpace() : base() { }
		internal IfcPolygonalBoundedHalfSpace(DatabaseIfc db, IfcPolygonalBoundedHalfSpace s) : base(db, s) { Position = db.Factory.Duplicate(s.Position) as IfcAxis2Placement3D; PolygonalBoundary = db.Factory.Duplicate(s.PolygonalBoundary) as IfcBoundedCurve; }
	}
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
	public partial class IfcPolyline : IfcBoundedCurve
	{
		private List<int> mPoints = new List<int>();// : LIST [2:?] OF IfcCartesianPoint;
		public ReadOnlyCollection<IfcCartesianPoint> Points { get { return new ReadOnlyCollection<IfcCartesianPoint>(mPoints.ConvertAll(x => mDatabase[x] as IfcCartesianPoint)); } }

		internal IfcPolyline() : base() { }
		internal IfcPolyline(DatabaseIfc db, IfcPolyline p) : base(db, p) { p.Points.ToList().ForEach(x => addPoint(db.Factory.Duplicate(x) as IfcCartesianPoint)); }
		public IfcPolyline(IfcCartesianPoint start, IfcCartesianPoint end) : base(start.mDatabase) { mPoints.Add(start.mIndex); mPoints.Add(end.mIndex); }
		public IfcPolyline(List<IfcCartesianPoint> pts) : base(pts[0].mDatabase) { pts.ForEach(x => addPoint(x)); }
		public IfcPolyline(DatabaseIfc db, List<Tuple<double, double>> points) : base(db) { points.ForEach(x => addPoint(new IfcCartesianPoint(db, x.Item1, x.Item2))); }
		public IfcPolyline(DatabaseIfc db, List<Tuple<double, double, double>> points) : base(db) { points.ForEach(x => addPoint(new IfcCartesianPoint(db, x.Item1, x.Item2, x.Item3))); }
		
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
						Tuple<double, double, double> p = cp.Coordinates;
						pts.Add(new Tuple<double, double>(p.Item1, p.Item2));
					}
					IfcIndexedPolyCurve ipc = pts.Count > 0 ? new IfcIndexedPolyCurve(new IfcCartesianPointList2D(mDatabase, pts)) : new IfcIndexedPolyCurve(new IfcCartesianPointList3D(mDatabase, Points.ToList().ConvertAll(x => x.Coordinates)));
					ReplaceDatabase(ipc);
					return;

				}
			}
			else
				base.changeSchema(schema);
		}

		internal void addPoint(IfcCartesianPoint point) { mPoints.Add(point.mIndex); }
		internal void setPoints(IEnumerable<IfcCartesianPoint> points) { mPoints.Clear(); foreach (IfcCartesianPoint p in points) addPoint(p); }
	}
	public partial class IfcPolyloop : IfcLoop
	{
		internal List<int> mPolygon = new List<int>();// : LIST [3:?] OF UNIQUE IfcCartesianPoint;
		public ReadOnlyCollection<IfcCartesianPoint> Polygon { get { return new ReadOnlyCollection<IfcCartesianPoint>(mPolygon.ConvertAll(x => mDatabase[x] as IfcCartesianPoint)); } }

		internal IfcPolyloop() : base() { }
		internal IfcPolyloop(DatabaseIfc db, IfcPolyloop l) : base(db, l) { l.Polygon.ToList().ForEach(x => addPoint(db.Factory.Duplicate(x) as IfcCartesianPoint)); }
		public IfcPolyloop(List<IfcCartesianPoint> polygon) : base(polygon[0].mDatabase) { mPolygon = polygon.ConvertAll(x => x.mIndex); }
		public IfcPolyloop(IfcCartesianPoint cp1, IfcCartesianPoint cp2, IfcCartesianPoint cp3) : base(cp1.mDatabase) { mPolygon = new List<int>() { cp1.mIndex, cp2.mIndex, cp3.mIndex }; }
		
		internal void addPoint(IfcCartesianPoint point) { mPolygon.Add(point.mIndex); }
	}
	public abstract partial class IfcPort : IfcProduct
	{   //INVERSE	
		internal IfcRelConnectsPortToElement mContainedIn = null;//	 :	SET [0:1] OF IfcRelConnectsPortToElement FOR RelatingPort;
		internal IfcRelConnectsPorts mConnectedFrom = null;//	 :	SET [0:1] OF IfcRelConnectsPorts FOR RelatedPort;
		internal IfcRelConnectsPorts mConnectedTo = null;//	 :	SET [0:1] OF IfcRelConnectsPorts FOR RelatingPort;

		protected IfcPort() : base() { }
		protected IfcPort(DatabaseIfc db, IfcPort p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { }
		protected IfcPort(DatabaseIfc db) : base(db) { }
		protected IfcPort(IfcElement e) : base(e.mDatabase)
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
			{
				new IfcRelConnectsPortToElement(this, e);
			}
			else
			{
				if (e.mIsNestedBy.Count == 0)
					e.mIsNestedBy.Add(new IfcRelNests(e, this));
				else
					e.mIsNestedBy[0].addRelated(this);
			}
		}
		protected IfcPort(IfcElementType t) : base(t.mDatabase)
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
			{
				t.AddAggregated(this);
			}
			else
			{
				if (t.mIsNestedBy.Count == 0)
				{
					new IfcRelNests(t, this);
				}
				else
					t.mIsNestedBy[0].addRelated(this);
			}
		}

		internal IfcElement getElement()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
			{
			}
			else if (mNests != null)
				return mNests.RelatingObject as IfcElement;
			return null;
		}
	}
	public partial class IfcPostalAddress : IfcAddress
	{
		internal string mInternalLocation = "$";// : OPTIONAL IfcLabel;
		internal List<string> mAddressLines = new List<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		internal string mPostalBox = "$";// :OPTIONAL IfcLabel;
		internal string mTown = "$";// : OPTIONAL IfcLabel;
		internal string mRegion = "$";// : OPTIONAL IfcLabel;
		internal string mPostalCode = "$";// : OPTIONAL IfcLabel;
		internal string mCountry = "$";// : OPTIONAL IfcLabel; 

		public string InternalLocation { get { return (mInternalLocation == "$" ? "" : ParserIfc.Decode(mInternalLocation)); } set { mInternalLocation = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<string> AddressLines { get { return new ReadOnlyCollection<string>(mAddressLines.ConvertAll(x => ParserIfc.Decode(x))); } }
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
		
		public void AddAddressLine(string line) { if(!string.IsNullOrEmpty(line)) mAddressLines.Add(ParserIfc.Encode(line)); }
	}
	public abstract partial class IfcPreDefinedColour : IfcPreDefinedItem, IfcColour //	ABSTRACT SUPERTYPE OF(IfcDraughtingPreDefinedColour)
	{
		protected IfcPreDefinedColour() : base() { }
		protected IfcPreDefinedColour(DatabaseIfc db, IfcPreDefinedColour c) : base(db, c) { }
	}
	public abstract partial class IfcPreDefinedCurveFont : IfcPreDefinedItem, IfcCurveStyleFontSelect
	{
		protected IfcPreDefinedCurveFont() : base() { }
		protected IfcPreDefinedCurveFont(DatabaseIfc db, IfcPreDefinedCurveFont f) : base(db, f) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcPreDefinedDimensionSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
	{
		internal IfcPreDefinedDimensionSymbol() : base() { }
		internal IfcPreDefinedDimensionSymbol(DatabaseIfc db, IfcPreDefinedDimensionSymbol s) : base(db, s) { }
	}
	public abstract partial class IfcPreDefinedItem : IfcPresentationItem
	{
		internal string mName = "";//: IfcLabel; 
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcPreDefinedItem() : base() { }
		protected IfcPreDefinedItem(DatabaseIfc db, IfcPreDefinedItem i) : base(db, i) { mName = i.mName; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcPreDefinedPointMarkerSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
	{
		internal IfcPreDefinedPointMarkerSymbol() : base() { }
		internal IfcPreDefinedPointMarkerSymbol(DatabaseIfc db, IfcPreDefinedPointMarkerSymbol s) : base(db, s) { }
	}
	public abstract partial class IfcPreDefinedProperties : IfcPropertyAbstraction // IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcementBarProperties, IfcSectionProperties, IfcSectionReinforcementProperties))
	{
		protected IfcPreDefinedProperties() : base() { }
		protected IfcPreDefinedProperties(DatabaseIfc db) : base(db) { }
		protected IfcPreDefinedProperties(DatabaseIfc db, IfcPreDefinedProperties p) : base(db, p) { }
	}
	public abstract partial class IfcPreDefinedPropertySet : IfcPropertySetDefinition // IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcDoorLiningProperties,  
	{ //IfcDoorPanelProperties, IfcPermeableCoveringProperties, IfcReinforcementDefinitionProperties, IfcWindowLiningProperties, IfcWindowPanelProperties))
		protected IfcPreDefinedPropertySet() : base() { }
		protected IfcPreDefinedPropertySet(DatabaseIfc db, IfcPreDefinedPropertySet p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { }
		protected IfcPreDefinedPropertySet(DatabaseIfc m, string name) : base(m, name) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	public abstract partial class IfcPreDefinedSymbol : IfcPreDefinedItem // DEPRECEATED IFC4
	{
		protected IfcPreDefinedSymbol() : base() { }
		protected IfcPreDefinedSymbol(DatabaseIfc db, IfcPreDefinedSymbol s) : base(db, s) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcPreDefinedTerminatorSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
	{
		internal IfcPreDefinedTerminatorSymbol() : base() { }
		internal IfcPreDefinedTerminatorSymbol(DatabaseIfc db, IfcPreDefinedTerminatorSymbol s) : base(db, s) { }
	}
	public abstract partial class IfcPreDefinedTextFont : IfcPreDefinedItem
	{
		protected IfcPreDefinedTextFont() : base() { }
		protected IfcPreDefinedTextFont(DatabaseIfc db, IfcPreDefinedTextFont f) : base(db, f) { }
	}
	public abstract partial class IfcPresentationItem : BaseClassIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcColourRgbList, IfcColourSpecification,
	{ // IfcCurveStyleFont, IfcCurveStyleFontAndScaling, IfcCurveStyleFontPattern, IfcIndexedColourMap, IfcPreDefinedItem, IfcSurfaceStyleLighting, IfcSurfaceStyleRefraction, IfcSurfaceStyleShading, IfcSurfaceStyleWithTextures, IfcSurfaceTexture, IfcTextStyleForDefinedFont, IfcTextStyleTextModel, IfcTextureCoordinate, IfcTextureVertex, IfcTextureVertexList));
		protected IfcPresentationItem() : base() { }
		protected IfcPresentationItem(DatabaseIfc db) : base(db) { }
		protected IfcPresentationItem(DatabaseIfc db, IfcPresentationItem i) : base(db, i) { }
	}
	public partial class IfcPresentationLayerAssignment : BaseClassIfc //SUPERTYPE OF	(IfcPresentationLayerWithStyle);
	{
		private string mName = "$";// : IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal List<int> mAssignedItems = new List<int>();// : SET [1:?] OF IfcLayeredItem; 
		internal string mIdentifier = "$";// : OPTIONAL IfcIdentifier; 

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "Default Layer" : mName = ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcLayeredItem> AssignedItems { get { return new ReadOnlyCollection<IfcLayeredItem>(mAssignedItems.ConvertAll(x => mDatabase[x] as IfcLayeredItem)); } }
		public string Identifier { get { return (mIdentifier == "$" ? "" : ParserIfc.Decode(mIdentifier)); } set { mIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcPresentationLayerAssignment() : base() { }
		internal IfcPresentationLayerAssignment(DatabaseIfc db, IfcPresentationLayerAssignment a, bool downstream) : base(db, a)
		{
			mName = a.mName;
			mDescription = a.mDescription;
			if(downstream)
				a.mAssignedItems.ToList().ForEach(x => addItem(db.Factory.Duplicate(a.mDatabase[x]) as IfcLayeredItem));
			mIdentifier = a.mIdentifier;
		}
		public IfcPresentationLayerAssignment(DatabaseIfc db, string name) : base(db) { Name = name; }
		public IfcPresentationLayerAssignment(string name, IfcLayeredItem item) : this(item.Database, name) { addItem(item); }
		public IfcPresentationLayerAssignment(string name, List<IfcLayeredItem> items) : this(items[0].Database, name) { foreach (IfcLayeredItem item in items) addItem(item); }
		
		internal void addItem(IfcLayeredItem item)
		{
			mAssignedItems.Add(item.Index);
			item.AssignLayer(this);
		}
	}
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
	public abstract partial class IfcPresentationStyle : BaseClassIfc, IfcStyleAssignmentSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcCurveStyle,IfcFillAreaStyle,IfcSurfaceStyle,IfcSymbolStyle,IfcTextStyle));
	{
		private string mName = "$";// : OPTIONAL IfcLabel;		
								   //INVERSE
		internal List<IfcStyledItem> mStyledItems = new List<IfcStyledItem>();

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcStyledItem> StyledItems { get { return new ReadOnlyCollection<IfcStyledItem>(mStyledItems); } }

		protected IfcPresentationStyle() : base() { }
		protected IfcPresentationStyle(DatabaseIfc db) : base(db) { }
		protected IfcPresentationStyle(DatabaseIfc db, IfcPresentationStyle s) : base(db, s) { mName = s.mName; }
		protected IfcPresentationStyle(IfcPresentationStyle i) : base() { mName = i.mName; }

		public void associateItem(IfcStyledItem item) { mStyledItems.Add(item); }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcPresentationStyleAssignment : BaseClassIfc, IfcStyleAssignmentSelect //DEPRECEATED IFC4
	{
		internal List<int> mStyles = new List<int>();// : SET [1:?] OF IfcPresentationStyleSelect; 
													 //INVERSE
		internal List<IfcStyledItem> mStyledItems = new List<IfcStyledItem>();

		public ReadOnlyCollection<IfcPresentationStyleSelect> Styles { get { return new ReadOnlyCollection<IfcPresentationStyleSelect>(mStyles.ConvertAll(x => mDatabase[x] as IfcPresentationStyleSelect)); } }
		public ReadOnlyCollection<IfcStyledItem> StyledItems { get { return new ReadOnlyCollection<IfcStyledItem>(mStyledItems); } }

		internal IfcPresentationStyleAssignment() : base() { }
		internal IfcPresentationStyleAssignment(IfcPresentationStyle style) : base(style.mDatabase) { mStyles.Add(style.Index); }
		internal IfcPresentationStyleAssignment(List<IfcPresentationStyle> styles) : base(styles[0].mDatabase) { mStyles = styles.ConvertAll(x => x.Index); }
		internal IfcPresentationStyleAssignment(DatabaseIfc db, IfcPresentationStyleAssignment s) : base(db, s) { s.mStyles.ToList().ForEach(x => addStyle(db.Factory.Duplicate(s.mDatabase[x]) as IfcPresentationStyleSelect)); }
		
		internal void addStyle(IfcPresentationStyleSelect style) { mStyles.Add(style.Index); }
		public void associateItem(IfcStyledItem item) { mStyledItems.Add(item); }
	}
	public interface IfcPresentationStyleSelect : IBaseClassIfc { } //DEPRECEATED IFC4 TYPE  = SELECT(IfcNullStyle, IfcCurveStyle, IfcSymbolStyle, IfcFillAreaStyle, IfcTextStyle, IfcSurfaceStyle);
	public partial class IfcProcedure : IfcProcess
	{
		internal string mProcedureID;// : IfcIdentifier;
		internal IfcProcedureTypeEnum mProcedureType;// : IfcProcedureTypeEnum;
		internal string mUserDefinedProcedureType = "$";// : OPTIONAL IfcLabel;
		internal IfcProcedure() : base() { }
		internal IfcProcedure(DatabaseIfc db, IfcProcedure p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mProcedureID = p.mProcedureID; mProcedureType = p.mProcedureType; mUserDefinedProcedureType = p.mUserDefinedProcedureType; }
	}
	public partial class IfcProcedureType : IfcTypeProcess //IFC4
	{
		internal IfcProcedureTypeEnum mPredefinedType = IfcProcedureTypeEnum.NOTDEFINED;// : IfcProcedureTypeEnum; 
		public IfcProcedureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProcedureType() : base() { }
		internal IfcProcedureType(DatabaseIfc db, IfcProcedureType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		internal IfcProcedureType(DatabaseIfc m, string name, IfcProcedureTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	public abstract partial class IfcProcess : IfcObject // ABSTRACT SUPERTYPE OF (ONEOF (IfcProcedure ,IfcTask))
	{
		internal string mIdentification = "$";// :OPTIONAL IfcIdentifier;
		internal string mLongDescription = "$";//: OPTIONAL IfcText; 
											   //INVERSE
		internal List<IfcRelSequence> mIsSuccessorFrom = new List<IfcRelSequence>();// : SET [0:?] OF IfcRelSequence FOR RelatedProcess;
		internal List<IfcRelSequence> mIsPredecessorTo = new List<IfcRelSequence>();// : SET [0:?] OF IfcRelSequence FOR RelatingProcess; 
		internal List<IfcRelAssignsToProcess> mOperatesOn = new List<IfcRelAssignsToProcess>();// : SET [0:?] OF IfcRelAssignsToProcess FOR RelatingProcess;

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string LongDescription { get { return (mLongDescription == "$" ? "" : ParserIfc.Decode(mLongDescription)); } set { mLongDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcProcess() : base() { }
		protected IfcProcess(DatabaseIfc db, IfcProcess p, IfcOwnerHistory ownerHistory, bool downStream ) : base(db, p, ownerHistory, downStream) { mIdentification = p.mIdentification; mLongDescription = p.mLongDescription; }
		protected IfcProcess(DatabaseIfc db) : base(db)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.Ifc2x3NotAssigned)
				throw new Exception("Invalid Model View for IfcProcess : " + db.ModelView.ToString());
		}
	}
	public abstract partial class IfcProduct : IfcObject, IfcProductSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcAnnotation ,IfcElement ,IfcGrid ,IfcPort ,IfcProxy ,IfcSpatialElement ,IfcStructuralActivity ,IfcStructuralItem))
	{
		private int mPlacement = 0; //: OPTIONAL IfcObjectPlacement;
		private int mRepresentation = 0; //: OPTIONAL IfcProductRepresentation 
										 //INVERSE
		internal List<IfcRelAssignsToProduct> mReferencedBy = new List<IfcRelAssignsToProduct>();//	 :	SET OF IfcRelAssignsToProduct FOR RelatingProduct;

		public IfcObjectPlacement Placement
		{
			get { return (mPlacement == 0 ? null : (IfcObjectPlacement)mDatabase[mPlacement]); }
			set
			{
				if (value == null)
				{
					if (mPlacement > 0)
					{
						IfcObjectPlacement pl = Placement;
						if (pl != null)
							pl.mPlacesObject.Remove(this);
					}
					mPlacement = 0;
				}
				else
				{
					mPlacement = value.mIndex;
					value.mPlacesObject.Add(this);
				}
			}
		}
		public IfcProductRepresentation Representation
		{
			get { return mDatabase[mRepresentation] as IfcProductRepresentation; }
			set
			{
				IfcProductDefinitionShape pds = Representation as IfcProductDefinitionShape;
				if (pds != null)
					pds.mShapeOfProduct.Remove(this);
				if (value == null)
					mRepresentation = 0;
				else
				{
					mRepresentation = value.mIndex;
					pds = value as IfcProductDefinitionShape;

					if (pds != null)
					{
						pds.mShapeOfProduct.Add(this);
						if (mPlacement == 0)
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
		public ReadOnlyCollection<IfcRelAssignsToProduct> ReferencedBy { get { return new ReadOnlyCollection<IfcRelAssignsToProduct>(mReferencedBy); } }

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
			if (p.mPlacement > 0)
				Placement = db.Factory.Duplicate(p.Placement) as IfcObjectPlacement;
			if (p.mRepresentation > 0)
				Representation = db.Factory.Duplicate(p.Representation) as IfcProductRepresentation;
			foreach (IfcRelAssignsToProduct rap in p.mReferencedBy)
			{
				IfcRelAssignsToProduct rp = db.Factory.Duplicate(rap, ownerHistory, false) as IfcRelAssignsToProduct;
				foreach (IfcObjectDefinition od in rap.RelatedObjects)
				{
					IfcObjectDefinition dup = db.Factory.Duplicate(od, ownerHistory, false) as IfcObjectDefinition;
					rp.AddRelated(dup);
				}
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
			if (mRepresentation > 0 && !typeof(IfcRoot).IsAssignableFrom(type))
				result.AddRange(Representation.Extract<T>());
			return result;
		}
		internal override void changeSchema(ReleaseVersion schema)
		{
			IfcProductRepresentation rep = Representation;
			if (rep != null)
				rep.changeSchema(schema);

			base.changeSchema(schema);
		}

		public void Assign(IfcRelAssignsToProduct assigns) { mReferencedBy.Add(assigns); }
		public void Remove(IfcRelAssignsToProduct assigns) { mReferencedBy.Remove(assigns); }
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcProductsOfCombustionProperties	 // DEPRECEATED IFC4
	public partial class IfcProductDefinitionShape : IfcProductRepresentation, IfcProductRepresentationSelect
	{
		//INVERSE
		internal List<IfcProduct> mShapeOfProduct = new List<IfcProduct>();
		internal List<IfcShapeAspect> mHasShapeAspects = new List<IfcShapeAspect>();

		public ReadOnlyCollection<IfcProduct> ShapeOfProduct { get { return new ReadOnlyCollection<IfcProduct>(mShapeOfProduct); } }
		public ReadOnlyCollection<IfcShapeAspect> HasShapeAspects { get { return new ReadOnlyCollection<IfcShapeAspect>(mHasShapeAspects); } }

		public new ReadOnlyCollection<IfcShapeModel> Representations { get { return new ReadOnlyCollection<IfcShapeModel>(base.Representations.Cast<IfcShapeModel>().ToList()); } }

		internal IfcProductDefinitionShape() : base() { }
		public IfcProductDefinitionShape(IfcShapeModel rep) : base(rep) { }
		public IfcProductDefinitionShape(List<IfcShapeModel> reps) : base(reps.ConvertAll(x => x as IfcRepresentation)) { }
		internal IfcProductDefinitionShape(DatabaseIfc db, IfcProductDefinitionShape s) : base(db, s)
		{
#warning todo
			//internal List<IfcShapeAspect> mHasShapeAspects = new List<IfcShapeAspect>();
		}
		
		public void AddRepresentation(IfcShapeModel representation) { base.AddRepresentation(representation); }
		public void AddShapeAspect(IfcShapeAspect aspect) { mHasShapeAspects.Add(aspect); }
	}
	public partial class IfcProductRepresentation : BaseClassIfc //(IfcMaterialDefinitionRepresentation ,IfcProductDefinitionShape)); //IFC4 Abstract
	{
		private string mName = "$";// : OPTIONAL IfcLabel;
		private string mDescription = "$";// : OPTIONAL IfcText;
		internal List<int> mRepresentations = new List<int>();// : LIST [1:?] OF IfcRepresentation; 

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcRepresentation> Representations
		{
			get { return new ReadOnlyCollection<IfcRepresentation>(mRepresentations.ConvertAll(x => mDatabase[x] as IfcRepresentation)); }
		}

		internal IfcProductRepresentation() : base() { }
		public IfcProductRepresentation(IfcRepresentation r) : base(r.mDatabase) { mRepresentations.Add(r.mIndex); r.mOfProductRepresentation.Add(this); }
		public IfcProductRepresentation(List<IfcRepresentation> reps) : base(reps[0].mDatabase) { reps.ForEach(x => AddRepresentation(x)); }
		internal IfcProductRepresentation(DatabaseIfc db, IfcProductRepresentation r) : base(db, r)
		{
			mName = r.mName;
			mDescription = r.mDescription;
			r.Representations.ToList().ConvertAll(x => db.Factory.Duplicate(x) as IfcRepresentation).ForEach(x => AddRepresentation(x));
		}

		protected void AddRepresentation(IfcRepresentation representation)
		{
			if (representation != null)
			{
				mRepresentations.Add(representation.mIndex);
				representation.mOfProductRepresentation.Add(this);
			}
		}
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
		void Assign(IfcRelAssignsToProduct assigns);
		void Remove(IfcRelAssignsToProduct assigns);
		string GlobalId { get; }
	}
	public partial class IfcProfileDef : BaseClassIfc, IfcResourceObjectSelect // SUPERTYPE OF (ONEOF (IfcArbitraryClosedProfileDef ,IfcArbitraryOpenProfileDef
	{  //,IfcCompositeProfileDef ,IfcDerivedProfileDef ,IfcParameterizedProfileDef));  IFC2x3 abstract 
		internal IfcProfileTypeEnum mProfileType = IfcProfileTypeEnum.AREA;// : IfcProfileTypeEnum;
		private string mProfileName = "$";// : OPTIONAL IfcLabel; 
										  //INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcProfileProperties> mHasProperties = new List<IfcProfileProperties>();
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		public IfcProfileTypeEnum ProfileType { get { return mProfileType; } set { mProfileType = value; } }
		public string ProfileName { get { return mProfileName == "$" ? "" : ParserIfc.Decode(mProfileName); } set { mProfileName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public override string Name { get { return ProfileName; } set { ProfileName = value; } }
		public ReadOnlyCollection<IfcExternalReferenceRelationship> HasExternalReferences { get { return new ReadOnlyCollection<IfcExternalReferenceRelationship>(mHasExternalReferences); } }
		public ReadOnlyCollection<IfcProfileProperties> HasProperties { get { return new ReadOnlyCollection<IfcProfileProperties>(mHasProperties); } }

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
			if (db != null && db.mRelease == ReleaseVersion.IFC2x3)
				mHasProperties.Add(new IfcGeneralProfileProperties(ProfileName, this));
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

		public void AddExternalReferenceRelationship(IfcExternalReferenceRelationship referenceRelationship) { mHasExternalReferences.Add(referenceRelationship); }
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	public partial class IfcProfileProperties : IfcExtendedProperties //IFC2x3 Abstract : BaseClassIfc ABSTRACT SUPERTYPE OF	(ONEOF(IfcGeneralProfileProperties, IfcRibPlateProfileProperties));
	{
		public override string KeyWord { get { return mDatabase.Release == ReleaseVersion.IFC2x3 ? base.KeyWord : "IfcProfileProperties"; } }
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
		protected IfcProfileProperties(IfcProfileDef p) : this("", p) { Name = this.GetType().Name; }
		internal IfcProfileProperties(string name, IfcProfileDef p) : base(p.mDatabase)
		{
			Name = name;
			ProfileDefinition = p;
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
				mAssociates = new IfcRelAssociatesProfileProperties(this) { Name = p.ProfileName };
		}
		internal IfcProfileProperties(string name, List<IfcProperty> props, IfcProfileDef p) : base(name, props)
		{
			mProfileDefinition = p.mIndex;
			p.mHasProperties.Add(this);
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
				mAssociates = new IfcRelAssociatesProfileProperties(this) { Name = p.ProfileName };
		}
	}
	public partial class IfcProject : IfcContext
	{
		internal string ProjectNumber { get { return Name; } set { Name = (string.IsNullOrEmpty(value) ? "UNKNOWN PROJECT" : value); } }

		internal IfcProject() : base() { }
		internal IfcProject(DatabaseIfc db, IfcProject p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { }
		public IfcProject(IfcBuilding building, string projectNumber) : this(building.mDatabase, projectNumber) { new IfcRelAggregates(this, building); }
		public IfcProject(IfcSite site, string projectNumber) : this(site.mDatabase, projectNumber) { new IfcRelAggregates(this, site); }
		public IfcProject(IfcBuilding building, string projectNumber, IfcUnitAssignment.Length length) : this(building.mDatabase, projectNumber, length) { new IfcRelAggregates(this, building); }
		public IfcProject(IfcSite site, string projectNumber, IfcUnitAssignment.Length length) : this(site.mDatabase, projectNumber, length) { new IfcRelAggregates(this, site); }
		public IfcProject(DatabaseIfc db, string projectNumber) : base(db, projectNumber)
		{
			db.mContext = this;
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECT";
		}
		private IfcProject(DatabaseIfc db, string projectNumber, IfcUnitAssignment.Length length) : base(db, projectNumber, length)
		{
			db.mContext = this;
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
	public partial class IfcProjectionElement : IfcFeatureElementAddition
	{
		internal IfcProjectionElementTypeEnum mPredefinedType = IfcProjectionElementTypeEnum.NOTDEFINED;// :	OPTIONAL IfcProjectionElementTypeEnum; //IFC4
		public IfcProjectionElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		//INVERSE
		internal IfcProjectionElement() : base() { }
		internal IfcProjectionElement(DatabaseIfc db, IfcProjectionElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream){ mPredefinedType = e.mPredefinedType; }
	}
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
			if (schema == ReleaseVersion.IFC2x3)
			{
				IfcBuilding building = new IfcBuilding(mDatabase, Name);
				IfcProject project = new IfcProject(building, Name) { UnitsInContext = UnitsInContext };
				List<IfcTypeObject> types = DeclaredTypes;
				for (int icounter = 0; icounter < types.Count; icounter++)
				{
					IfcTypeObject tp = types[icounter];
					tp.changeSchema(schema);
					IfcTypeProduct typeP = tp as IfcTypeProduct;
					if (typeP != null)
					{
						if (typeP.mRepresentationMaps.Count > 0)
							typeP.genMappedItemElement(building, new IfcCartesianTransformationOperator3D(mDatabase));
					}

					ReadOnlyCollection<IfcPropertySetDefinition> psets = tp.HasPropertySets;
					foreach (IfcPropertySetDefinition pset in psets)
					{
						if (pset != null && pset.IsInstancePropertySet)
							tp.mHasPropertySets.Remove(pset.mIndex);
					}
				}
				mDatabase[mIndex] = null;
				return;
			}
		}
	}
	public partial class IfcProjectOrder : IfcControl
	{
		//internal string mID;// : IfcIdentifier; IFC4 relocated 
		internal IfcProjectOrderTypeEnum mPredefinedType;// : IfcProjectOrderTypeEnum;
		internal string mStatus = "$";// : OPTIONAL IfcLabel; 
		internal string mLongDescription = "$"; //	 :	OPTIONAL IfcText;
		internal IfcProjectOrder() : base() { }
		internal IfcProjectOrder(DatabaseIfc db, IfcProjectOrder o, IfcOwnerHistory ownerHistory, bool downStream) : base(db, o, ownerHistory, downStream) { mPredefinedType = o.mPredefinedType; mStatus = o.mStatus; mLongDescription = o.mLongDescription; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcProjectOrderRecord : IfcControl // DEPRECEATED IFC4
	{
		internal List<int> mRecords = new List<int>();// : LIST [1:?] OF UNIQUE IfcRelAssignsToProjectOrder;
		internal IfcProjectOrderRecordTypeEnum mPredefinedType;// : IfcProjectOrderRecordTypeEnum; 
															   //public List<ifcrelassignstopr>
		internal IfcProjectOrderRecord() : base() { }
		internal IfcProjectOrderRecord(DatabaseIfc db, IfcProjectOrderRecord r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { }// Records = r.Records mPredefinedType = i.mPredefinedType; }
	}
	public abstract partial class IfcProperty : IfcPropertyAbstraction  //ABSTRACT SUPERTYPE OF (ONEOF(IfcComplexProperty,IfcSimpleProperty));
	{
		internal string mName; //: IfcIdentifier;
		internal string mDescription = "$"; //: OPTIONAL IfcText;
		//INVERSE
		internal List<IfcPropertySet> mPartOfPset = new List<IfcPropertySet>();//	:	SET OF IfcPropertySet FOR HasProperties;
		internal List<IfcPropertyDependencyRelationship> mPropertyForDependance = new List<IfcPropertyDependencyRelationship>();//	:	SET OF IfcPropertyDependencyRelationship FOR DependingProperty;
		internal List<IfcPropertyDependencyRelationship> mPropertyDependsOn = new List<IfcPropertyDependencyRelationship>();//	:	SET OF IfcPropertyDependencyRelationship FOR DependantProperty;
		internal List<IfcComplexProperty> mPartOfComplex = new List<IfcComplexProperty>();//	:	SET OF IfcComplexProperty FOR HasProperties;

		public override string Name { get { return ParserIfc.Decode(mName); } set { mName = string.IsNullOrEmpty(value) ? "Unknown" : ParserIfc.Encode(value); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcPropertySet> PartOfPset { get { return new ReadOnlyCollection<IfcPropertySet>(mPartOfPset); } }

		protected IfcProperty() : base() { }
		protected IfcProperty(DatabaseIfc db, IfcProperty p) : base(db, p) { mName = p.mName; mDescription = p.mDescription; }
		protected IfcProperty(DatabaseIfc db, string name) : base(db) { Name = name; }

		public override bool Destruct(bool children)
		{
			return (mPartOfPset.Count == 0 && mPartOfComplex.Count == 0 ? base.Destruct(children) : false);
		}
	}
	public abstract partial class IfcPropertyAbstraction : BaseClassIfc, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcExtendedProperties ,IfcPreDefinedProperties ,IfcProperty ,IfcPropertyEnumeration));
	{ //INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4 
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public ReadOnlyCollection<IfcExternalReferenceRelationship> HasExternalReferences { get { return new ReadOnlyCollection<IfcExternalReferenceRelationship>(mHasExternalReferences); } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>(mHasConstraintRelationships); } }
		protected IfcPropertyAbstraction() : base() { }
		protected IfcPropertyAbstraction(DatabaseIfc db) : base(db) { }
		protected IfcPropertyAbstraction(DatabaseIfc db, IfcPropertyAbstraction p) : base(db, p) { }

		public void AddExternalReferenceRelationship(IfcExternalReferenceRelationship referenceRelationship) { mHasExternalReferences.Add(referenceRelationship); }
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
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
				Unit = p.mDatabase[p.mUnit] as IfcUnit;
			mSetPointValue = p.mSetPointValue;
		}
		internal IfcPropertyBoundedValue(DatabaseIfc db, string name, IfcValue upper, IfcValue lower, IfcUnit unit, IfcValue set)
			: base(db, name)
		{
			mUpperBoundValue = upper;
			mLowerBoundValue = lower;
			mSetPointValue = set;
			Unit = unit;
		}
	}
	public partial class IfcPropertyBoundedValue<T> : IfcSimpleProperty where T : IfcValue
	{
		public override string KeyWord => "IfcPropertyBoundedValue";
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
		internal IfcPropertyBoundedValue(DatabaseIfc db, string name, T upper, T lower, IfcUnit unit, T set)
			: base(db, name)
		{
			mUpperBoundValue = upper;
			mLowerBoundValue = lower;
			mSetPointValue = set;
			Unit = unit;
		}
	}
	public abstract partial class IfcPropertyDefinition : IfcRoot, IfcDefinitionSelect //(IfcPropertySetDefinition, IfcPropertyTemplateDefinition)
	{ //INVERSE
		internal IfcRelDeclares mHasContext = null;// :	SET [0:1] OF IfcRelDeclares FOR RelatedDefinitions;
		internal List<IfcRelAssociates> mHasAssociations = new List<IfcRelAssociates>();//	 : 	SET OF IfcRelAssociates FOR RelatedObjects;

		public IfcRelDeclares HasContext { get { return mHasContext; } set { mHasContext = value; } }
		public ReadOnlyCollection<IfcRelAssociates> HasAssociations { get { return new ReadOnlyCollection<IfcRelAssociates>(mHasAssociations); } }

		protected IfcPropertyDefinition() : base() { }
		internal IfcPropertyDefinition(DatabaseIfc db) : base(db) { }
		protected IfcPropertyDefinition(DatabaseIfc db, IfcPropertyDefinition p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory)
		{
			foreach (IfcRelAssociates associates in mHasAssociations)
			{
				IfcRelAssociates dup = db.Factory.Duplicate(associates) as IfcRelAssociates;
				dup.addRelated(this);
			}
		}

		public virtual void Associate(IfcRelAssociates a) { mHasAssociations.Add(a); }
		public void Remove(IfcRelAssociates associates) { mHasAssociations.Remove(associates); }
	}
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
			: base(depending.Database) { DependingProperty = depending; DependantProperty = dependant; }
	}
	public partial class IfcPropertyEnumeratedValue : IfcSimpleProperty
	{
		internal List<IfcValue> mEnumerationValues = new List<IfcValue>();// : LIST [1:?] OF IfcValue;
		internal int mEnumerationReference;// : OPTIONAL IfcPropertyEnumeration;   

		public ReadOnlyCollection<IfcValue> EnumerationValues { get { return new ReadOnlyCollection<IfcValue>(mEnumerationValues); } }
		public IfcPropertyEnumeration EnumerationReference { get { return mDatabase[mEnumerationReference] as IfcPropertyEnumeration; } set { mEnumerationReference = value == null ? 0 : value.mIndex; } }

		internal IfcPropertyEnumeratedValue() : base() { }
		public IfcPropertyEnumeratedValue(DatabaseIfc db, string name, IfcValue value) : base(db, name) { mEnumerationValues.Add(value); }
		public IfcPropertyEnumeratedValue(DatabaseIfc db, string name, IEnumerable<IfcValue> values) : base(db, name) { mEnumerationValues.AddRange(values); }
		public IfcPropertyEnumeratedValue(string name, IfcValue value, IfcPropertyEnumeration reference) : base(reference.Database, name) { mEnumerationValues.Add(value); EnumerationReference = reference; }
		public IfcPropertyEnumeratedValue(string name, IEnumerable<IfcValue> values, IfcPropertyEnumeration reference) : base(reference.Database, name) { mEnumerationValues.AddRange(values); EnumerationReference = reference; }
	}
	public partial class IfcPropertyEnumeration : IfcPropertyAbstraction
	{
		internal string mName;//	 :	IfcLabel;
		internal List<IfcValue> mEnumerationValues = new List<IfcValue>();// :	LIST [1:?] OF UNIQUE IfcValue
		internal int mUnit; //	 :	OPTIONAL IfcUnit;

		public override string Name { get { return ParserIfc.Decode(mName); } set { mName = (string.IsNullOrEmpty(value) ? "Unknown" : ParserIfc.Encode(value)); } }
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
	public partial class IfcPropertyReferenceValue : IfcSimpleProperty
	{
		internal string mUsageName = "$";// 	 :	OPTIONAL IfcText;
		internal int mPropertyReference = 0;// 	 :	OPTIONAL IfcObjectReferenceSelect;

		public string UsageName { get { return (mUsageName == "$" ? "" : ParserIfc.Decode(mUsageName)); } set { mUsageName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcObjectReferenceSelect PropertyReference { get { return  mDatabase[mPropertyReference] as IfcObjectReferenceSelect; } set { mPropertyReference = (value == null ? 0 : value.Index); } }

		internal IfcPropertyReferenceValue() : base() { }
		internal IfcPropertyReferenceValue(DatabaseIfc db, IfcPropertyReferenceValue p) : base(db, p)
		{
			mUsageName = p.mUsageName;
#warning todo
			//if(p.mPropertyReference > 0)
			//	PropertyReference = db.Factory.Duplicate( p.PropertyReference) as ;
		}
		public IfcPropertyReferenceValue(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPropertyReferenceValue(string name, IfcObjectReferenceSelect obj) : base(obj.Database,name) { PropertyReference = obj; }
	}
	public partial class IfcPropertySet : IfcPropertySetDefinition
	{
		public override string KeyWord { get { return "IfcPropertySet"; } }
		 Dictionary<string,IfcProperty> mHasProperties = new Dictionary<string, IfcProperty>();// : SET [1:?] OF IfcProperty;
		private List<int> mPropertyIndices = new List<int>();

		public ReadOnlyDictionary<string,IfcProperty> HasProperties { get { return new ReadOnlyDictionary<string, IfcProperty>( mHasProperties); } }

		internal override void Initialize()
		{
			mHasProperties = new Dictionary<string, IfcProperty>();
			mPropertyIndices = new List<int>();
		}

		internal IfcPropertySet() : base() { }
		protected IfcPropertySet(IfcObjectDefinition obj) : base(obj.mDatabase,"") { Name = this.GetType().Name; DefinesOccurrence.AddRelated(obj); }
		protected IfcPropertySet(IfcTypeObject type) : base(type.mDatabase,"") { Name = this.GetType().Name; type.AddPropertySet(this); }
		internal IfcPropertySet(DatabaseIfc db, IfcPropertySet s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { s.mPropertyIndices.ForEach(x => AddProperty( db.Factory.Duplicate(s.mDatabase[x]) as IfcProperty)); }
		public IfcPropertySet(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPropertySet(string name, IfcProperty prop) : base(prop.mDatabase, name) { AddProperty(prop); }
		public IfcPropertySet(string name, IEnumerable<IfcProperty> props) : base(props.First().mDatabase, name) { foreach(IfcProperty p in props) AddProperty(p);  }
		
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcProperty p in mHasProperties.Values)
				result.AddRange(p.Extract<T>());
			return result;
		}
		internal override bool isEmpty { get { return mHasProperties.Count == 0; } }
		public void AddProperty(IfcProperty property)
		{
			if(property != null && !mHasProperties.ContainsKey(property.Name))
			{
				mHasProperties.Add(property.Name, property);
				property.mPartOfPset.Add(this);
				mPropertyIndices.Add(property.Index);
			}
		}
		public void RemoveProperty(IfcProperty property)
		{
			if (property != null)
			{
				mHasProperties.Remove(property.Name);
				mPropertyIndices.Remove(property.Index);
			}
		}
		public IfcProperty FindProperty(string name)
		{
			if (string.IsNullOrEmpty(name))
				return null;
			return (mHasProperties.ContainsKey(name) ? mHasProperties[name] : null);
		}
		public void SetProperties(IEnumerable<IfcProperty> properties)
		{
			mHasProperties.Clear();
			mPropertyIndices.Clear();
			foreach (IfcProperty property in properties)
				AddProperty(property);
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
	public abstract partial class IfcPropertySetDefinition : IfcPropertyDefinition //ABSTRACT SUPERTYPE OF (ONEOF (IfcElementQuantity,IfcEnergyProperties ,
	{ // IfcFluidFlowProperties,IfcPropertySet, IfcServiceLifeFactor, IfcSoundProperties ,IfcSoundValue ,IfcSpaceThermalLoadProperties ))
		//INVERSE
		internal List<IfcTypeObject> mDefinesType = new List<IfcTypeObject>();// :	SET OF IfcTypeObject FOR HasPropertySets; IFC4change
		internal List<IfcRelDefinesByTemplate> mIsDefinedBy = new List<IfcRelDefinesByTemplate>();//IsDefinedBy	 :	SET OF IfcRelDefinesByTemplate FOR RelatedPropertySets;
		private IfcRelDefinesByProperties mDefinesOccurrence = null; //:	SET [0:1] OF IfcRelDefinesByProperties FOR RelatingPropertyDefinition;
		
		public ReadOnlyCollection<IfcTypeObject> DefinesType { get { return new ReadOnlyCollection<IfcTypeObject>( mDefinesType); } }
		public ReadOnlyCollection<IfcRelDefinesByTemplate> IsDefinedBy { get { return new ReadOnlyCollection<IfcRelDefinesByTemplate>(mIsDefinedBy); } }
		public IfcRelDefinesByProperties DefinesOccurrence { get { if (mDefinesOccurrence == null) mDefinesOccurrence = new IfcRelDefinesByProperties(this) { Name = Name }; return mDefinesOccurrence; } set { mDefinesOccurrence = value; } }

		protected IfcPropertySetDefinition() : base() { }
		protected IfcPropertySetDefinition(DatabaseIfc db, IfcPropertySetDefinition s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { }
		protected IfcPropertySetDefinition(DatabaseIfc m, string name) : base(m) { Name = name; }

		internal void AssignType(IfcTypeObject to) { mDefinesType.Add(to); to.mHasPropertySets.Add(mIndex); }
		public void AssignObjectDefinition(IfcObjectDefinition od)
		{
			IfcTypeObject to = od as IfcTypeObject;
			if (to != null)
				AssignType(to);
			else
			{
				if (mDefinesOccurrence == null)
					mDefinesOccurrence = new IfcRelDefinesByProperties(od, this);
				else
					mDefinesOccurrence.AddRelated(od);
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
	public partial class IfcPropertySetTemplate : IfcPropertyTemplateDefinition
	{
		internal IfcPropertySetTemplateTypeEnum mTemplateType = Ifc.IfcPropertySetTemplateTypeEnum.NOTDEFINED;//	:	OPTIONAL IfcPropertySetTemplateTypeEnum;
		internal string mApplicableEntity = "$";//	:	OPTIONAL IfcIdentifier;
		protected List<int> mHasPropertyTemplates = new List<int>(1);// : SET [1:?] OF IfcPropertyTemplate;
		//INVERSE
		internal List<IfcRelDefinesByTemplate> mDefines = new List<IfcRelDefinesByTemplate>();//	:	SET OF IfcRelDefinesByTemplate FOR RelatingTemplate;

		public IfcPropertySetTemplateTypeEnum TemplateType { get { return mTemplateType; } set { mTemplateType = value; } }
		public string ApplicableEntity { get { return (mApplicableEntity == "$" ? "" : ParserIfc.Decode(mApplicableEntity)); } set { mApplicableEntity = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcPropertyTemplate> HasPropertyTemplates { get { return new ReadOnlyCollection<IfcPropertyTemplate>(mHasPropertyTemplates.ConvertAll(x => mDatabase[x] as IfcPropertyTemplate)); } }

		internal IfcPropertySetTemplate() : base() { }
		public IfcPropertySetTemplate(DatabaseIfc db, IfcPropertySetTemplate p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream)
		{
			mTemplateType = p.mTemplateType;
			mApplicableEntity = p.mApplicableEntity;
			p.HasPropertyTemplates.ToList().ForEach(x => addPropertyTemplate(db.Factory.Duplicate(x) as IfcPropertyTemplate));
		}
		//public IfcPropertySetTemplate(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPropertySetTemplate(string name, List<IfcPropertyTemplate> props) : base(props[0].mDatabase, name) { props.ForEach(x=>addPropertyTemplate(x)); }
		
		internal void addPropertyTemplate(IfcPropertyTemplate template) { mHasPropertyTemplates.Add(template.mIndex); template.mPartOfPsetTemplate.Add(this); }
		//internal void relate()
		//{
		//	List<IfcPropertyTemplate> props = HasPropertyTemplates;
		//	for (int icounter = 0; icounter < props.Count; icounter++)
		//		props[icounter].mPartOfPsetTemplate.Add(this);
		//}
	}
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
	}
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
		internal IfcPropertyTableValue(DatabaseIfc db, string name) : base(db, name) { }
	}
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
		internal IfcPropertyTableValue(DatabaseIfc db, string name) : base(db, name) { }
	}
	public abstract partial class IfcPropertyTemplate : IfcPropertyTemplateDefinition    //ABSTRACT SUPERTYPE OF(ONEOF(IfcComplexPropertyTemplate, IfcSimplePropertyTemplate))
	{   //INVERSE
		//PartOfComplexTemplate	:	SET OF IfcComplexPropertyTemplate FOR HasPropertyTemplates;
		internal List<IfcPropertySetTemplate> mPartOfPsetTemplate = new List<IfcPropertySetTemplate>();//	:	SET OF IfcPropertySetTemplate FOR HasPropertyTemplates;

		protected IfcPropertyTemplate() : base() { }
		protected IfcPropertyTemplate(DatabaseIfc db, IfcPropertyTemplate t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
		protected IfcPropertyTemplate(DatabaseIfc db, string name) : base(db, name) { }
	}
	public abstract partial class IfcPropertyTemplateDefinition : IfcPropertyDefinition // ABSTRACT SUPERTYPE OF(ONEOF(IfcPropertySetTemplate, IfcPropertyTemplate))
	{ 
	 	protected IfcPropertyTemplateDefinition() : base() { }
		protected IfcPropertyTemplateDefinition(DatabaseIfc db, IfcPropertyTemplateDefinition p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { }
		protected IfcPropertyTemplateDefinition(DatabaseIfc m, string name) : base(m) { Name = name; }
	}
	public partial class IfcProtectiveDevice : IfcFlowController //IFC4
	{
		internal IfcProtectiveDeviceTypeEnum mPredefinedType = IfcProtectiveDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcProtectiveDeviceTypeEnum;
		public IfcProtectiveDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDevice() : base() { }
		internal IfcProtectiveDevice(DatabaseIfc db, IfcProtectiveDevice d, IfcOwnerHistory ownerHistory, bool downStream) : base(db, d, ownerHistory, downStream) { mPredefinedType = d.mPredefinedType; }
		public IfcProtectiveDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	public partial class IfcProtectiveDeviceTrippingUnit : IfcDistributionControlElement //IFC4  
	{
		internal IfcProtectiveDeviceTrippingUnitTypeEnum mPredefinedType = IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED;
		public IfcProtectiveDeviceTrippingUnitTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceTrippingUnit() : base() { }
		internal IfcProtectiveDeviceTrippingUnit(DatabaseIfc db, IfcProtectiveDeviceTrippingUnit u, IfcOwnerHistory ownerHistory, bool downStream) : base(db,u, ownerHistory, downStream) { mPredefinedType = u.mPredefinedType; }
		public IfcProtectiveDeviceTrippingUnit(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	public partial class IfcProtectiveDeviceTrippingUnitType : IfcDistributionControlElementType
	{
		internal IfcProtectiveDeviceTrippingUnitTypeEnum mPredefinedType = IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED;// : IfcProtectiveDeviceTrippingUnitTypeEnum;
		public IfcProtectiveDeviceTrippingUnitTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceTrippingUnitType() : base() { }
		internal IfcProtectiveDeviceTrippingUnitType(DatabaseIfc db, IfcProtectiveDeviceTrippingUnitType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		internal IfcProtectiveDeviceTrippingUnitType(DatabaseIfc m, string name, IfcProtectiveDeviceTrippingUnitTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	public partial class IfcProtectiveDeviceType : IfcFlowControllerType
	{
		internal IfcProtectiveDeviceTypeEnum mPredefinedType = IfcProtectiveDeviceTypeEnum.NOTDEFINED;// : IfcProtectiveDeviceTypeEnum; 
		public IfcProtectiveDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceType() : base() { }
		internal IfcProtectiveDeviceType(DatabaseIfc db, IfcProtectiveDeviceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		internal IfcProtectiveDeviceType(DatabaseIfc m, string name, IfcProtectiveDeviceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	public partial class IfcProxy : IfcProduct
	{
		internal IfcObjectTypeEnum mProxyType;// : IfcObjectTypeEnum;
		internal string mTag = "$";// : OPTIONAL IfcLabel;
		internal IfcProxy() : base() { }
		internal IfcProxy(DatabaseIfc db, IfcProxy p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mProxyType = p.mProxyType; mTag = p.mTag; }
	}
	public partial class IfcPump : IfcFlowMovingDevice //IFC4
	{
		internal IfcPumpTypeEnum mPredefinedType = IfcPumpTypeEnum.NOTDEFINED;// OPTIONAL : IfcPumpTypeEnum;
		public IfcPumpTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPump() : base() { }
		internal IfcPump(DatabaseIfc db, IfcPump p, IfcOwnerHistory ownerHistory, bool downStream) : base(db,p, ownerHistory, downStream) { mPredefinedType = p.mPredefinedType; }
		public IfcPump(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	public partial class IfcPumpType : IfcFlowMovingDeviceType
	{
		internal IfcPumpTypeEnum mPredefinedType = IfcPumpTypeEnum.NOTDEFINED;// : IfcPumpTypeEnum; 
		public IfcPumpTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPumpType() : base() { }
		internal IfcPumpType(DatabaseIfc db, IfcPumpType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcPumpType(DatabaseIfc m, string name, IfcPumpTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
}
