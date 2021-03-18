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
using System.Text;
using System.Reflection;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable]
	public partial class IfcEarthworksCut : IfcFeatureElementSubtraction
	{
		private IfcEarthworksCutTypeEnum mPredefinedType = IfcEarthworksCutTypeEnum.NOTDEFINED; //: OPTIONAL IfcEarthworksCutTypeEnum;
		public IfcEarthworksCutTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcEarthworksCut() : base() { }
		public IfcEarthworksCut(DatabaseIfc db) : base(db) { }
		public IfcEarthworksCut(DatabaseIfc db, IfcEarthworksCut earthworksCut, DuplicateOptions options) : base(db, earthworksCut, options) { PredefinedType = earthworksCut.PredefinedType; }
		public IfcEarthworksCut(IfcElement host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcEarthworksElement : IfcBuiltElement
	{
		public IfcEarthworksElement() : base() { }
		public IfcEarthworksElement(DatabaseIfc db) : base(db) { }
		public IfcEarthworksElement(DatabaseIfc db, IfcEarthworksElement earthworksElement, DuplicateOptions options) : base(db, earthworksElement, options) { }
		public IfcEarthworksElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcEarthworksFill : IfcEarthworksElement
	{
		private IfcEarthworksFillTypeEnum mPredefinedType = IfcEarthworksFillTypeEnum.NOTDEFINED; //: OPTIONAL IfcEarthworksFillTypeEnum;
		public IfcEarthworksFillTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcEarthworksFill() : base() { }
		public IfcEarthworksFill(DatabaseIfc db) : base(db) { }
		public IfcEarthworksFill(DatabaseIfc db, IfcEarthworksFill earthworksFill, DuplicateOptions options) : base(db, earthworksFill, options) { PredefinedType = earthworksFill.PredefinedType; }
		public IfcEarthworksFill(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcEdge : IfcTopologicalRepresentationItem //SUPERTYPE OF(ONEOF(IfcEdgeCurve, IfcOrientedEdge, IfcSubedge))
	{
		internal IfcVertex mEdgeStart, mEdgeEnd;// : IfcVertex;
		public IfcVertex EdgeStart { get { return mEdgeStart; } set { if (mEdgeStart != null) mEdgeStart.mAttachedEdges.Remove(this); mEdgeStart = value; if(value != null) mEdgeStart.mAttachedEdges.Add(this); } }
		public IfcVertex EdgeEnd { get { return mEdgeEnd; } set { if (mEdgeEnd != null) mEdgeEnd.mAttachedEdges.Remove(this); mEdgeEnd = value; if (value != null) mEdgeEnd.mAttachedEdges.Add(this); } }

		//INVERSE gg
		internal List<IfcOrientedEdge> mOfEdges = new List<IfcOrientedEdge>();

		internal IfcEdge() : base() { }
		protected IfcEdge(DatabaseIfc db) : base(db) { }
		internal IfcEdge(DatabaseIfc db, IfcEdge e, DuplicateOptions options) : base(db, e, options)
		{
			if(e.mEdgeStart != null)
				EdgeStart = db.Factory.Duplicate( e.EdgeStart) as IfcVertex;
			if(e.mEdgeEnd != null)
				EdgeEnd = db.Factory.Duplicate( e.EdgeEnd) as IfcVertex;
		}
		public IfcEdge(IfcVertex start, IfcVertex end) : base(start.mDatabase) { EdgeStart = start; EdgeEnd = end; }

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			IfcVertex vertex = EdgeStart;
			if(vertex != null)
				result.AddRange(vertex.Extract<T>());
			vertex = EdgeEnd;
			if(vertex != null)
				result.AddRange(vertex.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public partial class IfcEdgeCurve : IfcEdge, IfcCurveOrEdgeCurve
	{
		internal IfcCurve mEdgeGeometry;// IfcCurve;
		internal bool mSameSense = false;// : BOOL;

		public IfcCurve EdgeGeometry { get { return mEdgeGeometry; } set { mEdgeGeometry = value; value.mOfEdge = this; } }
		public bool SameSense { get { return mSameSense; } set { mSameSense = value; } }
		
		internal IfcEdgeCurve() : base() { }
		internal IfcEdgeCurve(DatabaseIfc db, IfcEdgeCurve e, DuplicateOptions options) : base(db, e, options) { EdgeGeometry = db.Factory.Duplicate(e.EdgeGeometry) as IfcCurve; mSameSense = e.mSameSense; }
		public IfcEdgeCurve(IfcVertexPoint start, IfcVertexPoint end, IfcCurve edge, bool sense) : base(start, end) { EdgeGeometry = edge; mSameSense = sense; }
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			result.AddRange(EdgeGeometry.Extract<T>());
			return result;
		}
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public abstract partial class IfcEdgeFeature : IfcFeatureElementSubtraction //  ABSTRACT SUPERTYPE OF (ONEOF (IfcChamferEdgeFeature , IfcRoundedEdgeFeature)) DEPRECATED IFC4
	{
		internal double mFeatureLength;// OPTIONAL IfcPositiveLengthMeasure; 
		protected IfcEdgeFeature() : base() { }
		protected IfcEdgeFeature(DatabaseIfc db, IfcEdgeFeature f, DuplicateOptions options) : base(db, f, options) { mFeatureLength = f.mFeatureLength; }
	}
	[Serializable]
	public partial class IfcEdgeLoop : IfcLoop
	{
		internal LIST<IfcOrientedEdge> mEdgeList = new LIST<IfcOrientedEdge>();// LIST [1:?] OF IfcOrientedEdge;
		public LIST<IfcOrientedEdge> EdgeList { get { return mEdgeList; } }
		internal IfcEdgeLoop() : base() { }
		internal IfcEdgeLoop(DatabaseIfc db, IfcEdgeLoop l, DuplicateOptions options) : base(db, l, options) { mEdgeList.AddRange(l.EdgeList.Select(x => db.Factory.Duplicate(x) as IfcOrientedEdge)); }
		public IfcEdgeLoop(IfcOrientedEdge edge) : base(edge.mDatabase) { mEdgeList.Add(edge); }
		public IfcEdgeLoop(IfcOrientedEdge edge1, IfcOrientedEdge edge2) : base(edge1.mDatabase) { mEdgeList.Add(edge1); mEdgeList.Add(edge2); }
		public IfcEdgeLoop(List<IfcOrientedEdge> edges) : base(edges[0].mDatabase) { mEdgeList.AddRange(edges); }
		public IfcEdgeLoop(List<IfcVertexPoint> vertex)
			: base(vertex[0].mDatabase)
		{
			for (int icounter = 1; icounter < vertex.Count; icounter++)
				mEdgeList.Add(new IfcOrientedEdge(vertex[icounter - 1], vertex[icounter]));
			mEdgeList.Add(new IfcOrientedEdge(vertex[vertex.Count - 1], vertex[0]));
		}

		protected override void initialize()
		{
			base.initialize();
			mEdgeList.CollectionChanged += mEdgeList_CollectionChanged;
		}
		private void mEdgeList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcOrientedEdge edge in e.NewItems)
				{
					edge.mOfLoop = this;	
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcOrientedEdge edge in e.OldItems)
				{
					if (edge.mOfLoop == this)
						edge.mOfLoop = null;
				}
			}
		}
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcOrientedEdge e in EdgeList)
				result.AddRange(e.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public partial class IfcElectricAppliance : IfcFlowTerminal //IFC4
	{
		internal IfcElectricApplianceTypeEnum mPredefinedType = IfcElectricApplianceTypeEnum.NOTDEFINED;// OPTIONAL : IfcElectricApplianceTypeEnum;
		public IfcElectricApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricAppliance() : base() { }
		internal IfcElectricAppliance(DatabaseIfc db, IfcElectricAppliance a, DuplicateOptions options) : base(db, a, options) { mPredefinedType = a.mPredefinedType; }
		public IfcElectricAppliance(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcElectricApplianceType : IfcFlowTerminalType
	{
		internal IfcElectricApplianceTypeEnum mPredefinedType = IfcElectricApplianceTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum;
		public IfcElectricApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricApplianceType() : base() { }
		internal IfcElectricApplianceType(DatabaseIfc db, IfcElectricApplianceType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcElectricApplianceType(DatabaseIfc m, string name, IfcElectricApplianceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcElectricDistributionBoard : IfcFlowController //IFC4
	{
		internal IfcElectricDistributionBoardTypeEnum mPredefinedType = IfcElectricDistributionBoardTypeEnum.NOTDEFINED;// OPTIONAL : IfcDamperTypeEnum;
		public IfcElectricDistributionBoardTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricDistributionBoard() : base() { }
		internal IfcElectricDistributionBoard(DatabaseIfc db, IfcElectricDistributionBoard b, DuplicateOptions options) : base(db, b, options) { mPredefinedType = b.mPredefinedType; }
		public IfcElectricDistributionBoard(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcElectricDistributionBoardType : IfcFlowControllerType
	{
		internal IfcElectricDistributionBoardTypeEnum mPredefinedType = IfcElectricDistributionBoardTypeEnum.NOTDEFINED;// : IfcElectricDistributionBoardTypeEnum;
		public IfcElectricDistributionBoardTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricDistributionBoardType() : base() { }
		internal IfcElectricDistributionBoardType(DatabaseIfc db, IfcElectricDistributionBoardType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcElectricDistributionBoardType(DatabaseIfc m, string name, IfcElectricDistributionBoardTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcElectricDistributionPoint : IfcFlowController // DEPRECATED IFC4
	{
		internal IfcElectricDistributionPointFunctionEnum mDistributionPointFunction;// : IfcElectricDistributionPointFunctionEnum;
		internal string mUserDefinedFunction = "$";// : OPTIONAL IfcLabel;

		public IfcElectricDistributionPointFunctionEnum DistributionPointFunction { get { return mDistributionPointFunction; } set { mDistributionPointFunction = value; } }
		public string UserDefinedFunction { get { return mUserDefinedFunction == "$" ? "" : ParserIfc.Decode(mUserDefinedFunction); } set { mUserDefinedFunction = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value); } }

		internal IfcElectricDistributionPoint() : base() { }
		internal IfcElectricDistributionPoint(DatabaseIfc db, IfcElectricDistributionPoint p, DuplicateOptions options) : base(db, p, options) { mDistributionPointFunction = p.mDistributionPointFunction; mUserDefinedFunction = p.mUserDefinedFunction; }
		public IfcElectricDistributionPoint(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcElectricFlowStorageDevice : IfcFlowStorageDevice //IFC4
	{
		internal IfcElectricFlowStorageDeviceTypeEnum mPredefinedType = IfcElectricFlowStorageDeviceTypeEnum.NOTDEFINED;// OPTIONAL : IfcElectricFlowStorageDeviceTypeEnum;
		public IfcElectricFlowStorageDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricFlowStorageDevice() : base() { }
		internal IfcElectricFlowStorageDevice(DatabaseIfc db, IfcElectricFlowStorageDevice d, DuplicateOptions options) : base(db, d, options) { mPredefinedType = d.mPredefinedType; }
		public IfcElectricFlowStorageDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcElectricFlowStorageDeviceType : IfcFlowStorageDeviceType
	{
		internal IfcElectricFlowStorageDeviceTypeEnum mPredefinedType = IfcElectricFlowStorageDeviceTypeEnum.NOTDEFINED;// : IfcElectricFlowStorageDeviceTypeEnum;
		public IfcElectricFlowStorageDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricFlowStorageDeviceType() : base() { }
		internal IfcElectricFlowStorageDeviceType(DatabaseIfc db, IfcElectricFlowStorageDeviceType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcElectricFlowStorageDeviceType(DatabaseIfc db, string name, IfcElectricFlowStorageDeviceTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcElectricFlowTreatmentDevice : IfcFlowTreatmentDevice
	{
		private IfcElectricFlowTreatmentDeviceTypeEnum mPredefinedType = IfcElectricFlowTreatmentDeviceTypeEnum.NOTDEFINED; //: OPTIONAL IfcElectricFlowTreatmentDeviceTypeEnum;
		public IfcElectricFlowTreatmentDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcElectricFlowTreatmentDevice() : base() { }
		public IfcElectricFlowTreatmentDevice(DatabaseIfc db) : base(db) { }
		public IfcElectricFlowTreatmentDevice(DatabaseIfc db, IfcElectricFlowTreatmentDevice electricFlowTreatmentDevice, DuplicateOptions options) : base(db, electricFlowTreatmentDevice, options) { PredefinedType = electricFlowTreatmentDevice.PredefinedType; }
		public IfcElectricFlowTreatmentDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcElectricFlowTreatmentDeviceType : IfcFlowTreatmentDeviceType
	{
		private IfcElectricFlowTreatmentDeviceTypeEnum mPredefinedType = IfcElectricFlowTreatmentDeviceTypeEnum.NOTDEFINED; //: IfcElectricFlowTreatmentDeviceTypeEnum;
		public IfcElectricFlowTreatmentDeviceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcElectricFlowTreatmentDeviceType() : base() { }
		public IfcElectricFlowTreatmentDeviceType(DatabaseIfc db, string name, IfcElectricFlowTreatmentDeviceTypeEnum predefinedType)
			: base(db) { Name = name; PredefinedType = predefinedType; }
	}
	[Serializable]
	public partial class IfcElectricGenerator : IfcEnergyConversionDevice //IFC4
	{
		internal IfcElectricGeneratorTypeEnum mPredefinedType = IfcElectricGeneratorTypeEnum.NOTDEFINED;// OPTIONAL : IfcElectricGeneratorTypeEnum;
		public IfcElectricGeneratorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricGenerator() : base() { }
		internal IfcElectricGenerator(DatabaseIfc db, IfcElectricGenerator g, DuplicateOptions options) : base(db, g, options) { mPredefinedType = g.mPredefinedType; }
		public IfcElectricGenerator(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcElectricGeneratorType : IfcEnergyConversionDeviceType
	{
		internal IfcElectricGeneratorTypeEnum mPredefinedType = IfcElectricGeneratorTypeEnum.NOTDEFINED;// : IfcElectricGeneratorTypeEnum;
		public IfcElectricGeneratorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricGeneratorType() : base() { }
		internal IfcElectricGeneratorType(DatabaseIfc db, IfcElectricGeneratorType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcElectricGeneratorType(DatabaseIfc db, string name, IfcElectricGeneratorTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcElectricHeaterType : IfcFlowTerminalType // DEPRECATED IFC4
	{
		internal IfcElectricHeaterTypeEnum mPredefinedType = IfcElectricHeaterTypeEnum.NOTDEFINED;// : IfcElectricHeaterTypeEnum
		public IfcElectricHeaterTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricHeaterType() : base() { }
		internal IfcElectricHeaterType(DatabaseIfc db, IfcElectricHeaterType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcElectricHeaterType(DatabaseIfc db, string name, IfcElectricHeaterTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcElectricMotor : IfcEnergyConversionDevice //IFC4
	{
		internal IfcElectricMotorTypeEnum mPredefinedType = IfcElectricMotorTypeEnum.NOTDEFINED;// OPTIONAL : IfcElectricMotorTypeEnum;
		public IfcElectricMotorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricMotor() : base() { }
		internal IfcElectricMotor(DatabaseIfc db, IfcElectricMotor m, DuplicateOptions options) : base(db, m, options) { mPredefinedType = m.mPredefinedType; }
		public IfcElectricMotor(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcElectricMotorType : IfcEnergyConversionDeviceType
	{
		internal IfcElectricMotorTypeEnum mPredefinedType = IfcElectricMotorTypeEnum.NOTDEFINED;// : IfcElectricMotorTypeEnum;
		public IfcElectricMotorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricMotorType() : base() { }
		internal IfcElectricMotorType(DatabaseIfc db, IfcElectricMotorType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcElectricMotorType(DatabaseIfc db, string name, IfcElectricMotorTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcElectricTimeControl : IfcFlowController //IFC4
	{
		internal IfcElectricTimeControlTypeEnum mPredefinedType = IfcElectricTimeControlTypeEnum.NOTDEFINED;// OPTIONAL : IfcElectricTimeControlTypeEnum;
		public IfcElectricTimeControlTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricTimeControl() : base() { }
		internal IfcElectricTimeControl(DatabaseIfc db, IfcElectricTimeControl c, DuplicateOptions options) : base(db, c, options) { mPredefinedType = c.mPredefinedType; }
		public IfcElectricTimeControl(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcElectricTimeControlType : IfcFlowControllerType
	{
		internal IfcElectricTimeControlTypeEnum mPredefinedType = IfcElectricTimeControlTypeEnum.NOTDEFINED;// : IfcElectricTimeControlTypeEnum;
		public IfcElectricTimeControlTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElectricTimeControlType() : base() { }
		internal IfcElectricTimeControlType(DatabaseIfc db, IfcElectricTimeControlType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcElectricTimeControlType(DatabaseIfc m, string name, IfcElectricTimeControlTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcElectricalBaseProperties // DEPRECATED IFC4
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcElectricalCircuit : IfcSystem // DEPRECATED IFC4
	{
		internal IfcElectricalCircuit() : base() { }
		internal IfcElectricalCircuit(DatabaseIfc db, IfcElectricalCircuit c, DuplicateOptions options) : base(db, c, options) { }
	}
	[Obsolete("DEPRECATED IFC2x2", false)]
	[Serializable]
	public partial class IfcElectricalElement : IfcElement  /* DEPRECATED IFC2x2*/ {  	}
	[Serializable]
	public abstract partial class IfcElement : IfcProduct, IfcInterferenceSelect, IfcStructuralActivityAssignmentSelect //ABSTRACT SUPERTYPE OF (ONEOF(IfcBuildingElement, IfcCivilElement
	{ //,IfcDistributionElement,IfcElementAssembly,IfcElementComponent,IfcFeatureElement,IfcFurnishingElement,IfcGeographicElement,IfcTransportElement ,IfcVirtualElement,IfcElectricalElement SS,IfcEquipmentElement SS)) 
		private string mTag = "$";// : OPTIONAL IfcIdentifier;

		//INVERSE  
		internal SET<IfcRelFillsElement> mFillsVoids = new SET<IfcRelFillsElement>();// : SET [0:1] OF IfcRelFillsElement FOR RelatedBuildingElement;
		internal SET<IfcRelConnectsElements> mConnectedTo = new SET<IfcRelConnectsElements>();// : SET OF IfcRelConnectsElements FOR RelatingElement;
		internal SET<IfcRelInterferesElements> mIsInterferedByElements = new SET<IfcRelInterferesElements>();//	 :	SET OF IfcRelInterferesElements FOR RelatedElement;
		internal SET<IfcRelInterferesElements> mInterferesElements = new SET<IfcRelInterferesElements>();// :	SET OF IfcRelInterferesElements FOR RelatingElement;
		internal SET<IfcRelProjectsElement> mHasProjections = new SET<IfcRelProjectsElement>();// : SET OF IfcRelProjectsElement FOR RelatingElement;
		//internal List<IfcRelReferencedInSpatialStructure> mReferencedInStructures = new List<IfcRelReferencedInSpatialStructure>();//  : 	SET OF IfcRelReferencedInSpatialStructure FOR RelatedElements;
		internal SET<IfcRelConnectsPortToElement> mHasPorts = new SET<IfcRelConnectsPortToElement>();// :	SET OF IfcRelConnectsPortToElement FOR RelatedElement; moved IFC4 to IfcDistributionElement
		internal SET<IfcRelVoidsElement> mHasOpenings = new SET<IfcRelVoidsElement>(); //: SET [0:?] OF IfcRelVoidsElement FOR RelatingBuildingElement;
		internal SET<IfcRelConnectsWithRealizingElements> mIsConnectionRealization = new SET<IfcRelConnectsWithRealizingElements>();//	 : 	SET OF IfcRelConnectsWithRealizingElements FOR RealizingElements;
		internal SET<IfcRelSpaceBoundary> mProvidesBoundaries = new SET<IfcRelSpaceBoundary>();//	 : 	SET OF IfcRelSpaceBoundary FOR RelatedBuildingElement;
		internal SET<IfcRelConnectsElements> mConnectedFrom = new SET<IfcRelConnectsElements>();//	 : 	SET OF IfcRelConnectsElements FOR RelatedElement;
		internal SET<IfcRelConnectsStructuralActivity> mAssignedStructuralActivity = new SET<IfcRelConnectsStructuralActivity>();//: 	SET OF IfcRelConnectsStructuralActivity FOR RelatingElement;

		internal SET<IfcRelCoversBldgElements> mHasCoverings = new SET<IfcRelCoversBldgElements>();// : SET OF IfcRelCoversBldgElements FOR RelatingBuildingElement; DEL IFC4

		internal SET<IfcRelConnectsStructuralElement> mHasStructuralMember = new SET<IfcRelConnectsStructuralElement>();// DEL IFC4	 : 	SET OF IfcRelConnectsStructuralElement FOR RelatingElement;

		public string Tag { get { return mTag == "$" ? "" : ParserIfc.Decode(mTag); } set { mTag = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value); } }
		public SET<IfcRelFillsElement> FillsVoids { get { return mFillsVoids; } }
		public SET<IfcRelConnectsElements> ConnectedTo { get { return mConnectedTo; } }
		public SET<IfcRelInterferesElements> IsInterferedByElements { get { return mIsInterferedByElements; } }
		public SET<IfcRelInterferesElements> InterferesElements { get { return mInterferesElements; } }
		public SET<IfcRelProjectsElement> HasProjections { get { return mHasProjections; } }
		[Obsolete("DEPRECATED IFC4", false)]
		public SET<IfcRelConnectsPortToElement> HasPortsSS { get { return mHasPorts; } }
		public SET<IfcRelVoidsElement> HasOpenings { get { return mHasOpenings; } }
		public SET<IfcRelConnectsWithRealizingElements> IsConnectionRealization { get { return mIsConnectionRealization; } }
		public SET<IfcRelSpaceBoundary> ProvidesBoundaries { get { return mProvidesBoundaries; } }
		public SET<IfcRelConnectsElements> ConnectedFrom { get { return mConnectedFrom; } }
		public IfcRelContainedInSpatialStructure ContainedInStructure { get { return mContainedInStructure; } }
		public SET<IfcRelConnectsStructuralActivity> AssignedStructuralActivity { get { return mAssignedStructuralActivity; } }

		public SET<IfcRelCoversBldgElements> HasCoverings { get { return mHasCoverings; } }
		//GEOMGYM
		//List<IfcRelConnectsStructuralActivity> mAssignedStructuralActivity = new List<IfcRelConnectsStructuralActivity>();//: 	SET OF IfcRelConnectsStructuralActivity FOR RelatingElement;

		protected IfcElement() : base() { }
		protected IfcElement(IfcElement element, bool replace) : base(element, replace)
		{
			mTag = element.mTag;
			//todo setInverse
		}
		protected IfcElement(DatabaseIfc db) : base(db) { }
		protected IfcElement(DatabaseIfc db, IfcElement e, DuplicateOptions options) : base(db, e, options)
		{
			mTag = e.mTag;
#warning todo finish inverse
			
			List<IfcRelConnectsElements> rces = e.ConnectedTo.ToList();
			rces.AddRange(e.ConnectedFrom);
			foreach (IfcRelConnectsElements ce in rces)
			{
				IfcElement relating = ce.RelatingElement, related = ce.RelatedElement;
				int relatingIndex = db.Factory.mDuplicateMapping.FindExisting(relating), relatedIndex = db.Factory.mDuplicateMapping.FindExisting(related);
				if (relating.mIndex != e.mIndex && relatingIndex > 0)
				{
					IfcRelConnectsElements rce = db.Factory.Duplicate(ce, new DuplicateOptions(options.DeviationTolerance) { DuplicateDownstream = false }) as IfcRelConnectsElements;
					rce.RelatedElement = this;
					rce.RelatingElement = db[relatingIndex] as IfcElement;
				}
				else if (related.mIndex != e.mIndex && relatedIndex > 0)
				{
					IfcRelConnectsElements rce = db.Factory.Duplicate(ce, new DuplicateOptions(options.DeviationTolerance) { DuplicateDownstream = false }) as IfcRelConnectsElements;
					rce.RelatingElement = this;
					rce.RelatedElement = db[relatedIndex] as IfcElement;
				}
			}
			if (options.DuplicateDownstream)
			{
				foreach (IfcRelVoidsElement ve in e.mHasOpenings)
				{
					IfcRelVoidsElement rv = db.Factory.Duplicate(ve, options) as IfcRelVoidsElement;
					rv.RelatingBuildingElement = this;
				}
			}
		
			foreach (IfcRelConnectsStructuralActivity rcss in e.mAssignedStructuralActivity)
			{
				IfcRelConnectsStructuralActivity rc = db.Factory.Duplicate(rcss) as IfcRelConnectsStructuralActivity;
				rc.RelatingElement = this;
			}

			foreach (IfcRelConnectsStructuralElement rcse in e.mHasStructuralMember)
			{
				IfcRelConnectsStructuralElement rc = db.Factory.Duplicate(rcse) as IfcRelConnectsStructuralElement;
				rc.RelatingElement = this;
			}
		}
		protected IfcElement(IfcObjectDefinition host, IfcObjectPlacement p, IfcProductDefinitionShape r) : base(host, p, r) { }
		protected IfcElement(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) 
			: base(host, null, null)
		{
			IfcObjectPlacement hostplacement = host.ObjectPlacement;
			if (hostplacement == null)
			{
				if (host.Decomposes != null)
				{
					IfcProduct product = host.Decomposes.RelatingObject as IfcProduct;
					if (product != null)
						host.ObjectPlacement = hostplacement = new IfcLocalPlacement(product.ObjectPlacement, mDatabase.Factory.XYPlanePlacement);
				}
			}
			ObjectPlacement = new IfcLocalPlacement(hostplacement, placement);
			List<IfcShapeModel> reps = new List<IfcShapeModel>();
			IfcCartesianPoint cp = new IfcCartesianPoint(mDatabase, 0, 0, length);
			IfcPolyline ipl = new IfcPolyline(mDatabase.Factory.Origin, cp);
			reps.Add(new IfcShapeRepresentation( mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis), ipl, ShapeRepresentationType.Curve2D));

			profile.Associate(this);

			IfcMaterialProfileSet ps = profile.ForProfileSet;
			IfcMaterialProfileSetUsageTapering psut = profile as IfcMaterialProfileSetUsageTapering;
			if (psut != null)
				throw new Exception("Tapering Elements not implemented yet!");
			IfcProfileDef pd = null;
			if (ps.mCompositeProfile != null)
				pd = ps.CompositeProfile;
			else
			{
				if (ps.mMaterialProfiles.Count > 0)
					pd = ps.MaterialProfiles[0].Profile;
				else
					throw new Exception("Profile not provided");
			}
			if (pd != null)
			{
				if(pd.ProfileType == IfcProfileTypeEnum.AREA)
					reps.Add(new IfcShapeRepresentation(new IfcExtrudedAreaSolid(pd, pd.CalculateTransform(profile.CardinalPoint), length)));
				else
					reps.Add(new IfcShapeRepresentation(new IfcSurfaceOfLinearExtrusion(pd, pd.CalculateTransform(profile.CardinalPoint), length)));
			}

			Representation = new IfcProductDefinitionShape(reps);

		}
		protected IfcElement(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, Tuple<double, double> arcOrigin, double arcAngle) : base(host, new IfcLocalPlacement(host.ObjectPlacement, placement), null)
		{
			IfcMaterialProfileSet ps = profile.ForProfileSet;
			profile.Associate(this);
			IfcMaterialProfile mp = ps.MaterialProfiles[0];
			IfcProfileDef pd = mp.Profile;
			DatabaseIfc db = host.mDatabase;
			List<IfcShapeModel> reps = new List<IfcShapeModel>();
			double length = Math.Sqrt(Math.Pow(arcOrigin.Item1, 2) + Math.Pow(arcOrigin.Item2, 2)), angMultipler = 1 / mDatabase.mContext.UnitsInContext.ScaleSI(IfcUnitEnum.PLANEANGLEUNIT);
			Tuple<double, double> normal = new Tuple<double, double>(-arcOrigin.Item2 / length, arcOrigin.Item1 / length);
			reps.Add(new IfcShapeRepresentation(mDatabase.Factory.SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis), new IfcTrimmedCurve(new IfcCircle(new IfcAxis2Placement3D(new IfcCartesianPoint(db, arcOrigin.Item1, arcOrigin.Item2, 0), new IfcDirection(db, normal.Item1, normal.Item2, 0), new IfcDirection(db, -arcOrigin.Item1, -arcOrigin.Item2, 0)), length), new IfcTrimmingSelect(0), new IfcTrimmingSelect(arcAngle * angMultipler), true, IfcTrimmingPreference.PARAMETER), ShapeRepresentationType.Curve2D));
			IfcAxis2Placement3D translation = pd.CalculateTransform(profile.CardinalPoint);
			LIST<double> pt = translation.Location.Coordinates;
			IfcAxis1Placement axis = new IfcAxis1Placement(new IfcCartesianPoint(db, arcOrigin.Item1 - pt[0], arcOrigin.Item2 - (pt.Count > 1 ? pt[1] : 0)), new IfcDirection(db, normal.Item1, normal.Item2));
			reps.Add(new IfcShapeRepresentation(new IfcRevolvedAreaSolid(pd, translation, axis, arcAngle * angMultipler)));
			Representation = new IfcProductDefinitionShape(reps);
		}

		protected override void initialize()
		{
			base.initialize();
			mConnectedTo.CollectionChanged += mConnectedTo_CollectionChanged;
			mIsConnectionRealization.CollectionChanged += mIsConnectionRealization_CollectionChanged;
			mConnectedFrom.CollectionChanged += mConnectedFrom_CollectionChanged;
		}
		private void mConnectedTo_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelConnectsElements r in e.NewItems)
				{
					if (r.RelatingElement != this)
						r.RelatingElement = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelConnectsElements r in e.NewItems)
					r.RelatingElement = null;
			}
		}
		private void mIsConnectionRealization_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelConnectsWithRealizingElements r in e.NewItems)
				{
					if (!r.RealizingElements.Contains(this))
						r.RealizingElements.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelConnectsWithRealizingElements r in e.NewItems)
					r.RealizingElements.Remove(this);
			}
		}
		private void mConnectedFrom_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRelConnectsElements r in e.NewItems)
				{
					if (r.RelatedElement != this)
						r.RelatedElement = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcRelConnectsElements r in e.NewItems)
					r.RelatedElement = null;
			}
		}
		public IfcMaterialSelect MaterialSelect() { return GetMaterialSelect(); }
		public void SetMaterial(IfcMaterialSelect material) { this.setMaterial(material); }
		protected override IfcMaterialSelect GetMaterialSelect()
		{
			IfcMaterialSelect m = base.GetMaterialSelect();
			if (m != null)
				return m;
			if (IsTypedBy != null)
			{
				IfcElementType t = RelatingType() as IfcElementType;
				if (t != null)
					return t.MaterialSelect;
			}
			return null;
		}

		internal IfcProduct getContainer()
		{
			if (mDecomposes != null)
				return mDecomposes.RelatingObject as IfcProduct;
			return (mContainedInStructure != null ? mContainedInStructure.RelatingStructure : null);
		}	
		public void AddMember(IfcStructuralMember m)
		{
			if (m == null)
				return;
			if (mDatabase.mRelease < ReleaseVersion.IFC4)
			{
				mHasStructuralMember.Add(new IfcRelConnectsStructuralElement(this, m));
			}
			else
			{
				if (mReferencedBy.Count > 0)
					mReferencedBy.First().RelatedObjects.Add(m);
				else
				{
					new IfcRelAssignsToProduct(m, this);
				}
			}
		}
		public void AssignStructuralActivity(IfcRelConnectsStructuralActivity connects) { mAssignedStructuralActivity.Add(connects); }
	}
	[Serializable]
	public abstract partial class IfcElementarySurface : IfcSurface //	ABSTRACT SUPERTYPE OF(ONEOF(IfcCylindricalSurface, IfcPlane))
	{
		private int mPosition;// : IfcAxis2Placement3D; 
		public IfcAxis2Placement3D Position { get { return mDatabase[mPosition] as IfcAxis2Placement3D; } set { mPosition = value.mIndex; } }

		protected IfcElementarySurface() : base() { }
		protected IfcElementarySurface(DatabaseIfc db, IfcElementarySurface s, DuplicateOptions options) : base(db, s, options) { Position = db.Factory.Duplicate(s.Position) as IfcAxis2Placement3D; }
		protected IfcElementarySurface(IfcAxis2Placement3D placement) : base(placement.mDatabase) { mPosition = placement.mIndex; }
	}
	[Serializable]
	public partial class IfcElementAssembly : IfcElement
	{
		internal IfcAssemblyPlaceEnum mAssemblyPlace = IfcAssemblyPlaceEnum.NOTDEFINED;//: OPTIONAL IfcAssemblyPlaceEnum;
		internal IfcElementAssemblyTypeEnum mPredefinedType = IfcElementAssemblyTypeEnum.NOTDEFINED;//: OPTIONAL IfcElementAssemblyTypeEnum;
		public IfcAssemblyPlaceEnum AssemblyPlace { get { return mAssemblyPlace; } set { mAssemblyPlace = value; } }
		public IfcElementAssemblyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElementAssembly() : base() { }
		internal IfcElementAssembly(DatabaseIfc db, IfcElementAssembly a, DuplicateOptions options) : base(db, a, options) { mPredefinedType = a.mPredefinedType; }
		public IfcElementAssembly(IfcObjectDefinition host, IfcAssemblyPlaceEnum place, IfcElementAssemblyTypeEnum type) : base(host, null, null) { AssemblyPlace = place; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcElementAssemblyType : IfcElementType //IFC4
	{
		internal IfcElementAssemblyTypeEnum mPredefinedType = IfcElementAssemblyTypeEnum.NOTDEFINED;// IfcElementAssemblyTypeEnum; 
		public IfcElementAssemblyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcElementAssemblyType() : base() { }
		internal IfcElementAssemblyType(DatabaseIfc db, IfcElementAssemblyType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcElementAssemblyType(DatabaseIfc m, string name, IfcElementAssemblyTypeEnum type) : base(m) { Name = name; mPredefinedType = type; if (m.mRelease < ReleaseVersion.IFC4) throw new Exception(StepClassName + " only supported in IFC4!"); }
	}
	[Serializable]
	public abstract partial class IfcElementComponent : IfcElement //	ABSTRACT SUPERTYPE OF(ONEOF(IfcBuildingElementPart, IfcDiscreteAccessory, IfcFastener, IfcMechanicalFastener, IfcReinforcingElement, IfcVibrationIsolator))
	{
		protected IfcElementComponent() : base() { }
		protected IfcElementComponent(DatabaseIfc db) : base(db) { }
		protected IfcElementComponent(DatabaseIfc db, IfcElementComponent c, DuplicateOptions options) : base(db, c, options) { }
		protected IfcElementComponent(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host,placement,representation) { }
		protected IfcElementComponent(IfcProduct host, IfcMaterialProfileSetUsage profile, IfcAxis2Placement3D placement, double length) : base(host, profile, placement,length) { }
	}
	[Serializable]
	public abstract partial class IfcElementComponentType : IfcElementType // ABSTRACT SUPERTYPE OF (ONEOF	((IfcBuildingElementPartType, IfcDiscreteAccessoryType, IfcFastenerType, IfcMechanicalFastenerType, IfcReinforcingElementType, IfcVibrationIsolatorType)))
	{
		protected IfcElementComponentType() : base() { }
		protected IfcElementComponentType(DatabaseIfc db, IfcElementComponentType t, DuplicateOptions options) : base(db, t, options) { }
		protected IfcElementComponentType(DatabaseIfc db) : base(db) { }
		protected IfcElementComponentType(DatabaseIfc db, string name) : base(db) { Name = name; }
	}
	[Serializable]
	public partial class IfcElementQuantity : IfcQuantitySet
	{
		public override string StepClassName { get { return "IfcElementQuantity"; } }
		internal string mMethodOfMeasurement = "$";// : OPTIONAL IfcLabel;
		private Dictionary<string, IfcPhysicalQuantity> mQuantities = new Dictionary<string, IfcPhysicalQuantity>();// : SET [1:?] OF IfcPhysicalQuantity;

		public string MethodOfMeasurement { get { return (mMethodOfMeasurement == "$" ? "" : ParserIfc.Decode(mMethodOfMeasurement)); } set { mMethodOfMeasurement = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public Dictionary<string, IfcPhysicalQuantity> Quantities { get { return mQuantities; } }

		internal IfcElementQuantity() : base() { }
		internal IfcElementQuantity(DatabaseIfc db, IfcElementQuantity q, DuplicateOptions options) : base(db, q, options) { mMethodOfMeasurement = q.mMethodOfMeasurement; SetQuantities(q.Quantities.Values.Select(x=> db.Factory.Duplicate(x) as IfcPhysicalQuantity)); }
		protected IfcElementQuantity(IfcObjectDefinition obj) : base(obj.mDatabase,"") { Name = this.GetType().Name; new IfcRelDefinesByProperties(obj, this); }
		protected IfcElementQuantity(IfcTypeObject type) : base(type.mDatabase,"") { Name = this.GetType().Name; type.HasPropertySets.Add(this); }
		public IfcElementQuantity(DatabaseIfc db, string name) : base(db, name) { }
		public IfcElementQuantity(string name, IfcPhysicalQuantity quantity) : base(quantity.mDatabase, name) { addQuantity(quantity); }
		public IfcElementQuantity(string name, IEnumerable<IfcPhysicalQuantity> quantities) : base(quantities.First().mDatabase, name) { foreach(IfcPhysicalQuantity q in quantities) addQuantity(q); }
		public IfcElementQuantity(string name, params IfcPhysicalQuantity[] quantities) : base(quantities.First().mDatabase, name) { foreach(IfcPhysicalQuantity q in quantities) addQuantity(q); }
		
		internal override List<IBaseClassIfc> retrieveReference(IfcReference reference)
		{
			IfcReference ir = reference.InnerReference;
			List<IBaseClassIfc> result = new List<IBaseClassIfc>();
			if (ir == null)
			{
				if (string.Compare(reference.mAttributeIdentifier, "Quantities", true) == 0)
				{
					if (reference.mListPositions.Count == 0)
					{
						string name = reference.InstanceName;
						if (!string.IsNullOrEmpty(name))
						{
							foreach (IfcPhysicalQuantity q in mQuantities.Values)
							{
								if (string.Compare(q.Name, name) == 0)
									result.Add(q);
							}
						}
						else
							result.AddRange(mQuantities.Values);
					}
					else
					{
						List<IfcPhysicalQuantity> quantities = mQuantities.Values.ToList();
						foreach (int i in reference.mListPositions)
						{
							result.Add(quantities[i - 1]);
						}
					}
					return result;
				}
			}
			if (string.Compare(reference.mAttributeIdentifier, "Quantities", true) == 0)
			{
				if (reference.mListPositions.Count == 0)
				{
					string name = reference.InstanceName;

					if (string.IsNullOrEmpty(name))
					{
						foreach (IfcPhysicalQuantity q in mQuantities.Values)
							result.AddRange(q.retrieveReference(reference.InnerReference));
					}
					else
					{
						foreach (IfcPhysicalQuantity q in mQuantities.Values)
						{
							if (string.Compare(name, q.Name) == 0)
								result.AddRange(q.retrieveReference(reference.InnerReference));
						}
					}
				}
				else
				{
					List<IfcPhysicalQuantity> quantities = mQuantities.Values.ToList();
					foreach (int i in reference.mListPositions)
						result.AddRange(quantities[i - 1].retrieveReference(ir));
				}
				return result;
			}
			return base.retrieveReference(reference);
		}
		internal override bool isEmpty { get { return mQuantities.Count == 0; } }

		internal void addQuantity(IfcPhysicalQuantity quantity)
		{
			if(quantity == null)
				return;
			IfcPhysicalQuantity existing = null;
			if (mQuantities.TryGetValue(quantity.Name, out existing))
			{
				if (quantity.isDuplicate(existing, mDatabase.Tolerance))
					return;
			}
			mQuantities[quantity.Name] = quantity;
		}

		public IfcPhysicalQuantity this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
					return null;
				IfcPhysicalQuantity result = null;
				mQuantities.TryGetValue(name, out result);
				return result;
			}
			set { addQuantity(value); }
		}
		public void SetQuantities(IEnumerable<IfcPhysicalQuantity> quantities)
		{
			mQuantities.Clear();
			foreach (IfcPhysicalQuantity quantity in quantities)
				addQuantity(quantity);
		}
	}
	[Serializable]
	public abstract partial class IfcElementType : IfcTypeProduct //ABSTRACT SUPERTYPE OF(ONEOF(IfcBuildingElementType, IfcDistributionElementType, IfcElementAssemblyType, IfcElementComponentType, IfcFurnishingElementType, IfcGeographicElementType, IfcTransportElementType))
	{
		private string mElementType = "$";// : OPTIONAL IfcLabel
		public string ElementType { get { return mElementType == "$" ? "" : ParserIfc.Decode( mElementType); } set { mElementType = string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode( value); } }

		protected IfcElementType() : base() { }
		protected IfcElementType(IfcElementType basis) : base(basis) { mElementType = basis.mElementType; }
		protected IfcElementType(DatabaseIfc db, IfcElementType t, DuplicateOptions options) : base(db, t, options) { mElementType = t.mElementType; }
		protected IfcElementType(DatabaseIfc db) : base(db) { }
		
		public IfcMaterialSelect MaterialSelect
		{
			get { return GetMaterialSelect(); }
			set { base.setMaterial(value); }
		}

		public IfcElement GenerateMappedItemElement(IfcProduct container, IfcCartesianTransformationOperator transform)  
		{
			string typename = this.GetType().Name;
			typename = typename.Substring(0, typename.Length - 4);
			IfcProductDefinitionShape pds = new IfcProductDefinitionShape(new IfcShapeRepresentation(new IfcMappedItem(RepresentationMaps[0], transform)));
			IfcElement element = container.Database.Factory.ConstructElement(typename, container,null, pds);
			element.setRelatingType(this);
			return element;
		}
	}
	[Serializable]
	public partial class IfcEllipse : IfcConic
	{
		internal double mSemiAxis1;// : IfcPositiveLengthMeasure;
		internal double mSemiAxis2;// : IfcPositiveLengthMeasure;

		public double SemiAxis1 { get { return mSemiAxis1; } set { mSemiAxis1 = value; } }
		public double SemiAxis2 { get { return mSemiAxis2; } set { mSemiAxis2 = value; } }

		internal IfcEllipse() : base() { }
		internal IfcEllipse(DatabaseIfc db, IfcEllipse e, DuplicateOptions options) : base(db, e, options) { mSemiAxis1 = e.mSemiAxis1; mSemiAxis2 = e.mSemiAxis2; }
		public IfcEllipse(IfcAxis2Placement placement, double axis1, double axis2) : base(placement) { mSemiAxis1 = axis1; mSemiAxis2 = axis2; }
		public IfcEllipse(DatabaseIfc db, double axis1, double axis2) : base(db.Factory.Origin2dPlace) { mSemiAxis1 = axis1; mSemiAxis2 = axis2; }
	}
	[Serializable]
	public partial class IfcEllipseProfileDef : IfcParameterizedProfileDef
	{
		internal double mSemiAxis1;// : IfcPositiveLengthMeasure;
		internal double mSemiAxis2;// : IfcPositiveLengthMeasure;

		public double SemiAxis1 { get { return mSemiAxis1; } set { mSemiAxis1 = value; } }
		public double SemiAxis2 { get { return mSemiAxis2; } set { mSemiAxis2 = value; } }

		internal IfcEllipseProfileDef() : base() { }
		internal IfcEllipseProfileDef(DatabaseIfc db, IfcEllipseProfileDef p, DuplicateOptions options) : base(db, p, options) { mSemiAxis1 = p.mSemiAxis1; mSemiAxis2 = p.mSemiAxis2; }
		public IfcEllipseProfileDef(DatabaseIfc db, string name, double semiAxis1, double semiAxis2) : base(db, name) { SemiAxis1 = semiAxis1; SemiAxis2 = semiAxis2;  }
	}
	[Serializable]
	public partial class IfcEnergyConversionDevice : IfcDistributionFlowElement //IFC4 Abstract
	{  //	SUPERTYPE OF(ONEOF(IfcAirToAirHeatRecovery, IfcBoiler, IfcBurner, IfcChiller, IfcCoil, IfcCondenser, IfcCooledBeam, 
		//IfcCoolingTower, IfcElectricGenerator, IfcElectricMotor, IfcEngine, IfcEvaporativeCooler, IfcEvaporator, IfcHeatExchanger,
		//IfcHumidifier, IfcMotorConnection, IfcSolarDevice, IfcTransformer, IfcTubeBundle, IfcUnitaryEquipment))
		public override string StepClassName { get { return mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcEnergyConversionDevice" : base.StepClassName; } }

		internal IfcEnergyConversionDevice() : base() { }
		internal IfcEnergyConversionDevice(DatabaseIfc db, IfcEnergyConversionDevice d, DuplicateOptions options) : base(db, d, options) { }
		public IfcEnergyConversionDevice(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public abstract partial class IfcEnergyConversionDeviceType : IfcDistributionFlowElementType
	{ //ABSTRACT SUPERTYPE OF (ONEOF (IfcAirToAirHeatRecoveryType ,IfcBoilerType, Ifctype ,IfcChillerType ,IfcCoilType ,IfcCondenserType ,IfcCooledBeamType
		//,IfcCoolingTowerType ,IfcElectricGeneratorType ,IfcElectricMotorType ,IfcEvaporativeCoolerType ,IfcEvaporatorType ,IfcHeatExchangerType
		//,IfcHumidifierType ,IfcMotorConnectionType ,IfcSpaceHeaterType ,IfcTransformerType ,IfcTubeBundleType ,IfcUnitaryEquipmentType))
		protected IfcEnergyConversionDeviceType() : base() { }
		protected IfcEnergyConversionDeviceType(DatabaseIfc db) : base(db) { }
		protected IfcEnergyConversionDeviceType(DatabaseIfc db, IfcEnergyConversionDeviceType t, DuplicateOptions options) : base(db, t, options) { }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//IfcEnergyProperties // DEPRECATED IFC4
	[Serializable]
	public partial class IfcEngine : IfcEnergyConversionDevice //IFC4
	{
		internal IfcEngineTypeEnum mPredefinedType = IfcEngineTypeEnum.NOTDEFINED;// OPTIONAL : IfcEngineTypeEnum;
		public IfcEngineTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcEngine() : base() { }
		internal IfcEngine(DatabaseIfc db, IfcEngine e, DuplicateOptions options) : base(db, e, options) { mPredefinedType = e.mPredefinedType; }
		public IfcEngine(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcEngineType : IfcEnergyConversionDeviceType
	{
		internal IfcEngineTypeEnum mPredefinedType = IfcEngineTypeEnum.NOTDEFINED;// : IfcEngineTypeEnum;
		public IfcEngineTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcEngineType() : base() { }
		internal IfcEngineType(DatabaseIfc db, IfcEngineType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcEngineType(DatabaseIfc db, string name, IfcEngineTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcEnvironmentalImpactValue : IfcAppliedValue //DEPRECATED
	{
		internal string mImpactType;// : IfcLabel;
		internal IfcEnvironmentalImpactCategoryEnum mEnvCategory = IfcEnvironmentalImpactCategoryEnum.NOTDEFINED;// IfcEnvironmentalImpactCategoryEnum
		internal string mUserDefinedCategory = "$";//  : OPTIONAL IfcLabel;
		internal IfcEnvironmentalImpactValue() : base() { }
		internal IfcEnvironmentalImpactValue(DatabaseIfc db, IfcEnvironmentalImpactValue v) : base(db,v) { mImpactType = v.mImpactType; mEnvCategory = v.mEnvCategory; mUserDefinedCategory = v.mUserDefinedCategory; }
	}
	[Obsolete("DEPRECATED IFC2x2", false)]
	[Serializable]
	public partial class IfcEquipmentElement : IfcElement  
	{
		internal IfcEquipmentElement() : base() { }
		internal IfcEquipmentElement(DatabaseIfc db, IfcEquipmentElement e, DuplicateOptions options) : base(db, e, options) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcEquipmentStandard : IfcControl 
	{
		internal IfcEquipmentStandard() : base() { }
		internal IfcEquipmentStandard(DatabaseIfc db, IfcEquipmentStandard s, DuplicateOptions options) : base(db,s, options) { }
	}
	[Serializable]
	public partial class IfcEvaporativeCooler : IfcEnergyConversionDevice //IFC4
	{
		internal IfcEvaporativeCoolerTypeEnum mPredefinedType = IfcEvaporativeCoolerTypeEnum.NOTDEFINED;// OPTIONAL : IfcEvaporativeCoolerTypeEnum;
		public IfcEvaporativeCoolerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcEvaporativeCooler() : base() { }
		internal IfcEvaporativeCooler(DatabaseIfc db, IfcEvaporativeCooler c, DuplicateOptions options) : base(db, c, options) { mPredefinedType = c.mPredefinedType; }
		public IfcEvaporativeCooler(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcEvaporativeCoolerType : IfcEnergyConversionDeviceType
	{
		internal IfcEvaporativeCoolerTypeEnum mPredefinedType = IfcEvaporativeCoolerTypeEnum.NOTDEFINED;// : IfcEvaporativeCoolerTypeEnum;
		public IfcEvaporativeCoolerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcEvaporativeCoolerType() : base() { }
		internal IfcEvaporativeCoolerType(DatabaseIfc db, IfcEvaporativeCoolerType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcEvaporativeCoolerType(DatabaseIfc db, string name, IfcEvaporativeCoolerTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcEvaporator : IfcEnergyConversionDevice //IFC4
	{
		internal IfcEvaporatorTypeEnum mPredefinedType = IfcEvaporatorTypeEnum.NOTDEFINED;// OPTIONAL : IfcEvaporatorTypeEnum;
		public IfcEvaporatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcEvaporator() : base() { }
		internal IfcEvaporator(DatabaseIfc db, IfcEvaporator e, DuplicateOptions options) : base(db, e, options) { mPredefinedType = e.mPredefinedType; }
		public IfcEvaporator(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
	}
	[Serializable]
	public partial class IfcEvaporatorType : IfcEnergyConversionDeviceType
	{
		internal IfcEvaporatorTypeEnum mPredefinedType = IfcEvaporatorTypeEnum.NOTDEFINED;// : IfcEvaporatorTypeEnum;
		public IfcEvaporatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcEvaporatorType() : base() { }
		internal IfcEvaporatorType(DatabaseIfc db, IfcEvaporatorType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcEvaporatorType(DatabaseIfc db, string name, IfcEvaporatorTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
	}
	[Serializable]
	public partial class IfcEvent : IfcProcess //IFC4
	{
		internal IfcEventTypeEnum mPredefinedType = IfcEventTypeEnum.NOTDEFINED;// : IfcEventTypeEnum; 
		internal IfcEventTriggerTypeEnum mEventTriggerType = IfcEventTriggerTypeEnum.NOTDEFINED;// : IfcEventTypeEnum; 
		internal string mUserDefinedEventTriggerType = "$";//	:	OPTIONAL IfcLabel;
		internal int mEventOccurrenceTime;//	:	OPTIONAL IfcEventTime;

		public IfcEventTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcEventTriggerTypeEnum EventTriggerType { get { return mEventTriggerType; } set { mEventTriggerType = value; } }
		public string UserDefinedEventTriggerType { get { return ParserIfc.Decode(mUserDefinedEventTriggerType); } set { mUserDefinedEventTriggerType = ParserIfc.Encode(value); } }
		
		internal IfcEvent() : base() { }
		internal IfcEvent(DatabaseIfc db, IfcEvent e, DuplicateOptions options) : base(db, e, options) { mPredefinedType = e.mPredefinedType; mEventTriggerType = e.mEventTriggerType; mUserDefinedEventTriggerType = e.mUserDefinedEventTriggerType; }
	}
	[Serializable]
	public partial class IfcEventTime : IfcSchedulingTime
	{
		private DateTime mActualDate = DateTime.MinValue; //: OPTIONAL IfcDateTime;
		private DateTime mEarlyDate = DateTime.MinValue; //: OPTIONAL IfcDateTime;
		private DateTime mLateDate = DateTime.MinValue; //: OPTIONAL IfcDateTime;
		private DateTime mScheduleDate = DateTime.MinValue; //: OPTIONAL IfcDateTime;

		public DateTime ActualDate { get { return mActualDate; } set { mActualDate = value; } }
		public DateTime EarlyDate { get { return mEarlyDate; } set { mEarlyDate = value; } }
		public DateTime LateDate { get { return mLateDate; } set { mLateDate = value; } }
		public DateTime ScheduleDate { get { return mScheduleDate; } set { mScheduleDate = value; } }

		public IfcEventTime() : base() { }
		public IfcEventTime(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcEventType : IfcTypeProcess //IFC4
	{
		internal IfcEventTypeEnum mPredefinedType = IfcEventTypeEnum.NOTDEFINED;// : IfcEventTypeEnum; 
		internal IfcEventTriggerTypeEnum mEventTriggerType = IfcEventTriggerTypeEnum.NOTDEFINED;// : IfcEventTypeEnum; 
		internal string mUserDefinedEventTriggerType = "$";//	:	OPTIONAL IfcLabel;

		public IfcEventTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcEventTriggerTypeEnum EventTriggerType { get { return mEventTriggerType; } set { mEventTriggerType = value; } }
		public string UserDefinedEventTriggerType { get { return (mUserDefinedEventTriggerType == "$" ? "" : ParserIfc.Decode(mUserDefinedEventTriggerType)); } set { mUserDefinedEventTriggerType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcEventType() : base() { }
		internal IfcEventType(DatabaseIfc db, IfcEventType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; mEventTriggerType = t.mEventTriggerType; mUserDefinedEventTriggerType = t.mUserDefinedEventTriggerType; }
		public IfcEventType(DatabaseIfc m, string name, IfcEventTypeEnum t, IfcEventTriggerTypeEnum trigger)
			: base(m) { Name = name; mPredefinedType = t; mEventTriggerType = trigger; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcExtendedMaterialProperties : IfcMaterialProperties   // DEPRECATED IFC4
	{
		internal List<int> mExtendedProperties = new List<int>(); //: SET [1:?] OF IfcProperty

		public ReadOnlyCollection<IfcProperty> ExtendedProperties { get { return new ReadOnlyCollection<IfcProperty>( mExtendedProperties.ConvertAll(x => mDatabase[x] as IfcProperty)); } }

		internal IfcExtendedMaterialProperties() : base() { }
		internal IfcExtendedMaterialProperties(DatabaseIfc db, IfcExtendedMaterialProperties p, DuplicateOptions options) : base(db, p, options) 
		{ 
			foreach(IfcProperty prop in p.ExtendedProperties)
				addProperty( db.Factory.Duplicate(prop));
			mDescription = p.mDescription;
			mName = p.mName; 
		}
		public IfcExtendedMaterialProperties(IfcMaterialDefinition materialDefinition, IEnumerable<IfcProperty> properties) : base(materialDefinition)
		{
			mExtendedProperties.AddRange(properties.Select(x => x.StepId));
		}

		internal void addProperty(IfcProperty property) { mExtendedProperties.Add(property.mIndex); }
	}
	[Serializable]
	public abstract partial class IfcExtendedProperties : IfcPropertyAbstraction, NamedObjectIfc //IFC4 ABSTRACT SUPERTYPE OF (ONEOF (IfcMaterialProperties,IfcProfileProperties))
	{
		protected string mName = ""; //: OPTIONAL IfcLabel;
		protected string mDescription = ""; //: OPTIONAL IfcText;
		internal Dictionary<string, IfcProperty> mProperties = new Dictionary<string, IfcProperty>();//: SET [1:?] OF IfcProperty 

		public string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }
		public ReadOnlyDictionary<string, IfcProperty> Properties { get { return new ReadOnlyDictionary<string, IfcProperty>( mProperties); } }

		protected override void initialize()
		{
			base.initialize();
			mProperties = new Dictionary<string, IfcProperty>();
		}

		protected IfcExtendedProperties() : base() { }
		protected IfcExtendedProperties(DatabaseIfc db) : base(db) {  }
		protected IfcExtendedProperties(DatabaseIfc db, IfcExtendedProperties p, DuplicateOptions options) : base(db, p, options) { mName = p.mName; mDescription = p.mDescription; p.Properties.Values.ToList().ForEach(x => AddProperty( db.Factory.Duplicate(x) as IfcProperty));   }
		public IfcExtendedProperties(List<IfcProperty> props) : base(props[0].mDatabase)
		{
			if (props != null)
				props.ForEach(x => AddProperty(x));
		}
		
		public void AddProperty(IfcProperty property)
		{
			if (property != null)
				mProperties[property.Name] = property;
		}
		public void RemoveProperty(IfcProperty property)
		{
			if (property != null)
				mProperties.Remove(property.Name);
		}
		public IfcProperty this[string name]
		{
			get
			{
				if (string.IsNullOrEmpty(name))
					return null;
				IfcProperty result = null;
				mProperties.TryGetValue(name, out result);
				return result;
			}
		}
	}
	[Serializable]
	public abstract partial class IfcExternalInformation : BaseClassIfc, IfcResourceObjectSelect // NEW IFC4	ABSTRACT SUPERTYPE OF(ONEOF(IfcClassification, IfcDocumentInformation, IfcLibraryInformation));
	{ //INVERSE
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } set { mHasExternalReference.Clear();  if (value != null) { mHasExternalReference.CollectionChanged -= mHasExternalReference_CollectionChanged; mHasExternalReference = value; mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged; } } }
		public ReadOnlyCollection<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return new ReadOnlyCollection<IfcResourceConstraintRelationship>(mHasConstraintRelationships); } }

		protected IfcExternalInformation() : base() { }
		protected IfcExternalInformation(DatabaseIfc db) : base(db) { }
		protected IfcExternalInformation(DatabaseIfc db, IfcExternalInformation i) : base(db, i) { }
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
	[Serializable]
	public partial class IfcExternallyDefinedHatchStyle : IfcExternalReference, IfcFillStyleSelect
	{
		internal IfcExternallyDefinedHatchStyle() : base() { }
		internal IfcExternallyDefinedHatchStyle(DatabaseIfc db, IfcExternallyDefinedHatchStyle s) : base(db, s) { }
		public IfcExternallyDefinedHatchStyle(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcExternallyDefinedSurfaceStyle : IfcExternalReference, IfcSurfaceStyleElementSelect
	{
		internal IfcExternallyDefinedSurfaceStyle() : base() { }
		internal IfcExternallyDefinedSurfaceStyle(DatabaseIfc db, IfcExternallyDefinedSurfaceStyle s) : base(db, s) { }
		public IfcExternallyDefinedSurfaceStyle(DatabaseIfc db) : base(db) { }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcExternallyDefinedSymbol // DEPRECATED IFC4
	[Serializable]
	public partial class IfcExternallyDefinedTextFont : IfcExternalReference, IfcTextFontSelect
	{
		internal IfcExternallyDefinedTextFont() : base() { }
		internal IfcExternallyDefinedTextFont(DatabaseIfc db, IfcExternallyDefinedTextFont f) : base(db, f) { }
		public IfcExternallyDefinedTextFont(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public abstract partial class IfcExternalReference : BaseClassIfc, IfcLightDistributionDataSourceSelect, IfcObjectReferenceSelect, IfcResourceObjectSelect, NamedObjectIfc
	{ //ABSTRACT SUPERTYPE OF (ONEOF (IfcClassificationReference ,IfcDocumentReference ,IfcExternallyDefinedHatchStyle ,IfcExternallyDefinedSurfaceStyle ,IfcExternallyDefinedSymbol ,IfcExternallyDefinedTextFont ,IfcLibraryReference)); 
		private string mLocation = "$";//  :	OPTIONAL IfcURIReference; ifc2x3 ifclabel
		private string mIdentification = "$";// : OPTIONAL IfcIdentifier; ifc2x3 ItemReference
		private string mName = "";//  : OPTIONAL IfcLabel;
		//INVERSE  
		private SET<IfcExternalReferenceRelationship> mHasExternalReference = new SET<IfcExternalReferenceRelationship>(); //IFC4 SET [0:?] OF IfcExternalReferenceRelationship FOR RelatedResourceObjects;	public override string Name { get { return (mName == "$" ? "" : mName); } set { if (!string.IsNullOrEmpty(value)) mName = value; } } 
		internal SET<IfcResourceConstraintRelationship> mHasConstraintRelationships = new SET<IfcResourceConstraintRelationship>(); //gg
		internal SET<IfcExternalReferenceRelationship> mExternalReferenceForResources = new SET<IfcExternalReferenceRelationship>();//	:	SET [0:?] OF IfcExternalReferenceRelationship FOR RelatingReference;

		public string Location { get { return (mLocation == "$" ? "" : ParserIfc.Decode(mLocation)); } set { mLocation = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Name { get { return mName; } set { mName = value; } } 
		public SET<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } set { mHasExternalReference.Clear();  if (value != null) { mHasExternalReference.CollectionChanged -= mHasExternalReference_CollectionChanged; mHasExternalReference = value; mHasExternalReference.CollectionChanged += mHasExternalReference_CollectionChanged; } } }
		public SET<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }
		public SET<IfcExternalReferenceRelationship> ExternalReferenceForResources { get { return mExternalReferenceForResources; } }

		protected IfcExternalReference() : base() { }
		protected IfcExternalReference(DatabaseIfc db, IfcExternalReference r) 
			: base(db,r) { mLocation = r.mLocation; mIdentification = r.mIdentification; mName = r.mName; }
		protected IfcExternalReference(DatabaseIfc db) : base(db) { }
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

		public void Reference(IfcResourceObjectSelect related)
		{
			if (mExternalReferenceForResources.Count == 0)
				new IfcExternalReferenceRelationship(this, related);
			else
			{
				IfcExternalReferenceRelationship referenceRelationship = mExternalReferenceForResources.First();
				if (!referenceRelationship.RelatedResourceObjects.Contains(related))
					referenceRelationship.RelatedResourceObjects.Add(related);
			}
		}
	}
	[Serializable]
	public partial class IfcExternalReferenceRelationship : IfcResourceLevelRelationship //IFC4
	{
		private IfcExternalReference mRelatingReference;// :	IfcExternalReference;
		private SET<IfcResourceObjectSelect> mRelatedResourceObjects = new SET<IfcResourceObjectSelect>(); //	:	SET [1:?] OF IfcResourceObjectSelect;

		public IfcExternalReference RelatingReference { get { return mRelatingReference as IfcExternalReference; } set { mRelatingReference = value; value.mExternalReferenceForResources.Add(this); } }
		public SET<IfcResourceObjectSelect> RelatedResourceObjects { get { return mRelatedResourceObjects; } set { mRelatedResourceObjects.Clear(); if (value != null) { mRelatedResourceObjects.CollectionChanged -= mRelatedResourceObjects_CollectionChanged; mRelatedResourceObjects = value; mRelatedResourceObjects.CollectionChanged += mRelatedResourceObjects_CollectionChanged; } } } 

		//INVERSE
		public List<IfcExternalReferenceRelationship> HasExternalReference { get { return mHasExternalReference; } }
		internal List<IfcExternalReferenceRelationship> mHasExternalReference = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal IfcExternalReferenceRelationship() : base() { }
		internal IfcExternalReferenceRelationship(DatabaseIfc db, IfcExternalReferenceRelationship r) : base(db,r) { RelatingReference = db.Factory.Duplicate(r.RelatingReference) as IfcExternalReference; RelatedResourceObjects.AddRange(r.mRelatedResourceObjects.ConvertAll(x=>db.Factory.Duplicate(x.Database[x.Index]) as IfcResourceObjectSelect)); }
		public IfcExternalReferenceRelationship(IfcExternalReference reference, IfcResourceObjectSelect related) : this(reference, new List<IfcResourceObjectSelect>() { related }) { }
		public IfcExternalReferenceRelationship(IfcExternalReference reference, IEnumerable<IfcResourceObjectSelect> related)
			: base(reference.mDatabase) { RelatingReference = reference; RelatedResourceObjects.AddRange(related); }

		protected override void initialize()
		{
			base.initialize();
			mRelatedResourceObjects.CollectionChanged += mRelatedResourceObjects_CollectionChanged;
		}
		private void mRelatedResourceObjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcResourceObjectSelect r in e.NewItems)
				{
					if (!r.HasExternalReference.Contains(this))
						r.HasExternalReference.Add(this);
				} 
			}
			if (e.OldItems != null)
			{
				foreach (IfcResourceObjectSelect r in e.OldItems)
					r.HasExternalReference.Remove(this);
			}
		}
	}
	[Serializable]
	public partial class IfcExternalSpatialElement : IfcExternalSpatialStructureElement, IfcSpaceBoundarySelect //NEW IFC4
	{
		internal IfcExternalSpatialElementTypeEnum mPredefinedType = IfcExternalSpatialElementTypeEnum.NOTDEFINED;
		//INVERSE
		internal List<IfcRelSpaceBoundary> mBoundedBy = new List<IfcRelSpaceBoundary>();  //	BoundedBy : SET [0:?] OF IfcRelExternalSpatialElementBoundary FOR RelatingExternalSpatialElement;

		public IfcExternalSpatialElementTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public ReadOnlyCollection<IfcRelSpaceBoundary> BoundedBy { get { return new ReadOnlyCollection<IfcRelSpaceBoundary>( mBoundedBy); } }

		internal IfcExternalSpatialElement() : base() { }
		internal IfcExternalSpatialElement(DatabaseIfc db, IfcExternalSpatialElement e, DuplicateOptions options) : base(db, e, options) { mPredefinedType = e.mPredefinedType; }
		public IfcExternalSpatialElement(IfcSite host, string name, IfcExternalSpatialElementTypeEnum te)
			: base(host, name) { mPredefinedType = te; }
		
		public void AddBoundary(IfcRelSpaceBoundary boundary) { mBoundedBy.Add(boundary); }
	}
	[Serializable]
	public abstract partial class IfcExternalSpatialStructureElement : IfcSpatialElement //	ABSTRACT SUPERTYPE OF(IfcExternalSpatialElement)
	{
		protected IfcExternalSpatialStructureElement() : base() { }
		protected IfcExternalSpatialStructureElement(IfcObjectPlacement pl) : base(pl) { }
		protected IfcExternalSpatialStructureElement(IfcSite host, string name) : base(host, name) { }
		protected IfcExternalSpatialStructureElement(DatabaseIfc db, IfcExternalSpatialStructureElement e, DuplicateOptions options) : base(db, e, options) { }
	}
	[Serializable]
	public partial class IfcExtrudedAreaSolid : IfcSweptAreaSolid // SUPERTYPE OF(IfcExtrudedAreaSolidTapered)
	{
		private int mExtrudedDirection;//: IfcDirection;
		private double mDepth;// : IfcPositiveLengthMeasure;

		public IfcDirection ExtrudedDirection { get { return mDatabase[mExtrudedDirection] as IfcDirection; } set { mExtrudedDirection = value.mIndex; } }
		public double Depth { get { return mDepth; } set { mDepth = value; } }

		internal IfcExtrudedAreaSolid() : base() { }
		internal IfcExtrudedAreaSolid(DatabaseIfc db, IfcExtrudedAreaSolid e, DuplicateOptions options) : base(db, e, options) { ExtrudedDirection = db.Factory.Duplicate(e.ExtrudedDirection) as IfcDirection; mDepth = e.mDepth; }
		public IfcExtrudedAreaSolid(IfcProfileDef prof, double depth) : base(prof) { ExtrudedDirection = mDatabase.Factory.ZAxis; mDepth = depth; }
		public IfcExtrudedAreaSolid(IfcProfileDef prof, IfcDirection dir, double depth) : base(prof) { mExtrudedDirection = dir.mIndex; mDepth = depth; }
		public IfcExtrudedAreaSolid(IfcProfileDef prof, IfcAxis2Placement3D position, double depth) : base(prof, position) { ExtrudedDirection = mDatabase.Factory.ZAxis; mDepth = depth; }
		public IfcExtrudedAreaSolid(IfcProfileDef prof, IfcAxis2Placement3D position, IfcDirection dir, double depth) : base(prof, position) { if(dir != null) mExtrudedDirection = dir.mIndex; mDepth = depth; }
	}
	[Serializable]
	public partial class IfcExtrudedAreaSolidTapered : IfcExtrudedAreaSolid
	{
		private int mEndSweptArea;//: IfcProfileDef 
		public IfcProfileDef EndSweptArea { get { return mDatabase[mEndSweptArea] as IfcProfileDef; } set { mEndSweptArea = value.mIndex; } }

		internal IfcExtrudedAreaSolidTapered() : base() { }
		internal IfcExtrudedAreaSolidTapered(DatabaseIfc db, IfcExtrudedAreaSolidTapered e, DuplicateOptions options) : base(db, e, options) { EndSweptArea = db.Factory.Duplicate(e.EndSweptArea, options) as IfcProfileDef; }
		public IfcExtrudedAreaSolidTapered(IfcParameterizedProfileDef start, IfcAxis2Placement3D placement, double depth, IfcParameterizedProfileDef end) : base(start, placement, new IfcDirection(start.mDatabase,0,0,1), depth) { EndSweptArea = end; }
		public IfcExtrudedAreaSolidTapered(IfcDerivedProfileDef start, IfcAxis2Placement3D placement, double depth, IfcDerivedProfileDef end) : base(start, placement,new IfcDirection(start.mDatabase,0,0,1), depth ) { EndSweptArea = end; }
	}
}
