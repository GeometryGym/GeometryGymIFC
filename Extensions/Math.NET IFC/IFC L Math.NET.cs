using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace GeometryGym.Ifc
{
	public partial class IfcLocalPlacement : IfcObjectPlacement
	{
		private CoordinateSystem mCoordinateSystem = null;
		public override CoordinateSystem CoordinateSystem
		{
			get
			{
				if (!mCalculated)
				{
					mCalculated = true;
					CoordinateSystem coordinateSystem = new CoordinateSystem();
					Point3D o = new Point3D(0,0,0), x = new Point3D(1, 0, 0), y = new Point3D(0, 1, 0);
					IfcObjectPlacement pl = PlacementRelTo;
					CoordinateSystem RelativeTo = pl == null ? new CoordinateSystem() : pl.CoordinateSystem;
					o = RelativeTo.Transform(o);
					x = RelativeTo.Transform(x);
					y = RelativeTo.Transform(y);
					Vector3D vx = x - o, vy = y-o;
					Vector3D vz = vx.CrossProduct(vy);
					coordinateSystem = new CoordinateSystem(o, vx, vy, vz);

					IfcAxis2Placement apl = RelativePlacement;
					CoordinateSystem relativeCoordinateSystem = (apl == null ? new CoordinateSystem() : apl.CoordinateSystem);
					mCoordinateSystem = coordinateSystem.Transform(relativeCoordinateSystem);
				}
				return mCoordinateSystem;
			}
		}
	}

}
