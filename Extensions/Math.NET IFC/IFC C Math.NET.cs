using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace GeometryGym.Ifc
{
	public partial class IfcCartesianPoint : IfcPoint
	{
		public override Point3D Location 
		{
			get { return new Point3D(mCoordinateX, mCoordinateY, Double.IsNaN(mCoordinateZ) ? 0 : mCoordinateZ); }
		}
		internal Point3D Coordinates3d { set { mCoordinateX = value.X; mCoordinateY = value.Y; mCoordinateZ = value.Z; } }
		internal Point2D Coordinates2d { set { mCoordinateX = value.X; mCoordinateY = value.Y; mCoordinateZ = double.NaN; } }
		internal IfcCartesianPoint(DatabaseIfc m, Point3D pt) : base(m) { Coordinates3d = pt; }
		internal IfcCartesianPoint(DatabaseIfc m, Point2D pt) : base(m) { Coordinates2d = pt; }
	}
}	
