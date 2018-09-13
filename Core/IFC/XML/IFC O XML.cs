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
	public abstract partial class IfcObject : IfcObjectDefinition //ABSTRACT SUPERTYPE OF (ONEOF (IfcActor ,IfcControl ,IfcGroup ,IfcProcess ,IfcProduct ,IfcProject ,IfcResource))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("ObjectType"))
				ObjectType = xml.Attributes["ObjectType"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "IsTypedBy") == 0)
					mIsTypedBy = mDatabase.ParseXml<IfcRelDefinesByType>(child as XmlElement);
				if (string.Compare(name, "IsDefinedBy") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcRelDefinesByProperties rd = mDatabase.ParseXml<IfcRelDefinesByProperties>(node as XmlElement);
						if (rd != null)
							rd.RelatedObjects.Add(this);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "ObjectType", ObjectType);
			if (mIsTypedBy != null)
				xml.AppendChild(mIsTypedBy.GetXML(xml.OwnerDocument, "IsTypedBy", this, processed));
			if (mIsDefinedBy.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("IsDefinedBy");
				xml.AppendChild(element);
				foreach (IfcRelDefinesByProperties rd in IsDefinedBy)
					element.AppendChild(rd.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public abstract partial class IfcObjectDefinition : IfcRoot, IfcDefinitionSelect  //ABSTRACT SUPERTYPE OF (ONEOF ((IfcContext, IfcObject, IfcTypeObject))))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "HasAssignments") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcRelAssigns ra = mDatabase.ParseXml<IfcRelAssigns>(node as XmlElement);
						if (ra != null)
							ra.RelatedObjects.Add(this);
					}
				}
				else if (string.Compare(name, "IsNestedBy") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcRelNests rn = mDatabase.ParseXml<IfcRelNests>(node as XmlElement);
						if (rn != null)
							rn.RelatingObject = this;
					}
				}
				else if (string.Compare(name, "IsDecomposedBy") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcRelAggregates ra = mDatabase.ParseXml<IfcRelAggregates>(node as XmlElement);
						if (ra != null)
							ra.RelatingObject = this;
					}
				}
				else if (string.Compare(name, "HasAssociations") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcRelAssociates ra = mDatabase.ParseXml<IfcRelAssociates>(node as XmlElement);
						if (ra != null)
							ra.RelatedObjects.Add(this);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mHasAssignments.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasAssignments");
				foreach (IfcRelAssigns rap in mHasAssignments)
				{
					if (rap.mIndex != host.mIndex)
						element.AppendChild(rap.GetXML(xml.OwnerDocument, "", this, processed));
				}
				if (element.HasChildNodes)
					xml.AppendChild(element);
			}
			if (mIsNestedBy.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("IsNestedBy");
				xml.AppendChild(element);
				foreach (IfcRelNests rn in IsNestedBy)
					element.AppendChild(rn.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (mIsDecomposedBy.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("IsDecomposedBy");
				xml.AppendChild(element);
				foreach (IfcRelAggregates rags in IsDecomposedBy)
					element.AppendChild(rags.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (mHasAssociations.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasAssociations");
				xml.AppendChild(element);
				foreach (IfcRelAssociates ra in HasAssociations)
					element.AppendChild(ra.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public partial class IfcObjective : IfcConstraint
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "BenchmarkValues") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcConstraint constraint = mDatabase.ParseXml<IfcConstraint>(cn as XmlElement);
						if (constraint != null)
							AddBenchmark(constraint);
					}
				}

			}
			if (xml.HasAttribute("LogicalAggregator"))
				Enum.TryParse<IfcLogicalOperatorEnum>(xml.Attributes["LogicalAggregator"].Value, true, out mLogicalAggregator);
			if (xml.HasAttribute("ObjectiveQualifier"))
				Enum.TryParse<IfcObjectiveEnum>(xml.Attributes["ObjectiveQualifier"].Value, true, out mObjectiveQualifier);
			if (xml.HasAttribute("UserDefinedQualifier"))
				UserDefinedQualifier = xml.Attributes["UserDefinedQualifier"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mBenchmarkValues.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("BenchmarkValues");
				xml.AppendChild(element);
				foreach (IfcConstraint constraint in BenchmarkValues)
					element.AppendChild(constraint.GetXML(xml.OwnerDocument, "", this, processed));
				if (mLogicalAggregator != IfcLogicalOperatorEnum.NONE)
					xml.SetAttribute("LogicalAggregator", mLogicalAggregator.ToString().ToLower());
				if (mObjectiveQualifier != IfcObjectiveEnum.NOTDEFINED)
					xml.SetAttribute("ObjectiveQualifier", mObjectiveQualifier.ToString().ToLower());
				setAttribute(xml, "UserDefinedQualifier", UserDefinedQualifier);
			}

		}
	}
	public partial class IfcOpeningElement : IfcFeatureElementSubtraction //SUPERTYPE OF(IfcOpeningStandardCase)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcOpeningElementTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "HasFillings") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcRelFillsElement fills = mDatabase.ParseXml<IfcRelFillsElement>(node as XmlElement);
						if (fills != null)
							fills.RelatingOpeningElement = this;
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcOpeningElementTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
			if (mHasFillings.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasFillings");
				foreach (IfcRelFillsElement fills in mHasFillings)
				{
					if (fills.mIndex != host.mIndex)
						element.AppendChild(fills.GetXML(xml.OwnerDocument, "", this, processed));
				}
				if (element.HasChildNodes)
					xml.AppendChild(element);
			}
		}
	}
	public partial class IfcOrganization : BaseClassIfc, IfcActorSelect, IfcResourceObjectSelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Identification"))
				Identification = xml.Attributes["Identification"].Value;
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Roles") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcActorRole role = mDatabase.ParseXml<IfcActorRole>(cn as XmlElement);
						if (role != null)
							Roles.Add(role);
					}
				}
				else if (string.Compare(name, "Addresses") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcAddress address = mDatabase.ParseXml<IfcAddress>(cn as XmlElement);
						if (address != null)
							Addresses.Add(address);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Identification", Identification);
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			if(mRoles.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("Roles");
				xml.AppendChild(element);
				foreach (IfcActorRole role in Roles)
					element.AppendChild(role.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if(mAddresses.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("Addresses");
				xml.AppendChild(element);
				foreach (IfcAddress address in Addresses)
					element.AppendChild(address.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public partial class IfcOwnerHistory : BaseClassIfc
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "OwningUser") == 0)
					OwningUser = mDatabase.ParseXml<IfcPersonAndOrganization>(child as XmlElement);
				else if (string.Compare(name, "OwningApplication") == 0)
					OwningApplication = mDatabase.ParseXml<IfcApplication>(child as XmlElement);
				else if (string.Compare(name, "LastModifyingUser") == 0)
					LastModifyingUser = mDatabase.ParseXml<IfcPersonAndOrganization>(child as XmlElement);
				else if (string.Compare(name, "LastModifyingApplication") == 0)
					LastModifyingApplication = mDatabase.ParseXml<IfcApplication>(child as XmlElement);
			}
			if (xml.HasAttribute("State")) 
				Enum.TryParse<IfcStateEnum>(xml.Attributes["State"].Value, true, out mState);
			if (xml.HasAttribute("ChangeAction"))
				Enum.TryParse<IfcChangeActionEnum>(xml.Attributes["ChangeAction"].Value,true, out mChangeAction);
			if (xml.HasAttribute("LastModifiedDate"))
				mLastModifiedDate = int.Parse(xml.Attributes["LastModifiedDate"].Value);
			if (xml.HasAttribute("CreationDate"))
				mCreationDate = int.Parse(xml.Attributes["CreationDate"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			
			xml.AppendChild(OwningUser.GetXML(xml.OwnerDocument, "OwningUser", this, processed));
			xml.AppendChild(OwningApplication.GetXML(xml.OwnerDocument, "OwningApplication", this, processed));
			if (mState != IfcStateEnum.NOTDEFINED)
				xml.SetAttribute("State", mState.ToString().ToLower());
			xml.SetAttribute("ChangeAction", mChangeAction.ToString().ToLower());
			if (mLastModifiedDate > 0)
				xml.SetAttribute("LastModifiedDate", mLastModifiedDate.ToString());
			if(mLastModifyingUser != null)
				xml.AppendChild(LastModifyingUser.GetXML(xml.OwnerDocument, "LastModifyingUser", this, processed));
			if (mLastModifyingApplication != null)
				xml.AppendChild(LastModifyingApplication.GetXML(xml.OwnerDocument, "LastModifyingApplication", this, processed));
			xml.SetAttribute("CreationDate", mCreationDate.ToString());
		}
	}
}
