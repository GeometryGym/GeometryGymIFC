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
using System.Collections.Specialized;
using System.Reflection;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public partial class IfcHalfSpaceSolid : IfcGeometricRepresentationItem, IfcBooleanOperand /* SUPERTYPE OF (ONEOF (IfcBoxedHalfSpace ,IfcPolygonalBoundedHalfSpace)) */
	{
		private IfcSurface mBaseSurface;// : IfcSurface;
		private bool mAgreementFlag;// : BOOLEAN;

		public IfcSurface BaseSurface { get { return mBaseSurface; } set { mBaseSurface = value; } }
		public bool AgreementFlag { get { return mAgreementFlag; } set { mAgreementFlag = value; } }

		internal IfcHalfSpaceSolid() : base() { }
		internal IfcHalfSpaceSolid(DatabaseIfc db, IfcHalfSpaceSolid h, DuplicateOptions options) : base(db, h, options) { BaseSurface = db.Factory.Duplicate(h.BaseSurface, options) as IfcSurface; mAgreementFlag = h.mAgreementFlag; }
		public IfcHalfSpaceSolid(IfcSurface baseSurface, bool agreementFlag) : base(baseSurface.mDatabase) { BaseSurface = baseSurface; AgreementFlag = agreementFlag; }
	}
	public interface IfcHatchLineDistanceSelect : IBaseClassIfc { } // SELECT(IfcPositiveLengthMeasure, IfcVector);
	[Serializable]
	public partial class IfcHeatExchanger : IfcEnergyConversionDevice //IFC4
	{
		private IfcHeatExchangerTypeEnum mPredefinedType = IfcHeatExchangerTypeEnum.NOTDEFINED;// OPTIONAL : IfcHeatExchangerTypeEnum;
		public IfcHeatExchangerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcHeatExchangerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcHeatExchanger() : base() { }
		internal IfcHeatExchanger(DatabaseIfc db, IfcHeatExchanger e, DuplicateOptions options) : base(db, e, options) { PredefinedType = e.PredefinedType; }
		public IfcHeatExchanger(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcHeatExchangerType : IfcEnergyConversionDeviceType
	{
		private IfcHeatExchangerTypeEnum mPredefinedType = IfcHeatExchangerTypeEnum.NOTDEFINED;// : IfcHeatExchangerTypeEnum;
		public IfcHeatExchangerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcHeatExchangerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcHeatExchangerType() : base() { }
		internal IfcHeatExchangerType(DatabaseIfc db, IfcHeatExchangerType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcHeatExchangerType(DatabaseIfc db, string name, IfcHeatExchangerTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcHumidifier : IfcEnergyConversionDevice //IFC4
	{
		private IfcHumidifierTypeEnum mPredefinedType = IfcHumidifierTypeEnum.NOTDEFINED;// OPTIONAL : IfcHumidifierTypeEnum;
		public IfcHumidifierTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcHumidifierTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcHumidifier() : base() { }
		internal IfcHumidifier(DatabaseIfc db, IfcHumidifier h, DuplicateOptions options) : base(db,h, options) { PredefinedType = h.PredefinedType; }
		public IfcHumidifier(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcHumidifierType : IfcEnergyConversionDeviceType
	{
		private IfcHumidifierTypeEnum mPredefinedType = IfcHumidifierTypeEnum.NOTDEFINED;// : IfcHumidifierExchangerEnum;
		public IfcHumidifierTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcHumidifierTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		internal IfcHumidifierType() : base() { }
		internal IfcHumidifierType(DatabaseIfc db, IfcHumidifierType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcHumidifierType(DatabaseIfc db, string name, IfcHumidifierTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcHygroscopicMaterialProperties : IfcMaterialProperties // DEPRECATED IFC4
	{
		internal double mUpperVaporResistanceFactor = double.NaN, mLowerVaporResistanceFactor = double.NaN; //: OPTIONAL IfcPositiveRatioMeasure;
		internal double mIsothermalMoistureCapacity = double.NaN; //: : OPTIONAL IfcIsothermalMoistureCapacityMeasure;
		internal double mVaporPermeability = double.NaN;//: OPTIONAL IfcVaporPermeabilityMeasure;
		internal double mMoistureDiffusivity = double.NaN;// : OPTIONAL IfcMoistureDiffusivityMeasure;*/
		internal IfcHygroscopicMaterialProperties() : base() { }
		internal IfcHygroscopicMaterialProperties(DatabaseIfc db, IfcHygroscopicMaterialProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mUpperVaporResistanceFactor = p.mUpperVaporResistanceFactor;
			mLowerVaporResistanceFactor = p.mLowerVaporResistanceFactor;
			mIsothermalMoistureCapacity = p.mIsothermalMoistureCapacity;
			mVaporPermeability = p.mVaporPermeability;
			mMoistureDiffusivity = p.mMoistureDiffusivity;
		}
	} 
}
