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
	public partial class IfcRectangleHollowProfileDef : IfcRectangleProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("WallThickness"))
				mWallThickness = double.Parse(xml.Attributes["WallThickness"].Value);
			if (xml.HasAttribute("InnerFilletRadius"))
				mInnerFilletRadius = double.Parse(xml.Attributes["InnerFilletRadius"].Value);
			if (xml.HasAttribute("OuterFilletRadius"))
				mOuterFilletRadius = double.Parse(xml.Attributes["OuterFilletRadius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("WallThickness", mWallThickness.ToString());
			if (double.IsNaN(mInnerFilletRadius))
				xml.SetAttribute("InnerFilletRadius", mInnerFilletRadius.ToString());
			if (double.IsNaN(mOuterFilletRadius))
				xml.SetAttribute("OuterFilletRadius", mOuterFilletRadius.ToString());
		}
	}
	public partial class IfcRectangleProfileDef : IfcParameterizedProfileDef //	SUPERTYPE OF(ONEOF(IfcRectangleHollowProfileDef, IfcRoundedRectangleProfileDef))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("XDim"))
				mXDim = double.Parse(xml.Attributes["XDim"].Value);
			if (xml.HasAttribute("YDim"))
				mYDim = double.Parse(xml.Attributes["YDim"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("XDim", mXDim.ToString());
			xml.SetAttribute("YDim", mYDim.ToString());
		}
	}
	public partial class IfcRectangularPyramid : IfcCsgPrimitive3D
	{
		//internal double mXLength, mYLength, mHeight;// : IfcPositiveLengthMeasure;
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("XLength"))
				mXLength = double.Parse(xml.Attributes["XLength"].Value);
			if (xml.HasAttribute("YLength"))
				mYLength = double.Parse(xml.Attributes["YLength"].Value);
			if (xml.HasAttribute("Height"))
				mHeight = double.Parse(xml.Attributes["Height"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Height", mHeight.ToString());
			xml.SetAttribute("XLength", mXLength.ToString());
			xml.SetAttribute("YLength", mYLength.ToString());
		}
	}
	public partial class IfcRectangularTrimmedSurface : IfcBoundedSurface
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "BasisSurface") == 0)
					BasisSurface = mDatabase.ParseXml<IfcPlane>(child as XmlElement);
			}
			if (xml.HasAttribute("U1"))
				mU1 = double.Parse(xml.Attributes["U1"].Value);
			if (xml.HasAttribute("V1"))
				mV1 = double.Parse(xml.Attributes["V1"].Value);
			if (xml.HasAttribute("U2"))
				mU2 = double.Parse(xml.Attributes["U2"].Value);
			if (xml.HasAttribute("V2"))
				mV2 = double.Parse(xml.Attributes["V2"].Value);
			if (xml.HasAttribute("Usense"))
				mUsense = bool.Parse(xml.Attributes["Usense"].Value);
			if (xml.HasAttribute("Vsense"))
				mVsense = bool.Parse(xml.Attributes["Vsense"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(BasisSurface.GetXML(xml.OwnerDocument, "BasisSurface", this, processed));
			xml.SetAttribute("U1", mU1.ToString());
			xml.SetAttribute("V1", mV1.ToString());
			xml.SetAttribute("U2", mU2.ToString());
			xml.SetAttribute("V2", mV2.ToString());
			xml.SetAttribute("Usense", mUsense.ToString());
			xml.SetAttribute("Vsense", mVsense.ToString());
		}
	}
	public partial class IfcReference : BaseClassIfc, IfcMetricValueSelect, IfcAppliedValueSelect // IFC4
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("TypeIdentifier"))
				TypeIdentifier = xml.Attributes["TypeIdentifier"].Value;
			if (xml.HasAttribute("AttributeIdentifier"))
				AttributeIdentifier = xml.Attributes["AttributeIdentifier"].Value;
			if (xml.HasAttribute("InstanceName"))
				InstanceName = xml.Attributes["InstanceName"].Value;
			if (xml.HasAttribute("ListPositions"))
				mListPositions = xml.Attributes["ListPositions"].Value.Split(" ".ToCharArray()).ToList().ConvertAll(x => int.Parse(x));
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "InnerReference") == 0)
					InnerReference = mDatabase.ParseXml<IfcReference>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "TypeIdentifier", TypeIdentifier);
			setAttribute(xml, "AttributeIdentifier", AttributeIdentifier);
			setAttribute(xml, "InstanceName", InstanceName);
			if (mListPositions.Count > 0)
				xml.SetAttribute("ListPositions", String.Join(" ", mListPositions));
			if (mInnerReference > 0)
				xml.AppendChild(InnerReference.GetXML(xml.OwnerDocument, "InnerReference", this, processed));
		}
	}
	public partial class IfcReinforcementBarProperties : IfcPreDefinedProperties
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "TotalCrossSectionArea", TotalCrossSectionArea);
			setAttribute(xml, "SteelGrade", SteelGrade);
			if(mBarSurface != IfcReinforcingBarSurfaceEnum.NOTDEFINED)
				setAttribute(xml, "BarSurface", BarSurface.ToString().ToLower());
			setAttribute(xml, "EffectiveDepth", EffectiveDepth);
			setAttribute(xml, "NominalBarDiameter", NominalBarDiameter);
			setAttribute(xml, "BarCount", BarCount);
		}
	}
	public partial class IfcReinforcementDefinitionProperties : IfcPreDefinedPropertySet //IFC2x3 IfcPropertySetDefinition
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "DefinitionType", DefinitionType);
			XmlElement element = xml.OwnerDocument.CreateElement("ReinforcementSectionDefinitions");
			xml.AppendChild(element);
			foreach (IfcSectionReinforcementProperties prop in ReinforcementSectionDefinitions)
				element.AppendChild(prop.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcRelAggregates : IfcRelDecomposes
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatedObjects") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcObjectDefinition od = mDatabase.ParseXml<IfcObjectDefinition>(cn as XmlElement);
						if (od != null)
							addObject(od);
					}
				}
				else if (string.Compare(name, "RelatingObject") == 0)
					RelatingObject = mDatabase.ParseXml<IfcObjectDefinition>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("RelatedObjects");
			xml.AppendChild(element);
			foreach (IfcObjectDefinition od in RelatedObjects)
				element.AppendChild(od.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public abstract partial class IfcRelAssigns : IfcRelationship //	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelAssignsToActor, IfcRelAssignsToControl, IfcRelAssignsToGroup, IfcRelAssignsToProcess, IfcRelAssignsToProduct, IfcRelAssignsToResource))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatedObjects") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcObjectDefinition od = mDatabase.ParseXml<IfcObjectDefinition>(node as XmlElement);
						if (od != null)
							 RelatedObjects.Add(od);
					}
				}
			}
			if (xml.HasAttribute("RelatedObjectsType"))
			{
				if (!Enum.TryParse<IfcObjectTypeEnum>(xml.Attributes["GlobalId"].Value,true, out mRelatedObjectsType))
					mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (!mRelatedObjects.Contains(host))
			{
				XmlElement element = xml.OwnerDocument.CreateElement("RelatedObjects");
				xml.AppendChild(element);
				foreach (IfcObjectDefinition od in RelatedObjects)
					element.AppendChild(od.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public partial class IfcRelAssignsToActor : IfcRelAssigns
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingActor") == 0)
					RelatingActor = mDatabase.ParseXml<IfcActor>(child as XmlElement);
				if (string.Compare(name, "ActingRole") == 0)
					ActingRole = mDatabase.ParseXml<IfcActorRole>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (host != mRelatingActor)
				xml.AppendChild(RelatingActor.GetXML(xml.OwnerDocument, "RelatingActor", this, processed));
			if (mActingRole != null && host != mActingRole)
				xml.AppendChild(ActingRole.GetXML(xml.OwnerDocument, "ActingRole", this, processed));
		}
	}
	public partial class IfcRelAssignsToProduct : IfcRelAssigns
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingProduct") == 0)
					RelatingProduct = mDatabase.ParseXml<IfcProductSelect>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (host != mRelatingProduct)
				xml.AppendChild(mDatabase[mRelatingProduct.Index].GetXML(xml.OwnerDocument, "RelatingProduct", this, processed));
		}
	}

	public abstract partial class IfcRelAssociates : IfcRelationship   //ABSTRACT SUPERTYPE OF (ONEOF(IfcRelAssociatesApproval,IfcRelAssociatesclassification,IfcRelAssociatesConstraint,IfcRelAssociatesDocument,IfcRelAssociatesLibrary,IfcRelAssociatesMaterial))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatedObjects") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcDefinitionSelect o = mDatabase.ParseXml<IfcDefinitionSelect>(cn as XmlElement);
						if (o != null)
							RelatedObjects.Add(o);
					}
				}
			}
		}
	}
	public partial class IfcRelAssociatesClassification : IfcRelAssociates
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingClassification") == 0)
					RelatingClassification = mDatabase.ParseXml<IfcClassificationSelect>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild((RelatingClassification as BaseClassIfc) .GetXML(xml.OwnerDocument, "RelatingClassification", this, processed));
		}
	}
	public partial class IfcRelAssociatesConstraint : IfcRelAssociates
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Intent"))
				Intent = xml.Attributes["Intent"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingConstraint") == 0)
					RelatingConstraint = mDatabase.ParseXml<IfcConstraint>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Intent", Intent);
			xml.AppendChild(RelatingConstraint.GetXML(xml.OwnerDocument, "RelatingConstraint", this, processed));
		}
	}
	public partial class IfcRelAssociatesDocument
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingDocument") == 0)
					RelatingDocument = mDatabase.ParseXml<IfcDocumentSelect>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(mDatabase[mRelatingDocument].GetXML(xml.OwnerDocument, "RelatingDocument", this, processed));
		}
	}
	public partial class IfcRelAssociatesLibrary
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingLibrary") == 0)
				{
					if(child.HasChildNodes)
						RelatingLibrary = mDatabase.ParseXml<IfcLibrarySelect>(child.ChildNodes[0] as XmlElement);
					else
						RelatingLibrary = mDatabase.ParseXml<IfcLibrarySelect>(child as XmlElement);
				}
				if(string.Compare(name, "RelatedObjects")== 0)
				{
					foreach (XmlNode n in child.ChildNodes)
						RelatedObjects.Add(mDatabase.ParseXml<IfcDefinitionSelect>(n as XmlElement));
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if(host.mIndex != mRelatingLibrary)
				xml.AppendChild(mDatabase[mRelatingLibrary].GetXML(xml.OwnerDocument, "RelatingLibrary", this, processed));
		}
	}
	public partial class IfcRelAssociatesMaterial : IfcRelAssociates
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingMaterial") == 0)
					RelatingMaterial = mDatabase.ParseXml<IfcMaterialSelect>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(mDatabase[mRelatingMaterial].GetXML(xml.OwnerDocument, "RelatingMaterial", this, processed));
		}
	}
	public partial class IfcRelContainedInSpatialStructure : IfcRelConnects
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatedElements") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcProduct p = mDatabase.ParseXml<IfcProduct>(cn as XmlElement);
						if (p != null)
							RelatedElements.Add(p);
					}
				}
				else if (string.Compare(name, "RelatingStructure") == 0)
					RelatingStructure = mDatabase.ParseXml<IfcSpatialElement>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("RelatedElements");
			xml.AppendChild(element);
			foreach (IfcProduct product in RelatedElements)
				element.AppendChild(product.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcRelDeclares : IfcRelationship //IFC4
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatedDefinitions") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcDefinitionSelect d = mDatabase.ParseXml<IfcDefinitionSelect>(cn as XmlElement);
						if (d != null)
							RelatedDefinitions.Add(d);
					}
				}
				else if (string.Compare(name, "RelatingContext") == 0)
					RelatingContext = mDatabase.ParseXml<IfcContext>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("RelatedDefinitions");
			xml.AppendChild(element);
			foreach (IfcDefinitionSelect d in mRelatedDefinitions)
				element.AppendChild(mDatabase[d.Index].GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcRelDefinesByTemplate : IfcRelDefines
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatedPropertySets") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcPropertySetDefinition p = mDatabase.ParseXml<IfcPropertySetDefinition>(cn as XmlElement);
						if (p != null)
							AddRelated(p);
					}
				}
				else if (string.Compare(name, "RelatingTemplate") == 0)
					RelatingTemplate = mDatabase.ParseXml<IfcPropertySetTemplate>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(RelatingTemplate.GetXML(xml.OwnerDocument, "RelatingTemplate", this, processed));
		}
	}
	public partial class IfcRelDefinesByType : IfcRelDefines
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatedObjects") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcObject o = mDatabase.ParseXml<IfcObject>(cn as XmlElement);
						if (o != null)
							RelatedObjects.Add(o);
					}
				}
				else if (string.Compare(name, "RelatingType") == 0)
					RelatingType = mDatabase.ParseXml<IfcTypeObject>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(RelatingType.GetXML(xml.OwnerDocument, "RelatingType", this, processed));
		}
	}
	public partial class IfcRelDefinesByProperties : IfcRelDefines
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatedObjects") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcObjectDefinition od = mDatabase.ParseXml<IfcObjectDefinition>(cn as XmlElement);
						if (od != null)
							RelatedObjects.Add(od);
					}
				}
				else if (string.Compare(name, "RelatingPropertyDefinition") == 0)
					RelatingPropertyDefinition = mDatabase.ParseXml<IfcPropertySetDefinition>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(RelatingPropertyDefinition.GetXML(xml.OwnerDocument, "RelatingPropertyDefinition", this, processed));
		}
	}
	public partial class IfcRelFillsElement : IfcRelConnects
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingOpeningElement") == 0)
					RelatingOpeningElement = mDatabase.ParseXml<IfcOpeningElement>(child as XmlElement);
				else if (string.Compare(name, "RelatedBuildingElement") == 0)
					RelatedBuildingElement = mDatabase.ParseXml<IfcElement>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(RelatedBuildingElement.GetXML(xml.OwnerDocument, "RelatedBuildingElement", this, processed));
		}
	}
	public partial class IfcRelNests : IfcRelDecomposes
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatedObjects") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcObjectDefinition od = mDatabase.ParseXml<IfcObjectDefinition>(cn as XmlElement);
						if (od != null)
							addRelated(od);
					}
				}
				else if (string.Compare(name, "RelatingObject") == 0)
					RelatingObject = mDatabase.ParseXml<IfcObjectDefinition>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("RelatedObjects");
			xml.AppendChild(element);
			foreach (IfcObjectDefinition od in RelatedObjects)
				element.AppendChild(od.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcRelReferencedInSpatialStructure : IfcRelConnects
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingStructure") == 0)
					RelatingStructure = mDatabase.ParseXml<IfcSpatialElement>(child as XmlElement);
				else if (string.Compare(name, "RelatedElements") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcProduct p = mDatabase.ParseXml<IfcProduct>(cn as XmlElement);
						if (p != null)
							RelatedElements.Add(p);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(RelatingStructure.GetXML(xml.OwnerDocument, "RelatingStructure", this, processed));
		}
	}
	public partial class IfcRelServicesBuildings : IfcRelConnects
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingSystem") == 0)
					RelatingSystem = mDatabase.ParseXml<IfcSystem>(child as XmlElement);
				else if (string.Compare(name, "RelatedBuildings") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcSpatialElement s = mDatabase.ParseXml<IfcSpatialElement>(cn as XmlElement);
						if (s != null)
							addRelated(s);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(RelatingSystem.GetXML(xml.OwnerDocument, "RelatingSystem", this, processed));
		}
	}
	public partial class IfcRelVoidsElement : IfcRelDecomposes // Ifc2x3 IfcRelConnects
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingBuildingElement") == 0)
					RelatingBuildingElement = mDatabase.ParseXml<IfcElement>(child as XmlElement);
				else if (string.Compare(name, "RelatedOpeningElement") == 0)
					RelatedOpeningElement = mDatabase.ParseXml<IfcFeatureElementSubtraction>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(RelatedOpeningElement.GetXML(xml.OwnerDocument, "RelatedOpeningElement", this, processed));
		}
	}
	public partial class IfcRepresentation : BaseClassIfc, IfcLayeredItem // Abstract IFC4 ,SUPERTYPE OF (ONEOF(IfcShapeModel,IfcStyleModel));
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("RepresentationIdentifier"))
				mRepresentationIdentifier = xml.Attributes["RepresentationIdentifier"].Value;
			if (xml.HasAttribute("RepresentationType"))
				RepresentationType = xml.Attributes["RepresentationType"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ContextOfItems") == 0)
					ContextOfItems = mDatabase.ParseXml<IfcRepresentationContext>(child as XmlElement);
				else if (string.Compare(name, "Items") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcRepresentationItem ri = mDatabase.ParseXml<IfcRepresentationItem>(cn as XmlElement);
						if (ri != null)
							Items.Add(ri);
					}
				}
				else if (string.Compare(name, "LayerAssignments") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcPresentationLayerAssignment a = mDatabase.ParseXml<IfcPresentationLayerAssignment>(cn as XmlElement);
						if (a != null)
							a.AssignedItems.Add(this);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(ContextOfItems.GetXML(xml.OwnerDocument, "ContextOfItems", this, processed));
			setAttribute(xml, "RepresentationIdentifier", RepresentationIdentifier);
			setAttribute(xml, "RepresentationType", RepresentationType);
			XmlElement element = xml.OwnerDocument.CreateElement("Items");
			xml.AppendChild(element);
			foreach (IfcRepresentationItem item in Items)
				element.AppendChild(item.GetXML(xml.OwnerDocument, "", this, processed));

			if (mLayerAssignments.Count > 0)
			{
				element = xml.OwnerDocument.CreateElement("LayerAssignments");
				xml.AppendChild(element);
				foreach (IfcPresentationLayerAssignment pla in mLayerAssignments)
					element.AppendChild(pla.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public abstract partial class IfcRepresentationContext : BaseClassIfc
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("ContextIdentifier"))
				ContextIdentifier = xml.Attributes["ContextIdentifier"].Value;
			if (xml.HasAttribute("ContextType"))
				ContextType = xml.Attributes["ContextType"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "ContextIdentifier", ContextIdentifier);
			setAttribute(xml, "ContextType", ContextType);
		}
	}
	public abstract partial class IfcRepresentationItem : BaseClassIfc, IfcLayeredItem /*(IfcGeometricRepresentationItem,IfcMappedItem,IfcStyledItem,IfcTopologicalRepresentationItem));*/
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "LayerAssignments") == 0)
				{
					List<IfcPresentationLayerAssignment> repItems = new List<IfcPresentationLayerAssignment>(child.ChildNodes.Count);
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcPresentationLayerAssignment a = mDatabase.ParseXml<IfcPresentationLayerAssignment>(cn as XmlElement);
						if (a != null)
							a.AssignedItems.Add(this);
					}
				}
				else if (string.Compare(name, "StyledByItem") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcStyledItem styled = mDatabase.ParseXml<IfcStyledItem>(node as XmlElement);
						if (styled != null)
							styled.Item = this;
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mLayerAssignments.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("LayerAssignments");
				xml.AppendChild(element);
				foreach (IfcPresentationLayerAssignment pla in mLayerAssignments)
					element.AppendChild(pla.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (mStyledByItem != null)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("StyledByItem");
				xml.AppendChild(element);
				element.AppendChild(mStyledByItem.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public partial class IfcRepresentationMap : BaseClassIfc, IfcProductRepresentationSelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "MappingOrigin") == 0)
					MappingOrigin = mDatabase.ParseXml<IfcAxis2Placement>(child as XmlElement);
				else if (string.Compare(name, "MappedRepresentation") == 0)
					MappedRepresentation = mDatabase.ParseXml<IfcShapeModel>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(mDatabase[mMappingOrigin].GetXML(xml.OwnerDocument, "MappingOrigin", this, processed));
			xml.AppendChild(MappedRepresentation.GetXML(xml.OwnerDocument, "MappedRepresentation", this, processed));
			if(mHasShapeAspects.Count > 0)
			{


			}
		}
	}
	public partial class IfcResourceConstraintRelationship : IfcResourceLevelRelationship  // IfcPropertyConstraintRelationship; // DEPRECEATED IFC4 renamed
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RelatingConstraint") == 0)
					RelatingConstraint = mDatabase.ParseXml<IfcConstraint>(child as XmlElement);
				else if (string.Compare(name, "RelatedResourceObjects") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcResourceObjectSelect r = mDatabase.ParseXml<IfcResourceObjectSelect>(cn as XmlElement);
						if (r != null)
							addRelated(r);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mRelatingConstraint != host.mIndex)
				xml.AppendChild(RelatingConstraint.GetXML(xml.OwnerDocument, "RelatingConstraint", this, processed));
			XmlElement element = xml.OwnerDocument.CreateElement("RelatedResourceObjects");
			foreach (int r in mRelatedResourceObjects)
			{
				if (r != host.mIndex)
					element.AppendChild(mDatabase[r].GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (element.HasChildNodes)
				xml.AppendChild(element);
		}
	}
	public abstract partial class IfcResourceLevelRelationship : BaseClassIfc //IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcApprovalRelationship,
	{ // IfcCurrencyRelationship, IfcDocumentInformationRelationship, IfcExternalReferenceRelationship, IfcMaterialRelationship, IfcOrganizationRelationship, IfcPropertyDependencyRelationship, IfcResourceApprovalRelationship, IfcResourceConstraintRelationship));
	  //internal string mName = "$";// : OPTIONAL IfcLabel
	  //internal string mDescription = "$";// : OPTIONAL IfcText; 
	}
	public partial class IfcRevolvedAreaSolid : IfcSweptAreaSolid // SUPERTYPE OF(IfcRevolvedAreaSolidTapered)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Axis") == 0)
					Axis = mDatabase.ParseXml<IfcAxis1Placement>(child as XmlElement);
			}
			if (xml.HasAttribute("Angle"))
				mAngle = double.Parse(xml.Attributes["Angle"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Axis.GetXML(xml.OwnerDocument, "Axis", this, processed));
			xml.SetAttribute("Angle", mAngle.ToString());
		}
	}
	public partial class IfcRightCircularCone : IfcCsgPrimitive3D
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Height"))
				mHeight = double.Parse(xml.Attributes["Height"].Value);
			if (xml.HasAttribute("BottomRadius"))
				mBottomRadius = double.Parse(xml.Attributes["BottomRadius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Height", mHeight.ToString());
			xml.SetAttribute("BottomRadius", mBottomRadius.ToString());
		}
	}
	public partial class IfcRightCircularCylinder : IfcCsgPrimitive3D
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Height"))
				mHeight = double.Parse(xml.Attributes["Height"].Value);
			if (xml.HasAttribute("Radius"))
				mRadius = double.Parse(xml.Attributes["Radius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Height", mHeight.ToString());
			xml.SetAttribute("Radius", mRadius.ToString());
		}
	}
	public abstract partial class IfcRoot : BaseClassIfc//ABSTRACT SUPERTYPE OF (ONEOF (IfcObjectDefinition ,IfcPropertyDefinition ,IfcRelationship));
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("GlobalId"))
				GlobalId = xml.Attributes["GlobalId"].Value;
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "OwnerHistory") == 0)
					OwnerHistory = mDatabase.ParseXml<IfcOwnerHistory>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("GlobalId", GlobalId);
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			if (OwnerHistory != null)
				xml.AppendChild(OwnerHistory.GetXML(xml.OwnerDocument, "OwnerHistory", this, processed));
		}
	}
	public partial class IfcRoundedRectangleProfileDef : IfcRectangleProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("RoundingRadius"))
				mRoundingRadius = double.Parse(xml.Attributes["RoundingRadius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("RoundingRadius", mRoundingRadius.ToString());
		}
	}
}
