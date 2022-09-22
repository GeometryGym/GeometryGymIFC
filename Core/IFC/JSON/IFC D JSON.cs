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
	public partial class IfcDerivedUnit : BaseClassIfc, IfcUnit
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Elements.AddRange(mDatabase.extractJsonArray<IfcDerivedUnitElement>(obj["Elements"] as JsonArray));
			var node = obj["UnitType"];
			if (node != null)
				Enum.TryParse<IfcDerivedUnitEnum>(node.GetValue<string>(), true, out mUnitType);
			node = obj["UserDefinedType"];
			if (node != null)
				UserDefinedType = node.GetValue<string>();
			node = obj["Name"];
			if (node != null)
				Name = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mElements.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcDerivedUnitElement derived in mElements)
					array.Add(derived.getJson(this, options));
				obj["Elements"] = array;
			}
			base.setAttribute(obj, "UnitType", mUnitType.ToString());
			base.setAttribute(obj, "UserDefinedType", UserDefinedType);
			base.setAttribute(obj, "Name", Name);

		}
	}
	public partial class IfcDerivedUnitElement : BaseClassIfc
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);

			JsonObject jobj = obj["Unit"] as JsonObject;
			if (jobj != null)
				Unit = extractObject<IfcNamedUnit>(jobj);
			var node = obj["Exponent"];
			if (node != null)
				Exponent = node.GetValue<int>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Unit"] = Unit.getJson(this, options);
			obj["Exponent"] = mExponent;
		}
	}
	public partial class IfcDimensionalExponents : BaseClassIfc
	{
		//		internal int mLengthExponent, mMassExponent, mTimeExponent, mElectricCurrentExponent, mThermodynamicTemperatureExponent, mAmountOfSubstanceExponent, mLuminousIntensityExponent;// : INTEGER;
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["LengthExponent"];
			if (node != null)
				int.TryParse(node.GetValue<string>(), out mLengthExponent);
			node = obj["MassExponent"];
			if (node != null)
				int.TryParse(node.GetValue<string>(), out mMassExponent);
			node = obj["TimeExponent"];
			if (node != null)
				int.TryParse(node.GetValue<string>(), out mTimeExponent);
			node = obj["ElectricCurrentExponent"];
			if (node != null)
				int.TryParse(node.GetValue<string>(), out mElectricCurrentExponent);
			node = obj["ThermodynamicTemperatureExponent"];
			if (node != null)
				int.TryParse(node.GetValue<string>(), out mThermodynamicTemperatureExponent);
			node = obj["AmountOfSubstanceExponent"];
			if (node != null)
				int.TryParse(node.GetValue<string>(), out mAmountOfSubstanceExponent);
			node = obj["LuminousIntensityExponent"];
			if (node != null)
				int.TryParse(node.GetValue<string>(), out mLuminousIntensityExponent);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mLengthExponent != 0)
				obj["LengthExponent"] = mLengthExponent;
			if (mMassExponent != 0)
				obj["MassExponent"] = mMassExponent;
			if (mTimeExponent != 0)
				obj["TimeExponent"] = mTimeExponent;
			if (mElectricCurrentExponent != 0)
				obj["ElectricCurrentExponent"] = mElectricCurrentExponent;
			if (mThermodynamicTemperatureExponent != 0)
				obj["ThermodynamicTemperatureExponent"] = mThermodynamicTemperatureExponent;
			if (mAmountOfSubstanceExponent != 0)
				obj["AmountOfSubstanceExponent"] = mAmountOfSubstanceExponent;
			if (mLuminousIntensityExponent != 0)
				obj["LuminousIntensityExponent"] = mLuminousIntensityExponent;
		}
	}
	public partial class IfcDirection : IfcGeometricRepresentationItem
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["DirectionRatios"];
			if (node != null)
			{
				JsonArray array = node as JsonArray;
				if (array != null)
				{
					mDirectionRatioX = array[0].GetValue<double>();
					if (array.Count > 1)
					{
						mDirectionRatioY = array[1].GetValue<double>();
						if (array.Count > 2)
							mDirectionRatioZ = array[2].GetValue<double>();
					}
				}
				else
				{
					string[] values = node.GetValue<string>().Split(" ".ToCharArray());
					if (values.Length > 0)
					{
						mDirectionRatioX = double.Parse(values[0]);
						if (values.Length > 1)
						{
							mDirectionRatioY = double.Parse(values[1]);
							if (values.Length > 2)
								mDirectionRatioZ = double.Parse(values[2]);
						}
					}
				}
			}
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["DirectionRatios"] = mDirectionRatioX + (double.IsNaN(mDirectionRatioY) ? "" : (" " + RoundRatio(mDirectionRatioY) + (double.IsNaN(mDirectionRatioZ) ? "" : " " + RoundRatio(mDirectionRatioZ))));
		}
	}
	public abstract partial class IfcDirectrixCurveSweptAreaSolid : IfcSweptAreaSolid
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Directrix"] = Directrix.getJson(this, options);
			if (mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4X3_RC2)
			{
				IfcParameterValue startParameter = mStartParam as IfcParameterValue;
				if(startParameter != null)
					obj["StartParam"] = startParameter.Measure.ToString();
				IfcParameterValue endParameter = mEndParam as IfcParameterValue;
				if(endParameter != null)
					obj["EndParam"] = endParameter.Measure.ToString();
			}
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Directrix"] as JsonObject;
			if (jobj != null)
				Directrix = mDatabase.ParseJsonObject<IfcCurve>(jobj);
			var node = obj["StartParam"];
			if (node != null)
				mStartParam = new IfcParameterValue(node.GetValue<double>());
			node = obj["EndParam"];
			if (node != null)
				mEndParam = new	IfcParameterValue(node.GetValue<double>());
		}
	}
	public partial class IfcDistributionBoard : IfcFlowController
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcDistributionBoardTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcDistributionBoardTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcDistributionBoardType : IfcFlowControllerType
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
				Enum.TryParse<IfcDistributionBoardTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcDistributionPort : IfcPort
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["FlowDirection"];
			if (node != null)
				Enum.TryParse<IfcFlowDirectionEnum>(node.GetValue<string>(), true, out mFlowDirection);
			node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcDistributionPortTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
			node = obj["SystemType"];
			if (node != null)
				Enum.TryParse<IfcDistributionSystemEnum>(node.GetValue<string>(), true, out mSystemType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mFlowDirection != IfcFlowDirectionEnum.NOTDEFINED)
				obj["FlowDirection"] = mFlowDirection.ToString();
			if (mPredefinedType != IfcDistributionPortTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (mSystemType != IfcDistributionSystemEnum.NOTDEFINED)
				obj["SystemType"] = mSystemType.ToString();
		}
	}
	public partial class IfcDocumentInformation : IfcExternalInformation, IfcDocumentSelect
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Identification = extractString(obj["Idenfification"]);
			Name = extractString(obj["Name"]);
			Description = extractString(obj["Description"]);
			DocumentReferences.AddRange(mDatabase.extractJsonArray<IfcDocumentReference>(obj["DocumentReferences"] as JsonArray));
			Location = extractString(obj["Location"]);
			Purpose = extractString(obj["Purpose"]);
			Revision = extractString(obj["Revision"]);
			JsonObject jobj = obj["DocumentOwner"] as JsonObject;
			if (jobj != null)
				DocumentOwner = mDatabase.ParseJsonObject<IfcActorSelect>(jobj);
			Editors.AddRange(mDatabase.extractJsonArray<IfcActorSelect>(obj["Editors"] as JsonArray));
			CreationTime = extractDateTime(obj, "CreationTime");
			LastRevisionTime = extractDateTime(obj, "LastRevisionTime");
			ElectronicFormat = extractString(obj["ElectronicFormat"]);
			ValidFrom = extractDateTime(obj, "ValidFrom");
			ValidUntil = extractDateTime(obj, "ValidUntil");
			var node = obj["Confidentiality"];
			if (node != null)
				Enum.TryParse<IfcDocumentConfidentialityEnum>(node.GetValue<string>(), true, out mConfidentiality);
			node = obj["Status"];
			if (node != null)
				Enum.TryParse<IfcDocumentStatusEnum>(node.GetValue<string>(), true, out mStatus);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Identification", Identification);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Location", Location);
			setAttribute(obj, "Purpose", Purpose);
			setAttribute(obj, "Revision", Revision);
			if(mDocumentOwner != null)
				obj["DocumentOwner"] = mDocumentOwner.getJson(this, options);
			createArray(obj, "Editors", Editors, this, options);
			if (mCreationTime != DateTime.MinValue)
				obj["CreationTime"] = IfcDateTime.FormatSTEP(CreationTime);
			if (mLastRevisionTime != DateTime.MinValue)
				obj["LastRevisionTime"] = IfcDateTime.FormatSTEP(LastRevisionTime);
			setAttribute(obj, "ElectronicFormat", ElectronicFormat);
			if (mValidFrom != DateTime.MinValue)
				obj["ValidFrom"] = IfcDate.FormatSTEP(ValidFrom);
			if (mValidUntil != DateTime.MinValue)
				obj["ValidUntil"] = IfcDate.FormatSTEP(ValidUntil);
			if (mConfidentiality != IfcDocumentConfidentialityEnum.NOTDEFINED)
				obj["Confidentiality"] = mConfidentiality.ToString();
			if (mStatus != IfcDocumentStatusEnum.NOTDEFINED)
				obj["Status"] = mStatus.ToString();
		}
	}
	public partial class IfcDocumentReference : IfcExternalReference, IfcDocumentSelect
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Description = extractString(obj["Description"]);
			JsonObject jobj = obj["ReferencedDocument"] as JsonObject;
			if (jobj != null)
				ReferencedDocument = mDatabase.ParseJsonObject<IfcDocumentInformation>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Description", Description);
			if (mReferencedDocument != null)
				obj["ReferencedDocument"] = ReferencedDocument.getJson(this, options);
		}
	}
	public partial class IfcDoorPanelProperties : IfcPreDefinedPropertySet //IFC2x3 IfcPropertySetDefinition
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PanelDepth"];
			if (node != null)
				mPanelDepth = node.GetValue<double>();
			node = obj["OperationType"];
			if (node != null)
				Enum.TryParse<IfcDoorPanelOperationEnum>(node.GetValue<string>(), true, out mOperationType);
			node = obj["PanelWidth"];
			if (node != null)
				mPanelWidth = node.GetValue<double>();
			node = obj["PanelPosition"];
			if (node != null)
				Enum.TryParse<IfcDoorPanelPositionEnum>(node.GetValue<string>(), true, out mPanelPosition);
			JsonObject jobj = obj["ShapeAspectStyle"] as JsonObject;
			if (jobj != null)
				ShapeAspectStyle = mDatabase.ParseJsonObject<IfcShapeAspect>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mPanelDepth))
				obj["PanelDepth"] = mPanelDepth;
			obj["OperationType"] = mOperationType.ToString();
			if (!double.IsNaN(mPanelWidth))
				obj["PanelWidth"] = mPanelWidth;
			obj["PanelPosition"] = mPanelPosition.ToString();
			if (mShapeAspectStyle != null)
				obj["ShapeAspectStyle"] = ShapeAspectStyle.getJson(this, options);
		}
	}
	public partial class IfcDoorType : IfcBuiltElementType //IFC2x3 IfcDoorStyle
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcDoorTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
			node = obj["OperationType"];
			if (node != null)
				Enum.TryParse<IfcDoorTypeOperationEnum>(node.GetValue<string>(), true, out mOperationType);
			node = obj["ParameterTakesPrecedence"];
			if (node != null)
				mParameterTakesPrecedence = node.GetValue<bool>();
			node = obj["UserDefinedOperationType"];
			if (node != null)
				UserDefinedOperationType = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["PredefinedType"] = mPredefinedType.ToString();
			obj["OperationType"] = mOperationType.ToString();
			obj["ParameterTakesPrecedence"] = mParameterTakesPrecedence;
			setAttribute(obj, "UserDefinedOperationType", UserDefinedOperationType);
		}
	}
}
#endif
