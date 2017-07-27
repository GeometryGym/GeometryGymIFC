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
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;

using Newtonsoft.Json.Linq;

namespace GeometryGym.Ifc
{
	public abstract partial class IfcParameterizedProfileDef : IfcProfileDef //ABSTRACT SUPERTYPE OF (ONEOF (IfcCShapeProfileDef ,IfcCircleProfileDef ,IfcCraneRailAShapeProfileDef ,IfcCraneRailFShapeProfileDef ,
	{//IfcEllipseProfileDef ,IfcIShapeProfileDef ,IfcLShapeProfileDef ,IfcRectangleProfileDef ,IfcTShapeProfileDef ,IfcTrapeziumProfileDef ,IfcUShapeProfileDef ,IfcZShapeProfileDef))*/
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Position", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Position = mDatabase.parseJObject<IfcAxis2Placement2D>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPosition > 0)
				obj["Position"] = Position.getJson(this, processed);
		}
	}
	public partial class IfcPerson : BaseClassIfc, IfcActorSelect, IfcResourceObjectSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Identification", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Identification = token.Value<string>();
			token = obj.GetValue("FamilyName", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				FamilyName = token.Value<string>();
			token = obj.GetValue("GivenName", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				GivenName = token.Value<string>();
			JArray array = obj.GetValue("MiddleName", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
			{
				foreach (string s in array.Values<string>())
					AddMiddleName(s);
			}
			mDatabase.extractJArray<IfcActorRole>(obj.GetValue("Roles", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => AddRole(x));
			mDatabase.extractJArray<IfcAddress>(obj.GetValue("Addresses", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => AddAddress(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			string identification = Identification;
			if (!string.IsNullOrEmpty(identification))
				obj["Identification"] = identification;
			string familyName = FamilyName;
			if (!string.IsNullOrEmpty(familyName))
				obj["FamilyName"] = familyName;
			string givenName = GivenName;
			if (!string.IsNullOrEmpty(givenName))
				obj["GivenName"] = givenName;
			
			if (mMiddleNames.Count > 0)
			{
				JArray array = new JArray();
				IEnumerable<string> middleNames = MiddleNames;
				foreach (string name in middleNames)
					array.Add(name);
				obj["MiddleNames"] = array;
			}
			ReadOnlyCollection<IfcActorRole> roles = Roles;
			if (roles.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcActorRole role in roles)
					array.Add(role.getJson(this, processed));
				obj["Roles"] = array;
			}
			ReadOnlyCollection<IfcAddress> addresses = Addresses;
			if (addresses.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcAddress address in addresses)
					array.Add(address.getJson(this, processed));
				obj["Addresses"] = array;
			}
		}

	}
	public partial class IfcPersonAndOrganization : BaseClassIfc, IfcActorSelect, IfcResourceObjectSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			ThePerson = mDatabase.parseJObject<IfcPerson>(obj.GetValue("ThePerson", StringComparison.InvariantCultureIgnoreCase) as JObject);
			TheOrganization = mDatabase.parseJObject<IfcOrganization>(obj.GetValue("TheOrganization", StringComparison.InvariantCultureIgnoreCase) as JObject);
			mDatabase.extractJArray<IfcActorRole>(obj.GetValue("Roles", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>AddRole(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["ThePerson"] = ThePerson.getJson(this, processed);
			obj["TheOrganization"] = TheOrganization.getJson(this, processed);
			ReadOnlyCollection<IfcActorRole> roles = Roles;
			if (roles.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcActorRole role in roles)
					array.Add(role.getJson(this, processed));
				obj["Roles"] = array;
			}

		}
		//internal int mThePerson;// : IfcPerson;
		//internal int mTheOrganization;// : IfcOrganization;
		//internal string mRoles = "$";// TODO : OPTIONAL LIST [1:?] OF IfcActorRole
	}
	public abstract partial class IfcPhysicalQuantity : BaseClassIfc, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF(ONEOF(IfcPhysicalComplexQuantity, IfcPhysicalSimpleQuantity));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
			token = obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Description = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["Name"] = Name;
			setAttribute(obj, "Description", Description);
		}
	}
	public partial class IfcPipeFittingType : IfcFlowFittingType
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcPipeFittingTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPredefinedType != IfcPipeFittingTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public abstract partial class IfcPlacement : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcAxis1Placement ,IfcAxis2Placement2D ,IfcAxis2Placement3D))*/
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Location", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
			{
				JObject jobj = token as JObject;
				if (jobj != null)
				{
					JToken jtoken = jobj["href"];
					if (jtoken != null)
						mLocation = jtoken.Value<int>();
					else
						Location = mDatabase.parseJObject<IfcCartesianPoint>(jobj);
				}
				else
					mLocation = token.Value<int>();
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["Location"] = Location.getJson(this, processed);
		}
	}
	public partial class IfcPolyline : IfcBoundedCurve
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mDatabase.extractJArray<IfcCartesianPoint>(obj.GetValue("Points", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>addPoint(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			JArray array = new JArray();
			foreach (IfcCartesianPoint p in Points)
				array.Add(p.getJson(this, processed));
			obj["Points"] = array;
		}
	}
	public partial class IfcPresentationLayerAssignment : BaseClassIfc //SUPERTYPE OF	(IfcPresentationLayerWithStyle);
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Name = extractString(obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase));
			Description = extractString(obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase));
			mDatabase.extractJArray<IfcLayeredItem>(obj.GetValue("AssignedItems", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>assign(x));
			Identifier = extractString(obj.GetValue("Identifier", StringComparison.InvariantCultureIgnoreCase));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			//XmlElement element = xml.OwnerDocument.CreateElement("AssignedItems");
			//xml.AppendChild(element);
			//foreach (int item in mAssignedItems)
			//	element.AppendChild(mDatabase[item].GetXML(xml.OwnerDocument, "", this, processed));
			setAttribute(obj, "Identifier", Identifier);
		}
	}
	public partial class IfcPresentationLayerWithStyle : IfcPresentationLayerAssignment
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("LayerOn", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcLogicalEnum>(token.Value<string>(), true, out mLayerOn);
			token = obj.GetValue("LayerFrozen", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcLogicalEnum>(token.Value<string>(), true, out mLayerFrozen);
			token = obj.GetValue("LayerBlocked", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcLogicalEnum>(token.Value<string>(), true, out mLayerBlocked);
			mDatabase.extractJArray<IfcPresentationStyle>(obj.GetValue("LayerStyles", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>addLayerStyle(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["LayerOn"] = mLayerOn.ToString();
			obj["LayeyFrozen"] = mLayerFrozen.ToString();
			obj["LayerBlocked"] = mLayerBlocked.ToString();
			if (mLayerStyles.Count > 0)
				obj["LayerStyles"] = new JArray(LayerStyles.ToList().ConvertAll(x => x.getJson(this, processed)));
		}
	}
	public abstract partial class IfcPresentationStyle : BaseClassIfc, IfcStyleAssignmentSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcCurveStyle,IfcFillAreaStyle,IfcSurfaceStyle,IfcSymbolStyle,IfcTextStyle));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Name = extractString(obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			base.setAttribute(obj, "Name", Name);
		}
	}
	public abstract partial class IfcProduct : IfcObject, IfcProductSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcAnnotation ,IfcElement ,IfcGrid ,IfcPort ,IfcProxy ,IfcSpatialElement ,IfcStructuralActivity ,IfcStructuralItem))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Placement", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Placement = mDatabase.parseJObject<IfcObjectPlacement>(jobj);
			jobj = obj.GetValue("Representation", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Representation = mDatabase.parseJObject<IfcProductRepresentation>(jobj);

		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			IfcObjectPlacement placement = Placement;
			if (placement != null)
				obj["Placement"] = placement.getJson(this, processed);
			IfcProductRepresentation representation = Representation;
			if (representation != null)
			{
				//IfcProductDefinitionShape shape = representation as IfcProductDefinitionShape;
				//if(shape != null)
				//{
				//	if(shape.mHasShapeAspects.Count == 1)
				//	{
				obj["Representation"] = representation.getJson(this, processed);
				//	}
				//	else
				//}
				//else
				//{
				//}
			}
			//internal List<IfcRelAssignsToProduct> mReferencedBy = new List<IfcRelAssignsToProduct>();//	 :	SET OF IfcRelAssignsToProduct FOR RelatingProduct;

		}
	}
	public partial class IfcProductDefinitionShape : IfcProductRepresentation, IfcProductRepresentationSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JArray array = obj.GetValue("ShapeOfProduct", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
			{
				foreach (JObject jobj in array)
				{
					IfcProduct product = mDatabase.parseJObject<IfcProduct>(jobj);
					if (product != null)
						mShapeOfProduct.Add(product);
				}
			}
			array = obj.GetValue("HasShapeAspects", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
			{
				List<IfcShapeAspect> aspects = mDatabase.extractJArray<IfcShapeAspect>(array);
				for (int icounter = 0; icounter < aspects.Count; icounter++)
					aspects[icounter].PartOfProductDefinitionShape = this;
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mHasShapeAspects.Count > 0)
				obj["HasShapeAspects"] = new JArray(mHasShapeAspects.ConvertAll(x => x.getJson(this, processed)));
		}
	}
	public partial class IfcProductRepresentation : BaseClassIfc //(IfcMaterialDefinitionRepresentation ,IfcProductDefinitionShape));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
			token = obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Description = token.Value<string>();
			mDatabase.extractJArray<IfcRepresentation>(obj.GetValue("Representations", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>AddRepresentation(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			string name = Name;
			if (!string.IsNullOrEmpty(name))
				obj["Name"] = name;
			string description = Description;
			if (!string.IsNullOrEmpty(description))
				obj["Description"] = description;

			ReadOnlyCollection<IfcRepresentation> representations = Representations;
			JArray jreps = new JArray();
			foreach (IfcRepresentation rep in representations)
			{
				jreps.Add(rep.getJson(this, processed));
				//				if(rep.OfProductRepresentation.Count == 1)
				//				{
				//ofshaperep
				//				}
			}
			obj["Representations"] = jreps;
		}
	}
	public partial class IfcProfileDef : BaseClassIfc, IfcResourceObjectSelect // SUPERTYPE OF (ONEOF (IfcArbitraryClosedProfileDef ,IfcArbitraryOpenProfileDef
	{  //,IfcCompositeProfileDef ,IfcDerivedProfileDef ,IfcParameterizedProfileDef));  IFC2x3 abstract 
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("ProfileType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcProfileTypeEnum>(token.Value<string>(), true, out mProfileType);
			token = obj.GetValue("ProfileName", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ProfileName = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["ProfileType"] = mProfileType.ToString();
			setAttribute(obj, "ProfileName", ProfileName);
		}
	}
	public partial class IfcProfileProperties : IfcExtendedProperties //IFC2x3 Abstract : BaseClassIfc ABSTRACT SUPERTYPE OF	(ONEOF(IfcGeneralProfileProperties, IfcRibPlateProfileProperties));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("ProfileName", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
			JObject jobj = obj.GetValue("ProfileDefinition", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				ProfileDefinition = extractObject<IfcProfileDef>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mDatabase.Release == ReleaseVersion.IFC2x3 && mName != "$")
				obj["ProfileName"] = Name;
			obj["ProfileDefinition"] = ProfileDefinition.getJson(this, processed);
		}
	}
	public partial class IfcProjectedCRS : IfcCoordinateReferenceSystem //IFC4
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			string str = MapProjection;
			if (!string.IsNullOrEmpty(str))
				obj["MapProjection"] = str;
			str = MapZone;
			if (!string.IsNullOrEmpty(str))
				obj["MapZone"] = str;
			IfcNamedUnit unit = MapUnit;
			if (unit != null)
				obj["MapUnit"] = unit.getJson(this, processed);
		}
	}
	public abstract partial class IfcProperty : IfcPropertyAbstraction  //ABSTRACT SUPERTYPE OF (ONEOF(IfcComplexProperty,IfcSimpleProperty));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
			token = obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Description = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["Name"] = Name;
			setAttribute(obj, "Description", Description);
		}
	}
	public abstract partial class IfcPropertyAbstraction : BaseClassIfc, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcExtendedProperties ,IfcPreDefinedProperties ,IfcProperty ,IfcPropertyEnumeration));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			foreach (IfcExternalReferenceRelationship r in mDatabase.extractJArray<IfcExternalReferenceRelationship>(obj.GetValue("HasExternalReferences", StringComparison.InvariantCultureIgnoreCase) as JArray))
				r.addRelated(this);
			foreach (IfcResourceConstraintRelationship r in mDatabase.extractJArray<IfcResourceConstraintRelationship>(obj.GetValue("HasConstraintRelationships", StringComparison.InvariantCultureIgnoreCase) as JArray))
				r.addRelated(this);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			createArray(obj, "HasExternalReferences", HasExternalReferences, this, processed);
			//if (mHasConstraintRelationships.Count > 0)

		}
	}
	public partial class IfcPropertyEnumeratedValue : IfcSimpleProperty
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JArray array = obj.GetValue("EnumerationValues", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
				mEnumerationValues = array.ToList().ConvertAll(x => DatabaseIfc.ParseValue(x as JObject));
			JObject jobj = obj.GetValue("EnumerationReference", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				EnumerationReference = mDatabase.parseJObject<IfcPropertyEnumeration>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["EnumerationValues"] = new JArray(EnumerationValues.ToList().ConvertAll(x => DatabaseIfc.extract(x)));
			if (mEnumerationReference > 0)
				obj["EnumerationReference"] = EnumerationReference.getJson(this, processed);
		}
	}
	public partial class IfcPropertySet : IfcPropertySetDefinition
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mDatabase.extractJArray<IfcProperty>(obj.GetValue("HasProperties", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>AddProperty(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["HasProperties"] = new JArray(mPropertyIndices.ConvertAll(x=>mDatabase[x].getJson(this,processed)));
		}
	}
	public partial class IfcPropertySetDefinition : IfcPropertyDefinition
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JArray array = obj.GetValue("DefinesType", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if(array != null)
			{
				foreach (IfcTypeObject t in mDatabase.extractJArray<IfcTypeObject>(array))
					t.AddPropertySet(this);
			}
			array = obj.GetValue("IsDefinedBy", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if(array != null)
			{
				foreach (IfcRelDefinesByTemplate r in mDatabase.extractJArray<IfcRelDefinesByTemplate>(array))
					r.AddRelated(this);
			}
			array = obj.GetValue("DefinesOccurrence", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if(array != null)
			{
				foreach (IfcRelDefinesByProperties r in mDatabase.extractJArray<IfcRelDefinesByProperties>(array))
					r.RelatingPropertyDefinition = this;
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mIsDefinedBy.Count > 0)
				obj["IsDefinedBy"] = new JArray(IsDefinedBy.ToList().ConvertAll(x => x.getJson(this, processed)));
		}
	}
	public partial class IfcPropertySetTemplate : IfcPropertyTemplateDefinition
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("TemplateType", StringComparison.InvariantCultureIgnoreCase);
			if(token != null)
				Enum.TryParse<IfcPropertySetTemplateTypeEnum>(token.Value<string>(), true, out mTemplateType);
			token = obj.GetValue("ApplicableEntity", StringComparison.InvariantCultureIgnoreCase);
			if(token != null)
				ApplicableEntity = token.Value<string>();
			mDatabase.extractJArray<IfcPropertyTemplate>(obj.GetValue("HasPropertyTemplates", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>addPropertyTemplate(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mTemplateType != IfcPropertySetTemplateTypeEnum.NOTDEFINED)
				obj["TemplateType"] = mTemplateType.ToString();
			setAttribute(obj, "ApplicableEntity", ApplicableEntity);
			obj["HasPropertyTemplates"] = new JArray( HasPropertyTemplates.ToList().ConvertAll(x=>x.getJson(this, processed)));
		}
	}
	public partial class IfcPropertySingleValue : IfcSimpleProperty
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("NominalValue", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				NominalValue = DatabaseIfc.ParseValue(jobj);
			JObject jo = obj.GetValue("Unit", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jo != null)
				Unit = mDatabase.parseJObject<IfcUnit>(jo);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			IfcValue value = NominalValue;
			if (value != null)
				obj["NominalValue"] = DatabaseIfc.extract(value);
			IfcUnit unit = Unit;
			if (unit != null)
				obj["Unit"] = mDatabase[mUnit].getJson(this, processed);
		}
	}
	public partial class IfcPumpType : IfcFlowMovingDeviceType
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcPumpTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPredefinedType != IfcPumpTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
}
