using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using GeometryGym.Mvd;

namespace GeometryGym.Ifc
{
	public abstract partial class IfcElement : IfcProduct, IfcStructuralActivityAssignmentSelect
	{
		public ValidationResult Validate_IsContainedIn<T>(bool recurseParent) where T : IfcSpatialElement
		{
			var containedInStructure = this.ContainedInStructure;
			if (containedInStructure != null)
			{
				var relatingStructure = containedInStructure.RelatingStructure;
				if (relatingStructure is T)
					return ValidationResult.Success;
				if (recurseParent && relatingStructure.Validate_DecomposesOrNests<T>(recurseParent) == ValidationResult.Success)
						return ValidationResult.Success;
			}
			else if (recurseParent)
			{
				var decomposes = this.Decomposes;
				if (decomposes != null)
				{
					var relatingObject = decomposes.RelatingObject as IfcElement;
					if (relatingObject != null)
						return relatingObject.Validate_IsContainedIn<T>(recurseParent);
				}
				var nests = this.Nests;
				if (nests != null)
				{
					var relatingObject = nests.RelatingObject as IfcElement;
					if (relatingObject != null)
						return relatingObject.Validate_IsContainedIn<T>(recurseParent);
				}
			}
			return new ValidationResult("Element " + this.StepClassName + " is not contained within spatial element of type " + typeof(T).Name);
		}
	}
	public abstract partial class IfcObjectDefinition : IfcRoot, IfcDefinitionSelect  //ABSTRACT SUPERTYPE OF (ONEOF ((IfcContext, IfcObject, IfcTypeObject))))
	{
		public ValidationResult Validate_DecomposesOrNests<T>(bool recurseParent) where T : IfcObjectDefinition
		{
			var decomposes = this.Decomposes;
			if(decomposes != null)
			{
				var relatingObject = decomposes.RelatingObject;
				if (relatingObject is T)
					return ValidationResult.Success;
				if (recurseParent)
					return relatingObject.Validate_DecomposesOrNests<T>(recurseParent);
			}
			var nests = this.Nests;
			if(nests != null)
			{
				var relatingObject = nests.RelatingObject;
				if (relatingObject is T)
					return ValidationResult.Success;
				if (recurseParent)
					return relatingObject.Validate_DecomposesOrNests<T>(recurseParent);
			}
			return new ValidationResult("Object " + this.StepClassName + " does not decompose or nest an object of type " + typeof(T).Name);
		}
		public Dictionary<IfcRoot, List<ValidationResult>> Validate(List<IConceptRoot> conceptRoots, List<IfcPropertySetTemplate> propertySetTemplates)
		{
			Dictionary<IfcRoot, List<ValidationResult>> results = new Dictionary<IfcRoot, List<ValidationResult>>();
			this.validateWorker(conceptRoots, propertySetTemplates, ref results);
			return results;
		}
		private void validateWorker(List<IConceptRoot> conceptRoots, List<IfcPropertySetTemplate> propertySetTemplates, ref Dictionary<IfcRoot, List<ValidationResult>> results)
		{
			List<ValidationResult> validationResults = new List<ValidationResult>();
			foreach (var propertySetTemplate in propertySetTemplates)
			{
				List<ValidationResult> templateResults = validatePropertySetTemplate(propertySetTemplate);
				if(templateResults != null)
					validationResults.AddRange(templateResults);
			}

			foreach (var conceptRoot in conceptRoots)
			{
				List<ValidationResult> rootResults = conceptRoot.Validate(this);
				if(rootResults != null)
				{
					validationResults.AddRange(rootResults);
				}
			}

			if (validationResults.Count > 0)
				results[this] = validationResults;
				
			foreach (var od in IsDecomposedBy.SelectMany(x => x.RelatedObjects))
				od.validateWorker(conceptRoots, propertySetTemplates, ref results);

			foreach (var od in IsNestedBy.SelectMany(x => x.RelatedObjects))
				od.validateWorker(conceptRoots, propertySetTemplates, ref results);

			IfcSpatialElement spatialElement = this as IfcSpatialElement;
			if (spatialElement != null)
			{
				foreach (var relatedElement in spatialElement.ContainsElements.SelectMany(x => x.RelatedElements))
				{
					relatedElement.validateWorker(conceptRoots, propertySetTemplates, ref results);
				}
				foreach (var relatingSystem in spatialElement.ServicedBySystems.Select(x => x.RelatingSystem))
					relatingSystem.validateWorker(conceptRoots, propertySetTemplates, ref results);
			}

		}
		internal List<ValidationResult> validatePropertySetTemplate(IfcPropertySetTemplate propertySetTemplate)
		{
			List<ValidationResult> result = new List<ValidationResult>();
			if (propertySetTemplate.IsApplicable(this))
			{
				IfcPropertySet pset = FindPropertySet(propertySetTemplate.Name) as IfcPropertySet;
				if (pset == null)
					result.Add(new ValidationResult("Missing Property Set " + propertySetTemplate.Name));
				else
				{
					foreach (var propertyTemplate in propertySetTemplate.HasPropertyTemplates.Values)
					{
						IfcSimplePropertyTemplate simplePropertyTemplate = propertyTemplate as IfcSimplePropertyTemplate;
						if(simplePropertyTemplate != null)
						{
							if (simplePropertyTemplate.TemplateType == IfcSimplePropertyTemplateTypeEnum.P_SINGLEVALUE)
							{
								IfcPropertySingleValue property = pset.FindProperty(propertyTemplate.Name) as IfcPropertySingleValue;
								if (property == null)
									result.Add(new ValidationResult("Missing Property " + propertySetTemplate.Name + " : " + propertyTemplate.Name));
								else
								{
									IfcValue value = property.NominalValue;
									if (value == null)
										result.Add(new ValidationResult("Missing Property Value " + propertySetTemplate.Name + " : " + propertyTemplate.Name));
									else
									{
										string valueString = value.ValueString;
										if (string.IsNullOrEmpty(valueString))
											result.Add(new ValidationResult("Missing Property Value " + propertySetTemplate.Name + " : " + propertyTemplate.Name));
										else
										{
											string primary = simplePropertyTemplate.PrimaryMeasureType;
											if (!string.IsNullOrEmpty(primary))
											{
												string valueClass = value.StepClassName;
												if (string.Compare(valueClass, primary, true) != 0)
													result.Add(new ValidationResult(" Unexpected value type " + valueClass + " for " + propertySetTemplate.Name + " : " + propertyTemplate.Name));
											}
										}
									}
								}
							}
							else
								result.Add(new ValidationResult("!!! Unhandled PropertyTemplate " + propertyTemplate.ToString()));
						}
						else
							result.Add(new ValidationResult("!!! Unhandled PropertyTemplate " + propertyTemplate.ToString()));
					}
				}
			}
			return result;
		}

