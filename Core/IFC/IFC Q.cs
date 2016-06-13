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
	public partial class IfcQuantityArea : IfcPhysicalSimpleQuantity
	{
		internal double mAreaValue;// : IfcAreaMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel; IFC4

		public double AreaValue { get { return mAreaValue; } }
		public string Forumla { get { return mFormula == "$" ? "" : mFormula; } }

		internal IfcQuantityArea() : base() { }
		internal IfcQuantityArea(DatabaseIfc m, string name, string desc, IfcNamedUnit unit, double area, string formula) : base(m, name, desc, unit) { mAreaValue = area; mFormula = formula; }
		internal static void parseFields(IfcQuantityArea q, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcPhysicalSimpleQuantity.parseFields(q, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (!double.TryParse(str, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out q.mAreaValue))
			{
				if (str.StartsWith("IFCAREAMEASURE"))
				{
					str = str.Substring(15, str.Length - 16);
					double.TryParse(str, out q.mAreaValue);
				}
			}
			if (schema != ReleaseVersion.IFC2x3)
				q.mFormula = arrFields[ipos++].Replace("'", "");
		}
		internal static IfcQuantityArea Parse(string strDef, ReleaseVersion schema) { IfcQuantityArea q = new IfcQuantityArea(); int ipos = 0; parseFields(q, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return q; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mAreaValue) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
	}
	public partial class IfcQuantityCount : IfcPhysicalSimpleQuantity
	{
		internal double mCountValue;// : IfcCountMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double CountValue { get { return mCountValue; } }
		public string Forumla { get { return mFormula == "$" ? "" : mFormula; } }

		internal IfcQuantityCount() : base() { }
		internal IfcQuantityCount(DatabaseIfc m, string name, string desc, IfcNamedUnit unit, double count, string formula) : base(m, name, desc, unit) { mCountValue = count; mFormula = formula; }
		internal static void parseFields(IfcQuantityCount q, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcPhysicalSimpleQuantity.parseFields(q, arrFields, ref ipos); q.mCountValue = ParserSTEP.ParseDouble(arrFields[ipos++]); if (schema != ReleaseVersion.IFC2x3) q.mFormula = arrFields[ipos++].Replace("'", ""); }
		internal static IfcQuantityCount Parse(string strDef, ReleaseVersion schema) { IfcQuantityCount q = new IfcQuantityCount(); int ipos = 0; parseFields(q, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return q; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mCountValue) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
	}
	public partial class IfcQuantityLength : IfcPhysicalSimpleQuantity
	{
		internal double mLengthValue;// : IfcLengthMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double LengthValue { get { return mLengthValue; } }
		public string Forumla { get { return mFormula == "$" ? "" : mFormula; } }

		internal IfcQuantityLength() : base() { }
		internal IfcQuantityLength(DatabaseIfc m, string name, string desc, IfcNamedUnit unit, double length, string formula) : base(m, name, desc, unit) { mLengthValue = length; mFormula = formula; }
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
		internal static IfcQuantityLength Parse(string strDef, ReleaseVersion schema) { IfcQuantityLength q = new IfcQuantityLength(); int ipos = 0; parseFields(q, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return q; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(Math.Max(0, mLengthValue)) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
	}
	public partial class IfcQuantityTime : IfcPhysicalSimpleQuantity
	{
		internal int mTimeValue;// : IfcTimeMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public int TimeValue { get { return mTimeValue; } }
		public string Forumla { get { return mFormula == "$" ? "" : mFormula; } }

		internal IfcQuantityTime() : base() { }
		internal IfcQuantityTime(DatabaseIfc m, string name, string desc, IfcNamedUnit unit, int ifctimemeasure, string formula) : base(m, name, desc, unit) { mTimeValue = ifctimemeasure; mFormula = formula; }
		internal static void parseFields(IfcQuantityTime q, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcPhysicalSimpleQuantity.parseFields(q, arrFields, ref ipos); q.mTimeValue = int.Parse(arrFields[ipos++]); if (schema != ReleaseVersion.IFC2x3) q.mFormula = arrFields[ipos++].Replace("'", ""); }
		internal static IfcQuantityTime Parse(string strDef, ReleaseVersion schema) { IfcQuantityTime q = new IfcQuantityTime(); int ipos = 0; parseFields(q, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return q; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mTimeValue.ToString() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
	}
	public partial class IfcQuantityVolume : IfcPhysicalSimpleQuantity
	{
		internal double mVolumeValue;// : IfcVolumeMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double VolumeValue { get { return mVolumeValue; } }
		public string Forumla { get { return mFormula == "$" ? "" : mFormula; } }

		internal IfcQuantityVolume() : base() { }
		internal IfcQuantityVolume(DatabaseIfc m, string name, string desc, IfcNamedUnit unit, double vol, string formula) : base(m, name, desc, unit) { mVolumeValue = vol; mFormula = formula; }
		internal static void parseFields(IfcQuantityVolume q, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcPhysicalSimpleQuantity.parseFields(q, arrFields, ref ipos);
			string str = arrFields[ipos++];
			double vol;
			if (double.TryParse(str, out vol))
				q.mVolumeValue = vol;
			else
			{
				IfcMeasureValue mv = ParserIfc.parseMeasureValue(str);
				if (mv != null)
					q.mVolumeValue = mv.Measure;
			}
			if (schema != ReleaseVersion.IFC2x3)
				q.mFormula = arrFields[ipos++].Replace("'", "");
		}
		internal static IfcQuantityVolume Parse(string strDef, ReleaseVersion schema) { IfcQuantityVolume q = new IfcQuantityVolume(); int ipos = 0; parseFields(q, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return q; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mVolumeValue) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
	}
	public partial class IfcQuantityWeight : IfcPhysicalSimpleQuantity
	{
		internal double mWeightValue;// : IfcWeightMeasure;	
		internal string mFormula = "$"; //:	OPTIONAL IfcLabel;  IFC4

		public double WeightValue { get { return mWeightValue; } }
		public string Forumla { get { return mFormula == "$" ? "" : mFormula; } }

		internal IfcQuantityWeight() : base() { }
		internal IfcQuantityWeight(DatabaseIfc m, string name, string desc, IfcNamedUnit unit, double weight, string formula) : base(m, name, desc, unit) { mWeightValue = weight; mFormula = formula; }
		internal static void parseFields(IfcQuantityWeight q, List<string> arrFields, ref int ipos, ReleaseVersion schema) { IfcPhysicalSimpleQuantity.parseFields(q, arrFields, ref ipos); q.mWeightValue = ParserSTEP.ParseDouble(arrFields[ipos++]); if (schema != ReleaseVersion.IFC2x3) q.mFormula = arrFields[ipos++].Replace("'", ""); }
		internal static IfcQuantityWeight Parse(string strDef, ReleaseVersion schema) { IfcQuantityWeight q = new IfcQuantityWeight(); int ipos = 0; parseFields(q, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return q; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mWeightValue) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
	}
}
