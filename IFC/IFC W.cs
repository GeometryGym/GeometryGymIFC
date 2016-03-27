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
using System.Drawing;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class IfcWall : IfcBuildingElement
	{
		internal IfcWallTypeEnum mPredefinedType = IfcWallTypeEnum.NOTDEFINED;//: OPTIONAL IfcWallTypeEnum; 
		public IfcWallTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcWall() : base() { }
		internal IfcWall(IfcWall w) : base(w) { mPredefinedType = w.mPredefinedType; }
		public IfcWall(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }

		internal static IfcWall Parse(string strDef, Schema schema) { IfcWall w = new IfcWall(); int ipos = 0; parseFields(w, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return w; }
		internal static void parseFields(IfcWall w, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcBuildingElement.parseFields(w, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
				{
					try
					{
						w.mPredefinedType = (IfcWallTypeEnum)Enum.Parse(typeof(IfcWallTypeEnum), str.Substring(1, str.Length - 2));
					}
					catch (Exception) { }
				}
			}
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcWallTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcWallStandardCase : IfcWall
	{
		internal IfcWallStandardCase() : base() { }
		internal IfcWallStandardCase(IfcWallStandardCase w) : base(w) { }

		internal new static IfcWallStandardCase Parse(string strDef, Schema schema) { IfcWallStandardCase w = new IfcWallStandardCase(); int ipos = 0; parseFields(w, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return w; }
		internal static void parseFields(IfcWallStandardCase w, List<string> arrFields, ref int ipos, Schema schema) { IfcWall.parseFields(w, arrFields, ref ipos,schema); }
	}
	public partial class IfcWallType : IfcBuildingElementType
	{
		internal IfcWallTypeEnum mPredefinedType = IfcWallTypeEnum.NOTDEFINED;
		public IfcWallTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcWallType() : base() { }
		internal IfcWallType(IfcWallType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcWallType(DatabaseIfc m, string name, IfcWallTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcWallType(string name, IfcMaterialLayerSet ls, IfcWallTypeEnum type) : base(ls.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = ls; }
		internal static void parseFields(IfcWallType t, List<string> arrFields, ref int ipos)
		{
			IfcBuildingElementType.parseFields(t, arrFields, ref ipos);
			try
			{
				string str = arrFields[ipos++].Replace(".", "");
				if (string.Compare(str, "STANDARD", true) != 0)
					t.mPredefinedType = (IfcWallTypeEnum)Enum.Parse(typeof(IfcWallTypeEnum), str);
			}
			catch (Exception) { }
		}
		internal new static IfcWallType Parse(string strDef) { IfcWallType t = new IfcWallType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcWarpingStiffnessSelect
	{
		internal bool mFixed;
		internal double mStiffness;
		internal IfcWarpingStiffnessSelect(bool fix) { mFixed = fix; mStiffness = 0; }
		internal IfcWarpingStiffnessSelect(double stiff) { mFixed = false; mStiffness = stiff; }
		internal static IfcWarpingStiffnessSelect Parse(string str) { if (str.StartsWith(".")) return new IfcWarpingStiffnessSelect(ParserSTEP.ParseBool(str)); return new IfcWarpingStiffnessSelect(ParserSTEP.ParseDouble(str)); }
		public override string ToString() { return (mFixed ? ParserSTEP.BoolToString(mFixed) : ParserSTEP.DoubleToString(mStiffness)); }
	}
	public class IfcWasteTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcWasteTerminalTypeEnum mPredefinedType = IfcWasteTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcWasteTerminalTypeEnum;
		public IfcWasteTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcWasteTerminal() : base() { }
		internal IfcWasteTerminal(IfcWasteTerminal t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcWasteTerminal(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcWasteTerminal s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcWasteTerminalTypeEnum)Enum.Parse(typeof(IfcWasteTerminalTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcWasteTerminal Parse(string strDef) { IfcWasteTerminal s = new IfcWasteTerminal(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcWasteTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public class IfcWasteTerminalType : IfcFlowTerminalType
	{
		internal IfcWasteTerminalTypeEnum mPredefinedType = IfcWasteTerminalTypeEnum.NOTDEFINED;// : IfcWasteTerminalTypeEnum; 
		public IfcWasteTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcWasteTerminalType() : base() { }
		internal IfcWasteTerminalType(IfcWasteTerminalType be) : base(be) { mPredefinedType = be.mPredefinedType; }
		internal IfcWasteTerminalType(DatabaseIfc m, string name, IfcWasteTerminalTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcWasteTerminalType t, List<string> arrFields, ref int ipos) { IfcFlowTerminalType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcWasteTerminalTypeEnum)Enum.Parse(typeof(IfcWasteTerminalTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcWasteTerminalType Parse(string strDef) { IfcWasteTerminalType t = new IfcWasteTerminalType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcWaterProperties : IfcMaterialPropertiesSuperSeded // DEPRECEATED IFC4
	{
		internal double mIsPotable;// : OPTIONAL IfcDynamicViscosityMeasure;
		internal double mHardness;// : OPTIONAL IfcModulusOfElasticityMeasure;
		internal double mAlkalinityConcentration;// : OPTIONAL IfcModulusOfElasticityMeasure;
		internal double mPoissonRatio;// : OPTIONAL IfcPositiveRatioMeasure;
		internal double mThermalExpansionCoefficient;// : OPTIONAL IfcThermalExpansionCoefficientMeasure; 
		internal IfcWaterProperties() : base() { }
		internal IfcWaterProperties(IfcWaterProperties be)
			: base(be)
		{
			mIsPotable = be.mIsPotable;
			mHardness = be.mHardness;
			mAlkalinityConcentration = be.mAlkalinityConcentration;
			mPoissonRatio = be.mPoissonRatio;
			mThermalExpansionCoefficient = be.mThermalExpansionCoefficient;
		}
		internal static IfcWaterProperties Parse(string strDef) { IfcWaterProperties p = new IfcWaterProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcWaterProperties p, List<string> arrFields, ref int ipos)
		{
			IfcMaterialPropertiesSuperSeded.parseFields(p, arrFields, ref ipos);
			p.mIsPotable = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mHardness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mAlkalinityConcentration = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mPoissonRatio = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mThermalExpansionCoefficient = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mIsPotable) + "," + ParserSTEP.DoubleOptionalToString(mHardness) + "," + ParserSTEP.DoubleOptionalToString(mAlkalinityConcentration) + "," + ParserSTEP.DoubleOptionalToString(mPoissonRatio) + "," + ParserSTEP.DoubleOptionalToString(mThermalExpansionCoefficient); }
	}
	public partial class IfcWindow : IfcBuildingElement
	{
		internal double mOverallHeight;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mOverallWidth;// : OPTIONAL IfcPositiveLengthMeasure; 
		internal IfcWindowTypeEnum mPredefinedType = IfcWindowTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWindowTypeEnum;
		internal IfcWindowTypePartitioningEnum mPartitioningType = IfcWindowTypePartitioningEnum.NOTDEFINED;//	 :	OPTIONAL IfcWindowTypePartitioningEnum;
		internal string mUserDefinedPartitioningType = "$";//:	OPTIONAL IfcLabel;

		public IfcWindowTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcWindow() : base() { }
		internal IfcWindow(IfcWindow o) : base(o) { mOverallHeight = o.mOverallHeight; mOverallWidth = o.mOverallWidth; mPredefinedType = o.mPredefinedType; mPartitioningType = o.mPartitioningType; mUserDefinedPartitioningType = o.mUserDefinedPartitioningType; }
		public IfcWindow(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }

		internal static IfcWindow Parse(string strDef, Schema schema) { IfcWindow w = new IfcWindow(); int ipos = 0; parseFields(w, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return w; }
		internal static void parseFields(IfcWindow w, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcBuildingElement.parseFields(w, arrFields, ref ipos);
			w.mOverallHeight = ParserSTEP.ParseDouble(arrFields[ipos++]);
			w.mOverallWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema != Schema.IFC2x3)
			{
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					w.mPredefinedType = (IfcWindowTypeEnum)Enum.Parse(typeof(IfcWindowTypeEnum), s.Replace(".", ""));
				s = arrFields[ipos++];
				if (s.StartsWith("."))
					w.mPredefinedType = (IfcWindowTypeEnum)Enum.Parse(typeof(IfcWindowTypeEnum), s.Replace(".", ""));
				w.mUserDefinedPartitioningType = arrFields[ipos++];
			}
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mOverallHeight) + "," + ParserSTEP.DoubleOptionalToString(mOverallWidth) + (mDatabase.mSchema == Schema.IFC2x3 ? "" : ",." + mPredefinedType + ".,." + mPartitioningType + (mUserDefinedPartitioningType == "$" ? ".,$" : ".,'" + mUserDefinedPartitioningType + "'")); }

	}
	public class IfcWindowLiningProperties : IfcPreDefinedPropertySet //IFC2x3 : IfcPropertySetDefinition
	{
		internal double mLiningDepth;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mLiningThickness; //: OPTIONAL  IfcNonNegativeLengthMeasure
		internal double mTransomThickness, mMullionThickness;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mFirstTransomOffset, mSecondTransomOffset, mFirstMullionOffset, mSecondMullionOffset;// : OPTIONAL IfcNormalisedRatioMeasure;
		private int mShapeAspectStyle;// : OPTIONAL IfcShapeAspect; IFC4 Depreceated
		internal double mLiningOffset, mLiningToPanelOffsetX, mLiningToPanelOffsetY;//	 :	OPTIONAL IfcLengthMeasure;
		internal IfcWindowLiningProperties() : base() { }
		internal IfcWindowLiningProperties(IfcWindowLiningProperties p)
			: base(p)
		{
			mLiningDepth = p.mLiningDepth;
			mLiningThickness = p.mLiningThickness;
			mTransomThickness = p.mTransomThickness;
			mMullionThickness = p.mMullionThickness;
			mFirstTransomOffset = p.mFirstTransomOffset;
			mSecondTransomOffset = p.mSecondTransomOffset;
			mFirstMullionOffset = p.mFirstMullionOffset;
			mSecondMullionOffset = p.mSecondMullionOffset;
			mShapeAspectStyle = p.mShapeAspectStyle;
			mLiningOffset = p.mLiningOffset;
			mLiningToPanelOffsetX = p.mLiningToPanelOffsetX;
			mLiningToPanelOffsetY = p.mLiningToPanelOffsetY;
		}
		internal IfcWindowLiningProperties(DatabaseIfc m, string name, double lngDpth, double lngThck, double trnsmThck, double mllnThck,
			double trnsmOffst1, double trnsmOffst2, double mllnOffst1, double mllnOffst2, double lngOffset, double lngToPnlOffstX, double lngToPnlOffstY)
			: base(m, name)
		{
			mLiningDepth = lngDpth;
			mLiningThickness = lngThck;
			mTransomThickness = trnsmThck;
			mMullionThickness = mllnThck;
			mFirstTransomOffset = Math.Min(1, Math.Max(0, trnsmOffst1));
			mSecondTransomOffset = Math.Min(1, Math.Max(0, trnsmOffst2));
			mFirstMullionOffset = Math.Min(1, Math.Max(0, mllnOffst1));
			mSecondMullionOffset = Math.Min(1, Math.Max(0, mllnOffst2));
			mLiningOffset = lngOffset;
			mLiningToPanelOffsetX = lngToPnlOffstX;
			mLiningToPanelOffsetY = lngToPnlOffstY;
		}
		internal static IfcWindowLiningProperties Parse(string strDef, Schema schema) { IfcWindowLiningProperties p = new IfcWindowLiningProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return p; }
		internal static void parseFields(IfcWindowLiningProperties p, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcPropertySetDefinition.parseFields(p, arrFields, ref ipos);
			p.mLiningDepth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mLiningThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mTransomThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mMullionThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFirstTransomOffset = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mSecondTransomOffset = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFirstMullionOffset = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mSecondMullionOffset = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mShapeAspectStyle = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (schema != Schema.IFC2x3)
			{
				p.mLiningOffset = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mLiningToPanelOffsetX = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mLiningToPanelOffsetY = ParserSTEP.ParseDouble(arrFields[ipos++]);
			}
		}
		protected override string BuildString()
		{
			return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mLiningDepth) + "," + ParserSTEP.DoubleOptionalToString(mLiningThickness) + "," + ParserSTEP.DoubleOptionalToString(mTransomThickness) + "," + ParserSTEP.DoubleOptionalToString(mMullionThickness)
				+ "," + ParserSTEP.DoubleOptionalToString(mFirstTransomOffset) + "," + ParserSTEP.DoubleOptionalToString(mSecondTransomOffset) + "," + ParserSTEP.DoubleOptionalToString(mFirstMullionOffset) + "," + ParserSTEP.DoubleOptionalToString(mSecondMullionOffset) + "," +
				ParserSTEP.LinkToString(mShapeAspectStyle) + (mDatabase.mSchema == Schema.IFC2x3 ? "" : "," + ParserSTEP.DoubleOptionalToString(mLiningOffset) + "," + ParserSTEP.DoubleOptionalToString(mLiningToPanelOffsetX) + "," + ParserSTEP.DoubleOptionalToString(mLiningToPanelOffsetY));
		}
	}
	public class IfcWindowPanelProperties : IfcPreDefinedPropertySet //IFC2x3: IfcPropertySetDefinition
	{
		internal IfcWindowPanelOperationEnum mOperationType;// : IfcWindowPanelOperationEnum;
		internal IfcWindowPanelPositionEnum mPanelPosition;// :IfcWindowPanelPositionEnume;
		internal double mFrameDepth;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mFrameThickness;// : OPTIONAL IfcPositiveLengthMeasure;
		private int mShapeAspectStyle;// : OPTIONAL IfcShapeAspect; IFC4 Depreceated
		internal IfcWindowPanelProperties() : base() { }
		internal IfcWindowPanelProperties(IfcWindowPanelProperties p)
			: base(p)
		{
			mOperationType = p.mOperationType;
			mPanelPosition = p.mPanelPosition;
			mFrameDepth = p.mFrameDepth;
			mFrameThickness = p.mFrameThickness;
			mShapeAspectStyle = p.mShapeAspectStyle;
		}
		internal IfcWindowPanelProperties(DatabaseIfc m, string name, IfcWindowPanelOperationEnum op, IfcWindowPanelPositionEnum panel, double frameDepth, double frameThick)
			: base(m, name)
		{
			mOperationType = op;
			mPanelPosition = panel;
			mFrameDepth = frameDepth;
			mFrameThickness = frameThick;
		}
		internal static IfcWindowPanelProperties Parse(string strDef) { IfcWindowPanelProperties p = new IfcWindowPanelProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcWindowPanelProperties p, List<string> arrFields, ref int ipos)
		{
			IfcPropertySetDefinition.parseFields(p, arrFields, ref ipos);
			p.mOperationType = (IfcWindowPanelOperationEnum)Enum.Parse(typeof(IfcWindowPanelOperationEnum), arrFields[ipos++].Replace(".", ""));
			p.mPanelPosition = (IfcWindowPanelPositionEnum)Enum.Parse(typeof(IfcWindowPanelPositionEnum), arrFields[ipos++].Replace(".", ""));
			p.mFrameDepth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFrameThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mShapeAspectStyle = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + ",." + mOperationType.ToString() + ".,." + mPanelPosition.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mFrameDepth) + "," + ParserSTEP.DoubleOptionalToString(mFrameThickness) + "," + ParserSTEP.LinkToString(mShapeAspectStyle); }
	}
	public partial class IfcWindowStandardCase : IfcWindow
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 || mDatabase.mModelView == ModelView.Ifc4Reference ? "IFCWINDOW" : base.KeyWord); } }
		internal IfcWindowStandardCase() : base() { }
		internal IfcWindowStandardCase(IfcWindowStandardCase w) : base(w) { }
		internal new static IfcWindowStandardCase Parse(string strDef, Schema schema) { IfcWindowStandardCase s = new IfcWindowStandardCase(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return s; }
		internal static void parseFields(IfcWindowStandardCase s, List<string> arrFields, ref int ipos, Schema schema) { IfcWindow.parseFields(s, arrFields, ref ipos,schema); }
	}
	public partial class IfcWindowStyle : IfcTypeProduct // IFC2x3
	{
		internal IfcWindowStyleConstructionEnum mConstructionType;// : IfcWindowStyleConstructionEnum;
		internal IfcWindowStyleOperationEnum mOperationType;// : IfcWindowStyleOperationEnum;
		internal bool mParameterTakesPrecedence;// : BOOLEAN;
		internal bool mSizeable;// : BOOLEAN;
		internal IfcWindowStyle() : base() { }
		internal IfcWindowStyle(IfcWindowStyle el) : base(el) { mConstructionType = el.mConstructionType; mOperationType = el.mOperationType; mParameterTakesPrecedence = el.mParameterTakesPrecedence; mSizeable = el.mSizeable; }
		internal new static IfcWindowStyle Parse(string strDef) { IfcWindowStyle s = new IfcWindowStyle(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcWindowStyle s, List<string> arrFields, ref int ipos)
		{
			IfcTypeProduct.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str != "$")
				s.mConstructionType = (IfcWindowStyleConstructionEnum)Enum.Parse(typeof(IfcWindowStyleConstructionEnum), str.Replace(".", ""));
			str = arrFields[ipos++];
			if (str != "$")
				s.mOperationType = (IfcWindowStyleOperationEnum)Enum.Parse(typeof(IfcWindowStyleOperationEnum), str.Replace(".", ""));
			s.mParameterTakesPrecedence = ParserSTEP.ParseBool(arrFields[ipos++]);
			s.mSizeable = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + ",." + mConstructionType.ToString() + ".,." + mOperationType.ToString() + ".," + ParserSTEP.BoolToString(mParameterTakesPrecedence) + "," + ParserSTEP.BoolToString(mSizeable); }
	}
	public partial class IfcWindowType : IfcBuildingElementType //IFCWindowStyle IFC2x3
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 ? "IFCWINDOWSTYLE" : base.KeyWord); } }
		internal IfcWindowTypeEnum mPredefinedType = IfcWindowTypeEnum.NOTDEFINED;
		internal IfcWindowTypePartitioningEnum mPartitioningType = IfcWindowTypePartitioningEnum.NOTDEFINED;// : IfcWindowTypePartitioningEnum; 
		internal bool mParameterTakesPrecedence;// : BOOLEAN; 
		internal string mUserDefinedPartitioningType = "$"; // 	 :	OPTIONAL IfcLabel;
		public IfcWindowTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcWindowType() : base() { }
		internal IfcWindowType(IfcWindowType t) : base(t) { mPredefinedType = t.mPredefinedType; mPartitioningType = t.mPartitioningType; mParameterTakesPrecedence = t.mParameterTakesPrecedence; mUserDefinedPartitioningType = t.mUserDefinedPartitioningType; }
		public IfcWindowType(DatabaseIfc m, string name, IfcWindowTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal IfcWindowType(DatabaseIfc m, string name, IfcWindowTypeEnum type, IfcWindowTypePartitioningEnum partition, bool parameterTakesPrecendence)
			: base(m) { Name = name; mPredefinedType = type; mPartitioningType = partition; mParameterTakesPrecedence = parameterTakesPrecendence; }
		internal IfcWindowType(DatabaseIfc m, string name, IfcWindowTypeEnum type, IfcWindowTypePartitioningEnum partition, string userDefinedPartionType, IfcWindowLiningProperties wlp, List<IfcWindowPanelProperties> pps)
			: base(m)
		{
			Name = name;
			mPredefinedType = type;
			mPartitioningType = partition;
			mParameterTakesPrecedence = true;
			if (wlp != null)
				mHasPropertySets.Add(wlp.mIndex);
			if (pps != null && pps.Count > 0)
				mHasPropertySets.AddRange(pps.ConvertAll(x => x.mIndex));
			if (!string.IsNullOrEmpty(userDefinedPartionType))
				mUserDefinedPartitioningType = userDefinedPartionType.Replace("'", "");
		}

		internal new static IfcWindowType Parse(string strDef) { IfcWindowType s = new IfcWindowType(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcWindowType s, List<string> arrFields, ref int ipos)
		{
			IfcBuildingElementType.parseFields(s, arrFields, ref ipos);
			s.mPredefinedType = (IfcWindowTypeEnum)Enum.Parse(typeof(IfcWindowTypeEnum), arrFields[ipos++].Replace(".", ""));
			s.mPartitioningType = (IfcWindowTypePartitioningEnum)Enum.Parse(typeof(IfcWindowTypePartitioningEnum), arrFields[ipos++].Replace(".", ""));
			s.mParameterTakesPrecedence = ParserSTEP.ParseBool(arrFields[ipos++]);
			s.mUserDefinedPartitioningType = arrFields[ipos++];
		}
		protected override string BuildString()
		{
			return (mDatabase.mSchema == Schema.IFC2x3 ? base.BuildString() + ",.NOTDEFINED.,.NOTDEFINED.," + ParserSTEP.BoolToString(mParameterTakesPrecedence) + "," + ParserSTEP.BoolToString(false) :
				base.BuildString() + ",." + mPredefinedType.ToString() + ".,." + mPartitioningType.ToString() + ".," + ParserSTEP.BoolToString(mParameterTakesPrecedence) + (mUserDefinedPartitioningType == "$" ? ",$" : ",'" + mUserDefinedPartitioningType + "'"));
		}
	}
	public class IfcWorkCalendar : IfcControl //IFC4
	{
		internal List<int> mWorkingTimes = new List<int>();// :	OPTIONAL SET [1:?] OF IfcWorkTime;
		internal List<int> mExceptionTimes = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcWorkTime;
		internal IfcWorkCalendarTypeEnum mPredefinedType = IfcWorkCalendarTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkCalendarTypeEnum 
		internal IfcWorkCalendar() : base() { }
		internal IfcWorkCalendar(IfcWorkCalendar i) : base(i) { mWorkingTimes.AddRange(i.mWorkingTimes); mExceptionTimes.AddRange(i.mExceptionTimes); mPredefinedType = i.mPredefinedType; }
		internal IfcWorkCalendar(DatabaseIfc m, List<IfcWorkTime> working, List<IfcWorkTime> exception, IfcWorkCalendarTypeEnum type, IfcProject prj)
			: base(m)
		{
			if (working != null)
				mWorkingTimes = working.ConvertAll(x => x.mIndex);
			if (exception != null)
				mExceptionTimes = exception.ConvertAll(x => x.mIndex);
			mPredefinedType = type;
			if (prj != null)
				prj.AddDeclared(this);
		}
		internal static IfcWorkCalendar Parse(string strDef) { IfcWorkCalendar p = new IfcWorkCalendar(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcWorkCalendar c, List<string> arrFields, ref int ipos)
		{
			IfcControl.parseFields(c, arrFields, ref ipos);
			c.mWorkingTimes = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			c.mExceptionTimes = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				c.mPredefinedType = (IfcWorkCalendarTypeEnum)Enum.Parse(typeof(IfcWorkCalendarTypeEnum), s.Replace(".", ""));
		}
		protected override string BuildString()
		{
			string str = "";
			if (mWorkingTimes.Count > 0)
			{
				str += ",(" + ParserSTEP.LinkToString(mWorkingTimes[0]);
				for (int icounter = 1; icounter < mWorkingTimes.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mWorkingTimes[icounter]);
				str += "),";
			}
			else
				str += ",$,";
			if (mExceptionTimes.Count > 0)
			{
				str += "(" + ParserSTEP.LinkToString(mExceptionTimes[0]);
				for (int icounter = 1; icounter < mExceptionTimes.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mExceptionTimes[icounter]);
				str += "),.";
			}
			else
				str += "$,.";
			return base.BuildString() + str + mPredefinedType.ToString() + ".";
		}
	}
	public abstract partial class IfcWorkControl : IfcControl //ABSTRACT SUPERTYPE OF(ONEOF(IfcWorkPlan, IfcWorkSchedule))
	{
		//internal string mIdentifier	 : 	IfcIdentifier; IFC4 moved to control
		internal string mCreationDate;// : IfcDateTime;
		internal List<int> mCreators = new List<int>();// : OPTIONAL SET [1:?] OF IfcPerson;
		internal string mPurpose = "$";// : OPTIONAL IfcLabel;
		internal string mDuration = "$", mTotalFloat = "$";// : OPTIONAL IfcDuration; IFC4
		internal string mStartTime;// : IfcDateTime;
		internal string mFinishTime = "$";// : OPTIONAL IfcDateTime;  IFC4
		internal double mSSDuration = 0, mSSTotalFloat = 0; //: 	OPTIONAL IfcTimeMeasure; 
		internal int mSSCreationDate, mSSStartTime; //: 	IfcDateTimeSelect;
		internal int mSSFinishTime; //: OPTIONAL IfcDateTimeSelect;
		internal IfcWorkControlTypeEnum mWorkControlType = IfcWorkControlTypeEnum.NOTDEFINED;//	 : 	OPTIONAL IfcWorkControlTypeEnum; IFC2x3
		internal string mUserDefinedControlType = "$";//	 : 	OPTIONAL IfcLabel;

		public string Purpose { get { return (mPurpose == "$" ? "" : ParserIfc.Decode(mPurpose)); } set { mPurpose = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }


		protected IfcWorkControl() : base() { }
		protected IfcWorkControl(IfcWorkControl i)
			: base(i)
		{
			mCreationDate = i.mCreationDate;
			mCreators.AddRange(i.mCreators);
			mDuration = i.mDuration;
			mTotalFloat = i.mTotalFloat;
			mStartTime = i.mStartTime;
			mFinishTime = i.mFinishTime;
		}
		
		protected static void parseFields(IfcWorkControl c, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcControl.parseFields(c, arrFields, ref ipos,schema);
			if (schema == Schema.IFC2x3)
			{
				c.mIdentification = arrFields[ipos++].Replace("'", "");
				c.mSSCreationDate = ParserSTEP.ParseLink(arrFields[ipos++]);
				c.mCreators = ParserSTEP.SplitListLinks(arrFields[ipos++]);
				c.mPurpose = arrFields[ipos++];
				c.mSSDuration = ParserSTEP.ParseDouble(arrFields[ipos++]);
				c.mSSTotalFloat = ParserSTEP.ParseDouble(arrFields[ipos++]);
				c.mSSStartTime = ParserSTEP.ParseLink(arrFields[ipos++]);
				c.mSSFinishTime = ParserSTEP.ParseLink(arrFields[ipos++]);
				string s = arrFields[ipos++];
				if (s[0] == '.')
					c.mWorkControlType = (IfcWorkControlTypeEnum)Enum.Parse(typeof(IfcWorkControlTypeEnum), s.Replace(".", ""));
				c.mUserDefinedControlType = arrFields[ipos++];
			}
			else
			{
				c.mCreationDate = arrFields[ipos++].Replace("'", "");
				c.mCreators = ParserSTEP.SplitListLinks(arrFields[ipos++]);
				c.mPurpose = arrFields[ipos++];
				c.mDuration = arrFields[ipos++];
				c.mTotalFloat = arrFields[ipos++];
				c.mStartTime = arrFields[ipos++].Replace("'", "");
				c.mFinishTime = arrFields[ipos++].Replace("'", "");
			}
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + (mDatabase.mSchema == Schema.IFC2x3 ? "'" + mIdentification + "'," + ParserSTEP.LinkToString(mSSCreationDate) : (mCreationDate == "$" ? "$" : "'" + mCreationDate + "'"));
			if (mCreators.Count > 0)
			{
				str += ",(" + ParserSTEP.LinkToString(mCreators[0]);
				for (int icounter = 1; icounter < mCreators.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mCreators[icounter]);
				str += "),";
			}
			else
				str += ",$,";
			if (mDatabase.mSchema == Schema.IFC2x3)
				return str + (mPurpose == "$" ? "$," : "'" + mPurpose + "',") + ParserSTEP.DoubleOptionalToString(mSSDuration) + "," + ParserSTEP.DoubleOptionalToString(mSSTotalFloat) + "," +
					ParserSTEP.LinkToString(mSSStartTime) + "," + ParserSTEP.LinkToString(mSSFinishTime) + ",." + mWorkControlType.ToString() + (mUserDefinedControlType == "$" ? ".,$" : ".,'" + mUserDefinedControlType + "'");
			return str + (mPurpose == "$" ? "$," : "'" + mPurpose + "',") + mDuration + "," + mTotalFloat + (mStartTime == "$" ? ",$," : ",'" + mStartTime + "',") + (mFinishTime == "$" ? "$" : "'" + mFinishTime + "'");
		}
		internal DateTime getStart() { return (mDatabase.mSchema == Schema.IFC2x3 ? (mDatabase.mIfcObjects[mSSStartTime] as IfcDateTimeSelect).DateTime : DateTime.MinValue); }
		
	}
	public partial class IfcWorkPlan : IfcWorkControl
	{
		internal IfcWorkPlanTypeEnum mPredefinedType = IfcWorkPlanTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkPlanTypeEnum; IFC4
		internal IfcWorkPlan() : base() { }
		internal IfcWorkPlan(IfcWorkPlan p) : base(p) { mPredefinedType = p.mPredefinedType; }
		internal static IfcWorkPlan Parse(string strDef) { IfcWorkPlan p = new IfcWorkPlan(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcWorkPlan p, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcWorkControl.parseFields(p, arrFields, ref ipos,schema);
			if (schema != Schema.IFC2x3)
			{
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					p.mPredefinedType = (IfcWorkPlanTypeEnum)Enum.Parse(typeof(IfcWorkPlanTypeEnum), s.Replace(".", ""));
			}
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcWorkSchedule : IfcWorkControl
	{
		internal IfcWorkScheduleTypeEnum mPredefinedType = IfcWorkScheduleTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkScheduleTypeEnum; IFC4
		internal IfcWorkSchedule() : base() { }
		internal IfcWorkSchedule(IfcWorkSchedule p) : base(p) { mPredefinedType = p.mPredefinedType; }
		internal static IfcWorkSchedule Parse(string strDef, Schema schema) { IfcWorkSchedule s = new IfcWorkSchedule(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return s; }
		internal static void parseFields(IfcWorkSchedule s, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcWorkControl.parseFields(s, arrFields, ref ipos,schema);
			if (schema != Schema.IFC2x3)
			{
				string st = arrFields[ipos++];
				if (st.StartsWith("."))
					s.mPredefinedType = (IfcWorkScheduleTypeEnum)Enum.Parse(typeof(IfcWorkScheduleTypeEnum), st.Replace(".", ""));
			}
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : ",." + mPredefinedType.ToString() + "."); }
	}
	public class IfcWorkTime : IfcSchedulingTime //IFC4
	{
		internal int mRecurrencePattern;// OPTIONAL	IfcRecurrencePattern
		internal string mStart = "$";//	 :	OPTIONAL IfcDate;
		internal string mFinish = "$";//	 :	OPTIONAL IfcDate; 
		internal IfcWorkTime() : base() { }
		internal IfcWorkTime(IfcWorkTime i) : base(i) { mRecurrencePattern = i.mRecurrencePattern; mStart = i.mStart; mFinish = i.mFinish; }
		internal IfcWorkTime(DatabaseIfc m, string name, IfcDataOriginEnum origin, string userOrigin, IfcRecurrencePattern recur, DateTime start, DateTime finish)
			: base(m, name, origin, userOrigin) { if (recur != null) mRecurrencePattern = recur.mIndex; if (start != DateTime.MinValue) mStart = IfcDate.convert(start); if (finish != DateTime.MinValue) mFinish = IfcDate.convert(finish); }
		internal static IfcWorkTime Parse(string strDef) { IfcWorkTime f = new IfcWorkTime(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
		internal static void parseFields(IfcWorkTime f, List<string> arrFields, ref int ipos)
		{
			IfcSchedulingTime.parseFields(f, arrFields, ref ipos);
			f.mRecurrencePattern = ParserSTEP.ParseLink(arrFields[ipos++]);
			f.mStart = arrFields[ipos++].Replace("'", "");
			f.mFinish = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mRecurrencePattern) + (mStart == "$" ? ",$," : ",'" + mStart + "',") + (mFinish == "$" ? "$" : "'" + mFinish + "'")); }
	}
}
