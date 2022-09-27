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
	public partial class IfcSecondOrderPolynomialSpiral
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["QuadraticTerm"] = mQuadraticTerm;
			if (double.IsNaN(mLinearTerm))
				obj["LinearTerm"] = mLinearTerm;
			if (double.IsNaN(mConstantTerm))
				obj["ConstantTerm"] = mConstantTerm;
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			QuadraticTerm = extractDouble(obj["QuadraticTerm"]);
			LinearTerm = extractDouble(obj["LinearTerm"]);
			ConstantTerm = extractDouble(obj["ConstantTerm"]);
		}
	}
	public partial class IfcSectionedSurface
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Directrix"] = Directrix.getJson(this, options);
			createArray(obj, "CrossSectionPositions", CrossSectionPositions, this, options);
			createArray(obj, "CrossSections", CrossSections, this, options);
			if(mDatabase != null && mDatabase.Release < ReleaseVersion.IFC4X3)
				obj["FixedAxisVertical"] = mFixedAxisVertical;
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Directrix"] as JsonObject;
			if (jobj != null)
				Directrix = mDatabase.ParseJsonObject<IfcCurve>(jobj);
			CrossSectionPositions.AddRange(mDatabase.extractJsonArray<IfcAxis2PlacementLinear>(obj["CrossSectionPositions"] as JsonArray));
			CrossSections.AddRange(mDatabase.extractJsonArray<IfcProfileDef>(obj["CrossSections"] as JsonArray));
			var node = obj["FixedAxisVertical"];
			if (node != null)
				mFixedAxisVertical = node.GetValue<bool>();
		}
	}
	public abstract partial class IfcSegment
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Transition"] = mTransition.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Transition"];
			if (node != null)
				Enum.TryParse<IfcTransitionCode>(node.GetValue<string>(), true, out mTransition);
		}
	}
	public partial class IfcSegmentedReferenceCurve
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["BaseCurve"] = BaseCurve.getJson(this, options);
			if (EndPoint != null)
				obj["EndPoint"] = EndPoint.getJson(this, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["BaseCurve"] as JsonObject;
			if (jobj != null)
				BaseCurve = mDatabase.ParseJsonObject<IfcBoundedCurve>(jobj);
			jobj = obj["EndPoint"] as JsonObject;
			if (jobj != null)
				EndPoint = mDatabase.ParseJsonObject<IfcPlacement>(jobj);
		}
	}
	public partial class IfcShapeAspect
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mShapeRepresentations.AddRange(mDatabase.extractJsonArray<IfcShapeModel>(obj["ShapeRepresentations"] as JsonArray));
			var node = obj["Name"];
			if (node != null)
				Name = node.GetValue<string>();
			node = obj["Description"];
			if (node != null)
				Description = node.GetValue<string>();
			node = obj["ProductDefinitional"];
			if (node != null)
				Enum.TryParse<IfcLogicalEnum>(node.GetValue<string>(), true, out mProductDefinitional);
			PartOfProductDefinitionShape = mDatabase.ParseJsonObject<IfcProductRepresentationSelect>(obj["PartOfProductDefinitionShape"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			foreach (IfcShapeModel sm in ShapeRepresentations)
				array.Add(sm.getJson(this, options));
			obj["ShapeRepresentations"] = array;
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "ProductDefinitional", mProductDefinitional.ToString());
			if (mPartOfProductDefinitionShape != null && mPartOfProductDefinitionShape != host)
				obj["PartOfProductDefinitionShape"] = mPartOfProductDefinitionShape.getJson(this, options);
		}
	}
	public abstract partial class IfcShapeModel 
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mOfShapeAspect != null)
				obj["OfShapeAspect"] = mOfShapeAspect.getJson(this, options);
		}
	}
	public partial class IfcSign
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSignTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcSignTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcSignal
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSignalTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcSignalTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcSignalType 
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
				Enum.TryParse<IfcSignalTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcSignType
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
				Enum.TryParse<IfcSignTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcSimplePropertyTemplate
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["TemplateType"];
			if (node != null)
				Enum.TryParse<IfcSimplePropertyTemplateTypeEnum>(node.GetValue<string>(), true, out mTemplateType);
			PrimaryMeasureType = extractString(obj["PrimaryMeasureType"]);
			SecondaryMeasureType = extractString(obj["SecondaryMeasureType"]);
			Enumerators = extractObject<IfcPropertyEnumeration>(obj["Enumerators"] as JsonObject);
			PrimaryUnit = extractObject<IfcUnit>(obj["PrimaryUnit"] as JsonObject);
			SecondaryUnit = extractObject<IfcUnit>(obj["SecondaryUnit"] as JsonObject);
			Expression = extractString(obj["Expression"]);
			node = obj["AccessState"];
			if (node != null)
				Enum.TryParse<IfcStateEnum>(node.GetValue<string>(), true, out mAccessState);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mTemplateType != IfcSimplePropertyTemplateTypeEnum.NOTDEFINED)
				obj["TemplateType"] = mTemplateType.ToString();
			setAttribute(obj, "PrimaryMeasureType", PrimaryMeasureType);
			setAttribute(obj, "SecondaryMeasureType", SecondaryMeasureType);
			if (mEnumerators != null)
				obj["Enumerators"] = Enumerators.getJson(this, options);
			if (mPrimaryUnit is BaseClassIfc o)
				obj["PrimaryUnit"] = o.getJson(this, options);
			if (mSecondaryUnit != null)
				obj["SecondaryUnit"] = mSecondaryUnit.getJson(this, options);
			setAttribute(obj, "Expression", Expression);
			if (mAccessState != IfcStateEnum.NOTDEFINED)
				obj["AccessState"] = mAccessState.ToString();
		}

	}

	public partial class IfcSIUnit
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Prefix"];
			if (node != null)
				Enum.TryParse<IfcSIPrefix>(node.GetValue<string>(), out mPrefix);
			node = obj["Name"];
			if (node != null)
				Enum.TryParse<IfcSIUnitName>(node.GetValue<string>(), out mName);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPrefix != IfcSIPrefix.NONE)
				obj["Prefix"] = mPrefix.ToString();
			obj["Name"] = Name.ToString();
		}
	}
	public partial class IfcSineSpiral
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["SineTerm"] = mSineTerm;
			if(!double.IsNaN(mLinearTerm))
				obj["LinearTerm"] = mLinearTerm.ToString();
			if(!double.IsNaN(mConstantTerm))
				obj["ConstantTerm"] = mConstantTerm.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mSineTerm = extractDouble(obj["SineTerm"]);
			mLinearTerm = extractDouble(obj["LinearTerm"]);
			mConstantTerm = extractDouble(obj["ConstantTerm"]);
		}
	}
	public partial class IfcSlab
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcSlabTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSlabTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcSlabType
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcSlabTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSlabTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcSpace
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcSpaceTypeEnum>(node.GetValue<string>(), out mPredefinedType);
			mElevationWithFlooring = extractDouble(obj["ElevationWithFlooring"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mDatabase.mRelease < ReleaseVersion.IFC4)
				obj["InteriorOrExteriorSpace"] = mPredefinedType == IfcSpaceTypeEnum.INTERNAL || mPredefinedType == IfcSpaceTypeEnum.EXTERNAL ? mPredefinedType.ToString() : "NOTDEFINED";
			else if (mPredefinedType != IfcSpaceTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (!double.IsNaN(mElevationWithFlooring))
				obj["ElevationWithFlooring"] = mElevationWithFlooring;
		}
	}
	public partial class IfcSpaceHeater
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcSpaceHeaterTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSpaceHeaterTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcSpaceHeaterType
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcSpaceHeaterTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSpaceHeaterTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public abstract partial class IfcSpatialElement
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			LongName = extractString(obj["LongName"]);
			foreach (IfcRelContainedInSpatialStructure rcss in mDatabase.extractJsonArray<IfcRelContainedInSpatialStructure>(obj["ContainsElements"] as JsonArray))
				rcss.RelatingStructure = this;
			foreach (IfcRelServicesBuildings rsbs in mDatabase.extractJsonArray<IfcRelServicesBuildings>(obj["ServicedBySystems"] as JsonArray))
				rsbs.addRelated(this);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			string longName = LongName;
			if (!string.IsNullOrEmpty(longName))
				obj["LongName"] = longName;

			if (options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				if (mContainsElements.Count > 0)
				{
					JsonArray array = new JsonArray();
					foreach (IfcRelContainedInSpatialStructure con in ContainsElements)
						array.Add(con.getJson(this, options));
					obj["ContainsElements"] = array;
				}
			}
			//Reference buildings from system ?? not structural analysis
			if (mServicedBySystems.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcRelServicesBuildings systems in ServicedBySystems)
					array.Add(systems.getJson(this, options));
				obj["ServicedBySystems"] = array;
			}
		}
	}
	public abstract partial class IfcSpatialStructureElement
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["CompositionType"];
			if (node != null)
				Enum.TryParse<IfcElementCompositionEnum>(node.GetValue<string>(), out mCompositionType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mCompositionType != IfcElementCompositionEnum.NOTDEFINED)
				obj["CompositionType"] = mCompositionType.ToString();
		}
	}
	public partial class IfcSpiral
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Position"] = Position.getJson(this, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Position"] as JsonObject;
			if (jobj != null)
				Position = mDatabase.ParseJsonObject<IfcAxis2Placement>(jobj);
		}
	}
	public abstract partial class IfcStructuralAction 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["DestabilizingLoad"];
			if (node != null)
				Enum.TryParse<IfcLogicalEnum>(node.GetValue<string>(), true, out mDestabilizingLoad);
			JsonObject rp = obj["CausedBy"] as JsonObject;
			if (rp != null)
				CausedBy = mDatabase.ParseJsonObject<IfcStructuralReaction>(rp);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mDestabilizingLoad == IfcLogicalEnum.UNKNOWN)
			{
				if (mDatabase.Release < ReleaseVersion.IFC4)
					obj["DestabilizingLoad"] = false;
			}
			else
				obj["DestabilizingLoad"] = mDestabilizingLoad == IfcLogicalEnum.TRUE;
			if (mCausedBy != null)
				obj["CausedBy"] = CausedBy.getJson(this, options);
		}
	}
	public abstract partial class IfcStructuralActivity
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject rp = obj["AppliedLoad"] as JsonObject;
			if (rp != null)
				AppliedLoad = mDatabase.ParseJsonObject<IfcStructuralLoad>(rp);
			var node = obj["GlobalOrLocal"];
			if (node != null)
				Enum.TryParse<IfcGlobalOrLocalEnum>(node.GetValue<string>(), true, out mGlobalOrLocal);
			rp = obj["AssignedToStructuralItem"] as JsonObject;
			if (rp != null)
				AssignedToStructuralItem = mDatabase.ParseJsonObject<IfcRelConnectsStructuralActivity>(rp);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["AppliedLoad"] = AppliedLoad.getJson(this, options);
			obj["IfcGlobalOrLocalEnum"] = mGlobalOrLocal.ToString();
			if (mAssignedToStructuralItem != null)
				obj["AssignedToStructuralItem"] = mAssignedToStructuralItem.getJson(this, options);
		}
	}
	public partial class IfcStructuralAnalysisModel
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcAnalysisModelTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
			JsonObject rp = obj["OrientationOf2DPlane"] as JsonObject;
			if (rp != null)
				OrientationOf2DPlane = mDatabase.ParseJsonObject<IfcAxis2Placement3D>(rp);
			mDatabase.extractJsonArray<IfcStructuralLoadGroup>(obj["LoadedBy"] as JsonArray).ForEach(x => addLoadGroup(x));
			mDatabase.extractJsonArray<IfcStructuralResultGroup>(obj["HasResults"] as JsonArray).ForEach(x => addResultGroup(x));
			SharedPlacement = mDatabase.ParseJsonObject<IfcObjectPlacement>(obj["SharedPlacement"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcAnalysisModelTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (mOrientationOf2DPlane != null)
				obj["OrientationOf2DPlane"] = OrientationOf2DPlane.getJson(this, options);
			createArray(obj, "LoadedBy", LoadedBy, this, options);
			createArray(obj, "HasResults", HasResults, this, options);
			if (mSharedPlacement != null)
				obj["SharedPlacement"] = SharedPlacement.getJson(this, options);
		}
	}
	public abstract partial class IfcStructuralConnection 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);

			JsonObject rp = obj["AppliedCondition"] as JsonObject;
			if (rp != null)
				AppliedCondition = mDatabase.ParseJsonObject<IfcBoundaryCondition>(rp);
			mConnectsStructuralMembers.AddRange(mDatabase.extractJsonArray<IfcRelConnectsStructuralMember>(obj["ConnectsStructuralMembers"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			if (mAppliedCondition != null)
				obj["AppliedCondition"] = AppliedCondition.getJson(this, options);
			JsonArray array = new JsonArray();
			foreach(IfcRelConnectsStructuralMember connects in mConnectsStructuralMembers)
			{
				IfcStructuralMember member = connects.RelatingStructuralMember;
				if(host == null || member.StepId != host.StepId)
					array.Add(member.getJson(this, options));
			}
			if (array.Count > 0)
				obj["ConnectsStructuralMembers"] = array;
		}
	}
	public abstract partial class IfcStructuralConnectionCondition
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Name"];
			if (node != null)
				Name = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "Name", Name);
		}
	}
	public partial class IfcStructuralCurveAction
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["ProjectedOrTrue"];
			if (node != null)
				Enum.TryParse<IfcProjectedOrTrueLengthEnum>(node.GetValue<string>(), true, out mProjectedOrTrue);
			node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcStructuralCurveActivityTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ProjectedOrTrue"] = mProjectedOrTrue.ToString();
			if (mPredefinedType != IfcStructuralCurveActivityTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcStructuralCurveMember
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcStructuralCurveMemberTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
			JsonObject rp = obj["Axis"] as JsonObject;
			if (rp != null)
				Axis = mDatabase.ParseJsonObject<IfcDirection>(rp);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcStructuralCurveMemberTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (mAxis != null)
				obj["Axis"] = Axis.getJson(this, options);
		}
	}
	public partial class IfcStructuralCurveReaction
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcStructuralCurveActivityTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcStructuralCurveActivityTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public abstract partial class IfcStructuralItem
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			foreach (IfcRelConnectsStructuralActivity rcsa in mDatabase.extractJsonArray<IfcRelConnectsStructuralActivity>(obj["AssignedStructuralActivity"] as JsonArray))
				rcsa.RelatingElement = this;
		}
	}
	public partial class IfcStructuralLoadLinearForce
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mLinearForceX = extractDouble(obj["LinearForceX"]);
			mLinearForceY = extractDouble(obj["LinearForceY"]);
			mLinearForceZ = extractDouble(obj["LinearForceZ"]);
			mLinearMomentX = extractDouble(obj["LinearMomentX"]);
			mLinearMomentY = extractDouble(obj["LinearMomentY"]);
			mLinearMomentZ = extractDouble(obj["LinearMomentZ"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mLinearForceX))
				obj["LinearForceX"] = mLinearForceX;
			if (!double.IsNaN(mLinearForceY))
				obj["LinearForceY"] = mLinearForceY;
			if (!double.IsNaN(mLinearForceZ))
				obj["LinearForceZ"] = mLinearForceZ;
			if (!double.IsNaN(mLinearMomentX))
				obj["LinearMomentX"] = mLinearMomentX;
			if (!double.IsNaN(mLinearMomentY))
				obj["LinearMomentY"] = mLinearMomentY;
			if (!double.IsNaN(mLinearMomentZ))
				obj["LinearMomentZ"] = mLinearMomentZ;
		}
	}
	public partial class IfcStructuralLoadSingleForce
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mForceX = extractDouble(obj["ForceX"]);
			mForceY = extractDouble(obj["ForceY"]);
			mForceZ = extractDouble(obj["ForceZ"]);
			mMomentX = extractDouble(obj["MomentX"]);
			mMomentY = extractDouble(obj["MomentY"]);
			mMomentZ = extractDouble(obj["MomentZ"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mForceX))
				obj["ForceX"] = mForceX;
			if (!double.IsNaN(mForceY))
				obj["ForceY"] = mForceY;
			if (!double.IsNaN(mForceZ))
				obj["ForceZ"] = mForceZ;
			if (!double.IsNaN(mMomentX))
				obj["MomentX"] = mMomentX;
			if (!double.IsNaN(mMomentY))
				obj["MomentY"] = mMomentY;
			if (!double.IsNaN(mMomentZ))
				obj["MomentZ"] = mMomentZ;
		}
	}
	public abstract partial class IfcStructuralMember 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			foreach (IfcRelConnectsStructuralMember rcsm in mDatabase.extractJsonArray<IfcRelConnectsStructuralMember>(obj["ConnectedBy"] as JsonArray))
				rcsm.RelatingStructuralMember = this;
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JsonArray array = new JsonArray();
			foreach (IfcRelConnectsStructuralMember connects in mConnectedBy)
			{
				IfcStructuralConnection connection = connects.RelatedStructuralConnection;
				if (host == null || connection.StepId != host.StepId)
					array.Add(connects.getJson(this, options));
			}
			if (array.Count > 0)
				obj["ConnectedBy"] = array;
		}
	}
	public partial class IfcStructuralPointConnection
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject rp = obj["ConditionCoordinateSystem"] as JsonObject;
			if (rp != null)
				ConditionCoordinateSystem = mDatabase.ParseJsonObject<IfcAxis2Placement3D>(rp);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mConditionCoordinateSystem != null)
				obj["ConditionCoordinateSystem"] = ConditionCoordinateSystem.getJson(this, options);
		}
	}
	public partial class IfcStructuralSurfaceMember 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcStructuralSurfaceMemberTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
			node = obj["Thickness"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mThickness);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcStructuralSurfaceMemberTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			obj["Thickness"] = mThickness;
		}
	}
	public partial class IfcStyledItem
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);

			JsonObject jobj = obj["Item"] as JsonObject;
			if (jobj != null)
				Item = extractObject<IfcRepresentationItem>(jobj);
			foreach (IfcStyleAssignmentSelect sas in mDatabase.extractJsonArray<IfcStyleAssignmentSelect>(obj["Styles"] as JsonArray))
				addStyle(sas);	
			var node = obj["Name"];
			if (node != null)
				Name = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mItem != null && Item != host)
				obj["Item"] = Item.getJson(this, options);
			JsonArray array = new JsonArray();
			foreach (IfcStyleAssignmentSelect style in mStyles)
				array.Add(style.getJson(this, options));
			obj["Styles"] = array;
			base.setAttribute(obj, "Name", Name);
		}
	}
	public partial class IfcSurfaceStyle 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Side"];
			if (node != null)
				Enum.TryParse<IfcSurfaceSide>(node.GetValue<string>(), out mSide);
		 	mStyles.AddRange(mDatabase.extractJsonArray<IfcSurfaceStyleElementSelect>(obj["Styles"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Side"] = mSide.ToString();
			createArray(obj, "Styles", Styles, this, options);
		}
	}
	public partial class IfcSurfaceStyleRefraction 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["RefractionIndex"];
			if (node != null)
				mRefractionIndex = node.GetValue<double>();
			JsonObject jobj = obj["DispersionFactor"] as JsonObject;
			if (jobj != null)
				mDispersionFactor = node.GetValue<double>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mRefractionIndex))
				obj["RefractionIndex"] = mRefractionIndex;
			if (!double.IsNaN(mDispersionFactor))
				obj["DispersionFactor"] = mDispersionFactor;
		}
	}
	public partial class IfcSurfaceStyleRendering
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			
			JsonObject jobj = obj["DiffuseColour"] as JsonObject;
			if (jobj != null)
				DiffuseColour = extractObject<IfcColourOrFactor>(jobj);
			jobj = obj["TransmissionColour"] as JsonObject;
			if (jobj != null)
				TransmissionColour = extractObject<IfcColourOrFactor>(jobj);
			jobj = obj["DiffuseTransmissionColour"] as JsonObject;
			if (jobj != null)
				DiffuseTransmissionColour = extractObject<IfcColourOrFactor>(jobj);
			jobj = obj["ReflectionColour"] as JsonObject;
			if (jobj != null)
				ReflectionColour = extractObject<IfcColourOrFactor>(jobj);
			jobj = obj["SpecularColour"] as JsonObject;
			if (jobj != null)
				SpecularColour = extractObject<IfcColourOrFactor>(jobj);
			jobj = obj["SpecularHighlight"] as JsonObject;
			if (jobj != null)
				SpecularHighlight = extractObject<IfcSpecularHighlightSelect>(jobj);
			var node = obj["ReflectanceMethod"];
			if (node != null)
				Enum.TryParse<IfcReflectanceMethodEnum>(node.GetValue<string>(), out mReflectanceMethod);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			
			setJSON(DiffuseColour, "DiffuseColour", obj, this, options);
			setJSON(TransmissionColour, "TransmissionColour", obj, this, options);
			setJSON(DiffuseTransmissionColour, "DiffuseTransmissionColour", obj, this, options);
			setJSON(ReflectionColour, "ReflectionColour", obj, this, options);
			setJSON(SpecularColour, "SpecularColour", obj, this, options);
			if (SpecularHighlight != null)
			{
				BaseClassIfc baseClass = SpecularHighlight as BaseClassIfc;
				if(baseClass != null)
					obj["SpecularHighlight"] = baseClass.getJson(this, options);
				else
				{
					IfcValue value = SpecularHighlight as IfcValue;
					if (value != null)
						obj["SpecularHighlight"] = DatabaseIfc.extract(value); ;
						
				}
			}
			obj["ReflectanceMethod"] = ReflectanceMethod.ToString();
		}
	}
	public partial class IfcSurfaceStyleShading
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["SurfaceColour"] as JsonObject;
			if (jobj != null)
				SurfaceColour = extractObject<IfcColourRgb>(jobj);

			var node = obj["Transparency"];
			if (node != null)
				Transparency = node.GetValue<double>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["SurfaceColour"] = SurfaceColour.getJson(this, options);
			if (!double.IsNaN(mTransparency))
				obj["Transparency"] = Transparency;
		}
	}
	public abstract partial class IfcSweptAreaSolid
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["SweptArea"] as JsonObject;
			if (jobj != null)
				SweptArea = mDatabase.ParseJsonObject<IfcProfileDef>(jobj);
			jobj = obj["Position"] as JsonObject;
			if (jobj != null)
				Position = mDatabase.ParseJsonObject<IfcAxis2Placement3D>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["SweptArea"] = SweptArea.getJson(this, options);
			if (mPosition != null)
				obj["Position"] = Position.getJson(this, options);
		}
	}
	public partial class IfcSweptDiskSolid
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Directrix"] = Directrix.getJson(this, options);
			obj["Radius"] = Radius;
			if (!double.IsNaN(mInnerRadius) && mInnerRadius < mDatabase.Tolerance)
				obj["InnerRadius"] = InnerRadius;
			if (!double.IsNaN(mStartParam))
				obj["StartParam"] = InnerRadius;
			if (!double.IsNaN(mEndParam))
				obj["EndParam"] = InnerRadius;
		}
	}
	public partial class IfcSystem 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			foreach (IfcRelServicesBuildings rsb in mDatabase.extractJsonArray<IfcRelServicesBuildings>(obj["ServicesBuildings"] as JsonArray))
				rsb.RelatingSystem = this;
		}
	}
}
#endif
