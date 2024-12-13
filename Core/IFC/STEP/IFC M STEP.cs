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
	public abstract partial class IfcManifoldSolidBrep
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mOuter.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mOuter = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcClosedShell;
		}
	}
	public partial class IfcMapConversion
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return "";
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mEastings) + "," + ParserSTEP.DoubleToString(mNorthings) + "," +
				ParserSTEP.DoubleToString(mOrthogonalHeight) + "," + ParserSTEP.DoubleOptionalToString(mXAxisAbscissa) + "," +
				ParserSTEP.DoubleOptionalToString(mXAxisOrdinate) + "," + ParserSTEP.DoubleOptionalToString(mScale) +
				(release < ReleaseVersion.IFC4X3_RC3 || release >= ReleaseVersion.IFC4X3_ADD2 ? "" : "," + ParserSTEP.DoubleOptionalToString(mScaleY) + "," + ParserSTEP.DoubleOptionalToString(mScaleZ));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mEastings = ParserSTEP.StripDouble(str, ref pos, len);
			mNorthings = ParserSTEP.StripDouble(str, ref pos, len);
			mOrthogonalHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mXAxisAbscissa = ParserSTEP.StripDouble(str, ref pos, len);
			mXAxisOrdinate = ParserSTEP.StripDouble(str, ref pos, len);
			mScale = ParserSTEP.StripDouble(str, ref pos, len);
			if (release >= ReleaseVersion.IFC4X3_RC3 && release < ReleaseVersion.IFC4X3_ADD2)
			{
				mScaleY = ParserSTEP.StripDouble(str, ref pos, len);
				mScaleZ = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
	public partial class IfcMapConversionScaled
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4X3_ADD2)
				return "";
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mFactorX) + "," + ParserSTEP.DoubleOptionalToString(mFactorY) + "," + ParserSTEP.DoubleOptionalToString(mFactorZ);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mFactorX = ParserSTEP.StripDouble(str, ref pos, len);
			mFactorY = ParserSTEP.StripDouble(str, ref pos, len);
			mFactorZ = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcMappedItem
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mMappingSource.StepId + ",#" + mMappingTarget.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			MappingSource = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcRepresentationMap;
			MappingTarget = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianTransformationOperator;
		}
	}
	public partial class IfcMarineFacility
	{
		public override string StepClassName
		{
			get
			{
				if (mDatabase != null)
				{
					if (mDatabase.Release < ReleaseVersion.IFC4X2)
						return "IfcBuilding";
					if (mDatabase.Release < ReleaseVersion.IFC4X3_RC1)
						return "IfcFacility";
				}
				return base.StepClassName;
			}
		}
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4X3_RC1 ? ",$,$,$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMarineFacilityTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcMarinePart
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4X3 && release >= ReleaseVersion.IFC4X3_RC1)
				return base.BuildStringSTEP(release);
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcMarinePartTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4X3_RC1 || release >= ReleaseVersion.IFC4X3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
				{
					if (Enum.TryParse<IfcMarinePartTypeEnum>(s.Replace(".", ""), true, out IfcMarinePartTypeEnum partType))
						PredefinedType = partType;
				}
			}
		}
	}
	public partial class IfcMaterial
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return "'" + ParserSTEP.Encode(mName) + (release > ReleaseVersion.IFC2x3 ? (string.IsNullOrEmpty(mDescription) ? "',$," : "','" + ParserSTEP.Encode(mDescription) + "',") + 
				(string.IsNullOrEmpty(mCategory) ? "$" : "'" + ParserSTEP.Encode(mCategory) + "'") : "'" ); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			if (release > ReleaseVersion.IFC2x3)
			{
				try
				{
					mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
					mCategory = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				}
				catch (Exception) { }
			}
		}
	}
	public partial class IfcMaterialClassificationRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mMaterialClassifications.Select(x=>"#" + x.StepId)) + "),#" + mClassifiedMaterial.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mMaterialClassifications.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcClassificationNotationSelect));
			mClassifiedMaterial = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterial;
		}
	}
	public partial class IfcMaterialConstituent
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$,#" : "'" + ParserSTEP.Encode(mDescription) + "',#") + mMaterial.StepId + "," + 
				ParserSTEP.DoubleToString(mFraction) + (string.IsNullOrEmpty(mCategory) ? ",$" : ",'" + ParserSTEP.Encode(mCategory) + "'"); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mMaterial = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterial;
			mFraction = ParserSTEP.StripDouble(str, ref pos, len);
			mCategory = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcMaterialConstituentSet
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") + (string.IsNullOrEmpty(mDescription) ? "$,(#" : "'" + ParserSTEP.Encode(mDescription) + "',(#") + string.Join(",#", mMaterialConstituents.Values.Select(x=>x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			foreach (IfcMaterialConstituent constituent in ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcMaterialConstituent))
				mMaterialConstituents[constituent.Name] = constituent;
		}
	}
	public partial class IfcMaterialDefinitionRepresentation
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mRepresentedMaterial.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RepresentedMaterial = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterial;
		}
	}
	public partial class IfcMaterialLayer
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (mMaterial == null ? "$," : "#" + mMaterial.StepId + ",") + ParserSTEP.DoubleToString(mLayerThickness) + "," + 
				ParserIfc.LogicalToString(mIsVentilated) + (release < ReleaseVersion.IFC4 ? "" : 
				(string.IsNullOrEmpty(mName) ? ",$," : ",'" + ParserSTEP.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$," : "'" + ParserSTEP.Encode(mDescription) + "',") + 
				(string.IsNullOrEmpty(mCategory) ? "$," : "'" + ParserSTEP.Encode(mCategory) + "',") + 
				(mPriority == int.MinValue ? "$" : (release < ReleaseVersion.IFC4A1 ? ParserSTEP.DoubleToString(mPriority/100.0) : mPriority.ToString())));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mMaterial = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterial;
			mLayerThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mIsVentilated = ParserIfc.StripLogical(str, ref pos, len);
			try
			{
				if (release != ReleaseVersion.IFC2x3)
				{
					mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
					mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
					mCategory = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
					string s = ParserSTEP.StripField(str, ref pos, len);
					if (s.Contains("."))
					{
						if (double.TryParse(s, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out double d)) //Was normalizedRatioMeasure
							Priority = (int)(d * 100);
					}
					else if (s != "$")
						mPriority = ParserSTEP.ParseInt(s);
				}
			}
			catch (Exception) { }
		}
	}
	public partial class IfcMaterialLayerSet
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mMaterialLayers.Select(x=>"#" +x.StepId)) +
				(string.IsNullOrEmpty(mLayerSetName) ? "),$" : "),'" + ParserSTEP.Encode(mLayerSetName) + "'") + 
				(release < ReleaseVersion.IFC4 ? "" : (string.IsNullOrEmpty(mDescription) ? ",$" : ",'" + ParserSTEP.Encode(mDescription) + "'"));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			MaterialLayers.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcMaterialLayer));
			mLayerSetName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			if (release != ReleaseVersion.IFC2x3)
				mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcMaterialLayerSetUsage
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mForLayerSet.StepId + ",." + mLayerSetDirection.ToString() + ".,." + mDirectionSense.ToString() + ".," + ParserSTEP.DoubleToString(mOffsetFromReferenceLine) + (release < ReleaseVersion.IFC4 ? "" : "," + ParserSTEP.DoubleOptionalToString(mReferenceExtent)); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mForLayerSet = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterialLayerSet;
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
	public partial class IfcMaterialLayerSetWithOffsets
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
	public partial class IfcMaterialLayerWithOffsets
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
	public partial class IfcMaterialList
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + String.Join(",", mMaterials.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) 
		{
			mMaterials.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcMaterial)); 
		}
	}
	public partial class IfcMaterialProfile
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return (release < ReleaseVersion.IFC4 ? "" : (string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$," : "'" + ParserSTEP.Encode(mDescription) + "',") +
				ParserSTEP.ObjToLinkString(mMaterial) + "," + ParserSTEP.ObjToLinkString(mProfile) +
				(mPriority == int.MinValue ? ",$" : (release < ReleaseVersion.IFC4A1 ? ParserSTEP.DoubleToString(mPriority / 100.0) : mPriority.ToString())) +
				(string.IsNullOrEmpty(mCategory) ? ",$" : ",'" + ParserSTEP.Encode(mCategory) + "'")); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			Material = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterial;
			Profile = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcProfileDef;
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.Contains("."))
			{
				if (double.TryParse(s, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out double d)) //Was normalizedRatioMeasure
					Priority = (int)(d * 100);
			}
			else if(s != "$")
				mPriority = ParserSTEP.ParseInt(s);

			mCategory = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcMaterialProfileSet
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4 || mMaterialProfiles.Count == 0)
				return "";
			return (string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") + 
				(string.IsNullOrEmpty(mDescription) ? "$,(" : "'" + ParserSTEP.Encode(mDescription) + "',(") +
				string.Join(",", mMaterialProfiles.Select(x=>"#" + x.StepId)) + ")," + ParserSTEP.ObjToLinkString(mCompositeProfile);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			MaterialProfiles.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcMaterialProfile));
			CompositeProfile = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCompositeProfileDef;
		}
	}
	public partial class IfcMaterialProfileSetUsage
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			if (release < ReleaseVersion.IFC4 || AssociatedTo.SelectMany(x => x.mRelatedObjects).Count() == 0)
				return "";
			return "#" + mForProfileSet.StepId + "," + (mCardinalPoint == IfcCardinalPointReference.DEFAULT ? "$" : ((int)mCardinalPoint).ToString()) + "," + ParserSTEP.DoubleOptionalToString(mReferenceExtent);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			ForProfileSet = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterialProfileSet;
			int i = ParserSTEP.StripInt(str, ref pos, len);
			if(i > 0)
				CardinalPoint = (IfcCardinalPointReference)i;
			ReferenceExtent = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcMaterialProfileSetUsageTapering
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4 || AssociatedTo.SelectMany(x => x.mRelatedObjects).Count() == 0)
				return "";
			return base.BuildStringSTEP(release) + ",#" + ForProfileEndSet.StepId + "," + (mCardinalEndPoint == IfcCardinalPointReference.DEFAULT ? "$" : ((int)mCardinalEndPoint).ToString());
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			ForProfileEndSet = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterialProfileSet;
			int i = ParserSTEP.StripInt(str, ref pos, len);
			if (i > 0)
				CardinalEndPoint = (IfcCardinalPointReference)i;
		}
	}
	public partial class IfcMaterialProfileWithOffsets
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
	public partial class IfcMaterialProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (release >= ReleaseVersion.IFC4  ? base.BuildStringSTEP(release) + "," : "" ) + "#" + mMaterial.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Material = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterialDefinition;
		}
	}
	public partial class IfcMaterialRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return "";
			return base.BuildStringSTEP(release) + ",#" + mRelatingMaterial.StepId + ",(" + 
				string.Join(",", mRelatedMaterials.Select(x=>"#" + x.StepId)) +
				(string.IsNullOrEmpty(mMaterialExpression) ? "),$" : "),'" + ParserSTEP.Encode(mMaterialExpression) + "'");

		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RelatingMaterial = dictionary[ ParserSTEP.StripLink(str, ref pos, len)] as IfcMaterial;
			RelatedMaterials.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>Database[x] as IfcMaterial));
			mMaterialExpression = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcMeasureWithUnit
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (mValueComponent != null ? mValueComponent.ToString() : mVal) + ",#" + mUnitComponent.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			mValueComponent = ParserIfc.parseValue(s);
			if (mValueComponent == null)
				mVal = s;
			mUnitComponent = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcUnit;
		}
	}
	public partial class IfcMechanicalConcreteMaterialProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{ 
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mCompressiveStrength) + "," + 
				ParserSTEP.DoubleOptionalToString(mMaxAggregateSize) + 
				(string.IsNullOrEmpty(mAdmixturesDescription) ? ",$," : ",'" + ParserSTEP.Encode(mAdmixturesDescription) + "',") + 
				(string.IsNullOrEmpty(mWorkability) ? "$," : "'" + mWorkability + "',") + ParserSTEP.DoubleOptionalToString(mProtectivePoreRatio) +
				(string.IsNullOrEmpty(mWaterImpermeability) ? ",$" : ",'" + ParserSTEP.Encode(mWaterImpermeability) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mCompressiveStrength = ParserSTEP.StripDouble(str, ref pos, len);
			mMaxAggregateSize = ParserSTEP.StripDouble(str, ref pos, len);
			mAdmixturesDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mWorkability = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mProtectivePoreRatio = ParserSTEP.StripDouble(str, ref pos, len);
			mWaterImpermeability = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcMechanicalFastener
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
	public partial class IfcMechanicalFastenerType
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
	public partial class IfcMechanicalMaterialProperties
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
	public partial class IfcMechanicalSteelMaterialProperties
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mYieldStress) + "," +
				ParserSTEP.DoubleOptionalToString(mUltimateStress) + "," + ParserSTEP.DoubleOptionalToString(mUltimateStrain) + "," +
				ParserSTEP.DoubleOptionalToString(mHardeningModule) + "," + ParserSTEP.DoubleOptionalToString(mProportionalStress) + "," +
				ParserSTEP.DoubleOptionalToString(mPlasticStrain) +
				(mRelaxations.Count == 0 ? ",$" : ",(" + String.Join(",", mRelaxations.Select(x => "#" + x.StepId)) + ")");
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
			mRelaxations.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcRelaxation));
		}
	}
	public partial class IfcMedicalDevice
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
	public partial class IfcMedicalDeviceType
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
	public partial class IfcMember
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
	public partial class IfcMemberType
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
	public partial class IfcMetric
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return base.BuildStringSTEP(release) + ",." + mBenchMark.ToString() +
				(string.IsNullOrEmpty(mValueSource) ? ".,$," : ".,'" + ParserSTEP.Encode(mValueSource) + "',") +
				(mDataValue == null ? "$" : (mDataValue is BaseClassIfc o ? "#" + o.StepId : mDataValue.ToString())) +
				(release < ReleaseVersion.IFC4 ? "" : (mReferencePath == null ? ",$" : ",#" + mReferencePath.StepId)); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcBenchmarkEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mBenchMark);
			mValueSource = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			string s = ParserSTEP.StripField(str, ref pos, len);
			if(s[0] == '#')
				mDataValue = dictionary[ParserSTEP.ParseLink(s)] as IfcMetricValueSelect;
			else
				mDataValue = ParserIfc.parseValue(s) as IfcMetricValueSelect;
			mReferencePath = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcReference;
		}
	}
	public partial class IfcMobileTelecommunicationsAppliance
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcMobileTelecommunicationsApplianceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMobileTelecommunicationsApplianceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcMobileTelecommunicationsApplianceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMobileTelecommunicationsApplianceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcModulusOfRotationalSubgradeReactionSelect
	{
		internal static IfcModulusOfRotationalSubgradeReactionSelect Parse(string str, ReleaseVersion version)
		{
			if (str == "$")
				return null;
			IfcModulusOfRotationalSubgradeReactionSelect stiffness = new IfcModulusOfRotationalSubgradeReactionSelect();
			stiffness.ParseValue(str, version);
			return stiffness;
		}
	}
	public partial class IfcModulusOfSubgradeReactionSelect
	{
		internal static IfcModulusOfSubgradeReactionSelect Parse(string str, ReleaseVersion version)
		{
			if (str == "$")
				return null;
			IfcModulusOfSubgradeReactionSelect stiffness = new IfcModulusOfSubgradeReactionSelect();
			stiffness.ParseValue(str, version);
			return stiffness;
		}
	}
	public partial class IfcModulusOfTranslationalSubgradeReactionSelect
	{
		internal static IfcModulusOfTranslationalSubgradeReactionSelect Parse(string str, ReleaseVersion version)
		{
			if (str == "$")
				return null;
			IfcModulusOfTranslationalSubgradeReactionSelect stiffness = new IfcModulusOfTranslationalSubgradeReactionSelect();
			stiffness.ParseValue(str, version);
			return stiffness;
		}
	}
	public partial class IfcMonetaryUnit
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "." + mCurrency + "." : "'" + ParserSTEP.Encode(mCurrency) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mCurrency = ParserSTEP.Decode( ParserSTEP.StripField(str, ref pos, len).Replace(release < ReleaseVersion.IFC4 ? "." : "'", ""));
		}
	}
	public partial class IfcMooringDevice
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcMooringDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMooringDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcMooringDeviceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcMooringDeviceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcMotorConnection
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
	public partial class IfcMotorConnectionType
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
}
