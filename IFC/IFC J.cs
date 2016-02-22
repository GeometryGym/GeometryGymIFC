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
	public class IfcJunctionBox : IfcFlowFitting //IFC4
	{
		internal IfcJunctionBoxTypeEnum mPredefinedType = IfcJunctionBoxTypeEnum.NOTDEFINED;// OPTIONAL : IfcJunctionBoxTypeEnum;
		public IfcJunctionBoxTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcJunctionBox() : base() { }
		internal IfcJunctionBox(IfcJunctionBox b) : base(b) { mPredefinedType = b.mPredefinedType; }
		internal IfcJunctionBox(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcJunctionBox s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcJunctionBoxTypeEnum)Enum.Parse(typeof(IfcJunctionBoxTypeEnum), str);
		}
		internal new static IfcJunctionBox Parse(string strDef) { IfcJunctionBox s = new IfcJunctionBox(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcJunctionBoxTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	internal class IfcJunctionBoxType : IfcFlowFittingType
	{
		internal IfcJunctionBoxTypeEnum mPredefinedType = IfcJunctionBoxTypeEnum.NOTDEFINED;// : IfcJunctionBoxTypeEnum;
		public IfcJunctionBoxTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcJunctionBoxType() : base() { }
		internal IfcJunctionBoxType(IfcJunctionBoxType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcJunctionBoxType(DatabaseIfc m, string name, IfcJunctionBoxTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcJunctionBoxType t, List<string> arrFields, ref int ipos) { IfcFlowFittingType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcJunctionBoxTypeEnum)Enum.Parse(typeof(IfcJunctionBoxTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcJunctionBoxType Parse(string strDef) { IfcJunctionBoxType t = new IfcJunctionBoxType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
}
