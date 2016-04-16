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
	public partial class IfcFace : IfcTopologicalRepresentationItem //	SUPERTYPE OF(IfcFaceSurface)
	{
		internal List<int> mBounds = new List<int>();// : SET [1:?] OF IfcFaceBound;
		internal List<IfcFaceBound> Bounds { get { return mBounds.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcFaceBound); } }
		internal IfcFace() : base() { }
		internal IfcFace(IfcFace i) : base(i) { mBounds = new List<int>(i.mBounds.ToArray()); }
		public IfcFace(IfcFaceOuterBound outer) : base(outer.mDatabase) { mBounds.Add(outer.mIndex); }
		public IfcFace(IfcFaceOuterBound outer, IfcFaceBound inner) : this(outer) { mBounds.Add(inner.mIndex); }
		internal IfcFace(List<IfcFaceBound> bounds) : base(bounds[0].mDatabase) { mBounds = bounds.ConvertAll(x => x.mIndex); }
		internal static IfcFace Parse(string str)
		{
			IfcFace f = new IfcFace();
			int pos = 0;
			f.mBounds = ParserSTEP.StripListLink(str, ref pos);
			return f;
		}
		internal static void parseFields(IfcFace f, List<string> arrFields, ref int ipos) { IfcTopologicalRepresentationItem.parseFields(f, arrFields, ref ipos); f.mBounds = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential)
				return "";
			string str = base.BuildString() + ",(";
			if (mBounds.Count > 0)
				str += ParserSTEP.LinkToString(mBounds[0]);
			for (int icounter = 1; icounter < mBounds.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mBounds[icounter]);
			return str + ")";
		}
	}
	public partial class IfcFaceBasedSurfaceModel : IfcGeometricRepresentationItem
	{
		private List<int> mFbsmFaces = new List<int>();// : SET [1:?] OF IfcConnectedFaceSet;
		public List<IfcConnectedFaceSet> FbsmFaces { get { return mFbsmFaces.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcConnectedFaceSet); } }

		internal IfcFaceBasedSurfaceModel() : base() { }
		internal IfcFaceBasedSurfaceModel(IfcFaceBasedSurfaceModel p) : base(p) { mFbsmFaces = new List<int>(p.mFbsmFaces.ToArray()); }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mFbsmFaces[0]);
			for (int icounter = 1; icounter < mFbsmFaces.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mFbsmFaces[icounter]);
			return str + ")";
		}
		internal static void parseFields(IfcFaceBasedSurfaceModel m, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(m, arrFields, ref ipos); m.mFbsmFaces = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcFaceBasedSurfaceModel Parse(string strDef) { IfcFaceBasedSurfaceModel m = new IfcFaceBasedSurfaceModel(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
	}
	public partial class IfcFaceBound : IfcTopologicalRepresentationItem //SUPERTYPE OF (ONEOF (IfcFaceOuterBound))
	{
		internal int mBound;// : IfcLoop;
		internal bool mOrientation = true;// : BOOLEAN;
		internal IfcLoop Bound { get { return mDatabase.mIfcObjects[mBound] as IfcLoop; } }
		internal IfcFaceBound() : base() { }
		internal IfcFaceBound(IfcFaceBound i) : base(i) { mBound = i.mBound; mOrientation = i.mOrientation; }
		public IfcFaceBound(IfcLoop l, bool orientation) : base(l.mDatabase) { mBound = l.mIndex; mOrientation = orientation; }
		internal static IfcFaceBound Parse(string str)
		{
			IfcFaceBound b = new IfcFaceBound();
			int pos = 0;
			parseString(b, str, ref pos);
			return b;
		}
		protected static void parseString(IfcFaceBound b, string str, ref int pos)
		{
			b.mBound = ParserSTEP.StripLink(str, ref pos);
			b.mOrientation = ParserSTEP.StripBool(str, ref pos);
		}
		internal static void parseFields(IfcFaceBound b, List<string> arrFields, ref int ipos)
		{
			IfcTopologicalRepresentationItem.parseFields(b, arrFields, ref ipos);
			b.mBound = ParserSTEP.ParseLink(arrFields[ipos++]);
			b.mOrientation = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential)
				return "";
			return base.BuildString() + "," + ParserSTEP.LinkToString(mBound) + "," + ParserSTEP.BoolToString(mOrientation);
		}
	}
	public partial class IfcFaceOuterBound : IfcFaceBound
	{
		internal IfcFaceOuterBound() : base() { }
		internal IfcFaceOuterBound(IfcFaceOuterBound i) : base(i) { }
		public IfcFaceOuterBound(IfcLoop l, bool orientation) : base(l, orientation) { }
		internal new static IfcFaceOuterBound Parse(string str)
		{
			IfcFaceOuterBound b = new IfcFaceOuterBound();
			int pos = 0;
			parseString(b, str, ref pos);
			return b;
		}
	}
	public partial class IfcFaceSurface : IfcFace //SUPERTYPE OF(IfcAdvancedFace)
	{
		internal int mFaceSurface;// : IfcSurface;
		internal bool mSameSense = true;// : BOOLEAN;

		public IfcSurface FaceSurface { get { return mDatabase.mIfcObjects[mFaceSurface] as IfcSurface; } }
		public bool SameSense { get { return mSameSense; } set { mSameSense = value; } }

		internal IfcFaceSurface() : base() { }
		internal IfcFaceSurface(IfcFaceSurface s) : base(s) { mFaceSurface = s.mFaceSurface; mSameSense = s.mSameSense; }
		internal IfcFaceSurface(IfcFaceOuterBound bound, IfcSurface srf, bool sameSense) : base(bound) { mFaceSurface = srf.mIndex; mSameSense = sameSense; }
		internal IfcFaceSurface(IfcFaceOuterBound outer, IfcFaceBound inner, IfcSurface srf, bool sameSense) : base(outer, inner) { mFaceSurface = srf.mIndex; mSameSense = sameSense; }
		internal IfcFaceSurface(List<IfcFaceBound> bounds, IfcSurface srf, bool sameSense)
			: base(bounds) { mFaceSurface = srf.mIndex; mSameSense = sameSense; }
		internal new static IfcFaceSurface Parse(string strDef) { IfcFaceSurface s = new IfcFaceSurface(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcFaceSurface s, List<string> arrFields, ref int ipos) { IfcFace.parseFields(s, arrFields, ref ipos); s.mFaceSurface = ParserSTEP.ParseLink(arrFields[ipos++]); s.mSameSense = ParserSTEP.ParseBool(arrFields[ipos]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mFaceSurface) + "," + ParserSTEP.BoolToString(mSameSense); }
	}
	public partial class IfcFacetedBrep : IfcManifoldSolidBrep
	{
		internal IfcFacetedBrep() : base() { }
		internal IfcFacetedBrep(IfcFacetedBrep p) : base(p) { }
		public IfcFacetedBrep(IfcClosedShell s) : base(s) { }
		internal static IfcFacetedBrep Parse(string strDef) { IfcFacetedBrep b = new IfcFacetedBrep(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcFacetedBrep b, List<string> arrFields, ref int ipos) { IfcManifoldSolidBrep.parseFields(b, arrFields, ref ipos); }
		protected override string BuildString() { return (mDatabase.mOutputEssential ? "" : base.BuildString()); }
	}
	public partial class IfcFacetedBrepWithVoids : IfcFacetedBrep
	{
		internal List<int> mVoids = new List<int>();// : SET [1:?] OF IfcClosedShell
		internal IfcFacetedBrepWithVoids() : base() { }
		internal IfcFacetedBrepWithVoids(IfcFacetedBrepWithVoids p) : base(p) { mVoids = new List<int>(p.mVoids.ToArray()); }
		internal new static IfcFacetedBrepWithVoids Parse(string strDef) { IfcFacetedBrepWithVoids b = new IfcFacetedBrepWithVoids(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcFacetedBrepWithVoids b, List<string> arrFields, ref int ipos) { IfcManifoldSolidBrep.parseFields(b, arrFields, ref ipos); b.mVoids = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(";
			if (mVoids.Count > 0)
			{
				str += ParserSTEP.LinkToString(mVoids[0]);
				for (int icounter = 1; icounter < mVoids.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mVoids[icounter]);
			}
			return str + ")";
		}
	}
	//ENTITY IfcFailureConnectionCondition
	public class IfcFan : IfcFlowMovingDevice //IFC4
	{
		internal IfcFanTypeEnum mPredefinedType = IfcFanTypeEnum.NOTDEFINED;// OPTIONAL : IfcFanTypeEnum;
		public IfcFanTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcFan() : base() { }
		internal IfcFan(IfcFan f) : base(f) { mPredefinedType = f.mPredefinedType; }
		internal IfcFan(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFan s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcFanTypeEnum)Enum.Parse(typeof(IfcFanTypeEnum), str);
		}
		internal new static IfcFan Parse(string strDef) { IfcFan s = new IfcFan(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcFanTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcFanType : IfcFlowMovingDeviceType
	{
		internal IfcFanTypeEnum mPredefinedType = IfcFanTypeEnum.NOTDEFINED;// : IfcFanTypeEnum;
		public IfcFanTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcFanType() : base() { }
		internal IfcFanType(IfcFanType be) : base((IfcFlowMovingDeviceType)be) { mPredefinedType = be.mPredefinedType; }
		internal static void parseFields(IfcFanType t, List<string> arrFields, ref int ipos) { IfcFlowMovingDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFanTypeEnum)Enum.Parse(typeof(IfcFanTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFanType Parse(string strDef) { IfcFanType t = new IfcFanType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcFastener : IfcElementComponent
	{
		internal IfcFastenerTypeEnum mPredefinedType = IfcFastenerTypeEnum.NOTDEFINED;// : IfcFastenerTypeEnum; //IFC4
		public IfcFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcFastener() : base() { }
		internal IfcFastener(IfcFastener f) : base(f) { mPredefinedType = f.mPredefinedType; }
		internal IfcFastener(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
		
		internal static void parseFields(IfcFastener f, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcElementComponent.parseFields(f, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
				f.mPredefinedType = (IfcFastenerTypeEnum)Enum.Parse(typeof(IfcFastenerTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		internal static IfcFastener Parse(string strDef, Schema schema) { int ipos = 0; IfcFastener f = new IfcFastener(); parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return f; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcFastenerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public class IfcFastenerType : IfcElementComponentType
	{
		internal IfcFastenerTypeEnum mPredefinedType = IfcFastenerTypeEnum.NOTDEFINED;// : IfcFastenerTypeEnum; //IFC4
		public IfcFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcFastenerType() : base() { }
		internal IfcFastenerType(IfcFastenerType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcFastenerType(DatabaseIfc m, string name, IfcFastenerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static IfcFastenerType Parse(string strDef, Schema schema) { int ipos = 0; IfcFastenerType t = new IfcFastenerType(); parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return t; }
		internal static void parseFields(IfcFastenerType t, List<string> arrFields, ref int ipos, Schema schema) { IfcElementComponentType.parseFields(t, arrFields, ref ipos); if (schema != Schema.IFC2x3) t.mPredefinedType = (IfcFastenerTypeEnum)Enum.Parse(typeof(IfcFastenerTypeEnum), arrFields[ipos++].Replace(".", "")); }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : ",." + mPredefinedType + "."); }
	}
	public abstract class IfcFeatureElement : IfcElement //	ABSTRACT SUPERTYPE OF(ONEOF(IfcFeatureElementAddition, IfcFeatureElementSubtraction, IfcSurfaceFeature))
	{
		protected IfcFeatureElement() : base() { }
		protected IfcFeatureElement(IfcFeatureElement e) : base(e) { }
		protected IfcFeatureElement(DatabaseIfc db) : base(db) {  }
		protected IfcFeatureElement(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
		protected static void parseFields(IfcFeatureElement e, List<string> arrFields, ref int ipos) { IfcElement.parseFields(e, arrFields, ref ipos); }
	}
	public abstract partial class IfcFeatureElementAddition : IfcFeatureElement //ABSTRACT SUPERTYPE OF(IfcProjectionElement)
	{	//INVERSE
		internal List<IfcRelProjectsElement> mProjectsElements = new List<IfcRelProjectsElement>();
		public List<IfcRelProjectsElement> ProjectsElements { get { return mProjectsElements; } }

		protected IfcFeatureElementAddition() : base() { }
		protected IfcFeatureElementAddition(IfcFeatureElementAddition el) : base(el) { }
		protected static void parseFields(IfcFeatureElementAddition e, List<string> arrFields, ref int ipos) { IfcFeatureElement.parseFields(e, arrFields, ref ipos); }
	}
	public abstract partial class IfcFeatureElementSubtraction : IfcFeatureElement //ABSTRACT SUPERTYPE OF (ONEOF (IfcOpeningElement ,IfcVoidingFeature)) 
	{ //INVERSE
		internal IfcRelVoidsElement mVoidsElement = null;
		public IfcRelVoidsElement VoidsElement { get { return mVoidsElement; } }

		protected IfcFeatureElementSubtraction() : base() { }
		protected IfcFeatureElementSubtraction(IfcFeatureElementSubtraction s) : base(s) { }
		protected IfcFeatureElementSubtraction(DatabaseIfc db) : base(db) {  }
		protected IfcFeatureElementSubtraction(IfcElement host, IfcProductRepresentation rep) : base(host.mDatabase)
		{
			new IfcRelVoidsElement(host, this);
			Representation = rep;
			Placement = new IfcLocalPlacement(host.Placement, new IfcAxis2Placement3D(host.mDatabase));	
		}
		
		protected static void parseFields(IfcFeatureElementSubtraction e, List<string> arrFields, ref int ipos) { IfcFeatureElement.parseFields(e, arrFields, ref ipos); }
	}
	public class IfcFillAreaStyleHatching : IfcGeometricRepresentationItem
	{
		internal int mHatchLineAppearance;// : IfcCurveStyle;
		internal string mStartOfNextHatchLine;// : IfcHatchLineDistanceSelect;
		//IfcOneDirectionRepeatFacton,IfcPositiveLengthMeasure
		internal int mPointOfReferenceHatchLine;// : OPTIONAL IfcCartesianPoint; //DEPRECEATED IFC4
		internal int mPatternStart;// : OPTIONAL IfcCartesianPoint;
		internal double mHatchLineAngle;// : IfcPlaneAngleMeasure;
		internal IfcFillAreaStyleHatching() : base() { }
		internal IfcFillAreaStyleHatching(IfcFillAreaStyleHatching p)
			: base(p)
		{
			mHatchLineAppearance = p.mHatchLineAppearance;
			mStartOfNextHatchLine = p.mStartOfNextHatchLine;
			mPointOfReferenceHatchLine = p.mPointOfReferenceHatchLine;
			mPatternStart = p.mPatternStart;
			mHatchLineAngle = p.mHatchLineAngle;
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mHatchLineAppearance) + "," + mStartOfNextHatchLine + "," + ParserSTEP.LinkToString(mPointOfReferenceHatchLine) + "," + ParserSTEP.LinkToString(mPatternStart) + "," + ParserSTEP.DoubleToString(mHatchLineAngle); }
		internal static void parseFields(IfcFillAreaStyleHatching h, List<string> arrFields, ref int ipos)
		{
			IfcGeometricRepresentationItem.parseFields(h, arrFields, ref ipos);
			h.mHatchLineAppearance = ParserSTEP.ParseLink(arrFields[ipos++]);
			h.mStartOfNextHatchLine = arrFields[ipos++];
			h.mPointOfReferenceHatchLine = ParserSTEP.ParseLink(arrFields[ipos++]);
			h.mPatternStart = ParserSTEP.ParseLink(arrFields[ipos++]);
			h.mHatchLineAngle = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		internal static IfcFillAreaStyleHatching Parse(string strDef) { IfcFillAreaStyleHatching h = new IfcFillAreaStyleHatching(); int ipos = 0; parseFields(h, ParserSTEP.SplitLineFields(strDef), ref ipos); return h; }
	}
	//ENTITY IfcFillAreaStyleTileSymbolWithStyle // DEPRECEATED IFC4
	//ENTITY IfcFillAreaStyleTiles
	public class IfcFilter : IfcFlowTreatmentDevice //IFC4  
	{
		internal IfcFilterTypeEnum mPredefinedType = IfcFilterTypeEnum.NOTDEFINED;
		public IfcFilterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFilter() : base() { }
		internal IfcFilter(IfcFilter f) : base(f) { mPredefinedType = f.mPredefinedType; }
		internal IfcFilter(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcFilter a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcFilterTypeEnum)Enum.Parse(typeof(IfcFilterTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcFilter Parse(string strDef) { IfcFilter d = new IfcFilter(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcFilterTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcFilterType : IfcFlowTreatmentDeviceType
	{
		internal IfcFilterTypeEnum mPredefinedType = IfcFilterTypeEnum.NOTDEFINED;// : IfcFilterTypeEnum;
		internal IfcFilterType() : base() { }
		internal IfcFilterType(IfcFilterType be) : base((IfcFlowTreatmentDeviceType)be) { mPredefinedType = be.mPredefinedType; }
		internal static void parseFields(IfcFilterType t, List<string> arrFields, ref int ipos) { IfcFlowTreatmentDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFilterTypeEnum)Enum.Parse(typeof(IfcFilterTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFilterType Parse(string strDef) { IfcFilterType t = new IfcFilterType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcFillAreaStyle : IfcPresentationStyle
	{
		internal List<int> mFillStyles = new List<int>();// : SET [1:?] OF IfcFillStyleSelect;
		internal IfcFillAreaStyle() : base() { }
		internal IfcFillAreaStyle(IfcFillAreaStyle i) : base(i) { mFillStyles = new List<int>(i.mFillStyles.ToArray()); }
		internal IfcFillAreaStyle(DatabaseIfc m, string name) : base(m, name) { }
		internal static void parseFields(IfcFillAreaStyle s, List<string> arrFields, ref int ipos) { IfcPresentationStyle.parseFields(s, arrFields, ref ipos); s.mFillStyles = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcFillAreaStyle Parse(string strDef) { IfcFillAreaStyle s = new IfcFillAreaStyle(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mFillStyles[0]);
			for (int icounter = 1; icounter < mFillStyles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mFillStyles[icounter]);
			return str + ")";
		}
	}
	public interface IfcFillStyleSelect { } // SELECT ( IfcFillAreaStyleHatching, IfcFillAreaStyleTiles, IfcExternallyDefinedHatchStyle, IfcColour);
	public class IfcFireSuppressionTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcFireSuppressionTerminalTypeEnum mPredefinedType = IfcFireSuppressionTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcFireSuppressinTerminalTypeEnum;
		public IfcFireSuppressionTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcFireSuppressionTerminal() : base() { }
		internal IfcFireSuppressionTerminal(IfcFireSuppressionTerminal t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcFireSuppressionTerminal(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		
		internal static void parseFields(IfcFireSuppressionTerminal s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcFireSuppressionTerminalTypeEnum)Enum.Parse(typeof(IfcFireSuppressionTerminalTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcFireSuppressionTerminal Parse(string strDef) { IfcFireSuppressionTerminal s = new IfcFireSuppressionTerminal(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcFireSuppressionTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public class IfcFireSuppressionTerminalType : IfcFlowTerminalType
	{
		internal IfcFireSuppressionTerminalTypeEnum mPredefinedType = IfcFireSuppressionTerminalTypeEnum.NOTDEFINED;// : IfcFireSuppressionTerminalTypeEnum;
		public IfcFireSuppressionTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcFireSuppressionTerminalType() : base() { }
		internal IfcFireSuppressionTerminalType(IfcFireSuppressionTerminalType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcFireSuppressionTerminalType(DatabaseIfc m, string name, IfcFireSuppressionTerminalTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcFireSuppressionTerminalType t, List<string> arrFields, ref int ipos) { IfcFlowTerminalType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFireSuppressionTerminalTypeEnum)Enum.Parse(typeof(IfcFireSuppressionTerminalTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFireSuppressionTerminalType Parse(string strDef) { IfcFireSuppressionTerminalType t = new IfcFireSuppressionTerminalType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcFixedReferenceSweptAreaSolid : IfcSweptAreaSolid //IFC4
	{
		internal int mDirectrix; // : IfcCurve;
		internal double mStartParam = 0;// : OPT IfcParameterValue;  
		internal double mEndParam = 0;//: OPT IfcParameterValue; 
		internal int mFixedReference;// : 	IfcDirection; 

		internal IfcCurve Directrix { get { return mDatabase.mIfcObjects[mDirectrix] as IfcCurve; } }
		internal IfcDirection FixedReference { get { return mDatabase.mIfcObjects[mFixedReference] as IfcDirection; } }

		internal IfcFixedReferenceSweptAreaSolid() : base() { }
		internal IfcFixedReferenceSweptAreaSolid(IfcFixedReferenceSweptAreaSolid p) : base(p) { mDirectrix = p.mDirectrix; mStartParam = p.mStartParam; mEndParam = p.mEndParam; mFixedReference = p.mFixedReference; }

		internal static void parseFields(IfcFixedReferenceSweptAreaSolid s, List<string> arrFields, ref int ipos) { IfcSweptAreaSolid.parseFields(s, arrFields, ref ipos); s.mDirectrix = ParserSTEP.ParseLink(arrFields[ipos++]); s.mStartParam = ParserSTEP.ParseDouble(arrFields[ipos++]); s.mEndParam = ParserSTEP.ParseDouble(arrFields[ipos++]); s.mFixedReference = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcFixedReferenceSweptAreaSolid Parse(string strDef) { IfcFixedReferenceSweptAreaSolid s = new IfcFixedReferenceSweptAreaSolid(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mDirectrix) + "," + ParserSTEP.DoubleToString(mStartParam) + "," + ParserSTEP.DoubleToString(mEndParam) + "," + ParserSTEP.LinkToString(mFixedReference); }
	}
	public partial class IfcFlowController : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcAirTerminalBox, IfcDamper
	{ //, IfcElectricDistributionBoard, IfcElectricTimeControl, IfcFlowMeter, IfcProtectiveDevice, IfcSwitchingDevice, IfcValve))
		public override string KeyWord { get { return mDatabase.mSchema == Schema.IFC2x3 ? "IFCFLOWCONTROLLER" : base.KeyWord; } }

		internal IfcFlowController() : base() { }
		internal IfcFlowController(IfcFlowController c) : base(c) { }
		internal IfcFlowController(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFlowController c, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(c, arrFields, ref ipos); }
		internal new static IfcFlowController Parse(string strDef) { IfcFlowController c = new IfcFlowController(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
	}
	public abstract class IfcFlowControllerType : IfcDistributionFlowElementType
	{
		protected IfcFlowControllerType() : base() { }
		protected IfcFlowControllerType(IfcFlowControllerType t) : base(t) { }
		protected IfcFlowControllerType(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcFlowControllerType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcFlowFitting : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcCableCarrierFitting, IfcCableFitting, IfcDuctFitting, IfcJunctionBox, IfcPipeFitting))
	{
		public override string KeyWord { get { return mDatabase.mSchema == Schema.IFC2x3 ? "IFCFLOWFITTING" : base.KeyWord; } }

		internal IfcFlowFitting() : base() { }
		internal IfcFlowFitting(IfcFlowFitting f) : base(f) { }
		public IfcFlowFitting(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFlowFitting f, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(f, arrFields, ref ipos); }
		internal new static IfcFlowFitting Parse(string strDef) { IfcFlowFitting f = new IfcFlowFitting(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
	}
	public abstract partial class IfcFlowFittingType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcCableCarrierFittingType ,IfcDuctFittingType ,IfcJunctionBoxType ,IfcPipeFittingType))
	{
		protected IfcFlowFittingType() : base() { }
		protected IfcFlowFittingType(IfcFlowFittingType t) : base(t) { }
		protected IfcFlowFittingType(DatabaseIfc db) : base(db) { }
		protected static void parseFields(IfcFlowFittingType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public class IfcFlowInstrument : IfcDistributionControlElement //IFC4  
	{
		internal IfcFlowInstrumentTypeEnum mPredefinedType = IfcFlowInstrumentTypeEnum.NOTDEFINED;
		public IfcFlowInstrumentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowInstrument() : base() { }
		internal IfcFlowInstrument(IfcFlowInstrument i) : base(i) { mPredefinedType = i.mPredefinedType; }
		internal IfcFlowInstrument(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcFlowInstrument a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcFlowInstrumentTypeEnum)Enum.Parse(typeof(IfcFlowInstrumentTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcFlowInstrument Parse(string strDef) { IfcFlowInstrument d = new IfcFlowInstrument(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcFlowInstrumentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcFlowInstrumentType : IfcDistributionControlElementType
	{
		internal IfcFlowInstrumentTypeEnum mPredefinedType = IfcFlowInstrumentTypeEnum.NOTDEFINED;// : IfcFlowInstrumentTypeEnum;
		public IfcFlowInstrumentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcFlowInstrumentType() : base() { }
		internal IfcFlowInstrumentType(IfcFlowInstrumentType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcFlowInstrumentType(DatabaseIfc m, string name, IfcFlowInstrumentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcFlowInstrumentType t, List<string> arrFields, ref int ipos) { IfcDistributionControlElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFlowInstrumentTypeEnum)Enum.Parse(typeof(IfcFlowInstrumentTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFlowInstrumentType Parse(string strDef) { IfcFlowInstrumentType t = new IfcFlowInstrumentType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcFlowMeter : IfcFlowController //IFC4
	{
		internal IfcFlowMeterTypeEnum mPredefinedType = IfcFlowMeterTypeEnum.NOTDEFINED;// OPTIONAL : IfcDamperTypeEnum;
		public IfcFlowMeterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcFlowMeter() : base() { }
		internal IfcFlowMeter(IfcFlowMeter m) : base(m) { mPredefinedType = m.mPredefinedType; }
		internal IfcFlowMeter(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFlowMeter s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcFlowMeterTypeEnum)Enum.Parse(typeof(IfcFlowMeterTypeEnum), str);
		}
		internal new static IfcFlowMeter Parse(string strDef) { IfcFlowMeter s = new IfcFlowMeter(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcFlowMeterTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcFlowMeterType : IfcFlowControllerType
	{
		internal IfcFlowMeterTypeEnum mPredefinedType = IfcFlowMeterTypeEnum.NOTDEFINED;// : IfcFlowMeterTypeEnum;
		public IfcFlowMeterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcFlowMeterType() : base() { }
		internal IfcFlowMeterType(IfcFlowMeterType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcFlowMeterType(DatabaseIfc m, string name, IfcFlowMeterTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcFlowMeterType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFlowMeterTypeEnum)Enum.Parse(typeof(IfcFlowMeterTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFlowMeterType Parse(string strDef) { IfcFlowMeterType t = new IfcFlowMeterType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcFlowMovingDevice : IfcDistributionFlowElement //	SUPERTYPE OF(ONEOF(IfcCompressor, IfcFan, IfcPump))
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 ? "IFCFLOWMOVINGDEVICE" : base.KeyWord); } }

		internal IfcFlowMovingDevice() : base() { }
		internal IfcFlowMovingDevice(IfcFlowMovingDevice be) : base((IfcDistributionFlowElement)be) { }
		internal IfcFlowMovingDevice(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFlowMovingDevice d, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(d, arrFields, ref ipos); }
		internal new static IfcFlowMovingDevice Parse(string strDef) { IfcFlowMovingDevice d = new IfcFlowMovingDevice(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
	}
	public abstract class IfcFlowMovingDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF(ONEOF(IfcCompressorType, IfcFanType, IfcPumpType))
	{
		protected IfcFlowMovingDeviceType() : base() { }
		protected IfcFlowMovingDeviceType(IfcFlowMovingDeviceType t) : base(t) { }
		protected IfcFlowMovingDeviceType(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcFlowMovingDeviceType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcFlowSegment : IfcDistributionFlowElement //	SUPERTYPE OF(ONEOF(IfcCableCarrierSegment, IfcCableSegment, IfcDuctSegment, IfcPipeSegment))
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 ? "IFCFLOWSEGMENT" : base.KeyWord); } }

		internal IfcFlowSegment() : base() { }
		internal IfcFlowSegment(IfcFlowSegment s) : base(s) { }
		public IfcFlowSegment(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFlowSegment s, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(s, arrFields, ref ipos); }
		internal new static IfcFlowSegment Parse(string strDef) { IfcFlowSegment s = new IfcFlowSegment(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public abstract partial class IfcFlowSegmentType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcCableCarrierSegmentType ,IfcCableSegmentType ,IfcDuctSegmentType ,IfcPipeSegmentType))
	{
		protected IfcFlowSegmentType() : base() { }
		protected IfcFlowSegmentType(IfcFlowSegmentType t) : base(t) { }
		protected IfcFlowSegmentType(DatabaseIfc db) : base(db) { }
		protected static void parseFields(IfcFlowSegmentType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcFlowStorageDevice : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcElectricFlowStorageDevice, IfcTank))
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 ? "IFCFLOWSTORAGEDEVICE" : base.KeyWord); } }

		internal IfcFlowStorageDevice() : base() { }
		internal IfcFlowStorageDevice(IfcFlowStorageDevice d) : base(d) { }
		internal IfcFlowStorageDevice(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcFlowStorageDevice d, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(d, arrFields, ref ipos); }
		internal new static IfcFlowStorageDevice Parse(string strDef) { IfcFlowStorageDevice d = new IfcFlowStorageDevice(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
	}
	public abstract class IfcFlowStorageDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcElectricFlowStorageDeviceType ,IfcTankType))
	{
		protected IfcFlowStorageDeviceType() : base() { }
		protected IfcFlowStorageDeviceType(IfcFlowStorageDeviceType t) : base(t) { }
		protected static void parseFields(IfcFlowStorageDeviceType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcFlowTerminal : IfcDistributionFlowElement 	//SUPERTYPE OF(ONEOF(IfcAirTerminal, IfcAudioVisualAppliance, IfcCommunicationsAppliance, IfcElectricAppliance, IfcFireSuppressionTerminal, IfcLamp, IfcLightFixture, IfcMedicalDevice, IfcOutlet, IfcSanitaryTerminal, IfcSpaceHeater, IfcStackTerminal, IfcWasteTerminal))
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 ? "IFCFLOWTERMINAL" : base.KeyWord); } }

		internal IfcFlowTerminal() : base() { }
		internal IfcFlowTerminal(IfcFlowTerminal t) : base(t) { }
		public IfcFlowTerminal(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcFlowTerminal t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(t, arrFields, ref ipos); }
		internal new static IfcFlowTerminal Parse(string strDef) { IfcFlowTerminal t = new IfcFlowTerminal(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public abstract partial class IfcFlowTerminalType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcAirTerminalType ,
	{ // IfcElectricApplianceType ,IfcElectricHeaterType ,IfcFireSuppressionTerminalType,IfcLampType ,IfcLightFixtureType ,IfcOutletType ,IfcSanitaryTerminalType ,IfcStackTerminalType ,IfcWasteTerminalType)) 
		// IFC4 deleted ,IfcGasTerminalType 
		protected IfcFlowTerminalType() : base() { }
		protected IfcFlowTerminalType(IfcFlowTerminalType t) : base(t) { }
		protected IfcFlowTerminalType(DatabaseIfc db) : base(db) { }
		protected static void parseFields(IfcFlowTerminalType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcFlowTreatmentDevice : IfcDistributionFlowElement // 	SUPERTYPE OF(ONEOF(IfcDuctSilencer, IfcFilter, IfcInterceptor))
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 ? "IFCFLOWTREATMENTDEVICE" : base.KeyWord); } }

		internal IfcFlowTreatmentDevice() : base() { }
		internal IfcFlowTreatmentDevice(IfcFlowTreatmentDevice d) : base(d) { }
		internal IfcFlowTreatmentDevice(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		
		internal static void parseFields(IfcFlowTreatmentDevice d, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(d, arrFields, ref ipos); }
		internal new static IfcFlowTreatmentDevice Parse(string strDef) { IfcFlowTreatmentDevice d = new IfcFlowTreatmentDevice(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
	}
	public abstract class IfcFlowTreatmentDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF(ONEOF(IfcDuctSilencerType, IfcFilterType, IfcInterceptorType))
	{
		protected IfcFlowTreatmentDeviceType() : base() { }
		protected IfcFlowTreatmentDeviceType(IfcFlowTreatmentDeviceType be) : base(be) { }
		protected static void parseFields(IfcFlowTreatmentDeviceType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public class IfcFluidFlowProperties : IfcPropertySetDefinition // DEPRECEATED IFC4
	{
		internal IfcPropertySourceEnum mPropertySource;// : IfcPropertySourceEnum;
		internal int mFlowConditionTimeSeries, mVelocityTimeSeries, mFlowrateTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal int mFluid;// : IfcMaterial;
		internal int mPressureTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal string mUserDefinedPropertySource = "$";// : OPTIONAL IfcLabel;
		internal int mTemperatureSingleValue, mWetBulbTemperatureSingleValue;// : OPTIONAL IfcThermodynamicTemperatureMeasure;
		internal int mWetBulbTemperatureTimeSeries, mTemperatureTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal int mFlowrateSingleValue;// : OPTIONAL IfcDerivedMeasureValue;
		internal int mFlowConditionSingleValue;// : OPTIONAL IfcPositiveRatioMeasure;
		internal int mVelocitySingleValue;// : OPTIONAL IfcLinearVelocityMeasure;
		internal int mPressureSingleValue;// : OPTIONAL IfcPressureMeasure;
		internal IfcFluidFlowProperties() : base() { }
		internal IfcFluidFlowProperties(IfcFluidFlowProperties p)
			: base(p)
		{
			mPropertySource = p.mPropertySource;
			mFlowConditionTimeSeries = p.mFlowConditionTimeSeries;
			mVelocityTimeSeries = p.mVelocityTimeSeries;
			mFlowrateTimeSeries = p.mFlowrateTimeSeries;
			mFluid = p.mFluid;
			mPressureTimeSeries = p.mPressureTimeSeries;
			mUserDefinedPropertySource = p.mUserDefinedPropertySource;
			mTemperatureSingleValue = p.mTemperatureSingleValue;
			mWetBulbTemperatureSingleValue = p.mWetBulbTemperatureSingleValue;
			mWetBulbTemperatureTimeSeries = p.mWetBulbTemperatureTimeSeries;
			mTemperatureTimeSeries = p.mTemperatureTimeSeries;
			mFlowrateSingleValue = p.mFlowrateSingleValue;
			mFlowConditionSingleValue = p.mFlowConditionSingleValue;
			mVelocitySingleValue = p.mVelocitySingleValue;
			mPressureSingleValue = p.mPressureSingleValue;
		}
		internal static IfcFluidFlowProperties Parse(string strDef) { IfcFluidFlowProperties p = new IfcFluidFlowProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcFluidFlowProperties p, List<string> arrFields, ref int ipos)
		{
			IfcPropertySetDefinition.parseFields(p, arrFields, ref ipos);
			p.mPropertySource = (IfcPropertySourceEnum)Enum.Parse(typeof(IfcPropertySourceEnum), arrFields[ipos++].Replace(".", ""));
			p.mFlowConditionTimeSeries = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mVelocityTimeSeries = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mFlowrateTimeSeries = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mFluid = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mPressureTimeSeries = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mUserDefinedPropertySource = arrFields[ipos++];
			p.mTemperatureSingleValue = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mWetBulbTemperatureSingleValue = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mWetBulbTemperatureTimeSeries = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mTemperatureTimeSeries = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mFlowrateSingleValue = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mFlowConditionSingleValue = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mVelocitySingleValue = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mPressureSingleValue = ParserSTEP.ParseLink(arrFields[ipos++]);

		}
		protected override string BuildString()
		{
			return base.BuildString() + ",." + mPropertySource.ToString() + ".," + ParserSTEP.LinkToString(mFlowConditionTimeSeries) + "," + ParserSTEP.LinkToString(mVelocityTimeSeries) + "," + ParserSTEP.LinkToString(mFlowrateTimeSeries)
				+ "," + ParserSTEP.LinkToString(mFluid) + "," + ParserSTEP.LinkToString(mPressureTimeSeries) + "," + mUserDefinedPropertySource + "," + ParserSTEP.LinkToString(mTemperatureSingleValue)
				+ "," + ParserSTEP.LinkToString(mWetBulbTemperatureSingleValue) + "," + ParserSTEP.LinkToString(mWetBulbTemperatureTimeSeries) + "," + ParserSTEP.LinkToString(mTemperatureTimeSeries) + "," + ParserSTEP.LinkToString(mFlowrateSingleValue)
				+ "," + ParserSTEP.LinkToString(mFlowConditionSingleValue) + "," + ParserSTEP.LinkToString(mVelocitySingleValue) + "," + ParserSTEP.LinkToString(mPressureSingleValue);
		}
	}
	public partial class IfcFooting : IfcBuildingElement
	{
		internal IfcFootingTypeEnum mPredefinedType = IfcFootingTypeEnum.NOTDEFINED;// OPTIONAL : IfcFootingTypeEnum;
		public IfcFootingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFooting() : base() { }
		internal IfcFooting(IfcFooting b) : base(b) { mPredefinedType = b.mPredefinedType; }
		public IfcFooting(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcFooting Parse(string strDef) { IfcFooting f = new IfcFooting(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
		internal static void parseFields(IfcFooting f, List<string> arrFields, ref int ipos)
		{
			IfcBuildingElement.parseFields(f, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				f.mPredefinedType = (IfcFootingTypeEnum)Enum.Parse(typeof(IfcFootingTypeEnum), str.Substring(1, str.Length - 2));
		}
	}
	public partial class IfcFootingType : IfcBuildingElementType
	{
		internal IfcFootingTypeEnum mPredefinedType = IfcFootingTypeEnum.NOTDEFINED;
		public IfcFootingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcFootingType() : base() { }
		internal IfcFootingType(IfcFootingType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcFootingType(DatabaseIfc m, string name, IfcFootingTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcFootingType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFootingTypeEnum)Enum.Parse(typeof(IfcFootingTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFootingType Parse(string strDef) { IfcFootingType t = new IfcFootingType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString() + ",." + mPredefinedType.ToString() + "."); }

	}
	//ENTITY IfcFuelProperties // DEPRECEATED IFC4
	public partial class IfcFurnishingElement : IfcElement // DEPRECEATED IFC4 to make abstract SUPERTYPE OF(ONEOF(IfcFurniture, IfcSystemFurnitureElement))
	{
		internal IfcFurnishingElement() : base() { }
		internal IfcFurnishingElement(IfcFurnishingElement f) : base(f) { }
		internal IfcFurnishingElement(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static void parseFields(IfcFurnishingElement e, List<string> arrFields, ref int ipos) { IfcElement.parseFields(e, arrFields, ref ipos); }
		internal static IfcFurnishingElement Parse(string strDef) { IfcFurnishingElement e = new IfcFurnishingElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
	}
	public class IfcFurnishingElementType : IfcElementType //IFC4 Depreceated //SUPERTYPE OF (ONEOF (IfcFurnitureType ,IfcSystemFurnitureElementType))
	{
		internal IfcFurnishingElementType() : base() { }
		internal IfcFurnishingElementType(IfcFurnishingElementType t) : base(t) { }
		internal IfcFurnishingElementType(DatabaseIfc db,string name) : base(db) { Name = name; }
		internal static void parseFields(IfcFurnishingElementType t, List<string> arrFields, ref int ipos) { IfcElementType.parseFields(t, arrFields, ref ipos); }
		internal new static IfcFurnishingElementType Parse(string strDef) { IfcFurnishingElementType t = new IfcFurnishingElementType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public partial class IfcFurniture : IfcFurnishingElement
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 ? "IFCFURNISHINGELEMENT" : base.KeyWord); } }
		internal IfcFurnitureTypeEnum mPredefinedType = IfcFurnitureTypeEnum.NOTDEFINED;//: OPTIONAL IfcFurnitureTypeEnum;
		internal IfcFurniture() : base() { }
		internal IfcFurniture(IfcFurniture f) : base(f) { mPredefinedType = f.mPredefinedType; }
		internal IfcFurniture(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static void parseFields(IfcFurniture e, List<string> arrFields, ref int ipos)
		{
			IfcFurnishingElement.parseFields(e, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				e.mPredefinedType = (IfcFurnitureTypeEnum)Enum.Parse(typeof(IfcFurnitureTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcFurniture Parse(string strDef) { IfcFurniture e = new IfcFurniture(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : ",." + mPredefinedType + "."); }
	}
	internal class IfcFurnitureStandard : IfcControl // DEPRECEATED IFC4
	{
		internal IfcFurnitureStandard() : base() { }
		internal IfcFurnitureStandard(IfcFurnitureStandard i) : base((IfcControl)i) { }
		internal static IfcFurnitureStandard Parse(string strDef, Schema schema) { IfcFurnitureStandard s = new IfcFurnitureStandard(); int ipos = 0; IfcControl.parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return s; }
	}
	public partial class IfcFurnitureType : IfcFurnishingElementType
	{
		internal IfcAssemblyPlaceEnum mAssemblyPlace = IfcAssemblyPlaceEnum.NOTDEFINED;
		internal IfcFurnitureTypeEnum mPredefinedType = IfcFurnitureTypeEnum.NOTDEFINED; // IFC4 OPTIONAL
		internal IfcFurnitureType() : base() { }
		internal IfcFurnitureType(IfcFurnitureType be) : base(be) { }
		internal IfcFurnitureType(DatabaseIfc m, string name, IfcAssemblyPlaceEnum a, IfcFurnitureTypeEnum type)
			: base(m,name)
		{
			mAssemblyPlace = a;
			mPredefinedType = type;
			if (mDatabase.mSchema == Schema.IFC2x3 && string.IsNullOrEmpty(ElementType) && type != IfcFurnitureTypeEnum.NOTDEFINED)
				ElementType = type.ToString();
		}

		internal static void parseFields(IfcFurnitureType t, List<string> arrFields, ref int ipos,Schema schema)
		{
			IfcFurnishingElementType.parseFields(t, arrFields, ref ipos);
			t.mAssemblyPlace = (IfcAssemblyPlaceEnum)Enum.Parse(typeof(IfcAssemblyPlaceEnum), arrFields[ipos++].Replace(".", ""));
			if (schema != Schema.IFC2x3)
			{
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					t.mPredefinedType = (IfcFurnitureTypeEnum)Enum.Parse(typeof(IfcFurnitureTypeEnum), s.Replace(".", ""));
			}
		}
		internal static IfcFurnitureType Parse(string strDef, Schema schema) { IfcFurnitureType t = new IfcFurnitureType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mAssemblyPlace.ToString() + (mDatabase.mSchema == Schema.IFC2x3 ? "." : ".,." + mPredefinedType + "."); }
	}
}
