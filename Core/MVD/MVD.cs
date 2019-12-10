using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

using GeometryGym.Ifc;

namespace GeometryGym.Mvd
{
	public enum StatusEnum
	{
		Sample = 0, // default
		Proposal = 1,
		Draft = 2,
		Candidate = 3,
		Final = 4,
		Deprecated = -1,
	}
	public abstract class Identity
	{
		public Guid Uuid { get; set; }
		public string Name { get; set; }
		public string Code { get; set; } // e.g. 'bsi-100'
		public string Version { get; set; }
		public StatusEnum Status { get; set; } // e.g. 'draft'
		public string Author { get; set; }
		public string Owner { get; set; } // e.g. 'buildingSMART international'
		public string Copyright { get; set; }

		public bool ShouldSerializeUuid()
		{
			return Uuid != Guid.Empty;
		}
		public bool ShouldSerializeStatus()
		{
			return Status != StatusEnum.Sample;
		}
		public Identity(string name, Guid uuid)
		{
			Name = name;
			Uuid = uuid;
		}
	}
	public abstract class Element : Identity
	{
		public List<Definition> Definitions { get; set; }

		public Element(string name, Guid uuid)
			: base(name, uuid)
		{
			Definitions = new List<Definition>();
		}

		public bool ShouldSerializeDefinitions()
		{
			return null != this.Definitions && this.Definitions.Any();
		}
	}
	public enum ApplicabilityEnum
	{
		Both = 0,
		Export = 1,
		Import = 2,
	}

	public enum RequirementEnum
	{
		Mandatory = 1,
		Recommended = 2,
		NotRelevant = 3,
		NotRecommended = 4,
		Excluded = 5,
	}
	public delegate bool TemplateRule<T>(T item);

	public class BaseConcept
	{
		public Guid Ref { get; set; }
	}
	public class Concept<T> : Element where T : IfcRoot
	{
		public TemplateRef Template { get; set; }// links to ConceptTemplate 
		public RequirementEnum Requirement { get; set; }
		public TemplateRule<T> TemplateRules { get; set; }

		public List<Concept<T>> SubConcepts { get; set; }// added v3.8
		public bool Override { get; set; }// added in v5.6
		public BaseConcept BaseConcept { get; set; }

		public Concept(string name, Guid uuid)
			: base(name, uuid)
		{
			SubConcepts = new List<Concept<T>>();
		}
		public bool ShouldSerializeSubConcepts()
		{
			return null != this.SubConcepts && this.SubConcepts.Any();
		}
	}

	public interface IConceptRoot
	{
		List<ValidationResult> Validate(IfcRoot root);
	}
	public abstract class ConceptRoot<T> : Element, IConceptRoot where T : IfcRoot
	{
		public ApplicabilityRules<T> Applicability { get; set; }
		public List<Concept<T>> Concepts { get; set; }

		public ConceptRoot(Guid guid)
			: base("", guid)
		{
			Name = this.GetType().Name;
		}

		public List<ValidationResult> Validate(IfcRoot root)
		{
			T obj = root as T;
			if (obj == null)
				return null;
			// Test Applicability
			List<ValidationResult> results = new List<ValidationResult>();
			foreach(ValidationResult result in ValidateWorker(obj))
			{
				if (result == ValidationResult.Success)
					continue;
				results.Add(new ValidationResult(Uuid.ToString() + " | " + Name + " | " + result.ErrorMessage));
			}
			return results;
		}
		protected abstract List<ValidationResult> ValidateWorker(T obj);
	}

	public class ApplicabilityRules<T> where T : IfcRoot
	{
		public Definition Definitions { get; set; }
		public TemplateRef Template { get; set; }
		public TemplateRule<T> TemplateRule { get; set; }
	}
	public class Definition
	{
		public string Body { get; set; }
		public List<Link> Links { get; set; }
		public string Tags { get; set; }

		public Definition()
		{
			Links = new List<Link>();
		}
	}

	public enum CategoryEnum
	{
		definition = 0,
		agreement = 1,
		diagram = 2,
		instantiation = 3,
		example = 4,
	}
	public class Link
	{
		public string Lang { get; set; }
		public CategoryEnum Category { get; set; }
		public string Title { get; set; }
		public string Href { get; set; }
		public string Content { get; set; }
	}

	public class TemplateRef
	{
		public Guid Ref { get; set; }
	}



	public class ExchangeRequirements
	{
		protected List<IConceptRoot> Concepts { get; }
		protected List<IfcPropertySetTemplate> PropertyTemplates { get; }

		public List<IfcClassificationReference> ApplicableProcess { get; }
		public List<IfcClassificationReference> Senders { get; }
		public List<IfcClassificationReference> Receivers { get; }
		public List<IfcClassificationReference> Activities { get; }

		public ExchangeRequirements()
		{
			Concepts = new List<IConceptRoot>();
			PropertyTemplates = new List<IfcPropertySetTemplate>();
			ApplicableProcess = new List<IfcClassificationReference>();
			Senders = new List<IfcClassificationReference>();
			Receivers = new List<IfcClassificationReference>();
			Activities = new List<IfcClassificationReference>();
		}

		public Dictionary<IfcRoot, List<ValidationResult>> Validate(IfcObjectDefinition objectDefinition)
		{
			return objectDefinition.Validate(Concepts, PropertyTemplates);
		}

	}

	public class ConceptGridAttributes : ConceptRoot<IfcGrid>
	{
		public ConceptGridAttributes()
			: base(new Guid("9c7b470d-978a-40fa-a777-449b40588800"))
		{

		}
		protected override List<ValidationResult> ValidateWorker(IfcGrid obj)
		{
			List<ValidationResult> result = new List<ValidationResult>();
			foreach (IfcGridAxis axis in obj.UAxes)
			{
				if (string.IsNullOrEmpty(axis.AxisTag))
					result.Add(new ValidationResult("Empty U Axis Tag"));
				ValidationResult validation = ValidateGridAxisCurve(axis);
				if (validation != ValidationResult.Success)
					result.Add(validation);
			}
			foreach (IfcGridAxis axis in obj.VAxes)
			{
				if (string.IsNullOrEmpty(axis.AxisTag))
					result.Add(new ValidationResult("Empty V Axis Tag"));
				ValidationResult validation = ValidateGridAxisCurve(axis);
				if (validation != ValidationResult.Success)
					result.Add(validation);
			}
			foreach (IfcGridAxis axis in obj.WAxes)
			{
				if (string.IsNullOrEmpty(axis.AxisTag))
					result.Add(new ValidationResult("Empty W Axis Tag"));
				ValidationResult validation = ValidateGridAxisCurve(axis);
				if (validation != ValidationResult.Success)
					result.Add(validation);
			}
			return result;
		}

		private ValidationResult ValidateGridAxisCurve(IfcGridAxis axis)
		{
			if (axis.AxisCurve is IfcIndexedPolyCurve || axis.AxisCurve is IfcCircle)
				return ValidationResult.Success;
			return new ValidationResult("");
		}
	}

}
