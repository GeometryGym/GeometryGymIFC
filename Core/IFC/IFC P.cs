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
		protected IfcParameterizedProfileDef(DatabaseIfc m,string name) : base(m,name)
		{
			if (mDatabase.mModelView == ModelView.Ifc4Reference)
				throw new Exception("Invalid Model View for IfcParameterizedProfileDef : " + m.ModelView.ToString());
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
				Position = mDatabase.Factory.Origin2dPlace;
		}

		protected static void parseFields(IfcParameterizedProfileDef p, List<string> arrFields, ref int ipos) { IfcProfileDef.parseFields(p, arrFields, ref ipos); p.mPosition = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPosition); }

		internal override void changeSchema(ReleaseVersion schema)
		{
			IfcAxis2Placement2D position = Position;
			if (schema == ReleaseVersion.IFC2x3)
			{
				if (position == null)
					Position = mDatabase.Factory.Origin2dPlace;
			}
			else
			{
				if (position.IsWorldXY)
					mPosition = 0;
			}
		}
	}
	public partial class IfcPath : IfcTopologicalRepresentationItem
	{
		internal List<int> mEdgeList = new List<int>();// : SET [1:?] OF IfcOrientedEdge;
		public List<IfcOrientedEdge> EdgeList { get { return mEdgeList.ConvertAll(x => mDatabase[x] as IfcOrientedEdge); } set { mEdgeList = value.ConvertAll(x => x.mIndex); } }

		internal IfcPath() : base() { }
		internal IfcPath(IfcOrientedEdge edge) : base(edge.mDatabase) { mEdgeList.Add(edge.mIndex); }
		internal IfcPath(List<IfcOrientedEdge> edges) : base(edges[0].mDatabase) { EdgeList = edges; }
		internal IfcPath(DatabaseIfc db, IfcPath p) : base(db,p) { EdgeList = p.EdgeList.ConvertAll(x=>db.Factory.Duplicate(x) as IfcOrientedEdge); }
		internal static IfcPath Parse(string strDef) { IfcPath p = new IfcPath(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcPath p, List<string> arrFields, ref int ipos) {  p.mEdgeList = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(";
			if (mEdgeList.Count > 0)
				str += ParserSTEP.LinkToString(mEdgeList[0]);
			for (int icounter = 1; icounter < mEdgeList.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mEdgeList[icounter]);
			return str + ")";
		}
	}
	public partial class IfcPCurve : IfcCurve
	{
		internal int mBasisSurface;// :	IfcSurface;
		internal int mReferenceCurve;// :	IfcCurve; 

		public IfcSurface BasisSurface { get { return mDatabase[mBasisSurface] as IfcSurface; } set { mBasisSurface = value.mIndex;  } }
		public IfcCurve ReferenceCurve { get { return mDatabase[mReferenceCurve] as IfcCurve; } set { mReferenceCurve = value.mIndex; } }

		internal IfcPCurve() : base() { }
		internal IfcPCurve(DatabaseIfc db, IfcPCurve c) : base(db,c) { BasisSurface = db.Factory.Duplicate(c.BasisSurface) as IfcSurface; ReferenceCurve = db.Factory.Duplicate(c.ReferenceCurve) as IfcCurve; }
		internal static IfcPCurve Parse(string str)
		{
			IfcPCurve c = new IfcPCurve();
			int pos = 0, len = str.Length;
			c.mBasisSurface = ParserSTEP.StripLink(str, ref pos, len);
			c.mReferenceCurve = ParserSTEP.StripLink(str, ref pos, len);
			return c;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.LinkToString(mReferenceCurve); }
	}
	public partial class IfcPerformanceHistory : IfcControl
	{
		internal string mLifeCyclePhase;// : IfcLabel; 
		internal IfcPerformanceHistory() : base() { }
		internal IfcPerformanceHistory(DatabaseIfc db, IfcPerformanceHistory h) : base(db,h) { mLifeCyclePhase = h.mLifeCyclePhase; }
		internal static IfcPerformanceHistory Parse(string strDef, ReleaseVersion schema) { IfcPerformanceHistory h = new IfcPerformanceHistory(); int ipos = 0; parseFields(h, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return h; }
		internal static void parseFields(IfcPerformanceHistory h, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcControl.parseFields(h, arrFields, ref ipos,schema); h.mLifeCyclePhase = arrFields[ipos++]; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mLifeCyclePhase; }
	}
	//ENTITY IfcPermeableCoveringProperties : IfcPreDefinedPropertySet //IFC2x3 
	public partial class IfcPermit : IfcControl
	{
		internal string mPermitID;// : IfcIdentifier; 
		internal IfcPermit() : base() { }
		internal IfcPermit(DatabaseIfc db, IfcPermit p) : base(db,p) { mPermitID = p.mPermitID; }
		internal static IfcPermit Parse(string strDef, ReleaseVersion schema) { IfcPermit p = new IfcPermit(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcPermit p, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcControl.parseFields(p, arrFields, ref ipos,schema); p.mPermitID = arrFields[ipos++]; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mPermitID; }
	}
	public partial class IfcPerson : BaseClassIfc, IfcActorSelect, IfcResourceObjectSelect
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
		public IEnumerable<string> MiddleNames { get { return mMiddleNames.ConvertAll(x => ParserIfc.Decode(x)); } set { mMiddleNames = (value == null ? new List<string>() : value.ToList().ConvertAll(x => ParserIfc.Encode(x.Replace("'", "")))); } }
		public IEnumerable<string> PrefixTitles { get { return mPrefixTitles.ConvertAll(x => ParserIfc.Decode(x)); } set { mPrefixTitles = (value == null ? new List<string>() : value.ToList().ConvertAll(x => ParserIfc.Encode(x.Replace("'", "")))); } }
		public IEnumerable<string> SuffixTitles { get { return mSuffixTitles.ConvertAll(x => ParserIfc.Decode(x)); } set { mSuffixTitles = (value == null ? new List<string>() : value.ToList().ConvertAll(x => ParserIfc.Encode(x.Replace("'", "")))); } }

		public List<IfcActorRole> Roles { get { return mRoles.ConvertAll(x => mDatabase[x] as IfcActorRole); } set { mRoles = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		public List<IfcAddress> Addresses { get { return mAddresses.ConvertAll(x => mDatabase[x] as IfcAddress); } set { mAddresses = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }

		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcPerson() : base() { }
		public IfcPerson(DatabaseIfc db) : base(db) { }
		internal IfcPerson(DatabaseIfc db, IfcPerson p) : base(db,p)
		{
			mIdentification = p.mIdentification;
			mFamilyName = p.mFamilyName;
			mGivenName = p.mGivenName;
			mMiddleNames.AddRange(p.mMiddleNames);
			mPrefixTitles.AddRange(p.mPrefixTitles);
			mSuffixTitles.AddRange(p.mSuffixTitles);
			Roles = p.Roles.ConvertAll(x => db.Factory.Duplicate(x) as IfcActorRole);
			Addresses = p.Addresses.ConvertAll(x => db.Factory.Duplicate(x) as IfcAddress);
		}
		internal IfcPerson(DatabaseIfc m, string id, string familyname, string givenName) : base(m)
		{
			Identification = id;
			FamilyName = familyname;
			GivenName = givenName;
		}
		internal static void parseFields(IfcPerson p, List<string> arrFields, ref int ipos)
		{
			p.mIdentification = arrFields[ipos++].Replace("'", "");
			p.mFamilyName = arrFields[ipos++].Replace("'", "");
			p.mGivenName = arrFields[ipos++].Replace("'", "");
			p.mMiddleNames = ParserSTEP.SplitListStrings(arrFields[ipos++]).ConvertAll(x => x.Replace("'", ""));
			p.mPrefixTitles = ParserSTEP.SplitListStrings(arrFields[ipos++]).ConvertAll(x => x.Replace("'", ""));
			p.mSuffixTitles = ParserSTEP.SplitListStrings(arrFields[ipos++]).ConvertAll(x => x.Replace("'", ""));
			p.mRoles = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			p.mAddresses = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',");
			if (mFamilyName == "$" && mGivenName == "$")
				str += (mIdentification == "$" ? "'Unknown',$," : "'" + mIdentification + "',$,");
			else
				str += (mFamilyName == "$" ? "$," : "'" + mFamilyName + "',") + (mGivenName == "$" ? "$," : "'" + mGivenName + "',");
			if (mMiddleNames.Count == 0)
				str += "$,";
			else
			{
				str += "('" + mMiddleNames[0];
				for (int icounter = 1; icounter < mMiddleNames.Count; icounter++)
					str += "','" + mMiddleNames[icounter];
				str += "'),";
			}
			if (mPrefixTitles.Count == 0)
				str += "$,";
			else
			{
				str += "('" + mPrefixTitles[0];
				for (int icounter = 1; icounter < mPrefixTitles.Count; icounter++)
					str += "','" + mPrefixTitles[icounter];
				str += "'),";
			}
			if (mSuffixTitles.Count == 0)
				str += "$,";
			else
			{
				str += "('" + mSuffixTitles[0];
				for (int icounter = 1; icounter < mSuffixTitles.Count; icounter++)
					str += "','" + mSuffixTitles[icounter];
				str += "'),";
			}
			if (mRoles.Count == 0)
				str += "$,";
			else
			{
				str += "(" + ParserSTEP.LinkToString(mRoles[0]);
				for (int icounter = 1; icounter < mRoles.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRoles[icounter]);
				str += "),";
			}
			if (mAddresses.Count == 0)
				str += "$";
			else
			{
				str += "(" + ParserSTEP.LinkToString(mAddresses[0]);
				for (int icounter = 1; icounter < mAddresses.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mAddresses[icounter]);
				str += ")";
			}
			return str;
		}
		internal static IfcPerson Parse(string strDef) { IfcPerson p = new IfcPerson(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
	}
	public partial class IfcPersonAndOrganization : BaseClassIfc, IfcActorSelect, IfcResourceObjectSelect
	{
		internal int mThePerson;// : IfcPerson;
		internal int mTheOrganization;// : IfcOrganization;
		private List<int> mRoles = new List<int>();// : OPTIONAL LIST [1:?] OF IfcActorRole;
		   //INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public IfcPerson ThePerson { get { return mDatabase[mThePerson] as IfcPerson; } set { mThePerson = value.mIndex; } }
		public IfcOrganization TheOrganization { get { return mDatabase[mTheOrganization] as IfcOrganization; } set { mTheOrganization = value.mIndex; } }
		public List<IfcActorRole> Roles { get { return mRoles.ConvertAll(x => mDatabase[x] as IfcActorRole); } set { mRoles = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }

		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcPersonAndOrganization() : base() { }
		internal IfcPersonAndOrganization(DatabaseIfc db) : base(db) { }
		internal IfcPersonAndOrganization(DatabaseIfc db, IfcPersonAndOrganization p) : base(db,p) { ThePerson = db.Factory.Duplicate(p.ThePerson) as IfcPerson; TheOrganization = db.Factory.Duplicate(p.TheOrganization) as IfcOrganization; Roles = p.Roles.ConvertAll(x => db.Factory.Duplicate(x) as IfcActorRole); }
		public IfcPersonAndOrganization(IfcPerson person, IfcOrganization organization) : base(person.mDatabase) { ThePerson = person; TheOrganization = organization; }
		internal static void parseFields(IfcPersonAndOrganization c, List<string> arrFields, ref int ipos)
		{
			c.mThePerson = ParserSTEP.ParseLink(arrFields[ipos++]);
			c.mTheOrganization = ParserSTEP.ParseLink(arrFields[ipos++]);
			c.mRoles = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		internal static IfcPersonAndOrganization Parse(string strDef) { IfcPersonAndOrganization c = new IfcPersonAndOrganization(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildStringSTEP()
		{
			string str;
			if (mRoles.Count == 0)
				str = ",$";
			else
			{
				str = ",(" + ParserSTEP.LinkToString(mRoles[0]);
				for (int icounter = 1; icounter < mRoles.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRoles[icounter]);
				str += ")";
			}
			return base.BuildStringSTEP() + ",#" + mThePerson + ",#" + mTheOrganization + str;
		}
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
		protected IfcPhysicalQuantity(DatabaseIfc db, IfcPhysicalQuantity q) : base(db,q) { mName = q.mName; mDescription = q.mDescription; }
		protected IfcPhysicalQuantity(DatabaseIfc db, string name) : base(db) { Name = name; }
		protected static void parseFields(IfcPhysicalQuantity q, List<string> arrFields, ref int ipos) { q.mName = arrFields[ipos++].Replace("'", ""); q.mDescription = arrFields[ipos++].Replace("'",""); }
		protected virtual void Parse(string str, ref int pos, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mName + (mDescription == "$" ? "',$" : "','" + mDescription + "'"); }
	}
	public abstract partial class IfcPhysicalSimpleQuantity : IfcPhysicalQuantity //ABSTRACT SUPERTYPE OF (ONEOF (IfcQuantityArea ,IfcQuantityCount ,IfcQuantityLength ,IfcQuantityTime ,IfcQuantityVolume ,IfcQuantityWeight))
	{
		internal int mUnit = 0;// : OPTIONAL IfcNamedUnit;	
		public IfcNamedUnit Unit { get { return mDatabase[mUnit] as IfcNamedUnit; } set { mUnit = (value == null ? 0 : value.mIndex); } }
		
		protected IfcPhysicalSimpleQuantity() : base() { }
		protected IfcPhysicalSimpleQuantity(DatabaseIfc db, IfcPhysicalSimpleQuantity q) : base(db, q) { if (q.mUnit > 0) Unit = db.Factory.Duplicate(q.Unit) as IfcNamedUnit; }
		protected IfcPhysicalSimpleQuantity(DatabaseIfc db,string name) : base(db,name) { }
		protected static void parseFields(IfcPhysicalSimpleQuantity q, List<string> arrFields, ref int ipos) { IfcPhysicalQuantity.parseFields(q, arrFields, ref ipos); q.mUnit = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mUnit); }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mUnit = ParserSTEP.StripLink(str, ref pos, len);
		}

		internal abstract IfcMeasureValue MeasureValue { get; }
	}
	public partial class IfcPile : IfcBuildingElement
	{
		internal IfcPileTypeEnum mPredefinedType = IfcPileTypeEnum.NOTDEFINED;// OPTIONAL : IfcPileTypeEnum;
		internal IfcPileConstructionEnum mConstructionType = IfcPileConstructionEnum.NOTDEFINED;// : OPTIONAL IfcPileConstructionEnum; IFC4 	Deprecated.

		public IfcPileTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcPileConstructionEnum ConstructionType { get { return mConstructionType; } set { mConstructionType = value; } }

		internal IfcPile() : base() { }
		internal IfcPile(DatabaseIfc db, IfcPile p) : base(db, p) { mPredefinedType = p.mPredefinedType; mConstructionType = p.mConstructionType; }
		public IfcPile(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcPile Parse(string str) { IfcPile p = new IfcPile(); int pos = 0; p.Parse(str, ref pos, str.Length); return p; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if(s.StartsWith("."))
				mPredefinedType = (IfcPileTypeEnum)Enum.Parse(typeof(IfcPileTypeEnum), s.Replace(".", ""));
			s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				mConstructionType = (IfcPileConstructionEnum)Enum.Parse(typeof(IfcPileConstructionEnum), s.Replace(".", ""));
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease != ReleaseVersion.IFC2x3 && mPredefinedType == IfcPileTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") +
				(mConstructionType == IfcPileConstructionEnum.NOTDEFINED ? "$" : "." + mConstructionType.ToString() + ".");
		}
		
	}
	public partial class IfcPileType : IfcBuildingElementType
	{
		internal IfcPileTypeEnum mPredefinedType = IfcPileTypeEnum.NOTDEFINED;
		public IfcPileTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPileType() : base() { }
		internal IfcPileType(DatabaseIfc db, IfcPileType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcPileType(DatabaseIfc m, string name, IfcPileTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal IfcPileType(string name, IfcMaterialProfileSet mps, IfcPileTypeEnum type) : base(mps.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = mps; }

		internal static void parseFields(IfcStairType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcStairTypeEnum)Enum.Parse(typeof(IfcStairTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcStairType Parse(string strDef) { IfcStairType t = new IfcStairType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcPipeFitting : IfcFlowFitting //IFC4
	{
		internal IfcPipeFittingTypeEnum mPredefinedType = IfcPipeFittingTypeEnum.NOTDEFINED;	// :	OPTIONAL IfcPipeFittingTypeEnum;
		public IfcPipeFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeFitting() : base() { }
		internal IfcPipeFitting(DatabaseIfc db, IfcPipeFitting f) : base(db,f) { mPredefinedType = f.mPredefinedType; }
		public IfcPipeFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcPipeFitting s, List<string> arrFields, ref int ipos)
		{
			IfcFlowFitting.parseFields(s, arrFields, ref ipos);
			if (ipos < arrFields.Count)
			{
				string str = arrFields[ipos++];
				if (str.StartsWith("."))
					s.mPredefinedType = (IfcPipeFittingTypeEnum)Enum.Parse(typeof(IfcPipeFittingTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		internal new static IfcPipeFitting Parse(string strDef) { IfcPipeFitting s = new IfcPipeFitting(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcPipeFittingTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcPipeFittingType : IfcFlowFittingType
	{
		internal IfcPipeFittingTypeEnum mPredefinedType = IfcPipeFittingTypeEnum.NOTDEFINED;// : IfcPipeFittingTypeEnum; 
		public IfcPipeFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeFittingType() : base() { }
		internal IfcPipeFittingType(DatabaseIfc db, IfcPipeFittingType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcPipeFittingType(DatabaseIfc m, string name, IfcPipeFittingTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal IfcPipeFittingType(DatabaseIfc m, string name, double radius, double bendAngle) : base(m)
		{
			Name = name;
			mHasPropertySets.Add(genPSetBend(m, bendAngle, radius).mIndex);
			mPredefinedType = IfcPipeFittingTypeEnum.BEND;
		}

		internal static void parseFields(IfcPipeFittingType t, List<string> arrFields, ref int ipos) { IfcFlowFittingType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcPipeFittingTypeEnum)Enum.Parse(typeof(IfcPipeFittingTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcPipeFittingType Parse(string strDef) { IfcPipeFittingType t = new IfcPipeFittingType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }

		internal static IfcPropertySet genPSetBend(DatabaseIfc m, double bendAngle, double bendRadius)
		{
			List<IfcProperty> props = new List<IfcProperty>();
			if (bendAngle > 0)
				props.Add(new IfcPropertySingleValue(m, "BendAngle", "", new IfcPlaneAngleMeasure(bendAngle), null));
			if (bendRadius > 0)
				props.Add(new IfcPropertySingleValue(m, "BendRadius", "", new IfcPositiveLengthMeasure(bendRadius), null));
			if (props.Count == 0)
				return null;
			return new IfcPropertySet("Pset_PipeFittingTypeBend", props);

		}
	}
	public partial class IfcPipeSegment : IfcFlowSegment //IFC4
	{
		internal IfcPipeSegmentTypeEnum mPredefinedType = IfcPipeSegmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcPipeSegmentTypeEnum;
		public IfcPipeSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeSegment() : base() { }
		internal IfcPipeSegment(DatabaseIfc db, IfcPipeSegment s) : base(db,s) { mPredefinedType = s.mPredefinedType; }
		public IfcPipeSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcPipeSegment s, List<string> arrFields, ref int ipos)
		{
			IfcFlowSegment.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcPipeSegmentTypeEnum)Enum.Parse(typeof(IfcPipeSegmentTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcPipeSegment Parse(string strDef) { IfcPipeSegment s = new IfcPipeSegment(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcPipeSegmentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcPipeSegmentType : IfcFlowSegmentType
	{
		internal IfcPipeSegmentTypeEnum mPredefinedType = IfcPipeSegmentTypeEnum.NOTDEFINED;// : IfcPipeSegmentTypeEnum; 
		public IfcPipeSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeSegmentType() : base() { }
		internal IfcPipeSegmentType(DatabaseIfc db, IfcPipeSegmentType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcPipeSegmentType(DatabaseIfc m, string name, IfcPipeSegmentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcPipeSegmentType t, List<string> arrFields, ref int ipos) { IfcFlowSegmentType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcPipeSegmentTypeEnum)Enum.Parse(typeof(IfcPipeSegmentTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcPipeSegmentType Parse(string strDef) { IfcPipeSegmentType t = new IfcPipeSegmentType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
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
		internal static IfcPixelTexture Parse(string strDef, ReleaseVersion schema) { IfcPixelTexture t = new IfcPixelTexture(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return t; }
		internal static void parseFields(IfcPixelTexture t, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			try
			{
				IfcSurfaceTexture.parseFields(t, arrFields, ref ipos, schema);
				t.mWidth = int.Parse( arrFields[ipos++]);
				t.mHeight = int.Parse(arrFields[ipos++]);
				t.mColourComponents = int.Parse(arrFields[ipos++]);
				t.mPixel = ParserSTEP.SplitListStrings(arrFields[ipos++]);
			}
			catch (Exception) { }
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mWidth + "," + mHeight + "," + mColourComponents + ",(" + string.Join(",", mPixel.ConvertAll(x=>"'" + x + "'")) + ")"; }
	}
	public abstract partial class IfcPlacement : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcAxis1Placement ,IfcAxis2Placement2D ,IfcAxis2Placement3D))*/
	{
		private int mLocation;// : IfcCartesianPoint;
		public IfcCartesianPoint Location { get { return mDatabase[mLocation] as IfcCartesianPoint; } set { mLocation = value.mIndex; } }

		protected IfcPlacement() : base() { }
		protected IfcPlacement(DatabaseIfc db) : base(db) { Location = db.Factory.Origin; }
		protected IfcPlacement(IfcCartesianPoint location) : base(location.mDatabase) { Location = location; }
		protected IfcPlacement(DatabaseIfc db, IfcPlacement p) : base(db, p) { Location = db.Factory.Duplicate(p.Location) as IfcCartesianPoint; }
		protected virtual void Parse(string str, ref int pos, int len) { mLocation = ParserSTEP.StripLink(str, ref pos, len); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mLocation); }

		public virtual bool IsWorldXY { get { return Location.isOrigin; } }
	}
	public partial class IfcPlanarBox : IfcPlanarExtent
	{
		internal int mPlacement;// : IfcAxis2Placement; 
		public IfcAxis2Placement Placement { get { return mDatabase[mPlacement] as IfcAxis2Placement; } set { mPlacement = value.Index; } }

		internal IfcPlanarBox() : base() { }
		internal IfcPlanarBox(DatabaseIfc db, IfcPlanarBox b) : base(db,b) { Placement = db.Factory.Duplicate(b.mDatabase[b.mPlacement]) as IfcAxis2Placement; }
		internal new static IfcPlanarBox Parse(string str) { IfcPlanarBox b = new IfcPlanarBox(); int pos = 0; b.Parse(str, ref pos, str.Length); return b; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mPlacement = ParserSTEP.StripLink(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPlacement); }
	}
	public partial class IfcPlanarExtent : IfcGeometricRepresentationItem
	{
		internal double mSizeInX;// : IfcLengthMeasure;
		internal double mSizeInY;// : IfcLengthMeasure; 
		internal IfcPlanarExtent() : base() { }
		internal IfcPlanarExtent(DatabaseIfc db, IfcPlanarExtent p) : base(db,p) { mSizeInX = p.mSizeInX; mSizeInY = p.mSizeInY; }
		internal static IfcPlanarExtent Parse(string str) { IfcPlanarExtent p = new IfcPlanarExtent(); int pos = 0; p.Parse(str, ref pos, str.Length); return p; }
		protected virtual void Parse(string str, ref int pos, int len)
		{ 
			mSizeInX = ParserSTEP.StripDouble(str, ref pos, len);
			mSizeInY = ParserSTEP.StripDouble(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mSizeInX) + "," + ParserSTEP.DoubleToString(mSizeInY); }
	}
	public partial class IfcPlane : IfcElementarySurface
	{
		internal IfcPlane() : base() { }
		internal IfcPlane(DatabaseIfc db, IfcPlane p) : base(db,p) { }
		public IfcPlane(IfcAxis2Placement3D placement) : base(placement) { }
		internal static IfcPlane Parse(string str) { IfcPlane p = new IfcPlane(); int pos = 0; p.Parse(str, ref pos, str.Length); return p; }
	}
	public partial class IfcPlate : IfcBuildingElement
	{
		internal IfcPlateTypeEnum mPredefinedType = IfcPlateTypeEnum.NOTDEFINED;//: OPTIONAL IfcPlateTypeEnum;
		public IfcPlateTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPlate() : base() { }
		internal IfcPlate(DatabaseIfc db, IfcPlate p) : base(db, p) { mPredefinedType = p.mPredefinedType; }
		public IfcPlate(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcPlate Parse(string str, ReleaseVersion schema) { IfcPlate p = new IfcPlate(); int pos = 0; p.Parse(str, ref pos, str.Length, schema); return p; }
		protected void Parse(string str, ref int pos, int len, ReleaseVersion schema)
		{
			base.Parse(str, ref pos, len);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s[0] == '.')
					Enum.TryParse<IfcPlateTypeEnum>(s.Substring(1, s.Length - 2), out mPredefinedType);
			}
		}
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? base.BuildStringSTEP() : base.BuildStringSTEP() + (mPredefinedType == IfcPlateTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcPlateStandardCase : IfcPlate //IFC4
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mDatabase.mModelView == ModelView.Ifc4Reference ? "IfcPlate" : base.KeyWord); } }
		internal IfcPlateStandardCase() : base() { }
		internal IfcPlateStandardCase(DatabaseIfc db, IfcPlateStandardCase p) : base(db, p) { }

		internal static IfcPlateStandardCase Parse(string strDef) { IfcPlateStandardCase s = new IfcPlateStandardCase(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcPlateStandardCase s, List<string> arrFields, ref int ipos) { IfcPlate.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcPlateType : IfcBuildingElementType
	{
		internal IfcPlateTypeEnum mPredefinedType = IfcPlateTypeEnum.NOTDEFINED;
		public IfcPlateTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPlateType() : base() { }
		internal IfcPlateType(DatabaseIfc db, IfcPlateType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcPlateType(DatabaseIfc m, string name, IfcPlateTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal IfcPlateType(string name, IfcMaterialLayerSet mls, IfcPlateTypeEnum type) : this(mls.mDatabase, name, type) { MaterialSelect = mls; }
		internal static void parseFields(IfcPlateType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcPlateTypeEnum)Enum.Parse(typeof(IfcPlateTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcPlateType Parse(string strDef) { IfcPlateType t = new IfcPlateType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
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
		internal static IfcPointOnCurve Parse(string str)
		{
			IfcPointOnCurve p = new IfcPointOnCurve();
			int pos = 0, len = str.Length;
			p.mBasisCurve = ParserSTEP.StripLink(str, ref pos, len);
			p.mPointParameter = ParserSTEP.StripDouble(str, ref pos, len);
			return p;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBasisCurve) + "," + ParserSTEP.DoubleToString(mPointParameter); }
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
		internal static IfcPointOnSurface Parse(string str)
		{
			IfcPointOnSurface p = new IfcPointOnSurface();
			int pos = 0, len = str.Length;
			p.mBasisSurface = ParserSTEP.StripLink(str, ref pos, len);
			p.mPointParameterU = ParserSTEP.StripDouble(str, ref pos, len);
			p.mPointParameterV = ParserSTEP.StripDouble(str, ref pos, len);
			return p;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.DoubleToString(mPointParameterU) + "," + ParserSTEP.DoubleToString(mPointParameterV); }
	}
	public interface IfcPointOrVertexPoint : IBaseClassIfc { }  // = SELECT ( IfcPoint, IfcVertexPoint);
	public partial class IfcPolygonalBoundedHalfSpace : IfcHalfSpaceSolid
	{
		internal int mPosition;// : IfcAxis2Placement3D;
		internal int mPolygonalBoundary;// : IfcBoundedCurve; 

		public IfcAxis2Placement3D Position { get { return mDatabase[mPosition] as IfcAxis2Placement3D; } set { mPosition = value.mIndex; } }
		public IfcBoundedCurve PolygonalBoundary { get { return mDatabase[mPolygonalBoundary] as IfcBoundedCurve; } set { mPolygonalBoundary = value.mIndex; } }

		internal IfcPolygonalBoundedHalfSpace() : base() { }
		internal IfcPolygonalBoundedHalfSpace(DatabaseIfc db, IfcPolygonalBoundedHalfSpace s) : base(db,s) { Position = db.Factory.Duplicate(s.Position) as IfcAxis2Placement3D;  PolygonalBoundary = db.Factory.Duplicate(s.PolygonalBoundary) as IfcBoundedCurve; }
		internal new static IfcPolygonalBoundedHalfSpace Parse(string str) { IfcPolygonalBoundedHalfSpace s = new IfcPolygonalBoundedHalfSpace(); int pos = 0; s.Parse(str, ref pos, str.Length); return s; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mPosition = ParserSTEP.StripLink(str, ref pos, len);
			mPolygonalBoundary = ParserSTEP.StripLink(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPosition) + "," + ParserSTEP.LinkToString(mPolygonalBoundary); }
	}
	public partial class IfcPolygonalFaceSet : IfcTessellatedFaceSet //IFC4A2
	{
		internal bool mClosed; // 	OPTIONAL BOOLEAN;
		internal List<int> mFaces = new List<int>(); // : SET [1:?] OF IfcIndexedPolygonalFace;
		internal List<int> mPnIndex = new List<int>(); // : OPTIONAL LIST [1:?] OF IfcPositiveInteger;

		public bool Closed { get { return mClosed; } set { mClosed = value; } }
		public IEnumerable<IfcIndexedPolygonalFace> Faces { get { return mFaces.ConvertAll(x => mDatabase[x] as IfcIndexedPolygonalFace); } set { mFaces = value.ToList().ConvertAll(x => x.mIndex); } }
		public List<int> PnIndex { get { return mPnIndex; } set { mPnIndex = (value == null ? new List<int>() : value); } }

		internal IfcPolygonalFaceSet() : base() { }
		internal IfcPolygonalFaceSet(DatabaseIfc db, IfcPolygonalFaceSet s) : base(db, s) { Faces = s.Faces.ToList().ConvertAll(x => db.Factory.Duplicate(x) as IfcIndexedPolygonalFace); }
		public IfcPolygonalFaceSet(IfcCartesianPointList3D pl, bool closed, IEnumerable<IfcIndexedPolygonalFace> faces) : base(pl) { mClosed = closed; Faces = faces; }
		internal static IfcPolygonalFaceSet Parse(string str)
		{
			IfcPolygonalFaceSet t = new IfcPolygonalFaceSet();
			int pos = 0;
			t.Parse(str, ref pos, str.Length);
			return t;
		}
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mClosed = ParserSTEP.StripBool(str, ref pos, len);
			mFaces = ParserSTEP.StripListLink(str, ref pos, len);
			mPnIndex = ParserSTEP.StripListInt(str, ref pos, len);
		}
		protected override string BuildStringSTEP()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("," + ParserSTEP.BoolToString(mClosed) + ",(#" + mFaces[0]);
			for (int icounter = 1; icounter < mFaces.Count; icounter++)
				sb.Append(",#" + mFaces[icounter]);
			if (mPnIndex.Count == 0)
				sb.Append("),$");
			else
			{
				sb.Append("),(" + mPnIndex[0]);
				for (int icounter = 1; icounter < mPnIndex.Count; icounter++)
					sb.Append("," + mPnIndex[icounter]);
				sb.Append(")");
			}

			return base.BuildStringSTEP() + sb.ToString();
		}
	}
	public partial class IfcPolyline : IfcBoundedCurve
	{
		private List<int> mPoints = new List<int>();// : LIST [2:?] OF IfcCartesianPoint;
		public List<IfcCartesianPoint> Points { get { return mPoints.ConvertAll(x => mDatabase[x] as IfcCartesianPoint); } set { mPoints = value.ConvertAll(x => x.mIndex); } }

		internal IfcPolyline() : base() { }
		internal IfcPolyline(DatabaseIfc db, IfcPolyline p) : base(db, p) { Points = p.Points.ConvertAll(x => db.Factory.Duplicate(x) as IfcCartesianPoint); }
		public IfcPolyline(IfcCartesianPoint start, IfcCartesianPoint end) : base(start.mDatabase) { mPoints.Add(start.mIndex); mPoints.Add(end.mIndex); }
		public IfcPolyline(List<IfcCartesianPoint> pts) : base(pts[1].mDatabase) { Points = pts; }
		public IfcPolyline(DatabaseIfc db, List<Tuple<double,double>> points) : base(db) { Points = points.ConvertAll(x => new IfcCartesianPoint(db, x.Item1, x.Item2)); }
		public IfcPolyline(DatabaseIfc db, List<Tuple<double,double,double>> points) : base(db) { Points = points.ConvertAll(x => new IfcCartesianPoint(db, x.Item1, x.Item2,x.Item3)); }
		internal static IfcPolyline Parse(string str) { IfcPolyline p = new IfcPolyline(); p.mPoints = ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)); return p; }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(";
			if (mPoints.Count > 0)
				str += ParserSTEP.LinkToString(mPoints[0]);
			for (int icounter = 1; icounter < mPoints.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mPoints[icounter]);
			str += ")";
			return base.BuildStringSTEP() + str;
		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			if (schema != ReleaseVersion.IFC2x3)
			{
				if(mPoints.Count > 2)
				{
					List<Tuple<double, double>> pts = new List<Tuple<double, double>>(mPoints.Count);
					
					foreach(IfcCartesianPoint cp in Points)
					{
						if(!cp.is2D)
						{
							pts.Clear();
							break;
						}
						Tuple<double,double,double> p = cp.Coordinates;
						pts.Add(new Tuple<double, double>(p.Item1, p.Item2));
					}
					IfcIndexedPolyCurve ipc = pts.Count > 0 ? new IfcIndexedPolyCurve(new IfcCartesianPointList2D(mDatabase, pts)) : new IfcIndexedPolyCurve(new IfcCartesianPointList3D(mDatabase,Points.ConvertAll(x=>x.Coordinates)));
					ReplaceDatabase(ipc);
					return;
					
				}
			}
			else
				base.changeSchema(schema);
		}
	}
	public partial class IfcPolyloop : IfcLoop
	{
		internal List<int> mPolygon = new List<int>();// : LIST [3:?] OF UNIQUE IfcCartesianPoint;
		public List<IfcCartesianPoint> Polygon { get { return mPolygon.ConvertAll(x => mDatabase[x] as IfcCartesianPoint); } set { mPolygon = value.ConvertAll(x => x.mIndex); } }

		internal IfcPolyloop() : base() { }
		internal IfcPolyloop(DatabaseIfc db, IfcPolyloop l) : base(db,l) { Polygon = l.Polygon.ConvertAll(x=>db.Factory.Duplicate(x) as IfcCartesianPoint); }
		public IfcPolyloop(List<IfcCartesianPoint> polygon) : base(polygon[0].mDatabase) { mPolygon = polygon.ConvertAll(x => x.mIndex); }
		public IfcPolyloop(IfcCartesianPoint cp1, IfcCartesianPoint cp2, IfcCartesianPoint cp3) : base(cp1.mDatabase) { mPolygon = new List<int>() { cp1.mIndex, cp2.mIndex, cp3.mIndex }; }
		internal static IfcPolyloop Parse(string str)
		{
			IfcPolyloop l = new IfcPolyloop();
			l.mPolygon = ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2));
			return l;
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(";
			if (mPolygon.Count > 0)
				str += "#" + mPolygon[0];
			for (int icounter = 1; icounter < mPolygon.Count; icounter++)
				str += ",#" + mPolygon[icounter];
			return str + ")";
		}
	}
	public abstract partial class IfcPort : IfcProduct
	{	//INVERSE	
		internal IfcRelConnectsPortToElement mContainedIn = null;//	 :	SET [0:1] OF IfcRelConnectsPortToElement FOR RelatingPort;
		internal IfcRelConnectsPorts mConnectedFrom = null;//	 :	SET [0:1] OF IfcRelConnectsPorts FOR RelatedPort;
		internal IfcRelConnectsPorts mConnectedTo = null;//	 :	SET [0:1] OF IfcRelConnectsPorts FOR RelatingPort;

		protected IfcPort() : base() { }
		protected IfcPort(DatabaseIfc db, IfcPort p) : base(db,p,false) { }
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
					e.mIsNestedBy[0].addObject(this);
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
					t.mIsNestedBy[0].addObject(this);
			}
		}
		
		protected static void parseFields(IfcPort p, List<string> arrFields, ref int ipos) { IfcProduct.parseFields(p, arrFields, ref ipos); }
		
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
		public List<string> AddressLines { get { return mAddressLines.ConvertAll(x => ParserIfc.Decode(x)); } set { mAddressLines = (value == null ? new List<string>() : value.ConvertAll(x => ParserIfc.Encode(x))); } }
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
		internal static void parseFields(IfcPostalAddress a, List<string> arrFields, ref int ipos)
		{
			IfcAddress.parseFields(a, arrFields, ref ipos);
			a.mInternalLocation = arrFields[ipos++].Replace("'", "");
			if (string.IsNullOrEmpty(a.mInternalLocation))
				a.mInternalLocation = "$";
			string str = arrFields[ipos++];
			if (str != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(str.Substring(1,str.Length-2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
					a.mAddressLines.Add(lst[icounter].Replace("'", ""));
			}
			a.mPostalBox = arrFields[ipos++].Replace("'", "");
			if (string.IsNullOrEmpty(a.mPostalBox))
				a.mPostalBox = "$";
			a.mTown = arrFields[ipos++].Replace("'", "");
			if (string.IsNullOrEmpty(a.mTown))
				a.mTown = "$";
			a.mRegion = arrFields[ipos++].Replace("'", "");
			if (string.IsNullOrEmpty(a.mRegion))
				a.mRegion = "$";
			a.mPostalCode = arrFields[ipos++].Replace("'", "");
			if (string.IsNullOrEmpty(a.mPostalCode))
				a.mPostalCode = "$";
			a.mCountry = arrFields[ipos++].Replace("'", "");
			if (string.IsNullOrEmpty(a.mCountry))
				a.mCountry = "$";
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + (mInternalLocation == "$" ? ",$," : ",'" + mInternalLocation + "',");
			if (mAddressLines.Count == 0)
				str += "$";
			else
			{
				str += "('" + mAddressLines[0];
				for (int icounter = 1; icounter < mAddressLines.Count; icounter++)
					str += "','" + mAddressLines[icounter];
				str += "')";
			}
			return str + (mPostalBox == "$" ? ",$" : ",'" + mPostalBox + "'") + (mTown == "$" ? ",$" : ",'" + mTown + "'") + (mRegion == "$" ? ",$" : ",'" + mRegion + "'") + (mPostalCode == "$" ? ",$" : ",'" + mPostalCode + "'") + (mCountry == "$" ? ",$" : ",'" + mCountry + "'");
		}
		internal static IfcPostalAddress Parse(string strDef) { IfcPostalAddress a = new IfcPostalAddress(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
	}
	public abstract partial class IfcPreDefinedColour : IfcPreDefinedItem, IfcColour //	ABSTRACT SUPERTYPE OF(IfcDraughtingPreDefinedColour)
	{
		protected IfcPreDefinedColour() : base() { }
		protected IfcPreDefinedColour(DatabaseIfc db, IfcPreDefinedColour c) : base(db,c) { }
		protected static void parseFields(IfcPreDefinedColour c, List<string> arrFields, ref int ipos) { IfcPreDefinedItem.parseFields(c, arrFields, ref ipos); }
	}
	public abstract partial class IfcPreDefinedCurveFont : IfcPreDefinedItem, IfcCurveStyleFontSelect
	{
		protected IfcPreDefinedCurveFont() : base() { }
		protected IfcPreDefinedCurveFont(DatabaseIfc db, IfcPreDefinedCurveFont f) : base(db,f) { }
		protected static void parseFields(IfcPreDefinedCurveFont f, List<string> arrFields, ref int ipos) { IfcPreDefinedItem.parseFields(f, arrFields, ref ipos); }
	}
	public partial class IfcPreDefinedDimensionSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
	{
		internal IfcPreDefinedDimensionSymbol() : base() { }
		internal IfcPreDefinedDimensionSymbol(DatabaseIfc db, IfcPreDefinedDimensionSymbol s) : base(db,s) { }
		internal static IfcPreDefinedDimensionSymbol Parse(string strDef) { IfcPreDefinedDimensionSymbol s = new IfcPreDefinedDimensionSymbol(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcPreDefinedDimensionSymbol s, List<string> arrFields, ref int ipos) { IfcPreDefinedSymbol.parseFields(s, arrFields, ref ipos); }
	}
	public abstract partial class IfcPreDefinedItem : IfcPresentationItem
	{
		internal string mName = "";//: IfcLabel; 
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } } 

		protected IfcPreDefinedItem() : base() { }
		protected IfcPreDefinedItem(DatabaseIfc db, IfcPreDefinedItem i) : base(db,i) { mName = i.mName; }
		protected static void parseFields(IfcPreDefinedItem i, List<string> arrFields, ref int ipos) { i.mName = arrFields[ipos++]; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mName; }
	}
	public partial class IfcPreDefinedPointMarkerSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
	{
		internal IfcPreDefinedPointMarkerSymbol() : base() { }
		internal IfcPreDefinedPointMarkerSymbol(DatabaseIfc db, IfcPreDefinedPointMarkerSymbol s) : base(db,s) { }
		internal static IfcPreDefinedPointMarkerSymbol Parse(string strDef) { IfcPreDefinedPointMarkerSymbol s = new IfcPreDefinedPointMarkerSymbol(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcPreDefinedPointMarkerSymbol s, List<string> arrFields, ref int ipos) { IfcPreDefinedSymbol.parseFields(s, arrFields, ref ipos); }
	}
	public abstract partial class IfcPreDefinedProperties : IfcPropertyAbstraction // IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcementBarProperties, IfcSectionProperties, IfcSectionReinforcementProperties))
	{
		protected IfcPreDefinedProperties() : base() { }
		protected IfcPreDefinedProperties(DatabaseIfc db) : base(db) { }
		protected IfcPreDefinedProperties(DatabaseIfc db, IfcPreDefinedProperties p) : base(db, p) { }
		protected static void parseFields(IfcPreDefinedProperties p, List<string> arrFields, ref int ipos) { IfcPropertyAbstraction.parseFields(p, arrFields, ref ipos); }
	}
	public abstract partial class IfcPreDefinedPropertySet : IfcPropertySetDefinition // IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcDoorLiningProperties,  
	{ //IfcDoorPanelProperties, IfcPermeableCoveringProperties, IfcReinforcementDefinitionProperties, IfcWindowLiningProperties, IfcWindowPanelProperties))
		protected IfcPreDefinedPropertySet() : base() { }
		protected IfcPreDefinedPropertySet(DatabaseIfc db, IfcPreDefinedPropertySet p) : base(db, p) { }
		protected IfcPreDefinedPropertySet(DatabaseIfc m, string name) : base(m, name) { }
		protected static void parseFields(IfcPreDefinedPropertySet s, List<string> arrFields, ref int ipos) { IfcPropertySetDefinition.parseFields(s, arrFields, ref ipos); }
	}
	public abstract partial class IfcPreDefinedSymbol : IfcPreDefinedItem // DEPRECEATED IFC4
	{
		protected IfcPreDefinedSymbol() : base() { }
		protected IfcPreDefinedSymbol(DatabaseIfc db, IfcPreDefinedSymbol s) : base(db,s) { }
		protected static void parseFields(IfcPreDefinedSymbol s, List<string> arrFields, ref int ipos) { IfcPreDefinedItem.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcPreDefinedTerminatorSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
	{
		internal IfcPreDefinedTerminatorSymbol() : base() { }
		internal IfcPreDefinedTerminatorSymbol(DatabaseIfc db, IfcPreDefinedTerminatorSymbol s) : base(db,s) { }
		internal static IfcPreDefinedTerminatorSymbol Parse(string strDef) { IfcPreDefinedTerminatorSymbol s = new IfcPreDefinedTerminatorSymbol(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcPreDefinedTerminatorSymbol s, List<string> arrFields, ref int ipos) { IfcPreDefinedSymbol.parseFields(s, arrFields, ref ipos); }
	}
	public abstract partial class IfcPreDefinedTextFont : IfcPreDefinedItem
	{
		protected IfcPreDefinedTextFont() : base() { }
		protected IfcPreDefinedTextFont(DatabaseIfc db, IfcPreDefinedTextFont f) : base(db,f) { }
		protected static void parseFields(IfcPreDefinedTextFont f, List<string> arrFields, ref int ipos) { IfcPreDefinedItem.parseFields(f, arrFields, ref ipos); }
	}
	public abstract partial class IfcPresentationItem : BaseClassIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcColourRgbList, IfcColourSpecification,
	{ // IfcCurveStyleFont, IfcCurveStyleFontAndScaling, IfcCurveStyleFontPattern, IfcIndexedColourMap, IfcPreDefinedItem, IfcSurfaceStyleLighting, IfcSurfaceStyleRefraction, IfcSurfaceStyleShading, IfcSurfaceStyleWithTextures, IfcSurfaceTexture, IfcTextStyleForDefinedFont, IfcTextStyleTextModel, IfcTextureCoordinate, IfcTextureVertex, IfcTextureVertexList));
		protected IfcPresentationItem() : base() { }
		protected IfcPresentationItem(DatabaseIfc db, IfcPresentationItem i) : base(db,i) { }
		protected IfcPresentationItem(DatabaseIfc db) : base(db) { }
	}
	public partial class IfcPresentationLayerAssignment : BaseClassIfc //SUPERTYPE OF	(IfcPresentationLayerWithStyle);
	{
		private string mName = "$";// : IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal List<int> mAssignedItems = new List<int>();// : SET [1:?] OF IfcLayeredItem; 
		internal string mIdentifier = "$";// : OPTIONAL IfcIdentifier; 

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "Default Layer" : mName = ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public List<IfcLayeredItem> AssignedItems { get { return mAssignedItems.ConvertAll(x => mDatabase[x] as IfcLayeredItem); } set { mAssignedItems = value.ConvertAll(x => x.Index); } }
		public string Identifier { get { return (mIdentifier == "$" ? "" : ParserIfc.Decode(mIdentifier)); } set { mIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcPresentationLayerAssignment() : base() { }
		internal IfcPresentationLayerAssignment(DatabaseIfc db, IfcPresentationLayerAssignment a) : base(db,a) { mName = a.mName; mDescription = a.mDescription; AssignedItems = a.mAssignedItems.ConvertAll(x=>db.Factory.Duplicate(a.mDatabase[x]) as IfcLayeredItem); mIdentifier = a.mIdentifier; }
		public IfcPresentationLayerAssignment(DatabaseIfc db, string name) : base(db) { Name = name; }
		public IfcPresentationLayerAssignment(string name, IfcLayeredItem item) : this(item.Database,name) { assign(item); }
		public IfcPresentationLayerAssignment(string name, List<IfcLayeredItem> items) : this(items[0].Database,name) { foreach(IfcLayeredItem item in items) assign(item); }
		internal static IfcPresentationLayerAssignment Parse(string str)
		{
			IfcPresentationLayerAssignment a = new IfcPresentationLayerAssignment();
			int pos = 0;
			parseString(a, str, str.Length, ref pos);
			return a;
		}
		protected static void parseString(IfcPresentationLayerAssignment a, string str, int len, ref int pos)
		{
			a.mName = ParserSTEP.StripString(str, ref pos, len);
			a.mDescription = ParserSTEP.StripString(str, ref pos, len);
			a.mAssignedItems = ParserSTEP.StripListLink(str, ref pos, len);
			a.mIdentifier = ParserSTEP.StripString(str, ref pos, len);
		}
		protected override string BuildStringSTEP()
		{
			if (mAssignedItems.Count < 1)
				return "";
			string str = base.BuildStringSTEP() + ",'" + mName + (mDescription == "$" ? "',$,(" : "'," + mDescription + ",(") + ParserSTEP.LinkToString(mAssignedItems[0]);
			if (mAssignedItems.Count > 100)
			{
				StringBuilder sb = new StringBuilder();
				for (int icounter = 1; icounter < mAssignedItems.Count; icounter++)
					sb.Append(",#" + mAssignedItems[icounter]);
				str += sb.ToString();
			}
			else
			{
				for (int icounter = 1; icounter < mAssignedItems.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mAssignedItems[icounter]);
			}
			return str + (mIdentifier == "$" ? "),$" : "),'" + mIdentifier + "'");
		}
		internal void addItem(IfcRepresentation rep) { mAssignedItems.Add(rep.mIndex); rep.mLayerAssignments.Add(this); }
		internal void addItem(IfcRepresentationItem rep) { mAssignedItems.Add(rep.mIndex); rep.mLayerAssignments.Add(this); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			List<IfcLayeredItem> items = AssignedItems;
			for (int icounter = 0; icounter < items.Count; icounter++)
				items[icounter].LayerAssignments.Add(this);
		}
		private void assign(IfcLayeredItem item)
		{
			mAssignedItems.Add(item.Index);
			item.LayerAssignments.Add(this);
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
		public List<IfcPresentationStyle> LayerStyles { get { return mLayerStyles.ConvertAll(x => mDatabase[x] as IfcPresentationStyle); } set { mLayerStyles = value.ConvertAll(x => x.Index); } }

		internal IfcPresentationLayerWithStyle() : base() { }
		internal IfcPresentationLayerWithStyle(DatabaseIfc db, IfcPresentationLayerWithStyle l) : base(db,l) { mLayerOn = l.mLayerOn; mLayerFrozen = l.mLayerFrozen; mLayerBlocked = l.mLayerBlocked; LayerStyles = l.LayerStyles.ConvertAll(x=>db.Factory.Duplicate(x) as IfcPresentationStyle); }

		internal IfcPresentationLayerWithStyle(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPresentationLayerWithStyle(string name, IfcLayeredItem item, IfcPresentationStyle style ) : base(name, item) { mLayerStyles.Add(style.mIndex); }
		public IfcPresentationLayerWithStyle(string name, IfcLayeredItem item, List<IfcPresentationStyle> styles ) : base(name, item) { LayerStyles = styles; }
		public IfcPresentationLayerWithStyle(string name, List<IfcLayeredItem> items, List<IfcPresentationStyle> styles ) : base(name, items) { LayerStyles = styles; }
		public IfcPresentationLayerWithStyle(string name, List<IfcLayeredItem> items, IfcPresentationStyle style ) : base(name, items) { mLayerStyles.Add(style.mIndex); }
		internal new static IfcPresentationLayerWithStyle Parse(string str)
		{
			IfcPresentationLayerWithStyle s = new IfcPresentationLayerWithStyle();
			int pos = 0, len = str.Length;
			IfcPresentationLayerAssignment.parseString(s, str, len, ref pos);
			s.mLayerOn = ParserIfc.StripLogical(str, ref pos, len);
			s.mLayerFrozen = ParserIfc.StripLogical(str, ref pos, len);
			s.mLayerBlocked = ParserIfc.StripLogical(str, ref pos, len);
			s.mLayerStyles = ParserSTEP.StripListLink(str, ref pos, len);
			return s;
		}
		protected override string BuildStringSTEP()
		{
			if (mAssignedItems.Count < 1 || mLayerStyles.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + "," + ParserIfc.LogicalToString(mLayerOn) + "," +
				ParserIfc.LogicalToString(mLayerFrozen) + "," + ParserIfc.LogicalToString(mLayerBlocked);
			str += ",(" + ParserSTEP.LinkToString(mLayerStyles[0]);
			for (int icounter = 1; icounter < mLayerStyles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mLayerStyles[icounter]);
			return str + ")";
		}
	}
	public abstract partial class IfcPresentationStyle : BaseClassIfc, IfcStyleAssignmentSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcCurveStyle,IfcFillAreaStyle,IfcSurfaceStyle,IfcSymbolStyle,IfcTextStyle));
	{
		private string mName = "$";// : OPTIONAL IfcLabel;		
		//INVERSE
		internal List<IfcStyledItem> mStyledItems = new List<IfcStyledItem>();

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public List<IfcStyledItem> StyledItems { get { return mStyledItems; } }

		protected IfcPresentationStyle() : base() { }
		protected IfcPresentationStyle(DatabaseIfc m, string name) : base(m) { Name = name; }
		protected IfcPresentationStyle(DatabaseIfc db, IfcPresentationStyle s) : base(db,s) { mName = s.mName; }
		protected IfcPresentationStyle(IfcPresentationStyle i) : base() { mName = i.mName; }
		protected static void parseFields(IfcPresentationStyle s, List<string> arrFields, ref int ipos) { s.mName = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mName == "$" ? ",$" : ",'" + mName + "'"); }
	}
	public partial class IfcPresentationStyleAssignment : BaseClassIfc, IfcStyleAssignmentSelect //DEPRECEATED IFC4
	{
		internal List<int> mStyles = new List<int>();// : SET [1:?] OF IfcPresentationStyleSelect; 
		//INVERSE
		internal List<IfcStyledItem> mStyledItems = new List<IfcStyledItem>();
		
		public List<IfcPresentationStyleSelect> Styles { get { return mStyles.ConvertAll(x => mDatabase[x] as IfcPresentationStyleSelect); } set { mStyles = value.ConvertAll(x => x.Index); } }
		public List<IfcStyledItem> StyledItems { get { return mStyledItems; } }

		internal IfcPresentationStyleAssignment() : base() { }
		internal IfcPresentationStyleAssignment(IfcPresentationStyle style) : base(style.mDatabase) { mStyles.Add(style.Index); }
		internal IfcPresentationStyleAssignment(List<IfcPresentationStyle> styles) : base(styles[0].mDatabase) { mStyles = styles.ConvertAll(x => x.Index); }
		internal IfcPresentationStyleAssignment(DatabaseIfc db, IfcPresentationStyleAssignment s) : base(db,s) { Styles = s.mStyles.ConvertAll(x => db.Factory.Duplicate(s.mDatabase[x]) as IfcPresentationStyleSelect); }
		internal static IfcPresentationStyleAssignment Parse(string strDef) { IfcPresentationStyleAssignment s = new IfcPresentationStyleAssignment(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcPresentationStyleAssignment s, List<string> arrFields, ref int ipos) { s.mStyles = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mStyles[0]);
			for (int icounter = 1; icounter < mStyles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mStyles[icounter]);
			return str + ")";
		}
	}
	public interface IfcPresentationStyleSelect : IBaseClassIfc { } //DEPRECEATED IFC4 TYPE  = SELECT(IfcNullStyle, IfcCurveStyle, IfcSymbolStyle, IfcFillAreaStyle, IfcTextStyle, IfcSurfaceStyle);
	public partial class IfcProcedure : IfcProcess
	{
		internal string mProcedureID;// : IfcIdentifier;
		internal IfcProcedureTypeEnum mProcedureType;// : IfcProcedureTypeEnum;
		internal string mUserDefinedProcedureType = "$";// : OPTIONAL IfcLabel;
		internal IfcProcedure() : base() { }
		internal IfcProcedure(DatabaseIfc db, IfcProcedure p) : base(db,p) { mProcedureID = p.mProcedureID; mProcedureType = p.mProcedureType; mUserDefinedProcedureType = p.mUserDefinedProcedureType; }
		internal static IfcProcedure Parse(string strDef) { IfcProcedure p = new IfcProcedure(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcProcedure p, List<string> arrFields, ref int ipos) { IfcProcess.parseFields(p, arrFields, ref ipos); p.mProcedureID = arrFields[ipos++]; p.mProcedureType = (IfcProcedureTypeEnum)Enum.Parse(typeof(IfcProcedureTypeEnum), arrFields[ipos++].Replace(".", "")); p.mUserDefinedProcedureType = arrFields[ipos++]; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mProcedureID + ",." + mProcedureType.ToString() + ".," + mUserDefinedProcedureType; }
	}
	public partial class IfcProcedureType : IfcTypeProcess //IFC4
	{
		internal IfcProcedureTypeEnum mPredefinedType = IfcProcedureTypeEnum.NOTDEFINED;// : IfcProcedureTypeEnum; 
		public IfcProcedureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProcedureType() : base() { }
		internal IfcProcedureType(DatabaseIfc db, IfcProcedureType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcProcedureType(DatabaseIfc m, string name, IfcProcedureTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcProcedureType t, List<string> arrFields, ref int ipos) { IfcTypeProcess.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcProcedureTypeEnum)Enum.Parse(typeof(IfcProcedureTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcProcedureType Parse(string strDef) { IfcProcedureType t = new IfcProcedureType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."); }
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
		protected IfcProcess(DatabaseIfc db, IfcProcess p) : base(db,p,false) { mIdentification = p.mIdentification; mLongDescription = p.mLongDescription; }
		protected IfcProcess(DatabaseIfc db) : base(db)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.If2x3NotAssigned)
				throw new Exception("Invalid Model View for IfcProcess : " + db.ModelView.ToString());
		}
		
		protected static void parseFields(IfcProcess p, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcObject.parseFields(p, arrFields, ref ipos);
			if (schema != ReleaseVersion.IFC2x3)
			{
				p.mIdentification = arrFields[ipos++].Replace("'", "");
				p.mLongDescription = arrFields[ipos++].Replace("'", "");
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease != ReleaseVersion.IFC2x3 ? (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',") + (mLongDescription == "$" ? "$" : "'" + mLongDescription + "'") : ""); }
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
					if(mPlacement > 0)
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
				if(pds != null)
					pds.mShapeOfProduct.Remove(this);
				if(value == null)
					mRepresentation = 0;
				else
				{
					mRepresentation = value.mIndex;
					pds = value as IfcProductDefinitionShape;
					
					if (pds != null && mPlacement == 0)
					{
						IfcElement element = this as IfcElement;
						if (element == null)
							Placement = new IfcLocalPlacement(mDatabase.Factory.PlaneXYPlacement);
						else
						{
							IfcProduct product = element.getContainer();
							if (product == null)
								Placement = new IfcLocalPlacement(mDatabase.Factory.PlaneXYPlacement);
							else
								Placement = new IfcLocalPlacement(product.Placement, mDatabase.Factory.PlaneXYPlacement);
						}
					}
				}
			}
		}
		public List<IfcRelAssignsToProduct> ReferencedBy { get { return mReferencedBy; } }

		internal IfcObjectPlacement mContainerCommonPlacement = null; //GeometryGym common Placement reference for aggregated items

		protected IfcProduct() : base() { }
		protected IfcProduct(IfcProduct basis) : base(basis) { mPlacement = basis.mPlacement; mRepresentation = basis.mRepresentation; mReferencedBy = basis.mReferencedBy; }
		protected IfcProduct(IfcProductRepresentation rep) : base(rep.mDatabase) { mRepresentation = rep.mIndex; }
		protected IfcProduct(IfcObjectPlacement placement) : base(placement.mDatabase) { Placement = placement; }
		protected IfcProduct(IfcObjectPlacement placement, IfcProductRepresentation rep) : base(placement == null ? rep.mDatabase : placement.mDatabase)
		{
			if(placement != null) 
				Placement = placement;
			if(rep != null)
				mRepresentation = rep.mIndex;
		}
		protected IfcProduct(DatabaseIfc db) : base(db) { }
		protected IfcProduct(DatabaseIfc db, IfcProduct p, bool downStream) : base(db, p,downStream)
		{
			if (p.mPlacement > 0)
				Placement = db.Factory.Duplicate(p.Placement) as IfcObjectPlacement;
			if (p.mRepresentation > 0)
				Representation = db.Factory.Duplicate(p.Representation) as IfcProductRepresentation;
			foreach(IfcRelAssignsToProduct rap in p.mReferencedBy)
			{
				IfcRelAssignsToProduct rp = db.Factory.Duplicate(rap) as IfcRelAssignsToProduct;
				foreach(IfcObjectDefinition od in rap.RelatedObjects)
				{
					IfcObjectDefinition dup = db.Factory.Duplicate(od) as IfcObjectDefinition;
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
			{
				if (host.mIsDecomposedBy.Count == 0)
				{
					new IfcRelAggregates(host, this);
				}
				else
					host.mIsDecomposedBy[0].addObject(this);
			}
			Placement = p; 
			Representation = r; 
		}
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mPlacement = ParserSTEP.StripLink(str, ref pos, len);
			mRepresentation = ParserSTEP.StripLink(str, ref pos, len);
		}
		protected static void parseFields(IfcProduct p, List<string> arrFields, ref int ipos) { IfcObject.parseFields(p, arrFields, ref ipos); p.mPlacement = ParserSTEP.ParseLink(arrFields[ipos++]); p.mRepresentation = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPlacement) + "," + ParserSTEP.LinkToString(mRepresentation); }

		public virtual bool AddElement(IfcProduct s)
		{
			for (int icounter = 0; icounter < mIsDecomposedBy.Count; icounter++)
			{
				IfcRelAggregates a = mIsDecomposedBy[icounter] as IfcRelAggregates;
				if (a.Description.EndsWith("Elements", true, System.Globalization.CultureInfo.CurrentCulture))
				{
					a.addObject(s);
					return true;
				}
			}
			IfcRelAggregates ra = new IfcRelAggregates(mDatabase, KeyWord.Substring(3), "Element", this);
			ra.addObject(s);
			return true;
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mPlacement > 0)
				Placement.mPlacesObject.Add(this);
			if (mRepresentation > 0)
			{
				IfcProductDefinitionShape pds = Representation as IfcProductDefinitionShape;
				if (pds != null)
					pds.mShapeOfProduct.Add(this);
			}
		}

		public override List<T> Extract<T>()
		{
			List<T> result = base.Extract<T>();
			if(mRepresentation > 0)
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
	}
	//ENTITY IfcProductsOfCombustionProperties	 // DEPRECEATED IFC4
	public partial class IfcProductDefinitionShape : IfcProductRepresentation, IfcProductRepresentationSelect
	{
		//INVERSE
		internal List<IfcProduct> mShapeOfProduct = new List<IfcProduct>();
		internal List<IfcShapeAspect> mHasShapeAspects = new List<IfcShapeAspect>();

		public List<IfcProduct> ShapeOfProduct { get { return mShapeOfProduct; } }
		public List<IfcShapeAspect> HasShapeAspects { get { return mHasShapeAspects; } }

		public new List<IfcShapeModel> Representations
		{
			get { return base.Representations.ConvertAll(x => (IfcShapeModel)x); }
			set { base.Representations = value.ConvertAll(x => x as IfcRepresentation); }
		}

		internal IfcProductDefinitionShape() : base() { }
		public IfcProductDefinitionShape(IfcShapeModel rep) : base(rep) { }
		public IfcProductDefinitionShape(List<IfcShapeModel> reps) : base(reps.ConvertAll(x => x as IfcRepresentation)) { }
		internal IfcProductDefinitionShape(DatabaseIfc db, IfcProductDefinitionShape s) : base(db, s)
		{
#warning todo
			//internal List<IfcShapeAspect> mHasShapeAspects = new List<IfcShapeAspect>();
		}
		internal new static IfcProductDefinitionShape Parse(string strDef) { IfcProductDefinitionShape s = new IfcProductDefinitionShape(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcProductDefinitionShape s, List<string> arrFields, ref int ipos) { IfcProductRepresentation.parseFields(s, arrFields, ref ipos); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP(); }
		public void AddShapeAspect(IfcShapeAspect aspect) { mHasShapeAspects.Add(aspect); }
	}
	public partial class IfcProductRepresentation : BaseClassIfc //(IfcMaterialDefinitionRepresentation ,IfcProductDefinitionShape));
	{
		private string mName = "$";// : OPTIONAL IfcLabel;
		private string mDescription = "$";// : OPTIONAL IfcText;
		internal List<int> mRepresentations = new List<int>();// : LIST [1:?] OF IfcRepresentation; 

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public List<IfcRepresentation> Representations
		{
			get { return mRepresentations.ConvertAll(x => (IfcRepresentation)mDatabase[x]); }
			set { mRepresentations = value.ConvertAll(x => x.mIndex); }
		}

		public IfcRepresentation Representation { set { mRepresentations = new List<int>() { value.mIndex }; } }

		internal IfcProductRepresentation() : base() { }
		public IfcProductRepresentation(IfcRepresentation r) : base(r.mDatabase) { mRepresentations.Add(r.mIndex); }
		public IfcProductRepresentation(List<IfcRepresentation> reps) : base(reps[0].mDatabase) { Representations = reps; }
		internal IfcProductRepresentation(DatabaseIfc db, IfcProductRepresentation r) : base(db,r)
		{
			mName = r.mName;
			mDescription = r.mDescription;
			Representations = r.Representations.ConvertAll(x => db.Factory.Duplicate(x) as IfcRepresentation);
		}

		internal static IfcProductRepresentation Parse(string strDef) { IfcProductRepresentation r = new IfcProductRepresentation(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
		internal static void parseFields(IfcProductRepresentation r, List<string> arrFields, ref int ipos) { r.mName = arrFields[ipos++].Replace("'", ""); r.mDescription = arrFields[ipos++].Replace("'", ""); r.mRepresentations = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,(" : "'" + mDescription + "',(");
			if (mRepresentations.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRepresentations[0]);
				for (int icounter = 1; icounter < mRepresentations.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRepresentations[icounter]);
			}
			return str + ")";
		}

		internal override void postParseRelate()
		{
			base.postParseRelate();
			foreach (IfcRepresentation r in Representations)
			{
				if (r != null)
					r.OfProductRepresentation.Add(this);
			}
		}
		public override List<T> Extract<T>()
		{
			List<T> result = base.Extract<T>();
			foreach (IfcRepresentation r in Representations)
				result.AddRange(r.Extract<T>());
			return result;

		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			List<IfcRepresentation> reps = Representations;
			for (int icounter = 0; icounter < reps.Count; icounter++)
				reps[icounter].changeSchema(schema);
			base.changeSchema(schema);
		}
	}
	public interface IfcProductRepresentationSelect : IBaseClassIfc { List<IfcShapeAspect> HasShapeAspects { get; }  void AddShapeAspect(IfcShapeAspect aspect); }// 	IfcProductDefinitionShape,	IfcRepresentationMap);
	public interface IfcProductSelect : IBaseClassIfc { List<IfcRelAssignsToProduct> ReferencedBy { get; } string GlobalId { get; }  } //	IfcProduct, IfcTypeProduct);
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
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }

		protected IfcProfileDef() : base() { }
		protected IfcProfileDef(DatabaseIfc db, IfcProfileDef p) : base(db,p)
		{
			mProfileType = p.mProfileType;
			mProfileName = p.mProfileName;
			foreach (IfcProfileProperties pp in p.mHasProperties)
				(db.Factory.Duplicate(pp) as IfcProfileProperties).ProfileDefinition = this;
		}
		public IfcProfileDef(DatabaseIfc db,string name) : base(db)
		{
			ProfileName = name;
			if (db.mRelease == ReleaseVersion.IFC2x3)
				mHasProperties.Add(new IfcGeneralProfileProperties(ProfileName, this));
		}
		internal static IfcProfileDef Parse(string strDef) { IfcProfileDef p = new IfcProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected static void parseFields(IfcProfileDef p, List<string> arrFields, ref int ipos)
		{
			string str = arrFields[ipos++];
			if (str.StartsWith("."))
				p.mProfileType = (IfcProfileTypeEnum)Enum.Parse(typeof(IfcProfileTypeEnum), str.Replace(".", ""));
			p.mProfileName = ParserSTEP.ParseString(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mProfileType.ToString() + (mProfileName == "$" ? ".,$" : ".,'" + mProfileName + "'"); }

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
	}
	public partial class IfcProfileProperties : IfcExtendedProperties //IFC2x3 Abstract : BaseClassIfc ABSTRACT SUPERTYPE OF	(ONEOF(IfcGeneralProfileProperties, IfcRibPlateProfileProperties));
	{
		public override string KeyWord { get { return mDatabase.Release  == ReleaseVersion.IFC2x3 ? base.KeyWord : "IfcProfileProperties"; } }
		//internal string mProfileName = "$";// : OPTIONAL IfcLabel; DELETED IFC4
		private int mProfileDefinition;// : OPTIONAL IfcProfileDef; 
		public IfcProfileDef ProfileDefinition { get { return mDatabase[mProfileDefinition] as IfcProfileDef; } set { mProfileDefinition = value == null ? 0 : value.mIndex; if (value != null) value.mHasProperties.Add(this); } }

		internal IfcRelAssociatesProfileProperties mAssociates = null; //GeometryGym attribute

		internal IfcProfileProperties() : base() { }
		internal IfcProfileProperties(DatabaseIfc db, IfcProfileProperties p) : base(db,p) { ProfileDefinition = db.Factory.Duplicate( p.ProfileDefinition) as IfcProfileDef; }
		internal IfcProfileProperties(string name, IfcProfileDef p) : base(p.mDatabase)
		{
			Name = name;
			mProfileDefinition = p.mIndex;
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
				mAssociates = new IfcRelAssociatesProfileProperties(this) { Name = p.ProfileName };
		}
		internal IfcProfileProperties(string name, List<IfcProperty> props, IfcProfileDef p) : base(name, props)
		{
			mProfileDefinition = p.mIndex;
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
				mAssociates = new IfcRelAssociatesProfileProperties(this) { Name = p.ProfileName };
		}
		internal static IfcProfileProperties Parse(string strDef, ReleaseVersion schema) { IfcProfileProperties p = new IfcProfileProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return p; }
		internal static void parseFields(IfcProfileProperties p, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			if (schema == ReleaseVersion.IFC2x3)
			{
				p.mName = arrFields[ipos++].Replace("'", "");
				p.mProfileDefinition = ParserSTEP.ParseLink(arrFields[ipos++]);
			}
			else
			{
				IfcExtendedProperties.parseFields(p, arrFields, ref ipos,schema);
				p.mProfileDefinition = ParserSTEP.ParseLink(arrFields[ipos++]);
			}
		}
		protected override string BuildStringSTEP() { return (ProfileDefinition == null ? "" : base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? (mName == "$" ? ",$," : ",'" + mName + "',") : ",") + ParserSTEP.LinkToString(mProfileDefinition)); }

		internal override void postParseRelate()
		{
			base.postParseRelate();
			ProfileDefinition.mHasProperties.Add(this);
		}
	}
	public partial class IfcProject : IfcContext
	{
		internal IfcProject() : base() { }
		internal IfcProject(DatabaseIfc db, IfcProject p) : base(db, p) { }
		public IfcProject(IfcBuilding building, string name) : this(building.mDatabase, name) { new IfcRelAggregates(building.mDatabase, "Project", "Building", this, building); }
		public IfcProject(IfcSite site, string name) : this(site.mDatabase, name) { new IfcRelAggregates(site.mDatabase, "Project", "Site", this, site); }
		public IfcProject(IfcBuilding building, string name, IfcUnitAssignment.Length length) : this(building.mDatabase, name, length) { IfcRelAggregates ra = new IfcRelAggregates(building.mDatabase, "Project", "Building", this, building); }
		public IfcProject(IfcSite site, string name, IfcUnitAssignment.Length length) : this(site.mDatabase, name, length) { IfcRelAggregates ra = new IfcRelAggregates(site.mDatabase, "Project", "Site", this, site); }
		public IfcProject(DatabaseIfc m, string name) : base(m, name) 
		{
			m.mContext = this;
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECT";
		}
		private IfcProject(DatabaseIfc m, string name, IfcUnitAssignment.Length length)
			: base(m, name, length)
		{
			m.mContext = this;
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECT";
		} 

		internal static void parseFields(IfcProject p, List<string> arrFields, ref int ipos) { IfcContext.parseFields(p, arrFields, ref ipos); }
		internal static IfcProject Parse(string strDef) { IfcProject p = new IfcProject(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }

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
		public IfcNamedUnit MapUnit { get { return  mDatabase[mMapUnit] as IfcNamedUnit; } set { mMapUnit = (value == null ? 0 : value.mIndex); }  }
		
		internal IfcProjectedCRS() : base() { }
		internal IfcProjectedCRS(DatabaseIfc db, IfcProjectedCRS p) : base(db,p) { mName = p.mName; mMapZone = p.mMapZone; if(p.mMapUnit > 0) MapUnit = db.Factory.Duplicate( p.MapUnit) as IfcNamedUnit; }
		internal IfcProjectedCRS(DatabaseIfc m, string name) : base(m, name) { }
		internal static IfcProjectedCRS Parse(string strDef) { IfcProjectedCRS m = new IfcProjectedCRS(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcProjectedCRS m, List<string> arrFields, ref int ipos)
		{
			IfcCoordinateReferenceSystem.parseFields(m, arrFields, ref ipos);
			m.mName = arrFields[ipos++].Replace("'", "");
			m.mMapZone = arrFields[ipos++].Replace("'", "");
			m.mMapUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mMapProjection == "$" ? ",$," : ",'" + mMapProjection + "',") +
				(mMapZone == "$" ? "$," : "'" + mMapZone + "',") + ParserSTEP.LinkToString(mMapUnit);
		}
	}
	//ENTITY IfcProjectionCurve // DEPRECEATED IFC4
	public partial class IfcProjectionElement : IfcFeatureElementAddition
	{
		internal IfcProjectionElementTypeEnum mPredefinedType = IfcProjectionElementTypeEnum.NOTDEFINED;// :	OPTIONAL IfcProjectionElementTypeEnum; //IFC4
		public IfcProjectionElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		//INVERSE
		internal IfcProjectionElement() : base() { }
		internal IfcProjectionElement(DatabaseIfc db, IfcProjectionElement e) : base(db,e) { mPredefinedType = e.mPredefinedType; }

		internal static IfcProjectionElement Parse(string strDef, ReleaseVersion schema) { IfcProjectionElement e = new IfcProjectionElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return e; }
		internal static void parseFields(IfcProjectionElement e, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcFeatureElementAddition.parseFields(e, arrFields, ref ipos);
			if (schema != ReleaseVersion.IFC2x3)
				e.mPredefinedType = (IfcProjectionElementTypeEnum)Enum.Parse(typeof(IfcProjectionElementTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcProjectionElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
	}
	public partial class IfcProjectLibrary : IfcContext
	{
		internal IfcProjectLibrary() : base() { }
		internal IfcProjectLibrary(DatabaseIfc db, IfcProjectLibrary l) : base(db, l) { }
		public IfcProjectLibrary(DatabaseIfc m, string name, IfcUnitAssignment.Length length)
			: base(m, name, length)
		{
			if (m.ModelView == ModelView.Ifc4Reference || m.ModelView == ModelView.Ifc2x3Coordination)
				throw new Exception("Invalid Model View for IfcProjectLibrary : " + m.ModelView.ToString());
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECTLIBRARY";
		}
		internal static IfcProjectLibrary Parse(string strDef) { IfcProjectLibrary p = new IfcProjectLibrary(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			if (schema == ReleaseVersion.IFC2x3)
			{
				IfcBuilding building = new IfcBuilding(mDatabase, Name);
				IfcProject project = new IfcProject(building, Name) { UnitsInContext = UnitsInContext };
				List<IfcTypeProduct> types = DeclaredTypes;
				for (int icounter = 0; icounter < types.Count; icounter++)
				{
					IfcTypeProduct tp = types[icounter];
					tp.changeSchema(schema);
					if (tp.mRepresentationMaps.Count > 0)
						tp.genMappedItemElement(building, new IfcCartesianTransformationOperator3D(mDatabase));

					List<IfcPropertySetDefinition> psets = tp.HasPropertySets;
					for (int pcounter = 0; pcounter < psets.Count; pcounter++)
					{
						IfcPropertySet pset = psets[pcounter] as IfcPropertySet;
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
		internal IfcProjectOrder(DatabaseIfc db, IfcProjectOrder o) : base(db,o) { mPredefinedType = o.mPredefinedType; mStatus = o.mStatus; mLongDescription = o.mLongDescription; }
		internal static IfcProjectOrder Parse(string strDef,ReleaseVersion schema) { IfcProjectOrder p = new IfcProjectOrder(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcProjectOrder p, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcControl.parseFields(p, arrFields, ref ipos,schema);
			if (schema == ReleaseVersion.IFC2x3)
				p.mIdentification = arrFields[ipos++].Replace("'", "");
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				p.mPredefinedType = (IfcProjectOrderTypeEnum)Enum.Parse(typeof(IfcProjectOrderTypeEnum), s.Replace(".", ""));
			p.mStatus = arrFields[ipos++].Replace("'", "");
			if (schema != ReleaseVersion.IFC2x3)
				p.mLongDescription = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ",'" + mIdentification + "',." : ",.") + mPredefinedType.ToString() + (mStatus == "$" ? ".,$" : ".," + mStatus + "'") + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mLongDescription == "$" ? ",$" : ",'" + mLongDescription + "'")); }
	}
	public partial class IfcProjectOrderRecord : IfcControl // DEPRECEATED IFC4
	{
		internal List<int> mRecords = new List<int>();// : LIST [1:?] OF UNIQUE IfcRelAssignsToProjectOrder;
		internal IfcProjectOrderRecordTypeEnum mPredefinedType;// : IfcProjectOrderRecordTypeEnum; 
		//public List<ifcrelassignstopr>
		internal IfcProjectOrderRecord() : base() { }
		internal IfcProjectOrderRecord(DatabaseIfc db, IfcProjectOrderRecord r) : base(db, r) { }// Records = r.Records mPredefinedType = i.mPredefinedType; }
		internal static IfcProjectOrderRecord Parse(string strDef, ReleaseVersion schema) { IfcProjectOrderRecord r = new IfcProjectOrderRecord(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return r; }
		internal static void parseFields(IfcProjectOrderRecord r, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcControl.parseFields(r, arrFields, ref ipos,schema);
			r.mRecords = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			r.mPredefinedType = (IfcProjectOrderRecordTypeEnum)Enum.Parse(typeof(IfcProjectOrderRecordTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRecords[0]);
			for (int icounter = 1; icounter < mRecords.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRecords[icounter]);
			return str + "),." + mPredefinedType.ToString() + ".";
		}
	}
	public abstract partial class IfcProperty : IfcPropertyAbstraction  //ABSTRACT SUPERTYPE OF (ONEOF(IfcComplexProperty,IfcSimpleProperty));
	{
		internal string mName = ""; //: IfcIdentifier;
		internal string mDescription = "$"; //: OPTIONAL IfcText;
		//INVERSE
		internal List<IfcPropertySet> mPartOfPset = new List<IfcPropertySet>();//	:	SET OF IfcPropertySet FOR HasProperties;
		//internal List<IfcPropertyDependencyRelationship> mPropertyForDependance	:	SET OF IfcPropertyDependencyRelationship FOR DependingProperty;
		//PropertyDependsOn	:	SET OF IfcPropertyDependencyRelationship FOR DependantProperty;
		internal List<IfcComplexProperty> mPartOfComplex = new List<IfcComplexProperty>();//	:	SET OF IfcComplexProperty FOR HasProperties;

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { if (!string.IsNullOrEmpty(value)) mName = ParserIfc.Encode(value); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcProperty() : base() { }
		protected IfcProperty(DatabaseIfc db, IfcProperty p) : base(db, p) { mName = p.mName; mDescription = p.mDescription; }
		protected IfcProperty(DatabaseIfc db, string name, string desc) : base(db) { Name = name; Description = desc; }
		protected static void parseFields(IfcProperty p, List<string> arrFields, ref int ipos) { p.mName = arrFields[ipos++].Replace("'", ""); p.mDescription = arrFields[ipos++].Replace("'", ""); }
		protected override void Parse(string str, ref int pos, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mName + (mDescription == "$" ? "',$" : "','" + mDescription + "'"); }
		public override bool Destruct(bool children)
		{
			return (mPartOfPset.Count == 0 && mPartOfComplex.Count == 0 ? base.Destruct(children) : false);
		}
	}
	public abstract partial class IfcPropertyAbstraction : BaseClassIfc, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcExtendedProperties ,IfcPreDefinedProperties ,IfcProperty ,IfcPropertyEnumeration));
	{ //INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4 
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }
		protected IfcPropertyAbstraction() : base() { }
		protected IfcPropertyAbstraction(DatabaseIfc db) : base(db) { }
		protected IfcPropertyAbstraction(DatabaseIfc db, IfcPropertyAbstraction p) : base(db,p) { }
		protected static void parseFields(IfcPropertyAbstraction p, List<string> arrFields, ref int ipos) { }
		protected virtual void Parse(string str, ref int pos, int len) { }
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
		internal IfcPropertyBoundedValue(DatabaseIfc db, string name, string desc, IfcValue upper, IfcValue lower, IfcUnit unit, IfcValue set)
			: base(db, name, desc)
		{
			mUpperBoundValue = upper;
			mLowerBoundValue = lower;
			mSetPointValue = set;
			Unit = unit;
		}
		internal static void parseFields(IfcPropertyBoundedValue p, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcSimpleProperty.parseFields(p, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s != "$")
				p.mUpperBoundValue = ParserIfc.parseValue(s);
			s = arrFields[ipos++];
			if (s != "$")
				p.mLowerBoundValue = ParserIfc.parseValue(s);
			s = arrFields[ipos++];
			if (s != "$")
				p.mUnit = ParserSTEP.ParseLink(s);
			if (schema != ReleaseVersion.IFC2x3)
			{
				s = arrFields[ipos++];
				if (s != "$")
					p.mSetPointValue = ParserIfc.parseValue(arrFields[ipos++]);
			}
		}
		internal static IfcPropertyBoundedValue Parse(string strDef, ReleaseVersion schema) { IfcPropertyBoundedValue p = new IfcPropertyBoundedValue(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return p; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mUpperBoundValue == null ? ",$," : "," + mUpperBoundValue.ToString() + ",") + (mLowerBoundValue == null ? "$," : mLowerBoundValue.ToString() + ",") + ParserSTEP.LinkToString(mUnit) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mSetPointValue == null ? ",$" : "," + mSetPointValue.ToString())); }

	}
	public partial class IfcPropertyBoundedValue<T> : IfcSimpleProperty where T : IfcValue
	{
		internal T mUpperBoundValue;// : OPTIONAL IfcValue;
		internal T mLowerBoundValue;// : OPTIONAL IfcValue;   
		internal int mUnit;// : OPTIONAL IfcUnit;
		internal T mSetPointValue;// 	OPTIONAL IfcValue; IFC4

		public T UpperBoundValue { get { return mUpperBoundValue; } set { mUpperBoundValue = value; } }
		public T LowerBoundValue { get { return mUpperBoundValue; } set { mUpperBoundValue = value; } }
		public IfcUnit Unit { get { return mDatabase[mUnit] as IfcUnit; } set { mUnit = (value == null ? 0 : value.Index); } }
		public T SetPointValue { get { return mSetPointValue; } set { mSetPointValue = value; } }

		internal IfcPropertyBoundedValue() : base() { }
		//internal IfcPropertyBoundedValue(DatabaseIfc db, IfcPropertyBoundedValue p) : base(db, p)
		//{
		//	mUpperBoundValue = p.mUpperBoundValue;
		//	mLowerBoundValue = p.mLowerBoundValue;
		//	if (p.mUnit > 0)
		//		Unit = p.Unit.Duplicate(db);
		//	mSetPointValue = p.mSetPointValue;
		//}
		internal IfcPropertyBoundedValue(DatabaseIfc db, string name, string desc, T upper, T lower, IfcUnit unit, T set)
			: base(db, name, desc)
		{
			mUpperBoundValue = upper;
			mLowerBoundValue = lower;
			mSetPointValue = set;
			Unit = unit;
		}
		//internal static void parseFields(IfcPropertyBoundedValue p, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		//{
		//	IfcSimpleProperty.parseFields(p, arrFields, ref ipos);
		//	string s = arrFields[ipos++];
		//	if (s != "$")
		//		p.mUpperBoundValue = ParserIfc.parseValue(s);
		//	s = arrFields[ipos++];
		//	if (s != "$")
		//		p.mLowerBoundValue = ParserIfc.parseValue(s);
		//	s = arrFields[ipos++];
		//	if (s != "$")
		//		p.mUnit = ParserSTEP.ParseLink(s);
		//	if (schema != ReleaseVersion.IFC2x3)
		//	{
		//		s = arrFields[ipos++];
		//		if (s != "$")
		//			p.mSetPointValue = ParserIfc.parseValue(arrFields[ipos++]);
		//	}
		//}
		//internal static IfcPropertyBoundedValue Parse(string strDef, ReleaseVersion schema) { IfcPropertyBoundedValue p = new IfcPropertyBoundedValue(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mUpperBoundValue == null ? ",$," : "," + mUpperBoundValue.ToString() + ",") + (mLowerBoundValue == null ? "$," : mLowerBoundValue.ToString() + ",") + ParserSTEP.LinkToString(mUnit) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mSetPointValue == null ? ",$" : "," + mSetPointValue.ToString())); }
	}
	public abstract partial class IfcPropertyDefinition : IfcRoot, IfcDefinitionSelect //(IfcPropertySetDefinition, IfcPropertyTemplateDefinition)
	{ //INVERSE
		internal IfcRelDeclares mHasContext = null;// :	SET [0:1] OF IfcRelDeclares FOR RelatedDefinitions;
		internal List<IfcRelAssociates> mHasAssociations = new List<IfcRelAssociates>();//	 : 	SET OF IfcRelAssociates FOR RelatedObjects;

		public IfcRelDeclares HasContext { get { return mHasContext; } set { mHasContext = value; } }
		public List<IfcRelAssociates> HasAssociations { get { return mHasAssociations; } }

		protected IfcPropertyDefinition() : base() { }
		internal IfcPropertyDefinition(DatabaseIfc db) : base(db) { }
		protected IfcPropertyDefinition(DatabaseIfc db, IfcPropertyDefinition p) : base(db, p)
		{
			foreach (IfcRelAssociates associates in mHasAssociations)
			{
				IfcRelAssociates dup = db.Factory.Duplicate(associates) as IfcRelAssociates;
				dup.addAssociation(this);
			}
		}
		protected static void parseFields(IfcPropertyDefinition p, List<string> arrFields, ref int ipos) { IfcRoot.parseFields(p, arrFields, ref ipos); }
		internal virtual void Associate(IfcRelAssociates a) { mHasAssociations.Add(a); }
	}
	//ENTITY IfcPropertyDependencyRelationship;	
	public partial class IfcPropertyEnumeratedValue : IfcSimpleProperty
	{
		internal List<IfcValue> mEnumerationValues = new List<IfcValue>();// : LIST [1:?] OF IfcValue;
		internal int mEnumerationReference;// : OPTIONAL IfcPropertyEnumeration;   

		public List<IfcValue> EnumerationValues { get { return mEnumerationValues; } set { mEnumerationValues = value; } }
		public IfcPropertyEnumeration EnumerationReference { get { return mDatabase[mEnumerationReference] as IfcPropertyEnumeration; } set { mEnumerationReference = value == null ? 0 : value.mIndex; } }

		internal IfcPropertyEnumeratedValue() : base() { }
		public IfcPropertyEnumeratedValue(DatabaseIfc db, string name, IfcValue value) : base(db,name,"") { EnumerationValues.Add(value); }
		internal IfcPropertyEnumeratedValue(DatabaseIfc db, string name, IfcValue value, IfcPropertyEnumeration reference) : base(db,name,"") { EnumerationValues.Add(value); EnumerationReference = reference; }
		internal IfcPropertyEnumeratedValue(DatabaseIfc db, string name, List<IfcValue> values, IfcPropertyEnumeration reference) : base(db,name,"") { EnumerationValues = values; EnumerationReference = reference; }
		internal static void parseFields(IfcPropertyEnumeratedValue p, List<string> arrFields, ref int ipos)
		{
			IfcSimpleProperty.parseFields(p, arrFields, ref ipos);
			string str = arrFields[ipos++];
			List<string> fields = ParserSTEP.SplitLineFields(str.Substring(1,str.Length-2));
			p.mEnumerationValues = fields.ConvertAll(x=>ParserIfc.parseValue(x));
			p.mEnumerationReference = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		internal static IfcPropertyEnumeratedValue Parse(string strDef) { IfcPropertyEnumeratedValue p = new IfcPropertyEnumeratedValue(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + ",(" + mEnumerationValues[0].ToString();
			for (int icounter = 1; icounter < mEnumerationValues.Count; icounter++)
				result += "," + mEnumerationValues[icounter].ToString();
			return result + ")," + ParserSTEP.LinkToString(mEnumerationReference); }
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
				Unit = db.Factory.Duplicate( p.mDatabase[p.mUnit]) as IfcUnit;
		}
		public IfcPropertyEnumeration(DatabaseIfc db,string name, List<IfcValue> values, IfcUnit unit) : base(db) { Name = name; mEnumerationValues = values; Unit = unit; }
		internal static void parseFields(IfcPropertyEnumeration p, List<string> arrFields, ref int ipos)
		{
			IfcPropertyAbstraction.parseFields(p, arrFields, ref ipos);
			p.mName = arrFields[ipos++].Replace("'", "");
			string s = arrFields[ipos++];
			p.mEnumerationValues = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => ParserIfc.parseValue(x));
			p.mUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		internal static IfcPropertyEnumeration Parse(string strDef) { IfcPropertyEnumeration p = new IfcPropertyEnumeration(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildStringSTEP()
		{
			string s = base.BuildStringSTEP() + ",'" + mName + "',(" + mEnumerationValues[0].ToString();
			for (int icounter = 1; icounter < mEnumerationValues.Count; icounter++)
				s += "," + mEnumerationValues[icounter].ToString();
			return s + ")," + ParserSTEP.LinkToString(mUnit);
		}
	}
	public partial class IfcPropertyListValue : IfcSimpleProperty
	{
		private List<IfcValue> mNominalValue = new List<IfcValue>();// :	OPTIONAL LIST [1:?] OF IfcValue;
		private int mUnit;// : OPTIONAL IfcUnit; 

		public List<IfcValue> NominalValue { get { return mNominalValue.Count == 0 ? null : mNominalValue; } set { mNominalValue = (value == null ? new List<IfcValue>() : value); } }
		public IfcUnit Unit { get { return mDatabase[mUnit] as IfcUnit; } set { mUnit = value == null ? 0 : value.Index; } }

		internal IfcPropertyListValue() : base() { }
		internal IfcPropertyListValue(DatabaseIfc db, IfcPropertyListValue p) : base(db, p)
		{
			mNominalValue = p.mNominalValue;
			if (p.mUnit > 0)
				Unit = db.Factory.Duplicate(p.mDatabase[p.mUnit]) as IfcUnit;
		}
		internal IfcPropertyListValue(DatabaseIfc db, string name, string desc, List<IfcValue> values, IfcUnit unit)
			: base(db, name, desc) { NominalValue = values; Unit = unit; }
		internal static IfcPropertyListValue parseFields(IfcPropertyListValue p, List<string> arrFields, ref int ipos)
		{
			IfcSimpleProperty.parseFields(p, arrFields, ref ipos);
			String str = arrFields[ipos++];
			List<string> values = ParserSTEP.SplitLineFields(str.Substring(1, str.Length - 2));
			for (int icounter = 0; icounter < values.Count; icounter++)
			{
				IfcValue value = ParserIfc.parseValue(values[icounter]);
				if (value != null)
					p.mNominalValue.Add(value);
			}
			p.mUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
			return p;
		}
		internal static IfcPropertyListValue Parse(string strDef) { int ipos = 0; IfcPropertyListValue p = new IfcPropertyListValue(); parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP();
			if (mNominalValue == null)
				result += ",$,";
			else
			{
				result += ",(" + mNominalValue[0].ToString();
				for (int icounter = 1; icounter < mNominalValue.Count; icounter++)
					result += "," + mNominalValue[icounter].ToString();
				result += "),";
			}
			return result + (mUnit == 0 ? "$" : "#" + mUnit);
		}
	}
	public partial class IfcPropertyReferenceValue : IfcSimpleProperty
	{
		internal string mUsageName = "$";// 	 :	OPTIONAL IfcText;
		internal int mPropertyReference = 0;// 	 :	OPTIONAL IfcObjectReferenceSelect;

		public string UsageName { get { return (mUsageName == "$" ? "" : ParserIfc.Decode(mUsageName)); } set { mUsageName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		//

		internal IfcPropertyReferenceValue() : base() { }
		internal IfcPropertyReferenceValue(DatabaseIfc db, IfcPropertyReferenceValue p) : base(db, p)
		{
			mUsageName = p.mUsageName;
#warning todo
			//if(p.mPropertyReference > 0)
			//	PropertyReference = db.Factory.Duplicate( p.PropertyReference) as ;
		}
		internal IfcPropertyReferenceValue(DatabaseIfc db, string name, string desc) : base(db, name, desc) { }
		internal static void parseFields(IfcPropertyReferenceValue p, List<string> arrFields, ref int ipos) { IfcSimpleProperty.parseFields(p, arrFields, ref ipos); p.mUsageName = arrFields[ipos++]; p.mPropertyReference = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcPropertyReferenceValue Parse(string strDef) { IfcPropertyReferenceValue p = new IfcPropertyReferenceValue(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mUsageName == "$" ? ",$," : ",'" + mUsageName + "',") + (mPropertyReference == 0 ? "$" : "#" + mPropertyReference); }
	}
	public partial class IfcPropertySet : IfcPropertySetDefinition
	{
		public override string KeyWord { get { return "IfcPropertySet"; } }
		protected List<int> mHasProperties = new List<int>(1);// : SET [1:?] OF IfcProperty;

		public List<IfcProperty> HasProperties
		{
			get { return mHasProperties.ConvertAll(x => mDatabase[x] as IfcProperty); }
			set
			{
				mHasProperties.Clear();
				for (int icounter = 0; icounter < value.Count; icounter++)
				{
					mHasProperties.Add(value[icounter].mIndex);
					if(!value[icounter].mPartOfPset.Contains(this))
						value[icounter].mPartOfPset.Add(this);
				}
			}
		}

		internal IfcPropertySet() : base() { }
		internal IfcPropertySet(DatabaseIfc db, IfcPropertySet s) : base(db, s) { HasProperties = s.HasProperties.ConvertAll(x => db.Factory.Duplicate(x) as IfcProperty); }
		public IfcPropertySet(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPropertySet(string name, IfcProperty prop) : base(prop.mDatabase, name) { HasProperties = new List<IfcProperty>() { prop };  }
		public IfcPropertySet(string name, List<IfcProperty> props) : base(props[0].mDatabase, name) { HasProperties = props;  }
		internal static IfcPropertySet Parse(string str)
		{
			IfcPropertySet p = new IfcPropertySet();
			int pos = 0, len = str.Length;
			p.Parse(str, ref pos, len);
			p.mHasProperties = ParserSTEP.StripListLink(str, ref pos, len);
			return p;
		}
		protected override string BuildStringSTEP()
		{
			if (mHasProperties.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mHasProperties[0]);
			for (int icounter = 1; icounter < mHasProperties.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mHasProperties[icounter]);
			return str + ")";
		}

		internal override void postParseRelate()
		{
			base.postParseRelate();
			List<IfcProperty> props = HasProperties;
			for (int icounter = 0; icounter < props.Count; icounter++)
				props[icounter].mPartOfPset.Add(this);
		}
		public override List<T> Extract<T>()
		{
			List<T> result = base.Extract<T>();
			foreach (int i in mHasProperties)
				result.AddRange(mDatabase[i].Extract<T>());
			return result;
		}

		
		public IfcProperty FindProperty(string name)
		{
			List<IfcProperty> props = HasProperties;
			foreach (IfcProperty p in props)
			{
				if (string.Compare(p.mName, name, true) == 0)
					return p;
			}
			return null;
		}
		internal override List<IBaseClassIfc> retrieveReference(IfcReference r)
		{
			IfcReference ir = r.InnerReference;
			List<IBaseClassIfc> result = new List<IBaseClassIfc>();
			if (ir == null)
			{
				if (string.Compare(r.mAttributeIdentifier, "HasProperties", true) == 0)
				{
					List<IfcProperty> props = HasProperties;
					if (r.mListPositions.Count == 0)
					{
						string name = r.InstanceName;

						if (!string.IsNullOrEmpty(name))
						{
							foreach (IfcProperty p in props)
							{
								if (string.Compare(p.Name, name) == 0)
									result.Add(p);
							}
						}
						else
							result.AddRange(props);
					}
					else
					{
						foreach (int i in r.mListPositions)
						{
							result.Add(props[i - 1]);
						}
					}
					return result;
				}
			}
			if (string.Compare(r.mAttributeIdentifier, "HasProperties", true) == 0)
			{
				List<IfcProperty> props = HasProperties;
				if (r.mListPositions.Count == 0)
				{
					string name = r.InstanceName;

					if (string.IsNullOrEmpty(name))
					{
						foreach (IfcProperty p in props)
							result.AddRange(p.retrieveReference(r.InnerReference));
					}
					else
					{
						foreach (IfcProperty p in props)
						{
							if (string.Compare(name, p.Name) == 0)
								result.AddRange(p.retrieveReference(r.InnerReference));
						}
					}
				}
				else
				{
					foreach (int i in r.mListPositions)
						result.AddRange(props[i - 1].retrieveReference(ir));
				}
				return result;
			}
			return base.retrieveReference(r);
		}
	}
	public abstract partial class IfcPropertySetDefinition : IfcPropertyDefinition //ABSTRACT SUPERTYPE OF (ONEOF (IfcElementQuantity,IfcEnergyProperties ,
	{ // IfcFluidFlowProperties,IfcPropertySet, IfcServiceLifeFactor ,IfcSoundProperties ,IfcSoundValue ,IfcSpaceThermalLoadProperties ))
		//INVERSE
		internal List<IfcTypeObject> mDefinesType = new List<IfcTypeObject>();// :	SET OF IfcTypeObject FOR HasPropertySets; IFC4change
		internal List<IfcRelDefinesByTemplate> mIsDefinedBy = new List<IfcRelDefinesByTemplate>();//IsDefinedBy	 :	SET OF IfcRelDefinesByTemplate FOR RelatedPropertySets;
		private IfcRelDefinesByProperties mDefinesOccurrence = null; //:	SET [0:1] OF IfcRelDefinesByProperties FOR RelatingPropertyDefinition;
		
		public List<IfcTypeObject> DefinesType { get { return mDefinesType; } }
		public List<IfcRelDefinesByTemplate> IsDefinedBy { get { return mIsDefinedBy; } }
		public IfcRelDefinesByProperties DefinesOccurrence { get { if (mDefinesOccurrence == null) mDefinesOccurrence = new IfcRelDefinesByProperties(this) { Name = Name }; return mDefinesOccurrence; } set { mDefinesOccurrence = value; } }

		protected IfcPropertySetDefinition() : base() { }
		protected IfcPropertySetDefinition(DatabaseIfc db, IfcPropertySetDefinition s) : base(db, s) { }
		protected IfcPropertySetDefinition(DatabaseIfc m, string name) : base(m) { Name = name; }
		internal static void parseFields(IfcPropertySetDefinition d, List<string> arrFields, ref int ipos) { IfcPropertyDefinition.parseFields(d, arrFields, ref ipos); }

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
					mDefinesOccurrence.Assign(od);
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
		public List<IfcPropertyTemplate> HasPropertyTemplates { get { return mHasPropertyTemplates.ConvertAll(x => mDatabase[x] as IfcPropertyTemplate); } set { mHasPropertyTemplates = value.ConvertAll(x => x.mIndex); } }

		internal IfcPropertySetTemplate() : base() { }
		public IfcPropertySetTemplate(DatabaseIfc db, IfcPropertySetTemplate p) : base(db, p)
		{
			mTemplateType = p.mTemplateType;
			mApplicableEntity = p.mApplicableEntity;
			HasPropertyTemplates = p.HasPropertyTemplates.ConvertAll(x => db.Factory.Duplicate(x) as IfcPropertyTemplate);
		}
		//public IfcPropertySetTemplate(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPropertySetTemplate(string name, List<IfcPropertyTemplate> props) : base(props[0].mDatabase, name) { HasPropertyTemplates = props; }
		internal static IfcPropertySetTemplate Parse(string str)
		{
			IfcPropertySetTemplate p = new IfcPropertySetTemplate();
			int pos = 0, len = str.Length;
			p.Parse(str, ref pos, len);
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (field.StartsWith("."))
				p.mTemplateType = (IfcPropertySetTemplateTypeEnum)Enum.Parse(typeof(IfcPropertySetTemplateTypeEnum), field.Replace(".", ""));
			p.mApplicableEntity = ParserSTEP.StripField(str, ref pos, len).Replace("'", "");
			p.mHasPropertyTemplates = ParserSTEP.StripListLink(str, ref pos, len);
			return p;
		}
		protected override string BuildStringSTEP()
		{
			if (mDatabase.Release == ReleaseVersion.IFC2x3 || mHasPropertyTemplates.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + (mTemplateType == IfcPropertySetTemplateTypeEnum.NOTDEFINED ? ",$," : ",." + mTemplateType + ".,") +
				(mApplicableEntity == "$" ? "$,(" : "'" + mApplicableEntity + "',(") + ParserSTEP.LinkToString(mHasPropertyTemplates[0]);
			for (int icounter = 1; icounter < mHasPropertyTemplates.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mHasPropertyTemplates[icounter]);
			return str + ")";
		}

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
		public IfcPropertySingleValue(DatabaseIfc m, string name, IfcValue value) : base(m, name,"") { mNominalValue = value; }
		public IfcPropertySingleValue(DatabaseIfc m, string name, string desc, IfcValue value) : base(m, name, desc) { mNominalValue = value; }
		public IfcPropertySingleValue(DatabaseIfc m, string name, string desc, bool value) : this(m, name, desc, new IfcBoolean(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, string desc, int value) : this(m, name, desc, new IfcInteger(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, string desc, double value) : this(m, name, desc, new IfcReal(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, string desc, string value) : this(m, name, desc, new IfcLabel(value)) { }
		public IfcPropertySingleValue(DatabaseIfc m, string name, string desc, IfcValue val, IfcUnit unit) : this(m, name, desc,val) {  Unit = unit; }
		
		
		internal static IfcPropertySingleValue Parse(string str)
		{
			IfcPropertySingleValue p = new IfcPropertySingleValue();
			int pos = 0, len = str.Length;
			p.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$" && !string.IsNullOrEmpty(s))
			{
				p.mNominalValue = ParserIfc.parseValue(s);
				if (p.mNominalValue == null)
					p.mVal = s;
			}
			p.mUnit = ParserSTEP.StripLink(str, ref pos, len);
			return p;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + (mNominalValue == null ? (string.IsNullOrEmpty(mVal) ? "$" :  mVal) : mNominalValue.ToString()) + "," + ParserSTEP.LinkToString(mUnit); }
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
		internal IfcPropertyTableValue(DatabaseIfc db, string name, string desc) : base(db, name, desc) { }
		internal static void parseFields(IfcPropertyTableValue p, List<string> arrFields, ref int ipos)
		{
			IfcSimpleProperty.parseFields(p, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] != '$')
			{
				List<string> ss = ParserSTEP.SplitLineFields(str);
				for (int icounter = 0; icounter < ss.Count; icounter++)
				{
					IfcValue v = ParserIfc.parseValue(ss[icounter]);
					if (v != null)
						p.mDefiningValues.Add(v);
				}
			}
			str = arrFields[ipos++];
			if (str[0] != '$')
			{
				List<string> ss = ParserSTEP.SplitLineFields(arrFields[ipos++]);
				for (int icounter = 0; icounter < ss.Count; icounter++)
				{
					IfcValue v = ParserIfc.parseValue(ss[icounter]);
					if (v != null)
						p.mDefinedValues.Add(v);
				}
			}
			p.mExpression = arrFields[ipos++];
			p.mDefiningUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mDefinedUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
			str = arrFields[ipos++];
			if (str[0] != '$')
				p.mCurveInterpolation = (IfcCurveInterpolationEnum)Enum.Parse(typeof(IfcCurveInterpolationEnum), str.Replace(".", ""));
		}
		internal static IfcPropertyTableValue Parse(string strDef) { IfcPropertyTableValue p = new IfcPropertyTableValue(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + (mDefiningValues.Count > 0 ? ",(" + mDefiningValues[0].ToString() : ",$,");
			for (int icounter = 1; icounter < mDefiningValues.Count; icounter++)
				result += "," + mDefiningValues[icounter].ToString();
			result += (mDefiningValues.Count > 0 ? ")," : "") + (mDefinedValues.Count > 0 ? "(" + mDefinedValues[0].ToString() : "$,");
			for (int icounter = 1; icounter < mDefinedValues.Count; icounter++)
				result += "," + mDefinedValues[icounter].ToString();
			return result + (mDefinedValues.Count > 0 ? ")," : "") + mExpression + "," + ParserSTEP.LinkToString(mDefiningUnit) + "," + ParserSTEP.LinkToString(mDefinedUnit) + ",." + mCurveInterpolation.ToString() + ".";
		}
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
		internal IfcPropertyTableValue(DatabaseIfc db, string name, string desc) : base(db, name, desc) { }
		internal static void parseFields(IfcPropertyTableValue p, List<string> arrFields, ref int ipos)
		{
			IfcSimpleProperty.parseFields(p, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] != '$')
			{
				List<string> ss = ParserSTEP.SplitLineFields(str);
				for (int icounter = 0; icounter < ss.Count; icounter++)
				{
					IfcValue v = ParserIfc.parseValue(ss[icounter]);
					if (v != null)
						p.mDefiningValues.Add(v);
				}
			}
			str = arrFields[ipos++];
			if (str[0] != '$')
			{
				List<string> ss = ParserSTEP.SplitLineFields(arrFields[ipos++]);
				for (int icounter = 0; icounter < ss.Count; icounter++)
				{
					IfcValue v = ParserIfc.parseValue(ss[icounter]);
					if (v != null)
						p.mDefinedValues.Add(v);
				}
			}
			p.mExpression = arrFields[ipos++];
			p.mDefiningUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mDefinedUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
			str = arrFields[ipos++];
			if (str[0] != '$')
				p.mCurveInterpolation = (IfcCurveInterpolationEnum)Enum.Parse(typeof(IfcCurveInterpolationEnum), str.Replace(".", ""));
		}
		internal static IfcPropertyTableValue Parse(string strDef) { IfcPropertyTableValue p = new IfcPropertyTableValue(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + (mDefiningValues.Count > 0 ? ",(" + mDefiningValues[0].ToString() : ",$,");
			for (int icounter = 1; icounter < mDefiningValues.Count; icounter++)
				result += "," + mDefiningValues[icounter].ToString();
			result += (mDefiningValues.Count > 0 ? ")," : "") + (mDefinedValues.Count > 0 ? "(" + mDefinedValues[0].ToString() : "$,");
			for (int icounter = 1; icounter < mDefinedValues.Count; icounter++)
				result += "," + mDefinedValues[icounter].ToString();
			return result + (mDefinedValues.Count > 0 ? ")," : "") + mExpression + "," + ParserSTEP.LinkToString(mDefiningUnit) + "," + ParserSTEP.LinkToString(mDefinedUnit) + ",." + mCurveInterpolation.ToString() + ".";
		}
	}
	public abstract partial class IfcPropertyTemplate : IfcPropertyTemplateDefinition    //ABSTRACT SUPERTYPE OF(ONEOF(IfcComplexPropertyTemplate, IfcSimplePropertyTemplate))
	{   //INVERSE
		//PartOfComplexTemplate	:	SET OF IfcComplexPropertyTemplate FOR HasPropertyTemplates;
		internal List<IfcPropertySetTemplate> mPartOfPsetTemplate = new List<IfcPropertySetTemplate>();//	:	SET OF IfcPropertySetTemplate FOR HasPropertyTemplates;

		protected IfcPropertyTemplate() : base() { }
		protected IfcPropertyTemplate(DatabaseIfc db, IfcPropertyTemplate t) : base(db,t) { }
		protected IfcPropertyTemplate(DatabaseIfc db, string name) : base(db, name) { }
	}
	public abstract partial class IfcPropertyTemplateDefinition : IfcPropertyDefinition // ABSTRACT SUPERTYPE OF(ONEOF(IfcPropertySetTemplate, IfcPropertyTemplate))
	{ 
	 	protected IfcPropertyTemplateDefinition() : base() { }
		protected IfcPropertyTemplateDefinition(DatabaseIfc db, IfcPropertyTemplateDefinition p) : base(db,p) { }
		protected IfcPropertyTemplateDefinition(DatabaseIfc m, string name) : base(m) { Name = name; }
		internal static void parseFields(IfcPropertyTemplateDefinition d, List<string> arrFields, ref int ipos) { IfcPropertyDefinition.parseFields(d, arrFields, ref ipos); }
	}
	public partial class IfcProtectiveDevice : IfcFlowController //IFC4
	{
		internal IfcProtectiveDeviceTypeEnum mPredefinedType = IfcProtectiveDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcProtectiveDeviceTypeEnum;
		public IfcProtectiveDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDevice() : base() { }
		internal IfcProtectiveDevice(DatabaseIfc db, IfcProtectiveDevice d) : base(db, d) { mPredefinedType = d.mPredefinedType; }
		public IfcProtectiveDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcProtectiveDevice s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcProtectiveDeviceTypeEnum)Enum.Parse(typeof(IfcProtectiveDeviceTypeEnum), str);
		}
		internal new static IfcProtectiveDevice Parse(string strDef) { IfcProtectiveDevice s = new IfcProtectiveDevice(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcProtectiveDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcProtectiveDeviceTrippingUnit : IfcDistributionControlElement //IFC4  
	{
		internal IfcProtectiveDeviceTrippingUnitTypeEnum mPredefinedType = IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED;
		public IfcProtectiveDeviceTrippingUnitTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceTrippingUnit() : base() { }
		internal IfcProtectiveDeviceTrippingUnit(DatabaseIfc db, IfcProtectiveDeviceTrippingUnit u) : base(db,u) { mPredefinedType = u.mPredefinedType; }
		public IfcProtectiveDeviceTrippingUnit(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcProtectiveDeviceTrippingUnit a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcProtectiveDeviceTrippingUnitTypeEnum)Enum.Parse(typeof(IfcProtectiveDeviceTrippingUnitTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcProtectiveDeviceTrippingUnit Parse(string strDef) { IfcProtectiveDeviceTrippingUnit d = new IfcProtectiveDeviceTrippingUnit(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcProtectiveDeviceTrippingUnitType : IfcDistributionControlElementType
	{
		internal IfcProtectiveDeviceTrippingUnitTypeEnum mPredefinedType = IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED;// : IfcProtectiveDeviceTrippingUnitTypeEnum;
		public IfcProtectiveDeviceTrippingUnitTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceTrippingUnitType() : base() { }
		internal IfcProtectiveDeviceTrippingUnitType(DatabaseIfc db, IfcProtectiveDeviceTrippingUnitType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcProtectiveDeviceTrippingUnitType(DatabaseIfc m, string name, IfcProtectiveDeviceTrippingUnitTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcProtectiveDeviceTrippingUnitType t, List<string> arrFields, ref int ipos) { IfcDistributionControlElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcProtectiveDeviceTrippingUnitTypeEnum)Enum.Parse(typeof(IfcProtectiveDeviceTrippingUnitTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcProtectiveDeviceTrippingUnitType Parse(string strDef) { IfcProtectiveDeviceTrippingUnitType t = new IfcProtectiveDeviceTrippingUnitType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcProtectiveDeviceType : IfcFlowControllerType
	{
		internal IfcProtectiveDeviceTypeEnum mPredefinedType = IfcProtectiveDeviceTypeEnum.NOTDEFINED;// : IfcProtectiveDeviceTypeEnum; 
		public IfcProtectiveDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProtectiveDeviceType() : base() { }
		internal IfcProtectiveDeviceType(DatabaseIfc db, IfcProtectiveDeviceType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcProtectiveDeviceType(DatabaseIfc m, string name, IfcProtectiveDeviceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcProtectiveDeviceType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcProtectiveDeviceTypeEnum)Enum.Parse(typeof(IfcProtectiveDeviceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcProtectiveDeviceType Parse(string strDef) { IfcProtectiveDeviceType t = new IfcProtectiveDeviceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcProxy : IfcProduct
	{
		internal IfcObjectTypeEnum mProxyType;// : IfcObjectTypeEnum;
		internal string mTag = "$";// : OPTIONAL IfcLabel;
		internal IfcProxy() : base() { }
		internal IfcProxy(DatabaseIfc db, IfcProxy p) : base(db,p,false) { mProxyType = p.mProxyType; mTag = p.mTag; }
		internal static void parseFields(IfcProxy p, List<string> arrFields, ref int ipos) { IfcProduct.parseFields(p, arrFields, ref ipos); p.mProxyType = (IfcObjectTypeEnum)Enum.Parse(typeof(IfcObjectTypeEnum), arrFields[ipos++].Replace(".", "")); p.mTag = arrFields[ipos++].Replace("'", ""); }
		internal static IfcProxy Parse(string strDef) { IfcProxy p = new IfcProxy(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mProxyType.ToString() + ".," + (mTag == "$" ? "$" : "'" + mTag + "'"); }
	}
	public partial class IfcPump : IfcFlowMovingDevice //IFC4
	{
		internal IfcPumpTypeEnum mPredefinedType = IfcPumpTypeEnum.NOTDEFINED;// OPTIONAL : IfcPumpTypeEnum;
		public IfcPumpTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPump() : base() { }
		internal IfcPump(DatabaseIfc db, IfcPump p) : base(db,p) { mPredefinedType = p.mPredefinedType; }
		public IfcPump(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcPump s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcPumpTypeEnum)Enum.Parse(typeof(IfcPumpTypeEnum), str);
		}
		internal new static IfcPump Parse(string strDef) { IfcPump s = new IfcPump(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcPumpTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcPumpType : IfcFlowMovingDeviceType
	{
		internal IfcPumpTypeEnum mPredefinedType = IfcPumpTypeEnum.NOTDEFINED;// : IfcPumpTypeEnum; 
		public IfcPumpTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPumpType() : base() { }
		internal IfcPumpType(DatabaseIfc db, IfcPumpType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcPumpType(DatabaseIfc m, string name, IfcPumpTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcPumpType t, List<string> arrFields, ref int ipos) { IfcFlowMovingDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcPumpTypeEnum)Enum.Parse(typeof(IfcPumpTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcPumpType Parse(string strDef) { IfcPumpType t = new IfcPumpType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
}
