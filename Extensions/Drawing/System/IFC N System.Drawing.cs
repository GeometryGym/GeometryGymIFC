using System;
using System.Drawing;

namespace GeometryGym.Ifc
{
	public partial class IfcNormalisedRatioMeasure : IfcMeasureValue, IfcColourOrFactor
	{
		public IfcNormalisedRatioMeasure(Color col) : this(System.Drawing.Color.FromArgb(0, col.R, col.G, col.B).ToArgb() / 16581375.0) { }
		public Color Color() { return System.Drawing.Color.FromArgb((int)(mValue * 16581375)); }	
	}
}	
