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
	public partial class IfcIndexedPolyCurve : IfcBoundedCurve
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Points", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Points = mDatabase.parseJObject<IfcCartesianPointList>(jobj);
			JArray array = obj.GetValue("Segments", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
			{
				foreach (JToken tok in array)
				{
					JObject ob = tok as JObject;
					if (ob != null)
					{
						JToken jtoken = ob.GetValue("IfcLineIndex", StringComparison.InvariantCultureIgnoreCase);
						if (jtoken != null)
							mSegments.Add(new IfcLineIndex(jtoken.Value<string>().Split(" ".ToCharArray()).ToList().ConvertAll(x => int.Parse(x))));
						else
						{
							jtoken = ob.GetValue("IfcArcIndex", StringComparison.InvariantCultureIgnoreCase);
							if (jtoken != null)
							{
								List<int> tokens = jtoken.Value<string>().Split(" ".ToCharArray()).ToList().ConvertAll(x => int.Parse(x));
								mSegments.Add(new IfcArcIndex(tokens[0], tokens[1], tokens[2]));
							}
						}
					}
				}
			}
			JToken token = obj.GetValue("SelfIntersect", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcLogicalEnum>(token.Value<string>(), true, out mSelfIntersect);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Points"] = Points.getJson(this, options);
			if (mSegments.Count > 0)
			{
				JArray array = new JArray();
				obj["Segments"] = array;
				foreach (IfcSegmentIndexSelect seg in Segments)
				{
					IfcArcIndex ai = seg as IfcArcIndex;
					JObject jobj = new JObject();
					if (ai != null)
					{
						jobj["IfcArcIndex"] = ai.mA + " " + ai.mB + " " + ai.mC;
					}
					else
					{
						IfcLineIndex li = seg as IfcLineIndex;
						jobj["IfcLineIndex"] = string.Join(" ", li.mIndices.ConvertAll(x => x.ToString()));
					}
					array.Add(jobj);
				}
			}
			if (mSelfIntersect != IfcLogicalEnum.UNKNOWN)
				obj["SelfIntersect"] = mSelfIntersect.ToString();
		}
	}

	public partial class IfcIShapeProfileDef : IfcParameterizedProfileDef // Ifc2x3 SUPERTYPE OF	(IfcAsymmetricIShapeProfileDef) 
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("OverallWidth", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mOverallWidth);
			token = obj.GetValue("OverallDepth", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mOverallDepth);
			token = obj.GetValue("WebThickness", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mWebThickness);
			token = obj.GetValue("FlangeThickness", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mFlangeThickness);
			token = obj.GetValue("FilletRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mFilletRadius);
			token = obj.GetValue("FlangeEdgeRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mFlangeEdgeRadius);
			token = obj.GetValue("FlangeSlope", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mFlangeSlope);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			int digits = mDatabase == null ? 4 : mDatabase.mLengthDigits;
			base.setJSON(obj, host, options);
			obj["OverallWidth"] = Math.Round(mOverallWidth, digits);
			obj["OverallDepth"] = Math.Round(mOverallDepth, digits);
			obj["WebThickness"] = Math.Round(mWebThickness, digits);
			obj["FlangeThickness"] = Math.Round(mFlangeThickness, digits);
			if (!double.IsNaN(mFilletRadius) && mFilletRadius > 0)
				obj["FilletRadius"] = Math.Round(mFilletRadius, digits);
			if (!double.IsNaN(mFlangeEdgeRadius) && mFlangeEdgeRadius > 0)
				obj["FlangeEdgeRadius"] = Math.Round(mFlangeEdgeRadius, digits);
			if (!double.IsNaN(mFlangeSlope) && mFlangeSlope > 0)
				obj["FlangeSlope"] = mFlangeSlope;
		}
	}
}
