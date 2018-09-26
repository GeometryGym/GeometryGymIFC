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
	public partial class IfcQuantityArea : IfcPhysicalSimpleQuantity
	{
		internal double mAreaValue;// : IfcAreaMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel; IFC4

		public double AreaValue { get { return mAreaValue; } set { mAreaValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value);  } }

		internal IfcQuantityArea() : base() { }
		internal IfcQuantityArea(DatabaseIfc db, IfcQuantityArea q) : base(db,q) { mAreaValue = q.mAreaValue; mFormula = q.mFormula; }
		public IfcQuantityArea(DatabaseIfc db,string name, double area) : base(db,name) { mAreaValue = area;  }
		
		public override IfcMeasureValue MeasureValue { get { return new IfcAreaMeasure(mAreaValue); } }
	}
	[Serializable]
	public partial class IfcQuantityCount : IfcPhysicalSimpleQuantity
	{
		internal double mCountValue;// : IfcCountMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double CountValue { get { return mCountValue; } set { mCountValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value); } }

		internal IfcQuantityCount() : base() { }
		internal IfcQuantityCount(DatabaseIfc db, IfcQuantityCount q) : base(db,q) { mCountValue = q.mCountValue; mFormula = q.mFormula; }
		public IfcQuantityCount(DatabaseIfc db, string name, double count) : base(db, name) { mCountValue = count; }
		
		public override IfcMeasureValue MeasureValue { get { return new IfcCountMeasure(mCountValue); } }
	}
	[Serializable]
	public partial class IfcQuantityLength : IfcPhysicalSimpleQuantity
	{
		internal double mLengthValue;// : IfcLengthMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double LengthValue { get { return mLengthValue; } set { mLengthValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value);  } }

		internal IfcQuantityLength() : base() { }
		internal IfcQuantityLength(DatabaseIfc db, IfcQuantityLength q) : base(db,q) { mLengthValue = q.mLengthValue; mFormula = q.mFormula; }
		public IfcQuantityLength(DatabaseIfc db, string name, double length) : base(db, name) { mLengthValue = length; }
		
		public override IfcMeasureValue MeasureValue { get { return new IfcLengthMeasure(mLengthValue); } }
	}
	[Serializable]
	public abstract partial class IfcQuantitySet : IfcPropertySetDefinition // IFC4  ABSTRACT SUPERTYPE OF(IfcElementQuantity)
	{
		protected IfcQuantitySet() : base() { }
		protected IfcQuantitySet(DatabaseIfc db, string name) : base(db,name) { }
		protected IfcQuantitySet(DatabaseIfc db, IfcQuantitySet s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcQuantityTime : IfcPhysicalSimpleQuantity
	{
		internal double mTimeValue;// : IfcTimeMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double TimeValue { get { return mTimeValue; } set { mTimeValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value);  } }

		internal IfcQuantityTime() : base() { }
		internal IfcQuantityTime(DatabaseIfc db, IfcQuantityTime q) : base(db,q) { mTimeValue = q.mTimeValue; mFormula = q.mFormula; }
		public IfcQuantityTime(DatabaseIfc db, string name, int ifctimemeasure) : base(db, name) { mTimeValue = ifctimemeasure; }
		
		public override IfcMeasureValue MeasureValue { get { return new IfcTimeMeasure(mTimeValue); } }
	}
	[Serializable]
	public partial class IfcQuantityVolume : IfcPhysicalSimpleQuantity
	{
		internal double mVolumeValue;// : IfcVolumeMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double VolumeValue { get { return mVolumeValue; } set { mVolumeValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value); } }

		internal IfcQuantityVolume() : base() { }
		internal IfcQuantityVolume(DatabaseIfc db, IfcQuantityVolume q) : base(db,q) { mVolumeValue = q.mVolumeValue; mFormula = q.mFormula; }
		public IfcQuantityVolume(DatabaseIfc db, string name, double volume) : base(db, name) { mVolumeValue = volume; }
		
		public override IfcMeasureValue MeasureValue { get { return new IfcVolumeMeasure(mVolumeValue); } }
	}
	[Serializable]
	public partial class IfcQuantityWeight : IfcPhysicalSimpleQuantity
	{
		internal double mWeightValue;// : IfcMassMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double WeightValue { get { return mWeightValue; } set { mWeightValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value); } }

		internal IfcQuantityWeight() : base() { }
		internal IfcQuantityWeight(DatabaseIfc db, IfcQuantityWeight q) : base(db,q) { mWeightValue = q.mWeightValue; mFormula = q.mFormula; }
		public IfcQuantityWeight(DatabaseIfc db, string name, double weight) : base(db, name) { mWeightValue = weight; }
		
		public override IfcMeasureValue MeasureValue { get { return new IfcMassMeasure(mWeightValue); } }
	}
}