		public ValidationResult ValidateIsObject<T>(bool acceptSubTypes) where T : IfcObjectDefinition 
		{
			if (this is T)
			{
				if (!acceptSubTypes && this.GetType().IsSubclassOf(typeof(T)))
					return new ValidationResult("Object (" + StepClassName + ") is not " + typeof(T).Name);
				return ValidationResult.Success;
			}
			return new ValidationResult("Object (" + StepClassName + ") is not " + typeof(T).Name);
		}
	}
	public abstract partial class IfcProduct : IfcObject, IfcProductSelect
	{
		private IfcRepresentationItem SingleBodyRepresentationItem(ref ValidationResult validation)
		{
			var representation = Representation;
			if (representation == null)
			{
				validation = new ValidationResult("Missing Product Representation");
				return null;
			}
			var representations = representation.Representations.Where(x => string.Compare(x.RepresentationIdentifier, "Body", true) == 0).ToList();
			if (representations.Count == 0)
			{
				validation = new ValidationResult("Missing Body Representation");
				return null;
			}
			if (representations.Count > 1)
			{
				validation = new ValidationResult("Multiple Body Representations");
				return null;
			}
			List<IfcRepresentationItem> items = representations[0].Items.ToList();
			if (items.Count == 0)
			{
				validation = new ValidationResult("Missing Body Representation");
				return null;
			}
			if (items.Count > 1)
			{
				validation = new ValidationResult("Multiple Body Representations");
				return null;
			}
			return items[0];
		}
		public ValidationResult Validate_SingleBodyRepresentation<T>(bool acceptSubTypes) where T : IfcRepresentationItem
		{
			ValidationResult result = null;
			IfcRepresentationItem singleModelRepresenationItem = SingleBodyRepresentationItem(ref result);
			if (singleModelRepresenationItem == null)
				return result;
			if (!(singleModelRepresenationItem is T))
				return new ValidationResult("Single model representation item (" + singleModelRepresenationItem.StepClassName + ") is not " + typeof(T).Name);
			if(!acceptSubTypes)
			{
				if(singleModelRepresenationItem.GetType().IsSubclassOf(typeof(T)))
					return new ValidationResult("Single model representation item (" + singleModelRepresenationItem.StepClassName + ") is subclass of " + typeof(T).Name);
			}

			return ValidationResult.Success;
		}
		public ValidationResult Validate_SingleBodyRepresentation<T, U>(bool acceptSubTypes) where T : IfcRepresentationItem where U : IfcRepresentationItem
		{
			ValidationResult result = null;
			IfcRepresentationItem singleModelRepresenationItem = SingleBodyRepresentationItem(ref result);
			if (singleModelRepresenationItem == null)
				return result;
			if (!(singleModelRepresenationItem is T || singleModelRepresenationItem is U))
				return new ValidationResult("Single model representation item (" + singleModelRepresenationItem.StepClassName + ") is not " + typeof(T).Name + " or " + typeof(U).Name);
			if (!acceptSubTypes)
			{
				if(singleModelRepresenationItem.GetType().IsSubclassOf(typeof(T)))
					return new ValidationResult("Single model representation item (" + singleModelRepresenationItem.StepClassName + ") is subclass of " + typeof(T).Name);
				if(singleModelRepresenationItem.GetType().IsSubclassOf(typeof(U)))
					return new ValidationResult("Single model representation item (" + singleModelRepresenationItem.StepClassName + ") is subclass of " + typeof(U).Name);
			}
			return ValidationResult.Success;
		}
		public ValidationResult Validate_SingleBodyRepresentation<T, U, V>(bool acceptSubTypes) where T : IfcRepresentationItem where U : IfcRepresentationItem where V : IfcRepresentationItem
		{
			ValidationResult result = null;
			IfcRepresentationItem singleModelRepresenationItem = SingleBodyRepresentationItem(ref result);
			if (singleModelRepresenationItem == null)
				return result;
			if (!(singleModelRepresenationItem is T || singleModelRepresenationItem is U || singleModelRepresenationItem is V))
				return new ValidationResult("Single Body representation item (" + singleModelRepresenationItem.StepClassName + ") is not " + typeof(T).Name + " or " + typeof(U).Name + " or " + typeof(V).Name);
			if (!acceptSubTypes)
			{
				if(singleModelRepresenationItem.GetType().IsSubclassOf(typeof(T)))
					return new ValidationResult("Single Body representation item (" + singleModelRepresenationItem.StepClassName + ") is subclass of " + typeof(T).Name);
				if(singleModelRepresenationItem.GetType().IsSubclassOf(typeof(U)))
					return new ValidationResult("Single Body representation item (" + singleModelRepresenationItem.StepClassName + ") is subclass of " + typeof(U).Name);
				if(singleModelRepresenationItem.GetType().IsSubclassOf(typeof(V)))
					return new ValidationResult("Single Body representation item (" + singleModelRepresenationItem.StepClassName + ") is subclass of " + typeof(V).Name);
			}
			return ValidationResult.Success;
		}
	}
}
