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

using Newtonsoft.Json.Linq;

namespace GeometryGym.Ifc
{
	public partial class IfcEdge : IfcTopologicalRepresentationItem //SUPERTYPE OF(ONEOF(IfcEdgeCurve, IfcOrientedEdge, IfcSubedge))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("EdgeStart", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				EdgeStart = mDatabase.parseJObject<IfcVertex>(jobj);
			jobj = obj.GetValue("EdgeEnd", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				EdgeEnd = mDatabase.parseJObject<IfcVertex>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["EdgeStart"] = mDatabase[mEdgeStart].getJson(this, options);
			obj["EdgeEnd"] = mDatabase[mEdgeEnd].getJson(this, options);
		}
	}
	public abstract partial class IfcElement : IfcProduct, IfcStructuralActivityAssignmentSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcBuildingElement,IfcCivilElement
	{ //,IfcDistributionElement,IfcElementAssembly,IfcElementComponent,IfcFeatureElement,IfcFurnishingElement,IfcGeographicElement,IfcTransportElement ,IfcVirtualElement,IfcElectricalElement SS,IfcEquipmentElement SS)) 
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Tag", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Tag = token.Value<string>();
			foreach (IfcRelVoidsElement voids in mDatabase.extractJArray<IfcRelVoidsElement>(obj.GetValue("HasOpenings", StringComparison.InvariantCultureIgnoreCase) as JArray))
				voids.RelatingBuildingElement = this;
			foreach (IfcRelConnectsStructuralActivity rcsa in mDatabase.extractJArray<IfcRelConnectsStructuralActivity>(obj.GetValue("AssignedStructuralActivity", StringComparison.InvariantCultureIgnoreCase) as JArray))
				rcsa.RelatingElement = this;
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			string tag = Tag;
			if (!string.IsNullOrEmpty(tag))
				obj["Tag"] = tag;
			if (mHasOpenings.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelVoidsElement rv in HasOpenings)
				{
					if (rv.mIndex != host.mIndex)
						array.Add(rv.getJson(this, options));
				}
				if (array.Count > 0)
					obj["HasOpenings"] = array;
			}
		}
	}
	public partial class IfcElementQuantity : IfcQuantitySet
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("MethodOfMeasurement", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				MethodOfMeasurement = token.Value<string>();
			mDatabase.extractJArray<IfcPhysicalQuantity>(obj.GetValue("Quantities", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addQuantity(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "MethodOfMeasurement", MethodOfMeasurement);
			obj["Quantities"] = new JArray(Quantities.Values.Select(x => x.getJson(this, options)));
		}
	}
	public partial class IfcElementAssembly : IfcElement
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcElementAssemblyTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcElementAssemblyTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public abstract partial class IfcElementarySurface : IfcSurface //	ABSTRACT SUPERTYPE OF(ONEOF(IfcCylindricalSurface, IfcPlane))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Position = extractObject<IfcAxis2Placement3D>(obj.GetValue("Position", StringComparison.InvariantCultureIgnoreCase) as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Position"] = Position.getJson(this, options);
		}
	}
	public abstract partial class IfcElementType : IfcTypeProduct //ABSTRACT SUPERTYPE OF(ONEOF(IfcBuildingElementType, IfcDistributionElementType, IfcElementAssemblyType, IfcElementComponentType, IfcFurnishingElementType, IfcGeographicElementType, IfcTransportElementType))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("ElementType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mElementType = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "ElementType", ElementType);
		}
	}
	public partial class IfcEllipseProfileDef : IfcParameterizedProfileDef
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			SemiAxis1 = obj.GetValue("SemiAxis1", StringComparison.InvariantCultureIgnoreCase).Value<double>();
			SemiAxis2 = obj.GetValue("SemiAxis2", StringComparison.InvariantCultureIgnoreCase).Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["SemiAxis1"] = SemiAxis1;
			obj["SemiAxis2"] = SemiAxis2;
		}
	}
	public abstract partial class IfcExtendedProperties : IfcPropertyAbstraction //IFC4 ABSTRACT SUPERTYPE OF (ONEOF (IfcMaterialProperties,IfcProfileProperties))
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
			mDatabase.extractJArray<IfcProperty>(obj.GetValue("Properties", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>AddProperty(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if(mDatabase.Release != ReleaseVersion.IFC2x3)
				base.setAttribute(obj, "Name", Name);
			base.setAttribute(obj, "Description", Description);
			if(mProperties.Count > 0)
			{
				JArray array = new JArray();
				foreach (int i in mPropertyIndices)
					array.Add(mDatabase[i].getJson(this, options));
				obj["Properties"] = array;
			}
		}
	}
	public abstract partial class IfcExternalReference : BaseClassIfc, IfcLightDistributionDataSourceSelect, IfcResourceObjectSelect//ABSTRACT SUPERTYPE OF (ONEOF (IfcClassificationReference ,IfcDocumentReference ,IfcExternallyDefinedHatchStyle
	{ //,IfcExternallyDefinedSurfaceStyle ,IfcExternallyDefinedSymbol ,IfcExternallyDefinedTextFont ,IfcLibraryReference)); 
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Location = extractString(obj.GetValue("Location", StringComparison.InvariantCultureIgnoreCase));
			Identification = extractString(obj.GetValue("Identification", StringComparison.InvariantCultureIgnoreCase));
			Name = extractString(obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase));
			foreach (IfcExternalReferenceRelationship r in mDatabase.extractJArray<IfcExternalReferenceRelationship>(obj.GetValue("HasExternalReferences", StringComparison.InvariantCultureIgnoreCase) as JArray))
				r.RelatedResourceObjects.Add(this);
			foreach (IfcResourceConstraintRelationship r in mDatabase.extractJArray<IfcResourceConstraintRelationship>(obj.GetValue("HasConstraintRelationships", StringComparison.InvariantCultureIgnoreCase) as JArray))
				r.addRelated(this);
			//foreach (IfcExternalReferenceRelationship r in mDatabase.extractJArray<IfcExternalReferenceRelationship>(obj.GetValue("ExternalReferenceForResources", StringComparison.InvariantCultureIgnoreCase) as JArray))
			//	r.addRelated(this);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Location", Location);
			setAttribute(obj, "Identification", Identification);
			setAttribute(obj, "Name", Name);
			createArray(obj, "HasExternalReferences", HasExternalReferences, this, options);
			createArray(obj, "HasConstraintRelationships", HasConstraintRelationships, this, options);
			createArray(obj, "ExternalReferenceForResources", ExternalReferenceForResources, this, options);
		}
	}

	public partial class IfcExternalReferenceRelationship : IfcResourceLevelRelationship //IFC4
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RelatingReference = mDatabase.parseJObject<IfcExternalReference>(obj.GetValue("RelatingReference", StringComparison.InvariantCultureIgnoreCase) as JObject);
			RelatedResourceObjects.AddRange(mDatabase.extractJArray<IfcResourceObjectSelect>(obj.GetValue("RelatedResourceObjects", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RelatingReference"] = RelatingReference.getJson(this, options);
		}
	}
	public partial class IfcExtrudedAreaSolid : IfcSweptAreaSolid // SUPERTYPE OF(IfcExtrudedAreaSolidTapered)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("ExtrudedDirection", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
			{
				JToken jtoken = jobj["href"];
				if (jtoken != null)
					mExtrudedDirection = jtoken.Value<int>();
				else
					ExtrudedDirection = mDatabase.parseJObject<IfcDirection>(jobj);
			}
			JToken token = obj.GetValue("Depth", StringComparison.InvariantCultureIgnoreCase);
			if(token != null)
				mDepth= token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			obj["ExtrudedDirection"] = ExtrudedDirection.getJson(this, options);
			obj["Depth"] = mDepth;
		}
	}
}
