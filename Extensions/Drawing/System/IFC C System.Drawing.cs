using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GeometryGym.Ifc
{
	public partial interface IfcColour : IBaseClassIfc { Color Colour { get; } }// = SELECT (IfcColourSpecification ,IfcPreDefinedColour); 
	public partial class IfcColourRgb : IfcColourSpecification, IfcColourOrFactor
	{
		public override Color Colour { get { return Color.FromArgb((int)(mRed * 255), (int)(mGreen * 255), (int)(mBlue * 255)); } }

		public IfcColourRgb(DatabaseIfc db, Color col) : base(db) { Name = col.Name; mRed = col.R / 255.0; mGreen = col.G / 255.0; mBlue = col.B / 255.0; }
	}
	public partial class IfcColourRgbList : IfcPresentationItem
	{
		public IfcColourRgbList(DatabaseIfc db, IEnumerable<Color> colourList) : base(db)
		{
			mColourList = new Tuple<double, double, double>[colourList.Count()];
			int ilast = colourList.Count();
			for (int icounter = 0; icounter < ilast; icounter++)
			{
				Color c = colourList.ElementAt(icounter);
				mColourList[icounter] = new Tuple<double, double, double>(c.R / 255.0, c.G / 255.0, c.B / 255.0);
			}
		}

		internal List<Color> ColorList
		{
			get
			{
				List<Color> result = new List<Color>();
				foreach (Tuple<double, double, double> c in mColourList)
					result.Add(Color.FromArgb((int)(c.Item1 * 255), (int)(c.Item2 * 255), (int)(c.Item3 * 255)));
				return result;
			}
		}
	}
	public abstract partial class IfcColourSpecification : IfcPresentationItem, IfcColour //	ABSTRACT SUPERTYPE OF(IfcColourRgb)
	{
		public abstract Color Colour { get; }
	}
}
