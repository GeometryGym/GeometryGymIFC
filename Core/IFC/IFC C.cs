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
	public partial class Ifc2dCompositeCurve : IfcCompositeCurve
	{
		internal Ifc2dCompositeCurve() : base() { }
		internal Ifc2dCompositeCurve(DatabaseIfc db, Ifc2dCompositeCurve c, DuplicateOptions options) : base(db, c, options) { }
	}
	[Serializable]
	public partial class IfcCableCarrierFitting : IfcFlowFitting //IFC4
	{
		private IfcCableCarrierFittingTypeEnum mPredefinedType = IfcCableCarrierFittingTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableCarrierFittingTypeEnum;
		public IfcCableCarrierFittingTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCableCarrierFittingTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		internal IfcCableCarrierFitting() : base() { }
		internal IfcCableCarrierFitting(DatabaseIfc db, IfcCableCarrierFitting f, DuplicateOptions options) : base(db,f, options) { PredefinedType = f.PredefinedType; }
		public IfcCableCarrierFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCableCarrierFittingType : IfcFlowFittingType
	{
		private IfcCableCarrierFittingTypeEnum mPredefinedType = IfcCableCarrierFittingTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum; 
		public IfcCableCarrierFittingTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCableCarrierFittingTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		internal IfcCableCarrierFittingType() : base() { }
		internal IfcCableCarrierFittingType(DatabaseIfc db, IfcCableCarrierFittingType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcCableCarrierFittingType(DatabaseIfc db, string name, IfcCableCarrierFittingTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcCableCarrierSegment : IfcFlowSegment //IFC4
	{
		private IfcCableCarrierSegmentTypeEnum mPredefinedType = IfcCableCarrierSegmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableCarrierSegmentTypeEnum;
		public IfcCableCarrierSegmentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCableCarrierSegmentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCableCarrierSegment() : base() { }
		internal IfcCableCarrierSegment(DatabaseIfc db, IfcCableCarrierSegment s, DuplicateOptions options) : base(db, s, options) { PredefinedType = s.PredefinedType; }
		public IfcCableCarrierSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCableCarrierSegmentType : IfcFlowSegmentType
	{
		private IfcCableCarrierSegmentTypeEnum mPredefinedType = IfcCableCarrierSegmentTypeEnum.NOTDEFINED;// : IfcCableCarrierSegmentTypeEnum; 
		public IfcCableCarrierSegmentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCableCarrierSegmentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCableCarrierSegmentType() : base() { }
		internal IfcCableCarrierSegmentType(DatabaseIfc db, IfcCableCarrierSegmentType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcCableCarrierSegmentType(DatabaseIfc db, string name, IfcCableCarrierSegmentTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcCableFitting : IfcFlowFitting //IFC4
	{
		private IfcCableFittingTypeEnum mPredefinedType = IfcCableFittingTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableFittingTypeEnum;
		public IfcCableFittingTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCableFittingTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCableFitting() : base() { }
		internal IfcCableFitting(DatabaseIfc db, IfcCableFitting f, DuplicateOptions options) : base(db, f, options) { PredefinedType = f.PredefinedType; }
		public IfcCableFitting(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCableFittingType : IfcFlowFittingType
	{
		private IfcCableFittingTypeEnum mPredefinedType = IfcCableFittingTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum; 
		public IfcCableFittingTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCableFittingTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCableFittingType() : base() { }
		public IfcCableFittingType(DatabaseIfc db, IfcCableFittingType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
	}
	[Serializable]
	public partial class IfcCableSegment : IfcFlowSegment //IFC4
	{
		private IfcCableSegmentTypeEnum mPredefinedType = IfcCableSegmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableSegmentTypeEnum;
		public IfcCableSegmentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCableSegmentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		
		internal IfcCableSegment() : base() { }
		internal IfcCableSegment(DatabaseIfc db, IfcCableSegment s, DuplicateOptions options) : base(db, s, options) { PredefinedType = s.PredefinedType; }
		public IfcCableSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCableSegmentType : IfcFlowSegmentType
	{
		private IfcCableSegmentTypeEnum mPredefinedType = IfcCableSegmentTypeEnum.NOTDEFINED;// : IfcCableSegmentTypeEnum; 
		public IfcCableSegmentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCableSegmentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCableSegmentType() : base() { }
		internal IfcCableSegmentType(DatabaseIfc db, IfcCableSegmentType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.mPredefinedType; }
		public IfcCableSegmentType(DatabaseIfc db, string name, IfcCableSegmentTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
		public IfcCableSegmentType(DatabaseIfc db, string name, IfcMaterialProfileSet mps, IfcCableSegmentTypeEnum t) : base(db) { Name = name; MaterialSelect = mps; PredefinedType = t; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X2)]
	public partial class IfcCaissonFoundation : IfcDeepFoundation
	{
		private IfcCaissonFoundationTypeEnum mPredefinedType = IfcCaissonFoundationTypeEnum.NOTDEFINED; //: OPTIONAL IfcCaissonFoundationTypeEnum;
		public IfcCaissonFoundationTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCaissonFoundationTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcCaissonFoundation() : base() { }
		public IfcCaissonFoundation(DatabaseIfc db) : base(db) { }
		public IfcCaissonFoundation(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X2)]
	public partial class IfcCaissonFoundationType : IfcDeepFoundationType
	{
		private IfcCaissonFoundationTypeEnum mPredefinedType = IfcCaissonFoundationTypeEnum.NOTDEFINED; //: IfcCaissonFoundationTypeEnum;
		public IfcCaissonFoundationTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCaissonFoundationTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcCaissonFoundationType() : base() { }
		public IfcCaissonFoundationType(DatabaseIfc db, string name, IfcCaissonFoundationTypeEnum predefinedType)
			: base(db, name) { PredefinedType = predefinedType; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcCalendarDate : BaseClassIfc, IfcDateTimeSelect //DEPRECATED IFC4
	{
		internal int mDayComponent;//  : IfcDayInMonthNumber;
		internal int mMonthComponent;//  : IfcMonthInYearNumber;
		internal int mYearComponent;// : IfcYearNumber; 

		internal IfcCalendarDate() : base() { }
		internal IfcCalendarDate(DatabaseIfc db, IfcCalendarDate d) : base(db,d) { mDayComponent = d.mDayComponent; mMonthComponent = d.mMonthComponent; mYearComponent = d.mYearComponent; }
		public IfcCalendarDate(DatabaseIfc db, DateTime date) : this(db, date.Day, date.Month, date.Year) { }
		public IfcCalendarDate(DatabaseIfc db, int day, int month, int year) : base(db)
		{
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

		public double CoordinateX { get { return mCoordinateX; } set { mCoordinateX = value; } }
		public double CoordinateY { get { return mCoordinateY; } set { mCoordinateY = value; } }
		public double CoordinateZ { get { return mCoordinateZ; } set { mCoordinateZ = value; } }

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
						if (value.Count > 2)
							mCoordinateZ = value[2];
					}
				}
			}
		}
		public Tuple<double, double, double> Tuple() { return new Tuple<double, double, double>(mCoordinateX, double.IsNaN(mCoordinateY) ? 0 : mCoordinateY, double.IsNaN(mCoordinateZ) ? 0 : mCoordinateZ); }
		internal IfcCartesianPoint() : base() { }
		internal IfcCartesianPoint(DatabaseIfc db, IfcCartesianPoint p, DuplicateOptions options) : base(db, p, options) { mCoordinateX = p.mCoordinateX; mCoordinateY = p.mCoordinateY; mCoordinateZ = p.mCoordinateZ; }

		public IfcCartesianPoint(DatabaseIfc db, double x, double y) : base(db) { mCoordinateX = x; mCoordinateY = y; mCoordinateZ = double.NaN; }
		public IfcCartesianPoint(DatabaseIfc db, double x, double y, double z) : base(db) { mCoordinateX = x; mCoordinateY = y; mCoordinateZ = z; }

		internal bool is2D { get { return double.IsNaN(mCoordinateZ); } }
		internal override bool isOrigin(double tol) 
		{
			return ((double.IsNaN(mCoordinateX) || Math.Abs(mCoordinateX) < tol) && 
				(double.IsNaN(mCoordinateY) || Math.Abs(mCoordinateY) < tol) && 
				(double.IsNaN(mCoordinateZ) || Math.Abs(mCoordinateZ) < tol)); 
		}

		private Tuple<double, double, double> SerializeCoordinates
		{
			get
			{
				//Also adjust similar method for length in baseclass
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
		internal void Scale(double scaleFactor)
		{
			mCoordinateX *= scaleFactor;
			mCoordinateY *= scaleFactor;
			if (!is2D)
				mCoordinateZ *= scaleFactor;
		}
	}
	[Serializable]
	public abstract partial class IfcCartesianPointList : IfcGeometricRepresentationItem //IFC4 // SUPERTYPE OF(IfcCartesianPointList2D, IfcCartesianPointList3D)
	{
		protected IfcCartesianPointList() : base() { }
		protected IfcCartesianPointList(DatabaseIfc db) : base(db) { }
		protected IfcCartesianPointList(DatabaseIfc db, IfcCartesianPointList l, DuplicateOptions options) : base(db, l, options) { }
	}
	[Serializable]
	public partial class IfcCartesianPointList2D : IfcCartesianPointList //IFC4
	{
		internal List<Tuple<double, double>> mCoordList = new List<Tuple<double, double>>();//	 :	LIST [1:?] OF LIST [2:2] OF IfcLengthMeasure; 
		internal List<string> mTagList = new List<string>(); // : OPTIONAL LIST [1:?] OF IfcLabel;
		public List<Tuple<double,double>> CoordList
		{
			get { return mCoordList; }
			set { mCoordList = value; }
		}
		public List<string> TagList { get { return mTagList; } }

		internal IfcCartesianPointList2D() : base() { }
		internal IfcCartesianPointList2D(DatabaseIfc db, IfcCartesianPointList2D l, DuplicateOptions options)
			: base(db, l, options) 
		{ 
			mCoordList.AddRange(l.mCoordList);
			if(l.mTagList.Count > 0)
				mTagList.AddRange(l.mTagList);
		}
		public IfcCartesianPointList2D(DatabaseIfc db, IEnumerable<Tuple<double, double>> coordList) : base(db) { mCoordList.AddRange(coordList); }
	}
	[Serializable]
	public partial class IfcCartesianPointList3D : IfcCartesianPointList //IFC4
	{
		private List<Tuple<double,double,double>> mCoordList = new List<Tuple<double,double,double>>();//	 :	LIST [1:?] OF LIST [3:3] OF IfcLengthMeasure; 
		internal List<string> mTagList = new List<string>(); // : OPTIONAL LIST [1:?] OF IfcLabel;
		public List<Tuple<double, double, double>> CoordList { get { return mCoordList; } }
		public List<string> TagList { get { return mTagList; } }
		internal IfcCartesianPointList3D() : base() { }
		internal IfcCartesianPointList3D(DatabaseIfc db, IfcCartesianPointList3D l, DuplicateOptions options) 
			: base(db, l, options) 
		{ 
			mCoordList.AddRange(l.mCoordList); 
			if(l.mTagList.Count > 0)
				mTagList.AddRange(l.mTagList);
		}

		public IfcCartesianPointList3D(DatabaseIfc db, IEnumerable<Tuple<double, double, double>> coordList) : base(db)
		{
			mCoordList.AddRange(coordList);
		}
	}
	[Serializable]
	public abstract partial class IfcCartesianTransformationOperator : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCartesianTransformationOperator2D ,IfcCartesianTransformationOperator3D))*/
	{ 
		protected IfcDirection mAxis1;// : OPTIONAL IfcDirection
		protected IfcDirection mAxis2;// : OPTIONAL IfcDirection;
		private IfcCartesianPoint mLocalOrigin;// : IfcCartesianPoint;
		private double mScale = double.NaN;// : OPTIONAL REAL;

		public IfcDirection Axis1 { get { return mAxis1; } set { mAxis1 = value; } }
		public IfcDirection Axis2 { get { return mAxis2; } set { mAxis2 = value; } }
		public IfcCartesianPoint LocalOrigin { get { return mLocalOrigin; } set { mLocalOrigin = value; } }
		public double Scale { get { return double.IsNaN(mScale) ? 1 : mScale; } set { mScale = value; } }

		protected IfcCartesianTransformationOperator() { }
		protected IfcCartesianTransformationOperator(IfcCartesianPoint p) : base(p.mDatabase) { LocalOrigin = p; }
		protected IfcCartesianTransformationOperator(DatabaseIfc db) : base(db)
		{
			LocalOrigin = db.Factory.Origin;
		}
		protected IfcCartesianTransformationOperator(DatabaseIfc db, IfcCartesianTransformationOperator o, DuplicateOptions options) 
			: base(db, o, options)
		{
			if (o.mAxis1 != null)
				Axis1 = db.Factory.Duplicate(o.Axis1, options);
			if (o.mAxis2 != null)
				Axis2 = db.Factory.Duplicate(o.Axis2, options);
			LocalOrigin = db.Factory.Duplicate(o.LocalOrigin, options);
			mScale = o.mScale;
		}
	}
	[Serializable]
	public partial class IfcCartesianTransformationOperator2D : IfcCartesianTransformationOperator // SUPERTYPE OF(IfcCartesianTransformationOperator2DnonUniform)
	{
		internal IfcCartesianTransformationOperator2D() : base() { }
		internal IfcCartesianTransformationOperator2D(DatabaseIfc db, IfcCartesianTransformationOperator2D o, DuplicateOptions options) : base(db, o, options) { }
		public IfcCartesianTransformationOperator2D(IfcCartesianPoint cp) : base(cp) { }
		public IfcCartesianTransformationOperator2D(DatabaseIfc db) : base(new IfcCartesianPoint(db, 0, 0)) { }
	}
	[Serializable]
	public partial class IfcCartesianTransformationOperator2DnonUniform : IfcCartesianTransformationOperator2D
	{
		private double mScale2 = double.NaN; //OPTIONAL REAL;
		public double Scale2 { get { return double.IsNaN(mScale2) ? Scale : mScale2; } set { mScale2 = value; } }

		internal IfcCartesianTransformationOperator2DnonUniform() : base() { }
		internal IfcCartesianTransformationOperator2DnonUniform(DatabaseIfc db, IfcCartesianTransformationOperator2DnonUniform o, DuplicateOptions options) : base(db, o, options) { mScale2 = o.mScale2; }
		public IfcCartesianTransformationOperator2DnonUniform(IfcCartesianPoint cp) : base(cp) { }
	}
	[Serializable]
	public partial class IfcCartesianTransformationOperator3D : IfcCartesianTransformationOperator //SUPERTYPE OF(IfcCartesianTransformationOperator3DnonUniform)
	{
		private IfcDirection mAxis3;// : OPTIONAL IfcDirection
		public IfcDirection Axis3 { get { return mAxis3; } set { mAxis3 = value; } }

		internal IfcCartesianTransformationOperator3D() { }
		internal IfcCartesianTransformationOperator3D(DatabaseIfc db, IfcCartesianTransformationOperator3D o, DuplicateOptions options) : base(db, o, options) { if(o.mAxis3 != null) Axis3 = db.Factory.Duplicate( o.Axis3) as IfcDirection; }
		public IfcCartesianTransformationOperator3D(DatabaseIfc db) : base(db) { }
		public IfcCartesianTransformationOperator3D(IfcCartesianPoint localOrigin) : base(localOrigin) { }
		
		internal IfcAxis2Placement3D generate()
		{
			if (LocalOrigin.isOrigin(mDatabase.Tolerance) && (mAxis1 == null || Axis1.isXAxis) && (mAxis2 == null || Axis2.isYAxis))
				return mDatabase.Factory.XYPlanePlacement;
			return new IfcAxis2Placement3D(LocalOrigin, mAxis3 == null ? mDatabase.Factory.ZAxis : Axis3, mAxis1 == null ? mDatabase.Factory.XAxis : Axis1);
		}
	}
	[Serializable]
	public partial class IfcCartesianTransformationOperator3DnonUniform : IfcCartesianTransformationOperator3D
	{
		private double mScale2 = 1;// : OPTIONAL REAL;
		private double mScale3 = 1;// : OPTIONAL REAL; 

		public double Scale2 { get { return (double.IsNaN(mScale2) ? Scale : mScale2); } set { mScale2 = value; } }
		public double Scale3 { get { return double.IsNaN(mScale3) ? Scale : mScale3; } set { mScale3 = value; } }

		internal IfcCartesianTransformationOperator3DnonUniform() { }
		internal IfcCartesianTransformationOperator3DnonUniform(DatabaseIfc db, IfcCartesianTransformationOperator3DnonUniform o, DuplicateOptions options) : base(db, o, options) { mScale2 = o.mScale2; mScale3 = o.mScale3; }
		public IfcCartesianTransformationOperator3DnonUniform(IfcCartesianPoint localOrigin) : base(localOrigin) { }
	}
	[Serializable]
	public partial class IfcCenterLineProfileDef : IfcArbitraryOpenProfileDef
	{
		internal double mThickness;// : IfcPositiveLengthMeasure;
		public double Thickness { get { return mThickness; } set { mThickness = value; } }

		internal IfcCenterLineProfileDef() : base() { }
		internal IfcCenterLineProfileDef(DatabaseIfc db, IfcCenterLineProfileDef p, DuplicateOptions options) : base(db, p, options) { mThickness = p.mThickness; }
		public IfcCenterLineProfileDef(string name, IfcBoundedCurve curve, double thickness) : base(name, curve) { mThickness = thickness; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcChamferEdgeFeature : IfcEdgeFeature //DEPRECATED IFC4
	{
		internal double mWidth;// : OPTIONAL IfcPositiveLengthMeasure
		internal double mHeight;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcChamferEdgeFeature() : base() { }
		internal IfcChamferEdgeFeature(DatabaseIfc db, IfcChamferEdgeFeature f, DuplicateOptions options) : base(db, f, options) { mWidth = f.mWidth; mHeight = f.mHeight; }
	}
	public interface IfcCharacterStyleSelect : IBaseClassIfc { } // SELECT(IfcTextStyleForDefinedFont);
	[Serializable]
	public partial class IfcChiller : IfcEnergyConversionDevice //IFC4
	{
		private IfcChillerTypeEnum mPredefinedType = IfcChillerTypeEnum.NOTDEFINED;// OPTIONAL : IfctypeEnum;
		public IfcChillerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcChillerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcChiller() : base() { }
		internal IfcChiller(DatabaseIfc db, IfcChiller c, DuplicateOptions options) : base(db, c, options) { PredefinedType = c.PredefinedType; }
		public IfcChiller(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcChillerType : IfcEnergyConversionDeviceType
	{
		private IfcChillerTypeEnum mPredefinedType = IfcChillerTypeEnum.NOTDEFINED;// : IfcChillerTypeEnum
		public IfcChillerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcChillerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcChillerType() : base() { }
		internal IfcChillerType(DatabaseIfc db, IfcChillerType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcChillerType(DatabaseIfc db, string name, IfcChillerTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcChimney : IfcBuiltElement
	{
		private IfcChimneyTypeEnum mPredefinedType = IfcChimneyTypeEnum.NOTDEFINED;//: OPTIONAL IfcChimneyTypeEnum; 
		public IfcChimneyTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcChimneyTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcChimney() : base() { }
		internal IfcChimney(DatabaseIfc db, IfcChimney c, DuplicateOptions options) : base(db, c, options) { PredefinedType = c.PredefinedType; }
		public IfcChimney(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcChimneyType : IfcBuiltElementType
	{
		private IfcChimneyTypeEnum mPredefinedType = IfcChimneyTypeEnum.NOTDEFINED;
		public IfcChimneyTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcChimneyTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcChimneyType() : base() { } 
		internal IfcChimneyType(DatabaseIfc db, IfcChimneyType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcChimneyType(DatabaseIfc db, string name, IfcChimneyTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcCircle : IfcConic
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		public double Radius { get { return mRadius; } set { mRadius = value; } }

		internal IfcCircle() : base() { }
		internal IfcCircle(DatabaseIfc db, IfcCircle c, DuplicateOptions options) : base(db, c, options) { mRadius = c.mRadius; }
		public IfcCircle(DatabaseIfc db, double radius) : base(db.Factory.Origin2dPlace) { mRadius = radius; }
		public IfcCircle(IfcAxis2Placement ap, double radius) : base(ap) { mRadius = radius; }
	}
	[Serializable]
	public partial class IfcCircleHollowProfileDef : IfcCircleProfileDef
	{
		public override string StepClassName { get { return (mWallThickness < (mDatabase == null ? 1e-6 : mDatabase.Tolerance) ? "IfcCircleProfileDef" : base.StepClassName); } }

		internal double mWallThickness;// : IfcPositiveLengthMeasure;
		public double WallThickness { get { return mWallThickness; } set { mWallThickness = value; } }

		internal IfcCircleHollowProfileDef() : base() { }
		internal IfcCircleHollowProfileDef(DatabaseIfc db, IfcCircleHollowProfileDef c, DuplicateOptions options) : base(db, c, options) { mWallThickness = c.mWallThickness; }
		public IfcCircleHollowProfileDef(DatabaseIfc db, string name, double radius, double wallThickness) : base(db, name, radius) { mWallThickness = wallThickness; }
	}
	[Serializable]
	public partial class IfcCircleProfileDef : IfcParameterizedProfileDef //SUPERTYPE OF(IfcCircleHollowProfileDef)
	{
		internal double mRadius;// : IfcPositiveLengthMeasure;		
		public double Radius { get { return mRadius; } set { mRadius = value; } }
		internal IfcCircleProfileDef() : base() { }
		internal IfcCircleProfileDef(DatabaseIfc db, IfcCircleProfileDef c, DuplicateOptions options) : base(db, c, options) { mRadius = c.mRadius; }
		public IfcCircleProfileDef(DatabaseIfc db, string name, double radius) : base(db,name) { mRadius = radius; }
	}
	[Obsolete("DEPRECATED IFC4X3", false)]
	[Serializable]
	public partial class IfcCircularArcSegment2D : IfcCurveSegment2D  //IFC4.1
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		private bool mIsCCW;// : IfcBoolean;

		public double Radius { get { return mRadius; } set { mRadius = value; } }
		public bool IsCCW { get { return mIsCCW; } set { mIsCCW = value; } }

		internal IfcCircularArcSegment2D() : base() { }
		internal IfcCircularArcSegment2D(DatabaseIfc db, IfcCircularArcSegment2D s, DuplicateOptions options) : base(db, s, options) { mRadius = s.mRadius; mIsCCW = s.mIsCCW; }
		public IfcCircularArcSegment2D(IfcCartesianPoint start, double startDirection, double length, double radius, bool isCCW)
			: base(start, startDirection, length)
		{
			mRadius = radius;
			mIsCCW = isCCW;
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4)]
	public partial class IfcCivilElement : IfcElement  //IFC4
	{
		internal IfcCivilElement() : base() { }
		internal IfcCivilElement(DatabaseIfc db, IfcCivilElement e, DuplicateOptions options) : base(db, e, options) { }
		public IfcCivilElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { if (mDatabase.mRelease < ReleaseVersion.IFC4) throw new Exception(StepClassName + " only supported in IFC4!"); }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4)]
	public partial class IfcCivilElementType : IfcElementType //IFC4
	{
		internal IfcCivilElementType() : base() { }
		internal IfcCivilElementType(DatabaseIfc db, IfcCivilElementType t, DuplicateOptions options) : base(db, t, options) { }
		public IfcCivilElementType(DatabaseIfc db, string name) : base(db) { Name = name; }
	}
	[Serializable]
	public partial class IfcClassification : IfcExternalInformation, IfcClassificationReferenceSelect, IfcClassificationSelect, NamedObjectIfc //	SUBTYPE OF IfcExternalInformation;
	{
		internal string mSource = ""; //  : OPTIONAL IfcLabel;
		internal string mEdition = ""; //  : OPTIONAL IfcLabel;
		internal DateTime mEditionDate = DateTime.MinValue; // : OPTIONAL IfcDate IFC4 change 
		private IfcCalendarDate mEditionDateSS =  null; // : OPTIONAL IfcCalendarDate;
		internal string mName;//  : IfcLabel;
		internal string mDescription = "";//	 :	OPTIONAL IfcText; IFC4 Addition
		internal string mSpecification = "";//	 :	OPTIONAL IfcURIReference; IFC4 Addition
		internal List<string> mReferenceTokens = new List<string>();//	 :	OPTIONAL LIST [1:?] OF IfcIdentifier; IFC4 Addition
		//INVERSE 
		internal SET<IfcRelAssociatesClassification> mClassificationForObjects = new SET<IfcRelAssociatesClassification>();//	 :	SET OF IfcRelAssociatesclassification FOR Relatingclassification;
		internal SET<IfcClassificationReference> mHasReferences = new SET<IfcClassificationReference>();//	 :	SET OF IfcClassificationReference FOR ReferencedSource;

		public string Source { get { return mSource; } set { mSource = value; } }
		public string Edition { get { return mEdition; } set { mEdition = value; } }
		public DateTime EditionDate
		{
			get { return (mEditionDateSS != null ? mEditionDateSS.DateTime : mEditionDate); }
			set
			{
				if (mDatabase.Release <= ReleaseVersion.IFC2x3)
				{
					if (value > DateTime.MinValue)
						mEditionDateSS = new IfcCalendarDate(mDatabase, value.Day, value.Month, value.Year);
				}
				else
					mEditionDate = value;
			}
		}
		public string Name { get { return mName; } set {  mName = (string.IsNullOrEmpty(value) ? "Unknown" : value); } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public string Specification { get { return mSpecification; } set { mSpecification = value; } }
		public List<string> ReferenceTokens { get { return mReferenceTokens.ConvertAll(x => ParserSTEP.Decode(x)); } }
		public SET<IfcRelAssociatesClassification> ClassificationForObjects { get { return mClassificationForObjects; } }
		public SET<IfcClassificationReference> HasReferences { get { return mHasReferences; } }
		[Obsolete("RENAMED IFC4X3 to Specification", false)]
		public string Location { get { return mSpecification; } set { mSpecification = value; } }

		internal IfcClassification() : base() { }
		internal IfcClassification(DatabaseIfc db, IfcClassification c) 
			: base(db, c) { mSource = c.mSource; mEdition = c.mEdition; mEditionDate = c.mEditionDate; mName = c.mName; mDescription = c.mDescription; mSpecification = c.mSpecification; mReferenceTokens.AddRange(c.mReferenceTokens); }
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
		public IfcRelAssociatesClassification Associate(IfcDefinitionSelect related)
		{
			if (mClassificationForObjects.Count == 0)
				return new IfcRelAssociatesClassification(this, related);
			else
			{
				IfcRelAssociatesClassification associates = mClassificationForObjects.First();
				associates.RelatedObjects.Add(related);
				return associates;
			}
		}

		public IfcClassificationReference FindItem(string identification, bool prefixHierarchy)
		{
			foreach (IfcClassificationReference classificationReference in HasReferences)
			{
				if (prefixHierarchy && !identification.StartsWith(classificationReference.Identification))
					continue;
				IfcClassificationReference result = classificationReference.FindItem(identification, prefixHierarchy);
				if (result != null)
					return result;
			}
			return null;
		}
		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcClassification classification = e as IfcClassification;
			if (classification == null || !base.isDuplicate(e, tol))
				return false;

			if (string.Compare(Specification, classification.Specification, true) != 0)
				return false;
			if (string.Compare(Name, classification.Name, true) != 0)
				return false;
			if (string.Compare(Edition, classification.Edition, true) != 0)
				return false;
			return true;
		}
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcClassificationItem : BaseClassIfc //DEPRECATED IFC4
	{
		internal IfcClassificationNotationFacet mNotation;// : IfcClassificationNotationFacet;
		internal IfcClassification mItemOf;//: OPTIONAL IfcClassification;
		internal string mTitle;// : IfcLabel; 

		internal IfcClassificationItem() : base() { }
		//internal IfcClassificationItem( IfcClassificationItem i) : base() { mNotation = i.mNotation; mItemOf = i.mItemOf; mTitle = i.mTitle; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcClassificationItemRelationship : BaseClassIfc //DEPRECATED IFC4
	{
		internal string mSource;// : IfcLabel;
		internal string mEdition;// : IfcLabel;
		internal IfcCalendarDate mEditionDate;// : OPTIONAL IfcCalendarDate;
		internal string mName;// : IfcLabel;
		internal IfcClassificationItemRelationship() : base() { }
		internal IfcClassificationItemRelationship(DatabaseIfc db) : base(db) { }
		//internal IfcClassificationItemRelationship(IfcClassificationItemRelationship i) : base()
		//{
		//	mSource = i.mSource;
		//	mEdition = i.mEdition;
		//	mEditionDate = i.mEditionDate;
		//	mName = i.mName;
		//}
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcClassificationNotation : BaseClassIfc, IfcClassificationNotationSelect //DEPRECATED IFC4
	{
		internal SET<IfcClassificationNotationFacet> mNotationFacets = new SET<IfcClassificationNotationFacet>();// : SET [1:?] OF IfcClassificationNotationFacet;

		internal IfcClassificationNotation() : base() { }
		internal IfcClassificationNotation(DatabaseIfc db) : base(db) { }
		//internal IfcClassificationNotation(IfcClassificationNotation i) : base() { mNotationFacets = new List<int>(i.mNotationFacets.ToArray()); }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcClassificationNotationFacet : BaseClassIfc  //DEPRECATED IFC4
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
		internal string mDescription = "";// :	OPTIONAL IfcText; IFC4
		internal string mSort = "";//	 :	OPTIONAL IfcIdentifier;
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
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public string Sort { get { return mSort; } set { mSort = value; } }
		public SET<IfcRelAssociatesClassification> ClassificationRefForObjects { get { return mClassificationRefForObjects; } }
		public SET<IfcClassificationReference> HasReferences { get { return mHasReferences; } }

		internal IfcClassificationReference() : base() { }
		internal IfcClassificationReference(DatabaseIfc db, IfcClassificationReference r, DuplicateOptions options) : base(db, r, options)
		{
			if (options.DuplicateHost)
			{
				if (db.Release < ReleaseVersion.IFC4)
				{
					IfcClassification classification = r.ReferencedClassification();
					if (classification != null)
						ReferencedSource = db.Factory.Duplicate(classification) as IfcClassification;
				}
				else
					ReferencedSource = db.Factory.Duplicate(r.ReferencedSource) as IfcClassificationReferenceSelect;
			}
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
			IfcRelAssociatesClassification associates = mClassificationRefForObjects.FirstOrDefault();
			if (associates == null)
				new IfcRelAssociatesClassification(this, related);
			else if (!associates.RelatedObjects.Contains(related))
				associates.RelatedObjects.Add(related);
		}
		public void Relate(IfcResourceObjectSelect resource)
		{
			IfcExternalReferenceRelationship reference = mExternalReferenceForResources.FirstOrDefault();
			if (reference == null)
				new IfcExternalReferenceRelationship(this, resource);
			else if (!reference.RelatedResourceObjects.Contains(resource))
				reference.RelatedResourceObjects.Add(resource);
		}
		public IfcClassificationReference FindItem(string identification, bool prefixHierarchy)
		{
			if (string.Compare(identification, Identification, true) == 0)
				return this;
			foreach (IfcClassificationReference classificationReference in HasReferences)
			{
				if (prefixHierarchy && !identification.StartsWith(classificationReference.Identification))
					continue;
				IfcClassificationReference result = classificationReference.FindItem(identification, prefixHierarchy);
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
		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcClassificationReference classificationReference = e as IfcClassificationReference;
			if (classificationReference == null || !base.isDuplicate(e, tol))
				return false;
			if(mReferencedSource != null && classificationReference.ReferencedSource != null)
			{
				if (!(mReferencedSource as BaseClassIfc).isDuplicate(classificationReference.ReferencedSource as BaseClassIfc, tol))
					return false;
			}

			return true;
		}
	}
	public interface IfcClassificationReferenceSelect : IBaseClassIfc { SET<IfcClassificationReference> HasReferences { get; } } // SELECT ( IfcClassificationReference, IfcClassification);
	public interface IfcClassificationSelect : NamedObjectIfc // IFC4 rename IfcClassification,IfcClassificationReference
	{
		IfcClassificationReference FindItem(string identification, bool prefixHierarchy);
	}  
	[Serializable]
	public partial class IfcClosedShell : IfcConnectedFaceSet, IfcShell, IfcSolidOrShell
	{
		internal IfcClosedShell() : base() { }
		internal IfcClosedShell(DatabaseIfc db, IfcClosedShell c, DuplicateOptions options) : base(db, c, options) { }
		public IfcClosedShell(IEnumerable<IfcFace> faces) : base(faces) { }
	}
	[Serializable]
	public partial class IfcClothoid : IfcSpiral
	{
		private double mClothoidConstant = 0; //: IfcLengthMeasure;
		public double ClothoidConstant { get { return mClothoidConstant; } set { mClothoidConstant = value; } }

		public IfcClothoid() : base() { }
		internal IfcClothoid(DatabaseIfc db, IfcClothoid clothoid, DuplicateOptions options)
			:base(db, clothoid, options) { ClothoidConstant = clothoid.ClothoidConstant; }
		public IfcClothoid(IfcAxis2Placement position, double clothoidConstant)
			: base(position) { ClothoidConstant = clothoidConstant; }
	}
	[Serializable]
	public partial class IfcCoil : IfcEnergyConversionDevice //IFC4
	{
		private IfcCoilTypeEnum mPredefinedType = IfcCoilTypeEnum.NOTDEFINED;// OPTIONAL : IfcCoilTypeEnum;
		public IfcCoilTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCoilTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCoil() : base() { }
		internal IfcCoil(DatabaseIfc db, IfcCoil c, DuplicateOptions options) : base(db, c, options) { PredefinedType = c.PredefinedType; }
		public IfcCoil(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCoilType : IfcEnergyConversionDeviceType
	{
		private IfcCoilTypeEnum mPredefinedType = IfcCoilTypeEnum.NOTDEFINED;// : IfcCoilTypeEnum;
		public IfcCoilTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCoilTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCoilType() : base() { }
		internal IfcCoilType(DatabaseIfc db, IfcCoilType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcCoilType(DatabaseIfc db, string name, IfcCoilTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	public partial interface IfcColour : IBaseClassIfc, IfcFillStyleSelect { }// = SELECT (IfcColourSpecification ,IfcPreDefinedColour); 
	public partial interface IfcColourOrFactor : IBaseClassIfc { } // IfcNormalisedRatioMeasure, IfcColourRgb);
	[Serializable]
	public partial class IfcColourRgb : IfcColourSpecification, IfcColourOrFactor
	{
		internal double mRed, mGreen, mBlue;// : IfcNormalisedRatioMeasure;

		public double Red { get { return mRed; } set { mRed = value; } }
		public double Green { get { return mGreen; } set { mGreen = value; } }
		public double Blue { get { return mBlue; } set { mBlue = value; } }
		
		internal IfcColourRgb() : base() { }
		internal IfcColourRgb(DatabaseIfc db, IfcColourRgb c, DuplicateOptions options) : base(db, c, options) { mRed = c.mRed; mGreen = c.mGreen; mBlue = c.mBlue; }
		public IfcColourRgb(DatabaseIfc db, double red, double green, double blue) : base(db) { mRed = red; mGreen = green; mBlue = blue; }		
	}
	[Serializable]
	public partial class IfcColourRgbList : IfcPresentationItem
	{
		internal List<Tuple<double, double, double>> mColourList = new List<Tuple<double, double, double>>();//	: LIST [1:?] OF LIST [3:3] OF IfcNormalisedRatioMeasure; 
		internal IfcColourRgbList() : base() { }
		internal IfcColourRgbList(DatabaseIfc db, IfcColourRgbList l, DuplicateOptions options) : base(db,l, options) { mColourList.AddRange(l.mColourList); }
	}
	[Serializable]
	public abstract partial class IfcColourSpecification : IfcPresentationItem, IfcColour, NamedObjectIfc //	ABSTRACT SUPERTYPE OF(IfcColourRgb)
	{
		private string mName = "";// : OPTIONAL IfcLabel
		public string Name { get { return mName; } set { mName = value; } }

		protected IfcColourSpecification() : base() { }
		protected IfcColourSpecification(DatabaseIfc db, IfcColourSpecification s, DuplicateOptions options) : base(db, s, options) { mName = s.mName; }
		protected IfcColourSpecification(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcColumn : IfcBuiltElement
	{
		private IfcColumnTypeEnum mPredefinedType = IfcColumnTypeEnum.NOTDEFINED;//: OPTIONAL IfcColumnTypeEnum; 
		public IfcColumnTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcColumnTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		
		internal IfcColumn() : base()  { }
		internal IfcColumn(DatabaseIfc db, IfcColumn c, DuplicateOptions options) : base(db, c, options) { PredefinedType = c.PredefinedType; }
		public IfcColumn(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		public IfcColumn(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double height) : base(host, profile, placement, height) { }
	}
	[Serializable]
	public partial class IfcColumnStandardCase : IfcColumn
	{
		public override string StepClassName { get { return "IfcColumn"; } }
		internal IfcColumnStandardCase() : base() { }
		internal IfcColumnStandardCase(DatabaseIfc db, IfcColumnStandardCase c, DuplicateOptions options) : base(db, c, options) { }
		public IfcColumnStandardCase(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double height) : base(host, profile, placement, height) { }
	} 
	[Serializable]
	public partial class IfcColumnType : IfcBuiltElementType
	{
		private IfcColumnTypeEnum mPredefinedType = IfcColumnTypeEnum.NOTDEFINED;
		public IfcColumnTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcColumnTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcColumnType() : base() { }
		internal IfcColumnType(DatabaseIfc db, IfcColumnType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcColumnType(DatabaseIfc db, string name, IfcColumnTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
		public IfcColumnType(string name, IfcMaterialProfile ps, IfcColumnTypeEnum type) : this(name,new IfcMaterialProfileSet(ps.Name, ps), type) { }
		public IfcColumnType(string name, IfcMaterialProfileSet ps, IfcColumnTypeEnum type) : base(ps.mDatabase)
		{
			Name = name;
			PredefinedType = type;
			if (ps.mTaperEnd != null)
				mTapering = ps;
			else
				MaterialSelect = ps;
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcComplementaryData : IfcProduct
	{
		internal IfcComplementaryData() : base() { }
		public IfcComplementaryData(DatabaseIfc db) : base(db) { }
		internal IfcComplementaryData(DatabaseIfc db, IfcComplementaryData d, DuplicateOptions options) : base(db, d, options) { }
		public IfcComplementaryData(IfcProduct host) : base(host.mDatabase) { host.AddElement(this); }
		public IfcComplementaryData(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation)
			: base(host.Database)
		{
			if (host is IfcSpatialElement spatialElement)
				spatialElement.AddElement(this);
			else
				host.AddAggregated(this);
			ObjectPlacement = placement;
			Representation = representation;
		}
	}
	[Serializable]
	public partial class IfcComplexProperty : IfcProperty
	{
		public override string StepClassName { get { return "IfcComplexProperty"; } }
		internal string mUsageName;// : IfcIdentifier;
		private Dictionary<string, IfcProperty> mHasProperties = new Dictionary<string, IfcProperty>();// : SET [1:?] OF IfcProperty;

		public string UsageName { get { return mUsageName; } set { mUsageName = (string.IsNullOrEmpty(value) ? "Unknown" : value); } }
		public Dictionary<string, IfcProperty> HasProperties { get { return mHasProperties; } }

		internal IfcComplexProperty() : base() { }
		internal IfcComplexProperty(DatabaseIfc db, IfcComplexProperty p, DuplicateOptions options) : base(db, p, options) 
		{ 
			mUsageName = p.mUsageName; 
			foreach(IfcProperty prop in p.HasProperties.Values) 
				addProperty(db.Factory.DuplicateProperty(prop, options)); 
		}
		public IfcComplexProperty(DatabaseIfc db, string name, string usageName) : base(db, name) { UsageName = usageName; }
		public IfcComplexProperty(DatabaseIfc db, string name, string usageName, IEnumerable<IfcProperty> properties) : this(db, name, usageName) { foreach (IfcProperty p in properties) addProperty(p); }
		
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
			}
		}
		public void RemoveProperty(IfcProperty property)
		{
			if (property != null)
			{
				mHasProperties.Remove(property.Name);
				property.mPartOfComplex.Remove(this);
			}
		}
		public IfcProperty FindProperty(string name)
		{
			IfcProperty result = this[name];
			if (result != null)
				return result;
			foreach (IfcComplexProperty complexProperty in mHasProperties.OfType<IfcComplexProperty>())
			{
				result = complexProperty.FindProperty(name);
				if (result != null)
					return result;
			}
			return null;
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
		private Dictionary<string, IfcPropertyTemplate> mHasPropertyTemplates = new Dictionary<string, IfcPropertyTemplate>();//  : SET [1:?] OF IfcPropertyTemplate;

		public string UsageName { get { return mUsageName; } set { mUsageName = value; } }
		public IfcComplexPropertyTemplateTypeEnum TemplateType { get { return mTemplateType; } set { mTemplateType = value; } }
		public Dictionary<string,IfcPropertyTemplate> HasPropertyTemplates { get { return mHasPropertyTemplates; } }

		internal IfcComplexPropertyTemplate() : base() { }
		public IfcComplexPropertyTemplate(DatabaseIfc db, IfcComplexPropertyTemplate t, DuplicateOptions options) : base(db, t, options)
		{
			mUsageName = t.mUsageName;
			mTemplateType = t.mTemplateType;
			if(options.DuplicateDownstream)
				t.HasPropertyTemplates.Values.ToList().ForEach(x => AddPropertyTemplate(db.Factory.Duplicate(x) as IfcPropertyTemplate));
		}
		public IfcComplexPropertyTemplate(DatabaseIfc db, string name) : base(db, name) { }

		public void AddPropertyTemplate(IfcPropertyTemplate template) {  mHasPropertyTemplates.Add(template.Name, template); template.mPartOfComplexTemplate.Add(this); }
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
		public void Remove(string templateName) { IfcPropertyTemplate template = this[templateName]; if (template != null) { template.mPartOfComplexTemplate.Remove(this); mHasPropertyTemplates.Remove(templateName); } }
	}
	public partial class IfcCompositeCurve : IfcBoundedCurve
	{
		private LIST<IfcSegment> mSegments = new LIST<IfcSegment>();// : LIST [1:?] OF IfcCompositeCurveSegment;
		private IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// : LOGICAL;

		public LIST<IfcSegment> Segments { get { return mSegments; } set { mSegments = value; } }
		public IfcLogicalEnum SelfIntersect { get { return mSelfIntersect; } set { mSelfIntersect = value; } }

		internal IfcCompositeCurve() : base() { }
		internal IfcCompositeCurve(DatabaseIfc db, IfcCompositeCurve c, DuplicateOptions options) : base(db, c, options)
		{
			Segments.AddRange(c.Segments.Select(x => db.Factory.Duplicate(x, options)));
			mSelfIntersect = c.mSelfIntersect;
			IfcLinearElement linearElement = representationOf<IfcLinearElement>();
			if (linearElement != null)
				db.Factory.Duplicate(linearElement, new DuplicateOptions(options) { DuplicateHost = true });
			else
			{
				IfcPositioningElement positioningElement = c.representationOf<IfcPositioningElement>();
				if (positioningElement != null)
					db.Factory.Duplicate(positioningElement, new DuplicateOptions(options) { DuplicateHost = true });
			}
		}
		public IfcCompositeCurve(IEnumerable<IfcSegment> segments) : base(segments.First().mDatabase) { mSegments.AddRange(segments); }
		public IfcCompositeCurve(params IfcSegment[] segments) : base(segments[0].mDatabase) { mSegments.AddRange(segments); }
	}
	[Serializable]
	public partial class IfcCompositeCurveOnSurface : IfcCompositeCurve, IfcCurveOnSurface
	{
		internal IfcSurface mBasisSurface;// : IfcSurface;
		public IfcSurface BasisSurface { get { return mBasisSurface; } set { mBasisSurface = value; } }

		internal IfcCompositeCurveOnSurface() : base() { }
		internal IfcCompositeCurveOnSurface(DatabaseIfc db, IfcCompositeCurveOnSurface c, DuplicateOptions options) : base(db, c, options) { BasisSurface = db.Factory.Duplicate(c.BasisSurface) as IfcSurface; }
		public IfcCompositeCurveOnSurface(List<IfcCompositeCurveSegment> segs,IfcSurface surface) : base(segs) { BasisSurface = surface; }
	}
	[Serializable]
	public partial class IfcCompositeCurveSegment : IfcSegment 
	{
		private bool mSameSense;// : BOOLEAN;
		internal IfcCurve mParentCurve;// : IfcCurve;  Really IfcBoundedCurve WR1

		public bool SameSense { get { return mSameSense; } set { mSameSense = value; } }
		public IfcBoundedCurve ParentCurve { get { return mParentCurve as IfcBoundedCurve; } set { mParentCurve = value; } }

		internal IfcCompositeCurveSegment() : base() { }
		internal IfcCompositeCurveSegment(DatabaseIfc db, IfcCompositeCurveSegment s, DuplicateOptions options) : base(db, s, options) { mSameSense = s.mSameSense; mParentCurve = db.Factory.Duplicate(s.mParentCurve, options); }
		public IfcCompositeCurveSegment(IfcTransitionCode tc, bool sense, IfcBoundedCurve bc) : base(bc.mDatabase, tc) { mSameSense = sense;  ParentCurve = bc; }
	}
	[Serializable]
	public partial class IfcCompositeProfileDef : IfcProfileDef
	{
		private SET<IfcProfileDef> mProfiles = new SET<IfcProfileDef>();// : SET [2:?] OF IfcProfileDef;
		private string mLabel = "";// : OPTIONAL IfcLabel;

		public SET<IfcProfileDef> Profiles { get { return mProfiles; } }
		public string Label { get { return mLabel; } set { mLabel = value; } }

		internal IfcCompositeProfileDef() : base() { }
		internal IfcCompositeProfileDef(DatabaseIfc db, IfcCompositeProfileDef p, DuplicateOptions options) : base(db, p, options)
		{
			p.Profiles.ToList().ForEach(x => addProfile(db.Factory.Duplicate(x) as IfcProfileDef));
			mLabel = p.mLabel;
		}
		private IfcCompositeProfileDef(DatabaseIfc db, string name) : base(db, name)
		{
			if (db != null && db.mModelView == ModelView.Ifc4Reference)
				throw new Exception("Invalid Model View for IfcCompositeProfileDef : " + db.ModelView.ToString());
		}
		public IfcCompositeProfileDef(string name, IEnumerable<IfcProfileDef> profiles) : this(profiles.First().Database, name) { mProfiles.AddRange(profiles); }
		public IfcCompositeProfileDef(string name, IfcProfileDef p1, IfcProfileDef p2) : this(p1.mDatabase, name) { mProfiles.Add(p1); mProfiles.Add(p2); }

		internal void addProfile(IfcProfileDef profile) { mProfiles.Add(profile); }
	}
	[Serializable]
	public partial class IfcCompressor : IfcFlowMovingDevice //IFC4
	{
		private IfcCompressorTypeEnum mPredefinedType = IfcCompressorTypeEnum.NOTDEFINED;// OPTIONAL : IfcCompressorTypeEnum;
		public IfcCompressorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCompressorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		internal IfcCompressor() : base() { }
		internal IfcCompressor(DatabaseIfc db, IfcCompressor c, DuplicateOptions options) : base(db, c, options) { PredefinedType = c.PredefinedType; }
		public IfcCompressor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCompressorType : IfcFlowMovingDeviceType
	{
		private IfcCompressorTypeEnum mPredefinedType = IfcCompressorTypeEnum.NOTDEFINED;// : IfcCompressorTypeEnum;
		public IfcCompressorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCompressorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		internal IfcCompressorType() : base() { }
		internal IfcCompressorType(DatabaseIfc db, IfcCompressorType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcCompressorType(DatabaseIfc db, string name, IfcCompressorTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcCommunicationsAppliance : IfcFlowTerminal //IFC4
	{
		private IfcCommunicationsApplianceTypeEnum mPredefinedType = IfcCommunicationsApplianceTypeEnum.NOTDEFINED;// OPTIONAL : IfcCommunicationsApplianceTypeEnum;
		public IfcCommunicationsApplianceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCommunicationsApplianceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCommunicationsAppliance() : base() { }
		internal IfcCommunicationsAppliance(DatabaseIfc db, IfcCommunicationsAppliance a, DuplicateOptions options) : base(db,a, options) { PredefinedType = a.PredefinedType; }
		public IfcCommunicationsAppliance(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCommunicationsApplianceType : IfcFlowTerminalType
	{
		private IfcCommunicationsApplianceTypeEnum mPredefinedType = IfcCommunicationsApplianceTypeEnum.NOTDEFINED;// : IfcCommunicationsApplianceBoxTypeEnum; 
		public IfcCommunicationsApplianceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCommunicationsApplianceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		internal IfcCommunicationsApplianceType() : base() { }
		internal IfcCommunicationsApplianceType(DatabaseIfc db, IfcCommunicationsApplianceType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcCommunicationsApplianceType(DatabaseIfc db, string name, IfcCommunicationsApplianceTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcCondenser : IfcEnergyConversionDevice //IFC4
	{
		private IfcCondenserTypeEnum mPredefinedType = IfcCondenserTypeEnum.NOTDEFINED;// OPTIONAL : IfcCCondenserTypeEnum;
		public IfcCondenserTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCondenserTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCondenser() : base() { }
		internal IfcCondenser(DatabaseIfc db, IfcCondenser c, DuplicateOptions options) : base(db, c, options) { PredefinedType = c.PredefinedType; }
		public IfcCondenser(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCondenserType : IfcEnergyConversionDeviceType
	{
		private IfcCondenserTypeEnum mPredefinedType = IfcCondenserTypeEnum.NOTDEFINED;// : IfcCondenserTypeEnum;
		public IfcCondenserTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCondenserTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		internal IfcCondenserType() : base() { }
		internal IfcCondenserType(DatabaseIfc db, IfcCondenserType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcCondenserType(DatabaseIfc db, string name, IfcCondenserTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcCondition : IfcGroup //DEPRECATED IFC4
	{
		internal IfcCondition() : base() { }
		internal IfcCondition(DatabaseIfc db, IfcCondition c, DuplicateOptions options) : base(db, c, options) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcConditionCriterion : IfcControl //DEPRECATED IFC4
	{
		internal IfcConditionCriterionSelect mCriterion;// : IfcConditionCriterionSelect;
		internal IfcDateTimeSelect mCriterionDateTime;// : IfcDateTimeSelect; 
		internal IfcConditionCriterion() : base() { }
		internal IfcConditionCriterion(DatabaseIfc db, IfcConditionCriterion c, DuplicateOptions options) : base(db, c, options) { } // mCriterion = c.mCriterion; mCriterionDateTime = c.mCriterionDateTime; }
	}
	public interface IfcConditionCriterionSelect : IBaseClassIfc { } // SELECT(IfcLabel, IfcMeasureWithUnit);
	[Serializable]
	public abstract partial class IfcConic : IfcCurve /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCircle ,IfcEllipse))*/
	{
		private IfcAxis2Placement mPosition;// : IfcAxis2Placement;
		public IfcAxis2Placement Position { get { return mPosition; } set { mPosition = value; } }

		protected IfcConic() : base() { }
		protected IfcConic(IfcAxis2Placement ap) : base(ap.Database) { mPosition = ap; }
		protected IfcConic(DatabaseIfc db, IfcConic c, DuplicateOptions options) : base(db, c, options) { Position = db.Factory.Duplicate(c.mPosition, options); }
	}
	[Serializable]
	public partial class IfcConnectedFaceSet : IfcTopologicalRepresentationItem //SUPERTYPE OF (ONEOF (IfcClosedShell ,IfcOpenShell))
	{
		internal SET<IfcFace> mCfsFaces = new SET<IfcFace>();// : SET [1:?] OF IfcFace;
		public SET<IfcFace> CfsFaces { get { return mCfsFaces; } }

		internal IfcConnectedFaceSet() : base() { }
		internal IfcConnectedFaceSet(DatabaseIfc db, IfcConnectedFaceSet c, DuplicateOptions options) : base(db, c, options) 
		{ 
			CfsFaces.AddRange(c.CfsFaces.ConvertAll(x=>db.Factory.Duplicate(x, options))); 
		}
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
		private IfcCurveOrEdgeCurve mCurveOnRelatingElement;// : IfcCurveOrEdgeCurve;
		private IfcCurveOrEdgeCurve mCurveOnRelatedElement;// : OPTIONAL IfcCurveOrEdgeCurve; 

		public IfcCurveOrEdgeCurve CurveOnRelatingElement { get { return mCurveOnRelatingElement; } set { mCurveOnRelatingElement = value; } }
		public IfcCurveOrEdgeCurve CurveOnRelatedElement { get { return mCurveOnRelatedElement; } set { mCurveOnRelatedElement = value; } }

		internal IfcConnectionCurveGeometry() : base() { }
		internal IfcConnectionCurveGeometry(DatabaseIfc db, IfcConnectionCurveGeometry g) : base(db, g) { CurveOnRelatingElement = db.Factory.Duplicate(g.mCurveOnRelatingElement) as IfcCurveOrEdgeCurve; CurveOnRelatedElement = db.Factory.Duplicate(g.mCurveOnRelatedElement) as IfcCurveOrEdgeCurve; }
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
		private IfcPointOrVertexPoint mPointOnRelatingElement;// : IfcPointOrVertexPoint;
		private IfcPointOrVertexPoint mPointOnRelatedElement;// : OPTIONAL IfcPointOrVertexPoint;

		public IfcPointOrVertexPoint PointOnRelatingElement { get { return mPointOnRelatingElement; } set { mPointOnRelatingElement = value; } }
		public IfcPointOrVertexPoint PointOnRelatedElement { get { return mPointOnRelatedElement; } set { mPointOnRelatedElement = value; } }

		internal IfcConnectionPointGeometry() : base() { }
		internal IfcConnectionPointGeometry(DatabaseIfc db, IfcConnectionPointGeometry g) : base(db,g) { PointOnRelatingElement = db.Factory.Duplicate(g.mPointOnRelatingElement) as IfcPointOrVertexPoint;  PointOnRelatedElement = db.Factory.Duplicate(g.mPointOnRelatedElement) as IfcPointOrVertexPoint; }
		public IfcConnectionPointGeometry(IfcPointOrVertexPoint v) : base(v.Database) { mPointOnRelatingElement = v; }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcConnectionPortGeometry  // DEPRECATED IFC4
	[Serializable]
	public partial class IfcConnectionSurfaceGeometry : IfcConnectionGeometry
	{
		internal IfcSurfaceOrFaceSurface mSurfaceOnRelatingElement;// : IfcSurfaceOrFaceSurface;
		internal IfcSurfaceOrFaceSurface mSurfaceOnRelatedElement;// : OPTIONAL IfcSurfaceOrFaceSurface;

		public IfcSurfaceOrFaceSurface SurfaceOnRelatingElement { get { return mSurfaceOnRelatingElement; } set { mSurfaceOnRelatingElement = value; } }
		public IfcSurfaceOrFaceSurface SurfaceOnRelatedElement { get { return mSurfaceOnRelatedElement; } set { mSurfaceOnRelatedElement = value; } }

		internal IfcConnectionSurfaceGeometry() : base() { }
		internal IfcConnectionSurfaceGeometry(DatabaseIfc db, IfcConnectionSurfaceGeometry g) : base(db,g) { SurfaceOnRelatingElement = db.Factory.Duplicate(g.mSurfaceOnRelatingElement) as IfcSurfaceOrFaceSurface; SurfaceOnRelatedElement = db.Factory.Duplicate(g.mSurfaceOnRelatedElement) as IfcSurfaceOrFaceSurface; }
	}
	[Serializable]
	public partial class IfcConnectionVolumeGeometry : IfcConnectionGeometry
	{
		private IfcSolidOrShell mVolumeOnRelatingElement = null; //: IfcSolidOrShell;
		private IfcSolidOrShell mVolumeOnRelatedElement = null; //: OPTIONAL IfcSolidOrShell;

		public IfcSolidOrShell VolumeOnRelatingElement { get { return mVolumeOnRelatingElement; } set { mVolumeOnRelatingElement = value; } }
		public IfcSolidOrShell VolumeOnRelatedElement { get { return mVolumeOnRelatedElement; } set { mVolumeOnRelatedElement = value; } }

		public IfcConnectionVolumeGeometry() : base() { }
		public IfcConnectionVolumeGeometry(DatabaseIfc db, IfcSolidOrShell volumeOnRelatingElement)
			: base(db) { VolumeOnRelatingElement = volumeOnRelatingElement; }
	}
	[Serializable]
	public abstract partial class IfcConstraint : BaseClassIfc, IfcResourceObjectSelect, NamedObjectIfc //IFC4Change ABSTRACT SUPERTYPE OF(ONEOF(IfcMetric, IfcObjective));
	{
		internal string mName = "NOTDEFINED";// :  IfcLabel;
		internal string mDescription = "";// : OPTIONAL IfcText;
		internal IfcConstraintEnum mConstraintGrade;// : IfcConstraintEnum
		internal string mConstraintSource = "";// : OPTIONAL IfcLabel;
		internal IfcActorSelect mCreatingActor;// : OPTIONAL IfcActorSelect;
		internal DateTime mCreationTime = DateTime.MinValue; // : OPTIONAL IfcDateTimeSelect; IFC4 IfcDateTime 
		internal IfcDateTimeSelect mSSCreationTime;// : OPTIONAL IfcDateTimeSelect; IFC4 IfcDateTime 
		internal string mUserDefinedGrade = "";// : OPTIONAL IfcLabel

		public string Name { get { return mName; } set { mName = (string.IsNullOrEmpty(value) ? "NOTDEFINED" : value); } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public IfcConstraintEnum ConstraintGrade { get { return mConstraintGrade; } set { mConstraintGrade = value; } }
		public string ConstraintSource { get { return mConstraintSource; } set { mConstraintSource = value; } }
		public IfcActorSelect CreatingActor { get { return mCreatingActor; } set { mCreatingActor = value; } }
		//creationtime
		public string UserDefinedGrade { get { return mUserDefinedGrade; } set { mUserDefinedGrade = value; } }

		//	INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mPropertiesForConstraint = new List<IfcResourceConstraintRelationship>();//	 :	SET OF IfcResourceConstraintRelationship FOR RelatingConstraint;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //ggc
		internal List<IfcRelAssociatesConstraint> mConstraintForObjects = new List<IfcRelAssociatesConstraint>();// gg	 :	SET [0:?] OF IfcRelAssociatesConstraint FOR RelatedResourceObjects;
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		protected IfcConstraint() : base() { }
		protected IfcConstraint(DatabaseIfc db, IfcConstraint c) : base(db,c)
		{
			mName = c.mName;
			mDescription = c.mDescription;
			mConstraintGrade = c.mConstraintGrade;
			mConstraintSource = c.mConstraintSource;
			if(mCreatingActor != null)
				CreatingActor = db.Factory.Duplicate(c.mCreatingActor) as IfcActorSelect;
			mCreationTime = c.mCreationTime;
			if (mSSCreationTime != null)
				mSSCreationTime = db.Factory.Duplicate(c.mSSCreationTime) as IfcDateTimeSelect;
			mUserDefinedGrade = c.mUserDefinedGrade;
		}
		protected IfcConstraint(DatabaseIfc db, string name, IfcConstraintEnum constraint) : base(db) { Name = name; mConstraintGrade = constraint; }
		protected override void initialize()
		{
			base.initialize();
			mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged;
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
		protected override bool DisposeWorker(bool children)
		{
			foreach (IfcRelAssociatesConstraint rac in mConstraintForObjects)
				mDatabase[rac.mStepId] = null;
			List<IfcResourceConstraintRelationship> rcrs = mPropertiesForConstraint;
			for (int icounter = 0; icounter < rcrs.Count; icounter++)
				rcrs[icounter].Dispose(true);
			return base.DisposeWorker(children);
		}

		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { } // mHasConstraintRelationships.Add(constraintRelationship); }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcConstraintAggregationRelationship; // DEPRECATED IFC4
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcConstraintclassificationRelationship; // DEPRECATED IFC4
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcConstraintRelationship; // DEPRECATED IFC4
	[Serializable]
	public partial class IfcConstructionEquipmentResource : IfcConstructionResource
	{
		private IfcConstructionEquipmentResourceTypeEnum mPredefinedType = IfcConstructionEquipmentResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcConstructionEquipmentResourceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcConstructionEquipmentResourceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcConstructionEquipmentResource() : base() { }
		internal IfcConstructionEquipmentResource(DatabaseIfc db, IfcConstructionEquipmentResource r, DuplicateOptions options) : base(db,r, options) { PredefinedType = r.PredefinedType; }
		public IfcConstructionEquipmentResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcConstructionEquipmentResourceType : IfcConstructionResourceType //IFC4
	{
		private IfcConstructionEquipmentResourceTypeEnum mPredefinedType = IfcConstructionEquipmentResourceTypeEnum.NOTDEFINED;
		public IfcConstructionEquipmentResourceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcConstructionEquipmentResourceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcConstructionEquipmentResourceType() : base() { }
		internal IfcConstructionEquipmentResourceType(DatabaseIfc db, IfcConstructionEquipmentResourceType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcConstructionEquipmentResourceType(DatabaseIfc db, string name, IfcConstructionEquipmentResourceTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcConstructionMaterialResource : IfcConstructionResource
	{
		private IfcConstructionMaterialResourceTypeEnum mPredefinedType = IfcConstructionMaterialResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcConstructionMaterialResourceTypeEnum; 
		[Obsolete("DEPRECATED IFC4", false)]
		internal SET<IfcActorSelect> mSuppliers = new SET<IfcActorSelect>(); //: 	OPTIONAL SET[1:?] OF IfcActorSelect; ifc2x3
		[Obsolete("DEPRECATED IFC4", false)]
		internal IfcRatioMeasure mUsageRatio = null; //: 	OPTIONAL IfcRatioMeasure; ifc2x3

		public IfcConstructionMaterialResourceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcConstructionMaterialResourceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		[Obsolete("DEPRECATED IFC4", false)]
		public SET<IfcActorSelect> Suppliers { get { return mSuppliers; } } //: 	OPTIONAL SET[1:?] OF IfcActorSelect; ifc2x3
		[Obsolete("DEPRECATED IFC4", false)]
		public IfcRatioMeasure UsageRatio { get { return mUsageRatio; } set { mUsageRatio = value; } } //: 	OPTIONAL IfcRatioMeasure; ifc2x3
		internal IfcConstructionMaterialResource() : base() { }
		internal IfcConstructionMaterialResource(DatabaseIfc db, IfcConstructionMaterialResource r, DuplicateOptions options) : base(db,r, options) { PredefinedType = r.PredefinedType; }
		public IfcConstructionMaterialResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcConstructionMaterialResourceType : IfcConstructionResourceType //IFC4
	{
		private IfcConstructionMaterialResourceTypeEnum mPredefinedType = IfcConstructionMaterialResourceTypeEnum.NOTDEFINED;
		public IfcConstructionMaterialResourceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcConstructionMaterialResourceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcConstructionMaterialResourceType() : base() { }
		internal IfcConstructionMaterialResourceType(DatabaseIfc db, IfcConstructionMaterialResourceType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcConstructionMaterialResourceType(DatabaseIfc db, string name, IfcConstructionMaterialResourceTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcConstructionProductResource : IfcConstructionResource
	{
		private IfcConstructionProductResourceTypeEnum mPredefinedType = IfcConstructionProductResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcConstructionProductResourceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcConstructionProductResourceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcConstructionProductResource() : base() { }
		internal IfcConstructionProductResource(DatabaseIfc db, IfcConstructionProductResource r, DuplicateOptions options) : base(db,r, options) { PredefinedType = r.PredefinedType; }
		public IfcConstructionProductResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcConstructionProductResourceType : IfcConstructionResourceType //IFC4
	{
		private IfcConstructionProductResourceTypeEnum mPredefinedType = IfcConstructionProductResourceTypeEnum.NOTDEFINED;
		public IfcConstructionProductResourceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcConstructionProductResourceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcConstructionProductResourceType() : base() { }
		internal IfcConstructionProductResourceType(DatabaseIfc db, IfcConstructionProductResourceType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcConstructionProductResourceType(DatabaseIfc db, string name, IfcConstructionProductResourceTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcConstructionResource : IfcResource //ABSTRACT SUPERTYPE OF (ONEOF(IfcConstructionEquipmentResource, IfcConstructionMaterialResource, IfcConstructionProductResource, IfcCrewResource, IfcLaborResource, IfcSubContractResource))
	{
		internal IfcResourceTime mUsage; //:	OPTIONAL IfcResourceTime; IFC4
		internal LIST<IfcAppliedValue> mBaseCosts = new LIST<IfcAppliedValue>();//	 :	OPTIONAL LIST [1:?] OF IfcAppliedValue; IFC4
		internal IfcPhysicalQuantity mBaseQuantity = null;//	 :	OPTIONAL IfcPhysicalQuantity; IFC4 

		internal string mResourceGroup = "";// : 	OPTIONAL IfcLabel;
		internal IfcResourceConsumptionEnum mResourceConsumption = IfcResourceConsumptionEnum.NOTDEFINED;//	 : 	OPTIONAL IfcResourceConsumptionEnum;
		internal IfcMeasureWithUnit mBaseQuantitySS = null;//	 : 	OPTIONAL IfcMeasureWithUnit;

		public IfcResourceTime Usage { get { return mUsage; } set { mUsage = value; } }
		public LIST<IfcAppliedValue> BaseCosts { get { return mBaseCosts; } }
		public IfcPhysicalQuantity BaseQuantity { get { return mBaseQuantity; } set { mBaseQuantity = value; } }
		protected IfcConstructionResource() : base() { }
		protected IfcConstructionResource(DatabaseIfc db, IfcConstructionResource r, DuplicateOptions options) : base(db,r, options)
		{
			if(r.mUsage != null)
				Usage = db.Factory.Duplicate(r.Usage);
			if(r.mBaseCosts.Count > 0)
				BaseCosts.AddRange(r.BaseCosts.Select(x=>db.Factory.Duplicate(x)));
			if(r.mBaseQuantity != null)
				BaseQuantity = db.Factory.Duplicate(r.BaseQuantity);
		}
		protected IfcConstructionResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public abstract partial class IfcConstructionResourceType : IfcTypeResource //IFC4
	{
		internal LIST<IfcAppliedValue> mBaseCosts = new LIST<IfcAppliedValue>();//	 :	OPTIONAL LIST [1:?] OF IfcAppliedValue; 
		internal IfcPhysicalQuantity mBaseQuantity;//	 :	OPTIONAL IfcPhysicalQuantity; 

		public LIST<IfcAppliedValue> BaseCosts { get { return mBaseCosts; } }
		public IfcPhysicalQuantity BaseQuantity { get { return mBaseQuantity; } set { mBaseQuantity = value; } }

		protected IfcConstructionResourceType() : base() { }
		protected IfcConstructionResourceType(DatabaseIfc db) : base(db) { }
		protected IfcConstructionResourceType(DatabaseIfc db, IfcConstructionResourceType t, DuplicateOptions options) : base(db, t, options)
		{
			BaseCosts.AddRange(t.BaseCosts.Select(x => db.Factory.Duplicate(x)));
			BaseQuantity = db.Factory.Duplicate(t.BaseQuantity) as IfcPhysicalQuantity;
		}
	}
	[Serializable]
	public abstract partial class IfcContext : IfcObjectDefinition//(IfcProject, IfcProjectLibrary)
	{
		internal string mObjectType = "";//	 :	OPTIONAL IfcLabel;
		private string mLongName = "";// : OPTIONAL IfcLabel;
		private string mPhase = "";// : OPTIONAL IfcLabel;
		internal SET<IfcRepresentationContext> mRepresentationContexts = new SET<IfcRepresentationContext>();// : 	OPTIONAL SET [1:?] OF IfcRepresentationContext;
		private IfcUnitAssignment mUnitsInContext;// : OPTIONAL IfcUnitAssignment; IFC2x3 not Optional
		//INVERSE
		internal SET<IfcRelDeclares> mDeclares = new SET<IfcRelDeclares>();

		public string ObjectType { get { return mObjectType; } set { mObjectType = value; } }
		public string LongName { get { return mLongName; } set { mLongName = value; } }
		public string Phase { get { return mPhase; } set { mPhase = value; } }
		public SET<IfcRepresentationContext> RepresentationContexts { get { return mRepresentationContexts; } }
		public IfcUnitAssignment UnitsInContext
		{
			get { return mUnitsInContext; }
			set
			{
				mUnitsInContext = value;
				if(value != null)
				{
					IfcNamedUnit namedUnit = value[IfcUnitEnum.PLANEANGLEUNIT];
					if(namedUnit != null)
					{
						double scale = namedUnit.SIFactor;
						mDatabase.Factory.Options.AngleUnitsInRadians = Math.Abs(1 - scale) < 1e-4;
					}
				}
			}
		}

		public SET<IfcRelDefinesByProperties> IsDefinedBy { get { return mIsDefinedBy; } }
		public SET<IfcRelDeclares> Declares { get { return mDeclares; } }
	
		protected IfcContext() : base() { }
		protected IfcContext(DatabaseIfc db, IfcContext c, DuplicateOptions options) : base(db, c, options)
		{
			if(db.mContext == null)
				db.mContext = this;
			mObjectType = c.mObjectType;
			mLongName = c.mLongName;
			mPhase = c.mPhase;
			RepresentationContexts.AddRange(c.RepresentationContexts.ConvertAll(x => db.Factory.Duplicate(x, options) as IfcRepresentationContext));
			if (c.mUnitsInContext != null)
				UnitsInContext = db.Factory.Duplicate(c.UnitsInContext, options);
			if(options.DuplicateDownstream)
			{
				foreach(IfcRelDeclares declares in c.Declares)
					db.Factory.Duplicate(declares, options);

				foreach(IfcGroup group in c.Database.OfType<IfcGroup>())
				{
					db.Factory.Duplicate(group, options);
				}
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
				foreach(IfcGeometricRepresentationContext grc in mRepresentationContexts.OfType<IfcGeometricRepresentationContext>())
				{
					if(!double.IsNaN(grc.mPrecision))
						mDatabase.Tolerance = grc.mPrecision;
					break;
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
			IfcRelDeclares declares = mDeclares.First();
			declares.RelatedDefinitions.Add(d);
			return declares;
		}

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);

			Type[] interfaces = typeof(T).GetInterfaces();
			if(interfaces.Contains(typeof(IfcDefinitionSelect)))
			{
				foreach(IfcRelDeclares rd in Declares)
				{
					SET<IfcDefinitionSelect> ds = rd.RelatedDefinitions;
					foreach(IfcDefinitionSelect d in ds)
					{
						result.AddRange(d.Extract<T>());
						if (d is T && !result.Contains((T)d))
							result.Add((T)d);
					}
				}
				
			}
			foreach (IfcRelAssociates rac in HasAssociations)
			{
				NamedObjectIfc relating = rac.Relating();
				if (relating is T)
					result.Add((T)relating);
			}
			foreach (IfcRelDefinesByProperties rdp in mIsDefinedBy)
				result.AddRange(rdp.Extract<T>());
			return result;
		}
		public List<IfcTypeObject> DeclaredTypes()
		{
			if(mDeclares.Count > 0)
				return mDeclares.SelectMany(x=>x.RelatedDefinitions).OfType<IfcTypeObject>().ToList();
			return mDatabase.OfType<IfcTypeObject>().ToList();
		}
		public override IfcPropertySetDefinition FindPropertySet(string name)
		{
			foreach (IfcPropertySetDefinition pset in mIsDefinedBy.SelectMany(x => x.RelatingPropertyDefinition))
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
	}
	[Serializable]
	public partial class IfcContextDependentUnit : IfcNamedUnit, IfcResourceObjectSelect
	{
		private string mName = ""; //: IfcLabel;
		//INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>();
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public string Name { get { return mName; } set { mName = value; } }
		//INVERSE
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } }

		public IfcContextDependentUnit() : base() { }
		public IfcContextDependentUnit(IfcDimensionalExponents dimensions, IfcUnitEnum unitType, string name)
			: base(dimensions, unitType) { Name = name; }

		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
		public override double SIFactor { get { return 1; } }
	}
	[Serializable]
	public abstract partial class IfcControl : IfcObject //ABSTRACT SUPERTYPE OF (ONEOF (IfcActionRequest ,IfcConditionCriterion ,IfcCostItem ,IfcCostSchedule,IfcEquipmentStandard ,IfcFurnitureStandard
	{ //  ,IfcPerformanceHistory ,IfcPermit ,IfcProjectOrder ,IfcProjectOrderRecord ,IfcScheduleTimeControl ,IfcServiceLife ,IfcSpaceProgram ,IfcTimeSeriesSchedule,IfcWorkControl))
		internal string mIdentification = ""; // : OPTIONAL IfcIdentifier; IFC4
		//INVERSE
		internal SET<IfcRelAssignsToControl> mControls = new SET<IfcRelAssignsToControl>();/// : SET OF IfcRelAssignsToControl FOR RelatingControl;

		public string Identification { get { return mIdentification; } set { mIdentification = value; } }
		public SET<IfcRelAssignsToControl> Controls { get { return mControls; } }
		protected IfcControl() : base() { }
		protected IfcControl(DatabaseIfc db, IfcControl c, DuplicateOptions options) : base(db, c, options)
		{ 
			mIdentification = c.mIdentification;
			if (options.DuplicateDownstream)
			{
				foreach (IfcRelAssignsToControl controls in mControls)
					db.Factory.Duplicate(controls, options);
			}
		}
		protected IfcControl(DatabaseIfc db) : base(db)
		{
			if (mDatabase.mModelView == ModelView.Ifc4Reference)
				throw new Exception("Invalid Model View for IfcControl : " + db.ModelView.ToString());
		}

		public void Assign(IfcObjectDefinition o) 
		{ 
			if (mControls.Count == 0) 
				new IfcRelAssignsToControl(this, o); 
			else 
				mControls.First().RelatedObjects.Add(o); 
		}
	}
	[Serializable]
	public partial class IfcController : IfcDistributionControlElement //IFC4  
	{
		private IfcControllerTypeEnum mPredefinedType = IfcControllerTypeEnum.NOTDEFINED;
		public IfcControllerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcControllerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcController() : base() { }
		internal IfcController(DatabaseIfc db, IfcController c, DuplicateOptions options) : base(db, c, options) { PredefinedType = c.PredefinedType; }
		public IfcController(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcControllerType : IfcDistributionControlElementType
	{
		private IfcControllerTypeEnum mPredefinedType = IfcControllerTypeEnum.NOTDEFINED;// : IfcControllerTypeEnum;
		public IfcControllerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcControllerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcControllerType() : base() { }
		internal IfcControllerType(DatabaseIfc db, IfcControllerType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcControllerType(DatabaseIfc db, string name, IfcControllerTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcConversionBasedUnit : IfcNamedUnit, IfcResourceObjectSelect, NamedObjectIfc //	SUPERTYPE OF(IfcConversionBasedUnitWithOffset)
	{
		public enum CommonUnitName
		{
			inch, foot, yard, mile, square_inch, square_foot, square_yard, acre, square_mile, cubic_inch, cubic_foot, cubic_yard, litre, fluid_ounce_UK,
			fluid_ounce_US, pint_UK, pint_US, gallon_UK, gallon_US, degree, ounce, pound, ton_UK, ton_US, lbf, kip, psi, ksi, minute, hour, day, btu
		};
		
		private string mName = "";// : IfcLabel;
		private IfcMeasureWithUnit mConversionFactor;// : IfcMeasureWithUnit; 
		//INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal SET<IfcResourceConstraintRelationship> mHasConstraintRelationships = new SET<IfcResourceConstraintRelationship>(); //gg
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } }
		public SET<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		public string Name { get { return ParserSTEP.Decode(mName); } set { mName = ParserSTEP.Encode(value); } }
		public IfcMeasureWithUnit ConversionFactor { get { return mConversionFactor; } set { mConversionFactor = value; } }
		
		internal IfcConversionBasedUnit() : base() { }
		internal IfcConversionBasedUnit(DatabaseIfc db, IfcConversionBasedUnit u, DuplicateOptions options) : base(db, u, options) 
		{
			mName = u.mName; 
			ConversionFactor = db.Factory.Duplicate(u.ConversionFactor, options); 
		}
		public IfcConversionBasedUnit(IfcUnitEnum unit, string name, IfcMeasureWithUnit mu)
			: base(mu.mDatabase, unit, true) { Name = name.Replace("'", ""); mConversionFactor = mu; }
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged;
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
		public override double SIFactor { get { return ConversionFactor.SIFactor(); } }

		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	[Serializable]
	public partial class IfcConversionBasedUnitWithOffset : IfcConversionBasedUnit //IFC4
	{
		internal double mConversionOffset = 0;//	 :	IfcReal
		public double ConversionOffset { get { return mConversionOffset; } set { mConversionOffset = value; } }
		
		internal IfcConversionBasedUnitWithOffset() : base() { }
		internal IfcConversionBasedUnitWithOffset(DatabaseIfc db, IfcConversionBasedUnitWithOffset u, DuplicateOptions options) : base(db, u, options) { mConversionOffset = u.mConversionOffset; }
		public IfcConversionBasedUnitWithOffset(IfcUnitEnum unit, string name, IfcMeasureWithUnit mu, double offset)
			: base(unit, name, mu) { mConversionOffset = offset; }
	}
	[Serializable]
	public partial class IfcConveyorSegment : IfcFlowSegment
	{
		private IfcConveyorSegmentTypeEnum mPredefinedType = IfcConveyorSegmentTypeEnum.NOTDEFINED; //: OPTIONAL IfcConveyorSegmentTypeEnum;
		public IfcConveyorSegmentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcConveyorSegmentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcConveyorSegment() : base() { }
		public IfcConveyorSegment(DatabaseIfc db) : base(db) { }
		public IfcConveyorSegment(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcConveyorSegmentType : IfcFlowSegmentType
	{
		private IfcConveyorSegmentTypeEnum mPredefinedType = IfcConveyorSegmentTypeEnum.NOTDEFINED; //: IfcConveyorSegmentTypeEnum;
		public IfcConveyorSegmentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcConveyorSegmentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcConveyorSegmentType() : base() { }
		public IfcConveyorSegmentType(DatabaseIfc db, string name, IfcConveyorSegmentTypeEnum predefinedType)
			: base(db) { Name = name; PredefinedType = predefinedType; }
	}
	[Serializable]
	public partial class IfcCooledBeam : IfcEnergyConversionDevice //IFC4
	{
		private IfcCooledBeamTypeEnum mPredefinedType = IfcCooledBeamTypeEnum.NOTDEFINED;// OPTIONAL : IfcCooledBeamTypeEnum;
		public IfcCooledBeamTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCooledBeamTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCooledBeam() : base() { }
		internal IfcCooledBeam(DatabaseIfc db, IfcCooledBeam b, DuplicateOptions options) : base(db, b, options) { PredefinedType = b.PredefinedType; }
		public IfcCooledBeam(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCooledBeamType : IfcEnergyConversionDeviceType
	{
		private IfcCooledBeamTypeEnum mPredefinedType = IfcCooledBeamTypeEnum.NOTDEFINED;// : IfcCooledBeamTypeEnum
		public IfcCooledBeamTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCooledBeamTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCooledBeamType() : base() { }
		internal IfcCooledBeamType(DatabaseIfc db, IfcCooledBeamType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcCooledBeamType(DatabaseIfc db, string name, IfcCooledBeamTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcCoolingTower : IfcEnergyConversionDevice //IFC4
	{
		private IfcCoolingTowerTypeEnum mPredefinedType = IfcCoolingTowerTypeEnum.NOTDEFINED;// OPTIONAL : IfcCoolingTowerTypeEnum;
		public IfcCoolingTowerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCoolingTowerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCoolingTower() : base() { }
		private IfcCoolingTower(DatabaseIfc db, IfcCoolingTower t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcCoolingTower(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcCoolingTowerType : IfcEnergyConversionDeviceType
	{
		private IfcCoolingTowerTypeEnum mPredefinedType = IfcCoolingTowerTypeEnum.NOTDEFINED;// : IfcCoolingTowerTypeEnum
		public IfcCoolingTowerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCoolingTowerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCoolingTowerType() : base() { }
		internal IfcCoolingTowerType(DatabaseIfc db, IfcCoolingTowerType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcCoolingTowerType(DatabaseIfc db, string name, IfcCoolingTowerTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcCoordinatedUniversalTimeOffset : BaseClassIfc //DEPRECATED IFC4
	{
		internal int mHourOffset;// : IfcHourInDay;
		internal int mMinuteOffset;// : OPTIONAL IfcMinuteInHour;
		internal IfcAheadOrBehind mSense = IfcAheadOrBehind.AHEAD;// : IfcAheadOrBehind; 
		//internal IfcCoordinatedUniversalTimeOffset(IfcCoordinatedUniversalTimeOffset v) : base() { mHourOffset = v.mHourOffset; mMinuteOffset = v.mMinuteOffset; mSense = v.mSense; }
		internal IfcCoordinatedUniversalTimeOffset() : base() { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4)]
	public abstract partial class IfcCoordinateOperation : BaseClassIfc // IFC4 	ABSTRACT SUPERTYPE OF(IfcMapConversion);
	{
		internal IfcCoordinateReferenceSystemSelect mSourceCRS;// :	IfcCoordinateReferenceSystemSelect;
		private IfcCoordinateReferenceSystem mTargetCRS;// :	IfcCoordinateReferenceSystem;

		public IfcCoordinateReferenceSystemSelect SourceCRS { get { return mSourceCRS; } set { mSourceCRS = value; if(value.HasCoordinateOperation != this) value.HasCoordinateOperation = this; } }
		public IfcCoordinateReferenceSystem TargetCRS { get { return mTargetCRS; } set { mTargetCRS = value; } }

		protected IfcCoordinateOperation() : base() { }
		protected IfcCoordinateOperation(DatabaseIfc db, IfcCoordinateOperation p) : base(db, p) { SourceCRS = db.Factory.Duplicate(p.mSourceCRS) as IfcCoordinateReferenceSystemSelect; TargetCRS = db.Factory.Duplicate(p.TargetCRS) as IfcCoordinateReferenceSystem; }
		protected IfcCoordinateOperation(IfcCoordinateReferenceSystemSelect source, IfcCoordinateReferenceSystem target) : base(source.Database) { SourceCRS = source; TargetCRS = target; }
	}
	[Serializable]
	public abstract partial class IfcCoordinateReferenceSystem : BaseClassIfc, IfcCoordinateReferenceSystemSelect, NamedObjectIfc  // IFC4 	ABSTRACT SUPERTYPE OF(IfcGeographicCRS, IfcProjectedCRS);
	{
		internal string mName;//	:	OPTIONAL IfcLabel;
		internal string mDescription = "";//	:	OPTIONAL IfcText;
		internal string mGeodeticDatum = ""; //	: OPTIONAL IfcIdentifier;
		//INVERSE
		private IfcCoordinateOperation mHasCoordinateOperation = null;
		private IfcWellKnownText mWellKnownText = null;

		public string Name { get { return mName; } set { mName = string.IsNullOrEmpty(value) ? "Unknown" :  value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public string GeodeticDatum { get { return mGeodeticDatum; } set { mGeodeticDatum = value; } }
		public IfcCoordinateOperation HasCoordinateOperation { get { return mHasCoordinateOperation; } set { mHasCoordinateOperation = value; value.SourceCRS = this; } }
		public IfcWellKnownText WellKnownText { get { return mWellKnownText; } set { mWellKnownText = value; } }

		protected IfcCoordinateReferenceSystem() : base() { }
		protected IfcCoordinateReferenceSystem(DatabaseIfc db, IfcCoordinateReferenceSystem p) : base(db,p)
		{ 
			mName = p.mName; 
			mDescription = p.mDescription;
			mGeodeticDatum = p.mGeodeticDatum;
		}
		protected IfcCoordinateReferenceSystem(DatabaseIfc db) : base(db) { }

	}
	public interface IfcCoordinateReferenceSystemSelect : IBaseClassIfc // IfcCoordinateReferenceSystem, IfcGeometricRepresentationContext
	{
		IfcCoordinateOperation HasCoordinateOperation { get; set; }
	}
	[Serializable]
	public partial class IfcCosineSpiral : IfcSpiral
	{
		private double mCosineTerm = 0; //: IfcLengthMeasure;
		private double mConstantTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;

		public double CosineTerm { get { return mCosineTerm; } set { mCosineTerm = value; } }
		public double ConstantTerm { get { return mConstantTerm; } set { mConstantTerm = value; } }

		public IfcCosineSpiral() : base() { }
		internal IfcCosineSpiral(DatabaseIfc db, IfcCosineSpiral cosine, DuplicateOptions options)
			: base(db, cosine, options) { CosineTerm = cosine.CosineTerm; ConstantTerm = cosine.ConstantTerm;  }
		public IfcCosineSpiral(IfcAxis2Placement position, double cosineTerm)
			: base(position) { CosineTerm = cosineTerm; }
	}
	[Serializable]
	public partial class IfcCostItem : IfcControl
	{
		private IfcCostItemTypeEnum mPredefinedType = IfcCostItemTypeEnum.NOTDEFINED; // IFC4
		internal List<int> mCostValues = new List<int>();//	 : OPTIONAL LIST [1:?] OF IfcCostValue; IFC4
		internal List<int> mCostQuantities = new List<int>();//	 : OPTIONAL LIST [1:?] OF IfcPhysicalQuantity; IFC4

		public IfcCostItemTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCostItemTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCostItem() : base() { } 
		internal IfcCostItem(DatabaseIfc db, IfcCostItem i, DuplicateOptions options) : base(db, i, options) { PredefinedType = i.PredefinedType; }
		public IfcCostItem(IfcCostSchedule s, IfcCostItemTypeEnum t)
			: base(s.mDatabase) { s.AddAggregated(this); PredefinedType = t; }
		public IfcCostItem(IfcCostItem i, IfcCostItemTypeEnum t)
			: base(i.mDatabase) { i.AddNested(this); PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcCostSchedule : IfcControl
	{
		private IfcCostScheduleTypeEnum mPredefinedType = IfcCostScheduleTypeEnum.NOTDEFINED;// :	OPTIONAL IfcCostScheduleTypeEnum;
		internal string mStatus = "";// : OPTIONAL IfcLabel; 
		private DateTime mSubmittedOn = DateTime.Now;// : 	IfcDateTime
		private IfcDateTimeSelect mSubmittedOnSS = null; // : OPTIONAL IfcDateTimeSelect;
		private DateTime mUpdateDate = DateTime.Now;// : 	IfcDateTime
		private IfcDateTimeSelect mUpdateDateSS = null; // : OPTIONAL IfcDateTimeSelect; 
		private IfcActorSelect mSubmittedBy;// : OPTIONAL IfcActorSelect; IFC4 DELETED 
		private IfcActorSelect mPreparedBy;// : OPTIONAL IfcActorSelect; IFC4 DELETED 
		private SET<IfcActorSelect> mTargetUsers = new SET<IfcActorSelect>();// : OPTIONAL SET [1:?] OF IfcActorSelect; //IFC4 DELETED 
		//internal string mID;// : IfcIdentifier; IFC4 relocated 
		public IfcCostScheduleTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCostScheduleTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public string Staus { get { return mStatus; } set { mStatus = value; } }
		internal DateTime SubmittedOn
		{
			get { return (mSubmittedOnSS != null ? mSubmittedOnSS.DateTime : mSubmittedOn); }
			set
			{
				if (mDatabase.Release <= ReleaseVersion.IFC2x3)
					mSubmittedOnSS = new IfcCalendarDate(mDatabase, value.Day, value.Month, value.Year);
				else
					mSubmittedOn = value;
			}
		}
		internal DateTime UpdateDate
		{
			get { return (mUpdateDateSS != null ? mUpdateDateSS.DateTime : mUpdateDate); }
			set
			{
				if (mDatabase.Release <= ReleaseVersion.IFC2x3)
					mUpdateDateSS = new IfcCalendarDate(mDatabase, value.Day, value.Month, value.Year);
				else
					mUpdateDate = value;
			}
		}
		internal IfcCostSchedule() : base() { }
		internal IfcCostSchedule(DatabaseIfc db, IfcCostSchedule s, DuplicateOptions options) : base(db,s, options)
		{
			mSubmittedBy = s.mSubmittedBy;
			mPreparedBy = s.mPreparedBy;
			mSubmittedBy = s.mSubmittedBy;
			mStatus = s.mStatus;
			mUpdateDate = s.mUpdateDate; 
			PredefinedType = s.PredefinedType;
			if (s.mSubmittedBy != null)
				mSubmittedBy = db.Factory.Duplicate(s.mSubmittedBy, options) as IfcActorSelect;
			if (s.mPreparedBy != null)
				mPreparedBy = db.Factory.Duplicate(s.mPreparedBy, options) as IfcActorSelect;
			if (s.mTargetUsers.Count > 0)
				mTargetUsers.AddRange(s.mTargetUsers.Select(x => db.Factory.Duplicate(x, options) as IfcActorSelect));
		}
		internal IfcCostSchedule(DatabaseIfc db, IfcCostScheduleTypeEnum t, string status, DateTime submitted, IfcProject prj)
			: base(db) 
		{
			PredefinedType = t;
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
		//internal string mCondition = "";//  : OPTIONAL IfcText; IFC4 moved to condition
		internal IfcCostValue() : base() { }
		internal IfcCostValue(DatabaseIfc db, IfcCostValue v) : base(db,v) { }
		public IfcCostValue(DatabaseIfc db) : base(db) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcCourse : IfcBuiltElement
	{
		private IfcCourseTypeEnum mPredefinedType = IfcCourseTypeEnum.NOTDEFINED; //: OPTIONAL IfcCourseTypeEnum;
		public IfcCourseTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCourseTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcCourse() : base() { }
		public IfcCourse(DatabaseIfc db) : base(db) { }
		public IfcCourse(DatabaseIfc db, IfcCourse course, DuplicateOptions options) : base(db, course, options) { PredefinedType = course.PredefinedType; }
		public IfcCourse(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcCourseType : IfcBuiltElementType
	{
		private IfcCourseTypeEnum mPredefinedType = IfcCourseTypeEnum.NOTDEFINED; //: IfcCourseTypeEnum;
		public IfcCourseTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCourseTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcCourseType() : base() { }
		public IfcCourseType(DatabaseIfc db, IfcCourseType courseType, DuplicateOptions options) : base(db, courseType, options) { PredefinedType = courseType.PredefinedType; }
		public IfcCourseType(DatabaseIfc db, string name, IfcCourseTypeEnum predefinedType)
			: base(db, name) { PredefinedType = predefinedType; }
	}
	[Serializable]
	public partial class IfcCovering : IfcBuiltElement
	{
		private IfcCoveringTypeEnum mPredefinedType = IfcCoveringTypeEnum.NOTDEFINED;// : OPTIONAL IfcCoveringTypeEnum;
		//INVERSE
		internal IfcRelCoversSpaces mCoversSpaces = null;//	 : 	SET [0:1] OF IfcRelCoversSpaces FOR RelatedCoverings;
		internal IfcRelCoversBldgElements mCoversElements = null;//	 : 	SET [0:1] OF IfcRelCoversBldgElements FOR RelatedCoverings;

		public IfcCoveringTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCoveringTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public IfcRelCoversSpaces CoversSpaces { get { return mCoversSpaces; } set { mCoversSpaces = value; } }
		
		internal IfcCovering() : base() { }
		internal IfcCovering(DatabaseIfc db, IfcCovering c, DuplicateOptions options) : base(db, c, options) { PredefinedType = c.PredefinedType; }
		public IfcCovering(DatabaseIfc db) : base(db) { }
		public IfcCovering(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcCoveringType : IfcBuiltElementType
	{
		private IfcCoveringTypeEnum mPredefinedType = IfcCoveringTypeEnum.NOTDEFINED;
		public IfcCoveringTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCoveringTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCoveringType() : base() { }
		internal IfcCoveringType(DatabaseIfc db, IfcCoveringType t, DuplicateOptions options) : base(db,t,options) { PredefinedType = t.PredefinedType; }
		public IfcCoveringType(DatabaseIfc db, string name, IfcCoveringTypeEnum type) : base(db) { Name = name; PredefinedType = type; } 
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
		internal IfcCraneRailAShapeProfileDef(DatabaseIfc db, IfcCraneRailAShapeProfileDef p, DuplicateOptions options) : base(db, p, options)
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
		internal IfcCraneRailFShapeProfileDef(DatabaseIfc db, IfcCraneRailFShapeProfileDef p, DuplicateOptions options)
			: base(db, p, options)
		{
			mOverallHeight = p.mOverallHeight; mHeadWidth = p.mHeadWidth; mRadius = p.mRadius; mHeadDepth2 = p.mHeadDepth2; mHeadDepth3 = p.mHeadDepth3;
			mWebThickness = p.mWebThickness; mBaseDepth1 = p.mBaseDepth1; mBaseDepth2 = p.mBaseDepth2; mCentreOfGravityInY = p.mCentreOfGravityInY;
		}
	} 
	[Serializable]
	public partial class IfcCrewResource : IfcConstructionResource
	{
		private IfcCrewResourceTypeEnum mPredefinedType = IfcCrewResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcCrewResourceTypeEnum; 
		public IfcCrewResourceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCrewResourceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCrewResource() : base() { }
		internal IfcCrewResource(DatabaseIfc db, IfcCrewResource r, DuplicateOptions options) : base(db, r, options) { PredefinedType = r.PredefinedType; }
		public IfcCrewResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcCrewResourceType : IfcConstructionResourceType //IFC4
	{
		private IfcCrewResourceTypeEnum mPredefinedType = IfcCrewResourceTypeEnum.NOTDEFINED;
		public IfcCrewResourceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCrewResourceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCrewResourceType() : base() { }
		internal IfcCrewResourceType(DatabaseIfc db, IfcCrewResourceType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcCrewResourceType(DatabaseIfc db, string name, IfcCrewResourceTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcCsgPrimitive3D : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBlock, IfcRectangularPyramid, IfcRightCircularCone, IfcRightCircularCylinder, IfcSphere))*/
	{
		private IfcAxis2Placement3D mPosition;// : IfcAxis2Placement3D;
		public IfcAxis2Placement3D Position { get { return mPosition; } set { mPosition = value; } }

		protected IfcCsgPrimitive3D() : base() { }
		protected IfcCsgPrimitive3D(IfcAxis2Placement3D position) :base (position.mDatabase) { Position = position; }
		protected IfcCsgPrimitive3D(DatabaseIfc db, IfcCsgPrimitive3D p, DuplicateOptions options) : base(db, p, options) { Position = db.Factory.Duplicate(p.Position) as IfcAxis2Placement3D; }
	}
	public partial interface IfcCsgSelect : IBaseClassIfc { } //	IfcBooleanResult, IfcCsgPrimitive3D
	[Serializable]
	public partial class IfcCsgSolid : IfcSolidModel
	{
		private IfcCsgSelect mTreeRootExpression;// : IfcCsgSelect
		public IfcCsgSelect TreeRootExpression { get { return mTreeRootExpression; } set { mTreeRootExpression = value; } }

		internal IfcCsgSolid() : base() { }
		internal IfcCsgSolid(DatabaseIfc db, IfcCsgSolid p, DuplicateOptions options) : base(db, p, options) { TreeRootExpression = db.Factory.Duplicate<IfcCsgSelect>(p.mTreeRootExpression); }
		public IfcCsgSolid(IfcCsgSelect csg) : base(csg.Database) { TreeRootExpression = csg; }
	}
	[Serializable]
	public partial class IfcCShapeProfileDef : IfcParameterizedProfileDef
	{
		private double mDepth, mWidth, mWallThickness, mGirth;// : IfcPositiveLengthMeasure;
		private double mInternalFilletRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		private double mCentreOfGravityInX = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure // DELETED IFC4 	Superseded by respective attribute of IfcStructuralProfileProperties 

		public double Depth { get { return mDepth; } set { mDepth = value; } }
		public double Width { get { return mWidth; } set { mWidth = value; } }
		public double WallThickness { get { return mWallThickness; } set { mWallThickness = value; } }
		public double Girth { get { return mGirth; } set { mGirth = value; } }
		public double InternalFilletRadius 
		{ 
			get { return mInternalFilletRadius; } 
			set 
			{
				double eps = (mDatabase == null ? 1e-5 : mDatabase.Tolerance);
				mInternalFilletRadius = (value < eps ? double.NaN : value); 
			} 
		}
		
		internal IfcCShapeProfileDef() : base() { }
		internal IfcCShapeProfileDef(DatabaseIfc db, IfcCShapeProfileDef c, DuplicateOptions options) : base(db, c, options)
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
	[Serializable]
	public partial class IfcCurrencyRelationship : IfcResourceLevelRelationship
	{
		private IfcMonetaryUnit mRelatingMonetaryUnit = null; //: IfcMonetaryUnit;
		private IfcMonetaryUnit mRelatedMonetaryUnit = null; //: IfcMonetaryUnit;
		private double mExchangeRate = 0; //: IfcPositiveRatioMeasure;
		private DateTime mRateDateTime = DateTime.MinValue; //: OPTIONAL IfcDateTime;
		private IfcLibraryInformation mRateSource = null; //: OPTIONAL IfcLibraryInformation;

		public IfcMonetaryUnit RelatingMonetaryUnit { get { return mRelatingMonetaryUnit; } set { mRelatingMonetaryUnit = value; } }
		public IfcMonetaryUnit RelatedMonetaryUnit { get { return mRelatedMonetaryUnit; } set { mRelatedMonetaryUnit = value; } }
		public double ExchangeRate { get { return mExchangeRate; } set { mExchangeRate = value; } }
		public DateTime RateDateTime { get { return mRateDateTime; } set { mRateDateTime = value; } }
		public IfcLibraryInformation RateSource { get { return mRateSource; } set { mRateSource = value; } }

		public IfcCurrencyRelationship() : base() { }
		public IfcCurrencyRelationship(IfcMonetaryUnit relatingMonetaryUnit, IfcMonetaryUnit relatedMonetaryUnit, double exchangeRate)
			: base(relatedMonetaryUnit.Database)
		{
			RelatingMonetaryUnit = relatingMonetaryUnit;
			RelatedMonetaryUnit = relatedMonetaryUnit;
			ExchangeRate = exchangeRate;
		}
	}
	[Serializable]
	public partial class IfcCurtainWall : IfcBuiltElement
	{
		private IfcCurtainWallTypeEnum mPredefinedType = IfcCurtainWallTypeEnum.NOTDEFINED;//: OPTIONAL IfcCurtainWallTypeEnum; 
		public IfcCurtainWallTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCurtainWallTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCurtainWall() : base() { }
		internal IfcCurtainWall(DatabaseIfc db, IfcCurtainWall w, DuplicateOptions options) : base(db, w, options) { PredefinedType = w.PredefinedType; }
		public IfcCurtainWall(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcCurtainWallType : IfcBuiltElementType
	{
		private IfcCurtainWallTypeEnum mPredefinedType = IfcCurtainWallTypeEnum.NOTDEFINED;
		public IfcCurtainWallTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcCurtainWallTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcCurtainWallType() : base() { }
		internal IfcCurtainWallType(DatabaseIfc db, IfcCurtainWallType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcCurtainWallType(DatabaseIfc db, string name, IfcCurtainWallTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcCurve : IfcGeometricRepresentationItem, IfcGeometricSetSelect, IfcLinearAxisSelect /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBoundedCurve, IfcConic, IfcLine, IfcOffsetCurve2D, IfcOffsetCurve3D, IfcPcurve, IfcClothoid))*/
	{   //IF Adding a new subtype also consider IfcTrimmedCurve constructor
		//INVERSE
		internal IfcLinearPositioningElement mPositioningElement = null;// : SET[0:1] OF IfcLinearPositioningElement FOR Axis;
		internal IfcEdgeCurve mOfEdge = null; //INVERSE GeomGym
		internal SET<IfcSectionedSolid> mDirectrixOfSectionedSolids = new SET<IfcSectionedSolid>();
		internal SET<IfcOffsetCurve> mBasisCurveOfOffsets = new SET<IfcOffsetCurve>();

		public IfcLinearPositioningElement PositioningElement { get { return mPositioningElement; } set { mPositioningElement = value; } }

		protected IfcCurve() : base() { }
		protected IfcCurve(DatabaseIfc db) : base(db) { }
		protected IfcCurve(DatabaseIfc db, IfcCurve c, DuplicateOptions options) : base(db, c, options) { }
	}
	[Serializable]
	public partial class IfcCurveBoundedPlane : IfcBoundedSurface
	{
		internal IfcPlane mBasisSurface;// : IfcPlane;
		internal IfcCurve mOuterBoundary;// : IfcCurve;
		internal SET<IfcCurve> mInnerBoundaries = new SET<IfcCurve>();//: SET OF IfcCurve;

		public IfcPlane BasisSurface { get { return mBasisSurface; } set { mBasisSurface = value; } }
		public IfcCurve OuterBoundary { get { return mOuterBoundary; } set { mOuterBoundary = value; } }
		public SET<IfcCurve> InnerBoundaries { get { return mInnerBoundaries; } }

		internal IfcCurveBoundedPlane() : base() { }
		internal IfcCurveBoundedPlane(DatabaseIfc db, IfcCurveBoundedPlane p, DuplicateOptions options) 
			: base(db, p, options) 
		{ 
			BasisSurface = db.Factory.Duplicate(p.BasisSurface) as IfcPlane; 
			OuterBoundary = db.Factory.Duplicate(p.OuterBoundary) as IfcCurve;
			InnerBoundaries.AddRange(p.InnerBoundaries.Select(x=> db.Factory.Duplicate(x) as IfcCurve));
		}
		public IfcCurveBoundedPlane(IfcPlane p, IfcCurve outer) : base(p.mDatabase) { BasisSurface = p; OuterBoundary = outer;  }
		public IfcCurveBoundedPlane(IfcPlane p, IfcCurve outer, IEnumerable<IfcCurve> inner)
			: this(p, outer) { InnerBoundaries.AddRange(inner); }
	}
	[Serializable]
	public partial class IfcCurveBoundedSurface : IfcBoundedSurface //IFC4
	{
		private IfcSurface mBasisSurface;// : IfcSurface; 
		private SET<IfcBoundaryCurve> mBoundaries = new SET<IfcBoundaryCurve>();//: SET [1:?] OF IfcBoundaryCurve;
		private bool mImplicitOuter = false;//	 :	BOOLEAN; 

		public IfcSurface BasisSurface { get { return mBasisSurface; } set { mBasisSurface = value; } }
		public SET<IfcBoundaryCurve> Boundaries { get { return mBoundaries; } }
		public bool ImplicitOuter { get { return mImplicitOuter; } }

		internal IfcCurveBoundedSurface() : base() { }
		internal IfcCurveBoundedSurface(DatabaseIfc db, IfcCurveBoundedSurface s, DuplicateOptions options) : base(db, s, options) 
		{
			BasisSurface = db.Factory.Duplicate(s.BasisSurface) as IfcSurface;
			Boundaries.AddRange(s.Boundaries.Select(x => db.Factory.Duplicate(x) as IfcBoundaryCurve));
			mImplicitOuter = s.mImplicitOuter; 
		}
		public IfcCurveBoundedSurface(DatabaseIfc db, IfcSurface s, List<IfcBoundaryCurve> bounds)
			: base(db) { BasisSurface = s; Boundaries.AddRange(bounds); mImplicitOuter = false; }
	}
	public interface IfcCurveMeasureSelect : IBaseClassIfc { } // SELECT(IfcParameterValue, IfcNonNegativeLengthMeasure);
	public interface IfcCurveOnSurface : IBaseClassIfc { } // SELECT(IfcCompositeCurveOnSurface, IfcPcurve, IfcSurfaceCurve);
	public interface IfcCurveOrEdgeCurve : IBaseClassIfc { }  // = SELECT (	IfcBoundedCurve, IfcEdgeCurve);
	[Serializable]
	public partial class IfcCurveSegment : IfcSegment
	{
		private IfcPlacement mPlacement = null; //: IfcPlacement;
		private IfcCurveMeasureSelect mSegmentStart = null; //: IfcCurveMeasureSelect;
		private IfcCurveMeasureSelect mSegmentLength = null; //: IfcCurveMeasureSelect;
		private IfcCurve mParentCurve = null; //: IfcCurve;

		public IfcPlacement Placement { get { return mPlacement; } set { mPlacement = value; } }
		public IfcCurveMeasureSelect SegmentStart { get { return mSegmentStart; } set { mSegmentStart = value; } }
		public IfcCurveMeasureSelect SegmentLength { get { return mSegmentLength; } set { mSegmentLength = value; } }
		public IfcCurve ParentCurve { get { return mParentCurve; } set { mParentCurve = value; } }

		public IfcCurveSegment() : base() { }
		internal IfcCurveSegment(DatabaseIfc db, IfcCurveSegment curveSegment, DuplicateOptions options)
			: base(db, curveSegment, options)
		{
			Placement = db.Factory.Duplicate(curveSegment.Placement, options) as IfcPlacement;
			SegmentStart = curveSegment.SegmentStart;
			SegmentLength = curveSegment.SegmentLength;
			ParentCurve = db.Factory.Duplicate(curveSegment.ParentCurve, options) as IfcCurve;
		}
		public IfcCurveSegment(IfcTransitionCode transition, IfcPlacement placement, IfcCurveMeasureSelect segmentStart, IfcCurveMeasureSelect segmentLength, IfcCurve parentCurve)
			: base(placement.Database, transition)
		{
			Placement = placement;
			SegmentStart = segmentStart;
			SegmentLength = segmentLength;
			ParentCurve = parentCurve;
		}
	}
	[Obsolete("DEPRECATED IFC4X3", false)]
	[Serializable]
	public abstract partial class IfcCurveSegment2D : IfcBoundedCurve //ABSTRACT SUPERTYPE OF(ONEOF(IfcCircularArcSegment2D, IfcLineSegment2D, IfcTransitionCurveSegment2D))
	{
		private IfcCartesianPoint mStartPoint;// : IfcCartesianPoint;
		private double mStartDirection;// : IfcPlaneAngleMeasure;
		private double mSegmentLength;// : IfcPositiveLengthMeasure;
		//INVERSE GG
		internal IfcAlignment2DHorizontalSegment mToSegment = null;

		public IfcCartesianPoint StartPoint { get { return mStartPoint; } set { mStartPoint = value; } }
		public double StartDirection { get { return mStartDirection; } set { mStartDirection = value; } }
		public double SegmentLength { get { return mSegmentLength; } set { mSegmentLength = value; } }

		protected IfcCurveSegment2D() : base() { }
		protected IfcCurveSegment2D(DatabaseIfc db, IfcCurveSegment2D p, DuplicateOptions options) : base(db, p, options) { StartPoint = db.Factory.Duplicate(p.StartPoint) as IfcCartesianPoint; mStartDirection = p.mStartDirection; mSegmentLength = p.mSegmentLength; }
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
		internal IfcCurveFontOrScaledCurveFontSelect mCurveFont = null;// : OPTIONAL IfcCurveFontOrScaledCurveFontSelect;
		internal IfcSizeSelect mCurveWidth = null;// : OPTIONAL IfcSizeSelect; 
		internal IfcColour mCurveColour = null;// : OPTIONAL IfcColour;
		internal IfcLogicalEnum mModelOrDraughting = IfcLogicalEnum.UNKNOWN;//	:	OPTIONAL BOOLEAN; IFC4 

		public IfcCurveFontOrScaledCurveFontSelect CurveFont { get { return mCurveFont; } set { mCurveFont = value; } }
		public IfcSizeSelect CurveWidth { get { return mCurveWidth; } set { mCurveWidth = value; } }
		public IfcColour CurveColour { get { return mCurveColour; } set { mCurveColour = value; } }
		public bool ModelOrDraughting { get { return mModelOrDraughting == IfcLogicalEnum.TRUE; } set { mModelOrDraughting = value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE; } }

		internal IfcCurveStyle() : base() { }
		internal IfcCurveStyle(DatabaseIfc db, IfcCurveStyle s, DuplicateOptions options) : base(db, s, options)
		{
			if(s.mCurveFont != null)
				CurveFont = db.Factory.Duplicate(s.mCurveFont, options);
			mCurveWidth = s.mCurveWidth;
			if(s.mCurveColour != null)
				CurveColour = db.Factory.Duplicate(s.mCurveColour, options);
			mModelOrDraughting = s.mModelOrDraughting;
		}
		public IfcCurveStyle(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcCurveStyleFont : IfcPresentationItem, IfcCurveStyleFontSelect, NamedObjectIfc
	{
		internal string mName = "";// : OPTIONAL IfcLabel;
		internal LIST<IfcCurveStyleFontPattern> mPatternList = new LIST<IfcCurveStyleFontPattern>();// :  LIST [1:?] OF IfcCurveStyleFontPattern;

		public string Name { get { return mName; } set { mName = value; } }

		internal IfcCurveStyleFont() : base() { }
		internal IfcCurveStyleFont(DatabaseIfc db, IfcCurveStyleFont f, DuplicateOptions options) : base(db, f, options)
		{
			mName = f.mName;
			mPatternList.AddRange(f.mPatternList.Select(x => db.Factory.Duplicate(x) as IfcCurveStyleFontPattern));
		}
	}
	[Serializable]
	public partial class IfcCurveStyleFontAndScaling : IfcPresentationItem, IfcCurveFontOrScaledCurveFontSelect
	{
		internal string mName; // : 	OPTIONAL IfcLabel;
		internal IfcCurveStyleFontSelect mCurveStyleFont;// : 	IfcCurveStyleFontSelect;
		internal double mCurveFontScaling;//: 	IfcPositiveRatioMeasure;
		internal IfcCurveStyleFontAndScaling() : base() { }
		internal IfcCurveStyleFontAndScaling(DatabaseIfc db, IfcCurveStyleFontAndScaling f, DuplicateOptions options) : base(db, f, options)
		{
			mName = f.mName;
			mCurveStyleFont = db.Factory.Duplicate(f.mCurveStyleFont);
			mCurveFontScaling = f.mCurveFontScaling;
		}
		public IfcCurveStyleFontAndScaling(IfcCurveStyleFontSelect curveStyleFont, double scaling)
		{
			mCurveStyleFont = curveStyleFont;
			mCurveFontScaling = scaling;
		}
	}
	public interface IfcCurveFontOrScaledCurveFontSelect : IBaseClassIfc { } //SELECT (IfcCurveStyleFontAndScaling ,IfcCurveStyleFontSelect);
	[Serializable]
	public partial class IfcCurveStyleFontPattern : IfcPresentationItem
	{
		internal double mVisibleSegmentLength;// : IfcLengthMeasure;
		internal double mInvisibleSegmentLength;//: IfcPositiveLengthMeasure;	
		internal IfcCurveStyleFontPattern() : base() { }
		internal IfcCurveStyleFontPattern(DatabaseIfc db, IfcCurveStyleFontPattern p, DuplicateOptions options) : base(db, p, options) 
		{ 
			mVisibleSegmentLength = p.mVisibleSegmentLength; 
			mInvisibleSegmentLength = p.mInvisibleSegmentLength; 
		}
	}
	public interface IfcCurveStyleFontSelect : IfcCurveFontOrScaledCurveFontSelect { } //SELECT (IfcCurveStyleFont ,IfcPreDefinedCurveFont);
	[Serializable]
	public partial class IfcCylindricalSurface : IfcElementarySurface //IFC4
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		public double Radius { get { return mRadius; } set { mRadius = value; } }

		internal IfcCylindricalSurface() : base() { }
		internal IfcCylindricalSurface(DatabaseIfc db, IfcCylindricalSurface s, DuplicateOptions options) : base(db, s, options) { mRadius = s.mRadius; }
		public IfcCylindricalSurface(IfcAxis2Placement3D placement, double radius) : base(placement) { Radius = radius; }
	}
}
