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
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public abstract partial class IfcManifoldSolidBrep : IfcSolidModel //ABSTRACT SUPERTYPE OF(ONEOF(IfcAdvancedBrep, IfcFacetedBrep))
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mOuter); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mOuter = ParserSTEP.StripLink(str, ref pos, len); }
	} 
	public partial class IfcMapConversion : IfcCoordinateOperation //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mEastings) + "," + ParserSTEP.DoubleToString(mNorthings) + "," + ParserSTEP.DoubleToString(mOrthogonalHeight) + "," + ParserSTEP.DoubleOptionalToString(mXAxisAbscissa) + "," + ParserSTEP.DoubleOptionalToString(mXAxisOrdinate) + "," + ParserSTEP.DoubleOptionalToString(mScale); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mEastings = ParserSTEP.StripDouble(str, ref pos, len);
			mNorthings = ParserSTEP.StripDouble(str, ref pos, len);
			mOrthogonalHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mXAxisAbscissa = ParserSTEP.StripDouble(str, ref pos, len);
			mXAxisOrdinate = ParserSTEP.StripDouble(str, ref pos, len);
			mScale = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcMappedItem : IfcRepresentationItem
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mMappingSource) + "," + ParserSTEP.LinkToString(mMappingTarget); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mMappingSource = ParserSTEP.StripLink(str, ref pos, len);
			mMappingTarget = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			MappingSource.mMapUsage.Add(this);
		}
	}
	public partial class IfcMaterial : IfcMaterialDefinition
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mName + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "'" : (mDescription == "$" ? "',$," : "','" + mDescription + "',") + (mCategory == "$" ? "$" : "'" + mCategory + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				try
				{
					mDescription = ParserSTEP.StripString(str, ref pos, len);
					mCategory = ParserSTEP.StripString(str, ref pos, len);
				}
				catch (Exception) { }
			}
		}
	}
	public partial class IfcMaterialClassificationRelationship : BaseClassIfc
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mMaterialClassifications[0]);
			for (int icounter = 1; icounter < mMaterialClassifications.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterialClassifications[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mClassifiedMaterial);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mMaterialClassifications = ParserSTEP.StripListLink(str, ref pos, len);
			mClassifiedMaterial = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcMaterialConstituent : IfcMaterialDefinition //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserSTEP.LinkToString(mMaterial) + "," + ParserSTEP.DoubleToString(mFraction) + (mCategory == "$" ? ",$" : ",'" + mDescription + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mMaterial = ParserSTEP.StripLink(str, ref pos, len);
			mFraction = ParserSTEP.StripDouble(str, ref pos, len);
			mCategory = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcMaterialConstituentSet : IfcMaterialDefinition
	{
		protected override string BuildStringSTEP()
		{
			string str = ParserSTEP.LinkToString(mMaterialConstituents[0]);
			for (int icounter = 1; icounter < mMaterialConstituents.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterialConstituents[icounter]);
			return base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,(" : "'" + mDescription + "',(") + str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mMaterialConstituents = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcMaterialDefinitionRepresentation : IfcProductRepresentation
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRepresentedMaterial); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRepresentedMaterial = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RepresentedMaterial.mHasRepresentation = this;
		}
	}
	public partial class IfcMaterialLayer : IfcMaterialDefinition
	{
		protected override string BuildStringSTEP()
		{
			string s = (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + (mCategory == "$" ? "$," : "'" + mCategory + "',") + ParserSTEP.DoubleOptionalToString(mPriority));
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mMaterial) + "," + ParserSTEP.DoubleToString(mLayerThickness) + "," + ParserIfc.LogicalToString(mIsVentilated) + s;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mMaterial = ParserSTEP.StripLink(str, ref pos, len);
			mLayerThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mIsVentilated = ParserIfc.StripLogical(str, ref pos, len);
			try
			{
				if (release != ReleaseVersion.IFC2x3)
				{
					mName = ParserSTEP.StripString(str, ref pos, len);
					mDescription = ParserSTEP.StripString(str, ref pos, len);
					mCategory = ParserSTEP.StripString(str, ref pos, len);
					mPriority = ParserSTEP.StripDouble(str, ref pos, len);
				}
			}
			catch (Exception) { }
		}
	}
	public partial class IfcMaterialLayerSet : IfcMaterialDefinition
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mMaterialLayers[0]);
			for (int icounter = 1; icounter < mMaterialLayers.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterialLayers[icounter]);
			return str + (mLayerSetName == "$" ? "),$" : "),'" + mLayerSetName + "'") + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mDescription == "$" ? ",$" : ",'" + mDescription + "'"));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mMaterialLayers = ParserSTEP.StripListLink(str, ref pos, len);
			mLayerSetName = ParserSTEP.StripString(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
				mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcMaterialLayerSetUsage : IfcMaterialUsageDefinition
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mForLayerSet) + ",." + mLayerSetDirection.ToString() + ".,." + mDirectionSense.ToString() + ".," + ParserSTEP.DoubleToString(mOffsetFromReferenceLine) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.DoubleOptionalToString(mReferenceExtent)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mForLayerSet = ParserSTEP.StripLink(str, ref pos, len);
			Enum.TryParse<IfcLayerSetDirectionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mLayerSetDirection);
			Enum.TryParse<IfcDirectionSenseEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mDirectionSense);
			mOffsetFromReferenceLine = ParserSTEP.StripDouble(str, ref pos, len);
			try
			{
				if (release != ReleaseVersion.IFC2x3)
					mReferenceExtent = ParserSTEP.StripDouble(str, ref pos, len);
			}
			catch (Exception) { }
		}
	}
	public partial class IfcMaterialLayerSetWithOffsets : IfcMaterialLayerSet
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mOffsetDirection.ToString() + ".,(" + ParserSTEP.DoubleToString( mOffsetValues[0]) + (Math.Abs(mOffsetValues[1]) > mDatabase.Tolerance ? "," + ParserSTEP.DoubleToString( mOffsetValues[1]) : "") + ")"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			Enum.TryParse<IfcLayerSetDirectionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mOffsetDirection);
			string s = ParserSTEP.StripField(str, ref pos, len);
			string[] ss = s.Substring(1, s.Length - 2).Split(",".ToCharArray());
			mOffsetValues[0] = ParserSTEP.ParseDouble(ss[0]);
			if (ss.Length > 1)
				mOffsetValues[1] = ParserSTEP.ParseDouble(ss[1]);
		}
	}
	public partial class IfcMaterialLayerWithOffsets : IfcMaterialLayer
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mOffsetDirection.ToString() + ".(," + ParserSTEP.DoubleToString(mOffsetValues[0]) + (mOffsetValues.Length > 1 ? "," + ParserSTEP.DoubleToString(mOffsetValues[1]) : "") + ")"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			Enum.TryParse<IfcLayerSetDirectionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mOffsetDirection);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> arrNodes = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			mOffsetValues = arrNodes.ConvertAll(x => ParserSTEP.ParseDouble(x)).ToArray();
		}
	}
	public partial class IfcMaterialList : BaseClassIfc, IfcMaterialSelect //DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mMaterials[0]);
			for (int icounter = 1; icounter < mMaterials.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterials[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mMaterials = ParserSTEP.StripListLink(str, ref pos, len); }
	}
	public partial class IfcMaterialProfile : IfcMaterialDefinition // IFC4
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserSTEP.LinkToString(mMaterial) + "," + ParserSTEP.LinkToString(mProfile) + (mPriority >= 0 && mPriority <= 100 ? "," + mPriority+ "," : ",$,") + (mCategory == "$" ? "$" : "'" + mCategory + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mMaterial = ParserSTEP.StripLink(str, ref pos, len);
			mProfile = ParserSTEP.StripLink(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			double d = 0;
			if( double.TryParse(s, out d)) //Was normalizedRatioMeasure
				Priority =(int)d;
			mCategory = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcMaterialProfileSet : IfcMaterialDefinition //IFC4
	{
		protected override string BuildStringSTEP()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mMaterialProfiles.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,(" : "'" + mDescription + "',(") + ParserSTEP.LinkToString(mMaterialProfiles[0]);
			for (int icounter = 1; icounter < mMaterialProfiles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterialProfiles[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mCompositeProfile);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mMaterialProfiles = ParserSTEP.StripListLink(str, ref pos, len);
			mCompositeProfile = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcMaterialProfileSetUsage : IfcMaterialUsageDefinition //IFC4
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mAssociatedTo == null || Associates.mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mForProfileSet) + "," + (mCardinalPoint == IfcCardinalPointReference.DEFAULT ? "$" : ((int)mCardinalPoint).ToString()) + "," + ParserSTEP.DoubleOptionalToString(mReferenceExtent)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mForProfileSet = ParserSTEP.StripLink(str, ref pos, len);
			mCardinalPoint = (IfcCardinalPointReference)ParserSTEP.StripInt(str, ref pos, len);
			mReferenceExtent = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcMaterialProfileSetUsageTapering : IfcMaterialProfileSetUsage //IFC4
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mAssociatedTo == null || Associates.mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mForProfileEndSet) + "," + (int)mCardinalEndPoint); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mForProfileEndSet = ParserSTEP.StripLink(str, ref pos, len);
			mCardinalEndPoint = (IfcCardinalPointReference)ParserSTEP.StripInt(str, ref pos, len);
		}
	}
	public partial class IfcMaterialProfileWithOffsets : IfcMaterialProfile //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",(" + ParserSTEP.DoubleToString(mOffsetValues[0]) + (mOffsetValues.Length > 1 ? "," + ParserSTEP.DoubleToString(mOffsetValues[1]) + ")" : ")"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> arrNodes = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			mOffsetValues = arrNodes.ConvertAll(x => ParserSTEP.ParseDouble(x)).ToArray();
		}
	}
	public abstract partial class IfcMaterialPropertiesSuperseded : BaseClassIfc //ABSTRACT SUPERTYPE OF (ONE(IfcExtendedMaterialProperties,IfcFuelProperties,IfcGeneralMaterialProperties,IfcHygroscopicMaterialProperties,IfcMechanicalMaterialProperties,IfcOpticalMaterialProperties,IfcProductsOfCombustionProperties,IfcThermalMaterialProperties,IfcWaterProperties));
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mMaterial); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len) { mMaterial = ParserSTEP.StripLink(str, ref pos, len); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			Material.mHasPropertiesSS.Add(this);
		}
	}
	public partial class IfcMaterialProperties : IfcExtendedProperties //IFC4
	{
		protected override string BuildStringSTEP()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
				return "";
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mMaterial);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mMaterial = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			Material.AddProperties(this);
		}
	}
	public partial class IfcMaterialRelationship : IfcResourceLevelRelationship //IFC4
	{
		protected override string BuildStringSTEP()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
				return "";
			string result = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingMaterial) + ",(" + ParserSTEP.LinkToString(mRelatedMaterials[0]);
			for (int icounter = 1; icounter < mRelatedMaterials.Count; icounter++)
				result += "," + ParserSTEP.LinkToString(mRelatedMaterials[icounter]);
			return result += mExpression == "$" ? "),$" : "),'" + mExpression + "'";

		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mRelatingMaterial = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedMaterials = ParserSTEP.StripListLink(str, ref pos, len);
			mExpression = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcMeasureWithUnit : BaseClassIfc, IfcAppliedValueSelect
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + (mValueComponent != null ? mValueComponent.ToString() : mVal) + "," + ParserSTEP.LinkToString(mUnitComponent); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			mValueComponent = ParserIfc.parseValue(s);
			if (mValueComponent == null)
				mVal = s;
			mUnitComponent = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcMechanicalConcreteMaterialProperties : IfcMechanicalMaterialProperties // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mCompressiveStrength) + "," + ParserSTEP.DoubleOptionalToString(mMaxAggregateSize) + (mAdmixturesDescription == "$" ? ",$," : ",'" + mAdmixturesDescription + "',") + (mWorkability == "$" ? "$," : "'" + mWorkability + "',") + ParserSTEP.DoubleOptionalToString(mProtectivePoreRatio) + (mWaterImpermeability== "$" ? ",$" : ",'" + mWaterImpermeability + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mCompressiveStrength = ParserSTEP.StripDouble(str, ref pos, len);
			mMaxAggregateSize = ParserSTEP.StripDouble(str, ref pos, len);
			mAdmixturesDescription = ParserSTEP.StripString(str, ref pos, len);
			mWorkability = ParserSTEP.StripString(str, ref pos, len);
			mProtectivePoreRatio = ParserSTEP.StripDouble(str, ref pos, len);
			mWaterImpermeability = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcMechanicalFastener : IfcElementComponent //IFC4 change
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + "," + ParserSTEP.DoubleOptionalToString(mNominalLength) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcMechanicalFastenerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mNominalLength = ParserSTEP.StripDouble(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcMechanicalFastenerTypeEnum>(s.Replace(".", ""), out mPredefinedType);
			}
		}
	}
	public partial class IfcMechanicalFastenerType : IfcElementComponentType //IFC4 change
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + "," + ParserSTEP.DoubleOptionalToString(mNominalLength) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				Enum.TryParse<IfcMechanicalFastenerTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".",""), out mPredefinedType);
				mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
				mNominalLength = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
	public partial class IfcMechanicalMaterialProperties : IfcMaterialPropertiesSuperseded // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mDynamicViscosity) + "," + ParserSTEP.DoubleOptionalToString(mYoungModulus) + "," + ParserSTEP.DoubleOptionalToString(mShearModulus) + "," + ParserSTEP.DoubleOptionalToString(mPoissonRatio) + "," + ParserSTEP.DoubleOptionalToString(mThermalExpansionCoefficient); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mDynamicViscosity = ParserSTEP.StripDouble(str, ref pos, len);
			mYoungModulus = ParserSTEP.StripDouble(str, ref pos, len);
			mShearModulus = ParserSTEP.StripDouble(str, ref pos, len);
			mPoissonRatio = ParserSTEP.StripDouble(str, ref pos, len);
			mThermalExpansionCoefficient = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcMechanicalSteelMaterialProperties : IfcMechanicalMaterialProperties // DEPRECEATED IFC4
	{
		
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mYieldStress) + "," + ParserSTEP.DoubleOptionalToString(mUltimateStress) + "," + ParserSTEP.DoubleOptionalToString(mUltimateStrain) + "," + ParserSTEP.DoubleOptionalToString(mHardeningModule) + "," + ParserSTEP.DoubleOptionalToString(mProportionalStress) + "," + ParserSTEP.DoubleOptionalToString(mPlasticStrain);
			if (mRelaxations.Count == 0)
				return str + ",$";
			str += ",(" + ParserSTEP.LinkToString(mRelaxations[0]);
			for (int icounter = 1; icounter < mRelaxations.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelaxations[icounter]);
			return str += ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mYieldStress = ParserSTEP.StripDouble(str, ref pos, len);
			mUltimateStress = ParserSTEP.StripDouble(str, ref pos, len);
			mUltimateStrain = ParserSTEP.StripDouble(str, ref pos, len);
			mHardeningModule = ParserSTEP.StripDouble(str, ref pos, len);
			mProportionalStress = ParserSTEP.StripDouble(str, ref pos, len);
			mPlasticStrain = ParserSTEP.StripDouble(str, ref pos, len);
			mRelaxations = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcMedicalDevice : IfcFlowTerminal //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcMedicalDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMedicalDeviceTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcMedicalDeviceType : IfcFlowTerminalType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMedicalDeviceTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcMember : IfcBuildingElement
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcMemberTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcMemberTypeEnum>(s.Replace(".", ""), out mPredefinedType);
			}
		}
	}
	public partial class IfcMemberType : IfcBuildingElementType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMemberTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcMetric : IfcConstraint
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mBenchMark.ToString() + (mValueSource == "$" ? ".,$," : ".,'" + mValueSource + "',") + (mDataValueValue == null ? ParserSTEP.LinkToString(mDataValue) : mDataValueValue.ToString()) + (mDatabase.Release == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.LinkToString(mReferencePath)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			Enum.TryParse<IfcBenchmarkEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), out mBenchMark);
			mValueSource = ParserSTEP.StripString(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			mDataValueValue = ParserIfc.parseValue(s);
			if (mDataValueValue == null)
				mDataValue = ParserSTEP.ParseLink(s);
			mReferencePath = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcMonetaryUnit : BaseClassIfc, IfcUnit
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ",." + mCurrency + "." : ",'" + mCurrency + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mCurrency = ParserSTEP.StripField(str, ref pos, len).Replace(release == ReleaseVersion.IFC2x3 ? "." : "'", "");
		}
	}
	public partial class IfcMotorConnection : IfcEnergyConversionDevice //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcMotorConnectionTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMotorConnectionTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcMotorConnectionType : IfcEnergyConversionDeviceType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMotorConnectionTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	//ENTITY IfcMove // DEPRECEATED IFC4
}
