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
	public partial class IfcCableCarrierFitting : IfcFlowFitting //IFC4
	{
		internal IfcCableCarrierFittingTypeEnum mPredefinedType = IfcCableCarrierFittingTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableCarrierFittingTypeEnum;
		public IfcCableCarrierFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCableCarrierFitting() : base() { }
		internal IfcCableCarrierFitting(DatabaseIfc db, IfcCableCarrierFitting f, IfcOwnerHistory ownerHistory, bool downStream) : base(db,f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		protected IfcCableCarrierFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCableCarrierFittingType : IfcFlowFittingType
	{
		internal IfcCableCarrierFittingTypeEnum mPredefinedType = IfcCableCarrierFittingTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum; 
		public IfcCableCarrierFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCableCarrierFittingType() : base() { }
		internal IfcCableCarrierFittingType(DatabaseIfc db, IfcCableCarrierFittingType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCableCarrierFittingType(DatabaseIfc m, string name, IfcCableCarrierFittingTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcCableCarrierSegment : IfcFlowSegment //IFC4
	{
		internal IfcCableCarrierSegmentTypeEnum mPredefinedType = IfcCableCarrierSegmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableCarrierSegmentTypeEnum;
		public IfcCableCarrierSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCableCarrierSegment() : base() { }
		internal IfcCableCarrierSegment(DatabaseIfc db, IfcCableCarrierSegment s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { mPredefinedType = s.mPredefinedType; }
	}
	[Serializable]
	public partial class IfcCableCarrierSegmentType : IfcFlowSegmentType
	{
		internal IfcCableCarrierSegmentTypeEnum mPredefinedType = IfcCableCarrierSegmentTypeEnum.NOTDEFINED;// : IfcCableCarrierSegmentTypeEnum; 
		public IfcCableCarrierSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCableCarrierSegmentType() : base() { }
		internal IfcCableCarrierSegmentType(DatabaseIfc db, IfcCableCarrierSegmentType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCableCarrierSegmentType(DatabaseIfc m, string name, IfcCableCarrierSegmentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcCableFitting : IfcFlowFitting //IFC4
	{
		internal IfcCableFittingTypeEnum mPredefinedType = IfcCableFittingTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableFittingTypeEnum;
		public IfcCableFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCableFitting() : base() { }
		internal IfcCableFitting(DatabaseIfc db, IfcCableFitting f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcCableFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCableFittingType : IfcFlowFittingType
	{
		internal IfcCableFittingTypeEnum mPredefinedType = IfcCableFittingTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum; 
		public IfcCableFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCableFittingType() : base() { }
		public IfcCableFittingType(DatabaseIfc db, IfcCableFittingType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
	}
	[Serializable]
	public partial class IfcCableSegment : IfcFlowSegment //IFC4
	{
		internal IfcCableSegmentTypeEnum mPredefinedType = IfcCableSegmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableSegmentTypeEnum;
		public IfcCableSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcCableSegment() : base() { }
		internal IfcCableSegment(DatabaseIfc db, IfcCableSegment s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { mPredefinedType = s.mPredefinedType; }
	}
	[Serializable]
	public partial class IfcCableSegmentType : IfcFlowSegmentType
	{
		internal IfcCableSegmentTypeEnum mPredefinedType = IfcCableSegmentTypeEnum.NOTDEFINED;// : IfcCableSegmentTypeEnum; 
		public IfcCableSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCableSegmentType() : base() { }
		internal IfcCableSegmentType(DatabaseIfc db, IfcCableSegmentType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCableSegmentType(DatabaseIfc m, string name, IfcCableSegmentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		public IfcCableSegmentType(DatabaseIfc m, string name, IfcMaterialProfileSet mps, IfcCableSegmentTypeEnum t) : base(m) { Name = name; MaterialSelect = mps; PredefinedType = t; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcCalendarDate : BaseClassIfc, IfcDateTimeSelect //DEPRECEATED IFC4
	{
		internal int mDayComponent;//  : IfcDayInMonthNumber;
		internal int mMonthComponent;//  : IfcMonthInYearNumber;
		internal int mYearComponent;// : IfcYearNumber; 

		internal IfcCalendarDate() : base() { }
		internal IfcCalendarDate(DatabaseIfc db, IfcCalendarDate d) : base(db,d) { mDayComponent = d.mDayComponent; mMonthComponent = d.mMonthComponent; mYearComponent = d.mYearComponent; }
		public IfcCalendarDate(DatabaseIfc m, int day, int month, int year) : base(m)
		{
			if (m.mRelease != ReleaseVersion.IFC2x3) throw new Exception("IfcCalanderDate DEPRECEATED, use IfcDate");
			mDayComponent = day;
			mMonthComponent = month;
			mYearComponent = year;
		}
		public DateTime DateTime { get { return new DateTime(mYearComponent, mMonthComponent, mDayComponent); } }
	}
	[Serializable]
	public partial class IfcCartesianPoint : IfcPoint
	{
		private double mCoordinateX = 0, mCoordinateY = double.NaN, mCoordinateZ = double.NaN;

		public LIST<double> Coordinates
		{
			get
			{
				if (double.IsNaN(mCoordinateY))
					return new LIST<double>() { mCoordinateX };
				if (double.IsNaN(mCoordinateZ))
					return new LIST<double>() { mCoordinateX, mCoordinateY };
				return new LIST<double>() { mCoordinateX, mCoordinateY, mCoordinateZ };
			}
			set
			{
				mCoordinateY = mCoordinateZ = double.NaN;
				if (value == null || value.Count == 0)
					mCoordinateX = 0;
				else
				{
					mCoordinateX = value[0];
					if (value.Count > 1)
					{
						mCoordinateY = value[1];
						if(value.Count > 2)
							mCoordinateZ = value[2];
					}
				}
			}
		}
		public Tuple<double, double, double> Tuple() { return new Tuple<double, double, double>(mCoordinateX, double.IsNaN(mCoordinateY) ? 0 : mCoordinateY, double.IsNaN(mCoordinateZ) ? 0 : mCoordinateZ); }
		internal IfcCartesianPoint() : base() { }
		internal IfcCartesianPoint(DatabaseIfc db, IfcCartesianPoint p) : base(db, p) { mCoordinateX = p.mCoordinateX; mCoordinateY = p.mCoordinateY; mCoordinateZ = p.mCoordinateZ; }

		public IfcCartesianPoint(DatabaseIfc db, double x, double y) : base(db) { mCoordinateX = x; mCoordinateY = y; mCoordinateZ = double.NaN; }
		public IfcCartesianPoint(DatabaseIfc db, double x, double y, double z) : base(db) { mCoordinateX = x; mCoordinateY = y; mCoordinateZ = z; }

		internal bool is2D => double.IsNaN(mCoordinateZ);
		internal bool isOrigin { get { double tol = mDatabase.Tolerance; return ((double.IsNaN(mCoordinateX) || Math.Abs(mCoordinateX) < tol) && (double.IsNaN(mCoordinateY) || Math.Abs(mCoordinateY) < tol) && (double.IsNaN(mCoordinateZ) || Math.Abs(mCoordinateZ) < tol)); } }

		private Tuple<double, double, double> SerializeCoordinates
		{
			get
			{
				double tol = (mDatabase == null ? 1e-6 : mDatabase.Tolerance / 100);
				int digits = (mDatabase == null ? 5 : mDatabase.mLengthDigits);
				double x = Math.Round(mCoordinateX, digits), y = Math.Round(double.IsNaN(mCoordinateY) ? 0 : mCoordinateY, digits);

				if (Math.Abs(x) < tol)
					x = 0;
				if (Math.Abs(y) < tol)
					y = 0;
				if(double.IsNaN(mCoordinateZ))
					return new Tuple<double, double, double>(x, y, double.NaN);
				double z = Math.Round(mCoordinateZ, digits);
				if (Math.Abs(z) < tol)
					z = 0;
				return new Tuple<double, double, double>(x, y, z);
			}
		}
	}
	[Serializable]
	public abstract partial class IfcCartesianPointList : IfcGeometricRepresentationItem //IFC4 // SUPERTYPE OF(IfcCartesianPointList2D, IfcCartesianPointList3D)
	{
		protected IfcCartesianPointList() : base() { }
		protected IfcCartesianPointList(DatabaseIfc db) : base(db) { }
		protected IfcCartesianPointList(DatabaseIfc db, IfcCartesianPointList l) : base(db,l) { }
	}
	[Serializable]
	public partial class IfcCartesianPointList2D : IfcCartesianPointList //IFC4
	{ 
		internal double[][] mCoordList = new double[0][];//	 :	LIST [1:?] OF LIST [2:2] OF IfcLengthMeasure; 

		public double[][] CoordList
		{
			get { return mCoordList; }
			set { mCoordList = value; }
		}

		internal IfcCartesianPointList2D() : base() { }
		internal IfcCartesianPointList2D(DatabaseIfc db, IfcCartesianPointList2D l) : base(db,l) { mCoordList = l.mCoordList.ToArray(); }
		public IfcCartesianPointList2D(DatabaseIfc m, IEnumerable<Tuple<double, double>> coordList) : base(m) { mCoordList = coordList.Select(x=> new double[] { x.Item1, x.Item2 }).ToArray(); }
	}
	[Serializable]
	public partial class IfcCartesianPointList3D : IfcCartesianPointList //IFC4
	{
		private double[][] mCoordList = new double[0][];//	 :	LIST [1:?] OF LIST [3:3] OF IfcLengthMeasure; 
		public double[][] CoordList { get { return mCoordList; } set { mCoordList = value; } }
		internal IfcCartesianPointList3D() : base() { }
		internal IfcCartesianPointList3D(DatabaseIfc db, IfcCartesianPointList3D l) : base(db,l) { mCoordList = l.mCoordList.ToArray(); }

		public IfcCartesianPointList3D(DatabaseIfc m, IEnumerable<Tuple<double, double, double>> coordList) : base(m) { mCoordList = coordList.Select(x=> new double[] { x.Item1, x.Item2, x.Item3 }).ToArray(); }
	}
	[Serializable]
	public abstract partial class IfcCartesianTransformationOperator : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCartesianTransformationOperator2D ,IfcCartesianTransformationOperator3D))*/
	{ 
		protected int mAxis1;// : OPTIONAL IfcDirection
		protected int mAxis2;// : OPTIONAL IfcDirection;
		private int mLocalOrigin;// : IfcCartesianPoint;
		private double mScale = 1;// : OPTIONAL REAL;

		public IfcDirection Axis1 { get { return mDatabase[mAxis1] as IfcDirection; } set { mAxis1 = (value == null ? 0 : value.mIndex); } }
		public IfcDirection Axis2 { get { return mDatabase[mAxis2] as IfcDirection; } set { mAxis2 = (value == null ? 0 : value.mIndex); } }
		public IfcCartesianPoint LocalOrigin
		{
			get { return mDatabase[mLocalOrigin] as IfcCartesianPoint; }
			set { mLocalOrigin = value.mIndex; }
		}
		public double Scale { get { return mScale; } set { mScale = double.IsNaN( value) || Math.Abs(value) < 1e-6 ? 1 : value; } }

		protected IfcCartesianTransformationOperator() { }
		protected IfcCartesianTransformationOperator(IfcCartesianPoint p) : base(p.mDatabase) { LocalOrigin = p; }
		protected IfcCartesianTransformationOperator(DatabaseIfc db) : base(db)
		{
			LocalOrigin = db.Factory.Origin;
		}
		protected IfcCartesianTransformationOperator(DatabaseIfc db, IfcCartesianTransformationOperator o) : base(db,o) { if(o.mAxis1 > 0) Axis1 = db.Factory.Duplicate( o.Axis1) as IfcDirection; if(o.mAxis2 > 0) Axis2 = db.Factory.Duplicate( o.Axis2) as IfcDirection; LocalOrigin = db.Factory.Duplicate(o.LocalOrigin) as IfcCartesianPoint; mScale = o.mScale; }
		protected IfcCartesianTransformationOperator(IfcDirection ax1, IfcDirection ax2, IfcCartesianPoint o, double scale)
			: base(ax1 == null ? (ax2 == null ? o.mDatabase : ax2.mDatabase) : ax1.mDatabase) { if (ax1 != null) mAxis1 = ax1.mIndex; if (ax2 != null) mAxis2 = ax2.mIndex; mLocalOrigin = o.mIndex; mScale = scale; }
	}
	[Serializable]
	public partial class IfcCartesianTransformationOperator2D : IfcCartesianTransformationOperator // SUPERTYPE OF(IfcCartesianTransformationOperator2DnonUniform)
	{
		internal IfcCartesianTransformationOperator2D() : base() { }
		internal IfcCartesianTransformationOperator2D(DatabaseIfc db, IfcCartesianTransformationOperator2D o) : base(db,o) { }
		public IfcCartesianTransformationOperator2D(IfcCartesianPoint cp) : base(cp) { }
		public IfcCartesianTransformationOperator2D(DatabaseIfc db) : base(new IfcCartesianPoint(db,0,0)) { }
		public IfcCartesianTransformationOperator2D(IfcDirection ax1, IfcDirection ax2, IfcCartesianPoint o, double scale) : base(ax1,ax2,o, scale) { }
	}
	[Serializable]
	public partial class IfcCartesianTransformationOperator2DnonUniform : IfcCartesianTransformationOperator2D
	{
		private double mScale2 = double.NaN; //OPTIONAL REAL;
		public double Scale2 { get { return mScale2; } set { mScale2 = value; } }

		internal IfcCartesianTransformationOperator2DnonUniform() : base() { }
		internal IfcCartesianTransformationOperator2DnonUniform(DatabaseIfc db, IfcCartesianTransformationOperator2DnonUniform o) : base(db,o) { mScale2 = o.mScale2; }
	}
	[Serializable]
	public partial class IfcCartesianTransformationOperator3D : IfcCartesianTransformationOperator //SUPERTYPE OF(IfcCartesianTransformationOperator3DnonUniform)
	{
		private int mAxis3;// : OPTIONAL IfcDirection
		public IfcDirection Axis3 { get { return mDatabase[mAxis3] as IfcDirection; } set { mAxis3 = (value == null ? 0 : value.mIndex); } }

		internal IfcCartesianTransformationOperator3D() { }
		internal IfcCartesianTransformationOperator3D(DatabaseIfc db, IfcCartesianTransformationOperator3D o) : base(db,o) { if(o.mAxis3 > 0) Axis3 = db.Factory.Duplicate( o.Axis3) as IfcDirection; }
		public IfcCartesianTransformationOperator3D(DatabaseIfc db) : base(db) { }
		public IfcCartesianTransformationOperator3D(IfcCartesianPoint localOrigin) : base(localOrigin) { }
		
		internal IfcAxis2Placement3D generate()
		{
			if (LocalOrigin.isOrigin && (mAxis1 == 0 || Axis1.isXAxis) && (mAxis2 == 0 || Axis2.isYAxis))
				return mDatabase.Factory.XYPlanePlacement;
			return new IfcAxis2Placement3D(LocalOrigin, mAxis3 == 0 ? mDatabase.Factory.ZAxis : Axis3, mAxis1 == 0 ? mDatabase.Factory.XAxis : Axis1);
		}
	}
	[Serializable]
	public partial class IfcCartesianTransformationOperator3DnonUniform : IfcCartesianTransformationOperator3D
	{
		private double mScale2 = 1;// : OPTIONAL REAL;
		private double mScale3 = 1;// : OPTIONAL REAL; 

		public double Scale2 { get { return mScale2; } set { mScale2 = value; } }
		public double Scale3 { get { return mScale3; } set { mScale3 = value; } }

		internal IfcCartesianTransformationOperator3DnonUniform() { }
		internal IfcCartesianTransformationOperator3DnonUniform(DatabaseIfc db, IfcCartesianTransformationOperator3DnonUniform o) : base(db,o) { mScale2 = o.mScale2; mScale3 = o.mScale3; }
	}
	[Serializable]
	public partial class IfcCenterLineProfileDef : IfcArbitraryOpenProfileDef
	{
		internal double mThickness;// : IfcPositiveLengthMeasure;
		public double Thickness { get { return mThickness; } set { mThickness = value; } }

		internal IfcCenterLineProfileDef() : base() { }
		internal IfcCenterLineProfileDef(DatabaseIfc db, IfcCenterLineProfileDef p) : base(db, p) { mThickness = p.mThickness; }
		public IfcCenterLineProfileDef(string name, IfcBoundedCurve curve, double thickness) : base(name, curve) { mThickness = thickness; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcChamferEdgeFeature : IfcEdgeFeature //DEPRECEATED IFC4
	{
		internal double mWidth;// : OPTIONAL IfcPositiveLengthMeasure
		internal double mHeight;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcChamferEdgeFeature() : base() { }
		internal IfcChamferEdgeFeature(DatabaseIfc db, IfcChamferEdgeFeature f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mWidth = f.mWidth; mHeight = f.mHeight; }
	}
	[Serializable]
	public partial class IfcChiller : IfcEnergyConversionDevice //IFC4
	{
		internal IfcChillerTypeEnum mPredefinedType = IfcChillerTypeEnum.NOTDEFINED;// OPTIONAL : IfctypeEnum;
		public IfcChillerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcChiller() : base() { }
		internal IfcChiller(DatabaseIfc db, IfcChiller c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { mPredefinedType = c.mPredefinedType; }
		public IfcChiller(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcChillerType : IfcEnergyConversionDeviceType
	{
		internal IfcChillerTypeEnum mPredefinedType = IfcChillerTypeEnum.NOTDEFINED;// : IfcChillerTypeEnum
		public IfcChillerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcChillerType() : base() { }
		internal IfcChillerType(DatabaseIfc db, IfcChillerType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcChillerType(DatabaseIfc db, string name, IfcChillerTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcChimney : IfcBuildingElement
	{
		internal IfcChimneyTypeEnum mPredefinedType = IfcChimneyTypeEnum.NOTDEFINED;//: OPTIONAL IfcChimneyTypeEnum; 
		public IfcChimneyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcChimney() : base() { }
		internal IfcChimney(DatabaseIfc db, IfcChimney c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { mPredefinedType = c.mPredefinedType; }
		public IfcChimney(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcChimneyType : IfcBuildingElementType
	{
		internal IfcChimneyTypeEnum mPredefinedType = IfcChimneyTypeEnum.NOTDEFINED;
		public IfcChimneyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcChimneyType() : base() { } 
		internal IfcChimneyType(DatabaseIfc db, IfcChimneyType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcChimneyType(DatabaseIfc m, string name, IfcChimneyTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcCircle : IfcConic
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		public double Radius { get { return mRadius; } set { mRadius = value; } }

		internal IfcCircle() : base() { }
		internal IfcCircle(DatabaseIfc db, IfcCircle c) : base(db,c) { mRadius = c.mRadius; }
		public IfcCircle(DatabaseIfc db, double radius) : base(db.Factory.Origin2dPlace) { mRadius = radius; }
		public IfcCircle(IfcAxis2Placement ap, double radius) : base(ap) { mRadius = radius; }
	}
	[Serializable]
	public partial class IfcCircleHollowProfileDef : IfcCircleProfileDef
	{
		internal override string KeyWord { get { return (mWallThickness < (mDatabase == null ? 1e-6 : mDatabase.Tolerance) ? "IfcCircleProfileDef" : base.KeyWord); } }

		internal double mWallThickness;// : IfcPositiveLengthMeasure;
		public double WallThickness { get { return mWallThickness; } set { mWallThickness = value; } }

		internal IfcCircleHollowProfileDef() : base() { }
		internal IfcCircleHollowProfileDef(DatabaseIfc db, IfcCircleHollowProfileDef c) : base(db, c) { mWallThickness = c.mWallThickness; }
		public IfcCircleHollowProfileDef(DatabaseIfc m, string name, double radius, double wallThickness) : base(m, name, radius) { mWallThickness = wallThickness; }
	}
	[Serializable]
	public partial class IfcCircleProfileDef : IfcParameterizedProfileDef //SUPERTYPE OF(IfcCircleHollowProfileDef)
	{
		internal double mRadius;// : IfcPositiveLengthMeasure;		
		public double Radius { get { return mRadius; } set { mRadius = value; } }
		internal IfcCircleProfileDef() : base() { }
		internal IfcCircleProfileDef(DatabaseIfc db, IfcCircleProfileDef c) : base(db, c) { mRadius = c.mRadius; }
		public IfcCircleProfileDef(DatabaseIfc db, string name, double radius) : base(db,name) { mRadius = radius; }
	}
	[Serializable]
	public partial class IfcCircularArcSegment2D : IfcCurveSegment2D  //IFC4.1
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		private bool mIsCCW;// : IfcBoolean;

		public double Radius { get { return mRadius; } set { mRadius = value; } }
		public bool IsCCW { get { return mIsCCW; } set { mIsCCW = value; } }

		internal IfcCircularArcSegment2D() : base() { }
		internal IfcCircularArcSegment2D(DatabaseIfc db, IfcCircularArcSegment2D s) : base(db, s) { mRadius = s.mRadius; mIsCCW = s.mIsCCW; }
		public IfcCircularArcSegment2D(IfcCartesianPoint start, double startDirection, double length, double radius, bool isCCW)
			: base(start, startDirection, length)
		{
			mRadius = radius;
			mIsCCW = isCCW;
		}
	}
	[Serializable]
	public partial class IfcCivilElement : IfcElement  //IFC4
	{
		internal IfcCivilElement() : base() { }
		internal IfcCivilElement(DatabaseIfc db, IfcCivilElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { }
		public IfcCivilElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { if (mDatabase.mRelease < ReleaseVersion.IFC4) throw new Exception(KeyWord + " only supported in IFC4!"); }
	}
	[Serializable]
	public abstract partial class IfcCivilElementPart : IfcElementComponent //	ABSTRACT SUPERTYPE OF(ONEOF(IfcBridgeSegmentPart , IfcBridgeContactElement , IfcCivilSheath , IfcCivilVoid))
	{
		// INVERSE
		//ContainedInSegment : IfcBridgeSegment FOR SegmentParts;
		protected IfcCivilElementPart() : base() { }
		protected IfcCivilElementPart(DatabaseIfc db, IfcCivilElementPart e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { }
		protected IfcCivilElementPart(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcCivilElementType : IfcElementType //IFC4
	{
		internal IfcCivilElementType() : base() { }
		internal IfcCivilElementType(DatabaseIfc db, IfcCivilElementType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
		public IfcCivilElementType(DatabaseIfc m, string name) : base(m) { Name = name; if (m.mRelease < ReleaseVersion.IFC4) throw new Exception(KeyWord + " only supported in IFC4!"); }
	}
	[Serializable]
	public partial class IfcCivilSheath : IfcCivilElementPart //IFC5
	{
		internal IfcCivilSheath() : base() { }
		internal IfcCivilSheath(DatabaseIfc db, IfcCivilSheath s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { }
		public IfcCivilSheath(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcCivilStructureElement : IfcSpatialStructureElement //IFC5
	{
		protected IfcCivilStructureElement() : base() { }
		protected IfcCivilStructureElement(IfcSpatialStructureElement host, string name) : base(host, name) { }
		protected IfcCivilStructureElement(DatabaseIfc db, IfcCivilStructureElement p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcCivilVoid : IfcCivilElementPart //IFC5
	{
		internal IfcCivilVoid() : base() { }
		internal IfcCivilVoid(DatabaseIfc db, IfcCivilVoid v, IfcOwnerHistory ownerHistory, bool downStream) : base(db, v, ownerHistory, downStream) { }
		public IfcCivilVoid(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcClassification : IfcExternalInformation, IfcClassificationReferenceSelect, IfcClassificationSelect, NamedObjectIfc //	SUBTYPE OF IfcExternalInformation;
	{
		internal string mSource = "$"; //  : OPTIONAL IfcLabel;
		internal string mEdition = "$"; //  : OPTIONAL IfcLabel;
		internal DateTime mEditionDate = DateTime.MinValue; // : OPTIONAL IfcDate IFC4 change 
		private int mEditionDateSS = 0; // : OPTIONAL IfcCalendarDate;
		internal string mName;//  : IfcLabel;
		internal string mDescription = "$";//	 :	OPTIONAL IfcText; IFC4 Addition
		internal string mLocation = "$";//	 :	OPTIONAL IfcURIReference; IFC4 Addition
		internal List<string> mReferenceTokens = new List<string>();//	 :	OPTIONAL LIST [1:?] OF IfcIdentifier; IFC4 Addition
		//INVERSE 
		internal SET<IfcRelAssociatesClassification> mClassificationForObjects = new SET<IfcRelAssociatesClassification>();//	 :	SET OF IfcRelAssociatesclassification FOR Relatingclassification;
		internal SET<IfcClassificationReference> mHasReferences = new SET<IfcClassificationReference>();//	 :	SET OF IfcClassificationReference FOR ReferencedSource;

		public string Source { get { return (mSource == "$" ? "" : ParserIfc.Decode(mSource)); } set { mSource = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Edition { get { return (mEdition == "$" ? "" : ParserIfc.Decode(mEdition)); } set { mEdition = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public DateTime EditionDate
		{
			get { return (mEditionDateSS > 0 ? (mDatabase[mEditionDateSS] as IfcCalendarDate).DateTime : mEditionDate); }
			set
			{
				if (mDatabase.Release <= ReleaseVersion.IFC2x3)
				{
					if (value > DateTime.MinValue)
						mEditionDateSS = new IfcCalendarDate(mDatabase, value.Day, value.Month, value.Year).mIndex;
				}
				else
					mEditionDate = value;
			}
		}
		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { if (!string.IsNullOrEmpty(value)) mName = ParserIfc.Encode(value); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Location { get { return (mLocation == "$" ? "" : ParserIfc.Decode(mLocation)); } set { mLocation = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public List<string> ReferenceTokens { get { return mReferenceTokens.ConvertAll(x => ParserIfc.Decode(x)); } }
		public SET<IfcRelAssociatesClassification> ClassificationForObjects { get { return mClassificationForObjects; } }
		public SET<IfcClassificationReference> HasReferences { get { return mHasReferences; } }

		internal IfcClassification() : base() { }
		internal IfcClassification(DatabaseIfc db, IfcClassification c) : base(db, c) { mSource = c.mSource; mEdition = c.mEdition; mEditionDate = c.mEditionDate; mName = c.mName; }
		public IfcClassification(DatabaseIfc db, string name) : base(db) { Name = name; }
		protected override void initialize()
		{
			base.initialize();
			mHasReferences.CollectionChanged += mHasReferences_CollectionChanged;
		}
		private void mHasReferences_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcClassificationReference r in e.NewItems)
				{
					if (r.mReferencedSource != this)
						r.mReferencedSource = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcClassificationReference r in e.OldItems)
				{
					r.mReferencedSource = null;
				}
			}
		}
		public void Associate(IfcDefinitionSelect related)
		{
			if (mClassificationForObjects.Count == 0)
				new IfcRelAssociatesClassification(this, related);
			else
				mClassificationForObjects[0].RelatedObjects.Add(related);
		}

		public IfcClassificationReference FindItem(string identification)
		{
			foreach (IfcClassificationReference classificationReference in HasReferences)
			{
				IfcClassificationReference result = classificationReference.FindItem(identification);
				if (result != null)
					return result;
			}
			return null;
		}
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcClassificationItem : BaseClassIfc //DEPRECEATED IFC4
	{
		internal int mNotation;// : IfcClassificationNotationFacet;
		internal int mItemOf;//: OPTIONAL IfcClassification;
		internal string mTitle;// : IfcLabel; 

		internal IfcClassificationItem() : base() { }
		//internal IfcClassificationItem( IfcClassificationItem i) : base() { mNotation = i.mNotation; mItemOf = i.mItemOf; mTitle = i.mTitle; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcClassificationItemRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		internal string mSource;// : IfcLabel;
		internal string mEdition;// : IfcLabel;
		internal int mEditionDate;// : OPTIONAL IfcCalendarDate;
		internal string mName;// : IfcLabel;
		internal IfcClassificationItemRelationship() : base() { }
		//internal IfcClassificationItemRelationship(IfcClassificationItemRelationship i) : base()
		//{
		//	mSource = i.mSource;
		//	mEdition = i.mEdition;
		//	mEditionDate = i.mEditionDate;
		//	mName = i.mName;
		//}
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcClassificationNotation : BaseClassIfc, IfcClassificationNotationSelect //DEPRECEATED IFC4
	{
		internal List<int> mNotationFacets = new List<int>();// : SET [1:?] OF IfcClassificationNotationFacet;

		internal IfcClassificationNotation() : base() { }
		//internal IfcClassificationNotation(IfcClassificationNotation i) : base() { mNotationFacets = new List<int>(i.mNotationFacets.ToArray()); }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcClassificationNotationFacet : BaseClassIfc  //DEPRECEATED IFC4
	{
		internal string mNotationValue;//  : IfcLabel;
		internal IfcClassificationNotationFacet() : base() { }
	//	internal IfcClassificationNotationFacet(IfcClassification i) : base() { mNotationValue = i.mSource; }
	}
	public interface IfcClassificationNotationSelect : IBaseClassIfc { } // List<IfcRelAssociatesclassification> classificationForObjects { get; } 	IfcClassificationNotation, IfcClassificationReference) 
	[Serializable]
	public partial class IfcClassificationReference : IfcExternalReference, IfcClassificationReferenceSelect, IfcClassificationSelect, IfcClassificationNotationSelect
	{
		internal IfcClassificationReferenceSelect mReferencedSource = null;// : OPTIONAL IfcClassificationReferenceSelect; //IFC2x3 : 	OPTIONAL IfcClassification;
		internal string mDescription = "$";// :	OPTIONAL IfcText; IFC4
		internal string mSort = "$";//	 :	OPTIONAL IfcIdentifier;
		//INVERSE
		internal SET<IfcRelAssociatesClassification> mClassificationRefForObjects = new SET<IfcRelAssociatesClassification>();//	 :	SET [0:?] OF IfcRelAssociatesclassification FOR Relatingclassification;
		internal SET<IfcClassificationReference> mHasReferences = new SET<IfcClassificationReference>();//	 :	SET [0:?] OF IfcClassificationReference FOR ReferencedSource;

		public IfcClassificationReferenceSelect ReferencedSource
		{
			get { return mReferencedSource; }
			set
			{
				mReferencedSource = value;
				if (value == null)
					return;
				if (mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4)
				{
					IfcClassificationReference classificationReference = value as IfcClassificationReference;
					if (classificationReference != null)
						mReferencedSource = classificationReference.ReferencedClassification();
				}
				if (mReferencedSource != null && !mReferencedSource.HasReferences.Contains(this))
					mReferencedSource.HasReferences.Add(this);
			}
		}
		public string Description
		{
			get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); }
			set
			{
				mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value));
				if (mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4 && string.IsNullOrEmpty(Name))
					Name = value;
			}
		}
		public string Sort { get { return (mSort == "$" ? "" : ParserIfc.Decode(mSort)); } set { mSort = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public SET<IfcRelAssociatesClassification> ClassificationRefForObjects { get { return mClassificationRefForObjects; } }
		public SET<IfcClassificationReference> HasReferences { get { return mHasReferences; } }

		internal IfcClassificationReference() : base() { }
		internal IfcClassificationReference(DatabaseIfc db, IfcClassificationReference r) : base(db, r)
		{
			if(db.Release < ReleaseVersion.IFC4)
			{
				IfcClassification classification =	r.ReferencedClassification();
				if (classification != null)
					ReferencedSource = db.Factory.Duplicate(classification) as IfcClassification;
			}
			else
				ReferencedSource = db.Factory.Duplicate(r.ReferencedSource) as IfcClassificationReferenceSelect;
			mDescription = r.mDescription;
			mSort = r.mSort;
		}
		public IfcClassificationReference(DatabaseIfc db) : base(db) {  }
		public IfcClassificationReference(IfcClassificationReferenceSelect referencedSource) : base(referencedSource.Database) { ReferencedSource = referencedSource; }

		protected override void initialize()
		{
			base.initialize();

			mHasReferences.CollectionChanged += mHasReferences_CollectionChanged;
		}
		private void mHasReferences_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcClassificationReference r in e.NewItems)
				{
					if (r.mReferencedSource != this)
						r.mReferencedSource = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcClassificationReference r in e.OldItems)
				{
					r.mReferencedSource = null;
				}	
			}
		}
		public void Associate(IfcDefinitionSelect related)
		{
			if (mClassificationRefForObjects.Count == 0)
				new IfcRelAssociatesClassification(this, related);
			else if (!mClassificationRefForObjects[0].RelatedObjects.Contains(related))
				mClassificationRefForObjects[0].RelatedObjects.Add(related);
		}
		public IfcClassificationReference FindItem(string identification)
		{
			if (string.Compare(identification, Identification, true) == 0)
				return this;
			foreach (IfcClassificationReference classificationReference in HasReferences)
			{
				IfcClassificationReference result = classificationReference.FindItem(identification);
				if (result != null)
					return result;
			}
			return null;
		}
		public IfcClassification ReferencedClassification()
		{
			IfcClassificationReferenceSelect classificationReferenceSelect = ReferencedSource;
			IfcClassificationReference classificationReference = ReferencedSource as IfcClassificationReference;
			if (classificationReference != null)
				return classificationReference.ReferencedClassification();
			return classificationReferenceSelect as IfcClassification; 
		}
	}
	public interface IfcClassificationReferenceSelect : IBaseClassIfc { SET<IfcClassificationReference> HasReferences { get; } } // SELECT ( IfcClassificationReference, IfcClassification);
	public interface IfcClassificationSelect : NamedObjectIfc { } // IFC4 rename IfcClassification,IfcClassificationReference 
	[Serializable]
	public partial class IfcClosedShell : IfcConnectedFaceSet, IfcShell
	{
		internal IfcClosedShell() : base() { }
		internal IfcClosedShell(DatabaseIfc db, IfcClosedShell c) : base(db,c) { }
		public IfcClosedShell(IEnumerable<IfcFace> faces) : base(faces) { }
	}
	[Serializable]
	public partial class IfcCoil : IfcEnergyConversionDevice //IFC4
	{
		internal IfcCoilTypeEnum mPredefinedType = IfcCoilTypeEnum.NOTDEFINED;// OPTIONAL : IfcCoilTypeEnum;
		public IfcCoilTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCoil() : base() { }
		internal IfcCoil(DatabaseIfc db, IfcCoil c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { mPredefinedType = c.mPredefinedType; }
		public IfcCoil(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCoilType : IfcEnergyConversionDeviceType
	{
		internal IfcCoilTypeEnum mPredefinedType = IfcCoilTypeEnum.NOTDEFINED;// : IfcCoilTypeEnum;
		public IfcCoilTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCoilType() : base() { }
		internal IfcCoilType(DatabaseIfc db, IfcCoilType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCoilType(DatabaseIfc m, string name, IfcCoilTypeEnum t) : base(m) { Name = name; PredefinedType = t; }
	}
	public partial interface IfcColour : IBaseClassIfc, IfcFillStyleSelect { }// = SELECT (IfcColourSpecification ,IfcPreDefinedColour); 
	public partial interface IfcColourOrFactor { } // IfcNormalisedRatioMeasure, IfcColourRgb);
	[Serializable]
	public partial class IfcColourRgb : IfcColourSpecification, IfcColourOrFactor
	{
		internal double mRed, mGreen, mBlue;// : IfcNormalisedRatioMeasure; 
		
		internal IfcColourRgb() : base() { }
		internal IfcColourRgb(DatabaseIfc db, IfcColourRgb c) : base(db, c) { mRed = c.mRed; mGreen = c.mGreen; mBlue = c.mBlue; }
		public IfcColourRgb(DatabaseIfc db, double red, double green, double blue) : base(db) { mRed = red; mGreen = green; mBlue = blue; }		
	}
	[Serializable]
	public partial class IfcColourRgbList : IfcPresentationItem
	{
		internal double[][] mColourList = new double[0][];//	: LIST [1:?] OF LIST [3:3] OF IfcNormalisedRatioMeasure; 
		internal IfcColourRgbList() : base() { }
		internal IfcColourRgbList(DatabaseIfc db,IfcColourRgbList l) : base(db,l) { mColourList = l.mColourList; }
	}
	[Serializable]
	public abstract partial class IfcColourSpecification : IfcPresentationItem, IfcColour, NamedObjectIfc //	ABSTRACT SUPERTYPE OF(IfcColourRgb)
	{
		private string mName = "$";// : OPTIONAL IfcLabel
		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { if (!string.IsNullOrEmpty(value)) mName = ParserIfc.Encode(value); } }

		protected IfcColourSpecification() : base() { }
		protected IfcColourSpecification(DatabaseIfc db, IfcColourSpecification s) : base(db, s) { mName = s.mName; }
		protected IfcColourSpecification(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcColumn : IfcBuildingElement
	{
		internal IfcColumnTypeEnum mPredefinedType = IfcColumnTypeEnum.NOTDEFINED;//: OPTIONAL IfcColumnTypeEnum; 
		public IfcColumnTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcColumn() : base()  { }
		internal IfcColumn(DatabaseIfc db, IfcColumn c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { mPredefinedType = c.mPredefinedType; }
		public IfcColumn(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		public IfcColumn(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double height) : base(host, profile, placement, height) { }
	}
	[Serializable]
	public partial class IfcColumnStandardCase : IfcColumn
	{
		internal override string KeyWord { get { return "IfcColumn"; } }
		internal IfcColumnStandardCase() : base() { }
		internal IfcColumnStandardCase(DatabaseIfc db, IfcColumnStandardCase c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { }
		public IfcColumnStandardCase(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double height) : base(host, profile, placement, height) { }
	} 
	[Serializable]
	public partial class IfcColumnType : IfcBuildingElementType
	{
		internal IfcColumnTypeEnum mPredefinedType = IfcColumnTypeEnum.NOTDEFINED;
		public IfcColumnTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcColumnType() : base() { }
		internal IfcColumnType(DatabaseIfc db, IfcColumnType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcColumnType(DatabaseIfc m, string name, IfcColumnTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcColumnType(string name, IfcMaterialProfile ps, IfcColumnTypeEnum type) : this(name,new IfcMaterialProfileSet(ps.Name, ps), type) { }
		public IfcColumnType(string name, IfcMaterialProfileSet ps, IfcColumnTypeEnum type) : base(ps.mDatabase)
		{
			Name = name;
			mPredefinedType = type;
			if (ps.mTaperEnd != null)
				mTapering = ps;
			else
				MaterialSelect = ps;
		}
	}
	[Serializable]
	public partial class IfcComplexProperty : IfcProperty
	{
		internal override string KeyWord { get { return "IfcComplexProperty"; } }
		internal string mUsageName;// : IfcIdentifier;
		private Dictionary<string, IfcProperty> mHasProperties = new Dictionary<string, IfcProperty>();// : SET [1:?] OF IfcProperty;
		private List<int> mPropertyIndices = new List<int>();

		public string UsageName { get { return mUsageName; } set { mUsageName = (string.IsNullOrEmpty(value) ? "Unknown" : value); } }
		public ReadOnlyDictionary<string, IfcProperty> HasProperties { get { return new ReadOnlyDictionary<string, IfcProperty>(mHasProperties); } }

		internal IfcComplexProperty() : base() { }
		internal IfcComplexProperty(DatabaseIfc db, IfcComplexProperty p) : base(db, p) { mUsageName = p.mUsageName; foreach(IfcProperty prop in p.HasProperties.Values) addProperty( db.Factory.Duplicate(prop) as IfcProperty); }
		public IfcComplexProperty(DatabaseIfc m, string name, string usageName) : base(m, name) { UsageName = usageName; }
		public IfcComplexProperty(DatabaseIfc m, string name, string usageName, IEnumerable<IfcProperty> properties) : this(m, name, usageName) { foreach (IfcProperty p in properties) addProperty(p); }
		
		public IfcPropertyBoundedValue AddProperty(IfcPropertyBoundedValue property) { addProperty(property); return property; }
		public IfcPropertyEnumeratedValue AddProperty(IfcPropertyEnumeratedValue property) { addProperty(property); return property; }
		public IfcPropertyReferenceValue AddProperty(IfcPropertyReferenceValue property) { addProperty(property); return property; }
		public IfcPropertySingleValue AddProperty(IfcPropertySingleValue property) { addProperty(property); return property; }
		public IfcPropertyTableValue AddProperty(IfcPropertyTableValue property) { addProperty(property); return property; }
		public IfcComplexProperty AddProperty(IfcComplexProperty property) { addProperty(property); return property; }
		internal void addProperty(IfcProperty property)
		{
			IfcProperty existing = null;
			if (mHasProperties.TryGetValue(property.Name, out existing))
				RemoveProperty(existing);
			if (property != null)
			{
				mHasProperties[property.Name] = property;
				property.mPartOfComplex.Add(this);
				if(!mPropertyIndices.Contains(property.Index))
					mPropertyIndices.Add(property.mIndex);
			}
		}
		public void RemoveProperty(IfcProperty property)
		{
			if (property != null)
			{
				mHasProperties.Remove(property.Name);
				mPropertyIndices.Remove(property.mIndex);
				property.mPartOfComplex.Remove(this);
			}
		}
		public IfcProperty this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
					return null;
				IfcProperty result = null;
				mHasProperties.TryGetValue(name, out result);
				return result;
			}
		}
	}
	[Serializable]
	public partial class IfcComplexPropertyTemplate : IfcPropertyTemplate
	{
		private string mUsageName = "";// : OPTIONAL IfcLabel;
		private IfcComplexPropertyTemplateTypeEnum mTemplateType = IfcComplexPropertyTemplateTypeEnum.NOTDEFINED;// : OPTIONAL IfcComplexPropertyTemplateTypeEnum;
		private List<int> mHasPropertyTemplateIndices = new List<int>(1);//
		private Dictionary<string, IfcPropertyTemplate> mHasPropertyTemplates = new Dictionary<string, IfcPropertyTemplate>();//  : SET [1:?] OF IfcPropertyTemplate;

		public string UsageName { get { return mUsageName; } set { mUsageName = value; } }
		public IfcComplexPropertyTemplateTypeEnum TemplateType { get { return mTemplateType; } set { mTemplateType = value; } }
		public ReadOnlyDictionary<string,IfcPropertyTemplate> HasPropertyTemplates { get { return new ReadOnlyDictionary<string, IfcPropertyTemplate>(mHasPropertyTemplates); } }

		internal IfcComplexPropertyTemplate() : base() { }
		public IfcComplexPropertyTemplate(DatabaseIfc db, IfcComplexPropertyTemplate t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream)
		{
			mUsageName = t.mUsageName;
			mTemplateType = t.mTemplateType;
			if(downStream)
				t.HasPropertyTemplates.Values.ToList().ForEach(x => AddPropertyTemplate(db.Factory.Duplicate(x) as IfcPropertyTemplate));
		}
		public IfcComplexPropertyTemplate(DatabaseIfc db, string name) : base(db, name) { }

		public void AddPropertyTemplate(IfcPropertyTemplate template) { mHasPropertyTemplateIndices.Add(template.mIndex); mHasPropertyTemplates.Add(template.Name, template); template.mPartOfComplexTemplate.Add(this); }
		public IfcPropertyTemplate this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
					return null;
				IfcPropertyTemplate result = null;
				mHasPropertyTemplates.TryGetValue(name, out result);
				return result;
			}
		}
		public void Remove(string templateName) { IfcPropertyTemplate template = this[templateName]; if (template != null) { template.mPartOfComplexTemplate.Remove(this); mHasPropertyTemplateIndices.Remove(template.Index); mHasPropertyTemplates.Remove(templateName); } }
	}
	[Serializable]
	public partial class IfcCompositeCurve : IfcBoundedCurve
	{
		private List<int> mSegments = new List<int>();// : LIST [1:?] OF IfcCompositeCurveSegment;
		private IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// : LOGICAL;

		public ReadOnlyCollection<IfcCompositeCurveSegment> Segments { get { return new ReadOnlyCollection<IfcCompositeCurveSegment>(mSegments.ConvertAll(x => mDatabase[x] as IfcCompositeCurveSegment)); } }
		public IfcLogicalEnum SelfIntersect { get { return mSelfIntersect; } set { mSelfIntersect = value; } }

		internal IfcCompositeCurve() : base() { }
		internal IfcCompositeCurve(DatabaseIfc db, IfcCompositeCurve c) : base(db,c) { c.Segments.ToList().ForEach(x=>addSegment( db.Factory.Duplicate(x) as IfcCompositeCurveSegment)); mSelfIntersect = c.mSelfIntersect; }
		public IfcCompositeCurve(List<IfcCompositeCurveSegment> segs) : base(segs[0].mDatabase) { mSegments = segs.ConvertAll(x => x.mIndex); }
		
		internal void addSegment(IfcCompositeCurveSegment segment) { mSegments.Add(segment.mIndex); }
		internal void setSegments(IEnumerable<IfcCompositeCurveSegment> segments) { mSegments.Clear(); foreach (IfcCompositeCurveSegment segment in segments) addSegment(segment); }
	}
	[Serializable]
	public partial class Ifc2dCompositeCurve : IfcCompositeCurve
	{
		internal Ifc2dCompositeCurve() : base() { }
		internal Ifc2dCompositeCurve(DatabaseIfc db, Ifc2dCompositeCurve c) : base(db,c) { }
	}
	[Serializable]
	public partial class IfcCompositeCurveOnSurface : IfcCompositeCurve
	{
		internal int mBasisSurface;// : IfcSurface;
		public IfcSurface BasisSurface { get { return mDatabase[mBasisSurface] as IfcSurface; } set { mBasisSurface = value.mIndex; } }

		internal IfcCompositeCurveOnSurface() : base() { }
		internal IfcCompositeCurveOnSurface(DatabaseIfc db, IfcCompositeCurveOnSurface c) : base(db,c) { BasisSurface = db.Factory.Duplicate(c.BasisSurface) as IfcSurface; }
		public IfcCompositeCurveOnSurface(List<IfcCompositeCurveSegment> segs,IfcSurface surface) : base(segs) { BasisSurface = surface; }
	}
	[Serializable]
	public partial class IfcCompositeCurveSegment : IfcGeometricRepresentationItem
	{
		private IfcTransitionCode mTransition;// : IfcTransitionCode;
		private bool mSameSense;// : BOOLEAN;
		private int mParentCurve;// : IfcCurve;  Really IfcBoundedCurve WR1

		public IfcTransitionCode Transition { get { return mTransition; } }
		public bool SameSense { get { return mSameSense; } set { mSameSense = value; } }
		public IfcBoundedCurve ParentCurve { get { return mDatabase[mParentCurve] as IfcBoundedCurve; } set { mParentCurve = value.mIndex; } }

		internal IfcCompositeCurveSegment() : base() { }
		internal IfcCompositeCurveSegment(DatabaseIfc db, IfcCompositeCurveSegment s) : base(db,s) { mTransition = s.mTransition; mSameSense = s.mSameSense; ParentCurve = db.Factory.Duplicate(s.ParentCurve) as IfcBoundedCurve; }
		public IfcCompositeCurveSegment(IfcTransitionCode tc, bool sense, IfcBoundedCurve bc) : base(bc.mDatabase) { mSameSense = sense; mTransition = tc; ParentCurve = bc; }
	}
	[Serializable]
	public partial class IfcCompositeProfileDef : IfcProfileDef
	{
		private List<int> mProfiles = new List<int>();// : SET [2:?] OF IfcProfileDef;
		private string mLabel = "$";// : OPTIONAL IfcLabel;

		public ReadOnlyCollection<IfcProfileDef> Profiles { get { return new ReadOnlyCollection<IfcProfileDef>( mProfiles.ConvertAll(x => mDatabase[x] as IfcProfileDef)); } }
		public string Label { get { return (mLabel == "$" ? "" : ParserIfc.Decode(mLabel)); } set { mLabel = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcCompositeProfileDef() : base() { }
		internal IfcCompositeProfileDef(DatabaseIfc db, IfcCompositeProfileDef p) : base(db, p)
		{
			p.Profiles.ToList().ForEach(x => addProfile(db.Factory.Duplicate(x) as IfcProfileDef));
			mLabel = p.mLabel;
		}
		private IfcCompositeProfileDef(DatabaseIfc m, string name) : base(m,name)
		{
			if (mDatabase.mModelView == ModelView.Ifc4Reference)
				throw new Exception("Invalid Model View for IfcCompositeProfileDef : " + m.ModelView.ToString());
		}
		public IfcCompositeProfileDef(string name, List<IfcProfileDef> defs) : this(defs[0].mDatabase, name) { mProfiles = defs.ConvertAll(x => x.mIndex); }
		public IfcCompositeProfileDef(string name, IfcProfileDef p1, IfcProfileDef p2) : this(p1.mDatabase, name) { mProfiles.Add(p1.mIndex); mProfiles.Add(p2.mIndex); }

		internal void addProfile(IfcProfileDef profile) { mProfiles.Add(profile.mIndex); }
	}
	[Serializable]
	public partial class IfcCompressor : IfcFlowMovingDevice //IFC4
	{
		internal IfcCompressorTypeEnum mPredefinedType = IfcCompressorTypeEnum.NOTDEFINED;// OPTIONAL : IfcCompressorTypeEnum;
		public IfcCompressorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCompressor() : base() { }
		internal IfcCompressor(DatabaseIfc db, IfcCompressor c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { mPredefinedType = c.mPredefinedType; }
		public IfcCompressor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCompressorType : IfcFlowMovingDeviceType
	{
		internal IfcCompressorTypeEnum mPredefinedType = IfcCompressorTypeEnum.NOTDEFINED;// : IfcCompressorTypeEnum;
		public IfcCompressorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCompressorType() : base() { }
		internal IfcCompressorType(DatabaseIfc db, IfcCompressorType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCompressorType(DatabaseIfc m, string name, IfcCompressorTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcCommunicationsAppliance : IfcFlowTerminal //IFC4
	{
		internal IfcCommunicationsApplianceTypeEnum mPredefinedType = IfcCommunicationsApplianceTypeEnum.NOTDEFINED;// OPTIONAL : IfcCommunicationsApplianceTypeEnum;
		public IfcCommunicationsApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCommunicationsAppliance() : base() { }
		internal IfcCommunicationsAppliance(DatabaseIfc db, IfcCommunicationsAppliance a, IfcOwnerHistory ownerHistory, bool downStream) : base(db,a, ownerHistory, downStream) { mPredefinedType = a.mPredefinedType; }
	}
	[Serializable]
	public partial class IfcCommunicationsApplianceType : IfcFlowTerminalType
	{
		internal IfcCommunicationsApplianceTypeEnum mPredefinedType = IfcCommunicationsApplianceTypeEnum.NOTDEFINED;// : IfcCommunicationsApplianceBoxTypeEnum; 
		public IfcCommunicationsApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCommunicationsApplianceType() : base() { }
		internal IfcCommunicationsApplianceType(DatabaseIfc db, IfcCommunicationsApplianceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCommunicationsApplianceType(DatabaseIfc m, string name, IfcCommunicationsApplianceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcCondenser : IfcEnergyConversionDevice //IFC4
	{
		internal IfcCondenserTypeEnum mPredefinedType = IfcCondenserTypeEnum.NOTDEFINED;// OPTIONAL : IfcCCondenserTypeEnum;
		public IfcCondenserTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCondenser() : base() { }
		internal IfcCondenser(DatabaseIfc db, IfcCondenser c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { mPredefinedType = c.mPredefinedType; }
		public IfcCondenser(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCondenserType : IfcEnergyConversionDeviceType
	{
		internal IfcCondenserTypeEnum mPredefinedType = IfcCondenserTypeEnum.NOTDEFINED;// : IfcCondenserTypeEnum;
		public IfcCondenserTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCondenserType() : base() { }
		internal IfcCondenserType(DatabaseIfc db, IfcCondenserType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCondenserType(DatabaseIfc m, string name, IfcCondenserTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcCondition : IfcGroup //DEPRECEATED IFC4
	{
		internal IfcCondition() : base() { }
		internal IfcCondition(DatabaseIfc db, IfcCondition c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcConditionCriterion : IfcControl //DEPRECEATED IFC4
	{
		internal int mCriterion;// : IfcConditionCriterionSelect;
		internal int mCriterionDateTime;// : IfcDateTimeSelect; 
		internal IfcConditionCriterion() : base() { }
		internal IfcConditionCriterion(DatabaseIfc db, IfcConditionCriterion c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { } // mCriterion = c.mCriterion; mCriterionDateTime = c.mCriterionDateTime; }
	}
	[Serializable]
	public abstract partial class IfcConic : IfcCurve /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCircle ,IfcEllipse))*/
	{
		private int mPosition;// : IfcAxis2Placement;
		public IfcAxis2Placement Position { get { return mDatabase[mPosition] as IfcAxis2Placement; } set { mPosition = value.Index; } }

		protected IfcConic() : base() { }
		protected IfcConic(IfcAxis2Placement ap) : base(ap.Database) { mPosition = ap.Index; }
		protected IfcConic(DatabaseIfc db, IfcConic c) : base(db,c) { Position = db.Factory.Duplicate( c.mDatabase[c.mPosition]) as IfcAxis2Placement; }
	}
	[Serializable]
	public partial class IfcConnectedFaceSet : IfcTopologicalRepresentationItem //SUPERTYPE OF (ONEOF (IfcClosedShell ,IfcOpenShell))
	{
		internal SET<IfcFace> mCfsFaces = new SET<IfcFace>();// : SET [1:?] OF IfcFace;
		public SET<IfcFace> CfsFaces { get { return mCfsFaces; } set { if (value == null) mCfsFaces.Clear(); else mCfsFaces = value; } }

		internal IfcConnectedFaceSet() : base() { }
		internal IfcConnectedFaceSet(DatabaseIfc db, IfcConnectedFaceSet c) : base(db,c) { CfsFaces.AddRange(c.CfsFaces.ConvertAll(x=>db.Factory.Duplicate(x) as IfcFace)); }
		public IfcConnectedFaceSet(IEnumerable<IfcFace> faces) : base(faces.First().mDatabase) { mCfsFaces.AddRange(faces); }

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach(IfcFace f in CfsFaces)
				result.AddRange(f.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public partial class IfcConnectionCurveGeometry : IfcConnectionGeometry
	{
		private int mCurveOnRelatingElement;// : IfcCurveOrEdgeCurve;
		private int mCurveOnRelatedElement;// : OPTIONAL IfcCurveOrEdgeCurve; 

		public IfcCurveOrEdgeCurve CurveOnRelatingElement { get { return mDatabase[mCurveOnRelatingElement] as IfcCurveOrEdgeCurve; } set { mCurveOnRelatingElement = value.Index; } }
		public IfcCurveOrEdgeCurve CurveOnRelatedElement { get { return mDatabase[mCurveOnRelatedElement] as IfcCurveOrEdgeCurve; } set { mCurveOnRelatedElement = value.Index; } }

		internal IfcConnectionCurveGeometry() : base() { }
		internal IfcConnectionCurveGeometry(DatabaseIfc db, IfcConnectionCurveGeometry g) : base(db, g) { CurveOnRelatingElement = db.Factory.Duplicate( g.mDatabase[ g.mCurveOnRelatingElement]) as IfcCurveOrEdgeCurve; CurveOnRelatedElement = db.Factory.Duplicate(g.mDatabase[ g.mCurveOnRelatedElement]) as IfcCurveOrEdgeCurve; }
	}
	[Serializable]
	public abstract partial class IfcConnectionGeometry : BaseClassIfc /*ABSTRACT SUPERTYPE OF (ONEOF(IfcConnectionCurveGeometry,IfcConnectionPointGeometry,IfcConnectionPortGeometry,IfcConnectionSurfaceGeometry));*/
	{
		protected IfcConnectionGeometry() : base() { }
		protected IfcConnectionGeometry(DatabaseIfc db) : base(db) { }
		protected IfcConnectionGeometry(DatabaseIfc db, IfcConnectionGeometry c) : base(db) { }
	}
	[Serializable]
	public partial class IfcConnectionPointEccentricity : IfcConnectionPointGeometry
	{ 
		private double mEccentricityInX, mEccentricityInY, mEccentricityInZ;// : OPTIONAL IfcLengthMeasure;
		public double EccentricityInX { get { return mEccentricityInX; } set { mEccentricityInX = value; } }
		public double EccentricityInY { get { return mEccentricityInY; } set { mEccentricityInY = value; } }
		public double EccentricityInZ { get { return mEccentricityInZ; } set { mEccentricityInZ = value; } }

		internal IfcConnectionPointEccentricity() : base() { }
		internal IfcConnectionPointEccentricity(DatabaseIfc db, IfcConnectionPointEccentricity e) : base(db, e) { mEccentricityInX = e.mEccentricityInX; mEccentricityInY = e.mEccentricityInY; mEccentricityInZ = e.mEccentricityInZ; }
		public IfcConnectionPointEccentricity(IfcPointOrVertexPoint v, double x, double y, double z) : base(v) { mEccentricityInX = x; mEccentricityInY = y; mEccentricityInZ = z; }
	}
	[Serializable]
	public partial class IfcConnectionPointGeometry : IfcConnectionGeometry
	{
		private int mPointOnRelatingElement;// : IfcPointOrVertexPoint;
		private int mPointOnRelatedElement;// : OPTIONAL IfcPointOrVertexPoint;

		public IfcPointOrVertexPoint PointOnRelatingElement { get { return mDatabase[mPointOnRelatingElement] as IfcPointOrVertexPoint; } set { mPointOnRelatingElement = value.Index; } }
		public IfcPointOrVertexPoint PointOnRelatedElement { get { return mDatabase[mPointOnRelatedElement] as IfcPointOrVertexPoint; } set { mPointOnRelatedElement = (value == null ? 0 : value.Index); } }

		internal IfcConnectionPointGeometry() : base() { }
		internal IfcConnectionPointGeometry(DatabaseIfc db, IfcConnectionPointGeometry g) : base(db,g) { PointOnRelatingElement = db.Factory.Duplicate(g.mDatabase[g.mPointOnRelatingElement]) as IfcPointOrVertexPoint;  PointOnRelatedElement = db.Factory.Duplicate(g.mDatabase[g.mPointOnRelatedElement]) as IfcPointOrVertexPoint; }
		public IfcConnectionPointGeometry(IfcPointOrVertexPoint v) : base(v.Database) { mPointOnRelatingElement = v.Index; }
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcConnectionPortGeometry  // DEPRECEATED IFC4
	[Serializable]
	public partial class IfcConnectionSurfaceGeometry : IfcConnectionGeometry
	{
		internal int mSurfaceOnRelatingElement;// : IfcSurfaceOrFaceSurface;
		internal int mSurfaceOnRelatedElement;// : OPTIONAL IfcSurfaceOrFaceSurface;

		public IfcSurfaceOrFaceSurface SurfaceOnRelatingElement { get { return mDatabase[mSurfaceOnRelatingElement] as IfcSurfaceOrFaceSurface; } set { mSurfaceOnRelatingElement = value.Index; } }
		public IfcSurfaceOrFaceSurface SurfaceOnRelatedElement { get { return mDatabase[mSurfaceOnRelatedElement] as IfcSurfaceOrFaceSurface; } set { mSurfaceOnRelatedElement = value.Index; } }

		internal IfcConnectionSurfaceGeometry() : base() { }
		internal IfcConnectionSurfaceGeometry(DatabaseIfc db, IfcConnectionSurfaceGeometry g) : base(db,g) { SurfaceOnRelatingElement = db.Factory.Duplicate(g.mDatabase[g.mSurfaceOnRelatingElement]) as IfcSurfaceOrFaceSurface; SurfaceOnRelatedElement = db.Factory.Duplicate(g.mDatabase[g.mSurfaceOnRelatedElement]) as IfcSurfaceOrFaceSurface; }
	}
	[Serializable]
	public abstract partial class IfcConstraint : BaseClassIfc, IfcResourceObjectSelect, NamedObjectIfc //IFC4Change ABSTRACT SUPERTYPE OF(ONEOF(IfcMetric, IfcObjective));
	{
		internal string mName = "NOTDEFINED";// :  IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal IfcConstraintEnum mConstraintGrade;// : IfcConstraintEnum
		internal string mConstraintSource = "$";// : OPTIONAL IfcLabel;
		internal int mCreatingActor;// : OPTIONAL IfcActorSelect;
		internal string mCreationTime = "$";// : OPTIONAL IfcDateTimeSelect; IFC4 IfcDateTime 
		internal int mSSCreationTime;// : OPTIONAL IfcDateTimeSelect; IFC4 IfcDateTime 
		internal string mUserDefinedGrade = "$";// : OPTIONAL IfcLabel

		public string Name { get { return ParserIfc.Decode(mName); } set { mName = (string.IsNullOrEmpty(value) ? "NOTDEFINED" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcConstraintEnum ConstraintGrade { get { return mConstraintGrade; } set { mConstraintGrade = value; } }
		public string ConstraintSource { get { return (mConstraintSource == "$" ? "" : ParserIfc.Decode(mConstraintSource)); } set { mConstraintSource = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcActorSelect CreatingActor { get { return mDatabase[mCreatingActor] as IfcActorSelect; } set { mCreatingActor = (value == null ? 0 : value.Index); } }
		//creationtime
		public string UserDefinedGrade { get { return (mUserDefinedGrade == "$" ? "" : ParserIfc.Decode(mUserDefinedGrade)); } set { mUserDefinedGrade = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		//	INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReferences = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mPropertiesForConstraint = new List<IfcResourceConstraintRelationship>();//	 :	SET OF IfcResourceConstraintRelationship FOR RelatingConstraint;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //ggc
		internal List<IfcRelAssociatesConstraint> mConstraintForObjects = new List<IfcRelAssociatesConstraint>();// gg	 :	SET [0:?] OF IfcRelAssociatesConstraint FOR RelatedResourceObjects;
		public SET<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } set { mHasExternalReferences.Clear();  if (value != null) { mHasExternalReferences.CollectionChanged -= mHasExternalReferences_CollectionChanged; mHasExternalReferences = value; mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged; } } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		protected IfcConstraint() : base() { }
		protected IfcConstraint(DatabaseIfc db, IfcConstraint c) : base(db,c)
		{
			mName = c.mName;
			mDescription = c.mDescription;
			mConstraintGrade = c.mConstraintGrade;
			mConstraintSource = c.mConstraintSource;
			if(mCreatingActor > 0)
				CreatingActor = db.Factory.Duplicate(c.mDatabase[c.mCreatingActor]) as IfcActorSelect;
			mCreationTime = c.mCreationTime;
			if(mSSCreationTime > 0)
				mSSCreationTime = db.Factory.Duplicate(c.mDatabase[ c.mSSCreationTime]).mIndex;
			mUserDefinedGrade = c.mUserDefinedGrade;
		}
		protected IfcConstraint(DatabaseIfc db, string name, IfcConstraintEnum constraint) : base(db) { Name = name; mConstraintGrade = constraint; }
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged;
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
		public override bool Dispose(bool children)
		{
			foreach (IfcRelAssociatesConstraint rac in mConstraintForObjects)
				mDatabase[rac.mIndex] = null;
			List<IfcResourceConstraintRelationship> rcrs = mPropertiesForConstraint;
			for (int icounter = 0; icounter < rcrs.Count; icounter++)
				rcrs[icounter].Dispose(true);
			return base.Dispose(children);
		}

		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcConstraintAggregationRelationship; // DEPRECEATED IFC4
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcConstraintclassificationRelationship; // DEPRECEATED IFC4
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcConstraintRelationship; // DEPRECEATED IFC4
	[Serializable]
	public partial class IfcConstructionEquipmentResource : IfcConstructionResource
	{
		internal IfcConstructionEquipmentResourceTypeEnum mPredefinedType = IfcConstructionEquipmentResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcConstructionEquipmentResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcConstructionEquipmentResource() : base() { }
		internal IfcConstructionEquipmentResource(DatabaseIfc db, IfcConstructionEquipmentResource r, IfcOwnerHistory ownerHistory, bool downStream) : base(db,r, ownerHistory, downStream) { mPredefinedType = r.mPredefinedType; }
		public IfcConstructionEquipmentResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcConstructionEquipmentResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcConstructionEquipmentResourceTypeEnum mPredefinedType = IfcConstructionEquipmentResourceTypeEnum.NOTDEFINED;
		public IfcConstructionEquipmentResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcConstructionEquipmentResourceType() : base() { }
		internal IfcConstructionEquipmentResourceType(DatabaseIfc db, IfcConstructionEquipmentResourceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcConstructionEquipmentResourceType(DatabaseIfc db, string name, IfcConstructionEquipmentResourceTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcConstructionMaterialResource : IfcConstructionResource
	{
		internal IfcConstructionMaterialResourceTypeEnum mPredefinedType = IfcConstructionMaterialResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcConstructionMaterialResourceTypeEnum; 
		public IfcConstructionMaterialResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		//Suppliers	 : 	OPTIONAL SET[1:?] OF IfcActorSelect; ifc2x3
		//UsageRatio	 : 	OPTIONAL IfcRatioMeasure; ifc2x3
		internal IfcConstructionMaterialResource() : base() { }
		internal IfcConstructionMaterialResource(DatabaseIfc db, IfcConstructionMaterialResource r, IfcOwnerHistory ownerHistory, bool downStream) : base(db,r, ownerHistory, downStream) { mPredefinedType = r.mPredefinedType; }
		public IfcConstructionMaterialResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcConstructionMaterialResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcConstructionMaterialResourceTypeEnum mPredefinedType = IfcConstructionMaterialResourceTypeEnum.NOTDEFINED;
		public IfcConstructionMaterialResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcConstructionMaterialResourceType() : base() { }
		internal IfcConstructionMaterialResourceType(DatabaseIfc db, IfcConstructionMaterialResourceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcConstructionMaterialResourceType(DatabaseIfc db, string name, IfcConstructionMaterialResourceTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcConstructionProductResource : IfcConstructionResource
	{
		internal IfcConstructionProductResourceTypeEnum mPredefinedType = IfcConstructionProductResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcConstructionProductResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcConstructionProductResource() : base() { }
		internal IfcConstructionProductResource(DatabaseIfc db, IfcConstructionProductResource r, IfcOwnerHistory ownerHistory, bool downStream) : base(db,r, ownerHistory, downStream) { mPredefinedType = r.mPredefinedType; }
		public IfcConstructionProductResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcConstructionProductResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcConstructionProductResourceTypeEnum mPredefinedType = IfcConstructionProductResourceTypeEnum.NOTDEFINED;
		public IfcConstructionProductResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcConstructionProductResourceType() : base() { }
		internal IfcConstructionProductResourceType(DatabaseIfc db, IfcConstructionProductResourceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcConstructionProductResourceType(DatabaseIfc db, string name, IfcConstructionProductResourceTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcConstructionResource : IfcResource //ABSTRACT SUPERTYPE OF (ONEOF(IfcConstructionEquipmentResource, IfcConstructionMaterialResource, IfcConstructionProductResource, IfcCrewResource, IfcLaborResource, IfcSubContractResource))
	{
		internal int mUsage; //:	OPTIONAL IfcResourceTime; IFC4
		internal List<int> mBaseCosts = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcAppliedValue; IFC4
		internal IfcPhysicalQuantity mBaseQuantity = null;//	 :	OPTIONAL IfcPhysicalQuantity; IFC4 

		internal string mResourceGroup = "";// : 	OPTIONAL IfcLabel;
		internal IfcResourceConsumptionEnum mResourceConsumption = IfcResourceConsumptionEnum.NOTDEFINED;//	 : 	OPTIONAL IfcResourceConsumptionEnum;
		internal IfcMeasureWithUnit mBaseQuantitySS = null;//	 : 	OPTIONAL IfcMeasureWithUnit;

		public IfcResourceTime Usage { get { return mDatabase[mUsage] as IfcResourceTime; } set { mUsage = (value == null  ? 0 : value.mIndex); } }
		public ReadOnlyCollection<IfcAppliedValue> BaseCosts { get { return new ReadOnlyCollection<IfcAppliedValue>( mBaseCosts.ConvertAll(x => mDatabase[x] as IfcAppliedValue)); } }
		public IfcPhysicalQuantity BaseQuantity { get { return mBaseQuantity; } set { mBaseQuantity = value; } }
		protected IfcConstructionResource() : base() { }
		protected IfcConstructionResource(DatabaseIfc db, IfcConstructionResource r, IfcOwnerHistory ownerHistory, bool downStream) : base(db,r, ownerHistory, downStream)
		{
			if(r.mUsage > 0)
				Usage = db.Factory.Duplicate(r.Usage) as IfcResourceTime;
			if(r.mBaseCosts.Count > 0)
				r.BaseCosts.ToList().ForEach(x=>addCost( db.Factory.Duplicate(x) as IfcAppliedValue));
			if(r.mBaseQuantity != null)
				BaseQuantity = db.Factory.Duplicate(r.BaseQuantity) as IfcPhysicalQuantity;
		}
		protected IfcConstructionResource(DatabaseIfc db) : base(db) { }
		
		internal void addCost(IfcAppliedValue cost) { mBaseCosts.Add(cost.mIndex); }
	}
	[Serializable]
	public abstract partial class IfcConstructionResourceType : IfcTypeResource //IFC4
	{
		internal List<int> mBaseCosts = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcAppliedValue; 
		internal int mBaseQuantity;//	 :	OPTIONAL IfcPhysicalQuantity; 

		public ReadOnlyCollection<IfcAppliedValue> BaseCosts { get { return new ReadOnlyCollection<IfcAppliedValue>( mBaseCosts.ConvertAll(x => mDatabase[x] as IfcAppliedValue)); } }
		public IfcPhysicalQuantity BaseQuantity { get { return mDatabase[mBaseQuantity] as IfcPhysicalQuantity; } set { mBaseQuantity = (value == null ? 0 : value.mIndex); } }

		protected IfcConstructionResourceType() : base() { }
		protected IfcConstructionResourceType(DatabaseIfc db) : base(db) { }
		protected IfcConstructionResourceType(DatabaseIfc db, IfcConstructionResourceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream)
		{
			t.BaseCosts.ToList().ForEach(x => addCost(db.Factory.Duplicate(x) as IfcAppliedValue));
			BaseQuantity = db.Factory.Duplicate(t.BaseQuantity) as IfcPhysicalQuantity;
		}
		
		internal void addCost(IfcAppliedValue cost) { mBaseCosts.Add(cost.mIndex); }
	}
	[Serializable]
	public abstract partial class IfcContext : IfcObjectDefinition//(IfcProject, IfcProjectLibrary)
	{
		internal string mObjectType = "$";//	 :	OPTIONAL IfcLabel;
		private string mLongName = "$";// : OPTIONAL IfcLabel;
		private string mPhase = "$";// : OPTIONAL IfcLabel;
		internal SET<IfcRepresentationContext> mRepresentationContexts = new SET<IfcRepresentationContext>();// : 	OPTIONAL SET [1:?] OF IfcRepresentationContext;
		private int mUnitsInContext;// : OPTIONAL IfcUnitAssignment; IFC2x3 not Optional
		//INVERSE
		internal List<IfcRelDefinesByProperties> mIsDefinedBy = new List<IfcRelDefinesByProperties>();
		internal List<IfcRelDeclares> mDeclares = new List<IfcRelDeclares>();

		public string ObjectType { get { return mObjectType == "$" ? "" : ParserIfc.Decode(mObjectType); } set { mObjectType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string LongName { get { return (mLongName == "$" ? "" : ParserIfc.Decode(mLongName)); } set { mLongName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Phase { get { return (mPhase == "$" ? "" : ParserIfc.Decode(mPhase)); } set { mPhase = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public SET<IfcRepresentationContext> RepresentationContexts { get { return mRepresentationContexts; } set { mRepresentationContexts.Clear(); if (value != null) mRepresentationContexts = value; } }
		public IfcUnitAssignment UnitsInContext
		{
			get { return mDatabase[mUnitsInContext] as IfcUnitAssignment; }
			set
			{
				mUnitsInContext = (value == null ? 0 : value.mIndex);
				if(value != null)
					mDatabase.Factory.Options.AngleUnitsInRadians = Math.Abs(1 - value.ScaleSI(IfcUnitEnum.PLANEANGLEUNIT)) < 1e-4;
			}
		}

		public ReadOnlyCollection<IfcRelDefinesByProperties> IsDefinedBy { get { return new ReadOnlyCollection<IfcRelDefinesByProperties>( mIsDefinedBy); } }
		public ReadOnlyCollection<IfcRelDeclares> Declares { get { return new ReadOnlyCollection<IfcRelDeclares>( mDeclares); } }
	
		protected IfcContext() : base() { }
		protected IfcContext(DatabaseIfc db, IfcContext c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream)
		{
			if(db.mContext == null)
				db.mContext = this;
			mObjectType = c.mObjectType;
			mLongName = c.mLongName;
			mPhase = c.mPhase;
			RepresentationContexts.AddRange(c.RepresentationContexts.ConvertAll(x => db.Factory.Duplicate(x) as IfcRepresentationContext));
			if (c.mUnitsInContext > 0)
				UnitsInContext = db.Factory.Duplicate(c.UnitsInContext) as IfcUnitAssignment;

			foreach (IfcRelDefinesByProperties rdp in c.mIsDefinedBy)
			{
				IfcRelDefinesByProperties drdp = db.Factory.Duplicate(rdp) as IfcRelDefinesByProperties;
				drdp.RelatedObjects.Add(this);
			}
		}
		protected IfcContext(string name, IfcUnitAssignment units) : this(units.Database, name)
		{
			UnitsInContext = units;
		}
		protected IfcContext(DatabaseIfc db, string name, IfcUnitAssignment.Length length) : this(db,name)
		{
			UnitsInContext = new IfcUnitAssignment(db);
			UnitsInContext.SetUnits(length);
		}
		protected IfcContext(DatabaseIfc db, string name) : base(db)
		{
			Name = name;
			RepresentationContexts.AddRange(db.Factory.mContexts.Values);
			db.mContext = this;
		}
		
		internal void setStructuralUnits()
		{
			IfcUnitAssignment ua = UnitsInContext;
			if (ua != null)
				ua.setStructuralUnits();
		}
		
		internal void initializeUnitsAndScales()
		{
			if (mRepresentationContexts.Count > 0)
			{
				for (int icounter = 0; icounter < mRepresentationContexts.Count; icounter++)
				{
					IfcGeometricRepresentationContext c = mRepresentationContexts[icounter] as IfcGeometricRepresentationContext;
					if (c != null)
					{
						if(!double.IsNaN(c.mPrecision))
							mDatabase.Tolerance = c.mPrecision;
						break;
					}
				}
			}
			IfcUnitAssignment units = UnitsInContext;
			if (units != null)
			{
				IfcNamedUnit namedUnit = units[IfcUnitEnum.LENGTHUNIT];
				if (namedUnit != null)
					mDatabase.ScaleSI = namedUnit.SIFactor;
			}
		}
		public IfcRelDeclares AddDeclared(IfcDefinitionSelect d)
		{
			if (mDeclares.Count == 0)
				return new IfcRelDeclares(this, d);
			mDeclares[0].RelatedDefinitions.Add(d);
			return mDeclares[0];
		}

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);

			Type[] interfaces = typeof(T).GetInterfaces();
			if(interfaces.Contains(typeof(IfcDefinitionSelect)))
			{
				foreach(IfcRelDeclares rd in Declares)
				{
					IReadOnlyCollection<IfcDefinitionSelect> ds = rd.RelatedDefinitions;
					foreach(IfcDefinitionSelect d in ds)
					{
						result.AddRange(d.Extract<T>());
						if (d is T && !result.Contains((T)d))
							result.Add((T)d);
					}
				}
			}
			foreach (IfcRelDefinesByProperties rdp in mIsDefinedBy)
				result.AddRange(rdp.Extract<T>());
			return result;
		}
		public List<IfcTypeObject> DeclaredTypes
		{
			get
			{
				if(mDeclares.Count > 0)
					return mDeclares.SelectMany(x=>x.RelatedDefinitions).OfType<IfcTypeObject>().ToList();
				return mDatabase.OfType<IfcTypeObject>().ToList();
			}
		}
		public override IfcPropertySetDefinition FindPropertySet(string name)
		{
			foreach (IfcPropertySetDefinition pset in mIsDefinedBy.Select(x => x.RelatingPropertyDefinition))
			{
				if (string.Compare(pset.Name, name) == 0)
					return pset;
			}
			return null;
		}
		public override IfcProperty FindProperty(string name)
		{
			foreach(IfcPropertySet pset in mIsDefinedBy.ConvertAll(x=>x.RelatingPropertyDefinition).OfType<IfcPropertySet>())
			{
				IfcProperty property = pset[name];
				if (property != null)
					return property;
			}
			return null;
		}
		public override void RemoveProperty(IfcProperty property)
		{
			removeProperty(property, IsDefinedBy);
		}
		public void ChangeSchemaWIP(ReleaseVersion release) { changeSchema(release); }
		internal override void changeSchema(ReleaseVersion schema)
		{
			if (schema < ReleaseVersion.IFC4)
			{

			}
			else if (schema > ReleaseVersion.IFC2x3)
			{
				if (mDatabase.mModelView == ModelView.Ifc2x3NotAssigned)
					mDatabase.mModelView = ModelView.Ifc4NotAssigned;
				
			}
			else
			{
				throw new Exception("Early releases of IFC not supported yet!");
			}
			mDatabase.mRelease = schema;
			base.changeSchema(schema);
		}
	}
	//ENTITY IfcContextDependentUnit, IfcResourceObjectSelect
	[Serializable]
	public abstract partial class IfcControl : IfcObject //ABSTRACT SUPERTYPE OF (ONEOF (IfcActionRequest ,IfcConditionCriterion ,IfcCostItem ,IfcCostSchedule,IfcEquipmentStandard ,IfcFurnitureStandard
	{ //  ,IfcPerformanceHistory ,IfcPermit ,IfcProjectOrder ,IfcProjectOrderRecord ,IfcScheduleTimeControl ,IfcServiceLife ,IfcSpaceProgram ,IfcTimeSeriesSchedule,IfcWorkControl))
		internal string mIdentification = "$"; // : OPTIONAL IfcIdentifier; IFC4
		//INVERSE
		internal List<IfcRelAssignsToControl> mControls = new List<IfcRelAssignsToControl>();/// : SET OF IfcRelAssignsToControl FOR RelatingControl;

		public string Identification { get { return mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcRelAssignsToControl> Controls => new ReadOnlyCollection<IfcRelAssignsToControl>(mControls);
		protected IfcControl() : base() { }
		protected IfcControl(DatabaseIfc db, IfcControl c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { mIdentification = c.mIdentification; }
		protected IfcControl(DatabaseIfc db) : base(db)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.Ifc2x3NotAssigned)
				throw new Exception("Invalid Model View for IfcActor : " + db.ModelView.ToString());
		}

		public void Assign(IfcObjectDefinition o) { if (mControls.Count == 0) new IfcRelAssignsToControl(this, o); else mControls[0].RelatedObjects.Add(o); }
	}
	[Serializable]
	public partial class IfcController : IfcDistributionControlElement //IFC4  
	{
		internal IfcControllerTypeEnum mPredefinedType = IfcControllerTypeEnum.NOTDEFINED;
		public IfcControllerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcController() : base() { }
		internal IfcController(DatabaseIfc db, IfcController c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { mPredefinedType = c.mPredefinedType; }
		public IfcController(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcControllerType : IfcDistributionControlElementType
	{
		internal IfcControllerTypeEnum mPredefinedType = IfcControllerTypeEnum.NOTDEFINED;// : IfcControllerTypeEnum;
		public IfcControllerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcControllerType() : base() { }
		internal IfcControllerType(DatabaseIfc db, IfcControllerType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcControllerType(DatabaseIfc m, string name, IfcControllerTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcConversionBasedUnit : IfcNamedUnit, IfcResourceObjectSelect, NamedObjectIfc //	SUPERTYPE OF(IfcConversionBasedUnitWithOffset)
	{
		public enum Common
		{
			inch, foot, yard, mile, square_inch, square_foot, square_yard, acre, square_mile, cubic_inch, cubic_foot, cubic_yard, litre, fluid_ounce_UK,
			fluid_ounce_US, pint_UK, pint_US, gallon_UK, gallon_US, degree, ounce, pound, ton_UK, ton_US, lbf, kip, psi, ksi, minute, hour, day, btu
		};
		
		private string mName = "";// : IfcLabel;
		private int mConversionFactor;// : IfcMeasureWithUnit; 
									  //INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReferences = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg
		public SET<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } set { mHasExternalReferences.Clear();  if (value != null) { mHasExternalReferences.CollectionChanged -= mHasExternalReferences_CollectionChanged; mHasExternalReferences = value; mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged; } } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>( mHasConstraintRelationships); } }

		public string Name { get { return ParserIfc.Decode(mName); } set { mName = ParserIfc.Encode(value); } }
		public IfcMeasureWithUnit ConversionFactor { get { return mDatabase[mConversionFactor] as IfcMeasureWithUnit; } set { mConversionFactor = value.mIndex; } }
		
		internal IfcConversionBasedUnit() : base() { }
		internal IfcConversionBasedUnit(DatabaseIfc db, IfcConversionBasedUnit u) : base(db,u) { mName = u.mName; ConversionFactor = db.Factory.Duplicate( u.ConversionFactor) as IfcMeasureWithUnit; }
		public IfcConversionBasedUnit(IfcUnitEnum unit, string name, IfcMeasureWithUnit mu)
			: base(mu.mDatabase, unit, true) { Name = name.Replace("'", ""); mConversionFactor = mu.mIndex; }
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged;
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
		public override double SIFactor { get { return ConversionFactor.SIFactor(); } }

		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	[Serializable]
	public partial class IfcConversionBasedUnitWithOffset : IfcConversionBasedUnit //IFC4
	{
		internal double mConversionOffset = 0;//	 :	IfcReal
		public double ConversionOffset { get { return mConversionOffset; } set { mConversionOffset = value; } }
		
		internal IfcConversionBasedUnitWithOffset() : base() { }
		internal IfcConversionBasedUnitWithOffset(DatabaseIfc db, IfcConversionBasedUnitWithOffset u) : base(db,u) { mConversionOffset = u.mConversionOffset; }
		public IfcConversionBasedUnitWithOffset(IfcUnitEnum unit, string name, IfcMeasureWithUnit mu, double offset)
			: base(unit, name, mu) { mConversionOffset = offset; }
	}
	[Serializable]
	public partial class IfcCooledBeam : IfcEnergyConversionDevice //IFC4
	{
		internal IfcCooledBeamTypeEnum mPredefinedType = IfcCooledBeamTypeEnum.NOTDEFINED;// OPTIONAL : IfcCooledBeamTypeEnum;
		public IfcCooledBeamTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCooledBeam() : base() { }
		internal IfcCooledBeam(DatabaseIfc db, IfcCooledBeam b, IfcOwnerHistory ownerHistory, bool downStream) : base(db, b, ownerHistory, downStream) { mPredefinedType = b.mPredefinedType; }
		public IfcCooledBeam(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCooledBeamType : IfcEnergyConversionDeviceType
	{
		internal IfcCooledBeamTypeEnum mPredefinedType = IfcCooledBeamTypeEnum.NOTDEFINED;// : IfcCooledBeamTypeEnum
		public IfcCooledBeamTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCooledBeamType() : base() { }
		internal IfcCooledBeamType(DatabaseIfc db, IfcCooledBeamType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCooledBeamType(DatabaseIfc m, string name, IfcCooledBeamTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcCoolingTower : IfcEnergyConversionDevice //IFC4
	{
		internal IfcCoolingTowerTypeEnum mPredefinedType = IfcCoolingTowerTypeEnum.NOTDEFINED;// OPTIONAL : IfcCoolingTowerTypeEnum;
		public IfcCoolingTowerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCoolingTower() : base() { }
		internal IfcCoolingTower(DatabaseIfc db, IfcCoolingTower t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCoolingTower(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCoolingTowerType : IfcEnergyConversionDeviceType
	{
		internal IfcCoolingTowerTypeEnum mPredefinedType = IfcCoolingTowerTypeEnum.NOTDEFINED;// : IfcCoolingTowerTypeEnum
		public IfcCoolingTowerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCoolingTowerType() : base() { }
		internal IfcCoolingTowerType(DatabaseIfc db, IfcCoolingTowerType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCoolingTowerType(DatabaseIfc m, string name, IfcCoolingTowerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcCoordinatedUniversalTimeOffset : BaseClassIfc //DEPRECEATED IFC4
	{
		internal int mHourOffset;// : IfcHourInDay;
		internal int mMinuteOffset;// : OPTIONAL IfcMinuteInHour;
		internal IfcAheadOrBehind mSense = IfcAheadOrBehind.AHEAD;// : IfcAheadOrBehind; 
		//internal IfcCoordinatedUniversalTimeOffset(IfcCoordinatedUniversalTimeOffset v) : base() { mHourOffset = v.mHourOffset; mMinuteOffset = v.mMinuteOffset; mSense = v.mSense; }
		internal IfcCoordinatedUniversalTimeOffset() : base() { }
	}
	[Serializable]
	public abstract partial class IfcCoordinateOperation : BaseClassIfc // IFC4 	ABSTRACT SUPERTYPE OF(IfcMapConversion);
	{
		internal int mSourceCRS;// :	IfcCoordinateReferenceSystemSelect;
		private int mTargetCRS;// :	IfcCoordinateReferenceSystem;

		public IfcCoordinateReferenceSystemSelect SourceCRS { get { return mDatabase[mSourceCRS] as IfcCoordinateReferenceSystemSelect; } set { mSourceCRS = value.Index; if(value.HasCoordinateOperation != this) value.HasCoordinateOperation = this; } }
		public IfcCoordinateReferenceSystem TargetCRS { get { return mDatabase[mTargetCRS] as IfcCoordinateReferenceSystem; } set { mTargetCRS = value.mIndex; } }

		protected IfcCoordinateOperation() : base() { }
		protected IfcCoordinateOperation(DatabaseIfc db, IfcCoordinateOperation p) : base(db,p) { SourceCRS = db.Factory.Duplicate(p.mDatabase[p.mSourceCRS]) as IfcCoordinateReferenceSystemSelect; TargetCRS = db.Factory.Duplicate(p.TargetCRS) as IfcCoordinateReferenceSystem; }
		protected IfcCoordinateOperation(IfcCoordinateReferenceSystemSelect source, IfcCoordinateReferenceSystem target) : base(source.Database) { SourceCRS = source; TargetCRS = target; }
	}
	[Serializable]
	public abstract partial class IfcCoordinateReferenceSystem : BaseClassIfc, IfcCoordinateReferenceSystemSelect, NamedObjectIfc  // IFC4 	ABSTRACT SUPERTYPE OF(IfcProjectedCRS);
	{
		internal string mName = "$";//	:	OPTIONAL IfcLabel;
		internal string mDescription = "$";//	:	OPTIONAL IfcText;
		internal string mGeodeticDatum; //	: OPTIONAL IfcIdentifier;
		internal string mVerticalDatum = "$";	//:	OPTIONAL IfcIdentifier;
		//INVERSE
		private IfcCoordinateOperation mHasCoordinateOperation = null;

		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = string.IsNullOrEmpty(value) ? "Unknown" :  ParserIfc.Encode(value); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string GeodeticDatum { get { return  ParserIfc.Decode(mGeodeticDatum); } set { mGeodeticDatum = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string VerticalDatum { get { return (mVerticalDatum == "$" ? "" : ParserIfc.Decode(mVerticalDatum)); } set { mVerticalDatum = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcCoordinateOperation HasCoordinateOperation { get { return mHasCoordinateOperation; } set { mHasCoordinateOperation = value; if (value.mSourceCRS != mIndex) value.SourceCRS = this; } }

		protected IfcCoordinateReferenceSystem() : base() { }
		protected IfcCoordinateReferenceSystem(DatabaseIfc db, IfcCoordinateReferenceSystem p) : base(db,p) { mName = p.mName; mDescription = p.mDescription; mGeodeticDatum = p.mGeodeticDatum; mVerticalDatum = p.mVerticalDatum; }
		protected IfcCoordinateReferenceSystem(DatabaseIfc db, string name) : base(db) { Name = name; }
	}
	public interface IfcCoordinateReferenceSystemSelect : IBaseClassIfc // IfcCoordinateReferenceSystem, IfcGeometricRepresentationContext
	{
		IfcCoordinateOperation HasCoordinateOperation { get; set; }
	} 
	[Serializable]
	public partial class IfcCostItem : IfcControl
	{
		internal IfcCostItemTypeEnum mPredefinedType = IfcCostItemTypeEnum.NOTDEFINED; // IFC4
		internal List<int> mCostValues = new List<int>();//	 : OPTIONAL LIST [1:?] OF IfcCostValue; IFC4
		internal List<int> mCostQuantities = new List<int>();//	 : OPTIONAL LIST [1:?] OF IfcPhysicalQuantity; IFC4

		public IfcCostItemTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCostItem() : base() { } 
		internal IfcCostItem(DatabaseIfc db, IfcCostItem i, IfcOwnerHistory ownerHistory, bool downStream) : base(db, i, ownerHistory, downStream) { mPredefinedType = i.mPredefinedType; }
		internal IfcCostItem(IfcCostSchedule s, IfcCostItemTypeEnum t)
			: base(s.mDatabase) { s.AddAggregated(this); mPredefinedType = t; }
		internal IfcCostItem(IfcCostItem i, IfcCostItemTypeEnum t)
			: base(i.mDatabase) { i.AddNested(this); mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcCostSchedule : IfcControl
	{
		internal IfcCostScheduleTypeEnum mPredefinedType = IfcCostScheduleTypeEnum.NOTDEFINED;// :	OPTIONAL IfcCostScheduleTypeEnum;
		internal string mStatus = "$";// : OPTIONAL IfcLabel; 
		private DateTime mSubmittedOn = DateTime.Now;// : 	IfcDateTime
		private int mSubmittedOnSS = 0; // : OPTIONAL IfcDateTimeSelect;
		private DateTime mUpdateDate = DateTime.Now;// : 	IfcDateTime
		private int mUpdateDateSS = 0; // : OPTIONAL IfcDateTimeSelect; 
		private int mSubmittedBy;// : OPTIONAL IfcActorSelect; IFC4 DELETED 
		private int mPreparedBy;// : OPTIONAL IfcActorSelect; IFC4 DELETED 
		private List< int> mTargetUsers = new List<int>();// : OPTIONAL SET [1:?] OF IfcActorSelect; //IFC4 DELETED 
		//internal string mID;// : IfcIdentifier; IFC4 relocated 
		public IfcCostScheduleTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public string Staus { get { return (mStatus == "$" ? "" : ParserIfc.Decode( mStatus)); } set { mStatus = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode( value.Replace("'",""))); } }
		internal DateTime SubmittedOn
		{
			get { return (mSubmittedOnSS > 0 ? (mDatabase[mSubmittedOnSS] as IfcDateTimeSelect).DateTime : mSubmittedOn); }
			set
			{
				if (mDatabase.Release <= ReleaseVersion.IFC2x3)
					mSubmittedOnSS = new IfcCalendarDate(mDatabase, value.Day, value.Month, value.Year).mIndex;
				else
					mSubmittedOn = value;
			}
		}
		internal DateTime UpdateDate
		{
			get { return (mUpdateDateSS > 0 ? (mDatabase[mUpdateDateSS] as IfcDateTimeSelect).DateTime : mUpdateDate); }
			set
			{
				if (mDatabase.Release <= ReleaseVersion.IFC2x3)
					mUpdateDateSS = new IfcCalendarDate(mDatabase, value.Day, value.Month, value.Year).mIndex;
				else
					mUpdateDate = value;
			}
		}
		internal IfcCostSchedule() : base() { }
		internal IfcCostSchedule(DatabaseIfc db, IfcCostSchedule s, IfcOwnerHistory ownerHistory, bool downStream) : base(db,s, ownerHistory, downStream)
		{
			mSubmittedBy = s.mSubmittedBy;
			mPreparedBy = s.mPreparedBy;
			mSubmittedBy = s.mSubmittedBy;
			mStatus = s.mStatus;
			//mTargetUsers = new List<int>( s.mTargetUsers.ToArray());
			mUpdateDate = s.mUpdateDate; 
			mPredefinedType = s.mPredefinedType;
		}
		internal IfcCostSchedule(DatabaseIfc db, IfcCostScheduleTypeEnum t, string status, DateTime submitted, IfcProject prj)
			: base(db) 
		{
			mPredefinedType = t;
			if (!string.IsNullOrEmpty(status)) 
				mStatus = status.Replace("'", "");
			mSubmittedOn = submitted;	
			if (prj != null) 
				prj.AddDeclared(this);
		}
	}
	[Serializable]
	public partial class IfcCostValue : IfcAppliedValue
	{
		//internal string mCostType;// : IfcLabel;  IFC4 renamed to category
		//internal string mCondition = "$";//  : OPTIONAL IfcText; IFC4 moved to condition
		internal IfcCostValue() : base() { }
		internal IfcCostValue(DatabaseIfc db, IfcCostValue v) : base(db,v) { }
		public IfcCostValue(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcCovering : IfcBuildingElement
	{
		internal IfcCoveringTypeEnum mPredefinedType = IfcCoveringTypeEnum.NOTDEFINED;// : OPTIONAL IfcCoveringTypeEnum;
		//INVERSE
		internal IfcRelCoversSpaces mCoversSpaces = null;//	 : 	SET [0:1] OF IfcRelCoversSpaces FOR RelatedCoverings;
		internal IfcRelCoversBldgElements mCoversElements = null;//	 : 	SET [0:1] OF IfcRelCoversBldgElements FOR RelatedCoverings;

		public IfcCoveringTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcRelCoversSpaces CoversSpaces { get { return mCoversSpaces; } set { mCoversSpaces = value; } }
		
		internal IfcCovering() : base() { }
		internal IfcCovering(DatabaseIfc db, IfcCovering c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { mPredefinedType = c.mPredefinedType; }
		public IfcCovering(DatabaseIfc db) : base(db) { }
		public IfcCovering(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcCoveringType : IfcBuildingElementType
	{
		internal IfcCoveringTypeEnum mPredefinedType = IfcCoveringTypeEnum.NOTDEFINED;
		public IfcCoveringTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCoveringType() : base() { }
		internal IfcCoveringType(DatabaseIfc db, IfcCoveringType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db,t,ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCoveringType(DatabaseIfc m, string name, IfcCoveringTypeEnum type) : base(m) { Name = name; mPredefinedType = type; } 
	}
	[Obsolete("DELETED IFC4", false)]
	[Serializable]
	public partial class IfcCraneRailAShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mOverallHeight, mBaseWidth2;// : IfcPositiveLengthMeasure;
		internal double mRadius;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mHeadWidth, mHeadDepth2, mHeadDepth3, mWebThickness, mBaseWidth4, mBaseDepth1, mBaseDepth2, mBaseDepth3;// : IfcPositiveLengthMeasure;
		internal double mCentreOfGravityInY;// : OPTIONAL IfcPositiveLengthMeasure; 
		internal IfcCraneRailAShapeProfileDef() : base() { }
		internal IfcCraneRailAShapeProfileDef(DatabaseIfc db, IfcCraneRailAShapeProfileDef p) : base(db, p)
		{
			mOverallHeight = p.mOverallHeight; mBaseWidth2 = p.mBaseWidth2; mRadius = p.mRadius; mHeadWidth = p.mHeadWidth; mHeadDepth2 = p.mHeadDepth2;
			mHeadDepth3 = p.mHeadDepth3; mWebThickness = p.mWebThickness; mBaseWidth4 = p.mBaseWidth4; mBaseDepth1 = p.mBaseDepth1;
			mBaseDepth2 = p.mBaseDepth2; mBaseDepth3 = p.mBaseDepth3; mCentreOfGravityInY = p.mCentreOfGravityInY;
		}
	}
	[Obsolete("DELETED IFC4", false)]
	[Serializable]
	public partial class IfcCraneRailFShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mOverallHeight, mHeadWidth;// : IfcPositiveLengthMeasure;
		internal double mRadius;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mHeadDepth2, mHeadDepth3, mWebThickness, mBaseDepth1, mBaseDepth2;// : IfcPositiveLengthMeasure;
		internal double mCentreOfGravityInY;// : OPTIONAL IfcPositiveLengthMeasure; 
		internal IfcCraneRailFShapeProfileDef() : base() { }
		internal IfcCraneRailFShapeProfileDef(DatabaseIfc db, IfcCraneRailFShapeProfileDef p)
			: base(db, p)
		{
			mOverallHeight = p.mOverallHeight; mHeadWidth = p.mHeadWidth; mRadius = p.mRadius; mHeadDepth2 = p.mHeadDepth2; mHeadDepth3 = p.mHeadDepth3;
			mWebThickness = p.mWebThickness; mBaseDepth1 = p.mBaseDepth1; mBaseDepth2 = p.mBaseDepth2; mCentreOfGravityInY = p.mCentreOfGravityInY;
		}
	} 
	[Serializable]
	public partial class IfcCrewResource : IfcConstructionResource
	{
		internal IfcCrewResourceTypeEnum mPredefinedType = IfcCrewResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcCrewResourceTypeEnum; 
		public IfcCrewResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCrewResource() : base() { }
		internal IfcCrewResource(DatabaseIfc db, IfcCrewResource r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { mPredefinedType = r.mPredefinedType; }
		public IfcCrewResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcCrewResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcCrewResourceTypeEnum mPredefinedType = IfcCrewResourceTypeEnum.NOTDEFINED;
		public IfcCrewResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCrewResourceType() : base() { }
		internal IfcCrewResourceType(DatabaseIfc db, IfcCrewResourceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCrewResourceType(DatabaseIfc m, string name, IfcCrewResourceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcCsgPrimitive3D : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBlock ,IfcRectangularPyramid ,IfcRightCircularCone ,IfcRightCircularCylinder ,IfcSphere))*/
	{
		private int mPosition;// : IfcAxis2Placement3D;
		public IfcAxis2Placement3D Position { get { return mDatabase[mPosition] as IfcAxis2Placement3D; } set { mPosition = value.mIndex; } }

		protected IfcCsgPrimitive3D() : base() { }
		protected IfcCsgPrimitive3D(IfcAxis2Placement3D position) :base (position.mDatabase) { Position = position; }
		protected IfcCsgPrimitive3D(DatabaseIfc db, IfcCsgPrimitive3D p) : base(db, p) { Position = db.Factory.Duplicate(p.Position) as IfcAxis2Placement3D; }
	}
	public partial interface IfcCsgSelect : IBaseClassIfc { } //	IfcBooleanResult, IfcCsgPrimitive3D
	[Serializable]
	public partial class IfcCsgSolid : IfcSolidModel
	{
		private int mTreeRootExpression;// : IfcCsgSelect

		public IfcCsgSelect TreeRootExpression { get { return mDatabase[mTreeRootExpression] as IfcCsgSelect; } set { mTreeRootExpression = value.Index; } }

		internal IfcCsgSolid() : base() { }
		internal IfcCsgSolid(DatabaseIfc db, IfcCsgSolid p) : base(db,p) { TreeRootExpression = db.Factory.Duplicate(p.mDatabase[ p.mTreeRootExpression]) as IfcCsgSelect; }
		public IfcCsgSolid(IfcCsgSelect csg)
			: base(csg.Database)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.Ifc2x3NotAssigned && mDatabase.mModelView != ModelView.Ifc4DesignTransfer)
				throw new Exception("Invalid Model View for IfcCsgSolid : " + mDatabase.ModelView.ToString());
			TreeRootExpression = csg;
		}
	}
	[Serializable]
	public partial class IfcCShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mDepth, mWidth, mWallThickness, mGirth;// : IfcPositiveLengthMeasure;
		internal double mInternalFilletRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mCentreOfGravityInX = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure // DELETED IFC4 	Superseded by respective attribute of IfcStructuralProfileProperties 

		public double Depth { get { return mDepth; } set { mDepth = value; } }
		public double Width { get { return mWidth; } set { mWidth = value; } }
		public double WallThickness { get { return mWallThickness; } set { mWallThickness = value; } }
		public double Girth { get { return mGirth; } set { mGirth = value; } }
		public double InternalFilletRadius { get { return mInternalFilletRadius; } set { mInternalFilletRadius = (value < mDatabase.Tolerance ? double.NaN : value); } }
		
		internal IfcCShapeProfileDef() : base() { }
		internal IfcCShapeProfileDef(DatabaseIfc db, IfcCShapeProfileDef c) : base(db, c)
		{
			mDepth = c.mDepth;
			mWidth = c.mWidth;
			mWallThickness = c.mWallThickness;
			mGirth = c.mGirth;
			mInternalFilletRadius = c.mInternalFilletRadius;
			mCentreOfGravityInX = c.mCentreOfGravityInX;
		}
		public IfcCShapeProfileDef(DatabaseIfc db, string name, double depth, double width, double wallThickness, double girth)
			: base(db,name) { mDepth = depth; mWidth = width; mWallThickness = wallThickness; mGirth = girth; }
	}
	//ENTITY IfcCurrencyRelationship; 
	[Serializable]
	public partial class IfcCurtainWall : IfcBuildingElement
	{
		internal IfcCurtainWallTypeEnum mPredefinedType = IfcCurtainWallTypeEnum.NOTDEFINED;//: OPTIONAL IfcCurtainWallTypeEnum; 
		public IfcCurtainWallTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCurtainWall() : base() { }
		internal IfcCurtainWall(DatabaseIfc db, IfcCurtainWall w, IfcOwnerHistory ownerHistory, bool downStream) : base(db, w, ownerHistory, downStream) { mPredefinedType = w.mPredefinedType; }
		public IfcCurtainWall(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcCurtainWallType : IfcBuildingElementType
	{
		internal IfcCurtainWallTypeEnum mPredefinedType = IfcCurtainWallTypeEnum.NOTDEFINED;
		public IfcCurtainWallTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCurtainWallType() : base() { }
		internal IfcCurtainWallType(DatabaseIfc db, IfcCurtainWallType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcCurtainWallType(DatabaseIfc m, string name, IfcCurtainWallTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcCurve : IfcGeometricRepresentationItem, IfcGeometricSetSelect /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBoundedCurve ,IfcConic ,IfcLine ,IfcOffsetCurve2D ,IfcOffsetCurve3D,IfcPcurve,IfcClothoid))*/
	{   //INVERSE GeomGym   IF Adding a new subtype also consider IfcTrimmedCurve constructor
		internal IfcEdgeCurve mEdge = null;

		protected IfcCurve() : base() { }
		protected IfcCurve(DatabaseIfc db) : base(db) { }
		protected IfcCurve(DatabaseIfc db, IfcCurve c) : base(db,c) { }
	}
	[Serializable]
	public partial class IfcCurveBoundedPlane : IfcBoundedSurface
	{
		internal int mBasisSurface;// : IfcPlane;
		internal int mOuterBoundary;// : IfcCurve;
		internal List<int> mInnerBoundaries = new List<int>();//: SET OF IfcCurve;

		public IfcPlane BasisSurface { get { return mDatabase[mBasisSurface] as IfcPlane; } set { mBasisSurface = value.mIndex; } }
		public IfcCurve OuterBoundary { get { return mDatabase[mOuterBoundary] as IfcCurve; } set { mOuterBoundary = value.mIndex; } }
		public ReadOnlyCollection<IfcCurve> InnerBoundaries { get { return new ReadOnlyCollection<IfcCurve>( mInnerBoundaries.ConvertAll(x => mDatabase[x] as IfcCurve)); } }

		internal IfcCurveBoundedPlane() : base() { }
		internal IfcCurveBoundedPlane(DatabaseIfc db, IfcCurveBoundedPlane p) : base(db,p) { BasisSurface = db.Factory.Duplicate( p.BasisSurface) as IfcPlane; OuterBoundary = db.Factory.Duplicate(p.OuterBoundary) as IfcCurve; p.InnerBoundaries.ToList().ForEach(x=>addInnerBoundary( db.Factory.Duplicate(x) as IfcCurve)); }
		public IfcCurveBoundedPlane(IfcPlane p, IfcCurve outer) : base(p.mDatabase) { BasisSurface = p; OuterBoundary = outer;  }
		public IfcCurveBoundedPlane(IfcPlane p, IfcCurve outer, List<IfcCurve> inner)
			: this(p, outer) { inner.ForEach(x=>addInnerBoundary(x)); }

		internal void addInnerBoundary(IfcCurve boundary) { mInnerBoundaries.Add(boundary.mIndex); }
	}
	[Serializable]
	public partial class IfcCurveBoundedSurface : IfcBoundedSurface //IFC4
	{
		private int mBasisSurface;// : IfcSurface; 
		private List<int> mBoundaries = new List<int>();//: SET [1:?] OF IfcBoundaryCurve;
		private bool mImplicitOuter = false;//	 :	BOOLEAN; 

		public IfcSurface BasisSurface { get { return mDatabase[mBasisSurface] as IfcSurface; } set { mBasisSurface = value.mIndex; } }
		public ReadOnlyCollection<IfcBoundaryCurve> Boundaries { get { return new ReadOnlyCollection<IfcBoundaryCurve>( mBoundaries.ConvertAll(x => mDatabase[x] as IfcBoundaryCurve)); } }
		public bool ImplicitOuter { get { return mImplicitOuter; } }

		internal IfcCurveBoundedSurface() : base() { }
		internal IfcCurveBoundedSurface(DatabaseIfc db, IfcCurveBoundedSurface s) : base(db,s) { BasisSurface = db.Factory.Duplicate( s.BasisSurface) as IfcSurface; s.Boundaries.ToList().ForEach(x => addBoundary(db.Factory.Duplicate(x) as IfcBoundaryCurve)); mImplicitOuter = s.mImplicitOuter; }
		public IfcCurveBoundedSurface(DatabaseIfc m, IfcSurface s, List<IfcBoundaryCurve> bounds)
			: base(m) { mBasisSurface = s.mIndex; mBoundaries = bounds.ConvertAll(x => x.mIndex); mImplicitOuter = false; }

		internal void addBoundary(IfcBoundaryCurve boundary) { mBoundaries.Add(boundary.mIndex); }
	}
	public interface IfcCurveOrEdgeCurve : IBaseClassIfc { }  // = SELECT (	IfcBoundedCurve, IfcEdgeCurve);
	[Serializable]
	public abstract partial class IfcCurveSegment2D : IfcBoundedCurve
	{
		private IfcCartesianPoint mStartPoint;// : IfcCartesianPoint;
		private double mStartDirection;// : IfcPlaneAngleMeasure;
		private double mSegmentLength;// : IfcPositiveLengthMeasure;

		public IfcCartesianPoint StartPoint { get { return mStartPoint; } set { mStartPoint = value; } }
		public double StartDirection { get { return mStartDirection; } set { mStartDirection = value; } }
		public double SegmentLength { get { return mSegmentLength; } set { mSegmentLength = value; } }

		protected IfcCurveSegment2D() : base() { }
		protected IfcCurveSegment2D(DatabaseIfc db, IfcCurveSegment2D p) : base(db, p) { StartPoint = db.Factory.Duplicate(p.StartPoint) as IfcCartesianPoint; mStartDirection = p.mStartDirection; mSegmentLength = p.mSegmentLength; }
		protected IfcCurveSegment2D(IfcCartesianPoint start, double startDirection, double length)
			: base(start.mDatabase)
		{
			mStartPoint = start;
			mStartDirection = startDirection;
			mSegmentLength = length;
		}
	}
	[Serializable]
	public partial class IfcCurveStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal int mCurveFont;// : OPTIONAL IfcCurveFontOrScaledCurveFontSelect;
		internal IfcSizeSelect mCurveWidth = null;// : OPTIONAL IfcSizeSelect; 
		internal int mCurveColour;// : OPTIONAL IfcColour;
		internal IfcLogicalEnum mModelOrDraughting = IfcLogicalEnum.UNKNOWN;//	:	OPTIONAL BOOLEAN; IFC4 

		public IfcCurveFontOrScaledCurveFontSelect CurveFont { get { return mDatabase[mCurveFont] as IfcCurveFontOrScaledCurveFontSelect; } set { mCurveFont = (value == null ? 0 : value.Index); } }
		public IfcSizeSelect CurveWidth { get { return mCurveWidth; } set { mCurveWidth = value; } }
		public IfcColour CurveColour { get { return mDatabase[mCurveColour] as IfcColour; } set { mCurveColour = (value == null ? 0 : value.Index); } }
		public bool ModelOrDraughting { get { return mModelOrDraughting == IfcLogicalEnum.TRUE; } set { mModelOrDraughting = value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE; } }

		internal IfcCurveStyle() : base() { }
		internal IfcCurveStyle(DatabaseIfc db, IfcCurveStyle s) : base(db,s)
		{
			CurveFont = db.Factory.Duplicate(s.mDatabase[s.mCurveFont]) as IfcCurveFontOrScaledCurveFontSelect;
			mCurveWidth = s.mCurveWidth;
			CurveColour = db.Factory.Duplicate(s.mDatabase[s.mCurveColour]) as IfcColour;
			mModelOrDraughting = s.mModelOrDraughting;
		}
		public IfcCurveStyle(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcCurveStyleFont : IfcPresentationItem, IfcCurveStyleFontSelect, NamedObjectIfc
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal List<int> mPatternList = new List<int>();// :  LIST [1:?] OF IfcCurveStyleFontPattern;

		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { if (!string.IsNullOrEmpty(value)) mName = ParserIfc.Encode(value); } }

		internal IfcCurveStyleFont() : base() { }
		//internal IfcCurveStyleFont(DatabaseIfc db, IfcCurveStyleFont v) : base(db,v) { mName = v.mName; mPatternList = new List<int>(v.mPatternList.ToArray()); }
	}
	[Serializable]
	public partial class IfcCurveStyleFontAndScaling : IfcPresentationItem, IfcCurveFontOrScaledCurveFontSelect
	{
		internal string mName; // : 	OPTIONAL IfcLabel;
		internal int mCurveFont;// : 	IfcCurveStyleFontSelect;
		internal IfcPositiveRatioMeasure mCurveFontScaling;//: 	IfcPositiveRatioMeasure;
		internal IfcCurveStyleFontAndScaling() : base() { }
	//	internal IfcCurveStyleFontAndScaling(IfcCurveStyleFontAndScaling i) : base() { mName = i.mName; mCurveFont = i.mCurveFont; mCurveFontScaling = i.mCurveFontScaling; }
	}
	public interface IfcCurveFontOrScaledCurveFontSelect : IBaseClassIfc { } //SELECT (IfcCurveStyleFontAndScaling ,IfcCurveStyleFontSelect);
	[Serializable]
	public partial class IfcCurveStyleFontPattern : IfcPresentationItem
	{
		internal double mVisibleSegmentLength;// : IfcLengthMeasure;
		internal double mInvisibleSegmentLength;//: IfcPositiveLengthMeasure;	
		internal IfcCurveStyleFontPattern() : base() { }
	//	internal IfcCurveStyleFontPattern(IfcCurveStyleFontPattern i) : base() { mVisibleSegmentLength = i.mVisibleSegmentLength; mInvisibleSegmentLength = i.mInvisibleSegmentLength; }
	}
	public interface IfcCurveStyleFontSelect : IfcCurveFontOrScaledCurveFontSelect { } //SELECT (IfcCurveStyleFont ,IfcPreDefinedCurveFont);
	[Serializable]
	public partial class IfcCylindricalSurface : IfcElementarySurface //IFC4
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		public double Radius { get { return mRadius; } set { mRadius = value; } }

		internal IfcCylindricalSurface() : base() { }
		internal IfcCylindricalSurface(DatabaseIfc db, IfcCylindricalSurface s) : base(db,s) { mRadius = s.mRadius; }
		public IfcCylindricalSurface(IfcAxis2Placement3D placement, double radius) : base(placement) { Radius = radius; }
	}
}
