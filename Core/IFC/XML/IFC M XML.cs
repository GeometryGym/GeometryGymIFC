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
	public abstract partial class IfcManifoldSolidBrep : IfcSolidModel //ABSTRACT SUPERTYPE OF(ONEOF(IfcAdvancedBrep, IfcFacetedBrep))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Outer") == 0)
					Outer = mDatabase.ParseXml<IfcClosedShell>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Outer.GetXML(xml.OwnerDocument, "Outer", this, processed));
		}
	}
	public partial class IfcMapConversion : IfcCoordinateOperation //IFC4
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Eastings"))
				Eastings = double.Parse(xml.Attributes["Eastings"].Value);
			if (xml.HasAttribute("Northings"))
				Northings = double.Parse(xml.Attributes["Northings"].Value);
			if (xml.HasAttribute("OrthogonalHeight"))
				OrthogonalHeight = double.Parse(xml.Attributes["OrthogonalHeight"].Value);
			if (xml.HasAttribute("XAxisAbscissa"))
				XAxisAbscissa = double.Parse(xml.Attributes["XAxisAbscissa"].Value);
			if (xml.HasAttribute("XAxisOrdinate"))
				XAxisOrdinate = double.Parse(xml.Attributes["XAxisOrdinate"].Value);
			if (xml.HasAttribute("Scale"))
				Scale = double.Parse(xml.Attributes["Scale"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Eastings", mEastings.ToString());
			xml.SetAttribute("Northings", mNorthings.ToString());
			xml.SetAttribute("OrthogonalHeight", mOrthogonalHeight.ToString());
			setAttribute(xml, "XAxisAbscissa", mXAxisAbscissa);
			setAttribute(xml, "XAxisOrdinate", mXAxisOrdinate);
			setAttribute(xml, "Scale", mScale);
		}
	}
	public partial class IfcMaterial : IfcMaterialDefinition
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			if (xml.HasAttribute("Category"))
				Category = xml.Attributes["Category"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "HasRepresentation") == 0)
					HasRepresentation = mDatabase.ParseXml<IfcMaterialDefinitionRepresentation>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Name", Name);
			if (mDatabase.Release != ReleaseVersion.IFC2x3)
			{
				setAttribute(xml, "Description", Description);
				setAttribute(xml, "Category", Category);
			}
			if (mHasRepresentation != null)
				xml.AppendChild(mHasRepresentation.GetXML(xml.OwnerDocument, "HasRepresentation", this, processed));
		}
	}
	public partial class IfcMaterialDefinitionRepresentation : IfcProductRepresentation
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RepresentedMaterial") == 0)
					RepresentedMaterial = mDatabase.ParseXml<IfcMaterial>(child as XmlElement);
			}

		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			//xml.AppendChild(RepresentedMaterial.GetXML(xml.OwnerDocument, "RepresentedMaterial", this, processed));
		}

	}
	public partial class IfcMaterialLayer : IfcMaterialDefinition
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Material") == 0)
					Material = mDatabase.ParseXml<IfcMaterial>(child as XmlElement);
			}
			if (xml.HasAttribute("LayerThickness"))
				LayerThickness = double.Parse(xml.Attributes["LayerThickness"].Value);
			if (xml.HasAttribute("IsVentilated"))
				Enum.TryParse<IfcLogicalEnum>(xml.Attributes["IsVentilated"].Value, true, out mIsVentilated);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			if (xml.HasAttribute("Description"))
				Name = xml.Attributes["Description"].Value;
			if (xml.HasAttribute("Category"))
				Category = xml.Attributes["Category"].Value;
			if (xml.HasAttribute("Priority"))
				Priority = double.Parse(xml.Attributes["Priority"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			IfcMaterial material = Material;
			if (material != null)
				xml.AppendChild(material.GetXML(xml.OwnerDocument, "Material", this, processed));
			xml.SetAttribute("LayerThickness", mLayerThickness.ToString());
			xml.SetAttribute("IsVentilated", mIsVentilated.ToString().ToLower());
			if (mDatabase.Release != ReleaseVersion.IFC2x3)
			{
				setAttribute(xml, "Name", Name);
				setAttribute(xml, "Description", Description);
				setAttribute(xml, "Category", Category);
				if (!double.IsNaN(mPriority))
					xml.SetAttribute("Priority", mPriority.ToString());
			}
		}
	}
	public partial class IfcMaterialLayerSet : IfcMaterialDefinition
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "MaterialLayers") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcMaterialLayer l = mDatabase.ParseXml<IfcMaterialLayer>(node as XmlElement);
						if (l != null)
							addMaterialLayer(l);
					}
				}
			}
			if (xml.HasAttribute("LayerSetName"))
				LayerSetName = xml.Attributes["LayerSetName"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("MaterialLayers");
			foreach (IfcMaterialLayer l in MaterialLayers)
				element.AppendChild(l.GetXML(xml.OwnerDocument, "", this, processed));
			xml.AppendChild(element);
			setAttribute(xml, "LayerSetName", LayerSetName);
			setAttribute(xml, "Description", Description);
		}
	}
	public partial class IfcMaterialLayerSetUsage : IfcMaterialUsageDefinition
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ForLayerSet") == 0)
					ForLayerSet = mDatabase.ParseXml<IfcMaterialLayerSet>(child as XmlElement);
			}
			if (xml.HasAttribute("LayerSetDirection"))
				Enum.TryParse<IfcLayerSetDirectionEnum>(xml.Attributes["LayerSetDirection"].Value, true, out mLayerSetDirection);
			if (xml.HasAttribute("DirectionSense"))
				Enum.TryParse<IfcDirectionSenseEnum>(xml.Attributes["DirectionSense"].Value, true, out mDirectionSense);
			if (xml.HasAttribute("OffsetFromReferenceLine"))
				mOffsetFromReferenceLine = double.Parse(xml.Attributes["OffsetFromReferenceLine"].Value);
			if (xml.HasAttribute("ReferenceExtent"))
				mReferenceExtent = double.Parse(xml.Attributes["ReferenceExtent"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(ForLayerSet.GetXML(xml.OwnerDocument, "ForLayerSet", this, processed));
			xml.SetAttribute("LayerSetDirection", mLayerSetDirection.ToString().ToLower());
			xml.SetAttribute("DirectionSense", mDirectionSense.ToString().ToLower());
			xml.SetAttribute("OffsetFromReferenceLine", mOffsetFromReferenceLine.ToString());
			if (!double.IsNaN(mReferenceExtent) && mDatabase.Release != ReleaseVersion.IFC2x3)
				xml.SetAttribute("ReferenceExtent", mReferenceExtent.ToString());
		}
	}
	public partial class IfcMaterialProfile : IfcMaterialDefinition // IFC4
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Material") == 0)
					Material = mDatabase.ParseXml<IfcMaterial>(child as XmlElement);
				else if (string.Compare(name, "Profile") == 0)
					Profile = mDatabase.ParseXml<IfcProfileDef>(child as XmlElement);
			}
			if (xml.HasAttribute("Priority"))
				int.TryParse(xml.Attributes["Priority"].Value, out mPriority);
			if (xml.HasAttribute("Category"))
				Category = xml.Attributes["Category"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			if (mMaterial != null)
				xml.AppendChild(Material.GetXML(xml.OwnerDocument, "Material", this, processed));
			if (mProfile != null)
				xml.AppendChild(Profile.GetXML(xml.OwnerDocument, "Profile", this, processed));
			if (mPriority != int.MaxValue)
				setAttribute(xml, "Priority", mPriority.ToString());
			setAttribute(xml, "Category", Category);
		}
	}
	public partial class IfcMaterialProfileSet : IfcMaterialDefinition //IFC4
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			XmlElement element = xml.OwnerDocument.CreateElement("MaterialProfiles");
			xml.AppendChild(element);
			foreach (IfcMaterialProfile p in MaterialProfiles)
				element.AppendChild(p.GetXML(xml.OwnerDocument, "", this, processed));
			if (mCompositeProfile != null)
				xml.AppendChild(CompositeProfile.GetXML(xml.OwnerDocument, "CompositeProfile", this, processed));
		}
	}
	public partial class IfcMaterialProfileSetUsage : IfcMaterialUsageDefinition //IFC4
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(ForProfileSet.GetXML(xml.OwnerDocument, "ForProfileSet", this, processed));
			if (mCardinalPoint != IfcCardinalPointReference.DEFAULT)
				xml.SetAttribute("CardinalPoint", ((int)mCardinalPoint).ToString());
			if(!double.IsNaN(mReferenceExtent))
				setAttribute(xml, "ReferenceExtent", ReferenceExtent);
		}
	}
	public partial class IfcMeasureWithUnit : BaseClassIfc, IfcAppliedValueSelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ValueComponent") == 0)
					mValueComponent = extractValue(child.FirstChild);
				else if (string.Compare(name, "UnitComponent") == 0)
					UnitComponent = mDatabase.ParseXml<IfcUnit>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(convert(xml.OwnerDocument, mValueComponent, "ValueComponent"));
			xml.AppendChild(mDatabase[mUnitComponent].GetXML(xml.OwnerDocument, "UnitComponent", this, processed));
		}
	}
	public partial class IfcMetric : IfcConstraint
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("BenchMark"))
				Enum.TryParse<IfcBenchmarkEnum>(xml.Attributes["BenchMark"].Value, true, out mBenchMark);
			if (xml.HasAttribute("ValueSource"))
				ValueSource = xml.Attributes["ValueSource"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "DataValue") == 0)
				{
					if(child.HasChildNodes)
						mDataValueValue = extractValue(child.FirstChild);
					if (mDataValueValue == null)
					{
						BaseClassIfc baseClass = mDatabase.ParseXml<BaseClassIfc>(child as XmlElement);
						IfcMetricValueSelect metric = baseClass as IfcMetricValueSelect;
						if (metric != null)
							DataValue = metric;
						else
							mDataValueValue = extractValue(child as XmlNode);
					}
				}
				else if (string.Compare(name, "ReferencePath") == 0)
					ReferencePath = mDatabase.ParseXml<IfcReference>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("BenchMark", mBenchMark.ToString().ToLower());
			setAttribute(xml, "ValueSource", ValueSource);
			if (mDataValue > 0)
				xml.AppendChild(mDatabase[mDataValue].GetXML(xml.OwnerDocument, "DataValue", this, processed));
			else if(mDataValueValue != null)
				xml.AppendChild(convert(xml.OwnerDocument, mDataValueValue, "DataValue"));
			if(mReferencePath > 0)
				xml.AppendChild(ReferencePath.GetXML(xml.OwnerDocument, "ReferencePath", this, processed));
		}
	}
}
