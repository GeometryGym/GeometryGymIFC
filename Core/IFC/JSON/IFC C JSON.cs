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
	public partial class IfcCartesianPoint
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Coordinates"];
			if (node != null)
			{
				JsonArray array = node as JsonArray;
				if (array != null)
				{
					mCoordinateX = array[0].GetValue<double>();
					if (array.Count > 1)
					{
						mCoordinateY = array[1].GetValue<double>();
						if (array.Count > 2)
							mCoordinateZ = array[2].GetValue<double>();
					}
				}
				else
				{
					string[] values = node.GetValue<string>().Split(" ".ToCharArray());
					if (values.Length > 0)
					{
						mCoordinateX = double.Parse(values[0], ParserSTEP.NumberFormat);
						if (values.Length > 1)
						{
							mCoordinateY = double.Parse(values[1], ParserSTEP.NumberFormat);
							if (values.Length > 2)
								mCoordinateZ = double.Parse(values[2], ParserSTEP.NumberFormat);
						}
					}
				}
			}
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			Tuple<double, double, double> coordinates = SerializeCoordinates;
			obj["Coordinates"] = coordinates.Item1 + (double.IsNaN(coordinates.Item2) ? "" : (" " + coordinates.Item2 + (double.IsNaN(coordinates.Item3) ? "" : " " + coordinates.Item3)));
		}
	}
	public partial class IfcCartesianPointList2D
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["CoordList"];
			if (node != null)
			{
				List<Tuple<double, double>> points = new List<Tuple<double, double>>();
				List<double> vals = node.GetValue<string>().Split(" ".ToCharArray()).ToList().ConvertAll(x => double.Parse(x));
				for (int icounter = 0; icounter < vals.Count; icounter += 2)
					points.Add(new Tuple<double, double>(vals[icounter], vals[icounter + 1]));
				mCoordList.AddRange(points);
			}
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["CoordList"] = string.Join(" ", mCoordList.Select(x => x.Item1 + " " + x.Item2));
		}
	}
	public partial class IfcCartesianPointList3D
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["CoordList"];
			if (node != null)
			{
				List<double> vals = node.GetValue<string>().Split(" ".ToCharArray()).ToList().ConvertAll(x => double.Parse(x));
				List<Tuple<double, double, double>> points = new List<Tuple<double, double, double>>(vals.Count / 3);
				for (int icounter = 0; icounter < vals.Count; icounter += 3)
					points.Add(new Tuple<double, double, double>(vals[icounter], vals[icounter + 1], vals[icounter + 2]));
				mCoordList.AddRange(points);

			}
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["CoordList"] = string.Join(" ", mCoordList.Select(x => x.Item1 + " " + x.Item2 + " " + x.Item3));
		}
	}
	public abstract partial class IfcCartesianTransformationOperator
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Axis1"] as JsonObject;
			if (jobj != null)
				Axis1 = mDatabase.ParseJsonObject<IfcDirection>(jobj);
			jobj = obj["Axis2"] as JsonObject;
			if (jobj != null)
				Axis2 = mDatabase.ParseJsonObject<IfcDirection>(jobj);
			jobj = obj["LocalOrigin"] as JsonObject;
			if (jobj != null)
				LocalOrigin = mDatabase.ParseJsonObject<IfcCartesianPoint>(jobj);
		
			mScale = extractDouble(obj["Scale"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mAxis1 != null)
				obj["Axis1"] = Axis1.getJson(this, options);
			if (mAxis2 != null)
				obj["Axis2"] = Axis2.getJson(this, options);
			obj["LocalOrigin"] = LocalOrigin.getJson(this, options);
			if (!double.IsNaN(mScale))
				obj["Scale"] = Scale;

		}
	}
	public partial class IfcCartesianTransformationOperator3D
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Axis3"] as JsonObject;
			if (jobj != null)
				Axis3 = mDatabase.ParseJsonObject<IfcDirection>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mAxis3 != null)
				obj["Axis3"] = Axis3.getJson(this, options);
		}
	}
	public partial class IfcCircle
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Radius = extractDouble(obj["Radius"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Radius"] = Radius;
		}
	}
	public partial class IfcCircleHollowProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mWallThickness = extractDouble(obj["WallThickness"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["WallThickness"] = WallThickness;
		}
	}
	public partial class IfcCircleProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Radius = extractDouble(obj["Radius"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Radius"] = Radius;
		}
	}
	public partial class IfcCircularArcSegment2D 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mRadius = extractDouble(obj["Radius"]);
			var node = obj["IsCCW"];
			if (node != null)
				IsCCW = node.GetValue<bool>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Radius"] = Radius;
			obj["IsCCW"] = IsCCW;
		}
	}
	public partial class IfcClassification
	{
		//internal string mSource = "$"; //  : OPTIONAL IfcLabel;
		//internal string mEdition = "$"; //  : OPTIONAL IfcLabel;
		//internal DateTime mEditionDate = DateTime.MinValue; // : OPTIONAL IfcDate IFC4 change 
		//private int mEditionDateSS = 0; // : OPTIONAL IfcCalendarDate;
		//internal string mName;//  : IfcLabel;
		//internal string mDescription = "$";//	 :	OPTIONAL IfcText; IFC4 Addition
		//internal string mLocation = "$";//	 :	OPTIONAL IfcURIReference; IFC4 Addtion
		//internal List<string> mReferenceTokens = new List<string>();//	 :	OPTIONAL LIST [1:?] OF IfcIdentifier; IFC4 Addition
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			//Radius = obj["Radius"].Value<double>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Source", Source);
			setAttribute(obj, "Edition", Edition);
			if (mEditionDate != DateTime.MinValue)
				setAttribute(obj, "EditionDate", IfcDate.FormatSTEP(EditionDate));
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Specification", Specification);
			createArray(obj, "ReferenceTokens", ReferenceTokens);
		}
	}
	public partial class IfcClassificationReference
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["ReferencedSource"];
			if (node != null)
			{
				JsonObject jobj = node as JsonObject;
				if (jobj != null)
					ReferencedSource = mDatabase.ParseJsonObject<IfcClassificationReferenceSelect>(jobj);
			}
			node = obj["Description"];
			if (node != null)
				Description = node.GetValue<string>();
			node = obj["Sort"];
			if (node != null)
				Sort = node.GetValue<string>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mReferencedSource != null)
				obj["ReferencedSource"] = ReferencedSource.getJson(this, options);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Sort", Sort);
		}
	}
	public partial class IfcClothoid 
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["ClothoidConstant"] = mClothoidConstant;
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mClothoidConstant = extractDouble(obj["ClothoidConstant"]);
		}
	}
	public partial class IfcColourRgb
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mRed = extractDouble(obj["Red"]);
			mGreen = extractDouble(obj["Green"]);
			mBlue = extractDouble(obj["Blue"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Red"] = mRed;
			obj["Green"] = mGreen;
			obj["Blue"] = mBlue;
		}
	}
	public abstract partial class IfcColourSpecification
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			Name = extractString(obj["Name"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "Name", Name);
		}
	}
	public partial class IfcComplexProperty
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			UsageName = extractString(obj["UsageName"]);
			mDatabase.extractJsonArray<IfcProperty>(obj["HasProperties"] as JsonArray).ForEach(x => addProperty(x));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "UsageName", UsageName);
			createArray(obj, "HasProperties", HasProperties.Values, this, options);
		}
	}
	public partial class IfcCompositeCurve
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "Segments", Segments, this, options);
			obj["SelfIntersect"] = mSelfIntersect.ToString();
		}
	}
	public partial class IfcCompositeCurveSegment
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["SameSense"] = mSameSense.ToString();
			obj["ParentCurve"] = mParentCurve.getJson(this, options);
		}
	}
	public partial class IfcComplexPropertyTemplate
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["UsageName"];
			if (node != null)
				UsageName = node.GetValue<string>();
			node = obj["TemplateType"];
			if (node != null)
				Enum.TryParse<IfcComplexPropertyTemplateTypeEnum>(node.GetValue<string>(), true, out mTemplateType);
			mDatabase.extractJsonArray<IfcPropertyTemplate>(obj["HasPropertyTemplates"] as JsonArray).ForEach(x => AddPropertyTemplate(x));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "UsageName", UsageName);
			if (mTemplateType != IfcComplexPropertyTemplateTypeEnum.NOTDEFINED)
				obj["TemplateType"] = mTemplateType.ToString();
			createArray(obj, "HasPropertyTemplates", HasPropertyTemplates.Values, this, options);
		}
	}
	public partial class IfcConnectedFaceSet
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			CfsFaces.AddRange(mDatabase.extractJsonArray<IfcFace>(obj["CfsFaces"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			createArray(obj, "CfsFaces", CfsFaces, this, options);
		}
	}
	public abstract partial class IfcConstraint
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);

			var node = obj["Name"];
			if (node != null)
				Name = node.GetValue<string>();
			node = obj["Description"];
			if (node != null)
				Description = node.GetValue<string>();
			node = obj["ConstraintGrade"];
			if (node != null)
				Enum.TryParse<IfcConstraintEnum>(node.GetValue<string>(), out mConstraintGrade);
			node = obj["ConstraintSource"];
			if (node != null)
				ConstraintSource = node.GetValue<string>();
			CreatingActor = mDatabase.ParseJsonObject<IfcActorSelect>(obj["CreatingActor"] as JsonObject);
			List<IfcExternalReferenceRelationship> ers = mDatabase.extractJsonArray<IfcExternalReferenceRelationship>(obj["HasExternalReference"] as JsonArray);
			foreach (IfcExternalReferenceRelationship er in ers)
				er.RelatedResourceObjects.Add(this);
			List<IfcResourceConstraintRelationship> rcr = mDatabase.extractJsonArray<IfcResourceConstraintRelationship>(obj["PropertiesForConstraint"] as JsonArray);
			foreach (IfcResourceConstraintRelationship r in rcr)
				r.RelatingConstraint = this;
			rcr = mDatabase.extractJsonArray<IfcResourceConstraintRelationship>(obj["HasConstraintRelationships"] as JsonArray);
			foreach (IfcResourceConstraintRelationship r in rcr)
				r.RelatedResourceObjects.Add(this);
			UserDefinedGrade = extractString(obj["UserDefinedGrade"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			obj["ConstraintGrade"] = mConstraintGrade.ToString();
			setAttribute(obj, "ConstraintSource", ConstraintSource);

			if (mHasExternalReference.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcExternalReferenceRelationship r in mHasExternalReference)
					array.Add(r.getJson(this, options));
				obj["HasExternalReference"] = array;
			}
			if (mPropertiesForConstraint.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcResourceConstraintRelationship r in mPropertiesForConstraint)
					array.Add(r.getJson(this, options));
				obj["PropertiesForConstraint"] = array;
			}
			if (mHasConstraintRelationships.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcResourceConstraintRelationship r in mHasConstraintRelationships)
					array.Add(r.getJson(this, options));
				obj["HasConstraintRelationships"] = array;
			}
			setAttribute(obj, "UserDefinedGrade", UserDefinedGrade);
		}
	}
	public abstract partial class IfcContext
	{
		public string Json
		{
			get
			{
				SetJsonOptions options = new SetJsonOptions();
				if(mDatabase != null)
				{
					options.LengthDigitCount = mDatabase.mLengthDigits;
					options.Version = mDatabase.Release;
				}
				JsonObject jobj = getJson(null, options);
				return jobj.ToString();
			}
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			if (mDatabase.mContext == null || this as IfcProjectLibrary == null)
				mDatabase.mContext = this;

			var node = obj["ObjectType"];
			if (node != null)
				ObjectType = node.GetValue<string>();
			node = obj["LongName"];
			if (node != null)
				LongName = node.GetValue<string>();
			node = obj["Phase"];
			if (node != null)
				Phase = node.GetValue<string>();
			mDatabase.extractJsonArray<IfcRelDefinesByProperties>(obj["IsDefinedBy"] as JsonArray).ForEach(x => x.RelatedObjects.Add(this));
			mDatabase.extractJsonArray<IfcRelDeclares>(obj["Declares"] as JsonArray).ForEach(x => x.RelatingContext = this);
			RepresentationContexts.AddRange(mDatabase.extractJsonArray<IfcRepresentationContext>(obj["RepresentationContexts"] as JsonArray));
			UnitsInContext = mDatabase.ParseJsonObject<IfcUnitAssignment>(obj["UnitsInContext"] as JsonObject);

			base.parseJsonObject(obj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			if (mRepresentationContexts.Count > 0)
			{
				JsonArray array = new JsonArray() { };
				foreach (IfcRepresentationContext representationContext in RepresentationContexts)
					array.Add(representationContext.getJson(this, options));
				obj["RepresentationContexts"] = array;
			}
			IfcUnitAssignment unitsInContext = UnitsInContext;
			if (unitsInContext != null)
				obj["UnitsInContext"] = unitsInContext.getJson(this, options);
			base.setJSON(obj, host, options);
			setAttribute(obj, "ObjectType", ObjectType);
			setAttribute(obj, "LongName", LongName);
			setAttribute(obj, "Phase", Phase);

			if (mIsDefinedBy.Count > 0 && options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				JsonArray array = new JsonArray();
				foreach (IfcRelDefinesByProperties rdp in mIsDefinedBy)
					array.Add(rdp.getJson(this, options));
				obj["IsDefinedBy"] = array;
			}
			if (mDeclares.Count > 0)
			{
				JsonArray array = new JsonArray() { };
				foreach (IfcRelDeclares declare in Declares)
				{
					if (declare.RelatedDefinitions.Count > 0)
						array.Add(declare.getJson(this, options));
				}
				if (array.Count > 0)
					obj["Declares"] = array;
			}
		}
	}
	public partial class IfcConversionBasedUnit
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Name"];
			if (node != null)
				Name = node.GetValue<string>();
			JsonObject jobj = obj["ConversionFactor"] as JsonObject;
			if (jobj != null)
				ConversionFactor = extractObject<IfcMeasureWithUnit>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "Name", Name);
			obj["ConversionFactor"] = ConversionFactor.getJson(this, options);
		}
	}
	public partial class IfcConveyorSegment
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcConveyorSegmentTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcConveyorSegmentTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcConveyorSegmentType
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
				Enum.TryParse<IfcConveyorSegmentTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcCoordinateOperation
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["TargetCRS"] = TargetCRS.getJson(this, options);
		}
	}
	public abstract partial class IfcCoordinateReferenceSystem
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			string str = Name;
			if (!string.IsNullOrEmpty(str))
				obj["Name"] = str;
			str = Description;
			if (!string.IsNullOrEmpty(str))
				obj["Description"] = str;
			obj["GeodeticDatum"] = GeodeticDatum;
			str = VerticalDatum;
			if (!string.IsNullOrEmpty(str))
				obj["VerticalDatum"] = str;
		}
	}
	public partial class IfcCosineSpiral
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["CosineTerm"] = mCosineTerm.ToString();
			if(!double.IsNaN(mConstantTerm))
				obj["ConstantTerm"] = mConstantTerm.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mCosineTerm = extractDouble(obj["CosineTerm"]);
			mConstantTerm = extractDouble(obj["Constant"]);
		}
	}
	public partial class IfcCourse
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcCourseTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcCourseTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcCourseType
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
				Enum.TryParse<IfcCourseTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
	}
	public partial class IfcCovering
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcCoveringTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcCoveringTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcCoveringType
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcCoveringTypeEnum>(node.GetValue<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcCoveringTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public abstract partial class IfcCsgPrimitive3D
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Position"] as JsonObject;
			if (jobj != null)
				Position = mDatabase.ParseJsonObject<IfcAxis2Placement3D>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Position"] = Position.getJson(this, options);
		}
	}
	public partial class IfcCsgSolid
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["TreeRootExpression"] as JsonObject;
			if (jobj != null)
				TreeRootExpression = mDatabase.ParseJsonObject<IfcCsgSelect>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["TreeRootExpression"] = TreeRootExpression.getJson(this, options);
		}
	}
	public partial class IfcCShapeProfileDef
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mDepth = extractDouble(obj["Depth"]);
			mWidth = extractDouble(obj["Width"]);
			mWallThickness = extractDouble(obj["WallThickness"]);
			mGirth = extractDouble(obj["Girth"]);
			mInternalFilletRadius = extractDouble(obj["InternalFilletRadius"]);
			mCentreOfGravityInX = extractDouble(obj["CentreOfGravityInX"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Depth"] = formatLength(mDepth);
			obj["Width"] = formatLength(mWidth);
			obj["WallThickness"] = formatLength(mWallThickness);
			obj["Girth"] = formatLength(mGirth);
			if (!double.IsNaN(mInternalFilletRadius) && mInternalFilletRadius > 0)
				obj["InternalFilletRadius"] = formatLength(mInternalFilletRadius);
			if (mDatabase.Release <= ReleaseVersion.IFC2x3)
			{
				if (!double.IsNaN(mCentreOfGravityInX) && mCentreOfGravityInX > 0)
					obj["CentreOfGravityInX"] = formatLength(mCentreOfGravityInX);
			}
		}
	}
	public partial class IfcCurveBoundedPlane
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			BasisSurface = extractObject<IfcPlane>(obj["BasisSurface"] as JsonObject);
			OuterBoundary = extractObject<IfcCurve>(obj["OuterBoundary"] as JsonObject);
			JsonArray array = obj["InnerBoundaries"] as JsonArray;
			if (array != null)
				InnerBoundaries.AddRange(mDatabase.extractJsonArray<IfcCurve>(array));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["BasisSurface"] = BasisSurface.getJson(this, options);
			obj["OuterBoundary"] = OuterBoundary.getJson(this, options);
			createArray(obj, "InnerBoundaries", InnerBoundaries, this, options);
		}
	}
	public partial class IfcCurveSegment
	{
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Placement"] = Placement.getJson(this, options);
			obj["SegmentLength"] = SegmentLength.getJson(this, options);
			obj["ParentCurve"] = ParentCurve.getJson(this, options);
		}
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["Placement"] as JsonObject;
			if (jobj != null)
				Placement = mDatabase.ParseJsonObject<IfcPlacement>(jobj);
			jobj = obj["SegmentLength"] as JsonObject;
			if (jobj != null)
				SegmentLength = mDatabase.ParseJsonObject<IfcCurveMeasureSelect>(jobj);
			jobj = obj["ParentCurve"] as JsonObject;
			if (jobj != null)
				ParentCurve = mDatabase.ParseJsonObject<IfcCurve>(jobj);
		}
	}
	public abstract partial class IfcCurveSegment2D
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["StartPoint"];
			if (node != null)
				StartPoint = mDatabase.ParseJsonObject<IfcCartesianPoint>(node as JsonObject);
			mStartDirection = extractDouble(obj["StartDirection"]);
			mSegmentLength = extractDouble(obj["SegmentLength"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["StartPoint"] = StartPoint.getJson(this, options);
			obj["StartDirection"] = StartDirection;
			obj["SegmentLength"] = SegmentLength;
		}
	}
}
#endif
