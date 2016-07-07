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
	public abstract partial class IfcManifoldSolidBrep : IfcSolidModel //ABSTRACT SUPERTYPE OF(ONEOF(IfcAdvancedBrep, IfcFacetedBrep))
	{
		private int mOuter;// : IfcClosedShell; 
		public IfcClosedShell Outer { get { return mDatabase[mOuter] as IfcClosedShell; } set { mOuter = value.mIndex; } }

		protected IfcManifoldSolidBrep() : base() { }
		protected IfcManifoldSolidBrep(IfcClosedShell s) : base(s.mDatabase) { Outer = s; }
		protected IfcManifoldSolidBrep(DatabaseIfc db, IfcManifoldSolidBrep b) : base(db,b) { Outer = db.Duplicate( b.Outer) as IfcClosedShell; }

		internal static void parseFields(IfcManifoldSolidBrep b, List<string> arrFields, ref int ipos) { IfcSolidModel.parseFields(b, arrFields, ref ipos); b.mOuter = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mOuter); }
	} 
	public partial class IfcMapConversion : IfcCoordinateOperation //IFC4
	{
		internal double mEastings, mNorthings, mOrthogonalHeight;// :  	IfcLengthMeasure;
		internal double mXAxisAbscissa = double.NaN, mXAxisOrdinate = double.NaN, mScale = double.NaN;// 	:	OPTIONAL IfcReal;
		public double Eastings { get { return mEastings; } set { mEastings = value; } }
		public double Northings { get { return mNorthings; } set { mNorthings = value; } }
		public double OrthogonalHeight { get { return mOrthogonalHeight; } set { mOrthogonalHeight = value; } }
		public double XAxisAbscissa { get { return mXAxisAbscissa; } set { mXAxisAbscissa = value; } }
		public double XAxisOrdinate { get { return mXAxisOrdinate; } set { mXAxisOrdinate = value; } }
		public double Scale { get { return double.IsNaN( mScale) ? 1 : mScale; } set { mScale = value; } }

		internal IfcMapConversion() : base() { }
		internal IfcMapConversion(IfcMapConversion i) : base(i) { mEastings = i.mEastings; mNorthings = i.mNorthings; mOrthogonalHeight = i.mOrthogonalHeight; mXAxisAbscissa = i.mXAxisAbscissa; mXAxisOrdinate = i.mXAxisOrdinate; mScale = i.mScale; }
		internal IfcMapConversion(IfcCoordinateReferenceSystemSelect source, IfcCoordinateReferenceSystem target, double eastings, double northings, double orthogonalHeight)
			: base(source, target)
		{
			mEastings = eastings;
			mNorthings = northings;
			mOrthogonalHeight = orthogonalHeight;
		}
		internal static IfcMapConversion Parse(string strDef) { IfcMapConversion b = new IfcMapConversion(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcMapConversion b, List<string> arrFields, ref int ipos)
		{
			IfcCoordinateOperation.parseFields(b, arrFields, ref ipos);
			b.mEastings = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mNorthings = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mOrthogonalHeight = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mXAxisAbscissa = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mXAxisOrdinate = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mScale = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mEastings) + "," + ParserSTEP.DoubleToString(mNorthings) + "," + ParserSTEP.DoubleToString(mOrthogonalHeight) + "," + ParserSTEP.DoubleOptionalToString(mXAxisAbscissa) + "," + ParserSTEP.DoubleOptionalToString(mXAxisOrdinate) + "," + ParserSTEP.DoubleOptionalToString(mScale); }
	}
	public partial class IfcMappedItem : IfcRepresentationItem
	{
		private int mMappingSource;// : IfcRepresentationMap;
		private int mMappingTarget;// : IfcCartesianTransformationOperator;

		public IfcRepresentationMap MappingSource { get { return mDatabase[mMappingSource] as IfcRepresentationMap; } set { mMappingSource = value.mIndex; } }
		public IfcCartesianTransformationOperator MappingTarget { get { return mDatabase[mMappingTarget] as IfcCartesianTransformationOperator; } set { mMappingTarget = value.mIndex; } }

		internal IfcMappedItem() : base() { }
		internal IfcMappedItem(DatabaseIfc db, IfcMappedItem i) : base(db,i) { MappingSource = db.Duplicate(i.MappingSource) as IfcRepresentationMap; MappingTarget = db.Duplicate(i.MappingTarget) as IfcCartesianTransformationOperator; }
		internal IfcMappedItem(IfcRepresentationMap rm, IfcCartesianTransformationOperator co) : base(rm.mDatabase) { mMappingSource = rm.mIndex; mMappingTarget = co.mIndex; }
		
		internal static void parseFields(IfcMappedItem i, List<string> arrFields, ref int ipos) { IfcRepresentationItem.parseFields(i, arrFields, ref ipos); i.mMappingSource = ParserSTEP.ParseLink(arrFields[ipos++]); i.mMappingTarget = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcMappedItem Parse(string strDef) { IfcMappedItem i = new IfcMappedItem(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mMappingSource) + "," + ParserSTEP.LinkToString(mMappingTarget); }
	}
	public partial class IfcMaterial : IfcMaterialDefinition
	{
		private string mName = "";// : IfcLabel; 
		private string mDescription = "$";// : IFC4 OPTIONAL IfcText;
		private string mCategory = "$";// : IFC4 OPTIONAL IfcLabel; 

		//INVERSE
		internal IfcMaterialDefinitionRepresentation mHasRepresentation = null;//	 : 	SET [0:1] OF IfcMaterialDefinitionRepresentation FOR RepresentedMaterial;
		//internal IfcMaterialclassificationRelationship mClassifiedAs = null;//	 : 	SET [0:1] OF IfcMaterialClassificationRelationship FOR classifiedMaterial;
		internal List<IfcMaterialPropertiesSuperSeded> mHasPropertiesSS = new List<IfcMaterialPropertiesSuperSeded>();
		internal List<IfcMaterialRelationship> mIsRelatedWith = new List<IfcMaterialRelationship>();//	:	SET OF IfcMaterialRelationship FOR RelatedMaterials;
		internal IfcMaterialRelationship mRelatesTo = null;//	:	SET [0:1] OF IfcMaterialRelationship FOR RelatingMaterial;

		public override string Name { get { return ParserIfc.Decode(mName); } set { mName = (string.IsNullOrEmpty(value) ? "UNKNOWN NAME" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Category { get { return (mCategory == "$" ? "" : ParserIfc.Decode(mCategory)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcMaterialDefinitionRepresentation HasRepresentation { get { return mHasRepresentation; } set { mHasRepresentation = value; if (value != null && value.RepresentedMaterial != this) value.RepresentedMaterial = this; } }

		public override IfcMaterial PrimaryMaterial { get { return this; } }

		public IfcMaterial() : base() { }
		public IfcMaterial(DatabaseIfc m, string name) : base(m) { Name = name; }

		internal static IfcMaterial Parse(string strDef, ReleaseVersion schema) { IfcMaterial m = new IfcMaterial(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return m; }
		internal static void parseFields(IfcMaterial m, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcMaterialDefinition.parseFields(m, arrFields, ref ipos);
			m.mName = arrFields[ipos++].Replace("'", "");
			if (schema != ReleaseVersion.IFC2x3)
			{
				try
				{
					m.mDescription = arrFields[ipos++].Replace("'", "");
					m.mCategory = arrFields[ipos++].Replace("'", "");
				}
				catch (Exception) { }
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mName + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "'" : (mDescription == "$" ? "',$," : "','" + mDescription + "',") + (mCategory == "$" ? "$" : "'" + mCategory + "'")); }
		internal void associateElement(IfcElement el) { Associates.mRelatedObjects.Add(el.mIndex); }
		internal void associateElement(IfcElementType eltype) { Associates.mRelatedObjects.Add(eltype.mIndex); }
		internal void associateElement(IfcStructuralMember memb) { Associates.mRelatedObjects.Add(memb.mIndex); }
	}
	public partial class IfcMaterialClassificationRelationship : BaseClassIfc
	{
		internal List<int> mMaterialClassifications = new List<int>();// : SET [1:?] OF IfcClassificationNotationSelect;
		internal int mClassifiedMaterial;// : IfcMaterial;
		internal IfcMaterialClassificationRelationship() : base() { }
		internal IfcMaterialClassificationRelationship(IfcMaterialClassificationRelationship m) : base() { mMaterialClassifications = new List<int>(m.mMaterialClassifications.ToArray()); mClassifiedMaterial = m.mClassifiedMaterial; }
		internal IfcMaterialClassificationRelationship(DatabaseIfc db) : base(db) { }
		internal static IfcMaterialClassificationRelationship Parse(string strDef) { IfcMaterialClassificationRelationship r = new IfcMaterialClassificationRelationship(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
		internal static void parseFields(IfcMaterialClassificationRelationship r, List<string> arrFields, ref int ipos) { r.mMaterialClassifications = ParserSTEP.SplitListLinks(arrFields[ipos++]); r.mClassifiedMaterial = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mMaterialClassifications[0]);
			for (int icounter = 1; icounter < mMaterialClassifications.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterialClassifications[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mClassifiedMaterial);
		}
	}
	public partial class IfcMaterialConstituent : IfcMaterialDefinition //IFC4
	{
		internal string mName = "$";// :	OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText 
		internal int mMaterial;// : IfcMaterial;
		internal double mFraction;//	 :	OPTIONAL IfcNormalisedRatioMeasure;
		internal string mCategory = "$";//	 :	OPTIONAL IfcLabel;  

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcMaterial Material { get { return mDatabase[mMaterial] as IfcMaterial; } set { mMaterial = (value == null ? 0 : value.mIndex); } }
		public double Fraction { get { return mFraction; } set { mFraction = value; } }
		public string Category { get { return (mCategory == "$" ? "" : ParserIfc.Decode(mCategory)); } set { mCategory = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		public override IfcMaterial PrimaryMaterial { get { return Material; } }
		
		internal IfcMaterialConstituent() : base() { }
		public IfcMaterialConstituent(string name, string desc, IfcMaterial mat, double fraction, string category)
			: base(mat.mDatabase)
		{
			Name = name;
			Description = desc;
			mMaterial = mat.mIndex;
			mFraction = fraction;
			Category = category;
		}
		internal static IfcMaterialConstituent Parse(string strDef) { IfcMaterialConstituent m = new IfcMaterialConstituent(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcMaterialConstituent m, List<string> arrFields, ref int ipos)
		{
			IfcMaterialDefinition.parseFields(m, arrFields, ref ipos);
			m.mName = arrFields[ipos++].Replace("'", "");
			m.mDescription = arrFields[ipos++].Replace("'", "");
			m.mMaterial = ParserSTEP.ParseLink(arrFields[ipos++]);
			m.mFraction = ParserSTEP.ParseDouble(arrFields[ipos++]);
			m.mCategory = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserSTEP.LinkToString(mMaterial) + "," + ParserSTEP.DoubleToString(mFraction) + (mCategory == "$" ? ",$" : ",'" + mDescription + "'"); }
	}
	public partial class IfcMaterialConstituentSet : IfcMaterialDefinition
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText 
		internal List<int> mMaterialConstituents = new List<int>();// LIST [1:?] OF IfcMaterialConstituent;

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public List<IfcMaterialConstituent> MaterialConstituents { get { return mMaterialConstituents.ConvertAll(x => mDatabase[x] as IfcMaterialConstituent); } set { mMaterialConstituents = value.ConvertAll(x => x.mIndex); } }

		public override IfcMaterial PrimaryMaterial { get { return MaterialConstituents[0].PrimaryMaterial; } }

		internal IfcMaterialConstituentSet() : base() { }
		public IfcMaterialConstituentSet(string name, string desc, List<IfcMaterialConstituent> mcs)
			: base(mcs[0].mDatabase)
		{
			Name = name;
			Description = desc;
			mMaterialConstituents = mcs.ConvertAll(x => x.mIndex);
		}
		internal static IfcMaterialConstituentSet Parse(string strDef) { IfcMaterialConstituentSet m = new IfcMaterialConstituentSet(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcMaterialConstituentSet m, List<string> arrFields, ref int ipos)
		{
			IfcMaterialDefinition.parseFields(m, arrFields, ref ipos);
			m.mName = arrFields[ipos++].Replace("'", "");
			m.mDescription = arrFields[ipos++].Replace("'", "");
			m.mMaterialConstituents = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			string str = ParserSTEP.LinkToString(mMaterialConstituents[0]);
			for (int icounter = 1; icounter < mMaterialConstituents.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterialConstituents[icounter]);
			return base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,(" : "'" + mDescription + "',(") + str + ")";
		}
	}
	public abstract partial class IfcMaterialDefinition : BaseClassIfc, IfcMaterialSelect, IfcResourceObjectSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcMaterial ,IfcMaterialConstituent ,IfcMaterialConstituentSet ,IfcMaterialLayer ,IfcMaterialProfile ,IfcMaterialProfileSet));
	{ 
		//INVERSE  
		internal List<IfcRelAssociatesMaterial> mAssociatedTo = new List<IfcRelAssociatesMaterial>();
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		private List<IfcMaterialProperties> mHasProperties = new List<IfcMaterialProperties>();
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public IfcRelAssociatesMaterial Associates
		{
			get
			{
				if (mAssociatedTo.Count == 0)
				{
					new IfcRelAssociatesMaterial(this);
				}
				return mAssociatedTo[0];
			}
			set { mAssociatedTo.Add(value); }
		}
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcMaterialProperties> HasProperties { get { return mHasProperties; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		public virtual IfcMaterial PrimaryMaterial { get { return null; } }

		protected IfcMaterialDefinition() : base() { }
		protected IfcMaterialDefinition(DatabaseIfc db) : base(db) { new IfcRelAssociatesMaterial(this); }
		protected static void parseFields(IfcMaterialDefinition m, List<string> arrFields, ref int ipos) { }

		internal void AddProperties(IfcMaterialProperties mp) { if(!mHasProperties.Contains(mp)) mHasProperties.Add(mp); }
	}
	public partial class IfcMaterialDefinitionRepresentation : IfcProductRepresentation
	{
		internal int mRepresentedMaterial;// : IfcMaterial;

		public new List<IfcStyledRepresentation> Representations { get { return mRepresentations.ConvertAll(x =>mDatabase[x] as IfcStyledRepresentation); } }
		public IfcMaterial RepresentedMaterial { get { return mDatabase[mRepresentedMaterial] as IfcMaterial; } set { mRepresentedMaterial = value.mIndex; if (value.mHasRepresentation != this) value.HasRepresentation = this; } }

		internal IfcMaterialDefinitionRepresentation() : base() { }
		public IfcMaterialDefinitionRepresentation(IfcStyledRepresentation representation, IfcMaterial mat) : base(representation) { mRepresentedMaterial = mat.mIndex; mat.mHasRepresentation = this; }
		public IfcMaterialDefinitionRepresentation(List<IfcStyledRepresentation> representations, IfcMaterial mat) : base(representations.ConvertAll(x => x as IfcRepresentation)) { mRepresentedMaterial = mat.mIndex; mat.mHasRepresentation = this; }

		internal new static IfcMaterialDefinitionRepresentation Parse(string strDef) { IfcMaterialDefinitionRepresentation m = new IfcMaterialDefinitionRepresentation(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcMaterialDefinitionRepresentation m, List<string> arrFields, ref int ipos) { IfcProductRepresentation.parseFields(m, arrFields, ref ipos); m.mRepresentedMaterial = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRepresentedMaterial); }
	
		internal override void relate()
		{
			base.relate();
			IfcMaterial m = RepresentedMaterial;
			if (m != null)
				m.mHasRepresentation = this;
		}
	}
	public partial class IfcMaterialLayer : IfcMaterialDefinition
	{
		internal int mMaterial;// : OPTIONAL IfcMaterial;
		internal double mLayerThickness;// ::	IfcNonNegativeLengthMeasure IFC4Chagne IfcPositiveLengthMeasure;
		internal IfcLogicalEnum mIsVentilated = IfcLogicalEnum.UNKNOWN;// : OPTIONAL IfcLogical; 
		internal string mName = "$";// : OPTIONAL IfcLabel; IFC4
		internal string mDescription = "$";// : OPTIONAL IfcText; IFC4
		internal string mCategory = "$";// : OPTIONAL IfcLabel; IFC4
		internal double mPriority = double.NaN;//	 :	OPTIONAL IfcNormalisedRatioMeasure;

		public IfcMaterial Material { get { return mDatabase[mMaterial] as IfcMaterial; } set { mMaterial = (value == null ? 0 : value.mIndex); } }
		public double LayerThickness { get { return mLayerThickness; } set { mLayerThickness = value; } }
		public IfcLogicalEnum IsVentilated { get { return mIsVentilated; } set { mIsVentilated = value; } }
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Category { get { return (mCategory == "$" ? "" : ParserIfc.Decode(mCategory)); } set { mCategory = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public double Priority { get { return mPriority; } set { mPriority = value; } }

		public override IfcMaterial PrimaryMaterial { get { return Material; } }

		internal IfcMaterialLayer() : base() { }
		public IfcMaterialLayer(DatabaseIfc db, double thickness, string name) : base(db) { mLayerThickness = Math.Abs(thickness); Name = name; }
		public IfcMaterialLayer(IfcMaterial mat, double thickness, string name) : base(mat.mDatabase)
		{
			Material = mat;
			mLayerThickness = Math.Abs(thickness);
			Name = (string.IsNullOrEmpty(name) ? mat.Name : name);
		}
		internal static IfcMaterialLayer Parse(string strDef,ReleaseVersion schema) { IfcMaterialLayer m = new IfcMaterialLayer(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcMaterialLayer m, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcMaterialDefinition.parseFields(m, arrFields, ref ipos);
			m.mMaterial = ParserSTEP.ParseLink(arrFields[ipos++]);
			m.mLayerThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			m.mIsVentilated = ParserIfc.ParseIFCLogical(arrFields[ipos++]);
			try
			{
				if (schema != ReleaseVersion.IFC2x3)
				{
					m.mName = arrFields[ipos++].Replace("'", "");
					m.mDescription = arrFields[ipos++].Replace("'", "");
					m.mCategory = arrFields[ipos++].Replace("'", "");
					m.mPriority = ParserSTEP.ParseDouble(arrFields[ipos++]);
				}
			}
			catch (Exception) { }
		}
		protected override string BuildStringSTEP()
		{
			string s = (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + (mCategory == "$" ? "$," : "'" + mCategory + "',") + ParserSTEP.DoubleOptionalToString(mPriority));
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mMaterial) + "," + ParserSTEP.DoubleToString(mLayerThickness) + "," + ParserIfc.LogicalToString(mIsVentilated) + s;
		}
	}
	public partial class IfcMaterialLayerSet : IfcMaterialDefinition
	{
		private List<int> mMaterialLayers = new List<int>();// LIST [1:?] OF IfcMaterialLayer;
		private string mLayerSetName = "$";// : OPTIONAL IfcLabel;
		private string mDescription = "$";// : OPTIONAL IfcText

		public List<IfcMaterialLayer> MaterialLayers { get { return mMaterialLayers.ConvertAll(x => (IfcMaterialLayer)mDatabase[x]); }  set { mMaterialLayers = value.ConvertAll(x => x.mIndex); } }
		public string LayerSetName { get { return (mLayerSetName == "$" ? "" : ParserIfc.Decode(mLayerSetName)); } set { mLayerSetName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		public override string Name { get { return LayerSetName; } set { LayerSetName = value; } }
		public override IfcMaterial PrimaryMaterial { get { return (mMaterialLayers.Count != 1 ? null : MaterialLayers[0].Material); } }
		
		internal IfcMaterialLayerSet() : base() { }
		protected IfcMaterialLayerSet(DatabaseIfc db) : base(db) { }
		public IfcMaterialLayerSet(IfcMaterialLayer layer, string name) : base(layer.mDatabase) { mMaterialLayers.Add(layer.mIndex); Name = name;  }
		public IfcMaterialLayerSet(List<IfcMaterialLayer> layers, string name) : base(layers[0].mDatabase)
		{
			Name = name;
			mMaterialLayers = layers.ConvertAll(x => x.mIndex);
		}
		internal static IfcMaterialLayerSet Parse(string strDef, ReleaseVersion schema) { IfcMaterialLayerSet m = new IfcMaterialLayerSet(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcMaterialLayerSet m, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcMaterialDefinition.parseFields(m, arrFields, ref ipos);
			m.mMaterialLayers = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			m.mLayerSetName = arrFields[ipos++].Replace("'", "");
			if (schema != ReleaseVersion.IFC2x3)
				m.mDescription = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mMaterialLayers[0]);
			for (int icounter = 1; icounter < mMaterialLayers.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterialLayers[icounter]);
			return str + (mLayerSetName == "$" ? "),$" : "),'" + mLayerSetName + "'") + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mDescription == "$" ? ",$" : ",'" + mDescription + "'"));
		}
	}
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

		public override IfcMaterial PrimaryMaterial { get { return ForLayerSet.PrimaryMaterial; } }	
		
		internal IfcMaterialLayerSetUsage() : base() { }
		public IfcMaterialLayerSetUsage(DatabaseIfc db, IfcMaterialLayerSet ls, IfcLayerSetDirectionEnum dir, IfcDirectionSenseEnum sense, double offset) : base(db)
		{
			mForLayerSet = ls.mIndex;
			mLayerSetDirection = dir;
			mDirectionSense = sense;
			mOffsetFromReferenceLine = offset;
		}
		internal static IfcMaterialLayerSetUsage Parse(string strDef,ReleaseVersion schema) { IfcMaterialLayerSetUsage u = new IfcMaterialLayerSetUsage(); int ipos = 0; parseFields(u, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return u; }
		internal static void parseFields(IfcMaterialLayerSetUsage m, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcMaterialUsageDefinition.parseFields(m, arrFields, ref ipos);
			m.mForLayerSet = ParserSTEP.ParseLink(arrFields[ipos++]);
			m.mLayerSetDirection = (IfcLayerSetDirectionEnum)Enum.Parse(typeof(IfcLayerSetDirectionEnum), arrFields[ipos++].Replace(".", ""));
			m.mDirectionSense = (IfcDirectionSenseEnum)Enum.Parse(typeof(IfcDirectionSenseEnum), arrFields[ipos++].Replace(".", ""));
			m.mOffsetFromReferenceLine = ParserSTEP.ParseDouble(arrFields[ipos++]);
			try
			{
				if (schema != ReleaseVersion.IFC2x3 && ipos + 1 < arrFields.Count)
					m.mReferenceExtent = ParserSTEP.ParseDouble(arrFields[ipos++]);
			}
			catch (Exception) { }
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mForLayerSet) + ",." + mLayerSetDirection.ToString() + ".,." + mDirectionSense.ToString() + ".," + ParserSTEP.DoubleToString(mOffsetFromReferenceLine) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.DoubleOptionalToString(mReferenceExtent)); }
	}
	public partial class IfcMaterialLayerSetWithOffsets : IfcMaterialLayerSet
	{
		internal IfcLayerSetDirectionEnum mOffsetDirection = IfcLayerSetDirectionEnum.AXIS1;
		internal double[] mOffsetValues = new double[2];// LIST [1:2] OF IfcLengthMeasure; 
		internal IfcMaterialLayerSetWithOffsets() : base() { }
		internal IfcMaterialLayerSetWithOffsets(IfcMaterialLayer layer, string name, IfcLayerSetDirectionEnum dir, double offset)
			: base(layer, name) { mOffsetDirection = dir; mOffsetValues[0] = offset; }
		internal IfcMaterialLayerSetWithOffsets(IfcMaterialLayer layer, string name, IfcLayerSetDirectionEnum dir, double offset1, double offset2)
			: this(layer, name,dir,offset1) { mOffsetValues[1] = offset2; }
		internal IfcMaterialLayerSetWithOffsets(List<IfcMaterialLayer> layers, string name, IfcLayerSetDirectionEnum dir, double offset)
			: base(layers, name) { mOffsetDirection = dir; mOffsetValues[0] = offset; }
		internal IfcMaterialLayerSetWithOffsets(List<IfcMaterialLayer> layers, string name, IfcLayerSetDirectionEnum dir, double offset1,double offset2)
			: this(layers, name,dir,offset1) { mOffsetValues[1] = offset2; }
		internal new static IfcMaterialLayerSetWithOffsets Parse(string strDef,ReleaseVersion schema) { IfcMaterialLayerSetWithOffsets m = new IfcMaterialLayerSetWithOffsets(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcMaterialLayerSetWithOffsets m, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcMaterialLayerSet.parseFields(m, arrFields, ref ipos,schema);
			m.mOffsetDirection = (IfcLayerSetDirectionEnum)Enum.Parse(typeof(IfcLayerSetDirectionEnum), arrFields[ipos++].Replace(".", ""));
			string s = arrFields[ipos++];
			string[] ss = s.Substring(1, s.Length - 2).Split(",".ToCharArray());
			m.mOffsetValues[0] = ParserSTEP.ParseDouble(ss[0]);
			if(ss.Length > 1)
				m.mOffsetValues[1] = ParserSTEP.ParseDouble(ss[1]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mOffsetDirection.ToString() + ".,(" + ParserSTEP.DoubleToString( mOffsetValues[0]) + (Math.Abs(mOffsetValues[1]) > mDatabase.Tolerance ? "," + ParserSTEP.DoubleToString( mOffsetValues[1]) : "") + ")"; }
	}
	public partial class IfcMaterialLayerWithOffsets : IfcMaterialLayer
	{
		internal IfcLayerSetDirectionEnum mOffsetDirection = IfcLayerSetDirectionEnum.AXIS1;// : IfcLayerSetDirectionEnum;
		internal double[] mOffsetValues = new double[2];// : ARRAY [1:2] OF IfcLengthMeasure; 
		internal IfcMaterialLayerWithOffsets() : base() { }
		internal IfcMaterialLayerWithOffsets(IfcMaterial mat, double thickness, string name, IfcLayerSetDirectionEnum d, double startOffset)
			: base(mat, thickness, name) { mOffsetDirection = d; mOffsetValues = new double[]{ startOffset}; }
		internal IfcMaterialLayerWithOffsets(IfcMaterial mat, double thickness, string name, IfcLayerSetDirectionEnum d, double startOffset,double endOffset)
			: base(mat, thickness, name) { mOffsetDirection = d; mOffsetValues = new double[]{ startOffset,endOffset}; }
		internal static IfcMaterialLayerWithOffsets Parse(string strDef) { IfcMaterialLayerWithOffsets m = new IfcMaterialLayerWithOffsets(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcMaterialLayerWithOffsets m, List<string> arrFields, ref int ipos)
		{
			IfcMaterialLayer.parseFields(m, arrFields, ref ipos);
			m.mOffsetDirection = (IfcLayerSetDirectionEnum)Enum.Parse(typeof(IfcLayerSetDirectionEnum), arrFields[ipos++].Replace(".", ""));
			string s = arrFields[ipos++];
			List<string> arrNodes = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			m.mOffsetValues = arrNodes.ConvertAll(x => ParserSTEP.ParseDouble(x)).ToArray();
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mOffsetDirection.ToString() + ".(," + ParserSTEP.DoubleToString(mOffsetValues[0]) + (mOffsetValues.Length > 1 ? "," + ParserSTEP.DoubleToString(mOffsetValues[1]) : "") + ")"; }
	}
	public partial class IfcMaterialList : BaseClassIfc, IfcMaterialSelect //DEPRECEATED IFC4
	{
		internal List<int> mMaterials = new List<int>();// LIST [1:?] OF IfcMaterial; 

		public List<IfcMaterial> Materials { get { return mMaterials.ConvertAll(x =>mDatabase[x] as IfcMaterial); } }

		internal List<IfcRelAssociatesMaterial> mAssociatedTo = new List<IfcRelAssociatesMaterial>();
		public IfcRelAssociatesMaterial Associates
		{
			get
			{
				if (mAssociatedTo.Count == 0)
				{
					new IfcRelAssociatesMaterial(this);
				}
				return mAssociatedTo[0];
			}
			set { mAssociatedTo.Add(value); }
		}
		
		public IfcMaterial PrimaryMaterial { get { return mDatabase[mMaterials[0]] as IfcMaterial; } }

		internal IfcMaterialList() : base() { }
		internal IfcMaterialList(IfcMaterialList m) : base() { mMaterials = new List<int>(m.mMaterials.ToArray()); }
		internal static IfcMaterialList Parse(string strDef) { IfcMaterialList m = new IfcMaterialList(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcMaterialList m, List<string> arrFields, ref int ipos) { m.mMaterials = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mMaterials[0]);
			for (int icounter = 1; icounter < mMaterials.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterials[icounter]);
			return str + ")";
		}
	}
	public partial class IfcMaterialProfile : IfcMaterialDefinition // IFC4
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal int mMaterial;// : OPTIONAL IfcMaterial;
		internal int mProfile;// : OPTIONAL IfcProfileDef;
		internal double mPriority;// : OPTIONAL IfcNormalisedRatioMeasure
		internal string mCategory = "$";// : OPTIONAL IfcLabel
		// INVERSE
		private IfcMaterialProfileSet mToMaterialProfileSet = null;// : IfcMaterialProfileSet FOR 
		
		public IfcMaterial Material { get { return mDatabase[mMaterial] as IfcMaterial; } set { mMaterial = (value == null ? 0 : value.mIndex); } }
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcProfileDef Profile { get { return mDatabase[mProfile] as IfcProfileDef; } set { mProfile = (value == null ? 0 : value.mIndex); } }
		public string Category { get { return (mCategory == "$" ? "" : ParserIfc.Decode(mCategory)); } set { mCategory = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public double Priority { get { return mPriority; } set { mPriority = value; } }
		public IfcMaterialProfileSet ToMaterialProfileSet { get { return mToMaterialProfileSet; } set { mToMaterialProfileSet = value; } }

		public override IfcMaterial PrimaryMaterial { get { return Material; } }

		internal IfcMaterialProfile() : base() { }
		public IfcMaterialProfile(DatabaseIfc db) : base(db) { }
		public IfcMaterialProfile(string name, IfcMaterial mat, IfcProfileDef p) : base(mat == null ? p.mDatabase : mat.mDatabase)
		{
			Name = name;
			Material = mat;
			Profile = p;
		}
		protected static void parseFields(IfcMaterialProfile l, List<string> arrFields, ref int ipos)
		{
			IfcMaterialDefinition.parseFields(l, arrFields, ref ipos);
			l.mName = arrFields[ipos++].Replace("'", "");
			l.mDescription = arrFields[ipos++].Replace("'", "");
			l.mMaterial = ParserSTEP.ParseLink(arrFields[ipos++]);
			l.mProfile = ParserSTEP.ParseLink(arrFields[ipos++]);
			l.mPriority = ParserSTEP.ParseDouble(arrFields[ipos++]);
			l.mCategory = arrFields[ipos++].Replace("'", "");
		}
		internal static IfcMaterialProfile Parse(string strDef) { IfcMaterialProfile m = new IfcMaterialProfile(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserSTEP.LinkToString(mMaterial) + "," + ParserSTEP.LinkToString(mProfile) + "," + ParserSTEP.DoubleToString(mPriority) + (mCategory == "$" ? ",$" : ",'" + mCategory + "'")); }
	}
	public partial class IfcMaterialProfileSet : IfcMaterialDefinition //IFC4
	{
		internal string mName = "$"; //: OPTIONAL IfcLabel;
		internal string mDescription = "$"; //: OPTIONAL IfcText; 
		internal List<int> mMaterialProfiles = new List<int>();// LIST [1:?] OF IfcMaterialProfile;
		internal int mCompositeProfile;// : OPTIONAL IfcCompositeProfileDef; 

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public List<IfcMaterialProfile> MaterialProfiles { get { return mMaterialProfiles.ConvertAll(x=>mDatabase[x] as IfcMaterialProfile); } set { mMaterialProfiles = value.ConvertAll(x => x.mIndex); } }
		public IfcCompositeProfileDef CompositeProfile { get { return mDatabase[mCompositeProfile] as IfcCompositeProfileDef; } set { mCompositeProfile = (value == null ? 0 : value.mIndex);  } }

		public override IfcMaterial PrimaryMaterial { get { return (mMaterialProfiles.Count != 1 ? null : MaterialProfiles[0].Material); } }

		//GeomGym
		internal IfcMaterialProfileSet mTaperEnd = null;

		internal IfcMaterialProfileSet() : base() { }
		private IfcMaterialProfileSet(DatabaseIfc m, string name) : base(m) { Name = name; }
		public IfcMaterialProfileSet(string name, IfcMaterialProfile profile) : base(profile.mDatabase)
		{
			Name = name;
			
			if (profile.ToMaterialProfileSet == null)
				profile.ToMaterialProfileSet = this;
			else
				throw new Exception("Material Profile can be assigned to only a single profile set");
			mMaterialProfiles.Add(profile.mIndex);
		}
		internal IfcMaterialProfileSet(string name, List<IfcMaterialProfile> profiles) : base(profiles[0].mDatabase)
		{
			List<IfcProfileDef> defs = new List<IfcProfileDef>(profiles.Count);
			for (int icounter = 0; icounter < profiles.Count; icounter++)
			{
				IfcMaterialProfile mp = profiles[icounter];
				if (mp.ToMaterialProfileSet == null)
					mp.ToMaterialProfileSet = this;
				else
					throw new Exception("Material Profile can be assigned to only a single profile set");
				mMaterialProfiles.Add(mp.mIndex);
				if (mp.mProfile > 0)
					defs.Add(mDatabase[mp.mProfile] as IfcProfileDef);
			}
			if (defs.Count > 0)
				mCompositeProfile = new IfcCompositeProfileDef(name, defs).mIndex;
		}
		internal static IfcMaterialProfileSet Parse(string strDef) { IfcMaterialProfileSet m = new IfcMaterialProfileSet(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcMaterialProfileSet m, List<string> arrFields, ref int ipos) { IfcMaterialDefinition.parseFields(m, arrFields, ref ipos); m.mName = arrFields[ipos++].Replace("'", ""); m.mDescription = arrFields[ipos++].Replace("'", ""); m.mMaterialProfiles = ParserSTEP.SplitListLinks(arrFields[ipos++]); m.mCompositeProfile = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mMaterialProfiles.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,(" : "'" + mDescription + "',(") + ParserSTEP.LinkToString(mMaterialProfiles[0]);
			for (int icounter = 1; icounter < mMaterialProfiles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterialProfiles[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mCompositeProfile);
		}
	}
	public partial class IfcMaterialProfileSetUsage : IfcMaterialUsageDefinition //IFC4
	{
		internal int mForProfileSet;// : IfcMaterialProfileSet;
		internal IfcCardinalPointReference mCardinalPoint = IfcCardinalPointReference.MID;// 	:	OPTIONAL IfcCardinalPointReference;
		internal double mReferenceExtent = double.NaN;// 	:	OPTIONAL IfcPositiveLengthMeasure;  

		public IfcMaterialProfileSet ForProfileSet { get { return mDatabase[mForProfileSet] as IfcMaterialProfileSet; } set { mForProfileSet = value.mIndex; } }
		public IfcCardinalPointReference CardinalPoint { get { return mCardinalPoint; } set { mCardinalPoint = value; } }
		public double ReferenceExtent { get { return mReferenceExtent; } set { mReferenceExtent = value; } }

		public override IfcMaterial PrimaryMaterial { get { return ForProfileSet.PrimaryMaterial; } }

		internal IfcMaterialProfileSetUsage() : base() { }
		internal IfcMaterialProfileSetUsage(DatabaseIfc db, IfcMaterialProfileSetUsage m) : base(db,m) { ForProfileSet = db.Duplicate( m.ForProfileSet) as IfcMaterialProfileSet; mCardinalPoint = m.mCardinalPoint; mReferenceExtent = m.mReferenceExtent; }
		public IfcMaterialProfileSetUsage(IfcMaterialProfileSet ps)
			: base(ps.mDatabase) { ForProfileSet = ps; }
		public IfcMaterialProfileSetUsage(IfcMaterialProfileSet ps, IfcCardinalPointReference ip)
			: this(ps) { mCardinalPoint = ip; }
		internal static IfcMaterialProfileSetUsage Parse(string strDef) { IfcMaterialProfileSetUsage u = new IfcMaterialProfileSetUsage(); int ipos = 0; parseFields(u, ParserSTEP.SplitLineFields(strDef), ref ipos); return u; }
		internal static void parseFields(IfcMaterialProfileSetUsage m, List<string> arrFields, ref int ipos) { IfcMaterialUsageDefinition.parseFields(m, arrFields, ref ipos); m.mForProfileSet = ParserSTEP.ParseLink(arrFields[ipos++]); m.mCardinalPoint = (IfcCardinalPointReference)ParserSTEP.ParseInt(arrFields[ipos++]); m.mReferenceExtent = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mAssociatedTo == null || Associates.mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mForProfileSet) + "," + (mCardinalPoint == IfcCardinalPointReference.DEFAULT ? "$" : ((int)mCardinalPoint).ToString()) + "," + ParserSTEP.DoubleOptionalToString(mReferenceExtent)); }

	}
	public partial class IfcMaterialProfileSetUsageTapering : IfcMaterialProfileSetUsage //IFC4
	{
		internal int mForProfileEndSet;// : IfcMaterialProfileSet;
		internal IfcCardinalPointReference mCardinalEndPoint = IfcCardinalPointReference.MID;// : IfcCardinalPointReference 
		 
		public IfcMaterialProfileSet ForProfileEndSet { get { return mDatabase[mForProfileEndSet] as IfcMaterialProfileSet; } set { mForProfileEndSet = value.mIndex; } }
		public IfcCardinalPointReference CardinalEndPoint { get { return mCardinalEndPoint; } set { mCardinalEndPoint = value; } }

		internal IfcMaterialProfileSetUsageTapering() : base() { }
		public IfcMaterialProfileSetUsageTapering(IfcMaterialProfileSet ps, IfcCardinalPointReference ip, IfcMaterialProfileSet eps, IfcCardinalPointReference eip)
			: base(ps, ip) { mForProfileEndSet = eps.mIndex; mCardinalEndPoint = eip; }
		internal new static IfcMaterialProfileSetUsageTapering Parse(string strDef) { IfcMaterialProfileSetUsageTapering u = new IfcMaterialProfileSetUsageTapering(); int ipos = 0; parseFields(u, ParserSTEP.SplitLineFields(strDef), ref ipos); return u; }
		internal static void parseFields(IfcMaterialProfileSetUsageTapering m, List<string> arrFields, ref int ipos) { IfcMaterialProfileSetUsage.parseFields(m, arrFields, ref ipos); m.mForProfileEndSet = ParserSTEP.ParseLink(arrFields[ipos++]); m.mCardinalEndPoint = (IfcCardinalPointReference)ParserSTEP.ParseInt(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mAssociatedTo == null || Associates.mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mForProfileEndSet) + "," + (int)mCardinalEndPoint); }
	}
	public partial class IfcMaterialProfileWithOffsets : IfcMaterialProfile //IFC4
	{
		internal double[] mOffsetValues = new double[2];// ARRAY [1:2] OF IfcLengthMeasure;
		internal IfcMaterialProfileWithOffsets() : base() { }
		public IfcMaterialProfileWithOffsets(string name, IfcMaterial mat, IfcProfileDef p, double startOffset)
			: base(name, mat, p) { mOffsetValues = new double[] { startOffset }; }
		public IfcMaterialProfileWithOffsets(string name, IfcMaterial mat, IfcProfileDef p, double startOffset,double endOffset)
			: base(name, mat, p) { mOffsetValues = new double[] { startOffset,endOffset }; }
		internal new static IfcMaterialProfileWithOffsets Parse(string strDef) { IfcMaterialProfileWithOffsets u = new IfcMaterialProfileWithOffsets(); int ipos = 0; parseFields(u, ParserSTEP.SplitLineFields(strDef), ref ipos); return u; }
		internal static void parseFields(IfcMaterialProfileWithOffsets m, List<string> arrFields, ref int ipos)
		{
			IfcMaterialProfile.parseFields(m, arrFields, ref ipos);
			string s = arrFields[ipos++];
			List<string> arrNodes = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			m.mOffsetValues = arrNodes.ConvertAll(x => ParserSTEP.ParseDouble(x)).ToArray();
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",(" + ParserSTEP.DoubleToString(mOffsetValues[0]) + (mOffsetValues.Length > 1 ? "," + ParserSTEP.DoubleToString(mOffsetValues[1]) + ")" : ")"); }
	}
	public abstract partial class IfcMaterialPropertiesSuperSeded : BaseClassIfc //ABSTRACT SUPERTYPE OF (ONE(IfcExtendedMaterialProperties,IfcFuelProperties,IfcGeneralMaterialProperties,IfcHygroscopicMaterialProperties,IfcMechanicalMaterialProperties,IfcOpticalMaterialProperties,IfcProductsOfCombustionProperties,IfcThermalMaterialProperties,IfcWaterProperties));
	{
		internal int mMaterial;// : IfcMaterial;  
		public IfcMaterial Material { get { return mDatabase[mMaterial] as IfcMaterial; } set { mMaterial = value.mIndex; } }

		protected IfcMaterialPropertiesSuperSeded() : base() { }
		protected IfcMaterialPropertiesSuperSeded(IfcMaterial mat) : base(mat.mDatabase) { if (mat.mDatabase.mRelease != ReleaseVersion.IFC2x3) throw new Exception(KeyWord + " Depreceated IFC4!"); mMaterial = mat.mIndex; }
		protected IfcMaterialPropertiesSuperSeded(DatabaseIfc db, IfcMaterialPropertiesSuperSeded m) : base(db) { Material = db.Duplicate(m.Material) as IfcMaterial; }
		protected static void parseFields(IfcMaterialPropertiesSuperSeded p, List<string> arrFields, ref int ipos) { p.mMaterial = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mMaterial); }
		internal void relate()
		{
			IfcMaterial m =mDatabase[mMaterial] as IfcMaterial;
			if (m != null)
				m.mHasPropertiesSS.Add(this);
		}
	}
	public partial class IfcMaterialProperties : IfcExtendedProperties //IFC4
	{
		public override string KeyWord { get { return "IfcMaterialProperties"; } }
		private int mMaterial;// : IfcMaterialDefinition; 
		public IfcMaterialDefinition Material { get { return mDatabase[mMaterial] as IfcMaterialDefinition; } set { mMaterial = value.mIndex; value.AddProperties(this); } }

		internal IfcMaterialProperties() : base() { }
		internal IfcMaterialProperties(DatabaseIfc db, IfcMaterialProperties p) : base(db, p) { Material = db.Duplicate(p.Material) as IfcMaterialDefinition; }
		public IfcMaterialProperties(string name, IfcMaterialDefinition mat) : base(mat.mDatabase) { Name = name; mMaterial = mat.mIndex; mat.AddProperties(this); }
		internal IfcMaterialProperties(string name, List<IfcProperty> props, IfcMaterialDefinition mat) : base(name, props) { mMaterial = mat.mIndex; mat.AddProperties(this); }

		internal static IfcMaterialProperties Parse(string strDef, ReleaseVersion schema) { IfcMaterialProperties b = new IfcMaterialProperties(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return b; }
		internal static void parseFields(IfcMaterialProperties b, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcExtendedProperties.parseFields(b, arrFields, ref ipos,schema); b.mMaterial = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
				return "";
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mMaterial);
		}
		internal void relate()
		{
			IfcMaterialDefinition md = Material;
			if (md != null)
				md.AddProperties(this);
		}
	}
	public partial class IfcMaterialRelationship : IfcResourceLevelRelationship //IFC4
	{
		internal int mRelatingMaterial;// : IfcMaterial;
		internal List<int> mRelatedMaterials = new List<int>(); //	:	SET [1:?] OF IfcMaterial;
		internal string mExpression = "$";//:	OPTIONAL IfcLabel;
		internal IfcMaterialRelationship() : base() { }
		internal IfcMaterialRelationship(IfcMaterialRelationship i) : base(i) { mRelatingMaterial = i.mRelatingMaterial; mRelatedMaterials.AddRange(i.mRelatedMaterials); mExpression = i.mExpression; }
		internal IfcMaterialRelationship(IfcMaterial mat, List<IfcMaterial> related, string expr)
			: base(mat.mDatabase) { mRelatingMaterial = mat.mIndex; mRelatedMaterials = related.ConvertAll(x => x.mIndex); if (!string.IsNullOrEmpty(expr)) mExpression = expr.Replace("'", "");  }
		internal static IfcMaterialRelationship Parse(string strDef, ReleaseVersion schema) { IfcMaterialRelationship m = new IfcMaterialRelationship(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcMaterialRelationship m, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcResourceLevelRelationship.parseFields(m, arrFields, ref ipos,schema);
			m.mRelatingMaterial = ParserSTEP.ParseLink(arrFields[ipos++]);
			m.mRelatedMaterials = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			m.mExpression = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildStringSTEP()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
				return "";
			string result = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingMaterial) + ",(" + ParserSTEP.LinkToString(mRelatedMaterials[0]);
			for (int icounter = 1; icounter < mRelatedMaterials.Count; icounter++)
				result += "," + ParserSTEP.LinkToString(mRelatedMaterials[icounter]);
			return result += mExpression == "$" ? "),$" : "),'" + mExpression + "'";

		}
		//internal override void relate()
		//{
		//	base.relate();
		//	IfcMaterial m = mModel.mIFCobjs[mRelatingMaterial] as IfcMaterial;
		//	if (m != null)
		//		m.mHasRepresentation = this;
		//}
	}
	public partial interface IfcMaterialSelect : IBaseClassIfc  //IFC4 SELECT (IfcMaterialDefinition ,IfcMaterialList ,IfcMaterialUsageDefinition);
	{
		IfcRelAssociatesMaterial Associates { get; set; }

		IfcMaterial PrimaryMaterial { get; } //Geometry Gym Property
	}
	public abstract partial class IfcMaterialUsageDefinition : BaseClassIfc, IfcMaterialSelect // ABSTRACT SUPERTYPE OF(ONEOF(IfcMaterialLayerSetUsage, IfcMaterialProfileSetUsage));
	{	//INVERSE 
		internal List<IfcRelAssociatesMaterial> mAssociatedTo = new List<IfcRelAssociatesMaterial>();
		public IfcRelAssociatesMaterial Associates
		{
			get
			{
				if (mAssociatedTo.Count == 0)
				{
					new IfcRelAssociatesMaterial(this);
				}
				return mAssociatedTo[0];
			}
			set { mAssociatedTo.Add(value); }
		}

		public abstract IfcMaterial PrimaryMaterial { get; }

		protected IfcMaterialUsageDefinition() : base() { }
		protected IfcMaterialUsageDefinition(DatabaseIfc db) : base(db) { IfcRelAssociatesMaterial am = new IfcRelAssociatesMaterial(this); }
		protected IfcMaterialUsageDefinition(DatabaseIfc db, IfcMaterialUsageDefinition d) : base(db) { }
		protected static void parseFields(IfcMaterialUsageDefinition m, List<string> arrFields, ref int ipos) { }
	}
	public partial class IfcMeasureWithUnit : BaseClassIfc, IfcAppliedValueSelect
	{
		internal IfcValue mValueComponent;// : IfcValue; 
		private string mVal;
		private int mUnitComponent;// : IfcUnit; 

		public IfcUnit UnitComponent { get { return mDatabase[mUnitComponent] as IfcUnit; } set { mUnitComponent = value.Index; } }

		internal IfcMeasureWithUnit() : base() { }
		internal IfcMeasureWithUnit(IfcValue v, IfcUnit u) : base(u.Database) { mValueComponent = v; mUnitComponent = u.Index; }
		internal IfcMeasureWithUnit(double value, IfcUnit u) : base(u.Database) { mValueComponent = new IfcReal(value); mUnitComponent = u.Index;  }
		internal static IfcMeasureWithUnit Parse(string strDef) { IfcMeasureWithUnit m = new IfcMeasureWithUnit(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcMeasureWithUnit m, List<string> arrFields, ref int ipos)
		{
			string s = arrFields[ipos++];
			m.mValueComponent = ParserIfc.parseValue(s);
			if (m.mValueComponent == null)
				m.mVal = s; m.mUnitComponent = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + (mValueComponent != null ? mValueComponent.ToString() : mVal) + "," + ParserSTEP.LinkToString(mUnitComponent); }
		internal double getSIFactor()
		{
			IfcUnit u = UnitComponent;
			IfcSIUnit si = u as IfcSIUnit;
			double sif = (si == null ? 1 : si.getSIFactor());
			IfcMeasureValue m = mValueComponent as IfcMeasureValue;

			if (m != null)
				return m.Measure * sif;
			IfcDerivedMeasureValue dm = mValueComponent as IfcDerivedMeasureValue;
			if (dm != null)
				return dm.Measure * sif;
			return sif;
		}
	}
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
		internal new static IfcMechanicalConcreteMaterialProperties Parse(string strDef) { IfcMechanicalConcreteMaterialProperties p = new IfcMechanicalConcreteMaterialProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcMechanicalConcreteMaterialProperties p, List<string> arrFields, ref int ipos)
		{
			IfcMechanicalMaterialProperties.parseFields(p, arrFields, ref ipos);
			p.mCompressiveStrength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mMaxAggregateSize = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mAdmixturesDescription = arrFields[ipos++];
			p.mWorkability = arrFields[ipos++];
			p.mProtectivePoreRatio = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mWaterImpermeability = arrFields[ipos++];
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mCompressiveStrength) + "," + ParserSTEP.DoubleOptionalToString(mMaxAggregateSize) + (mAdmixturesDescription == "$" ? ",$," : ",'" + mAdmixturesDescription + "',") + (mWorkability == "$" ? "$," : "'" + mWorkability + "',") + ParserSTEP.DoubleOptionalToString(mProtectivePoreRatio) + (mWaterImpermeability== "$" ? ",$" : ",'" + mWaterImpermeability + "'"); }
	}
	public partial class IfcMechanicalFastener : IfcElementComponent //IFC4 change
	{
		internal double mNominalDiameter;// : OPTIONAL IfcPositiveLengthMeasure; IFC4 depreceated
		internal double mNominalLength;// : OPTIONAL IfcPositiveLengthMeasure;  IFC4 depreceated
		internal IfcMechanicalFastenerTypeEnum mPredefinedType = IfcMechanicalFastenerTypeEnum.NOTDEFINED;// : OPTIONAL IfcMechanicalFastenerTypeEnum;
		public IfcMechanicalFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMechanicalFastener() : base() { }
		internal IfcMechanicalFastener(DatabaseIfc db, IfcMechanicalFastener f) : base(db, f) { mNominalDiameter = f.mNominalDiameter; mNominalLength = f.mNominalLength; }
		internal IfcMechanicalFastener(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		
		internal static IfcMechanicalFastener Parse(string strDef,ReleaseVersion schema) { IfcMechanicalFastener f = new IfcMechanicalFastener(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return f; }
		internal static void parseFields(IfcMechanicalFastener f, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcElementComponent.parseFields(f, arrFields, ref ipos);
			f.mNominalDiameter = ParserSTEP.ParseDouble(arrFields[ipos++]);
			f.mNominalLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					f.mPredefinedType = (IfcMechanicalFastenerTypeEnum)Enum.Parse(typeof(IfcMechanicalFastenerTypeEnum), s.Replace(".", ""));
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + "," + ParserSTEP.DoubleOptionalToString(mNominalLength) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcMechanicalFastenerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
	}
	public partial class IfcMechanicalFastenerType : IfcElementComponentType //IFC4 change
	{
		internal double mNominalDiameter;// : OPTIONAL IfcPositiveLengthMeasure; IFC4
		internal double mNominalLength;// : OPTIONAL IfcPositiveLengthMeasure; IFC4
		internal IfcMechanicalFastenerTypeEnum mPredefinedType = IfcMechanicalFastenerTypeEnum.NOTDEFINED;// : IfcMechanicalFastenerTypeEnum; IFC4
		public IfcMechanicalFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMechanicalFastenerType() : base() { }
		internal IfcShapeRepresentation mProfileRep = null;
		internal IfcMechanicalFastenerType(DatabaseIfc db, IfcMechanicalFastenerType t) : base(db, t) { mNominalDiameter = t.mNominalDiameter; mNominalLength = t.mNominalLength; mPredefinedType = t.mPredefinedType; }

		internal static IfcMechanicalFastenerType Parse(string strDef,ReleaseVersion schema) { IfcMechanicalFastenerType t = new IfcMechanicalFastenerType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return t; }
		internal static void parseFields(IfcMechanicalFastenerType t, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcElementComponentType.parseFields(t, arrFields, ref ipos);
			if (schema != ReleaseVersion.IFC2x3)
			{
				t.mPredefinedType = (IfcMechanicalFastenerTypeEnum)Enum.Parse(typeof(IfcMechanicalFastenerTypeEnum), arrFields[ipos++].Replace(".", ""));
				t.mNominalDiameter = ParserSTEP.ParseDouble(arrFields[ipos++]);
				t.mNominalLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + "," + ParserSTEP.DoubleOptionalToString(mNominalLength) + ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcMechanicalMaterialProperties : IfcMaterialPropertiesSuperSeded // DEPRECEATED IFC4
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
		internal static IfcMechanicalMaterialProperties Parse(string strDef) { IfcMechanicalMaterialProperties p = new IfcMechanicalMaterialProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcMechanicalMaterialProperties p, List<string> arrFields, ref int ipos)
		{
			IfcMaterialPropertiesSuperSeded.parseFields(p, arrFields, ref ipos);
			p.mDynamicViscosity = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mYoungModulus = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mShearModulus = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mPoissonRatio = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mThermalExpansionCoefficient = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mDynamicViscosity) + "," + ParserSTEP.DoubleOptionalToString(mYoungModulus) + "," + ParserSTEP.DoubleOptionalToString(mShearModulus) + "," + ParserSTEP.DoubleOptionalToString(mPoissonRatio) + "," + ParserSTEP.DoubleOptionalToString(mThermalExpansionCoefficient); }
	}
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
			Relaxations = p.Relaxations.ConvertAll(x=> db.Duplicate(x) as IfcRelaxation);
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
		internal new static IfcMechanicalSteelMaterialProperties Parse(string strDef) { IfcMechanicalSteelMaterialProperties p = new IfcMechanicalSteelMaterialProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcMechanicalSteelMaterialProperties p, List<string> arrFields, ref int ipos)
		{
			IfcMechanicalMaterialProperties.parseFields(p, arrFields, ref ipos);
			p.mYieldStress = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mUltimateStress = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mUltimateStrain = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mHardeningModule = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mProportionalStress = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mPlasticStrain = ParserSTEP.ParseDouble(arrFields[ipos++]);
			string str = arrFields[ipos++];
			if (str != "$")
				p.mRelaxations = ParserSTEP.SplitListLinks(str);
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mYieldStress) + "," + ParserSTEP.DoubleOptionalToString(mUltimateStress) + "," + ParserSTEP.DoubleOptionalToString(mUltimateStrain) + "," + ParserSTEP.DoubleOptionalToString(mHardeningModule) + "," + ParserSTEP.DoubleOptionalToString(mProportionalStress) + "," + ParserSTEP.DoubleOptionalToString(mPlasticStrain);
			if (mRelaxations.Count == 0)
				return str + ",$";
			str += ",(" + ParserSTEP.LinkToString(mRelaxations[0]);
			for (int icounter = 1; icounter < mRelaxations.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelaxations[icounter]);
			return str += ")";
		}
	}
	public partial class IfcMedicalDevice : IfcFlowTerminal //IFC4
	{
		internal IfcMedicalDeviceTypeEnum mPredefinedType = IfcMedicalDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcMedicalDeviceTypeEnum;
		public IfcMedicalDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMedicalDevice() : base() { }
		internal IfcMedicalDevice(DatabaseIfc db, IfcMedicalDevice d) : base(db, d) { mPredefinedType = d.mPredefinedType; }
		internal static void parseFields(IfcMedicalDevice s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcMedicalDeviceTypeEnum)Enum.Parse(typeof(IfcMedicalDeviceTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcMedicalDevice Parse(string strDef) { IfcMedicalDevice s = new IfcMedicalDevice(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcMedicalDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcMedicalDeviceType : IfcFlowTerminalType
	{
		internal IfcMedicalDeviceTypeEnum mPredefinedType = IfcMedicalDeviceTypeEnum.NOTDEFINED;// : IfcMedicalDeviceBoxTypeEnum; 
		public IfcMedicalDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMedicalDeviceType() : base() { }
		internal IfcMedicalDeviceType(DatabaseIfc db, IfcMedicalDeviceType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcMedicalDeviceType(DatabaseIfc m, string name, IfcMedicalDeviceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcMedicalDeviceType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcMedicalDeviceTypeEnum)Enum.Parse(typeof(IfcMedicalDeviceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcMedicalDeviceType Parse(string strDef) { IfcMedicalDeviceType t = new IfcMedicalDeviceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }

	}
	public partial class IfcMember : IfcBuildingElement
	{
		internal IfcMemberTypeEnum mPredefinedType = IfcMemberTypeEnum.NOTDEFINED;//: OPTIONAL IfcMemberTypeEnum; 
		public IfcMemberTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMember() : base() { }
		public IfcMember(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcMember Parse(string strDef, ReleaseVersion schema) { IfcMember m = new IfcMember(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcMember m, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcBuildingElement.parseFields(m, arrFields, ref ipos);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					m.mPredefinedType = (IfcMemberTypeEnum)Enum.Parse(typeof(IfcMemberTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcMemberTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }

	}
	public partial class IfcMemberStandardCase : IfcMember
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mDatabase.mModelView == ModelView.Ifc4Reference ? "IfcMember" : base.KeyWord); } }
		internal IfcMemberStandardCase() : base() { }

		internal new static IfcMemberStandardCase Parse(string strDef,ReleaseVersion schema) { IfcMemberStandardCase c = new IfcMemberStandardCase(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return c; }
		internal static void parseFields(IfcMemberStandardCase c, List<string> arrFields, ref int ipos,ReleaseVersion schema) { IfcMember.parseFields(c, arrFields, ref ipos,schema); }
	}
	public partial class IfcMemberType : IfcBuildingElementType
	{
		internal IfcMemberTypeEnum mPredefinedType = IfcMemberTypeEnum.NOTDEFINED;
		public IfcMemberTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMemberType() : base() { }
		internal IfcMemberType(DatabaseIfc db, IfcMemberType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		internal IfcMemberType(string name, IfcMaterialProfileSet ps, IfcMemberTypeEnum type) : base(ps.mDatabase)
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

		internal static void parseFields(IfcMemberType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcMemberTypeEnum)Enum.Parse(typeof(IfcMemberTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcMemberType Parse(string strDef) { IfcMemberType t = new IfcMemberType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }

	}
	public partial class IfcMetric : IfcConstraint//, IfcAppliedValueSelect
	{
		internal IfcBenchmarkEnum mBenchMark = IfcBenchmarkEnum.EQUALTO;// : OPTIONAL IfcLogicalOperatorEnum;
		internal string mValueSource = "$"; //	 :	OPTIONAL IfcLabel;
		private int mDataValue;// : OPTIONAL IfcMetricValueSelect;
		internal IfcValue mDataValueValue = null;
		private int mReferencePath;// :	OPTIONAL IfcReference;

		public IfcBenchmarkEnum BenchMark { get { return mBenchMark; } set { mBenchMark = value; } }
		public string ValueSource { get { return (mValueSource == "$" ? "" : ParserIfc.Decode(mValueSource)); } set { mValueSource = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcMetricValueSelect DataValue { get { return mDatabase[mDataValue] as IfcMetricValueSelect; } set { mDataValue = value.Index; } }
		public IfcValue Value { get { return mDataValueValue; } set { mDataValueValue = value; } }
		public IfcReference ReferencePath { get { return mDatabase[mReferencePath] as IfcReference; } set { mReferencePath = (value == null ? 0 : value.mIndex); } }

		internal IfcMetric() : base() { }
		internal IfcMetric(DatabaseIfc db, IfcMetric m) : base(db,m)
		{
			mBenchMark = m.mBenchMark;
			mValueSource = m.mValueSource;
			if(mDataValue > 0)
				DataValue = db.Duplicate(m.mDatabase[ m.mDataValue]) as IfcMetricValueSelect;
			mDataValueValue = m.mDataValueValue;
			if(mReferencePath > 0)
				ReferencePath = db.Duplicate( m.ReferencePath) as IfcReference;
		}
		public IfcMetric(DatabaseIfc db, string name, IfcConstraintEnum constraint) : base(db, name, constraint) { }
		
		internal static IfcMetric Parse(string strDef, ReleaseVersion schema) { IfcMetric m = new IfcMetric(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcMetric m, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcConstraint.parseFields(m, arrFields, ref ipos,schema);
			m.mBenchMark = (IfcBenchmarkEnum)Enum.Parse(typeof(IfcBenchmarkEnum), arrFields[ipos++].Replace(".", ""));
			m.mValueSource = arrFields[ipos++].Replace("'", "");
			string str = arrFields[ipos++];
			m.mDataValueValue = ParserIfc.parseValue(str);
			if (m.mDataValueValue == null)
				m.mDataValue = ParserSTEP.ParseLink(str);
			m.mReferencePath = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mBenchMark.ToString() + (mValueSource == "$" ? ".,$," : ".,'" + mValueSource + "',") + (mDataValueValue == null ? ParserSTEP.LinkToString(mDataValue) : mDataValueValue.ToString()) + "," + ParserSTEP.LinkToString(mReferencePath); }
	}
	public interface IfcMetricValueSelect : IBaseClassIfc { } //SELECT ( IfcMeasureWithUnit, IfcTable, IfcTimeSeries, IfcAppliedValue, IfcValue, IfcReference);
	public partial class IfcMirroredProfileDef : IfcDerivedProfileDef //SUPERTYPE OF(IfcMirroredProfileDef)
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcDerivedProfileDef" : base.KeyWord); } }
		internal IfcMirroredProfileDef() : base() { }
		public IfcMirroredProfileDef(IfcProfileDef p, string name) : base(p, new IfcCartesianTransformationOperator2D(p.mDatabase) { Axis1 = new IfcDirection(p.mDatabase, -1, 0) },name) { }

		internal static void parseFields(IfcMirroredProfileDef p, List<string> arrFields, ref int ipos) { IfcDerivedProfileDef.parseFields(p, arrFields, ref ipos); }
		internal new static IfcMirroredProfileDef Parse(string strDef) { int ipos = 0; IfcMirroredProfileDef p = new IfcMirroredProfileDef(); parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
	}
	public partial class IfcMonetaryUnit : BaseClassIfc, IfcUnit
	{
		internal string mCurrency = "USD";   //: IFC4 change	Ifc2x3 IfcCurrencyEnum 
		
		internal IfcMonetaryUnit() : base() { }
		internal IfcMonetaryUnit(IfcMonetaryUnit m) : base(m) { mCurrency = m.mCurrency; }
		internal IfcMonetaryUnit(DatabaseIfc db, string currency) : base(db) { mCurrency = currency; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ",." + mCurrency + "." : ",'" + mCurrency + "'"); }
		internal static void parseFields(IfcMonetaryUnit u, List<string> arrFields, ref int ipos,ReleaseVersion schema) { u.mCurrency = arrFields[ipos++].Replace(schema == ReleaseVersion.IFC2x3 ? "." : "'", ""); }
		internal static IfcMonetaryUnit Parse(string strDef,ReleaseVersion schema) { IfcMonetaryUnit u = new IfcMonetaryUnit(); int ipos = 0; parseFields(u, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return u; }
	}
	public partial class IfcMotorConnection : IfcEnergyConversionDevice //IFC4
	{
		internal IfcMotorConnectionTypeEnum mPredefinedType = IfcMotorConnectionTypeEnum.NOTDEFINED;// OPTIONAL : IfcMotorConnectionTypeEnum;
		public IfcMotorConnectionTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMotorConnection() : base() { }
		internal IfcMotorConnection(DatabaseIfc db, IfcMotorConnection c) : base(db, c) { mPredefinedType = c.mPredefinedType; }
		internal IfcMotorConnection(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcMotorConnection s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcMotorConnectionTypeEnum)Enum.Parse(typeof(IfcMotorConnectionTypeEnum), str);
		}
		internal new static IfcMotorConnection Parse(string strDef) { IfcMotorConnection s = new IfcMotorConnection(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcMotorConnectionTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcMotorConnectionType : IfcEnergyConversionDeviceType
	{
		internal IfcMotorConnectionTypeEnum mPredefinedType = IfcMotorConnectionTypeEnum.NOTDEFINED;// : IfcMotorConnectionTypeEnum; 
		public IfcMotorConnectionTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcMotorConnectionType() : base() { }
		internal IfcMotorConnectionType(DatabaseIfc db, IfcMotorConnectionType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcMotorConnectionType(DatabaseIfc m, string name, IfcMotorConnectionTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcMotorConnectionType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcMotorConnectionTypeEnum)Enum.Parse(typeof(IfcMotorConnectionTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcMotorConnectionType Parse(string strDef) { IfcMotorConnectionType t = new IfcMotorConnectionType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	//ENTITY IfcMove // DEPRECEATED IFC4
}
