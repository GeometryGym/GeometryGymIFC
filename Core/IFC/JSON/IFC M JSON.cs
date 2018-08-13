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
using GeometryGym.STEP;
using Newtonsoft.Json.Linq;

namespace GeometryGym.Ifc
{
	public abstract partial class IfcManifoldSolidBrep : IfcSolidModel //ABSTRACT SUPERTYPE OF(ONEOF(IfcAdvancedBrep, IfcFacetedBrep))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Outer", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Outer = mDatabase.parseJObject<IfcClosedShell>(jobj);

		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Outer"] = Outer.getJson(this, options);
		}
	}
	public partial class IfcMapConversion : IfcCoordinateOperation //IFC4
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Eastings"] = mEastings;
			obj["Northings"] = mNorthings;
			obj["OrthogonalHeight"] = mOrthogonalHeight;
			if (!double.IsNaN(mXAxisAbscissa))
				obj["XAxisAbscissa"] = mXAxisAbscissa;
			if (!double.IsNaN(mXAxisOrdinate))
				obj["XAxisOrdinate"] = mXAxisOrdinate;
			if (!double.IsNaN(mScale))
				obj["Scale"] = mScale;
		}
	}
	public partial class IfcMappedItem : IfcRepresentationItem
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("MappingSource", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				MappingSource = mDatabase.parseJObject<IfcRepresentationMap>(jobj);
			jobj = obj.GetValue("MappingTarget", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				MappingTarget = mDatabase.parseJObject<IfcCartesianTransformationOperator3D>(jobj);

		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["MappingSource"] = MappingSource.getJson(this, options);
			obj["MappingTarget"] = MappingTarget.getJson(this, options);
		}
	}
	public partial class IfcMaterial : IfcMaterialDefinition
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
			token = obj.GetValue("Category", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Category = token.Value<string>();

		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Category", Category);
		}
	}

	public abstract partial class IfcMaterialDefinition : BaseClassIfc, IfcObjectReferenceSelect, IfcMaterialSelect, IfcResourceObjectSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcMaterial ,IfcMaterialConstituent ,IfcMaterialConstituentSet ,IfcMaterialLayer ,IfcMaterialProfile ,IfcMaterialProfileSet));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			HasProperties.AddRange(mDatabase.extractJArray<IfcMaterialProperties>(obj.GetValue("HasProperties", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			if (mHasProperties.Count > 0)
				obj["HasProperties"] = new JArray(HasProperties.ToList().ConvertAll(x => x.getJson(this, options)));
		}
	}
	public partial class IfcMaterialLayer : IfcMaterialDefinition
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Material", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Material = mDatabase.parseJObject<IfcMaterial>(jobj);
			JToken token = obj.GetValue("LayerThickness", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				LayerThickness = token.Value<double>();
			token = obj.GetValue("IsVentilated", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcLogicalEnum>(token.Value<string>(), out mIsVentilated);
			token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
			token = obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Description = token.Value<string>();
			token = obj.GetValue("Category", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Category = token.Value<string>();
			token = obj.GetValue("Priority", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Priority = token.Value<double>();

		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			if (mMaterial > 0)
				obj["Material"] = Material.getJson(this, options);
			obj["LayerThickness"] = mLayerThickness;
			obj["IsVentilated"] = mIsVentilated.ToString();
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Category", Category);
			if (!double.IsNaN(mPriority))
				obj["Priority"] = mPriority;
		}
	}
	public partial class IfcMaterialLayerSet : IfcMaterialDefinition
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mDatabase.extractJArray<IfcMaterialLayer>(obj.GetValue("MaterialLayers", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addMaterialLayer(x));
			LayerSetName = extractString(obj.GetValue("LayerSetName", StringComparison.InvariantCultureIgnoreCase));
			Description = extractString(obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["MaterialLayers"] = new JArray(MaterialLayers.ToList().ConvertAll(x => x.getJson(this, options)));
			setAttribute(obj, "LayerSetName", Name);
			setAttribute(obj, "Description", Description);
		}
	}
	public partial class IfcMaterialProfile : IfcMaterialDefinition // IFC4
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Name = extractString(obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase));
			Description = extractString(obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase));
			JObject jobj = obj.GetValue("Material", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Material = mDatabase.parseJObject<IfcMaterial>(jobj);
			jobj = obj.GetValue("Profile", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Profile = mDatabase.parseJObject<IfcProfileDef>(jobj);
			JToken token = obj.GetValue("Priority", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Priority = token.Value<int>();
			Category = extractString(obj.GetValue("Category", StringComparison.InvariantCultureIgnoreCase));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			if (mMaterial != null)
				obj["Material"] = Material.getJson(this, options);
			if (mProfile != null)
				obj["Profile"] = Profile.getJson(this, options);
			if (mPriority != int.MaxValue)
				obj["Priority"] = Priority;
			base.setAttribute(obj, "Category", Category);
		}
	}
	public partial class IfcMaterialProfileSet : IfcMaterialDefinition //IFC4
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Name = extractString(obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase));
			Description = extractString(obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase));
			JArray array = obj.GetValue("MaterialProfiles") as JArray;
			if(array != null)
			{
				foreach(JObject o in array)
				{
					IfcMaterialProfile mp = mDatabase.parseJObject<IfcMaterialProfile>(o);
					if (mp != null)
						MaterialProfiles.Add(mp);
				}
			}
			JObject jobj = obj.GetValue("CompositeProfile", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				CompositeProfile = mDatabase.parseJObject<IfcCompositeProfileDef>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			obj["MaterialProfiles"] = new JArray(MaterialProfiles.ConvertAll(x => x.getJson(this, options)));
			if (mCompositeProfile != null)
				obj["CompositeProfile"] = CompositeProfile.getJson(this, options);
		}
	}
	public partial class IfcMaterialProfileSetUsage : IfcMaterialUsageDefinition //IFC4
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("ForProfileSet", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				ForProfileSet = mDatabase.parseJObject<IfcMaterialProfileSet>(jobj);
			JToken token = obj.GetValue("CardinalPoint", StringComparison.InvariantCultureIgnoreCase) as JToken;
			if (token != null)
				mCardinalPoint = (IfcCardinalPointReference) token.Value<int>();
			token = obj.GetValue("ReferenceExtent", StringComparison.InvariantCultureIgnoreCase) as JToken;
			if (token != null)
				mReferenceExtent = token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ForProfileSet"] = ForProfileSet.getJson(this, options);
			if (mCardinalPoint != IfcCardinalPointReference.DEFAULT)
				obj["CardinalPoint"] = (int)mCardinalPoint;
			if (!double.IsNaN(mReferenceExtent))
				obj["ReferenceExtent"] = mReferenceExtent;
		}
	}

	public partial class IfcMeasureWithUnit : BaseClassIfc, IfcAppliedValueSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("ValueComponent", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				mValueComponent = DatabaseIfc.ParseValue(jobj);
			jobj = obj.GetValue("UnitComponent", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				UnitComponent = mDatabase.parseJObject<IfcUnit>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			IfcValue value = mValueComponent; 
			if (value != null)
				obj["ValueComponent"] = DatabaseIfc.extract(value);
			IfcUnit unit = UnitComponent;
			if (unit != null)
				obj["UnitComponent"] = mDatabase[mUnitComponent].getJson(this, options);
		}
	}
	public partial class IfcMetric : IfcConstraint
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("BenchMark", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcBenchmarkEnum>(token.Value<string>(), out mBenchMark);
			token = obj.GetValue("ValueSource", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ValueSource = token.Value<string>();
			token = obj.GetValue("DataValue", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
			{
				JObject jobj = token as JObject;
				if (jobj != null)
				{
					BaseClassIfc bc = mDatabase.parseJObject<BaseClassIfc>(jobj);
					IfcMetricValueSelect value = bc as IfcMetricValueSelect;
					if (value != null)
						DataValue = value;
					else
						mDataValueValue = DatabaseIfc.ParseValue(jobj);
				}
				//else

			}
			JObject jo = obj.GetValue("ReferencePath", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jo != null)
				ReferencePath = mDatabase.parseJObject<IfcReference>(jo);
		}
		
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["BenchMark"] = mBenchMark.ToString();
			setAttribute(obj, "ValueSource", ValueSource);
			if (mDataValue > 0)
				obj["DataValue"] = mDatabase[mDataValue].getJson(this, options);
			else if(mDataValueValue != null)
				obj["DataValue"] = DatabaseIfc.extract(mDataValueValue);
			if (mReferencePath > 0)
				obj["ReferencePath"] = ReferencePath.getJson(this, options);
		}
	}
}
