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
using System.Linq;
using System.Xml;


namespace GeometryGym.Ifc
{
	public partial class IfcCartesianPoint
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
			else
			{
				foreach (XmlNode child in xml.ChildNodes)
				{
					string name = child.Name;
					if (string.Compare(name, "Coordinates") == 0)
					{
						List<double> coordinates = new List<double>();
						foreach (XmlNode coordNode in child.ChildNodes)
						{
							int pos = coordinates.Count;
							XmlElement coordElement = coordNode as XmlElement;
							if (coordElement.HasAttribute("exp:pos"))
								pos = int.Parse(coordNode.Attributes["exp:pos"].Value);
							if (pos + 1 > coordinates.Count)
								coordinates.AddRange(Enumerable.Repeat<double>(0, 1 + pos - coordinates.Count));
							double d = 0;
							if (double.TryParse(coordNode.InnerText, out d))
								coordinates[pos] = d;
						}
						if (coordinates.Count > 0)
						{
							mCoordinateX = coordinates[0];
							if (coordinates.Count > 1)
							{
								mCoordinateY = coordinates[1];
								if (coordinates.Count > 2)
									mCoordinateZ = coordinates[2];
							}
						}
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			Tuple<double, double, double> coordinates = SerializeCoordinates;
			xml.SetAttribute("Coordinates", coordinates.Item1 + (double.IsNaN(coordinates.Item2) ? "" : " " + coordinates.Item2) + (double.IsNaN(coordinates.Item3) ? "" : " " + coordinates.Item3));
		}
	}
	public partial class IfcCartesianPointList2D
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("CoordList"))
			{
				string[] fields = xml.Attributes["CoordList"].Value.Split(" ".ToCharArray());
				List<Tuple<double,double>> coords = new List<Tuple<double,double>>(fields.Length / 10);
				for (int icounter = 0; icounter < fields.Length; icounter += 2)
					coords.Add(new Tuple<double, double> (double.Parse(fields[icounter]), double.Parse(fields[icounter + 1])));
				CoordList.AddRange(coords);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("CoordList", string.Join(" ", CoordList.Select(x => x.Item1 + " " + x.Item2)));
		}
	}
	public partial class IfcCartesianPointList3D
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("CoordList"))
			{
				string[] fields = xml.Attributes["CoordList"].Value.Split(" ".ToCharArray());
				List<Tuple<double,double,double>> points = new List<Tuple<double,double,double>>(fields.Length / 30);
				for (int icounter = 0; icounter < fields.Length; icounter += 3)
					points.Add(new Tuple<double, double, double>(double.Parse(fields[icounter]), double.Parse(fields[icounter + 1]), double.Parse(fields[icounter + 2])));
				mCoordList.AddRange(points);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("CoordList", string.Join(" ", mCoordList.Select(x => x.Item1 + " " + x.Item2 + " " + x.Item3)));
		}
	}
	public partial class IfcCartesianTransformationOperator
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
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mAxis1 != null)
				xml.AppendChild(Axis1.GetXML(xml.OwnerDocument, "Axis1", this, processed));
			if (mAxis2 != null)
				xml.AppendChild(Axis2.GetXML(xml.OwnerDocument, "Axis2", this, processed));
			xml.AppendChild(LocalOrigin.GetXML(xml.OwnerDocument, "LocalOrigin", this, processed));
			if (!double.IsNaN(mScale))
				xml.SetAttribute("Scale", mScale.ToString());
		}
	}
	public partial class IfcCartesianTransformationOperator2DnonUniform
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			if (xml.HasAttribute("Scale2"))
				Scale2 = double.Parse(xml.Attributes["Scale2"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (!double.IsNaN(mScale2))
				xml.SetAttribute("Scale2", mScale2.ToString());
		}
	}
	public partial class IfcCartesianTransformationOperator3D
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
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mAxis3 != null)
				xml.AppendChild(Axis1.GetXML(xml.OwnerDocument, "Axis3", this, processed));
		}
	}
	public partial class IfcCartesianTransformationOperator3DnonUniform 
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			if (xml.HasAttribute("Scale2"))
				Scale2 = double.Parse(xml.Attributes["Scale2"].Value);
			if (xml.HasAttribute("Scale3"))
				Scale3 = double.Parse(xml.Attributes["Scale3"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (!double.IsNaN(mScale2))
				xml.SetAttribute("Scale2", mScale2.ToString());
			if (!double.IsNaN(mScale3))
				xml.SetAttribute("Scale3", mScale3.ToString());
		}
	}
	public partial class IfcCenterLineProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Thickness"))
				Thickness = double.Parse(xml.Attributes["Thickness"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Thickness", mThickness.ToString());
		}
	}
	public partial class IfcChimney
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcChimneyTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcChimneyTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcChimneyType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcChimneyTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcChimneyTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcCircle
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Radius"))
				Radius = double.Parse(xml.Attributes["Radius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Radius", mRadius.ToString());
		}
	}
	public partial class IfcCircleHollowProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("WallThickness"))
				mWallThickness = double.Parse(xml.Attributes["WallThickness"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("WallThickness", mWallThickness.ToString());
		}
	}
	public partial class IfcCircleProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Radius"))
				mRadius = double.Parse(xml.Attributes["Radius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Radius", mRadius.ToString());
		}
	}
	public partial class IfcCircularArcSegment2D
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Radius"))
				double.TryParse(xml.Attributes["Radius"].Value, out mRadius);
			if (xml.HasAttribute("IsCCW"))
				bool.TryParse(xml.Attributes["IsCCW"].Value, out mIsCCW);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Radius", Radius.ToString());
			setAttribute(xml, "IsCCW", IsCCW.ToString());
		}
	}
	public partial class IfcClassification
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Source"))
				Source = xml.Attributes["Source"].Value;
			if (xml.HasAttribute("Edition"))
				Edition = xml.Attributes["Edition"].Value;
			if (xml.HasAttribute("EditionDate"))
				EditionDate = IfcDate.ParseSTEP(xml.Attributes["EditionDate"].Value);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			if (xml.HasAttribute("Location"))
				Specification = xml.Attributes["Location"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "HasReferences") == 0)
					HasReferences.AddRange(mDatabase.ParseXMLList<IfcClassificationReference>(child as XmlElement));
				else if (string.Compare(name, "Source", true) == 0)
					Source = child.InnerText;
				else if (string.Compare(name, "Edition", true) == 0)
					Edition = child.InnerText;
				else if (string.Compare(name, "EditionDate", true) == 0)
					mEditionDateSS = mDatabase.ParseXml<IfcCalendarDate>(child as XmlElement);
				else if (string.Compare(name, "Name", true) == 0)
					Name = child.InnerText;
				else if (string.Compare(name, "Description", true) == 0)
					Description = child.InnerText;
				else if (string.Compare(name, "Specification", true) == 0)
					Specification = child.InnerText;
				else if (string.Compare(name, "Location", true) == 0)
					Specification = child.InnerText;
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);

			setAttribute(xml, "Source", Source);
			setAttribute(xml, "Edition", Edition);
			if (EditionDate != DateTime.MinValue)
				xml.SetAttribute("EditionDate", IfcDate.FormatSTEP(EditionDate));
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			if(mDatabase != null && mDatabase.mRelease < ReleaseVersion.IFC4X3)
				setAttribute(xml, "Location", Specification);
			else
				setAttribute(xml, "Specification", Specification);
			// tokens

			XmlElement element = xml.OwnerDocument.CreateElement("HasReferences", mDatabase.mXmlNamespace);
			foreach (IfcClassificationReference cr in HasReferences)
				element.AppendChild(cr.GetXML(xml.OwnerDocument, "", this, processed));
			if (element.ChildNodes.Count > 0)
				xml.AppendChild(element);
		}
	}
	public partial class IfcClassificationReference
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			if (xml.HasAttribute("Sort"))
				Sort = xml.Attributes["Sort"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ReferencedSource") == 0)
					ReferencedSource = mDatabase.ParseXml<IfcClassificationReferenceSelect>(child as XmlElement);
				else if (string.Compare(name, "HasReferences") == 0)
					HasReferences.AddRange(mDatabase.ParseXMLList<IfcClassificationReference>(child as XmlElement));
				else if (string.Compare(name, "Description", true) == 0)
					Description = child.InnerText;
				else if (string.Compare(name, "Sort", true) == 0)
					Sort = child.InnerText;
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			IfcClassificationReferenceSelect referenced = ReferencedSource;
			if (referenced != null && referenced != host)
				xml.AppendChild((referenced as BaseClassIfc).GetXML(xml.OwnerDocument, "ReferencedSource", this, processed));
			setAttribute(xml, "Description", Description);
			setAttribute(xml, "Sort", Sort);

			XmlElement element = xml.OwnerDocument.CreateElement("HasReferences", mDatabase.mXmlNamespace);
			foreach (IfcClassificationReference cr in HasReferences)
				element.AppendChild(cr.GetXML(xml.OwnerDocument, "", this, processed));
			if (element.ChildNodes.Count > 0)
				xml.AppendChild(element);
		}
	}
	public partial class IfcClothoid 
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("ClothoidConstant", mClothoidConstant.ToString());
		}
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			string clothoidConstant = xml.GetAttribute("ClothoidConstant");
			if (!string.IsNullOrEmpty(clothoidConstant))
				double.TryParse(clothoidConstant, out mClothoidConstant);
		}
	}

	public partial class IfcColourRgb
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
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Red", true) == 0)
					mRed = double.Parse(child.InnerText);
				else if (string.Compare(name, "Green", true) == 0)
					mGreen = double.Parse(child.InnerText);
				else if (string.Compare(name, "Blue", true) == 0)
					mBlue = double.Parse(child.InnerText);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Red", mRed.ToString());
			xml.SetAttribute("Green", mGreen.ToString());
			xml.SetAttribute("Blue", mBlue.ToString());
		}
	}
	public partial class IfcColourSpecification
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Name", Name);
		}
	}
	public partial class IfcColumn
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcColumnTypeEnum>(xml.Attributes["PredefinedType"].Value, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcColumnTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcColumnType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcColumnTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcColumnTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcComplexPropertyTemplate
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("UsageName"))
				UsageName = xml.Attributes["UsageName"].Value;
			if (xml.HasAttribute("TemplateType"))
				Enum.TryParse<IfcComplexPropertyTemplateTypeEnum>(xml.Attributes["TemplateType"].Value, true, out mTemplateType);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "HasPropertyTemplates") == 0)
					mDatabase.ParseXMLList<IfcPropertyTemplate>(child).ForEach(x => AddPropertyTemplate(x));
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "UsageName", UsageName);
			if (mTemplateType != IfcComplexPropertyTemplateTypeEnum.NOTDEFINED)
				xml.SetAttribute("TemplateType", mTemplateType.ToString().ToLower());
			setChild(xml, "HasPropertyTemplates", HasPropertyTemplates.Values, processed);
		}
	}
	public partial class IfcCompositeCurve
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Segments") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcSegment s = mDatabase.ParseXml<IfcSegment>(cn as XmlElement);
						if (s != null)
							Segments.Add(s);
					}
				}
			}
			if (xml.HasAttribute("SelfIntersect "))
				Enum.TryParse<IfcLogicalEnum>(xml.Attributes["SelfIntersect"].Value, true, out mSelfIntersect);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Segments", mDatabase.mXmlNamespace);
			xml.AppendChild(element);
			foreach (var segment in Segments)
				element.AppendChild(segment.GetXML(xml.OwnerDocument, "", this, processed));
			setAttribute(xml, "SelfIntersect", mSelfIntersect.ToString().ToLower());
		}
	}
	public partial class IfcCompositeCurveSegment
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("SameSense"))
				bool.TryParse(xml.Attributes["SameSense"].Value, out mSameSense);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ParentCurve") == 0)
					ParentCurve = mDatabase.ParseXml<IfcBoundedCurve>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("SameSense", mSameSense.ToString().ToLower());
			xml.AppendChild(mParentCurve.GetXML(xml.OwnerDocument, "ParentCurve", this, processed));
		}
	}
	public partial class IfcCompositeProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Profiles") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcProfileDef p = mDatabase.ParseXml<IfcProfileDef>(cn as XmlElement);
						if (p != null)
							addProfile(p);
					}
				}
			}
			if (xml.HasAttribute("Label"))
				Label = xml.Attributes["Label"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Profiles", mDatabase.mXmlNamespace);
			xml.AppendChild(element);
			foreach (IfcProfileDef pd in Profiles)
				element.AppendChild(pd.GetXML(xml.OwnerDocument, "", this, processed));
			setAttribute(xml, "Label", Label);
		}
	}
	public partial class IfcConic
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
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild((mPosition as BaseClassIfc).GetXML(xml.OwnerDocument, "Position", this, processed));
		}
	}
	public partial class IfcConnectedFaceSet
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "CfsFaces") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcFace f = mDatabase.ParseXml<IfcFace>(cn as XmlElement);
						if (f != null)
							CfsFaces.Add(f);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("CfsFaces", mDatabase.mXmlNamespace);
			xml.AppendChild(element);
			foreach (IfcFace face in CfsFaces)
				element.AppendChild(face.GetXML(xml.OwnerDocument, "", host, processed));
		}
	}
	public partial class IfcContext
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
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcRepresentationContext rc = mDatabase.ParseXml<IfcRepresentationContext>(cn as XmlElement);
						if (rc != null)
							RepresentationContexts.Add(rc);
					}
				}
				else if (string.Compare(name, "UnitsInContext") == 0)
					UnitsInContext = mDatabase.ParseXml<IfcUnitAssignment>(child as XmlElement);
				else if (string.Compare(name, "IsDefinedBy") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcRelDefinesByProperties rd = mDatabase.ParseXml<IfcRelDefinesByProperties>(cn as XmlElement);
						if (rd != null)
							rd.RelatedObjects.Add(this);
					}
				}
				else if (string.Compare(name, "Declares") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcRelDeclares rd = mDatabase.ParseXml<IfcRelDeclares>(cn as XmlElement);
						if (rd != null)
							rd.RelatingContext = this;
					}
				}
			}
			if (mDatabase.Context == null || this as IfcProjectLibrary == null)
				mDatabase.SetContext(this);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			XmlElement repContexts = null;
			if (mRepresentationContexts.Count > 0)
			{
				repContexts = xml.OwnerDocument.CreateElement("RepresentationContexts", mDatabase.mXmlNamespace);
				foreach (IfcRepresentationContext rc in RepresentationContexts)
					repContexts.AppendChild(rc.GetXML(xml.OwnerDocument, "", this, processed));
			}
			base.SetXML(xml, host, processed);
			setAttribute(xml, "ObjectType", ObjectType);
			setAttribute(xml, "LongName", LongName);
			setAttribute(xml, "Phase", Phase);
			if (repContexts != null)
				xml.AppendChild(repContexts);
			if (mUnitsInContext != null)
				xml.AppendChild(UnitsInContext.GetXML(xml.OwnerDocument, "UnitsInContext", this, processed));
			if (mIsDefinedBy.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("IsDefinedBy", mDatabase.mXmlNamespace);
				xml.AppendChild(element);
				foreach (IfcRelDefines rd in IsDefinedBy)
					element.AppendChild(rd.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (mDeclares.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("Declares", mDatabase.mXmlNamespace);
				xml.AppendChild(element);
				foreach (IfcRelDeclares rd in Declares)
					element.AppendChild(rd.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public partial class IfcConversionBasedUnit
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
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Name", Name);
			xml.AppendChild(ConversionFactor.GetXML(xml.OwnerDocument, "ConversionFactor", this, processed));
		}
	}
	public partial class IfcConnectionCurveGeometry
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "CurveOnRelatingElement") == 0)
					CurveOnRelatingElement = mDatabase.ParseXml<IfcCurveOrEdgeCurve>(child as XmlElement);
				else if (string.Compare(name, "CurveOnRelatedElement") == 0)
					CurveOnRelatedElement = mDatabase.ParseXml<IfcCurveOrEdgeCurve>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild((CurveOnRelatingElement as BaseClassIfc).GetXML(xml.OwnerDocument, "CurveOnRelatingElement", this, processed));
			if(mCurveOnRelatedElement != null)
			xml.AppendChild((CurveOnRelatedElement as BaseClassIfc).GetXML(xml.OwnerDocument, "CurveOnRelatedElement", this, processed));
		}
	}
	public partial class IfcConstraint
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
				else if (string.Compare(name, "HasExternalReference") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcExternalReferenceRelationship r = mDatabase.ParseXml<IfcExternalReferenceRelationship>(cn as XmlElement);
						if (r != null)
							r.RelatedResourceObjects.Add(this);
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
							r.RelatedResourceObjects.Add(this);
					}
				}
			}
			if (xml.HasAttribute("UserDefinedGrade"))
				UserDefinedGrade = xml.Attributes["UserDefinedGrade"].Value;

		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			xml.SetAttribute("ConstraintGrade", mConstraintGrade.ToString().ToLower());
			setAttribute(xml, "ConstraintSource", ConstraintSource);
			if (mCreatingActor != null)
				xml.AppendChild((mCreatingActor as BaseClassIfc).GetXML(xml.OwnerDocument, "CreatingActor", host, processed));
			setAttribute(xml, "UserDefinedGrade", UserDefinedGrade);

			if (mHasExternalReference.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasExternalReference", mDatabase.mXmlNamespace);
				xml.AppendChild(element);
				foreach (IfcExternalReferenceRelationship r in HasExternalReference)
					element.AppendChild(r.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if(mPropertiesForConstraint.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("PropertiesForConstraint", mDatabase.mXmlNamespace);
				foreach (IfcResourceConstraintRelationship r in mPropertiesForConstraint)
				{
					if (host != r && !processed.ContainsKey(r.xmlId()))
						element.AppendChild(r.GetXML(xml.OwnerDocument, "", this, processed));
				}
				if (element.HasChildNodes)
					xml.AppendChild(element);
			}
			if (mHasConstraintRelationships.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasConstraintRelationships", mDatabase.mXmlNamespace);
				foreach (IfcResourceConstraintRelationship r in HasConstraintRelationships)
				{
					if (host != r)
						element.AppendChild(r.GetXML(xml.OwnerDocument, "", this, processed));
				}
				if (element.HasChildNodes)
					xml.AppendChild(element);
			}
		}
	}
	public partial class IfcConveyorSegment
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcConveyorSegmentTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			XmlAttribute predefinedType = xml.Attributes["PredefinedType"];
			if (predefinedType != null)
				Enum.TryParse<IfcConveyorSegmentTypeEnum>(predefinedType.Value, out mPredefinedType);
		}
	}
	public partial class IfcConveyorSegmentType
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			XmlAttribute predefinedType = xml.Attributes["PredefinedType"];
			if (predefinedType != null)
				Enum.TryParse<IfcConveyorSegmentTypeEnum>(predefinedType.Value, out mPredefinedType);
		}
	}
	public partial class IfcCoordinateOperation
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
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(TargetCRS.GetXML(xml.OwnerDocument, "TargetCRS", this, processed));
		}
	}
	public partial class IfcCoordinateReferenceSystem
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
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			setAttribute(xml, "GeodeticDatum", GeodeticDatum);
		}
	}
	public partial class IfcCosineSpiral
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("CosineTerm", mCosineTerm.ToString());
			if(double.IsNaN(mConstantTerm))
				xml.SetAttribute("ConstantTerm", mConstantTerm.ToString());
		}
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			string att = xml.GetAttribute("CosineTerm");
			if (!string.IsNullOrEmpty(att))
				double.TryParse(att, out mCosineTerm);
			att = xml.GetAttribute("Constant");
			if (!string.IsNullOrEmpty(att))
				double.TryParse(att, out mConstantTerm);
		}
	}
	public partial class IfcCourse
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcCourseTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			XmlAttribute predefinedType = xml.Attributes["PredefinedType"];
			if (predefinedType != null)
				Enum.TryParse<IfcCourseTypeEnum>(predefinedType.Value, out mPredefinedType);
		}
	}
	public partial class IfcCourseType
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			XmlAttribute predefinedType = xml.Attributes["PredefinedType"];
			if (predefinedType != null)
				Enum.TryParse<IfcCourseTypeEnum>(predefinedType.Value, out mPredefinedType);
		}
	}
	public partial class IfcCovering
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcCoveringTypeEnum>(xml.Attributes["PredefinedType"].Value,true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcCoveringTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcCoveringType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcCoveringTypeEnum>(xml.Attributes["PredefinedType"].Value,true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcCoveringTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcCsgPrimitive3D
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
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Position.GetXML(xml.OwnerDocument, "Position", this, processed));
		}
	}
	public partial class IfcCsgSolid
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
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild((mTreeRootExpression as BaseClassIfc).GetXML(xml.OwnerDocument, "TreeRootExpression", this, processed));
		}
	}
	public partial class IfcCShapeProfileDef
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
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
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
	public partial class IfcCurtainWall
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcCurtainWallTypeEnum>(xml.Attributes["PredefinedType"].Value,true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcCurtainWallTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcCurtainWallType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcCurtainWallTypeEnum>(xml.Attributes["PredefinedType"].Value,true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcCurtainWallTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcCurveBoundedPlane
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
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcCurve c = mDatabase.ParseXml<IfcCurve>(cn as XmlElement);
						if (c != null)
							InnerBoundaries.Add(c);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(BasisSurface.GetXML(xml.OwnerDocument, "BasisSurface", this, processed));
			xml.AppendChild(OuterBoundary.GetXML(xml.OwnerDocument, "OuterBoundary", this, processed));
			if (mInnerBoundaries.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("InnerBoundaries", mDatabase.mXmlNamespace);
				xml.AppendChild(element);
				foreach (IfcCurve c in InnerBoundaries)
					element.AppendChild(c.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public partial class IfcCurveSegment
	{
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Placement.GetXML(xml.OwnerDocument, "Placement", this, processed));
			xml.AppendChild(convert(xml.OwnerDocument, SegmentStart as IfcValue, "SegmentStart", mDatabase.mXmlNamespace));
			xml.AppendChild(convert(xml.OwnerDocument, SegmentLength as IfcValue, "SegmentLength", mDatabase.mXmlNamespace));
			xml.AppendChild(ParentCurve.GetXML(xml.OwnerDocument, "ParentCurve", this, processed));
		}
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Placement", true) == 0)
					Placement = mDatabase.ParseXml<IfcPlacement>(child as XmlElement);
				else if (string.Compare(name, "SegmentStart", true) == 0)
					SegmentStart = extractValue(child.FirstChild) as IfcCurveMeasureSelect;
				else if (string.Compare(name, "SegmentLength", true) == 0)
					SegmentLength = extractValue(child.FirstChild) as IfcCurveMeasureSelect;
				else if (string.Compare(name, "ParentCurve", true) == 0)
					ParentCurve = mDatabase.ParseXml<IfcCurve>(child as XmlElement);
			}
		}
	}
	public partial class IfcCurveSegment2D
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("StartDirection"))
				double.TryParse(xml.Attributes["StartDirection"].Value, out mStartDirection);
			if (xml.HasAttribute("SegmentLength"))
				double.TryParse(xml.Attributes["SegmentLength"].Value, out mSegmentLength);
			foreach (XmlNode child in xml.ChildNodes)
			{
				if (string.Compare(child.Name, "StartPoint") == 0)
					StartPoint = mDatabase.ParseXml<IfcCartesianPoint>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(StartPoint.GetXML(xml.OwnerDocument, "StartPoint", this, processed));
			setAttribute(xml, "StartDirection", StartDirection.ToString());
			setAttribute(xml, "SegmentLength", SegmentLength.ToString());
		}
	}
	public partial class IfcCylindricalSurface
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Radius"))
				mRadius = double.Parse(xml.Attributes["Radius"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Radius", mRadius.ToString());
		}
	}
}
