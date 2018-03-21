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
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (this as IfcGeometricRepresentationSubContext == null)
			{
				xml.SetAttribute("CoordinateSpaceDimension", mCoordinateSpaceDimension.ToString());
				if (!double.IsNaN(mPrecision))
					xml.SetAttribute("Precision", mPrecision.ToString());
				if (mWorldCoordinateSystem != null)
					xml.AppendChild(mDatabase[mWorldCoordinateSystem.Index].GetXML(xml.OwnerDocument, "WorldCoordinateSystem", this, processed));
				if (mTrueNorth != null)
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
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
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
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcGeometricSetSelect e = mDatabase.ParseXml<IfcGeometricSetSelect>(cn as XmlElement);
						if (e != null)
							mElements.Add(e);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Elements");
			xml.AppendChild(element);
			foreach (IfcGeometricSetSelect el in mElements)
				element.AppendChild(mDatabase[el.Index].GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcGrid : IfcProduct
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "UAxes") == 0)
					extractAxes(child).ForEach(x=>UAxes.Add(x));
				else if (string.Compare(name, "VAxes") == 0)
					extractAxes(child).ForEach(x=>VAxes.Add(x));
				else if (string.Compare(name, "WAxes") == 0)
					extractAxes(child).ForEach(x=>WAxes.Add(x));
			}
		}
		internal List<IfcGridAxis> extractAxes(XmlNode node)
		{
			List<IfcGridAxis> axes = new List<IfcGridAxis>(node.ChildNodes.Count);
			foreach (XmlNode cn in node.ChildNodes)
			{
				IfcGridAxis a = mDatabase.ParseXml<IfcGridAxis>(cn as XmlElement);
				if (a != null)
					axes.Add(a);
			}
			return axes;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			XmlElement uAxes = null, vAxes = null, wAxes = null;
			if (mUAxes.Count > 0)
			{
				uAxes = xml.OwnerDocument.CreateElement("UAxes");
				foreach (IfcGridAxis a in UAxes)
					uAxes.AppendChild(a.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (mVAxes.Count > 0)
			{
				vAxes = xml.OwnerDocument.CreateElement("VAxes");
				foreach (IfcGridAxis a in VAxes)
					vAxes.AppendChild(a.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (mWAxes.Count > 0)
			{
				wAxes = xml.OwnerDocument.CreateElement("WAxes");
				foreach (IfcGridAxis a in WAxes)
					wAxes.AppendChild(a.GetXML(xml.OwnerDocument, "", this, processed));
			}
			base.SetXML(xml, host, processed);
			if(uAxes != null)
				xml.AppendChild(uAxes);
			if(vAxes != null)
				xml.AppendChild(vAxes);
			if(wAxes != null)
				xml.AppendChild(wAxes);
		}
	}
	public partial class IfcGridAxis : BaseClassIfc
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("AxisTag"))
				AxisTag = xml.Attributes["AxisTag"].Value;
			if (xml.HasAttribute("SameSense"))
				bool.TryParse(xml.Attributes["SameSense"].Value, out mSameSense);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "AxisCurve") == 0)
					AxisCurve = mDatabase.ParseXml<IfcCurve>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if(mAxisTag != "$")
				xml.SetAttribute("AxisTag", mAxisTag);
			xml.SetAttribute("SameSense", mSameSense.ToString().ToLower());
			xml.AppendChild(AxisCurve.GetXML(xml.OwnerDocument, "AxisCurve", this, processed));
		}
	}
	public partial class IfcGroup : IfcObject //SUPERTYPE OF (ONEOF (IfcAsset ,IfcCondition ,IfcInventory ,IfcStructuralLoadGroup ,IfcStructuralResultGroup ,IfcSystem ,IfcZone))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "IsGroupedBy") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcRelAssignsToGroup r = mDatabase.ParseXml<IfcRelAssignsToGroup>(cn as XmlElement);
						if (r != null)
							r.RelatingGroup = this;
					}
				}
				
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mIsGroupedBy.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("IsGroupedBy");
				xml.AppendChild(element);
				foreach (IfcRelAssignsToGroup rag in mIsGroupedBy)
					element.AppendChild(rag.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
}
