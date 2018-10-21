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
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public abstract partial class IfcObject : IfcObjectDefinition //ABSTRACT SUPERTYPE OF (ONEOF (IfcActor ,IfcControl ,IfcGroup ,IfcProcess ,IfcProduct ,IfcProject ,IfcResource))
	{
		internal string mObjectType = "$"; //: OPTIONAL IfcLabel;
		//INVERSE
		internal IfcRelDefinesByObject mIsDeclaredBy = null;
		internal List<IfcRelDefinesByObject> mDeclares = new List<IfcRelDefinesByObject>();
		internal IfcRelDefinesByType mIsTypedBy = null;
		internal List<IfcRelDefinesByProperties> mIsDefinedBy = new List<IfcRelDefinesByProperties>();

		public string ObjectType { get { return mObjectType == "$" ? "" : ParserIfc.Decode(mObjectType); } set { mObjectType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcRelDefinesByType IsTypedBy
		{
			get { return mIsTypedBy; }
			set
			{
				if (mIsTypedBy != null)
					mIsTypedBy.mRelatedObjects.Remove(this);
				mIsTypedBy = value;
				if (value != null && !value.RelatedObjects.Contains(this))
					value.RelatedObjects.Add(this);
			}
		}
		public ReadOnlyCollection<IfcRelDefinesByProperties> IsDefinedBy { get { return new ReadOnlyCollection<IfcRelDefinesByProperties>(mIsDefinedBy); } }

		public IfcTypeObject RelatingType() { return (mIsTypedBy == null ? null : mIsTypedBy.RelatingType); }
		public void setRelatingType(IfcTypeObject typeObject)
		{
			if (mIsTypedBy != null)
			{
				if (mIsTypedBy.RelatingType == typeObject)
					return;
				mIsTypedBy.mRelatedObjects.Remove(this);
			}
			if (typeObject == null)
				mIsTypedBy = null;
			else //TODO CHECK CLASS NAME MATCHES INSTANCE
			{
				if (typeObject.mObjectTypeOf == null)
					typeObject.mObjectTypeOf = new IfcRelDefinesByType(this, typeObject);
				else if (!typeObject.mObjectTypeOf.RelatedObjects.Contains(this))
					typeObject.mObjectTypeOf.RelatedObjects.Add(this);
			}
		}
		
		protected IfcObject() : base() { }
		protected IfcObject(IfcObject basis) : base(basis) { mObjectType = basis.mObjectType; mIsDeclaredBy = basis.mIsDeclaredBy; mIsTypedBy = basis.mIsTypedBy; mIsDefinedBy = basis.mIsDefinedBy; }
		protected IfcObject(DatabaseIfc db, IfcObject o, IfcOwnerHistory ownerHistory, bool downStream) : base(db, o, ownerHistory,downStream)//, bool downStream) : base(db, o, downStream)
		{
			mObjectType = o.mObjectType;
			foreach (IfcRelDefinesByProperties rdp in o.mIsDefinedBy)
			{
				IfcRelDefinesByProperties drdp = db.Factory.Duplicate(rdp, ownerHistory,false) as IfcRelDefinesByProperties;
				drdp.RelatedObjects.Add(this);
			}
			if(o.mIsTypedBy != null)
				IsTypedBy = db.Factory.Duplicate(o.mIsTypedBy, ownerHistory,false) as IfcRelDefinesByType;
		}
		internal IfcObject(DatabaseIfc db) : base(db) { }
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcRelDefinesByProperties rdp in mIsDefinedBy)
				result.AddRange(rdp.Extract<T>());
			return result;
		}
		internal override void changeSchema(ReleaseVersion schema)
		{
			for (int icounter = 0; icounter < mIsDefinedBy.Count; icounter++)
				mIsDefinedBy[icounter].changeSchema(schema);
			if(mIsTypedBy != null)
				mIsTypedBy.changeSchema(schema);
			base.changeSchema(schema);
		}
		public override IfcProperty FindProperty(string name)
		{
			foreach (IfcPropertySet pset in mIsDefinedBy.ConvertAll(x=>x.RelatingPropertyDefinition).OfType<IfcPropertySet>())
			{
				IfcProperty property = pset[name];
				if (property != null)
					return property;
			}
			return (mIsTypedBy != null ? mIsTypedBy.RelatingType.FindProperty(name) : null);
		}
		public IfcPhysicalQuantity FindQuantity(string name)
		{
			foreach(IfcElementQuantity qset in mIsDefinedBy.ConvertAll(x=>x.RelatingPropertyDefinition).OfType<IfcElementQuantity>())
			{
				IfcPhysicalQuantity quantity = qset[name];
				if (quantity != null)
					return quantity;
			}
			return null; 
		}
		public override void RemoveProperty(IfcProperty property)
		{
			removeProperty(property, IsDefinedBy);	
		}
		public override IfcPropertySetDefinition FindPropertySet(string name)
		{
			foreach(IfcPropertySetDefinition pset in mIsDefinedBy.Select(x=>x.RelatingPropertyDefinition))
			{
				if (string.Compare(pset.Name, name) == 0)
					return pset;
			}
			return null;
		}
	}
	[Serializable]
	public abstract partial class IfcObjectDefinition : IfcRoot, IfcDefinitionSelect  //ABSTRACT SUPERTYPE OF (ONEOF ((IfcContext, IfcObject, IfcTypeObject))))
	{	//INVERSE  
		private SET<IfcRelAssigns> mHasAssignments = new SET<IfcRelAssigns>();//	 : 	SET OF IfcRelAssigns FOR RelatedObjects;
		[NonSerialized] private IfcRelNests mNests = null;//	 :	SET [0:1] OF IfcRelNests FOR RelatedObjects;
		private SET<IfcRelNests> mIsNestedBy = new SET<IfcRelNests>();//	 :	SET OF IfcRelNests FOR RelatingObject;
		internal IfcRelDeclares mHasContext = null;// :	SET [0:1] OF IfcRelDeclares FOR RelatedDefinitions; 
		internal List<IfcRelAggregates> mIsDecomposedBy = new List<IfcRelAggregates>();//	 : 	SET OF IfcRelDecomposes FOR RelatingObject;
		[NonSerialized] internal IfcRelAggregates mDecomposes = null;//	 : 	SET [0:1] OF IfcRelDecomposes FOR RelatedObjects; IFC4  IfcRelAggregates
		internal SET<IfcRelAssociates> mHasAssociations = new SET<IfcRelAssociates>();//	 : 	SET OF IfcRelAssociates FOR RelatedObjects;

		public SET<IfcRelAssigns> HasAssignments { get { return mHasAssignments; } set { mHasAssignments.Clear(); if (value != null) { mHasAssignments.CollectionChanged -= mHasAssignments_CollectionChanged; mHasAssignments = value; mHasAssignments.CollectionChanged += mHasAssignments_CollectionChanged; } } }
		public IfcRelNests Nests { get { return mNests; } set { if (mNests != null) mNests.mRelatedObjects.Remove(mIndex); mNests = value; if (value != null && !value.mRelatedObjects.Contains(mIndex)) value.mRelatedObjects.Add(mIndex); } }
		public SET<IfcRelNests> IsNestedBy { get { return mIsNestedBy; } set { mIsNestedBy.Clear(); if (value != null) { mIsNestedBy.CollectionChanged -= mIsNestedBy_CollectionChanged; mIsNestedBy = value; mIsNestedBy.CollectionChanged += mIsNestedBy_CollectionChanged; } } }
		public IfcRelDeclares HasContext { get { return mHasContext; } set { mHasContext = value; } }
		public ReadOnlyCollection<IfcRelAggregates> IsDecomposedBy { get { return new ReadOnlyCollection<IfcRelAggregates>(mIsDecomposedBy); } }
		public IfcRelAggregates Decomposes { get { return mDecomposes; } set { if (mDecomposes != null) mDecomposes.mRelatedObjects.Remove(mIndex); mDecomposes = value; if (value != null && !value.mRelatedObjects.Contains(mIndex)) value.mRelatedObjects.Add(mIndex); } }
		public SET<IfcRelAssociates> HasAssociations { get { return mHasAssociations; } }

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
		protected IfcObjectDefinition(DatabaseIfc db, IfcObjectDefinition o, IfcOwnerHistory ownerHistory, bool downStream) : base(db, o, ownerHistory)
		{
			foreach(IfcRelAssigns assigns in o.mHasAssignments)
			{
				IfcRelAssigns dup = db.Factory.Duplicate(assigns, ownerHistory, false) as IfcRelAssigns;
				dup.RelatedObjects.Add(this);
			}
			if (o.mDecomposes != null)
				(db.Factory.Duplicate(o.mDecomposes, ownerHistory, false) as IfcRelAggregates).addObject(this);
			foreach (IfcRelAssociates associates in o.mHasAssociations)
			{
				IfcRelAssociates dup = db.Factory.Duplicate(associates, ownerHistory, downStream) as IfcRelAssociates;
				dup.RelatedObjects.Add(this);
			}
			if (mHasContext != null)
				(db.Factory.Duplicate(mHasContext, ownerHistory, false) as IfcRelDeclares).RelatedDefinitions.Add(this);	
			if(downStream)
			{
				foreach (IfcRelAggregates rag in o.mIsDecomposedBy)
					mDatabase.Factory.Duplicate(rag, ownerHistory, true);
				foreach (IfcRelNests rn in o.mIsNestedBy)
					mDatabase.Factory.Duplicate(rn, ownerHistory, true);
			}
		}
		protected override void initialize()
		{
			base.initialize();

			mHasAssignments.CollectionChanged += mHasAssignments_CollectionChanged;
			mIsNestedBy.CollectionChanged += mIsNestedBy_CollectionChanged;
			mHasAssociations.CollectionChanged += mHasAssociations_CollectionChanged;
		}
		private void mHasAssignments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelAssigns r in e.NewItems)
				{
					if (!r.mRelatedObjects.Contains(this))
						r.RelatedObjects.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelAssigns r in e.OldItems)
					r.RelatedObjects.Remove(this);
			}
		}
		private void mIsNestedBy_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelNests r in e.NewItems)
				{
					if (r.RelatingObject != this)
						r.RelatingObject = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelNests r in e.OldItems)
				{
					if (r.RelatingObject == this)
						r.RelatingObject = null;
				}
			}
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

		public void AddNested(IfcObjectDefinition o)
		{
			if (o.mNests != null)
				o.mNests.removeObject(o);
			if (mIsNestedBy.Count() == 0)
			{
				IfcRelNests rn = new IfcRelNests(this, o);
			}
			else mIsNestedBy[0].addRelated(o);
		}
		public bool AddAggregated(IfcObjectDefinition o)
		{
			if (o.mDecomposes != null)
				o.mDecomposes.removeObject(o);
			if (mIsDecomposedBy.Count == 0)
			{
				new IfcRelAggregates(this, o);
				return true;
			}
			else
				return mIsDecomposedBy[0].addObject(o);
		}
		
		internal virtual void relateNested(IfcRelNests n) { mIsNestedBy.Add(n); }
		
		protected virtual IfcMaterialSelect GetMaterialSelect()
		{
			foreach (IfcRelAssociates ra in HasAssociations)
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
			if (mDatabase.mRelease < ReleaseVersion.IFC4)
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
							IfcProfileProperties pp = new IfcProfileProperties(pd);
							pp.mAssociates.RelatedObjects.Add(this);
						}
						else
							pd.mHasProperties[0].mAssociates.RelatedObjects.Add(this);
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
						m = profileSet.PrimaryMaterial();
						mMaterialSelectIFC4 = profileSet;
						foreach (IfcMaterialProfile matp in profileSet.MaterialProfiles)
						{
							IfcProfileDef pd = matp.Profile;
							if (pd != null)
							{
								if (pd.mHasProperties.Count == 0)
								{
									IfcProfileProperties pp = new IfcProfileProperties(pd);
								}
								pd.mHasProperties[0].mAssociates.RelatedObjects.Add(this);
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
					rm.RelatedObjects.Remove(this);
			}
			if (m != null)
			{
				m.Associate(this);
			}
		}

		internal string Particular
		{
			get
			{
				string result = "";
				Type type = GetType();
				PropertyInfo pi = type.GetProperty("PredefinedType");
				if(pi != null)
					result = pi.GetValue(this).ToString();	
				if(string.IsNullOrEmpty(result) || string.Compare("USERDEFINED", result, true) == 0 || string.Compare("NOTDEFINED", result, true) == 0)
				{
					IfcObject obj = this as IfcObject;
					if (obj != null)
						result = obj.ObjectType;
					else
					{
						IfcElementType elementType = this as IfcElementType;
						if (elementType != null)
							result = elementType.ElementType; 
					}
				}
				return result;
			}
		}

		internal IfcMaterialLayerSet detectMaterialLayerSet()
		{
			IfcRelAssociatesMaterial associates = RelatedMaterialAssociation;
			if (associates == null)
			{
				IfcTypeProduct typeProduct = this as IfcTypeProduct;
				if(typeProduct != null && typeProduct.ObjectTypeOf != null)
				{
					SET<IfcObject> related = typeProduct.ObjectTypeOf.RelatedObjects;
					IfcMaterialLayerSet layerSet = related[0].detectMaterialLayerSet();
					if (layerSet == null)
						return null;
					for(int icounter = 1; icounter < related.Count; icounter++)
					{
						IfcMaterialLayerSet set = related[icounter].detectMaterialLayerSet();
						if (set == null)
							continue;
						if (!set.isDuplicate(layerSet))
							return null;
					}
					return layerSet;
				}

				return null;
			}
			IfcMaterialSelect material = associates.RelatingMaterial;
			IfcMaterialLayerSet materialLayerSet = material as IfcMaterialLayerSet;
			if (materialLayerSet != null)
				return materialLayerSet;
			IfcMaterialLayerSetUsage materialLayerSetUsage = material as IfcMaterialLayerSetUsage;
			if (materialLayerSetUsage != null)
				return materialLayerSetUsage.ForLayerSet;
			return null;
		}
		public abstract IfcProperty FindProperty(string name);
		public abstract void RemoveProperty(IfcProperty property);
		protected void removeProperty(IfcProperty property, IEnumerable<IfcRelDefinesByProperties> definesByProperties)
		{
			Dictionary<string, IfcPropertySet> propertySets = new Dictionary<string, IfcPropertySet>();
			foreach (IfcPropertySet pset in property.PartOfPset.OfType<IfcPropertySet>())
				propertySets.Add(pset.GlobalId, pset);
			IfcPropertySet ps = null;
			foreach (IfcRelDefinesByProperties rdp in definesByProperties.ToList())
			{
				IfcPropertySet propertySet = rdp.RelatingPropertyDefinition as IfcPropertySet;
				if (propertySet == null)
					continue;
				if (propertySets.TryGetValue(propertySet.GlobalId, out ps))
				{
					if (rdp.RelatedObjects.Count == 1 && propertySet.DefinesType.Count == 0)
					{
						if (propertySet.HasProperties.Count == 1)
							rdp.Dispose(true);
						else
							property.Dispose(true);
					}
					else
					{
						rdp.RelatedObjects.Remove(this);
						new IfcPropertySet(this, propertySet.Name, propertySet.HasProperties.Values.Where(x => x != property));
					}
					return;
				}
			}
		}
		public abstract IfcPropertySetDefinition FindPropertySet(string name);
		protected override List<T> Extract<T>(Type type)
		{
			// Early implementation, should search for typed objects such as products and type products.  Contact Jon
			// for expanding for more ifc classes
			List<T> result = base.Extract<T>(type);	
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
			foreach(IfcRelAssigns assigns in HasAssignments)
				assigns.changeSchema(schema);
			foreach(IfcRelNests nests in IsNestedBy)
				nests.changeSchema(schema);
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
	[Serializable]
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

		internal virtual bool isXYPlane { get { return false; } }
	}
	[Serializable]
	public partial class IfcObjective : IfcConstraint
	{
		internal List<int> mBenchmarkValues = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcConstraint;
		internal IfcLogicalOperatorEnum mLogicalAggregator = IfcLogicalOperatorEnum.NONE;// : OPTIONAL IfcLogicalOperatorEnum;
		internal IfcObjectiveEnum mObjectiveQualifier = IfcObjectiveEnum.NOTDEFINED;// : 	IfcObjectiveEnum
		internal string mUserDefinedQualifier = "$"; //	:	OPTIONAL IfcLabel;

		public ReadOnlyCollection<IfcConstraint> BenchmarkValues { get { return new ReadOnlyCollection<IfcConstraint>( mBenchmarkValues.ConvertAll(x => mDatabase[x] as IfcConstraint)); } }
		public IfcLogicalOperatorEnum LogicalAggregator { get { return mLogicalAggregator; } set { mLogicalAggregator = value; } }
		public IfcObjectiveEnum ObjectiveQualifier { get { return mObjectiveQualifier; } set { mObjectiveQualifier = value; } }
		public string UserDefinedQualifier { get { return (mUserDefinedQualifier == "$" ? "" : ParserIfc.Decode(mUserDefinedQualifier)); } set { mUserDefinedQualifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcObjective() : base() { }
		internal IfcObjective(DatabaseIfc db, IfcObjective o) : base(db,o) { o.BenchmarkValues.ToList().ForEach(x=>AddBenchmark( db.Factory.Duplicate(x) as IfcConstraint)); mLogicalAggregator = o.mLogicalAggregator;  mObjectiveQualifier = o.mObjectiveQualifier; mUserDefinedQualifier = o.mUserDefinedQualifier; }
		public IfcObjective(IfcConstraint benchmark, string name, IfcConstraintEnum constraint, IfcObjectiveEnum qualifier)
		 	: base(benchmark.mDatabase, name, constraint) { AddBenchmark(benchmark); mObjectiveQualifier = qualifier; }
		public IfcObjective(DatabaseIfc db, string name, IfcConstraintEnum constraint, IfcObjectiveEnum qualifier)
		 	: base(db, name, constraint) { mObjectiveQualifier = qualifier; }

		public override bool Dispose(bool children)
		{
			if (children)
			{
				for (int icounter = 0; icounter < mBenchmarkValues.Count; icounter++)
				{
					BaseClassIfc bc = mDatabase[mBenchmarkValues[icounter]];
					if (bc != null)
						bc.Dispose(true);
				}
			}
			return base.Dispose(children);
		}

		public void AddBenchmark(IfcConstraint benchmark) { mBenchmarkValues.Add(benchmark.mIndex); }
	}
	public interface IfcObjectReferenceSelect : IBaseClassIfc // SELECT (IfcMaterialDefinition, IfcPerson, IfcOrganization, IfcPersonAndOrganization, IfcExternalReference, IfcTimeSeries, IfcAddress, IfcAppliedValue, IfcTable);
	{
		
	}
	[Serializable]
	public partial class IfcOccupant : IfcActor
	{
		internal IfcOccupantTypeEnum mPredefinedType = IfcOccupantTypeEnum.NOTDEFINED;//		:	OPTIONAL IfcOccupantTypeEnum;
		public IfcOccupantTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcOccupant() : base() { }
		internal IfcOccupant(DatabaseIfc db, IfcOccupant o, IfcOwnerHistory ownerHistory, bool downStream) : base(db, o, ownerHistory, downStream) { mPredefinedType = o.mPredefinedType; }
		public IfcOccupant(IfcActorSelect a, IfcOccupantTypeEnum type) : base(a) { mPredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcOffsetCurve : IfcCurve
	{
		private IfcCurve mBasisCurve;//: IfcCurve;

		public IfcCurve BasisCurve { get { return mBasisCurve; } set { mBasisCurve = value; } }

		internal IfcOffsetCurve() : base() { }
		internal IfcOffsetCurve(DatabaseIfc db, IfcOffsetCurve c) : base(db, c) { BasisCurve = db.Factory.Duplicate(c.BasisCurve) as IfcCurve; }
		public IfcOffsetCurve(IfcCurve basis) : base(basis.Database) { BasisCurve = basis; }
	}
	//ENTITY IfcOffsetCurve2D
	//ENTITY IfcOffsetCurve3D
	[Serializable]
	public partial class IfcOffsetCurveByDistances : IfcOffsetCurve
	{
		private LIST<IfcDistanceExpression> mOffsetValues = new LIST<IfcDistanceExpression>();// : LIST[1:?] OF IfcDistanceExpression;>
		private string mTag = "$";// : OPTIONAL IfcLabel;

		public LIST<IfcDistanceExpression> OffsetValues { get { return mOffsetValues; } set { mOffsetValues.Clear(); if (value != null) mOffsetValues = value; } }
		public string Tag { get { return mTag; } set { mTag = value; } }

		internal IfcOffsetCurveByDistances() : base() { }
		internal IfcOffsetCurveByDistances(DatabaseIfc db, IfcOffsetCurveByDistances c) : base(db, c) { BasisCurve = db.Factory.Duplicate(c.BasisCurve) as IfcCurve; }
		public IfcOffsetCurveByDistances(IfcCurve basis) : base(basis) { BasisCurve = basis; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcOneDirectionRepeatFactor : IfcGeometricRepresentationItem // DEPRECEATED IFC4 SUPERTYPE OF	(IfcTwoDirectionRepeatFactor)
	{
		internal int mRepeatFactor;//  : IfcVector 
		public IfcVector RepeatFactor { get { return mDatabase[mRepeatFactor] as IfcVector; } set { mRepeatFactor = value.mIndex; } }

		internal IfcOneDirectionRepeatFactor() : base() { }
		internal IfcOneDirectionRepeatFactor(DatabaseIfc db, IfcOneDirectionRepeatFactor f) : base(db, f) { RepeatFactor = db.Factory.Duplicate(f.RepeatFactor) as IfcVector; }
	}
	[Serializable]
	public partial class IfcOpeningElement : IfcFeatureElementSubtraction //SUPERTYPE OF(IfcOpeningStandardCase)
	{
		internal IfcOpeningElementTypeEnum mPredefinedType = IfcOpeningElementTypeEnum.NOTDEFINED;// :	OPTIONAL IfcOpeningElementTypeEnum; //IFC4
		//INVERSE
		internal List<IfcRelFillsElement> mHasFillings = new List<IfcRelFillsElement>();

		public IfcOpeningElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public ReadOnlyCollection<IfcRelFillsElement> HasFillings { get { return new ReadOnlyCollection<IfcRelFillsElement>( mHasFillings); } }

		internal IfcOpeningElement() : base() { }
		internal IfcOpeningElement(DatabaseIfc db, IfcOpeningElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream)
		{
			mPredefinedType = e.mPredefinedType;
			if (downStream)
				foreach (IfcRelFillsElement fills in e.HasFillings)
					mDatabase.Factory.Duplicate(fills, true);
		}
		internal IfcOpeningElement(DatabaseIfc db) : base(db) { }
		public IfcOpeningElement(IfcElement host, IfcObjectPlacement placement, IfcProductRepresentation rep) : base(host.mDatabase)
		{
			if (placement == null)
				Placement = new IfcLocalPlacement(host.Placement, new IfcAxis2Placement3D(mDatabase.Factory.Origin));
			else
				Placement = placement;
			Representation = rep;
			IfcRelVoidsElement rve = new IfcRelVoidsElement(host, this);
		}
	}
	[Serializable]
	public partial class IfcOpeningStandardCase : IfcOpeningElement //IFC4
	{
		internal override string KeyWord { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcOpeningElement" : base.KeyWord); } }
		internal IfcOpeningStandardCase() : base() { }
		internal IfcOpeningStandardCase(DatabaseIfc db, IfcOpeningStandardCase o, IfcOwnerHistory ownerHistory, bool downStream) : base(db, o, ownerHistory, downStream) { }
		public IfcOpeningStandardCase(IfcElement host, IfcObjectPlacement placement, IfcExtrudedAreaSolid eas) : base(host, placement, new IfcProductDefinitionShape(new IfcShapeRepresentation(eas))) { }
		public IfcOpeningStandardCase(DatabaseIfc db, IfcObjectPlacement placement, IfcExtrudedAreaSolid eas) : base(db) { Placement = placement; Representation = new IfcProductDefinitionShape(new IfcShapeRepresentation(eas)); }
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcOpticalMaterialProperties // DEPRECEATED IFC4
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcOrderAction // DEPRECEATED IFC4
	[Serializable]
	public partial class IfcOpenShell : IfcConnectedFaceSet, IfcShell
	{
		internal IfcOpenShell() : base() { }
		internal IfcOpenShell(DatabaseIfc db, IfcOpenShell s) : base(db,s) { }
	}
	[Serializable]
	public partial class IfcOrganization : BaseClassIfc, IfcActorSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect, NamedObjectIfc
	{
		internal string mIdentification = "$";// : OPTIONAL IfcIdentifier;
		private string mName = "";// : IfcLabel;
		private string mDescription = "$";// : OPTIONAL IfcText;
		private LIST<IfcActorRole> mRoles = new LIST<IfcActorRole>();// : OPTIONAL LIST [1:?] OF IfcActorRole;
		private LIST<IfcAddress> mAddresses = new LIST<IfcAddress>();//: OPTIONAL LIST [1:?] OF IfcAddress; 
													   //INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReferences = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Name
		{
			get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); }
			set { mName = (string.IsNullOrEmpty(value) ? "UNKNOWN" : ParserIfc.Encode(value)); }
		}
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public LIST<IfcActorRole> Roles { get { return mRoles; } }
		public LIST<IfcAddress> Addresses { get { return mAddresses; } }
		public SET<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } set { mHasExternalReferences.Clear();  if (value != null) { mHasExternalReferences.CollectionChanged -= mHasExternalReferences_CollectionChanged; mHasExternalReferences = value; mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged; } } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>( mHasConstraintRelationships); } }

		internal static string Organization
		{
			get
			{
				try
				{
#if (!NETSTANDARD2_0)
					string name = ((string)Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion", "RegisteredOrganization", "")).Replace("'", "");
					if (!string.IsNullOrEmpty(name) && string.Compare(name, "Microsoft", true) != 0 && string.Compare(name, "HP Inc.",true) != 0)
						return name;
#endif
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
			Roles.AddRange(o.Roles.Select(x => db.Factory.Duplicate(x) as IfcActorRole));
			Addresses.AddRange(o.Addresses.Select(x => db.Factory.Duplicate(x) as IfcAddress));
		}
		public IfcOrganization(DatabaseIfc m, string name) : base(m) { Name = name; }
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
	//ENTITY IfcOrganizationRelationship; //optional name
	[Serializable]
	public partial class IfcOrientedEdge : IfcEdge
	{
		internal int mEdgeElement;// IfcEdge;
		internal bool mOrientation = true; // : BOOL;

		public IfcEdge EdgeElement { get { return mDatabase[mEdgeElement] as IfcEdge; } set { mEdgeElement = value.mIndex; } }
		public bool Orientation { get { return mOrientation; } set { mOrientation = value; } }

		internal IfcOrientedEdge() : base() { }
		internal IfcOrientedEdge(DatabaseIfc db, IfcOrientedEdge e) : base(db,e) { EdgeElement = db.Factory.Duplicate( e.EdgeElement) as IfcEdge; mOrientation = e.mOrientation; }
		public IfcOrientedEdge(IfcEdge e, bool sense) : base(e.mDatabase) { mEdgeElement = e.mIndex; mOrientation = sense; }
		public IfcOrientedEdge(IfcVertexPoint a, IfcVertexPoint b) : base(a.mDatabase) { mEdgeElement = new IfcEdge(a, b).mIndex; }
	}
	[Serializable]
	public partial class IfcOuterBoundaryCurve : IfcBoundaryCurve
	{
		internal IfcOuterBoundaryCurve() : base() { }
		internal IfcOuterBoundaryCurve(DatabaseIfc db, IfcOuterBoundaryCurve c) : base(db,c) { }
		public IfcOuterBoundaryCurve(List<IfcCompositeCurveSegment> segs, IfcSurface surface ) : base(segs,surface) { }
	}
	[Serializable]
	public partial class IfcOutlet : IfcFlowTerminal //IFC4
	{
		internal IfcOutletTypeEnum mPredefinedType = IfcOutletTypeEnum.NOTDEFINED;// OPTIONAL : IfcOutletTypeEnum;
		public IfcOutletTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcOutlet() : base() { }
		internal IfcOutlet(DatabaseIfc db, IfcOutlet o, IfcOwnerHistory ownerHistory, bool downStream) : base(db,o, ownerHistory, downStream) { mPredefinedType = o.mPredefinedType; }
		public IfcOutlet(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcOutletType : IfcFlowTerminalType
	{
		internal IfcOutletTypeEnum mPredefinedType = IfcOutletTypeEnum.NOTDEFINED;// : IfcOutletTypeEnum; 
		public IfcOutletTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcOutletType() : base() { }
		internal IfcOutletType(DatabaseIfc db, IfcOutletType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcOutletType(DatabaseIfc m, string name, IfcOutletTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcOwnerHistory : BaseClassIfc
	{ 
		private IfcPersonAndOrganization mOwningUser = null;// : IfcPersonAndOrganization;
		private IfcApplication mOwningApplication = null;// : IfcApplication;
		private IfcStateEnum mState = IfcStateEnum.NOTDEFINED;// : OPTIONAL IfcStateEnum;
		private IfcChangeActionEnum mChangeAction;// : IfcChangeActionEnum;
		private int mLastModifiedDate = int.MinValue;// : OPTIONAL IfcTimeStamp;
		private IfcPersonAndOrganization mLastModifyingUser;// : OPTIONAL IfcPersonAndOrganization;
		private IfcApplication mLastModifyingApplication;// : OPTIONAL IfcApplication;
		private int mCreationDate;// : IfcTimeStamp; 

		public IfcPersonAndOrganization OwningUser { get { return mOwningUser; } set { mOwningUser = value; } }
		public IfcApplication OwningApplication { get { return mOwningApplication; } set { mOwningApplication = value; } }
		public IfcStateEnum State { get { return mState; } set { mState = value; } }
		public IfcChangeActionEnum ChangeAction { get { return mChangeAction; } set { mChangeAction = value; } }
		public DateTime LastModifiedDate { get { return (mLastModifiedDate == int.MinValue ? DateTime.MinValue : DateTime.SpecifyKind(zeroTime.AddSeconds(mLastModifiedDate), DateTimeKind.Utc)); } set { mLastModifiedDate = (value == DateTime.MinValue ? int.MinValue : (int)(value.ToUniversalTime() - zeroTime).TotalSeconds); } }
		public IfcPersonAndOrganization LastModifyingUser { get { return mLastModifyingUser; } set { mLastModifyingUser = value; } }
		public IfcApplication LastModifyingApplication { get { return mLastModifyingApplication; } set { mLastModifyingApplication = value; } }
		public DateTime CreationDate { get { return DateTime.SpecifyKind( zeroTime.AddSeconds(mCreationDate), DateTimeKind.Utc); } set { mCreationDate = (int)(value.ToUniversalTime() - zeroTime).TotalSeconds; } } 

		private DateTime zeroTime = new DateTime(1970, 1, 1, 0, 0, 0);
		internal IfcOwnerHistory() : base() { }
		internal IfcOwnerHistory(DatabaseIfc db, IfcOwnerHistory o) : base(db, o)
		{
			OwningUser = db.Factory.Duplicate(o.OwningUser) as IfcPersonAndOrganization;
			OwningApplication = db.Factory.Duplicate(o.OwningApplication) as IfcApplication;
			mState = o.mState;
			mChangeAction = o.mChangeAction;
			mLastModifiedDate = o.mLastModifiedDate;
			if(o.mLastModifyingUser != null)
				LastModifyingUser = db.Factory.Duplicate( o.LastModifyingUser) as IfcPersonAndOrganization;
			if (o.mLastModifyingApplication != null)
				LastModifyingApplication = db.Factory.Duplicate(o.LastModifyingApplication) as IfcApplication;
			mCreationDate = o.mCreationDate;
		}
		public IfcOwnerHistory(IfcPersonAndOrganization owningUser, IfcApplication owningApplication, IfcChangeActionEnum action) : base(owningUser.mDatabase)
		{
			OwningUser = owningUser;
			OwningApplication = owningApplication;
			mChangeAction = action;
			CreationDate = DateTime.Now;
		}

		internal override bool isDuplicate(BaseClassIfc e)
		{
			IfcOwnerHistory o = e as IfcOwnerHistory;
			if (o == null || !OwningUser.isDuplicate(o.OwningUser) || !OwningApplication.isDuplicate(o.OwningApplication))
				return false;
			if (mState != o.mState || mChangeAction != o.mChangeAction || mLastModifiedDate != o.mLastModifiedDate || mCreationDate != o.mCreationDate)
				return false;
			if (mLastModifyingUser != null)
			{
				if (o.mLastModifyingUser == null || !LastModifyingUser.isDuplicate(o.LastModifyingUser))
					return false;
			}
			else if (o.mLastModifyingUser != null)
				return false;
			if (mLastModifyingApplication != null)
			{
				if (o.mLastModifyingApplication == null || !LastModifyingApplication.isDuplicate(o.LastModifyingApplication))
					return false;
			}
			else if (o.mLastModifyingApplication != null)
				return false;

			return base.isDuplicate(e);
		}
	}
}
