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
using System.Linq;
using System.Xml;


namespace GeometryGym.Ifc
{
	public partial class IfcFace
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Bounds") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcFaceBound f = mDatabase.ParseXml<IfcFaceBound>(cn as XmlElement);
						if (f != null)
							Bounds.Add(f);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Bounds", mDatabase.mXmlNamespace);
			xml.AppendChild(element);
			foreach (IfcFaceBound face in Bounds)
				element.AppendChild(face.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcFaceBasedSurfaceModel
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "FbsmFaces") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcConnectedFaceSet f = mDatabase.ParseXml<IfcConnectedFaceSet>(cn as XmlElement);
						if (f != null)
							mFbsmFaces.Add(f);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("FbsmFaces", mDatabase.mXmlNamespace);
			xml.AppendChild(element);
			foreach (IfcConnectedFaceSet face in FbsmFaces)
				element.AppendChild(face.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcFaceBound
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Bound") == 0)
					Bound = mDatabase.ParseXml<IfcLoop>(child as XmlElement);
			}
			if (xml.HasAttribute("Orientation"))
				mOrientation = bool.Parse(xml.Attributes["Orientation"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Bound.GetXML(xml.OwnerDocument, "Bound", this, processed));
			xml.SetAttribute("Orientation", mOrientation.ToString().ToLower());
		}
	}
	public partial class IfcFacetedBrepWithVoids
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Voids") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcClosedShell s = mDatabase.ParseXml<IfcClosedShell>(cn as XmlElement);
						if (s != null)
							Voids.Add(s);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Voids", mDatabase.mXmlNamespace);
			xml.AppendChild(element);
			foreach (IfcClosedShell s in Voids)
				element.AppendChild(s.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcFixedReferenceSweptAreaSolid
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "FixedReference") == 0)
					FixedReference = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(FixedReference.GetXML(xml.OwnerDocument, "FixedReference", this, processed));
		}
	}
}
