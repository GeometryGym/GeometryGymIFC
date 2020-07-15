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
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public abstract partial class IfcNamedUnit : BaseClassIfc, IfcUnit  //ABSTRACT SUPERTYPE OF (ONEOF(IfcContextDependentUnit,IfcConversionBasedUnit,IfcSIUnit));
	{
		internal IfcDimensionalExponents mDimensions = null;// : IfcDimensionalExponents;
		private IfcUnitEnum mUnitType;// : IfcUnitEnum 

		public IfcDimensionalExponents Dimensions { get { return mDimensions as IfcDimensionalExponents; } set { mDimensions = value; } }
		public IfcUnitEnum UnitType { get { return mUnitType; } set { mUnitType = value; } }

		protected IfcNamedUnit() : base() { }
		protected IfcNamedUnit(DatabaseIfc db, IfcNamedUnit u) : base(db, u) { if (u.mDimensions != null) Dimensions = db.Factory.Duplicate(u.Dimensions) as IfcDimensionalExponents; mUnitType = u.mUnitType; }
		protected IfcNamedUnit(DatabaseIfc m, IfcUnitEnum unitEnum, bool gendims) : base(m)
		{
			mUnitType = unitEnum;
			if (gendims)
			{
				if (unitEnum == IfcUnitEnum.LENGTHUNIT)
					Dimensions = new IfcDimensionalExponents(m, 1, 0, 0, 0, 0, 0, 0);
				else if (unitEnum == IfcUnitEnum.AREAUNIT)
					Dimensions = new IfcDimensionalExponents(m, 2, 0, 0, 0, 0, 0, 0);
				else if (unitEnum == IfcUnitEnum.VOLUMEUNIT)
					Dimensions = new IfcDimensionalExponents(m, 3, 0, 0, 0, 0, 0, 0);
				else if (unitEnum == IfcUnitEnum.MASSUNIT)
					Dimensions = new IfcDimensionalExponents(m, 0, 1, 0, 0, 0, 0, 0);
				else if (unitEnum == IfcUnitEnum.TIMEUNIT)
					Dimensions = new IfcDimensionalExponents(m, 0, 0, 1, 0, 0, 0, 0);
				else if (unitEnum == IfcUnitEnum.ELECTRICCURRENTUNIT)
					Dimensions = new IfcDimensionalExponents(m, 0, 0, 0, 1, 0, 0, 0);
				else if (unitEnum == IfcUnitEnum.THERMODYNAMICTEMPERATUREUNIT)
					Dimensions = new IfcDimensionalExponents(m, 0, 0, 0, 0, 1, 0, 0);
				else if (unitEnum == IfcUnitEnum.AMOUNTOFSUBSTANCEUNIT)
					Dimensions = new IfcDimensionalExponents(m, 0, 0, 0, 0, 0, 1, 0);
				else if (unitEnum == IfcUnitEnum.LUMINOUSINTENSITYUNIT)
					Dimensions = new IfcDimensionalExponents(m, 0, 0, 0, 0, 0, 0, 1);
				else if (unitEnum == IfcUnitEnum.PLANEANGLEUNIT)
					Dimensions = new IfcDimensionalExponents(m, 0, 0, 0, 0, 0, 0, 0);
			}
		}
		public IfcNamedUnit(IfcDimensionalExponents dimensions, IfcUnitEnum unitType)
			: base(dimensions.Database) { Dimensions = dimensions; UnitType = unitType; }

		public abstract double SIFactor { get; }
	}
	[Serializable]
	public partial class IfcNavigationElement : IfcBuiltElement
	{
		private IfcNavigationElementTypeEnum mPredefinedType = IfcNavigationElementTypeEnum.NOTDEFINED; //: OPTIONAL IfcNavigationElementTypeEnum;
		public IfcNavigationElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcNavigationElement() : base() { }
		public IfcNavigationElement(DatabaseIfc db) : base(db) { }
		public IfcNavigationElement(DatabaseIfc db, IfcNavigationElement navigationElement, DuplicateOptions options) : base(db, navigationElement, options) { PredefinedType = navigationElement.PredefinedType; }
		public IfcNavigationElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcNavigationElementType : IfcBuiltElementType
	{
		private IfcNavigationElementTypeEnum mPredefinedType = IfcNavigationElementTypeEnum.NOTDEFINED; //: IfcNavigationElementTypeEnum;
		public IfcNavigationElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcNavigationElementType() : base() { }
		public IfcNavigationElementType(DatabaseIfc db, IfcNavigationElementType navigationElementType, DuplicateOptions options) : base(db, navigationElementType, options) { PredefinedType = navigationElementType.PredefinedType; }
		public IfcNavigationElementType(DatabaseIfc db, string name, IfcNavigationElementTypeEnum predefinedType)
			: base(db, name) { PredefinedType = predefinedType; }
	}
}
