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
	public partial class IfcRectangleHollowProfileDef : IfcRectangleProfileDef
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			WallThickness = obj.GetValue("WallThickness", StringComparison.InvariantCultureIgnoreCase).Value<double>();
			JToken token = obj.GetValue("InnerFilletRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mInnerFilletRadius);
			token = obj.GetValue("OuterFilletRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mOuterFilletRadius);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["WallThickness"] = WallThickness;
			if (!double.IsNaN(mInnerFilletRadius))
				obj["InnerFilletRadius"] = InnerFilletRadius;
			if (!double.IsNaN(mOuterFilletRadius))
				obj["OuterFilletRadius"] = OuterFilletRadius;
		}
	}
	public partial class IfcRectangleProfileDef : IfcParameterizedProfileDef
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			XDim = obj.GetValue("XDim", StringComparison.InvariantCultureIgnoreCase).Value<double>();
			YDim = obj.GetValue("YDim", StringComparison.InvariantCultureIgnoreCase).Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["XDim"] = XDim;
			obj["YDim"] = YDim;
		}
	}
	public partial class IfcReference : BaseClassIfc, IfcMetricValueSelect, IfcAppliedValueSelect // IFC4
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("TypeIdentifier", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				TypeIdentifier = token.Value<string>();
			token = obj.GetValue("AttributeIdentifier", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				AttributeIdentifier = token.Value<string>();
			token = obj.GetValue("InstanceName", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				InstanceName = token.Value<string>();
			JArray array = obj.GetValue("ListPositions", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
				mListPositions = array.ToList().ConvertAll(x => x.Value<int>());
			JObject jobj = obj.GetValue("InnerReference", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				InnerReference = mDatabase.parseJObject<IfcReference>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "TypeIdentifier", TypeIdentifier);
			setAttribute(obj, "AttributeIdentifier", AttributeIdentifier);
			setAttribute(obj, "InstanceName", InstanceName);
			if (mListPositions.Count > 0)
				obj["ListPositions"] = new JArray(mListPositions.ToArray());
			if (mInnerReference > 0)
				obj["InnerReference"] = InnerReference.getJson(this, options);
		}
	}
	public partial class IfcRelAggregates : IfcRelDecomposes
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mDatabase.extractJArray<IfcObjectDefinition>(obj.GetValue("RelatedObjects", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addObject(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				JArray array = new JArray();
				if (host == null || !mRelatedObjects.Contains(host.mIndex))
				{
					foreach (IfcObjectDefinition od in RelatedObjects)
						array.Add(od.getJson(this, options));
					obj["RelatedObjects"] = array;
				}
			}
		}
	}
	public partial class IfcRelAssigns : IfcRelationship //	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelAssignsToActor, IfcRelAssignsToControl, IfcRelAssignsToGroup, IfcRelAssignsToProcess, IfcRelAssignsToProduct, IfcRelAssignsToResource))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RelatedObjects.AddRange(mDatabase.extractJArray<IfcObjectDefinition>(obj.GetValue("RelatedObjects", StringComparison.InvariantCultureIgnoreCase) as JArray));			JToken token = obj.GetValue("RelatedObjectsType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
			{
				if (!Enum.TryParse<IfcObjectTypeEnum>(token.Value<string>(), true, out mRelatedObjectsType))
					mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JArray array = new JArray();
			if (!RelatedObjects.Contains(host))
			{
				foreach (IfcObjectDefinition od in RelatedObjects)
					array.Add(od.getJson(this, options));
				obj["RelatedObjects"] = array;
			}
		}
	}
	public partial class IfcRelAssignsToGroup : IfcRelAssigns   //SUPERTYPE OF(IfcRelAssignsToGroupByFactor)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingProduct", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingGroup = mDatabase.parseJObject<IfcGroup>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingGroup)
				obj["RelatingGroup"] = mRelatingGroup.getJson(this, options);
		}
	}
	public partial class IfcRelAssignsToGroupByFactor : IfcRelAssignsToGroup //IFC4
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Factor");
			if (token != null)
				Factor = token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Factor"] = mFactor;
		}
	}
	public partial class IfcRelAssignsToProduct : IfcRelAssigns
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RelatingProduct = mDatabase.parseJObject<IfcProductSelect>(obj.GetValue("RelatingProduct", StringComparison.InvariantCultureIgnoreCase) as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingProduct)
				obj["RelatingProduct"] = mDatabase[mRelatingProduct.Index].getJson(this, options);
		}
	}
	public partial class IfcRelAssociates : IfcRelationship   //ABSTRACT SUPERTYPE OF (ONEOF(IfcRelAssociatesApproval,IfcRelAssociatesclassification,IfcRelAssociatesConstraint,IfcRelAssociatesDocument,IfcRelAssociatesLibrary,IfcRelAssociatesMaterial))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RelatedObjects.AddRange(mDatabase.extractJArray<IfcDefinitionSelect>(obj.GetValue("RelatedObjects", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JArray array = new JArray();
			IfcDefinitionSelect d = host as IfcDefinitionSelect;
			if (d == null || !mRelatedObjects.Contains(d))
			{
				foreach (IfcDefinitionSelect ds in mRelatedObjects)
					array.Add((ds as BaseClassIfc).getJson(this, options));
				obj["RelatedObjects"] = array;
			}
		}
	}
	public partial class IfcRelAssociatesClassification : IfcRelAssociates
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingClassification", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingClassification = mDatabase.parseJObject<IfcClassificationSelect>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host != mRelatingClassification)
				obj["RelatingClassification"] = mRelatingClassification.getJson(this, options);
		}
	}
	public partial class IfcRelAssociatesConstraint : IfcRelAssociates
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Intent", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Intent = token.Value<string>();
			JObject jobj = obj.GetValue("RelatingConstraint", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingConstraint = mDatabase.parseJObject<IfcConstraint>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Intent", Intent);
			if (host.mIndex != mRelatingConstraint)
				obj["RelatingConstraint"] = RelatingConstraint.getJson(this, options);
		}
	}
	public partial class IfcRelAssociatesDocument : IfcRelAssociates
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingDocument", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingDocument = mDatabase.parseJObject<IfcDocumentSelect>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host.mIndex != mRelatingDocument)
				obj["RelatingDocument"] = mDatabase[mRelatingDocument].getJson(this, options);
		}
	}
	public partial class IfcRelAssociatesLibrary : IfcRelAssociates
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingLibrary", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingLibrary = mDatabase.parseJObject<IfcLibrarySelect>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host.mIndex != mRelatingLibrary)
				obj["RelatingLibrary"] = mDatabase[mRelatingLibrary].getJson(this, options);
		}
	}
	public partial class IfcRelAssociatesMaterial : IfcRelAssociates
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingMaterial", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingMaterial = mDatabase.parseJObject<IfcMaterialSelect>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host.mIndex != mRelatingMaterial)
				obj["RelatingMaterial"] = mDatabase[mRelatingMaterial].getJson(this, options);
		}
	}
	public partial class IfcRelAssociatesProfileProperties : IfcRelAssociates //IFC4 DELETED Replaced by IfcRelAssociatesMaterial together with material-profile sets
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingProfileProperties", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingProfileProperties = mDatabase.parseJObject<IfcProfileProperties>(jobj);
			jobj = obj.GetValue("ProfileSectionLocation", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				ProfileSectionLocation = mDatabase.parseJObject<IfcShapeAspect>(jobj);

			jobj = obj.GetValue("ProfileOrientation", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
			{
				JToken measure = jobj["IfcPlaneAngleMeasure"];
				if (measure != null)
					mProfileOrientationValue = measure.Value<double>();
				else
				{
					IfcDirection dir = mDatabase.parseJObject<IfcDirection>(jobj);
					if (dir != null)
						mProfileOrientation = dir.mIndex;
				}
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host.mIndex != mRelatingProfileProperties)
				obj["RelatingProfileProperties"] = mDatabase[mRelatingProfileProperties].getJson(this, options);
			if (mProfileSectionLocation > 0 && host.mIndex != mProfileSectionLocation)
				obj["ProfileSectionLocation"] = mDatabase[mProfileSectionLocation].getJson(this, options);
			if (mProfileOrientation > 0)
				obj["ProfileOrientation"] = mDatabase[mProfileOrientation].getJson(this, options);
			else if (!double.IsNaN(mProfileOrientationValue))
			{
				JObject jobj = new JObject();
				jobj["IfcPlaneAngleMeasure"] = mProfileOrientationValue;
				obj["ProfileOrientation"] = jobj;
			}
		}
	}
	public partial class IfcRelConnectsStructuralActivity : IfcRelConnects
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingElement", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingElement = mDatabase.parseJObject<IfcStructuralActivityAssignmentSelect>(jobj);
			jobj = obj.GetValue("RelatedStructuralActivity", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatedStructuralActivity = mDatabase.parseJObject<IfcStructuralActivity>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host.mIndex != mRelatingElement)
				obj["RelatingElement"] = mDatabase[mRelatingElement].getJson(this, options);
			if (host.mIndex != mRelatedStructuralActivity)
				obj["RelatedStructuralActivity"] = mDatabase[mRelatedStructuralActivity].getJson(this, options);

		}
	}
	public partial class IfcRelConnectsStructuralMember : IfcRelConnects
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host == null || host.mIndex != mRelatingStructuralMember)
				obj["RelatingStructuralMember"] = RelatingStructuralMember.getJson(host, options);
			if (host == null || host.mIndex != mRelatedStructuralConnection)
				obj["RelatedStructuralConnection"] = RelatedStructuralConnection.getJson(host, options);
			if (mAppliedCondition > 0)
				obj["AppliedCondition"] = AppliedCondition.getJson(this, options);
			if (mAdditionalConditions > 0)
				obj["AdditionalConditions"] = AdditionalConditions.getJson(this, options);
			if (mSupportedLength > 0)
				obj["SupportedLength"] = SupportedLength;
			if (mConditionCoordinateSystem > 0)
				obj["ConditionCoordinateSystem"] = ConditionCoordinateSystem.getJson(this, options);
		}
	}
	public partial class IfcRelContainedInSpatialStructure : IfcRelConnects
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RelatedElements.AddRange(mDatabase.extractJArray<IfcProduct>(obj.GetValue("RelatedElements", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			if (options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				JArray array = new JArray();
				foreach (IfcProduct product in RelatedElements)
					array.Add(product.getJson(this, options));
				obj["RelatedElements"] = array;
			}
		}
	}
	public partial class IfcRelDeclares : IfcRelationship //IFC4
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingContext", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingContext = mDatabase.parseJObject<IfcContext>(jobj);
			RelatedDefinitions.AddRange(mDatabase.extractJArray<IfcDefinitionSelect>(obj.GetValue("RelatedDefinitions", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (RelatingContext != host)
				obj["RelatingContext"] = RelatingContext.getJson(this, options);
			obj["RelatedDefinitions"] = new JArray(mRelatedDefinitions.ConvertAll(x => mDatabase[x.Index].getJson(this, options)));
		}
	}
	public partial class IfcRelDefinesByProperties : IfcRelDefines
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mDatabase.extractJArray<IfcObjectDefinition>(obj.GetValue("RelatedObjects", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=> RelatedObjects.Add(x));
			RelatingPropertyDefinition = mDatabase.parseJObject<IfcPropertySetDefinition>(obj.GetValue("RelatingPropertyDefinition", StringComparison.InvariantCultureIgnoreCase) as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RelatingPropertyDefinition"] = RelatingPropertyDefinition.getJson(this, options);
		}
	}
	public partial class IfcRelDefinesByTemplate : IfcRelDefines
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JArray array = obj.GetValue("RelatedPropertySets", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
				mDatabase.extractJArray<IfcPropertySetDefinition>(array).ForEach(x=>AddRelated(x));
			RelatingTemplate = extractObject<IfcPropertySetTemplate>(obj.GetValue("RelatingTemplate", StringComparison.InvariantCultureIgnoreCase) as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RelatingTemplate"] = RelatingTemplate.getJson(this, options);
			//List<IfcObject> relatedObjects = RelatedObjects;
			//JArray array = new JArray();
			//foreach (IfcObject obj in relatedObjects)
			//	array.Add(obj.Index);
			//obj["RelatedObjects"] = array;
		}
	}
	public partial class IfcRelDefinesByType : IfcRelDefines
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JArray array = obj.GetValue("RelatedObjects", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
				RelatedObjects.AddRange(mDatabase.extractJArray<IfcObject>(array));
			JObject jobj = obj.GetValue("RelatingType", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if(jobj != null)
				RelatingType = extractObject<IfcTypeObject>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RelatingType"] = RelatingType.getJson(this, options);
			//List<IfcObject> relatedObjects = RelatedObjects;
			//JArray array = new JArray();
			//foreach (IfcObject obj in relatedObjects)
			//	array.Add(obj.Index);
			//obj["RelatedObjects"] = array;
		}
	}
	public partial class IfcRelFillsElement : IfcRelConnects
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingOpeningElement", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingOpeningElement = mDatabase.parseJObject<IfcOpeningElement>(jobj);
			jobj = obj.GetValue("RelatedBuildingElement", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatedBuildingElement = mDatabase.parseJObject<IfcElement>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host.mIndex != mRelatingOpeningElement)
				obj["RelatingOpeningElement"] = mDatabase[mRelatingOpeningElement].getJson(this, options);
			if (host.mIndex != mRelatedBuildingElement)
				obj["RelatedBuildingElement"] = mDatabase[mRelatedBuildingElement].getJson(this, options);
		}
	}
	public partial class IfcRelNests : IfcRelDecomposes
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingObject", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingObject = mDatabase.parseJObject<IfcObjectDefinition>(jobj);
			mDatabase.extractJArray<IfcObjectDefinition>(obj.GetValue("RelatedObjects", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>addRelated(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mRelatingObject != host)
				obj["RelatingObject"] = RelatingObject.getJson(this, options);
			ReadOnlyCollection<IfcObjectDefinition> relatedObjects = RelatedObjects;
			JArray array = new JArray();
			foreach (IfcObjectDefinition od in relatedObjects)
				array.Add(od.getJson(this, options));
			obj["RelatedObjects"] = array;
		}
	}
	public partial class IfcRelServicesBuildings : IfcRelConnects
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingSystem", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingSystem = mDatabase.parseJObject<IfcSystem>(jobj);
			mDatabase.extractJArray<IfcSpatialElement>(obj.GetValue("RelatedBuildings", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>addRelated(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mRelatingSystem != host.mIndex)
				obj["RelatingSystem"] = RelatingSystem.getJson(this, options);
			//List<IfcObjectDefinition> relatedObjects = RelatedObjects;
			//JArray array = new JArray();
			//foreach (IfcObjectDefinition od in relatedObjects)
			//	array.Add(od.getJson(this, options));
			//obj["RelatedObjects"] = array;
		}
	}
	public partial class IfcRelVoidsElement : IfcRelDecomposes
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingBuildingElement", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatingBuildingElement = mDatabase.parseJObject<IfcElement>(jobj);
			jobj = obj.GetValue("RelatedOpeningElement", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				RelatedOpeningElement = mDatabase.parseJObject<IfcFeatureElementSubtraction>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (host.mIndex != mRelatingBuildingElement)
				obj["RelatingBuildingElement"] = mDatabase[mRelatingBuildingElement].getJson(this, options);
			if (host.mIndex != mRelatedOpeningElement)
				obj["RelatedOpeningElement"] = mDatabase[mRelatedOpeningElement].getJson(this, options);
		}
	}
	public partial class IfcRepresentation : BaseClassIfc, IfcLayeredItem // Abstract IFC4 ,SUPERTYPE OF (ONEOF(IfcShapeModel,IfcStyleModel));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("ContextOfItems", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				ContextOfItems = extractObject<IfcRepresentationContext>(jobj);
			JToken token = obj.GetValue("RepresentationIdentifier", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mRepresentationIdentifier = token.Value<string>();
			token = obj.GetValue("RepresentationType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				RepresentationType = token.Value<string>();
			Items.AddRange(mDatabase.extractJArray<IfcRepresentationItem>(obj.GetValue("Items", StringComparison.InvariantCultureIgnoreCase) as JArray));

				
			List<IfcPresentationLayerAssignment> assignments = mDatabase.extractJArray<IfcPresentationLayerAssignment>(obj.GetValue("LayerAssignments", StringComparison.InvariantCultureIgnoreCase) as JArray);
			foreach (IfcPresentationLayerAssignment a in assignments)
				a.AssignedItems.Add(this);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ContextOfItems"] = ContextOfItems.getJson(this, options);
			setAttribute(obj, "RepresentationIdentifier", RepresentationIdentifier);
			setAttribute(obj, "RepresentationType", RepresentationType);
			obj["Items"] = new JArray(Items.ToList().ConvertAll(x => x.getJson(this, options)));

			if (mLayerAssignments.Count > 0)
				obj["LayerAssignments"] = new JArray(mLayerAssignments.ConvertAll(x => x.getJson(this, options)));
		}
	}
	public abstract partial class IfcRepresentationContext : BaseClassIfc
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);

			JToken token = obj.GetValue("ContextIdentifier", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ContextIdentifier = token.Value<string>();
			token = obj.GetValue("ContextType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ContextType = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if(options.Style == SetJsonOptions.JsonStyle.Repository && string.IsNullOrEmpty(mGlobalId))
				mGlobalId = ParserIfc.EncodeGuid(Guid.NewGuid());
			string contextIdentifier = ContextIdentifier;
			if (!string.IsNullOrEmpty(contextIdentifier))
				obj["ContextIdentifier"] = contextIdentifier;
			string contextType = ContextType;
			if (!string.IsNullOrEmpty(contextType))
				obj["ContextType"] = contextType;
			JArray reps = new JArray();
			foreach(IfcRepresentation r in RepresentationsInContext)
			{
				if (r.mOfProductRepresentation.Count == 0 && r.mRepresentationMap == null)
					reps.Add(r.getJson(this, options));
			}
			if (reps.Count > 0)
				obj["RepresentationsInContext"] = reps;

		}
	}
	public abstract partial class IfcRepresentationItem : BaseClassIfc, IfcLayeredItem /*(IfcGeometricRepresentationItem,IfcMappedItem,IfcStyledItem,IfcTopologicalRepresentationItem));*/
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			List<IfcPresentationLayerAssignment> assignments = mDatabase.extractJArray<IfcPresentationLayerAssignment>(obj.GetValue("LayerAssignments", StringComparison.InvariantCultureIgnoreCase) as JArray);
			foreach (IfcPresentationLayerAssignment a in assignments)
				a.AssignedItems.Add(this);
			JObject jobj = obj.GetValue("StyledByItem", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				StyledByItem = mDatabase.parseJObject<IfcStyledItem>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mLayerAssignments.Count > 0)
				obj["LayerAssignments"] = new JArray(mLayerAssignments.ConvertAll(x => x.getJson(this, options)));
			if (mStyledByItem != null)
				obj["StyledByItem"] = mStyledByItem.getJson(this, options);
		}

	}
	public partial class IfcRepresentationMap : BaseClassIfc, IfcProductRepresentationSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("MappingOrigin", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				MappingOrigin = mDatabase.parseJObject<IfcAxis2Placement>(jobj);
			jobj = obj.GetValue("MappedRepresentation", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				MappedRepresentation = mDatabase.parseJObject<IfcShapeModel>(jobj);
			JArray array = obj.GetValue("HasShapeAspects", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
			{
				List<IfcShapeAspect> aspects = mDatabase.extractJArray<IfcShapeAspect>(array);
				for(int icounter = 0; icounter < aspects.Count; icounter++)
					aspects[icounter].PartOfProductDefinitionShape = this;
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["MappingOrigin"] = mDatabase[mMappingOrigin].getJson(this, options);
			obj["MappedRepresentation"] = MappedRepresentation.getJson(this, options);
			if (mHasShapeAspects.Count > 0)
				obj["HasShapeAspects"] = new JArray(mHasShapeAspects.ConvertAll(x => x.getJson(this, options)));
		}
	}
	public partial class IfcResourceConstraintRelationship : IfcResourceLevelRelationship  // IfcPropertyConstraintRelationship; // DEPRECEATED IFC4 renamed
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RelatingConstraint", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if(jobj != null)
				RelatingConstraint = mDatabase.parseJObject<IfcConstraint>(jobj);
			mDatabase.extractJArray<IfcResourceObjectSelect>(obj.GetValue("RelatedResourceObjects", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>addRelated(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mRelatingConstraint != host.mIndex)
				obj["RelatingConstraint"] = RelatingConstraint.getJson(this, options);
			JArray array = new JArray();
			foreach (IfcResourceObjectSelect r in RelatedResourceObjects)
			{
				IfcResourceConstraintRelationship rcr = r as IfcResourceConstraintRelationship;
				if (r.Index != host.mIndex)
					array.Add(mDatabase[r.Index].getJson(this, options));
			}
			if (array.Count > 0)
				obj["RelatedResourceObjects"] = array;
		}
	}
	public partial class IfcRevolvedAreaSolid : IfcSweptAreaSolid // SUPERTYPE OF(IfcRevolvedAreaSolidTapered)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Axis", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Axis = mDatabase.parseJObject<IfcAxis1Placement>(jobj);
			JToken token = obj.GetValue("Angle", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mAngle = token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			obj["Axis"] = Axis.getJson(this, options);
			obj["Angle"] = mAngle;
		}
	}
	public abstract partial class IfcRoot : BaseClassIfc//ABSTRACT SUPERTYPE OF (ONEOF (IfcObjectDefinition ,IfcPropertyDefinition ,IfcRelationship));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
		
			JToken token = obj.GetValue("GlobalId", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				GlobalId = token.Value<string>();
			token = obj.GetValue("OwnerHistory", StringComparison.InvariantCultureIgnoreCase);
			if(token != null)
			{
				JObject jobj = token as JObject;
				if(jobj != null)
					OwnerHistory = mDatabase.parseJObject<IfcOwnerHistory>(jobj);
			}
			token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
			token = obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Description = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["GlobalId"] = GlobalId;
			IfcOwnerHistory ownerHistory = OwnerHistory;
			if (ownerHistory != null)
				obj["OwnerHistory"] = ownerHistory.getJson(this, options);
			base.setAttribute(obj, "Name", Name);
			base.setAttribute(obj, "Description", Description);
		}
	}
	public partial class IfcRotationalStiffnessSelect//SELECT ( IfcBoolean, IfcLinearStiffnessMeasure); 
	{
		internal static IfcRotationalStiffnessSelect parseJObject(JObject obj)
		{
			JObject jobj = obj.GetValue("IfcBoolean", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				return new IfcRotationalStiffnessSelect(jobj.Value<bool>());
			jobj = obj.GetValue("IfcRotationalStiffnessMeasure", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				return new IfcRotationalStiffnessSelect(jobj.Value<double>());
			return IfcRotationalStiffnessSelect.Parse(obj.Value<double>().ToString(), ReleaseVersion.IFC2x3);
		}
		internal JObject getJObject()
		{
			JObject obj = new JObject();
			if (mStiffness != null)
				obj["IfcRotationalStiffnessMeasure"] = mStiffness.Measure;
			else
				obj["IfcBoolean"] = mRigid;
			return obj;
		}
	}
	public partial class IfcRoundedRectangleProfileDef : IfcRectangleProfileDef
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RoundingRadius = obj.GetValue("RoundingRadius", StringComparison.InvariantCultureIgnoreCase).Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RoundingRadius"] = RoundingRadius;
		}
	}
}
