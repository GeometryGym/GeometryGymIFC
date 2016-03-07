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
	public abstract partial class IfcParameterizedProfileDef : IfcProfileDef //GG //ABSTRACT SUPERTYPE OF (ONEOF (IfcCShapeProfileDef ,IfcCircleProfileDef ,IfcCraneRailAShapeProfileDef ,IfcCraneRailFShapeProfileDef ,
	{//IfcEllipseProfileDef ,IfcIShapeProfileDef ,IfcLShapeProfileDef ,IfcRectangleProfileDef ,IfcTShapeProfileDef ,IfcTrapeziumProfileDef ,IfcUShapeProfileDef ,IfcZShapeProfileDef))*/
		internal int mPosition;// : IfcAxis2Placement2D //IFC4  OPTIONAL

		public IfcAxis2Placement2D Position
		{
			get { return (mPosition > 0 ? mDatabase.mIfcObjects[mPosition] as IfcAxis2Placement2D : null); }
			set { mPosition = (value == null ? (mDatabase.mSchema == Schema.IFC2x3 ? mDatabase.m2DPlaceOrigin.mIndex : 0) : value.mIndex); }
		}

		protected IfcParameterizedProfileDef() : base() { }
		protected IfcParameterizedProfileDef(IfcParameterizedProfileDef i) : base(i) { mPosition = i.mPosition; }
		protected IfcParameterizedProfileDef(DatabaseIfc m)
			: base(m)
		{
			if (mDatabase.mModelView == ModelView.Ifc4Reference)
				throw new Exception("Invalid Model View for IfcParameterizedProfileDef : " + m.ModelView.ToString());
			if (mDatabase.mSchema == Schema.IFC2x3)
				Position = (mDatabase.m2DPlaceOrigin == null ? new IfcAxis2Placement2D(m) : mDatabase.m2DPlaceOrigin);
		}

		protected static void parseFields(IfcParameterizedProfileDef p, List<string> arrFields, ref int ipos) { IfcProfileDef.parseFields(p, arrFields, ref ipos); p.mPosition = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mPosition); }
	}
	public class IfcPath : IfcTopologicalRepresentationItem
	{
		internal List<int> mEdgeList = new List<int>();// : SET [1:?] OF IfcOrientedEdge;
		internal IfcPath() : base() { }
		internal IfcPath(IfcPath i) : base(i) { mEdgeList = new List<int>(i.mEdgeList.ToArray()); }
		internal IfcPath(List<IfcOrientedEdge> edges) : base(edges[0].mDatabase) { mEdgeList = edges.ConvertAll(x => x.mIndex); }
		internal IfcPath(IfcOrientedEdge edge) : base(edge.mDatabase) { mEdgeList.Add(edge.mIndex); }
		internal static IfcPath Parse(string strDef) { IfcPath p = new IfcPath(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcPath p, List<string> arrFields, ref int ipos) { IfcTopologicalRepresentationItem.parseFields(p, arrFields, ref ipos); p.mEdgeList = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(";
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
		internal IfcPCurve() : base() { }
		internal IfcPCurve(IfcPCurve el) : base(el) { mBasisSurface = el.mBasisSurface; mReferenceCurve = el.mReferenceCurve; }

		internal static IfcPCurve Parse(string strDef) { IfcPCurve l = new IfcPCurve(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcPCurve l, List<string> arrFields, ref int ipos) { IfcCurve.parseFields(l, arrFields, ref ipos); l.mBasisSurface = ParserSTEP.ParseLink(arrFields[ipos++]); l.mReferenceCurve = ParserSTEP.ParseLink(arrFields[ipos++]); }

		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.LinkToString(mReferenceCurve); }
	}
	public class IfcPerformanceHistory : IfcControl
	{
		internal string mLifeCyclePhase;// : IfcLabel; 
		internal IfcPerformanceHistory() : base() { }
		internal IfcPerformanceHistory(IfcPerformanceHistory i) : base((IfcControl)i) { mLifeCyclePhase = i.mLifeCyclePhase; }
		internal static IfcPerformanceHistory Parse(string strDef, Schema schema) { IfcPerformanceHistory h = new IfcPerformanceHistory(); int ipos = 0; parseFields(h, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return h; }
		internal static void parseFields(IfcPerformanceHistory h, List<string> arrFields, ref int ipos, Schema schema) { IfcControl.parseFields(h, arrFields, ref ipos,schema); h.mLifeCyclePhase = arrFields[ipos++]; }
		protected override string BuildString() { return base.BuildString() + "," + mLifeCyclePhase; }
	}
	//ENTITY IfcPermeableCoveringProperties : IfcPreDefinedPropertySet //IFC2x3 
	public class IfcPermit : IfcControl
	{
		internal string mPermitID;// : IfcIdentifier; 
		internal IfcPermit() : base() { }
		internal IfcPermit(IfcPermit i) : base((IfcControl)i) { mPermitID = i.mPermitID; }
		internal static IfcPermit Parse(string strDef, Schema schema) { IfcPermit p = new IfcPermit(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcPermit p, List<string> arrFields, ref int ipos, Schema schema) { IfcControl.parseFields(p, arrFields, ref ipos,schema); p.mPermitID = arrFields[ipos++]; }
		protected override string BuildString() { return base.BuildString() + "," + mPermitID; }
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

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string FamilyName { get { return (mFamilyName == "$" ? "" : ParserIfc.Decode(mFamilyName)); } set { mFamilyName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string GivenName { get { return (mGivenName == "$" ? "" : ParserIfc.Decode(mGivenName)); } set { mGivenName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public List<string> MiddleNames { get { return mMiddleNames.ConvertAll(x => ParserIfc.Decode(x)); } set { mMiddleNames = (value == null ? new List<string>() : value.ConvertAll(x => ParserIfc.Encode(x.Replace("'", "")))); } }
		public List<string> PrefixTitles { get { return mPrefixTitles.ConvertAll(x => ParserIfc.Decode(x)); } set { mPrefixTitles = (value == null ? new List<string>() : value.ConvertAll(x => ParserIfc.Encode(x.Replace("'", "")))); } }
		public List<string> SuffixTitles { get { return mSuffixTitles.ConvertAll(x => ParserIfc.Decode(x)); } set { mSuffixTitles = (value == null ? new List<string>() : value.ConvertAll(x => ParserIfc.Encode(x.Replace("'", "")))); } }

		public List<IfcActorRole> Roles { get { return mRoles.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcActorRole); } set { mRoles = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		public List<IfcAddress> Addresses { get { return mAddresses.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcAddress); } set { mAddresses = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }

		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcPerson() : base() { }
		internal IfcPerson(IfcPerson i)
			: base()
		{
			mIdentification = i.mIdentification;
			mFamilyName = i.mFamilyName;
			mGivenName = i.mGivenName;
			mMiddleNames = i.mMiddleNames;
			mPrefixTitles = i.mPrefixTitles;
			mSuffixTitles = i.mSuffixTitles;
			mRoles = i.mRoles;
			mAddresses = i.mAddresses;
		}
		internal IfcPerson(DatabaseIfc m) : base(m)
		{
			mIdentification = System.Environment.UserName.Replace("'", "");
#if(IFCMODEL && !IFCIMPORTONLY && (RHINO ||GH))
			string str = GGYM.ggAssembly.mOptions.OwnerRole;
			if (!string.IsNullOrEmpty(str))
			{
				IfcRoleEnum role = IfcRoleEnum.NOTDEFINED;
				if (Enum.TryParse<IfcRoleEnum>(str, out role))
				{
					if (role != IfcRoleEnum.NOTDEFINED)
						Roles = new List<IfcActorRole>() { new IfcActorRole(m, role, "", "", new List<int>()) };
				}
				else
					Roles = new List<IfcActorRole>() { new IfcActorRole(m, IfcRoleEnum.USERDEFINED, str, "", new List<int>()) };
			}
#endif
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
		protected override string BuildString()
		{
			string str = base.BuildString() + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',");
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
	public class IfcPersonAndOrganization : BaseClassIfc, IfcActorSelect, IfcResourceObjectSelect
	{
		internal int mThePerson;// : IfcPerson;
		internal int mTheOrganization;// : IfcOrganization;
		internal string mRoles = "$";// TODO : OPTIONAL LIST [1:?] OF IfcActorRole
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcPersonAndOrganization() : base() { }
		internal IfcPersonAndOrganization(IfcPersonAndOrganization i) : base() { mThePerson = i.mThePerson; mTheOrganization = i.mTheOrganization; mRoles = i.mRoles; }
		internal IfcPersonAndOrganization(DatabaseIfc m) : base(m) { mThePerson = new IfcPerson(m).mIndex; mTheOrganization = new IfcOrganization(m).mIndex; }
		internal static void parseFields(IfcPersonAndOrganization c, List<string> arrFields, ref int ipos) { c.mThePerson = ParserSTEP.ParseLink(arrFields[ipos++]); c.mTheOrganization = ParserSTEP.ParseLink(arrFields[ipos++]); c.mRoles = arrFields[ipos++]; }
		internal static IfcPersonAndOrganization Parse(string strDef) { IfcPersonAndOrganization c = new IfcPersonAndOrganization(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mThePerson) + "," + ParserSTEP.LinkToString(mTheOrganization) + "," + mRoles; }
	}
	//ENTITY IfcPhysicalComplexQuantity
	public abstract class IfcPhysicalQuantity : BaseClassIfc, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF(ONEOF(IfcPhysicalComplexQuantity, IfcPhysicalSimpleQuantity));
	{
		internal string mName = "NoName";// : IfcLabel;
		internal string mDescription = "$"; // : OPTIONAL IfcText;
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public override string Name
		{
			get { return ParserIfc.Decode(mName); }
			set { mName = (string.IsNullOrEmpty(value) ? "NoName" : ParserIfc.Encode(value.Replace("'", ""))); }
		}
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		protected IfcPhysicalQuantity() : base() { }
		protected IfcPhysicalQuantity(IfcPhysicalQuantity q) : base() { mName = q.mName; mDescription = q.mDescription; }
		protected IfcPhysicalQuantity(DatabaseIfc m, string name, string desc) : base(m) { Name = name; Description = desc; }
		protected static void parseFields(IfcPhysicalQuantity q, List<string> arrFields, ref int ipos) { q.mName = arrFields[ipos++].Replace("'", ""); q.mDescription = arrFields[ipos++].Replace("'",""); }
		protected override string BuildString() { return base.BuildString() + ",'" + mName + (mDescription == "$" ? "',$" : "','" + mDescription + "'"); }
	}
	public abstract class IfcPhysicalSimpleQuantity : IfcPhysicalQuantity //ABSTRACT SUPERTYPE OF (ONEOF (IfcQuantityArea ,IfcQuantityCount ,IfcQuantityLength ,IfcQuantityTime ,IfcQuantityVolume ,IfcQuantityWeight))
	{
		internal int mUnit = 0;// : OPTIONAL IfcNamedUnit;	

		public IfcNamedUnit Unit { get { return mDatabase.mIfcObjects[mUnit] as IfcNamedUnit; } set { mUnit = (value == null ? 0 : value.mIndex); } }

		protected IfcPhysicalSimpleQuantity() : base() { }
		protected IfcPhysicalSimpleQuantity(IfcPhysicalSimpleQuantity q) : base(q) { mUnit = q.mUnit; }
		protected IfcPhysicalSimpleQuantity(DatabaseIfc m, string name, string desc, IfcNamedUnit unit) : base(m, name, desc) { Unit = unit; }
		protected static void parseFields(IfcPhysicalSimpleQuantity q, List<string> arrFields, ref int ipos) { IfcPhysicalQuantity.parseFields(q, arrFields, ref ipos); q.mUnit = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mUnit); }
	}
	public partial class IfcPile : IfcBuildingElement
	{
		internal IfcPileTypeEnum mPredefinedType = IfcPileTypeEnum.NOTDEFINED;// OPTIONAL : IfcPileTypeEnum;
		internal IfcPileConstructionEnum mConstructionType = IfcPileConstructionEnum.NOTDEFINED;// : OPTIONAL IfcPileConstructionEnum; 

		public IfcPileTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPile() : base() { }
		internal IfcPile(IfcPile p) : base(p) { mPredefinedType = p.mPredefinedType; mConstructionType = p.mConstructionType; }
		internal IfcPile(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }

		internal static IfcPile Parse(string strDef) { IfcPile p = new IfcPile(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema != Schema.IFC2x3 && mPredefinedType == IfcPileTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") +
				(mConstructionType == IfcPileConstructionEnum.NOTDEFINED ? "$" : "." + mConstructionType.ToString() + ".");
		}
		internal static void parseFields(IfcPile p, List<string> arrFields, ref int ipos)
		{
			IfcBuildingElement.parseFields(p, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s[0] == '.')
				p.mPredefinedType = (IfcPileTypeEnum)Enum.Parse(typeof(IfcPileTypeEnum), s.Replace(".", ""));
			s = arrFields[ipos++];
			if (s[0] == '.')
				p.mConstructionType = (IfcPileConstructionEnum)Enum.Parse(typeof(IfcPileConstructionEnum), s.Replace(".", ""));
		}
	}
	public partial class IfcPileType : IfcBuildingElementType
	{
		internal IfcPileTypeEnum mPredefinedType = IfcPileTypeEnum.NOTDEFINED;
		public IfcPileTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcPileType() : base() { }
		internal IfcPileType(IfcPileType b) : base(b) { mPredefinedType = b.mPredefinedType; }
		public IfcPileType(DatabaseIfc m, string name, IfcPileTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal IfcPileType(string name, IfcMaterialProfileSet mps, IfcPileTypeEnum type) : base(mps.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = mps; }

		internal static void parseFields(IfcStairType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcStairTypeEnum)Enum.Parse(typeof(IfcStairTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcStairType Parse(string strDef) { IfcStairType t = new IfcStairType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString() + ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcPipeFitting : IfcFlowFitting //IFC4
	{
		internal IfcPipeFittingTypeEnum mPredefinedType = IfcPipeFittingTypeEnum.NOTDEFINED;	// :	OPTIONAL IfcPipeFittingTypeEnum;
		public IfcPipeFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcPipeFitting() : base() { }
		internal IfcPipeFitting(IfcPipeFitting t) : base(t) { }

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
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcPipeFittingTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcPipeFittingType : IfcFlowFittingType
	{
		internal IfcPipeFittingTypeEnum mPredefinedType = IfcPipeFittingTypeEnum.NOTDEFINED;// : IfcPipeFittingTypeEnum; 
		public IfcPipeFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPipeFittingType() : base() { }
		internal IfcPipeFittingType(IfcPipeFittingType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcPipeFittingType(DatabaseIfc m, string name, double radius, double bendAngle) : base(m)
		{
			Name = name;
			mHasPropertySets.Add(genPSetBend(m, bendAngle, radius).mIndex);
			mPredefinedType = IfcPipeFittingTypeEnum.BEND;
		}
		internal IfcPipeFittingType(DatabaseIfc m, string name, IfcPipeFittingTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcPipeFittingType t, List<string> arrFields, ref int ipos) { IfcFlowFittingType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcPipeFittingTypeEnum)Enum.Parse(typeof(IfcPipeFittingTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcPipeFittingType Parse(string strDef) { IfcPipeFittingType t = new IfcPipeFittingType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }

		internal static IfcPropertySet genPSetBend(DatabaseIfc m, double bendAngle, double bendRadius)
		{
			List<IfcProperty> props = new List<IfcProperty>();
			if (bendAngle > 0)
				props.Add(new IfcPropertySingleValue(m, "BendAngle", "", new IfcPlaneAngleMeasure(bendAngle / m.mPlaneAngleToRadians), null));
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
		internal IfcPipeSegment(IfcPipeSegment s) : base(s) { mPredefinedType = s.mPredefinedType; }

		internal static void parseFields(IfcPipeSegment s, List<string> arrFields, ref int ipos)
		{
			IfcFlowSegment.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcPipeSegmentTypeEnum)Enum.Parse(typeof(IfcPipeSegmentTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcPipeSegment Parse(string strDef) { IfcPipeSegment s = new IfcPipeSegment(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcPipeSegmentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcPipeSegmentType : IfcFlowSegmentType
	{
		internal IfcPipeSegmentTypeEnum mPredefinedType = IfcPipeSegmentTypeEnum.NOTDEFINED;// : IfcPipeSegmentTypeEnum; 
		public IfcPipeSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcPipeSegmentType() : base() { }
		internal IfcPipeSegmentType(IfcPipeSegmentType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcPipeSegmentType(DatabaseIfc m, string name, IfcPipeSegmentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcPipeSegmentType t, List<string> arrFields, ref int ipos) { IfcFlowSegmentType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcPipeSegmentTypeEnum)Enum.Parse(typeof(IfcPipeSegmentTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcPipeSegmentType Parse(string strDef) { IfcPipeSegmentType t = new IfcPipeSegmentType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	//ENTITY IfcPixelTexture 
	public abstract partial class IfcPlacement : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcAxis1Placement ,IfcAxis2Placement2D ,IfcAxis2Placement3D))*/
	{
		private int mLocation;// : IfcCartesianPoint;
		internal IfcCartesianPoint Location { get { return (IfcCartesianPoint)mDatabase.mIfcObjects[mLocation]; } set { mLocation = value.mIndex; } }
		protected IfcPlacement(IfcPlacement o) : base(o) { mLocation = o.mLocation; }
		protected IfcPlacement() : base() { }
		protected IfcPlacement(DatabaseIfc m) : base(m) { mLocation = m.WorldOrigin.mIndex; }
		protected IfcPlacement(IfcCartesianPoint p) : base(p.mDatabase) { mLocation = p.mIndex; }
		protected static void parseFields(IfcPlacement p, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(p, arrFields, ref ipos); p.mLocation = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mLocation); }
	}
	public class IfcPlanarBox : IfcPlanarExtent
	{
		internal int mPlacement;// : IfcAxis2Placement; 
		internal IfcPlanarBox() : base() { }
		internal IfcPlanarBox(IfcPlanarBox p) : base((IfcPlanarExtent)p) { mPlacement = p.mPlacement; }
		internal static void parseFields(IfcPlanarBox b, List<string> arrFields, ref int ipos) { IfcPlanarExtent.parseFields(b, arrFields, ref ipos); b.mPlacement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal new static IfcPlanarBox Parse(string strDef) { IfcPlanarBox b = new IfcPlanarBox(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mPlacement); }
	}
	public class IfcPlanarExtent : IfcGeometricRepresentationItem
	{
		internal double mSizeInX;// : IfcLengthMeasure;
		internal double mSizeInY;// : IfcLengthMeasure; 
		internal IfcPlanarExtent() : base() { }
		internal IfcPlanarExtent(IfcPlanarExtent p) : base(p) { mSizeInX = p.mSizeInX; mSizeInY = p.mSizeInY; }
		internal static void parseFields(IfcPlanarExtent p, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(p, arrFields, ref ipos); p.mSizeInX = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mSizeInY = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcPlanarExtent Parse(string strDef) { IfcPlanarExtent p = new IfcPlanarExtent(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mSizeInX) + "," + ParserSTEP.DoubleToString(mSizeInY); }
	}
	public partial class IfcPlane : IfcElementarySurface
	{
		internal IfcPlane() : base() { }
		internal IfcPlane(IfcPlane o) : base(o) { }
		public IfcPlane(IfcAxis2Placement3D placement) : base(placement) { }
		internal static IfcPlane Parse(string strDef) { IfcPlane p = new IfcPlane(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcPlane p, List<string> arrFields, ref int ipos) { IfcElementarySurface.parseFields(p, arrFields, ref ipos); }
	}
	public partial class IfcPlate : IfcBuildingElement
	{
		internal IfcPlateTypeEnum mPredefinedType = IfcPlateTypeEnum.NOTDEFINED;//: OPTIONAL IfcPlateTypeEnum;
		public IfcPlateTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcPlate() : base() { }
		internal IfcPlate(IfcPlate o) : base(o) { mPredefinedType = o.mPredefinedType; }
		public IfcPlate(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }

		internal static IfcPlate Parse(string strDef, Schema schema) { IfcPlate p = new IfcPlate(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return p; }
		internal static void parseFields(IfcPlate p, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcBuildingElement.parseFields(p, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					p.mPredefinedType = (IfcPlateTypeEnum)Enum.Parse(typeof(IfcPlateTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? base.BuildString() : base.BuildString() + (mPredefinedType == IfcPlateTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcPlateStandardCase : IfcPlate //IFC4
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 || mDatabase.mModelView == ModelView.Ifc4Reference ? "IFCPLATE" : base.KeyWord); } }
		internal IfcPlateStandardCase() : base() { }
		internal IfcPlateStandardCase(IfcPlateStandardCase o) : base(o) { }

		internal static IfcPlateStandardCase Parse(string strDef) { IfcPlateStandardCase s = new IfcPlateStandardCase(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcPlateStandardCase s, List<string> arrFields, ref int ipos) { IfcPlate.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcPlateType : IfcBuildingElementType
	{
		internal IfcPlateTypeEnum mPredefinedType = IfcPlateTypeEnum.NOTDEFINED;
		public IfcPlateTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcPlateType() : base() { }
		internal IfcPlateType(IfcPlateType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcPlateType(DatabaseIfc m, string name, IfcPlateTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal IfcPlateType(string name, IfcMaterialLayerSet mls, IfcPlateTypeEnum type) : this(mls.mDatabase, name, type) { MaterialSelect = mls; }
		internal static void parseFields(IfcPlateType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcPlateTypeEnum)Enum.Parse(typeof(IfcPlateTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcPlateType Parse(string strDef) { IfcPlateType t = new IfcPlateType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract partial class IfcPoint : IfcGeometricRepresentationItem, IfcGeometricSetSelect, IfcPointOrVertexPoint /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCartesianPoint ,IfcPointOnCurve ,IfcPointOnSurface))*/
	{
		protected IfcPoint() : base() { }
		protected IfcPoint(IfcPoint p) : base(p) { }
		protected IfcPoint(DatabaseIfc m) : base(m) { }
		internal static void parseFields(IfcPoint p, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(p, arrFields, ref ipos); }
	}
	public partial class IfcPointOnCurve : IfcPoint
	{
		internal int mBasisCurve;// : IfcCurve;
		internal double mPointParameter;// : IfcParameterValue; 

		public IfcCurve BasisCurve { get { return mDatabase.mIfcObjects[mBasisCurve] as IfcCurve; } set { mBasisCurve = value.mIndex; } }
		internal IfcPointOnCurve() : base() { }
		internal IfcPointOnCurve(IfcPointOnCurve cp) : base() { mBasisCurve = cp.mBasisCurve; mPointParameter = cp.mPointParameter; }
		internal IfcPointOnCurve(DatabaseIfc m, IfcCurve c, double p) : base(m) { mBasisCurve = c.mIndex; mPointParameter = p; }
		internal static void parseFields(IfcPointOnCurve p, List<string> arrFields, ref int ipos) { IfcPoint.parseFields(p, arrFields, ref ipos); p.mBasisCurve = ParserSTEP.ParseLink(arrFields[ipos++]); p.mPointParameter = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcPointOnCurve Parse(string strDef) { IfcPointOnCurve p = new IfcPointOnCurve(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mBasisCurve) + "," + ParserSTEP.DoubleToString(mPointParameter); }
	}
	public partial class IfcPointOnSurface : IfcPoint
	{
		internal int mBasisSurface;// : IfcSurface;
		internal double mPointParameterU, mPointParameterV;// : IfcParameterValue; 
		public IfcSurface BasisSurface { get { return mDatabase.mIfcObjects[mBasisSurface] as IfcSurface; } set { mBasisSurface = value.mIndex; } }
		internal IfcPointOnSurface() : base() { }
		internal IfcPointOnSurface(IfcPointOnSurface cp) : base() { mBasisSurface = cp.mBasisSurface; mPointParameterU = cp.mPointParameterU; mPointParameterV = cp.mPointParameterV; }
		internal static void parseFields(IfcPointOnSurface p, List<string> arrFields, ref int ipos) { IfcPoint.parseFields(p, arrFields, ref ipos); p.mBasisSurface = ParserSTEP.ParseLink(arrFields[ipos++]); p.mPointParameterU = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mPointParameterV = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcPointOnSurface Parse(string strDef) { IfcPointOnSurface p = new IfcPointOnSurface(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.DoubleToString(mPointParameterU) + "," + ParserSTEP.DoubleToString(mPointParameterV); }
	}
	public interface IfcPointOrVertexPoint : IfcInterface { }
	public partial class IfcPolygonalBoundedHalfSpace : IfcHalfSpaceSolid
	{
		internal int mPosition;// : IfcAxis2Placement3D;
		internal int mPolygonalBoundary;// : IfcBoundedCurve; 
		internal IfcPolygonalBoundedHalfSpace() : base() { }
		internal IfcPolygonalBoundedHalfSpace(IfcPolygonalBoundedHalfSpace pl) : base(pl) { mPosition = pl.mPosition; mPolygonalBoundary = pl.mPolygonalBoundary; }
		internal static void parseFields(IfcPolygonalBoundedHalfSpace s, List<string> arrFields, ref int ipos) { IfcHalfSpaceSolid.parseFields(s, arrFields, ref ipos); s.mPosition = ParserSTEP.ParseLink(arrFields[ipos++]); s.mPolygonalBoundary = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal new static IfcPolygonalBoundedHalfSpace Parse(string strDef) { IfcPolygonalBoundedHalfSpace s = new IfcPolygonalBoundedHalfSpace(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mPosition) + "," + ParserSTEP.LinkToString(mPolygonalBoundary); }
	}
	public partial class IfcPolyline : IfcBoundedCurve
	{
		private List<int> mPoints = new List<int>();// : LIST [2:?] OF IfcCartesianPoint;
		public List<IfcCartesianPoint> Points { get { return mPoints.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcCartesianPoint); } set { mPoints = value.ConvertAll(x => x.mIndex); } }

		internal IfcPolyline() : base() { }
		internal IfcPolyline(IfcPolyline pl) : base(pl) { mPoints = new List<int>(pl.mPoints.ToArray()); }
		public IfcPolyline(IfcCartesianPoint start, IfcCartesianPoint end) : base(start.mDatabase) { mPoints.Add(start.mIndex); mPoints.Add(end.mIndex); }
		internal IfcPolyline(List<IfcCartesianPoint> pts) : base(pts[1].mDatabase) { Points = pts; }
		internal static void parseFields(IfcPolyline p, List<string> arrFields, ref int ipos) { IfcBoundedCurve.parseFields(p, arrFields, ref ipos); p.mPoints = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcPolyline Parse(string strDef) { IfcPolyline p = new IfcPolyline(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential)
				return "";
			string str = base.BuildString() + ",(";
			if (mPoints.Count > 0)
				str += ParserSTEP.LinkToString(mPoints[0]);
			for (int icounter = 1; icounter < mPoints.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mPoints[icounter]);
			str += ")";
			return base.BuildString() + str;
		}
	}
	public partial class IfcPolyloop : IfcLoop
	{
		internal List<int> mPolygon = new List<int>();// : LIST [3:?] OF UNIQUE IfcCartesianPoint;

		internal List<IfcCartesianPoint> Polygon { get { return mPolygon.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcCartesianPoint); } }

		internal IfcPolyloop() : base() { }
		internal IfcPolyloop(IfcPolyloop o) : base(o) { mPolygon = new List<int>(o.mPolygon.ToArray()); }
		public IfcPolyloop(List<IfcCartesianPoint> polygon) : base(polygon[0].mDatabase) { mPolygon = polygon.ConvertAll(x => x.mIndex); }
		public IfcPolyloop(IfcCartesianPoint cp1, IfcCartesianPoint cp2, IfcCartesianPoint cp3) : base(cp1.mDatabase) { mPolygon = new List<int>() { cp1.mIndex, cp2.mIndex, cp3.mIndex }; }
		internal new static IfcPolyloop Parse(string strDef) { IfcPolyloop l = new IfcPolyloop(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcPolyloop l, List<string> arrFields, ref int ipos) { IfcLoop.parseFields(l, arrFields, ref ipos); l.mPolygon = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential)
				return "";
			string str = base.BuildString() + ",(";
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
		protected IfcPort(IfcPort q) : base(q) { }
		protected IfcPort(DatabaseIfc db) : base(db) { }
		protected IfcPort(IfcElement e) : base(e.mDatabase)
		{
			if (mDatabase.mSchema == Schema.IFC2x3)
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
		protected IfcPort(IfcElementType t)
			: base(t.mDatabase)
		{
			if (t.mIsNestedBy.Count == 0)
				t.mIsNestedBy.Add(new IfcRelNests(t, this));
			else
				t.mIsNestedBy[0].addObject(this);
		}
		
		protected static void parseFields(IfcPort p, List<string> arrFields, ref int ipos) { IfcProduct.parseFields(p, arrFields, ref ipos); }
		
		internal IfcElement getElement()
		{
			if (mDatabase.mSchema == Schema.IFC2x3)
			{
			}
			else if (mNests != null)
				return mNests.RelatingObject as IfcElement;
			return null;
		}
	}
	public abstract class IfcPositioningElement : IfcProduct //IFC4.1
	{
		protected IfcPositioningElement() : base() { }
		protected IfcPositioningElement(IfcPositioningElement q) : base(q) { }
		protected static void parseFields(IfcPositioningElement p, List<string> arrFields, ref int ipos) { IfcProduct.parseFields(p, arrFields, ref ipos); }
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

		public string InternalLocation { get { return (mInternalLocation == "$" ? "" : ParserIfc.Decode(mInternalLocation)); } set { mInternalLocation = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public List<string> AddressLines { get { return mAddressLines.ConvertAll(x => ParserIfc.Decode(x)); } set { mAddressLines = (value == null ? new List<string>() : value.ConvertAll(x => ParserIfc.Encode(x))); } }
		public string PostalBox { get { return (mPostalBox == "$" ? "" : ParserIfc.Decode(mPostalBox)); } set { mPostalBox = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Town { get { return (mTown == "$" ? "" : ParserIfc.Decode(mTown)); } set { mTown = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Region { get { return (mRegion == "$" ? "" : ParserIfc.Decode(mRegion)); } set { mRegion = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string PostalCode { get { return (mPostalCode == "$" ? "" : ParserIfc.Decode(mPostalCode)); } set { mPostalCode = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Country { get { return (mCountry == "$" ? "" : ParserIfc.Decode(mCountry)); } set { mCountry = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		internal IfcPostalAddress() : base() { }
		internal IfcPostalAddress(IfcPostalAddress o)
			: base(o)
		{
			mInternalLocation = o.mInternalLocation; mAddressLines = o.mAddressLines; mPostalBox = o.mPostalBox;
			mTown = o.mTown; mRegion = o.mRegion; mPostalCode = o.mPostalCode; mCountry = o.mCountry;
		}
		public IfcPostalAddress(DatabaseIfc m, IfcAddressTypeEnum purpose) : base(m, purpose) { }

		internal static void parseFields(IfcPostalAddress a, List<string> arrFields, ref int ipos)
		{
			IfcAddress.parseFields(a, arrFields, ref ipos);
			a.mInternalLocation = arrFields[ipos++].Replace("'", "");
			if (string.IsNullOrEmpty(a.mInternalLocation))
				a.mInternalLocation = "$";
			string str = arrFields[ipos++];
			if (str != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(str);
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
		protected override string BuildString()
		{
			string str = base.BuildString() + (mInternalLocation == "$" ? ",$," : ",'" + mInternalLocation + "',");
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
	public abstract class IfcPreDefinedColour : IfcPreDefinedItem, IfcColour //	ABSTRACT SUPERTYPE OF(IfcDraughtingPreDefinedColour)
	{
		protected IfcPreDefinedColour() : base() { }
		protected IfcPreDefinedColour(IfcPreDefinedColour c) : base(c) { }
		protected static void parseFields(IfcPreDefinedColour c, List<string> arrFields, ref int ipos) { IfcPreDefinedItem.parseFields(c, arrFields, ref ipos); }
		public Color Colour { get { return Color.Empty; } }
	}
	public abstract class IfcPreDefinedCurveFont : IfcPreDefinedItem, IfcCurveStyleFontSelect
	{
		protected IfcPreDefinedCurveFont() : base() { }
		protected IfcPreDefinedCurveFont(IfcPreDefinedCurveFont f) : base(f) { }
		protected static void parseFields(IfcPreDefinedCurveFont f, List<string> arrFields, ref int ipos) { IfcPreDefinedItem.parseFields(f, arrFields, ref ipos); }
	}
	public class IfcPreDefinedDimensionSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
	{
		internal IfcPreDefinedDimensionSymbol() : base() { }
		internal IfcPreDefinedDimensionSymbol(IfcPreDefinedDimensionSymbol s) : base(s) { }
		internal static IfcPreDefinedDimensionSymbol Parse(string strDef) { IfcPreDefinedDimensionSymbol s = new IfcPreDefinedDimensionSymbol(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcPreDefinedDimensionSymbol s, List<string> arrFields, ref int ipos) { IfcPreDefinedSymbol.parseFields(s, arrFields, ref ipos); }
	}
	public abstract class IfcPreDefinedItem : IfcPresentationItem
	{
		internal string mName = "";//: IfcLabel; 
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } } 

		protected IfcPreDefinedItem() : base() { }
		protected IfcPreDefinedItem(IfcPreDefinedItem i) : base() { mName = i.mName; }
		protected static void parseFields(IfcPreDefinedItem i, List<string> arrFields, ref int ipos) { i.mName = arrFields[ipos++]; }
		protected override string BuildString() { return base.BuildString() + "," + mName; }
	}
	public class IfcPreDefinedPointMarkerSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
	{
		internal IfcPreDefinedPointMarkerSymbol() : base() { }
		internal IfcPreDefinedPointMarkerSymbol(IfcPreDefinedPointMarkerSymbol s) : base(s) { }
		internal static IfcPreDefinedPointMarkerSymbol Parse(string strDef) { IfcPreDefinedPointMarkerSymbol s = new IfcPreDefinedPointMarkerSymbol(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcPreDefinedPointMarkerSymbol s, List<string> arrFields, ref int ipos) { IfcPreDefinedSymbol.parseFields(s, arrFields, ref ipos); }
	}
	public abstract class IfcPreDefinedProperties : IfcPropertyAbstraction // IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcementBarProperties, IfcSectionProperties, IfcSectionReinforcementProperties))
	{
		protected IfcPreDefinedProperties() : base() { }
		protected IfcPreDefinedProperties(IfcPreDefinedProperties i) : base(i) { }
		protected IfcPreDefinedProperties(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcPreDefinedProperties p, List<string> arrFields, ref int ipos) { IfcPropertyAbstraction.parseFields(p, arrFields, ref ipos); }
	}
	public abstract class IfcPreDefinedPropertySet : IfcPropertySetDefinition // IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcDoorLiningProperties,  
	{ //IfcDoorPanelProperties, IfcPermeableCoveringProperties, IfcReinforcementDefinitionProperties, IfcWindowLiningProperties, IfcWindowPanelProperties))
		protected IfcPreDefinedPropertySet() : base() { }
		protected IfcPreDefinedPropertySet(IfcPreDefinedPropertySet el) : base(el) { }
		protected IfcPreDefinedPropertySet(DatabaseIfc m, string name) : base(m, name) { }
		protected static void parseFields(IfcPreDefinedPropertySet s, List<string> arrFields, ref int ipos) { IfcPropertySetDefinition.parseFields(s, arrFields, ref ipos); }
	}
	public abstract class IfcPreDefinedSymbol : IfcPreDefinedItem // DEPRECEATED IFC4
	{
		protected IfcPreDefinedSymbol() : base() { }
		protected IfcPreDefinedSymbol(IfcPreDefinedSymbol el) : base(el) { }
		protected static void parseFields(IfcPreDefinedSymbol s, List<string> arrFields, ref int ipos) { IfcPreDefinedItem.parseFields(s, arrFields, ref ipos); }
	}
	public class IfcPreDefinedTerminatorSymbol : IfcPreDefinedSymbol // DEPRECEATED IFC4
	{
		internal IfcPreDefinedTerminatorSymbol() : base() { }
		internal IfcPreDefinedTerminatorSymbol(IfcPreDefinedTerminatorSymbol i) : base(i) { }
		internal static IfcPreDefinedTerminatorSymbol Parse(string strDef) { IfcPreDefinedTerminatorSymbol s = new IfcPreDefinedTerminatorSymbol(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcPreDefinedTerminatorSymbol s, List<string> arrFields, ref int ipos) { IfcPreDefinedSymbol.parseFields(s, arrFields, ref ipos); }
	}
	public abstract class IfcPreDefinedTextFont : IfcPreDefinedItem
	{
		protected IfcPreDefinedTextFont() : base() { }
		protected IfcPreDefinedTextFont(IfcPreDefinedTextFont el) : base(el) { }
		protected static void parseFields(IfcPreDefinedTextFont f, List<string> arrFields, ref int ipos) { IfcPreDefinedItem.parseFields(f, arrFields, ref ipos); }
	}
	public abstract class IfcPresentationItem : BaseClassIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcColourRgbList, IfcColourSpecification,
	{ // IfcCurveStyleFont, IfcCurveStyleFontAndScaling, IfcCurveStyleFontPattern, IfcIndexedColourMap, IfcPreDefinedItem, IfcSurfaceStyleLighting, IfcSurfaceStyleRefraction, IfcSurfaceStyleShading, IfcSurfaceStyleWithTextures, IfcSurfaceTexture, IfcTextStyleForDefinedFont, IfcTextStyleTextModel, IfcTextureCoordinate, IfcTextureVertex, IfcTextureVertexList));
		protected IfcPresentationItem() : base() { }
		protected IfcPresentationItem(IfcPresentationItem i) : base() { }
		protected IfcPresentationItem(DatabaseIfc m) : base(m) { }
		protected virtual void parseFields(List<string> arrFields, ref int ipos) { }
	}
	public partial class IfcPresentationLayerAssignment : BaseClassIfc //SUPERTYPE OF	(IfcPresentationLayerWithStyle);
	{
		private string mName = "$";// : IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal List<int> mAssignedItems = new List<int>();// : SET [1:?] OF IfcLayeredItem; 
		internal string mIdentifier = "$";// : OPTIONAL IfcIdentifier; 

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "Default Layer" : mName = ParserIfc.Encode(value.Replace("'", ""))); } }
		public List<IfcLayeredItem> AssignedItems { get { return mAssignedItems.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcLayeredItem); } }

		internal IfcPresentationLayerAssignment() : base() { }
		internal IfcPresentationLayerAssignment(IfcPresentationLayerAssignment o) : base() { mName = o.mName; mDescription = o.mDescription; mAssignedItems = new List<int>(o.mAssignedItems.ToArray()); mIdentifier = o.mIdentifier; }
		internal IfcPresentationLayerAssignment(DatabaseIfc db, string name, string desc, string identifier) : base(db)
		{
			Name = name;
			if (!string.IsNullOrEmpty(desc))
				mDescription = desc.Replace("'", "");
			if (!string.IsNullOrEmpty(identifier))
				mIdentifier = identifier.Replace("'", "");
		}
		internal IfcPresentationLayerAssignment(string name, string desc, List<IfcLayeredItem> items, string identifier) : this(items[0].Database,name,desc,identifier)
		{
			if (items != null)
				mAssignedItems = items.ConvertAll(x => x.Index);
		}
		internal static IfcPresentationLayerAssignment Parse(string str)
		{
			IfcPresentationLayerAssignment a = new IfcPresentationLayerAssignment();
			int pos = 0;
			parseString(a, str, ref pos);
			return a;
		}
		protected static void parseString(IfcPresentationLayerAssignment a, string str, ref int pos)
		{
			a.mName = ParserSTEP.StripString(str, ref pos);
			a.mDescription = ParserSTEP.StripString(str, ref pos);
			a.mAssignedItems = ParserSTEP.StripListLink(str, ref pos);
			a.mIdentifier = ParserSTEP.StripString(str, ref pos);
		}
		protected override string BuildString()
		{
			if (mAssignedItems.Count < 1 || mDatabase.mOutputEssential)
				return "";
			string str = base.BuildString() + ",'" + mName + (mDescription == "$" ? "',$,(" : "'," + mDescription + ",(") + ParserSTEP.LinkToString(mAssignedItems[0]);
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
		internal void addItem(IfcRepresentation rep) { mAssignedItems.Add(rep.mIndex); }
		internal void relate()
		{
			List<IfcLayeredItem> items = AssignedItems;
			for (int icounter = 0; icounter < items.Count; icounter++)
				items[icounter].LayerAssignments.Add(this);
		}
	}
	public partial class IfcPresentationLayerWithStyle : IfcPresentationLayerAssignment
	{
		internal IfcLogicalEnum mLayerOn = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		internal IfcLogicalEnum mLayerFrozen = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		internal IfcLogicalEnum mLayerBlocked = IfcLogicalEnum.UNKNOWN;// LOGICAL;
		internal List<int> mLayerStyles = new List<int>();// SET OF IfcPresentationStyleSelect; IFC4 IfcPresentationStyle
		internal List<IfcPresentationStyleSelect> LayerStyles { get { return mLayerStyles.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcPresentationStyleSelect); } }
		internal IfcPresentationLayerWithStyle() : base() { }
		internal IfcPresentationLayerWithStyle(IfcPresentationLayerWithStyle o) : base(o) { mLayerOn = o.mLayerOn; mLayerFrozen = o.mLayerFrozen; mLayerBlocked = o.mLayerBlocked; mLayerStyles = new List<int>(o.mLayerStyles.ToArray()); }
		internal IfcPresentationLayerWithStyle(DatabaseIfc db, string name, string desc, string identifier, bool on, bool frozen, bool blocked, List<IfcPresentationStyle> styles )
			: base(db,name, desc, identifier) { mLayerOn = (on ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); mLayerFrozen = (frozen ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); mLayerBlocked = (blocked ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); if (styles != null && styles.Count > 0) mLayerStyles = styles.ConvertAll(x => x.Index); }
		internal IfcPresentationLayerWithStyle(string name, string desc, List<IfcLayeredItem> items, string identifier, bool on, bool frozen, bool blocked, List<IfcPresentationStyle> styles )
			: base(name, desc, items, identifier) { mLayerOn = (on ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); mLayerFrozen = (frozen ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); mLayerBlocked = (blocked ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); if (styles != null && styles.Count > 0) mLayerStyles = styles.ConvertAll(x => x.Index); }
		internal new static IfcPresentationLayerWithStyle Parse(string str)
		{
			IfcPresentationLayerWithStyle s = new IfcPresentationLayerWithStyle();
			int pos = 0;
			IfcPresentationLayerAssignment.parseString(s, str, ref pos);
			s.mLayerOn = ParserIfc.StripLogical(str, ref pos);
			s.mLayerFrozen = ParserIfc.StripLogical(str, ref pos);
			s.mLayerBlocked = ParserIfc.StripLogical(str, ref pos);
			s.mLayerStyles = ParserSTEP.StripListLink(str, ref pos);
			return s;
		}
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential || mAssignedItems.Count < 1 || mLayerStyles.Count == 0)
				return "";
			string str = base.BuildString() + "," + ParserIfc.LogicalToString(mLayerOn) + "," +
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

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public List<IfcStyledItem> StyledItems { get { return mStyledItems; } }

		protected IfcPresentationStyle() : base() { }
		protected IfcPresentationStyle(IfcPresentationStyle i) : base() { mName = i.mName; }
		protected IfcPresentationStyle(DatabaseIfc m, string name) : base(m) { Name = name; }
		protected static void parseFields(IfcPresentationStyle s, List<string> arrFields, ref int ipos) { s.mName = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + (mName == "$" ? ",$" : ",'" + mName + "'"); }
	}
	public partial class IfcPresentationStyleAssignment : BaseClassIfc, IfcStyleAssignmentSelect //DEPRECEATED IFC4
	{
		internal List<int> mStyles = new List<int>();// : SET [1:?] OF IfcPresentationStyleSelect; 

		internal List<IfcPresentationStyleSelect> Styles { get { return mStyles.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcPresentationStyleSelect); } }
		//INVERSE
		internal List<IfcStyledItem> mStyledItems = new List<IfcStyledItem>();
		public List<IfcStyledItem> StyledItems { get { return mStyledItems; } }

		internal IfcPresentationStyleAssignment() : base() { }
		internal IfcPresentationStyleAssignment(IfcPresentationStyleAssignment o) : base() { mStyles = new List<int>(o.mStyles.ToArray()); }
		internal IfcPresentationStyleAssignment(IfcPresentationStyle style) : base(style.mDatabase) { mStyles.Add(style.Index); }
		internal IfcPresentationStyleAssignment(List<IfcPresentationStyle> styles) : base(styles[0].mDatabase) { mStyles = styles.ConvertAll(x => x.Index); }
		internal static IfcPresentationStyleAssignment Parse(string strDef) { IfcPresentationStyleAssignment s = new IfcPresentationStyleAssignment(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcPresentationStyleAssignment s, List<string> arrFields, ref int ipos) { s.mStyles = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential)
				return "";
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mStyles[0]);
			for (int icounter = 1; icounter < mStyles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mStyles[icounter]);
			return str + ")";
		}
	}
	public interface IfcPresentationStyleSelect : IfcInterface { } //DEPRECEATED IFC4 TYPE  = SELECT(IfcNullStyle, IfcCurveStyle, IfcSymbolStyle, IfcFillAreaStyle, IfcTextStyle, IfcSurfaceStyle);
	public interface IfcSpecularHighlightSelect { } //SELECT ( IfcSpecularExponent, IfcSpecularRoughness);
	public class IfcProcedure : IfcProcess
	{
		internal string mProcedureID;// : IfcIdentifier;
		internal IfcProcedureTypeEnum mProcedureType;// : IfcProcedureTypeEnum;
		internal string mUserDefinedProcedureType = "$";// : OPTIONAL IfcLabel;
		internal IfcProcedure() : base() { }
		internal IfcProcedure(IfcProcedure o) : base(o) { mProcedureID = o.mProcedureID; mProcedureType = o.mProcedureType; mUserDefinedProcedureType = o.mUserDefinedProcedureType; }
		internal static IfcProcedure Parse(string strDef) { IfcProcedure p = new IfcProcedure(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcProcedure p, List<string> arrFields, ref int ipos) { IfcProcess.parseFields(p, arrFields, ref ipos); p.mProcedureID = arrFields[ipos++]; p.mProcedureType = (IfcProcedureTypeEnum)Enum.Parse(typeof(IfcProcedureTypeEnum), arrFields[ipos++].Replace(".", "")); p.mUserDefinedProcedureType = arrFields[ipos++]; }
		protected override string BuildString() { return base.BuildString() + "," + mProcedureID + ",." + mProcedureType.ToString() + ".," + mUserDefinedProcedureType; }
	}
	public class IfcProcedureType : IfcTypeProcess //IFC4
	{
		internal IfcProcedureTypeEnum mPredefinedType = IfcProcedureTypeEnum.NOTDEFINED;// : IfcProcedureTypeEnum; 
		public IfcProcedureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcProcedureType() : base() { }
		internal IfcProcedureType(IfcProcedureType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcProcedureType(DatabaseIfc m, string name, IfcProcedureTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcProcedureType t, List<string> arrFields, ref int ipos) { IfcTypeProcess.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcProcedureTypeEnum)Enum.Parse(typeof(IfcProcedureTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcProcedureType Parse(string strDef) { IfcProcedureType t = new IfcProcedureType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString() + ",." + mPredefinedType.ToString() + "."); }
	}
	public abstract partial class IfcProcess : IfcObject // ABSTRACT SUPERTYPE OF (ONEOF (IfcProcedure ,IfcTask))
	{
		internal string mIdentification = "$";// :OPTIONAL IfcIdentifier;
		internal string mLongDescription = "$";//: OPTIONAL IfcText; 
		//INVERSE
		internal List<IfcRelSequence> mIsSuccessorFrom = new List<IfcRelSequence>();// : SET [0:?] OF IfcRelSequence FOR RelatedProcess;
		internal List<IfcRelSequence> mIsPredecessorTo = new List<IfcRelSequence>();// : SET [0:?] OF IfcRelSequence FOR RelatingProcess; 
		internal List<IfcRelAssignsToProcess> mOperatesOn = new List<IfcRelAssignsToProcess>();// : SET [0:?] OF IfcRelAssignsToProcess FOR RelatingProcess;

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string LongDescription { get { return (mLongDescription == "$" ? "" : ParserIfc.Decode(mLongDescription)); } set { mLongDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcProcess() : base() { }
		protected IfcProcess(IfcProcess o) : base(o) { }
		protected IfcProcess(DatabaseIfc m) : base(m)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.If2x3NotAssigned)
				throw new Exception("Invalid Model View for IfcProcess : " + m.ModelView.ToString());
		}
		
		protected static void parseFields(IfcProcess p, List<string> arrFields, ref int ipos,Schema schema)
		{
			IfcObject.parseFields(p, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
			{
				p.mIdentification = arrFields[ipos++].Replace("'", "");
				p.mLongDescription = arrFields[ipos++].Replace("'", "");
			}
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema != Schema.IFC2x3 ? (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',") + (mLongDescription == "$" ? "$" : "'" + mLongDescription + "'") : ""); }
	}
	public abstract partial class IfcProduct : IfcObject, IfcProductSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcAnnotation ,IfcElement ,IfcGrid ,IfcPort ,IfcProxy ,IfcSpatialElement ,IfcStructuralActivity ,IfcStructuralItem))
	{
		private int mPlacement = 0; //: OPTIONAL IfcObjectPlacement;
		private int mRepresentation = 0; //: OPTIONAL IfcProductRepresentation 
		//INVERSE
		internal List<IfcRelAssignsToProduct> mReferencedBy = new List<IfcRelAssignsToProduct>();//	 :	SET OF IfcRelAssignsToProduct FOR RelatingProduct;
		//internal List<IfcRelContainedInSpatialStructure> mContainedInStructure = new List<IfcRelContainedInSpatialStructure>(); //ERR IFC4 change to Element
		public IfcObjectPlacement Placement
		{
			get { return (mPlacement == 0 ? null : (IfcObjectPlacement)mDatabase.mIfcObjects[mPlacement]); }
			set 
			{
				if (value == null)
					mPlacement = 0;
				else
				{
					mPlacement = value.mIndex;
					value.mPlacesObject.Add(this);
				}
			}
		}
		public IfcProductRepresentation Representation
		{
			get { return mDatabase.mIfcObjects[mRepresentation] as IfcProductRepresentation; }
			set
			{
				if(value == null)
					mRepresentation = 0;
				else
				{
					mRepresentation = value.mIndex;
					IfcProductDefinitionShape pds = value as IfcProductDefinitionShape;
					
					if (pds != null && mPlacement == 0)
					{
						IfcElement element = this as IfcElement;
						if (element == null)
							Placement = new IfcLocalPlacement(new IfcAxis2Placement3D(mDatabase));
						else
						{
							IfcProduct product = element.getContainer();
							Placement = (product == null ? new IfcLocalPlacement(new IfcAxis2Placement3D(mDatabase))  : new IfcLocalPlacement(product.Placement, new IfcAxis2Placement3D(mDatabase)));
						}
					}
				}
			}
		}
		public List<IfcRelAssignsToProduct> ReferencedBy { get { return mReferencedBy; } }

		internal IfcObjectPlacement mContainerCommonPlacement = null; //GeometryGym common Placement reference for aggregated items

		protected IfcProduct() : base() { }
		protected IfcProduct(IfcProduct o) : base(o) { mPlacement = o.mPlacement; mRepresentation = o.mRepresentation; }
		protected IfcProduct(IfcProductRepresentation rep) : base(rep.mDatabase) { mRepresentation = rep.mIndex; }
		protected IfcProduct(IfcObjectPlacement placement) : base(placement.mDatabase) { mPlacement = placement.mIndex; }
		protected IfcProduct(IfcObjectPlacement placement, IfcProductRepresentation rep) : base(rep.mDatabase) { mPlacement = placement.mIndex; mRepresentation = rep.mIndex; }
		protected IfcProduct(DatabaseIfc db) : base(db) { }
		protected IfcProduct(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host.mDatabase) 
		{
			IfcElement el = this as IfcElement;
			if (el != null)
				host.AddElement(el);
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

		protected static void parseFields(IfcProduct p, List<string> arrFields, ref int ipos) { IfcObject.parseFields(p, arrFields, ref ipos); p.mPlacement = ParserSTEP.ParseLink(arrFields[ipos++]); p.mRepresentation = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mPlacement) + "," + ParserSTEP.LinkToString((mDatabase.mOutputEssential ? 0 : mRepresentation)); }

		public virtual bool AddElement(IfcElement s)
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
		internal void relate()
		{
			if (mPlacement > 0)
				Placement.mPlacesObject.Add(this);
			if (mRepresentation > 0)
			{
				IfcProductDefinitionShape pds = Representation as IfcProductDefinitionShape;
				if (pds != null)
					pds.mShapeOfProduct.Add(this);
			}
		}
	}
	//ENTITY IfcProductsOfCombustionProperties	 // DEPRECEATED IFC4
	public partial class IfcProductDefinitionShape : IfcProductRepresentation, IfcProductRepresentationSelect
	{
		//INVERSE
		internal List<IfcProduct> mShapeOfProduct = new List<IfcProduct>();
		internal List<IfcShapeAspect> mHasShapeAspects = new List<IfcShapeAspect>();

		public List<IfcShapeAspect> HasShapeAspects { get { return mHasShapeAspects; } }

		public new List<IfcShapeModel> Representations
		{
			get { return base.Representations.ConvertAll(x => (IfcShapeModel)x); }
			set { base.Representations = value.ConvertAll(x => x as IfcRepresentation); }
		}

		internal IfcProductDefinitionShape() : base() { }
		internal IfcProductDefinitionShape(IfcProductDefinitionShape i) : base(i) { }
		public IfcProductDefinitionShape(IfcShapeModel rep) : base(rep) { }
		public IfcProductDefinitionShape(List<IfcShapeModel> reps) : base(reps.ConvertAll(x => x as IfcRepresentation)) { }
		internal new static IfcProductDefinitionShape Parse(string strDef) { IfcProductDefinitionShape s = new IfcProductDefinitionShape(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcProductDefinitionShape s, List<string> arrFields, ref int ipos) { IfcProductRepresentation.parseFields(s, arrFields, ref ipos); }
		protected override string BuildString() { if (mDatabase.mOutputEssential) return ""; else return base.BuildString(); }
	}
	public partial class IfcProductRepresentation : BaseClassIfc //(IfcMaterialDefinitionRepresentation ,IfcProductDefinitionShape));
	{
		private string mName = "$";// : OPTIONAL IfcLabel;
		private string mDescription = "$";// : OPTIONAL IfcText;
		internal List<int> mRepresentations = new List<int>();// : LIST [1:?] OF IfcRepresentation; 

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public List<IfcRepresentation> Representations
		{
			get { return mRepresentations.ConvertAll(x => (IfcRepresentation)mDatabase.mIfcObjects[x]); }
			set { mRepresentations = value.ConvertAll(x => x.mIndex); }
		}

		public IfcRepresentation Representation { set { mRepresentations = new List<int>() { value.mIndex }; } }

		internal IfcProductRepresentation() : base() { }
		internal IfcProductRepresentation(IfcProductRepresentation i) : base() { mName = i.mName; mDescription = i.mDescription; mRepresentations.AddRange(i.mRepresentations.ToArray()); }
		public IfcProductRepresentation(IfcRepresentation r) : base(r.mDatabase) { mRepresentations.Add(r.mIndex); }
		public IfcProductRepresentation(List<IfcRepresentation> reps) : base(reps[0].mDatabase) { Representations = reps; }

		internal static IfcProductRepresentation Parse(string strDef) { IfcProductRepresentation r = new IfcProductRepresentation(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
		internal static void parseFields(IfcProductRepresentation r, List<string> arrFields, ref int ipos) { r.mName = arrFields[ipos++].Replace("'", ""); r.mDescription = arrFields[ipos++].Replace("'", ""); r.mRepresentations = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = base.BuildString() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,(" : "'" + mDescription + "',(");
			if (mRepresentations.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRepresentations[0]);
				for (int icounter = 1; icounter < mRepresentations.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRepresentations[icounter]);
			}
			return str + ")";
		}

		internal virtual void relate()
		{
			List<IfcRepresentation> reps = Representations;
			foreach (IfcRepresentation r in reps)
			{
				if (r != null)
					r.OfProductRepresentation.Add(this);
			}
		}
	}
	public interface IfcProductRepresentationSelect : IfcInterface { List<IfcShapeAspect> HasShapeAspects { get; } }// 	IfcProductDefinitionShape,	IfcRepresentationMap);
	public interface IfcProductSelect : IfcInterface { List<IfcRelAssignsToProduct> ReferencedBy { get; } string GlobalId { get; }  } //	IfcProduct, IfcTypeProduct);
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
		public string ProfileName { get { return mProfileName == "$" ? "" : ParserIfc.Decode(mProfileName); } set { mProfileName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public override string Name { get { return ProfileName; } set { ProfileName = value; } }
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }

		protected IfcProfileDef() : base() { }
		protected IfcProfileDef(IfcProfileDef i) : base(i) { mProfileType = i.mProfileType; mProfileName = i.mProfileName; }
		protected IfcProfileDef(DatabaseIfc m) : base(m)
		{
			if (mDatabase.mSchema == Schema.IFC2x3)
				mHasProperties.Add(new IfcGeneralProfileProperties(mProfileName, this));
		}
		internal static IfcProfileDef Parse(string strDef) { IfcProfileDef p = new IfcProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected static void parseFields(IfcProfileDef p, List<string> arrFields, ref int ipos)
		{
			string str = arrFields[ipos++];
			if (str.StartsWith("."))
				p.mProfileType = (IfcProfileTypeEnum)Enum.Parse(typeof(IfcProfileTypeEnum), str.Replace(".", ""));
			p.mProfileName = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildString() { return base.BuildString() + ",." + mProfileType.ToString() + (mProfileName == "$" ? ".,$" : ".,'" + mProfileName + "'"); }
	}
	public partial class IfcProfileProperties : IfcExtendedProperties //IFC2x3 Abstract : BaseClassIfc ABSTRACT SUPERTYPE OF	(ONEOF(IfcGeneralProfileProperties, IfcRibPlateProfileProperties));
	{
		//internal string mProfileName = "$";// : OPTIONAL IfcLabel; DELETED IFC4
		private int mProfileDefinition;// : OPTIONAL IfcProfileDef; 

		internal IfcProfileDef ProfileDefinition { get { return mDatabase.mIfcObjects[mProfileDefinition] as IfcProfileDef; } set { mProfileDefinition = value == null ? 0 : value.mIndex; } }

		internal IfcRelAssociatesProfileProperties mAssociates = null; //GeometryGym attribute

		internal IfcProfileProperties() : base() { }
		internal IfcProfileProperties(IfcProfileProperties p) : base() { mProfileDefinition = p.mProfileDefinition; }
		internal IfcProfileProperties(string name, IfcProfileDef p)
			: base(p.mDatabase)
		{
			Name = name;
			mProfileDefinition = p.mIndex;
			if (p.mDatabase.mSchema == Schema.IFC2x3)
				mAssociates = new IfcRelAssociatesProfileProperties(this) { Name = p.ProfileName };
		}
		internal IfcProfileProperties(string name, List<IfcProperty> props, IfcProfileDef p)
			: base(name, props)
		{
			mProfileDefinition = p.mIndex;
			if (p.mDatabase.mSchema == Schema.IFC2x3)
				mAssociates = new IfcRelAssociatesProfileProperties(this) { Name = p.ProfileName };
		}
		internal static IfcProfileProperties Parse(string strDef, Schema schema) { IfcProfileProperties p = new IfcProfileProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return p; }
		internal static void parseFields(IfcProfileProperties p, List<string> arrFields, ref int ipos, Schema schema)
		{
			if (schema == Schema.IFC2x3)
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
		protected override string BuildString() { return (ProfileDefinition == null ? "" : base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? (mName == "$" ? ",$," : ",'" + mName + "',") : ",") + ParserSTEP.LinkToString(mProfileDefinition)); }

		internal static IfcPropertySet genPSetMechanical(DatabaseIfc m, double massPerLength, double crossSectionArea, double perimeter, double minPlThck, double maxPlThck, double cogX, double cogY, double shearZ, double momIntertiaY, double momIntertiaZ, double momIntertiaYZ,
			double torsionalConst, double warpingConst, double shearDeformAreaZ, double shearDeformAreaY, double maxSectModY, double minSectModY, double maxSectModZ, double minSectModZ, double torsSectMod, double shearAreaZ, double shearAreaY, double plasticFactorY, double plasticFactorZ)
		{
			List<IfcProperty> props = new List<IfcProperty>();
			if (massPerLength > 0)
				props.Add(new IfcPropertySingleValue(m, "MassPerLength", "", new IfcMassPerLengthMeasure(massPerLength)));
			if (crossSectionArea > 0)
				props.Add(new IfcPropertySingleValue(m, "CrossSectionArea", "", new IfcAreaMeasure(crossSectionArea)));
			if (perimeter > 0)
				props.Add(new IfcPropertySingleValue(m, "Perimeter", "", new IfcPositiveLengthMeasure(perimeter)));
			if (minPlThck > 0)
				props.Add(new IfcPropertySingleValue(m, "MinimumPlateThickness", "", new IfcPositiveLengthMeasure(minPlThck)));
			if (maxPlThck > 0)
				props.Add(new IfcPropertySingleValue(m, "MaximumPlateThickness", "", new IfcPositiveLengthMeasure(maxPlThck)));
			props.Add(new IfcPropertySingleValue(m, "CentreOfGravityInX", "", new IfcLengthMeasure(cogX)));
			props.Add(new IfcPropertySingleValue(m, "CentreOfGravityInY", "", new IfcLengthMeasure(cogY)));
			props.Add(new IfcPropertySingleValue(m, "ShearCentreZ", "", new IfcLengthMeasure(cogX)));
			props.Add(new IfcPropertySingleValue(m, "ShearCentreY", "", new IfcLengthMeasure(cogY)));
			if (momIntertiaY > 0)
				props.Add(new IfcPropertySingleValue(m, "MomentOfInertiaY", "", new IfcMomentOfInertiaMeasure(momIntertiaY)));
			if (momIntertiaZ > 0)
				props.Add(new IfcPropertySingleValue(m, "MomentOfInertiaZ", "", new IfcMomentOfInertiaMeasure(momIntertiaZ)));
			if (momIntertiaYZ > 0)
				props.Add(new IfcPropertySingleValue(m, "MomentOfInertiaYZ", "", new IfcMomentOfInertiaMeasure(momIntertiaYZ)));
			if (torsionalConst > 0)
				props.Add(new IfcPropertySingleValue(m, "TorsionalConstantX", "", new IfcMomentOfInertiaMeasure(torsionalConst)));
			if (warpingConst > 0)
				props.Add(new IfcPropertySingleValue(m, "WarpingConstant", "", new IfcWarpingConstantMeasure(warpingConst)));
			if (shearDeformAreaZ > 0)
				props.Add(new IfcPropertySingleValue(m, "ShearDeformationAreaZ", "", new IfcAreaMeasure(shearDeformAreaZ)));
			if (shearDeformAreaY > 0)
				props.Add(new IfcPropertySingleValue(m, "ShearDeformationAreaY", "", new IfcAreaMeasure(shearDeformAreaY)));
			if (maxSectModY > 0)
				props.Add(new IfcPropertySingleValue(m, "MaximumSectionModulusY", "", new IfcSectionModulusMeasure(maxSectModY)));
			if (minSectModY > 0)
				props.Add(new IfcPropertySingleValue(m, "MinimumSectionModulusY", "", new IfcSectionModulusMeasure(minSectModY)));
			if (maxSectModZ > 0)
				props.Add(new IfcPropertySingleValue(m, "MaximumSectionModulusZ", "", new IfcSectionModulusMeasure(maxSectModZ)));
			if (minSectModZ > 0)
				props.Add(new IfcPropertySingleValue(m, "MinimumSectionModulusZ", "", new IfcSectionModulusMeasure(minSectModZ)));
			if (torsSectMod > 0)
				props.Add(new IfcPropertySingleValue(m, "TorsionalSectionModulus", "", new IfcSectionModulusMeasure(torsSectMod)));
			if (shearAreaZ > 0)
				props.Add(new IfcPropertySingleValue(m, "ShearAreaZ", "", new IfcAreaMeasure(shearAreaZ)));
			if (shearAreaY > 0)
				props.Add(new IfcPropertySingleValue(m, "ShearAreaY", "", new IfcAreaMeasure(shearAreaY), null));
			if (plasticFactorY > 0)
				props.Add(new IfcPropertySingleValue(m, "PlasticShapeFactorY", "", new IfcPositiveRatioMeasure(plasticFactorY)));
			if (plasticFactorZ > 0)
				props.Add(new IfcPropertySingleValue(m, "PlasticShapeFactorZ", "", new IfcPositiveRatioMeasure(plasticFactorZ)));
			return new IfcPropertySet("Pset_ProfileMechanical", props);
		}
	}
	public partial class IfcProject : IfcContext
	{
		internal IfcProject() : base() { }
		internal IfcProject(IfcProject o) : base(o) { }

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
				BaseClassIfc ent = mDatabase.mIfcObjects[mIsDecomposedBy[0].mRelatedObjects[0]];
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
	public class IfcProjectedCRS : IfcCoordinateReferenceSystem //IFC4
	{
		internal string mMapProjection = "$";// :	OPTIONAL IfcIdentifier;
		internal string mMapZone = "$";// : OPTIONAL IfcIdentifier 
		internal IfcNamedUnit mMapUnit = null;// :	OPTIONAL IfcNamedUnit;
		internal IfcProjectedCRS() : base() { }
		internal IfcProjectedCRS(IfcProjectedCRS m) : base(m) { mName = m.mName; mMapZone = m.mMapZone; mMapUnit = m.mMapUnit; }
		internal IfcProjectedCRS(DatabaseIfc m, string name, string desc, string geodeticDatum, string verticalDatum, string mapProjection, string mapZone, IfcLengthMeasure optMeasure)
			: base(m, name, desc, geodeticDatum, verticalDatum)
		{
			if (mapProjection != "")
				mMapProjection = mapProjection.Replace("'", "");
			if (desc != "")
				mMapZone = desc.Replace("'", "");
		}
		internal static IfcProjectedCRS Parse(string strDef) { IfcProjectedCRS m = new IfcProjectedCRS(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcProjectedCRS m, List<string> arrFields, ref int ipos)
		{
			IfcCoordinateReferenceSystem.parseFields(m, arrFields, ref ipos);
			m.mName = arrFields[ipos++].Replace("'", "");
			m.mMapZone = arrFields[ipos++].Replace("'", "");
			//m.mMapUnit = IFCModel.mSTP.parseSTPLink(arrFields[ipos++]);
		}
		protected override string BuildString()
		{
			return base.BuildString() + (mMapProjection == "$" ? ",$," : ",'" + mMapProjection + "',") +
				(mMapZone == "$" ? "$," : "'" + mMapZone + "',") +
				(mMapUnit == null ? "$" : mMapUnit.ToString());
		}
	}
	//ENTITY IfcProjectionCurve // DEPRECEATED IFC4
	public partial class IfcProjectionElement : IfcFeatureElementAddition
	{
		internal IfcProjectionElementTypeEnum mPredefinedType = IfcProjectionElementTypeEnum.NOTDEFINED;// :	OPTIONAL IfcProjectionElementTypeEnum; //IFC4
		//INVERSE
		internal IfcProjectionElement() : base() { }
		internal IfcProjectionElement(IfcProjectionElement od) : base(od) { mPredefinedType = od.mPredefinedType; }

		internal static IfcProjectionElement Parse(string strDef, Schema schema) { IfcProjectionElement e = new IfcProjectionElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return e; }
		internal static void parseFields(IfcProjectionElement e, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcFeatureElementAddition.parseFields(e, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
				e.mPredefinedType = (IfcProjectionElementTypeEnum)Enum.Parse(typeof(IfcProjectionElementTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcProjectionElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
	}
	public partial class IfcProjectLibrary : IfcContext
	{
		internal IfcProjectLibrary() : base() { }
		internal IfcProjectLibrary(IfcProjectLibrary o) : base(o) { }
		public IfcProjectLibrary(DatabaseIfc m, string name, IfcUnitAssignment.Length length)
			: base(m, name, length)
		{
			if (m.ModelView == ModelView.Ifc4Reference || m.ModelView == ModelView.Ifc2x3Coordination)
				throw new Exception("Invalid Model View for IfcProjectLibrary : " + m.ModelView.ToString());
			if (string.IsNullOrEmpty(Name))
				Name = "UNKNOWN PROJECTLIBRARY";
		}
		internal static IfcProjectLibrary Parse(string strDef) { IfcProjectLibrary p = new IfcProjectLibrary(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
	}
	public class IfcProjectOrder : IfcControl
	{
		//internal string mID;// : IfcIdentifier; IFC4 relocated 
		internal IfcProjectOrderTypeEnum mPredefinedType;// : IfcProjectOrderTypeEnum;
		internal string mStatus = "$";// : OPTIONAL IfcLabel; 
		internal string mLongDescription = "$"; //	 :	OPTIONAL IfcText;
		internal IfcProjectOrder() : base() { }
		internal IfcProjectOrder(IfcProjectOrder i) : base(i) { mPredefinedType = i.mPredefinedType; mStatus = i.mStatus; mLongDescription = i.mLongDescription; }
		internal static IfcProjectOrder Parse(string strDef,Schema schema) { IfcProjectOrder p = new IfcProjectOrder(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcProjectOrder p, List<string> arrFields, ref int ipos,Schema schema)
		{
			IfcControl.parseFields(p, arrFields, ref ipos,schema);
			if (schema == Schema.IFC2x3)
				p.mIdentification = arrFields[ipos++].Replace("'", "");
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				p.mPredefinedType = (IfcProjectOrderTypeEnum)Enum.Parse(typeof(IfcProjectOrderTypeEnum), s.Replace(".", ""));
			p.mStatus = arrFields[ipos++].Replace("'", "");
			if (schema != Schema.IFC2x3)
				p.mLongDescription = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? ",'" + mIdentification + "',." : ",.") + mPredefinedType.ToString() + (mStatus == "$" ? ".,$" : ".," + mStatus + "'") + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mLongDescription == "$" ? ",$" : ",'" + mLongDescription + "'")); }
	}
	public class IfcProjectOrderRecord : IfcControl // DEPRECEATED IFC4
	{
		internal List<int> mRecords = new List<int>();// : LIST [1:?] OF UNIQUE IfcRelAssignsToProjectOrder;
		internal IfcProjectOrderRecordTypeEnum mPredefinedType;// : IfcProjectOrderRecordTypeEnum; 
		internal IfcProjectOrderRecord() : base() { }
		internal IfcProjectOrderRecord(IfcProjectOrderRecord i) : base(i) { mRecords = new List<int>(i.mRecords.ToArray()); mPredefinedType = i.mPredefinedType; }
		internal static IfcProjectOrderRecord Parse(string strDef, Schema schema) { IfcProjectOrderRecord r = new IfcProjectOrderRecord(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return r; }
		internal static void parseFields(IfcProjectOrderRecord r, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcControl.parseFields(r, arrFields, ref ipos,schema);
			r.mRecords = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			r.mPredefinedType = (IfcProjectOrderRecordTypeEnum)Enum.Parse(typeof(IfcProjectOrderRecordTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mRecords[0]);
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

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { if (!string.IsNullOrEmpty(value)) mName = ParserIfc.Encode(value.Replace("'", "")); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcProperty(IfcProperty r) { mName = r.mName; mDescription = r.mDescription; }
		protected IfcProperty() : base() { }
		protected IfcProperty(DatabaseIfc db, string name, string desc) : base(db) { Name = name; Description = desc; }
		protected static void parseFields(IfcProperty p, List<string> arrFields, ref int ipos) { p.mName = arrFields[ipos++].Replace("'", ""); p.mDescription = arrFields[ipos++].Replace("'", ""); }
		protected override void parseString(string str, ref int pos)
		{
			mName = ParserSTEP.StripString(str, ref pos);
			mDescription = ParserSTEP.StripString(str, ref pos);
		}
		protected override string BuildString() { return base.BuildString() + ",'" + mName + (mDescription == "$" ? "',$" : "','" + mDescription + "'"); }
	}
	public abstract class IfcPropertyAbstraction : BaseClassIfc, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcExtendedProperties ,IfcPreDefinedProperties ,IfcProperty ,IfcPropertyEnumeration));
	{ //INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4 
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }
		protected IfcPropertyAbstraction(IfcPropertyAbstraction p) : base() { }
		protected IfcPropertyAbstraction() : base() { }
		protected IfcPropertyAbstraction(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcPropertyAbstraction p, List<string> arrFields, ref int ipos) { }
		protected virtual void parseString(string str, ref int pos) { }
	}
	public partial class IfcPropertyBoundedValue : IfcSimpleProperty
	{
		internal IfcValue mUpperBoundValue;// : OPTIONAL IfcValue;
		internal IfcValue mLowerBoundValue;// : OPTIONAL IfcValue;   
		internal int mUnit;// : OPTIONAL IfcUnit;
		internal IfcValue mSetPointValue;// 	OPTIONAL IfcValue; IFC4
		internal IfcPropertyBoundedValue(IfcPropertyBoundedValue q) : base(q) { mUpperBoundValue = q.mUpperBoundValue; mLowerBoundValue = q.mLowerBoundValue; mUnit = q.mUnit; mSetPointValue = q.mSetPointValue; }
		internal IfcPropertyBoundedValue() : base() { }
		internal IfcPropertyBoundedValue(DatabaseIfc m, string name, string desc, IfcValue upper, IfcValue lower, IfcNamedUnit unit, IfcValue set)
			: base(m, name, desc)
		{
			mUpperBoundValue = upper;
			mLowerBoundValue = lower;
			mSetPointValue = set;
			if (unit != null)
				mUnit = unit.mIndex;
		}
		internal static void parseFields(IfcPropertyBoundedValue p, List<string> arrFields, ref int ipos, Schema schema)
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
			if (schema != Schema.IFC2x3)
			{
				s = arrFields[ipos++];
				if (s != "$")
					p.mSetPointValue = ParserIfc.parseValue(arrFields[ipos++]);
			}
		}
		internal static IfcPropertyBoundedValue Parse(string strDef, Schema schema) { IfcPropertyBoundedValue p = new IfcPropertyBoundedValue(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		protected override string BuildString() { return base.BuildString() + (mUpperBoundValue == null ? ",$," : "," + mUpperBoundValue.ToString() + ",") + (mLowerBoundValue == null ? "$," : mLowerBoundValue.ToString() + ",") + ParserSTEP.LinkToString(mUnit) + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mSetPointValue == null ? ",$" : "," + mSetPointValue.ToString())); }
	}
	public abstract partial class IfcPropertyDefinition : IfcRoot, IfcDefinitionSelect //(IfcPropertySetDefinition, IfcPropertyTemplateDefinition)
	{ //INVERSE
		internal IfcRelDeclares mHasContext = null;// :	SET [0:1] OF IfcRelDeclares FOR RelatedDefinitions;
		internal List<IfcRelAssociates> mHasAssociations = new List<IfcRelAssociates>();//	 : 	SET OF IfcRelAssociates FOR RelatedObjects;
		public IfcRelDeclares HasContext { get { return mHasContext; } set { mHasContext = value; } }
		public List<IfcRelAssociates> HasAssociations { get { return mHasAssociations; } }

		DatabaseIfc IfcDefinitionSelect.Model { get { return mDatabase; } }

		protected IfcPropertyDefinition() : base() { }
		protected IfcPropertyDefinition(IfcPropertyDefinition i) : base(i) { }
		internal IfcPropertyDefinition(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcPropertyDefinition p, List<string> arrFields, ref int ipos) { IfcRoot.parseFields(p, arrFields, ref ipos); }
		internal virtual void Associate(IfcRelAssociates a) { mHasAssociations.Add(a); }
	}
	//ENTITY IfcPropertyDependencyRelationship;	
	public partial class IfcPropertyEnumeratedValue : IfcSimpleProperty
	{
		//internal List<string> mEnumerationValues = new List<string>();// : LIST [1:?] OF IfcValue;
		internal string mEnumerationValues = "";// : LIST [1:?] OF IfcValue; 
		internal int mEnumerationReference;// : OPTIONAL IfcPropertyEnumeration;   

		internal IfcPropertyEnumeratedValue(IfcPropertyEnumeratedValue q) : base(q) { mEnumerationValues = q.mEnumerationValues; /*mEnumerationValues = new List<string>( q.mEnumerationValues.ToArray());*/ mEnumerationReference = q.mEnumerationReference; }
		internal IfcPropertyEnumeratedValue() : base() { }
		internal static void parseFields(IfcPropertyEnumeratedValue p, List<string> arrFields, ref int ipos) { IfcSimpleProperty.parseFields(p, arrFields, ref ipos); string str = arrFields[ipos++]; p.mEnumerationValues = str; p.mEnumerationReference = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcPropertyEnumeratedValue Parse(string strDef) { IfcPropertyEnumeratedValue p = new IfcPropertyEnumeratedValue(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + "," + mEnumerationValues + "," + ParserSTEP.LinkToString(mEnumerationReference); }
	}
	public class IfcPropertyEnumeration : IfcPropertyAbstraction
	{
		internal string mName;//	 :	IfcLabel;
		internal List<IfcValue> mEnumerationValues = new List<IfcValue>();// :	LIST [1:?] OF UNIQUE IfcValue
		internal int mUnit; //	 :	OPTIONAL IfcUnit;
		internal IfcPropertyEnumeration(IfcPropertyEnumeration q) : base(q) { mName = q.mName; mEnumerationValues.AddRange(q.mEnumerationValues); mUnit = q.mUnit; }
		internal IfcPropertyEnumeration() : base() { }
		internal static void parseFields(IfcPropertyEnumeration p, List<string> arrFields, ref int ipos)
		{
			IfcPropertyAbstraction.parseFields(p, arrFields, ref ipos);
			p.mName = arrFields[ipos++].Replace("'", "");
			string s = arrFields[ipos++];
			p.mEnumerationValues = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)).ConvertAll(x => ParserIfc.parseValue(x));
			p.mUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		internal static IfcPropertyEnumeration Parse(string strDef) { IfcPropertyEnumeration p = new IfcPropertyEnumeration(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString()
		{
			string s = base.BuildString() + ",'" + mName + "',(" + mEnumerationValues[0].ToString();
			for (int icounter = 1; icounter < mEnumerationValues.Count; icounter++)
				s += "," + mEnumerationValues[icounter].ToString();
			return s + ")," + ParserSTEP.LinkToString(mUnit);
		}
	}
	public class IfcPropertyListValue : IfcSimpleProperty
	{
		internal List<IfcValue> mNominalValue = new List<IfcValue>();// :	OPTIONAL LIST [1:?] OF IfcValue;
		internal int mUnit;// : OPTIONAL IfcUnit; 
		internal IfcPropertyListValue(IfcPropertyListValue q) : base(q) { mNominalValue = q.mNominalValue; mUnit = q.mUnit; }
		internal IfcPropertyListValue(IfcSimpleProperty bc) : base(bc) { }
		internal IfcPropertyListValue() : base() { }
		internal IfcPropertyListValue(DatabaseIfc m, string name, string desc, IfcNamedUnit unit, List<IfcValue> values)
			: base(m, name, desc) { if (unit != null) mUnit = unit.mIndex; mNominalValue = values; }
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
		protected override string BuildString()
		{
			string result = base.BuildString() + ",(" + mNominalValue[0].ToString();
			for (int icounter = 1; icounter < mNominalValue.Count; icounter++)
				result += "," + mNominalValue[icounter].ToString();
			return result + (mUnit == 0 ? "),$" : "),#" + mUnit);
		}
	}
	public partial class IfcPropertyReferenceValue : IfcSimpleProperty
	{
		internal string mUsageName;// 	 :	OPTIONAL IfcText;
		internal int mPropertyReference = 0;// 	 :	OPTIONAL IfcObjectReferenceSelect;

		internal IfcPropertyReferenceValue(IfcPropertyReferenceValue p) : base(p) { mUsageName = p.mUsageName; mPropertyReference = p.mPropertyReference; }
		internal IfcPropertyReferenceValue() : base() { }
		internal IfcPropertyReferenceValue(DatabaseIfc m, string name, string desc) : base(m, name, desc) { }
		internal static void parseFields(IfcPropertyReferenceValue p, List<string> arrFields, ref int ipos) { IfcSimpleProperty.parseFields(p, arrFields, ref ipos); p.mUsageName = arrFields[ipos++]; p.mPropertyReference = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcPropertyReferenceValue Parse(string strDef) { IfcPropertyReferenceValue p = new IfcPropertyReferenceValue(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + "," + mUsageName + "," + ParserSTEP.LinkToString(mPropertyReference); }
	}
	public partial class IfcPropertySet : IfcPropertySetDefinition
	{
		protected List<int> mHasProperties = new List<int>(1);// : SET [1:?] OF IfcProperty;

		public List<IfcProperty> HasProperties { get { return mHasProperties.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcProperty); } }

		internal IfcPropertySet() : base() { }
		internal IfcPropertySet(IfcPropertySet i) : base(i) { mHasProperties = new List<int>(i.mHasProperties.ToArray()); }
		public IfcPropertySet(DatabaseIfc db, string name) : base(db, name) { }
		public IfcPropertySet(string name, List<IfcProperty> props) : base(props[0].mDatabase, name) { mHasProperties = props.ConvertAll(x => x.mIndex); }
		internal static IfcPropertySet Parse(string str)
		{
			IfcPropertySet p = new IfcPropertySet();
			int pos = 0;
			p.parseString(str, ref pos);
			p.mHasProperties = ParserSTEP.StripListLink(str, ref pos);
			return p;
		}
		protected override string BuildString()
		{
			if (mHasProperties.Count == 0)
				return "";
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mHasProperties[0]);
			for (int icounter = 1; icounter < mHasProperties.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mHasProperties[icounter]);
			return str + ")";
		}

		internal void relate()
		{
			List<IfcProperty> props = HasProperties;
			for (int icounter = 0; icounter < props.Count; icounter++)
				props[icounter].mPartOfPset.Add(this);
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
	}
	public abstract partial class IfcPropertySetDefinition : IfcPropertyDefinition //ABSTRACT SUPERTYPE OF (ONEOF (IfcElementQuantity,IfcEnergyProperties ,
	{ // IfcFluidFlowProperties,IfcPropertySet, IfcServiceLifeFactor ,IfcSoundProperties ,IfcSoundValue ,IfcSpaceThermalLoadProperties ))
		//INVERSE
		internal List<IfcTypeObject> mDefinesType = new List<IfcTypeObject>();// :	SET OF IfcTypeObject FOR HasPropertySets; IFC4change
		//internal List<IfcRelDefinesByTemplate> mIsDefinedBy = new List<IfcRelDefinesByTemplate>();//IsDefinedBy	 :	SET OF IfcRelDefinesByTemplate FOR RelatedPropertySets;
		private IfcRelDefinesByProperties mDefinesOccurrence = null; //:	SET [0:1] OF IfcRelDefinesByProperties FOR RelatingPropertyDefinition;
		internal IfcRelDefinesByProperties DefinesOccurrence { get { if (mDefinesOccurrence == null) mDefinesOccurrence = new IfcRelDefinesByProperties(this) { Name = Name }; return mDefinesOccurrence; } set { mDefinesOccurrence = value; } }
		protected IfcPropertySetDefinition() : base() { }
		protected IfcPropertySetDefinition(IfcPropertySetDefinition i) : base(i) { }
		protected IfcPropertySetDefinition(DatabaseIfc m, string name) : base(m) { Name = name; }
		internal static void parseFields(IfcPropertySetDefinition d, List<string> arrFields, ref int ipos) { IfcPropertyDefinition.parseFields(d, arrFields, ref ipos); }
	}
	public interface IfcPropertySetDefinitionSelect : IfcInterface { }// = SELECT ( IfcPropertySetDefinitionSet,  IfcPropertySetDefinition);
	//  IfcPropertySetDefinitionSet
	public partial class IfcPropertySingleValue : IfcSimpleProperty
	{
		private IfcValue mNominalValue;// : OPTIONAL IfcValue; 
		private int mUnit;// : OPTIONAL IfcUnit; 

		public IfcValue NominalValue { get { return mNominalValue; } set { mNominalValue = value; } }
		internal IfcUnit Unit { get { return mDatabase.mIfcObjects[mUnit] as IfcUnit; } set { mUnit = (value == null ? 0 : value.Index); } }

		private string mVal;
		internal IfcPropertySingleValue(IfcPropertySingleValue q) : base(q) { mNominalValue = q.mNominalValue; mUnit = q.mUnit; }
		internal IfcPropertySingleValue() : base() { }
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
			int pos = 0;
			p.parseString(str, ref pos);
			string s = ParserSTEP.StripField(str, ref pos);// arrFields[ipos++];
			p.mNominalValue = ParserIfc.parseValue(s);
			if (p.mNominalValue == null)
				p.mVal = s;
			p.mUnit = ParserSTEP.StripLink(str, ref pos); //ParserSTEP.ParseSTPLink(arrFields[ipos++]);
			return p;
		}
		protected override string BuildString() { return base.BuildString() + "," + (mNominalValue == null ? mVal : mNominalValue.ToString()) + "," + ParserSTEP.LinkToString(mUnit); }
	}
	public class IfcPropertyTableValue : IfcSimpleProperty
	{
		internal List<IfcValue> mDefiningValues = new List<IfcValue>();//	 :	OPTIONAL LIST [1:?] OF UNIQUE IfcValue;
		internal List<IfcValue> mDefinedValues = new List<IfcValue>();//	 :	OPTIONAL LIST [1:?] OF IfcValue;
		internal string mExpression = "$";// ::	OPTIONAL IfcText; 
		internal int mDefiningUnit;// : :	OPTIONAL IfcUnit;   
		internal int mDefinedUnit;// : :	OPTIONAL IfcUnit;
		internal IfcCurveInterpolationEnum mCurveInterpolation = IfcCurveInterpolationEnum.NOTDEFINED;// : :	OPTIONAL IfcCurveInterpolationEnum; 

		internal IfcPropertyTableValue(IfcPropertyTableValue q) : base(q) { mDefiningValues = new List<IfcValue>(q.mDefiningValues.ToArray()); mDefinedValues = new List<IfcValue>(q.mDefinedValues.ToArray()); mExpression = q.mExpression; mDefiningUnit = q.mDefiningUnit; mDefinedUnit = q.mDefinedUnit; mCurveInterpolation = q.mCurveInterpolation; }
		internal IfcPropertyTableValue() : base() { }
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
		protected override string BuildString()
		{
			string result = base.BuildString() + (mDefiningValues.Count > 0 ? ",(" + mDefiningValues[0].ToString() : ",$,");
			for (int icounter = 1; icounter < mDefiningValues.Count; icounter++)
				result += "," + mDefiningValues[icounter].ToString();
			result += (mDefiningValues.Count > 0 ? ")," : "") + (mDefinedValues.Count > 0 ? "(" + mDefinedValues[0].ToString() : "$,");
			for (int icounter = 1; icounter < mDefinedValues.Count; icounter++)
				result += "," + mDefinedValues[icounter].ToString();
			return result + (mDefinedValues.Count > 0 ? ")," : "") + mExpression + "," + ParserSTEP.LinkToString(mDefiningUnit) + "," + ParserSTEP.LinkToString(mDefinedUnit) + ",." + mCurveInterpolation.ToString() + ".";
		}
	}
	public class IfcProtectiveDevice : IfcFlowController //IFC4
	{
		internal IfcProtectiveDeviceTypeEnum mPredefinedType = IfcProtectiveDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcProtectiveDeviceTypeEnum;
		public IfcProtectiveDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcProtectiveDevice() : base() { }
		internal IfcProtectiveDevice(IfcProtectiveDevice d) : base(d) { mPredefinedType = d.mPredefinedType; }
		internal IfcProtectiveDevice(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcProtectiveDevice s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcProtectiveDeviceTypeEnum)Enum.Parse(typeof(IfcProtectiveDeviceTypeEnum), str);
		}
		internal new static IfcProtectiveDevice Parse(string strDef) { IfcProtectiveDevice s = new IfcProtectiveDevice(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcProtectiveDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcProtectiveDeviceTrippingUnit : IfcDistributionControlElement //IFC4  
	{
		internal IfcProtectiveDeviceTrippingUnitTypeEnum mPredefinedType = IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED;
		public IfcProtectiveDeviceTrippingUnitTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcProtectiveDeviceTrippingUnit() : base() { }
		internal IfcProtectiveDeviceTrippingUnit(IfcProtectiveDeviceTrippingUnit u) : base(u) { mPredefinedType = u.mPredefinedType; }
		internal IfcProtectiveDeviceTrippingUnit(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcProtectiveDeviceTrippingUnit a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcProtectiveDeviceTrippingUnitTypeEnum)Enum.Parse(typeof(IfcProtectiveDeviceTrippingUnitTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcProtectiveDeviceTrippingUnit Parse(string strDef) { IfcProtectiveDeviceTrippingUnit d = new IfcProtectiveDeviceTrippingUnit(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcProtectiveDeviceTrippingUnitType : IfcDistributionControlElementType
	{
		internal IfcProtectiveDeviceTrippingUnitTypeEnum mPredefinedType = IfcProtectiveDeviceTrippingUnitTypeEnum.NOTDEFINED;// : IfcProtectiveDeviceTrippingUnitTypeEnum;
		public IfcProtectiveDeviceTrippingUnitTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcProtectiveDeviceTrippingUnitType() : base() { }
		internal IfcProtectiveDeviceTrippingUnitType(IfcProtectiveDeviceTrippingUnitType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcProtectiveDeviceTrippingUnitType(DatabaseIfc m, string name, IfcProtectiveDeviceTrippingUnitTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcProtectiveDeviceTrippingUnitType t, List<string> arrFields, ref int ipos) { IfcDistributionControlElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcProtectiveDeviceTrippingUnitTypeEnum)Enum.Parse(typeof(IfcProtectiveDeviceTrippingUnitTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcProtectiveDeviceTrippingUnitType Parse(string strDef) { IfcProtectiveDeviceTrippingUnitType t = new IfcProtectiveDeviceTrippingUnitType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcProtectiveDeviceType : IfcFlowControllerType
	{
		internal IfcProtectiveDeviceTypeEnum mPredefinedType = IfcProtectiveDeviceTypeEnum.NOTDEFINED;// : IfcProtectiveDeviceTypeEnum; 
		public IfcProtectiveDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcProtectiveDeviceType() : base() { }
		internal IfcProtectiveDeviceType(IfcProtectiveDeviceType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcProtectiveDeviceType(DatabaseIfc m, string name, IfcProtectiveDeviceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcProtectiveDeviceType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcProtectiveDeviceTypeEnum)Enum.Parse(typeof(IfcProtectiveDeviceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcProtectiveDeviceType Parse(string strDef) { IfcProtectiveDeviceType t = new IfcProtectiveDeviceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcProxy : IfcProduct
	{
		internal IfcObjectTypeEnum mProxyType;// : IfcObjectTypeEnum;
		internal string mTag = "$";// : OPTIONAL IfcLabel;
		internal IfcProxy() : base() { }
		internal IfcProxy(IfcProxy q) : base(q) { mProxyType = q.mProxyType; mTag = q.mTag; }
		internal static void parseFields(IfcProxy p, List<string> arrFields, ref int ipos) { IfcProduct.parseFields(p, arrFields, ref ipos); p.mProxyType = (IfcObjectTypeEnum)Enum.Parse(typeof(IfcObjectTypeEnum), arrFields[ipos++].Replace(".", "")); p.mTag = arrFields[ipos++].Replace("'", ""); }
		internal static IfcProxy Parse(string strDef) { IfcProxy p = new IfcProxy(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + ",." + mProxyType.ToString() + ".," + (mTag == "$" ? "$" : "'" + mTag + "'"); }
	}
	public class IfcPump : IfcFlowMovingDevice //IFC4
	{
		internal IfcPumpTypeEnum mPredefinedType = IfcPumpTypeEnum.NOTDEFINED;// OPTIONAL : IfcPumpTypeEnum;
		public IfcPumpTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcPump() : base() { }
		internal IfcPump(IfcPump p) : base(p) { mPredefinedType = p.mPredefinedType; }
		internal IfcPump(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcPump s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcPumpTypeEnum)Enum.Parse(typeof(IfcPumpTypeEnum), str);
		}
		internal new static IfcPump Parse(string strDef) { IfcPump s = new IfcPump(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcPumpTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcPumpType : IfcFlowMovingDeviceType
	{
		internal IfcPumpTypeEnum mPredefinedType = IfcPumpTypeEnum.NOTDEFINED;// : IfcPumpTypeEnum; 
		public IfcPumpTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcPumpType() : base() { }
		internal IfcPumpType(IfcPumpType be) : base(be) { mPredefinedType = be.mPredefinedType; }
		internal static void parseFields(IfcPumpType t, List<string> arrFields, ref int ipos) { IfcFlowMovingDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcPumpTypeEnum)Enum.Parse(typeof(IfcPumpTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcPumpType Parse(string strDef) { IfcPumpType t = new IfcPumpType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
}
