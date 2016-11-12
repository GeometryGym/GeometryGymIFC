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
	public abstract partial class IfcObject : IfcObjectDefinition //ABSTRACT SUPERTYPE OF (ONEOF (IfcActor ,IfcControl ,IfcGroup ,IfcProcess ,IfcProduct ,IfcProject ,IfcResource))
	{
		internal string mObjectType = "$"; //: OPTIONAL IfcLabel;
		//INVERSE
		internal IfcRelDefinesByObject mIsDeclaredBy = null;
		internal List<IfcRelDefinesByObject> mDeclares = new List<IfcRelDefinesByObject>();
		internal IfcRelDefinesByType mIsTypedBy = null;
		internal List<IfcRelDefinesByProperties> mIsDefinedBy = new List<IfcRelDefinesByProperties>();

		public string ObjectType { get { return mObjectType == "$" ? "" : ParserIfc.Decode(mObjectType); } set { mObjectType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcRelDefinesByType IsTypedBy { get { return mIsTypedBy; } set { value.AssignObj(this); } }
		public List<IfcRelDefinesByProperties> IsDefinedBy { get { return mIsDefinedBy; } }

		public IfcTypeObject RelatingType 
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
						value.mObjectTypeOf.AssignObj(this);
				}
			}
		}
		
		protected IfcObject() : base() { }
		protected IfcObject(IfcObject basis) : base(basis) { mObjectType = basis.mObjectType; mIsDeclaredBy = basis.mIsDeclaredBy; mIsTypedBy = basis.mIsTypedBy; mIsDefinedBy = basis.mIsDefinedBy; }
		protected IfcObject(DatabaseIfc db, IfcObject o) : base(db, o)//, bool downStream) : base(db, o, downStream)
		{
			mObjectType = o.mObjectType;
			foreach (IfcRelDefinesByProperties rdp in o.mIsDefinedBy)
			{
				IfcRelDefinesByProperties drdp = db.Factory.Duplicate(rdp) as IfcRelDefinesByProperties;
				drdp.Assign(this);
			}
			if(o.mIsTypedBy != null)
				IsTypedBy = db.Factory.Duplicate(o.mIsTypedBy,false) as IfcRelDefinesByType;
		}
		internal IfcObject(DatabaseIfc db) : base(db) { }
		protected override void Parse(string str, ref int pos)
		{
			base.Parse(str, ref pos);
			mObjectType = ParserSTEP.StripString(str, ref pos);
		}
		protected static void parseFields(IfcObject obj, List<string> arrFields, ref int ipos) { IfcObjectDefinition.parseFields(obj, arrFields, ref ipos); obj.mObjectType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mObjectType == "$" ? ",$" : ",'" + mObjectType + "'"); }

		public override List<T> Extract<T>()
		{
			List<T> result = base.Extract<T>();
			foreach (IfcRelDefinesByProperties rdp in mIsDefinedBy)
				result.AddRange(rdp.Extract<T>());
			return result;
		}
		internal void IsolateObject(string filename)
		{
			DatabaseIfc db = new DatabaseIfc(mDatabase);
			IfcSpatialElement spatial = this as IfcSpatialElement;
			if (spatial != null)
				db.Factory.Duplicate(spatial, true);
			else
				db.Factory.Duplicate(this);
			db.WriteFile(filename);
		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			for (int icounter = 0; icounter < mIsDefinedBy.Count; icounter++)
				mIsDefinedBy[icounter].changeSchema(schema);
			if(mIsTypedBy != null)
				mIsTypedBy.changeSchema(schema);
			base.changeSchema(schema);
		}
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
		protected IfcObjectDefinition(DatabaseIfc db) : base(db) {  }
		protected IfcObjectDefinition(IfcObjectDefinition basis) : base(basis)
		{
			mHasAssignments = basis.mHasAssignments;
			mNests = basis.mNests;
			mIsNestedBy = basis.mIsNestedBy;
			mHasContext = basis.mHasContext;
			mIsDecomposedBy = basis.mIsDecomposedBy;
			mDecomposes = basis.mDecomposes;
			mHasAssociations = basis.mHasAssociations;
		}
		protected IfcObjectDefinition(DatabaseIfc db, IfcObjectDefinition o) : base (db, o)//, bool downStream) : base(db, o)
		{
			foreach(IfcRelAssigns assigns in o.mHasAssignments)
			{
				IfcRelAssigns dup = db.Factory.Duplicate(assigns) as IfcRelAssigns;
				dup.AddRelated(this);
			}
			if (o.mDecomposes != null)
				(db.Factory.Duplicate(o.mDecomposes, false) as IfcRelAggregates).addObject(this);
			foreach (IfcRelAssociates associates in o.mHasAssociations)
			{
				IfcRelAssociates dup = db.Factory.Duplicate(associates) as IfcRelAssociates;
				dup.addAssociation(this);
			}
			if (mHasContext != null)
				(db.Factory.Duplicate(mHasContext, false) as IfcRelDeclares).AddRelated(this);	
		}
		
		protected static void parseFields(IfcObjectDefinition objDef, List<string> arrFields, ref int ipos) { IfcRoot.parseFields(objDef, arrFields, ref ipos); }

		public void AddNested(IfcObjectDefinition o)
		{
			if (mIsNestedBy.Count == 0)
			{
				IfcRelNests rn = new IfcRelNests(this, o);
			}
			else mIsNestedBy[0].addObject(o);
		}
		public void AddAggregated(IfcObjectDefinition o)
		{
			if (mIsDecomposedBy.Count == 0)
			{
				new IfcRelAggregates(this, o);
			}
			else
				mIsDecomposedBy[0].addObject(o); }
		
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
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
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

		internal virtual IfcProperty findProperty(string name)
		{
			List<IfcPropertySet> psets = Extract<IfcPropertySet>();
			foreach (IfcPropertySet pset in psets)
			{
				IfcProperty p = pset.FindProperty(name);
				if (p != null)
					return p;
			}
			return null;
		}
		public override List<T> Extract<T>()
		{
			// Early implementation, should search for typed objects such as products and type products.  Contact Jon
			// for expanding for more ifc classes
			List<T> result = base.Extract<T>();	
			foreach(IfcRelNests rns in mIsNestedBy)
			{
				foreach(IfcObjectDefinition od in rns.RelatedObjects)
					result.AddRange(od.Extract<T>());
			}
			foreach(IfcRelAggregates rags in mIsDecomposedBy)
			{
				foreach (IfcObjectDefinition od in rags.RelatedObjects)
					result.AddRange(od.Extract<T>());
			}
			return result;
		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			for (int icounter = 0; icounter < mHasAssignments.Count; icounter++)
			{
				IfcRelAssigns assigns = mDatabase[mHasAssignments[icounter].Index] as IfcRelAssigns;
				if(assigns != null)
					assigns.changeSchema(schema);
			}
			for (int icounter = 0; icounter < mIsNestedBy.Count; icounter++)
				mIsNestedBy[icounter].changeSchema(schema);
			for (int icounter = 0; icounter < mHasAssociations.Count; icounter++)
				mHasAssociations[icounter].changeSchema(schema);
			for (int icounter = 0; icounter < mIsDecomposedBy.Count; icounter++)
				mIsDecomposedBy[icounter].changeSchema(schema);
			base.changeSchema(schema);
		}
		public virtual IfcStructuralAnalysisModel CreateOrFindStructAnalysisModel()
		{
			return (mDecomposes != null ? mDecomposes.RelatingObject.CreateOrFindStructAnalysisModel() : null);
		}

		public virtual IfcStructuralAnalysisModel FindStructAnalysisModel(bool strict)
		{
			return (!strict && mDecomposes != null ? mDecomposes.RelatingObject.FindStructAnalysisModel(false) : null);
		}
	}
	public abstract partial class IfcObjectPlacement : BaseClassIfc  //	 ABSTRACT SUPERTYPE OF (ONEOF (IfcGridPlacement ,IfcLocalPlacement));
	{	//INVERSE 
		internal List<IfcProduct> mPlacesObject = new List<IfcProduct>();// : SET [0:?] OF IfcProduct FOR ObjectPlacement; ifc2x3 [1:?] 
		internal List<IfcLocalPlacement> mReferencedByPlacements = new List<IfcLocalPlacement>();// : SET [0:?] OF IfcLocalPlacement FOR PlacementRelTo;
		internal IfcProduct mContainerHost = null;

		protected IfcObjectPlacement() : base() { }
		protected IfcObjectPlacement(DatabaseIfc db) : base(db) { }
		protected IfcObjectPlacement(DatabaseIfc db, IfcProduct p) : base(db)
		{
			if (p != null)
			{
				p.Placement = this;
				if (!mPlacesObject.Contains(p))
					mPlacesObject.Add(p);
			}
		}
		protected IfcObjectPlacement(DatabaseIfc db, IfcObjectPlacement p) : base(db,p) { }
		protected static void parseFields(IfcObjectPlacement p, List<string> arrFields, ref int ipos) { }

		internal virtual bool isWorldXY { get { return false; } }
	}
	public partial class IfcObjective : IfcConstraint
	{
		internal List<int> mBenchmarkValues = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcConstraint;
		internal IfcLogicalOperatorEnum mLogicalAggregator = IfcLogicalOperatorEnum.NONE;// : OPTIONAL IfcLogicalOperatorEnum;
		internal IfcObjectiveEnum mObjectiveQualifier = IfcObjectiveEnum.NOTDEFINED;// : 	IfcObjectiveEnum
		internal string mUserDefinedQualifier = "$"; //	:	OPTIONAL IfcLabel;

		public List<IfcConstraint> BenchmarkValues { get { return mBenchmarkValues.ConvertAll(x => mDatabase[x] as IfcConstraint); } set { mBenchmarkValues = value.ConvertAll(x => x.mIndex); } }
		public IfcLogicalOperatorEnum LogicalAggregator { get { return mLogicalAggregator; } set { mLogicalAggregator = value; } }
		public IfcObjectiveEnum ObjectiveQualifier { get { return mObjectiveQualifier; } set { mObjectiveQualifier = value; } }
		public string UserDefinedQualifier { get { return (mUserDefinedQualifier == "$" ? "" : ParserIfc.Decode(mUserDefinedQualifier)); } set { mUserDefinedQualifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcObjective() : base() { }
		internal IfcObjective(DatabaseIfc db, IfcObjective o) : base(db,o) { BenchmarkValues = o.BenchmarkValues.ConvertAll(x=>db.Factory.Duplicate(x) as IfcConstraint); mLogicalAggregator = o.mLogicalAggregator;  mObjectiveQualifier = o.mObjectiveQualifier; mUserDefinedQualifier = o.mUserDefinedQualifier; }
		public IfcObjective(DatabaseIfc db, string name, IfcConstraintEnum constraint, IfcObjectiveEnum qualifier)
		 	: base(db, name, constraint) { mObjectiveQualifier = qualifier; }

		internal static IfcObjective Parse(string strDef, ReleaseVersion schema) { IfcObjective m = new IfcObjective(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcObjective m, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcConstraint.parseFields(m, arrFields, ref ipos,schema);
			m.mBenchmarkValues = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			string s = arrFields[ipos++];
			if (s[0] == '.')
				m.mLogicalAggregator = (IfcLogicalOperatorEnum)Enum.Parse(typeof(IfcLogicalOperatorEnum), s.Substring(1, s.Length - 2));
			m.mObjectiveQualifier = (IfcObjectiveEnum)Enum.Parse(typeof(IfcObjectiveEnum), arrFields[ipos++].Replace(".", ""));
			m.mUserDefinedQualifier = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP();
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
		public override bool Destruct(bool children)
		{
			if (children)
			{
				for (int icounter = 0; icounter < mBenchmarkValues.Count; icounter++)
				{
					BaseClassIfc bc = mDatabase[mBenchmarkValues[icounter]];
					if (bc != null)
						bc.Destruct(true);
				}
			}
			return base.Destruct(children);
		}
	}
	public partial class IfcOccupant : IfcActor
	{
		internal IfcOccupantTypeEnum mPredefinedType = IfcOccupantTypeEnum.NOTDEFINED;//		:	OPTIONAL IfcOccupantTypeEnum;
		public IfcOccupantTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcOccupant() : base() { }
		internal IfcOccupant(DatabaseIfc db, IfcOccupant o) : base(db, o) { mPredefinedType = o.mPredefinedType; }
		public IfcOccupant(IfcActorSelect a, IfcOccupantTypeEnum type) : base(a) { mPredefinedType = type; }
		internal new static IfcOccupant Parse(string strDef) { IfcOccupant a = new IfcOccupant(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcOccupant a, List<string> arrFields, ref int ipos) { IfcObject.parseFields(a, arrFields, ref ipos); a.mTheActor = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mPredefinedType != IfcOccupantTypeEnum.NOTDEFINED ? ",." + mPredefinedType.ToString() + "." : ",$"); }
	}
	//ENTITY IfcOffsetCurve2D
	//ENTITY IfcOffsetCurve3D
	public partial class IfcOneDirectionRepeatFactor : IfcGeometricRepresentationItem // DEPRECEATED IFC4 SUPERTYPE OF	(IfcTwoDirectionRepeatFactor)

	{
		internal int mRepeatFactor;//  : IfcVector 
		public IfcVector RepeatFactor { get { return mDatabase[mRepeatFactor] as IfcVector; } set { mRepeatFactor = value.mIndex; } }

		internal IfcOneDirectionRepeatFactor() : base() { }
		internal IfcOneDirectionRepeatFactor(DatabaseIfc db, IfcOneDirectionRepeatFactor f) : base(db, f) { RepeatFactor = db.Factory.Duplicate(f.RepeatFactor) as IfcVector; }
		internal static IfcOneDirectionRepeatFactor Parse(string str) { IfcOneDirectionRepeatFactor f = new IfcOneDirectionRepeatFactor(); int pos = 0; f.Parse(str, ref pos); return f; }
		protected virtual void Parse(string str,ref int pos)
		{
			mRepeatFactor = ParserSTEP.StripLink(str, ref pos);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRepeatFactor); }
	}
	public partial class IfcOpeningElement : IfcFeatureElementSubtraction //SUPERTYPE OF(IfcOpeningStandardCase)
	{
		internal IfcOpeningElementTypeEnum mPredefinedType = IfcOpeningElementTypeEnum.NOTDEFINED;// :	OPTIONAL IfcOpeningElementTypeEnum; //IFC4
		//INVERSE
		internal List<IfcRelFillsElement> mHasFillings = new List<IfcRelFillsElement>();

		public IfcOpeningElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcOpeningElement() : base() { }
		internal IfcOpeningElement(DatabaseIfc db, IfcOpeningElement e) : base(db, e) { mPredefinedType = e.mPredefinedType; }
		internal IfcOpeningElement(DatabaseIfc db) : base(db) { }
		public IfcOpeningElement(IfcElement host, IfcObjectPlacement placement, IfcProductRepresentation rep) : base(host.mDatabase)
		{
			if (placement == null)
				Placement = new IfcLocalPlacement(host.Placement, new IfcAxis2Placement3D(Database.Factory.Origin));
			else
				Placement = placement;
			Representation = rep;
			IfcRelVoidsElement rve = new IfcRelVoidsElement(host, this);
		}
	
		internal static IfcOpeningElement Parse(string strDef, ReleaseVersion schema) { IfcOpeningElement e = new IfcOpeningElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		internal static void parseFields(IfcOpeningElement e, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcFeatureElementSubtraction.parseFields(e, arrFields, ref ipos);
			if (schema != ReleaseVersion.IFC2x3)
				e.mPredefinedType = (IfcOpeningElementTypeEnum)Enum.Parse(typeof(IfcOpeningElementTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcOpeningElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
	}
	public partial class IfcOpeningStandardCase : IfcOpeningElement //IFC4
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcOpeningElement" : base.KeyWord); } }
		internal IfcOpeningStandardCase() : base() { }
		internal IfcOpeningStandardCase(DatabaseIfc db, IfcOpeningStandardCase o) : base(db,o) { }
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
		internal IfcOpenShell(DatabaseIfc db, IfcOpenShell s) : base(db,s) { }
		internal new static IfcOpenShell Parse(string str) { IfcOpenShell s = new IfcOpenShell(); s.parse(str); return s; }
	}
	public partial class IfcOrganization : BaseClassIfc, IfcActorSelect, IfcResourceObjectSelect
	{
		internal string mIdentification = "$";// : OPTIONAL IfcIdentifier;
		private string mName = "";// : IfcLabel;
		private string mDescription = "$";// : OPTIONAL IfcText;
		private List<int> mRoles = new List<int>();// : OPTIONAL LIST [1:?] OF IfcActorRole;
		private List<int> mAddresses = new List<int>();// : OPTIONAL LIST [1:?] OF IfcAddress; 
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public override string Name
		{
			get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); }
			set { mName = (string.IsNullOrEmpty(value) ? "UNKNOWN" : ParserIfc.Encode(value)); }
		}
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public List<IfcActorRole> Roles { get { return mRoles.ConvertAll(x => mDatabase[x] as IfcActorRole); } set { mRoles = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		public List<IfcAddress> Addresses { get { return mAddresses.ConvertAll(x => mDatabase[x] as IfcAddress); } set { mAddresses = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal static string Organization
		{
			get
			{
				try
				{
					string name = ((string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", "RegisteredOrganization", "")).Replace("'", "");
					if (!string.IsNullOrEmpty(name) && string.Compare(name, "Microsoft", true) != 0)
						return name;
				}
				catch (Exception) { }
				return "Unknown";
			}
		}

		internal IfcOrganization() : base() { }
		internal IfcOrganization(DatabaseIfc db) : base(db) { } 
		internal IfcOrganization(DatabaseIfc db, IfcOrganization o) : base(db,o)
		{
			mIdentification = o.mIdentification;
			mName = o.mName;
			mDescription = o.mDescription;
			Roles = o.Roles.ConvertAll(x => db.Factory.Duplicate(x) as IfcActorRole);
			Addresses = o.Addresses.ConvertAll(x => db.Factory.Duplicate(x) as IfcAddress);
		}
		public IfcOrganization(DatabaseIfc m, string name) : base(m) { Name = name; }
		internal static void parseFields(IfcOrganization o, List<string> arrFields, ref int ipos)
		{
			o.mIdentification = arrFields[ipos++];
			o.mName = arrFields[ipos++].Replace("'", "");
			o.mDescription = arrFields[ipos++];
			o.mRoles = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			o.mAddresses = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		internal static IfcOrganization Parse(string strDef) { IfcOrganization o = new IfcOrganization(); int ipos = 0; parseFields(o, ParserSTEP.SplitLineFields(strDef), ref ipos); return o; }
		protected override string BuildStringSTEP()
		{
			string name = mName;
			if(string.IsNullOrEmpty(name))
				name = mDatabase.Factory.ApplicationDeveloper;
			string str = base.BuildStringSTEP() + "," + mIdentification + ",'" + name + "'," + mDescription + (mRoles.Count == 0 ? ",$" : ",(#" + mRoles[0]);

			for (int icounter = 1; icounter < mRoles.Count; icounter++)
				str += ",#" + mRoles;
			str += (mRoles.Count == 0 ? "" : ")") + (mAddresses.Count == 0 ? ",$" : ",(#" + mAddresses[0]);
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

		public IfcEdge EdgeElement { get { return mDatabase[mEdgeElement] as IfcEdge; } set { mEdgeElement = value.mIndex; } }
		public bool Orientation { get { return mOrientation; } set { mOrientation = value; } }

		internal IfcOrientedEdge() : base() { }
		internal IfcOrientedEdge(DatabaseIfc db, IfcOrientedEdge e) : base(db,e) { EdgeElement = db.Factory.Duplicate( e.EdgeElement) as IfcEdge; mOrientation = e.mOrientation; }
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mEdgeElement) + "," + ParserSTEP.BoolToString(mOrientation); }
	}
	public partial class IfcOuterBoundaryCurve : IfcBoundaryCurve
	{
		internal IfcOuterBoundaryCurve() : base() { }
		internal IfcOuterBoundaryCurve(DatabaseIfc db, IfcOuterBoundaryCurve c) : base(db,c) { }
		internal IfcOuterBoundaryCurve(List<IfcCompositeCurveSegment> segs, IfcSurface surface ) : base(segs,surface) { }
		internal new static IfcOuterBoundaryCurve Parse(string str) { IfcOuterBoundaryCurve b = new IfcOuterBoundaryCurve(); int pos = 0; b.Parse(str, ref pos); return b; }
	}
	public partial class IfcOutlet : IfcFlowTerminal //IFC4
	{
		internal IfcOutletTypeEnum mPredefinedType = IfcOutletTypeEnum.NOTDEFINED;// OPTIONAL : IfcOutletTypeEnum;
		public IfcOutletTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcOutlet() : base() { }
		internal IfcOutlet(DatabaseIfc db, IfcOutlet o) : base(db,o) { mPredefinedType = o.mPredefinedType; }
		public IfcOutlet(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcOutlet s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcOutletTypeEnum)Enum.Parse(typeof(IfcOutletTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcOutlet Parse(string strDef) { IfcOutlet s = new IfcOutlet(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcOutletTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcOutletType : IfcFlowTerminalType
	{
		internal IfcOutletTypeEnum mPredefinedType = IfcOutletTypeEnum.NOTDEFINED;// : IfcOutletTypeEnum; 
		public IfcOutletTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcOutletType() : base() { }
		internal IfcOutletType(DatabaseIfc db, IfcOutletType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcOutletType(DatabaseIfc m, string name, IfcOutletTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcOutletType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcOutletTypeEnum)Enum.Parse(typeof(IfcOutletTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcOutletType Parse(string strDef) { IfcOutletType t = new IfcOutletType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcOwnerHistory : BaseClassIfc
	{ 
		internal int mOwningUser;// : IfcPersonAndOrganization;
		internal int mOwningApplication;// : IfcApplication;
		internal IfcStateEnum mState = IfcStateEnum.NA;// : OPTIONAL IfcStateEnum;
		internal IfcChangeActionEnum mChangeAction;// : IfcChangeActionEnum;
		internal int mLastModifiedDate;// : OPTIONAL IfcTimeStamp;
		internal int mLastModifyingUser;// : OPTIONAL IfcPersonAndOrganization;
		internal int mLastModifyingApplication;// : OPTIONAL IfcApplication;
		internal int mCreationDate;// : IfcTimeStamp; 

		public IfcPersonAndOrganization OwningUser { get { return mDatabase[mOwningUser] as IfcPersonAndOrganization; } set { mOwningUser = value.mIndex; } }
		public IfcApplication OwningApplication { get { return mDatabase[mOwningApplication] as IfcApplication; } set { mOwningApplication = value.mIndex; } }
		public IfcStateEnum State { get { return mState; } set { mState = value; } }
		public IfcChangeActionEnum ChangeAction { get { return mChangeAction; } set { mChangeAction = value; } }
		public int LastModifiedDate { get { return mLastModifiedDate; } set { mLastModifiedDate = value; } }
		public IfcPersonAndOrganization LastModifyingUser { get { return mDatabase[mLastModifyingUser] as IfcPersonAndOrganization; } set { mOwningUser = (value == null ? 0 : value.mIndex); } }
		public IfcApplication LastModifyingApplication { get { return mDatabase[mLastModifyingApplication] as IfcApplication; } set { mLastModifyingApplication = (value == null ? 0 : value.mIndex); } }
		public int CreationDate { get { return mCreationDate; } set { mCreationDate = value; } }

		internal IfcOwnerHistory() : base() { }
		internal IfcOwnerHistory(DatabaseIfc db, IfcOwnerHistory o) : base(db)
		{
			OwningUser = db.Factory.Duplicate(o.OwningUser) as IfcPersonAndOrganization;
			OwningApplication = db.Factory.Duplicate(o.OwningApplication) as IfcApplication;
			mState = o.mState;
			mChangeAction = o.mChangeAction;
			mLastModifiedDate = o.mLastModifiedDate;
			mLastModifyingUser = o.mLastModifyingUser;
			mLastModifyingApplication = o.mLastModifyingApplication;
			mCreationDate = o.mCreationDate;
		}
		public IfcOwnerHistory(IfcPersonAndOrganization po, IfcApplication app, IfcChangeActionEnum ca) : base(po.mDatabase)
		{
			mOwningUser = po.mIndex;
			mOwningApplication = app.mIndex;
			mState = IfcStateEnum.NA;
			mChangeAction = ca;
			TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0);
			mCreationDate = (int)ts.TotalSeconds;
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
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mOwningUser) + "," + ParserSTEP.LinkToString(mOwningApplication) + ",";
			if (mState == IfcStateEnum.NA)
				str += "$";
			else
				str += "." + mState.ToString() + ".";
			return str + ",." + (mDatabase.mRelease == ReleaseVersion.IFC2x3 && mChangeAction == IfcChangeActionEnum.NOTDEFINED ? IfcChangeActionEnum.NOCHANGE : mChangeAction).ToString() + ".," + ParserSTEP.IntOptionalToString(mLastModifiedDate) + ","
				+ ParserSTEP.LinkToString(mLastModifyingUser) + "," + ParserSTEP.LinkToString(mLastModifyingApplication) + "," + ParserSTEP.IntToString(mCreationDate);
		}
		internal static IfcOwnerHistory Parse(string strDef) { IfcOwnerHistory o = new IfcOwnerHistory(); int ipos = 0; parseFields(o, ParserSTEP.SplitLineFields(strDef), ref ipos); return o; }
	}
}
