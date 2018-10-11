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
	public partial class IfcBeam : IfcBuildingElement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcBeamTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcBeamTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcBeamType : IfcBuildingElementType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcBeamTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcBeamTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcBlock : IfcCsgPrimitive3D
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("XLength"))
				XLength = double.Parse(xml.Attributes["XLength"].Value);
			if (xml.HasAttribute("YLength"))
				YLength = double.Parse(xml.Attributes["YLength"].Value);
			if (xml.HasAttribute("ZLength"))
				ZLength = double.Parse(xml.Attributes["ZLength"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("XLength", XLength.ToString());
			xml.SetAttribute("YLength", YLength.ToString());
			xml.SetAttribute("ZLength", ZLength.ToString());
		}
	}
	public partial class IfcBooleanResult : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Operator"))
				Enum.TryParse<IfcBooleanOperator>(xml.Attributes["Operator"].Value, true, out mOperator);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "FirstOperand") == 0)
					FirstOperand = mDatabase.ParseXml<IfcBooleanOperand>(child as XmlElement);
				else if (string.Compare(name, "SecondOperand") == 0)
					SecondOperand = mDatabase.ParseXml<IfcBooleanOperand>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Operator", mOperator.ToString().ToLower());
			xml.AppendChild(mDatabase[mFirstOperand].GetXML(xml.OwnerDocument, "FirstOperand", this, processed));
			xml.AppendChild(mDatabase[mSecondOperand].GetXML(xml.OwnerDocument, "SecondOperand", this, processed));
		}
	}
	public partial class IfcBoundingBox : IfcGeometricRepresentationItem
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Corner") == 0)
					Corner = mDatabase.ParseXml<IfcCartesianPoint>(child as XmlElement);
			}
			if (xml.HasAttribute("XDim"))
				XDim = double.Parse(xml.Attributes["XDim"].Value);
			if (xml.HasAttribute("YDim"))
				YDim = double.Parse(xml.Attributes["YDim"].Value);
			if (xml.HasAttribute("ZDim"))
				ZDim = double.Parse(xml.Attributes["ZDim"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Corner.GetXML(xml.OwnerDocument, "Corner", this, processed));
			xml.SetAttribute("XDim", mXDim.ToString());
			xml.SetAttribute("YDim", mYDim.ToString());
			xml.SetAttribute("ZDim", mZDim.ToString());
		}
	}
	public abstract partial class IfcBSplineSurface : IfcBoundedSurface
	{
		//internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		//{
		//	base.SetXML(xml, host, processed);
		//	xml.AppendChild(Corner.GetXML(xml.OwnerDocument, "Corner", this, processed));
		//	xml.SetAttribute("XDim", mXDim.ToString());
		//	xml.SetAttribute("YDim", mYDim.ToString());
		//	xml.SetAttribute("ZDim", mZDim.ToString());
		//}
	}
	public partial class IfcBuilding : IfcFacility
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "BuildingAddress") == 0)
					BuildingAddress = mDatabase.ParseXml<IfcPostalAddress>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mBuildingAddress != null) 
				xml.AppendChild(BuildingAddress.GetXML(xml.OwnerDocument, "BuildingAddress", this, processed));
		}
	}
	public partial class IfcBuildingElementProxy : IfcBuildingElement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcBuildingElementProxyTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
			else if (xml.HasAttribute("CompositionType"))
				Enum.TryParse<IfcBuildingElementProxyTypeEnum>(xml.Attributes["CompositionType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcBuildingElementProxyTypeEnum.NOTDEFINED)
			{
				if(mDatabase.Release < ReleaseVersion.IFC4)
				{
					if (mPredefinedType == IfcBuildingElementProxyTypeEnum.COMPLEX || mPredefinedType == IfcBuildingElementProxyTypeEnum.ELEMENT || mPredefinedType == IfcBuildingElementProxyTypeEnum.PARTIAL)
						xml.SetAttribute("CompositionType", mPredefinedType.ToString().ToLower());
				}
				else
					xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
			}
		}
	}
	public partial class IfcBuildingElementProxyType : IfcBuildingElementType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcBuildingElementProxyTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcBuildingElementProxyTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcBuildingStorey : IfcFacilityPart
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Elevation"))
				Elevation = double.Parse(xml.Attributes["Elevation"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (!double.IsNaN(mElevation))
				xml.SetAttribute("Elevation", mElevation.ToString());
		}
	}
}
