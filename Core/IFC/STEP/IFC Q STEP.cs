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
using System.Collections.Concurrent;
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
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mAreaValue) + (release < ReleaseVersion.IFC4 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (!double.TryParse(s, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out mAreaValue))
			{
				if (s.StartsWith("IFCAREAMEASURE"))
				{
					s = s.Substring(15, s.Length - 16);
					double.TryParse(s, out mAreaValue);
				}
			}
			if (release != ReleaseVersion.IFC2x3)
				mFormula = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcQuantityCount : IfcPhysicalSimpleQuantity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mCountValue) + (release < ReleaseVersion.IFC4 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (!double.TryParse(s, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out mCountValue))
			{
				if (s.StartsWith("IFCCOUNTMEASURE"))
				{
					s = s.Substring(16, s.Length - 17);
					double.TryParse(s, out mCountValue);
				}
			}
			if (release != ReleaseVersion.IFC2x3)
				mFormula = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcQuantityLength : IfcPhysicalSimpleQuantity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(Math.Max(0, mLengthValue)) + (release < ReleaseVersion.IFC4 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (!double.TryParse(s, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out mLengthValue))
			{
				if (s.StartsWith("IFCLENGTHMEASURE"))
				{
					s = s.Substring(17, s.Length - 18);
					double.TryParse(s, out mLengthValue);
				}
			}
			if (release != ReleaseVersion.IFC2x3)
				mFormula = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	
	public partial class IfcQuantityTime : IfcPhysicalSimpleQuantity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString( mTimeValue) + (release < ReleaseVersion.IFC4 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			double.TryParse(ParserSTEP.StripField(str, ref pos, len), out mTimeValue);
			if (release != ReleaseVersion.IFC2x3)
				mFormula = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcQuantityVolume : IfcPhysicalSimpleQuantity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mVolumeValue) + (release < ReleaseVersion.IFC4 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (!double.TryParse(s, out mVolumeValue))
			{
				IfcMeasureValue mv = ParserIfc.parseMeasureValue(s);
				if (mv != null)
					mVolumeValue = mv.Measure;
			}
			if (release != ReleaseVersion.IFC2x3)
				mFormula = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcQuantityWeight : IfcPhysicalSimpleQuantity
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mWeightValue) + (release < ReleaseVersion.IFC4 ? "" : (mFormula == "$" ? ",$" : ",'" + mFormula + "'")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (!double.TryParse(s, out mWeightValue))
			{
				IfcMeasureValue mv = ParserIfc.parseMeasureValue(s);
				if (mv != null)
					mWeightValue = mv.Measure;
			}
			if (release != ReleaseVersion.IFC2x3)
				mFormula = ParserSTEP.StripString(str, ref pos, len);
		}
	}
}
