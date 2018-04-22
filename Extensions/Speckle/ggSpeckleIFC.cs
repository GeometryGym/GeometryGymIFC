using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if (DEBUG)
using SpeckleCore;
using GeometryGym.Ifc;

namespace GeometryGym.Ifc
{
	public partial class BaseClassIfc : ISpeckleSerializable
	{
		SpeckleObject ISpeckleSerializable.ToSpeckle()
		{
			ContainerIFC container = new ContainerIFC(this);
			SpeckleObject result = Converter.ToAbstract(container);
			if (this is IfcRoot root)
				result.ApplicationId = root.GlobalId;
			return result;
		}
	}
}
#endif
