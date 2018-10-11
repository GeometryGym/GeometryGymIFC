using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace GeometryGym.Ifc
{
	public partial class IfcAxis1Placement : IfcPlacement
	{
		internal Vector3D AxisVector { get { return (mAxis > 0 ? Axis.Vector : new Vector3D(1,0,0)); } }
		public override CoordinateSystem CoordinateSystem
		{
			get
			{
				Point3D p = LocationPoint;
				Vector3D xAxis = AxisVector, zAxis = new Vector3D(0,0,1);
				Vector3D yAxis = zAxis.CrossProduct(xAxis);
				return new CoordinateSystem(p, xAxis, yAxis,zAxis);
			}
		}
	}
	public partial interface IfcAxis2Placement : IBaseClassIfc //SELECT ( IfcAxis2Placement2D, IfcAxis2Placement3D);
	{
		CoordinateSystem CoordinateSystem { get; }
	}
	public partial class IfcAxis2Placement2D : IfcPlacement, IfcAxis2Placement
	{
		internal Vector3D DirectionVector { get { return (mRefDirection != null ? RefDirection.Vector : new Vector3D(1,0,0)); } }

		internal IfcAxis2Placement2D(DatabaseIfc db, Point2D position, Vector2D dir) : base(db, position)
		{
			if (dir.Length > 0 && new Vector3D(dir.X, dir.Y, 0).IsParallelTo(new Vector3D(1,0,0), Math.PI / 1800))
				RefDirection = new IfcDirection(db, dir);
		}

		public override CoordinateSystem CoordinateSystem
		{
			get
			{
				Point3D o = LocationPoint;
				Vector3D xAxis = DirectionVector, zAxis = new Vector3D(0, 0, 1);
				Vector3D yAxis = zAxis.CrossProduct( xAxis);
				return new CoordinateSystem(o, xAxis, yAxis,zAxis);
			}
		}
	}
	public partial class IfcAxis2Placement3D : IfcPlacement, IfcAxis2Placement
	{
		public override CoordinateSystem CoordinateSystem
		{
			get
			{
				Point3D orig = LocationPoint;
				IfcDirection axis = Axis, refDirection = RefDirection;
				Vector3D norm = axis == null ? new Vector3D(0,0,1) : axis.Vector;
				Vector3D xAxis = refDirection == null ? new Vector3D(1,0,0) : refDirection.Vector;
				Vector3D yAxis = norm.CrossProduct(xAxis);
				return new CoordinateSystem(orig, xAxis, yAxis, norm);
			}
		}
	}
}
