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

#if (!NOIFCJSON)
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
	public partial class IfcEarthworksCut : IfcFeatureElementSubtraction
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcEarthworksCutTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcEarthworksCutTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcEarthworksFill : IfcEarthworksElement
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcEarthworksFillTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcEarthworksFillTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcEdge : IfcTopologicalRepresentationItem //SUPERTYPE OF(ONEOF(IfcEdgeCurve, IfcOrientedEdge, IfcSubedge))
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["EdgeStart"] as JsonObject;
			if (jobj != null)
				EdgeStart = mDatabase.ParseJsonObject<IfcVertex>(jobj);
			jobj = obj["EdgeEnd"] as JsonObject;
			if (jobj != null)
				EdgeEnd = mDatabase.ParseJsonObject<IfcVertex>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if(mEdgeStart != null)
				obj["EdgeStart"] = EdgeStart.getJson(this, options);
			if(mEdgeEnd != null)
				obj["EdgeEnd"] = mEdgeEnd.getJson(this, options);
		}
	}
	public partial class IfcElectricFlowTreatmentDevice : IfcFlowTreatmentDevice
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcElectricFlowTreatmentDeviceTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcElectricFlowTreatmentDeviceTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcElectricFlowTreatmentDeviceType : IfcFlowTreatmentDeviceType
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcElectricFlowTreatmentDeviceTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcElement : IfcProduct, IfcStructuralActivityAssignmentSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcBuildingElement,IfcCivilElement
	{ //,IfcDistributionElement,IfcElementAssembly,IfcElementComponent,IfcFeatureElement,IfcFurnishingElement,IfcGeographicElement,IfcTransportElement ,IfcVirtualElement,IfcElectricalElement SS,IfcEquipmentElement SS)) 
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Tag"];
			if (node != null)
				Tag = node.GetValue<string>();
			foreach (IfcRelVoidsElement voids in mDatabase.extractJsonArray<IfcRelVoidsElement>(obj["HasOpenings"] as JsonArray))
				voids.RelatingBuildingElement = this;
			foreach (IfcRelConnectsStructuralActivity rcsa in mDatabase.extractJsonArray<IfcRelConnectsStructuralActivity>(obj["AssignedStructuralActivity"] as JsonArray))
				rcsa.RelatingElement = this;
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			string tag = Tag;
			if (!string.IsNullOrEmpty(tag))
				obj["Tag"] = tag;
			if (mHasOpenings.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcRelVoidsElement rv in HasOpenings)
				{
					if (rv.StepId != host.StepId)
						array.Add(rv.getJson(this, options));
				}
				if (array.Count > 0)
					obj["HasOpenings"] = array;
			}
		}
	}
	public partial class IfcElementQuantity : IfcQuantitySet
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["MethodOfMeasurement"];
			if (node != null)
				MethodOfMeasurement = node.GetValue<string>();
			mDatabase.extractJsonArray<IfcPhysicalQuantity>(obj["Quantities"] as JsonArray).ForEach(x => addQuantity(x));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "MethodOfMeasurement", MethodOfMeasurement);
			createArray(obj, "Quantities", Quantities.Values, this, options);
		}
	}
	public partial class IfcElementAssembly : IfcElement
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcElementAssemblyTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcElementAssemblyTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public abstract partial class IfcElementarySurface : IfcSurface //	ABSTRACT SUPERTYPE OF(ONEOF(IfcCylindricalSurface, IfcPlane))
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Position = extractObject<IfcAxis2Placement3D>(obj["Position"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Position"] = Position.getJson(this, options);
		}
	}
	public abstract partial class IfcElementType : IfcTypeProduct //ABSTRACT SUPERTYPE OF(ONEOF(IfcBuildingElementType, IfcDistributionElementType, IfcElementAssemblyType, IfcElementComponentType, IfcFurnishingElementType, IfcGeographicElementType, IfcTransportElementType))
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["ElementType"];
			if (node != null)
				mElementType = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "ElementType", ElementType);
		}
	}
	public partial class IfcEllipseProfileDef : IfcParameterizedProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			SemiAxis1 = obj["SemiAxis1"].GetValue<double>();
			SemiAxis2 = obj["SemiAxis2"].GetValue<double>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["SemiAxis1"] = SemiAxis1;
			obj["SemiAxis2"] = SemiAxis2;
		}
	}
	public abstract partial class IfcExtendedProperties : IfcPropertyAbstraction //IFC4 ABSTRACT SUPERTYPE OF (ONEOF (IfcMaterialProperties,IfcProfileProperties))
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
			mDatabase.extractJsonArray<IfcProperty>(obj["Properties"] as JsonArray).ForEach(x=>AddProperty(x));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if(mDatabase == null || mDatabase.Release > ReleaseVersion.IFC2x3)
				base.setAttribute(obj, "Name", Name);
			base.setAttribute(obj, "Description", Description);
			if(mProperties.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcProperty p in mProperties.Values)
					array.Add(p.getJson(this, options));
				obj["Properties"] = array;
			}
		}
	}
	public abstract partial class IfcExternalReference : BaseClassIfc, IfcLightDistributionDataSourceSelect, IfcResourceObjectSelect//ABSTRACT SUPERTYPE OF (ONEOF (IfcClassificationReference ,IfcDocumentReference ,IfcExternallyDefinedHatchStyle
	{ //,IfcExternallyDefinedSurfaceStyle ,IfcExternallyDefinedSymbol ,IfcExternallyDefinedTextFont ,IfcLibraryReference)); 
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Location = extractString(obj["Location"]);
			Identification = extractString(obj["Identification"]);
			Name = extractString(obj["Name"]);
			foreach (IfcExternalReferenceRelationship r in mDatabase.extractJsonArray<IfcExternalReferenceRelationship>(obj["HasExternalReference"] as JsonArray))
				r.RelatedResourceObjects.Add(this);
			foreach (IfcResourceConstraintRelationship r in mDatabase.extractJsonArray<IfcResourceConstraintRelationship>(obj["HasConstraintRelationships"] as JsonArray))
				r.RelatedResourceObjects.Add(this);
			//foreach (IfcExternalReferenceRelationship r in mDatabase.extractJsonArray<IfcExternalReferenceRelationship>(obj["ExternalReferenceForResources"] as JsonArray))
			//	r.addRelated(this);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Location", Location);
			setAttribute(obj, "Identification", Identification);
			setAttribute(obj, "Name", Name);
			createArray(obj, "HasExternalReference", HasExternalReference, this, options);
			createArray(obj, "HasConstraintRelationships", HasConstraintRelationships, this, options);
			createArray(obj, "ExternalReferenceForResources", ExternalReferenceForResources, this, options);
		}
	}

	public partial class IfcExternalReferenceRelationship : IfcResourceLevelRelationship //IFC4
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			RelatingReference = mDatabase.ParseJsonObject<IfcExternalReference>(obj["RelatingReference"] as JsonObject);
			RelatedResourceObjects.AddRange(mDatabase.extractJsonArray<IfcResourceObjectSelect>(obj["RelatedResourceObjects"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RelatingReference"] = RelatingReference.getJson(this, options);
		}
	}
	public partial class IfcExtrudedAreaSolid : IfcSweptAreaSolid // SUPERTYPE OF(IfcExtrudedAreaSolidTapered)
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["ExtrudedDirection"] as JsonObject;
			if (jobj != null)
					ExtrudedDirection = mDatabase.ParseJsonObject<IfcDirection>(jobj);
			var node = obj["Depth"];
			if(node != null)
				mDepth= node.GetValue<double>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			obj["ExtrudedDirection"] = ExtrudedDirection.getJson(this, options);
			obj["Depth"] = mDepth;
		}
	}
}
#endif
