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
using System.Collections.Concurrent;
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mOuter); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mOuter = ParserSTEP.StripLink(str, ref pos, len); }
	} 
	public partial class IfcMapConversion : IfcCoordinateOperation //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mEastings) + "," + ParserSTEP.DoubleToString(mNorthings) + "," + ParserSTEP.DoubleToString(mOrthogonalHeight) + "," + ParserSTEP.DoubleOptionalToString(mXAxisAbscissa) + "," + ParserSTEP.DoubleOptionalToString(mXAxisOrdinate) + "," + ParserSTEP.DoubleOptionalToString(mScale); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mMappingSource) + "," + ParserSTEP.LinkToString(mMappingTarget); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",'" + mName + (release > ReleaseVersion.IFC2x3 ?  (mDescription == "$" ? "',$," : "','" + mDescription + "',") + (mCategory == "$" ? "$" : "'" + mCategory + "'") : "'" ); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
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
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.LinkToString(mMaterialClassifications[0]);
			for (int icounter = 1; icounter < mMaterialClassifications.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterialClassifications[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mClassifiedMaterial);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mMaterialClassifications = ParserSTEP.StripListLink(str, ref pos, len);
			mClassifiedMaterial = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcMaterialConstituent : IfcMaterialDefinition //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserSTEP.LinkToString(mMaterial) + "," + ParserSTEP.DoubleToString(mFraction) + (mCategory == "$" ? ",$" : ",'" + mDescription + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
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
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = ParserSTEP.LinkToString(mMaterialConstituents[0]);
			for (int icounter = 1; icounter < mMaterialConstituents.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterialConstituents[icounter]);
			return base.BuildStringSTEP(release) + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,(" : "'" + mDescription + "',(") + str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			mMaterialConstituents = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcMaterialDefinitionRepresentation : IfcProductRepresentation
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRepresentedMaterial); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
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
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string s = (release < ReleaseVersion.IFC4 ? "" : (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + (mCategory == "$" ? "$," : "'" + mCategory + "',") + ParserSTEP.DoubleOptionalToString(mPriority));
			return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mMaterial) + "," + ParserSTEP.DoubleToString(mLayerThickness) + "," + ParserIfc.LogicalToString(mIsVentilated) + s;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
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
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.LinkToString(mMaterialLayers[0]);
			for (int icounter = 1; icounter < mMaterialLayers.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterialLayers[icounter]);
			return str + (mLayerSetName == "$" ? "),$" : "),'" + mLayerSetName + "'") + (release < ReleaseVersion.IFC4 ? "" : (mDescription == "$" ? ",$" : ",'" + mDescription + "'"));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mMaterialLayers = ParserSTEP.StripListLink(str, ref pos, len);
			mLayerSetName = ParserSTEP.StripString(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
				mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcMaterialLayerSetUsage : IfcMaterialUsageDefinition
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mForLayerSet) + ",." + mLayerSetDirection.ToString() + ".,." + mDirectionSense.ToString() + ".," + ParserSTEP.DoubleToString(mOffsetFromReferenceLine) + (release < ReleaseVersion.IFC4 ? "" : "," + ParserSTEP.DoubleOptionalToString(mReferenceExtent)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mForLayerSet = ParserSTEP.StripLink(str, ref pos, len);
			Enum.TryParse<IfcLayerSetDirectionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mLayerSetDirection);
			Enum.TryParse<IfcDirectionSenseEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mDirectionSense);
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mOffsetDirection.ToString() + ".,(" + ParserSTEP.DoubleToString( mOffsetValues[0]) + (Math.Abs(mOffsetValues[1]) > mDatabase.Tolerance ? "," + ParserSTEP.DoubleToString( mOffsetValues[1]) : "") + ")"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcLayerSetDirectionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mOffsetDirection);
			string s = ParserSTEP.StripField(str, ref pos, len);
			string[] ss = s.Substring(1, s.Length - 2).Split(",".ToCharArray());
			mOffsetValues[0] = ParserSTEP.ParseDouble(ss[0]);
			if (ss.Length > 1)
				mOffsetValues[1] = ParserSTEP.ParseDouble(ss[1]);
		}
	}
	public partial class IfcMaterialLayerWithOffsets : IfcMaterialLayer
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mOffsetDirection.ToString() + ".(," + ParserSTEP.DoubleToString(mOffsetValues[0]) + (mOffsetValues.Length > 1 ? "," + ParserSTEP.DoubleToString(mOffsetValues[1]) : "") + ")"; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcLayerSetDirectionEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mOffsetDirection);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> arrNodes = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			mOffsetValues = arrNodes.ConvertAll(x => ParserSTEP.ParseDouble(x)).ToArray();
		}
	}
	public partial class IfcMaterialList : BaseClassIfc, IfcMaterialSelect //DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(" + ParserSTEP.LinkToString(mMaterials[0]);
			for (int icounter = 1; icounter < mMaterials.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mMaterials[icounter]);
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mMaterials = ParserSTEP.StripListLink(str, ref pos, len); }
	}
	public partial class IfcMaterialProfile : IfcMaterialDefinition // IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release) + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserSTEP.ObjToLinkString(mMaterial) + "," + ParserSTEP.ObjToLinkString(mProfile) + (mPriority >= 0 && mPriority <= 100 ? "," + mPriority+ "," : ",$,") + (mCategory == "$" ? "$" : "'" + mCategory + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			Material = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterial;
			Profile = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
			string s = ParserSTEP.StripField(str, ref pos, len);
			double d = 0;
			if( double.TryParse(s, out d)) //Was normalizedRatioMeasure
				Priority =(int)d;
			mCategory = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcMaterialProfileSet : IfcMaterialDefinition //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4 || mMaterialProfiles.Count == 0)
				return "";
			return base.BuildStringSTEP(release) + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,(#" : "'" + mDescription + "',(#") + string.Join(",#", mMaterialProfiles.ConvertAll(x=>x.mIndex)) + ")," + ParserSTEP.ObjToLinkString(mCompositeProfile);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
			MaterialProfiles.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcMaterialProfile));
			CompositeProfile = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCompositeProfileDef;
		}
	}
	public partial class IfcMaterialProfileSetUsage : IfcMaterialUsageDefinition //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 || AssociatedTo.SelectMany(x => x.mRelatedObjects).Count() == 0 ? "" : base.BuildStringSTEP(release) + ",#" + mForProfileSet.Index + "," + (mCardinalPoint == IfcCardinalPointReference.DEFAULT ? "$" : ((int)mCardinalPoint).ToString()) + "," + ParserSTEP.DoubleOptionalToString(mReferenceExtent)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			ForProfileSet = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterialProfileSet;
			CardinalPoint = (IfcCardinalPointReference)ParserSTEP.StripInt(str, ref pos, len);
			ReferenceExtent = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcMaterialProfileSetUsageTapering : IfcMaterialProfileSetUsage //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 || AssociatedTo.SelectMany(x => x.mRelatedObjects).Count() == 0 ? "" : base.BuildStringSTEP(release) + ",#" + ForProfileEndSet.Index + "," + (int)mCardinalEndPoint); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			ForProfileEndSet = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterialProfileSet;
			mCardinalEndPoint = (IfcCardinalPointReference)ParserSTEP.StripInt(str, ref pos, len);
		}
	}
	public partial class IfcMaterialProfileWithOffsets : IfcMaterialProfile //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",(" + ParserSTEP.DoubleToString(mOffsetValues[0]) + (mOffsetValues.Length > 1 ? "," + ParserSTEP.DoubleToString(mOffsetValues[1]) + ")" : ")"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			List<string> arrNodes = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
			mOffsetValues = arrNodes.ConvertAll(x => ParserSTEP.ParseDouble(x)).ToArray();
		}
	}
	public partial class IfcMaterialProperties : IfcExtendedProperties //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (release >= ReleaseVersion.IFC4  ? base.BuildStringSTEP(release) : "" ) + ",#" + mMaterial.Index;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Material = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterialDefinition;
		}
	}
	public partial class IfcMaterialRelationship : IfcResourceLevelRelationship //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return "";
			string result = base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mRelatingMaterial) + ",(" + ParserSTEP.LinkToString(mRelatedMaterials[0]);
			for (int icounter = 1; icounter < mRelatedMaterials.Count; icounter++)
				result += "," + ParserSTEP.LinkToString(mRelatedMaterials[icounter]);
			return result += mExpression == "$" ? "),$" : "),'" + mExpression + "'";

		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRelatingMaterial = ParserSTEP.StripLink(str, ref pos, len);
			mRelatedMaterials = ParserSTEP.StripListLink(str, ref pos, len);
			mExpression = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcMeasureWithUnit : BaseClassIfc, IfcAppliedValueSelect
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + (mValueComponent != null ? mValueComponent.ToString() : mVal) + "," + ParserSTEP.LinkToString(mUnitComponent); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mCompressiveStrength) + "," + ParserSTEP.DoubleOptionalToString(mMaxAggregateSize) + (mAdmixturesDescription == "$" ? ",$," : ",'" + mAdmixturesDescription + "',") + (mWorkability == "$" ? "$," : "'" + mWorkability + "',") + ParserSTEP.DoubleOptionalToString(mProtectivePoreRatio) + (mWaterImpermeability== "$" ? ",$" : ",'" + mWaterImpermeability + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + "," + ParserSTEP.DoubleOptionalToString(mNominalLength) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcMechanicalFastenerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
			mNominalLength = ParserSTEP.StripDouble(str, ref pos, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcMechanicalFastenerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcMechanicalFastenerType : IfcElementComponentType //IFC4 change
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : "," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + "," + ParserSTEP.DoubleOptionalToString(mNominalLength) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				Enum.TryParse<IfcMechanicalFastenerTypeEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPredefinedType);
				mNominalDiameter = ParserSTEP.StripDouble(str, ref pos, len);
				mNominalLength = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
	public partial class IfcMechanicalMaterialProperties : IfcMaterialProperties // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mDynamicViscosity) + "," + ParserSTEP.DoubleOptionalToString(mYoungModulus) + "," + ParserSTEP.DoubleOptionalToString(mShearModulus) + "," + ParserSTEP.DoubleOptionalToString(mPoissonRatio) + "," + ParserSTEP.DoubleOptionalToString(mThermalExpansionCoefficient); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mDynamicViscosity = ParserSTEP.StripDouble(str, ref pos, len);
			mYoungModulus = ParserSTEP.StripDouble(str, ref pos, len);
			mShearModulus = ParserSTEP.StripDouble(str, ref pos, len);
			mPoissonRatio = ParserSTEP.StripDouble(str, ref pos, len);
			mThermalExpansionCoefficient = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcMechanicalSteelMaterialProperties : IfcMechanicalMaterialProperties // DEPRECEATED IFC4
	{
		
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mYieldStress) + "," + ParserSTEP.DoubleOptionalToString(mUltimateStress) + "," + ParserSTEP.DoubleOptionalToString(mUltimateStrain) + "," + ParserSTEP.DoubleOptionalToString(mHardeningModule) + "," + ParserSTEP.DoubleOptionalToString(mProportionalStress) + "," + ParserSTEP.DoubleOptionalToString(mPlasticStrain);
			if (mRelaxations.Count == 0)
				return str + ",$";
			str += ",(" + ParserSTEP.LinkToString(mRelaxations[0]);
			for (int icounter = 1; icounter < mRelaxations.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelaxations[icounter]);
			return str += ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcMedicalDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMedicalDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcMedicalDeviceType : IfcFlowTerminalType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMedicalDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcMember : IfcBuildingElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcMemberTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcMemberTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcMemberType : IfcBuildingElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMemberTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcMetric : IfcConstraint
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mBenchMark.ToString() + (mValueSource == "$" ? ".,$," : ".,'" + mValueSource + "',") + (mDataValueValue == null ? ParserSTEP.LinkToString(mDataValue) : mDataValueValue.ToString()) + (mDatabase.Release < ReleaseVersion.IFC4 ? "" : "," + ParserSTEP.LinkToString(mReferencePath)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcBenchmarkEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mBenchMark);
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? ",." + mCurrency + "." : ",'" + ParserIfc.Encode(mCurrency) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mCurrency = ParserIfc.Decode( ParserSTEP.StripField(str, ref pos, len).Replace(release < ReleaseVersion.IFC4 ? "." : "'", ""));
		}
	}
	public partial class IfcMotorConnection : IfcEnergyConversionDevice //IFC4
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcMotorConnectionTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMotorConnectionTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcMotorConnectionType : IfcEnergyConversionDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMotorConnectionTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	//ENTITY IfcMove // DEPRECEATED IFC4
}
