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
	public class IfcZone : IfcSystem
	{
		internal string mLongName = "$";// :	OPTIONAL IfcLabel; IFC4
		public string LongName { get { return (mLongName == "$" ? "" : ParserIfc.Decode(mLongName)); } set { mLongName = (string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value.Replace("'", ""))); } }

		internal IfcZone() : base() { }
		internal IfcZone(IfcZone z) : base(z) { mLongName = z.mLongName; }
		internal IfcZone(DatabaseIfc m, string name) : base(m, name) { }
		internal IfcZone(IfcSpatialElement e, string name, string longname, List<IfcSpace> spaces) : base(e, name)
		{
			if (spaces != null)
				mIsGroupedBy[0].mRelatedObjects.AddRange(spaces.ConvertAll(x => x.mIndex));
		}
		internal new static IfcZone Parse(string strDef) { IfcZone z = new IfcZone(); int ipos = 0; parseFields(z, ParserSTEP.SplitLineFields(strDef), ref ipos); return z; }
		internal static void parseFields(IfcZone z, List<string> arrFields, ref int ipos) { IfcGroup.parseFields(z, arrFields, ref ipos); }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mLongName == "$" ? ",$" : ",'" + mLongName + "'")); }
	}
	public partial class IfcZShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mDepth;// : IfcPositiveLengthMeasure;
		internal double mFlangeWidth;// : IfcPositiveLengthMeasure;
		internal double mWebThickness;// : IfcPositiveLengthMeasure;
		internal double mFlangeThickness;// : IfcPositiveLengthMeasure;
		internal double mFilletRadius;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mEdgeRadius;// : OPTIONAL IfcPositiveLengthMeasure; 
		internal IfcZShapeProfileDef() : base() { }
		internal IfcZShapeProfileDef(IfcZShapeProfileDef i) : base(i) { mDepth = i.mDepth; mFlangeWidth = i.mFlangeWidth; mWebThickness = i.mWebThickness; mFlangeThickness = i.mFlangeThickness; mFilletRadius = i.mFilletRadius; mEdgeRadius = i.mEdgeRadius; }
		
		internal new static IfcZShapeProfileDef Parse(string strDef) { IfcZShapeProfileDef p = new IfcZShapeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcZShapeProfileDef p, List<string> arrFields, ref int ipos)
		{
			IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos);
			p.mDepth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFlangeWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mWebThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFlangeThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mEdgeRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
	}
}
