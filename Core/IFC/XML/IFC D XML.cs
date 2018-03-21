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
	public partial class IfcDerivedProfileDef : IfcProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ContainerProfile") == 0)
					ContainerProfile = mDatabase.ParseXml<IfcProfileDef>(child as XmlElement);
				else if (string.Compare(name, "Operator") == 0)
					Operator = mDatabase.ParseXml<IfcCartesianTransformationOperator2D>(child as XmlElement);
			}
			if (xml.HasAttribute("Label"))
				Label = xml.Attributes["Label"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(ContainerProfile.GetXML(xml.OwnerDocument, "ContainerProfile", this, processed));
			xml.AppendChild(Operator.GetXML(xml.OwnerDocument, "Operator", this, processed));
			setAttribute(xml, "Label", Label);
		}
	}
	public partial class IfcDerivedUnit : BaseClassIfc, IfcUnit
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("UnitType"))
				Enum.TryParse<IfcDerivedUnitEnum>(xml.Attributes["UnitType"].Value, true, out mUnitType);
			if (xml.HasAttribute("UserDefinedType"))
				UserDefinedType = xml.Attributes["UserDefinedType"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Elements") == 0)
				{
					List<IfcDerivedUnitElement> elements = new List<IfcDerivedUnitElement>();
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcDerivedUnitElement element = mDatabase.ParseXml<IfcDerivedUnitElement>(node as XmlElement);
						if (element != null)
							elements.Add(element);
					}
					Elements = elements;
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Elements");
			xml.AppendChild(element);
			foreach (IfcDerivedUnitElement unit in Elements)
				element.AppendChild(unit.GetXML(xml.OwnerDocument, "", this, processed));
			xml.SetAttribute("UnitType", mUnitType.ToString().ToLower());
			setAttribute(xml, "UserDefinedType", UserDefinedType);
		}
	}
	public partial class IfcDerivedUnitElement : BaseClassIfc
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Unit") == 0)
					Unit = mDatabase.ParseXml<IfcNamedUnit>(child as XmlElement);
			}
			if (xml.HasAttribute("Exponent"))
				mExponent = int.Parse(xml.Attributes["Exponent"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Unit.GetXML(xml.OwnerDocument, "Unit", this, processed));
			xml.SetAttribute("Exponent", mExponent.ToString());
		}
	}
	public partial class IfcDirection : IfcGeometricRepresentationItem
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("DirectionRatios"))
			{
				string[] ratios = xml.Attributes["DirectionRatios"].Value.Split(" ".ToArray());
				if (ratios.Length > 0)
				{
					mDirectionRatioX = double.Parse(ratios[0]);
					if (ratios.Length > 1)
					{
						mDirectionRatioY = double.Parse(ratios[1]);
						if (ratios.Length > 2 && !string.IsNullOrEmpty(ratios[2]))
							mDirectionRatioZ = double.Parse(ratios[2]);
						else
							mDirectionRatioZ = double.NaN;
					}
					else
					{
						mDirectionRatioY = double.NaN;
						mDirectionRatioZ = double.NaN;
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("DirectionRatios", RoundRatio(mDirectionRatioX) + " " + RoundRatio(mDirectionRatioY) + (double.IsNaN(mDirectionRatioZ) ? "" : " " + RoundRatio(mDirectionRatioZ)));
		}
	}
	public partial class IfcDistributionPort : IfcPort
	{
		//internal IfcFlowDirectionEnum mFlowDirection = IfcFlowDirectionEnum.NOTDEFINED; //:	OPTIONAL IfcFlowDirectionEnum;
		//internal IfcDistributionPortTypeEnum mPredefinedType = IfcDistributionPortTypeEnum.NOTDEFINED; // IFC4 : OPTIONAL IfcDistributionPortTypeEnum;
		//internal IfcDistributionSystemEnum mSystemType = IfcDistributionSystemEnum.NOTDEFINED;// IFC4 : OPTIONAL IfcDistributionSystemEnum;
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("FlowDirection"))
				Enum.TryParse<IfcFlowDirectionEnum>(xml.Attributes["FlowDirection"].Value, true, out mFlowDirection);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcDistributionPortTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
			if (xml.HasAttribute("SystemType"))
				Enum.TryParse<IfcDistributionSystemEnum>(xml.Attributes["SystemType"].Value, true, out mSystemType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mFlowDirection != IfcFlowDirectionEnum.NOTDEFINED)
				xml.SetAttribute("FlowDirection", mFlowDirection.ToString().ToLower());
			if (mPredefinedType != IfcDistributionPortTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
			if (mSystemType != IfcDistributionSystemEnum.NOTDEFINED)
				xml.SetAttribute("SystemType", mSystemType.ToString().ToLower());
		}
	}
	public partial class IfcDocumentReference : IfcExternalReference, IfcDocumentSelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if(xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ReferencedDocument") == 0)
					ReferencedDocument = mDatabase.ParseXml<IfcDocumentInformation>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Description", Description);
			if (mReferencedDocument > 0)
				xml.AppendChild(ReferencedDocument.GetXML(xml.OwnerDocument, "ReferencedDocument", this, processed));
		}
	}
	public partial class IfcDoor : IfcBuildingElement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcDoorTypeEnum>(xml.Attributes["PredefinedType"].Value,true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcDoorTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcDoorType : IfcBuildingElementType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcDoorTypeEnum>(xml.Attributes["PredefinedType"].Value,true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcDoorTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
}
