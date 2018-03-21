// MIT License
// Copyright (c) 2016 Geometry Gym Pty Ltd

// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
// and associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, 
// subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all copies or substantial 
// portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
// LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Xml;
//using System.Xml.Linq;


namespace GeometryGym.Ifc
{
	public partial class IfcIndexedPolyCurve : IfcBoundedCurve
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Points") == 0)
					Points = mDatabase.ParseXml<IfcCartesianPointList>(child as XmlElement);
				else if (string.Compare(name, "Segments") == 0)
				{
					foreach(XmlNode node in child.ChildNodes)
					{
						List<int> ints = node.InnerText.Split(" ".ToCharArray()).ToList().ConvertAll(x => int.Parse(x));
						if (string.Compare("IfcLineIndex-wrapper", node.Name) == 0)
							addSegment(new IfcLineIndex(ints));
						else
							addSegment(new IfcArcIndex(ints[0], ints[1], ints[2]));
					}
				}
			}
			if (xml.HasAttribute("SelfIntersect"))
				mSelfIntersect = bool.Parse(xml.Attributes["SelfIntersect"].Value) ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Points.GetXML(xml.OwnerDocument, "Points", this, processed));
			if (mSegments.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("Segments");
				xml.AppendChild(element);
				foreach (IfcSegmentIndexSelect seg in Segments)
				{
					XmlElement s = xml.OwnerDocument.CreateElement(seg.GetType().Name + "-wrapper");
					element.AppendChild(s);
					IfcArcIndex ai = seg as IfcArcIndex;
					if (ai != null)
						s.InnerText = ai.mA + " " + ai.mB + " " + ai.mC;
					else
					{
						IfcLineIndex li = seg as IfcLineIndex;
						s.InnerText = string.Join(" ", li.mIndices.ConvertAll(x => x.ToString()));
					}
				}
			}
		}
	}
	public partial class IfcIShapeProfileDef : IfcParameterizedProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("OverallWidth"))
				mOverallWidth = double.Parse(xml.Attributes["OverallWidth"].Value);
			if (xml.HasAttribute("OverallDepth"))
				mOverallDepth = double.Parse(xml.Attributes["OverallDepth"].Value);
			if (xml.HasAttribute("WebThickness"))
				mWebThickness = double.Parse(xml.Attributes["WebThickness"].Value);
			if (xml.HasAttribute("FlangeThickness"))
				mFlangeThickness = double.Parse(xml.Attributes["FlangeThickness"].Value);
			if (xml.HasAttribute("FilletRadius"))
				mFilletRadius = double.Parse(xml.Attributes["FilletRadius"].Value);
			if (xml.HasAttribute("FlangeEdgeRadius"))
				mFlangeEdgeRadius = double.Parse(xml.Attributes["FlangeEdgeRadius"].Value);
			if (xml.HasAttribute("FlangeSlope"))
				mFlangeSlope = double.Parse(xml.Attributes["FlangeSlope"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("OverallWidth", mOverallWidth.ToString());
			xml.SetAttribute("OverallDepth", mOverallDepth.ToString());
			xml.SetAttribute("WebThickness", mWebThickness.ToString());
			xml.SetAttribute("FlangeThickness", mFlangeThickness.ToString());
			if (!double.IsNaN(mFilletRadius) && mFilletRadius > 0)
				xml.SetAttribute("FilletRadius", mFilletRadius.ToString());
			if (!double.IsNaN(mFlangeEdgeRadius) && mFlangeEdgeRadius > 0)
				xml.SetAttribute("FlangeEdgeRadius", mFlangeEdgeRadius.ToString());
			if (!double.IsNaN(mFlangeSlope) && mFlangeSlope > 0)
				xml.SetAttribute("FlangeSlope", mFlangeSlope.ToString());
		}
	}
}
