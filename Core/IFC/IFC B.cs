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
	public partial class IfcBeam : IfcBuildingElement
	{
		internal IfcBeamTypeEnum mPredefinedType = IfcBeamTypeEnum.NOTDEFINED;//: OPTIONAL IfcBeamTypeEnum; IFC4
		public IfcBeamTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBeam() : base() { }
		public IfcBeam(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
 
		internal static void parseFields(IfcBeam b, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcBuildingElement.parseFields(b, arrFields, ref ipos);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					b.mPredefinedType = (IfcBeamTypeEnum)Enum.Parse(typeof(IfcBeamTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		internal static IfcBeam Parse(string strDef, ReleaseVersion schema) { IfcBeam b = new IfcBeam(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return b; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcBeamTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcBeamStandardCase : IfcBeam
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mDatabase.mModelView == ModelView.Ifc4Reference ? "IfcBeam" : base.KeyWord); } }

		internal IfcBeamStandardCase() : base() { }

		internal new static IfcBeamStandardCase Parse(string strDef, ReleaseVersion schema) { IfcBeamStandardCase c = new IfcBeamStandardCase(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return c; }
		internal static void parseFields(IfcBeamStandardCase c, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcBeam.parseFields(c, arrFields, ref ipos,schema); }
	}
	public partial class IfcBeamType : IfcBuildingElementType
	{
		internal IfcBeamTypeEnum mPredefinedType;
		public IfcBeamTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBeamType() : base() { }
		internal IfcBeamType(DatabaseIfc db, IfcBeamType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcBeamType(DatabaseIfc m, string name, IfcBeamTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcBeamType(string name, IfcMaterialProfile mp, IfcBeamTypeEnum type) : this(name, new IfcMaterialProfileSet(name, mp), type) { }
		public IfcBeamType(string name, IfcMaterialProfileSet ps, IfcBeamTypeEnum type) : base(ps.mDatabase)
		{
			Name = name;
			mPredefinedType = type;
			if(ps.mTaperEnd != null)
				mTapering = ps;
			else
				MaterialSelect = ps;
		}
		internal static void parseFields(IfcBeamType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcBeamTypeEnum)Enum.Parse(typeof(IfcBeamTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcBeamType Parse(string strDef) { IfcBeamType t = new IfcBeamType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public interface IfcBendingParameterSelect { } // 	IfcLengthMeasure, IfcPlaneAngleMeasure
	public partial class IfcBezierCurve : IfcBSplineCurve // DEPRECEATED IFC4
	{
		internal IfcBezierCurve() : base() { }
		internal IfcBezierCurve(DatabaseIfc db, IfcBezierCurve c) : base(db,c) { }
		internal static void parseFields(IfcBezierCurve c, List<string> arrFields, ref int ipos) { IfcBSplineCurve.parseFields(c, arrFields, ref ipos); }
		internal static IfcBezierCurve Parse(string strDef) { IfcBezierCurve c = new IfcBezierCurve(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
	}	
	public partial class IfcBlobTexture : IfcSurfaceTexture
	{
		internal string mRasterFormat;// : IfcIdentifier;
		internal bool mRasterCode;// : BOOLEAN;	
		internal IfcBlobTexture() : base() { }
		internal IfcBlobTexture(IfcBlobTexture i) : base(i) { mRasterFormat = i.mRasterFormat; mRasterCode = i.mRasterCode; }
		internal static IfcBlobTexture Parse(string strDef, ReleaseVersion schema) { IfcBlobTexture t = new IfcBlobTexture(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return t; }
		internal static void parseFields(IfcBlobTexture t, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcSurfaceTexture.parseFields(t, arrFields, ref ipos,schema); t.mRasterFormat = arrFields[ipos++].Replace("'", ""); t.mRasterCode = ParserSTEP.ParseBool(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mRasterFormat + "'," + ParserSTEP.BoolToString(mRasterCode); }
	}
	public partial class IfcBlock : IfcCsgPrimitive3D
	{
		private double mXLength, mYLength, mZLength;// : IfcPositiveLengthMeasure;

		public double XLength { get { return mXLength; } set { mXLength = value; } }
		public double YLength { get { return mYLength; } set { mYLength = value; } }
		public double ZLength { get { return mZLength; } set { mZLength = value; } }

		internal IfcBlock() : base() { }
		internal IfcBlock(DatabaseIfc db, IfcBlock b) : base(db,b) { mXLength = b.mXLength; mYLength = b.mYLength; mZLength = b.mZLength; }
		public IfcBlock(IfcAxis2Placement3D position, double x,double y, double z) : base(position) { mXLength = x; mYLength = y; mZLength = z; }
		internal static void parseFields(IfcBlock b, List<string> arrFields, ref int ipos) { IfcCsgPrimitive3D.parseFields(b, arrFields, ref ipos); b.mXLength = ParserSTEP.ParseDouble(arrFields[ipos++]); b.mYLength = ParserSTEP.ParseDouble(arrFields[ipos++]); b.mZLength = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcBlock Parse(string strDef) { IfcBlock b = new IfcBlock(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mXLength) + "," + ParserSTEP.DoubleToString(mYLength) + "," + ParserSTEP.DoubleToString(mZLength); }
	}
	public partial class IfcBoiler : IfcEnergyConversionDevice //IFC4  
	{
		internal IfcBoilerTypeEnum mPredefinedType = IfcBoilerTypeEnum.NOTDEFINED;
		public IfcBoilerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBoiler() : base() { }
		internal IfcBoiler(DatabaseIfc db, IfcBoiler b) : base(db,b) { mPredefinedType = b.mPredefinedType; }
		internal IfcBoiler(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcBoiler a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcBoilerTypeEnum)Enum.Parse(typeof(IfcBoilerTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcBoiler Parse(string strDef) { IfcBoiler d = new IfcBoiler(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcBoilerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcBoilerType : IfcEnergyConversionDeviceType
	{
		internal IfcBoilerTypeEnum mPredefinedType = IfcBoilerTypeEnum.NOTDEFINED;// : IfcBoilerypeEnum; 
		public IfcBoilerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcBoilerType() : base() { }
		internal IfcBoilerType(DatabaseIfc db, IfcBoilerType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcBoilerType(DatabaseIfc m, string name, IfcBoilerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcBoilerType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcBoilerTypeEnum)Enum.Parse(typeof(IfcBoilerTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcBoilerType Parse(string strDef) { IfcBoilerType t = new IfcBoilerType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcBooleanClippingResult : IfcBooleanResult
	{
		internal IfcBooleanClippingResult() : base() { }
		internal IfcBooleanClippingResult(DatabaseIfc db, IfcBooleanClippingResult c) : base(db,c) { }
		internal IfcBooleanClippingResult(IfcBooleanClippingResult bc, IfcHalfSpaceSolid hss) : base(IfcBooleanOperator.DIFFERENCE, bc, hss) { }
		internal IfcBooleanClippingResult(IfcSweptAreaSolid s, IfcHalfSpaceSolid hss) : base(IfcBooleanOperator.DIFFERENCE, s, hss) { }
		internal static void parseFields(IfcBooleanClippingResult c, List<string> arrFields, ref int ipos) { IfcBooleanResult.parseFields(c, arrFields, ref ipos); }
		internal new static IfcBooleanClippingResult Parse(string strDef) { IfcBooleanClippingResult c = new IfcBooleanClippingResult(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
	}
	public partial interface IfcBooleanOperand : IBaseClassIfc { } //  SELECT (IfcSolidModel ,IfcHalfSpaceSolid ,IfcBooleanResult ,IfcCsgPrimitive3D);
	public partial class IfcBooleanResult : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect
	{
		private IfcBooleanOperator mOperator;// : IfcBooleanOperator;
		private int mFirstOperand;// : IfcBooleanOperand;
		private int mSecondOperand;// : IfcBooleanOperand;

		internal IfcBooleanOperator Operator { get { return mOperator; } }
		internal IfcBooleanOperand FirstOperand { get { return mDatabase[mFirstOperand] as IfcBooleanOperand; } set { mFirstOperand = value.Index; } }
		internal IfcBooleanOperand SecondOperand { get { return mDatabase[mSecondOperand] as IfcBooleanOperand; } set { mSecondOperand = value.Index; } }

		internal IfcBooleanResult() : base() { }
		internal IfcBooleanResult(DatabaseIfc db, IfcBooleanResult b) : base(db,b) { mOperator = b.mOperator; FirstOperand = db.Duplicate(b.mDatabase[ b.mFirstOperand]) as IfcBooleanOperand; SecondOperand = db.Duplicate(b.mDatabase[b.mSecondOperand]) as IfcBooleanOperand; }
		public IfcBooleanResult(IfcBooleanOperator op, IfcBooleanOperand first, IfcBooleanOperand second) : base(first.Database)
		{
			mOperator = op;
			mFirstOperand = first.Index;
			mSecondOperand = second.Index;
		}
		internal static void parseFields(IfcBooleanResult b, List<string> arrFields, ref int ipos)
		{
			IfcCurve.parseFields(b, arrFields, ref ipos);
			b.mOperator = (IfcBooleanOperator)Enum.Parse(typeof(IfcBooleanOperator), arrFields[ipos++].Replace(".", ""));
			b.mFirstOperand = ParserSTEP.ParseLink(arrFields[ipos++]);
			b.mSecondOperand = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		internal static IfcBooleanResult Parse(string strDef) { IfcBooleanResult b = new IfcBooleanResult(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mOperator.ToString() + ".," + ParserSTEP.LinkToString(mFirstOperand) + "," + ParserSTEP.LinkToString(mSecondOperand); }
	}
	public abstract partial class IfcBoundaryCondition : BaseClassIfc //ABSTRACT SUPERTYPE OF (ONEOF (IfcBoundaryEdgeCondition ,IfcBoundaryFaceCondition ,IfcBoundaryNodeCondition));
	{
		internal string mName;//  : OPTIONAL IfcLabel;
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } } 
		protected IfcBoundaryCondition() : base() { }
		protected IfcBoundaryCondition(IfcBoundaryCondition i) : base() { mName = i.mName; }
		protected IfcBoundaryCondition(DatabaseIfc m, string name) : base(m) { mName = (string.IsNullOrEmpty(name) ? "Boundary Condition" : name.Replace("'", "")); }
		protected static void parseFields(IfcBoundaryCondition c, List<string> arrFields, ref int ipos) { c.mName = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mName + "'"; }
	}
	public partial class IfcBoundaryCurve : IfcCompositeCurveOnSurface
	{
		internal IfcBoundaryCurve() : base() { }
		internal IfcBoundaryCurve(DatabaseIfc db, IfcBoundaryCurve c) : base(db,c) { }
		internal IfcBoundaryCurve(List<IfcCompositeCurveSegment> segs, IfcSurface surface) : base(segs,surface) { }
		internal new static IfcBoundaryCurve Parse(string strDef) { IfcBoundaryCurve b = new IfcBoundaryCurve(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcBoundaryCurve b, List<string> arrFields, ref int ipos) { IfcCompositeCurveOnSurface.parseFields(b, arrFields, ref ipos); }
	}
	public partial class IfcBoundaryEdgeCondition : IfcBoundaryCondition
	{
		internal double mLinearStiffnessByLengthX, mLinearStiffnessByLengthY, mLinearStiffnessByLengthZ;// : OPTIONAL IfcModulusOfLinearSubgradeReactionMeasure;
		internal double mRotationalStiffnessByLengthX, mRotationalStiffnessByLengthY, mRotationalStiffnessByLengthZ;// : OPTIONAL IfcModulusOfRotationalSubgradeReactionMeasure; 
		internal IfcBoundaryEdgeCondition() : base() { }
		internal IfcBoundaryEdgeCondition(IfcBoundaryEdgeCondition i) : base(i) { mLinearStiffnessByLengthX = i.mLinearStiffnessByLengthX; mLinearStiffnessByLengthY = i.mLinearStiffnessByLengthY; mLinearStiffnessByLengthZ = i.mLinearStiffnessByLengthZ; mRotationalStiffnessByLengthX = i.mRotationalStiffnessByLengthX; mRotationalStiffnessByLengthY = i.mRotationalStiffnessByLengthY; mRotationalStiffnessByLengthZ = i.mRotationalStiffnessByLengthZ; }
		internal IfcBoundaryEdgeCondition(DatabaseIfc m, string name) : base(m, name) {  }
		internal static IfcBoundaryEdgeCondition Parse(string strDef) { IfcBoundaryEdgeCondition b = new IfcBoundaryEdgeCondition(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcBoundaryEdgeCondition b, List<string> arrFields, ref int ipos)
		{
			IfcBoundaryCondition.parseFields(b, arrFields, ref ipos);
			b.mLinearStiffnessByLengthX = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mLinearStiffnessByLengthY = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mLinearStiffnessByLengthZ = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mRotationalStiffnessByLengthX = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mRotationalStiffnessByLengthY = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mRotationalStiffnessByLengthZ = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mLinearStiffnessByLengthX) + "," + ParserSTEP.DoubleOptionalToString(mLinearStiffnessByLengthY) + "," + ParserSTEP.DoubleOptionalToString(mLinearStiffnessByLengthZ) + "," + ParserSTEP.DoubleOptionalToString(mRotationalStiffnessByLengthX) + "," + ParserSTEP.DoubleOptionalToString(mRotationalStiffnessByLengthY) + "," + ParserSTEP.DoubleOptionalToString(mRotationalStiffnessByLengthZ); }
	}
	public partial class IfcBoundaryFaceCondition : IfcBoundaryCondition
	{
		internal double mLinearStiffnessByAreaX, mLinearStiffnessByAreaY, mLinearStiffnessByAreaZ;// : OPTIONAL IfcModulusOfSubgradeReactionMeasure 
		internal IfcBoundaryFaceCondition() : base() { }
		internal IfcBoundaryFaceCondition(IfcBoundaryFaceCondition i) : base(i) { mLinearStiffnessByAreaX = i.mLinearStiffnessByAreaX; mLinearStiffnessByAreaY = i.mLinearStiffnessByAreaY; mLinearStiffnessByAreaZ = i.mLinearStiffnessByAreaZ; }
		internal IfcBoundaryFaceCondition(DatabaseIfc m, string name) : base(m, name) { }
		internal static IfcBoundaryFaceCondition Parse(string strDef) { IfcBoundaryFaceCondition b = new IfcBoundaryFaceCondition(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcBoundaryFaceCondition b, List<string> arrFields, ref int ipos) { IfcBoundaryCondition.parseFields(b, arrFields, ref ipos); b.mLinearStiffnessByAreaX = ParserSTEP.ParseDouble(arrFields[ipos++]); b.mLinearStiffnessByAreaY = ParserSTEP.ParseDouble(arrFields[ipos++]); b.mLinearStiffnessByAreaZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mLinearStiffnessByAreaX) + "," + ParserSTEP.DoubleOptionalToString(mLinearStiffnessByAreaY) + "," + ParserSTEP.DoubleOptionalToString(mLinearStiffnessByAreaZ); }
	}
	public partial class IfcBoundaryNodeCondition : IfcBoundaryCondition 
	{
		internal IfcTranslationalStiffnessSelect mTranslationalStiffnessX, mTranslationalStiffnessY, mTranslationalStiffnessZ;// : OPTIONAL IfcTranslationalStiffnessSelect;
		internal IfcRotationalStiffnessSelect mRotationalStiffnessX, mRotationalStiffnessY, mRotationalStiffnessZ;// : OPTIONAL IfcRotationalStiffnessSelect; 

		public IfcTranslationalStiffnessSelect TranslationalStiffnessX { get { return mTranslationalStiffnessX; } set { mTranslationalStiffnessX = value; } }
		public IfcTranslationalStiffnessSelect TranslationalStiffnessY { get { return mTranslationalStiffnessY; } set { mTranslationalStiffnessY = value; } }
		public IfcTranslationalStiffnessSelect TranslationalStiffnessZ { get { return mTranslationalStiffnessZ; } set { mTranslationalStiffnessZ = value; } }
		public IfcRotationalStiffnessSelect RotationalStiffnessX { get { return mRotationalStiffnessX; } set { mRotationalStiffnessX = value; } }
		public IfcRotationalStiffnessSelect RotationalStiffnessY { get { return mRotationalStiffnessY; } set { mRotationalStiffnessY = value; } }
		public IfcRotationalStiffnessSelect RotationalStiffnessZ { get { return mRotationalStiffnessZ; } set { mRotationalStiffnessZ = value; } }

		internal IfcBoundaryNodeCondition() : base() { }
		internal IfcBoundaryNodeCondition(IfcBoundaryNodeCondition i) : base(i) { mTranslationalStiffnessX = i.mTranslationalStiffnessX; mTranslationalStiffnessY = i.mTranslationalStiffnessY; mTranslationalStiffnessZ = i.mTranslationalStiffnessZ; mRotationalStiffnessX = i.mRotationalStiffnessX; mRotationalStiffnessY = i.mRotationalStiffnessY; mRotationalStiffnessZ = i.mRotationalStiffnessZ; }
		public IfcBoundaryNodeCondition(DatabaseIfc m, string name, bool restrainX, bool restrainY, bool restrainZ, bool restrainXX, bool restrainYY, bool restrainZZ)
			: this(m, name, new IfcTranslationalStiffnessSelect(restrainX), new IfcTranslationalStiffnessSelect(restrainY), new IfcTranslationalStiffnessSelect(restrainZ), new IfcRotationalStiffnessSelect(restrainXX), new IfcRotationalStiffnessSelect(restrainYY), new IfcRotationalStiffnessSelect(restrainZZ)) { }
		public IfcBoundaryNodeCondition(DatabaseIfc m, string name, IfcTranslationalStiffnessSelect x, IfcTranslationalStiffnessSelect y, IfcTranslationalStiffnessSelect z, IfcRotationalStiffnessSelect xx, IfcRotationalStiffnessSelect yy, IfcRotationalStiffnessSelect zz) : base(m, name)
		{
			if (m.mRelease == ReleaseVersion.IFC2x3)
			{
				if (x != null && (x.mRigid || x.mStiffness == null))
					mTranslationalStiffnessX = new IfcTranslationalStiffnessSelect(x.mRigid ? -1 : 0);
				if (y != null && (y.mRigid || y.mStiffness == null))
					mTranslationalStiffnessY = new IfcTranslationalStiffnessSelect(y.mRigid ? -1 : 0);
				if (z != null && (z.mRigid || z.mStiffness == null))
					mTranslationalStiffnessZ = new IfcTranslationalStiffnessSelect(z.mRigid ? -1 : 0);
				if (xx != null && (xx.mRigid || xx.mStiffness == null))
					mRotationalStiffnessX = new IfcRotationalStiffnessSelect(xx.mRigid ? -1 : 0);
				if (yy != null && (yy.mRigid || yy.mStiffness == null))
					mRotationalStiffnessY = new IfcRotationalStiffnessSelect(yy.mRigid ? -1 : 0);
				if (zz != null && (zz.mRigid || zz.mStiffness == null))
					mRotationalStiffnessZ = new IfcRotationalStiffnessSelect(zz.mRigid ? -1 : 0);
			}
			else
			{
				mTranslationalStiffnessX = x;
				mTranslationalStiffnessY = y;
				mTranslationalStiffnessZ = z;
				mRotationalStiffnessX = xx;
				mRotationalStiffnessY = yy;
				mRotationalStiffnessZ = zz;
			}
		}
		internal static IfcBoundaryNodeCondition Parse(string strDef,ReleaseVersion schema) { IfcBoundaryNodeCondition b = new IfcBoundaryNodeCondition(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return b; }
		internal static void parseFields(IfcBoundaryNodeCondition b, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcBoundaryCondition.parseFields(b, arrFields, ref ipos);
			b.mTranslationalStiffnessX = IfcTranslationalStiffnessSelect.Parse(arrFields[ipos++], schema);
			b.mTranslationalStiffnessY = IfcTranslationalStiffnessSelect.Parse(arrFields[ipos++], schema);
			b.mTranslationalStiffnessZ = IfcTranslationalStiffnessSelect.Parse(arrFields[ipos++], schema);
			b.mRotationalStiffnessX = IfcRotationalStiffnessSelect.Parse(arrFields[ipos++], schema);
			b.mRotationalStiffnessY = IfcRotationalStiffnessSelect.Parse(arrFields[ipos++], schema);
			b.mRotationalStiffnessZ = IfcRotationalStiffnessSelect.Parse(arrFields[ipos++], schema);
		}
		protected override string BuildStringSTEP()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
				return base.BuildStringSTEP() + "," + (mTranslationalStiffnessX == null ? "$" : ParserSTEP.DoubleToString(mTranslationalStiffnessX.mStiffness.mValue)) + "," +
					(mTranslationalStiffnessY == null ? "$" : ParserSTEP.DoubleToString(mTranslationalStiffnessY.mStiffness.mValue)) + "," +
					(mTranslationalStiffnessZ == null ? "$" : ParserSTEP.DoubleToString(mTranslationalStiffnessZ.mStiffness.mValue)) + "," +
					(mRotationalStiffnessX == null ? "$" : ParserSTEP.DoubleToString(mRotationalStiffnessX.mStiffness.mValue)) + "," +
					(mRotationalStiffnessY == null ? "$" : ParserSTEP.DoubleToString(mRotationalStiffnessY.mStiffness.mValue)) + "," +
					(mRotationalStiffnessZ == null ? "$" : ParserSTEP.DoubleToString(mRotationalStiffnessZ.mStiffness.mValue));
			return base.BuildStringSTEP() + "," + (mTranslationalStiffnessX == null ? "$" : mTranslationalStiffnessX.ToString()) + "," +
				(mTranslationalStiffnessY == null ? "$" : mTranslationalStiffnessY.ToString()) + "," +
				(mTranslationalStiffnessZ == null ? "$" : mTranslationalStiffnessZ.ToString()) + "," +
				(mRotationalStiffnessX == null ? "$" : mRotationalStiffnessX.ToString()) + "," +
				(mRotationalStiffnessY == null ? "$" : mRotationalStiffnessY.ToString()) + "," +
				(mRotationalStiffnessZ == null ? "$" : mRotationalStiffnessZ.ToString());
		}
	}	
	public partial class IfcBoundaryNodeConditionWarping : IfcBoundaryNodeCondition
	{
		internal IfcWarpingStiffnessSelect mWarpingStiffness;// : OPTIONAL IfcWarpingStiffnessSelect; 
		internal IfcBoundaryNodeConditionWarping() : base() { }
		internal IfcBoundaryNodeConditionWarping(IfcBoundaryNodeConditionWarping i) : base(i) { mWarpingStiffness = i.mWarpingStiffness; }
		internal IfcBoundaryNodeConditionWarping(DatabaseIfc m, string name, IfcTranslationalStiffnessSelect x, IfcTranslationalStiffnessSelect y, IfcTranslationalStiffnessSelect z, IfcRotationalStiffnessSelect xx, IfcRotationalStiffnessSelect yy, IfcRotationalStiffnessSelect zz, IfcWarpingStiffnessSelect w)
			: base(m, name, x, y, z, xx, yy, zz) { mWarpingStiffness = w; }
		internal new static IfcBoundaryNodeConditionWarping Parse(string strDef,ReleaseVersion schema) { IfcBoundaryNodeConditionWarping b = new IfcBoundaryNodeConditionWarping(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return b; }
		internal static void parseFields(IfcBoundaryNodeConditionWarping b, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcBoundaryNodeCondition.parseFields(b, arrFields, ref ipos,schema); b.mWarpingStiffness = IfcWarpingStiffnessSelect.Parse(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mWarpingStiffness.ToString(); }
	}
	public abstract partial class IfcBoundedCurve : IfcCurve, IfcCurveOrEdgeCurve  //ABSTRACT SUPERTYPE OF (ONEOF (IfcBSplineCurve ,IfcCompositeCurve ,IfcPolyline ,IfcTrimmedCurve)) IFC4 IfcIndexedPolyCurve IFC4x1 IfcCurveSegment2D
	{
		protected IfcBoundedCurve() : base() { }
		protected IfcBoundedCurve(DatabaseIfc db) : base(db) { }
		protected IfcBoundedCurve(DatabaseIfc db, IfcBoundedCurve c) : base(db,c) { }
		protected static void parseFields(IfcBoundedCurve c, List<string> arrFields, ref int ipos) { IfcCurve.parseFields(c, arrFields, ref ipos); }
	}
	public abstract partial class IfcBoundedSurface : IfcSurface //	ABSTRACT SUPERTYPE OF (ONEOF(IfcCurveBoundedPlane,IfcRectangularTrimmedSurface))
	{
		protected IfcBoundedSurface() : base() { }
		protected IfcBoundedSurface(DatabaseIfc db) : base(db) { }
		protected IfcBoundedSurface(DatabaseIfc db, IfcBoundedSurface s) : base(db,s) { }
		protected static void parseFields(IfcBoundedSurface s, List<string> arrFields, ref int ipos) { IfcSurface.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcBoundingBox : IfcGeometricRepresentationItem
	{
		private int mCorner;// : IfcCartesianPoint;
		private double mXDim, mYDim, mZDim;// : IfcPositiveLengthMeasure

		public IfcCartesianPoint Corner { get { return mDatabase[mCorner] as IfcCartesianPoint; } set { mCorner = value.mIndex; } }
		internal double XDim { get { return mXDim; } }
		internal double YDim { get { return mYDim; } }
		internal double ZDim { get { return mZDim; } }

		internal IfcBoundingBox() : base() { }
		internal IfcBoundingBox(DatabaseIfc db, IfcBoundingBox b) : base(db,b) { Corner = db.Duplicate(b.Corner) as IfcCartesianPoint; mXDim = b.mXDim; mYDim = b.mYDim; mZDim = b.mZDim; }
		internal IfcBoundingBox(IfcCartesianPoint pt, double xdim, double ydim, double zdim) : base(pt.mDatabase)
		{
			//if (mModel.mModelView != ModelView.NotAssigned && mModel.mModelView != ModelView.IFC2x3Coordination)
			//	throw new Exception("Invalid Model View for IfcBoundingBox : " + m.ModelView.ToString());
			mCorner = pt.mIndex;
			mXDim = xdim;
			mYDim = ydim;
			mZDim = zdim;
		}
		internal static IfcBoundingBox Parse(string strDef) { IfcBoundingBox b = new IfcBoundingBox(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcBoundingBox b, List<string> arrFields, ref int ipos)
		{
			IfcGeometricRepresentationItem.parseFields(b, arrFields, ref ipos);
			b.mCorner = ParserSTEP.ParseLink(arrFields[ipos++]);
			b.mXDim = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mYDim = ParserSTEP.ParseDouble(arrFields[ipos++]);
			b.mZDim = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mCorner) + "," + ParserSTEP.DoubleToString(mXDim) + "," + ParserSTEP.DoubleToString(mYDim) + "," + ParserSTEP.DoubleToString(mZDim); }
	}
	public partial class IfcBoxedHalfSpace : IfcHalfSpaceSolid
	{
		private int mEnclosure;// : IfcBoundingBox; 
		internal IfcBoundingBox Enclosure { get { return mDatabase[mEnclosure] as IfcBoundingBox; } set { mEnclosure = value.mIndex; } }

		internal IfcBoxedHalfSpace() : base() { }
		internal IfcBoxedHalfSpace(DatabaseIfc db, IfcBoxedHalfSpace s) : base(db,s) { Enclosure = db.Duplicate(s.Enclosure) as IfcBoundingBox; }
		internal static void parseFields(IfcBoxedHalfSpace s, List<string> arrFields, ref int ipos) { IfcHalfSpaceSolid.parseFields(s, arrFields, ref ipos); s.mEnclosure = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal new static IfcBoxedHalfSpace Parse(string strDef) { IfcBoxedHalfSpace s = new IfcBoxedHalfSpace(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mEnclosure); }
	}
	public abstract partial class IfcBSplineCurve : IfcBoundedCurve //SUPERTYPE OF(IfcBSplineCurveWithKnots)
	{
		private int mDegree;// : INTEGER;
		private List<int> mControlPointsList = new List<int>();// : LIST [2:?] OF IfcCartesianPoint;
		private IfcBSplineCurveForm mCurveForm;// : IfcBSplineCurveForm;
		private IfcLogicalEnum mClosedCurve = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		private IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// : LOGICAL; 

		public int Degree { get { return mDegree; } }
		public List<IfcCartesianPoint> ControlPointsList { get { return mControlPointsList.ConvertAll(x =>mDatabase[x] as IfcCartesianPoint); } set { mControlPointsList = value.ConvertAll(x => x.mIndex); } }
		public IfcBSplineCurveForm CurveForm { get { return mCurveForm; } }
		public IfcLogicalEnum ClosedCurve { get { return mClosedCurve; } set { mClosedCurve = value; } }
		public IfcLogicalEnum SelfIntersect { get { return mSelfIntersect; } set { mSelfIntersect = value; } }

		protected IfcBSplineCurve() : base() { }
		protected IfcBSplineCurve(DatabaseIfc db, IfcBSplineCurve c) : base(db, c)
		{
			mDegree = c.mDegree;
			ControlPointsList = c.ControlPointsList.ConvertAll(x => db.Duplicate(x) as IfcCartesianPoint);
			mCurveForm = c.mCurveForm;
			mClosedCurve = c.mClosedCurve;
			mSelfIntersect = c.mSelfIntersect;
		}
		private IfcBSplineCurve(DatabaseIfc db, int degree, IfcBSplineCurveForm form) : base(db) { mDegree = degree; mCurveForm = form; }
		protected IfcBSplineCurve(DatabaseIfc db, int degree, List<IfcCartesianPoint> controlPoints, IfcBSplineCurveForm form)
			: this(db, degree, form) { mControlPointsList = controlPoints.ConvertAll(x => x.mIndex); }

		protected static void parseFields(IfcBSplineCurve c, List<string> arrFields, ref int ipos)
		{
			IfcBoundedCurve.parseFields(c, arrFields, ref ipos);
			c.mDegree = int.Parse(arrFields[ipos++]);
			c.mControlPointsList = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			c.mCurveForm = (IfcBSplineCurveForm)Enum.Parse(typeof(IfcBSplineCurveForm), arrFields[ipos++].Replace(".", ""));
			c.mClosedCurve = ParserIfc.ParseIFCLogical(arrFields[ipos++]);
			c.mSelfIntersect = ParserIfc.ParseIFCLogical(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + mDegree + ",(";
			str += ParserSTEP.LinkToString(mControlPointsList[0]);
			for (int icounter = 1; icounter < mControlPointsList.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mControlPointsList[icounter]);
			return str + "),." + mCurveForm.ToString() + ".," + ParserIfc.LogicalToString(mClosedCurve) + "," +
				ParserIfc.LogicalToString(mSelfIntersect);
		}
	}
	public partial class IfcBSplineCurveWithKnots : IfcBSplineCurve
	{
		private List<int> mMultiplicities = new List<int>();// : LIST [2:?] OF INTEGER;
		private List<double> mKnots = new List<double>();// : LIST [2:?] OF IfcParameterValue;
		private IfcKnotType mKnotSpec = IfcKnotType.UNSPECIFIED;//: IfcKnotType;

		internal List<int> Multiplicities { get { return mMultiplicities; } }
		internal List<double> Knots { get { return mKnots; } }
		internal IfcKnotType KnotSpec { get { return mKnotSpec; } }

		internal IfcBSplineCurveWithKnots() : base() { }
		internal IfcBSplineCurveWithKnots(DatabaseIfc db, IfcBSplineCurveWithKnots c) : base(db, c)
		{
			mMultiplicities.AddRange(c.Multiplicities);
			mKnots.AddRange(c.Knots);
			mKnotSpec = c.mKnotSpec;
		}
		internal static void parseFields(IfcBSplineCurveWithKnots c, List<string> arrFields, ref int ipos)
		{
			IfcBSplineCurve.parseFields(c, arrFields, ref ipos);
			string s = arrFields[ipos++];
			List<string> arrLists = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			for (int icounter = 0; icounter < arrLists.Count; icounter++)
				c.mMultiplicities.Add(ParserSTEP.ParseInt(arrLists[icounter]));
			s = arrFields[ipos++];
			arrLists = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			for (int icounter = 0; icounter < arrLists.Count; icounter++)
				c.mKnots.Add(ParserSTEP.ParseDouble(arrLists[icounter]));
			c.mKnotSpec = (IfcKnotType)Enum.Parse(typeof(IfcKnotType), arrFields[ipos++].Replace(".", ""));
		}
		internal static IfcBSplineCurveWithKnots Parse(string strDef) { IfcBSplineCurveWithKnots c = new IfcBSplineCurveWithKnots(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.IntToString(mMultiplicities[0]);
			for (int jcounter = 1; jcounter < mMultiplicities.Count; jcounter++)
				str += "," + ParserSTEP.IntToString(mMultiplicities[jcounter]);
			str += "),(" + ParserSTEP.DoubleToString(mKnots[0]);
			for (int jcounter = 1; jcounter < mKnots.Count; jcounter++)
				str += "," + ParserSTEP.DoubleToString(mKnots[jcounter]);
			return str + "),." + mKnotSpec.ToString() + ".";
		}
	}
	public abstract partial class IfcBSplineSurface : IfcBoundedSurface //ABSTRACT SUPERTYPE OF	(IfcBSplineSurfaceWithKnots)
	{
		private int mUDegree;// : INTEGER;
		private int mVDegree;// : INTEGER;
		private List<List<int>> mControlPointsList = new List<List<int>>();// : LIST [2:?] OF LIST [2:?] OF IfcCartesianPoint;
		private IfcBSplineSurfaceForm mSurfaceForm = IfcBSplineSurfaceForm.UNSPECIFIED;// : IfcBSplineSurfaceForm;
		private IfcLogicalEnum mUClosed = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		private IfcLogicalEnum mVClosed = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		private IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// : LOGICAL; 

		public int UDegree { get { return mUDegree; } }
		public int VDegree { get { return mVDegree; } }
		public List<List<IfcCartesianPoint>> ControlPointsList { get { return mControlPointsList.ConvertAll(x => x.ConvertAll(y =>mDatabase[y] as IfcCartesianPoint)); } }
		public IfcBSplineSurfaceForm SurfaceForm { get { return mSurfaceForm; } }
		public IfcLogicalEnum UClosed { get { return mUClosed; } set { mUClosed = value; } }
		public IfcLogicalEnum VClosed { get { return mVClosed; } set { mVClosed = value; } }
		public IfcLogicalEnum SelfIntersect { get { return mSelfIntersect; } set { mSelfIntersect = value; } }

		protected IfcBSplineSurface() : base() { }
		protected IfcBSplineSurface(DatabaseIfc db, IfcBSplineSurface s) : base(db,s)
		{
			mUDegree = s.mUDegree;
			mVDegree = s.mVDegree;
			List<List<IfcCartesianPoint>> points = ControlPointsList;
			foreach(List<IfcCartesianPoint> ps in points)
				mControlPointsList.Add(ps.ConvertAll(x=>db.Duplicate(x).mIndex));
			mSurfaceForm = s.mSurfaceForm;
			mUClosed = s.mUClosed;
			mVClosed = s.mVClosed;
			mSelfIntersect = s.mSelfIntersect;
		}
		private IfcBSplineSurface(DatabaseIfc db, int uDegree, int vDegree, IfcBSplineSurfaceForm form) : base(db)
		{
			mUDegree = uDegree;
			mVDegree = vDegree;
			mSurfaceForm = form;
		}
		protected IfcBSplineSurface(DatabaseIfc db, int uDegree, int vDegree, List<List<IfcCartesianPoint>> controlPoints, IfcBSplineSurfaceForm form) :
			this(db, uDegree, vDegree, form)
		{
			foreach (List<IfcCartesianPoint> cps in controlPoints)
				mControlPointsList.Add(cps.ConvertAll(x => x.mIndex));
		}

		protected static void parseFields(IfcBSplineSurface bs, List<string> arrFields, ref int ipos)
		{
			IfcBoundedSurface.parseFields(bs, arrFields, ref ipos);
			bs.mUDegree = int.Parse(arrFields[ipos++]);
			bs.mVDegree = int.Parse(arrFields[ipos++]);
			string s = arrFields[ipos++];
			List<string> arrLists = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			int ilast = arrLists.Count;
			for (int icounter = 0; icounter < ilast; icounter++)
			{
				List<int> points = new List<int>();
				s = arrLists[icounter];
				List<string> arrPoints = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int jcounter = 0; jcounter < arrPoints.Count; jcounter++)
					points.Add(ParserSTEP.ParseLink(arrPoints[jcounter]));
				bs.mControlPointsList.Add(points);
			}
			bs.mSurfaceForm = (IfcBSplineSurfaceForm)Enum.Parse(typeof(IfcBSplineSurfaceForm), arrFields[ipos++].Replace(".", ""));
			bs.mUClosed = ParserIfc.ParseIFCLogical(arrFields[ipos++]);
			bs.mVClosed = ParserIfc.ParseIFCLogical(arrFields[ipos++]);
			bs.mSelfIntersect = ParserIfc.ParseIFCLogical(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			List<int> cps = mControlPointsList[0];
			string str = base.BuildStringSTEP() + "," + mUDegree + "," + mVDegree + ",((" +
				ParserSTEP.LinkToString(cps[0]);
			for (int jcounter = 1; jcounter < cps.Count; jcounter++)
				str += "," + ParserSTEP.LinkToString(cps[jcounter]);
			str += ")";
			for (int icounter = 1; icounter < mControlPointsList.Count; icounter++)
			{
				cps = mControlPointsList[icounter];
				str += ",(" + ParserSTEP.LinkToString(cps[0]);
				for (int jcounter = 1; jcounter < cps.Count; jcounter++)
					str += "," + ParserSTEP.LinkToString(cps[jcounter]);
				str += ")";
			}
			return str + "),." + mSurfaceForm.ToString() + ".," + ParserIfc.LogicalToString(mUClosed) + ","
				+ ParserIfc.LogicalToString(mVClosed) + "," + ParserIfc.LogicalToString(mSelfIntersect);
		}
	}
	public partial class IfcBSplineSurfaceWithKnots : IfcBSplineSurface
	{
		internal List<int> mUMultiplicities = new List<int>();// : LIST [2:?] OF INTEGER;
		internal List<int> mVMultiplicities = new List<int>();// : LIST [2:?] OF INTEGER;
		internal List<double> mUKnots = new List<double>();// : LIST [2:?] OF IfcParameterValue;
		internal List<double> mVKnots = new List<double>();// : LIST [2:?] OF IfcParameterValue;
		internal IfcKnotType mKnotSpec = IfcKnotType.UNSPECIFIED;//: IfcKnotType; 

		internal IfcBSplineSurfaceWithKnots() : base() { }
		internal IfcBSplineSurfaceWithKnots(DatabaseIfc db, IfcBSplineSurfaceWithKnots s) : base(db,s)
		{
			mUMultiplicities = new List<int>(s.mUMultiplicities.ToArray());
			mVMultiplicities = new List<int>(s.mVMultiplicities.ToArray());
			mUKnots = new List<double>(s.mUKnots.ToArray());
			mVKnots = new List<double>(s.mVKnots.ToArray());
			mKnotSpec = s.mKnotSpec;
		}
		internal IfcBSplineSurfaceWithKnots(DatabaseIfc m, int uDegree, int vDegree, IfcBSplineSurfaceForm form, List<List<IfcCartesianPoint>> controlPoints, IfcLogicalEnum uClosed, IfcLogicalEnum vClosed, IfcLogicalEnum selfIntersect, List<int> uMultiplicities, List<int> vMultiplicities, List<double> uKnots, List<double> vKnots, IfcKnotType type)
			: base(m, uDegree, vDegree, controlPoints, form)
		{
			mUMultiplicities.AddRange(uMultiplicities);
			mVMultiplicities.AddRange(vMultiplicities);
			mUKnots.AddRange(uKnots);
			mVKnots.AddRange(vKnots);
		}

		internal static void parseFields(IfcBSplineSurfaceWithKnots ss, List<string> arrFields, ref int ipos)
		{
			IfcBSplineSurface.parseFields(ss, arrFields, ref ipos);
			string s = arrFields[ipos++];
			List<string> arrLists = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			for (int icounter = 0; icounter < arrLists.Count; icounter++)
				ss.mUMultiplicities.Add(ParserSTEP.ParseInt(arrLists[icounter]));
			s = arrFields[ipos++];
			arrLists = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			for (int icounter = 0; icounter < arrLists.Count; icounter++)
				ss.mVMultiplicities.Add(ParserSTEP.ParseInt(arrLists[icounter]));
			s = arrFields[ipos++];
			arrLists = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			for (int icounter = 0; icounter < arrLists.Count; icounter++)
				ss.mUKnots.Add(ParserSTEP.ParseDouble(arrLists[icounter]));
			s = arrFields[ipos++];
			arrLists = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			for (int icounter = 0; icounter < arrLists.Count; icounter++)
				ss.mVKnots.Add(ParserSTEP.ParseDouble(arrLists[icounter]));
			ss.mKnotSpec = (IfcKnotType)Enum.Parse(typeof(IfcKnotType), arrFields[ipos++].Replace(".", ""));
		}
		internal static IfcBSplineSurfaceWithKnots Parse(string strDef) { IfcBSplineSurfaceWithKnots s = new IfcBSplineSurfaceWithKnots(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.IntToString(mUMultiplicities[0]);
			for (int jcounter = 1; jcounter < mUMultiplicities.Count; jcounter++)
				str += "," + ParserSTEP.IntToString(mUMultiplicities[jcounter]);
			str += "),(" + ParserSTEP.IntToString(mVMultiplicities[0]);
			for (int jcounter = 1; jcounter < mVMultiplicities.Count; jcounter++)
				str += "," + ParserSTEP.IntToString(mVMultiplicities[jcounter]);
			str += "),(" + ParserSTEP.DoubleToString(mUKnots[0]);
			for (int jcounter = 1; jcounter < mUKnots.Count; jcounter++)
				str += "," + ParserSTEP.DoubleToString(mUKnots[jcounter]);
			str += "),(" + ParserSTEP.DoubleToString(mVKnots[0]);
			for (int jcounter = 1; jcounter < mVKnots.Count; jcounter++)
				str += "," + ParserSTEP.DoubleToString(mVKnots[jcounter]);
			return str + "),." + mKnotSpec.ToString() + ".";
		}
	}
	public partial class IfcBuilding : IfcSpatialStructureElement
	{
		internal double mElevationOfRefHeight = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal double mElevationOfTerrain = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal int mBuildingAddress;// : OPTIONAL IfcPostalAddress; 

		internal double ElevationOfRefHeight { get { return mElevationOfRefHeight; } set {mElevationOfRefHeight = value; } }
		internal double ElevationOfTerrain { get { return mElevationOfTerrain; } set {mElevationOfTerrain = value; } }
		internal IfcPostalAddress BuildingAddress {get {return mDatabase[mBuildingAddress] as IfcPostalAddress;} set { mBuildingAddress = (value == null ? 0 : value.mIndex); }}

		internal IfcBuilding() : base() { }
		public IfcBuilding(DatabaseIfc db, string name) : base(new IfcLocalPlacement(db.PlaneXYPlacement) ) { Name = name; setDefaultAddress();  }
		public IfcBuilding(IfcBuilding host, string name) : base(host, name) { setDefaultAddress(); }
		public IfcBuilding(IfcSite host, string name) : base(host, name) { setDefaultAddress(); }
		public IfcBuilding(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { setDefaultAddress(); }

		private void setDefaultAddress()  //Implementers Agreement requires address
		{
			BuildingAddress = new IfcPostalAddress(mDatabase) { AddressLines = new List<string>() { "Unknown" }, Town = "Unknown", Country = "Unknown", PostalCode = "Unknown" };
		}
		private void init(IfcSpatialElement container)
		{
			IfcRelAggregates ra = new IfcRelAggregates(mDatabase, "Building", "Building Storie", this);
			if (container != null) 
				container.addBuilding(this);
			//mBuildingAddress = new IfcPostalAddress(mModel, IfcAddressTypeEnum.NOTDEFINED).mIndex;
		}
		internal static IfcBuilding Parse(string strDef) { IfcBuilding b = new IfcBuilding(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcBuilding b, List<string> arrFields, ref int ipos) { IfcSpatialStructureElement.parseFields(b, arrFields, ref ipos); b.mElevationOfRefHeight = ParserSTEP.ParseDouble(arrFields[ipos++]); b.mElevationOfTerrain = ParserSTEP.ParseDouble(arrFields[ipos++]); b.mBuildingAddress = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mElevationOfRefHeight) + "," + ParserSTEP.DoubleOptionalToString(mElevationOfTerrain) + "," + ParserSTEP.LinkToString(mBuildingAddress); }

		internal new bool addStorey(IfcBuildingStorey s) { return base.addStorey(s); }
	}
	public abstract partial class IfcBuildingElement : IfcElement //ABSTRACT SUPERTYPE OF (ONEOF (IfcBeam,IfcBuildingElementProxy,IfcColumn,IfcCovering,IfcCurtainWall,IfcDoor,IfcFooting
	{ //,IfcMember,IfcPile,IfcPlate,IfcRailing,IfcRamp,IfcRampFlight,IfcRoof,IfcSlab,IfcStair,IfcStairFlight,IfcWall,IfcWindow) IFC2x3 IfcBuildingElementComponent IFC4  IfcShadingDevice
		protected IfcBuildingElement() : base() { }
		protected IfcBuildingElement(DatabaseIfc db) : base(db) { }
		protected IfcBuildingElement(DatabaseIfc db, IfcBuildingElement e) : base(db, e) { }
		protected IfcBuildingElement(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }

		protected static void parseFields(IfcBuildingElement e, List<string> arrFields, ref int ipos) { IfcElement.parseFields(e, arrFields, ref ipos); }
	}
	/*internal class IfcBuildingElementComponent : IfcBuildingElement //IFC4 DELETED
	{
		protected IfcBuildingElementComponent(IfcBuildingElementComponent b) : base(b) { }
		protected IfcBuildingElementComponent() : base() { }
		protected static void parseFields(IfcBuildingElementComponent c, List<string> arrFields, ref int ipos) { IfcBuildingElement.parseFields(c, arrFields, ref ipos); }
	}*/
	public partial class IfcBuildingElementPart : IfcElementComponent
	{
		internal IfcBuildingElementPartTypeEnum mPredefinedType = IfcBuildingElementPartTypeEnum.NOTDEFINED;//:	OPTIONAL IfcBuildingElementPartTypeEnum; IFC4 added
		public IfcBuildingElementPartTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBuildingElementPart() : base() { }
		internal IfcBuildingElementPart(DatabaseIfc db, IfcBuildingElementPart p) : base(db, p) { mPredefinedType = p.mPredefinedType; }
		internal IfcBuildingElementPart(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static IfcBuildingElementPart Parse(string strDef, ReleaseVersion schema) { IfcBuildingElementPart p = new IfcBuildingElementPart(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcBuildingElementPart a, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcElementComponent.parseFields(a, arrFields, ref ipos);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					a.mPredefinedType = (IfcBuildingElementPartTypeEnum)Enum.Parse(typeof(IfcBuildingElementPartTypeEnum), s.Replace(".", ""));
			}
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcBuildingElementPartTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcBuildingElementPartType : IfcElementComponentType
	{
		internal IfcBuildingElementPartTypeEnum mPredefinedType = IfcBuildingElementPartTypeEnum.NOTDEFINED;// : IfcBuildingElementPartTypeEnum;
		public IfcBuildingElementPartTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcBuildingElementPartType() : base() { }
		internal IfcBuildingElementPartType(DatabaseIfc db, IfcBuildingElementPartType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcBuildingElementPartType(DatabaseIfc m, string name, IfcBuildingElementPartTypeEnum type) : base(m) { Name = name; if (mDatabase.mRelease == ReleaseVersion.IFC2x3) throw new Exception("XXX Only valid in IFC4 or newer!"); mPredefinedType = type; }
		internal static void parseFields(IfcBuildingElementPartType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcBuildingElementPartTypeEnum)Enum.Parse(typeof(IfcBuildingElementPartTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcBuildingElementPartType Parse(string strDef) { IfcBuildingElementPartType t = new IfcBuildingElementPartType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcBuildingElementProxy : IfcBuildingElement
	{
		internal IfcBuildingElementProxyTypeEnum mPredefinedType = IfcBuildingElementProxyTypeEnum.NOTDEFINED; //	:	OPTIONAL IfcBuildingElementProxyTypeEnum;
		//Ifc2x3 internal IfcElementCompositionEnum mCompositionType = IfcElementCompositionEnum.NA;// : OPTIONAL IfcElementCompositionEnum; 

		public IfcBuildingElementProxyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public override string Name { set { base.Name = (string.IsNullOrEmpty(value) ? "NOTDEFINED" : value); } }

		internal IfcBuildingElementProxy() : base() { }
		internal IfcBuildingElementProxy(DatabaseIfc db, IfcBuildingElementProxy p) : base(db, p) { mPredefinedType = p.mPredefinedType; }
		public IfcBuildingElementProxy(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { Name = "NOTDEFINED"; }
		internal static void parseFields(IfcBuildingElementProxy p, List<string> arrFields, ref int ipos)
		{
			IfcBuildingElement.parseFields(p, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str.StartsWith("."))
			{
				try
				{
					p.mPredefinedType = (IfcBuildingElementProxyTypeEnum)Enum.Parse(typeof(IfcBuildingElementProxyTypeEnum), str.Replace(".", ""));
				}
				catch (Exception) { }
			}
		}
		protected override string BuildStringSTEP()
		{
			string str = (mPredefinedType == IfcBuildingElementProxyTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
			if (mPredefinedType != IfcBuildingElementProxyTypeEnum.NOTDEFINED && mDatabase.mRelease == ReleaseVersion.IFC2x3)
			{
				if (mPredefinedType != IfcBuildingElementProxyTypeEnum.COMPLEX && mPredefinedType != IfcBuildingElementProxyTypeEnum.ELEMENT && mPredefinedType != IfcBuildingElementProxyTypeEnum.PARTIAL)
					str = ",$";
			}
			return base.BuildStringSTEP() + str;
		}
		internal static IfcBuildingElementProxy Parse(string strDef) { IfcBuildingElementProxy p = new IfcBuildingElementProxy(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
	}
	public partial class IfcBuildingElementProxyType : IfcBuildingElementType
	{
		internal IfcBuildingElementProxyTypeEnum mPredefinedType = IfcBuildingElementProxyTypeEnum.NOTDEFINED;// : IfcBuildingElementProxyTypeEnum;
		public IfcBuildingElementProxyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBuildingElementProxyType() : base() { }
		internal IfcBuildingElementProxyType(DatabaseIfc db, IfcBuildingElementProxyType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcBuildingElementProxyType(DatabaseIfc m, string name, IfcBuildingElementProxyTypeEnum type) : base(m)
		{
			Name = name;
			mPredefinedType = type;
			if (m.mRelease == ReleaseVersion.IFC2x3)
			{
				if (type != IfcBuildingElementProxyTypeEnum.USERDEFINED && type != IfcBuildingElementProxyTypeEnum.NOTDEFINED)
				{
					if (ElementType == "$")
						ElementType = type.ToString();
					mPredefinedType = IfcBuildingElementProxyTypeEnum.USERDEFINED;
				}
			}
		}
		internal static void parseFields(IfcBuildingElementProxyType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); try { t.mPredefinedType = (IfcBuildingElementProxyTypeEnum)Enum.Parse(typeof(IfcBuildingElementProxyTypeEnum), arrFields[ipos++].Replace(".", "")); } catch (Exception) { } }
		internal new static IfcBuildingElementProxyType Parse(string strDef) { IfcBuildingElementProxyType t = new IfcBuildingElementProxyType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract partial class IfcBuildingElementType : IfcElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcBeamType, IfcBuildingElementProxyType, IfcChimneyType, IfcColumnType, 
	{	//IfcCoveringType, IfcCurtainWallType, IfcDoorType, IfcFootingType, IfcMemberType, IfcPileType, IfcPlateType, IfcRailingType, IfcRampFlightType, IfcRampType, 
		//IfcRoofType, IfcShadingDeviceType, IfcSlabType, IfcStairFlightType, IfcStairType, IfcWallType, IfcWindowType))
		protected IfcBuildingElementType() : base() { }
		protected IfcBuildingElementType(DatabaseIfc db) : base(db) { }
		protected IfcBuildingElementType(DatabaseIfc db, IfcBuildingElementType t) : base(db,t) { }
		protected static void parseFields(IfcBuildingElementType t, List<string> arrFields, ref int ipos) { IfcElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcBuildingStorey : IfcSpatialStructureElement
	{ 
		internal double mElevation = double.NaN;// : OPTIONAL IfcLengthMeasure; 
		public double Elevation
		{
			get { return mElevation; }
			set
			{
				mElevation = value;
#if(RHINO)
				try
				{
					int i = Rhino.RhinoDoc.ActiveDoc.NamedConstructionPlanes.Find(Name);
					if (i >= 0)
						Rhino.RhinoDoc.ActiveDoc.NamedConstructionPlanes.Delete(i);
					Rhino.RhinoDoc.ActiveDoc.NamedConstructionPlanes.Add(Name, new Rhino.Geometry.Plane(new Rhino.Geometry.Point3d(0, 0, mElevation), Rhino.Geometry.Vector3d.XAxis, Rhino.Geometry.Vector3d.YAxis));
				}
				catch (Exception) { }
#endif
			}
		}

		public IfcBuildingStorey() : base() { }
		
		public IfcBuildingStorey(IfcBuilding host, string name, double elev) : base(new IfcLocalPlacement(host.Placement, new IfcAxis2Placement3D(new IfcCartesianPoint(host.mDatabase, 0, 0, elev))))
		{
			host.addStorey(this);
			Name = name;
			Elevation = elev;
		}
		public IfcBuildingStorey(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
		internal static void parseFields(IfcBuildingStorey s, List<string> arrFields, ref int ipos) { IfcSpatialStructureElement.parseFields(s, arrFields, ref ipos); s.mElevation = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mElevation); }
		internal static IfcBuildingStorey Parse(string strDef) { IfcBuildingStorey s = new IfcBuildingStorey(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public partial class IfcBuildingSystem : IfcSystem //IFC4
	{
		internal IfcBuildingSystemTypeEnum mPredefinedType = IfcBuildingSystemTypeEnum.NOTDEFINED;// : OPTIONAL IfcBuildingSystemTypeEnum;
		internal string mLongName = "$"; // 	OPTIONAL IfcLabel IFC4ADD1 

		public IfcBuildingSystemTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public string LongName { get { return (mLongName == "$" ? "" : ParserIfc.Decode(mLongName)); } set { mLongName = (string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value.Replace("'", ""))); } }

		internal IfcBuildingSystem() : base() { }
		internal IfcBuildingSystem(DatabaseIfc db, IfcBuildingSystem s) : base(db,s) { mLongName = s.mLongName; mPredefinedType = s.mPredefinedType; }
		internal IfcBuildingSystem(IfcSpatialElement bldg, string name,  IfcBuildingSystemTypeEnum type) : base(bldg, name) { mPredefinedType = type; }
		internal new static IfcBuildingSystem Parse(string strDef) { IfcBuildingSystem m = new IfcBuildingSystem(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcBuildingSystem c, List<string> arrFields, ref int ipos)
		{
			IfcSystem.parseFields(c, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				c.mPredefinedType = (IfcBuildingSystemTypeEnum)Enum.Parse(typeof(IfcBuildingSystemTypeEnum), s.Replace(".", ""));
			if (ipos++ < arrFields.Count)
				c.mLongName = arrFields[ipos].Replace("'", "");

		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mLongName == "$" ? ",$,." : ",'" + mLongName + "',.") + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcBurner : IfcEnergyConversionDevice //IFC4
	{
		internal IfcBurnerTypeEnum mPredefinedType = IfcBurnerTypeEnum.NOTDEFINED;// OPTIONAL : IfcBurnerTypeEnum;
		internal IfcBurner() : base() { }
		internal IfcBurner(DatabaseIfc db, IfcBurner b) : base(db, b) { mPredefinedType = b.mPredefinedType; }
		internal IfcBurner(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcBurner s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcBurnerTypeEnum)Enum.Parse(typeof(IfcBurnerTypeEnum), str);
		}
		internal new static IfcBurner Parse(string strDef) { IfcBurner s = new IfcBurner(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcBurnerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcBurnerType : IfcEnergyConversionDeviceType
	{
		internal IfcBurnerTypeEnum mPredefinedType = IfcBurnerTypeEnum.NOTDEFINED;// : IfcBurnerTypeEnum
		public IfcBurnerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcBurnerType() : base() { }
		internal IfcBurnerType(DatabaseIfc db, IfcBurnerType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcBurnerType(DatabaseIfc m, string name, IfcBurnerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcBurnerType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcBurnerTypeEnum)Enum.Parse(typeof(IfcBurnerTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcBurnerType Parse(string strDef) { IfcBurnerType t = new IfcBurnerType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
}
