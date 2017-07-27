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
	public partial class IfcMapConversion : IfcCoordinateOperation //IFC4
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["MappingSource"] = MappingSource.getJson(this, processed);
			obj["MappingTarget"] = MappingTarget.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);

			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Category", Category);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);

			if (mMaterial > 0)
				obj["Material"] = Material.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["MaterialLayers"] = new JArray(MaterialLayers.ToList().ConvertAll(x => x.getJson(this, processed)));
			setAttribute(obj, "LayerSetName", Name);
			setAttribute(obj, "Description", Description);
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
			JObject jo = obj.GetValue("UnitComponent", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jo != null)
				UnitComponent = mDatabase.parseJObject<IfcUnit>(jo);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			IfcValue value = mValueComponent; 
			if (value != null)
				obj["ValueComponent"] = DatabaseIfc.extract(value);
			IfcUnit unit = UnitComponent;
			if (unit != null)
				obj["UnitComponent"] = mDatabase[mUnitComponent].getJson(this, processed);
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
					DataValue = mDatabase.parseJObject<IfcMetricValueSelect>(jobj);
				//else

			}
			JObject jo = obj.GetValue("ReferencePath", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jo != null)
				ReferencePath = mDatabase.parseJObject<IfcReference>(jo);
		}
		
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["BenchMark"] = mBenchMark.ToString();
			setAttribute(obj, "ValueSource", ValueSource);
			if (mDataValue > 0)
				obj["DataValue"] = mDatabase[mDataValue].getJson(this, processed);
			else if(mDataValueValue != null)
				obj["DataValue"] = DatabaseIfc.extract(mDataValueValue);
			if (mReferencePath > 0)
				obj["ReferencePath"] = ReferencePath.getJson(this, processed);
		}
	}
}
