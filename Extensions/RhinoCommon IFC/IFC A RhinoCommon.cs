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
using System.Text;
using System.Reflection;
using System.IO; 

using Rhino.Geometry;

namespace GeometryGym.Ifc
{
	public partial class IfcAxis1Placement : IfcPlacement
	{
		internal Vector3d AxisVector { get { return (mAxis > 0 ? Axis.Vector3d : Vector3d.XAxis); } }

		public override Plane Plane
		{
			get
			{
				Point3d p = LocationPoint;
				Vector3d xaxis = AxisVector;
				Vector3d yAxis = Vector3d.CrossProduct(Vector3d.ZAxis, xaxis);
				return new Plane(p, xaxis, yAxis);
			}
		}
	}
	public partial interface IfcAxis2Placement : IBaseClassIfc //SELECT ( IfcAxis2Placement2D, IfcAxis2Placement3D);
	{
		Transform Transform { get; }
		Plane Plane { get; }
	}
	public partial class IfcAxis2Placement2D : IfcPlacement, IfcAxis2Placement
	{
		internal Vector3d DirectionVector { get { return (mRefDirection != null ? RefDirection.Vector3d : Vector3d.XAxis); } }

		internal IfcAxis2Placement2D(DatabaseIfc db, Point2d position, Vector2d dir) : base(db, position)
		{
			if (dir.Length > 0 && new Vector3d(dir.X, dir.Y, 0).IsParallelTo(Vector3d.XAxis, Math.PI / 1800) != 1)
				RefDirection = new IfcDirection(db, dir);
		}
		public override Plane Plane
		{
			get
			{
				Point3d o = LocationPoint;
				Vector3d xaxis = DirectionVector;
				Vector3d yAxis = Vector3d.CrossProduct(Vector3d.ZAxis, xaxis);
				return new Plane(o, xaxis, yAxis);
			}
		}
	}
	public partial class IfcAxis2Placement3D
	{
		internal Plane mPlane = Plane.Unset; 
		public IfcAxis2Placement3D(DatabaseIfc db, Plane plane) : base(db,plane.Origin)
		{
			double angTol = Math.PI / 1800;
			if (plane.ZAxis.IsParallelTo(Vector3d.ZAxis, angTol) != 1)
			{
				Axis = new IfcDirection(db, plane.ZAxis);
				RefDirection = new IfcDirection(db, plane.XAxis);
			}
			else if (plane.XAxis.IsParallelTo(Vector3d.XAxis, angTol) != 1)
			{
				RefDirection = new IfcDirection(db, plane.XAxis);
				Axis = db.Factory.ZAxis;
			}
		}

		public override Plane Plane
		{
			get
			{
				if (!mPlane.IsValid)
				{
					Point3d orig = LocationPoint;
					IfcDirection axis = Axis, refDirection = RefDirection;
					Vector3d norm = axis == null ? Vector3d.ZAxis : axis.Vector3d;
					Vector3d xaxis = refDirection == null ? Vector3d.XAxis : refDirection.Vector3d;
					Vector3d yAxis = Vector3d.CrossProduct(norm, xaxis);
					mPlane = new Plane(orig, xaxis, yAxis);
				}
				return mPlane;
			}
		}
	}
}
