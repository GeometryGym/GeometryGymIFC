using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace GeometryGym.Ifc
{
	public abstract partial class IfcObjectPlacement : BaseClassIfc  //	 ABSTRACT SUPERTYPE OF (ONEOF (IfcGridPlacement ,IfcLocalPlacement));
	{
		public abstract CoordinateSystem CoordinateSystem { get; }
	}
}	
