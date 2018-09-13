using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GeometryGym.Ifc
{
	public abstract partial class IfcPreDefinedColour : IfcPreDefinedItem, IfcColour //	ABSTRACT SUPERTYPE OF(IfcDraughtingPreDefinedColour)
	{
		public Color Color() { return System.Drawing.Color.Empty; }
	}
	public partial class IfcPresentationLayerAssignment : BaseClassIfc //SUPERTYPE OF	(IfcPresentationLayerWithStyle);
	{
		internal virtual Color LayerColour { get { return Color.Empty; } }
	}
	public partial class IfcPresentationLayerWithStyle : IfcPresentationLayerAssignment
	{
		internal override Color LayerColour
		{
			get
			{
				ReadOnlyCollection<IfcPresentationStyle> styles = LayerStyles;
				foreach (IfcPresentationStyle ps in styles)
				{
					IfcSurfaceStyle ss = ps as IfcSurfaceStyle;
					if (ss != null)
					{
						List<IfcSurfaceStyleShading> sss = ss.Extract<IfcSurfaceStyleShading>();
						if (sss.Count > 0)
							return sss[0].SurfaceColour.Color();
					}
				}
				foreach (IfcPresentationStyle ps in styles)
				{
					IfcCurveStyle cs = ps as IfcCurveStyle;
					if (cs != null)
					{
						IfcColour col = cs.CurveColour;
						if (col != null)
							return col.Color();
					}
				}
				return base.LayerColour;
			}
		}
	}
}

