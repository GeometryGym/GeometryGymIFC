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
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class IfcFace : IfcTopologicalRepresentationItem //	SUPERTYPE OF(IfcFaceSurface)
	{
		internal List<int> mBounds = new List<int>();// : SET [1:?] OF IfcFaceBound;
		public ReadOnlyCollection<IfcFaceBound> Bounds { get { return new ReadOnlyCollection<IfcFaceBound>( mBounds.ConvertAll(x =>mDatabase[x] as IfcFaceBound)); } }

		internal IfcFace() : base() { }
		internal IfcFace(DatabaseIfc db, IfcFace f) : base(db,f) { f.Bounds.ToList().ForEach(x=>addBound( db.Factory.Duplicate(x) as IfcFaceBound)); }
		public IfcFace(IfcFaceOuterBound outer) : base(outer.mDatabase) { mBounds.Add(outer.mIndex); }
		public IfcFace(IfcFaceOuterBound outer, IfcFaceBound inner) : this(outer) { mBounds.Add(inner.mIndex); }
		public IfcFace(List<IfcFaceBound> bounds) : base(bounds[0].mDatabase) { mBounds = bounds.ConvertAll(x => x.mIndex); }
		internal static IfcFace Parse(string str)
		{
			IfcFace f = new IfcFace();
			int pos = 0;
			f.mBounds = ParserSTEP.StripListLink(str, ref pos, str.Length);
			return f;
		}

        /// <summary>
        /// - abbreviation
        /// </summary>
        public static IfcFace GenFace(DatabaseIfc db, IfcCartesianPoint v1, IfcCartesianPoint v2, IfcCartesianPoint v3, IfcCartesianPoint v4)
        {
            IfcPolyloop polyloop1 = new IfcPolyloop(new List<IfcCartesianPoint> { v1, v2, v3, v4 });
            return new IfcFace(new IfcFaceOuterBound(polyloop1, true));
        }

        internal static void parseFields(IfcFace f, List<string> arrFields, ref int ipos) { f.mBounds = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(";
			if (mBounds.Count > 0)
				str += ParserSTEP.LinkToString(mBounds[0]);
			for (int icounter = 1; icounter < mBounds.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mBounds[icounter]);
			return str + ")";
		}

		internal void addBound(IfcFaceBound bound) { mBounds.Add(bound.mIndex); }
	}
	public partial class IfcFaceBasedSurfaceModel : IfcGeometricRepresentationItem, IfcSurfaceOrFaceSurface
	{
		private List<int> mFbsmFaces = new List<int>();// : SET [1:?] OF IfcConnectedFaceSet;
		public ReadOnlyCollection<IfcConnectedFaceSet> FbsmFaces { get { return new ReadOnlyCollection<IfcConnectedFaceSet>( mFbsmFaces.ConvertAll(x =>mDatabase[x] as IfcConnectedFaceSet)); } }

		internal IfcFaceBasedSurfaceModel() : base() { }
		internal IfcFaceBasedSurfaceModel(DatabaseIfc db, IfcFaceBasedSurfaceModel s) : base(db,s) { s.FbsmFaces.ToList().ForEach(x => addFace( db.Factory.Duplicate(x) as IfcConnectedFaceSet)); }
		public IfcFaceBasedSurfaceModel(IfcConnectedFaceSet face) : base(face.mDatabase) { mFbsmFaces.Add(face.mIndex); }
		public IfcFaceBasedSurfaceModel(List<IfcConnectedFaceSet> faces) : base(faces[0].mDatabase) { faces.ForEach(x => addFace(x)); }
		internal static IfcFaceBasedSurfaceModel Parse(string str)
		{
			IfcFaceBasedSurfaceModel m = new IfcFaceBasedSurfaceModel();
			m.mFbsmFaces = ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2));
			return m;
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mFbsmFaces[0]);
			for (int icounter = 1; icounter < mFbsmFaces.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mFbsmFaces[icounter]);
			return str + ")";
		}

		internal void addFace(IfcConnectedFaceSet face) { mFbsmFaces.Add(face.mIndex); }
	}
	public partial class IfcFaceBound : IfcTopologicalRepresentationItem //SUPERTYPE OF (ONEOF (IfcFaceOuterBound))
	{
		internal int mBound;// : IfcLoop;
		internal bool mOrientation = true;// : BOOLEAN;

		public IfcLoop Bound { get { return mDatabase[mBound] as IfcLoop; } set { mBound = value.mIndex; } }

		internal IfcFaceBound() : base() { }
		internal IfcFaceBound(DatabaseIfc db, IfcFaceBound b) : base(db,b) { Bound = db.Factory.Duplicate(b.Bound) as IfcLoop; mOrientation = b.mOrientation; }
		public IfcFaceBound(IfcLoop l, bool orientation) : base(l.mDatabase) { mBound = l.mIndex; mOrientation = orientation; }
		internal static IfcFaceBound Parse(string str)
		{
			IfcFaceBound b = new IfcFaceBound();
			int pos = 0;
			b.Parse( str, ref pos, str.Length);
			return b;
		}
		protected void Parse( string str, ref int pos, int len)
		{
			mBound = ParserSTEP.StripLink(str, ref pos, len);
			mOrientation = ParserSTEP.StripBool(str, ref pos, len);
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBound) + "," + ParserSTEP.BoolToString(mOrientation);
		}
	}
	public partial class IfcFaceOuterBound : IfcFaceBound
	{
		internal IfcFaceOuterBound() : base() { }
		internal IfcFaceOuterBound(DatabaseIfc db, IfcFaceOuterBound b) : base(db,b) { }
		public IfcFaceOuterBound(IfcLoop l, bool orientation) : base(l, orientation) { }
		internal new static IfcFaceOuterBound Parse(string str)
		{
			IfcFaceOuterBound b = new IfcFaceOuterBound();
			int pos = 0;
			b.Parse(str, ref pos, str.Length);
			return b;
		}
	}
	public partial class IfcFaceSurface : IfcFace, IfcSurfaceOrFaceSurface //SUPERTYPE OF(IfcAdvancedFace)
	{
		internal int mFaceSurface;// : IfcSurface;
		internal bool mSameSense = true;// : BOOLEAN;

		public IfcSurface FaceSurface { get { return mDatabase[mFaceSurface] as IfcSurface; } set { mFaceSurface = value.mIndex; } }
		public bool SameSense { get { return mSameSense; } set { mSameSense = value; } }

		internal IfcFaceSurface() : base() { } 
		internal IfcFaceSurface(DatabaseIfc db, IfcFaceSurface s) : base(db,s) { FaceSurface = db.Factory.Duplicate( s.FaceSurface) as IfcSurface; mSameSense = s.mSameSense; }
		public IfcFaceSurface(IfcFaceOuterBound bound, IfcSurface srf, bool sameSense) : base(bound) { mFaceSurface = srf.mIndex; mSameSense = sameSense; }
		public IfcFaceSurface(IfcFaceOuterBound outer, IfcFaceBound inner, IfcSurface srf, bool sameSense) : base(outer, inner) { mFaceSurface = srf.mIndex; mSameSense = sameSense; }
		public IfcFaceSurface(List<IfcFaceBound> bounds, IfcSurface srf, bool sameSense) : base(bounds) { mFaceSurface = srf.mIndex; mSameSense = sameSense; }
		internal new static IfcFaceSurface Parse(string strDef) { IfcFaceSurface s = new IfcFaceSurface(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcFaceSurface s, List<string> arrFields, ref int ipos) { IfcFace.parseFields(s, arrFields, ref ipos); s.mFaceSurface = ParserSTEP.ParseLink(arrFields[ipos++]); s.mSameSense = ParserSTEP.ParseBool(arrFields[ipos]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mFaceSurface) + "," + ParserSTEP.BoolToString(mSameSense); }
	}
	public partial class IfcFacetedBrep : IfcManifoldSolidBrep
	{
		internal IfcFacetedBrep() : base() { }
		public IfcFacetedBrep(IfcClosedShell s) : base(s) { }
		internal IfcFacetedBrep(DatabaseIfc db, IfcFacetedBrep b) : base(db,b) { }
		internal static IfcFacetedBrep Parse(string str) { IfcFacetedBrep b = new IfcFacetedBrep(); int pos = 0; b.Parse(str,ref pos, str.Length); return b; }
	}
	public partial class IfcFacetedBrepWithVoids : IfcFacetedBrep
	{
		internal List<int> mVoids = new List<int>();// : SET [1:?] OF IfcClosedShell
		public ReadOnlyCollection<IfcClosedShell> Voids { get { return new ReadOnlyCollection<IfcClosedShell>( mVoids.ConvertAll(x => mDatabase[x] as IfcClosedShell)); } }

		internal IfcFacetedBrepWithVoids() : base() { }
		internal IfcFacetedBrepWithVoids(DatabaseIfc db, IfcFacetedBrepWithVoids b) : base(db,b) { b.Voids.ToList().ForEach(x=>addVoid( db.Factory.Duplicate(x) as IfcClosedShell)); }
		internal new static IfcFacetedBrepWithVoids Parse(string str)
		{
			IfcFacetedBrepWithVoids b = new IfcFacetedBrepWithVoids();
			int pos = 0, len = str.Length;
			b.Parse(str, ref pos, len);
			b.mVoids = ParserSTEP.StripListLink(str, ref pos, len);
			return b;
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(";
			if (mVoids.Count > 0)
			{
				str += ParserSTEP.LinkToString(mVoids[0]);
				for (int icounter = 1; icounter < mVoids.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mVoids[icounter]);
			}
			return str + ")";
		}

		internal void addVoid(IfcClosedShell shell) { mVoids.Add(shell.mIndex); }
	}
	//ENTITY IfcFailureConnectionCondition
	public partial class IfcFan : IfcFlowMovingDevice //IFC4
	{
		internal IfcFanTypeEnum mPredefinedType = IfcFanTypeEnum.NOTDEFINED;// OPTIONAL : IfcFanTypeEnum;
		public IfcFanTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFan() : base() { }
		internal IfcFan(DatabaseIfc db, IfcFan f) : base(db,f) { mPredefinedType = f.mPredefinedType; }
		internal IfcFan(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFan s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcFanTypeEnum)Enum.Parse(typeof(IfcFanTypeEnum), str);
		}
		internal new static IfcFan Parse(string strDef) { IfcFan s = new IfcFan(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcFanTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcFanType : IfcFlowMovingDeviceType
	{
		internal IfcFanTypeEnum mPredefinedType = IfcFanTypeEnum.NOTDEFINED;// : IfcFanTypeEnum;
		public IfcFanTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFanType() : base() { }
		internal IfcFanType(DatabaseIfc db, IfcFanType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal static void parseFields(IfcFanType t, List<string> arrFields, ref int ipos) { IfcFlowMovingDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFanTypeEnum)Enum.Parse(typeof(IfcFanTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFanType Parse(string strDef) { IfcFanType t = new IfcFanType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcFastener : IfcElementComponent
	{
		internal IfcFastenerTypeEnum mPredefinedType = IfcFastenerTypeEnum.NOTDEFINED;// : IfcFastenerTypeEnum; //IFC4
		public IfcFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFastener() : base() { }
		internal IfcFastener(DatabaseIfc db, IfcFastener f) : base(db, f) { mPredefinedType = f.mPredefinedType; }
		internal IfcFastener(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		
		internal static void parseFields(IfcFastener f, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcElementComponent.parseFields(f, arrFields, ref ipos);
			if (schema != ReleaseVersion.IFC2x3)
				f.mPredefinedType = (IfcFastenerTypeEnum)Enum.Parse(typeof(IfcFastenerTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		internal static IfcFastener Parse(string strDef, ReleaseVersion schema) { int ipos = 0; IfcFastener f = new IfcFastener(); parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return f; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcFastenerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcFastenerType : IfcElementComponentType
	{
		internal IfcFastenerTypeEnum mPredefinedType = IfcFastenerTypeEnum.NOTDEFINED;// : IfcFastenerTypeEnum; //IFC4
		public IfcFastenerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFastenerType() : base() { }
		internal IfcFastenerType(DatabaseIfc db, IfcFastenerType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcFastenerType(DatabaseIfc m, string name, IfcFastenerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static IfcFastenerType Parse(string strDef, ReleaseVersion schema) { int ipos = 0; IfcFastenerType t = new IfcFastenerType(); parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return t; }
		internal static void parseFields(IfcFastenerType t, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcElementComponentType.parseFields(t, arrFields, ref ipos); if (schema != ReleaseVersion.IFC2x3) t.mPredefinedType = (IfcFastenerTypeEnum)Enum.Parse(typeof(IfcFastenerTypeEnum), arrFields[ipos++].Replace(".", "")); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : ",." + mPredefinedType + "."); }
	}
	public abstract partial class IfcFeatureElement : IfcElement //	ABSTRACT SUPERTYPE OF(ONEOF(IfcFeatureElementAddition, IfcFeatureElementSubtraction, IfcSurfaceFeature))
	{
		protected IfcFeatureElement() : base() { }
		protected IfcFeatureElement(DatabaseIfc db) : base(db) {  }
		protected IfcFeatureElement(DatabaseIfc db, IfcFeatureElement e) : base(db, e,false) { }
		protected IfcFeatureElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		protected static void parseFields(IfcFeatureElement e, List<string> arrFields, ref int ipos) { IfcElement.parseFields(e, arrFields, ref ipos); }
	}
	public abstract partial class IfcFeatureElementAddition : IfcFeatureElement //ABSTRACT SUPERTYPE OF(IfcProjectionElement)
	{	//INVERSE
		internal List<IfcRelProjectsElement> mProjectsElements = new List<IfcRelProjectsElement>();
		public ReadOnlyCollection<IfcRelProjectsElement> ProjectsElements { get { return new ReadOnlyCollection<IfcRelProjectsElement>( mProjectsElements); } }

		protected IfcFeatureElementAddition() : base() { }
		protected IfcFeatureElementAddition(DatabaseIfc db, IfcFeatureElementAddition e) : base(db,e) { }
		protected static void parseFields(IfcFeatureElementAddition e, List<string> arrFields, ref int ipos) { IfcFeatureElement.parseFields(e, arrFields, ref ipos); }
	}
	public abstract partial class IfcFeatureElementSubtraction : IfcFeatureElement //ABSTRACT SUPERTYPE OF (ONEOF (IfcOpeningElement ,IfcVoidingFeature)) 
	{ //INVERSE
		internal IfcRelVoidsElement mVoidsElement = null;
		public IfcRelVoidsElement VoidsElement { get { return mVoidsElement; } }

		protected IfcFeatureElementSubtraction() : base() { }
		protected IfcFeatureElementSubtraction(DatabaseIfc db, IfcFeatureElementSubtraction s) : base(db,s) { }
		protected IfcFeatureElementSubtraction(DatabaseIfc db) : base(db) {  }
		protected IfcFeatureElementSubtraction(IfcElement host, IfcProductRepresentation rep) : base(host.mDatabase)
		{
			new IfcRelVoidsElement(host, this);
			Representation = rep;
			Placement = new IfcLocalPlacement(host.Placement, mDatabase.Factory.XYPlanePlacement);	
		}
		
		protected static void parseFields(IfcFeatureElementSubtraction e, List<string> arrFields, ref int ipos) { IfcFeatureElement.parseFields(e, arrFields, ref ipos); }
	}
	public partial class IfcFillAreaStyleHatching : IfcGeometricRepresentationItem
	{
		internal int mHatchLineAppearance;// : IfcCurveStyle;
		internal string mStartOfNextHatchLine;// : IfcHatchLineDistanceSelect; IfcOneDirectionRepeatFactor,IfcPositiveLengthMeasure
		internal int mPointOfReferenceHatchLine;// : OPTIONAL IfcCartesianPoint; //DEPRECEATED IFC4
		internal int mPatternStart;// : OPTIONAL IfcCartesianPoint;
		internal double mHatchLineAngle;// : IfcPlaneAngleMeasure;

		public IfcCurveStyle HatchLineAppearance { get { return mDatabase[mHatchLineAppearance] as IfcCurveStyle; } set { mHatchLineAppearance = value.mIndex; } }
		public IfcCartesianPoint PatternStart { get { return mDatabase[mPatternStart] as IfcCartesianPoint; } set { mPatternStart = (value == null ? 0 : value.mIndex); } }
			
		internal IfcFillAreaStyleHatching() : base() { }
		internal IfcFillAreaStyleHatching(DatabaseIfc db, IfcFillAreaStyleHatching h) : base(db,h)
		{
			mHatchLineAppearance = db.Factory.Duplicate( h.HatchLineAppearance).mIndex;
			mStartOfNextHatchLine = h.mStartOfNextHatchLine;
			if(h.mPointOfReferenceHatchLine > 0)
				mPointOfReferenceHatchLine = db.Factory.Duplicate( h.mDatabase[h.mPointOfReferenceHatchLine]).mIndex;
			if(h.mPatternStart > 0)
				PatternStart = db.Factory.Duplicate( h.PatternStart) as IfcCartesianPoint;
			mHatchLineAngle = h.mHatchLineAngle;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mHatchLineAppearance) + "," + mStartOfNextHatchLine + "," + ParserSTEP.LinkToString(mPointOfReferenceHatchLine) + "," + ParserSTEP.LinkToString(mPatternStart) + "," + ParserSTEP.DoubleToString(mHatchLineAngle); }
		internal static IfcFillAreaStyleHatching Parse(string str)
		{
			IfcFillAreaStyleHatching h = new IfcFillAreaStyleHatching();
			int pos = 0, len = str.Length;
			h.mHatchLineAppearance = ParserSTEP.StripLink(str, ref pos, len);
			h.mStartOfNextHatchLine = ParserSTEP.StripField(str, ref pos, len);
			h.mPointOfReferenceHatchLine = ParserSTEP.StripLink(str, ref pos, len);
			h.mPatternStart = ParserSTEP.StripLink(str, ref pos, len);
			h.mHatchLineAngle = ParserSTEP.StripDouble(str, ref pos, len);
			return h;
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcFillAreaStyleTileSymbolWithStyle // DEPRECEATED IFC4
	//ENTITY IfcFillAreaStyleTiles
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcFilter : IfcFlowTreatmentDevice //IFC4  
	{
		internal IfcFilterTypeEnum mPredefinedType = IfcFilterTypeEnum.NOTDEFINED;
		public IfcFilterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFilter() : base() { }
		internal IfcFilter(DatabaseIfc db, IfcFilter f) : base(db,f) { mPredefinedType = f.mPredefinedType; }
		internal IfcFilter(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcFilter a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcFilterTypeEnum)Enum.Parse(typeof(IfcFilterTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcFilter Parse(string strDef) { IfcFilter d = new IfcFilter(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcFilterTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcFilterType : IfcFlowTreatmentDeviceType
	{
		internal IfcFilterTypeEnum mPredefinedType = IfcFilterTypeEnum.NOTDEFINED;// : IfcFilterTypeEnum;
		public IfcFilterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFilterType() : base() { }
		internal IfcFilterType(DatabaseIfc db, IfcFilterType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal static void parseFields(IfcFilterType t, List<string> arrFields, ref int ipos) { IfcFlowTreatmentDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFilterTypeEnum)Enum.Parse(typeof(IfcFilterTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFilterType Parse(string strDef) { IfcFilterType t = new IfcFilterType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcFillAreaStyle : IfcPresentationStyle
	{
		internal List<int> mFillStyles = new List<int>();// : SET [1:?] OF IfcFillStyleSelect;
		internal IfcFillAreaStyle() : base() { }
		//internal IfcFillAreaStyle(IfcFillAreaStyle i) : base(i) { mFillStyles = new List<int>(i.mFillStyles.ToArray()); }
		internal IfcFillAreaStyle(DatabaseIfc db) : base(db) { }
		internal static void parseFields(IfcFillAreaStyle s, List<string> arrFields, ref int ipos) { IfcPresentationStyle.parseFields(s, arrFields, ref ipos); s.mFillStyles = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcFillAreaStyle Parse(string strDef) { IfcFillAreaStyle s = new IfcFillAreaStyle(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mFillStyles[0]);
			for (int icounter = 1; icounter < mFillStyles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mFillStyles[icounter]);
			return str + ")";
		}
	}
	public interface IfcFillStyleSelect { } // SELECT ( IfcFillAreaStyleHatching, IfcFillAreaStyleTiles, IfcExternallyDefinedHatchStyle, IfcColour);
	public partial class IfcFireSuppressionTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcFireSuppressionTerminalTypeEnum mPredefinedType = IfcFireSuppressionTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcFireSuppressinTerminalTypeEnum;
		public IfcFireSuppressionTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcFireSuppressionTerminal() : base() { }
		internal IfcFireSuppressionTerminal(DatabaseIfc db, IfcFireSuppressionTerminal t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcFireSuppressionTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		
		internal static void parseFields(IfcFireSuppressionTerminal s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcFireSuppressionTerminalTypeEnum)Enum.Parse(typeof(IfcFireSuppressionTerminalTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcFireSuppressionTerminal Parse(string strDef) { IfcFireSuppressionTerminal s = new IfcFireSuppressionTerminal(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcFireSuppressionTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcFireSuppressionTerminalType : IfcFlowTerminalType
	{
		internal IfcFireSuppressionTerminalTypeEnum mPredefinedType = IfcFireSuppressionTerminalTypeEnum.NOTDEFINED;// : IfcFireSuppressionTerminalTypeEnum;
		public IfcFireSuppressionTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFireSuppressionTerminalType() : base() { }
		internal IfcFireSuppressionTerminalType(DatabaseIfc db, IfcFireSuppressionTerminalType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcFireSuppressionTerminalType(DatabaseIfc m, string name, IfcFireSuppressionTerminalTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcFireSuppressionTerminalType t, List<string> arrFields, ref int ipos) { IfcFlowTerminalType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFireSuppressionTerminalTypeEnum)Enum.Parse(typeof(IfcFireSuppressionTerminalTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFireSuppressionTerminalType Parse(string strDef) { IfcFireSuppressionTerminalType t = new IfcFireSuppressionTerminalType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcFixedReferenceSweptAreaSolid : IfcSweptAreaSolid //IFC4
	{
		internal int mDirectrix; // : IfcCurve;
		internal double mStartParam = double.NaN;// : OPT IfcParameterValue;  
		internal double mEndParam = double.NaN;//: OPT IfcParameterValue; 
		internal int mFixedReference;// : 	IfcDirection; 

		public IfcCurve Directrix { get { return mDatabase[mDirectrix] as IfcCurve; } set { mDirectrix = value.mIndex; } }
		public IfcDirection FixedReference { get { return mDatabase[mFixedReference] as IfcDirection; } set { mFixedReference = value.mIndex; } }

		internal IfcFixedReferenceSweptAreaSolid() : base() { }
		internal IfcFixedReferenceSweptAreaSolid(DatabaseIfc db, IfcFixedReferenceSweptAreaSolid s) : base(db, s)
		{
			Directrix = db.Factory.Duplicate(s.Directrix) as IfcCurve;
			mStartParam = s.mStartParam;
			mEndParam = s.mEndParam;
			FixedReference = db.Factory.Duplicate(s.FixedReference) as IfcDirection;
		}
		public IfcFixedReferenceSweptAreaSolid(IfcProfileDef sweptArea, IfcCurve directrix, IfcDirection reference) : base(sweptArea)
		{
			Directrix = directrix;
			FixedReference = reference;
		}
		internal static IfcFixedReferenceSweptAreaSolid Parse(string str)
		{
			IfcFixedReferenceSweptAreaSolid s = new IfcFixedReferenceSweptAreaSolid();
			int pos = 0;
			s.Parse(str, ref pos, str.Length);
			return s;
		}
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mDirectrix = ParserSTEP.StripLink(str, ref pos, len);
			mStartParam = ParserSTEP.StripDouble(str, ref pos, len);
			mEndParam = ParserSTEP.StripDouble(str, ref pos, len);
			mFixedReference = ParserSTEP.StripLink(str, ref pos, len);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mDirectrix) + "," + ParserSTEP.DoubleOptionalToString(mStartParam) + "," + ParserSTEP.DoubleOptionalToString(mEndParam) + "," + ParserSTEP.LinkToString(mFixedReference); }
	}
	public partial class IfcFlowController : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcAirTerminalBox, IfcDamper
	{ //, IfcElectricDistributionBoard, IfcElectricTimeControl, IfcFlowMeter, IfcProtectiveDevice, IfcSwitchingDevice, IfcValve))
		public override string KeyWord { get { return mDatabase.mRelease == ReleaseVersion.IFC2x3 && this as IfcElectricDistributionPoint == null ? "IfcFlowController" : base.KeyWord; } }

		internal IfcFlowController() : base() { }
		internal IfcFlowController(DatabaseIfc db, IfcFlowController c) : base(db,c) { }
		public IfcFlowController(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFlowController c, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(c, arrFields, ref ipos); }
		internal new static IfcFlowController Parse(string strDef) { IfcFlowController c = new IfcFlowController(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
	}
	public abstract partial class IfcFlowControllerType : IfcDistributionFlowElementType
	{
		protected IfcFlowControllerType() : base() { }
		protected IfcFlowControllerType(DatabaseIfc db) : base(db) { }
		protected IfcFlowControllerType(DatabaseIfc db, IfcFlowControllerType t) : base(db, t) { }
		protected static void parseFields(IfcFlowControllerType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcFlowFitting : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcCableCarrierFitting, IfcCableFitting, IfcDuctFitting, IfcJunctionBox, IfcPipeFitting))
	{
		public override string KeyWord { get { return mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcFlowFitting" : base.KeyWord; } }

		internal IfcFlowFitting() : base() { }
		internal IfcFlowFitting(DatabaseIfc db, IfcFlowFitting f) : base(db,f) { }
		public IfcFlowFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFlowFitting f, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(f, arrFields, ref ipos); }
		internal new static IfcFlowFitting Parse(string strDef) { IfcFlowFitting f = new IfcFlowFitting(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
	}
	public abstract partial class IfcFlowFittingType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcCableCarrierFittingType ,IfcDuctFittingType ,IfcJunctionBoxType ,IfcPipeFittingType))
	{
		protected IfcFlowFittingType() : base() { }
		protected IfcFlowFittingType(DatabaseIfc db) : base(db) { }
		protected IfcFlowFittingType(DatabaseIfc db, IfcFlowFittingType t) : base(db, t) { }
		protected static void parseFields(IfcFlowFittingType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcFlowInstrument : IfcDistributionControlElement //IFC4  
	{
		internal IfcFlowInstrumentTypeEnum mPredefinedType = IfcFlowInstrumentTypeEnum.NOTDEFINED;
		public IfcFlowInstrumentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowInstrument() : base() { }
		internal IfcFlowInstrument(DatabaseIfc db, IfcFlowInstrument i) : base(db,i) { mPredefinedType = i.mPredefinedType; }
		internal IfcFlowInstrument(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcFlowInstrument a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcFlowInstrumentTypeEnum)Enum.Parse(typeof(IfcFlowInstrumentTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcFlowInstrument Parse(string strDef) { IfcFlowInstrument d = new IfcFlowInstrument(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcFlowInstrumentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcFlowInstrumentType : IfcDistributionControlElementType
	{
		internal IfcFlowInstrumentTypeEnum mPredefinedType = IfcFlowInstrumentTypeEnum.NOTDEFINED;// : IfcFlowInstrumentTypeEnum;
		public IfcFlowInstrumentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowInstrumentType() : base() { }
		internal IfcFlowInstrumentType(DatabaseIfc db, IfcFlowInstrumentType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcFlowInstrumentType(DatabaseIfc m, string name, IfcFlowInstrumentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcFlowInstrumentType t, List<string> arrFields, ref int ipos) { IfcDistributionControlElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFlowInstrumentTypeEnum)Enum.Parse(typeof(IfcFlowInstrumentTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFlowInstrumentType Parse(string strDef) { IfcFlowInstrumentType t = new IfcFlowInstrumentType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcFlowMeter : IfcFlowController //IFC4
	{
		internal IfcFlowMeterTypeEnum mPredefinedType = IfcFlowMeterTypeEnum.NOTDEFINED;// OPTIONAL : IfcDamperTypeEnum;
		public IfcFlowMeterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowMeter() : base() { }
		internal IfcFlowMeter(DatabaseIfc db, IfcFlowMeter m) : base(db, m) { mPredefinedType = m.mPredefinedType; }
		internal IfcFlowMeter(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFlowMeter s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcFlowMeterTypeEnum)Enum.Parse(typeof(IfcFlowMeterTypeEnum), str);
		}
		internal new static IfcFlowMeter Parse(string strDef) { IfcFlowMeter s = new IfcFlowMeter(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcFlowMeterTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcFlowMeterType : IfcFlowControllerType
	{
		internal IfcFlowMeterTypeEnum mPredefinedType = IfcFlowMeterTypeEnum.NOTDEFINED;// : IfcFlowMeterTypeEnum;
		public IfcFlowMeterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFlowMeterType() : base() { }
		internal IfcFlowMeterType(DatabaseIfc db, IfcFlowMeterType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcFlowMeterType(DatabaseIfc m, string name, IfcFlowMeterTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcFlowMeterType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFlowMeterTypeEnum)Enum.Parse(typeof(IfcFlowMeterTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFlowMeterType Parse(string strDef) { IfcFlowMeterType t = new IfcFlowMeterType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcFlowMovingDevice : IfcDistributionFlowElement //	SUPERTYPE OF(ONEOF(IfcCompressor, IfcFan, IfcPump))
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcFlowMovingDevice" : base.KeyWord); } }

		internal IfcFlowMovingDevice() : base() { }
		internal IfcFlowMovingDevice(DatabaseIfc db, IfcFlowMovingDevice d) : base(db, d) { }
		internal IfcFlowMovingDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFlowMovingDevice d, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(d, arrFields, ref ipos); }
		internal new static IfcFlowMovingDevice Parse(string strDef) { IfcFlowMovingDevice d = new IfcFlowMovingDevice(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
	}
	public abstract partial class IfcFlowMovingDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF(ONEOF(IfcCompressorType, IfcFanType, IfcPumpType))
	{
		protected IfcFlowMovingDeviceType() : base() { }
		protected IfcFlowMovingDeviceType(DatabaseIfc db) : base(db) { }
		protected IfcFlowMovingDeviceType(DatabaseIfc db, IfcFlowMovingDeviceType t) : base(db, t) { }
		protected static void parseFields(IfcFlowMovingDeviceType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcFlowSegment : IfcDistributionFlowElement //	SUPERTYPE OF(ONEOF(IfcCableCarrierSegment, IfcCableSegment, IfcDuctSegment, IfcPipeSegment))
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcFlowSegment" : base.KeyWord); } }

		internal IfcFlowSegment() : base() { }
		internal IfcFlowSegment(DatabaseIfc db, IfcFlowSegment s) : base(db, s) { }
		public IfcFlowSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcFlowSegment s, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(s, arrFields, ref ipos); }
		internal new static IfcFlowSegment Parse(string strDef) { IfcFlowSegment s = new IfcFlowSegment(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public abstract partial class IfcFlowSegmentType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcCableCarrierSegmentType ,IfcCableSegmentType ,IfcDuctSegmentType ,IfcPipeSegmentType))
	{
		protected IfcFlowSegmentType() : base() { }
		protected IfcFlowSegmentType(DatabaseIfc db) : base(db) { }
		protected IfcFlowSegmentType(DatabaseIfc db, IfcFlowSegmentType t) : base(db, t) { }
		protected static void parseFields(IfcFlowSegmentType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcFlowStorageDevice : IfcDistributionFlowElement //SUPERTYPE OF(ONEOF(IfcElectricFlowStorageDevice, IfcTank))
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcFlowStorageDevice" : base.KeyWord); } }

		internal IfcFlowStorageDevice() : base() { }
		internal IfcFlowStorageDevice(DatabaseIfc db, IfcFlowStorageDevice d) : base(db,d) { }
		internal IfcFlowStorageDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcFlowStorageDevice d, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(d, arrFields, ref ipos); }
		internal new static IfcFlowStorageDevice Parse(string strDef) { IfcFlowStorageDevice d = new IfcFlowStorageDevice(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
	}
	public abstract partial class IfcFlowStorageDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcElectricFlowStorageDeviceType ,IfcTankType))
	{
		protected IfcFlowStorageDeviceType() : base() { }
		protected IfcFlowStorageDeviceType(DatabaseIfc db, IfcFlowStorageDeviceType t) : base(db, t) { }
		protected static void parseFields(IfcFlowStorageDeviceType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcFlowTerminal : IfcDistributionFlowElement 	//SUPERTYPE OF(ONEOF(IfcAirTerminal, IfcAudioVisualAppliance, IfcCommunicationsAppliance, IfcElectricAppliance, IfcFireSuppressionTerminal, IfcLamp, IfcLightFixture, IfcMedicalDevice, IfcOutlet, IfcSanitaryTerminal, IfcSpaceHeater, IfcStackTerminal, IfcWasteTerminal))
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcFlowTerminal" : base.KeyWord); } }

		internal IfcFlowTerminal() : base() { }
		protected IfcFlowTerminal(IfcFlowTerminal basis) : base(basis) { }
		internal IfcFlowTerminal(DatabaseIfc db, IfcFlowTerminal t) : base(db, t) { }
		public IfcFlowTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcFlowTerminal t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(t, arrFields, ref ipos); }
		internal new static IfcFlowTerminal Parse(string strDef) { IfcFlowTerminal t = new IfcFlowTerminal(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public abstract partial class IfcFlowTerminalType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcAirTerminalType ,
	{ // IfcElectricApplianceType ,IfcElectricHeaterType ,IfcFireSuppressionTerminalType,IfcLampType ,IfcLightFixtureType ,IfcOutletType ,IfcSanitaryTerminalType ,IfcStackTerminalType ,IfcWasteTerminalType)) 
		// IFC4 deleted ,IfcGasTerminalType 
		protected IfcFlowTerminalType() : base() { }
		protected IfcFlowTerminalType(IfcDistributionFlowElementType basis) : base(basis) { }
		protected IfcFlowTerminalType(DatabaseIfc db) : base(db) { }
		protected IfcFlowTerminalType(DatabaseIfc db, IfcFlowTerminalType t) : base(db,t) { }
		protected static void parseFields(IfcFlowTerminalType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcFlowTreatmentDevice : IfcDistributionFlowElement // 	SUPERTYPE OF(ONEOF(IfcDuctSilencer, IfcFilter, IfcInterceptor))
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcFlowTreatmentDevice" : base.KeyWord); } }

		internal IfcFlowTreatmentDevice() : base() { }
		internal IfcFlowTreatmentDevice(DatabaseIfc db, IfcFlowTreatmentDevice d) : base(db, d) { }
		internal IfcFlowTreatmentDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		
		internal static void parseFields(IfcFlowTreatmentDevice d, List<string> arrFields, ref int ipos) { IfcDistributionFlowElement.parseFields(d, arrFields, ref ipos); }
		internal new static IfcFlowTreatmentDevice Parse(string strDef) { IfcFlowTreatmentDevice d = new IfcFlowTreatmentDevice(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
	}
	public abstract partial class IfcFlowTreatmentDeviceType : IfcDistributionFlowElementType //ABSTRACT SUPERTYPE OF(ONEOF(IfcDuctSilencerType, IfcFilterType, IfcInterceptorType))
	{
		protected IfcFlowTreatmentDeviceType() : base() { }
		protected IfcFlowTreatmentDeviceType(DatabaseIfc db,  IfcFlowTreatmentDeviceType t) : base(db, t) { }
		protected static void parseFields(IfcFlowTreatmentDeviceType t, List<string> arrFields, ref int ipos) { IfcDistributionFlowElementType.parseFields(t, arrFields, ref ipos); }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcFluidFlowProperties : IfcPropertySetDefinition 
	{
		internal IfcPropertySourceEnum mPropertySource;// : IfcPropertySourceEnum;
		internal int mFlowConditionTimeSeries, mVelocityTimeSeries, mFlowrateTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal int mFluid;// : IfcMaterial;
		internal int mPressureTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal string mUserDefinedPropertySource = "$";// : OPTIONAL IfcLabel;
		internal double mTemperatureSingleValue = double.NaN, mWetBulbTemperatureSingleValue = double.NaN;// : OPTIONAL IfcThermodynamicTemperatureMeasure;
		internal int mWetBulbTemperatureTimeSeries, mTemperatureTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal double mFlowrateSingleValue = double.NaN;// : OPTIONAL IfcDerivedMeasureValue;
		internal double mFlowConditionSingleValue = double.NaN;// : OPTIONAL IfcPositiveRatioMeasure;
		internal double mVelocitySingleValue = double.NaN;// : OPTIONAL IfcLinearVelocityMeasure;
		internal double mPressureSingleValue = double.NaN;// : OPTIONAL IfcPressureMeasure;

		internal IfcFluidFlowProperties() : base() { }
		internal IfcFluidFlowProperties(DatabaseIfc db, IfcFluidFlowProperties p) : base(db, p)
		{
			mPropertySource = p.mPropertySource;
			//if(p.mFlowConditionTimeSeries > 0)
			//	mFlowConditionTimeSeries = p.mFlowConditionTimeSeries;

			//mVelocityTimeSeries = p.mVelocityTimeSeries;
			//mFlowrateTimeSeries = p.mFlowrateTimeSeries;
			//mFluid = p.mFluid;
			//mPressureTimeSeries = p.mPressureTimeSeries;
			mUserDefinedPropertySource = p.mUserDefinedPropertySource;
			mTemperatureSingleValue = p.mTemperatureSingleValue;
			mWetBulbTemperatureSingleValue = p.mWetBulbTemperatureSingleValue;
			//mWetBulbTemperatureTimeSeries = p.mWetBulbTemperatureTimeSeries;
			//mTemperatureTimeSeries = p.mTemperatureTimeSeries;
			mFlowrateSingleValue = p.mFlowrateSingleValue;
			mFlowConditionSingleValue = p.mFlowConditionSingleValue;
			mVelocitySingleValue = p.mVelocitySingleValue;
			mPressureSingleValue = p.mPressureSingleValue;
		}
		internal IfcFluidFlowProperties(DatabaseIfc db, string name) : base(db, name) { }
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
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",." + mPropertySource.ToString() + ".," + ParserSTEP.LinkToString(mFlowConditionTimeSeries) + "," + ParserSTEP.LinkToString(mVelocityTimeSeries) + "," + ParserSTEP.LinkToString(mFlowrateTimeSeries)
				+ "," + ParserSTEP.LinkToString(mFluid) + "," + ParserSTEP.LinkToString(mPressureTimeSeries) + "," + mUserDefinedPropertySource + "," + ParserSTEP.DoubleOptionalToString(mTemperatureSingleValue)
				+ "," + ParserSTEP.DoubleOptionalToString(mWetBulbTemperatureSingleValue) + "," + ParserSTEP.LinkToString(mWetBulbTemperatureTimeSeries) + "," + ParserSTEP.LinkToString(mTemperatureTimeSeries) + "," + ParserSTEP.DoubleOptionalToString(mFlowrateSingleValue)
				+ "," + ParserSTEP.DoubleOptionalToString(mFlowConditionSingleValue) + "," + ParserSTEP.DoubleOptionalToString(mVelocitySingleValue) + "," + ParserSTEP.DoubleOptionalToString(mPressureSingleValue);
		}

	}
	public partial class IfcFooting : IfcBuildingElement
	{
		internal IfcFootingTypeEnum mPredefinedType = IfcFootingTypeEnum.NOTDEFINED;// OPTIONAL : IfcFootingTypeEnum;
		public IfcFootingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFooting() : base() { }
		internal IfcFooting(DatabaseIfc db, IfcFooting f, bool downStream) : base(db, f, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcFooting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcFooting Parse(string str) { IfcFooting f = new IfcFooting(); int pos = 0; f.Parse(str, ref pos, str.Length); return f; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFootingTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcFootingType : IfcBuildingElementType
	{
		internal IfcFootingTypeEnum mPredefinedType = IfcFootingTypeEnum.NOTDEFINED;
		public IfcFootingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcFootingType() : base() { }
		internal IfcFootingType(DatabaseIfc db, IfcFootingType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcFootingType(DatabaseIfc m, string name, IfcFootingTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcFootingType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcFootingTypeEnum)Enum.Parse(typeof(IfcFootingTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcFootingType Parse(string strDef) { IfcFootingType t = new IfcFootingType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."); }

	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcFuelProperties
	public partial class IfcFurnishingElement : IfcElement // DEPRECEATED IFC4 to make abstract SUPERTYPE OF(ONEOF(IfcFurniture, IfcSystemFurnitureElement))
	{
		internal IfcFurnishingElement() : base() { }
		internal IfcFurnishingElement(DatabaseIfc db, IfcFurnishingElement e) : base(db, e,false) { }
		internal IfcFurnishingElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static void parseFields(IfcFurnishingElement e, List<string> arrFields, ref int ipos) { IfcElement.parseFields(e, arrFields, ref ipos); }
		internal static IfcFurnishingElement Parse(string strDef) { IfcFurnishingElement e = new IfcFurnishingElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
	}
	public partial class IfcFurnishingElementType : IfcElementType //IFC4 Depreceated //SUPERTYPE OF (ONEOF (IfcFurnitureType ,IfcSystemFurnitureElementType))
	{
		internal IfcFurnishingElementType() : base() { }
		internal IfcFurnishingElementType(DatabaseIfc db, IfcFurnishingElementType t) : base(db, t) { }
		internal IfcFurnishingElementType(DatabaseIfc db,string name) : base(db) { Name = name; }
		internal static void parseFields(IfcFurnishingElementType t, List<string> arrFields, ref int ipos) { IfcElementType.parseFields(t, arrFields, ref ipos); }
		internal new static IfcFurnishingElementType Parse(string strDef) { IfcFurnishingElementType t = new IfcFurnishingElementType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public partial class IfcFurniture : IfcFurnishingElement
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcFurnishingElement" : base.KeyWord); } }
		internal IfcFurnitureTypeEnum mPredefinedType = IfcFurnitureTypeEnum.NOTDEFINED;//: OPTIONAL IfcFurnitureTypeEnum;
		internal IfcFurniture() : base() { }
		internal IfcFurniture(DatabaseIfc db, IfcFurniture f) : base(db, f) { mPredefinedType = f.mPredefinedType; }
		internal IfcFurniture(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static void parseFields(IfcFurniture e, List<string> arrFields, ref int ipos)
		{
			IfcFurnishingElement.parseFields(e, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				e.mPredefinedType = (IfcFurnitureTypeEnum)Enum.Parse(typeof(IfcFurnitureTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcFurniture Parse(string strDef) { IfcFurniture e = new IfcFurniture(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : ",." + mPredefinedType + "."); }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	internal class IfcFurnitureStandard : IfcControl 
	{
		internal IfcFurnitureStandard() : base() { }
		internal IfcFurnitureStandard(DatabaseIfc db, IfcFurnitureStandard s) : base(db,s) { }
		internal static IfcFurnitureStandard Parse(string strDef, ReleaseVersion schema) { IfcFurnitureStandard s = new IfcFurnitureStandard(); int ipos = 0; IfcControl.parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return s; }
	}
	public partial class IfcFurnitureType : IfcFurnishingElementType
	{
		internal IfcAssemblyPlaceEnum mAssemblyPlace = IfcAssemblyPlaceEnum.NOTDEFINED;
		internal IfcFurnitureTypeEnum mPredefinedType = IfcFurnitureTypeEnum.NOTDEFINED; // IFC4 OPTIONAL
		internal IfcFurnitureType() : base() { }
		internal IfcFurnitureType(DatabaseIfc db, IfcFurnitureType t) : base(db, t) { mAssemblyPlace = t.mAssemblyPlace; mPredefinedType = t.mPredefinedType; }
		internal IfcFurnitureType(DatabaseIfc m, string name, IfcAssemblyPlaceEnum a, IfcFurnitureTypeEnum type) : base(m,name)
		{
			mAssemblyPlace = a;
			mPredefinedType = type;
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3 && string.IsNullOrEmpty(ElementType) && type != IfcFurnitureTypeEnum.NOTDEFINED)
				ElementType = type.ToString();
		}

		internal static void parseFields(IfcFurnitureType t, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcFurnishingElementType.parseFields(t, arrFields, ref ipos);
			t.mAssemblyPlace = (IfcAssemblyPlaceEnum)Enum.Parse(typeof(IfcAssemblyPlaceEnum), arrFields[ipos++].Replace(".", ""));
			if (schema != ReleaseVersion.IFC2x3)
			{
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					t.mPredefinedType = (IfcFurnitureTypeEnum)Enum.Parse(typeof(IfcFurnitureTypeEnum), s.Replace(".", ""));
			}
		}
		internal static IfcFurnitureType Parse(string strDef, ReleaseVersion schema) { IfcFurnitureType t = new IfcFurnitureType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mAssemblyPlace.ToString() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "." : ".,." + mPredefinedType + "."); }
	}
}
