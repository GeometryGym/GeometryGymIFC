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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public partial class IfcTable : BaseClassIfc, IfcMetricValueSelect, IfcObjectReferenceSelect, NamedObjectIfc
	{
		internal string mName = "$"; //:	OPTIONAL IfcLabel;
		private List<int> mRows = new List<int>();// OPTIONAL LIST [1:?] OF IfcTableRow;
		private List<int> mColumns = new List<int>();// :	OPTIONAL LIST [1:?] OF IfcTableColumn;

		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcTableRow> Rows { get { return new ReadOnlyCollection<IfcTableRow>(mRows.ConvertAll(x => mDatabase[x] as IfcTableRow)); } }
		public ReadOnlyCollection<IfcTableColumn> Columns { get { return new ReadOnlyCollection<IfcTableColumn>(mColumns.ConvertAll(x => mDatabase[x] as IfcTableColumn)); } }

		internal IfcTable() : base() { }
		public IfcTable(DatabaseIfc db) : base(db) { }
		internal IfcTable(DatabaseIfc db, IfcTable t) : base(db) { mName = t.mName; t.Rows.ToList().ForEach(x => addRow(db.Factory.Duplicate(t) as IfcTableRow)); t.Columns.ToList().ForEach(x => addColumn(db.Factory.Duplicate(x) as IfcTableColumn)); }
		public IfcTable(string name, List<IfcTableRow> rows, List<IfcTableColumn> cols) : base(rows == null || rows.Count == 0 ? cols[0].mDatabase : rows[0].mDatabase)
		{
			Name = name.Replace("'", "");
			rows.ForEach(x => addRow(x));
			cols.ForEach(x => addColumn(x));
		}

		internal void addRow(IfcTableRow row) { mRows.Add(row.mIndex); }
		internal void addColumn(IfcTableColumn column) { mColumns.Add(column.mIndex); }
	}
	[Serializable]
	public partial class IfcTableColumn : BaseClassIfc, NamedObjectIfc
	{
		internal string mIdentifier = "$";//	 :	OPTIONAL IfcIdentifier;
		internal string mName = "$";//	 :	OPTIONAL IfcLabel;
		internal string mDescription = "$";//	 :	OPTIONAL IfcText;
		internal int mUnit;//	 :	OPTIONAL IfcUnit;
		private int mReferencePath;//	 :	OPTIONAL IfcReference;

		public string Identifier { get { return (mIdentifier == "$" ? "" : ParserIfc.Decode(mIdentifier)); } set { mIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcUnit Unit { get { return mDatabase[mUnit] as IfcUnit; } set { mUnit = (value == null ? 0 : value.Index); } }
		public IfcReference ReferencePath { get { return mDatabase[mReferencePath] as IfcReference; } set { mReferencePath = (value == null ? 0 : value.mIndex); } }

		internal IfcTableColumn() : base() { }
		public IfcTableColumn(DatabaseIfc db) : base(db) { }
		internal IfcTableColumn(DatabaseIfc db, IfcTableColumn c) : base(db, c) { mIdentifier = c.mIdentifier; mName = c.mName; mDescription = c.mDescription; if (c.mUnit > 0) Unit = db.Factory.Duplicate(c.mDatabase[c.mUnit]) as IfcUnit; if (c.mReferencePath > 0) ReferencePath = db.Factory.Duplicate(c.ReferencePath) as IfcReference; }
	}
	[Serializable]
	public partial class IfcTableRow : BaseClassIfc
	{
		internal List<IfcValue> mRowCells = new List<IfcValue>();// :	OPTIONAL LIST [1:?] OF IfcValue;
		internal bool mIsHeading = false; //:	:	OPTIONAL BOOLEAN;

		public ReadOnlyCollection<IfcValue> RowCells { get { return new ReadOnlyCollection<IfcValue>(mRowCells); } }
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
		internal IfcTankTypeEnum mPredefinedType = IfcTankTypeEnum.NOTDEFINED;// OPTIONAL : IfcTankTypeEnum;
		public IfcTankTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTank() : base() { }
		internal IfcTank(DatabaseIfc db, IfcTank t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcTank(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcTankType : IfcFlowStorageDeviceType
	{
		internal IfcTankTypeEnum mPredefinedType = IfcTankTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum; 
		public IfcTankTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTankType() : base() { }
		internal IfcTankType(DatabaseIfc db, IfcTankType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcTankType(DatabaseIfc db, string name, IfcTankTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcTask : IfcProcess //SUPERTYPE OF (ONEOF(IfcMove,IfcOrderAction) both depreceated IFC4) 
	{
		//internal string mTaskId; //  : 	IfcIdentifier; IFC4 midentification at IfcProcess
		private string mStatus = "";// : OPTIONAL IfcLabel;
		internal string mWorkMethod = "";// : OPTIONAL IfcLabel;
		internal bool mIsMilestone = false;// : BOOLEAN
		internal int mPriority;// : OPTIONAL INTEGER IFC4
		internal int mTaskTime;// : OPTIONAL IfcTaskTime; IFC4
		internal IfcTaskTypeEnum mPredefinedType = IfcTaskTypeEnum.NOTDEFINED;// : OPTIONAL IfcTaskTypeEnum

		public string Status { get { return mStatus; } set { mStatus = value; } }
		public string WorkMethod { get { return mWorkMethod; } set { mWorkMethod = value; } }
		public bool IsMilestone { get { return mIsMilestone; } set { mIsMilestone = value; } }
		public int Priority { get { return mPriority; } set { mPriority = value; } }
		internal IfcTaskTime TaskTime { get { return mDatabase[mTaskTime] as IfcTaskTime; } set { mTaskTime = value == null ? 0 : value.mIndex; } }

		internal IfcTask() : base() { }
		internal IfcTask(DatabaseIfc db, IfcTask t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mStatus = t.mStatus; mWorkMethod = t.mWorkMethod; mIsMilestone = t.mIsMilestone; mPriority = t.mPriority; if (t.mTaskTime > 0) TaskTime = db.Factory.Duplicate(t.TaskTime) as IfcTaskTime; mPredefinedType = t.mPredefinedType; }
		public IfcTask(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcTaskTime : IfcSchedulingTime //IFC4
	{
		internal IfcTaskDurationEnum mDurationType = IfcTaskDurationEnum.NOTDEFINED;    // :	OPTIONAL IfcTaskDurationEnum;
		internal string mScheduleDuration = "$";//	 :	OPTIONAL IfcDuration;
		internal DateTime mScheduleStart = DateTime.MinValue, mScheduleFinish = DateTime.MinValue, mEarlyStart = DateTime.MinValue, mEarlyFinish = DateTime.MinValue, mLateStart = DateTime.MinValue, mLateFinish = DateTime.MinValue; //:	OPTIONAL IfcDateTime;
		internal string mFreeFloat = "$", mTotalFloat = "$";//	 :	OPTIONAL IfcDuration;
		internal bool mIsCritical;//	 :	OPTIONAL BOOLEAN;
		internal DateTime mStatusTime = DateTime.MinValue;//	 :	OPTIONAL IfcDateTime;
		internal string mActualDuration = "$";//	 :	OPTIONAL IfcDuration;
		internal DateTime mActualStart = DateTime.MinValue, mActualFinish = DateTime.MinValue;//	 :	OPTIONAL IfcDateTime;
		internal string mRemainingTime = "$";//	 :	OPTIONAL IfcDuration;
		internal double mCompletion = double.NaN;//	 :	OPTIONAL IfcPositiveRatioMeasure; 

		public IfcTaskDurationEnum DurationType { get { return mDurationType; } set { mDurationType = value; } }
		public IfcDuration ScheduleDuration { get { return IfcDuration.Convert(mScheduleDuration); } set { mScheduleDuration = IfcDuration.Convert(value); } }
		public DateTime ScheduleStart { get { return mScheduleStart; } set { mScheduleStart = value; } }
		public DateTime ScheduleFinish { get { return mScheduleFinish; } set { mScheduleFinish = value; } }
		public DateTime EarlyStart { get { return mEarlyStart; } set { mEarlyStart = value; } }
		public DateTime EarlyFinish { get { return mEarlyFinish; } set { mEarlyFinish = value; } }
		public DateTime LateStart { get { return mLateStart; } set { mLateStart = value; } }
		public DateTime LateFinish { get { return mLateFinish; } set { mLateFinish = value; } }
		public IfcDuration FreeFloat { get { return IfcDuration.Convert(mFreeFloat); } set { mFreeFloat = IfcDuration.Convert(value); } }
		public IfcDuration TotalFloat { get { return IfcDuration.Convert(mTotalFloat); } set { mTotalFloat = IfcDuration.Convert(value); } }
		public bool IsCritical { get { return mIsCritical; } set { mIsCritical = value; } }
		public DateTime StatusTime { get { return mStatusTime; } set { mStatusTime = value; } }
		public IfcDuration ActualDuration { get { return IfcDuration.Convert(mActualDuration); } set { mActualDuration = IfcDuration.Convert(value); } }
		public DateTime ActualStart { get { return mActualStart; } set { mActualStart = value; } }
		public DateTime ActualFinish { get { return mActualFinish; } set { mActualFinish = value; } }
		public IfcDuration RemainingTime { get { return IfcDuration.Convert(mRemainingTime); } set { mRemainingTime = IfcDuration.Convert(value); } }
		public double Completion { get { return mCompletion; } set { mCompletion = value; } }

		internal IfcTaskTime() : base() { }
		internal IfcTaskTime(DatabaseIfc db, IfcTaskTime t) : base(db, t)
		{
			mDurationType = t.mDurationType; mScheduleDuration = t.mScheduleDuration; mScheduleStart = t.mScheduleStart; mScheduleFinish = t.mScheduleFinish;
			mEarlyStart = t.mEarlyStart; mEarlyFinish = t.mEarlyFinish; mLateStart = t.mLateStart; mLateFinish = t.mLateFinish; mFreeFloat = t.mFreeFloat; mTotalFloat = t.mTotalFloat;
			mIsCritical = t.mIsCritical; mStatusTime = t.mStatusTime; mActualDuration = t.mActualDuration; mActualStart = t.mActualStart; mActualFinish = t.mActualFinish;
			mRemainingTime = t.mRemainingTime; mCompletion = t.mCompletion;
		}
		public IfcTaskTime(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcTaskType : IfcTypeProcess //IFC4
	{
		internal IfcTaskTypeEnum mPredefinedType = IfcTaskTypeEnum.NOTDEFINED;// : IfcTaskTypeEnum; 
		private string mWorkMethod = "$";// : OPTIONAL IfcLabel;

		public IfcTaskTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public string WorkMethod { get { return (mWorkMethod == "$" ? "" : ParserIfc.Decode(mWorkMethod)); } set { mWorkMethod = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcTaskType() : base() { }
		internal IfcTaskType(DatabaseIfc db, IfcTaskType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; mWorkMethod = t.mWorkMethod; }
		public IfcTaskType(DatabaseIfc m, string name, IfcTaskTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
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
		internal IfcTendonTypeEnum mPredefinedType = IfcTendonTypeEnum.NOTDEFINED;// : IfcTendonTypeEnum;//
		internal double mNominalDiameter;// : IfcPositiveLengthMeasure;
		internal double mCrossSectionArea;// : IfcAreaMeasure;
		internal double mTensionForce;// : OPTIONAL IfcForceMeasure;
		internal double mPreStress;// : OPTIONAL IfcPressureMeasure;
		internal double mFrictionCoefficient;// //: OPTIONAL IfcNormalisedRatioMeasure;
		internal double mAnchorageSlip;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mMinCurvatureRadius;// : OPTIONAL IfcPositiveLengthMeasure; 
		public IfcTendonTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcTendon() : base() { }
		internal IfcTendon(DatabaseIfc db, IfcTendon t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream)
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
			: base(host, placement, representation)
		{
			mNominalDiameter = diam;
			mCrossSectionArea = area;
			mTensionForce = forceMeasure;
			mPreStress = pretress;
			mFrictionCoefficient = fricCoeff;
			mAnchorageSlip = anchorSlip;
			mMinCurvatureRadius = minCurveRadius;
		}
	}
	[Serializable]
	public partial class IfcTendonAnchor : IfcReinforcingElement
	{
		internal IfcTendonAnchorTypeEnum mPredefinedType = IfcTendonAnchorTypeEnum.NOTDEFINED;// :	OPTIONAL IfcTendonAnchorTypeEnum;
		public IfcTendonAnchorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTendonAnchor() : base() { }
		internal IfcTendonAnchor(DatabaseIfc db, IfcTendonAnchor a, IfcOwnerHistory ownerHistory, bool downStream) : base(db, a, ownerHistory, downStream) { mPredefinedType = a.mPredefinedType; }
		public IfcTendonAnchor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	//IfcTendonAnchorType
	[Serializable]
	public partial class IfcTendonType : IfcReinforcingElementType  //IFC4
	{
		internal IfcTendonTypeEnum mPredefinedType = IfcTendonTypeEnum.NOTDEFINED;// : IfcTendonType; //IFC4
		private double mNominalDiameter;// : IfcPositiveLengthMeasure; 	IFC4 OPTIONAL
		internal double mCrossSectionArea;// : IfcAreaMeasure; IFC4 OPTIONAL
		internal double mSheathDiameter;// : OPTIONAL IfcPositiveLengthMeasure;

		public IfcTendonTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public double NominalDiameter { get { return mNominalDiameter; } set { mNominalDiameter = value; } }

		internal IfcTendonType() : base() { }
		internal IfcTendonType(DatabaseIfc db, IfcTendonType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream)
		{
			mPredefinedType = t.mPredefinedType;
			mNominalDiameter = t.mNominalDiameter;
			mCrossSectionArea = t.mCrossSectionArea;
			mSheathDiameter = t.mSheathDiameter;
		}

		public IfcTendonType(DatabaseIfc m, string name, IfcTendonTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcTerminatorSymbol : IfcAnnotationSymbolOccurrence // DEPRECEATED IFC4
	{
		internal int mAnnotatedCurve;// : IfcAnnotationCurveOccurrence; 
		internal IfcTerminatorSymbol() : base() { }
		//internal IfcTerminatorSymbol(IfcTerminatorSymbol i) : base(i) { mAnnotatedCurve = i.mAnnotatedCurve; }
	}
	[Serializable]
	public abstract partial class IfcTessellatedFaceSet : IfcTessellatedItem, IfcBooleanOperand //ABSTRACT SUPERTYPE OF(IfcTriangulatedFaceSet, IfcPolygonalFaceSet )
	{
		internal int mCoordinates;// : 	IfcCartesianPointList;

		// INVERSE
		internal IfcIndexedColourMap mHasColours = null;// : SET [0:1] OF IfcIndexedColourMap FOR MappedTo;
		internal List<IfcIndexedTextureMap> mHasTextures = new List<IfcIndexedTextureMap>();// : SET [0:?] OF IfcIndexedTextureMap FOR MappedTo;

		public IfcCartesianPointList Coordinates { get { return mDatabase[mCoordinates] as IfcCartesianPointList; } set { mCoordinates = value.mIndex; } }
		public IfcIndexedColourMap HasColours { get { return mHasColours; } }
		public ReadOnlyCollection<IfcIndexedTextureMap> HasTextures { get { return new ReadOnlyCollection<IfcIndexedTextureMap>(mHasTextures); } }

		protected IfcTessellatedFaceSet() : base() { }
		protected IfcTessellatedFaceSet(DatabaseIfc db, IfcTessellatedFaceSet s) : base(db, s) { Coordinates = db.Factory.Duplicate(s.Coordinates) as IfcCartesianPointList; }
		protected IfcTessellatedFaceSet(IfcCartesianPointList3D pl) : base(pl.mDatabase) { mCoordinates = pl.mIndex; }
	}
	[Serializable]
	public abstract partial class IfcTessellatedItem : IfcGeometricRepresentationItem //IFC4
	{
		protected IfcTessellatedItem() : base() { }
		protected IfcTessellatedItem(DatabaseIfc db, IfcTessellatedItem i) : base(db, i) { }
		protected IfcTessellatedItem(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcTextLiteral : IfcGeometricRepresentationItem //SUPERTYPE OF	(IfcTextLiteralWithExtent)
	{
		internal string mLiteral = "";// : IfcPresentableText;
		internal int mPlacement;// : IfcAxis2Placement;
		internal IfcTextPath mPath;// : IfcTextPath;

		public string Literal { get { return ParserIfc.Decode(mLiteral); } set { mLiteral = ParserIfc.Encode(value); } }
		public IfcAxis2Placement Placement { get { return mDatabase[mPlacement] as IfcAxis2Placement; } }
		public IfcTextPath Path { get { return mPath; } set { mPath = value; } }

		internal IfcTextLiteral() : base() { }
		internal IfcTextLiteral(DatabaseIfc db, IfcTextLiteral l) : base(db, l) { mLiteral = l.mLiteral; mPlacement = db.Factory.Duplicate(l.mDatabase[l.mPlacement]).mIndex; mPath = l.mPath; }
	}
	[Serializable]
	public partial class IfcTextLiteralWithExtent : IfcTextLiteral
	{
		internal int mExtent;// : IfcPlanarExtent;
		internal string mBoxAlignment;// : IfcBoxAlignment; 

		public IfcPlanarExtent Extent { get { return mDatabase[mExtent] as IfcPlanarExtent; } }

		internal IfcTextLiteralWithExtent() : base() { }
		//internal IfcTextLiteralWithExtent(IfcTextLiteralWithExtent o) : base(o) { mExtent = o.mExtent; mBoxAlignment = o.mBoxAlignment; }
	}
	[Serializable]
	public partial class IfcTextStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal int mTextCharacterAppearance;// : OPTIONAL IfcCharacterStyleSelect;
		internal int mTextStyle;// : OPTIONAL IfcTextStyleSelect;
		internal int mTextFontStyle;// : IfcTextFontSelect; 
		internal bool mModelOrDraughting = true;//	:	OPTIONAL BOOLEAN; IFC4CHANGE
		internal IfcTextStyle() : base() { }
		//	internal IfcTextStyle(IfcTextStyle v) : base(v) { mTextCharacterAppearance = v.mTextCharacterAppearance; mTextStyle = v.mTextStyle; mTextFontStyle = v.mTextFontStyle; mModelOrDraughting = v.mModelOrDraughting; }
	}
	[Serializable]
	public partial class IfcTextStyleFontModel : IfcPreDefinedTextFont
	{
		internal List<string> mFontFamily = new List<string>(1);// : OPTIONAL LIST [1:?] OF IfcTextFontName;
		internal string mFontStyle = "$";// : OPTIONAL IfcFontStyle; ['normal','italic','oblique'];
		internal string mFontVariant = "$";// : OPTIONAL IfcFontVariant; ['normal','small-caps'];
		internal string mFontWeight = "$";// : OPTIONAL IfcFontWeight; // ['normal','small-caps','100','200','300','400','500','600','700','800','900'];
		internal string mFontSize;// : IfcSizeSelect; IfcSizeSelect = SELECT (IfcRatioMeasure ,IfcLengthMeasure ,IfcDescriptiveMeasure ,IfcPositiveLengthMeasure ,IfcNormalisedRatioMeasure ,IfcPositiveRatioMeasure);
		internal IfcTextStyleFontModel() : base() { }
		internal IfcTextStyleFontModel(DatabaseIfc db, IfcTextStyleFontModel m) : base(db, m)
		{
			//		mFontFamily = new List<string>(i.mFontFamily.ToArray());
			mFontStyle = m.mFontStyle;
			mFontVariant = m.mFontVariant;
			mFontWeight = m.mFontWeight;
			mFontSize = m.mFontSize;
		}
	}
	[Serializable]
	public partial class IfcTextStyleForDefinedFont : BaseClassIfc
	{
		internal int mColour;// : IfcColour;
		internal int mBackgroundColour;// : OPTIONAL IfcColour;
		internal IfcTextStyleForDefinedFont() : base() { }
		//	internal IfcTextStyleForDefinedFont(IfcTextStyleForDefinedFont o) : base() { mColour = o.mColour; mBackgroundColour = o.mBackgroundColour; }
	}
	[Serializable]
	public partial class IfcTextStyleTextModel : IfcPresentationItem
	{
		//internal int mDiffuseTransmissionColour, mDiffuseReflectionColour, mTransmissionColour, mReflectanceColour;//	 :	IfcColourRgb;
		internal IfcTextStyleTextModel() : base() { }
		internal IfcTextStyleTextModel(DatabaseIfc db, IfcTextStyleTextModel m) : base(db, m) { }
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcTextStyleWithBoxCharacteristics; // DEPRECEATED IFC4
	[Serializable]
	public abstract partial class IfcTextureCoordinate : IfcPresentationItem  //ABSTRACT SUPERTYPE OF(ONEOF(IfcIndexedTextureMap, IfcTextureCoordinateGenerator, IfcTextureMap))
	{
		internal List<int> mMaps = new List<int>();// : LIST [1:?] OF IfcSurfaceTexture
		public ReadOnlyCollection<IfcSurfaceTexture> Maps { get { return new ReadOnlyCollection<IfcSurfaceTexture>(mMaps.ConvertAll(x => mDatabase[x] as IfcSurfaceTexture)); } }

		internal IfcTextureCoordinate() : base() { }
		internal IfcTextureCoordinate(DatabaseIfc db, IfcTextureCoordinate c) : base(db, c) { c.Maps.ToList().ForEach(x => addMap(db.Factory.Duplicate(x) as IfcSurfaceTexture)); }
		public IfcTextureCoordinate(DatabaseIfc m, List<IfcSurfaceTexture> maps) : base(m) { mMaps = maps.ConvertAll(x => x.mIndex); }

		internal void addMap(IfcSurfaceTexture map) { mMaps.Add(map.mIndex); }
	}
	//ENTITY IfcTextureCoordinateGenerator
	//ENTITY IfcTextureMap
	//ENTITY IfcTextureVertex;
	[Serializable]
	public partial class IfcTextureVertexList : IfcPresentationItem
	{
		internal double[][] mTexCoordsList = new double[0][];// : LIST [1:?] OF IfcSurfaceTexture

		internal IfcTextureVertexList() : base() { }
		internal IfcTextureVertexList(DatabaseIfc db, IfcTextureVertexList l) : base(db, l) { mTexCoordsList = l.mTexCoordsList; }
		public IfcTextureVertexList(DatabaseIfc m, IEnumerable<Tuple<double, double>> coords) : base(m) { mTexCoordsList = coords.Select(x => new double[] { x.Item1, x.Item2 }).ToArray(); }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcThermalMaterialProperties : IfcMaterialProperties // DEPRECEATED IFC4
	{
		internal double mSpecificHeatCapacity = double.NaN;// : OPTIONAL IfcSpecificHeatCapacityMeasure;
		internal double mBoilingPoint = double.NaN;// : OPTIONAL IfcThermodynamicTemperatureMeasure;
		internal double mFreezingPoint = double.NaN;// : OPTIONAL IfcThermodynamicTemperatureMeasure;
		internal double mThermalConductivity = double.NaN;// : OPTIONAL IfcThermalConductivityMeasure; 
		internal IfcThermalMaterialProperties() : base() { }
		internal IfcThermalMaterialProperties(DatabaseIfc db, IfcThermalMaterialProperties p) : base(db, p) { mSpecificHeatCapacity = p.mSpecificHeatCapacity; mBoilingPoint = p.mBoilingPoint; mFreezingPoint = p.mFreezingPoint; mThermalConductivity = p.mThermalConductivity; }
	}
	public interface IfcTimeOrRatioSelect { } // IFC4 	IfcRatioMeasure, IfcDuration	
	[Serializable]
	public partial class IfcTimePeriod : BaseClassIfc // IFC4
	{
		internal string mStart; //:	IfcTime;
		internal string mFinish; //:	IfcTime;
		internal IfcTimePeriod() : base() { }
		internal IfcTimePeriod(IfcTimePeriod m) : base() { mStart = m.mStart; mFinish = m.mFinish; }
		public IfcTimePeriod(DatabaseIfc m, DateTime start, DateTime finish) : base(m) { mStart = IfcTime.convert(start); mFinish = IfcTime.convert(finish); }
	}
	[Serializable]
	public abstract partial class IfcTimeSeries : BaseClassIfc, IfcMetricValueSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect, NamedObjectIfc
	{ // ABSTRACT SUPERTYPE OF (ONEOF(IfcIrregularTimeSeries,IfcRegularTimeSeries));
		internal string mName = "$";// : OPTIONAL IfcLabel;		
		internal string mDescription;// : OPTIONAL IfcText;
		internal int mStartTime;// : IfcDateTimeSelect;
		internal int mEndTime;// : IfcDateTimeSelect;
		internal IfcTimeSeriesDataTypeEnum mTimeSeriesDataType = IfcTimeSeriesDataTypeEnum.NOTDEFINED;// : IfcTimeSeriesDataTypeEnum;
		internal IfcDataOriginEnum mDataOrigin = IfcDataOriginEnum.NOTDEFINED;// : IfcDataOriginEnum;
		internal string mUserDefinedDataOrigin = "$";// : OPTIONAL IfcLabel;
		internal int mUnit;// : OPTIONAL IfcUnit; 
						   //INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReferences = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public SET<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } set { mHasExternalReferences.Clear(); if (value != null) { mHasExternalReferences.CollectionChanged -= mHasExternalReferences_CollectionChanged; mHasExternalReferences = value; mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged; } } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>(mHasConstraintRelationships); } }

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
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReferences.CollectionChanged += mHasExternalReferences_CollectionChanged;
		}
		private void mHasExternalReferences_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcTimeSeriesReferenceRelationship; // DEPRECEATED IFC4
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcTimeSeriesSchedule // DEPRECEATED IFC4
	//ENTITY IfcTimeSeriesValue;  
	[Serializable]
	public abstract partial class IfcTopologicalRepresentationItem : IfcRepresentationItem  /*(IfcConnectedFaceSet,IfcEdge,IfcFace,IfcFaceBound,IfcLoop,IfcPath,IfcVertex))*/
	{
		protected IfcTopologicalRepresentationItem() : base() { }
		protected IfcTopologicalRepresentationItem(DatabaseIfc db) : base(db) { }
		protected IfcTopologicalRepresentationItem(DatabaseIfc db, IfcTopologicalRepresentationItem i) : base(db, i) { }
	}
	[Serializable]
	public partial class IfcTopologyRepresentation : IfcShapeModel
	{
		internal IfcTopologyRepresentation() : base() { }
		internal IfcTopologyRepresentation(DatabaseIfc db, IfcTopologyRepresentation r) : base(db, r) { }
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
		internal IfcToroidalSurface(DatabaseIfc db, IfcToroidalSurface s) : base(db, s) { mMajorRadius = s.mMajorRadius; mMinorRadius = s.mMinorRadius; }
		public IfcToroidalSurface(IfcAxis2Placement3D placement, double majorRadius, double minorRadius) : base(placement) { MajorRadius = majorRadius; MinorRadius = minorRadius; }
	}
	[Serializable]
	public partial class IfcTransformer : IfcEnergyConversionDevice //IFC4
	{
		internal IfcTransformerTypeEnum mPredefinedType = IfcTransformerTypeEnum.NOTDEFINED;// OPTIONAL : IfcTransformerTypeEnum;
		public IfcTransformerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTransformer() : base() { }
		internal IfcTransformer(DatabaseIfc db, IfcTransformer t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcTransformer(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcTransformerType : IfcEnergyConversionDeviceType
	{
		internal IfcTransformerTypeEnum mPredefinedType = IfcTransformerTypeEnum.NOTDEFINED;// : IfcTransformerEnum; 
		public IfcTransformerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTransformerType() : base() { }
		internal IfcTransformerType(DatabaseIfc db, IfcTransformerType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcTransformerType(DatabaseIfc m, string name, IfcTransformerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcTransitionCurveSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		private double mStartRadius = double.PositiveInfinity;// OPTIONAL IfcPositiveLengthMeasure;
		private double mEndRadius = double.PositiveInfinity;// OPTIONAL IfcPositiveLengthMeasure;
		private bool mIsStartRadiusCCW;// : IfcBoolean;
		private bool mIsEndRadiusCCW;// : IfcBoolean;
		private IfcTransitionCurveType mTransitionCurveType = IfcTransitionCurveType.BIQUADRATICPARABOLA;

		public double StartRadius { get { return double.IsNaN(mStartRadius) ? double.PositiveInfinity : mStartRadius; } set { mStartRadius = value; } }
		public double EndRadius { get { return double.IsNaN(mEndRadius) ? double.PositiveInfinity : mEndRadius; } set { mEndRadius = value; } }
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
		internal IfcTransitionCurveSegment2D(DatabaseIfc db, IfcTransitionCurveSegment2D s) : base(db, s) { mStartRadius = s.mStartRadius; mEndRadius = s.mEndRadius; mIsStartRadiusCCW = s.mIsStartRadiusCCW; mIsEndRadiusCCW = s.mIsEndRadiusCCW; mTransitionCurveType = s.mTransitionCurveType; }
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
	[Serializable]
	public partial class IfcTransportElement : IfcElement
	{
		internal IfcTransportElementTypeEnum mPredefinedType = IfcTransportElementTypeEnum.NOTDEFINED;// : 	OPTIONAL IfcTransportElementTypeEnum;
		internal double mCapacityByWeight = double.NaN;// : 	OPTIONAL IfcMassMeasure;
		internal double mCapacityByNumber = double.NaN;//	 : 	OPTIONAL IfcCountMeasure;

		public IfcTransportElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		//public double CapacityByWeight { get { return mCapacityByWeight; } set { mCapacityByWeight = value; } }
		//public double CapacityByNumber { get { return CapacityByNumber; } set { mCapacityByNumber = value; } }

		internal IfcTransportElement() : base() { }
		internal IfcTransportElement(DatabaseIfc db, IfcTransportElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { }
		public IfcTransportElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcTransportElementType : IfcElementType
	{
		internal IfcTransportElementTypeEnum mPredefinedType = IfcTransportElementTypeEnum.NOTDEFINED;// IfcTransportElementTypeEnum; 
		public IfcTransportElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTransportElementType() : base() { }
		internal IfcTransportElementType(DatabaseIfc db, IfcTransportElementType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcTransportElementType(DatabaseIfc m, string name, IfcTransportElementTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcTrapeziumProfileDef : IfcParameterizedProfileDef
	{
		internal double mBottomXDim;// : IfcPositiveLengthMeasure;
		internal double mTopXDim;// : IfcPositiveLengthMeasure;
		internal double mYDim;// : IfcPositiveLengthMeasure;
		internal double mTopXOffset;// : IfcPositiveLengthMeasure; 
		internal IfcTrapeziumProfileDef() : base() { }
		internal IfcTrapeziumProfileDef(DatabaseIfc db, IfcTrapeziumProfileDef p) : base(db, p) { mBottomXDim = p.mBottomXDim; mTopXDim = p.mTopXDim; mYDim = p.mYDim; mTopXOffset = p.mTopXOffset; }
		public IfcTrapeziumProfileDef(DatabaseIfc db, string name, double bottomXDim, double topXDim, double yDim, double topXOffset) : base(db, name)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.Ifc2x3NotAssigned)
				throw new Exception("Invalid Model View for IfcTrapeziumProfileDef : " + db.ModelView.ToString());
			mBottomXDim = bottomXDim;
			mTopXDim = topXDim;
			mYDim = yDim;
			mTopXOffset = topXOffset;
		}
	}
	[Serializable]
	public partial class IfcTriangulatedFaceSet : IfcTessellatedFaceSet
	{
		internal double[][] mNormals = new double[0][];// : OPTIONAL LIST [1:?] OF LIST [3:3] OF IfcParameterValue; 
		internal IfcLogicalEnum mClosed = IfcLogicalEnum.UNKNOWN; // 	OPTIONAL BOOLEAN;
		internal Tuple<int, int, int>[] mCoordIndex = new Tuple<int, int, int>[0];// : 	LIST [1:?] OF LIST [3:3] OF INTEGER;
		internal Tuple<int, int, int>[] mNormalIndex = new Tuple<int, int, int>[0];// :	OPTIONAL LIST [1:?] OF LIST [3:3] OF INTEGER;  
		internal List<int> mPnIndex = new List<int>(); // : OPTIONAL LIST [1:?] OF IfcPositiveInteger;

		public double[][] Normals { get { return mNormals; } set { mNormals = value; } }
		public bool Closed { get { return mClosed == IfcLogicalEnum.TRUE; } set { mClosed = value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE; } }
		public ReadOnlyCollection<Tuple<int, int, int>> CoordIndex { get { return new ReadOnlyCollection<Tuple<int, int, int>>(mCoordIndex); } }
		public ReadOnlyCollection<Tuple<int, int, int>> NormalIndex { get { return new ReadOnlyCollection<Tuple<int, int, int>>(mNormalIndex); } }
		public ReadOnlyCollection<int> PnIndex { get { return new ReadOnlyCollection<int>(mPnIndex); } }

		internal IfcTriangulatedFaceSet() : base() { }
		internal IfcTriangulatedFaceSet(DatabaseIfc db, IfcTriangulatedFaceSet s) : base(db, s)
		{
			if (s.mNormals.Length > 0)
				mNormals = s.mNormals.ToArray();
			mClosed = s.mClosed;
			mCoordIndex = s.mCoordIndex.ToArray();
			if (s.mNormalIndex.Length > 0)
				mNormalIndex = s.mNormalIndex.ToArray();
		}
		public IfcTriangulatedFaceSet(IfcCartesianPointList3D pl, bool closed, IEnumerable<Tuple<int, int, int>> coords)
			: base(pl) { setCoordIndex(coords); Closed = closed; }

		internal void setCoordIndex(IEnumerable<Tuple<int, int, int>> coords) { mCoordIndex = coords.ToArray(); }
	}
	[Serializable]
	public partial class IfcTriangulatedIrregularNetwork : IfcTriangulatedFaceSet
	{
		internal List<int> mFlags = new List<int>(); // : LIST [1:?] OF IfcInteger;
		public ReadOnlyCollection<int> Flags { get { return new ReadOnlyCollection<int>(mFlags); } }

		internal IfcTriangulatedIrregularNetwork() : base() { }
		internal IfcTriangulatedIrregularNetwork(DatabaseIfc db, IfcTriangulatedIrregularNetwork s) : base(db, s)
		{
			mFlags.AddRange(s.mFlags);
		}
		public IfcTriangulatedIrregularNetwork(IfcCartesianPointList3D pl, IEnumerable<Tuple<int, int, int>> coords, List<int> flags)
			: base(pl, false, coords) { mFlags.AddRange(flags); }
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
		internal IfcTrimmedCurve(DatabaseIfc db, IfcTrimmedCurve c) : base(db,c)
		{
			BasisCurve = db.Factory.Duplicate(c.BasisCurve) as IfcCurve;
			mTrim1 = new IfcTrimmingSelect(c.mTrim1.mIfcParameterValue);
			mTrim2 = new IfcTrimmingSelect(c.mTrim2.mIfcParameterValue);
			if (c.mTrim1.mIfcCartesianPoint > 0)
				mTrim1.mIfcCartesianPoint = db.Factory.Duplicate(c.mDatabase[c.mTrim1.mIfcCartesianPoint]).mIndex;
			if (c.mTrim2.mIfcCartesianPoint > 0)
				mTrim2.mIfcCartesianPoint = db.Factory.Duplicate(c.mDatabase[c.mTrim2.mIfcCartesianPoint]).mIndex;
			mSenseAgreement = c.mSenseAgreement;
			mMasterRepresentation = c.mMasterRepresentation;
		}
		public IfcTrimmedCurve(IfcConic basis, IfcTrimmingSelect start, IfcTrimmingSelect end, bool senseAgreement, IfcTrimmingPreference tp) 
			: this(basis.mDatabase, start,end, senseAgreement,tp) { BasisCurve = basis; }
		public IfcTrimmedCurve(IfcLine basis, IfcTrimmingSelect start, IfcTrimmingSelect end, bool senseAgreement, IfcTrimmingPreference tp)
			: this(basis.mDatabase, start, end, senseAgreement, tp) { BasisCurve = basis; }
		//public IfcTrimmedCurve(IfcClothoid basis, IfcTrimmingSelect start, IfcTrimmingSelect end, bool senseAgreement, IfcTrimmingPreference tp)
		//	: this(basis.Database, start, end, senseAgreement, tp) { BasisCurve = basis; }
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
				x = (aSlope * bSlope * (pt1[0] - pt3[1]) + bSlope * (pt1[0] + arcInteriorPoint.Item1)
					- aSlope * (arcInteriorPoint.Item1 + pt3[0])) / (2 * (bSlope - aSlope));
				y = -1 * (x - (pt1[0] + arcInteriorPoint.Item1) / 2) / aSlope + (pt1[1] + arcInteriorPoint.Item2) / 2;
			}

			double radius = Math.Sqrt(Math.Pow(pt1[0] - x, 2) + Math.Pow(pt1[1] - y, 2));
			BasisCurve = new IfcCircle(new IfcAxis2Placement2D(new IfcCartesianPoint(db, x, y)) { RefDirection = new IfcDirection(db, pt1[0]-x, pt1[1]-y) }, radius);
			mTrim1 = new IfcTrimmingSelect(0,start);
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
			mIfcParameterValue = double.NaN;
			mIfcCartesianPoint = (cp != null ? cp.mIndex : 0);
		}
		public IfcTrimmingSelect(double param) { mIfcParameterValue = param; }
		public IfcTrimmingSelect(double param, IfcCartesianPoint cp) : this(cp) { mIfcParameterValue = param; }
		
		internal double mIfcParameterValue = double.NaN;
		public double IfcParameterValue { get { return mIfcParameterValue; } }
		internal int mIfcCartesianPoint;
		public int IfcCartesianPoint { get { return mIfcCartesianPoint; } }
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
	}
	[Serializable]
	public partial class IfcTubeBundle : IfcEnergyConversionDevice //IFC4
	{
		internal IfcTubeBundleTypeEnum mPredefinedType = IfcTubeBundleTypeEnum.NOTDEFINED;// OPTIONAL : IfcTubeBundleTypeEnum;
		public IfcTubeBundleTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTubeBundle() : base() { }
		internal IfcTubeBundle(DatabaseIfc db, IfcTubeBundle b, IfcOwnerHistory ownerHistory, bool downStream) : base(db, b, ownerHistory, downStream) { mPredefinedType = b.mPredefinedType; }
		public IfcTubeBundle(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcTubeBundleType : IfcEnergyConversionDeviceType
	{
		internal IfcTubeBundleTypeEnum mPredefinedType = IfcTubeBundleTypeEnum.NOTDEFINED;// : IfcTubeBundleEnum; 
		public IfcTubeBundleTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcTubeBundleType() : base() { }
		internal IfcTubeBundleType(DatabaseIfc db, IfcTubeBundleType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcTubeBundleType(DatabaseIfc m, string name, IfcTubeBundleTypeEnum t) : base(m) { Name = name; PredefinedType = t; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcTwoDirectionRepeatFactor : IfcOneDirectionRepeatFactor // DEPRECEATED IFC4
	{
		internal int mSecondRepeatFactor;//  : IfcVector 
		public IfcVector SecondRepeatFactor { get { return mDatabase[mSecondRepeatFactor] as IfcVector; } set { mSecondRepeatFactor = value.mIndex; } }

		internal IfcTwoDirectionRepeatFactor() : base() { }
		internal IfcTwoDirectionRepeatFactor(DatabaseIfc db, IfcTwoDirectionRepeatFactor f) : base(db,f) { SecondRepeatFactor = db.Factory.Duplicate(f.SecondRepeatFactor) as IfcVector; }
	}
	[Serializable]
	public partial class IfcTypeObject : IfcObjectDefinition //(IfcTypeProcess, IfcTypeProduct, IfcTypeResource) IFC4 ABSTRACT 
	{
		internal string mApplicableOccurrence = "$";// : OPTIONAL IfcLabel;
		internal SET<IfcPropertySetDefinition> mHasPropertySets = new SET<IfcPropertySetDefinition>();// : OPTIONAL SET [1:?] OF IfcPropertySetDefinition 
		//INVERSE 
		internal IfcRelDefinesByType mObjectTypeOf = null;

		public string ApplicableOccurrence { get { return (mApplicableOccurrence == "$" ? "" : ParserIfc.Decode(mApplicableOccurrence)); } set { mApplicableOccurrence = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public SET<IfcPropertySetDefinition> HasPropertySets { get { return mHasPropertySets; } set { mHasPropertySets.Clear(); if (value != null) { mHasPropertySets.CollectionChanged -= mHasPropertySets_CollectionChanged; mHasPropertySets = value; mHasPropertySets.CollectionChanged += mHasPropertySets_CollectionChanged; } } }
		public IfcRelDefinesByType ObjectTypeOf { get { return mObjectTypeOf; } }
		//GeomGym
		internal IfcMaterialProfileSet mTapering = null;

		public new string Name { get { return base.Name; } set { base.Name = string.IsNullOrEmpty( value) ? "NameNotAssigned" : value; } }

		protected IfcTypeObject() : base() { Name = "NameNotAssigned"; }
		protected IfcTypeObject(IfcTypeObject basis) : base(basis) { mApplicableOccurrence = basis.mApplicableOccurrence; mHasPropertySets = basis.mHasPropertySets; mObjectTypeOf = basis.mObjectTypeOf; }
		protected IfcTypeObject(DatabaseIfc db, IfcTypeObject t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mApplicableOccurrence = t.mApplicableOccurrence; HasPropertySets.AddRange(t.HasPropertySets.ConvertAll(x=> db.Factory.Duplicate(x) as IfcPropertySetDefinition)); }
		[Obsolete("DEPRECEATED IFC4", false)]
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
		public IfcProfileProperties ProfileProperties
		{
			get
			{
				IEnumerable<IfcRelAssociatesProfileProperties> associates = HasAssociations.OfType<IfcRelAssociatesProfileProperties>();
				return associates.Count() <= 0 ? null : associates.First().RelatingProfileProperties;
			}
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
		internal void IsolateObject(string filename)
		{
			DatabaseIfc db = new DatabaseIfc(mDatabase);
			db.Factory.Duplicate(this,true);
			if (mObjectTypeOf != null)
			{
				foreach (IfcObject o in mObjectTypeOf.RelatedObjects)
					db.Factory.Duplicate(o);
			}
			IfcSite site = db.Project.RootElement as IfcSite;
			if (site != null)
			{
				IfcProductRepresentation pr = site.Representation;
				if (pr != null)
				{
					site.Representation = null;
					pr.Dispose(true);
				}
			}
			db.WriteFile(filename);
		}
	}
	[Serializable]
	public abstract partial class IfcTypeProcess : IfcTypeObject //ABSTRACT SUPERTYPE OF(ONEOF(IfcEventType, IfcProcedureType, IfcTaskType))
	{
		private string mIdentification = "$";// :	OPTIONAL IfcIdentifier;
		private string mLongDescription = "$";//	 :	OPTIONAL IfcText;
		private string mProcessType = "$";//	 :	OPTIONAL IfcLabel;

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string LongDescription { get { return (mLongDescription == "$" ? "" : ParserIfc.Decode(mLongDescription)); } set { mLongDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string ProcessType { get { return (mProcessType == "$" ? "" : ParserIfc.Decode(mProcessType)); } set { mProcessType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcTypeProcess() : base() { }
		protected IfcTypeProcess(DatabaseIfc db, IfcTypeProcess t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mIdentification = t.mIdentification; mLongDescription = t.mLongDescription; mProcessType = t.mProcessType; }
		protected IfcTypeProcess(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcTypeProduct : IfcTypeObject, IfcProductSelect //SUPERTYPE OF (ONEOF (IfcDoorStyle ,IfcElementType ,IfcSpatialElementType ,IfcWindowStyle)) 
	{ 
		internal LIST<IfcRepresentationMap> mRepresentationMaps = new LIST<IfcRepresentationMap>();// : OPTIONAL LIST [1:?] OF UNIQUE IfcRepresentationMap;
		private string mTag = "$";// : OPTIONAL IfcLabel 
		//INVERSE
		internal SET<IfcRelAssignsToProduct> mReferencedBy = new SET<IfcRelAssignsToProduct>();//	 :	SET OF IfcRelAssignsToProduct FOR RelatingProduct;
		
		public LIST<IfcRepresentationMap> RepresentationMaps { get { return mRepresentationMaps; } set { mRepresentationMaps.Clear(); if (value != null) { mRepresentationMaps.CollectionChanged -= mRepresentationMaps_CollectionChanged; mRepresentationMaps = value; mRepresentationMaps.CollectionChanged += mRepresentationMaps_CollectionChanged; } } }
		public string Tag { get { return (mTag == "$" ? "" : mTag); } set { mTag = (string.IsNullOrEmpty(value) ? "$" : value); } }
		public SET<IfcRelAssignsToProduct> ReferencedBy { get { return mReferencedBy; } }

		protected IfcTypeProduct() : base() { }
		protected IfcTypeProduct(IfcTypeProduct basis) : base(basis)
		{
			mRepresentationMaps = basis.mRepresentationMaps;
			mTag = basis.mTag;
		}
		protected IfcTypeProduct(DatabaseIfc db, IfcTypeProduct t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { RepresentationMaps.AddRange(t.RepresentationMaps.ConvertAll(x=> db.Factory.Duplicate(x) as IfcRepresentationMap)); mTag = t.mTag; }
		public IfcTypeProduct(DatabaseIfc db) : base(db) {  }

		protected override void initialize()
		{
			base.initialize();

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
		
		internal override void changeSchema(ReleaseVersion schema)
		{
			foreach(IfcRepresentationMap repMap in RepresentationMaps)
				repMap.changeSchema(schema);
			foreach(IfcPropertySetDefinition pset in HasPropertySets)
				pset.changeSchema(schema);
			base.changeSchema(schema);
		}

		internal IfcElement genMappedItemElement(IfcProduct host, IfcAxis2Placement3D relativePlacement)
		{
			DatabaseIfc db = host.Database;
			string typename = this.GetType().Name;
			typename = typename.Substring(0, typename.Length - 4);
			IfcShapeRepresentation sr = new IfcShapeRepresentation(new IfcMappedItem(RepresentationMaps[0], db.Factory.XYPlaneTransformation));
			IfcProductDefinitionShape pds = new IfcProductDefinitionShape(sr);
			IfcElement element = IfcElement.ConstructElement(typename, host, new IfcLocalPlacement(host.Placement, relativePlacement), pds);
			element.setRelatingType(this);
			foreach (IfcRelNests nests in IsNestedBy)
			{
				foreach (IfcObjectDefinition od in nests.RelatedObjects)
				{
					IfcDistributionPort port = od as IfcDistributionPort;
					if (port != null)
					{
						IfcDistributionPort newPort = new IfcDistributionPort(element) { FlowDirection = port.FlowDirection, PredefinedType = port.PredefinedType, SystemType = port.SystemType };
						newPort.Placement = new IfcLocalPlacement(element.Placement, (port.Placement as IfcLocalPlacement).RelativePlacement);
						for (int dcounter = 0; dcounter < port.mIsDefinedBy.Count; dcounter++)
							port.mIsDefinedBy[dcounter].RelatedObjects.Add(newPort);
					}
				}
			}
			foreach(IfcPropertySetDefinition pset in HasPropertySets)
			{
				if (pset.IsInstancePropertySet)
					pset.RelateObjectDefinition(element);
			}
			return element;
		}

		internal static IfcTypeProduct constructType(DatabaseIfc db, string className, string name)
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
			IfcTypeProduct result = null;
			Type type = Type.GetType("GeometryGym.Ifc." + str);
			if (type != null)
			{
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
						result = ctor.Invoke(new object[] { db, name,predefined  }) as IfcTypeProduct;
					}
				}
				else
					result = ctor.Invoke(new object[] { db, name }) as IfcTypeProduct;

			
				if (result != null && !string.IsNullOrEmpty(definedType))
				{
					IfcElementType et = result as IfcElementType;
					type = result.GetType();
					PropertyInfo pi = type.GetProperty("PredefinedType");
					if (pi != null)
					{
						if (enumType != null)
						{
							FieldInfo fi = enumType.GetField(definedType);
							if (fi == null)
							{
								if (et != null)
								{
									et.ElementType = definedType;
									fi = enumType.GetField("NOTDEFINED");
								}
							}
							if (fi != null)
							{
								int i = (int)fi.GetValue(enumType);
								object newEnumValue = Enum.ToObject(enumType, i);
								pi.SetValue(result, newEnumValue, null);
							}
							else if (et != null)
								et.ElementType = definedType;
						}
						else if (et != null)
							et.ElementType = definedType;
					}
				}
			}
			return result;
		}
	}
	[Serializable]
	public abstract partial class IfcTypeResource : IfcTypeObject //ABSTRACT SUPERTYPE OF(IfcConstructionResourceType)
	{
		internal string mIdentification = "$";// :	OPTIONAL IfcIdentifier;
		internal string mLongDescription = "$";//	 :	OPTIONAL IfcText;
		internal string mResourceType = "$";//	 :	OPTIONAL IfcLabel;

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string LongDescription { get { return (mLongDescription == "$" ? "" : ParserIfc.Decode(mLongDescription)); } set { mLongDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string ResourceType { get { return (mResourceType == "$" ? "" : ParserIfc.Decode(mResourceType)); } set { mResourceType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcTypeResource() : base() { }
		protected IfcTypeResource(DatabaseIfc db, IfcTypeResource t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mIdentification = t.mIdentification; mLongDescription = t.mLongDescription; mResourceType = t.mResourceType; }
		protected IfcTypeResource(DatabaseIfc db) : base(db) { }
	}
}
