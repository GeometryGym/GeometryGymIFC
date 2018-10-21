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
	public abstract partial class IfcManifoldSolidBrep : IfcSolidModel //ABSTRACT SUPERTYPE OF(ONEOF(IfcAdvancedBrep, IfcFacetedBrep))
	{
		protected int mOuter;// : IfcClosedShell; 
		public IfcClosedShell Outer { get { return mDatabase[mOuter] as IfcClosedShell; } set { mOuter = value.mIndex; } }

		protected IfcManifoldSolidBrep() : base() { }
		protected IfcManifoldSolidBrep(IfcClosedShell s) : base(s.mDatabase) { Outer = s; }
		protected IfcManifoldSolidBrep(DatabaseIfc db, IfcManifoldSolidBrep b) : base(db,b) { Outer = db.Factory.Duplicate( b.Outer) as IfcClosedShell; }

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			result.AddRange(Outer.Extract<T>());
			return result;
		}
	} 
	[Serializable]
	public partial class IfcMapConversion : IfcCoordinateOperation //IFC4
	{
		internal double mEastings, mNorthings, mOrthogonalHeight;// :  	IfcLengthMeasure;
		internal double mXAxisAbscissa = double.NaN, mXAxisOrdinate = double.NaN, mScale = double.NaN;// 	:	OPTIONAL IfcReal;
		public double Eastings { get { return mEastings; } set { mEastings = value; } }  //IfcLengthMeasure
		public double Northings { get { return mNorthings; } set { mNorthings = value; } }  //IfcLengthMeasure
		public double OrthogonalHeight { get { return mOrthogonalHeight; } set { mOrthogonalHeight = value; } }  //IfcLengthMeasure
		public double XAxisAbscissa { get { return mXAxisAbscissa; } set { mXAxisAbscissa = value; } }  // OPTIONAL  IfcReal
		public double XAxisOrdinate { get { return mXAxisOrdinate; } set { mXAxisOrdinate = value; } }  // OPTIONAL IfcReal
		public double Scale { get { return double.IsNaN( mScale) ? 1 : mScale; } set { mScale = value; } }  // OPTIONAL  IfcReal

		internal IfcMapConversion() : base() { }
		internal IfcMapConversion(DatabaseIfc db, IfcMapConversion c) : base(db,c) { mEastings = c.mEastings; mNorthings = c.mNorthings; mOrthogonalHeight = c.mOrthogonalHeight; mXAxisAbscissa = c.mXAxisAbscissa; mXAxisOrdinate = c.mXAxisOrdinate; mScale = c.mScale; }
		public IfcMapConversion(IfcCoordinateReferenceSystemSelect source, IfcCoordinateReferenceSystem target, double eastings, double northings, double orthogonalHeight)
			: base(source, target)
		{
			mEastings = eastings;
			mNorthings = northings;
			mOrthogonalHeight = orthogonalHeight;
		}
	}
	[Serializable]
	public partial class IfcMappedItem : IfcRepresentationItem
	{
		private int mMappingSource;// : IfcRepresentationMap;
		private int mMappingTarget;// : IfcCartesianTransformationOperator;

		public IfcRepresentationMap MappingSource { get { return mDatabase[mMappingSource] as IfcRepresentationMap; } set { mMappingSource = value.mIndex; value.mMapUsage.Add(this); } }
		public IfcCartesianTransformationOperator MappingTarget { get { return mDatabase[mMappingTarget] as IfcCartesianTransformationOperator; } set { mMappingTarget = value.mIndex;  } }

		internal IfcMappedItem() : base() { }
		internal IfcMappedItem(DatabaseIfc db, IfcMappedItem i) : base(db,i) { MappingSource = db.Factory.Duplicate(i.MappingSource) as IfcRepresentationMap; MappingTarget = db.Factory.Duplicate(i.MappingTarget) as IfcCartesianTransformationOperator; }
		public IfcMappedItem(IfcRepresentationMap source, IfcCartesianTransformationOperator target) : base(source.mDatabase) { MappingSource = source; MappingTarget = target; }
		
		internal override void changeSchema(ReleaseVersion schema) { MappingSource.changeSchema(schema); }
	}
	[Serializable]
	public partial class IfcMaterial : IfcMaterialDefinition, NamedObjectIfc
	{
		private string mName = "";// : IfcLabel; 
		private string mDescription = "$";// : IFC4 OPTIONAL IfcText;
		private string mCategory = "$";// : IFC4 OPTIONAL IfcLabel; 

		//INVERSE
		internal IfcMaterialDefinitionRepresentation mHasRepresentation = null;//	 : 	SET [0:1] OF IfcMaterialDefinitionRepresentation FOR RepresentedMaterial;
		//internal IfcMaterialclassificationRelationship mClassifiedAs = null;//	 : 	SET [0:1] OF IfcMaterialClassificationRelationship FOR classifiedMaterial;
		internal List<IfcMaterialRelationship> mIsRelatedWith = new List<IfcMaterialRelationship>();//	:	SET OF IfcMaterialRelationship FOR RelatedMaterials;
		internal IfcMaterialRelationship mRelatesTo = null;//	:	SET [0:1] OF IfcMaterialRelationship FOR RelatingMaterial;

		public override string Name { get { return ParserIfc.Decode(mName); } set { mName = (string.IsNullOrEmpty(value) ? "UNKNOWN NAME" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Category { get { return (mCategory == "$" ? "" : ParserIfc.Decode(mCategory)); } set { mCategory = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcMaterialDefinitionRepresentation HasRepresentation { get { return mHasRepresentation; } set { mHasRepresentation = value; if (value != null && value.RepresentedMaterial != this) value.RepresentedMaterial = this; } }

		public override IfcMaterial PrimaryMaterial() { return this;  }

		public IfcMaterial() : base() { }
		internal IfcMaterial(DatabaseIfc db, IfcMaterial m) : base(db, m)
		{
			mName = m.mName;
			mDescription = m.mDescription;
			mCategory = m.mCategory;

			if(m.mHasRepresentation != null)
				db.Factory.Duplicate(m.mHasRepresentation);
		}
		public IfcMaterial(DatabaseIfc m, string name) : base(m) { Name = name; }

		internal void associateElement(IfcElement el) { Associate(el); }
		internal void associateElement(IfcElementType eltype) { Associate(eltype); }
		internal void associateElement(IfcStructuralMember memb) { Associate(memb); }

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);;
			if (mHasRepresentation != null)
				result.AddRange( mHasRepresentation.Extract<T>());
			return result;
		}

		internal enum MaterialType { STEEL, CONCRETE, PRECASTCONCRETE, WOOD, OTHER };
		internal MaterialType materialType
		{
			get
			{
				if (mCategory != "$")
				{
					string cat = mCategory.ToUpper();
					if (cat.Contains("STEEL"))
						return MaterialType.STEEL;
					if (cat.Contains("PRECAST"))
						return MaterialType.PRECASTCONCRETE;
					if (cat.Contains("CONCRETE"))
						return MaterialType.CONCRETE;
					if (cat.Contains("WOOD") || cat.Contains("TIMBER"))
						return MaterialType.WOOD;
				}
				if (mName.ToUpper().Contains("STEEL") || char.ToUpper(mName[0]) == 'S')
					return MaterialType.STEEL;
				if (mName.ToUpper().Contains("CONC") || char.ToUpper(mName[0]) == 'N' || char.ToUpper(mName[0]) == 'C')
					return MaterialType.CONCRETE;
				return MaterialType.OTHER;
			}
		}
	}
	[Serializable]
	public partial class IfcMaterialClassificationRelationship : BaseClassIfc
	{
		internal List<int> mMaterialClassifications = new List<int>();// : SET [1:?] OF IfcClassificationNotationSelect;
		internal int mClassifiedMaterial;// : IfcMaterial;
		internal IfcMaterialClassificationRelationship() : base() { }
		//internal IfcMaterialClassificationRelationship(IfcMaterialClassificationRelationship m) : base() { mMaterialClassifications = new List<int>(m.mMaterialClassifications.ToArray()); mClassifiedMaterial = m.mClassifiedMaterial; }
		internal IfcMaterialClassificationRelationship(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcMaterialConstituent : IfcMaterialDefinition //IFC4
	{
		internal string mName = "$";// :	OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText 
		internal int mMaterial;// : IfcMaterial;
		internal double mFraction;//	 :	OPTIONAL IfcNormalisedRatioMeasure;
		internal string mCategory = "$";//	 :	OPTIONAL IfcLabel;  

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcMaterial Material { get { return mDatabase[mMaterial] as IfcMaterial; } set { mMaterial = (value == null ? 0 : value.mIndex); } }
		public double Fraction { get { return mFraction; } set { mFraction = value; } }
		public string Category { get { return (mCategory == "$" ? "" : ParserIfc.Decode(mCategory)); } set { mCategory = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		public override IfcMaterial PrimaryMaterial() { return Material; }
		
		internal IfcMaterialConstituent() : base() { }
		internal IfcMaterialConstituent(DatabaseIfc db, IfcMaterialConstituent m) : base(db, m) { mName = m.mName; mDescription = m.mDescription; Material = db.Factory.Duplicate(m.Material) as IfcMaterial; mFraction = m.mFraction; mCategory = m.mCategory; }
		public IfcMaterialConstituent(string name, string desc, IfcMaterial mat, double fraction, string category)
			: base(mat.mDatabase)
		{
			Name = name;
			Description = desc;
			mMaterial = mat.mIndex;
			mFraction = fraction;
			Category = category;
		}
	}
	[Serializable]
	public partial class IfcMaterialConstituentSet : IfcMaterialDefinition
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText 
		internal List<int> mMaterialConstituents = new List<int>();// LIST [1:?] OF IfcMaterialConstituent;

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcMaterialConstituent> MaterialConstituents { get { return new ReadOnlyCollection<IfcMaterialConstituent>( mMaterialConstituents.ConvertAll(x => mDatabase[x] as IfcMaterialConstituent)); } }

		public override IfcMaterial PrimaryMaterial() { return MaterialConstituents[0].PrimaryMaterial(); }

		internal IfcMaterialConstituentSet() : base() { }
		internal IfcMaterialConstituentSet(DatabaseIfc db, IfcMaterialConstituentSet m) : base(db, m) { m.MaterialConstituents.ToList().ForEach(x => addConstituent( db.Factory.Duplicate(x) as IfcMaterialConstituent)); mName = m.mName; mDescription = m.mDescription; }
		public IfcMaterialConstituentSet(string name, string desc, List<IfcMaterialConstituent> mcs)
			: base(mcs[0].mDatabase)
		{
			Name = name;
			Description = desc;
			mMaterialConstituents = mcs.ConvertAll(x => x.mIndex);
		}
		
		internal void addConstituent(IfcMaterialConstituent constituent) { mMaterialConstituents.Add(constituent.mIndex); }
	}
	[Serializable]
	public abstract partial class IfcMaterialDefinition : BaseClassIfc, IfcObjectReferenceSelect, IfcMaterialSelect, IfcResourceObjectSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcMaterial ,IfcMaterialConstituent ,IfcMaterialConstituentSet ,IfcMaterialLayer ,IfcMaterialProfile ,IfcMaterialProfileSet));
	{
		//INVERSE  
		[NonSerialized] private SET<IfcRelAssociatesMaterial> mAssociatedTo = new SET<IfcRelAssociatesMaterial>();
		private SET<IfcExternalReferenceRelationship> mHasExternalReferences = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		private SET<IfcMaterialProperties> mHasProperties = new SET<IfcMaterialProperties>();
		[NonSerialized] internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public SET<IfcRelAssociatesMaterial> AssociatedTo { get { return mAssociatedTo; } set { mAssociatedTo.Clear(); if (value != null) { mAssociatedTo.CollectionChanged -= mAssociatedTo_CollectionChanged; mAssociatedTo =value; mAssociatedTo.CollectionChanged += mAssociatedTo_CollectionChanged; } } }
		public SET<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } set { mHasExternalReferences.Clear();  if (value != null) { mHasExternalReferences.CollectionChanged -= mHasExternalReferences_CollectionChanged; mHasExternalReferences = value; mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged; } } }
		public SET<IfcMaterialProperties> HasProperties { get { return mHasProperties; } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>( mHasConstraintRelationships); } }

		public abstract string Name { get; set; }
		public virtual IfcMaterial PrimaryMaterial() { return null; }

		protected IfcMaterialDefinition() : base() { }

		protected IfcMaterialDefinition(DatabaseIfc db) : base(db) { }
		protected IfcMaterialDefinition(DatabaseIfc db, IfcMaterialDefinition m) : base(db, m) { }
		protected override void initialize()
		{
			base.initialize();
			mAssociatedTo.CollectionChanged += mAssociatedTo_CollectionChanged;
			mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged;
		}
		private void mAssociatedTo_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelAssociatesMaterial r in e.NewItems)
				{
					if (r.RelatingMaterial != this)
						r.RelatingMaterial = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelAssociatesMaterial r in e.OldItems)
				{
					if (r.RelatingMaterial == this)
						r.RelatingMaterial = null;
				}
			}
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

		public void Associate(IfcDefinitionSelect obj)
		{
			IfcRelAssociatesMaterial associates = (mAssociatedTo.Count == 0 ? new IfcRelAssociatesMaterial(this) : mAssociatedTo[0]);
			associates.RelatedObjects.Add(obj);
		}
		public void Associate(IfcRelAssociatesMaterial associates) { if(!mAssociatedTo.Contains(associates)) mAssociatedTo.Add(associates); }

		public void AddExternalReferenceRelationship(IfcExternalReferenceRelationship referenceRelationship) { mHasExternalReferences.Add(referenceRelationship); }
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }

		public IfcProperty FindProperty(string name)
		{
			foreach (IfcProperty property in mHasProperties.SelectMany(x => x.Properties.Values))
			{
				if (property != null && string.Compare(property.Name, name,true) == 0)
					return property;
			}
			return null;
		}
	}
	[Serializable]
	public partial class IfcMaterialDefinitionRepresentation : IfcProductRepresentation
	{
		internal int mRepresentedMaterial;// : IfcMaterial;
		private LIST<IfcStyledRepresentation> mStyledRepresentations = new LIST<IfcStyledRepresentation>(); 

		public IfcMaterial RepresentedMaterial { get { return mDatabase[mRepresentedMaterial] as IfcMaterial; } set { mRepresentedMaterial = value.mIndex; if (value.mHasRepresentation != this) value.HasRepresentation = this; } }
		public new LIST<IfcStyledRepresentation> Representations { get { return mStyledRepresentations; } set { base.Representations.Clear(); mStyledRepresentations.Clear(); if (value != null) { mStyledRepresentations.CollectionChanged -= mStyledRepresentations_CollectionChanged;  mStyledRepresentations = value; base.Representations.AddRange(value); mStyledRepresentations.CollectionChanged += mStyledRepresentations_CollectionChanged;  } } }

		internal IfcMaterialDefinitionRepresentation() : base() { }
		internal IfcMaterialDefinitionRepresentation(DatabaseIfc db, IfcMaterialDefinitionRepresentation r) : base(db, r)
		{
			RepresentedMaterial = db.Factory.Duplicate(r.RepresentedMaterial) as IfcMaterial;
		}
		public IfcMaterialDefinitionRepresentation(IfcStyledRepresentation representation, IfcMaterial mat) : base(representation) { mRepresentedMaterial = mat.mIndex; mat.mHasRepresentation = this; }
		public IfcMaterialDefinitionRepresentation(List<IfcStyledRepresentation> representations, IfcMaterial mat) : base(representations.ConvertAll(x => x as IfcRepresentation)) { mRepresentedMaterial = mat.mIndex; mat.mHasRepresentation = this; }

		protected override void initialize()
		{
			base.initialize();
			mStyledRepresentations.CollectionChanged += mStyledRepresentations_CollectionChanged;
		}

		private void mStyledRepresentations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcStyledRepresentation r in e.NewItems)
					base.Representations.Add(r);	
			}
			if (e.OldItems != null)
			{
				foreach (IfcStyledRepresentation r in e.OldItems)
					base.Representations.Remove(r);
			}
		}
	}
	[Serializable]
	public partial class IfcMaterialLayer : IfcMaterialDefinition
	{
		internal int mMaterial;// : OPTIONAL IfcMaterial;
		internal double mLayerThickness;// ::	IfcNonNegativeLengthMeasure IFC4Chagne IfcPositiveLengthMeasure;
		internal IfcLogicalEnum mIsVentilated = IfcLogicalEnum.FALSE; // : OPTIONAL IfcLogical; 
		internal string mName = "$";// : OPTIONAL IfcLabel; IFC4
		internal string mDescription = "$";// : OPTIONAL IfcText; IFC4
		internal string mCategory = "$";// : OPTIONAL IfcLabel; IFC4
		internal double mPriority = double.NaN;//	 :	OPTIONAL IfcNormalisedRatioMeasure;

		public IfcMaterial Material { get { return mDatabase[mMaterial] as IfcMaterial; } set { mMaterial = (value == null ? 0 : value.mIndex); } }
		public double LayerThickness { get { return mLayerThickness; } set { mLayerThickness = value; } }
		public IfcLogicalEnum IsVentilated { get { return mIsVentilated; } set { mIsVentilated = value; } }
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Category { get { return (mCategory == "$" ? "" : ParserIfc.Decode(mCategory)); } set { mCategory = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public double Priority { get { return mPriority; } set { mPriority = value; } }

		public override IfcMaterial PrimaryMaterial() { return Material; }

		internal IfcMaterialLayer() : base() { }
		internal IfcMaterialLayer(DatabaseIfc db, IfcMaterialLayer m) : base(db, m) { Material = db.Factory.Duplicate(m.Material) as IfcMaterial; mLayerThickness = m.mLayerThickness; mIsVentilated = m.mIsVentilated; }
		public IfcMaterialLayer(DatabaseIfc db, double thickness, string name) : base(db) { mLayerThickness = Math.Abs(thickness); Name = name; }
		public IfcMaterialLayer(IfcMaterial mat, double thickness, string name) : base(mat.mDatabase)
		{
			Material = mat;
			mLayerThickness = Math.Abs(thickness);
			Name = (string.IsNullOrEmpty(name) ? mat.Name : name);
		}
	}
	[Serializable]
	public partial class IfcMaterialLayerSet : IfcMaterialDefinition
	{
		private List<int> mMaterialLayers = new List<int>();// LIST [1:?] OF IfcMaterialLayer;
		private string mLayerSetName = "$";// : OPTIONAL IfcLabel;
		private string mDescription = "$";// : OPTIONAL IfcText

		public ReadOnlyCollection<IfcMaterialLayer> MaterialLayers { get { return new ReadOnlyCollection<IfcMaterialLayer>(mMaterialLayers.ConvertAll(x => (IfcMaterialLayer)mDatabase[x])); } }
		public string LayerSetName { get { return (mLayerSetName == "$" ? "" : ParserIfc.Decode(mLayerSetName)); } set { mLayerSetName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		public override string Name { get { return LayerSetName; } set { LayerSetName = value; } }
		public override IfcMaterial PrimaryMaterial() { return (mMaterialLayers.Count != 1 ? null : MaterialLayers[0].Material); }
	 	
		internal IfcMaterialLayerSet() : base() { }
		internal IfcMaterialLayerSet(DatabaseIfc db, IfcMaterialLayerSet m) : base(db, m) { m.MaterialLayers.ToList().ForEach(x => addMaterialLayer( db.Factory.Duplicate(x) as IfcMaterialLayer)); mLayerSetName = m.mLayerSetName; mDescription = m.mDescription; }
		protected IfcMaterialLayerSet(DatabaseIfc db) : base(db) { }
		public IfcMaterialLayerSet(IfcMaterialLayer layer, string name) : base(layer.mDatabase) { mMaterialLayers.Add(layer.mIndex); Name = name;  }
		public IfcMaterialLayerSet(List<IfcMaterialLayer> layers, string name) : base(layers[0].mDatabase)
		{
			Name = name;
			mMaterialLayers = layers.ConvertAll(x => x.mIndex);
		}
		
		internal void addMaterialLayer(IfcMaterialLayer layer) { mMaterialLayers.Add(layer.mIndex); }
	}
	[Serializable]
	public partial class IfcMaterialLayerSetUsage : IfcMaterialUsageDefinition
	{
		private int mForLayerSet;// : IfcMaterialLayerSet;
		private IfcLayerSetDirectionEnum mLayerSetDirection = IfcLayerSetDirectionEnum.AXIS1;// : IfcLayerSetDirectionEnum;
		private IfcDirectionSenseEnum mDirectionSense = IfcDirectionSenseEnum.POSITIVE;// : IfcDirectionSenseEnum;
		private double mOffsetFromReferenceLine;// : IfcLengthMeasure;  
		private double mReferenceExtent = double.NaN;//	 : IFC4	OPTIONAL IfcPositiveLengthMeasure;

		public IfcMaterialLayerSet ForLayerSet { get { return mDatabase[mForLayerSet] as IfcMaterialLayerSet; } set { mForLayerSet = value.mIndex; } }
		public IfcLayerSetDirectionEnum LayerSetDirection { get { return mLayerSetDirection; } }
		public IfcDirectionSenseEnum DirectionSense { get { return mDirectionSense; } }
		public double OffsetFromReferenceLine { get { return mOffsetFromReferenceLine; } set { mOffsetFromReferenceLine = value; } }
		public double ReferenceExtent { get { return mReferenceExtent; } }

		public override string Name { get { return ForLayerSet.Name; } }
		public override IfcMaterial PrimaryMaterial() { return ForLayerSet.PrimaryMaterial(); }	
		
		internal IfcMaterialLayerSetUsage() : base() { }
		internal IfcMaterialLayerSetUsage(DatabaseIfc db, IfcMaterialLayerSetUsage m) : base(db, m) { ForLayerSet = db.Factory.Duplicate(m.ForLayerSet) as IfcMaterialLayerSet; mLayerSetDirection = m.mLayerSetDirection; mDirectionSense = m.mDirectionSense; mOffsetFromReferenceLine = m.mOffsetFromReferenceLine; mReferenceExtent = m.mReferenceExtent; }
		public IfcMaterialLayerSetUsage(IfcMaterialLayerSet ls, IfcLayerSetDirectionEnum dir, IfcDirectionSenseEnum sense, double offset) : base(ls.mDatabase)
		{
			mForLayerSet = ls.mIndex;
			mLayerSetDirection = dir;
			mDirectionSense = sense;
			mOffsetFromReferenceLine = offset;
		}
	}
	[Serializable]
	public partial class IfcMaterialLayerSetWithOffsets : IfcMaterialLayerSet
	{
		internal IfcLayerSetDirectionEnum mOffsetDirection = IfcLayerSetDirectionEnum.AXIS1;
		internal double[] mOffsetValues = new double[2];// LIST [1:2] OF IfcLengthMeasure; 

		internal IfcMaterialLayerSetWithOffsets() : base() { }
		internal IfcMaterialLayerSetWithOffsets(DatabaseIfc db, IfcMaterialLayerSetWithOffsets m) : base(db, m) { mOffsetDirection = m.mOffsetDirection; mOffsetValues = m.mOffsetValues.ToArray(); }
		internal IfcMaterialLayerSetWithOffsets(IfcMaterialLayer layer, string name, IfcLayerSetDirectionEnum dir, double offset)
			: base(layer, name) { mOffsetDirection = dir; mOffsetValues[0] = offset; }
		internal IfcMaterialLayerSetWithOffsets(IfcMaterialLayer layer, string name, IfcLayerSetDirectionEnum dir, double offset1, double offset2)
			: this(layer, name,dir,offset1) { mOffsetValues[1] = offset2; }
		internal IfcMaterialLayerSetWithOffsets(List<IfcMaterialLayer> layers, string name, IfcLayerSetDirectionEnum dir, double offset)
			: base(layers, name) { mOffsetDirection = dir; mOffsetValues[0] = offset; }
		internal IfcMaterialLayerSetWithOffsets(List<IfcMaterialLayer> layers, string name, IfcLayerSetDirectionEnum dir, double offset1,double offset2)
			: this(layers, name,dir,offset1) { mOffsetValues[1] = offset2; }
	}
	[Serializable]
	public partial class IfcMaterialLayerWithOffsets : IfcMaterialLayer
	{
		internal IfcLayerSetDirectionEnum mOffsetDirection = IfcLayerSetDirectionEnum.AXIS1;// : IfcLayerSetDirectionEnum;
		internal double[] mOffsetValues = new double[2];// : ARRAY [1:2] OF IfcLengthMeasure; 

		internal IfcMaterialLayerWithOffsets() : base() { }
		internal IfcMaterialLayerWithOffsets(DatabaseIfc db, IfcMaterialLayerWithOffsets m) : base(db, m) { mOffsetDirection = m.mOffsetDirection; mOffsetValues = m.mOffsetValues.ToArray(); }
		internal IfcMaterialLayerWithOffsets(IfcMaterial mat, double thickness, string name, IfcLayerSetDirectionEnum d, double startOffset)
			: base(mat, thickness, name) { mOffsetDirection = d; mOffsetValues = new double[]{ startOffset}; }
		internal IfcMaterialLayerWithOffsets(IfcMaterial mat, double thickness, string name, IfcLayerSetDirectionEnum d, double startOffset,double endOffset)
			: base(mat, thickness, name) { mOffsetDirection = d; mOffsetValues = new double[]{ startOffset,endOffset}; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcMaterialList : BaseClassIfc, IfcMaterialSelect, NamedObjectIfc //DEPRECEATED IFC4
	{
		internal List<int> mMaterials = new List<int>();// LIST [1:?] OF IfcMaterial; 
		//INVERSE
		[NonSerialized] private SET<IfcRelAssociatesMaterial> mAssociatedTo = new SET<IfcRelAssociatesMaterial>();

		public ReadOnlyCollection<IfcMaterial> Materials { get { return new ReadOnlyCollection<IfcMaterial>( mMaterials.ConvertAll(x =>mDatabase[x] as IfcMaterial)); } }
		public SET<IfcRelAssociatesMaterial> AssociatedTo { get { return mAssociatedTo; } set { mAssociatedTo.Clear(); if (value != null) { mAssociatedTo.CollectionChanged -= mAssociatedTo_CollectionChanged; mAssociatedTo =value; mAssociatedTo.CollectionChanged += mAssociatedTo_CollectionChanged; } } }

		public string Name { get { return "MaterialList"; } }
		public IfcMaterial PrimaryMaterial() {  return mDatabase[mMaterials[0]] as IfcMaterial; }

		internal IfcMaterialList() : base() { }
		internal IfcMaterialList(DatabaseIfc db, IfcMaterialList m) : base(db) { m.Materials.ToList().ForEach(x=>addMaterial( db.Factory.Duplicate(x) as IfcMaterial)); }
		protected override void initialize()
		{
			base.initialize();
			mAssociatedTo.CollectionChanged += mAssociatedTo_CollectionChanged;
		}
		private void mAssociatedTo_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelAssociatesMaterial r in e.NewItems)
				{
					if (r.RelatingMaterial != this)
						r.RelatingMaterial = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelAssociatesMaterial r in e.OldItems)
				{
					if (r.RelatingMaterial == this)
						r.RelatingMaterial = null;
				}
			}
		}

		public void Associate(IfcDefinitionSelect obj)
		{
			IfcRelAssociatesMaterial associates = (mAssociatedTo.Count == 0 ? new IfcRelAssociatesMaterial(this) : mAssociatedTo[0]);
			associates.RelatedObjects.Add(obj);
		}
		public void Associate(IfcRelAssociatesMaterial associates) { if(!mAssociatedTo.Contains(associates)) mAssociatedTo.Add(associates); }

		internal void addMaterial(IfcMaterial material) { mMaterials.Add(material.mIndex); }
		internal override void changeSchema(ReleaseVersion schema)
		{
			IfcMaterialConstituentSet mcs = new IfcMaterialConstituentSet("", "", Materials.ToList().ConvertAll(x => new IfcMaterialConstituent(x.Name, "", x, 0, "")));
			mDatabase[mcs.mIndex] = null;
			mcs.mIndex = mIndex;
			mDatabase[mIndex] = mcs;
			base.changeSchema(schema);
		}
	}
	[Serializable]
	public partial class IfcMaterialProfile : IfcMaterialDefinition // IFC4
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal IfcMaterial mMaterial = null;// : OPTIONAL IfcMaterial;
		internal IfcProfileDef mProfile = null;// : OPTIONAL IfcProfileDef;
		internal int mPriority = int.MaxValue;// : OPTIONAL IfcInteger [0..100] was  IfcNormalisedRatioMeasure
		internal string mCategory = "$";// : OPTIONAL IfcLabel
		// INVERSE
		private IfcMaterialProfileSet mToMaterialProfileSet = null;// : IfcMaterialProfileSet FOR 
		
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcMaterial Material { get { return mMaterial; } set { mMaterial = value; } }
		public IfcProfileDef Profile { get { return mProfile; } set { mProfile = value; } }
		public string Category { get { return (mCategory == "$" ? "" : ParserIfc.Decode(mCategory)); } set { mCategory = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public int Priority { get { return mPriority; } set { mPriority = (value >= 0 && value <= 100 ? value : int.MaxValue); } }
		public IfcMaterialProfileSet ToMaterialProfileSet { get { return mToMaterialProfileSet; } set { mToMaterialProfileSet = value; } }

		public override IfcMaterial PrimaryMaterial() { return Material; }

		internal IfcMaterialProfile() : base() { }
		internal IfcMaterialProfile(DatabaseIfc db, IfcMaterialProfile p) : base(db, p)
		{
			mName = p.mName;
			mDescription = p.mDescription;
			if (p.mMaterial != null)
				Material = db.Factory.Duplicate(p.Material) as IfcMaterial;
			if (p.mProfile != null)
				Profile = db.Factory.Duplicate(p.Profile) as IfcProfileDef;
			mPriority = p.mPriority;
			mCategory = p.mCategory;
		}
		public IfcMaterialProfile(DatabaseIfc db) : base(db) { }
		public IfcMaterialProfile(string name, IfcMaterial mat, IfcProfileDef p) : base(mat == null ? p.mDatabase : mat.mDatabase)
		{
			Name = name;
			Material = mat;
			Profile = p;
		}
	}
	[Serializable]
	public partial class IfcMaterialProfileSet : IfcMaterialDefinition //IFC4
	{
		internal string mName = "$"; //: OPTIONAL IfcLabel;
		internal string mDescription = "$"; //: OPTIONAL IfcText; 
		internal LIST<IfcMaterialProfile> mMaterialProfiles = new LIST<IfcMaterialProfile>();// LIST [1:?] OF IfcMaterialProfile;
		internal IfcCompositeProfileDef mCompositeProfile = null;// : OPTIONAL IfcCompositeProfileDef; 

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public LIST<IfcMaterialProfile> MaterialProfiles { get { return mMaterialProfiles; } set { mMaterialProfiles.Clear();  if(value != null) mMaterialProfiles = value; } }
		public IfcCompositeProfileDef CompositeProfile { get { return mCompositeProfile; } set { mCompositeProfile = value; } }

		public override IfcMaterial PrimaryMaterial() { return (mMaterialProfiles.Count != 1 ? null : MaterialProfiles[0].Material); }

		//GeomGym
		internal IfcMaterialProfileSet mTaperEnd = null;

		internal IfcMaterialProfileSet() : base() { }
		internal IfcMaterialProfileSet(DatabaseIfc db, IfcMaterialProfileSet m) : base(db, m) { mName = m.mName; mDescription = m.mDescription; MaterialProfiles.AddRange(m.mMaterialProfiles.ConvertAll(x => db.Factory.Duplicate(x) as IfcMaterialProfile)); if (m.mCompositeProfile != null) CompositeProfile = db.Factory.Duplicate(m.CompositeProfile) as IfcCompositeProfileDef; }
		private IfcMaterialProfileSet(DatabaseIfc m, string name) : base(m) { Name = name; }
		public IfcMaterialProfileSet(string name, IfcMaterialProfile profile) : base(profile.mDatabase)
		{
			Name = name;
			
			if (profile.ToMaterialProfileSet == null)
				profile.ToMaterialProfileSet = this;
			else
				throw new Exception("Material Profile can be assigned to only a single profile set");
			mMaterialProfiles.Add(profile);
		}
		public IfcMaterialProfileSet(string name, List<IfcMaterialProfile> profiles) : base(profiles[0].mDatabase)
		{
			List<IfcProfileDef> defs = new List<IfcProfileDef>(profiles.Count);
			for (int icounter = 0; icounter < profiles.Count; icounter++)
			{
				IfcMaterialProfile mp = profiles[icounter];
				if (mp.ToMaterialProfileSet == null)
					mp.ToMaterialProfileSet = this;
				else
					throw new Exception("Material Profile can be assigned to only a single profile set");
				mMaterialProfiles.Add(mp);
				if (mp.mProfile != null)
					defs.Add(mp.mProfile);
			}
			if (defs.Count > 0)
				CompositeProfile = new IfcCompositeProfileDef(name, defs);
		}
	}
	[Serializable]
	public partial class IfcMaterialProfileSetUsage : IfcMaterialUsageDefinition //IFC4
	{
		internal IfcMaterialProfileSet mForProfileSet;// : IfcMaterialProfileSet;
		internal IfcCardinalPointReference mCardinalPoint = IfcCardinalPointReference.MID;// 	:	OPTIONAL IfcCardinalPointReference;
		internal double mReferenceExtent = double.NaN;// 	:	OPTIONAL IfcPositiveLengthMeasure;  

		public IfcMaterialProfileSet ForProfileSet { get { return mForProfileSet; } set { mForProfileSet = value; } }
		public IfcCardinalPointReference CardinalPoint { get { return mCardinalPoint; } set { mCardinalPoint = value; } }
		public double ReferenceExtent { get { return mReferenceExtent; } set { mReferenceExtent = value; } }

		public override string Name { get { return ForProfileSet.Name; } }
		public override IfcMaterial PrimaryMaterial() { return ForProfileSet.PrimaryMaterial(); }

		internal IfcMaterialProfileSetUsage() : base() { }
		internal IfcMaterialProfileSetUsage(DatabaseIfc db, IfcMaterialProfileSetUsage m) : base(db,m) { ForProfileSet = db.Factory.Duplicate( m.ForProfileSet) as IfcMaterialProfileSet; mCardinalPoint = m.mCardinalPoint; mReferenceExtent = m.mReferenceExtent; }
		public IfcMaterialProfileSetUsage(IfcMaterialProfile profile) : base(profile.mDatabase) { ForProfileSet = new IfcMaterialProfileSet(profile.Name, profile); }
		public IfcMaterialProfileSetUsage(IfcMaterialProfileSet ps) 
			: base(ps.mDatabase) { ForProfileSet = ps; }
		public IfcMaterialProfileSetUsage(IfcMaterialProfileSet ps, IfcCardinalPointReference ip)
			: this(ps) { mCardinalPoint = ip; }
	}
	[Serializable]
	public partial class IfcMaterialProfileSetUsageTapering : IfcMaterialProfileSetUsage //IFC4
	{
		internal IfcMaterialProfileSet mForProfileEndSet;// : IfcMaterialProfileSet;
		internal IfcCardinalPointReference mCardinalEndPoint = IfcCardinalPointReference.MID;// : IfcCardinalPointReference 
		 
		public IfcMaterialProfileSet ForProfileEndSet { get { return ForProfileEndSet; } set { mForProfileEndSet = value; } }
		public IfcCardinalPointReference CardinalEndPoint { get { return mCardinalEndPoint; } set { mCardinalEndPoint = value; } }

		internal IfcMaterialProfileSetUsageTapering() : base() { }
		internal IfcMaterialProfileSetUsageTapering(DatabaseIfc db, IfcMaterialProfileSetUsageTapering m) : base(db,m) { ForProfileEndSet = db.Factory.Duplicate(m.ForProfileEndSet) as IfcMaterialProfileSet; }
		public IfcMaterialProfileSetUsageTapering(IfcMaterialProfileSet ps, IfcCardinalPointReference ip, IfcMaterialProfileSet eps, IfcCardinalPointReference eip)
			: base(ps, ip) { mForProfileEndSet = eps; mCardinalEndPoint = eip; }
	}
	[Serializable]
	public partial class IfcMaterialProfileWithOffsets : IfcMaterialProfile //IFC4
	{
		internal double[] mOffsetValues = new double[2];// ARRAY [1:2] OF IfcLengthMeasure;

		internal IfcMaterialProfileWithOffsets() : base() { }
		internal IfcMaterialProfileWithOffsets(DatabaseIfc db, IfcMaterialProfileWithOffsets m) : base(db, m) { mOffsetValues = m.mOffsetValues.ToArray(); }
		public IfcMaterialProfileWithOffsets(string name, IfcMaterial mat, IfcProfileDef p, double startOffset)
			: base(name, mat, p) { mOffsetValues = new double[] { startOffset }; }
		public IfcMaterialProfileWithOffsets(string name, IfcMaterial mat, IfcProfileDef p, double startOffset,double endOffset)
			: base(name, mat, p) { mOffsetValues = new double[] { startOffset,endOffset }; }
	}
	
	[Serializable]
	public partial class IfcMaterialProperties : IfcExtendedProperties //IFC4, IFC2x3 ABSTRACT SUPERTYPE OF (ONE(IfcExtendedMaterialProperties,IfcFuelProperties,IfcGeneralMaterialProperties,IfcHygroscopicMaterialProperties,IfcMechanicalMaterialProperties,IfcOpticalMaterialProperties,IfcProductsOfCombustionProperties,IfcThermalMaterialProperties,IfcWaterProperties));
	{
		internal override string KeyWord { get { return "IfcMaterialProperties"; } }
		private IfcMaterialDefinition mMaterial;// : IfcMaterialDefinition; 
		public IfcMaterialDefinition Material { get { return mMaterial; } set { mMaterial = value; value.HasProperties.Add(this); } }

		internal IfcMaterialProperties() : base() { }
		internal IfcMaterialProperties(DatabaseIfc db, IfcMaterialProperties p) : base(db, p) { Material = db.Factory.Duplicate(p.Material) as IfcMaterialDefinition; }
		protected IfcMaterialProperties(IfcMaterialDefinition mat) : base(mat.mDatabase) { Name = this.GetType().Name; Material = mat; }
		public IfcMaterialProperties(string name, IfcMaterialDefinition mat) : base(mat.mDatabase) { Name = name; Material = mat; }
		internal IfcMaterialProperties(string name, List<IfcProperty> props, IfcMaterialDefinition mat) : base(props) { Name = name; Material = mat; }
	}
	[Serializable]
	public partial class IfcMaterialRelationship : IfcResourceLevelRelationship //IFC4
	{
		internal int mRelatingMaterial;// : IfcMaterial;
		internal List<int> mRelatedMaterials = new List<int>(); //	:	SET [1:?] OF IfcMaterial;
		internal string mExpression = "$";//:	OPTIONAL IfcLabel;
		internal IfcMaterialRelationship() : base() { }
		//internal IfcMaterialRelationship(DatabaseIfc db, IfcMaterialRelationship r) : base(db,r) { mRelatingMaterial = i.mRelatingMaterial; mRelatedMaterials.AddRange(i.mRelatedMaterials); mExpression = i.mExpression; }
		internal IfcMaterialRelationship(IfcMaterial mat, List<IfcMaterial> related, string expr)
			: base(mat.mDatabase) { mRelatingMaterial = mat.mIndex; mRelatedMaterials = related.ConvertAll(x => x.mIndex); if (!string.IsNullOrEmpty(expr)) mExpression = expr.Replace("'", "");  }
		//internal override void relate()
		//{
		//	base.relate();
		//	IfcMaterial m = mModel.mIFCobjs[mRelatingMaterial] as IfcMaterial;
		//	if (m != null)
		//		m.mHasRepresentation = this;
		//}
	}
	public partial interface IfcMaterialSelect : NamedObjectIfc  //IFC4 SELECT (IfcMaterialDefinition, IfcMaterialList, IfcMaterialUsageDefinition);
	{
		void Associate(IfcDefinitionSelect obj);
		SET<IfcRelAssociatesMaterial> AssociatedTo { get; }
		IfcMaterial PrimaryMaterial(); 
	}
	[Serializable]
	public abstract partial class IfcMaterialUsageDefinition : BaseClassIfc, IfcMaterialSelect // ABSTRACT SUPERTYPE OF(ONEOF(IfcMaterialLayerSetUsage, IfcMaterialProfileSetUsage));
	{   //INVERSE 
		[NonSerialized] private SET<IfcRelAssociatesMaterial> mAssociatedTo = new SET<IfcRelAssociatesMaterial>();
		public SET<IfcRelAssociatesMaterial> AssociatedTo { get { return mAssociatedTo; } set { mAssociatedTo.Clear(); if (value != null) { mAssociatedTo.CollectionChanged -= mAssociatedTo_CollectionChanged; mAssociatedTo =value; mAssociatedTo.CollectionChanged += mAssociatedTo_CollectionChanged; } } }

		public abstract string Name { get; }
		public abstract IfcMaterial PrimaryMaterial();

		protected IfcMaterialUsageDefinition() : base() { }
		protected IfcMaterialUsageDefinition(DatabaseIfc db) : base(db) { }
		protected IfcMaterialUsageDefinition(DatabaseIfc db, IfcMaterialUsageDefinition d) : base(db,d) { }

		protected override void initialize()
		{
			base.initialize();
			mAssociatedTo.CollectionChanged += mAssociatedTo_CollectionChanged;
		}
		private void mAssociatedTo_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelAssociatesMaterial r in e.NewItems)
				{
					if (r.RelatingMaterial != this)
						r.RelatingMaterial = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelAssociatesMaterial r in e.OldItems)
				{
					if (r.RelatingMaterial == this)
						r.RelatingMaterial = null;
				}
			}
		}
		public void Associate(IfcDefinitionSelect obj)
		{
			IfcRelAssociatesMaterial associates = (mAssociatedTo.Count == 0 ? new IfcRelAssociatesMaterial(this) : mAssociatedTo[0]);
			associates.RelatedObjects.Add(obj);
		}
		public void Associate(IfcRelAssociatesMaterial associates) { if (!mAssociatedTo.Contains(associates)) mAssociatedTo.Add(associates); }
	}
	[Serializable]
	public partial class IfcMeasureWithUnit : BaseClassIfc, IfcAppliedValueSelect
	{
		internal IfcValue mValueComponent;// : IfcValue; 
		private string mVal;
		private int mUnitComponent;// : IfcUnit; 

		public IfcValue ValueComponent { get { return mValueComponent; } set { mValueComponent = value; } }
		public IfcUnit UnitComponent { get { return mDatabase[mUnitComponent] as IfcUnit; } set { mUnitComponent = value.Index; } }

		internal IfcMeasureWithUnit() : base() { }
		internal IfcMeasureWithUnit(DatabaseIfc db, IfcMeasureWithUnit m) : base(db) { mValueComponent = m.mValueComponent; mVal = m.mVal;  UnitComponent = db.Factory.Duplicate(m.mDatabase[ m.mUnitComponent]) as IfcUnit; }
		public IfcMeasureWithUnit(IfcValue v, IfcUnit u) : base(u.Database) { mValueComponent = v; mUnitComponent = u.Index; }
		internal IfcMeasureWithUnit(double value, IfcUnit u) : base(u.Database) { mValueComponent = new IfcReal(value); mUnitComponent = u.Index;  }
		public double SIFactor()
		{
			IfcUnit u = UnitComponent;
			IfcSIUnit si = u as IfcSIUnit;
			double sif = (si == null ? 1 : si.SIFactor), d = 1;
			if (mValueComponent != null)
			{
				IfcMeasureValue m = mValueComponent as IfcMeasureValue;
				if (m != null)
					return m.Measure * sif;
				IfcDerivedMeasureValue dm = mValueComponent as IfcDerivedMeasureValue;
				if (dm != null)
					return dm.Measure * sif;
				if (double.TryParse(mValueComponent.ValueString, out d))
					return d * sif;
			}
			if (double.TryParse(mVal, out d))
				return d * sif;
			return sif;
		}
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcMechanicalConcreteMaterialProperties : IfcMechanicalMaterialProperties // DEPRECEATED IFC4
	{
		internal double mCompressiveStrength = double.NaN;// : OPTIONAL IfcPressureMeasure;
		internal double mMaxAggregateSize = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal string mAdmixturesDescription = "$", mWorkability = "$";// : OPTIONAL IfcText
		internal double mProtectivePoreRatio = double.NaN;// : OPTIONAL IfcNormalisedRatioMeasure;
		internal string mWaterImpermeability = "$";// : OPTIONAL IfcText; 
		internal IfcMechanicalConcreteMaterialProperties() : base() { }
		internal IfcMechanicalConcreteMaterialProperties(DatabaseIfc db, IfcMechanicalConcreteMaterialProperties p) : base(db,p)
		{
			mCompressiveStrength = p.mCompressiveStrength;
			mMaxAggregateSize = p.mMaxAggregateSize;
			mAdmixturesDescription = p.mAdmixturesDescription;
			mWorkability = p.mWorkability;
			mProtectivePoreRatio = p.mProtectivePoreRatio;
			mWaterImpermeability = p.mWaterImpermeability;
		}
	}
	[Serializable]
	public partial class IfcMechanicalFastener : IfcElementComponent //IFC4 change
	{
		internal double mNominalDiameter = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure; IFC4 depreceated
		internal double mNominalLength = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;  IFC4 depreceated
		internal IfcMechanicalFastenerTypeEnum mPredefinedType = IfcMechanicalFastenerTypeEnum.NOTDEFINED;// : OPTIONAL IfcMechanicalFastenerTypeEnum;
		public IfcMechanicalFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMechanicalFastener() : base() { }
		internal IfcMechanicalFastener(DatabaseIfc db, IfcMechanicalFastener f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mNominalDiameter = f.mNominalDiameter; mNominalLength = f.mNominalLength; }
		public IfcMechanicalFastener(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		public IfcMechanicalFastener(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public partial class IfcMechanicalFastenerType : IfcElementComponentType //IFC4 change
	{
		internal double mNominalDiameter;// : OPTIONAL IfcPositiveLengthMeasure; IFC4
		internal double mNominalLength;// : OPTIONAL IfcPositiveLengthMeasure; IFC4
		internal IfcMechanicalFastenerTypeEnum mPredefinedType = IfcMechanicalFastenerTypeEnum.NOTDEFINED;// : IfcMechanicalFastenerTypeEnum; IFC4
		public IfcMechanicalFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMechanicalFastenerType() : base() { }
		internal IfcShapeRepresentation mProfileRep = null;
		internal IfcMechanicalFastenerType(DatabaseIfc db, IfcMechanicalFastenerType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mNominalDiameter = t.mNominalDiameter; mNominalLength = t.mNominalLength; mPredefinedType = t.mPredefinedType; }
		public IfcMechanicalFastenerType(DatabaseIfc db, string name, IfcMechanicalFastenerTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcMechanicalMaterialProperties : IfcMaterialProperties // DEPRECEATED IFC4
	{
		internal double mDynamicViscosity = double.NaN;// : OPTIONAL IfcDynamicViscosityMeasure;
		internal double mYoungModulus = double.NaN;// : OPTIONAL IfcModulusOfElasticityMeasure;
		internal double mShearModulus = double.NaN;// : OPTIONAL IfcModulusOfElasticityMeasure;
		internal double mPoissonRatio = double.NaN;// : OPTIONAL IfcPositiveRatioMeasure;
		internal double mThermalExpansionCoefficient = double.NaN;// : OPTIONAL IfcThermalExpansionCoefficientMeasure; 

		public double DynamicViscosity { get { return mDynamicViscosity; } set { mDynamicViscosity = value; } }
		public double YoungModulus { get { return mYoungModulus; } set { mYoungModulus = value; } }
		public double ShearModulus { get { return mShearModulus; } set { mShearModulus = value; } }
		public double PoissonRatio { get { return mPoissonRatio; } set { mPoissonRatio = value; } }
		public double ThermalExpansionCoefficient { get { return mThermalExpansionCoefficient; } set { mThermalExpansionCoefficient = value; } }

		internal IfcMechanicalMaterialProperties() : base() { }
		public IfcMechanicalMaterialProperties(IfcMaterial material) : base(material) { }
		internal IfcMechanicalMaterialProperties(DatabaseIfc db, IfcMechanicalMaterialProperties p) : base(db,p) { mDynamicViscosity = p.mDynamicViscosity; mYoungModulus = p.mYoungModulus; mShearModulus = p.mShearModulus; mPoissonRatio = p.mPoissonRatio; mThermalExpansionCoefficient = p.mThermalExpansionCoefficient; }
		public IfcMechanicalMaterialProperties(IfcMaterial mat, double dynVisc, double youngs, double shear, double poisson, double thermalExp)
			: base(mat) { mDynamicViscosity = dynVisc; mYoungModulus = youngs; mShearModulus = shear; mPoissonRatio = poisson; mThermalExpansionCoefficient = thermalExp; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcMechanicalSteelMaterialProperties : IfcMechanicalMaterialProperties // DEPRECEATED IFC4
	{
		internal double mYieldStress = double.NaN, mUltimateStress = double.NaN;// : OPTIONAL IfcPressureMeasure;
		internal double mUltimateStrain = double.NaN;// :  OPTIONAL IfcPositiveRatioMeasure;
		internal double mHardeningModule = double.NaN;// : OPTIONAL IfcModulusOfElasticityMeasure
		internal double mProportionalStress = double.NaN;// : OPTIONAL IfcPressureMeasure;
		internal double mPlasticStrain = double.NaN;// : OPTIONAL IfcPositiveRatioMeasure;
		internal List<int> mRelaxations = new List<int>();// : OPTIONAL SET [1:?] OF IfcRelaxation 

		public double YieldStress { get { return mYieldStress; } set { mYieldStress = value; } }
		public double UltimateStress { get { return mUltimateStress; } set { mUltimateStress = value; } }
		public double HardeningModule { get { return mHardeningModule; } set { mHardeningModule = value; } }
		public double ProportionalStress { get { return mProportionalStress; } set { mProportionalStress = value; } }
		public double PlasticStrain { get { return mPlasticStrain; } set { mPlasticStrain = value; } }
		public List<IfcRelaxation> Relaxations { get { return mRelaxations.ConvertAll(x => mDatabase[x] as IfcRelaxation); } set { mRelaxations = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }

		internal IfcMechanicalSteelMaterialProperties() : base() { }
		internal IfcMechanicalSteelMaterialProperties(DatabaseIfc db, IfcMechanicalSteelMaterialProperties p) : base(db,p)
		{
			mYieldStress = p.mYieldStress;
			mUltimateStress = p.mUltimateStress;
			mUltimateStrain = p.mUltimateStrain;
			mHardeningModule = p.mHardeningModule;
			mProportionalStress = p.mProportionalStress;
			mPlasticStrain = p.mPlasticStrain;
			Relaxations = p.Relaxations.ConvertAll(x=> db.Factory.Duplicate(x) as IfcRelaxation);
		}
		public IfcMechanicalSteelMaterialProperties(IfcMaterial mat) : base(mat) { }
		public IfcMechanicalSteelMaterialProperties(IfcMaterial mat, double dynVisc, double youngs, double shear, double poisson, double thermalExp, double yieldStress, double ultimateStress, double ultimateStrain, double hardeningModule, double proportionalStress, double plasticStrain)
			: base(mat, dynVisc, youngs, shear, poisson, thermalExp)
		{
			mYieldStress = yieldStress;
			mUltimateStress = ultimateStress;
			mUltimateStrain = ultimateStrain;
			mHardeningModule = hardeningModule;
			mProportionalStress = proportionalStress;
			mPlasticStrain = plasticStrain;
		}
	}
	[Serializable]
	public partial class IfcMedicalDevice : IfcFlowTerminal //IFC4
	{
		internal IfcMedicalDeviceTypeEnum mPredefinedType = IfcMedicalDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcMedicalDeviceTypeEnum;
		public IfcMedicalDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMedicalDevice() : base() { }
		internal IfcMedicalDevice(DatabaseIfc db, IfcMedicalDevice d, IfcOwnerHistory ownerHistory, bool downStream) : base(db, d, ownerHistory, downStream) { mPredefinedType = d.mPredefinedType; }
		public IfcMedicalDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcMedicalDeviceType : IfcFlowTerminalType
	{
		internal IfcMedicalDeviceTypeEnum mPredefinedType = IfcMedicalDeviceTypeEnum.NOTDEFINED;// : IfcMedicalDeviceBoxTypeEnum; 
		public IfcMedicalDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMedicalDeviceType() : base() { }
		internal IfcMedicalDeviceType(DatabaseIfc db, IfcMedicalDeviceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcMedicalDeviceType(DatabaseIfc m, string name, IfcMedicalDeviceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcMember : IfcBuildingElement
	{
		internal IfcMemberTypeEnum mPredefinedType = IfcMemberTypeEnum.NOTDEFINED;//: OPTIONAL IfcMemberTypeEnum; 
		public IfcMemberTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMember() : base() { }
		internal IfcMember(DatabaseIfc db, IfcMember m, IfcOwnerHistory ownerHistory, bool downStream) : base(db, m, ownerHistory, downStream) { mPredefinedType = m.mPredefinedType; }
		public IfcMember(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		public IfcMember(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public partial class IfcMemberStandardCase : IfcMember
	{
		internal override string KeyWord { get { return "IfcMember"; } }

		internal IfcMemberStandardCase() : base() { }
		internal IfcMemberStandardCase(DatabaseIfc db, IfcMemberStandardCase m, IfcOwnerHistory ownerHistory, bool downStream) : base(db, m, ownerHistory, downStream) { }
		public IfcMemberStandardCase(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public partial class IfcMemberType : IfcBuildingElementType
	{
		internal IfcMemberTypeEnum mPredefinedType = IfcMemberTypeEnum.NOTDEFINED;
		public IfcMemberTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMemberType() : base() { }
		internal IfcMemberType(DatabaseIfc db, IfcMemberType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcMemberType(string name, IfcMaterialProfileSet ps, IfcMemberTypeEnum type) : base(ps.mDatabase)
		{
			Name = name;
			mPredefinedType = type;
			if (ps.mTaperEnd != null)
				mTapering = ps;
			else
				MaterialSelect = ps;
		}
		public IfcMemberType(DatabaseIfc m, string name, IfcMemberTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcMemberType(string name, IfcMaterialProfile mp, IfcMemberTypeEnum type) : base(mp.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = mp; }
	}
	[Serializable]
	public partial class IfcMetric : IfcConstraint
	{
		internal IfcBenchmarkEnum mBenchMark = IfcBenchmarkEnum.EQUALTO;// : IfcBenchmarkEnum
		internal string mValueSource = "$"; //	 :	OPTIONAL IfcLabel;
		private int mDataValue = 0;// : OPTIONAL IfcMetricValueSelect;
		private IfcValue mDataValueValue = null;
		private int mReferencePath;// :	OPTIONAL IfcReference;

		public IfcBenchmarkEnum BenchMark { get { return mBenchMark; } set { mBenchMark = value; } }
		public string ValueSource { get { return (mValueSource == "$" ? "" : ParserIfc.Decode(mValueSource)); } set { mValueSource = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcMetricValueSelect DataValue
		{
			get
			{
				if (mDataValueValue != null)
					return mDataValueValue;
				return mDatabase[mDataValue] as IfcMetricValueSelect;
			}
			set
			{
				IfcValue ivalue = value as IfcValue;
				if (value != null)
				{
					mDataValueValue = ivalue;
					mDataValue = 0;
				}
				else
				{
					BaseClassIfc baseClass = value as BaseClassIfc;
					mDataValue = baseClass == null ? 0 : baseClass.mIndex;
					mDataValueValue = null;
				}
			}
		}
		public IfcValue Value { get { return mDataValueValue; } set { mDataValueValue = value; } }
		public IfcReference ReferencePath { get { return mDatabase[mReferencePath] as IfcReference; } set { mReferencePath = (value == null ? 0 : value.mIndex); } }

		internal IfcMetric() : base() { }
		internal IfcMetric(DatabaseIfc db, IfcMetric m) : base(db,m)
		{
			mBenchMark = m.mBenchMark;
			mValueSource = m.mValueSource;
			if(mDataValue > 0)
				DataValue = db.Factory.Duplicate(m.mDatabase[ m.mDataValue]) as IfcMetricValueSelect;
			mDataValueValue = m.mDataValueValue;
			if(mReferencePath > 0)
				ReferencePath = db.Factory.Duplicate( m.ReferencePath) as IfcReference;
		}
		public IfcMetric(DatabaseIfc db, string name, IfcConstraintEnum constraint) : base(db, name, constraint) { }
	}
	public interface IfcMetricValueSelect { } //SELECT ( IfcMeasureWithUnit, IfcTable, IfcTimeSeries, IfcAppliedValue, IfcValue, IfcReference);
	[Serializable]
	public partial class IfcMirroredProfileDef : IfcDerivedProfileDef //SUPERTYPE OF(IfcMirroredProfileDef)
	{
		internal override string KeyWord { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcDerivedProfileDef" : base.KeyWord); } }
		internal IfcMirroredProfileDef() : base() { }
		internal IfcMirroredProfileDef(DatabaseIfc db, IfcMirroredProfileDef p) : base(db,p) { }
		public IfcMirroredProfileDef(IfcProfileDef p, string name) : base(p, new IfcCartesianTransformationOperator2D(p.mDatabase) { Axis1 = new IfcDirection(p.mDatabase, -1, 0) },name) { }
	}
	[Serializable]
	public partial class IfcMonetaryUnit : BaseClassIfc, IfcUnit
	{
		internal string mCurrency = "USD";   //: IFC4 change	Ifc2x3 IfcCurrencyEnum 
		public string Currency { get { return mCurrency; } set { mCurrency = value; } }
		
		internal IfcMonetaryUnit() : base() { }
		internal IfcMonetaryUnit(DatabaseIfc db, IfcMonetaryUnit m) : base(db, m) { mCurrency = m.mCurrency; }
		public IfcMonetaryUnit(DatabaseIfc db, string currency) : base(db) { mCurrency = currency; }
	}
	[Serializable]
	public partial class IfcMotorConnection : IfcEnergyConversionDevice //IFC4
	{
		internal IfcMotorConnectionTypeEnum mPredefinedType = IfcMotorConnectionTypeEnum.NOTDEFINED;// OPTIONAL : IfcMotorConnectionTypeEnum;
		public IfcMotorConnectionTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMotorConnection() : base() { }
		internal IfcMotorConnection(DatabaseIfc db, IfcMotorConnection c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { mPredefinedType = c.mPredefinedType; }
		public IfcMotorConnection(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcMotorConnectionType : IfcEnergyConversionDeviceType
	{
		internal IfcMotorConnectionTypeEnum mPredefinedType = IfcMotorConnectionTypeEnum.NOTDEFINED;// : IfcMotorConnectionTypeEnum; 
		public IfcMotorConnectionTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMotorConnectionType() : base() { }
		internal IfcMotorConnectionType(DatabaseIfc db, IfcMotorConnectionType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcMotorConnectionType(DatabaseIfc m, string name, IfcMotorConnectionTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcMove // DEPRECEATED IFC4
}
