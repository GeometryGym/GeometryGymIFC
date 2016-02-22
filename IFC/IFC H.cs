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

		internal IfcSurface BaseSurface { get { return mDatabase.mIfcObjects[mBaseSurface] as IfcSurface; } }
		internal bool AgreementFlag { get { return mAgreementFlag; } }

		internal IfcHalfSpaceSolid() : base() { }
		internal IfcHalfSpaceSolid(IfcHalfSpaceSolid pl) : base(pl) { mBaseSurface = pl.mBaseSurface; mAgreementFlag = pl.mAgreementFlag; }

		internal static void parseFields(IfcHalfSpaceSolid s, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(s, arrFields, ref ipos); s.mBaseSurface = ParserSTEP.ParseLink(arrFields[ipos++]); s.mAgreementFlag = ParserSTEP.ParseBool(arrFields[ipos++]); }
		internal static IfcHalfSpaceSolid Parse(string strDef) { IfcHalfSpaceSolid s = new IfcHalfSpaceSolid(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mBaseSurface) + "," + ParserSTEP.BoolToString(mAgreementFlag); }
	}
	public class IfcHeatExchanger : IfcEnergyConversionDevice //IFC4
	{
		internal IfcHeatExchangerTypeEnum mPredefinedType = IfcHeatExchangerTypeEnum.NOTDEFINED;// OPTIONAL : IfcHeatExchangerTypeEnum;
		public IfcHeatExchangerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcHeatExchanger() : base() { }
		internal IfcHeatExchanger(IfcHeatExchanger b) : base(b) { mPredefinedType = b.mPredefinedType; }
		internal IfcHeatExchanger(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcHeatExchanger s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcHeatExchangerTypeEnum)Enum.Parse(typeof(IfcHeatExchangerTypeEnum), str);
		}
		internal new static IfcHeatExchanger Parse(string strDef) { IfcHeatExchanger s = new IfcHeatExchanger(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcHeatExchangerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcHeatExchangerType : IfcEnergyConversionDeviceType
	{
		internal IfcHeatExchangerTypeEnum mPredefinedType = IfcHeatExchangerTypeEnum.NOTDEFINED;// : IfcHeatExchangerTypeEnum;
		public IfcHeatExchangerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcHeatExchangerType() : base() { }
		internal IfcHeatExchangerType(IfcHeatExchangerType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcHeatExchangerType(DatabaseIfc m, string name, IfcHeatExchangerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcHeatExchangerType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcHeatExchangerTypeEnum)Enum.Parse(typeof(IfcHeatExchangerTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcHeatExchangerType Parse(string strDef) { IfcHeatExchangerType t = new IfcHeatExchangerType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcHumidifier : IfcEnergyConversionDevice //IFC4
	{
		internal IfcHumidifierTypeEnum mPredefinedType = IfcHumidifierTypeEnum.NOTDEFINED;// OPTIONAL : IfcHumidifierTypeEnum;
		public IfcHumidifierTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcHumidifier() : base() { }
		internal IfcHumidifier(IfcHumidifier b) : base(b) { mPredefinedType = b.mPredefinedType; }
		internal IfcHumidifier(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcHumidifier s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcHumidifierTypeEnum)Enum.Parse(typeof(IfcHumidifierTypeEnum), str);
		}
		internal new static IfcHumidifier Parse(string strDef) { IfcHumidifier s = new IfcHumidifier(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcHumidifierTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcHumidifierType : IfcEnergyConversionDeviceType
	{
		internal IfcHumidifierTypeEnum mPredefinedType = IfcHumidifierTypeEnum.NOTDEFINED;// : IfcHumidifierExchangerEnum;
		public IfcHumidifierTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcHumidifierType() : base() { }
		internal IfcHumidifierType(IfcHumidifierType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcHumidifierType(DatabaseIfc m, string name, IfcHumidifierTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcHumidifierType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcHumidifierTypeEnum)Enum.Parse(typeof(IfcHumidifierTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcHumidifierType Parse(string strDef) { IfcHumidifierType t = new IfcHumidifierType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcHygroscopicMaterialProperties : IfcMaterialPropertiesSuperSeded // DEPRECEATED IFC4
	{
		internal double mUpperVaporResistanceFactor, mLowerVaporResistanceFactor; //: OPTIONAL IfcPositiveRatioMeasure;
		internal double mIsothermalMoistureCapacity; //: : OPTIONAL IfcIsothermalMoistureCapacityMeasure;
		internal double mVaporPermeability;//: OPTIONAL IfcVaporPermeabilityMeasure;
		internal double mMoistureDiffusivity;// : OPTIONAL IfcMoistureDiffusivityMeasure;*/
		internal IfcHygroscopicMaterialProperties() : base() { }
		internal IfcHygroscopicMaterialProperties(IfcHygroscopicMaterialProperties el)
			: base(el)
		{
			mUpperVaporResistanceFactor = el.mUpperVaporResistanceFactor;
			mLowerVaporResistanceFactor = el.mLowerVaporResistanceFactor;
			mIsothermalMoistureCapacity = el.mIsothermalMoistureCapacity;
			mVaporPermeability = el.mVaporPermeability;
			mMoistureDiffusivity = el.mMoistureDiffusivity;
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
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mUpperVaporResistanceFactor) + "," + ParserSTEP.DoubleOptionalToString(mLowerVaporResistanceFactor) + "," + ParserSTEP.DoubleOptionalToString(mIsothermalMoistureCapacity) + "," + ParserSTEP.DoubleOptionalToString(mVaporPermeability) + "," + ParserSTEP.DoubleOptionalToString(mMoistureDiffusivity); }
	} 
}
