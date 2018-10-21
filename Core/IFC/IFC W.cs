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
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public partial class IfcWall : IfcBuildingElement
	{
		internal IfcWallTypeEnum mPredefinedType = IfcWallTypeEnum.NOTDEFINED;//: OPTIONAL IfcWallTypeEnum; 
		public IfcWallTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcWall() : base() { }
		internal IfcWall(DatabaseIfc db, IfcWall w, IfcOwnerHistory ownerHistory, bool downStream) : base(db, w, ownerHistory, downStream) { mPredefinedType = w.mPredefinedType; }
		public IfcWall(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcWallElementedCase : IfcWall
	{
		internal IfcWallElementedCase() : base() { }
		internal IfcWallElementedCase(DatabaseIfc db, IfcWallElementedCase w, IfcOwnerHistory ownerHistory, bool downStream) : base(db, w, ownerHistory, downStream) { }
	}
	[Serializable]
	public partial class IfcWallStandardCase : IfcWall
	{
		internal override string KeyWord { get { return "IfcWall" + (mDatabase.mRelease < ReleaseVersion.IFC4 ? "StandardCase" : ""); } }
		internal IfcWallStandardCase() : base() { }
		internal IfcWallStandardCase(DatabaseIfc db, IfcWallStandardCase w, IfcOwnerHistory ownerHistory, bool downStream) : base(db, w, ownerHistory, downStream) { }
		public IfcWallStandardCase(IfcProduct container, IfcMaterialLayerSetUsage layerSetUsage, IfcAxis2Placement3D placement, double length, double height)
			:base(container,new IfcLocalPlacement(container.Placement, placement),null)
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
	public partial class IfcWallType : IfcBuildingElementType
	{
		internal IfcWallTypeEnum mPredefinedType = IfcWallTypeEnum.NOTDEFINED;
		public IfcWallTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcWallType() : base() { }
		internal IfcWallType(DatabaseIfc db, IfcWallType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcWallType(DatabaseIfc m, string name, IfcWallTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcWallType(string name, IfcMaterialLayerSet ls, IfcWallTypeEnum type) : base(ls.mDatabase) { Name = name; mPredefinedType = type; MaterialSelect = ls; }
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
		internal IfcWasteTerminalTypeEnum mPredefinedType = IfcWasteTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcWasteTerminalTypeEnum;
		public IfcWasteTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcWasteTerminal() : base() { }
		internal IfcWasteTerminal(DatabaseIfc db, IfcWasteTerminal t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcWasteTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcWasteTerminalType : IfcFlowTerminalType
	{
		internal IfcWasteTerminalTypeEnum mPredefinedType = IfcWasteTerminalTypeEnum.NOTDEFINED;// : IfcWasteTerminalTypeEnum; 
		public IfcWasteTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcWasteTerminalType() : base() { }
		internal IfcWasteTerminalType(DatabaseIfc db, IfcWasteTerminalType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcWasteTerminalType(DatabaseIfc m, string name, IfcWasteTerminalTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcWaterProperties : IfcMaterialProperties // DEPRECEATED IFC4
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
	}
	[Serializable]
	public partial class IfcWindow : IfcBuildingElement
	{
		internal double mOverallHeight = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mOverallWidth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure; 
		internal IfcWindowTypeEnum mPredefinedType = IfcWindowTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWindowTypeEnum;
		internal IfcWindowTypePartitioningEnum mPartitioningType = IfcWindowTypePartitioningEnum.NOTDEFINED;//	 :	OPTIONAL IfcWindowTypePartitioningEnum;
		internal string mUserDefinedPartitioningType = "$";//:	OPTIONAL IfcLabel;

		public double OverallHeight { get { return mOverallHeight; } set { mOverallHeight = (value > 0 ? value : double.NaN); } } 
		public double OverallWidth { get { return mOverallWidth; } set { mOverallWidth = (value > 0 ? value : double.NaN); } } 
		public IfcWindowTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcWindowTypePartitioningEnum PartitioningType { get { return mPartitioningType; } set { mPartitioningType = value; } }
		public string UserDefinedPartitioningType { get { return (mUserDefinedPartitioningType == "$" ? "" : ParserIfc.Decode(mUserDefinedPartitioningType)); } set { mUserDefinedPartitioningType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcWindow() : base() { }
		internal IfcWindow(DatabaseIfc db, IfcWindow w, IfcOwnerHistory ownerHistory, bool downStream) : base(db, w, ownerHistory, downStream) { mOverallHeight = w.mOverallHeight; mOverallWidth = w.mOverallWidth; mPredefinedType = w.mPredefinedType; mPartitioningType = w.mPartitioningType; mUserDefinedPartitioningType = w.mUserDefinedPartitioningType; }
		public IfcWindow(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcWindowLiningProperties : IfcPreDefinedPropertySet //IFC2x3 : IfcPropertySetDefinition
	{
		internal double mLiningDepth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mLiningThickness = double.NaN; //: OPTIONAL  IfcNonNegativeLengthMeasure
		internal double mTransomThickness = double.NaN, mMullionThickness = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mFirstTransomOffset = double.NaN, mSecondTransomOffset = double.NaN, mFirstMullionOffset = double.NaN, mSecondMullionOffset = double.NaN;// : OPTIONAL IfcNormalisedRatioMeasure;
		private int mShapeAspectStyle;// : OPTIONAL IfcShapeAspect; IFC4 Depreceated
		internal double mLiningOffset = double.NaN, mLiningToPanelOffsetX = double.NaN, mLiningToPanelOffsetY = double.NaN;//	 :	OPTIONAL IfcLengthMeasure;
		[Obsolete("DEPRECEATED IFC4", false)]
		public IfcShapeAspect ShapeAspectStyle { get { return mDatabase[mShapeAspectStyle] as IfcShapeAspect; } set { mShapeAspectStyle = (value == null ? 0 : value.mIndex); } }
		internal IfcWindowLiningProperties() : base() { }
		internal IfcWindowLiningProperties(DatabaseIfc db, IfcWindowLiningProperties p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream)
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
	}
	[Serializable]
	public partial class IfcWindowPanelProperties : IfcPreDefinedPropertySet //IFC2x3: IfcPropertySetDefinition
	{
		internal IfcWindowPanelOperationEnum mOperationType;// : IfcWindowPanelOperationEnum;
		internal IfcWindowPanelPositionEnum mPanelPosition;// :IfcWindowPanelPositionEnume;
		internal double mFrameDepth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mFrameThickness = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		private int mShapeAspectStyle;// : OPTIONAL IfcShapeAspect; IFC4 Depreceated
		[Obsolete("DEPRECEATED IFC4", false)]
		public IfcShapeAspect ShapeAspectStyle { get { return mDatabase[mShapeAspectStyle] as IfcShapeAspect; } set { mShapeAspectStyle = (value == null ? 0 : value.mIndex); } }

		internal IfcWindowPanelProperties() : base() { }
		internal IfcWindowPanelProperties(DatabaseIfc db, IfcWindowPanelProperties p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream)
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
	}
	[Serializable]
	public partial class IfcWindowStandardCase : IfcWindow
	{
		internal override string KeyWord { get { return "IfcWindow"; } }
		internal IfcWindowStandardCase() : base() { }
		internal IfcWindowStandardCase(DatabaseIfc db, IfcWindowStandardCase w, IfcOwnerHistory ownerHistory, bool downStream) : base(db, w, ownerHistory, downStream) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcWindowStyle : IfcTypeProduct // IFC2x3
	{
		internal IfcWindowStyleConstructionEnum mConstructionType;// : IfcWindowStyleConstructionEnum;
		internal IfcWindowStyleOperationEnum mOperationType;// : IfcWindowStyleOperationEnum;
		internal bool mParameterTakesPrecedence;// : BOOLEAN;
		internal bool mSizeable;// : BOOLEAN;
		internal IfcWindowStyle() : base() { }
		internal IfcWindowStyle(DatabaseIfc db, IfcWindowStyle s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { mConstructionType = s.mConstructionType; mOperationType = s.mOperationType; mParameterTakesPrecedence = s.mParameterTakesPrecedence; mSizeable = s.mSizeable; }
	}
	[Serializable]
	public partial class IfcWindowType : IfcBuildingElementType //IFCWindowStyle IFC2x3
	{
		internal override string KeyWord { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcWindowStyle" : base.KeyWord); } }
		internal IfcWindowTypeEnum mPredefinedType = IfcWindowTypeEnum.NOTDEFINED;
		internal IfcWindowTypePartitioningEnum mPartitioningType = IfcWindowTypePartitioningEnum.NOTDEFINED;// : IfcWindowTypePartitioningEnum; 
		internal bool mParameterTakesPrecedence;// : BOOLEAN; 
		internal string mUserDefinedPartitioningType = "$"; // 	 :	OPTIONAL IfcLabel;

		public IfcWindowTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcWindowType() : base() { }
		internal IfcWindowType(DatabaseIfc db, IfcWindowType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; mPartitioningType = t.mPartitioningType; mParameterTakesPrecedence = t.mParameterTakesPrecedence; mUserDefinedPartitioningType = t.mUserDefinedPartitioningType; }
		public IfcWindowType(DatabaseIfc m, string name, IfcWindowTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcWindowType(DatabaseIfc m, string name, IfcWindowTypeEnum type, IfcWindowTypePartitioningEnum partition, bool parameterTakesPrecendence)
			: base(m) { Name = name; mPredefinedType = type; mPartitioningType = partition; mParameterTakesPrecedence = parameterTakesPrecendence; }
		public IfcWindowType(DatabaseIfc m, string name, IfcWindowTypeEnum type, IfcWindowTypePartitioningEnum partition, string userDefinedPartionType, IfcWindowLiningProperties wlp, List<IfcWindowPanelProperties> pps)
			: base(m)
		{
			Name = name;
			mPredefinedType = type;
			mPartitioningType = partition;
			mParameterTakesPrecedence = true;
			if (wlp != null)
				mHasPropertySets.Add(wlp);
			if (pps != null && pps.Count > 0)
				mHasPropertySets.AddRange(pps);
			if (!string.IsNullOrEmpty(userDefinedPartionType))
				mUserDefinedPartitioningType = userDefinedPartionType.Replace("'", "");
		}
	}
	[Serializable]
	public partial class IfcWorkCalendar : IfcControl //IFC4
	{
		internal List<int> mWorkingTimes = new List<int>();// :	OPTIONAL SET [1:?] OF IfcWorkTime;
		internal List<int> mExceptionTimes = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcWorkTime;
		internal IfcWorkCalendarTypeEnum mPredefinedType = IfcWorkCalendarTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkCalendarTypeEnum 
		public ReadOnlyCollection<IfcWorkTime> WorkingTimes { get { return new ReadOnlyCollection<IfcWorkTime>(mWorkingTimes.ConvertAll(x => mDatabase[x] as IfcWorkTime)); } }
		public ReadOnlyCollection<IfcWorkTime> ExceptionTimes { get { return new ReadOnlyCollection<IfcWorkTime>( mExceptionTimes.ConvertAll(x => mDatabase[x] as IfcWorkTime)); } }
		internal IfcWorkCalendar() : base() { }
		internal IfcWorkCalendar(DatabaseIfc db, IfcWorkCalendar c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream)
		{
			c.WorkingTimes.ToList().ForEach(x => addWorkingTime( db.Factory.Duplicate(x) as IfcWorkTime));
			c.ExceptionTimes.ToList().ForEach(x=>addExceptionTimes( db.Factory.Duplicate(x) as IfcWorkTime));
			mPredefinedType = c.mPredefinedType;
		}
		internal IfcWorkCalendar(DatabaseIfc db, string name, IfcWorkCalendarTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }
		
		internal void addWorkingTime(IfcWorkTime time) { mWorkingTimes.Add(time.mIndex); }
		internal void addExceptionTimes(IfcWorkTime time) { mExceptionTimes.Add(time.mIndex); }
	}
	[Serializable]
	public abstract partial class IfcWorkControl : IfcControl //ABSTRACT SUPERTYPE OF(ONEOF(IfcWorkPlan, IfcWorkSchedule))
	{
		//internal string mIdentifier	 : 	IfcIdentifier; IFC4 moved to control
		internal DateTime mCreationDate;// : IfcDateTime;
		internal SET<IfcPerson> mCreators = new SET<IfcPerson>();// : OPTIONAL SET [1:?] OF IfcPerson;
		internal string mPurpose = "$";// : OPTIONAL IfcLabel;
		internal string mDuration = "$", mTotalFloat = "$";// : OPTIONAL IfcDuration; IFC4
		internal DateTime mStartTime;// : IfcDateTime;
		internal DateTime mFinishTime = DateTime.MinValue;// : OPTIONAL IfcDateTime;  IFC4
		internal double mSSDuration = 0, mSSTotalFloat = 0; //: 	OPTIONAL IfcTimeMeasure; 
		internal int mSSCreationDate, mSSStartTime; //: 	IfcDateTimeSelect;
		internal int mSSFinishTime; //: OPTIONAL IfcDateTimeSelect;
		internal IfcWorkControlTypeEnum mWorkControlType = IfcWorkControlTypeEnum.NOTDEFINED;//	 : 	OPTIONAL IfcWorkControlTypeEnum; IFC2x3
		internal string mUserDefinedControlType = "$";//	 : 	OPTIONAL IfcLabel;

		public SET<IfcPerson> Creators { get { return mCreators; } } 
		public string Purpose { get { return (mPurpose == "$" ? "" : ParserIfc.Decode(mPurpose)); } set { mPurpose = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcWorkControl() : base() { }
		protected IfcWorkControl(DatabaseIfc db, IfcWorkControl c, IfcOwnerHistory ownerHistory, bool downStream) : base(db,c, ownerHistory, downStream)
		{
			mCreationDate = c.mCreationDate;
			Creators.AddRange(c.mCreators.ConvertAll(x=>db.Factory.Duplicate(x) as IfcPerson));
			mDuration = c.mDuration;
			mTotalFloat = c.mTotalFloat;
			mStartTime = c.mStartTime;
			mFinishTime = c.mFinishTime;
		}
	
		internal DateTime getStart() { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? (mDatabase[mSSStartTime] as IfcDateTimeSelect).DateTime : DateTime.MinValue); }
	}
	[Serializable]
	public partial class IfcWorkPlan : IfcWorkControl
	{
		internal IfcWorkPlanTypeEnum mPredefinedType = IfcWorkPlanTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkPlanTypeEnum; IFC4
		internal IfcWorkPlan() : base() { }
		internal IfcWorkPlan(DatabaseIfc db, IfcWorkPlan p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream) { mPredefinedType = p.mPredefinedType; }
	}
	[Serializable]
	public partial class IfcWorkSchedule : IfcWorkControl
	{
		internal IfcWorkScheduleTypeEnum mPredefinedType = IfcWorkScheduleTypeEnum.NOTDEFINED;//	 :	OPTIONAL IfcWorkScheduleTypeEnum; IFC4
		internal IfcWorkSchedule() : base() { }
		internal IfcWorkSchedule(DatabaseIfc db, IfcWorkSchedule s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory, downStream) { mPredefinedType = s.mPredefinedType; }
	}
	[Serializable]
	public partial class IfcWorkTime : IfcSchedulingTime //IFC4
	{
		internal int mRecurrencePattern;// OPTIONAL	IfcRecurrencePattern
		internal DateTime mStart = DateTime.MinValue;//	 :	OPTIONAL IfcDate;
		internal DateTime mFinish = DateTime.MinValue;//	 :	OPTIONAL IfcDate; 

		internal IfcRecurrencePattern RecurrencePattern { get { return mDatabase[mRecurrencePattern] as IfcRecurrencePattern; } set { mRecurrencePattern = (value == null ? 0 : value.mIndex); } }
		public DateTime Start { get { return mStart; } set { mStart = value; } }
		public DateTime Finish { get { return mFinish; } set { mFinish = value; } }

		internal IfcWorkTime() : base() { }
		internal IfcWorkTime(DatabaseIfc db, IfcWorkTime t) : base(db,t) { mRecurrencePattern = t.mRecurrencePattern; mStart = t.mStart; mFinish = t.mFinish; }
		public IfcWorkTime(DatabaseIfc db) : base(db) { }
	}
}
