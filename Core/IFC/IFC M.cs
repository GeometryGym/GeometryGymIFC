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
using System.Reflection;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public abstract partial class IfcManifoldSolidBrep : IfcSolidModel //ABSTRACT SUPERTYPE OF(ONEOF(IfcAdvancedBrep, IfcFacetedBrep))
	{
		protected IfcClosedShell mOuter;// : IfcClosedShell; 
		public IfcClosedShell Outer { get { return mOuter; } set { mOuter = value; } }

		protected IfcManifoldSolidBrep() : base() { }
		protected IfcManifoldSolidBrep(IfcClosedShell s) : base(s.mDatabase) { Outer = s; }
		protected IfcManifoldSolidBrep(DatabaseIfc db, IfcManifoldSolidBrep b, DuplicateOptions options) : base(db, b, options) { Outer = db.Factory.Duplicate(b.Outer, options) as IfcClosedShell; }

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			result.AddRange(Outer.Extract<T>());
			return result;
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4)]
	public partial class IfcMapConversion : IfcCoordinateOperation 
	{
		internal double mEastings, mNorthings, mOrthogonalHeight;// :  	IfcLengthMeasure;
		internal double mXAxisAbscissa = double.NaN, mXAxisOrdinate = double.NaN, mScale = double.NaN, mScaleY = double.NaN, mScaleZ = double.NaN;// 	:	OPTIONAL IfcReal;
		public double Eastings { get { return mEastings; } set { mEastings = value; } }  //IfcLengthMeasure
		public double Northings { get { return mNorthings; } set { mNorthings = value; } }  //IfcLengthMeasure
		public double OrthogonalHeight { get { return mOrthogonalHeight; } set { mOrthogonalHeight = value; } }  //IfcLengthMeasure
		public double XAxisAbscissa { get { return mXAxisAbscissa; } set { mXAxisAbscissa = value; } }  // OPTIONAL  IfcReal
		public double XAxisOrdinate { get { return mXAxisOrdinate; } set { mXAxisOrdinate = value; } }  // OPTIONAL IfcReal
		public double Scale { get { return double.IsNaN(mScale) ? 1 : mScale; } set { mScale = value; } }  // OPTIONAL  IfcReal
		public double ScaleY { get { return double.IsNaN(mScaleY) ? 1 : mScaleY; } set { mScaleY = value; } }  // OPTIONAL  IfcReal
		public double ScaleZ { get { return double.IsNaN(mScaleZ) ? 1 : mScaleZ; } set { mScaleZ = value; } }  // OPTIONAL  IfcReal

		internal IfcMapConversion() : base() { }
		internal IfcMapConversion(DatabaseIfc db, IfcMapConversion c) : base(db, c) { mEastings = c.mEastings; mNorthings = c.mNorthings; mOrthogonalHeight = c.mOrthogonalHeight; mXAxisAbscissa = c.mXAxisAbscissa; mXAxisOrdinate = c.mXAxisOrdinate; mScale = c.mScale; mScaleY = c.mScaleY; mScaleZ = c.mScaleY; }
		public IfcMapConversion(IfcCoordinateReferenceSystemSelect source, IfcCoordinateReferenceSystem target, double eastings, double northings, double orthogonalHeight)
			: base(source, target)
		{
			mEastings = eastings;
			mNorthings = northings;
			mOrthogonalHeight = orthogonalHeight;
		}
		public override IfcCoordinateOperation CreateDuplicate(IfcCoordinateReferenceSystemSelect source)
		{
			var mapConversion = new IfcMapConversion(source, TargetCRS, mEastings, mNorthings, mOrthogonalHeight);
			mapConversion.setOptionalAttributes(this);
			return mapConversion;
		}
		protected void setOptionalAttributes(IfcMapConversion toDuplicate)
		{ 
			mXAxisAbscissa = toDuplicate.mXAxisAbscissa;
			mXAxisOrdinate = toDuplicate.mXAxisOrdinate;
			mScale = toDuplicate.mScale;
			mScaleY = toDuplicate.mScaleY;
			mScaleZ = toDuplicate.mScaleZ;
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3_ADD2)]
	public partial class IfcMapConversionScaled : IfcMapConversion
	{
		public override string StepClassName { get { return (mDatabase != null && mDatabase.mRelease < ReleaseVersion.IFC4X3_ADD2 ? "IfcMapConversion" : base.StepClassName); } }
		internal double mFactorX = 1; //: IfcReal;
		internal double mFactorY = 1; //: IfcReal;
		internal double mFactorZ = 1; //: IfcReal;
		public double FactorX { get { return mFactorX; } set { mFactorX = value; } }  //: IfcReal
		public double FactorY { get { return mFactorY; } set { mFactorY = value; } }  //: IfcReal
		public double FactorZ { get { return mFactorZ; } set { mFactorZ = value; } }  //: IfcReal
		internal IfcMapConversionScaled() : base() { }
		internal IfcMapConversionScaled(DatabaseIfc db, IfcMapConversionScaled c) : base(db, c) 
		{
			mFactorX = c.mFactorX;
			mFactorY = c.mFactorY;
			mFactorZ = c.mFactorZ;
		}
		public IfcMapConversionScaled(IfcCoordinateReferenceSystemSelect source, IfcCoordinateReferenceSystem target, 
			double eastings, double northings, double orthogonalHeight, double factorX, double factorY, double factorZ)
			: base(source, target, eastings, northings, orthogonalHeight)
		{
			mFactorX = factorX;
			mFactorY = factorY;
			mFactorZ = factorZ;
		}
		public override IfcCoordinateOperation CreateDuplicate(IfcCoordinateReferenceSystemSelect source)
		{
			var mapConversion = new IfcMapConversionScaled(source, TargetCRS, mEastings, mNorthings, 
				mOrthogonalHeight, mFactorX, mFactorY, mFactorZ);
			mapConversion.setOptionalAttributes(this);
			return mapConversion;
		}
	}
	[Serializable]
	public partial class IfcMappedItem : IfcRepresentationItem
	{
		private IfcRepresentationMap mMappingSource;// : IfcRepresentationMap;
		private IfcCartesianTransformationOperator mMappingTarget;// : IfcCartesianTransformationOperator;

		public IfcRepresentationMap MappingSource { get { return mMappingSource as IfcRepresentationMap; } set { mMappingSource = value; value.mMapUsage.Add(this); } }
		public IfcCartesianTransformationOperator MappingTarget { get { return mMappingTarget; } set { mMappingTarget = value;  } }

		internal IfcMappedItem() : base() { }
		internal IfcMappedItem(DatabaseIfc db, IfcMappedItem i, DuplicateOptions options) : base(db, i, options) 
		{
			MappingSource = db.Factory.Duplicate(i.MappingSource, options);
			MappingTarget = db.Factory.Duplicate(i.MappingTarget, options);
		}
		public IfcMappedItem(IfcRepresentationMap source, IfcCartesianTransformationOperator target) : base(source.mDatabase) { MappingSource = source; MappingTarget = target; }
	}
	[Serializable]
	public partial class IfcMarineFacility : IfcFacility
	{
		private IfcMarineFacilityTypeEnum mPredefinedType = IfcMarineFacilityTypeEnum.NOTDEFINED; //: OPTIONAL IfcMarineFacilityTypeEnum;
		public IfcMarineFacilityTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMarineFacilityTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcMarineFacility() : base() { }
		internal IfcMarineFacility(DatabaseIfc db) : base(db) { }
		public IfcMarineFacility(DatabaseIfc db, string name) : base(db, name) { }
		public IfcMarineFacility(DatabaseIfc db, IfcMarineFacility marineFacility, DuplicateOptions options) : base(db, marineFacility, options) { }
		public IfcMarineFacility(DatabaseIfc db, IfcMarineFacilityTypeEnum predefinedType)
			: base(db) { PredefinedType = predefinedType; }
		public IfcMarineFacility(IfcFacility host, string name, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcMarineFacilityTypeEnum predefinedType) : base(host, placement, representation) { Name = name; }
		internal IfcMarineFacility(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcMarinePart : IfcFacilityPart
	{
		private IfcMarinePartTypeEnum mPredefinedType = IfcMarinePartTypeEnum.NOTDEFINED; //: OPTIONAL IfcMarinePartTypeEnum;
		public IfcMarinePartTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMarinePartTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public override string StepClassName { get { if (mDatabase != null && mDatabase.Release > ReleaseVersion.IFC4X2 && mDatabase.Release < ReleaseVersion.IFC4X3) return "IfcFacilityPart"; return base.StepClassName; } }
		public IfcMarinePart() : base() { }
		public IfcMarinePart(DatabaseIfc db) : base(db) { }
		public IfcMarinePart(DatabaseIfc db, IfcMarinePart marinePart, DuplicateOptions options) : base(db, marinePart, options) { }
		public IfcMarinePart(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcMaterial : IfcMaterialDefinition, NamedObjectIfc
	{
		private string mName = "";// : IfcLabel; 
		private string mDescription = "";// : IFC4 OPTIONAL IfcText;
		private string mCategory = "";// : IFC4 OPTIONAL IfcLabel; 

		//INVERSE
		internal IfcMaterialDefinitionRepresentation mHasRepresentation = null;//	 : 	SET [0:1] OF IfcMaterialDefinitionRepresentation FOR RepresentedMaterial;
		//internal IfcMaterialclassificationRelationship mClassifiedAs = null;//	 : 	SET [0:1] OF IfcMaterialClassificationRelationship FOR classifiedMaterial;
		internal List<IfcMaterialRelationship> mIsRelatedWith = new List<IfcMaterialRelationship>();//	:	SET OF IfcMaterialRelationship FOR RelatedMaterials;
		internal IfcMaterialRelationship mRelatesTo = null;//	:	SET [0:1] OF IfcMaterialRelationship FOR RelatingMaterial;

		public override string Name { get { return mName; } set { mName = (string.IsNullOrEmpty(value) ? "UNKNOWN NAME" : value); } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public string Category { get { return mCategory; } set { mCategory = value; } }
		public IfcMaterialDefinitionRepresentation HasRepresentation { get { return mHasRepresentation; } set { mHasRepresentation = value; if (value != null && value.RepresentedMaterial != this) value.RepresentedMaterial = this; } }

		public override IfcMaterial PrimaryMaterial() { return this;  }

		public IfcMaterial() : base() { }
		internal IfcMaterial(DatabaseIfc db, IfcMaterial m, DuplicateOptions options) : base(db, m, options)
		{
			mName = m.mName;
			mDescription = m.mDescription;
			mCategory = m.mCategory;

			if(m.mHasRepresentation != null)
				db.Factory.Duplicate(m.mHasRepresentation, options);
		}
		public IfcMaterial(DatabaseIfc db, string name) : base(db) { Name = name; }

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
				if (!string.IsNullOrEmpty(mCategory))
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
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcMaterialClassificationRelationship : BaseClassIfc
	{
		internal SET<IfcClassificationNotationSelect> mMaterialClassifications = new SET<IfcClassificationNotationSelect>();// : SET [1:?] OF IfcClassificationNotationSelect;
		internal IfcMaterial mClassifiedMaterial;// : IfcMaterial;
		internal IfcMaterialClassificationRelationship() : base() { }
		internal IfcMaterialClassificationRelationship(DatabaseIfc db) : base(db) { }
		internal IfcMaterialClassificationRelationship(DatabaseIfc db, IfcMaterialClassificationRelationship m, DuplicateOptions options) 
			: base(db, m)
		{
			mMaterialClassifications.AddRange(m.mMaterialClassifications.Select(x=>db.Factory.Duplicate(x, options))); 
			mClassifiedMaterial = db.Factory.Duplicate(m.mClassifiedMaterial, options);
		}
	}
	[Serializable]
	public partial class IfcMaterialConstituent : IfcMaterialDefinition //IFC4
	{
		internal string mName = "";// :	OPTIONAL IfcLabel;
		internal string mDescription = "";// : OPTIONAL IfcText 
		internal IfcMaterial mMaterial;// : IfcMaterial;
		internal double mFraction;//	 :	OPTIONAL IfcNormalisedRatioMeasure;
		internal string mCategory = "";//	 :	OPTIONAL IfcLabel;  

		public override string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public IfcMaterial Material { get { return mMaterial; } set { mMaterial = value; } }
		public double Fraction { get { return mFraction; } set { mFraction = value; } }
		public string Category { get { return mCategory; } set { mCategory = value; } }

		public override IfcMaterial PrimaryMaterial() { return Material; }
		
		internal IfcMaterialConstituent() : base() { }
		internal IfcMaterialConstituent(DatabaseIfc db, IfcMaterialConstituent m, DuplicateOptions options) : base(db, m, options) 
		{
			mName = m.mName; 
			mDescription = m.mDescription;
			Material = db.Factory.Duplicate(m.Material, options);
			mFraction = m.mFraction;
			mCategory = m.mCategory;
		}
		public IfcMaterialConstituent(string name, IfcMaterial mat)
			: base(mat.mDatabase)
		{
			Name = name;
			mMaterial = mat;
		}
	}
	[Serializable]
	public partial class IfcMaterialConstituentSet : IfcMaterialDefinition
	{
		internal string mName = "";// : OPTIONAL IfcLabel;
		internal string mDescription = "";// : OPTIONAL IfcText 
		internal Dictionary<string, IfcMaterialConstituent> mMaterialConstituents = new Dictionary<string, IfcMaterialConstituent>();// LIST [1:?] OF IfcMaterialConstituent;

		public override string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public Dictionary<string, IfcMaterialConstituent> MaterialConstituents { get { return mMaterialConstituents; } }

		public override IfcMaterial PrimaryMaterial() { return MaterialConstituents.First().Value.PrimaryMaterial(); }

		internal IfcMaterialConstituentSet() : base() { }
		internal IfcMaterialConstituentSet(DatabaseIfc db, IfcMaterialConstituentSet m, DuplicateOptions options) : base(db, m, options)
		{
			mName = m.mName;
			mDescription = m.mDescription;
			foreach (IfcMaterialConstituent constituent in m.MaterialConstituents.Values)
				MaterialConstituents[constituent.Name] = db.Factory.Duplicate(constituent, options);
		}
		public IfcMaterialConstituentSet(IEnumerable<IfcMaterialConstituent> materialConstituents)
			: base(materialConstituents.First().Database)
		{
			foreach (IfcMaterialConstituent constituent in materialConstituents)
				mMaterialConstituents[constituent.Name] = constituent;
		}
		public IfcMaterialConstituentSet(string name, IEnumerable<IfcMaterialConstituent> materialConstituents)
			: this(materialConstituents)
		{
			Name = name;
		}
	}
	[Serializable]
	public abstract partial class IfcMaterialDefinition : BaseClassIfc, IfcObjectReferenceSelect, IfcMaterialSelect, IfcResourceObjectSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcMaterial ,IfcMaterialConstituent ,IfcMaterialConstituentSet ,IfcMaterialLayer ,IfcMaterialProfile ,IfcMaterialProfileSet));
	{
		//INVERSE  
		[NonSerialized] private SET<IfcRelAssociatesMaterial> mAssociatedTo = new SET<IfcRelAssociatesMaterial>();
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		private SET<IfcMaterialProperties> mHasProperties = new SET<IfcMaterialProperties>();
		[NonSerialized] internal SET<IfcResourceConstraintRelationship> mHasConstraintRelationships = new SET<IfcResourceConstraintRelationship>(); //gg

		public SET<IfcRelAssociatesMaterial> AssociatedTo { get { return mAssociatedTo; } }
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } }
		public SET<IfcMaterialProperties> HasProperties { get { return mHasProperties; } }
		public SET<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		public abstract string Name { get; set; }
		public virtual IfcMaterial PrimaryMaterial() { return null; }

		protected IfcMaterialDefinition() : base() { }

		protected IfcMaterialDefinition(DatabaseIfc db) : base(db) { }
		protected IfcMaterialDefinition(DatabaseIfc db, IfcMaterialDefinition m, DuplicateOptions options) : base(db, m) { }
		protected override void initialize()
		{
			base.initialize();
			mAssociatedTo.CollectionChanged += mAssociatedTo_CollectionChanged;
			mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged;
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

		public void Associate(IfcDefinitionSelect obj)
		{
			IfcRelAssociatesMaterial associates = (mAssociatedTo.Count == 0 ? new IfcRelAssociatesMaterial(this) : mAssociatedTo.First());
			associates.RelatedObjects.Add(obj);
		}
		public void Associate(IfcRelAssociatesMaterial associates) { if(!mAssociatedTo.Contains(associates)) mAssociatedTo.Add(associates); }

		public void AddExternalReferenceRelationship(IfcExternalReferenceRelationship referenceRelationship) { mHasExternalReference.Add(referenceRelationship); }
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
	public partial class IfcMaterialDefinitionRepresentation :  IfcProductRepresentation<IfcStyledRepresentation, IfcStyledItem>
	{
		internal IfcMaterial mRepresentedMaterial;// : IfcMaterial;
		public IfcMaterial RepresentedMaterial { get { return mRepresentedMaterial; } set { mRepresentedMaterial = value; if (value.mHasRepresentation != this) value.HasRepresentation = this; } }

		internal IfcMaterialDefinitionRepresentation() : base() { }
		internal IfcMaterialDefinitionRepresentation(DatabaseIfc db, IfcMaterialDefinitionRepresentation r, DuplicateOptions options) : base(db, r, options)
		{
			RepresentedMaterial = db.Factory.Duplicate(r.RepresentedMaterial, options);
		}
		public IfcMaterialDefinitionRepresentation(IfcStyledRepresentation representation, IfcMaterial mat) : base(representation) { RepresentedMaterial = mat; }
		public IfcMaterialDefinitionRepresentation(IEnumerable<IfcStyledRepresentation> representations, IfcMaterial mat) : base(representations) { RepresentedMaterial = mat; }
	}
	[Serializable]
	public partial class IfcMaterialLayer : IfcMaterialDefinition
	{
		internal IfcMaterial mMaterial;// : OPTIONAL IfcMaterial;
		internal double mLayerThickness;// ::	IfcNonNegativeLengthMeasure IFC4Chagne IfcPositiveLengthMeasure;
		internal IfcLogicalEnum mIsVentilated = IfcLogicalEnum.FALSE; // : OPTIONAL IfcLogical; 
		internal string mName = "";// : OPTIONAL IfcLabel; IFC4
		internal string mDescription = "";// : OPTIONAL IfcText; IFC4
		internal string mCategory = "";// : OPTIONAL IfcLabel; IFC4
		internal int mPriority = int.MinValue;// : OPTIONAL IfcInteger [0..100] was  IfcNormalisedRatioMeasure

		public IfcMaterial Material { get { return mMaterial; } set { mMaterial = value; } }
		public double LayerThickness { get { return mLayerThickness; } set { mLayerThickness = value; } }
		public IfcLogicalEnum IsVentilated { get { return mIsVentilated; } set { mIsVentilated = value; } }
		public override string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public string Category { get { return mCategory; } set { mCategory = value; } }
		public int Priority { get { return mPriority; } set { mPriority = (value >= 0 && value <= 100 ? value : int.MinValue); } }

		public override IfcMaterial PrimaryMaterial() { return Material; }

		internal IfcMaterialLayer() : base() { }
		internal IfcMaterialLayer(DatabaseIfc db, IfcMaterialLayer m, DuplicateOptions options) : base(db, m, options) 
		{ 
			Material = db.Factory.Duplicate(m.Material, options);
			mLayerThickness = m.mLayerThickness; 
			mIsVentilated = m.mIsVentilated;
		}
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
		private LIST<IfcMaterialLayer> mMaterialLayers = new LIST<IfcMaterialLayer>();// LIST [1:?] OF IfcMaterialLayer;
		private string mLayerSetName = "";// : OPTIONAL IfcLabel;
		private string mDescription = "";// : OPTIONAL IfcText

		public LIST<IfcMaterialLayer> MaterialLayers { get { return mMaterialLayers; } }
		public string LayerSetName { get { return mLayerSetName; } set { mLayerSetName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }

		public override string Name { get { return LayerSetName; } set { LayerSetName = value; } }
		public override IfcMaterial PrimaryMaterial() { return (mMaterialLayers.Count != 1 ? null : MaterialLayers[0].Material); }
	 	
		internal IfcMaterialLayerSet() : base() { } 
		internal IfcMaterialLayerSet(DatabaseIfc db, IfcMaterialLayerSet m, DuplicateOptions options) : base(db, m, options) 
		{
			MaterialLayers.AddRange(m.MaterialLayers.Select(x=> db.Factory.Duplicate(x, options))); 
			mLayerSetName = m.mLayerSetName; 
			mDescription = m.mDescription;
		}
		protected IfcMaterialLayerSet(DatabaseIfc db) : base(db) { }
		public IfcMaterialLayerSet(IfcMaterialLayer layer, string name) : base(layer.mDatabase) { MaterialLayers.Add(layer); Name = name;  }
		public IfcMaterialLayerSet(IEnumerable<IfcMaterialLayer> layers, string name) : base(layers.First().mDatabase)
		{
			Name = name;
			MaterialLayers.AddRange(layers);
		}
		
	}
	[Serializable]
	public partial class IfcMaterialLayerSetUsage : IfcMaterialUsageDefinition
	{
		private IfcMaterialLayerSet mForLayerSet;// : IfcMaterialLayerSet;
		private IfcLayerSetDirectionEnum mLayerSetDirection = IfcLayerSetDirectionEnum.AXIS1;// : IfcLayerSetDirectionEnum;
		private IfcDirectionSenseEnum mDirectionSense = IfcDirectionSenseEnum.POSITIVE;// : IfcDirectionSenseEnum;
		private double mOffsetFromReferenceLine;// : IfcLengthMeasure;  
		private double mReferenceExtent = double.NaN;//	 : IFC4	OPTIONAL IfcPositiveLengthMeasure;

		public IfcMaterialLayerSet ForLayerSet { get { return mForLayerSet; } set { mForLayerSet = value; } }
		public IfcLayerSetDirectionEnum LayerSetDirection { get { return mLayerSetDirection; } }
		public IfcDirectionSenseEnum DirectionSense { get { return mDirectionSense; } }
		public double OffsetFromReferenceLine { get { return mOffsetFromReferenceLine; } set { mOffsetFromReferenceLine = value; } }
		public double ReferenceExtent { get { return mReferenceExtent; } }

		public override string Name { get { return ForLayerSet.Name; } }
		public override IfcMaterial PrimaryMaterial() { return ForLayerSet.PrimaryMaterial(); }	
		
		internal IfcMaterialLayerSetUsage() : base() { }
		internal IfcMaterialLayerSetUsage(DatabaseIfc db, IfcMaterialLayerSetUsage m, DuplicateOptions options) : base(db, m, options) 
		{ 
			ForLayerSet = db.Factory.Duplicate(m.ForLayerSet, options); 
			mLayerSetDirection = m.mLayerSetDirection;
			mDirectionSense = m.mDirectionSense; 
			mOffsetFromReferenceLine = m.mOffsetFromReferenceLine; 
			mReferenceExtent = m.mReferenceExtent; 
		}
		public IfcMaterialLayerSetUsage(IfcMaterialLayerSet ls, IfcLayerSetDirectionEnum dir, IfcDirectionSenseEnum sense, double offset) : base(ls.mDatabase)
		{
			mForLayerSet = ls;
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
		internal IfcMaterialLayerSetWithOffsets(DatabaseIfc db, IfcMaterialLayerSetWithOffsets m, DuplicateOptions options) : base(db, m, options) { mOffsetDirection = m.mOffsetDirection; mOffsetValues = m.mOffsetValues.ToArray(); }
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
		internal IfcMaterialLayerWithOffsets(DatabaseIfc db, IfcMaterialLayerWithOffsets m, DuplicateOptions options) : base(db, m, options) 
		{ 
			mOffsetDirection = m.mOffsetDirection; 
			mOffsetValues = m.mOffsetValues.ToArray();
		}
		internal IfcMaterialLayerWithOffsets(IfcMaterial mat, double thickness, string name, IfcLayerSetDirectionEnum d, double startOffset)
			: base(mat, thickness, name) { mOffsetDirection = d; mOffsetValues = new double[]{ startOffset}; }
		internal IfcMaterialLayerWithOffsets(IfcMaterial mat, double thickness, string name, IfcLayerSetDirectionEnum d, double startOffset,double endOffset)
			: base(mat, thickness, name) { mOffsetDirection = d; mOffsetValues = new double[]{ startOffset,endOffset}; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcMaterialList : BaseClassIfc, IfcMaterialSelect, NamedObjectIfc //DEPRECATED IFC4
	{
		internal LIST<IfcMaterial> mMaterials = new LIST<IfcMaterial>();// LIST [1:?] OF IfcMaterial; 
		//INVERSE
		[NonSerialized] private SET<IfcRelAssociatesMaterial> mAssociatedTo = new SET<IfcRelAssociatesMaterial>();

		public LIST<IfcMaterial> Materials { get { return mMaterials; } }
		public SET<IfcRelAssociatesMaterial> AssociatedTo { get { return mAssociatedTo; } }

		public string Name { get { return "MaterialList"; } }
		public IfcMaterial PrimaryMaterial() {  return mMaterials.First(); }

		internal IfcMaterialList() : base() { }
		internal IfcMaterialList(DatabaseIfc db, IfcMaterialList m, DuplicateOptions options) : base(db) { Materials.AddRange(m.Materials.Select(x=>db.Factory.Duplicate(x, options))); }
		public IfcMaterialList(IEnumerable<IfcMaterial> materials) : base(materials.First().Database) { Materials.AddRange(materials); }
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
			IfcRelAssociatesMaterial associates = (mAssociatedTo.Count == 0 ? new IfcRelAssociatesMaterial(this) : mAssociatedTo.First());
			associates.RelatedObjects.Add(obj);
		}
		public void Associate(IfcRelAssociatesMaterial associates) { if(!mAssociatedTo.Contains(associates)) mAssociatedTo.Add(associates); }

	}
	[Serializable]
	public partial class IfcMaterialProfile : IfcMaterialDefinition // IFC4
	{
		internal string mName = "";// : OPTIONAL IfcLabel;
		internal string mDescription = "";// : OPTIONAL IfcText;
		internal IfcMaterial mMaterial = null;// : OPTIONAL IfcMaterial;
		internal IfcProfileDef mProfile = null;// : OPTIONAL IfcProfileDef;
		internal int mPriority = int.MinValue;// : OPTIONAL IfcInteger [0..100] was  IfcNormalisedRatioMeasure
		internal string mCategory = "";// : OPTIONAL IfcLabel
		// INVERSE
		private IfcMaterialProfileSet mToMaterialProfileSet = null;// : IfcMaterialProfileSet FOR 
		
		public override string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public IfcMaterial Material { get { return mMaterial; } set { mMaterial = value; } }
		public IfcProfileDef Profile { get { return mProfile; } set { mProfile = value; } }
		public string Category { get { return mCategory; } set { mCategory = value; } }
		public int Priority { get { return mPriority; } set { mPriority = (value >= 0 && value <= 100 ? value : int.MinValue); } }
		public IfcMaterialProfileSet ToMaterialProfileSet { get { return mToMaterialProfileSet; } set { mToMaterialProfileSet = value; } }

		public override IfcMaterial PrimaryMaterial() { return Material; }

		internal IfcMaterialProfile() : base() { }
		internal IfcMaterialProfile(DatabaseIfc db, IfcMaterialProfile p, DuplicateOptions options) : base(db, p, options)
		{
			mName = p.mName;
			mDescription = p.mDescription;
			if (p.mMaterial != null)
				Material = db.Factory.Duplicate(p.Material, options);
			if (p.mProfile != null)
				Profile = db.Factory.Duplicate(p.Profile, options);
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
		internal string mName = ""; //: OPTIONAL IfcLabel;
		internal string mDescription = ""; //: OPTIONAL IfcText; 
		internal LIST<IfcMaterialProfile> mMaterialProfiles = new LIST<IfcMaterialProfile>();// LIST [1:?] OF IfcMaterialProfile;
		internal IfcCompositeProfileDef mCompositeProfile = null;// : OPTIONAL IfcCompositeProfileDef; 

		public override string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public LIST<IfcMaterialProfile> MaterialProfiles { get { return mMaterialProfiles; } }
		public IfcCompositeProfileDef CompositeProfile { get { return mCompositeProfile; } set { mCompositeProfile = value; } }

		public override IfcMaterial PrimaryMaterial() { return (mMaterialProfiles.Count != 1 ? null : MaterialProfiles[0].Material); }

		//GeomGym
		internal IfcMaterialProfileSet mTaperEnd = null;

		internal IfcMaterialProfileSet() : base() { }
		internal IfcMaterialProfileSet(DatabaseIfc db, IfcMaterialProfileSet m, DuplicateOptions options) : base(db, m, options) 
		{
			mName = m.mName;
			mDescription = m.mDescription; 
			MaterialProfiles.AddRange(m.mMaterialProfiles.ConvertAll(x => db.Factory.Duplicate(x, options)));
			if (m.mCompositeProfile != null) 
				CompositeProfile = db.Factory.Duplicate(m.CompositeProfile, options);
		}
		private IfcMaterialProfileSet(DatabaseIfc db, string name) : base(db) { Name = name; }
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
			for (int icounter = 0; icounter < profiles.Count; icounter++)
			{
				IfcMaterialProfile mp = profiles[icounter];
				if (mp.ToMaterialProfileSet == null)
					mp.ToMaterialProfileSet = this;
				else
					throw new Exception("Material Profile can be assigned to only a single profile set");
				mMaterialProfiles.Add(mp);
			}
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
		internal IfcMaterialProfileSetUsage(DatabaseIfc db, IfcMaterialProfileSetUsage m, DuplicateOptions options) : base(db, m, options) 
		{
			ForProfileSet = db.Factory.Duplicate(m.ForProfileSet, options);
			mCardinalPoint = m.mCardinalPoint; 
			mReferenceExtent = m.mReferenceExtent;
		}
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
		 
		public IfcMaterialProfileSet ForProfileEndSet { get { return mForProfileEndSet; } set { mForProfileEndSet = value; } }
		public IfcCardinalPointReference CardinalEndPoint { get { return mCardinalEndPoint; } set { mCardinalEndPoint = value; } }

		internal IfcMaterialProfileSetUsageTapering() : base() { }
		internal IfcMaterialProfileSetUsageTapering(DatabaseIfc db, IfcMaterialProfileSetUsageTapering m, DuplicateOptions options) 
			: base(db, m, options) 
		{
			ForProfileEndSet = db.Factory.Duplicate(m.ForProfileEndSet, options);
		}
		public IfcMaterialProfileSetUsageTapering(IfcMaterialProfileSet ps, IfcCardinalPointReference ip, IfcMaterialProfileSet eps, IfcCardinalPointReference eip)
			: base(ps, ip) { mForProfileEndSet = eps; mCardinalEndPoint = eip; }
	}
	[Serializable]
	public partial class IfcMaterialProfileWithOffsets : IfcMaterialProfile //IFC4
	{
		internal double[] mOffsetValues = new double[2] { 0, 0 };// ARRAY [1:2] OF IfcLengthMeasure;
		public double[] OffsetValues { get { return mOffsetValues; } set { mOffsetValues = value; } }

		internal IfcMaterialProfileWithOffsets() : base() { }
		internal IfcMaterialProfileWithOffsets(DatabaseIfc db, IfcMaterialProfileWithOffsets m, DuplicateOptions options) : base(db, m, options)
		{
			mOffsetValues = m.mOffsetValues.ToArray();
		}
		public IfcMaterialProfileWithOffsets(string name, IfcMaterial mat, IfcProfileDef p, double startOffset)
			: base(name, mat, p) { mOffsetValues = new double[] { startOffset }; }
		public IfcMaterialProfileWithOffsets(string name, IfcMaterial mat, IfcProfileDef p, double startOffset,double endOffset)
			: base(name, mat, p) { mOffsetValues = new double[] { startOffset,endOffset }; }
	}
	
	[Serializable]
	public partial class IfcMaterialProperties : IfcExtendedProperties //IFC4, IFC2x3 ABSTRACT SUPERTYPE OF (ONE(IfcExtendedMaterialProperties,IfcFuelProperties,IfcGeneralMaterialProperties,IfcHygroscopicMaterialProperties,IfcMechanicalMaterialProperties,IfcOpticalMaterialProperties,IfcProductsOfCombustionProperties,IfcThermalMaterialProperties,IfcWaterProperties));
	{
		public override string StepClassName { get { return (mDatabase != null && mDatabase.mRelease < ReleaseVersion.IFC4 ? base.StepClassName : "IfcMaterialProperties"); } }
		private IfcMaterialDefinition mMaterial;// : IfcMaterialDefinition; 
		public IfcMaterialDefinition Material { get { return mMaterial; } set { mMaterial = value; value.HasProperties.Add(this); } }

		internal IfcMaterialProperties() : base() { }
		internal IfcMaterialProperties(DatabaseIfc db, IfcMaterialProperties p, DuplicateOptions options) : base(db, p, options) { Material = db.Factory.Duplicate(p.Material, options) as IfcMaterialDefinition; }
		protected IfcMaterialProperties(IfcMaterialDefinition mat) : base(mat.mDatabase) { Name = this.GetType().Name; Material = mat; }
		public IfcMaterialProperties(string name, IfcMaterialDefinition mat) : base(mat.mDatabase) { Name = name; Material = mat; }
		internal IfcMaterialProperties(string name, IEnumerable<IfcProperty> props, IfcMaterialDefinition mat) : base(props) { Name = name; Material = mat; }
	}
	[Serializable]
	public partial class IfcMaterialRelationship : IfcResourceLevelRelationship //IFC4
	{
		internal IfcMaterial mRelatingMaterial;// : IfcMaterial;
		internal SET<IfcMaterial> mRelatedMaterials = new SET<IfcMaterial>(); //	:	SET [1:?] OF IfcMaterial;
		internal string mMaterialExpression = "";//:	OPTIONAL IfcLabel;
		public IfcMaterial RelatingMaterial { get { return mRelatingMaterial; } set { mRelatingMaterial = value; } }
		public SET<IfcMaterial> RelatedMaterials { get { return mRelatedMaterials; } }
		public string MaterialExpression { get { return mMaterialExpression; } set { mMaterialExpression = value; } }

		internal IfcMaterialRelationship() : base() { }
		internal IfcMaterialRelationship(DatabaseIfc db) : base(db) { }
		internal IfcMaterialRelationship(DatabaseIfc db, IfcMaterialRelationship r, DuplicateOptions options) : base(db, r, options) 
		{
			RelatingMaterial = db.Factory.Duplicate(r.RelatingMaterial, options) as IfcMaterial;
			RelatedMaterials.AddRange(r.RelatedMaterials.Select(x => db.Factory.Duplicate(x) as IfcMaterial));
			MaterialExpression = r.MaterialExpression;
		}
		public IfcMaterialRelationship(IfcMaterial mat, List<IfcMaterial> related) : base(mat.mDatabase)
		{
			mRelatingMaterial = mat;
			mRelatedMaterials.AddRange(related);
		}
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
		public SET<IfcRelAssociatesMaterial> AssociatedTo { get { return mAssociatedTo; } }

		public abstract string Name { get; }
		public abstract IfcMaterial PrimaryMaterial();

		protected IfcMaterialUsageDefinition() : base() { }
		protected IfcMaterialUsageDefinition(DatabaseIfc db) : base(db) { }
		protected IfcMaterialUsageDefinition(DatabaseIfc db, IfcMaterialUsageDefinition d, DuplicateOptions options) : base(db,d) { }

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
			IfcRelAssociatesMaterial associates = (mAssociatedTo.Count == 0 ? new IfcRelAssociatesMaterial(this) : mAssociatedTo.First());
			associates.RelatedObjects.Add(obj);
		}
		public void Associate(IfcRelAssociatesMaterial associates) { if (!mAssociatedTo.Contains(associates)) mAssociatedTo.Add(associates); }
	}
	[Serializable]
	public partial class IfcMeasureWithUnit : BaseClassIfc, IfcAppliedValueSelect, IfcConditionCriterionSelect
	{
		internal IfcValue mValueComponent;// : IfcValue; 
		private string mVal;
		private IfcUnit mUnitComponent;// : IfcUnit; 

		public IfcValue ValueComponent { get { return mValueComponent; } set { mValueComponent = value; } }
		public IfcUnit UnitComponent { get { return mUnitComponent; } set { mUnitComponent = value; } }

		internal IfcMeasureWithUnit() : base() { }
		internal IfcMeasureWithUnit(DatabaseIfc db, IfcMeasureWithUnit m, DuplicateOptions options) : base(db) { mValueComponent = m.mValueComponent; mVal = m.mVal;  UnitComponent = db.Factory.Duplicate(m.mUnitComponent, options); }
		public IfcMeasureWithUnit(IfcValue v, IfcUnit u) : base(u.Database) { mValueComponent = v; mUnitComponent = u; }
		internal IfcMeasureWithUnit(double value, IfcUnit u) : base(u.Database) { mValueComponent = new IfcReal(value); mUnitComponent = u;  }
		public double SIFactor()
		{
			IfcUnit u = UnitComponent;
			IfcSIUnit si = u as IfcSIUnit;
			double sif = (si == null ? 1 : si.SIFactor()), d = 1;
			if (mValueComponent != null)
			{
				IfcMeasureValue m = mValueComponent as IfcMeasureValue;
				if (m != null)
					return m.Measure * sif;
				IfcDerivedMeasureValue dm = mValueComponent as IfcDerivedMeasureValue;
				if (dm != null)
					return dm.Measure * sif;
				if (double.TryParse(mValueComponent.ValueString, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out d))
					return d * sif;
			}
			if (double.TryParse(mVal, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out d))
				return d * sif;
			return sif;
		}
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcMechanicalConcreteMaterialProperties : IfcMechanicalMaterialProperties // DEPRECATED IFC4
	{
		internal double mCompressiveStrength = double.NaN;// : OPTIONAL IfcPressureMeasure;
		internal double mMaxAggregateSize = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal string mAdmixturesDescription = "", mWorkability = "";// : OPTIONAL IfcText
		internal double mProtectivePoreRatio = double.NaN;// : OPTIONAL IfcNormalisedRatioMeasure;
		internal string mWaterImpermeability = "";// : OPTIONAL IfcText; 
		internal IfcMechanicalConcreteMaterialProperties() : base() { }
		internal IfcMechanicalConcreteMaterialProperties(DatabaseIfc db, IfcMechanicalConcreteMaterialProperties p, DuplicateOptions options) : base(db, p, options)
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
		internal double mNominalDiameter = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure; IFC4 DEPRECATED
		internal double mNominalLength = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;  IFC4 DEPRECATED
		private IfcMechanicalFastenerTypeEnum mPredefinedType = IfcMechanicalFastenerTypeEnum.NOTDEFINED;// : OPTIONAL IfcMechanicalFastenerTypeEnum;
		public IfcMechanicalFastenerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMechanicalFastenerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcMechanicalFastener() : base() { }
		internal IfcMechanicalFastener(DatabaseIfc db, IfcMechanicalFastener f, DuplicateOptions options) : base(db, f, options) { mNominalDiameter = f.mNominalDiameter; mNominalLength = f.mNominalLength; }
		public IfcMechanicalFastener(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		public IfcMechanicalFastener(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public partial class IfcMechanicalFastenerType : IfcElementComponentType //IFC4 change
	{
		internal double mNominalDiameter;// : OPTIONAL IfcPositiveLengthMeasure; IFC4
		internal double mNominalLength;// : OPTIONAL IfcPositiveLengthMeasure; IFC4
		private IfcMechanicalFastenerTypeEnum mPredefinedType = IfcMechanicalFastenerTypeEnum.NOTDEFINED;// : IfcMechanicalFastenerTypeEnum; IFC4
		public IfcMechanicalFastenerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMechanicalFastenerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcMechanicalFastenerType() : base() { }
		internal IfcShapeRepresentation mProfileRep = null;
		internal IfcMechanicalFastenerType(DatabaseIfc db, IfcMechanicalFastenerType t, DuplicateOptions options) : base(db, t, options) { mNominalDiameter = t.mNominalDiameter; mNominalLength = t.mNominalLength; PredefinedType = t.PredefinedType; }
		public IfcMechanicalFastenerType(DatabaseIfc db, string name, IfcMechanicalFastenerTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcMechanicalMaterialProperties : IfcMaterialProperties // DEPRECATED IFC4
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
		internal IfcMechanicalMaterialProperties(DatabaseIfc db, IfcMechanicalMaterialProperties p, DuplicateOptions options) : base(db, p, options) { mDynamicViscosity = p.mDynamicViscosity; mYoungModulus = p.mYoungModulus; mShearModulus = p.mShearModulus; mPoissonRatio = p.mPoissonRatio; mThermalExpansionCoefficient = p.mThermalExpansionCoefficient; }
		public IfcMechanicalMaterialProperties(IfcMaterial mat, double dynVisc, double youngs, double shear, double poisson, double thermalExp)
			: base(mat) { mDynamicViscosity = dynVisc; mYoungModulus = youngs; mShearModulus = shear; mPoissonRatio = poisson; mThermalExpansionCoefficient = thermalExp; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcMechanicalSteelMaterialProperties : IfcMechanicalMaterialProperties // DEPRECATED IFC4
	{
		internal double mYieldStress = double.NaN, mUltimateStress = double.NaN;// : OPTIONAL IfcPressureMeasure;
		internal double mUltimateStrain = double.NaN;// :  OPTIONAL IfcPositiveRatioMeasure;
		internal double mHardeningModule = double.NaN;// : OPTIONAL IfcModulusOfElasticityMeasure
		internal double mProportionalStress = double.NaN;// : OPTIONAL IfcPressureMeasure;
		internal double mPlasticStrain = double.NaN;// : OPTIONAL IfcPositiveRatioMeasure;
		internal SET<IfcRelaxation> mRelaxations = new SET<IfcRelaxation>();// : OPTIONAL SET [1:?] OF IfcRelaxation 

		public double YieldStress { get { return mYieldStress; } set { mYieldStress = value; } }
		public double UltimateStress { get { return mUltimateStress; } set { mUltimateStress = value; } }
		public double HardeningModule { get { return mHardeningModule; } set { mHardeningModule = value; } }
		public double ProportionalStress { get { return mProportionalStress; } set { mProportionalStress = value; } }
		public double PlasticStrain { get { return mPlasticStrain; } set { mPlasticStrain = value; } }
		public SET<IfcRelaxation> Relaxations { get { return mRelaxations; } }

		internal IfcMechanicalSteelMaterialProperties() : base() { }
		internal IfcMechanicalSteelMaterialProperties(DatabaseIfc db, IfcMechanicalSteelMaterialProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mYieldStress = p.mYieldStress;
			mUltimateStress = p.mUltimateStress;
			mUltimateStrain = p.mUltimateStrain;
			mHardeningModule = p.mHardeningModule;
			mProportionalStress = p.mProportionalStress;
			mPlasticStrain = p.mPlasticStrain;
			Relaxations.AddRange(p.Relaxations.Select(x=> db.Factory.Duplicate(x)));
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
		private IfcMedicalDeviceTypeEnum mPredefinedType = IfcMedicalDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcMedicalDeviceTypeEnum;
		public IfcMedicalDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMedicalDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcMedicalDevice() : base() { }
		internal IfcMedicalDevice(DatabaseIfc db, IfcMedicalDevice d, DuplicateOptions options) : base(db, d, options) { PredefinedType = d.PredefinedType; }
		public IfcMedicalDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcMedicalDeviceType : IfcFlowTerminalType
	{
		private IfcMedicalDeviceTypeEnum mPredefinedType = IfcMedicalDeviceTypeEnum.NOTDEFINED;// : IfcMedicalDeviceBoxTypeEnum; 
		public IfcMedicalDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMedicalDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcMedicalDeviceType() : base() { }
		internal IfcMedicalDeviceType(DatabaseIfc db, IfcMedicalDeviceType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcMedicalDeviceType(DatabaseIfc db, string name, IfcMedicalDeviceTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcMember : IfcBuiltElement
	{
		private IfcMemberTypeEnum mPredefinedType = IfcMemberTypeEnum.NOTDEFINED;//: OPTIONAL IfcMemberTypeEnum; 
		public IfcMemberTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMemberTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcMember() : base() { }
		internal IfcMember(DatabaseIfc db, IfcMember m, DuplicateOptions options) : base(db, m, options) { PredefinedType = m.PredefinedType; }
		public IfcMember(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		public IfcMember(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable, Obsolete("DEPRECATED IFC4", false)]
	public partial class IfcMemberStandardCase : IfcMember
	{
		public override string StepClassName { get { return "IfcMember"; } }

		internal IfcMemberStandardCase() : base() { }
		internal IfcMemberStandardCase(DatabaseIfc db, IfcMemberStandardCase m, DuplicateOptions options) : base(db, m, options) { }
		public IfcMemberStandardCase(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public partial class IfcMemberType : IfcBuiltElementType
	{
		private IfcMemberTypeEnum mPredefinedType = IfcMemberTypeEnum.NOTDEFINED;
		public IfcMemberTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMemberTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcMemberType() : base() { }
		internal IfcMemberType(DatabaseIfc db, IfcMemberType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcMemberType(string name, IfcMaterialProfileSet ps, IfcMemberTypeEnum type) : base(ps.mDatabase)
		{
			Name = name;
			PredefinedType = type;
			if (ps.mTaperEnd != null)
				mTapering = ps;
			else
				MaterialSelect = ps;
		}
		public IfcMemberType(DatabaseIfc db, string name, IfcMemberTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
		public IfcMemberType(string name, IfcMaterialProfile mp, IfcMemberTypeEnum type) : base(mp.mDatabase) { Name = name; PredefinedType = type; MaterialSelect = mp; }
	}
	[Serializable]
	public partial class IfcMetric : IfcConstraint
	{
		internal IfcBenchmarkEnum mBenchMark = IfcBenchmarkEnum.EQUALTO;// : IfcBenchmarkEnum
		internal string mValueSource = ""; //	 :	OPTIONAL IfcLabel;
		private IfcMetricValueSelect mDataValue = null;// : OPTIONAL IfcMetricValueSelect;
		private IfcReference mReferencePath;// :	OPTIONAL IfcReference;

		public IfcBenchmarkEnum BenchMark { get { return mBenchMark; } set { mBenchMark = value; } }
		public string ValueSource { get { return mValueSource; } set { mValueSource = value; } }
		public IfcMetricValueSelect DataValue
		{
			get { return mDataValue; }
			set { mDataValue = value; }
		}
		public IfcReference ReferencePath { get { return mReferencePath; } set { mReferencePath = value; } }

		internal IfcMetric() : base() { }
		internal IfcMetric(DatabaseIfc db, IfcMetric m) : base(db,m)
		{
			mBenchMark = m.mBenchMark;
			mValueSource = m.mValueSource;
			if (m.DataValue is BaseClassIfc o)
				DataValue = db.Factory.Duplicate(o) as IfcMetricValueSelect;
			else
				DataValue = m.DataValue;
			ReferencePath = db.Factory.Duplicate(m.ReferencePath);
		}
		public IfcMetric(DatabaseIfc db, string name, IfcConstraintEnum constraint) : base(db, name, constraint) { }
	}
	public interface IfcMetricValueSelect : IBaseClassIfc { } //SELECT ( IfcMeasureWithUnit, IfcTable, IfcTimeSeries, IfcAppliedValue, IfcValue, IfcReference);
	[Serializable]
	public partial class IfcMirroredProfileDef : IfcDerivedProfileDef //SUPERTYPE OF(IfcMirroredProfileDef)
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcDerivedProfileDef" : base.StepClassName); } }
		internal IfcMirroredProfileDef() : base() { }
		internal IfcMirroredProfileDef(DatabaseIfc db, IfcMirroredProfileDef p, DuplicateOptions options) : base(db, p, options) { }
		public IfcMirroredProfileDef(IfcProfileDef p, string name) : base(p, null, name) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcMobileTelecommunicationsAppliance : IfcFlowTerminal
	{
		private IfcMobileTelecommunicationsApplianceTypeEnum mPredefinedType = IfcMobileTelecommunicationsApplianceTypeEnum.NOTDEFINED; //: OPTIONAL IfcMobileTelecommunicationsApplianceTypeEnum;
		public IfcMobileTelecommunicationsApplianceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMobileTelecommunicationsApplianceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcMobileTelecommunicationsAppliance() : base() { }
		public IfcMobileTelecommunicationsAppliance(DatabaseIfc db) : base(db) { }
		public IfcMobileTelecommunicationsAppliance(DatabaseIfc db, IfcMobileTelecommunicationsAppliance mobileAppliance, DuplicateOptions options) : base(db, mobileAppliance, options) { PredefinedType = mobileAppliance.PredefinedType; }
		public IfcMobileTelecommunicationsAppliance(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcMobileTelecommunicationsApplianceType : IfcFlowTerminalType
	{
		private IfcMobileTelecommunicationsApplianceTypeEnum mPredefinedType = IfcMobileTelecommunicationsApplianceTypeEnum.NOTDEFINED; //: IfcMobileTelecommunicationsApplianceTypeEnum;
		public IfcMobileTelecommunicationsApplianceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMobileTelecommunicationsApplianceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcMobileTelecommunicationsApplianceType() : base() { }
		public IfcMobileTelecommunicationsApplianceType(DatabaseIfc db, IfcMobileTelecommunicationsApplianceType mobileApplianceType, DuplicateOptions options) : base(db, mobileApplianceType, options) { PredefinedType = mobileApplianceType.PredefinedType; }
		public IfcMobileTelecommunicationsApplianceType(DatabaseIfc db, string name, IfcMobileTelecommunicationsApplianceTypeEnum predefinedType)
			: base(db) { Name = name; PredefinedType = predefinedType; }
	}
	[Serializable]
	public partial class IfcModulusOfRotationalSubgradeReactionSelect : StiffnessSelect<IfcModulusOfRotationalSubgradeReactionMeasure>
	{
		public IfcModulusOfRotationalSubgradeReactionSelect() : base() { }
		public IfcModulusOfRotationalSubgradeReactionSelect(bool fix) : base(fix) { }
		public IfcModulusOfRotationalSubgradeReactionSelect(double stiffness) : base(new IfcModulusOfRotationalSubgradeReactionMeasure(stiffness)) { }
		public IfcModulusOfRotationalSubgradeReactionSelect(IfcModulusOfRotationalSubgradeReactionMeasure stiffness) : base(stiffness) { }
	}
	[Serializable]
	public partial class IfcModulusOfSubgradeReactionSelect : StiffnessSelect<IfcModulusOfSubgradeReactionMeasure>
	{
		public IfcModulusOfSubgradeReactionSelect() : base() { }
		public IfcModulusOfSubgradeReactionSelect(bool fix) : base(fix) { }
		public IfcModulusOfSubgradeReactionSelect(double stiffness) : base(new IfcModulusOfSubgradeReactionMeasure(stiffness)) { }
		public IfcModulusOfSubgradeReactionSelect(IfcModulusOfSubgradeReactionMeasure stiffness) : base(stiffness) { }
	}
	[Serializable]
	public partial class IfcModulusOfTranslationalSubgradeReactionSelect : StiffnessSelect<IfcModulusOfLinearSubgradeReactionMeasure>
	{
		public IfcModulusOfTranslationalSubgradeReactionSelect() : base() { }
		public IfcModulusOfTranslationalSubgradeReactionSelect(bool fix) : base(fix) { }
		public IfcModulusOfTranslationalSubgradeReactionSelect(double stiffness) : base(new IfcModulusOfLinearSubgradeReactionMeasure(stiffness)) { }
		public IfcModulusOfTranslationalSubgradeReactionSelect(IfcModulusOfLinearSubgradeReactionMeasure stiffness) : base(stiffness) { }
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
	public partial class IfcMooringDevice : IfcBuiltElement
	{
		private IfcMooringDeviceTypeEnum mPredefinedType = IfcMooringDeviceTypeEnum.NOTDEFINED; //: OPTIONAL IfcMooringDeviceTypeEnum;
		public IfcMooringDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMooringDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcMooringDevice() : base() { }
		public IfcMooringDevice(DatabaseIfc db) : base(db) { }
		public IfcMooringDevice(DatabaseIfc db, IfcMooringDevice mooringDevice, DuplicateOptions options) : base(db, mooringDevice, options) { PredefinedType = mooringDevice.PredefinedType; }
		public IfcMooringDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcMooringDeviceType : IfcBuiltElementType
	{
		private IfcMooringDeviceTypeEnum mPredefinedType = IfcMooringDeviceTypeEnum.NOTDEFINED; //: IfcMooringDeviceTypeEnum;
		public IfcMooringDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMooringDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcMooringDeviceType() : base() { }
		public IfcMooringDeviceType(DatabaseIfc db, IfcMooringDeviceType mooringDeviceType, DuplicateOptions options) : base(db, mooringDeviceType, options) { PredefinedType = mooringDeviceType.PredefinedType; }
		public IfcMooringDeviceType(DatabaseIfc db, string name, IfcMooringDeviceTypeEnum predefinedType)
			: base(db, name) { PredefinedType = predefinedType; }
	}
	[Serializable]
	public partial class IfcMotorConnection : IfcEnergyConversionDevice //IFC4
	{
		private IfcMotorConnectionTypeEnum mPredefinedType = IfcMotorConnectionTypeEnum.NOTDEFINED;// OPTIONAL : IfcMotorConnectionTypeEnum;
		public IfcMotorConnectionTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMotorConnectionTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcMotorConnection() : base() { }
		internal IfcMotorConnection(DatabaseIfc db, IfcMotorConnection c, DuplicateOptions options) : base(db, c, options) { PredefinedType = c.PredefinedType; }
		public IfcMotorConnection(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcMotorConnectionType : IfcEnergyConversionDeviceType
	{
		private IfcMotorConnectionTypeEnum mPredefinedType = IfcMotorConnectionTypeEnum.NOTDEFINED;// : IfcMotorConnectionTypeEnum; 
		public IfcMotorConnectionTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcMotorConnectionTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcMotorConnectionType() : base() { }
		internal IfcMotorConnectionType(DatabaseIfc db, IfcMotorConnectionType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcMotorConnectionType(DatabaseIfc db, string name, IfcMotorConnectionTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcMove // DEPRECATED IFC4
}
