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

#if (NET || !NOIFCJSON)
#if (NEWTONSOFT)
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonObject = Newtonsoft.Json.Linq.JObject;
using JsonArray = Newtonsoft.Json.Linq.JArray;
#else
using System.Text.Json.Nodes;
#endif

namespace GeometryGym.Ifc
{
	public partial class IfcIndexedPolyCurve
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Points"] as JsonObject;
			if (jobj != null)
				Points = mDatabase.ParseJsonObject<IfcCartesianPointList>(jobj);
			JsonArray array = obj["Segments"] as JsonArray;
			if (array != null)
			{
				foreach (var token in array)
				{
					JsonObject ob = token as JsonObject;
					if (ob != null)
					{
						var jtoken = ob["IfcLineIndex"];
						if (jtoken != null)
							mSegments.Add(new IfcLineIndex(jtoken.GetValue<string>().Split(' ').Select(x => int.Parse(x))));
						else
						{
							jtoken = ob["IfcArcIndex"];
							if (jtoken != null)
							{
								List<int> tokens = jtoken.GetValue<string>().Split(' ').Select(x => int.Parse(x)).ToList();
								mSegments.Add(new IfcArcIndex(tokens[0], tokens[1], tokens[2]));
							}
						}
					}
				}
			}
			var node = obj["SelfIntersect"];
			if (node != null)
				Enum.TryParse<IfcLogicalEnum>(node.GetValue<string>(), true, out mSelfIntersect);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Points"] = Points.getJson(this, options);
			if (mSegments.Count > 0)
			{
				JsonArray array = new JsonArray();
				obj["Segments"] = array;
				foreach (IfcSegmentIndexSelect seg in Segments)
				{
					IfcArcIndex ai = seg as IfcArcIndex;
					JsonObject jobj = new JsonObject();
					if (ai != null)
					{
						jobj["IfcArcIndex"] = ai[0] + " " + ai[1] + " " + ai[2];
					}
					else
					{
						IfcLineIndex li = seg as IfcLineIndex;
						jobj["IfcLineIndex"] = string.Join(" ", li.ConvertAll(x => x.ToString()));
					}
					array.Add(jobj);
				}
			}
			if (mSelfIntersect != IfcLogicalEnum.UNKNOWN)
				obj["SelfIntersect"] = mSelfIntersect.ToString();
		}
	}

	public partial class IfcIShapeProfileDef 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mOverallWidth = extractDouble(obj["OverallWidth"]);
			mOverallDepth = extractDouble(obj["OverallDepth"]);
			mWebThickness = extractDouble(obj["WebThickness"]);
			mFlangeThickness = extractDouble(obj["FlangeThickness"]);
			mFilletRadius = extractDouble(obj["FilletRadius"]);
			mFlangeEdgeRadius = extractDouble(obj["FlangeEdgeRadius"]);
			mFlangeSlope = extractDouble(obj["FlangeSlope"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["OverallWidth"] = formatLength(mOverallWidth);
			obj["OverallDepth"] = formatLength(mOverallDepth);
			obj["WebThickness"] = formatLength(mWebThickness);
			obj["FlangeThickness"] = formatLength(mFlangeThickness);
			if (!double.IsNaN(mFilletRadius) && mFilletRadius > 0)
				obj["FilletRadius"] = formatLength(mFilletRadius);
			if (!double.IsNaN(mFlangeEdgeRadius) && mFlangeEdgeRadius > 0)
				obj["FlangeEdgeRadius"] = formatLength(mFlangeEdgeRadius);
			if (!double.IsNaN(mFlangeSlope) && mFlangeSlope > 0)
				obj["FlangeSlope"] = formatLength(mFlangeSlope);
		}
	}
}
#endif
