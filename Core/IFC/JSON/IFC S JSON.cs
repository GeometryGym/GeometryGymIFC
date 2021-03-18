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
	public partial class IfcSectionedSurface : IfcSurface
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Directrix"] = Directrix.getJson(this, options);
			obj["CrossSectionPositions"] = new JArray(CrossSectionPositions.Select(x => x.getJson(this, options)));
			obj["CrossSections"] = new JArray(CrossSections.Select(x => x.getJson(this, options)));
			obj["FixedAxisVertical"] = mFixedAxisVertical;
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Directrix", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Directrix = mDatabase.ParseJObject<IfcCurve>(jobj);
			CrossSectionPositions.AddRange(mDatabase.extractJArray<IfcPointByDistanceExpression>(obj.GetValue("CrossSectionPositions", StringComparison.InvariantCultureIgnoreCase) as JArray));
			CrossSections.AddRange(mDatabase.extractJArray<IfcProfileDef>(obj.GetValue("CrossSections", StringComparison.InvariantCultureIgnoreCase) as JArray));
			JToken fixedAxisVertical = obj.GetValue("FixedAxisVertical", StringComparison.InvariantCultureIgnoreCase);
			if (fixedAxisVertical != null)
				mFixedAxisVertical = fixedAxisVertical.Value<bool>();
		}
	}
	public abstract partial class IfcSegment : IfcGeometricRepresentationItem
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Transition"] = mTransition.ToString();
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Transition", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcTransitionCode>(token.Value<string>(), true, out mTransition);
		}
	}
	public partial class IfcSegmentedReferenceCurve
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["BaseCurve"] = BaseCurve.getJson(this, options);
			if (EndPoint != null)
				obj["EndPoint"] = EndPoint.getJson(this, options);
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("BaseCurve", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				BaseCurve = mDatabase.ParseJObject<IfcBoundedCurve>(jobj);
			jobj = obj.GetValue("EndPoint", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				EndPoint = mDatabase.ParseJObject<IfcPlacement>(jobj);
		}
	}
	public partial class IfcShapeAspect : BaseClassIfc
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mDatabase.extractJArray<IfcShapeModel>(obj.GetValue("ShapeRepresentations", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addRepresentation(x));
			JToken token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
			token = obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Description = token.Value<string>();
			token = obj.GetValue("ProductDefinitional", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcLogicalEnum>(token.Value<string>(), true, out mProductDefinitional);
			PartOfProductDefinitionShape = mDatabase.ParseJObject<IfcProductRepresentationSelect>(obj.GetValue("PartOfProductDefinitionShape", StringComparison.InvariantCultureIgnoreCase) as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JArray array = new JArray();
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
	public abstract partial class IfcShapeModel : IfcRepresentation<IfcRepresentationItem>//ABSTRACT SUPERTYPE OF (ONEOF (IfcShapeRepresentation,IfcTopologyRepresentation))
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mOfShapeAspect != null)
				obj["OfShapeAspect"] = mOfShapeAspect.getJson(this, options);
		}
	}
	public partial class IfcSign : IfcElementComponent
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSignTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSignTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcSignal : IfcFlowTerminal
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSignalTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSignalTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcSignalType : IfcFlowTerminalType
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSignalTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcSignType : IfcElementComponentType
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSignTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcSimplePropertyTemplate : IfcPropertyTemplate
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("TemplateType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSimplePropertyTemplateTypeEnum>(token.Value<string>(), true, out mTemplateType);
			PrimaryMeasureType = extractString(obj.GetValue("PrimaryMeasureType", StringComparison.InvariantCultureIgnoreCase));
			SecondaryMeasureType = extractString(obj.GetValue("SecondaryMeasureType", StringComparison.InvariantCultureIgnoreCase));
			Enumerators = extractObject<IfcPropertyEnumeration>(obj.GetValue("Enumerators", StringComparison.InvariantCultureIgnoreCase) as JObject);
			PrimaryUnit = extractObject<IfcUnit>(obj.GetValue("PrimaryUnit", StringComparison.InvariantCultureIgnoreCase) as JObject);
			SecondaryUnit = extractObject<IfcUnit>(obj.GetValue("SecondaryUnit", StringComparison.InvariantCultureIgnoreCase) as JObject);
			Expression = extractString(obj.GetValue("Expression", StringComparison.InvariantCultureIgnoreCase));
			token = obj.GetValue("AccessState", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcStateEnum>(token.Value<string>(), true, out mAccessState);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mTemplateType != IfcSimplePropertyTemplateTypeEnum.NOTDEFINED)
				obj["TemplateType"] = mTemplateType.ToString();
			setAttribute(obj, "PrimaryMeasureType", PrimaryMeasureType);
			setAttribute(obj, "SecondaryMeasureType", SecondaryMeasureType);
			if (mEnumerators > 0)
				obj["Enumerators"] = Enumerators.getJson(this, options);
			if (mPrimaryUnit > 0)
				obj["PrimaryUnit"] = mDatabase[mPrimaryUnit].getJson(this, options);
			if (mSecondaryUnit > 0)
				obj["SecondaryUnit"] = mDatabase[mSecondaryUnit].getJson(this, options);
			setAttribute(obj, "Expression", Expression);
			if (mAccessState != IfcStateEnum.NOTDEFINED)
				obj["AccessState"] = mAccessState.ToString();
		}

	}

	public partial class IfcSIUnit : IfcNamedUnit
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Prefix", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSIPrefix>(token.Value<string>(), out mPrefix);
			token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSIUnitName>(token.Value<string>(), out mName);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPrefix != IfcSIPrefix.NONE)
				obj["Prefix"] = mPrefix.ToString();
			obj["Name"] = Name.ToString();
		}
	}
	public partial class IfcSine
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["CosineTerm"] = mSineTerm.ToString();
			obj["Constant"] = mConstant.ToString();
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("CosineTerm", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mSineTerm = token.Value<double>();
			token = obj.GetValue("Constant", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mConstant = token.Value<double>();
		}
	}
	public partial class IfcSlab : IfcBuiltElement
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSlabTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSlabTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcSlabType : IfcBuiltElementType
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSlabTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSlabTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcSpace : IfcSpatialStructureElement, IfcSpaceBoundarySelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSpaceTypeEnum>(token.Value<string>(), out mPredefinedType);
			token = obj.GetValue("ElevationWithFlooring", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mElevationWithFlooring = token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
	public partial class IfcSpaceHeater : IfcFlowTerminal
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSpaceHeaterTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSpaceHeaterTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcSpaceHeaterType : IfcFlowTerminalType
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSpaceHeaterTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcSpaceHeaterTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public abstract partial class IfcSpatialElement : IfcProduct //ABSTRACT SUPERTYPE OF (ONEOF (IfcExternalSpatialStructureElement ,IfcSpatialStructureElement ,IfcSpatialZone))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("LongName", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				LongName = token.Value<string>();
			foreach (IfcRelContainedInSpatialStructure rcss in mDatabase.extractJArray<IfcRelContainedInSpatialStructure>(obj.GetValue("ContainsElements", StringComparison.InvariantCultureIgnoreCase) as JArray))
				rcss.RelatingStructure = this;
			foreach (IfcRelServicesBuildings rsbs in mDatabase.extractJArray<IfcRelServicesBuildings>(obj.GetValue("ServicedBySystems", StringComparison.InvariantCultureIgnoreCase) as JArray))
				rsbs.addRelated(this);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			string longName = LongName;
			if (!string.IsNullOrEmpty(longName))
				obj["LongName"] = longName;

			if (options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				if (mContainsElements.Count > 0)
				{
					JArray array = new JArray();
					foreach (IfcRelContainedInSpatialStructure con in ContainsElements)
						array.Add(con.getJson(this, options));
					obj["ContainsElements"] = array;
				}
			}
			//Reference buildings from system ?? not structural analysis
			if (mServicedBySystems.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelServicesBuildings systems in ServicedBySystems)
					array.Add(systems.getJson(this, options));
				obj["ServicedBySystems"] = array;
			}
		}
	}
	public abstract partial class IfcSpatialStructureElement : IfcSpatialElement /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBuilding ,IfcBuildingStorey ,IfcSite ,IfcSpace, IfcCivilStructureElement))*/
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("CompositionType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcElementCompositionEnum>(token.Value<string>(), out mCompositionType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mCompositionType != IfcElementCompositionEnum.NOTDEFINED)
				obj["CompositionType"] = mCompositionType.ToString();
		}
	}
	public partial class IfcSpiral
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Position"] = Position.getJson(this, options);
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Position", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Position = mDatabase.ParseJObject<IfcAxis2Placement>(jobj);
		}
	}
	public abstract partial class IfcStructuralAction : IfcStructuralActivity // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveAction, IfcStructuralPointAction, IfcStructuralSurfaceAction))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("DestabilizingLoad", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcLogicalEnum>(token.Value<string>(), true, out mDestabilizingLoad);
			JObject rp = obj.GetValue("CausedBy", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				CausedBy = mDatabase.ParseJObject<IfcStructuralReaction>(rp);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mDestabilizingLoad == IfcLogicalEnum.UNKNOWN)
			{
				if (mDatabase.Release < ReleaseVersion.IFC4)
					obj["DestabilizingLoad"] = false;
			}
			else
				obj["DestabilizingLoad"] = mDestabilizingLoad == IfcLogicalEnum.TRUE;
			if (mCausedBy > 0)
				obj["CausedBy"] = CausedBy.getJson(this, options);
		}
	}
	public abstract partial class IfcStructuralActivity : IfcProduct
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject rp = obj.GetValue("AppliedLoad", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				AppliedLoad = mDatabase.ParseJObject<IfcStructuralLoad>(rp);
			JToken token = obj.GetValue("GlobalOrLocal", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcGlobalOrLocalEnum>(token.Value<string>(), true, out mGlobalOrLocal);
			rp = obj.GetValue("AssignedToStructuralItem", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				AssignedToStructuralItem = mDatabase.ParseJObject<IfcRelConnectsStructuralActivity>(rp);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["AppliedLoad"] = AppliedLoad.getJson(this, options);
			obj["IfcGlobalOrLocalEnum"] = mGlobalOrLocal.ToString();
			if (mAssignedToStructuralItem != null)
				obj["AssignedToStructuralItem"] = mAssignedToStructuralItem.getJson(this, options);
		}
	}
	public partial class IfcStructuralAnalysisModel : IfcSystem
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcAnalysisModelTypeEnum>(token.Value<string>(), true, out mPredefinedType);
			JObject rp = obj.GetValue("OrientationOf2DPlane", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				OrientationOf2DPlane = mDatabase.ParseJObject<IfcAxis2Placement3D>(rp);
			mDatabase.extractJArray<IfcStructuralLoadGroup>(obj.GetValue("LoadedBy", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addLoadGroup(x));
			mDatabase.extractJArray<IfcStructuralResultGroup>(obj.GetValue("HasResults", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addResultGroup(x));
			token = obj.GetValue("SharedPlacement", StringComparison.InvariantCultureIgnoreCase) as JToken;
			if (token != null)
			{
				JObject jobj = token as JObject;
				if (jobj != null)
					SharedPlacement = mDatabase.ParseJObject<IfcObjectPlacement>(jobj);
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcAnalysisModelTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (mOrientationOf2DPlane > 0)
				obj["OrientationOf2DPlane"] = OrientationOf2DPlane.getJson(this, options);
			if (mLoadedBy.Count > 0)
				obj["LoadedBy"] = new JArray(mLoadedBy.ConvertAll(x => mDatabase[x].getJson(this, options)));
			if (mHasResults.Count > 0)
				obj["HasResults"] = new JArray(mHasResults.ConvertAll(x => mDatabase[x].getJson(this, options)));
			if (mSharedPlacement != null)
				obj["SharedPlacement"] = SharedPlacement.getJson(this, options);
		}
	}
	public abstract partial class IfcStructuralConnection : IfcStructuralItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveConnection ,IfcStructuralPointConnection ,IfcStructuralSurfaceConnection))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);

			JObject rp = obj.GetValue("AppliedCondition", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				AppliedCondition = mDatabase.ParseJObject<IfcBoundaryCondition>(rp);
			mConnectsStructuralMembers.AddRange(mDatabase.extractJArray<IfcRelConnectsStructuralMember>(obj.GetValue("ConnectsStructuralMembers", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			if (mAppliedCondition > 0)
				obj["AppliedCondition"] = AppliedCondition.getJson(this, options);
			JArray array = new JArray();
			foreach(IfcRelConnectsStructuralMember connects in mConnectsStructuralMembers)
			{
				IfcStructuralMember member = connects.RelatingStructuralMember;
				if(host == null || member.mIndex != host.mIndex)
					array.Add(member.getJson(this, options));
			}
			if (array.Count > 0)
				obj["ConnectsStructuralMembers"] = array;
		}
	}
	public abstract partial class IfcStructuralConnectionCondition : BaseClassIfc //ABSTRACT SUPERTYPE OF (ONEOF (IfcFailureConnectionCondition ,IfcSlippageConnectionCondition));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "Name", Name);
		}
	}
	public partial class IfcStructuralCurveAction : IfcStructuralAction // IFC4 SUPERTYPE OF(IfcStructuralLinearAction)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("ProjectedOrTrue", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcProjectedOrTrueLengthEnum>(token.Value<string>(), true, out mProjectedOrTrue);
			token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcStructuralCurveActivityTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ProjectedOrTrue"] = mProjectedOrTrue.ToString();
			if (mPredefinedType != IfcStructuralCurveActivityTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcStructuralCurveMember : IfcStructuralMember
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcStructuralCurveMemberTypeEnum>(token.Value<string>(), true, out mPredefinedType);
			JObject rp = obj.GetValue("Axis", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				Axis = mDatabase.ParseJObject<IfcDirection>(rp);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcStructuralCurveMemberTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (mAxis > 0)
				obj["Axis"] = Axis.getJson(this, options);
		}
	}
	public partial class IfcStructuralCurveReaction : IfcStructuralReaction
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcStructuralCurveActivityTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcStructuralCurveActivityTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public abstract partial class IfcStructuralItem : IfcProduct, IfcStructuralActivityAssignmentSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralConnection ,IfcStructuralMember))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			foreach (IfcRelConnectsStructuralActivity rcsa in mDatabase.extractJArray<IfcRelConnectsStructuralActivity>(obj.GetValue("AssignedStructuralActivity", StringComparison.InvariantCultureIgnoreCase) as JArray))
				rcsa.RelatingElement = this;
		}
	}
	public partial class IfcStructuralLoadLinearForce : IfcStructuralLoadStatic
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("LinearForceX", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mLinearForceX);
			token = obj.GetValue("LinearForceY", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mLinearForceY);
			token = obj.GetValue("LinearForceZ", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mLinearForceZ);
			token = obj.GetValue("LinearMomentX", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mLinearMomentX);
			token = obj.GetValue("LinearMomentY", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mLinearMomentY);
			token = obj.GetValue("LinearMomentZ", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mLinearMomentZ);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
	public partial class IfcStructuralLoadSingleForce : IfcStructuralLoadStatic
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("ForceX", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mForceX);
			token = obj.GetValue("ForceY", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mForceY);
			token = obj.GetValue("ForceZ", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mForceZ);
			token = obj.GetValue("MomentX", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mMomentX);
			token = obj.GetValue("MomentY", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mMomentY);
			token = obj.GetValue("MomentZ", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mMomentZ);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
	public abstract partial class IfcStructuralMember : IfcStructuralItem //ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralCurveMember, IfcStructuralSurfaceMember))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			foreach (IfcRelConnectsStructuralMember rcsm in mDatabase.extractJArray<IfcRelConnectsStructuralMember>(obj.GetValue("ConnectedBy", StringComparison.InvariantCultureIgnoreCase) as JArray))
				rcsm.RelatingStructuralMember = this;
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			JArray array = new JArray();
			foreach (IfcRelConnectsStructuralMember connects in mConnectedBy)
			{
				IfcStructuralConnection connection = connects.RelatedStructuralConnection;
				if (host == null || connection.mIndex != host.mIndex)
					array.Add(connects.getJson(this, options));
			}
			if (array.Count > 0)
				obj["ConnectedBy"] = array;
		}
	}
	public partial class IfcStructuralPointConnection : IfcStructuralConnection
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject rp = obj.GetValue("ConditionCoordinateSystem", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				ConditionCoordinateSystem = mDatabase.ParseJObject<IfcAxis2Placement3D>(rp);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mConditionCoordinateSystem > 0)
				obj["ConditionCoordinateSystem"] = ConditionCoordinateSystem.getJson(this, options);
		}
	}
	public partial class IfcStructuralSurfaceMember : IfcStructuralMember
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcStructuralSurfaceMemberTypeEnum>(token.Value<string>(), true, out mPredefinedType);
			token = obj.GetValue("Thickness", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mThickness);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcStructuralSurfaceMemberTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			obj["Thickness"] = mThickness;
		}
	}
	public partial class IfcStyledItem : IfcRepresentationItem
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);

			JObject jobj = obj.GetValue("Item", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Item = extractObject<IfcRepresentationItem>(jobj);
			foreach (IfcStyleAssignmentSelect sas in mDatabase.extractJArray<IfcStyleAssignmentSelect>(obj.GetValue("Styles", StringComparison.InvariantCultureIgnoreCase) as JArray))
				addStyle(sas);	
			JToken token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mItem != null && Item != host)
				obj["Item"] = Item.getJson(this, options);
			JArray array = new JArray();
			foreach (IfcStyleAssignmentSelect style in mStyles)
				array.Add(style.getJson(this, options));
			obj["Styles"] = array;
			base.setAttribute(obj, "Name", Name);
		}
	}
	public partial class IfcSurfaceStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Side", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSurfaceSide>(token.Value<string>(), out mSide);
		 	mStyles.AddRange(mDatabase.extractJArray<IfcSurfaceStyleElementSelect>(obj.GetValue("Styles", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Side"] = mSide.ToString();
			obj["Styles"] = new JArray(mStyles.ConvertAll(x => x.getJson(this, options)));
		}
	}
	public partial class IfcSurfaceStyleRefraction : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("RefractionIndex", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mRefractionIndex = token.Value<double>();
			JObject jobj = obj.GetValue("DispersionFactor", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				mDispersionFactor = token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mRefractionIndex))
				obj["RefractionIndex"] = mRefractionIndex;
			if (!double.IsNaN(mDispersionFactor))
				obj["DispersionFactor"] = mDispersionFactor;
		}
	}
	public partial class IfcSurfaceStyleRendering : IfcSurfaceStyleShading
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			
			JObject jobj = obj.GetValue("DiffuseColour", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				DiffuseColour = extractObject<IfcColourOrFactor>(jobj);
			jobj = obj.GetValue("TransmissionColour", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				TransmissionColour = extractObject<IfcColourOrFactor>(jobj);
			jobj = obj.GetValue("DiffuseTransmissionColour", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				DiffuseTransmissionColour = extractObject<IfcColourOrFactor>(jobj);
			jobj = obj.GetValue("ReflectionColour", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				ReflectionColour = extractObject<IfcColourOrFactor>(jobj);
			jobj = obj.GetValue("SpecularColour", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				SpecularColour = extractObject<IfcColourOrFactor>(jobj);
			jobj = obj.GetValue("SpecularHighlight", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				SpecularHighlight = extractObject<IfcSpecularHighlightSelect>(jobj);
			JToken token = obj.GetValue("ReflectanceMethod", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcReflectanceMethodEnum>(token.Value<string>(), out mReflectanceMethod);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
	public partial class IfcSurfaceStyleShading : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("SurfaceColour", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				SurfaceColour = extractObject<IfcColourRgb>(jobj);

			JToken token = obj.GetValue("Transparency", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Transparency = token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["SurfaceColour"] = SurfaceColour.getJson(this, options);
			if (!double.IsNaN(mTransparency))
				obj["Transparency"] = Transparency;
		}
	}
	public abstract partial class IfcSweptAreaSolid : IfcSolidModel  /*ABSTRACT SUPERTYPE OF (ONEOF (IfcExtrudedAreaSolid, IfcFixedReferenceSweptAreaSolid ,IfcRevolvedAreaSolid ,IfcSurfaceCurveSweptAreaSolid))*/
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("SweptArea", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				SweptArea = mDatabase.ParseJObject<IfcProfileDef>(jobj);
			jobj = obj.GetValue("Position", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Position = mDatabase.ParseJObject<IfcAxis2Placement3D>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["SweptArea"] = SweptArea.getJson(this, options);
			if (mPosition != null)
				obj["Position"] = Position.getJson(this, options);
		}
	}
	public partial class IfcSweptDiskSolid : IfcSolidModel
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
	public partial class IfcSystem : IfcGroup //SUPERTYPE OF(ONEOF(IfcBuildingSystem, IfcDistributionSystem, IfcStructuralAnalysisModel, IfcZone))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			foreach (IfcRelServicesBuildings rsb in mDatabase.extractJArray<IfcRelServicesBuildings>(obj.GetValue("ServicesBuildings", StringComparison.InvariantCultureIgnoreCase) as JArray))
				rsb.RelatingSystem = this;
		}
	}
}
