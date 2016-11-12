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
		internal IfcWall(DatabaseIfc db, IfcWall w) : base(db, w) { mPredefinedType = w.mPredefinedType; }
		public IfcWall(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcWall Parse(string str, ReleaseVersion schema) { IfcWall w = new IfcWall(); int pos = 0; w.Parse(str, ref pos, schema); return w; }
		protected void Parse(string str, ref int pos, ReleaseVersion schema)
		{
			base.Parse(str, ref pos);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos);
				if (s[0] == '.')
					Enum.TryParse< IfcWallTypeEnum >(s.Substring(1, s.Length - 2), out mPredefinedType);
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcWallTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcWallElementedCase : IfcWall
	{
		internal IfcWallElementedCase() : base() { }
		internal IfcWallElementedCase(DatabaseIfc db, IfcWallElementedCase w) : base(db, w) { }
		

		internal new static IfcWallElementedCase Parse(string str, ReleaseVersion schema) { IfcWallElementedCase w = new IfcWallElementedCase(); int pos = 0; w.Parse(str, ref pos, schema); return w; }
	}
	public partial class IfcWallStandardCase : IfcWall
	{
		internal IfcWallStandardCase() : base() { }
		internal IfcWallStandardCase(DatabaseIfc db, IfcWallStandardCase w) : base(db, w) { }
		public IfcWallStandardCase(IfcProduct container, IfcMaterialLayerSetUsage layerSetUsage, IfcAxis2Placement3D placement, double length, double height)
			:base(container,new IfcLocalPlacement(container.Placement, placement),null)
		{
			DatabaseIfc db = mDatabase;
			double tol = mDatabase.Tolerance;
			setMaterial(layerSetUsage);

			IfcShapeRepresentation asr = IfcShapeRepresentation.GetAxisRep(new IfcPolyline(new IfcCartesianPoint(db,0,0,0),new IfcCartesianPoint(db,length,0,0)));
			List<IfcShapeModel> reps = new List<IfcShapeModel>();
			reps.Add(asr);
			double t = layerSetUsage.ForLayerSet.MaterialLayers.ConvertAll(x=>x.LayerThickness).Sum();

			reps.Add(new IfcShapeRepresentation( new IfcExtrudedAreaSolid(new IfcRectangleProfileDef(db,"",length, t), new IfcAxis2Placement3D(new IfcCartesianPoint(db,length/2.0, layerSetUsage.OffsetFromReferenceLine + (layerSetUsage.DirectionSense == IfcDirectionSenseEnum.POSITIVE ? 1 : -1) * t/2.0, 0)), height)));
			Representation = new IfcProductDefinitionShape(reps);
		}

		internal new static IfcWallStandardCase Parse(string str, ReleaseVersion schema) { IfcWallStandardCase w = new IfcWallStandardCase(); int pos = 0; w.Parse(str, ref pos, schema); return w; }
	}
	public partial class IfcWallType : IfcBuildingElementType
	{
		internal IfcWallTypeEnum mPredefinedType = IfcWallTypeEnum.NOTDEFINED;
		public IfcWallTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcWallType() : base() { }
		internal IfcWallType(DatabaseIfc db, IfcWallType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcWarpingStiffnessSelect
	{
		internal bool mFixed;
		internal double mStiffness;
		internal IfcWarpingStiffnessSelect(bool fix) { mFixed = fix; mStiffness = 0; }
		internal IfcWarpingStiffnessSelect(double stiff) { mFixed = false; mStiffness = stiff; }
		internal static IfcWarpingStiffnessSelect Parse(string str) { if (str.StartsWith(".")) return new IfcWarpingStiffnessSelect(ParserSTEP.ParseBool(str)); return new IfcWarpingStiffnessSelect(ParserSTEP.ParseDouble(str)); }
		public override string ToString() { return (mFixed ? ParserSTEP.BoolToString(mFixed) : ParserSTEP.DoubleToString(mStiffness)); }
	}
	public partial class IfcWasteTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcWasteTerminalTypeEnum mPredefinedType = IfcWasteTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcWasteTerminalTypeEnum;
		public IfcWasteTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcWasteTerminal() : base() { }
		internal IfcWasteTerminal(DatabaseIfc db, IfcWasteTerminal t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcWasteTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcWasteTerminal s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcWasteTerminalTypeEnum)Enum.Parse(typeof(IfcWasteTerminalTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcWasteTerminal Parse(string strDef) { IfcWasteTerminal s = new IfcWasteTerminal(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcWasteTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcWasteTerminalType : IfcFlowTerminalType
	{
		internal IfcWasteTerminalTypeEnum mPredefinedType = IfcWasteTerminalTypeEnum.NOTDEFINED;// : IfcWasteTerminalTypeEnum; 
		public IfcWasteTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcWasteTerminalType() : base() { }
		internal IfcWasteTerminalType(DatabaseIfc db, IfcWasteTerminalType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcWasteTerminalType(DatabaseIfc m, string name, IfcWasteTerminalTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcWasteTerminalType t, List<string> arrFields, ref int ipos) { IfcFlowTerminalType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcWasteTerminalTypeEnum)Enum.Parse(typeof(IfcWasteTerminalTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcWasteTerminalType Parse(string strDef) { IfcWasteTerminalType t = new IfcWasteTerminalType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcWaterProperties : IfcMaterialPropertiesSuperSeded // DEPRECEATED IFC4
	{
		internal bool mIsPotable = false;// : 	OPTIONAL BOOLEAN;
		internal double mHardness = double.NaN, mAlkalinityConcentration = double.NaN, mAcidityConcentration = double.NaN;// : : 	OPTIONAL IfcIonConcentrationMeasure
		internal double mImpuritiesContent = double.NaN;//: 	OPTIONAL IfcNormalisedRatioMeasure
		internal double mPHLevel = double.NaN;  //: 	OPTIONAL IfcPHMeasure;
		internal double mDissolvedSolidsContent = double.NaN;//: 	OPTIONAL IfcNormalisedRatioMeasure
		internal IfcWaterProperties() : base() { }
		internal IfcWaterProperties(DatabaseIfc db, IfcWaterProperties p) : base(db,p)
		{
			mIsPotable = p.mIsPotable;
			mHardness = p.mHardness;
			mAlkalinityConcentration = p.mAlkalinityConcentration;
			mAcidityConcentration = p.mAcidityConcentration;
			mImpuritiesContent = p.mImpuritiesContent;
			mPHLevel = p.mPHLevel;
			mDissolvedSolidsContent = p.mDissolvedSolidsContent;
		}
		internal static IfcWaterProperties Parse(string strDef) { IfcWaterProperties p = new IfcWaterProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcWaterProperties p, List<string> arrFields, ref int ipos)
		{
			IfcMaterialPropertiesSuperSeded.parseFields(p, arrFields, ref ipos);
			p.mIsPotable = ParserSTEP.ParseBool(arrFields[ipos++]);
			p.mHardness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mAlkalinityConcentration = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mAcidityConcentration = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mImpuritiesContent = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mPHLevel = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mDissolvedSolidsContent = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.BoolToString(mIsPotable) + "," + ParserSTEP.DoubleOptionalToString(mHardness) + "," + ParserSTEP.DoubleOptionalToString(mAlkalinityConcentration) + "," + ParserSTEP.DoubleOptionalToString(mAcidityConcentration) + "," + ParserSTEP.DoubleOptionalToString(mImpuritiesContent) + "," + ParserSTEP.DoubleOptionalToString(mPHLevel) + "," + ParserSTEP.DoubleOptionalToString(mDissolvedSolidsContent); }
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
		internal IfcWindow(DatabaseIfc db, IfcWindow w) : base(db, w) { mOverallHeight = w.mOverallHeight; mOverallWidth = w.mOverallWidth; mPredefinedType = w.mPredefinedType; mPartitioningType = w.mPartitioningType; mUserDefinedPartitioningType = w.mUserDefinedPartitioningType; }
		public IfcWindow(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcWindow Parse(string str, ReleaseVersion schema) { IfcWindow w = new IfcWindow(); int pos = 0; w.Parse(str, ref pos, schema); return w; }
		protected void Parse(string str, ref int pos, ReleaseVersion schema)
		{
			base.Parse(str, ref pos);
			mOverallHeight = ParserSTEP.StripDouble(str, ref pos);
			mOverallWidth = ParserSTEP.StripDouble(str,ref pos);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos);
				if (s.StartsWith("."))
					mPredefinedType = (IfcWindowTypeEnum)Enum.Parse(typeof(IfcWindowTypeEnum), s.Replace(".", ""));
				s = ParserSTEP.StripField(str, ref pos);
				if (s.StartsWith("."))
					mPredefinedType = (IfcWindowTypeEnum)Enum.Parse(typeof(IfcWindowTypeEnum), s.Replace(".", ""));
				mUserDefinedPartitioningType = ParserSTEP.StripString(str, ref pos);
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mOverallHeight) + "," + ParserSTEP.DoubleOptionalToString(mOverallWidth) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : ",." + mPredefinedType + ".,." + mPartitioningType + (mUserDefinedPartitioningType == "$" ? ".,$" : ".,'" + mUserDefinedPartitioningType + "'")); }

	}
	public partial class IfcWindowLiningProperties : IfcPreDefinedPropertySet //IFC2x3 : IfcPropertySetDefinition
	{
		internal double mLiningDepth;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mLiningThickness; //: OPTIONAL  IfcNonNegativeLengthMeasure
		internal double mTransomThickness, mMullionThickness;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mFirstTransomOffset, mSecondTransomOffset, mFirstMullionOffset, mSecondMullionOffset;// : OPTIONAL IfcNormalisedRatioMeasure;
		private int mShapeAspectStyle;// : OPTIONAL IfcShapeAspect; IFC4 Depreceated
		internal double mLiningOffset, mLiningToPanelOffsetX, mLiningToPanelOffsetY;//	 :	OPTIONAL IfcLengthMeasure;

		public IfcShapeAspect ShapeAspectStyle { get { return mDatabase[mShapeAspectStyle] as IfcShapeAspect; } set { mShapeAspectStyle = (value == null ? 0 : value.mIndex); } }
		internal IfcWindowLiningProperties() : base() { }
		internal IfcWindowLiningProperties(DatabaseIfc db, IfcWindowLiningProperties p) : base(db, p)
		{
			mLiningDepth = p.mLiningDepth;
			mLiningThickness = p.mLiningThickness;
			mTransomThickness = p.mTransomThickness;
			mMullionThickness = p.mMullionThickness;
			mFirstTransomOffset = p.mFirstTransomOffset;
			mSecondTransomOffset = p.mSecondTransomOffset;
			mFirstMullionOffset = p.mFirstMullionOffset;
			mSecondMullionOffset = p.mSecondMullionOffset;
			if (p.mShapeAspectStyle > 0)
				ShapeAspectStyle = db.Factory.Duplicate(p.ShapeAspectStyle) as IfcShapeAspect;
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
		internal static IfcWindowLiningProperties Parse(string strDef, ReleaseVersion schema) { IfcWindowLiningProperties p = new IfcWindowLiningProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return p; }
		internal static void parseFields(IfcWindowLiningProperties p, List<string> arrFields, ref int ipos, ReleaseVersion schema)
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
			if (schema != ReleaseVersion.IFC2x3)
			{
				p.mLiningOffset = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mLiningToPanelOffsetX = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mLiningToPanelOffsetY = ParserSTEP.ParseDouble(arrFields[ipos++]);
			}
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mLiningDepth) + "," + ParserSTEP.DoubleOptionalToString(mLiningThickness) + "," + ParserSTEP.DoubleOptionalToString(mTransomThickness) + "," + ParserSTEP.DoubleOptionalToString(mMullionThickness)
				+ "," + ParserSTEP.DoubleOptionalToString(mFirstTransomOffset) + "," + ParserSTEP.DoubleOptionalToString(mSecondTransomOffset) + "," + ParserSTEP.DoubleOptionalToString(mFirstMullionOffset) + "," + ParserSTEP.DoubleOptionalToString(mSecondMullionOffset) + "," +
				ParserSTEP.LinkToString(mShapeAspectStyle) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.DoubleOptionalToString(mLiningOffset) + "," + ParserSTEP.DoubleOptionalToString(mLiningToPanelOffsetX) + "," + ParserSTEP.DoubleOptionalToString(mLiningToPanelOffsetY));
		}
	}
	public partial class IfcWindowPanelProperties : IfcPreDefinedPropertySet //IFC2x3: IfcPropertySetDefinition
	{
		internal IfcWindowPanelOperationEnum mOperationType;// : IfcWindowPanelOperationEnum;
		internal IfcWindowPanelPositionEnum mPanelPosition;// :IfcWindowPanelPositionEnume;
		internal double mFrameDepth;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mFrameThickness;// : OPTIONAL IfcPositiveLengthMeasure;
		private int mShapeAspectStyle;// : OPTIONAL IfcShapeAspect; IFC4 Depreceated
		
		public IfcShapeAspect ShapeAspectStyle { get { return mDatabase[mShapeAspectStyle] as IfcShapeAspect; } set { mShapeAspectStyle = (value == null ? 0 : value.mIndex); } }

		internal IfcWindowPanelProperties() : base() { }
		internal IfcWindowPanelProperties(DatabaseIfc db, IfcWindowPanelProperties p) : base(db, p)
		{
			mOperationType = p.mOperationType;
			mPanelPosition = p.mPanelPosition;
			mFrameDepth = p.mFrameDepth;
			mFrameThickness = p.mFrameThickness;
			if (p.mShapeAspectStyle > 0)
				ShapeAspectStyle = db.Factory.Duplicate(p.ShapeAspectStyle) as IfcShapeAspect;
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mOperationType.ToString() + ".,." + mPanelPosition.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mFrameDepth) + "," + ParserSTEP.DoubleOptionalToString(mFrameThickness) + "," + ParserSTEP.LinkToString(mShapeAspectStyle); }
	}
	public partial class IfcWindowStandardCase : IfcWindow
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mDatabase.mModelView == ModelView.Ifc4Reference ? "IfcWindow" : base.KeyWord); } }
		internal IfcWindowStandardCase() : base() { }
		internal IfcWindowStandardCase(DatabaseIfc db, IfcWindowStandardCase w) : base(db,w) { }
		internal new static IfcWindowStandardCase Parse(string str, ReleaseVersion schema) { IfcWindowStandardCase w = new IfcWindowStandardCase(); int pos = 0; w.Parse(str, ref pos, schema); return w; }
	}
	public partial class IfcWindowStyle : IfcTypeProduct // IFC2x3
	{
		internal IfcWindowStyleConstructionEnum mConstructionType;// : IfcWindowStyleConstructionEnum;
		internal IfcWindowStyleOperationEnum mOperationType;// : IfcWindowStyleOperationEnum;
		internal bool mParameterTakesPrecedence;// : BOOLEAN;
		internal bool mSizeable;// : BOOLEAN;
		internal IfcWindowStyle() : base() { }
		internal IfcWindowStyle(DatabaseIfc db, IfcWindowStyle s) : base(db,s) { mConstructionType = s.mConstructionType; mOperationType = s.mOperationType; mParameterTakesPrecedence = s.mParameterTakesPrecedence; mSizeable = s.mSizeable; }
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mConstructionType.ToString() + ".,." + mOperationType.ToString() + ".," + ParserSTEP.BoolToString(mParameterTakesPrecedence) + "," + ParserSTEP.BoolToString(mSizeable); }
	}
	public partial class IfcWindowType : IfcBuildingElementType //IFCWindowStyle IFC2x3
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcWindowStyle" : base.KeyWord); } }
		internal IfcWindowTypeEnum mPredefinedType = IfcWindowTypeEnum.NOTDEFINED;
		internal IfcWindowTypePartitioningEnum mPartitioningType = IfcWindowTypePartitioningEnum.NOTDEFINED;// : IfcWindowTypePartitioningEnum; 
		internal bool mParameterTakesPrecedence;// : BOOLEAN; 
		internal string mUserDefinedPartitioningType = "$"; // 	 :	OPTIONAL IfcLabel;

		public IfcWindowTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcWindowType() : base() { }
		internal IfcWindowType(DatabaseIfc db, IfcWindowType t) : base(db,t) { mPredefinedType = t.mPredefinedType; mPartitioningType = t.mPartitioningType; mParameterTakesPrecedence = t.mParameterTakesPrecedence; mUserDefinedPartitioningType = t.mUserDefinedPartitioningType; }
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
		protected override string BuildStringSTEP()
		{
			return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? base.BuildStringSTEP() + ",.NOTDEFINED.,.NOTDEFINED.," + ParserSTEP.BoolToString(mParameterTakesPrecedence) + "," + ParserSTEP.BoolToString(false) :
				base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + ".,." + mPartitioningType.ToString() + ".," + ParserSTEP.BoolToString(mParameterTakesPrecedence) + (mUserDefinedPartitioningType == "$" ? ",$" : ",'" + mUserDefinedPartitioningType + "'"));
		}
	}
	public partial class IfcWorkCalendar : IfcControl //IFC4
	{
		internal List<int> mWorkingTimes = new List<int>();// :	OPTIONAL SET [1:?] OF IfcWorkTime;
		internal List<int> mExceptionTimes = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcWorkTime;
		internal IfcWorkCalendarTypeEnum mPredefinedType = IfcWorkCalendarTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkCalendarTypeEnum 
		public List<IfcWorkTime> WorkingTimes { get { return mWorkingTimes.ConvertAll(x => mDatabase[x] as IfcWorkTime); } set { mWorkingTimes = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		public List<IfcWorkTime> ExceptionTimes { get { return mExceptionTimes.ConvertAll(x => mDatabase[x] as IfcWorkTime); } set { mExceptionTimes = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		internal IfcWorkCalendar() : base() { }
		internal IfcWorkCalendar(DatabaseIfc db, IfcWorkCalendar c) : base(db,c) { WorkingTimes = c.WorkingTimes.ConvertAll(x => db.Factory.Duplicate(x) as IfcWorkTime); ExceptionTimes = c.ExceptionTimes.ConvertAll(x=>db.Factory.Duplicate(x) as IfcWorkTime); mPredefinedType = c.mPredefinedType; }
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
		protected override string BuildStringSTEP()
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
			return base.BuildStringSTEP() + str + mPredefinedType.ToString() + ".";
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

		public string Purpose { get { return (mPurpose == "$" ? "" : ParserIfc.Decode(mPurpose)); } set { mPurpose = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcWorkControl() : base() { }
		protected IfcWorkControl(DatabaseIfc db, IfcWorkControl c) : base(db,c)
		{
			mCreationDate = c.mCreationDate;
			if(c.mCreators.Count > 0)
				mCreators = c.mCreators.ConvertAll(x=>db.Factory.Duplicate(c.mDatabase[x]).mIndex);
			mDuration = c.mDuration;
			mTotalFloat = c.mTotalFloat;
			mStartTime = c.mStartTime;
			mFinishTime = c.mFinishTime;
		}
		
		protected static void parseFields(IfcWorkControl c, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcControl.parseFields(c, arrFields, ref ipos,schema);
			if (schema == ReleaseVersion.IFC2x3)
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
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "'" + mIdentification + "'," + ParserSTEP.LinkToString(mSSCreationDate) : (mCreationDate == "$" ? "$" : "'" + mCreationDate + "'"));
			if (mCreators.Count > 0)
			{
				str += ",(" + ParserSTEP.LinkToString(mCreators[0]);
				for (int icounter = 1; icounter < mCreators.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mCreators[icounter]);
				str += "),";
			}
			else
				str += ",$,";
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
				return str + (mPurpose == "$" ? "$," : "'" + mPurpose + "',") + ParserSTEP.DoubleOptionalToString(mSSDuration) + "," + ParserSTEP.DoubleOptionalToString(mSSTotalFloat) + "," +
					ParserSTEP.LinkToString(mSSStartTime) + "," + ParserSTEP.LinkToString(mSSFinishTime) + ",." + mWorkControlType.ToString() + (mUserDefinedControlType == "$" ? ".,$" : ".,'" + mUserDefinedControlType + "'");
			return str + (mPurpose == "$" ? "$," : "'" + mPurpose + "',") + mDuration + "," + mTotalFloat + (mStartTime == "$" ? ",$," : ",'" + mStartTime + "',") + (mFinishTime == "$" ? "$" : "'" + mFinishTime + "'");
		}
		internal DateTime getStart() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? (mDatabase[mSSStartTime] as IfcDateTimeSelect).DateTime : DateTime.MinValue); }
		
	}
	public partial class IfcWorkPlan : IfcWorkControl
	{
		internal IfcWorkPlanTypeEnum mPredefinedType = IfcWorkPlanTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkPlanTypeEnum; IFC4
		internal IfcWorkPlan() : base() { }
		internal IfcWorkPlan(DatabaseIfc db, IfcWorkPlan p) : base(db,p) { mPredefinedType = p.mPredefinedType; }
		internal static IfcWorkPlan Parse(string strDef) { IfcWorkPlan p = new IfcWorkPlan(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcWorkPlan p, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcWorkControl.parseFields(p, arrFields, ref ipos,schema);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					p.mPredefinedType = (IfcWorkPlanTypeEnum)Enum.Parse(typeof(IfcWorkPlanTypeEnum), s.Replace(".", ""));
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcWorkSchedule : IfcWorkControl
	{
		internal IfcWorkScheduleTypeEnum mPredefinedType = IfcWorkScheduleTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkScheduleTypeEnum; IFC4
		internal IfcWorkSchedule() : base() { }
		internal IfcWorkSchedule(DatabaseIfc db, IfcWorkSchedule s) : base(db,s) { mPredefinedType = s.mPredefinedType; }
		internal static IfcWorkSchedule Parse(string strDef, ReleaseVersion schema) { IfcWorkSchedule s = new IfcWorkSchedule(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return s; }
		internal static void parseFields(IfcWorkSchedule s, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcWorkControl.parseFields(s, arrFields, ref ipos,schema);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string st = arrFields[ipos++];
				if (st.StartsWith("."))
					s.mPredefinedType = (IfcWorkScheduleTypeEnum)Enum.Parse(typeof(IfcWorkScheduleTypeEnum), st.Replace(".", ""));
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcWorkTime : IfcSchedulingTime //IFC4
	{
		internal int mRecurrencePattern;// OPTIONAL	IfcRecurrencePattern
		internal string mStart = "$";//	 :	OPTIONAL IfcDate;
		internal string mFinish = "$";//	 :	OPTIONAL IfcDate; 
		internal IfcWorkTime() : base() { }
		internal IfcWorkTime(DatabaseIfc db, IfcWorkTime t) : base(db,t) { mRecurrencePattern = t.mRecurrencePattern; mStart = t.mStart; mFinish = t.mFinish; }
		internal IfcWorkTime(DatabaseIfc db, IfcRecurrencePattern recur, DateTime start, DateTime finish)
			: base(db) { if (recur != null) mRecurrencePattern = recur.mIndex; if (start != DateTime.MinValue) mStart = IfcDate.convert(start); if (finish != DateTime.MinValue) mFinish = IfcDate.convert(finish); }
		internal static IfcWorkTime Parse(string strDef) { IfcWorkTime f = new IfcWorkTime(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
		internal static void parseFields(IfcWorkTime f, List<string> arrFields, ref int ipos)
		{
			IfcSchedulingTime.parseFields(f, arrFields, ref ipos);
			f.mRecurrencePattern = ParserSTEP.ParseLink(arrFields[ipos++]);
			f.mStart = arrFields[ipos++].Replace("'", "");
			f.mFinish = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRecurrencePattern) + (mStart == "$" ? ",$," : ",'" + mStart + "',") + (mFinish == "$" ? "$" : "'" + mFinish + "'")); }
	}
}
