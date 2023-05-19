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
	public partial class IfcWall : IfcBuiltElement
	{
		private IfcWallTypeEnum mPredefinedType = IfcWallTypeEnum.NOTDEFINED;//: OPTIONAL IfcWallTypeEnum; 
		public IfcWallTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcWallTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcWall() : base() { }
		internal IfcWall(DatabaseIfc db, IfcWall w, DuplicateOptions options) : base(db, w, options) { PredefinedType = w.PredefinedType; }
		public IfcWall(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcWallElementedCase : IfcWall
	{
		internal IfcWallElementedCase() : base() { }
		internal IfcWallElementedCase(DatabaseIfc db, IfcWallElementedCase w, DuplicateOptions options) : base(db, w, options) { }
	}
	[Serializable]
	public partial class IfcWallStandardCase : IfcWall
	{
		public override string StepClassName { get { return "IfcWall" + (mDatabase.mRelease < ReleaseVersion.IFC4 ? "StandardCase" : ""); } }
		internal IfcWallStandardCase() : base() { }
		internal IfcWallStandardCase(DatabaseIfc db, IfcWallStandardCase w, DuplicateOptions options) : base(db, w, options) { }
		public IfcWallStandardCase(IfcProduct container, IfcMaterialLayerSetUsage layerSetUsage, IfcAxis2Placement3D placement, double length, double height)
			:base(container,new IfcLocalPlacement(container.ObjectPlacement, placement),null)
		{
			DatabaseIfc db = mDatabase;
			double tol = mDatabase.Tolerance;
			setMaterial(layerSetUsage);

			IfcShapeRepresentation asr = new IfcShapeRepresentation(mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis), new IfcPolyline(new IfcCartesianPoint(db,0,0,0),new IfcCartesianPoint(db,length,0,0)), ShapeRepresentationType.Curve2D);
			List<IfcShapeModel> reps = new List<IfcShapeModel>();
			reps.Add(asr);
			double t = layerSetUsage.ForLayerSet.MaterialLayers.ToList().ConvertAll(x=>x.LayerThickness).Sum();

			reps.Add(new IfcShapeRepresentation( new IfcExtrudedAreaSolid(new IfcRectangleProfileDef(db,"",length, t), new IfcAxis2Placement3D(new IfcCartesianPoint(db,length/2.0, layerSetUsage.OffsetFromReferenceLine + (layerSetUsage.DirectionSense == IfcDirectionSenseEnum.POSITIVE ? 1 : -1) * t/2.0, 0)), height)));
			Representation = new IfcProductDefinitionShape(reps);
		}
	}
	[Serializable]
	public partial class IfcWallType : IfcBuiltElementType
	{
		private IfcWallTypeEnum mPredefinedType = IfcWallTypeEnum.NOTDEFINED;
		public IfcWallTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcWallTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcWallType() : base() { }
		internal IfcWallType(DatabaseIfc db, IfcWallType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcWallType(DatabaseIfc db, string name, IfcWallTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
		public IfcWallType(string name, IfcMaterialLayerSet ls, IfcWallTypeEnum type) : base(ls.mDatabase) { Name = name; PredefinedType = type; MaterialSelect = ls; }
	}
	[Serializable]
	public partial class IfcWarpingStiffnessSelect
	{
		internal bool mFixed;
		internal double mStiffness;
		internal IfcWarpingStiffnessSelect(bool fix) { mFixed = fix; mStiffness = 0; }
		internal IfcWarpingStiffnessSelect(double stiff) { mFixed = false; mStiffness = stiff; }
	}
	[Serializable]
	public partial class IfcWasteTerminal : IfcFlowTerminal //IFC4
	{
		private IfcWasteTerminalTypeEnum mPredefinedType = IfcWasteTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcWasteTerminalTypeEnum;
		public IfcWasteTerminalTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcWasteTerminalTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcWasteTerminal() : base() { }
		internal IfcWasteTerminal(DatabaseIfc db, IfcWasteTerminal t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcWasteTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcWasteTerminalType : IfcFlowTerminalType
	{
		private IfcWasteTerminalTypeEnum mPredefinedType = IfcWasteTerminalTypeEnum.NOTDEFINED;// : IfcWasteTerminalTypeEnum; 
		public IfcWasteTerminalTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcWasteTerminalTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcWasteTerminalType() : base() { }
		internal IfcWasteTerminalType(DatabaseIfc db, IfcWasteTerminalType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcWasteTerminalType(DatabaseIfc db, string name, IfcWasteTerminalTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcWaterProperties : IfcMaterialProperties // DEPRECATED IFC4
	{
		internal bool mIsPotable = false;// : 	OPTIONAL BOOLEAN;
		internal double mHardness = double.NaN, mAlkalinityConcentration = double.NaN, mAcidityConcentration = double.NaN;// : : 	OPTIONAL IfcIonConcentrationMeasure
		internal double mImpuritiesContent = double.NaN;//: 	OPTIONAL IfcNormalisedRatioMeasure
		internal double mPHLevel = double.NaN;  //: 	OPTIONAL IfcPHMeasure;
		internal double mDissolvedSolidsContent = double.NaN;//: 	OPTIONAL IfcNormalisedRatioMeasure
		internal IfcWaterProperties() : base() { }
		internal IfcWaterProperties(DatabaseIfc db, IfcWaterProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mIsPotable = p.mIsPotable;
			mHardness = p.mHardness;
			mAlkalinityConcentration = p.mAlkalinityConcentration;
			mAcidityConcentration = p.mAcidityConcentration;
			mImpuritiesContent = p.mImpuritiesContent;
			mPHLevel = p.mPHLevel;
			mDissolvedSolidsContent = p.mDissolvedSolidsContent;
		}
	}
	[Obsolete("RELEASE CANDIDATE IFC4X3", false)]
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcWaterStratum : IfcGeotechnicalStratum
	{
		public override string StepClassName { get { return (mDatabase.mRelease >= ReleaseVersion.IFC4X3 ? "IfcGeotechnicalStratum" : base.StepClassName); } }

		internal IfcWaterStratum() : base() { PredefinedType = IfcGeotechnicalStratumTypeEnum.WATER; }
		internal IfcWaterStratum(DatabaseIfc db) : base(db) { PredefinedType = IfcGeotechnicalStratumTypeEnum.WATER; }
		internal IfcWaterStratum(DatabaseIfc db, IfcWaterStratum waterStratum, DuplicateOptions options) : base(db, waterStratum, options) { PredefinedType = IfcGeotechnicalStratumTypeEnum.WATER; }
		internal IfcWaterStratum(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape shape) 
			: base(host, placement, shape) { PredefinedType = IfcGeotechnicalStratumTypeEnum.WATER; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcWellKnownText : BaseClassIfc
	{
		internal string mWellKnownText = "";// : IfcWellKnownTextLiteral;
		internal IfcCoordinateReferenceSystem mCoordinateReferenceSystem = null;// : IfcCoordinateReferenceSystem;
		public string WellKnownText { get { return mWellKnownText; } set { mWellKnownText = value; } }
		public IfcCoordinateReferenceSystem CoordinateReferenceSystem { get { return mCoordinateReferenceSystem; } set { mCoordinateReferenceSystem = value; if (value != null) value.WellKnownText = this; } }

		internal IfcWellKnownText() : base() { }
		internal IfcWellKnownText(DatabaseIfc db) : base(db) { }
		internal IfcWellKnownText(DatabaseIfc db, IfcWellKnownText w, DuplicateOptions options) : base(db) { mWellKnownText = w.mWellKnownText; mCoordinateReferenceSystem = db.Factory.Duplicate(w.mCoordinateReferenceSystem, options); }
		public IfcWellKnownText(string wellKnownText, IfcCoordinateReferenceSystem coordinateReferenceSystem) 
			: base(coordinateReferenceSystem.Database) 
		{
			mWellKnownText = wellKnownText;
			mCoordinateReferenceSystem = coordinateReferenceSystem;
		}
	}
	[Serializable]
	public partial class IfcWindow : IfcBuiltElement
	{
		internal double mOverallHeight = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mOverallWidth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure; 
		private IfcWindowTypeEnum mPredefinedType = IfcWindowTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWindowTypeEnum;
		internal IfcWindowTypePartitioningEnum mPartitioningType = IfcWindowTypePartitioningEnum.NOTDEFINED;//	 :	OPTIONAL IfcWindowTypePartitioningEnum;
		internal string mUserDefinedPartitioningType = "";//:	OPTIONAL IfcLabel;

		public double OverallHeight { get { return mOverallHeight; } set { mOverallHeight = (value > 0 ? value : double.NaN); } } 
		public double OverallWidth { get { return mOverallWidth; } set { mOverallWidth = (value > 0 ? value : double.NaN); } } 
		public IfcWindowTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcWindowTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public IfcWindowTypePartitioningEnum PartitioningType { get { return mPartitioningType; } set { mPartitioningType = value; } }
		public string UserDefinedPartitioningType { get { return mUserDefinedPartitioningType; } set { mUserDefinedPartitioningType = value; } }

		internal IfcWindow() : base() { }
		internal IfcWindow(DatabaseIfc db, IfcWindow w, DuplicateOptions options) : base(db, w, options) { mOverallHeight = w.mOverallHeight; mOverallWidth = w.mOverallWidth; PredefinedType = w.mPredefinedType; mPartitioningType = w.mPartitioningType; mUserDefinedPartitioningType = w.mUserDefinedPartitioningType; }
		public IfcWindow(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
		public IfcWindow(IfcOpeningElement host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) :
			base(host.Database)
		{
			ObjectPlacement = placement;
			Representation = representation;
			IfcRelFillsElement relFillsElement = new IfcRelFillsElement(host, this);
		}
	}
	[Serializable]
	public partial class IfcWindowLiningProperties : IfcPreDefinedPropertySet //IFC2x3 : IfcPropertySetDefinition
	{
		internal double mLiningDepth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mLiningThickness = double.NaN; //: OPTIONAL  IfcNonNegativeLengthMeasure
		internal double mTransomThickness = double.NaN, mMullionThickness = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mFirstTransomOffset = double.NaN, mSecondTransomOffset = double.NaN, mFirstMullionOffset = double.NaN, mSecondMullionOffset = double.NaN;// : OPTIONAL IfcNormalisedRatioMeasure;
		private IfcShapeAspect mShapeAspectStyle;// : OPTIONAL IfcShapeAspect; IFC4 DEPRECATED
		internal double mLiningOffset = double.NaN, mLiningToPanelOffsetX = double.NaN, mLiningToPanelOffsetY = double.NaN;//	 :	OPTIONAL IfcLengthMeasure;
		[Obsolete("DEPRECATED IFC4", false)]
		public IfcShapeAspect ShapeAspectStyle { get { return mShapeAspectStyle; } set { mShapeAspectStyle = value; } }
		internal IfcWindowLiningProperties() : base() { }
		internal IfcWindowLiningProperties(DatabaseIfc db, IfcWindowLiningProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mLiningDepth = p.mLiningDepth;
			mLiningThickness = p.mLiningThickness;
			mTransomThickness = p.mTransomThickness;
			mMullionThickness = p.mMullionThickness;
			mFirstTransomOffset = p.mFirstTransomOffset;
			mSecondTransomOffset = p.mSecondTransomOffset;
			mFirstMullionOffset = p.mFirstMullionOffset;
			mSecondMullionOffset = p.mSecondMullionOffset;
			ShapeAspectStyle = db.Factory.Duplicate(p.ShapeAspectStyle);
			mLiningOffset = p.mLiningOffset;
			mLiningToPanelOffsetX = p.mLiningToPanelOffsetX;
			mLiningToPanelOffsetY = p.mLiningToPanelOffsetY;
		}
		internal IfcWindowLiningProperties(DatabaseIfc db, string name, double lngDpth, double lngThck, double trnsmThck, double mllnThck,
			double trnsmOffst1, double trnsmOffst2, double mllnOffst1, double mllnOffst2, double lngOffset, double lngToPnlOffstX, double lngToPnlOffstY)
			: base(db, name)
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
	}
	[Serializable]
	public partial class IfcWindowPanelProperties : IfcPreDefinedPropertySet //IFC2x3: IfcPropertySetDefinition
	{
		internal IfcWindowPanelOperationEnum mOperationType;// : IfcWindowPanelOperationEnum;
		internal IfcWindowPanelPositionEnum mPanelPosition;// :IfcWindowPanelPositionEnume;
		internal double mFrameDepth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mFrameThickness = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		private IfcShapeAspect mShapeAspectStyle;// : OPTIONAL IfcShapeAspect; IFC4 DEPRECATED
		[Obsolete("DEPRECATED IFC4", false)]
		public IfcShapeAspect ShapeAspectStyle { get { return mShapeAspectStyle; } set { mShapeAspectStyle = value; } }

		internal IfcWindowPanelProperties() : base() { }
		internal IfcWindowPanelProperties(DatabaseIfc db, IfcWindowPanelProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mOperationType = p.mOperationType;
			mPanelPosition = p.mPanelPosition;
			mFrameDepth = p.mFrameDepth;
			mFrameThickness = p.mFrameThickness;
			if (p.mShapeAspectStyle != null)
				ShapeAspectStyle = db.Factory.Duplicate(p.ShapeAspectStyle);
		}
		public IfcWindowPanelProperties(DatabaseIfc db, string name, IfcWindowPanelOperationEnum op, IfcWindowPanelPositionEnum panel)
			: base(db, name)
		{
			mOperationType = op;
			mPanelPosition = panel;
		}
	}
	[Serializable]
	public partial class IfcWindowStandardCase : IfcWindow
	{
		public override string StepClassName { get { return "IfcWindow"; } }
		internal IfcWindowStandardCase() : base() { }
		internal IfcWindowStandardCase(DatabaseIfc db, IfcWindowStandardCase w, DuplicateOptions options) : base(db, w, options) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcWindowStyle : IfcTypeProduct // IFC2x3
	{
		internal IfcWindowStyleConstructionEnum mConstructionType;// : IfcWindowStyleConstructionEnum;
		internal IfcWindowStyleOperationEnum mOperationType;// : IfcWindowStyleOperationEnum;
		internal bool mParameterTakesPrecedence;// : BOOLEAN;
		internal bool mSizeable;// : BOOLEAN;
		internal IfcWindowStyle() : base() { }
		internal IfcWindowStyle(DatabaseIfc db, IfcWindowStyle s, DuplicateOptions options) : base(db, s, options) { mConstructionType = s.mConstructionType; mOperationType = s.mOperationType; mParameterTakesPrecedence = s.mParameterTakesPrecedence; mSizeable = s.mSizeable; }
	}
	[Serializable]
	public partial class IfcWindowType : IfcBuiltElementType //IFCWindowStyle IFC2x3
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcWindowStyle" : base.StepClassName); } }
		private IfcWindowTypeEnum mPredefinedType = IfcWindowTypeEnum.NOTDEFINED;
		internal IfcWindowTypePartitioningEnum mPartitioningType = IfcWindowTypePartitioningEnum.NOTDEFINED;// : IfcWindowTypePartitioningEnum; 
		internal bool mParameterTakesPrecedence = false;// : BOOLEAN; 
		internal string mUserDefinedPartitioningType = ""; // 	 :	OPTIONAL IfcLabel;

		public IfcWindowTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcWindowTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcWindowType() : base() { }
		internal IfcWindowType(DatabaseIfc db, IfcWindowType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; mPartitioningType = t.mPartitioningType; mParameterTakesPrecedence = t.mParameterTakesPrecedence; mUserDefinedPartitioningType = t.mUserDefinedPartitioningType; }
		public IfcWindowType(DatabaseIfc db, string name, IfcWindowTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcWorkCalendar : IfcControl //IFC4
	{
		internal SET<IfcWorkTime> mWorkingTimes = new SET<IfcWorkTime>();// :	OPTIONAL SET [1:?] OF IfcWorkTime;
		internal SET<IfcWorkTime> mExceptionTimes = new SET<IfcWorkTime>();//	 :	OPTIONAL SET [1:?] OF IfcWorkTime;
		private IfcWorkCalendarTypeEnum mPredefinedType = IfcWorkCalendarTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkCalendarTypeEnum 
		
		public SET<IfcWorkTime> WorkingTimes { get { return mWorkingTimes; } }
		public SET<IfcWorkTime> ExceptionTimes { get { return mExceptionTimes; } }
		public IfcWorkCalendarTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcWorkCalendarTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
	
		internal IfcWorkCalendar() : base() { }
		internal IfcWorkCalendar(DatabaseIfc db, IfcWorkCalendar c, DuplicateOptions options) : base(db, c, options)
		{
			mWorkingTimes.AddRange(c.WorkingTimes.Select(x => db.Factory.Duplicate(x) as IfcWorkTime));
			mExceptionTimes.AddRange(c.ExceptionTimes.Select(x=>db.Factory.Duplicate(x) as IfcWorkTime));
			mPredefinedType = c.mPredefinedType;
		}
		public IfcWorkCalendar(DatabaseIfc db, string name, IfcWorkCalendarTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcWorkControl : IfcControl //ABSTRACT SUPERTYPE OF(ONEOF(IfcWorkPlan, IfcWorkSchedule))
	{
		//internal string mIdentifier	 : 	IfcIdentifier; IFC4 moved to control
		internal DateTime mCreationDate;// : IfcDateTime;
		internal SET<IfcPerson> mCreators = new SET<IfcPerson>();// : OPTIONAL SET [1:?] OF IfcPerson;
		internal string mPurpose = "";// : OPTIONAL IfcLabel;
		internal string mDuration = "", mTotalFloat = "";// : OPTIONAL IfcDuration; IFC4
		internal DateTime mStartTime;// : IfcDateTime;
		internal DateTime mFinishTime = DateTime.MinValue;// : OPTIONAL IfcDateTime;  IFC4
		internal double mSSDuration = 0, mSSTotalFloat = 0; //: 	OPTIONAL IfcTimeMeasure; 
		internal IfcDateTimeSelect mSSCreationDate, mSSStartTime; //: 	IfcDateTimeSelect;
		internal IfcDateTimeSelect mSSFinishTime; //: OPTIONAL IfcDateTimeSelect;
		internal IfcWorkControlTypeEnum mWorkControlType = IfcWorkControlTypeEnum.NOTDEFINED;//	 : 	OPTIONAL IfcWorkControlTypeEnum; IFC2x3
		internal string mUserDefinedControlType = "";//	 : 	OPTIONAL IfcLabel;

		public DateTime CreationDate { get { return mCreationDate; } set { mCreationDate = value; } }
		public SET<IfcPerson> Creators { get { return mCreators; } } 
		public DateTime StartTime { get { return mStartTime; } set { mStartTime = value; } }
		public DateTime FinishTime { get { return mFinishTime; } set { mFinishTime = value; } }
		public string Purpose { get { return mPurpose; } set { mPurpose = value; } }

		protected IfcWorkControl() : base() { mCreationDate = DateTime.Now; mStartTime = DateTime.Now; }
		protected IfcWorkControl(DatabaseIfc db) : base(db)
		{
			mCreationDate = DateTime.Now;
			mStartTime = DateTime.Now;
			if (db.Release < ReleaseVersion.IFC4)
			{
				mSSCreationDate = new IfcCalendarDate(db, DateTime.Now);
				mSSStartTime = mSSCreationDate;
			}
		}
		protected IfcWorkControl(DatabaseIfc db, IfcWorkControl c, DuplicateOptions options) : base(db,c, options)
		{
			mCreationDate = c.mCreationDate;
			Creators.AddRange(c.mCreators.ConvertAll(x=>db.Factory.Duplicate(x) as IfcPerson));
			mDuration = c.mDuration;
			mTotalFloat = c.mTotalFloat;
			mStartTime = c.mStartTime;
			mFinishTime = c.mFinishTime;
		}
	
		internal DateTime getStart()
		{
			if (mStartTime != DateTime.MinValue)
				return mStartTime;
			if (mSSStartTime != null)
				return mSSStartTime.DateTime;
			return DateTime.MinValue;
		}
		public Tuple<DateTime, DateTime> ComputeAndSetScheduledStartFinishTimes()
		{
			DateTime scheduledStart = DateTime.MaxValue, scheduledFinish = DateTime.MinValue;
			List<IfcWorkControl> subControls = IsDecomposedBy.SelectMany(x => x.RelatedObjects).OfType<IfcWorkControl>().ToList();
			subControls.AddRange(IsNestedBy.SelectMany(x => x.RelatedObjects).OfType<IfcWorkControl>());
			foreach (IfcWorkControl workControl in subControls)
			{
				Tuple<DateTime, DateTime> startFinish = workControl.ComputeAndSetScheduledStartFinishTimes();
				if (startFinish.Item1 != DateTime.MinValue && startFinish.Item1 < scheduledStart)
					scheduledStart = startFinish.Item1;
				if (startFinish.Item2 > scheduledFinish)
					scheduledFinish = startFinish.Item2;
			}
			foreach(IfcTask task in Controls.SelectMany(x=>x.RelatedObjects).OfType<IfcTask>())
			{
				Tuple<DateTime, DateTime> startFinish = task.computeScheduleStartFinish();
				if (startFinish.Item1 != DateTime.MinValue && startFinish.Item1 < scheduledStart)
					scheduledStart = startFinish.Item1;
				if (startFinish.Item2 > scheduledFinish)
					scheduledFinish = startFinish.Item2;
			}
			if (mDatabase.Release > ReleaseVersion.IFC2x3)
			{
				mStartTime = scheduledStart;
				mFinishTime = scheduledFinish;
			}
			else
			{
				if (scheduledStart > DateTime.MinValue)
					mSSStartTime = new IfcDateAndTime(mDatabase, scheduledStart);
				if (scheduledFinish > DateTime.MinValue)
					mSSFinishTime = new IfcDateAndTime(mDatabase, scheduledFinish);
			}
			return new Tuple<DateTime, DateTime>(scheduledStart, scheduledFinish);
		}
	}
	[Serializable]
	public partial class IfcWorkPlan : IfcWorkControl
	{
		private IfcWorkPlanTypeEnum mPredefinedType = IfcWorkPlanTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkPlanTypeEnum; IFC4
		public IfcWorkPlanTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcWorkPlanTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		
		internal IfcWorkPlan() : base() { }
		public IfcWorkPlan(DatabaseIfc db) : base(db) { }
		internal IfcWorkPlan(DatabaseIfc db, IfcWorkPlan p, DuplicateOptions options) : base(db, p, options) { PredefinedType = p.PredefinedType; }
	}
	[Serializable]
	public partial class IfcWorkSchedule : IfcWorkControl
	{
		private IfcWorkScheduleTypeEnum mPredefinedType = IfcWorkScheduleTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkScheduleTypeEnum; IFC4
		public IfcWorkScheduleTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcWorkScheduleTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcWorkSchedule() : base() { }
		public IfcWorkSchedule(DatabaseIfc db) : base(db) { }
		public IfcWorkSchedule(IfcWorkPlan workPlan) : base(workPlan.Database) { workPlan.AddAggregated(this); }
		internal IfcWorkSchedule(DatabaseIfc db, IfcWorkSchedule s, DuplicateOptions options) : base(db, s, options) { PredefinedType = s.PredefinedType; }
	}
	[Serializable]
	public partial class IfcWorkTime : IfcSchedulingTime //IFC4
	{
		internal IfcRecurrencePattern mRecurrencePattern;// OPTIONAL	IfcRecurrencePattern
		internal DateTime mStartDate = DateTime.MinValue;//	 :	OPTIONAL IfcDate;
		internal DateTime mFinishDate = DateTime.MinValue;//	 :	OPTIONAL IfcDate; 

		internal IfcRecurrencePattern RecurrencePattern { get { return mRecurrencePattern; } set { mRecurrencePattern = value; } }
		public DateTime StartDate { get { return mStartDate; } set { mStartDate = value; } }
		public DateTime FinishDate { get { return mFinishDate; } set { mFinishDate = value; } }

		internal IfcWorkTime() : base() { }
		internal IfcWorkTime(DatabaseIfc db, IfcWorkTime t, DuplicateOptions options) : base(db, t, options) { mRecurrencePattern = t.mRecurrencePattern; mStartDate = t.mStartDate; mFinishDate = t.mFinishDate; }
		public IfcWorkTime(DatabaseIfc db) : base(db) { }
	}
}
