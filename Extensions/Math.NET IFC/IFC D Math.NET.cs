using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace GeometryGym.Ifc
{
	public partial class IfcDirection : IfcGeometricRepresentationItem
	{
		internal Vector3D Vector { get { return new Vector3D(mCoordinateX, mCoordinateY, double.IsNaN(mCoordinateZ) ? 0 : mCoordinateZ); } }
		public IfcDirection(DatabaseIfc db, Vector3D v) : base(db)
		{
			UnitVector3D unit = v.Normalize();

			mCoordinateX = unit.X;
			mCoordinateY = unit.Y;
			mCoordinateZ = unit.Z;
		}
		public IfcDirection(DatabaseIfc db, Vector2D v) : base(db)
		{
			double len = v.Length;
			mCoordinateX = v.X / len;
			mCoordinateY = v.Y / len;
			mCoordinateZ = double.NaN;
		}
	}
}	
