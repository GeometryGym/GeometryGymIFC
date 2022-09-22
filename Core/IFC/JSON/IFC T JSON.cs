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
	public abstract partial class IfcTessellatedFaceSet : IfcTessellatedItem, IfcBooleanOperand //ABSTRACT SUPERTYPE OF(IfcTriangulatedFaceSet)
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Coordinates"] = Coordinates.getJson(this, options);
			if (mHasColours != null)
				obj["HasColours"] = mHasColours.getJson(this, options);
			if (mHasTextures.Count > 0)
			{
				JsonArray array = new JsonArray(mHasTextures.Count);
				foreach (IfcIndexedTextureMap tm in HasTextures)
					array.Add(tm.getJson(this, options));
				obj["HasTextures"] = array;
			}
		}
	}
	public partial class IfcThirdOrderPolynomialSpiral
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["QubicTerm"] = mQubicTerm.ToString();
			if (double.IsNaN(mQuadraticTerm))
				obj["QuadraticTerm"] = mQuadraticTerm.ToString();
			if (double.IsNaN(mLinearTerm))
				obj["Radius"] = mLinearTerm.ToString();
			if (double.IsNaN(mConstantTerm))
				obj["ConstantTerm"] = mConstantTerm.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["QubicTerm"];
			if (node != null)
				mQubicTerm = node.GetValue<double>();
			node = obj["QuadraticTerm"];
			if (node != null)
				mQuadraticTerm = node.GetValue<double>();
			node = obj["LinearTerm"];
			if (node != null)
				mLinearTerm = node.GetValue<double>();
			node = obj["ConstantTerm"];
			if (node != null)
				mConstantTerm = node.GetValue<double>();
		}
	}
	public partial class IfcTrackElement : IfcBuiltElement
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcTrackElementTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcTrackElementTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcTrackElementType : IfcBuiltElementType
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
				Enum.TryParse<IfcTrackElementTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcTransitionCurveSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["StartRadius"];
			if (node != null)
				StartRadius = node.GetValue<double>();
			node = obj["EndRadius"];
			if (node != null)
				EndRadius = node.GetValue<double>();
			node = obj["IsStartRadiusCCW"];
			if (node != null)
				IsStartRadiusCCW = node.GetValue<bool>();
			node = obj["IsEndRadiusCCW"];
			if (node != null)
				IsEndRadiusCCW = node.GetValue<bool>();
			node = obj["TransitionCurveType"];
			if (node != null)
				Enum.TryParse<IfcTransitionCurveType>(node.GetValue<string>(), true, out mTransitionCurveType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["StartRadius"] = StartRadius;
			obj["EndRadius"] = EndRadius;
			obj["IsStartRadiusCCW"] = IsStartRadiusCCW;
			obj["IsEndRadiusCCW"] = IsEndRadiusCCW;
			obj["TransitionCurveType"] = mTransitionCurveType.ToString();
		}
	}
	public partial class IfcTranslationalStiffnessSelect //SELECT ( IfcBoolean, IfcLinearStiffnessMeasure); 
	{
		internal static IfcTranslationalStiffnessSelect parseJsonObject(JsonObject obj)
		{
			JsonObject jobj = obj["IfcBoolean"] as JsonObject;
			if (jobj != null)
				return new IfcTranslationalStiffnessSelect(jobj.GetValue<bool>());
			jobj = obj["IfcLinearStiffnessMeasure"] as JsonObject;
			return (jobj != null ? new IfcTranslationalStiffnessSelect(jobj.GetValue<double>()) : null);
		}
		internal JsonObject getJsonObject()
		{
			JsonObject obj = new JsonObject();
			if (mStiffness != null)
				obj["IfcLinearStiffnessMeasure"] = mStiffness.Measure;
			else
				obj["IfcBoolean"] = mRigid;
			return obj;
		}
	}
	public partial class IfcTrapeziumProfileDef : IfcParameterizedProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["BottomXDim"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mBottomXDim);
			node = obj["TopXDim"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mTopXDim);
			node = obj["YDim"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mYDim);
			node = obj["TopXOffset"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mTopXOffset);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["BottomXDim"] = formatLength(mBottomXDim);
			obj["TopXDim"] = formatLength(mTopXDim);
			obj["YDim"] = formatLength(mYDim);
			obj["TopXOffset"] = formatLength(mTopXOffset);
		}
	}
	//public partial class IfcTriangulatedFaceSet : IfcTessellatedFaceSet
	//{
	//	protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
	//	{
	//		base.setJSON(obj, host, options);

	//		if (mNormals.Length > 0)
	//		{ // spaced list of numbers
	//			JsonArray array = new JsonArray() { };
	//			foreach (uple<double, double, double> normal in Normals)
	//			{
	//				JsonArray norm = new JsonArray() { };
	//				norm.Add(normal.Item1);
	//				norm.Add(normal.Item2);
	//				norm.Add(normal.Item3);
	//				array.Add(norm);
	//			}
	//			obj["Normals"] = array;
	//		}
	//		obj["Closed"] = Closed;
	//		JsonArray arr = new JsonArray();
	//		foreach (Tuple<int, int, int> coord in mCoordIndex)
	//		{
	//			JsonArray c = new JsonArray();
	//			c.Add(coord.Item1);
	//			c.Add(coord.Item2);
	//			c.Add(coord.Item3);
	//			arr.Add(c);
	//		}
	//		obj["CoordIndex"] = arr;
	//		if (mNormalIndex.Length > 0)
	//		{
	//			arr = new JsonArray();
	//			foreach (Tuple<int, int, int> norm in mNormalIndex)
	//			{
	//				JsonArray n = new JsonArray();
	//				n.Add(norm.Item1);
	//				n.Add(norm.Item2);
	//				n.Add(norm.Item3);
	//				arr.Add(n);
	//			}
	//			obj["NormalIndex"] = arr;

	//		}
	//	}
	//}
	public partial class IfcTrimmedCurve : IfcBoundedCurve
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["BasisCurve"] = BasisCurve.getJson(this, options);
			obj["Trim1"] = Trim1.getJSON(mDatabase);
			obj["Trim2"] = Trim2.getJSON(mDatabase);
			obj["SenseAgreement"] = mSenseAgreement;
			obj["MasterRepresentation"] = mMasterRepresentation.ToString();
		}
	}
	public partial class IfcTrimmingSelect
	{
		internal JsonArray getJSON(DatabaseIfc db)
		{
			JsonArray result = new JsonArray();
			if (!double.IsNaN(ParameterValue))
				result.Add(DatabaseIfc.extract(new IfcParameterValue(ParameterValue)));
			if (CartesianPoint != null)
				result.Add(CartesianPoint.getJson(null, new BaseClassIfc.SetJsonOptions()));
			return result;
		}
	}
	public partial class IfcTShapeProfileDef : IfcParameterizedProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Depth"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mDepth);
			node = obj["FlangeWidth"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mFlangeWidth);
			node = obj["WebThickness"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mWebThickness);
			node = obj["FlangeThickness"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mFlangeThickness);
			node = obj["FilletRadius"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mFilletRadius);
			node = obj["FlangeEdgeRadius"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mFlangeEdgeRadius);
			node = obj["WebEdgeRadius"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mWebEdgeRadius);
			node = obj["WebSlope"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mWebSlope);
			node = obj["FlangeSlope"];
			if (node != null)
				double.TryParse(node.GetValue<string>(), out mFlangeSlope);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Depth"] = formatLength(mDepth);
			obj["FlangeWidth"] = formatLength(mFlangeWidth);
			obj["WebThickness"] = formatLength(mWebThickness);
			obj["FlangeThickness"] = formatLength(mFlangeThickness);
			if (!double.IsNaN(mFilletRadius) && mFilletRadius > 0)
				obj["FilletRadius"] = formatLength(mFilletRadius);
			if (!double.IsNaN(mFlangeEdgeRadius) && mFlangeEdgeRadius > 0)
				obj["FlangeEdgeRadius"] = formatLength(mFlangeEdgeRadius);
			if (!double.IsNaN(mWebEdgeRadius) && mWebEdgeRadius > 0)
				obj["WebEdgeRadius"] = formatLength(mFlangeEdgeRadius);
			if (!double.IsNaN(mWebSlope) && mWebSlope > 0)
				obj["WebSlope"] = mWebSlope;
			if (!double.IsNaN(mFlangeSlope) && mFlangeSlope > 0)
				obj["FlangeSlope"] = formatLength(mFlangeSlope);
		}
	}
	public partial class IfcTypeObject : IfcObjectDefinition //(IfcTypeProcess, IfcTypeProduct, IfcTypeResource) IFC4 ABSTRACT 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			ApplicableOccurrence = extractString(obj["ApplicableOccurrence"]);
			HasPropertySets.AddRange(mDatabase.extractJsonArray<IfcPropertySetDefinition>(obj["HasPropertySets"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "ApplicableOccurrence", ApplicableOccurrence);
			createArray(obj, "HasPropertySets", HasPropertySets, this, options);
		}
	}
	public partial class IfcTypeProduct : IfcTypeObject, IfcProductSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcDoorStyle ,IfcElementType ,IfcSpatialElementType ,IfcWindowStyle)) 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			RepresentationMaps.AddRange(mDatabase.extractJsonArray<IfcRepresentationMap>(obj["RepresentationMaps"] as JsonArray));
			Tag = extractString(obj["Tag"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "RepresentationMaps", RepresentationMaps, this, options);
			setAttribute(obj, "Tag", Tag);
		}
	}
}
#endif
