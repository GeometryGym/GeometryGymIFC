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
	public partial class IfcValveType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcValveTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcValveTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcVector
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Orientation") == 0)
					Orientation = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
			}
			if (xml.HasAttribute("Magnitude"))
				mMagnitude = double.Parse(xml.Attributes["Magnitude"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Orientation.GetXML(xml.OwnerDocument, "Orientation", this, processed));
			xml.SetAttribute("Magnitude", mMagnitude.ToString());
		}
	}
}
