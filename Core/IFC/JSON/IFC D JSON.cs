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
	public partial class IfcDerivedUnit : BaseClassIfc, IfcUnit
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Elements = mDatabase.extractJArray<IfcDerivedUnitElement>(obj.GetValue("Elements", StringComparison.InvariantCultureIgnoreCase) as JArray);
			JToken token = obj.GetValue("UnitType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcDerivedUnitEnum>(token.Value<string>(), true, out mUnitType);
			token = obj.GetValue("UserDefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				UserDefinedType = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mElements.Count > 0)
			{
				JArray array = new JArray();
				foreach (int i in mElements)
					array.Add(mDatabase[i].getJson(this, options));
				obj["Elements"] = array;
			}
			base.setAttribute(obj, "UnitType", mUnitType.ToString());
			base.setAttribute(obj, "UserDefinedType", UserDefinedType);

		}
	}
	public partial class IfcDerivedUnitElement : BaseClassIfc
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);

			JObject jobj = obj.GetValue("Unit", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Unit = extractObject<IfcNamedUnit>(jobj);
			JToken token = obj.GetValue("Exponent", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Exponent = token.Value<int>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Unit"] = Unit.getJson(this, options);
			obj["Exponent"] = mExponent;
		}
	}
	public partial class IfcDimensionalExponents : BaseClassIfc
	{
		//		internal int mLengthExponent, mMassExponent, mTimeExponent, mElectricCurrentExponent, mThermodynamicTemperatureExponent, mAmountOfSubstanceExponent, mLuminousIntensityExponent;// : INTEGER;
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("LengthExponent", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				int.TryParse(token.Value<string>(), out mLengthExponent);
			token = obj.GetValue("MassExponent", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				int.TryParse(token.Value<string>(), out mMassExponent);
			token = obj.GetValue("TimeExponent", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				int.TryParse(token.Value<string>(), out mTimeExponent);
			token = obj.GetValue("ElectricCurrentExponent", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				int.TryParse(token.Value<string>(), out mElectricCurrentExponent);
			token = obj.GetValue("ThermodynamicTemperatureExponent", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				int.TryParse(token.Value<string>(), out mThermodynamicTemperatureExponent);
			token = obj.GetValue("AmountOfSubstanceExponent", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				int.TryParse(token.Value<string>(), out mAmountOfSubstanceExponent);
			token = obj.GetValue("LuminousIntensityExponent", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				int.TryParse(token.Value<string>(), out mLuminousIntensityExponent);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("DirectionRatios", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
			{
				JArray array = token as JArray;
				if (array != null)
				{
					mDirectionRatioX = array[0].Value<double>();
					if (array.Count > 1)
					{
						mDirectionRatioY = array[1].Value<double>();
						if (array.Count > 2)
							mDirectionRatioZ = array[2].Value<double>();
					}
				}
				else
				{
					string[] values = token.Value<string>().Split(" ".ToCharArray());
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
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["DirectionRatios"] = mDirectionRatioX + (double.IsNaN(mDirectionRatioY) ? "" : (" " + RoundRatio(mDirectionRatioY) + (double.IsNaN(mDirectionRatioZ) ? "" : " " + RoundRatio(mDirectionRatioZ))));
		}
	}
	public partial class IfcDistributionPort : IfcPort
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("FlowDirection", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcFlowDirectionEnum>(token.Value<string>(), true, out mFlowDirection);
			token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcDistributionPortTypeEnum>(token.Value<string>(), true, out mPredefinedType);
			token = obj.GetValue("SystemType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcDistributionSystemEnum>(token.Value<string>(), true, out mSystemType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Identification = extractString(obj.GetValue("Idenfification", StringComparison.InvariantCultureIgnoreCase));
			Name = extractString(obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase));
			Description = extractString(obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase));
			mDatabase.extractJArray<IfcDocumentReference>(obj.GetValue("DocumentReferences", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addReference(x));
			Location = extractString(obj.GetValue("Location", StringComparison.InvariantCultureIgnoreCase));
			Purpose = extractString(obj.GetValue("Purpose", StringComparison.InvariantCultureIgnoreCase));
			Revision = extractString(obj.GetValue("Revision", StringComparison.InvariantCultureIgnoreCase));
			JObject jobj = obj["DocumentOwner"] as JObject;
			if (jobj != null)
				DocumentOwner = mDatabase.parseJObject<IfcActorSelect>(jobj);
			mDatabase.extractJArray<IfcActorSelect>(obj.GetValue("Editors", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addEditor(x));
			JToken token = obj.GetValue("CreationTime");
			if(token != null)
				CreationTime = DateTime.Parse(token.Value<string>());
			token = obj.GetValue("LastRevisionTime");
			if(token != null)
				LastRevisionTime = DateTime.Parse(token.Value<string>());
			ElectronicFormat = extractString(obj.GetValue("ElectronicFormat", StringComparison.InvariantCultureIgnoreCase));
			token = obj.GetValue("ValidFrom");
			if(token != null)
				ValidFrom = DateTime.Parse(token.Value<string>());
			token = obj.GetValue("ValidUntil");
			if(token != null)
				ValidUntil = DateTime.Parse(token.Value<string>());
			token = obj.GetValue("Confidentiality");
			if (token != null)
				Enum.TryParse<IfcDocumentConfidentialityEnum>(token.Value<string>(), true, out mConfidentiality);
			token = obj.GetValue("Status");
			if (token != null)
				Enum.TryParse<IfcDocumentStatusEnum>(token.Value<string>(), true, out mStatus);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Identification", Identification);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Location", Location);
			setAttribute(obj, "Purpose", Purpose);
			setAttribute(obj, "Revision", Revision);
			if(mDocumentOwner > 0)
				obj["DocumentOwner"] = mDatabase[mDocumentOwner].getJson(this, options);
			if(mEditors.Count > 0)
				obj["Editors"] = new JArray(mEditors.ToList().ConvertAll(x => mDatabase[x].getJson(this, options)));
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
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Description = extractString(obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase));
			JObject jobj = obj["ReferencedDocument"] as JObject;
			if (jobj != null)
				ReferencedDocument = mDatabase.parseJObject<IfcDocumentInformation>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Description", Description);
			if (mReferencedDocument > 0)
				obj["ReferencedDocument"] = ReferencedDocument.getJson(this, options);
		}
	}
	public partial class IfcDoorPanelProperties : IfcPreDefinedPropertySet //IFC2x3 IfcPropertySetDefinition
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PanelDepth", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mPanelDepth = token.Value<double>();
			token = obj.GetValue("OperationType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcDoorPanelOperationEnum>(token.Value<string>(), true, out mOperationType);
			token = obj.GetValue("PanelWidth", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mPanelWidth = token.Value<double>();
			token = obj.GetValue("PanelPosition", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcDoorPanelPositionEnum>(token.Value<string>(), true, out mPanelPosition);
			JObject jobj = obj.GetValue("ShapeAspectStyle", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				ShapeAspectStyle = mDatabase.parseJObject<IfcShapeAspect>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mPanelDepth))
				obj["PanelDepth"] = mPanelDepth;
			obj["OperationType"] = mOperationType.ToString();
			if (!double.IsNaN(mPanelWidth))
				obj["PanelWidth"] = mPanelWidth;
			obj["PanelPosition"] = mPanelPosition.ToString();
			if (mShapeAspectStyle > 0)
				obj["ShapeAspectStyle"] = ShapeAspectStyle.getJson(this, options);
		}
	}
	public partial class IfcDoorType : IfcBuildingElementType //IFC2x3 IfcDoorStyle
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcDoorTypeEnum>(token.Value<string>(), true, out mPredefinedType);
			token = obj.GetValue("OperationType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcDoorTypeOperationEnum>(token.Value<string>(), true, out mOperationType);
			token = obj.GetValue("ParameterTakesPrecedence", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mParameterTakesPrecedence = token.Value<bool>();
			token = obj.GetValue("UserDefinedOperationType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				UserDefinedOperationType = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["PredefinedType"] = mPredefinedType.ToString();
			obj["OperationType"] = mOperationType.ToString();
			obj["ParameterTakesPrecedence"] = mParameterTakesPrecedence;
			setAttribute(obj, "UserDefinedOperationType", UserDefinedOperationType);
		}
	}
}
