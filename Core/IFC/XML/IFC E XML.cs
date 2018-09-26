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
	public partial class IfcEdge : IfcTopologicalRepresentationItem //SUPERTYPE OF(ONEOF(IfcEdgeCurve, IfcOrientedEdge, IfcSubedge))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "EdgeStart") == 0)
					EdgeStart = mDatabase.ParseXml<IfcVertex>(child as XmlElement);
				if (string.Compare(name, "EdgeEnd") == 0)
					EdgeEnd = mDatabase.ParseXml<IfcVertex>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(mDatabase[mEdgeStart].GetXML(xml.OwnerDocument, "EdgeStart", this, processed));
			xml.AppendChild(mDatabase[mEdgeEnd].GetXML(xml.OwnerDocument, "EdgeEnd", this, processed));
		}
	}
	public abstract partial class IfcElement : IfcProduct, IfcStructuralActivityAssignmentSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcBuildingElement,IfcCivilElement
	{ //,IfcDistributionElement,IfcElementAssembly,IfcElementComponent,IfcFeatureElement,IfcFurnishingElement,IfcGeographicElement,IfcTransportElement ,IfcVirtualElement,IfcElectricalElement SS,IfcEquipmentElement SS)) 

		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Tag"))
				Tag = xml.Attributes["Tag"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "HasOpenings",true) == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcRelVoidsElement voids = mDatabase.ParseXml<IfcRelVoidsElement>(node as XmlElement);
						if (voids != null)
							voids.RelatingBuildingElement = this;
					}
				}
				if (string.Compare(name, "Tag", true) == 0)
					Tag = child.InnerText;
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Tag", Tag);
			if(mHasOpenings.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasOpenings");
				foreach (IfcRelVoidsElement voids in mHasOpenings)
				{
					if (voids.mIndex != host.mIndex)
						element.AppendChild(voids.GetXML(xml.OwnerDocument, "", this, processed));
				}
				if (element.HasChildNodes)
					xml.AppendChild(element);
			}
			XmlElement referencedBy = xml.OwnerDocument.CreateElement("ReferencedInStructures");
			foreach(IfcRelReferencedInSpatialStructure rss in mReferencedInStructures)
			{
				if (rss != host)
					referencedBy.AppendChild(rss.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (referencedBy.HasChildNodes)
				xml.AppendChild(referencedBy);
		}
	}

	public partial class IfcElementQuantity : IfcQuantitySet
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("MethodOfMeasurement"))
				Name = xml.Attributes["MethodOfMeasurement"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Quantities") == 0)
					mDatabase.ParseXMLList<IfcPhysicalQuantity>(child).ForEach(x=>addQuantity(x));
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			base.setAttribute(xml, "MethodOfMeasurement", MethodOfMeasurement);
			setChild(xml, "Quantities", Quantities.Values, processed);
		}
	}
	public abstract partial class IfcElementarySurface : IfcSurface //	ABSTRACT SUPERTYPE OF(ONEOF(IfcCylindricalSurface, IfcPlane))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Position") == 0)
					Position = mDatabase.ParseXml<IfcAxis2Placement3D>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Position.GetXML(xml.OwnerDocument, "Position", this, processed));
		}
	}
	public abstract partial class IfcElementType : IfcTypeProduct //ABSTRACT SUPERTYPE OF(ONEOF(IfcBuildingElementType, IfcDistributionElementType, IfcElementAssemblyType, IfcElementComponentType, IfcFurnishingElementType, IfcGeographicElementType, IfcTransportElementType))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("ElementType"))
				ElementType = xml.Attributes["ElementType"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "ElementType", ElementType);
		}
	}
	public partial class IfcEllipse : IfcConic
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("SemiAxis1"))
				mSemiAxis1 = double.Parse(xml.Attributes["SemiAxis1"].Value);
			if (xml.HasAttribute("SemiAxis2"))
				mSemiAxis2 = double.Parse(xml.Attributes["SemiAxis2"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("SemiAxis1", mSemiAxis1.ToString());
			xml.SetAttribute("SemiAxis2", mSemiAxis2.ToString());
		}
	}
	public abstract partial class IfcExtendedProperties : IfcPropertyAbstraction, NamedObjectIfc //IFC4 ABSTRACT SUPERTYPE OF (ONEOF (IfcMaterialProperties,IfcProfileProperties))
	{
		//protected string mName = "$"; //: OPTIONAL IfcLabel;
		//private string mDescription = "$"; //: OPTIONAL IfcText;
		//internal Dictionary<string, IfcProperty> mProperties = new Dictionary<string, IfcProperty>();//: SET [1:?] OF IfcProperty 
		//private List<int> mPropertyIndices = new List<int>();
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Location"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Properties") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcProperty p = mDatabase.ParseXml<IfcProperty>(cn as XmlElement);
						if (p != null)
							mProperties[p.Name] = p;
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			XmlElement element = xml.OwnerDocument.CreateElement("Properties");
			xml.AppendChild(element);
			foreach (IfcProperty p in Properties.Values)
				element.AppendChild(p.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public abstract partial class IfcExternalReference : BaseClassIfc, IfcLightDistributionDataSourceSelect, IfcResourceObjectSelect//ABSTRACT SUPERTYPE OF (ONEOF (IfcClassificationReference ,IfcDocumentReference ,IfcExternallyDefinedHatchStyle
	{ //,IfcExternallyDefinedSurfaceStyle ,IfcExternallyDefinedSymbol ,IfcExternallyDefinedTextFont ,IfcLibraryReference)); 
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Location"))
				Location = xml.Attributes["Location"].Value;
			if (xml.HasAttribute("Identification"))
				Identification = xml.Attributes["Identification"].Value;
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "HasExternalReferences") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcExternalReferenceRelationship r = mDatabase.ParseXml<IfcExternalReferenceRelationship>(cn as XmlElement);
						if (r != null)
							mHasExternalReferences.Add(r);
					}
				}
				else if (string.Compare(name, "HasConstraintRelationships") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcResourceConstraintRelationship r = mDatabase.ParseXml<IfcResourceConstraintRelationship>(cn as XmlElement);
						if (r != null)
							r.addRelated(this);
					}
				}
				else if (string.Compare(name, "ExternalReferenceForResources") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcExternalReferenceRelationship r = mDatabase.ParseXml<IfcExternalReferenceRelationship>(cn as XmlElement);
						if (r != null)
							r.RelatedResourceObjects.Add(this);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Location", Location);
			setAttribute(xml, "Identification", Identification);
			setAttribute(xml, "Name", Name);
			XmlElement element = xml.OwnerDocument.CreateElement("HasExternalReferences");
			foreach (IfcExternalReferenceRelationship r in HasExternalReferences)
				element.AppendChild(r.GetXML(xml.OwnerDocument, "", this, processed));
			if(element.HasChildNodes)
				xml.AppendChild(element);
			element = xml.OwnerDocument.CreateElement("HasConstraintRelationships");
			foreach (IfcResourceConstraintRelationship r in HasConstraintRelationships)
			{
				if (host.mIndex != r.mIndex)
					element.AppendChild(r.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (element.HasChildNodes)
				xml.AppendChild(element);
			element = xml.OwnerDocument.CreateElement("ExternalReferenceForResources");
			foreach (IfcExternalReferenceRelationship r in ExternalReferenceForResources)
			{
				if (host.mIndex != r.mIndex)
					element.AppendChild(r.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (element.HasChildNodes)
				xml.AppendChild(element);
		}
	}
	public partial class IfcExternalReferenceRelationship : IfcResourceLevelRelationship //IFC4
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingReference") == 0)
					RelatingReference = mDatabase.ParseXml<IfcExternalReference>(child as XmlElement);
				else if (string.Compare(name, "RelatedResourceObjects") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcResourceObjectSelect o = mDatabase.ParseXml<IfcResourceObjectSelect>(cn as XmlElement);
						if (o != null)
							RelatedResourceObjects.Add(o);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(RelatingReference.GetXML(xml.OwnerDocument, "RelatingReference", this, processed));
		}
	}
	public partial class IfcExtrudedAreaSolid : IfcSweptAreaSolid // SUPERTYPE OF(IfcExtrudedAreaSolidTapered)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ExtrudedDirection") == 0)
					ExtrudedDirection = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
			}
			if (xml.HasAttribute("Depth"))
				mDepth = double.Parse(xml.Attributes["Depth"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(ExtrudedDirection.GetXML(xml.OwnerDocument, "ExtrudedDirection", this, processed));
			xml.SetAttribute("Depth", mDepth.ToString());
		}
	}
	public partial class IfcExtrudedAreaSolidTapered : IfcExtrudedAreaSolid
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "EndSweptArea") == 0)
					EndSweptArea = mDatabase.ParseXml<IfcProfileDef>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(EndSweptArea.GetXML(xml.OwnerDocument, "EndSweptArea", this, processed));
		}
	}
}
