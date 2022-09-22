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

#if (!NOIFCJSON)
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
	public partial class IfcQuantityArea : IfcPhysicalSimpleQuantity
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["AreaValue"];
			if (node != null)
				AreaValue = node.GetValue<double>();
			node = obj["Formula"];
			if (node != null)
				Formula = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["AreaValue"] = AreaValue;
			base.setAttribute(obj, "Formula", Formula);
		}

	}
	public partial class IfcQuantityCount : IfcPhysicalSimpleQuantity
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["CountValue"];
			if (node != null)
				CountValueDouble = node.GetValue<double>();
			node = obj["Formula"];
			if (node != null)
				Formula = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["CountValue"] = CountValue;
			base.setAttribute(obj, "Formula", Formula);
		}

	}
	public partial class IfcQuantityLength : IfcPhysicalSimpleQuantity
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["LengthValue"];
			if (node != null)
				LengthValue = node.GetValue<double>();
			node = obj["Formula"];
			if (node != null)
				Formula = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["LengthValue"] = LengthValue;
			base.setAttribute(obj, "Formula", Formula);
		}
	}
	public partial class IfcQuantityTime : IfcPhysicalSimpleQuantity
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["TimeValue"];
			if (node != null)
				TimeValue = node.GetValue<double>();
			node = obj["Formula"];
			if (node != null)
				Formula = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["TimeValue"] = TimeValue;
			base.setAttribute(obj, "Formula", Formula);
		}

	}
	public partial class IfcQuantityVolume : IfcPhysicalSimpleQuantity
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["VolumeValue"];
			if (node != null)
				VolumeValue = node.GetValue<double>();
			node = obj["Formula"];
			if (node != null)
				Formula = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["VolumeValue"] = VolumeValue;
			base.setAttribute(obj, "Formula", Formula);
		}

	}
	public partial class IfcQuantityWeight : IfcPhysicalSimpleQuantity
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["WeightValue"];
			if (node != null)
				WeightValue = node.GetValue<double>();
			node = obj["Formula"];
			if (node != null)
				Formula = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["WeightValue"] = WeightValue;
			base.setAttribute(obj, "Formula", Formula);
		}

	}
}
#endif
