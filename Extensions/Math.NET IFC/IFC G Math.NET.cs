using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace GeometryGym.Ifc
{
	public partial class IfcGridPlacement : IfcObjectPlacement
	{
		public override CoordinateSystem CoordinateSystem { get { return new CoordinateSystem(CoordinateSystem.Origin, CoordinateSystem.XAxis.Normalize(), CoordinateSystem.YAxis.Normalize(), CoordinateSystem.ZAxis.Normalize()); } }
	}
}	
