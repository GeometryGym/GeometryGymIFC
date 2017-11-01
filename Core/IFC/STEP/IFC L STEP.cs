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
	public partial class IfcLaborResource : IfcConstructionResource
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcLaborResourceTypeEnum>(s.Replace(".", ""), out mPredefinedType);
			}
		}
	}
	public partial class IfcLaborResourceType : IfcConstructionResourceType //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcLaborResourceTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcLagTime : IfcSchedulingTime //IFC4
	{
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + "," + mLagValue.ToString() + ",." + mDurationType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				mLagValue = ParserIfc.parseValue(s) as IfcTimeOrRatioSelect;
				s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcTaskDurationEnum>(s.Replace(".", ""), out mDurationType);
			}
		}
	}
	public partial class IfcLamp : IfcFlowTerminal //IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcLampTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcLampTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcLampType : IfcFlowTerminalType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcLampTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcLibraryInformation : IfcExternalInformation
	{
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + ",'" + mName + (mVersion == "$" ? "',$," : "','" + mVersion + "',") + ParserSTEP.LinkToString(mPublisher);
			if (mDatabase.Release == ReleaseVersion.IFC2x3)
			{
				string refs =  mHasLibraryReferences.Count > 0 ? "#" + mHasLibraryReferences[0].mIndex : "";
				for (int icounter = 1; icounter < mHasLibraryReferences.Count; icounter++)
					refs += ",#" + mHasLibraryReferences[icounter].mIndex;
				return result + ",$,(" + refs + ")"; //TODO date
			}
			return result + (mVersionDate == "$" ? ",$," : ",'" + mVersionDate + "',") + (mLocation == "$" ? "$," : "'" + mLocation + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mVersion = ParserSTEP.StripString(str, ref pos, len);
			mPublisher = ParserSTEP.StripLink(str, ref pos, len);
			if (release == ReleaseVersion.IFC2x3)
			{
				mVersionDateSS = ParserSTEP.StripLink(str, ref pos, len);
				mLibraryReference = ParserSTEP.StripListLink(str, ref pos, len);
			}
			else
			{
				mVersionDate = ParserSTEP.StripString(str, ref pos, len);
				mLocation = ParserSTEP.StripString(str, ref pos, len);
				mDescription = ParserSTEP.StripString(str, ref pos, len);
			}
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mDatabase.Release == ReleaseVersion.IFC2x3)
			{
				foreach (int i in mLibraryReference)
					(mDatabase[i] as IfcLibraryReference).ReferencedLibrary = this;
			}
		}
	}
	public partial class IfcLibraryReference : IfcExternalReference, IfcLibrarySelect
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : ((mDescription == "$" ? ",$," : ",'" + mDescription + "',") + (mLanguage == "$" ? "$," : "'" + mLanguage + "',") + ParserSTEP.LinkToString(mReferencedLibrary))); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			if (release != ReleaseVersion.IFC2x3)
			{
				mDescription = ParserSTEP.StripString(str, ref pos, len);
				mLanguage = ParserSTEP.StripString(str, ref pos, len);
				mReferencedLibrary = ParserSTEP.StripLink(str, ref pos, len);
			}
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mDatabase.Release != ReleaseVersion.IFC2x3 && mReferencedLibrary > 0)
				ReferencedLibrary.mHasLibraryReferences.Add(this);
		}
	}
	//ENTITY IfcLightDistributionData;
	public partial class IfcLightFixture : IfcFlowTerminal
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcLightFixtureTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcLightFixtureTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	public partial class IfcLightFixtureType : IfcFlowTerminalType
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcLightFixtureTypeEnum>(s.Replace(".", ""), out mPredefinedType);
		}
	}
	//ENTITY IfcLightIntensityDistribution ,IfcLightDistributionDataSourceSelect
	public abstract partial class IfcLightSource : IfcGeometricRepresentationItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcLightSourceAmbient ,IfcLightSourceDirectional ,IfcLightSourceGoniometric ,IfcLightSourcePositional))
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + ParserSTEP.LinkToString(mLightColour) + "," + ParserSTEP.DoubleOptionalToString(mAmbientIntensity) + "," + ParserSTEP.DoubleOptionalToString(mIntensity); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mName = ParserSTEP.StripString(str, ref pos, len);
			mLightColour = ParserSTEP.StripLink(str, ref pos, len);
			mAmbientIntensity = ParserSTEP.StripDouble(str, ref pos, len);
			mIntensity = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcLightSourceDirectional : IfcLightSource
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mOrientation); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mOrientation = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcLightSourceGoniometric : IfcLightSource
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPosition) + "," + ParserSTEP.LinkToString(mColourAppearance) + "," +
				ParserSTEP.DoubleToString(mColourTemperature) + "," + ParserSTEP.DoubleToString(mLuminousFlux) + ",." +
				mLightEmissionSource.ToString() + ".," + ParserSTEP.LinkToString(mLightDistributionDataSource);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mPosition = ParserSTEP.StripLink(str, ref pos, len);
			mColourAppearance = ParserSTEP.StripLink(str, ref pos, len);
			mColourTemperature = ParserSTEP.StripDouble(str, ref pos, len);
			mLuminousFlux = ParserSTEP.StripDouble(str, ref pos, len);
			mLightEmissionSource = (IfcLightEmissionSourceEnum)Enum.Parse(typeof(IfcLightEmissionSourceEnum), ParserSTEP.StripField(str, ref pos, len).Replace(".", ""));
			mLightDistributionDataSource = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcLightSourcePositional : IfcLightSource
	{
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPosition) + "," + ParserSTEP.DoubleToString(mRadius) + "," +
				ParserSTEP.DoubleToString(mConstantAttenuation) + "," + ParserSTEP.DoubleToString(mDistanceAttenuation) + "," +
				ParserSTEP.DoubleToString(mQuadricAttenuation);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mPosition = ParserSTEP.StripLink(str, ref pos, len);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mConstantAttenuation = ParserSTEP.StripDouble(str, ref pos, len);
			mDistanceAttenuation = ParserSTEP.StripDouble(str, ref pos, len);
			mQuadricAttenuation = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcLightSourceSpot : IfcLightSource
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mOrientation) + "," + ParserSTEP.DoubleToString(mConcentrationExponent) + "," + ParserSTEP.DoubleToString(mSpreadAngle) + "," + ParserSTEP.DoubleToString(mBeamWidthAngle); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mOrientation = ParserSTEP.StripLink(str, ref pos, len);
			mConcentrationExponent = ParserSTEP.StripDouble(str, ref pos, len);
			mSpreadAngle = ParserSTEP.StripDouble(str, ref pos, len);
			mBeamWidthAngle = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcLine : IfcCurve
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPnt) + "," + ParserSTEP.LinkToString(mDir); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mPnt = ParserSTEP.StripLink(str, ref pos, len);
			mDir = ParserSTEP.StripLink(str, ref pos, len);
		}
	}
	public partial class IfcLocalPlacement : IfcObjectPlacement
	{
		protected override string BuildStringSTEP()
		{
			if (mPlacesObject.Count == 0 && mReferencedByPlacements.Count == 0)
				return "";
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPlacementRelTo) + "," + ParserSTEP.LinkToString(mRelativePlacement == 0 ? mDatabase.Factory.XYPlanePlacement.mIndex : mRelativePlacement);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mPlacementRelTo = ParserSTEP.StripLink(str, ref pos, len);
			mRelativePlacement = ParserSTEP.StripLink(str, ref pos, len);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mPlacementRelTo > 0)
				PlacementRelTo.mReferencedByPlacements.Add(this);
		}
	}
	public partial class IfcLocalTime : BaseClassIfc, IfcDateTimeSelect // DEPRECEATED IFC4
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mHourComponent + "," + mMinuteComponent + "," + ParserSTEP.DoubleToString(mSecondComponent) + "," + ParserSTEP.LinkToString(mZone) + "," + mDaylightSavingOffset; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			mHourComponent = ParserSTEP.StripInt(str, ref pos, len);
			mMinuteComponent = ParserSTEP.StripInt(str, ref pos, len);
			mSecondComponent = ParserSTEP.StripDouble(str, ref pos, len);
			mZone = ParserSTEP.StripLink(str, ref pos, len);
			mDaylightSavingOffset = ParserSTEP.StripInt(str, ref pos, len);
		}
	}
	public partial class IfcLShapeProfileDef : IfcParameterizedProfileDef
	{
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mDepth) + "," + ParserSTEP.DoubleToString(mWidth) + "," + ParserSTEP.DoubleToString(mThickness) + "," + ParserSTEP.DoubleOptionalToString(mFilletRadius) + "," + ParserSTEP.DoubleOptionalToString(mEdgeRadius) + "," + ParserSTEP.DoubleOptionalToString(mLegSlope) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInX) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY) : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len)
		{
			base.parse(str, ref pos, release, len);
			mDepth = ParserSTEP.StripDouble(str, ref pos, len);
			mWidth = ParserSTEP.StripDouble(str, ref pos, len);
			mThickness = ParserSTEP.StripDouble(str, ref pos, len);
			mFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mEdgeRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mLegSlope = ParserSTEP.StripDouble(str, ref pos, len);
			if (release == ReleaseVersion.IFC2x3)
			{
				mCentreOfGravityInX = ParserSTEP.StripDouble(str, ref pos, len);
				mCentreOfGravityInY = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
}
