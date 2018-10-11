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
	public partial class IfcTable : BaseClassIfc, IfcMetricValueSelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Rows") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcTableRow r = mDatabase.ParseXml<IfcTableRow>(node as XmlElement);
						if (r != null)
							addRow(r);
					}
				}
				else if (string.Compare(name, "Columns") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcTableColumn c = mDatabase.ParseXml<IfcTableColumn>(node as XmlElement);
						if (c != null)
							addColumn(c);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Name", Name);
			if (mRows.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("Rows");
				xml.AppendChild(element);
				foreach (IfcTableRow r in Rows)
					element.AppendChild(r.GetXML(xml.OwnerDocument, "", null, processed));
			}
			if (mColumns.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("Columns");
				xml.AppendChild(element);
				foreach (IfcTableColumn c in Columns)
					element.AppendChild(c.GetXML(xml.OwnerDocument, "", this, processed));
			}
		}
	}
	public partial class IfcTableColumn : BaseClassIfc
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Identifier"))
				Identifier = xml.Attributes["Identifier"].Value;
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			if (xml.HasAttribute("Description"))
				Name = xml.Attributes["Description"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Unit") == 0)
					Unit = mDatabase.ParseXml<IfcUnit>(child as XmlElement);
				else if (string.Compare(name, "ReferencePath") == 0)
					ReferencePath = mDatabase.ParseXml<IfcReference>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Identifier", Identifier);
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			if (mUnit > 0)
				xml.AppendChild(mDatabase[mUnit].GetXML(xml.OwnerDocument, "Unit", this, processed));
			if (mReferencePath > 0)
				xml.AppendChild(ReferencePath.GetXML(xml.OwnerDocument, "ReferencePath",this,processed));
		}
	}
	public partial class IfcTableRow : BaseClassIfc
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RowCells") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcValue v = extractValue(child);
						if (v != null)
							mRowCells.Add(v);
					}
				}
			}
			if (xml.HasAttribute("IsHeading"))
				mIsHeading = bool.Parse(xml.Attributes["IsHeading"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if(mRowCells.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("RowCells");
				xml.AppendChild(element);
				foreach (IfcValue v in RowCells)
					element.AppendChild(convert(xml.OwnerDocument, v, "IfcValue"));
			}
			xml.SetAttribute("IsHeading", mIsHeading.ToString().ToLower());
		}
	}
	public abstract partial class IfcTessellatedFaceSet : IfcTessellatedItem, IfcBooleanOperand //ABSTRACT SUPERTYPE OF(IfcTriangulatedFaceSet)
	{
		//internal int mCoordinates;// : 	IfcCartesianPointList;
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Coordinates.GetXML(xml.OwnerDocument, "Coordinates", this, processed));
		}
	}
	public partial class IfcTransitionCurveSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("StartRadius"))
				double.TryParse(xml.Attributes["StartRadius"].Value, out mStartRadius);
			if (xml.HasAttribute("EndRadius"))
				double.TryParse(xml.Attributes["EndRadius"].Value, out mEndRadius);
			if (xml.HasAttribute("IsStartRadiusCCW"))
				bool.TryParse(xml.Attributes["IsStartRadiusCCW"].Value, out mIsStartRadiusCCW);
			if (xml.HasAttribute("IsEndRadiusCCW"))
				bool.TryParse(xml.Attributes["IsEndRadiusCCW"].Value, out mIsEndRadiusCCW);
			if (xml.HasAttribute("TransitionCurveType"))
				Enum.TryParse<IfcTransitionCurveType>(xml.Attributes["TransitionCurveType"].Value, out mTransitionCurveType);

		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "StartRadius", StartRadius.ToString());
			setAttribute(xml, "EndRadius", EndRadius.ToString());
			setAttribute(xml, "IsStartRadiusCCW", IsStartRadiusCCW.ToString());
			setAttribute(xml, "IsEndRadiusCCW", IsEndRadiusCCW.ToString());
			setAttribute(xml, "TransitionCurveType", TransitionCurveType.ToString());
		}
	}
	public partial class IfcTrapeziumProfileDef : IfcParameterizedProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("BottomXDim"))
				mBottomXDim = double.Parse(xml.Attributes["BottomXDim"].Value);
			if (xml.HasAttribute("TopXDim"))
				mTopXDim = double.Parse(xml.Attributes["TopXDim"].Value);
			if (xml.HasAttribute("YDim"))
				mYDim = double.Parse(xml.Attributes["YDim"].Value);
			if (xml.HasAttribute("TopXOffset"))
				mTopXOffset = double.Parse(xml.Attributes["TopXOffset"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("BottomXDim", mBottomXDim.ToString());
			xml.SetAttribute("TopXDim", mTopXDim.ToString());
			xml.SetAttribute("YDim", mYDim.ToString());
			xml.SetAttribute("TopXOffset", mTopXOffset.ToString());
		}
	}
	public partial class IfcTriangulatedFaceSet : IfcTessellatedFaceSet
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Normals"))
			{
				string[] fields = xml.Attributes["CoordList"].Value.Split(" ".ToCharArray());
				List<double[]> normals = new List<double[]>(fields.Length/3);
				for (int icounter = 0; icounter < fields.Length; icounter += 3)
					normals.Add(new double[] { double.Parse(fields[icounter]), double.Parse(fields[icounter + 1]), double.Parse(fields[icounter + 2]) });
				mNormals = normals.ToArray();
			}
			if (xml.HasAttribute("Closed"))
				mClosed = bool.Parse(xml.Attributes["Closed"].Value) ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE;
			if (xml.HasAttribute("CoordIndex"))
			{
				string[] fields = xml.Attributes["CoordIndex"].Value.Split(" ".ToCharArray());
				mCoordIndex = new Tuple<int, int, int>[fields.Length / 3];
				int pos = 0;
				for (int icounter = 0; icounter < fields.Length; icounter += 3)
					mCoordIndex[pos++] = new Tuple<int, int, int>(int.Parse(fields[icounter]), int.Parse(fields[icounter + 1]), int.Parse(fields[icounter + 2]));
			}
			if (xml.HasAttribute("CoordIndex"))
			{
				string[] fields = xml.Attributes["CoordIndex"].Value.Split(" ".ToCharArray());
				mCoordIndex = new Tuple<int, int, int>[fields.Length / 3];
				int pos = 0;
				for (int icounter = 0; icounter < fields.Length; icounter += 3)
					mCoordIndex[pos++] = new Tuple<int, int, int>(int.Parse(fields[icounter]), int.Parse(fields[icounter + 1]), int.Parse(fields[icounter + 2]));
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mNormals != null && mNormals.Length > 0)
				xml.SetAttribute("Normals", string.Join(" ", mNormals.Select(x => x[0] + " " + x[1] + " " + x[2])));
			if (mClosed != IfcLogicalEnum.UNKNOWN)
				xml.SetAttribute("Closed", (mClosed == IfcLogicalEnum.TRUE).ToString().ToLower());
			Tuple<int, int, int> coord = mCoordIndex[0];
			string coords = coord.Item1 + " " + coord.Item2 + " " + coord.Item3;
			for (int icounter = 1; icounter < mCoordIndex.Length; icounter++)
			{
				coord = mCoordIndex[icounter];
				coords += " " + coord.Item1 + " " + coord.Item2 + " " + coord.Item3;
			}
			xml.SetAttribute("CoordIndex", coords);
			if (mNormalIndex != null && mNormalIndex.Length > 0)
			{
				Tuple<int, int, int> normal = mNormalIndex[0];
				string normals = normal.Item1 + " " + normal.Item2 + " " + normal.Item3;
				for (int icounter = 1; icounter < mNormalIndex.Length; icounter++)
				{
					normal = mCoordIndex[icounter];
					normals += " " + normal.Item1 + " " + normal.Item2 + " " + normal.Item3;
				}
				xml.SetAttribute("NormalIndex", normals);
			}

		}
	}
	public partial class IfcTrimmedCurve : IfcBoundedCurve
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "BasisCurve") == 0)
					BasisCurve = mDatabase.ParseXml<IfcCurve>(child as XmlElement);
				else if (string.Compare(name, "Trim1") == 0)
					mTrim1 = IfcTrimmingSelect.Parse(child as XmlElement, mDatabase);
				else if (string.Compare(name, "Trim2") == 0)
					mTrim2 = IfcTrimmingSelect.Parse(child as XmlElement, mDatabase);
			}
			if (xml.HasAttribute("SenseAgreement"))
				bool.TryParse(xml.Attributes["SenseAgreement"].Value, out mSenseAgreement);
			if (xml.HasAttribute("MasterRepresentation"))
				Enum.TryParse<IfcTrimmingPreference>(xml.Attributes["MasterRepresentation"].Value, true, out mMasterRepresentation);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(BasisCurve.GetXML(xml.OwnerDocument, "BasisCurve", this, processed));
			xml.AppendChild(mTrim1.getXML(xml.OwnerDocument, "Trim1", processed, mDatabase));
			xml.AppendChild(mTrim2.getXML(xml.OwnerDocument, "Trim2", processed, mDatabase));
			xml.SetAttribute("SenseAgreement", mSenseAgreement.ToString().ToLower());
			xml.SetAttribute("MasterRepresentation", mMasterRepresentation.ToString().ToLower());
		}
	}
	public partial class IfcTrimmingSelect
	{
		internal static IfcTrimmingSelect Parse(XmlElement xml, DatabaseIfc db)
		{
			IfcTrimmingSelect result = new IfcTrimmingSelect();
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "IfcParameterValue-wrapper") == 0)
					result.mIfcParameterValue = double.Parse(child.InnerText);
				else if (string.Compare(name, "IfcCartesianPoint") == 0)
				{
					IfcCartesianPoint p = db.ParseXml<IfcCartesianPoint>(child as XmlElement);
					if (p != null)
						result.mIfcCartesianPoint = p.mIndex;
				}
			}
			return result;
		}
		internal XmlElement getXML(XmlDocument doc, string name, Dictionary<int, XmlElement> processed, DatabaseIfc db)
		{
			XmlElement result = doc.CreateElement(name);
			if (!double.IsNaN(mIfcParameterValue))
			{
				XmlElement element = doc.CreateElement("IfcParameterValue-wrapper");
				element.InnerText = mIfcParameterValue.ToString();
				result.AppendChild(element);
			}
			if (mIfcCartesianPoint > 0)
				result.AppendChild(db[mIfcCartesianPoint].GetXML(doc, "", null, processed));
			return result;
		}
	}
	public partial class IfcTShapeProfileDef : IfcParameterizedProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Depth"))
				mDepth = double.Parse(xml.Attributes["Depth"].Value);
			if (xml.HasAttribute("FlangeWidth"))
				mFlangeWidth = double.Parse(xml.Attributes["FlangeWidth"].Value);
			if (xml.HasAttribute("WebThickness"))
				mWebThickness = double.Parse(xml.Attributes["WebThickness"].Value);
			if (xml.HasAttribute("FlangeThickness"))
				mFlangeThickness = double.Parse(xml.Attributes["FlangeThickness"].Value);
			if (xml.HasAttribute("FilletRadius"))
				mFilletRadius = double.Parse(xml.Attributes["FilletRadius"].Value);
			if (xml.HasAttribute("FlangeEdgeRadius"))
				mFlangeEdgeRadius = double.Parse(xml.Attributes["FlangeEdgeRadius"].Value);
			if (xml.HasAttribute("WebEdgeRadius"))
				mWebEdgeRadius = double.Parse(xml.Attributes["WebEdgeRadius"].Value);
			if (xml.HasAttribute("WebSlope"))
				mWebSlope = double.Parse(xml.Attributes["WebSlope"].Value);
			if (xml.HasAttribute("FlangeSlope"))
				mFlangeSlope = double.Parse(xml.Attributes["FlangeSlope"].Value);
			if (xml.HasAttribute("CentreOfGravityInX"))
				mCentreOfGravityInX = double.Parse(xml.Attributes["CentreOfGravityInX"].Value);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.SetAttribute("Depth", mDepth.ToString());
			xml.SetAttribute("FlangeWidth", mFlangeWidth.ToString());
			xml.SetAttribute("WebThickness", mWebThickness.ToString());
			xml.SetAttribute("FlangeThickness", mFlangeThickness.ToString());
			if (!double.IsNaN(mFilletRadius))
				xml.SetAttribute("FilletRadius", mFilletRadius.ToString());
			if (!double.IsNaN(mFlangeEdgeRadius))
				xml.SetAttribute("FlangeEdgeRadius", mFlangeEdgeRadius.ToString());
			if (!double.IsNaN(mWebEdgeRadius))
				xml.SetAttribute("WebEdgeRadius", mWebEdgeRadius.ToString());
			if (!double.IsNaN(mWebSlope))
				xml.SetAttribute("WebSlope", mWebSlope.ToString());
			if (!double.IsNaN(mFlangeSlope))
				xml.SetAttribute("FlangeSlope", mFlangeSlope.ToString());
			if (mDatabase.Release < ReleaseVersion.IFC4 && !double.IsNaN(mCentreOfGravityInX))
				xml.SetAttribute("CentreOfGravityInX", mCentreOfGravityInX.ToString());
		}
	}
	public partial class IfcTypeObject : IfcObjectDefinition //(IfcTypeProcess, IfcTypeProduct, IfcTypeResource) IFC4 ABSTRACT 
	{
		//internal List<int> mHasPropertySets = new List<int>();// : OPTIONAL SET [1:?] OF IfcPropertySetDefinition 
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("ApplicableOccurrence"))
				ApplicableOccurrence = xml.Attributes["ApplicableOccurrence"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "HasPropertySets") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcPropertySetDefinition ps = mDatabase.ParseXml<IfcPropertySetDefinition>(cn as XmlElement);
						if (ps != null)
							HasPropertySets.Add(ps);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			XmlElement element = xml.OwnerDocument.CreateElement("HasPropertySets");
			if (mHasPropertySets.Count > 0)
			{
				foreach (IfcPropertySetDefinition item in HasPropertySets)
					element.AppendChild(item.GetXML(xml.OwnerDocument, "", this, processed));
			}

			base.SetXML(xml, host, processed);
			setAttribute(xml, "ApplicableOccurrence", ApplicableOccurrence);
			if(element.HasChildNodes)
				xml.AppendChild(element);
		}
	}	
	public partial class IfcTypeProduct : IfcTypeObject, IfcProductSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcDoorStyle ,IfcElementType ,IfcSpatialElementType ,IfcWindowStyle)) 
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RepresentationMaps") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcRepresentationMap rm = mDatabase.ParseXml<IfcRepresentationMap>(cn as XmlElement);
						if (rm != null)
							RepresentationMaps.Add(rm);
					}
				}
			}
			if (xml.HasAttribute("Tag"))
				Tag = xml.Attributes["Tag"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mRepresentationMaps.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("RepresentationMaps");
				xml.AppendChild(element);

				foreach (IfcRepresentationMap item in RepresentationMaps)
					element.AppendChild(item.GetXML(xml.OwnerDocument, "", this, processed));
			}
			setAttribute(xml, "Tag", Tag);
		}
	}
}
