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
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public partial class IfcZone : IfcSystem
	{
		internal string mLongName = "$";// :	OPTIONAL IfcLabel; IFC4
		public string LongName { get { return (mLongName == "$" ? "" : ParserIfc.Decode(mLongName)); } set { mLongName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcZone() : base() { }
		internal IfcZone(DatabaseIfc db, IfcZone z, IfcOwnerHistory ownerHistory, bool downStream) : base(db, z, ownerHistory, downStream) { mLongName = z.mLongName; }
		internal IfcZone(DatabaseIfc m, string name) : base(m, name) { }
		public IfcZone(IfcSpatialElement e, string name, List<IfcSpace> spaces) : base(e, name)
		{
			if (spaces != null && spaces.Count > 0)
			{
				new IfcRelAssignsToGroup(spaces, this);
			}
		}
	}
	[Serializable]
	public partial class IfcZShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mDepth;// : IfcPositiveLengthMeasure;
		internal double mFlangeWidth;// : IfcPositiveLengthMeasure;
		internal double mWebThickness;// : IfcPositiveLengthMeasure;
		internal double mFlangeThickness;// : IfcPositiveLengthMeasure;
		internal double mFilletRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mEdgeRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure; 

		internal IfcZShapeProfileDef() : base() { }
		internal IfcZShapeProfileDef(DatabaseIfc db, IfcZShapeProfileDef p) : base(db, p) { mDepth = p.mDepth; mFlangeWidth = p.mFlangeWidth; mWebThickness = p.mWebThickness; mFlangeThickness = p.mFlangeThickness; mFilletRadius = p.mFilletRadius; mEdgeRadius = p.mEdgeRadius; }
		public IfcZShapeProfileDef(DatabaseIfc db, string name, double depth, double flangeWidth, double webThickness, double flangeThickness) : base(db, name) { mDepth = depth; mFlangeWidth = flangeWidth; mWebThickness = webThickness; mFlangeThickness = flangeThickness; }
	}
}
