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
	public partial class IfcLibraryInformation : IfcExternalInformation
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Name = extractString(obj["Name"]);
			Version = extractString(obj["Version"]);
			//versiondate
			Location = extractString(obj["Location"]);
			Description = extractString(obj["Description"]);
			Publisher = extractObject<IfcActorSelect>(obj["Publisher"] as JsonObject);
			//				else if (string.Compare(name, "LibraryRefForObjects") == 0)
			//todo
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Name"] = Name;
			setAttribute(obj, "Version", Version);
			if (mPublisher != null)
				obj["Publisher"] = mPublisher.getJson(this, options);
			//VersionDate
			setAttribute(obj, "Location", Location);
			setAttribute(obj, "Description", Description);
		}
	}
	public partial class IfcLibraryReference : IfcExternalReference, IfcLibrarySelect
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Description = extractString(obj["Description"]);
			Language = extractString(obj["Language"]);
			JsonObject jobj = obj["ReferencedLibrary"] as JsonObject;
			if (jobj != null)
				ReferencedLibrary = mDatabase.ParseJsonObject<IfcLibraryInformation>(jobj);
			//				else if (string.Compare(name, "LibraryRefForObjects") == 0)
			//			{
			//todo
			//		}
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Language", Language);
			if (mReferencedLibrary != null)
				obj["ReferencedLibrary"] = ReferencedLibrary.getJson(this, options);
		}
	}
	public partial class IfcLine
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Pnt"] = Pnt.getJson(this, options);
			obj["Dir"] = Dir.getJson(this, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Pnt"] as JsonObject;
			if (jobj != null)
				Pnt = mDatabase.ParseJsonObject<IfcCartesianPoint>(jobj);
			jobj = obj["Dir"] as JsonObject;
			if (jobj != null)
				Dir = mDatabase.ParseJsonObject<IfcVector>(jobj);
		}
	}
	public partial class IfcLinearAxisWithInclination : IfcGeometricRepresentationItem
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Directrix"] = Directrix.getJson(this, options);
			obj["Inclinating"] = Inclinating.getJson(this, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Directrix"] as JsonObject;
			if (jobj != null)
				Directrix = mDatabase.ParseJsonObject<IfcCurve>(jobj);
			jobj = obj["Inclinating"] as JsonObject;
			if (jobj != null)
				Inclinating = mDatabase.ParseJsonObject<IfcAxisLateralInclination>(jobj);
		}
	}
	public partial class IfcLinearPositioningElement : IfcPositioningElement //IFC4.1
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Axis = mDatabase.ParseJsonObject<IfcCurve>(obj["Axis"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Axis"] = Axis.getJson(this, options);
		}
	}
	internal partial class IfcLinearSpanPlacement : IfcLinearPlacement
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Span"] = mSpan.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Span"];
			if (node != null)
				mSpan = node.GetValue<double>();
		}
	}
	public partial class IfcLiquidTerminal : IfcFlowTerminal
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcLiquidTerminalTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcLiquidTerminalTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcLiquidTerminalType : IfcFlowTerminalType
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcLiquidTerminalTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcLocalPlacement : IfcObjectPlacement
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			PlacementRelTo = mDatabase.ParseJsonObject<IfcObjectPlacement>(obj["PlacementRelTo"] as JsonObject);
			RelativePlacement = mDatabase.ParseJsonObject<IfcAxis2Placement>(obj["RelativePlacement"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RelativePlacement"] = mRelativePlacement.getJson(this, options);
		}
	}
	public partial class IfcLShapeProfileDef : IfcParameterizedProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Depth"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mDepth);
			node = obj["Width"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mWidth);
			node = obj["Thickness"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mThickness);
			node = obj["FilletRadius"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mFilletRadius);
			node = obj["EdgeRadius"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mEdgeRadius);
			node = obj["LegSlope"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mLegSlope);
			node = obj["CentreOfGravityInX"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mCentreOfGravityInX);
			node = obj["CentreOfGravityInY"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mCentreOfGravityInY);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			int digits = options.LengthDigitCount;
			base.setJSON(obj, host, options);
			obj["Depth"] = Math.Round(mDepth, digits);
			obj["Width"] = Math.Round(mWidth, digits);
			obj["Thickness"] = Math.Round(mThickness, digits);
			if (!double.IsNaN(mFilletRadius) && mFilletRadius > 0)
				obj["FilletRadius"] = Math.Round(mFilletRadius, digits);
			if (!double.IsNaN(mEdgeRadius) && mEdgeRadius > 0)
				obj["EdgeRadius"] = Math.Round(mEdgeRadius, digits);
			if (!double.IsNaN(mLegSlope) && mLegSlope > 0)
				obj["LegSlope"] = mLegSlope;
			if (options.Version <= ReleaseVersion.IFC2x3)
			{
				if (!double.IsNaN(mCentreOfGravityInX) && mCentreOfGravityInX > 0)
					obj["CentreOfGravityInX"] = mCentreOfGravityInX;
				if (!double.IsNaN(mCentreOfGravityInY) && mCentreOfGravityInY > 0)
					obj["CentreOfGravityInY"] = mCentreOfGravityInY;
			}
		}
	}
}
#endif
