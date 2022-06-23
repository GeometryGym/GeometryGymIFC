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
	public partial class IfcQuantityArea : IfcPhysicalSimpleQuantity
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("AreaValue"))
				AreaValue = double.Parse(xml.Attributes["AreaValue"].Value);
			if (xml.HasAttribute("Formula"))
				Formula = xml.Attributes["Formula"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("AreaValue", AreaValue.ToString());
			setAttribute(xml, "Formula", Formula);
		}
	}
	public partial class IfcQuantityCount : IfcPhysicalSimpleQuantity
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("CountValue"))
				CountValueDouble = double.Parse(xml.Attributes["CountValue"].Value);
			if (xml.HasAttribute("Formula"))
				Formula = xml.Attributes["Formula"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("CountValue", CountValue.ToString());
			setAttribute(xml, "Formula", Formula);
		}
	}
	public partial class IfcQuantityLength : IfcPhysicalSimpleQuantity
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("LengthValue"))
				LengthValue = double.Parse(xml.Attributes["LengthValue"].Value);
			if (xml.HasAttribute("Formula"))
				Formula = xml.Attributes["Formula"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("LengthValue", LengthValue.ToString());
			setAttribute(xml, "Formula", Formula);
		}
	}
	public partial class IfcQuantityTime : IfcPhysicalSimpleQuantity
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("LengthValue"))
				TimeValue = double.Parse(xml.Attributes["TimeValue"].Value);
			if (xml.HasAttribute("Formula"))
				Formula = xml.Attributes["Formula"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("TimeValue", TimeValue.ToString());
			setAttribute(xml, "Formula", Formula);
		}
	}
	public partial class IfcQuantityVolume : IfcPhysicalSimpleQuantity
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("VolumeValue"))
				VolumeValue = double.Parse(xml.Attributes["VolumeValue"].Value);
			if (xml.HasAttribute("Formula"))
				Formula = xml.Attributes["Formula"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("VolumeValue", VolumeValue.ToString());
			setAttribute(xml, "Formula", Formula);
		}
	}
	public partial class IfcQuantityWeight : IfcPhysicalSimpleQuantity
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("WeightValue"))
				WeightValue = double.Parse(xml.Attributes["WeightValue"].Value);
			if (xml.HasAttribute("Formula"))
				Formula = xml.Attributes["Formula"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("WeightValue", WeightValue.ToString());
			setAttribute(xml, "Formula", Formula);
		}
	}
}
