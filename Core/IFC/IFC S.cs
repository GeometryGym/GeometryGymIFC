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
	public partial class IfcSanitaryTerminal : IfcFlowTerminal
	{
		internal IfcSanitaryTerminalTypeEnum mPredefinedType = IfcSanitaryTerminalTypeEnum.NOTDEFINED;// : OPTIONAL IfcSanitaryTerminalTypeEnum; 
		public IfcSanitaryTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSanitaryTerminal() : base() { }
		internal IfcSanitaryTerminal(DatabaseIfc db, IfcSanitaryTerminal t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcSanitaryTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcSanitaryTerminal t, List<string> arrFields, ref int ipos) { IfcFlowTerminal.parseFields(t, arrFields, ref ipos); string s = arrFields[ipos++]; if (s[0] == '.') t.mPredefinedType = (IfcSanitaryTerminalTypeEnum)Enum.Parse(typeof(IfcSanitaryTerminalTypeEnum), s.Substring(1, s.Length - 2)); }
		internal new static IfcSanitaryTerminal Parse(string strDef) { IfcSanitaryTerminal t = new IfcSanitaryTerminal(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcSanitaryTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcSanitaryTerminalType : IfcFlowTerminalType
	{
		internal IfcSanitaryTerminalTypeEnum mPredefinedType = IfcSanitaryTerminalTypeEnum.NOTDEFINED;// : IfcSanitaryTerminalTypeEnum; 
		public IfcSanitaryTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSanitaryTerminalType() : base() { }
		internal IfcSanitaryTerminalType(DatabaseIfc db, IfcSanitaryTerminalType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcSanitaryTerminalType(DatabaseIfc m, string name, IfcSanitaryTerminalTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcSanitaryTerminalType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSanitaryTerminalTypeEnum)Enum.Parse(typeof(IfcSanitaryTerminalTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSanitaryTerminalType Parse(string strDef) { IfcSanitaryTerminalType t = new IfcSanitaryTerminalType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcScheduleTimeControl : IfcControl // DEPRECEATED IFC4
	{
		internal int mActualStart, mEarlyStart, mLateStart, mScheduleStart, mActualFinish, mEarlyFinish, mLateFinish, mScheduleFinish;// OPTIONAL  IfcDateTimeSelect;
		internal double mScheduleDuration = double.NaN, mActualDuration = double.NaN, mRemainingTime = double.NaN, mFreeFloat = double.NaN, mTotalFloat = double.NaN;//	 OPTIONAL IfcTimeMeasure;
		internal bool mIsCritical;//	 :	OPTIONAL BOOLEAN;
		internal int mStatusTime;//	: 	OPTIONAL IfcDateTimeSelect
		internal double mStartFloat, mFinishFloat;//	 OPTIONAL IfcTimeMeasure; 
		internal double mCompletion;//	 :	OPTIONAL IfcPositiveRatioMeasure; 
		//INVERSE
		internal IfcRelAssignsTasks mScheduleTimeControlAssigned = null;//	 : 	IfcRelAssignsTasks FOR TimeForTask;

		public IfcDateTimeSelect ActualStart { get { return mDatabase[mActualStart] as IfcDateTimeSelect; } set { mActualStart = value == null ? 0 : value.Index;  } }
		public IfcDateTimeSelect EarlyStart { get { return mDatabase[mEarlyStart] as IfcDateTimeSelect; } set { mEarlyStart = value == null ? 0 : value.Index;  } }
		public IfcDateTimeSelect LateStart { get { return mDatabase[mLateStart] as IfcDateTimeSelect; } set { mLateStart = value == null ? 0 : value.Index;  } }
		public IfcDateTimeSelect ScheduleStart { get { return mDatabase[mScheduleStart] as IfcDateTimeSelect; } set { mScheduleStart = value == null ? 0 : value.Index;  } }
		public IfcDateTimeSelect ActualFinish { get { return mDatabase[mActualFinish] as IfcDateTimeSelect; } set { mActualFinish = value == null ? 0 : value.Index; } }
		public IfcDateTimeSelect EarlyFinish { get { return mDatabase[mEarlyFinish] as IfcDateTimeSelect; } set { mEarlyFinish = value == null ? 0 : value.Index; } }
		public IfcDateTimeSelect LateFinish { get { return mDatabase[mLateFinish] as IfcDateTimeSelect; } set { mLateFinish = value == null ? 0 : value.Index; } }
		public IfcDateTimeSelect ScheduleFinish { get { return mDatabase[mScheduleFinish] as IfcDateTimeSelect; } set { mScheduleFinish = value == null ? 0 : value.Index; } }
		public double ScheduleDuration { get { return mScheduleDuration; } set { mScheduleDuration = value; } }
		public double ActualDuration { get { return mActualDuration; } set { mActualDuration = value; } }
		public double RemainingTime { get { return mRemainingTime; } set { mRemainingTime = value; } }
		public double FreeFloat { get { return mFreeFloat; } set { mFreeFloat = value; } }
		public double TotalFloat { get { return mTotalFloat; } set { mTotalFloat = value; } }
		public bool IsCritical { get { return mIsCritical; } set { mIsCritical = value; } }
		public IfcDateTimeSelect StatusTime { get { return mDatabase[mStatusTime] as IfcDateTimeSelect; } set { mStatusTime = value == null ? 0 : value.Index; } }
		public double StartFloat { get { return mStartFloat; } set { mStartFloat = value; } }
		public double FinishFloat { get { return mFinishFloat; } set { mFinishFloat = value; } }
		public double Completion { get { return mCompletion; } set { mCompletion = value; } }

		internal IfcScheduleTimeControl() : base() { }
		internal IfcScheduleTimeControl(DatabaseIfc db, IfcScheduleTimeControl c) : base(db,c)
		{
			mActualStart = c.mActualStart; mEarlyStart = c.mEarlyStart; mLateStart = c.mLateStart; mScheduleStart = c.mScheduleStart;
			mActualFinish = c.mActualFinish; mEarlyFinish = c.mEarlyFinish; mLateFinish = c.mLateFinish; mScheduleFinish = c.mScheduleFinish;
			mScheduleDuration = c.mScheduleDuration; mActualDuration = c.mActualDuration; mRemainingTime = c.mRemainingTime; mFreeFloat = c.mFreeFloat;
			mTotalFloat = c.mTotalFloat; mIsCritical = c.mIsCritical; mStatusTime = c.mStatusTime; mStartFloat = c.mStartFloat; mFinishFloat = c.mFinishFloat; mCompletion = c.mCompletion;
		}
		public IfcScheduleTimeControl(DatabaseIfc db) : base (db) { }
		internal static IfcScheduleTimeControl Parse(string strDef, ReleaseVersion schema) { IfcScheduleTimeControl s = new IfcScheduleTimeControl(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return s; }
		internal static void parseFields(IfcScheduleTimeControl s, List<string> arrFields, ref int ipos, ReleaseVersion schema)
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
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mActualStart) + "," + ParserSTEP.LinkToString(mEarlyStart) + "," + ParserSTEP.LinkToString(mLateStart) + "," +
				ParserSTEP.LinkToString(mScheduleStart) + "," + ParserSTEP.LinkToString(mActualFinish) + "," + ParserSTEP.LinkToString(mEarlyFinish) + "," + ParserSTEP.LinkToString(mLateFinish) + "," + ParserSTEP.LinkToString(mScheduleFinish) + "," +
				ParserSTEP.DoubleOptionalToString(mScheduleDuration) + "," + ParserSTEP.DoubleOptionalToString(mActualDuration) + "," + ParserSTEP.DoubleOptionalToString(mRemainingTime) + "," + ParserSTEP.DoubleOptionalToString(mFreeFloat) + "," + ParserSTEP.DoubleOptionalToString(mTotalFloat) + "," + ParserSTEP.BoolToString(mIsCritical) + "," + ParserSTEP.LinkToString(mStatusTime) + "," +
				ParserSTEP.DoubleOptionalToString(mStartFloat) + "," + ParserSTEP.DoubleOptionalToString(mFinishFloat) + "," + ParserSTEP.DoubleOptionalToString(mCompletion); //(mScheduleTimeControlAssigned == null ? "" :
		}
		internal DateTime getActualStart() { IfcDateTimeSelect dts = ActualStart; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal DateTime getActualFinish() { IfcDateTimeSelect dts = ActualFinish; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal DateTime getScheduleStart() { IfcDateTimeSelect dts = ScheduleStart; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal DateTime getScheduleFinish() { IfcDateTimeSelect dts = ScheduleFinish; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal TimeSpan getScheduleDuration() { return new TimeSpan(0, 0, (int)mScheduleDuration); }
	}
	public abstract partial class IfcSchedulingTime : BaseClassIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcEventTime, IfcLagTime, IfcResourceTime, IfcTaskTime, IfcWorkTime));
	{
		internal string mName = "$";//	 :	OPTIONAL IfcLabel;
		internal IfcDataOriginEnum mDataOrigin = IfcDataOriginEnum.NOTDEFINED;// OPTIONAL : IfcDataOriginEnum;
		internal string mUserDefinedDataOrigin = "$";//:	OPTIONAL IfcLabel; 

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } } 
		public IfcDataOriginEnum DataOrigin { get { return mDataOrigin; } set { mDataOrigin = value; } }
		public string UserDefinedDataOrigin { get { return (mUserDefinedDataOrigin == "$" ? "" : ParserIfc.Decode(mName)); } set { mUserDefinedDataOrigin = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcSchedulingTime() : base() { }
		protected IfcSchedulingTime(DatabaseIfc db, IfcSchedulingTime t) : base(db,t) { mName = t.mName; mDataOrigin = t.mDataOrigin; mUserDefinedDataOrigin = t.mUserDefinedDataOrigin; }
		protected IfcSchedulingTime(DatabaseIfc db) : base(db) { }
		internal static void parseFields(IfcSchedulingTime p, List<string> arrFields, ref int ipos)
		{
			p.mName = arrFields[ipos++];
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				p.mDataOrigin = (IfcDataOriginEnum)Enum.Parse(typeof(IfcDataOriginEnum), s.Replace(".", ""));
			p.mUserDefinedDataOrigin = arrFields[ipos++];
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mName == "$" ? ",$,." : ",'" + mName + "',.") + mDataOrigin.ToString() + (mUserDefinedDataOrigin == "$" ? ".,$" : ".,'" + mUserDefinedDataOrigin + "'"); }
	}
	public partial class IfcSeamCurve : IfcSurfaceCurve //IFC4 Add2
	{
		internal IfcSeamCurve() : base() { }
		internal IfcSeamCurve(DatabaseIfc db, IfcIntersectionCurve c) : base(db, c) { }
		internal IfcSeamCurve(IfcCurve curve, IfcPCurve p1, IfcPCurve p2, IfcPreferredSurfaceCurveRepresentation cr) : base(curve, p1, p2, cr) { }
		internal new static void parseFields(IfcSurfaceCurve c, List<string> arrFields, ref int ipos)
		{
			IfcSurfaceCurve.parseFields(c, arrFields, ref ipos);
		}
		internal new static IfcSeamCurve Parse(string strDef) { IfcSeamCurve c = new IfcSeamCurve(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
	}
	public partial class IfcSectionedSpine : IfcGeometricRepresentationItem
	{
		internal int mSpineCurve;// : IfcCompositeCurve;
		internal List<int> mCrossSections = new List<int>();// : LIST [2:?] OF IfcProfileDef;
		internal List<int> mCrossSectionPositions = new List<int>();// : LIST [2:?] OF IfcAxis2Placement3D; 

		public IfcCompositeCurve SpineCurve { get { return mDatabase[mSpineCurve] as IfcCompositeCurve; } set { mSpineCurve = value.mIndex; } }
		public List<IfcProfileDef> CrossSections { get { return mCrossSections.ConvertAll(x => mDatabase[x] as IfcProfileDef); } set { mCrossSections = value.ConvertAll(x => x.mIndex); } }
		public List<IfcAxis2Placement3D> CrossSectionPositions { get { return mCrossSectionPositions.ConvertAll(x => mDatabase[x] as IfcAxis2Placement3D); } set { mCrossSectionPositions = value.ConvertAll(x => x.mIndex); } }

		internal IfcSectionedSpine() : base() { }
		internal IfcSectionedSpine(DatabaseIfc db, IfcSectionedSpine s) : base(db,s) { SpineCurve = db.Factory.Duplicate(s.SpineCurve) as IfcCompositeCurve; CrossSections = s.CrossSections.ConvertAll(x => db.Factory.Duplicate(x) as IfcProfileDef); CrossSectionPositions = s.CrossSectionPositions.ConvertAll(x=>db.Factory.Duplicate(x) as IfcAxis2Placement3D); }
		internal static void parseFields(IfcSectionedSpine s, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(s, arrFields, ref ipos); s.mSpineCurve = ParserSTEP.ParseLink(arrFields[ipos++]); s.mCrossSections = ParserSTEP.SplitListLinks(arrFields[ipos++]); s.mCrossSectionPositions = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcSectionedSpine Parse(string strDef) { IfcSectionedSpine s = new IfcSectionedSpine(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mSpineCurve) + ",("
				+ ParserSTEP.LinkToString(mCrossSections[0]);
			for (int icounter = 1; icounter < mCrossSections.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mCrossSections[icounter]);
			str += "),(" + ParserSTEP.LinkToString(mCrossSectionPositions[0]);
			for (int icounter = 1; icounter < mCrossSectionPositions.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mCrossSectionPositions[icounter]);
			return str + ")";
		}
	} 
	public partial class IfcSectionProperties : IfcPreDefinedProperties // IFC2x3 STPEntity
	{
		internal IfcSectionTypeEnum mSectionType;// : IfcSectionTypeEnum;
		internal int mStartProfile;// IfcProfileDef;
		internal int mEndProfile;// : OPTIONAL IfcProfileDef;
		internal IfcSectionProperties() : base() { }
		internal IfcSectionProperties(DatabaseIfc db, IfcSectionProperties p) : base(db,p) { mSectionType = p.mSectionType; mStartProfile = db.Factory.Duplicate(p.mDatabase[p.mStartProfile]).mIndex; mEndProfile = db.Factory.Duplicate(p.mDatabase[p.mEndProfile]).mIndex; }
		internal static IfcSectionProperties Parse(string strDef) { IfcSectionProperties p = new IfcSectionProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcSectionProperties p, List<string> arrFields, ref int ipos) { p.mSectionType = (IfcSectionTypeEnum)Enum.Parse(typeof(IfcSectionTypeEnum), arrFields[ipos++].Replace(".", "")); p.mStartProfile = ParserSTEP.ParseLink(arrFields[ipos++]); p.mEndProfile = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mSectionType.ToString() + ".," + ParserSTEP.LinkToString(mStartProfile) + "," + ParserSTEP.LinkToString(mEndProfile); }
	}
	public partial class IfcSectionReinforcementProperties : IfcPreDefinedProperties // IFC2x3 STPEntity
	{
		internal double mLongitudinalStartPosition;//	:	IfcLengthMeasure;
		internal double mLongitudinalEndPosition;//	:	IfcLengthMeasure;
		internal double mTransversePosition = 0;//	:	OPTIONAL IfcLengthMeasure;
		internal IfcReinforcingBarRoleEnum mReinforcementRole = IfcReinforcingBarRoleEnum.NOTDEFINED;//	:	IfcReinforcingBarRoleEnum;
		internal int mSectionDefinition;//	:	IfcSectionProperties;
		internal List<int> mCrossSectionReinforcementDefinitions = new List<int>();//	:	SET [1:?] OF IfcReinforcementBarProperties;
		internal IfcSectionReinforcementProperties() : base() { }
		internal IfcSectionReinforcementProperties(DatabaseIfc db, IfcSectionReinforcementProperties p) : base(db,p)
		{
			mLongitudinalStartPosition = p.mLongitudinalStartPosition;
			mLongitudinalEndPosition = p.mLongitudinalEndPosition;
			mTransversePosition = p.mTransversePosition;
			mReinforcementRole = p.mReinforcementRole;
			mSectionDefinition = db.Factory.Duplicate(p.mDatabase[p.mSectionDefinition]).mIndex;
			mCrossSectionReinforcementDefinitions = p.mCrossSectionReinforcementDefinitions.ConvertAll(x=>db.Factory.Duplicate( p.mDatabase[x]).mIndex);
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
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mLongitudinalStartPosition) + "," + ParserSTEP.DoubleToString(mLongitudinalEndPosition) + "," +
			ParserSTEP.DoubleOptionalToString(mTransversePosition) + ",." + mReinforcementRole.ToString() + ".," + ParserSTEP.LinkToString(mSectionDefinition) + ",(" + ParserSTEP.LinkToString(mCrossSectionReinforcementDefinitions[0]);
			for (int icounter = 1; icounter < mCrossSectionReinforcementDefinitions.Count; icounter++)
				result += ",#" + mCrossSectionReinforcementDefinitions;
			return result + ")";
		}
	}
	public interface IfcSegmentIndexSelect { } //SELECT ( IfcLineIndex, IfcArcIndex);
	public partial class IfcSensor : IfcDistributionControlElement //IFC4  
	{
		internal IfcSensorTypeEnum mPredefinedType = IfcSensorTypeEnum.NOTDEFINED;
		public IfcSensorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcSensor() : base() { }
		internal IfcSensor(DatabaseIfc db, IfcSensor s) : base(db, s) { mPredefinedType = s.mPredefinedType; }
		public IfcSensor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcSensor a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcSensorTypeEnum)Enum.Parse(typeof(IfcSensorTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcSensor Parse(string strDef) { IfcSensor d = new IfcSensor(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcSensorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcSensorType : IfcDistributionControlElementType
	{
		internal IfcSensorTypeEnum mPredefinedType = IfcSensorTypeEnum.NOTDEFINED;// : IfcSensorTypeEnum; 
		public IfcSensorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSensorType() : base() { }
		internal IfcSensorType(DatabaseIfc db, IfcSensorType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcSensorType(DatabaseIfc m, string name, IfcSensorTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcSensorType t, List<string> arrFields, ref int ipos) { IfcDistributionControlElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSensorTypeEnum)Enum.Parse(typeof(IfcSensorTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSensorType Parse(string strDef) { IfcSensorType t = new IfcSensorType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	//ENTITY IfcServiceLife // DEPRECEATED IFC4
	//ENTITY IfcServiceLifeFactor // DEPRECEATED IFC4
	public partial class IfcShadingDevice : IfcBuildingElement
	{
		internal IfcShadingDeviceTypeEnum mPredefinedType = IfcShadingDeviceTypeEnum.NOTDEFINED;//: OPTIONAL IfcShadingDeviceTypeEnum; 
		public IfcShadingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcShadingDevice() : base() { }
		internal IfcShadingDevice(DatabaseIfc db, IfcShadingDevice d) : base(db, d) { mPredefinedType = d.mPredefinedType; }
		public IfcShadingDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcShadingDevice Parse(string strDef, ReleaseVersion schema) { IfcShadingDevice w = new IfcShadingDevice(); int ipos = 0; parseFields(w, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return w; }
		internal static void parseFields(IfcShadingDevice w, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcBuildingElement.parseFields(w, arrFields, ref ipos);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					w.mPredefinedType = (IfcShadingDeviceTypeEnum)Enum.Parse(typeof(IfcShadingDeviceTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcShadingDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcShadingDeviceType : IfcBuildingElementType
	{
		internal IfcShadingDeviceTypeEnum mPredefinedType = IfcShadingDeviceTypeEnum.NOTDEFINED;
		public IfcShadingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcShadingDeviceType() : base() { }
		internal IfcShadingDeviceType(DatabaseIfc db, IfcShadingDeviceType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcShadingDeviceType(DatabaseIfc m, string name, IfcShadingDeviceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcShadingDeviceType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcShadingDeviceTypeEnum)Enum.Parse(typeof(IfcShadingDeviceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcShadingDeviceType Parse(string strDef) { IfcShadingDeviceType t = new IfcShadingDeviceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcShapeAspect : BaseClassIfc
	{
		internal List<int> mShapeRepresentations = new List<int>();// : LIST [1:?] OF IfcShapeModel;
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		private IfcLogicalEnum mProductDefinitional;// : LOGICAL;
		internal int mPartOfProductDefinitionShape;// IFC4 OPTIONAL IfcProductRepresentationSelect IFC2x3 IfcProductDefinitionShape;

		public List<IfcShapeModel> ShapeRepresentations { get { return mShapeRepresentations.ConvertAll(x => mDatabase[x] as IfcShapeModel); } set { mShapeRepresentations = value.ConvertAll(x => x.mIndex); for (int icounter = 0; icounter < value.Count; icounter++) value[icounter].mOfShapeAspect = this; } }
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcLogicalEnum ProductDefinitional { get { return mProductDefinitional; } set { mProductDefinitional = value; } }
		public IfcProductRepresentationSelect PartOfProductDefinitionShape { get { return mDatabase[mPartOfProductDefinitionShape] as IfcProductRepresentationSelect; } set { mPartOfProductDefinitionShape = value == null ? 0 : value.Index; if (value != null) value.AddShapeAspect(this); } }

		internal IfcShapeAspect() : base() { }
		internal IfcShapeAspect(DatabaseIfc db, IfcShapeAspect a) : base(db,a)
		{
			ShapeRepresentations = a.ShapeRepresentations.ConvertAll(x=>db.Factory.Duplicate(x) as IfcShapeModel);
			mName = a.mName;
			mDescription = a.mDescription;
			mProductDefinitional = a.mProductDefinitional;
			if(a.mPartOfProductDefinitionShape > 0)
				PartOfProductDefinitionShape = db.Factory.Duplicate(a.mDatabase[a.mPartOfProductDefinitionShape]) as IfcProductRepresentationSelect;
		}
		public IfcShapeAspect(IfcShapeModel shape) : base(shape.mDatabase) { mShapeRepresentations.Add(shape.mIndex); shape.mOfShapeAspect = this;  }
		public IfcShapeAspect(List<IfcShapeModel> reps, string name, string desc, IfcProductRepresentationSelect pr)
			: this(reps[0].mDatabase, name, desc, pr) { ShapeRepresentations = reps; }
		public IfcShapeAspect(IfcShapeModel rep, string name, string desc, IfcProductRepresentationSelect pr)
			: this(rep.mDatabase, name, desc, pr) { mShapeRepresentations.Add(rep.mIndex); rep.mOfShapeAspect = this; }
		private IfcShapeAspect(DatabaseIfc db, string name, string desc, IfcProductRepresentationSelect pr) : base(db)
		{
			Name = name;
			Description = desc;
			PartOfProductDefinitionShape = pr;
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
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mShapeRepresentations[0]);
			for (int icounter = 1; icounter < mShapeRepresentations.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mShapeRepresentations[icounter]);
			return str + (mName == "$" ? "),$," : "),'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserIfc.LogicalToString(mProductDefinitional)
				+ "," + ParserSTEP.LinkToString(mPartOfProductDefinitionShape);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			List<IfcShapeModel> sms = ShapeRepresentations;
			for (int icounter = 0; icounter < sms.Count; icounter++)
				sms[icounter].mOfShapeAspect = this;

			IfcProductRepresentationSelect pds = PartOfProductDefinitionShape;
			if (pds != null)
				pds.HasShapeAspects.Add(this);
		}
	}
	public abstract partial class IfcShapeModel : IfcRepresentation//ABSTRACT SUPERTYPE OF (ONEOF (IfcShapeRepresentation,IfcTopologyRepresentation))
	{
		//INVERSE
		internal IfcShapeAspect mOfShapeAspect = null; //:	SET [0:1] OF IfcShapeAspect FOR ShapeRepresentations;

		protected IfcShapeModel() : base() { }
		protected IfcShapeModel(IfcRepresentationItem ri, string identifier, string repType) : base(ri, identifier, repType)
		{
			ContextOfItems = mDatabase.Factory.SubContext(string.Compare(identifier, "Axis", true) == 0 ? FactoryIfc.SubContextIdentifier.Axis : FactoryIfc.SubContextIdentifier.Body);
		}
		protected IfcShapeModel(List<IfcRepresentationItem> reps, string identifier, string repType) : base(reps, identifier, repType)
		{
			ContextOfItems = mDatabase.Factory.SubContext(string.Compare(identifier, "Axis", true) == 0 ? FactoryIfc.SubContextIdentifier.Axis : FactoryIfc.SubContextIdentifier.Body);
		}
		protected IfcShapeModel(DatabaseIfc db, IfcShapeModel m) : base(db, m)
		{
#warning todo
			//internal IfcShapeAspect mOfShapeAspect = null; //:	SET [0:1] OF IfcShapeAspect FOR ShapeRepresentations;
		}
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
		internal IfcShapeRepresentation(DatabaseIfc db, IfcShapeRepresentation r) : base(db, r) { }
		public IfcShapeRepresentation(IfcGeometricRepresentationItem representation) : base(representation, "Body", "") { setIdentifiers(representation); }
		public IfcShapeRepresentation(List<IfcRepresentationItem> items) : base(items, "Body", "") { setIdentifiers(items[0]); }
		public IfcShapeRepresentation(IfcAdvancedBrep ab) : base(ab, "Body", "AdvancedBrep") { }
		public IfcShapeRepresentation(IfcBooleanResult br) : base(br, "Body", "CSG")
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
				IfcProfileDef pd = mDatabase[ss.mCrossSections[0]] as IfcProfileDef;
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
		public IfcShapeRepresentation(IfcGeometricRepresentationItem rep, string identifier, string repType) : base(rep, identifier, repType) { }
		public IfcShapeRepresentation(List<IfcGeometricRepresentationItem> reps, string identifier, string repType) : base(reps.ConvertAll(x=>x as IfcRepresentationItem), identifier, repType) { }
		public IfcShapeRepresentation(IfcTopologicalRepresentationItem rep, string identifier, string repType) : base(rep, identifier, repType) { }
		internal new static IfcShapeRepresentation Parse(string strDef)
		{
			IfcShapeRepresentation r = new IfcShapeRepresentation();
			int pos = 0;
			IfcShapeModel.parseString(r, strDef, ref pos);
			return r;
		}
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

		public static IfcShapeRepresentation GetAxisRep(IfcBoundedCurve c) { return new IfcShapeRepresentation(c, "Axis", "Curve3D"); }
		internal static IfcShapeRepresentation getProfileRep(DatabaseIfc m, IfcCurve c) { return new IfcShapeRepresentation(c, "Profile","Curve3D" ); }
		internal static IfcShapeRepresentation getRowRep(DatabaseIfc m, IfcGeometricCurveSet cs) { return new IfcShapeRepresentation(cs, "Row", "GeometricCurveSet" ); }

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

		internal override void changeSchema(ReleaseVersion schema)
		{
			List<IfcRepresentationItem> items = Items;
			for (int jcounter = 0; jcounter < items.Count; jcounter++)
			{
				if (schema != ReleaseVersion.IFC2x3)
				{
					IfcRepresentationItem repItem = items[jcounter];
					IfcFacetedBrep fb = repItem as IfcFacetedBrep;
					if (fb != null)
					{
						IfcConnectedFaceSet cfs = fb.Outer;
#if (RHCOMM)
						IfcTriangulatedFaceSet tfs = cfs.Convert();
						if (tfs != null)
						{
							int index = tfs.mIndex;
							tfs.mIndex = fb.mIndex;
							mDatabase[tfs.mIndex] = tfs;
							mDatabase[index] = null;
							cfs.Destruct(true);
							setIdentifiers(tfs);
							continue;
						}
#endif
					}
					else
					{
						IfcFaceBasedSurfaceModel fbsm = repItem as IfcFaceBasedSurfaceModel;
						if (fbsm != null)
						{
							List<IfcConnectedFaceSet> faces = fbsm.FbsmFaces;
							if (faces.Count == 1)
							{
#if (RHCOMM)
								IfcTriangulatedFaceSet tfs = faces[0].Convert();
								if (tfs != null)
								{
									int index = tfs.mIndex;
									tfs.mIndex = faces[0].mIndex;
									mDatabase[tfs.mIndex] = tfs;
									mDatabase[index] = null;
									fbsm.Destruct(true);
									setIdentifiers(tfs);
									continue;
								}
#endif
							}
						}
					}
				}
				items[jcounter].changeSchema(schema);
			}
			base.changeSchema(schema);
		}
	}
	public partial interface IfcShell : IBaseClassIfc  // SELECT(IfcClosedShell, IfcOpenShell);
	{
		List<IfcFace> CfsFaces { get; }
	}
	public partial class IfcShellBasedSurfaceModel : IfcGeometricRepresentationItem
	{
		internal List<int> mSbsmBoundary = new List<int>();// : SET [1:?] OF IfcShell; 
		public List<IfcShell> SbsmBoundary { get { return mSbsmBoundary.ConvertAll(x => mDatabase[x] as IfcShell); } set { mSbsmBoundary = value.ConvertAll(x => x.Index); } }

		internal IfcShellBasedSurfaceModel() : base() { }
		internal IfcShellBasedSurfaceModel(DatabaseIfc db, IfcShellBasedSurfaceModel m) : base(db,m) { SbsmBoundary = m.mSbsmBoundary.ConvertAll(x=> db.Factory.Duplicate(m.mDatabase[x]) as IfcShell); }
		public IfcShellBasedSurfaceModel(IfcShell shell) : base(shell.Database) { mSbsmBoundary.Add(shell.Index); }
		public IfcShellBasedSurfaceModel(List<IfcShell> shells) : base(shells[0].Database) { mSbsmBoundary = shells.ConvertAll(x => x.Index); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mSbsmBoundary[0]);
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
		protected IfcSimpleProperty(DatabaseIfc db, IfcSimpleProperty p) : base(db, p) { }
		protected IfcSimpleProperty(DatabaseIfc m, string name, string desc) : base(m, name, desc) { }
		protected static void parseFields(IfcSimpleProperty p, List<string> arrFields, ref int ipos) { IfcProperty.parseFields(p, arrFields, ref ipos); }
	}
	public partial class IfcSimplePropertyTemplate : IfcPropertyTemplate
	{
		private IfcSimplePropertyTemplateTypeEnum mTemplateType = IfcSimplePropertyTemplateTypeEnum.NOTDEFINED;// : OPTIONAL IfcSimplePropertyTemplateTypeEnum;
		internal string mPrimaryMeasureType = "$";// : OPTIONAL IfcLabel;
		internal string mSecondaryMeasureType = "$";// : OPTIONAL IfcLabel;
		internal int mEnumerators = 0;// : OPTIONAL IfcPropertyEnumeration;
		internal int mPrimaryUnit = 0, mSecondaryUnit = 0;// : OPTIONAL IfcUnit; 
		internal string mExpression = "$";// : OPTIONAL IfcLabel;
		internal IfcStateEnum mAccessState = IfcStateEnum.NA;//	:	OPTIONAL IfcStateEnum;

		public IfcSimplePropertyTemplateTypeEnum TemplateType { get { return mTemplateType; } set { mTemplateType = value; } }
		public string PrimaryMeasureType { get { return (mPrimaryMeasureType == "$" ? "" : ParserIfc.Decode(mPrimaryMeasureType)); } set { mPrimaryMeasureType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string SecondaryMeasureType { get { return (mSecondaryMeasureType == "$" ? "" : ParserIfc.Decode(mSecondaryMeasureType)); } set { mSecondaryMeasureType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcPropertyEnumeration Enumerators { get { return mDatabase[mEnumerators] as IfcPropertyEnumeration; } set { mEnumerators = (value == null ? 0 : value.Index); } }
		public IfcUnit PrimaryUnit { get { return mDatabase[mPrimaryUnit] as IfcUnit; } set { mPrimaryUnit = (value == null ? 0 : value.Index); } }
		public IfcUnit SecondaryUnit { get { return mDatabase[mSecondaryUnit] as IfcUnit; } set { mSecondaryUnit = (value == null ? 0 : value.Index); } }
		public string Expression { get { return (mExpression == "$" ? "" : ParserIfc.Decode(mExpression)); } set { mExpression = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcStateEnum AccessState { get { return mAccessState; } set { mAccessState = value; } }

		internal IfcSimplePropertyTemplate() : base() { }
		internal IfcSimplePropertyTemplate(DatabaseIfc db, IfcSimplePropertyTemplate s) : base(db, s)
		{
			mTemplateType = s.mTemplateType;
			mPrimaryMeasureType = s.mPrimaryMeasureType;
			mSecondaryMeasureType = s.mSecondaryMeasureType;
			if (s.mEnumerators > 0)
				Enumerators = db.Factory.Duplicate(s.Enumerators) as IfcPropertyEnumeration;	
			if (s.mPrimaryUnit > 0)
				PrimaryUnit = db.Factory.Duplicate(s.mDatabase[s.mPrimaryUnit]) as IfcUnit;
			if (s.mSecondaryUnit > 0)
				SecondaryUnit = db.Factory.Duplicate(s.mDatabase[s.mSecondaryUnit]) as IfcUnit;
			mExpression = s.mExpression;
			mAccessState = s.mAccessState;
		}
		public IfcSimplePropertyTemplate(DatabaseIfc db, string name) : base(db,name) { }
		internal static void parseFields(IfcSimplePropertyTemplate s, List<string> arrFields, ref int ipos)
		{
			IfcPropertyTemplate.parseFields(s, arrFields, ref ipos);
			if (!Enum.TryParse<IfcSimplePropertyTemplateTypeEnum>(arrFields[ipos++].Replace(".", ""),true, out s.mTemplateType))
				s.mTemplateType = IfcSimplePropertyTemplateTypeEnum.NOTDEFINED;
			s.mPrimaryMeasureType = arrFields[ipos++].Replace("'", ""); ;
			s.mSecondaryMeasureType = arrFields[ipos++].Replace("'", ""); ;
			s.mEnumerators = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mPrimaryUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mSecondaryUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mExpression = arrFields[ipos++].Replace("'", "");
			if (!Enum.TryParse<IfcStateEnum>(arrFields[ipos++].Replace(".", ""), out s.mAccessState))
				s.mAccessState = IfcStateEnum.NA;
		}
		protected override string BuildStringSTEP()
		{
			if (mDatabase.Release == ReleaseVersion.IFC2x3)
				return "";
			return base.BuildStringSTEP() + (mTemplateType == IfcSimplePropertyTemplateTypeEnum.NOTDEFINED ? ",$," : ",." + mTemplateType.ToString() + ".,") + 
				(mPrimaryMeasureType == "$" ? "$," : "'" + mPrimaryMeasureType + "',") + (mSecondaryMeasureType == "$" ? "$," : "'" + mSecondaryMeasureType + "',") + 
				ParserSTEP.LinkToString(mEnumerators) + "," + ParserSTEP.LinkToString(mPrimaryUnit) + "," + ParserSTEP.LinkToString(mSecondaryUnit) + "," +
				(mExpression == "$" ? "$," : "'" + mExpression + "',") + (mAccessState == IfcStateEnum.NA ? "$" : "." + mAccessState.ToString() + ".");
		}
		internal static IfcSimplePropertyTemplate Parse(string strDef) { IfcSimplePropertyTemplate s = new IfcSimplePropertyTemplate(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public partial class IfcSite : IfcSpatialStructureElement
	{
		internal string mRefLatitude = "$";// : OPTIONAL IfcCompoundPlaneAngleMeasure;
		internal string mRefLongitude = "$";// : OPTIONAL IfcCompoundPlaneAngleMeasure;
		internal double mRefElevation = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal string mLandTitleNumber = "$";// : OPTIONAL IfcLabel;
		internal int mSiteAddress;// : OPTIONAL IfcPostalAddress; 

		public IfcCompoundPlaneAngleMeasure RefLatitude { set { mRefLatitude = (value == null ? "$" : value.ToString()); } }
		public IfcCompoundPlaneAngleMeasure RefLongitude { set { mRefLongitude = (value == null ? "$" : value.ToString()); } }
		public double RefElevation { get { return mRefElevation; } set { mRefElevation = value; } }
		public string LandTitleNumber { get { return (mLandTitleNumber == "$" ? "" : ParserIfc.Decode(mLandTitleNumber)); } set { mLandTitleNumber = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcPostalAddress SiteAddress { get { return mDatabase[mSiteAddress] as IfcPostalAddress; } set { mSiteAddress = (value == null ? 0 : value.mIndex); } }

		internal IfcSite() : base() { }
		internal IfcSite(DatabaseIfc db, IfcSite site,bool downStream) :base(db, site, downStream) { mRefLatitude = site.mRefLatitude; mRefLongitude = site.mRefLongitude; mRefElevation = site.mRefElevation; mLandTitleNumber = site.mLandTitleNumber; if (site.mSiteAddress > 0) SiteAddress = db.Factory.Duplicate(site.SiteAddress) as IfcPostalAddress; }
		public IfcSite(DatabaseIfc db, string name) : base(new IfcLocalPlacement(db.Factory.PlaneXYPlacement)) { Name = name; }
		public IfcSite(IfcSite host, string name) : base(host, name) { }
		internal static void parseFields(IfcSite s, List<string> arrFields, ref int ipos)
		{
			IfcSpatialStructureElement.parseFields(s, arrFields, ref ipos);
			s.mRefLatitude = arrFields[ipos++];
			s.mRefLongitude = arrFields[ipos++];
			s.mRefElevation = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mLandTitleNumber = arrFields[ipos++];
			s.mSiteAddress = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mRefLatitude + "," + mRefLongitude + "," + ParserSTEP.DoubleOptionalToString(mRefElevation) + "," + mLandTitleNumber + "," + ParserSTEP.LinkToString(mSiteAddress); }
		internal static IfcSite Parse(string strDef) { IfcSite s = new IfcSite(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public partial class IfcSIUnit : IfcNamedUnit
	{
		private static double[] mFactors = new double[] { Math.Pow(10, 18), Math.Pow(10, 15), Math.Pow(10, 12), Math.Pow(10, 9), Math.Pow(10, 6), Math.Pow(10, 3), Math.Pow(10, 2), Math.Pow(10, 1), Math.Pow(10, -1), Math.Pow(10, -2), Math.Pow(10, -3), Math.Pow(10, -6), Math.Pow(10, -9), Math.Pow(10, -12), Math.Pow(10, -15), Math.Pow(10, -18), 1 };
		private IfcSIPrefix mPrefix = IfcSIPrefix.NONE;// : OPTIONAL IfcSIPrefix;
		private IfcSIUnitName mName;// : IfcSIUnitName; 

		public IfcSIPrefix Prefix { get { return mPrefix; } }
		public new IfcSIUnitName Name { get { return mName; } set { mName = value; } }

		internal IfcSIUnit() : base() { }
		internal IfcSIUnit(DatabaseIfc db, IfcSIUnit u) : base(db, u) { mPrefix = u.mPrefix; mName = u.mName; }
		public IfcSIUnit(DatabaseIfc m, IfcUnitEnum unitEnum, IfcSIPrefix pref, IfcSIUnitName name) : base(m, unitEnum, false) { mPrefix = pref; mName = name; }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP();
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
			u.mPrefix = (str.Contains("$") ? IfcSIPrefix.NONE : (IfcSIPrefix)Enum.Parse(typeof(IfcSIPrefix), str.Replace(".", "")));
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
		internal IfcSlab(DatabaseIfc db, IfcSlab s) : base(db, s) { mPredefinedType = s.mPredefinedType; }
		public IfcSlab(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcSlab Parse(string strDef) { IfcSlab s = new IfcSlab(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcSlab s, List<string> arrFields, ref int ipos) { IfcBuildingElement.parseFields(s, arrFields, ref ipos); string str = arrFields[ipos++]; if (str != "$") s.mPredefinedType = (IfcSlabTypeEnum)Enum.Parse(typeof(IfcSlabTypeEnum), str.Replace(".", "")); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mPredefinedType == IfcSlabTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcSlabStandardCase : IfcSlab
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mDatabase.mModelView == ModelView.Ifc4Reference ? "IfcSlab" : base.KeyWord); } }
		internal IfcSlabStandardCase() : base() { }
		internal IfcSlabStandardCase(DatabaseIfc db, IfcSlabStandardCase s) : base(db, s) { }
		internal new static IfcSlabStandardCase Parse(string strDef) { IfcSlabStandardCase s = new IfcSlabStandardCase(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcSlabStandardCase s, List<string> arrFields, ref int ipos) { IfcSlab.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcSlabType : IfcBuildingElementType
	{
		internal IfcSlabTypeEnum mPredefinedType = IfcSlabTypeEnum.NOTDEFINED;
		public IfcSlabTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcSlabType() : base() { }
		public IfcSlabType(DatabaseIfc m, string name, IfcSlabTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal IfcSlabType(DatabaseIfc db, IfcSlabType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcSlabType(string name, IfcMaterialLayerSet ls, IfcSlabTypeEnum type) : base(ls.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = ls; }
		internal static void parseFields(IfcSlabType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSlabTypeEnum)Enum.Parse(typeof(IfcSlabTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSlabType Parse(string strDef) { IfcSlabType t = new IfcSlabType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	//ENTITY IfcSlippageConnectionCondition
	public partial class IfcSolarDevice : IfcEnergyConversionDevice //IFC4
	{
		internal IfcSolarDeviceTypeEnum mPredefinedType = IfcSolarDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcSolarDeviceTypeEnum;
		public IfcSolarDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSolarDevice() : base() { }
		internal IfcSolarDevice(DatabaseIfc db, IfcSolarDevice d) : base(db, d) { mPredefinedType = d.mPredefinedType; }
		public IfcSolarDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcSolarDevice s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcSolarDeviceTypeEnum)Enum.Parse(typeof(IfcSolarDeviceTypeEnum), str);
		}
		internal new static IfcSolarDevice Parse(string strDef) { IfcSolarDevice s = new IfcSolarDevice(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcSolarDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcSolarDeviceType : IfcEnergyConversionDeviceType
	{
		internal IfcSolarDeviceTypeEnum mPredefinedType = IfcSolarDeviceTypeEnum.NOTDEFINED;// : IfcSolarDeviceTypeEnum; 
		public IfcSolarDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSolarDeviceType() : base() { }
		internal IfcSolarDeviceType(DatabaseIfc db, IfcSolarDeviceType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcSolarDeviceType(DatabaseIfc m, string name, IfcSolarDeviceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcSolarDeviceType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSolarDeviceTypeEnum)Enum.Parse(typeof(IfcSolarDeviceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSolarDeviceType Parse(string strDef) { IfcSolarDeviceType t = new IfcSolarDeviceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract partial class IfcSolidModel : IfcGeometricRepresentationItem, IfcBooleanOperand /* ABSTRACT SUPERTYPE OF (ONEOF(IfcCsgSolid ,IfcManifoldSolidBrep,IfcSweptAreaSolid,IfcSweptDiskSolid))*/
	{
		protected IfcSolidModel() : base() { }
		protected IfcSolidModel(DatabaseIfc db) : base(db) { }
		protected IfcSolidModel(DatabaseIfc db, IfcSolidModel p) : base(db,p) { }
		internal static void parseFields(IfcSolidModel s, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcSoundProperties : IfcPropertySetDefinition // DEPRECEATED IFC4
	{
		internal bool mIsAttenuating;// : IfcBoolean;
		internal IfcSoundScaleEnum mSoundScale = IfcSoundScaleEnum.NOTDEFINED;// : OPTIONAL IfcSoundScaleEnum
		internal List<int> mSoundValues = new List<int>(1);// : LIST [1:8] OF IfcSoundValue;

		public List<IfcSoundValue> SoundValues { get { return mSoundValues.ConvertAll(x => mDatabase[x] as IfcSoundValue); } set { mSoundValues = value.ConvertAll(x => x.mIndex); } }

		internal IfcSoundProperties() : base() { }
		internal IfcSoundProperties(DatabaseIfc db, IfcSoundProperties p) : base(db, p)
		{
			mIsAttenuating = p.mIsAttenuating;
			mSoundScale = p.mSoundScale;
			SoundValues = p.SoundValues.ConvertAll(x => db.Factory.Duplicate(x) as IfcSoundValue);
		}
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
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.BoolToString(mIsAttenuating) + ",." + mSoundScale.ToString() + ".,(" + ParserSTEP.LinkToString(mSoundValues[0]);
			for (int icounter = 1; icounter < mSoundValues.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSoundValues[icounter]);
			return str + ")";
		}
	}
	public partial class IfcSoundValue : IfcPropertySetDefinition // DEPRECEATED IFC4
	{
		internal int mSoundLevelTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal double mFrequency;// : IfcFrequencyMeasure;
		internal double mSoundLevelSingleValue;// : OPTIONAL IfcDerivedMeasureValue; 

		public IfcTimeSeries SoundLevelTimeSeries { get { return mDatabase[mSoundLevelTimeSeries] as IfcTimeSeries; } set { mSoundLevelTimeSeries = (value == null ? 0 : value.mIndex); } }

		internal IfcSoundValue() : base() { }
		internal IfcSoundValue(DatabaseIfc db, IfcSoundValue v) : base(db, v)
		{
			if (v.mSoundLevelTimeSeries > 0)
				SoundLevelTimeSeries = db.Factory.Duplicate(v.SoundLevelTimeSeries) as IfcTimeSeries;
			mFrequency = v.mFrequency;
			mSoundLevelSingleValue = v.mSoundLevelSingleValue;
		}
		internal static IfcSoundValue Parse(string strDef) { IfcSoundValue v = new IfcSoundValue(); int ipos = 0; parseFields(v, ParserSTEP.SplitLineFields(strDef), ref ipos); return v; }
		internal static void parseFields(IfcSoundValue v, List<string> arrFields, ref int ipos) { IfcPropertySetDefinition.parseFields(v, arrFields, ref ipos); v.mSoundLevelTimeSeries = ParserSTEP.ParseLink(arrFields[ipos++]); v.mFrequency = ParserSTEP.ParseDouble(arrFields[ipos++]); v.mSoundLevelSingleValue = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mSoundLevelTimeSeries) + "," + ParserSTEP.DoubleToString(mFrequency) + "," + ParserSTEP.DoubleOptionalToString(mSoundLevelSingleValue); }
	}
	public partial class IfcSpace : IfcSpatialStructureElement, IfcSpaceBoundarySelect
	{
		//internal IfcInternalOrExternalEnum mInteriorOrExteriorSpace = IfcInternalOrExternalEnum.NOTDEFINED;// : IfcInternalOrExternalEnum; replaced IFC4
		internal IfcSpaceTypeEnum mPredefinedType = IfcSpaceTypeEnum.NOTDEFINED; 	//:	OPTIONAL IfcSpaceTypeEnum;
		internal double mElevationWithFlooring = double.NaN;// : OPTIONAL IfcLengthMeasure;
		//INVERSE
		internal List<IfcRelCoversSpaces> mHasCoverings = new List<IfcRelCoversSpaces>(); // : SET [0:?] OF IfcRelCoversSpaces FOR RelatedSpace;
		internal List<IfcRelSpaceBoundary> mBoundedBy = new List<IfcRelSpaceBoundary>();  //	BoundedBy : SET [0:?] OF IfcRelSpaceBoundary FOR RelatingSpace;

		public IfcSpaceTypeEnum PredefinedType
		{
			get { return mPredefinedType; }
			set
			{
				mPredefinedType = value;
				if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
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
		internal IfcSpace(DatabaseIfc db, IfcSpace s, bool downStream) : base(db, s, downStream) { mPredefinedType = s.mPredefinedType; mElevationWithFlooring = s.mElevationWithFlooring; }
		internal IfcSpace(IfcSpatialStructureElement host, string name) : base(host, name) { IfcRelCoversSpaces cs = new IfcRelCoversSpaces(this, null); }
		public IfcSpace(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static void parseFields(IfcSpace gp, List<string> arrFields, ref int ipos)
		{
			IfcSpatialStructureElement.parseFields(gp, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s[0] == '.')
				gp.mPredefinedType = (IfcSpaceTypeEnum)Enum.Parse(typeof(IfcSpaceTypeEnum), s.Replace(".", ""));
			gp.mElevationWithFlooring = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mPredefinedType != IfcSpaceTypeEnum.NOTDEFINED ? ",." + mPredefinedType.ToString() + ".," : ",$,") + ParserSTEP.DoubleOptionalToString(mElevationWithFlooring); }
		internal static IfcSpace Parse(string strDef) { IfcSpace s = new IfcSpace(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		public override bool AddElement(IfcProduct s)
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
	public partial interface IfcSpaceBoundarySelect : IBaseClassIfc //IFC4 SELECT (	IfcSpace, IfcExternalSpatialElement);
	{
		List<IfcRelSpaceBoundary> BoundedBy { get; }
	}
	public partial class IfcSpaceHeater : IfcFlowTerminal //IFC4
	{
		internal IfcSpaceHeaterTypeEnum mPredefinedType = IfcSpaceHeaterTypeEnum.NOTDEFINED;// OPTIONAL : IfcSpaceHeaterTypeEnum;
		public IfcSpaceHeaterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSpaceHeater() : base() { }
		internal IfcSpaceHeater(DatabaseIfc db, IfcSpaceHeater h) : base(db, h) { mPredefinedType = h.mPredefinedType; }
		public IfcSpaceHeater(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcSpaceHeater s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcSpaceHeaterTypeEnum)Enum.Parse(typeof(IfcSpaceHeaterTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcSpaceHeater Parse(string strDef) { IfcSpaceHeater s = new IfcSpaceHeater(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcSpaceHeaterType : IfcFlowTerminalType
	{
		internal IfcSpaceHeaterTypeEnum mPredefinedType = IfcSpaceHeaterTypeEnum.NOTDEFINED;// : IfcSpaceHeaterExchangerEnum; 
		public IfcSpaceHeaterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSpaceHeaterType() : base() { }
		internal IfcSpaceHeaterType(DatabaseIfc db, IfcSpaceHeaterType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcSpaceHeaterType(DatabaseIfc m, string name, IfcSpaceHeaterTypeEnum t) : base(m) { Name = name; PredefinedType = t; }
		internal static void parseFields(IfcSpaceHeaterType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSpaceHeaterTypeEnum)Enum.Parse(typeof(IfcSpaceHeaterTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSpaceHeaterType Parse(string strDef) { IfcSpaceHeaterType t = new IfcSpaceHeaterType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcSpaceProgram : IfcControl // DEPRECEATED IFC4
	{
		internal string mSpaceProgramIdentifier;// : IfcIdentifier;
		internal double mMaxRequiredArea, mMinRequiredArea;// : OPTIONAL IfcAreaMeasure;
		internal int mRequestedLocation;// : OPTIONAL IfcSpatialStructureElement;
		internal double mStandardRequiredArea;// : IfcAreaMeasure; 
		public IfcSpatialStructureElement RequestedLocation { get { return mDatabase[mRequestedLocation] as IfcSpatialStructureElement; } set { mRequestedLocation = value == null ? 0 : value.mIndex; } }
		internal IfcSpaceProgram() : base() { }
		internal IfcSpaceProgram(DatabaseIfc db, IfcSpaceProgram p) : base(db,p)
		{
			mSpaceProgramIdentifier = p.mSpaceProgramIdentifier;
			mMaxRequiredArea = p.mMaxRequiredArea;
			mMinRequiredArea = p.mMinRequiredArea;
			if(p.mRequestedLocation > 0)
				RequestedLocation = db.Factory.Duplicate( p.RequestedLocation) as IfcSpatialStructureElement;
			mStandardRequiredArea = p.mStandardRequiredArea;
		}
		internal static IfcSpaceProgram Parse(string strDef, ReleaseVersion schema) { IfcSpaceProgram p = new IfcSpaceProgram(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		internal static void parseFields(IfcSpaceProgram p, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcControl.parseFields(p, arrFields, ref ipos,schema);
			p.mSpaceProgramIdentifier = arrFields[ipos++];
			p.mMaxRequiredArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mMinRequiredArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mRequestedLocation = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mStandardRequiredArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mSpaceProgramIdentifier + "," + ParserSTEP.DoubleOptionalToString(mMaxRequiredArea) + "," + ParserSTEP.DoubleOptionalToString(mMinRequiredArea) + "," + ParserSTEP.LinkToString(mRequestedLocation) + "," + ParserSTEP.DoubleToString(mStandardRequiredArea); }
	}
	//ENTITY IfcSpaceThermalLoadProperties // DEPRECEATED IFC4
	public partial class IfcSpaceType : IfcSpatialStructureElementType
	{
		internal IfcSpaceTypeEnum mPredefinedType = IfcSpaceTypeEnum.NOTDEFINED;
		public IfcSpaceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSpaceType() : base() { }
		internal IfcSpaceType(DatabaseIfc db, IfcSpaceType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcSpaceType(DatabaseIfc db, string name, IfcSpaceTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcSpaceType t, List<string> arrFields, ref int ipos) { IfcSpatialStructureElementType.parseFields(t, arrFields, ref ipos); try { t.mPredefinedType = (IfcSpaceTypeEnum)Enum.Parse(typeof(IfcSpaceTypeEnum), arrFields[ipos++].Replace(".", "")); } catch (Exception) { } }
		internal new static IfcSpaceType Parse(string strDef) { IfcSpaceType t = new IfcSpaceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract partial class IfcSpatialElement : IfcProduct //ABSTRACT SUPERTYPE OF (ONEOF (IfcExternalSpatialStructureElement ,IfcSpatialStructureElement ,IfcSpatialZone))
	{
		private string mLongName = "$";// : OPTIONAL IfcLabel; 
									   //INVERSE
		internal List<IfcRelContainedInSpatialStructure> mContainsElements = new List<IfcRelContainedInSpatialStructure>();// : SET [0:?] OF IfcRelReferencedInSpatialStructure FOR RelatingStructure;
		internal List<IfcRelServicesBuildings> mServicedBySystems = new List<IfcRelServicesBuildings>();// : SET [0:?] OF IfcRelServicesBuildings FOR RelatedBuildings;	

		public string LongName { get { return (mLongName == "$" ? "" : ParserIfc.Decode(mLongName)); } set { mLongName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public List<IfcRelContainedInSpatialStructure> ContainsElements { get { return mContainsElements; } }
		public List<IfcRelServicesBuildings> ServicedBySystems { get { return mServicedBySystems; } }

		protected IfcSpatialElement() : base() { }
		protected IfcSpatialElement(DatabaseIfc db, IfcSpatialElement e, bool downStream) : base(db, e)
		{
			mLongName = e.mLongName;
			if (downStream)
			{
				foreach (IfcRelContainedInSpatialStructure css in e.ContainsElements)
					db.Factory.Duplicate(css);
			}
		}
		protected IfcSpatialElement(IfcSpatialElement host, string name) : this(new IfcLocalPlacement(host.Placement, new IfcAxis2Placement3D(new IfcCartesianPoint(host.mDatabase, 0, 0, 0))))
		{
			Name = name;
			IfcBuilding building = this as IfcBuilding;
			if (building != null)
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
		protected IfcSpatialElement(IfcObjectPlacement placement) : base(placement)
		{
			IfcRelContainedInSpatialStructure rs = new IfcRelContainedInSpatialStructure(this);
			if (mDatabase.mRelease != ReleaseVersion.IFC2x3)
			{
				mContainerCommonPlacement = new IfcLocalPlacement(placement, mDatabase.Factory.PlaneXYPlacement);
				mContainerCommonPlacement.mContainerHost = this;
			}
		}
		protected IfcSpatialElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		protected static void parseFields(IfcSpatialElement s, List<string> arrFields, ref int ipos)
		{
			IfcProduct.parseFields(s, arrFields, ref ipos);
			s.LongName = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + (mLongName == "$" ? "$" : "'" + mLongName + "'"); }

		public override bool AddElement(IfcProduct e)
		{
			if (mContainsElements.Count == 0)
				new IfcRelContainedInSpatialStructure(this);
			mContainsElements[0].add(e);
				
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
					IfcBuilding b = mDatabase[rd.mRelatedObjects[0]] as IfcBuilding;
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
		public override List<T> Extract<T>() 
		{
			List<T> result = base.Extract<T>();
			foreach (IfcRelContainedInSpatialStructure rcss in mContainsElements)
			{
				foreach (IfcProduct p in rcss.RelatedElements)
					result.AddRange(p.Extract<T>());
			}
			return result;
		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			List<IfcRelContainedInSpatialStructure> css = ContainsElements;
			for (int icounter = 0; icounter < css.Count; icounter++)
			{
				List<IfcProduct> products = css[icounter].RelatedElements;
				for (int jcounter = 0; jcounter < products.Count; jcounter++)
					products[jcounter].changeSchema(schema);
			}
			base.changeSchema(schema);
		}
	}
	public abstract partial class IfcSpatialElementType : IfcTypeProduct //IFC4 ABSTRACT SUPERTYPE OF(IfcSpaceType)
	{
		internal string mElementType = "$";// : OPTIONAL IfcLabel
		public string ElementType { get { return (mElementType == "$" ? "" : ParserIfc.Decode(mElementType)); } set { mElementType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcSpatialElementType() : base() { }
		protected IfcSpatialElementType(DatabaseIfc db) : base(db) { }
		protected IfcSpatialElementType(DatabaseIfc db, IfcSpatialElementType t) : base(db, t) { mElementType = t.mElementType; }
		protected static void parseFields(IfcSpatialElementType t, List<string> arrFields, ref int ipos) { IfcTypeProduct.parseFields(t, arrFields, ref ipos); t.mElementType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mElementType == "$" ? ",$" : ",'" + mElementType + "'"); }
	}
	public abstract partial class IfcSpatialStructureElement : IfcSpatialElement /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBuilding ,IfcBuildingStorey ,IfcSite ,IfcSpace, IfcCivilStructureElement))*/
	{
		internal IfcElementCompositionEnum mCompositionType = IfcElementCompositionEnum.NA;// : IfcElementCompositionEnum;  IFC4 Optional 
		public IfcElementCompositionEnum CompositionType { get { return mCompositionType; } set { mCompositionType = value; } }

		protected IfcSpatialStructureElement() : base() { }
		protected IfcSpatialStructureElement(IfcObjectPlacement pl) : base(pl) { if (pl.mDatabase.mRelease == ReleaseVersion.IFC2x3) mCompositionType = IfcElementCompositionEnum.ELEMENT; }
		protected IfcSpatialStructureElement(DatabaseIfc db, IfcSpatialStructureElement e,bool downStream) : base(db, e, downStream) { mCompositionType = e.mCompositionType; }
		protected IfcSpatialStructureElement(IfcSpatialStructureElement host,string name) : base(host,name) { if (mDatabase.mRelease == ReleaseVersion.IFC2x3) mCompositionType = IfcElementCompositionEnum.ELEMENT; }
		protected IfcSpatialStructureElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		protected static void parseFields(IfcSpatialStructureElement s, List<string> arrFields, ref int ipos)
		{
			IfcSpatialElement.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str != "$")
				s.mCompositionType = (IfcElementCompositionEnum)Enum.Parse(typeof(IfcElementCompositionEnum), str.Replace(".", ""));
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mCompositionType == IfcElementCompositionEnum.NA ? (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ",." + IfcElementCompositionEnum.ELEMENT.ToString() + "." : ",$") : ",." + mCompositionType.ToString() + "."); }
	}
	public abstract partial class IfcSpatialStructureElementType : IfcSpatialElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcSpaceType))
	{
		protected IfcSpatialStructureElementType() : base() { }
		protected IfcSpatialStructureElementType(DatabaseIfc db) : base(db) { }
		protected IfcSpatialStructureElementType(DatabaseIfc db, IfcSpatialStructureElementType t) : base(db, t) { }
		protected static void parseFields(IfcSpatialStructureElementType t, List<string> arrFields, ref int ipos) { IfcSpatialElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcSpatialZone : IfcSpatialElement  //IFC4
	{
		internal IfcSpatialZoneTypeEnum mPredefinedType = IfcSpatialZoneTypeEnum.NOTDEFINED;
		public IfcSpatialZoneTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		protected IfcSpatialZone() : base() { }
		internal IfcSpatialZone(IfcSpatialStructureElement host, string name) : base(host, name) { if (mDatabase.mRelease == ReleaseVersion.IFC2x3) throw new Exception("IFCSpatial Zone only valid in IFC4 or newer!"); }
		protected IfcSpatialZone(DatabaseIfc db, IfcSpatialZone p, bool downStream) : base(db, p, downStream) { mPredefinedType = p.mPredefinedType; }

		protected static void parseFields(IfcSpatialZone s, List<string> arrFields, ref int ipos) { IfcProduct.parseFields(s, arrFields, ref ipos); if (arrFields[ipos++][0] == '.') s.mPredefinedType = (IfcSpatialZoneTypeEnum)Enum.Parse(typeof(IfcSpatialZoneTypeEnum), arrFields[ipos++].Replace(".", "")); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
		internal static IfcSpatialZone Parse(string strDef) { IfcSpatialZone t = new IfcSpatialZone(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public partial class IfcSpatialZoneType : IfcSpatialElementType  //IFC4
	{
		internal IfcSpatialZoneTypeEnum mPredefinedType = IfcSpatialZoneTypeEnum.NOTDEFINED;
		public IfcSpatialZoneTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSpatialZoneType() : base() { }
		internal IfcSpatialZoneType(DatabaseIfc db, IfcSpatialZoneType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcSpatialZoneType(DatabaseIfc m, string name) : base(m) { Name = name; if (mDatabase.mRelease == ReleaseVersion.IFC2x3) throw new Exception("IFCSpatial Zone Type only valid in IFC4 or newer!"); }
		internal static void parseFields(IfcSpatialZoneType t, List<string> arrFields, ref int ipos) { IfcSpatialElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSpatialZoneTypeEnum)Enum.Parse(typeof(IfcSpatialZoneTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSpatialZoneType Parse(string strDef) { IfcSpatialZoneType t = new IfcSpatialZoneType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public partial class IfcSphere : IfcCsgPrimitive3D
	{
		internal double mRadius;// : IfcPositiveLengthMeasure; 
		internal IfcSphere() : base() { }
		internal IfcSphere(DatabaseIfc db, IfcSphere s) : base(db,s) { mRadius = s.mRadius; }

		internal static void parseFields(IfcSphere s, List<string> arrFields, ref int ipos) { IfcCsgPrimitive3D.parseFields(s, arrFields, ref ipos); s.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcSphere Parse(string strDef) { IfcSphere s = new IfcSphere(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mRadius); }
	}
	public partial class IfcStackTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcStackTerminalTypeEnum mPredefinedType = IfcStackTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcStackTerminalTypeEnum;
		public IfcStackTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStackTerminal() : base() { }
		internal IfcStackTerminal(DatabaseIfc db, IfcStackTerminal t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcStackTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcStackTerminal s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcStackTerminalTypeEnum)Enum.Parse(typeof(IfcStackTerminalTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcStackTerminal Parse(string strDef) { IfcStackTerminal s = new IfcStackTerminal(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcStackTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcStackTerminalType : IfcFlowTerminalType
	{
		internal IfcStackTerminalTypeEnum mPredefinedType = IfcStackTerminalTypeEnum.NOTDEFINED;
		public IfcStackTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStackTerminalType() : base() { }
		internal IfcStackTerminalType(DatabaseIfc db, IfcStackTerminalType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcStackTerminalType(DatabaseIfc m, string name, IfcStackTerminalTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcStackTerminalType t, List<string> arrFields, ref int ipos) { IfcFlowTerminalType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcStackTerminalTypeEnum)Enum.Parse(typeof(IfcStackTerminalTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcStackTerminalType Parse(string strDef) { IfcStackTerminalType t = new IfcStackTerminalType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcStair : IfcBuildingElement
	{
		internal IfcStairTypeEnum mPredefinedType = IfcStairTypeEnum.NOTDEFINED;// OPTIONAL : IfcStairTypeEnum
		public IfcStairTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStair() : base() { }
		internal IfcStair(DatabaseIfc db, IfcStair s) : base(db, s) { mPredefinedType = s.mPredefinedType; }
		public IfcStair(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static IfcStair Parse(string strDef) { IfcStair s = new IfcStair(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcStair s, List<string> arrFields, ref int ipos)
		{
			IfcBuildingElement.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcStairTypeEnum)Enum.Parse(typeof(IfcStairTypeEnum), str.Substring(1, str.Length - 2));
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
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
		internal IfcStairFlight(DatabaseIfc db, IfcStairFlight f) : base(db, f) { mNumberOfRiser = f.mNumberOfRiser; mNumberOfTreads = f.mNumberOfTreads; mRiserHeight = f.mRiserHeight; mTreadLength = f.mTreadLength; mPredefinedType = f.mPredefinedType; }
		public IfcStairFlight(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }

		internal static IfcStairFlight Parse(string strDef, ReleaseVersion schema) { IfcStairFlight f = new IfcStairFlight(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return f; }
		internal static void parseFields(IfcStairFlight f, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcBuildingElement.parseFields(f, arrFields, ref ipos);
			f.mNumberOfRiser = ParserSTEP.ParseInt(arrFields[ipos++]);
			f.mNumberOfTreads = ParserSTEP.ParseInt(arrFields[ipos++]);
			f.mRiserHeight = ParserSTEP.ParseDouble(arrFields[ipos++]);
			f.mTreadLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					f.mPredefinedType = (IfcStairFlightTypeEnum)Enum.Parse(typeof(IfcStairFlightTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + "," + ParserSTEP.IntOptionalToString(mNumberOfRiser) + "," + ParserSTEP.IntOptionalToString(mNumberOfTreads) + "," +
				ParserSTEP.DoubleOptionalToString(mRiserHeight) + "," + ParserSTEP.DoubleOptionalToString(mTreadLength);
			return result + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcStairFlightTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcStairFlightType : IfcBuildingElementType
	{
		internal IfcStairFlightTypeEnum mPredefinedType = IfcStairFlightTypeEnum.NOTDEFINED;
		public IfcStairFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStairFlightType() : base() { }
		internal IfcStairFlightType(DatabaseIfc db, IfcStairFlightType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcStairFlightType(DatabaseIfc m, string name, IfcStairFlightTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcStairFlightType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcStairFlightTypeEnum)Enum.Parse(typeof(IfcStairFlightTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcStairFlightType Parse(string strDef) { IfcStairFlightType t = new IfcStairFlightType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcStairType : IfcBuildingElementType
	{
		internal IfcStairTypeEnum mPredefinedType = IfcStairTypeEnum.NOTDEFINED;
		public IfcStairTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStairType() : base() { }
		internal IfcStairType(DatabaseIfc db, IfcStairType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcStairType(DatabaseIfc m, string name, IfcStairTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcStairType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcStairTypeEnum)Enum.Parse(typeof(IfcStairTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcStairType Parse(string strDef) { IfcStairType t = new IfcStairType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."); }
	}
	public abstract partial class IfcStructuralAction : IfcStructuralActivity // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveAction, IfcStructuralPointAction, IfcStructuralSurfaceAction))
	{
		internal bool mDestabilizingLoad = true, mDestabSet = false;//: OPTIONAL BOOLEAN; IFC4 made optional
		internal int mCausedBy;// : OPTIONAL IfcStructuralReaction; DELETED IFC4 

		public bool DestabilizingLoad { get { return mDestabilizingLoad; } set { mDestabilizingLoad = value; mDestabSet = true; } }
		public IfcStructuralReaction CausedBy { get { return mDatabase[mCausedBy] as IfcStructuralReaction; } set { mCausedBy = (value == null ? 0 : value.mIndex); } }

		protected IfcStructuralAction() : base() { }
		protected IfcStructuralAction(DatabaseIfc db, IfcStructuralAction a) : base(db,a)
		{
			mDestabilizingLoad = a.mDestabilizingLoad;
			if(a.mCausedBy > 0)
				CausedBy = a.CausedBy;
		}
		protected IfcStructuralAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, bool global)
			: base(lc.mDatabase, item, load, global, lc.IsGroupedBy[0]) { }
		protected static void parseFields(IfcStructuralAction a, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcStructuralActivity.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
			{
				a.mDestabilizingLoad = ParserSTEP.ParseBool(s);
				a.mDestabSet = true;
			}
			if (schema == ReleaseVersion.IFC2x3)
				a.mCausedBy = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDestabSet || mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "," + ParserSTEP.BoolToString(mDestabilizingLoad) : ",$") + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "," + ParserSTEP.LinkToString(mCausedBy) : ""); }
	}
	public abstract partial class IfcStructuralActivity : IfcProduct
	{
		private int mAppliedLoad;// : IfcStructuralLoad;
		private IfcGlobalOrLocalEnum mGlobalOrLocal = IfcGlobalOrLocalEnum.GLOBAL_COORDS;// : IfcGlobalOrLocalEnum; 
		//INVERSE 
		private IfcRelConnectsStructuralActivity mAssignedToStructuralItem = null; // : SET [0:1] OF IfcRelConnectsStructuralActivity FOR RelatedStructuralActivity; 

		public IfcStructuralLoad AppliedLoad { get { return mDatabase[mAppliedLoad] as IfcStructuralLoad; } set { mAppliedLoad = value.mIndex; } }
		public IfcGlobalOrLocalEnum GlobalOrLocal { get { return mGlobalOrLocal; } }
		public IfcRelConnectsStructuralActivity AssignedToStructuralItem { get { return mAssignedToStructuralItem; } set { mAssignedToStructuralItem = value; } }

		protected IfcStructuralActivity() : base() { }
		protected IfcStructuralActivity(DatabaseIfc db, IfcStructuralActivity a) : base(db,a)
		{
			AppliedLoad = db.Factory.Duplicate(a.AppliedLoad) as IfcStructuralLoad;
			mGlobalOrLocal = a.mGlobalOrLocal;
		}
		protected IfcStructuralActivity(DatabaseIfc db, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, bool global, IfcRelAssignsToGroup loadcase) : base(db)
		{
			mAssignedToStructuralItem = new IfcRelConnectsStructuralActivity(item, this);
			mAppliedLoad = load.mIndex;
			mGlobalOrLocal = global ? IfcGlobalOrLocalEnum.GLOBAL_COORDS : IfcGlobalOrLocalEnum.LOCAL_COORDS;
			loadcase.AddRelated(this);
		}
		protected static void parseFields(IfcStructuralActivity a, List<string> arrFields, ref int ipos)
		{
			IfcProduct.parseFields(a, arrFields, ref ipos);
			a.mAppliedLoad = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mGlobalOrLocal = (IfcGlobalOrLocalEnum)Enum.Parse(typeof(IfcGlobalOrLocalEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mAppliedLoad) + ",." + mGlobalOrLocal.ToString() + "."; }
	}
	public interface IfcStructuralActivityAssignmentSelect : IBaseClassIfc { List<IfcRelConnectsStructuralActivity> AssignedStructuralActivity { get; } } //SELECT(IfcStructuralItem,IfcElement);
	public partial class IfcStructuralAnalysisModel : IfcSystem
	{
		internal IfcAnalysisModelTypeEnum mPredefinedType = IfcAnalysisModelTypeEnum.NOTDEFINED;// : IfcAnalysisModelTypeEnum;
		internal int mOrientationOf2DPlane;// : OPTIONAL IfcAxis2Placement3D;
		internal List<int> mLoadedBy = new List<int>();//  : OPTIONAL SET [1:?] OF IfcStructuralLoadGroup;
		internal List<int> mHasResults = new List<int>();//: OPTIONAL SET [1:?] OF IfcStructuralResultGroup  
		internal int mSharedPlacement;//	:	OPTIONAL IfcObjectPlacement;

		public IfcAnalysisModelTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcAxis2Placement3D OrientationOf2DPlane { get { return mDatabase[mOrientationOf2DPlane] as IfcAxis2Placement3D; } set { mOrientationOf2DPlane = value == null ? 0 : value.mIndex;  } }
		public List<IfcStructuralLoadGroup> LoadedBy { get { return mLoadedBy.ConvertAll(x => mDatabase[x] as IfcStructuralLoadGroup); } set { mLoadedBy = value.ConvertAll(x => x.mIndex); } }
		public List<IfcStructuralResultGroup> HasResults { get { return mHasResults.ConvertAll(x => mDatabase[x] as IfcStructuralResultGroup); } set { mHasResults = value.ConvertAll(x => x.mIndex); } } 
		public IfcObjectPlacement SharedPlacement { get { return mDatabase[mSharedPlacement] as IfcObjectPlacement; } set { mSharedPlacement = (value == null ? 0 : value.mIndex); } }

		internal IfcStructuralAnalysisModel() : base() { }
		internal IfcStructuralAnalysisModel(DatabaseIfc db, IfcStructuralAnalysisModel m) : base(db,m)
		{
			mPredefinedType = m.mPredefinedType;
			if(m.mOrientationOf2DPlane > 0)
				OrientationOf2DPlane = db.Factory.Duplicate(m.OrientationOf2DPlane) as IfcAxis2Placement3D;
			LoadedBy = m.LoadedBy.ConvertAll(x => db.Factory.Duplicate(x) as IfcStructuralLoadGroup);
			HasResults = m.HasResults.ConvertAll(x => db.Factory.Duplicate(x) as IfcStructuralResultGroup);
			if(m.mSharedPlacement > 0)
				SharedPlacement = db.Factory.Duplicate(m.SharedPlacement) as IfcObjectPlacement;
		}
		public IfcStructuralAnalysisModel(IfcSpatialElement bldg, string name, IfcAnalysisModelTypeEnum type) : base(bldg, name)
		{
			mPredefinedType = type;
			SharedPlacement = new IfcLocalPlacement(mDatabase.Factory.PlaneXYPlacement);
		}
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
			if (ipos < arrFields.Count) // IFC4 late change
				c.mSharedPlacement = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + ".," + ParserSTEP.LinkToString(mOrientationOf2DPlane) + ",";
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
			if (mDatabase.mRelease != ReleaseVersion.IFC2x3)
				str += (mSharedPlacement == 0 ? ",$" : ",#" + mSharedPlacement);
			return str;
		}

		internal void addLoadGroup(IfcStructuralLoadGroup lg) { mLoadedBy.Add(lg.mIndex); }
	}
	public abstract partial class IfcStructuralConnection : IfcStructuralItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveConnection ,IfcStructuralPointConnection ,IfcStructuralSurfaceConnection))
	{
		internal int mAppliedCondition = 0; //: OPTIONAL IfcBoundaryCondition
		//INVERSE
		internal List<IfcRelConnectsStructuralMember> mConnectsStructuralMembers = new List<IfcRelConnectsStructuralMember>();//	 :	SET [1:?] OF IfcRelConnectsStructuralMember FOR RelatedStructuralConnection;

		public IfcBoundaryCondition AppliedCondition { get { return mDatabase[mAppliedCondition] as IfcBoundaryCondition; } set { mAppliedCondition = (value == null ? 0 : value.mIndex); } }

		protected IfcStructuralConnection() : base() { }
		protected IfcStructuralConnection(DatabaseIfc db, IfcStructuralConnection c) : base(db,c) { if(c.mAppliedCondition > 0) AppliedCondition = db.Factory.Duplicate(c.AppliedCondition) as IfcBoundaryCondition; }
		protected IfcStructuralConnection(IfcStructuralAnalysisModel sm) : base(sm) {  }
		protected static void parseFields(IfcStructuralConnection c, List<string> arrFields, ref int ipos) { IfcStructuralItem.parseFields(c, arrFields, ref ipos); c.mAppliedCondition = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mAppliedCondition); }
	}
	public abstract partial class IfcStructuralConnectionCondition : BaseClassIfc //ABSTRACT SUPERTYPE OF (ONEOF (IfcFailureConnectionCondition ,IfcSlippageConnectionCondition));
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcStructuralConnectionCondition() : base() { }
		protected IfcStructuralConnectionCondition(DatabaseIfc db, IfcStructuralConnectionCondition c) : base(db,c) { mName = c.mName; }
		protected static void parseFields(IfcStructuralConnectionCondition c, List<string> arrFields, ref int ipos) { c.mName = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mName == "$" ? ",$" : ",'" + mName + "'"); }
	}
	public partial class IfcStructuralCurveAction : IfcStructuralAction // IFC4 SUPERTYPE OF(IfcStructuralLinearAction)
	{
		internal IfcProjectedOrTrueLengthEnum mProjectedOrTrue = IfcProjectedOrTrueLengthEnum.TRUE_LENGTH;// : IfcProjectedOrTrueLengthEnum
		internal IfcStructuralCurveActivityTypeEnum mPredefinedType = IfcStructuralCurveActivityTypeEnum.NOTDEFINED;//IfcStructuralCurveActivityTypeEnum

		public IfcProjectedOrTrueLengthEnum ProjectedOrTrue { get { return mProjectedOrTrue; } set { mProjectedOrTrue = value; } }
		public IfcStructuralCurveActivityTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStructuralCurveAction() : base() { }
		internal IfcStructuralCurveAction(DatabaseIfc db, IfcStructuralCurveAction a) : base(db,a) { mProjectedOrTrue = a.mProjectedOrTrue; mPredefinedType = a.mPredefinedType; }
		public IfcStructuralCurveAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadSingleForce force, double length, bool global)
			: base(lc, member, new IfcStructuralLoadConfiguration(force, length), global) { mPredefinedType = IfcStructuralCurveActivityTypeEnum.DISCRETE; }
		public IfcStructuralCurveAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoad load, bool global, bool projected, IfcStructuralCurveActivityTypeEnum type)
			: base(lc, member, load, global) { ProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH; PredefinedType = type; }

		internal static IfcStructuralCurveAction Parse(string strDef,ReleaseVersion schema) { IfcStructuralCurveAction a = new IfcStructuralCurveAction(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcStructuralCurveAction a, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcStructuralAction.parseFields(a, arrFields, ref ipos,schema);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mProjectedOrTrue = (IfcProjectedOrTrueLengthEnum)Enum.Parse(typeof(IfcProjectedOrTrueLengthEnum), s.Replace(".", ""));
			if (schema != ReleaseVersion.IFC2x3)
				a.mPredefinedType = (IfcStructuralCurveActivityTypeEnum)Enum.Parse(typeof(IfcStructuralCurveActivityTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",." + mProjectedOrTrue.ToString() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "." : ".,." + mPredefinedType.ToString() + ".");
		}
	}
	public partial class IfcStructuralCurveConnection : IfcStructuralConnection
	{
		internal IfcStructuralCurveConnection() : base() { }
		internal IfcStructuralCurveConnection(DatabaseIfc db, IfcStructuralCurveConnection c) : base(db,c) { }
		internal static IfcStructuralCurveConnection Parse(string strDef) { IfcStructuralCurveConnection c = new IfcStructuralCurveConnection(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcStructuralCurveConnection c, List<string> arrFields, ref int ipos) { IfcStructuralConnection.parseFields(c, arrFields, ref ipos); }
	}
	public partial class IfcStructuralCurveMember : IfcStructuralMember
	{
		internal IfcStructuralCurveTypeEnum mPredefinedType= IfcStructuralCurveTypeEnum.NOTDEFINED;// : IfcStructuralCurveTypeEnum; 
		internal int mAxis; //: IfcDirection

		public IfcStructuralCurveTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcDirection Axis { get { return mDatabase[mAxis] as IfcDirection; } set { mAxis = value.mIndex; } }

		public IfcEdgeCurve EdgeCurve { get; set; } //Used for load applications in ifc2x3

		public IfcStructuralCurveMember() : base() { }
		internal IfcStructuralCurveMember(DatabaseIfc db, IfcStructuralCurveMember m) : base(db,m) { mPredefinedType = m.mPredefinedType; Axis = db.Factory.Duplicate(m.Axis) as IfcDirection; }
		public IfcStructuralCurveMember(IfcStructuralAnalysisModel sm, IfcStructuralPointConnection A, ExtremityAttributes start, IfcStructuralPointConnection B, ExtremityAttributes end, IfcMaterialProfileSetUsage mp, IfcDirection dir, int id)
			: this(sm, new IfcEdgeCurve(A.Vertex,B.Vertex,new IfcPolyline(A.Vertex.VertexGeometry as IfcCartesianPoint,B.Vertex.VertexGeometry as IfcCartesianPoint),true),A,start,B,end, mp,dir, id) { }
		public IfcStructuralCurveMember(IfcStructuralAnalysisModel sm, IfcEdgeCurve e, IfcStructuralPointConnection A, ExtremityAttributes start, IfcStructuralPointConnection B, ExtremityAttributes end, IfcMaterialProfileSetUsage mp, IfcDirection dir,int id)
			: base(sm, mp ,id)
		{
			EdgeCurve = e;
			Representation = new IfcProductDefinitionShape(new IfcTopologyRepresentation(e, "Reference"));
			IfcRelConnectsStructuralMember csm = IfcRelConnectsStructuralMember.Create(this, A, true, start);
			csm = IfcRelConnectsStructuralMember.Create(this, B, false, end);
			Axis = dir;
		}

		internal static IfcStructuralCurveMember Parse(string strDef, ReleaseVersion schema) { IfcStructuralCurveMember m = new IfcStructuralCurveMember(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcStructuralCurveMember m, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcStructuralMember.parseFields(m, arrFields, ref ipos);
			string s = arrFields[ipos++];
			m.mPredefinedType = (IfcStructuralCurveTypeEnum)Enum.Parse(typeof(IfcStructuralCurveTypeEnum), s.Substring(1, s.Length - 2));
			if (schema != ReleaseVersion.IFC2x3)
				m.mAxis = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "." : ".," + ParserSTEP.LinkToString(mAxis)); }

		public partial class ExtremityAttributes
		{
			public IfcBoundaryNodeCondition BoundaryCondition { get; set; }
			public IfcStructuralConnectionCondition StructuralConnectionCondition { get; set; }
			public double SupportedLength { get; set; } = 0;
			public Tuple<double, double, double> Eccentricity { get; set; } = null;
			internal IfcAxis2Placement3D ConditionCoordinateSystem { get; set; } = null;
			public ExtremityAttributes() { }
			public ExtremityAttributes(ExtremityAttributes atts)
			{
				if(atts != null)
				{
					BoundaryCondition = atts.BoundaryCondition;
					StructuralConnectionCondition = atts.StructuralConnectionCondition;
					SupportedLength = atts.SupportedLength;
					Eccentricity = atts.Eccentricity;
					ConditionCoordinateSystem = atts.ConditionCoordinateSystem;
				}
			}
		}
	}
	public partial class IfcStructuralCurveMemberVarying : IfcStructuralCurveMember
	{
		internal IfcStructuralCurveMemberVarying() : base() { }
		internal IfcStructuralCurveMemberVarying(DatabaseIfc db, IfcStructuralCurveMemberVarying m) : base(db,m) { }
		internal new static IfcStructuralCurveMemberVarying Parse(string strDef,ReleaseVersion schema) { IfcStructuralCurveMemberVarying m = new IfcStructuralCurveMemberVarying(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return m; }
		internal static void parseFields(IfcStructuralCurveMemberVarying m, List<string> arrFields, ref int ipos,ReleaseVersion schema) { IfcStructuralCurveMember.parseFields(m, arrFields, ref ipos,schema); }
	}
	public partial class IfcStructuralCurveReaction : IfcStructuralReaction
	{
		internal IfcStructuralCurveActivityTypeEnum mPredefinedType = IfcStructuralCurveActivityTypeEnum.NOTDEFINED;//: 	IfcStructuralCurveActivityTypeEnum; 
		internal IfcStructuralCurveReaction() : base() { }
		internal IfcStructuralCurveReaction(DatabaseIfc db, IfcStructuralCurveReaction r) : base(db,r) { mPredefinedType = r.mPredefinedType; }
		internal static IfcStructuralCurveReaction Parse(string strDef) { IfcStructuralCurveReaction r = new IfcStructuralCurveReaction(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
		internal static void parseFields(IfcStructuralCurveReaction r, List<string> arrFields, ref int ipos) { IfcStructuralReaction.parseFields(r, arrFields, ref ipos); r.mPredefinedType = (IfcStructuralCurveActivityTypeEnum)Enum.Parse(typeof(IfcStructuralCurveActivityTypeEnum), arrFields[ipos++].Replace(".", "")); }
	}
	public abstract partial class IfcStructuralItem : IfcProduct, IfcStructuralActivityAssignmentSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralConnection ,IfcStructuralMember))
	{
		internal int mID = 0;
		//INVERSE
		internal List<IfcRelConnectsStructuralActivity> mAssignedStructuralActivity = new List<IfcRelConnectsStructuralActivity>();//: 	SET OF IfcRelConnectsStructuralActivity FOR RelatingElement;
		public List<IfcRelConnectsStructuralActivity> AssignedStructuralActivity { get { return mAssignedStructuralActivity; } }
		protected IfcStructuralItem() : base() { }
		protected IfcStructuralItem(DatabaseIfc db, IfcStructuralItem i) : base(db,i)
		{
			foreach(IfcRelConnectsStructuralActivity rcss in i.mAssignedStructuralActivity)
			{
				IfcRelConnectsStructuralActivity rc = db.Factory.Duplicate(rcss) as IfcRelConnectsStructuralActivity;
				rc.RelatingElement = this;
			}
		}
		protected IfcStructuralItem(IfcStructuralAnalysisModel sm) : base(sm.mDatabase.mRelease == ReleaseVersion.IFC2x3 ? new IfcLocalPlacement(sm.SharedPlacement,sm.mDatabase.Factory.PlaneXYPlacement) : sm.SharedPlacement ,null)
		{
			sm.mIsGroupedBy[0].AddRelated(this);
			mDatabase.mContext.setStructuralUnits();
		}
		protected IfcStructuralItem(IfcStructuralAnalysisModel sm, int id) :this(sm)
		{
			mID = id;
			if (id >= 0)
				Name = id.ToString();
		}
		protected static void parseFields(IfcStructuralItem i, List<string> arrFields, ref int ipos) { IfcProduct.parseFields(i, arrFields, ref ipos); }
	}
	public partial class IfcStructuralLinearAction : IfcStructuralCurveAction 
	{
		internal IfcStructuralLinearAction() : base() { }
		internal IfcStructuralLinearAction(DatabaseIfc db, IfcStructuralLinearAction a) : base(db,a) {  }
		public IfcStructuralLinearAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadTemperature load)
			: this(lc, member, load, true,false, IfcStructuralCurveActivityTypeEnum.CONST) { }
		public IfcStructuralLinearAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadLinearForce load, bool global, bool projected) 
			: this(lc, member, load, global, projected, IfcStructuralCurveActivityTypeEnum.CONST) { }
		
		// SurfaceMembers to be added
		protected IfcStructuralLinearAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadStatic load, bool global, bool projected, IfcStructuralCurveActivityTypeEnum activity) 
			: base(lc, member, load, global, projected, activity)
		{
			if(mDatabase.mRelease == ReleaseVersion.IFC2x3)
			{
				Representation = new IfcProductRepresentation(new IfcTopologyRepresentation(member.EdgeCurve, "Reference"));
			}
		}
		internal new static IfcStructuralLinearAction Parse(string strDef,ReleaseVersion schema) { IfcStructuralLinearAction a = new IfcStructuralLinearAction(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcStructuralLinearAction a, List<string> arrFields, ref int ipos,ReleaseVersion schema) { IfcStructuralCurveAction.parseFields(a, arrFields, ref ipos,schema); }
	}
	public partial class IfcStructuralLinearActionVarying : IfcStructuralLinearAction // DELETED IFC4
	{
		internal int mVaryingAppliedLoadLocation;// : IfcShapeAspect;
		internal List<int> mSubsequentAppliedLoads = new List<int>();//: LIST [1:?] OF IfcStructuralLoad; 

		public IfcShapeAspect VaryingAppliedLoadLocation { get { return mDatabase[mVaryingAppliedLoadLocation] as IfcShapeAspect; } set { mVaryingAppliedLoadLocation = value.mIndex; } }
		public List<IfcStructuralLoad> SubsequentAppliedLoads { get { return mSubsequentAppliedLoads.ConvertAll(x => mDatabase[x] as IfcStructuralLoad); } set { mSubsequentAppliedLoads = value.ConvertAll(x => x.mIndex); } }

		internal IfcStructuralLinearActionVarying() : base() { }
		internal IfcStructuralLinearActionVarying(DatabaseIfc db, IfcStructuralLinearActionVarying a) : base(db, a)
		{
			VaryingAppliedLoadLocation = db.Factory.Duplicate( a.VaryingAppliedLoadLocation) as IfcShapeAspect;
			SubsequentAppliedLoads = a.SubsequentAppliedLoads.ConvertAll(x=>db.Factory.Duplicate(x) as IfcStructuralLoad);
		}
		public IfcStructuralLinearActionVarying(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadLinearForce start, IfcStructuralLoadLinearForce end, bool global, bool projected) 
			: base(lc, member, start, global, projected, IfcStructuralCurveActivityTypeEnum.LINEAR)
		{
			if (mDatabase.mRelease != ReleaseVersion.IFC2x3)
				throw new Exception(this.KeyWord + " deleted in IFC4");
			IfcCurve curve = member.EdgeCurve.EdgeGeometry;
			List<IfcShapeModel> aspects = new List<IfcShapeModel>();
			aspects.Add( new IfcShapeRepresentation(new IfcPointOnCurve(mDatabase,curve, 0)));
			aspects.Add(new IfcShapeRepresentation(new IfcPointOnCurve(mDatabase, curve, 1)));
			VaryingAppliedLoadLocation = new IfcShapeAspect(aspects, "", "", null);
		}

		internal new static IfcStructuralLinearActionVarying Parse(string strDef,ReleaseVersion schema) { IfcStructuralLinearActionVarying a = new IfcStructuralLinearActionVarying(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcStructuralLinearActionVarying a, List<string> arrFields, ref int ipos,ReleaseVersion schema) { IfcStructuralLinearAction.parseFields(a, arrFields, ref ipos,schema); a.mVaryingAppliedLoadLocation = ParserSTEP.ParseLink(arrFields[ipos++]); a.mSubsequentAppliedLoads = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mVaryingAppliedLoadLocation) + ",("
				+ ParserSTEP.LinkToString(mSubsequentAppliedLoads[0]);
			for (int icounter = 1; icounter < mSubsequentAppliedLoads.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSubsequentAppliedLoads[icounter]);
			return str + ")";
		}
	}
	public abstract partial class IfcStructuralLoad : BaseClassIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralLoadConfiguration, IfcStructuralLoadOrResult));
	{
		internal string mName = "$";// : OPTIONAL IfcLabel
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcStructuralLoad() : base() { }
		protected IfcStructuralLoad(DatabaseIfc db) : base(db) { }
		protected IfcStructuralLoad(DatabaseIfc db, IfcStructuralLoad l) : base(db) { mName = l.mName; }
		protected static void parseFields(IfcStructuralLoad l, List<string> arrFields, ref int ipos) { l.mName = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mName == "$" ? ",$" : ",'" + mName + "'"); }
	}
	public partial class IfcStructuralLoadConfiguration : IfcStructuralLoad //IFC4
	{
		internal List<int> mValues = new List<int>();//	 :	LIST [1:?] OF IfcStructuralLoadOrResult;
		internal List<List<double>> mLocations = new List<List<double>>();//	 :	OPTIONAL LIST [1:?] OF UNIQUE LIST [1:2] OF IfcLengthMeasure; 

		public List<IfcStructuralLoadOrResult> Values { get { return mValues.ConvertAll(x => (mDatabase[x] as IfcStructuralLoadOrResult)); } set { mValues = value.ConvertAll(x => x.mIndex); } }
		public List<List<double>> Locations { get { return mLocations; } }

		internal IfcStructuralLoadConfiguration() : base() { }
		internal IfcStructuralLoadConfiguration(DatabaseIfc db, IfcStructuralLoadConfiguration p) : base(db,p)
		{
			Values = p.Values.ConvertAll(x=>db.Factory.Duplicate(x) as IfcStructuralLoadOrResult);
			mLocations.AddRange(p.mLocations);
		}
		public IfcStructuralLoadConfiguration(IfcStructuralLoadOrResult val, double length)
			: base(val.mDatabase) { mValues.Add(val.mIndex); mLocations.Add( new List<double>() { length } );  }
		public IfcStructuralLoadConfiguration(List<IfcStructuralLoadOrResult> vals, List<List<double>> lengths)
			: base(vals[0].mDatabase) { mValues = vals.ConvertAll(x => x.mIndex); if (lengths != null) mLocations = lengths; }
		public IfcStructuralLoadConfiguration(IfcStructuralLoadOrResult val1, double loc1, IfcStructuralLoadOrResult val2, double loc2)
			: base(val1.mDatabase) { mValues = new List<int>() { val1.mIndex, val2.mIndex }; mLocations = new List<List<double>>() { new List<double>() { loc1 }, new List<double>() { loc2 } }; }
		internal static IfcStructuralLoadConfiguration Parse(string strDef) { IfcStructuralLoadConfiguration l = new IfcStructuralLoadConfiguration(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadConfiguration l, List<string> arrFields, ref int ipos)
		{
			IfcStructuralLoad.parseFields(l, arrFields, ref ipos);
			l.mValues = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			string s = arrFields[ipos++];
			if (s != "$")
			{
				List<string> fields = ParserSTEP.SplitLineFields(s.Substring(1,s.Length-2));
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
		protected override string BuildStringSTEP()
		{
			string s = ",$";
			if (mLocations.Count > 0)
			{
				s = ",((" + ParserSTEP.DoubleToString(mLocations[0][0]) + (mLocations[0].Count > 1 ? "," + ParserSTEP.DoubleToString(mLocations[0][1]) : "");
				for (int icounter = 1; icounter < mLocations.Count; icounter++)
					s += "),(" + ParserSTEP.DoubleToString(mLocations[icounter][0]) + (mLocations[icounter].Count > 1 ? "," + ParserSTEP.DoubleToString(mLocations[icounter][1]) : "");
				s += "))";
			}
			return base.BuildStringSTEP() + "," + ParserSTEP.ListLinksToString(mValues) + s;
		}
	}
	public abstract partial class IfcStructuralLoadOrResult : IfcStructuralLoad // ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralLoadStatic, IfcSurfaceReinforcementArea))
	{
		protected IfcStructuralLoadOrResult() : base() { }
		protected IfcStructuralLoadOrResult(DatabaseIfc db) : base(db) { }
		protected IfcStructuralLoadOrResult(DatabaseIfc db, IfcStructuralLoadOrResult l) : base(db,l) { }
		protected static void parseFields(IfcStructuralLoadOrResult l, List<string> arrFields, ref int ipos) { IfcStructuralLoad.parseFields(l, arrFields, ref ipos); }
	}
	public partial class IfcStructuralLoadCase : IfcStructuralLoadGroup //IFC4
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcStructuralLoadGroup" : base.KeyWord); } }

		internal Tuple<double,double,double> mSelfWeightCoefficients = null;// : OPTIONAL LIST [3:3] OF IfcRatioMeasure; 
		public Tuple<double,double,double> SelfWeightCoefficients { get { return mSelfWeightCoefficients; } set { mSelfWeightCoefficients = value; } }

		internal IfcStructuralLoadCase() : base() { }
		internal IfcStructuralLoadCase(DatabaseIfc db, IfcStructuralLoadCase c) : base(db,c) { mSelfWeightCoefficients = c.mSelfWeightCoefficients; }
		public IfcStructuralLoadCase(IfcStructuralAnalysisModel sm, string name, IfcActionTypeEnum action, IfcActionSourceTypeEnum source, double coeff, string purpose)
			: base(sm, name, IfcLoadGroupTypeEnum.LOAD_CASE, action, source, coeff, purpose) { new IfcRelAssignsToGroup(this) { Name = Name + " Actions", Description = Description }; }
		internal new static IfcStructuralLoadCase Parse(string strDef) { IfcStructuralLoadCase g = new IfcStructuralLoadCase(); int ipos = 0; parseFields(g, ParserSTEP.SplitLineFields(strDef), ref ipos); return g; }
		internal static void parseFields(IfcStructuralLoadCase g, List<string> arrFields, ref int ipos) { IfcStructuralLoadGroup.parseFields(g, arrFields, ref ipos); string s = arrFields[ipos++]; if (s.StartsWith("(")) { List<string> fields = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2)); g.mSelfWeightCoefficients = new Tuple<double,double,double>(double.Parse(fields[0]), double.Parse(fields[1]), double.Parse(fields[2])); } }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mSelfWeightCoefficients != null ? ",(" + ParserSTEP.DoubleToString(mSelfWeightCoefficients.Item1) + "," + ParserSTEP.DoubleToString(mSelfWeightCoefficients.Item2) + "," + ParserSTEP.DoubleToString(mSelfWeightCoefficients.Item3) + ")" : ",$")); }
	}
	public partial class IfcStructuralLoadGroup : IfcGroup
	{
		internal IfcLoadGroupTypeEnum mPredefinedType = IfcLoadGroupTypeEnum.NOTDEFINED;// : IfcLoadGroupTypeEnum;
		internal IfcActionTypeEnum mActionType = IfcActionTypeEnum.NOTDEFINED;// : IfcActionTypeEnum;
		internal IfcActionSourceTypeEnum mActionSource = IfcActionSourceTypeEnum.NOTDEFINED;//: IfcActionSourceTypeEnum;
		internal double mCoefficient = 0;//: OPTIONAL IfcRatioMeasure;
		internal string mPurpose = "$";// : OPTIONAL IfcLabel; 
		//INVERSE 
		//SourceOfResultGroup	 :	SET [0:1] OF IfcStructuralResultGroup FOR ResultForLoadGroup;
		internal List<IfcStructuralAnalysisModel> mLoadGroupFor = new List<IfcStructuralAnalysisModel>();//	 :	SET [0:?] OF IfcStructuralAnalysisModel 

		public IfcLoadGroupTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcActionTypeEnum ActionType { get { return mActionType; } set { mActionType = value; } }
		public IfcActionSourceTypeEnum ActionSource { get { return mActionSource; } set { mActionSource = value; } }
		public double Coefficient { get { return mCoefficient; } set { mCoefficient = value; } }
		public string Purpose { get { return (mPurpose == "$" ? "" : ParserIfc.Decode(mPurpose)); } set { mPurpose = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		internal IfcStructuralLoadGroup() : base() { }
		internal IfcStructuralLoadGroup(DatabaseIfc db, IfcStructuralLoadGroup g) : base(db,g) { mPredefinedType = g.mPredefinedType; mActionType = g.mActionType; mActionSource = g.mActionSource; mCoefficient = g.mCoefficient; mPurpose = g.mPurpose; }
		public IfcStructuralLoadGroup(IfcStructuralAnalysisModel sm, string name, IfcLoadGroupTypeEnum type, IfcActionTypeEnum action, IfcActionSourceTypeEnum source, double coeff, string purpose)
			: base(sm.mDatabase, name) { mLoadGroupFor.Add(sm); sm.addLoadGroup(this); mPredefinedType = type; mActionType = action; mActionSource = source; mCoefficient = coeff; if (!string.IsNullOrEmpty(purpose)) mPurpose = purpose; }
		public IfcStructuralLoadGroup(IfcStructuralAnalysisModel sm, string name, List<double> factors, List<IfcStructuralLoadGroup> cases, bool ULS)
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + ".,." + mActionType.ToString() + ".,." + mActionSource.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mCoefficient) + (mPurpose == "$" ? ",$" : ",'" + mPurpose + "'"); }
	}
	public partial class IfcStructuralLoadLinearForce : IfcStructuralLoadStatic
	{
		internal double mLinearForceX = 0, mLinearForceY = 0, mLinearForceZ = 0; // : OPTIONAL IfcLinearForceMeasure
		internal double mLinearMomentX = 0, mLinearMomentY = 0, mLinearMomentZ = 0;// : OPTIONAL IfcLinearMomentMeasure; 

		public double LinearForceX { get { return mLinearForceX; } set { mLinearForceX = value; } }
		public double LinearForceY { get { return mLinearForceY; } set { mLinearForceY = value; } }
		public double LinearForceZ { get { return mLinearForceZ; } set { mLinearForceZ = value; } }
		public double LinearMomentX { get { return mLinearMomentX; } set { mLinearMomentX = value; } }
		public double LinearMomentY { get { return mLinearMomentY; } set { mLinearMomentY = value; } }
		public double LinearMomentZ { get { return mLinearMomentZ; } set { mLinearMomentZ = value; } }

		internal IfcStructuralLoadLinearForce() : base() { }
		public IfcStructuralLoadLinearForce(DatabaseIfc db) : base(db) { }
		internal IfcStructuralLoadLinearForce(DatabaseIfc db, IfcStructuralLoadLinearForce f) : base(db,f) { mLinearForceX = f.mLinearForceX; mLinearForceY = f.mLinearForceY; mLinearForceZ = f.mLinearForceZ; mLinearMomentX = f.mLinearMomentX; mLinearMomentY = f.mLinearMomentY; mLinearMomentZ = f.mLinearMomentZ; }
		public IfcStructuralLoadLinearForce(DatabaseIfc db, double forceX, double forceY, double forceZ, double momentX, double momentY, double momentZ) : base(db) { mLinearForceX = forceX; mLinearForceY = forceY; mLinearForceZ = forceZ; mLinearMomentX = momentX; mLinearMomentY = momentY; mLinearMomentZ = momentZ; }
		internal static IfcStructuralLoadLinearForce Parse(string strDef) { IfcStructuralLoadLinearForce l = new IfcStructuralLoadLinearForce(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadLinearForce l, List<string> arrFields, ref int ipos)
		{ IfcStructuralLoadStatic.parseFields(l, arrFields, ref ipos); l.mLinearForceX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mLinearForceY= ParserSTEP.ParseDouble(arrFields[ipos++]); l.mLinearForceZ = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mLinearMomentX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mLinearMomentY = ParserSTEP.ParseDouble(arrFields[ipos++]);  l.mLinearMomentZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mLinearForceX) + "," + ParserSTEP.DoubleOptionalToString(mLinearForceY) + "," + ParserSTEP.DoubleOptionalToString(mLinearForceZ) + "," + ParserSTEP.DoubleOptionalToString(mLinearMomentX) + "," + ParserSTEP.DoubleOptionalToString(mLinearMomentY) + "," + ParserSTEP.DoubleOptionalToString(mLinearMomentZ); }
	}
	public partial class IfcStructuralLoadPlanarForce : IfcStructuralLoadStatic
	{
		internal double mPlanarForceX = 0, mPlanarForceY = 0, mPlanarForceZ = 0;// : OPTIONAL IfcPlanarForceMeasure; 
		internal IfcStructuralLoadPlanarForce() : base() { }
		internal IfcStructuralLoadPlanarForce(DatabaseIfc db) : base(db) { }
		internal IfcStructuralLoadPlanarForce(DatabaseIfc db, IfcStructuralLoadPlanarForce f) : base(db,f) { mPlanarForceX = f.mPlanarForceX; mPlanarForceY = f.mPlanarForceY; mPlanarForceZ = f.mPlanarForceZ; }

		internal static IfcStructuralLoadPlanarForce Parse(string strDef) { IfcStructuralLoadPlanarForce l = new IfcStructuralLoadPlanarForce(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadPlanarForce l, List<string> arrFields, ref int ipos) { IfcStructuralLoadStatic.parseFields(l, arrFields, ref ipos); l.mPlanarForceX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mPlanarForceY = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mPlanarForceZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mPlanarForceX) + "," + ParserSTEP.DoubleOptionalToString(mPlanarForceY) + "," + ParserSTEP.DoubleOptionalToString(mPlanarForceZ); }
	}
	public partial class IfcStructuralLoadSingleDisplacement : IfcStructuralLoadStatic
	{
		internal double mDisplacementX = 0, mDisplacementY = 0, mDisplacementZ = 0;// : OPTIONAL IfcLengthMeasure;
		internal double mRotationalDisplacementRX = 0, mRotationalDisplacementRY = 0, mRotationalDisplacementRZ = 0;// : OPTIONAL IfcPlaneAngleMeasure; 
		internal IfcStructuralLoadSingleDisplacement() : base() { }
		public IfcStructuralLoadSingleDisplacement(DatabaseIfc db) : base(db) { }
		internal IfcStructuralLoadSingleDisplacement(DatabaseIfc db, IfcStructuralLoadSingleDisplacement d) : base(db,d) { mDisplacementX = d.mDisplacementX; mDisplacementY = d.mDisplacementY; mDisplacementZ = d.mDisplacementZ; mRotationalDisplacementRX = d.mRotationalDisplacementRX; mRotationalDisplacementRY = d.mRotationalDisplacementRY; mRotationalDisplacementRZ = d.mRotationalDisplacementRZ; }
		internal static IfcStructuralLoadSingleDisplacement Parse(string strDef) { IfcStructuralLoadSingleDisplacement l = new IfcStructuralLoadSingleDisplacement(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadSingleDisplacement l, List<string> arrFields, ref int ipos) { IfcStructuralLoadStatic.parseFields(l, arrFields, ref ipos); l.mDisplacementX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mDisplacementY = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mDisplacementZ = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mRotationalDisplacementRX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mRotationalDisplacementRY = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mRotationalDisplacementRZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mDisplacementX) + "," + ParserSTEP.DoubleOptionalToString(mDisplacementY) + "," + ParserSTEP.DoubleOptionalToString(mDisplacementZ) + "," + ParserSTEP.DoubleOptionalToString(mRotationalDisplacementRX) + "," + ParserSTEP.DoubleOptionalToString(mRotationalDisplacementRY) + "," + ParserSTEP.DoubleOptionalToString(mRotationalDisplacementRZ); }
	}
	public partial class IfcStructuralLoadSingleDisplacementDistortion : IfcStructuralLoadSingleDisplacement
	{
		internal double mDistortion;// : OPTIONAL IfcCurvatureMeasure;
		internal IfcStructuralLoadSingleDisplacementDistortion() : base() { }
		internal IfcStructuralLoadSingleDisplacementDistortion(DatabaseIfc db, IfcStructuralLoadSingleDisplacementDistortion d) : base(db,d) { mDistortion = d.mDistortion; }
		internal new static IfcStructuralLoadSingleDisplacementDistortion Parse(string strDef) { IfcStructuralLoadSingleDisplacementDistortion l = new IfcStructuralLoadSingleDisplacementDistortion(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadSingleDisplacementDistortion l, List<string> arrFields, ref int ipos) { IfcStructuralLoadSingleDisplacement.parseFields(l, arrFields, ref ipos); l.mDistortion = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mDistortion); }
	}
	public partial class IfcStructuralLoadSingleForce : IfcStructuralLoadStatic
	{
		internal double mForceX = 0, mForceY = 0, mForceZ = 0;// : OPTIONAL IfcForceMeasure;
		internal double mMomentX = 0, mMomentY = 0, mMomentZ = 0;// : OPTIONAL IfcTorqueMeasure; 

		public double ForceX { get { return mForceX; } set { mForceX = value; } }
		public double ForceY { get { return mForceY; } set { mForceY = value; } }
		public double ForceZ { get { return mForceZ; } set { mForceZ = value; } }
		public double MomentX { get { return mMomentX; } set { mMomentX = value; } }
		public double MomentY { get { return mMomentY; } set { mMomentY = value; } }
		public double MomentZ { get { return mMomentZ; } set { mMomentZ = value; } }

		internal IfcStructuralLoadSingleForce() : base() { }
		public IfcStructuralLoadSingleForce(DatabaseIfc db) : base(db) { }
		internal IfcStructuralLoadSingleForce(DatabaseIfc db, IfcStructuralLoadSingleForce f) : base(db,f) { mForceX = f.mForceX; mForceY = f.mForceY; mForceZ = f.mForceZ; mMomentX = f.mMomentX; mMomentY = f.mMomentY; mMomentZ = f.mMomentZ; }
		public IfcStructuralLoadSingleForce(DatabaseIfc db, double forceX, double forceY,double forceZ) : base(db) { mForceX = forceX; mForceY = forceY; mForceZ = forceZ; }
		internal static IfcStructuralLoadSingleForce Parse(string strDef) { IfcStructuralLoadSingleForce l = new IfcStructuralLoadSingleForce(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadSingleForce l, List<string> arrFields, ref int ipos) { IfcStructuralLoadStatic.parseFields(l, arrFields, ref ipos); l.mForceX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mForceY = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mForceZ = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mMomentX = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mMomentY = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mMomentZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mForceX) + "," + ParserSTEP.DoubleOptionalToString(mForceY) + "," + ParserSTEP.DoubleOptionalToString(mForceZ) + "," + ParserSTEP.DoubleOptionalToString(mMomentX) + "," + ParserSTEP.DoubleOptionalToString(mMomentY) + "," + ParserSTEP.DoubleOptionalToString(mMomentZ); }
	}
	public partial class IfcStructuralLoadSingleForceWarping : IfcStructuralLoadSingleForce
	{
		internal double mWarpingMoment;// : OPTIONAL IfcWarpingMomentMeasure;
		internal IfcStructuralLoadSingleForceWarping() : base() { }
		internal IfcStructuralLoadSingleForceWarping(DatabaseIfc db, IfcStructuralLoadSingleForceWarping w) : base(db,w) { mWarpingMoment = w.mWarpingMoment; }
		internal new static IfcStructuralLoadSingleForceWarping Parse(string strDef) { IfcStructuralLoadSingleForceWarping l = new IfcStructuralLoadSingleForceWarping(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadSingleForceWarping l, List<string> arrFields, ref int ipos) { IfcStructuralLoadSingleForce.parseFields(l, arrFields, ref ipos); l.mWarpingMoment = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mWarpingMoment); }
	}
	public abstract partial class IfcStructuralLoadStatic : IfcStructuralLoadOrResult /*ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralLoadLinearForce ,IfcStructuralLoadPlanarForce ,IfcStructuralLoadSingleDisplacement ,IfcStructuralLoadSingleForce ,IfcStructuralLoadTemperature))*/
	{
		protected IfcStructuralLoadStatic() : base() { }
		protected IfcStructuralLoadStatic(DatabaseIfc db) : base(db) { }
		protected IfcStructuralLoadStatic(DatabaseIfc db, IfcStructuralLoadStatic l) : base(db,l) { }
		protected static void parseFields(IfcStructuralLoadStatic l, List<string> arrFields, ref int ipos) { IfcStructuralLoadOrResult.parseFields(l, arrFields, ref ipos); }
	}
	public partial class IfcStructuralLoadTemperature : IfcStructuralLoadStatic
	{
		internal double mDeltaT_Constant, mDeltaT_Y, mDeltaT_Z;// : OPTIONAL IfcThermodynamicTemperatureMeasure; 
		public double DeltaT_Constant { get { return mDeltaT_Constant; } set { mDeltaT_Constant = value; } }
		public double DeltaT_Y { get { return mDeltaT_Y; } set { mDeltaT_Y = value; } }
		public double DeltaT_Z { get { return mDeltaT_Z; } set { mDeltaT_Z = value; } }

		internal IfcStructuralLoadTemperature() : base() { }
		internal IfcStructuralLoadTemperature(DatabaseIfc db, IfcStructuralLoadTemperature t) : base(db,t) { mDeltaT_Constant = t.mDeltaT_Constant; mDeltaT_Y = t.mDeltaT_Y; mDeltaT_Z = t.mDeltaT_Z; }
		public IfcStructuralLoadTemperature(DatabaseIfc db, double T, double TY, double TZ) : base(db) { mDeltaT_Constant = T; mDeltaT_Y = TY; mDeltaT_Z = TZ; }
		internal static IfcStructuralLoadTemperature Parse(string strDef) { IfcStructuralLoadTemperature l = new IfcStructuralLoadTemperature(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcStructuralLoadTemperature l, List<string> arrFields, ref int ipos) { IfcStructuralLoadStatic.parseFields(l, arrFields, ref ipos); l.mDeltaT_Constant = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mDeltaT_Y = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mDeltaT_Z = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mDeltaT_Constant) + "," + ParserSTEP.DoubleOptionalToString(mDeltaT_Y) + "," + ParserSTEP.DoubleOptionalToString(mDeltaT_Z); }
	}
	public abstract partial class IfcStructuralMember : IfcStructuralItem //ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralCurveMember, IfcStructuralSurfaceMember))
	{
		//INVERSE
		internal List<IfcRelConnectsStructuralMember> mConnectedBy = new List<IfcRelConnectsStructuralMember>();// : SET [0:?] OF IfcRelConnectsStructuralMember FOR RelatingStructuralMember 
		internal IfcRelConnectsStructuralElement mStructuralMemberForGG = null;

		protected IfcStructuralMember() : base() { }
		protected IfcStructuralMember(DatabaseIfc db, IfcStructuralMember m) : base(db,m)
		{
			foreach (IfcRelConnectsStructuralMember sm in m.mConnectedBy)
				(db.Factory.Duplicate(sm) as IfcRelConnectsStructuralMember).RelatingStructuralMember = this;
			
		}
		protected IfcStructuralMember(IfcStructuralAnalysisModel sm, IfcMaterialSelect ms, int id) : base(sm,id) { MaterialSelect = ms;  }
		
		protected static void parseFields(IfcStructuralMember m, List<string> arrFields, ref int ipos) { IfcStructuralItem.parseFields(m, arrFields, ref ipos); }

		public IfcMaterialSelect MaterialSelect
		{
			get { return GetMaterialSelect(); }
			set { base.setMaterial(value); }
		}
	}
	public partial class IfcStructuralPlanarAction : IfcStructuralSurfaceAction // Ifc2x3 IfcStructuralAction
	{
		internal IfcStructuralPlanarAction() : base() { }
		internal IfcStructuralPlanarAction(DatabaseIfc db, IfcStructuralPlanarAction a) : base(db,a) {  }
		internal IfcStructuralPlanarAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoadPlanarForce load, bool global, bool projected)
			: base(lc, item, load, global, projected, IfcStructuralSurfaceActivityTypeEnum.CONST) { if (mDatabase.mRelease == ReleaseVersion.IFC2x3) throw new Exception(KeyWord + " in IFC4 Only"); }
		internal IfcStructuralPlanarAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoadTemperature load, bool global, bool projected)
			: base(lc, item, load, global, projected, IfcStructuralSurfaceActivityTypeEnum.CONST) { if (mDatabase.mRelease == ReleaseVersion.IFC2x3) throw new Exception(KeyWord + " in IFC4 Only"); }

		internal new static IfcStructuralPlanarAction Parse(string strDef, ReleaseVersion schema) { IfcStructuralPlanarAction a = new IfcStructuralPlanarAction(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcStructuralPlanarAction a, List<string> arrFields, ref int ipos,ReleaseVersion schema) { IfcStructuralSurfaceAction.parseFields(a, arrFields, ref ipos,schema); }
	}
	// IfcStructuralPlanarActionVarying : IfcStructuralPlanarAction //DELETED IFC4
	public partial class IfcStructuralPointAction : IfcStructuralAction
	{
		internal IfcStructuralPointAction() : base() { }
		internal IfcStructuralPointAction(DatabaseIfc db, IfcStructuralPointAction a) : base(db,a) { }
		public IfcStructuralPointAction(IfcStructuralLoadCase lc, IfcStructuralPointConnection point, IfcStructuralLoadSingleForce l, bool global) : base(lc, point, l, global) { }
		public IfcStructuralPointAction(IfcStructuralLoadCase lc, IfcStructuralPointConnection point, IfcStructuralLoadSingleDisplacement l, bool global) : base(lc, point, l, global) { }
		internal static IfcStructuralPointAction Parse(string strDef,ReleaseVersion schema)
		{
			IfcStructuralPointAction a = new IfcStructuralPointAction();
			int ipos = 0;
			parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema);
			return a;
		}
		internal static void parseFields(IfcStructuralPointAction a, List<string> arrFields, ref int ipos,ReleaseVersion schema) { IfcStructuralAction.parseFields(a, arrFields, ref ipos,schema); }
	}
	public partial class IfcStructuralPointConnection : IfcStructuralConnection
	{
		private int mConditionCoordinateSystem = 0;//	:	OPTIONAL IfcAxis2Placement3D;

		public IfcAxis2Placement3D ConditionCoordinateSystem { get { return mDatabase[mConditionCoordinateSystem] as IfcAxis2Placement3D; } set { mConditionCoordinateSystem = (value == null ? 0 : value.mIndex); } }
		public new IfcBoundaryNodeCondition AppliedCondition { get { return mDatabase[mAppliedCondition] as IfcBoundaryNodeCondition; } set { mAppliedCondition = (value == null ? 0 : value.mIndex); } }

		public IfcStructuralPointConnection() : base() { }
		internal IfcStructuralPointConnection(DatabaseIfc db, IfcStructuralPointConnection c) : base(db,c) { }
		public IfcStructuralPointConnection(IfcStructuralAnalysisModel sm, IfcVertexPoint point)
			: base(sm) { Representation = new IfcProductDefinitionShape(IfcTopologyRepresentation.getRepresentation(point)); }
		internal static IfcStructuralPointConnection Parse(string strDef) { IfcStructuralPointConnection c = new IfcStructuralPointConnection(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcStructuralPointConnection c, List<string> arrFields, ref int ipos)
		{
			IfcStructuralConnection.parseFields(c, arrFields, ref ipos);
			if (ipos < arrFields.Count)
				c.mConditionCoordinateSystem = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mConditionCoordinateSystem == 0 ? ",$" : ",#" + mConditionCoordinateSystem)); }

		public IfcVertexPoint Vertex
		{
			get
			{
				IfcProductDefinitionShape ps = Representation as IfcProductDefinitionShape;
				if (ps != null)
				{
					List<IfcShapeModel> reps = ps.Representations;
					foreach (IfcRepresentation r in reps)
					{
						IfcTopologyRepresentation tr = r as IfcTopologyRepresentation;
						if (tr != null)
						{
							List<IfcRepresentationItem> items = tr.Items;
							foreach (IfcRepresentationItem ri in items)
							{
								IfcVertexPoint vp = ri as IfcVertexPoint;
								if (vp != null)
									return vp;
							}
						}
					}
				}
				return null;
			}
		}
	}
	public partial class IfcStructuralPointReaction : IfcStructuralReaction
	{
		internal IfcStructuralPointReaction() : base() { }
		internal IfcStructuralPointReaction(DatabaseIfc db, IfcStructuralPointReaction r) : base(db,r) { }
		internal static IfcStructuralPointReaction Parse(string strDef) { IfcStructuralPointReaction r = new IfcStructuralPointReaction(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
		internal static void parseFields(IfcStructuralPointReaction r, List<string> arrFields, ref int ipos) { IfcStructuralReaction.parseFields(r, arrFields, ref ipos); }
	}
	public partial class IfcStructuralProfileProperties : IfcGeneralProfileProperties //IFC4 DELETED Entity replaced by IfcProfileProperties
	{
		internal double mTorsionalConstantX = double.NaN, mMomentOfInertiaYZ = double.NaN, mMomentOfInertiaY = double.NaN, mMomentOfInertiaZ = double.NaN;// : OPTIONAL IfcMomentOfInertiaMeasure;
		internal double mWarpingConstant = double.NaN;// : OPTIONAL IfcWarpingConstantMeasure;
		internal double mShearCentreZ = double.NaN, mShearCentreY = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal double mShearDeformationAreaZ = double.NaN, mShearDeformationAreaY = double.NaN;// : OPTIONAL IfcAreaMeasure;
		internal double mMaximumSectionModulusY = double.NaN, mMinimumSectionModulusY = double.NaN, mMaximumSectionModulusZ = double.NaN, mMinimumSectionModulusZ = double.NaN;// : OPTIONAL IfcSectionModulusMeasure;
		internal double mTorsionalSectionModulus = double.NaN;// : OPTIONAL IfcSectionModulusMeasure;
		internal double mCentreOfGravityInX = double.NaN, mCentreOfGravityInY = double.NaN;// : OPTIONAL IfcLengthMeasure; 

		public double TorsionalConstantX { get { return mTorsionalConstantX; } set { mTorsionalConstantX = value; } }
		public double MomentOfInertiaYZ { get { return mMomentOfInertiaYZ; } set { mMomentOfInertiaYZ = value; } }
		public double MomentOfInertiaY { get { return mMomentOfInertiaY; } set { mMomentOfInertiaY = value; } }
		public double MomentOfInertiaZ { get { return mMomentOfInertiaZ; } set { mMomentOfInertiaZ = value; } }
		public double WarpingConstant { get { return mWarpingConstant; } set { mWarpingConstant = value; } }
		public double ShearCentreZ { get { return mShearCentreZ; } set { mShearCentreZ = value; } }
		public double ShearCentreY { get { return mShearCentreY; } set { mShearCentreY = value; } }
		public double ShearDeformationAreaZ { get { return mShearDeformationAreaZ; } set { mShearDeformationAreaZ = value; } }
		public double ShearDeformationAreaY { get { return mShearDeformationAreaY; } set { mShearDeformationAreaY = value; } }
		public double MaximumSectionModulusY { get { return mMaximumSectionModulusY; } set { mMaximumSectionModulusY = value; } }
		public double MinimumSectionModulusY { get { return mMinimumSectionModulusY; } set { mMinimumSectionModulusY = value; } }
		public double MaximumSectionModulusZ { get { return mMaximumSectionModulusZ; } set { mMaximumSectionModulusZ = value; } }
		public double MinimumSectionModulusZ { get { return mMinimumSectionModulusZ; } set { mMinimumSectionModulusZ = value; } }
		public double TorsionalSectionModulus { get { return mTorsionalSectionModulus; } set { mTorsionalSectionModulus = value; } }
		public double CentreOfGravityInX { get { return mCentreOfGravityInX; } set { mCentreOfGravityInX = value; } }
		public double CentreOfGravityInY { get { return mCentreOfGravityInY; } set { mCentreOfGravityInY = value; } }

		internal IfcStructuralProfileProperties() : base() { }
		internal IfcStructuralProfileProperties(DatabaseIfc db, IfcStructuralProfileProperties p) : base(db, p)
		{
			mTorsionalConstantX = p.mTorsionalConstantX; mMomentOfInertiaYZ = p.mMomentOfInertiaYZ; mMomentOfInertiaY = p.mMomentOfInertiaY;
			mMomentOfInertiaZ = p.mMomentOfInertiaZ; mWarpingConstant = p.mWarpingConstant;
			mShearCentreZ = p.mShearCentreZ; mShearCentreY = p.mShearCentreY;
			mShearDeformationAreaZ = p.mShearDeformationAreaZ; mShearDeformationAreaY = p.mShearDeformationAreaY;
			mMaximumSectionModulusY = p.mMaximumSectionModulusY; mMinimumSectionModulusY = p.mMinimumSectionModulusY; mMaximumSectionModulusZ = p.mMaximumSectionModulusZ; mMinimumSectionModulusZ = p.mMinimumSectionModulusZ;
			mTorsionalSectionModulus = p.mTorsionalSectionModulus;
			mCentreOfGravityInX = p.mCentreOfGravityInX; mCentreOfGravityInY = p.mCentreOfGravityInY;
		}
		public IfcStructuralProfileProperties(string name, IfcProfileDef p) : base(name, p) { }

		internal static void parseFields(IfcStructuralProfileProperties gp, List<string> arrFields, ref int ipos,ReleaseVersion schema)
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
		internal new static IfcStructuralProfileProperties Parse(string strDef,ReleaseVersion schema) { IfcStructuralProfileProperties p = new IfcStructuralProfileProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mTorsionalConstantX) + "," + ParserSTEP.DoubleOptionalToString(mMomentOfInertiaYZ) + "," + ParserSTEP.DoubleOptionalToString(mMomentOfInertiaY) + "," + ParserSTEP.DoubleOptionalToString(mMomentOfInertiaZ) + "," +
				ParserSTEP.DoubleOptionalToString(mWarpingConstant) + "," + ParserSTEP.DoubleOptionalToString(mShearCentreZ) + "," + ParserSTEP.DoubleOptionalToString(mShearCentreY) + "," + ParserSTEP.DoubleOptionalToString(mShearDeformationAreaZ) + "," +
				ParserSTEP.DoubleOptionalToString(mShearDeformationAreaY) + "," + ParserSTEP.DoubleOptionalToString(mMaximumSectionModulusY) + "," + ParserSTEP.DoubleOptionalToString(mMinimumSectionModulusY) + "," + ParserSTEP.DoubleOptionalToString(mMaximumSectionModulusZ) + "," +
				ParserSTEP.DoubleOptionalToString(mMinimumSectionModulusZ) + "," + ParserSTEP.DoubleOptionalToString(mTorsionalSectionModulus) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInX) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY);
		}
	}
	public abstract partial class IfcStructuralReaction : IfcStructuralActivity
	{   //INVERSE 
		//internal List<int> mCauses = new List<int>();// : OPTIONAL IfcStructuralReaction;
		protected IfcStructuralReaction() : base() { }
		protected IfcStructuralReaction(DatabaseIfc db, IfcStructuralReaction p) : base(db,p) { }
		protected static void parseFields(IfcStructuralReaction a, List<string> arrFields, ref int ipos) { IfcStructuralActivity.parseFields(a, arrFields, ref ipos); }
	}
	public partial class IfcStructuralResultGroup : IfcGroup
	{
		internal IfcAnalysisTheoryTypeEnum mTheoryType = IfcAnalysisTheoryTypeEnum.NOTDEFINED;// : IfcAnalysisTheoryTypeEnum;
		internal int mResultForLoadGroup;// : OPTIONAL IfcStructuralLoadGroup;
		internal bool mIsLinear = false;// : BOOLEAN; 

		public IfcStructuralLoadGroup ResultForLoadGroup { get { return mDatabase[mResultForLoadGroup] as IfcStructuralLoadGroup; } set { mResultForLoadGroup = (value == null ? 0 : value.mIndex); } }

		internal IfcStructuralResultGroup() : base() { }
		internal IfcStructuralResultGroup(DatabaseIfc db, IfcStructuralResultGroup g) : base(db, g)
		{
			mTheoryType = g.mTheoryType;
			if(g.mResultForLoadGroup > 0)
				ResultForLoadGroup = db.Factory.Duplicate( g.ResultForLoadGroup) as IfcStructuralLoadGroup;
			mIsLinear = g.mIsLinear;
		}
		internal IfcStructuralResultGroup(DatabaseIfc m, string name) : base(m, name) { }
		internal new static IfcStructuralResultGroup Parse(string strDef) { IfcStructuralResultGroup g = new IfcStructuralResultGroup(); int ipos = 0; parseFields(g, ParserSTEP.SplitLineFields(strDef), ref ipos); return g; }
		internal static void parseFields(IfcStructuralResultGroup g, List<string> arrFields, ref int ipos)
		{
			IfcGroup.parseFields(g, arrFields, ref ipos);
			g.mTheoryType = (IfcAnalysisTheoryTypeEnum)Enum.Parse(typeof(IfcAnalysisTheoryTypeEnum), arrFields[ipos++].Replace(".", ""));
			g.mResultForLoadGroup = ParserSTEP.ParseLink(arrFields[ipos++]);
			g.mIsLinear = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mTheoryType.ToString() + ".," + ParserSTEP.LinkToString(mResultForLoadGroup) + "," + ParserSTEP.BoolToString(mIsLinear); }
	}
	public partial class IfcStructuralSteelProfileProperties : IfcStructuralProfileProperties //IFC4 DELETED Entity replaced by IfcProfileProperties
	{
		internal double mShearAreaZ = double.NaN;// : OPTIONAL IfcAreaMeasure;
		internal double mShearAreaY = double.NaN;// : OPTIONAL IfcAreaMeasure;
		internal double mPlasticShapeFactorY = double.NaN;// : OPTIONAL IfcPositiveRatioMeasure;
		internal double mPlasticShapeFactorZ = double.NaN;// : OPTIONAL IfcPositiveRatioMeasure; 

		public double ShearAreaZ { get { return mShearAreaZ; } set { mShearAreaZ = value; } }
		public double ShearAreaY { get { return mShearAreaY; } set { mShearAreaY = value; } }
		public double PlasticShapeFactorY { get { return mPlasticShapeFactorY; } set { mPlasticShapeFactorY = value; } }
		public double PlasticShapeFactorZ { get { return mPlasticShapeFactorZ; } set { mPlasticShapeFactorZ = value; } }

		internal IfcStructuralSteelProfileProperties() : base() { }
		internal IfcStructuralSteelProfileProperties(DatabaseIfc db, IfcStructuralSteelProfileProperties p) : base(db, p) { mShearAreaZ = p.mShearAreaZ; mShearAreaY = p.mShearAreaY; mPlasticShapeFactorY = p.mPlasticShapeFactorY; mPlasticShapeFactorZ = p.mPlasticShapeFactorZ; }
		internal static void parseFields(IfcStructuralSteelProfileProperties gp, List<string> arrFields, ref int ipos,ReleaseVersion schema) { IfcStructuralProfileProperties.parseFields(gp, arrFields, ref ipos,schema); gp.mShearAreaZ = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mShearAreaY = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mPlasticShapeFactorY = ParserSTEP.ParseDouble(arrFields[ipos++]); gp.mPlasticShapeFactorZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal new static IfcStructuralSteelProfileProperties Parse(string strDef, ReleaseVersion schema) { IfcStructuralSteelProfileProperties p = new IfcStructuralSteelProfileProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mShearAreaZ) + "," + ParserSTEP.DoubleOptionalToString(mShearAreaY) + "," + ParserSTEP.DoubleOptionalToString(mPlasticShapeFactorY) + "," + ParserSTEP.DoubleOptionalToString(mPlasticShapeFactorZ); }
	}
	public partial class IfcStructuralSurfaceAction : IfcStructuralAction //IFC4 SUPERTYPE OF(IfcStructuralPlanarAction)
	{
		internal IfcProjectedOrTrueLengthEnum mProjectedOrTrue = IfcProjectedOrTrueLengthEnum.TRUE_LENGTH;// : IfcProjectedOrTrueLengthEnum
		internal IfcStructuralSurfaceActivityTypeEnum mPredefinedType = IfcStructuralSurfaceActivityTypeEnum.NOTDEFINED;//IfcStructuralCurveActivityTypeEnum
		internal IfcStructuralSurfaceAction() : base() { }
		internal IfcStructuralSurfaceAction(DatabaseIfc db, IfcStructuralSurfaceAction a) : base(db,a) { mProjectedOrTrue = a.mProjectedOrTrue; mPredefinedType = a.mPredefinedType; }
		internal IfcStructuralSurfaceAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, bool global, bool projected, IfcStructuralSurfaceActivityTypeEnum type)
			: base(lc, item, load, global) { if (mDatabase.mRelease == ReleaseVersion.IFC2x3) throw new Exception(KeyWord + " in IFC4 Only"); mProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH; mPredefinedType = type; }
		internal static IfcStructuralSurfaceAction Parse(string strDef, ReleaseVersion schema) { IfcStructuralSurfaceAction a = new IfcStructuralSurfaceAction(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
		internal static void parseFields(IfcStructuralSurfaceAction a, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcStructuralAction.parseFields(a, arrFields, ref ipos,schema);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mProjectedOrTrue = (IfcProjectedOrTrueLengthEnum)Enum.Parse(typeof(IfcProjectedOrTrueLengthEnum), s.Replace(".", ""));
			if (schema != ReleaseVersion.IFC2x3)
				a.mPredefinedType = (IfcStructuralSurfaceActivityTypeEnum)Enum.Parse(typeof(IfcStructuralSurfaceActivityTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mProjectedOrTrue.ToString() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "." : ".,." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcStructuralSurfaceConnection : IfcStructuralConnection
	{
		internal IfcStructuralSurfaceConnection() : base() { }
		internal IfcStructuralSurfaceConnection(DatabaseIfc db, IfcStructuralSurfaceConnection c) : base(db,c) { }
		
		internal static IfcStructuralSurfaceConnection Parse(string strDef) { IfcStructuralSurfaceConnection c = new IfcStructuralSurfaceConnection(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcStructuralSurfaceConnection c, List<string> arrFields, ref int ipos) { IfcStructuralConnection.parseFields(c, arrFields, ref ipos); }
	}
	public partial class IfcStructuralSurfaceMember : IfcStructuralMember
	{
		internal IfcStructuralSurfaceTypeEnum mPredefinedType = IfcStructuralSurfaceTypeEnum.NOTDEFINED;// : IfcStructuralSurfaceTypeEnum;
		internal double mThickness;// : OPTIONAL IfcPositiveLengthMeasure; 

		public IfcStructuralSurfaceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public double Thickness { get { return mThickness; } set { mThickness = value; } }

		public IfcStructuralSurfaceMember() : base() { }
		internal IfcStructuralSurfaceMember(DatabaseIfc db, IfcStructuralSurfaceMember m) : base(db,m) { mPredefinedType = m.mPredefinedType; mThickness = m.mThickness; }
		public IfcStructuralSurfaceMember(IfcStructuralAnalysisModel sm, IfcFaceSurface f, IfcMaterial material, int id, double thickness)
			: base(sm, material, id)
		{
			IfcTopologyRepresentation tr = new IfcTopologyRepresentation(f, "Reference");
			Representation = new IfcProductRepresentation(tr);
			mThickness = thickness;
		}

		internal static IfcStructuralSurfaceMember Parse(string strDef) { IfcStructuralSurfaceMember m = new IfcStructuralSurfaceMember(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcStructuralSurfaceMember sm, List<string> arrFields, ref int ipos) { IfcStructuralMember.parseFields(sm, arrFields, ref ipos); sm.mPredefinedType = (IfcStructuralSurfaceTypeEnum)Enum.Parse(typeof(IfcStructuralSurfaceTypeEnum), arrFields[ipos++].Replace(".", "")); sm.mThickness = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + ".," + ParserSTEP.DoubleOptionalToString(mThickness); }
	}
	public partial class IfcStructuralSurfaceMemberVarying : IfcStructuralSurfaceMember
	{
		internal List<double> mSubsequentThickness = new List<double>();// : LIST [2:?] OF IfcPositiveLengthMeasure;
		internal int mVaryingThicknessLocation;// : IfcShapeAspect; 

		public IfcShapeAspect VaryingThicknessLocation { get { return mDatabase[mVaryingThicknessLocation] as IfcShapeAspect; } set { mVaryingThicknessLocation = value.mIndex; } }

		internal IfcStructuralSurfaceMemberVarying() : base() { }
		internal IfcStructuralSurfaceMemberVarying(DatabaseIfc db, IfcStructuralSurfaceMemberVarying m) : base(db,m) { mSubsequentThickness.AddRange(m.mSubsequentThickness); mVaryingThicknessLocation = m.mVaryingThicknessLocation; }
		internal new static IfcStructuralSurfaceMemberVarying Parse(string strDef) { IfcStructuralSurfaceMemberVarying m = new IfcStructuralSurfaceMemberVarying(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcStructuralSurfaceMemberVarying sm, List<string> arrFields, ref int ipos)
		{
			IfcStructuralSurfaceMember.parseFields(sm, arrFields, ref ipos);
			List<string> lst = ParserSTEP.SplitLineFields(arrFields[ipos++]);
			for (int icounter = 0; icounter < lst.Count; icounter++)
				sm.mSubsequentThickness.Add(ParserSTEP.ParseDouble(lst[icounter]));
			sm.mVaryingThicknessLocation = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.DoubleToString(mSubsequentThickness[0]);
			for (int icounter = 1; icounter < mSubsequentThickness.Count; icounter++)
				str += "," + ParserSTEP.DoubleToString(mSubsequentThickness[icounter]);
			return str + ")" + "," + ParserSTEP.LinkToString(mVaryingThicknessLocation);
		}
	}
	public partial class IfcStructuredDimensionCallout : IfcDraughtingCallout // DEPRECEATED IFC4
	{
		internal IfcStructuredDimensionCallout() : base() { }
		//internal IfcStructuredDimensionCallout(IfcStructuredDimensionCallout i) : base(i) { }
		internal new static IfcStructuredDimensionCallout Parse(string strDef) { IfcStructuredDimensionCallout d = new IfcStructuredDimensionCallout(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		internal static void parseFields(IfcStructuredDimensionCallout d, List<string> arrFields, ref int ipos) { IfcDraughtingCallout.parseFields(d, arrFields, ref ipos); }
	}
	public partial interface IfcStyleAssignmentSelect : IBaseClassIfc //(IfcPresentationStyle ,IfcPresentationStyleAssignment); 
	{
		List<IfcStyledItem> StyledItems { get; }
	}
	public partial class IfcStyledItem : IfcRepresentationItem
	{
		private int mItem;// : OPTIONAL IfcRepresentationItem;
		private List<int> mStyles = new List<int>();// : SET [1:?] OF IfcStyleAssignmentSelect; ifc2x3 IfcPresentationStyleAssignment;
		private string mName = "$";// : OPTIONAL IfcLabel; 

		public IfcRepresentationItem Item
		{
			get { return mDatabase[mItem] as IfcRepresentationItem; }
			set
			{
				if (value == null)
					mItem = 0;
				else
				{
					mItem = value.mIndex;
					value.mStyledByItem = this;
				}
			}
		}
		public List<IfcStyleAssignmentSelect> Styles { get { return mStyles.ConvertAll(x => mDatabase[x] as IfcStyleAssignmentSelect); } set { mStyles = value.ConvertAll(x => x.Index); } }
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		internal IfcStyledItem() : base() { }
		internal IfcStyledItem(DatabaseIfc db, IfcStyledItem i) : base(db, i)
		{
			//if (i.mItem > 0)
			//	Item = db.Factory.Duplicate(i.Item) as IfcRepresentationItem;
			Styles = i.mStyles.ConvertAll(x => db.Factory.Duplicate(i.mDatabase[x]) as IfcStyleAssignmentSelect);
			mName = i.mName;
		}
		public IfcStyledItem(IfcStyleAssignmentSelect style, string name) : base(style.Database)
		{
			Name = name;
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
			{
				IfcPresentationStyle ps = style as IfcPresentationStyle;
				if (ps != null)
					mStyles.Add(new IfcPresentationStyleAssignment(ps).mIndex);
				else
					throw new Exception("XX Invalid style for schema " + mDatabase.mRelease.ToString());
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
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mItem);
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

		internal override void postParseRelate()
		{
			base.postParseRelate();
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
		
		public override List<T> Extract<T>()
		{
			List<T> result = base.Extract<T>();
			foreach (int i in mStyles)
				result.AddRange(mDatabase[i].Extract<T>());
			return result;
		}
	}
	public partial class IfcStyledRepresentation : IfcStyleModel
	{
		public new List<IfcStyledItem> Items { get { return base.Items.ConvertAll(x => x as IfcStyledItem); } set { base.Items = value.ConvertAll(x => x as IfcRepresentationItem); } }

		internal IfcStyledRepresentation() : base() { }
		internal IfcStyledRepresentation(DatabaseIfc db, IfcStyledRepresentation r) : base(db, r) { }
		public IfcStyledRepresentation(IfcStyledItem ri) : base(ri) { }
		public IfcStyledRepresentation(List<IfcStyledItem> reps) : base(reps.ConvertAll(x => x as IfcRepresentationItem)) { }
		internal new static IfcStyledRepresentation Parse(string strDef)
		{
			IfcStyledRepresentation r = new IfcStyledRepresentation();
			int pos = 0;
			IfcShapeModel.parseString(r, strDef, ref pos);
			return r;
		}
	}
	public abstract partial class IfcStyleModel : IfcRepresentation  //ABSTRACT SUPERTYPE OF(IfcStyledRepresentation)
	{
		protected IfcStyleModel() : base() { }
		protected IfcStyleModel(DatabaseIfc db, IfcStyleModel m) : base(db, m) { }
		protected IfcStyleModel(IfcRepresentationItem ri) : base(ri) { }
		protected IfcStyleModel(List<IfcRepresentationItem> reps) : base(reps, "", "") { }

		protected static void parseString(IfcStyleModel shape, string str, ref int pos) { IfcRepresentation.parseString(shape, str, ref pos); }
	}
	public partial class IfcSubContractResource : IfcConstructionResource
	{
		internal IfcSubContractResourceTypeEnum mPredefinedType = IfcSubContractResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcSubContractResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSubContractResource() : base() { }
		internal IfcSubContractResource(DatabaseIfc db, IfcSubContractResource r) : base(db,r) { mPredefinedType = r.mPredefinedType; }
		internal IfcSubContractResource(DatabaseIfc db) : base(db) { }
		internal static IfcSubContractResource Parse(string strDef, ReleaseVersion schema) { IfcSubContractResource r = new IfcSubContractResource(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return r; }
		internal static void parseFields(IfcSubContractResource r, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcConstructionResource.parseFields(r, arrFields, ref ipos,schema);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					r.mPredefinedType = (IfcSubContractResourceTypeEnum)Enum.Parse(typeof(IfcSubContractResourceTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcSubContractResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcSubContractResourceTypeEnum mPredefinedType = IfcSubContractResourceTypeEnum.NOTDEFINED;
		public IfcSubContractResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSubContractResourceType() : base() { }
		internal IfcSubContractResourceType(DatabaseIfc db, IfcSubContractResourceType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcSubContractResourceType(DatabaseIfc m, string name, IfcSubContractResourceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcSubContractResourceType t, List<string> arrFields, ref int ipos) { IfcSubContractResourceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSubContractResourceTypeEnum)Enum.Parse(typeof(IfcSubContractResourceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSubContractResourceType Parse(string strDef) { IfcSubContractResourceType t = new IfcSubContractResourceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcSubedge : IfcEdge
	{
		internal int mParentEdge;// IfcEdge;
		public IfcEdge ParentEdge { get { return mDatabase[mParentEdge] as IfcEdge; } set { mParentEdge = value.mIndex; } }

		internal IfcSubedge() : base() { }
		internal IfcSubedge(DatabaseIfc db, IfcSubedge e) : base(db,e) { ParentEdge = db.Factory.Duplicate(e.ParentEdge) as IfcEdge; }
		public IfcSubedge(IfcVertex v1, IfcVertex v2, IfcEdge e) : base(v1, v2) { mParentEdge = e.mIndex; }
		internal new static IfcSubedge Parse(string strDef) { IfcSubedge e = new IfcSubedge(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		internal static void parseFields(IfcSubedge e, List<string> arrFields, ref int ipos) { IfcEdge.parseFields(e, arrFields, ref ipos); e.mParentEdge = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mParentEdge); }
	}
	public abstract partial class IfcSurface : IfcGeometricRepresentationItem, IfcGeometricSetSelect, IfcSurfaceOrFaceSurface  /*	ABSTRACT SUPERTYPE OF (ONEOF(IfcBoundedSurface,IfcElementarySurface,IfcSweptSurface))*/
	{
		protected IfcSurface() : base() { }
		protected IfcSurface(DatabaseIfc db, IfcSurface s) : base(db,s) { }
		protected IfcSurface(DatabaseIfc db) : base(db) { }
		protected static void parseFields(IfcSurface s, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcSurfaceCurve : IfcCurve //IFC4 Add2
	{
		private int mCurve3D;//: IfcCurve;
		internal List<int> mAssociatedGeometry = new List<int>();// : SET [1:2] OF IfcTrimmingSelect;
		internal IfcPreferredSurfaceCurveRepresentation mMasterRepresentation = IfcPreferredSurfaceCurveRepresentation.CURVE3D;// : IfcPreferredSurfaceCurveRepresentation; 

		public IfcCurve Curve3D { get { return mDatabase[mCurve3D] as IfcCurve; } set { mCurve3D = value.mIndex; } }
		public List<IfcPCurve> AssociatedGeometry { get { return mAssociatedGeometry.ConvertAll(x => mDatabase[x] as IfcPCurve); } set { mAssociatedGeometry = value.ConvertAll(x => x.mIndex); } }
		public IfcPreferredSurfaceCurveRepresentation MasterRepresentation { get { return mMasterRepresentation; } set { mMasterRepresentation = value; } }

		internal IfcSurfaceCurve() : base() { }
		internal IfcSurfaceCurve(DatabaseIfc db, IfcSurfaceCurve c) : base(db, c)
		{
			Curve3D = db.Factory.Duplicate(c.Curve3D) as IfcCurve;
			AssociatedGeometry = c.AssociatedGeometry.ConvertAll(x => db.Factory.Duplicate(x) as IfcPCurve);
			mMasterRepresentation = c.mMasterRepresentation;
		}
		internal IfcSurfaceCurve(IfcCurve curve, IfcPCurve p1, IfcPreferredSurfaceCurveRepresentation cr) : base(curve.mDatabase)
		{
			Curve3D = curve;
			AssociatedGeometry = new List<IfcPCurve>() { p1 };
			mMasterRepresentation = cr;
		}
		internal IfcSurfaceCurve(IfcCurve curve, IfcPCurve p1, IfcPCurve p2, IfcPreferredSurfaceCurveRepresentation cr) : base(curve.mDatabase)
		{
			Curve3D = curve;
			AssociatedGeometry = new List<IfcPCurve>() { p1, p2 };
			mMasterRepresentation = cr;
		}
		internal static void parseFields(IfcSurfaceCurve c, List<string> arrFields, ref int ipos)
		{
			IfcCurve.parseFields(c, arrFields, ref ipos);
			c.mCurve3D = ParserSTEP.ParseLink(arrFields[ipos++]);
			c.mAssociatedGeometry = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			c.mMasterRepresentation = (IfcPreferredSurfaceCurveRepresentation)Enum.Parse(typeof(IfcPreferredSurfaceCurveRepresentation), arrFields[ipos++].Replace(".", ""));
		}
		internal static IfcSurfaceCurve Parse(string strDef) { IfcSurfaceCurve c = new IfcSurfaceCurve(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mCurve3D) + ",(#" + mAssociatedGeometry[0] + (mAssociatedGeometry.Count == 2 ? ",#" + mAssociatedGeometry[1] : "") + "),." + mMasterRepresentation.ToString() + ".";
		}
	}
	public partial class IfcSurfaceCurveSweptAreaSolid : IfcSweptAreaSolid
	{
		internal int mDirectrix; // : IfcCurve;
		internal double mStartParam = 0;// : OPT IfcParameterValue; OPT IFC4
		internal double mEndParam;//: OPT IfcParameterValue; OPT IFC4
		internal int mReferenceSurface;// : IfcSurface; 

		public IfcCurve Directrix { get { return mDatabase[mDirectrix] as IfcCurve; } set { mDirectrix = value.mIndex; } }
		public IfcSurface ReferenceSurface { get { return mDatabase[mReferenceSurface] as IfcSurface; } set { mReferenceSurface = value.mIndex; } }

		internal IfcSurfaceCurveSweptAreaSolid() : base() { }
		internal IfcSurfaceCurveSweptAreaSolid(DatabaseIfc db, IfcSurfaceCurveSweptAreaSolid s) : base(db, s) { Directrix = db.Factory.Duplicate(s.Directrix) as IfcCurve; mStartParam = s.mStartParam; mEndParam = s.mEndParam; ReferenceSurface = db.Factory.Duplicate(s.ReferenceSurface) as IfcSurface; }

		internal static void parseFields(IfcSurfaceCurveSweptAreaSolid s, List<string> arrFields, ref int ipos) { IfcSweptAreaSolid.parseFields(s, arrFields, ref ipos); s.mDirectrix = ParserSTEP.ParseLink(arrFields[ipos++]); s.mStartParam = ParserSTEP.ParseDouble(arrFields[ipos++]); s.mEndParam = ParserSTEP.ParseDouble(arrFields[ipos++]); s.mReferenceSurface = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcSurfaceCurveSweptAreaSolid Parse(string strDef) { IfcSurfaceCurveSweptAreaSolid s = new IfcSurfaceCurveSweptAreaSolid(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mDirectrix) + "," + ParserSTEP.DoubleToString(mStartParam) + "," + ParserSTEP.DoubleToString(mEndParam) + "," + ParserSTEP.LinkToString(mReferenceSurface); }
	}
	public partial class IfcSurfaceOfLinearExtrusion : IfcSweptSurface
	{
		internal int mExtrudedDirection;//  : IfcDirection;
		internal double mDepth;// : IfcLengthMeasure;

		public IfcDirection ExtrudedDirection { get { return mDatabase[mExtrudedDirection] as IfcDirection; } set { mExtrudedDirection = value.mIndex; } }

		internal IfcSurfaceOfLinearExtrusion() : base() { }
		internal IfcSurfaceOfLinearExtrusion(DatabaseIfc db, IfcSurfaceOfLinearExtrusion s) : base(db,s) { ExtrudedDirection = db.Factory.Duplicate(s.ExtrudedDirection) as IfcDirection; mDepth = s.mDepth; }

		internal static void parseFields(IfcSurfaceOfLinearExtrusion s, List<string> arrFields, ref int ipos) { IfcSweptSurface.parseFields(s, arrFields, ref ipos); s.mExtrudedDirection = ParserSTEP.ParseLink(arrFields[ipos++]); s.mDepth = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcSurfaceOfLinearExtrusion Parse(string strDef) { IfcSurfaceOfLinearExtrusion s = new IfcSurfaceOfLinearExtrusion(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mExtrudedDirection) + "," + ParserSTEP.DoubleToString(mDepth); }
	}
	public partial class IfcSurfaceOfRevolution : IfcSweptSurface
	{
		internal int mAxisPosition;//  : IfcAxis1Placement;
		public IfcAxis1Placement AxisPosition { get { return mDatabase[mAxisPosition] as IfcAxis1Placement; } set { mAxisPosition = value.mIndex; } }

		internal IfcSurfaceOfRevolution() : base() { }
		internal IfcSurfaceOfRevolution(DatabaseIfc db, IfcSurfaceOfRevolution s) : base(db,s) { AxisPosition = db.Factory.Duplicate( s.AxisPosition) as IfcAxis1Placement; }

		internal static void parseFields(IfcSurfaceOfRevolution s, List<string> arrFields, ref int ipos) { IfcSweptSurface.parseFields(s, arrFields, ref ipos); s.mAxisPosition = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcSurfaceOfRevolution Parse(string strDef) { IfcSurfaceOfRevolution s = new IfcSurfaceOfRevolution(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mAxisPosition); }
	}
	public interface IfcSurfaceOrFaceSurface : IBaseClassIfc { }  // = SELECT (	IfcSurface, IfcFaceSurface, IfcFaceBasedSurfaceModel);
	public partial class IfcSurfaceStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal IfcSurfaceSide mSide = IfcSurfaceSide.BOTH;// : IfcSurfaceSide;
		internal List<int> mStyles = new List<int>();// : SET [1:5] OF IfcSurfaceStyleElementSelect; 

		public List<IfcSurfaceStyleElementSelect> Styles { get { return mStyles.ConvertAll(x => mDatabase[x] as IfcSurfaceStyleElementSelect); } set { mStyles = value.ConvertAll(x => x.Index); } }

		internal IfcSurfaceStyle() : base() { }
		internal IfcSurfaceStyle(DatabaseIfc db, IfcSurfaceStyle s) : base(db,s) { mSide = s.mSide; Styles = s.mStyles.ConvertAll(x=>db.Factory.Duplicate(s.mDatabase[x]) as IfcSurfaceStyleElementSelect ); }
		internal IfcSurfaceStyle(DatabaseIfc db) : base(db,"") { }
		public IfcSurfaceStyle(IfcSurfaceStyleShading shading, IfcSurfaceStyleLighting lighting, IfcSurfaceStyleWithTextures textures, IfcExternallyDefinedSurfaceStyle external, IfcSurfaceStyleRefraction refraction)
			:base(shading != null ? shading.mDatabase : (lighting != null ? lighting.mDatabase : (textures != null ? textures.mDatabase : (external != null ? external.mDatabase : refraction.mDatabase))),"")
		{
			List<IfcSurfaceStyleElementSelect> styles = new List<IfcSurfaceStyleElementSelect>();
			if (shading != null)
				styles.Add(shading);
			if (lighting != null)
				styles.Add(lighting);
			if (textures != null)
				styles.Add(textures);
			if (external != null)
				styles.Add(external);
			if (refraction != null)
				styles.Add(refraction);
			Styles = styles;
		}
		internal static void parseFields(IfcSurfaceStyle s, List<string> arrFields, ref int ipos)
		{
			IfcPresentationStyle.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str.StartsWith("."))
				s.mSide = (IfcSurfaceSide)Enum.Parse(typeof(IfcSurfaceSide), str.Replace(".", ""));
			s.mStyles = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		internal static IfcSurfaceStyle Parse(string strDef) { IfcSurfaceStyle s = new IfcSurfaceStyle(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",." + mSide.ToString() + ".,(" + ParserSTEP.LinkToString(mStyles[0]);
			for (int icounter = 1; icounter < mStyles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mStyles[icounter]);
			return str + ")";
		}

		public override List<T> Extract<T>()
		{
			List<T> result = base.Extract<T>();
			foreach (int i in mStyles)
				result.AddRange(mDatabase[i].Extract<T>());
			return result;
		}
	}
	public partial interface IfcSurfaceStyleElementSelect : IBaseClassIfc //SELECT(IfcSurfaceStyleShading, IfcSurfaceStyleLighting, IfcSurfaceStyleWithTextures
	{ //, IfcExternallyDefinedSurfaceStyle, IfcSurfaceStyleRefraction);
	}
	public partial class IfcSurfaceStyleLighting : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal int mDiffuseTransmissionColour, mDiffuseReflectionColour, mTransmissionColour, mReflectanceColour;//	 :	IfcColourRgb;
		internal IfcSurfaceStyleLighting() : base() { }
		internal IfcSurfaceStyleLighting(DatabaseIfc db, IfcSurfaceStyleLighting i) : base(db,i)
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
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mDiffuseTransmissionColour) + "," + ParserSTEP.LinkToString(mDiffuseReflectionColour) + "," + ParserSTEP.LinkToString(mTransmissionColour) + "," + ParserSTEP.LinkToString(mReflectanceColour); }
	}
	public partial class IfcSurfaceStyleRefraction : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal double mRefractionIndex, mDispersionFactor;//	 :	OPTIONAL IfcReal;
		internal IfcSurfaceStyleRefraction() : base() { }
		internal IfcSurfaceStyleRefraction(DatabaseIfc db, IfcSurfaceStyleRefraction s) : base(db, s)
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
		protected override string BuildStringSTEP() { return  base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mRefractionIndex) + "," + ParserSTEP.DoubleOptionalToString(mDispersionFactor); }
	}
	public partial class IfcSurfaceStyleRendering : IfcSurfaceStyleShading
	{
		internal double mTransparency = double.NaN;// : OPTIONAL IfcNormalisedRatioMeasure;
		internal IfcColourOrFactor mDiffuseColour, mTransmissionColour, mDiffuseTransmissionColour, mReflectionColour, mSpecularColour;//:	OPTIONAL IfcColourOrFactor;
		internal IfcSpecularHighlightSelect mSpecularHighlight;// : OPTIONAL 
		internal IfcReflectanceMethodEnum mReflectanceMethod = IfcReflectanceMethodEnum.NOTDEFINED;// : IfcReflectanceMethodEnum; 
		internal IfcSurfaceStyleRendering() : base() { }
		internal IfcSurfaceStyleRendering(DatabaseIfc db, IfcSurfaceStyleRendering r) : base(db, r)
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
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mTransparency) +
				(mDiffuseColour == null ? ",$" : "," + mDiffuseColour.ToString()) + (mTransmissionColour == null ? ",$" : "," + mTransmissionColour.ToString()) +
				(mDiffuseTransmissionColour == null ? ",$" : "," + mDiffuseTransmissionColour) + (mReflectionColour == null ? ",$" : mReflectionColour.ToString()) + (mSpecularColour == null ? ",$" : "," + mSpecularColour.ToString()) +
				(mSpecularHighlight == null ? ",$" : "," + mSpecularHighlight.ToString()) + ",." + mReflectanceMethod.ToString() + ".";
		}
	}
	public partial class IfcSurfaceStyleShading : IfcPresentationItem, IfcSurfaceStyleElementSelect //SUPERTYPE OF(IfcSurfaceStyleRendering)
	{
		private int mSurfaceColour;// : IfcColourRgb;
		public IfcColourRgb SurfaceColour { get { return mDatabase[mSurfaceColour] as IfcColourRgb; } set { mSurfaceColour = value.mIndex; } }

		internal IfcSurfaceStyleShading() : base() { }
		internal IfcSurfaceStyleShading(DatabaseIfc db, IfcSurfaceStyleShading s) : base(db,s) { SurfaceColour = db.Factory.Duplicate( s.SurfaceColour) as IfcColourRgb; }
		public IfcSurfaceStyleShading(IfcColourRgb colour) : base(colour.mDatabase) { SurfaceColour = colour; }
		public IfcSurfaceStyleShading(DatabaseIfc db, Color colour) : base(db) { SurfaceColour =  new IfcColourRgb(db,"",colour); }
		internal static void parseFields(IfcSurfaceStyleShading s, List<string> arrFields, ref int ipos) { s.mSurfaceColour = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcSurfaceStyleShading Parse(string strDef) { IfcSurfaceStyleShading s = new IfcSurfaceStyleShading(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mSurfaceColour); }
	}
	public partial class IfcSurfaceStyleWithTextures : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal List<int> mTextures = new List<int>();//: LIST [1:?] OF IfcSurfaceTexture; 
		internal IfcSurfaceStyleWithTextures() : base() { }
		public IfcSurfaceStyleWithTextures(IfcSurfaceTexture texture) : base(texture.mDatabase) { mTextures.Add(texture.mIndex); }
	//	internal IfcSurfaceStyleWithTextures(IfcSurfaceStyleWithTextures i) : base() { mTextures = new List<int>(i.mTextures.ToArray()); }
		internal IfcSurfaceStyleWithTextures(List<IfcSurfaceTexture> textures) : base(textures[0].mDatabase) { mTextures = textures.ConvertAll(x => x.mIndex);  }
		internal static void parseFields(IfcSurfaceStyleWithTextures s, List<string> arrFields, ref int ipos) { s.mTextures = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcSurfaceStyleWithTextures Parse(string strDef) { IfcSurfaceStyleWithTextures s = new IfcSurfaceStyleWithTextures(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mTextures[0]);
			for (int icounter = 1; icounter < mTextures.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mTextures[icounter]);
			return str + ")";
		}
	}
	public abstract partial class IfcSurfaceTexture : IfcPresentationItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcBlobTexture ,IfcImageTexture ,IfcPixelTexture));
	{
		internal bool mRepeatS;// : BOOLEAN;
		internal bool mRepeatT;// : BOOLEAN;
		internal string mMode = "$"; //:	OPTIONAL IfcIdentifier; ifc2x3 IfcSurfaceTextureEnum mTextureType;// 
		internal int mTextureTransform;// : OPTIONAL IfcCartesianTransformationOperator2D;
		internal List<string> mParameter = new List<string>();// IFC4 OPTIONAL LIST [1:?] OF IfcIdentifier;

		public bool RepeatS { get { return mRepeatS; } set { mRepeatS = value; } }
		public bool RepeatT { get { return mRepeatT; } set { mRepeatT = value; } }
		public string Mode { get { return (mMode == "$" ? "" : ParserIfc.Decode(mMode)); } set { mMode = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcCartesianTransformationOperator2D TextureTransform { get { return mDatabase[mTextureTransform] as IfcCartesianTransformationOperator2D; } set { mTextureTransform = (value == null ? 0 : value.mIndex); } }
		public List<string> Parameters { get { return mParameter.ConvertAll(x => ParserIfc.Decode(x)); } set { mParameter = (value == null ? new List<string>() : value.ConvertAll(x => ParserIfc.Encode(x))); } }

		protected IfcSurfaceTexture() : base() { }
		protected IfcSurfaceTexture(DatabaseIfc db, IfcSurfaceTexture t) : base(db,t) { mRepeatS = t.mRepeatS; mRepeatT = t.mRepeatT; mMode = t.mMode; mTextureTransform = t.mTextureTransform; }
		protected IfcSurfaceTexture(DatabaseIfc db,bool repeatS, bool repeatT) : base(db) { mRepeatS = repeatS; mRepeatT = repeatT; }
		protected static void parseFields(IfcSurfaceTexture t, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			t.mRepeatS = ParserSTEP.ParseBool(arrFields[ipos++]);
			t.mRepeatT = ParserSTEP.ParseBool(arrFields[ipos++]);
			t.mMode = (schema == ReleaseVersion.IFC2x3 ? arrFields[ipos++].Replace(".", "") : arrFields[ipos++].Replace("'", ""));
			t.mTextureTransform = ParserSTEP.ParseLink(arrFields[ipos++]);
			string str = arrFields[ipos++];
			if (str != "$")
				t.mParameter = ParserSTEP.SplitListStrings(str.Substring(1, str.Length - 2));
		}
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + "," + ParserSTEP.BoolToString(mRepeatS) + "," + ParserSTEP.BoolToString(mRepeatT);
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3)
			{
				IfcSurfaceTextureEnum texture = IfcSurfaceTextureEnum.NOTDEFINED;
				if (!Enum.TryParse<IfcSurfaceTextureEnum>(mMode,true, out texture))
					texture = IfcSurfaceTextureEnum.NOTDEFINED;
				result += ",." + texture.ToString() + ".,"+ ParserSTEP.LinkToString(mTextureTransform);
			}
			else
			{
				result += (mMode == "$" ? ",$," : ",'" + mMode + "',") + ParserSTEP.LinkToString(mTextureTransform);
				if (mParameter.Count == 0)
					result += ",$";
				else
				{
					result += ",('" + mParameter[0];
					for (int icounter = 1; icounter < mParameter.Count; icounter++)
						result += "','" + mParameter[icounter];
					result += "')";
				}
			}
			return result;
		}
	}
	public abstract partial class IfcSweptAreaSolid : IfcSolidModel  /*ABSTRACT SUPERTYPE OF (ONEOF (IfcExtrudedAreaSolid, IfcFixedReferenceSweptAreaSolid ,IfcRevolvedAreaSolid ,IfcSurfaceCurveSweptAreaSolid))*/
	{
		private int mSweptArea;// : IfcProfileDef;
		private int mPosition;// : IfcAxis2Placement3D; 	 :	OPTIONAL IFC4

		public IfcProfileDef SweptArea { get { return mDatabase[mSweptArea] as IfcProfileDef; } set { mSweptArea = value.mIndex; } }
		public IfcAxis2Placement3D Position { get { return mDatabase[mPosition] as IfcAxis2Placement3D; } set { mPosition = (value == null ? 0 : value.mIndex); } }

		protected IfcSweptAreaSolid() : base() { }
		protected IfcSweptAreaSolid(DatabaseIfc db) : base(db) { }
		protected IfcSweptAreaSolid(DatabaseIfc db, IfcSweptAreaSolid s) : base(db, s)
		{
			SweptArea = db.Factory.Duplicate(s.SweptArea) as IfcProfileDef;
			if (s.mPosition > 0)
				Position = db.Factory.Duplicate(s.Position) as IfcAxis2Placement3D;
		}
		protected IfcSweptAreaSolid(IfcProfileDef sweptArea) : base(sweptArea.mDatabase) { SweptArea = sweptArea; if (sweptArea.mDatabase.Release == ReleaseVersion.IFC2x3) Position = sweptArea.mDatabase.Factory.PlaneXYPlacement; }
		protected IfcSweptAreaSolid(IfcProfileDef prof, IfcAxis2Placement3D position) : this(prof) { Position = position; }

		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mSweptArea) + (mPosition > 0 ? ",#" + mPosition : ",$"); }
		protected static void parseFields(IfcSweptAreaSolid s, List<string> arrFields, ref int ipos) { IfcSolidModel.parseFields(s, arrFields, ref ipos); s.mSweptArea = ParserSTEP.ParseLink(arrFields[ipos++]); s.mPosition = ParserSTEP.ParseLink(arrFields[ipos++]); }

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			SweptArea.changeSchema(schema);
			IfcAxis2Placement3D position = Position;
			if (schema == ReleaseVersion.IFC2x3)
			{
				if (position == null)
					Position = mDatabase.Factory.PlaneXYPlacement;
			}
			else
			{
				if (position != null && position.isWorldXY)
					mPosition = 0;
			}
		}
	}
	public partial class IfcSweptDiskSolid : IfcSolidModel
	{
		internal int mDirectrix;// : IfcCurve;
		internal double mRadius;// : IfcPositiveLengthMeasure;
		internal double mInnerRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mStartParam = double.NaN;// : OPTIONAL IfcParameterValue; IFC4
		internal double mEndParam = double.NaN;// : OPTIONAL IfcParameterValue;  IFC4

		public IfcCurve Directrix { get { return mDatabase[mDirectrix] as IfcCurve; } set { mDirectrix = value.mIndex; } }

		internal IfcSweptDiskSolid() : base() { }
		internal IfcSweptDiskSolid(DatabaseIfc db, IfcSweptDiskSolid s) : base(db,s) { Directrix = db.Factory.Duplicate(s.Directrix) as IfcCurve; mRadius = s.mRadius; mInnerRadius = s.mInnerRadius; mStartParam = s.mStartParam; mEndParam = s.mEndParam; }
		public IfcSweptDiskSolid(IfcCurve directrix, double radius) : base(directrix.mDatabase) { Directrix = directrix; mRadius = radius; }
		public IfcSweptDiskSolid(IfcCurve directrix, double radius, double innerRadius) : base(directrix.mDatabase) { Directrix = directrix; mRadius = radius; mInnerRadius = innerRadius; }

		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mDirectrix) + "," + ParserSTEP.DoubleToString(mRadius) + "," + ParserSTEP.DoubleOptionalToString(mInnerRadius) + "," + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ParserSTEP.DoubleToString(mStartParam) + "," + ParserSTEP.DoubleToString(mEndParam) : ParserSTEP.DoubleOptionalToString(mStartParam) + "," + ParserSTEP.DoubleOptionalToString(mEndParam)); }
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
		internal IfcSweptDiskSolidPolygonal(DatabaseIfc db, IfcSweptDiskSolidPolygonal p) : base(db,p) { mFilletRadius = p.mFilletRadius; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mFilletRadius); }
		internal static void parseFields(IfcSweptDiskSolidPolygonal s, List<string> arrFields, ref int ipos) { IfcSweptDiskSolid.parseFields(s, arrFields, ref ipos); s.mFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal new static IfcSweptDiskSolidPolygonal Parse(string strDef) { IfcSweptDiskSolidPolygonal s = new IfcSweptDiskSolidPolygonal(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
	}
	public abstract partial class IfcSweptSurface : IfcSurface /*	ABSTRACT SUPERTYPE OF (ONEOF (IfcSurfaceOfLinearExtrusion ,IfcSurfaceOfRevolution))*/
	{
		internal int mSweptCurve;// : IfcProfileDef;
		internal int mPosition;// : IfcAxis2Placement3D;

		public IfcProfileDef SweptCurve { get { return mDatabase[mSweptCurve] as IfcProfileDef; } set { mSweptCurve = value.mIndex; } }
		public IfcAxis2Placement3D Position { get { return mDatabase[mPosition] as IfcAxis2Placement3D; } set { mSweptCurve = value.mIndex; } }

		protected IfcSweptSurface() : base() { }
		protected IfcSweptSurface(DatabaseIfc db, IfcSweptSurface s) : base(db,s) { SweptCurve = db.Factory.Duplicate(s.SweptCurve) as IfcProfileDef; Position = db.Factory.Duplicate(s.Position) as IfcAxis2Placement3D; }
		protected static void parseFields(IfcSweptSurface s, List<string> arrFields, ref int ipos) { IfcSurface.parseFields(s, arrFields, ref ipos); s.mSweptCurve = ParserSTEP.ParseLink(arrFields[ipos++]); s.mPosition = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mSweptCurve) + "," + ParserSTEP.LinkToString(mPosition); }
		internal IfcProfileDef getProfile() { return mDatabase[mSweptCurve] as IfcProfileDef; }
	}
	public partial class IfcSwitchingDevice : IfcFlowController //IFC4
	{
		internal IfcSwitchingDeviceTypeEnum mPredefinedType = IfcSwitchingDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcSwitchingDeviceTypeEnum;
		public IfcSwitchingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSwitchingDevice() : base() { }
		internal IfcSwitchingDevice(DatabaseIfc db, IfcSwitchingDevice d) : base(db, d) { mPredefinedType = d.mPredefinedType; }
		public IfcSwitchingDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcSwitchingDevice s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcSwitchingDeviceTypeEnum)Enum.Parse(typeof(IfcSwitchingDeviceTypeEnum), str);
		}
		internal new static IfcSwitchingDevice Parse(string strDef) { IfcSwitchingDevice s = new IfcSwitchingDevice(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcSwitchingDeviceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcSwitchingDeviceType : IfcFlowControllerType
	{
		internal IfcSwitchingDeviceTypeEnum mPredefinedType = IfcSwitchingDeviceTypeEnum.NOTDEFINED;// : IfcFlowMeterTypeEnum;
		public IfcSwitchingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSwitchingDeviceType() : base() { }
		internal IfcSwitchingDeviceType(DatabaseIfc db, IfcSwitchingDeviceType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcSwitchingDeviceType(DatabaseIfc m, string name, IfcSwitchingDeviceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcSwitchingDeviceType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcSwitchingDeviceTypeEnum)Enum.Parse(typeof(IfcSwitchingDeviceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcSwitchingDeviceType Parse(string strDef) { IfcSwitchingDeviceType t = new IfcSwitchingDeviceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	//ENTITY IfcSymbolStyle // DEPRECEATED IFC4
	public partial class IfcSystem : IfcGroup //SUPERTYPE OF(ONEOF(IfcBuildingSystem, IfcDistributionSystem, IfcStructuralAnalysisModel, IfcZone))
	{
		//INVERSE
		internal IfcRelServicesBuildings mServicesBuildings = null;// : SET [0:1] OF IfcRelServicesBuildings FOR RelatingSystem  
		public IfcRelServicesBuildings ServicesBuildings { get { return mServicesBuildings; } set { mServicesBuildings = value; } }

		internal IfcSystem() : base() { }
		internal IfcSystem(DatabaseIfc db, IfcSystem s) : base(db,s)
		{
			if(s.mServicesBuildings != null)
			{
				IfcRelServicesBuildings rsb = db.Factory.Duplicate(s.mServicesBuildings) as IfcRelServicesBuildings;
				rsb.RelatingSystem = this;
			}
		}
		internal IfcSystem(DatabaseIfc m, string name) : base(m, name) { }
		internal IfcSystem(IfcSpatialElement e, string name) : base(e.mDatabase, name) { mServicesBuildings = new IfcRelServicesBuildings(this, e) { Name = name };  }
		internal new static IfcSystem Parse(string strDef) { IfcSystem s = new IfcSystem(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcSystem s, List<string> arrFields, ref int ipos) { IfcGroup.parseFields(s, arrFields, ref ipos); }
	}
	public partial class IfcSystemFurnitureElement : IfcFurnishingElement //IFC4
	{
		internal IfcSystemFurnitureElementTypeEnum mPredefinedType = IfcSystemFurnitureElementTypeEnum.NOTDEFINED;//: OPTIONAL IfcSystemFurnitureElementTypeEnum
		public IfcSystemFurnitureElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSystemFurnitureElement() : base() { }
		internal IfcSystemFurnitureElement(DatabaseIfc db, IfcSystemFurnitureElement f) : base(db, f) { mPredefinedType = f.mPredefinedType; }
		public IfcSystemFurnitureElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static void parseFields(IfcSystemFurnitureElement e, List<string> arrFields, ref int ipos)
		{
			IfcFurnishingElement.parseFields(e, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				e.mPredefinedType = (IfcSystemFurnitureElementTypeEnum)Enum.Parse(typeof(IfcSystemFurnitureElementTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcSystemFurnitureElement Parse(string strDef) { IfcSystemFurnitureElement e = new IfcSystemFurnitureElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType + "."; }
	}
	public partial class IfcSystemFurnitureElementType : IfcFurnishingElementType
	{
		internal IfcSystemFurnitureElementTypeEnum mPredefinedType = IfcSystemFurnitureElementTypeEnum.NOTDEFINED;//: OPTIONAL IfcSystemFurnitureElementTypeEnum
		public IfcSystemFurnitureElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSystemFurnitureElementType() : base() { }
		internal IfcSystemFurnitureElementType(DatabaseIfc db, IfcSystemFurnitureElementType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcSystemFurnitureElementType(DatabaseIfc db, string name, IfcSystemFurnitureElementTypeEnum type) : base(db,name)
		{
			mPredefinedType = type;
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3 && string.IsNullOrEmpty(ElementType) && type != IfcSystemFurnitureElementTypeEnum.NOTDEFINED)
				ElementType = type.ToString();
		}
		internal static void parseFields(IfcSystemFurnitureElementType t, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcFurnishingElementType.parseFields(t, arrFields, ref ipos);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					t.mPredefinedType = (IfcSystemFurnitureElementTypeEnum)Enum.Parse(typeof(IfcSystemFurnitureElementTypeEnum), s.Substring(1, s.Length - 2));
			}
		}
		internal static IfcSystemFurnitureElementType Parse(string strDef, ReleaseVersion schema) { IfcSystemFurnitureElementType t = new IfcSystemFurnitureElementType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return t; }
	}
}
