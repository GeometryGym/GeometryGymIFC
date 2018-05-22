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
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

using Rhino.Geometry;

namespace GeometryGym.Ifc
{
	public partial class IfcCartesianPoint : IfcPoint
	{
		public override Point3d Location { get { return new Point3d(mCoordinateX, double.IsNaN( mCoordinateY) ? 0 : mCoordinateY, double.IsNaN(mCoordinateZ) ? 0 : mCoordinateZ); } }
		internal Point3d Coordinates3d { set { mCoordinateX = value.X; mCoordinateY = value.Y; mCoordinateZ = value.Z; } }
		internal Point2d Coordinates2d { set { mCoordinateX = value.X; mCoordinateY = value.Y; mCoordinateZ = double.NaN; } }
		internal IfcCartesianPoint(DatabaseIfc m, Point3d pt) : base(m) { Coordinates3d = pt; }
		internal void adopt(Point3d pt)
		{
			if (this.is2D)
				Coordinates2d = new Point2d(pt.X, pt.Y);
			else
				Coordinates3d = pt;
		}
		internal IfcCartesianPoint(DatabaseIfc m, Point2d pt) : base(m) { Coordinates2d = pt; }
	}
	public abstract partial class IfcCartesianPointList : IfcGeometricRepresentationItem //IFC4
	{
		internal abstract List<Point3d> Points { get; }
	}
	public partial class IfcCartesianPointList2D : IfcCartesianPointList //IFC4
	{
		public IfcCartesianPointList2D(DatabaseIfc db, IEnumerable<Point2d> coordList) : base(db)
		{
			mCoordList = coordList.Select(x => new double[] { x.X, x.Y }).ToArray();
		}
		internal override List<Point3d> Points { get { return mCoordList.ToList().ConvertAll(x => new Point3d(x[0], x[1], 0)); } }
	}
	public partial class IfcCartesianPointList3D : IfcCartesianPointList //IFC4
	{
		public IfcCartesianPointList3D(DatabaseIfc db, IEnumerable<Point3d> coordList) : base(db) { mCoordList = coordList.Select(x=> new double[] { x.X, x.Y, x.Z }).ToArray(); }
		internal override List<Point3d> Points { get { return mCoordList.Select(x => new Point3d(x[0], x[1], x[2])).ToList(); } }
	}
	public abstract partial class IfcCartesianTransformationOperator
	{
		internal Transform Transform
		{
			get
			{
				IfcCartesianPoint cp = LocalOrigin;
				Point3d p = (cp == null ? Point3d.Origin : cp.Location);
				return Transform.Translation(p.X, p.Y, p.Z) * vecsTransform() * getScaleTransform(p);
			}
		}
		internal virtual Transform getScaleTransform(Point3d location) { return double.IsNaN(mScale) ? Transform.Identity : Transform.Scale(location, mScale); }
		protected virtual Transform vecsTransform()
		{
			Vector3d vx = new Vector3d(1, 0, 0), vy = new Vector3d(0, 1, 0);
			Transform tr = Transform.Identity;
			if (mAxis1 > 0)
			{
				vx = Axis1.Vector3d;
				tr.M00 = vx.X;
				tr.M10 = vx.Y;
				tr.M20 = vx.Z;
			}
			if (mAxis2 > 0)
			{
				vy = Axis2.Vector3d;
				tr.M01 = vy.X;
				tr.M11 = vy.Y;
				tr.M21 = vy.Z;
			}
			return tr;
		}
	}
	public partial class IfcCartesianTransformationOperator2DnonUniform
	{
		internal override Transform getScaleTransform(Point3d location) { return Transform.Scale(new Plane(location, Vector3d.XAxis, Vector3d.YAxis), Scale, mScale2, 1); }
	}
	public partial class IfcCartesianTransformationOperator3D
	{
		internal Vector3d Axis3Vector { get { return (mAxis3 > 0 ? Axis3.Vector3d : Vector3d.ZAxis); } }
		protected override Transform vecsTransform()
		{
			Transform tr = base.vecsTransform();
			Vector3d v = Axis3Vector;
			tr.M02 = v.X;
			tr.M12 = v.Y;
			tr.M22 = v.Z;
			return tr;
		}
	}
	public partial class IfcCartesianTransformationOperator3DnonUniform
	{
		internal override Transform getScaleTransform(Point3d location) { return Transform.Scale(new Plane(location, Vector3d.XAxis, Vector3d.YAxis), Scale, Scale2, Scale3); }
	}
	public partial class IfcCompositeCurve : IfcBoundedCurve
	{
		
		
	}
	public partial class IfcCircle : IfcConic
	{
		public override Curve Curve { get { return new ArcCurve(Circle); } }
		public Circle Circle { get { return new Circle(Plane, mRadius); } }
	}
	public partial class IfcCompositeCurveSegment
	{
		//internal IfcCompositeCurveSegment(DatabaseIfc db, Curve c, bool sense, IfcTransitionCode tc, bool twoD, IfcCartesianPoint optStrt, out IfcCartesianPoint end)
		//	: this(tc, sense, IfcBoundedCurve.convCurve(db, c, optStrt, twoD, out end)) { }
	}
	public abstract partial class IfcConic : IfcCurve /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCircle ,IfcEllipse))*/
	{
		public Transform Transform { get { return (mPosition > 0 ? Position.Transform : Transform.Translation(0, 0, 0)); } }
		public Plane Plane { get { return (mPosition > 0 ? Position.Plane : Plane.WorldXY); } }
	}
	public partial class IfcConnectionPointEccentricity
	{
		internal Vector3d Eccentricity { get { return new Vector3d(double.IsNaN(mEccentricityInX) ? 0 : mEccentricityInX, double.IsNaN(mEccentricityInY) ? 0 : mEccentricityInY, double.IsNaN(mEccentricityInZ) ? 0 : mEccentricityInZ); } }

		internal IfcConnectionPointEccentricity(IfcPointOrVertexPoint v, Vector3d ecc) : base(v) { mEccentricityInX = ecc.X; mEccentricityInY = ecc.Y; mEccentricityInZ = ecc.Z; }
	}
	public abstract partial class IfcCurve : IfcGeometricRepresentationItem, IfcGeometricSetSelect /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBoundedCurve ,IfcConic ,IfcLine ,IfcOffsetCurve2D ,IfcOffsetCurve3D,IfcPcurve,IfcClothoid))*/
	{
		public abstract Curve Curve { get; }
	}
}
