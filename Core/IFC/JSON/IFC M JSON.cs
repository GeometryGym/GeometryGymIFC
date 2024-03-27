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

#if (NET || !NOIFCJSON)
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
	public partial class IfcManifoldSolidBrep 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Outer"] as JsonObject;
			if (jobj != null)
				Outer = mDatabase.ParseJsonObject<IfcClosedShell>(jobj);

		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Outer"] = Outer.getJson(this, options);
		}
	}
	public partial class IfcMapConversion
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
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
			if (!double.IsNaN(mScaleY))
				obj["ScaleY"] = mScaleY;
			if (!double.IsNaN(mScaleZ))
				obj["ScaleZ"] = mScaleZ;
		}
	}
	public partial class IfcMappedItem
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["MappingSource"] as JsonObject;
			if (jobj != null)
				MappingSource = mDatabase.ParseJsonObject<IfcRepresentationMap>(jobj);
			jobj = obj["MappingTarget"] as JsonObject;
			if (jobj != null)
				MappingTarget = mDatabase.ParseJsonObject<IfcCartesianTransformationOperator3D>(jobj);

		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["MappingSource"] = MappingSource.getJson(this, options);
			obj["MappingTarget"] = MappingTarget.getJson(this, options);
		}
	}
	public partial class IfcMarineFacility
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
				Enum.TryParse<IfcMarineFacilityTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcMaterial
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
			node = obj["Category"];
			if (node != null)
				Category = node.GetValue<string>();
			JsonObject jobj = obj["HasRepresentation"] as JsonObject;
			if (jobj != null)
				HasRepresentation = mDatabase.ParseJsonObject<IfcMaterialDefinitionRepresentation>(jobj);

		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Category", Category);

			if (mHasRepresentation != null)
				obj["HasRepresentation"] = mHasRepresentation.getJson(this, options);
		}
	}
	public partial class IfcMaterialConstituent
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Name = extractString(obj["Name"]);
			Description = extractString(obj["Description"]);
			JsonObject jobj = obj["Material"] as JsonObject;
			if (jobj != null)
				Material = mDatabase.ParseJsonObject<IfcMaterial>(jobj);
			var node = obj["Fraction"];
			if (node != null)
				Fraction = node.GetValue<double>();
			Category = extractString(obj["Category"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			if (mMaterial != null)
				obj["Material"] = Material.getJson(this, options);
			setAttribute(obj, "Category", Category);
		}
	}
	public partial class IfcMaterialConstituentSet
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mDatabase.extractJsonArray<IfcMaterialConstituent>(obj["MaterialConstituents"] as JsonArray).ForEach(x => MaterialConstituents[x.Name] = x);
			Name = extractString(obj["Name"]);
			Description = extractString(obj["Description"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "MaterialConstituents", MaterialConstituents.Values, this, options);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
		}
	}
	public partial class IfcMaterialDefinition 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			HasProperties.AddRange(mDatabase.extractJsonArray<IfcMaterialProperties>(obj["HasProperties"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "HasProperties", HasProperties, this, options);
		}
	}
	public partial class IfcMaterialLayer
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Material"] as JsonObject;
			if (jobj != null)
				Material = mDatabase.ParseJsonObject<IfcMaterial>(jobj);
			mLayerThickness = extractDouble(obj["LayerThickness"]);
			var node = obj["IsVentilated"];
			if (node != null)
				Enum.TryParse<IfcLogicalEnum>(node.GetValue<string>(), out mIsVentilated);
			Name = extractString(obj["Name"]);
			Description = extractString(obj["Description"]);
			Category = extractString(obj["Category"]);
			Priority = extractInt( obj["Priority"], int.MinValue);

		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			if (mMaterial != null)
				obj["Material"] = Material.getJson(this, options);
			obj["LayerThickness"] = mLayerThickness;
			obj["IsVentilated"] = mIsVentilated.ToString();
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Category", Category);
			if (mPriority != int.MinValue)
				obj["Priority"] = mPriority;
		}
	}
	public partial class IfcMaterialLayerSet
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			MaterialLayers.AddRange(mDatabase.extractJsonArray<IfcMaterialLayer>(obj["MaterialLayers"] as JsonArray));
			LayerSetName = extractString(obj["LayerSetName"]);
			Description = extractString(obj["Description"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "MaterialLayers", MaterialLayers, this, options);
			setAttribute(obj, "LayerSetName", Name);
			setAttribute(obj, "Description", Description);
		}
	}
	public partial class IfcMaterialLayerSetUsage
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["ForLayerSet"] as JsonObject;
			if (jobj != null)
				ForLayerSet = mDatabase.ParseJsonObject<IfcMaterialLayerSet>(jobj);
			var node = obj["LayerSetDirection"];
			if (node != null)
				Enum.TryParse<IfcLayerSetDirectionEnum>(node.GetValue<string>(), true, out mLayerSetDirection);
			node = obj["DirectionSense"];
			if (node != null)
				Enum.TryParse<IfcDirectionSenseEnum>(node.GetValue<string>(), true, out mDirectionSense);
			mOffsetFromReferenceLine = extractDouble(obj["OffsetFromReferenceLine"]);
			mReferenceExtent = extractDouble(obj["ReferenceExtent"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ForLayerSet"] = ForLayerSet.getJson(this, options);
			obj["LayerSetDirection"] = mLayerSetDirection.ToString();
			obj["DirectionSense"] = mDirectionSense.ToString();
			obj["OffsetFromReferenceLine"] = mOffsetFromReferenceLine;
			if(!double.IsNaN(mReferenceExtent))
				obj["ReferenceExtent"] = mReferenceExtent;
		}
	}
	public partial class IfcMaterialProfile 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Name = extractString(obj["Name"]);
			Description = extractString(obj["Description"]);
			JsonObject jobj = obj["Material"] as JsonObject;
			if (jobj != null)
				Material = mDatabase.ParseJsonObject<IfcMaterial>(jobj);
			jobj = obj["Profile"] as JsonObject;
			if (jobj != null)
				Profile = mDatabase.ParseJsonObject<IfcProfileDef>(jobj);
			mPriority = extractInt(obj["Priority"], int.MinValue);
			Category = extractString(obj["Category"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			if (mMaterial != null)
				obj["Material"] = Material.getJson(this, options);
			if (mProfile != null)
				obj["Profile"] = Profile.getJson(this, options);
			if (mPriority != int.MinValue)
				obj["Priority"] = Priority;
			base.setAttribute(obj, "Category", Category);
		}
	}
	public partial class IfcMaterialProfileSet
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Name = extractString(obj,"Name");
			Description = extractString(obj, "Description");
			JsonArray array = obj["MaterialProfiles"] as JsonArray;
			if(array != null)
			{
				foreach(JsonObject o in array)
				{
					IfcMaterialProfile mp = mDatabase.ParseJsonObject<IfcMaterialProfile>(o);
					if (mp != null)
						MaterialProfiles.Add(mp);
				}
			}
			JsonObject jobj = obj["CompositeProfile"] as JsonObject;
			if (jobj != null)
				CompositeProfile = mDatabase.ParseJsonObject<IfcCompositeProfileDef>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			createArray(obj, "MaterialProfiles", MaterialProfiles, this, options);
			if (mCompositeProfile != null)
				obj["CompositeProfile"] = CompositeProfile.getJson(this, options);
		}
	}
	public partial class IfcMaterialProfileSetUsage
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["ForProfileSet"] as JsonObject;
			if (jobj != null)
				ForProfileSet = mDatabase.ParseJsonObject<IfcMaterialProfileSet>(jobj);
			var node = obj["CardinalPoint"];
			if (node != null)
				mCardinalPoint = (IfcCardinalPointReference) node.GetValue<int>();
			mReferenceExtent = extractDouble(obj["ReferenceExtent"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ForProfileSet"] = ForProfileSet.getJson(this, options);
			if (mCardinalPoint != IfcCardinalPointReference.DEFAULT)
				obj["CardinalPoint"] = (int)mCardinalPoint;
			if (!double.IsNaN(mReferenceExtent))
				obj["ReferenceExtent"] = mReferenceExtent;
		}
	}

	public partial class IfcMeasureWithUnit
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["ValueComponent"] as JsonObject;
			if (jobj != null)
				mValueComponent = DatabaseIfc.ParseValue(jobj);
			jobj = obj["UnitComponent"] as JsonObject;
			if (jobj != null)
				UnitComponent = mDatabase.ParseJsonObject<IfcUnit>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			IfcValue value = mValueComponent; 
			if (value != null)
				obj["ValueComponent"] = DatabaseIfc.extract(value);
			IfcUnit unit = UnitComponent;
			if (unit != null)
				obj["UnitComponent"] = mUnitComponent.getJson(this, options);
		}
	}
	public partial class IfcMetric
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["BenchMark"];
			if (node != null)
				Enum.TryParse<IfcBenchmarkEnum>(node.GetValue<string>(), out mBenchMark);
			ValueSource = extractString(obj["ValueSource"]);
			node = obj["DataValue"];
			if (node != null)
			{
				JsonObject jobj = node as JsonObject;
				if (jobj != null)
				{
					BaseClassIfc bc = mDatabase.ParseJsonObject<BaseClassIfc>(jobj);
					IfcMetricValueSelect value = bc as IfcMetricValueSelect;
					if (value != null)
						DataValue = value;
					else
						mDataValue = DatabaseIfc.ParseValue(jobj) as IfcMetricValueSelect;
				}
				//else

			}
			JsonObject jo = obj["ReferencePath"] as JsonObject;
			if (jo != null)
				ReferencePath = mDatabase.ParseJsonObject<IfcReference>(jo);
		}
		
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["BenchMark"] = mBenchMark.ToString();
			setAttribute(obj, "ValueSource", ValueSource);
			if (mDataValue is BaseClassIfc o)
				obj["DataValue"] = o.getJson(this, options);
			else if(mDataValue is IfcValue val)
				obj["DataValue"] = DatabaseIfc.extract(val);
			if (mReferencePath != null)
				obj["ReferencePath"] = ReferencePath.getJson(this, options);
		}
	}
	public partial class IfcMobileTelecommunicationsAppliance
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcMobileTelecommunicationsApplianceTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcMobileTelecommunicationsApplianceTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcMobileTelecommunicationsApplianceType
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
				Enum.TryParse<IfcMobileTelecommunicationsApplianceTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcMooringDevice
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcMooringDeviceTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcMooringDeviceTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcMooringDeviceType 
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
				Enum.TryParse<IfcMooringDeviceTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
}
#endif
