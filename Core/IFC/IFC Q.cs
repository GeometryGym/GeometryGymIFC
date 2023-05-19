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
	public partial class IfcQuantityArea : IfcPhysicalSimpleQuantity
	{
		private double mAreaValue;// : IfcAreaMeasure;	
		private string mFormula = ""; //:	OPTIONAL IfcLabel; IFC4

		public double AreaValue { get { return mAreaValue; } set { mAreaValue = value; } }
		public string Formula { get { return mFormula; } set { mFormula = value;  } }

		internal IfcQuantityArea() : base() { }
		internal IfcQuantityArea(DatabaseIfc db, IfcQuantityArea q) : base(db,q) { mAreaValue = q.mAreaValue; mFormula = q.mFormula; }
		public IfcQuantityArea(DatabaseIfc db,string name, double area) : base(db,name) { mAreaValue = area;  }
		
		public override IfcMeasureValue MeasureValue { get { return new IfcAreaMeasure(mAreaValue); } }
	}
	[Serializable]
	public partial class IfcQuantityCount : IfcPhysicalSimpleQuantity
	{
		private int mCountValue = int.MinValue;// : IfcCountMeasure;	
		private double mCountValueDouble;// : IfcCountMeasure;	
		private string mFormula = ""; //:	OPTIONAL IfcLabel;  IFC4

		public int CountValue { get { return (mCountValue == int.MinValue ? (int) mCountValueDouble :  mCountValue); } set { mCountValue = value; } }
		public double CountValueDouble { get { return (mCountValue == int.MinValue ? mCountValueDouble :  mCountValue); } set { mCountValueDouble = value; mCountValue = int.MinValue; } }
		public string Formula { get { return mFormula; } set { mFormula = value; } }

		internal IfcQuantityCount() : base() { }
		internal IfcQuantityCount(DatabaseIfc db, IfcQuantityCount q) : base(db,q) { mCountValue = q.mCountValue; mFormula = q.mFormula; }
		public IfcQuantityCount(DatabaseIfc db, string name, int count) : base(db, name) { mCountValue = count; }
		[Obsolete("Type changed to int IFC4x3", false)]
		public IfcQuantityCount(DatabaseIfc db, string name, double count) : base(db, name) { mCountValueDouble = count; }
		public override IfcMeasureValue MeasureValue { get { return new IfcCountMeasure(mCountValue); } }
	}
	[Serializable]
	public partial class IfcQuantityLength : IfcPhysicalSimpleQuantity
	{
		private double mLengthValue;// : IfcLengthMeasure;	
		private string mFormula = ""; //:	OPTIONAL IfcLabel;  IFC4

		public double LengthValue { get { return mLengthValue; } set { mLengthValue = value; } }
		public string Formula { get { return mFormula; } set { mFormula = value;  } }

		internal IfcQuantityLength() : base() { }
		internal IfcQuantityLength(DatabaseIfc db, IfcQuantityLength q) : base(db,q) { mLengthValue = q.mLengthValue; mFormula = q.mFormula; }
		public IfcQuantityLength(DatabaseIfc db, string name, double length) : base(db, name) { mLengthValue = length; }
		
		public override IfcMeasureValue MeasureValue { get { return new IfcLengthMeasure(mLengthValue); } }
	}
	[Serializable]
	public partial class IfcQuantityNumber : IfcPhysicalSimpleQuantity //IFC4x3
	{
		public override string StepClassName { get { return (mDatabase == null || mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcQuantityCount" : base.StepClassName); } }
		private double mNumberValue;// : IfcNumericMeasure;	
		private string mFormula = ""; //:	OPTIONAL IfcLabel;  

		public double NumberValue { get { return mNumberValue; } set { mNumberValue = value; } }
		public string Formula { get { return mFormula; } set { mFormula = value; } }

		internal IfcQuantityNumber() : base() { }
		internal IfcQuantityNumber(DatabaseIfc db, IfcQuantityNumber q) : base(db, q) { mNumberValue = q.mNumberValue; mFormula = q.mFormula; }
		public IfcQuantityNumber(DatabaseIfc db, string name, double count) : base(db, name) { mNumberValue = count; }
		public override IfcMeasureValue MeasureValue { get { return new IfcNumericMeasure(mNumberValue); } }
	}
	[Serializable]
	public abstract partial class IfcQuantitySet : IfcPropertySetDefinition // IFC4  ABSTRACT SUPERTYPE OF(IfcElementQuantity)
	{
		protected IfcQuantitySet() : base() { }
		protected IfcQuantitySet(DatabaseIfc db, string name) : base(db,name) { }
		protected IfcQuantitySet(DatabaseIfc db, IfcQuantitySet s, DuplicateOptions options) : base(db, s, options) { }
	}
	[Serializable]
	public partial class IfcQuantityTime : IfcPhysicalSimpleQuantity
	{
		private double mTimeValue;// : IfcTimeMeasure;	
		private string mFormula = ""; //:	OPTIONAL IfcLabel;  IFC4

		public double TimeValue { get { return mTimeValue; } set { mTimeValue = value; } }
		public string Formula { get { return mFormula; } set { mFormula = value;  } }

		internal IfcQuantityTime() : base() { }
		internal IfcQuantityTime(DatabaseIfc db, IfcQuantityTime q) : base(db,q) { mTimeValue = q.mTimeValue; mFormula = q.mFormula; }
		internal IfcQuantityTime(DatabaseIfc db, string name, double timeMeasure) : base(db, name) { mTimeValue = timeMeasure; }
		
		public override IfcMeasureValue MeasureValue { get { return new IfcTimeMeasure(mTimeValue); } }
	}
	[Serializable]
	public partial class IfcQuantityVolume : IfcPhysicalSimpleQuantity
	{
		private double mVolumeValue;// : IfcVolumeMeasure;	
		private string mFormula = ""; //:	OPTIONAL IfcLabel;  IFC4

		public double VolumeValue { get { return mVolumeValue; } set { mVolumeValue = value; } }
		public string Formula { get { return mFormula; } set { mFormula = value; } }

		internal IfcQuantityVolume() : base() { }
		internal IfcQuantityVolume(DatabaseIfc db, IfcQuantityVolume q) : base(db,q) { mVolumeValue = q.mVolumeValue; mFormula = q.mFormula; }
		public IfcQuantityVolume(DatabaseIfc db, string name, double volume) : base(db, name) { mVolumeValue = volume; }
		
		public override IfcMeasureValue MeasureValue { get { return new IfcVolumeMeasure(mVolumeValue); } }
	}
	[Serializable]
	public partial class IfcQuantityWeight : IfcPhysicalSimpleQuantity
	{
		private double mWeightValue;// : IfcMassMeasure;	
		private string mFormula = ""; //:	OPTIONAL IfcLabel;  IFC4

		public double WeightValue { get { return mWeightValue; } set { mWeightValue = value; } }
		public string Formula { get { return mFormula; } set { mFormula = value; } }

		internal IfcQuantityWeight() : base() { }
		internal IfcQuantityWeight(DatabaseIfc db, IfcQuantityWeight q) : base(db,q) { mWeightValue = q.mWeightValue; mFormula = q.mFormula; }
		public IfcQuantityWeight(DatabaseIfc db, string name, double weight) : base(db, name) { mWeightValue = weight; }
		
		public override IfcMeasureValue MeasureValue { get { return new IfcMassMeasure(mWeightValue); } }
	}
}
