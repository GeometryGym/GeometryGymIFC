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
	public partial class IfcAirTerminal : IfcFlowTerminal
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType",StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcAirTerminalTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcAirTerminalTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcAirTerminalType : IfcFlowTerminalType
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase); 
			if (token != null)
				Enum.TryParse<IfcAirTerminalTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcAnnotationFillArea : IfcGeometricRepresentationItem
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("OuterBoundary", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				OuterBoundary = mDatabase.parseJObject<IfcCurve>(jobj);
			mDatabase.extractJArray<IfcCurve>(obj.GetValue("InnerBoundaries", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>AddInner(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["OuterBoundary"] = OuterBoundary.getJson(this, options);
			if(mInnerBoundaries.Count > 0)
				obj["InnerBoundaries"] = new JArray(InnerBoundaries.ToList().ConvertAll(x => x.getJson(this, options)));
		}
		
	}
	public partial class IfcApplication : BaseClassIfc
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("ApplicationDeveloper", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ApplicationDeveloper = mDatabase.parseJObject<IfcOrganization>(token as JObject);
			token = obj.GetValue("Version", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Version = token.Value<string>();
			token = obj.GetValue("ApplicationFullName", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ApplicationFullName = token.Value<string>();
			token = obj.GetValue("ApplicationIdentifier", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ApplicationIdentifier = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			
			obj["ApplicationDeveloper"] = ApplicationDeveloper.getJson(this, options);
			obj["Version"] = Version;
			obj["ApplicationFullName"] = ApplicationFullName;
			obj["ApplicationIdentifier"] = ApplicationIdentifier;
		}
	}
	public partial class IfcAppliedValue : BaseClassIfc, IfcMetricValueSelect, IfcAppliedValueSelect, IfcResourceObjectSelect //SUPERTYPE OF(IfcCostValue);
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
			JObject jobj = obj.GetValue("AppliedValue", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
			{
				AppliedValue = mDatabase.parseJObject<IfcAppliedValueSelect>(jobj);
				if (mAppliedValueIndex <= 0)
					mAppliedValueValue = DatabaseIfc.ParseValue(jobj);
			}
			jobj = obj.GetValue("UnitBasis", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				UnitBasis = mDatabase.parseJObject<IfcMeasureWithUnit>(jobj);

			mDatabase.extractJArray<IfcAppliedValue>(obj.GetValue("Components", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>addComponent(x));


			List <IfcExternalReferenceRelationship> ers = mDatabase.extractJArray<IfcExternalReferenceRelationship>(obj.GetValue("HasExternalReferences", StringComparison.InvariantCultureIgnoreCase) as JArray);
			for (int icounter = 0; icounter < ers.Count; icounter++)
				ers[icounter].addRelated(this);
			List<IfcResourceConstraintRelationship> crs = mDatabase.extractJArray<IfcResourceConstraintRelationship>(obj.GetValue("HasConstraintRelationships", StringComparison.InvariantCultureIgnoreCase) as JArray);
			for (int icounter = 0; icounter < crs.Count; icounter++)
				crs[icounter].addRelated(this);
			//todo
			token = obj.GetValue("Category", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Category = token.Value<string>();
			token = obj.GetValue("Condition", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Condition = token.Value<string>();
			token = obj.GetValue("ArithmeticOperator", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcArithmeticOperatorEnum>(token.Value<string>(),out mArithmeticOperator);

		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			if (mAppliedValueIndex > 0)
				obj["AppliedValue"] = mDatabase[mAppliedValueIndex].getJson(this, options);
			else if (mAppliedValueValue != null)
				obj["AppliedValue"] = DatabaseIfc.extract(mAppliedValueValue);
			if (mUnitBasis > 0)
				obj["UnitBasis"] = UnitBasis.getJson(this, options);
			//todo
			setAttribute(obj, "Category", Category);
			setAttribute(obj, "Condition", Condition);
			if (mArithmeticOperator != IfcArithmeticOperatorEnum.NONE)
				obj["ArithmeticOperator"] = ArithmeticOperator.ToString();
			if(mComponents.Count > 0)
				obj["Components"] = new JArray(Components.ToList().ConvertAll(x => x.getJson(this, options)));
			if (mHasExternalReferences.Count > 0)
				obj["HasExternalReferences"] = new JArray(HasExternalReferences.ToList().ConvertAll(x => x.getJson(this, options)));
			if (mHasConstraintRelationships.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcResourceConstraintRelationship r in HasConstraintRelationships)
				{
					if (r.mIndex != host.mIndex)
						array.Add(r.getJson(this, options));
				}
				if(array.Count > 0)
					obj["HasConstraintRelationships"] = array;
			}
		}
	}
	public partial class IfcArbitraryClosedProfileDef : IfcProfileDef
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("OuterCurve", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if(jobj != null)
				OuterCurve = mDatabase.parseJObject<IfcBoundedCurve>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["OuterCurve"] = OuterCurve.getJson(this, options);
		}
	}
	public partial class IfcAxis1Placement : IfcPlacement
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Axis = mDatabase.parseJObject<IfcDirection>(obj.GetValue("Axis", StringComparison.InvariantCultureIgnoreCase) as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			IfcDirection axis = Axis;
			if (axis != null)
				obj["Axis"] = axis.getJson(this, options);
		}
	}
	public partial class IfcAxis2Placement2D : IfcPlacement, IfcAxis2Placement
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("RefDirection", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if(jobj != null)
			{
				JToken token = jobj["href"];
				if (token != null)
					mRefDirection = token.Value<int>();
				else
					RefDirection = mDatabase.parseJObject<IfcDirection>(obj.GetValue("RefDirection", StringComparison.InvariantCultureIgnoreCase) as JObject);
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			IfcDirection refDirection = RefDirection;
			if (refDirection != null)
				obj["RefDirection"] = refDirection.getJson(this, options);
		}
	}
	public partial class IfcAxis2Placement3D : IfcPlacement, IfcAxis2Placement
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			IfcCartesianPoint cp = Location;
			if(cp != null)
				cp.Coordinates = cp.Coordinates; // Force 3d
			IfcDirection dir = mDatabase.parseJObject<IfcDirection>(obj.GetValue("Axis", StringComparison.InvariantCultureIgnoreCase) as JObject);
			if (dir != null)
			{
				mAxis = dir.mIndex;
				dir.DirectionRatioY = dir.DirectionRatioY;
				dir.DirectionRatioZ = dir.DirectionRatioZ;
			}
			dir = mDatabase.parseJObject<IfcDirection>(obj.GetValue("RefDirection", StringComparison.InvariantCultureIgnoreCase) as JObject);
			if (dir != null)
			{
				mRefDirection = dir.mIndex;
				dir.DirectionRatioY = dir.DirectionRatioY;
				dir.DirectionRatioZ = dir.DirectionRatioZ;
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			IfcDirection axis = Axis;
			if (axis != null)
				obj["Axis"] = axis.getJson(this, options);
			IfcDirection refDirection = RefDirection;
			if (refDirection != null)
				obj["RefDirection"] = refDirection.getJson(this, options);
		}
	}
}
