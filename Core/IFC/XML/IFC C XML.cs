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
using System.Xml;
//using System.Xml.Linq;


namespace GeometryGym.Ifc
{
	public partial class IfcCartesianPoint : IfcPoint
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Coordinates"))
			{
				string[] coords = xml.Attributes["Coordinates"].Value.Split(" ".ToArray());
				if (coords.Length > 0)
				{
					mCoordinateX = double.Parse(coords[0]);
					if (coords.Length > 1)
					{
						mCoordinateY = double.Parse(coords[1]);
						if (coords.Length > 2 && !string.IsNullOrEmpty(coords[2]))
							mCoordinateZ = double.Parse(coords[2]);
						else
							mCoordinateZ = double.NaN;
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Coordinates", mCoordinateX.ToString() + (double.IsNaN(mCoordinateY) ? "" : " " + mCoordinateY.ToString()) + (double.IsNaN(mCoordinateZ) ? "" : " " + mCoordinateZ.ToString()));
		}
	}
	public partial class IfcCartesianPointList2D : IfcCartesianPointList //IFC4
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("CoordList"))
			{
				string[] fields = xml.Attributes["CoordList"].Value.Split(" ".ToCharArray());
				mCoordList = new Tuple<double, double>[fields.Length / 2];
				int pos = 0;
				for (int icounter = 0; icounter < fields.Length; icounter += 2)
					mCoordList[pos++] = new Tuple<double, double>(double.Parse(fields[icounter]), double.Parse(fields[icounter + 1]));
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			Tuple<double, double> tuple = mCoordList[0];
			string str = tuple.Item1 + " " + tuple.Item2;
			for (int icounter = 1; icounter < mCoordList.Length; icounter++)
			{
				tuple = mCoordList[icounter];
				str += " " + tuple.Item1 + " " + tuple.Item2;
			}
			xml.SetAttribute("CoordList", str);
		}
	}
	public partial class IfcCartesianPointList3D : IfcCartesianPointList //IFC4
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("CoordList"))
			{
				string[] fields = xml.Attributes["CoordList"].Value.Split(" ".ToCharArray());
				mCoordList = new Tuple<double, double, double>[fields.Length / 3];
				int pos = 0;
				for (int icounter = 0; icounter < fields.Length; icounter += 3)
					mCoordList[pos++] = new Tuple<double, double, double>(double.Parse(fields[icounter]), double.Parse(fields[icounter + 1]), double.Parse(fields[icounter + 2]));
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			Tuple<double, double, double> tuple = mCoordList[0];
			string str = tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3;
			for (int icounter = 1; icounter < mCoordList.Length; icounter++)
			{
				tuple = mCoordList[icounter];
				str += " " + tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3;
			}
			xml.SetAttribute("CoordList", str);
		}
	}
	public abstract partial class IfcCartesianTransformationOperator : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCartesianTransformationOperator2D ,IfcCartesianTransformationOperator3D))*/
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Axis1") == 0)
					Axis1 = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
				if (string.Compare(name, "Axis2") == 0)
					Axis2 = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
				if (string.Compare(name, "LocalOrigin") == 0)
					LocalOrigin = mDatabase.ParseXml<IfcCartesianPoint>(child as XmlElement);
			}
			if (xml.HasAttribute("Scale"))
				Scale = double.Parse(xml.Attributes["Scale"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (mAxis1 > 0)
				xml.AppendChild(Axis1.GetXML(xml.OwnerDocument, "Axis1", this, processed));
			if (mAxis2 > 0)
				xml.AppendChild(Axis2.GetXML(xml.OwnerDocument, "Axis2", this, processed));
			xml.AppendChild(LocalOrigin.GetXML(xml.OwnerDocument, "LocalOrigin", this, processed));
			if (!double.IsNaN(mScale))
				xml.SetAttribute("Scale", mScale.ToString());
		}
	}
	public partial class IfcCartesianTransformationOperator2DnonUniform : IfcCartesianTransformationOperator2D
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			if (xml.HasAttribute("Scale2"))
				Scale2 = double.Parse(xml.Attributes["Scale2"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (!double.IsNaN(mScale2))
				xml.SetAttribute("Scale2", mScale2.ToString());
		}
	}
	public partial class IfcCartesianTransformationOperator3D : IfcCartesianTransformationOperator //SUPERTYPE OF(IfcCartesianTransformationOperator3DnonUniform)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Axis3") == 0)
					Axis3 = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (mAxis3 > 0)
				xml.AppendChild(Axis1.GetXML(xml.OwnerDocument, "Axis3", this, processed));
		}
	}
	public partial class IfcCartesianTransformationOperator3DnonUniform : IfcCartesianTransformationOperator3D
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			if (xml.HasAttribute("Scale2"))
				Scale2 = double.Parse(xml.Attributes["Scale2"].Value);
			if (xml.HasAttribute("Scale3"))
				Scale3 = double.Parse(xml.Attributes["Scale3"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (!double.IsNaN(mScale2))
				xml.SetAttribute("Scale2", mScale2.ToString());
			if (!double.IsNaN(mScale3))
				xml.SetAttribute("Scale3", mScale3.ToString());
		}
	}
	public partial class IfcCenterLineProfileDef : IfcArbitraryOpenProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Thickness"))
				Thickness = double.Parse(xml.Attributes["Thickness"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Thickness", mThickness.ToString());
		}
	}
	public partial class IfcChimney : IfcBuildingElement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcChimneyTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcChimneyTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcChimneyType : IfcBuildingElementType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcChimneyTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcChimneyTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcCircle : IfcConic
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Radius"))
				Radius = double.Parse(xml.Attributes["Radius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Radius", mRadius.ToString());
		}
	}
	public partial class IfcCircleHollowProfileDef : IfcCircleProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("WallThickness"))
				mWallThickness = double.Parse(xml.Attributes["WallThickness"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("WallThickness", mWallThickness.ToString());
		}
	}
	public partial class IfcCircleProfileDef : IfcParameterizedProfileDef //SUPERTYPE OF(IfcCircleHollowProfileDef)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Radius"))
				mRadius = double.Parse(xml.Attributes["Radius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Radius", mRadius.ToString());
		}
	}
	public partial class IfcColourRgb : IfcColourSpecification, IfcColourOrFactor
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Red"))
				mRed = double.Parse(xml.Attributes["Red"].Value);
			if (xml.HasAttribute("Green"))
				mGreen = double.Parse(xml.Attributes["Green"].Value);
			if (xml.HasAttribute("Blue"))
				mBlue = double.Parse(xml.Attributes["Blue"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Red", mRed.ToString());
			xml.SetAttribute("Green", mGreen.ToString());
			xml.SetAttribute("Blue", mBlue.ToString());
		}
	}
	public partial class IfcColumn : IfcBuildingElement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcColumnTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcColumnTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcColumnType : IfcBuildingElementType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcColumnTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcColumnTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcCompositeCurve : IfcBoundedCurve
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Segments") == 0)
				{
					List<IfcCompositeCurveSegment> segments = new List<IfcCompositeCurveSegment>(child.ChildNodes.Count);
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcCompositeCurveSegment s = mDatabase.ParseXml<IfcCompositeCurveSegment>(cn as XmlElement);
						if (s != null)
							segments.Add(s);
					}
					Segments = segments;
				}
			}
			if (xml.HasAttribute("SelfIntersect "))
				Enum.TryParse<IfcLogicalEnum>(xml.Attributes["SelfIntersect"].Value, true, out mSelfIntersect);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Segments");
			xml.AppendChild(element);
			foreach (IfcCompositeCurveSegment s in Segments)
				element.AppendChild(s.GetXML(xml.OwnerDocument, "", this, processed));
			setAttribute(xml, "SelfIntersect", mSelfIntersect.ToString().ToLower());
		}
	}
	public partial class IfcCompositeCurveSegment : IfcGeometricRepresentationItem
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Transition"))
				Enum.TryParse<IfcTransitionCode>(xml.Attributes["Transition"].Value, true, out mTransition);
			if (xml.HasAttribute("SameSense"))
				bool.TryParse(xml.Attributes["SameSense"].Value, out mSameSense);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ParentCurve") == 0)
					ParentCurve = mDatabase.ParseXml<IfcBoundedCurve>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Transition", mTransition.ToString().ToLower());
			xml.SetAttribute("SameSense", mSameSense.ToString().ToLower());
			xml.AppendChild(ParentCurve.GetXML(xml.OwnerDocument, "ParentCurve", this, processed));
		}
	}
	public partial class IfcCompositeProfileDef : IfcProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Profiles") == 0)
				{
					List<IfcProfileDef> profiles = new List<IfcProfileDef>(child.ChildNodes.Count);
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcProfileDef p = mDatabase.ParseXml<IfcProfileDef>(cn as XmlElement);
						if (p != null)
							profiles.Add(p);
					}
					Profiles = profiles;
				}
			}
			if (xml.HasAttribute("Label"))
				Label = xml.Attributes["Label"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Profiles");
			xml.AppendChild(element);
			foreach (IfcProfileDef pd in Profiles)
				element.AppendChild(pd.GetXML(xml.OwnerDocument, "", this, processed));
			setAttribute(xml, "Label", Label);
		}
	}
	public abstract partial class IfcConic : IfcCurve /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCircle ,IfcEllipse))*/
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Position") == 0)
					Position = mDatabase.ParseXml<IfcAxis2Placement>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(mDatabase[mPosition].GetXML(xml.OwnerDocument, "Position", this, processed));
		}
	}
	public partial class IfcConnectedFaceSet : IfcTopologicalRepresentationItem //SUPERTYPE OF (ONEOF (IfcClosedShell ,IfcOpenShell))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "CfsFaces") == 0)
				{
					List<IfcFace> faces = new List<IfcFace>(child.ChildNodes.Count);
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcFace f = mDatabase.ParseXml<IfcFace>(cn as XmlElement);
						if (f != null)
							faces.Add(f);
					}
					CfsFaces = faces;
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("CfsFaces");
			xml.AppendChild(element);
			foreach (IfcFace face in CfsFaces)
				element.AppendChild(face.GetXML(xml.OwnerDocument, "", host, processed));
		}
	}
	public abstract partial class IfcContext : IfcObjectDefinition//(IfcProject, IfcProjectLibrary)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("ObjectType"))
				ObjectType = xml.Attributes["ObjectType"].Value;
			if (xml.HasAttribute("LongName"))
				LongName = xml.Attributes["LongName"].Value;
			if (xml.HasAttribute("Phase"))
				Phase = xml.Attributes["Phase"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RepresentationContexts") == 0)
				{
					List<IfcRepresentationContext> repContexts = new List<IfcRepresentationContext>(child.ChildNodes.Count);
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcRepresentationContext rc = mDatabase.ParseXml<IfcRepresentationContext>(cn as XmlElement);
						if (rc != null)
							repContexts.Add(rc);
					}
					RepresentationContexts = repContexts;
				}
				else if (string.Compare(name, "UnitsInContext") == 0)
					UnitsInContext = mDatabase.ParseXml<IfcUnitAssignment>(child as XmlElement);
				else if (string.Compare(name, "IsDefinedBy") == 0)
				{
					List<IfcRelDefinesByProperties> defines = new List<IfcRelDefinesByProperties>(child.ChildNodes.Count);
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcRelDefinesByProperties rd = mDatabase.ParseXml<IfcRelDefinesByProperties>(cn as XmlElement);
						if (rd != null)
							defines.Add(rd);
					}
					IsDefinedBy = defines;
				}
				else if (string.Compare(name, "Declares") == 0)
				{
					List<IfcRelDeclares> declares = new List<IfcRelDeclares>(child.ChildNodes.Count);
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcRelDeclares rd = mDatabase.ParseXml<IfcRelDeclares>(cn as XmlElement);
						if (rd != null)
							declares.Add(rd);
					}
					Declares = declares;
				}
			}
			if (this as IfcProjectLibrary == null || mDatabase.mContext == null)
				mDatabase.mContext = this;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			XmlElement repContexts = null;
			if (mRepresentationContexts.Count > 0)
			{
				repContexts = xml.OwnerDocument.CreateElement("RepresentationContexts");
				foreach (IfcRepresentationContext rc in RepresentationContexts)
					repContexts.AppendChild(rc.GetXML(xml.OwnerDocument, "", this, processed));
			}
			base.SetXML(xml, host, processed);
			setAttribute(xml, "ObjectType", ObjectType);
			setAttribute(xml, "LongName", LongName);
			setAttribute(xml, "Phase", Phase);
			if (repContexts != null)
				xml.AppendChild(repContexts);
			if (mUnitsInContext > 0)
				xml.AppendChild(UnitsInContext.GetXML(xml.OwnerDocument, "UnitsInContext", this, processed));
			if (mIsDefinedBy.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("IsDefinedBy");
				xml.AppendChild(element);
				foreach (IfcRelDefines rd in IsDefinedBy)
					element.AppendChild(rd.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (mDeclares.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("Declares");
				xml.AppendChild(element);
				foreach (IfcRelDeclares rd in Declares)
					element.AppendChild(rd.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public partial class IfcConversionBasedUnit : IfcNamedUnit, IfcResourceObjectSelect //	SUPERTYPE OF(IfcConversionBasedUnitWithOffset)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ConversionFactor") == 0)
					ConversionFactor = mDatabase.ParseXml<IfcMeasureWithUnit>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Name", Name);
			xml.AppendChild(ConversionFactor.GetXML(xml.OwnerDocument, "ConversionFactor", this, processed));
		}
	}
	public abstract partial class IfcConstraint : BaseClassIfc, IfcResourceObjectSelect //IFC4Change ABSTRACT SUPERTYPE OF(ONEOF(IfcMetric, IfcObjective));
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			if (xml.HasAttribute("ConstraintGrade"))
				Enum.TryParse<IfcConstraintEnum>(xml.Attributes["ConstraintGrade"].Value, true, out mConstraintGrade);
			if (xml.HasAttribute("ConstraintSource"))
				ConstraintSource = xml.Attributes["ConstraintSource"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "CreatingActor") == 0)
					CreatingActor = mDatabase.ParseXml<IfcActorSelect>(child as XmlElement);
				else if (string.Compare(name, "HasExternalReferences") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcExternalReferenceRelationship r = mDatabase.ParseXml<IfcExternalReferenceRelationship>(cn as XmlElement);
						if (r != null)
							r.addRelated(this);
					}
				} 
				else if (string.Compare(name, "PropertiesForConstraint") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcResourceConstraintRelationship r = mDatabase.ParseXml<IfcResourceConstraintRelationship>(cn as XmlElement);
						if (r != null)
							r.RelatingConstraint = this;
					}
				}
				else if (string.Compare(name, "HasConstraintRelationships") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcResourceConstraintRelationship r = mDatabase.ParseXml<IfcResourceConstraintRelationship>(cn as XmlElement);
						if (r != null)
							r.addRelated(this);
					}
				}
			}
			if (xml.HasAttribute("UserDefinedGrade"))
				UserDefinedGrade = xml.Attributes["UserDefinedGrade"].Value;

		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			xml.SetAttribute("ConstraintGrade", mConstraintGrade.ToString().ToLower());
			setAttribute(xml, "ConstraintSource", ConstraintSource);
			if (mCreatingActor > 0)
				xml.AppendChild(mDatabase[mCreatingActor].GetXML(xml.OwnerDocument, "CreatingActor", host, processed));
			setAttribute(xml, "UserDefinedGrade", UserDefinedGrade);

			if (mHasExternalReferences.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasExternalReferences");
				xml.AppendChild(element);
				foreach (IfcExternalReferenceRelationship r in HasExternalReferences)
					element.AppendChild(r.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if(mPropertiesForConstraint.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("PropertiesForConstraint");
				foreach (IfcResourceConstraintRelationship r in mPropertiesForConstraint)
				{
					if (host.Index != r.mIndex && !processed.Contains(r.mIndex))
						element.AppendChild(r.GetXML(xml.OwnerDocument, "", this, processed));
				}
				if (element.HasChildNodes)
					xml.AppendChild(element);
			}
			if (mHasConstraintRelationships.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasConstraintRelationships");
				foreach (IfcResourceConstraintRelationship r in HasConstraintRelationships)
				{
					if (host.Index != r.mIndex)
						element.AppendChild(r.GetXML(xml.OwnerDocument, "", this, processed));
				}
				if (element.HasChildNodes)
					xml.AppendChild(element);
			}
		}
	}
	public abstract partial class IfcCoordinateOperation : BaseClassIfc // IFC4 	ABSTRACT SUPERTYPE OF(IfcMapConversion);
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "TargetCRS") == 0)
					TargetCRS = mDatabase.ParseXml<IfcCoordinateReferenceSystem>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(TargetCRS.GetXML(xml.OwnerDocument, "TargetCRS", this, processed));
		}
	}
	public abstract partial class IfcCoordinateReferenceSystem : BaseClassIfc, IfcCoordinateReferenceSystemSelect  // IFC4 	ABSTRACT SUPERTYPE OF(IfcProjectedCRS);
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			if (xml.HasAttribute("GeodeticDatum"))
				GeodeticDatum = xml.Attributes["GeodeticDatum"].Value;
			if (xml.HasAttribute("VerticalDatum"))
				VerticalDatum = xml.Attributes["VerticalDatum"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			xml.SetAttribute("GeodeticDatum", GeodeticDatum);
			setAttribute(xml, "VerticalDatum", VerticalDatum);
		}
	}
	public partial class IfcCovering : IfcBuildingElement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcCoveringTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcCoveringTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcCoveringType : IfcBuildingElementType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcCoveringTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcCoveringTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public abstract partial class IfcCsgPrimitive3D : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBlock ,IfcRectangularPyramid ,IfcRightCircularCone ,IfcRightCircularCylinder ,IfcSphere))*/
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Position") == 0)
					Position = mDatabase.ParseXml<IfcAxis2Placement3D>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Position.GetXML(xml.OwnerDocument, "Position", this, processed));
		}
	}
	public partial class IfcCsgSolid : IfcSolidModel
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "TreeRootExpression") == 0)
					TreeRootExpression = mDatabase.ParseXml<IfcCsgSelect>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(mDatabase[mTreeRootExpression].GetXML(xml.OwnerDocument, "TreeRootExpression", this, processed));
		}
	}
	public partial class IfcCShapeProfileDef : IfcParameterizedProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Depth"))
				mDepth = double.Parse(xml.Attributes["Depth"].Value);
			if (xml.HasAttribute("Width"))
				mWidth = double.Parse(xml.Attributes["Width"].Value);
			if (xml.HasAttribute("WallThickness"))
				mWallThickness = double.Parse(xml.Attributes["WallThickness"].Value);
			if (xml.HasAttribute("Girth"))
				mGirth = double.Parse(xml.Attributes["Girth"].Value);
			if (xml.HasAttribute("InternalFilletRadius"))
				mInternalFilletRadius = double.Parse(xml.Attributes["InternalFilletRadius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Depth", mDepth.ToString());
			xml.SetAttribute("Width", mWidth.ToString());
			xml.SetAttribute("WallThickness", mWallThickness.ToString());
			xml.SetAttribute("Girth", mGirth.ToString());
			if (!double.IsNaN(mInternalFilletRadius))
				xml.SetAttribute("InternalFilletRadius", mInternalFilletRadius.ToString());
		}
	}
	public partial class IfcCurtainWall : IfcBuildingElement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcCurtainWallTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcCurtainWallTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcCurtainWallType : IfcBuildingElementType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcCurtainWallTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcCurtainWallTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcCurveBoundedPlane : IfcBoundedSurface
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "BasisSurface") == 0)
					BasisSurface = mDatabase.ParseXml<IfcPlane>(child as XmlElement);
				else if (string.Compare(name, "OuterBoundary") == 0)
					OuterBoundary = mDatabase.ParseXml<IfcCurve>(child as XmlElement);
				else if (string.Compare(name, "InnerBoundaries") == 0)
				{
					List<IfcCurve> inners = new List<IfcCurve>(child.ChildNodes.Count);
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcCurve c = mDatabase.ParseXml<IfcCurve>(cn as XmlElement);
						if (c != null)
							inners.Add(c);
					}
					InnerBoundaries = inners;
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(BasisSurface.GetXML(xml.OwnerDocument, "BasisSurface", this, processed));
			xml.AppendChild(OuterBoundary.GetXML(xml.OwnerDocument, "OuterBoundary", this, processed));
			if (mInnerBoundaries.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("InnerBoundaries");
				xml.AppendChild(element);
				foreach (IfcCurve c in InnerBoundaries)
					element.AppendChild(c.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public partial class IfcCylindricalSurface : IfcElementarySurface //IFC4
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Radius"))
				mRadius = double.Parse(xml.Attributes["Radius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, HashSet<int> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Radius", mRadius.ToString());
		}
	}
}
