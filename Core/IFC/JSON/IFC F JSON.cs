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
	public partial class IfcFace : IfcTopologicalRepresentationItem //	SUPERTYPE OF(IfcFaceSurface)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Bounds.AddRange(mDatabase.extractJArray<IfcFaceBound>(obj.GetValue("Bounds", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Bounds"] = new JArray(Bounds.ConvertAll(x=> x.getJson(this, options)));
		}
	}
	public partial class IfcFaceBasedSurfaceModel : IfcGeometricRepresentationItem, IfcSurfaceOrFaceSurface
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mDatabase.extractJArray<IfcConnectedFaceSet>(obj.GetValue("FbsmFaces", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addFace(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["FbsmFaces"] = new JArray(mFbsmFaces.ConvertAll(x => mDatabase[x].getJson(this, options)));
		}
	}
	public partial class IfcFaceBound : IfcTopologicalRepresentationItem //SUPERTYPE OF (ONEOF (IfcFaceOuterBound))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Bound", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Bound = extractObject<IfcLoop>(jobj);
			JToken token = obj.GetValue("Orientation", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Orientation = token.Value<bool>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Bound"] = Bound.getJson(this, options);
			obj["Orientation"] = Orientation;
		}
	}
	public partial class IfcFacility : IfcSpatialStructureElement
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("ElevationOfRefHeight", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ElevationOfRefHeight = token.Value<double>();
			token = obj.GetValue("ElevationOfTerrain", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ElevationOfTerrain = token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (!double.IsNaN(mElevationOfRefHeight))
				obj["ElevationOfRefHeight"] = ElevationOfRefHeight.ToString();
			if (!double.IsNaN(mElevationOfTerrain))
				obj["ElevationOfTerrain"] = ElevationOfTerrain.ToString();
		}
	}
}
