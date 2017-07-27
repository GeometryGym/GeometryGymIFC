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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mElements.Count > 0)
			{
				JArray array = new JArray();
				foreach (int i in mElements)
					array.Add(mDatabase[i].getJson(this, processed));
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["Unit"] = Unit.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if(mLengthExponent != 0)
				obj["LengthExponent"] = mLengthExponent;
			if(mMassExponent != 0)
				obj["MassExponent"] = mMassExponent;
			if(mTimeExponent != 0)
				obj["TimeExponent"] = mTimeExponent;
			if(mElectricCurrentExponent != 0)
				obj["ElectricCurrentExponent"] = mElectricCurrentExponent;
			if(mThermodynamicTemperatureExponent != 0)
				obj["ThermodynamicTemperatureExponent"] = mThermodynamicTemperatureExponent;
			if(mAmountOfSubstanceExponent != 0)
				obj["AmountOfSubstanceExponent"] = mAmountOfSubstanceExponent;
			if(mLuminousIntensityExponent != 0)
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["DirectionRatios"] = mDirectionRatioX + (double.IsNaN(mDirectionRatioY) ? "" : (" " + mDirectionRatioY + (double.IsNaN(mDirectionRatioZ) ? "" : " " + mDirectionRatioZ)));
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mFlowDirection != IfcFlowDirectionEnum.NOTDEFINED)
				obj["FlowDirection"] = mFlowDirection.ToString();
			if (mPredefinedType != IfcDistributionPortTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (mSystemType != IfcDistributionSystemEnum.NOTDEFINED)
				obj["SystemType"] = mSystemType.ToString();
		}
	}
	public partial class IfcDocumentReference : IfcExternalReference, IfcDocumentSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Description = token.Value<string>();
			JObject jobj = obj["ReferencedDocument"] as JObject;
			if (jobj != null)
				ReferencedDocument = mDatabase.parseJObject<IfcDocumentInformation>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			setAttribute(obj, "Description", Description);
			if (mReferencedDocument > 0)
				obj["ReferencedDocument"] = ReferencedDocument.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (!double.IsNaN(mPanelDepth))
				obj["PanelDepth"] = mPanelDepth;
			obj["OperationType"] = mOperationType.ToString();
			if (!double.IsNaN(mPanelWidth))
				obj["PanelWidth"] = mPanelWidth;
			obj["PanelPosition"] = mPanelPosition.ToString();
			if (mShapeAspectStyle > 0)
				obj["ShapeAspectStyle"] = ShapeAspectStyle.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["PredefinedType"] = mPredefinedType.ToString();
			obj["OperationType"] = mOperationType.ToString();
			obj["ParameterTakesPrecedence"] = mParameterTakesPrecedence;
			setAttribute(obj, "UserDefinedOperationType", UserDefinedOperationType);
		}
	}
}
