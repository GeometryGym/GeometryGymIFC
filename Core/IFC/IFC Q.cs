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
	public partial class IfcQuantityArea : IfcPhysicalSimpleQuantity
	{
		internal double mAreaValue;// : IfcAreaMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel; IFC4

		public double AreaValue { get { return mAreaValue; } set { mAreaValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value);  } }

		internal IfcQuantityArea() : base() { }
		internal IfcQuantityArea(DatabaseIfc db, IfcQuantityArea q) : base(db,q) { mAreaValue = q.mAreaValue; mFormula = q.mFormula; }
		public IfcQuantityArea(DatabaseIfc db,string name, double area) : base(db,name) { mAreaValue = area;  }
		internal static IfcQuantityArea Parse(string str, ReleaseVersion schema)
		{
			IfcQuantityArea q = new IfcQuantityArea();
			int pos = 0, len = str.Length;
			q.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (!double.TryParse(s, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out q.mAreaValue))
			{
				if (s.StartsWith("IFCAREAMEASURE"))
				{
					s = s.Substring(15, s.Length - 16);
					double.TryParse(s, out q.mAreaValue);
				}
			}
			if (schema != ReleaseVersion.IFC2x3)
				q.mFormula =  ParserSTEP.StripString(str, ref pos, len);
			return q;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mAreaValue) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }

		internal override IfcMeasureValue MeasureValue { get { return new IfcAreaMeasure(mAreaValue); } }
	}
	public partial class IfcQuantityCount : IfcPhysicalSimpleQuantity
	{
		internal double mCountValue;// : IfcCountMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double CountValue { get { return mCountValue; } set { mCountValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value); } }

		internal IfcQuantityCount() : base() { }
		internal IfcQuantityCount(DatabaseIfc db, IfcQuantityCount q) : base(db,q) { mCountValue = q.mCountValue; mFormula = q.mFormula; }
		public IfcQuantityCount(DatabaseIfc db, string name, double count) : base(db, name) { mCountValue = count; }
		internal static IfcQuantityCount Parse(string str, ReleaseVersion schema)
		{
			IfcQuantityCount q = new IfcQuantityCount();
			int pos = 0, len = str.Length;
			q.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (!double.TryParse(s, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out q.mCountValue))
			{
				if (s.StartsWith("IFCCOUNTMEASURE"))
				{
					s = s.Substring(16, s.Length - 17);
					double.TryParse(s, out q.mCountValue);
				}
			}
			if (schema != ReleaseVersion.IFC2x3)
				q.mFormula = ParserSTEP.StripString(str, ref pos, len);
			return q;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mCountValue) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }

		internal override IfcMeasureValue MeasureValue { get { return new IfcCountMeasure(mCountValue); } }
	}
	public partial class IfcQuantityLength : IfcPhysicalSimpleQuantity
	{
		internal double mLengthValue;// : IfcLengthMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double LengthValue { get { return mLengthValue; } set { mLengthValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value);  } }

		internal IfcQuantityLength() : base() { }
		internal IfcQuantityLength(DatabaseIfc db, IfcQuantityLength q) : base(db,q) { mLengthValue = q.mLengthValue; mFormula = q.mFormula; }
		public IfcQuantityLength(DatabaseIfc db, string name, double length) : base(db, name) { mLengthValue = length; }
		internal static void parseFields(IfcQuantityLength q, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcPhysicalSimpleQuantity.parseFields(q, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (!double.TryParse(str, out q.mLengthValue))
			{
				if (str.StartsWith("IFCLENGTHMEASURE"))
				{
					str = str.Substring(17, str.Length - 18);
					double.TryParse(str, out q.mLengthValue);
				}
			}
			if (schema != ReleaseVersion.IFC2x3)
				q.mFormula = arrFields[ipos++].Replace("'", "");
		}
		internal static IfcQuantityLength Parse(string str, ReleaseVersion schema)
		{
			IfcQuantityLength q = new IfcQuantityLength();
			int pos = 0, len = str.Length;
			q.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (!double.TryParse(s, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out q.mLengthValue))
			{
				if (s.StartsWith("IFCLENGTHMEASURE"))
				{
					s = s.Substring(17, s.Length - 18);
					double.TryParse(s, out q.mLengthValue);
				}
			}
			if (schema != ReleaseVersion.IFC2x3)
				q.mFormula = ParserSTEP.StripString(str, ref pos, len);
			return q;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(Math.Max(0, mLengthValue)) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }

		internal override IfcMeasureValue MeasureValue { get { return new IfcLengthMeasure(mLengthValue); } }
	}
	public abstract partial class IfcQuantitySet : IfcPropertySetDefinition // IFC4  ABSTRACT SUPERTYPE OF(IfcElementQuantity)
	{
		protected IfcQuantitySet() : base() { }
		protected IfcQuantitySet(DatabaseIfc db, string name) : base(db,name) { }
		protected IfcQuantitySet(DatabaseIfc db, IfcQuantitySet s) : base(db, s) { }
		protected static void parseFields(IfcExternalSpatialStructureElement s, List<string> arrFields, ref int ipos) { IfcSpatialElement.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcQuantityTime : IfcPhysicalSimpleQuantity
	{
		internal double mTimeValue;// : IfcTimeMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double TimeValue { get { return mTimeValue; } set { mTimeValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value);  } }

		internal IfcQuantityTime() : base() { }
		internal IfcQuantityTime(DatabaseIfc db, IfcQuantityTime q) : base(db,q) { mTimeValue = q.mTimeValue; mFormula = q.mFormula; }
		public IfcQuantityTime(DatabaseIfc db, string name, int ifctimemeasure) : base(db, name) { mTimeValue = ifctimemeasure; }
		internal static void parseFields(IfcQuantityTime q, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcPhysicalSimpleQuantity.parseFields(q, arrFields, ref ipos); q.mTimeValue = int.Parse(arrFields[ipos++]); if (schema != ReleaseVersion.IFC2x3) q.mFormula = arrFields[ipos++].Replace("'", ""); }
		internal static IfcQuantityTime Parse(string str, ReleaseVersion schema)
		{
			IfcQuantityTime q = new IfcQuantityTime();
			int pos = 0, len = str.Length;
			q.Parse(str, ref pos, len);
			double.TryParse(ParserSTEP.StripField(str, ref pos, len), out q.mTimeValue);
			if (schema != ReleaseVersion.IFC2x3)
				q.mFormula = ParserSTEP.StripString(str, ref pos, len);
			return q;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString( mTimeValue) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }

		internal override IfcMeasureValue MeasureValue { get { return new IfcTimeMeasure(mTimeValue); } }
	}
	public partial class IfcQuantityVolume : IfcPhysicalSimpleQuantity
	{
		internal double mVolumeValue;// : IfcVolumeMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double VolumeValue { get { return mVolumeValue; } set { mVolumeValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value); } }

		internal IfcQuantityVolume() : base() { }
		internal IfcQuantityVolume(DatabaseIfc db, IfcQuantityVolume q) : base(db,q) { mVolumeValue = q.mVolumeValue; mFormula = q.mFormula; }
		public IfcQuantityVolume(DatabaseIfc db, string name, double volume) : base(db, name) { mVolumeValue = volume; }
		
		internal static IfcQuantityVolume Parse(string str, ReleaseVersion schema)
		{
			IfcQuantityVolume q = new IfcQuantityVolume();
			int pos = 0, len = str.Length;
			q.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (!double.TryParse(s, out q.mVolumeValue))
			{
				IfcMeasureValue mv = ParserIfc.parseMeasureValue(s);
				if (mv != null)
					q.mVolumeValue = mv.Measure;
			}
			if (schema != ReleaseVersion.IFC2x3)
				q.mFormula = ParserSTEP.StripString(str, ref pos, len);
			return q;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mVolumeValue) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
		internal override IfcMeasureValue MeasureValue { get { return new IfcVolumeMeasure(mVolumeValue); } }
	}
	public partial class IfcQuantityWeight : IfcPhysicalSimpleQuantity
	{
		internal double mWeightValue;// : IfcMassMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double WeightValue { get { return mWeightValue; } set { mWeightValue = value; } }
		public string Formula { get { return mFormula == "$" ? "" : mFormula; } set { mFormula = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value); } }

		internal IfcQuantityWeight() : base() { }
		internal IfcQuantityWeight(DatabaseIfc db, IfcQuantityWeight q) : base(db,q) { mWeightValue = q.mWeightValue; mFormula = q.mFormula; }
		public IfcQuantityWeight(DatabaseIfc db, string name, double weight) : base(db, name) { mWeightValue = weight; }
		internal static void parseFields(IfcQuantityWeight q, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcPhysicalSimpleQuantity.parseFields(q, arrFields, ref ipos); q.mWeightValue = ParserSTEP.ParseDouble(arrFields[ipos++]); if (schema != ReleaseVersion.IFC2x3) q.mFormula = arrFields[ipos++].Replace("'", ""); }
		internal static IfcQuantityWeight Parse(string str, ReleaseVersion schema)
		{
			IfcQuantityWeight q = new IfcQuantityWeight();
			int pos = 0, len = str.Length;
			q.Parse(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (!double.TryParse(s, out q.mWeightValue))
			{
				IfcMeasureValue mv = ParserIfc.parseMeasureValue(s);
				if (mv != null)
					q.mWeightValue = mv.Measure;
			}
			if (schema != ReleaseVersion.IFC2x3)
				q.mFormula = ParserSTEP.StripString(str, ref pos, len);
			return q;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mWeightValue) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }

		internal override IfcMeasureValue MeasureValue { get { return new IfcMassMeasure(mWeightValue); } }
	}
}
