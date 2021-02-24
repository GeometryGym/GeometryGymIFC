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
	public partial class IfcBeam : IfcBuiltElement
	{
		internal IfcBeamTypeEnum mPredefinedType = IfcBeamTypeEnum.NOTDEFINED;//: OPTIONAL IfcBeamTypeEnum; IFC4
		public IfcBeamTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBeam() : base() { }
		internal IfcBeam(DatabaseIfc db, IfcBeam b, DuplicateOptions options) : base(db, b, options) { mPredefinedType = b.mPredefinedType; }
		public IfcBeam(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape shape) : base(host, placement, shape) { }
		public IfcBeam(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
		public IfcBeam(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, Tuple<double, double> arcOrigin, double arcAngle) : base(host, profile, placement, arcOrigin,arcAngle) { }
	}
	[Serializable]
	public partial class IfcBeamStandardCase : IfcBeam
	{
		public override string StepClassName { get { return "IfcBeam"; } }

		internal IfcBeamStandardCase() : base() { }
		internal IfcBeamStandardCase(DatabaseIfc db, IfcBeamStandardCase b, DuplicateOptions options) : base(db, b, options) { }
		public IfcBeamStandardCase(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
		public IfcBeamStandardCase(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, Tuple<double, double> arcOrigin, double arcAngle) : base(host, profile, placement,arcOrigin, arcAngle) { }
	}
	[Serializable]
	public partial class IfcBeamType : IfcBuiltElementType
	{
		internal IfcBeamTypeEnum mPredefinedType = IfcBeamTypeEnum.NOTDEFINED;
		public IfcBeamTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBeamType() : base() { }
		internal IfcBeamType(DatabaseIfc db, IfcBeamType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
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
	[Serializable]
	public partial class IfcBearing : IfcBuiltElement
	{
		private IfcBearingTypeEnum mPredefinedType = IfcBearingTypeEnum.NOTDEFINED; //: OPTIONAL IfcBearingTypeEnum;
		public IfcBearingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcBearing() : base() { }
		public IfcBearing(DatabaseIfc db) : base(db) { }
		public IfcBearing(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcBearingType : IfcBuiltElementType
	{
		private IfcBearingTypeEnum mPredefinedType = IfcBearingTypeEnum.NOTDEFINED; //: IfcBearingTypeEnum;
		public IfcBearingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcBearingType() : base() { }
		public IfcBearingType(DatabaseIfc db, string name, IfcBearingTypeEnum predefinedType)
			: base(db, name)
		{
			PredefinedType = predefinedType;
		}
	}
	public interface IfcBendingParameterSelect { } // 	IfcLengthMeasure, IfcPlaneAngleMeasure
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcBezierCurve : IfcBSplineCurve // DEPRECATED IFC4
	{
		internal IfcBezierCurve() : base() { }
		internal IfcBezierCurve(DatabaseIfc db, IfcBezierCurve c, DuplicateOptions options) : base(db, c, options) { }
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
		internal IfcBlock(DatabaseIfc db, IfcBlock b, DuplicateOptions options) : base(db, b, options) { mXLength = b.mXLength; mYLength = b.mYLength; mZLength = b.mZLength; }
		public IfcBlock(IfcAxis2Placement3D position, double x,double y, double z) : base(position) { mXLength = x; mYLength = y; mZLength = z; }
	}
	[Serializable]
	public partial class IfcBlossCurve : IfcSpiral
	{
		private double mQubicTerm = 0; //: IfcLengthMeasure;
		private double mQuadraticTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mLinearTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;

		public double QubicTerm { get { return mQubicTerm; } set { mQubicTerm = value; } }
		public double QuadraticTerm { get { return mQuadraticTerm; } set { mQuadraticTerm = value; } }
		public double LinearTerm { get { return mLinearTerm; } set { mLinearTerm = value; } }

		public IfcBlossCurve() : base() { }
		internal IfcBlossCurve(DatabaseIfc db, IfcBlossCurve bloss, DuplicateOptions options)
			: base(db, bloss, options) { QubicTerm = bloss.QubicTerm; QuadraticTerm = bloss.QuadraticTerm; LinearTerm = bloss.LinearTerm; }
		public IfcBlossCurve(IfcAxis2Placement position, double qubicTerm)
			: base(position) { QubicTerm = qubicTerm; }
	}
	[Serializable]
	public partial class IfcBoiler : IfcEnergyConversionDevice //IFC4  
	{
		internal IfcBoilerTypeEnum mPredefinedType = IfcBoilerTypeEnum.NOTDEFINED;
		public IfcBoilerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBoiler() : base() { }
		internal IfcBoiler(DatabaseIfc db, IfcBoiler b, DuplicateOptions options) : base(db, b, options) { mPredefinedType = b.mPredefinedType; }
		public IfcBoiler(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcBoilerType : IfcEnergyConversionDeviceType
	{
		internal IfcBoilerTypeEnum mPredefinedType = IfcBoilerTypeEnum.NOTDEFINED;// : IfcBoilerypeEnum; 
		public IfcBoilerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcBoilerType() : base() { }
		internal IfcBoilerType(DatabaseIfc db, IfcBoilerType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcBoilerType(DatabaseIfc db, string name, IfcBoilerTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcBooleanClippingResult : IfcBooleanResult
	{
		internal IfcBooleanClippingResult() : base() { }
		internal IfcBooleanClippingResult(DatabaseIfc db, IfcBooleanClippingResult c, DuplicateOptions options) : base(db, c, options) { }
		public IfcBooleanClippingResult(IfcBooleanClippingResult bc, IfcHalfSpaceSolid hss) : base(IfcBooleanOperator.DIFFERENCE, bc, hss) { }
		public IfcBooleanClippingResult(IfcSweptAreaSolid s, IfcHalfSpaceSolid hss) : base(IfcBooleanOperator.DIFFERENCE, s, hss) { }
	}
	public partial interface IfcBooleanOperand : IBaseClassIfc { } //  SELECT (IfcSolidModel ,IfcHalfSpaceSolid ,IfcBooleanResult ,IfcCsgPrimitive3D);
	[Serializable]
	public partial class IfcBooleanResult : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect
	{
		private IfcBooleanOperator mOperator;// : IfcBooleanOperator;
		private IfcBooleanOperand mFirstOperand;// : IfcBooleanOperand;
		private IfcBooleanOperand mSecondOperand;// : IfcBooleanOperand;

		public IfcBooleanOperator Operator { get { return mOperator; } }
		public IfcBooleanOperand FirstOperand { get { return mFirstOperand; } set { mFirstOperand = value; } }
		public IfcBooleanOperand SecondOperand { get { return mSecondOperand; } set { mSecondOperand = value; } }

		internal IfcBooleanResult() : base() { }
		internal IfcBooleanResult(DatabaseIfc db, IfcBooleanResult b, DuplicateOptions options) : base(db, b, options)
		{
			mOperator = b.mOperator;
			FirstOperand = db.Factory.Duplicate(b.FirstOperand) as IfcBooleanOperand;
			SecondOperand = db.Factory.Duplicate(b.SecondOperand) as IfcBooleanOperand;
		}
		public IfcBooleanResult(IfcBooleanOperator op, IfcBooleanOperand first, IfcBooleanOperand second) : base(first.Database)
		{
			mOperator = op;
			mFirstOperand = first;
			mSecondOperand = second;
		}
	}
	[Serializable]
	public partial class IfcBorehole : IfcGeotechnicalAssembly
	{
		public IfcBorehole() : base() { }
		public IfcBorehole(DatabaseIfc db) : base(db) { }
		public IfcBorehole(DatabaseIfc db, IfcBorehole borehole, DuplicateOptions options) : base(db, borehole, options) { }
		public IfcBorehole(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
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
		internal IfcBoundaryCurve(DatabaseIfc db, IfcBoundaryCurve c, DuplicateOptions options) : base(db, c, options) { }
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
	public abstract partial class IfcBoundedCurve : IfcCurve, IfcCurveOrEdgeCurve  //ABSTRACT SUPERTYPE OF (ONEOF (IfcBSplineCurve ,IfcCompositeCurve ,IfcPolyline ,IfcTrimmedCurve)) IFC4 IfcIndexedPolyCurve IFC4x1 IfcCurveSegment2D IfcAlignment2DHorizontal
	{
		protected IfcBoundedCurve() : base() { }
		protected IfcBoundedCurve(DatabaseIfc db) : base(db) { }
		protected IfcBoundedCurve(DatabaseIfc db, IfcBoundedCurve c, DuplicateOptions options) : base(db, c, options) { }

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
						segs.Add(new IfcCompositeCurveSegment(IfcTransitionCode.CONTINUOUS, true, new IfcTrimmedCurve(pts[arc[0] - 1], points.ElementAt(arc[1] - 1), pts[arc[2] - 1])));
					else
					{
						IfcLineIndex line = seg as IfcLineIndex;
						if(line != null)
						{
							for(int icounter = 1; icounter < line.Count; icounter++)
								segs.Add(new IfcCompositeCurveSegment(IfcTransitionCode.CONTINUOUS, true, new IfcPolyline(pts[line[icounter - 1]-1], pts[line[icounter]-1])));
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
		protected IfcBoundedSurface(DatabaseIfc db, IfcBoundedSurface s, DuplicateOptions options) : base(db, s, options) { }
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
		internal IfcBoundingBox(DatabaseIfc db, IfcBoundingBox b, DuplicateOptions options) : base(db, b, options) { Corner = db.Factory.Duplicate(b.Corner) as IfcCartesianPoint; mXDim = b.mXDim; mYDim = b.mYDim; mZDim = b.mZDim; }
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
		internal IfcBoxedHalfSpace(DatabaseIfc db, IfcBoxedHalfSpace s, DuplicateOptions options) : base(db, s, options) { Enclosure = db.Factory.Duplicate(s.Enclosure) as IfcBoundingBox; }
	}
	[Serializable]
	public partial class IfcBridge : IfcFacility
	{
		private IfcBridgeTypeEnum mPredefinedType = IfcBridgeTypeEnum.NOTDEFINED; //: OPTIONAL IfcBridgeTypeEnum;
		public IfcBridgeTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcBridge() : base() { }
		public IfcBridge(DatabaseIfc db) : base(db) { }
		public IfcBridge(DatabaseIfc db, IfcBridge bridge, DuplicateOptions options) : base(db, bridge, options) { }
		public IfcBridge(IfcFacility host, string name, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { Name = name; }
		public IfcBridge(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcBridgePart : IfcFacilityPart
	{
		public override string StepClassName { get { if (mDatabase != null && mDatabase.Release > ReleaseVersion.IFC4X2) return "IfcFacilityPart"; return base.StepClassName; } }
		public IfcBridgePart() : base() { }
		public IfcBridgePart(DatabaseIfc db) : base(db) { }
		public IfcBridgePart(DatabaseIfc db, IfcBridgePart bridgePart, DuplicateOptions options) : base(db, bridgePart, options) { }
		public IfcBridgePart(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcBSplineCurve : IfcBoundedCurve //SUPERTYPE OF(IfcBSplineCurveWithKnots)
	{
		private int mDegree;// : INTEGER;
		private LIST<IfcCartesianPoint> mControlPointsList = new LIST<IfcCartesianPoint>();// : LIST [2:?] OF IfcCartesianPoint;
		private IfcBSplineCurveForm mCurveForm = IfcBSplineCurveForm.UNSPECIFIED;// : IfcBSplineCurveForm;
		private IfcLogicalEnum mClosedCurve = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		private IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// : LOGICAL; 

		public int Degree { get { return mDegree; } }
		public LIST<IfcCartesianPoint> ControlPointsList { get { return mControlPointsList; } }
		public IfcBSplineCurveForm CurveForm { get { return mCurveForm; } set { mCurveForm = value; } }
		public IfcLogicalEnum ClosedCurve { get { return mClosedCurve; } set { mClosedCurve = value; } }
		public IfcLogicalEnum SelfIntersect { get { return mSelfIntersect; } set { mSelfIntersect = value; } }

		protected IfcBSplineCurve() : base() { }
		protected IfcBSplineCurve(DatabaseIfc db, IfcBSplineCurve c, DuplicateOptions options) : base(db, c, options)
		{
			mDegree = c.mDegree;
			ControlPointsList.AddRange(c.ControlPointsList.Select(x=>db.Factory.Duplicate(x) as IfcCartesianPoint));
			mCurveForm = c.mCurveForm;
			mClosedCurve = c.mClosedCurve;
			mSelfIntersect = c.mSelfIntersect;
		}
		private IfcBSplineCurve(DatabaseIfc db, int degree) : base(db) { mDegree = degree; }
		protected IfcBSplineCurve(int degree, IEnumerable<IfcCartesianPoint> controlPoints)
			: this(controlPoints.First().mDatabase, degree) { ControlPointsList.AddRange(controlPoints); }

	}
	[Serializable]
	public partial class IfcBSplineCurveWithKnots : IfcBSplineCurve
	{
		private LIST<int> mKnotMultiplicities = new LIST<int>();// : LIST [2:?] OF INTEGER;
		private LIST<double> mKnots = new LIST<double>();// : LIST [2:?] OF IfcParameterValue;
		private IfcKnotType mKnotSpec = IfcKnotType.UNSPECIFIED;//: IfcKnotType;

		public LIST<int> KnotMultiplicities { get { return mKnotMultiplicities; } }
		public ReadOnlyCollection<double> Knots { get { return new ReadOnlyCollection<double>( mKnots); } }
		public IfcKnotType KnotSpec { get { return mKnotSpec; } }

		internal IfcBSplineCurveWithKnots() : base() { }
		internal IfcBSplineCurveWithKnots(DatabaseIfc db, IfcBSplineCurveWithKnots c, DuplicateOptions options) : base(db, c, options)
		{
			mKnotMultiplicities.AddRange(c.KnotMultiplicities);
			mKnots.AddRange(c.Knots);
			mKnotSpec = c.mKnotSpec;
		}
		public IfcBSplineCurveWithKnots(int degree, IEnumerable<IfcCartesianPoint> controlPoints, IEnumerable<int> multiplicities, IEnumerable<double> knots, IfcKnotType knotSpec) :
			base(degree, controlPoints)
		{
			mKnotMultiplicities.AddRange(multiplicities);
			mKnots.AddRange(knots);
		}
	}
	[Serializable]
	public abstract partial class IfcBSplineSurface : IfcBoundedSurface //ABSTRACT SUPERTYPE OF	(IfcBSplineSurfaceWithKnots)
	{
		private int mUDegree;// : INTEGER;
		private int mVDegree;// : INTEGER;
		private LIST<LIST<IfcCartesianPoint>> mControlPointsList = new LIST<LIST<IfcCartesianPoint>>();// : LIST [2:?] OF LIST [2:?] OF IfcCartesianPoint;
		private IfcBSplineSurfaceForm mSurfaceForm = IfcBSplineSurfaceForm.UNSPECIFIED;// : IfcBSplineSurfaceForm;
		private IfcLogicalEnum mUClosed = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		private IfcLogicalEnum mVClosed = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		private IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// : LOGICAL; 

		public int UDegree { get { return mUDegree; } }
		public int VDegree { get { return mVDegree; } }
		public LIST<LIST<IfcCartesianPoint>> ControlPointsList { get { return mControlPointsList; } }
		public IfcBSplineSurfaceForm SurfaceForm { get { return mSurfaceForm; } set { mSurfaceForm = value; } }
		public IfcLogicalEnum UClosed { get { return mUClosed; } set { mUClosed = value; } }
		public IfcLogicalEnum VClosed { get { return mVClosed; } set { mVClosed = value; } }
		public IfcLogicalEnum SelfIntersect { get { return mSelfIntersect; } set { mSelfIntersect = value; } }

		protected IfcBSplineSurface() : base() { }
		protected IfcBSplineSurface(DatabaseIfc db, IfcBSplineSurface s, DuplicateOptions options) : base(db, s, options)
		{
			mUDegree = s.mUDegree;
			mVDegree = s.mVDegree;
			foreach(LIST<IfcCartesianPoint> ps in s.ControlPointsList)
				mControlPointsList.Add(new LIST<IfcCartesianPoint>(ps.ConvertAll(x=>db.Factory.Duplicate(x) as IfcCartesianPoint)));
			mSurfaceForm = s.mSurfaceForm;
			mUClosed = s.mUClosed;
			mVClosed = s.mVClosed;
			mSelfIntersect = s.mSelfIntersect;
		}
		private IfcBSplineSurface(DatabaseIfc db, int uDegree, int vDegree) : base(db)
		{
			mUDegree = uDegree;
			mVDegree = vDegree;
		}
		protected IfcBSplineSurface(int uDegree, int vDegree, IEnumerable<IEnumerable<IfcCartesianPoint>> controlPoints) :
			this(controlPoints.First().First().mDatabase, uDegree, vDegree)
		{
			foreach (IEnumerable<IfcCartesianPoint> points in controlPoints)
				ControlPointsList.Add(new LIST<IfcCartesianPoint>(points));
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

		public ReadOnlyCollection<int> UMultiplicities { get { return new ReadOnlyCollection<int>(mUMultiplicities); } }
		public ReadOnlyCollection<int> VMultiplicities { get { return new ReadOnlyCollection<int>(mVMultiplicities); } }
		public ReadOnlyCollection<double> UKnots { get { return new ReadOnlyCollection<double>(mUKnots); } }
		public ReadOnlyCollection<double> VKnots { get { return new ReadOnlyCollection<double>(mVKnots); } }
		public IfcKnotType KnotSpec { get { return mKnotSpec; } }

		internal IfcBSplineSurfaceWithKnots() : base() { }
		internal IfcBSplineSurfaceWithKnots(DatabaseIfc db, IfcBSplineSurfaceWithKnots s, DuplicateOptions options) : base(db, s, options)
		{
			mUMultiplicities = new List<int>(s.mUMultiplicities.ToArray());
			mVMultiplicities = new List<int>(s.mVMultiplicities.ToArray());
			mUKnots = new List<double>(s.mUKnots.ToArray());
			mVKnots = new List<double>(s.mVKnots.ToArray());
			mKnotSpec = s.mKnotSpec;
		}
		public IfcBSplineSurfaceWithKnots(int uDegree, int vDegree, IEnumerable<IEnumerable<IfcCartesianPoint>> controlPoints, IEnumerable<int> uMultiplicities, IEnumerable<int> vMultiplicities, IEnumerable<double> uKnots, IEnumerable<double> vKnots, IfcKnotType type)
			: base(uDegree, vDegree, controlPoints)
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
		internal double mElevationOfRefHeight = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal double mElevationOfTerrain = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal IfcPostalAddress mBuildingAddress = null;// : OPTIONAL IfcPostalAddress; 

		public double ElevationOfRefHeight { get { return mElevationOfRefHeight; } set { mElevationOfRefHeight = value; } }
		public double ElevationOfTerrain { get { return mElevationOfTerrain; } set { mElevationOfTerrain = value; } }
		public IfcPostalAddress BuildingAddress { get { return mBuildingAddress; } set { mBuildingAddress = value; } }

		internal IfcBuilding() : base() { }
		internal IfcBuilding(DatabaseIfc db, IfcBuilding b, DuplicateOptions options) : base(db, b, options)
		{
			mElevationOfRefHeight = b.mElevationOfRefHeight;
			mElevationOfTerrain = b.mElevationOfTerrain;
			if (b.mBuildingAddress != null)
				BuildingAddress = db.Factory.Duplicate(b.BuildingAddress) as IfcPostalAddress;
		}
		public IfcBuilding(DatabaseIfc db, string name) : base(db, name) { setDefaultAddress();  }
		public IfcBuilding(IfcSpatialStructureElement host, string name) : base(host, name) { }
		public IfcBuilding(IfcFacility host, string name, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, name, placement, representation) { setDefaultAddress(); }
		internal IfcBuilding(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { setDefaultAddress(); }
		

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
	}
	[Serializable]
	public partial class IfcBuiltElement : IfcElement //SUPERTYPE OF (ONEOF (IfcBeam,IfcBuildingElementProxy,IfcColumn,IfcCovering,IfcCurtainWall,IfcDoor,IfcFooting
	{ //,IfcMember,IfcPile,IfcPlate,IfcRailing,IfcRamp,IfcRampFlight,IfcRoof,IfcSlab,IfcStair,IfcStairFlight,IfcWall,IfcWindow) IFC2x3 IfcBuildingElementComponent IFC4  IfcShadingDevice
			//Non-Abstract in IFC4x3
		protected IfcBuiltElement() : base() { }
		public IfcBuiltElement(DatabaseIfc db) : base(db) { }
		protected IfcBuiltElement(DatabaseIfc db, IfcBuiltElement e, DuplicateOptions options) : base(db, e, options) { }
		public IfcBuiltElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductDefinitionShape r) : base(host, p, r) { }
		public IfcBuiltElement(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement, length) { }
		public IfcBuiltElement(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, Tuple<double, double> arcOrigin, double arcAngle) : base(host, profile, placement,arcOrigin, arcAngle) { }
	}
	[Obsolete("DELETED IFC4", false)]
	public abstract class IfcBuildingElementComponent : IfcBuiltElement 
	{
		protected IfcBuildingElementComponent() : base() { }
	}
	[Serializable]
	public partial class IfcBuildingElementPart : IfcElementComponent
	{
		internal IfcBuildingElementPartTypeEnum mPredefinedType = IfcBuildingElementPartTypeEnum.NOTDEFINED;//:	OPTIONAL IfcBuildingElementPartTypeEnum; IFC4 added
		public IfcBuildingElementPartTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBuildingElementPart() : base() { }
		internal IfcBuildingElementPart(DatabaseIfc db, IfcBuildingElementPart p, DuplicateOptions options) : base(db, p, options) { mPredefinedType = p.mPredefinedType; }
		public IfcBuildingElementPart(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcBuildingElementPartType : IfcElementComponentType
	{
		internal IfcBuildingElementPartTypeEnum mPredefinedType = IfcBuildingElementPartTypeEnum.NOTDEFINED;// : IfcBuildingElementPartTypeEnum;
		public IfcBuildingElementPartTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBuildingElementPartType() : base() { }
		internal IfcBuildingElementPartType(DatabaseIfc db, IfcBuildingElementPartType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcBuildingElementPartType(DatabaseIfc m, string name, IfcBuildingElementPartTypeEnum type) : base(m) { Name = name; if (mDatabase.mRelease < ReleaseVersion.IFC4) throw new Exception("XXX Only valid in IFC4 or newer!"); mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcBuildingElementProxy : IfcBuiltElement
	{
		internal IfcBuildingElementProxyTypeEnum mPredefinedType = IfcBuildingElementProxyTypeEnum.NOTDEFINED; //	:	OPTIONAL IfcBuildingElementProxyTypeEnum;
		//Ifc2x3 internal IfcElementCompositionEnum mCompositionType = IfcElementCompositionEnum.NA;// : OPTIONAL IfcElementCompositionEnum; 

		public IfcBuildingElementProxyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public override string Name { get { return base.Name; } set { base.Name = (string.IsNullOrEmpty(value) ? "NOTDEFINED" : value); } }

		internal IfcBuildingElementProxy() : base() { }
		internal IfcBuildingElementProxy(DatabaseIfc db, IfcBuildingElementProxy p, DuplicateOptions options) : base(db, p, options) { mPredefinedType = p.mPredefinedType; }
		public IfcBuildingElementProxy(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductDefinitionShape r) : base(host, p, r) { Name = "NOTDEFINED"; }
		public IfcBuildingElementProxy(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public partial class IfcBuildingElementProxyType : IfcBuiltElementType
	{
		internal IfcBuildingElementProxyTypeEnum mPredefinedType = IfcBuildingElementProxyTypeEnum.NOTDEFINED;// : IfcBuildingElementProxyTypeEnum;
		public IfcBuildingElementProxyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBuildingElementProxyType() : base() { }
		internal IfcBuildingElementProxyType(DatabaseIfc db, IfcBuildingElementProxyType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
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
	public partial class IfcBuiltElementType : IfcElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcBeamType, IfcBuildingElementProxyType, IfcChimneyType, IfcColumnType, 
	{	//IfcCoveringType, IfcCurtainWallType, IfcDoorType, IfcFootingType, IfcMemberType, IfcPileType, IfcPlateType, IfcRailingType, IfcRampFlightType, IfcRampType, 
		//IfcRoofType, IfcShadingDeviceType, IfcSlabType, IfcStairFlightType, IfcStairType, IfcWallType, IfcWindowType))
		protected IfcBuiltElementType() : base() { }
		protected IfcBuiltElementType(DatabaseIfc db) : base(db) { }
		protected IfcBuiltElementType(DatabaseIfc db, string name) : base(db) { Name = name; }
		protected IfcBuiltElementType(DatabaseIfc db, IfcBuiltElementType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcBuildingStorey : IfcSpatialStructureElement
	{
		internal double mElevation = double.NaN;// : OPTIONAL IfcLengthMeasure; 
		public double Elevation
		{
			get { return mElevation; }
			set { mElevation = Math.Abs(value) < mDatabase.Tolerance ? 0 : value; }
		}
		public IfcBuildingStorey() : base() { }
		internal IfcBuildingStorey(DatabaseIfc db, IfcBuildingStorey s, DuplicateOptions options) : base(db, s, options) { mElevation = s.mElevation; }
		public IfcBuildingStorey(IfcFacility host, string name, double elevation) : base(host, name) { Elevation = elevation; }
		public IfcBuildingStorey(IfcFacilityPart host, string name, double elevation) : base(host, name) { Elevation = elevation; }
		public IfcBuildingStorey(IfcSite host, string name, double elevation) : base(host, name) { Elevation = elevation; }
		public IfcBuildingStorey(IfcBuildingStorey host, string name, double elevation) : base(host, name) { Elevation = elevation; }
		public IfcBuildingStorey(IfcFacility host, string name, IfcObjectPlacement p, IfcProductDefinitionShape r) : base(host, p, r) { Name = name; }
		internal IfcBuildingStorey(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductDefinitionShape r) : base(host, p, r) { }
	}
	[Serializable]
	public partial class IfcBuiltSystem : IfcSystem //IFC4 IfcBuildingSystem
	{
		public override string StepClassName
		{
			get
			{
				if (mDatabase != null)
				{
					if (mDatabase.Release < ReleaseVersion.IFC4)
						return "IfcSystem";
					if (mDatabase.Release < ReleaseVersion.IFC4X3_RC1)
						return "IfcBuildingSystem";
				}
				return base.StepClassName;
			}
		}
		internal IfcBuiltSystemTypeEnum mPredefinedType = IfcBuiltSystemTypeEnum.NOTDEFINED;// : OPTIONAL IfcBuildingSystemTypeEnum;
		internal string mLongName = "$"; // 	OPTIONAL IfcLabel IFC4ADD1 

		public IfcBuiltSystemTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public string LongName { get { return (mLongName == "$" ? "" : ParserIfc.Decode(mLongName)); } set { mLongName = (string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value)); } }

		internal IfcBuiltSystem() : base() { }
		internal IfcBuiltSystem(DatabaseIfc db, IfcBuiltSystem s, DuplicateOptions options) : base(db, s, options) { mLongName = s.mLongName; mPredefinedType = s.mPredefinedType; }
		public IfcBuiltSystem(IfcSpatialElement bldg, string name,  IfcBuiltSystemTypeEnum type) : base(bldg, name) { mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcBurner : IfcEnergyConversionDevice //IFC4
	{
		internal IfcBurnerTypeEnum mPredefinedType = IfcBurnerTypeEnum.NOTDEFINED;// OPTIONAL : IfctypeEnum;
		public IfcBurnerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBurner() : base() { }
		internal IfcBurner(DatabaseIfc db, IfcBurner b, DuplicateOptions options) : base(db, b, options) { mPredefinedType = b.mPredefinedType; }
		public IfcBurner(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcBurnerType : IfcEnergyConversionDeviceType
	{
		internal IfcBurnerTypeEnum mPredefinedType = IfcBurnerTypeEnum.NOTDEFINED;// : IfcBurnerTypeEnum
		public IfcBurnerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcBurnerType() : base() { }
		internal IfcBurnerType(DatabaseIfc db, IfcBurnerType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcBurnerType(DatabaseIfc m, string name, IfcBurnerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

	}
}
