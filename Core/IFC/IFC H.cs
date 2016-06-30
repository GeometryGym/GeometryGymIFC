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
using System.Drawing;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class IfcHalfSpaceSolid : IfcGeometricRepresentationItem, IfcBooleanOperand /* SUPERTYPE OF (ONEOF (IfcBoxedHalfSpace ,IfcPolygonalBoundedHalfSpace)) */
	{
		private int mBaseSurface;// : IfcSurface;
		private bool mAgreementFlag;// : BOOLEAN;

		internal IfcSurface BaseSurface { get { return mDatabase[mBaseSurface] as IfcSurface; } set { mBaseSurface = value.mIndex; } }
		internal bool AgreementFlag { get { return mAgreementFlag; } }

		internal IfcHalfSpaceSolid() : base() { }
		internal IfcHalfSpaceSolid(DatabaseIfc db, IfcHalfSpaceSolid h) : base(db,h) { BaseSurface = db.Duplicate(h.BaseSurface) as IfcSurface; mAgreementFlag = h.mAgreementFlag; }

		internal static void parseFields(IfcHalfSpaceSolid s, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(s, arrFields, ref ipos); s.mBaseSurface = ParserSTEP.ParseLink(arrFields[ipos++]); s.mAgreementFlag = ParserSTEP.ParseBool(arrFields[ipos++]); }
		internal static IfcHalfSpaceSolid Parse(string strDef) { IfcHalfSpaceSolid s = new IfcHalfSpaceSolid(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBaseSurface) + "," + ParserSTEP.BoolToString(mAgreementFlag); }
	}
	public partial class IfcHeatExchanger : IfcEnergyConversionDevice //IFC4
	{
		internal IfcHeatExchangerTypeEnum mPredefinedType = IfcHeatExchangerTypeEnum.NOTDEFINED;// OPTIONAL : IfcHeatExchangerTypeEnum;
		public IfcHeatExchangerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcHeatExchanger() : base() { }
		internal IfcHeatExchanger(DatabaseIfc db, IfcHeatExchanger e) : base(db, e) { mPredefinedType = e.mPredefinedType; }
		internal IfcHeatExchanger(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcHeatExchanger s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcHeatExchangerTypeEnum)Enum.Parse(typeof(IfcHeatExchangerTypeEnum), str);
		}
		internal new static IfcHeatExchanger Parse(string strDef) { IfcHeatExchanger s = new IfcHeatExchanger(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcHeatExchangerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcHeatExchangerType : IfcEnergyConversionDeviceType
	{
		internal IfcHeatExchangerTypeEnum mPredefinedType = IfcHeatExchangerTypeEnum.NOTDEFINED;// : IfcHeatExchangerTypeEnum;
		public IfcHeatExchangerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcHeatExchangerType() : base() { }
		internal IfcHeatExchangerType(DatabaseIfc db, IfcHeatExchangerType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcHeatExchangerType(DatabaseIfc m, string name, IfcHeatExchangerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcHeatExchangerType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcHeatExchangerTypeEnum)Enum.Parse(typeof(IfcHeatExchangerTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcHeatExchangerType Parse(string strDef) { IfcHeatExchangerType t = new IfcHeatExchangerType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcHumidifier : IfcEnergyConversionDevice //IFC4
	{
		internal IfcHumidifierTypeEnum mPredefinedType = IfcHumidifierTypeEnum.NOTDEFINED;// OPTIONAL : IfcHumidifierTypeEnum;
		public IfcHumidifierTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcHumidifier() : base() { }
		internal IfcHumidifier(DatabaseIfc db, IfcHumidifier h) : base(db,h) { mPredefinedType = h.mPredefinedType; }
		internal IfcHumidifier(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcHumidifier s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcHumidifierTypeEnum)Enum.Parse(typeof(IfcHumidifierTypeEnum), str);
		}
		internal new static IfcHumidifier Parse(string strDef) { IfcHumidifier s = new IfcHumidifier(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcHumidifierTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcHumidifierType : IfcEnergyConversionDeviceType
	{
		internal IfcHumidifierTypeEnum mPredefinedType = IfcHumidifierTypeEnum.NOTDEFINED;// : IfcHumidifierExchangerEnum;
		public IfcHumidifierTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcHumidifierType() : base() { }
		internal IfcHumidifierType(DatabaseIfc db, IfcHumidifierType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcHumidifierType(DatabaseIfc m, string name, IfcHumidifierTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcHumidifierType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcHumidifierTypeEnum)Enum.Parse(typeof(IfcHumidifierTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcHumidifierType Parse(string strDef) { IfcHumidifierType t = new IfcHumidifierType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcHygroscopicMaterialProperties : IfcMaterialPropertiesSuperSeded // DEPRECEATED IFC4
	{
		internal double mUpperVaporResistanceFactor = double.NaN, mLowerVaporResistanceFactor = double.NaN; //: OPTIONAL IfcPositiveRatioMeasure;
		internal double mIsothermalMoistureCapacity = double.NaN; //: : OPTIONAL IfcIsothermalMoistureCapacityMeasure;
		internal double mVaporPermeability = double.NaN;//: OPTIONAL IfcVaporPermeabilityMeasure;
		internal double mMoistureDiffusivity = double.NaN;// : OPTIONAL IfcMoistureDiffusivityMeasure;*/
		internal IfcHygroscopicMaterialProperties() : base() { }
		internal IfcHygroscopicMaterialProperties(DatabaseIfc db, IfcHygroscopicMaterialProperties p) : base(db,p)
		{
			mUpperVaporResistanceFactor = p.mUpperVaporResistanceFactor;
			mLowerVaporResistanceFactor = p.mLowerVaporResistanceFactor;
			mIsothermalMoistureCapacity = p.mIsothermalMoistureCapacity;
			mVaporPermeability = p.mVaporPermeability;
			mMoistureDiffusivity = p.mMoistureDiffusivity;
		}
		internal static IfcHygroscopicMaterialProperties Parse(string strDef) { IfcHygroscopicMaterialProperties p = new IfcHygroscopicMaterialProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcHygroscopicMaterialProperties p, List<string> arrFields, ref int ipos)
		{
			IfcMaterialPropertiesSuperSeded.parseFields(p, arrFields, ref ipos);
			p.mUpperVaporResistanceFactor = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mLowerVaporResistanceFactor = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mIsothermalMoistureCapacity = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mVaporPermeability = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mMoistureDiffusivity = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mUpperVaporResistanceFactor) + "," + ParserSTEP.DoubleOptionalToString(mLowerVaporResistanceFactor) + "," + ParserSTEP.DoubleOptionalToString(mIsothermalMoistureCapacity) + "," + ParserSTEP.DoubleOptionalToString(mVaporPermeability) + "," + ParserSTEP.DoubleOptionalToString(mMoistureDiffusivity); }
	} 
}
