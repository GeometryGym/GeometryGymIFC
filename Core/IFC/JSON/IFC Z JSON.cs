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

using Newtonsoft.Json.Linq;

namespace GeometryGym.Ifc
{
	public partial class IfcZShapeProfileDef : IfcParameterizedProfileDef
	{
		//internal double mDepth;// : IfcPositiveLengthMeasure;
		//internal double mFlangeWidth;// : IfcPositiveLengthMeasure;
		//internal double mWebThickness;// : IfcPositiveLengthMeasure;
		//internal double mFlangeThickness;// : IfcPositiveLengthMeasure;
		//internal double mFilletRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		//internal double mEdgeRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure; 

		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Depth", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mDepth);
			token = obj.GetValue("FlangeWidth", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mFlangeWidth);
			token = obj.GetValue("WebThickness", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mWebThickness);
			token = obj.GetValue("FlangeThickness", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mFlangeThickness);
			token = obj.GetValue("FilletRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mFilletRadius);
			token = obj.GetValue("EdgeRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mEdgeRadius);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			int digits = mDatabase.mLengthDigits;
			base.setJSON(obj, host, options);
			obj["Depth"] = Math.Round(mDepth, digits);
			obj["FlangeWidth"] = Math.Round(mFlangeWidth, digits);
			obj["WebThickness"] = Math.Round(mWebThickness, digits);
			obj["FlangeThickness"] = Math.Round(mFlangeThickness, digits);
			if (!double.IsNaN(mFilletRadius) && mFilletRadius > 0)
				obj["FilletRadius"] = Math.Round(mFilletRadius, digits);
			if (!double.IsNaN(mEdgeRadius) && mEdgeRadius > 0)
				obj["FlangeEdgeRadius"] = Math.Round(mEdgeRadius, digits);
		}
	}
}
