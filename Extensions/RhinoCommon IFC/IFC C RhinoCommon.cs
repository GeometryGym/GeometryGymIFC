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
	public partial class IfcCartesianPoint
	{
		public override Point3d Location { get { return new Point3d(mCoordinateX, double.IsNaN( mCoordinateY) ? 0 : mCoordinateY, double.IsNaN(mCoordinateZ) ? 0 : mCoordinateZ); } }
		public Point2d Location2d() { return new Point2d(mCoordinateX, double.IsNaN( mCoordinateY) ? 0 : mCoordinateY); }
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
	public partial class IfcCartesianPointList
	{
		public abstract List<Point3d> Points();
	}
	public partial class IfcCartesianPointList2D
	{
		public IfcCartesianPointList2D(DatabaseIfc db, IEnumerable<Point2d> coordList) : base(db)
		{
			mCoordList.AddRange(coordList.Select(x => new Tuple<double, double>(x.X, x.Y)));
		}
		public override List<Point3d> Points() { return mCoordList.Select(x => new Point3d(x.Item1, x.Item2, 0)).ToList(); }
	}
	public partial class IfcCartesianPointList3D
	{
		public IfcCartesianPointList3D(DatabaseIfc db, IEnumerable<Point3d> coordList) : base(db) 
		{
			mCoordList.AddRange(coordList.Select(x=> new Tuple<double, double, double>(x.X, x.Y, x.Z))); }
		public override List<Point3d> Points() { return mCoordList.Select(x => new Point3d(x.Item1, x.Item2, x.Item3)).ToList(); } 
	}
	public partial class IfcCartesianTransformationOperator
	{
		public Transform Transform()
		{
			IfcCartesianPoint cp = LocalOrigin;
			Point3d p = (cp == null ? Point3d.Origin : cp.Location);
			Transform translation = Rhino.Geometry.Transform.Translation(p.X, p.Y, p.Z);
			Transform changeBasis = vecsTransform();
			Transform scale = getScaleTransform();
			Transform result = translation * changeBasis * scale;
			return result;
		}

		internal Plane placementPlane()
		{
			IfcCartesianPoint cp = LocalOrigin;
			Transform changeBasis = vecsTransform();
			Plane plane = Plane.WorldXY;
			plane.Transform(changeBasis);
			if (cp != null)
				plane.Origin = cp.Location;
			return plane;
		}
		internal virtual Transform getScaleTransform() 
		{
			if (double.IsNaN(mScale))
				return Rhino.Geometry.Transform.Identity;
			return Rhino.Geometry.Transform.Scale(Point3d.Origin, mScale); 
		}
		protected virtual Transform vecsTransform()
		{
			Vector3d vx = Vector3d.XAxis, vy = Vector3d.YAxis;
			Transform tr = Rhino.Geometry.Transform.Identity;
			if (mAxis1 != null)
			{
				vx = Axis1.Vector3d;
				vx.Unitize();
				tr.M00 = vx.X;
				tr.M10 = vx.Y;
				tr.M20 = vx.Z;
			}
			if (mAxis2 != null)
			{
				vy = Axis2.Vector3d;
			}
			else
			{
				IfcCartesianTransformationOperator2D operator2D = this as IfcCartesianTransformationOperator2D;
				if(operator2D != null)
				{
					vy = Vector3d.CrossProduct(Vector3d.ZAxis, vx);
				}
			}
			vy.Unitize();
			tr.M01 = vy.X;
			tr.M11 = vy.Y;
			tr.M21 = vy.Z;
			Vector3d vz = Vector3d.CrossProduct(vx, vy);
			IfcCartesianTransformationOperator3D operator3d = this as IfcCartesianTransformationOperator3D;
			if(operator3d != null)
			{
				if (operator3d.Axis3 != null)
				{
					vz = operator3d.Axis3.Vector3d;
				}
			}
			vz.Unitize();
			tr.M02 = vz.X;
			tr.M12 = vz.Y;
			tr.M22 = vz.Z;

			return tr;
		}
	}
	public partial class IfcCartesianTransformationOperator2DnonUniform
	{
		internal override Transform getScaleTransform() 
		{
			double scaleX = double.IsNaN(Scale) ? 1 : Scale;
			double scaleY = double.IsNaN(Scale2) ? scaleX : Scale2;
			return Rhino.Geometry.Transform.Scale(Plane.WorldXY, scaleX, scaleY, 1); 
		}
	}
	public partial class IfcCartesianTransformationOperator3D
	{
		internal Vector3d Axis3Vector { get { return (mAxis3 != null ? Axis3.Vector3d : Vector3d.ZAxis); } }
	}
	public partial class IfcCartesianTransformationOperator3DnonUniform
	{
		internal override Transform getScaleTransform() 
		{
			double scaleX = Scale, scaleY = Scale2, scaleZ = Scale3;
			return Rhino.Geometry.Transform.Scale(Plane.WorldXY, scaleX, scaleY, scaleZ);
		}
	}
	public partial class IfcCompositeCurveSegment
	{
		//internal IfcCompositeCurveSegment(DatabaseIfc db, Curve c, bool sense, IfcTransitionCode tc, bool twoD, IfcCartesianPoint optStrt, out IfcCartesianPoint end)
		//	: this(tc, sense, IfcBoundedCurve.convCurve(db, c, optStrt, twoD, out end)) { }
	}
	public partial class IfcConnectionPointEccentricity
	{
		internal Vector3d Eccentricity { get { return new Vector3d(double.IsNaN(mEccentricityInX) ? 0 : mEccentricityInX, double.IsNaN(mEccentricityInY) ? 0 : mEccentricityInY, double.IsNaN(mEccentricityInZ) ? 0 : mEccentricityInZ); } }

		internal IfcConnectionPointEccentricity(IfcPointOrVertexPoint v, Vector3d ecc) : base(v) { mEccentricityInX = ecc.X; mEccentricityInY = ecc.Y; mEccentricityInZ = ecc.Z; }
	}
}
