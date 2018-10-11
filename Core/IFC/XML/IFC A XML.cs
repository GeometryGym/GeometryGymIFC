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
	public partial class IfcActor : IfcObject // SUPERTYPE OF(IfcOccupant)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "TheActor") == 0)
					TheActor = mDatabase.ParseXml<IfcActorSelect>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (host != mTheActor)
				xml.AppendChild((TheActor as BaseClassIfc).GetXML(xml.OwnerDocument, "TheActor", this, processed));
		}
	}
	public partial class IfcActorRole : BaseClassIfc, IfcResourceObjectSelect
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Role"))
				Enum.TryParse<IfcRoleEnum>(xml.Attributes["Role"].Value, true, out mRole);
			if (xml.HasAttribute("UserDefinedRole"))
				UserDefinedRole = xml.Attributes["UserDefinedRole"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mRole != IfcRoleEnum.NOTDEFINED)
				xml.SetAttribute("Role", mRole.ToString().ToLower());
			setAttribute(xml, "UserDefinedRole", UserDefinedRole);
			setAttribute(xml, "Description", Description);
		}
	}
	public abstract partial class IfcAddress : BaseClassIfc     //ABSTRACT SUPERTYPE OF(ONEOF(IfcPostalAddress, IfcTelecomAddress));
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Purpose"))
				Purpose = (IfcAddressTypeEnum)Enum.Parse(typeof(IfcAddressTypeEnum), xml.Attributes["Purpose"].Value);
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			if (xml.HasAttribute("UserDefinedPurpose"))
				UserDefinedPurpose = xml.Attributes["UserDefinedPurpose"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPurpose != IfcAddressTypeEnum.NOTDEFINED)
				xml.SetAttribute("Purpose", mPurpose.ToString().ToLower());
			setAttribute(xml, "Description", Description);
			setAttribute(xml, "UserDefinedPurpose", UserDefinedPurpose);
		}
	}
	public partial class IfcAdvancedBrepWithVoids : IfcAdvancedBrep
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Voids") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcClosedShell s = mDatabase.ParseXml<IfcClosedShell>(cn as XmlElement);
						if (s != null)
							mVoids.Add(s);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("Voids");
			xml.AppendChild(element);
			foreach (IfcClosedShell s in Voids)
				element.AppendChild(s.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcAirTerminal : IfcFlowTerminal
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcAirTerminalTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcAirTerminalTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcAirTerminalType : IfcFlowTerminalType
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcAirTerminalTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcAirTerminalTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcAlignment : IfcLinearPositioningElement //IFC4.1
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("PredefinedType"))
				Enum.TryParse<IfcAlignmentTypeEnum>(xml.Attributes["PredefinedType"].Value, true, out mPredefinedType);
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mPredefinedType != IfcAlignmentTypeEnum.NOTDEFINED)
				xml.SetAttribute("PredefinedType", mPredefinedType.ToString().ToLower());
		}
	}
	public partial class IfcAlignment2DHorizontal : IfcGeometricRepresentationItem //IFC4.1
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("StartDistAlong"))
				double.TryParse(xml.Attributes["StartDistAlong"].Value, out mStartDistAlong);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Segments") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcAlignment2DHorizontalSegment s = mDatabase.ParseXml<IfcAlignment2DHorizontalSegment>(cn as XmlElement);
						if (s != null)
							Segments.Add(s);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);

			if (!double.IsNaN(mStartDistAlong))
				setAttribute(xml, "StartDistAlong", mStartDistAlong.ToString());
			XmlElement element = xml.OwnerDocument.CreateElement("Segments");
			xml.AppendChild(element);
			foreach (IfcAlignment2DHorizontalSegment s in Segments)
				element.AppendChild(s.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcAlignment2DHorizontalSegment : IfcAlignment2DSegment //IFC4.1
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "CurveGeometry") == 0)
					CurveGeometry = mDatabase.ParseXml<IfcCurveSegment2D>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(CurveGeometry.GetXML(xml.OwnerDocument, "CurveGeometry", this, processed));
		}
	}
	public abstract partial class IfcAlignment2DSegment : BaseClassIfc //IFC4.1 ABSTRACT SUPERTYPE OF(ONEOF(IfcAlignment2DHorizontalSegment, IfcAlignment2DVerticalSegment))
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("TangentialContinuity"))
				TangentialContinuity = Convert.ToBoolean(xml.Attributes["TangentialContinuity"].Value);
			if (xml.HasAttribute("StartTag"))
				StartTag = xml.Attributes["StartTag"].Value;
			if (xml.HasAttribute("StartTag"))
				StartTag = xml.Attributes["StartTag"].Value;
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);

			if (mTangentialContinuity != IfcLogicalEnum.UNKNOWN)
				setAttribute(xml, "TangentialContinuity", mTangentialContinuity == IfcLogicalEnum.TRUE ? "true" : "false");
			setAttribute(xml, "StartTag", StartTag);
			setAttribute(xml, "EndTag", EndTag);
		}
	}
	public partial class IfcAlignment2DVerSegCircularArc : IfcAlignment2DVerticalSegment  //IFC4.1
	{

	}
	public partial class IfcAlignmentCurve : IfcBoundedCurve //IFC4.1
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Tag"))
				Tag = xml.Attributes["Tag"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Horizontal") == 0)
					Horizontal = mDatabase.ParseXml<IfcAlignment2DHorizontal>(child as XmlElement);
				else if (string.Compare(name, "Vertical") == 0)
					Vertical = mDatabase.ParseXml<IfcAlignment2DVertical>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mHorizontal != null)
				xml.AppendChild(Horizontal.GetXML(xml.OwnerDocument, "Horizontal", this, processed));
			if (mVertical != null)
				xml.AppendChild(Vertical.GetXML(xml.OwnerDocument, "Vertical", this, processed));
			setAttribute(xml, "Tag", Tag);
		}
	}
	public partial class IfcAnnotationFillArea : IfcGeometricRepresentationItem
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "OuterBoundary") == 0)
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
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
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
	public partial class IfcApplication : BaseClassIfc
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Version"))
				Version = xml.Attributes["Version"].Value;
			if (xml.HasAttribute("ApplicationFullName"))
				ApplicationFullName = xml.Attributes["ApplicationFullName"].Value;
			if (xml.HasAttribute("ApplicationIdentifier"))
				ApplicationIdentifier = xml.Attributes["ApplicationIdentifier"].Value;

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "ApplicationDeveloper") == 0)
					ApplicationDeveloper = mDatabase.ParseXml<IfcOrganization>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(ApplicationDeveloper.GetXML(xml.OwnerDocument, "ApplicationDeveloper", this, processed));
			xml.SetAttribute("Version", Version);
			xml.SetAttribute("ApplicationFullName", ApplicationFullName);
			xml.SetAttribute("ApplicationIdentifier", ApplicationIdentifier);
		}
	}
	public partial class IfcAppliedValue : BaseClassIfc, IfcMetricValueSelect, IfcAppliedValueSelect, IfcResourceObjectSelect //SUPERTYPE OF(IfcCostValue);
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("Name"))
				Name = xml.Attributes["Name"].Value;
			if (xml.HasAttribute("Description"))
				Description = xml.Attributes["Description"].Value;
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "AppliedValue") == 0)
				{
					AppliedValue = mDatabase.ParseXml<IfcAppliedValueSelect>(child as XmlElement);
					if (mAppliedValueIndex == 0)
						mAppliedValueValue = extractValue(child.FirstChild);
				}
				else if (string.Compare(name, "UnitBasis") == 0)
					UnitBasis = mDatabase.ParseXml<IfcMeasureWithUnit>(child as XmlElement);
				else if (string.Compare(name, "Components") == 0)
				{
					foreach (XmlNode node in child.ChildNodes)
					{
						IfcAppliedValue v = mDatabase.ParseXml<IfcAppliedValue>(node as XmlElement);
						if (v != null)
							addComponent(v);
					}
				}
				else if (string.Compare(name, "HasExternalReferences") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcExternalReferenceRelationship r = mDatabase.ParseXml<IfcExternalReferenceRelationship>(cn as XmlElement);
						if (r != null)
							r.RelatedResourceObjects.Add(this);
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
			//todo
			if (xml.HasAttribute("Category"))
				Category = xml.Attributes["Category"].Value;
			if (xml.HasAttribute("Condition"))
				Condition = xml.Attributes["Condition"].Value;
			if (xml.HasAttribute("ArithmeticOperator"))
				Enum.TryParse<IfcArithmeticOperatorEnum>(xml.Attributes["ArithmeticOperator"].Value, true, out mArithmeticOperator);


		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			setAttribute(xml, "Name", Name);
			setAttribute(xml, "Description", Description);
			if (mAppliedValueIndex > 0 && (host == null || host.mIndex != mAppliedValueIndex))
				xml.AppendChild(mDatabase[mAppliedValueIndex].GetXML(xml.OwnerDocument, "AppliedValue", this, processed));
			else if (mAppliedValueValue != null)
				xml.AppendChild(convert(xml.OwnerDocument, mAppliedValueValue, "AppliedValue"));
			if (mUnitBasis > 0)
				xml.AppendChild(UnitBasis.GetXML(xml.OwnerDocument, "UnitBasis", this, processed));
			//todo
			setAttribute(xml, "Category", Category);
			setAttribute(xml, "Condition", Condition);
			if (mArithmeticOperator != IfcArithmeticOperatorEnum.NONE)
				xml.SetAttribute("ArithmeticOperator", mArithmeticOperator.ToString().ToLower());
			if (mComponents.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("Components");
				xml.AppendChild(element);
				foreach (IfcAppliedValue v in Components)
					element.AppendChild(v.GetXML(xml.OwnerDocument, "", this, processed));
			}

			if (mHasExternalReferences.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasExternalReferences");
				xml.AppendChild(element);
				foreach (IfcExternalReferenceRelationship r in HasExternalReferences)
					element.AppendChild(r.GetXML(xml.OwnerDocument, "", this, processed));
			}
			if (mHasConstraintRelationships.Count > 0)
			{
				XmlElement element = xml.OwnerDocument.CreateElement("HasConstraintRelationships");
				foreach (IfcResourceConstraintRelationship r in HasConstraintRelationships)
				{
					if (r.mIndex != host.mIndex)
						element.AppendChild(r.GetXML(xml.OwnerDocument, "", this, processed));
				}
				if (element.HasChildNodes)
					xml.AppendChild(element);
			}
		}
	}
	public partial class IfcArbitraryClosedProfileDef : IfcProfileDef //SUPERTYPE OF(IfcArbitraryProfileDefWithVoids)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "OuterCurve") == 0)
					OuterCurve = mDatabase.ParseXml<IfcBoundedCurve>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(OuterCurve.GetXML(xml.OwnerDocument, "OuterCurve", this, processed));
		}
	}
	public partial class IfcArbitraryOpenProfileDef : IfcProfileDef //	SUPERTYPE OF(IfcCenterLineProfileDef)
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Curve") == 0)
					Curve = mDatabase.ParseXml<IfcBoundedCurve>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			xml.AppendChild(Curve.GetXML(xml.OwnerDocument, "Curve", this, processed));
		}
	}
	public partial class IfcArbitraryProfileDefWithVoids : IfcArbitraryClosedProfileDef
	{
		//private List<int> mInnerCurves = new List<int>();// : SET [1:?] OF IfcCurve;
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "InnerCurves") == 0)
				{
					foreach (XmlNode cn in child.ChildNodes)
					{
						IfcCurve c = mDatabase.ParseXml<IfcCurve>(cn as XmlElement);
						if (c != null)
							addVoid(c);
					}
				}
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			XmlElement element = xml.OwnerDocument.CreateElement("InnerCurves");
			xml.AppendChild(element);
			foreach (IfcCurve c in InnerCurves)
				element.AppendChild(c.GetXML(xml.OwnerDocument, "", this, processed));
		}
	}
	public partial class IfcAsymmetricIShapeProfileDef : IfcParameterizedProfileDef //IFC4 IfcParameterizedProfileDef
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			if (xml.HasAttribute("OverallWidth"))
				mBottomFlangeWidth = double.Parse(xml.Attributes["OverallWidth"].Value);
			else if (xml.HasAttribute("BottomFlangeWidth"))
				mBottomFlangeWidth = double.Parse(xml.Attributes["BottomFlangeWidth"].Value);
			if (xml.HasAttribute("OverallDepth"))
				mOverallDepth = double.Parse(xml.Attributes["OverallDepth"].Value);
			if (xml.HasAttribute("WebThickness"))
				mWebThickness = double.Parse(xml.Attributes["WebThickness"].Value);
			if (xml.HasAttribute("OverallWidth"))
				mBottomFlangeWidth = double.Parse(xml.Attributes["OverallWidth"].Value);
			else if (xml.HasAttribute("BottomFlangeWidth"))
				mBottomFlangeWidth = double.Parse(xml.Attributes["BottomFlangeWidth"].Value);
			if (xml.HasAttribute("FlangeThickness"))
				mBottomFlangeThickness = double.Parse(xml.Attributes["FlangeThickness"].Value);
			else if (xml.HasAttribute("BottomFlangeThickness"))
				mBottomFlangeThickness = double.Parse(xml.Attributes["BottomFlangeThickness"].Value);
			if (xml.HasAttribute("FilletRadius"))
				mBottomFlangeFilletRadius = double.Parse(xml.Attributes["FilletRadius"].Value);
			else if (xml.HasAttribute("BottomFlangeFilletRadius"))
				mBottomFlangeFilletRadius = double.Parse(xml.Attributes["BottomFlangeFilletRadius"].Value);
			if (xml.HasAttribute("TopFlangeWidth"))
				mTopFlangeWidth = double.Parse(xml.Attributes["TopFlangeWidth"].Value);
			if (xml.HasAttribute("TopFlangeThickness"))
				mTopFlangeThickness = double.Parse(xml.Attributes["TopFlangeThickness"].Value);
			if (xml.HasAttribute("TopFlangeFilletRadius"))
				mTopFlangeFilletRadius = double.Parse(xml.Attributes["TopFlangeFilletRadius"].Value);
			if (xml.HasAttribute("BottomFlangeEdgeRadius"))
				mBottomFlangeEdgeRadius = double.Parse(xml.Attributes["BottomFlangeEdgeRadius"].Value);
			if (xml.HasAttribute("BottomFlangeSlope"))
				mBottomFlangeSlope = double.Parse(xml.Attributes["BottomFlangeSlope"].Value);
			if (xml.HasAttribute("TopFlangeEdgeRadius"))
				mTopFlangeEdgeRadius = double.Parse(xml.Attributes["TopFlangeEdgeRadius"].Value);
			if (xml.HasAttribute("TopFlangeSlope"))
				mTopFlangeSlope = double.Parse(xml.Attributes["TopFlangeSlope"].Value);
			if (xml.HasAttribute("CentreOfGravityInY"))
				mCentreOfGravityInY = double.Parse(xml.Attributes["CentreOfGravityInY"].Value);

		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if(mDatabase.Release < ReleaseVersion.IFC4)
			{
				xml.SetAttribute("OverallWidth", mBottomFlangeWidth.ToString());
				xml.SetAttribute("OverallDepth", mOverallDepth.ToString());
				xml.SetAttribute("WebThickness", mWebThickness.ToString());
				xml.SetAttribute("FlangeThickness", mBottomFlangeThickness.ToString());
				if (!double.IsNaN(mBottomFlangeFilletRadius))
					xml.SetAttribute("FilletRadius", mBottomFlangeFilletRadius.ToString());
				if (!double.IsNaN(mCentreOfGravityInY))
					xml.SetAttribute("CentreOfGravityInY", mCentreOfGravityInY.ToString());
			}
			else
			{
				xml.SetAttribute("BottomFlangeWidth", mBottomFlangeWidth.ToString());
				xml.SetAttribute("OverallDepth", mOverallDepth.ToString());
				xml.SetAttribute("WebThickness", mWebThickness.ToString());
				xml.SetAttribute("BottomFlangeThickness", mBottomFlangeThickness.ToString());
				if (!double.IsNaN(mBottomFlangeFilletRadius))
					xml.SetAttribute("BottomFlangeFilletRadius", mBottomFlangeFilletRadius.ToString());
			}
			xml.SetAttribute("TopFlangeWidth", mTopFlangeWidth.ToString());
			xml.SetAttribute("TopFlangeThickness", mTopFlangeThickness.ToString());
			if (!double.IsNaN(mTopFlangeFilletRadius))
				xml.SetAttribute("TopFlangeFilletRadius", mTopFlangeFilletRadius.ToString());
			if (!double.IsNaN(mBottomFlangeEdgeRadius))
				xml.SetAttribute("BottomFlangeEdgeRadius", mBottomFlangeEdgeRadius.ToString());
			if (!double.IsNaN(mBottomFlangeSlope))
				xml.SetAttribute("BottomFlangeSlope", mBottomFlangeSlope.ToString());
			if (!double.IsNaN(mTopFlangeEdgeRadius))
				xml.SetAttribute("TopFlangeEdgeRadius", mTopFlangeEdgeRadius.ToString());
			if (!double.IsNaN(mTopFlangeSlope))
				xml.SetAttribute("TopFlangeSlope", mTopFlangeSlope.ToString());
		}
	}
	public partial class IfcAxis1Placement : IfcPlacement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);

			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Axis") == 0)
					Axis = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mAxis > 0)
				xml.AppendChild(Axis.GetXML(xml.OwnerDocument, "Axis", this, processed));
		}
	}
	public partial class IfcAxis2Placement2D : IfcPlacement, IfcAxis2Placement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "RefDirection") == 0)
					RefDirection = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mRefDirection != null)
				xml.AppendChild(RefDirection.GetXML(xml.OwnerDocument, "RefDirection", this, processed));
		}
	}
	public partial class IfcAxis2Placement3D : IfcPlacement, IfcAxis2Placement
	{
		internal override void ParseXml(XmlElement xml)
		{
			base.ParseXml(xml);
			foreach (XmlNode child in xml.ChildNodes)
			{
				string name = child.Name;
				if (string.Compare(name, "Axis") == 0)
					Axis = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
				else if (string.Compare(name, "RefDirection") == 0)
					RefDirection = mDatabase.ParseXml<IfcDirection>(child as XmlElement);
			}
		}
		internal override void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<int, XmlElement> processed)
		{
			base.SetXML(xml, host, processed);
			if (mAxis != null)
				xml.AppendChild(Axis.GetXML(xml.OwnerDocument, "Axis", this, processed));
			if (mRefDirection != null)
				xml.AppendChild(RefDirection.GetXML(xml.OwnerDocument, "RefDirection", this, processed));
		}
	}
}
