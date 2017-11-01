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
	public partial class IfcQuantityArea : IfcPhysicalSimpleQuantity
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("AreaValue", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				AreaValue = token.Value<double>();
			token = obj.GetValue("Formula", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Formula = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["AreaValue"] = AreaValue;
			base.setAttribute(obj, "Formula", Formula);
		}

	}
	public partial class IfcQuantityCount : IfcPhysicalSimpleQuantity
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("CountValue", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				CountValue = token.Value<double>();
			token = obj.GetValue("Formula", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Formula = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["CountValue"] = CountValue;
			base.setAttribute(obj, "Formula", Formula);
		}

	}
	public partial class IfcQuantityLength : IfcPhysicalSimpleQuantity
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("LengthValue", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				LengthValue = token.Value<double>();
			token = obj.GetValue("Formula", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Formula = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["LengthValue"] = LengthValue;
			base.setAttribute(obj, "Formula", Formula);
		}
	}
	public partial class IfcQuantityTime : IfcPhysicalSimpleQuantity
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("TimeValue", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				TimeValue = token.Value<double>();
			token = obj.GetValue("Formula", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Formula = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["TimeValue"] = TimeValue;
			base.setAttribute(obj, "Formula", Formula);
		}

	}
	public partial class IfcQuantityVolume : IfcPhysicalSimpleQuantity
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("VolumeValue", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				VolumeValue = token.Value<double>();
			token = obj.GetValue("Formula", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Formula = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["VolumeValue"] = VolumeValue;
			base.setAttribute(obj, "Formula", Formula);
		}

	}
	public partial class IfcQuantityWeight : IfcPhysicalSimpleQuantity
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("WeightValue", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				WeightValue = token.Value<double>();
			token = obj.GetValue("Formula", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Formula = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["WeightValue"] = WeightValue;
			base.setAttribute(obj, "Formula", Formula);
		}

	}
}
