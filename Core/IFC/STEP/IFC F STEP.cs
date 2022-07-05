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
using System.Xml.Serialization;

namespace GeometryGym.Ifc
{
	public partial class IfcFace
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (mBounds.Count == 0 ? "()" : "(#" + string.Join(",#", Bounds.ConvertAll(x => x.mIndex)) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Bounds.AddRange(ParserSTEP.StripListLink(str, ref pos, str.Length).ConvertAll(x => dictionary[x] as IfcFaceBound));
		}
	}
	public partial class IfcFaceBasedSurfaceModel
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mFbsmFaces.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mFbsmFaces.AddRange(ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)).Select(x => dictionary[x] as IfcConnectedFaceSet));
		}
	}
	public partial class IfcFaceBound
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mBound.StepId + "," + ParserSTEP.BoolToString(mOrientation); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Bound = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcLoop;
			mOrientation = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcFaceSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mFaceSurface.StepId + "," + ParserSTEP.BoolToString(mSameSense); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			FaceSurface = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcSurface;
			mSameSense = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcFacetedBrepWithVoids
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = base.BuildStringSTEP(release) + ",(";
			if (mVoids.Count > 0)
			{
				str += ParserSTEP.LinkToString(mVoids[0]);
				for (int icounter = 1; icounter < mVoids.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mVoids[icounter]);
			}
			return str + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mVoids = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcFacilityPart
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if(release >= ReleaseVersion.IFC4X3_RC1 && release < ReleaseVersion.IFC4X3)
			{
				string predefined = "IFCFACILITYPARTTYPEENUM(.NOTDEFINED.),.";
				if(this is IfcFacilityPartCommon facilityPartCommon)
				if (this is IfcBridgePart bridgePart)
					predefined = "IFCBRIDGEPARTTYPEENUM(." + bridgePart.PredefinedType.ToString() + ".),.";

				return base.BuildStringSTEP(release) + "," + predefined + mUsageType.ToString() + ".";
			}
			if (release > ReleaseVersion.IFC4X2)
			{
				return base.BuildStringSTEP(release) + ",." + mUsageType.ToString() + ".";
			}
			return base.BuildStringSTEP(release);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release > ReleaseVersion.IFC4X2 && release < ReleaseVersion.IFC4X3)
			{
				string s = "";
				if (release > ReleaseVersion.IFC4X2 && release < ReleaseVersion.IFC4X3)
				{
					s = ParserSTEP.StripField(str, ref pos, len);
					int index = s.IndexOf('.');
					if(index > 0)
					{
						s = s.Substring(index + 1, s.Length - index - 3);
						if (this is IfcFacilityPartCommon facilityPartCommon && Enum.TryParse<IfcFacilityPartCommonTypeEnum>(s, true, out IfcFacilityPartCommonTypeEnum facilityPartCommonTypeEnum))
							facilityPartCommon.PredefinedType = facilityPartCommonTypeEnum;
						else if (this is IfcBridgePart bridgePart && Enum.TryParse<IfcBridgePartTypeEnum>(s, true, out IfcBridgePartTypeEnum bridgePartTypeEnum))
							bridgePart.PredefinedType = bridgePartTypeEnum;
						//else if (this is IfcRoadPart roadPart && Enum.TryParse<IfcBridgePartTypeEnum>(s, true, out IfcBridgePartTypeEnum bridgePartTypeEnum))
						//	bridgePart.PredefinedType = bridgePartTypeEnum;
					}
				}
				s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcFacilityUsageEnum>(s.Replace(".", ""), true, out mUsageType);
			}
		}
	}
	public partial class IfcFacilityPartCommon
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4X3 && release >= ReleaseVersion.IFC4X3_RC1)
				return base.BuildStringSTEP(release);
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcFacilityPartCommonTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4X3_RC1 || release >= ReleaseVersion.IFC4X3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
				{
					if (Enum.TryParse<IfcFacilityPartCommonTypeEnum>(s.Replace(".", ""), true, out IfcFacilityPartCommonTypeEnum partType))
						PredefinedType = partType;
				}
			}
		}
	}
	public partial class IfcFailureConnectionCondition
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," +
			ParserSTEP.DoubleOptionalToString(mTensionFailureX) + "," +
			ParserSTEP.DoubleOptionalToString(mTensionFailureY) + "," +
			ParserSTEP.DoubleOptionalToString(mTensionFailureZ) + "," +
			ParserSTEP.DoubleOptionalToString(mCompressionFailureX) + "," +
			ParserSTEP.DoubleOptionalToString(mCompressionFailureY) + "," +
			ParserSTEP.DoubleOptionalToString(mCompressionFailureZ);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			TensionFailureX = ParserSTEP.StripDouble(str, ref pos, len);
			TensionFailureY = ParserSTEP.StripDouble(str, ref pos, len);
			TensionFailureZ = ParserSTEP.StripDouble(str, ref pos, len);
			CompressionFailureX = ParserSTEP.StripDouble(str, ref pos, len);
			CompressionFailureY = ParserSTEP.StripDouble(str, ref pos, len);
			CompressionFailureZ = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcFan
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcFanTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFanTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFanType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFanTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFastener
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcFastenerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFastenerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFastenerType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : ",." + mPredefinedType + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFastenerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFillAreaStyleHatching
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return ParserSTEP.LinkToString(mHatchLineAppearance) + "," + mStartOfNextHatchLine + "," + ParserSTEP.LinkToString(mPointOfReferenceHatchLine) + "," + ParserSTEP.LinkToString(mPatternStart) + "," + ParserSTEP.DoubleToString(mHatchLineAngle); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mHatchLineAppearance = ParserSTEP.StripLink(str, ref pos, len);
			mStartOfNextHatchLine = ParserSTEP.StripField(str, ref pos, len);
			mPointOfReferenceHatchLine = ParserSTEP.StripLink(str, ref pos, len);
			mPatternStart = ParserSTEP.StripLink(str, ref pos, len);
			mHatchLineAngle = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcFillAreaStyleTiles
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(#" + string.Join(",#", mTilingPattern.ConvertAll(x => x.StepId.ToString())) + ")" +
			",(#" + string.Join(",#", mTiles.ConvertAll(x => x.StepId.ToString())) + ")" + "," +
			ParserSTEP.DoubleOptionalToString(mTilingScale);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			TilingPattern.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcVector));
			Tiles.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcStyledItem));
			TilingScale = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcFilter  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcFilterTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFilterTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFilterType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFilterTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFillAreaStyle
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(#" + string.Join(",#", mFillStyles.ConvertAll(x=>x.Index)) + ")" + (release > ReleaseVersion.IFC2x3 ? "," + ParserSTEP.BoolToString(mModelorDraughting) : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			FillStyles.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=> dictionary[x] as IfcFillStyleSelect));
		}
	}
	public partial class IfcFireSuppressionTerminal
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcFireSuppressionTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFireSuppressionTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFireSuppressionTerminalType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFireSuppressionTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFixedReferenceSweptAreaSolid
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.LinkToString(mFixedReference); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mFixedReference = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcFlowInstrument  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcFlowInstrumentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFlowInstrumentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFlowInstrumentType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFlowInstrumentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFlowMeter
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcFlowMeterTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFlowMeterTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFlowMeterType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFlowMeterTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFluidFlowProperties 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",." + mPropertySource.ToString() + ".," + ParserSTEP.LinkToString(mFlowConditionTimeSeries) + "," +
				ParserSTEP.LinkToString(mVelocityTimeSeries) + "," + ParserSTEP.LinkToString(mFlowrateTimeSeries) + "," + 
				ParserSTEP.LinkToString(mFluid) + "," + ParserSTEP.LinkToString(mPressureTimeSeries) + 
				(string.IsNullOrEmpty(mUserDefinedPropertySource) ? ",$," : ",'" + ParserIfc.Encode(mUserDefinedPropertySource) + "',") + 
				ParserSTEP.DoubleOptionalToString(mTemperatureSingleValue) + "," + ParserSTEP.DoubleOptionalToString(mWetBulbTemperatureSingleValue) + "," + 
				ParserSTEP.LinkToString(mWetBulbTemperatureTimeSeries) + "," + ParserSTEP.LinkToString(mTemperatureTimeSeries) + "," + 
				ParserSTEP.DoubleOptionalToString(mFlowrateSingleValue) + "," + ParserSTEP.DoubleOptionalToString(mFlowConditionSingleValue) + "," + 
				ParserSTEP.DoubleOptionalToString(mVelocitySingleValue) + "," + ParserSTEP.DoubleOptionalToString(mPressureSingleValue);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			Enum.TryParse<IfcPropertySourceEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mPropertySource);
			mFlowConditionTimeSeries = ParserSTEP.StripLink(str, ref pos, len);
			mVelocityTimeSeries = ParserSTEP.StripLink(str, ref pos, len);
			mFlowrateTimeSeries = ParserSTEP.StripLink(str, ref pos, len);
			mFluid = ParserSTEP.StripLink(str, ref pos, len);
			mPressureTimeSeries = ParserSTEP.StripLink(str, ref pos, len);
			mUserDefinedPropertySource = ParserIfc.Decode(ParserSTEP.StripString(str, ref pos, len));
			mTemperatureSingleValue = ParserSTEP.StripLink(str, ref pos, len);
			mWetBulbTemperatureSingleValue = ParserSTEP.StripLink(str, ref pos, len);
			mWetBulbTemperatureTimeSeries = ParserSTEP.StripLink(str, ref pos, len);
			mTemperatureTimeSeries = ParserSTEP.StripLink(str, ref pos, len);
			mFlowrateSingleValue = ParserSTEP.StripLink(str, ref pos, len);
			mFlowConditionSingleValue = ParserSTEP.StripLink(str, ref pos, len);
			mVelocitySingleValue = ParserSTEP.StripLink(str, ref pos, len);
			mPressureSingleValue = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcFooting
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFootingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFootingType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFootingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFurniture
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : ",." + mPredefinedType + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFurnitureTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcFurnitureType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mAssemblyPlace.ToString() + (release < ReleaseVersion.IFC4 ? "." : ".,." + mPredefinedType + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcFurnitureTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
}
