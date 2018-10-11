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
	public partial class IfcCartesianPoint : IfcPoint
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Coordinates", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
			{
				JArray array = token as JArray;
				if (array != null)
				{
					mCoordinateX = array[0].Value<double>();
					if (array.Count > 1)
					{
						mCoordinateY = array[1].Value<double>();
						if (array.Count > 2)
							mCoordinateZ = array[2].Value<double>();
					}
				}
				else
				{
					string[] values = token.Value<string>().Split(" ".ToCharArray());
					if (values.Length > 0)
					{
						mCoordinateX = double.Parse(values[0]);
						if (values.Length > 1)
						{
							mCoordinateY = double.Parse(values[1]);
							if (values.Length > 2)
								mCoordinateZ = double.Parse(values[2]);
						}
					}
				}
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			Tuple<double, double, double> coordinates = SerializeCoordinates;
			obj["Coordinates"] = coordinates.Item1 + (double.IsNaN(coordinates.Item2) ? "" : (" " + coordinates.Item2 + (double.IsNaN(coordinates.Item3) ? "" : " " + coordinates.Item3)));
		}
	}
	public partial class IfcCartesianPointList2D : IfcCartesianPointList //IFC4
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("CoordList", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
			{
				List<double[]> points = new List<double[]>();
				List<double> vals = token.Value<string>().Split(" ".ToCharArray()).ToList().ConvertAll(x => double.Parse(x));
				for (int icounter = 0; icounter < vals.Count; icounter += 2)
					points.Add(new double[] { vals[icounter], vals[icounter + 1] });
				mCoordList = points.ToArray();

			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["CoordList"] = string.Join(" ", mCoordList.Select(x => x[0] + " " + x[1]));
		}
	}
	public partial class IfcCartesianPointList3D : IfcCartesianPointList //IFC4
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("CoordList", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
			{
				List<double> vals = token.Value<string>().Split(" ".ToCharArray()).ToList().ConvertAll(x => double.Parse(x));
				List<double[]> points = new List<double[]>(vals.Count / 3);
				for (int icounter = 0; icounter < vals.Count; icounter += 3)
					points.Add(new double[] { vals[icounter], vals[icounter + 1], vals[icounter + 2] });
				mCoordList = points.ToArray();

			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["CoordList"] = string.Join(" ", mCoordList.ToList().ConvertAll(x => x[0] + " " + x[1] + " " + x[2]));
		}
	}
	public abstract partial class IfcCartesianTransformationOperator : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCartesianTransformationOperator2D ,IfcCartesianTransformationOperator3D))*/
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Axis1", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Axis1 = mDatabase.parseJObject<IfcDirection>(jobj);
			jobj = obj.GetValue("Axis2", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Axis2 = mDatabase.parseJObject<IfcDirection>(jobj);
			jobj = obj.GetValue("LocalOrigin", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
			{
				JToken jtoken = jobj["href"];
				if (jtoken != null)
					mLocalOrigin = jtoken.Value<int>();
				else
					LocalOrigin = mDatabase.parseJObject<IfcCartesianPoint>(jobj);
			}
			JToken token = obj.GetValue("Scale", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mScale = token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mAxis1 > 0)
				obj["Axis1"] = Axis1.getJson(this, options);
			if (mAxis2 > 0)
				obj["Axis2"] = Axis2.getJson(this, options);
			obj["LocalOrigin"] = LocalOrigin.getJson(this, options);
			if (!double.IsNaN(mScale))
				obj["Scale"] = Scale;

		}
	}
	public partial class IfcCartesianTransformationOperator3D : IfcCartesianTransformationOperator //SUPERTYPE OF(IfcCartesianTransformationOperator3DnonUniform)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("Axis3", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				Axis3 = mDatabase.parseJObject<IfcDirection>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mAxis3 > 0)
				obj["Axis3"] = Axis3.getJson(this, options);
		}
	}
	public partial class IfcCircle : IfcConic
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Radius = obj.GetValue("Radius", StringComparison.InvariantCultureIgnoreCase).Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Radius"] = Radius;
		}
	}
	public partial class IfcCircleHollowProfileDef : IfcCircleProfileDef
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			WallThickness = obj.GetValue("WallThickness", StringComparison.InvariantCultureIgnoreCase).Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["WallThickness"] = WallThickness;
		}
	}
	public partial class IfcCircleProfileDef : IfcParameterizedProfileDef
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Radius = obj.GetValue("Radius", StringComparison.InvariantCultureIgnoreCase).Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Radius"] = Radius;
		}
	}
	public partial class IfcCircularArcSegment2D : IfcCurveSegment2D  //IFC4.1
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Radius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Radius = token.Value<double>();
			token = obj.GetValue("IsCCW", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				IsCCW = token.Value<bool>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Radius"] = Radius;
			obj["IsCCW"] = IsCCW;
		}
	}
	public partial class IfcClassification : IfcExternalInformation, IfcClassificationReferenceSelect, IfcClassificationSelect, NamedObjectIfc //	SUBTYPE OF IfcExternalInformation;
	{
		//internal string mSource = "$"; //  : OPTIONAL IfcLabel;
		//internal string mEdition = "$"; //  : OPTIONAL IfcLabel;
		//internal DateTime mEditionDate = DateTime.MinValue; // : OPTIONAL IfcDate IFC4 change 
		//private int mEditionDateSS = 0; // : OPTIONAL IfcCalendarDate;
		//internal string mName;//  : IfcLabel;
		//internal string mDescription = "$";//	 :	OPTIONAL IfcText; IFC4 Addition
		//internal string mLocation = "$";//	 :	OPTIONAL IfcURIReference; IFC4 Addtion
		//internal List<string> mReferenceTokens = new List<string>();//	 :	OPTIONAL LIST [1:?] OF IfcIdentifier; IFC4 Addition
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			//Radius = obj.GetValue("Radius", StringComparison.InvariantCultureIgnoreCase).Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "Source", Source);
			setAttribute(obj, "Edition", Edition);
			if(mEditionDate != DateTime.MinValue)
				setAttribute(obj, "EditionDate", IfcDate.FormatSTEP(EditionDate));
			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Location", Location);
			if (mReferenceTokens.Count > 0)
				obj["ReferenceTokens"] = new JArray(ReferenceTokens);
		}
	}
	public partial class IfcClassificationReference : IfcExternalReference, IfcClassificationReferenceSelect, IfcClassificationSelect, IfcClassificationNotationSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("ReferencedSource", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
			{
				JObject jobj = token as JObject;
				if (jobj != null)
					ReferencedSource = mDatabase.parseJObject<IfcClassificationReferenceSelect>(jobj);
			}
			token = obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Description = token.Value<string>();
			token = obj.GetValue("Sort", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Sort = token.Value<string>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mReferencedSource != null)
				obj["ReferencedSource"] = ReferencedSource.getJson(this, options);
			setAttribute(obj, "Description", Description);
			setAttribute(obj, "Sort", Sort);
		}
	}
	public partial class IfcColourRgb : IfcColourSpecification, IfcColourOrFactor
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mRed = obj.GetValue("Red", StringComparison.InvariantCultureIgnoreCase).Value<double>();
			mGreen = obj.GetValue("Green", StringComparison.InvariantCultureIgnoreCase).Value<double>();
			mBlue = obj.GetValue("Blue", StringComparison.InvariantCultureIgnoreCase).Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Red"] = mRed;
			obj["Green"] = mGreen;
			obj["Blue"] = mBlue;
		}
	}
	public abstract partial class IfcColourSpecification : IfcPresentationItem, IfcColour //	ABSTRACT SUPERTYPE OF(IfcColourRgb)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Name = extractString(obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "Name", Name);
		}
	}
	public partial class IfcComplexProperty : IfcProperty
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			UsageName = extractString(obj.GetValue("UsageName", StringComparison.InvariantCultureIgnoreCase));
			mDatabase.extractJArray<IfcProperty>(obj.GetValue("HasProperties", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => addProperty(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "UsageName", UsageName);
			obj["HasProperties"] = new JArray(mPropertyIndices.Select(x => mDatabase[x].getJson(this, options)));
		}
	}
	public partial class IfcCompositeCurve : IfcBoundedCurve
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Segments"] = new JArray(Segments.ToList().ConvertAll(x => x.getJson(this, options)));
			obj["SelfIntersect"] = mSelfIntersect.ToString();
		}
	}
	public partial class IfcComplexPropertyTemplate : IfcPropertyTemplate
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("UsageName", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				UsageName = token.Value<string>();
			token = obj.GetValue("TemplateType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcComplexPropertyTemplateTypeEnum>(token.Value<string>(), true, out mTemplateType);
			mDatabase.extractJArray<IfcPropertyTemplate>(obj.GetValue("HasPropertyTemplates", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => AddPropertyTemplate(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "UsageName", UsageName);
			if (mTemplateType != IfcComplexPropertyTemplateTypeEnum.NOTDEFINED)
				obj["TemplateType"] = mTemplateType.ToString();
			if(mHasPropertyTemplates.Count > 0)
				obj["HasPropertyTemplates"] = new JArray(HasPropertyTemplates.Values.ToList().ConvertAll(x => x.getJson(this, options)));
		}
	}
	public partial class IfcConnectedFaceSet : IfcTopologicalRepresentationItem //SUPERTYPE OF (ONEOF (IfcClosedShell ,IfcOpenShell))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			CfsFaces.AddRange(mDatabase.extractJArray<IfcFace>(obj.GetValue("CfsFaces", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["CfsFaces"] = new JArray(mCfsFaces.ConvertAll(x => x.getJson(this, options)));
		}
	}
	public abstract partial class IfcConstraint : BaseClassIfc, IfcResourceObjectSelect //IFC4Change ABSTRACT SUPERTYPE OF(ONEOF(IfcMetric, IfcObjective));
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);

			JToken token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
			token = obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Description = token.Value<string>();
			token = obj.GetValue("ConstraintGrade", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcConstraintEnum>(token.Value<string>(), out mConstraintGrade);
			token = obj.GetValue("ConstraintSource", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ConstraintSource = token.Value<string>();
			CreatingActor = mDatabase.parseJObject<IfcActorSelect>(obj.GetValue("CreatingActor", StringComparison.InvariantCultureIgnoreCase) as JObject);
			List<IfcExternalReferenceRelationship> ers = mDatabase.extractJArray<IfcExternalReferenceRelationship>(obj.GetValue("HasExternalReferences", StringComparison.InvariantCultureIgnoreCase) as JArray);
			foreach (IfcExternalReferenceRelationship er in ers)
				er.RelatedResourceObjects.Add(this);
			List<IfcResourceConstraintRelationship> rcr = mDatabase.extractJArray<IfcResourceConstraintRelationship>(obj.GetValue("PropertiesForConstraint", StringComparison.InvariantCultureIgnoreCase) as JArray);
			foreach (IfcResourceConstraintRelationship r in rcr)
				r.RelatingConstraint = this;
			rcr = mDatabase.extractJArray<IfcResourceConstraintRelationship>(obj.GetValue("HasConstraintRelationships", StringComparison.InvariantCultureIgnoreCase) as JArray);
			foreach (IfcResourceConstraintRelationship r in rcr)
				r.addRelated(this);
			UserDefinedGrade = extractString(obj.GetValue("UserDefinedGrade", StringComparison.InvariantCultureIgnoreCase));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);

			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			obj["ConstraintGrade"] = mConstraintGrade.ToString();
			setAttribute(obj, "ConstraintSource", ConstraintSource);

			if (mHasExternalReferences.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcExternalReferenceRelationship r in mHasExternalReferences)
					array.Add(r.getJson(this, options));
				obj["HasExternalReferences"] = array;
			}
			if (mPropertiesForConstraint.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcResourceConstraintRelationship r in mPropertiesForConstraint)
					array.Add(r.getJson(this, options));
				obj["PropertiesForConstraint"] = array;
			}
			if (mHasConstraintRelationships.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcResourceConstraintRelationship r in mHasConstraintRelationships)
					array.Add(r.getJson(this, options));
				obj["HasConstraintRelationships"] = array;
			}
			setAttribute(obj, "UserDefinedGrade", UserDefinedGrade);
		}
	}
	public abstract partial class IfcContext : IfcObjectDefinition//(IfcProject, IfcProjectLibrary)
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
				JObject jobj = getJson(null, options);
				return jobj.ToString();
			}
		}
		internal override void parseJObject(JObject obj)
		{
			if (mDatabase.mContext == null) //this as IfcProjectLibrary == null ||
				mDatabase.mContext = this;
			base.parseJObject(obj);

			JToken token = obj.GetValue("ObjectType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				ObjectType = token.Value<string>();
			token = obj.GetValue("LongName", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				LongName = token.Value<string>();
			token = obj.GetValue("Phase", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Phase = token.Value<string>();
			RepresentationContexts.AddRange(mDatabase.extractJArray<IfcRepresentationContext>(obj.GetValue("RepresentationContexts", StringComparison.InvariantCultureIgnoreCase) as JArray));
			UnitsInContext = mDatabase.parseJObject<IfcUnitAssignment>(obj.GetValue("UnitsInContext", StringComparison.InvariantCultureIgnoreCase) as JObject);
			mDatabase.extractJArray<IfcRelDefinesByProperties>(obj.GetValue("IsDefinedBy", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => x.RelatedObjects.Add(this));
			mDatabase.extractJArray<IfcRelDeclares>(obj.GetValue("Declares", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x => x.RelatingContext = this);


		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "ObjectType", ObjectType);
			setAttribute(obj, "LongName", LongName);
			setAttribute(obj, "Phase", Phase);

			if (mRepresentationContexts.Count > 0)
			{
				JArray array = new JArray() { };
				foreach (IfcRepresentationContext representationContext in RepresentationContexts)
					array.Add(representationContext.getJson(this, options));
				obj["RepresentationContexts"] = array;
			}
			IfcUnitAssignment unitsInContext = UnitsInContext;
			if (unitsInContext != null)
				obj["UnitsInContext"] = unitsInContext.getJson(this, options);
			if (mIsDefinedBy.Count > 0 && options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				JArray array = new JArray();
				foreach (IfcRelDefinesByProperties rdp in mIsDefinedBy)
					array.Add(rdp.getJson(this, options));
				obj["IsDefinedBy"] = array;
			}
			if (mDeclares.Count > 0)
			{
				JArray array = new JArray() { };
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
	public partial class IfcConversionBasedUnit : IfcNamedUnit, IfcResourceObjectSelect //	SUPERTYPE OF(IfcConversionBasedUnitWithOffset)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
			JObject jobj = obj.GetValue("ConversionFactor", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				ConversionFactor = extractObject<IfcMeasureWithUnit>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			base.setAttribute(obj, "Name", Name);
			obj["ConversionFactor"] = ConversionFactor.getJson(this, options);
		}
	}
	public abstract partial class IfcCoordinateOperation : BaseClassIfc // IFC4 	ABSTRACT SUPERTYPE OF(IfcMapConversion);
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["TargetCRS"] = TargetCRS.getJson(this, options);
		}
	}
	public abstract partial class IfcCoordinateReferenceSystem : BaseClassIfc, IfcCoordinateReferenceSystemSelect  // IFC4 	ABSTRACT SUPERTYPE OF(IfcProjectedCRS);
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
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
	public partial class IfcCovering : IfcBuildingElement
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcCoveringTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcCoveringTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public partial class IfcCoveringType : IfcBuildingElementType
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcCoveringTypeEnum>(token.Value<string>(), true, out mPredefinedType);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcCoveringTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
		}
	}
	public abstract partial class IfcCsgPrimitive3D : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBlock ,IfcRectangularPyramid ,IfcRightCircularCone ,IfcRightCircularCylinder ,IfcSphere))*/
	{
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["Position"] = Position.getJson(this, options);
		}
	}
	public partial class IfcCShapeProfileDef : IfcParameterizedProfileDef
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
			token = obj.GetValue("WallThickness", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mWallThickness);
			token = obj.GetValue("Girth", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mGirth);
			token = obj.GetValue("InternalFilletRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mInternalFilletRadius);
			token = obj.GetValue("EdgeRadius", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				double.TryParse(token.Value<string>(), out mCentreOfGravityInX);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			int digits = mDatabase.mLengthDigits;
			base.setJSON(obj, host, options);
			obj["Depth"] = Math.Round(mDepth, digits);
			obj["Width"] = Math.Round(mWidth, digits);
			obj["WallThickness"] = Math.Round(mWallThickness, digits);
			obj["Girth"] = Math.Round(mGirth, digits);
			if (!double.IsNaN(mInternalFilletRadius) && mInternalFilletRadius > 0)
				obj["InternalFilletRadius"] = Math.Round(mInternalFilletRadius, digits);
			if (mDatabase.Release <= ReleaseVersion.IFC2x3)
			{
				if (!double.IsNaN(mCentreOfGravityInX) && mCentreOfGravityInX > 0)
					obj["CentreOfGravityInX"] = mCentreOfGravityInX;
			}
		}
	}
	public partial class IfcCurveBoundedPlane : IfcBoundedSurface
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			BasisSurface = extractObject<IfcPlane>(obj.GetValue("BasisSurface", StringComparison.InvariantCultureIgnoreCase) as JObject);
			OuterBoundary = extractObject<IfcCurve>(obj.GetValue("OuterBoundary", StringComparison.InvariantCultureIgnoreCase) as JObject);
			JArray array = obj.GetValue("InnerBoundaries", StringComparison.InvariantCultureIgnoreCase) as JArray;
			if (array != null)
				mDatabase.extractJArray<IfcCurve>(array).ForEach(x=> addInnerBoundary(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["BasisSurface"] = BasisSurface.getJson(this, options);
			obj["OuterBoundary"] = OuterBoundary.getJson(this, options);
			if (mInnerBoundaries.Count > 0)
				obj["InnerBoundaries"] = new JArray(InnerBoundaries.ToList().ConvertAll(x => x.getJson(this, options)));
		}
	}
	public abstract partial class IfcCurveSegment2D : IfcBoundedCurve
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("StartPoint", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				StartPoint = mDatabase.parseJObject<IfcCartesianPoint>(token as JObject);
			token = obj.GetValue("StartDirection", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				StartDirection = token.Value<double>();
			token = obj.GetValue("SegmentLength", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				SegmentLength = token.Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["StartPoint"] = StartPoint.getJson(this, options);
			obj["StartDirection"] = StartDirection;
			obj["SegmentLength"] = SegmentLength;
		}
	}
}
