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
	public partial class IfcSanitaryTerminal : IfcFlowTerminal
	{
		private IfcSanitaryTerminalTypeEnum mPredefinedType = IfcSanitaryTerminalTypeEnum.NOTDEFINED;// : OPTIONAL IfcSanitaryTerminalTypeEnum; 
		public IfcSanitaryTerminalTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSanitaryTerminalTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSanitaryTerminal() : base() { }
		internal IfcSanitaryTerminal(DatabaseIfc db, IfcSanitaryTerminal t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcSanitaryTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcSanitaryTerminalType : IfcFlowTerminalType
	{
		private IfcSanitaryTerminalTypeEnum mPredefinedType = IfcSanitaryTerminalTypeEnum.NOTDEFINED;// : IfcSanitaryTerminalTypeEnum; 
		public IfcSanitaryTerminalTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSanitaryTerminalTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSanitaryTerminalType() : base() { }
		internal IfcSanitaryTerminalType(DatabaseIfc db, IfcSanitaryTerminalType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcSanitaryTerminalType(DatabaseIfc db, string name, IfcSanitaryTerminalTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcScheduleTimeControl : IfcControl // DEPRECATED IFC4
	{
		internal IfcDateTimeSelect mActualStart, mEarlyStart, mLateStart, mScheduleStart, mActualFinish, mEarlyFinish, mLateFinish, mScheduleFinish;// OPTIONAL  IfcDateTimeSelect;
		internal double mScheduleDuration = double.NaN, mActualDuration = double.NaN, mRemainingTime = double.NaN, mFreeFloat = double.NaN, mTotalFloat = double.NaN;//	 OPTIONAL IfcTimeMeasure;
		internal bool mIsCritical;//	 :	OPTIONAL BOOLEAN;
		internal IfcDateTimeSelect mStatusTime;//	: 	OPTIONAL IfcDateTimeSelect
		internal double mStartFloat, mFinishFloat;//	 OPTIONAL IfcTimeMeasure; 
		internal double mCompletion = double.NaN;//	 :	OPTIONAL IfcPositiveRatioMeasure; 
		//INVERSE
		internal IfcRelAssignsTasks mScheduleTimeControlAssigned = null;//	 : 	IfcRelAssignsTasks FOR TimeForTask;

		public IfcDateTimeSelect ActualStart { get { return mActualStart; } set { mActualStart = value;  } }
		public IfcDateTimeSelect EarlyStart { get { return mEarlyStart; } set { mEarlyStart = value;  } }
		public IfcDateTimeSelect LateStart { get { return mLateStart; } set { mLateStart = value;  } }
		public IfcDateTimeSelect ScheduleStart { get { return mScheduleStart; } set { mScheduleStart = value;  } }
		public IfcDateTimeSelect ActualFinish { get { return mActualFinish; } set { mActualFinish = value; } }
		public IfcDateTimeSelect EarlyFinish { get { return mEarlyFinish; } set { mEarlyFinish = value; } }
		public IfcDateTimeSelect LateFinish { get { return mLateFinish; } set { mLateFinish = value; } }
		public IfcDateTimeSelect ScheduleFinish { get { return mScheduleFinish; } set { mScheduleFinish = value; } }
		public double ScheduleDuration { get { return mScheduleDuration; } set { mScheduleDuration = value; } }
		public double ActualDuration { get { return mActualDuration; } set { mActualDuration = value; } }
		public double RemainingTime { get { return mRemainingTime; } set { mRemainingTime = value; } }
		public double FreeFloat { get { return mFreeFloat; } set { mFreeFloat = value; } }
		public double TotalFloat { get { return mTotalFloat; } set { mTotalFloat = value; } }
		public bool IsCritical { get { return mIsCritical; } set { mIsCritical = value; } }
		public IfcDateTimeSelect StatusTime { get { return mStatusTime; } set { mStatusTime = value; } }
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
		internal string mName = "";//	 :	OPTIONAL IfcLabel;
		internal IfcDataOriginEnum mDataOrigin = IfcDataOriginEnum.NOTDEFINED;// OPTIONAL : IfcDataOriginEnum;
		internal string mUserDefinedDataOrigin = "";//:	OPTIONAL IfcLabel; 

		public string Name { get { return mName; } set { mName = value; } } 
		public IfcDataOriginEnum DataOrigin { get { return mDataOrigin; } set { mDataOrigin = value; } }
		public string UserDefinedDataOrigin { get { return mUserDefinedDataOrigin; } set { mUserDefinedDataOrigin = value; } }

		protected IfcSchedulingTime() : base() { }
		protected IfcSchedulingTime(DatabaseIfc db, IfcSchedulingTime t, DuplicateOptions options) : base(db,t) 
		{
			mName = t.mName; 
			mDataOrigin = t.mDataOrigin; 
			mUserDefinedDataOrigin = t.mUserDefinedDataOrigin;
		}
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
	public partial class IfcSecondOrderPolynomialSpiral : IfcSpiral
	{
		private double mQuadraticTerm = 0; //: IfcLengthMeasure;
		private double mLinearTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mConstantTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;

		public double QuadraticTerm { get { return mQuadraticTerm; } set { mQuadraticTerm = value; } }
		public double LinearTerm { get { return mLinearTerm; } set { mLinearTerm = value; } }
		public double ConstantTerm { get { return mConstantTerm; } set { mConstantTerm = value; } }

		public IfcSecondOrderPolynomialSpiral() : base() { }
		internal IfcSecondOrderPolynomialSpiral(DatabaseIfc db, IfcSecondOrderPolynomialSpiral curve, DuplicateOptions options)
			: base(db, curve, options) { QuadraticTerm = curve.QuadraticTerm; LinearTerm = curve.LinearTerm; ConstantTerm = curve.ConstantTerm; }
		public IfcSecondOrderPolynomialSpiral(IfcAxis2Placement position, double qubicTerm)
			: base(position) { QuadraticTerm = qubicTerm; }
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
		[Obsolete("REVISED IFC4x3RC3", false)]
		internal LIST<IfcPointByDistanceExpression> mCrossSectionPositions_OBSOLETE = new LIST<IfcPointByDistanceExpression>();// : LIST [2:?] OF IfcDistanceExpression;
		[Obsolete("REVISED IFC4x3RC4", false)]
		internal LIST<IfcCurveMeasureSelect> mCrossSectionPositionMeasures_OBSOLETE = new LIST<IfcCurveMeasureSelect>();// : LIST [2:?] OF IfcCurveMeasureSelect;
		internal LIST<IfcAxis2PlacementLinear> mCrossSectionPositions = new LIST<IfcAxis2PlacementLinear>();// : LIST [2:?] OF IfcAxis2PlacementLinear;
		[Obsolete("REVISED IFC4x3", false)]
		internal bool mFixedAxisVertical;// : IfcBoolean

		public LIST<IfcAxis2PlacementLinear> CrossSectionPositions { get { return mCrossSectionPositions; } set { mCrossSectionPositions.Clear(); if (value != null) CrossSectionPositions = value; } }
		[Obsolete("REVISED IFC4x3", false)]
		public bool FixedAxisVertical { get { return mFixedAxisVertical; } set { mFixedAxisVertical = value; } }

		internal IfcSectionedSolidHorizontal() : base() { }
		internal IfcSectionedSolidHorizontal(DatabaseIfc db, IfcSectionedSolidHorizontal s, DuplicateOptions options) : base(db, s, options)
		{
			CrossSectionPositions.AddRange(s.CrossSectionPositions.Select(x=>db.Factory.Duplicate(x) as IfcAxis2PlacementLinear));
			mCrossSectionPositions_OBSOLETE.AddRange(s.mCrossSectionPositions_OBSOLETE.Select(x => db.Factory.Duplicate(x) as IfcPointByDistanceExpression));
			mCrossSectionPositionMeasures_OBSOLETE.AddRange(s.mCrossSectionPositionMeasures_OBSOLETE);
			FixedAxisVertical = s.FixedAxisVertical;
		}
		public IfcSectionedSolidHorizontal(IfcCurve directrix, IEnumerable<IfcProfileDef> profiles, IEnumerable<IfcAxis2PlacementLinear> positions)
			: base(directrix, profiles)
		{
			CrossSectionPositions.AddRange(positions);
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
		public LIST<IfcProfileDef> CrossSections { get { return mCrossSections; } }
		public LIST<IfcAxis2Placement3D> CrossSectionPositions { get { return mCrossSectionPositions; } }

		internal IfcSectionedSpine() : base() { }
		internal IfcSectionedSpine(DatabaseIfc db, IfcSectionedSpine s, DuplicateOptions options) : base(db, s, options)
		{
			SpineCurve = db.Factory.Duplicate(s.SpineCurve) as IfcCompositeCurve;
			CrossSections.AddRange(s.CrossSections.ConvertAll(x => db.Factory.Duplicate(x) as IfcProfileDef));
			CrossSectionPositions.AddRange(s.CrossSectionPositions.ConvertAll(x=>db.Factory.Duplicate(x) as IfcAxis2Placement3D));
		}
		public IfcSectionedSpine(IfcCompositeCurve spine, IEnumerable<IfcProfileDef> crossSections, IEnumerable<IfcAxis2Placement3D> positions)
		{
			SpineCurve = spine;
			CrossSections.AddRange(crossSections);
			CrossSectionPositions.AddRange(positions);
		}
	}
	[Serializable]
	public partial class IfcSectionedSurface : IfcSurface 
	{
		private IfcCurve mDirectrix = null; //: IfcCurve;
		[Obsolete("REVISED IFC4x3", false)]
		private LIST<IfcPointByDistanceExpression> mCrossSectionPositions_OBSOLETE = new LIST<IfcPointByDistanceExpression>(); //: LIST[2:?] OF IfcDistanceExpression;
		internal LIST<IfcAxis2PlacementLinear> mCrossSectionPositions = new LIST<IfcAxis2PlacementLinear>();// : LIST [2:?] OF IfcAxis2PlacementLinear;
		private LIST<IfcProfileDef> mCrossSections = new LIST<IfcProfileDef>(); //: LIST[2:?] OF IfcProfileDef;
		[Obsolete("REVISED IFC4x3", false)]
		private bool mFixedAxisVertical = false; //: IfcBoolean;

		public IfcCurve Directrix { get { return mDirectrix; } set { mDirectrix = value; } }
		public LIST<IfcAxis2PlacementLinear> CrossSectionPositions { get { return mCrossSectionPositions; } }
		public LIST<IfcProfileDef> CrossSections { get { return mCrossSections; } }
		[Obsolete("REVISED IFC4x3", false)]
		public bool FixedAxisVertical { get { return mFixedAxisVertical; } set { mFixedAxisVertical = value; } }
		[Obsolete("REVISED IFC4x3", false)]
		public LIST<IfcPointByDistanceExpression> CrossSectionPositions_OBSOLETE { get { return mCrossSectionPositions_OBSOLETE; } }

		public IfcSectionedSurface() : base() { }
		internal IfcSectionedSurface(DatabaseIfc db, IfcSectionedSurface s, DuplicateOptions options) : base(db, s, options)
		{
			Directrix = db.Factory.Duplicate(s.Directrix) as IfcCurve;
			mCrossSectionPositions_OBSOLETE.AddRange(s.mCrossSectionPositions_OBSOLETE.ConvertAll(x => db.Factory.Duplicate(x) as IfcPointByDistanceExpression));
			CrossSectionPositions.AddRange(s.CrossSectionPositions.ConvertAll(x => db.Factory.Duplicate(x) as IfcAxis2PlacementLinear));
			CrossSections.AddRange(s.CrossSections.ConvertAll(x => db.Factory.Duplicate(x) as IfcProfileDef));
			FixedAxisVertical = s.FixedAxisVertical;
		}

		public IfcSectionedSurface(IfcCurve directrix, IEnumerable<IfcAxis2PlacementLinear> crossSectionPositions, IEnumerable<IfcProfileDef> crossSections, bool fixedAxisVertical)
				: base(directrix.Database)
		{
			Directrix = directrix;
			CrossSectionPositions.AddRange(crossSectionPositions);
			CrossSections.AddRange(crossSections);
			FixedAxisVertical = fixedAxisVertical;
		}
		[Obsolete("REVISED IFC4x3RC4", false)]
		public IfcSectionedSurface(IfcCurve directrix, IEnumerable<IfcPointByDistanceExpression> crossSectionPositions, IEnumerable<IfcProfileDef> crossSections, bool fixedAxisVertical)
			: base(directrix.Database)
		{
			Directrix = directrix;
			mCrossSectionPositions_OBSOLETE.AddRange(crossSectionPositions);
			CrossSections.AddRange(crossSections);
			FixedAxisVertical = fixedAxisVertical;
		}
	}
	[Serializable]
	public partial class IfcSectionProperties : IfcPreDefinedProperties // IFC2x3 BaseClassIfc
	{
		internal IfcSectionTypeEnum mSectionType = IfcSectionTypeEnum.UNIFORM;// : IfcSectionTypeEnum;
		internal IfcProfileDef mStartProfile;// IfcProfileDef;
		internal IfcProfileDef mEndProfile;// : OPTIONAL IfcProfileDef;

		public IfcSectionTypeEnum SectionType { get { return mSectionType; } set { mSectionType = value; } }
		public IfcProfileDef StartProfile { get { return mStartProfile; } set { mStartProfile = value; } }
		public IfcProfileDef EndProfile { get { return mEndProfile; } set { mStartProfile = value; } }

		internal IfcSectionProperties() : base() { }
		internal IfcSectionProperties(DatabaseIfc db, IfcSectionProperties p, DuplicateOptions options) : base(db, p, options) { mSectionType = p.mSectionType; mStartProfile = db.Factory.Duplicate(p.mStartProfile); mEndProfile = db.Factory.Duplicate(p.mEndProfile); }
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
		internal IfcSectionProperties mSectionDefinition;//	:	IfcSectionProperties;
		internal SET<IfcReinforcementBarProperties> mCrossSectionReinforcementDefinitions = new SET<IfcReinforcementBarProperties>();// : SET [1:?] OF IfcReinforcementBarProperties;

		public double LongitudinalStartPosition { get { return mLongitudinalStartPosition; } set { mLongitudinalStartPosition = value; } }
		public double LongitudinalEndPosition { get { return mLongitudinalEndPosition; } set { mLongitudinalEndPosition = value; } }
		public double TransversePosition { get { return mTransversePosition; } set { mTransversePosition = value; } }
		public IfcReinforcingBarRoleEnum ReinforcementRole { get { return mReinforcementRole; } set { mReinforcementRole = value; } }
		public IfcSectionProperties SectionDefinition { get { return mSectionDefinition; } set { mSectionDefinition = value; } }
		public SET<IfcReinforcementBarProperties> CrossSectionReinforcementDefinitions { get { return mCrossSectionReinforcementDefinitions; } } 

		internal IfcSectionReinforcementProperties() : base() { }
		internal IfcSectionReinforcementProperties(DatabaseIfc db, IfcSectionReinforcementProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mLongitudinalStartPosition = p.mLongitudinalStartPosition;
			mLongitudinalEndPosition = p.mLongitudinalEndPosition;
			mTransversePosition = p.mTransversePosition;
			mReinforcementRole = p.mReinforcementRole;
			mSectionDefinition = db.Factory.Duplicate(p.mSectionDefinition);
			mCrossSectionReinforcementDefinitions.AddRange(p.mCrossSectionReinforcementDefinitions.Select(x=>db.Factory.Duplicate(x)));
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
			mCrossSectionReinforcementDefinitions.Add(definition);
		}
		public IfcSectionReinforcementProperties(double longitudinalStart, double longitudinalEnd, IfcReinforcingBarRoleEnum role, IfcSectionProperties properties, IEnumerable<IfcReinforcementBarProperties> definitions)
			: this(longitudinalStart, longitudinalEnd, role, properties)
		{
			mCrossSectionReinforcementDefinitions.AddRange(definitions);
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
			BaseCurve = db.Factory.Duplicate(segmentedReferenceCurve.BaseCurve, options) as IfcBoundedCurve;
			EndPoint = db.Factory.Duplicate(segmentedReferenceCurve.EndPoint, options) as IfcPlacement;
			IfcLinearElement linearElement = segmentedReferenceCurve.representationOf<IfcLinearElement>();
			if (linearElement != null)
				db.Factory.Duplicate(linearElement, new DuplicateOptions(options) {  DuplicateHost = true });
			else
			{
				IfcPositioningElement positioningElement = segmentedReferenceCurve.representationOf<IfcPositioningElement>();
				if (positioningElement != null)
					db.Factory.Duplicate(positioningElement, new DuplicateOptions(options) { DuplicateHost = true });
			}
		}
		public IfcSegmentedReferenceCurve(IfcBoundedCurve baseCurve, IEnumerable<IfcCurveSegment> segments)
			: base(segments) { BaseCurve = baseCurve; }
	}
	[Serializable]
	public partial class IfcSensor : IfcDistributionControlElement //IFC4  
	{
		private IfcSensorTypeEnum mPredefinedType = IfcSensorTypeEnum.NOTDEFINED;
		public IfcSensorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSensorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		
		internal IfcSensor() : base() { }
		internal IfcSensor(DatabaseIfc db, IfcSensor s, DuplicateOptions options) : base(db, s, options) { PredefinedType = s.PredefinedType; }
		public IfcSensor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcSensorType : IfcDistributionControlElementType
	{
		private IfcSensorTypeEnum mPredefinedType = IfcSensorTypeEnum.NOTDEFINED;// : IfcSensorTypeEnum; 
		public IfcSensorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSensorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSensorType() : base() { }
		internal IfcSensorType(DatabaseIfc db, IfcSensorType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcSensorType(DatabaseIfc db, string name, IfcSensorTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcServiceLife // DEPRECATED IFC4
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcServiceLifeFactor // DEPRECATED IFC4
	[Serializable]
	public partial class IfcSeventhOrderPolynomialSpiral : IfcSpiral
	{
		private double mSepticTerm = 0; //: IfcLengthMeasure;
		private double mSexticTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mQuinticTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mQuarticTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mCubicTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mQuadraticTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mLinearTerm = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mConstantTerm = double.NaN; //: OPTIONAL IfcReal;

		public double SepticTerm { get { return mSepticTerm; } set { mSepticTerm = value; } }
		public double SexticTerm { get { return mSexticTerm; } set { mSexticTerm = value; } }
		public double QuinticTerm { get { return mQuinticTerm; } set { mQuinticTerm = value; } }
		public double QuarticTerm { get { return mQuarticTerm; } set { mQuarticTerm = value; } }
		public double CubicTerm { get { return mCubicTerm; } set { mCubicTerm = value; } }
		public double QuadraticTerm { get { return mQuadraticTerm; } set { mQuadraticTerm = value; } }
		public double LinearTerm { get { return mLinearTerm; } set { mLinearTerm = value; } }
		public double ConstantTerm { get { return mConstantTerm; } set { mConstantTerm = value; } }

		public IfcSeventhOrderPolynomialSpiral() : base() { }
		internal IfcSeventhOrderPolynomialSpiral(DatabaseIfc db, IfcSeventhOrderPolynomialSpiral spiral, DuplicateOptions options)
			: base(db, spiral, options) 
		{
			SepticTerm = spiral.SepticTerm;
			SexticTerm = spiral.SexticTerm;
			QuinticTerm = spiral.QuinticTerm;
			QuarticTerm = spiral.QuarticTerm; 
			CubicTerm = spiral.CubicTerm; 
			QuadraticTerm = spiral.QuadraticTerm; 
			LinearTerm = spiral.LinearTerm;
			ConstantTerm = spiral.ConstantTerm;
		}
		public IfcSeventhOrderPolynomialSpiral(IfcAxis2Placement position, double septicTerm)
			: base(position) { SepticTerm = septicTerm; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4)]
	public partial class IfcShadingDevice : IfcBuiltElement
	{
		private IfcShadingDeviceTypeEnum mPredefinedType = IfcShadingDeviceTypeEnum.NOTDEFINED;//: OPTIONAL IfcShadingDeviceTypeEnum; 
		public IfcShadingDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcShadingDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcShadingDevice() : base() { }
		internal IfcShadingDevice(DatabaseIfc db, IfcShadingDevice d, DuplicateOptions options) : base(db, d, options) { PredefinedType = d.PredefinedType; }
		public IfcShadingDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4)]
	public partial class IfcShadingDeviceType : IfcBuiltElementType
	{
		private IfcShadingDeviceTypeEnum mPredefinedType = IfcShadingDeviceTypeEnum.NOTDEFINED;
		public IfcShadingDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcShadingDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcShadingDeviceType() : base() { }
		internal IfcShadingDeviceType(DatabaseIfc db, IfcShadingDeviceType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.mPredefinedType; }
		public IfcShadingDeviceType(DatabaseIfc db, string name, IfcShadingDeviceTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcShapeAspect : BaseClassIfc, IfcResourceObjectSelect, NamedObjectIfc
	{
		internal LIST<IfcShapeModel> mShapeRepresentations = new LIST<IfcShapeModel>();// : LIST [1:?] OF IfcShapeModel;
		internal string mName = "";// : OPTIONAL IfcLabel;
		internal string mDescription = "";// : OPTIONAL IfcText;
		private IfcLogicalEnum mProductDefinitional;// : LOGICAL;
		internal IfcProductRepresentationSelect mPartOfProductDefinitionShape;// IFC4 OPTIONAL IfcProductRepresentationSelect IFC2x3 IfcProductDefinitionShape;
		//	INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;

		public LIST<IfcShapeModel> ShapeRepresentations { get { return mShapeRepresentations; } }
		public string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public IfcLogicalEnum ProductDefinitional { get { return mProductDefinitional; } set { mProductDefinitional = value; } }
		public IfcProductRepresentationSelect PartOfProductDefinitionShape { get { return mPartOfProductDefinitionShape; } set { mPartOfProductDefinitionShape = value; if (value != null) value.HasShapeAspects.Add(this); } }
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } }

		internal IfcShapeAspect() : base() { }
		internal IfcShapeAspect(DatabaseIfc db, IfcShapeAspect a) : base(db,a)
		{
			ShapeRepresentations.AddRange(a.ShapeRepresentations.Select(x=> db.Factory.Duplicate(x) as IfcShapeModel));
			mName = a.mName;
			mDescription = a.mDescription;
			mProductDefinitional = a.mProductDefinitional;
		}
		public IfcShapeAspect(List<IfcShapeModel> shapeRepresentations) : base(shapeRepresentations[0].Database) { mShapeRepresentations.AddRange(shapeRepresentations); }
		public IfcShapeAspect(IfcShapeModel shapeRepresentation) : base(shapeRepresentation.mDatabase) { mShapeRepresentations.Add(shapeRepresentation); }

		protected override void initialize()
		{
			base.initialize();
			mShapeRepresentations.CollectionChanged += mShapeRepresentations_CollectionChanged;
		}
		private void mShapeRepresentations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcShapeModel m in e.NewItems)
					m.mOfShapeAspect = this;
			}
			if (e.OldItems != null)
			{
				foreach (IfcShapeModel m in e.OldItems)
					m.OfShapeAspect = null;
			}
		}

		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { }// mHasConstraintRelationships.Add(constraintRelationship); }
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

		internal IfcProfileDef sweptProfileFromReprepesentation()
		{
			if (Items.Count != 1)
				return null;

			IfcRepresentationItem item = Items.First();
			IfcSweptAreaSolid sweptAreaSolid = item as IfcSweptAreaSolid;
			if (sweptAreaSolid != null)
				return sweptAreaSolid.SweptArea;
			IfcBooleanResult booleanResult = item as IfcBooleanResult;
			if (booleanResult != null)
				return booleanResult.underlyingSweptProfile();
			IfcMappedItem mappedItem = item as IfcMappedItem;
			if (mappedItem != null)
				return mappedItem.MappingSource.MappedRepresentation.sweptProfileFromReprepesentation();
			return null;
		}
	}
	[Serializable]
	public partial class IfcShapeRepresentation : IfcShapeModel
	{
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
					RepresentationType = ShapeRepresentationType.Surface3D.ToString();
			}
		}
		public IfcShapeRepresentation(IfcSectionedSurface surface) : base(surface) { RepresentationType = ShapeRepresentationType.SectionedSurface.ToString(); }
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

			System.Diagnostics.Trace.WriteLine("XX Error Can't identify " + representationItem.ToString() + " as shape representation!");
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
		internal IfcShellBasedSurfaceModel(DatabaseIfc db, IfcShellBasedSurfaceModel m, DuplicateOptions options) : base(db, m, options)
		{
			mSbsmBoundary.AddRange(m.mSbsmBoundary.Select(x => db.Factory.Duplicate(x, options)));
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
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcSign : IfcElementComponent
	{
		private IfcSignTypeEnum mPredefinedType = IfcSignTypeEnum.NOTDEFINED; //: OPTIONAL IfcSignTypeEnum;
		public IfcSignTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSignTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcSign() : base() { }
		public IfcSign(DatabaseIfc db) : base(db) { }
		public IfcSign(DatabaseIfc db, IfcSign sign, DuplicateOptions options) : base(db, sign, options) { PredefinedType = sign.PredefinedType; }
		public IfcSign(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcSignal : IfcFlowTerminal
	{
		private IfcSignalTypeEnum mPredefinedType = IfcSignalTypeEnum.NOTDEFINED; //: OPTIONAL IfcSignalTypeEnum;
		public IfcSignalTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSignalTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcSignal() : base() { }
		public IfcSignal(DatabaseIfc db) : base(db) { }
		public IfcSignal(DatabaseIfc db, IfcSignal signal, DuplicateOptions options) : base(db, signal, options) { PredefinedType = signal.PredefinedType; }
		public IfcSignal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcSignalType : IfcFlowTerminalType
	{
		private IfcSignalTypeEnum mPredefinedType = IfcSignalTypeEnum.NOTDEFINED; //: IfcSignalTypeEnum;
		public IfcSignalTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSignalTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcSignalType() : base() { }
		public IfcSignalType(DatabaseIfc db, IfcSignalType signalType, DuplicateOptions options) : base(db, signalType, options) { PredefinedType = signalType.PredefinedType; }
		public IfcSignalType(DatabaseIfc db, string name, IfcSignalTypeEnum predefinedType) : base(db)
		{
			Name = name;
			PredefinedType = predefinedType;
		}
	}

	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcSignType : IfcElementComponentType
	{
		private IfcSignTypeEnum mPredefinedType = IfcSignTypeEnum.NOTDEFINED; //: IfcSignTypeEnum;
		public IfcSignTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSignTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

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
		protected IfcSimpleProperty(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Serializable]
	public partial class IfcSimplePropertyTemplate : IfcPropertyTemplate
	{
		private IfcSimplePropertyTemplateTypeEnum mTemplateType = IfcSimplePropertyTemplateTypeEnum.NOTDEFINED;// : OPTIONAL IfcSimplePropertyTemplateTypeEnum;
		internal string mPrimaryMeasureType = "";// : OPTIONAL IfcLabel;
		internal string mSecondaryMeasureType = "";// : OPTIONAL IfcLabel;
		internal IfcPropertyEnumeration mEnumerators = null;// : OPTIONAL IfcPropertyEnumeration;
		internal IfcUnit mPrimaryUnit = null, mSecondaryUnit = null;// : OPTIONAL IfcUnit; 
		internal string mExpression = "";// : OPTIONAL IfcLabel;
		internal IfcStateEnum mAccessState = IfcStateEnum.NOTDEFINED;//	:	OPTIONAL IfcStateEnum;

		public IfcSimplePropertyTemplateTypeEnum TemplateType { get { return mTemplateType; } set { mTemplateType = value; } }
		public string PrimaryMeasureType { get { return mPrimaryMeasureType; } set { mPrimaryMeasureType = value; } }
		public string SecondaryMeasureType { get { return mSecondaryMeasureType; } set { mSecondaryMeasureType = value; } }
		public IfcPropertyEnumeration Enumerators { get { return mEnumerators; } set { mEnumerators = value; } }
		public IfcUnit PrimaryUnit { get { return mPrimaryUnit; } set { mPrimaryUnit = value; } }
		public IfcUnit SecondaryUnit { get { return mSecondaryUnit; } set { mSecondaryUnit = value; } }
		public string Expression { get { return mExpression; } set { mExpression = value; } }
		public IfcStateEnum AccessState { get { return mAccessState; } set { mAccessState = value; } }

		internal IfcSimplePropertyTemplate() : base() { }
		internal IfcSimplePropertyTemplate(DatabaseIfc db, IfcSimplePropertyTemplate s, DuplicateOptions options) : base(db, s, options)
		{
			mTemplateType = s.mTemplateType;
			mPrimaryMeasureType = s.mPrimaryMeasureType;
			mSecondaryMeasureType = s.mSecondaryMeasureType;
			if (s.mEnumerators != null)
				Enumerators = db.Factory.Duplicate(s.Enumerators);	
			if (s.mPrimaryUnit != null)
				PrimaryUnit = db.Factory.Duplicate<IfcUnit>(s.mPrimaryUnit);
			if (s.mSecondaryUnit != null)
				SecondaryUnit = db.Factory.Duplicate<IfcUnit>(s.mSecondaryUnit);
			mExpression = s.mExpression;
			mAccessState = s.mAccessState;
		}
		public IfcSimplePropertyTemplate(DatabaseIfc db, string name) : base(db,name) { }
	}
	[Serializable]
	public partial class IfcSineSpiral : IfcSpiral
	{
		private double mSineTerm = 0; //: IfcLengthMeasure;
		private double mLinearTerm = double.NaN; //: OPTIONAL IfcReal;
		private double mConstantTerm = double.NaN; //: OPTIONAL IfcReal;

		public double SineTerm { get { return mSineTerm; } set { mSineTerm = value; } }
		public double LinearTerm { get { return mLinearTerm; } set { mLinearTerm = value; } }
		public double ConstantTerm { get { return mConstantTerm; } set { mConstantTerm = value; } }

		public IfcSineSpiral() : base() { }
		internal IfcSineSpiral(DatabaseIfc db, IfcSineSpiral sine, DuplicateOptions options)
			: base(db, sine, options) { SineTerm = sine.SineTerm; LinearTerm = sine.LinearTerm; ConstantTerm = sine.ConstantTerm; }
		public IfcSineSpiral(IfcAxis2Placement position, double sineTerm)
			: base(position) { SineTerm = sineTerm; }
	}
	[Serializable]
	public partial class IfcSite : IfcSpatialStructureElement
	{
		internal IfcCompoundPlaneAngleMeasure mRefLatitude = null;// : OPTIONAL IfcCompoundPlaneAngleMeasure;
		internal IfcCompoundPlaneAngleMeasure mRefLongitude = null;// : OPTIONAL IfcCompoundPlaneAngleMeasure;
		internal double mRefElevation = double.NaN;// : OPTIONAL IfcLengthMeasure;
		[Obsolete("DEPRECATED IFC4", false)]
		internal string mLandTitleNumber = "";// : OPTIONAL IfcLabel;
		internal IfcPostalAddress mSiteAddress;// : OPTIONAL IfcPostalAddress; 

		public IfcCompoundPlaneAngleMeasure RefLatitude { get { return mRefLatitude; } set { mRefLatitude = value; } }
		public IfcCompoundPlaneAngleMeasure RefLongitude { get { return mRefLongitude; } set { mRefLongitude = value; } }
		public double RefElevation { get { return mRefElevation; } set { mRefElevation = value; } }
		[Obsolete("DEPRECATED IFC4", false)]
		public string LandTitleNumber { get { return mLandTitleNumber; } set { mLandTitleNumber = value; } }
		public IfcPostalAddress SiteAddress { get { return mSiteAddress; } set { mSiteAddress = value; } }

		internal IfcSite() : base() { }
		internal IfcSite(DatabaseIfc db, IfcSpatialElement spatial, DuplicateOptions options) : base(db, spatial, options) { }
		internal IfcSite(DatabaseIfc db, IfcSite site, DuplicateOptions options) : base(db, site, options) 
		{ 
			mRefLatitude = site.mRefLatitude; 
			mRefLongitude = site.mRefLongitude; 
			mRefElevation = site.mRefElevation; 
			mLandTitleNumber = site.mLandTitleNumber; 
			if (site.mSiteAddress != null) 
				SiteAddress = db.Factory.Duplicate(site.SiteAddress, options); 
		}
		public IfcSite(DatabaseIfc db, string name) : base(db.Factory.RootPlacement) { Name = name; }
		public IfcSite(string name, IfcObjectPlacement placement) : base(placement) { Name = name; }
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
		internal IfcSIUnit(DatabaseIfc db, IfcSIUnit u, DuplicateOptions options) : base(db, u, options) { mPrefix = u.mPrefix; mName = u.mName; }
		public IfcSIUnit(DatabaseIfc db, IfcUnitEnum unitEnum, IfcSIPrefix prefix, IfcSIUnitName name) : base(db, unitEnum, false) { mPrefix = prefix; mName = name; }
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
		private IfcSlabTypeEnum mPredefinedType = IfcSlabTypeEnum.NOTDEFINED;// : OPTIONAL IfcSlabTypeEnum 
		public IfcSlabTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSlabTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSlab() : base() { }
		internal IfcSlab(DatabaseIfc db, IfcSlab s, DuplicateOptions options) : base(db, s, options) { PredefinedType = s.PredefinedType; }
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
		private IfcSlabTypeEnum mPredefinedType = IfcSlabTypeEnum.NOTDEFINED;
		public IfcSlabTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSlabTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcSlabType() : base() { }
		public IfcSlabType(DatabaseIfc db, string name, IfcSlabTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
		internal IfcSlabType(DatabaseIfc db, IfcSlabType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.mPredefinedType; }
		public IfcSlabType(string name, IfcMaterialLayerSet ls, IfcSlabTypeEnum type) : base(ls.mDatabase) { Name = name; PredefinedType = type; MaterialSelect = ls; }
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
		private IfcSolarDeviceTypeEnum mPredefinedType = IfcSolarDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcSolarDeviceTypeEnum;
		public IfcSolarDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSolarDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSolarDevice() : base() { }
		internal IfcSolarDevice(DatabaseIfc db, IfcSolarDevice d, DuplicateOptions options) : base(db, d, options) { PredefinedType = d.PredefinedType; }
		public IfcSolarDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcSolarDeviceType : IfcEnergyConversionDeviceType
	{
		private IfcSolarDeviceTypeEnum mPredefinedType = IfcSolarDeviceTypeEnum.NOTDEFINED;// : IfcSolarDeviceTypeEnum; 
		public IfcSolarDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSolarDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSolarDeviceType() : base() { }
		internal IfcSolarDeviceType(DatabaseIfc db, IfcSolarDeviceType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcSolarDeviceType(DatabaseIfc db, string name, IfcSolarDeviceTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcSolidModel : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcSolidOrShell /* ABSTRACT SUPERTYPE OF (ONEOF(IfcCsgSolid ,IfcManifoldSolidBrep,IfcSweptAreaSolid,IfcSweptDiskSolid))*/
	{
		protected IfcSolidModel() : base() { }
		protected IfcSolidModel(DatabaseIfc db) : base(db) { }
		protected IfcSolidModel(DatabaseIfc db, IfcSolidModel p, DuplicateOptions options) : base(db, p, options) { }
	}
	public interface IfcSolidOrShell : IBaseClassIfc { } // SELECT(IfcSolidModel, IfcClosedShell);
	[Obsolete("RELEASE CANDIDATE IFC4X3", false)]
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcSolidStratum : IfcGeotechnicalStratum
	{
		public override string StepClassName { get { return (mDatabase.mRelease >= ReleaseVersion.IFC4X3 ? "IfcGeotechnicalStratum" : base.StepClassName); } }
		internal IfcSolidStratum() : base() { PredefinedType = IfcGeotechnicalStratumTypeEnum.SOLID; }
		internal IfcSolidStratum(DatabaseIfc db) : base(db) { PredefinedType = IfcGeotechnicalStratumTypeEnum.SOLID; }
		internal IfcSolidStratum(DatabaseIfc db, IfcSolidStratum solidStratum, DuplicateOptions options) 
			: base(db, solidStratum, options) { PredefinedType = IfcGeotechnicalStratumTypeEnum.SOLID; }
		internal IfcSolidStratum(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape shape) : base(host, placement, shape) { PredefinedType = IfcGeotechnicalStratumTypeEnum.SOLID; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcSoundProperties : IfcPropertySetDefinition // DEPRECATED IFC4
	{
		internal bool mIsAttenuating;// : IfcBoolean;
		internal IfcSoundScaleEnum mSoundScale = IfcSoundScaleEnum.NOTDEFINED;// : OPTIONAL IfcSoundScaleEnum
		internal LIST<IfcSoundValue> mSoundValues = new LIST<IfcSoundValue>();// : LIST [1:8] OF IfcSoundValue;

		public LIST<IfcSoundValue> SoundValues { get { return mSoundValues; } }

		internal IfcSoundProperties() : base() { }
		internal IfcSoundProperties(DatabaseIfc db, IfcSoundProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mIsAttenuating = p.mIsAttenuating;
			mSoundScale = p.mSoundScale;
			SoundValues.AddRange(p.SoundValues.Select(x=> db.Factory.Duplicate(x)));
		}
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcSoundValue : IfcPropertySetDefinition // DEPRECATED IFC4
	{
		internal IfcTimeSeries mSoundLevelTimeSeries;// : OPTIONAL IfcTimeSeries;
		internal double mFrequency;// : IfcFrequencyMeasure;
		internal double mSoundLevelSingleValue;// : OPTIONAL IfcDerivedMeasureValue; 

		public IfcTimeSeries SoundLevelTimeSeries { get { return mSoundLevelTimeSeries; } set { mSoundLevelTimeSeries = value; } }

		internal IfcSoundValue() : base() { }
		internal IfcSoundValue(DatabaseIfc db, IfcSoundValue v, DuplicateOptions options) : base(db, v, options)
		{
			if (v.mSoundLevelTimeSeries != null)
				SoundLevelTimeSeries = db.Factory.Duplicate(v.SoundLevelTimeSeries);
			mFrequency = v.mFrequency;
			mSoundLevelSingleValue = v.mSoundLevelSingleValue;
		}
	}
	[Serializable]
	public partial class IfcSpace : IfcSpatialStructureElement, IfcSpaceBoundarySelect
	{
		//internal IfcInternalOrExternalEnum mInteriorOrExteriorSpace = IfcInternalOrExternalEnum.NOTDEFINED;// : IfcInternalOrExternalEnum; replaced IFC4
		private IfcSpaceTypeEnum mPredefinedType = IfcSpaceTypeEnum.NOTDEFINED; 	//:	OPTIONAL IfcSpaceTypeEnum;
		internal double mElevationWithFlooring = double.NaN;// : OPTIONAL IfcLengthMeasure;
		//INVERSE
		internal SET<IfcRelCoversSpaces> mHasCoverings = new SET<IfcRelCoversSpaces>(); // : SET [0:?] OF IfcRelCoversSpaces FOR RelatedSpace;
		internal SET<IfcRelSpaceBoundary> mBoundedBy = new SET<IfcRelSpaceBoundary>();  //	BoundedBy : SET [0:?] OF IfcRelSpaceBoundary FOR RelatingSpace;

		public IfcSpaceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSpaceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public double ElevationWithFlooring { get { return mElevationWithFlooring; } set { mElevationWithFlooring = value; } }
		public SET<IfcRelCoversSpaces> HasCoverings { get { return mHasCoverings; } }
		public SET<IfcRelSpaceBoundary> BoundedBy { get { return mBoundedBy; } }

		internal IfcSpace() : base() { }
		internal IfcSpace(DatabaseIfc db, IfcSpace s, DuplicateOptions options) : base(db, s, options) { PredefinedType = s.PredefinedType; mElevationWithFlooring = s.mElevationWithFlooring; }
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
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcFlowTerminal" : base.StepClassName); } }
		private IfcSpaceHeaterTypeEnum mPredefinedType = IfcSpaceHeaterTypeEnum.NOTDEFINED;// OPTIONAL : IfcSpaceHeaterTypeEnum;
		public IfcSpaceHeaterTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSpaceHeaterTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSpaceHeater() : base() { }
		internal IfcSpaceHeater(DatabaseIfc db, IfcSpaceHeater h, DuplicateOptions options) : base(db, h, options) { PredefinedType = h.PredefinedType; }

		public IfcSpaceHeater(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcSpaceHeaterType : IfcFlowTerminalType
	{
		private IfcSpaceHeaterTypeEnum mPredefinedType = IfcSpaceHeaterTypeEnum.NOTDEFINED;// : IfcSpaceHeaterExchangerEnum; 
		public IfcSpaceHeaterTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSpaceHeaterTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSpaceHeaterType() : base() { }
		internal IfcSpaceHeaterType(DatabaseIfc db, IfcSpaceHeaterType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcSpaceHeaterType(DatabaseIfc db, string name, IfcSpaceHeaterTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcSpaceProgram : IfcControl // DEPRECATED IFC4
	{
		internal string mSpaceProgramIdentifier;// : IfcIdentifier;
		internal double mMaxRequiredArea, mMinRequiredArea;// : OPTIONAL IfcAreaMeasure;
		internal IfcSpatialStructureElement mRequestedLocation;// : OPTIONAL IfcSpatialStructureElement;
		internal double mStandardRequiredArea;// : IfcAreaMeasure; 
		public IfcSpatialStructureElement RequestedLocation { get { return mRequestedLocation; } set { mRequestedLocation = value; } }
		internal IfcSpaceProgram() : base() { }
		internal IfcSpaceProgram(DatabaseIfc db, IfcSpaceProgram p, DuplicateOptions options) : base(db, p, options)
		{
			mSpaceProgramIdentifier = p.mSpaceProgramIdentifier;
			mMaxRequiredArea = p.mMaxRequiredArea;
			mMinRequiredArea = p.mMinRequiredArea;
			if(p.mRequestedLocation != null)
				RequestedLocation = db.Factory.Duplicate( p.RequestedLocation);
			mStandardRequiredArea = p.mStandardRequiredArea;
		}
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcSpaceThermalLoadProperties // DEPRECATED IFC4
	[Serializable]
	public partial class IfcSpaceType : IfcSpatialStructureElementType
	{
		private IfcSpaceTypeEnum mPredefinedType = IfcSpaceTypeEnum.NOTDEFINED;
		private string mLongName = "";// : OPTIONAL IfcLabel; // Added IFC4

		public IfcSpaceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSpaceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public string LongName { get { return mLongName; } set { mLongName = value; } }

		internal IfcSpaceType() : base() { }
		internal IfcSpaceType(DatabaseIfc db, IfcSpaceType t, DuplicateOptions options) : base(db, t, options) 
		{ 
			PredefinedType = t.PredefinedType;
			mLongName = t.mLongName;
		}
		public IfcSpaceType(DatabaseIfc db, string name, IfcSpaceTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcSpatialElement : IfcProduct, IfcInterferenceSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcExternalSpatialStructureElement ,IfcSpatialStructureElement ,IfcSpatialZone))
	{
		private string mLongName = "";// : OPTIONAL IfcLabel; 
		//INVERSE
		internal SET<IfcRelContainedInSpatialStructure> mContainsElements = new SET<IfcRelContainedInSpatialStructure>();// : SET [0:?] OF IfcRelReferencedInSpatialStructure FOR RelatingStructure;
		internal SET<IfcRelServicesBuildings> mServicedBySystems = new SET<IfcRelServicesBuildings>();// : SET [0:?] OF IfcRelServicesBuildings FOR RelatedBuildings;	
		internal SET<IfcRelReferencedInSpatialStructure> mReferencesElements = new SET<IfcRelReferencedInSpatialStructure>();// : SET [0:?] OF IfcRelReferencedInSpatialStructure FOR RelatingStructure;
		internal SET<IfcRelInterferesElements> mIsInterferedByElements = new SET<IfcRelInterferesElements>();//	 :	SET OF IfcRelInterferesElements FOR RelatedElement;
		internal SET<IfcRelInterferesElements> mInterferesElements = new SET<IfcRelInterferesElements>();// :	SET OF IfcRelInterferesElements FOR RelatingElement;

		public string LongName { get { return mLongName; } set { mLongName = value; } }
		public SET<IfcRelContainedInSpatialStructure> ContainsElements { get { return mContainsElements; } }
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
				{
					foreach(IfcProduct obj in css.RelatedElements)
						db.Factory.Duplicate(obj, options);
				}
				DuplicateOptions optionsNoHost = new DuplicateOptions(options) { DuplicateHost = false };
				foreach(IfcSystem system in e.ServicedBySystems.Select(x=>x.RelatingSystem))
				{
					IfcSystem sys = db.Factory.Duplicate(system, optionsNoHost) as IfcSystem;
					if(sys.ServicesBuildings == null)
					{
						new IfcRelServicesBuildings(sys, this);
					}
				}
				foreach (var reference in e.ReferencesElements)
				{
					List<IfcSystem> systems = new List<IfcSystem>();
					foreach (var related in reference.RelatedElements)
					{
						IfcSystem system = related as IfcSystem;
						if (related != null)
						{
							IfcSystem sys = db.Factory.Duplicate(system, optionsNoHost) as IfcSystem;
							if (sys.ReferencedInStructures.Count == 0)
								systems.Add(sys);
						}
					}
					if (systems.Count > 0)
					{
						new IfcRelReferencedInSpatialStructure(systems, this) { GlobalId = reference.GlobalId, Name = reference.Name };
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
		protected IfcSpatialElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) 
		{ 
			if(placement == null)
			{
				IfcAxis2Placement3D relativePlacement = mDatabase.Factory.XYPlanePlacement;
				if (host is IfcProduct product && product.ObjectPlacement != null)
					ObjectPlacement = new IfcLocalPlacement(product.ObjectPlacement, relativePlacement);
				else
					ObjectPlacement = new IfcLocalPlacement(relativePlacement);
			}
		}
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
		internal string mElementType = "";// : OPTIONAL IfcLabel
		public string ElementType { get { return mElementType; } set { mElementType = value; } }

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

		private bool mWorkInLocalCoordinates = false;  //Identify Transform when ignoring placement for Site
		public void EnableLocalCoordinates() { mWorkInLocalCoordinates = true; }
		public void DisableLocalCoordinates() { mWorkInLocalCoordinates = false; }
		public bool WorkingInLocalCoordinates() { return mWorkInLocalCoordinates; }
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
		private IfcSpatialZoneTypeEnum mPredefinedType = IfcSpatialZoneTypeEnum.NOTDEFINED;
		public IfcSpatialZoneTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSpatialZoneTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		protected IfcSpatialZone() : base() { }
		protected IfcSpatialZone(DatabaseIfc db, IfcSpatialZone p, DuplicateOptions options) : base(db, p, options) { PredefinedType = p.PredefinedType; }
		public IfcSpatialZone(DatabaseIfc db, string name) : base(db.Factory.RootPlacement) { Name = name; }
		public IfcSpatialZone(IfcSpatialElement host, string name) : base(host, name) {  }
		internal IfcSpatialZone(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape shape) : base(host, placement, shape) { }
	}
	[Serializable]
	public partial class IfcSpatialZoneType : IfcSpatialElementType  //IFC4
	{
		private IfcSpatialZoneTypeEnum mPredefinedType = IfcSpatialZoneTypeEnum.NOTDEFINED;
		public IfcSpatialZoneTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSpatialZoneTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSpatialZoneType() : base() { }
		internal IfcSpatialZoneType(DatabaseIfc db, IfcSpatialZoneType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcSpatialZoneType(DatabaseIfc db, string name, IfcSpatialZoneTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
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
		private IfcStackTerminalTypeEnum mPredefinedType = IfcStackTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcStackTerminalTypeEnum;
		public IfcStackTerminalTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStackTerminalTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcStackTerminal() : base() { }
		internal IfcStackTerminal(DatabaseIfc db, IfcStackTerminal t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcStackTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcStackTerminalType : IfcFlowTerminalType
	{
		private IfcStackTerminalTypeEnum mPredefinedType = IfcStackTerminalTypeEnum.NOTDEFINED;
		public IfcStackTerminalTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStackTerminalTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcStackTerminalType() : base() { }
		internal IfcStackTerminalType(DatabaseIfc db, IfcStackTerminalType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcStackTerminalType(DatabaseIfc db, string name, IfcStackTerminalTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcStair : IfcBuiltElement
	{
		private IfcStairTypeEnum mPredefinedType = IfcStairTypeEnum.NOTDEFINED;// OPTIONAL : IfcStairTypeEnum
		public IfcStairTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStairTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcStair() : base() { }
		internal IfcStair(DatabaseIfc db, IfcStair s, DuplicateOptions options) : base(db, s, options) { PredefinedType = s.PredefinedType; }
		public IfcStair(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcStairFlight : IfcBuiltElement
	{
		internal int mNumberOfRiser = int.MinValue;//	:	OPTIONAL INTEGER;
		internal int mNumberOfTreads = int.MinValue;//	:	OPTIONAL INTEGER;
		internal double mRiserHeight = double.NaN;//	:	OPTIONAL IfcPositiveLengthMeasure;
		internal double mTreadLength = double.NaN;//	:	OPTIONAL IfcPositiveLengthMeasure;
		private IfcStairFlightTypeEnum mPredefinedType = IfcStairFlightTypeEnum.NOTDEFINED;//: OPTIONAL IfcStairFlightTypeEnum; IFC4

		public IfcStairFlightTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStairFlightTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcStairFlight() : base() { }
		internal IfcStairFlight(DatabaseIfc db, IfcStairFlight f, DuplicateOptions options) : base(db, f, options) { mNumberOfRiser = f.mNumberOfRiser; mNumberOfTreads = f.mNumberOfTreads; mRiserHeight = f.mRiserHeight; mTreadLength = f.mTreadLength; PredefinedType = f.PredefinedType; }
		public IfcStairFlight(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcStairFlightType : IfcBuiltElementType
	{
		private IfcStairFlightTypeEnum mPredefinedType = IfcStairFlightTypeEnum.NOTDEFINED;
		public IfcStairFlightTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStairFlightTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcStairFlightType() : base() { }
		internal IfcStairFlightType(DatabaseIfc db, IfcStairFlightType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcStairFlightType(DatabaseIfc db, string name, IfcStairFlightTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcStairType : IfcBuiltElementType
	{
		private IfcStairTypeEnum mPredefinedType = IfcStairTypeEnum.NOTDEFINED;
		public IfcStairTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStairTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcStairType() : base() { }
		internal IfcStairType(DatabaseIfc db, IfcStairType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcStairType(DatabaseIfc db, string name, IfcStairTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcStructuralAction : IfcStructuralActivity // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralCurveAction, IfcStructuralPointAction, IfcStructuralSurfaceAction))
	{
		private IfcLogicalEnum mDestabilizingLoad = IfcLogicalEnum.UNKNOWN;//: OPTIONAL BOOLEAN; IFC4 made optional
		[Obsolete("DELETED IFC4", false)]
		internal IfcStructuralReaction mCausedBy;// : OPTIONAL IfcStructuralReaction; DELETED IFC4 

		public bool DestabilizingLoad { get { return mDestabilizingLoad == IfcLogicalEnum.TRUE; } set { mDestabilizingLoad = value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE; } }
		[Obsolete("DELETED IFC4", false)]
		public IfcStructuralReaction CausedBy { get { return mCausedBy; } set { mCausedBy = value; } }

		protected IfcStructuralAction() : base() { }
		protected IfcStructuralAction(DatabaseIfc db, IfcStructuralAction a, DuplicateOptions options) : base(db,a, options)
		{
			mDestabilizingLoad = a.mDestabilizingLoad;
			if(a.mCausedBy != null)
				CausedBy = db.Factory.Duplicate(a.CausedBy);
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
		private IfcStructuralLoad mAppliedLoad;// : IfcStructuralLoad;
		private IfcGlobalOrLocalEnum mGlobalOrLocal = IfcGlobalOrLocalEnum.GLOBAL_COORDS;// : IfcGlobalOrLocalEnum; 
		//INVERSE 
		private IfcRelConnectsStructuralActivity mAssignedToStructuralItem = null; // : SET [0:1] OF IfcRelConnectsStructuralActivity FOR RelatedStructuralActivity; 

		public IfcStructuralLoad AppliedLoad { get { return mAppliedLoad; } set { mAppliedLoad = value; } }
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
			mAppliedLoad = load;
			mGlobalOrLocal = global ? IfcGlobalOrLocalEnum.GLOBAL_COORDS : IfcGlobalOrLocalEnum.LOCAL_COORDS;
			loadcase.RelatedObjects.Add(this);
		}
	}
	public interface IfcStructuralActivityAssignmentSelect : IBaseClassIfc { void AssignStructuralActivity(IfcRelConnectsStructuralActivity connects); } //SELECT(IfcStructuralItem,IfcElement);
	[Serializable]
	public partial class IfcStructuralAnalysisModel : IfcSystem
	{
		private IfcAnalysisModelTypeEnum mPredefinedType = IfcAnalysisModelTypeEnum.NOTDEFINED;// : IfcAnalysisModelTypeEnum;
		internal IfcAxis2Placement3D mOrientationOf2DPlane;// : OPTIONAL IfcAxis2Placement3D;
		internal SET<IfcStructuralLoadGroup> mLoadedBy = new SET<IfcStructuralLoadGroup>();//  : OPTIONAL SET [1:?] OF IfcStructuralLoadGroup;
		internal SET<IfcStructuralResultGroup> mHasResults = new SET<IfcStructuralResultGroup>();//: OPTIONAL SET [1:?] OF IfcStructuralResultGroup  
		internal IfcObjectPlacement mSharedPlacement;//	:	OPTIONAL IfcObjectPlacement;

		public IfcAnalysisModelTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAnalysisModelTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public IfcAxis2Placement3D OrientationOf2DPlane { get { return mOrientationOf2DPlane; } set { mOrientationOf2DPlane = value;  } }
		public SET<IfcStructuralLoadGroup> LoadedBy { get { return mLoadedBy; } }
		public SET<IfcStructuralResultGroup> HasResults { get { return mHasResults; } } 
		public IfcObjectPlacement SharedPlacement { get { return mSharedPlacement; } set { mSharedPlacement = value; } }

		internal IfcStructuralAnalysisModel() : base() { }
		internal IfcStructuralAnalysisModel(DatabaseIfc db, IfcStructuralAnalysisModel m, DuplicateOptions options) : base(db, m, options)
		{
			PredefinedType = m.PredefinedType;
			if(m.mOrientationOf2DPlane != null)
				OrientationOf2DPlane = db.Factory.Duplicate(m.OrientationOf2DPlane);
			m.LoadedBy.ToList().ForEach(x => addLoadGroup( db.Factory.Duplicate(x)));
			m.HasResults.ToList().ForEach(x => addResultGroup( db.Factory.Duplicate(x)));
			if(m.mSharedPlacement != null)
				SharedPlacement = db.Factory.Duplicate(m.SharedPlacement);
		}
		public IfcStructuralAnalysisModel(IfcSpatialElement facility, string name, IfcAnalysisModelTypeEnum type) : base(facility, name)
		{
			PredefinedType = type;
			if (mDatabase.Release < ReleaseVersion.IFC4)
				SharedPlacement = facility.ObjectPlacement;
			else
				SharedPlacement = new IfcLocalPlacement(facility.ObjectPlacement, mDatabase.Factory.XYPlanePlacement); 
		}

		internal void addLoadGroup(IfcStructuralLoadGroup lg) { mLoadedBy.Add(lg); lg.mLoadGroupFor.Add(this); }
		internal void addResultGroup(IfcStructuralResultGroup lg) { mHasResults.Add(lg); lg.mResultGroupFor = this; }

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
		internal IfcBoundaryCondition mAppliedCondition = null; //: OPTIONAL IfcBoundaryCondition
		//INVERSE
		internal SET<IfcRelConnectsStructuralMember> mConnectsStructuralMembers = new SET<IfcRelConnectsStructuralMember>();//	 :	SET [1:?] OF IfcRelConnectsStructuralMember FOR RelatedStructuralConnection;

		public IfcBoundaryCondition AppliedCondition { get { return mAppliedCondition; } set { mAppliedCondition = value; } }
		public SET<IfcRelConnectsStructuralMember> ConnectsStructuralMembers { get { return mConnectsStructuralMembers; } }

		protected IfcStructuralConnection() : base() { }
		protected IfcStructuralConnection(DatabaseIfc db, IfcStructuralConnection c, DuplicateOptions options) : base(db, c, options) 
		{ 
			AppliedCondition = db.Factory.Duplicate(c.AppliedCondition);
		}
		protected IfcStructuralConnection(IfcStructuralAnalysisModel sm) : base(sm) {  }
	}
	[Serializable]
	public abstract partial class IfcStructuralConnectionCondition : BaseClassIfc, NamedObjectIfc //ABSTRACT SUPERTYPE OF (ONEOF (IfcFailureConnectionCondition ,IfcSlippageConnectionCondition));
	{
		internal string mName = "";// : OPTIONAL IfcLabel;
		public string Name { get { return mName; } set { mName = value; } }

		protected IfcStructuralConnectionCondition() : base() { }
		protected IfcStructuralConnectionCondition(DatabaseIfc db) : base(db) { }
		protected IfcStructuralConnectionCondition(DatabaseIfc db, IfcStructuralConnectionCondition c) : base(db,c) { mName = c.mName; }
	}
	[Serializable]
	public partial class IfcStructuralCurveAction : IfcStructuralAction // IFC4 SUPERTYPE OF(IfcStructuralLinearAction)
	{
		internal IfcProjectedOrTrueLengthEnum mProjectedOrTrue = IfcProjectedOrTrueLengthEnum.TRUE_LENGTH;// : IfcProjectedOrTrueLengthEnum
		private IfcStructuralCurveActivityTypeEnum mPredefinedType = IfcStructuralCurveActivityTypeEnum.NOTDEFINED;//IfcStructuralCurveActivityTypeEnum

		public IfcProjectedOrTrueLengthEnum ProjectedOrTrue { get { return mProjectedOrTrue; } set { mProjectedOrTrue = value; } }
		public IfcStructuralCurveActivityTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStructuralCurveActivityTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcStructuralCurveAction() : base() { }
		internal IfcStructuralCurveAction(DatabaseIfc db, IfcStructuralCurveAction a, DuplicateOptions options) : base(db,a, options) { mProjectedOrTrue = a.mProjectedOrTrue; PredefinedType = a.PredefinedType; }
		public IfcStructuralCurveAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadSingleForce force, double length, bool global)
			: base(lc, member, new IfcStructuralLoadConfiguration(force, length), null, global) { PredefinedType = IfcStructuralCurveActivityTypeEnum.DISCRETE; }
		public IfcStructuralCurveAction(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoad load, bool global, bool projected, IfcStructuralCurveActivityTypeEnum type)
			: base(lc, member, load,null, global) { ProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcStructuralCurveConnection : IfcStructuralConnection
	{
		internal IfcDirection mAxisDirection; //: IfcDirection
		public IfcDirection AxisDirection { get { return mAxisDirection; } set { mAxisDirection = value; } }
		internal IfcStructuralCurveConnection() : base() { }
		internal IfcStructuralCurveConnection(DatabaseIfc db, IfcStructuralCurveConnection c, DuplicateOptions options) : base(db, c, options) { }
	}
	[Serializable]
	public partial class IfcStructuralCurveMember : IfcStructuralMember
	{
		internal IfcStructuralCurveMemberTypeEnum mPredefinedType= IfcStructuralCurveMemberTypeEnum.NOTDEFINED;// : IfcStructuralCurveMemberTypeEnum; 
		internal IfcDirection mAxis; //: IfcDirection

		public IfcStructuralCurveMemberTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStructuralCurveMemberTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public IfcDirection Axis { get { return mAxis; } set { mAxis = value; } }

		internal IfcEdgeCurve EdgeCurve { get; set; } //Used for load applications in ifc2x3

		public IfcStructuralCurveMember() : base() { }
		internal IfcStructuralCurveMember(DatabaseIfc db, IfcStructuralCurveMember m, DuplicateOptions options) : base(db,m, options) { PredefinedType = m.PredefinedType; Axis = db.Factory.Duplicate(m.Axis) as IfcDirection; }
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
		private IfcStructuralCurveActivityTypeEnum mPredefinedType = IfcStructuralCurveActivityTypeEnum.NOTDEFINED;//: 	IfcStructuralCurveActivityTypeEnum; 
		public IfcStructuralCurveActivityTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStructuralCurveActivityTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcStructuralCurveReaction() : base() { }
		internal IfcStructuralCurveReaction(DatabaseIfc db, IfcStructuralCurveReaction r, DuplicateOptions options) : base(db, r, options) { PredefinedType = r.PredefinedType; }
	}
	[Serializable]
	public abstract partial class IfcStructuralItem : IfcProduct, IfcStructuralActivityAssignmentSelect // ABSTRACT SUPERTYPE OF (ONEOF (IfcStructuralConnection ,IfcStructuralMember))
	{
		//INVERSE
		internal SET<IfcRelConnectsStructuralActivity> mAssignedStructuralActivity = new SET<IfcRelConnectsStructuralActivity>();//: 	SET OF IfcRelConnectsStructuralActivity FOR RelatingElement;
		public SET<IfcRelConnectsStructuralActivity> AssignedStructuralActivity { get { return mAssignedStructuralActivity; } }
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
	[Obsolete("DELETED IFC4", false)]
	[Serializable]
	public partial class IfcStructuralLinearActionVarying : IfcStructuralLinearAction // DELETED IFC4
	{
		internal IfcShapeAspect mVaryingAppliedLoadLocation;// : IfcShapeAspect;
		internal LIST<IfcStructuralLoad> mSubsequentAppliedLoads = new LIST<IfcStructuralLoad>();//: LIST [1:?] OF IfcStructuralLoad; 

		public IfcShapeAspect VaryingAppliedLoadLocation { get { return mVaryingAppliedLoadLocation; } set { mVaryingAppliedLoadLocation = value; } }
		public LIST<IfcStructuralLoad> SubsequentAppliedLoads { get { return mSubsequentAppliedLoads; } }

		internal IfcStructuralLinearActionVarying() : base() { }
		internal IfcStructuralLinearActionVarying(DatabaseIfc db, IfcStructuralLinearActionVarying a, DuplicateOptions options) : base(db, a, options)
		{
			VaryingAppliedLoadLocation = db.Factory.Duplicate( a.VaryingAppliedLoadLocation) as IfcShapeAspect;
			SubsequentAppliedLoads.AddRange(a.SubsequentAppliedLoads.Select(x=>db.Factory.Duplicate(x)));
		}
		public IfcStructuralLinearActionVarying(IfcStructuralLoadCase lc, IfcStructuralCurveMember member, IfcStructuralLoadLinearForce start, IfcStructuralLoadLinearForce end, bool global, bool projected) 
			: base(lc, member, start, global, projected, IfcStructuralCurveActivityTypeEnum.LINEAR)
		{
			IfcCurve curve = member.EdgeCurve.EdgeGeometry;
			List<IfcShapeModel> aspects = new List<IfcShapeModel>();
			aspects.Add( new IfcShapeRepresentation(new IfcPointOnCurve(curve, 0)));
			aspects.Add(new IfcShapeRepresentation(new IfcPointOnCurve(curve, 1)));
			VaryingAppliedLoadLocation = new IfcShapeAspect(aspects);
		}
	}
	[Serializable]
	public abstract partial class IfcStructuralLoad : BaseClassIfc, NamedObjectIfc //	ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralLoadConfiguration, IfcStructuralLoadOrResult));
	{
		internal string mName = "";// : OPTIONAL IfcLabel
		public string Name { get { return mName; } set { mName = value; } }

		protected IfcStructuralLoad() : base() { }
		protected IfcStructuralLoad(DatabaseIfc db) : base(db) { }
		protected IfcStructuralLoad(DatabaseIfc db, IfcStructuralLoad l) : base(db) { mName = l.mName; }
	}
	[Serializable]
	public partial class IfcStructuralLoadConfiguration : IfcStructuralLoad //IFC4
	{
		internal LIST<IfcStructuralLoadOrResult> mValues = new LIST<IfcStructuralLoadOrResult>();//	 :	LIST [1:?] OF IfcStructuralLoadOrResult;
		internal List<List<double>> mLocations = new List<List<double>>();//	 :	OPTIONAL LIST [1:?] OF UNIQUE LIST [1:2] OF IfcLengthMeasure; 

		public LIST<IfcStructuralLoadOrResult> Values { get { return mValues; } }
		public List<List<double>> Locations { get { return mLocations; } }

		internal IfcStructuralLoadConfiguration() : base() { }
		internal IfcStructuralLoadConfiguration(DatabaseIfc db, IfcStructuralLoadConfiguration p) : base(db,p)
		{
			Values.AddRange(p.Values.Select(x=>db.Factory.Duplicate<IfcStructuralLoadOrResult>(x)));
			mLocations.AddRange(p.mLocations);
		}
		public IfcStructuralLoadConfiguration(IfcStructuralLoadOrResult val, double length)
			: base(val.Database) { mValues.Add(val); mLocations.Add( new List<double>() { length } );  }
		public IfcStructuralLoadConfiguration(List<IfcStructuralLoadOrResult> vals, IEnumerable<List<double>> locations)
			: base(vals[0].mDatabase) { mValues.AddRange(vals); if (locations != null) mLocations.AddRange(locations); }
		public IfcStructuralLoadConfiguration(IfcStructuralLoadOrResult val1, double loc1, IfcStructuralLoadOrResult val2, double loc2)
			: this(new List<IfcStructuralLoadOrResult>() { val1, val2}, new List<List<double>>() { new List<double>() { loc1 }, new List<double>() { loc2 } }) {  }
		
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
		private IfcLoadGroupTypeEnum mPredefinedType = IfcLoadGroupTypeEnum.NOTDEFINED;// : IfcLoadGroupTypeEnum;
		internal IfcActionTypeEnum mActionType = IfcActionTypeEnum.NOTDEFINED;// : IfcActionTypeEnum;
		internal IfcActionSourceTypeEnum mActionSource = IfcActionSourceTypeEnum.NOTDEFINED;//: IfcActionSourceTypeEnum;
		internal double mCoefficient = double.NaN;//: OPTIONAL IfcRatioMeasure;
		internal string mPurpose = "";// : OPTIONAL IfcLabel; 
		//INVERSE 
		internal IfcStructuralResultGroup mSourceOfResultGroup = null;// :	SET [0:1] OF IfcStructuralResultGroup FOR ResultForLoadGroup;
		internal List<IfcStructuralAnalysisModel> mLoadGroupFor = new List<IfcStructuralAnalysisModel>();//	 :	SET [0:?] OF IfcStructuralAnalysisModel 

		public IfcLoadGroupTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcLoadGroupTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public IfcActionTypeEnum ActionType { get { return mActionType; } set { mActionType = value; } }
		public IfcActionSourceTypeEnum ActionSource { get { return mActionSource; } set { mActionSource = value; } }
		public double Coefficient { get { return mCoefficient; } set { mCoefficient = value; } }
		public string Purpose { get { return mPurpose; } set { mPurpose = value; } }

		internal IfcStructuralLoadGroup() : base() { }
		internal IfcStructuralLoadGroup(DatabaseIfc db, IfcStructuralLoadGroup g, DuplicateOptions options) : base(db, g, options) { PredefinedType = g.PredefinedType; mActionType = g.mActionType; mActionSource = g.mActionSource; mCoefficient = g.mCoefficient; mPurpose = g.mPurpose; }
		public IfcStructuralLoadGroup(IfcStructuralAnalysisModel sm, string name, IfcLoadGroupTypeEnum type)
			: base(sm.mDatabase, name) { mLoadGroupFor.Add(sm); sm.addLoadGroup(this); PredefinedType = type; }
		public IfcStructuralLoadGroup(IfcStructuralAnalysisModel sm, string name, List<double> factors, List<IfcStructuralLoadGroup> cases, bool ULS)
			: base(sm.mDatabase, name)
		{
			PredefinedType = IfcLoadGroupTypeEnum.LOAD_COMBINATION;
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
				structuralAnalysisModel.mLoadedBy.Remove(this);
			return base.DisposeWorker(children);
		}
	}
	[Serializable]
	public partial class IfcStructuralLoadLinearForce : IfcStructuralLoadStatic
	{
		private double mLinearForceX = double.NaN, mLinearForceY = double.NaN, mLinearForceZ = double.NaN; // : OPTIONAL IfcLinearForceMeasure
		private double mLinearMomentX = double.NaN, mLinearMomentY = double.NaN, mLinearMomentZ = double.NaN;// : OPTIONAL IfcLinearMomentMeasure; 

		public double LinearForceX { get { return double.IsNaN(mLinearForceX) ? 0 : mLinearForceX; } set { mLinearForceX = value; } }
		public double LinearForceY { get { return double.IsNaN(mLinearForceY) ? 0 : mLinearForceY; } set { mLinearForceY = value; } }
		public double LinearForceZ { get { return double.IsNaN(mLinearForceZ) ? 0 : mLinearForceZ; } set { mLinearForceZ = value; } }
		public double LinearMomentX { get { return double.IsNaN(mLinearMomentX) ? 0 : mLinearMomentX; } set { mLinearMomentX = value; } }
		public double LinearMomentY { get { return double.IsNaN(mLinearMomentY) ? 0 : mLinearMomentY; } set { mLinearMomentY = value; } }
		public double LinearMomentZ { get { return double.IsNaN(mLinearMomentZ) ? 0 : mLinearMomentZ; } set { mLinearMomentZ = value; } }

		internal IfcStructuralLoadLinearForce() : base() { }
		internal IfcStructuralLoadLinearForce(DatabaseIfc db, IfcStructuralLoadLinearForce f) : base(db,f) { mLinearForceX = f.mLinearForceX; mLinearForceY = f.mLinearForceY; mLinearForceZ = f.mLinearForceZ; mLinearMomentX = f.mLinearMomentX; mLinearMomentY = f.mLinearMomentY; mLinearMomentZ = f.mLinearMomentZ; }
		public IfcStructuralLoadLinearForce(DatabaseIfc db) : base(db) { }
		public IfcStructuralLoadLinearForce(DatabaseIfc db, double forceX, double forceY, double forceZ, double momentX, double momentY, double momentZ) : base(db) { mLinearForceX = forceX; mLinearForceY = forceY; mLinearForceZ = forceZ; mLinearMomentX = momentX; mLinearMomentY = momentY; mLinearMomentZ = momentZ; }
	}
	[Serializable]
	public partial class IfcStructuralLoadPlanarForce : IfcStructuralLoadStatic
	{
		private double mPlanarForceX = double.NaN, mPlanarForceY = double.NaN, mPlanarForceZ = double.NaN;// : OPTIONAL IfcPlanarForceMeasure; 
		public double PlanarForceX { get { return double.IsNaN(mPlanarForceX) ? 0 : mPlanarForceX; } set { mPlanarForceX = value; } }
		public double PlanarForceY { get { return double.IsNaN(mPlanarForceY) ? 0 : mPlanarForceY; } set { mPlanarForceY = value; } }
		public double PlanarForceZ { get { return double.IsNaN(mPlanarForceZ) ? 0 : mPlanarForceZ; } set { mPlanarForceZ = value; } }
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
		private double mForceX = double.NaN, mForceY = double.NaN, mForceZ = double.NaN;// : OPTIONAL IfcForceMeasure;
		private double mMomentX = double.NaN, mMomentY = double.NaN, mMomentZ = double.NaN;// : OPTIONAL IfcTorqueMeasure; 

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
		internal double mDeltaT_Constant = double.NaN, mDeltaT_Y = double.NaN, mDeltaT_Z = double.NaN;// : OPTIONAL IfcThermodynamicTemperatureMeasure; 
		public double DeltaT_Constant { get { return mDeltaT_Constant; } set { mDeltaT_Constant = value; } }
		public double DeltaT_Y { get { return mDeltaT_Y; } set { mDeltaT_Y = value; } }
		public double DeltaT_Z { get { return mDeltaT_Z; } set { mDeltaT_Z = value; } }

		internal IfcStructuralLoadTemperature() : base() { }
		internal IfcStructuralLoadTemperature(DatabaseIfc db, IfcStructuralLoadTemperature t) : base(db,t) { mDeltaT_Constant = t.mDeltaT_Constant; mDeltaT_Y = t.mDeltaT_Y; mDeltaT_Z = t.mDeltaT_Z; }
		public IfcStructuralLoadTemperature(DatabaseIfc db) : base(db) { }
		public IfcStructuralLoadTemperature(DatabaseIfc db, double T, double TY, double TZ) : base(db) { mDeltaT_Constant = T; mDeltaT_Y = TY; mDeltaT_Z = TZ; }
	}
	[Serializable]
	public abstract partial class IfcStructuralMember : IfcStructuralItem //ABSTRACT SUPERTYPE OF(ONEOF(IfcStructuralCurveMember, IfcStructuralSurfaceMember))
	{
		//INVERSE
		internal SET<IfcRelConnectsStructuralMember> mConnectedBy = new SET<IfcRelConnectsStructuralMember>();// : SET [0:?] OF IfcRelConnectsStructuralMember FOR RelatingStructuralMember 
		internal IfcRelConnectsStructuralElement mStructuralMemberFor = null; // GG attribute

		public SET<IfcRelConnectsStructuralMember> ConnectedBy { get { return mConnectedBy; } }

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
		private IfcAxis2Placement3D mConditionCoordinateSystem = null;//:	OPTIONAL IfcAxis2Placement3D;
		public IfcAxis2Placement3D ConditionCoordinateSystem { get { return mConditionCoordinateSystem; } set { mConditionCoordinateSystem = value; } }
		public new IfcBoundaryNodeCondition AppliedCondition
		{
			get { return mAppliedCondition as IfcBoundaryNodeCondition; }
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
		private IfcStructuralSurfaceActivityTypeEnum mPredefinedType = IfcStructuralSurfaceActivityTypeEnum.NOTDEFINED;//IfcStructuralCurveActivityTypeEnum
		
		public IfcProjectedOrTrueLengthEnum ProjectedOrTrue { get { return mProjectedOrTrue; } set { mProjectedOrTrue = value; } }
		public IfcStructuralSurfaceActivityTypeEnum PredefinedType {  get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStructuralSurfaceActivityTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcStructuralSurfaceAction() : base() { }
		internal IfcStructuralSurfaceAction(DatabaseIfc db, IfcStructuralSurfaceAction a, DuplicateOptions options) : base(db, a, options) { mProjectedOrTrue = a.mProjectedOrTrue; PredefinedType = a.PredefinedType; }
		public IfcStructuralSurfaceAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, bool global, bool projected, IfcStructuralSurfaceActivityTypeEnum type)
			: base(lc, item, load,null, global) { mProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH; PredefinedType = type; }
		public IfcStructuralSurfaceAction(IfcStructuralLoadCase lc, IfcStructuralLoad load, IfcFaceSurface extent, bool global, bool projected, IfcStructuralSurfaceActivityTypeEnum type)
			: base(lc,null,load,new IfcTopologyRepresentation(lc.mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Reference), extent), global)
		{
			if (mDatabase.mRelease < ReleaseVersion.IFC4)
				throw new Exception(StepClassName + "added in IFC4");
			mProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH;
			PredefinedType = type;
		}
		public IfcStructuralSurfaceAction(IfcStructuralLoadCase lc, IfcStructuralActivityAssignmentSelect item, IfcStructuralLoad load, IfcFaceSurface extent, bool global, bool projected, IfcStructuralSurfaceActivityTypeEnum type)
			: base(lc, item, load, new IfcTopologyRepresentation(lc.mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Reference), extent), global)
		{ 
			if (mDatabase.mRelease < ReleaseVersion.IFC4) throw new Exception(StepClassName + "added in IFC4"); 
			mProjectedOrTrue = projected ? IfcProjectedOrTrueLengthEnum.PROJECTED_LENGTH : IfcProjectedOrTrueLengthEnum.TRUE_LENGTH; 
			PredefinedType = type; 
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
		private IfcStructuralSurfaceMemberTypeEnum mPredefinedType = IfcStructuralSurfaceMemberTypeEnum.NOTDEFINED;// : IfcStructuralSurfaceMemberTypeEnum;
		internal double mThickness = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure; 

		public IfcStructuralSurfaceMemberTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStructuralSurfaceMemberTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public double Thickness { get { return mThickness; } set { mThickness = value; } }

		public IfcStructuralSurfaceMember() : base() { }
		internal IfcStructuralSurfaceMember(DatabaseIfc db, IfcStructuralSurfaceMember m, DuplicateOptions options) : base(db, m, options) { PredefinedType = m.PredefinedType; mThickness = m.mThickness; }
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
		internal IfcShapeAspect mVaryingThicknessLocation;// : IfcShapeAspect; 

		public IfcShapeAspect VaryingThicknessLocation { get { return mVaryingThicknessLocation; } set { mVaryingThicknessLocation = value; } }

		internal IfcStructuralSurfaceMemberVarying() : base() { }
		internal IfcStructuralSurfaceMemberVarying(DatabaseIfc db, IfcStructuralSurfaceMemberVarying m, DuplicateOptions options) : base(db, m, options) { mSubsequentThickness.AddRange(m.mSubsequentThickness); mVaryingThicknessLocation = m.mVaryingThicknessLocation; }
	}
	[Serializable]
	public partial class IfcStructuralSurfaceReaction : IfcStructuralReaction
	{
		private IfcStructuralSurfaceActivityTypeEnum mPredefinedType = IfcStructuralSurfaceActivityTypeEnum.NOTDEFINED; //: IfcStructuralSurfaceActivityTypeEnum;
		public IfcStructuralSurfaceActivityTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcStructuralSurfaceActivityTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

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
		SET<IfcStyledItem> StyledItems { get; }
	}
	[Serializable]
	public partial class IfcStyledItem : IfcRepresentationItem, NamedObjectIfc
	{
		private IfcRepresentationItem mItem = null;// : OPTIONAL IfcRepresentationItem;
		private SET<IfcStyleAssignmentSelect> mStyles = new SET<IfcStyleAssignmentSelect>();// : SET [1:?] OF IfcStyleAssignmentSelect; ifc2x3 IfcPresentationStyleAssignment;
		private string mName = "";// : OPTIONAL IfcLabel; 

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
		public string Name { get { return mName; } set { mName = value; } }

		internal IfcStyledItem() : base() { }
		internal IfcStyledItem(DatabaseIfc db, IfcStyledItem i, DuplicateOptions options) : base(db, i, options)
		{
			foreach(IfcStyleAssignmentSelect style in i.Styles)
				addStyle(db.Factory.Duplicate(style, options) as IfcStyleAssignmentSelect);
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
					IfcPresentationStyleSelect presentationStyle = style as IfcPresentationStyleSelect;
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
		private IfcSubContractResourceTypeEnum mPredefinedType = IfcSubContractResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcSubContractResourceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSubContractResourceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSubContractResource() : base() { }
		internal IfcSubContractResource(DatabaseIfc db, IfcSubContractResource r, DuplicateOptions options) : base(db, r, options) { PredefinedType = r.PredefinedType; }
		public IfcSubContractResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcSubContractResourceType : IfcConstructionResourceType //IFC4
	{
		private IfcSubContractResourceTypeEnum mPredefinedType = IfcSubContractResourceTypeEnum.NOTDEFINED;
		public IfcSubContractResourceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSubContractResourceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSubContractResourceType() : base() { }
		internal IfcSubContractResourceType(DatabaseIfc db, IfcSubContractResourceType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcSubContractResourceType(DatabaseIfc db, string name, IfcSubContractResourceTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcSubedge : IfcEdge
	{
		internal IfcEdge mParentEdge;// IfcEdge;
		public IfcEdge ParentEdge { get { return mParentEdge; } set { mParentEdge = value; } }

		internal IfcSubedge() : base() { }
		internal IfcSubedge(DatabaseIfc db, IfcSubedge e, DuplicateOptions options) : base(db, e, options) { ParentEdge = db.Factory.Duplicate(e.ParentEdge) as IfcEdge; }
		public IfcSubedge(IfcVertex v1, IfcVertex v2, IfcEdge e) : base(v1, v2) { mParentEdge = e; }
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
		internal IfcSurface mReferenceSurface;// : IfcSurface; 
		public IfcSurface ReferenceSurface { get { return mReferenceSurface; } set { mReferenceSurface = value; } }

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
		public IfcSurfaceFeatureTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSurfaceFeatureTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		//INVERSE
		internal IfcRelAdheresToElement mAdheresToElement = null;
		public IfcRelAdheresToElement AdheresToElement { get { return mAdheresToElement; } set { mAdheresToElement = value; } }

		public IfcSurfaceFeature() : base() { }
		public IfcSurfaceFeature(DatabaseIfc db) : base(db) { }
		public IfcSurfaceFeature(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcSurfaceOfLinearExtrusion : IfcSweptSurface
	{
		internal IfcDirection mExtrudedDirection;//  : IfcDirection;
		internal double mDepth;// : IfcLengthMeasure;

		public IfcDirection ExtrudedDirection { get { return mExtrudedDirection; } set { mExtrudedDirection = value; } }
		public double Depth { get { return mDepth; } set { mDepth = value; } }

		internal IfcSurfaceOfLinearExtrusion() : base() { }
		internal IfcSurfaceOfLinearExtrusion(DatabaseIfc db, IfcSurfaceOfLinearExtrusion s, DuplicateOptions options) : base(db, s, options) { ExtrudedDirection = db.Factory.Duplicate(s.ExtrudedDirection); mDepth = s.mDepth; }
		public IfcSurfaceOfLinearExtrusion(IfcProfileDef sweptCurve, IfcAxis2Placement3D position, double depth) : base(sweptCurve, position) { ExtrudedDirection = mDatabase.Factory.ZAxis; mDepth = depth; }
	}
	[Serializable]
	public partial class IfcSurfaceOfRevolution : IfcSweptSurface
	{
		internal IfcAxis1Placement mAxisPosition;//  : IfcAxis1Placement;
		public IfcAxis1Placement AxisPosition { get { return mAxisPosition; } set { mAxisPosition = value; } }

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
		internal IfcSurfaceStyle(DatabaseIfc db, IfcSurfaceStyle s, DuplicateOptions options) : base(db, s, options) 
		{ 
			mSide = s.mSide; 
			mStyles.AddRange(s.mStyles.Select(x=> db.Factory.Duplicate(x, options)));
		}
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
		internal IfcColourRgb mDiffuseTransmissionColour, mDiffuseReflectionColour, mTransmissionColour, mReflectanceColour;//	 :	IfcColourRgb;
		public IfcColourRgb DiffuseTransmissionColour {  get { return mDiffuseTransmissionColour; } set { mDiffuseTransmissionColour = value; } }
		public IfcColourRgb DiffuseReflectionColour {  get { return mDiffuseReflectionColour; } set { mDiffuseReflectionColour = value; } }
		public IfcColourRgb TransmissionColour {  get { return mTransmissionColour; } set { mTransmissionColour = value; } }
		public IfcColourRgb ReflectanceColour {  get { return mReflectanceColour; } set { mReflectanceColour = value; } }
		internal IfcSurfaceStyleLighting() : base() { }
		internal IfcSurfaceStyleLighting(DatabaseIfc db, IfcSurfaceStyleLighting i, DuplicateOptions options) : base(db, i, options)
		{
			mDiffuseTransmissionColour = db.Factory.Duplicate(i.mDiffuseTransmissionColour);
			mDiffuseReflectionColour = db.Factory.Duplicate(i.mDiffuseReflectionColour);
			mTransmissionColour = db.Factory.Duplicate(i.mTransmissionColour);
			mReflectanceColour = db.Factory.Duplicate(i.mReflectanceColour);
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
		public double RefractionIndex { get { return mRefractionIndex; } set { mRefractionIndex = value; } }
		public double DispersionFactor { get { return mDispersionFactor; } set { mDispersionFactor = value; } }

		internal IfcSurfaceStyleRefraction() : base() { }
		public IfcSurfaceStyleRefraction(DatabaseIfc db) : base(db) { }
		internal IfcSurfaceStyleRefraction(DatabaseIfc db, IfcSurfaceStyleRefraction s, DuplicateOptions options) : base(db, s, options)
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
		internal IfcSurfaceStyleRendering(DatabaseIfc db, IfcSurfaceStyleRendering r, DuplicateOptions options) : base(db, r, options)
		{
			IfcColourRgb colourRgb = r.mDiffuseColour as IfcColourRgb;
			if (colourRgb != null)
				mDiffuseColour = db.Factory.Duplicate(colourRgb, options);
			else
				mDiffuseColour = r.mDiffuseColour;

			colourRgb = r.mTransmissionColour as IfcColourRgb;
			if (colourRgb != null)
				mTransmissionColour = db.Factory.Duplicate(colourRgb, options);
			else
				mTransmissionColour = r.mTransmissionColour;

			colourRgb = r.mDiffuseTransmissionColour as IfcColourRgb;
			if (colourRgb != null)
				mDiffuseTransmissionColour = db.Factory.Duplicate(colourRgb, options);
			else
				mDiffuseTransmissionColour = r.mDiffuseTransmissionColour;

			colourRgb = r.mReflectionColour as IfcColourRgb;
			if (colourRgb != null)
				mReflectionColour = db.Factory.Duplicate(colourRgb, options);
			else
				mReflectionColour = r.mReflectionColour;

			colourRgb = r.mSpecularColour as IfcColourRgb;
			if (colourRgb != null)
				mSpecularColour = db.Factory.Duplicate(colourRgb);
			else
				mSpecularColour = r.mSpecularColour;

			mSpecularHighlight = r.mSpecularHighlight;
			mReflectanceMethod = r.mReflectanceMethod;
		}
		public IfcSurfaceStyleRendering(IfcColourRgb surfaceColour) : base(surfaceColour) { }
	}
	[Serializable]
	public partial class IfcSurfaceStyleShading : IfcPresentationItem, IfcSurfaceStyleElementSelect //SUPERTYPE OF(IfcSurfaceStyleRendering)
	{
		private IfcColourRgb mSurfaceColour;// : IfcColourRgb;
		private double mTransparency = double.NaN; // : IfcNormalisedRatioMeasure
		
		public IfcColourRgb SurfaceColour { get { return mSurfaceColour; } set { mSurfaceColour = value; } }
		public double Transparency { get { return mTransparency; } set { mTransparency = value; } }

		internal IfcSurfaceStyleShading() : base() { }
		internal IfcSurfaceStyleShading(DatabaseIfc db, IfcSurfaceStyleShading s, DuplicateOptions options) : base(db,s, options) 
		{
			SurfaceColour = db.Factory.Duplicate(s.SurfaceColour, options) as IfcColourRgb;
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
		internal string mMode = ""; //:	OPTIONAL IfcIdentifier; ifc2x3 IfcSurfaceTextureEnum mTextureType;// 
		internal IfcCartesianTransformationOperator2D mTextureTransform;// : OPTIONAL IfcCartesianTransformationOperator2D;
		internal List<string> mParameter = new List<string>();// IFC4 OPTIONAL LIST [1:?] OF IfcIdentifier;

		public bool RepeatS { get { return mRepeatS; } set { mRepeatS = value; } }
		public bool RepeatT { get { return mRepeatT; } set { mRepeatT = value; } }
		public string Mode { get { return mMode; } set { mMode = value; } }
		public IfcCartesianTransformationOperator2D TextureTransform { get { return mTextureTransform; } set { mTextureTransform = value; } }
		public List<string> Parameter { get { return mParameter; } }

		protected IfcSurfaceTexture() : base() { }
		protected IfcSurfaceTexture(DatabaseIfc db, IfcSurfaceTexture t, DuplicateOptions options)
			: base(db, t, options) 
		{ 
			mRepeatS = t.mRepeatS; 
			mRepeatT = t.mRepeatT; 
			mMode = t.mMode;
			if (t.mTextureTransform != null)
				TextureTransform = db.Factory.Duplicate(t.TextureTransform, options) as IfcCartesianTransformationOperator2D;
			mParameter.AddRange(t.mParameter);
		}
		protected IfcSurfaceTexture(DatabaseIfc db, bool repeatS, bool repeatT) : base(db) { mRepeatS = repeatS; mRepeatT = repeatT; }
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
		internal IfcCurve mDirectrix;// : IfcCurve;
		internal double mRadius;// : IfcPositiveLengthMeasure;
		internal double mInnerRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mStartParam = double.NaN;// : OPTIONAL IfcParameterValue; IFC4
		internal double mEndParam = double.NaN;// : OPTIONAL IfcParameterValue;  IFC4

		public IfcCurve Directrix { get { return mDirectrix; } set { mDirectrix = value; } }
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
		internal IfcProfileDef mSweptCurve;// : IfcProfileDef;
		internal IfcAxis2Placement3D mPosition;// : OPTIONAL IfcAxis2Placement3D; IFC4 Optional

		public IfcProfileDef SweptCurve { get { return mSweptCurve; } set { mSweptCurve = value; } }
		public IfcAxis2Placement3D Position { get { return mPosition; } set { mPosition = value; } }

		protected IfcSweptSurface() : base() { }
		protected IfcSweptSurface(DatabaseIfc db, IfcSweptSurface s, DuplicateOptions options) : base(db, s, options)
		{
			SweptCurve = db.Factory.Duplicate(s.SweptCurve, options);
			if(s.mPosition != null)
				Position = db.Factory.Duplicate(s.Position);
		}
		protected IfcSweptSurface(IfcProfileDef sweptCurve) : base(sweptCurve.mDatabase) { SweptCurve = sweptCurve; if (sweptCurve.mDatabase.Release < ReleaseVersion.IFC4) Position = sweptCurve.mDatabase.Factory.XYPlanePlacement; }
		protected IfcSweptSurface(IfcProfileDef sweptCurve, IfcAxis2Placement3D position) : this(sweptCurve) { Position = (position == null && mDatabase.Release < ReleaseVersion.IFC4 ? new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase,0,0,0)) : position); }
	}
	[Serializable]
	public partial class IfcSwitchingDevice : IfcFlowController //IFC4
	{
		private IfcSwitchingDeviceTypeEnum mPredefinedType = IfcSwitchingDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcSwitchingDeviceTypeEnum;
		public IfcSwitchingDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSwitchingDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSwitchingDevice() : base() { }
		internal IfcSwitchingDevice(DatabaseIfc db, IfcSwitchingDevice d, DuplicateOptions options) : base(db, d, options) { PredefinedType = d.PredefinedType; }
		public IfcSwitchingDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcSwitchingDeviceType : IfcFlowControllerType
	{
		private IfcSwitchingDeviceTypeEnum mPredefinedType = IfcSwitchingDeviceTypeEnum.NOTDEFINED;// : IfcFlowMeterTypeEnum;
		public IfcSwitchingDeviceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSwitchingDeviceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSwitchingDeviceType() : base() { }
		internal IfcSwitchingDeviceType(DatabaseIfc db, IfcSwitchingDeviceType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcSwitchingDeviceType(DatabaseIfc db, string name, IfcSwitchingDeviceTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
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
		private IfcSystemFurnitureElementTypeEnum mPredefinedType = IfcSystemFurnitureElementTypeEnum.NOTDEFINED;//: OPTIONAL IfcSystemFurnitureElementTypeEnum
		public IfcSystemFurnitureElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSystemFurnitureElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSystemFurnitureElement() : base() { }
		internal IfcSystemFurnitureElement(DatabaseIfc db, IfcSystemFurnitureElement f, DuplicateOptions options) : base(db, f, options) { PredefinedType = f.PredefinedType; }
		public IfcSystemFurnitureElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcSystemFurnitureElementType : IfcFurnishingElementType
	{
		private IfcSystemFurnitureElementTypeEnum mPredefinedType = IfcSystemFurnitureElementTypeEnum.NOTDEFINED;//: OPTIONAL IfcSystemFurnitureElementTypeEnum
		public IfcSystemFurnitureElementTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcSystemFurnitureElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcSystemFurnitureElementType() : base() { }
		internal IfcSystemFurnitureElementType(DatabaseIfc db, IfcSystemFurnitureElementType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcSystemFurnitureElementType(DatabaseIfc db, string name, IfcSystemFurnitureElementTypeEnum type) : base(db,name)
		{
			PredefinedType = type;
			if (mDatabase.mRelease < ReleaseVersion.IFC4 && string.IsNullOrEmpty(ElementType) && type != IfcSystemFurnitureElementTypeEnum.NOTDEFINED)
				ElementType = type.ToString();
		}
	}
}
