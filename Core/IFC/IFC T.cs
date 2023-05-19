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
using System.Collections.Specialized;
using System.Reflection;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public partial class IfcTable : BaseClassIfc, IfcMetricValueSelect, IfcObjectReferenceSelect, NamedObjectIfc
	{
		internal string mName = ""; //:	OPTIONAL IfcLabel;
		private LIST<IfcTableRow> mRows = new LIST<IfcTableRow>();// OPTIONAL LIST [1:?] OF IfcTableRow;
		private LIST<IfcTableColumn> mColumns = new LIST<IfcTableColumn>();// :	OPTIONAL LIST [1:?] OF IfcTableColumn;

		public string Name { get { return mName; } set { mName = value; } }
		public LIST<IfcTableRow> Rows { get { return mRows; } }
		public LIST<IfcTableColumn> Columns { get { return mColumns; } }

		internal IfcTable() : base() { }
		public IfcTable(DatabaseIfc db) : base(db) { }
		internal IfcTable(DatabaseIfc db, IfcTable t) : base(db) 
		{ 
			mName = t.mName;
			mRows.AddRange(t.Rows.Select(x=> db.Factory.Duplicate(x) as IfcTableRow));
			mColumns.AddRange(t.Columns.Select(x => db.Factory.Duplicate(x) as IfcTableColumn)); 
		}
		public IfcTable(string name, IEnumerable<IfcTableRow> rows, IEnumerable<IfcTableColumn> cols) 
			: base(rows == null || rows.Count() == 0 ? cols.First().mDatabase : rows.First().mDatabase)
		{
			Name = name;
			mRows.AddRange(rows);
			mColumns.AddRange(cols);
		}
	}
	[Serializable]
	public partial class IfcTableColumn : BaseClassIfc, NamedObjectIfc
	{
		internal string mIdentifier = "";//	 :	OPTIONAL IfcIdentifier;
		internal string mName = "";//	 :	OPTIONAL IfcLabel;
		internal string mDescription = "";//	 :	OPTIONAL IfcText;
		internal IfcUnit mUnit;//	 :	OPTIONAL IfcUnit;
		private IfcReference mReferencePath;//	 :	OPTIONAL IfcReference;

		public string Identifier { get { return mIdentifier; } set { mIdentifier = value; } }
		public string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public IfcUnit Unit { get { return mUnit; } set { mUnit = value; } }
		public IfcReference ReferencePath { get { return mReferencePath; } set { mReferencePath = value; } }

		internal IfcTableColumn() : base() { }
		public IfcTableColumn(DatabaseIfc db) : base(db) { }
		internal IfcTableColumn(DatabaseIfc db, IfcTableColumn c) : base(db, c)
		{ 
			mIdentifier = c.mIdentifier;
			mName = c.mName; 
			mDescription = c.mDescription; 
			if (c.mUnit != null) 
				Unit = db.Factory.Duplicate<IfcUnit>(c.mUnit);
			if (c.mReferencePath != null)
				ReferencePath = db.Factory.Duplicate(c.ReferencePath);
		}
	}
	[Serializable]
	public partial class IfcTableRow : BaseClassIfc
	{
		internal LIST<IfcValue> mRowCells = new LIST<IfcValue>();// :	OPTIONAL LIST [1:?] OF IfcValue;
		internal bool mIsHeading = false; //:	:	OPTIONAL BOOLEAN;

		public LIST<IfcValue> RowCells { get { return mRowCells; } }
		public bool IsHeading { get { return mIsHeading; } set { mIsHeading = value; } }

		internal IfcTableRow() : base() { }
		internal IfcTableRow(DatabaseIfc db, IfcTableRow r) : base(db, r) { mRowCells = r.mRowCells; mIsHeading = r.mIsHeading; }
		public IfcTableRow(DatabaseIfc db, IfcValue val) : this(db, new List<IfcValue>() { val }, false) { }
		public IfcTableRow(DatabaseIfc db, List<IfcValue> vals, bool isHeading) : base(db)
		{
			mRowCells.AddRange(vals);
			mIsHeading = isHeading;
		}
	}
	[Serializable]
	public partial class IfcTank : IfcFlowStorageDevice //IFC4
	{
		private IfcTankTypeEnum mPredefinedType = IfcTankTypeEnum.NOTDEFINED;// OPTIONAL : IfcTankTypeEnum;
		public IfcTankTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTankTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcTank() : base() { }
		internal IfcTank(DatabaseIfc db, IfcTank t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcTank(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcTankType : IfcFlowStorageDeviceType
	{
		private IfcTankTypeEnum mPredefinedType = IfcTankTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum; 
		public IfcTankTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTankTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcTankType() : base() { }
		internal IfcTankType(DatabaseIfc db, IfcTankType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcTankType(DatabaseIfc db, string name, IfcTankTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcTask : IfcProcess //SUPERTYPE OF (ONEOF(IfcMove,IfcOrderAction) both DEPRECATED IFC4) 
	{
		//internal string mTaskId; //  : 	IfcIdentifier; IFC4 midentification at IfcProcess
		private string mStatus = "";// : OPTIONAL IfcLabel;
		internal string mWorkMethod = "";// : OPTIONAL IfcLabel;
		internal bool mIsMilestone = false;// : BOOLEAN
		internal int mPriority = int.MinValue;// : OPTIONAL INTEGER IFC4
		internal IfcTaskTime mTaskTime = null;// : OPTIONAL IfcTaskTime; IFC4
		private IfcTaskTypeEnum mPredefinedType = IfcTaskTypeEnum.NOTDEFINED;// : OPTIONAL IfcTaskTypeEnum IFC4
		//INVERSE
		internal SET<IfcRelAssignsTasks> mOwningControls = new SET<IfcRelAssignsTasks>(); //gg

		public string Status { get { return mStatus; } set { mStatus = value; } }
		public string WorkMethod { get { return mWorkMethod; } set { mWorkMethod = value; } }
		public bool IsMilestone { get { return mIsMilestone; } set { mIsMilestone = value; } }
		public int Priority { get { return mPriority; } set { mPriority = value; } }
		public IfcTaskTime TaskTime { get { return mTaskTime; } set { mTaskTime = value; } }
		public IfcTaskTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTaskTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcTask() : base() { }
		internal IfcTask(DatabaseIfc db, IfcTask t, DuplicateOptions options) : base(db, t, options)
		{
			mStatus = t.mStatus;
			mWorkMethod = t.mWorkMethod;
			mIsMilestone = t.mIsMilestone;
			mPriority = t.mPriority;
			if (t.mTaskTime != null)
				TaskTime = db.Factory.Duplicate(t.TaskTime) as IfcTaskTime;
			PredefinedType = t.PredefinedType;
		}
		public IfcTask(DatabaseIfc db) : base(db) { }
		public IfcTask(IfcTask task) : base(task)
		{
			mStatus = task.mStatus;
			mWorkMethod = task.mWorkMethod;
			mIsMilestone = task.mIsMilestone;
			mPriority = task.mPriority;
			mTaskTime = task.mTaskTime;
			PredefinedType = task.PredefinedType;
		}

		public Tuple<DateTime, DateTime> computeScheduleStartFinish()
		{
			DateTime scheduledStart = DateTime.MaxValue, scheduledFinish = DateTime.MinValue;
			if(mTaskTime != null)
			{
				if (mTaskTime.ScheduleStart > DateTime.MinValue && mTaskTime.ScheduleStart < scheduledStart)
					scheduledStart = mTaskTime.ScheduleStart;
				if (mTaskTime.ScheduleFinish > scheduledFinish)
					scheduledFinish = mTaskTime.ScheduleFinish;
			}
			List<IfcTask> subTasks = mIsDecomposedBy.SelectMany(x => x.RelatedObjects).OfType<IfcTask>().ToList();
			subTasks.AddRange(IsNestedBy.SelectMany(x => x.RelatedObjects).OfType<IfcTask>().ToList());
			foreach(IfcTask t in subTasks)
			{
				Tuple<DateTime, DateTime> startFinish = t.computeScheduleStartFinish();
				if (startFinish.Item1 != DateTime.MinValue && startFinish.Item1 < scheduledStart)
					scheduledStart = startFinish.Item1;
				if (startFinish.Item2 > scheduledStart)
					scheduledFinish = startFinish.Item2;
			}
			return new Tuple<DateTime, DateTime>(scheduledStart, scheduledFinish);

		}

	}
	[Serializable]
	public partial class IfcTaskTime : IfcSchedulingTime //IFC4
	{
		internal IfcTaskDurationEnum mDurationType = IfcTaskDurationEnum.NOTDEFINED;    // :	OPTIONAL IfcTaskDurationEnum;
		internal IfcDuration mScheduleDuration = null;//	 :	OPTIONAL IfcDuration;
		internal DateTime mScheduleStart = DateTime.MinValue, mScheduleFinish = DateTime.MinValue, mEarlyStart = DateTime.MinValue, mEarlyFinish = DateTime.MinValue, mLateStart = DateTime.MinValue, mLateFinish = DateTime.MinValue; //:	OPTIONAL IfcDateTime;
		internal IfcDuration mFreeFloat = null, mTotalFloat = null;//	 :	OPTIONAL IfcDuration;
		internal bool mIsCritical;//	 :	OPTIONAL BOOLEAN;
		internal DateTime mStatusTime = DateTime.MinValue;//	 :	OPTIONAL IfcDateTime;
		internal IfcDuration mActualDuration = null;//	 :	OPTIONAL IfcDuration;
		internal DateTime mActualStart = DateTime.MinValue, mActualFinish = DateTime.MinValue;//	 :	OPTIONAL IfcDateTime;
		internal IfcDuration mRemainingTime = null;//	 :	OPTIONAL IfcDuration;
		internal double mCompletion = double.NaN;//	 :	OPTIONAL IfcPositiveRatioMeasure; 

		public IfcTaskDurationEnum DurationType { get { return mDurationType; } set { mDurationType = value; } }
		public IfcDuration ScheduleDuration { get { return mScheduleDuration; } set { mScheduleDuration = value; } }
		public DateTime ScheduleStart { get { return mScheduleStart; } set { mScheduleStart = value; } }
		public DateTime ScheduleFinish { get { return mScheduleFinish; } set { mScheduleFinish = value; } }
		public DateTime EarlyStart { get { return mEarlyStart; } set { mEarlyStart = value; } }
		public DateTime EarlyFinish { get { return mEarlyFinish; } set { mEarlyFinish = value; } }
		public DateTime LateStart { get { return mLateStart; } set { mLateStart = value; } }
		public DateTime LateFinish { get { return mLateFinish; } set { mLateFinish = value; } }
		public IfcDuration FreeFloat { get { return mFreeFloat; } set { mFreeFloat = value; } }
		public IfcDuration TotalFloat { get { return mTotalFloat; } set { mTotalFloat = value; } }
		public bool IsCritical { get { return mIsCritical; } set { mIsCritical = value; } }
		public DateTime StatusTime { get { return mStatusTime; } set { mStatusTime = value; } }
		public IfcDuration ActualDuration { get { return mActualDuration; } set { mActualDuration = value; } }
		public DateTime ActualStart { get { return mActualStart; } set { mActualStart = value; } }
		public DateTime ActualFinish { get { return mActualFinish; } set { mActualFinish = value; } }
		public IfcDuration RemainingTime { get { return mRemainingTime; } set { mRemainingTime = value; } }
		public double Completion { get { return mCompletion; } set { mCompletion = value; } }

		internal IfcTaskTime() : base() { }
		internal IfcTaskTime(DatabaseIfc db, IfcTaskTime t, DuplicateOptions options) : base(db, t, options)
		{
			mDurationType = t.mDurationType; mScheduleDuration = t.mScheduleDuration; mScheduleStart = t.mScheduleStart; mScheduleFinish = t.mScheduleFinish;
			mEarlyStart = t.mEarlyStart; mEarlyFinish = t.mEarlyFinish; mLateStart = t.mLateStart; mLateFinish = t.mLateFinish; mFreeFloat = t.mFreeFloat; mTotalFloat = t.mTotalFloat;
			mIsCritical = t.mIsCritical; mStatusTime = t.mStatusTime; mActualDuration = t.mActualDuration; mActualStart = t.mActualStart; mActualFinish = t.mActualFinish;
			mRemainingTime = t.mRemainingTime; mCompletion = t.mCompletion;
		}
		public IfcTaskTime(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcTaskTimeRecurring : IfcTaskTime
	{
		private IfcRecurrencePattern mRecurrence = null; //: IfcRecurrencePattern;
		public IfcRecurrencePattern Recurrence { get { return mRecurrence; } set { mRecurrence = value; } }

		public IfcTaskTimeRecurring() : base() { }
		public IfcTaskTimeRecurring(IfcRecurrencePattern recurrence)
			: base(recurrence.Database) { Recurrence = recurrence; }
	}
	[Serializable]
	public partial class IfcTaskType : IfcTypeProcess //IFC4
	{
		private IfcTaskTypeEnum mPredefinedType = IfcTaskTypeEnum.NOTDEFINED;// : IfcTaskTypeEnum; 
		private string mWorkMethod = "";// : OPTIONAL IfcLabel;

		public IfcTaskTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTaskTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public string WorkMethod { get { return mWorkMethod; } set { mWorkMethod = value; } }

		internal IfcTaskType() : base() { }
		internal IfcTaskType(DatabaseIfc db, IfcTaskType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; mWorkMethod = t.mWorkMethod; }
		public IfcTaskType(DatabaseIfc db, string name, IfcTaskTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcTelecomAddress : IfcAddress
	{
		internal LIST<string> mTelephoneNumbers = new LIST<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		internal LIST<string> mFacsimileNumbers = new LIST<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		internal string mPagerNumber = "";// :OPTIONAL IfcLabel;
		internal LIST<string> mElectronicMailAddresses = new LIST<string>();// : OPTIONAL LIST [1:?] OF IfcLabel;
		internal string mWWWHomePageURL = "";// : OPTIONAL IfcLabel;
		internal LIST<string> mMessagingIDs = new LIST<string>();// : OPTIONAL LIST [1:?] OF IfcURIReference //IFC4

		public LIST<string> TelephoneNumbers { get { return mTelephoneNumbers; } }
		public LIST<string> FacsimileNumbers { get { return mFacsimileNumbers; } }
		public string PagerNumber { get { return mPagerNumber; } set { mPagerNumber = value; } }
		public LIST<string> ElectronicMailAddresses { get { return mElectronicMailAddresses; } }
		public string WWWHomePageURL { get { return mWWWHomePageURL; } set { mWWWHomePageURL = value; } }
		public LIST<string> MessagingIDs { get { return mMessagingIDs; } }

		internal IfcTelecomAddress() : base() { }
		internal IfcTelecomAddress(DatabaseIfc db, IfcTelecomAddress a) : base(db, a) { mTelephoneNumbers.AddRange(a.mTelephoneNumbers); mFacsimileNumbers.AddRange(a.mFacsimileNumbers); mPagerNumber = a.mPagerNumber; mElectronicMailAddresses.AddRange(a.mElectronicMailAddresses); mWWWHomePageURL = a.mWWWHomePageURL; mMessagingIDs.AddRange(a.mMessagingIDs); }
		public IfcTelecomAddress(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcTendon : IfcReinforcingElement
	{
		private IfcTendonTypeEnum mPredefinedType = IfcTendonTypeEnum.NOTDEFINED;// : IfcTendonTypeEnum;//
		internal double mNominalDiameter;// : IfcPositiveLengthMeasure;
		internal double mCrossSectionArea;// : IfcAreaMeasure;
		internal double mTensionForce;// : OPTIONAL IfcForceMeasure;
		internal double mPreStress;// : OPTIONAL IfcPressureMeasure;
		internal double mFrictionCoefficient;// //: OPTIONAL IfcNormalisedRatioMeasure;
		internal double mAnchorageSlip;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mMinCurvatureRadius;// : OPTIONAL IfcPositiveLengthMeasure; 
		public IfcTendonTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTendonTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		internal IfcTendon() : base() { }
		internal IfcTendon(DatabaseIfc db, IfcTendon t, DuplicateOptions options) : base(db, t, options)
		{
			PredefinedType = t.PredefinedType;
			mNominalDiameter = t.mNominalDiameter;
			mCrossSectionArea = t.mCrossSectionArea;
			mTensionForce = t.mTensionForce;
			mPreStress = t.mPreStress;
			mFrictionCoefficient = t.mFrictionCoefficient;
			mAnchorageSlip = t.mAnchorageSlip;
			mMinCurvatureRadius = t.mMinCurvatureRadius;
		}
		public IfcTendon(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		public IfcTendon(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, double diam, double area)
			: base(host, placement, representation)
		{
			mNominalDiameter = diam;
			mCrossSectionArea = area;
		}
	}
	[Serializable]
	public partial class IfcTendonAnchor : IfcReinforcingElement
	{
		private IfcTendonAnchorTypeEnum mPredefinedType = IfcTendonAnchorTypeEnum.NOTDEFINED;// :	OPTIONAL IfcTendonAnchorTypeEnum;
		public IfcTendonAnchorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTendonAnchorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcTendonAnchor() : base() { }
		internal IfcTendonAnchor(DatabaseIfc db, IfcTendonAnchor a, DuplicateOptions options) : base(db, a, options) { PredefinedType = a.PredefinedType; }
		public IfcTendonAnchor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcTendonAnchorType : IfcReinforcingElementType
	{
		private IfcTendonAnchorTypeEnum mPredefinedType = IfcTendonAnchorTypeEnum.NOTDEFINED; //: IfcTendonAnchorTypeEnum;
		public IfcTendonAnchorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTendonAnchorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcTendonAnchorType() : base() { }
		public IfcTendonAnchorType(DatabaseIfc db, string name, IfcTendonAnchorTypeEnum predefinedType)
			: base(db) { Name = name; PredefinedType = predefinedType; }
	}
	[Serializable]
	public partial class IfcTendonConduit : IfcReinforcingElement
	{
		private IfcTendonConduitTypeEnum mPredefinedType = IfcTendonConduitTypeEnum.NOTDEFINED; //: OPTIONAL IfcTendonConduitTypeEnum;
		public IfcTendonConduitTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTendonConduitTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcTendonConduit() : base() { }
		public IfcTendonConduit(DatabaseIfc db, IfcTendonConduitTypeEnum predefinedType)
			: base(db) { PredefinedType = predefinedType; }
		public IfcTendonConduit(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcTendonConduitType : IfcReinforcingElementType
	{
		private IfcTendonConduitTypeEnum mPredefinedType = IfcTendonConduitTypeEnum.NOTDEFINED; //: IfcTendonConduitTypeEnum;
		public IfcTendonConduitTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTendonConduitTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcTendonConduitType() : base() { }
		public IfcTendonConduitType(DatabaseIfc db, string name, IfcTendonConduitTypeEnum predefinedType)
			: base(db, name) { PredefinedType = predefinedType; }
	}
	[Serializable]
	public partial class IfcTendonType : IfcReinforcingElementType  //IFC4
	{
		private IfcTendonTypeEnum mPredefinedType = IfcTendonTypeEnum.NOTDEFINED;// : IfcTendonType; //IFC4
		private double mNominalDiameter;// : IfcPositiveLengthMeasure; 	IFC4 OPTIONAL
		internal double mCrossSectionArea;// : IfcAreaMeasure; IFC4 OPTIONAL
		internal double mSheathDiameter;// : OPTIONAL IfcPositiveLengthMeasure;

		public IfcTendonTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTendonTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public double NominalDiameter { get { return mNominalDiameter; } set { mNominalDiameter = value; } }

		internal IfcTendonType() : base() { }
		internal IfcTendonType(DatabaseIfc db, IfcTendonType t, DuplicateOptions options) : base(db, t, options)
		{
			PredefinedType = t.PredefinedType;
			mNominalDiameter = t.mNominalDiameter;
			mCrossSectionArea = t.mCrossSectionArea;
			mSheathDiameter = t.mSheathDiameter;
		}
		public IfcTendonType(DatabaseIfc db, string name, IfcTendonTypeEnum type) 
			: base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcTerminatorSymbol : IfcAnnotationSymbolOccurrence // DEPRECATED IFC4
	{
		internal IfcAnnotationCurveOccurrence  mAnnotatedCurve;// : IfcAnnotationCurveOccurrence; 
		public IfcAnnotationCurveOccurrence AnnotatedCurve {  get { return mAnnotatedCurve; } set { mAnnotatedCurve = value; } } 
		internal IfcTerminatorSymbol() : base() { }
		internal IfcTerminatorSymbol(DatabaseIfc db, IfcTerminatorSymbol s, DuplicateOptions options) : base(db, s, options) { mAnnotatedCurve = db.Factory.Duplicate(s.mAnnotatedCurve); }
		public IfcTerminatorSymbol(IfcPresentationStyleAssignment style, IfcAnnotationCurveOccurrence curve)
			: base(style) { mAnnotatedCurve = curve; }


	}
	[Serializable]
	public abstract partial class IfcTessellatedFaceSet : IfcTessellatedItem, IfcBooleanOperand //ABSTRACT SUPERTYPE OF(IfcTriangulatedFaceSet, IfcPolygonalFaceSet )
	{
		internal IfcCartesianPointList mCoordinates;// : 	IfcCartesianPointList;
		internal IfcLogicalEnum mClosed = IfcLogicalEnum.UNKNOWN; // 	OPTIONAL BOOLEAN;

		// INVERSE
		internal IfcIndexedColourMap mHasColours = null;// : SET [0:1] OF IfcIndexedColourMap FOR MappedTo;
		internal SET<IfcIndexedTextureMap> mHasTextures = new SET<IfcIndexedTextureMap>();// : SET [0:?] OF IfcIndexedTextureMap FOR MappedTo;

		public IfcCartesianPointList Coordinates { get { return mCoordinates; } set { mCoordinates = value; } }
		public bool Closed { get { return mClosed == IfcLogicalEnum.TRUE; } set { mClosed = value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE; } }
		public IfcIndexedColourMap HasColours { get { return mHasColours; } set { mHasColours = value; } }
		public SET<IfcIndexedTextureMap> HasTextures { get { return mHasTextures; } }

		protected IfcTessellatedFaceSet() : base() { }
		protected IfcTessellatedFaceSet(DatabaseIfc db, IfcTessellatedFaceSet s, DuplicateOptions options) : base(db, s, options) { Coordinates = db.Factory.Duplicate(s.Coordinates) as IfcCartesianPointList; }
		protected IfcTessellatedFaceSet(IfcCartesianPointList pl) : base(pl.mDatabase) { Coordinates = pl; }
	}
	[Serializable]
	public abstract partial class IfcTessellatedItem : IfcGeometricRepresentationItem //IFC4
	{
		protected IfcTessellatedItem() : base() { }
		protected IfcTessellatedItem(DatabaseIfc db, IfcTessellatedItem i, DuplicateOptions options) : base(db, i, options) { }
		protected IfcTessellatedItem(DatabaseIfc db) : base(db) { }
	}
	public interface IfcTextFontSelect : IBaseClassIfc { } // SELECT(IfcPreDefinedTextFont, IfcExternallyDefinedTextFont);
	[Serializable]
	public partial class IfcTextLiteral : IfcGeometricRepresentationItem //SUPERTYPE OF	(IfcTextLiteralWithExtent)
	{
		internal string mLiteral = "";// : IfcPresentableText;
		internal IfcAxis2Placement mPlacement;// : IfcAxis2Placement;
		internal IfcTextPath mPath;// : IfcTextPath;

		public string Literal { get { return mLiteral; } set { mLiteral = value; } }
		public IfcAxis2Placement Placement { get { return mPlacement; } }
		public IfcTextPath Path { get { return mPath; } set { mPath = value; } }

		internal IfcTextLiteral() : base() { }
		internal IfcTextLiteral(DatabaseIfc db, IfcTextLiteral l, DuplicateOptions options) : base(db, l, options)
		{
			mLiteral = l.mLiteral; 
			mPlacement = db.Factory.Duplicate<IfcAxis2Placement>(l.mPlacement);
			mPath = l.mPath; 
		}
		public IfcTextLiteral(string literal, IfcAxis2Placement placement, IfcTextPath path)
			: base(placement.Database)
		{
			Literal = literal;
			mPlacement = placement;
			mPath = path;
		}
	}
	[Serializable]
	public partial class IfcTextLiteralWithExtent : IfcTextLiteral
	{
		internal IfcPlanarExtent mExtent;// : IfcPlanarExtent;
		internal IfcBoxAlignment mBoxAlignment = IfcBoxAlignment.CENTER;// : IfcBoxAlignment; 

		public IfcPlanarExtent Extent { get { return mExtent; } set { mExtent = value; } }

		internal IfcTextLiteralWithExtent() : base() { }
		public IfcTextLiteralWithExtent(string literal, IfcAxis2Placement placement, IfcTextPath path, IfcPlanarExtent extent, IfcBoxAlignment alignment)
			: base(literal, placement, path)
		{
			mExtent = extent;
			mBoxAlignment = alignment;
		}
	}
	[Serializable]
	public partial class IfcTextStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal IfcCharacterStyleSelect mTextCharacterAppearance;// : OPTIONAL IfcCharacterStyleSelect;
		internal IfcTextStyleSelect mTextStyle;// : OPTIONAL IfcTextStyleSelect;
		internal IfcTextFontSelect mTextFontStyle;// : IfcTextFontSelect; 
		internal bool mModelOrDraughting = true;//	:	OPTIONAL BOOLEAN; IFC4CHANGE
		internal IfcTextStyle() : base() { }
		internal IfcTextStyle(DatabaseIfc db, IfcTextStyle v, DuplicateOptions options) : base(db, v, options) 
		{
			mTextCharacterAppearance = db.Factory.Duplicate(v.mTextCharacterAppearance, options); 
			mTextStyle = db.Factory.Duplicate(v.mTextStyle, options);
			mTextFontStyle = db.Factory.Duplicate(v.mTextFontStyle, options);
			mModelOrDraughting = v.mModelOrDraughting; 
		}
	}
	[Serializable]
	public partial class IfcTextStyleFontModel : IfcPreDefinedTextFont
	{
		internal List<string> mFontFamily = new List<string>(1);// : OPTIONAL LIST [1:?] OF IfcTextFontName;
		internal string mFontStyle = "";// : OPTIONAL IfcFontStyle; ['normal','italic','oblique'];
		internal string mFontVariant = "";// : OPTIONAL IfcFontVariant; ['normal','small-caps'];
		internal string mFontWeight = "";// : OPTIONAL IfcFontWeight; // ['normal','small-caps','100','200','300','400','500','600','700','800','900'];
		internal IfcSizeSelect mFontSize = null;// : IfcSizeSelect; IfcSizeSelect = SELECT (IfcRatioMeasure ,IfcLengthMeasure ,IfcDescriptiveMeasure ,IfcPositiveLengthMeasure ,IfcNormalisedRatioMeasure ,IfcPositiveRatioMeasure);
		internal IfcTextStyleFontModel() : base() { }
		internal IfcTextStyleFontModel(DatabaseIfc db, IfcTextStyleFontModel m, DuplicateOptions options) : base(db, m, options)
		{
			mFontFamily.AddRange(m.mFontFamily);
			mFontStyle = m.mFontStyle;
			mFontVariant = m.mFontVariant;
			mFontWeight = m.mFontWeight;
			mFontSize = m.mFontSize;
		}
		public IfcTextStyleFontModel(DatabaseIfc db, string name, IfcSizeSelect size) : base(db, name)
		{
			mFontSize = size; 
		}
	}
	[Serializable]
	public partial class IfcTextStyleForDefinedFont : IfcPresentationItem
	{
		internal IfcColour mColour;// : IfcColour;
		internal IfcColour mBackgroundColour;// : OPTIONAL IfcColour;
		internal IfcTextStyleForDefinedFont() : base() { }
		//	internal IfcTextStyleForDefinedFont(IfcTextStyleForDefinedFont o) : base() { mColour = o.mColour; mBackgroundColour = o.mBackgroundColour; }
	}
	public interface IfcTextStyleSelect : IBaseClassIfc { } // SELECT(IfcTextStyleWithBoxCharacteristics, IfcTextStyleTextModel);
	[Serializable]
	public partial class IfcTextStyleTextModel : IfcPresentationItem, IfcTextStyleSelect
	{
		private IfcSizeSelect mTextIndent = null; //: OPTIONAL IfcSizeSelect;
		private string mTextAlign = ""; //: OPTIONAL IfcTextAlignment;
		private string mTextDecoration = ""; //: OPTIONAL IfcTextDecoration;
		private IfcSizeSelect mLetterSpacing = null; //: OPTIONAL IfcSizeSelect;
		private IfcSizeSelect mWordSpacing = null; //: OPTIONAL IfcSizeSelect;
		private string mTextTransform = ""; //: OPTIONAL IfcTextTransformation;
		private IfcSizeSelect mLineHeight = null; //: OPTIONAL IfcSizeSelect;

		public IfcSizeSelect TextIndent { get { return mTextIndent; } set { mTextIndent = value; } }
		public string TextAlign { get { return mTextAlign; } set { mTextAlign = value; } }
		public string TextDecoration { get { return mTextDecoration; } set { mTextDecoration = value; } }
		public IfcSizeSelect LetterSpacing { get { return mLetterSpacing; } set { mLetterSpacing = value; } }
		public IfcSizeSelect WordSpacing { get { return mWordSpacing; } set { mWordSpacing = value; } }
		public string TextTransform { get { return mTextTransform; } set { mTextTransform = value; } }
		public IfcSizeSelect LineHeight { get { return mLineHeight; } set { mLineHeight = value; } }

		public IfcTextStyleTextModel() : base() { }
		public IfcTextStyleTextModel(DatabaseIfc db) : base(db) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcTextStyleWithBoxCharacteristics : BaseClassIfc, IfcTextStyleSelect // DEPRECATED IFC4
	{
		internal double mBoxHeight = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mBoxWidth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mBoxSlantAngle = double.NaN;// : OPTIONAL IfcPlaneAngleMeasure;
		internal double mBoxRotateAngle = double.NaN;// : OPTIONAL IfcPlaneAngleMeasure; 
		internal IfcSizeSelect mCharacterSpacing = null;// : OPTIONAL IfcSizeSelect
		internal IfcTextStyleWithBoxCharacteristics() : base() { }
		internal IfcTextStyleWithBoxCharacteristics(DatabaseIfc db, IfcTextStyleWithBoxCharacteristics p, DuplicateOptions options) 
			: base(db) 
		{ 
			mBoxHeight = p.mBoxHeight;
			mBoxWidth = p.mBoxWidth; 
			mBoxSlantAngle = p.mBoxSlantAngle;
			mBoxRotateAngle = p.mBoxRotateAngle;
			mCharacterSpacing = p.mCharacterSpacing;
		}
	}
	[Serializable]
	public abstract partial class IfcTextureCoordinate : IfcPresentationItem  //ABSTRACT SUPERTYPE OF(ONEOF(IfcIndexedTextureMap, IfcTextureCoordinateGenerator, IfcTextureMap))
	{
		internal LIST<IfcSurfaceTexture> mMaps = new LIST<IfcSurfaceTexture>();// : LIST [1:?] OF IfcSurfaceTexture
		public LIST<IfcSurfaceTexture> Maps { get { return mMaps; } }

		protected IfcTextureCoordinate() : base() { }
		protected IfcTextureCoordinate(DatabaseIfc db) : base(db) { }
		protected IfcTextureCoordinate(DatabaseIfc db, IfcTextureCoordinate c, DuplicateOptions options) : base(db, c, options) { Maps.AddRange(c.Maps.Select(x=> db.Factory.Duplicate(x) as IfcSurfaceTexture)); }
		protected IfcTextureCoordinate(IEnumerable<IfcSurfaceTexture> maps) : base(maps.First().Database) { mMaps.AddRange(maps); }
	}
	[Serializable]
	public partial class IfcTextureCoordinateGenerator : IfcTextureCoordinate
	{
		private string mMode = ""; //: IfcLabel;
		private List<double> mParameter = new List<double>(); //: OPTIONAL LIST[1:?] OF IfcReal;

		public string Mode { get { return mMode; } set { mMode = value; } }
		public List<double> Parameter { get { return mParameter; } }

		public IfcTextureCoordinateGenerator() : base() { }
		public IfcTextureCoordinateGenerator(IEnumerable<IfcSurfaceTexture> maps, string mode)
			: base(maps) { Mode = mode; }
	}
	[Serializable]
	public partial class IfcTextureCoordinateIndices : BaseClassIfc
	{
		private List<int> mTexCoordIndex = new List<int>();// : LIST[3:?] OF IfcPositiveInteger;
		private IfcIndexedPolygonalFace mTexCoordsOf = null;//: IfcIndexedPolygonalFace;
		//INVERSE
		private IfcIndexedPolygonalTextureMap mToTexMap = null;// : IfcIndexedPolygonalTextureMap FOR TexCoordIndices;

		public List<int> TexCoordIndex { get { return mTexCoordIndex; } }
		public IfcIndexedPolygonalFace TexCoordsOf { get { return mTexCoordsOf; } set { mTexCoordsOf = value; } }
		public IfcIndexedPolygonalTextureMap ToTexMap { get { return mToTexMap; } set { mToTexMap = value; } }

		public IfcTextureCoordinateIndices() : base() { }
		public IfcTextureCoordinateIndices(DatabaseIfc db) : base(db) { }
		public IfcTextureCoordinateIndices(IEnumerable<int> texCoordIndex, IfcIndexedPolygonalFace texCoordsOf)
			: base(texCoordsOf.Database)
		{
			mTexCoordIndex.AddRange(texCoordIndex);
			TexCoordsOf = texCoordsOf;
		}
	}
	[Serializable]
	public partial class IfcTextureCoordinateIndicesWithVoids : IfcTextureCoordinateIndices
	{
		private List<List<int>> mInnerTexCoordIndices = new List<List<int>>();// : LIST[3:?] OF IfcPositiveInteger;
		public List<List<int>> InnerTexCoordIndices { get { return mInnerTexCoordIndices; } }

		public IfcTextureCoordinateIndicesWithVoids() : base() { }
		public IfcTextureCoordinateIndicesWithVoids(DatabaseIfc db) : base(db) { }
		public IfcTextureCoordinateIndicesWithVoids(IEnumerable<int> texCoordIndex, IfcIndexedPolygonalFace texCoordsOf, IEnumerable<List<int>> innerTexCoordIndices)
			: base(texCoordIndex, texCoordsOf)
		{
			mInnerTexCoordIndices.AddRange(InnerTexCoordIndices);
		}
	}
	[Serializable]
	public partial class IfcTextureMap : IfcTextureCoordinate
	{
		private LIST<IfcTextureVertex> mVertices = new LIST<IfcTextureVertex>(); //: LIST[3:?] OF IfcTextureVertex;
		private IfcFace mMappedTo = null; //: IfcFace;

		public LIST<IfcTextureVertex> Vertices { get { return mVertices; } set { mVertices = value; } }
		public IfcFace MappedTo { get { return mMappedTo; } set { mMappedTo = value; mMappedTo.HasTextureMaps.Add(this); } }

		public IfcTextureMap() : base() { }
		public IfcTextureMap(DatabaseIfc db) : base(db) { }
		public IfcTextureMap(IEnumerable<IfcSurfaceTexture> maps, IEnumerable<IfcTextureVertex> vertices, IfcFace mappedTo)
			: base(maps)
		{
			Vertices.AddRange(vertices);
			MappedTo = mappedTo;
		}
	}
	[Serializable]
	public partial class IfcTextureVertex : IfcPresentationItem
	{
		private LIST<double> mCoordinates = new LIST<double>(); //: LIST[2:2] OF IfcParameterValue;
		public LIST<double> Coordinates { get { return mCoordinates; } set { mCoordinates = value; } }

		public IfcTextureVertex() : base() { }
		public IfcTextureVertex(DatabaseIfc db, IEnumerable<double> coordinates)
			: base(db) { Coordinates.AddRange(coordinates); }
	}
	[Serializable]
	public partial class IfcTextureVertexList : IfcPresentationItem
	{
		internal List<Tuple<double,double>> mTexCoordsList = new List<Tuple<double, double>>();// : LIST [1:?] OF LIST [2:2] OF IfcParameterValue;
		public List<Tuple<double, double>> TexCoordsList {  get { return mTexCoordsList; } }

		internal IfcTextureVertexList() : base() { }
		internal IfcTextureVertexList(DatabaseIfc db, IfcTextureVertexList l, DuplicateOptions options) : base(db, l, options) { mTexCoordsList = l.mTexCoordsList; }
		public IfcTextureVertexList(DatabaseIfc db, IEnumerable<Tuple<double, double>> coords) : base(db) { mTexCoordsList.AddRange(coords); }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcThermalMaterialProperties : IfcMaterialProperties // DEPRECATED IFC4
	{
		internal double mSpecificHeatCapacity = double.NaN;// : OPTIONAL IfcSpecificHeatCapacityMeasure;
		internal double mBoilingPoint = double.NaN;// : OPTIONAL IfcThermodynamicTemperatureMeasure;
		internal double mFreezingPoint = double.NaN;// : OPTIONAL IfcThermodynamicTemperatureMeasure;
		internal double mThermalConductivity = double.NaN;// : OPTIONAL IfcThermalConductivityMeasure; 
		internal IfcThermalMaterialProperties() : base() { }
		internal IfcThermalMaterialProperties(DatabaseIfc db, IfcThermalMaterialProperties p, DuplicateOptions options) : base(db, p, options) { mSpecificHeatCapacity = p.mSpecificHeatCapacity; mBoilingPoint = p.mBoilingPoint; mFreezingPoint = p.mFreezingPoint; mThermalConductivity = p.mThermalConductivity; }
	}
	[Serializable]
	public partial class IfcThirdOrderPolynomialSpiral : IfcSpiral
	{
		private double mQubicTerm = 0; //: IfcLengthMeasure;
		private double mQuadraticTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mLinearTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mConstantTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;

		public double QubicTerm { get { return mQubicTerm; } set { mQubicTerm = value; } }
		public double QuadraticTerm { get { return mQuadraticTerm; } set { mQuadraticTerm = value; } }
		public double LinearTerm { get { return mLinearTerm; } set { mLinearTerm = value; } }
		public double ConstantTerm { get { return mConstantTerm; } set { mConstantTerm = value; } }

		public IfcThirdOrderPolynomialSpiral() : base() { }
		internal IfcThirdOrderPolynomialSpiral(DatabaseIfc db, IfcThirdOrderPolynomialSpiral spiral, DuplicateOptions options)
			: base(db, spiral, options) { QubicTerm = spiral.QubicTerm; QuadraticTerm = spiral.QuadraticTerm; LinearTerm = spiral.LinearTerm; ConstantTerm = spiral.ConstantTerm; }
		public IfcThirdOrderPolynomialSpiral(IfcAxis2Placement position, double cubicTerm)
			: base(position) { QubicTerm = cubicTerm; }
	}
	public interface IfcTimeOrRatioSelect { } // IFC4 	IfcRatioMeasure, IfcDuration	
	[Serializable]
	public partial class IfcTimePeriod : BaseClassIfc // IFC4
	{
		internal DateTime mStart; //:	IfcTime;
		internal DateTime mFinish; //:	IfcTime;
		internal IfcTimePeriod() : base() { }
		internal IfcTimePeriod(IfcTimePeriod m) : base() { mStart = m.mStart; mFinish = m.mFinish; }
		public IfcTimePeriod(DatabaseIfc db, DateTime start, DateTime finish) : base(db) { mStart = start; mFinish = finish; }
	}
	[Serializable]
	public abstract partial class IfcTimeSeries : BaseClassIfc, IfcMetricValueSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect, NamedObjectIfc
	{ // ABSTRACT SUPERTYPE OF (ONEOF(IfcIrregularTimeSeries,IfcRegularTimeSeries));
		internal string mName = "";// : IfcLabel;		
		internal string mDescription = "";// : OPTIONAL IfcText;
		internal IfcDateTimeSelect mStartTime;// : IfcDateTimeSelect;
		internal IfcDateTimeSelect mEndTime;// : IfcDateTimeSelect;
		internal IfcTimeSeriesDataTypeEnum mTimeSeriesDataType = IfcTimeSeriesDataTypeEnum.NOTDEFINED;// : IfcTimeSeriesDataTypeEnum;
		internal IfcDataOriginEnum mDataOrigin = IfcDataOriginEnum.NOTDEFINED;// : IfcDataOriginEnum;
		internal string mUserDefinedDataOrigin = "";// : OPTIONAL IfcLabel;
		internal IfcUnit mUnit;// : OPTIONAL IfcUnit; 
		//INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal SET<IfcResourceConstraintRelationship> mHasConstraintRelationships = new SET<IfcResourceConstraintRelationship>(); //gg

		public string Name { get { return mName; } set { mName = value; } }
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } }
		public SET<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		protected IfcTimeSeries() : base() { }
		protected IfcTimeSeries(DatabaseIfc db, IfcTimeSeries s) : base(db,s)
		{
			mName = s.mName;
			mDescription = s.mDescription;
			mStartTime = db.Factory.Duplicate<IfcDateTimeSelect>(s.mStartTime);
			mEndTime = db.Factory.Duplicate<IfcDateTimeSelect>(s.mEndTime);
			mTimeSeriesDataType = s.mTimeSeriesDataType;
			mDataOrigin = s.mDataOrigin;
			mUserDefinedDataOrigin = s.mUserDefinedDataOrigin;
			mUnit = db.Factory.Duplicate<IfcUnit>(s.mUnit);
		}
		protected IfcTimeSeries(DatabaseIfc db) : base(db) { }
		protected IfcTimeSeries(string name, IfcDateTimeSelect startTime, IfcDateTimeSelect endTime, IfcTimeSeriesDataTypeEnum timeSeriesDataType, IfcDataOriginEnum dataOrigin)
			: base(startTime.Database)
		{
			Name = name;
			mStartTime = startTime;
			mEndTime = endTime;
			mTimeSeriesDataType = timeSeriesDataType;
			mDataOrigin = dataOrigin;
		}
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged;
		}
		private void mHasExternalReference_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.NewItems)
				{
					if (!r.RelatedResourceObjects.Contains(this))
						r.RelatedResourceObjects.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.OldItems)
					r.RelatedResourceObjects.Remove(this);
			}
		}
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcTimeSeriesReferenceRelationship; // DEPRECATED IFC4
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcTimeSeriesSchedule // DEPRECATED IFC4
	[Serializable]
	public partial class IfcTimeSeriesValue : BaseClassIfc
	{
		private LIST<IfcValue> mListValues = new LIST<IfcValue>(); //: LIST[1:?] OF IfcValue;

		public LIST<IfcValue> ListValues { get { return mListValues; } set { mListValues = value; } }

		public IfcTimeSeriesValue() : base() { }
		public IfcTimeSeriesValue(DatabaseIfc db, IEnumerable<IfcValue> listValues)
			: base(db)
		{
			ListValues.AddRange(listValues);
		}
	}
	[Serializable]
	public abstract partial class IfcTopologicalRepresentationItem : IfcRepresentationItem  /*(IfcConnectedFaceSet,IfcEdge,IfcFace,IfcFaceBound,IfcLoop,IfcPath,IfcVertex))*/
	{
		protected IfcTopologicalRepresentationItem() : base() { }
		protected IfcTopologicalRepresentationItem(DatabaseIfc db) : base(db) { }
		protected IfcTopologicalRepresentationItem(DatabaseIfc db, IfcTopologicalRepresentationItem i, DuplicateOptions options) : base(db, i, options) { }
	}
	[Serializable]
	public partial class IfcTopologyRepresentation : IfcShapeModel
	{
		internal IfcTopologyRepresentation() : base() { }
		internal IfcTopologyRepresentation(DatabaseIfc db, IfcTopologyRepresentation r, DuplicateOptions options) : base(db, r, options) { }
		public IfcTopologyRepresentation(IfcGeometricRepresentationContext context, IfcConnectedFaceSet fs) : base(context, fs) { RepresentationType = "FaceSet"; }
		public IfcTopologyRepresentation(IfcGeometricRepresentationContext context, IfcEdge e) : base(context, e) { RepresentationType = "Edge"; }
		public IfcTopologyRepresentation(IfcGeometricRepresentationContext context, IfcFace fs) : base(context, fs) { RepresentationType = "Face"; }
		public IfcTopologyRepresentation(IfcGeometricRepresentationContext context, IfcVertex v) : base(context, v) { RepresentationType = "Vertex"; }
		internal static IfcTopologyRepresentation getRepresentation(IfcGeometricRepresentationContext context, IfcTopologicalRepresentationItem item)
		{
			IfcConnectedFaceSet cfs = item as IfcConnectedFaceSet;
			if (cfs != null)
				return new IfcTopologyRepresentation(context, cfs);
			IfcEdge e = item as IfcEdge;
			if (e != null)
				return new IfcTopologyRepresentation(context, e);
			IfcFace f = item as IfcFace;
			if (f != null)
				return new IfcTopologyRepresentation(context, f);
			IfcVertex v = item as IfcVertex;
			if (v != null)
				return new IfcTopologyRepresentation(context, v);
			return null;
		}
	}
	[Serializable]
	public partial class IfcToroidalSurface : IfcElementarySurface //IFC4.2
	{
		internal double mMajorRadius;// : IfcPositiveLengthMeasure; 
		internal double mMinorRadius;// : IfcPositiveLengthMeasure; 
		public double MajorRadius { get { return mMajorRadius; } set { mMajorRadius = value; } }
		public double MinorRadius { get { return mMinorRadius; } set { mMinorRadius = value; } }
		internal IfcToroidalSurface() : base() { }
		internal IfcToroidalSurface(DatabaseIfc db, IfcToroidalSurface s, DuplicateOptions options) : base(db, s, options) { mMajorRadius = s.mMajorRadius; mMinorRadius = s.mMinorRadius; }
		public IfcToroidalSurface(IfcAxis2Placement3D placement, double majorRadius, double minorRadius) : base(placement) { MajorRadius = majorRadius; MinorRadius = minorRadius; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcTrackElement : IfcBuiltElement
	{
		private IfcTrackElementTypeEnum mPredefinedType = IfcTrackElementTypeEnum.NOTDEFINED; //: OPTIONAL IfcTrackElementTypeEnum;
		public IfcTrackElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTrackElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcTrackElement() : base() { }
		public IfcTrackElement(DatabaseIfc db) : base(db) { }
		public IfcTrackElement(DatabaseIfc db, IfcTrackElement trackElement, DuplicateOptions options) : base(db, trackElement, options) { PredefinedType = trackElement.PredefinedType; }
		public IfcTrackElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcTrackElementType : IfcBuiltElementType
	{
		private IfcTrackElementTypeEnum mPredefinedType = IfcTrackElementTypeEnum.NOTDEFINED; //: IfcTrackElementTypeEnum;
		public IfcTrackElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTrackElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcTrackElementType() : base() { }
		public IfcTrackElementType(DatabaseIfc db, IfcTrackElementType trackElementType, DuplicateOptions options) : base(db, trackElementType, options) { PredefinedType = trackElementType.PredefinedType; }
		public IfcTrackElementType(DatabaseIfc db, string name, IfcTrackElementTypeEnum predefinedType)
			: base(db, name) { PredefinedType = predefinedType; }
	}
	[Serializable]
	public partial class IfcTransformer : IfcEnergyConversionDevice //IFC4
	{
		private IfcTransformerTypeEnum mPredefinedType = IfcTransformerTypeEnum.NOTDEFINED;// OPTIONAL : IfcTransformerTypeEnum;
		public IfcTransformerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTransformerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcTransformer() : base() { }
		internal IfcTransformer(DatabaseIfc db, IfcTransformer t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcTransformer(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcTransformerType : IfcEnergyConversionDeviceType
	{
		private IfcTransformerTypeEnum mPredefinedType = IfcTransformerTypeEnum.NOTDEFINED;// : IfcTransformerEnum; 
		public IfcTransformerTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTransformerTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcTransformerType() : base() { }
		internal IfcTransformerType(DatabaseIfc db, IfcTransformerType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcTransformerType(DatabaseIfc db, string name, IfcTransformerTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Obsolete("DEPRECATED IFC4X3", false)]
	[Serializable]
	public partial class IfcTransitionCurveSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		private double mStartRadius = double.PositiveInfinity;// OPTIONAL IfcPositiveLengthMeasure;
		private double mEndRadius = double.PositiveInfinity;// OPTIONAL IfcPositiveLengthMeasure;
		private bool mIsStartRadiusCCW;// : IfcBoolean;
		private bool mIsEndRadiusCCW;// : IfcBoolean;
		private IfcTransitionCurveType mTransitionCurveType = IfcTransitionCurveType.CLOTHOIDCURVE;

		public double StartRadius { get { return double.IsNaN(mStartRadius) ? double.PositiveInfinity : mStartRadius; } set { mStartRadius = ((double.IsInfinity(value) || value < 1e-8) ? double.NaN : value); } }
		public double EndRadius { get { return double.IsNaN(mEndRadius) ? double.PositiveInfinity : mEndRadius; } set { mEndRadius = ((double.IsInfinity(value) || value < 1e-8) ? double.NaN : value); } }
		public bool IsStartRadiusCCW { get { return mIsStartRadiusCCW; } set { mIsStartRadiusCCW = value; } }
		public bool IsEndRadiusCCW { get { return mIsEndRadiusCCW; } set { mIsEndRadiusCCW = value; } }
		public IfcTransitionCurveType TransitionCurveType { get { return mTransitionCurveType; } set { mTransitionCurveType = value; } }

		internal IfcTransitionCurveSegment2D() : base() { }
		public IfcTransitionCurveSegment2D(IfcCartesianPoint start, double startDirection, double length, double startRadius, double endRadius, bool isStartCCW, bool isEndCCW, IfcTransitionCurveType curveType)
			: base(start, startDirection, length)
		{
			mStartRadius = startRadius;
			mEndRadius = endRadius;
			mIsStartRadiusCCW = isStartCCW;
			mIsEndRadiusCCW = isEndCCW;
			mTransitionCurveType = curveType;
		}
		internal IfcTransitionCurveSegment2D(DatabaseIfc db, IfcTransitionCurveSegment2D s, DuplicateOptions options) : base(db, s, options) { mStartRadius = s.mStartRadius; mEndRadius = s.mEndRadius; mIsStartRadiusCCW = s.mIsStartRadiusCCW; mIsEndRadiusCCW = s.mIsEndRadiusCCW; mTransitionCurveType = s.mTransitionCurveType; }
	}
	[Serializable]
	public partial class IfcTranslationalStiffnessSelect
	{
		internal bool mRigid = false;
		internal IfcLinearStiffnessMeasure mStiffness = null;

		public bool Rigid { get { return mRigid; } }
		public IfcLinearStiffnessMeasure Stiffness { get { return mStiffness; } }

		public IfcTranslationalStiffnessSelect(bool fix) { mRigid = fix; }
		public IfcTranslationalStiffnessSelect(double stiff) { mStiffness = new IfcLinearStiffnessMeasure(stiff); }
		public IfcTranslationalStiffnessSelect(IfcLinearStiffnessMeasure stiff) { mStiffness = stiff; }
	}
	public partial class IfcTransportationDevice : IfcElement //ABSTRACT SUPERTYPE OF (ONEOF (IfcTransportElement , IfcVehicle))
	{
		internal IfcTransportationDevice() : base() { }
		internal IfcTransportationDevice(DatabaseIfc db) : base(db) { }
		protected IfcTransportationDevice(DatabaseIfc db, IfcTransportationDevice e, DuplicateOptions options) : base(db, e, options) { }
		public IfcTransportationDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcTransportationDeviceType : IfcElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcTransportElementType , IfcVehicleType))
	{
		internal IfcTransportationDeviceType() : base() { }
		public IfcTransportationDeviceType(DatabaseIfc db) : base(db) { }
		protected IfcTransportationDeviceType(DatabaseIfc db, IfcTransportationDeviceType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcTransportElement : IfcTransportationDevice
	{
		private IfcTransportElementTypeEnum mPredefinedType = IfcTransportElementTypeEnum.NOTDEFINED;// : 	OPTIONAL IfcTransportElementTypeEnum;
		internal double mCapacityByWeight = double.NaN;// : 	OPTIONAL IfcMassMeasure;
		internal double mCapacityByNumber = double.NaN;//	 : 	OPTIONAL IfcCountMeasure;

		public IfcTransportElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTransportElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		//public double CapacityByWeight { get { return mCapacityByWeight; } set { mCapacityByWeight = value; } }
		//public double CapacityByNumber { get { return CapacityByNumber; } set { mCapacityByNumber = value; } }

		internal IfcTransportElement() : base() { }
		internal IfcTransportElement(DatabaseIfc db, IfcTransportElement e, DuplicateOptions options) : base(db, e, options) { }
		public IfcTransportElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcTransportElementType : IfcTransportationDeviceType
	{
		private IfcTransportElementTypeEnum mPredefinedType = IfcTransportElementTypeEnum.NOTDEFINED;// IfcTransportElementTypeEnum; 
		public IfcTransportElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTransportElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcTransportElementType() : base() { }
		internal IfcTransportElementType(DatabaseIfc db, IfcTransportElementType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcTransportElementType(DatabaseIfc db, string name, IfcTransportElementTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcTrapeziumProfileDef : IfcParameterizedProfileDef
	{
		internal double mBottomXDim;// : IfcPositiveLengthMeasure;
		internal double mTopXDim;// : IfcPositiveLengthMeasure;
		internal double mYDim;// : IfcPositiveLengthMeasure;
		internal double mTopXOffset;// : IfcPositiveLengthMeasure; 

		public double BottomXDim { get { return mBottomXDim; } set { mBottomXDim = value; } }
		public double TopXDim { get { return mTopXDim; } set { mTopXDim = value; } }
		public double YDim { get { return mYDim; } set { mYDim = value; } }
		public double TopXOffset { get { return mTopXOffset; } set { mTopXOffset = value; } }

		internal IfcTrapeziumProfileDef() : base() { }
		internal IfcTrapeziumProfileDef(DatabaseIfc db, IfcTrapeziumProfileDef p, DuplicateOptions options) : base(db, p, options) { mBottomXDim = p.mBottomXDim; mTopXDim = p.mTopXDim; mYDim = p.mYDim; mTopXOffset = p.mTopXOffset; }
		public IfcTrapeziumProfileDef(DatabaseIfc db, string name, double bottomXDim, double topXDim, double yDim, double topXOffset) : base(db, name)
		{
			mBottomXDim = bottomXDim;
			mTopXDim = topXDim;
			mYDim = yDim;
			mTopXOffset = topXOffset;
		}
	}
	[Serializable]
	public partial class IfcTriangulatedFaceSet : IfcTessellatedFaceSet
	{
		internal List<Tuple<double, double, double>> mNormals = new List<Tuple<double,double,double>>();// : OPTIONAL LIST [1:?] OF LIST [3:3] OF IfcParameterValue; 
		internal List<Tuple<int, int, int>> mCoordIndex = new List<Tuple<int, int, int>>();// : 	LIST [1:?] OF LIST [3:3] OF INTEGER;
		internal List<Tuple<int, int, int>> mNormalIndex = new List<Tuple<int, int, int>>();// :	OPTIONAL LIST [1:?] OF LIST [3:3] OF INTEGER;  
		internal List<int> mPnIndex = new List<int>(); // : OPTIONAL LIST [1:?] OF IfcPositiveInteger;

		public List<Tuple<double, double, double>> Normals { get { return mNormals; } }
		public List<Tuple<int, int, int>> CoordIndex { get { return mCoordIndex; } }
		public List<Tuple<int, int, int>> NormalIndex { get { return mNormalIndex; } }
		public List<int> PnIndex { get { return mPnIndex; } }

		internal IfcTriangulatedFaceSet() : base() { }
		internal IfcTriangulatedFaceSet(DatabaseIfc db, IfcTriangulatedFaceSet s, DuplicateOptions options) : base(db, s, options)
		{
			if (s.mNormals.Count > 0)
				mNormals.AddRange(s.mNormals);
			mClosed = s.mClosed;
			mCoordIndex.AddRange(s.mCoordIndex);
			if (s.mNormalIndex.Count > 0)
				mNormalIndex.AddRange(s.mNormalIndex);
		}
		public IfcTriangulatedFaceSet(IfcCartesianPointList pointList, IEnumerable<Tuple<int, int, int>> coords)
			: base(pointList) { CoordIndex.AddRange(coords); }
	}
	[Serializable]
	public partial class IfcTriangulatedIrregularNetwork : IfcTriangulatedFaceSet
	{
		public override string StepClassName { get { return (mDatabase != null && mDatabase.mRelease < ReleaseVersion.IFC4X1) ? "IfcTriangulatedFaceSet" : base.StepClassName; } }

		internal List<int> mFlags = new List<int>(); // : LIST [1:?] OF IfcInteger;
		public List<int> Flags { get { return mFlags; } }

		internal IfcTriangulatedIrregularNetwork() : base() { }
		internal IfcTriangulatedIrregularNetwork(DatabaseIfc db, IfcTriangulatedIrregularNetwork s, DuplicateOptions options) : base(db, s, options)
		{
			mFlags.AddRange(s.mFlags);
		}
		public IfcTriangulatedIrregularNetwork(IfcCartesianPointList3D pl, IEnumerable<Tuple<int, int, int>> coords, List<int> flags)
			: base(pl, coords) { mFlags.AddRange(flags); }
	}
	[Serializable]
	public partial class IfcTrimmedCurve : IfcBoundedCurve
	{
		private IfcCurve mBasisCurve;//: IfcCurve;
		internal IfcTrimmingSelect mTrim1;// : SET [1:2] OF IfcTrimmingSelect;
		internal IfcTrimmingSelect mTrim2;//: SET [1:2] OF IfcTrimmingSelect;
		private bool mSenseAgreement = false;// : BOOLEAN;
		internal IfcTrimmingPreference mMasterRepresentation = IfcTrimmingPreference.UNSPECIFIED;// : IfcTrimmingPreference; 

		public IfcCurve BasisCurve { get { return mBasisCurve; } set { mBasisCurve = value; } }
		public IfcTrimmingSelect Trim1 { get { return mTrim1; } set { mTrim1 = value; } }
		public IfcTrimmingSelect Trim2 { get { return mTrim2; } set { mTrim2 = value; } }
		public bool SenseAgreement { get { return mSenseAgreement; } set { mSenseAgreement = value; } }
		public IfcTrimmingPreference MasterRepresentation { get { return mMasterRepresentation; } set { mMasterRepresentation = value; } }

		internal IfcTrimmedCurve() : base() { }
		internal IfcTrimmedCurve(DatabaseIfc db) : base(db) { }
		internal IfcTrimmedCurve(DatabaseIfc db, IfcTrimmedCurve c, DuplicateOptions options) : base(db, c, options)
		{
			BasisCurve = db.Factory.Duplicate(c.BasisCurve, options);
			mTrim1 = new IfcTrimmingSelect(c.mTrim1.ParameterValue);
			mTrim2 = new IfcTrimmingSelect(c.mTrim2.ParameterValue);
			if (c.mTrim1.CartesianPoint != null)
				mTrim1.CartesianPoint = db.Factory.Duplicate(c.mTrim1.CartesianPoint, options);
			if (c.mTrim2.CartesianPoint != null)
				mTrim2.CartesianPoint = db.Factory.Duplicate(c.mTrim2.CartesianPoint, options);
			mSenseAgreement = c.mSenseAgreement;
			mMasterRepresentation = c.mMasterRepresentation;
		}
		public IfcTrimmedCurve(IfcConic basis, IfcTrimmingSelect start, IfcTrimmingSelect end, bool senseAgreement, IfcTrimmingPreference tp) 
			: this(basis.mDatabase, start,end, senseAgreement,tp) { BasisCurve = basis; }
		public IfcTrimmedCurve(IfcLine basis, IfcTrimmingSelect start, IfcTrimmingSelect end, bool senseAgreement, IfcTrimmingPreference tp)
			: this(basis.mDatabase, start, end, senseAgreement, tp) { BasisCurve = basis; }
		public IfcTrimmedCurve(IfcPolynomialCurve basis, IfcTrimmingSelect start, IfcTrimmingSelect end, bool senseAgreement, IfcTrimmingPreference tp)
			: this(basis.Database, start, end, senseAgreement, tp) { BasisCurve = basis; }
		public IfcTrimmedCurve(IfcClothoid basis, IfcTrimmingSelect start, IfcTrimmingSelect end, bool senseAgreement, IfcTrimmingPreference tp)
			: this(basis.Database, start, end, senseAgreement, tp) { BasisCurve = basis; }
		private IfcTrimmedCurve(DatabaseIfc db, IfcTrimmingSelect start, IfcTrimmingSelect end, bool senseAgreement, IfcTrimmingPreference tp) : base(db)
		{
			mTrim1 = start;
			mTrim2 = end;
			mSenseAgreement = senseAgreement;
			mMasterRepresentation = tp;
		}
		internal IfcTrimmedCurve(IfcCartesianPoint start, Tuple<double, double> arcInteriorPoint, IfcCartesianPoint end) : base(start.mDatabase)
		{
			LIST<double> pt1 = start.Coordinates, pt3 = end.Coordinates;
			DatabaseIfc db = start.mDatabase;
			double xDelta_a = arcInteriorPoint.Item1 - pt1[0];
			double yDelta_a = arcInteriorPoint.Item2 - pt1[1];
			double xDelta_b = pt3[0] - arcInteriorPoint.Item1;
			double yDelta_b = pt3[1] - arcInteriorPoint.Item2;
			double x = 0, y = 0;
			double tol = 1e-9;
			if (Math.Abs(xDelta_a) < tol && Math.Abs(yDelta_b) < tol)
			{
				x = (arcInteriorPoint.Item1 + pt3[0]) / 2;
				y = (pt1[1] + arcInteriorPoint.Item2) / 2;
			}
			else
			{
				double aSlope = yDelta_a / xDelta_a; // 
				double bSlope = yDelta_b / xDelta_b;
				if (Math.Abs(aSlope - bSlope) < tol)
				{   // points are colinear
					// line curve
					BasisCurve = new IfcPolyline(start, end);
					mTrim1 = new IfcTrimmingSelect(0);
					mTrim2 = new IfcTrimmingSelect(1);
					MasterRepresentation = IfcTrimmingPreference.PARAMETER;
					return;
				}

				// calc center
				x = (aSlope * bSlope * (pt1[1] - pt3[1]) + bSlope * (pt1[0] + arcInteriorPoint.Item1)
					- aSlope * (arcInteriorPoint.Item1 + pt3[0])) / (2 * (bSlope - aSlope));
				y = -1 * (x - (pt1[0] + arcInteriorPoint.Item1) / 2) / aSlope + (pt1[1] + arcInteriorPoint.Item2) / 2;
			}

			double radius = Math.Sqrt(Math.Pow(pt1[0] - x, 2) + Math.Pow(pt1[1] - y, 2));
			BasisCurve = new IfcCircle(new IfcAxis2Placement2D(new IfcCartesianPoint(db, x, y)) { RefDirection = new IfcDirection(db, pt1[0]-x, pt1[1]-y) }, radius);
			mTrim1 = new IfcTrimmingSelect(0, start);
			mSenseAgreement = (((arcInteriorPoint.Item1 - pt1[0]) * (pt3[1] - arcInteriorPoint.Item2)) - ((arcInteriorPoint.Item2 - pt1[1]) * (pt3[0] - arcInteriorPoint.Item1))) > 0;
			double t3 = Math.Atan2(pt3[1] - y, pt3[0] - x), t1 = Math.Atan2(pt1[1] - y, pt1[0] - x);
			if (t3 < 0)
				t3 = 2 * Math.PI + t3;
			mTrim2 = new IfcTrimmingSelect((t3 - t1 ) / db.mContext.UnitsInContext.ScaleSI(IfcUnitEnum.PLANEANGLEUNIT), end );
			mMasterRepresentation = IfcTrimmingPreference.PARAMETER;
		}	
	}
	[Serializable]
	public partial class IfcTrimmingSelect
	{
		public IfcTrimmingSelect() { }
		public IfcTrimmingSelect(IfcCartesianPoint cp)
		{
			mParameterValue = double.NaN;
			mCartesianPoint = cp;
		}
		public IfcTrimmingSelect(double param) { mParameterValue = param; }
		public IfcTrimmingSelect(double param, IfcCartesianPoint cp) : this(cp) { mParameterValue = param; }
		
		internal double mParameterValue = double.NaN;
		public double ParameterValue { get { return mParameterValue; } set { mParameterValue = value; } }
		private IfcCartesianPoint mCartesianPoint = null;
		public IfcCartesianPoint CartesianPoint { get { return mCartesianPoint; } set { mCartesianPoint = value; } }
	}
	[Serializable]
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
		internal IfcTShapeProfileDef(DatabaseIfc db, IfcTShapeProfileDef p, DuplicateOptions options) : base(db, p, options)
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
	}
	[Serializable]
	public partial class IfcTubeBundle : IfcEnergyConversionDevice //IFC4
	{
		private IfcTubeBundleTypeEnum mPredefinedType = IfcTubeBundleTypeEnum.NOTDEFINED;// OPTIONAL : IfcTubeBundleTypeEnum;
		public IfcTubeBundleTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTubeBundleTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcTubeBundle() : base() { }
		internal IfcTubeBundle(DatabaseIfc db, IfcTubeBundle b, DuplicateOptions options) : base(db, b, options) { PredefinedType = b.PredefinedType; }
		public IfcTubeBundle(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcTubeBundleType : IfcEnergyConversionDeviceType
	{
		private IfcTubeBundleTypeEnum mPredefinedType = IfcTubeBundleTypeEnum.NOTDEFINED;// : IfcTubeBundleEnum; 
		public IfcTubeBundleTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcTubeBundleTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcTubeBundleType() : base() { }
		internal IfcTubeBundleType(DatabaseIfc db, IfcTubeBundleType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcTubeBundleType(DatabaseIfc db, string name, IfcTubeBundleTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcTunnel : IfcFacility
	{
		private IfcTunnelTypeEnum mPredefinedType = IfcTunnelTypeEnum.NOTDEFINED; //: IfcTunnelTypeEnum;
		public IfcTunnelTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcTunnelTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X4_DRAFT : mDatabase.Release); } }

		public IfcTunnel() : base() { }
		public IfcTunnel(DatabaseIfc db) : base(db) { }
		public IfcTunnel(DatabaseIfc db, IfcTunnel tunnel, DuplicateOptions options) : base(db, tunnel, options) { PredefinedType = tunnel.PredefinedType; }
		public IfcTunnel(DatabaseIfc db, string name) : base(db, name) { }
		public IfcTunnel(IfcFacility host, string name, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { Name = name; }
		internal IfcTunnel(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcTunnelPart : IfcFacilityPart
	{
		private IfcTunnelPartTypeEnum mPredefinedType = IfcTunnelPartTypeEnum.NOTDEFINED; //: IfcTunnelTypeEnum;
		public IfcTunnelPartTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcTunnelPartTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X4_DRAFT : mDatabase.Release); } }
		public IfcTunnelPart() : base() { }
		public IfcTunnelPart(DatabaseIfc db) : base(db) { }
		public IfcTunnelPart(DatabaseIfc db, IfcTunnelPart tunnelPart, DuplicateOptions options) : base(db, tunnelPart, options) { }
		public IfcTunnelPart(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcTwoDirectionRepeatFactor : IfcOneDirectionRepeatFactor // DEPRECATED IFC4
	{
		internal IfcVector mSecondRepeatFactor;//  : IfcVector 
		public IfcVector SecondRepeatFactor { get { return mSecondRepeatFactor; } set { mSecondRepeatFactor = value; } }

		internal IfcTwoDirectionRepeatFactor() : base() { }
		internal IfcTwoDirectionRepeatFactor(DatabaseIfc db, IfcTwoDirectionRepeatFactor f, DuplicateOptions options) : base(db, f, options) { SecondRepeatFactor = db.Factory.Duplicate(f.SecondRepeatFactor) as IfcVector; }
	}
	[Serializable]
	public partial class IfcTypeObject : IfcObjectDefinition //(IfcTypeProcess, IfcTypeProduct, IfcTypeResource) IFC4 ABSTRACT 
	{
		internal string mApplicableOccurrence = "";// : OPTIONAL IfcLabel;
		internal SET<IfcPropertySetDefinition> mHasPropertySets = new SET<IfcPropertySetDefinition>();// : OPTIONAL SET [1:?] OF IfcPropertySetDefinition 
		//INVERSE 
		internal IfcRelDefinesByType mTypes = null;

		public string ApplicableOccurrence { get { return mApplicableOccurrence; } set { mApplicableOccurrence = value; } }
		public SET<IfcPropertySetDefinition> HasPropertySets { get { return mHasPropertySets; } }
		public IfcRelDefinesByType Types { get { return mTypes; } }
		[Obsolete("RENAMED IFC4", false)]
		public IfcRelDefinesByType ObjectTypeOf { get { return mTypes; } }
		//GeomGym
		internal IfcMaterialProfileSet mTapering = null;

		public new string Name { get { return base.Name; } set { base.Name = string.IsNullOrEmpty( value) ? "NameNotAssigned" : value; } }

		protected IfcTypeObject() : base() { Name = "NameNotAssigned"; }
		protected IfcTypeObject(DatabaseIfc db, IfcTypeObject t, DuplicateOptions options) : base(db, t, options) 
		{ 
			mApplicableOccurrence = t.mApplicableOccurrence;
			if (options.DuplicateProperties)
			{
				foreach (IfcPropertySetDefinition propertySetDefinition in t.HasPropertySets)
				{
					if (propertySetDefinition is IfcPropertySet propertySet)
					{
						IfcPropertySet duplicatePset = db.Factory.DuplicatePropertySet(propertySet, options);
						if (duplicatePset != null)
							HasPropertySets.Add(duplicatePset);
					}
					else
					{
						IfcPropertySetDefinition duplicate = db.Factory.Duplicate(propertySetDefinition, options) as IfcPropertySetDefinition;
						if (duplicate != null)
							HasPropertySets.Add(duplicate);
					}
				}
			}
		}
		[Obsolete("DEPRECATED IFC4", false)]
		public IfcTypeObject(DatabaseIfc db) : base(db) { Name = "NameNotAssigned"; IfcRelDefinesByType rdt = new IfcRelDefinesByType(this) { Name = Name }; }

		protected override void initialize()
		{
			base.initialize();

			mHasPropertySets.CollectionChanged += mHasPropertySets_CollectionChanged;
		}
		private void mHasPropertySets_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcPropertySetDefinition s in e.NewItems)
				{
					if (!s.mDefinesType.Contains(this))
						s.mDefinesType.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcPropertySetDefinition s in e.OldItems)
					s.mDefinesType.Remove(this);
			}
		}
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcPropertySetDefinition psd in HasPropertySets)
				result.AddRange(psd.Extract<T>());
			return result;
		}
		public override IfcProperty FindProperty(string name)
		{
			foreach (IfcPropertySet pset in HasPropertySets.OfType<IfcPropertySet>())
			{
				if(pset != null)
				{
					IfcProperty property = pset[name];
					if (property != null)
						return property;
				}
			}
			return null;
		}
		public override void RemoveProperty(IfcProperty property)
		{
			foreach(IfcPropertySet pset in HasPropertySets.OfType<IfcPropertySet>().ToList())
			{
				foreach(IfcProperty p in pset.HasProperties.Values)
				{
					if(p == property)
					{
						if(pset.DefinesType.Count == 1 && pset.DefinesOccurrence == null)
						{
							if (pset.HasProperties.Count == 1)
								pset.Dispose(true);
							else
								property.Dispose(true);
						}
						else
						{
							HasPropertySets.Remove(pset);
							HasPropertySets.Add(new IfcPropertySet(pset.Name, pset.HasProperties.Values.Where(x => x != property)));
						}
						return;
					}
				}
			}
		}
		public override IfcPropertySetDefinition FindPropertySet(string name)
		{
			IfcPropertySetDefinition pset = HasPropertySets.FirstOrDefault(x => string.Compare(x.Name, name, true) == 0);
			if (pset != null)
				return pset;
			return null;
		}
		public IfcProfileProperties ProfileProperties()
		{
			IEnumerable<IfcRelAssociatesProfileProperties> associates = HasAssociations.OfType<IfcRelAssociatesProfileProperties>();
			return associates.Count() <= 0 ? null : associates.First().RelatingProfileProperties;
		}
		public bool MaterialProfile(out IfcMaterial material, out IfcProfileDef profile)
		{
			material = null;
			profile = null;
			foreach (IfcRelAssociates associates in mHasAssociations)
			{
				IfcRelAssociatesMaterial associatesMaterial = associates as IfcRelAssociatesMaterial;
				if (associatesMaterial != null)
				{
					IfcMaterialSelect ms = associatesMaterial.RelatingMaterial;
					IfcMaterialProfile mp = ms as IfcMaterialProfile;
					if (mp == null)
					{
						IfcMaterialProfileSet mps = ms as IfcMaterialProfileSet;
						if (mps != null)
							mp = mps.MaterialProfiles[0];
						else
						{
							IfcMaterialProfileSetUsage mpu = ms as IfcMaterialProfileSetUsage;
							if (mpu != null)
								mp = mpu.ForProfileSet.MaterialProfiles[0];
						}
					}
					if (mp != null)
					{
						material = mp.Material;
						profile = mp.Profile;
						return true;
					}
					IfcMaterial m = ms as IfcMaterial;
					if (m != null)
						material = m;
				}
				IfcRelAssociatesProfileProperties associatesProfileProperties = associates as IfcRelAssociatesProfileProperties;	
				if (associatesProfileProperties != null)
				{
					IfcProfileProperties profileProperties = associatesProfileProperties.RelatingProfileProperties;
					if (profileProperties != null)
						profile = profileProperties.ProfileDefinition;
				}
			}
			if (profile != null)
				return true;
				IfcRelDefinesByType rbt = Types;
			if (rbt != null)
			{
				List<IfcElement> relatedElements = rbt.RelatedObjects.OfType<IfcElement>().ToList();
				if (relatedElements.Count > 0)
				{
					relatedElements[0].instanceMaterialProfile(out material, out profile);
					if (profile == null)
						return false;

					IfcMaterial mat = null;
					IfcProfileDef prof = null;
					double tol = 1e-5;
					foreach (IfcElement el in relatedElements.Skip(1))
					{
						el.instanceMaterialProfile(out mat, out prof);
						if (prof == null || !prof.isDuplicate(profile, tol))
							return false;
						if (mat == null)
							material = null;
						else if (material != null && !material.isDuplicate(mat, tol))
							material = null;
					}
				}
			}
			return profile != null;
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

				SET<IfcPropertySetDefinition> psets = HasPropertySets;
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
					List<IfcPropertySetDefinition> list = psets.ToList();
					foreach (int i in r.mListPositions)
						result.AddRange(list[i - 1].retrieveReference(ir));
				}
				return result;
			}
			return base.retrieveReference(r);
		}
		internal void IsolateObject(string filename, bool relatedObjects)
		{
			DatabaseIfc db = new DatabaseIfc(mDatabase);
			IfcTypeObject typeObject = db.Factory.Duplicate(this, new DuplicateOptions(db.Tolerance) { DuplicateDownstream = true }) as IfcTypeObject;
			if (relatedObjects)
			{
				if (mTypes != null)
				{
					foreach (IfcObject o in mTypes.RelatedObjects)
						db.Factory.Duplicate(o);
				}
				if (HasContext != null)
					(db.Factory.Duplicate(mDatabase.Context, new DuplicateOptions(db.Tolerance) { DuplicateDownstream = false }) as IfcContext).AddDeclared(typeObject);
			}
			else
			{
				if (HasContext != null)
				{
					IfcContext context = mDatabase.Context;
					if (db.Release > ReleaseVersion.IFC2x3)
					{
						IfcProjectLibrary library = new IfcProjectLibrary(db, context.Name) { LongName = context.LongName, GlobalId = context.GlobalId, UnitsInContext = db.Factory.Duplicate(context.UnitsInContext, new DuplicateOptions(db.Tolerance) { DuplicateDownstream = true }) as IfcUnitAssignment };
						library.AddDeclared(typeObject);
					}
					else
						(db.Factory.Duplicate(mDatabase.Context, new DuplicateOptions(db.Tolerance) { DuplicateDownstream = false }) as IfcContext).AddDeclared(typeObject);
				}
			}
			IfcProject project = db.Project;
			if (project != null)
			{
				IfcSite site = db.Project.RootElement() as IfcSite;
				if (site != null)
				{
					IfcProductDefinitionShape pr = site.Representation;
					if (pr != null)
					{
						site.Representation = null;
						pr.Dispose(true);
					}
				}
			}
		
			db.WriteFile(filename);
		}
	}
	[Serializable]
	public abstract partial class IfcTypeProcess : IfcTypeObject, IfcProcessSelect  //ABSTRACT SUPERTYPE OF(ONEOF(IfcEventType, IfcProcedureType, IfcTaskType))
	{
		private string mIdentification = "";// :	OPTIONAL IfcIdentifier;
		private string mLongDescription = "";//	 :	OPTIONAL IfcText;
		private string mProcessType = "";//	 :	OPTIONAL IfcLabel;
		//INVERSE
		internal SET<IfcRelAssignsToProcess> mOperatesOn = new SET<IfcRelAssignsToProcess>();// : SET [0:?] OF IfcRelAssignsToProcess FOR RelatingProcess;

		public string Identification { get { return mIdentification; } set { mIdentification = value; } }
		public string LongDescription { get { return mLongDescription; } set { mLongDescription = value; } }
		public string ProcessType { get { return mProcessType; } set { mProcessType = value; } }
		//INVERSE
		public SET<IfcRelAssignsToProcess> OperatesOn { get { return mOperatesOn; } }

		protected IfcTypeProcess() : base() { }
		protected IfcTypeProcess(DatabaseIfc db, IfcTypeProcess t, DuplicateOptions options) : base(db, t, options) { mIdentification = t.mIdentification; mLongDescription = t.mLongDescription; mProcessType = t.mProcessType; }
		protected IfcTypeProcess(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcTypeProduct : IfcTypeObject, IfcProductSelect //SUPERTYPE OF (ONEOF (IfcDoorStyle ,IfcElementType ,IfcSpatialElementType ,IfcWindowStyle)) 
	{ 
		internal LIST<IfcRepresentationMap> mRepresentationMaps = new LIST<IfcRepresentationMap>();// : OPTIONAL LIST [1:?] OF UNIQUE IfcRepresentationMap;
		private string mTag = "";// : OPTIONAL IfcLabel 
		//INVERSE
		internal SET<IfcRelAssignsToProduct> mReferencedBy = new SET<IfcRelAssignsToProduct>();//	 :	SET OF IfcRelAssignsToProduct FOR RelatingProduct;
		
		public LIST<IfcRepresentationMap> RepresentationMaps { get { return mRepresentationMaps; } set { mRepresentationMaps.Clear(); if (value != null) { mRepresentationMaps.CollectionChanged -= mRepresentationMaps_CollectionChanged; mRepresentationMaps = value; mRepresentationMaps.CollectionChanged += mRepresentationMaps_CollectionChanged; } } }
		public string Tag { get { return mTag; } set { mTag = value; } }
		public SET<IfcRelAssignsToProduct> ReferencedBy { get { return mReferencedBy; } }

		protected IfcTypeProduct() : base() { }
		protected IfcTypeProduct(DatabaseIfc db, IfcTypeProduct t, DuplicateOptions options) : base(db, t, options)
		{
			if (options.DuplicateRepresentation)
			{
				if(!options.DuplicateAxisRepresentations && t.RepresentationMaps.Count > 0)
				{
					foreach(IfcRepresentationMap representationMap in t.RepresentationMaps)
					{
						if (representationMap.MappedRepresentation is IfcShapeModel shapeModel &&
							string.Compare(shapeModel.RepresentationIdentifier, "Axis", true) == 0)
							continue;
						RepresentationMaps.Add(db.Factory.Duplicate(representationMap, options));
					}

				}
				else
					RepresentationMaps.AddRange(t.RepresentationMaps.Select(x => db.Factory.Duplicate(x, options)));
			}
			mTag = t.mTag;
		}
		public IfcTypeProduct(DatabaseIfc db) : base(db) {  }

		protected override void initialize()
		{
			base.initialize();
			RelatedMaterial();

			mRepresentationMaps.CollectionChanged += mRepresentationMaps_CollectionChanged;
			mReferencedBy.CollectionChanged += mReferencedBy_CollectionChanged;
		}
		private void mRepresentationMaps_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRepresentationMap m in e.NewItems)
				{
					if (!m.mRepresents.Contains(this))
						m.mRepresents.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRepresentationMap m in e.OldItems)
					m.mRepresents.Remove(this);
			}
		}
		private void mReferencedBy_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelAssignsToProduct r in e.NewItems)
				{
					if (r.RelatingProduct != this)
						r.RelatingProduct = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelAssignsToProduct r in e.OldItems)
				{
					if (r.RelatingProduct == this)
						r.RelatingProduct = null;
				}
			}
		}
		
		internal IfcProduct GenerateMappedProduct(IfcProduct host, IfcAxis2Placement3D relativePlacement)
		{
			if (RepresentationMaps.Count != 1)
				return null;
			DatabaseIfc db = host.Database;
			string typename = this.GetType().Name;
			typename = typename.Substring(0, typename.Length - 4);
			IfcShapeRepresentation sr = new IfcShapeRepresentation(new IfcMappedItem(RepresentationMaps[0], db.Factory.XYPlaneTransformation));
			IfcProductDefinitionShape pds = new IfcProductDefinitionShape(sr);
			IfcProduct product = db.Factory.ConstructProduct(typename, host, new IfcLocalPlacement(host.ObjectPlacement, relativePlacement), pds);
			product.setRelatingType(this);
			if (product is IfcElement element)
			{
				foreach (IfcRelNests nests in IsNestedBy)
				{
					foreach (IfcObjectDefinition od in nests.RelatedObjects)
					{
						IfcDistributionPort port = od as IfcDistributionPort;
						if (port != null)
						{
							IfcDistributionPort newPort = new IfcDistributionPort(element) { FlowDirection = port.FlowDirection, PredefinedType = port.PredefinedType, SystemType = port.SystemType };
							newPort.ObjectPlacement = new IfcLocalPlacement(element.ObjectPlacement, (port.ObjectPlacement as IfcLocalPlacement).RelativePlacement);
							foreach (IfcRelDefinesByProperties rdp in port.mIsDefinedBy)
								rdp.RelatedObjects.Add(newPort);
						}
					}
				}
			}
			foreach(IfcPropertySet pset in HasPropertySets.OfType<IfcPropertySet>())
			{
				if (pset.IsInstancePropertySet())
					pset.RelateObjectDefinition(product);
			}
			return product;
		}

		public static IfcTypeProduct ConstructType(DatabaseIfc db, string className, string name)
		{
			string str = className, definedType = "";
			if (!string.IsNullOrEmpty(str))
			{
				string[] fields = str.Split(".".ToCharArray());
				if (fields.Length > 1)
				{
					str = fields[0];
					definedType = fields[1];
				}
			}
			if (!str.ToLower().EndsWith("type"))
				str = str + "type";
			if (db.Release < ReleaseVersion.IFC4X3)
			{
				if (string.Compare(str, "IfcBuiltElementType", true) == 0)
					str = "IfcBuildingElementProxyType";
			}
			IfcTypeProduct result = null;
			Type type = BaseClassIfc.GetType(str);
			if (type != null)
			{
				VersionAddedAttribute versionAdded = type.GetCustomAttribute(typeof(VersionAddedAttribute)) as VersionAddedAttribute;
				if (versionAdded != null && versionAdded.Release > db.Release)
					type = typeof(IfcBuildingElementProxyType);

				Type enumType = Type.GetType("GeometryGym.Ifc." + type.Name + "Enum");
				ConstructorInfo ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new[] { typeof(DatabaseIfc), typeof(string) }, null);
				if (ctor == null)
				{
					ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new[] { typeof(DatabaseIfc), typeof(string), enumType }, null);
					if (ctor == null)
						throw new Exception("XXX Unrecognized Ifc Constructor for " + className);
					else 
					{
						object predefined = Enum.Parse(enumType, "NOTDEFINED");
						result = ctor.Invoke(new object[] { db, name, predefined}) as IfcTypeProduct;
					}
				}
				else
					result = ctor.Invoke(new object[] { db, name }) as IfcTypeProduct;
			
				if (result != null && !string.IsNullOrEmpty(definedType))
				{
					result.SetPredefinedType(definedType);	
				}
			}
			return result;
		}
	}
	[Serializable]
	public abstract partial class IfcTypeResource : IfcTypeObject, IfcResourceSelect //ABSTRACT SUPERTYPE OF(IfcConstructionResourceType)
	{
		internal string mIdentification = "";// :	OPTIONAL IfcIdentifier;
		internal string mLongDescription = "";//	 :	OPTIONAL IfcText;
		internal string mResourceType = "";//	 :	OPTIONAL IfcLabel;
		//INVERSE
		internal SET<IfcRelAssignsToResource> mResourceOf = new SET<IfcRelAssignsToResource>();// : SET [0:?] OF IfcRelAssignsToResource FOR RelatingResource; 

		public string Identification { get { return mIdentification; } set { mIdentification = value; } }
		public string LongDescription { get { return mLongDescription; } set { mLongDescription = value; } }
		public string ResourceType { get { return mResourceType; } set { mResourceType = value; } }
		//INVERSE
		public SET<IfcRelAssignsToResource> ResourceOf { get { return mResourceOf; } } 

		protected IfcTypeResource() : base() { }
		protected IfcTypeResource(DatabaseIfc db, IfcTypeResource t, DuplicateOptions options) : base(db, t, options) { mIdentification = t.mIdentification; mLongDescription = t.mLongDescription; mResourceType = t.mResourceType; }
		protected IfcTypeResource(DatabaseIfc db) : base(db) { }
	}
}
