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
	public partial class IfcTable : BaseClassIfc, IfcMetricValueSelect
	{
		internal string mName = "$"; //:	OPTIONAL IfcLabel;
		private List<int> mRows = new List<int>();// OPTIONAL LIST [1:?] OF IfcTableRow;
		private List<int> mColumns = new List<int>();// :	OPTIONAL LIST [1:?] OF IfcTableColumn;

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } } 
		public List<IfcTableRow> Rows { get { return mRows.ConvertAll(x => mDatabase[x] as IfcTableRow); } set { mRows = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		public List<IfcTableColumn> Columns { get { return mColumns.ConvertAll(x => mDatabase[x] as IfcTableColumn); } set { mColumns = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }

		internal IfcTable() : base() { }
		public IfcTable(DatabaseIfc db) : base(db) { }
		internal IfcTable(DatabaseIfc db, IfcTable t) : base(db) { mName = t.mName; Rows = t.Rows.ConvertAll(x=>db.Factory.Duplicate(t) as IfcTableRow); Columns = t.Columns.ConvertAll(x=>db.Factory.Duplicate(x) as IfcTableColumn); }
		public IfcTable(string name, List<IfcTableRow> rows, List<IfcTableColumn> cols) : base(rows == null || rows.Count == 0 ? cols[0].mDatabase : rows[0].mDatabase)
		{
			Name = name.Replace("'", "");
			Rows = rows;
			Columns = cols;
		}
		internal static void parseFields(IfcTable t, List<string> arrFields, ref int ipos) { t.mName = arrFields[ipos++]; t.mRows = ParserSTEP.SplitListLinks(arrFields[ipos++]); t.mColumns = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string s = "";
			if (mRows.Count == 0)
				s = "$";
			else
			{
				s = "(" + ParserSTEP.LinkToString(mRows[0]);
				for (int icounter = 1; icounter < mRows.Count; icounter++)
					s += "," + ParserSTEP.LinkToString(mRows[icounter]);
				s += ")";
			}
			if (mDatabase.mRelease != ReleaseVersion.IFC2x3)
			{
				if (mColumns.Count == 0)
					s += ",$";
				else
				{
					s += ",(" + ParserSTEP.LinkToString(mColumns[0]);
					for (int icounter = 1; icounter < mColumns.Count; icounter++)
						s += "," + ParserSTEP.LinkToString(mColumns[icounter]);
					s += ")";
				}
			}
			return base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + s;
		}
		internal static IfcTable Parse(string strDef) { IfcTable t = new IfcTable(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public partial class IfcTableColumn : BaseClassIfc
	{
		internal string mIdentifier = "$";//	 :	OPTIONAL IfcIdentifier;
		internal string mName = "$";//	 :	OPTIONAL IfcLabel;
		internal string mDescription = "$";//	 :	OPTIONAL IfcText;
		internal int mUnit;//	 :	OPTIONAL IfcUnit;
		private int mReferencePath;//	 :	OPTIONAL IfcReference;

		public string Identifier { get { return (mIdentifier == "$" ? "" : ParserIfc.Decode(mIdentifier)); } set { mIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcUnit Unit { get { return mDatabase[mUnit] as IfcUnit; } set { mUnit = (value == null ? 0 : value.Index); } }
		public IfcReference ReferencePath { get { return mDatabase[mReferencePath] as IfcReference; } set { mReferencePath = (value == null ? 0 : value.mIndex); } }

		internal IfcTableColumn() : base() { }
		public IfcTableColumn(DatabaseIfc db) : base(db) { }
		internal IfcTableColumn(DatabaseIfc db, IfcTableColumn c) : base(db,c) { mIdentifier = c.mIdentifier; mName = c.mName; mDescription = c.mDescription; if(c.mUnit >0) Unit = db.Factory.Duplicate(c.mDatabase[ c.mUnit]) as IfcUnit; if(c.mReferencePath > 0) ReferencePath = db.Factory.Duplicate(c.ReferencePath) as IfcReference; }
		 
		internal static void parseFields(IfcTableColumn t, List<string> arrFields, ref int ipos)
		{
			t.mIdentifier = arrFields[ipos++];
			t.mName = arrFields[ipos++].Replace("'", "");
			t.mDescription = arrFields[ipos++];
			t.mUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
			t.mReferencePath = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + (mIdentifier == "$" ? ",$," : ",'" + mIdentifier + "',") + (mName == "$" ? "$," : "'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserSTEP.LinkToString(mUnit) + "," + ParserSTEP.LinkToString(mReferencePath)); }
		internal static IfcTableColumn Parse(string strDef) { IfcTableColumn t = new IfcTableColumn(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public partial class IfcTableRow : BaseClassIfc
	{
		internal List<IfcValue> mRowCells = new List<IfcValue>();// :	OPTIONAL LIST [1:?] OF IfcValue;
		internal bool mIsHeading = false; //:	:	OPTIONAL BOOLEAN;

		public List<IfcValue> RowCells { get { return mRowCells; } set { mRowCells = value; } }
		public bool IsHeading { get { return mIsHeading; } set { mIsHeading = value; } }

		internal IfcTableRow() : base() { }
		internal IfcTableRow(DatabaseIfc db, IfcTableRow r) : base(db,r) { RowCells = r.RowCells; mIsHeading = r.mIsHeading; }
		public IfcTableRow(DatabaseIfc db, IfcValue val) : this(db, new List<IfcValue>() { val }, false) { }
		public IfcTableRow(DatabaseIfc db, List<IfcValue> vals, bool isHeading) : base(db)
		{
			mRowCells.AddRange(vals);
			mIsHeading = isHeading;
		}
		internal static void parseFields(IfcTableRow t, List<string> arrFields, ref int ipos)
		{
			string s = arrFields[ipos++];
			if (s != "$")
			{
				List<string> ss = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < ss.Count; icounter++)
				{
					IfcValue v = ParserIfc.parseValue(ss[icounter]);
					if (v != null)
						t.mRowCells.Add(v);
				}
			}
			t.mIsHeading = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			string s = "";
			if (mRowCells.Count == 0)
				s = ",$,";
			else
			{
				s = ",(" + mRowCells[0].ToString();
				for (int icounter = 1; icounter < mRowCells.Count; icounter++)
					s += "," + mRowCells[icounter].ToString();
				s += "),";
			}
			return base.BuildStringSTEP() + s + ParserSTEP.BoolToString(mIsHeading);
		}
		internal static IfcTableRow Parse(string strDef) { IfcTableRow t = new IfcTableRow(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public partial class IfcTank : IfcFlowStorageDevice //IFC4
	{
		internal IfcTankTypeEnum mPredefinedType = IfcTankTypeEnum.NOTDEFINED;// OPTIONAL : IfcTankTypeEnum;
		public IfcTankTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTank() : base() { }
		internal IfcTank(DatabaseIfc db, IfcTank t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcTank(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcTank s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcTankTypeEnum)Enum.Parse(typeof(IfcTankTypeEnum), str);
		}
		internal new static IfcTank Parse(string strDef) { IfcTank s = new IfcTank(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcTankTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcTankType : IfcFlowStorageDeviceType
	{
		internal IfcTankTypeEnum mPredefinedType = IfcTankTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum; 
		public IfcTankTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTankType() : base() { }
		internal IfcTankType(DatabaseIfc db, IfcTankType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal static void parseFields(IfcTankType t, List<string> arrFields, ref int ipos) { IfcFlowStorageDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcTankTypeEnum)Enum.Parse(typeof(IfcTankTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcTankType Parse(string strDef) { IfcTankType t = new IfcTankType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcTask : IfcProcess //SUPERTYPE OF (ONEOF(IfcMove,IfcOrderAction) both depreceated IFC4) 
	{
		//internal string mTaskId; //  : 	IfcIdentifier; IFC4 midentification
		private string mStatus = "$";// : OPTIONAL IfcLabel;
		internal string mWorkMethod = "$";// : OPTIONAL IfcLabel;
		internal bool mIsMilestone;// : BOOLEAN
		internal int mPriority;// : OPTIONAL INTEGER IFC4
		internal int mTaskTime;// : OPTIONAL IfcTaskTime; IFC4
		internal IfcTaskTypeEnum mPredefinedType = IfcTaskTypeEnum.NOTDEFINED;// : OPTIONAL IfcTaskTypeEnum

		internal string Status { get { return mStatus; } }
		internal IfcTaskTime TaskTime { get { return mDatabase[mTaskTime] as IfcTaskTime; } set { mTaskTime = value == null ? 0 : value.mIndex; } }

		internal IfcTask() : base() { }
		internal IfcTask(DatabaseIfc db, IfcTask t) : base(db,t) { mStatus = t.mStatus; mWorkMethod = t.mWorkMethod; mIsMilestone = t.mIsMilestone; mPriority = t.mPriority; if(t.mTaskTime > 0) TaskTime = db.Factory.Duplicate(t.TaskTime) as IfcTaskTime; mPredefinedType = t.mPredefinedType; }
		
		internal static IfcTask Parse(string strDef, ReleaseVersion schema) { IfcTask t = new IfcTask(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return t; }
		internal static void parseFields(IfcTask t, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcProcess.parseFields(t, arrFields, ref ipos);
			if (schema == ReleaseVersion.IFC2x3)
				t.mIdentification = arrFields[ipos++];
			t.mStatus = arrFields[ipos++];
			t.mWorkMethod = arrFields[ipos++];
			t.mIsMilestone = ParserSTEP.ParseBool(arrFields[ipos++]);
			t.mPriority = ParserSTEP.ParseInt(arrFields[ipos++]);
			if (schema != ReleaseVersion.IFC2x3)
			{
				t.mTaskTime = ParserSTEP.ParseLink(arrFields[ipos++]);
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					t.mPredefinedType = (IfcTaskTypeEnum)Enum.Parse(typeof(IfcTaskTypeEnum), s.Replace(".", ""));
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ",'" + mIdentification + "'" : "") + "," + mStatus + "," + mWorkMethod + "," + ParserSTEP.BoolToString(mIsMilestone) + "," + mPriority.ToString() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.LinkToString(mTaskTime) + ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcTaskTime : IfcSchedulingTime //IFC4
	{
		internal IfcTaskDurationEnum mDurationType = IfcTaskDurationEnum.NOTDEFINED;	// :	OPTIONAL IfcTaskDurationEnum;
		internal string mScheduleDuration = "$";//	 :	OPTIONAL IfcDuration;
		internal string mScheduleStart = "$", mScheduleFinish = "$", mEarlyStart = "$", mEarlyFinish = "$", mLateStart = "$", mLateFinish = "$"; //:	OPTIONAL IfcDateTime;
		internal string mFreeFloat = "$", mTotalFloat = "$";//	 :	OPTIONAL IfcDuration;
		internal bool mIsCritical;//	 :	OPTIONAL BOOLEAN;
		internal string mStatusTime = "$";//	 :	OPTIONAL IfcDateTime;
		internal string mActualDuration = "$";//	 :	OPTIONAL IfcDuration;
		internal string mActualStart = "$", mActualFinish = "$";//	 :	OPTIONAL IfcDateTime;
		internal string mRemainingTime = "$";//	 :	OPTIONAL IfcDuration;
		internal double mCompletion = double.NaN;//	 :	OPTIONAL IfcPositiveRatioMeasure; 

		public IfcTaskDurationEnum DurationType { get { return mDurationType; } set { mDurationType = value; } }
		public IfcDuration ScheduleDuration { get { return IfcDuration.Convert(mScheduleDuration); } set { mScheduleDuration = IfcDuration.Convert(value); } }
		public DateTime ScheduleStart { get { return IfcDateTime.Convert(mScheduleStart); } set { mScheduleStart = IfcDateTime.Convert(value); } }
		public DateTime ScheduleFinish { get { return IfcDateTime.Convert(mScheduleFinish); } set { mScheduleFinish = IfcDateTime.Convert(value); } }
		public DateTime EarlyStart { get { return IfcDateTime.Convert(mEarlyStart); } set { mEarlyStart = IfcDateTime.Convert(value); } }
		public DateTime EarlyFinish { get { return IfcDateTime.Convert(mEarlyFinish); } set { mEarlyFinish = IfcDateTime.Convert(value); } }
		public DateTime LateStart { get { return IfcDateTime.Convert(mLateStart); } set { mLateStart = IfcDateTime.Convert(value); } }
		public DateTime LateFinish { get { return IfcDateTime.Convert(mLateFinish); } set { mLateFinish = IfcDateTime.Convert(value); } }
		public IfcDuration FreeFloat { get { return IfcDuration.Convert(mFreeFloat); } set { mFreeFloat = IfcDuration.Convert(value); } }
		public IfcDuration TotalFloat { get { return IfcDuration.Convert(mTotalFloat); } set { mTotalFloat = IfcDuration.Convert(value); } }
		public bool IsCritical { get { return mIsCritical; } set { mIsCritical = value; } }
		public DateTime StatusTime { get { return IfcDateTime.Convert(mStatusTime); } set { mStatusTime = IfcDateTime.Convert(value); } }
		public IfcDuration ActualDuration { get { return IfcDuration.Convert(mActualDuration); } set { mActualDuration = IfcDuration.Convert(value); } }
		public DateTime ActualStart { get { return IfcDateTime.Convert(mActualStart); } set { mActualStart = IfcDateTime.Convert(value); } }
		public DateTime ActualFinish { get { return IfcDateTime.Convert(mActualFinish); } set { mActualFinish = IfcDateTime.Convert(value); } }
		public IfcDuration RemainingTime { get { return IfcDuration.Convert(mRemainingTime); } set { mRemainingTime = IfcDuration.Convert(value); } }
		public double Completion { get { return mCompletion; } set { mCompletion = value; } }

		internal IfcTaskTime() : base() { }
		internal IfcTaskTime(DatabaseIfc db, IfcTaskTime t) : base(db,t)
		{
			mDurationType = t.mDurationType; mScheduleDuration = t.mScheduleDuration; mScheduleStart = t.mScheduleStart; mScheduleFinish = t.mScheduleFinish;
			mEarlyStart = t.mEarlyStart; mEarlyFinish = t.mEarlyFinish; mLateStart = t.mLateStart; mLateFinish = t.mLateFinish; mFreeFloat = t.mFreeFloat; mTotalFloat = t.mTotalFloat;
			mIsCritical = t.mIsCritical; mStatusTime = t.mStatusTime; mActualDuration = t.mActualDuration; mActualStart = t.mActualStart; mActualFinish = t.mActualFinish;
			mRemainingTime = t.mRemainingTime; mCompletion = t.mCompletion;
		}
		internal IfcTaskTime(DatabaseIfc db) : base(db) { }
		
		internal static IfcTaskTime Parse(string strDef) { IfcTaskTime s = new IfcTaskTime(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcTaskTime s, List<string> arrFields, ref int ipos)
		{
			IfcSchedulingTime.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str.StartsWith("."))
				s.mDurationType = (IfcTaskDurationEnum)Enum.Parse(typeof(IfcTaskDurationEnum), str.Replace(".", ""));
			s.mScheduleDuration = arrFields[ipos++].Replace("'", "");
			s.mScheduleStart = arrFields[ipos++].Replace("'", "");
			s.mScheduleFinish = arrFields[ipos++].Replace("'", "");
			s.mEarlyStart = arrFields[ipos++].Replace("'", "");
			s.mEarlyFinish = arrFields[ipos++].Replace("'", "");
			s.mLateStart = arrFields[ipos++].Replace("'", "");
			s.mLateFinish = arrFields[ipos++].Replace("'", "");
			s.mFreeFloat = arrFields[ipos++].Replace("'", "");
			s.mTotalFloat = arrFields[ipos++].Replace("'", "");
			s.mIsCritical = ParserSTEP.ParseBool(arrFields[ipos++]);
			s.mStatusTime = arrFields[ipos++].Replace("'", "");
			s.mActualDuration = arrFields[ipos++].Replace("'", "");
			s.mActualStart = arrFields[ipos++].Replace("'", "");
			s.mActualFinish = arrFields[ipos++].Replace("'", "");
			s.mRemainingTime = arrFields[ipos++].Replace("'", "");
			s.mCompletion = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",." + mDurationType + (mScheduleDuration == "$" ? ".,$," : ".,'" + mScheduleDuration + "',") + (mScheduleStart == "$" ? "$," : "'" + mScheduleStart + "',") +
				(mScheduleFinish == "$" ? "$," : "'" + mScheduleFinish + "',") + (mEarlyStart == "$" ? "$," : "'" + mEarlyStart + "',") + (mEarlyFinish == "$" ? "$," : "'" + mEarlyFinish + "',") + (mLateStart == "$" ? "$," : "'" + mLateStart + "',") +
				(mLateFinish == "$" ? "$," : "'" + mLateFinish + "',") + (mFreeFloat == "$" ? "$," : "'" + mFreeFloat + "',") + (mTotalFloat == "$" ? "$," : "'" + mTotalFloat + "',") + ParserSTEP.BoolToString(mIsCritical) + "," +
				(mStatusTime == "$" ? "$," : "'" + mStatusTime + "',") + (mActualDuration == "$" ? "$," : "'" + mActualDuration + "',") + (mActualStart == "$" ? "$," : "'" + mActualStart + "',") + (mActualFinish == "$" ? "$," : "'" + mActualFinish + "',") +
				(mRemainingTime == "$" ? "$," : "'" + mRemainingTime + "',") + ParserSTEP.DoubleOptionalToString(mCompletion);
		}
	}
	public partial class IfcTaskType : IfcTypeProcess //IFC4
	{
		internal IfcTaskTypeEnum mPredefinedType = IfcTaskTypeEnum.NOTDEFINED;// : IfcTaskTypeEnum; 
		private string mWorkMethod = "$";// : OPTIONAL IfcLabel;

		public IfcTaskTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public string WorkMethod { get { return (mWorkMethod == "$" ? "" : ParserIfc.Decode(mWorkMethod)); } set { mWorkMethod = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		internal IfcTaskType() : base() { }
		internal IfcTaskType(DatabaseIfc db, IfcTaskType t) : base(db, t) { mPredefinedType = t.mPredefinedType; mWorkMethod = t.mWorkMethod; }
		internal IfcTaskType(DatabaseIfc m, string name, IfcTaskTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcTaskType t, List<string> arrFields, ref int ipos) { IfcTypeProcess.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcTaskTypeEnum)Enum.Parse(typeof(IfcTaskTypeEnum), arrFields[ipos++].Replace(".", "")); t.mWorkMethod = arrFields[ipos++].Replace("'", ""); }
		internal new static IfcTaskType Parse(string strDef) { IfcTaskType t = new IfcTaskType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + (mWorkMethod == "$" ? ".,$" : (".,'" + mWorkMethod + "'"))); }
	}
	public partial class IfcTelecomAddress : IfcAddress
	{
		internal List<string> mTelephoneNumbers = new List<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		internal List<string> mFacsimileNumbers = new List<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		internal string mPagerNumber = "$";// :OPTIONAL IfcLabel;
		internal List<string> mElectronicMailAddresses = new List<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		internal string mWWWHomePageURL = "$";// : OPTIONAL IfcLabel;
		internal List<string> mMessagingIDs = new List<string>();// : OPTIONAL LIST [1:?] OF IfcURIReference //IFC4

		internal IfcTelecomAddress() : base() { }
		public IfcTelecomAddress(DatabaseIfc db) : base(db) { }
		internal IfcTelecomAddress(DatabaseIfc db, IfcTelecomAddress a) : base(db, a) { mTelephoneNumbers = new List<string>(a.mTelephoneNumbers.ToArray()); mFacsimileNumbers = new List<string>(a.mFacsimileNumbers.ToArray()); mPagerNumber = a.mPagerNumber; mElectronicMailAddresses = new List<string>(a.mElectronicMailAddresses.ToArray()); mWWWHomePageURL = a.mWWWHomePageURL; mMessagingIDs.AddRange(a.mMessagingIDs); }
		internal static void parseFields(IfcTelecomAddress a, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcAddress.parseFields(a, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(str.Substring(1, str.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
					a.mTelephoneNumbers.Add(lst[icounter]);
			}
			str = arrFields[ipos++];
			if (str != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(str.Substring(1, str.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
					a.mFacsimileNumbers.Add(lst[icounter]);
			}
			a.mPagerNumber = arrFields[ipos++];
			str = arrFields[ipos++];
			if (str != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(str.Substring(1, str.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
					a.mElectronicMailAddresses.Add(lst[icounter]);
			}
			a.mWWWHomePageURL = arrFields[ipos++];
			if (schema != ReleaseVersion.IFC2x3)
			{
				str = arrFields[ipos++];
				if (!str.StartsWith("$"))
					a.mMessagingIDs = ParserSTEP.SplitListStrings(str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP();
			if (mTelephoneNumbers.Count == 0)
				str += ",$,";
			else
			{
				str += ",(" + mTelephoneNumbers[0];
				for (int icounter = 1; icounter < mTelephoneNumbers.Count; icounter++)
					str += "," + mTelephoneNumbers[icounter];
				str += "),";
			}
			if (mFacsimileNumbers.Count == 0)
				str += "$,";
			else
			{
				str += "(" + mFacsimileNumbers[0];
				for (int icounter = 1; icounter < mFacsimileNumbers.Count; icounter++)
					str += "," + mFacsimileNumbers[icounter];
				str += "),";
			}

			str += mPagerNumber;
			if (mElectronicMailAddresses.Count == 0)
				str += ",$,";
			else
			{
				str += ",(" + mElectronicMailAddresses[0];
				for (int icounter = 1; icounter < mElectronicMailAddresses.Count; icounter++)
					str += "," + mElectronicMailAddresses[icounter];
				str += "),";
			}
			str += mWWWHomePageURL;
			if (mDatabase.mRelease != ReleaseVersion.IFC2x3)
			{
				if (mMessagingIDs.Count == 0)
					str += ",$";
				else
				{
					str += ",('" + mMessagingIDs[0];
					for (int icounter = 1; icounter < mMessagingIDs.Count; icounter++)
						str += "','" + mMessagingIDs[icounter];
					str += "')";
				}
			}
			return str;
		}
		internal static IfcTelecomAddress Parse(string strDef,ReleaseVersion schema) { IfcTelecomAddress a = new IfcTelecomAddress(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
	}
	public partial class IfcTendon : IfcReinforcingElement
	{
		internal IfcTendonTypeEnum mPredefinedType;// : IfcTendonTypeEnum;//
		internal double mNominalDiameter;// : IfcPositiveLengthMeasure;
		internal double mCrossSectionArea;// : IfcAreaMeasure;
		internal double mTensionForce;// : OPTIONAL IfcForceMeasure;
		internal double mPreStress;// : OPTIONAL IfcPressureMeasure;
		internal double mFrictionCoefficient;// //: OPTIONAL IfcNormalisedRatioMeasure;
		internal double mAnchorageSlip;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mMinCurvatureRadius;// : OPTIONAL IfcPositiveLengthMeasure; 
		public IfcTendonTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcTendon() : base() { }
		internal IfcTendon(DatabaseIfc db, IfcTendon t) : base(db, t)
		{
			mPredefinedType = t.mPredefinedType;
			mNominalDiameter = t.mNominalDiameter;
			mCrossSectionArea = t.mCrossSectionArea;
			mTensionForce = t.mTensionForce;
			mPreStress = t.mPreStress;
			mFrictionCoefficient = t.mFrictionCoefficient;
			mAnchorageSlip = t.mAnchorageSlip;
			mMinCurvatureRadius = t.mMinCurvatureRadius;
		}
		public IfcTendon(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, double diam, double area, double forceMeasure, double pretress, double fricCoeff, double anchorSlip, double minCurveRadius)
			: base(host, placement,representation)
		{
			mNominalDiameter = diam;
			mCrossSectionArea = area;
			mTensionForce = forceMeasure;
			mPreStress = pretress;
			mFrictionCoefficient = fricCoeff;
			mAnchorageSlip = anchorSlip;
			mMinCurvatureRadius = minCurveRadius;
		}
		internal static IfcTendon Parse(string strDef) { IfcTendon t = new IfcTendon(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal static void parseFields(IfcTendon c, List<string> arrFields, ref int ipos)
		{
			IfcReinforcingElement.parseFields(c, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				c.mPredefinedType = (IfcTendonTypeEnum)Enum.Parse(typeof(IfcTendonTypeEnum), str.Replace(".", ""));
			c.mNominalDiameter = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mCrossSectionArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mTensionForce = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mPreStress = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mFrictionCoefficient = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mAnchorageSlip = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mMinCurvatureRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);

		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease != ReleaseVersion.IFC2x3 && mPredefinedType == IfcTendonTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") + ParserSTEP.DoubleToString(mNominalDiameter) + "," +
				ParserSTEP.DoubleToString(mCrossSectionArea) + "," + ParserSTEP.DoubleToString(mTensionForce) + "," +
				ParserSTEP.DoubleToString(mPreStress) + "," + ParserSTEP.DoubleToString(mFrictionCoefficient) + "," +
				ParserSTEP.DoubleToString(mAnchorageSlip) + "," + ParserSTEP.DoubleToString(mMinCurvatureRadius);
		}
	}
	public partial class IfcTendonAnchor : IfcReinforcingElement
	{
		internal IfcTendonAnchorTypeEnum mPredefinedType = IfcTendonAnchorTypeEnum.NOTDEFINED;// :	OPTIONAL IfcTendonAnchorTypeEnum;
		public IfcTendonAnchorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTendonAnchor() : base() { }
		internal IfcTendonAnchor(DatabaseIfc db, IfcTendonAnchor a) : base(db, a) { mPredefinedType = a.mPredefinedType; }
		public IfcTendonAnchor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static IfcTendonAnchor Parse(string strDef) { IfcTendonAnchor t = new IfcTendonAnchor(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal static void parseFields(IfcTendonAnchor a, List<string> arrFields, ref int ipos)
		{
			IfcReinforcingElement.parseFields(a, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				a.mPredefinedType = (IfcTendonAnchorTypeEnum)Enum.Parse(typeof(IfcTendonAnchorTypeEnum), str.Replace(".", ""));
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcTendonAnchorTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + "."));
		}
	}
	//IfcTendonAnchorType
	public partial class IfcTendonType : IfcReinforcingElementType  //IFC4
	{
		internal IfcTendonTypeEnum mPredefinedType = IfcTendonTypeEnum.NOTDEFINED;// : IfcTendonType; //IFC4
		private double mNominalDiameter;// : IfcPositiveLengthMeasure; 	IFC4 OPTIONAL
		internal double mCrossSectionArea;// : IfcAreaMeasure; IFC4 OPTIONAL
		internal double mSheathDiameter;// : OPTIONAL IfcPositiveLengthMeasure;

		public IfcTendonTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public double NominalDiameter { get { return mNominalDiameter; } set { mNominalDiameter = value; } }

		internal IfcTendonType() : base() { }
		internal IfcTendonType(DatabaseIfc db, IfcTendonType t) : base(db, t)
		{
			mPredefinedType = t.mPredefinedType;
			mNominalDiameter = t.mNominalDiameter;
			mCrossSectionArea = t.mCrossSectionArea;
			mSheathDiameter = t.mSheathDiameter;
		}

		public IfcTendonType(DatabaseIfc m, string name, IfcTendonTypeEnum type, double diameter, double area, double sheathDiameter)
			: base(m)
		{
			Name = name;
			mPredefinedType = type;
			mNominalDiameter = diameter;
			mCrossSectionArea = area;
			mSheathDiameter = sheathDiameter;
		}
		internal new static IfcTendonType Parse(string strDef) { int ipos = 0; IfcTendonType t = new IfcTendonType(); parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal static void parseFields(IfcTendonType t, List<string> arrFields, ref int ipos)
		{
			IfcReinforcingElementType.parseFields(t, arrFields, ref ipos);
			t.mPredefinedType = (IfcTendonTypeEnum)Enum.Parse(typeof(IfcTendonTypeEnum), arrFields[ipos++].Replace(".", ""));
			t.mNominalDiameter = ParserSTEP.ParseDouble(arrFields[ipos++]);
			t.mCrossSectionArea = ParserSTEP.ParseDouble(arrFields[ipos++]);
			t.mSheathDiameter = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP();
			result += ",." + mPredefinedType + ".," + ParserSTEP.DoubleOptionalToString(mNominalDiameter) + ",";
			result += ParserSTEP.DoubleOptionalToString(mCrossSectionArea) + "," + ParserSTEP.DoubleOptionalToString(mSheathDiameter);
			return result;
		}
	}
	public partial class IfcTerminatorSymbol : IfcAnnotationSymbolOccurrence // DEPRECEATED IFC4
	{
		internal int mAnnotatedCurve;// : IfcAnnotationCurveOccurrence; 
		internal IfcTerminatorSymbol() : base() { }
		//internal IfcTerminatorSymbol(IfcTerminatorSymbol i) : base(i) { mAnnotatedCurve = i.mAnnotatedCurve; }
		internal new static IfcTerminatorSymbol Parse(string strDef) { IfcTerminatorSymbol s = new IfcTerminatorSymbol(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcTerminatorSymbol s, List<string> arrFields, ref int ipos) { IfcAnnotationSymbolOccurrence.parseFields(s, arrFields, ref ipos); s.mAnnotatedCurve = ParserSTEP.ParseLink(arrFields[ipos++]); }
	}
	public abstract partial class IfcTessellatedFaceSet : IfcTessellatedItem, IfcBooleanOperand //ABSTRACT SUPERTYPE OF(IfcTriangulatedFaceSet, IfcPolygonalFaceSet )
	{
		internal int mCoordinates;// : 	IfcCartesianPointList;
		
		// INVERSE
		internal IfcIndexedColourMap mHasColours = null;// : SET [0:1] OF IfcIndexedColourMap FOR MappedTo;
		internal List<IfcIndexedTextureMap> mHasTextures = new List<IfcIndexedTextureMap>();// : SET [0:?] OF IfcIndexedTextureMap FOR MappedTo;

		public IfcCartesianPointList Coordinates { get { return mDatabase[mCoordinates] as IfcCartesianPointList; } set { mCoordinates = value.mIndex; } }
		public IfcIndexedColourMap HasColours { get { return mHasColours; } }
		public IEnumerable<IfcIndexedTextureMap> HasTextures { get { return mHasTextures; } }

		protected IfcTessellatedFaceSet() : base() { }
		protected IfcTessellatedFaceSet(DatabaseIfc db, IfcTessellatedFaceSet s) : base(db,s) { Coordinates = db.Factory.Duplicate( s.Coordinates) as IfcCartesianPointList; }
		protected IfcTessellatedFaceSet(IfcCartesianPointList3D pl) : base(pl.mDatabase) { mCoordinates = pl.mIndex; }
		protected override string BuildStringSTEP() { return  base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mCoordinates); }
		protected override void Parse(string str, ref int pos)
		{
			base.Parse(str, ref pos);
			mCoordinates = ParserSTEP.StripLink(str, ref pos);
		}
	}
	public abstract partial class IfcTessellatedItem : IfcGeometricRepresentationItem //IFC4
	{
		protected IfcTessellatedItem() : base() { }
		protected IfcTessellatedItem(DatabaseIfc db, IfcTessellatedItem i) : base(db,i) { }
		protected IfcTessellatedItem(DatabaseIfc db) : base(db) { }
		protected override void Parse(string str, ref int ipos) { base.Parse(str, ref ipos); }
	}
	public partial class IfcTextLiteral : IfcGeometricRepresentationItem
	{
		internal string mLiteral;// : IfcPresentableText;
		internal int mPlacement;// : IfcAxis2Placement;
		internal IfcTextPath mPath;// : IfcTextPath;
		 
		public IfcAxis2Placement Placement { get { return mDatabase[mPlacement] as IfcAxis2Placement; } }

		internal IfcTextLiteral() : base() { }
		internal IfcTextLiteral(DatabaseIfc db, IfcTextLiteral l) : base(db,l) { mLiteral = l.mLiteral; mPlacement = db.Factory.Duplicate(l.mDatabase[l.mPlacement]).mIndex; mPath = l.mPath; }
		internal static IfcTextLiteral Parse(string strDef) { IfcTextLiteral l = new IfcTextLiteral(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcTextLiteral l, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(l, arrFields, ref ipos); l.mLiteral = arrFields[ipos++]; l.mPlacement = ParserSTEP.ParseLink(arrFields[ipos++]); l.mPath = (IfcTextPath)Enum.Parse(typeof(IfcTextPath), arrFields[ipos++].Replace(".", "")); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mLiteral + "," + ParserSTEP.LinkToString(mPlacement) + ",." + mPath.ToString() + "."; }
	}
	public partial class IfcTextLiteralWithExtent : IfcTextLiteral
	{
		internal int mExtent;// : IfcPlanarExtent;
		internal string mBoxAlignment;// : IfcBoxAlignment; 
		internal IfcTextLiteralWithExtent() : base() { }
		//internal IfcTextLiteralWithExtent(IfcTextLiteralWithExtent o) : base(o) { mExtent = o.mExtent; mBoxAlignment = o.mBoxAlignment; }

		internal new static IfcTextLiteralWithExtent Parse(string strDef) { IfcTextLiteralWithExtent l = new IfcTextLiteralWithExtent(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcTextLiteralWithExtent l, List<string> arrFields, ref int ipos) { IfcTextLiteral.parseFields(l, arrFields, ref ipos); l.mExtent = ParserSTEP.ParseLink(arrFields[ipos++]); l.mBoxAlignment = arrFields[ipos++]; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mExtent) + "," + mBoxAlignment; }
	}
	public partial class IfcTextStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal int mTextCharacterAppearance;// : OPTIONAL IfcCharacterStyleSelect;
		internal int mTextStyle;// : OPTIONAL IfcTextStyleSelect;
		internal int mTextFontStyle;// : IfcTextFontSelect; 
		internal bool mModelOrDraughting = true;//	:	OPTIONAL BOOLEAN; IFC4CHANGE
		internal IfcTextStyle() : base() { }
	//	internal IfcTextStyle(IfcTextStyle v) : base(v) { mTextCharacterAppearance = v.mTextCharacterAppearance; mTextStyle = v.mTextStyle; mTextFontStyle = v.mTextFontStyle; mModelOrDraughting = v.mModelOrDraughting; }
		internal static void parseFields(IfcTextStyle s, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcPresentationStyle.parseFields(s, arrFields, ref ipos);
			s.mTextCharacterAppearance = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mTextStyle = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mTextFontStyle = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (schema != ReleaseVersion.IFC2x3)
				s.mModelOrDraughting = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		internal static IfcTextStyle Parse(string strDef,ReleaseVersion schema) { IfcTextStyle s = new IfcTextStyle(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mTextCharacterAppearance) + "," + ParserSTEP.LinkToString(mTextStyle) + "," + ParserSTEP.LinkToString(mTextFontStyle) + (mDatabase.mRelease != ReleaseVersion.IFC2x3 ? "," + ParserSTEP.BoolToString(mModelOrDraughting) : ""); }
	}
	public partial class IfcTextStyleFontModel : IfcPreDefinedTextFont
	{
		internal List<string> mFontFamily = new List<string>(1);// : OPTIONAL LIST [1:?] OF IfcTextFontName;
		internal string mFontStyle = "$";// : OPTIONAL IfcFontStyle; ['normal','italic','oblique'];
		internal string mFontVariant = "$";// : OPTIONAL IfcFontVariant; ['normal','small-caps'];
		internal string mFontWeight = "$";// : OPTIONAL IfcFontWeight; // ['normal','small-caps','100','200','300','400','500','600','700','800','900'];
		internal string mFontSize;// : IfcSizeSelect; IfcSizeSelect = SELECT (IfcRatioMeasure ,IfcLengthMeasure ,IfcDescriptiveMeasure ,IfcPositiveLengthMeasure ,IfcNormalisedRatioMeasure ,IfcPositiveRatioMeasure);
		internal IfcTextStyleFontModel() : base() { }
		internal IfcTextStyleFontModel(DatabaseIfc db, IfcTextStyleFontModel m) : base(db,m)
		{
	//		mFontFamily = new List<string>(i.mFontFamily.ToArray());
			mFontStyle = m.mFontStyle;
			mFontVariant = m.mFontVariant;
			mFontWeight = m.mFontWeight;
			mFontSize = m.mFontSize;
		}
		internal static IfcTextStyleFontModel Parse(string strDef) { IfcTextStyleFontModel f = new IfcTextStyleFontModel(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
		internal static void parseFields(IfcTextStyleFontModel f, List<string> arrFields, ref int ipos)
		{
			IfcPreDefinedTextFont.parseFields(f, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s != "$")
			{
				List<string> lst = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				for (int icounter = 0; icounter < lst.Count; icounter++)
					f.mFontFamily.Add(lst[icounter]);
			}
			f.mFontStyle = arrFields[ipos++];
			f.mFontVariant = arrFields[ipos++];
			f.mFontWeight = arrFields[ipos++];
			f.mFontSize = arrFields[ipos++];
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP();
			if (mFontFamily.Count > 0)
			{
				str += ",(" + mFontFamily[0];
				for (int icounter = 1; icounter < mFontFamily.Count; icounter++)
					str += "," + mFontFamily[icounter];
				str += "),";
			}
			else
				str += ",$,";
			return str + mFontStyle + "," + mFontVariant + "," + mFontWeight + "," + mFontSize;
		}
	}
	public partial class IfcTextStyleForDefinedFont : BaseClassIfc
	{
		internal int mColour;// : IfcColour;
		internal int mBackgroundColour;// : OPTIONAL IfcColour;
		internal IfcTextStyleForDefinedFont() : base() { }
	//	internal IfcTextStyleForDefinedFont(IfcTextStyleForDefinedFont o) : base() { mColour = o.mColour; mBackgroundColour = o.mBackgroundColour; }
		internal static void parseFields(IfcTextStyleForDefinedFont f, List<string> arrFields, ref int ipos) { f.mColour = ParserSTEP.ParseLink(arrFields[ipos++]); f.mBackgroundColour = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mColour) + "," + ParserSTEP.LinkToString(mBackgroundColour); }
		internal static IfcTextStyleForDefinedFont Parse(string strDef) { IfcTextStyleForDefinedFont f = new IfcTextStyleForDefinedFont(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
	}
	public partial class IfcTextStyleTextModel : IfcPresentationItem
	{
		//internal int mDiffuseTransmissionColour, mDiffuseReflectionColour, mTransmissionColour, mReflectanceColour;//	 :	IfcColourRgb;
		internal IfcTextStyleTextModel() : base() { }
		internal IfcTextStyleTextModel(DatabaseIfc db, IfcTextStyleTextModel m) : base(db,m) { }
	 
		protected override void parseFields(List<string> arrFields, ref int ipos)
		{
			base.parseFields(arrFields, ref ipos);
			//s.mDiffuseTransmissionColour = IFCModel.mSTP.parseSTPLink(arrFields[ipos++]);
			//s.mDiffuseReflectionColour = IFCModel.mSTP.parseSTPLink(arrFields[ipos++]);
			//s.mTransmissionColour = IFCModel.mSTP.parseSTPLink(arrFields[ipos++]);
			//s.mReflectanceColour = IFCModel.mSTP.parseSTPLink(arrFields[ipos++]);
		}
		internal static IfcTextStyleTextModel Parse(string strDef) { IfcTextStyleTextModel s = new IfcTextStyleTextModel(); int ipos = 0; s.parseFields(ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		//protected override string BuildString() { return (mModel.mOutputEssential ? "" : base.BuildString() + "," + IFCModel.mSTP.STPLinkToString(mDiffuseTransmissionColour) + "," + IFCModel.mSTP.STPLinkToString(mDiffuseReflectionColour) + "," + IFCModel.mSTP.STPLinkToString(mTransmissionColour) + "," + IFCModel.mSTP.STPLinkToString(mReflectanceColour)); }
	}
	//ENTITY IfcTextStyleWithBoxCharacteristics; // DEPRECEATED IFC4
	public abstract partial class IfcTextureCoordinate : IfcPresentationItem  //ABSTRACT SUPERTYPE OF(ONEOF(IfcIndexedTextureMap, IfcTextureCoordinateGenerator, IfcTextureMap))
	{
		internal List<int> mMaps = new List<int>();// : LIST [1:?] OF IfcSurfaceTexture
		public List<IfcSurfaceTexture> Maps { get { return mMaps.ConvertAll(x => mDatabase[x] as IfcSurfaceTexture); } set { mMaps = value.ConvertAll(x => x.mIndex); } }

		internal IfcTextureCoordinate() : base() { }
		internal IfcTextureCoordinate(DatabaseIfc db, IfcTextureCoordinate c) : base(db, c) { Maps = c.Maps.ConvertAll(x=>db.Factory.Duplicate(x) as IfcSurfaceTexture); }
		public IfcTextureCoordinate(DatabaseIfc m, List<IfcSurfaceTexture> maps) : base(m) { mMaps = maps.ConvertAll(x => x.mIndex); }

		protected override void parseFields(List<string> arrFields, ref int pos)
		{
			mMaps = ParserSTEP.SplitListLinks(arrFields[pos++]);
		}
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + ",(#" + mMaps[0];
			for (int icounter = 1; icounter < mMaps.Count; icounter++)
				result += ",#" + mMaps[icounter];
			return result + ")";
		}
	}
	//ENTITY IfcTextureCoordinateGenerator
	//ENTITY IfcTextureMap
	//ENTITY IfcTextureVertex;
	public partial class IfcTextureVertexList : IfcPresentationItem
	{
		internal Tuple<double, double>[] mTexCoordsList = new Tuple<double, double>[0];// : LIST [1:?] OF IfcSurfaceTexture

		internal IfcTextureVertexList() : base() { }
		internal IfcTextureVertexList(DatabaseIfc db, IfcTextureVertexList l) : base(db,l) { mTexCoordsList = l.mTexCoordsList; }
		public IfcTextureVertexList(DatabaseIfc m, IEnumerable<Tuple<double, double>> coords) : base(m) { mTexCoordsList = coords.ToArray(); }

		internal static IfcTextureVertexList Parse(string strDef) { IfcTextureVertexList l = new IfcTextureVertexList(); int pos = 0; l.parseFields(ParserSTEP.SplitLineFields(strDef), ref pos); return l; }
		protected override void parseFields(List<string> arrFields, ref int ipos) { base.parseFields(arrFields, ref ipos); mTexCoordsList = ParserSTEP.SplitListDoubleTuple(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			Tuple<double, double> pair = mTexCoordsList[0];
			string result = base.BuildStringSTEP() + ",((" + ParserSTEP.DoubleToString(pair.Item1) + "," + ParserSTEP.DoubleToString(pair.Item2);
			for (int icounter = 1; icounter < mTexCoordsList.Length; icounter++)
			{
				pair = mTexCoordsList[icounter];
				result += "),(" + ParserSTEP.DoubleToString(pair.Item1) + "," + ParserSTEP.DoubleToString(pair.Item2);
			}

			return result + "))";
		}
	}
	public partial class IfcThermalMaterialProperties : IfcMaterialPropertiesSuperSeded // DEPRECEATED IFC4
	{
		internal double mSpecificHeatCapacity = double.NaN;// : OPTIONAL IfcSpecificHeatCapacityMeasure;
		internal double mBoilingPoint = double.NaN;// : OPTIONAL IfcThermodynamicTemperatureMeasure;
		internal double mFreezingPoint = double.NaN;// : OPTIONAL IfcThermodynamicTemperatureMeasure;
		internal double mThermalConductivity = double.NaN;// : OPTIONAL IfcThermalConductivityMeasure; 
		internal IfcThermalMaterialProperties() : base() { }
		internal IfcThermalMaterialProperties(DatabaseIfc db, IfcThermalMaterialProperties p) : base(db,p) { mSpecificHeatCapacity = p.mSpecificHeatCapacity; mBoilingPoint = p.mBoilingPoint; mFreezingPoint = p.mFreezingPoint; mThermalConductivity = p.mThermalConductivity; }
		internal static IfcThermalMaterialProperties Parse(string strDef) { IfcThermalMaterialProperties p = new IfcThermalMaterialProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcThermalMaterialProperties p, List<string> arrFields, ref int ipos)
		{
			IfcMaterialPropertiesSuperSeded.parseFields(p, arrFields, ref ipos);
			p.mSpecificHeatCapacity = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mBoilingPoint = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFreezingPoint = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mThermalConductivity = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleOptionalToString(mSpecificHeatCapacity) + "," + ParserSTEP.DoubleOptionalToString(mBoilingPoint) + "," + ParserSTEP.DoubleOptionalToString(mFreezingPoint) + "," + ParserSTEP.DoubleOptionalToString(mThermalConductivity); }
	}
	public struct IfcTime
	{
		internal static string convert(DateTime date) { return (date.Hour < 10 ? "T0" : "T") + date.Hour + (date.Minute < 10 ? "-0" : "-") + date.Minute + (date.Second < 10 ? "-0" : "-") + date.Second; }
	}
	public interface IfcTimeOrRatioSelect { string String { get; } } // IFC4 	IfcRatioMeasure, IfcDuration	
	public partial class IfcTimePeriod : BaseClassIfc // IFC4
	{
		internal string mStart; //:	IfcTime;
		internal string mFinish; //:	IfcTime;
		internal IfcTimePeriod() : base() { }
		internal IfcTimePeriod(IfcTimePeriod m) : base() { mStart = m.mStart; mFinish = m.mFinish; }
		internal IfcTimePeriod(DatabaseIfc m, DateTime start, DateTime finish) : base(m) { mStart = IfcTime.convert(start); mFinish = IfcTime.convert(finish);}
		internal static IfcTimePeriod Parse(string strDef) { IfcTimePeriod m = new IfcTimePeriod(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcTimePeriod m, List<string> arrFields, ref int ipos) { m.mStart = arrFields[ipos++]; m.mFinish = arrFields[ipos++]; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mStart + "','" + mFinish + "'"; }
	}
	public abstract partial class IfcTimeSeries : BaseClassIfc, IfcMetricValueSelect, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcIrregularTimeSeries,IfcRegularTimeSeries));
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;		
		internal string mDescription;// : OPTIONAL IfcText;
		internal int mStartTime;// : IfcDateTimeSelect;
		internal int mEndTime;// : IfcDateTimeSelect;
		internal IfcTimeSeriesDataTypeEnum mTimeSeriesDataType = IfcTimeSeriesDataTypeEnum.NOTDEFINED;// : IfcTimeSeriesDataTypeEnum;
		internal IfcDataOriginEnum mDataOrigin = IfcDataOriginEnum.NOTDEFINED;// : IfcDataOriginEnum;
		internal string mUserDefinedDataOrigin = "$";// : OPTIONAL IfcLabel;
		internal int mUnit;// : OPTIONAL IfcUnit; 
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		protected IfcTimeSeries() : base() { }
		//protected IfcTimeSeries(DatabaseIfc db, IfcTimeSeries i)
		//	: base(db,i)
		//{
		//	mName = i.mName;
		//	mDescription = i.mDescription;
		//	mStartTime = i.mStartTime;
		//	mEndTime = i.mEndTime;
		//	mTimeSeriesDataType = i.mTimeSeriesDataType;
		//	mDataOrigin = i.mDataOrigin;
		//	mUserDefinedDataOrigin = i.mUserDefinedDataOrigin;
		//	mUnit = i.mUnit;
		//}
		protected IfcTimeSeries(DatabaseIfc db) : base(db) { }
		internal static void parseFields(IfcTimeSeries s, List<string> arrFields, ref int ipos)
		{
			s.mName = arrFields[ipos++].Replace("'", "");
			s.mDescription = arrFields[ipos++].Replace("'", "");
			s.mStartTime = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mEndTime = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mTimeSeriesDataType = (IfcTimeSeriesDataTypeEnum)Enum.Parse(typeof(IfcTimeSeriesDataTypeEnum), arrFields[ipos++].Replace(".", ""));
			string str = arrFields[ipos++];
			if (str.StartsWith("."))
				s.mDataOrigin = (IfcDataOriginEnum)Enum.Parse(typeof(IfcDataOriginEnum), str.Replace(".", ""));
			s.mUserDefinedDataOrigin = arrFields[ipos++];
			s.mUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",'" + mName + "','" + mDescription + "'," + ParserSTEP.LinkToString(mStartTime) + "," + ParserSTEP.LinkToString(mEndTime) + ",." + mTimeSeriesDataType.ToString() + ".,." + mDataOrigin.ToString() + ".," + mUserDefinedDataOrigin + "," + ParserSTEP.LinkToString(mUnit); }
	}
	//ENTITY IfcTimeSeriesReferenceRelationship; // DEPRECEATED IFC4
	//ENTITY IfcTimeSeriesSchedule // DEPRECEATED IFC4
	//ENTITY IfcTimeSeriesValue;  
	public abstract partial class IfcTopologicalRepresentationItem : IfcRepresentationItem  /*(IfcConnectedFaceSet,IfcEdge,IfcFace,IfcFaceBound,IfcLoop,IfcPath,IfcVertex))*/
	{
		protected IfcTopologicalRepresentationItem() : base() { }
		protected IfcTopologicalRepresentationItem(DatabaseIfc db) : base(db) { }
		protected IfcTopologicalRepresentationItem(DatabaseIfc db, IfcTopologicalRepresentationItem i) : base(db,i) { }
		protected static void parseFields(IfcTopologicalRepresentationItem i, List<string> arrFields, ref int ipos) { IfcRepresentationItem.parseFields(i, arrFields, ref ipos); }
	}
	public partial class IfcTopologyRepresentation : IfcShapeModel
	{
		internal IfcTopologyRepresentation() : base() { }
		internal IfcTopologyRepresentation(DatabaseIfc db, IfcTopologyRepresentation r) : base(db, r) { }
		internal IfcTopologyRepresentation(IfcConnectedFaceSet fs, string identifier) : base(fs, identifier, "FaceSet") { }
		internal IfcTopologyRepresentation(IfcEdge e, string identifier) : base(e, identifier, "Edge") { }
		internal IfcTopologyRepresentation(IfcFace fs, string identifier) : base(fs, identifier, "Face") { }
		internal IfcTopologyRepresentation(IfcVertex v, string identifier) : base(v, identifier, "Vertex") { }
		internal new static IfcTopologyRepresentation Parse(string strDef)
		{
			IfcTopologyRepresentation r = new IfcTopologyRepresentation();
			int pos = 0;
			IfcShapeModel.parseString(r, strDef, ref pos);
			return r;
		}
		internal static IfcTopologyRepresentation getRepresentation(IfcTopologicalRepresentationItem ri)
		{
			IfcConnectedFaceSet cfs = ri as IfcConnectedFaceSet;
			if (cfs != null)
				return new IfcTopologyRepresentation(cfs, "");
			IfcEdge e = ri as IfcEdge;
			if (e != null)
				return new IfcTopologyRepresentation(e, "");
			IfcFace f = ri as IfcFace;
			if (f != null)
				return new IfcTopologyRepresentation(f, "");
			IfcVertex v = ri as IfcVertex;
			if (v != null)
				return new IfcTopologyRepresentation(v, "");
			return null;
		}
		
	}
	public partial class IfcTransformer : IfcEnergyConversionDevice //IFC4
	{
		internal IfcTransformerTypeEnum mPredefinedType = IfcTransformerTypeEnum.NOTDEFINED;// OPTIONAL : IfcTransformerTypeEnum;
		public IfcTransformerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTransformer() : base() { }
		internal IfcTransformer(DatabaseIfc db, IfcTransformer t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcTransformer(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcTransformer s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcTransformerTypeEnum)Enum.Parse(typeof(IfcTransformerTypeEnum), str);
		}
		internal new static IfcTransformer Parse(string strDef) { IfcTransformer s = new IfcTransformer(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcTransformerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcTransformerType : IfcEnergyConversionDeviceType
	{
		internal IfcTransformerTypeEnum mPredefinedType = IfcTransformerTypeEnum.NOTDEFINED;// : IfcTransformerEnum; 
		public IfcTransformerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTransformerType() : base() { }
		internal IfcTransformerType(DatabaseIfc db, IfcTransformerType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcTransformerType(DatabaseIfc m, string name, IfcTransformerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcTransformerType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcTransformerTypeEnum)Enum.Parse(typeof(IfcTransformerTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcTransformerType Parse(string strDef) { IfcTransformerType t = new IfcTransformerType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcTranslationalStiffnessSelect
	{
		internal bool mRigid = false;
		internal IfcLinearStiffnessMeasure mStiffness = null;

		public bool Rigid { get { return mRigid; } }
		public IfcLinearStiffnessMeasure Stiffness { get { return mStiffness; } }

		internal IfcTranslationalStiffnessSelect(bool fix) { mRigid = fix; }
		internal IfcTranslationalStiffnessSelect(double stiff) { mStiffness = new IfcLinearStiffnessMeasure(stiff); }
		internal IfcTranslationalStiffnessSelect(IfcLinearStiffnessMeasure stiff) { mStiffness = stiff; }
		internal static IfcTranslationalStiffnessSelect Parse(string str,ReleaseVersion schema)
		{
			if (str == "$")
				return null;
			if (str.StartsWith("IFCBOOL"))
				return new IfcTranslationalStiffnessSelect(((IfcBoolean)ParserIfc.parseSimpleValue(str)).mValue);
			if (str.StartsWith("IFCLIN"))
				return new IfcTranslationalStiffnessSelect((IfcLinearStiffnessMeasure)ParserIfc.parseDerivedMeasureValue(str));
			if (str.StartsWith("."))
				return new IfcTranslationalStiffnessSelect(ParserSTEP.ParseBool(str));
			double d = ParserSTEP.ParseDouble(str), tol = 1e-9;
			if (schema == ReleaseVersion.IFC2x3)
			{
				if (Math.Abs(d + 1) < tol)
					return new IfcTranslationalStiffnessSelect(true) { mStiffness = new IfcLinearStiffnessMeasure(-1) };
				if (Math.Abs(d) < tol)
					return new IfcTranslationalStiffnessSelect(false) { mStiffness = new IfcLinearStiffnessMeasure(0) };
			}
			return new IfcTranslationalStiffnessSelect(new IfcLinearStiffnessMeasure(d));
		}
		public override string ToString() { return (mStiffness == null ? "IFCBOOLEAN(" + ParserSTEP.BoolToString(mRigid) + ")" : mStiffness.ToString()); }
	}
	public partial class IfcTransportElement : IfcElement
	{

		internal IfcTransportElementTypeEnum mPredefinedType = IfcTransportElementTypeEnum.NOTDEFINED;// : 	OPTIONAL IfcTransportElementTypeEnum;
		internal double mCapacityByWeight = double.NaN;// : 	OPTIONAL IfcMassMeasure;
		internal double mCapacityByNumber = double.NaN;//	 : 	OPTIONAL IfcCountMeasure;

		public IfcTransportElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		//public double CapacityByWeight { get { return mCapacityByWeight; } set { mCapacityByWeight = value; } }
		//public double CapacityByNumber { get { return CapacityByNumber; } set { mCapacityByNumber = value; } }

		internal IfcTransportElement() : base() { }
		internal IfcTransportElement(DatabaseIfc db, IfcTransportElement e) : base(db, e) { }
		public IfcTransportElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static void parseFields(IfcTransportElement e, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcElement.parseFields(e, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str != "$")
				Enum.TryParse<IfcTransportElementTypeEnum>(str.Substring(1, str.Length - 2), out e.mPredefinedType);
			if(schema == ReleaseVersion.IFC2x3)
			{
				e.mCapacityByWeight = ParserSTEP.ParseDouble(arrFields[ipos++]);
				e.mCapacityByNumber = ParserSTEP.ParseDouble(arrFields[ipos++]);
			}
		}
		internal static IfcTransportElement Parse(string strDef, ReleaseVersion schema) { IfcTransportElement e = new IfcTransportElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return e; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mPredefinedType == IfcTransportElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".") + (mDatabase.Release == ReleaseVersion.IFC2x3 ? "," + ParserSTEP.DoubleOptionalToString(mCapacityByWeight) + "," + ParserSTEP.DoubleOptionalToString(mCapacityByNumber) : ""); }
	}
	public partial class IfcTransportElementType : IfcElementType
	{
		internal IfcTransportElementTypeEnum mPredefinedType;// IfcTransportElementTypeEnum; 
		public IfcTransportElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTransportElementType() : base() { }
		internal IfcTransportElementType(DatabaseIfc db, IfcTransportElementType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		public IfcTransportElementType(DatabaseIfc m, string name, IfcTransportElementTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal new static IfcTransportElementType Parse(string strDef) { IfcTransportElementType t = new IfcTransportElementType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal static void parseFields(IfcTransportElementType t, List<string> arrFields, ref int ipos) { IfcElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcTransportElementTypeEnum)Enum.Parse(typeof(IfcTransportElementTypeEnum), arrFields[ipos++].Replace(".", "")); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcTrapeziumProfileDef : IfcParameterizedProfileDef
	{
		internal double mBottomXDim;// : IfcPositiveLengthMeasure;
		internal double mTopXDim;// : IfcPositiveLengthMeasure;
		internal double mYDim;// : IfcPositiveLengthMeasure;
		internal double mTopXOffset;// : IfcPositiveLengthMeasure; 
		internal IfcTrapeziumProfileDef() : base() { }
		internal IfcTrapeziumProfileDef(DatabaseIfc db, IfcTrapeziumProfileDef p) : base(db, p) { mBottomXDim = p.mBottomXDim; mTopXDim = p.mTopXDim; mYDim = p.mYDim; mTopXOffset = p.mTopXOffset; }
		internal IfcTrapeziumProfileDef(DatabaseIfc db, string name,double bottomXDim, double topXDim,double yDim,double topXOffset) : base(db,name)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.If2x3NotAssigned)
				throw new Exception("Invalid Model View for IfcTrapeziumProfileDef : " + db.ModelView.ToString());
			mBottomXDim = bottomXDim;
			mTopXDim = topXDim;
			mYDim = yDim;
			mTopXOffset = topXOffset;
		}
		internal static void parseFields(IfcTrapeziumProfileDef p, List<string> arrFields, ref int ipos)
		{
			IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos);
			p.mBottomXDim = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mTopXDim = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mYDim = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mTopXOffset = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		internal new static IfcTrapeziumProfileDef Parse(string strDef) { IfcTrapeziumProfileDef p = new IfcTrapeziumProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mBottomXDim) + "," + ParserSTEP.DoubleToString(mTopXDim) + "," + ParserSTEP.DoubleToString(mYDim) + "," + ParserSTEP.DoubleToString(mTopXOffset); }
	}
	public partial class IfcTriangulatedFaceSet : IfcTessellatedFaceSet
	{
		internal Tuple<double, double, double>[] mNormals = new Tuple<double, double, double>[0];// : OPTIONAL LIST [1:?] OF LIST [3:3] OF IfcParameterValue; 
		internal IfcLogicalEnum mClosed = IfcLogicalEnum.UNKNOWN; // 	OPTIONAL BOOLEAN;
		internal Tuple<int, int, int>[] mCoordIndex = new Tuple<int, int, int>[0];// : 	LIST [1:?] OF LIST [3:3] OF INTEGER;
		internal Tuple<int, int, int>[] mNormalIndex = new Tuple<int, int, int>[0];// :	OPTIONAL LIST [1:?] OF LIST [3:3] OF INTEGER;  
		internal List<int> mPnIndex = new List<int>(); // : OPTIONAL LIST [1:?] OF IfcPositiveInteger;

		public IEnumerable< Tuple<double, double,double>> Normals { get { return mNormals; } set { mNormals = (value == null ? null : value.ToArray()); } }
		public bool Closed { get { return mClosed == IfcLogicalEnum.TRUE; } set { mClosed = value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE; } }
		public IEnumerable<Tuple<int, int, int>> CoordIndex { get { return mCoordIndex; } set { mCoordIndex = value.ToArray(); } }
		public IEnumerable<Tuple<int, int, int>> NormalIndex { get { return mNormalIndex; } set { mNormalIndex = (value == null ? null : value.ToArray()); } }
		public List<int> PnIndex { get { return mPnIndex; } set { mPnIndex = (value == null ? new List<int>() : value); } }

		internal IfcTriangulatedFaceSet() : base() { }
		internal IfcTriangulatedFaceSet(DatabaseIfc db, IfcTriangulatedFaceSet s) : base(db,s)
		{
			if (s.mNormals.Length > 0)
				mNormals = s.mNormals.ToArray();
			mClosed = s.mClosed;
			mCoordIndex = s.mCoordIndex.ToArray();
			if(s.mNormalIndex.Length > 0)
			mNormalIndex = s.mNormalIndex.ToArray();
		}
		public IfcTriangulatedFaceSet(IfcCartesianPointList3D pl, bool closed, IEnumerable<Tuple<int, int, int>> coords)
			: base(pl) { CoordIndex = coords; Closed = closed; }
		internal static IfcTriangulatedFaceSet Parse(string str)
		{
			IfcTriangulatedFaceSet t = new IfcTriangulatedFaceSet();
			int pos = 0;
			t.Parse(str, ref pos);
			return t;
		}
		protected override void Parse(string str, ref int pos)
		{
			base.Parse(str, ref pos);
			string field = ParserSTEP.StripField(str, ref pos);
			if (field.StartsWith("("))
				mNormals = ParserSTEP.SplitListDoubleTriple(field);
			mClosed = ParserIfc.StripLogical(str, ref pos);
			field = ParserSTEP.StripField(str, ref pos);
			mCoordIndex = ParserSTEP.SplitListSTPIntTriple(field);
			field = ParserSTEP.StripField(str, ref pos);
			if (field.StartsWith("("))
				mNormalIndex = ParserSTEP.SplitListSTPIntTriple(field);
			mPnIndex = ParserSTEP.StripListInt(str, ref pos);
		}
		protected override string BuildStringSTEP()
		{
			StringBuilder sb = new StringBuilder();
			if (mNormals.Length == 0)
				sb.Append( ",$,");
			else
			{
				Tuple<double, double, double> normal = mNormals[0];
				sb.Append( ",((" + ParserSTEP.DoubleToString(normal.Item1) + "," + ParserSTEP.DoubleToString(normal.Item2) + "," + ParserSTEP.DoubleToString(normal.Item3) + ")");
				for (int icounter = 1; icounter < mNormals.Length; icounter++)
				{
					normal = mNormals[icounter];
					sb.Append( ",(" + ParserSTEP.DoubleToString(normal.Item1) + "," + ParserSTEP.DoubleToString(normal.Item2) + "," + ParserSTEP.DoubleToString(normal.Item3) + ")");
				}
				sb.Append("),");
			}
			sb.Append( mClosed == IfcLogicalEnum.UNKNOWN ? "$" : ParserSTEP.BoolToString(Closed));
			Tuple<int, int, int> p = mCoordIndex[0];
			sb.Append(",((" + p.Item1 + "," + p.Item2 + "," + p.Item3);
			for (int icounter = 1; icounter < mCoordIndex.Length; icounter++)
			{
				p = mCoordIndex[icounter];
				sb.Append("),(" + p.Item1 + "," + p.Item2 + "," + p.Item3);
			}
			if (mNormalIndex.Length == 0)
				sb.Append(")),$");
			else
			{
				p = mNormalIndex[0];
				sb.Append(")),((" + p.Item1 + "," + p.Item2 + "," + p.Item3);
				for (int icounter = 1; icounter < mNormalIndex.Length; icounter++)
				{
					p = mNormalIndex[icounter];
					sb.Append("),(" + p.Item1 + "," + p.Item2 + "," + p.Item3);
				}
				sb.Append("))");
			}
			if (mPnIndex.Count == 0)
				sb.Append(",$");
			else
			{
				sb.Append(",(" + mPnIndex[0]);
				for (int icounter = 1; icounter < mPnIndex.Count; icounter++)
					sb.Append("," + mPnIndex[icounter]);
				sb.Append(")");
			}
			return base.BuildStringSTEP() + sb.ToString();
		}
	}
	public partial class IfcTrimmedCurve : IfcBoundedCurve
	{
		private int mBasisCurve;//: IfcCurve;
		internal IfcTrimmingSelect mTrim1;// : SET [1:2] OF IfcTrimmingSelect;
		internal IfcTrimmingSelect mTrim2;//: SET [1:2] OF IfcTrimmingSelect;
		private bool mSenseAgreement;// : BOOLEAN;
		internal IfcTrimmingPreference mMasterRepresentation = IfcTrimmingPreference.UNSPECIFIED;// : IfcTrimmingPreference; 

		public IfcCurve BasisCurve { get { return mDatabase[mBasisCurve] as IfcCurve; } set { mBasisCurve = value.mIndex; } }
		public bool SenseAgreement { get { return mSenseAgreement; } }
		public IfcTrimmingPreference MasterRepresentation { get { return mMasterRepresentation; } set { mMasterRepresentation = value; } } 

		internal IfcTrimmedCurve() : base() { }
		internal IfcTrimmedCurve(DatabaseIfc db, IfcTrimmedCurve c) : base(db,c)
		{
			BasisCurve = db.Factory.Duplicate(c.BasisCurve) as IfcCurve;
			mTrim1 = c.mTrim1;
			mTrim2 = c.mTrim2;
			if (c.mTrim1.mIfcCartesianPoint > 0)
				mTrim1.mIfcCartesianPoint = db.Factory.Duplicate(c.mDatabase[c.mTrim1.mIfcCartesianPoint]).mIndex;
			if (c.mTrim2.mIfcCartesianPoint > 0)
				mTrim2.mIfcCartesianPoint = db.Factory.Duplicate(c.mDatabase[c.mTrim2.mIfcCartesianPoint]).mIndex;
			mSenseAgreement = c.mSenseAgreement;
			mMasterRepresentation = c.mMasterRepresentation;
		}
		internal IfcTrimmedCurve(IfcCurve basis, IfcTrimmingSelect start, IfcTrimmingSelect end, bool senseAgreement, IfcTrimmingPreference tp) : base(basis.mDatabase)
		{
			mBasisCurve = basis.mIndex;
			mTrim1 = start;
			mTrim2 = end;
			mSenseAgreement = senseAgreement;
			mMasterRepresentation = tp;
		}
		internal IfcTrimmedCurve(IfcCartesianPoint start, Tuple<double, double> arcInteriorPoint, IfcCartesianPoint end) : base(start.mDatabase)
		{
			Tuple<double, double, double> pt1 = start.Coordinates, pt3 = end.Coordinates;

			double xDelta_a = arcInteriorPoint.Item1 - pt1.Item1;
			double yDelta_a = arcInteriorPoint.Item2 - pt1.Item2;
			double xDelta_b = pt3.Item1 - arcInteriorPoint.Item1;
			double yDelta_b = pt3.Item2 - arcInteriorPoint.Item2;
			double x = 0, y = 0;
			double tol = 1e-9;
			if (Math.Abs(xDelta_a) < tol && Math.Abs(yDelta_b) < tol)
			{
				x = (arcInteriorPoint.Item1 + pt3.Item1) / 2;
				y = (pt1.Item2 + arcInteriorPoint.Item2) / 2;
			}
			else
			{
				double aSlope = yDelta_a / xDelta_a; // 
				double bSlope = yDelta_b / xDelta_b;
				if (Math.Abs(aSlope - bSlope) < tol)
				{   // points are colinear
					// line curve
					throw new Exception("Not implemented");
				}

				// calc center
				x = (aSlope * bSlope * (pt1.Item2 - pt3.Item2) + bSlope * (pt1.Item1 + arcInteriorPoint.Item1)
					- aSlope * (arcInteriorPoint.Item1 + pt3.Item1)) / (2 * (bSlope - aSlope));
				y = -1 * (x - (pt1.Item1 + arcInteriorPoint.Item1) / 2) / aSlope + (pt1.Item2 + arcInteriorPoint.Item2) / 2;
			}

			double radius = Math.Sqrt(Math.Pow(pt1.Item1 - x,2)+ Math.Pow(pt1.Item2 - y,2));
			BasisCurve = new IfcCircle(new IfcAxis2Placement2D(new IfcCartesianPoint(start.mDatabase, x, y)) { }, radius);
			mTrim1 = new IfcTrimmingSelect(start);
			mTrim2 = new IfcTrimmingSelect(end);
		}	
		internal static void parseFields(IfcTrimmedCurve c, List<string> arrFields, ref int ipos) { IfcBoundedCurve.parseFields(c, arrFields, ref ipos); c.mBasisCurve = ParserSTEP.ParseLink(arrFields[ipos++]); c.mTrim1 = IfcTrimmingSelect.Parse(arrFields[ipos++]); c.mTrim2 = IfcTrimmingSelect.Parse(arrFields[ipos++]); c.mSenseAgreement = ParserSTEP.ParseBool(arrFields[ipos++]); c.mMasterRepresentation = (IfcTrimmingPreference)Enum.Parse(typeof(IfcTrimmingPreference), arrFields[ipos++].Replace(".", "")); }
		internal static IfcTrimmedCurve Parse(string strDef) { IfcTrimmedCurve c = new IfcTrimmedCurve(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mBasisCurve) + "," + mTrim1.ToString() + "," + mTrim2.ToString() + "," + ParserSTEP.BoolToString(mSenseAgreement) + ",." + mMasterRepresentation.ToString() + "."; }
	}
	public partial struct IfcTrimmingSelect
	{
		internal IfcTrimmingSelect(double param, IfcCartesianPoint cp)
		{
			mIfcParameterValue = param;
			mIfcCartesianPoint = 0;
			if (cp != null)
				mIfcCartesianPoint = cp.mIndex;
		}
		internal IfcTrimmingSelect(IfcCartesianPoint cp)
		{
			mIfcParameterValue = double.NaN;
			mIfcCartesianPoint = 0;
			if (cp != null)
				mIfcCartesianPoint = cp.mIndex;
		}
		internal double mIfcParameterValue;
		internal int mIfcCartesianPoint;
		internal static IfcTrimmingSelect Parse(string str)
		{
			IfcTrimmingSelect ts = new IfcTrimmingSelect();
			ts.mIfcParameterValue = double.NaN;
			int i = 0;
			if (str[i] == '(')
				i++;
			char c = str[i];
			if (c == '#')
			{
				string ls = "#";
				i++;
				while (i < str.Length)
				{
					c = str[i];
					if (c == ',' || c == ')')
						break;
					ls += c;
					i++;
				}
				ts.mIfcCartesianPoint = ParserSTEP.ParseLink(ls);
				if (c == ',')
				{
					if (str.Substring(i + 1).StartsWith("IFCPARAMETERVALUE(", true, System.Globalization.CultureInfo.CurrentCulture))
					{
						i += 19;
						string pv = "";
						while (str[i] != ')')
						{
							pv += str[i++];
						}
						ts.mIfcParameterValue = ParserSTEP.ParseDouble(pv);
					}
				}
			}
			else
			{
				if (str.Substring(i).StartsWith("IFCPARAMETERVALUE(", true, System.Globalization.CultureInfo.CurrentCulture))
				{
					i += 18;
					string pv = "";
					while (str[i] != ')')
					{
						pv += str[i++];
					}
					ts.mIfcParameterValue = ParserSTEP.ParseDouble(pv);
				}
				if (++i < str.Length)
				{
					if (str[i++] == ',')
					{
						ts.mIfcCartesianPoint = ParserSTEP.ParseLink(str.Substring(i, str.Length - i - 1));
					}
				}
			}
			return ts;
		}
		public override string ToString()
		{
			string str = "(";
			if (!double.IsNaN(mIfcParameterValue))
			{
				str += "IFCPARAMETERVALUE(" + ParserSTEP.DoubleToString(mIfcParameterValue) + ")";
				if (mIfcCartesianPoint > 0)
					str += "," + ParserSTEP.LinkToString(mIfcCartesianPoint);
				return str + ")";
			}
			else
				return str + ParserSTEP.LinkToString(mIfcCartesianPoint) + ")";
		}
	}
	public partial class IfcTShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mDepth, mFlangeWidth, mWebThickness, mFlangeThickness;// : IfcPositiveLengthMeasure;
		internal double mFilletRadius = double.NaN, mFlangeEdgeRadius = double.NaN, mWebEdgeRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mWebSlope = double.NaN, mFlangeSlope = double.NaN;// : OPTIONAL IfcPlaneAngleMeasure;
		internal double mCentreOfGravityInX = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure 

		public double Depth { get { return mDepth; } set { mDepth = value; } }
		public double FlangeWidth { get { return mFlangeWidth; } set { mFlangeWidth = value; } }
		public double WebThickness { get { return mWebThickness; } set { mWebThickness = value; } }
		public double FlangeThickness { get { return mFlangeThickness; } set { mFlangeThickness = value; } }
		public double FilletRadius { get { return mFilletRadius; } set { mFilletRadius = value; } }
		public double FlangeEdgeRadius { get { return mFlangeEdgeRadius; } set { mFlangeEdgeRadius = value; } }
		public double WebEdgeRadius { get { return mWebEdgeRadius; } set { mWebEdgeRadius = value; } }
		public double WebSlope { get { return mWebSlope; } set { mWebSlope = value; } }
		public double FlangeSlope { get { return mFlangeSlope; } set { mFlangeSlope = value; } }

		internal IfcTShapeProfileDef() : base() { }
		internal IfcTShapeProfileDef(DatabaseIfc db, IfcTShapeProfileDef p) : base(db, p)
		{
			mDepth = p.mDepth;
			mFlangeWidth = p.mFlangeWidth;
			mWebThickness = p.mWebThickness;
			mFlangeThickness = p.mFlangeThickness;
			mFilletRadius = p.mFilletRadius;
			mFlangeEdgeRadius = p.mFlangeEdgeRadius;
			mWebEdgeRadius = p.mWebEdgeRadius;
			mWebSlope = p.mWebSlope;
			mFlangeSlope = p.mFlangeSlope;
		}
		public IfcTShapeProfileDef(DatabaseIfc db, string name, double depth, double flangeWidth, double webThickness, double flangeThickness)
			: base(db,name)
		{
			mDepth = depth;
			mFlangeWidth = flangeWidth;
			mWebThickness = webThickness;
			mFlangeThickness = flangeThickness;
		}


		internal static void parseFields(IfcTShapeProfileDef p, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos);
			p.mDepth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFlangeWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mWebThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFlangeThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFlangeEdgeRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mWebEdgeRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mWebSlope = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFlangeSlope = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema == ReleaseVersion.IFC2x3)
				p.mCentreOfGravityInX = ParserSTEP.ParseDouble(arrFields[ipos++]);	
		}
		internal static IfcTShapeProfileDef Parse(string strDef,ReleaseVersion schema) { IfcTShapeProfileDef p = new IfcTShapeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mDepth) + "," + ParserSTEP.DoubleToString(mFlangeWidth) + "," +
				ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mFlangeThickness) + "," +
				ParserSTEP.DoubleOptionalToString(mFilletRadius) + "," + ParserSTEP.DoubleOptionalToString(mFlangeEdgeRadius) + "," +
				ParserSTEP.DoubleOptionalToString(mWebEdgeRadius) + "," + ParserSTEP.DoubleOptionalToString(mWebSlope) + "," +
				ParserSTEP.DoubleOptionalToString(mFlangeSlope) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInX) : "");
		}
	}
	public partial class IfcTubeBundle : IfcEnergyConversionDevice //IFC4
	{
		internal IfcTubeBundleTypeEnum mPredefinedType = IfcTubeBundleTypeEnum.NOTDEFINED;// OPTIONAL : IfcTubeBundleTypeEnum;
		public IfcTubeBundleTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTubeBundle() : base() { }
		internal IfcTubeBundle(DatabaseIfc db, IfcTubeBundle b) : base(db, b) { mPredefinedType = b.mPredefinedType; }
		public IfcTubeBundle(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcTubeBundle s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcTubeBundleTypeEnum)Enum.Parse(typeof(IfcTubeBundleTypeEnum), str);
		}
		internal new static IfcTubeBundle Parse(string strDef) { IfcTubeBundle s = new IfcTubeBundle(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcTubeBundleTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcTubeBundleType : IfcEnergyConversionDeviceType
	{
		internal IfcTubeBundleTypeEnum mPredefinedType = IfcTubeBundleTypeEnum.NOTDEFINED;// : IfcTubeBundleEnum; 
		public IfcTubeBundleTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTubeBundleType() : base() { }
		internal IfcTubeBundleType(DatabaseIfc db, IfcTubeBundleType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcTubeBundleType(DatabaseIfc m, string name, IfcTubeBundleTypeEnum t) : base(m) { Name = name; PredefinedType = t; }
		internal static void parseFields(IfcTubeBundleType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcTubeBundleTypeEnum)Enum.Parse(typeof(IfcTubeBundleTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcTubeBundleType Parse(string strDef) { IfcTubeBundleType t = new IfcTubeBundleType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcTwoDirectionRepeatFactor : IfcOneDirectionRepeatFactor // DEPRECEATED IFC4
	{
		internal int mSecondRepeatFactor;//  : IfcVector 
		public IfcVector SecondRepeatFactor { get { return mDatabase[mSecondRepeatFactor] as IfcVector; } set { mSecondRepeatFactor = value.mIndex; } }

		internal IfcTwoDirectionRepeatFactor() : base() { }
		internal IfcTwoDirectionRepeatFactor(DatabaseIfc db, IfcTwoDirectionRepeatFactor f) : base(db,f) { SecondRepeatFactor = db.Factory.Duplicate(f.SecondRepeatFactor) as IfcVector; }
		internal static void parseFields(IfcTwoDirectionRepeatFactor f, List<string> arrFields, ref int ipos) { IfcOneDirectionRepeatFactor.parseFields(f, arrFields, ref ipos); f.mSecondRepeatFactor = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal new static IfcTwoDirectionRepeatFactor Parse(string strDef) { IfcTwoDirectionRepeatFactor f = new IfcTwoDirectionRepeatFactor(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mSecondRepeatFactor); }
	}
	public partial class IfcTypeObject : IfcObjectDefinition //(IfcTypeProcess, IfcTypeProduct, IfcTypeResource) IFC4 ABSTRACT 
	{
		internal string mApplicableOccurrence = "$";// : OPTIONAL IfcLabel;
		internal List<int> mHasPropertySets = new List<int>();// : OPTIONAL SET [1:?] OF IfcPropertySetDefinition 
		//INVERSE 
		internal IfcRelDefinesByType mObjectTypeOf = null;

		public string ApplicableOccurrence { get { return (mApplicableOccurrence == "$" ? "" : ParserIfc.Decode(mApplicableOccurrence)); } set { mApplicableOccurrence = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public List<IfcPropertySetDefinition> HasPropertySets { get { return mHasPropertySets.ConvertAll(x => mDatabase[x] as IfcPropertySetDefinition); } set { mHasPropertySets = value.ConvertAll(x => x.mIndex); } }
		public IfcRelDefinesByType ObjectTypeOf { get { return mObjectTypeOf; } }
		//GeomGym
		internal IfcMaterialProfileSet mTapering = null;
		protected IfcTypeObject() : base() { }
		protected IfcTypeObject(DatabaseIfc db, IfcTypeObject t) : base(db,t) { mApplicableOccurrence = t.mApplicableOccurrence; HasPropertySets = t.HasPropertySets.ConvertAll(x=>db.Factory.Duplicate(x) as IfcPropertySetDefinition); }
		internal IfcTypeObject(DatabaseIfc db) : base(db) { IfcRelDefinesByType rdt = new IfcRelDefinesByType(this) { Name = Name }; }
		
		internal static void parseFields(IfcTypeObject t, List<string> arrFields, ref int ipos)
		{
			IfcObjectDefinition.parseFields(t, arrFields, ref ipos);
			t.mApplicableOccurrence = arrFields[ipos++];
			string str = arrFields[ipos++];
			if (str != "$")
				t.mHasPropertySets = ParserSTEP.SplitListLinks(str);
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + mApplicableOccurrence;
			if (mHasPropertySets.Count > 0)
			{
				str += ",(" + ParserSTEP.LinkToString(mHasPropertySets[0]);
				for (int icounter = 1; icounter < mHasPropertySets.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mHasPropertySets[icounter]);
				str += ")";
			}
			else
				str += ",$";
			return str;
		}
		internal static IfcTypeObject Parse(string strDef) { IfcTypeObject o = new IfcTypeObject(); int ipos = 0; parseFields(o, ParserSTEP.SplitLineFields(strDef), ref ipos); return o; }

		public void AddPropertySet(IfcPropertySetDefinition psd) { mHasPropertySets.Add(psd.mIndex); psd.mDefinesType.Add(this); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			List<IfcPropertySetDefinition> psets = HasPropertySets;
			for (int jcounter = 0; jcounter < psets.Count; jcounter++)
			{
				psets[jcounter].mDefinesType.Add(this);
			}
		}
		public override List<T> Extract<T>()
		{
			List<T> result = base.Extract<T>();
			foreach (IfcPropertySetDefinition psd in HasPropertySets)
				result.AddRange(psd.Extract<T>());
			return result;
		}
		internal IfcPropertySet findPropertySet(string name)
		{
			foreach(IfcPropertySet pset in HasPropertySets)
			{
				if (pset != null && string.Compare(pset.Name, name) == 0)
					return pset;
			}
			return null;
		}
		internal override List<IBaseClassIfc> retrieveReference(IfcReference r)
		{
			IfcReference ir = r.InnerReference;
			List<IBaseClassIfc> result = new List<IBaseClassIfc>();
			if (ir == null)
			{
				return null;
			}
			if (string.Compare(r.mAttributeIdentifier, "HasPropertySets", true) == 0)
			{

				List<IfcPropertySetDefinition> psets = HasPropertySets;
				if (r.mListPositions.Count == 0)
				{
					string name = r.InstanceName;

					if (string.IsNullOrEmpty(name))
					{
						foreach (IfcPropertySetDefinition pset in psets)
							result.AddRange(pset.retrieveReference(r.InnerReference));
					}
					else
					{
						foreach (IfcPropertySetDefinition pset in psets)
						{
							if (string.Compare(name, pset.Name) == 0)
								result.AddRange(pset.retrieveReference(r.InnerReference));
						}
					}
				}
				else
				{
					foreach (int i in r.mListPositions)
						result.AddRange(psets[i - 1].retrieveReference(ir));
				}
				return result;
			}
			return base.retrieveReference(r);
		}
		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			if (mObjectTypeOf != null)
				mObjectTypeOf.changeSchema(schema);
		}
	}
	public abstract partial class IfcTypeProcess : IfcTypeObject //ABSTRACT SUPERTYPE OF(ONEOF(IfcEventType, IfcProcedureType, IfcTaskType))
	{
		private string mIdentification = "$";// :	OPTIONAL IfcIdentifier;
		private string mLongDescription = "$";//	 :	OPTIONAL IfcText;
		private string mProcessType = "$";//	 :	OPTIONAL IfcLabel;

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string LongDescription { get { return (mLongDescription == "$" ? "" : ParserIfc.Decode(mLongDescription)); } set { mLongDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string ProcessType { get { return (mProcessType == "$" ? "" : ParserIfc.Decode(mProcessType)); } set { mProcessType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcTypeProcess() : base() { }
		protected IfcTypeProcess(DatabaseIfc db, IfcTypeProcess t) : base(db, t) { mIdentification = t.mIdentification; mLongDescription = t.mLongDescription; mProcessType = t.mProcessType; }
		protected IfcTypeProcess(DatabaseIfc db) : base(db) { }
		protected static void parseFields(IfcTypeProcess p, List<string> arrFields, ref int ipos) { IfcTypeObject.parseFields(p, arrFields, ref ipos); p.mIdentification = arrFields[ipos++].Replace("'", ""); p.mLongDescription = arrFields[ipos++].Replace("'", ""); p.mProcessType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',") + (mLongDescription == "$" ? "$," : "'" + mLongDescription + "',") + (mProcessType == "$" ? "$" : "'" + mProcessType + "'"); }
	}
	public partial class IfcTypeProduct : IfcTypeObject, IfcProductSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcDoorStyle ,IfcElementType ,IfcSpatialElementType ,IfcWindowStyle)) 
	{ 
		internal List<int> mRepresentationMaps = new List<int>();// : OPTIONAL LIST [1:?] OF UNIQUE IfcRepresentationMap;
		private string mTag = "$";// : OPTIONAL IfcLabel 
		//INVERSE
		internal List<IfcRelAssignsToProduct> mReferencedBy = new List<IfcRelAssignsToProduct>();//	 :	SET OF IfcRelAssignsToProduct FOR RelatingProduct;
		
		public List<IfcRepresentationMap> RepresentationMaps { get { return mRepresentationMaps.ConvertAll(x => mDatabase[x] as IfcRepresentationMap); } set { mRepresentationMaps = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		public string Tag { get { return (mTag == "$" ? "" : mTag); } set { mTag = (string.IsNullOrEmpty(value) ? "$" : value); } }
		public List<IfcRelAssignsToProduct> ReferencedBy { get { return mReferencedBy; } }

		protected IfcTypeProduct() : base() { }
		protected IfcTypeProduct(DatabaseIfc db, IfcTypeProduct t) : base(db,t) { RepresentationMaps = t.RepresentationMaps.ConvertAll(x=>db.Factory.Duplicate(x) as IfcRepresentationMap); mTag = t.mTag; }
		protected IfcTypeProduct(DatabaseIfc db) : base(db) {  }

		internal new static IfcTypeProduct Parse(string strDef) { IfcTypeProduct p = new IfcTypeProduct(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcTypeProduct p, List<string> arrFields, ref int ipos) { IfcTypeObject.parseFields(p, arrFields, ref ipos); p.mRepresentationMaps = ParserSTEP.SplitListLinks(arrFields[ipos++]); p.mTag = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",";
			if (mRepresentationMaps.Count > 0)
			{
				str += "(" + ParserSTEP.LinkToString(mRepresentationMaps[0]);
				for (int icounter = 1; icounter < mRepresentationMaps.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRepresentationMaps[icounter]);
				str += ")";
			}
			else
				str += "$";
			return str + (mTag == "$" ? ",$" : ",'" + mTag + "'");
		}

		internal override void postParseRelate()
		{
			base.postParseRelate();
			List<IfcRepresentationMap> repMaps = RepresentationMaps;
			for (int jcounter = 0; jcounter < repMaps.Count; jcounter++)
				repMaps[jcounter].mTypeProducts.Add(this);
		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			List<IfcRepresentationMap> repMaps = RepresentationMaps;
			for (int icounter = 0; icounter < repMaps.Count; icounter++)
				repMaps[icounter].changeSchema(schema);
			List<IfcPropertySetDefinition> psets = HasPropertySets;
			for (int icounter = 0; icounter < psets.Count; icounter++)
				psets[icounter].changeSchema(schema);
			base.changeSchema(schema);
		}

		internal IfcElement genMappedItemElement(IfcProduct container, IfcCartesianTransformationOperator3D t)
		{
			string typename = this.GetType().Name;
			typename = typename.Substring(0, typename.Length - 4);
			IfcShapeRepresentation sr = new IfcShapeRepresentation(new IfcMappedItem(RepresentationMaps[0], t));
			IfcProductDefinitionShape pds = new IfcProductDefinitionShape(sr);
			IfcElement element = IfcElement.constructElement(typename, container, null, pds);
			element.RelatingType = this;
			foreach (IfcRelNests nests in mIsNestedBy)
			{
				foreach (IfcObjectDefinition od in nests.RelatedObjects)
				{
					IfcDistributionPort port = od as IfcDistributionPort;
					if (port != null)
					{
						IfcDistributionPort newPort = new IfcDistributionPort(element) { FlowDirection = port.FlowDirection, PredefinedType = port.PredefinedType, SystemType = port.SystemType };
						newPort.Placement = new IfcLocalPlacement(element.Placement, t.generate());
						IfcLocalPlacement placement = port.Placement as IfcLocalPlacement;
						if (placement != null)
							newPort.Placement = new IfcLocalPlacement(newPort.Placement, placement.RelativePlacement);
						for (int dcounter = 0; dcounter < port.mIsDefinedBy.Count; dcounter++)
							port.mIsDefinedBy[dcounter].Assign(newPort);
					}
				}
			}
			List<IfcPropertySetDefinition> psets = HasPropertySets;
			for (int icounter = 0; icounter < psets.Count; icounter++)
			{
				IfcPropertySet pset = psets[icounter] as IfcPropertySet;
				if (pset != null)
				{
					if (pset.IsInstancePropertySet)
						pset.AssignObjectDefinition(element);
				}
			}
			return element;
		}
	}
	public abstract partial class IfcTypeResource : IfcTypeObject //ABSTRACT SUPERTYPE OF(IfcConstructionResourceType)
	{
		internal string mIdentification = "$";// :	OPTIONAL IfcIdentifier;
		internal string mLongDescription = "$";//	 :	OPTIONAL IfcText;
		internal string mResourceType = "$";//	 :	OPTIONAL IfcLabel;

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string LongDescription { get { return (mLongDescription == "$" ? "" : ParserIfc.Decode(mLongDescription)); } set { mLongDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string ResourceType { get { return (mResourceType == "$" ? "" : ParserIfc.Decode(mResourceType)); } set { mResourceType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcTypeResource() : base() { }
		protected IfcTypeResource(DatabaseIfc db, IfcTypeResource t) : base(db,t) { mIdentification = t.mIdentification; mLongDescription = t.mLongDescription; mResourceType = t.mResourceType; }
		protected IfcTypeResource(DatabaseIfc db) : base(db) { }
		protected static void parseFields(IfcTypeResource p, List<string> arrFields, ref int ipos) { IfcTypeObject.parseFields(p, arrFields, ref ipos); p.mIdentification = arrFields[ipos++].Replace("'", ""); p.mLongDescription = arrFields[ipos++].Replace("'", ""); p.mResourceType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',") + (mLongDescription == "$" ? "$," : "'" + mLongDescription + "',") + (mResourceType == "$" ? "$" : "'" + mResourceType + "'"); }
	}
}
