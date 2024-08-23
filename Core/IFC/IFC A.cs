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
	public partial class IfcActionRequest : IfcControl
	{
		//internal string mRequestID;// : IfcIdentifier; IFC4 relocated
		internal IfcActionRequest() : base() { }
		internal IfcActionRequest(DatabaseIfc db, IfcActionRequest r, DuplicateOptions options) : base(db,r, options) { }

		public IfcActionRequest(IfcProject project) : base(project.Database)
		{
			project.AddDeclared(this);
		}
	}
	[Serializable]
	public partial class IfcActor : IfcObject // SUPERTYPE OF(IfcOccupant)
	{
		internal IfcActorSelect mTheActor = null;//	 :	IfcActorSelect; 
		//INVERSE
		internal SET<IfcRelAssignsToActor> mIsActingUpon = new SET<IfcRelAssignsToActor>();// : SET [0:?] OF IfcRelAssignsToActor FOR RelatingActor;

		public IfcActorSelect TheActor { get { return mTheActor; } set { mTheActor = value; } }
		public SET<IfcRelAssignsToActor> IsActingUpon { get { return mIsActingUpon; } }

		internal IfcActor() : base() { }
		internal IfcActor(DatabaseIfc db, IfcActor actor, DuplicateOptions options) : base(db, actor, options) { TheActor = db.Factory.Duplicate(actor.TheActor as BaseClassIfc) as IfcActorSelect; }
		public IfcActor(IfcActorSelect actor) : base(actor.Database) { mTheActor = actor; }
	}
	[Serializable]
	public partial class IfcActorRole : BaseClassIfc, IfcResourceObjectSelect
	{
		internal IfcRoleEnum mRole = IfcRoleEnum.NOTDEFINED;// : OPTIONAL IfcRoleEnum
		internal string mUserDefinedRole = "";// : OPTIONAL IfcLabel
		internal string mDescription = "";// : OPTIONAL IfcText; 
		//INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal LIST<IfcResourceConstraintRelationship> mHasConstraintRelationships = new LIST<IfcResourceConstraintRelationship>(); //gg

		public IfcRoleEnum Role { get { return mRole; } set { mRole = value; } }
		public string UserDefinedRole { get { return mUserDefinedRole; } set { mUserDefinedRole = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } }
		public LIST<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcActorRole() : base() { }
		internal IfcActorRole(DatabaseIfc db, IfcActorRole r) : base(db,r) { mRole = r.mRole; mDescription = r.mDescription; mUserDefinedRole = r.mUserDefinedRole; }
		public IfcActorRole(DatabaseIfc db) : base(db) { }

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
	public interface IfcActorSelect : IBaseClassIfc {  }// IfcOrganization,  IfcPerson,  IfcPersonAndOrganization);
	[Serializable]
	public partial class IfcActuator : IfcDistributionControlElement //IFC4  
	{   
		private IfcActuatorTypeEnum mPredefinedType = IfcActuatorTypeEnum.NOTDEFINED;
		public IfcActuatorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcActuatorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcActuator() : base() { }
		internal IfcActuator(DatabaseIfc db, IfcActuator a, DuplicateOptions options) : base(db,a, options) { PredefinedType = a.mPredefinedType; }
		public IfcActuator(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement,representation, system) { }
	}
	[Serializable]
	public partial class IfcActuatorType : IfcDistributionControlElementType
	{
		private IfcActuatorTypeEnum mPredefinedType = IfcActuatorTypeEnum.NOTDEFINED;// : IfcActuatorTypeEnum; 
		public IfcActuatorTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcActuatorTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcActuatorType() : base() { }
		internal IfcActuatorType(DatabaseIfc db, IfcActuatorType t, DuplicateOptions options) : base(db, t, options)  { PredefinedType = t.mPredefinedType; }
		public IfcActuatorType(DatabaseIfc db, string name, IfcActuatorTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public abstract partial class IfcAddress : BaseClassIfc, IfcObjectReferenceSelect   //ABSTRACT SUPERTYPE OF(ONEOF(IfcPostalAddress, IfcTelecomAddress));
	{
		internal IfcAddressTypeEnum mPurpose = IfcAddressTypeEnum.NOTDEFINED;// : OPTIONAL IfcAddressTypeEnum
		internal string mDescription = "";// : OPTIONAL IfcText;
		internal string mUserDefinedPurpose = "";// : OPTIONAL IfcLabel 

		public IfcAddressTypeEnum Purpose { get { return mPurpose; } set { mPurpose = value; } }
		public string Description { get { return mDescription ; } set { mDescription = value; } }
		public string UserDefinedPurpose { get { return mUserDefinedPurpose; } set { mUserDefinedPurpose = value; } }
		
		protected IfcAddress() : base() { }
		protected IfcAddress(DatabaseIfc db) : base(db) {  }
		protected IfcAddress(DatabaseIfc db, IfcAddress a) : base(db,a)
		{
			mPurpose = a.mPurpose;
			mDescription = a.mDescription;
			mUserDefinedPurpose = a.mUserDefinedPurpose;
		}
	}
	[Serializable]
	public partial class IfcAdvancedBrep : IfcManifoldSolidBrep // IFC4
	{
		internal IfcAdvancedBrep() : base() { }
		public IfcAdvancedBrep(List<IfcAdvancedFace> faces) : base(new IfcClosedShell(faces.ConvertAll(x => (IfcFace)x))) { }
		internal IfcAdvancedBrep(DatabaseIfc db, IfcAdvancedBrep b, DuplicateOptions options) : base(db, b, options) { }
		public IfcAdvancedBrep(IfcClosedShell s) : base(s) { }
	}
	[Serializable]
	public partial class IfcAdvancedBrepWithVoids : IfcAdvancedBrep
	{
		private SET<IfcClosedShell> mVoids = new SET<IfcClosedShell>();// : SET [1:?] OF IfcClosedShell
		public SET<IfcClosedShell> Voids { get { return mVoids; } }

		internal IfcAdvancedBrepWithVoids() : base() { }
		internal IfcAdvancedBrepWithVoids(DatabaseIfc db, IfcAdvancedBrepWithVoids b, DuplicateOptions options) : base(db, b, options) { mVoids.AddRange(b.Voids.ConvertAll(x=> db.Factory.Duplicate(x) as IfcClosedShell)); }
		public IfcAdvancedBrepWithVoids(IfcClosedShell s, IEnumerable<IfcClosedShell> voids) : base(s) { Voids.AddRange(voids); }
	}
	[Serializable]
	public partial class IfcAdvancedFace : IfcFaceSurface
	{
		internal IfcAdvancedFace() : base() { }
		internal IfcAdvancedFace(DatabaseIfc db, IfcAdvancedFace f, DuplicateOptions options) : base(db, f, options) { }
		public IfcAdvancedFace(IfcFaceOuterBound bound, IfcSurface f, bool sense) : base(bound, f, sense) { }
		public IfcAdvancedFace(List<IfcFaceBound> bounds, IfcSurface f, bool sense) : base(bounds, f, sense) { }
		public IfcAdvancedFace(IfcFaceOuterBound outer, IfcFaceBound inner, IfcSurface f, bool sense) : base(outer,inner, f, sense) { }
	}
	[Serializable]
	public partial class IfcAirTerminal : IfcFlowTerminal //IFC4
	{
		private IfcAirTerminalTypeEnum mPredefinedType = IfcAirTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcAirTerminalTypeEnum;
		public IfcAirTerminalTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAirTerminalTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAirTerminal() : base() { }
		internal IfcAirTerminal(DatabaseIfc db, IfcAirTerminal t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcAirTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcAirTerminalBox : IfcFlowController //IFC4
	{
		private IfcAirTerminalBoxTypeEnum mPredefinedType = IfcAirTerminalBoxTypeEnum.NOTDEFINED;// OPTIONAL : IfcAirTerminalBoxTypeEnum;
		public IfcAirTerminalBoxTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAirTerminalBoxTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAirTerminalBox() : base() { }
		internal IfcAirTerminalBox(DatabaseIfc db, IfcAirTerminalBox b, DuplicateOptions options) : base(db, b, options) { PredefinedType = b.PredefinedType; }
		public IfcAirTerminalBox(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcAirTerminalBoxType : IfcFlowControllerType
	{
		private IfcAirTerminalBoxTypeEnum mPredefinedType = IfcAirTerminalBoxTypeEnum.NOTDEFINED;// : IfcAirTerminalBoxTypeEnum; 
		public IfcAirTerminalBoxTypeEnum PredefinedType { get { return mPredefinedType;}  set { mPredefinedType = validPredefinedType<IfcAirTerminalBoxTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAirTerminalBoxType() : base() { }
		internal IfcAirTerminalBoxType(DatabaseIfc db, IfcAirTerminalBoxType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.mPredefinedType; }
		public IfcAirTerminalBoxType(DatabaseIfc db, string name, IfcAirTerminalBoxTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcAirTerminalType : IfcFlowTerminalType
	{
		private IfcAirTerminalTypeEnum mPredefinedType = IfcAirTerminalTypeEnum.NOTDEFINED;// : IfcAirTerminalBoxTypeEnum; 
		public IfcAirTerminalTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAirTerminalTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAirTerminalType() : base() { }
		internal IfcAirTerminalType(DatabaseIfc db, IfcAirTerminalType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.mPredefinedType; }
		public IfcAirTerminalType(DatabaseIfc db, string name, IfcAirTerminalTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcAirToAirHeatRecovery : IfcEnergyConversionDevice //IFC4  
	{
		private IfcAirToAirHeatRecoveryTypeEnum mPredefinedType = IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED;
		public IfcAirToAirHeatRecoveryTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAirToAirHeatRecoveryTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAirToAirHeatRecovery() : base() { }
		internal IfcAirToAirHeatRecovery(DatabaseIfc db, IfcAirToAirHeatRecovery a, DuplicateOptions options) : base(db, a, options) { PredefinedType = a.mPredefinedType; }
		public IfcAirToAirHeatRecovery(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcAirToAirHeatRecoveryType : IfcEnergyConversionDeviceType
	{
		private IfcAirToAirHeatRecoveryTypeEnum mPredefinedType = IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED;// : IfcAirToAirHeatRecoveryTypeEnum; 
		public IfcAirToAirHeatRecoveryTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAirToAirHeatRecoveryTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAirToAirHeatRecoveryType() : base() { }
		internal IfcAirToAirHeatRecoveryType(DatabaseIfc db, IfcAirToAirHeatRecoveryType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.mPredefinedType; }
		public IfcAirToAirHeatRecoveryType(DatabaseIfc db, string name, IfcAirToAirHeatRecoveryTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcAlarm : IfcDistributionControlElement //IFC4  
	{
		private IfcAlarmTypeEnum mPredefinedType = IfcAlarmTypeEnum.NOTDEFINED;
		public IfcAlarmTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAlarmTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAlarm() : base() { }
		internal IfcAlarm(DatabaseIfc db, IfcAlarm a, DuplicateOptions options) : base(db, a, options) { PredefinedType = a.PredefinedType; }
		public IfcAlarm(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcAlarmType : IfcDistributionControlElementType
	{
		private IfcAlarmTypeEnum mPredefinedType = IfcAlarmTypeEnum.NOTDEFINED;// : IfcAlarmTypeEnum; 
		public IfcAlarmTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAlarmTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAlarmType() : base() { }
		internal IfcAlarmType(DatabaseIfc db, IfcAlarmType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcAlarmType(DatabaseIfc db, string name, IfcAlarmTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment : IfcLinearPositioningElement 
	{
		private IfcAlignmentTypeEnum mPredefinedType = IfcAlignmentTypeEnum.NOTDEFINED;// : OPTIONAL IfcAlignmentTypeEnum;
		public IfcAlignmentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAlignmentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAlignment() : base() { }
		internal IfcAlignment(DatabaseIfc db, IfcAlignment alignment, DuplicateOptions options) 
			: base(db, alignment, new DuplicateOptions(options) { DuplicateDownstream = true }) { PredefinedType = alignment.PredefinedType; }
		public IfcAlignment(IfcProject host) : base(host) { }
		public IfcAlignment(IfcAlignment host) : base(host) { }
		public IfcAlignment(IfcSite host) : base(host) { }
		public IfcAlignment(IfcFacility host) : base(host) { }
		public IfcAlignment(IfcFacilityPart host) : base(host) { }
		[Obsolete("DEPRECATED IFC4X3", false)]
		protected IfcAlignment(IfcObjectPlacement placement, IfcAlignmentHorizontal horizontal, IfcAlignmentVertical vertical, IfcAlignmentCant cant, IfcSpatialStructureElement host, IfcCurve axis)
			:base(host, axis)
		{
			ObjectPlacement = placement;
			if (horizontal != null)
				AddAggregated(horizontal);
			if (vertical != null)
				AddAggregated(vertical);
			if (cant != null)
				AddAggregated(cant);
		}
		[Obsolete("DEPRECATED IFC4X3", false)]
		public IfcAlignment(IfcFacility host, IfcCurve axis) : base(host, axis) { }
		[Obsolete("DEPRECATED IFC4X3", false)]
		public IfcAlignment(IfcSite host, IfcCurve axis) : base(host, axis) { }
		[Obsolete("DEPRECATED IFC4X3", false)]
		public IfcAlignment(IfcFacility host, IfcObjectPlacement placement, IfcAlignmentHorizontal horizontal, IfcAlignmentVertical vertical, IfcAlignmentCant cant, IfcCurve axis) 
			: this(placement, horizontal, vertical, cant, host, axis) { }
		[Obsolete("DEPRECATED IFC4X3", false)]
		public IfcAlignment(IfcFacilityPart host, IfcObjectPlacement placement, IfcAlignmentHorizontal horizontal, IfcAlignmentVertical vertical, IfcAlignmentCant cant, IfcCurve axis) 
			: this(placement, horizontal, vertical, cant, host, axis) { }
		[Obsolete("DEPRECATED IFC4X3", false)]
		public IfcAlignment(IfcSite host, IfcObjectPlacement placement, IfcAlignmentHorizontal horizontal, IfcAlignmentVertical vertical, IfcAlignmentCant cant, IfcCurve axis)
			: this(placement, horizontal, vertical, cant, host, axis) { }

		internal override bool isDuplicate(BaseClassIfc e, OptionsTestDuplicate options)
		{
			IfcAlignment alignment = e as IfcAlignment;
			if (alignment == null)
				return false;
			if (PredefinedType != alignment.PredefinedType)
				return false;
			return base.isDuplicate(e, options);
		}
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DCant : IfcAxisLateralInclination
	{
		private LIST<IfcAlignment2DCantSegment> mSegments = new LIST<IfcAlignment2DCantSegment>(); //: LIST[1:?] OF IfcAlignment2DCantSegment;
		private double mRailHeadDistance = 0; //: IfcPositiveLengthMeasure;

		public LIST<IfcAlignment2DCantSegment> Segments { get { return mSegments; } set { mSegments = value; } }
		public double RailHeadDistance { get { return mRailHeadDistance; } set { mRailHeadDistance = value; } }

		public IfcAlignment2DCant() : base() { }
		internal IfcAlignment2DCant(DatabaseIfc db, IfcAlignment2DCant alignment2dCant, DuplicateOptions options) : base(db, alignment2dCant, options)
		{
			Segments.AddRange(alignment2dCant.Segments.Select(x => db.Factory.Duplicate(x) as IfcAlignment2DCantSegment));
			RailHeadDistance = alignment2dCant.RailHeadDistance;
		}
		public IfcAlignment2DCant(DatabaseIfc db, IEnumerable<IfcAlignment2DCantSegment> segments, double railHeadDistance)
			: base(db)
		{
			Segments.AddRange(segments);
			RailHeadDistance = railHeadDistance;
		}
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DCantSegLine : IfcAlignment2DCantSegment
	{
		public IfcAlignment2DCantSegLine() : base() { }
		internal IfcAlignment2DCantSegLine(DatabaseIfc db, IfcAlignment2DCantSegLine alignment2DCantSegLine, DuplicateOptions options) : base(db, alignment2DCantSegLine, options) { }
		public IfcAlignment2DCantSegLine(DatabaseIfc db, double startDistAlong, double horizontalLength, double startCantLeft, double startCantRight)
			: base(db, startDistAlong, horizontalLength, startCantLeft, startCantRight) { }
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public abstract partial class IfcAlignment2DCantSegment : IfcAlignment2DSegment
	{
		private double mStartDistAlong = 0; //: IfcPositiveLengthMeasure;
		private double mHorizontalLength = 0; //: IfcPositiveLengthMeasure;
		private double mStartCantLeft = 0; //: IfcLengthMeasure;
		private double mStartCantRight = 0; //: IfcLengthMeasure;
												   //INVERSE
		private SET<IfcAlignment2DCant> mToCant = new SET<IfcAlignment2DCant>();

		public double StartDistAlong { get { return mStartDistAlong; } set { mStartDistAlong = value; } }
		public double HorizontalLength { get { return mHorizontalLength; } set { mHorizontalLength = value; } }
		public double StartCantLeft { get { return mStartCantLeft; } set { mStartCantLeft = value; } }
		public double StartCantRight { get { return mStartCantRight; } set { mStartCantRight = value; } }
		//INVERSE
		public SET<IfcAlignment2DCant> ToCant { get { return mToCant; } }

		protected IfcAlignment2DCantSegment() : base() { }
		protected IfcAlignment2DCantSegment(DatabaseIfc db, IfcAlignment2DCantSegment alignment2DCantSegment, DuplicateOptions options) : base(db, alignment2DCantSegment, options)
		{
			StartDistAlong = alignment2DCantSegment.StartDistAlong;
			HorizontalLength = alignment2DCantSegment.HorizontalLength;
			StartCantLeft = alignment2DCantSegment.StartCantLeft;
			StartCantLeft = alignment2DCantSegment.StartCantLeft;
		}
		protected IfcAlignment2DCantSegment(DatabaseIfc db, double startDistAlong, double horizontalLength, double startCantLeft, double startCantRight)
			: base(db)
		{
			StartDistAlong = startDistAlong;
			HorizontalLength = horizontalLength;
			StartCantLeft = startCantLeft;
			StartCantRight = startCantRight;
		}
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DCantSegConstant : IfcAlignment2DCantSegment
	{
		public IfcAlignment2DCantSegConstant() : base() { }
		internal IfcAlignment2DCantSegConstant(DatabaseIfc db, IfcAlignment2DCantSegTransition alignment2DCantSegTransition, DuplicateOptions options) 
			: base(db, alignment2DCantSegTransition, options) { }
		public IfcAlignment2DCantSegConstant(DatabaseIfc db, double startDistAlong, double horizontalLength, double startCantLeft, double startCantRight, bool isStartRadiusCCW, bool isEndRadiusCCW, IfcTransitionCurveType transitionCurveType, double endCantLeft, double endCantRight)
			: base(db, startDistAlong, horizontalLength, startCantLeft, startCantRight) { }
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DCantSegTransition : IfcAlignment2DCantSegment
	{
		private double mEndCantLeft = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mEndCantRight = double.NaN; //: OPTIONAL IfcLengthMeasure;

		public double EndCantLeft { get { return mEndCantLeft; } set { mEndCantLeft = value; } }
		public double EndCantRight { get { return mEndCantRight; } set { mEndCantRight = value; } }

		public IfcAlignment2DCantSegTransition() : base() { }
		internal IfcAlignment2DCantSegTransition(DatabaseIfc db, IfcAlignment2DCantSegTransition alignment2DCantSegTransition, DuplicateOptions options) : base(db, alignment2DCantSegTransition, options)
		{
			EndCantLeft = alignment2DCantSegTransition.EndCantLeft;
			EndCantRight = alignment2DCantSegTransition.EndCantRight;
		}
		public IfcAlignment2DCantSegTransition(DatabaseIfc db, double startDistAlong, double horizontalLength, double startCantLeft, double startCantRight, double endCantLeft, double endCantRight)
			: base(db, startDistAlong, horizontalLength, startCantLeft, startCantRight)
		{
			EndCantLeft = endCantLeft;
			EndCantRight = endCantRight;
		}
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DCantSegTransitionNonLinear : IfcAlignment2DCantSegTransition
	{
		private double mStartRadius = double.NaN; //: OPTIONAL IfcPositiveLengthMeasure;
		private double mEndRadius = double.NaN; //: OPTIONAL IfcPositiveLengthMeasure;
		private bool mIsStartRadiusCCW = false; //: IfcBoolean;
		private bool mIsEndRadiusCCW = false; //: IfcBoolean;
		private IfcTransitionCurveType mTransitionCurveType = IfcTransitionCurveType.BIQUADRATICPARABOLA; //: IfcTransitionCurveType;

		public double StartRadius { get { return mStartRadius; } set { mStartRadius = value; } }
		public double EndRadius { get { return mEndRadius; } set { mEndRadius = value; } }
		public bool IsStartRadiusCCW { get { return mIsStartRadiusCCW; } set { mIsStartRadiusCCW = value; } }
		public bool IsEndRadiusCCW { get { return mIsEndRadiusCCW; } set { mIsEndRadiusCCW = value; } }
		public IfcTransitionCurveType TransitionCurveType { get { return mTransitionCurveType; } set { mTransitionCurveType = value; } }

		public IfcAlignment2DCantSegTransitionNonLinear() : base() { }
		internal IfcAlignment2DCantSegTransitionNonLinear(DatabaseIfc db, IfcAlignment2DCantSegTransitionNonLinear alignment2DCantSegTransition, DuplicateOptions options) : base(db, alignment2DCantSegTransition, options)
		{
			StartRadius = alignment2DCantSegTransition.StartRadius;
			EndRadius = alignment2DCantSegTransition.EndRadius;
			IsStartRadiusCCW = alignment2DCantSegTransition.IsStartRadiusCCW;
			IsEndRadiusCCW = alignment2DCantSegTransition.IsEndRadiusCCW;
			TransitionCurveType = alignment2DCantSegTransition.TransitionCurveType;
		}
		public IfcAlignment2DCantSegTransitionNonLinear(DatabaseIfc db, double startDistAlong, double horizontalLength, double startCantLeft, double startCantRight, double endCantLeft, double endCantRight, bool isStartRadiusCCW, bool isEndRadiusCCW, IfcTransitionCurveType transitionCurveType)
			: base(db, startDistAlong, horizontalLength, startCantLeft, startCantRight, endCantLeft, endCantRight)
		{
			IsStartRadiusCCW = isStartRadiusCCW;
			IsEndRadiusCCW = isEndRadiusCCW;
			TransitionCurveType = transitionCurveType;

		}
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DVerSegTransition : IfcAlignment2DVerticalSegment
	{
		private double mStartRadius = double.NaN; //: OPTIONAL IfcPositiveLengthMeasure;
		private double mEndRadius = double.NaN; //: OPTIONAL IfcPositiveLengthMeasure;
		private bool mIsStartRadiusCCW = false; //: IfcBoolean;
		private bool mIsEndRadiusCCW = false; //: IfcBoolean;
		private IfcTransitionCurveType mTransitionCurveType = IfcTransitionCurveType.BIQUADRATICPARABOLA; //: IfcTransitionCurveType;

		public double StartRadius { get { return mStartRadius; } set { mStartRadius = value; } }
		public double EndRadius { get { return mEndRadius; } set { mEndRadius = value; } }
		public bool IsStartRadiusCCW { get { return mIsStartRadiusCCW; } set { mIsStartRadiusCCW = value; } }
		public bool IsEndRadiusCCW { get { return mIsEndRadiusCCW; } set { mIsEndRadiusCCW = value; } }
		public IfcTransitionCurveType TransitionCurveType { get { return mTransitionCurveType; } set { mTransitionCurveType = value; } }

		public IfcAlignment2DVerSegTransition() : base() { }
		internal IfcAlignment2DVerSegTransition(DatabaseIfc db, IfcAlignment2DVerSegTransition alignment2DVerSegTransition, DuplicateOptions options) : base(db, alignment2DVerSegTransition, options)
		{
			StartRadius = alignment2DVerSegTransition.StartRadius;
			EndRadius = alignment2DVerSegTransition.EndRadius;
			IsStartRadiusCCW = alignment2DVerSegTransition.IsStartRadiusCCW;
			IsEndRadiusCCW = alignment2DVerSegTransition.IsEndRadiusCCW;
			TransitionCurveType = alignment2DVerSegTransition.TransitionCurveType;
		}
		public IfcAlignment2DVerSegTransition(DatabaseIfc db, double startDistAlong, double horizontalLength, double startHeight, double startGradient, bool isStartRadiusCCW, bool isEndRadiusCCW, IfcTransitionCurveType transitionCurveType)
			: base(db, startDistAlong, horizontalLength, startHeight, startGradient)
		{
			IsStartRadiusCCW = isStartRadiusCCW;
			IsEndRadiusCCW = isEndRadiusCCW;
			TransitionCurveType = transitionCurveType;
		}
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DHorizontal : IfcGeometricRepresentationItem //IFC4.1
	{
		internal double mStartDistAlong = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal LIST<IfcAlignment2DHorizontalSegment> mSegments = new LIST<IfcAlignment2DHorizontalSegment>();// : LIST [1:?] OF IfcAlignment2DHorizontalSegment;
	   //INVERSE
		internal SET<IfcAlignmentCurve> mToAlignmentCurve = new SET<IfcAlignmentCurve>();// : SET[1:?] OF IfcAlignmentCurve FOR Horizontal;

		public double StartDistAlong { get { return double.IsNaN(mStartDistAlong) ? 0 : mStartDistAlong; } set { mStartDistAlong = value; } }
		public LIST<IfcAlignment2DHorizontalSegment> Segments { get { return mSegments; } }
		public SET<IfcAlignmentCurve> ToAlignmentCurve { get { return mToAlignmentCurve; } } 

		internal IfcAlignment2DHorizontal() : base() { }
		internal IfcAlignment2DHorizontal(DatabaseIfc db, IfcAlignment2DHorizontal a, DuplicateOptions options) : base(db, a, options)
		{
			mStartDistAlong = a.mStartDistAlong;
			Segments.AddRange(a.Segments.ConvertAll(x => db.Factory.Duplicate(x) as IfcAlignment2DHorizontalSegment));
		}
		public IfcAlignment2DHorizontal(IEnumerable<IfcAlignment2DHorizontalSegment> segments) : base(segments.First().Database) { mSegments.AddRange(segments); }
		protected override void initialize()
		{
			base.initialize();
			mSegments.CollectionChanged += mSegments_CollectionChanged;
		}
		private void mSegments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcAlignment2DHorizontalSegment segment in e.NewItems)
					segment.ToHorizontal = this;
			}
			if (e.OldItems != null)
			{
				foreach (IfcAlignment2DHorizontalSegment segment in e.OldItems)
					segment.ToHorizontal = null;
			}
		}
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DHorizontalSegment : IfcAlignment2DSegment //IFC4.1
	{
		private IfcCurveSegment2D mCurveGeometry;// : IfcCurveSegment2D;
		//INVERSE
		internal IfcAlignment2DHorizontal mToHorizontal = null;

		public IfcCurveSegment2D CurveGeometry { get { return mCurveGeometry; } set { if (mCurveGeometry != null) mCurveGeometry.mToSegment = null; mCurveGeometry = value; if (value != null) mCurveGeometry.mToSegment = this; } }
		public IfcAlignment2DHorizontal ToHorizontal { get { return mToHorizontal; } set { mToHorizontal = value; } }

		internal IfcAlignment2DHorizontalSegment() : base() { }
		internal IfcAlignment2DHorizontalSegment(DatabaseIfc db, IfcAlignment2DHorizontalSegment s, DuplicateOptions options) : base(db, s, options) { CurveGeometry = db.Factory.Duplicate(s.CurveGeometry) as IfcCurveSegment2D; }
		public IfcAlignment2DHorizontalSegment(IfcCurveSegment2D seg) : base(seg.mDatabase) { CurveGeometry = seg; }
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public abstract partial class IfcAlignment2DSegment : IfcGeometricRepresentationItem //IFC4.1 ABSTRACT SUPERTYPE OF(ONEOF(IfcAlignment2DHorizontalSegment, IfcAlignment2DVerticalSegment))
	{
		internal IfcLogicalEnum mTangentialContinuity = IfcLogicalEnum.UNKNOWN;// : OPTIONAL IfcBoolean;
		private string mStartTag = "";// : OPTIONAL IfcLabel;
		private string mEndTag = "";// : OPTIONAL IfcLabel;

		public bool TangentialContinuity { get { return mTangentialContinuity == IfcLogicalEnum.TRUE; } set { mTangentialContinuity = (value ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); } }
		public string StartTag { get { return mStartTag; } set { mStartTag = value; } }
		public string EndTag { get { return mEndTag; } set { mEndTag = value; } }

		protected IfcAlignment2DSegment() : base() { }
		protected IfcAlignment2DSegment(DatabaseIfc db) : base(db) { }
		protected IfcAlignment2DSegment(DatabaseIfc db, IfcAlignment2DSegment s, DuplicateOptions options) : base(db, s, options)
		{
			mTangentialContinuity = s.mTangentialContinuity;
			StartTag = s.StartTag;
			EndTag = s.EndTag;
		}
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DVerSegCircularArc : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		private bool mIsConvex;// : IfcBoolean;

		public double Radius { get { return mRadius; } set { mRadius = value; } }
		public bool IsConvex { get { return mIsConvex; } set { mIsConvex = value; } }

		internal IfcAlignment2DVerSegCircularArc() : base() { }
		internal IfcAlignment2DVerSegCircularArc(DatabaseIfc db, IfcAlignment2DVerSegCircularArc alignment2DVerSegCircularArc, DuplicateOptions options) : base(db, alignment2DVerSegCircularArc, options)
		{
			Radius = alignment2DVerSegCircularArc.Radius;
			IsConvex = alignment2DVerSegCircularArc.IsConvex;
		}
		public IfcAlignment2DVerSegCircularArc(DatabaseIfc db, double startDist, double horizontalLength, double startHeight, double startGradient, double radius, bool isConvex)
			: base(db, startDist, horizontalLength, startHeight, startGradient)
		{
			mRadius = radius;
			mIsConvex = isConvex;
		}
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DVerSegLine : IfcAlignment2DVerticalSegment 
	{
		internal IfcAlignment2DVerSegLine() : base() { }
		internal IfcAlignment2DVerSegLine(DatabaseIfc db, IfcAlignment2DVerSegLine alignment2DVerSegLine, DuplicateOptions options) : base(db, alignment2DVerSegLine, options) { }
		public IfcAlignment2DVerSegLine(DatabaseIfc db, double startDist, double horizontalLength, double startHeight, double startGradient)
			: base(db, startDist, horizontalLength, startHeight, startGradient) { }
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DVerSegParabolicArc : IfcAlignment2DVerticalSegment  //IFC4.1
	{
		private double mParabolaConstant;// : IfcPositiveLengthMeasure;
		private bool mIsConvex;// : IfcBoolean;

		public double ParabolaConstant { get { return mParabolaConstant; } set { mParabolaConstant = value; } }
		public bool IsConvex { get { return mIsConvex; } set { mIsConvex = value; } }

		internal IfcAlignment2DVerSegParabolicArc() : base() { }
		internal IfcAlignment2DVerSegParabolicArc(DatabaseIfc db, IfcAlignment2DVerSegParabolicArc alignment2DVerSegParabolicArc, DuplicateOptions options) : base(db, alignment2DVerSegParabolicArc, options)
		{
			ParabolaConstant = alignment2DVerSegParabolicArc.ParabolaConstant;
			IsConvex = alignment2DVerSegParabolicArc.IsConvex;
		}
		public IfcAlignment2DVerSegParabolicArc(DatabaseIfc db, double startDist, double horizontalLength, double startHeight, double startGradient, double parabolaConstant, bool isConvex)
			: base(db, startDist, horizontalLength, startHeight, startGradient)
		{
			mParabolaConstant = parabolaConstant;
			mIsConvex = isConvex;
		}
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignment2DVertical : IfcGeometricRepresentationItem 
	{
		internal LIST<IfcAlignment2DVerticalSegment> mSegments = new LIST<IfcAlignment2DVerticalSegment>();// : LIST [1:?] OF IfcAlignment2DVerticalSegment;
		//INVERSE
		internal IfcAlignmentCurve mToAlignmentCurve = null;// : SET[1:1] OF IfcAlignmentCurve FOR Vertical;
		public LIST<IfcAlignment2DVerticalSegment> Segments { get { return mSegments; } }
		public IfcAlignmentCurve ToAlignmentCurve { get { return mToAlignmentCurve; } set { mToAlignmentCurve = value; } }// : SET[1:1] OF IfcAlignmentCurve FOR Vertical;
	
		internal IfcAlignment2DVertical() : base() { }
		internal IfcAlignment2DVertical(DatabaseIfc db, IfcAlignment2DVertical a, DuplicateOptions options) : base(db, a, options)
		{
			Segments.AddRange(a.Segments.ConvertAll(x => db.Factory.Duplicate(x) as IfcAlignment2DVerticalSegment));
		}
		public IfcAlignment2DVertical(IEnumerable<IfcAlignment2DVerticalSegment> segments) : base(segments.First().Database) { Segments.AddRange(segments); }
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public abstract partial class IfcAlignment2DVerticalSegment : IfcAlignment2DSegment 
	{
		internal double mStartDistAlong;// : IfcLengthMeasure;
		internal double mHorizontalLength;// : IfcPositiveLengthMeasure;
		internal double mStartHeight;// : IfcLengthMeasure;
		internal double mStartGradient;// : IfcRatioMeasure;

		public double StartDistAlong { get { return mStartDistAlong; } set { mStartDistAlong = value; } }
		public double HorizontalLength { get { return mHorizontalLength; } set { mHorizontalLength = value; } }
		public double StartHeight { get { return mStartHeight; } set { mStartHeight = value; } }
		public double StartGradient { get { return mStartGradient; } set { mStartGradient = value; } }

		protected IfcAlignment2DVerticalSegment() : base() { }
		protected IfcAlignment2DVerticalSegment(DatabaseIfc db, IfcAlignment2DVerticalSegment alignment2DVerticalSegment, DuplicateOptions options) : base(db, alignment2DVerticalSegment, options)
		{
			StartDistAlong = alignment2DVerticalSegment.StartDistAlong;
			HorizontalLength = alignment2DVerticalSegment.HorizontalLength;
			StartHeight = alignment2DVerticalSegment.StartHeight;
			StartGradient = alignment2DVerticalSegment.StartGradient;
		}
		protected IfcAlignment2DVerticalSegment(DatabaseIfc db, double startDist, double horizontalLength, double startHeight, double startGradient) : base(db)
		{
			mStartDistAlong = startDist;
			mHorizontalLength = horizontalLength;
			mStartHeight = startHeight;
			mStartGradient = startGradient;
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcAlignmentCant : IfcLinearElement
	{
		private double mRailHeadDistance = 0; //: IfcPositiveLengthMeasure;
		[Obsolete("Interim during IFC4x3RC2", false)]
		private LIST<IfcAlignmentCantSegment> mCantSegments = new LIST<IfcAlignmentCantSegment>(); //: LIST [1:?] OF IfcAlignmentCantSegment; 

		public double RailHeadDistance { get { return mRailHeadDistance; } set { mRailHeadDistance = value; } }
		public IEnumerable<IfcAlignmentCantSegment> CantSegments
		{
			get
			{
				if (mCantSegments.Count > 0)
					return mCantSegments;
				return IsNestedBy.SelectMany(x => x.RelatedObjects).OfType<IfcAlignmentSegment>().Select(x => x.DesignParameters as IfcAlignmentCantSegment);
			}
		}

		public IfcAlignmentCant() : base() { }
		internal IfcAlignmentCant(DatabaseIfc db, IfcAlignmentCant alignmentCant, DuplicateOptions options)
		: base(db, alignmentCant, options)
		{
			RailHeadDistance = alignmentCant.RailHeadDistance;
			mCantSegments.AddRange(alignmentCant.mCantSegments.Select(x => db.Factory.Duplicate(x, options) as IfcAlignmentCantSegment));
		}
		public IfcAlignmentCant(DatabaseIfc db, double railHeadDistance) : base(db) { RailHeadDistance = railHeadDistance; }
		public IfcAlignmentCant(IfcAlignment alignment, double railHeadDistance)
			: base(alignment.Database)
		{
			alignment.AddNested(this);
			ObjectPlacement = alignment.ObjectPlacement;
			RailHeadDistance = railHeadDistance;
		}
		public IfcSegmentedReferenceCurve ComputeCantGeometry(IfcAlignment alignment, IfcGradientCurve gradientCurve)
		{
			List<IfcCurveSegment> curveSegments = CantSegments.Select(x => x.generateCurveSegment(RailHeadDistance)).ToList();
			IfcSegmentedReferenceCurve segmentedReferenceCurve = new IfcSegmentedReferenceCurve(gradientCurve, curveSegments);
			segmentedReferenceCurve.EndPoint = CantSegments.Last().ComputeEndPlacement(RailHeadDistance);
			if (alignment != null)
			{
				var context = mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis);
				if (alignment.Representation == null)
				{
					IfcShapeRepresentation shapeRepresentation = new IfcShapeRepresentation(context, segmentedReferenceCurve, ShapeRepresentationType.Curve3D);
					alignment.Representation = new IfcProductDefinitionShape(shapeRepresentation);
				}
				else
				{
					IfcShapeRepresentation axisRepresentation = alignment.Representation.Representations.OfType<IfcShapeRepresentation>().
						Where(x => string.Compare(x.RepresentationIdentifier, context.ContextIdentifier, true) == 0).FirstOrDefault();

					if (axisRepresentation == null)
					{
						axisRepresentation = new IfcShapeRepresentation(context, segmentedReferenceCurve, ShapeRepresentationType.Curve3D);
						alignment.Representation.Representations.Add(axisRepresentation);
					}
					else
					{
						axisRepresentation.Items.Clear();
						axisRepresentation.Items.Add(segmentedReferenceCurve);
					}
				}
			}
			return segmentedReferenceCurve;
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcAlignmentCantSegment : IfcAlignmentParameterSegment
	{
		private double mStartDistAlong = 0; //: IfcLengthMeasure;
		private double mHorizontalLength = 0; //: IfcNonNegativeLengthMeasure;
		private double mStartCantLeft = 0; //: IfcLengthMeasure;
		private double mEndCantLeft = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private double mStartCantRight = 0; //: IfcLengthMeasure;
		private double mEndCantRight = double.NaN; //: OPTIONAL IfcLengthMeasure;
		private IfcAlignmentCantSegmentTypeEnum mPredefinedType; //: IfcAlignmentCantSegmentTypeEnum;

		public double StartDistAlong { get { return mStartDistAlong; } set { mStartDistAlong = value; } }
		public double HorizontalLength { get { return mHorizontalLength; } set { mHorizontalLength = value; } }
		public double StartCantLeft { get { return mStartCantLeft; } set { mStartCantLeft = value; } }
		public double EndCantLeft { get { return mEndCantLeft; } set { mEndCantLeft = value; } }
		public double StartCantRight { get { return mStartCantRight; } set { mStartCantRight = value; } }
		public double EndCantRight { get { return mEndCantRight; } set { mEndCantRight = value; } }
		public IfcAlignmentCantSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcAlignmentCantSegment() : base() { }
		internal IfcAlignmentCantSegment(DatabaseIfc db, IfcAlignmentCantSegment alignmentCantSegment, DuplicateOptions options)
			: base(db, alignmentCantSegment, options)
		{
			StartDistAlong = alignmentCantSegment.StartDistAlong;
			HorizontalLength = alignmentCantSegment.HorizontalLength;
			StartCantLeft = alignmentCantSegment.StartCantLeft;
			EndCantLeft = alignmentCantSegment.EndCantLeft;
			StartCantRight = alignmentCantSegment.StartCantRight;
			EndCantRight = alignmentCantSegment.EndCantRight;
			PredefinedType = alignmentCantSegment.PredefinedType;
		}
		public IfcAlignmentCantSegment(DatabaseIfc db, double startDistAlong, double horizontalLength, double startCantLeft, double startCantRight)
			: base(db)
		{
			StartDistAlong = startDistAlong;
			HorizontalLength = horizontalLength;
			StartCantLeft = startCantLeft;
			StartCantRight = startCantRight;
			PredefinedType = IfcAlignmentCantSegmentTypeEnum.CONSTANTCANT;
		}
		public IfcAlignmentCantSegment(DatabaseIfc db, double startDistAlong, double horizontalLength, double startCantLeft, double startCantRight,
			double endCantLeft, double endCantRight, IfcAlignmentCantSegmentTypeEnum predefinedType)
			: this(db, startDistAlong, horizontalLength, startCantLeft, startCantRight)
		{
			EndCantLeft = endCantLeft;
			EndCantRight = endCantRight;
			PredefinedType = predefinedType;
		}

		public IfcCurveSegment generateCurveSegment(double railHeadDistance)
		{
			if(PredefinedType != IfcAlignmentCantSegmentTypeEnum.LINEARTRANSITION && PredefinedType != IfcAlignmentCantSegmentTypeEnum.CONSTANTCANT)
				throw new NotImplementedException("XX Not Implemented cant segment " + PredefinedType.ToString());
			DatabaseIfc db = mDatabase;
			double tol = db.Tolerance;
			double startCantLeft = double.IsNaN(StartCantLeft) ? 0 : StartCantLeft, startCantRight = double.IsNaN(StartCantRight) ? 0 : StartCantRight;

			double deltaStartCant = startCantLeft - startCantRight;
			double theta = Math.Asin(deltaStartCant / railHeadDistance);
			double zOffset = StartCantLeft - (deltaStartCant / 2);
			double segmentLength = HorizontalLength;
			IfcCartesianPoint startPoint = new IfcCartesianPoint(db, StartDistAlong, 0, zOffset);
			IfcAxis2Placement3D segmentPlacement = new IfcAxis2Placement3D(startPoint);
			IfcCurve parentCurve = null;
			if(PredefinedType == IfcAlignmentCantSegmentTypeEnum.CONSTANTCANT)
			{
				parentCurve = db.Factory.LineX2d;
				segmentPlacement.Axis = (Math.Abs(deltaStartCant) > tol ? new IfcDirection(db, 0, Math.Cos(theta), Math.Sin(theta)) : db.Factory.YAxis);
			}
			else if(PredefinedType == IfcAlignmentCantSegmentTypeEnum.LINEARTRANSITION)
			{ 
				double endCantLeft = double.IsNaN(EndCantLeft) ? 0 : EndCantLeft, endCantRight = double.IsNaN(EndCantRight) ? 0 : EndCantRight;
				double deltaEndCant = endCantLeft - endCantRight;
				double endZOffset = endCantLeft - (deltaEndCant / 2);
				double deltaZ = endZOffset - zOffset;
				segmentLength = Math.Sqrt(deltaZ * deltaZ + HorizontalLength * HorizontalLength);
				double refDirectionX = HorizontalLength / segmentLength, refDirectionZ = deltaZ / HorizontalLength;
				double cosTheta = 1, sinTheta = 0;
				if (Math.Abs(deltaStartCant) > tol)
				{
					cosTheta = Math.Cos(theta);
					sinTheta = Math.Sin(theta);
				}
				segmentPlacement.Axis = new IfcDirection(db, cosTheta * refDirectionZ, sinTheta, cosTheta * refDirectionX);
				segmentPlacement.RefDirection = new IfcDirection(db, refDirectionX, 0, refDirectionZ);

				double factor = ((endCantLeft + endCantRight) - (startCantLeft + startCantRight)) / 2.0;
				double linearTerm = factor;

				double clothoidConstant = HorizontalLength * Math.Pow(Math.Abs(linearTerm), -0.5) * linearTerm / Math.Abs(linearTerm);
				parentCurve = new IfcClothoid(db.Factory.Origin2dPlace, clothoidConstant);
			}

			IfcCurveMeasureSelect start = new IfcLengthMeasure(0), length = new IfcLengthMeasure(segmentLength);
			bool useNonNegativeLengthMeasures = db.Release < ReleaseVersion.IFC4X3_ADD2;
			if (useNonNegativeLengthMeasures)
			{
				start = new IfcNonNegativeLengthMeasure(0);
				length = new IfcNonNegativeLengthMeasure(segmentLength);
			}

			IfcTransitionCode transitionCode = HorizontalLength < tol ? IfcTransitionCode.DISCONTINUOUS : IfcTransitionCode.CONTINUOUS;
			IfcCurveSegment curveSegment = new IfcCurveSegment(transitionCode, segmentPlacement, start, length, parentCurve);
			if(mDesignerOf != null)
				mDesignerOf.Representation = new IfcProductDefinitionShape(new IfcShapeRepresentation(mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis), curveSegment, ShapeRepresentationType.Segment));
			return curveSegment;
		}
		public IfcPlacement ComputeEndPlacement(double railHeadDistance)
		{
			if (PredefinedType != IfcAlignmentCantSegmentTypeEnum.LINEARTRANSITION && PredefinedType != IfcAlignmentCantSegmentTypeEnum.CONSTANTCANT)
				throw new NotImplementedException("XX Not Implemented cant segment " + PredefinedType.ToString());
			DatabaseIfc db = mDatabase;
			double tol = db.Tolerance;
			double dz = EndCantLeft - EndCantRight;
			double theta = Math.Asin(dz / railHeadDistance);
			double yOffset = EndCantLeft - dz /2;
			IfcCartesianPoint endPoint = new IfcCartesianPoint(db, StartDistAlong + HorizontalLength, yOffset, 0);
			IfcAxis2Placement3D result = new IfcAxis2Placement3D(endPoint);
			result.Axis = Math.Abs(dz) > tol ? new IfcDirection(db, 0, Math.Cos(theta), Math.Sin(theta)) : db.Factory.YAxis;
			//todo calculate end tangent
			return result;
		}
	}
	[Serializable, Obsolete("DEPRECATED IFC4X3", false), VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcAlignmentCurve : IfcBoundedCurve //IFC4.1
	{
		internal IfcAlignment2DHorizontal mHorizontal = null;// : OPTIONAL IfcAlignment2DHorizontal;
		private IfcAlignment2DVertical mVertical = null;// : OPTIONAL IfcAlignment2DVertical;
		internal string mTag = "";// : OPTIONAL IfcLabel;

		public IfcAlignment2DHorizontal Horizontal { get { return mHorizontal; } set { mHorizontal = value; } }
		public IfcAlignment2DVertical Vertical { get { return mVertical; } set { if (mVertical != null) mVertical.ToAlignmentCurve = null; mVertical = value; if (value != null) value.ToAlignmentCurve = this; } }
		public string Tag { get { return mTag; } set { mTag = value; } }

		internal IfcAlignmentCurve() : base() { }
		internal IfcAlignmentCurve(DatabaseIfc db, IfcAlignmentCurve c, DuplicateOptions options) : base(db, c, options)
		{
			if (c.mHorizontal != null)
				Horizontal = db.Factory.Duplicate(c.Horizontal) as IfcAlignment2DHorizontal;
			if (c.mVertical != null)
				Vertical = db.Factory.Duplicate(c.Vertical) as IfcAlignment2DVertical;
			Tag = c.Tag;
		}
		public IfcAlignmentCurve(DatabaseIfc db) : base(db) { }
		public IfcAlignmentCurve(IfcAlignment2DHorizontal horizontal) : base(horizontal.Database) { Horizontal = horizontal; }
		public IfcAlignmentCurve(IfcAlignment2DVertical vertical) : base(vertical.Database) { Vertical = vertical; }
		public IfcAlignmentCurve(IfcAlignment2DHorizontal horizontal, IfcAlignment2DVertical vertical) : this(horizontal) { Vertical = vertical; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcAlignmentHorizontal : IfcLinearElement
	{
		[Obsolete("Removed IFC4x3RC4", false)]
		private double mStartDistAlong = double.NaN; //: OPTIONAL IfcLengthMeasure;
		[Obsolete("Interim during IFC4x3RC2", false)]
		private LIST<IfcAlignmentHorizontalSegment> mHorizontalSegments = new LIST<IfcAlignmentHorizontalSegment>(); //: LIST [1:?] OF IfcAlignmentHorizontalSegment; 
	
		[Obsolete("DEPRECATED IFC4X3", false)]
		public double StartDistAlong { get { return mStartDistAlong; } set { mStartDistAlong = value; } }
		public IEnumerable<IfcAlignmentHorizontalSegment> HorizontalSegments
		{
			get
			{
				if (mHorizontalSegments.Count > 0)
					return mHorizontalSegments;
				return IsNestedBy.SelectMany(x => x.RelatedObjects).OfType<IfcAlignmentSegment>().Select(x=>x.DesignParameters as IfcAlignmentHorizontalSegment);
			}
		}

		public IfcAlignmentHorizontal() : base() { }
		public IfcAlignmentHorizontal(DatabaseIfc db) : base(db) { }
		internal IfcAlignmentHorizontal(DatabaseIfc db, IfcAlignmentHorizontal alignmentHorizontal, DuplicateOptions options)
			: base(db, alignmentHorizontal, options) 
		{
			StartDistAlong = alignmentHorizontal.StartDistAlong;
			if(alignmentHorizontal.mHorizontalSegments.Count > 0)
				mHorizontalSegments.AddRange(alignmentHorizontal.mHorizontalSegments.Select(x => db.Factory.Duplicate(x, options) as IfcAlignmentHorizontalSegment));
		}
		public IfcAlignmentHorizontal(IfcAlignment alignment) 
			: base(alignment.Database) 
		{
			alignment.AddNested(this);
			ObjectPlacement = alignment.ObjectPlacement;
		}
		public IfcCompositeCurve ComputeHorizontalGeometry(IfcAlignment alignment)
		{
			List<IfcCurveSegment> curveSegments = new List<IfcCurveSegment>();
			List<IfcAlignmentHorizontalSegment> horizontalSegments = HorizontalSegments.ToList();
			if (horizontalSegments.Count == 0)
				return null;
			int segmentCount = horizontalSegments.Count;
			for (int counter = 0; counter < segmentCount; counter++)
			{
				try
				{
					IfcAlignmentHorizontalSegment nextSegment = counter + 1 < segmentCount ? horizontalSegments[counter + 1] : null;
					IfcCurveSegment curveSegment = horizontalSegments[counter].generateCurveSegment(nextSegment);
					if(curveSegment != null)
						curveSegments.Add(curveSegment);
				}
				catch (Exception) { }
			}
			IfcCompositeCurve compositeCurve = new IfcCompositeCurve(curveSegments);
			var subContext = mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.FootPrint);
			IfcShapeRepresentation shapeRepresentation = new IfcShapeRepresentation(subContext, compositeCurve, ShapeRepresentationType.Curve2D);
			if (alignment != null)
			{
				if (alignment.Representation == null)
					alignment.Representation = new IfcProductDefinitionShape(shapeRepresentation);
				else
					alignment.Representation.Representations.Add(shapeRepresentation);
			}
			else
			{
				if(Representation == null)
					Representation = new IfcProductDefinitionShape(shapeRepresentation);
				else
					Representation.Representations.Add(shapeRepresentation);
			}
			return compositeCurve;
		}

		public bool IsDuplicateWorkInProgress(IfcAlignmentHorizontal horizontal, double tol)
		{
			return isDuplicate(horizontal, tol);
		}
		internal override bool isDuplicate(BaseClassIfc e, OptionsTestDuplicate options)
		{
			IfcAlignmentHorizontal horizontal = e as IfcAlignmentHorizontal;
			if (horizontal == null)
				return false;
			if (double.IsNaN(mStartDistAlong))
			{
				if (!double.IsNaN(horizontal.mStartDistAlong))
					return false;
			}
			else if (double.IsNaN(horizontal.mStartDistAlong))
				return false;
			else if (Math.Abs(mStartDistAlong - horizontal.mStartDistAlong) > options.Tolerance)
				return false;

			if (mHorizontalSegments.Count != horizontal.mHorizontalSegments.Count)
				return false;
			int count = mHorizontalSegments.Count; 
			for(int icounter = 0; icounter < count; icounter++)
			{
				IfcAlignmentHorizontalSegment segment1 = mHorizontalSegments[icounter];
				IfcAlignmentHorizontalSegment segment2 = horizontal.mHorizontalSegments[icounter];
				if (!segment1.isDuplicate(segment2, options.Tolerance))
					return false;
			}

			return base.isDuplicate(e, options);
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcAlignmentHorizontalSegment : IfcAlignmentParameterSegment
	{
		private IfcCartesianPoint mStartPoint;// : IfcCartesianPoint;
		private double mStartDirection;// : IfcPlaneAngleMeasure;
		private double mStartRadiusOfCurvature = 0; //: IfcLengthMeasure;
		private double mEndRadiusOfCurvature = 0; //: IfcLengthMeasure;
		private double mSegmentLength = 0; //: IfcNonNegativeLengthMeasure;
		private double mGravityCenterLineHeight = double.NaN; //: OPTIONAL IfcPositiveLengthMeasure;
		private IfcAlignmentHorizontalSegmentTypeEnum mPredefinedType; //: IfcAlignmentHorizontalSegmentTypeEnum;
		public IfcCartesianPoint StartPoint { get { return mStartPoint; } set { mStartPoint = value; } }
		public double StartDirection { get { return mStartDirection; } set { mStartDirection = value; } }
		public double StartRadiusOfCurvature { get { return mStartRadiusOfCurvature; } set { mStartRadiusOfCurvature = double.IsNaN(value) ? 0 : value; } }
		public double EndRadiusOfCurvature { get { return mEndRadiusOfCurvature; } set { mEndRadiusOfCurvature = double.IsNaN(value) ? 0 : value; } }
		public double SegmentLength { get { return mSegmentLength; } set { mSegmentLength = value; } }
		public double GravityCenterLineHeight { get { return mGravityCenterLineHeight; } set { mGravityCenterLineHeight = value; } }
		public IfcAlignmentHorizontalSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcAlignmentHorizontalSegment() : base() { }
		internal IfcAlignmentHorizontalSegment(DatabaseIfc db, IfcAlignmentHorizontalSegment alignmentHorizontalSegment, DuplicateOptions options)
			: base(db, alignmentHorizontalSegment, options)
		{
			StartPoint = db.Factory.Duplicate(alignmentHorizontalSegment.StartPoint, options) as IfcCartesianPoint;
			StartDirection = alignmentHorizontalSegment.StartDirection;
			StartRadiusOfCurvature = alignmentHorizontalSegment.StartRadiusOfCurvature;
			EndRadiusOfCurvature = alignmentHorizontalSegment.EndRadiusOfCurvature;
			SegmentLength = alignmentHorizontalSegment.SegmentLength;
			GravityCenterLineHeight = alignmentHorizontalSegment.GravityCenterLineHeight;
			PredefinedType = alignmentHorizontalSegment.PredefinedType;
		}
		public IfcAlignmentHorizontalSegment(IfcCartesianPoint start, double startDirection, double startRadiusOfCurvature, double endRadiusOfCurvature, double segmentLength, IfcAlignmentHorizontalSegmentTypeEnum predefinedType)
			: base(start.Database)
		{
			StartPoint = start;
			StartDirection = startDirection;
			StartRadiusOfCurvature = startRadiusOfCurvature;
			EndRadiusOfCurvature = endRadiusOfCurvature;
			SegmentLength = segmentLength;
			PredefinedType = predefinedType;
		}
		
		public Tuple<double, double> StartTangent()
		{
			double startDirection = mStartDirection * (mDatabase == null ? 1 : mDatabase.ScaleAngle());
			return new Tuple<double, double>(Math.Cos(startDirection), Math.Sin(startDirection));
		}
		internal IfcCurveSegment generateCurveSegment(IfcAlignmentHorizontalSegment nextSegment)
		{
			DatabaseIfc db = mDatabase;
			double tol = db.Tolerance;
			bool useNonNegativeLengthMeasures = db.Release < ReleaseVersion.IFC4X3_ADD2;
			Tuple<double, double> startTangent = StartTangent();
			IfcCartesianPoint startPoint = StartPoint;
			IfcAxis2Placement2D placement = new IfcAxis2Placement2D(startPoint) { RefDirection = new IfcDirection(db, startTangent.Item1, startTangent.Item2) };
			IfcCurveMeasureSelect start = new IfcLengthMeasure(0), length = new	IfcLengthMeasure(SegmentLength);
			if (useNonNegativeLengthMeasures)
			{
				start = new IfcNonNegativeLengthMeasure(0);
				length = new IfcNonNegativeLengthMeasure(SegmentLength);
			}

			IfcCurve parentCurve = null;
			if (PredefinedType == IfcAlignmentHorizontalSegmentTypeEnum.LINE)
			{
				parentCurve = db.Factory.LineX2d;
			}
			else if (PredefinedType == IfcAlignmentHorizontalSegmentTypeEnum.CIRCULARARC)
			{
				parentCurve = new IfcCircle(db, Math.Abs(StartRadiusOfCurvature));
				if (StartRadiusOfCurvature < 0)
				{
					if (useNonNegativeLengthMeasures)
					{
						if (Math.Abs(SegmentLength) < tol)
							length = new IfcLengthMeasure(0);
						else
							length = new IfcParameterValue(SegmentLength / StartRadiusOfCurvature / mDatabase.ScaleAngle());
					}
					else
						length = new IfcLengthMeasure(-SegmentLength);
				}
			}
			else if (PredefinedType == IfcAlignmentHorizontalSegmentTypeEnum.CLOTHOID)
			{
				double clothoidConstant = 0;
				if (Math.Abs(StartRadiusOfCurvature) < tol)
				{
					clothoidConstant = Math.Sqrt(Math.Abs(EndRadiusOfCurvature) * SegmentLength) * (EndRadiusOfCurvature > 0 ? 1 : -1);
				}
				else if (Math.Abs(EndRadiusOfCurvature) < tol)
				{
					clothoidConstant = Math.Sqrt(Math.Abs(StartRadiusOfCurvature) * SegmentLength) * (StartRadiusOfCurvature > 0 ? -1 : 1);
					if (useNonNegativeLengthMeasures)
					{
						double parameterValue = SegmentLength / (Math.Abs(clothoidConstant) * Math.Sqrt(Math.PI));
						start = new IfcParameterValue((clothoidConstant > 0 ? -1 : 1) * parameterValue);
						length = new IfcParameterValue((clothoidConstant > 0 ? 1 : -1) * parameterValue);
					}
					else
						start = new IfcLengthMeasure(-SegmentLength);
				}
				else if (Math.Abs(StartRadiusOfCurvature) > Math.Abs(EndRadiusOfCurvature))
				{
					double offsetLength = SegmentLength / ((StartRadiusOfCurvature / EndRadiusOfCurvature) - 1);
					if (StartRadiusOfCurvature > 0 && EndRadiusOfCurvature > 0)
					{
						clothoidConstant = Math.Sqrt(StartRadiusOfCurvature * offsetLength);
						if (useNonNegativeLengthMeasures)
							start = new IfcNonNegativeLengthMeasure(offsetLength);
						else
							start = new IfcLengthMeasure(offsetLength);
					}
					else if (StartRadiusOfCurvature < 0 && EndRadiusOfCurvature < 0)
					{
						clothoidConstant = -Math.Sqrt(-StartRadiusOfCurvature * offsetLength);
						if (useNonNegativeLengthMeasures)
							start = new IfcNonNegativeLengthMeasure(offsetLength);
						else
							start = new IfcLengthMeasure(offsetLength);
					}
					else
					{
						throw new Exception("Unhandled oppositehand transition!");
					}
				}
				else
				{
					double offsetLength = SegmentLength / ((EndRadiusOfCurvature / StartRadiusOfCurvature) - 1);
					if (StartRadiusOfCurvature > 0 && EndRadiusOfCurvature > 0)
					{
						if (StartRadiusOfCurvature < EndRadiusOfCurvature)
							clothoidConstant = -Math.Sqrt(EndRadiusOfCurvature * offsetLength);
						else
							clothoidConstant = Math.Sqrt(EndRadiusOfCurvature * offsetLength);
						if (useNonNegativeLengthMeasures)
						{
							double parameterValue = (-offsetLength - SegmentLength) / (clothoidConstant * Math.Sqrt(Math.PI));
							start = new IfcParameterValue(parameterValue);
							parameterValue = SegmentLength / (clothoidConstant * Math.Sqrt(Math.PI));
							length = new IfcParameterValue(parameterValue);
						}
						else
							start = new IfcLengthMeasure(-offsetLength - SegmentLength);
						
					}
					else if (StartRadiusOfCurvature < 0 && EndRadiusOfCurvature < 0)
					{
						if (StartRadiusOfCurvature < EndRadiusOfCurvature)
							clothoidConstant = -Math.Sqrt(-EndRadiusOfCurvature * offsetLength);
						else
							clothoidConstant = Math.Sqrt(-EndRadiusOfCurvature * offsetLength);
						if (useNonNegativeLengthMeasures)
						{
							double parameterValue = (-offsetLength - SegmentLength) / (clothoidConstant * Math.Sqrt(Math.PI));
							start = new IfcParameterValue(parameterValue);
							parameterValue = SegmentLength / (clothoidConstant * Math.Sqrt(Math.PI));
							length = new IfcParameterValue(parameterValue);
						}
						else
						{
							start = new IfcLengthMeasure(-offsetLength - SegmentLength);
						}
					}
					else
					{
						throw new Exception("Unhandled oppositehand transition!");
					}
				}
				parentCurve = new IfcClothoid(db.Factory.Origin2dPlace, clothoidConstant);
			}
#if (RHINO || GH)
			else if (PredefinedType == IfcAlignmentHorizontalSegmentTypeEnum.HELMERTCURVE)
			{
				if (nextSegment == null)
					throw new NotImplementedException("XX Next Segment required to compute " + PredefinedType.ToString());

				throw new NotImplementedException("XX Not Implemented!");

				//	Plane plane = new Plane();
				//	nextSegment.StartPoint.Location;

			}
#endif
			else if (PredefinedType == IfcAlignmentHorizontalSegmentTypeEnum.CUBIC)
			{
				if (nextSegment == null)
					throw new Exception("XX CubicParabola transition requires next segment!");
				double radius = Math.Abs(StartRadiusOfCurvature) < tol ? EndRadiusOfCurvature : StartRadiusOfCurvature;
				IfcAxis2Placement2D curvePlacement = db.Factory.Origin2dPlace;
				double m = 1;

				IfcCartesianPoint nextSegmentStart = nextSegment.StartPoint;
				double s = 0, t = 0;

				if (Math.Abs(StartRadiusOfCurvature) < tol)
				{
					placement.RemapToParameter(nextSegmentStart.CoordinateX, nextSegmentStart.CoordinateY, out s, out t);
					start = new IfcParameterValue(0);
					length = new IfcParameterValue(s);

					m = Math.Abs(t) / Math.Pow(s, 3);
					if (EndRadiusOfCurvature < tol)
						m = -m;
				}
				else if (Math.Abs(EndRadiusOfCurvature) < tol)
				{
					Tuple<double, double> nextSegmentDirection = nextSegment.StartTangent();
					curvePlacement = new IfcAxis2Placement2D(nextSegmentStart);
					curvePlacement.RefDirection = new IfcDirection(db, nextSegmentDirection.Item1, nextSegmentDirection.Item2);

					curvePlacement.RemapToParameter(StartPoint.CoordinateX, StartPoint.CoordinateY, out s, out t);
					start = new IfcParameterValue(s);
					length = new IfcParameterValue(-s);
					m = Math.Abs(t) / Math.Pow(-s, 3);
					if (StartRadiusOfCurvature > tol)
						m = -m;
				}
				else
					throw new NotImplementedException("XX CubicParabola with finite start and end radius Not Implemented!");
				parentCurve = new IfcPolynomialCurve(curvePlacement, new List<double>() { 0, 1 }, new List<double>() { 0, 0, 0, m });
			}
			else
				throw new NotImplementedException("XX Not Implemented horizontal segment " + PredefinedType.ToString());

			IfcTransitionCode transitionCode = (SegmentLength < tol ? IfcTransitionCode.DISCONTINUOUS : IfcTransitionCode.CONTINUOUS);
			IfcCurveSegment curveSegment = new IfcCurveSegment(transitionCode, placement, start, length, parentCurve);
			if (mDesignerOf != null)
				mDesignerOf.Representation = new IfcProductDefinitionShape(new IfcShapeRepresentation(mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis), curveSegment, ShapeRepresentationType.Segment));

			return curveSegment;
		}

		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcAlignmentHorizontalSegment horizontalSegment = e as IfcAlignmentHorizontalSegment;
			if (horizontalSegment == null)
				return false;
			if (!horizontalSegment.StartPoint.isDuplicate(horizontalSegment.StartPoint, tol))
				return false;
			if (Math.Abs(StartDirection - horizontalSegment.StartDirection) > 0.001)
				return false;
			if (Math.Abs(StartRadiusOfCurvature - horizontalSegment.StartRadiusOfCurvature) > tol)
				return false;
			if (Math.Abs(EndRadiusOfCurvature - horizontalSegment.EndRadiusOfCurvature) > tol)
				return false;
			if (Math.Abs(SegmentLength - horizontalSegment.SegmentLength) > tol)
				return false;
			if (Math.Abs(GravityCenterLineHeight - horizontalSegment.GravityCenterLineHeight) > tol)
				return false;
			if (PredefinedType != horizontalSegment.PredefinedType)
				return false;
			return base.isDuplicate(e, tol);
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public abstract partial class IfcAlignmentParameterSegment : BaseClassIfc
	{
		private string mStartTag = ""; //: OPTIONAL IfcLabel;
		private string mEndTag = ""; //: OPTIONAL IfcLabel;
		internal IfcAlignmentSegment mDesignerOf = null;

		public string StartTag { get { return mStartTag; } set { mStartTag = value; } }
		public string EndTag { get { return mEndTag; } set { mEndTag = value; } }

		protected IfcAlignmentParameterSegment() : base() { }
		protected IfcAlignmentParameterSegment(DatabaseIfc db, IfcAlignmentParameterSegment alignmentParameterSegment, DuplicateOptions options)
			: base(db) 
		{ 
			StartTag = alignmentParameterSegment.StartTag; 
			EndTag = alignmentParameterSegment.EndTag;
			if (mDesignerOf != null)
				db.Factory.Duplicate(mDesignerOf, options);
		}
		protected IfcAlignmentParameterSegment(DatabaseIfc db) : base(db) { }

		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcAlignmentParameterSegment parameterSegment = e as IfcAlignmentParameterSegment;
			if (parameterSegment == null)
				return false;

			if (string.Compare(StartTag, parameterSegment.StartTag, false) != 0)
				return false;

			if (string.Compare(EndTag, parameterSegment.EndTag, false) != 0)
				return false;

			return base.isDuplicate(e, tol);
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcAlignmentSegment : IfcLinearElement
	{
		private IfcAlignmentParameterSegment mDesignParameters = null;// : IfcAlignmentParameterSegment;
		public IfcAlignmentParameterSegment DesignParameters 
		{ 
			get { return mDesignParameters; } 
			set 
			{ 
				if (mDesignParameters != null) 
					mDesignParameters.mDesignerOf = null; 
				mDesignParameters = value;
				if (mDesignParameters != null)
					mDesignParameters.mDesignerOf = this; 
			} 
		}

		public IfcAlignmentSegment() : base() { }
		public IfcAlignmentSegment(DatabaseIfc db) : base(db) { }
		internal IfcAlignmentSegment(DatabaseIfc db, IfcAlignmentSegment alignmentSegment, DuplicateOptions options)
			: base(db, alignmentSegment, options)
		{
			DesignParameters = db.Factory.Duplicate(alignmentSegment.DesignParameters, options) as IfcAlignmentParameterSegment;
		}
		public IfcAlignmentSegment(IfcLinearElement host, IfcAlignmentParameterSegment design) 
			: base(host.Database)
		{
			host.AddNested(this);
			ObjectPlacement = host.ObjectPlacement;
			DesignParameters = design; 
		}

		internal override bool isDuplicate(BaseClassIfc e, OptionsTestDuplicate options)
		{
			IfcAlignmentSegment segment = e as IfcAlignmentSegment;
			if (segment == null)
				return false;
			if (!mDesignParameters.isDuplicate(segment.mDesignParameters, options.Tolerance))
				return false;
			return base.isDuplicate(e, options);
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcAlignmentVertical : IfcLinearElement
	{
		[Obsolete("Interim during IFC4x3RC2", false)]
		private LIST<IfcAlignmentVerticalSegment> mVerticalSegments = new LIST<IfcAlignmentVerticalSegment>(); //: LIST [1:?] OF IfcAlignmentVerticalSegment; 
		
		public IEnumerable<IfcAlignmentVerticalSegment> VerticalSegments 
		{ 
			get 
			{
				if(mVerticalSegments.Count > 0)
					return mVerticalSegments;
				return IsNestedBy.SelectMany(x => x.RelatedObjects).OfType<IfcAlignmentSegment>().Select(x => x.DesignParameters as IfcAlignmentVerticalSegment);
			}
		}

		public IfcAlignmentVertical() : base() { }
		public IfcAlignmentVertical(DatabaseIfc db) : base(db) { }
		internal IfcAlignmentVertical(DatabaseIfc db, IfcAlignmentVertical alignmentVertical, DuplicateOptions options)
			: base(db, alignmentVertical, options)
		{
			if(db.Release == ReleaseVersion.IFC4X3_RC2)
				mVerticalSegments.AddRange(alignmentVertical.VerticalSegments.Select(x => db.Factory.Duplicate(x, options) as IfcAlignmentVerticalSegment));
		}
		public IfcAlignmentVertical(IfcAlignment alignment) 
			: base(alignment.Database) 
		{
			alignment.AddNested(this);
			ObjectPlacement = alignment.ObjectPlacement;
		}
	
		public IfcGradientCurve ComputeVerticalGeometry(IfcAlignment alignment, IfcCompositeCurve horizontalCurve)
		{
			List<IfcCurveSegment> segments = VerticalSegments.Select(x => x.generateCurveSegment()).ToList();
			IfcGradientCurve gradientCurve = new IfcGradientCurve(horizontalCurve, segments);
			if (alignment != null)
			{
				var subContext = mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis);

				IfcShapeRepresentation shapeRepresentation = new IfcShapeRepresentation(subContext, gradientCurve, ShapeRepresentationType.Curve3D);
				if (alignment.Representation == null)
					alignment.Representation = new IfcProductDefinitionShape(shapeRepresentation);
				else
				{
					IfcShapeRepresentation axisShape = alignment.Representation.Representations.Where(x => string.Compare(x.RepresentationIdentifier, "Axis", true) == 0).FirstOrDefault() as IfcShapeRepresentation;
					if(axisShape != null)
					{
						var footPrintContext = mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.FootPrint);
						axisShape.ContextOfItems = footPrintContext;
						axisShape.RepresentationIdentifier = footPrintContext.ContextIdentifier;
					}
					alignment.Representation.Representations.Add(shapeRepresentation);
				}
			}
			return gradientCurve;
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcAlignmentVerticalSegment : IfcAlignmentParameterSegment
	{
		private double mStartDistAlong = 0; //: IfcLengthMeasure;
		private double mHorizontalLength = 0; //: IfcNonNegativeLengthMeasure;
		private double mStartHeight = 0; //: IfcLengthMeasure;
		private double mStartGradient = 0; //: IfcRatioMeasure;
		internal double mEndGradient;// : IfcRatioMeasure; 
		internal double mRadiusOfCurvature = double.NaN;// : OPTIONAL IfcLengthMeasure;
		private IfcAlignmentVerticalSegmentTypeEnum mPredefinedType; //: IfcAlignmentVerticalSegmentTypeEnum;

		public double StartDistAlong { get { return mStartDistAlong; } set { mStartDistAlong = value; } }
		public double HorizontalLength { get { return mHorizontalLength; } set { mHorizontalLength = value; } }
		public double StartHeight { get { return mStartHeight; } set { mStartHeight = value; } }
		public double StartGradient { get { return mStartGradient; } set { mStartGradient = value; } }
		public double EndGradient { get { return mEndGradient; } set { mEndGradient = value; } }
		public double RadiusOfCurvature { get { return mRadiusOfCurvature; } set { mRadiusOfCurvature = value; } }
		public IfcAlignmentVerticalSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcAlignmentVerticalSegment() : base() { }
		public IfcAlignmentVerticalSegment(DatabaseIfc db) : base(db) { }
		internal IfcAlignmentVerticalSegment(DatabaseIfc db, IfcAlignmentVerticalSegment alignmentVerticalSegment, DuplicateOptions options)
			: base(db, alignmentVerticalSegment, options)
		{
			StartDistAlong = alignmentVerticalSegment.StartDistAlong;
			HorizontalLength = alignmentVerticalSegment.HorizontalLength;
			StartHeight = alignmentVerticalSegment.StartHeight;
			StartGradient = alignmentVerticalSegment.StartGradient;
			EndGradient = alignmentVerticalSegment.EndGradient;
			RadiusOfCurvature = alignmentVerticalSegment.RadiusOfCurvature;
			PredefinedType = alignmentVerticalSegment.PredefinedType;
		}
		public IfcAlignmentVerticalSegment(DatabaseIfc db, double startDistAlong, double horizontalLength, double startHeight, double constantGradient)
			: this(db, startDistAlong, horizontalLength, startHeight, constantGradient, constantGradient, IfcAlignmentVerticalSegmentTypeEnum.CONSTANTGRADIENT) { }
		public IfcAlignmentVerticalSegment(DatabaseIfc db, double startDistAlong, double horizontalLength, double startHeight, double startGradient, double endGradient, IfcAlignmentVerticalSegmentTypeEnum predefinedType)
			: base(db)
		{
			StartDistAlong = startDistAlong;
			HorizontalLength = horizontalLength;
			StartHeight = startHeight;
			StartGradient = startGradient;
			EndGradient = endGradient;
			PredefinedType = predefinedType;
		}

		public IfcCurveSegment generateCurveSegment()
		{
			DatabaseIfc db = mDatabase;
			double tol = db.Tolerance;
			double theta = Math.Atan(StartGradient);
			IfcCartesianPoint startPoint = new IfcCartesianPoint(db, StartDistAlong, StartHeight);
			IfcDirection refDirection = new IfcDirection(db, Math.Cos(theta), Math.Sin(theta));
			IfcAxis2Placement2D axis2Placement2D = new IfcAxis2Placement2D(startPoint) { RefDirection = refDirection };
			IfcTransitionCode transitionCode = HorizontalLength < tol ? IfcTransitionCode.DISCONTINUOUS : IfcTransitionCode.CONTINUOUS;
			IfcCurveSegment curveSegment = null;
			bool useNonNegativeLengthMeasures = db.Release < ReleaseVersion.IFC4X3_ADD2;
			if (PredefinedType == IfcAlignmentVerticalSegmentTypeEnum.CONSTANTGRADIENT)
			{
				double segmentLength = HorizontalLength / Math.Cos(theta);
				IfcLine line = db.Factory.LineX2d;
				IfcCurveMeasureSelect start = new IfcLengthMeasure(0), length = new IfcLengthMeasure(segmentLength);
				if(useNonNegativeLengthMeasures)
				{
					start = new IfcNonNegativeLengthMeasure(0);
					length = new IfcNonNegativeLengthMeasure(segmentLength);
				}
				curveSegment = new IfcCurveSegment(transitionCode, axis2Placement2D, start, length, line);
			}
			else if (PredefinedType == IfcAlignmentVerticalSegmentTypeEnum.CIRCULARARC)
			{
				double parametricLength = (Math.Atan(EndGradient) - theta) / db.ScaleAngle();

				IfcCircle circle = new IfcCircle(db, Math.Abs(mRadiusOfCurvature));
				IfcCurveMeasureSelect start = new IfcLengthMeasure(0), length = new IfcParameterValue(parametricLength);
				if (useNonNegativeLengthMeasures)
				{
					start = new IfcNonNegativeLengthMeasure(0); 
				}
				curveSegment = new IfcCurveSegment(transitionCode, axis2Placement2D, start, length, circle);
			}
			else if (PredefinedType == IfcAlignmentVerticalSegmentTypeEnum.PARABOLICARC)
			{
				double m = (EndGradient - StartGradient) / (2 * HorizontalLength);
				var coefficientsX = new List<double>() { 0, 1 };
				var coefficientsY = new List<double>() { StartHeight, StartGradient, m };
				IfcPolynomialCurve seriesParameterCurve = new IfcPolynomialCurve(db.Factory.Origin2dPlace, coefficientsX, coefficientsY);
				IfcParameterValue start = new IfcParameterValue(0);
				IfcParameterValue segmentLength = new IfcParameterValue(HorizontalLength);
				curveSegment = new IfcCurveSegment(transitionCode, axis2Placement2D, start, segmentLength, seriesParameterCurve);
			}
			else
				throw new NotImplementedException("XX Not Implemented vertical segment " + PredefinedType.ToString());

			if (mDesignerOf != null)
				mDesignerOf.Representation = new IfcProductDefinitionShape(new IfcShapeRepresentation(mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis), curveSegment, ShapeRepresentationType.Segment));
			return curveSegment;
		}

		public double HeightAt(double distanceAlongSegment)
		{
			double gradient = 0;
			return HeightAt(distanceAlongSegment, out gradient);
		}

		public double HeightAt(double distanceAlongSegment, out double gradient)
		{
			if (PredefinedType == IfcAlignmentVerticalSegmentTypeEnum.CONSTANTGRADIENT)
			{
				gradient = StartGradient;
				return StartHeight + StartGradient * (distanceAlongSegment);
			}
			if (PredefinedType == IfcAlignmentVerticalSegmentTypeEnum.CIRCULARARC)
			{
				double x = distanceAlongSegment;
				double R = Math.Abs(mRadiusOfCurvature), g1 = StartGradient;
				double c = 1 / Math.Sqrt(1 + g1 * g1);
				double xc = g1 * R * c, yc = R * c;
				double y = 0;
				if (g1 > EndGradient)
				{
					y = Math.Sqrt(R * R - Math.Pow(x - xc, 2));
					gradient = (xc - x) / y;
					return StartHeight - yc + y;
				}
				y = Math.Sqrt(R * R - Math.Pow(x + xc, 2));
				gradient = (x + xc) / y;
				return StartHeight + yc - y;
			}
			if (PredefinedType == IfcAlignmentVerticalSegmentTypeEnum.PARABOLICARC)
			{
				double x = distanceAlongSegment;
				double R = Math.Abs(RadiusOfCurvature) * (EndGradient < StartGradient ? -1 : 1);
				gradient = x / R + StartGradient;
				double result = StartHeight + x * (gradient + StartGradient) / 2;
				return result;
			}
			gradient = double.NaN;	
			throw new NotImplementedException("Computation of height for " + PredefinedType + " not implemented yet!");
		}
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcAngularDimension : IfcDimensionCurveDirectedCallout 
	{
		internal IfcAngularDimension() : base() { }
		public IfcAngularDimension(IfcDraughtingCalloutElement content) : base(content) { }
		public IfcAngularDimension(IEnumerable<IfcDraughtingCalloutElement> contents) : base(contents) { }
	}
	[Serializable]
	public partial class IfcAnnotation : IfcProduct
	{    
		private IfcAnnotationTypeEnum mPredefinedType = IfcAnnotationTypeEnum.NOTDEFINED;//: OPTIONAL IfcBeamTypeEnum; IFC4
		public IfcAnnotationTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAnnotationTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAnnotation() : base() { }
		public IfcAnnotation(DatabaseIfc db) : base(db) { }
		internal IfcAnnotation(DatabaseIfc db, IfcAnnotation a, DuplicateOptions options) : base(db, a, options) { }
		public IfcAnnotation(IfcProduct host) : base(host.mDatabase) { host.AddElement(this); }
		public IfcAnnotation(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) 
			: base(host.Database) 
		{
			if (host is IfcSpatialElement spatialElement)
				spatialElement.AddElement(this);
			else
				host.AddAggregated(this);
			ObjectPlacement = placement;
			Representation = representation;
		}

	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public abstract partial class IfcAnnotationCurveOccurrence : IfcAnnotationOccurrence
	{
		protected IfcAnnotationCurveOccurrence() : base() { }
		protected IfcAnnotationCurveOccurrence(IfcPresentationStyleAssignment style) : base(style) { }
	}
	[Serializable]
	public partial class IfcAnnotationFillArea : IfcGeometricRepresentationItem  
	{
		internal IfcCurve mOuterBoundary;// : IfcCurve;
		internal SET<IfcCurve> mInnerBoundaries = new SET<IfcCurve>();// OPTIONAL SET [1:?] OF IfcCurve; 

		public IfcCurve OuterBoundary { get { return mOuterBoundary; } set { mOuterBoundary = value; } }
		public SET<IfcCurve> InnerBoundaries { get { return mInnerBoundaries; } }

		internal IfcAnnotationFillArea() : base() { }
		internal IfcAnnotationFillArea(DatabaseIfc db, IfcAnnotationFillArea a, DuplicateOptions options) : base(db, a, options) { OuterBoundary = a.OuterBoundary.Duplicate(db, options) as IfcCurve; InnerBoundaries.AddRange(a.InnerBoundaries.ConvertAll(x=> db.Factory.Duplicate(x) as IfcCurve)); }
		public IfcAnnotationFillArea(IfcCurve outerBoundary) : base(outerBoundary.mDatabase) { OuterBoundary = outerBoundary; }
		public IfcAnnotationFillArea(IfcCurve outerBoundary, List<IfcCurve> innerBoundaries) : this(outerBoundary) { InnerBoundaries.AddRange(innerBoundaries); }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcAnnotationFillAreaOccurrence : IfcAnnotationOccurrence
	{
		internal IfcPoint mFillStyleTarget;// : OPTIONAL IfcPoint;
		internal IfcGlobalOrLocalEnum  mGlobalOrLocal = IfcGlobalOrLocalEnum.NOTDEFINED;// : OPTIONAL IfcGlobalOrLocalEnum; 
		public IfcPoint FillStyleTarget { get { return mFillStyleTarget; } set { mFillStyleTarget = value; } }
		public IfcGlobalOrLocalEnum GlobalOrLocal { get { return mGlobalOrLocal; } set { mGlobalOrLocal = value; } }

		internal IfcAnnotationFillAreaOccurrence() : base() { }
		internal IfcAnnotationFillAreaOccurrence(DatabaseIfc db, IfcAnnotationFillAreaOccurrence f, DuplicateOptions options) : base(db, f, options) { }
		public IfcAnnotationFillAreaOccurrence(IfcPresentationStyleAssignment style) : base(style) { }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public abstract partial class IfcAnnotationOccurrence : IfcStyledItem 
	{
		protected IfcAnnotationOccurrence(DatabaseIfc db, IfcAnnotationOccurrence o, DuplicateOptions options) : base(db, o, options) { }
		protected IfcAnnotationOccurrence() : base() { }
		protected IfcAnnotationOccurrence(IfcPresentationStyleAssignment style) : base(style) { }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcAnnotationSurface : IfcGeometricRepresentationItem 
	{
		internal IfcGeometricRepresentationItem mItem;// : IfcGeometricRepresentationItem;
		internal IfcTextureCoordinate mTextureCoordinates;// OPTIONAL IfcTextureCoordinate;

		public IfcGeometricRepresentationItem Item { get { return mItem; } set { mItem = value; } }
		public IfcTextureCoordinate TextureCoordinates { get { return mTextureCoordinates; } set { mTextureCoordinates = value; } }

		internal IfcAnnotationSurface() : base() { } 
		internal IfcAnnotationSurface(DatabaseIfc db, IfcAnnotationSurface a, DuplicateOptions options) : base(db, a, options) { Item = a.Item.Duplicate(db, options) as IfcGeometricRepresentationItem; if(a.mTextureCoordinates != null) TextureCoordinates = db.Factory.Duplicate(a.TextureCoordinates) as IfcTextureCoordinate; }
		public IfcAnnotationSurface(IfcGeometricRepresentationItem item) : base(item.Database) { mItem = item; }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcAnnotationSurfaceOccurrence : IfcAnnotationOccurrence 
	{
		internal IfcAnnotationSurfaceOccurrence() : base() { }
		internal IfcAnnotationSurfaceOccurrence(DatabaseIfc db, IfcAnnotationSurfaceOccurrence o, DuplicateOptions options) : base(db, o, options) { }
		public IfcAnnotationSurfaceOccurrence(IfcPresentationStyleAssignment style) : base(style) { }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcAnnotationSymbolOccurrence : IfcAnnotationOccurrence
	{
		internal IfcAnnotationSymbolOccurrence() : base() { }
		internal IfcAnnotationSymbolOccurrence(DatabaseIfc db, IfcAnnotationSymbolOccurrence o, DuplicateOptions options) : base(db, o, options) { }
		public IfcAnnotationSymbolOccurrence(IfcPresentationStyleAssignment style) : base(style) { }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcAnnotationTextOccurrence : IfcAnnotationOccurrence
	{
		internal IfcAnnotationTextOccurrence() : base() { }
		internal IfcAnnotationTextOccurrence(DatabaseIfc db, IfcAnnotationTextOccurrence o, DuplicateOptions options) : base(db, o, options) { }
		public IfcAnnotationTextOccurrence(IfcPresentationStyleAssignment style) : base(style) { }
	}
	[Serializable]
	public partial class IfcApplication : BaseClassIfc, NamedObjectIfc
	{
		internal IfcOrganization mApplicationDeveloper = null;// : IfcOrganization;
		internal string mVersion;// : IfcLabel;
		private string mApplicationFullName;// : IfcLabel;
		internal string mApplicationIdentifier;// : IfcIdentifier; 
		
		public IfcOrganization ApplicationDeveloper { get { return mApplicationDeveloper; } set { mApplicationDeveloper = value; } }
		public string Version { get { return mVersion; } set { mVersion = ParserSTEP.Encode(value); } }
		public string ApplicationFullName { get { return ParserSTEP.Decode(mApplicationFullName); } set { mApplicationFullName =  ParserSTEP.Encode(value); } }
		public string ApplicationIdentifier { get { return ParserSTEP.Decode(mApplicationIdentifier); } set { mApplicationIdentifier =  ParserSTEP.Encode(value); } }

		public string Name { get { return ApplicationFullName; } set { ApplicationFullName = value; } }

		internal IfcApplication() : base() { }
		private IfcApplication(DatabaseIfc db) : base(db) { }
		internal IfcApplication(DatabaseIfc db, IfcApplication a, DuplicateOptions options) : base(db,a)
		{
			ApplicationDeveloper = db.Factory.Duplicate(a.ApplicationDeveloper, options);
			mVersion = a.mVersion;
			mApplicationFullName = a.mApplicationFullName;
			mApplicationIdentifier = a.mApplicationIdentifier;
		}
		public static IfcApplication Construct(IfcOrganization developer, string version, string fullName, string identifier)
		{
			DatabaseIfc db = developer.Database;
			if(db != null)
			{
				IfcApplication existing = SeekExisting(db, version, fullName, identifier);
				if (existing != null)
					return existing;
			}
			IfcApplication result = new IfcApplication(db);
			result.ApplicationDeveloper = developer; 
			result.Version = version; 
			result.ApplicationFullName = fullName; 
			result.ApplicationIdentifier = identifier;
			return result;
		}
		public static IfcApplication Construct(DatabaseIfc db)
		{
			IfcOrganization applicationDeveloper = new IfcOrganization(db, "Geometry Gym Pty Ltd");
			string version = IdentifyVersion();
			string applicationFullName = db.Factory.ApplicationFullName;
			string applicationIdentifier = db.Factory.ApplicationIdentifier;
			return Construct(applicationDeveloper, version, applicationFullName, applicationIdentifier);
		}
		public static string IdentifyVersion()
		{
			try
			{
				Assembly assembly = Assembly.GetEntryAssembly();
				if (assembly == null)
					assembly = Assembly.GetCallingAssembly();
				if (assembly != null)
				{
					AssemblyName name = assembly.GetName();
					return "v" + name.Version.ToString();
				}
			}
			catch (Exception) { }
			return "Unknown"; 
		}
		internal IfcApplication SeekExisting(DatabaseIfc db) { return SeekExisting(db, Version, ApplicationFullName, ApplicationIdentifier); }
		internal static IfcApplication SeekExisting(DatabaseIfc db, string version, string fullName, string identifier)
		{ 
			foreach (IfcApplication application in db.OfType<IfcApplication>())
			{
				if (string.Compare(identifier, application.ApplicationIdentifier, true) == 0)
					return application;
				if (string.Compare(application.ApplicationFullName, fullName, true) == 0 && string.Compare(application.Version, version, true) == 0)
					return application;
			}
			return null;
		}
	}
	[Serializable]
	public partial class IfcAppliedValue : BaseClassIfc, IfcMetricValueSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect, NamedObjectIfc
	{  // SUPERTYPE OF(IfcCostValue);
		internal string mName = "";// : OPTIONAL IfcLabel;
		internal string mDescription = "";// : OPTIONAL IfcText;
		internal IfcAppliedValueSelect mAppliedValue = null;// : OPTIONAL IfcAppliedValueSelect;
		internal IfcMeasureWithUnit mUnitBasis = null;// : OPTIONAL IfcMeasureWithUnit;
		internal DateTime mApplicableDate = DateTime.MinValue;// : OPTIONAL IfcDateTimeSelect; 4 IfcDate
		internal DateTime mFixedUntilDate = DateTime.MinValue;// : OPTIONAL IfcDateTimeSelect; 4 IfcDate
		private IfcDateTimeSelect mSSApplicableDate = null;
		private IfcDateTimeSelect mSSFixedUntilDate = null;
		internal string mCategory = "";// : OPTIONAL IfcLabel; IFC4
		internal string mCondition = "";// : OPTIONAL IfcLabel; IFC4
		internal IfcArithmeticOperatorEnum mArithmeticOperator = IfcArithmeticOperatorEnum.NONE;//	 :	OPTIONAL IfcArithmeticOperatorEnum; IFC4 
		internal LIST<IfcAppliedValue> mComponents = new LIST<IfcAppliedValue>();//	 :	OPTIONAL LIST [1:?] OF IfcAppliedValue; IFC4
		//INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal SET<IfcResourceConstraintRelationship> mHasConstraintRelationships = new SET<IfcResourceConstraintRelationship>(); //gg
		internal SET<IfcAppliedValue> mComponentFor = new SET<IfcAppliedValue>(); //gg

		public string Name { get { return mName; } set { mName = value; } } 
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public IfcAppliedValueSelect AppliedValue { get { return mAppliedValue; } set { mAppliedValue = value; } }
		public IfcMeasureWithUnit UnitBasis { get { return mUnitBasis; } set { mUnitBasis = value; } }
		public DateTime ApplicableDate { get { return mApplicableDate; } set { mApplicableDate = value; } }
		public DateTime FixedUntilDate { get { return mFixedUntilDate; } set { mFixedUntilDate = value; } }
		public string Category { get { return mCategory; } set { mCategory = value; } }
		public string Condition { get { return mCondition; } set { mCondition = value; } }
		public IfcArithmeticOperatorEnum ArithmeticOperator { get { return mArithmeticOperator; } set { mArithmeticOperator = value; } }
		public LIST<IfcAppliedValue> Components { get { return mComponents; } }
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } }
		public SET<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcAppliedValue() : base() { }
		internal IfcAppliedValue(DatabaseIfc db, IfcAppliedValue v) : base(db, v)
		{
			mName = v.mName;
			mDescription = v.mDescription;
			if(v.mAppliedValue != null)
			{
				IfcValue value = v.mAppliedValue as IfcValue;
				if (value != null)
					mAppliedValue = value;
				else
					AppliedValue = db.Factory.Duplicate(v.mAppliedValue);
			}
			UnitBasis = db.Factory.Duplicate(v.UnitBasis);
			mApplicableDate = v.mApplicableDate; 
			mFixedUntilDate = v.mFixedUntilDate; 
			mCategory = v.mCategory; 
			mCondition = v.mCondition; 
			mArithmeticOperator = v.mArithmeticOperator;
			Components.AddRange(v.Components.Select(x => db.Factory.Duplicate(x)));
		}
		public IfcAppliedValue(DatabaseIfc db) : base(db) { }
		public IfcAppliedValue(IfcAppliedValueSelect appliedValue) : base(appliedValue.Database) { AppliedValue = appliedValue; }
		public IfcAppliedValue(DatabaseIfc db, IfcValue value) : base(db) { AppliedValue = value; }
		public IfcAppliedValue(IfcAppliedValue component1, IfcArithmeticOperatorEnum op,IfcAppliedValue component2) 
			: base(component1.mDatabase)
		{ 
			Components.Add(component1); 
			Components.Add(component2); 
			mArithmeticOperator = op; 
		} 
		protected override void initialize()
		{
			base.initialize();

			mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged;
			mComponents.CollectionChanged += mComponents_CollectionChanged;
		}
		private void mHasExternalReference_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.NewItems)
					r.RelatedResourceObjects.Add(this);
			}
			if (e.OldItems != null)
			{
				foreach (IfcExternalReferenceRelationship r in e.OldItems)
					r.RelatedResourceObjects.Remove(this);
			}
		}
		private void mComponents_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcAppliedValue appliedValue in e.NewItems)
					appliedValue.mComponentFor.Add(this);
			}
			if (e.OldItems != null)
			{
				foreach (IfcAppliedValue appliedValue in e.OldItems)
					appliedValue.mComponentFor.Remove(this);
			}
		}

		protected override bool DisposeWorker(bool children)
		{
			if (children)
			{
				BaseClassIfc value = mAppliedValue as BaseClassIfc;
				if(value != null)
					value.Dispose(children);
				foreach(IfcAppliedValue appliedValue in mComponents.ToList())
					appliedValue.Dispose(true);
			}
			return base.DisposeWorker(children);
		}

		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcAppliedValueRelationship : BaseClassIfc, NamedObjectIfc
	{
		internal IfcAppliedValue mComponentOfTotal;// : IfcAppliedValue;
		internal SET<IfcAppliedValue> mComponents = new SET<IfcAppliedValue>();// : SET [1:?] OF IfcAppliedValue;
		internal IfcArithmeticOperatorEnum mArithmeticOperator;// : IfcArithmeticOperatorEnum;
		internal string mName = "";// : OPTIONAL IfcLabel;
		internal string mDescription = "";// : OPTIONAL IfcText 

		public IfcAppliedValue ComponentOfTotal { get { return mComponentOfTotal; } set { mComponentOfTotal = value; } }
		public SET<IfcAppliedValue> Components { get { return mComponents; } }
		public IfcArithmeticOperatorEnum ArithmeticOperator { get { return mArithmeticOperator; } set { mArithmeticOperator = value; } }
		public string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }

		internal IfcAppliedValueRelationship() : base() { }
		public IfcAppliedValueRelationship(IfcAppliedValue componentOfTotal, IEnumerable<IfcAppliedValue> components, IfcArithmeticOperatorEnum arithmeticOperator)
			: base(componentOfTotal.Database)
		{
			mComponentOfTotal = componentOfTotal;
			mComponents.AddRange(components);
			mArithmeticOperator = arithmeticOperator;
		}
	}
	public interface IfcAppliedValueSelect : IBaseClassIfc  //	IfcMeasureWithUnit, IfcValue, IfcReference); IFC2x3 //IfcRatioMeasure, IfcMeasureWithUnit, IfcMonetaryMeasure); 
	{
		//List<IfcAppliedValue> AppliedValueFor { get; }
	}
	[Serializable]
	public partial class IfcApproval : BaseClassIfc, IfcResourceObjectSelect, NamedObjectIfc
	{
		internal string mIdentifier = "";// : OPTIONAL IfcIdentifier;
		internal string mName = "";// :OPTIONAL IfcLabel;
		internal string mDescription = "";// : OPTIONAL IfcText;
		internal DateTime mTimeOfApproval;// : IfcDateTime
		internal IfcDateTimeSelect mApprovalDateTime;// : IfcDateTimeSelect;
		internal string mStatus = "";// : OPTIONAL IfcLabel;
		internal string mLevel = "";// : OPTIONAL IfcLabel;
		internal string mQualifier = "";// : OPTIONAL IfcText;
		internal IfcActorSelect mRequestingApproval = null;// : OPTIONAL IfcActorSelect;
		internal IfcActorSelect mGivingApproval = null;// : OPTIONAL IfcActorSelect;
		//INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		private SET<IfcRelAssociatesApproval> mApprovedObjects = new SET<IfcRelAssociatesApproval>();
		internal SET<IfcResourceConstraintRelationship> mHasConstraintRelationships = new SET<IfcResourceConstraintRelationship>(); //gg

		public string Identifier { get { return mIdentifier; } set { mIdentifier = value; } }
		public string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public DateTime TimeOfApproval { get { return mTimeOfApproval; } set { mTimeOfApproval = value; } }
		public string Status { get { return mStatus; } set { mStatus = value; } }
		public string Level { get { return mLevel; } set { mLevel = value; } }
		public string Qualifier { get { return mQualifier; } set { mQualifier = value; } }
		public IfcActorSelect RequestingApproval { get { return mRequestingApproval; } set { mRequestingApproval = value; } }
		public IfcActorSelect GivingApproval { get { return mGivingApproval; } set { mGivingApproval = value; } }
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } }
		public SET<IfcRelAssociatesApproval> ApprovedObjects { get { return mApprovedObjects; } }
		public SET<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcApproval() : base() { }
		public IfcApproval(DatabaseIfc db) : base(db) { }
		//internal IfcApproval(IfcApproval o) : base()
		//{
		//	mDescription = o.mDescription;
		//	mApprovalDateTime = o.mApprovalDateTime;
		//	mApprovalStatus = o.mApprovalStatus;
		//	mApprovalLevel = o.mApprovalLevel;
		//	mApprovalQualifier = o.mApprovalQualifier;
		//	mName = o.mName;
		//	mIdentifier = o.mIdentifier;
		//}
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
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcApprovalActorRelationship : BaseClassIfc 
	{
		internal IfcActorSelect mActor;// : IfcActorSelect;
		internal IfcApproval mApproval;// : IfcApproval;
		internal IfcActorRole mRole;// : IfcActorRole; 
		internal IfcApprovalActorRelationship() : base() { }
		public IfcApprovalActorRelationship(IfcActorSelect actor, IfcApproval approval, IfcActorRole role)
			: base(actor.Database) { mActor = actor; mApproval = approval; mRole = role; }
	}
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcApprovalPropertyRelationship : BaseClassIfc 
	{
		internal SET<IfcProperty> mApprovedProperties = new SET<IfcProperty>();// : SET [1:?] OF IfcProperty;
		internal IfcApproval mApproval;// : IfcApproval; 

		public SET<IfcProperty> ApprovedProperties { get { return mApprovedProperties; } }
		public IfcApproval Approval { get { return mApproval; } set { mApproval = value; } }
		internal IfcApprovalPropertyRelationship() : base() { }
		internal IfcApprovalPropertyRelationship(DatabaseIfc db) : base(db) { }
		public IfcApprovalPropertyRelationship(IEnumerable<IfcProperty> properties, IfcApproval approval) 
			: base(approval.Database) { mApprovedProperties.AddRange(properties); Approval = approval; }
	}
	[Serializable]
	public partial class IfcApprovalRelationship : IfcResourceLevelRelationship //IFC4Change
	{
		internal IfcApproval mRelatedApproval;// : IfcApproval;
		internal SET<IfcApproval> mRelatingApprovals = new SET<IfcApproval>();// SET [1:?] OF IfcApproval; 
		public IfcApproval RelatedApproval { get { return mRelatedApproval; } set { mRelatedApproval = value; } }
		public SET<IfcApproval> RelatingApprovals { get { return mRelatingApprovals; } }
		internal IfcApprovalRelationship() : base() { }
		internal IfcApprovalRelationship(DatabaseIfc db) : base(db) { }
		public IfcApprovalRelationship(IfcApproval related, IfcApproval relating) 
			: base(related.Database) { mRelatedApproval = related; mRelatingApprovals.Add(relating); }
		public IfcApprovalRelationship(IfcApproval related, IEnumerable<IfcApproval> relating) 
			: base(related.Database) { mRelatedApproval = related; mRelatingApprovals.AddRange(relating); }
	}
	[Serializable]
	public partial class IfcArbitraryClosedProfileDef : IfcProfileDef //SUPERTYPE OF(IfcArbitraryProfileDefWithVoids)
	{
		private IfcCurve mOuterCurve;//: IfcCurve;
		public IfcCurve OuterCurve { get { return mOuterCurve; } set { mOuterCurve = value; } }

		internal IfcArbitraryClosedProfileDef() : base() { }
		internal IfcArbitraryClosedProfileDef(DatabaseIfc db, IfcArbitraryClosedProfileDef p, DuplicateOptions options) 
			: base(db, p, options) { OuterCurve = p.OuterCurve.Duplicate(db, options) as IfcCurve; }
		public IfcArbitraryClosedProfileDef(string name, IfcCurve boundedCurve) 
			: base(boundedCurve.mDatabase,name) { mOuterCurve = boundedCurve; }

		public override string StepClassName { get { return (this is IfcArbitraryProfileDefWithVoids profileWithVoids && profileWithVoids.InnerCurves.Count == 0 ? "IfcArbitraryClosedProfileDef" : base.StepClassName); } }
	}
	[Serializable]
	public partial class IfcArbitraryOpenProfileDef : IfcProfileDef //	SUPERTYPE OF(IfcCenterLineProfileDef)
	{
		private IfcBoundedCurve mCurve;// : IfcBoundedCurve
		public IfcBoundedCurve Curve { get { return mCurve; } set { mCurve = value; } }

		internal IfcArbitraryOpenProfileDef() : base() { }
		internal IfcArbitraryOpenProfileDef(DatabaseIfc db, IfcArbitraryOpenProfileDef p, DuplicateOptions options) : base(db, p, options) { Curve = p.Curve.Duplicate(db, options) as IfcBoundedCurve; }
		public IfcArbitraryOpenProfileDef(string name, IfcBoundedCurve boundedCurve) 
			: base(boundedCurve.mDatabase,name) { mCurve = boundedCurve; mProfileType = IfcProfileTypeEnum.CURVE; }
	}
	[Serializable]
	public partial class IfcArbitraryProfileDefWithVoids : IfcArbitraryClosedProfileDef
	{
		private SET<IfcCurve> mInnerCurves = new SET<IfcCurve>();// : SET [1:?] OF IfcCurve; 
		public SET<IfcCurve> InnerCurves { get { return mInnerCurves; } }

		internal IfcArbitraryProfileDefWithVoids() : base() { }
		internal IfcArbitraryProfileDefWithVoids(DatabaseIfc db, IfcArbitraryProfileDefWithVoids p, DuplicateOptions options) : base(db, p, options) 
		{ 
			InnerCurves.AddRange(p.InnerCurves.Select(x=> db.Factory.Duplicate(x, options) as IfcCurve));
		}
		public IfcArbitraryProfileDefWithVoids(string name, IfcCurve perim, IfcCurve inner) : base(name, perim) { mInnerCurves.Add(inner); }
		public IfcArbitraryProfileDefWithVoids(string name, IfcCurve perim, IEnumerable<IfcCurve> inner) : base(name, perim) { mInnerCurves.AddRange(inner); }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcArchElement : IfcBuiltElement  
	{
		private IfcArchElementTypeEnum mPredefinedType = IfcArchElementTypeEnum.NOTDEFINED;// OPTIONAL IfcArchElementTypeEnum; 
		public IfcArchElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcArchElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X4_DRAFT : mDatabase.Release); } }

		internal IfcArchElement() : base() { }
		internal IfcArchElement(DatabaseIfc db) : base(db) { }
		internal IfcArchElement(DatabaseIfc db, IfcArchElement e, DuplicateOptions options) : base(db, e, options) { PredefinedType = e.PredefinedType; }
		public IfcArchElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcArchElementType : IfcBuiltElementType 
	{
		private IfcArchElementTypeEnum mPredefinedType = IfcArchElementTypeEnum.NOTDEFINED;// IfcArchElementTypeEnum; 
		public IfcArchElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = validPredefinedType<IfcArchElementTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X4_DRAFT : mDatabase.Release); } }

		internal IfcArchElementType() : base() { }
		internal IfcArchElementType(DatabaseIfc db, IfcArchElementType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcArchElementType(DatabaseIfc db, string name, IfcArchElementTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcArcIndex : List<int>, IfcSegmentIndexSelect
	{
		public IfcArcIndex(int a, int b, int c) { Add(a); Add(b); Add(c); }
		public override string ToString() { return "IFCARCINDEX((" + this[0] + "," + this[1] + "," + this[2] + "))"; }
	}
	[Serializable]
	public partial class IfcAsset : IfcGroup
	{
		internal string mIdentification = "";// ifc2x3 AssetID; : OPTIONAL IfcIdentifier;
		internal IfcCostValue mOriginalValue = null;// : OPTIONAL IfcCostValue;
		internal IfcCostValue mCurrentValue = null;// : OPTIONAL IfcCostValue;
		internal IfcCostValue mTotalReplacementCost = null;// : OPTIONAL IfcCostValue;
		internal IfcActorSelect mOwner;// : IfcActorSelect;
		internal IfcActorSelect mUser;// : IfcActorSelect;
		internal IfcPerson mResponsiblePerson;// : IfcPerson;
		internal DateTime mIncorporationDate = DateTime.MinValue; // : IfcDate 
		internal IfcCalendarDate mIncorporationDateSS;// : IfcDate Ifc2x3 IfcCalendarDate;
		internal IfcCostValue mDepreciatedValue;// : IfcCostValue; 

		public string Identification { get { return mIdentification; } set { mIdentification = value; } }
		public IfcCostValue OriginalValue { get { return mOriginalValue; } set { mOriginalValue = value; } } 
		public IfcCostValue CurrentValue { get { return mCurrentValue; } set { mCurrentValue = value; } } 
		public IfcCostValue TotalReplacementCost { get { return mTotalReplacementCost; } set { mTotalReplacementCost = value; } } 
		public IfcActorSelect Owner { get { return mOwner; } set { mOwner = value; } }
		public IfcActorSelect User { get { return mUser; } set { mUser = value; } }
		public IfcPerson ResponsiblePerson { get { return mResponsiblePerson; } set { mResponsiblePerson = value; } }
		//public  IncorporationDate
		public IfcCostValue DepreciatedValue { get { return mDepreciatedValue; } set { mDepreciatedValue = value; } } 

		
		internal IfcAsset() : base() { }
		internal IfcAsset(DatabaseIfc db, IfcAsset a, DuplicateOptions options) : base(db, a, options)
		{
			mIdentification = a.mIdentification;
			OriginalValue = db.Factory.Duplicate(a.OriginalValue);
			CurrentValue = db.Factory.Duplicate(a.CurrentValue);
			TotalReplacementCost = db.Factory.Duplicate(a.TotalReplacementCost);
			Owner = db.Factory.Duplicate(a.mOwner);
			User = db.Factory.Duplicate(a.mUser);
			ResponsiblePerson = db.Factory.Duplicate(a.ResponsiblePerson);
			mIncorporationDate = a.mIncorporationDate;
			if(a.mIncorporationDateSS != null)
				mIncorporationDateSS = db.Factory.Duplicate(a.mIncorporationDateSS);

			DepreciatedValue =  db.Factory.Duplicate(a.DepreciatedValue) as IfcCostValue;
		}
		public IfcAsset(DatabaseIfc db, string name) : base(db, name) { }
	}
	[Serializable]
	public partial class IfcAsymmetricIShapeProfileDef : IfcParameterizedProfileDef // Ifc2x3 IfcIShapeProfileDef 
	{
		internal double mBottomFlangeWidth, mOverallDepth, mWebThickness, mBottomFlangeThickness;//	:	IfcPositiveLengthMeasure;
		internal double mBottomFlangeFilletRadius = double.NaN;//	:	OPTIONAL IfcNonNegativeLengthMeasure;
		internal double mTopFlangeWidth;// : IfcPositiveLengthMeasure;
		internal double mTopFlangeThickness = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mTopFlangeFilletRadius = double.NaN;// 	:	OPTIONAL IfcNonNegativeLengthMeasure;
		internal double mBottomFlangeEdgeRadius = double.NaN;//	:	OPTIONAL IfcNonNegativeLengthMeasure;
		internal double mBottomFlangeSlope = double.NaN;//	:	OPTIONAL IfcPlaneAngleMeasure;
		internal double mTopFlangeEdgeRadius = double.NaN;//	:	OPTIONAL IfcNonNegativeLengthMeasure;
		internal double mTopFlangeSlope = double.NaN;//:	OPTIONAL IfcPlaneAngleMeasure;
		internal double mCentreOfGravityInY = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure IFC4 deleted

		public double BottomFlangeWidth { get { return mBottomFlangeWidth; } set { mBottomFlangeWidth = value; } }
		public double OverallDepth { get { return mOverallDepth; } set { mOverallDepth = value; } }
		public double WebThickness { get { return mWebThickness; } set { mWebThickness = value; } }
		public double BottomFlangeThickness { get { return mBottomFlangeThickness; } set { mBottomFlangeThickness = value; } }
		public double BottomFlangeFilletRadius { get { return mBottomFlangeFilletRadius; } set { mBottomFlangeFilletRadius = value; } }
		public double TopFlangeWidth { get { return mTopFlangeWidth; } set { mTopFlangeWidth = value; } }
		public double TopFlangeThickness { get { return mTopFlangeThickness; } set { mTopFlangeThickness = value; } }
		public double TopFlangeFilletRadius { get { return mTopFlangeFilletRadius; } set { mTopFlangeFilletRadius = value; } }
		public double BottomFlangeEdgeRadius { get { return mBottomFlangeEdgeRadius; } set { mBottomFlangeEdgeRadius = value; } }
		public double BottomFlangeSlope { get { return mBottomFlangeSlope; } set { mBottomFlangeSlope = value; } }
		public double TopFlangeEdgeRadius { get { return mTopFlangeEdgeRadius; } set { mTopFlangeEdgeRadius = value; } }
		public double TopFlangeSlope { get { return mTopFlangeSlope; } set { mTopFlangeSlope = value; } }

		internal IfcAsymmetricIShapeProfileDef() : base() { }
		internal IfcAsymmetricIShapeProfileDef(DatabaseIfc db, IfcAsymmetricIShapeProfileDef p, DuplicateOptions options) : base(db, p, options)
		{
			mBottomFlangeWidth = p.mBottomFlangeWidth;
			mOverallDepth = p.mOverallDepth;
			mWebThickness = p.mWebThickness;
			mBottomFlangeThickness = p.mBottomFlangeThickness;
			mBottomFlangeFilletRadius = p.mBottomFlangeFilletRadius;
			mTopFlangeWidth = p.mTopFlangeWidth;
			mTopFlangeThickness = p.mTopFlangeThickness;
			mTopFlangeFilletRadius = p.mTopFlangeFilletRadius;
			mBottomFlangeEdgeRadius = p.mBottomFlangeEdgeRadius;
			mBottomFlangeSlope = p.mBottomFlangeSlope;
			mTopFlangeEdgeRadius = p.mTopFlangeEdgeRadius;
			mTopFlangeSlope = p.mTopFlangeSlope;
			mCentreOfGravityInY = p.mCentreOfGravityInY;
		}
		public IfcAsymmetricIShapeProfileDef(DatabaseIfc db, string name, double bottomFlangeWidth, double overallDepth, double webThickness, double bottomFlangeThickness, double topFlangeWidth)
			: base(db, name)
		{
			BottomFlangeWidth = bottomFlangeWidth;
			OverallDepth = overallDepth;
			WebThickness = webThickness;
			BottomFlangeThickness = bottomFlangeThickness;
			TopFlangeWidth = topFlangeWidth;
		}
	}
	[Serializable]
	public partial class IfcAudioVisualAppliance : IfcFlowTerminal //IFC4
	{
		private IfcAudioVisualApplianceTypeEnum mPredefinedType = IfcAudioVisualApplianceTypeEnum.NOTDEFINED;// OPTIONAL : IfcAudioVisualApplianceTypeEnum;
		public IfcAudioVisualApplianceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAudioVisualApplianceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAudioVisualAppliance() : base() { }
		internal IfcAudioVisualAppliance(DatabaseIfc db, IfcAudioVisualAppliance a, DuplicateOptions options) : base(db,a, options) { PredefinedType = a.PredefinedType; }
		public IfcAudioVisualAppliance(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcAudioVisualApplianceType : IfcFlowTerminalType
	{
		private IfcAudioVisualApplianceTypeEnum mPredefinedType = IfcAudioVisualApplianceTypeEnum.NOTDEFINED;// : IfcAudioVisualApplianceBoxTypeEnum; 
		public IfcAudioVisualApplianceTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcAudioVisualApplianceTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcAudioVisualApplianceType() : base() { }
		internal IfcAudioVisualApplianceType(DatabaseIfc db, IfcAudioVisualApplianceType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcAudioVisualApplianceType(DatabaseIfc db, string name, IfcAudioVisualApplianceTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcAxis1Placement : IfcPlacement
	{
		private IfcDirection mAxis;//  : OPTIONAL IfcDirection

		public IfcCartesianPoint Location { get { return mLocation as IfcCartesianPoint; } set { mLocation = value; } }
		public IfcDirection Axis { get { return mAxis; } set { mAxis = value; } }
		
		internal IfcAxis1Placement() : base() { }
		internal IfcAxis1Placement(DatabaseIfc db, IfcAxis1Placement p, DuplicateOptions options)
			: base(db, p, options) { if(p.mAxis != null) Axis = db.Factory.Duplicate(p.Axis, options); }
		public IfcAxis1Placement(DatabaseIfc db) : base(db) { }
		public IfcAxis1Placement(IfcCartesianPoint location) : base(location) { }
		public IfcAxis1Placement(IfcDirection axis) : base(axis.mDatabase) { Axis = axis; }
		public IfcAxis1Placement(IfcCartesianPoint location, IfcDirection axis) : base(location) { Axis = axis; }
	}	
	public partial interface IfcAxis2Placement : IBaseClassIfc { bool IsXYPlane(double tol); } //SELECT ( IfcAxis2Placement2D, IfcAxis2Placement3D);
	[Serializable]
	public partial class IfcAxis2Placement2D : IfcPlacement, IfcAxis2Placement
	{ 
		private IfcDirection mRefDirection;// : OPTIONAL IfcDirection;

		public IfcCartesianPoint Location { get { return mLocation as IfcCartesianPoint; } set { mLocation = value; } }
		public IfcDirection RefDirection { get { return mRefDirection; } set { mRefDirection = value; } }
		
		internal IfcAxis2Placement2D() : base() { }
		internal IfcAxis2Placement2D(DatabaseIfc db, IfcAxis2Placement2D p, DuplicateOptions options) : base(db, p, options)
		{
			if (p.mRefDirection != null)
				RefDirection = db.Factory.DuplicateDirection(p.RefDirection, options);
		}
		public IfcAxis2Placement2D(DatabaseIfc db) : base(db.Factory.Origin2d) { }
		public IfcAxis2Placement2D(IfcCartesianPoint location) : base(location) { }
		
		public override bool IsXYPlane(double tol) { return base.IsXYPlane(tol) && (mRefDirection == null || RefDirection.isXAxis); } 
	
		public void RemapToParameter(double pointX, double pointY, out double s, out double t)
		{
			IfcCartesianPoint location = Location;
			double vx = pointX - location.CoordinateX, vy = pointY - location.CoordinateY;
			IfcDirection refDirection = RefDirection;
			double refX = refDirection.DirectionRatioX, refY = refDirection.DirectionRatioY;
			double refLength = Math.Sqrt(refX * refX + refY * refY);
			refX = refX / refLength;
			refY = refY / refLength;

			s = (refX * vx) + (refY * vy);
			t = (refX * -vy) + (refY * vx); 
		}
	}
	[Serializable]
	public partial class IfcAxis2Placement3D : IfcPlacement, IfcAxis2Placement
	{
		private IfcDirection mAxis = null;// : OPTIONAL IfcDirection;
		private IfcDirection mRefDirection = null;// : OPTIONAL IfcDirection; 

		public IfcCartesianPoint Location { get { return mLocation as IfcCartesianPoint; } set { mLocation = value; } }
		public IfcDirection Axis
		{
			get { return mAxis; }
			set
			{
				mAxis = value;
				if (mAxis != null)
				{
					mAxis.mAsAxisPlacements3D.Add(this);
					if (mRefDirection == null && mDatabase != null)
						RefDirection = (Math.Abs(value.DirectionRatioX - 1) < 1e-3 ? mDatabase.Factory.YAxis : mDatabase.Factory.XAxis);
				}
			}
		}
		public IfcDirection RefDirection
		{
			get { return mRefDirection; }
			set
			{
				mRefDirection = value;
				if (mRefDirection != null)
				{
					mRefDirection.mAsRefDirectionPlacements3D.Add(this);
					if (mAxis == null && mDatabase != null)
						Axis = (Math.Abs(value.DirectionRatioZ - 1) < 1e-3 ? mDatabase.Factory.XAxis : mDatabase.Factory.ZAxis);
				}
			}
		}

		internal IfcAxis2Placement3D() : base() { }
		internal IfcAxis2Placement3D(DatabaseIfc db, IfcAxis2Placement3D p, DuplicateOptions options) : base(db, p, options)
		{
			if (p.mAxis != null)
				Axis = db.Factory.DuplicateDirection(p.Axis, options);
			if (p.mRefDirection != null)
				RefDirection = db.Factory.DuplicateDirection(p.RefDirection, options);
		}
		public IfcAxis2Placement3D(IfcCartesianPoint location) : base(location) { }
		public IfcAxis2Placement3D(IfcCartesianPoint location, IfcDirection axis, IfcDirection refDirection) : base(location) { Axis = axis; RefDirection = refDirection; }

		public override bool IsXYPlane(double tol)
		{
			if (mAxis != null && !Axis.isZAxis)
				return false;
			if (mRefDirection != null && !RefDirection.isXAxis)
				return false;
			return base.IsXYPlane(tol);
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcAxis2PlacementLinear : IfcPlacement
	{
		private IfcDirection mAxis = null;// : OPTIONAL IfcDirection;
		private IfcDirection mRefDirection = null;// : OPTIONAL IfcDirection; 

		public IfcPointByDistanceExpression Location { get { return mLocation as IfcPointByDistanceExpression; } set { mLocation = value; } }
		public IfcDirection Axis 
		{ 
			get { return mAxis; } 
			set 
			{ 
				mAxis = value; 
				if(mAxis != null)
					mAxis.mAsAxisPlacementsLinear.Add(this);
			} 
		}
		public IfcDirection RefDirection 
		{ 
			get { return mRefDirection; } 
			set 
			{ 
				mRefDirection = value;
				if (mRefDirection != null)
					mRefDirection.mAsRefDirectionPlacementsLinear.Add(this);
			} 
		}

		internal IfcAxis2PlacementLinear() : base() { }
		internal IfcAxis2PlacementLinear(DatabaseIfc db, IfcAxis2PlacementLinear p, DuplicateOptions options) : base(db, p, options)
		{
			if (p.mAxis != null)
				Axis = db.Factory.Duplicate(p.Axis) as IfcDirection;
			if (p.mRefDirection != null)
				RefDirection = db.Factory.Duplicate(p.RefDirection) as IfcDirection;
		}
		public IfcAxis2PlacementLinear(IfcPointByDistanceExpression location) : base(location) { }
		public IfcAxis2PlacementLinear(IfcPointByDistanceExpression location, IfcDirection axis, IfcDirection refDirection) : base(location) { Axis = axis; RefDirection = refDirection; }
	}
	[Serializable]
	public abstract partial class IfcAxisLateralInclination : IfcGeometricRepresentationItem
	{
		//INVERSE
		private SET<IfcLinearAxisWithInclination> mToLinearAxis = new SET<IfcLinearAxisWithInclination>();
		//INVERSE
		public SET<IfcLinearAxisWithInclination> ToLinearAxis { get { return mToLinearAxis; } }

		protected IfcAxisLateralInclination() : base() { }
		protected IfcAxisLateralInclination(DatabaseIfc db) : base(db) { }
		protected IfcAxisLateralInclination(DatabaseIfc db, IfcAxisLateralInclination axisLateralInclination, DuplicateOptions options) : base(db, axisLateralInclination, options) { }
	}
}
