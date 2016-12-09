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
	public abstract partial class IfcNamedUnit : BaseClassIfc, IfcUnit //ABSTRACT SUPERTYPE OF (ONEOF(IfcContextDependentUnit,IfcConversionBasedUnit,IfcSIUnit));
	{
		internal int mDimensions;// : IfcDimensionalExponents;
		private IfcUnitEnum mUnitType;// : IfcUnitEnum 

		public IfcDimensionalExponents Dimensions { get { return mDatabase[mDimensions] as IfcDimensionalExponents;  } set { mDimensions = value.Index; } }
		public IfcUnitEnum UnitType { get { return mUnitType; } set { mUnitType = value; } }

		protected IfcNamedUnit() : base() { }
		protected IfcNamedUnit(DatabaseIfc db, IfcNamedUnit u) : base(db,u) { if(u.mDimensions > 0) Dimensions = db.Factory.Duplicate(u.Dimensions) as IfcDimensionalExponents; mUnitType = u.mUnitType; }
		protected IfcNamedUnit(DatabaseIfc m, IfcUnitEnum unitEnum, bool gendims) : base(m)
		{
			mUnitType = unitEnum;
			if (gendims)
			{
				if (unitEnum == IfcUnitEnum.LENGTHUNIT)
					mDimensions = new IfcDimensionalExponents(m, 1, 0, 0, 0, 0, 0, 0).mIndex;
				else if (unitEnum == IfcUnitEnum.AREAUNIT)
					mDimensions = new IfcDimensionalExponents(m, 2, 0, 0, 0, 0, 0, 0).mIndex;
				else if (unitEnum == IfcUnitEnum.VOLUMEUNIT)
					mDimensions = new IfcDimensionalExponents(m, 3, 0, 0, 0, 0, 0, 0).mIndex;
				else if (unitEnum == IfcUnitEnum.PLANEANGLEUNIT)
					mDimensions = new IfcDimensionalExponents(m, 0, 0, 0, 0, 0, 0, 0).mIndex;
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + (mDimensions == 0 ? "*" : ParserSTEP.LinkToString(mDimensions)) + ",." + mUnitType.ToString() + "."; }
		protected static void parseFields(IfcNamedUnit u, List<string> arrFields, ref int ipos) { u.mDimensions = ParserSTEP.ParseLink(arrFields[ipos++]); u.mUnitType = (IfcUnitEnum)Enum.Parse(typeof(IfcUnitEnum), arrFields[ipos++].Replace(".", "")); }
		internal virtual double getSIFactor() { return 1; }
	}
}
