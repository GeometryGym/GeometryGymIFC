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
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public partial class IfcSanitaryTerminal : IfcFlowTerminal
	{
		internal IfcSanitaryTerminalTypeEnum mPredefinedType = IfcSanitaryTerminalTypeEnum.NOTDEFINED;// : OPTIONAL IfcSanitaryTerminalTypeEnum; 
		public IfcSanitaryTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSanitaryTerminal() : base() { }
		internal IfcSanitaryTerminal(DatabaseIfc db, IfcSanitaryTerminal t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcSanitaryTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcSanitaryTerminalType : IfcFlowTerminalType
	{
		internal IfcSanitaryTerminalTypeEnum mPredefinedType = IfcSanitaryTerminalTypeEnum.NOTDEFINED;// : IfcSanitaryTerminalTypeEnum; 
		public IfcSanitaryTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSanitaryTerminalType() : base() { }
		internal IfcSanitaryTerminalType(DatabaseIfc db, IfcSanitaryTerminalType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcSanitaryTerminalType(DatabaseIfc m, string name, IfcSanitaryTerminalTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcScheduleTimeControl : IfcControl // DEPRECATED IFC4
	{
		internal int mActualStart, mEarlyStart, mLateStart, mScheduleStart, mActualFinish, mEarlyFinish, mLateFinish, mScheduleFinish;// OPTIONAL  IfcDateTimeSelect;
		internal double mScheduleDuration = double.NaN, mActualDuration = double.NaN, mRemainingTime = double.NaN, mFreeFloat = double.NaN, mTotalFloat = double.NaN;//	 OPTIONAL IfcTimeMeasure;
		internal bool mIsCritical;//	 :	OPTIONAL BOOLEAN;
		internal int mStatusTime;//	: 	OPTIONAL IfcDateTimeSelect
		internal double mStartFloat, mFinishFloat;//	 OPTIONAL IfcTimeMeasure; 
		internal double mCompletion = double.NaN;//	 :	OPTIONAL IfcPositiveRatioMeasure; 
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
		internal IfcScheduleTimeControl(DatabaseIfc db, IfcScheduleTimeControl c, DuplicateOptions options) : base(db,c, options)
		{
			mActualStart = c.mActualStart; mEarlyStart = c.mEarlyStart; mLateStart = c.mLateStart; mScheduleStart = c.mScheduleStart;
			mActualFinish = c.mActualFinish; mEarlyFinish = c.mEarlyFinish; mLateFinish = c.mLateFinish; mScheduleFinish = c.mScheduleFinish;
			mScheduleDuration = c.mScheduleDuration; mActualDuration = c.mActualDuration; mRemainingTime = c.mRemainingTime; mFreeFloat = c.mFreeFloat;
			mTotalFloat = c.mTotalFloat; mIsCritical = c.mIsCritical; mStatusTime = c.mStatusTime; mStartFloat = c.mStartFloat; mFinishFloat = c.mFinishFloat; mCompletion = c.mCompletion;
		}
		public IfcScheduleTimeControl(DatabaseIfc db) : base (db) { }
		
		internal DateTime getActualStart() { IfcDateTimeSelect dts = ActualStart; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal DateTime getActualFinish() { IfcDateTimeSelect dts = ActualFinish; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal DateTime getScheduleStart() { IfcDateTimeSelect dts = ScheduleStart; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal DateTime getScheduleFinish() { IfcDateTimeSelect dts = ScheduleFinish; return (dts == null ? DateTime.MinValue : dts.DateTime); }
		internal TimeSpan getScheduleDuration() { return new TimeSpan(0, 0, (int)mScheduleDuration); }
	}
	[Serializable]
	public abstract partial class IfcSchedulingTime : BaseClassIfc, NamedObjectIfc  //	ABSTRACT SUPERTYPE OF(ONEOF(IfcEventTime, IfcLagTime, IfcResourceTime, IfcTaskTime, IfcWorkTime));
	{
		internal string mName = "$";//	 :	OPTIONAL IfcLabel;
		internal IfcDataOriginEnum mDataOrigin = IfcDataOriginEnum.NOTDEFINED;// OPTIONAL : IfcDataOriginEnum;
		internal string mUserDefinedDataOrigin = "$";//:	OPTIONAL IfcLabel; 

		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } } 
		public IfcDataOriginEnum DataOrigin { get { return mDataOrigin; } set { mDataOrigin = value; } }
		public string UserDefinedDataOrigin { get { return (mUserDefinedDataOrigin == "$" ? "" : ParserIfc.Decode(mName)); } set { mUserDefinedDataOrigin = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcSchedulingTime() : base() { }
		protected IfcSchedulingTime(DatabaseIfc db, IfcSchedulingTime t) : base(db,t) { mName = t.mName; mDataOrigin = t.mDataOrigin; mUserDefinedDataOrigin = t.mUserDefinedDataOrigin; }
		protected IfcSchedulingTime(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcSeamCurve : IfcSurfaceCurve //IFC4 Add2
	{
		internal IfcSeamCurve() : base() { }
		internal IfcSeamCurve(DatabaseIfc db, IfcIntersectionCurve c, DuplicateOptions options) : base(db, c, options) { }
		public IfcSeamCurve(IfcCurve curve, IfcPcurve p1, IfcPcurve p2, IfcPreferredSurfaceCurveRepresentation cr) : base(curve, p1, p2, cr) { }
	}
	[Serializable]
	public abstract partial class IfcSectionedSolid : IfcSolidModel
	{
		internal IfcCurve mDirectrix;// : IfcCurve;
		internal LIST<IfcProfileDef> mCrossSections = new LIST<IfcProfileDef>();// : LIST [2:?] OF IfcProfileDef;

		public IfcCurve Directrix { get { return mDirectrix; } set { mDirectrix = value; value.mDirectrixOfSectionedSolids.Add(this); } }
		public LIST<IfcProfileDef> CrossSections { get { return mCrossSections; } set { mCrossSections.Clear(); if (value != null) CrossSections = value; } }

		protected IfcSectionedSolid() : base() { }
		protected IfcSectionedSolid(DatabaseIfc db, IfcSectionedSolid s, DuplicateOptions options) : base(db, s, options)
		{
			Directrix = db.Factory.Duplicate(s.Directrix) as IfcCurve;
			CrossSections.AddRange(s.CrossSections.ConvertAll(x => db.Factory.Duplicate(x) as IfcProfileDef));
		}
		protected IfcSectionedSolid(IfcCurve directrix, IEnumerable<IfcProfileDef> crossSections) : base(directrix.Database)
		{
			Directrix = directrix;
			CrossSections.AddRange(crossSections);
		}
	}
	[Serializable]
	public partial class IfcSectionedSolidHorizontal : IfcSectionedSolid 
	{
		[Obsolete("REVISED IFC4x3", false)]
		internal LIST<IfcPointByDistanceExpression> mCrossSectionPositions_OBSOLETE = new LIST<IfcPointByDistanceExpression>();// : LIST [2:?] OF IfcDistanceExpression;
		internal LIST<IfcCurveMeasureSelect> mCrossSectionPositions = new LIST<IfcCurveMeasureSelect>();// : LIST [2:?] OF IfcDistanceExpression;
		internal bool mFixedAxisVertical;// : IfcBoolean

		public LIST<IfcCurveMeasureSelect> CrossSectionPositions { get { return mCrossSectionPositions; } set { mCrossSectionPositions.Clear(); if (value != null) CrossSectionPositions = value; } }
		public bool FixedAxisVertical { get { return mFixedAxisVertical; } set { mFixedAxisVertical = value; } }

		internal IfcSectionedSolidHorizontal() : base() { }
		internal IfcSectionedSolidHorizontal(DatabaseIfc db, IfcSectionedSolidHorizontal s, DuplicateOptions options) : base(db, s, options)
		{
			CrossSectionPositions.AddRange(s.CrossSectionPositions);
			mCrossSectionPositions_OBSOLETE.AddRange(s.mCrossSectionPositions_OBSOLETE.ConvertAll(x => db.Factory.Duplicate(x) as IfcPointByDistanceExpression));
			FixedAxisVertical = s.FixedAxisVertical;
		}
		public IfcSectionedSolidHorizontal(IfcCurve directrix, IEnumerable<IfcProfileDef> profiles, IEnumerable<IfcCurveMeasureSelect> positions, bool fixedAxisVertical)
			: base(directrix, profiles)
		{
			CrossSectionPositions.AddRange(positions);
			FixedAxisVertical = fixedAxisVertical;
		}
		[Obsolete("REVISED IFC4x3", false)]
		public IfcSectionedSolidHorizontal(IfcCurve directrix, IEnumerable<IfcProfileDef> profiles, IEnumerable<IfcPointByDistanceExpression> positions, bool fixedAxisVertical)
		: base(directrix, profiles)
		{
			mCrossSectionPositions_OBSOLETE.AddRange(positions);
			FixedAxisVertical = fixedAxisVertical;
		}
	}
	[Serializable]
	public partial class IfcSectionedSpine : IfcGeometricRepresentationItem
	{
		internal IfcCompositeCurve mSpineCurve;// : IfcCompositeCurve;
		internal LIST<IfcProfileDef> mCrossSections = new LIST<IfcProfileDef>();// : LIST [2:?] OF IfcProfileDef;
		internal LIST<IfcAxis2Placement3D> mCrossSectionPositions = new LIST<IfcAxis2Placement3D>();// : LIST [2:?] OF IfcAxis2Placement3D; 

		public IfcCompositeCurve SpineCurve { get { return mSpineCurve; } set { mSpineCurve = value; } }
		public LIST<IfcProfileDef> CrossSections { get { return mCrossSections; } set { mCrossSections.Clear(); if (value != null) CrossSections = value; } }
		public LIST<IfcAxis2Placement3D> CrossSectionPositions { get { return mCrossSectionPositions; } set { mCrossSectionPositions.Clear(); if(value != null) mCrossSectionPositions = value; } }

		internal IfcSectionedSpine() : base() { }
		internal IfcSectionedSpine(DatabaseIfc db, IfcSectionedSpine s, DuplicateOptions options) : base(db, s, options)
		{
			SpineCurve = db.Factory.Duplicate(s.SpineCurve) as IfcCompositeCurve;
			CrossSections.AddRange(s.CrossSections.ConvertAll(x => db.Factory.Duplicate(x) as IfcProfileDef));
			CrossSectionPositions.AddRange(s.CrossSectionPositions.ConvertAll(x=>db.Factory.Duplicate(x) as IfcAxis2Placement3D));
		}
	}
	[Serializable]
	public partial class IfcSectionedSurface : IfcSurface
	{
		private IfcCurve mDirectrix = null; //: IfcCurve;
		private LIST<IfcPointByDistanceExpression> mCrossSectionPositions = new LIST<IfcPointByDistanceExpression>(); //: LIST[2:?] OF IfcDistanceExpression;
		private LIST<IfcProfileDef> mCrossSections = new LIST<IfcProfileDef>(); //: LIST[2:?] OF IfcProfileDef;
		private bool mFixedAxisVertical = false; //: IfcBoolean;

		public IfcCurve Directrix { get { return mDirectrix; } set { mDirectrix = value; } }
		public LIST<IfcPointByDistanceExpression> CrossSectionPositions { get { return mCrossSectionPositions; } set { mCrossSectionPositions = value; } }
		public LIST<IfcProfileDef> CrossSections { get { return mCrossSections; } set { mCrossSections = value; } }
		public bool FixedAxisVertical { get { return mFixedAxisVertical; } set { mFixedAxisVertical = value; } }

		public IfcSectionedSurface() : base() { }
		internal IfcSectionedSurface(DatabaseIfc db, IfcSectionedSurface s, DuplicateOptions options) : base(db, s, options)
		{
			Directrix = db.Factory.Duplicate(s.Directrix) as IfcCurve;
			CrossSectionPositions.AddRange(s.CrossSectionPositions.ConvertAll(x => db.Factory.Duplicate(x) as IfcPointByDistanceExpression));
			CrossSections.AddRange(s.CrossSections.ConvertAll(x => db.Factory.Duplicate(x) as IfcProfileDef));
			FixedAxisVertical = s.FixedAxisVertical;
		}
		public IfcSectionedSurface(IfcCurve directrix, IEnumerable<IfcPointByDistanceExpression> crossSectionPositions, IEnumerable<IfcProfileDef> crossSections, bool fixedAxisVertical)
			: base(directrix.Database)
		{
			Directrix = directrix;
			CrossSectionPositions.AddRange(crossSectionPositions);
			CrossSections.AddRange(crossSections);
			FixedAxisVertical = fixedAxisVertical;
		}
	}
	[Serializable]
	public partial class IfcSectionProperties : IfcPreDefinedProperties // IFC2x3 BaseClassIfc
	{
		internal IfcSectionTypeEnum mSectionType = IfcSectionTypeEnum.UNIFORM;// : IfcSectionTypeEnum;
		internal int mStartProfile;// IfcProfileDef;
		internal int mEndProfile;// : OPTIONAL IfcProfileDef;

		public IfcSectionTypeEnum SectionType { get { return mSectionType; } set { mSectionType = value; } }
		public IfcProfileDef StartProfile { get { return mDatabase[mStartProfile] as IfcProfileDef; } set { mStartProfile = value.mIndex; } }
		public IfcProfileDef EndProfile { get { return mDatabase[mEndProfile] as IfcProfileDef; } set { mStartProfile = (value == null ? 0 : value.mIndex); } }

		internal IfcSectionProperties() : base() { }
		internal IfcSectionProperties(DatabaseIfc db, IfcSectionProperties p, DuplicateOptions options) : base(db, p, options) { mSectionType = p.mSectionType; mStartProfile = db.Factory.Duplicate(p.mDatabase[p.mStartProfile]).mIndex; mEndProfile = db.Factory.Duplicate(p.mDatabase[p.mEndProfile]).mIndex; }
		public IfcSectionProperties(IfcProfileDef startProfile) : base(startProfile.mDatabase) { StartProfile = startProfile; }
		public IfcSectionProperties(IfcProfileDef startProfile, IfcProfileDef endProfile) : this(startProfile) { SectionType = IfcSectionTypeEnum.TAPERED; EndProfile = endProfile; }
	}
	[Serializable]
	public partial class IfcSectionReinforcementProperties : IfcPreDefinedProperties // IFC2x3 STPEntity
	{
		internal double mLongitudinalStartPosition;//	:	IfcLengthMeasure;
		internal double mLongitudinalEndPosition;//	:	IfcLengthMeasure;
		internal double mTransversePosition = double.NaN;//	:	OPTIONAL IfcLengthMeasure;
		internal IfcReinforcingBarRoleEnum mReinforcementRole = IfcReinforcingBarRoleEnum.NOTDEFINED;//	:	IfcReinforcingBarRoleEnum;
		internal int mSectionDefinition;//	:	IfcSectionProperties;
		internal List<int> mCrossSectionReinforcementDefinitions = new List<int>();// : SET [1:?] OF IfcReinforcementBarProperties;

		public double LongitudinalStartPosition { get { return mLongitudinalStartPosition; } set { mLongitudinalStartPosition = value; } }
		public double LongitudinalEndPosition { get { return mLongitudinalEndPosition; } set { mLongitudinalEndPosition = value; } }
		public double TransversePosition { get { return mTransversePosition; } set { mTransversePosition = value; } }
		public IfcReinforcingBarRoleEnum ReinforcementRole { get { return mReinforcementRole; } set { mReinforcementRole = value; } }
		public IfcSectionProperties SectionDefinition { get { return mDatabase[mSectionDefinition] as IfcSectionProperties; } set { mSectionDefinition = value.mIndex; } }
		public ReadOnlyCollection<IfcReinforcementBarProperties> CrossSectionReinforcementDefinitions { get { return new ReadOnlyCollection<IfcReinforcementBarProperties>( mCrossSectionReinforcementDefinitions.ConvertAll(x => mDatabase[x] as IfcReinforcementBarProperties)); } } 

		internal IfcSectionReinforcementProperties() : base() { }
		internal IfcSectionReinforcementProperties(DatabaseIfc db, IfcSectionReinforcementProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mLongitudinalStartPosition = p.mLongitudinalStartPosition;
			mLongitudinalEndPosition = p.mLongitudinalEndPosition;
			mTransversePosition = p.mTransversePosition;
			mReinforcementRole = p.mReinforcementRole;
			mSectionDefinition = db.Factory.Duplicate(p.mDatabase[p.mSectionDefinition]).mIndex;
			mCrossSectionReinforcementDefinitions = p.mCrossSectionReinforcementDefinitions.ConvertAll(x=>db.Factory.Duplicate( p.mDatabase[x]).mIndex);
		}
		private IfcSectionReinforcementProperties(double longitudinalStart, double longitudinalEnd, IfcReinforcingBarRoleEnum role, IfcSectionProperties properties) 
			: base(properties.mDatabase)
		{
			LongitudinalStartPosition = longitudinalStart;
			LongitudinalEndPosition = longitudinalEnd;
			ReinforcementRole = role;
			SectionDefinition = properties;
		}
		public IfcSectionReinforcementProperties(double longitudinalStart, double longitudinalEnd, IfcReinforcingBarRoleEnum role, IfcSectionProperties properties, IfcReinforcementBarProperties definition) 
			: this(longitudinalStart, longitudinalEnd, role, properties)
		{
			mCrossSectionReinforcementDefinitions.Add(definition.mIndex);
		}
		public IfcSectionReinforcementProperties(double longitudinalStart, double longitudinalEnd, IfcReinforcingBarRoleEnum role, IfcSectionProperties properties, IEnumerable<IfcReinforcementBarProperties> definitions)
			: this(longitudinalStart, longitudinalEnd, role, properties)
		{
			foreach (IfcReinforcementBarProperties definition in definitions)
				mCrossSectionReinforcementDefinitions.Add(definition.mIndex);
		}
	}
	[Serializable]
	public abstract partial class IfcSegment : IfcGeometricRepresentationItem
	{
		private IfcTransitionCode mTransition = IfcTransitionCode.DISCONTINUOUS; //: IfcTransitionCode;
		public IfcTransitionCode Transition { get { return mTransition; } set { mTransition = value; } }

		//INVERSE
			//UsingCurves : SET[1:?] OF IfcCompositeCurve FOR Segments;

		protected IfcSegment() : base() { }
		protected IfcSegment(DatabaseIfc db, IfcSegment s, DuplicateOptions options) : base(db, s, options) { Transition = s.Transition; }
		protected IfcSegment(DatabaseIfc db, IfcTransitionCode transition)
			: base(db) { Transition = transition; }
	}
	public partial interface IfcSegmentIndexSelect { } //SELECT ( IfcLineIndex, IfcArcIndex);
	[Serializable]
	public partial class IfcSegmentedReferenceCurve : IfcCompositeCurve
	{
		private IfcBoundedCurve mBaseCurve = null; //: IfcBoundedCurve;
		private IfcPlacement mEndPoint = null; //: OPTIONAL IfcPlacement;

		public IfcBoundedCurve BaseCurve { get { return mBaseCurve; } set { mBaseCurve = value; } }
		public IfcPlacement EndPoint { get { return mEndPoint; } set { mEndPoint = value; } }

		public IfcSegmentedReferenceCurve() : base() { }
		internal IfcSegmentedReferenceCurve(DatabaseIfc db, IfcSegmentedReferenceCurve segmentedReferenceCurve, DuplicateOptions options)
		: base(db, segmentedReferenceCurve, options)
		{
			BaseCurve = db.Factory.Duplicate(segmentedReferenceCurve.BaseCurve) as IfcBoundedCurve;
			EndPoint = db.Factory.Duplicate(segmentedReferenceCurve.EndPoint) as IfcPlacement;
		}
		public IfcSegmentedReferenceCurve(IfcBoundedCurve baseCurve, IEnumerable<IfcCurveSegment> segments)
			: base(segments)
		{
			BaseCurve = baseCurve;
		}
	}
	[Serializable]
	public partial class IfcSensor : IfcDistributionControlElement //IFC4  
	{
		internal IfcSensorTypeEnum mPredefinedType = IfcSensorTypeEnum.NOTDEFINED;
		public IfcSensorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcSensor() : base() { }
		internal IfcSensor(DatabaseIfc db, IfcSensor s, DuplicateOptions options) : base(db, s, options) { mPredefinedType = s.mPredefinedType; }
		public IfcSensor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcSensorType : IfcDistributionControlElementType
	{
		internal IfcSensorTypeEnum mPredefinedType = IfcSensorTypeEnum.NOTDEFINED;// : IfcSensorTypeEnum; 
		public IfcSensorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSensorType() : base() { }
		internal IfcSensorType(DatabaseIfc db, IfcSensorType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcSensorType(DatabaseIfc m, string name, IfcSensorTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcServiceLife // DEPRECATED IFC4
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcServiceLifeFactor // DEPRECATED IFC4
	[Serializable]
	public partial class IfcShadingDevice : IfcBuiltElement
	{
		internal IfcShadingDeviceTypeEnum mPredefinedType = IfcShadingDeviceTypeEnum.NOTDEFINED;//: OPTIONAL IfcShadingDeviceTypeEnum; 
		public IfcShadingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcShadingDevice() : base() { }
		internal IfcShadingDevice(DatabaseIfc db, IfcShadingDevice d, DuplicateOptions options) : base(db, d, options) { mPredefinedType = d.mPredefinedType; }
		public IfcShadingDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcShadingDeviceType : IfcBuiltElementType
	{
		internal IfcShadingDeviceTypeEnum mPredefinedType = IfcShadingDeviceTypeEnum.NOTDEFINED;
		public IfcShadingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcShadingDeviceType() : base() { }
		internal IfcShadingDeviceType(DatabaseIfc db, IfcShadingDeviceType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcShadingDeviceType(DatabaseIfc m, string name, IfcShadingDeviceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcShapeAspect : BaseClassIfc, NamedObjectIfc
	{
		internal LIST<IfcShapeModel> mShapeRepresentations = new LIST<IfcShapeModel>();// : LIST [1:?] OF IfcShapeModel;
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		private IfcLogicalEnum mProductDefinitional;// : LOGICAL;
		internal IfcProductRepresentationSelect mPartOfProductDefinitionShape;// IFC4 OPTIONAL IfcProductRepresentationSelect IFC2x3 IfcProductDefinitionShape;

		public LIST<IfcShapeModel> ShapeRepresentations { get { return mShapeRepresentations; } }
		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcLogicalEnum ProductDefinitional { get { return mProductDefinitional; } set { mProductDefinitional = value; } }
		public IfcProductRepresentationSelect PartOfProductDefinitionShape { get { return mPartOfProductDefinitionShape; } set { mPartOfProductDefinitionShape = value; if (value != null) value.HasShapeAspects.Add(this); } }

		internal IfcShapeAspect() : base() { }
		internal IfcShapeAspect(DatabaseIfc db, IfcShapeAspect a) : base(db,a)
		{
			a.ShapeRepresentations.ToList().ForEach(x=>addRepresentation(db.Factory.Duplicate(x) as IfcShapeModel));
			mName = a.mName;
			mDescription = a.mDescription;
			mProductDefinitional = a.mProductDefinitional;
		}
		public IfcShapeAspect(List<IfcShapeModel> shapeRepresentations) : base(shapeRepresentations[0].Database) { shapeRepresentations.ForEach(x=>addRepresentation(x)); }
		public IfcShapeAspect(IfcShapeModel shapeRepresentation) : base(shapeRepresentation.mDatabase) { addRepresentation(shapeRepresentation); }
		
		internal void addRepresentation(IfcShapeModel model) { mShapeRepresentations.Add(model); model.mOfShapeAspect = this; }
	}
	[Serializable]
	public abstract partial class IfcShapeModel : IfcRepresentation<IfcRepresentationItem>//ABSTRACT SUPERTYPE OF (ONEOF (IfcShapeRepresentation,IfcTopologyRepresentation))
	{
		//INVERSE
		internal IfcShapeAspect mOfShapeAspect = null; //:	SET [0:1] OF IfcShapeAspect FOR ShapeRepresentations;
		public IfcShapeAspect OfShapeAspect { get { return mOfShapeAspect; } set { mOfShapeAspect = value; } }

		protected IfcShapeModel() : base() { }
		protected IfcShapeModel(IfcRepresentationItem item) : base(item.mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Body), item) { }
		protected IfcShapeModel(IfcGeometricRepresentationContext context, IfcRepresentationItem item) : base(context, item) { }
		protected IfcShapeModel(IfcGeometricRepresentationContext context, IEnumerable<IfcRepresentationItem> reps) : base(context, reps) { }
		protected IfcShapeModel(DatabaseIfc db, IfcShapeModel m, DuplicateOptions options) : base(db, m, options)
		{
			if (m.OfShapeAspect != null)
				OfShapeAspect = db.Factory.Duplicate(m.mOfShapeAspect) as IfcShapeAspect;
		}
	}
	[Serializable]
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
		internal IfcShapeRepresentation(DatabaseIfc db, IfcShapeRepresentation r, DuplicateOptions options) : base(db, r, options) { }
		public IfcShapeRepresentation(IfcGeometricRepresentationItem representation, ShapeRepresentationType representationType) : base(representation.mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Body), representation) { RepresentationType = representationType.ToString(); }
		public IfcShapeRepresentation(IfcGeometricRepresentationContext context, IfcGeometricRepresentationItem representation, ShapeRepresentationType representationType) : base(context, representation) { RepresentationType = representationType.ToString(); RepresentationIdentifier = context.ContextIdentifier; }
		public IfcShapeRepresentation(IfcGeometricRepresentationContext context, IEnumerable<IfcRepresentationItem> items, ShapeRepresentationType representationType) : base(context, items) { RepresentationType = representationType.ToString(); }
		public IfcShapeRepresentation(IfcMappedItem representation, ShapeRepresentationType representationType) : base(representation.mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Body), representation) { RepresentationType = representationType.ToString(); }
		public IfcShapeRepresentation(IfcAdvancedBrep brep) : base(brep) { RepresentationType = ShapeRepresentationType.AdvancedBrep.ToString(); }
		public IfcShapeRepresentation(IfcAnnotationFillArea area) : base(area.mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.PlanSymbol2d), area) { RepresentationType = ShapeRepresentationType.FillArea.ToString(); }
		public IfcShapeRepresentation(IfcBooleanResult boolean) : base(boolean) { RepresentationType = boolean is IfcBooleanClippingResult ? ShapeRepresentationType.Clipping.ToString() : ShapeRepresentationType.CSG.ToString(); }
		public IfcShapeRepresentation(IfcBoundingBox boundingBox) : base(boundingBox.Database.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.BoundingBox), boundingBox) { RepresentationType = ShapeRepresentationType.BoundingBox.ToString(); }
		public IfcShapeRepresentation(IfcCsgPrimitive3D csg) : base(csg) { RepresentationType = ShapeRepresentationType.CSG.ToString(); }
		public IfcShapeRepresentation(IfcCsgSolid csg) : base(csg) { RepresentationType = ShapeRepresentationType.CSG.ToString(); }
		public IfcShapeRepresentation(IfcCurve curve) : base(curve) { RepresentationType = ShapeRepresentationType.Curve.ToString(); }
		public IfcShapeRepresentation(IfcCurveSegment curveSegment, bool isTwoD) 
			: base(curveSegment.Database.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis), curveSegment)
		{
			IfcCurve parentCurve = curveSegment.ParentCurve;
			if (parentCurve != null)
				RepresentationType = isTwoD ? ShapeRepresentationType.Curve2D.ToString() : ShapeRepresentationType.Curve3D.ToString();
		}

		//should remove above as in 3d?? hierarchy test
		public IfcShapeRepresentation(IfcFacetedBrep brep) : base(brep) { RepresentationType = ShapeRepresentationType.Brep.ToString(); }
		public IfcShapeRepresentation(IfcFaceBasedSurfaceModel surface) : base(surface) { RepresentationType = ShapeRepresentationType.SurfaceModel.ToString(); }
		public IfcShapeRepresentation(IfcGeometricSet set) : base(set) { RepresentationType = ShapeRepresentationType.GeometricSet.ToString(); }
		public IfcShapeRepresentation(IfcMappedItem item) : base(item) { RepresentationType = ShapeRepresentationType.MappedRepresentation.ToString(); }
		public IfcShapeRepresentation(IfcPoint point) : base(point) { RepresentationType = ShapeRepresentationType.Point.ToString(); }
		//internal IfcShapeRepresentation(IfcRepresentationMap rm) : base(new IfcMappedItem(rm), "Model", "MappedRepresentation") { }
		public IfcShapeRepresentation(IfcSectionedSpine spine) : base(spine)
		{
			RepresentationType = ShapeRepresentationType.SectionedSpine.ToString();
			if (spine.mCrossSections.Count > 0)
			{
				IfcProfileDef pd = spine.CrossSections[0];
				if (pd.mProfileType == IfcProfileTypeEnum.CURVE)
					RepresentationType = "Surface3D";
			}
		}
		public IfcShapeRepresentation(IfcShellBasedSurfaceModel surface) : base(surface) { RepresentationType = ShapeRepresentationType.SurfaceModel.ToString(); }
		public IfcShapeRepresentation(IfcSurface surface) : base(surface) { RepresentationType = ShapeRepresentationType.Surface3D.ToString(); }
		public IfcShapeRepresentation(IfcExtrudedAreaSolid extrudedAreaSolid) : base(extrudedAreaSolid) { RepresentationType = ShapeRepresentationType.SweptSolid.ToString(); }
		public IfcShapeRepresentation(IfcRevolvedAreaSolid revolvedAreaSolid) : base(revolvedAreaSolid) { RepresentationType = ShapeRepresentationType.SweptSolid.ToString(); }
		public IfcShapeRepresentation(IfcExtrudedAreaSolidTapered extrudedAreaSolidTapered) : base(extrudedAreaSolidTapered) { RepresentationType = ShapeRepresentationType.AdvancedSweptSolid.ToString(); }
		public IfcShapeRepresentation(IfcRevolvedAreaSolidTapered revolvedAreaSolidTapered) : base(revolvedAreaSolidTapered) { RepresentationType = ShapeRepresentationType.AdvancedSweptSolid.ToString(); ; }
		public IfcShapeRepresentation(IfcSweptAreaSolid sweep) : base(sweep) { RepresentationType = ShapeRepresentationType.AdvancedSweptSolid.ToString(); ; }
		public IfcShapeRepresentation(IfcSectionedSolid sectionedSolid) : base(sectionedSolid) { RepresentationType = ShapeRepresentationType.SectionedSpine.ToString(); }
		public IfcShapeRepresentation(IfcSolidModel solid) : base(solid)
		{
			//ABSTRACT SUPERTYPE OF (ONEOF(IfcCsgSolid ,IfcManifoldSolidBrep,IfcSweptAreaSolid,IfcSweptDiskSolid))
			IfcCsgSolid cs = solid as IfcCsgSolid;
			if (cs != null)
				RepresentationType = ShapeRepresentationType.CSG.ToString(); 
			else
			{
				IfcManifoldSolidBrep msb = solid as IfcManifoldSolidBrep;
				if (msb != null)
					RepresentationType = ShapeRepresentationType.Brep.ToString();
				else
				{
					IfcAdvancedBrep ab = solid as IfcAdvancedBrep;
					if (ab != null)
						RepresentationType = ShapeRepresentationType.AdvancedBrep.ToString(); 
				}
			}
		}
		public IfcShapeRepresentation(IfcTessellatedItem item) : base(item) { RepresentationType = ShapeRepresentationType.Tessellation.ToString(); }
		public IfcShapeRepresentation(IfcGeometricRepresentationContext context, List<IfcMappedItem> reps) : base(context, reps.ConvertAll(x => (IfcRepresentationItem)x)) { RepresentationType = ShapeRepresentationType.MappedRepresentation.ToString(); }
		public IfcShapeRepresentation(IfcGeometricRepresentationContext context, IfcVertexPoint point) : base(context, point) { RepresentationType = ShapeRepresentationType.Point.ToString(); }
		public IfcShapeRepresentation(IfcGeometricRepresentationContext context, IfcEdgeCurve edge) : base(context, edge) { RepresentationType = ShapeRepresentationType.Curve.ToString(); }
		public IfcShapeRepresentation(IfcGeometricRepresentationContext context, IfcFaceSurface surface) : base(context, surface) { RepresentationType = ShapeRepresentationType.Surface.ToString(); }
		internal static IfcShapeRepresentation CreateRepresentation(IfcRepresentationItem representationItem)
		{
			if (representationItem == null)
				return null;
			IfcBooleanResult booleanResult = representationItem as IfcBooleanResult;
			if (booleanResult != null)
				return new IfcShapeRepresentation(booleanResult);
			IfcCurve curve = representationItem as IfcCurve;
			if (curve != null)
				return new IfcShapeRepresentation(curve, ShapeRepresentationType.Curve);
			IfcCsgPrimitive3D csgPrimitive = representationItem as IfcCsgPrimitive3D;
			if (csgPrimitive != null)
				return new IfcShapeRepresentation(csgPrimitive);
			IfcCsgSolid csgSolid = representationItem as IfcCsgSolid;
			if (csgSolid != null)
				return new IfcShapeRepresentation(csgSolid);
			IfcExtrudedAreaSolid extrudedAreaSolid = representationItem as IfcExtrudedAreaSolid;
			if (extrudedAreaSolid != null)
				return new IfcShapeRepresentation(extrudedAreaSolid);
			IfcRevolvedAreaSolid revolvedAreaSolid = representationItem as IfcRevolvedAreaSolid;
			if (revolvedAreaSolid != null)
				return new IfcShapeRepresentation(revolvedAreaSolid);
			IfcSweptAreaSolid sweptAreaSolid = representationItem as IfcSweptAreaSolid;
			if (sweptAreaSolid != null)
				return new IfcShapeRepresentation(sweptAreaSolid);
			IfcFacetedBrep facetedBrep = representationItem as IfcFacetedBrep;
			if (facetedBrep != null)
				return new IfcShapeRepresentation(facetedBrep);
			IfcAdvancedBrep advancedBrep = representationItem as IfcAdvancedBrep;
			if (advancedBrep != null)
				return new IfcShapeRepresentation(advancedBrep);
			IfcSolidModel solidModel = representationItem as IfcSolidModel;
			if (solidModel != null)
				return new IfcShapeRepresentation(solidModel);
			IfcFaceBasedSurfaceModel faceBasedSurfaceModel = representationItem as IfcFaceBasedSurfaceModel;
			if (faceBasedSurfaceModel != null)
				return new IfcShapeRepresentation(faceBasedSurfaceModel);
			IfcGeometricSet geometricSet = representationItem as IfcGeometricSet;
			if (geometricSet != null)
				return new IfcShapeRepresentation(geometricSet);
			IfcPoint point = representationItem as IfcPoint;
			if (point != null)
				return new IfcShapeRepresentation(point);
			IfcSectionedSpine sectionedSpine = representationItem as IfcSectionedSpine;
			if (sectionedSpine != null)
				return new IfcShapeRepresentation(sectionedSpine);
			IfcSectionedSolid sectionedSolid = representationItem as IfcSectionedSolid;
			if (sectionedSolid != null)
				return new IfcShapeRepresentation(sectionedSolid);
			IfcShellBasedSurfaceModel shellBasedSurfaceModel = representationItem as IfcShellBasedSurfaceModel;
			if (shellBasedSurfaceModel != null)
				return new IfcShapeRepresentation(shellBasedSurfaceModel);
			IfcSurface surface = representationItem as IfcSurface;
			if (surface != null)
				return new IfcShapeRepresentation(surface);
			IfcTessellatedItem tessellatedItem = representationItem as IfcTessellatedItem;
			if (tessellatedItem != null)
				return new IfcShapeRepresentation(tessellatedItem);
			IfcMappedItem mappedItem = representationItem as IfcMappedItem;
			if (mappedItem != null)
				return new IfcShapeRepresentation(mappedItem);

			Trace.WriteLine("XX Error Can't identify " + representationItem.ToString() + " as shape representation!");
			return null;
		}
	}
	public partial interface IfcShell : IBaseClassIfc  // SELECT(IfcClosedShell, IfcOpenShell);
	{
		SET<IfcFace> CfsFaces { get; }
	}
	[Serializable]
	public partial class IfcShellBasedSurfaceModel : IfcGeometricRepresentationItem
	{
		internal SET<IfcShell> mSbsmBoundary = new SET<IfcShell>();// : SET [1:?] OF IfcShell; 
		public SET<IfcShell> SbsmBoundary { get { return mSbsmBoundary; } }

		internal IfcShellBasedSurfaceModel() : base() { }
		internal IfcShellBasedSurfaceModel(DatabaseIfc db, IfcShellBasedSurfaceModel m, DuplicateOptions options) : base(db,m, options)
		{
			mSbsmBoundary.AddRange(m.mSbsmBoundary.Select(x => db.Factory.Duplicate(x) as IfcShell));
		}
		public IfcShellBasedSurfaceModel(IfcShell shell) : base(shell.Database) { mSbsmBoundary.Add(shell); }
		public IfcShellBasedSurfaceModel(List<IfcShell> shells) : base(shells[0].Database) { mSbsmBoundary.AddRange(shells); }
		
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach(BaseClassIfc shell in SbsmBoundary)
				result.AddRange(shell.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public partial class IfcSign : IfcElementComponent
	{
		private IfcSignTypeEnum mPredefinedType = IfcSignTypeEnum.NOTDEFINED; //: OPTIONAL IfcSignTypeEnum;
		public IfcSignTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcSign() : base() { }
		public IfcSign(DatabaseIfc db) : base(db) { }
		public IfcSign(DatabaseIfc db, IfcSign sign, DuplicateOptions options) : base(db, sign, options) { PredefinedType = sign.PredefinedType; }
		public IfcSign(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcSignal : IfcFlowTerminal
	{
		private IfcSignalTypeEnum mPredefinedType = IfcSignalTypeEnum.NOTDEFINED; //: OPTIONAL IfcSignalTypeEnum;
		public IfcSignalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcSignal() : base() { }
		public IfcSignal(DatabaseIfc db) : base(db) { }
		public IfcSignal(DatabaseIfc db, IfcSignal signal, DuplicateOptions options) : base(db, signal, options) { PredefinedType = signal.PredefinedType; }
		public IfcSignal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcSignalType : IfcFlowTerminalType
	{
		private IfcSignalTypeEnum mPredefinedType = IfcSignalTypeEnum.NOTDEFINED; //: IfcSignalTypeEnum;
		public IfcSignalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcSignalType() : base() { }
		public IfcSignalType(DatabaseIfc db, IfcSignalType signalType, DuplicateOptions options) : base(db, signalType, options) { PredefinedType = signalType.PredefinedType; }
		public IfcSignalType(DatabaseIfc db, string name, IfcSignalTypeEnum predefinedType) : base(db)
		{
			Name = name;
			PredefinedType = predefinedType;
		}
	}

	[Serializable]
	public partial class IfcSignType : IfcElementComponentType
	{
		private IfcSignTypeEnum mPredefinedType = IfcSignTypeEnum.NOTDEFINED; //: IfcSignTypeEnum;
		public IfcSignTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcSignType() : base() { }
		public IfcSignType(DatabaseIfc db, IfcSignType signType, DuplicateOptions options) : base(db, signType, options) { PredefinedType = signType.PredefinedType; }
		public IfcSignType(DatabaseIfc db, string name, IfcSignTypeEnum predefinedType)
			: base(db, name)
		{
			PredefinedType = predefinedType;
		}
	}

	[Serializable]
	public abstract partial class IfcSimpleProperty : IfcProperty //ABSTRACT SUPERTYPE OF (ONEOF (IfcPropertyBoundedValue ,IfcPropertyEnumeratedValue ,
	{ // IfcPropertyListValue,IfcPropertyReferenceValue,IfcPropertySingleValue,IfcPropertyTableValue)) 
		protected IfcSimpleProperty() : base() { }
		protected IfcSimpleProperty(IfcSimpleProperty simpleProperty) : base(simpleProperty) { }
		protected IfcSimpleProperty(DatabaseIfc db, IfcSimpleProperty p, DuplicateOptions options) : base(db, p, options) { }
		protected IfcSimpleProperty(DatabaseIfc m, string name) : base(m, name) { }
	}
	[Serializable]
	public partial class IfcSimplePropertyTemplate : IfcPropertyTemplate
	{
		private IfcSimplePropertyTemplateTypeEnum mTemplateType = IfcSimplePropertyTemplateTypeEnum.NOTDEFINED;// : OPTIONAL IfcSimplePropertyTemplateTypeEnum;
		internal string mPrimaryMeasureType = "$";// : OPTIONAL IfcLabel;
		internal string mSecondaryMeasureType = "$";// : OPTIONAL IfcLabel;
		internal int mEnumerators = 0;// : OPTIONAL IfcPropertyEnumeration;
		internal int mPrimaryUnit = 0, mSecondaryUnit = 0;// : OPTIONAL IfcUnit; 
		internal string mExpression = "$";// : OPTIONAL IfcLabel;
		internal IfcStateEnum mAccessState = IfcStateEnum.NOTDEFINED;//	:	OPTIONAL IfcStateEnum;

		public IfcSimplePropertyTemplateTypeEnum TemplateType { get { return mTemplateType; } set { mTemplateType = value; } }
		public string PrimaryMeasureType { get { return (mPrimaryMeasureType == "$" ? "" : ParserIfc.Decode(mPrimaryMeasureType)); } set { mPrimaryMeasureType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string SecondaryMeasureType { get { return (mSecondaryMeasureType == "$" ? "" : ParserIfc.Decode(mSecondaryMeasureType)); } set { mSecondaryMeasureType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcPropertyEnumeration Enumerators { get { return mDatabase[mEnumerators] as IfcPropertyEnumeration; } set { mEnumerators = (value == null ? 0 : value.mIndex); } }
		public IfcUnit PrimaryUnit { get { return mDatabase[mPrimaryUnit] as IfcUnit; } set { mPrimaryUnit = (value == null ? 0 : value.Index); } }
		public IfcUnit SecondaryUnit { get { return mDatabase[mSecondaryUnit] as IfcUnit; } set { mSecondaryUnit = (value == null ? 0 : value.Index); } }
		public string Expression { get { return (mExpression == "$" ? "" : ParserIfc.Decode(mExpression)); } set { mExpression = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcStateEnum AccessState { get { return mAccessState; } set { mAccessState = value; } }

		internal IfcSimplePropertyTemplate() : base() { }
		internal IfcSimplePropertyTemplate(DatabaseIfc db, IfcSimplePropertyTemplate s, DuplicateOptions options) : base(db, s, options)
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
	}
	[Serializable]
	public partial class IfcSine : IfcSpiral
	{
		private double mSineTerm = 0; //: IfcLengthMeasure;
		private double mConstant = double.NaN; //: IfcReal;

		public double SineTerm { get { return mSineTerm; } set { mSineTerm = value; } }
		public double Constant { get { return mConstant; } set { mConstant = value; } }

		public IfcSine() : base() { }
		internal IfcSine(DatabaseIfc db, IfcCosine cosine, DuplicateOptions options)
			: base(db, cosine, options) { SineTerm = cosine.CosineTerm; Constant = cosine.Constant; }
		public IfcSine(IfcAxis2Placement position, double cosineTerm)
			: base(position) { SineTerm = cosineTerm; }
	}
	[Serializable]
	public partial class IfcSite : IfcSpatialStructureElement
	{
		internal IfcCompoundPlaneAngleMeasure mRefLatitude = null;// : OPTIONAL IfcCompoundPlaneAngleMeasure;
		internal IfcCompoundPlaneAngleMeasure mRefLongitude = null;// : OPTIONAL IfcCompoundPlaneAngleMeasure;
		internal double mRefElevation = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal string mLandTitleNumber = "$";// : OPTIONAL IfcLabel;
		internal int mSiteAddress;// : OPTIONAL IfcPostalAddress; 

		public IfcCompoundPlaneAngleMeasure RefLatitude { get { return mRefLatitude; } set { mRefLatitude = value; } }
		public IfcCompoundPlaneAngleMeasure RefLongitude { get { return mRefLongitude; } set { mRefLongitude = value; } }
		public double RefElevation { get { return mRefElevation; } set { mRefElevation = value; } }
		public string LandTitleNumber { get { return (mLandTitleNumber == "$" ? "" : ParserIfc.Decode(mLandTitleNumber)); } set { mLandTitleNumber = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcPostalAddress SiteAddress { get { return mDatabase[mSiteAddress] as IfcPostalAddress; } set { mSiteAddress = (value == null ? 0 : value.mIndex); } }

		internal IfcSite() : base() { }
		internal IfcSite(DatabaseIfc db, IfcSpatialElement spatial, DuplicateOptions options) : base(db, spatial, options) { }
		internal IfcSite(DatabaseIfc db, IfcSite site, DuplicateOptions options) : base(db, site, options) 
		{ 
			mRefLatitude = site.mRefLatitude; 
			mRefLongitude = site.mRefLongitude; 
			mRefElevation = site.mRefElevation; 
			mLandTitleNumber = site.mLandTitleNumber; 
			if (site.mSiteAddress > 0) 
				SiteAddress = db.Factory.Duplicate(site.SiteAddress) as IfcPostalAddress; 
		}
		public IfcSite(DatabaseIfc db, string name) : base(db.Factory.RootPlacement) { Name = name; }
		public IfcSite(IfcSite host, string name) : base(host, name) { }
		public IfcSite(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape shape) : base(host, placement, shape) { }
	}
	[Serializable]
	public partial class IfcSIUnit : IfcNamedUnit
	{
		private static double[] mFactors = new double[] { 1, Math.Pow(10, 18), Math.Pow(10, 15), Math.Pow(10, 12), Math.Pow(10, 9), Math.Pow(10, 6), Math.Pow(10, 3), Math.Pow(10, 2), Math.Pow(10, 1), Math.Pow(10, -1), Math.Pow(10, -2), Math.Pow(10, -3), Math.Pow(10, -6), Math.Pow(10, -9), Math.Pow(10, -12), Math.Pow(10, -15), Math.Pow(10, -18) };
		private IfcSIPrefix mPrefix = IfcSIPrefix.NONE;// : OPTIONAL IfcSIPrefix;
		private IfcSIUnitName mName;// : IfcSIUnitName; 

		public IfcSIPrefix Prefix { get { return mPrefix; } set { mPrefix = value; } }
		public IfcSIUnitName Name { get { return mName; } set { mName = value; } }

		internal IfcSIUnit() : base() { }
		internal IfcSIUnit(DatabaseIfc db, IfcSIUnit u) : base(db, u) { mPrefix = u.mPrefix; mName = u.mName; }
		public IfcSIUnit(DatabaseIfc m, IfcUnitEnum unitEnum, IfcSIPrefix pref, IfcSIUnitName name) : base(m, unitEnum, false) { mPrefix = pref; mName = name; }
		public override double SIFactor
		{
			get
			{
				int pow = 1;
				if (UnitType == IfcUnitEnum.AREAUNIT)
					pow = 2;
				else if (UnitType == IfcUnitEnum.VOLUMEUNIT)
					pow = 3;
				return Math.Pow( mFactors[(int)mPrefix], pow);
			}
		}
	}
	[Serializable]
	public partial class IfcSlab : IfcBuiltElement
	{
		internal IfcSlabTypeEnum mPredefinedType = IfcSlabTypeEnum.NOTDEFINED;// : OPTIONAL IfcSlabTypeEnum 
		public IfcSlabTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSlab() : base() { }
		internal IfcSlab(DatabaseIfc db, IfcSlab s, DuplicateOptions options) : base(db, s, options) { mPredefinedType = s.mPredefinedType; }
		public IfcSlab(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcSlabElementedCase : IfcSlab
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 || mDatabase.mModelView == ModelView.Ifc4Reference ? "IfcSlab" : base.StepClassName); } }
		internal IfcSlabElementedCase() : base() { }
		internal IfcSlabElementedCase(DatabaseIfc db, IfcSlabElementedCase s, DuplicateOptions options) : base(db, s, options) { }
	}
	[Serializable]
	public partial class IfcSlabStandardCase : IfcSlab
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 || mDatabase.mModelView == ModelView.Ifc4Reference ? "IfcSlab" : base.StepClassName); } }
		internal IfcSlabStandardCase() : base() { }
		internal IfcSlabStandardCase(DatabaseIfc db, IfcSlabStandardCase s, DuplicateOptions options) : base(db, s, options) { }
		public IfcSlabStandardCase(IfcProduct host, IfcMaterialLayerSetUsage layerSetUsage, IfcAxis2Placement3D placement, IfcProfileDef perimeter) : base(host, new IfcLocalPlacement(host.ObjectPlacement, placement), null)
		{
			SetMaterial(layerSetUsage);
			IfcAxis2Placement3D position = (Math.Abs(layerSetUsage.OffsetFromReferenceLine) > mDatabase.Tolerance ? new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase, 0, 0, layerSetUsage.OffsetFromReferenceLine)) : null);
			Representation = new IfcProductDefinitionShape(new IfcShapeRepresentation(new IfcExtrudedAreaSolid(perimeter, position, layerSetUsage.DirectionSense == IfcDirectionSenseEnum.POSITIVE ? mDatabase.Factory.ZAxis : mDatabase.Factory.ZAxisNegative, layerSetUsage.ForLayerSet.MaterialLayers.Sum(x=>x.LayerThickness))));
		}
	}
	[Serializable]
	public partial class IfcSlabType : IfcBuiltElementType
	{
		internal IfcSlabTypeEnum mPredefinedType = IfcSlabTypeEnum.NOTDEFINED;
		public IfcSlabTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcSlabType() : base() { }
		public IfcSlabType(DatabaseIfc m, string name, IfcSlabTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal IfcSlabType(DatabaseIfc db, IfcSlabType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcSlabType(string name, IfcMaterialLayerSet ls, IfcSlabTypeEnum type) : base(ls.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = ls; }
	}
	[Serializable]
	public partial class IfcSlippageConnectionCondition : IfcStructuralConnectionCondition
	{
		private double mSlippageX = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mSlippageY = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mSlippageZ = double.NaN; //: OPTIONAL IfcLengthMeasure;

		public double SlippageX { get { return mSlippageX; } set { mSlippageX = value; } }
		public double SlippageY { get { return mSlippageY; } set { mSlippageY = value; } }
		public double SlippageZ { get { return mSlippageZ; } set { mSlippageZ = value; } }

		public IfcSlippageConnectionCondition() : base() { }
		public IfcSlippageConnectionCondition(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcSolarDevice : IfcEnergyConversionDevice //IFC4
	{
		internal IfcSolarDeviceTypeEnum mPredefinedType = IfcSolarDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcSolarDeviceTypeEnum;
		public IfcSolarDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSolarDevice() : base() { }
		internal IfcSolarDevice(DatabaseIfc db, IfcSolarDevice d, DuplicateOptions options) : base(db, d, options) { mPredefinedType = d.mPredefinedType; }
		public IfcSolarDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcSolarDeviceType : IfcEnergyConversionDeviceType
	{
		internal IfcSolarDeviceTypeEnum mPredefinedType = IfcSolarDeviceTypeEnum.NOTDEFINED;// : IfcSolarDeviceTypeEnum; 
		public IfcSolarDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSolarDeviceType() : base() { }
		internal IfcSolarDeviceType(DatabaseIfc db, IfcSolarDeviceType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcSolarDeviceType(DatabaseIfc m, string name, IfcSolarDeviceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcSolidModel : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcSolidOrShell /* ABSTRACT SUPERTYPE OF (ONEOF(IfcCsgSolid ,IfcManifoldSolidBrep,IfcSweptAreaSolid,IfcSweptDiskSolid))*/
	{
		protected IfcSolidModel() : base() { }
		protected IfcSolidModel(DatabaseIfc db) : base(db) { }
		protected IfcSolidModel(DatabaseIfc db, IfcSolidModel p, DuplicateOptions options) : base(db, p, options) { }
	}
	public interface IfcSolidOrShell : IBaseClassIfc { } // SELECT(IfcSolidModel, IfcClosedShell);
	[Serializable]
	public partial class IfcSolidStratum : IfcGeotechnicalStratum
	{
		public IfcSolidStratum() : base() { }
		public IfcSolidStratum(DatabaseIfc db) : base(db) { }
		public IfcSolidStratum(DatabaseIfc db, IfcSolidStratum solidStratum, DuplicateOptions options) : base(db, solidStratum, options) { }
		public IfcSolidStratum(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcSoundProperties : IfcPropertySetDefinition // DEPRECATED IFC4
	{
		internal bool mIsAttenuating;// : IfcBoolean;
		internal IfcSoundScaleEnum mSoundScale = IfcSoundScaleEnum.NOTDEFINED;// : OPTIONAL IfcSoundScaleEnum
		internal List<int> mSoundValues = new List<int>(1);// : LIST [1:8] OF IfcSoundValue;

		public ReadOnlyCollection<IfcSoundValue> SoundValues { get { return new ReadOnlyCollection<IfcSoundValue>( mSoundValues.ConvertAll(x => mDatabase[x] as IfcSoundValue)); } }

		internal IfcSoundProperties() : base() { }
		internal IfcSoundProperties(DatabaseIfc db, IfcSoundProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mIsAttenuating = p.mIsAttenuating;
			mSoundScale = p.mSoundScale;
			p.SoundValues.ToList().ForEach(x => addSoundValue( db.Factory.Duplicate(x) as IfcSoundValue));
		}
		
		internal void addSoundValue(IfcSoundValue value) { mSoundValues.Add(value.mIndex); }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcSoundValue : IfcPropertySetDefinition // DEPRECATED IFC4
	{
		internal int mSoundLevelTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal double mFrequency;// : IfcFrequencyMeasure;
		internal double mSoundLevelSingleValue;// : OPTIONAL IfcDerivedMeasureValue; 

		public IfcTimeSeries SoundLevelTimeSeries { get { return mDatabase[mSoundLevelTimeSeries] as IfcTimeSeries; } set { mSoundLevelTimeSeries = (value == null ? 0 : value.mIndex); } }

		internal IfcSoundValue() : base() { }
		internal IfcSoundValue(DatabaseIfc db, IfcSoundValue v, DuplicateOptions options) : base(db, v, options)
		{
			if (v.mSoundLevelTimeSeries > 0)
				SoundLevelTimeSeries = db.Factory.Duplicate(v.SoundLevelTimeSeries) as IfcTimeSeries;
			mFrequency = v.mFrequency;
			mSoundLevelSingleValue = v.mSoundLevelSingleValue;
		}
	}
	[Serializable]
	public partial class IfcSpace : IfcSpatialStructureElement, IfcSpaceBoundarySelect
	{
		//internal IfcInternalOrExternalEnum mInteriorOrExteriorSpace = IfcInternalOrExternalEnum.NOTDEFINED;// : IfcInternalOrExternalEnum; replaced IFC4
		internal IfcSpaceTypeEnum mPredefinedType = IfcSpaceTypeEnum.NOTDEFINED; 	//:	OPTIONAL IfcSpaceTypeEnum;
		internal double mElevationWithFlooring = double.NaN;// : OPTIONAL IfcLengthMeasure;
		//INVERSE
		internal SET<IfcRelCoversSpaces> mHasCoverings = new SET<IfcRelCoversSpaces>(); // : SET [0:?] OF IfcRelCoversSpaces FOR RelatedSpace;
		internal SET<IfcRelSpaceBoundary> mBoundedBy = new SET<IfcRelSpaceBoundary>();  //	BoundedBy : SET [0:?] OF IfcRelSpaceBoundary FOR RelatingSpace;

		public IfcSpaceTypeEnum PredefinedType
		{
			get { return mPredefinedType; }
			set
			{
				mPredefinedType = value;
				if (mDatabase.mRelease < ReleaseVersion.IFC4)
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
		public SET<IfcRelCoversSpaces> HasCoverings { get { return mHasCoverings; } }
		public SET<IfcRelSpaceBoundary> BoundedBy { get { return mBoundedBy; } }

		internal IfcSpace() : base() { }
		internal IfcSpace(DatabaseIfc db, IfcSpace s, DuplicateOptions options) : base(db, s, options) { mPredefinedType = s.mPredefinedType; mElevationWithFlooring = s.mElevationWithFlooring; }
		public IfcSpace(IfcSpatialStructureElement host, string name) : base(host, name) { IfcRelCoversSpaces cs = new IfcRelCoversSpaces(this, null); }
		public IfcSpace(IfcSpatialStructureElement host, string name, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { Name = name; }
		internal IfcSpace(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		public void AddBoundary(IfcRelSpaceBoundary boundary) { mBoundedBy.Add(boundary); }
	}
	public partial interface IfcSpaceBoundarySelect : IBaseClassIfc //IFC4 SELECT (	IfcSpace, IfcExternalSpatialElement);
	{
		void AddBoundary(IfcRelSpaceBoundary boundary);
	}
	[Serializable]
	public partial class IfcSpaceHeater : IfcFlowTerminal //IFC4
	{
		internal IfcSpaceHeaterTypeEnum mPredefinedType = IfcSpaceHeaterTypeEnum.NOTDEFINED;// OPTIONAL : IfcSpaceHeaterTypeEnum;
		public IfcSpaceHeaterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSpaceHeater() : base() { }
		internal IfcSpaceHeater(IfcFlowTerminal flowTerminal) : base(flowTerminal, true) { }
		internal IfcSpaceHeater(DatabaseIfc db, IfcSpaceHeater h, DuplicateOptions options) : base(db, h, options) { mPredefinedType = h.mPredefinedType; }
		public IfcSpaceHeater(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcSpaceHeaterType : IfcFlowTerminalType
	{
		internal IfcSpaceHeaterTypeEnum mPredefinedType = IfcSpaceHeaterTypeEnum.NOTDEFINED;// : IfcSpaceHeaterExchangerEnum; 
		public IfcSpaceHeaterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSpaceHeaterType() : base() { }
		internal IfcSpaceHeaterType(IfcDistributionFlowElementType basis) : base(basis) { }
		internal IfcSpaceHeaterType(DatabaseIfc db, IfcSpaceHeaterType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcSpaceHeaterType(DatabaseIfc m, string name, IfcSpaceHeaterTypeEnum t) : base(m) { Name = name; PredefinedType = t; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcSpaceProgram : IfcControl // DEPRECATED IFC4
	{
		internal string mSpaceProgramIdentifier;// : IfcIdentifier;
		internal double mMaxRequiredArea, mMinRequiredArea;// : OPTIONAL IfcAreaMeasure;
		internal int mRequestedLocation;// : OPTIONAL IfcSpatialStructureElement;
		internal double mStandardRequiredArea;// : IfcAreaMeasure; 
		public IfcSpatialStructureElement RequestedLocation { get { return mDatabase[mRequestedLocation] as IfcSpatialStructureElement; } set { mRequestedLocation = value == null ? 0 : value.mIndex; } }
		internal IfcSpaceProgram() : base() { }
		internal IfcSpaceProgram(DatabaseIfc db, IfcSpaceProgram p, DuplicateOptions options) : base(db, p, options)
		{
			mSpaceProgramIdentifier = p.mSpaceProgramIdentifier;
			mMaxRequiredArea = p.mMaxRequiredArea;
			mMinRequiredArea = p.mMinRequiredArea;
			if(p.mRequestedLocation > 0)
				RequestedLocation = db.Factory.Duplicate( p.RequestedLocation) as IfcSpatialStructureElement;
			mStandardRequiredArea = p.mStandardRequiredArea;
		}
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcSpaceThermalLoadProperties // DEPRECATED IFC4
	[Serializable]
	public partial class IfcSpaceType : IfcSpatialStructureElementType
	{
		internal IfcSpaceTypeEnum mPredefinedType = IfcSpaceTypeEnum.NOTDEFINED;
		public IfcSpaceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSpaceType() : base() { }
		internal IfcSpaceType(DatabaseIfc db, IfcSpaceType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcSpaceType(DatabaseIfc db, string name, IfcSpaceTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcSpatialElement : IfcProduct, IfcInterferenceSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcExternalSpatialStructureElement ,IfcSpatialStructureElement ,IfcSpatialZone))
	{
		private string mLongName = "$";// : OPTIONAL IfcLabel; 
		//INVERSE
		internal SET<IfcRelContainedInSpatialStructure> mContainsElements = new SET<IfcRelContainedInSpatialStructure>();// : SET [0:?] OF IfcRelReferencedInSpatialStructure FOR RelatingStructure;
		internal SET<IfcRelServicesBuildings> mServicedBySystems = new SET<IfcRelServicesBuildings>();// : SET [0:?] OF IfcRelServicesBuildings FOR RelatedBuildings;	
		internal SET<IfcRelReferencedInSpatialStructure> mReferencesElements = new SET<IfcRelReferencedInSpatialStructure>();// : SET [0:?] OF IfcRelReferencedInSpatialStructure FOR RelatingStructure;
		internal SET<IfcRelInterferesElements> mIsInterferedByElements = new SET<IfcRelInterferesElements>();//	 :	SET OF IfcRelInterferesElements FOR RelatedElement;
		internal SET<IfcRelInterferesElements> mInterferesElements = new SET<IfcRelInterferesElements>();// :	SET OF IfcRelInterferesElements FOR RelatingElement;

		public string LongName { get { return (mLongName == "$" ? "" : ParserIfc.Decode(mLongName)); } set { mLongName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public SET<IfcRelContainedInSpatialStructure> ContainsElements { get { return mContainsElements; } set { mContainsElements.Clear(); if (value != null) { mContainsElements.CollectionChanged -= mContainsElements_CollectionChanged; mContainsElements = value; mContainsElements.CollectionChanged += mContainsElements_CollectionChanged; } } }
		public SET<IfcRelServicesBuildings> ServicedBySystems { get { return mServicedBySystems; } }
		public SET<IfcRelReferencedInSpatialStructure> ReferencesElements { get { return mReferencesElements; } }
		public SET<IfcRelInterferesElements> IsInterferedByElements { get { return mIsInterferedByElements; } }
		public SET<IfcRelInterferesElements> InterferesElements { get { return mInterferesElements; } }

		protected IfcSpatialElement() : base() { }
		protected IfcSpatialElement(DatabaseIfc db) : base(db) { }
		protected IfcSpatialElement(DatabaseIfc db, IfcSpatialElement e, DuplicateOptions options) : base(db, e, options)
		{
			mLongName = e.mLongName;
			if (options.DuplicateDownstream)
			{
				foreach (IfcRelContainedInSpatialStructure css in e.ContainsElements)
					db.Factory.Duplicate(css, options);
				DuplicateOptions optionsNoHost = new DuplicateOptions(options) { DuplicateHost = false };
				foreach(IfcSystem system in e.ServicedBySystems.Select(x=>x.RelatingSystem))
				{
					IfcSystem sys = db.Factory.Duplicate(system, optionsNoHost) as IfcSystem;
					if(system.ServicesBuildings == null)
					{
						new IfcRelServicesBuildings(sys, this);
					}
				}
			}
		}
		protected IfcSpatialElement(IfcSpatialElement host, string name) : this(new IfcLocalPlacement(host.ObjectPlacement, new IfcAxis2Placement3D(new IfcCartesianPoint(host.mDatabase, 0, 0, 0))))
		{
			Name = name;
			host.AddAggregated(this);
		}
		protected IfcSpatialElement(IfcObjectPlacement placement) : base(placement) { }
		protected IfcSpatialElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		protected override void initialize()
		{
			base.initialize();

			mContainsElements.CollectionChanged += mContainsElements_CollectionChanged;
		}

		private void mContainsElements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelContainedInSpatialStructure r in e.NewItems)
				{
					if(r != null && r.RelatingStructure != this)
						r.RelatingStructure = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelContainedInSpatialStructure r in e.OldItems)
					r.RelatingStructure = null;
			}
		}
		
		internal bool addGrid(IfcGrid g)
		{
			if (mContainsElements.Count == 0)
				new IfcRelContainedInSpatialStructure(this);
			if (mContainsElements.First().RelatedElements.Contains(g))
				return false;
			ContainsElements.First().RelatedElements.Add(g);
			return true;
		}
		
		protected override List<T> Extract<T>(Type type) 
		{
			List<T> result = new List<T>();
			if (typeof(IfcSpatialElement).IsAssignableFrom(type))
			{
				result.AddRange(base.Extract<T>(type));
				foreach(IfcSpatialElement spatial in mContainsElements.SelectMany(x=>x.RelatedElements).OfType<IfcSpatialElement>())
					result.AddRange(spatial.Extract<T>());
				return result;
			}
			foreach (IfcRelServicesBuildings servicesBuildings in mServicedBySystems)
			{
				if (servicesBuildings.RelatingSystem != null)
					result.AddRange(servicesBuildings.RelatingSystem.Extract<T>());
			}
			result.AddRange(base.Extract<T>(type));
			foreach (IfcProduct p in mContainsElements.SelectMany(x => x.RelatedElements))
				result.AddRange(p.Extract<T>());
			return result;
		}

		public override IfcStructuralAnalysisModel CreateOrFindStructAnalysisModel()
		{
			IfcStructuralAnalysisModel result = FindStructAnalysisModel(false);
			if (result != null)
				return result;
			if (mDecomposes != null)
			{
				IfcObjectDefinition od = mDecomposes.RelatingObject;
				IfcSite s = od as IfcSite;
				IfcProject p = od as IfcProject;
				if (s == null && p == null)
					return od.CreateOrFindStructAnalysisModel();
			}
			return new IfcStructuralAnalysisModel(this, Name, IfcAnalysisModelTypeEnum.LOADING_3D) { Description = Description, SharedPlacement = ObjectPlacement };
		}
		public override IfcStructuralAnalysisModel FindStructAnalysisModel(bool strict)
		{
			IfcStructuralAnalysisModel result = mServicedBySystems.Select(x => x.RelatingSystem).OfType<IfcStructuralAnalysisModel>().FirstOrDefault();
			if (result != null)
				return result;
			if (!strict && mDecomposes != null)
				return mDecomposes.RelatingObject.FindStructAnalysisModel(false);
			return null;
		}
		public void ReferenceElement(IfcSpatialReferenceSelect element)
		{
			if (mReferencesElements.Count > 0)
				mReferencesElements.First().RelatedElements.Add(element);
			else
			{
				new IfcRelReferencedInSpatialStructure(element, this);
			}

		}
	}
	[Serializable]
	public abstract partial class IfcSpatialElementType : IfcTypeProduct //IFC4 ABSTRACT SUPERTYPE OF(IfcSpatialStructureElementType)
	{
		internal string mElementType = "$";// : OPTIONAL IfcLabel
		public string ElementType { get { return (mElementType == "$" ? "" : ParserIfc.Decode(mElementType)); } set { mElementType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcSpatialElementType() : base() { }
		protected IfcSpatialElementType(DatabaseIfc db) : base(db) { }
		protected IfcSpatialElementType(DatabaseIfc db, IfcSpatialElementType t, DuplicateOptions options) : base(db, t, options) { mElementType = t.mElementType; }
	}
	public interface IfcSpatialReferenceSelect : IBaseClassIfc // SELECT(IfcProduct, IfcSystem);
	{
		SET<IfcRelReferencedInSpatialStructure> ReferencedInStructures { get; }
	}
[Serializable]
	public abstract partial class IfcSpatialStructureElement : IfcSpatialElement /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBuilding ,IfcBuildingStorey ,IfcSite ,IfcSpace, IfcFacility, IfcFacilityPart, IfcCivilStructureElement))*/
	{
		internal IfcElementCompositionEnum mCompositionType = IfcElementCompositionEnum.NOTDEFINED;// : IfcElementCompositionEnum;  IFC4 Optional 
		public IfcElementCompositionEnum CompositionType { get { return mCompositionType; } set { mCompositionType = value; } }

		protected IfcSpatialStructureElement() : base() { }
		protected IfcSpatialStructureElement(DatabaseIfc db) : base(db) { }
		protected IfcSpatialStructureElement(IfcObjectPlacement pl) : base(pl) { if (pl.mDatabase.mRelease <= ReleaseVersion.IFC2x3) mCompositionType = IfcElementCompositionEnum.ELEMENT; }
		protected IfcSpatialStructureElement(IfcSpatialStructureElement host, string name, IfcObjectPlacement pl) : base(host, pl, null) { Name = name; if (pl.mDatabase.mRelease <= ReleaseVersion.IFC2x3) mCompositionType = IfcElementCompositionEnum.ELEMENT; }
		protected IfcSpatialStructureElement(DatabaseIfc db, IfcSpatialElement e, DuplicateOptions options) : base(db, e, options) { }
		protected IfcSpatialStructureElement(DatabaseIfc db, IfcSpatialStructureElement e, DuplicateOptions options) : base(db, e, options) { mCompositionType = e.mCompositionType; }
		protected IfcSpatialStructureElement(IfcSpatialStructureElement host, string name) : base(host,name) { if (mDatabase.mRelease < ReleaseVersion.IFC4) mCompositionType = IfcElementCompositionEnum.ELEMENT; }
		protected IfcSpatialStructureElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcSpatialStructureElementType : IfcSpatialElementType //ABSTRACT SUPERTYPE OF (ONEOF (IfcSpaceType))
	{
		protected IfcSpatialStructureElementType() : base() { }
		protected IfcSpatialStructureElementType(DatabaseIfc db) : base(db) { }
		protected IfcSpatialStructureElementType(DatabaseIfc db, IfcSpatialStructureElementType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcSpatialZone : IfcSpatialElement  //IFC4
	{
		internal IfcSpatialZoneTypeEnum mPredefinedType = IfcSpatialZoneTypeEnum.NOTDEFINED;
		public IfcSpatialZoneTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		protected IfcSpatialZone() : base() { }
		protected IfcSpatialZone(DatabaseIfc db, IfcSpatialZone p, DuplicateOptions options) : base(db, p, options) { mPredefinedType = p.mPredefinedType; }
		public IfcSpatialZone(DatabaseIfc db, string name) : base(db.Factory.RootPlacement) { Name = name; }
		public IfcSpatialZone(IfcSpatialElement host, string name) : base(host, name) { if (mDatabase.mRelease < ReleaseVersion.IFC4) throw new Exception("IFCSpatial Zone only valid in IFC4 or newer!"); }
	}
	[Serializable]
	public partial class IfcSpatialZoneType : IfcSpatialElementType  //IFC4
	{
		internal IfcSpatialZoneTypeEnum mPredefinedType = IfcSpatialZoneTypeEnum.NOTDEFINED;
		public IfcSpatialZoneTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSpatialZoneType() : base() { }
		internal IfcSpatialZoneType(DatabaseIfc db, IfcSpatialZoneType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcSpatialZoneType(DatabaseIfc m, string name, IfcSpatialZoneTypeEnum t) : base(m) { Name = name; PredefinedType = t; if (mDatabase.mRelease < ReleaseVersion.IFC4) throw new Exception("IFCSpatial Zone Type only valid in IFC4 or newer!"); }
	}
	public interface IfcSpecularHighlightSelect : IBaseClassIfc { } //SELECT ( IfcSpecularExponent, IfcSpecularRoughness);
	[Serializable]
	public partial class IfcSphere : IfcCsgPrimitive3D
	{
		internal double mRadius;// : IfcPositiveLengthMeasure; 
		public double Radius { get { return mRadius; } set { mRadius = value; } }

		internal IfcSphere() : base() { }
		internal IfcSphere(DatabaseIfc db, IfcSphere s, DuplicateOptions options) : base(db, s, options) { mRadius = s.mRadius; }
		public IfcSphere(IfcAxis2Placement3D position, double radius) : base(position) { Radius = radius; }
	}
	[Serializable]
	public partial class IfcSphericalSurface : IfcElementarySurface //IFC4.2
	{
		internal double mRadius;// : IfcPositiveLengthMeasure; 
		public double Radius { get { return mRadius; } set { mRadius = value; } }
		internal IfcSphericalSurface() : base() { }
		internal IfcSphericalSurface(DatabaseIfc db, IfcSphericalSurface s, DuplicateOptions options) : base(db, s, options) { mRadius = s.mRadius; }
		public IfcSphericalSurface(IfcAxis2Placement3D placement, double radius) : base(placement) { Radius = radius; }
	}
	[Serializable]
	public abstract partial class IfcSpiral : IfcCurve
	{
		private IfcAxis2Placement mPosition = null; //: IfcAxis2Placement;
		public IfcAxis2Placement Position { get { return mPosition; } set { mPosition = value; } }

		public IfcSpiral() : base() { }
		internal IfcSpiral(DatabaseIfc db, IfcSpiral spiral, DuplicateOptions options)
			: base(db, spiral, options) { Position = db.Factory.Duplicate(spiral.Position as BaseClassIfc, options) as IfcAxis2Placement; }
		public IfcSpiral(IfcAxis2Placement position)
			: base(position.Database) { Position = position; }
	}
	[Serializable]
	public partial class IfcStackTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcStackTerminalTypeEnum mPredefinedType = IfcStackTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcStackTerminalTypeEnum;
		public IfcStackTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStackTerminal() : base() { }
		internal IfcStackTerminal(DatabaseIfc db, IfcStackTerminal t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcStackTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcStackTerminalType : IfcFlowTerminalType
	{
		internal IfcStackTerminalTypeEnum mPredefinedType = IfcStackTerminalTypeEnum.NOTDEFINED;
		public IfcStackTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStackTerminalType() : base() { }
		internal IfcStackTerminalType(DatabaseIfc db, IfcStackTerminalType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcStackTerminalType(DatabaseIfc m, string name, IfcStackTerminalTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcStair : IfcBuiltElement
	{
		internal IfcStairTypeEnum mPredefinedType = IfcStairTypeEnum.NOTDEFINED;// OPTIONAL : IfcStairTypeEnum
		public IfcStairTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStair() : base() { }
		internal IfcStair(DatabaseIfc db, IfcStair s, DuplicateOptions options) : base(db, s, options) { mPredefinedType = s.mPredefinedType; }
		public IfcStair(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcStairFlight : IfcBuiltElement
	{
		internal int mNumberOfRiser = int.MinValue;//	:	OPTIONAL INTEGER;
		internal int mNumberOfTreads = int.MinValue;//	:	OPTIONAL INTEGER;
		internal double mRiserHeight = double.NaN;//	:	OPTIONAL IfcPositiveLengthMeasure;
		internal double mTreadLength = double.NaN;//	:	OPTIONAL IfcPositiveLengthMeasure;
		internal IfcStairFlightTypeEnum mPredefinedType = IfcStairFlightTypeEnum.NOTDEFINED;//: OPTIONAL IfcStairFlightTypeEnum; IFC4

		public IfcStairFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStairFlight() : base() { }
		internal IfcStairFlight(DatabaseIfc db, IfcStairFlight f, DuplicateOptions options) : base(db, f, options) { mNumberOfRiser = f.mNumberOfRiser; mNumberOfTreads = f.mNumberOfTreads; mRiserHeight = f.mRiserHeight; mTreadLength = f.mTreadLength; mPredefinedType = f.mPredefinedType; }
		public IfcStairFlight(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcStairFlightType : IfcBuiltElementType
	{
		internal IfcStairFlightTypeEnum mPredefinedType = IfcStairFlightTypeEnum.NOTDEFINED;
		public IfcStairFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStairFlightType() : base() { }
		internal IfcStairFlightType(DatabaseIfc db, IfcStairFlightType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcStairFlightType(DatabaseIfc m, string name, IfcStairFlightTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcStairType : IfcBuiltElementType
	{
		internal IfcStairTypeEnum mPredefinedType = IfcStairTypeEnum.NOTDEFINED;
		public IfcStairTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStairType() : base() { }
		internal IfcStairType(DatabaseIfc db, IfcStairType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcStairType(DatabaseIfc m, string name, IfcStairTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcStructuralAction : IfcStructuralActivity // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveAction, IfcStructuralPointAction, IfcStructuralSurfaceAction))
	{
		private IfcLogicalEnum mDestabilizingLoad = IfcLogicalEnum.UNKNOWN;//: OPTIONAL BOOLEAN; IFC4 made optional
		internal int mCausedBy;// : OPTIONAL IfcStructuralReaction; DELETED IFC4 

		public bool DestabilizingLoad { get { return mDestabilizingLoad == IfcLogicalEnum.TRUE; } set { mDestabilizingLoad = value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE; } }
		public IfcStructuralReaction CausedBy { get { return mDatabase[mCausedBy] as IfcStructuralReaction; } set { mCausedBy = (value == null ? 0 : value.mIndex); } }

		protected IfcStructuralAction() : base() { }
		protected IfcStructuralAction(DatabaseIfc db, IfcStructuralAction a, DuplicateOptions options) : base(db,a, options)
		{
			mDestabilizingLoad = a.mDestabilizingLoad;
			if(a.mCausedBy > 0)
				CausedBy = a.CausedBy;
		}
		protected IfcStructuralAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item,  IfcStructuralLoad load, IfcTopologyRepresentation location, bool global)
			: base(item, load, global, lc.IsGroupedBy.Count == 0 ? new IfcRelAssignsToGroup(lc) : lc.IsGroupedBy.First())
		{
			if (location != null)
			{
				if (lc.mLoadGroupFor.Count > 0)
					ObjectPlacement = lc.mLoadGroupFor[0].SharedPlacement;
				Representation = new IfcProductDefinitionShape(location);
			}
		}
	}
	[Serializable]
	public abstract partial class IfcStructuralActivity : IfcProduct //ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralAction, IfcStructuralReaction))
	{
		private int mAppliedLoad;// : IfcStructuralLoad;
		private IfcGlobalOrLocalEnum mGlobalOrLocal = IfcGlobalOrLocalEnum.GLOBAL_COORDS;// : IfcGlobalOrLocalEnum; 
		//INVERSE 
		private IfcRelConnectsStructuralActivity mAssignedToStructuralItem = null; // : SET [0:1] OF IfcRelConnectsStructuralActivity FOR RelatedStructuralActivity; 

		public IfcStructuralLoad AppliedLoad { get { return mDatabase[mAppliedLoad] as IfcStructuralLoad; } set { mAppliedLoad = value.mIndex; } }
		public IfcGlobalOrLocalEnum GlobalOrLocal { get { return mGlobalOrLocal; } set { mGlobalOrLocal = value; } }
		public IfcRelConnectsStructuralActivity AssignedToStructuralItem { get { return mAssignedToStructuralItem; } set { mAssignedToStructuralItem = value; } }

		protected IfcStructuralActivity() : base() { }
		protected IfcStructuralActivity(IfcStructuralLoad appliedLoad, IfcGlobalOrLocalEnum globalOrLocal)
			: base(appliedLoad.Database)
		{
			AppliedLoad = appliedLoad;
			GlobalOrLocal = globalOrLocal;
		}
		protected IfcStructuralActivity(DatabaseIfc db, IfcStructuralActivity a, DuplicateOptions options) : base(db, a, options)
		{
			AppliedLoad = db.Factory.Duplicate(a.AppliedLoad) as IfcStructuralLoad;
			mGlobalOrLocal = a.mGlobalOrLocal;
		}
		protected IfcStructuralActivity(IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, bool global, IfcRelAssignsToGroup loadcase) : base(load.mDatabase)
		{
			if(item != null)
				mAssignedToStructuralItem = new IfcRelConnectsStructuralActivity(item, this);
			mAppliedLoad = load.mIndex;
			mGlobalOrLocal = global ? IfcGlobalOrLocalEnum.GLOBAL_COORDS : IfcGlobalOrLocalEnum.LOCAL_COORDS;
			loadcase.RelatedObjects.Add(this);
		}
	}
	public interface IfcStructuralActivityAssignmentSelect : IBaseClassIfc { void AssignStructuralActivity(IfcRelConnectsStructuralActivity connects); } //SELECT(IfcStructuralItem,IfcElement);
	[Serializable]
	public partial class IfcStructuralAnalysisModel : IfcSystem
	{
		internal IfcAnalysisModelTypeEnum mPredefinedType = IfcAnalysisModelTypeEnum.NOTDEFINED;// : IfcAnalysisModelTypeEnum;
		internal int mOrientationOf2DPlane;// : OPTIONAL IfcAxis2Placement3D;
		internal List<int> mLoadedBy = new List<int>();//  : OPTIONAL SET [1:?] OF IfcStructuralLoadGroup;
		internal List<int> mHasResults = new List<int>();//: OPTIONAL SET [1:?] OF IfcStructuralResultGroup  
		internal IfcObjectPlacement mSharedPlacement;//	:	OPTIONAL IfcObjectPlacement;

		public IfcAnalysisModelTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcAxis2Placement3D OrientationOf2DPlane { get { return mDatabase[mOrientationOf2DPlane] as IfcAxis2Placement3D; } set { mOrientationOf2DPlane = value == null ? 0 : value.mIndex;  } }
		public ReadOnlyCollection<IfcStructuralLoadGroup> LoadedBy { get { return new ReadOnlyCollection<IfcStructuralLoadGroup>(mLoadedBy.ConvertAll(x => mDatabase[x] as IfcStructuralLoadGroup)); } }
		public ReadOnlyCollection<IfcStructuralResultGroup> HasResults { get { return new ReadOnlyCollection<IfcStructuralResultGroup>( mHasResults.ConvertAll(x => mDatabase[x] as IfcStructuralResultGroup)); } } 
		public IfcObjectPlacement SharedPlacement { get { return mSharedPlacement; } set { mSharedPlacement = value; } }

		internal IfcStructuralAnalysisModel() : base() { }
		internal IfcStructuralAnalysisModel(DatabaseIfc db, IfcStructuralAnalysisModel m, DuplicateOptions options) : base(db, m, options)
		{
			mPredefinedType = m.mPredefinedType;
			if(m.mOrientationOf2DPlane > 0)
				OrientationOf2DPlane = db.Factory.Duplicate(m.OrientationOf2DPlane) as IfcAxis2Placement3D;
			m.LoadedBy.ToList().ForEach(x => addLoadGroup( db.Factory.Duplicate(x) as IfcStructuralLoadGroup));
			m.HasResults.ToList().ForEach(x => addResultGroup( db.Factory.Duplicate(x) as IfcStructuralResultGroup));
			if(m.mSharedPlacement != null)
				SharedPlacement = db.Factory.Duplicate(m.SharedPlacement) as IfcObjectPlacement;
		}
		public IfcStructuralAnalysisModel(IfcSpatialElement facility, string name, IfcAnalysisModelTypeEnum type) : base(facility, name)
		{
			mPredefinedType = type;
			if (mDatabase.Release < ReleaseVersion.IFC4)
				SharedPlacement = facility.ObjectPlacement;
			else
				SharedPlacement = new IfcLocalPlacement(facility.ObjectPlacement, mDatabase.Factory.XYPlanePlacement); 
		}

		internal void addLoadGroup(IfcStructuralLoadGroup lg) { mLoadedBy.Add(lg.mIndex); lg.mLoadGroupFor.Add(this); }
		internal void addResultGroup(IfcStructuralResultGroup lg) { mHasResults.Add(lg.mIndex); lg.mResultGroupFor = this; }

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcStructuralLoadGroup lg in LoadedBy)
				result.AddRange(lg.Extract<T>());
			foreach (IfcStructuralResultGroup lg in HasResults)
				result.AddRange(lg.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public abstract partial class IfcStructuralConnection : IfcStructuralItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveConnection ,IfcStructuralPointConnection ,IfcStructuralSurfaceConnection))
	{
		internal int mAppliedCondition = 0; //: OPTIONAL IfcBoundaryCondition
		//INVERSE
		internal List<IfcRelConnectsStructuralMember> mConnectsStructuralMembers = new List<IfcRelConnectsStructuralMember>();//	 :	SET [1:?] OF IfcRelConnectsStructuralMember FOR RelatedStructuralConnection;

		public IfcBoundaryCondition AppliedCondition { get { return mDatabase[mAppliedCondition] as IfcBoundaryCondition; } set { mAppliedCondition = (value == null ? 0 : value.mIndex); } }
		public ReadOnlyCollection<IfcRelConnectsStructuralMember> ConnectsStructuralMembers { get { return new ReadOnlyCollection<IfcRelConnectsStructuralMember>(mConnectsStructuralMembers); } }

		protected IfcStructuralConnection() : base() { }
		protected IfcStructuralConnection(DatabaseIfc db, IfcStructuralConnection c, DuplicateOptions options) : base(db, c, options) { if(c.mAppliedCondition > 0) AppliedCondition = db.Factory.Duplicate(c.AppliedCondition) as IfcBoundaryCondition; }
		protected IfcStructuralConnection(IfcStructuralAnalysisModel sm) : base(sm) {  }
	}
	[Serializable]
	public abstract partial class IfcStructuralConnectionCondition : BaseClassIfc, NamedObjectIfc //ABSTRACT SUPERTYPE OF (ONEOF (IfcFailureConnectionCondition ,IfcSlippageConnectionCondition));
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcStructuralConnectionCondition() : base() { }
		protected IfcStructuralConnectionCondition(DatabaseIfc db) : base(db) { }
		protected IfcStructuralConnectionCondition(DatabaseIfc db, IfcStructuralConnectionCondition c) : base(db,c) { mName = c.mName; }
	}
	[Serializable]
	public partial class IfcStructuralCurveAction : IfcStructuralAction // IFC4 SUPERTYPE OF(IfcStructuralLinearAction)
	{
		internal IfcProjectedOrTrueLengthEnum mProjectedOrTrue = IfcProjectedOrTrueLengthEnum.TRUE_LENGTH;// : IfcProjectedOrTrueLengthEnum
		internal IfcStructuralCurveActivityTypeEnum mPredefinedType = IfcStructuralCurveActivityTypeEnum.NOTDEFINED;//IfcStructuralCurveActivityTypeEnum

		public IfcProjectedOrTrueLengthEnum ProjectedOrTrue { get { return mProjectedOrTrue; } set { mProjectedOrTrue = value; } }
		public IfcStructuralCurveActivityTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStructuralCurveAction() : base() { }
		internal IfcStructuralCurveAction(DatabaseIfc db, IfcStructuralCurveAction a, DuplicateOptions options) : base(db,a, options) { mProjectedOrTrue = a.mProjectedOrTrue; mPredefinedType = a.mPredefinedType; }
		public IfcStructuralCurveAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadSingleForce force, double length, bool global)
			: base(lc, member, new IfcStructuralLoadConfiguration(force, length), null, global) { mPredefinedType = IfcStructuralCurveActivityTypeEnum.DISCRETE; }
		public IfcStructuralCurveAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoad load, bool global, bool projected, IfcStructuralCurveActivityTypeEnum type)
			: base(lc, member, load,null, global) { ProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcStructuralCurveConnection : IfcStructuralConnection
	{
		internal IfcStructuralCurveConnection() : base() { }
		internal IfcStructuralCurveConnection(DatabaseIfc db, IfcStructuralCurveConnection c, DuplicateOptions options) : base(db, c, options) { }
	}
	[Serializable]
	public partial class IfcStructuralCurveMember : IfcStructuralMember
	{
		internal IfcStructuralCurveMemberTypeEnum mPredefinedType= IfcStructuralCurveMemberTypeEnum.NOTDEFINED;// : IfcStructuralCurveMemberTypeEnum; 
		internal int mAxis; //: IfcDirection

		public IfcStructuralCurveMemberTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcDirection Axis { get { return mDatabase[mAxis] as IfcDirection; } set { mAxis = value.mIndex; } }

		internal IfcEdgeCurve EdgeCurve { get; set; } //Used for load applications in ifc2x3

		public IfcStructuralCurveMember() : base() { }
		internal IfcStructuralCurveMember(DatabaseIfc db, IfcStructuralCurveMember m, DuplicateOptions options) : base(db,m, options) { mPredefinedType = m.mPredefinedType; Axis = db.Factory.Duplicate(m.Axis) as IfcDirection; }
		public IfcStructuralCurveMember(IfcStructuralAnalysisModel sm, IfcStructuralPointConnection A, IfcStructuralPointConnection B, IfcMaterialProfileSetUsage mp, IfcDirection dir, int id)
			: this(sm,A,null,B,null,mp,dir,id) { }
		public IfcStructuralCurveMember(IfcStructuralAnalysisModel sm, IfcStructuralPointConnection A, IfcStructuralPointConnection B, IfcDirection dir, int id)
			: this(sm, new IfcEdgeCurve(A.Vertex,B.Vertex,new IfcPolyline(A.Vertex.VertexGeometry as IfcCartesianPoint,B.Vertex.VertexGeometry as IfcCartesianPoint),true),A,null,B,null, null,dir, id) { }
		public IfcStructuralCurveMember(IfcStructuralAnalysisModel sm, IfcStructuralPointConnection A, ExtremityAttributes start, IfcStructuralPointConnection B, ExtremityAttributes end, IfcMaterialProfileSetUsage mp, IfcDirection dir, int id)
			: this(sm, new IfcEdgeCurve(A.Vertex,B.Vertex,new IfcPolyline(A.Vertex.VertexGeometry as IfcCartesianPoint,B.Vertex.VertexGeometry as IfcCartesianPoint),true),A,start,B,end, mp,dir, id) { }
		public IfcStructuralCurveMember(IfcStructuralAnalysisModel sm, IfcEdgeCurve e, IfcStructuralPointConnection A, ExtremityAttributes start, IfcStructuralPointConnection B, ExtremityAttributes end, IfcMaterialProfileSetUsage mp, IfcDirection dir,int id)
			: base(sm, mp ,id)
		{
			ObjectPlacement = sm.SharedPlacement;
			EdgeCurve = e;
			Representation = new IfcProductDefinitionShape(new IfcTopologyRepresentation(sm.mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis),e));
			IfcRelConnectsStructuralMember csm = IfcRelConnectsStructuralMember.Create(this, A, true, start);
			csm = IfcRelConnectsStructuralMember.Create(this, B, false, end);
			Axis = dir;
		}

		public partial class ExtremityAttributes
		{
			public IfcBoundaryNodeCondition BoundaryCondition { get; set; }
			public IfcStructuralConnectionCondition StructuralConnectionCondition { get; set; }
			public double SupportedLength { get; set; } 
			public Tuple<double, double, double> Eccentricity { get; set; }
			internal IfcAxis2Placement3D ConditionCoordinateSystem { get; set; } 
			public ExtremityAttributes() { SupportedLength = 0; Eccentricity = null; ConditionCoordinateSystem = null; }
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
			public ExtremityAttributes(IfcBoundaryNodeCondition boundary) { BoundaryCondition = boundary; }
		}
	}
	[Serializable]
	public partial class IfcStructuralCurveMemberVarying : IfcStructuralCurveMember
	{
		internal IfcStructuralCurveMemberVarying() : base() { }
		internal IfcStructuralCurveMemberVarying(DatabaseIfc db, IfcStructuralCurveMemberVarying m, DuplicateOptions options) : base(db, m, options) { }
	}
	[Serializable]
	public partial class IfcStructuralCurveReaction : IfcStructuralReaction
	{
		internal IfcStructuralCurveActivityTypeEnum mPredefinedType = IfcStructuralCurveActivityTypeEnum.NOTDEFINED;//: 	IfcStructuralCurveActivityTypeEnum; 
		public IfcStructuralCurveActivityTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStructuralCurveReaction() : base() { }
		internal IfcStructuralCurveReaction(DatabaseIfc db, IfcStructuralCurveReaction r, DuplicateOptions options) : base(db, r, options) { mPredefinedType = r.mPredefinedType; }
	}
	[Serializable]
	public abstract partial class IfcStructuralItem : IfcProduct, IfcStructuralActivityAssignmentSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralConnection ,IfcStructuralMember))
	{
		//INVERSE
		internal List<IfcRelConnectsStructuralActivity> mAssignedStructuralActivity = new List<IfcRelConnectsStructuralActivity>();//: 	SET OF IfcRelConnectsStructuralActivity FOR RelatingElement;
		public ReadOnlyCollection<IfcRelConnectsStructuralActivity> AssignedStructuralActivity { get { return new ReadOnlyCollection<IfcRelConnectsStructuralActivity>(mAssignedStructuralActivity); } }
		protected IfcStructuralItem() : base() { }
		protected IfcStructuralItem(DatabaseIfc db, IfcStructuralItem i, DuplicateOptions options) : base(db, i, options)
		{
			foreach(IfcRelConnectsStructuralActivity rcss in i.mAssignedStructuralActivity)
			{
				IfcRelConnectsStructuralActivity rc = db.Factory.Duplicate(rcss) as IfcRelConnectsStructuralActivity;
				rc.RelatingElement = this;
			}
		}
		protected IfcStructuralItem(IfcStructuralAnalysisModel sm) : base(sm.mDatabase.mRelease < ReleaseVersion.IFC4 ? new IfcLocalPlacement(sm.SharedPlacement,sm.mDatabase.Factory.XYPlanePlacement) : sm.SharedPlacement ,null)
		{
			sm.AddRelated(this);
			mDatabase.mContext.setStructuralUnits();
		}
		protected IfcStructuralItem(IfcStructuralAnalysisModel sm, int id) :this(sm)
		{
			if (id >= 0)
				Name = id.ToString();
		}
		public void AssignStructuralActivity(IfcRelConnectsStructuralActivity connects) { mAssignedStructuralActivity.Add(connects); }
	}
	[Serializable]
	public partial class IfcStructuralLinearAction : IfcStructuralCurveAction 
	{
		internal IfcStructuralLinearAction() : base() { }
		internal IfcStructuralLinearAction(DatabaseIfc db, IfcStructuralLinearAction a, DuplicateOptions options) : base(db, a, options) {  }
		public IfcStructuralLinearAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadTemperature load)
			: this(lc, member, load, true,false, IfcStructuralCurveActivityTypeEnum.CONST) { }
		public IfcStructuralLinearAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadLinearForce load, bool global, bool projected) 
			: this(lc, member, load, global, projected, IfcStructuralCurveActivityTypeEnum.CONST) { }
		
		// SurfaceMembers to be added
		protected IfcStructuralLinearAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadStatic load, bool global, bool projected, IfcStructuralCurveActivityTypeEnum activity) 
			: base(lc, member, load, global, projected, activity)
		{
			if(mDatabase.mRelease < ReleaseVersion.IFC4)
			{
				Representation = new IfcProductDefinitionShape(new IfcTopologyRepresentation(mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Reference), member.EdgeCurve));
			}
		}
	}
	[Serializable]
	public partial class IfcStructuralLinearActionVarying : IfcStructuralLinearAction // DELETED IFC4
	{
		internal int mVaryingAppliedLoadLocation;// : IfcShapeAspect;
		internal List<int> mSubsequentAppliedLoads = new List<int>();//: LIST [1:?] OF IfcStructuralLoad; 

		public IfcShapeAspect VaryingAppliedLoadLocation { get { return mDatabase[mVaryingAppliedLoadLocation] as IfcShapeAspect; } set { mVaryingAppliedLoadLocation = value.mIndex; } }
		public ReadOnlyCollection<IfcStructuralLoad> SubsequentAppliedLoads { get { return new ReadOnlyCollection<IfcStructuralLoad>( mSubsequentAppliedLoads.ConvertAll(x => mDatabase[x] as IfcStructuralLoad)); } }

		internal IfcStructuralLinearActionVarying() : base() { }
		internal IfcStructuralLinearActionVarying(DatabaseIfc db, IfcStructuralLinearActionVarying a, DuplicateOptions options) : base(db, a, options)
		{
			VaryingAppliedLoadLocation = db.Factory.Duplicate( a.VaryingAppliedLoadLocation) as IfcShapeAspect;
			a.SubsequentAppliedLoads.ToList().ForEach(x=>addSubsequentLoad(db.Factory.Duplicate(x) as IfcStructuralLoad));
		}
		public IfcStructuralLinearActionVarying(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadLinearForce start, IfcStructuralLoadLinearForce end, bool global, bool projected) 
			: base(lc, member, start, global, projected, IfcStructuralCurveActivityTypeEnum.LINEAR)
		{
			if (mDatabase.mRelease != ReleaseVersion.IFC2x3)
				throw new Exception(this.StepClassName + " deleted in IFC4");
			IfcCurve curve = member.EdgeCurve.EdgeGeometry;
			List<IfcShapeModel> aspects = new List<IfcShapeModel>();
			aspects.Add( new IfcShapeRepresentation(new IfcPointOnCurve(mDatabase,curve, 0)));
			aspects.Add(new IfcShapeRepresentation(new IfcPointOnCurve(mDatabase, curve, 1)));
			VaryingAppliedLoadLocation = new IfcShapeAspect(aspects);
		}

		internal void addSubsequentLoad(IfcStructuralLoad load) { mSubsequentAppliedLoads.Add(load.mIndex); }
	}
	[Serializable]
	public abstract partial class IfcStructuralLoad : BaseClassIfc, NamedObjectIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralLoadConfiguration, IfcStructuralLoadOrResult));
	{
		internal string mName = "$";// : OPTIONAL IfcLabel
		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcStructuralLoad() : base() { }
		protected IfcStructuralLoad(DatabaseIfc db) : base(db) { }
		protected IfcStructuralLoad(DatabaseIfc db, IfcStructuralLoad l) : base(db) { mName = l.mName; }
	}
	[Serializable]
	public partial class IfcStructuralLoadConfiguration : IfcStructuralLoad //IFC4
	{
		internal List<int> mValues = new List<int>();//	 :	LIST [1:?] OF IfcStructuralLoadOrResult;
		internal List<List<double>> mLocations = new List<List<double>>();//	 :	OPTIONAL LIST [1:?] OF UNIQUE LIST [1:2] OF IfcLengthMeasure; 

		public ReadOnlyCollection<IfcStructuralLoadOrResult> Values { get { return new ReadOnlyCollection<IfcStructuralLoadOrResult>( mValues.ConvertAll(x => (mDatabase[x] as IfcStructuralLoadOrResult))); } }
		public ReadOnlyCollection<ReadOnlyCollection<double>> Locations { get { return new ReadOnlyCollection<ReadOnlyCollection<double>>( mLocations.ConvertAll(x=>new ReadOnlyCollection<double>(x))); } }

		internal IfcStructuralLoadConfiguration() : base() { }
		internal IfcStructuralLoadConfiguration(DatabaseIfc db, IfcStructuralLoadConfiguration p) : base(db,p)
		{
			p.Values.ToList().ForEach(x=>addValue( db.Factory.Duplicate(x) as IfcStructuralLoadOrResult));
			mLocations.AddRange(p.mLocations);
		}
		public IfcStructuralLoadConfiguration(IfcStructuralLoadOrResult val, double length)
			: base(val.mDatabase) { mValues.Add(val.mIndex); mLocations.Add( new List<double>() { length } );  }
		public IfcStructuralLoadConfiguration(List<IfcStructuralLoadOrResult> vals, List<List<double>> lengths)
			: base(vals[0].mDatabase) { mValues = vals.ConvertAll(x => x.mIndex); if (lengths != null) mLocations = lengths; }
		public IfcStructuralLoadConfiguration(IfcStructuralLoadOrResult val1, double loc1, IfcStructuralLoadOrResult val2, double loc2)
			: base(val1.mDatabase) { mValues = new List<int>() { val1.mIndex, val2.mIndex }; mLocations = new List<List<double>>() { new List<double>() { loc1 }, new List<double>() { loc2 } }; }
		
		internal void addValue(IfcStructuralLoadOrResult value) { mValues.Add(value.mIndex); }
	}
	[Serializable]
	public abstract partial class IfcStructuralLoadOrResult : IfcStructuralLoad // ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralLoadStatic, IfcSurfaceReinforcementArea))
	{
		protected IfcStructuralLoadOrResult() : base() { }
		protected IfcStructuralLoadOrResult(DatabaseIfc db) : base(db) { }
		protected IfcStructuralLoadOrResult(DatabaseIfc db, IfcStructuralLoadOrResult l) : base(db,l) { }
	}
	[Serializable]
	public partial class IfcStructuralLoadCase : IfcStructuralLoadGroup //IFC4
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcStructuralLoadGroup" : base.StepClassName); } }

		internal Tuple<double,double,double> mSelfWeightCoefficients = null;// : OPTIONAL LIST [3:3] OF IfcRatioMeasure; 
		public Tuple<double,double,double> SelfWeightCoefficients { get { return mSelfWeightCoefficients; } set { mSelfWeightCoefficients = value; } }

		internal IfcStructuralLoadCase() : base() { }
		internal IfcStructuralLoadCase(DatabaseIfc db, IfcStructuralLoadCase c, DuplicateOptions options) : base(db, c, options) { mSelfWeightCoefficients = c.mSelfWeightCoefficients; }
		public IfcStructuralLoadCase(IfcStructuralAnalysisModel model, string name)
			: base(model, name, IfcLoadGroupTypeEnum.LOAD_CASE) { new IfcRelAssignsToGroup(this) { Name = Name + " Actions", Description = Description }; }
	}
	[Serializable]
	public partial class IfcStructuralLoadGroup : IfcGroup
	{
		internal IfcLoadGroupTypeEnum mPredefinedType = IfcLoadGroupTypeEnum.NOTDEFINED;// : IfcLoadGroupTypeEnum;
		internal IfcActionTypeEnum mActionType = IfcActionTypeEnum.NOTDEFINED;// : IfcActionTypeEnum;
		internal IfcActionSourceTypeEnum mActionSource = IfcActionSourceTypeEnum.NOTDEFINED;//: IfcActionSourceTypeEnum;
		internal double mCoefficient = double.NaN;//: OPTIONAL IfcRatioMeasure;
		internal string mPurpose = "$";// : OPTIONAL IfcLabel; 
		//INVERSE 
		internal IfcStructuralResultGroup mSourceOfResultGroup = null;// :	SET [0:1] OF IfcStructuralResultGroup FOR ResultForLoadGroup;
		internal List<IfcStructuralAnalysisModel> mLoadGroupFor = new List<IfcStructuralAnalysisModel>();//	 :	SET [0:?] OF IfcStructuralAnalysisModel 

		public IfcLoadGroupTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcActionTypeEnum ActionType { get { return mActionType; } set { mActionType = value; } }
		public IfcActionSourceTypeEnum ActionSource { get { return mActionSource; } set { mActionSource = value; } }
		public double Coefficient { get { return mCoefficient; } set { mCoefficient = value; } }
		public string Purpose { get { return (mPurpose == "$" ? "" : ParserIfc.Decode(mPurpose)); } set { mPurpose = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcStructuralLoadGroup() : base() { }
		internal IfcStructuralLoadGroup(DatabaseIfc db, IfcStructuralLoadGroup g, DuplicateOptions options) : base(db, g, options) { mPredefinedType = g.mPredefinedType; mActionType = g.mActionType; mActionSource = g.mActionSource; mCoefficient = g.mCoefficient; mPurpose = g.mPurpose; }
		public IfcStructuralLoadGroup(IfcStructuralAnalysisModel sm, string name, IfcLoadGroupTypeEnum type)
			: base(sm.mDatabase, name) { mLoadGroupFor.Add(sm); sm.addLoadGroup(this); mPredefinedType = type; }
		public IfcStructuralLoadGroup(IfcStructuralAnalysisModel sm, string name, List<double> factors, List<IfcStructuralLoadGroup> cases, bool ULS)
			: base(sm.mDatabase, name)
		{
			mPredefinedType = IfcLoadGroupTypeEnum.LOAD_COMBINATION;
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
						new IfcRelAssignsToGroupByFactor(ods, this, prevfactor);
						ods = new List<IfcObjectDefinition>();
						prevfactor = factor;
					}
					ods.Add(cases[icounter]);
				}
				new IfcRelAssignsToGroupByFactor(ods, this, prevfactor);
			}
			else
			{
				new IfcRelAssignsToGroupByFactor(cases.ConvertAll(x => x as IfcObjectDefinition), this, 1);
			}
		}
		protected override bool DisposeWorker(bool children)
		{
			foreach (IfcStructuralAnalysisModel structuralAnalysisModel in mLoadGroupFor.ToList())
				structuralAnalysisModel.mLoadedBy.Remove(this.Index);
			return base.DisposeWorker(children);
		}
	}
	[Serializable]
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
		internal IfcStructuralLoadLinearForce(DatabaseIfc db, IfcStructuralLoadLinearForce f) : base(db,f) { mLinearForceX = f.mLinearForceX; mLinearForceY = f.mLinearForceY; mLinearForceZ = f.mLinearForceZ; mLinearMomentX = f.mLinearMomentX; mLinearMomentY = f.mLinearMomentY; mLinearMomentZ = f.mLinearMomentZ; }
		public IfcStructuralLoadLinearForce(DatabaseIfc db) : base(db) { }
		public IfcStructuralLoadLinearForce(DatabaseIfc db, double forceX, double forceY, double forceZ, double momentX, double momentY, double momentZ) : base(db) { mLinearForceX = forceX; mLinearForceY = forceY; mLinearForceZ = forceZ; mLinearMomentX = momentX; mLinearMomentY = momentY; mLinearMomentZ = momentZ; }
	}
	[Serializable]
	public partial class IfcStructuralLoadPlanarForce : IfcStructuralLoadStatic
	{
		internal double mPlanarForceX = 0, mPlanarForceY = 0, mPlanarForceZ = 0;// : OPTIONAL IfcPlanarForceMeasure; 
		internal IfcStructuralLoadPlanarForce() : base() { }
		internal IfcStructuralLoadPlanarForce(DatabaseIfc db, IfcStructuralLoadPlanarForce f) : base(db,f) { mPlanarForceX = f.mPlanarForceX; mPlanarForceY = f.mPlanarForceY; mPlanarForceZ = f.mPlanarForceZ; }
		public IfcStructuralLoadPlanarForce(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcStructuralLoadSingleDisplacement : IfcStructuralLoadStatic
	{
		internal double mDisplacementX = 0, mDisplacementY = 0, mDisplacementZ = 0;// : OPTIONAL IfcLengthMeasure;
		internal double mRotationalDisplacementRX = 0, mRotationalDisplacementRY = 0, mRotationalDisplacementRZ = 0;// : OPTIONAL IfcPlaneAngleMeasure; 
		internal IfcStructuralLoadSingleDisplacement() : base() { }
		internal IfcStructuralLoadSingleDisplacement(DatabaseIfc db, IfcStructuralLoadSingleDisplacement d) : base(db,d) { mDisplacementX = d.mDisplacementX; mDisplacementY = d.mDisplacementY; mDisplacementZ = d.mDisplacementZ; mRotationalDisplacementRX = d.mRotationalDisplacementRX; mRotationalDisplacementRY = d.mRotationalDisplacementRY; mRotationalDisplacementRZ = d.mRotationalDisplacementRZ; }
		public IfcStructuralLoadSingleDisplacement(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcStructuralLoadSingleDisplacementDistortion : IfcStructuralLoadSingleDisplacement
	{
		internal double mDistortion;// : OPTIONAL IfcCurvatureMeasure;
		internal IfcStructuralLoadSingleDisplacementDistortion() : base() { }
		internal IfcStructuralLoadSingleDisplacementDistortion(DatabaseIfc db, IfcStructuralLoadSingleDisplacementDistortion d) : base(db,d) { mDistortion = d.mDistortion; }
	}
	[Serializable]
	public partial class IfcStructuralLoadSingleForce : IfcStructuralLoadStatic
	{
		internal double mForceX = double.NaN, mForceY = double.NaN, mForceZ = double.NaN;// : OPTIONAL IfcForceMeasure;
		internal double mMomentX = double.NaN, mMomentY = double.NaN, mMomentZ = double.NaN;// : OPTIONAL IfcTorqueMeasure; 

		public double ForceX { get { return double.IsNaN(mForceX) ? 0 : mForceX; } set { mForceX = value; } }
		public double ForceY { get { return double.IsNaN(mForceY) ? 0 : mForceY; } set { mForceY = value; } }
		public double ForceZ { get { return double.IsNaN(mForceZ) ? 0 : mForceZ; } set { mForceZ = value; } }
		public double MomentX { get { return double.IsNaN(mMomentX) ? 0 : mMomentX; } set { mMomentX = value; } }
		public double MomentY { get { return double.IsNaN(mMomentY) ? 0 : mMomentY; } set { mMomentY = value; } }
		public double MomentZ { get { return double.IsNaN(mMomentZ) ? 0 : mMomentZ; } set { mMomentZ = value; } }

		internal IfcStructuralLoadSingleForce() : base() { }
		internal IfcStructuralLoadSingleForce(DatabaseIfc db, IfcStructuralLoadSingleForce f) : base(db,f) { mForceX = f.mForceX; mForceY = f.mForceY; mForceZ = f.mForceZ; mMomentX = f.mMomentX; mMomentY = f.mMomentY; mMomentZ = f.mMomentZ; }
		public IfcStructuralLoadSingleForce(DatabaseIfc db) : base(db) { }
		public IfcStructuralLoadSingleForce(DatabaseIfc db, double forceX, double forceY,double forceZ) : base(db) { mForceX = forceX; mForceY = forceY; mForceZ = forceZ; }
	}
	[Serializable]
	public partial class IfcStructuralLoadSingleForceWarping : IfcStructuralLoadSingleForce
	{
		internal double mWarpingMoment;// : OPTIONAL IfcWarpingMomentMeasure;
		internal IfcStructuralLoadSingleForceWarping() : base() { }
		internal IfcStructuralLoadSingleForceWarping(DatabaseIfc db, IfcStructuralLoadSingleForceWarping w) : base(db,w) { mWarpingMoment = w.mWarpingMoment; }
	}
	[Serializable]
	public abstract partial class IfcStructuralLoadStatic : IfcStructuralLoadOrResult /*ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralLoadLinearForce ,IfcStructuralLoadPlanarForce ,IfcStructuralLoadSingleDisplacement ,IfcStructuralLoadSingleForce ,IfcStructuralLoadTemperature))*/
	{
		protected IfcStructuralLoadStatic() : base() { }
		protected IfcStructuralLoadStatic(DatabaseIfc db) : base(db) { }
		protected IfcStructuralLoadStatic(DatabaseIfc db, IfcStructuralLoadStatic l) : base(db,l) { }
	}
	[Serializable]
	public partial class IfcStructuralLoadTemperature : IfcStructuralLoadStatic
	{
		internal double mDeltaT_Constant, mDeltaT_Y, mDeltaT_Z;// : OPTIONAL IfcThermodynamicTemperatureMeasure; 
		public double DeltaT_Constant { get { return mDeltaT_Constant; } set { mDeltaT_Constant = value; } }
		public double DeltaT_Y { get { return mDeltaT_Y; } set { mDeltaT_Y = value; } }
		public double DeltaT_Z { get { return mDeltaT_Z; } set { mDeltaT_Z = value; } }

		internal IfcStructuralLoadTemperature() : base() { }
		internal IfcStructuralLoadTemperature(DatabaseIfc db, IfcStructuralLoadTemperature t) : base(db,t) { mDeltaT_Constant = t.mDeltaT_Constant; mDeltaT_Y = t.mDeltaT_Y; mDeltaT_Z = t.mDeltaT_Z; }
		public IfcStructuralLoadTemperature(DatabaseIfc db, double T, double TY, double TZ) : base(db) { mDeltaT_Constant = T; mDeltaT_Y = TY; mDeltaT_Z = TZ; }
	}
	[Serializable]
	public abstract partial class IfcStructuralMember : IfcStructuralItem //ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralCurveMember, IfcStructuralSurfaceMember))
	{
		//INVERSE
		internal List<IfcRelConnectsStructuralMember> mConnectedBy = new List<IfcRelConnectsStructuralMember>();// : SET [0:?] OF IfcRelConnectsStructuralMember FOR RelatingStructuralMember 
		internal IfcRelConnectsStructuralElement mStructuralMemberForGG = null;

		public ReadOnlyCollection<IfcRelConnectsStructuralMember> ConnectedBy { get { return new ReadOnlyCollection<IfcRelConnectsStructuralMember>(mConnectedBy); } }

		protected IfcStructuralMember() : base() { }
		protected IfcStructuralMember(DatabaseIfc db, IfcStructuralMember m, DuplicateOptions options) : base(db, m, options)
		{
			foreach (IfcRelConnectsStructuralMember sm in m.mConnectedBy)
				(db.Factory.Duplicate(sm) as IfcRelConnectsStructuralMember).RelatingStructuralMember = this;
			
		}
		protected IfcStructuralMember(IfcStructuralAnalysisModel sm, IfcMaterialSelect ms, int id) : base(sm,id) { MaterialSelect = ms;  }
		
		public IfcMaterialSelect MaterialSelect
		{
			get { return GetMaterialSelect(); }
			set { base.setMaterial(value); }
		}
	}
	[Serializable]
	public partial class IfcStructuralPlanarAction : IfcStructuralSurfaceAction // Ifc2x3 IfcStructuralAction
	{
		internal IfcStructuralPlanarAction() : base() { }
		internal IfcStructuralPlanarAction(DatabaseIfc db, IfcStructuralPlanarAction a, DuplicateOptions options) : base(db, a, options) {  }
		public IfcStructuralPlanarAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoadPlanarForce load, bool global, bool projected)
			: base(lc, item, load, global, projected, IfcStructuralSurfaceActivityTypeEnum.CONST) { }
		public IfcStructuralPlanarAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoadTemperature load, bool global, bool projected)
			: base(lc, item, load, global, projected, IfcStructuralSurfaceActivityTypeEnum.CONST) { }
		public IfcStructuralPlanarAction(IfcStructuralLoadCase lc, IfcStructuralLoadPlanarForce load, IfcFaceSurface extent, bool global, bool projected)
			: base(lc,load, extent, global,projected,IfcStructuralSurfaceActivityTypeEnum.CONST ) { }
		public IfcStructuralPlanarAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoadPlanarForce load, IfcFaceSurface extent, bool global, bool projected)
			: base(lc, item, load, extent, global, projected, IfcStructuralSurfaceActivityTypeEnum.CONST) { }

	}
	// IfcStructuralPlanarActionVarying : IfcStructuralPlanarAction //DELETED IFC4
	[Serializable]
	public partial class IfcStructuralPointAction : IfcStructuralAction
	{
		internal IfcStructuralPointAction() : base() { }
		internal IfcStructuralPointAction(DatabaseIfc db, IfcStructuralPointAction a, DuplicateOptions options) : base(db, a, options) { }
		public IfcStructuralPointAction(IfcStructuralLoadCase lc, IfcStructuralPointConnection point, IfcStructuralLoadSingleForce l, bool global) : base(lc, point, l, null, global) { }
		public IfcStructuralPointAction(IfcStructuralLoadCase lc, IfcStructuralPointConnection point, IfcStructuralLoadSingleDisplacement l, bool global) : base(lc, point, l, null, global) { }
	}
	[Serializable]
	public partial class IfcStructuralPointConnection : IfcStructuralConnection
	{
		private int mConditionCoordinateSystem = 0;//	:	OPTIONAL IfcAxis2Placement3D;
		public IfcAxis2Placement3D ConditionCoordinateSystem { get { return mDatabase[mConditionCoordinateSystem] as IfcAxis2Placement3D; } set { mConditionCoordinateSystem = (value == null ? 0 : value.mIndex); } }
		public new IfcBoundaryNodeCondition AppliedCondition
		{
			get { return mDatabase[mAppliedCondition] as IfcBoundaryNodeCondition; }
			set { base.AppliedCondition = value; }
		}

		public IfcStructuralPointConnection() : base() { }
		internal IfcStructuralPointConnection(DatabaseIfc db, IfcStructuralPointConnection c, DuplicateOptions options) : base(db, c, options) { }
		public IfcStructuralPointConnection(IfcStructuralAnalysisModel sm, IfcVertexPoint point)
			: base(sm) { Representation = new IfcProductDefinitionShape(new IfcTopologyRepresentation(mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis), point)); }
	
		public IfcVertexPoint Vertex
		{
			get
			{
				IfcProductDefinitionShape representation = Representation;
				if (representation == null)
					return null;
				return representation.Representations.OfType<IfcTopologyRepresentation>().SelectMany(x => x.Items).OfType<IfcVertexPoint>().First();
			}
		}
	}
	[Serializable]
	public partial class IfcStructuralPointReaction : IfcStructuralReaction
	{
		internal IfcStructuralPointReaction() : base() { }
		internal IfcStructuralPointReaction(DatabaseIfc db, IfcStructuralPointReaction r, DuplicateOptions options) : base(db, r, options) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
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
		internal IfcStructuralProfileProperties(DatabaseIfc db, IfcStructuralProfileProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mTorsionalConstantX = p.mTorsionalConstantX;
			mMomentOfInertiaYZ = p.mMomentOfInertiaYZ; 
			mMomentOfInertiaY = p.mMomentOfInertiaY;
			mMomentOfInertiaZ = p.mMomentOfInertiaZ; 
			mWarpingConstant = p.mWarpingConstant;
			mShearCentreZ = p.mShearCentreZ; 
			mShearCentreY = p.mShearCentreY;
			mShearDeformationAreaZ = p.mShearDeformationAreaZ; 
			mShearDeformationAreaY = p.mShearDeformationAreaY;
			mMaximumSectionModulusY = p.mMaximumSectionModulusY; 
			mMinimumSectionModulusY = p.mMinimumSectionModulusY; 
			mMaximumSectionModulusZ = p.mMaximumSectionModulusZ; 
			mMinimumSectionModulusZ = p.mMinimumSectionModulusZ;
			mTorsionalSectionModulus = p.mTorsionalSectionModulus;
			mCentreOfGravityInX = p.mCentreOfGravityInX; 
			mCentreOfGravityInY = p.mCentreOfGravityInY;
		}
		public IfcStructuralProfileProperties(IfcProfileDef p) : base(p) { }
	}
	[Serializable]
	public abstract partial class IfcStructuralReaction : IfcStructuralActivity
	{   //INVERSE 
		//internal List<int> mCauses = new List<int>();// : OPTIONAL IfcStructuralReaction;
		protected IfcStructuralReaction() : base() { }
		protected IfcStructuralReaction(DatabaseIfc db, IfcStructuralReaction p, DuplicateOptions options) : base(db, p, options) { }
		protected IfcStructuralReaction(IfcStructuralLoad appliedLoad, IfcGlobalOrLocalEnum globalOrLocal)
			: base(appliedLoad, globalOrLocal) { }
	}
	[Serializable]
	public partial class IfcStructuralResultGroup : IfcGroup
	{
		internal IfcAnalysisTheoryTypeEnum mTheoryType = IfcAnalysisTheoryTypeEnum.NOTDEFINED;// : IfcAnalysisTheoryTypeEnum;
		internal IfcStructuralLoadGroup mResultForLoadGroup = null;// : OPTIONAL IfcStructuralLoadGroup;
		internal bool mIsLinear = false;// : BOOLEAN; 
		//INVERSE
		internal IfcStructuralAnalysisModel mResultGroupFor = null;// : SET[0:1] OF IfcStructuralAnalysisModel FOR HasResults;

		public IfcStructuralLoadGroup ResultForLoadGroup { get { return mResultForLoadGroup; } set { mResultForLoadGroup = value; value.mSourceOfResultGroup = this; } }
		public IfcStructuralAnalysisModel ResultGroupFor { get { return mResultGroupFor; } set { mResultGroupFor = value; } }

		internal IfcStructuralResultGroup() : base() { }
		internal IfcStructuralResultGroup(DatabaseIfc db, IfcStructuralResultGroup g, DuplicateOptions options) : base(db, g, options)
		{
			mTheoryType = g.mTheoryType;
			if(g.mResultForLoadGroup != null)
				ResultForLoadGroup = db.Factory.Duplicate( g.ResultForLoadGroup) as IfcStructuralLoadGroup;
			mIsLinear = g.mIsLinear;
		}
		public IfcStructuralResultGroup(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Obsolete("DELETED IFC4", false)]
	[Serializable]
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
		internal IfcStructuralSteelProfileProperties(DatabaseIfc db, IfcStructuralSteelProfileProperties p, DuplicateOptions options) : base(db, p, options) { mShearAreaZ = p.mShearAreaZ; mShearAreaY = p.mShearAreaY; mPlasticShapeFactorY = p.mPlasticShapeFactorY; mPlasticShapeFactorZ = p.mPlasticShapeFactorZ; }
	}
	[Serializable]
	public partial class IfcStructuralSurfaceAction : IfcStructuralAction //IFC4 SUPERTYPE OF(IfcStructuralPlanarAction)
	{
		internal IfcProjectedOrTrueLengthEnum mProjectedOrTrue = IfcProjectedOrTrueLengthEnum.TRUE_LENGTH;// : IfcProjectedOrTrueLengthEnum
		internal IfcStructuralSurfaceActivityTypeEnum mPredefinedType = IfcStructuralSurfaceActivityTypeEnum.NOTDEFINED;//IfcStructuralCurveActivityTypeEnum
		
		public IfcProjectedOrTrueLengthEnum ProjectedOrTrue { get { return mProjectedOrTrue; } set { mProjectedOrTrue = value; } }
		public IfcStructuralSurfaceActivityTypeEnum PredefinedType {  get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcStructuralSurfaceAction() : base() { }
		internal IfcStructuralSurfaceAction(DatabaseIfc db, IfcStructuralSurfaceAction a, DuplicateOptions options) : base(db, a, options) { mProjectedOrTrue = a.mProjectedOrTrue; mPredefinedType = a.mPredefinedType; }
		public IfcStructuralSurfaceAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, bool global, bool projected, IfcStructuralSurfaceActivityTypeEnum type)
			: base(lc, item, load,null, global) { mProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH; mPredefinedType = type; }
		public IfcStructuralSurfaceAction(IfcStructuralLoadCase lc, IfcStructuralLoad load, IfcFaceSurface extent, bool global, bool projected, IfcStructuralSurfaceActivityTypeEnum type)
			: base(lc,null,load,new IfcTopologyRepresentation(lc.mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Reference), extent), global)
		{
			if (mDatabase.mRelease < ReleaseVersion.IFC4)
				throw new Exception(StepClassName + "added in IFC4");
			mProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH;
			mPredefinedType = type;
		}
		public IfcStructuralSurfaceAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, IfcFaceSurface extent, bool global, bool projected, IfcStructuralSurfaceActivityTypeEnum type)
			: base(lc, item, load, new IfcTopologyRepresentation(lc.mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Reference), extent), global)
		{ 
			if (mDatabase.mRelease < ReleaseVersion.IFC4) throw new Exception(StepClassName + "added in IFC4"); 
			mProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH; 
			mPredefinedType = type; 
		}
	}
	[Serializable]
	public partial class IfcStructuralSurfaceConnection : IfcStructuralConnection
	{
		internal IfcStructuralSurfaceConnection() : base() { }
		internal IfcStructuralSurfaceConnection(DatabaseIfc db, IfcStructuralSurfaceConnection c, DuplicateOptions options) : base(db, c, options) { }
	}
	[Serializable]
	public partial class IfcStructuralSurfaceMember : IfcStructuralMember
	{
		internal IfcStructuralSurfaceMemberTypeEnum mPredefinedType = IfcStructuralSurfaceMemberTypeEnum.NOTDEFINED;// : IfcStructuralSurfaceMemberTypeEnum;
		internal double mThickness = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure; 

		public IfcStructuralSurfaceMemberTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public double Thickness { get { return mThickness; } set { mThickness = value; } }

		public IfcStructuralSurfaceMember() : base() { }
		internal IfcStructuralSurfaceMember(DatabaseIfc db, IfcStructuralSurfaceMember m, DuplicateOptions options) : base(db, m, options) { mPredefinedType = m.mPredefinedType; mThickness = m.mThickness; }
		public IfcStructuralSurfaceMember(IfcStructuralAnalysisModel sm, IfcFaceSurface f, IfcMaterial material, int id, double thickness)
			: base(sm, material, id)
		{
			ObjectPlacement = sm.SharedPlacement;
			Representation = new IfcProductDefinitionShape(new IfcTopologyRepresentation(mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis), f));
			mThickness = thickness;
		}
		public IfcStructuralSurfaceMember(IfcStructuralAnalysisModel sm, IfcFaceSurface f, IfcMaterialLayer materialLayer, int id)
			: base(sm, materialLayer, id)
		{
			ObjectPlacement = sm.SharedPlacement;
			Representation = new IfcProductDefinitionShape(new IfcTopologyRepresentation(mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis), f));
		}
	}
	[Serializable]
	public partial class IfcStructuralSurfaceMemberVarying : IfcStructuralSurfaceMember
	{
		internal List<double> mSubsequentThickness = new List<double>();// : LIST [2:?] OF IfcPositiveLengthMeasure;
		internal int mVaryingThicknessLocation;// : IfcShapeAspect; 

		public IfcShapeAspect VaryingThicknessLocation { get { return mDatabase[mVaryingThicknessLocation] as IfcShapeAspect; } set { mVaryingThicknessLocation = value.mIndex; } }

		internal IfcStructuralSurfaceMemberVarying() : base() { }
		internal IfcStructuralSurfaceMemberVarying(DatabaseIfc db, IfcStructuralSurfaceMemberVarying m, DuplicateOptions options) : base(db, m, options) { mSubsequentThickness.AddRange(m.mSubsequentThickness); mVaryingThicknessLocation = m.mVaryingThicknessLocation; }
	}
	[Serializable]
	public partial class IfcStructuralSurfaceReaction : IfcStructuralReaction
	{
		private IfcStructuralSurfaceActivityTypeEnum mPredefinedType = IfcStructuralSurfaceActivityTypeEnum.NOTDEFINED; //: IfcStructuralSurfaceActivityTypeEnum;
		public IfcStructuralSurfaceActivityTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcStructuralSurfaceReaction() : base() { }
		public IfcStructuralSurfaceReaction(IfcStructuralLoad appliedLoad, IfcGlobalOrLocalEnum globalOrLocal, IfcStructuralSurfaceActivityTypeEnum predefinedType)
			: base(appliedLoad, globalOrLocal) { PredefinedType = predefinedType; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcStructuredDimensionCallout : IfcDraughtingCallout // DEPRECATED IFC4
	{
		internal IfcStructuredDimensionCallout() : base() { }
		//internal IfcStructuredDimensionCallout(IfcStructuredDimensionCallout i) : base(i) { }
	}
	public partial interface IfcStyleAssignmentSelect : IBaseClassIfc //(IfcPresentationStyle, IfcPresentationStyleAssignment); 
	{
		ReadOnlyCollection<IfcStyledItem> StyledItems { get; }
		void associateItem(IfcStyledItem item);
	}
	[Serializable]
	public partial class IfcStyledItem : IfcRepresentationItem, NamedObjectIfc
	{
		private IfcRepresentationItem mItem = null;// : OPTIONAL IfcRepresentationItem;
		private SET<IfcStyleAssignmentSelect> mStyles = new SET<IfcStyleAssignmentSelect>();// : SET [1:?] OF IfcStyleAssignmentSelect; ifc2x3 IfcPresentationStyleAssignment;
		private string mName = "$";// : OPTIONAL IfcLabel; 

		public IfcRepresentationItem Item
		{
			get { return mItem; }
			set
			{
				IfcRepresentationItem item = Item;
				if (item != null)
					item.mStyledByItem = null;
				mItem = value;
				if(value != null)
					value.mStyledByItem = this;
			}
		}
		public SET<IfcStyleAssignmentSelect> Styles { get { return mStyles; } }
		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcStyledItem() : base() { }
		internal IfcStyledItem(DatabaseIfc db, IfcStyledItem i, DuplicateOptions options) : base(db, i, options)
		{
			foreach(IfcStyleAssignmentSelect style in i.Styles)
				addStyle(db.Factory.Duplicate(style) as IfcStyleAssignmentSelect);
			mName = i.mName;
		}
		public IfcStyledItem(IfcStyleAssignmentSelect style) : base(style.Database) { addStyle(style); }
		public IfcStyledItem(IfcRepresentationItem item, IfcStyleAssignmentSelect style) : this(style) { Item = item; }
		public IfcStyledItem(IfcRepresentationItem item, IEnumerable<IfcStyleAssignmentSelect> styles) : base(item.mDatabase)
		{
			Item = item;
			foreach (IfcStyleAssignmentSelect style in styles)
				addStyle(style);
		}
		internal void addStyle(IfcStyleAssignmentSelect style)
		{
			if (mDatabase.mRelease < ReleaseVersion.IFC4)
			{
				IfcPresentationStyleAssignment presentationStyleAssignment = style as IfcPresentationStyleAssignment;
				if(presentationStyleAssignment != null)
					mStyles.Add(presentationStyleAssignment);
				else
				{
					IfcPresentationStyle presentationStyle = style as IfcPresentationStyle;
					if(presentationStyle != null)
						mStyles.Add(new IfcPresentationStyleAssignment(presentationStyle));
				}
			}
			else
				mStyles.Add(style);
		}
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (BaseClassIfc style in Styles)
				result.AddRange(style.Extract<T>());
			return result;
		}

		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcStyledItem s = e as IfcStyledItem;
			if (s == null || string.Compare(Name, s.Name,false) != 0 || mStyles.Count != s.mStyles.Count)
				return false;

			List<BaseClassIfc> styles = s.Styles.Select(x=>x as BaseClassIfc).ToList();
			foreach(IfcStyleAssignmentSelect style in Styles)
			{
				BaseClassIfc baseClass = style as BaseClassIfc;
				bool duplicate = false;
				for (int icounter = 0; icounter <styles.Count; icounter++)
				{
					if(baseClass.isDuplicate(styles[icounter], tol))
					{
						duplicate = true;
						styles.RemoveAt(icounter);
						break;
					}
				}
				if (!duplicate)
					return false;
			}
			return base.isDuplicate(e, tol);
		}
	}
	[Serializable]
	public partial class IfcStyledRepresentation : IfcStyleModel
	{
		internal IfcStyledRepresentation() : base() { }
		internal IfcStyledRepresentation(DatabaseIfc db, IfcStyledRepresentation r, DuplicateOptions options) : base(db, r, options) { }
		public IfcStyledRepresentation(IfcStyledItem ri) : base(ri.mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Body), ri) { }
		public IfcStyledRepresentation(IEnumerable<IfcStyledItem> reps) : base(reps.First().mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Body), reps) { }
		public IfcStyledRepresentation(IfcRepresentationContext context, IfcStyledItem item) : base(context, item) { }
		public IfcStyledRepresentation(IfcRepresentationContext context, IEnumerable<IfcStyledItem> reps) : base(context, reps) { }

		private void mStyledItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcStyledItem r in e.NewItems)
				{
					if(!base.Items.Contains(r))
						base.Items.Add(r);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcStyledItem r in e.OldItems)
					base.Items.Remove(r);
			}
		}
	}
	[Serializable]
	public abstract partial class IfcStyleModel : IfcRepresentation<IfcStyledItem> //ABSTRACT SUPERTYPE OF(IfcStyledRepresentation)
	{
		protected IfcStyleModel() : base() { }
		protected IfcStyleModel(DatabaseIfc db, IfcStyleModel m, DuplicateOptions options) : base(db, m, options) { }
		protected IfcStyleModel(IfcRepresentationContext context, IfcStyledItem styledItem) : base(context, styledItem) { }
		protected IfcStyleModel(IfcRepresentationContext context, IEnumerable<IfcStyledItem> reps) : base(context, reps) { }
	}
	[Serializable]
	public partial class IfcSubContractResource : IfcConstructionResource
	{
		internal IfcSubContractResourceTypeEnum mPredefinedType = IfcSubContractResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcSubContractResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSubContractResource() : base() { }
		internal IfcSubContractResource(DatabaseIfc db, IfcSubContractResource r, DuplicateOptions options) : base(db, r, options) { mPredefinedType = r.mPredefinedType; }
		public IfcSubContractResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcSubContractResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcSubContractResourceTypeEnum mPredefinedType = IfcSubContractResourceTypeEnum.NOTDEFINED;
		public IfcSubContractResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSubContractResourceType() : base() { }
		internal IfcSubContractResourceType(DatabaseIfc db, IfcSubContractResourceType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcSubContractResourceType(DatabaseIfc db, string name, IfcSubContractResourceTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcSubedge : IfcEdge
	{
		internal int mParentEdge;// IfcEdge;
		public IfcEdge ParentEdge { get { return mDatabase[mParentEdge] as IfcEdge; } set { mParentEdge = value.mIndex; } }

		internal IfcSubedge() : base() { }
		internal IfcSubedge(DatabaseIfc db, IfcSubedge e, DuplicateOptions options) : base(db, e, options) { ParentEdge = db.Factory.Duplicate(e.ParentEdge) as IfcEdge; }
		public IfcSubedge(IfcVertex v1, IfcVertex v2, IfcEdge e) : base(v1, v2) { mParentEdge = e.mIndex; }
	}
	[Serializable]
	public abstract partial class IfcSurface : IfcGeometricRepresentationItem, IfcGeometricSetSelect, IfcSurfaceOrFaceSurface  /*	ABSTRACT SUPERTYPE OF (ONEOF(IfcBoundedSurface,IfcElementarySurface,IfcSweptSurface))*/
	{
		protected IfcSurface() : base() { }
		protected IfcSurface(DatabaseIfc db, IfcSurface s, DuplicateOptions options) : base(db, s, options) { }
		protected IfcSurface(DatabaseIfc db) : base(db) { }

		//INVERSE gg
		public List<IfcFaceSurface> OfFaces = new List<IfcFaceSurface>();
	}
	[Serializable]
	public partial class IfcSurfaceCurve : IfcCurve, IfcCurveOnSurface //IFC4 Add2
	{
		private IfcCurve mCurve3D;//: IfcCurve;
		internal SET<IfcPcurve> mAssociatedGeometry = new SET<IfcPcurve>();// : SET [1:2] OF IfcTrimmingSelect;
		internal IfcPreferredSurfaceCurveRepresentation mMasterRepresentation = IfcPreferredSurfaceCurveRepresentation.CURVE3D;// : IfcPreferredSurfaceCurveRepresentation; 

		public IfcCurve Curve3D { get { return mCurve3D; } set { mCurve3D = value; } }
		public SET<IfcPcurve> AssociatedGeometry { get { return mAssociatedGeometry; } }
		public IfcPreferredSurfaceCurveRepresentation MasterRepresentation { get { return mMasterRepresentation; } set { mMasterRepresentation = value; } }

		internal IfcSurfaceCurve() : base() { }
		internal IfcSurfaceCurve(DatabaseIfc db, IfcSurfaceCurve c, DuplicateOptions options) : base(db, c, options)
		{
			Curve3D = db.Factory.Duplicate(c.Curve3D) as IfcCurve;
			AssociatedGeometry.AddRange(c.AssociatedGeometry.Select(x => db.Factory.Duplicate(x) as IfcPcurve));
			mMasterRepresentation = c.mMasterRepresentation;
		}
		internal IfcSurfaceCurve(IfcCurve curve3D, IfcPcurve p1, IfcPreferredSurfaceCurveRepresentation cr) : base(curve3D.mDatabase)
		{
			Curve3D = curve3D;
			AssociatedGeometry.Add(p1);
			mMasterRepresentation = cr;
		}
		internal IfcSurfaceCurve(IfcCurve curve3D, IfcPcurve p1, IfcPcurve p2, IfcPreferredSurfaceCurveRepresentation cr) : this(curve3D, p1, cr)
		{
			AssociatedGeometry.Add(p2);
		}
	}
	[Serializable]
	public partial class IfcSurfaceCurveSweptAreaSolid : IfcDirectrixCurveSweptAreaSolid
	{
		internal int mReferenceSurface;// : IfcSurface; 
		public IfcSurface ReferenceSurface { get { return mDatabase[mReferenceSurface] as IfcSurface; } set { mReferenceSurface = value.mIndex; } }

		internal IfcSurfaceCurveSweptAreaSolid() : base() { }
		internal IfcSurfaceCurveSweptAreaSolid(DatabaseIfc db, IfcSurfaceCurveSweptAreaSolid s, DuplicateOptions options) : base(db, s, options) 
		{ 
			ReferenceSurface = db.Factory.Duplicate(s.ReferenceSurface, options) as IfcSurface; 
		}
		public IfcSurfaceCurveSweptAreaSolid(IfcProfileDef sweptArea, IfcCurve directrix, IfcSurface referenceSurface) 
			: base(sweptArea, directrix)
		{
			ReferenceSurface = referenceSurface;
		}
	}
	[Serializable]
	public partial class IfcSurfaceFeature : IfcFeatureElement
	{
		private IfcSurfaceFeatureTypeEnum mPredefinedType = IfcSurfaceFeatureTypeEnum.NOTDEFINED; //: OPTIONAL IfcSurfaceFeatureTypeEnum;
		public IfcSurfaceFeatureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcSurfaceFeature() : base() { }
		public IfcSurfaceFeature(DatabaseIfc db) : base(db) { }
		public IfcSurfaceFeature(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcSurfaceOfLinearExtrusion : IfcSweptSurface
	{
		internal int mExtrudedDirection;//  : IfcDirection;
		internal double mDepth;// : IfcLengthMeasure;

		public IfcDirection ExtrudedDirection { get { return mDatabase[mExtrudedDirection] as IfcDirection; } set { mExtrudedDirection = value.mIndex; } }
		public double Depth { get { return mDepth; } set { mDepth = value; } }

		internal IfcSurfaceOfLinearExtrusion() : base() { }
		internal IfcSurfaceOfLinearExtrusion(DatabaseIfc db, IfcSurfaceOfLinearExtrusion s, DuplicateOptions options) : base(db, s, options) { ExtrudedDirection = db.Factory.Duplicate(s.ExtrudedDirection) as IfcDirection; mDepth = s.mDepth; }
		public IfcSurfaceOfLinearExtrusion(IfcProfileDef sweptCurve, IfcAxis2Placement3D position, double depth) : base(sweptCurve, position) { ExtrudedDirection = mDatabase.Factory.ZAxis; mDepth = depth; }
	}
	[Serializable]
	public partial class IfcSurfaceOfRevolution : IfcSweptSurface
	{
		internal int mAxisPosition;//  : IfcAxis1Placement;
		public IfcAxis1Placement AxisPosition { get { return mDatabase[mAxisPosition] as IfcAxis1Placement; } set { mAxisPosition = value.mIndex; } }

		internal IfcSurfaceOfRevolution() : base() { }
		internal IfcSurfaceOfRevolution(DatabaseIfc db, IfcSurfaceOfRevolution s, DuplicateOptions options) : base(db, s, options) { AxisPosition = db.Factory.Duplicate(s.AxisPosition) as IfcAxis1Placement; }
	}
	public interface IfcSurfaceOrFaceSurface : IBaseClassIfc { }  // = SELECT (	IfcSurface, IfcFaceSurface, IfcFaceBasedSurfaceModel);
	[Serializable]
	public partial class IfcSurfaceReinforcementArea : IfcStructuralLoadOrResult
	{
		private LIST<double> mSurfaceReinforcement1 = new LIST<double>(); //: OPTIONAL LIST[2:3] OF IfcLengthMeasure;
		private LIST<double> mSurfaceReinforcement2 = new LIST<double>(); //: OPTIONAL LIST[2:3] OF IfcLengthMeasure;
		private double mShearReinforcement = double.NaN; //: OPTIONAL IfcRatioMeasure;

		public LIST<double> SurfaceReinforcement1 { get { return mSurfaceReinforcement1; } set { mSurfaceReinforcement1 = value; } }
		public LIST<double> SurfaceReinforcement2 { get { return mSurfaceReinforcement2; } set { mSurfaceReinforcement2 = value; } }
		public double ShearReinforcement { get { return mShearReinforcement; } set { mShearReinforcement = value; } }

		public IfcSurfaceReinforcementArea() : base() { }
		public IfcSurfaceReinforcementArea(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcSurfaceStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal IfcSurfaceSide mSide = IfcSurfaceSide.BOTH;// : IfcSurfaceSide;
		internal SET<IfcSurfaceStyleElementSelect> mStyles = new SET<IfcSurfaceStyleElementSelect>();// : SET [1:5] OF IfcSurfaceStyleElementSelect; 

		public IfcSurfaceSide Side { get { return mSide; } set { mSide = value; } }
		public SET<IfcSurfaceStyleElementSelect> Styles { get { return mStyles; } }

		internal IfcSurfaceStyle() : base() { }
		internal IfcSurfaceStyle(DatabaseIfc db, IfcSurfaceStyle s) : base(db,s) { mSide = s.mSide; mStyles.AddRange(s.mStyles.Select(x=> db.Factory.Duplicate(x) as IfcSurfaceStyleElementSelect)); }
		internal IfcSurfaceStyle(DatabaseIfc db) : base(db) { }
		public IfcSurfaceStyle(IfcSurfaceStyleShading shading) : base(shading.mDatabase) { mStyles.Add(shading); }
		public IfcSurfaceStyle(IfcSurfaceStyleLighting lighting) : base(lighting.mDatabase) { mStyles.Add(lighting); }
		public IfcSurfaceStyle(IfcSurfaceStyleWithTextures textures) : base(textures.mDatabase) { mStyles.Add(textures); }
		public IfcSurfaceStyle(IfcExternallyDefinedSurfaceStyle external) : base(external.mDatabase) { mStyles.Add(external); }
		public IfcSurfaceStyle(IfcSurfaceStyleRefraction refraction) : base(refraction.mDatabase) { mStyles.Add(refraction); }
		public IfcSurfaceStyle(IfcSurfaceStyleShading shading, IfcSurfaceStyleLighting lighting, IfcSurfaceStyleWithTextures textures, IfcExternallyDefinedSurfaceStyle external, IfcSurfaceStyleRefraction refraction)
			:base(shading != null ? shading.mDatabase : (lighting != null ? lighting.mDatabase : (textures != null ? textures.mDatabase : (external != null ? external.mDatabase : refraction.mDatabase))))
		{
			if (shading != null)
				mStyles.Add(shading);
			if (lighting != null)
				mStyles.Add(lighting);
			if (textures != null)
				mStyles.Add(textures);
			if (external != null)
				mStyles.Add(external);
			if (refraction != null)
				mStyles.Add(refraction);
		}
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (BaseClassIfc style in mStyles.OfType<BaseClassIfc>())
				result.AddRange(style.Extract<T>());
			return result;
		}
	}
	public partial interface IfcSurfaceStyleElementSelect : IBaseClassIfc //SELECT(IfcSurfaceStyleShading, IfcSurfaceStyleLighting, IfcSurfaceStyleWithTextures
	{ //, IfcExternallyDefinedSurfaceStyle, IfcSurfaceStyleRefraction);
	}
	[Serializable]
	public partial class IfcSurfaceStyleLighting : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal int mDiffuseTransmissionColour, mDiffuseReflectionColour, mTransmissionColour, mReflectanceColour;//	 :	IfcColourRgb;
		public IfcColourRgb DiffuseTransmissionColour {  get { return mDatabase[mDiffuseTransmissionColour] as IfcColourRgb; } set { mDiffuseTransmissionColour = value.mIndex; } }
		public IfcColourRgb DiffuseReflectionColour {  get { return mDatabase[mDiffuseReflectionColour] as IfcColourRgb; } set { mDiffuseReflectionColour = value.mIndex; } }
		public IfcColourRgb TransmissionColour {  get { return mDatabase[mTransmissionColour] as IfcColourRgb; } set { mTransmissionColour = value.mIndex; } }
		public IfcColourRgb ReflectanceColour {  get { return mDatabase[mReflectanceColour] as IfcColourRgb; } set { mReflectanceColour = value.mIndex; } }
		internal IfcSurfaceStyleLighting() : base() { }
		internal IfcSurfaceStyleLighting(DatabaseIfc db, IfcSurfaceStyleLighting i) : base(db,i)
		{
			mDiffuseTransmissionColour = i.mDiffuseTransmissionColour;
			mDiffuseReflectionColour = i.mDiffuseReflectionColour;
			mTransmissionColour = i.mTransmissionColour;
			mReflectanceColour = i.mReflectanceColour;
		}
		public IfcSurfaceStyleLighting(IfcColourRgb diffuseTransmission, IfcColourRgb diffuseReflection, IfcColourRgb transmission, IfcColourRgb reflection)
			: base(diffuseTransmission.mDatabase)
		{
			DiffuseTransmissionColour = diffuseTransmission;
			DiffuseReflectionColour = diffuseReflection;
			TransmissionColour = transmission;
			ReflectanceColour = reflection;
		}
	}
	[Serializable]
	public partial class IfcSurfaceStyleRefraction : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal double mRefractionIndex = double.NaN, mDispersionFactor = double.NaN;//	 :	OPTIONAL IfcReal;
		internal IfcSurfaceStyleRefraction() : base() { }
		public IfcSurfaceStyleRefraction(DatabaseIfc db) : base(db) { }
		internal IfcSurfaceStyleRefraction(DatabaseIfc db, IfcSurfaceStyleRefraction s) : base(db, s)
		{
			mRefractionIndex = s.mRefractionIndex;
			mDispersionFactor = s.mDispersionFactor;
		}
	}
	[Serializable]
	public partial class IfcSurfaceStyleRendering : IfcSurfaceStyleShading
	{
		private IfcColourOrFactor mDiffuseColour, mTransmissionColour, mDiffuseTransmissionColour, mReflectionColour, mSpecularColour;//:	OPTIONAL IfcColourOrFactor;
		internal IfcSpecularHighlightSelect mSpecularHighlight;// : OPTIONAL 
		internal IfcReflectanceMethodEnum mReflectanceMethod = IfcReflectanceMethodEnum.NOTDEFINED;// : IfcReflectanceMethodEnum; 

		public IfcColourOrFactor DiffuseColour { get { return mDiffuseColour; } set { mDiffuseColour = value; } }
		public IfcColourOrFactor TransmissionColour { get { return mTransmissionColour; } set { mTransmissionColour = value; } }
		public IfcColourOrFactor DiffuseTransmissionColour { get { return mDiffuseTransmissionColour; } set { mDiffuseTransmissionColour = value; } }
		public IfcColourOrFactor ReflectionColour { get { return mReflectionColour; } set { mReflectionColour = value; } }
		public IfcColourOrFactor SpecularColour { get { return mSpecularColour; } set { mSpecularColour = value; } }
		public IfcSpecularHighlightSelect SpecularHighlight { get { return mSpecularHighlight; } set { mSpecularHighlight = value; } }
		public IfcReflectanceMethodEnum ReflectanceMethod { get { return mReflectanceMethod; } set { mReflectanceMethod = value; } }

		internal IfcSurfaceStyleRendering() : base() { }
		internal IfcSurfaceStyleRendering(DatabaseIfc db, IfcSurfaceStyleRendering r) : base(db, r)
		{
			mDiffuseColour = r.mDiffuseColour;
			mTransmissionColour = r.mTransmissionColour;
			mDiffuseTransmissionColour = r.mDiffuseTransmissionColour;
			mReflectionColour = r.mReflectionColour;
			mSpecularColour = r.mSpecularColour;
			mSpecularHighlight = r.mSpecularHighlight;
			mReflectanceMethod = r.mReflectanceMethod;
		}
		public IfcSurfaceStyleRendering(IfcColourRgb surfaceColour) : base(surfaceColour) { }
	}
	[Serializable]
	public partial class IfcSurfaceStyleShading : IfcPresentationItem, IfcSurfaceStyleElementSelect //SUPERTYPE OF(IfcSurfaceStyleRendering)
	{
		private int mSurfaceColour;// : IfcColourRgb;
		private double mTransparency = double.NaN; // : IfcNormalisedRatioMeasure
		
		public IfcColourRgb SurfaceColour { get { return mDatabase[mSurfaceColour] as IfcColourRgb; } set { mSurfaceColour = value.mIndex; } }
		public double Transparency { get { return mTransparency; } set { mTransparency = value; } }

		internal IfcSurfaceStyleShading() : base() { }
		internal IfcSurfaceStyleShading(DatabaseIfc db, IfcSurfaceStyleShading s) : base(db,s) 
		{
			SurfaceColour = db.Factory.Duplicate( s.SurfaceColour) as IfcColourRgb;
			mTransparency = s.mTransparency;
		}
		public IfcSurfaceStyleShading(IfcColourRgb surfaceColour) : base(surfaceColour.mDatabase) { SurfaceColour = surfaceColour; }
	}
	[Serializable]
	public partial class IfcSurfaceStyleWithTextures : IfcPresentationItem, IfcSurfaceStyleElementSelect
	{
		internal LIST<IfcSurfaceTexture> mTextures = new LIST<IfcSurfaceTexture>();//: LIST [1:?] OF IfcSurfaceTexture; 
		public LIST<IfcSurfaceTexture> Textures { get { return mTextures; } }
		internal IfcSurfaceStyleWithTextures() : base() { }
		public IfcSurfaceStyleWithTextures(IfcSurfaceTexture texture) : base(texture.mDatabase) { mTextures.Add(texture); }
		public IfcSurfaceStyleWithTextures(List<IfcSurfaceTexture> textures) : base(textures[0].mDatabase) { mTextures.AddRange(textures);  }
	}
	[Serializable]
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
		public ReadOnlyCollection<string> Parameters { get { return new ReadOnlyCollection<string>( mParameter.ConvertAll(x => ParserIfc.Decode(x))); } }

		protected IfcSurfaceTexture() : base() { }
		protected IfcSurfaceTexture(DatabaseIfc db, IfcSurfaceTexture t) : base(db,t) { mRepeatS = t.mRepeatS; mRepeatT = t.mRepeatT; mMode = t.mMode; mTextureTransform = t.mTextureTransform; }
		protected IfcSurfaceTexture(DatabaseIfc db,bool repeatS, bool repeatT) : base(db) { mRepeatS = repeatS; mRepeatT = repeatT; }
	}
	[Serializable]
	public abstract partial class IfcSweptAreaSolid : IfcSolidModel  /*ABSTRACT SUPERTYPE OF (ONEOF (IfcExtrudedAreaSolid, IfcFixedReferenceSweptAreaSolid ,IfcRevolvedAreaSolid ,IfcSurfaceCurveSweptAreaSolid))*/
	{
		private IfcProfileDef mSweptArea;// : IfcProfileDef;
		private IfcAxis2Placement3D mPosition;// : IfcAxis2Placement3D; 	 :	OPTIONAL IFC4

		public IfcProfileDef SweptArea { get { return mSweptArea; } set { mSweptArea = value; } }
		public IfcAxis2Placement3D Position { get { return mPosition; } set { mPosition = value; } }

		protected IfcSweptAreaSolid() : base() { }
		protected IfcSweptAreaSolid(DatabaseIfc db) : base(db) { }
		protected IfcSweptAreaSolid(DatabaseIfc db, IfcSweptAreaSolid s, DuplicateOptions options) : base(db, s, options)
		{
			SweptArea = db.Factory.Duplicate(s.SweptArea, options) as IfcProfileDef;
			if (s.mPosition == null)
			{
				if (db.Release < ReleaseVersion.IFC4)
					s.mPosition = db.Factory.XYPlanePlacement;
			}
			else
				Position = db.Factory.DuplicateAxis(s.Position, options);
		}
		protected IfcSweptAreaSolid(IfcProfileDef sweptArea) : base(sweptArea.mDatabase) { SweptArea = sweptArea; if (sweptArea.mDatabase.Release < ReleaseVersion.IFC4) Position = sweptArea.mDatabase.Factory.XYPlanePlacement; }
		protected IfcSweptAreaSolid(IfcProfileDef prof, IfcAxis2Placement3D position) 
			: this(prof) { if (position != null) Position = position; }
	}
	[Serializable]
	public partial class IfcSweptDiskSolid : IfcSolidModel
	{
		internal int mDirectrix;// : IfcCurve;
		internal double mRadius;// : IfcPositiveLengthMeasure;
		internal double mInnerRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mStartParam = double.NaN;// : OPTIONAL IfcParameterValue; IFC4
		internal double mEndParam = double.NaN;// : OPTIONAL IfcParameterValue;  IFC4

		public IfcCurve Directrix { get { return mDatabase[mDirectrix] as IfcCurve; } set { mDirectrix = value.mIndex; } }
		public double Radius { get { return mRadius; } set { mRadius = value; } }
		public double InnerRadius { get { return mInnerRadius; } set { mInnerRadius = value; } }
		public double StartParam { get { return mStartParam; } set { mStartParam = value; } }
		public double EndParam { get { return mEndParam; } set { mEndParam = value; } }

		internal IfcSweptDiskSolid() : base() { }
		internal IfcSweptDiskSolid(DatabaseIfc db, IfcSweptDiskSolid s, DuplicateOptions options) : base(db,s, options) 
		{ 
			Directrix = s.Directrix.Duplicate(db, options) as IfcCurve; 
			mRadius = s.mRadius; 
			mInnerRadius = s.mInnerRadius; 
			mStartParam = s.mStartParam; 
			mEndParam = s.mEndParam; 
		}
		public IfcSweptDiskSolid(IfcCurve directrix, double radius) : base(directrix.mDatabase) { Directrix = directrix; mRadius = radius; }
		public IfcSweptDiskSolid(IfcCurve directrix, double radius, double innerRadius) : base(directrix.mDatabase) { Directrix = directrix; mRadius = radius; mInnerRadius = innerRadius; }
	}
	[Serializable]
	public partial class IfcSweptDiskSolidPolygonal : IfcSweptDiskSolid
	{
		internal double mFilletRadius;// : OPTIONAL IfcPositiveLengthMeasure; 
		internal IfcSweptDiskSolidPolygonal() : base() { }
		internal IfcSweptDiskSolidPolygonal(DatabaseIfc db, IfcSweptDiskSolidPolygonal p, DuplicateOptions options) : base(db, p, options) { mFilletRadius = p.mFilletRadius; }
	}
	[Serializable]
	public abstract partial class IfcSweptSurface : IfcSurface /*	ABSTRACT SUPERTYPE OF (ONEOF (IfcSurfaceOfLinearExtrusion ,IfcSurfaceOfRevolution))*/
	{
		internal int mSweptCurve;// : IfcProfileDef;
		internal int mPosition;// : OPTIONAL IfcAxis2Placement3D; IFC4 Optional

		public IfcProfileDef SweptCurve { get { return mDatabase[mSweptCurve] as IfcProfileDef; } set { mSweptCurve = value.mIndex; } }
		public IfcAxis2Placement3D Position { get { return mDatabase[mPosition] as IfcAxis2Placement3D; } set { mPosition = (value == null ? 0 : value.mIndex); } }

		protected IfcSweptSurface() : base() { }
		protected IfcSweptSurface(DatabaseIfc db, IfcSweptSurface s, DuplicateOptions options) : base(db, s, options)
		{
			SweptCurve = db.Factory.Duplicate(s.SweptCurve, options) as IfcProfileDef;
			if(s.mPosition > 0)
				Position = db.Factory.Duplicate(s.Position) as IfcAxis2Placement3D;
		}
		protected IfcSweptSurface(IfcProfileDef sweptCurve) : base(sweptCurve.mDatabase) { SweptCurve = sweptCurve; if (sweptCurve.mDatabase.Release < ReleaseVersion.IFC4) Position = sweptCurve.mDatabase.Factory.XYPlanePlacement; }
		protected IfcSweptSurface(IfcProfileDef sweptCurve, IfcAxis2Placement3D position) : this(sweptCurve) { Position = (position == null && mDatabase.Release < ReleaseVersion.IFC4 ? new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase,0,0,0)) : position); }
	}
	[Serializable]
	public partial class IfcSwitchingDevice : IfcFlowController //IFC4
	{
		internal IfcSwitchingDeviceTypeEnum mPredefinedType = IfcSwitchingDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcSwitchingDeviceTypeEnum;
		public IfcSwitchingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSwitchingDevice() : base() { }
		internal IfcSwitchingDevice(DatabaseIfc db, IfcSwitchingDevice d, DuplicateOptions options) : base(db, d, options) { mPredefinedType = d.mPredefinedType; }
		public IfcSwitchingDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcSwitchingDeviceType : IfcFlowControllerType
	{
		internal IfcSwitchingDeviceTypeEnum mPredefinedType = IfcSwitchingDeviceTypeEnum.NOTDEFINED;// : IfcFlowMeterTypeEnum;
		public IfcSwitchingDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSwitchingDeviceType() : base() { }
		internal IfcSwitchingDeviceType(DatabaseIfc db, IfcSwitchingDeviceType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcSwitchingDeviceType(DatabaseIfc m, string name, IfcSwitchingDeviceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcSymbolStyle // IfcPresentationStyleSelect DEPRECATED IFC4
	[Serializable]
	public partial class IfcSystem : IfcGroup //SUPERTYPE OF(ONEOF(IfcBuildingSystem, IfcDistributionSystem, IfcStructuralAnalysisModel, IfcZone))
	{
		//INVERSE
		internal IfcRelServicesBuildings mServicesBuildings = null;// : SET [0:1] OF IfcRelServicesBuildings FOR RelatingSystem  
		public IfcRelServicesBuildings ServicesBuildings { get { return mServicesBuildings; } set { mServicesBuildings = value; } }

		internal IfcSystem() : base() { }
		internal IfcSystem(DatabaseIfc db, IfcSystem s, DuplicateOptions options) : base(db, s, options)
		{
			if(options.DuplicateHost && s.mServicesBuildings != null)
			{
				IfcRelServicesBuildings rsb = db.Factory.Duplicate(s.mServicesBuildings, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcRelServicesBuildings;
				rsb.RelatingSystem = this;
			}
		}
		public IfcSystem(DatabaseIfc db, string name) : base(db, name) { }
		public IfcSystem(IfcSpatialElement spatial, string name) : base(spatial, name) { }
		public IfcSystem(IfcSystem system, string name) : base(system.Database, name)
		{
			system.AddAggregated(this);
		}
	}
	[Serializable]
	public partial class IfcSystemFurnitureElement : IfcFurnishingElement //IFC4
	{
		internal IfcSystemFurnitureElementTypeEnum mPredefinedType = IfcSystemFurnitureElementTypeEnum.NOTDEFINED;//: OPTIONAL IfcSystemFurnitureElementTypeEnum
		public IfcSystemFurnitureElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSystemFurnitureElement() : base() { }
		internal IfcSystemFurnitureElement(DatabaseIfc db, IfcSystemFurnitureElement f, DuplicateOptions options) : base(db, f, options) { mPredefinedType = f.mPredefinedType; }
		public IfcSystemFurnitureElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcSystemFurnitureElementType : IfcFurnishingElementType
	{
		internal IfcSystemFurnitureElementTypeEnum mPredefinedType = IfcSystemFurnitureElementTypeEnum.NOTDEFINED;//: OPTIONAL IfcSystemFurnitureElementTypeEnum
		public IfcSystemFurnitureElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcSystemFurnitureElementType() : base() { }
		internal IfcSystemFurnitureElementType(DatabaseIfc db, IfcSystemFurnitureElementType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcSystemFurnitureElementType(DatabaseIfc db, string name, IfcSystemFurnitureElementTypeEnum type) : base(db,name)
		{
			mPredefinedType = type;
			if (mDatabase.mRelease < ReleaseVersion.IFC4 && string.IsNullOrEmpty(ElementType) && type != IfcSystemFurnitureElementTypeEnum.NOTDEFINED)
				ElementType = type.ToString();
		}
	}
}
