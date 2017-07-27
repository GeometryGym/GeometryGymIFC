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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["Coordinates"] = mCoordinateX + (double.IsNaN(mCoordinateY) ? "" : (" " + mCoordinateY + (double.IsNaN(mCoordinateZ) ? "" : " " + mCoordinateZ)));
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
				List<Tuple<double, double>> points = new List<Tuple<double, double>>();
				List<double> vals = token.Value<string>().Split(" ".ToCharArray()).ToList().ConvertAll(x => double.Parse(x));
				for (int icounter = 0; icounter < vals.Count; icounter += 2)
					points.Add(new Tuple<double, double>(vals[icounter], vals[icounter + 1]));
				mCoordList = points.ToArray();

			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["CoordList"] = string.Join(" ", mCoordList.ToList().ConvertAll(x => x.Item1 + " " + x.Item2));
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
				List<Tuple<double, double, double>> points = new List<Tuple<double, double, double>>();
				List<double> vals = token.Value<string>().Split(" ".ToCharArray()).ToList().ConvertAll(x => double.Parse(x));
				for (int icounter = 0; icounter < vals.Count; icounter += 3)
					points.Add(new Tuple<double, double, double>(vals[icounter], vals[icounter + 1], vals[icounter + 2]));
				mCoordList = points.ToArray();

			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["CoordList"] = string.Join(" ", mCoordList.ToList().ConvertAll(x => x.Item1 + " " + x.Item2 + " " + x.Item3));
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mAxis1 > 0)
				obj["Axis1"] = Axis1.getJson(this, processed);
			if (mAxis2 > 0)
				obj["Axis2"] = Axis2.getJson(this, processed);
			obj["LocalOrigin"] = LocalOrigin.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mAxis3 > 0)
				obj["Axis3"] = Axis3.getJson(this, processed);
		}
	}
	public partial class IfcCircleProfileDef : IfcParameterizedProfileDef
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			Radius = obj.GetValue("Radius", StringComparison.InvariantCultureIgnoreCase).Value<double>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["Radius"] = Radius;
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			base.setAttribute(obj, "Name", Name);
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
				er.addRelated(this);
			List<IfcResourceConstraintRelationship> rcr = mDatabase.extractJArray<IfcResourceConstraintRelationship>(obj.GetValue("PropertiesForConstraint", StringComparison.InvariantCultureIgnoreCase) as JArray);
			foreach (IfcResourceConstraintRelationship r in rcr)
				r.RelatingConstraint = this;
			rcr = mDatabase.extractJArray<IfcResourceConstraintRelationship>(obj.GetValue("HasConstraintRelationships", StringComparison.InvariantCultureIgnoreCase) as JArray);
			foreach (IfcResourceConstraintRelationship r in rcr)
				r.addRelated(this);
			UserDefinedGrade = extractString(obj.GetValue("UserDefinedGrade", StringComparison.InvariantCultureIgnoreCase));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);

			setAttribute(obj, "Name", Name);
			setAttribute(obj, "Description", Description);
			obj["ConstraintGrade"] = mConstraintGrade.ToString();
			setAttribute(obj, "ConstraintSource", ConstraintSource);

			if (mHasExternalReferences.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcExternalReferenceRelationship r in mHasExternalReferences)
					array.Add(r.getJson(this, processed));
				obj["HasExternalReferences"] = array;
			}
			if (mPropertiesForConstraint.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcResourceConstraintRelationship r in mPropertiesForConstraint)
					array.Add(r.getJson(this, processed));
				obj["PropertiesForConstraint"] = array;
			}
			if (mHasConstraintRelationships.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcResourceConstraintRelationship r in mHasConstraintRelationships)
					array.Add(r.getJson(this, processed));
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
				JObject jobj = getJson(null, new HashSet<int>());
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
			mDatabase.extractJArray<IfcRepresentationContext>(obj.GetValue("RepresentationContexts", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>addRepresentationContext(x));
			UnitsInContext = mDatabase.parseJObject<IfcUnitAssignment>(obj.GetValue("UnitsInContext", StringComparison.InvariantCultureIgnoreCase) as JObject);
			mDatabase.extractJArray<IfcRelDefinesByProperties>(obj.GetValue("IsDefinedBy", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>x.AddRelated(this));
			mDatabase.extractJArray<IfcRelDeclares>(obj.GetValue("Declares", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>x.RelatingContext = this);


		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			setAttribute(obj, "ObjectType", ObjectType);
			setAttribute(obj, "LongName", LongName);
			setAttribute(obj, "Phase", Phase);

			if (mRepresentationContexts.Count > 0)
			{
				JArray array = new JArray() { };
				foreach (IfcRepresentationContext representationContext in RepresentationContexts)
					array.Add(representationContext.getJson(this, processed));
				obj["RepresentationContexts"] = array;
			}
			IfcUnitAssignment unitsInContext = UnitsInContext;
			if (unitsInContext != null)
				obj["UnitsInContext"] = unitsInContext.getJson(this, processed);
			//INVERSE
			//public List<IfcRelDefinesByProperties> IsDefinedBy { get { return mIsDefinedBy; } }
			if (mDeclares.Count > 0)
			{
				JArray array = new JArray() { };
				foreach (IfcRelDeclares declare in Declares)
				{
					if (declare.RelatedDefinitions.Count > 0)
						array.Add(declare.getJson(this, processed));
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
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			base.setAttribute(obj, "Name", Name);
			obj["ConversionFactor"] = ConversionFactor.getJson(this, processed);
		}
	}
	public abstract partial class IfcCoordinateOperation : BaseClassIfc // IFC4 	ABSTRACT SUPERTYPE OF(IfcMapConversion);
	{
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["TargetCRS"] = TargetCRS.getJson(this, processed);
		}
	}
	public abstract partial class IfcCoordinateReferenceSystem : BaseClassIfc, IfcCoordinateReferenceSystemSelect  // IFC4 	ABSTRACT SUPERTYPE OF(IfcProjectedCRS);
	{
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
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


	public abstract partial class IfcCsgPrimitive3D : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBlock ,IfcRectangularPyramid ,IfcRightCircularCone ,IfcRightCircularCylinder ,IfcSphere))*/
	{
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["Position"] = Position.getJson(this, processed);
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
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["BasisSurface"] = BasisSurface.getJson(this, processed);
			obj["OuterBoundary"] = OuterBoundary.getJson(this, processed);
			if (mInnerBoundaries.Count > 0)
				obj["InnerBoundaries"] = new JArray(InnerBoundaries.ToList().ConvertAll(x => x.getJson(this, processed)));
		}
	}
}
