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
	public partial class IfcGeometricRepresentationContext : IfcRepresentationContext, IfcCoordinateReferenceSystemSelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("CoordinateSpaceDimension"))
				CoordinateSpaceDimension = int.Parse(xml.Attributes["CoordinateSpaceDimension"].Value);
			if (xml.HasAttribute("Precision"))
				Precision = double.Parse(xml.Attributes["Precision"].Value);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "WorldCoordinateSystem") == 0)
					WorldCoordinateSystem = mDatabase.ParseXml<IfcAxis2Placement3D>(child as XmlElement);
				else if (string.Compare(name, "TrueNorth") == 0)
					TrueNorth = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
				else if (string.Compare(name, "HasSubContexts") == 0)
				{
					List<IfcGeometricRepresentationSubContext> subs = new List<IfcGeometricRepresentationSubContext>();
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcGeometricRepresentationSubContext sub = mDatabase.ParseXml<IfcGeometricRepresentationSubContext>(node as XmlElement);
						if (sub != null)
							sub.ContainerContext = this;
					}
				}
				else if (string.Compare(name, "HasCoordinateOperation") == 0)
					HasCoordinateOperation = mDatabase.ParseXml<IfcCoordinateOperation>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (this as IfcGeometricRepresentationSubContext == null)
			{
				xml.SetAttribute("CoordinateSpaceDimension", mCoordinateSpaceDimension.ToString());
				if (!double.IsNaN(mPrecision))
					xml.SetAttribute("Precision", mPrecision.ToString());
				if (mWorldCoordinateSystem > 0)
					xml.AppendChild(mDatabase[mWorldCoordinateSystem].GetXML(xml.OwnerDocument, "WorldCoordinateSystem", this, processed));
				if (mTrueNorth > 0)
					xml.AppendChild(TrueNorth.GetXML(xml.OwnerDocument, "TrueNorth", this, processed));
			}
			if (mHasSubContexts.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasSubContexts");
				foreach (IfcGeometricRepresentationSubContext sub in mHasSubContexts)
					element.AppendChild(sub.GetXML(xml.OwnerDocument, "", this, processed));
				xml.AppendChild(element);
			}

			if (mHasCoordinateOperation != null)
				xml.AppendChild(HasCoordinateOperation.GetXML(xml.OwnerDocument, "HasCoordinateOperation", this, processed));
		}
	}
	public partial class IfcGeometricRepresentationSubContext : IfcGeometricRepresentationContext
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ContainerContext") == 0)
					ContainerContext = mDatabase.ParseXml<IfcGeometricRepresentationContext>(child as XmlElement);
			}
			if (xml.HasAttribute("TargetScale"))
				TargetScale = double.Parse(xml.Attributes["TargetScale"].Value);
			if (xml.HasAttribute("TargetView"))
				Enum.TryParse<IfcGeometricProjectionEnum>(xml.Attributes["TargetView"].Value, true, out mTargetView);
			if (xml.HasAttribute("UserDefinedTargetView"))
				UserDefinedTargetView = xml.Attributes["UserDefinedTargetView"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (!double.IsNaN(mTargetScale))
				xml.SetAttribute("TargetScale", mTargetScale.ToString());
			xml.SetAttribute("TargetView", mTargetView.ToString().ToLower());
			setAttribute(xml, "UserDefinedTargetView", UserDefinedTargetView);
		}
	}
	public partial class IfcGeometricSet : IfcGeometricRepresentationItem //SUPERTYPE OF(IfcGeometricCurveSet)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Elements") == 0)
				{
					List<IfcGeometricSetSelect> elements = new List<IfcGeometricSetSelect>(child.ChildNodes.Count);
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcGeometricSetSelect e = mDatabase.ParseXml<IfcGeometricSetSelect>(cn as XmlElement);
						if (e != null)
							elements.Add(e);
					}
					Elements = elements;
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Elements");
			xml.AppendChild(element);
			foreach (int i in mElements)
				element.AppendChild(mDatabase[i].GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
}
