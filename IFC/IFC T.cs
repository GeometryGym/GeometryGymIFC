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

		internal List<IfcTableRow> Rows { get { return mRows.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcTableRow); } }
		internal List<IfcTableColumn> Columns { get { return mColumns.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcTableColumn); } }

		internal IfcTable() : base() { }
		internal IfcTable(IfcTable o) : base() { mName = o.mName; mRows.AddRange(o.mRows); mColumns.AddRange(o.mColumns); }
		public IfcTable(DatabaseIfc m, string name, List<IfcTableRow> rows, List<IfcTableColumn> cols) : base(m)
		{
			if (!string.IsNullOrEmpty(name))
				mName = name.Replace("'", "");
			if (rows != null && rows.Count > 0)
				mRows = rows.ConvertAll(x => x.mIndex);
			if (cols != null && cols.Count > 0)
				mColumns = cols.ConvertAll(x => x.mIndex);
		}
		internal static void parseFields(IfcTable t, List<string> arrFields, ref int ipos) { t.mName = arrFields[ipos++]; t.mRows = ParserSTEP.SplitListLinks(arrFields[ipos++]); t.mColumns = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
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
			if (mDatabase.mSchema != Schema.IFC2x3)
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
			return base.BuildString() + (mName == "$" ? ",$," : ",'" + mName + "',") + s;
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
		public IfcUnit Unit { get { return mDatabase.mIfcObjects[mUnit] as IfcUnit; } set { mUnit = (value == null ? 0 : value.Index); } }
		public IfcReference ReferencePath { get { return mDatabase.mIfcObjects[mReferencePath] as IfcReference; } set { mReferencePath = (value == null ? 0 : value.mIndex); } }

		internal IfcTableColumn() : base() { }
		internal IfcTableColumn(IfcTableColumn c) : base() { mIdentifier = c.mIdentifier; mName = c.mName; mDescription = c.mDescription; mUnit = c.mUnit; mReferencePath = c.mReferencePath; }
		public IfcTableColumn(DatabaseIfc db) : base(db) { }
		 
		internal static void parseFields(IfcTableColumn t, List<string> arrFields, ref int ipos)
		{
			t.mIdentifier = arrFields[ipos++];
			t.mName = arrFields[ipos++].Replace("'", "");
			t.mDescription = arrFields[ipos++];
			t.mUnit = ParserSTEP.ParseLink(arrFields[ipos++]);
			t.mReferencePath = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString() + (mIdentifier == "$" ? ",$," : ",'" + mIdentifier + "',") + (mName == "$" ? "$," : "'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + ParserSTEP.LinkToString(mUnit) + "," + ParserSTEP.LinkToString(mReferencePath)); }
		internal static IfcTableColumn Parse(string strDef) { IfcTableColumn t = new IfcTableColumn(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public class IfcTableRow : BaseClassIfc
	{
		internal List<IfcValue> mRowCells = new List<IfcValue>();// :	OPTIONAL LIST [1:?] OF IfcValue;
		internal bool mIsHeading = false; //:	:	OPTIONAL BOOLEAN;

		public List<IfcValue> RowCells { get { return mRowCells; } }

		internal IfcTableRow() : base() { }
		internal IfcTableRow(IfcTableRow o) : base() { mRowCells.AddRange(o.mRowCells); mIsHeading = o.mIsHeading; }
		public IfcTableRow(DatabaseIfc m, IfcValue val) : this(m, new List<IfcValue>() { val }, false) { }
		public IfcTableRow(DatabaseIfc m, List<IfcValue> vals, bool isHeading) : base(m)
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
		protected override string BuildString()
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
			return base.BuildString() + s + ParserSTEP.BoolToString(mIsHeading);
		}
		internal static IfcTableRow Parse(string strDef) { IfcTableRow t = new IfcTableRow(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
	}
	public class IfcTank : IfcFlowStorageDevice //IFC4
	{
		internal IfcTankTypeEnum mPredefinedType = IfcTankTypeEnum.NOTDEFINED;// OPTIONAL : IfcTankTypeEnum;
		public IfcTankTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcTank() : base() { }
		internal IfcTank(IfcTank t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcTank(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcTank s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcTankTypeEnum)Enum.Parse(typeof(IfcTankTypeEnum), str);
		}
		internal new static IfcTank Parse(string strDef) { IfcTank s = new IfcTank(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcTankTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcTankType : IfcFlowStorageDeviceType
	{
		internal IfcTankTypeEnum mPredefinedType = IfcTankTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum; 
		internal IfcTankType() : base() { }
		internal IfcTankType(IfcTankType be) : base((IfcFlowStorageDeviceType)be) { mPredefinedType = be.mPredefinedType; }
		internal static void parseFields(IfcTankType t, List<string> arrFields, ref int ipos) { IfcFlowStorageDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcTankTypeEnum)Enum.Parse(typeof(IfcTankTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcTankType Parse(string strDef) { IfcTankType t = new IfcTankType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
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
		internal IfcTaskTime TaskTime { get { return mDatabase.mIfcObjects[mTaskTime] as IfcTaskTime; } }

		internal IfcTask() : base() { }
		internal IfcTask(IfcTask o) : base(o) { mStatus = o.mStatus; mWorkMethod = o.mWorkMethod; mIsMilestone = o.mIsMilestone; mPriority = o.mPriority; mTaskTime = o.mTaskTime; mPredefinedType = o.mPredefinedType; }
		
		internal static IfcTask Parse(string strDef, Schema schema) { IfcTask t = new IfcTask(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return t; }
		internal static void parseFields(IfcTask t, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcProcess.parseFields(t, arrFields, ref ipos);
			if (schema == Schema.IFC2x3)
				t.mIdentification = arrFields[ipos++];
			t.mStatus = arrFields[ipos++];
			t.mWorkMethod = arrFields[ipos++];
			t.mIsMilestone = ParserSTEP.ParseBool(arrFields[ipos++]);
			t.mPriority = ParserSTEP.ParseInt(arrFields[ipos++]);
			if (schema != Schema.IFC2x3)
			{
				t.mTaskTime = ParserSTEP.ParseLink(arrFields[ipos++]);
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					t.mPredefinedType = (IfcTaskTypeEnum)Enum.Parse(typeof(IfcTaskTypeEnum), s.Replace(".", ""));
			}
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? ",'" + mIdentification + "'" : "") + "," + mStatus + "," + mWorkMethod + "," + ParserSTEP.BoolToString(mIsMilestone) + "," + mPriority.ToString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : "," + ParserSTEP.LinkToString(mTaskTime) + ",." + mPredefinedType.ToString() + "."); }
	}
	public class IfcTaskTime : IfcSchedulingTime //IFC4
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
		internal double mCompletion;//	 :	OPTIONAL IfcPositiveRatioMeasure; 

		internal DateTime ScheduleStart { get { return IfcDateTime.Convert(mScheduleStart); } }
		internal DateTime ScheduleFinish { get { return IfcDateTime.Convert(mScheduleFinish); } }

		internal IfcTaskTime() : base() { }
		internal IfcTaskTime(IfcTaskTime t) : base(t)
		{
			mDurationType = t.mDurationType; mScheduleDuration = t.mScheduleDuration; mScheduleStart = t.mScheduleStart; mScheduleFinish = t.mScheduleFinish;
			mEarlyStart = t.mEarlyStart; mEarlyFinish = t.mEarlyFinish; mLateStart = t.mLateStart; mLateFinish = t.mLateFinish; mFreeFloat = t.mFreeFloat; mTotalFloat = t.mTotalFloat;
			mIsCritical = t.mIsCritical; mStatusTime = t.mStatusTime; mActualDuration = t.mActualDuration; mActualStart = t.mActualStart; mActualFinish = t.mActualFinish;
			mRemainingTime = t.mRemainingTime; mCompletion = t.mCompletion;
		}
		internal IfcTaskTime(DatabaseIfc m, string name, IfcDataOriginEnum orig, string userOrigin, IfcTaskDurationEnum durationtype, IfcDuration schedDuration, DateTime schedStart, DateTime schedFinish,
			DateTime earlyStart, DateTime earlyFinish, DateTime lateStart, DateTime lateFinish, IfcDuration freeFloat, IfcDuration totalFloat, bool isCritical, IfcDuration actualDuration, DateTime actualStart,
			DateTime actualFinish, IfcDuration remainingTime, double fractionComplete)
			: base(m, name, orig, userOrigin)
		{
			mDurationType = durationtype;
			if (schedDuration != null)
				mScheduleDuration = schedDuration.ToString();
			if (schedStart != DateTime.MinValue)
				mScheduleStart = IfcDateTime.Convert(schedStart);
			if (schedFinish != DateTime.MinValue)
				mScheduleFinish = IfcDateTime.Convert(schedFinish);
			if (earlyStart != DateTime.MinValue)
				mEarlyStart = IfcDateTime.Convert(earlyStart);
			if (earlyFinish != DateTime.MinValue)
				mEarlyFinish = IfcDateTime.Convert(earlyFinish);
			if (lateStart != DateTime.MinValue)
				mLateStart = IfcDateTime.Convert(lateStart);
			if (lateFinish != DateTime.MinValue)
				mLateFinish = IfcDateTime.Convert(lateFinish);
			if (freeFloat != null)
				mFreeFloat = freeFloat.ToString();
			if (totalFloat != null)
				mTotalFloat = totalFloat.ToString();
			mIsCritical = isCritical;
			if (actualDuration != null)
				mActualDuration = actualDuration.ToString();
			if (actualStart != DateTime.MinValue)
				mActualStart = IfcDateTime.Convert(actualStart);
			if (actualFinish != DateTime.MinValue)
				mActualFinish = IfcDateTime.Convert(actualFinish);
			if (remainingTime != null)
				mRemainingTime = remainingTime.ToString();
			mCompletion = fractionComplete;
		}
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
		protected override string BuildString()
		{
			return base.BuildString() + ",." + mDurationType + (mScheduleDuration == "$" ? ".,$," : ".,'" + mScheduleDuration + "',") + (mScheduleStart == "$" ? "$," : "'" + mScheduleStart + "',") +
				(mScheduleFinish == "$" ? "$," : "'" + mScheduleFinish + "',") + (mEarlyStart == "$" ? "$," : "'" + mEarlyStart + "',") + (mEarlyFinish == "$" ? "$," : "'" + mEarlyFinish + "',") + (mLateStart == "$" ? "$," : "'" + mLateStart + "',") +
				(mLateFinish == "$" ? "$," : "'" + mLateFinish + "',") + (mFreeFloat == "$" ? "$," : "'" + mFreeFloat + "',") + (mTotalFloat == "$" ? "$," : "'" + mTotalFloat + "',") + ParserSTEP.BoolToString(mIsCritical) + "," +
				(mStatusTime == "$" ? "$," : "'" + mStatusTime + "',") + (mActualDuration == "$" ? "$," : "'" + mActualDuration + "',") + (mActualStart == "$" ? "$," : "'" + mActualStart + "',") + (mActualFinish == "$" ? "$," : "'" + mActualFinish + "',") +
				(mRemainingTime == "$" ? "$," : "'" + mRemainingTime + "',") + ParserSTEP.DoubleOptionalToString(mCompletion);
		}
	}
	public class IfcTaskType : IfcTypeProcess //IFC4
	{
		internal IfcTaskTypeEnum mPredefinedType = IfcTaskTypeEnum.NOTDEFINED;// : IfcTaskTypeEnum; 
		private string mWorkMethod = "$";// : OPTIONAL IfcLabel;

		internal IfcTaskTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal string WorkMethod { get { return (mWorkMethod == "$" ? "" : ParserIfc.Decode(mWorkMethod)); } set { mWorkMethod = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		internal IfcTaskType() : base() { }
		internal IfcTaskType(IfcTaskType t) : base(t) { mPredefinedType = t.mPredefinedType; mWorkMethod = t.mWorkMethod; }
		internal IfcTaskType(DatabaseIfc m, string name, IfcTaskTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcTaskType t, List<string> arrFields, ref int ipos) { IfcTypeProcess.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcTaskTypeEnum)Enum.Parse(typeof(IfcTaskTypeEnum), arrFields[ipos++].Replace(".", "")); t.mWorkMethod = arrFields[ipos++].Replace("'", ""); }
		internal new static IfcTaskType Parse(string strDef) { IfcTaskType t = new IfcTaskType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString() + ",." + mPredefinedType.ToString() + (mWorkMethod == "$" ? ".,$" : (".,'" + mWorkMethod + "'"))); }
	}
	public class IfcTelecomAddress : IfcAddress
	{
		internal List<string> mTelephoneNumbers = new List<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		internal List<string> mFacsimileNumbers = new List<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		internal string mPagerNumber = "$";// :OPTIONAL IfcLabel;
		internal List<string> mElectronicMailAddresses = new List<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		internal string mWWWHomePageURL = "$";// : OPTIONAL IfcLabel;
		internal List<int> mMessagingIDs = new List<int>();// : OPTIONAL LIST [1:?] OF IfcURIReference //IFC4
		internal IfcTelecomAddress() : base() { }
		internal IfcTelecomAddress(IfcTelecomAddress o) : base(o) { mTelephoneNumbers = new List<string>(o.mTelephoneNumbers.ToArray()); mFacsimileNumbers = new List<string>(o.mFacsimileNumbers.ToArray()); mPagerNumber = o.mPagerNumber; mElectronicMailAddresses = new List<string>(o.mElectronicMailAddresses.ToArray()); mWWWHomePageURL = o.mWWWHomePageURL; mMessagingIDs = new List<int>(o.mMessagingIDs); }
		//internal IfcTelecomAddress(List<string> phone,List<string> fax,string pager,List<string> email,string www,List<int>
		internal static void parseFields(IfcTelecomAddress a, List<string> arrFields, ref int ipos,Schema schema)
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
			if (schema != Schema.IFC2x3)
			{
				str = arrFields[ipos++];
				if (!str.StartsWith("$"))
					a.mMessagingIDs = ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString()
		{
			string str = base.BuildString();
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
			if (mDatabase.mSchema != Schema.IFC2x3)
			{
				if (mMessagingIDs.Count == 0)
					str += ",$";
				else
				{
					str += ",(" + ParserSTEP.LinkToString(mMessagingIDs[0]);
					for (int icounter = 1; icounter < mMessagingIDs.Count; icounter++)
						str += "," + ParserSTEP.LinkToString(mMessagingIDs[icounter]);
					str += ")";
				}
			}
			return str;
		}
		internal static IfcTelecomAddress Parse(string strDef,Schema schema) { IfcTelecomAddress a = new IfcTelecomAddress(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
	}
	public class IfcTendon : IfcReinforcingElement
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
		internal IfcTendon(IfcTendon t) : base(t)
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
		internal IfcTendon(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, double diam, double area, double forceMeasure, double pretress, double fricCoeff, double anchorSlip, double minCurveRadius)
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
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema != Schema.IFC2x3 && mPredefinedType == IfcTendonTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") + ParserSTEP.DoubleToString(mNominalDiameter) + "," +
				ParserSTEP.DoubleToString(mCrossSectionArea) + "," + ParserSTEP.DoubleToString(mTensionForce) + "," +
				ParserSTEP.DoubleToString(mPreStress) + "," + ParserSTEP.DoubleToString(mFrictionCoefficient) + "," +
				ParserSTEP.DoubleToString(mAnchorageSlip) + "," + ParserSTEP.DoubleToString(mMinCurvatureRadius);
		}
	}
	public class IfcTendonAnchor : IfcReinforcingElement
	{
		internal IfcTendonAnchorTypeEnum mPredefinedType = IfcTendonAnchorTypeEnum.NOTDEFINED;// :	OPTIONAL IfcTendonAnchorTypeEnum;
		public IfcTendonAnchorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcTendonAnchor() : base() { }
		internal IfcTendonAnchor(IfcTendonAnchor a) : base(a) { mPredefinedType = a.mPredefinedType; }
		internal IfcTendonAnchor(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static IfcTendonAnchor Parse(string strDef) { IfcTendonAnchor t = new IfcTendonAnchor(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal static void parseFields(IfcTendonAnchor a, List<string> arrFields, ref int ipos)
		{
			IfcReinforcingElement.parseFields(a, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				a.mPredefinedType = (IfcTendonAnchorTypeEnum)Enum.Parse(typeof(IfcTendonAnchorTypeEnum), str.Replace(".", ""));
		}
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcTendonAnchorTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + "."));
		}
	}
	//IfcTendonAnchorType
	//IfcTendonType
	public class IfcTerminatorSymbol : IfcAnnotationSymbolOccurrence // DEPRECEATED IFC4
	{
		internal int mAnnotatedCurve;// : IfcAnnotationCurveOccurrence; 
		internal IfcTerminatorSymbol() : base() { }
		internal IfcTerminatorSymbol(IfcTerminatorSymbol i) : base(i) { mAnnotatedCurve = i.mAnnotatedCurve; }
		internal new static IfcTerminatorSymbol Parse(string strDef) { IfcTerminatorSymbol s = new IfcTerminatorSymbol(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcTerminatorSymbol s, List<string> arrFields, ref int ipos) { IfcAnnotationSymbolOccurrence.parseFields(s, arrFields, ref ipos); s.mAnnotatedCurve = ParserSTEP.ParseLink(arrFields[ipos++]); }
	}
	public abstract partial class IfcTessellatedFaceSet : IfcTessellatedItem //ABSTRACT SUPERTYPE OF(IfcTriangulatedFaceSet)
	{
		internal int mCoordinates;// : 	IfcCartesianPointList;
		internal Tuple<double, double, double>[] mNormals = new Tuple<double, double, double>[0];// : OPTIONAL LIST [1:?] OF LIST [3:3] OF IfcParameterValue; 
		internal bool mClosed; // 	OPTIONAL BOOLEAN;
		// INVERSE
		internal IfcIndexedColourMap mHasColours = null;// : SET [0:1] OF IfcIndexedColourMap FOR MappedTo;
		internal List<IfcIndexedTextureMap> mHasTextures = new List<IfcIndexedTextureMap>();// : SET [0:?] OF IfcIndexedTextureMap FOR MappedTo;

		public IfcCartesianPointList Coordinates { get { return mDatabase.mIfcObjects[mCoordinates] as IfcCartesianPointList; } }
		protected IfcTessellatedFaceSet() : base() { }
		protected IfcTessellatedFaceSet(IfcTessellatedFaceSet s)
			: base(s)
		{
			mCoordinates = s.mCoordinates;
			if (s.mNormals.Length > 0)
				mNormals = s.mNormals;
			mClosed = s.mClosed;
		}
		protected IfcTessellatedFaceSet(DatabaseIfc m, IfcCartesianPointList3D pl, IEnumerable<Tuple<double, double, double>> normals, bool closed)
			: base(m)
		{
			mCoordinates = pl.mIndex;
			if (normals != null)
				mNormals = normals.ToArray();
			mClosed = closed;
		}
		protected override string BuildString()
		{
			string result = base.BuildString() + "," + ParserSTEP.LinkToString(mCoordinates);
			if (mNormals.Length == 0)
				result += ",$,";
			else
			{
				Tuple<double, double, double> normal = mNormals[0];
				result += ",((" + ParserSTEP.DoubleToString(normal.Item1) + "," + ParserSTEP.DoubleToString(normal.Item2) + "," + ParserSTEP.DoubleToString(normal.Item3) + ")";
				for (int icounter = 1; icounter < mNormals.Length; icounter++)
				{
					normal = mNormals[icounter];
					result += ",(" + ParserSTEP.DoubleToString(normal.Item1) + "," + ParserSTEP.DoubleToString(normal.Item2) + "," + ParserSTEP.DoubleToString(normal.Item3) + ")";
				}
				result += "),";
			}
			return result + ParserSTEP.BoolToString(mClosed);
		}
		protected override void Parse(string str, ref int pos)
		{
			base.Parse(str, ref pos);
			mCoordinates = ParserSTEP.StripLink(str, ref pos);
			string field = ParserSTEP.StripField(str, ref pos);
			if (field.StartsWith("("))
				mNormals = ParserSTEP.SplitListDoubleTriple(field);
			mClosed = ParserSTEP.StripBool(str, ref pos);
		}

	}
	public abstract class IfcTessellatedItem : IfcGeometricRepresentationItem
	{
		protected IfcTessellatedItem() : base() { }
		protected IfcTessellatedItem(IfcTessellatedItem o) : base(o) { }
		protected IfcTessellatedItem(DatabaseIfc m) : base(m) { }
		protected override void Parse(string str, ref int ipos) { base.Parse(str, ref ipos); }
	}
	public partial class IfcTextLiteral : IfcGeometricRepresentationItem
	{
		internal string mLiteral;// : IfcPresentableText;
		internal int mPlacement;// : IfcAxis2Placement;
		internal IfcTextPath mPath;// : IfcTextPath;
		internal IfcAxis2Placement Placement { get { return mDatabase.mIfcObjects[mPlacement] as IfcAxis2Placement; } }
		internal IfcTextLiteral() : base() { }
		internal IfcTextLiteral(IfcTextLiteral o) : base(o) { mLiteral = o.mLiteral; mPlacement = o.mPlacement; mPath = o.mPath; }
		internal static IfcTextLiteral Parse(string strDef) { IfcTextLiteral l = new IfcTextLiteral(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcTextLiteral l, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(l, arrFields, ref ipos); l.mLiteral = arrFields[ipos++]; l.mPlacement = ParserSTEP.ParseLink(arrFields[ipos++]); l.mPath = (IfcTextPath)Enum.Parse(typeof(IfcTextPath), arrFields[ipos++].Replace(".", "")); }
		protected override string BuildString() { return base.BuildString() + "," + mLiteral + "," + ParserSTEP.LinkToString(mPlacement) + ",." + mPath.ToString() + "."; }
	}
	public partial class IfcTextLiteralWithExtent : IfcTextLiteral
	{
		internal int mExtent;// : IfcPlanarExtent;
		internal string mBoxAlignment;// : IfcBoxAlignment; 
		internal IfcTextLiteralWithExtent() : base() { }
		internal IfcTextLiteralWithExtent(IfcTextLiteralWithExtent o) : base(o) { mExtent = o.mExtent; mBoxAlignment = o.mBoxAlignment; }

		internal new static IfcTextLiteralWithExtent Parse(string strDef) { IfcTextLiteralWithExtent l = new IfcTextLiteralWithExtent(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcTextLiteralWithExtent l, List<string> arrFields, ref int ipos) { IfcTextLiteral.parseFields(l, arrFields, ref ipos); l.mExtent = ParserSTEP.ParseLink(arrFields[ipos++]); l.mBoxAlignment = arrFields[ipos++]; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mExtent) + "," + mBoxAlignment; }
	}
	public partial class IfcTextStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal int mTextCharacterAppearance;// : OPTIONAL IfcCharacterStyleSelect;
		internal int mTextStyle;// : OPTIONAL IfcTextStyleSelect;
		internal int mTextFontStyle;// : IfcTextFontSelect; 
		internal bool mModelOrDraughting = true;//	:	OPTIONAL BOOLEAN; IFC4CHANGE
		internal IfcTextStyle() : base() { }
		internal IfcTextStyle(IfcTextStyle v) : base(v) { mTextCharacterAppearance = v.mTextCharacterAppearance; mTextStyle = v.mTextStyle; mTextFontStyle = v.mTextFontStyle; mModelOrDraughting = v.mModelOrDraughting; }
		internal static void parseFields(IfcTextStyle s, List<string> arrFields, ref int ipos,Schema schema)
		{
			IfcPresentationStyle.parseFields(s, arrFields, ref ipos);
			s.mTextCharacterAppearance = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mTextStyle = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mTextFontStyle = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (schema != Schema.IFC2x3)
				s.mModelOrDraughting = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		internal static IfcTextStyle Parse(string strDef,Schema schema) { IfcTextStyle s = new IfcTextStyle(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return s; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mTextCharacterAppearance) + "," + ParserSTEP.LinkToString(mTextStyle) + "," + ParserSTEP.LinkToString(mTextFontStyle) + (mDatabase.mSchema != Schema.IFC2x3 ? "," + ParserSTEP.BoolToString(mModelOrDraughting) : ""); }
	}
	public class IfcTextStyleFontModel : IfcPreDefinedTextFont
	{
		internal List<string> mFontFamily = new List<string>(1);// : OPTIONAL LIST [1:?] OF IfcTextFontName;
		internal string mFontStyle = "$";// : OPTIONAL IfcFontStyle; ['normal','italic','oblique'];
		internal string mFontVariant = "$";// : OPTIONAL IfcFontVariant; ['normal','small-caps'];
		internal string mFontWeight = "$";// : OPTIONAL IfcFontWeight; // ['normal','small-caps','100','200','300','400','500','600','700','800','900'];
		internal string mFontSize;// : IfcSizeSelect; IfcSizeSelect = SELECT (IfcRatioMeasure ,IfcLengthMeasure ,IfcDescriptiveMeasure ,IfcPositiveLengthMeasure ,IfcNormalisedRatioMeasure ,IfcPositiveRatioMeasure);
		internal IfcTextStyleFontModel() : base() { }
		internal IfcTextStyleFontModel(IfcTextStyleFontModel i) : base(i)
		{
			mFontFamily = new List<string>(i.mFontFamily.ToArray());
			mFontStyle = i.mFontStyle;
			mFontVariant = i.mFontVariant;
			mFontWeight = i.mFontWeight;
			mFontSize = i.mFontSize;
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
		protected override string BuildString()
		{
			string str = base.BuildString();
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
	public class IfcTextStyleForDefinedFont : BaseClassIfc
	{
		internal int mColour;// : IfcColour;
		internal int mBackgroundColour;// : OPTIONAL IfcColour;
		internal IfcTextStyleForDefinedFont() : base() { }
		internal IfcTextStyleForDefinedFont(IfcTextStyleForDefinedFont o) : base() { mColour = o.mColour; mBackgroundColour = o.mBackgroundColour; }
		internal static void parseFields(IfcTextStyleForDefinedFont f, List<string> arrFields, ref int ipos) { f.mColour = ParserSTEP.ParseLink(arrFields[ipos++]); f.mBackgroundColour = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mColour) + "," + ParserSTEP.LinkToString(mBackgroundColour); }
		internal static IfcTextStyleForDefinedFont Parse(string strDef) { IfcTextStyleForDefinedFont f = new IfcTextStyleForDefinedFont(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
	}
	public class IfcTextStyleTextModel : IfcPresentationItem
	{
		//internal int mDiffuseTransmissionColour, mDiffuseReflectionColour, mTransmissionColour, mReflectanceColour;//	 :	IfcColourRgb;
		internal IfcTextStyleTextModel() : base() { }
		internal IfcTextStyleTextModel(IfcTextStyleTextModel m) : base(m) { }
	 
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
	public abstract class IfcTextureCoordinate : IfcPresentationItem  //ABSTRACT SUPERTYPE OF(ONEOF(IfcIndexedTextureMap, IfcTextureCoordinateGenerator, IfcTextureMap))
	{
		internal List<int> mMaps = new List<int>();// : LIST [1:?] OF IfcSurfaceTexture
		public List<IfcSurfaceTexture> Maps { get { return mMaps.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcSurfaceTexture); } }

		internal IfcTextureCoordinate() : base() { }
		internal IfcTextureCoordinate(IfcTextureCoordinate c) : base() { mMaps = c.mMaps; }
		public IfcTextureCoordinate(DatabaseIfc m, List<IfcSurfaceTexture> maps) : base(m) { mMaps = maps.ConvertAll(x => x.mIndex); }

		protected override void parseFields(List<string> arrFields, ref int pos)
		{
			mMaps = ParserSTEP.SplitListLinks(arrFields[pos++]);
		}
		protected override string BuildString()
		{
			string result = base.BuildString() + ",(#" + mMaps[0];
			for (int icounter = 1; icounter < mMaps.Count; icounter++)
				result += ",#" + mMaps[icounter];
			return result + ")";
		}
	}
	//ENTITY IfcTextureCoordinateGenerator
	//ENTITY IfcTextureMap
	//ENTITY IfcTextureVertex;
	public class IfcTextureVertexList : IfcPresentationItem
	{
		internal Tuple<double, double>[] mTexCoordsList = new Tuple<double, double>[0];// : LIST [1:?] OF IfcSurfaceTexture

		internal IfcTextureVertexList() : base() { }
		internal IfcTextureVertexList(IfcTextureVertexList c) : base() { mTexCoordsList = c.mTexCoordsList; }
		public IfcTextureVertexList(DatabaseIfc m, IEnumerable<Tuple<double, double>> coords) : base(m) { mTexCoordsList = coords.ToArray(); }

		internal static IfcTextureVertexList Parse(string strDef) { IfcTextureVertexList l = new IfcTextureVertexList(); int pos = 0; l.parseFields(ParserSTEP.SplitLineFields(strDef), ref pos); return l; }
		protected override void parseFields(List<string> arrFields, ref int ipos) { base.parseFields(arrFields, ref ipos); mTexCoordsList = ParserSTEP.SplitListDoubleTuple(arrFields[ipos++]); }
		protected override string BuildString()
		{
			Tuple<double, double> pair = mTexCoordsList[0];
			string result = base.BuildString() + ",((" + ParserSTEP.DoubleToString(pair.Item1) + "," + ParserSTEP.DoubleToString(pair.Item2);
			for (int icounter = 1; icounter < mTexCoordsList.Length; icounter++)
			{
				pair = mTexCoordsList[icounter];
				result += "),(" + ParserSTEP.DoubleToString(pair.Item1) + "," + ParserSTEP.DoubleToString(pair.Item2);
			}

			return result + "))";
		}
	}
	public class IfcThermalMaterialProperties : IfcMaterialPropertiesSuperSeded // DEPRECEATED IFC4
	{
		internal double mSpecificHeatCapacity;// : OPTIONAL IfcSpecificHeatCapacityMeasure;
		internal double mBoilingPoint;// : OPTIONAL IfcThermodynamicTemperatureMeasure;
		internal double mFreezingPoint;// : OPTIONAL IfcThermodynamicTemperatureMeasure;
		internal double mThermalConductivity;// : OPTIONAL IfcThermalConductivityMeasure; 
		internal IfcThermalMaterialProperties() : base() { }
		internal IfcThermalMaterialProperties(IfcThermalMaterialProperties el) : base(el) { mSpecificHeatCapacity = el.mSpecificHeatCapacity; mBoilingPoint = el.mBoilingPoint; mFreezingPoint = el.mFreezingPoint; mThermalConductivity = el.mThermalConductivity; }
		internal static IfcThermalMaterialProperties Parse(string strDef) { IfcThermalMaterialProperties p = new IfcThermalMaterialProperties(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcThermalMaterialProperties p, List<string> arrFields, ref int ipos)
		{
			IfcMaterialPropertiesSuperSeded.parseFields(p, arrFields, ref ipos);
			p.mSpecificHeatCapacity = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mBoilingPoint = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFreezingPoint = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mThermalConductivity = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mSpecificHeatCapacity) + "," + ParserSTEP.DoubleOptionalToString(mBoilingPoint) + "," + ParserSTEP.DoubleOptionalToString(mFreezingPoint) + "," + ParserSTEP.DoubleOptionalToString(mThermalConductivity); }
	}
	public struct IfcTime
	{
		internal static string convert(DateTime date) { return (date.Hour < 10 ? "T0" : "T") + date.Hour + (date.Minute < 10 ? "-0" : "-") + date.Minute + (date.Second < 10 ? "-0" : "-") + date.Second; }
	}
	public interface IfcTimeOrRatioSelect { string String { get; } } // IFC4 	IfcRatioMeasure, IfcDuration	
	public class IfcTimePeriod : BaseClassIfc // IFC4
	{
		internal string mStart; //:	IfcTime;
		internal string mFinish; //:	IfcTime;
		internal IfcTimePeriod() : base() { }
		internal IfcTimePeriod(IfcTimePeriod m) : base() { mStart = m.mStart; mFinish = m.mFinish; }
		internal IfcTimePeriod(DatabaseIfc m, DateTime start, DateTime finish) : base(m) { mStart = IfcTime.convert(start); mFinish = IfcTime.convert(finish);}
		internal static IfcTimePeriod Parse(string strDef) { IfcTimePeriod m = new IfcTimePeriod(); int ipos = 0; parseFields(m, ParserSTEP.SplitLineFields(strDef), ref ipos); return m; }
		internal static void parseFields(IfcTimePeriod m, List<string> arrFields, ref int ipos) { m.mStart = arrFields[ipos++]; m.mFinish = arrFields[ipos++]; }
		protected override string BuildString() { return base.BuildString() + ",'" + mStart + "','" + mFinish + "'"; }
	}
	public abstract class IfcTimeSeries : BaseClassIfc, IfcMetricValueSelect, IfcResourceObjectSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcIrregularTimeSeries,IfcRegularTimeSeries));
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
		protected IfcTimeSeries(IfcTimeSeries i)
			: base()
		{
			mName = i.mName;
			mDescription = i.mDescription;
			mStartTime = i.mStartTime;
			mEndTime = i.mEndTime;
			mTimeSeriesDataType = i.mTimeSeriesDataType;
			mDataOrigin = i.mDataOrigin;
			mUserDefinedDataOrigin = i.mUserDefinedDataOrigin;
			mUnit = i.mUnit;
		}
		protected IfcTimeSeries(DatabaseIfc m) : base(m) { }
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
		protected override string BuildString() { return base.BuildString() + ",'" + mName + "','" + mDescription + "'," + ParserSTEP.LinkToString(mStartTime) + "," + ParserSTEP.LinkToString(mEndTime) + ",." + mTimeSeriesDataType.ToString() + ".,." + mDataOrigin.ToString() + ".," + mUserDefinedDataOrigin + "," + ParserSTEP.LinkToString(mUnit); }
	}
	//ENTITY IfcTimeSeriesReferenceRelationship; // DEPRECEATED IFC4
	//ENTITY IfcTimeSeriesSchedule // DEPRECEATED IFC4
	//ENTITY IfcTimeSeriesValue;  
	public abstract partial class IfcTopologicalRepresentationItem : IfcRepresentationItem  /*(IfcConnectedFaceSet,IfcEdge,IfcFace,IfcFaceBound,IfcLoop,IfcPath,IfcVertex))*/
	{
		protected IfcTopologicalRepresentationItem() : base() { }
		protected IfcTopologicalRepresentationItem(IfcTopologicalRepresentationItem el) : base(el) { }
		protected IfcTopologicalRepresentationItem(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcTopologicalRepresentationItem i, List<string> arrFields, ref int ipos) { IfcRepresentationItem.parseFields(i, arrFields, ref ipos); }
	}
	public partial class IfcTopologyRepresentation : IfcShapeModel
	{
		internal IfcTopologyRepresentation() : base() { }
		internal IfcTopologyRepresentation(IfcTopologyRepresentation c) : base(c) { }
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
	public class IfcTransformer : IfcEnergyConversionDevice //IFC4
	{
		internal IfcTransformerTypeEnum mPredefinedType = IfcTransformerTypeEnum.NOTDEFINED;// OPTIONAL : IfcTransformerTypeEnum;
		public IfcTransformerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcTransformer() : base() { }
		internal IfcTransformer(IfcTransformer b) : base(b) { mPredefinedType = b.mPredefinedType; }
		internal IfcTransformer(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcTransformer s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcTransformerTypeEnum)Enum.Parse(typeof(IfcTransformerTypeEnum), str);
		}
		internal new static IfcTransformer Parse(string strDef) { IfcTransformer s = new IfcTransformer(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcTransformerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcTransformerType : IfcEnergyConversionDeviceType
	{
		internal IfcTransformerTypeEnum mPredefinedType = IfcTransformerTypeEnum.NOTDEFINED;// : IfcTransformerEnum; 
		public IfcTransformerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcTransformerType() : base() { }
		internal IfcTransformerType(IfcTransformerType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcTransformerType(DatabaseIfc m, string name, IfcTransformerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcTransformerType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcTransformerTypeEnum)Enum.Parse(typeof(IfcTransformerTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcTransformerType Parse(string strDef) { IfcTransformerType t = new IfcTransformerType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcTranslationalStiffnessSelect
	{
		internal bool mRigid = false;
		internal IfcLinearStiffnessMeasure mStiffness = null;
		internal IfcTranslationalStiffnessSelect(bool fix) { mRigid = fix; }
		internal IfcTranslationalStiffnessSelect(double stiff) { mStiffness = new IfcLinearStiffnessMeasure(stiff); }
		internal IfcTranslationalStiffnessSelect(IfcLinearStiffnessMeasure stiff) { mStiffness = stiff; }
		internal static IfcTranslationalStiffnessSelect Parse(string str,Schema schema)
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
			if (schema == Schema.IFC2x3)
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
	public class IfcTransportElement : IfcElement
	{
		internal IfcTransportElement() : base() { }
		internal IfcTransportElement(IfcTransportElement be) : base(be) { }
		internal IfcTransportElement(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static void parseFields(IfcTransportElement e, List<string> arrFields, ref int ipos) { IfcElement.parseFields(e, arrFields, ref ipos); }
		internal static IfcTransportElement Parse(string strDef) { IfcTransportElement e = new IfcTransportElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
	}
	public class IfcTransportElementType : IfcElementType
	{
		internal IfcTransportElementTypeEnum mPredefinedType;// IfcTransportElementTypeEnum; 
		public IfcTransportElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcTransportElementType() : base() { }
		internal IfcTransportElementType(IfcTransportElementType o) : base(o) { mPredefinedType = o.mPredefinedType; }
		public IfcTransportElementType(DatabaseIfc m, string name, IfcTransportElementTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal new static IfcTransportElementType Parse(string strDef) { IfcTransportElementType t = new IfcTransportElementType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal static void parseFields(IfcTransportElementType t, List<string> arrFields, ref int ipos) { IfcElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcTransportElementTypeEnum)Enum.Parse(typeof(IfcTransportElementTypeEnum), arrFields[ipos++].Replace(".", "")); }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcTrapeziumProfileDef : IfcParameterizedProfileDef
	{
		internal double mBottomXDim;// : IfcPositiveLengthMeasure;
		internal double mTopXDim;// : IfcPositiveLengthMeasure;
		internal double mYDim;// : IfcPositiveLengthMeasure;
		internal double mTopXOffset;// : IfcPositiveLengthMeasure; 
		internal IfcTrapeziumProfileDef() : base() { }
		internal IfcTrapeziumProfileDef(IfcTrapeziumProfileDef c) : base(c) { mBottomXDim = c.mBottomXDim; mTopXDim = c.mTopXDim; mYDim = c.mYDim; mTopXOffset = c.mTopXOffset; }
		internal IfcTrapeziumProfileDef(DatabaseIfc db, string name,double bottomXDim, double topXDim,double yDim,double topXOffset) : base(db)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.If2x3NotAssigned)
				throw new Exception("Invalid Model View for IfcTrapeziumProfileDef : " + db.ModelView.ToString());
			Name = name;
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
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mBottomXDim) + "," + ParserSTEP.DoubleToString(mTopXDim) + "," + ParserSTEP.DoubleToString(mYDim) + "," + ParserSTEP.DoubleToString(mTopXOffset); }
	}
	public partial class IfcTriangulatedFaceSet : IfcTessellatedFaceSet
	{
		internal Tuple<int, int, int>[] mCoordIndex = new Tuple<int, int, int>[0];// : 	LIST [1:?] OF LIST [3:3] OF INTEGER;
		internal Tuple<int, int, int>[] mNormalIndex = new Tuple<int, int, int>[0];// :	OPTIONAL LIST [1:?] OF LIST [3:3] OF INTEGER;  
		internal IfcTriangulatedFaceSet() : base() { }
		internal IfcTriangulatedFaceSet(IfcTriangulatedFaceSet s) : base(s)
		{
			mCoordIndex = s.mCoordIndex;
			mNormalIndex = s.mNormalIndex;
		}
		public IfcTriangulatedFaceSet(DatabaseIfc m, IfcCartesianPointList3D pl, List<Tuple<double, double, double>> normals, bool closed, IEnumerable<Tuple<int, int, int>> coords, IEnumerable<Tuple<int, int, int>> normalIndices)
			: base(m, pl, normals, closed) { mCoordIndex = coords.ToArray(); if (normals != null) mNormalIndex = normalIndices.ToArray(); }
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
			mCoordIndex = ParserSTEP.SplitListSTPIntTriple(field);
			field = ParserSTEP.StripField(str, ref pos);
			if (field.StartsWith("("))
				mNormalIndex = ParserSTEP.SplitListSTPIntTriple(field);
		}
		protected override string BuildString()
		{
			StringBuilder sb = new StringBuilder();
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
			return base.BuildString() + sb.ToString();
		}
	}
	public partial class IfcTrimmedCurve : IfcBoundedCurve
	{
		private int mBasisCurve;//: IfcCurve;
		internal IfcTrimmingSelect mTrim1;// : SET [1:2] OF IfcTrimmingSelect;
		internal IfcTrimmingSelect mTrim2;//: SET [1:2] OF IfcTrimmingSelect;
		private bool mSenseAgreement;// : BOOLEAN;

		internal IfcCurve BasisCurve { get { return mDatabase.mIfcObjects[mBasisCurve] as IfcCurve; } }
		internal bool SenseAgreement { get { return mSenseAgreement; } }

		internal IfcTrimmingPreference mMasterRepresentation = IfcTrimmingPreference.UNSPECIFIED;// : IfcTrimmingPreference; 
		internal IfcTrimmedCurve() : base() { }
		internal IfcTrimmedCurve(IfcTrimmedCurve c) : base(c) { mBasisCurve = c.mBasisCurve; mTrim1 = c.mTrim1; mTrim2 = c.mTrim2; mSenseAgreement = c.mSenseAgreement; mMasterRepresentation = c.mMasterRepresentation; }
		internal IfcTrimmedCurve(IfcCurve basis, IfcTrimmingSelect start, IfcTrimmingSelect end, bool senseAgreement, IfcTrimmingPreference tp) : base(basis.mDatabase)
		{
			mBasisCurve = basis.mIndex;
			mTrim1 = start;
			mTrim2 = end;
			mSenseAgreement = senseAgreement;
			mMasterRepresentation = tp;
		}
		
		internal static void parseFields(IfcTrimmedCurve c, List<string> arrFields, ref int ipos) { IfcBoundedCurve.parseFields(c, arrFields, ref ipos); c.mBasisCurve = ParserSTEP.ParseLink(arrFields[ipos++]); c.mTrim1 = IfcTrimmingSelect.Parse(arrFields[ipos++]); c.mTrim2 = IfcTrimmingSelect.Parse(arrFields[ipos++]); c.mSenseAgreement = ParserSTEP.ParseBool(arrFields[ipos++]); c.mMasterRepresentation = (IfcTrimmingPreference)Enum.Parse(typeof(IfcTrimmingPreference), arrFields[ipos++].Replace(".", "")); }
		internal static IfcTrimmedCurve Parse(string strDef) { IfcTrimmedCurve c = new IfcTrimmedCurve(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mBasisCurve) + "," + mTrim1.ToString() + "," + mTrim2.ToString() + "," + ParserSTEP.BoolToString(mSenseAgreement) + ",." + mMasterRepresentation.ToString() + "."; }
	}
	public struct IfcTrimmingSelect
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
		internal double mFilletRadius, mFlangeEdgeRadius, mWebEdgeRadius;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mWebSlope, mFlangeSlope;// : OPTIONAL IfcPlaneAngleMeasure;
		//internal double mCentreOfGravityInX;// : OPTIONAL IfcPositiveLengthMeasure 

		internal double Depth { get { return mDepth; } set { mDepth = value; } }
		internal double FlangeWidth { get { return mFlangeWidth; } set { mFlangeWidth = value; } }
		internal double WebThickness { get { return mWebThickness; } set { mWebThickness = value; } }
		internal double FlangeThickness { get { return mFlangeThickness; } set { mFlangeThickness = value; } }
		internal double FilletRadius { get { return mFilletRadius; } set { mFilletRadius = value; } }
		internal double FlangeEdgeRadius { get { return mFlangeEdgeRadius; } set { mFlangeEdgeRadius = value; } }
		internal double WebEdgeRadius { get { return mWebEdgeRadius; } set { mWebEdgeRadius = value; } }
		internal double WebSlope { get { return mWebSlope; } set { mWebSlope = value; } }
		internal double FlangeSlope { get { return mFlangeSlope; } set { mFlangeSlope = value; } }

		internal IfcTShapeProfileDef() : base() { }
		internal IfcTShapeProfileDef(IfcTShapeProfileDef p)
			: base(p)
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
		
		internal static void parseFields(IfcTShapeProfileDef p, List<string> arrFields, ref int ipos,Schema schema)
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
			if (schema == Schema.IFC2x3)
				ipos++;
		}
		internal static IfcTShapeProfileDef Parse(string strDef,Schema schema) { IfcTShapeProfileDef p = new IfcTShapeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		protected override string BuildString()
		{
			return base.BuildString() + "," + ParserSTEP.DoubleToString(mDepth) + "," + ParserSTEP.DoubleToString(mFlangeWidth) + "," +
				ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mFlangeThickness) + "," +
				ParserSTEP.DoubleOptionalToString(mFilletRadius) + "," + ParserSTEP.DoubleOptionalToString(mFlangeEdgeRadius) + "," +
				ParserSTEP.DoubleOptionalToString(mWebEdgeRadius) + "," + ParserSTEP.DoubleOptionalToString(mWebSlope) + "," +
				ParserSTEP.DoubleOptionalToString(mFlangeSlope) + (mDatabase.mSchema == Schema.IFC2x3 ? ",$" : "");
		}
	}
	public class IfcTubeBundle : IfcEnergyConversionDevice //IFC4
	{
		internal IfcTubeBundleTypeEnum mPredefinedType = IfcTubeBundleTypeEnum.NOTDEFINED;// OPTIONAL : IfcTubeBundleTypeEnum;
		public IfcTubeBundleTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcTubeBundle() : base() { }
		internal IfcTubeBundle(IfcTubeBundle b) : base(b) { mPredefinedType = b.mPredefinedType; }
		internal IfcTubeBundle(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcTubeBundle s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcTubeBundleTypeEnum)Enum.Parse(typeof(IfcTubeBundleTypeEnum), str);
		}
		internal new static IfcTubeBundle Parse(string strDef) { IfcTubeBundle s = new IfcTubeBundle(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcTubeBundleTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcTubeBundleType : IfcEnergyConversionDeviceType
	{
		internal IfcTubeBundleTypeEnum mPredefinedType = IfcTubeBundleTypeEnum.NOTDEFINED;// : IfcTubeBundleEnum; 
		public IfcTubeBundleTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcTubeBundleType() : base() { }
		internal IfcTubeBundleType(IfcTubeBundleType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcTubeBundleType(DatabaseIfc m, string name, IfcTubeBundleTypeEnum t) : base(m) { Name = name; PredefinedType = t; }
		internal static void parseFields(IfcTubeBundleType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcTubeBundleTypeEnum)Enum.Parse(typeof(IfcTubeBundleTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcTubeBundleType Parse(string strDef) { IfcTubeBundleType t = new IfcTubeBundleType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcTwoDirectionRepeatFactor : IfcOneDirectionRepeatFactor // DEPRECEATED IFC4
	{
		internal int mSecondRepeatFactor;//  : IfcVector 
		internal IfcTwoDirectionRepeatFactor() : base() { }
		internal IfcTwoDirectionRepeatFactor(IfcTwoDirectionRepeatFactor p) : base(p) { mSecondRepeatFactor = p.mSecondRepeatFactor; }
		internal static void parseFields(IfcTwoDirectionRepeatFactor f, List<string> arrFields, ref int ipos) { IfcOneDirectionRepeatFactor.parseFields(f, arrFields, ref ipos); f.mSecondRepeatFactor = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal new static IfcTwoDirectionRepeatFactor Parse(string strDef) { IfcTwoDirectionRepeatFactor f = new IfcTwoDirectionRepeatFactor(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mSecondRepeatFactor); }
	}
	public partial class IfcTypeObject : IfcObjectDefinition //(IfcTypeProcess, IfcTypeProduct, IfcTypeResource) IFC4 ABSTRACT 
	{
		internal string mApplicableOccurrence = "$";// : OPTIONAL IfcLabel;
		internal List<int> mHasPropertySets = new List<int>();// : OPTIONAL SET [1:?] OF IfcPropertySetDefinition 
		//INVERSE 
		internal IfcRelDefinesByType mObjectTypeOf = null;
		public IfcRelDefinesByType ObjectTypeOf { get { return mObjectTypeOf; } }
		public List<IfcPropertySetDefinition> HasPropertySets { get { return mHasPropertySets.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcPropertySetDefinition); } set { mHasPropertySets = value.ConvertAll(x => x.mIndex); } }
		//GeomGym
		internal IfcMaterialProfileSet mTapering = null;
		protected IfcTypeObject() : base() { }
		protected IfcTypeObject(IfcTypeObject obj) : base(obj) { mApplicableOccurrence = obj.mApplicableOccurrence; mHasPropertySets = new List<int>(obj.mHasPropertySets.ToArray()); }
		internal IfcTypeObject(DatabaseIfc m) : base(m) { IfcRelDefinesByType rdt = new IfcRelDefinesByType(this) { Name = Name }; }
		
		internal static void parseFields(IfcTypeObject t, List<string> arrFields, ref int ipos)
		{
			IfcObjectDefinition.parseFields(t, arrFields, ref ipos);
			t.mApplicableOccurrence = arrFields[ipos++];
			string str = arrFields[ipos++];
			if (str != "$")
				t.mHasPropertySets = ParserSTEP.SplitListLinks(str);
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + mApplicableOccurrence;
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

		public void AddPropertySet(IfcPropertySetDefinition psd) { mHasPropertySets.Add(psd.mIndex); }
	}
	public abstract class IfcTypeProcess : IfcTypeObject //ABSTRACT SUPERTYPE OF(ONEOF(IfcEventType, IfcProcedureType, IfcTaskType))
	{
		private string mIdentification = "$";// :	OPTIONAL IfcIdentifier;
		private string mLongDescription = "$";//	 :	OPTIONAL IfcText;
		private string mProcessType = "$";//	 :	OPTIONAL IfcLabel;

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string LongDescription { get { return (mLongDescription == "$" ? "" : ParserIfc.Decode(mLongDescription)); } set { mLongDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string ProcessType { get { return (mProcessType == "$" ? "" : ParserIfc.Decode(mProcessType)); } set { mProcessType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcTypeProcess() : base() { }
		protected IfcTypeProcess(IfcTypeProcess o) : base(o) { mIdentification = o.mIdentification; mLongDescription = o.mLongDescription; mProcessType = o.mProcessType; }
		protected IfcTypeProcess(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcTypeProcess p, List<string> arrFields, ref int ipos) { IfcTypeObject.parseFields(p, arrFields, ref ipos); p.mIdentification = arrFields[ipos++].Replace("'", ""); p.mLongDescription = arrFields[ipos++].Replace("'", ""); p.mProcessType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',") + (mLongDescription == "$" ? "$," : "'" + mLongDescription + "',") + (mProcessType == "$" ? "$" : "'" + mProcessType + "'"); }
	}
	public partial class IfcTypeProduct : IfcTypeObject, IfcProductSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcDoorStyle ,IfcElementType ,IfcSpatialElementType ,IfcWindowStyle)) 
	{ 
		internal List<int> mRepresentationMaps = new List<int>();// : OPTIONAL LIST [1:?] OF UNIQUE IfcRepresentationMap;
		private string mTag = "$";// : OPTIONAL IfcLabel 

		public List<IfcRepresentationMap> RepresentationMaps { get { return mRepresentationMaps.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcRepresentationMap); } set { mRepresentationMaps = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		public string Tag { get { return (mTag == "$" ? "" : mTag); } set { mTag = (string.IsNullOrEmpty(value) ? "$" : value); } }
		//INVERSE
		internal List<IfcRelAssignsToProduct> mReferencedBy = new List<IfcRelAssignsToProduct>();//	 :	SET OF IfcRelAssignsToProduct FOR RelatingProduct;
		public List<IfcRelAssignsToProduct> ReferencedBy { get { return mReferencedBy; } }

		protected IfcTypeProduct() : base() { }
		protected IfcTypeProduct(IfcTypeProduct o) : base(o) { mRepresentationMaps = new List<int>(o.mRepresentationMaps.ToArray()); mTag = o.mTag; }
		protected IfcTypeProduct(DatabaseIfc m) : base(m) { if (mDatabase.mContext != null) mDatabase.mContext.AddDeclared(this); }

		internal new static IfcTypeProduct Parse(string strDef) { IfcTypeProduct p = new IfcTypeProduct(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcTypeProduct p, List<string> arrFields, ref int ipos) { IfcTypeObject.parseFields(p, arrFields, ref ipos); p.mRepresentationMaps = ParserSTEP.SplitListLinks(arrFields[ipos++]); p.mTag = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",";
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
	}
	public abstract class IfcTypeResource : IfcTypeObject //ABSTRACT SUPERTYPE OF(IfcConstructionResourceType)
	{
		internal string mIdentification = "$";// :	OPTIONAL IfcIdentifier;
		internal string mLongDescription = "$";//	 :	OPTIONAL IfcText;
		internal string mResourceType = "$";//	 :	OPTIONAL IfcLabel;

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string LongDescription { get { return (mLongDescription == "$" ? "" : ParserIfc.Decode(mLongDescription)); } set { mLongDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string ResourceType { get { return (mResourceType == "$" ? "" : ParserIfc.Decode(mResourceType)); } set { mResourceType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcTypeResource() : base() { }
		protected IfcTypeResource(IfcTypeResource o) : base(o) { mIdentification = o.mIdentification; mLongDescription = o.mLongDescription; mResourceType = o.mResourceType; }
		protected IfcTypeResource(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcTypeResource p, List<string> arrFields, ref int ipos) { IfcTypeObject.parseFields(p, arrFields, ref ipos); p.mIdentification = arrFields[ipos++].Replace("'", ""); p.mLongDescription = arrFields[ipos++].Replace("'", ""); p.mResourceType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + (mIdentification == "$" ? ",$," : ",'" + mIdentification + "',") + (mLongDescription == "$" ? "$," : "'" + mLongDescription + "',") + (mResourceType == "$" ? "$" : "'" + mResourceType + "'"); }
	}
}
