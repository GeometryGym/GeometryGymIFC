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

using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public abstract partial class IfcParameterizedProfileDef : IfcProfileDef //ABSTRACT SUPERTYPE OF (ONEOF (IfcCShapeProfileDef ,IfcCircleProfileDef ,IfcCraneRailAShapeProfileDef ,IfcCraneRailFShapeProfileDef ,
	{//IfcEllipseProfileDef ,IfcIShapeProfileDef ,IfcLShapeProfileDef ,IfcRectangleProfileDef ,IfcTShapeProfileDef ,IfcTrapeziumProfileDef ,IfcUShapeProfileDef ,IfcZShapeProfileDef))*/
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Position", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj == null)
				Position = mDatabase.Factory.Origin2dPlace;
			else
				Position = mDatabase.parseJObject<IfcAxis2Placement2D>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPosition != null)
			{
				IfcAxis2Placement2D position = Position;
				if((mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4 )|| !position.IsXYPlane)
					obj["Position"] = Position.getJson(this, options);
			}
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
			Roles.AddRange(mDatabase.extractJArray<IfcActorRole>(obj.GetValue("Roles", StringComparison.InvariantCultureIgnoreCase) as JArray));
			Addresses.AddRange(mDatabase.extractJArray<IfcAddress>(obj.GetValue("Addresses", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
				JArray array = new JArray();
				IEnumerable<string> middleNames = MiddleNames;
				foreach (string name in middleNames)
					array.Add(name);
				obj["MiddleNames"] = array;
			}
			LIST<IfcActorRole> roles = Roles;
			if (roles.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcActorRole role in roles)
					array.Add(role.getJson(this, options));
				obj["Roles"] = array;
			}
			LIST<IfcAddress> addresses = Addresses;
			if (addresses.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcAddress address in addresses)
					array.Add(address.getJson(this, options));
				obj["Addresses"] = array;
			}
		}

	}
	public partial class IfcPersonAndOrganization : BaseClassIfc, IfcActorSelect, IfcResourceObjectSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("ThePerson", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ThePerson = mDatabase.parseJObject<IfcPerson>(token as JObject);
			token = obj.GetValue("TheOrganization", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				TheOrganization = mDatabase.parseJObject<IfcOrganization>(token as JObject);
			Roles.AddRange(mDatabase.extractJArray<IfcActorRole>(obj.GetValue("Roles", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ThePerson"] = ThePerson.getJson(this, options);
			obj["TheOrganization"] = TheOrganization.getJson(this, options);
			LIST<IfcActorRole> roles = Roles;
			if (roles.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcActorRole role in roles)
					array.Add(role.getJson(this, options));
				obj["Roles"] = array;
			}
		}
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
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
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
					Location = mDatabase.parseJObject<IfcCartesianPoint>(jobj);
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Location"] = Location.getJson(this, options);
		}
	}
	public partial class IfcPolyline : IfcBoundedCurve
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Points = new LIST<IfcCartesianPoint>(mDatabase.extractJArray<IfcCartesianPoint>(obj.GetValue("Points", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Points"] = new JArray(Points.ConvertAll(x => x.getJson(this, options)));
		}
	}
	public partial class IfcPolyloop : IfcLoop
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mPolygon.AddRange(mDatabase.extractJArray<IfcCartesianPoint>(obj.GetValue("Polygon", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Polygon"] = new JArray(mPolygon.ConvertAll(x => x.getJson(this, options)));
		}
	}
	public partial class IfcPresentationLayerAssignment : BaseClassIfc //SUPERTYPE OF	(IfcPresentationLayerWithStyle);
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Name = extractString(obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase));
			Description = extractString(obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase));
			AssignedItems.AddRange(mDatabase.extractJArray<IfcLayeredItem>(obj.GetValue("AssignedItems", StringComparison.InvariantCultureIgnoreCase) as JArray));
			Identifier = extractString(obj.GetValue("Identifier", StringComparison.InvariantCultureIgnoreCase));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
			mDatabase.extractJArray<IfcPresentationStyle>(obj.GetValue("LayerStyles", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addLayerStyle(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["LayerOn"] = mLayerOn.ToString();
			obj["LayeyFrozen"] = mLayerFrozen.ToString();
			obj["LayerBlocked"] = mLayerBlocked.ToString();
			if (mLayerStyles.Count > 0)
				obj["LayerStyles"] = new JArray(LayerStyles.ToList().ConvertAll(x => x.getJson(this, options)));
		}
	}
	public abstract partial class IfcPresentationStyle : BaseClassIfc, IfcStyleAssignmentSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcCurveStyle,IfcFillAreaStyle,IfcSurfaceStyle,IfcSymbolStyle,IfcTextStyle));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Name = extractString(obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "Name", Name);
		}
	}

	public partial class IfcPresentationStyleAssignment : BaseClassIfc, IfcStyleAssignmentSelect //DEPRECEATED IFC4
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			foreach (IfcPresentationStyleSelect sas in mDatabase.extractJArray<IfcPresentationStyleSelect>(obj.GetValue("Styles", StringComparison.InvariantCultureIgnoreCase) as JArray))
				addStyle(sas);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JArray array = new JArray();
			foreach (int style in mStyles)
				array.Add(mDatabase[style].getJson(this, options));
			obj["Styles"] = array;
		}
	}
	public abstract partial class IfcProduct : IfcObject, IfcProductSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcAnnotation ,IfcElement ,IfcGrid ,IfcPort ,IfcProxy ,IfcSpatialElement ,IfcStructuralActivity ,IfcStructuralItem))
	{
		internal override void parseJObject(JObject obj)
		{
			JObject jobj = obj.GetValue("Placement", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Placement = mDatabase.parseJObject<IfcObjectPlacement>(jobj);
			base.parseJObject(obj);
			jobj = obj.GetValue("Representation", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Representation = mDatabase.parseJObject<IfcProductRepresentation>(jobj);

		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			IfcObjectPlacement placement = Placement;
			JObject placementObj = null;	
			if (placement != null)
			{
				if (string.IsNullOrEmpty(placement.mGlobalId))
					placement.mGlobalId = ParserIfc.EncodeGuid(Guid.NewGuid());
				placementObj = placement.getJson(this, options);
			}
			base.setJSON(obj, host, options);

			if(placementObj != null)
				obj["Placement"] = placementObj;
			if (options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				IfcProductRepresentation representation = Representation;
				if (representation != null)
					obj["Representation"] = representation.getJson(this, options);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mHasShapeAspects.Count > 0)
				obj["HasShapeAspects"] = new JArray(mHasShapeAspects.ConvertAll(x => x.getJson(this, options)));
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
			Representations.AddRange(mDatabase.extractJArray<IfcRepresentation>(obj.GetValue("Representations", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			string name = Name;
			if (!string.IsNullOrEmpty(name))
				obj["Name"] = name;
			string description = Description;
			if (!string.IsNullOrEmpty(description))
				obj["Description"] = description;

			obj["Representations"] = new JArray(Representations.ConvertAll(x => x.getJson(this, options)));
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
			
			HasExternalReferences.AddRange(mDatabase.extractJArray<IfcExternalReferenceRelationship>(obj.GetValue("HasExternalReferences", StringComparison.InvariantCultureIgnoreCase) as JArray));
			HasProperties.AddRange(mDatabase.extractJArray<IfcProfileProperties>(obj.GetValue("HasProperties", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ProfileType"] = mProfileType.ToString();
			setAttribute(obj, "ProfileName", ProfileName);

			if (mDatabase == null || mDatabase.Release >= ReleaseVersion.IFC2x3)
			{
				JArray jArray = new JArray();
				foreach (IfcExternalReferenceRelationship r in mHasExternalReferences)
				{
					if (r == host)
						continue;
					jArray.Add(r.getJson(this, options));
				}
				if (jArray.Count > 0)
					obj["HasExternalReferences"] = jArray;


				jArray = new JArray();
				foreach (IfcProfileProperties p in mHasProperties)
					jArray.Add(p.getJson(this, options));
				if (jArray.Count > 0)
					obj["HasProperties"] = jArray;
			}
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
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mDatabase.Release < ReleaseVersion.IFC4 && mName != "$")
				obj["ProfileName"] = Name;
			obj["ProfileDefinition"] = ProfileDefinition.getJson(this, options);
		}
	}
	public partial class IfcProjectedCRS : IfcCoordinateReferenceSystem //IFC4
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
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
				r.RelatedResourceObjects.Add(this);
			foreach (IfcResourceConstraintRelationship r in mDatabase.extractJArray<IfcResourceConstraintRelationship>(obj.GetValue("HasConstraintRelationships", StringComparison.InvariantCultureIgnoreCase) as JArray))
				r.addRelated(this);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "HasExternalReferences", HasExternalReferences, this, options);
			//if (mHasConstraintRelationships.Count > 0)

		}
	}
	public partial class IfcPropertyBoundedValue : IfcSimpleProperty
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mUpperBoundValue != null)
				obj["UpperBoundValue"] = DatabaseIfc.extract(UpperBoundValue);
			if (mLowerBoundValue != null)
				obj["LowerBoundValue"] = DatabaseIfc.extract(LowerBoundValue);
			if (mUnit > 0)
				obj["Unit"] = mDatabase[mUnit].getJson(this, options);
			if (mSetPointValue != null)
				obj["SetPointValue"] = DatabaseIfc.extract(SetPointValue);
		}
	}
	public partial class IfcPropertyBoundedValue<T> : IfcSimpleProperty where T : IfcValue
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mUpperBoundValue != null)
				obj["UpperBoundValue"] = DatabaseIfc.extract(UpperBoundValue);
			if (mLowerBoundValue != null)
				obj["LowerBoundValue"] = DatabaseIfc.extract(LowerBoundValue);
			if (mUnit > 0)
				obj["Unit"] = mDatabase[mUnit].getJson(this, options);
			if (mSetPointValue != null)
				obj["SetPointValue"] = DatabaseIfc.extract(SetPointValue);
		}
	}
	public abstract partial class IfcPropertyDefinition : IfcRoot, IfcDefinitionSelect //(IfcPropertySetDefinition, IfcPropertyTemplateDefinition)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			foreach (IfcRelAssociates associates in mDatabase.extractJArray<IfcRelAssociates>(obj.GetValue("HasAssociations", StringComparison.InvariantCultureIgnoreCase) as JArray)) 
				mHasAssociations.Add(associates);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mHasAssociations.Count > 0)
				obj["HasAssociations"] = new JArray(mHasAssociations.ConvertAll(x => x.getJson(this, options)));
		}
	}
	public partial class IfcPropertyEnumeratedValue : IfcSimpleProperty
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JArray array = obj.GetValue("EnumerationValues", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
				mEnumerationValues.AddRange(array.Select(x => DatabaseIfc.ParseValue(x as JObject)));
			JObject jobj = obj.GetValue("EnumerationReference", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				EnumerationReference = mDatabase.parseJObject<IfcPropertyEnumeration>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["EnumerationValues"] = new JArray(EnumerationValues.ToList().ConvertAll(x => DatabaseIfc.extract(x)));
			if (mEnumerationReference > 0)
				obj["EnumerationReference"] = EnumerationReference.getJson(this, options);
		}
	}
	public partial class IfcPropertySet : IfcPropertySetDefinition
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mDatabase.extractJArray<IfcProperty>(obj.GetValue("HasProperties", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>addProperty(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["HasProperties"] = new JArray(mPropertyIndices.ConvertAll(x=>mDatabase[x].getJson(this, options)));
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
					t.HasPropertySets.Add(this);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mIsDefinedBy.Count > 0)
				obj["IsDefinedBy"] = new JArray(IsDefinedBy.ToList().ConvertAll(x => x.getJson(this, options)));
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
			mDatabase.extractJArray<IfcPropertyTemplate>(obj.GetValue("HasPropertyTemplates", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>AddPropertyTemplate(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mTemplateType != IfcPropertySetTemplateTypeEnum.NOTDEFINED)
				obj["TemplateType"] = mTemplateType.ToString();
			setAttribute(obj, "ApplicableEntity", ApplicableEntity);
			obj["HasPropertyTemplates"] = new JArray( HasPropertyTemplates.Values.ToList().ConvertAll(x=>x.getJson(this, options)));
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
			jobj = obj.GetValue("Unit", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Unit = mDatabase.parseJObject<IfcUnit>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			IfcValue value = NominalValue;
			if (value != null)
				obj["NominalValue"] = DatabaseIfc.extract(value);
			IfcUnit unit = Unit;
			if (unit != null)
				obj["Unit"] = mDatabase[mUnit].getJson(this, options);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcPumpTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
}
