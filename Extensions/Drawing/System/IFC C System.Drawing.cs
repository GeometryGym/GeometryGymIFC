using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GeometryGym.Ifc
{
	public partial interface IfcColour 
	{
		Color Color();
	}
	public partial class IfcColourRgb
	{
		private static List<Color> mNamedColors = new List<Color>();
		private static List<Color> NamedColors()
		{
			if (mNamedColors.Count == 0)
			{
				var colorProperties = typeof(Color).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where(p => p.PropertyType == typeof(Color));
				foreach (var colorProperty in colorProperties)
				{
					var colorValue = (Color)colorProperty.GetValue(null, null);
					if (colorValue.IsNamedColor)
						mNamedColors.Add(colorValue);
				}
			}
			return mNamedColors;
		}

		public override Color Color()
		{
			try
			{
				return System.Drawing.Color.FromArgb((int)(mRed * 255), (int)(mGreen * 255), (int)(mBlue * 255));
			}
			catch { }
			return System.Drawing.Color.Empty;
		}

		public IfcColourRgb(DatabaseIfc db, Color col)
			: base(db)
		{
			bool namedColor = col.IsNamedColor;
			if (!namedColor && (string.IsNullOrEmpty(col.Name) || int.TryParse(col.Name, out int i)))
			{
				var namedColors = NamedColors();
				foreach(var color in namedColors)
				{
					if(color.R == col.R && color.G == col.G && color.B == col.B)
					{
						Name = color.Name;
						break;
					}
				}
			}
			else
				Name = col.Name; 
			mRed = col.R / 255.0; 
			mGreen = col.G / 255.0; 
			mBlue = col.B / 255.0; 
		}
	}
	public partial class IfcColourRgbList 
	{
		public IfcColourRgbList(DatabaseIfc db, IEnumerable<Color> colourList) : base(db)
		{
			mColourList.AddRange(colourList.Select(x => new Tuple<double, double, double>(x.R / 255.0, x.G / 255.0, x.B / 255.0)));
		}

		internal List<Color> ColorList()
		{ 
			return mColourList.ConvertAll(x => Color.FromArgb((int)(x.Item1 * 255), (int)(x.Item2 * 255), (int)(x.Item3 * 255)));
		}
	}
	public abstract partial class IfcColourSpecification
	{
		public abstract Color Color();
	}

}
