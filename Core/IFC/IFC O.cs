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
	public abstract partial class IfcObject : IfcObjectDefinition //ABSTRACT SUPERTYPE OF (ONEOF (IfcActor, IfcControl, IfcGroup, IfcProcess, IfcProduct, IfcProject, IfcResource))
	{
		internal string mObjectType = ""; //: OPTIONAL IfcLabel;
		//INVERSE
		internal IfcRelDefinesByObject mIsDeclaredBy = null;
		internal List<IfcRelDefinesByObject> mDeclares = new List<IfcRelDefinesByObject>();
		internal IfcRelDefinesByType mIsTypedBy = null;

		public string ObjectType { get { return mObjectType; } set { mObjectType = value; } }
		public IfcRelDefinesByType IsTypedBy
		{
			get { return mIsTypedBy; }
			set
			{
				if (mIsTypedBy != null)
					mIsTypedBy.mRelatedObjects.Remove(this);
				if(mIsTypedBy != null)
				{
					if (mIsTypedBy == value)
						return;
					mIsTypedBy.RelatedObjects.Remove(this);
				}
				mIsTypedBy = value;
				if (value != null)
				{
					value.RelatedObjects.Add(this);
				}
			}
		}
		public SET<IfcRelDefinesByProperties> IsDefinedBy { get { return mIsDefinedBy; } }

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
		protected IfcObject(IfcObject obj, bool replace) : base(obj, replace)
		{
			mObjectType = obj.mObjectType;
			IfcTypeObject typeObject = obj.RelatingType();
			if (typeObject != null)
				setRelatingType(typeObject);
			if(replace)
			{
				mIsDeclaredBy = obj.mIsDeclaredBy;
				mIsDefinedBy.AddRange(obj.mIsDefinedBy);
			}

		}
		protected IfcObject(DatabaseIfc db, IfcObject o, DuplicateOptions options) : base(db, o, options)//, bool downStream) : base(db, o, downStream)
		{
			mObjectType = o.mObjectType;
			foreach (IfcRelDefinesByProperties rdp in o.mIsDefinedBy)
			{
				IfcPropertySetDefinition pset = db.Factory.DuplicatePropertySet(rdp.RelatingPropertyDefinition, options);
				if(pset != null)
					pset.RelateObjectDefinition(this);
			}
			
			if(o.mIsTypedBy != null)
				IsTypedBy = db.Factory.Duplicate(o.mIsTypedBy, options) as IfcRelDefinesByType;
		}
		protected IfcObject(DatabaseIfc db) : base(db) { }
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcRelDefinesByProperties rdp in mIsDefinedBy)
				result.AddRange(rdp.Extract<T>());
			return result;
		}
		public override IfcProperty FindProperty(string name) { return FindProperty(name, true); }
		public IfcProperty FindProperty(string name, bool includeRelatedType)
		{
			foreach (IfcPropertySet pset in mIsDefinedBy.Select(x=>x.RelatingPropertyDefinition).OfType<IfcPropertySet>())
			{
				IfcProperty result = pset.FindProperty(name);
				if (result != null)
					return result;
			}
			return (includeRelatedType && mIsTypedBy != null ? mIsTypedBy.RelatingType.FindProperty(name) : null);
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


		public override IfcPropertySetDefinition FindPropertySet(string name) { return FindPropertySet(name, true); }
		public IfcPropertySetDefinition FindPropertySet(string name, bool includeRelatedType)
		{
			foreach(IfcPropertySetDefinition pset in mIsDefinedBy.Select(x=>x.RelatingPropertyDefinition))
			{
				if (string.Compare(pset.Name, name) == 0)
					return pset;
			}
			return (includeRelatedType && mIsTypedBy != null ? mIsTypedBy.RelatingType.FindPropertySet(name) : null);
		}

		public string GetPredefinedType(bool checkRelatingType)
		{
			string result = base.GetPredefinedType();
			if(result == null)
			{
				IfcTypeObject typeObject = RelatingType();
				if (typeObject != null)
					result = typeObject.GetPredefinedType();
			}
			return result;
		}
	}
	[Serializable]
	public abstract partial class IfcObjectDefinition : IfcRoot, IfcDefinitionSelect  //ABSTRACT SUPERTYPE OF (ONEOF ((IfcContext, IfcObject, IfcTypeObject))))
	{	//INVERSE  
		private SET<IfcRelAssigns> mHasAssignments = new SET<IfcRelAssigns>();//	 : 	SET OF IfcRelAssigns FOR RelatedObjects;
		[NonSerialized] internal IfcRelNests mNests = null;//	 :	SET [0:1] OF IfcRelNests FOR RelatedObjects;
		private SET<IfcRelNests> mIsNestedBy = new SET<IfcRelNests>();//	 :	SET OF IfcRelNests FOR RelatingObject;
		internal IfcRelDeclares mHasContext = null;// :	SET [0:1] OF IfcRelDeclares FOR RelatedDefinitions; 
		internal SET<IfcRelAggregates> mIsDecomposedBy = new SET<IfcRelAggregates>();//	 : 	SET OF IfcRelDecomposes FOR RelatingObject;
		[NonSerialized] internal IfcRelAggregates mDecomposes = null;//	 : 	SET [0:1] OF IfcRelDecomposes FOR RelatedObjects; IFC4  IfcRelAggregates
		internal SET<IfcRelAssociates> mHasAssociations = new SET<IfcRelAssociates>();//	 : 	SET OF IfcRelAssociates FOR RelatedObjects;
		internal SET<IfcRelDefinesByProperties> mIsDefinedBy = new SET<IfcRelDefinesByProperties>();

		public SET<IfcRelAssigns> HasAssignments { get { return mHasAssignments; } set { mHasAssignments.Clear(); if (value != null) { mHasAssignments.CollectionChanged -= mHasAssignments_CollectionChanged; mHasAssignments = value; mHasAssignments.CollectionChanged += mHasAssignments_CollectionChanged; } } }
		public IfcRelNests Nests { get { return mNests; } set { if (mNests != null) mNests.mRelatedObjects.Remove(this); mNests = value; if (value != null && !value.mRelatedObjects.Contains(this)) value.mRelatedObjects.Add(this); } }
		public SET<IfcRelNests> IsNestedBy { get { return mIsNestedBy; } set { mIsNestedBy.Clear(); if (value != null) { mIsNestedBy.CollectionChanged -= mIsNestedBy_CollectionChanged; mIsNestedBy = value; mIsNestedBy.CollectionChanged += mIsNestedBy_CollectionChanged; } } }
		public IfcRelDeclares HasContext { get { return mHasContext; } set { mHasContext = value; } }
		public SET<IfcRelAggregates> IsDecomposedBy { get { return mIsDecomposedBy; } }
		public IfcRelAggregates Decomposes
		{
			get { return mDecomposes; } 
			set
			{ 
				if (mDecomposes != null)
					mDecomposes.mRelatedObjects.Remove(this);
				IfcProduct product = this as IfcProduct;
				if (product != null)
				{
					IfcRelContainedInSpatialStructure contained = product.mContainedInStructure;
					if (contained != null)
						contained.RelatedElements.Remove(product);
				}
				mDecomposes = value; 
				if (value != null && !value.mRelatedObjects.Contains(this))
					value.mRelatedObjects.Add(this); 
			} 
		}
		public SET<IfcRelAssociates> HasAssociations { get { return mHasAssociations; } }

		protected IfcObjectDefinition() : base() { }
		protected IfcObjectDefinition(DatabaseIfc db) : base(db) {  }
		protected IfcObjectDefinition(IfcObjectDefinition objectDefinition, bool replace) : base(objectDefinition, replace)
		{
			if(replace)
			{
				//todo
			}
		}
		protected IfcObjectDefinition(DatabaseIfc db, IfcObjectDefinition o, DuplicateOptions options) : base(db, o, options)
		{
			if (options.DuplicateHost)
			{
				foreach(IfcRelAssigns assigns in o.mHasAssignments)
				{
					IfcRelAssigns dup = db.Factory.Duplicate(assigns, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcRelAssigns;
					dup.RelatedObjects.Add(this);
				}
				if (o.mDecomposes != null)
					(db.Factory.Duplicate(o.mDecomposes, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcRelAggregates).RelatedObjects.Add(this);
				if(o.mNests != null)
					(db.Factory.Duplicate(o.mNests, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcRelNests).RelatedObjects.Add(this);
				if (mHasContext != null)
					(db.Factory.Duplicate(mHasContext, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcRelDeclares).RelatedDefinitions.Add(this);	
			}
			foreach (IfcRelAssociates associates in o.mHasAssociations)
			{
				IfcRelAssociates dup = db.Factory.Duplicate(associates, new DuplicateOptions(options) { DuplicateDownstream = true }) as IfcRelAssociates;
				dup.RelatedObjects.Add(this);
			}
			if(options.DuplicateDownstream)
			{
				foreach (IfcRelAggregates rag in o.mIsDecomposedBy)
					mDatabase.Factory.Duplicate(rag, options);
				foreach (IfcRelNests rn in o.mIsNestedBy)
					mDatabase.Factory.Duplicate(rn, options);
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
				o.mNests.RelatedObjects.Remove(o);
			if (mIsNestedBy.Count() == 0)
			{
				IfcRelNests rn = new IfcRelNests(this, o);
			}
			else mIsNestedBy.First().RelatedObjects.Add(o);
		}
		public void AddAggregated(IfcObjectDefinition o)
		{
			if (o.mDecomposes != null)
				o.mDecomposes.mRelatedObjects.Remove(o);
			if (mIsDecomposedBy.Count == 0)
			{
				new IfcRelAggregates(this, o);
			}
			else
				mIsDecomposedBy.First().RelatedObjects.Add(o);
		}
		
		
		internal IfcMaterialSelect RelatedMaterial() { return (mMaterialSelectIFC4 != null ? mMaterialSelectIFC4 : GetMaterialSelect()); }
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
			IfcMaterialSelect materialSelect = material;
			if (mDatabase.mRelease < ReleaseVersion.IFC4)
			{
				List<IfcProfileDef> profileDefs = new List<IfcProfileDef>();
				IfcMaterialProfile profile = material as IfcMaterialProfile;
				if (profile != null)
				{
					materialSelect = profile.Material;
					mMaterialSelectIFC4 = profile;
					IfcProfileDef profileDef = profile.Profile;
					if(profileDef != null)
						profileDefs.Add(profileDef);
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
						materialSelect = profileSet.PrimaryMaterial();
						mMaterialSelectIFC4 = profileSet;
						foreach (IfcMaterialProfile matp in profileSet.MaterialProfiles)
						{
							IfcProfileDef pd = matp.Profile;
							if (pd != null)
								profileDefs.Add(pd);
						}
					}
					else
					{
						//constituentset....
					}
				}
				foreach(IfcProfileDef profileDef in profileDefs)
				{
					if (profileDef.mHasProperties.Count == 0)
					{
						IfcGeneralProfileProperties generalProfileProperties = new IfcGeneralProfileProperties(profileDef);
						generalProfileProperties.mAssociates.RelatedObjects.Add(this);
					}
					else
					{
						if (profileDef.mHasProperties.First().mAssociates == null)
						{
							new IfcRelAssociatesProfileProperties(this, profileDef.mHasProperties.First());
						}
						else
							profileDef.mHasProperties.First().mAssociates.RelatedObjects.Add(this);
					}
				}
			}
			for (int icounter = 0; icounter < mHasAssociations.Count; icounter++)
			{
				IfcRelAssociatesMaterial rm = mHasAssociations.First() as IfcRelAssociatesMaterial;
				if (rm != null)
					rm.RelatedObjects.Remove(this);
			}
			if (materialSelect != null)
			{
				materialSelect.Associate(this);
			}
		}

		public string GetPredefinedType()
		{
			Type type = GetType();
			PropertyInfo pi = type.GetProperty("PredefinedType");
			if (pi != null)
			{
				object obj = pi.GetValue(this);
				if (obj != null)
					return obj.ToString();
			}
			return "";
		}
		public string GetObjectDefinitionType() 
		{
			string result = GetPredefinedType();
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

		internal IfcMaterialLayerSet detectMaterialLayerSet()
		{
			IfcRelAssociatesMaterial associates = HasAssociations.OfType<IfcRelAssociatesMaterial>().FirstOrDefault();
			if (associates == null)
			{
				IfcTypeProduct typeProduct = this as IfcTypeProduct;
				if(typeProduct != null && typeProduct.ObjectTypeOf != null)
				{
					SET<IfcObject> related = typeProduct.ObjectTypeOf.RelatedObjects;
					IfcMaterialLayerSet layerSet = related.First().detectMaterialLayerSet();
					if (layerSet == null)
						return null;
					for(int icounter = 1; icounter < related.Count; icounter++)
					{
						IfcMaterialLayerSet set = related.First().detectMaterialLayerSet();
						if (set == null)
							continue;
						if (!set.isDuplicate(layerSet, mDatabase.Tolerance))
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
	{
		private IfcObjectPlacement mPlacementRelTo = null;// : OPTIONAL IfcObjectPlacement;
		//INVERSE 
		internal SET<IfcProduct> mPlacesObject = new SET<IfcProduct>();// : SET [0:?] OF IfcProduct FOR ObjectPlacement; ifc2x3 [1:?] 
		internal SET<IfcObjectPlacement> mReferencedByPlacements = new SET<IfcObjectPlacement>();// : SET [0:?] OF IfcLocalPlacement FOR PlacementRelTo;
		internal IfcProduct mContainerHost = null;

		public IfcObjectPlacement PlacementRelTo
		{
			get { return mPlacementRelTo; }
			set
			{
				if (mPlacementRelTo != null)
					mPlacementRelTo.mReferencedByPlacements.Remove(this);
				mPlacementRelTo = value;
				if (value != null)
					value.mReferencedByPlacements.Add(this);
			}
		}
		public SET<IfcProduct> PlacesObject { get { return mPlacesObject; } set { mPlacesObject = value; } }
		public SET<IfcObjectPlacement> ReferencedByPlacements { get { return mReferencedByPlacements; } set { mReferencedByPlacements = value; } }

		protected IfcObjectPlacement() : base() { }
		protected IfcObjectPlacement(IfcObjectPlacement placementRelTo) : base(placementRelTo.Database) { PlacementRelTo = placementRelTo; }
		protected IfcObjectPlacement(DatabaseIfc db) : base(db) { }
		protected IfcObjectPlacement(DatabaseIfc db, IfcProduct p) : base(db)
		{
			if (p != null)
			{
				p.ObjectPlacement = this;
				if (!mPlacesObject.Contains(p))
					mPlacesObject.Add(p);
			}
		}
		protected IfcObjectPlacement(DatabaseIfc db, IfcObjectPlacement p) : base(db,p)
		{
			if (p.mPlacementRelTo != null)
				PlacementRelTo = db.Factory.Duplicate(p.mPlacementRelTo) as IfcObjectPlacement;
		}

		internal IfcObjectPlacement Duplicate(DatabaseIfc db)
		{
			IfcObjectPlacement result = DuplicateWorker(db);
			if (result != null)
				return result;
			return db.Factory.Duplicate(this) as IfcObjectPlacement;
		}
		protected virtual IfcObjectPlacement DuplicateWorker(DatabaseIfc db) { return null; }

		internal virtual bool isXYPlane(double tol) { return false; }
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

		protected override bool DisposeWorker(bool children)
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
			return base.DisposeWorker(children);
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
		internal IfcOccupant(DatabaseIfc db, IfcOccupant o, DuplicateOptions options) : base(db, o, options) { mPredefinedType = o.mPredefinedType; }
		public IfcOccupant(IfcActorSelect a, IfcOccupantTypeEnum type) : base(a) { mPredefinedType = type; }
	}
	public abstract partial class IfcOffsetCurve : IfcCurve //ABSTRACT SUPERTYPE OF(ONEOF(IfcOffsetCurve2D, IfcOffsetCurve3D, IfcOffsetCurveByDistances))
	{
		private IfcCurve mBasisCurve;//: IfcCurve;
		public IfcCurve BasisCurve { get { return mBasisCurve; } set { mBasisCurve = value; value.mBasisCurveOfOffsets.Add(this); } }

		protected IfcOffsetCurve() : base() { }
		protected IfcOffsetCurve(DatabaseIfc db, IfcOffsetCurve c, DuplicateOptions options) : base(db, c, options) { BasisCurve = db.Factory.Duplicate(c.BasisCurve) as IfcCurve; }
		protected IfcOffsetCurve(IfcCurve basis) : base(basis.Database) { BasisCurve = basis; }

	}
	[Serializable]
	public partial class IfcOffsetCurve2D : IfcOffsetCurve
	{
		private double mDistance;// : IfcLengthMeasure;
		private IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// : IfcLogical;
		public double Distance {  get { return mDistance; } set { mDistance = value; } }
		public IfcLogicalEnum SelfIntersect { get { return mSelfIntersect; } set { mSelfIntersect = value; } }

		internal IfcOffsetCurve2D() : base() { }
		internal IfcOffsetCurve2D(DatabaseIfc db, IfcOffsetCurve2D c, DuplicateOptions options) : base(db, c, options) { Distance = c.Distance; SelfIntersect = c.SelfIntersect; }
		public IfcOffsetCurve2D(IfcCurve basis, double distance, IfcLogicalEnum selfIntersect) : base(basis) { Distance = distance; SelfIntersect = selfIntersect; }
	}
	[Serializable]
	public partial class IfcOffsetCurve3D : IfcOffsetCurve
	{
		private double mDistance;// : IfcLengthMeasure;
		private IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// : IfcLogical;
		private IfcDirection mRefDirection;// : IfcDirection;
		public double Distance { get { return mDistance; } set { mDistance = value; } }
		public IfcLogicalEnum SelfIntersect { get { return mSelfIntersect; } set { mSelfIntersect = value; } }
		public IfcDirection RefDirection { get { return mRefDirection; } set { mRefDirection = value; } }

		internal IfcOffsetCurve3D() : base() { }
		internal IfcOffsetCurve3D(DatabaseIfc db, IfcOffsetCurve2D c, DuplicateOptions options) : base(db, c, options) { Distance = c.Distance; SelfIntersect = c.SelfIntersect; }
		public IfcOffsetCurve3D(IfcCurve basis, double distance, IfcLogicalEnum selfIntersect, IfcDirection refDirection) : base(basis) { Distance = distance; SelfIntersect = selfIntersect; RefDirection = RefDirection; }
	}
	[Serializable]
	public partial class IfcOffsetCurveByDistances : IfcOffsetCurve
	{
		private LIST<IfcPointByDistanceExpression> mOffsetValues = new LIST<IfcPointByDistanceExpression>();// : LIST[1:?] OF IfcDistanceExpression;
		private string mTag = "";// : OPTIONAL IfcLabel;

		public LIST<IfcPointByDistanceExpression> OffsetValues { get { return mOffsetValues; } set { mOffsetValues = value; } }
		public string Tag { get { return mTag; } set { mTag = value; } }

		internal IfcOffsetCurveByDistances() : base() { }
		internal IfcOffsetCurveByDistances(DatabaseIfc db, IfcOffsetCurveByDistances c, DuplicateOptions options) : base(db, c, options) { OffsetValues.AddRange(c.OffsetValues.Select(x => db.Factory.Duplicate(x) as IfcPointByDistanceExpression)); Tag = c.Tag; }
		public IfcOffsetCurveByDistances(IfcCurve basis, IEnumerable<IfcPointByDistanceExpression> offsets) : base(basis) { OffsetValues.AddRange(offsets); }
	}

	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcOneDirectionRepeatFactor : IfcGeometricRepresentationItem // DEPRECATED IFC4 SUPERTYPE OF	(IfcTwoDirectionRepeatFactor)
	{
		internal int mRepeatFactor;//  : IfcVector 
		public IfcVector RepeatFactor { get { return mDatabase[mRepeatFactor] as IfcVector; } set { mRepeatFactor = value.mIndex; } }

		internal IfcOneDirectionRepeatFactor() : base() { }
		internal IfcOneDirectionRepeatFactor(DatabaseIfc db, IfcOneDirectionRepeatFactor f, DuplicateOptions options) : base(db, f, options) { RepeatFactor = db.Factory.Duplicate(f.RepeatFactor) as IfcVector; }
	}
	[Serializable]
	public partial class IfcOpenCrossProfileDef : IfcProfileDef
	{
		private bool mHorizontalWidths = false; //: IfcBoolean;
		private LIST<double> mWidths = new LIST<double>(); //: LIST[1:?] OF IfcNonNegativeLengthMeasure;
		private LIST<double> mSlopes = new LIST<double>(); //: LIST[1:?] OF IfcPlaneAngleMeasure;
		private LIST<string> mTags = new LIST<string>(); //: OPTIONAL LIST[2:?] OF IfcLabel;

		public bool HorizontalWidths { get { return mHorizontalWidths; } set { mHorizontalWidths = value; } }
		public LIST<double> Widths { get { return mWidths; } set { mWidths = value; } }
		public LIST<double> Slopes { get { return mSlopes; } set { mSlopes = value; } }
		public LIST<string> Tags { get { return mTags; } set { mTags = value; } }

		public IfcOpenCrossProfileDef() : base() { }
		internal IfcOpenCrossProfileDef(DatabaseIfc db, IfcOpenCrossProfileDef openCrossProfileDef, DuplicateOptions options)
		: base(db, openCrossProfileDef, options)
		{
			HorizontalWidths = openCrossProfileDef.HorizontalWidths;
			Widths.AddRange(openCrossProfileDef.Widths);
			Slopes.AddRange(openCrossProfileDef.Slopes);
			Tags.AddRange(openCrossProfileDef.Tags);
		}
		public IfcOpenCrossProfileDef(DatabaseIfc db, string name, bool horizontalWidths, IEnumerable<double> widths, IEnumerable<double> slopes)
			: base(db, name)
		{
			HorizontalWidths = horizontalWidths;
			Widths.AddRange(widths);
			Slopes.AddRange(slopes);
		}
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
		internal IfcOpeningElement(DatabaseIfc db, IfcOpeningElement e, DuplicateOptions options) : base(db, e, options)
		{
			mPredefinedType = e.mPredefinedType;
			if (options.DuplicateDownstream)
			{
				foreach (IfcRelFillsElement fills in e.HasFillings)
					mDatabase.Factory.Duplicate(fills, options);
			}
		}
		internal IfcOpeningElement(DatabaseIfc db) : base(db) { }
		public IfcOpeningElement(IfcElement host, IfcObjectPlacement placement, IfcProductDefinitionShape rep) : base(host.mDatabase)
		{
			if (placement == null)
				ObjectPlacement = new IfcLocalPlacement(host.ObjectPlacement, new IfcAxis2Placement3D(mDatabase.Factory.Origin));
			else
				ObjectPlacement = placement;
			Representation = rep;
			IfcRelVoidsElement rve = new IfcRelVoidsElement(host, this);
		}
	}
	[Serializable]
	public partial class IfcOpeningStandardCase : IfcOpeningElement //IFC4
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcOpeningElement" : base.StepClassName); } }
		internal IfcOpeningStandardCase() : base() { }
		internal IfcOpeningStandardCase(DatabaseIfc db, IfcOpeningStandardCase o, DuplicateOptions options) : base(db, o, options) { }
		public IfcOpeningStandardCase(IfcElement host, IfcObjectPlacement placement, IfcExtrudedAreaSolid eas) : base(host, placement, new IfcProductDefinitionShape(new IfcShapeRepresentation(eas))) { }
		public IfcOpeningStandardCase(DatabaseIfc db, IfcObjectPlacement placement, IfcExtrudedAreaSolid eas) : base(db) { ObjectPlacement = placement; Representation = new IfcProductDefinitionShape(new IfcShapeRepresentation(eas)); }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcOpticalMaterialProperties // DEPRECATED IFC4
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcOrderAction // DEPRECATED IFC4
	[Serializable]
	public partial class IfcOpenShell : IfcConnectedFaceSet, IfcShell
	{
		internal IfcOpenShell() : base() { }
		internal IfcOpenShell(DatabaseIfc db, IfcOpenShell s, DuplicateOptions options) : base(db, s, options) { }
		public IfcOpenShell(IEnumerable<IfcFace> faces) : base(faces) { }
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
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Name
		{
			get { return ParserIfc.Decode(mName); }
			set { mName = (string.IsNullOrEmpty(value) ? "UNKNOWN" : ParserIfc.Encode(value)); }
		}
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public LIST<IfcActorRole> Roles { get { return mRoles; } }
		public LIST<IfcAddress> Addresses { get { return mAddresses; } }
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } set { mHasExternalReference.Clear();  if (value != null) { mHasExternalReference.CollectionChanged -= mHasExternalReference_CollectionChanged; mHasExternalReference = value; mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged; } } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>( mHasConstraintRelationships); } }

		private static string mOrganization;
		public static string Organization
		{
			get
			{
				if (!string.IsNullOrEmpty(mOrganization))
					return mOrganization;
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
			set
			{
				mOrganization = value;
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
	public partial class IfcOrganizationRelationship : IfcResourceLevelRelationship //IFC4
	{
		private int mRelatingOrganization;// :	IfcOrganization;
		private SET<IfcOrganization> mRelatedOrganizations = new SET<IfcOrganization>(); //	:	SET [1:?] OF IfcResourceObjectSelect;

		public IfcOrganization RelatingOrganization { get { return mDatabase[mRelatingOrganization] as IfcOrganization; } set { mRelatingOrganization = value.mIndex; } }
		public SET<IfcOrganization> RelatedOrganizations { get { return mRelatedOrganizations; } set { mRelatedOrganizations.Clear(); if (value != null) { mRelatedOrganizations.CollectionChanged -= mRelatedOrganizations_CollectionChanged; mRelatedOrganizations = value; mRelatedOrganizations.CollectionChanged += mRelatedOrganizations_CollectionChanged; } } }

		internal IfcOrganizationRelationship() : base() { }
		internal IfcOrganizationRelationship(DatabaseIfc db, IfcOrganizationRelationship r) : base(db, r) { RelatingOrganization = db.Factory.Duplicate(r.RelatingOrganization) as IfcOrganization; RelatedOrganizations.AddRange(r.mRelatedOrganizations.ConvertAll(x => db.Factory.Duplicate(x.Database[x.Index]) as IfcOrganization)); }
		public IfcOrganizationRelationship(IfcOrganization relating, IfcOrganization related) : this(relating, new List<IfcOrganization>() { related }) { }
		public IfcOrganizationRelationship(IfcOrganization relating, List<IfcOrganization> related)
			: base(relating.mDatabase) { mRelatingOrganization = relating.mIndex; RelatedOrganizations.AddRange(related); }

		protected override void initialize()
		{
			base.initialize();

			mRelatedOrganizations.CollectionChanged += mRelatedOrganizations_CollectionChanged;
		}
		private void mRelatedOrganizations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				//foreach (IfcResourceObjectSelect r in e.NewItems)
				//{
				//	if (!r.HasExternalReference.Contains(this))
				//		r.HasExternalReference.Add(this);
				//}
			}
			if (e.OldItems != null)
			{
				//foreach (IfcResourceObjectSelect r in e.OldItems)
				//	r.HasExternalReference.Remove(this);
			}
		}
	}
	[Serializable]
	public partial class IfcOrientationExpression : IfcGeometricRepresentationItem
	{
		private IfcDirection mLateralAxisDirection = null; //: IfcDirection;
		private IfcDirection mVerticalAxisDirection = null; //: IfcDirection;

		public IfcDirection LateralAxisDirection { get { return mLateralAxisDirection; } set { mLateralAxisDirection = value; } }
		public IfcDirection VerticalAxisDirection { get { return mVerticalAxisDirection; } set { mVerticalAxisDirection = value; } }

		public IfcOrientationExpression() : base() { }
		public IfcOrientationExpression(DatabaseIfc db, IfcOrientationExpression orientationExpression, DuplicateOptions options) : base(db, orientationExpression, options)
		{
			LateralAxisDirection = db.Factory.Duplicate(orientationExpression.LateralAxisDirection) as IfcDirection;
			VerticalAxisDirection = db.Factory.Duplicate(orientationExpression.VerticalAxisDirection) as IfcDirection;
		}
		public IfcOrientationExpression(IfcDirection lateralAxisDirection, IfcDirection verticalAxisDirection)
			: base(lateralAxisDirection.Database)
		{
			LateralAxisDirection = lateralAxisDirection;
			VerticalAxisDirection = verticalAxisDirection;
		}
	}
	[Serializable]
	public partial class IfcOrientedEdge : IfcEdge
	{
		internal IfcEdge mEdgeElement;// IfcEdge;
		internal bool mOrientation = true; // : BOOL;
		//INVERSE gg
		internal IfcEdgeLoop mOfLoop = null;

		public IfcEdge EdgeElement {
			get { return mEdgeElement; }
			set { if (mEdgeElement != null) mEdgeElement.mOfEdges.Remove(this); mEdgeElement = value; if (value != null) mEdgeElement.mOfEdges.Add(this); } }
		public bool Orientation { get { return mOrientation; } set { mOrientation = value; } }

		internal IfcOrientedEdge() : base() { }
		internal IfcOrientedEdge(DatabaseIfc db, IfcOrientedEdge e, DuplicateOptions options) : base(db, e, options) { EdgeElement = db.Factory.Duplicate( e.EdgeElement) as IfcEdge; mOrientation = e.mOrientation; }
		public IfcOrientedEdge(IfcEdge e, bool sense) : base(e.mDatabase) { EdgeElement = e; mOrientation = sense; }
		public IfcOrientedEdge(IfcVertexPoint a, IfcVertexPoint b) : base(a.mDatabase) { EdgeElement = new IfcEdge(a, b); }

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			result.AddRange(EdgeElement.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public partial class IfcOuterBoundaryCurve : IfcBoundaryCurve
	{
		internal IfcOuterBoundaryCurve() : base() { }
		internal IfcOuterBoundaryCurve(DatabaseIfc db, IfcOuterBoundaryCurve c, DuplicateOptions options) : base(db, c, options) { }
		public IfcOuterBoundaryCurve(List<IfcCompositeCurveSegment> segs, IfcSurface surface ) : base(segs,surface) { }
	}
	[Serializable]
	public partial class IfcOutlet : IfcFlowTerminal //IFC4
	{
		internal IfcOutletTypeEnum mPredefinedType = IfcOutletTypeEnum.NOTDEFINED;// OPTIONAL : IfcOutletTypeEnum;
		public IfcOutletTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcOutlet() : base() { }
		internal IfcOutlet(DatabaseIfc db, IfcOutlet o, DuplicateOptions options) : base(db,o, options) { mPredefinedType = o.mPredefinedType; }
		public IfcOutlet(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcOutletType : IfcFlowTerminalType
	{
		internal IfcOutletTypeEnum mPredefinedType = IfcOutletTypeEnum.NOTDEFINED;// : IfcOutletTypeEnum; 
		public IfcOutletTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcOutletType() : base() { }
		internal IfcOutletType(DatabaseIfc db, IfcOutletType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
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
		private int mCreationDate = 0;// : IfcTimeStamp; 

		public IfcPersonAndOrganization OwningUser { get { return mOwningUser; } set { mOwningUser = value; } }
		public IfcApplication OwningApplication { get { return mOwningApplication; } set { mOwningApplication = value; } }
		public IfcStateEnum State { get { return mState; } set { mState = value; } }
		public IfcChangeActionEnum ChangeAction { get { return mChangeAction; } set { mChangeAction = value; } }
		public DateTime LastModifiedDate { get { return (mLastModifiedDate == int.MinValue ? DateTime.MinValue : DateTime.SpecifyKind(zeroTime().AddSeconds(mLastModifiedDate), DateTimeKind.Utc)); } set { mLastModifiedDate = (value == DateTime.MinValue ? int.MinValue : (int)(value.ToUniversalTime() - zeroTime()).TotalSeconds); } }
		public IfcPersonAndOrganization LastModifyingUser { get { return mLastModifyingUser; } set { mLastModifyingUser = value; } }
		public IfcApplication LastModifyingApplication { get { return mLastModifyingApplication; } set { mLastModifyingApplication = value; } }
		public DateTime CreationDate { get { return DateTime.SpecifyKind( zeroTime().AddSeconds(mCreationDate), DateTimeKind.Utc); } set { mCreationDate = (int)(value.ToUniversalTime() - zeroTime()).TotalSeconds; } }

		private DateTime zeroTime() { return new DateTime(1970, 1, 1, 0, 0, 0); } 
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
			if (action != IfcChangeActionEnum.NOCHANGE && action != IfcChangeActionEnum.NOTDEFINED)
				LastModifiedDate = CreationDate;
		}

		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcOwnerHistory o = e as IfcOwnerHistory;
			if (o == null || !OwningUser.isDuplicate(o.OwningUser, tol) || !OwningApplication.isDuplicate(o.OwningApplication, tol))
				return false;
			if (mState != o.mState || mChangeAction != o.mChangeAction || mLastModifiedDate != o.mLastModifiedDate || mCreationDate != o.mCreationDate)
				return false;
			if (mLastModifyingUser != null)
			{
				if (o.mLastModifyingUser == null || !LastModifyingUser.isDuplicate(o.LastModifyingUser, tol))
					return false;
			}
			else if (o.mLastModifyingUser != null)
				return false;
			if (mLastModifyingApplication != null)
			{
				if (o.mLastModifyingApplication == null || !LastModifyingApplication.isDuplicate(o.LastModifyingApplication, tol))
					return false;
			}
			else if (o.mLastModifyingApplication != null)
				return false;

			return base.isDuplicate(e, tol);
		}
	}
}
