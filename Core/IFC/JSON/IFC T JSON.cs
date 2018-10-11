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
	public abstract partial class IfcTessellatedFaceSet : IfcTessellatedItem, IfcBooleanOperand //ABSTRACT SUPERTYPE OF(IfcTriangulatedFaceSet)
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Coordinates"] = mDatabase[mCoordinates].getJson(this, options);
			if (mHasColours != null)
				obj["HasColours"] = mHasColours.getJson(this, options);
			if (mHasTextures.Count > 0)
			{
				JArray array = new JArray(mHasTextures.Count);
				foreach (IfcIndexedTextureMap tm in HasTextures)
					array.Add(tm.getJson(this, options));
				obj["HasTextures"] = array;
			}
		}
	}
	public partial class IfcTransitionCurveSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("StartRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				StartRadius = token.Value<double>();
			token = obj.GetValue("EndRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				EndRadius = token.Value<double>();
			token = obj.GetValue("IsStartRadiusCCW", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				IsStartRadiusCCW = token.Value<bool>();
			token = obj.GetValue("IsEndRadiusCCW", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				IsEndRadiusCCW = token.Value<bool>();
			token = obj.GetValue("TransitionCurveType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcTransitionCurveType>(token.Value<string>(), true, out mTransitionCurveType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
		internal static IfcTranslationalStiffnessSelect parseJObject(JObject obj)
		{
			JObject jobj = obj.GetValue("IfcBoolean", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				return new IfcTranslationalStiffnessSelect(jobj.Value<bool>());
			jobj = obj.GetValue("IfcLinearStiffnessMeasure", StringComparison.InvariantCultureIgnoreCase) as JObject;
			return (jobj != null ? new IfcTranslationalStiffnessSelect(jobj.Value<double>()) : null);
		}
		internal JObject getJObject()
		{
			JObject obj = new JObject();
			if (mStiffness != null)
				obj["IfcLinearStiffnessMeasure"] = mStiffness.Measure;
			else
				obj["IfcBoolean"] = mRigid;
			return obj;
		}
	}
	public partial class IfcTrapeziumProfileDef : IfcParameterizedProfileDef
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("BottomXDim", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mBottomXDim);
			token = obj.GetValue("TopXDim", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mTopXDim);
			token = obj.GetValue("YDim", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mYDim);
			token = obj.GetValue("TopXOffset", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mTopXOffset);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			int digits = mDatabase.mLengthDigits;
			base.setJSON(obj, host, options);
			obj["BottomXDim"] = Math.Round(mBottomXDim, digits);
			obj["TopXDim"] = Math.Round(mTopXDim, digits);
			obj["YDim"] = Math.Round(mYDim, digits);
			obj["TopXOffset"] = Math.Round(mTopXOffset, digits);
		}
	}
	//public partial class IfcTriangulatedFaceSet : IfcTessellatedFaceSet
	//{
	//	protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
	//	{
	//		base.setJSON(obj, host, options);

	//		if (mNormals.Length > 0)
	//		{ // spaced list of numbers
	//			JArray array = new JArray() { };
	//			foreach (uple<double, double, double> normal in Normals)
	//			{
	//				JArray norm = new JArray() { };
	//				norm.Add(normal.Item1);
	//				norm.Add(normal.Item2);
	//				norm.Add(normal.Item3);
	//				array.Add(norm);
	//			}
	//			obj["Normals"] = array;
	//		}
	//		obj["Closed"] = Closed;
	//		JArray arr = new JArray();
	//		foreach (Tuple<int, int, int> coord in mCoordIndex)
	//		{
	//			JArray c = new JArray();
	//			c.Add(coord.Item1);
	//			c.Add(coord.Item2);
	//			c.Add(coord.Item3);
	//			arr.Add(c);
	//		}
	//		obj["CoordIndex"] = arr;
	//		if (mNormalIndex.Length > 0)
	//		{
	//			arr = new JArray();
	//			foreach (Tuple<int, int, int> norm in mNormalIndex)
	//			{
	//				JArray n = new JArray();
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
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
		internal JArray getJSON(DatabaseIfc db)
		{
			JArray result = new JArray();
			if (!double.IsNaN(mIfcParameterValue))
				result.Add(DatabaseIfc.extract(new IfcParameterValue(mIfcParameterValue)));
			if (mIfcCartesianPoint > 0)
				result.Add(db[mIfcCartesianPoint].getJson(null, new BaseClassIfc.SetJsonOptions()));
			return result;
		}
	}
	public partial class IfcTShapeProfileDef : IfcParameterizedProfileDef
	{
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
			token = obj.GetValue("FlangeEdgeRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mFlangeEdgeRadius);
			token = obj.GetValue("WebEdgeRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mWebEdgeRadius);
			token = obj.GetValue("WebSlope", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mWebSlope);
			token = obj.GetValue("FlangeSlope", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mFlangeSlope);
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
			if (!double.IsNaN(mFlangeEdgeRadius) && mFlangeEdgeRadius > 0)
				obj["FlangeEdgeRadius"] = Math.Round(mFlangeEdgeRadius, digits);
			if (!double.IsNaN(mWebEdgeRadius) && mWebEdgeRadius > 0)
				obj["WebEdgeRadius"] = Math.Round(mFlangeEdgeRadius, digits);
			if (!double.IsNaN(mWebSlope) && mWebSlope > 0)
				obj["WebSlope"] = mWebSlope;
			if (!double.IsNaN(mFlangeSlope) && mFlangeSlope > 0)
				obj["FlangeSlope"] = mFlangeSlope;
		}
	}
	public partial class IfcTypeObject : IfcObjectDefinition //(IfcTypeProcess, IfcTypeProduct, IfcTypeResource) IFC4 ABSTRACT 
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			ApplicableOccurrence = extractString(obj.GetValue("ApplicableOccurrence", StringComparison.InvariantCultureIgnoreCase));
			HasPropertySets.AddRange(mDatabase.extractJArray<IfcPropertySetDefinition>(obj.GetValue("HasPropertySets", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "ApplicableOccurrence", ApplicableOccurrence);
			
			if (mHasPropertySets.Count > 0)
				obj["HasPropertySets"] = new JArray(HasPropertySets.ToList().ConvertAll(x => x.getJson(this, options)));
			//IfcRelDefinesByType objectTypeOf = ObjectTypeOf;
			//if(objectTypeOf != null)
			//	obj["ObjectTypeOf"] = objectTypeOf.getJson(this, options);
		}
	}
	public partial class IfcTypeProduct : IfcTypeObject, IfcProductSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcDoorStyle ,IfcElementType ,IfcSpatialElementType ,IfcWindowStyle)) 
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			RepresentationMaps.AddRange(mDatabase.extractJArray<IfcRepresentationMap>(obj.GetValue("RepresentationMaps", StringComparison.InvariantCultureIgnoreCase) as JArray));
			Tag = extractString(obj.GetValue("Tag", StringComparison.InvariantCultureIgnoreCase));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if(mRepresentationMaps.Count > 0)
				obj["RepresentationMaps"] = new JArray(RepresentationMaps.ToList().ConvertAll(x=>x.getJson(this, options)));
			setAttribute(obj, "Tag", Tag);
		}
	}
}
