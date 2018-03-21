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
	public partial class IfcActionRequest : IfcControl
	{
		//internal string mRequestID;// : IfcIdentifier; IFC4 relocated
		internal IfcActionRequest(DatabaseIfc db, IfcActionRequest r, IfcOwnerHistory ownerHistory, bool downStream) : base(db,r, ownerHistory, downStream) { }
		internal IfcActionRequest() : base() { }
	}
	[Serializable]
	public partial class IfcActor : IfcObject // SUPERTYPE OF(IfcOccupant)
	{
		internal int mTheActor;//	 :	IfcActorSelect; 
		//INVERSE
		internal List<IfcRelAssignsToActor> mIsActingUpon = new List<IfcRelAssignsToActor>();// : SET [0:?] OF IfcRelAssignsToActor FOR RelatingActor;

		public IfcActorSelect TheActor { get { return mDatabase[mTheActor] as IfcActorSelect; } set { mTheActor = value.Index; } }
		public ReadOnlyCollection<IfcRelAssignsToActor> IsActingUpon { get { return new ReadOnlyCollection<IfcRelAssignsToActor>( mIsActingUpon); } }

		internal IfcActor() : base() { }
		internal IfcActor(DatabaseIfc db, IfcActor a, IfcOwnerHistory ownerHistory, bool downStream) : base(db,a, ownerHistory, downStream) { TheActor = db.Factory.Duplicate(a.mDatabase[ a.mTheActor]) as IfcActorSelect; }
		public IfcActor(IfcActorSelect a) : base(a.Database) { mTheActor = a.Index; }
	}
	[Serializable]
	public partial class IfcActorRole : BaseClassIfc, IfcResourceObjectSelect
	{
		internal IfcRoleEnum mRole = IfcRoleEnum.NOTDEFINED;// : OPTIONAL IfcRoleEnum
		internal string mUserDefinedRole = "$";// : OPTIONAL IfcLabel
		internal string mDescription = "$";// : OPTIONAL IfcText; 
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public IfcRoleEnum Role { get { return mRole; } set { mRole = value; } }
		public string UserDefinedRole { get { return (mUserDefinedRole == "$" ? "" : ParserIfc.Decode(mUserDefinedRole)); } set { mUserDefinedRole = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		public ReadOnlyCollection<IfcExternalReferenceRelationship> HasExternalReferences { get { return new ReadOnlyCollection<IfcExternalReferenceRelationship>( mHasExternalReferences); } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>(mHasConstraintRelationships); } }

		internal IfcActorRole() : base() { }
		internal IfcActorRole(DatabaseIfc db, IfcActorRole r) : base(db,r) { mRole = r.mRole; mDescription = r.mDescription; mUserDefinedRole = r.mUserDefinedRole; }
		public IfcActorRole(DatabaseIfc db) : base(db) { }
		
		public void AddExternalReferenceRelationship(IfcExternalReferenceRelationship referenceRelationship) { mHasExternalReferences.Add(referenceRelationship); }
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	public interface IfcActorSelect : IBaseClassIfc {  }// IfcOrganization,  IfcPerson,  IfcPersonAndOrganization);
	[Serializable]
	public partial class IfcActuator : IfcDistributionControlElement //IFC4  
	{   
		internal IfcActuatorTypeEnum mPredefinedType = IfcActuatorTypeEnum.NOTDEFINED;
		public IfcActuatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcActuator() : base() { }
		internal IfcActuator(DatabaseIfc db, IfcActuator a, IfcOwnerHistory ownerHistory, bool downStream) : base(db,a, ownerHistory, downStream) { mPredefinedType = a.mPredefinedType; }
		public IfcActuator(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement,representation, system) { }
	}
	[Serializable]
	public partial class IfcActuatorType : IfcDistributionControlElementType
	{
		internal IfcActuatorTypeEnum mPredefinedType = IfcActuatorTypeEnum.NOTDEFINED;// : IfcActuatorTypeEnum; 
		public IfcActuatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcActuatorType() : base() { }
		internal IfcActuatorType(DatabaseIfc db, IfcActuatorType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream)  { mPredefinedType = t.mPredefinedType; }
		internal IfcActuatorType(DatabaseIfc m, string name, IfcActuatorTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public abstract partial class IfcAddress : BaseClassIfc, IfcObjectReferenceSelect   //ABSTRACT SUPERTYPE OF(ONEOF(IfcPostalAddress, IfcTelecomAddress));
	{
		internal IfcAddressTypeEnum mPurpose = IfcAddressTypeEnum.NOTDEFINED;// : OPTIONAL IfcAddressTypeEnum
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal string mUserDefinedPurpose = "$";// : OPTIONAL IfcLabel 

		public IfcAddressTypeEnum Purpose { get { return mPurpose; } set { mPurpose = value; } }
		public string Description { get { return mDescription == "$" ? "" : ParserIfc.Decode(mDescription); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string UserDefinedPurpose { get { return mUserDefinedPurpose == "$" ? "" : ParserIfc.Decode(mUserDefinedPurpose); } set { mUserDefinedPurpose = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		
		internal IfcAddress() : base() { }
		internal IfcAddress(DatabaseIfc db) : base(db) {  }
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
		internal IfcAdvancedBrep(DatabaseIfc db, IfcAdvancedBrep b) : base(db,b) { }
		internal IfcAdvancedBrep(IfcClosedShell s) : base(s) { }
	}
	[Serializable]
	public partial class IfcAdvancedBrepWithVoids : IfcAdvancedBrep
	{
		private List<int> mVoids = new List<int>();// : SET [1:?] OF IfcClosedShell
		public ReadOnlyCollection<IfcClosedShell> Voids { get { return new ReadOnlyCollection<IfcClosedShell>( mVoids.ConvertAll(x => mDatabase[x] as IfcClosedShell)); } }

		internal IfcAdvancedBrepWithVoids() : base() { }
		internal IfcAdvancedBrepWithVoids(DatabaseIfc db, IfcAdvancedBrepWithVoids b) : base(db,b) { mVoids = b.Voids.ToList().ConvertAll(x=> db.Factory.Duplicate(x).mIndex); }
	
		internal void addVoid(IfcClosedShell shell) { mVoids.Add(shell.mIndex); }
	}
	[Serializable]
	public partial class IfcAdvancedFace : IfcFaceSurface
	{
		internal IfcAdvancedFace() : base() { }
		internal IfcAdvancedFace(DatabaseIfc db, IfcAdvancedFace f) : base(db,f) { }
		public IfcAdvancedFace(IfcFaceOuterBound bound, IfcSurface f, bool sense) : base(bound, f, sense) { }
		public IfcAdvancedFace(List<IfcFaceBound> bounds, IfcSurface f, bool sense) : base(bounds, f, sense) { }
		public IfcAdvancedFace(IfcFaceOuterBound outer, IfcFaceBound inner, IfcSurface f, bool sense) : base(outer,inner, f, sense) { }
	}
	[Serializable]
	public partial class IfcAirTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcAirTerminalTypeEnum mPredefinedType = IfcAirTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcAirTerminalTypeEnum;
		public IfcAirTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirTerminal() : base() { }
		internal IfcAirTerminal(DatabaseIfc db, IfcAirTerminal t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcAirTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcAirTerminalBox : IfcFlowController //IFC4
	{
		internal IfcAirTerminalBoxTypeEnum mPredefinedType = IfcAirTerminalBoxTypeEnum.NOTDEFINED;// OPTIONAL : IfcAirTerminalBoxTypeEnum;
		public IfcAirTerminalBoxTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirTerminalBox() : base() { }
		internal IfcAirTerminalBox(DatabaseIfc db, IfcAirTerminalBox b, IfcOwnerHistory ownerHistory, bool downStream) : base(db, b, ownerHistory, downStream) { mPredefinedType = b.mPredefinedType; }
		public IfcAirTerminalBox(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcAirTerminalBoxType : IfcFlowControllerType
	{
		internal IfcAirTerminalBoxTypeEnum mPredefinedType = IfcAirTerminalBoxTypeEnum.NOTDEFINED;// : IfcAirTerminalBoxTypeEnum; 
		public IfcAirTerminalBoxTypeEnum PredefinedType { get { return mPredefinedType;} set { mPredefinedType = value; } }

		internal IfcAirTerminalBoxType() : base() { }
		internal IfcAirTerminalBoxType(DatabaseIfc db, IfcAirTerminalBoxType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		internal IfcAirTerminalBoxType(DatabaseIfc m, string name, IfcAirTerminalBoxTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcAirTerminalType : IfcFlowTerminalType
	{
		internal IfcAirTerminalTypeEnum mPredefinedType = IfcAirTerminalTypeEnum.NOTDEFINED;// : IfcAirTerminalBoxTypeEnum; 
		public IfcAirTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirTerminalType() : base() { }
		internal IfcAirTerminalType(DatabaseIfc db, IfcAirTerminalType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcAirTerminalType(DatabaseIfc m, string name, IfcAirTerminalTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcAirToAirHeatRecovery : IfcEnergyConversionDevice //IFC4  
	{
		internal IfcAirToAirHeatRecoveryTypeEnum mPredefinedType = IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED;
		public IfcAirToAirHeatRecoveryTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirToAirHeatRecovery() : base() { }
		internal IfcAirToAirHeatRecovery(DatabaseIfc db, IfcAirToAirHeatRecovery a, IfcOwnerHistory ownerHistory, bool downStream) : base(db, a, ownerHistory, downStream) { mPredefinedType = a.mPredefinedType; }
		public IfcAirToAirHeatRecovery(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcAirToAirHeatRecoveryType : IfcEnergyConversionDeviceType
	{
		internal IfcAirToAirHeatRecoveryTypeEnum mPredefinedType = IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED;// : IfcAirToAirHeatRecoveryTypeEnum; 
		public IfcAirToAirHeatRecoveryTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirToAirHeatRecoveryType() : base() { }
		internal IfcAirToAirHeatRecoveryType(DatabaseIfc db, IfcAirToAirHeatRecoveryType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		internal IfcAirToAirHeatRecoveryType(DatabaseIfc m, string name, IfcAirToAirHeatRecoveryTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcAlarm : IfcDistributionControlElement //IFC4  
	{
		internal IfcAlarmTypeEnum mPredefinedType = IfcAlarmTypeEnum.NOTDEFINED;
		public IfcAlarmTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAlarm() : base() { }
		internal IfcAlarm(DatabaseIfc db, IfcAlarm a, IfcOwnerHistory ownerHistory, bool downStream) : base(db, a, ownerHistory, downStream) { mPredefinedType = a.mPredefinedType; }
		public IfcAlarm(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcAlarmType : IfcDistributionControlElementType
	{
		internal IfcAlarmTypeEnum mPredefinedType = IfcAlarmTypeEnum.NOTDEFINED;// : IfcAlarmTypeEnum; 
		public IfcAlarmTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAlarmType() : base() { }
		internal IfcAlarmType(DatabaseIfc db, IfcAlarmType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		internal IfcAlarmType(DatabaseIfc m, string name, IfcAlarmTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcAngularDimension : IfcDimensionCurveDirectedCallout //IFC4 depreceated
	{
		internal IfcAngularDimension() : base() { }
	}
	[Serializable]
	public partial class IfcAnnotation : IfcProduct
	{    //INVERSE
		[NonSerialized] internal IfcRelContainedInSpatialStructure mContainedInStructure = null;

		internal IfcAnnotation() : base() { }
		internal IfcAnnotation(DatabaseIfc db, IfcAnnotation a, IfcOwnerHistory ownerHistory, bool downStream) : base(db, a, ownerHistory, downStream) { }
		public IfcAnnotation(DatabaseIfc db) : base(db) { }
		public IfcAnnotation(IfcProduct host) : base(host.mDatabase) { host.AddElement(this); }

		internal override void detachFromHost()
		{
			base.detachFromHost();
			if (mContainedInStructure != null)
			{
				mContainedInStructure.mRelatedElements.Remove(mIndex);
				mContainedInStructure = null;
			}
		}
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public abstract partial class IfcAnnotationCurveOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		protected IfcAnnotationCurveOccurrence() : base() { }
	}
	[Serializable]
	public partial class IfcAnnotationFillArea : IfcGeometricRepresentationItem  
	{
		internal int mOuterBoundary;// : IfcCurve;
		internal List<int> mInnerBoundaries = new List<int>();// OPTIONAL SET [1:?] OF IfcCurve; 

		public IfcCurve OuterBoundary { get { return mDatabase[mOuterBoundary] as IfcCurve; } set { mOuterBoundary = value.mIndex; } }
		public ReadOnlyCollection<IfcCurve> InnerBoundaries { get { return new ReadOnlyCollection<IfcCurve>( mInnerBoundaries.ConvertAll(x => mDatabase[x] as IfcCurve)); } }

		internal IfcAnnotationFillArea() : base() { }
		internal IfcAnnotationFillArea(DatabaseIfc db, IfcAnnotationFillArea a) : base(db,a) { OuterBoundary = db.Factory.Duplicate(a.OuterBoundary) as IfcCurve; a.InnerBoundaries.ToList().ForEach(x=> AddInner( db.Factory.Duplicate(x) as IfcCurve)); }
		public IfcAnnotationFillArea(IfcCurve outerBoundary) : base(outerBoundary.mDatabase) { OuterBoundary = outerBoundary; }
		public IfcAnnotationFillArea(IfcCurve outerBoundary, List<IfcCurve> innerBoundaries) : this(outerBoundary) { innerBoundaries.ForEach(x=>AddInner(x)); }
		
		internal void AddInner(IfcCurve boundary) { mInnerBoundaries.Add(boundary.mIndex); }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcAnnotationFillAreaOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		internal int mFillStyleTarget;// : OPTIONAL IfcPoint;
		internal IfcGlobalOrLocalEnum  mGlobalOrLocal;// : OPTIONAL IfcGlobalOrLocalEnum; 
		internal IfcAnnotationFillAreaOccurrence(DatabaseIfc db, IfcAnnotationFillAreaOccurrence f) : base(db,f) { }
		internal IfcAnnotationFillAreaOccurrence() : base() { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public abstract partial class IfcAnnotationOccurrence : IfcStyledItem //DEPRECEATED IFC4
	{
		protected IfcAnnotationOccurrence(DatabaseIfc db, IfcAnnotationOccurrence o) : base(db,o) { }
		protected IfcAnnotationOccurrence() : base() { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcAnnotationSurface : IfcGeometricRepresentationItem //DEPRECEATED IFC4
	{
		internal int mItem;// : IfcGeometricRepresentationItem;
		internal int mTextureCoordinates;// OPTIONAL IfcTextureCoordinate;

		public IfcGeometricRepresentationItem Item { get { return mDatabase[mItem] as IfcGeometricRepresentationItem; } set { mItem = value.mIndex; } }
		public IfcTextureCoordinate TextureCoordinates { get { return mDatabase[mTextureCoordinates] as IfcTextureCoordinate; } set { mTextureCoordinates = value.mIndex; } }

		internal IfcAnnotationSurface() : base() { } 
		internal IfcAnnotationSurface(DatabaseIfc db, IfcAnnotationSurface a) : base(db, a) { Item = db.Factory.Duplicate(a.Item) as IfcGeometricRepresentationItem; if(a.mTextureCoordinates > 0) TextureCoordinates = db.Factory.Duplicate(a.TextureCoordinates) as IfcTextureCoordinate; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcAnnotationSurfaceOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		internal IfcAnnotationSurfaceOccurrence(DatabaseIfc db, IfcAnnotationSurfaceOccurrence o) : base(db,o) { }
		internal IfcAnnotationSurfaceOccurrence() : base() { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcAnnotationSymbolOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		internal IfcAnnotationSymbolOccurrence() : base() { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcAnnotationTextOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		internal IfcAnnotationTextOccurrence() : base() { }
		internal IfcAnnotationTextOccurrence(DatabaseIfc db, IfcAnnotationTextOccurrence o) : base(db,o) { }
	}
	[Serializable]
	public partial class IfcApplication : BaseClassIfc, NamedObjectIfc
	{
		internal IfcOrganization mApplicationDeveloper = null;// : IfcOrganization;
		internal string mVersion;// : IfcLabel;
		private string mApplicationFullName;// : IfcLabel;
		internal string mApplicationIdentifier;// : IfcIdentifier; 
		
		public IfcOrganization ApplicationDeveloper { get { return mApplicationDeveloper; } set { mApplicationDeveloper = value; } }
		public string Version { get { return mVersion; } set { mVersion = ParserIfc.Encode(value); } }
		public string ApplicationFullName { get { return ParserIfc.Decode(mApplicationFullName); } set { mApplicationFullName =  ParserIfc.Encode(value); } }
		public string ApplicationIdentifier { get { return ParserIfc.Decode(mApplicationIdentifier); } set { mApplicationIdentifier =  ParserIfc.Encode(value); } }

		public string Name { get { return ApplicationFullName; } set { ApplicationFullName = value; } }

		internal IfcApplication() : base() { }
		internal IfcApplication(DatabaseIfc db) : base(db)
		{
			ApplicationDeveloper = new IfcOrganization(db, "Geometry Gym Pty Ltd");
			try
			{
				mVersion =  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
			catch (Exception) { mVersion = "Unknown"; }
			mApplicationFullName = db.Factory.ApplicationFullName;
			mApplicationIdentifier = db.Factory.ApplicationIdentifier;

		}
		internal IfcApplication(DatabaseIfc db, IfcApplication a) : base(db,a)
		{
			ApplicationDeveloper = db.Factory.Duplicate(a.ApplicationDeveloper) as IfcOrganization;
			mVersion = a.mVersion;
			mApplicationFullName = a.mApplicationFullName;
			mApplicationIdentifier = a.mApplicationIdentifier;
		}
		public IfcApplication(IfcOrganization developer, string version, string fullName, string identifier) :base(developer.mDatabase) { ApplicationDeveloper = developer; Version = version; ApplicationFullName = fullName; ApplicationIdentifier = identifier; }


	}
	[Serializable]
	public partial class IfcAppliedValue : BaseClassIfc, IfcMetricValueSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect, NamedObjectIfc
	{  // SUPERTYPE OF(IfcCostValue);
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal int mAppliedValueIndex = 0;// : OPTIONAL IfcAppliedValueSelect
		internal IfcValue mAppliedValueValue = null;
		internal int mUnitBasis;// : OPTIONAL IfcMeasureWithUnit;
		internal DateTime mApplicableDate = DateTime.MinValue;// : OPTIONAL IfcDateTimeSelect; 4 IfcDate
		internal DateTime mFixedUntilDate = DateTime.MinValue;// : OPTIONAL IfcDateTimeSelect; 4 IfcDate
		private int mSSApplicableDate = 0;
		private int mSSFixedUntilDate = 0;
		internal string mCategory = "$";// : OPTIONAL IfcLabel; IFC4
		internal string mCondition = "$";// : OPTIONAL IfcLabel; IFC4
		internal IfcArithmeticOperatorEnum mArithmeticOperator = IfcArithmeticOperatorEnum.NONE;//	 :	OPTIONAL IfcArithmeticOperatorEnum; IFC4 
		internal List<int> mComponents = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcAppliedValue; IFC4
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg
		internal List<IfcAppliedValue> mComponentFor = new List<IfcAppliedValue>(); //gg

		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } } 
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcAppliedValueSelect AppliedValue { get { return mDatabase[mAppliedValueIndex] as IfcAppliedValueSelect; } set { mAppliedValueIndex = (value == null ? 0 : value.Index); } }
		public IfcValue Value { get { return mAppliedValueValue; } set { mAppliedValueValue = value; } }
		public IfcMeasureWithUnit UnitBasis { get { return mDatabase[mUnitBasis] as IfcMeasureWithUnit; } set { mUnitBasis = (value == null ? 0 : value.mIndex); } }
		public DateTime ApplicableDate { get { return mApplicableDate; } set { mApplicableDate = value; } }
		public DateTime FixedUntilDate { get { return mFixedUntilDate; } set { mFixedUntilDate = value; } }
		public string Category { get { return (mCategory == "$" ? "" : ParserIfc.Decode(mCategory)); } set { mCategory = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Condition { get { return (mCondition == "$" ? "" : ParserIfc.Decode(mCondition)); } set { mCondition = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcArithmeticOperatorEnum ArithmeticOperator { get { return mArithmeticOperator; } set { mArithmeticOperator = value; } }
		public ReadOnlyCollection<IfcAppliedValue> Components { get { return new ReadOnlyCollection<IfcAppliedValue>( mComponents.ConvertAll(x => mDatabase[x] as IfcAppliedValue)); } }
		public ReadOnlyCollection<IfcExternalReferenceRelationship> HasExternalReferences { get { return new ReadOnlyCollection<IfcExternalReferenceRelationship>( mHasExternalReferences); } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>(mHasConstraintRelationships); } }

		internal IfcAppliedValue() : base() { }
		public IfcAppliedValue(DatabaseIfc db) : base(db) { }
		public IfcAppliedValue(IfcAppliedValueSelect appliedValue) : base(appliedValue.Database) { AppliedValue = appliedValue; }
		public IfcAppliedValue(DatabaseIfc db, IfcValue value) : base(db) { Value = value; }
		public IfcAppliedValue(IfcAppliedValue component1, IfcArithmeticOperatorEnum op,IfcAppliedValue component2) : base(component1.mDatabase) { addComponent(component1); addComponent(component2); mArithmeticOperator = op; } 
		internal IfcAppliedValue(DatabaseIfc db, IfcAppliedValue v) : base(db,v)
		{
			mName = v.mName; mDescription = v.mDescription; mAppliedValueIndex = v.mAppliedValueIndex; mAppliedValueValue = v.mAppliedValueValue;
			UnitBasis = db.Factory.Duplicate(v.UnitBasis) as IfcMeasureWithUnit;
			mApplicableDate = v.mApplicableDate; mFixedUntilDate = v.mFixedUntilDate; mCategory = v.mCategory; mCondition = v.mCondition; mArithmeticOperator = v.mArithmeticOperator;
			v.Components.ToList().ForEach(x=>addComponent(db.Factory.Duplicate(x) as IfcAppliedValue));
		}
	
		public override bool Destruct(bool children)
		{
			if (children)
			{
				if (mAppliedValueIndex > 0)
					mDatabase[mAppliedValueIndex].Destruct(children);
				for (int icounter = 0; icounter < mComponents.Count; icounter++)
				{
					BaseClassIfc bc = mDatabase[mComponents[icounter]];
					if (bc != null)
						bc.Destruct(true);
				}
			}
			return base.Destruct(children);
		}

		public void AddExternalReferenceRelationship(IfcExternalReferenceRelationship referenceRelationship) { mHasExternalReferences.Add(referenceRelationship); }
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
		internal void addComponent(IfcAppliedValue value) { mComponents.Add(value.mIndex); value.mComponentFor.Add(this); }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcAppliedValueRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		internal int mComponentOfTotal;// : IfcAppliedValue;
		internal List< int> mComponents = new List<int>();// : SET [1:?] OF IfcAppliedValue;
		internal IfcArithmeticOperatorEnum mArithmeticOperator;// : IfcArithmeticOperatorEnum;
		internal string mName;// : OPTIONAL IfcLabel;
		internal string mDescription;// : OPTIONAL IfcText 
		internal IfcAppliedValueRelationship() : base() { }
		//internal IfcAppliedValueRelationship(IfcAppliedValueRelationship o) : base()
		//{
		//	mComponentOfTotal = o.mComponentOfTotal;
		//	mComponents = new List<int>(o.mComponents.ToArray());
		//	mArithmeticOperator = o.mArithmeticOperator;
		//	mName = o.mName;
		//	mDescription = o.mDescription;
		//}
	}
	public interface IfcAppliedValueSelect : IBaseClassIfc  //	IfcMeasureWithUnit, IfcValue, IfcReference); IFC2x3 //IfcRatioMeasure, IfcMeasureWithUnit, IfcMonetaryMeasure); 
	{
		//List<IfcAppliedValue> AppliedValueFor { get; }
	}
	[Serializable]
	public partial class IfcApproval : BaseClassIfc, IfcResourceObjectSelect
	{
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal int mApprovalDateTime;// : IfcDateTimeSelect;
		internal string mApprovalStatus = "$";// : OPTIONAL IfcLabel;
		internal string mApprovalLevel = "$";// : OPTIONAL IfcLabel;
		internal string mApprovalQualifier = "$";// : OPTIONAL IfcText;
		internal string mName = "$";// :OPTIONAL IfcLabel;
		internal string mIdentifier = "$";// : OPTIONAL IfcIdentifier;
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public ReadOnlyCollection<IfcExternalReferenceRelationship> HasExternalReferences { get { return new ReadOnlyCollection<IfcExternalReferenceRelationship>(mHasExternalReferences); } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>(mHasConstraintRelationships); } }

		internal IfcApproval() : base() { }
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
		
		public void AddExternalReferenceRelationship(IfcExternalReferenceRelationship referenceRelationship) { mHasExternalReferences.Add(referenceRelationship); }
		public void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship) { mHasConstraintRelationships.Add(constraintRelationship); }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcApprovalActorRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		internal int mActor;// : IfcActorSelect;
		internal int mApproval;// : IfcApproval;
		internal int mRole;// : IfcActorRole; 
		internal IfcApprovalActorRelationship() : base() { }
		//internal IfcApprovalActorRelationship(IfcApprovalActorRelationship o) : base() { mActor = o.mActor; mApproval = o.mApproval; mRole = o.mRole; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcApprovalPropertyRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		internal List<int> mApprovedProperties = new List<int>();// : SET [1:?] OF IfcProperty;
		internal int mApproval;// : IfcApproval; 
		internal IfcApprovalPropertyRelationship() : base() { }
		//internal IfcApprovalPropertyRelationship(IfcApprovalPropertyRelationship o) : base() { mApprovedProperties = new List<int>(o.mApprovedProperties.ToArray()); mApproval = o.mApproval; }
	}
	[Serializable]
	public partial class IfcApprovalRelationship : IfcResourceLevelRelationship //IFC4Change
	{
		internal int mRelatedApproval;// : IfcApproval;
		internal int mRelatingApproval;// : IfcApproval; 
		internal IfcApprovalRelationship() : base() { }
	//	internal IfcApprovalRelationship(IfcApprovalRelationship o) : base(o) { mRelatedApproval = o.mRelatedApproval; mRelatingApproval = o.mRelatingApproval;  }
	}
	[Serializable]
	public partial class IfcArbitraryClosedProfileDef : IfcProfileDef //SUPERTYPE OF(IfcArbitraryProfileDefWithVoids)
	{
		private int mOuterCurve;// : IfcBoundedCurve
		public IfcBoundedCurve OuterCurve { get { return mDatabase[mOuterCurve] as IfcBoundedCurve; } set { mOuterCurve = value.mIndex; } }

		internal IfcArbitraryClosedProfileDef() : base() { }
		internal IfcArbitraryClosedProfileDef(DatabaseIfc db, IfcArbitraryClosedProfileDef p) : base(db, p) { OuterCurve = db.Factory.Duplicate(p.OuterCurve) as IfcBoundedCurve; }
		public IfcArbitraryClosedProfileDef(string name, IfcBoundedCurve boundedCurve) : base(boundedCurve.mDatabase,name) { mOuterCurve = boundedCurve.mIndex; }//if (string.Compare(getKW, mKW) == 0) mModel.mArbProfiles.Add(this); }

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			OuterCurve.changeSchema(schema);
		}
	}
	[Serializable]
	public partial class IfcArbitraryOpenProfileDef : IfcProfileDef //	SUPERTYPE OF(IfcCenterLineProfileDef)
	{
		private int mCurve;// : IfcBoundedCurve
		public IfcBoundedCurve Curve { get { return mDatabase[mCurve] as IfcBoundedCurve; } set { mCurve = value.mIndex; } }

		internal IfcArbitraryOpenProfileDef() : base() { }
		internal IfcArbitraryOpenProfileDef(DatabaseIfc db, IfcArbitraryOpenProfileDef p) : base(db, p) { Curve = db.Factory.Duplicate(p.Curve) as IfcBoundedCurve; }
		public IfcArbitraryOpenProfileDef(string name, IfcBoundedCurve boundedCurve) : base(boundedCurve.mDatabase,name) { mCurve = boundedCurve.mIndex; }
	}
	[Serializable]
	public partial class IfcArbitraryProfileDefWithVoids : IfcArbitraryClosedProfileDef
	{
		private List<int> mInnerCurves = new List<int>();// : SET [1:?] OF IfcCurve; 
		public ReadOnlyCollection<IfcCurve> InnerCurves { get { return new ReadOnlyCollection<IfcCurve>(mInnerCurves.ConvertAll(x => mDatabase[x] as IfcCurve)); } }

		internal IfcArbitraryProfileDefWithVoids() : base() { }
		internal IfcArbitraryProfileDefWithVoids(DatabaseIfc db, IfcArbitraryProfileDefWithVoids p) : base(db, p) { p.InnerCurves.ToList().ForEach(x => addVoid( db.Factory.Duplicate(x) as IfcCurve)); }
		public IfcArbitraryProfileDefWithVoids(string name, IfcBoundedCurve perim, IfcCurve inner) : base(name, perim) { mInnerCurves.Add(inner.mIndex); }
		public IfcArbitraryProfileDefWithVoids(string name, IfcBoundedCurve perim, List<IfcCurve> inner) : base(name, perim) { inner.ForEach(x => addVoid(x)); }
		
		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			foreach (IfcCurve curve in InnerCurves)
				curve.changeSchema(schema);
		}
		internal void addVoid(IfcCurve inner) { mInnerCurves.Add(inner.mIndex); }
	}
	[Serializable]
	public partial class IfcArcIndex : IfcSegmentIndexSelect
	{
		internal int mA, mB, mC;
		public IfcArcIndex(int a, int b, int c) { mA = a; mB = b; mC = c; }
		public override string ToString() { return "IFCARCINDEX((" + mA + "," + mB + "," + mC + "))"; }
	}
	[Serializable]
	public partial class IfcAsset : IfcGroup
	{
		internal string mAssetID;// : IfcIdentifier;
		internal int mOriginalValue;// : IfcCostValue;
		internal int mCurrentValue;// : IfcCostValue;
		internal int mTotalReplacementCost;// : IfcCostValue;
		internal int mOwner;// : IfcActorSelect;
		internal int mUser;// : IfcActorSelect;
		internal int mResponsiblePerson;// : IfcPerson;
		internal string mIncorporationDate = ""; // : IfcDate 
		internal int mIncorporationDateSS;// : IfcDate Ifc2x3 IfcCalendarDate;
		internal int mDepreciatedValue;// : IfcCostValue; 

		public string AssetID { get { return (mAssetID == "$" ? "" : ParserIfc.Decode(mAssetID)); } set { mAssetID = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcCostValue OriginalValue { get { return mDatabase[mOriginalValue] as IfcCostValue; } set { mOriginalValue = value.mIndex; } } 
		public IfcCostValue CurrentValue { get { return mDatabase[mCurrentValue] as IfcCostValue; } set { mCurrentValue = value.mIndex; } } 
		public IfcCostValue TotalReplacementCost { get { return mDatabase[mTotalReplacementCost] as IfcCostValue; } set { mTotalReplacementCost = value.mIndex; } } 
		public IfcActorSelect Owner { get { return mDatabase[mOwner] as IfcActorSelect; } set { mOwner = value.Index; } }
		public IfcActorSelect User { get { return mDatabase[mUser] as IfcActorSelect; } set { mUser = value.Index; } }
		public IfcPerson ResponsiblePerson { get { return mDatabase[mResponsiblePerson] as IfcPerson; } set { mResponsiblePerson = value.mIndex; } }
		//public  IncorporationDate
		public IfcCostValue DepreciatedValue { get { return mDatabase[mDepreciatedValue] as IfcCostValue; } set { mDepreciatedValue = value.mIndex; } } 

		
		internal IfcAsset() : base() { }
		internal IfcAsset(DatabaseIfc db, IfcAsset a, IfcOwnerHistory ownerHistory, bool downStream) : base(db, a, ownerHistory, downStream)
		{
			mAssetID = a.mAssetID;
			OriginalValue = db.Factory.Duplicate(a.OriginalValue) as IfcCostValue;
			CurrentValue = db.Factory.Duplicate(a.CurrentValue) as IfcCostValue;
			TotalReplacementCost = db.Factory.Duplicate(a.TotalReplacementCost) as IfcCostValue;
			Owner = db.Factory.Duplicate(a.mDatabase[a.mOwner]) as IfcActorSelect;
			User = db.Factory.Duplicate(a.mDatabase[a.mUser]) as IfcActorSelect;
			ResponsiblePerson = db.Factory.Duplicate(a.ResponsiblePerson) as IfcPerson;
			mIncorporationDate = a.mIncorporationDate;
			if(a.mIncorporationDateSS > 0)
				mIncorporationDateSS = db.Factory.Duplicate(a.mDatabase[ a.mIncorporationDateSS]).mIndex;

			DepreciatedValue =  db.Factory.Duplicate(a.DepreciatedValue) as IfcCostValue;
		}
		internal IfcAsset(DatabaseIfc m, string name) : base(m,name) { }
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
		internal double mCentreOfGravityInY;// : OPTIONAL IfcPositiveLengthMeasure IFC4 deleted
		internal IfcAsymmetricIShapeProfileDef() : base() { }
		internal IfcAsymmetricIShapeProfileDef(DatabaseIfc db, IfcAsymmetricIShapeProfileDef p) : base(db, p)
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
	}
	[Serializable]
	public partial class IfcAudioVisualAppliance : IfcFlowTerminal //IFC4
	{
		internal IfcAudioVisualApplianceTypeEnum mPredefinedType = IfcAudioVisualApplianceTypeEnum.NOTDEFINED;// OPTIONAL : IfcAudioVisualApplianceTypeEnum;
		public IfcAudioVisualApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAudioVisualAppliance() : base() { }
		internal IfcAudioVisualAppliance(DatabaseIfc db, IfcAudioVisualAppliance a, IfcOwnerHistory ownerHistory, bool downStream) : base(db,a, ownerHistory, downStream) { mPredefinedType = a.mPredefinedType; }
	}
	[Serializable]
	public partial class IfcAudioVisualApplianceType : IfcFlowTerminalType
	{
		internal IfcAudioVisualApplianceTypeEnum mPredefinedType = IfcAudioVisualApplianceTypeEnum.NOTDEFINED;// : IfcAudioVisualApplianceBoxTypeEnum; 
		public IfcAudioVisualApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAudioVisualApplianceType() : base() { }
		internal IfcAudioVisualApplianceType(DatabaseIfc db, IfcAudioVisualApplianceType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		internal IfcAudioVisualApplianceType(DatabaseIfc m, string name, IfcAudioVisualApplianceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcAxis1Placement : IfcPlacement
	{
		private int mAxis;//  : OPTIONAL IfcDirection
		public IfcDirection Axis { get { return (mAxis > 0 ? mDatabase[mAxis] as IfcDirection : null); } set { mAxis = (value == null ? 0 : value.mIndex); } }
		
		internal IfcAxis1Placement() : base() { }
		public IfcAxis1Placement(DatabaseIfc db) : base(db) { }
		public IfcAxis1Placement(IfcCartesianPoint location) : base(location) { }
		public IfcAxis1Placement(IfcDirection axis) : base(axis.mDatabase) { Axis = axis; }
		public IfcAxis1Placement(IfcCartesianPoint location, IfcDirection axis) : base(location) { Axis = axis; }
		internal IfcAxis1Placement(DatabaseIfc db, IfcAxis1Placement p) : base(db,p) { if(p.mAxis > 0) Axis = db.Factory.Duplicate( p.Axis) as IfcDirection; }
	}
	public partial interface IfcAxis2Placement : IBaseClassIfc { bool IsXYPlane { get; } } //SELECT ( IfcAxis2Placement2D, IfcAxis2Placement3D);
	[Serializable]
	public partial class IfcAxis2Placement2D : IfcPlacement, IfcAxis2Placement
	{ 
		private int mRefDirection;// : OPTIONAL IfcDirection;
		public IfcDirection RefDirection { get { return mDatabase[mRefDirection] as IfcDirection; } set { mRefDirection = (value == null ? 0 : value.mIndex); } }
		
		internal IfcAxis2Placement2D() : base() { }
		internal IfcAxis2Placement2D(DatabaseIfc db, IfcAxis2Placement2D p) : base(db, p)
		{
			if (p.mRefDirection > 0)
				RefDirection = db.Factory.Duplicate(p.RefDirection) as IfcDirection;
		}
		public IfcAxis2Placement2D(DatabaseIfc db) : base(db.Factory.Origin2d) { }
		public IfcAxis2Placement2D(IfcCartesianPoint location) : base(location) { }
		
		public override bool IsXYPlane { get { return base.IsXYPlane && (mRefDirection == 0 || RefDirection.isXAxis); } }
	}
	[Serializable]
	public partial class IfcAxis2Placement3D : IfcPlacement, IfcAxis2Placement
	{
		private IfcDirection mAxis = null;// : OPTIONAL IfcDirection;
		private IfcDirection mRefDirection = null;// : OPTIONAL IfcDirection; 

		public IfcDirection Axis
		{
			get { return mAxis; }
			set
			{
				mAxis = value;
				if (value != null)
				{
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
				if (value != null)
				{
					if (mAxis == null && mDatabase != null)
						Axis = (Math.Abs(value.DirectionRatioZ - 1) < 1e-3 ? mDatabase.Factory.XAxis : mDatabase.Factory.ZAxis);
				}
			}
		}

		internal IfcAxis2Placement3D() : base() { }
		public IfcAxis2Placement3D(IfcCartesianPoint location) : base(location) { }
		public IfcAxis2Placement3D(IfcCartesianPoint location, IfcDirection axis, IfcDirection refDirection) : base(location) { Axis = axis; RefDirection = refDirection; }
		internal IfcAxis2Placement3D(DatabaseIfc db, IfcAxis2Placement3D p) : base(db, p)
		{
			if (p.mAxis != null)
				Axis = db.Factory.Duplicate(p.Axis) as IfcDirection;
			if (p.mRefDirection != null)
				RefDirection = db.Factory.Duplicate(p.RefDirection) as IfcDirection;
		}
		
		public override bool IsXYPlane
		{
			get
			{
				if (mAxis != null && !Axis.isZAxis)
					return false;
				if (mRefDirection != null && !RefDirection.isXAxis)
					return false;
				return base.IsXYPlane;
			}
		}
	}
}
