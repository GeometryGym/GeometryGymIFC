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
	public partial class IfcSectionedSpine : IfcGeometricRepresentationItem
	{
		//internal int mSpineCurve;// : IfcCompositeCurve;
		//internal List<int> mCrossSections = new List<int>();// : LIST [2:?] OF IfcProfileDef;
		//internal List<int> mCrossSectionPositions = new List<int>();// : LIST [2:?] OF IfcAxis2Placement3D; 
	}

	public partial class IfcSectionProperties : IfcPreDefinedProperties // IFC2x3 BaseClassIfc
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("SectionType", mSectionType.ToString().ToLower());
			xml.AppendChild(StartProfile.GetXML(xml.OwnerDocument, "StartProfile", this, processed));
			if(mEndProfile > 0)
				xml.AppendChild(EndProfile.GetXML(xml.OwnerDocument, "EndProfile", this, processed));
		}
	}
	public partial class IfcSectionReinforcementProperties : IfcPreDefinedProperties // IFC2x3 STPEntity
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("LongitudinalStartPosition", mLongitudinalStartPosition.ToString());
			xml.SetAttribute("LongitudinalEndPosition", mLongitudinalEndPosition.ToString());
			if(!double.IsNaN(mTransversePosition))
				xml.SetAttribute("TransversePosition", mTransversePosition.ToString());
			if(mReinforcementRole != IfcReinforcingBarRoleEnum.NOTDEFINED)
				xml.SetAttribute("ReinforcementRole", mReinforcementRole.ToString().ToLower());
			xml.AppendChild(SectionDefinition.GetXML(xml.OwnerDocument, "SectionDefinition", this, processed));
			XmlElement element = xml.OwnerDocument.CreateElement("CrossSectionReinforcementDefinitions");
			xml.AppendChild(element);
			foreach (IfcReinforcementBarProperties p in CrossSectionReinforcementDefinitions)
				element.AppendChild(p.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcShapeAspect : BaseClassIfc
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ShapeRepresentations") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcShapeModel s = mDatabase.ParseXml<IfcShapeModel>(cn as XmlElement);
						if (s != null)
							addRepresentation(s);
					}
				}
				else if (string.Compare(name, "PartOfProductDefinitionShape") == 0)
					PartOfProductDefinitionShape = mDatabase.ParseXml<IfcProductRepresentationSelect>(child as XmlElement);
			}
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			if (xml.HasAttribute("ProductDefinitional"))
				Enum.TryParse<IfcLogicalEnum>(xml.Attributes["ProductDefinitional"].Value, true, out mProductDefinitional);

		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("ShapeRepresentations");
			xml.AppendChild(element);
			foreach (IfcShapeModel s in ShapeRepresentations)
				element.AppendChild(s.GetXML(xml.OwnerDocument, "", this, processed));
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			xml.SetAttribute("ProductDefinitional", ProductDefinitional.ToString().ToLower());
			if (mPartOfProductDefinitionShape > 0)
				xml.AppendChild(mDatabase[mPartOfProductDefinitionShape].GetXML(xml.OwnerDocument, "PartOfProductDefinitionShape", this, processed));
		}
	}
	public partial class IfcShellBasedSurfaceModel : IfcGeometricRepresentationItem
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "SbsmBoundary") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcShell s = mDatabase.ParseXml<IfcShell>(cn as XmlElement);
						if (s != null)
							addBoundary(s);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("SbsmBoundary");
			xml.AppendChild(element);
			foreach (int i in mSbsmBoundary)
				element.AppendChild(mDatabase[i].GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcSimplePropertyTemplate : IfcPropertyTemplate
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("TemplateType"))
				Enum.TryParse<IfcSimplePropertyTemplateTypeEnum>(xml.Attributes["TemplateType"].Value, true, out mTemplateType);
			PrimaryMeasureType = extractString(xml, "PrimaryMeasureType");
			SecondaryMeasureType = extractString(xml, "SecondaryMeasureType");
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Enumerators") == 0)
				{
					if(child.HasChildNodes)
						Enumerators = mDatabase.ParseXml<IfcPropertyEnumeration>(child.ChildNodes[0] as XmlElement);
					else
						Enumerators = mDatabase.ParseXml<IfcPropertyEnumeration>(child as XmlElement);
				}
				else if (string.Compare(name, "PrimaryUnit") == 0)
					PrimaryUnit = mDatabase.ParseXml<IfcUnit>(child as XmlElement);
				else if (string.Compare(name, "SecondaryUnit") == 0)
					SecondaryUnit = mDatabase.ParseXml<IfcUnit>(child as XmlElement);
			}
			Expression = extractString(xml, "Expression");
			if (xml.HasAttribute("AccessState"))
				Enum.TryParse<IfcStateEnum>(xml.Attributes["AccessState"].Value, true, out mAccessState);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mTemplateType != IfcSimplePropertyTemplateTypeEnum.NOTDEFINED)
				xml.SetAttribute("TemplateType", mTemplateType.ToString().ToLower());
			setAttribute(xml, "PrimaryMeasureType", PrimaryMeasureType);
			setAttribute(xml, "SecondaryMeasureType", SecondaryMeasureType);
			if (mEnumerators > 0)
				xml.AppendChild(Enumerators.GetXML(xml.OwnerDocument, "Enumerators", this, processed));
			if (mPrimaryUnit > 0)
				xml.AppendChild(mDatabase[mPrimaryUnit].GetXML(xml.OwnerDocument, "PrimaryUnit", this, processed));
			if (mSecondaryUnit > 0)
				xml.AppendChild(mDatabase[mSecondaryUnit].GetXML(xml.OwnerDocument, "SecondaryUnit", this, processed));
			setAttribute(xml, "Expression", Expression);
			if (mAccessState != IfcStateEnum.NOTDEFINED)
				xml.SetAttribute("AccessState", mAccessState.ToString().ToLower());
		}
	}
	public partial class IfcSite : IfcSpatialStructureElement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("RefLatitude"))
			{
				string[] fields = xml.Attributes["RefLatitude"].Value.Split(" ".ToCharArray());
				RefLatitude = new IfcCompoundPlaneAngleMeasure(int.Parse(fields[0]), int.Parse(fields[1]), int.Parse(fields[2]), (fields.Length > 3 ? int.Parse(fields[3]) : 0));
			}
			if (xml.HasAttribute("RefLongitude"))
			{
				string[] fields = xml.Attributes["RefLongitude"].Value.Split(" ".ToCharArray());
				RefLongitude = new IfcCompoundPlaneAngleMeasure(int.Parse(fields[0]), int.Parse(fields[1]), int.Parse(fields[2]),(fields.Length > 3 ? int.Parse(fields[3]) : 0));
			}
			if (xml.HasAttribute("RefElevation"))
				RefElevation = double.Parse(xml.Attributes["RefElevation"].Value);
			if (xml.HasAttribute("LandTitleNumber"))
				LandTitleNumber = xml.Attributes["LandTitleNumber"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "SiteAddress") == 0)
					SiteAddress = mDatabase.ParseXml<IfcPostalAddress>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mRefLatitude != null)
			{
				string str = mRefLatitude.ToString();
				xml.SetAttribute("RefLatitude", str.Substring(1, str.Length - 2).Replace(',', ' '));
			}
			if (mRefLongitude != null)
			{
				string str = mRefLongitude.ToString();
				xml.SetAttribute("RefLongitude", str.Substring(1, str.Length - 2).Replace(',', ' '));
			}
			if (!double.IsNaN(mRefElevation))
				xml.SetAttribute("RefElevation", mRefElevation.ToString());
			setAttribute(xml, "LandTitleNumber", LandTitleNumber);
			if (mSiteAddress > 0)
				xml.AppendChild(SiteAddress.GetXML(xml.OwnerDocument, "SiteAddress", this, processed));
		}
	}
	public partial class IfcSIUnit : IfcNamedUnit
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Prefix"))
				Enum.TryParse<IfcSIPrefix>(xml.Attributes["Prefix"].Value, true, out mPrefix);
			if (xml.HasAttribute("Name"))
				Enum.TryParse<IfcSIUnitName>(xml.Attributes["Name"].Value, true, out mName);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPrefix != IfcSIPrefix.NONE)
				xml.SetAttribute("Prefix", mPrefix.ToString().ToLower());
			xml.SetAttribute("Name", mName.ToString().ToLower());
		}
	}
	public partial class IfcSlabType : IfcBuildingElementType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcSlabTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcSlabTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcSpace : IfcSpatialStructureElement, IfcSpaceBoundarySelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcSpaceTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
			else if (xml.HasAttribute("InteriorOrExteriorSpace"))
				Enum.TryParse<IfcSpaceTypeEnum>(xml.Attributes["InteriorOrExteriorSpace"].Value, true, out mPredefinedType);
			if (xml.HasAttribute("ElevationWithFlooring"))
				ElevationWithFlooring = double.Parse(xml.Attributes["ElevationWithFlooring"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mDatabase.mRelease < ReleaseVersion.IFC4)
				xml.SetAttribute("InteriorOrExteriorSpace", mPredefinedType == IfcSpaceTypeEnum.INTERNAL || mPredefinedType == IfcSpaceTypeEnum.EXTERNAL ? mPredefinedType.ToString().ToLower() : "notdefined");
			else if (mPredefinedType != IfcSpaceTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
			if (!double.IsNaN(mElevationWithFlooring))
				xml.SetAttribute("ElevationWithFlooring", mElevationWithFlooring.ToString());
		}
	}

	public abstract partial class IfcSpatialElement : IfcProduct //ABSTRACT SUPERTYPE OF (ONEOF (IfcExternalSpatialStructureElement ,IfcSpatialStructureElement ,IfcSpatialZone))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("LongName"))
				LongName = xml.Attributes["LongName"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ContainsElements") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcRelContainedInSpatialStructure rcss = mDatabase.ParseXml<IfcRelContainedInSpatialStructure>(node as XmlElement);
						if (rcss != null)
							rcss.RelatingStructure = this;
					}
				}
			}
		}

		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			base.setAttribute(xml, "LongName", LongName);
			if (mContainsElements.Count > 0)
			{
				XmlElement element = null;
				foreach (IfcRelContainedInSpatialStructure rcss in ContainsElements)
				{
					if (rcss.mRelatedElements.Count > 0)
					{
						if (element == null)
							element = xml.OwnerDocument.CreateElement("ContainsElements");
						element.AppendChild(rcss.GetXML(xml.OwnerDocument, "", this, processed));
					}
				}
				if (element != null)
					xml.AppendChild(element);
			}
			if (mServicedBySystems.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("ServicedBySystems");
				xml.AppendChild(element);
				foreach (IfcRelServicesBuildings rsbs in ServicedBySystems)
					element.AppendChild(rsbs.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public abstract partial class IfcSpatialStructureElement : IfcSpatialElement /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBuilding ,IfcBuildingStorey ,IfcSite ,IfcSpace, IfcCivilStructureElement))*/
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("CompositionType"))
				Enum.TryParse<IfcElementCompositionEnum>(xml.Attributes["CompositionType"].Value, true, out mCompositionType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mCompositionType != IfcElementCompositionEnum.NA)
				xml.SetAttribute("CompositionType", mCompositionType.ToString().ToLower());
		}
	}
	public partial class IfcSphere : IfcCsgPrimitive3D
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Radius"))
				mRadius = double.Parse(xml.Attributes["Radius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Radius", mRadius.ToString());
		}
	}
	public abstract partial class IfcStructuralAction : IfcStructuralActivity // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveAction, IfcStructuralPointAction, IfcStructuralSurfaceAction))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("DestabilizingLoad"))
				Enum.TryParse<IfcLogicalEnum>(xml.Attributes["DestabilizingLoad"].Value, true, out mDestabilizingLoad);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "CausedBy") == 0)
					CausedBy = mDatabase.ParseXml<IfcStructuralReaction>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mDestabilizingLoad == IfcLogicalEnum.UNKNOWN)
			{
				if (mDatabase.Release < ReleaseVersion.IFC4)
					xml.SetAttribute("DestabilizingLoad", "false");
			}
			else
				xml.SetAttribute("DestabilizingLoad", mDestabilizingLoad.ToString().ToLower());
			if (mCausedBy > 0)
				xml.AppendChild(mDatabase[mCausedBy].GetXML(xml.OwnerDocument, "CausedBy", this, processed));
		}
	}
	public abstract partial class IfcStructuralActivity : IfcProduct
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("GlobalOrLocal"))
				Enum.TryParse<IfcGlobalOrLocalEnum>(xml.Attributes["GlobalOrLocal"].Value, true, out mGlobalOrLocal);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "AppliedLoad") == 0)
					AppliedLoad = mDatabase.ParseXml<IfcStructuralLoad>(child as XmlElement);
				else if (string.Compare(name, "AssignedToStructuralItem") == 0)
					AssignedToStructuralItem = mDatabase.ParseXml<IfcRelConnectsStructuralActivity>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(mDatabase[mAppliedLoad].GetXML(xml.OwnerDocument, "AppliedLoad", this, processed));
			xml.SetAttribute("GlobalOrLocal", mGlobalOrLocal.ToString().ToLower());
			if (mAssignedToStructuralItem != null)
				xml.AppendChild(mAssignedToStructuralItem.GetXML(xml.OwnerDocument, "AssignedToStructuralItem", this, processed));
		}
	}
	public partial class IfcStructuralAnalysisModel : IfcSystem
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcAnalysisModelTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "OrientationOf2DPlane") == 0)
					OrientationOf2DPlane = mDatabase.ParseXml<IfcAxis2Placement3D>(child as XmlElement);
				else if (string.Compare(name, "LoadedBy") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcStructuralLoadGroup s = mDatabase.ParseXml<IfcStructuralLoadGroup>(cn as XmlElement);
						if (s != null)
							addLoadGroup(s);
					}
				}
				else if (string.Compare(name, "HasResults") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcStructuralResultGroup s = mDatabase.ParseXml<IfcStructuralResultGroup>(cn as XmlElement);
						if (s != null)
							addResultGroup(s);
					}
				}
				else if (string.Compare(name, "SharedPlacement") == 0)
					SharedPlacement = mDatabase.ParseXml<IfcObjectPlacement>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);

			if (mPredefinedType != IfcAnalysisModelTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
			if (mOrientationOf2DPlane > 0)
				xml.AppendChild(mDatabase[mOrientationOf2DPlane].GetXML(xml.OwnerDocument, "OrientationOf2DPlane", this, processed));
			if (mLoadedBy.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("LoadedBy");
				xml.AppendChild(element);
				foreach (int item in mLoadedBy)
					element.AppendChild(mDatabase[item].GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (mHasResults.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasResults");
				xml.AppendChild(element);
				foreach (int item in mHasResults)
					element.AppendChild(mDatabase[item].GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (mSharedPlacement > 0)
				xml.AppendChild(mDatabase[mSharedPlacement].GetXML(xml.OwnerDocument, "SharedPlacement", this, processed));
		}
	}
	public abstract partial class IfcStructuralConnection : IfcStructuralItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveConnection ,IfcStructuralPointConnection ,IfcStructuralSurfaceConnection))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "AppliedCondition") == 0)
					AppliedCondition = mDatabase.ParseXml<IfcBoundaryCondition>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mAppliedCondition > 0)
				xml.AppendChild(mDatabase[mAppliedCondition].GetXML(xml.OwnerDocument, "AppliedCondition", this, processed));
		}
	}
	public abstract partial class IfcStructuralConnectionCondition : BaseClassIfc //ABSTRACT SUPERTYPE OF (ONEOF (IfcFailureConnectionCondition ,IfcSlippageConnectionCondition));
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Name", Name);
		}
	}
	public partial class IfcStructuralCurveAction : IfcStructuralAction // IFC4 SUPERTYPE OF(IfcStructuralLinearAction)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("ProjectedOrTrue"))
				Enum.TryParse<IfcProjectedOrTrueLengthEnum>(xml.Attributes["ProjectedOrTrue"].Value, true, out mProjectedOrTrue);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcStructuralCurveActivityTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("ProjectedOrTrue", mProjectedOrTrue.ToString().ToLower());
			if (mPredefinedType != IfcStructuralCurveActivityTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcStructuralCurveMember : IfcStructuralMember
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcStructuralCurveMemberTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Axis") == 0)
					Axis = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcStructuralCurveMemberTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
			if (mAxis > 0)
				xml.AppendChild(mDatabase[mAxis].GetXML(xml.OwnerDocument, "Axis", this, processed));
		}
	}
	public partial class IfcStructuralCurveReaction : IfcStructuralReaction
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcStructuralCurveActivityTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcStructuralCurveActivityTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public abstract partial class IfcStructuralItem : IfcProduct, IfcStructuralActivityAssignmentSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralConnection ,IfcStructuralMember))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "AssignedStructuralActivity") == 0)
					mDatabase.ParseXml<IfcRelConnectsStructuralActivity>(child as XmlElement).RelatingElement = this;
			}
		}
	}
	public partial class IfcStructuralLoadLinearForce : IfcStructuralLoadStatic
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("LinearForceX"))
				double.TryParse(xml.Attributes["LinearForceX"].Value, out mLinearForceX);
			if (xml.HasAttribute("LinearForceY"))
				double.TryParse(xml.Attributes["LinearForceY"].Value, out mLinearForceY);
			if (xml.HasAttribute("LinearForceZ"))
				double.TryParse(xml.Attributes["LinearForceZ"].Value, out mLinearForceZ);
			if (xml.HasAttribute("LinearMomentX"))
				double.TryParse(xml.Attributes["LinearMomentX"].Value, out mLinearMomentX);
			if (xml.HasAttribute("LinearMomentY"))
				double.TryParse(xml.Attributes["LinearMomentY"].Value, out mLinearMomentY);
			if (xml.HasAttribute("LinearMomentZ"))
				double.TryParse(xml.Attributes["LinearMomentZ"].Value, out mLinearMomentZ);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (!double.IsNaN(mLinearForceX))
				xml.SetAttribute("LinearForceX", mLinearForceX.ToString());
			if (!double.IsNaN(mLinearForceY))
				xml.SetAttribute("LinearForceY", mLinearForceY.ToString());
			if (!double.IsNaN(mLinearForceZ))
				xml.SetAttribute("LinearForceZ", mLinearForceZ.ToString());
			if (!double.IsNaN(mLinearMomentX))
				xml.SetAttribute("LinearMomentX", mLinearMomentX.ToString());
			if (!double.IsNaN(mLinearMomentY))
				xml.SetAttribute("LinearMomentY", mLinearMomentY.ToString());
			if (!double.IsNaN(mLinearMomentZ))
				xml.SetAttribute("LinearMomentZ", mLinearMomentZ.ToString());
		}
	}
	public partial class IfcStructuralLoadSingleForce : IfcStructuralLoadStatic
	{

	}
	public partial class IfcStyledItem : IfcRepresentationItem
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Item") == 0)
					Item = mDatabase.ParseXml<IfcRepresentationItem>(child as XmlElement);
				else if (string.Compare(name, "Styles") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcStyleAssignmentSelect s = mDatabase.ParseXml<IfcStyleAssignmentSelect>(cn as XmlElement);
						if (s != null)
							addStyle(s);
					}
				}
			}
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Styles");
			xml.AppendChild(element);
			foreach (int item in mStyles)
				element.AppendChild(mDatabase[item].GetXML(xml.OwnerDocument, "", this, processed));
			setAttribute(xml, "Name", Name);
		}
	}
	public partial class IfcSurfaceCurveSweptAreaSolid : IfcSweptAreaSolid
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Directrix") == 0)
					Directrix = mDatabase.ParseXml<IfcCurve>(child as XmlElement);
				else if (string.Compare(name, "ReferenceSurface") == 0)
					ReferenceSurface = mDatabase.ParseXml<IfcSurface>(child as XmlElement);
			}
			if (xml.HasAttribute("StartParam"))
				mStartParam = double.Parse(xml.Attributes["StartParam"].Value);
			if (xml.HasAttribute("EndParam"))
				mStartParam = double.Parse(xml.Attributes["EndParam"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Directrix.GetXML(xml.OwnerDocument, "Directrix", this, processed));
			if (!double.IsNaN(mStartParam))
				xml.SetAttribute("StartParam", mStartParam.ToString());
			if (!double.IsNaN(mEndParam))
				xml.SetAttribute("EndParam", mEndParam.ToString());
			xml.AppendChild(ReferenceSurface.GetXML(xml.OwnerDocument, "ReferenceSurface", this, processed));
		}
	}
	public partial class IfcSurfaceOfLinearExtrusion : IfcSweptSurface
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
	public partial class IfcSurfaceOfRevolution : IfcSweptSurface
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "AxisPosition") == 0)
					AxisPosition = mDatabase.ParseXml<IfcAxis1Placement>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(AxisPosition.GetXML(xml.OwnerDocument, "AxisPosition", this, processed));
		}
	}
	public partial class IfcSurfaceStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Side"))
				Enum.TryParse<IfcSurfaceSide>(xml.Attributes["Side"].Value, true, out mSide);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Styles") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcSurfaceStyleElementSelect s = mDatabase.ParseXml<IfcSurfaceStyleElementSelect>(cn as XmlElement);
						if (s != null)
							addStyle(s);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Side", mSide.ToString().ToLower());
			XmlElement element = xml.OwnerDocument.CreateElement("Styles");
			xml.AppendChild(element);
			foreach (int item in mStyles)
				element.AppendChild(mDatabase[item].GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcSurfaceStyleRendering : IfcSurfaceStyleShading
	{
		//internal double mTransparency;// : OPTIONAL IfcNormalisedRatioMeasure;
		//internal IfcColourOrFactor mDiffuseColour, mTransmissionColour, mDiffuseTransmissionColour, mReflectionColour, mSpecularColour;//:	OPTIONAL IfcColourOrFactor;
		//internal IfcSpecularHighlightSelect mSpecularHighlight;// : OPTIONAL 
		//internal IfcReflectanceMethodEnum mReflectanceMethod = IfcReflectanceMethodEnum.NOTDEFINED;// : IfcReflectanceMethodEnum; 

		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Transparency"))
				mTransparency = double.Parse(xml.Attributes["Transparency"].Value);

		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (!double.IsNaN(mTransparency))
				xml.SetAttribute("Transparency", mTransparency.ToString());

		}
	}
	public partial class IfcSurfaceStyleShading : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "SurfaceColour") == 0)
					SurfaceColour = mDatabase.ParseXml<IfcColourRgb>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(SurfaceColour.GetXML(xml.OwnerDocument, "SurfaceColour", this, processed));
		}
	}
	public abstract partial class IfcSweptAreaSolid : IfcSolidModel  /*ABSTRACT SUPERTYPE OF (ONEOF (IfcExtrudedAreaSolid, IfcFixedReferenceSweptAreaSolid ,IfcRevolvedAreaSolid ,IfcSurfaceCurveSweptAreaSolid))*/
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "SweptArea") == 0)
					SweptArea = mDatabase.ParseXml<IfcProfileDef>(child as XmlElement);
				if (string.Compare(name, "Position") == 0)
					Position = mDatabase.ParseXml<IfcAxis2Placement3D>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(SweptArea.GetXML(xml.OwnerDocument, "SweptArea", this, processed));
			if (mPosition > 0)
				xml.AppendChild(Position.GetXML(xml.OwnerDocument, "Position", this, processed));
		}
	}
	public partial class IfcSweptDiskSolid : IfcSolidModel
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Directrix") == 0)
					Directrix = mDatabase.ParseXml<IfcCurve>(child as XmlElement);
			}
			if (xml.HasAttribute("Radius"))
				mRadius = double.Parse(xml.Attributes["Radius"].Value);
			if (xml.HasAttribute("InnerRadius"))
				mInnerRadius = double.Parse(xml.Attributes["InnerRadius"].Value);
			if (xml.HasAttribute("StartParam"))
				mStartParam = double.Parse(xml.Attributes["StartParam"].Value);
			if (xml.HasAttribute("EndParam"))
				mStartParam = double.Parse(xml.Attributes["EndParam"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Directrix.GetXML(xml.OwnerDocument, "Directrix", this, processed));
			xml.SetAttribute("Radius", mRadius.ToString());
			if (!double.IsNaN(mInnerRadius))
				xml.SetAttribute("InnerRadius", mInnerRadius.ToString());
			if (!double.IsNaN(mStartParam))
				xml.SetAttribute("StartParam", mStartParam.ToString());
			if (!double.IsNaN(mEndParam))
				xml.SetAttribute("EndParam", mEndParam.ToString());
		}
	}
	public abstract partial class IfcSweptSurface : IfcSurface /*	ABSTRACT SUPERTYPE OF (ONEOF (IfcSurfaceOfLinearExtrusion ,IfcSurfaceOfRevolution))*/
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "SweptCurve") == 0)
					SweptCurve = mDatabase.ParseXml<IfcProfileDef>(child as XmlElement);
				if (string.Compare(name, "Position") == 0)
					Position = mDatabase.ParseXml<IfcAxis2Placement3D>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(SweptCurve.GetXML(xml.OwnerDocument, "SweptCurve", this, processed));
			if (mPosition > 0)
				xml.AppendChild(Position.GetXML(xml.OwnerDocument, "Position", this, processed));
		}
	}
}
