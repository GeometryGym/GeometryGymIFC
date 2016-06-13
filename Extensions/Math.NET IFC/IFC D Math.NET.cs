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
		internal Vector3D Vector { get { return new Vector3D(mDirectionRatioX , mDirectionRatioY, double.IsNaN(mDirectionRatioZ) ? 0 : mDirectionRatioZ); } }
		public IfcDirection(DatabaseIfc db, Vector3D v) : base(db)
		{
			UnitVector3D unit = v.Normalize();

			mDirectionRatioX = unit.X;
			mDirectionRatioY = unit.Y;
			mDirectionRatioZ = unit.Z;
		}
		public IfcDirection(DatabaseIfc db, Vector2D v) : base(db)
		{
			double len = v.Length;
			mDirectionRatioX = v.X / len;
			mDirectionRatioY = v.Y / len;
			mDirectionRatioZ = double.NaN;
		}
	}
}	
