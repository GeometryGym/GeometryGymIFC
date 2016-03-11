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
	public class IfcSanitaryTerminal : IfcFlowTerminal
	{
		internal IfcSanitaryTerminalTypeEnum mPredefinedType = IfcSanitaryTerminalTypeEnum.NOTDEFINED;// : OPTIONAL IfcSanitaryTerminalTypeEnum; 
		public IfcSanitaryTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSanitaryTerminal() : base() { }
		internal IfcSanitaryTerminal(IfcSanitaryTerminal t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcSanitaryTerminal(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcSanitaryTerminal t, List<string> arrFields, ref int ipos) { IfcFlowTerminal.parseFields(t, arrFields, ref ipos); string s = arrFields[ipos++]; if (s[0] == '.') t.mPredefinedType = (IfcSanitaryTerminalTypeEnum)Enum.Parse(typeof(IfcSanitaryTerminalTypeEnum), s.Substring(1, s.Length - 2)); }
		internal new static IfcSanitaryTerminal Parse(string strDef) { IfcSanitaryTerminal t = new IfcSanitaryTerminal(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcSanitaryTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcSanitaryTerminalType : IfcFlowTerminalType
	{
		internal IfcSanitaryTerminalTypeEnum mPredefinedType = IfcSanitaryTerminalTypeEnum.NOTDEFINED;// : IfcSanitaryTerminalTypeEnum; 
		public IfcSanitaryTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcSanitaryTerminalType() : base() { }
		internal IfcSanitaryTerminalType(IfcSanitaryTerminalType be) : base(be) { mPredefinedType = be.mPredefinedType; }
		public IfcSanitaryTerminalType(DatabaseIfc m, string name, IfcSanitaryTerminalTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcSanitaryTerminalType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSanitaryTerminalTypeEnum)Enum.Parse(typeof(IfcSanitaryTerminalTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSanitaryTerminalType Parse(string strDef) { IfcSanitaryTerminalType t = new IfcSanitaryTerminalType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcScheduleTimeControl : IfcControl // DEPRECEATED IFC4
	{
		internal int mActualStart, mEarlyStart, mLateStart, mScheduleStart, mActualFinish, mEarlyFinish, mLateFinish, mScheduleFinish;// OPTIONAL  IfcDateTimeSelect;
		internal double mScheduleDuration, mActualDuration, mRemainingTime, mFreeFloat, mTotalFloat;//	 OPTIONAL IfcTimeMeasure;
		internal bool mIsCritical;//	 :	OPTIONAL BOOLEAN;
		internal int mStatusTime;//	: 	OPTIONAL IfcDateTimeSelect
		internal double mStartFloat, mFinishFloat;//	 OPTIONAL IfcTimeMeasure; 
		internal double mCompletion;//	 :	OPTIONAL IfcPositiveRatioMeasure; 
		//INVERSE
		internal IfcRelAssignsTasks mScheduleTimeControlAssigned = null;//	 : 	IfcRelAssignsTasks FOR TimeForTask;

		
		internal IfcScheduleTimeControl() : base() { }
		internal IfcScheduleTimeControl(IfcScheduleTimeControl c) : base(c)
		{
			mActualStart = c.mActualStart; mEarlyStart = c.mEarlyStart; mLateStart = c.mLateStart; mScheduleStart = c.mScheduleStart;
			mActualFinish = c.mActualFinish; mEarlyFinish = c.mEarlyFinish; mLateFinish = c.mLateFinish; mScheduleFinish = c.mScheduleFinish;
			mScheduleDuration = c.mScheduleDuration; mActualDuration = c.mActualDuration; mRemainingTime = c.mRemainingTime; mFreeFloat = c.mFreeFloat;
			mTotalFloat = c.mTotalFloat; mIsCritical = c.mIsCritical; mStatusTime = c.mStatusTime; mStartFloat = c.mStartFloat; mFinishFloat = c.mFinishFloat; mCompletion = c.mCompletion;
		}
		
		internal static IfcScheduleTimeControl Parse(string strDef, Schema schema) { IfcScheduleTimeControl s = new IfcScheduleTimeControl(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return s; }
		internal static void parseFields(IfcScheduleTimeControl s, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcControl.parseFields(s, arrFields, ref ipos,schema);
			s.mActualStart = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mEarlyStart = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mLateStart = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mScheduleStart = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mActualFinish = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mEarlyFinish = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mLateFinish = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mScheduleFinish = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mScheduleDuration = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mActualDuration = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mRemainingTime = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mFreeFloat = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mTotalFloat = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mIsCritical = ParserSTEP.ParseBool(arrFields[ipos++]);
			s.mStatusTime = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mStartFloat = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mFinishFloat = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mCompletion = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildString()
		{
			return base.BuildString() + "," + ParserSTEP.LinkToString(mActualStart) + "," + ParserSTEP.LinkToString(mEarlyStart) + "," + ParserSTEP.LinkToString(mLateStart) + "," +
				ParserSTEP.LinkToString(mScheduleStart) + "," + ParserSTEP.LinkToString(mActualFinish) + "," + ParserSTEP.LinkToString(mEarlyFinish) + "," + ParserSTEP.LinkToString(mLateFinish) + "," + ParserSTEP.LinkToString(mScheduleFinish) + "," +
				ParserSTEP.DoubleOptionalToString(mScheduleDuration) + "," + ParserSTEP.DoubleOptionalToString(mActualDuration) + "," + ParserSTEP.DoubleOptionalToString(mRemainingTime) + "," + ParserSTEP.DoubleOptionalToString(mFreeFloat) + "," + ParserSTEP.DoubleOptionalToString(mTotalFloat) + "," + ParserSTEP.BoolToString(mIsCritical) + "," + ParserSTEP.LinkToString(mStatusTime) + "," +
				ParserSTEP.DoubleOptionalToString(mStartFloat) + "," + ParserSTEP.DoubleOptionalToString(mFinishFloat) + "," + ParserSTEP.DoubleOptionalToString(mCompletion); //(mScheduleTimeControlAssigned == null ? "" :
		}
		internal DateTime getActualStart() { IfcDateTimeSelect dts = mDatabase.mIfcObjects[mActualStart] as IfcDateTimeSelect; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal DateTime getActualFinish() { IfcDateTimeSelect dts = mDatabase.mIfcObjects[mActualFinish] as IfcDateTimeSelect; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal DateTime getScheduleStart() { IfcDateTimeSelect dts = mDatabase.mIfcObjects[mScheduleStart] as IfcDateTimeSelect; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal DateTime getScheduleFinish() { IfcDateTimeSelect dts = mDatabase.mIfcObjects[mScheduleFinish] as IfcDateTimeSelect; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal TimeSpan getScheduleDuration() { return new TimeSpan(0, 0, (int)mScheduleDuration); }
	}
	public abstract class IfcSchedulingTime : BaseClassIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcEventTime, IfcLagTime, IfcResourceTime, IfcTaskTime, IfcWorkTime));
	{
		internal string mName = "$";//	 :	OPTIONAL IfcLabel;
		internal IfcDataOriginEnum mDataOrigin = IfcDataOriginEnum.NOTDEFINED;// OPTIONAL : IfcDataOriginEnum;
		internal string mUserDefinedDataOrigin = "$";//:	OPTIONAL IfcLabel; 
		protected IfcSchedulingTime() : base() { }
		protected IfcSchedulingTime(IfcSchedulingTime i) : base() { mName = i.mName; mDataOrigin = i.mDataOrigin; mUserDefinedDataOrigin = i.mUserDefinedDataOrigin; }
		protected IfcSchedulingTime(DatabaseIfc m, string name, IfcDataOriginEnum origin, string userOrigin)
			: base(m)
		{
			if (!string.IsNullOrEmpty(name))
				mName = name.Replace("'", "");
			mDataOrigin = origin;
			if (!string.IsNullOrEmpty(userOrigin))
				mUserDefinedDataOrigin = userOrigin.Replace("'", "");
		}
		internal static void parseFields(IfcSchedulingTime p, List<string> arrFields, ref int ipos)
		{
			p.mName = arrFields[ipos++];
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				p.mDataOrigin = (IfcDataOriginEnum)Enum.Parse(typeof(IfcDataOriginEnum), s.Replace(".", ""));
			p.mUserDefinedDataOrigin = arrFields[ipos++];
		}
		protected override string BuildString() { return base.BuildString() + (mName == "$" ? ",$,." : ",'" + mName + "',.") + mDataOrigin.ToString() + (mUserDefinedDataOrigin == "$" ? ".,$" : ".,'" + mUserDefinedDataOrigin + "'"); }
	}
	public partial class IfcSectionedSpine : IfcGeometricRepresentationItem
	{
		internal int mSpineCurve;// : IfcCompositeCurve;
		internal List<int> mCrossSections = new List<int>();// : LIST [2:?] OF IfcProfileDef;
		internal List<int> mCrossSectionPositions = new List<int>();// : LIST [2:?] OF IfcAxis2Placement3D; 

		internal IfcCompositeCurve SpineCurve { get { return mDatabase.mIfcObjects[mSpineCurve] as IfcCompositeCurve; } }
		internal List<IfcProfileDef> CrossSections { get { return mCrossSections.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcProfileDef); } }
		internal List<IfcAxis2Placement3D> CrossSectionPositions { get { return mCrossSectionPositions.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcAxis2Placement3D); } }

		internal IfcSectionedSpine() : base() { }
		internal IfcSectionedSpine(IfcSectionedSpine p) : base(p) { mSpineCurve = p.mSpineCurve; mCrossSections = new List<int>(p.mCrossSections.ToArray()); mCrossSectionPositions = new List<int>(p.mCrossSectionPositions.ToArray()); }
		internal static void parseFields(IfcSectionedSpine s, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(s, arrFields, ref ipos); s.mSpineCurve = ParserSTEP.ParseLink(arrFields[ipos++]); s.mCrossSections = ParserSTEP.SplitListLinks(arrFields[ipos++]); s.mCrossSectionPositions = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcSectionedSpine Parse(string strDef) { IfcSectionedSpine s = new IfcSectionedSpine(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + ParserSTEP.LinkToString(mSpineCurve) + ",("
				+ ParserSTEP.LinkToString(mCrossSections[0]);
			for (int icounter = 1; icounter < mCrossSections.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mCrossSections[icounter]);
			str += "),(" + ParserSTEP.LinkToString(mCrossSectionPositions[0]);
			for (int icounter = 1; icounter < mCrossSectionPositions.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mCrossSectionPositions[icounter]);
			return str + ")";
		}
	} 
	public class IfcSectionProperties : IfcPreDefinedProperties // IFC2x3 STPEntity
	{
		internal IfcSectionTypeEnum mSectionType;// : IfcSectionTypeEnum;
		internal int mStartProfile;// IfcProfileDef;
		internal int mEndProfile;// : OPTIONAL IfcProfileDef;
		internal IfcSectionProperties() : base() { }
		internal IfcSectionProperties(IfcSectionProperties i) : base() { mSectionType = i.mSectionType; mStartProfile = i.mStartProfile; mEndProfile = i.mEndProfile; }
		internal static IfcSectionProperties Parse(string strDef) { IfcSectionProperties p = new IfcSectionProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcSectionProperties p, List<string> arrFields, ref int ipos) { p.mSectionType = (IfcSectionTypeEnum)Enum.Parse(typeof(IfcSectionTypeEnum), arrFields[ipos++].Replace(".", "")); p.mStartProfile = ParserSTEP.ParseLink(arrFields[ipos++]); p.mEndProfile = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + ",." + mSectionType.ToString() + ".," + ParserSTEP.LinkToString(mStartProfile) + "," + ParserSTEP.LinkToString(mEndProfile); }
	}
	public class IfcSectionReinforcementProperties : IfcPreDefinedProperties // IFC2x3 STPEntity
	{
		internal double mLongitudinalStartPosition;//	:	IfcLengthMeasure;
		internal double mLongitudinalEndPosition;//	:	IfcLengthMeasure;
		internal double mTransversePosition = 0;//	:	OPTIONAL IfcLengthMeasure;
		internal IfcReinforcingBarRoleEnum mReinforcementRole = IfcReinforcingBarRoleEnum.NOTDEFINED;//	:	IfcReinforcingBarRoleEnum;
		internal int mSectionDefinition;//	:	IfcSectionProperties;
		internal List<int> mCrossSectionReinforcementDefinitions = new List<int>();//	:	SET [1:?] OF IfcReinforcementBarProperties;
		internal IfcSectionReinforcementProperties() : base() { }
		internal IfcSectionReinforcementProperties(IfcSectionReinforcementProperties i)
			: base()
		{
			mLongitudinalStartPosition = i.mLongitudinalStartPosition;
			mLongitudinalEndPosition = i.mLongitudinalEndPosition;
			mTransversePosition = i.mTransversePosition;
			mReinforcementRole = i.mReinforcementRole;
			mSectionDefinition = i.mSectionDefinition;
			mCrossSectionReinforcementDefinitions = new List<int>(i.mCrossSectionReinforcementDefinitions);
		}
		internal static IfcSectionReinforcementProperties Parse(string strDef) { IfcSectionReinforcementProperties p = new IfcSectionReinforcementProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcSectionReinforcementProperties p, List<string> arrFields, ref int ipos)
		{
			p.mLongitudinalStartPosition = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mLongitudinalEndPosition = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mTransversePosition = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mReinforcementRole = (IfcReinforcingBarRoleEnum)Enum.Parse(typeof(IfcReinforcingBarRoleEnum), arrFields[ipos++].Replace(".", ""));
			p.mSectionDefinition = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mCrossSectionReinforcementDefinitions = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		protected override string BuildString()
		{
			string result = base.BuildString() + "," + ParserSTEP.DoubleToString(mLongitudinalStartPosition) + "," + ParserSTEP.DoubleToString(mLongitudinalEndPosition) + "," +
			ParserSTEP.DoubleOptionalToString(mTransversePosition) + ",." + mReinforcementRole.ToString() + ".," + ParserSTEP.LinkToString(mSectionDefinition) + ",(" + ParserSTEP.LinkToString(mCrossSectionReinforcementDefinitions[0]);
			for (int icounter = 1; icounter < mCrossSectionReinforcementDefinitions.Count; icounter++)
				result += ",#" + mCrossSectionReinforcementDefinitions;
			return result + ")";
		}
	}
	public interface IfcSegmentIndexSelect { } //SELECT ( IfcLineIndex, IfcArcIndex);
	public class IfcSensor : IfcDistributionControlElement //IFC4  
	{
		internal IfcSensorTypeEnum mPredefinedType = IfcSensorTypeEnum.NOTDEFINED;
		public IfcSensorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcSensor() : base() { }
		internal IfcSensor(IfcSensor s) : base(s) { mPredefinedType = s.mPredefinedType; }
		internal IfcSensor(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcSensor a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcSensorTypeEnum)Enum.Parse(typeof(IfcSensorTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcSensor Parse(string strDef) { IfcSensor d = new IfcSensor(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcSensorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcSensorType : IfcDistributionControlElementType
	{
		internal IfcSensorTypeEnum mPredefinedType = IfcSensorTypeEnum.NOTDEFINED;// : IfcSensorTypeEnum; 
		public IfcSensorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSensorType() : base() { }
		internal IfcSensorType(IfcSensorType be) : base(be) { mPredefinedType = be.mPredefinedType; }
		public IfcSensorType(DatabaseIfc m, string name, IfcSensorTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcSensorType t, List<string> arrFields, ref int ipos) { IfcDistributionControlElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSensorTypeEnum)Enum.Parse(typeof(IfcSensorTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSensorType Parse(string strDef) { IfcSensorType t = new IfcSensorType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	//ENTITY IfcServiceLife // DEPRECEATED IFC4
	//ENTITY IfcServiceLifeFactor // DEPRECEATED IFC4
	public partial class IfcShadingDevice : IfcBuildingElement
	{
		internal IfcShadingDeviceTypeEnum mPredefinedType = IfcShadingDeviceTypeEnum.NOTDEFINED;//: OPTIONAL IfcShadingDeviceTypeEnum; 
		public IfcShadingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcShadingDevice() : base() { }
		internal IfcShadingDevice(IfcShadingDevice d) : base(d) { mPredefinedType = d.mPredefinedType; }
		public IfcShadingDevice(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }

		internal static IfcShadingDevice Parse(string strDef, Schema schema) { IfcShadingDevice w = new IfcShadingDevice(); int ipos = 0; parseFields(w, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return w; }
		internal static void parseFields(IfcShadingDevice w, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcBuildingElement.parseFields(w, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					w.mPredefinedType = (IfcShadingDeviceTypeEnum)Enum.Parse(typeof(IfcShadingDeviceTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcShadingDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcShadingDeviceType : IfcBuildingElementType
	{
		internal IfcShadingDeviceTypeEnum mPredefinedType = IfcShadingDeviceTypeEnum.NOTDEFINED;
		public IfcShadingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcShadingDeviceType() : base() { }
		internal IfcShadingDeviceType(IfcShadingDeviceType b) : base(b) { mPredefinedType = b.mPredefinedType; }
		public IfcShadingDeviceType(DatabaseIfc m, string name, IfcShadingDeviceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcShadingDeviceType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcShadingDeviceTypeEnum)Enum.Parse(typeof(IfcShadingDeviceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcShadingDeviceType Parse(string strDef) { IfcShadingDeviceType t = new IfcShadingDeviceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcShapeAspect : BaseClassIfc
	{
		internal List<int> mShapeRepresentations = new List<int>();// : LIST [1:?] OF IfcShapeModel;
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		private IfcLogicalEnum mProductDefinitional;// : LOGICAL;
		internal int mPartOfProductDefinitionShape;// IFC4 OPTIONAL IfcProductRepresentationSelect IFC2x3 IfcProductRepresentationSelect; 

		public List<IfcShapeModel> ShapeRepresentations { get { return mShapeRepresentations.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcShapeModel); } }
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcLogicalEnum ProductDefinitional { get { return mProductDefinitional; } set { mProductDefinitional = value; } }
		public IfcProductRepresentationSelect PartOfProductDefinitionShape { get { return mDatabase.mIfcObjects[mPartOfProductDefinitionShape] as IfcProductRepresentationSelect; } }

		internal IfcShapeAspect() : base() { }
		internal IfcShapeAspect(IfcShapeAspect a) : base()
		{
			mShapeRepresentations = new List<int>(a.mShapeRepresentations.ToArray());
			mName = a.mName;
			mDescription = a.mDescription;
			mProductDefinitional = a.mProductDefinitional;
			mPartOfProductDefinitionShape = a.mPartOfProductDefinitionShape;
		}
		public IfcShapeAspect(List<IfcShapeModel> reps, string name, string desc, IfcProductRepresentationSelect pr)
			: this(reps[0].mDatabase, name, desc, pr) { mShapeRepresentations = reps.ConvertAll(x => x.mIndex); }
		public IfcShapeAspect(IfcShapeModel rep, string name, string desc, IfcProductRepresentationSelect pr)
			: this(rep.mDatabase, name, desc, pr) { mShapeRepresentations.Add(rep.mIndex); }
		private IfcShapeAspect(DatabaseIfc db, string name, string desc, IfcProductRepresentationSelect pr) : base(db)
		{
			Name = name;
			Description = desc;
			if (pr != null)
				mPartOfProductDefinitionShape = pr.Index;
		}
		internal static IfcShapeAspect Parse(string strDef) { IfcShapeAspect a = new IfcShapeAspect(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcShapeAspect a, List<string> arrFields, ref int ipos)
		{
			a.mShapeRepresentations = ParserSTEP.SplitListLinks(arrFields[ipos++]);

			a.mName = arrFields[ipos++].Replace("'", "");
			a.mDescription = arrFields[ipos++].Replace("'", "");
			a.mProductDefinitional = ParserIfc.ParseIFCLogical(arrFields[ipos++]);
			a.mPartOfProductDefinitionShape = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mShapeRepresentations[0]);
			for (int icounter = 1; icounter < mShapeRepresentations.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mShapeRepresentations[icounter]);
			return str + (mName == "$" ? "),$," : "),'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserIfc.LogicalToString(mProductDefinitional)
				+ "," + ParserSTEP.LinkToString(mPartOfProductDefinitionShape);
		}
		internal void relate()
		{
			List<IfcShapeModel> sms = ShapeRepresentations;
			for (int icounter = 0; icounter < sms.Count; icounter++)
				sms[icounter].mOfShapeAspect = this;

			IfcProductRepresentationSelect pds = PartOfProductDefinitionShape;
			if (pds != null)
				pds.HasShapeAspects.Add(this);
		}
	}
	public abstract class IfcShapeModel : IfcRepresentation//ABSTRACT SUPERTYPE OF (ONEOF (IfcShapeRepresentation,IfcTopologyRepresentation))
	{
		//INVERSE
		internal IfcShapeAspect mOfShapeAspect = null; //:	SET [0:1] OF IfcShapeAspect FOR ShapeRepresentations;

		protected IfcShapeModel() : base() { }
		protected IfcShapeModel(IfcShapeModel i) : base(i) { }
		protected IfcShapeModel(IfcRepresentationItem ri, string identifier, string repType) : base(ri, identifier, repType) { }
		protected IfcShapeModel(List<IfcRepresentationItem> reps, string identifier, string repType) : base(reps, identifier, repType) { }
		protected static void parseString(IfcShapeModel shape, string str, ref int pos) { IfcRepresentation.parseString(shape, str, ref pos); }
	}
	public partial class IfcShapeRepresentation : IfcShapeModel
	{
		/*Curve2D	2 dimensional curve
Curve3D	3 dimensional curve
Surface2D	2 dimensional surface (a region on ground view)
Surface3D	3 dimensional surface
GeometricSet	points, curves, surfaces (2 or 3 dimensional)
	GeometricCurveSet	points, curves (2 or 3 dimensional)
Annotation2D	points, curves (2 or 3 dimensional), hatches and text (2 dimensional)
SurfaceModel	face based and shell based surface model
SolidModel	including swept solid, Boolean results and Brep bodies
more specific types are:
	SweptSolid	swept area solids, by extrusion and revolution, excluding tapered sweeps
	AdvancedSweptSolid	swept area solids created by sweeping a profile aint a directrix, and tapered sweeps
	Brep	faceted Brep's with and without voids
	AdvancedBrep	Brep's based on advanced faces, with b-spline surface geometry, with and without voids
	CSG	Boolean results of operations between solid models, half spaces and Boolean results
	Clipping	Boolean differences between swept area solids, half spaces and Boolean results
additional types	some additional representation types are given:
	BoundingBox	simplistic 3D representation by a bounding box
	SectionedSpine	cross section based representation of a spine curve and planar cross sections. It can represent a surface or a solid and the interpolations of the between the cross sections is not defined
	MappedRepresentation*/
		internal IfcShapeRepresentation() : base() { }
		internal IfcShapeRepresentation(IfcShapeRepresentation p) : base(p) { }
		public IfcShapeRepresentation(IfcGeometricRepresentationItem representation) : base(representation, "Body", "") { setIdentifiers(representation); }
		public IfcShapeRepresentation(List<IfcRepresentationItem> items) : base(items, "Body", "") { setIdentifiers(items[0]); }
		public IfcShapeRepresentation(IfcAdvancedBrep ab) : base(ab, "Body", "AdvancedBrep") { }
		public IfcShapeRepresentation(IfcBooleanResult br)
			: base(br, "Body", "CSG")
		{
			IfcBooleanClippingResult bcr = br as IfcBooleanClippingResult;
			if (bcr != null)
				mRepresentationType = "Clipping";
		}

		public IfcShapeRepresentation(IfcCsgPrimitive3D cg) : base(cg, "Body", "CSG") { }
		public IfcShapeRepresentation(IfcCsgSolid cg) : base(cg, "Body", "CSG") { }
		//should remove above as in 3d?? hierarchy test
		public IfcShapeRepresentation(IfcFacetedBrep fb) : base(fb, "Body", "Brep") { }
		public IfcShapeRepresentation(IfcFaceBasedSurfaceModel sm) : base(sm, "Body", "SurfaceModel") { }
		public IfcShapeRepresentation(IfcGeometricSet gs) : base(gs, "Body", "GeometricSet") { }
		public IfcShapeRepresentation(IfcMappedItem mi) : base(mi, "Body", "MappedRepresentation") { }
		public IfcShapeRepresentation(IfcPoint p) : base(p, "", "Point") { }
		//internal IfcShapeRepresentation(IfcRepresentationMap rm) : base(new IfcMappedItem(rm), "Model", "MappedRepresentation") { }
		public IfcShapeRepresentation(IfcSectionedSpine ss) : base(ss, "Body", "SectionedSpine")
		{
			if (ss.mCrossSections.Count > 0)
			{
				IfcProfileDef pd = mDatabase.mIfcObjects[ss.mCrossSections[0]] as IfcProfileDef;
				if (pd.mProfileType == IfcProfileTypeEnum.CURVE)
					mRepresentationIdentifier = "Surface3D";
			}
		}
		public IfcShapeRepresentation(IfcShellBasedSurfaceModel sm) : base(sm, "Body", "SurfaceModel") { }
		public IfcShapeRepresentation(IfcSurface s) : base(s, "Surface", "Surface3D") { }
		public IfcShapeRepresentation(IfcSweptAreaSolid sm) : base(sm, "Body", "SweptSolid") { }
		public IfcShapeRepresentation(IfcSweptDiskSolid sm) : base(sm, "Body", "AdvancedSweptSolid") { }
		public IfcShapeRepresentation(IfcSolidModel sm) : base(sm, "Body", "SweptSolid")
		{
			//ABSTRACT SUPERTYPE OF (ONEOF(IfcCsgSolid ,IfcManifoldSolidBrep,IfcSweptAreaSolid,IfcSweptDiskSolid))
			IfcCsgSolid cs = sm as IfcCsgSolid;
			if (cs != null)
				mRepresentationType = "CSG";
			else
			{
				IfcManifoldSolidBrep msb = sm as IfcManifoldSolidBrep;
				if (msb != null)
					mRepresentationType = "Brep";
				else
				{
					IfcAdvancedBrep ab = sm as IfcAdvancedBrep;
					if (ab != null)
						mRepresentationType = "AdvancedBrep";
				}
			}

		}
		public IfcShapeRepresentation(IfcTessellatedItem ti) : base(ti, "Body", "Tessellation") { } //Tessellation
		public IfcShapeRepresentation(List<IfcMappedItem> reps) : base(reps.ConvertAll(x => (IfcRepresentationItem)x), "Body", "MappedRepresentation") { }
	
		internal new static IfcShapeRepresentation Parse(string strDef)
		{
			IfcShapeRepresentation r = new IfcShapeRepresentation();
			int pos = 0;
			IfcShapeModel.parseString(r, strDef, ref pos);
			return r;
		}
		protected override string BuildString() { return (mDatabase.mOutputEssential ? "" : base.BuildString()); }

		internal static IfcShapeRepresentation CreateRepresentation(IfcRepresentationItem ri)
		{
			if (ri == null)
				return null;
			IfcBooleanResult br = ri as IfcBooleanResult;
			if (br != null)
				return new IfcShapeRepresentation(br);
			IfcCurve c = ri as IfcCurve;
			if (c != null)
				return new IfcShapeRepresentation(c);
			IfcCsgPrimitive3D csg = ri as IfcCsgPrimitive3D;
			if (csg != null)
				return new IfcShapeRepresentation(csg);
			IfcCsgSolid csgs = ri as IfcCsgSolid;
			if (csgs != null)
				return new IfcShapeRepresentation(csgs);
			IfcExtrudedAreaSolid eas = ri as IfcExtrudedAreaSolid;
			if (eas != null)
				return new IfcShapeRepresentation(eas);
			IfcFacetedBrep fb = ri as IfcFacetedBrep;
			if (fb != null)
				return new IfcShapeRepresentation(fb);
			IfcFaceBasedSurfaceModel fbs = ri as IfcFaceBasedSurfaceModel;
			if (fbs != null)
				return new IfcShapeRepresentation(fbs);
			IfcGeometricSet gs = ri as IfcGeometricSet;
			if (gs != null)
				return new IfcShapeRepresentation(gs);
			IfcPoint p = ri as IfcPoint;
			if (p != null)
				return new IfcShapeRepresentation(p);
			IfcSectionedSpine ss = ri as IfcSectionedSpine;
			if (ss != null)
				return new IfcShapeRepresentation(ss);
			IfcShellBasedSurfaceModel sbs = ri as IfcShellBasedSurfaceModel;
			if (sbs != null)
				return new IfcShapeRepresentation(sbs);
			IfcSurface s = ri as IfcSurface;
			if (s != null)
				return new IfcShapeRepresentation(s);
			IfcSweptAreaSolid sas = ri as IfcSweptAreaSolid;
			if (sas != null)
				return new IfcShapeRepresentation(sas);
			IfcSweptDiskSolid sds = ri as IfcSweptDiskSolid;
			if (sds != null)
				return new IfcShapeRepresentation(sds);
			IfcAdvancedBrep b = ri as IfcAdvancedBrep;
			if (b != null)
				return new IfcShapeRepresentation(b);
			IfcTessellatedItem ti = ri as IfcTessellatedItem;
			if (ti != null)
				return new IfcShapeRepresentation(ti);
			IfcMappedItem mi = ri as IfcMappedItem;
			if (mi != null)
				return new IfcShapeRepresentation(mi);
			ri.mDatabase.logError("XXX Error in identiying " + ri.ToString() + " as shape representation, please contact Jon!");
			return null;
		}

		internal static IfcShapeRepresentation getAxisRep(DatabaseIfc m, IfcBoundedCurve a) { return new IfcShapeRepresentation(a) { RepresentationIdentifier = "Axis", RepresentationType = "Curve3D" }; }
		internal static IfcShapeRepresentation getProfileRep(DatabaseIfc m, IfcCurve c) { return new IfcShapeRepresentation(c) { RepresentationIdentifier = "Profile", RepresentationType = "Curve3D" }; }
		internal static IfcShapeRepresentation getRowRep(DatabaseIfc m, IfcGeometricCurveSet cs) { return new IfcShapeRepresentation(cs) { RepresentationIdentifier = "Row", RepresentationType = "GeometricCurveSet" }; }

		private bool setIdentifiers(IfcRepresentationItem ri)
		{
			IfcGeometricRepresentationItem gri = ri as IfcGeometricRepresentationItem;
			if (ri != null)
			{
				mRepresentationIdentifier = "Body";
				mRepresentationType = "MappedRepresentation";
				IfcMappedItem mi = ri as IfcMappedItem;
				if (mi != null)
					return true;
				mRepresentationType = "SolidModel";
				IfcSolidModel sm = ri as IfcSolidModel; //IfcCsgSolid ,IfcManifoldSolidBrep,IfcSweptAreaSolid,IfcSweptDiskSolid
				if (sm != null)
					return true;

				mRepresentationType = "CSG";
				IfcBooleanResult br = ri as IfcBooleanResult;
				if (br != null)
					return true;
				IfcCsgPrimitive3D csg = ri as IfcCsgPrimitive3D;
				if (csg != null)
					return true;
				mRepresentationType = "SectionedSpine";
				IfcSectionedSpine ss = ri as IfcSectionedSpine;
				if (ss != null)
					return true;
				mRepresentationIdentifier = "Body";
				mRepresentationType = "SurfaceModel";
				IfcFaceBasedSurfaceModel fbs = ri as IfcFaceBasedSurfaceModel;
				if (fbs != null)
					return true;
				IfcShellBasedSurfaceModel sbs = ri as IfcShellBasedSurfaceModel;
				if (sbs != null)
					return true;
				mRepresentationType = "Tessellation";
				IfcTessellatedItem ti = ri as IfcTessellatedItem;
				if (ti != null)
					return true;
				mRepresentationIdentifier = "Surface";
				mRepresentationType = "Surface3D";
				IfcSurface s = ri as IfcSurface;
				if (s != null)
					return true;
				mRepresentationIdentifier = "Curve";
				mRepresentationType = "Curve3D";
				IfcCurve c = ri as IfcCurve;
				if (c != null)
					return true;
			}
			mRepresentationIdentifier = "$";
			mRepresentationType = "$";
			return false;
		}
	}
	public partial interface IfcShell : IfcInterface  // SELECT(IfcClosedShell, IfcOpenShell);
	{
		List<IfcFace> CfsFaces { get; }
	}
	public partial class IfcShellBasedSurfaceModel : IfcGeometricRepresentationItem
	{
		internal List<int> mSbsmBoundary = new List<int>();// : SET [1:?] OF IfcShell; 
		internal List<IfcShell> SbsmBoundary { get { return mSbsmBoundary.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcShell); } }
		internal IfcShellBasedSurfaceModel() : base() { }
		internal IfcShellBasedSurfaceModel(IfcShellBasedSurfaceModel m) : base(m) { mSbsmBoundary = new List<int>(m.mSbsmBoundary.ToArray()); }
		public IfcShellBasedSurfaceModel(IfcShell shell) : base(shell.Database) { mSbsmBoundary.Add(shell.Index); }
		public IfcShellBasedSurfaceModel(List<IfcShell> shells) : base(shells[0].Database) { mSbsmBoundary = shells.ConvertAll(x => x.Index); }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mSbsmBoundary[0]);
			for (int icounter = 1; icounter < mSbsmBoundary.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSbsmBoundary[icounter]);
			return str + ")";
		}
		internal static void parseFields(IfcShellBasedSurfaceModel s, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(s, arrFields, ref ipos); s.mSbsmBoundary = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcShellBasedSurfaceModel Parse(string strDef) { IfcShellBasedSurfaceModel s = new IfcShellBasedSurfaceModel(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public abstract partial class IfcSimpleProperty : IfcProperty //ABSTRACT SUPERTYPE OF (ONEOF (IfcPropertyBoundedValue ,IfcPropertyEnumeratedValue ,
	{ // IfcPropertyListValue,IfcPropertyReferenceValue,IfcPropertySingleValue,IfcPropertyTableValue)) 
		protected IfcSimpleProperty() : base() { }
		protected IfcSimpleProperty(IfcSimpleProperty p) : base(p) { }
		protected IfcSimpleProperty(DatabaseIfc m, string name, string desc) : base(m, name, desc) { }
		protected static void parseFields(IfcSimpleProperty p, List<string> arrFields, ref int ipos) { IfcProperty.parseFields(p, arrFields, ref ipos); }
	}
	public partial class IfcSite : IfcSpatialStructureElement
	{
		internal string mRefLatitude = "$";// : OPTIONAL IfcCompoundPlaneAngleMeasure;
		internal string mRefLongitude = "$";// : OPTIONAL IfcCompoundPlaneAngleMeasure;
		internal double mRefElevation;// : OPTIONAL IfcLengthMeasure;
		internal string mLandTitleNumber = "$";// : OPTIONAL IfcLabel;
		internal int mSiteAddress;// : OPTIONAL IfcPostalAddress; 

		internal IfcCompoundPlaneAngleMeasure RefLatitude { set { mRefLatitude = (value == null ? "$" : value.ToString()); } }
		internal IfcCompoundPlaneAngleMeasure RefLongitude { set { mRefLongitude = (value == null ? "$" : value.ToString()); } }
		internal double RefElevation { get { return mRefElevation; } set { mRefElevation = value; } }
		internal IfcPostalAddress SiteAddress { get { return mDatabase.mIfcObjects[mSiteAddress] as IfcPostalAddress; } set { mSiteAddress = (value == null ? 0 : value.mIndex); } }

		internal IfcSite() : base() { }
		internal IfcSite(IfcSite p) : base(p) { mRefLatitude = p.mRefLatitude; mRefLongitude = p.mRefLongitude; mRefElevation = p.mRefElevation; mLandTitleNumber = p.mLandTitleNumber; mSiteAddress = p.mSiteAddress; }
		internal IfcSite(DatabaseIfc db, string name) : base(db) { Name = name; }
		internal IfcSite(IfcSite host, string name) : base(host, name) { }
		internal static void parseFields(IfcSite s, List<string> arrFields, ref int ipos)
		{
			IfcSpatialStructureElement.parseFields(s, arrFields, ref ipos);
			s.mRefLatitude = arrFields[ipos++];
			s.mRefLongitude = arrFields[ipos++];
			s.mRefElevation = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mLandTitleNumber = arrFields[ipos++];
			s.mSiteAddress = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + mRefLatitude + "," + mRefLongitude + "," + ParserSTEP.DoubleToString(mRefElevation) + "," + mLandTitleNumber + "," + ParserSTEP.LinkToString(mSiteAddress); }
		internal static IfcSite Parse(string strDef) { IfcSite s = new IfcSite(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public partial class IfcSIUnit : IfcNamedUnit
	{
		private static double[] mFactors = new double[] { Math.Pow(10, 18), Math.Pow(10, 15), Math.Pow(10, 12), Math.Pow(10, 9), Math.Pow(10, 6), Math.Pow(10, 3), Math.Pow(10, 2), Math.Pow(10, 1), Math.Pow(10, -1), Math.Pow(10, -2), Math.Pow(10, -3), Math.Pow(10, -6), Math.Pow(10, -9), Math.Pow(10, -12), Math.Pow(10, -15), Math.Pow(10, -18), 1 };
		private IfcSIPrefix mPrefix = IfcSIPrefix.NONE;// : OPTIONAL IfcSIPrefix;
		private IfcSIUnitName mName;// : IfcSIUnitName; 

		internal IfcSIPrefix Prefix { get { return mPrefix; } }
		internal new IfcSIUnitName Name { get { return mName; } }

		internal IfcSIUnit(IfcSIUnit p) : base(p) { }
		internal IfcSIUnit() : base() { }
		public IfcSIUnit(DatabaseIfc m, IfcUnitEnum unitEnum, IfcSIPrefix pref, IfcSIUnitName name) : base(m, unitEnum, false) { mPrefix = pref; mName = name; }
		protected override string BuildString()
		{
			string str = base.BuildString();
			if (mPrefix == IfcSIPrefix.NONE)
				str += ",$,.";
			else
				str += ",." + mPrefix.ToString() + ".,.";
			return str + mName + ".";
		}
		internal static void parseFields(IfcSIUnit u, List<string> arrFields, ref int ipos)
		{
			IfcNamedUnit.parseFields(u, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str.Contains("$"))
				u.mPrefix = IfcSIPrefix.NONE;
			else
				u.mPrefix = (IfcSIPrefix)Enum.Parse(typeof(IfcSIPrefix), str.Replace(".", ""));
			u.mName = (IfcSIUnitName)Enum.Parse(typeof(IfcSIUnitName), arrFields[ipos++].Replace(".", ""));
		}
		internal static IfcSIUnit Parse(string strDef) { IfcSIUnit u = new IfcSIUnit(); int ipos = 0; parseFields(u, ParserSTEP.SplitLineFields(strDef), ref ipos); return u; }
		internal override double getSIFactor() { return mFactors[(int)mPrefix]; }
	}
	public partial class IfcSlab : IfcBuildingElement
	{
		internal IfcSlabTypeEnum mPredefinedType = IfcSlabTypeEnum.NOTDEFINED;// : OPTIONAL IfcSlabTypeEnum 
		public IfcSlabTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSlab() : base() { }
		internal IfcSlab(IfcSlab o) : base(o) { mPredefinedType = o.mPredefinedType; }
		public IfcSlab(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcSlab Parse(string strDef) { IfcSlab s = new IfcSlab(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcSlab s, List<string> arrFields, ref int ipos) { IfcBuildingElement.parseFields(s, arrFields, ref ipos); string str = arrFields[ipos++]; if (str != "$") s.mPredefinedType = (IfcSlabTypeEnum)Enum.Parse(typeof(IfcSlabTypeEnum), str.Replace(".", "")); }
		protected override string BuildString() { return base.BuildString() + (mPredefinedType == IfcSlabTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcSlabStandardCase : IfcSlab
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 || mDatabase.mModelView == ModelView.Ifc4Reference ? "IFCSLAB" : base.KeyWord); } }
		internal IfcSlabStandardCase() : base() { }
		internal IfcSlabStandardCase(IfcSlabStandardCase o) : base(o) { }
		internal new static IfcSlabStandardCase Parse(string strDef) { IfcSlabStandardCase s = new IfcSlabStandardCase(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcSlabStandardCase s, List<string> arrFields, ref int ipos) { IfcSlab.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcSlabType : IfcBuildingElementType
	{
		internal IfcSlabTypeEnum mPredefinedType = IfcSlabTypeEnum.NOTDEFINED;
		public IfcSlabTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcSlabType() : base() { }
		public IfcSlabType(DatabaseIfc m, string name, IfcSlabTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal IfcSlabType(IfcSlabType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcSlabType(string name, IfcMaterialLayerSet ls, IfcSlabTypeEnum type) : base(ls.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = ls; }
		internal static void parseFields(IfcSlabType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSlabTypeEnum)Enum.Parse(typeof(IfcSlabTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSlabType Parse(string strDef) { IfcSlabType t = new IfcSlabType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	//ENTITY IfcSlippageConnectionCondition
	public class IfcSolarDevice : IfcEnergyConversionDevice //IFC4
	{
		internal IfcSolarDeviceTypeEnum mPredefinedType = IfcSolarDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcSolarDeviceTypeEnum;
		public IfcSolarDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcSolarDevice() : base() { }
		internal IfcSolarDevice(IfcSolarDevice d) : base(d) { mPredefinedType = d.mPredefinedType; }
		internal IfcSolarDevice(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcSolarDevice s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcSolarDeviceTypeEnum)Enum.Parse(typeof(IfcSolarDeviceTypeEnum), str);
		}
		internal new static IfcSolarDevice Parse(string strDef) { IfcSolarDevice s = new IfcSolarDevice(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcSolarDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcSolarDeviceType : IfcEnergyConversionDeviceType
	{
		internal IfcSolarDeviceTypeEnum mPredefinedType = IfcSolarDeviceTypeEnum.NOTDEFINED;// : IfcSolarDeviceTypeEnum; 
		public IfcSolarDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcSolarDeviceType() : base() { }
		internal IfcSolarDeviceType(IfcSolarDeviceType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcSolarDeviceType(DatabaseIfc m, string name, IfcSolarDeviceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcSolarDeviceType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSolarDeviceTypeEnum)Enum.Parse(typeof(IfcSolarDeviceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSolarDeviceType Parse(string strDef) { IfcSolarDeviceType t = new IfcSolarDeviceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract partial class IfcSolidModel : IfcGeometricRepresentationItem, IfcBooleanOperand /* ABSTRACT SUPERTYPE OF (ONEOF(IfcCsgSolid ,IfcManifoldSolidBrep,IfcSweptAreaSolid,IfcSweptDiskSolid))*/
	{
		protected IfcSolidModel() : base() { }
		protected IfcSolidModel(IfcSolidModel p) : base(p) { }
		protected IfcSolidModel(DatabaseIfc m) : base(m) { }
		internal static void parseFields(IfcSolidModel s, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(s, arrFields, ref ipos); }
	}
	public class IfcSoundProperties : IfcPropertySetDefinition // DEPRECEATED IFC4
	{
		internal bool mIsAttenuating;// : IfcBoolean;
		internal IfcSoundScaleEnum mSoundScale = IfcSoundScaleEnum.NOTDEFINED;// : OPTIONAL IfcSoundScaleEnum
		internal List<int> mSoundValues = new List<int>(1);// : LIST [1:8] OF IfcSoundValue;
		internal IfcSoundProperties() : base() { }
		internal IfcSoundProperties(IfcSoundProperties p) : base(p) { mIsAttenuating = p.mIsAttenuating; mSoundScale = p.mSoundScale; mSoundValues = new List<int>(p.mSoundValues.ToArray()); }
		internal static IfcSoundProperties Parse(string strDef) { IfcSoundProperties p = new IfcSoundProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcSoundProperties p, List<string> arrFields, ref int ipos)
		{
			IfcPropertySetDefinition.parseFields(p, arrFields, ref ipos);
			p.mIsAttenuating = ParserSTEP.ParseBool(arrFields[ipos++]);
			string str = arrFields[ipos++];
			if (!str.StartsWith("$"))
				p.mSoundScale = (IfcSoundScaleEnum)Enum.Parse(typeof(IfcSoundScaleEnum), str);
			p.mSoundValues = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + ParserSTEP.BoolToString(mIsAttenuating) + ",." + mSoundScale.ToString() + ".,(" + ParserSTEP.LinkToString(mSoundValues[0]);
			for (int icounter = 1; icounter < mSoundValues.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSoundValues[icounter]);
			return str + ")";
		}
	}
	public class IfcSoundValue : IfcPropertySetDefinition // DEPRECEATED IFC4
	{
		internal int mSoundLevelTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal int mFrequency;// : IfcFrequencyMeasure;
		internal int mSoundLevelSingleValue;// : OPTIONAL IfcDerivedMeasureValue; 
		internal IfcSoundValue() : base() { }
		internal IfcSoundValue(IfcSoundValue i) : base(i) { mSoundLevelTimeSeries = i.mSoundLevelTimeSeries; mFrequency = i.mFrequency; mSoundLevelSingleValue = i.mSoundLevelSingleValue; }
		internal static IfcSoundValue Parse(string strDef) { IfcSoundValue v = new IfcSoundValue(); int ipos = 0; parseFields(v, ParserSTEP.SplitLineFields(strDef), ref ipos); return v; }
		internal static void parseFields(IfcSoundValue v, List<string> arrFields, ref int ipos) { IfcPropertySetDefinition.parseFields(v, arrFields, ref ipos); v.mSoundLevelTimeSeries = ParserSTEP.ParseLink(arrFields[ipos++]); v.mFrequency = ParserSTEP.ParseLink(arrFields[ipos++]); v.mSoundLevelSingleValue = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mSoundLevelTimeSeries) + "," + ParserSTEP.LinkToString(mFrequency) + "," + ParserSTEP.LinkToString(mSoundLevelSingleValue); }
	}
	public partial class IfcSpace : IfcSpatialStructureElement, IfcSpaceBoundarySelect
	{
		//internal IfcInternalOrExternalEnum mInteriorOrExteriorSpace = IfcInternalOrExternalEnum.NOTDEFINED;// : IfcInternalOrExternalEnum; replaced IFC4
		internal IfcSpaceTypeEnum mPredefinedType = IfcSpaceTypeEnum.NOTDEFINED; 	//:	OPTIONAL IfcSpaceTypeEnum;
		internal double mElevationWithFlooring;// : OPTIONAL IfcLengthMeasure;
		//INVERSE
		internal List<IfcRelCoversSpaces> mHasCoverings = new List<IfcRelCoversSpaces>(); // : SET [0:?] OF IfcRelCoversSpaces FOR RelatedSpace;
		internal List<IfcRelSpaceBoundary> mBoundedBy = new List<IfcRelSpaceBoundary>();  //	BoundedBy : SET [0:?] OF IfcRelSpaceBoundary FOR RelatingSpace;

		public IfcSpaceTypeEnum PredefinedType
		{
			get { return mPredefinedType; }
			set
			{
				mPredefinedType = value;
				if (mDatabase.mSchema == Schema.IFC2x3)
				{
					if (value != IfcSpaceTypeEnum.INTERNAL && value != IfcSpaceTypeEnum.EXTERNAL && value != IfcSpaceTypeEnum.NOTDEFINED)
					{
						mPredefinedType = IfcSpaceTypeEnum.NOTDEFINED;
						if (string.IsNullOrEmpty(ObjectType))
							ObjectType = value.ToString();
					}
				}
			}
		}
		public double ElevationWithFlooring { get { return mElevationWithFlooring; } set { mElevationWithFlooring = value; } }
		public List<IfcRelCoversSpaces> HasCoverings { get { return mHasCoverings; } }
		public List<IfcRelSpaceBoundary> BoundedBy { get { return mBoundedBy; } }

		internal IfcSpace() : base() { }
		internal IfcSpace(IfcSpace p) : base(p) { mPredefinedType = p.mPredefinedType; mElevationWithFlooring = p.mElevationWithFlooring; }
		internal IfcSpace(IfcSpatialStructureElement host, string name) : base(host, name)
		{
			IfcRelCoversSpaces cs = new IfcRelCoversSpaces(this, null);
		}

		internal static void parseFields(IfcSpace gp, List<string> arrFields, ref int ipos)
		{
			IfcSpatialStructureElement.parseFields(gp, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s[0] == '.')
				gp.mPredefinedType = (IfcSpaceTypeEnum)Enum.Parse(typeof(IfcSpaceTypeEnum), s.Replace(".", ""));
			gp.mElevationWithFlooring = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 || mPredefinedType != IfcSpaceTypeEnum.NOTDEFINED ? ",." + mPredefinedType.ToString() + ".," : ",$,") + ParserSTEP.DoubleOptionalToString(mElevationWithFlooring); }
		internal static IfcSpace Parse(string strDef) { IfcSpace s = new IfcSpace(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		public override bool AddElement(IfcElement s)
		{
			IfcCovering c = s as IfcCovering;
			if (c != null)
			{
				mHasCoverings[0].addCovering(c);
				return true;
			}
			return base.AddElement(s);
		}
	}
	public partial interface IfcSpaceBoundarySelect : IfcInterface //IFC4 SELECT (	IfcSpace, IfcExternalSpatialElement);
	{
		List<IfcRelSpaceBoundary> BoundedBy { get; }
	}
	public class IfcSpaceHeater : IfcFlowTerminal //IFC4
	{
		internal IfcSpaceHeaterTypeEnum mPredefinedType = IfcSpaceHeaterTypeEnum.NOTDEFINED;// OPTIONAL : IfcSpaceHeaterTypeEnum;
		public IfcSpaceHeaterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcSpaceHeater(IfcSpaceHeater h) : base(h) { mPredefinedType = h.mPredefinedType; }
		internal IfcSpaceHeater() : base() { }
		internal static void parseFields(IfcSpaceHeater s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcSpaceHeaterTypeEnum)Enum.Parse(typeof(IfcSpaceHeaterTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcSpaceHeater Parse(string strDef) { IfcSpaceHeater s = new IfcSpaceHeater(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : ",." + mPredefinedType.ToString() + "."); }
	}
	public class IfcSpaceHeaterType : IfcFlowTerminalType
	{
		internal IfcSpaceHeaterTypeEnum mPredefinedType = IfcSpaceHeaterTypeEnum.NOTDEFINED;// : IfcSpaceHeaterExchangerEnum; 
		public IfcSpaceHeaterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcSpaceHeaterType() : base() { }
		internal IfcSpaceHeaterType(IfcSpaceHeaterType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcSpaceHeaterType(DatabaseIfc m, string name, IfcSpaceHeaterTypeEnum t) : base(m) { Name = name; PredefinedType = t; }
		internal static void parseFields(IfcSpaceHeaterType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSpaceHeaterTypeEnum)Enum.Parse(typeof(IfcSpaceHeaterTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSpaceHeaterType Parse(string strDef) { IfcSpaceHeaterType t = new IfcSpaceHeaterType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcSpaceHeaterTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public class IfcSpaceProgram : IfcControl // DEPRECEATED IFC4
	{
		internal string mSpaceProgramIdentifier;// : IfcIdentifier;
		internal double mMaxRequiredArea, mMinRequiredArea;// : OPTIONAL IfcAreaMeasure;
		internal int mRequestedLocation;// : OPTIONAL IfcSpatialStructureElement;
		internal double mStandardRequiredArea;// : IfcAreaMeasure; 
		internal IfcSpaceProgram() : base() { }
		internal IfcSpaceProgram(IfcSpaceProgram i)
			: base(i)
		{
			mSpaceProgramIdentifier = i.mSpaceProgramIdentifier;
			mMaxRequiredArea = i.mMaxRequiredArea;
			mMinRequiredArea = i.mMinRequiredArea;
			mRequestedLocation = i.mRequestedLocation;
			mStandardRequiredArea = i.mStandardRequiredArea;
		}
		internal static IfcSpaceProgram Parse(string strDef, Schema schema) { IfcSpaceProgram p = new IfcSpaceProgram(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcSpaceProgram p, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcControl.parseFields(p, arrFields, ref ipos,schema);
			p.mSpaceProgramIdentifier = arrFields[ipos++];
			p.mMaxRequiredArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mMinRequiredArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mRequestedLocation = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mStandardRequiredArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + mSpaceProgramIdentifier + "," + ParserSTEP.DoubleOptionalToString(mMaxRequiredArea) + "," + ParserSTEP.DoubleOptionalToString(mMinRequiredArea) + "," + ParserSTEP.LinkToString(mRequestedLocation) + "," + ParserSTEP.DoubleToString(mStandardRequiredArea); }
	}
	//ENTITY IfcSpaceThermalLoadProperties // DEPRECEATED IFC4
	public class IfcSpaceType : IfcSpatialStructureElementType
	{
		internal IfcSpaceTypeEnum mPredefinedType = IfcSpaceTypeEnum.NOTDEFINED;
		public IfcSpaceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcSpaceType() : base() { }
		internal IfcSpaceType(IfcSpaceType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcSpaceType(DatabaseIfc m, string name, IfcSpaceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcSpaceType t, List<string> arrFields, ref int ipos) { IfcSpatialStructureElementType.parseFields(t, arrFields, ref ipos); try { t.mPredefinedType = (IfcSpaceTypeEnum)Enum.Parse(typeof(IfcSpaceTypeEnum), arrFields[ipos++].Replace(".", "")); } catch (Exception) { } }
		internal new static IfcSpaceType Parse(string strDef) { IfcSpaceType t = new IfcSpaceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract partial class IfcSpatialElement : IfcProduct //ABSTRACT SUPERTYPE OF (ONEOF (IfcExternalSpatialStructureElement ,IfcSpatialStructureElement ,IfcSpatialZone))
	{
		private string mLongName = "$";// : OPTIONAL IfcLabel; 
		//INVERSE
		internal List<IfcRelContainedInSpatialStructure> mContainsElements = new List<IfcRelContainedInSpatialStructure>();// : SET [0:?] OF IfcRelReferencedInSpatialStructure FOR RelatingStructure;
		internal List<IfcRelServicesBuildings> mServicedBySystems = new List<IfcRelServicesBuildings>();// : SET [0:?] OF IfcRelServicesBuildings FOR RelatedBuildings;	

		public string LongName { get { return (mLongName == "$" ? "" : ParserIfc.Decode(mLongName)); } set { mLongName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public List<IfcRelContainedInSpatialStructure> ContainsElements { get { return mContainsElements; } }
		public List<IfcRelServicesBuildings> ServicedBySystems { get { return mServicedBySystems; } }

		protected IfcSpatialElement() : base() { }
		protected IfcSpatialElement(IfcSpatialElement e) : base(e) { mLongName = e.mLongName; }
		protected IfcSpatialElement(IfcSpatialElement host, string name) : this(host.mDatabase)
		{ 
			Name = name;
			IfcBuilding building = this as IfcBuilding;
			if(building != null)
				host.addBuilding(building);
			else
			{
				building = host as IfcBuilding;
				IfcBuildingStorey bs = this as IfcBuildingStorey;
				if (building != null && bs != null)
					building.addStorey(bs);
				else
				{
					IfcSpace space = this as IfcSpace;
					if (space != null)
						host.addSpace(space);
					else
						host.AddAggregated(this);
				}
			}
		}
		protected IfcSpatialElement(DatabaseIfc m) : base(m)
		{
			IfcRelContainedInSpatialStructure rs = new IfcRelContainedInSpatialStructure(this);
			if (mDatabase.mSchema != Schema.IFC2x3)
			{
				IfcObjectPlacement pl = Placement;
				mContainerCommonPlacement = (pl == null ? new IfcLocalPlacement(new IfcAxis2Placement3D(mDatabase)) :new IfcLocalPlacement(pl,new IfcAxis2Placement3D(mDatabase)) );
				mContainerCommonPlacement.mContainerHost = this;
			}
		}

		protected static void parseFields(IfcSpatialElement s, List<string> arrFields, ref int ipos) { IfcProduct.parseFields(s, arrFields, ref ipos); s.mLongName = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + "," + (mLongName == "$" ? "$" : "'" + mLongName + "'"); }

		public override bool AddElement(IfcElement s)
		{
			if (mContainsElements.Count == 0)
				mContainsElements.Add(new IfcRelContainedInSpatialStructure(this));
			if (mContainsElements[0].mRelatedElements.Contains(s.mIndex))
				return false;
			if (s.mContainedInStructure != null)
				s.mContainedInStructure.removeObject(s);
			s.mContainedInStructure = mContainsElements[0];
			mContainsElements[0].mRelatedElements.Add(s.mIndex);
			return true;
		}

		internal bool addBuilding(IfcBuilding s)
		{
			for (int icounter = 0; icounter < mIsDecomposedBy.Count; icounter++)
			{
				IfcRelAggregates rd = mIsDecomposedBy[icounter];
				if (rd.Description.EndsWith("Buildings", true, System.Globalization.CultureInfo.CurrentCulture))
				{
					rd.addObject(s);
					return true;
				}
				if (rd.mRelatedObjects.Count > 0)
				{
					IfcBuilding b = mDatabase.mIfcObjects[rd.mRelatedObjects[0]] as IfcBuilding;
					if (b != null)
					{
						rd.addObject(s);
						return true;
					}
				}
			}
			IfcRelAggregates ra = new IfcRelAggregates(mDatabase, KeyWord, "Building", this, s);
			return true;
		}
		internal bool addSpace(IfcSpace s)
		{
			for (int icounter = 0; icounter < mIsDecomposedBy.Count; icounter++)
			{
				IfcRelAggregates rd = mIsDecomposedBy[icounter];
				if (rd.Description.EndsWith("Spaces", true, System.Globalization.CultureInfo.CurrentCulture))
					return rd.addObject(s);
			}
			IfcRelAggregates ra = new IfcRelAggregates(mDatabase, KeyWord, "Space", this, s);
			return true;
		}
		protected bool addStorey(IfcBuildingStorey s)
		{
			for (int icounter = 0; icounter < mIsDecomposedBy.Count; icounter++)
			{
				IfcRelAggregates rd = mIsDecomposedBy[icounter];
				if (rd.Description.EndsWith("Stories", true, System.Globalization.CultureInfo.CurrentCulture))
					return rd.addObject(s);
			}
			IfcRelAggregates ra = new IfcRelAggregates(mDatabase, "Building", "Storie", this, s);
			return true;
		}
		internal List<IfcBuilding> getBuildings()
		{
			List<IfcBuilding> result = new List<IfcBuilding>();
			for (int icounter = 0; icounter < mIsDecomposedBy.Count; icounter++)
			{
				List<IfcObjectDefinition> ods = mIsDecomposedBy[icounter].RelatedObjects;
				foreach (IfcObjectDefinition od in ods)
				{
					IfcBuilding b = od as IfcBuilding;
					if (b != null)
						result.Add(b);
				}
			}
			return result;
		}
	}
	public abstract class IfcSpatialElementType : IfcTypeProduct //IFC4 ABSTRACT SUPERTYPE OF(IfcSpaceType)
	{
		internal string mElementType = "$";// : OPTIONAL IfcLabel
		public string ElementType { get { return (mElementType == "$" ? "" : ParserIfc.Decode(mElementType)); } set { mElementType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		protected IfcSpatialElementType() : base() { }
		protected IfcSpatialElementType(IfcSpatialElementType t) : base(t) { mElementType = t.mElementType; }
		protected IfcSpatialElementType(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcSpatialElementType t, List<string> arrFields, ref int ipos) { IfcTypeProduct.parseFields(t, arrFields, ref ipos); t.mElementType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + (mElementType == "$" ? ",$" : ",'" + mElementType + "'"); }
	}
	public abstract class IfcSpatialStructureElement : IfcSpatialElement /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBuilding ,IfcBuildingStorey ,IfcSite ,IfcSpace))*/
	{
		internal IfcElementCompositionEnum mCompositionType = IfcElementCompositionEnum.NA;// : IfcElementCompositionEnum;  IFC4 Optional 
		public IfcElementCompositionEnum CompositionType { get { return mCompositionType; } set { mCompositionType = value; } }

		protected IfcSpatialStructureElement() : base() { }
		protected IfcSpatialStructureElement(IfcSpatialStructureElement p) : base(p) { mCompositionType = p.mCompositionType; }
		protected IfcSpatialStructureElement(DatabaseIfc m) : base(m) { if (m.mSchema == Schema.IFC2x3) mCompositionType = IfcElementCompositionEnum.ELEMENT; }
		protected IfcSpatialStructureElement(IfcSpatialStructureElement host,string name) : base(host,name) { if (mDatabase.mSchema == Schema.IFC2x3) mCompositionType = IfcElementCompositionEnum.ELEMENT; }
		protected static void parseFields(IfcSpatialStructureElement s, List<string> arrFields, ref int ipos)
		{
			IfcSpatialElement.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str != "$")
				s.mCompositionType = (IfcElementCompositionEnum)Enum.Parse(typeof(IfcElementCompositionEnum), str.Replace(".", ""));
		}
		protected override string BuildString() { return base.BuildString() + (mCompositionType == IfcElementCompositionEnum.NA ? (mDatabase.mSchema == Schema.IFC2x3 ? ",." + IfcElementCompositionEnum.ELEMENT.ToString() + "." : ",$") : ",." + mCompositionType.ToString() + "."); }
	}
	public abstract class IfcSpatialStructureElementType : IfcSpatialElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcSpaceType))
	{
		protected IfcSpatialStructureElementType() : base() { }
		protected IfcSpatialStructureElementType(IfcSpatialStructureElementType t) : base(t) { }
		protected IfcSpatialStructureElementType(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcSpatialStructureElementType t, List<string> arrFields, ref int ipos) { IfcSpatialElementType.parseFields(t, arrFields, ref ipos); }
	}
	public class IfcSpatialZone : IfcSpatialElement  //IFC4
	{
		internal IfcSpatialZoneTypeEnum mPredefinedType = IfcSpatialZoneTypeEnum.NOTDEFINED;
		public IfcSpatialZoneTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		protected IfcSpatialZone() : base() { }
		protected IfcSpatialZone(IfcSpatialZone p) : base(p) { mPredefinedType = p.mPredefinedType; }
		internal IfcSpatialZone(IfcSpatialStructureElement host, string name) : base(host, name) { if (mDatabase.mSchema == Schema.IFC2x3) throw new Exception("IFCSpatial Zone only valid in IFC4 or newer!"); }

		protected static void parseFields(IfcSpatialZone s, List<string> arrFields, ref int ipos) { IfcProduct.parseFields(s, arrFields, ref ipos); if (arrFields[ipos++][0] == '.') s.mPredefinedType = (IfcSpatialZoneTypeEnum)Enum.Parse(typeof(IfcSpatialZoneTypeEnum), arrFields[ipos++].Replace(".", "")); }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
		internal static IfcSpatialZone Parse(string strDef) { IfcSpatialZone t = new IfcSpatialZone(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public class IfcSpatialZoneType : IfcSpatialElementType  //IFC4
	{
		internal IfcSpatialZoneTypeEnum mPredefinedType = IfcSpatialZoneTypeEnum.NOTDEFINED;
		public IfcSpatialZoneTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSpatialZoneType() : base() { }
		internal IfcSpatialZoneType(IfcSpatialZoneType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcSpatialZoneType(DatabaseIfc m, string name) : base(m) { Name = name; if (mDatabase.mSchema == Schema.IFC2x3) throw new Exception("IFCSpatial Zone Type only valid in IFC4 or newer!"); }
		internal static void parseFields(IfcSpatialZoneType t, List<string> arrFields, ref int ipos) { IfcSpatialElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSpatialZoneTypeEnum)Enum.Parse(typeof(IfcSpatialZoneTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSpatialZoneType Parse(string strDef) { IfcSpatialZoneType t = new IfcSpatialZoneType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public partial class IfcSphere : IfcCsgPrimitive3D
	{
		internal double mRadius;// : IfcPositiveLengthMeasure; 
		internal IfcSphere() : base() { }
		internal IfcSphere(IfcSphere pl) : base(pl) { mRadius = pl.mRadius; }

		internal static void parseFields(IfcSphere s, List<string> arrFields, ref int ipos) { IfcCsgPrimitive3D.parseFields(s, arrFields, ref ipos); s.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcSphere Parse(string strDef) { IfcSphere s = new IfcSphere(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mRadius); }
	}
	public class IfcStackTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcStackTerminalTypeEnum mPredefinedType = IfcStackTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcStackTerminalTypeEnum;
		public IfcStackTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcStackTerminal() : base() { }
		internal IfcStackTerminal(IfcStackTerminal t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcStackTerminal(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcStackTerminal s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcStackTerminalTypeEnum)Enum.Parse(typeof(IfcStackTerminalTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcStackTerminal Parse(string strDef) { IfcStackTerminal s = new IfcStackTerminal(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcStackTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public class IfcStackTerminalType : IfcFlowTerminalType
	{
		internal IfcStackTerminalTypeEnum mPredefinedType = IfcStackTerminalTypeEnum.NOTDEFINED;
		public IfcStackTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcStackTerminalType() : base() { }
		internal IfcStackTerminalType(IfcStackTerminalType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcStackTerminalType(DatabaseIfc m, string name, IfcStackTerminalTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcStackTerminalType t, List<string> arrFields, ref int ipos) { IfcFlowTerminalType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcStackTerminalTypeEnum)Enum.Parse(typeof(IfcStackTerminalTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcStackTerminalType Parse(string strDef) { IfcStackTerminalType t = new IfcStackTerminalType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcStair : IfcBuildingElement
	{
		internal IfcStairTypeEnum mPredefinedType = IfcStairTypeEnum.NOTDEFINED;// OPTIONAL : IfcStairTypeEnum
		public IfcStairTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStair() : base() { }
		internal IfcStair(IfcStair s) : base(s) { mPredefinedType = s.mPredefinedType; }
		public IfcStair(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static IfcStair Parse(string strDef) { IfcStair s = new IfcStair(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcStair s, List<string> arrFields, ref int ipos)
		{
			IfcBuildingElement.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcStairTypeEnum)Enum.Parse(typeof(IfcStairTypeEnum), str.Substring(1, str.Length - 2));
		}
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcStairFlight : IfcBuildingElement
	{
		internal int mNumberOfRiser = 0;//	:	OPTIONAL INTEGER;
		internal int mNumberOfTreads = 0;//	:	OPTIONAL INTEGER;
		internal double mRiserHeight = 0;//	:	OPTIONAL IfcPositiveLengthMeasure;
		internal double mTreadLength = 0;//	:	OPTIONAL IfcPositiveLengthMeasure;

		internal IfcStairFlightTypeEnum mPredefinedType = IfcStairFlightTypeEnum.NOTDEFINED;//: OPTIONAL IfcStairFlightTypeEnum; IFC4
		public IfcStairFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStairFlight() : base() { }
		internal IfcStairFlight(IfcStairFlight f) : base(f) { mPredefinedType = f.mPredefinedType; }
		public IfcStairFlight(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcStairFlight Parse(string strDef, Schema schema) { IfcStairFlight f = new IfcStairFlight(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return f; }
		internal static void parseFields(IfcStairFlight f, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcBuildingElement.parseFields(f, arrFields, ref ipos);
			f.mNumberOfRiser = ParserSTEP.ParseInt(arrFields[ipos++]);
			f.mNumberOfTreads = ParserSTEP.ParseInt(arrFields[ipos++]);
			f.mRiserHeight = ParserSTEP.ParseDouble(arrFields[ipos++]);
			f.mTreadLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					f.mPredefinedType = (IfcStairFlightTypeEnum)Enum.Parse(typeof(IfcStairFlightTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString()
		{
			string result = base.BuildString() + "," + ParserSTEP.IntOptionalToString(mNumberOfRiser) + "," + ParserSTEP.IntOptionalToString(mNumberOfTreads) + "," +
				ParserSTEP.DoubleOptionalToString(mRiserHeight) + "," + ParserSTEP.DoubleOptionalToString(mTreadLength);
			return result + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcStairFlightTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcStairFlightType : IfcBuildingElementType
	{
		internal IfcStairFlightTypeEnum mPredefinedType = IfcStairFlightTypeEnum.NOTDEFINED;
		public IfcStairFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcStairFlightType() : base() { }
		internal IfcStairFlightType(IfcStairFlightType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcStairFlightType(DatabaseIfc m, string name, IfcStairFlightTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcStairFlightType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcStairFlightTypeEnum)Enum.Parse(typeof(IfcStairFlightTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcStairFlightType Parse(string strDef) { IfcStairFlightType t = new IfcStairFlightType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcStairType : IfcBuildingElementType
	{
		internal IfcStairTypeEnum mPredefinedType = IfcStairTypeEnum.NOTDEFINED;
		public IfcStairTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcStairType() : base() { }
		internal IfcStairType(IfcStairType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcStairType(DatabaseIfc m, string name, IfcStairTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcStairType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcStairTypeEnum)Enum.Parse(typeof(IfcStairTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcStairType Parse(string strDef) { IfcStairType t = new IfcStairType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString() + ",." + mPredefinedType.ToString() + "."); }
	}
	public abstract partial class IfcStructuralAction : IfcStructuralActivity // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveAction, IfcStructuralPointAction, IfcStructuralSurfaceAction))
	{
		internal bool mDestabilizingLoad = true, mDestabSet = false;//: OPTIONAL BOOLEAN; IFC4 made optional
		internal int mCausedBy;// : OPTIONAL IfcStructuralReaction; DELETED IFC4 

		public bool DestabilizingLoad { get { return mDestabilizingLoad; } set { mDestabilizingLoad = value; mDestabSet = true; } }

		protected IfcStructuralAction() : base() { }
		protected IfcStructuralAction(IfcStructuralAction a) : base(a) { mDestabilizingLoad = a.mDestabilizingLoad; mCausedBy = a.mCausedBy; }
		protected IfcStructuralAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, bool global)
			: base(lc.mDatabase, item, load, global, lc.IsGroupedBy[0]) { }
		protected static void parseFields(IfcStructuralAction a, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcStructuralActivity.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
			{
				a.mDestabilizingLoad = ParserSTEP.ParseBool(s);
				a.mDestabSet = true;
			}
			if (schema == Schema.IFC2x3)
				a.mCausedBy = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + (mDestabSet || mDatabase.mSchema == Schema.IFC2x3 ? "," + ParserSTEP.BoolToString(mDestabilizingLoad) : ",$") + (mDatabase.mSchema == Schema.IFC2x3 ? "," + ParserSTEP.LinkToString(mCausedBy) : ""); }
	}
	public abstract partial class IfcStructuralActivity : IfcProduct
	{
		private int mAppliedLoad;// : IfcStructuralLoad;
		private IfcGlobalOrLocalEnum mGlobalOrLocal = IfcGlobalOrLocalEnum.GLOBAL_COORDS;// : IfcGlobalOrLocalEnum; 
		//INVERSE 
		private IfcRelConnectsStructuralActivity mAssignedToStructuralItem = null; // : SET [0:1] OF IfcRelConnectsStructuralActivity FOR RelatedStructuralActivity; 

		internal IfcStructuralLoad AppliedLoad { get { return mDatabase.mIfcObjects[mAppliedLoad] as IfcStructuralLoad; } }
		internal IfcGlobalOrLocalEnum GlobalOrLocal { get { return mGlobalOrLocal; } }
		internal IfcRelConnectsStructuralActivity AssignedToStructuralItem { get { return mAssignedToStructuralItem; } set { mAssignedToStructuralItem = value; } }

		protected IfcStructuralActivity() : base() { }
		protected IfcStructuralActivity(IfcStructuralActivity p) : base(p) { mAppliedLoad = p.mAppliedLoad; mGlobalOrLocal = p.mGlobalOrLocal; }
		protected IfcStructuralActivity(DatabaseIfc db, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, bool global, IfcRelAssignsToGroup loadcase)
			: base(db)
		{
			mAssignedToStructuralItem = new IfcRelConnectsStructuralActivity(item, this);
			mAppliedLoad = load.mIndex;
			mGlobalOrLocal = global ? IfcGlobalOrLocalEnum.GLOBAL_COORDS : IfcGlobalOrLocalEnum.LOCAL_COORDS;
			loadcase.assign(this);
		}
		protected static void parseFields(IfcStructuralActivity a, List<string> arrFields, ref int ipos)
		{
			IfcProduct.parseFields(a, arrFields, ref ipos);
			a.mAppliedLoad = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mGlobalOrLocal = (IfcGlobalOrLocalEnum)Enum.Parse(typeof(IfcGlobalOrLocalEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mAppliedLoad) + ",." + mGlobalOrLocal.ToString() + "."; }
	}
	public interface IfcStructuralActivityAssignmentSelect : IfcInterface { } //SELECT(IfcStructuralItem,IfcElement);
	public partial class IfcStructuralAnalysisModel : IfcSystem
	{
		internal IfcAnalysisModelTypeEnum mPredefinedType;// : IfcAnalysisModelTypeEnum;
		internal int mOrientationOf2DPlane;// : OPTIONAL IfcAxis2Placement3D;
		internal List<int> mLoadedBy = new List<int>();//  : OPTIONAL SET [1:?] OF IfcStructuralLoadGroup;
		internal List<int> mHasResults = new List<int>();//: OPTIONAL SET [1:?] OF IfcStructuralResultGroup  

		internal List<IfcStructuralLoadGroup> LoadedBy { get { return mLoadedBy.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcStructuralLoadGroup); } }

		internal IfcStructuralAnalysisModel() : base() { }
		internal IfcStructuralAnalysisModel(IfcStructuralAnalysisModel i) : base(i) { mPredefinedType = i.mPredefinedType; mOrientationOf2DPlane = i.mOrientationOf2DPlane; mLoadedBy = new List<int>(i.mLoadedBy.ToArray()); mHasResults = new List<int>(i.mHasResults.ToArray()); }
		internal IfcStructuralAnalysisModel(IfcSpatialElement bldg, string name, IfcAnalysisModelTypeEnum type) : base(bldg, name) { mPredefinedType = type; }
		internal new static IfcStructuralAnalysisModel Parse(string strDef) { IfcStructuralAnalysisModel m = new IfcStructuralAnalysisModel(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcStructuralAnalysisModel c, List<string> arrFields, ref int ipos)
		{
			IfcSystem.parseFields(c, arrFields, ref ipos);
			c.mPredefinedType = (IfcAnalysisModelTypeEnum)Enum.Parse(typeof(IfcAnalysisModelTypeEnum), arrFields[ipos++].Replace(".", ""));
			c.mOrientationOf2DPlane = ParserSTEP.ParseLink(arrFields[ipos++]);
			string str = arrFields[ipos++];
			if (str != "$")
				c.mLoadedBy = ParserSTEP.SplitListLinks(str);
			str = arrFields[ipos++];
			if (str != "$")
				c.mHasResults = ParserSTEP.SplitListLinks(str);

		}
		protected override string BuildString()
		{
			string str = base.BuildString() + ",." + mPredefinedType.ToString() + ".," + ParserSTEP.LinkToString(mOrientationOf2DPlane) + ",";
			if (mLoadedBy.Count == 0)
				str += "$,";
			else
			{
				str += "(" + ParserSTEP.LinkToString(mLoadedBy[0]);
				for (int icounter = 1; icounter < mLoadedBy.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mLoadedBy[icounter]);
				str += "),";
			}
			if (mHasResults.Count == 0)
				str += "$";
			else
			{
				str += "(" + ParserSTEP.LinkToString(mHasResults[0]);
				for (int icounter = 1; icounter < mHasResults.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mHasResults[icounter]);
				str += ")";
			}
			return str;
		}

		internal void addLoadGroup(IfcStructuralLoadGroup lg) { mLoadedBy.Add(lg.mIndex); }
	}
	public abstract partial class IfcStructuralConnection : IfcStructuralItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveConnection ,IfcStructuralPointConnection ,IfcStructuralSurfaceConnection))
	{
		internal int mAppliedCondition = 0; //: OPTIONAL IfcBoundaryCondition
		//INVERSE
		internal List<IfcRelConnectsStructuralMember> mConnectsStructuralMembers = new List<IfcRelConnectsStructuralMember>();//	 :	SET [1:?] OF IfcRelConnectsStructuralMember FOR RelatedStructuralConnection;

		public IfcBoundaryCondition AppliedCondition { get { return mDatabase.mIfcObjects[mAppliedCondition] as IfcBoundaryCondition; } set { mAppliedCondition = (value == null ? 0 : value.mIndex); } }

		protected IfcStructuralConnection() : base() { }
		protected IfcStructuralConnection(IfcStructuralConnection i) : base(i) { mAppliedCondition = i.mAppliedCondition; }
		internal IfcStructuralConnection(IfcStructuralAnalysisModel sm, int id) : base(sm, id) {  }
		protected static void parseFields(IfcStructuralConnection c, List<string> arrFields, ref int ipos) { IfcStructuralItem.parseFields(c, arrFields, ref ipos); c.mAppliedCondition = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mAppliedCondition); }
	}
	public abstract class IfcStructuralConnectionCondition : BaseClassIfc //ABSTRACT SUPERTYPE OF (ONEOF (IfcFailureConnectionCondition ,IfcSlippageConnectionCondition));
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcStructuralConnectionCondition() : base() { }
		protected IfcStructuralConnectionCondition(IfcStructuralConnectionCondition i) : base() { mName = i.mName; }
		protected static void parseFields(IfcStructuralConnectionCondition c, List<string> arrFields, ref int ipos) { c.mName = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + (mName == "$" ? ",$" : ",'" + mName + "'"); }
	}
	public partial class IfcStructuralCurveAction : IfcStructuralAction //SUPERTYPE OF(IfcStructuralLinearAction)
	{
		internal IfcProjectedOrTrueLengthEnum mProjectedOrTrue = IfcProjectedOrTrueLengthEnum.TRUE_LENGTH;// : IfcProjectedOrTrueLengthEnum
		internal IfcStructuralCurveActivityTypeEnum mPredefinedType = IfcStructuralCurveActivityTypeEnum.NOTDEFINED;//IfcStructuralCurveActivityTypeEnum
		internal IfcStructuralCurveAction() : base() { }
		internal IfcStructuralCurveAction(IfcStructuralCurveAction p) : base(p) { mProjectedOrTrue = p.mProjectedOrTrue; mPredefinedType = p.mPredefinedType; }
		internal IfcStructuralCurveAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, bool global, bool projected, IfcStructuralCurveActivityTypeEnum type)
			: base(lc, item, load, global) { mProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH; mPredefinedType = type; }
		internal static IfcStructuralCurveAction Parse(string strDef,Schema schema) { IfcStructuralCurveAction a = new IfcStructuralCurveAction(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcStructuralCurveAction a, List<string> arrFields, ref int ipos,Schema schema)
		{
			IfcStructuralAction.parseFields(a, arrFields, ref ipos,schema);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mProjectedOrTrue = (IfcProjectedOrTrueLengthEnum)Enum.Parse(typeof(IfcProjectedOrTrueLengthEnum), s.Replace(".", ""));
			if (schema != Schema.IFC2x3)
				a.mPredefinedType = (IfcStructuralCurveActivityTypeEnum)Enum.Parse(typeof(IfcStructuralCurveActivityTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildString()
		{
			return base.BuildString() + ",." + mProjectedOrTrue.ToString() + (mDatabase.mSchema == Schema.IFC2x3 ? "." : ".,." + mPredefinedType.ToString() + ".");
		}
	}
	public class IfcStructuralCurveConnection : IfcStructuralConnection
	{
		internal IfcStructuralCurveConnection() : base() { }
		internal IfcStructuralCurveConnection(IfcStructuralCurveConnection i) : base(i) { }
		internal static IfcStructuralCurveConnection Parse(string strDef) { IfcStructuralCurveConnection c = new IfcStructuralCurveConnection(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcStructuralCurveConnection c, List<string> arrFields, ref int ipos) { IfcStructuralConnection.parseFields(c, arrFields, ref ipos); }
	}
	public partial class IfcStructuralCurveMember : IfcStructuralMember
	{
		internal IfcStructuralCurveTypeEnum mPredefinedType;// : IfcStructuralCurveTypeEnum; 
		internal int mAxis; //: IfcDirection

		public IfcStructuralCurveTypeEnum PredefinedType { get { return mPredefinedType; } }
		public IfcDirection Axis { get { return mDatabase.mIfcObjects[mAxis] as IfcDirection; } }

		public IfcStructuralCurveMember() : base() { }
		internal IfcStructuralCurveMember(IfcStructuralCurveMember p) : base(p) { mPredefinedType = p.mPredefinedType; }

		internal static IfcStructuralCurveMember Parse(string strDef, Schema schema) { IfcStructuralCurveMember m = new IfcStructuralCurveMember(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcStructuralCurveMember m, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcStructuralMember.parseFields(m, arrFields, ref ipos);
			string s = arrFields[ipos++];
			m.mPredefinedType = (IfcStructuralCurveTypeEnum)Enum.Parse(typeof(IfcStructuralCurveTypeEnum), s.Substring(1, s.Length - 2));
			if (schema != Schema.IFC2x3)
				m.mAxis = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + (mDatabase.mSchema == Schema.IFC2x3 ? "." : ".," + ParserSTEP.LinkToString(mAxis)); }
	}
	public class IfcStructuralCurveMemberVarying : IfcStructuralCurveMember
	{
		internal IfcStructuralCurveMemberVarying() : base() { }
		internal IfcStructuralCurveMemberVarying(IfcStructuralCurveMemberVarying p) : base(p) { }
		internal new static IfcStructuralCurveMemberVarying Parse(string strDef,Schema schema) { IfcStructuralCurveMemberVarying m = new IfcStructuralCurveMemberVarying(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcStructuralCurveMemberVarying m, List<string> arrFields, ref int ipos,Schema schema) { IfcStructuralCurveMember.parseFields(m, arrFields, ref ipos,schema); }
	}
	public class IfcStructuralCurveReaction : IfcStructuralReaction
	{
		internal IfcStructuralCurveActivityTypeEnum mPredefinedType = IfcStructuralCurveActivityTypeEnum.NOTDEFINED;//: 	IfcStructuralCurveActivityTypeEnum; 
		internal IfcStructuralCurveReaction() : base() { }
		internal IfcStructuralCurveReaction(IfcStructuralCurveReaction p) : base(p) { mPredefinedType = p.mPredefinedType; }
		internal static IfcStructuralCurveReaction Parse(string strDef) { IfcStructuralCurveReaction r = new IfcStructuralCurveReaction(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
		internal static void parseFields(IfcStructuralCurveReaction r, List<string> arrFields, ref int ipos) { IfcStructuralReaction.parseFields(r, arrFields, ref ipos); r.mPredefinedType = (IfcStructuralCurveActivityTypeEnum)Enum.Parse(typeof(IfcStructuralCurveActivityTypeEnum), arrFields[ipos++].Replace(".", "")); }
	}
	public abstract partial class IfcStructuralItem : IfcProduct, IfcStructuralActivityAssignmentSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralConnection ,IfcStructuralMember))
	{
		internal int mID = 0;
		//INVERSE
		internal List<IfcRelConnectsStructuralActivity> mAssignedStructuralActivity = new List<IfcRelConnectsStructuralActivity>();//: 	SET OF IfcRelConnectsStructuralActivity FOR RelatingElement;
		protected IfcStructuralItem() : base() { }
		protected IfcStructuralItem(IfcStructuralItem p) : base(p) { }
		protected IfcStructuralItem(IfcStructuralAnalysisModel sm, int id) : base(sm.mDatabase)
		{
			sm.mIsGroupedBy[0].assign(this);
			if (string.IsNullOrEmpty(Name))
				Name = id.ToString();
			else if (!string.IsNullOrEmpty(Description))
				Description = id.ToString();
			mID = id;
			mDatabase.mContext.setStructuralUnits();
		}
		protected static void parseFields(IfcStructuralItem i, List<string> arrFields, ref int ipos) { IfcProduct.parseFields(i, arrFields, ref ipos); }
	}
	public partial class IfcStructuralLinearAction : IfcStructuralCurveAction
	{
		internal IfcStructuralLinearAction() : base() { }
		internal IfcStructuralLinearAction(IfcStructuralLinearAction p) : base(p) { mProjectedOrTrue = p.mProjectedOrTrue; }
		internal IfcStructuralLinearAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoadLinearForce load, bool global, bool projected) : base(lc, item, load, global, projected, IfcStructuralCurveActivityTypeEnum.CONST) { }
		internal IfcStructuralLinearAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoadTemperature load, bool global, bool projected) : base(lc, item, load, global, projected, IfcStructuralCurveActivityTypeEnum.CONST) { }
		internal new static IfcStructuralLinearAction Parse(string strDef,Schema schema) { IfcStructuralLinearAction a = new IfcStructuralLinearAction(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcStructuralLinearAction a, List<string> arrFields, ref int ipos,Schema schema) { IfcStructuralCurveAction.parseFields(a, arrFields, ref ipos,schema); }
	}
	public class IfcStructuralLinearActionVarying : IfcStructuralLinearAction
	{
		internal int mVaryingAppliedLoadLocation;// : IfcShapeAspect;
		internal List<int> mSubsequentAppliedLoads = new List<int>();//: LIST [1:?] OF IfcStructuralLoad; 
		internal IfcStructuralLinearActionVarying() : base() { }
		internal IfcStructuralLinearActionVarying(IfcStructuralLinearActionVarying p) : base(p) { mVaryingAppliedLoadLocation = p.mVaryingAppliedLoadLocation; mSubsequentAppliedLoads = new List<int>(p.mSubsequentAppliedLoads.ToArray()); }
		internal new static IfcStructuralLinearActionVarying Parse(string strDef,Schema schema) { IfcStructuralLinearActionVarying a = new IfcStructuralLinearActionVarying(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcStructuralLinearActionVarying a, List<string> arrFields, ref int ipos,Schema schema) { IfcStructuralLinearAction.parseFields(a, arrFields, ref ipos,schema); a.mVaryingAppliedLoadLocation = ParserSTEP.ParseLink(arrFields[ipos++]); a.mSubsequentAppliedLoads = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + ParserSTEP.LinkToString(mVaryingAppliedLoadLocation) + ",("
				+ ParserSTEP.LinkToString(mSubsequentAppliedLoads[0]);
			for (int icounter = 1; icounter < mSubsequentAppliedLoads.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSubsequentAppliedLoads[icounter]);
			return str + ")";
		}
	}
	public abstract class IfcStructuralLoad : BaseClassIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralLoadConfiguration, IfcStructuralLoadOrResult));
	{
		internal string mName = "$";// : OPTIONAL IfcLabel
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcStructuralLoad() : base() { }
		protected IfcStructuralLoad(IfcStructuralLoad i) : base() { mName = i.mName; }
		protected IfcStructuralLoad(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcStructuralLoad l, List<string> arrFields, ref int ipos) { l.mName = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + (mName == "$" ? ",$" : ",'" + mName + "'"); }
	}
	public class IfcStructuralLoadConfiguration : IfcStructuralLoad //IFC4
	{
		internal List<int> mValues = new List<int>();//	 :	LIST [1:?] OF IfcStructuralLoadOrResult;
		internal List<List<double>> mLocations = new List<List<double>>();//	 :	OPTIONAL LIST [1:?] OF UNIQUE LIST [1:2] OF IfcLengthMeasure; 

		public List<IfcStructuralLoadOrResult> Values { get { return mValues.ConvertAll(x => (mDatabase.mIfcObjects[x] as IfcStructuralLoadOrResult)); } }

		internal IfcStructuralLoadConfiguration() : base() { }
		internal IfcStructuralLoadConfiguration(IfcStructuralLoadConfiguration p) : base(p) { mValues = p.mValues; mLocations = p.mLocations; }
		internal IfcStructuralLoadConfiguration(DatabaseIfc m, IfcStructuralLoadOrResult val, double length)
			: base(m) { mValues.Add(val.mIndex); List<double> list = new List<double>(1); list.Add(length); mLocations.Add(list);  }
		internal IfcStructuralLoadConfiguration(DatabaseIfc m, List<IfcStructuralLoadOrResult> vals, List<List<double>> lengths)
			: base(m) { mValues = vals.ConvertAll(x => x.mIndex); if (lengths != null) mLocations = lengths; }
		internal static IfcStructuralLoadConfiguration Parse(string strDef) { IfcStructuralLoadConfiguration l = new IfcStructuralLoadConfiguration(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadConfiguration l, List<string> arrFields, ref int ipos)
		{
			IfcStructuralLoad.parseFields(l, arrFields, ref ipos);
			l.mValues = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			string s = arrFields[ipos++];
			if (s != "$")
			{
				List<string> fields = ParserSTEP.SplitLineFields(s);
				char[] delim = ",".ToCharArray();
				for (int icounter = 0; icounter < fields.Count; icounter++)
				{
					List<double> list = new List<double>(2);
					string[] ss = fields[icounter].Substring(1, fields[icounter].Length - 2).Split(delim);
					list.Add(ParserSTEP.ParseDouble(ss[0]));
					if (ss.Length > 1)
						list.Add(ParserSTEP.ParseDouble(ss[1]));
					l.mLocations.Add(list);
				}
			}
		}
		protected override string BuildString()
		{
			string s = ",$";
			if (mLocations.Count > 0)
			{
				s = ",(" + ParserSTEP.DoubleToString(mLocations[0][0]) + (mLocations[0].Count > 1 ? "," + ParserSTEP.DoubleToString(mLocations[0][1]) : "");
				for (int icounter = 1; icounter < mLocations.Count; icounter++)
					s += ")," + ParserSTEP.DoubleToString(mLocations[icounter][0]) + (mLocations[icounter].Count > 1 ? "," + ParserSTEP.DoubleToString(mLocations[icounter][1]) : "");
				s += "))";
			}
			return base.BuildString() + "," + ParserSTEP.ListLinksToString(mValues) + s;
		}
	}
	public abstract class IfcStructuralLoadOrResult : IfcStructuralLoad // ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralLoadStatic, IfcSurfaceReinforcementArea))
	{
		protected IfcStructuralLoadOrResult() : base() { }
		protected IfcStructuralLoadOrResult(IfcStructuralLoadOrResult p) : base(p) { }
		protected IfcStructuralLoadOrResult(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcStructuralLoadOrResult l, List<string> arrFields, ref int ipos) { IfcStructuralLoad.parseFields(l, arrFields, ref ipos); }
	}
	public partial class IfcStructuralLoadCase : IfcStructuralLoadGroup //IFC4
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 ? "IFCSTRUCTURALLOADGROUP" : base.KeyWord); } }

		internal Tuple<double,double,double> mSelfWeightCoefficients = null;// : OPTIONAL LIST [3:3] OF IfcRatioMeasure; 
		internal IfcStructuralLoadCase() : base() { }
		internal IfcStructuralLoadCase(IfcStructuralLoadCase p) : base(p) { mSelfWeightCoefficients = p.mSelfWeightCoefficients; }
		public IfcStructuralLoadCase(IfcStructuralAnalysisModel sm, string name, IfcActionTypeEnum action, IfcActionSourceTypeEnum source, double coeff, string purpose)
			: base(sm, name, IfcLoadGroupTypeEnum.LOAD_CASE, action, source, coeff, purpose) { new IfcRelAssignsToGroup(this) { Name = Name + " Actions", Description = Description }; }
		internal new static IfcStructuralLoadCase Parse(string strDef) { IfcStructuralLoadCase g = new IfcStructuralLoadCase(); int ipos = 0; parseFields(g, ParserSTEP.SplitLineFields(strDef), ref ipos); return g; }
		internal static void parseFields(IfcStructuralLoadCase g, List<string> arrFields, ref int ipos) { IfcStructuralLoadGroup.parseFields(g, arrFields, ref ipos); string s = arrFields[ipos++]; if (s.StartsWith("(")) { List<string> fields = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)); g.mSelfWeightCoefficients = new Tuple<double,double,double>(double.Parse(fields[0]), double.Parse(fields[1]), double.Parse(fields[2])); } }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mSelfWeightCoefficients != null ? ",(" + ParserSTEP.DoubleToString(mSelfWeightCoefficients.Item1) + "," + ParserSTEP.DoubleToString(mSelfWeightCoefficients.Item2) + "," + ParserSTEP.DoubleToString(mSelfWeightCoefficients.Item3) + ")" : ",$")); }
	}
	public partial class IfcStructuralLoadGroup : IfcGroup
	{
		internal IfcLoadGroupTypeEnum mPredefinedType = IfcLoadGroupTypeEnum.NOTDEFINED;// : IfcLoadGroupTypeEnum;
		internal IfcActionTypeEnum mActionType = IfcActionTypeEnum.NOTDEFINED;// : IfcActionTypeEnum;
		internal IfcActionSourceTypeEnum mActionSource = IfcActionSourceTypeEnum.NOTDEFINED;//: IfcActionSourceTypeEnum;
		internal double mCoefficient;//: OPTIONAL IfcRatioMeasure;
		internal string mPurpose = "$";// : OPTIONAL IfcLabel; 
		//INVERSE 
		//SourceOfResultGroup	 :	SET [0:1] OF IfcStructuralResultGroup FOR ResultForLoadGroup;
		internal List<IfcStructuralAnalysisModel> mLoadGroupFor = new List<IfcStructuralAnalysisModel>();//	 :	SET [0:?] OF IfcStructuralAnalysisModel 

		internal IfcStructuralLoadGroup() : base() { }
		internal IfcStructuralLoadGroup(IfcStructuralLoadGroup p) : base(p) { mPredefinedType = p.mPredefinedType; mActionType = p.mActionType; mActionSource = p.mActionSource; mCoefficient = p.mCoefficient; mPurpose = p.mPurpose; }
		internal IfcStructuralLoadGroup(IfcStructuralAnalysisModel sm, string name, IfcLoadGroupTypeEnum type, IfcActionTypeEnum action, IfcActionSourceTypeEnum source, double coeff, string purpose)
			: base(sm.mDatabase, name) { mLoadGroupFor.Add(sm); sm.addLoadGroup(this); mPredefinedType = type; mActionType = action; mActionSource = source; mCoefficient = coeff; if (!string.IsNullOrEmpty(purpose)) mPurpose = purpose; }
		internal IfcStructuralLoadGroup(IfcStructuralAnalysisModel sm, string name, List<double> factors, List<IfcStructuralLoadCase> cases, bool ULS)
			: base(sm.mDatabase, name)
		{
			mPredefinedType = IfcLoadGroupTypeEnum.LOAD_COMBINATION;
			mLoadGroupFor.Add(sm);
			sm.addLoadGroup(this);
			mPurpose = (ULS ? "ULS" : "SLS");
			if (factors != null && factors.Count > 0)
			{
				double prevfactor = factors[0];
				List<IfcObjectDefinition> ods = new List<IfcObjectDefinition>();
				for (int icounter = 0; icounter < cases.Count; icounter++)
				{
					double factor = (factors.Count > icounter ? factors[icounter] : prevfactor);
					if (Math.Abs(factor - prevfactor) > mDatabase.Tolerance)
					{
						new IfcRelAssignsToGroupByFactor(this, ods, prevfactor);
						ods = new List<IfcObjectDefinition>();
						prevfactor = factor;
					}
					ods.Add(cases[icounter]);
				}
				new IfcRelAssignsToGroupByFactor(this, ods, prevfactor);
			}
			else
			{
				new IfcRelAssignsToGroupByFactor(this, cases.ConvertAll(x => x as IfcObjectDefinition), 1);
			}
		}

		internal new static IfcStructuralLoadGroup Parse(string strDef) { IfcStructuralLoadGroup g = new IfcStructuralLoadGroup(); int ipos = 0; parseFields(g, ParserSTEP.SplitLineFields(strDef), ref ipos); return g; }
		internal static void parseFields(IfcStructuralLoadGroup g, List<string> arrFields, ref int ipos) { IfcGroup.parseFields(g, arrFields, ref ipos); g.mPredefinedType = (IfcLoadGroupTypeEnum)Enum.Parse(typeof(IfcLoadGroupTypeEnum), arrFields[ipos++].Replace(".", "")); g.mActionType = (IfcActionTypeEnum)Enum.Parse(typeof(IfcActionTypeEnum), arrFields[ipos++].Replace(".", "")); g.mActionSource = (IfcActionSourceTypeEnum)Enum.Parse(typeof(IfcActionSourceTypeEnum), arrFields[ipos++].Replace(".", "")); g.mCoefficient = ParserSTEP.ParseDouble(arrFields[ipos++]); g.mPurpose = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + ".,." + mActionType.ToString() + ".,." + mActionSource.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mCoefficient) + (mPurpose == "$" ? ",$" : ",'" + mPurpose + "'"); }
	}
	public partial class IfcStructuralLoadLinearForce : IfcStructuralLoadStatic
	{
		internal double mLinearForceX = 0, mLinearForceY = 0, mLinearForceZ = 0; // : OPTIONAL IfcLinearForceMeasure
		internal double mLinearMomentX = 0, mLinearMomentY = 0, mLinearMomentZ = 0;// : OPTIONAL IfcLinearMomentMeasure; 
		internal IfcStructuralLoadLinearForce() : base() { }
		internal IfcStructuralLoadLinearForce(IfcStructuralLoadLinearForce f) : base(f) { mLinearForceX = f.mLinearForceX; mLinearForceY = f.mLinearForceY; mLinearForceZ = f.mLinearForceZ; mLinearMomentX = f.mLinearMomentX; mLinearMomentY = f.mLinearMomentY; mLinearMomentZ = f.mLinearMomentZ; }
		internal IfcStructuralLoadLinearForce(DatabaseIfc m) : base(m) { }
		internal static IfcStructuralLoadLinearForce Parse(string strDef) { IfcStructuralLoadLinearForce l = new IfcStructuralLoadLinearForce(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadLinearForce l, List<string> arrFields, ref int ipos)
		{ IfcStructuralLoadStatic.parseFields(l, arrFields, ref ipos); l.mLinearForceX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mLinearForceY= ParserSTEP.ParseDouble(arrFields[ipos++]); l.mLinearForceZ = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mLinearMomentX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mLinearMomentY = ParserSTEP.ParseDouble(arrFields[ipos++]);  l.mLinearMomentZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mLinearForceX) + "," + ParserSTEP.DoubleOptionalToString(mLinearForceY) + "," + ParserSTEP.DoubleOptionalToString(mLinearForceZ) + "," + ParserSTEP.DoubleOptionalToString(mLinearMomentX) + "," + ParserSTEP.DoubleOptionalToString(mLinearMomentY) + "," + ParserSTEP.DoubleOptionalToString(mLinearMomentZ); }
	}
	public partial class IfcStructuralLoadPlanarForce : IfcStructuralLoadStatic
	{
		internal double mPlanarForceX = 0, mPlanarForceY = 0, mPlanarForceZ = 0;// : OPTIONAL IfcPlanarForceMeasure; 
		internal IfcStructuralLoadPlanarForce() : base() { }
		internal IfcStructuralLoadPlanarForce(IfcStructuralLoadPlanarForce f) : base(f) { mPlanarForceX = f.mPlanarForceX; mPlanarForceY = f.mPlanarForceY; mPlanarForceZ = f.mPlanarForceZ; }
		internal IfcStructuralLoadPlanarForce(DatabaseIfc m) : base(m) { }

		internal static IfcStructuralLoadPlanarForce Parse(string strDef) { IfcStructuralLoadPlanarForce l = new IfcStructuralLoadPlanarForce(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadPlanarForce l, List<string> arrFields, ref int ipos) { IfcStructuralLoadStatic.parseFields(l, arrFields, ref ipos); l.mPlanarForceX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mPlanarForceY = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mPlanarForceZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mPlanarForceX) + "," + ParserSTEP.DoubleOptionalToString(mPlanarForceY) + "," + ParserSTEP.DoubleOptionalToString(mPlanarForceZ); }
	}
	public partial class IfcStructuralLoadSingleDisplacement : IfcStructuralLoadStatic
	{
		internal double mDisplacementX = 0, mDisplacementY = 0, mDisplacementZ = 0;// : OPTIONAL IfcLengthMeasure;
		internal double mRotationalDisplacementRX = 0, mRotationalDisplacementRY = 0, mRotationalDisplacementRZ = 0;// : OPTIONAL IfcPlaneAngleMeasure; 
		internal IfcStructuralLoadSingleDisplacement() : base() { }
		internal IfcStructuralLoadSingleDisplacement(IfcStructuralLoadSingleDisplacement d) : base(d) { mDisplacementX = d.mDisplacementX; mDisplacementY = d.mDisplacementY; mDisplacementZ = d.mDisplacementZ; mRotationalDisplacementRX = d.mRotationalDisplacementRX; mRotationalDisplacementRY = d.mRotationalDisplacementRY; mRotationalDisplacementRZ = d.mRotationalDisplacementRZ; }
		public IfcStructuralLoadSingleDisplacement(DatabaseIfc m) : base(m) { }
		internal static IfcStructuralLoadSingleDisplacement Parse(string strDef) { IfcStructuralLoadSingleDisplacement l = new IfcStructuralLoadSingleDisplacement(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadSingleDisplacement l, List<string> arrFields, ref int ipos) { IfcStructuralLoadStatic.parseFields(l, arrFields, ref ipos); l.mDisplacementX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mDisplacementY = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mDisplacementZ = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mRotationalDisplacementRX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mRotationalDisplacementRY = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mRotationalDisplacementRZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mDisplacementX) + "," + ParserSTEP.DoubleOptionalToString(mDisplacementY) + "," + ParserSTEP.DoubleOptionalToString(mDisplacementZ) + "," + ParserSTEP.DoubleOptionalToString(mRotationalDisplacementRX) + "," + ParserSTEP.DoubleOptionalToString(mRotationalDisplacementRY) + "," + ParserSTEP.DoubleOptionalToString(mRotationalDisplacementRZ); }
	}
	public class IfcStructuralLoadSingleDisplacementDistortion : IfcStructuralLoadSingleDisplacement
	{
		internal double mDistortion;// : OPTIONAL IfcCurvatureMeasure;
		internal IfcStructuralLoadSingleDisplacementDistortion() : base() { }
		internal IfcStructuralLoadSingleDisplacementDistortion(IfcStructuralLoadSingleDisplacementDistortion p) : base(p) { mDistortion = p.mDistortion; }
		internal new static IfcStructuralLoadSingleDisplacementDistortion Parse(string strDef) { IfcStructuralLoadSingleDisplacementDistortion l = new IfcStructuralLoadSingleDisplacementDistortion(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadSingleDisplacementDistortion l, List<string> arrFields, ref int ipos) { IfcStructuralLoadSingleDisplacement.parseFields(l, arrFields, ref ipos); l.mDistortion = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mDistortion); }
	}
	public partial class IfcStructuralLoadSingleForce : IfcStructuralLoadStatic
	{
		internal double mForceX = 0, mForceY = 0, mForceZ = 0;// : OPTIONAL IfcForceMeasure;
		internal double mMomentX = 0, mMomentY = 0, mMomentZ = 0;// : OPTIONAL IfcTorqueMeasure; 
		internal IfcStructuralLoadSingleForce() : base() { }
		internal IfcStructuralLoadSingleForce(IfcStructuralLoadSingleForce f) : base(f) { mForceX = f.mForceX; mForceY = f.mForceY; mForceZ = f.mForceZ; mMomentX = f.mMomentX; mMomentY = f.mMomentY; mMomentZ = f.mMomentZ; }
		internal IfcStructuralLoadSingleForce(DatabaseIfc m) : base(m) { }
		internal static IfcStructuralLoadSingleForce Parse(string strDef) { IfcStructuralLoadSingleForce l = new IfcStructuralLoadSingleForce(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadSingleForce l, List<string> arrFields, ref int ipos) { IfcStructuralLoadStatic.parseFields(l, arrFields, ref ipos); l.mForceX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mForceY = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mForceZ = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mMomentX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mMomentY = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mMomentZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mForceX) + "," + ParserSTEP.DoubleOptionalToString(mForceY) + "," + ParserSTEP.DoubleOptionalToString(mForceZ) + "," + ParserSTEP.DoubleOptionalToString(mMomentX) + "," + ParserSTEP.DoubleOptionalToString(mMomentY) + "," + ParserSTEP.DoubleOptionalToString(mMomentZ); }
	}
	public class IfcStructuralLoadSingleForceWarping : IfcStructuralLoadSingleForce
	{
		internal double mWarpingMoment;// : OPTIONAL IfcWarpingMomentMeasure;
		internal IfcStructuralLoadSingleForceWarping() : base() { }
		internal IfcStructuralLoadSingleForceWarping(IfcStructuralLoadSingleForceWarping p) : base(p) { mWarpingMoment = p.mWarpingMoment; }
		internal new static IfcStructuralLoadSingleForceWarping Parse(string strDef) { IfcStructuralLoadSingleForceWarping l = new IfcStructuralLoadSingleForceWarping(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadSingleForceWarping l, List<string> arrFields, ref int ipos) { IfcStructuralLoadSingleForce.parseFields(l, arrFields, ref ipos); l.mWarpingMoment = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mWarpingMoment); }
	}
	public abstract class IfcStructuralLoadStatic : IfcStructuralLoadOrResult /*ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralLoadLinearForce ,IfcStructuralLoadPlanarForce ,IfcStructuralLoadSingleDisplacement ,IfcStructuralLoadSingleForce ,IfcStructuralLoadTemperature))*/
	{
		protected IfcStructuralLoadStatic() : base() { }
		protected IfcStructuralLoadStatic(IfcStructuralLoadStatic p) : base(p) { }
		protected IfcStructuralLoadStatic(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcStructuralLoadStatic l, List<string> arrFields, ref int ipos) { IfcStructuralLoadOrResult.parseFields(l, arrFields, ref ipos); }
	}
	public class IfcStructuralLoadTemperature : IfcStructuralLoadStatic
	{
		internal double mDeltaT_Constant, mDeltaT_Y, mDeltaT_Z;// : OPTIONAL IfcThermodynamicTemperatureMeasure; 
		internal IfcStructuralLoadTemperature() : base() { }
		internal IfcStructuralLoadTemperature(IfcStructuralLoadTemperature p) : base(p) { mDeltaT_Constant = p.mDeltaT_Constant; mDeltaT_Y = p.mDeltaT_Y; mDeltaT_Z = p.mDeltaT_Z; }
		internal IfcStructuralLoadTemperature(DatabaseIfc m, double T, double TY, double TZ) : base(m) { mDeltaT_Constant = T; mDeltaT_Y = TY; mDeltaT_Z = TZ; }
		internal static IfcStructuralLoadTemperature Parse(string strDef) { IfcStructuralLoadTemperature l = new IfcStructuralLoadTemperature(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadTemperature l, List<string> arrFields, ref int ipos) { IfcStructuralLoadStatic.parseFields(l, arrFields, ref ipos); l.mDeltaT_Constant = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mDeltaT_Y = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mDeltaT_Z = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mDeltaT_Constant) + "," + ParserSTEP.DoubleOptionalToString(mDeltaT_Y) + "," + ParserSTEP.DoubleOptionalToString(mDeltaT_Z); }
	}
	public abstract partial class IfcStructuralMember : IfcStructuralItem //ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralCurveMember, IfcStructuralSurfaceMember))
	{
		//INVERSE
		internal List<IfcRelConnectsStructuralMember> mConnectedBy = new List<IfcRelConnectsStructuralMember>();// : SET [0:?] OF IfcRelConnectsStructuralMember FOR RelatingStructuralMember 
		internal IfcRelConnectsStructuralElement mStructuralMemberForGG = null;
		protected IfcStructuralMember() : base() { }
		protected IfcStructuralMember(IfcStructuralMember i) : base(i) { }
		
		protected static void parseFields(IfcStructuralMember m, List<string> arrFields, ref int ipos) { IfcStructuralItem.parseFields(m, arrFields, ref ipos); }
	}
	public partial class IfcStructuralPlanarAction : IfcStructuralSurfaceAction // Ifc2x3 IfcStructuralAction
	{
		internal IfcStructuralPlanarAction() : base() { }
		internal IfcStructuralPlanarAction(IfcStructuralPlanarAction p) : base(p) { mProjectedOrTrue = p.mProjectedOrTrue; }
		internal IfcStructuralPlanarAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoadPlanarForce load, bool global, bool projected)
			: base(lc, item, load, global, projected, IfcStructuralSurfaceActivityTypeEnum.CONST) { if (mDatabase.mSchema == Schema.IFC2x3) throw new Exception(KeyWord + " in IFC4 Only"); }
		internal IfcStructuralPlanarAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoadTemperature load, bool global, bool projected)
			: base(lc, item, load, global, projected, IfcStructuralSurfaceActivityTypeEnum.CONST) { if (mDatabase.mSchema == Schema.IFC2x3) throw new Exception(KeyWord + " in IFC4 Only"); }

		internal new static IfcStructuralPlanarAction Parse(string strDef,Schema schema) { IfcStructuralPlanarAction a = new IfcStructuralPlanarAction(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcStructuralPlanarAction a, List<string> arrFields, ref int ipos,Schema schema) { IfcStructuralSurfaceAction.parseFields(a, arrFields, ref ipos,schema); }
		protected override string BuildString() { return base.BuildString() + ",." + mProjectedOrTrue.ToString() + "."; }
	}
	public partial class IfcStructuralPointAction : IfcStructuralAction
	{
		internal IfcStructuralPointAction() : base() { }
		internal IfcStructuralPointAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoadSingleForce l, bool global) : base(lc, item, l, global) { }
		internal IfcStructuralPointAction(IfcStructuralPointAction p) : base(p) { }
		internal IfcStructuralPointAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoadSingleDisplacement l, bool global) : base(lc, item, l, global) { }
		internal static IfcStructuralPointAction Parse(string strDef,Schema schema)
		{
			IfcStructuralPointAction a = new IfcStructuralPointAction();
			int ipos = 0;
			parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema);
			return a;
		}
		internal static void parseFields(IfcStructuralPointAction a, List<string> arrFields, ref int ipos,Schema schema) { IfcStructuralAction.parseFields(a, arrFields, ref ipos,schema); }
	}
	public partial class IfcStructuralPointConnection : IfcStructuralConnection
	{
		private int mConditionCoordinateSystem = 0;//	:	OPTIONAL IfcAxis2Placement3D;

		internal IfcAxis2Placement3D ConditionCoordinateSystem { get { return mDatabase.mIfcObjects[mConditionCoordinateSystem] as IfcAxis2Placement3D; } set { mConditionCoordinateSystem = (value == null ? 0 : value.mIndex); } }
		public new IfcBoundaryNodeCondition AppliedCondition { get { return mDatabase.mIfcObjects[mAppliedCondition] as IfcBoundaryNodeCondition; } set { mAppliedCondition = (value == null ? 0 : value.mIndex); } }

		public IfcStructuralPointConnection() : base() { }
		internal IfcStructuralPointConnection(IfcStructuralPointConnection c) : base(c) { }
		
		internal static IfcStructuralPointConnection Parse(string strDef) { IfcStructuralPointConnection c = new IfcStructuralPointConnection(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcStructuralPointConnection c, List<string> arrFields, ref int ipos)
		{
			IfcStructuralConnection.parseFields(c, arrFields, ref ipos);
			if (ipos < arrFields.Count)
				c.mConditionCoordinateSystem = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mConditionCoordinateSystem == 0 ? ",$" : ",#" + mConditionCoordinateSystem)); }
	}
	public class IfcStructuralPointReaction : IfcStructuralReaction
	{
		internal IfcStructuralPointReaction() : base() { }
		internal IfcStructuralPointReaction(IfcStructuralPointReaction p) : base(p) { }
		internal static IfcStructuralPointReaction Parse(string strDef) { IfcStructuralPointReaction r = new IfcStructuralPointReaction(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
		internal static void parseFields(IfcStructuralPointReaction r, List<string> arrFields, ref int ipos) { IfcStructuralReaction.parseFields(r, arrFields, ref ipos); }
	}
	public class IfcStructuralProfileProperties : IfcGeneralProfileProperties //IFC4 DELETED Entity replaced by IfcProfileProperties
	{
		internal double mTorsionalConstantX, mMomentOfInertiaYZ, mMomentOfInertiaY, mMomentOfInertiaZ;// : OPTIONAL IfcMomentOfInertiaMeasure;
		internal double mWarpingConstant;// : OPTIONAL IfcWarpingConstantMeasure;
		internal double mShearCentreZ, mShearCentreY;// : OPTIONAL IfcLengthMeasure;
		internal double mShearDeformationAreaZ, mShearDeformationAreaY;// : OPTIONAL IfcAreaMeasure;
		internal double mMaximumSectionModulusY, mMinimumSectionModulusY, mMaximumSectionModulusZ, mMinimumSectionModulusZ;// : OPTIONAL IfcSectionModulusMeasure;
		internal double mTorsionalSectionModulus;// : OPTIONAL IfcSectionModulusMeasure;
		internal double mCentreOfGravityInX, mCentreOfGravityInY;// : OPTIONAL IfcLengthMeasure; 
		internal IfcStructuralProfileProperties() : base() { }
		internal IfcStructuralProfileProperties(IfcStructuralProfileProperties p) : base(p)
		{
			mTorsionalConstantX = p.mTorsionalConstantX; mMomentOfInertiaYZ = p.mMomentOfInertiaYZ; mMomentOfInertiaY = p.mMomentOfInertiaY;
			mMomentOfInertiaZ = p.mMomentOfInertiaZ; mWarpingConstant = p.mWarpingConstant;
			mShearCentreZ = p.mShearCentreZ; mShearCentreY = p.mShearCentreY;
			mShearDeformationAreaZ = p.mShearDeformationAreaZ; mShearDeformationAreaY = p.mShearDeformationAreaY;
			mMaximumSectionModulusY = p.mMaximumSectionModulusY; mMinimumSectionModulusY = p.mMinimumSectionModulusY; mMaximumSectionModulusZ = p.mMaximumSectionModulusZ; mMinimumSectionModulusZ = p.mMinimumSectionModulusZ;
			mTorsionalSectionModulus = p.mTorsionalSectionModulus;
			mCentreOfGravityInX = p.mCentreOfGravityInX; mCentreOfGravityInY = p.mCentreOfGravityInY;
		}
		internal static void parseFields(IfcStructuralProfileProperties gp, List<string> arrFields, ref int ipos,Schema schema)
		{
			IfcGeneralProfileProperties.parseFields(gp, arrFields, ref ipos,schema);
			gp.mTorsionalConstantX = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mMomentOfInertiaYZ = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mMomentOfInertiaY = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mMomentOfInertiaZ = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mWarpingConstant = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mShearCentreZ = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mShearCentreY = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mShearDeformationAreaZ = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mShearDeformationAreaY = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mMaximumSectionModulusY = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mMinimumSectionModulusY = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mMaximumSectionModulusZ = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mMinimumSectionModulusZ = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mTorsionalSectionModulus = ParserSTEP.ParseDouble(arrFields[ipos++]);
			gp.mCentreOfGravityInX = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mCentreOfGravityInY = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		internal new static IfcStructuralProfileProperties Parse(string strDef,Schema schema) { IfcStructuralProfileProperties p = new IfcStructuralProfileProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		protected override string BuildString()
		{
			return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mTorsionalConstantX) + "," + ParserSTEP.DoubleOptionalToString(mMomentOfInertiaYZ) + "," + ParserSTEP.DoubleOptionalToString(mMomentOfInertiaY) + "," + ParserSTEP.DoubleOptionalToString(mMomentOfInertiaZ) + "," +
				ParserSTEP.DoubleOptionalToString(mWarpingConstant) + "," + ParserSTEP.DoubleOptionalToString(mShearCentreZ) + "," + ParserSTEP.DoubleOptionalToString(mShearCentreY) + "," + ParserSTEP.DoubleOptionalToString(mShearDeformationAreaZ) + "," +
				ParserSTEP.DoubleOptionalToString(mShearDeformationAreaY) + "," + ParserSTEP.DoubleOptionalToString(mMaximumSectionModulusY) + "," + ParserSTEP.DoubleOptionalToString(mMinimumSectionModulusY) + "," + ParserSTEP.DoubleOptionalToString(mMaximumSectionModulusZ) + "," +
				ParserSTEP.DoubleOptionalToString(mMinimumSectionModulusZ) + "," + ParserSTEP.DoubleOptionalToString(mTorsionalSectionModulus) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInX) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY);
		}
	}
	public abstract class IfcStructuralReaction : IfcStructuralActivity
	{   //INVERSE 
		//internal List<int> mCauses = new List<int>();// : OPTIONAL IfcStructuralReaction;
		protected IfcStructuralReaction() : base() { }
		protected IfcStructuralReaction(IfcStructuralReaction p) : base(p) { }
		protected static void parseFields(IfcStructuralReaction a, List<string> arrFields, ref int ipos) { IfcStructuralActivity.parseFields(a, arrFields, ref ipos); }
	}
	public class IfcStructuralResultGroup : IfcGroup
	{
		internal IfcAnalysisTheoryTypeEnum mTheoryType = IfcAnalysisTheoryTypeEnum.NOTDEFINED;// : IfcAnalysisTheoryTypeEnum;
		internal int mResultForLoadGroup;// : OPTIONAL IfcStructuralLoadGroup;
		internal bool mIsLinear = false;// : BOOLEAN; 
		internal IfcStructuralResultGroup() : base() { }
		internal IfcStructuralResultGroup(IfcStructuralResultGroup p) : base(p) { mTheoryType = p.mTheoryType; mResultForLoadGroup = p.mResultForLoadGroup; mIsLinear = p.mIsLinear; }
		internal IfcStructuralResultGroup(DatabaseIfc m, string name) : base(m, name) { }
		internal new static IfcStructuralResultGroup Parse(string strDef) { IfcStructuralResultGroup g = new IfcStructuralResultGroup(); int ipos = 0; parseFields(g, ParserSTEP.SplitLineFields(strDef), ref ipos); return g; }
		internal static void parseFields(IfcStructuralResultGroup g, List<string> arrFields, ref int ipos)
		{
			IfcGroup.parseFields(g, arrFields, ref ipos);
			g.mTheoryType = (IfcAnalysisTheoryTypeEnum)Enum.Parse(typeof(IfcAnalysisTheoryTypeEnum), arrFields[ipos++].Replace(".", ""));
			g.mResultForLoadGroup = ParserSTEP.ParseLink(arrFields[ipos++]);
			g.mIsLinear = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + ",." + mTheoryType.ToString() + ".," + ParserSTEP.LinkToString(mResultForLoadGroup) + "," + ParserSTEP.BoolToString(mIsLinear); }
	}
	public class IfcStructuralSteelProfileProperties : IfcStructuralProfileProperties //IFC4 DELETED Entity replaced by IfcProfileProperties
	{
		internal double mShearAreaZ;// : OPTIONAL IfcAreaMeasure;
		internal double mShearAreaY;// : OPTIONAL IfcAreaMeasure;
		internal double mPlasticShapeFactorY;// : OPTIONAL IfcPositiveRatioMeasure;
		internal double mPlasticShapeFactorZ;// : OPTIONAL IfcPositiveRatioMeasure; 
		internal IfcStructuralSteelProfileProperties() : base() { }
		internal IfcStructuralSteelProfileProperties(IfcStructuralSteelProfileProperties p) : base(p) { mShearAreaZ = p.mShearAreaZ; mShearAreaY = p.mShearAreaY; mPlasticShapeFactorY = p.mPlasticShapeFactorY; mPlasticShapeFactorZ = p.mPlasticShapeFactorZ; }
		internal static void parseFields(IfcStructuralSteelProfileProperties gp, List<string> arrFields, ref int ipos) { IfcStructuralProfileProperties.parseFields(gp, arrFields, ref ipos); gp.mShearAreaZ = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mShearAreaY = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mPlasticShapeFactorY = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mPlasticShapeFactorZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal new static IfcStructuralSteelProfileProperties Parse(string strDef) { IfcStructuralSteelProfileProperties p = new IfcStructuralSteelProfileProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mShearAreaZ) + "," + ParserSTEP.DoubleOptionalToString(mShearAreaY) + "," + ParserSTEP.DoubleOptionalToString(mPlasticShapeFactorY) + "," + ParserSTEP.DoubleOptionalToString(mPlasticShapeFactorZ); }
	}
	public partial class IfcStructuralSurfaceAction : IfcStructuralAction //IFC4 SUPERTYPE OF(IfcStructuralPlanarAction)
	{
		internal IfcProjectedOrTrueLengthEnum mProjectedOrTrue = IfcProjectedOrTrueLengthEnum.TRUE_LENGTH;// : IfcProjectedOrTrueLengthEnum
		internal IfcStructuralSurfaceActivityTypeEnum mPredefinedType = IfcStructuralSurfaceActivityTypeEnum.NOTDEFINED;//IfcStructuralCurveActivityTypeEnum
		internal IfcStructuralSurfaceAction() : base() { }
		internal IfcStructuralSurfaceAction(IfcStructuralSurfaceAction p) : base(p) { mProjectedOrTrue = p.mProjectedOrTrue; mPredefinedType = p.mPredefinedType; }
		internal IfcStructuralSurfaceAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, bool global, bool projected, IfcStructuralSurfaceActivityTypeEnum type)
			: base(lc, item, load, global) { if (mDatabase.mSchema == Schema.IFC2x3) throw new Exception(KeyWord + " in IFC4 Only"); mProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH; mPredefinedType = type; }
		internal static IfcStructuralSurfaceAction Parse(string strDef, Schema schema) { IfcStructuralSurfaceAction a = new IfcStructuralSurfaceAction(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcStructuralSurfaceAction a, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcStructuralAction.parseFields(a, arrFields, ref ipos,schema);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mProjectedOrTrue = (IfcProjectedOrTrueLengthEnum)Enum.Parse(typeof(IfcProjectedOrTrueLengthEnum), s.Replace(".", ""));
			if (schema != Schema.IFC2x3)
				a.mPredefinedType = (IfcStructuralSurfaceActivityTypeEnum)Enum.Parse(typeof(IfcStructuralSurfaceActivityTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildString() { return base.BuildString() + ",." + mProjectedOrTrue.ToString() + (mDatabase.mSchema == Schema.IFC2x3 ? "." : ".,." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcStructuralSurfaceConnection : IfcStructuralConnection
	{
		internal IfcStructuralSurfaceConnection() : base() { }
		internal IfcStructuralSurfaceConnection(IfcStructuralSurfaceConnection c) : base(c) { }
		
		internal static IfcStructuralSurfaceConnection Parse(string strDef) { IfcStructuralSurfaceConnection c = new IfcStructuralSurfaceConnection(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcStructuralSurfaceConnection c, List<string> arrFields, ref int ipos) { IfcStructuralConnection.parseFields(c, arrFields, ref ipos); }
	}
	public partial class IfcStructuralSurfaceMember : IfcStructuralMember
	{
		internal IfcStructuralSurfaceTypeEnum mPredefinedType = IfcStructuralSurfaceTypeEnum.NOTDEFINED;// : IfcStructuralSurfaceTypeEnum;
		internal double mThickness;// : OPTIONAL IfcPositiveLengthMeasure; 

		public IfcStructuralSurfaceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcStructuralSurfaceMember() : base() { }
		internal IfcStructuralSurfaceMember(IfcStructuralSurfaceMember p) : base(p) { mPredefinedType = p.mPredefinedType; mThickness = p.mThickness; }

		internal static IfcStructuralSurfaceMember Parse(string strDef) { IfcStructuralSurfaceMember m = new IfcStructuralSurfaceMember(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcStructuralSurfaceMember sm, List<string> arrFields, ref int ipos) { IfcStructuralMember.parseFields(sm, arrFields, ref ipos); sm.mPredefinedType = (IfcStructuralSurfaceTypeEnum)Enum.Parse(typeof(IfcStructuralSurfaceTypeEnum), arrFields[ipos++].Replace(".", "")); sm.mThickness = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mThickness); }
	}
	public class IfcStructuralSurfaceMemberVarying : IfcStructuralSurfaceMember
	{
		internal List<double> mSubsequentThickness = new List<double>();// : LIST [2:?] OF IfcPositiveLengthMeasure;
		internal int mVaryingThicknessLocation;// : IfcShapeAspect; 
		internal IfcStructuralSurfaceMemberVarying() : base() { }
		internal IfcStructuralSurfaceMemberVarying(IfcStructuralSurfaceMemberVarying p) : base(p) { mSubsequentThickness = new List<double>(p.mSubsequentThickness.ToArray()); mVaryingThicknessLocation = p.mVaryingThicknessLocation; }
		internal new static IfcStructuralSurfaceMemberVarying Parse(string strDef) { IfcStructuralSurfaceMemberVarying m = new IfcStructuralSurfaceMemberVarying(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcStructuralSurfaceMemberVarying sm, List<string> arrFields, ref int ipos)
		{
			IfcStructuralSurfaceMember.parseFields(sm, arrFields, ref ipos);
			List<string> lst = ParserSTEP.SplitLineFields(arrFields[ipos++]);
			for (int icounter = 0; icounter < lst.Count; icounter++)
				sm.mSubsequentThickness.Add(ParserSTEP.ParseDouble(lst[icounter]));
			sm.mVaryingThicknessLocation = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.DoubleToString(mSubsequentThickness[0]);
			for (int icounter = 1; icounter < mSubsequentThickness.Count; icounter++)
				str += "," + ParserSTEP.DoubleToString(mSubsequentThickness[icounter]);
			return str + ")" + "," + ParserSTEP.LinkToString(mVaryingThicknessLocation);
		}
	}
	public class IfcStructuredDimensionCallout : IfcDraughtingCallout // DEPRECEATED IFC4
	{
		internal IfcStructuredDimensionCallout() : base() { }
		internal IfcStructuredDimensionCallout(IfcStructuredDimensionCallout i) : base(i) { }
		internal new static IfcStructuredDimensionCallout Parse(string strDef) { IfcStructuredDimensionCallout d = new IfcStructuredDimensionCallout(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		internal static void parseFields(IfcStructuredDimensionCallout d, List<string> arrFields, ref int ipos) { IfcDraughtingCallout.parseFields(d, arrFields, ref ipos); }
	}
	public partial interface IfcStyleAssignmentSelect : IfcInterface //(IfcPresentationStyle ,IfcPresentationStyleAssignment); update styledItems inverse 
	{
		List<IfcStyledItem> StyledItems { get; }
	}
	public partial class IfcStyledItem : IfcRepresentationItem
	{
		private int mItem;// : OPTIONAL IfcRepresentationItem;
		private List<int> mStyles = new List<int>();// : SET [1:?] OF IfcStyleAssignmentSelect; ifc2x3 IfcPresentationStyleAssignment;
		private string mName = "$";// : OPTIONAL IfcLabel; 

		internal IfcRepresentationItem Item { get { return mDatabase.mIfcObjects[mItem] as IfcRepresentationItem; } set { mItem = (value == null ? 0 : value.mIndex); } }
		internal List<IfcStyleAssignmentSelect> Styles { get { return mStyles.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcStyleAssignmentSelect); } }
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		internal IfcStyledItem() : base() { }
		internal IfcStyledItem(IfcStyledItem i) : base(i) { mItem = i.mItem; mStyles.AddRange(i.mStyles); }
		public IfcStyledItem(IfcStyleAssignmentSelect style, string name) : base(style.Database)
		{
			Name = name;
			if (mDatabase.mSchema == Schema.IFC2x3)
			{
				IfcPresentationStyle ps = style as IfcPresentationStyle;
				if (ps != null)
					mStyles.Add(new IfcPresentationStyleAssignment(ps).mIndex);
				else
					throw new Exception("XX Invalid style for schema " + mDatabase.mSchema.ToString());
			}
			else
				mStyles.Add(style.Index);
		}
		
		internal static IfcStyledItem Parse(string strDef) { IfcStyledItem i = new IfcStyledItem(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcStyledItem i, List<string> arrFields, ref int ipos)
		{
			IfcRepresentationItem.parseFields(i, arrFields, ref ipos);
			i.mItem = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mStyles = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			i.mName = arrFields[ipos++].Replace("'", "");
		}
		internal void associateItem()
		{
			IfcRepresentationItem ri = Item;
			if (ri != null)
				ri.mStyledByItem = this;
			List<IfcStyleAssignmentSelect> styles = Styles;
			for (int icounter = 0; icounter < styles.Count; icounter++)
			{
				IfcPresentationStyle ps = styles[icounter] as IfcPresentationStyle;
				if (ps != null)
					ps.mStyledItems.Add(this);
				else
				{
					IfcPresentationStyleAssignment psa = styles[icounter] as IfcPresentationStyleAssignment;
					if (psa != null)
						psa.mStyledItems.Add(this);
				}
			}
		}
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential)
				return "";
			string result = base.BuildString() + "," + ParserSTEP.LinkToString(mItem);
			if (mStyles.Count > 0)
			{
				result += ",(" + ParserSTEP.LinkToString(mStyles[0]);
				for (int icounter = 1; icounter < mStyles.Count; icounter++)
					result += "," + ParserSTEP.LinkToString(mStyles[icounter]);
			}
			else
				result += ",(";
			return result + (mName == "$" ? "),$" : "),'" + mName + "'");
		}
	}
	public class IfcStyledRepresentation : IfcStyleModel
	{
		internal new List<IfcStyledItem> Items { get { return base.Items.ConvertAll(x => x as IfcStyledItem); } }

		internal IfcStyledRepresentation() : base() { }
		internal IfcStyledRepresentation(IfcStyledRepresentation i) : base(i) { }
		internal IfcStyledRepresentation(IfcStyledItem ri) : base(ri) { }
		public IfcStyledRepresentation(List<IfcStyledItem> reps) : base(reps.ConvertAll(x => x as IfcRepresentationItem)) { }
		internal new static IfcStyledRepresentation Parse(string strDef)
		{
			IfcStyledRepresentation r = new IfcStyledRepresentation();
			int pos = 0;
			IfcShapeModel.parseString(r, strDef, ref pos);
			return r;
		}
	}
	public abstract class IfcStyleModel : IfcRepresentation  //ABSTRACT SUPERTYPE OF(IfcStyledRepresentation)
	{
		protected IfcStyleModel() : base() { }
		protected IfcStyleModel(IfcStyleModel i) : base(i) { }
		protected IfcStyleModel(IfcRepresentationItem ri) : base(ri) { }
		protected IfcStyleModel(List<IfcRepresentationItem> reps) : base(reps, "", "") { }

		protected static void parseString(IfcStyleModel shape, string str, ref int pos) { IfcRepresentation.parseString(shape, str, ref pos); }
	}
	public class IfcSubContractResource : IfcConstructionResource
	{
		internal IfcSubContractResourceTypeEnum mPredefinedType = IfcSubContractResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcSubContractResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSubContractResource() : base() { }
		internal IfcSubContractResource(IfcSubContractResource o) : base(o) { mPredefinedType = o.mPredefinedType; }
		internal IfcSubContractResource(DatabaseIfc m) : base(m) { }
		internal static IfcSubContractResource Parse(string strDef, Schema schema) { IfcSubContractResource r = new IfcSubContractResource(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return r; }
		internal static void parseFields(IfcSubContractResource r, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcConstructionResource.parseFields(r, arrFields, ref ipos,schema);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					r.mPredefinedType = (IfcSubContractResourceTypeEnum)Enum.Parse(typeof(IfcSubContractResourceTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcSubContractResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcSubContractResourceTypeEnum mPredefinedType = IfcSubContractResourceTypeEnum.NOTDEFINED;
		public IfcSubContractResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcSubContractResourceType() : base() { }
		internal IfcSubContractResourceType(IfcSubContractResourceType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcSubContractResourceType(DatabaseIfc m, string name, IfcSubContractResourceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcSubContractResourceType t, List<string> arrFields, ref int ipos) { IfcSubContractResourceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSubContractResourceTypeEnum)Enum.Parse(typeof(IfcSubContractResourceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSubContractResourceType Parse(string strDef) { IfcSubContractResourceType t = new IfcSubContractResourceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcSubedge : IfcEdge
	{
		internal int mParentEdge;// IfcEdge;
		internal IfcSubedge() : base() { }
		internal IfcSubedge(IfcSubedge el) : base(el) { mParentEdge = el.mParentEdge; }
		public IfcSubedge(IfcVertex v1, IfcVertex v2, IfcEdge e) : base(v1, v2) { mParentEdge = e.mIndex; }
		internal new static IfcSubedge Parse(string strDef) { IfcSubedge e = new IfcSubedge(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		internal static void parseFields(IfcSubedge e, List<string> arrFields, ref int ipos) { IfcEdge.parseFields(e, arrFields, ref ipos); e.mParentEdge = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mParentEdge); }
	}
	public abstract partial class IfcSurface : IfcGeometricRepresentationItem, IfcGeometricSetSelect /*	ABSTRACT SUPERTYPE OF (ONEOF(IfcBoundedSurface,IfcElementarySurface,IfcSweptSurface))*/
	{
		protected IfcSurface() : base() { }
		protected IfcSurface(IfcSurface p) : base(p) { }
		protected IfcSurface(DatabaseIfc db) : base(db) { }
		protected static void parseFields(IfcSurface s, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcSurfaceCurveSweptAreaSolid : IfcSweptAreaSolid
	{
		internal int mDirectrix; // : IfcCurve;
		internal double mStartParam = 0;// : OPT IfcParameterValue; OPT IFC4
		internal double mEndParam;//: OPT IfcParameterValue; OPT IFC4
		internal int mReferenceSurface;// : IfcSurface; 

		internal IfcCurve Directrix { get { return mDatabase.mIfcObjects[mDirectrix] as IfcCurve; } }
		internal IfcSurface ReferenceSurface { get { return mDatabase.mIfcObjects[mReferenceSurface] as IfcSurface; } }

		internal IfcSurfaceCurveSweptAreaSolid() : base() { }
		internal IfcSurfaceCurveSweptAreaSolid(IfcSurfaceCurveSweptAreaSolid p) : base(p) { mDirectrix = p.mDirectrix; mStartParam = p.mStartParam; mEndParam = p.mEndParam; mReferenceSurface = p.mReferenceSurface; }

		internal static void parseFields(IfcSurfaceCurveSweptAreaSolid s, List<string> arrFields, ref int ipos) { IfcSweptAreaSolid.parseFields(s, arrFields, ref ipos); s.mDirectrix = ParserSTEP.ParseLink(arrFields[ipos++]); s.mStartParam = ParserSTEP.ParseDouble(arrFields[ipos++]); s.mEndParam = ParserSTEP.ParseDouble(arrFields[ipos++]); s.mReferenceSurface = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcSurfaceCurveSweptAreaSolid Parse(string strDef) { IfcSurfaceCurveSweptAreaSolid s = new IfcSurfaceCurveSweptAreaSolid(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mDirectrix) + "," + ParserSTEP.DoubleToString(mStartParam) + "," + ParserSTEP.DoubleToString(mEndParam) + "," + ParserSTEP.LinkToString(mReferenceSurface); }
	}
	public partial class IfcSurfaceOfLinearExtrusion : IfcSweptSurface
	{
		internal int mExtrudedDirection;//  : IfcDirection;
		internal double mDepth;// : IfcLengthMeasure;

		internal IfcDirection ExtrudedDirection { get { return mDatabase.mIfcObjects[mExtrudedDirection] as IfcDirection; } }

		internal IfcSurfaceOfLinearExtrusion() : base() { }
		internal IfcSurfaceOfLinearExtrusion(IfcSurfaceOfLinearExtrusion p) : base(p) { mExtrudedDirection = p.mExtrudedDirection; mDepth = p.mDepth; }

		internal static void parseFields(IfcSurfaceOfLinearExtrusion s, List<string> arrFields, ref int ipos) { IfcSweptSurface.parseFields(s, arrFields, ref ipos); s.mExtrudedDirection = ParserSTEP.ParseLink(arrFields[ipos++]); s.mDepth = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcSurfaceOfLinearExtrusion Parse(string strDef) { IfcSurfaceOfLinearExtrusion s = new IfcSurfaceOfLinearExtrusion(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mExtrudedDirection) + "," + ParserSTEP.DoubleToString(mDepth); }
	}
	public partial class IfcSurfaceOfRevolution : IfcSweptSurface
	{
		internal int mAxisPosition;//  : IfcAxis1Placement;
		public IfcAxis1Placement AxisPosition { get { return mDatabase.mIfcObjects[mAxisPosition] as IfcAxis1Placement; } }

		internal IfcSurfaceOfRevolution() : base() { }
		internal IfcSurfaceOfRevolution(IfcSurfaceOfRevolution p) : base(p) { mAxisPosition = p.mAxisPosition; }

		internal static void parseFields(IfcSurfaceOfRevolution s, List<string> arrFields, ref int ipos) { IfcSweptSurface.parseFields(s, arrFields, ref ipos); s.mAxisPosition = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcSurfaceOfRevolution Parse(string strDef) { IfcSurfaceOfRevolution s = new IfcSurfaceOfRevolution(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mAxisPosition); }
	}
	public partial class IfcSurfaceStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal IfcSurfaceSide mSide = IfcSurfaceSide.BOTH;// : IfcSurfaceSide;
		internal List<int> mStyles = new List<int>();// : SET [1:5] OF IfcSurfaceStyleElementSelect; 
		internal IfcSurfaceStyle() : base() { }
		internal IfcSurfaceStyle(IfcSurfaceStyle s) : base(s) { mSide = s.mSide; mStyles.AddRange(s.mStyles); }
		
		internal static void parseFields(IfcSurfaceStyle s, List<string> arrFields, ref int ipos)
		{
			IfcPresentationStyle.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str.StartsWith("."))
				s.mSide = (IfcSurfaceSide)Enum.Parse(typeof(IfcSurfaceSide), str.Replace(".", ""));
			s.mStyles = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		internal static IfcSurfaceStyle Parse(string strDef) { IfcSurfaceStyle s = new IfcSurfaceStyle(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential)
				return "";
			string str = base.BuildString() + ",." + mSide.ToString() + ".,(" + ParserSTEP.LinkToString(mStyles[0]);
			for (int icounter = 1; icounter < mStyles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mStyles[icounter]);
			return str + ")";
		}
	}
	public partial interface IfcSurfaceStyleElementSelect : IfcInterface //SELECT(IfcSurfaceStyleShading, IfcSurfaceStyleLighting, IfcSurfaceStyleWithTextures
	{ //, IfcExternallyDefinedSurfaceStyle, IfcSurfaceStyleRefraction);
	}
	public partial class IfcSurfaceStyleLighting : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal int mDiffuseTransmissionColour, mDiffuseReflectionColour, mTransmissionColour, mReflectanceColour;//	 :	IfcColourRgb;
		internal IfcSurfaceStyleLighting() : base() { }
		internal IfcSurfaceStyleLighting(IfcSurfaceStyleLighting i)
			: base(i)
		{
			mDiffuseTransmissionColour = i.mDiffuseTransmissionColour;
			mDiffuseReflectionColour = i.mDiffuseReflectionColour;
			mTransmissionColour = i.mTransmissionColour;
			mReflectanceColour = i.mReflectanceColour;
		}
		
		protected override void parseFields(List<string> arrFields, ref int ipos)
		{
			base.parseFields(arrFields, ref ipos);
			mDiffuseTransmissionColour = ParserSTEP.ParseLink(arrFields[ipos++]);
			mDiffuseReflectionColour = ParserSTEP.ParseLink(arrFields[ipos++]);
			mTransmissionColour = ParserSTEP.ParseLink(arrFields[ipos++]);
			mReflectanceColour = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		internal static IfcSurfaceStyleLighting Parse(string strDef) { IfcSurfaceStyleLighting s = new IfcSurfaceStyleLighting(); int ipos = 0; s.parseFields(ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return (mDatabase.mOutputEssential ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mDiffuseTransmissionColour) + "," + ParserSTEP.LinkToString(mDiffuseReflectionColour) + "," + ParserSTEP.LinkToString(mTransmissionColour) + "," + ParserSTEP.LinkToString(mReflectanceColour)); }
	}
	public partial class IfcSurfaceStyleRefraction : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal double mRefractionIndex, mDispersionFactor;//	 :	OPTIONAL IfcReal;
		internal IfcSurfaceStyleRefraction() : base() { }
		internal IfcSurfaceStyleRefraction(IfcSurfaceStyleRefraction s) : base(s)
		{
			mRefractionIndex = s.mRefractionIndex;
			mDispersionFactor = s.mDispersionFactor;
		}
		
		protected override void parseFields(List<string> arrFields, ref int ipos)
		{
			base.parseFields(arrFields, ref ipos);
			mRefractionIndex = ParserSTEP.ParseDouble(arrFields[ipos++]);
			mDispersionFactor = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		internal static IfcSurfaceStyleRefraction Parse(string strDef) { IfcSurfaceStyleRefraction s = new IfcSurfaceStyleRefraction(); int ipos = 0; s.parseFields(ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return (mDatabase.mOutputEssential ? "" : base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mRefractionIndex) + "," + ParserSTEP.DoubleOptionalToString(mDispersionFactor)); }
	}
	public class IfcSurfaceStyleRendering : IfcSurfaceStyleShading
	{
		internal double mTransparency;// : OPTIONAL IfcNormalisedRatioMeasure;
		internal IfcColourOrFactor mDiffuseColour, mTransmissionColour, mDiffuseTransmissionColour, mReflectionColour, mSpecularColour;//:	OPTIONAL IfcColourOrFactor;
		internal IfcSpecularHighlightSelect mSpecularHighlight;// : OPTIONAL 
		internal IfcReflectanceMethodEnum mReflectanceMethod = IfcReflectanceMethodEnum.NOTDEFINED;// : IfcReflectanceMethodEnum; 
		internal IfcSurfaceStyleRendering() : base() { }
		internal IfcSurfaceStyleRendering(IfcSurfaceStyleRendering r) : base(r)
		{
			mTransparency = r.mTransparency;
			mDiffuseColour = r.mDiffuseColour;
			mTransmissionColour = r.mTransmissionColour;
			mDiffuseTransmissionColour = r.mDiffuseTransmissionColour;
			mReflectionColour = r.mReflectionColour;
			mSpecularColour = r.mSpecularColour;
			mSpecularHighlight = r.mSpecularHighlight;
			mReflectanceMethod = r.mReflectanceMethod;
		}
		internal IfcSurfaceStyleRendering(DatabaseIfc m, Color surface, double transparency, Color diffuse, Color transmission, Color reflection, Color specular, IfcSpecularHighlightSelect highlight, IfcReflectanceMethodEnum rm)
			: base(m, surface)
		{
			mTransparency = transparency;
			if (!diffuse.IsEmpty)
				mDiffuseColour = new IfcNormalisedRatioMeasure(diffuse);
			if (!transmission.IsEmpty)
				mTransmissionColour = new IfcNormalisedRatioMeasure(transmission);
			if (!reflection.IsEmpty)
				mReflectionColour = new IfcNormalisedRatioMeasure(reflection);
			if (!specular.IsEmpty)
				mSpecularColour = new IfcNormalisedRatioMeasure(specular);
			mSpecularHighlight = highlight;
			mReflectanceMethod = rm;
		}
		internal static void parseFields(IfcSurfaceStyleRendering s, List<string> arrFields, ref int ipos)
		{
			IfcSurfaceStyleShading.parseFields(s, arrFields, ref ipos);
			s.mTransparency = ParserSTEP.ParseDouble(arrFields[ipos++]);
			string str = arrFields[ipos++];
			if (str != "$")
				s.mDiffuseColour = ParserIfc.parseColourOrFactor(str);
			str = arrFields[ipos++];
			if (str != "$")
				s.mTransmissionColour = ParserIfc.parseColourOrFactor(str);
			str = arrFields[ipos++];
			if (str != "$")
				s.mDiffuseTransmissionColour = ParserIfc.parseColourOrFactor(str);
			str = arrFields[ipos++];
			if (str != "$")
				s.mReflectionColour = ParserIfc.parseColourOrFactor(str);
			str = arrFields[ipos++];
			if (str != "$")
				s.mSpecularColour = ParserIfc.parseColourOrFactor(str);
			str = arrFields[ipos++];
			if (str != "$")
			{
				if (str.StartsWith("IFCSPECULARROUGHNESS"))
					s.mSpecularHighlight = new IfcSpecularRoughness(double.Parse(str.Substring(21, str.Length - 22)));
				else
					s.mSpecularHighlight = new IfcSpecularExponent(double.Parse(str.Substring(20, str.Length - 21)));
			}
			str = arrFields[ipos++];
			if (str != "$")
				s.mReflectanceMethod = (IfcReflectanceMethodEnum)Enum.Parse(typeof(IfcReflectanceMethodEnum), str.Replace(".", ""));
		}
		internal new static IfcSurfaceStyleRendering Parse(string strDef) { IfcSurfaceStyleRendering s = new IfcSurfaceStyleRendering(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return (mDatabase.mOutputEssential ? "" : base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mTransparency) +
				(mDiffuseColour == null ? ",$" : "," + mDiffuseColour.ToString()) + (mTransmissionColour == null ? ",$" : "," + mTransmissionColour.ToString()) +
				(mDiffuseTransmissionColour == null ? ",$" : "," + mDiffuseTransmissionColour) + (mReflectionColour == null ? ",$" : mReflectionColour.ToString()) + (mSpecularColour == null ? ",$" : "," + mSpecularColour.ToString()) +
				(mSpecularHighlight == null ? ",$" : "," + mSpecularHighlight.ToString()) + ",." + mReflectanceMethod.ToString() + ".");
		}
	}
	public partial class IfcSurfaceStyleShading : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		private int mSurfaceColour;// : IfcColourRgb;
		internal IfcColourRgb SurfaceColour { get { return mDatabase.mIfcObjects[mSurfaceColour] as IfcColourRgb; } }
		internal IfcSurfaceStyleShading() : base() { }
		internal IfcSurfaceStyleShading(IfcSurfaceStyleShading i) : base() { mSurfaceColour = i.mSurfaceColour; }
		public IfcSurfaceStyleShading(DatabaseIfc m, Color surface) : base(m) { mSurfaceColour =  new IfcColourRgb(m,"",surface).mIndex; }
		internal static void parseFields(IfcSurfaceStyleShading s, List<string> arrFields, ref int ipos) { s.mSurfaceColour = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcSurfaceStyleShading Parse(string strDef) { IfcSurfaceStyleShading s = new IfcSurfaceStyleShading(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mSurfaceColour); }
	}
	public partial class IfcSurfaceStyleWithTextures : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal List<int> mTextures = new List<int>();//: LIST [1:?] OF IfcSurfaceTexture; 
		internal IfcSurfaceStyleWithTextures() : base() { }
		internal IfcSurfaceStyleWithTextures(IfcSurfaceStyleWithTextures i) : base() { mTextures = new List<int>(i.mTextures.ToArray()); }
		internal IfcSurfaceStyleWithTextures(List<IfcSurfaceTexture> textures) : base(textures[0].mDatabase) { mTextures = textures.ConvertAll(x => x.mIndex);  }
		internal static void parseFields(IfcSurfaceStyleWithTextures s, List<string> arrFields, ref int ipos) { s.mTextures = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcSurfaceStyleWithTextures Parse(string strDef) { IfcSurfaceStyleWithTextures s = new IfcSurfaceStyleWithTextures(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mTextures[0]);
			for (int icounter = 1; icounter < mTextures.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mTextures[icounter]);
			return str + ")";
		}
	}
	public abstract partial class IfcSurfaceTexture : IfcPresentationItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcBlobTexture ,IfcImageTexture ,IfcPixelTexture));
	{
		internal int mMatIndex = -1;
		internal bool mRepeatS;// : BOOLEAN;
		internal bool mRepeatT;// : BOOLEAN;
		internal string mMode = "$"; //:	OPTIONAL IfcIdentifier; internal IfcSurfaceTextureEnum mTextureType;// : IfcSurfaceTextureEnum; // IFC2x3
		internal int mTextureTransform;// : OPTIONAL IfcCartesianTransformationOperator2D;
		protected IfcSurfaceTexture() : base() { }
		protected IfcSurfaceTexture(IfcSurfaceTexture i) : base() { mRepeatS = i.mRepeatS; mRepeatT = i.mRepeatT; mMode = i.mMode; mTextureTransform = i.mTextureTransform; }
		protected static void parseFields(IfcSurfaceTexture t, List<string> arrFields, ref int ipos, Schema schema) { t.mRepeatS = ParserSTEP.ParseBool(arrFields[ipos++]); t.mRepeatT = ParserSTEP.ParseBool(arrFields[ipos++]); t.mMode = (schema == Schema.IFC2x3 ? arrFields[ipos++].Replace(".", "") : arrFields[ipos++].Replace("'", "")); t.mTextureTransform = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.BoolToString(mRepeatS) + "," + ParserSTEP.BoolToString(mRepeatT) + (mDatabase.mSchema == Schema.IFC2x3 ? ",." + mMode.ToString() + ".," : mMode == "$" ? ",$," : ",'" + mMode + "',") + ParserSTEP.LinkToString(mTextureTransform); }
	}
	public abstract partial class IfcSweptAreaSolid : IfcSolidModel  /*ABSTRACT SUPERTYPE OF (ONEOF (IfcExtrudedAreaSolid, IfcFixedReferenceSweptAreaSolid ,IfcRevolvedAreaSolid ,IfcSurfaceCurveSweptAreaSolid))*/
	{
		private int mSweptArea;// : IfcProfileDef;
		private int mPosition;// : IfcAxis2Placement3D; 	 :	OPTIONAL IFC4

		internal IfcProfileDef SweptArea { get { return mDatabase.mIfcObjects[mSweptArea] as IfcProfileDef; } set { mSweptArea = value.mIndex; } }
		internal IfcAxis2Placement3D Position { get { return mDatabase.mIfcObjects[mPosition] as IfcAxis2Placement3D; } set { mPosition = (value == null ? 0 : value.mIndex); } }
		protected IfcSweptAreaSolid() : base() { }
		protected IfcSweptAreaSolid(IfcSweptAreaSolid p) : base(p) { mSweptArea = p.mSweptArea; mPosition = p.mPosition; }
		protected IfcSweptAreaSolid(IfcProfileDef prof, IfcAxis2Placement3D placement) : base(prof.mDatabase) { SweptArea = prof; Position = placement; }
		protected IfcSweptAreaSolid(DatabaseIfc m) : base(m) { }

		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mSweptArea) + (mPosition > 0 ? ",#" + mPosition : ",$"); }
		protected static void parseFields(IfcSweptAreaSolid s, List<string> arrFields, ref int ipos) { IfcSolidModel.parseFields(s, arrFields, ref ipos); s.mSweptArea = ParserSTEP.ParseLink(arrFields[ipos++]); s.mPosition = ParserSTEP.ParseLink(arrFields[ipos++]); }
	}
	public partial class IfcSweptDiskSolid : IfcSolidModel
	{
		internal int mDirectrix;// : IfcCurve;
		internal double mRadius;// : IfcPositiveLengthMeasure;
		internal double mInnerRadius;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mStartParam;// : OPTIONAL IfcParameterValue; IFC4
		internal double mEndParam;// : OPTIONAL IfcParameterValue;  IFC4

		internal IfcCurve Directrix { get { return mDatabase.mIfcObjects[mDirectrix] as IfcCurve; } set { mDirectrix = value.mIndex; } }

		internal IfcSweptDiskSolid() : base() { }
		internal IfcSweptDiskSolid(IfcSweptDiskSolid p) : base(p) { mDirectrix = p.mDirectrix; mRadius = p.mRadius; mInnerRadius = p.mInnerRadius; mStartParam = p.mStartParam; mEndParam = p.mEndParam; }
		public IfcSweptDiskSolid(IfcCurve directrix, double radius, double innerRadius) : base(directrix.mDatabase) { Directrix = directrix; mRadius = radius; mInnerRadius = innerRadius; }

		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mDirectrix) + "," + ParserSTEP.DoubleToString(mRadius) + "," + ParserSTEP.DoubleOptionalToString(mInnerRadius) + "," + (mDatabase.mSchema == Schema.IFC2x3 ? ParserSTEP.DoubleToString(mStartParam) + "," + ParserSTEP.DoubleToString(mEndParam) : ParserSTEP.DoubleOptionalToString(mStartParam) + "," + ParserSTEP.DoubleOptionalToString(mEndParam)); }
		internal static void parseFields(IfcSweptDiskSolid s, List<string> arrFields, ref int ipos)
		{
			IfcSolidModel.parseFields(s, arrFields, ref ipos);
			s.mDirectrix = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mInnerRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mStartParam = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mEndParam = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		internal static IfcSweptDiskSolid Parse(string strDef) { IfcSweptDiskSolid s = new IfcSweptDiskSolid(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public partial class IfcSweptDiskSolidPolygonal : IfcSweptDiskSolid
	{
		internal double mFilletRadius;// : OPTIONAL IfcPositiveLengthMeasure; 
		internal IfcSweptDiskSolidPolygonal() : base() { }
		internal IfcSweptDiskSolidPolygonal(IfcSweptDiskSolidPolygonal p) : base(p) { mFilletRadius = p.mFilletRadius; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mFilletRadius); }
		internal static void parseFields(IfcSweptDiskSolidPolygonal s, List<string> arrFields, ref int ipos) { IfcSweptDiskSolid.parseFields(s, arrFields, ref ipos); s.mFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal new static IfcSweptDiskSolidPolygonal Parse(string strDef) { IfcSweptDiskSolidPolygonal s = new IfcSweptDiskSolidPolygonal(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public abstract partial class IfcSweptSurface : IfcSurface /*	ABSTRACT SUPERTYPE OF (ONEOF (IfcSurfaceOfLinearExtrusion ,IfcSurfaceOfRevolution))*/
	{
		internal int mSweptCurve;// : IfcProfileDef;
		internal int mPosition;// : IfcAxis2Placement3D;
		public IfcProfileDef SweptCurve { get { return mDatabase.mIfcObjects[mSweptCurve] as IfcProfileDef; } set { mSweptCurve = value.mIndex; } }
		protected IfcSweptSurface() : base() { }
		protected IfcSweptSurface(IfcSweptSurface p) : base(p) { mSweptCurve = p.mSweptCurve; mPosition = p.mPosition; }
		protected static void parseFields(IfcSweptSurface s, List<string> arrFields, ref int ipos) { IfcSurface.parseFields(s, arrFields, ref ipos); s.mSweptCurve = ParserSTEP.ParseLink(arrFields[ipos++]); s.mPosition = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mSweptCurve) + "," + ParserSTEP.LinkToString(mPosition); }
		internal IfcProfileDef getProfile() { return mDatabase.mIfcObjects[mSweptCurve] as IfcProfileDef; }
	}
	public class IfcSwitchingDevice : IfcFlowController //IFC4
	{
		internal IfcSwitchingDeviceTypeEnum mPredefinedType = IfcSwitchingDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcSwitchingDeviceTypeEnum;
		public IfcSwitchingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcSwitchingDevice() : base() { }
		internal IfcSwitchingDevice(IfcSwitchingDevice d) : base(d) { mPredefinedType = d.mPredefinedType; }
		internal IfcSwitchingDevice(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcSwitchingDevice s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcSwitchingDeviceTypeEnum)Enum.Parse(typeof(IfcSwitchingDeviceTypeEnum), str);
		}
		internal new static IfcSwitchingDevice Parse(string strDef) { IfcSwitchingDevice s = new IfcSwitchingDevice(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcSwitchingDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcSwitchingDeviceType : IfcFlowControllerType
	{
		internal IfcSwitchingDeviceTypeEnum mPredefinedType = IfcSwitchingDeviceTypeEnum.NOTDEFINED;// : IfcFlowMeterTypeEnum;
		public IfcSwitchingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcSwitchingDeviceType() : base() { }
		internal IfcSwitchingDeviceType(IfcSwitchingDeviceType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcSwitchingDeviceType(DatabaseIfc m, string name, IfcSwitchingDeviceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcSwitchingDeviceType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSwitchingDeviceTypeEnum)Enum.Parse(typeof(IfcSwitchingDeviceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSwitchingDeviceType Parse(string strDef) { IfcSwitchingDeviceType t = new IfcSwitchingDeviceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	//ENTITY IfcSymbolStyle // DEPRECEATED IFC4
	public partial class IfcSystem : IfcGroup //SUPERTYPE OF(ONEOF(IfcBuildingSystem, IfcDistributionSystem, IfcStructuralAnalysisModel, IfcZone))
	{
		//INVERSE
		internal IfcRelServicesBuildings mServicesBuildings = null;// : SET [0:1] OF IfcRelServicesBuildings FOR RelatingSystem  
		public IfcRelServicesBuildings ServicesBuildings { get { return mServicesBuildings; } set { mServicesBuildings = value; } }

		internal IfcSystem() : base() { }
		internal IfcSystem(IfcSystem p) : base(p) { }
		internal IfcSystem(DatabaseIfc m, string name) : base(m, name) { }
		internal IfcSystem(IfcSpatialElement e, string name) : base(e.mDatabase, name) { mServicesBuildings = new IfcRelServicesBuildings(this, e) { Name = name };  }
		internal new static IfcSystem Parse(string strDef) { IfcSystem s = new IfcSystem(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcSystem s, List<string> arrFields, ref int ipos) { IfcGroup.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcSystemFurnitureElement : IfcFurnishingElement //IFC4
	{
		internal IfcSystemFurnitureElementTypeEnum mPredefinedType = IfcSystemFurnitureElementTypeEnum.NOTDEFINED;//: OPTIONAL IfcSystemFurnitureElementTypeEnum
		internal IfcSystemFurnitureElement() : base() { }
		internal IfcSystemFurnitureElement(IfcSystemFurnitureElement be) : base(be) { mPredefinedType = be.mPredefinedType; }
		internal IfcSystemFurnitureElement(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static void parseFields(IfcSystemFurnitureElement e, List<string> arrFields, ref int ipos)
		{
			IfcFurnishingElement.parseFields(e, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				e.mPredefinedType = (IfcSystemFurnitureElementTypeEnum)Enum.Parse(typeof(IfcSystemFurnitureElementTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcSystemFurnitureElement Parse(string strDef) { IfcSystemFurnitureElement e = new IfcSystemFurnitureElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType + "."; }
	}
	public partial class IfcSystemFurnitureElementType : IfcFurnishingElementType
	{
		internal IfcSystemFurnitureElementTypeEnum mPredefinedType = IfcSystemFurnitureElementTypeEnum.NOTDEFINED;//: OPTIONAL IfcSystemFurnitureElementTypeEnum
		internal IfcSystemFurnitureElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcSystemFurnitureElementType() : base() { }
		internal IfcSystemFurnitureElementType(IfcSystemFurnitureElementType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcSystemFurnitureElementType(DatabaseIfc db, string name, IfcSystemFurnitureElementTypeEnum type)
			: base(db,name)
		{
			mPredefinedType = type;
			if (mDatabase.mSchema == Schema.IFC2x3 && string.IsNullOrEmpty(ElementType) && type != IfcSystemFurnitureElementTypeEnum.NOTDEFINED)
				ElementType = type.ToString();
		}
		internal static void parseFields(IfcSystemFurnitureElementType t, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcFurnishingElementType.parseFields(t, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
			{
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					t.mPredefinedType = (IfcSystemFurnitureElementTypeEnum)Enum.Parse(typeof(IfcSystemFurnitureElementTypeEnum), s.Substring(1, s.Length - 2));
			}
		}
		internal static IfcSystemFurnitureElementType Parse(string strDef, Schema schema) { IfcSystemFurnitureElementType t = new IfcSystemFurnitureElementType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return t; }
	}
}
