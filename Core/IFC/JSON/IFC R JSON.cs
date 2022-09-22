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
	public partial class IfcRail
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcRailTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcRailTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcRailType
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
				Enum.TryParse<IfcRailTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcRationalBSplineCurveWithKnots
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			foreach (double weight in mWeightsData)
				array.Add(weight);
			obj["WeightsData"] = array;
		}
	}
	public partial class IfcRationalBSplineSurfaceWithKnots
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			foreach (List<double> weights in mWeightsData)
			{
				JsonArray sub = new JsonArray();
				foreach (double weight in weights)
					sub.Add(weight);
				array.Add(sub);
			}
			obj["WeightsData"] = array;
		}
	}
	public partial class IfcRectangleHollowProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			WallThickness = obj["WallThickness"].GetValue<double>();
			var node = obj["InnerFilletRadius"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mInnerFilletRadius);
			node = obj["OuterFilletRadius"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mOuterFilletRadius);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["WallThickness"] = WallThickness;
			if (!double.IsNaN(mInnerFilletRadius))
				obj["InnerFilletRadius"] = InnerFilletRadius;
			if (!double.IsNaN(mOuterFilletRadius))
				obj["OuterFilletRadius"] = OuterFilletRadius;
		}
	}
	public partial class IfcRectangleProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			XDim = obj["XDim"].GetValue<double>();
			YDim = obj["YDim"].GetValue<double>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["XDim"] = XDim;
			obj["YDim"] = YDim;
		}
	}
	public partial class IfcReference
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["TypeIdentifier"];
			if (node != null)
				TypeIdentifier = node.GetValue<string>();
			node = obj["AttributeIdentifier"];
			if (node != null)
				AttributeIdentifier = node.GetValue<string>();
			node = obj["InstanceName"];
			if (node != null)
				InstanceName = node.GetValue<string>();
			JsonArray array = obj["ListPositions"] as JsonArray;
			if (array != null)
				mListPositions.AddRange(array.Select(x => x.GetValue<int>()));
			JsonObject jobj = obj["InnerReference"] as JsonObject;
			if (jobj != null)
				InnerReference = mDatabase.ParseJsonObject<IfcReference>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "TypeIdentifier", TypeIdentifier);
			setAttribute(obj, "AttributeIdentifier", AttributeIdentifier);
			setAttribute(obj, "InstanceName", InstanceName);
			createArray(obj, "ListPositions", mListPositions.Select(x => x.ToString()));
			if (mInnerReference != null)
				obj["InnerReference"] = InnerReference.getJson(this, options);
		}
	}

	public partial class IfcReinforcedSoil
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcReinforcedSoilTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcReinforcedSoilTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcRelAggregates
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			RelatedObjects.AddRange(mDatabase.extractJsonArray<IfcObjectDefinition>(obj["RelatedObjects"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				JsonArray array = new JsonArray();
				if (host == null || !mRelatedObjects.Contains(host))
				{
					foreach (IfcObjectDefinition od in RelatedObjects)
						array.Add(od.getJson(this, options));
					obj["RelatedObjects"] = array;
				}
			}
		}
	}
	public partial class IfcRelAssigns
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			RelatedObjects.AddRange(mDatabase.extractJsonArray<IfcObjectDefinition>(obj["RelatedObjects"] as JsonArray));			var node = obj["RelatedObjectsType"];
			if (node != null)
			{
				if (!Enum.TryParse<IfcObjectTypeEnum>(node.GetValue<string>(), true, out mRelatedObjectsType))
					mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;
			}
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			if (!RelatedObjects.Contains(host))
			{
				foreach (IfcObjectDefinition od in RelatedObjects)
					array.Add(od.getJson(this, options));
				obj["RelatedObjects"] = array;
			}
		}
	}
	public partial class IfcRelAssignsToGroup
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingProduct"] as JsonObject;
			if (jobj != null)
				RelatingGroup = mDatabase.ParseJsonObject<IfcGroup>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingGroup)
				obj["RelatingGroup"] = mRelatingGroup.getJson(this, options);
		}
	}
	public partial class IfcRelAssignsToGroupByFactor
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Factor"];
			if (node != null)
				Factor = node.GetValue<double>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Factor"] = mFactor;
		}
	}
	public partial class IfcRelAssignsToProduct
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			RelatingProduct = mDatabase.ParseJsonObject<IfcProductSelect>(obj["RelatingProduct"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingProduct)
				obj["RelatingProduct"] = mDatabase[mRelatingProduct.StepId].getJson(this, options);
		}
	}
	public partial class IfcRelAssociates
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			RelatedObjects.AddRange(mDatabase.extractJsonArray<IfcDefinitionSelect>(obj["RelatedObjects"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			IfcDefinitionSelect d = host as IfcDefinitionSelect;
			if (d == null || !mRelatedObjects.Contains(d))
			{
				foreach (IfcDefinitionSelect ds in mRelatedObjects)
					array.Add((ds as BaseClassIfc).getJson(this, options));
				obj["RelatedObjects"] = array;
			}
		}
	}
	public partial class IfcRelAssociatesClassification
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingClassification"] as JsonObject;
			if (jobj != null)
				RelatingClassification = mDatabase.ParseJsonObject<IfcClassificationSelect>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingClassification)
				obj["RelatingClassification"] = mRelatingClassification.getJson(this, options);
		}
	}
	public partial class IfcRelAssociatesConstraint
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Intent"];
			if (node != null)
				Intent = node.GetValue<string>();
			JsonObject jobj = obj["RelatingConstraint"] as JsonObject;
			if (jobj != null)
				RelatingConstraint = mDatabase.ParseJsonObject<IfcConstraint>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Intent", Intent);
			if (host != mRelatingConstraint)
				obj["RelatingConstraint"] = RelatingConstraint.getJson(this, options);
		}
	}
	public partial class IfcRelAssociatesDocument
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingDocument"] as JsonObject;
			if (jobj != null)
				RelatingDocument = mDatabase.ParseJsonObject<IfcDocumentSelect>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingDocument)
				obj["RelatingDocument"] = mRelatingDocument.getJson(this, options);
		}
	}
	public partial class IfcRelAssociatesLibrary
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingLibrary"] as JsonObject;
			if (jobj != null)
				RelatingLibrary = mDatabase.ParseJsonObject<IfcLibrarySelect>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingLibrary)
				obj["RelatingLibrary"] = mRelatingLibrary.getJson(this, options);
		}
	}
	public partial class IfcRelAssociatesMaterial
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingMaterial"] as JsonObject;
			if (jobj != null)
				RelatingMaterial = mDatabase.ParseJsonObject<IfcMaterialSelect>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingMaterial)
				obj["RelatingMaterial"] = mRelatingMaterial.getJson(this, options);
		}
	}
	public partial class IfcRelAssociatesProfileDef
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RelatingProfileDef"] = RelatingProfileDef.getJson(this, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingProfileDef"] as JsonObject;
			if (jobj != null)
				RelatingProfileDef = mDatabase.ParseJsonObject<IfcProfileDef>(jobj);
		}
	}
	public partial class IfcRelAssociatesProfileProperties
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingProfileProperties"] as JsonObject;
			if (jobj != null)
				RelatingProfileProperties = mDatabase.ParseJsonObject<IfcProfileProperties>(jobj);
			jobj = obj["ProfileSectionLocation"] as JsonObject;
			if (jobj != null)
				ProfileSectionLocation = mDatabase.ParseJsonObject<IfcShapeAspect>(jobj);

			jobj = obj["ProfileOrientation"] as JsonObject;
			if (jobj != null)
			{
				var measure = jobj["IfcPlaneAngleMeasure"];
				if (measure != null)
					mProfileOrientation = new IfcPlaneAngleMeasure(measure.GetValue<double>());
				else
				{
					IfcDirection dir = mDatabase.ParseJsonObject<IfcDirection>(jobj);
					if (dir != null)
						mProfileOrientation = dir;
				}
			}
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingProfileProperties)
				obj["RelatingProfileProperties"] = mRelatingProfileProperties.getJson(this, options);
			if (mProfileSectionLocation != null && host != mProfileSectionLocation)
				obj["ProfileSectionLocation"] = mProfileSectionLocation.getJson(this, options);
			if (mProfileOrientation is BaseClassIfc o)
				obj["ProfileOrientation"] = o.getJson(this, options);
			else if (mProfileOrientation is IfcPlaneAngleMeasure planeAngleMeasure)
			{
				JsonObject jobj = new JsonObject();
				jobj["IfcPlaneAngleMeasure"] = planeAngleMeasure.Measure;
				obj["ProfileOrientation"] = jobj;
			}
		}
	}
	public partial class IfcRelConnectsStructuralActivity
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingElement"] as JsonObject;
			if (jobj != null)
				RelatingElement = mDatabase.ParseJsonObject<IfcStructuralActivityAssignmentSelect>(jobj);
			jobj = obj["RelatedStructuralActivity"] as JsonObject;
			if (jobj != null)
				RelatedStructuralActivity = mDatabase.ParseJsonObject<IfcStructuralActivity>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingElement)
				obj["RelatingElement"] = mRelatingElement.getJson(this, options);
			if (host != mRelatedStructuralActivity)
				obj["RelatedStructuralActivity"] = mRelatedStructuralActivity.getJson(this, options);

		}
	}
	public partial class IfcRelConnectsStructuralMember 
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host == null || host != mRelatingStructuralMember)
				obj["RelatingStructuralMember"] = RelatingStructuralMember.getJson(host, options);
			if (host == null || host != mRelatedStructuralConnection)
				obj["RelatedStructuralConnection"] = RelatedStructuralConnection.getJson(host, options);
			if (mAppliedCondition != null)
				obj["AppliedCondition"] = AppliedCondition.getJson(this, options);
			if (mAdditionalConditions != null)
				obj["AdditionalConditions"] = AdditionalConditions.getJson(this, options);
			if (mSupportedLength > 0)
				obj["SupportedLength"] = SupportedLength;
			if (mConditionCoordinateSystem != null)
				obj["ConditionCoordinateSystem"] = ConditionCoordinateSystem.getJson(this, options);
		}
	}
	public partial class IfcRelContainedInSpatialStructure
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			RelatedElements.AddRange(mDatabase.extractJsonArray<IfcProduct>(obj["RelatedElements"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			if (options.Style != SetJsonOptions.JsonStyle.Repository)
				createArray(obj, "RelatedElements", RelatedElements, this, options);
		}
	}
	public partial class IfcRelDeclares
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingContext"] as JsonObject;
			if (jobj != null)
				RelatingContext = mDatabase.ParseJsonObject<IfcContext>(jobj);
			RelatedDefinitions.AddRange(mDatabase.extractJsonArray<IfcDefinitionSelect>(obj["RelatedDefinitions"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (RelatingContext != host)
				obj["RelatingContext"] = RelatingContext.getJson(this, options);
			createArray(obj, "RelatedDefinitions", mRelatedDefinitions, this, options);
		}
	}
	public partial class IfcRelDefinesByProperties
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mDatabase.extractJsonArray<IfcObjectDefinition>(obj["RelatedObjects"] as JsonArray).ForEach(x=> RelatedObjects.Add(x));
			RelatingPropertyDefinition.Add(mDatabase.ParseJsonObject<IfcPropertySetDefinition>(obj["RelatingPropertyDefinition"] as JsonObject));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RelatingPropertyDefinition"] = RelatingPropertyDefinition.First().getJson(this, options);
		}
	}
	public partial class IfcRelDefinesByTemplate : IfcRelDefines
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonArray array = obj["RelatedPropertySets"] as JsonArray;
			if (array != null)
				RelatedPropertySets.AddRange(mDatabase.extractJsonArray<IfcPropertySetDefinition>(array));
			JsonObject jobj = obj["RelatingTemplate"] as JsonObject;
			if(jobj != null)
				RelatingTemplate = extractObject<IfcPropertySetTemplate>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RelatingTemplate"] = RelatingTemplate.getJson(this, options);
			//List<IfcObject> relatedObjects = RelatedObjects;
			//JsonArray array = new JsonArray();
			//foreach (IfcObject obj in relatedObjects)
			//	array.Add(obj.StepId);
			//obj["RelatedObjects"] = array;
		}
	}
	public partial class IfcRelDefinesByType : IfcRelDefines
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonArray array = obj["RelatedObjects"] as JsonArray;
			if (array != null)
				RelatedObjects.AddRange(mDatabase.extractJsonArray<IfcObject>(array));
			JsonObject jobj = obj["RelatingType"] as JsonObject;
			if(jobj != null)
				RelatingType = extractObject<IfcTypeObject>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RelatingType"] = RelatingType.getJson(this, options);
			//List<IfcObject> relatedObjects = RelatedObjects;
			//JsonArray array = new JsonArray();
			//foreach (IfcObject obj in relatedObjects)
			//	array.Add(obj.StepId);
			//obj["RelatedObjects"] = array;
		}
	}
	public partial class IfcRelFillsElement : IfcRelConnects
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingOpeningElement"] as JsonObject;
			if (jobj != null)
				RelatingOpeningElement = mDatabase.ParseJsonObject<IfcOpeningElement>(jobj);
			jobj = obj["RelatedBuildingElement"] as JsonObject;
			if (jobj != null)
				RelatedBuildingElement = mDatabase.ParseJsonObject<IfcElement>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingOpeningElement)
				obj["RelatingOpeningElement"] = mRelatingOpeningElement.getJson(this, options);
			if (host != mRelatedBuildingElement)
				obj["RelatedBuildingElement"] = mRelatedBuildingElement.getJson(this, options);
		}
	}
	public partial class IfcRelNests : IfcRelDecomposes
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingObject"] as JsonObject;
			if (jobj != null)
				RelatingObject = mDatabase.ParseJsonObject<IfcObjectDefinition>(jobj);
			RelatedObjects.AddRange(mDatabase.extractJsonArray<IfcObjectDefinition>(obj["RelatedObjects"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mRelatingObject != host)
				obj["RelatingObject"] = RelatingObject.getJson(this, options);
			JsonArray array = new JsonArray();
			foreach (IfcObjectDefinition od in RelatedObjects)
				array.Add(od.getJson(this, options));
			obj["RelatedObjects"] = array;
		}
	}
	public partial class IfcRelServicesBuildings : IfcRelConnects
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingSystem"] as JsonObject;
			if (jobj != null)
				RelatingSystem = mDatabase.ParseJsonObject<IfcSystem>(jobj);
			mDatabase.extractJsonArray<IfcSpatialElement>(obj["RelatedBuildings"] as JsonArray).ForEach(x=>addRelated(x));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mRelatingSystem != host)
				obj["RelatingSystem"] = RelatingSystem.getJson(this, options);
			//List<IfcObjectDefinition> relatedObjects = RelatedObjects;
			//JsonArray array = new JsonArray();
			//foreach (IfcObjectDefinition od in relatedObjects)
			//	array.Add(od.getJson(this, options));
			//obj["RelatedObjects"] = array;
		}
	}
	public partial class IfcRelVoidsElement : IfcRelDecomposes
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingBuildingElement"] as JsonObject;
			if (jobj != null)
				RelatingBuildingElement = mDatabase.ParseJsonObject<IfcElement>(jobj);
			jobj = obj["RelatedOpeningElement"] as JsonObject;
			if (jobj != null)
				RelatedOpeningElement = mDatabase.ParseJsonObject<IfcFeatureElementSubtraction>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingBuildingElement)
				obj["RelatingBuildingElement"] = mRelatingBuildingElement.getJson(this, options);
			if (host != mRelatedOpeningElement)
				obj["RelatedOpeningElement"] = mRelatedOpeningElement.getJson(this, options);
		}
	}
	public partial class IfcRepresentation<RepresentationItem> : BaseClassIfc, IfcLayeredItem where RepresentationItem : IfcRepresentationItem // Abstract IFC4 ,SUPERTYPE OF (ONEOF(IfcShapeModel,IfcStyleModel));
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["ContextOfItems"] as JsonObject;
			if (jobj != null)
				ContextOfItems = extractObject<IfcRepresentationContext>(jobj);
			var node = obj["RepresentationIdentifier"];
			if (node != null)
				mRepresentationIdentifier = node.GetValue<string>();
			node = obj["RepresentationType"];
			if (node != null)
				RepresentationType = node.GetValue<string>();
			Items.AddRange(mDatabase.extractJsonArray<RepresentationItem>(obj["Items"] as JsonArray));

			List<IfcPresentationLayerAssignment> assignments = mDatabase.extractJsonArray<IfcPresentationLayerAssignment>(obj["LayerAssignments"] as JsonArray);
			foreach (IfcPresentationLayerAssignment a in assignments)
				a.AssignedItems.Add(this);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ContextOfItems"] = ContextOfItems.getJson(this, options);
			setAttribute(obj, "RepresentationIdentifier", RepresentationIdentifier);
			setAttribute(obj, "RepresentationType", RepresentationType);
			createArray(obj, "Items", Items, this, options);

			if (mLayerAssignment != null)
				obj["LayerAssignment"] = new JsonArray(mLayerAssignment.getJson(this, options));
		}
	}
	public abstract partial class IfcRepresentationContext : BaseClassIfc
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);

			var node = obj["ContextIdentifier"];
			if (node != null)
				ContextIdentifier = node.GetValue<string>();
			node = obj["ContextType"];
			if (node != null)
				ContextType = node.GetValue<string>();
			
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if(options.Style == SetJsonOptions.JsonStyle.Repository && string.IsNullOrEmpty(mGlobalId))
				setGlobalId(ParserIfc.EncodeGuid(Guid.NewGuid()));
			string contextIdentifier = ContextIdentifier;
			if (!string.IsNullOrEmpty(contextIdentifier))
				obj["ContextIdentifier"] = contextIdentifier;
			string contextType = ContextType;
			if (!string.IsNullOrEmpty(contextType))
				obj["ContextType"] = contextType;
		}
	}
	public abstract partial class IfcRepresentationItem : BaseClassIfc, IfcLayeredItem /*(IfcGeometricRepresentationItem,IfcMappedItem,IfcStyledItem,IfcTopologicalRepresentationItem));*/
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			List<IfcPresentationLayerAssignment> assignments = mDatabase.extractJsonArray<IfcPresentationLayerAssignment>(obj["LayerAssignments"] as JsonArray);
			foreach (IfcPresentationLayerAssignment a in assignments)
				a.AssignedItems.Add(this);
			JsonObject jobj = obj["StyledByItem"] as JsonObject;
			if (jobj != null)
				StyledByItem = mDatabase.ParseJsonObject<IfcStyledItem>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mLayerAssignment != null)
				obj["LayerAssignments"] = new JsonArray(mLayerAssignment.getJson(this, options));
			if (mStyledByItem != null)
				obj["StyledByItem"] = mStyledByItem.getJson(this, options);
		}

	}
	public partial class IfcRepresentationMap : BaseClassIfc, IfcProductRepresentationSelect
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["MappingOrigin"] as JsonObject;
			if (jobj != null)
				MappingOrigin = mDatabase.ParseJsonObject<IfcAxis2Placement>(jobj);
			jobj = obj["MappedRepresentation"] as JsonObject;
			if (jobj != null)
				MappedRepresentation = mDatabase.ParseJsonObject<IfcShapeModel>(jobj);
			JsonArray array = obj["HasShapeAspects"] as JsonArray;
			if (array != null)
			{
				List<IfcShapeAspect> aspects = mDatabase.extractJsonArray<IfcShapeAspect>(array);
				for(int icounter = 0; icounter < aspects.Count; icounter++)
					aspects[icounter].PartOfProductDefinitionShape = this;
			}
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["MappingOrigin"] = MappingOrigin.getJson(this, options);
			obj["MappedRepresentation"] = MappedRepresentation.getJson(this, options);
			createArray(obj, "HasShapeAspects", HasShapeAspects, this, options);
		}
	}
	public partial class IfcResourceConstraintRelationship : IfcResourceLevelRelationship  // IfcPropertyConstraintRelationship; // DEPRECATED IFC4 renamed
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["RelatingConstraint"] as JsonObject;
			if(jobj != null)
				RelatingConstraint = mDatabase.ParseJsonObject<IfcConstraint>(jobj);
			RelatedResourceObjects.AddRange(mDatabase.extractJsonArray<IfcResourceObjectSelect>(obj["RelatedResourceObjects"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mRelatingConstraint.StepId != host.StepId)
				obj["RelatingConstraint"] = RelatingConstraint.getJson(this, options);
			JsonArray array = new JsonArray();
			foreach (IfcResourceObjectSelect r in RelatedResourceObjects)
			{
				IfcResourceConstraintRelationship rcr = r as IfcResourceConstraintRelationship;
				if (r.StepId != host.StepId)
					array.Add(mDatabase[r.StepId].getJson(this, options));
			}
			if (array.Count > 0)
				obj["RelatedResourceObjects"] = array;
		}
	}
	public partial class IfcRevolvedAreaSolid : IfcSweptAreaSolid // SUPERTYPE OF(IfcRevolvedAreaSolidTapered)
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Axis"] as JsonObject;
			if (jobj != null)
				Axis = mDatabase.ParseJsonObject<IfcAxis1Placement>(jobj);
			var node = obj["Angle"];
			if (node != null)
				mAngle = node.GetValue<double>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			obj["Axis"] = Axis.getJson(this, options);
			obj["Angle"] = mAngle;
		}
	}
	public abstract partial class IfcRoot : BaseClassIfc//ABSTRACT SUPERTYPE OF (ONEOF (IfcObjectDefinition ,IfcPropertyDefinition ,IfcRelationship));
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
		
			var node = obj["GlobalId"];
			if (node != null)
				GlobalId = node.GetValue<string>();
			node = obj["OwnerHistory"];
			if(node != null)
			{
				JsonObject jobj = node as JsonObject;
				if(jobj != null)
					OwnerHistory = mDatabase.ParseJsonObject<IfcOwnerHistory>(jobj);
			}
			Name = extractString(obj, "Name");
			Description = extractString(obj, "Description");
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["GlobalId"] = GlobalId;
			if (options.SerializeOwnerHistory)
			{
				IfcOwnerHistory ownerHistory = OwnerHistory;
				if (ownerHistory != null)
					obj["OwnerHistory"] = ownerHistory.getJson(this, options);
			}
			base.setAttribute(obj, "Name", Name);
			base.setAttribute(obj, "Description", Description);
		}
	}
	public partial class IfcRotationalStiffnessSelect//SELECT ( IfcBoolean, IfcLinearStiffnessMeasure); 
	{
		internal static IfcRotationalStiffnessSelect parseJsonObject(JsonObject obj)
		{
			JsonObject jobj = obj["IfcBoolean"] as JsonObject;
			if (jobj != null)
				return new IfcRotationalStiffnessSelect(jobj.GetValue<bool>());
			jobj = obj["IfcRotationalStiffnessMeasure"] as JsonObject;
			if (jobj != null)
				return new IfcRotationalStiffnessSelect(jobj.GetValue<double>());
			return IfcRotationalStiffnessSelect.Parse(obj.GetValue<double>().ToString(), ReleaseVersion.IFC2x3);
		}
		internal JsonObject getJsonObject()
		{
			JsonObject obj = new JsonObject();
			if (mStiffness != null)
				obj["IfcRotationalStiffnessMeasure"] = mStiffness.Measure;
			else
				obj["IfcBoolean"] = mRigid;
			return obj;
		}
	}
	public partial class IfcRoundedRectangleProfileDef : IfcRectangleProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			RoundingRadius = obj["RoundingRadius"].GetValue<double>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RoundingRadius"] = RoundingRadius;
		}
	}
}
#endif
