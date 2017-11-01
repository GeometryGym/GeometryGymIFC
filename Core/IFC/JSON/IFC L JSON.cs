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

using Newtonsoft.Json.Linq;

namespace GeometryGym.Ifc
{
	public partial class IfcLibraryInformation : IfcExternalInformation
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Name = extractString(obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase));
			Version = extractString(obj.GetValue("Version", StringComparison.InvariantCultureIgnoreCase));
			//versiondate
			Location = extractString(obj.GetValue("Location", StringComparison.InvariantCultureIgnoreCase));
			Description = extractString(obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase));
			Publisher = extractObject<IfcActorSelect>(obj.GetValue("Publisher", StringComparison.InvariantCultureIgnoreCase) as JObject);
//				else if (string.Compare(name, "LibraryRefForObjects") == 0)
//todo
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Name"] = Name;
			setAttribute(obj, "Version", Version);
			if (mPublisher > 0)
				obj["Publisher"] = mDatabase[mPublisher].getJson(this, options);
			//VersionDate
			setAttribute(obj, "Location", Location);
			setAttribute(obj, "Description", Description);
		}
	}
	public partial class IfcLibraryReference : IfcExternalReference, IfcLibrarySelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Description = extractString(obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase));
			Language = extractString(obj.GetValue("Language", StringComparison.InvariantCultureIgnoreCase));
			JObject jobj = obj.GetValue("ReferencedLibrary", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				ReferencedLibrary = mDatabase.parseJObject< IfcLibraryInformation> (jobj); 
//				else if (string.Compare(name, "LibraryRefForObjects") == 0)
	//			{
					//todo
		//		}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Language", Language);
			if (mReferencedLibrary > 0)
				obj["ReferencedLibrary"] = ReferencedLibrary.getJson(this, options);
		}
	}
	public partial class IfcLocalPlacement : IfcObjectPlacement
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PlacementRelTo", StringComparison.InvariantCultureIgnoreCase) as JToken;
			if (token != null)
			{
				JObject jobj = token as JObject;
				if (jobj != null)
					PlacementRelTo = mDatabase.parseJObject<IfcObjectPlacement>(jobj);
				else
					mPlacementRelTo = token.Value<int>();
			}
			JObject rp = obj.GetValue("RelativePlacement", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				RelativePlacement = mDatabase.parseJObject<IfcAxis2Placement>(rp);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			
			if(mPlacementRelTo > 0)
				obj["PlacementRelTo"] = PlacementRelTo.getJson(this, options);
			obj["RelativePlacement"] = mDatabase[mRelativePlacement].getJson(this, options);
		}
	}
}
