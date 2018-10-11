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
	public partial class IfcLibraryInformation : IfcExternalInformation
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			Name = extractString(xml, "Name");
			Version = extractString(xml, "Version");
			//versiondate
			Location = extractString(xml, "Location");
			Description = extractString(xml, "Description");
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Publisher") == 0)
					Publisher = mDatabase.ParseXml<IfcActorSelect>(child as XmlElement);
				else if (string.Compare(name, "LibraryRefForObjects") == 0)
				{
					//todo
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Name", Name);
			setAttribute(xml, "Version", Version);
			if (mPublisher > 0)
				xml.AppendChild(mDatabase[mPublisher].GetXML(xml.OwnerDocument, "Publisher", this, processed));
			//VersionDate
			setAttribute(xml, "Location", Location);
			setAttribute(xml, "Description", Description);
		}
	}
	public partial class IfcLibraryReference : IfcExternalReference, IfcLibrarySelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			Description = extractString(xml,"Description");
			Language = extractString(xml,"Language");
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ReferencedLibrary") == 0)
					ReferencedLibrary = mDatabase.ParseXml<IfcLibraryInformation>(child as XmlElement);
				else if (string.Compare(name, "LibraryRefForObjects") == 0)
				{
					//todo
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Description", Description);
			setAttribute(xml, "Language", Language);
			if (mReferencedLibrary > 0)
				xml.AppendChild(ReferencedLibrary.GetXML(xml.OwnerDocument, "ReferencedLibrary", this, processed));	
		}
	}
	public partial class IfcLine : IfcCurve
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Pnt") == 0)
					Pnt = mDatabase.ParseXml<IfcCartesianPoint>(child as XmlElement);
				else if (string.Compare(name, "Dir") == 0)
					Dir = mDatabase.ParseXml<IfcVector>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Pnt.GetXML(xml.OwnerDocument, "Pnt", this, processed));
			xml.AppendChild(Dir.GetXML(xml.OwnerDocument, "Dir", this, processed));
		}
	}
	public abstract partial class IfcLinearPositioningElement : IfcPositioningElement //IFC4.1
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Axis") == 0)
					Axis = mDatabase.ParseXml<IfcCurve>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Axis.GetXML(xml.OwnerDocument, "Axis", this, processed));
		}
	}
	public partial class IfcLocalPlacement : IfcObjectPlacement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "PlacementRelTo") == 0)
					PlacementRelTo = mDatabase.ParseXml<IfcObjectPlacement>(child as XmlElement);
				else if (string.Compare(name, "RelativePlacement") == 0)
					RelativePlacement = mDatabase.ParseXml<IfcAxis2Placement>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if(mPlacementRelTo != null)
				xml.AppendChild(mPlacementRelTo.GetXML(xml.OwnerDocument, "PlacementRelTo", this, processed));
			xml.AppendChild(mDatabase[mRelativePlacement.Index].GetXML(xml.OwnerDocument, "RelativePlacement", this, processed));
		}
	}
	public partial class IfcLShapeProfileDef : IfcParameterizedProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Depth"))
				mDepth = double.Parse(xml.Attributes["Depth"].Value);
			if (xml.HasAttribute("Width"))
				mWidth = double.Parse(xml.Attributes["Width"].Value);
			if (xml.HasAttribute("WebThickness"))
				mThickness = double.Parse(xml.Attributes["Thickness"].Value);
			if (xml.HasAttribute("FilletRadius"))
				mFilletRadius = double.Parse(xml.Attributes["FilletRadius"].Value);
			if (xml.HasAttribute("EdgeRadius"))
				mEdgeRadius = double.Parse(xml.Attributes["EdgeRadius"].Value);
			if (xml.HasAttribute("LegSlope"))
				mLegSlope = double.Parse(xml.Attributes["LegSlope"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Depth", mDepth.ToString());
			xml.SetAttribute("Width", mWidth.ToString());
			xml.SetAttribute("Thickness", mThickness.ToString());
			if (!double.IsNaN(mFilletRadius))
				xml.SetAttribute("FilletRadius", mFilletRadius.ToString());
			if (!double.IsNaN(mEdgeRadius))
				xml.SetAttribute("EdgeRadius", mEdgeRadius.ToString());
			if (!double.IsNaN(mLegSlope))
				xml.SetAttribute("LegSlope", mLegSlope.ToString());
		}
	}
}
