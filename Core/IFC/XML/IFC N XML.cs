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
	public abstract partial class IfcNamedUnit
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Dimensions", true) == 0)
					Dimensions = mDatabase.ParseXml<IfcDimensionalExponents>(child as XmlElement);
				else if (string.Compare(name, "UnitType", true) == 0)
					Enum.TryParse<IfcUnitEnum>(child.InnerText, true, out mUnitType);
			}
			if (xml.HasAttribute("UnitType"))
				Enum.TryParse<IfcUnitEnum>(xml.Attributes["UnitType"].Value, true, out mUnitType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mDimensions != null)
				xml.AppendChild(Dimensions.GetXML(xml.OwnerDocument, "Dimensions", this, processed));
			xml.SetAttribute("UnitType", mUnitType.ToString().ToLower());
		}
	}
	public partial class IfcNavigationElement
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcNavigationElementTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			XmlAttribute predefinedType = xml.Attributes["PredefinedType"];
			if (predefinedType != null)
				Enum.TryParse<IfcNavigationElementTypeEnum>(predefinedType.Value, out mPredefinedType);
		}
	}
	public partial class IfcNavigationElementType
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			XmlAttribute predefinedType = xml.Attributes["PredefinedType"];
			if (predefinedType != null)
				Enum.TryParse<IfcNavigationElementTypeEnum>(predefinedType.Value, out mPredefinedType);
		}
	}
}
