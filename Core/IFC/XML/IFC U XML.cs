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
	public partial class IfcUnitaryEquipment : IfcEnergyConversionDevice
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcUnitaryEquipmentTypeEnum>(xml.Attributes["PredefinedType"].Value,true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcUnitaryEquipmentTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcUnitaryEquipmentType : IfcEnergyConversionDeviceType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcUnitaryEquipmentTypeEnum>(xml.Attributes["PredefinedType"].Value,true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcUnitaryEquipmentTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}

	public partial class IfcUnitAssignment : BaseClassIfc
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Units") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcUnit u = mDatabase.ParseXml<IfcUnit>(cn as XmlElement);
						if (u != null)
							Units.Add(u);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Units");
			xml.AppendChild(element);
			foreach (IfcUnit unit in Units)
				element.AppendChild(mDatabase[unit.Index].GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcUShapeProfileDef : IfcParameterizedProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Depth"))
				mDepth = double.Parse(xml.Attributes["Depth"].Value);
			if (xml.HasAttribute("FlangeWidth"))
				mFlangeWidth = double.Parse(xml.Attributes["FlangeWidth"].Value);
			if (xml.HasAttribute("WebThickness"))
				mWebThickness = double.Parse(xml.Attributes["WebThickness"].Value);
			if (xml.HasAttribute("FlangeThickness"))
				mFlangeThickness = double.Parse(xml.Attributes["FlangeThickness"].Value);
			if (xml.HasAttribute("FilletRadius"))
				mFilletRadius = double.Parse(xml.Attributes["FilletRadius"].Value);
			if (xml.HasAttribute("EdgeRadius"))
				mEdgeRadius = double.Parse(xml.Attributes["EdgeRadius"].Value);
			if (xml.HasAttribute("FlangeSlope"))
				mFlangeSlope = double.Parse(xml.Attributes["FlangeSlope"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Depth", mDepth.ToString());
			xml.SetAttribute("FlangeWidth", mFlangeWidth.ToString());
			xml.SetAttribute("WebThickness", mWebThickness.ToString());
			xml.SetAttribute("FlangeThickness", mFlangeThickness.ToString());
			if (!double.IsNaN(mFilletRadius))
				xml.SetAttribute("FilletRadius", mFilletRadius.ToString());
			if (!double.IsNaN(mEdgeRadius))
				xml.SetAttribute("EdgeRadius", mEdgeRadius.ToString());
			if (!double.IsNaN(mFlangeSlope))
				xml.SetAttribute("FlangeSlope", mFlangeSlope.ToString());
			if (mDatabase.Release < ReleaseVersion.IFC4 && !double.IsNaN(mCentreOfGravityInX))
				xml.SetAttribute("CentreOfGravityInX", mCentreOfGravityInX.ToString());
		}
	}
}
