using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GeometryGym.Ifc
{
	public partial interface IfcColour : IBaseClassIfc { Color Color(); }// = SELECT (IfcColourSpecification ,IfcPreDefinedColour); 
	public partial class IfcColourRgb : IfcColourSpecification, IfcColourOrFactor
	{
		public override Color Color() { return System.Drawing.Color.FromArgb((int)(mRed * 255), (int)(mGreen * 255), (int)(mBlue * 255)); }

		public IfcColourRgb(DatabaseIfc db, Color col) : base(db) { Name = col.Name; mRed = col.R / 255.0; mGreen = col.G / 255.0; mBlue = col.B / 255.0; }
	}
	public partial class IfcColourRgbList : IfcPresentationItem
	{
		public IfcColourRgbList(DatabaseIfc db, IEnumerable<Color> colourList) : base(db)
		{
			mColourList.AddRange(colourList.Select(x => new Tuple<double, double, double>(x.R / 255.0, x.G / 255.0, x.B / 255.0)));
		}

		internal List<Color> ColorList { get { return mColourList.Select(x => Color.FromArgb((int)(x.Item1 * 255), (int)(x.Item2 * 255), (int)(x.Item3 * 255))).ToList(); } }
	}
	public abstract partial class IfcColourSpecification : IfcPresentationItem, IfcColour //	ABSTRACT SUPERTYPE OF(IfcColourRgb)
	{
		public abstract Color Color();
	}

}
