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
			PartOfProductDefinitionShape = mDatabase.parseJObject<IfcProductRepresentationSelect>(obj.GetValue("PartOfProductDefinitionShape", StringComparison.InvariantCultureIgnoreCase) as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			JArray array = new JArray();
			foreach (IfcShapeModel sm in ShapeRepresentations)
				array.Add(sm.getJson(this, processed));
			obj["ShapeRepresentations"] = array;
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "ProductDefinitional", mProductDefinitional.ToString());
			if (mPartOfProductDefinitionShape > 0 && mPartOfProductDefinitionShape != host.mIndex)
				obj["PartOfProductDefinitionShape"] = mDatabase[mPartOfProductDefinitionShape].getJson(this, processed);
		}
	}
	public abstract partial class IfcShapeModel : IfcRepresentation//ABSTRACT SUPERTYPE OF (ONEOF (IfcShapeRepresentation,IfcTopologyRepresentation))
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mOfShapeAspect != null)
				obj["OfShapeAspect"] = mOfShapeAspect.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mTemplateType != IfcSimplePropertyTemplateTypeEnum.NOTDEFINED)
				obj["TemplateType"] = mTemplateType.ToString();
			setAttribute(obj, "PrimaryMeasureType", PrimaryMeasureType);
			setAttribute(obj, "SecondaryMeasureType", SecondaryMeasureType);
			if (mEnumerators > 0)
				obj["Enumerators"] = Enumerators.getJson(this, processed);
			if (mPrimaryUnit > 0)
				obj["PrimaryUnit"] = mDatabase[mPrimaryUnit].getJson(this, processed);
			if (mSecondaryUnit > 0)
				obj["SecondaryUnit"] = mDatabase[mSecondaryUnit].getJson(this, processed);
			setAttribute(obj, "Expression", Expression);
			if (mAccessState != IfcStateEnum.NA)
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPrefix != IfcSIPrefix.NONE)
				obj["Prefix"] = mPrefix.ToString();
			obj["Name"] = Name.ToString();
		}
	}
	public partial class IfcSlab : IfcBuildingElement
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSlabTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPredefinedType != IfcSlabTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcSlabType : IfcBuildingElementType
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcSlabTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPredefinedType != IfcSpaceTypeEnum.NOTDEFINED)
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			string longName = LongName;
			if (!string.IsNullOrEmpty(longName))
				obj["LongName"] = longName;

			if (mContainsElements.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelContainedInSpatialStructure con in ContainsElements)
					array.Add(con.getJson(this, processed));
				obj["ContainsElements"] = array;
			}
			//Reference buildings from system ?? not structural analysis
			if (mServicedBySystems.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelServicesBuildings systems in ServicedBySystems)
					array.Add(systems.getJson(this, processed));
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mCompositionType != IfcElementCompositionEnum.NA)
				obj["CompositionType"] = mCompositionType.ToString();
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
				CausedBy = mDatabase.parseJObject<IfcStructuralReaction>(rp);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mDestabilizingLoad == IfcLogicalEnum.UNKNOWN)
			{
				if (mDatabase.Release == ReleaseVersion.IFC2x3)
					obj["DestabilizingLoad"] = false;
			}
			else
				obj["DestabilizingLoad"] = mDestabilizingLoad == IfcLogicalEnum.TRUE;
			if (mCausedBy > 0)
				obj["CausedBy"] = CausedBy.getJson(this, processed);
		}
	}
	public abstract partial class IfcStructuralActivity : IfcProduct
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject rp = obj.GetValue("AppliedLoad", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				AppliedLoad = mDatabase.parseJObject<IfcStructuralLoad>(rp);
			JToken token = obj.GetValue("GlobalOrLocal", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcGlobalOrLocalEnum>(token.Value<string>(), true, out mGlobalOrLocal);
			rp = obj.GetValue("AssignedToStructuralItem", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				AssignedToStructuralItem = mDatabase.parseJObject<IfcRelConnectsStructuralActivity>(rp);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["AppliedLoad"] = AppliedLoad.getJson(this, processed);
			obj["IfcGlobalOrLocalEnum"] = mGlobalOrLocal.ToString();
			if (mAssignedToStructuralItem != null)
				obj["AssignedToStructuralItem"] = mAssignedToStructuralItem.getJson(this, processed);
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
				OrientationOf2DPlane = mDatabase.parseJObject<IfcAxis2Placement3D>(rp);
			mDatabase.extractJArray<IfcStructuralLoadGroup>(obj.GetValue("LoadedBy", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addLoadGroup(x));
			mDatabase.extractJArray<IfcStructuralResultGroup>(obj.GetValue("HasResults", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addResultGroup(x));
			token = obj.GetValue("SharedPlacement", StringComparison.InvariantCultureIgnoreCase) as JToken;
			if (token != null)
			{
				JObject jobj = token as JObject;
				if (jobj != null)
					SharedPlacement = mDatabase.parseJObject<IfcObjectPlacement>(jobj);
				else
					mSharedPlacement = token.Value<int>();
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPredefinedType != IfcAnalysisModelTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (mOrientationOf2DPlane > 0)
				obj["OrientationOf2DPlane"] = OrientationOf2DPlane.getJson(this, processed);
			if (mLoadedBy.Count > 0)
				obj["LoadedBy"] = new JArray(mLoadedBy.ConvertAll(x => mDatabase[x].getJson(this, processed)));
			if (mHasResults.Count > 0)
				obj["HasResults"] = new JArray(mHasResults.ConvertAll(x => mDatabase[x].getJson(this, processed)));
			if (mSharedPlacement > 0)
				obj["SharedPlacement"] = SharedPlacement.getJson(this, processed);
		}
	}
	public abstract partial class IfcStructuralConnection : IfcStructuralItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveConnection ,IfcStructuralPointConnection ,IfcStructuralSurfaceConnection))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);

			JObject rp = obj.GetValue("AppliedCondition", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				AppliedCondition = mDatabase.parseJObject<IfcBoundaryCondition>(rp);
			mConnectsStructuralMembers.AddRange(mDatabase.extractJArray<IfcRelConnectsStructuralMember>(obj.GetValue("ConnectsStructuralMembers", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);

			if (mAppliedCondition > 0)
				obj["AppliedCondition"] = AppliedCondition.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
				Axis = mDatabase.parseJObject<IfcDirection>(rp);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPredefinedType != IfcStructuralCurveMemberTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (mAxis > 0)
				obj["Axis"] = Axis.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
	}
	public partial class IfcStructuralPointConnection : IfcStructuralConnection
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject rp = obj.GetValue("ConditionCoordinateSystem", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				ConditionCoordinateSystem = mDatabase.parseJObject<IfcAxis2Placement3D>(rp);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mConditionCoordinateSystem > 0)
				obj["ConditionCoordinateSystem"] = ConditionCoordinateSystem.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPredefinedType != IfcStructuralSurfaceMemberTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			obj["Thickness"] = mThickness;
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
			mDatabase.extractJArray<IfcSurfaceStyleElementSelect>(obj.GetValue("Styles", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>addStyle(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["Side"] = mSide.ToString();
			obj["Styles"] = new JArray(mStyles.ConvertAll(x => mDatabase[x].getJson(this, processed)));
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
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["SurfaceColour"] = SurfaceColour.getJson(this, processed);
		}
	}
	public abstract partial class IfcSweptAreaSolid : IfcSolidModel  /*ABSTRACT SUPERTYPE OF (ONEOF (IfcExtrudedAreaSolid, IfcFixedReferenceSweptAreaSolid ,IfcRevolvedAreaSolid ,IfcSurfaceCurveSweptAreaSolid))*/
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("SweptArea", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				SweptArea = mDatabase.parseJObject<IfcProfileDef>(jobj);
			jobj = obj.GetValue("Position", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
			{
				JToken token = jobj.GetValue("href", StringComparison.InvariantCultureIgnoreCase);
				if (token != null)
					mPosition = token.Value<int>();
				else
					Position = mDatabase.parseJObject<IfcAxis2Placement3D>(jobj);
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["SweptArea"] = SweptArea.getJson(this, processed);
			if (mPosition > 0)
				obj["Position"] = Position.getJson(this, processed);
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
