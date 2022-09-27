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
using GeometryGym.STEP;

#if (NET || !NOIFCJSON)
#if (NEWTONSOFT)
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonObject = Newtonsoft.Json.Linq.JObject;
using JsonArray = Newtonsoft.Json.Linq.JArray;
#else
using System.Text.Json.Nodes;
#endif

namespace GeometryGym.Ifc
{
	public abstract partial class IfcParameterizedProfileDef 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Position"] as JsonObject;
			if (jobj == null)
				Position = mDatabase.Factory.Origin2dPlace;
			else
				Position = mDatabase.ParseJsonObject<IfcAxis2Placement2D>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPosition != null)
			{
				IfcAxis2Placement2D position = Position;
				if((mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4 )|| !position.IsXYPlane(mDatabase.Tolerance))
					obj["Position"] = Position.getJson(this, options);
			}
		}
	}
	public partial class IfcPavement
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcPavementTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcPavementTypeEnum>(node.GetValue<string>(), out mPredefinedType);
		}
	}
	public partial class IfcPavementType
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcPavementTypeEnum>(node.GetValue<string>(), out mPredefinedType);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcPavementTypeEnum>(node.GetValue<string>(), out mPredefinedType);
		}
	}
	public partial class IfcPerson
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Identification"];
			if (node != null)
				Identification = node.GetValue<string>();
			node = obj["FamilyName"];
			if (node != null)
				FamilyName = node.GetValue<string>();
			node = obj["GivenName"];
			if (node != null)
				GivenName = node.GetValue<string>();
			JsonArray array = obj["MiddleName"] as JsonArray;
			if (array != null)
			{
				foreach (var item in array)
					MiddleNames.Add(item.GetValue<string>());
			}
			
			Roles.AddRange(mDatabase.extractJsonArray<IfcActorRole>(obj["Roles"] as JsonArray));
			Addresses.AddRange(mDatabase.extractJsonArray<IfcAddress>(obj["Addresses"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
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
				JsonArray array = new JsonArray();
				IEnumerable<string> middleNames = MiddleNames;
				foreach (string name in middleNames)
					array.Add(name);
				obj["MiddleNames"] = array;
			}
			LIST<IfcActorRole> roles = Roles;
			if (roles.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcActorRole role in roles)
					array.Add(role.getJson(this, options));
				obj["Roles"] = array;
			}
			LIST<IfcAddress> addresses = Addresses;
			if (addresses.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcAddress address in addresses)
					array.Add(address.getJson(this, options));
				obj["Addresses"] = array;
			}
		}

	}
	public partial class IfcPersonAndOrganization
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["ThePerson"];
			if (node != null)
				ThePerson = mDatabase.ParseJsonObject<IfcPerson>(node as JsonObject);
			node = obj["TheOrganization"];
			if (node != null)
				TheOrganization = mDatabase.ParseJsonObject<IfcOrganization>(node as JsonObject);
			Roles.AddRange(mDatabase.extractJsonArray<IfcActorRole>(obj["Roles"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ThePerson"] = ThePerson.getJson(this, options);
			obj["TheOrganization"] = TheOrganization.getJson(this, options);
			LIST<IfcActorRole> roles = Roles;
			if (roles.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcActorRole role in roles)
					array.Add(role.getJson(this, options));
				obj["Roles"] = array;
			}
		}
	}
	public abstract partial class IfcPhysicalQuantity
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Name"];
			if (node != null)
				Name = node.GetValue<string>();
			node = obj["Description"];
			if (node != null)
				Description = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Name"] = Name;
			setAttribute(obj, "Description", Description);
		}
	}
	public partial class IfcPipeFittingType
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcPipeFittingTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcPipeFittingTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public abstract partial class IfcPlacement
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Location"];
			if (node != null)
			{
				JsonObject jobj = node as JsonObject;
				if (jobj != null)
					mLocation = mDatabase.ParseJsonObject<IfcPoint>(jobj);
			}
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Location"] = mLocation.getJson(this, options);
		}
	}
	public partial class IfcPolyline
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Points = new LIST<IfcCartesianPoint>(mDatabase.extractJsonArray<IfcCartesianPoint>(obj["Points"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "Points", Points, this, options);
		}
	}
	public partial class IfcPolyLoop
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mPolygon.AddRange(mDatabase.extractJsonArray<IfcCartesianPoint>(obj["Polygon"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "Polygon", Polygon, this, options);
		}
	}
	public partial class IfcPolynomialCurve
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Position = mDatabase.ParseJsonObject<IfcPlacement>(obj);
		}
	}
	public partial class IfcPresentationLayerAssignment
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Name = extractString(obj["Name"]);
			Description = extractString(obj["Description"]);
			AssignedItems.AddRange(mDatabase.extractJsonArray<IfcLayeredItem>(obj["AssignedItems"] as JsonArray));
			Identifier = extractString(obj["Identifier"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			//XmlElement element = xml.OwnerDocument.CreateElement("AssignedItems");
			//xml.AppendChild(element);
			//foreach (int item in mAssignedItems)
			//	element.AppendChild(mDatabase[item].GetXML(xml.OwnerDocument, "", this, processed));
			setAttribute(obj, "Identifier", Identifier);
		}
	}
	public partial class IfcPresentationLayerWithStyle
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["LayerOn"];
			if (node != null)
				Enum.TryParse<IfcLogicalEnum>(node.GetValue<string>(), true, out mLayerOn);
			node = obj["LayerFrozen"];
			if (node != null)
				Enum.TryParse<IfcLogicalEnum>(node.GetValue<string>(), true, out mLayerFrozen);
			node = obj["LayerBlocked"];
			if (node != null)
				Enum.TryParse<IfcLogicalEnum>(node.GetValue<string>(), true, out mLayerBlocked);
			LayerStyles.AddRange(mDatabase.extractJsonArray<IfcPresentationStyle>(obj["LayerStyles"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["LayerOn"] = mLayerOn.ToString();
			obj["LayeyFrozen"] = mLayerFrozen.ToString();
			obj["LayerBlocked"] = mLayerBlocked.ToString();
			createArray(obj, "LayerStyles", LayerStyles, this, options);
		}
	}
	public abstract partial class IfcPresentationStyle
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Name = extractString(obj["Name"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "Name", Name);
		}
	}

	public partial class IfcPresentationStyleAssignment
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			foreach (IfcPresentationStyleSelect sas in mDatabase.extractJsonArray<IfcPresentationStyleSelect>(obj["Styles"] as JsonArray))
				Styles.Add(sas);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			foreach (IfcPresentationStyleSelect style in mStyles)
				array.Add(style.getJson(this, options));
			obj["Styles"] = array;
		}
	}
	public abstract partial class IfcProduct
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			JsonObject jobj = obj["ObjectPlacement"] as JsonObject;
			if (jobj == null)
				jobj = obj["Placement"] as JsonObject;
			if(jobj != null)
				ObjectPlacement = mDatabase.ParseJsonObject<IfcObjectPlacement>(jobj);
			base.parseJsonObject(obj);
			jobj = obj["Representation"] as JsonObject;
			if (jobj != null)
				Representation = mDatabase.ParseJsonObject<IfcProductDefinitionShape>(jobj);

		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			IfcObjectPlacement placement = ObjectPlacement;
			JsonObject placementObj = null;	
			if (placement != null)
			{
				if (string.IsNullOrEmpty(placement.mGlobalId))
					placement.setGlobalId(ParserIfc.EncodeGuid(Guid.NewGuid()));
				placementObj = placement.getJson(this, options);
			}
			base.setJSON(obj, host, options);

			if(placementObj != null)
				obj["ObjectPlacement"] = placementObj;
			if (options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				IfcProductDefinitionShape representation = Representation;
				if (representation != null)
					obj["Representation"] = representation.getJson(this, options);
			}
			//internal List<IfcRelAssignsToProduct> mReferencedBy = new List<IfcRelAssignsToProduct>();//	 :	SET OF IfcRelAssignsToProduct FOR RelatingProduct;

		}
	}
	public partial class IfcProductDefinitionShape
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonArray array = obj["ShapeOfProduct"] as JsonArray;
			if (array != null)
			{
				foreach (JsonObject jobj in array)
				{
					IfcProduct product = mDatabase.ParseJsonObject<IfcProduct>(jobj);
					if (product != null)
						mShapeOfProduct.Add(product);
				}
			}
			array = obj["HasShapeAspects"] as JsonArray;
			if (array != null)
			{
				List<IfcShapeAspect> aspects = mDatabase.extractJsonArray<IfcShapeAspect>(array);
				for (int icounter = 0; icounter < aspects.Count; icounter++)
					aspects[icounter].PartOfProductDefinitionShape = this;
			}
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "HasShapeAspects", HasShapeAspects, this, options);
		}
	}
	public partial class IfcProductRepresentation<Representation, RepresentationItem> 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Name"];
			if (node != null)
				Name = node.GetValue<string>();
			node = obj["Description"];
			if (node != null)
				Description = node.GetValue<string>();
			Representations.AddRange(mDatabase.extractJsonArray<Representation>(obj["Representations"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			string name = Name;
			if (!string.IsNullOrEmpty(name))
				obj["Name"] = name;
			string description = Description;
			if (!string.IsNullOrEmpty(description))
				obj["Description"] = description;

			createArray(obj, "Representations", Representations, this, options);
		}
	}
	public partial class IfcProfileDef 
	{   
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["ProfileType"];
			if (node != null)
				Enum.TryParse<IfcProfileTypeEnum>(node.GetValue<string>(), true, out mProfileType);
			node = obj["ProfileName"];
			if (node != null)
				ProfileName = node.GetValue<string>();
			
			HasExternalReference.AddRange(mDatabase.extractJsonArray<IfcExternalReferenceRelationship>(obj["HasExternalReference"] as JsonArray));
			HasProperties.AddRange(mDatabase.extractJsonArray<IfcProfileProperties>(obj["HasProperties"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ProfileType"] = mProfileType.ToString();
			setAttribute(obj, "ProfileName", ProfileName);

			if (mDatabase == null || mDatabase.Release >= ReleaseVersion.IFC2x3)
			{
				JsonArray JsonArray = new JsonArray();
				foreach (IfcExternalReferenceRelationship r in mHasExternalReference)
				{
					if (r == host)
						continue;
					JsonArray.Add(r.getJson(this, options));
				}
				if (JsonArray.Count > 0)
					obj["HasExternalReference"] = JsonArray;


				JsonArray = new JsonArray();
				foreach (IfcProfileProperties p in mHasProperties)
					JsonArray.Add(p.getJson(this, options));
				if (JsonArray.Count > 0)
					obj["HasProperties"] = JsonArray;
			}
		}
	}
	public partial class IfcProfileProperties 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["ProfileName"];
			if (node != null)
				Name = node.GetValue<string>();
			JsonObject jobj = obj["ProfileDefinition"] as JsonObject;
			if (jobj != null)
				ProfileDefinition = extractObject<IfcProfileDef>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (options.Version > ReleaseVersion.IFC2x3)
				setAttribute(obj, "Name", Name);
			obj["ProfileDefinition"] = ProfileDefinition.getJson(this, options);
		}
	}
	public partial class IfcProjectedCRS 
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			string str = MapProjection;
			if (!string.IsNullOrEmpty(str))
				obj["MapProjection"] = str;
			str = MapZone;
			if (!string.IsNullOrEmpty(str))
				obj["MapZone"] = str;
			IfcNamedUnit unit = MapUnit;
			if (unit != null)
				obj["MapUnit"] = unit.getJson(this, options);
		}
	}
	public abstract partial class IfcProperty
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Name"];
			if (node != null)
				Name = node.GetValue<string>();
			node = obj["Specification"];
			if(node == null)
				node = obj["Description"];
			if (node != null)
				Specification = node.GetValue<string>();
			foreach (IfcPropertySet pset in mDatabase.extractJsonArray<IfcPropertySet>(obj["PartOfPset"] as JsonArray))
				pset.addProperty(this);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Name"] = Name;
			setAttribute(obj, "Specification", Specification);
			if(host is IfcExtendedProperties)
			{
				List<IfcPropertySet> psets = mPartOfPset.Where(x=>x.mDefinesType.Count == 0 && x.DefinesOccurrence.Count==0).ToList();
				if(psets.Count > 0)
					createArray(obj, "PartOfPset", psets, this, options);
			}
		}
	}
	public abstract partial class IfcPropertyAbstraction 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			foreach (IfcExternalReferenceRelationship r in mDatabase.extractJsonArray<IfcExternalReferenceRelationship>(obj["HasExternalReference"] as JsonArray))
				r.RelatedResourceObjects.Add(this);
			foreach (IfcResourceConstraintRelationship r in mDatabase.extractJsonArray<IfcResourceConstraintRelationship>(obj["HasConstraintRelationships"] as JsonArray))
				r.RelatedResourceObjects.Add(this);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "HasExternalReference", HasExternalReference, this, options);
			//if (mHasConstraintRelationships.Count > 0)

		}
	}
	public partial class IfcPropertyBoundedValue
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mUpperBoundValue != null)
				obj["UpperBoundValue"] = DatabaseIfc.extract(UpperBoundValue);
			if (mLowerBoundValue != null)
				obj["LowerBoundValue"] = DatabaseIfc.extract(LowerBoundValue);
			if (mUnit is BaseClassIfc o)
				obj["Unit"] = o.getJson(this, options);
			if (mSetPointValue != null)
				obj["SetPointValue"] = DatabaseIfc.extract(SetPointValue);
		}
	}
	public partial class IfcPropertyBoundedValue<T>
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mUpperBoundValue != null)
				obj["UpperBoundValue"] = DatabaseIfc.extract(UpperBoundValue);
			if (mLowerBoundValue != null)
				obj["LowerBoundValue"] = DatabaseIfc.extract(LowerBoundValue);
			if (mUnit is BaseClassIfc o)
				obj["Unit"] = o.getJson(this, options);
			if (mSetPointValue != null)
				obj["SetPointValue"] = DatabaseIfc.extract(SetPointValue);
		}
	}
	public abstract partial class IfcPropertyDefinition
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			foreach (IfcRelAssociates associates in mDatabase.extractJsonArray<IfcRelAssociates>(obj["HasAssociations"] as JsonArray)) 
				mHasAssociations.Add(associates);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "HasAssociations", HasAssociations, this, options);
		}
	}
	public partial class IfcPropertyEnumeratedValue
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonArray array = obj["EnumerationValues"] as JsonArray;
			if (array != null)
				mEnumerationValues.AddRange(array.Select(x => DatabaseIfc.ParseValue(x as JsonObject)));
			JsonObject jobj = obj["EnumerationReference"] as JsonObject;
			if (jobj != null)
				EnumerationReference = mDatabase.ParseJsonObject<IfcPropertyEnumeration>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "EnumerationValues", EnumerationValues.Select(x=>DatabaseIfc.extract(x)));
			if (mEnumerationReference != null)
				obj["EnumerationReference"] = EnumerationReference.getJson(this, options);
		}
	}
	public partial class IfcPropertySet 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mDatabase.extractJsonArray<IfcProperty>(obj["HasProperties"] as JsonArray).ForEach(x=>addProperty(x));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "HasProperties", mHasProperties.Values, this, options);
		}
	}
	public partial class IfcPropertySetDefinition
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonArray array = obj["DefinesType"] as JsonArray;
			if(array != null)
			{
				foreach (IfcTypeObject t in mDatabase.extractJsonArray<IfcTypeObject>(array))
					t.HasPropertySets.Add(this);
			}
			array = obj["IsDefinedBy"] as JsonArray;
			if(array != null)
			{
				foreach (IfcRelDefinesByTemplate r in mDatabase.extractJsonArray<IfcRelDefinesByTemplate>(array))
					r.RelatedPropertySets.Add(this);
			}
			array = obj["DefinesOccurrence"] as JsonArray;
			if(array != null)
			{
				foreach (IfcRelDefinesByProperties r in mDatabase.extractJsonArray<IfcRelDefinesByProperties>(array))
					r.RelatingPropertyDefinition.Add(this);
			}
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "IsDefinedBy", IsDefinedBy, this, options);
		}
	}
	public partial class IfcPropertySetTemplate
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["TemplateType"];
			if(node != null)
				Enum.TryParse<IfcPropertySetTemplateTypeEnum>(node.GetValue<string>(), true, out mTemplateType);
			node = obj["ApplicableEntity"];
			if(node != null)
				ApplicableEntity = node.GetValue<string>();
			mDatabase.extractJsonArray<IfcPropertyTemplate>(obj["HasPropertyTemplates"] as JsonArray).ForEach(x=>AddPropertyTemplate(x));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mTemplateType != IfcPropertySetTemplateTypeEnum.NOTDEFINED)
				obj["TemplateType"] = mTemplateType.ToString();
			setAttribute(obj, "ApplicableEntity", ApplicableEntity);
			createArray(obj, "HasPropertyTemplates", mHasPropertyTemplates.Values, this, options);
		}
	}
	public partial class IfcPropertySingleValue
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["NominalValue"] as JsonObject;
			if (jobj != null)
				NominalValue = DatabaseIfc.ParseValue(jobj);
			jobj = obj["Unit"] as JsonObject;
			if (jobj != null)
				Unit = mDatabase.ParseJsonObject<IfcUnit>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			IfcValue value = NominalValue;
			if (value != null)
				obj["NominalValue"] = DatabaseIfc.extract(value);
			IfcUnit unit = Unit;
			if (unit != null)
				obj["Unit"] = unit.getJson(this, options);
		}
	}
	public partial class IfcPumpType
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcPumpTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcPumpTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
}
#endif
