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
			if (mPublisher != null)
				obj["Publisher"] = mPublisher.getJson(this, options);
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
				ReferencedLibrary = mDatabase.ParseJObject<IfcLibraryInformation>(jobj);
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
			if (mReferencedLibrary != null)
				obj["ReferencedLibrary"] = ReferencedLibrary.getJson(this, options);
		}
	}
	public partial class IfcLine
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Pnt"] = Pnt.getJson(this, options);
			obj["Dir"] = Dir.getJson(this, options);
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Pnt", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Pnt = mDatabase.ParseJObject<IfcCartesianPoint>(jobj);
			jobj = obj.GetValue("Dir", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Dir = mDatabase.ParseJObject<IfcVector>(jobj);
		}
	}
	public partial class IfcLinearAxisWithInclination : IfcGeometricRepresentationItem
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Directrix"] = Directrix.getJson(this, options);
			obj["Inclinating"] = Inclinating.getJson(this, options);
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Directrix", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Directrix = mDatabase.ParseJObject<IfcCurve>(jobj);
			jobj = obj.GetValue("Inclinating", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Inclinating = mDatabase.ParseJObject<IfcAxisLateralInclination>(jobj);
		}
	}
	public partial class IfcLinearPositioningElement : IfcPositioningElement //IFC4.1
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Axis", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Axis = mDatabase.ParseJObject<IfcCurve>(token as JObject);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Axis"] = Axis.getJson(this, options);
		}
	}
	internal partial class IfcLinearSpanPlacement : IfcLinearPlacement
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Span"] = mSpan.ToString();
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken span = obj.GetValue("Span", StringComparison.InvariantCultureIgnoreCase);
			if (span != null)
				mSpan = span.Value<double>();
		}
	}
	public partial class IfcLiquidTerminal : IfcFlowTerminal
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcLiquidTerminalTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcLiquidTerminalTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcLiquidTerminalType : IfcFlowTerminalType
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcLiquidTerminalTypeEnum>(token.Value<string>(), true, out mPredefinedType);
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
					PlacementRelTo = mDatabase.ParseJObject<IfcObjectPlacement>(jobj);
			}
			JObject rp = obj.GetValue("RelativePlacement", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (rp != null)
				RelativePlacement = mDatabase.ParseJObject<IfcAxis2Placement>(rp);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["RelativePlacement"] = mRelativePlacement.getJson(this, options);
		}
	}
	public partial class IfcLShapeProfileDef : IfcParameterizedProfileDef
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Depth", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mDepth);
			token = obj.GetValue("Width", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mWidth);
			token = obj.GetValue("Thickness", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mThickness);
			token = obj.GetValue("FilletRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mFilletRadius);
			token = obj.GetValue("EdgeRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mEdgeRadius);
			token = obj.GetValue("LegSlope", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mLegSlope);
			token = obj.GetValue("CentreOfGravityInX", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mCentreOfGravityInX);
			token = obj.GetValue("CentreOfGravityInY", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mCentreOfGravityInY);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
