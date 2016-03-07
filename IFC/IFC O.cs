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
	public abstract partial class IfcObject : IfcObjectDefinition, IfcObjectDefinitionSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcActor ,IfcControl ,IfcGroup ,IfcProcess ,IfcProduct ,IfcProject ,IfcResource))
	{
		internal string mObjectType = "$"; //: OPTIONAL IfcLabel;
		//INVERSE
		internal IfcRelDefinesByObject mIsDeclaredBy = null;
		internal List<IfcRelDefinesByObject> mDeclares = new List<IfcRelDefinesByObject>();
		private IfcRelDefinesByType mIsTypedBy = null;
		internal List<IfcRelDefinesByProperties> mIsDefinedBy = new List<IfcRelDefinesByProperties>();

		public string ObjectType { get { return mObjectType == "$" ? "" : ParserIfc.Decode(mObjectType); } set { mObjectType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		internal IfcRelDefinesByType IsTypedBy { get { return mIsTypedBy; } set { mIsTypedBy = value; } }
		public List<IfcRelDefinesByProperties> IsDefinedBy { get { return mIsDefinedBy; } }

		internal IfcTypeObject RelatingType 
		{ 
			get { return (mIsTypedBy == null ? null : mIsTypedBy.RelatingType); }
			set
			{
				if (mIsTypedBy != null)
					mIsTypedBy.mRelatedObjects.Remove(mIndex);
				if (value == null)
					mIsTypedBy = null;
				else //TODO CHECK CLASS NAME MATCHES INSTANCE
				{
					if (value.mObjectTypeOf == null)
						value.mObjectTypeOf = new IfcRelDefinesByType(this,value);
					else
						value.mObjectTypeOf.assignObj(this);
				}
			}
		}
		
		protected IfcObject() : base() { }
		protected IfcObject(IfcObject obj) : base(obj) { mObjectType = obj.mObjectType; }
		internal IfcObject(DatabaseIfc m) : base(m) { }
		
		protected static void parseFields(IfcObject obj, List<string> arrFields, ref int ipos) { IfcObjectDefinition.parseFields(obj, arrFields, ref ipos); obj.mObjectType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + (mObjectType == "$" ? ",$" : ",'" + mObjectType + "'"); }

	}
	public abstract partial class IfcObjectDefinition : IfcRoot, IfcDefinitionSelect  //ABSTRACT SUPERTYPE OF (ONEOF ((IfcContext, IfcObject, IfcTypeObject))))
	{	//INVERSE  
		internal List<IfcRelAssigns> mHasAssignments = new List<IfcRelAssigns>();//	 : 	SET OF IfcRelAssigns FOR RelatedObjects;
		internal IfcRelNests mNests = null;//	 :	SET [0:1] OF IfcRelNests FOR RelatedObjects;
		internal List<IfcRelNests> mIsNestedBy = new List<IfcRelNests>();//	 :	SET OF IfcRelNests FOR RelatingObject;
		internal IfcRelDeclares mHasContext = null;// :	SET [0:1] OF IfcRelDeclares FOR RelatedDefinitions; 
		internal List<IfcRelAggregates> mIsDecomposedBy = new List<IfcRelAggregates>();//	 : 	SET OF IfcRelDecomposes FOR RelatingObject;
		internal IfcRelAggregates mDecomposes = null;//	 : 	SET [0:1] OF IfcRelDecomposes FOR RelatedObjects; IFC4  IfcRelAggregates
		internal List<IfcRelAssociates> mHasAssociations = new List<IfcRelAssociates>();//	 : 	SET OF IfcRelAssociates FOR RelatedObjects;

		public List<IfcRelAssigns> HasAssignments { get { return mHasAssignments; } }
		public List<IfcRelNests> IsNestedBy { get { return mIsNestedBy; } }
		public IfcRelDeclares HasContext { get { return mHasContext; } set { mHasContext = value; } }
		public List<IfcRelAggregates> IsDecomposedBy { get { return mIsDecomposedBy; } }
		public List<IfcRelAssociates> HasAssociations { get { return mHasAssociations; } }

		DatabaseIfc IfcDefinitionSelect.Model { get { return mDatabase; } }
		internal IfcRelAssociatesMaterial RelatedMaterialAssociation
		{
			get
			{
				for (int icounter = 0; icounter < mHasAssociations.Count; icounter++)
				{
					IfcRelAssociatesMaterial rm = mHasAssociations[icounter] as IfcRelAssociatesMaterial;
					if (rm != null)
						return rm;
				}
				return null;
			}
		}
		
		protected IfcObjectDefinition() : base() { }
		protected IfcObjectDefinition(IfcObjectDefinition od) : base(od) { }
		protected IfcObjectDefinition(DatabaseIfc db) : base(db) {  }

		protected static void parseFields(IfcObjectDefinition objDef, List<string> arrFields, ref int ipos) { IfcRoot.parseFields(objDef, arrFields, ref ipos); }

		public void AddNested(IfcObjectDefinition o)
		{
			if (mIsNestedBy.Count == 0)
			{
				IfcRelNests rn = new IfcRelNests(this, o);
			}
			else mIsNestedBy[0].addObject(o);
		}
		public void AddAggregated(IfcObjectDefinition o) { if (mDecomposes == null) mDecomposes = new IfcRelAggregates(this, o); else mDecomposes.addObject(o); }
		
		internal virtual void relateNested(IfcRelNests n) { mIsNestedBy.Add(n); }
		
		protected virtual IfcMaterialSelect GetMaterialSelect()
		{
			List<IfcRelAssociates> has = HasAssociations;
			foreach (IfcRelAssociates ra in has)
			{
				IfcRelAssociatesMaterial rm = ra as IfcRelAssociatesMaterial;
				if (rm != null)
					return rm.RelatingMaterial;
			}
			return null;
		}
		private IfcMaterialSelect mMaterialSelectIFC4 = null;
		internal void setMaterial(IfcMaterialSelect material)
		{
			IfcMaterialSelect m = material;
			if (mDatabase.mSchema == Schema.IFC2x3)
			{
				IfcMaterialProfile profile = material as IfcMaterialProfile;
				if (profile != null)
				{
					m = profile.Material;
					mMaterialSelectIFC4 = profile;
					IfcProfileDef pd = profile.Profile;
					if (pd != null)
					{
						if (pd.mHasProperties.Count == 0)
						{
							IfcProfileProperties pp = new IfcProfileProperties(pd.Name, null, pd);
						}
						pd.mHasProperties[0].mAssociates.addAssociation(this);
					}
				}
				else
				{
					IfcMaterialProfileSet profileSet = material as IfcMaterialProfileSet;
					if (profileSet == null)
					{
						IfcMaterialProfileSetUsage profileSetUsage = material as IfcMaterialProfileSetUsage;
						if (profileSetUsage != null)
							profileSet = profileSetUsage.ForProfileSet;
					}
					if (profileSet != null)
					{
						m = profileSet.PrimaryMaterial;
						mMaterialSelectIFC4 = profileSet;
						foreach (IfcMaterialProfile matp in profileSet.MaterialProfiles)
						{
							IfcProfileDef pd = matp.Profile;
							if (pd != null)
							{
								if (pd.mHasProperties.Count == 0)
								{
									IfcProfileProperties pp = new IfcProfileProperties(pd.Name, null, pd);
								}
								pd.mHasProperties[0].mAssociates.addAssociation(this);
							}
						}
					}
					else
					{
						//constituentset....
					}
				}


			}
			for (int icounter = 0; icounter < mHasAssociations.Count; icounter++)
			{
				IfcRelAssociatesMaterial rm = mHasAssociations[icounter] as IfcRelAssociatesMaterial;
				if (rm != null)
					rm.unassign(this);
			}
			if (m != null)
				m.Associates.addAssociation(this);
		}
	}
	public interface IfcObjectDefinitionSelect : IfcInterface { List<IfcRelDefinesByProperties> IsDefinedBy { get; } } // IFC4 SELECT (IfcContext, IfcObject,IfcReferencePlacement);
	public abstract partial class IfcObjectPlacement : BaseClassIfc  //	 ABSTRACT SUPERTYPE OF (ONEOF (IfcGridPlacement ,IfcLocalPlacement));
	{	//INVERSE 
		internal List<IfcProduct> mPlacesObject = new List<IfcProduct>();// : SET [1:?] OF IfcProduct FOR ObjectPlacement;
		internal List<IfcLocalPlacement> mReferencedByPlacements = new List<IfcLocalPlacement>();// : SET [0:?] OF IfcLocalPlacement FOR PlacementRelTo;
		internal IfcProduct mContainerHost = null;
		protected IfcObjectPlacement() : base() { }
		protected IfcObjectPlacement(IfcObjectPlacement o) : base() { }
		protected IfcObjectPlacement(DatabaseIfc m, IfcProduct p)
			: base(m)
		{
			if (p != null)
			{
				p.Placement = this;
				if (!mPlacesObject.Contains(p))
					mPlacesObject.Add(p);
			}
		}
		protected static void parseFields(IfcObjectPlacement p, List<string> arrFields, ref int ipos) { }
	}
	public partial class IfcObjective : IfcConstraint
	{
		internal List<int> mBenchmarkValues = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcConstraint;
		internal IfcLogicalOperatorEnum mLogicalAggregator = IfcLogicalOperatorEnum.NONE;// : OPTIONAL IfcLogicalOperatorEnum;
		internal IfcObjectiveEnum mObjectiveQualifier = IfcObjectiveEnum.NOTDEFINED;// : 	IfcObjectiveEnum
		internal string mUserDefinedQualifier = "$";

		internal List<IfcConstraint> BenchMarks { get { return mBenchmarkValues.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcConstraint); } }

		internal IfcObjective() : base() { }
		internal IfcObjective(IfcObjective m) : base(m) { mBenchmarkValues = new List<int>(m.mBenchmarkValues.ToArray()); mLogicalAggregator = m.mLogicalAggregator;  mObjectiveQualifier = m.mObjectiveQualifier; mUserDefinedQualifier = m.mUserDefinedQualifier; }
		public IfcObjective(DatabaseIfc db, string name, IfcConstraintEnum constraint, List<IfcConstraint> benchmarks, IfcObjectiveEnum qualifier, string userQualifier)
		 	: base(db, name, constraint)
		{
			if (benchmarks != null && benchmarks.Count > 0)
				mBenchmarkValues = benchmarks.ConvertAll(x => x.mIndex);
			mObjectiveQualifier = qualifier;
			if (!string.IsNullOrEmpty(userQualifier))
				mUserDefinedQualifier = userQualifier.Replace("'", "");
		}

		internal static IfcObjective Parse(string strDef, Schema schema) { IfcObjective m = new IfcObjective(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcObjective m, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcConstraint.parseFields(m, arrFields, ref ipos,schema);
			m.mBenchmarkValues = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			string s = arrFields[ipos++];
			if (s[0] == '.')
				m.mLogicalAggregator = (IfcLogicalOperatorEnum)Enum.Parse(typeof(IfcLogicalOperatorEnum), s.Substring(1, s.Length - 2));
			m.mObjectiveQualifier = (IfcObjectiveEnum)Enum.Parse(typeof(IfcObjectiveEnum), arrFields[ipos++].Replace(".", ""));
			m.mUserDefinedQualifier = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildString()
		{
			string str = base.BuildString();
			if (mBenchmarkValues.Count > 0)
			{
				str += ",(" + ParserSTEP.LinkToString(mBenchmarkValues[0]);
				for (int icounter = 1; icounter < mBenchmarkValues.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mBenchmarkValues[icounter]);
				str += "),";
			}
			else
				str += ",$,";
			return str + (mLogicalAggregator != IfcLogicalOperatorEnum.NONE ? "." + mLogicalAggregator.ToString() + ".,." : "$,.") + mObjectiveQualifier + (mUserDefinedQualifier == "$" ? ".,$" : ".,'" + mUserDefinedQualifier + "'");
		}
	}
	public class IfcOccupant : IfcActor
	{
		internal IfcOccupantTypeEnum mPredefinedType = IfcOccupantTypeEnum.NOTDEFINED;//		:	OPTIONAL IfcOccupantTypeEnum;
		public IfcOccupantTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcOccupant(IfcOccupant o) : base(o) { }
		internal IfcOccupant() : base() { }
		internal IfcOccupant(IfcActorSelect a, IfcOccupantTypeEnum type) : base(a) { mPredefinedType = type; }
		internal new static IfcOccupant Parse(string strDef) { IfcOccupant a = new IfcOccupant(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcOccupant a, List<string> arrFields, ref int ipos) { IfcObject.parseFields(a, arrFields, ref ipos); a.mTheActor = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 || mPredefinedType != IfcOccupantTypeEnum.NOTDEFINED ? ",." + mPredefinedType.ToString() + "." : ",$"); }
	}
	//ENTITY IfcOffsetCurve2D
	//ENTITY IfcOffsetCurve3D
	public class IfcOneDirectionRepeatFactor : IfcGeometricRepresentationItem // DEPRECEATED IFC4
	{
		internal int mRepeatFactor;//  : IfcVector 
		internal IfcOneDirectionRepeatFactor() : base() { }
		internal IfcOneDirectionRepeatFactor(IfcOneDirectionRepeatFactor p) : base((IfcGeometricRepresentationItem)p) { mRepeatFactor = p.mRepeatFactor; }
		internal static void parseFields(IfcOneDirectionRepeatFactor f, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(f, arrFields, ref ipos); f.mRepeatFactor = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcOneDirectionRepeatFactor Parse(string strDef) { IfcOneDirectionRepeatFactor f = new IfcOneDirectionRepeatFactor(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRepeatFactor); }
	}
	public partial class IfcOpeningElement : IfcFeatureElementSubtraction
	{
		internal IfcOpeningElementTypeEnum mPredefinedType = IfcOpeningElementTypeEnum.NOTDEFINED;// :	OPTIONAL IfcOpeningElementTypeEnum; //IFC4
		//INVERSE
		internal List<IfcRelFillsElement> mHasFillings = new List<IfcRelFillsElement>();

		public IfcOpeningElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcOpeningElement() : base() { }
		internal IfcOpeningElement(IfcOpeningElement od) : base(od) { mPredefinedType = od.mPredefinedType; }
		internal IfcOpeningElement(DatabaseIfc db) : base(db) { }
		public IfcOpeningElement(IfcElement host, IfcObjectPlacement placement, IfcProductRepresentation rep) : base(host.mDatabase) { Placement = placement; Representation = rep; IfcRelVoidsElement rve = new IfcRelVoidsElement(host, this); }
	
		internal static IfcOpeningElement Parse(string strDef, Schema schema) { IfcOpeningElement e = new IfcOpeningElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		internal static void parseFields(IfcOpeningElement e, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcFeatureElementSubtraction.parseFields(e, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
				e.mPredefinedType = (IfcOpeningElementTypeEnum)Enum.Parse(typeof(IfcOpeningElementTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcOpeningElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
	}
	public class IfcOpeningStandardCase : IfcOpeningElement //IFC4
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 ? "IFCOPENINGELEMENT" : base.KeyWord); } }
		internal IfcOpeningStandardCase() : base() { }
		internal IfcOpeningStandardCase(IfcOpeningStandardCase o) : base(o) { }
		public IfcOpeningStandardCase(IfcElement host, IfcObjectPlacement placement, IfcExtrudedAreaSolid eas) : base(host, placement, new IfcProductDefinitionShape(new IfcShapeRepresentation(eas))) { }
		public IfcOpeningStandardCase(DatabaseIfc db, IfcObjectPlacement placement, IfcExtrudedAreaSolid eas) : base(db) { Placement = placement; Representation = new IfcProductDefinitionShape(new IfcShapeRepresentation(eas)); }
		internal static IfcOpeningStandardCase Parse(string strDef) { IfcOpeningStandardCase c = new IfcOpeningStandardCase(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcOpeningStandardCase c, List<string> arrFields, ref int ipos) { IfcOpeningElement.parseFields(c, arrFields, ref ipos); }
	}
	//ENTITY IfcOpticalMaterialProperties // DEPRECEATED IFC4
	//ENTITY IfcOrderAction // DEPRECEATED IFC4
	public partial class IfcOpenShell : IfcConnectedFaceSet, IfcShell
	{
		internal IfcOpenShell() : base() { }
		internal IfcOpenShell(IfcOpenShell od) : base(od) { }
		internal new static IfcOpenShell Parse(string str)
		{
			IfcOpenShell s = new IfcOpenShell();
			int pos = 0;
			s.Parse(str, ref pos);
			return s;
		}
	}
	public class IfcOrganization : BaseClassIfc, IfcActorSelect, IfcResourceObjectSelect
	{
		internal string mIdentification = "$";// : OPTIONAL IfcIdentifier;
		private string mName;// : IfcLabel;
		private string mDescription = "$";// : OPTIONAL IfcText;
		private List<int> mRoles = new List<int>();// : OPTIONAL LIST [1:?] OF IfcActorRole;
		private List<int> mAddresses = new List<int>();// : OPTIONAL LIST [1:?] OF IfcAddress; 
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public override string Name
		{
			get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); }
			set { mName = (string.IsNullOrEmpty(value) ? "UNKNOWN" : ParserIfc.Encode(value.Replace("'", ""))); }
		}
		internal List<IfcActorRole> Roles { get { return mRoles.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcActorRole); } set { mRoles = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		internal List<IfcAddress> Addresses { get { return mAddresses.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcAddress); } set { mAddresses = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcOrganization() : base() { }
		internal IfcOrganization(IfcOrganization i) : base() { mIdentification = i.mIdentification; mName = i.mName; mDescription = i.mDescription; mRoles = i.mRoles; mAddresses = i.mAddresses; }
		internal IfcOrganization(DatabaseIfc db) : base(db)
		{
			mName = "UNKNOWN";
			try
			{
				mName = ((string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", "RegisteredOrganization", "")).Replace("'", "");
				if (string.IsNullOrEmpty(mName) || mName == "Microsoft")
					mName = "UNKNOWN";
			}
			catch (Exception) { }
		}
		internal IfcOrganization(DatabaseIfc m, string name) : base(m) { Name = name; }
		internal static void parseFields(IfcOrganization o, List<string> arrFields, ref int ipos)
		{
			o.mIdentification = arrFields[ipos++];
			o.mName = arrFields[ipos++].Replace("'", "");
			o.mDescription = arrFields[ipos++];
			o.mRoles = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			o.mAddresses = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		internal static IfcOrganization Parse(string strDef) { IfcOrganization o = new IfcOrganization(); int ipos = 0; parseFields(o, ParserSTEP.SplitLineFields(strDef), ref ipos); return o; }
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + mIdentification + ",'" + mName + "'," + mDescription + (mRoles.Count == 0 ? ",$" : ",(#" + mRoles[0]);

			for (int icounter = 1; icounter < mRoles.Count; icounter++)
				str += ",#" + mRoles;
			str += (mRoles.Count == 0 ? "" : "),") + (mAddresses.Count == 0 ? ",$" : "(#" + mAddresses[0]);
			for (int icounter = 1; icounter < mAddresses.Count; icounter++)
				str += ",#" + mAddresses[icounter];
			return str + (mAddresses.Count > 0 ? ")" : "");
		}
	}
	//ENTITY IfcOrganizationRelationship; //optional name
	public partial class IfcOrientedEdge : IfcEdge
	{
		internal int mEdgeElement;// IfcEdge;
		internal bool mOrientation = true;// : BOOL;
		internal IfcEdge EdgeElement { get { return mDatabase.mIfcObjects[mEdgeElement] as IfcEdge; } }
		internal IfcOrientedEdge() : base() { }
		internal IfcOrientedEdge(IfcOrientedEdge el) : base(el) { mEdgeElement = el.mEdgeElement; mOrientation = el.mOrientation; }
		public IfcOrientedEdge(IfcEdge e, bool sense) : base(e.mDatabase) { mEdgeElement = e.mIndex; mOrientation = sense; }
		internal IfcOrientedEdge(IfcVertexPoint a, IfcVertexPoint b) : base(a.mDatabase) { mEdgeElement = new IfcEdge(a, b).mIndex; }
		internal new static IfcOrientedEdge Parse(string strDef) { IfcOrientedEdge e = new IfcOrientedEdge(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		internal static void parseFields(IfcOrientedEdge e, List<string> arrFields, ref int ipos)
		{
			if (arrFields.Count > 2)
				IfcEdge.parseFields(e, arrFields, ref ipos);
			e.mEdgeElement = ParserSTEP.ParseLink(arrFields[ipos++]);
			e.mOrientation = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mEdgeElement) + "," + ParserSTEP.BoolToString(mOrientation); }
	}
	public class IfcOuterBoundaryCurve : IfcBoundaryCurve
	{
		internal IfcOuterBoundaryCurve() : base() { }
		internal IfcOuterBoundaryCurve(IfcOuterBoundaryCurve i) : base(i) { }
		internal IfcOuterBoundaryCurve(List<IfcCompositeCurveSegment> segs) : base(segs) { }
		internal new static IfcOuterBoundaryCurve Parse(string strDef) { IfcOuterBoundaryCurve b = new IfcOuterBoundaryCurve(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcOuterBoundaryCurve b, List<string> arrFields, ref int ipos) { IfcBoundaryCurve.parseFields(b, arrFields, ref ipos); }
	}
	public class IfcOutlet : IfcFlowTerminal //IFC4
	{
		internal IfcOutletTypeEnum mPredefinedType = IfcOutletTypeEnum.NOTDEFINED;// OPTIONAL : IfcOutletTypeEnum;
		public IfcOutletTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcOutlet() : base() { }
		internal IfcOutlet(IfcOutlet o) : base(o) { mPredefinedType = o.mPredefinedType; }
		internal IfcOutlet(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcOutlet s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcOutletTypeEnum)Enum.Parse(typeof(IfcOutletTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcOutlet Parse(string strDef) { IfcOutlet s = new IfcOutlet(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcOutletTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public class IfcOutletType : IfcFlowTerminalType
	{
		internal IfcOutletTypeEnum mPredefinedType = IfcOutletTypeEnum.NOTDEFINED;// : IfcOutletTypeEnum; 
		public IfcOutletTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcOutletType() : base() { }
		internal IfcOutletType(IfcOutletType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcOutletType(DatabaseIfc m, string name, IfcOutletTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcOutletType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcOutletTypeEnum)Enum.Parse(typeof(IfcOutletTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcOutletType Parse(string strDef) { IfcOutletType t = new IfcOutletType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcOwnerHistory : BaseClassIfc
	{ 
		internal int mOwningUser;// : IfcPersonAndOrganization;
		internal int mOwningApplication;// : IfcApplication;
		internal IfcStateEnum mState = IfcStateEnum.NA;// : OPTIONAL IfcStateEnum;
		internal IfcChangeActionEnum mChangeAction;// : IfcChangeActionEnum;
		internal int mLastModifiedDate;// : OPTIONAL IfcTimeStamp;
		internal int mLastModifyingUser;// : OPTIONAL IfcPersonAndOrganization;
		internal int mLastModifyingApplication;// : OPTIONAL IfcApplication;
		internal int mCreationDate;// : IfcTimeStamp; 

		public IfcStateEnum State { get { return mState; } }
		public IfcChangeActionEnum ChangeAction { get { return mChangeAction; } }

		internal IfcOwnerHistory() : base() { }
		internal IfcOwnerHistory(IfcOwnerHistory o) : base()
		{
			mOwningUser = o.mOwningUser;
			mOwningApplication = o.mOwningApplication;
			mState = o.mState;
			mChangeAction = o.mChangeAction;
			mLastModifiedDate = o.mLastModifiedDate;
			mLastModifyingUser = o.mLastModifyingUser;
			mLastModifyingApplication = o.mLastModifyingApplication;
			mCreationDate = o.mCreationDate;
		}
		internal IfcOwnerHistory(IfcPersonAndOrganization po, IfcApplication app, IfcChangeActionEnum ca) : base(po.mDatabase)
		{
			mOwningUser = po.mIndex;
			mOwningApplication = app.mIndex;
			mState = IfcStateEnum.NA;
			mChangeAction = ca;
			TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
			//if(ca == IfcChangeActionEnum.ADDED)
			mCreationDate = (int)ts.TotalSeconds;
			//if (ca != IfcChangeActionEnum.NOTDEFINED)
			mLastModifiedDate = (int)ts.TotalSeconds;
		}
		internal static void parseFields(IfcOwnerHistory o, List<string> arrFields, ref int ipos)
		{
			o.mOwningUser = ParserSTEP.ParseLink(arrFields[ipos++]);
			o.mOwningApplication = ParserSTEP.ParseLink(arrFields[ipos++]);
			string str = arrFields[ipos++].Replace(".", "");
			str = str.Trim();
			if (str == "" || str.StartsWith("$"))
				o.mState = IfcStateEnum.NA;
			else
				o.mState = (IfcStateEnum)Enum.Parse(typeof(IfcStateEnum), str);
			str = arrFields[ipos++].Replace(".", "");
			if (str.EndsWith("ADDED"))
				o.mChangeAction = IfcChangeActionEnum.ADDED;
			if (str.EndsWith("DELETED"))
				o.mChangeAction = IfcChangeActionEnum.DELETED;
			else
				o.mChangeAction = (IfcChangeActionEnum)Enum.Parse(typeof(IfcChangeActionEnum), str);
			o.mLastModifiedDate = ParserSTEP.ParseInt(arrFields[ipos++]);
			o.mLastModifyingUser = ParserSTEP.ParseLink(arrFields[ipos++]);
			o.mLastModifyingApplication = ParserSTEP.ParseLink(arrFields[ipos++]);
			o.mCreationDate = ParserSTEP.ParseInt(arrFields[ipos++]);
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + ParserSTEP.LinkToString(mOwningUser) + "," + ParserSTEP.LinkToString(mOwningApplication) + ",";
			if (mState == IfcStateEnum.NA)
				str += "$";
			else
				str += "." + mState.ToString() + ".";
			return str + ",." + (mDatabase.mSchema == Schema.IFC2x3 && mChangeAction == IfcChangeActionEnum.NOTDEFINED ? IfcChangeActionEnum.NOCHANGE : mChangeAction).ToString() + ".," + ParserSTEP.IntOptionalToString(mLastModifiedDate) + ","
				+ ParserSTEP.LinkToString(mLastModifyingUser) + "," + ParserSTEP.LinkToString(mLastModifyingApplication) + "," + ParserSTEP.IntToString(mCreationDate);
		}
		internal static IfcOwnerHistory Parse(string strDef) { IfcOwnerHistory o = new IfcOwnerHistory(); int ipos = 0; parseFields(o, ParserSTEP.SplitLineFields(strDef), ref ipos); return o; }
	}
}
