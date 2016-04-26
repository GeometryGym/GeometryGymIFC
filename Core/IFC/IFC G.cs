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
	public partial class IfcGasTerminalType : IfcFlowTerminalType // DEPRECEATED IFC4
	{
		internal IfcGasTerminalTypeEnum mPredefinedType = IfcGasTerminalTypeEnum.NOTDEFINED;// : IfcGasTerminalBoxTypeEnum;
		internal IfcGasTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcGasTerminalType() : base() { }
		internal IfcGasTerminalType(IfcGasTerminalType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcGasTerminalType(DatabaseIfc m, string name, IfcGasTerminalTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcGasTerminalType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcGasTerminalTypeEnum)Enum.Parse(typeof(IfcGasTerminalTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcGasTerminalType Parse(string strDef) { IfcGasTerminalType t = new IfcGasTerminalType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcGeneralMaterialProperties : IfcMaterialPropertiesSuperSeded // DEPRECEATED IFC4
	{
		internal double mMolecularWeight; //: OPTIONAL IfcMolecularWeightMeasure;
		internal double mPorosity; //: OPTIONAL IfcNormalisedRatioMeasure;
		internal double mMassDensity;//OPTIONAL IfcMassDensityMeasure

		public double MolecularWeight { get { return mMolecularWeight; } set { mMolecularWeight = value; } }
		public double Porosity { get { return mPorosity; } set { mPorosity = value; } }
		public double MassDensity { get { return mMassDensity; } set { mMassDensity = value; } } 

		internal IfcGeneralMaterialProperties() : base() { }
		internal IfcGeneralMaterialProperties(IfcGeneralMaterialProperties p) : base(p) { mMolecularWeight = p.mMolecularWeight; mPorosity = p.mPorosity; mMassDensity = p.mMassDensity; }
		public IfcGeneralMaterialProperties(IfcMaterial material) : base(material) { }
		internal IfcGeneralMaterialProperties(IfcMaterial mat, double molecularWeight, double porosity, double massDensity) : base(mat)
		{
			mMolecularWeight = molecularWeight;
			mPorosity = porosity;
			mMassDensity = massDensity;
		}
		internal static IfcGeneralMaterialProperties Parse(string strDef) { IfcGeneralMaterialProperties p = new IfcGeneralMaterialProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcGeneralMaterialProperties p, List<string> arrFields, ref int ipos) { IfcMaterialPropertiesSuperSeded.parseFields(p, arrFields, ref ipos); p.mMolecularWeight = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mPorosity = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mMassDensity = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mMolecularWeight) + "," + ParserSTEP.DoubleOptionalToString(mPorosity) + "," + ParserSTEP.DoubleOptionalToString(mMassDensity); }
	}
	public class IfcGeneralProfileProperties : IfcProfileProperties //DELETED IFC4  SUPERTYPE OF	(IfcStructuralProfileProperties)
	{ 
		internal double mPhysicalWeight;// : OPTIONAL IfcMassPerLengthMeasure;
		internal double mPerimeter;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mMinimumPlateThickness;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mMaximumPlateThickness;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mCrossSectionArea;// : OPTIONAL IfcAreaMeasure;
		internal IfcGeneralProfileProperties() : base() { }
		internal IfcGeneralProfileProperties(IfcGeneralProfileProperties p) : base(p) { mPhysicalWeight = p.mPhysicalWeight; mPerimeter = p.mPerimeter; mMinimumPlateThickness = p.mMinimumPlateThickness; mMaximumPlateThickness = p.mMaximumPlateThickness; mCrossSectionArea = p.mCrossSectionArea; }
		internal IfcGeneralProfileProperties(string name, IfcProfileDef p) : base(name, p) { }
		internal IfcGeneralProfileProperties(string name, List<IfcProperty> props, IfcProfileDef p) : base(name, props, p) { }

		internal new static IfcGeneralProfileProperties Parse(string strDef,Schema schema) { IfcGeneralProfileProperties p = new IfcGeneralProfileProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcGeneralProfileProperties gp, List<string> arrFields, ref int ipos,Schema schema)
		{
			IfcProfileProperties.parseFields(gp, arrFields, ref ipos,schema);
			gp.mPhysicalWeight = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mPerimeter = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mMinimumPlateThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mMaximumPlateThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mCrossSectionArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildString() { return (mDatabase.mSchema != Schema.IFC2x3 ? "" : base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mPhysicalWeight) + "," + ParserSTEP.DoubleOptionalToString(mPerimeter) + "," + ParserSTEP.DoubleOptionalToString(mMinimumPlateThickness) + "," + ParserSTEP.DoubleOptionalToString(mMaximumPlateThickness) + "," + ParserSTEP.DoubleOptionalToString(mCrossSectionArea)); }
	}
	public partial class IfcGeographicElement : IfcElement  //IFC4
	{
		internal IfcGeographicElementTypeEnum mPredefinedType = IfcGeographicElementTypeEnum.NOTDEFINED;// OPTIONAL IfcGeographicElementTypeEnum; 
		public IfcGeographicElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcGeographicElement() : base() { }
		internal IfcGeographicElement(IfcGeographicElement e) : base(e) { mPredefinedType = e.mPredefinedType; }
		internal IfcGeographicElement(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { if (mDatabase.mSchema == Schema.IFC2x3) throw new Exception(KeyWord + " only supported in IFC4!"); }
		
		internal static IfcGeographicElement Parse(string strDef) { IfcGeographicElement e = new IfcGeographicElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		internal static void parseFields(IfcGeographicElement e, List<string> arrFields, ref int ipos)
		{
			IfcElement.parseFields(e, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				e.mPredefinedType = (IfcGeographicElementTypeEnum)Enum.Parse(typeof(IfcGeographicElementTypeEnum), s.Replace(".", ""));
		}
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString() + ",." + mPredefinedType + "."); }
	}
	public partial class IfcGeographicElementType : IfcElementType //IFC4
	{
		internal IfcGeographicElementTypeEnum mPredefinedType = IfcGeographicElementTypeEnum.NOTDEFINED;// IfcGeographicElementTypeEnum; 
		internal IfcGeographicElementType() : base() { }
		internal IfcGeographicElementType(IfcGeographicElementType o) : base(o) { mPredefinedType = o.mPredefinedType; }
		internal IfcGeographicElementType(DatabaseIfc m, string name, IfcGeographicElementTypeEnum type) : base(m) { Name = name; mPredefinedType = type; if (m.mSchema == Schema.IFC2x3) throw new Exception(KeyWord + " only supported in IFC4!"); }
		internal new static IfcGeographicElementType Parse(string strDef) { IfcGeographicElementType t = new IfcGeographicElementType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal static void parseFields(IfcGeographicElementType t, List<string> arrFields, ref int ipos)
		{
			IfcElementType.parseFields(t, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				t.mPredefinedType = (IfcGeographicElementTypeEnum)Enum.Parse(typeof(IfcGeographicElementTypeEnum), s.Replace(".", ""));
		}
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString() + ",." + mPredefinedType.ToString() + "."); }
	}
	public class IfcGeometricCurveSet : IfcGeometricSet
	{
		internal IfcGeometricCurveSet() : base() { }
		internal IfcGeometricCurveSet(IfcGeometricCurveSet p) : base(p) { }
		internal IfcGeometricCurveSet(List<IfcGeometricSetSelect> set) : base(set)
		{
			for (int icounter = 0; icounter < set.Count; icounter++)
			{
				IfcSurface s = set[icounter] as IfcSurface;
				if (s != null)
					throw new Exception("XXX Error, IfcSurface cannot be added to IfcGeometricCurveSet " + mIndex);
			}
		}
		internal static void parseFields(IfcGeometricCurveSet s, List<string> arrFields, ref int ipos) { IfcGeometricSet.parseFields(s, arrFields, ref ipos); }
		internal new static IfcGeometricCurveSet Parse(string strDef) { IfcGeometricCurveSet s = new IfcGeometricCurveSet(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public partial class IfcGeometricRepresentationContext : IfcRepresentationContext, IfcCoordinateReferenceSystemSelect
	{
		internal int mCoordinateSpaceDimension;// : IfcDimensionCount;
		internal double mPrecision = 1e-8;// : OPTIONAL REAL;
		internal int mWorldCoordinateSystem;// : IfcAxis2Placement;
		internal int mTrueNorth;// : OPTIONAL IfcDirection; 
		//INVERSE
		private List<IfcGeometricRepresentationSubContext> mHasSubContexts = new List<IfcGeometricRepresentationSubContext>();//	 :	SET OF IfcGeometricRepresentationSubContext FOR ParentContext;
		private IfcCoordinateOperation mHasCoordinateOperation = null; //IFC4

		public int CoordinateSpaceDimension { get { return mCoordinateSpaceDimension; } set { mCoordinateSpaceDimension = value; } }
		public double Precision { get { return mPrecision; } set { mPrecision = value; } }
		public IfcAxis2Placement WorldCoordinateSystem { get { return mDatabase.mIfcObjects[mWorldCoordinateSystem] as IfcAxis2Placement; } set { mWorldCoordinateSystem = (value == null ? 0 : value.Index); } }
		public IfcDirection TrueNorth 
		{ 
			get { return mDatabase.mIfcObjects[mTrueNorth] as IfcDirection; }  
			set 
			{
				if (value == null)
					mTrueNorth = 0;
				else
				{
					if (Math.Abs(value.CoordinateZ) > mDatabase.Tolerance)
						throw new Exception("True North direction must be 2 dimensional direction");
					mTrueNorth = value.mIndex; 
				}
			} 
		}
		public List<IfcGeometricRepresentationSubContext> HasSubContexts { get { return mHasSubContexts; } }
		public IfcCoordinateOperation HasCoordinateOperation { get { return mHasCoordinateOperation; } set { mHasCoordinateOperation = value; } }
		

		internal IfcGeometricRepresentationContext() : base() { }
		internal IfcGeometricRepresentationContext(IfcGeometricRepresentationContext p) : base(p) { mCoordinateSpaceDimension = p.mCoordinateSpaceDimension; mPrecision = p.mPrecision; mTrueNorth = p.mTrueNorth; }
		protected IfcGeometricRepresentationContext(DatabaseIfc m) : base(m) { }
		internal IfcGeometricRepresentationContext(DatabaseIfc m, int SpaceDimension, double precision)
			: base(m)
		{
			mCoordinateSpaceDimension = SpaceDimension;
			mPrecision = Math.Max(1e-8, precision);
			WorldCoordinateSystem = mDatabase.WorldCoordinatePlacement;
		}

		internal static void parseFields(IfcGeometricRepresentationContext c, List<string> arrFields, ref int ipos)
		{
			IfcRepresentationContext.parseFields(c, arrFields, ref ipos);
			c.mCoordinateSpaceDimension = ParserSTEP.ParseInt(arrFields[ipos++]);
			c.mPrecision = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mWorldCoordinateSystem = ParserSTEP.ParseLink(arrFields[ipos++]);
			c.mTrueNorth = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString()
		{
			if (this as IfcGeometricRepresentationSubContext != null)
				return base.BuildString() + ",*,*,*,*";
			
			return base.BuildString() + "," + (mCoordinateSpaceDimension == 0 ? "*" : mCoordinateSpaceDimension.ToString()) + "," + (mPrecision == 0 ? "*" : ParserSTEP.DoubleToString(mPrecision)) + "," + ParserSTEP.LinkToString(mWorldCoordinateSystem) + "," + ParserSTEP.LinkToString(mTrueNorth);
		}
		internal static IfcGeometricRepresentationContext Parse(string strDef) { IfcGeometricRepresentationContext c = new IfcGeometricRepresentationContext(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
	}
	public abstract partial class IfcGeometricRepresentationItem : IfcRepresentationItem 
	{
		//ABSTRACT SUPERTYPE OF (ONEOF (IfcAnnotationFillArea, IfcBooleanResult, IfcBoundingBox, IfcCartesianTransformationOperator, 
		//		IfcCompositeCurveSegment, IfcCsgPrimitive3D, IfcCurve, IfcDirection,IfcFaceBasedSurfaceModel, IfcFillAreaStyleHatching, 
		//		IfcFillAreaStyleTiles, IfcGeometricSet, IfcHalfSpaceSolid, IfcLightSource, IfcPlacement, IfcPlanarExtent, IfcPoint,
		//		IfcSectionedSpine, IfcShellBasedSurfaceModel, IfcSolidModel, IfcSurface, IfcTextLiteral, IfcVector))
		// IFC2x3 IfcAnnotationSurface, IfcDefinedSymbol, IfcDraughtingCallout, IfcFillAreaStyleTileSymbolWithStyle, IfcOneDirectionRepeatFactor
		// IFC4 IfcCartesianPointList, IfcTessellatedItem
		protected IfcGeometricRepresentationItem() : base() { }
		protected IfcGeometricRepresentationItem(IfcGeometricRepresentationItem p) : base(p) { }
		protected IfcGeometricRepresentationItem(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcGeometricRepresentationItem i, List<string> arrFields, ref int ipos) { IfcRepresentationItem.parseFields(i, arrFields, ref ipos); }
		protected override void Parse(string str, ref int ipos) { base.Parse(str, ref ipos); }
	}
	public class IfcGeometricRepresentationSubContext : IfcGeometricRepresentationContext
	{
		internal int mContainerContext;// : IfcGeometricRepresentationContext;
		internal double mTargetScale;// : OPTIONAL IfcPositiveRatioMeasure;
		private IfcGeometricProjectionEnum mTargetView;// : IfcGeometricProjectionEnum;
		internal string mUserDefinedTargetView = "$";// : OPTIONAL IfcLabel;

		internal IfcGeometricProjectionEnum TargetView { get { return mTargetView; } }
		public string UserDefinedTargetView { get { return (mUserDefinedTargetView == "$" ? "" : ParserIfc.Decode(mUserDefinedTargetView)); } set { mUserDefinedTargetView = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		internal IfcGeometricRepresentationSubContext() : base() { }
		internal IfcGeometricRepresentationSubContext(IfcGeometricRepresentationSubContext p)
			: base(p)
		{
			mContainerContext = p.mContainerContext;
			mTargetScale = p.mTargetScale;
			mTargetView = p.mTargetView;
			mUserDefinedTargetView = p.mUserDefinedTargetView;
		}
		internal IfcGeometricRepresentationSubContext(IfcGeometricRepresentationContext container, double scale, IfcGeometricProjectionEnum view)
			: base(container.mDatabase)
		{
			mContainerContext = container.mIndex;
			mContextType = container.mContextType;
			mTargetScale = scale;
			mTargetView = view;
		}
		internal static void parseFields(IfcGeometricRepresentationSubContext c, List<string> arrFields, ref int ipos)
		{
			IfcGeometricRepresentationContext.parseFields(c, arrFields, ref ipos);
			c.mContainerContext = ParserSTEP.ParseLink(arrFields[ipos++]);
			c.mTargetScale = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mTargetView = (IfcGeometricProjectionEnum)Enum.Parse(typeof(IfcGeometricProjectionEnum), arrFields[ipos++].Replace(".", ""));
			c.mUserDefinedTargetView = arrFields[ipos++];
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mContainerContext) + "," + ParserSTEP.DoubleOptionalToString(mTargetScale) + ",." + mTargetView.ToString() + (mUserDefinedTargetView == "$" ?  ".,$" : ".,'" + mUserDefinedTargetView + "'"); }
		internal new static IfcGeometricRepresentationSubContext Parse(string strDef) { IfcGeometricRepresentationSubContext c = new IfcGeometricRepresentationSubContext(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal void relate()
		{
			IfcGeometricRepresentationContext gc = mDatabase.mIfcObjects[mContainerContext] as IfcGeometricRepresentationContext;
			if (gc != null)
			{
				mActive = gc.Active;
				gc.HasSubContexts.Add(this);
			}
		}
	}
	public partial class IfcGeometricSet : IfcGeometricRepresentationItem //SUPERTYPE OF(IfcGeometricCurveSet)
	{
		private List<int> mElements = new List<int>(); //SET [1:?] OF IfcGeometricSetSelect; 
		internal List<IfcGeometricSetSelect> Elements { get { return mElements.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcGeometricSetSelect); } }
		internal IfcGeometricSet() : base() { }
		internal IfcGeometricSet(IfcGeometricSet p) : base(p) { mElements = new List<int>(p.mElements.ToArray()); }
		internal IfcGeometricSet(List<IfcGeometricSetSelect> set) : base(set[0].Database) { mElements = set.ConvertAll(x => x.Index); }
		protected override string BuildString()
		{
			if (mElements.Count == 0)
				return "";
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mElements[0]);
			for (int icounter = 1; icounter < mElements.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mElements[icounter]);
			return str + ")";
		}
		internal static void parseFields(IfcGeometricSet s, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(s, arrFields, ref ipos); s.mElements = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcGeometricSet Parse(string strDef) { IfcGeometricSet s = new IfcGeometricSet(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public partial interface IfcGeometricSetSelect : IfcInterface { } //SELECT ( IfcPoint, IfcCurve,  IfcSurface);
	public partial class IfcGrid : IfcProduct
	{
		private List<int> mUAxes = new List<int>();// : LIST [1:?] OF UNIQUE IfcGridAxis;
		private List<int> mVAxes = new List<int>();// : LIST [1:?] OF UNIQUE IfcGridAxis;
		private List<int> mWAxes = new List<int>();// : OPTIONAL LIST [1:?] OF UNIQUE IfcGridAxis;
		internal IfcGridTypeEnum mPredefinedType = IfcGridTypeEnum.NOTDEFINED;// :OPTIONAL IfcGridTypeEnum;
		//INVERSE
		internal IfcRelContainedInSpatialStructure mContainedInStructure = null;

		internal IfcGridTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public List<IfcGridAxis> UAxes { get { return mUAxes.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcGridAxis); } }
		public List<IfcGridAxis> VAxes { get { return mVAxes.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcGridAxis); } }
		public List<IfcGridAxis> WAxes { get { return mWAxes.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcGridAxis); } }

		internal IfcGrid() : base() { }
		internal IfcGrid(IfcGrid g) : base(g) { mUAxes = new List<int>(g.mUAxes.ToArray()); mVAxes = new List<int>(g.mVAxes.ToArray()); mWAxes = new List<int>(g.mWAxes.ToArray()); mPredefinedType = g.mPredefinedType; }

		internal static IfcGrid Parse(string strDef, Schema schema) { IfcGrid g = new IfcGrid(); int ipos = 0; parseFields(g, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return g; }
		internal static void parseFields(IfcGrid g, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcProduct.parseFields(g, arrFields, ref ipos);
			g.mUAxes = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			g.mVAxes = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			string s = arrFields[ipos++];
			if (s != "$")
				g.mWAxes = ParserSTEP.SplitListLinks(s);
			if (schema != Schema.IFC2x3)
			{
				s = arrFields[ipos++];
				if (s[0] == '.')
					g.mPredefinedType = (IfcGridTypeEnum)Enum.Parse(typeof(IfcGridTypeEnum), s.Replace(".", ""));
			}
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(";
			if (mUAxes.Count > 0)
			{
				str += ParserSTEP.LinkToString(mUAxes[0]);
				for (int icounter = 1; icounter < mUAxes.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mUAxes[icounter]);
			}
			str += "),(";
			if (mVAxes.Count > 0)
			{
				str += ParserSTEP.LinkToString(mVAxes[0]);
				for (int icounter = 1; icounter < mVAxes.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mVAxes[icounter]);
			}
			str += "),";
			if (mWAxes.Count > 0)
			{
				str += "(" + ParserSTEP.LinkToString(mWAxes[0]);
				for (int icounter = 1; icounter < mWAxes.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mWAxes[icounter]);
				return str + ")";
			}
			return str + "$" + (mDatabase.mSchema == Schema.IFC2x3 ? "" : ",." + mPredefinedType.ToString() + ".");
		}

		internal void AddUAxis(IfcGridAxis a) { mUAxes.Add(a.mIndex); a.mPartOfU = this; }
		internal void AddVAxis(IfcGridAxis a) { mVAxes.Add(a.mIndex); a.mPartOfV = this; }
		internal void AddWAxis(IfcGridAxis a) { mWAxes.Add(a.mIndex); a.mPartOfW = this; }
		internal void RemoveUAxis(IfcGridAxis a) { mUAxes.Remove(a.mIndex); a.mPartOfU = null; }
		internal void RemoveVAxis(IfcGridAxis a) { mVAxes.Remove(a.mIndex); a.mPartOfV = null; }
		internal void RemoveWAxis(IfcGridAxis a) { mWAxes.Remove(a.mIndex); a.mPartOfW = null; }
	}
	public partial class IfcGridAxis : BaseClassIfc
	{
		private string mAxisTag = "$";// : OPTIONAL IfcLabel;
		private int mAxisCurve;// : IfcCurve;
		internal bool mSameSense;// : IfcBoolean;

		//INVERSE
		internal IfcGrid mPartOfW = null;//	:	SET [0:1] OF IfcGrid FOR WAxes;
		internal IfcGrid mPartOfV = null;//	:	SET [0:1] OF IfcGrid FOR VAxes;
		internal IfcGrid mPartOfU = null;//	:	SET [0:1] OF IfcGrid FOR UAxes;
		internal List<IfcVirtualGridIntersection> mHasIntersections = new List<IfcVirtualGridIntersection>();//:	SET OF IfcVirtualGridIntersection FOR IntersectingAxes;

		public override string Name { get { return AxisTag; } set { AxisTag = value; } }
		public string AxisTag { get { return mAxisTag == "$" ? "" : ParserIfc.Decode(mAxisTag); } set { mAxisTag = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcCurve AxisCurve { get { return mDatabase.mIfcObjects[mAxisCurve] as IfcCurve; } }

		internal IfcGridAxis() : base() { }
		internal IfcGridAxis(IfcGridAxis p) : base() { mAxisTag = p.mAxisTag; mAxisCurve = p.mAxisCurve; mSameSense = p.mSameSense; }
		internal IfcGridAxis(DatabaseIfc m, string tag, IfcCurve axis, bool sameSense) : base(m) { if (!string.IsNullOrEmpty(tag)) mAxisTag = tag.Replace("'", ""); mAxisCurve = axis.mIndex; mSameSense = sameSense; }
		internal static IfcGridAxis Parse(string strDef) { IfcGridAxis a = new IfcGridAxis(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcGridAxis a, List<string> arrFields, ref int ipos) { a.mAxisTag = arrFields[ipos++].Replace("'", ""); a.mAxisCurve = ParserSTEP.ParseLink(arrFields[ipos++]); a.mSameSense = ParserSTEP.ParseBool(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + (mAxisTag == "$" ? ",$," : ",'" + mAxisTag + "',") + ParserSTEP.LinkToString(mAxisCurve) + "," + ParserSTEP.BoolToString(mSameSense); }
	}
	public partial class IfcGridPlacement : IfcObjectPlacement
	{
		internal int mPlacementLocation;// : IfcVirtualGridIntersection ;
		internal int mPlacementRefDirection;// : OPTIONAL IfcVirtualGridIntersection;
		internal IfcGridPlacement() : base() { }
		internal IfcGridPlacement(IfcGridPlacement p) : base(p) { mPlacementLocation = p.mPlacementLocation; mPlacementRefDirection = p.mPlacementRefDirection; }
		internal static IfcGridPlacement Parse(string strDef) { IfcGridPlacement p = new IfcGridPlacement(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcGridPlacement p, List<string> arrFields, ref int ipos) { IfcObjectPlacement.parseFields(p, arrFields, ref ipos); p.mPlacementLocation = ParserSTEP.ParseLink(arrFields[ipos++]); p.mPlacementRefDirection = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return (mDatabase.mOutputEssential || mPlacesObject.Count == 0 ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mPlacementLocation) + "," + ParserSTEP.LinkToString(mPlacementRefDirection)); }
	}
	public partial class IfcGroup : IfcObject //SUPERTYPE OF (ONEOF (IfcAsset ,IfcCondition ,IfcInventory ,IfcStructuralLoadGroup ,IfcStructuralResultGroup ,IfcSystem ,IfcZone))
	{
		//INVERSE
		internal List<IfcRelAssignsToGroup> mIsGroupedBy = new List<IfcRelAssignsToGroup>();// IFC4 SET : IfcRelAssignsToGroup FOR RelatingGroup;
		public List<IfcRelAssignsToGroup> IsGroupedBy { get { return mIsGroupedBy; } }

		internal IfcGroup() : base() { }
		internal IfcGroup(IfcGroup p) : base(p) { }
		public IfcGroup(DatabaseIfc m, string name) : base(m) { Name = name; mIsGroupedBy.Add(new IfcRelAssignsToGroup(this)); }
		internal IfcGroup(List<IfcObjectDefinition> ods) : base(ods[0].mDatabase) { mIsGroupedBy.Add(new IfcRelAssignsToGroup(this, ods)); }

		internal static IfcGroup Parse(string strDef) { IfcGroup g = new IfcGroup(); int ipos = 0; parseFields(g, ParserSTEP.SplitLineFields(strDef), ref ipos); return g; }
		internal static void parseFields(IfcGroup g, List<string> arrFields, ref int ipos) { IfcObject.parseFields(g, arrFields, ref ipos); }
		protected override string BuildString() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination ? "" : base.BuildString()); }
		internal void assign(IfcObjectDefinition o) { mIsGroupedBy[0].assign(o); }
	}
}
