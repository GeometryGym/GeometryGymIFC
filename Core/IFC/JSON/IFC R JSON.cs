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
	public partial class IfcRectangleProfileDef : IfcParameterizedProfileDef
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			XDim = obj.GetValue("XDim", StringComparison.InvariantCultureIgnoreCase).Value<double>();
			YDim = obj.GetValue("YDim", StringComparison.InvariantCultureIgnoreCase).Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
				ListPositions = array.ToList().ConvertAll(x => x.Value<int>());
			JObject jobj = obj.GetValue("InnerReference", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				InnerReference = mDatabase.parseJObject<IfcReference>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			setAttribute(obj, "TypeIdentifier", TypeIdentifier);
			setAttribute(obj, "AttributeIdentifier", AttributeIdentifier);
			setAttribute(obj, "InstanceName", InstanceName);
			if (mListPositions.Count > 0)
				obj["ListPositions"] = new JArray(mListPositions.ToArray());
			if (mInnerReference > 0)
				obj["InnerReference"] = InnerReference.getJson(this, processed);
		}
	}
	public partial class IfcRelAggregates : IfcRelDecomposes
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RelatedObjects = mDatabase.extractJArray<IfcObjectDefinition>(obj.GetValue("RelatedObjects", StringComparison.InvariantCultureIgnoreCase) as JArray);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			JArray array = new JArray();
			if (!mRelatedObjects.Contains(host.mIndex))
			{
				foreach (IfcObjectDefinition od in RelatedObjects)
					array.Add(od.getJson(this, processed));
				obj["RelatedObjects"] = array;
			}
		}
	}
	public partial class IfcRelAssigns : IfcRelationship //	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelAssignsToActor, IfcRelAssignsToControl, IfcRelAssignsToGroup, IfcRelAssignsToProcess, IfcRelAssignsToProduct, IfcRelAssignsToResource))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RelatedObjects = mDatabase.extractJArray<IfcObjectDefinition>(obj.GetValue("RelatedObjects", StringComparison.InvariantCultureIgnoreCase) as JArray);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			JArray array = new JArray();
			if (!mRelatedObjects.Contains(host.mIndex))
			{
				foreach (IfcObjectDefinition od in RelatedObjects)
					array.Add(od.getJson(this, processed));
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (host.mIndex != mRelatingGroup)
				obj["RelatingGroup"] = mDatabase[mRelatingGroup].getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (host.mIndex != mRelatingProduct)
				obj["RelatingProduct"] = mDatabase[mRelatingProduct].getJson(this, processed);
		}
	}
	public partial class IfcRelAssociates : IfcRelationship   //ABSTRACT SUPERTYPE OF (ONEOF(IfcRelAssociatesApproval,IfcRelAssociatesclassification,IfcRelAssociatesConstraint,IfcRelAssociatesDocument,IfcRelAssociatesLibrary,IfcRelAssociatesMaterial))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RelatedObjects = mDatabase.extractJArray<IfcDefinitionSelect>(obj.GetValue("RelatedObjects", StringComparison.InvariantCultureIgnoreCase) as JArray);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			JArray array = new JArray();
			if (!mRelatedObjects.Contains(host.mIndex))
			{
				foreach (int i in mRelatedObjects)
					array.Add(mDatabase[i].getJson(this, processed));
				obj["RelatedObjects"] = array;
			}
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
			{
				token = jobj.GetValue("href", StringComparison.InvariantCultureIgnoreCase);
				if (token != null)
					mRelatingConstraint = token.Value<int>();
				else
					RelatingConstraint = mDatabase.parseJObject<IfcConstraint>(jobj);
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			setAttribute(obj, "Intent", Intent);
			if (host.mIndex != mRelatingConstraint)
				obj["RelatingConstraint"] = RelatingConstraint.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (host.mIndex != mRelatingDocument)
				obj["RelatingDocument"] = mDatabase[mRelatingDocument].getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (host.mIndex != mRelatingMaterial)
				obj["RelatingMaterial"] = mDatabase[mRelatingMaterial].getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (host.mIndex != mRelatingProfileProperties)
				obj["RelatingProfileProperties"] = mDatabase[mRelatingProfileProperties].getJson(this, processed);
			if (mProfileSectionLocation > 0 && host.mIndex != mProfileSectionLocation)
				obj["ProfileSectionLocation"] = mDatabase[mProfileSectionLocation].getJson(this, processed);
			if (mProfileOrientation > 0)
				obj["ProfileOrientation"] = mDatabase[mProfileOrientation].getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (host.mIndex != mRelatingElement)
				obj["RelatingElement"] = mDatabase[mRelatingElement].getJson(this, processed);
			if (host.mIndex != mRelatedStructuralActivity)
				obj["RelatedStructuralActivity"] = mDatabase[mRelatedStructuralActivity].getJson(this, processed);
			
		}
	}
	public partial class IfcRelContainedInSpatialStructure : IfcRelConnects
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RelatedElements = mDatabase.extractJArray<IfcProduct>(obj.GetValue("RelatedElements", StringComparison.InvariantCultureIgnoreCase) as JArray);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			List<IfcProduct> relatedElements = RelatedElements;
			JArray array = new JArray();
			foreach (IfcProduct product in relatedElements)
				array.Add(product.getJson(this, processed));
			obj["RelatedElements"] = array;
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
			RelatedDefinitions = mDatabase.extractJArray<IfcDefinitionSelect>(obj.GetValue("RelatedDefinitions", StringComparison.InvariantCultureIgnoreCase) as JArray);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mRelatingContext != host.mIndex)
				obj["RelatingContext"] = RelatingContext.getJson(this, processed);
			obj["RelatedDefinitions"] = new JArray(mRelatedDefinitions.ConvertAll(x => mDatabase[x].getJson(this, processed)));
		}
	}
	public partial class IfcRelDefinesByProperties : IfcRelDefines
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RelatedObjects = mDatabase.extractJArray<IfcObjectDefinition>(obj.GetValue("RelatedObjects", StringComparison.InvariantCultureIgnoreCase) as JArray);
			RelatingPropertyDefinition = mDatabase.parseJObject<IfcPropertySetDefinition>(obj.GetValue("RelatingPropertyDefinition", StringComparison.InvariantCultureIgnoreCase) as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["RelatingPropertyDefinition"] = RelatingPropertyDefinition.getJson(this, processed);
		}
	}
	public partial class IfcRelDefinesByTemplate : IfcRelDefines
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JArray array = obj.GetValue("RelatedPropertySets", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
				RelatedPropertySets = mDatabase.extractJArray<IfcPropertySetDefinition>(array);
			RelatingTemplate = extractObject<IfcPropertySetTemplate>(obj.GetValue("RelatingTemplate", StringComparison.InvariantCultureIgnoreCase) as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["RelatingTemplate"] = RelatingTemplate.getJson(this, processed);
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
				RelatedObjects = mDatabase.extractJArray<IfcObject>(array);
			RelatingType = extractObject<IfcTypeObject>(obj.GetValue("RelatingType", StringComparison.InvariantCultureIgnoreCase) as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["RelatingType"] = RelatingType.getJson(this, processed);
			//List<IfcObject> relatedObjects = RelatedObjects;
			//JArray array = new JArray();
			//foreach (IfcObject obj in relatedObjects)
			//	array.Add(obj.Index);
			//obj["RelatedObjects"] = array;
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
			RelatedObjects = mDatabase.extractJArray<IfcObjectDefinition>(obj.GetValue("RelatedObjects", StringComparison.InvariantCultureIgnoreCase) as JArray);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mRelatingObject != host.mIndex)
				obj["RelatingObject"] = RelatingObject.getJson(this, processed);
			List<IfcObjectDefinition> relatedObjects = RelatedObjects;
			JArray array = new JArray();
			foreach (IfcObjectDefinition od in relatedObjects)
				array.Add(od.getJson(this, processed));
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
			RelatedBuildings = mDatabase.extractJArray<IfcSpatialElement>(obj.GetValue("RelatedBuildings", StringComparison.InvariantCultureIgnoreCase) as JArray);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mRelatingSystem != host.mIndex)
				obj["RelatingSystem"] = RelatingSystem.getJson(this, processed);
			//List<IfcObjectDefinition> relatedObjects = RelatedObjects;
			//JArray array = new JArray();
			//foreach (IfcObjectDefinition od in relatedObjects)
			//	array.Add(od.getJson(this, processed));
			//obj["RelatedObjects"] = array;
		}
	}
	public partial class IfcRepresentation : BaseClassIfc, IfcLayeredItem // Abstract IFC4 ,SUPERTYPE OF (ONEOF(IfcShapeModel,IfcStyleModel));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("ContextOfItems", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
			{
				JToken tok = jobj["href"];
				if (tok != null)
					mContextOfItems = tok.Value<int>();
				else
				{
					IfcRepresentationContext rc = extractObject<IfcRepresentationContext>(jobj);
					IfcContext context = mDatabase.Context;
					if (context != null)
					{
							
						foreach(IfcRepresentationContext repContext in context.RepresentationContexts)
						{
							IfcGeometricRepresentationSubContext gsc = rc as IfcGeometricRepresentationSubContext;
							if (gsc != null)
							{
								IfcGeometricRepresentationContext grc = repContext as IfcGeometricRepresentationContext;
								if(grc != null)
								{
									foreach(IfcGeometricRepresentationSubContext sub in grc.HasSubContexts)
									{
										if(string.Compare(sub.mContextIdentifier,gsc.mContextIdentifier, true) == 0)
										{
											ContextOfItems = sub;
											mDatabase[rc.mIndex] = null;
										}
									}
								}
							}
						}
					}
					if (mContextOfItems == 0)
						ContextOfItems = rc;
				}
			}
			JToken token = obj.GetValue("RepresentationIdentifier", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				RepresentationIdentifier = token.Value<string>();
			token = obj.GetValue("RepresentationType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				RepresentationType = token.Value<string>();
			Items = mDatabase.extractJArray<IfcRepresentationItem>(obj.GetValue("Items", StringComparison.InvariantCultureIgnoreCase) as JArray);

			List<IfcPresentationLayerAssignment> assignments = mDatabase.extractJArray<IfcPresentationLayerAssignment>(obj.GetValue("LayerAssignments", StringComparison.InvariantCultureIgnoreCase) as JArray);
			foreach (IfcPresentationLayerAssignment a in assignments)
				a.addItem(this);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["ContextOfItems"] = ContextOfItems.getJson(this, processed);
			setAttribute(obj, "RepresentationIdentifier", RepresentationIdentifier);
			setAttribute(obj, "RepresentationType", RepresentationType);
			obj["Items"] = new JArray(Items.ConvertAll(x => x.getJson(this, processed)));

			if (mLayerAssignments.Count > 0)
				obj["LayerAssignments"] = new JArray(mLayerAssignments.ConvertAll(x => x.getJson(this, processed)));
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
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
		//internal List<IfcPresentationLayerAssignment> mLayerAssignments = new List<IfcPresentationLayerAssignment>();// null;
		//internal IfcStyledItem mStyledByItem = null;// : SET [0:1] OF IfcStyledItem FOR Item; 
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			List<IfcPresentationLayerAssignment> assignments = mDatabase.extractJArray<IfcPresentationLayerAssignment>(obj.GetValue("LayerAssignments", StringComparison.InvariantCultureIgnoreCase) as JArray);
			foreach (IfcPresentationLayerAssignment a in assignments)
				a.addItem(this);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mLayerAssignments.Count > 0)
				obj["LayerAssignments"] = new JArray(mLayerAssignments.ConvertAll(x => x.getJson(this, processed)));
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
				MappedRepresentation = mDatabase.parseJObject<IfcRepresentation>(jobj);
			JArray array = obj.GetValue("HasShapeAspects", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
			{
				List<IfcShapeAspect> aspects = mDatabase.extractJArray<IfcShapeAspect>(array);
				for(int icounter = 0; icounter < aspects.Count; icounter++)
					aspects[icounter].PartOfProductDefinitionShape = this;
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["MappingOrigin"] = mDatabase[mMappingOrigin].getJson(this, processed);
			obj["MappedRepresentation"] = MappedRepresentation.getJson(this, processed);
			if (mHasShapeAspects.Count > 0)
				obj["HasShapeAspects"] = new JArray(mHasShapeAspects.ConvertAll(x => x.getJson(this, processed)));
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
			RelatedResourceObjects = mDatabase.extractJArray<IfcResourceObjectSelect>(obj.GetValue("RelatedResourceObjects", StringComparison.InvariantCultureIgnoreCase) as JArray);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mRelatingConstraint != host.mIndex)
				obj["RelatingConstraint"] = RelatingConstraint.getJson(this, processed);
			JArray array = new JArray();
			foreach (IfcResourceObjectSelect r in RelatedResourceObjects)
			{
				IfcResourceConstraintRelationship rcr = r as IfcResourceConstraintRelationship;
				if (r.Index != host.mIndex)
					array.Add(mDatabase[r.Index].getJson(this, processed));
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);

			obj["Axis"] = Axis.getJson(this, processed);
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
				mOwnerHistory = token.Value<int>();	
			}
			token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
			token = obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Description = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["GlobalId"] = GlobalId;
			IfcOwnerHistory ownerHistory = OwnerHistory;
			if (ownerHistory != null)
				obj["OwnerHistory"] = ownerHistory.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["RoundingRadius"] = RoundingRadius;
		}
	}
}
