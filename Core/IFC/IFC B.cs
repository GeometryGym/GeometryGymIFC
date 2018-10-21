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
	[Serializable]
	public partial class IfcBeam : IfcBuildingElement
	{
		internal IfcBeamTypeEnum mPredefinedType = IfcBeamTypeEnum.NOTDEFINED;//: OPTIONAL IfcBeamTypeEnum; IFC4
		public IfcBeamTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBeam() : base() { }
		internal IfcBeam(DatabaseIfc db, IfcBeam b, IfcOwnerHistory ownerHistory, bool downStream) : base(db, b, ownerHistory, downStream) { mPredefinedType = b.mPredefinedType; }
		public IfcBeam(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
		public IfcBeam(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
		public IfcBeam(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, Tuple<double, double> arcOrigin, double arcAngle) : base(host, profile, placement, arcOrigin,arcAngle) { }
	}
	[Serializable]
	public partial class IfcBeamStandardCase : IfcBeam
	{
		internal override string KeyWord { get { return "IfcBeam"; } }

		internal IfcBeamStandardCase() : base() { }
		internal IfcBeamStandardCase(DatabaseIfc db, IfcBeamStandardCase b, IfcOwnerHistory ownerHistory, bool downStream) : base(db, b, ownerHistory, downStream) { }
		public IfcBeamStandardCase(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
		public IfcBeamStandardCase(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, Tuple<double, double> arcOrigin, double arcAngle) : base(host, profile, placement,arcOrigin, arcAngle) { }
	}
	[Serializable]
	public partial class IfcBeamType : IfcBuildingElementType
	{
		internal IfcBeamTypeEnum mPredefinedType = IfcBeamTypeEnum.NOTDEFINED;
		public IfcBeamTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBeamType() : base() { }
		internal IfcBeamType(DatabaseIfc db, IfcBeamType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcBeamType(DatabaseIfc db, string name, IfcBeamTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
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
	}
	public interface IfcBendingParameterSelect { } // 	IfcLengthMeasure, IfcPlaneAngleMeasure
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcBezierCurve : IfcBSplineCurve // DEPRECEATED IFC4
	{
		internal IfcBezierCurve() : base() { }
		internal IfcBezierCurve(DatabaseIfc db, IfcBezierCurve c) : base(db,c) { }
	}	
	[Serializable]
	public partial class IfcBlobTexture : IfcSurfaceTexture
	{
		internal string mRasterFormat;// : IfcIdentifier;
		internal bool mRasterCode;// : BOOLEAN;	
		internal IfcBlobTexture() : base() { }
		//internal IfcBlobTexture(IfcBlobTexture i) : base(i) { mRasterFormat = i.mRasterFormat; mRasterCode = i.mRasterCode; }
	}
	[Serializable]
	public partial class IfcBlock : IfcCsgPrimitive3D
	{
		private double mXLength, mYLength, mZLength;// : IfcPositiveLengthMeasure;

		public double XLength { get { return mXLength; } set { mXLength = value; } }
		public double YLength { get { return mYLength; } set { mYLength = value; } }
		public double ZLength { get { return mZLength; } set { mZLength = value; } }

		internal IfcBlock() : base() { }
		internal IfcBlock(DatabaseIfc db, IfcBlock b) : base(db,b) { mXLength = b.mXLength; mYLength = b.mYLength; mZLength = b.mZLength; }
		public IfcBlock(IfcAxis2Placement3D position, double x,double y, double z) : base(position) { mXLength = x; mYLength = y; mZLength = z; }
	}
	[Serializable]
	public partial class IfcBoiler : IfcEnergyConversionDevice //IFC4  
	{
		internal IfcBoilerTypeEnum mPredefinedType = IfcBoilerTypeEnum.NOTDEFINED;
		public IfcBoilerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBoiler() : base() { }
		internal IfcBoiler(DatabaseIfc db, IfcBoiler b, IfcOwnerHistory ownerHistory, bool downStream) : base(db, b, ownerHistory, downStream) { mPredefinedType = b.mPredefinedType; }
		public IfcBoiler(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcBoilerType : IfcEnergyConversionDeviceType
	{
		internal IfcBoilerTypeEnum mPredefinedType = IfcBoilerTypeEnum.NOTDEFINED;// : IfcBoilerypeEnum; 
		public IfcBoilerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcBoilerType() : base() { }
		internal IfcBoilerType(DatabaseIfc db, IfcBoilerType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcBoilerType(DatabaseIfc db, string name, IfcBoilerTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcBooleanClippingResult : IfcBooleanResult
	{
		internal IfcBooleanClippingResult() : base() { }
		internal IfcBooleanClippingResult(DatabaseIfc db, IfcBooleanClippingResult c) : base(db,c) { }
		public IfcBooleanClippingResult(IfcBooleanClippingResult bc, IfcHalfSpaceSolid hss) : base(IfcBooleanOperator.DIFFERENCE, bc, hss) { }
		public IfcBooleanClippingResult(IfcSweptAreaSolid s, IfcHalfSpaceSolid hss) : base(IfcBooleanOperator.DIFFERENCE, s, hss) { }
	}
	public partial interface IfcBooleanOperand : IBaseClassIfc { } //  SELECT (IfcSolidModel ,IfcHalfSpaceSolid ,IfcBooleanResult ,IfcCsgPrimitive3D);
	[Serializable]
	public partial class IfcBooleanResult : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect
	{
		private IfcBooleanOperator mOperator;// : IfcBooleanOperator;
		private int mFirstOperand;// : IfcBooleanOperand;
		private int mSecondOperand;// : IfcBooleanOperand;

		public IfcBooleanOperator Operator { get { return mOperator; } }
		public IfcBooleanOperand FirstOperand { get { return mDatabase[mFirstOperand] as IfcBooleanOperand; } set { mFirstOperand = value.Index; } }
		public IfcBooleanOperand SecondOperand { get { return mDatabase[mSecondOperand] as IfcBooleanOperand; } set { mSecondOperand = value.Index; } }

		internal IfcBooleanResult() : base() { }
		internal IfcBooleanResult(DatabaseIfc db, IfcBooleanResult b) : base(db,b) { mOperator = b.mOperator; FirstOperand = db.Factory.Duplicate(b.mDatabase[ b.mFirstOperand]) as IfcBooleanOperand; SecondOperand = db.Factory.Duplicate(b.mDatabase[b.mSecondOperand]) as IfcBooleanOperand; }
		public IfcBooleanResult(IfcBooleanOperator op, IfcBooleanOperand first, IfcBooleanOperand second) : base(first.Database)
		{
			mOperator = op;
			mFirstOperand = first.Index;
			mSecondOperand = second.Index;
		}
		
		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			mDatabase[mFirstOperand].changeSchema(schema);
			mDatabase[mSecondOperand].changeSchema(schema);
		}
	}
	[Serializable]
	public abstract partial class IfcBoundaryCondition : BaseClassIfc, NamedObjectIfc //ABSTRACT SUPERTYPE OF (ONEOF (IfcBoundaryEdgeCondition ,IfcBoundaryFaceCondition ,IfcBoundaryNodeCondition));
	{
		internal string mName  = "$";//  : OPTIONAL IfcLabel;
		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } } 
		protected IfcBoundaryCondition() : base() { }
		protected IfcBoundaryCondition(DatabaseIfc db, IfcBoundaryCondition b) : base(db,b) { mName = b.mName; }
		protected IfcBoundaryCondition(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcBoundaryCurve : IfcCompositeCurveOnSurface
	{
		internal IfcBoundaryCurve() : base() { }
		internal IfcBoundaryCurve(DatabaseIfc db, IfcBoundaryCurve c) : base(db,c) { }
		public IfcBoundaryCurve(List<IfcCompositeCurveSegment> segs, IfcSurface surface) : base(segs, surface) { }
	}
	[Serializable]
	public partial class IfcBoundaryEdgeCondition : IfcBoundaryCondition
	{
		internal double mLinearStiffnessByLengthX, mLinearStiffnessByLengthY, mLinearStiffnessByLengthZ;// : OPTIONAL IfcModulusOfLinearSubgradeReactionMeasure;
		internal double mRotationalStiffnessByLengthX, mRotationalStiffnessByLengthY, mRotationalStiffnessByLengthZ;// : OPTIONAL IfcModulusOfRotationalSubgradeReactionMeasure; 
		internal IfcBoundaryEdgeCondition() : base() { }
		internal IfcBoundaryEdgeCondition(DatabaseIfc db, IfcBoundaryEdgeCondition b) : base(db,b) { mLinearStiffnessByLengthX = b.mLinearStiffnessByLengthX; mLinearStiffnessByLengthY = b.mLinearStiffnessByLengthY; mLinearStiffnessByLengthZ = b.mLinearStiffnessByLengthZ; mRotationalStiffnessByLengthX = b.mRotationalStiffnessByLengthX; mRotationalStiffnessByLengthY = b.mRotationalStiffnessByLengthY; mRotationalStiffnessByLengthZ = b.mRotationalStiffnessByLengthZ; }
		public IfcBoundaryEdgeCondition(DatabaseIfc db) : base(db) {  }
	}
	[Serializable]
	public partial class IfcBoundaryFaceCondition : IfcBoundaryCondition
	{
		internal double mLinearStiffnessByAreaX, mLinearStiffnessByAreaY, mLinearStiffnessByAreaZ;// : OPTIONAL IfcModulusOfSubgradeReactionMeasure 
		internal IfcBoundaryFaceCondition() : base() { }
		internal IfcBoundaryFaceCondition(DatabaseIfc db, IfcBoundaryFaceCondition c) : base(db,c) { mLinearStiffnessByAreaX = c.mLinearStiffnessByAreaX; mLinearStiffnessByAreaY = c.mLinearStiffnessByAreaY; mLinearStiffnessByAreaZ = c.mLinearStiffnessByAreaZ; }
		public IfcBoundaryFaceCondition(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
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
		internal IfcBoundaryNodeCondition(DatabaseIfc db, IfcBoundaryNodeCondition b) : base(db,b) { mTranslationalStiffnessX = b.mTranslationalStiffnessX; mTranslationalStiffnessY = b.mTranslationalStiffnessY; mTranslationalStiffnessZ = b.mTranslationalStiffnessZ; mRotationalStiffnessX = b.mRotationalStiffnessX; mRotationalStiffnessY = b.mRotationalStiffnessY; mRotationalStiffnessZ = b.mRotationalStiffnessZ; }
		public IfcBoundaryNodeCondition(DatabaseIfc db) : base(db) { }
		public IfcBoundaryNodeCondition(DatabaseIfc db, string name, bool restrainX, bool restrainY, bool restrainZ, bool restrainXX, bool restrainYY, bool restrainZZ)
			: this(db, name, new IfcTranslationalStiffnessSelect(restrainX), new IfcTranslationalStiffnessSelect(restrainY), new IfcTranslationalStiffnessSelect(restrainZ), new IfcRotationalStiffnessSelect(restrainXX), new IfcRotationalStiffnessSelect(restrainYY), new IfcRotationalStiffnessSelect(restrainZZ)) { }
		public IfcBoundaryNodeCondition(DatabaseIfc db, string name, IfcTranslationalStiffnessSelect x, IfcTranslationalStiffnessSelect y, IfcTranslationalStiffnessSelect z, IfcRotationalStiffnessSelect xx, IfcRotationalStiffnessSelect yy, IfcRotationalStiffnessSelect zz) : base(db)
		{
			Name = name;
			if (db.mRelease < ReleaseVersion.IFC4)
			{
				if (x != null)
				{
					if (x.mRigid || x.mStiffness == null)
						mTranslationalStiffnessX = new IfcTranslationalStiffnessSelect(x.mRigid ? -1 : 0);
					else
						mTranslationalStiffnessX = x;
				}
				if (y != null)
				{
					if (y.mRigid || y.mStiffness == null)
						mTranslationalStiffnessY = new IfcTranslationalStiffnessSelect(y.mRigid ? -1 : 0);
					else
						mTranslationalStiffnessY = y;
				}
				if (z != null)
				{
					if (z.mRigid || z.mStiffness == null)
						mTranslationalStiffnessZ = new IfcTranslationalStiffnessSelect(z.mRigid ? -1 : 0);
					else
						mTranslationalStiffnessZ = z;
				}
				if (xx != null)
				{
					if (xx.mRigid || xx.mStiffness == null)
						mRotationalStiffnessX = new IfcRotationalStiffnessSelect(xx.mRigid ? -1 : 0);
					else
						mRotationalStiffnessX = xx;
				}
				if (yy != null)
				{
					if (yy.mRigid || yy.mStiffness == null)
						mRotationalStiffnessY = new IfcRotationalStiffnessSelect(yy.mRigid ? -1 : 0);
					else
						mRotationalStiffnessY = yy;
				}
				if (zz != null)
				{
					if (zz.mRigid || zz.mStiffness == null)
						mRotationalStiffnessZ = new IfcRotationalStiffnessSelect(zz.mRigid ? -1 : 0);
					else
						mRotationalStiffnessZ = zz;
				}
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
	}	
	[Serializable]
	public partial class IfcBoundaryNodeConditionWarping : IfcBoundaryNodeCondition
	{
		internal IfcWarpingStiffnessSelect mWarpingStiffness;// : OPTIONAL IfcWarpingStiffnessSelect; 
		internal IfcBoundaryNodeConditionWarping() : base() { }
		internal IfcBoundaryNodeConditionWarping(DatabaseIfc db, IfcBoundaryNodeConditionWarping b) : base(db, b) { mWarpingStiffness = b.mWarpingStiffness; }
		public IfcBoundaryNodeConditionWarping(DatabaseIfc m, string name, IfcTranslationalStiffnessSelect x, IfcTranslationalStiffnessSelect y, IfcTranslationalStiffnessSelect z, IfcRotationalStiffnessSelect xx, IfcRotationalStiffnessSelect yy, IfcRotationalStiffnessSelect zz, IfcWarpingStiffnessSelect w)
			: base(m, name, x, y, z, xx, yy, zz) { mWarpingStiffness = w; }
	}
	[Serializable]
	public abstract partial class IfcBoundedCurve : IfcCurve, IfcCurveOrEdgeCurve  //ABSTRACT SUPERTYPE OF (ONEOF (IfcBSplineCurve ,IfcCompositeCurve ,IfcPolyline ,IfcTrimmedCurve)) IFC4 IfcIndexedPolyCurve IFC4x1 IfcCurveSegment2D
	{
		protected IfcBoundedCurve() : base() { }
		protected IfcBoundedCurve(DatabaseIfc db) : base(db) { }
		protected IfcBoundedCurve(DatabaseIfc db, IfcBoundedCurve c) : base(db,c) { }

		public static IfcBoundedCurve Generate(DatabaseIfc db, IEnumerable<Tuple<double,double>> points, List<IfcSegmentIndexSelect> segments)
		{
			if(db.Release < ReleaseVersion.IFC4)
			{
				if(segments == null || segments.Count == 0)
					return new IfcPolyline(db, points);
				List<IfcCompositeCurveSegment> segs = new List<IfcCompositeCurveSegment>();
				List<IfcCartesianPoint> pts = points.ToList().ConvertAll(x => new IfcCartesianPoint(db, x.Item1, x.Item2));
				foreach(IfcSegmentIndexSelect seg in segments)
				{
					IfcArcIndex arc = seg as IfcArcIndex;
					if(arc != null)
						segs.Add(new IfcCompositeCurveSegment(IfcTransitionCode.CONTINUOUS, true, new IfcTrimmedCurve(pts[arc.mA - 1], points.ElementAt(arc.mB - 1), pts[arc.mC - 1])));
					else
					{
						IfcLineIndex line = seg as IfcLineIndex;
						if(line != null)
						{
							for(int icounter = 1; icounter < line.mIndices.Count; icounter++)
								segs.Add(new IfcCompositeCurveSegment(IfcTransitionCode.CONTINUOUS, true, new IfcPolyline(pts[line.mIndices[icounter - 1]-1], pts[line.mIndices[icounter]-1])));
						}
					}
				}
				return new IfcCompositeCurve(segs);
			}
			return new IfcIndexedPolyCurve(new IfcCartesianPointList2D(db, points), segments);
		}
	}
	[Serializable]
	public abstract partial class IfcBoundedSurface : IfcSurface //	ABSTRACT SUPERTYPE OF (ONEOF(IfcCurveBoundedPlane,IfcRectangularTrimmedSurface))
	{
		protected IfcBoundedSurface() : base() { }
		protected IfcBoundedSurface(DatabaseIfc db) : base(db) { }
		protected IfcBoundedSurface(DatabaseIfc db, IfcBoundedSurface s) : base(db,s) { }
	}
	[Serializable]
	public partial class IfcBoundingBox : IfcGeometricRepresentationItem
	{
		private int mCorner;// : IfcCartesianPoint;
		private double mXDim, mYDim, mZDim;// : IfcPositiveLengthMeasure

		public IfcCartesianPoint Corner { get { return mDatabase[mCorner] as IfcCartesianPoint; } set { mCorner = value.mIndex; } }
		public double XDim { get { return mXDim; } set { mXDim = value; } }
		public double YDim { get { return mYDim; } set { mYDim = value; } }
		public double ZDim { get { return mZDim; } set { mZDim = value; } }

		internal IfcBoundingBox() : base() { }
		internal IfcBoundingBox(DatabaseIfc db, IfcBoundingBox b) : base(db,b) { Corner = db.Factory.Duplicate(b.Corner) as IfcCartesianPoint; mXDim = b.mXDim; mYDim = b.mYDim; mZDim = b.mZDim; }
		public IfcBoundingBox(IfcCartesianPoint pt, double xdim, double ydim, double zdim) : base(pt.mDatabase)
		{
			//if (mModel.mModelView != ModelView.NotAssigned && mModel.mModelView != ModelView.IFC2x3Coordination)
			//	throw new Exception("Invalid Model View for IfcBoundingBox : " + m.ModelView.ToString());
			mCorner = pt.mIndex;
			mXDim = xdim;
			mYDim = ydim;
			mZDim = zdim;
		}
	}
	[Serializable]
	public partial class IfcBoxedHalfSpace : IfcHalfSpaceSolid
	{
		private int mEnclosure;// : IfcBoundingBox; 
		public IfcBoundingBox Enclosure { get { return mDatabase[mEnclosure] as IfcBoundingBox; } set { mEnclosure = value.mIndex; } }

		internal IfcBoxedHalfSpace() : base() { }
		internal IfcBoxedHalfSpace(DatabaseIfc db, IfcBoxedHalfSpace s) : base(db,s) { Enclosure = db.Factory.Duplicate(s.Enclosure) as IfcBoundingBox; }
	}
	[Serializable]
	public abstract partial class IfcBSplineCurve : IfcBoundedCurve //SUPERTYPE OF(IfcBSplineCurveWithKnots)
	{
		private int mDegree;// : INTEGER;
		private List<int> mControlPointsList = new List<int>();// : LIST [2:?] OF IfcCartesianPoint;
		private IfcBSplineCurveForm mCurveForm;// : IfcBSplineCurveForm;
		private IfcLogicalEnum mClosedCurve = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		private IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// : LOGICAL; 

		public int Degree { get { return mDegree; } }
		public ReadOnlyCollection<IfcCartesianPoint> ControlPointsList { get { return new ReadOnlyCollection<IfcCartesianPoint>( mControlPointsList.ConvertAll(x =>mDatabase[x] as IfcCartesianPoint)); } }
		public IfcBSplineCurveForm CurveForm { get { return mCurveForm; } }
		public IfcLogicalEnum ClosedCurve { get { return mClosedCurve; } set { mClosedCurve = value; } }
		public IfcLogicalEnum SelfIntersect { get { return mSelfIntersect; } set { mSelfIntersect = value; } }

		protected IfcBSplineCurve() : base() { }
		protected IfcBSplineCurve(DatabaseIfc db, IfcBSplineCurve c) : base(db, c)
		{
			mDegree = c.mDegree;
			c.ControlPointsList.ToList().ForEach(x => addControlPoint( db.Factory.Duplicate(x) as IfcCartesianPoint));
			mCurveForm = c.mCurveForm;
			mClosedCurve = c.mClosedCurve;
			mSelfIntersect = c.mSelfIntersect;
		}
		private IfcBSplineCurve(DatabaseIfc db, int degree, IfcBSplineCurveForm form) : base(db) { mDegree = degree; mCurveForm = form; }
		protected IfcBSplineCurve(int degree, List<IfcCartesianPoint> controlPoints, IfcBSplineCurveForm form)
			: this(controlPoints[0].mDatabase, degree, form) { mControlPointsList = controlPoints.ConvertAll(x => x.mIndex); }

		internal void addControlPoint(IfcCartesianPoint point) { mControlPointsList.Add(point.mIndex); }
	}
	[Serializable]
	public partial class IfcBSplineCurveWithKnots : IfcBSplineCurve
	{
		private List<int> mMultiplicities = new List<int>();// : LIST [2:?] OF INTEGER;
		private List<double> mKnots = new List<double>();// : LIST [2:?] OF IfcParameterValue;
		private IfcKnotType mKnotSpec = IfcKnotType.UNSPECIFIED;//: IfcKnotType;

		public ReadOnlyCollection<int> Multiplicities { get { return new ReadOnlyCollection<int>( mMultiplicities); } }
		public ReadOnlyCollection<double> Knots { get { return new ReadOnlyCollection<double>( mKnots); } }
		public IfcKnotType KnotSpec { get { return mKnotSpec; } }

		internal IfcBSplineCurveWithKnots() : base() { }
		internal IfcBSplineCurveWithKnots(DatabaseIfc db, IfcBSplineCurveWithKnots c) : base(db, c)
		{
			mMultiplicities.AddRange(c.Multiplicities);
			mKnots.AddRange(c.Knots);
			mKnotSpec = c.mKnotSpec;
		}
		public IfcBSplineCurveWithKnots(int degree, List<IfcCartesianPoint> controlPoints, IfcBSplineCurveForm form, List<int> multiplicities, List<double> knots, IfcKnotType knotSpec) :
			base(degree, controlPoints, form)
		{
			mMultiplicities.AddRange(multiplicities);
			mKnots.AddRange(knots);
		}
	}
	[Serializable]
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
		public ReadOnlyCollection<ReadOnlyCollection<IfcCartesianPoint>> ControlPointsList { get { return new ReadOnlyCollection<ReadOnlyCollection<IfcCartesianPoint>>( mControlPointsList.ConvertAll(x => new ReadOnlyCollection<IfcCartesianPoint>( x.ConvertAll(y =>mDatabase[y] as IfcCartesianPoint)))); } }
		public IfcBSplineSurfaceForm SurfaceForm { get { return mSurfaceForm; } }
		public IfcLogicalEnum UClosed { get { return mUClosed; } set { mUClosed = value; } }
		public IfcLogicalEnum VClosed { get { return mVClosed; } set { mVClosed = value; } }
		public IfcLogicalEnum SelfIntersect { get { return mSelfIntersect; } set { mSelfIntersect = value; } }

		protected IfcBSplineSurface() : base() { }
		protected IfcBSplineSurface(DatabaseIfc db, IfcBSplineSurface s) : base(db,s)
		{
			mUDegree = s.mUDegree;
			mVDegree = s.mVDegree;
			foreach(ReadOnlyCollection<IfcCartesianPoint> ps in s.ControlPointsList)
				mControlPointsList.Add(ps.ToList().ConvertAll(x=>db.Factory.Duplicate(x).mIndex));
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
		protected IfcBSplineSurface(int uDegree, int vDegree, List<List<IfcCartesianPoint>> controlPoints, IfcBSplineSurfaceForm form) :
			this(controlPoints[0][0].mDatabase, uDegree, vDegree, form)
		{
			foreach (List<IfcCartesianPoint> cps in controlPoints)
				mControlPointsList.Add(cps.ConvertAll(x => x.mIndex));
		}
	}
	[Serializable]
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
		public IfcBSplineSurfaceWithKnots(int uDegree, int vDegree, List<List<IfcCartesianPoint>> controlPoints, IfcBSplineSurfaceForm form, IfcLogicalEnum uClosed, IfcLogicalEnum vClosed, IfcLogicalEnum selfIntersect, List<int> uMultiplicities, List<int> vMultiplicities, List<double> uKnots, List<double> vKnots, IfcKnotType type)
			: base(uDegree, vDegree, controlPoints, form)
		{
			mUMultiplicities.AddRange(uMultiplicities);
			mVMultiplicities.AddRange(vMultiplicities);
			mUKnots.AddRange(uKnots);
			mVKnots.AddRange(vKnots);
		}
	}
	[Serializable]
	public partial class IfcBuilding : IfcFacility
	{
		internal IfcPostalAddress mBuildingAddress = null;// : OPTIONAL IfcPostalAddress; 
		public IfcPostalAddress BuildingAddress { get { return mBuildingAddress; } set { mBuildingAddress = value; } }

		internal IfcBuilding() : base() { }
		internal IfcBuilding(DatabaseIfc db, IfcBuilding b, IfcOwnerHistory ownerHistory, bool downStream) : base(db, b, ownerHistory, downStream)
		{
			if (b.mBuildingAddress != null)
				BuildingAddress = db.Factory.Duplicate(b.BuildingAddress) as IfcPostalAddress;
		}
		public IfcBuilding(DatabaseIfc db, string name) : base(db, name) { setDefaultAddress();  }
		public IfcBuilding(IfcSpatialStructureElement host, string name) : base(host, name) { }
		public IfcBuilding(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { setDefaultAddress(); }
		

		private void setDefaultAddress()  //Implementers Agreement requires address
		{
			BuildingAddress = new IfcPostalAddress(mDatabase) {  Town = "Unknown", Country = "Unknown", PostalCode = "Unknown" };
			BuildingAddress.AddressLines.Add("Unknown");
		}
		private void init(IfcSpatialElement container)
		{
			if (container != null) 
				container.AddAggregated(this);
		}

		internal bool addStorey(IfcBuildingStorey s) { return base.AddAggregated(s); }
	}
	[Serializable]
	public abstract partial class IfcBuildingElement : IfcElement //ABSTRACT SUPERTYPE OF (ONEOF (IfcBeam,IfcBuildingElementProxy,IfcColumn,IfcCovering,IfcCurtainWall,IfcDoor,IfcFooting
	{ //,IfcMember,IfcPile,IfcPlate,IfcRailing,IfcRamp,IfcRampFlight,IfcRoof,IfcSlab,IfcStair,IfcStairFlight,IfcWall,IfcWindow) IFC2x3 IfcBuildingElementComponent IFC4  IfcShadingDevice
		protected IfcBuildingElement() : base() { }
		protected IfcBuildingElement(DatabaseIfc db) : base(db) { }
		protected IfcBuildingElement(DatabaseIfc db, IfcBuildingElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { }
		protected IfcBuildingElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
		protected IfcBuildingElement(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement, length) { }
		protected IfcBuildingElement(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, Tuple<double, double> arcOrigin, double arcAngle) : base(host, profile, placement,arcOrigin, arcAngle) { }
	}
	/*internal class IfcBuildingElementComponent : IfcBuildingElement //IFC4 DELETED
	{
		protected IfcBuildingElementComponent(IfcBuildingElementComponent b) : base(b) { }
		protected IfcBuildingElementComponent() : base() { }
		protected static void parseFields(IfcBuildingElementComponent c, List<string> arrFields, ref int ipos) { IfcBuildingElement.parseFields(c, arrFields, ref ipos); }
	}*/
	[Serializable]
	public partial class IfcBuildingElementPart : IfcElementComponent
	{
		internal IfcBuildingElementPartTypeEnum mPredefinedType = IfcBuildingElementPartTypeEnum.NOTDEFINED;//:	OPTIONAL IfcBuildingElementPartTypeEnum; IFC4 added
		public IfcBuildingElementPartTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBuildingElementPart() : base() { }
		internal IfcBuildingElementPart(DatabaseIfc db, IfcBuildingElementPart p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mPredefinedType = p.mPredefinedType; }
		public IfcBuildingElementPart(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcBuildingElementPartType : IfcElementComponentType
	{
		internal IfcBuildingElementPartTypeEnum mPredefinedType = IfcBuildingElementPartTypeEnum.NOTDEFINED;// : IfcBuildingElementPartTypeEnum;
		public IfcBuildingElementPartTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBuildingElementPartType() : base() { }
		internal IfcBuildingElementPartType(DatabaseIfc db, IfcBuildingElementPartType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcBuildingElementPartType(DatabaseIfc m, string name, IfcBuildingElementPartTypeEnum type) : base(m) { Name = name; if (mDatabase.mRelease < ReleaseVersion.IFC4) throw new Exception("XXX Only valid in IFC4 or newer!"); mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcBuildingElementProxy : IfcBuildingElement
	{
		internal IfcBuildingElementProxyTypeEnum mPredefinedType = IfcBuildingElementProxyTypeEnum.NOTDEFINED; //	:	OPTIONAL IfcBuildingElementProxyTypeEnum;
		//Ifc2x3 internal IfcElementCompositionEnum mCompositionType = IfcElementCompositionEnum.NA;// : OPTIONAL IfcElementCompositionEnum; 

		public IfcBuildingElementProxyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public override string Name { get { return base.Name; } set { base.Name = (string.IsNullOrEmpty(value) ? "NOTDEFINED" : value); } }

		internal IfcBuildingElementProxy() : base() { }
		internal IfcBuildingElementProxy(DatabaseIfc db, IfcBuildingElementProxy p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mPredefinedType = p.mPredefinedType; }
		public IfcBuildingElementProxy(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { Name = "NOTDEFINED"; }
		public IfcBuildingElementProxy(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public partial class IfcBuildingElementProxyType : IfcBuildingElementType
	{
		internal IfcBuildingElementProxyTypeEnum mPredefinedType = IfcBuildingElementProxyTypeEnum.NOTDEFINED;// : IfcBuildingElementProxyTypeEnum;
		public IfcBuildingElementProxyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBuildingElementProxyType() : base() { }
		internal IfcBuildingElementProxyType(DatabaseIfc db, IfcBuildingElementProxyType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcBuildingElementProxyType(DatabaseIfc m, string name, IfcBuildingElementProxyTypeEnum type) : base(m)
		{
			Name = name;
			mPredefinedType = type;
			if (m.mRelease < ReleaseVersion.IFC4)
			{
				if (type != IfcBuildingElementProxyTypeEnum.USERDEFINED && type != IfcBuildingElementProxyTypeEnum.NOTDEFINED)
				{
					if (ElementType == "$")
						ElementType = type.ToString();
					mPredefinedType = IfcBuildingElementProxyTypeEnum.USERDEFINED;
				}
			}
		}
	}
	[Serializable]
	public abstract partial class IfcBuildingElementType : IfcElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcBeamType, IfcBuildingElementProxyType, IfcChimneyType, IfcColumnType, 
	{	//IfcCoveringType, IfcCurtainWallType, IfcDoorType, IfcFootingType, IfcMemberType, IfcPileType, IfcPlateType, IfcRailingType, IfcRampFlightType, IfcRampType, 
		//IfcRoofType, IfcShadingDeviceType, IfcSlabType, IfcStairFlightType, IfcStairType, IfcWallType, IfcWindowType))
		protected IfcBuildingElementType() : base() { }
		protected IfcBuildingElementType(DatabaseIfc db) : base(db) { }
		protected IfcBuildingElementType(DatabaseIfc db, IfcBuildingElementType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcBuildingStorey : IfcFacilityPart
	{ 
		public IfcBuildingStorey() : base() { }
		internal IfcBuildingStorey(DatabaseIfc db, IfcBuildingStorey s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { }
		public IfcBuildingStorey(IfcFacility host, string name, double elevation) : base(host, name, elevation) {  }
		public IfcBuildingStorey(IfcFacilityPart host, string name, double elevation) : base(host, name, elevation) {  }
		public IfcBuildingStorey(IfcFacility host, string name, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, name, p, r) { }
	}
	[Serializable]
	public partial class IfcBuildingSystem : IfcSystem //IFC4
	{
		internal IfcBuildingSystemTypeEnum mPredefinedType = IfcBuildingSystemTypeEnum.NOTDEFINED;// : OPTIONAL IfcBuildingSystemTypeEnum;
		internal string mLongName = "$"; // 	OPTIONAL IfcLabel IFC4ADD1 

		public IfcBuildingSystemTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public string LongName { get { return (mLongName == "$" ? "" : ParserIfc.Decode(mLongName)); } set { mLongName = (string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value)); } }

		internal IfcBuildingSystem() : base() { }
		internal IfcBuildingSystem(DatabaseIfc db, IfcBuildingSystem s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { mLongName = s.mLongName; mPredefinedType = s.mPredefinedType; }
		public IfcBuildingSystem(IfcSpatialElement bldg, string name,  IfcBuildingSystemTypeEnum type) : base(bldg, name) { mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcBurner : IfcEnergyConversionDevice //IFC4
	{
		internal IfcBurnerTypeEnum mPredefinedType = IfcBurnerTypeEnum.NOTDEFINED;// OPTIONAL : IfctypeEnum;
		public IfcBurnerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBurner() : base() { }
		internal IfcBurner(DatabaseIfc db, IfcBurner b, IfcOwnerHistory ownerHistory, bool downStream) : base(db, b, ownerHistory, downStream) { mPredefinedType = b.mPredefinedType; }
		public IfcBurner(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcBurnerType : IfcEnergyConversionDeviceType
	{
		internal IfcBurnerTypeEnum mPredefinedType = IfcBurnerTypeEnum.NOTDEFINED;// : IfcBurnerTypeEnum
		public IfcBurnerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcBurnerType() : base() { }
		internal IfcBurnerType(DatabaseIfc db, IfcBurnerType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcBurnerType(DatabaseIfc m, string name, IfcBurnerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			if (schema < ReleaseVersion.IFC4)
			{
				IfcSpaceHeaterType spaceHeaterType = new IfcSpaceHeaterType(this);
			}
		}
	}
}
