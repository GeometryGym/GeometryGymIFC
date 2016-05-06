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
using System.Globalization;
using System.Threading;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{ 
	public enum Schema {  IFC2x3, IFC4, IFC4A1 };
	public enum ModelView { Ifc4Reference, Ifc4DesignTransfer, Ifc4NotAssigned,Ifc2x3Coordination, If2x3NotAssigned };
	public partial class DatabaseIfc
	{
		internal partial class Aggregate
		{
			//internal List<IfcProfileDef> mProfiles = new List<IfcProfileDef>();
			//internal List<IfcWorkPlan> mWorkPlans = new List<IfcWorkPlan>();
			//internal List<IfcRepresentationMap> mRepMaps = new List<IfcRepresentationMap>();
			//internal List<IfcApplication> mApplications = new List<IfcApplication>();
			internal List<IfcBuildingStorey> mBuildingStories = new List<IfcBuildingStorey>();
			internal List<IfcComplexProperty> mComplexProperties = new List<IfcComplexProperty>();
			internal List<IfcCoordinateOperation> mCoordinateOperations = new List<IfcCoordinateOperation>();
			internal List<IfcEdgeCurve> mEdgeCurves = new List<IfcEdgeCurve>();
			internal List<IfcExternalReferenceRelationship> mExternalRelationships = new List<IfcExternalReferenceRelationship>();
			//internal List<IfcExtrudedAreaSolid> mExtrusions = new List<IfcExtrudedAreaSolid>();
			internal List<IfcGeometricRepresentationSubContext> mGeomContexts = new List<IfcGeometricRepresentationSubContext>();
			internal List<IfcGrid> mGrids = new List<IfcGrid>();
			internal List<IfcGroup> mGroups = new List<IfcGroup>();
			internal List<IfcIndexedColourMap> mIndexedColourMap = new List<IfcIndexedColourMap>();
			internal List<IfcIndexedTextureMap> mIndexedTextureMap = new List<IfcIndexedTextureMap>();
			internal List<IfcLocalPlacement> mLocalPlacements = new List<IfcLocalPlacement>();
			internal List<IfcMappedItem> mMappedItems = new List<IfcMappedItem>();
			internal List<IfcMaterial> mMaterials = new List<IfcMaterial>();
			internal List<IfcMaterialProperties> mMaterialProperties = new List<IfcMaterialProperties>();
			internal List<IfcMaterialPropertiesSuperSeded> mMaterialPropertiesSS = new List<IfcMaterialPropertiesSuperSeded>();
			//internal List<IfcMechanicalFastener> mFasteners = new List<IfcMechanicalFastener>();
			internal List<IfcOwnerHistory> mOwnHistories = new List<IfcOwnerHistory>();
			internal List<IfcPresentationLayerAssignment> mPresentationLayerAssignments = new List<IfcPresentationLayerAssignment>();
			internal List<IfcProduct> mProducts = new List<IfcProduct>();
			internal List<IfcProductRepresentation> mProductReps = new List<IfcProductRepresentation>();
			internal List<IfcPropertySet> mPropertySets = new List<IfcPropertySet>();
			internal List<IfcRelationship> mRelationships = new List<IfcRelationship>();
			internal List<IfcRepresentation> mRepresentations = new List<IfcRepresentation>();
			internal List<IfcResourceConstraintRelationship> mConstraintRelationships = new List<IfcResourceConstraintRelationship>();
			internal List<IfcShapeAspect> mShapeAspects = new List<IfcShapeAspect>();
			internal List<IfcSlab> mSlabs = new List<IfcSlab>();
			internal List<IfcStructuralItem> mStructItems = new List<IfcStructuralItem>();
			internal List<IfcStyledItem> mStyledItems = new List<IfcStyledItem>();
			internal List<IfcTypeProduct> mTypeProducts = new List<IfcTypeProduct>();
			internal List<IfcWall> mWalls = new List<IfcWall>();
			internal List<IfcZone> mZones = new List<IfcZone>();

			internal Aggregate() { }
			partial void setCustomAggregate(BaseClassIfc obj);
			internal void setAggregate(BaseClassIfc obj)
			{
				IfcProduct product = obj as IfcProduct;
				if (product != null)
				{
					mProducts.Add(product);
					IfcBuildingStorey buildingStorey = obj as IfcBuildingStorey;
					if (buildingStorey != null)
						mBuildingStories.Add(buildingStorey);
					IfcGrid grid = obj as IfcGrid;
					if (grid != null)
						mGrids.Add(grid);
					IfcSlab slab = product as IfcSlab;
					if (slab != null)
						mSlabs.Add(slab);
					IfcStructuralItem structuralItem = obj as IfcStructuralItem;
					if (structuralItem != null)
						mStructItems.Add(structuralItem);
					IfcWall wall = product as IfcWall;
					if (wall != null)
						mWalls.Add(wall);
					return;
				}
				//IfcApplication application = obj as IfcApplication;
				//if (application != null)
				//	mApplications.Add(application);
				IfcComplexProperty cp = obj as IfcComplexProperty;
				if (cp != null)
				{
					mComplexProperties.Add(cp);
					return;
				}
				IfcCoordinateOperation coordOp = obj as IfcCoordinateOperation;
				if (coordOp != null)
				{
					mCoordinateOperations.Add(coordOp);
					return;
				}

				IfcEdgeCurve edgeCurve = obj as IfcEdgeCurve;
				if (edgeCurve != null)
				{
					mEdgeCurves.Add(edgeCurve);
					return;
				}
				IfcExternalReferenceRelationship externalReferenceRelationship = obj as IfcExternalReferenceRelationship;
				if (externalReferenceRelationship != null)
				{
					mExternalRelationships.Add(externalReferenceRelationship);
					return;
				}
				//IfcExtrudedAreaSolid extrudedAreaSolid = result as IfcExtrudedAreaSolid;
				//if(extrudedAreaSolid != null)
				//{
				//	if(result as IfcExtrudedAreaSolidTapered == null)
				//		aggregate.mExtrusions.Add(extrudedAreaSolid);
				//	return extrudedAreaSolid;
				//}
				IfcGeometricRepresentationSubContext geometricRepresentationContext = obj as IfcGeometricRepresentationSubContext;
				if (geometricRepresentationContext != null)
				{
					mGeomContexts.Add(geometricRepresentationContext);
					return;
				}


				IfcGroup group = obj as IfcGroup;
				if (group != null)
				{
					IfcZone zone = group as IfcZone;
					if (zone != null)
					{
						mZones.Add(zone);
						return;
					}
					mGroups.Add(group);
					return;
				}
				IfcIndexedColourMap indexedColourMap = obj as IfcIndexedColourMap;
				if (indexedColourMap != null)
				{
					mIndexedColourMap.Add(indexedColourMap);
					return;
				}
				IfcIndexedTextureMap indexedTextureMap = obj as IfcIndexedTextureMap;
				if (indexedTextureMap != null)
				{
					mIndexedTextureMap.Add(indexedTextureMap);
					return;
				}
				IfcLocalPlacement localPlacement = obj as IfcLocalPlacement;
				if (localPlacement != null)
				{
					mLocalPlacements.Add(localPlacement);
					return;
				}
				IfcMappedItem mi = obj as IfcMappedItem;
				if (mi != null)
				{
					mMappedItems.Add(mi);
					return;
				}
				IfcMaterial material = obj as IfcMaterial;
				if (material != null)
				{
					mMaterials.Add(material);
					return;
				}
				IfcMaterialProperties materialProperties = obj as IfcMaterialProperties;
				if (materialProperties != null)
				{
					mMaterialProperties.Add(materialProperties);
					return;
				}
				IfcMaterialPropertiesSuperSeded materialPropertiesSS = obj as IfcMaterialPropertiesSuperSeded;
				if (materialPropertiesSS != null)
				{
					mMaterialPropertiesSS.Add(materialPropertiesSS);
					return;
				}
				//	IfcMechanicalFastener mechanicalFastener = result as IfcMechanicalFastener;
				//if(mechanicalFastener != null)
				//{
				//	mFasteners.Add(mechanicalFastener);
				//	return mechanicalFastener;
				//}
				//IfcOwnerHistory ownerHistory = result as IfcOwnerHistory;
				//if(ownerHistory != null)
				//{
				//	mOwnHistories.Add(ownerHistory);
				//	return ownerHistory;
				//} 
				IfcPresentationLayerAssignment presentationLayerAssignment = obj as IfcPresentationLayerAssignment;
				if (presentationLayerAssignment != null)
				{
					mPresentationLayerAssignments.Add(presentationLayerAssignment);
					return;
				}
				IfcProductRepresentation productRepresentation = obj as IfcProductRepresentation;
				if (productRepresentation != null)
				{
					mProductReps.Add(productRepresentation);
					return;
				}
				IfcPropertySet propSet = obj as IfcPropertySet;
				if (propSet != null)
				{
					mPropertySets.Add(propSet);
					return;
				}
				//IfcProfileDef profileDef = obj as IfcProfileDef;
				//if (profileDef != null)
				//{
				//	mProfiles.Add(profileDef);
				//	return profileDef;
				//}
				IfcRelationship relationship = obj as IfcRelationship;
				if (relationship != null)
				{
					mRelationships.Add(relationship);
					return;
				}
				IfcRepresentation representation = obj as IfcRepresentation;
				if (representation != null)
				{
					mRepresentations.Add(representation);
					return;
				}
				//IfcRepresentationMap representationMap = obj as IfcRepresentationMap;
				//if (representationMap != null)
				//{
				//	mRepMaps.Add(representationMap);
				//	return representationMap;
				//}
				IfcResourceConstraintRelationship rcr = obj as IfcResourceConstraintRelationship;
				if (rcr != null)
				{
					mConstraintRelationships.Add(rcr);
					return;
				}
				IfcShapeAspect shapeAspect = obj as IfcShapeAspect;
				if (shapeAspect != null)
				{
					mShapeAspects.Add(shapeAspect);
					return;
				}
				IfcStyledItem styledItem = obj as IfcStyledItem;
				if (styledItem != null)
				{
					mStyledItems.Add(styledItem);
					return;
				}

				IfcTypeProduct typeProduct = obj as IfcTypeProduct;
				if (typeProduct != null)
				{
					mTypeProducts.Add(typeProduct);
					return;
				}
				setCustomAggregate(obj);
			}
			partial void InitializeOthers(string folder);
			internal void RelateObjects(string folder)
			{
				try
				{
					int icounter;
					for (icounter = 0; icounter < mComplexProperties.Count; icounter++)
						mComplexProperties[icounter].relate();
					for (icounter = 0; icounter < mCoordinateOperations.Count; icounter++)
						mCoordinateOperations[icounter].Relate();
					for (icounter = 0; icounter < mEdgeCurves.Count; icounter++)
						mEdgeCurves[icounter].associate();
					foreach (IfcExternalReferenceRelationship er in mExternalRelationships)
					{
						try
						{
							er.relate();
						}
						catch (Exception) { }
					}
					for (icounter = 0; icounter < mProducts.Count; icounter++)
						mProducts[icounter].relate();
					foreach (IfcIndexedColourMap icm in mIndexedColourMap)
						icm.relate();
					foreach (IfcIndexedTextureMap itm in mIndexedTextureMap)
						itm.relate();
					for (icounter = 0; icounter < mLocalPlacements.Count; icounter++)
						mLocalPlacements[icounter].setReference();
					for (icounter = 0; icounter < mMappedItems.Count; icounter++)
						mMappedItems[icounter].MappingSource.mMapUsage.Add(mMappedItems[icounter]);
					for (icounter = 0; icounter < mMaterialProperties.Count; icounter++)
						mMaterialProperties[icounter].relate();
					for (icounter = 0; icounter < mMaterialPropertiesSS.Count; icounter++)
						mMaterialPropertiesSS[icounter].relate();
					for (icounter = 0; icounter < mPresentationLayerAssignments.Count; icounter++)
						mPresentationLayerAssignments[icounter].relate();
					for (icounter = 0; icounter < mProductReps.Count; icounter++)
					{
						try
						{
							mProductReps[icounter].relate();
						}
						catch (Exception) { }
					}
					for (icounter = 0; icounter < mPropertySets.Count; icounter++)
						mPropertySets[icounter].relate();
					for (icounter = 0; icounter < mRelationships.Count; icounter++)
					{
						try
						{
							mRelationships[icounter].relate();
						}
						catch (Exception) { }
					}
					foreach (IfcRepresentation r in mRepresentations)
						r.relate();
					foreach (IfcResourceConstraintRelationship rc in mConstraintRelationships)
						rc.relate();
					foreach (IfcShapeAspect sa in mShapeAspects)
						sa.relate();
					for (icounter = 0; icounter < mTypeProducts.Count; icounter++)
					{
						List<IfcRepresentationMap> repMaps = mTypeProducts[icounter].RepresentationMaps;
						for (int jcounter = 0; jcounter < repMaps.Count; jcounter++)
							repMaps[jcounter].mTypeProducts.Add(mTypeProducts[icounter]);
					}
					for (icounter = 0; icounter < mStyledItems.Count; icounter++)
						mStyledItems[icounter].associateItem();
					InitializeOthers(folder);
				}
				catch (Exception)
				{
					//logError.printMessage("XXX " + x.ToString());
				}
			}
		}
		internal Guid id = Guid.NewGuid();
		internal int mNextBlank = 1;

		internal Schema mSchema = Schema.IFC2x3;

		internal ModelView mModelView = ModelView.If2x3NotAssigned;
		internal bool mAccuratePreview = false;// mCoordinationView = false; 
		public string FolderPath { get; set; } = "";
		public string FileName { get; set; } = "";
		internal double mPlaneAngleToRadians = 1;
		internal bool mTimeInDays = false;
		public int NextObjectRecord { set { mNextBlank = value; } }
		public Schema IFCSchema
		{ 
			get { return mSchema; }  
			set { mSchema = value; } 
		}
		public ModelView ModelView
		{
			get { return mModelView; }
			set { mModelView = value; }
		}
		public double Tolerance
		{
			get { return mModelTolerance; }
			set { mModelTolerance = Math.Min(0.0005 / mModelSIScale, value); }
		}
		public double ScaleSI
		{
			get { return mModelSIScale; }
			set { mModelSIScale = value; }
		}
		
		private double mModelTolerance = 0.0001,mModelSIScale = 1;
		public IfcProject Project
		{
			get { return mContext as IfcProject; }
		}
		internal List<BaseClassIfc> mIfcObjects = new List<BaseClassIfc>(new BaseClassIfc[] { new BaseClassIfc() }); 

		public int RecordCount { get { return mIfcObjects.Count; } }
		public BaseClassIfc this[int index]
		{
			get { return (index < mIfcObjects.Count ? mIfcObjects[index] : null); }
			//set {  }
		}
		private IfcCartesianPoint mOrigin = null, mWorldOrigin = null,mOrigin2d = null;
		internal IfcDirection mXAxis, mYAxis, mZAxis;
		//internal int mTempWorldCoordinatePlacement = 0;
		private IfcAxis2Placement3D mWorldCoordinatePlacement;
		internal IfcAxis2Placement3D mPlacementPlaneXY;
		internal IfcAxis2Placement2D m2DPlaceOrigin;
		internal IfcSIUnit mSILength, mSIArea, mSIVolume;
		internal IfcGeometricRepresentationContext mGeomRepContxt;
		internal IfcGeometricRepresentationSubContext mGeoRepSubContxtBody = null, mGeoRepSubContxtAxis=null, mGeoRepSubContxtAnalysisSurface = null;
		private IfcApplication mApplication = null;
		internal IfcApplication Application
		{
			get
			{
				if (mApplication == null)
					mApplication = new IfcApplication(this);
				return mApplication;
			}
		}
		private IfcPersonAndOrganization mPersonOrganization = null;
		internal IfcPersonAndOrganization PersonOrganization
		{
			get
			{
				if (mPersonOrganization == null)
					mPersonOrganization = new IfcPersonAndOrganization(this);
				return mPersonOrganization;
			}
		}
		private IfcOwnerHistory mOwnerHistoryCreate,mOwnerHistoryModify,mOwnerHistoryDelete;
		internal IfcOwnerHistory OwnerHistory(IfcChangeActionEnum changeAction)
		{
			if(changeAction == IfcChangeActionEnum.ADDED)
			{
				if (mOwnerHistoryCreate == null)
					mOwnerHistoryCreate = new IfcOwnerHistory(PersonOrganization, Application, IfcChangeActionEnum.ADDED);
				return mOwnerHistoryCreate;
			}
			if(changeAction == IfcChangeActionEnum.DELETED)
			{
				if (mOwnerHistoryDelete == null)
					mOwnerHistoryDelete = new IfcOwnerHistory(PersonOrganization, Application, IfcChangeActionEnum.DELETED);
				return mOwnerHistoryDelete;
			}
			if(changeAction == IfcChangeActionEnum.MODIFIED)
			{
				if (mOwnerHistoryModify == null)
					mOwnerHistoryModify = new IfcOwnerHistory(PersonOrganization, Application, IfcChangeActionEnum.MODIFIED);
				return mOwnerHistoryModify;
			}
			return null;
		}

		
		internal IfcContext mContext = null;
		
		internal HashSet<string> mGlobalIDs = new HashSet<string>();

		partial void printError(string str);
		internal void logError(string str) { printError(str); }
		partial void getApplicationFullName(ref string app);
		partial void getApplicationIdentifier(ref string app);
		partial void getApplicationDeveloper(ref string app);

		private string mApplicationFullName = "", mApplicationIdentifier = "", mApplicationDeveloper = "";
		public string ApplicationFullName
		{
			get
			{
				if (string.IsNullOrEmpty(mApplicationFullName))
				{
					getApplicationFullName(ref mApplicationFullName);
					if (string.IsNullOrEmpty(mApplicationFullName))
					{
						try
						{
							Assembly assembly = Assembly.GetExecutingAssembly();
							AssemblyName name = assembly.GetName();
							return name.Name;
						}
						catch (Exception) { return "Unknown Application"; }
					}
				}
				return mApplicationFullName;
			}
			set { mApplicationFullName = value; }
		}
		public string ApplicationIdentifier
		{
			get
			{
				if (string.IsNullOrEmpty(mApplicationIdentifier))
				{
					getApplicationIdentifier(ref mApplicationIdentifier);
					if (string.IsNullOrEmpty(mApplicationIdentifier))
						return ApplicationFullName;
				}
				return mApplicationIdentifier;
			}
			set { mApplicationIdentifier = value; }
		}
		public string ApplicationDeveloper
		{
			get
			{
				if (string.IsNullOrEmpty(mApplicationDeveloper))
					getApplicationDeveloper(ref mApplicationDeveloper);
				return (string.IsNullOrEmpty(mApplicationDeveloper) ? "Unknown" : mApplicationDeveloper);
			}
			set { mApplicationDeveloper = value; }
		}

		internal string getHeaderString(string fileName)
		{
			string vd = (mModelView == ModelView.Ifc2x3Coordination ? "CoordinationView_V2" :
				(mModelView == ModelView.Ifc4Reference ? "ReferenceView_V1" : (mModelView == ModelView.Ifc4DesignTransfer ? "DesignTransferView_V1" : "notYetAssigned")));
			string hdr = "ISO-10303-21;\r\nHEADER;\r\nFILE_DESCRIPTION(('ViewDefinition [" + vd + "]'),'2;1');\r\n";

			hdr += "FILE_NAME(\r\n";
			hdr += "/* name */ '" + ParserIfc.Encode(fileName.Replace("\\", "\\\\")) + "',\r\n";
			DateTime now = DateTime.Now;
			hdr += "/* time_stamp */ '" + now.Year + "-" + (now.Month < 10 ? "0" : "") + now.Month + "-" + (now.Day < 10 ? "0" : "") + now.Day + "T" + (now.Hour < 10 ? "0" : "") + now.Hour + ":" + (now.Minute < 10 ? "0" : "") + now.Minute + ":" + (now.Second < 10 ? "0" : "") + now.Second + "',\r\n";
			hdr += "/* author */ ('" + System.Environment.UserName + "'),\r\n";
			hdr += "/* organization */ ('Unknown'),\r\n";
			hdr += "/* preprocessor_version */ 'GeomGymIFC by Geometry Gym Pty Ltd',\r\n";
			hdr += "/* originating_system */ '" + ApplicationFullName + "',\r\n";

			hdr += "/* authorization */ 'None');\r\n\r\n";
			hdr += "FILE_SCHEMA (('" + (mSchema == Schema.IFC2x3 ? "IFC2X3" : "IFC4") + "'));\r\n";
			hdr += "ENDSEC;\r\n";
			hdr += "\r\nDATA;";
			return hdr;
		}
		internal string getFooterString() { return "ENDSEC;\r\n\r\nEND-ISO-10303-21;\r\n\r\n"; } 
		internal bool mDrawBuildElemProx = true, mDrawSpaceReps = false,mDrawFurnishing = true,mDrawFasteners = false,mDrawPlates = true,mDrawRailings = true, mOutputEssential = false, mCFSasMesh = false;
		
		public DatabaseIfc(string fileName) : base() 
		{
			Aggregate aggregate = new Aggregate();
			ReadFile(getStreamReader(fileName),aggregate,0);
		}
		public DatabaseIfc(TextReader stream) : base()
		{
			Aggregate aggregate = new Aggregate();
			ReadFile(stream, aggregate, 0);
		}
		public DatabaseIfc(ModelView view) : this(true, view) { }
		public DatabaseIfc(bool generate, ModelView view) : this(generate, view == ModelView.Ifc2x3Coordination || view == ModelView.If2x3NotAssigned ? Schema.IFC2x3 : Schema.IFC4A1,view) { }
		public DatabaseIfc(bool generate, Schema schema) : this(generate,schema,schema == Schema.IFC2x3 ? ModelView.If2x3NotAssigned : ModelView.Ifc4NotAssigned) { }
		private DatabaseIfc(bool generate,Schema schema, ModelView view)
		{ 
			mSchema = schema;
			mModelView = view;
#if(RHINO)
			mModelSIScale = 1 / GGYM.Units.mLengthConversion[(int) GGYM.GGYMRhino.GGRhino.ActiveUnits()];
			Tolerance = Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
#endif 
			if (mSchema == Schema.IFC2x3 || mSchema == Schema.IFC4)
			{
				OwnerHistory(IfcChangeActionEnum.ADDED);
			}
			mGeomRepContxt = new IfcGeometricRepresentationContext(this, 3, Tolerance) { ContextType = "Model" };
			mGeoRepSubContxtAxis = new IfcGeometricRepresentationSubContext(mGeomRepContxt, 0, IfcGeometricProjectionEnum.MODEL_VIEW) { ContextIdentifier = "Axis" };
			mGeoRepSubContxtBody = new IfcGeometricRepresentationSubContext(mGeomRepContxt, 0, IfcGeometricProjectionEnum.MODEL_VIEW) { ContextIdentifier = "Body" };

			if (generate)
				initData();
		} 
		
		
			
		
		internal void initData()
		{ 
			initGeom();
			mSILength = new IfcSIUnit(this, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.NONE, IfcSIUnitName.METRE);
			mSIArea = new IfcSIUnit(this, IfcUnitEnum.AREAUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SQUARE_METRE);
			mSIVolume = new IfcSIUnit(this, IfcUnitEnum.VOLUMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.CUBIC_METRE);
		}
		internal void initGeom()
		{
			IfcCartesianPoint point = Origin;
			IfcDirection direction = XAxis;
			direction = YAxis;
			direction = ZAxis;
			IfcAxis2Placement pl = this.WorldCoordinatePlacement;
			IfcAxis2Placement2D placement = Origin2dPlace; 
		}
		public override string ToString()
		{  
			//IFCModel im = new IFCModel(mIFC2x3,true); 
			string result = getHeaderString("") + "\r\n";
			for (int icounter = 1; icounter < mIfcObjects.Count; icounter++)
			{
				BaseClassIfc ie = mIfcObjects[icounter];
				if (ie != null)
				{
					string str = ie.ToString();
					if (str != "")
						result += str +"\r\n";
				} 
			}
			return result + getFooterString();
		}
		internal IfcAxis2Placement2D Origin2dPlace
		{
			get
			{
				if (m2DPlaceOrigin == null)
					m2DPlaceOrigin = new IfcAxis2Placement2D(new IfcCartesianPoint(this, 0, 0));
				return m2DPlaceOrigin;
			}
		}
		internal IfcCartesianPoint WorldOrigin
		{
			get
			{
				if(mWorldOrigin == null)
					mWorldOrigin = WorldCoordinatePlacement.Location;
				return mWorldOrigin;
			}
		}
		internal IfcCartesianPoint Origin2d
		{
			get
			{
				if (mOrigin2d == null)
					mOrigin2d = new IfcCartesianPoint(this,0, 0);
				return mOrigin2d;
			}
		} 
		internal IfcAxis2Placement3D WorldCoordinatePlacement
		{
			get
			{
				if (mWorldCoordinatePlacement == null)
				{
					if (mContext != null)
					{
						List<IfcRepresentationContext> contexts = mContext.RepresentationContexts;
						foreach (IfcRepresentationContext context in contexts)
						{
							IfcGeometricRepresentationContext grc = context as IfcGeometricRepresentationContext;
							if (grc == null)
								continue;
							IfcAxis2Placement3D pl = grc.WorldCoordinateSystem as IfcAxis2Placement3D;
							if (pl != null)
							{
								mWorldCoordinatePlacement = pl;
								break;
							}
						}
					}
					if (mWorldCoordinatePlacement == null)
					{
						mWorldCoordinatePlacement = new IfcAxis2Placement3D(new IfcCartesianPoint(this, 0, 0, 0), mZAxis, mXAxis);
						if (mContext != null)
						{
							List<IfcRepresentationContext> contexts = mContext.RepresentationContexts;
							foreach (IfcRepresentationContext context in contexts)
							{
								IfcGeometricRepresentationContext grc = context as IfcGeometricRepresentationContext;
								if (grc != null)
								{
									grc.WorldCoordinateSystem = mWorldCoordinatePlacement;
									return mWorldCoordinatePlacement;
								}
							}
						}
					}
				}
				return mWorldCoordinatePlacement;
			}
		}
		internal IfcAxis2Placement3D PlaneXYPlacement
		{
			get
			{
				if (mPlacementPlaneXY == null)
					mPlacementPlaneXY = new IfcAxis2Placement3D(this);
				return mPlacementPlaneXY;
			}
		}
		internal IfcCartesianPoint Origin
		{
			get
			{
				if (mOrigin == null)
					mOrigin = new IfcCartesianPoint(this, 0, 0, 0);
				return mOrigin;
			}
		}
		internal IfcDirection XAxis
		{
			get
			{
				if (mXAxis == null)
					mXAxis = new IfcDirection(this, 1, 0, 0);
				return mXAxis;
			}
		}
		internal IfcDirection YAxis
		{
			get
			{
				if (mYAxis == null)
					mYAxis = new IfcDirection(this, 0, 1, 0);
				return mYAxis;
			}
		}
		internal IfcDirection ZAxis
		{
			get
			{
				if (mZAxis == null)
					mZAxis = new IfcDirection(this, 0, 0, 1);
				return mZAxis;
			}
		}

		internal StreamReader getStreamReader(string fileName)
		{
			string ext = Path.GetExtension(fileName);
#if (!NOIFCZIP)
			if (string.Compare(ext, ".IFCZIP", true) == 0)
			{
				System.IO.Compression.ZipArchive za = System.IO.Compression.ZipFile.OpenRead(fileName);
				if (za.Entries.Count != 1)
				{
					return null;
				}
				return new StreamReader(za.Entries[0].Open(), System.Text.Encoding.GetEncoding("windows-1252"));
			}
#endif
			FileName = fileName;
			FolderPath = Path.GetDirectoryName(fileName);
			return new StreamReader(fileName, System.Text.Encoding.GetEncoding("windows-1252"));
		}

		private IfcContext ReadFile(TextReader sr, Aggregate aggregate, int offset)
		{
			mSchema = Schema.IFC2x3;
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			string strLine = sr.ReadLine(), str = "";
			DateTime s = DateTime.Now;
			if (offset > 0)
			{
				while (strLine != null)
				{
					int index = strLine.IndexOf("/*");
					if (index >= 0)
					{
						string str2 = "", str3 = strLine;
						if (index > 0)
							str2 = strLine.Substring(0, index);
						int index2 = str3.IndexOf("*/");
						while (index2 < 0)
						{
							str3 = sr.ReadLine();
							if (strLine == null)
								break;
							index2 = str3.IndexOf("*/");
						}
						strLine = str2;
						if (strLine != null && index2 + 2 < str3.Length)
							strLine += str3.Substring(index2 + 2);
					}
					while (!strLine.EndsWith(";"))
					{
						str = sr.ReadLine();
						if (str != null)
						{
							index = str.IndexOf("/*");
							if (index >= 0)
							{
								string str2 = "", str3 = str;
								if (index > 0)
									str2 = strLine.Substring(0, index);
								int index2 = str3.IndexOf("*/");
								while (index2 < 0)
								{
									str3 = sr.ReadLine();
									if (strLine == null)
										break;
									index2 = str3.IndexOf("*/");
								}
								str = str2;
								if (strLine != null && index2 + 2 < str3.Length)
									str += str3.Substring(index2 + 2);
							}
							strLine += str;
							strLine.Trim();
						}
						else
						{
							strLine = strLine.Trim();
							strLine += ";";
							break;
						}
					}
					strLine = ParserSTEP.offsetSTEPRecords(strLine, offset);
					try
					{
						InterpretLine(strLine, aggregate);
					}
					catch (Exception ex) { logError("XXX Error in line " + strLine + " " + ex.Message); }
					strLine = sr.ReadLine();
				}
			}
			else
			{
				while (strLine != null)
				{
					int index = strLine.IndexOf("/*");
					if (index >= 0)
					{
						string str2 = "", str3 = strLine;
						if (index > 0)
							str2 = strLine.Substring(0, index);
						int index2 = str3.IndexOf("*/");
						while (index2 < 0)
						{
							str3 = sr.ReadLine();
							if (strLine == null)
								break;
							index2 = str3.IndexOf("*/");
						}
						strLine = str2;
						if (strLine != null && index2 + 2 < str3.Length)
							strLine += str3.Substring(index2 + 2);
					}
					strLine = strLine.Trim();
					while (!strLine.EndsWith(";"))
					{
						str = sr.ReadLine();
						if (str != null)
						{
							index = str.IndexOf("/*");
							if (index >= 0)
							{
								string str2 = "", str3 = str;
								if (index > 0)
									str2 = strLine.Substring(0, index);
								int index2 = str3.IndexOf("*/");
								while (index2 < 0)
								{
									str3 = sr.ReadLine();
									if (strLine == null)
										break;
									index2 = str3.IndexOf("*/");
								}
								str = str2;
								if (strLine != null && index2 + 2 < str3.Length)
									str += str3.Substring(index2 + 2);
							}
							strLine += str;
							strLine.Trim();
						}
						else
						{
							strLine = strLine.Trim();
							strLine += ";";
							break;
						}
					}
					try
					{
						InterpretLine(strLine, aggregate);
					}
					catch (Exception ex) { logError("XXX Error in line " + strLine + " " + ex.Message); }
					strLine = sr.ReadLine();
				}
			}
			sr.Close();
			Thread.CurrentThread.CurrentCulture = current; 
			postImport(aggregate);

			return mContext;
		}

		partial void customPostImport(Aggregate aggregate);
		private void postImport(Aggregate aggregate) 
		{
			mWorldCoordinatePlacement = null;
			aggregate.RelateObjects(FolderPath);
			if(mContext != null)
			{
				mContext.initializeUnitsAndScales();
				
				if (mContext.mRepresentationContexts.Count > 0)
					mGeomRepContxt = mIfcObjects[mContext.mRepresentationContexts[0]] as IfcGeometricRepresentationContext;
				if (mContext.mDeclares.Count == 0)
				{
					List<IfcDefinitionSelect> lds = aggregate.mTypeProducts.ConvertAll(x => (IfcDefinitionSelect)x);
					IfcRelDeclares rd = new IfcRelDeclares(mContext, lds) { Name = "DeclaredTypes" };
				}
			}
			customPostImport(aggregate);
			
		}
		internal BaseClassIfc InterpretLine(string line,Aggregate aggregate)
		{
			if (line.StartsWith("ISO"))
				return null;
			string ts = line.Trim().Replace(" ", "");
			if (ts.StartsWith("FILE_SCHEMA(('IFC2X4", true, System.Globalization.CultureInfo.CurrentCulture) ||
					ts.StartsWith("FILE_SCHEMA(('IFC4", true, System.Globalization.CultureInfo.CurrentCulture))
			{ 
				mSchema = Schema.IFC4;
				return null;
			}
			BaseClassIfc result = ParserIfc.ParseLine(line, mSchema);
			if (result == null)
			{
				int ifcID = 0;
				string kw = "", str = "";
				ParserIfc.GetKeyWord(line, out ifcID, out kw, out str);
				if (string.IsNullOrEmpty(kw))
					return null;

				result = new BaseClassIfc(ifcID, kw,str);
			}
			if(result == null)
				return null;
			IfcApplication application = result as IfcApplication;
			if (application != null)
			{
				IfcApplication ea = mApplication;
				if (ea != null && ea.mVersion == application.mVersion)
				{
					if (string.Compare(ea.ApplicationFullName, application.ApplicationFullName, true) == 0)
					{
						if (string.Compare(ea.mApplicationIdentifier, application.mApplicationIdentifier) == 0)
						{
							mIfcObjects[ea.mIndex] = null;
							mApplication = application;
							OwnerHistory(IfcChangeActionEnum.ADDED).mLastModifyingApplication = application.mIndex;
							if (mOwnerHistoryModify != null)
								mOwnerHistoryModify.mLastModifyingApplication = application.mIndex;
						}
					}
				}
			}
			IfcContext context = result as IfcContext;
			if (context != null)
				mContext = context;
			IfcGeometricRepresentationContext geometricRepresentationContext = result as IfcGeometricRepresentationContext;
			if (geometricRepresentationContext != null)
			{
				if (string.Compare(geometricRepresentationContext.mContextType, "Plan", true) != 0)
					mGeomRepContxt = geometricRepresentationContext;
				if (geometricRepresentationContext.mPrecision > 1e-6)
					Tolerance = geometricRepresentationContext.mPrecision;

			}
			IfcSIUnit unit = result as IfcSIUnit;
			if(unit != null)
			{
				if (unit.Name == IfcSIUnitName.METRE && unit.Prefix == IfcSIPrefix.NONE)
					mSILength = unit;
				else if (unit.Name == IfcSIUnitName.SQUARE_METRE && unit.Prefix == IfcSIPrefix.NONE)
					mSIArea = unit;
				else if (unit.Name == IfcSIUnitName.CUBIC_METRE && unit.Prefix == IfcSIPrefix.NONE)
					mSIVolume = unit;
			}
			aggregate.setAggregate(result);
			if (mIfcObjects.Count <= result.mIndex)
				for (int ncounter = mIfcObjects.Count; ncounter <= result.mIndex; ncounter++)
					mIfcObjects.Add(new BaseClassIfc());
			mIfcObjects[result.mIndex] = result;
			result.mDatabase = this;
			
			//IfcWorkPlan workPlan = result as IfcWorkPlan;
			//if(workPlan != null)
			//{
			//	mWorkPlans.Add(workPlan);
			//	return workPlan;
			//}
			return result;
		}

		public bool WriteFile(string filename)
		{
			StreamWriter sw = null;
			FolderPath = Path.GetDirectoryName(filename);
			FileName = filename;
#if (!NOIFCZIP)
			bool zip = filename.EndsWith(".ifczip");
			System.IO.Compression.ZipArchive za = null;
			if (zip)
			{
				if (System.IO.File.Exists(filename))
					System.IO.File.Delete(filename);
				za = System.IO.Compression.ZipFile.Open(filename, System.IO.Compression.ZipArchiveMode.Create);
				System.IO.Compression.ZipArchiveEntry zae = za.CreateEntry(System.IO.Path.GetFileNameWithoutExtension(filename) + ".ifc");
				sw = new StreamWriter(zae.Open());
			}
			else
#endif
				sw = new StreamWriter(filename);
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			sw.Write(getHeaderString(filename) + "\r\n");
			for (int icounter = 1; icounter < mIfcObjects.Count; icounter++)
			{
				BaseClassIfc ie = mIfcObjects[icounter];
				if (ie != null)
				{
					string str = ie.ToString();
					if (!string.IsNullOrEmpty(str))
						sw.WriteLine(str);
				}
			}
			sw.Write(getFooterString());
			sw.Close();
			Thread.CurrentThread.CurrentUICulture = current;
#if(!NOIFCZIP)
			if (zip)
				za.Dispose();
#endif
			return true;
		}
	}
}

 