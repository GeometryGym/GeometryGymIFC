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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;

using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public class VersionAddedAttribute : Attribute
	{
		public ReleaseVersion Release { get; set; }
		public VersionAddedAttribute(ReleaseVersion release)
		{
			Release = release;
		}
	}

	[Serializable()]
	public abstract partial class BaseClassIfc : STEPEntity, IBaseClassIfc
	{
		internal string mGlobalId { get; private set; } = ""; // :	IfcGloballyUniqueId;
		internal void setGlobalId(string globalId)
		{
			if (string.IsNullOrEmpty(mGlobalId))
			{
				mGlobalId = globalId;
				return;
			}
			if(string.Compare(mGlobalId, globalId, false) != 0)
			{
				GlobalIdChangeManager idChangeManager = new GlobalIdChangeManager(this);
				mGlobalId = globalId;
				idChangeManager.updateReferences();
			}
		}
		private class GlobalIdChangeManager
		{
			BaseClassIfc mObject = null;
			List<IfcRelDefinesByProperties> mIsDefinedBy = new List<IfcRelDefinesByProperties>();
			IfcRelAggregates mDecomposes = null;
			IfcRelNests mNests = null;
			IfcRelContainedInSpatialStructure mContainedIn = null;
			internal GlobalIdChangeManager(BaseClassIfc obj)
			{
				mObject = obj;
				IfcObjectDefinition objectDefinition = obj as IfcObjectDefinition;
				if(objectDefinition != null)
				{
					mIsDefinedBy.AddRange(objectDefinition.mIsDefinedBy);
					foreach (IfcRelDefinesByProperties rdp in mIsDefinedBy)
						rdp.RelatedObjects.Remove(objectDefinition);

					mDecomposes = objectDefinition.mDecomposes;
					if (mDecomposes != null)
						mDecomposes.RelatedObjects.Remove(objectDefinition);
					mNests = objectDefinition.mNests;
					if (mNests != null)
						mNests.RelatedObjects.Remove(objectDefinition);

					IfcObject o = mObject as IfcObject;
					if (o != null)
					{
						IfcProduct product = objectDefinition as IfcProduct;
						if (product != null)
						{
							mContainedIn = product.mContainedInStructure;
							if (mContainedIn != null)
								mContainedIn.RelatedElements.Remove(product);
						}
					}
				}
			}

			internal void updateReferences()
			{
				IfcObjectDefinition objectDefinition = mObject as IfcObjectDefinition;
				if(objectDefinition != null)
				{
					foreach (IfcRelDefinesByProperties isDefinedBy in mIsDefinedBy)
						isDefinedBy.RelatedObjects.Add(objectDefinition);

					if (mDecomposes != null)
						mDecomposes.RelatedObjects.Add(objectDefinition);
					if (mNests != null)
						mNests.RelatedObjects.Add(objectDefinition);

					IfcObject obj = mObject as IfcObject;
					if (obj != null)
					{
						IfcProduct product = objectDefinition as IfcProduct;
						if (product != null)
						{
							if (mContainedIn != null)
								mContainedIn.RelatedElements.Add(product);
						}
					}
				}
			}
		}
		protected void ChangeGlobalId(string newGlobalId, string oldGlobalId) 
		{
			IfcObjectDefinition objectDefinition = this as IfcObjectDefinition;
			if(objectDefinition != null)
			{
				List<IfcRelDefinesByProperties> isDefinedBy = objectDefinition.mIsDefinedBy.ToList();
				foreach(IfcRelDefinesByProperties definedBy in isDefinedBy)
				{
					definedBy.RelatedObjects.Remove(objectDefinition);
				}
			}
		}

		[NonSerialized] internal DatabaseIfc mDatabase = null;

		public DatabaseIfc Database { get { return mDatabase; } private set { mDatabase = value; } }

#if (NOIFCJSON)
		public BaseClassIfc() : base() { }// this(new DatabaseIfc(false, ModelView.Ifc4NotAssigned)) { }
#else
		public BaseClassIfc() : base() { } // this(new DatabaseIfc(false, ModelView.Ifc4NotAssigned) { Format = FormatIfcSerialization.JSON }) { }
#endif
		protected BaseClassIfc(BaseClassIfc basis, bool replace) : base()
		{
			if (replace)
				basis.ReplaceDatabase(this);
			else
				basis.Database.appendObject(this);	
		}
		protected BaseClassIfc(DatabaseIfc db, BaseClassIfc e) : base()
		{
			mGlobalId = e.mGlobalId;
			if (db != null)
			{
				db.appendObject(this);
				db.Factory.mDuplicateMapping.AddObject(e, mIndex);
			}
		}
		protected BaseClassIfc(DatabaseIfc db) : base()
		{
			if(db != null)
				db.appendObject(this);
		}

		protected virtual void parseFields(List<string> arrFields, ref int ipos) { }

		public List<T> Extract<T>() where T : IBaseClassIfc
		{
			List<T> extracted = Extract<T>(typeof(T));
			HashSet<string> rootIds = new HashSet<string>();
			List<T> result = new List<T>(extracted.Count);
			foreach(T t in extracted)
			{
				if(t is IfcRoot)
				{
					IfcRoot root = (IfcRoot)(IBaseClassIfc)t;
					string globalId = root.GlobalId;
					if (rootIds.Contains(globalId))
						continue;
					rootIds.Add(globalId);
				}
				result.Add(t);
			}
			return result;
		}
		protected virtual List<T> Extract<T>(Type type) where T : IBaseClassIfc
		{
			List<T> result = new List<T>();
			if (this is T)
				result.Add((T)(IBaseClassIfc)this);
			return result;
		}

		public override string ToString()
		{
#if (!NOIFCJSON)
			if (mDatabase == null || mDatabase.Format == FormatIfcSerialization.JSON)
				return getJson(null, new BaseClassIfc.SetJsonOptions()).ToString();
#endif
			if(mDatabase != null)
			{
				if (mDatabase.Format == FormatIfcSerialization.XML)
					return GetXML(new System.Xml.XmlDocument(), "", null, new Dictionary<string, System.Xml.XmlElement>()).OuterXml;
			}
			return base.ToString();
		}

		protected void Adopt(BaseClassIfc master)
		{
			IfcRoot thisRoot = this as IfcRoot, masterRoot = master as IfcRoot;
			if(thisRoot != null && masterRoot != null)
			{
				thisRoot.Name = masterRoot.Name;
				thisRoot.Description = masterRoot.Description;
				IfcObjectDefinition thisObjectDefinition = this as IfcObjectDefinition, masterObjectDefinition = master as IfcObjectDefinition;
				if (thisObjectDefinition != null && masterObjectDefinition != null)
				{
					foreach (IfcRelAssigns assigns in masterObjectDefinition.HasAssignments)
						assigns.RelatedObjects.Add(thisObjectDefinition);
					foreach (IfcRelAssociates associates in masterObjectDefinition.HasAssociations)
						associates.RelatedObjects.Add(thisObjectDefinition);
					if (masterObjectDefinition.HasContext != null)
						masterObjectDefinition.HasContext.RelatedDefinitions.Add(thisObjectDefinition);
					IfcObject thisObject = this as IfcObject, masterObject = master as IfcObject;
					if (thisObject != null && masterObject != null)
					{
						if (string.IsNullOrEmpty(thisObject.ObjectType))
							thisObject.ObjectType = masterObject.ObjectType;

					}
					else
					{
						IfcTypeObject thisTypeObject = this as IfcTypeObject, masterTypeObject = master as IfcTypeObject;
						if (thisTypeObject != null && masterTypeObject != null)
						{
							IfcTypeProduct thisTypeProduct = this as IfcTypeProduct, masterTypeProduct = master as IfcTypeProduct;
							if (thisTypeProduct != null && masterTypeProduct != null)
							{
								thisTypeProduct.Tag = masterTypeProduct.Tag;
								IfcElementType thisElementType = this as IfcElementType, masterElementType = master as IfcElementType;
								if (thisElementType != null && masterElementType != null)
								{
									thisElementType.ElementType = masterElementType.ElementType;
								}
							}
						}
					}
				}
			}
		}
		protected void ReplaceDatabase(BaseClassIfc revised)
		{
			IfcRoot thisRoot = this as IfcRoot, revisedRoot = revised as IfcRoot;
			if (thisRoot != null && revisedRoot != null)
			{
				revisedRoot.GlobalId = thisRoot.GlobalId;
				revisedRoot.OwnerHistory = thisRoot.OwnerHistory;
				revisedRoot.Name = thisRoot.Name;
				revisedRoot.Description = thisRoot.Description;
				IfcObjectDefinition thisObjectDefinition = this as IfcObjectDefinition, revisedObjectDefinition = revised as IfcObjectDefinition;
				if (thisObjectDefinition != null && revisedObjectDefinition != null)
				{
					foreach (IfcRelAggregates rel in thisObjectDefinition.IsDecomposedBy.ToList())
						rel.RelatingObject = revisedObjectDefinition;
					foreach (IfcRelNests rel in thisObjectDefinition.IsNestedBy.ToList())
						rel.RelatingObject = revisedObjectDefinition;

					IfcRelAggregates relAggregates = thisObjectDefinition.Decomposes;
					if(relAggregates != null)
					{
						relAggregates.RelatedObjects.Remove(thisObjectDefinition);
						relAggregates.RelatedObjects.Add(revisedObjectDefinition);
					}
					IfcRelNests relNests = thisObjectDefinition.Nests;
					if(relNests != null)
					{
						relNests.RelatedObjects.Remove(thisObjectDefinition);
						relNests.RelatedObjects.Add(thisObjectDefinition);
					}

					foreach (IfcRelDefinesByProperties relDefinesByProperties in thisObjectDefinition.mIsDefinedBy.ToList())
					{
						relDefinesByProperties.RelatedObjects.Remove(thisObjectDefinition);
						relDefinesByProperties.RelatedObjects.Add(revisedObjectDefinition);
					}

					foreach (IfcRelAssigns assigns in thisObjectDefinition.HasAssignments.ToList())
					{
						assigns.RelatedObjects.Remove(thisObjectDefinition);
						assigns.RelatedObjects.Add(revisedObjectDefinition);
					}
					IfcRelDeclares relDeclares = thisObjectDefinition.HasContext;
					if (relDeclares != null)
					{
						relDeclares.RelatedDefinitions.Remove(thisObjectDefinition);
						relDeclares.RelatedDefinitions.Add(revisedObjectDefinition);
					}
					foreach (IfcRelAssociates associates in thisObjectDefinition.HasAssociations.ToList())
					{
						associates.RelatedObjects.Remove(thisObjectDefinition);
						associates.RelatedObjects.Add(revisedObjectDefinition);
					}
					IfcObject thisObject = this as IfcObject, revisedObject = revised as IfcObject;
					if (thisObject != null && revisedObject != null)
					{
						if(!string.IsNullOrEmpty(thisObject.ObjectType))
							revisedObject.ObjectType = thisObject.ObjectType;

						if (thisObject.mIsTypedBy != null)
							thisObject.mIsTypedBy.mRelatedObjects.Remove(thisObject);
						IfcProduct thisProduct = this as IfcProduct, revisedProduct = revised as IfcProduct;
						if (thisProduct != null && revisedProduct != null)
						{
							IfcRelContainedInSpatialStructure containedInSpatialStructure = thisProduct.mContainedInStructure;
							if (containedInSpatialStructure != null)
							{
								containedInSpatialStructure.RelatedElements.Remove(thisProduct);
								containedInSpatialStructure.RelatedElements.Add(revisedProduct);
							}
							IfcElement thisElement = this as IfcElement, revisedElement = revised as IfcElement;
							if (thisElement != null && revisedElement != null)
							{
								revisedElement.Tag = thisElement.Tag;
							}
							IfcSpatialElement thisSpatial = this as IfcSpatialElement, revisedSpatial = revised as IfcSpatialElement;
							if(thisSpatial != null && revisedSpatial != null)
							{
								foreach(IfcRelContainedInSpatialStructure contained in thisSpatial.ContainsElements.ToList())
									contained.RelatingStructure = revisedSpatial;
							}
							else if (revisedSpatial != null && thisElement != null)
							{
								if(containedInSpatialStructure != null)
								{
									containedInSpatialStructure.RelatedElements.Remove(revisedProduct);
									containedInSpatialStructure.RelatingStructure.AddAggregated(revisedProduct);
								}
								List<IfcProduct> subProducts = thisObjectDefinition.IsDecomposedBy.SelectMany(x => x.RelatedObjects).OfType<IfcProduct>().ToList();
								if(subProducts.Count > 0)
								{
									new IfcRelContainedInSpatialStructure(subProducts, revisedSpatial);
								}
								foreach (IfcRelAssociatesMaterial associates in revisedSpatial.HasAssociations.OfType<IfcRelAssociatesMaterial>().ToList())
									associates.RelatedObjects.Remove(revisedSpatial);

								IfcFacilityPart facilityPart = revisedSpatial as IfcFacilityPart;
								if(facilityPart != null)
								{
									IfcFacility facility = revisedSpatial.FindHost<IfcFacility>();
									if (facility != null)
										facility.AddAggregated(revisedSpatial);
								}

							}
						}
					}
					else
					{
						IfcTypeObject thisTypeObject = this as IfcTypeObject, revisedTypeObject = revised as IfcTypeObject;
						if (thisTypeObject != null && revisedTypeObject != null)
						{
							IfcTypeProduct thisTypeProduct = this as IfcTypeProduct, revisedTypeProduct = revised as IfcTypeProduct;
							if (thisTypeProduct != null && revisedTypeProduct != null)
							{
								revisedTypeProduct.Tag = thisTypeProduct.Tag;
								IfcElementType thisElementType = this as IfcElementType, revisedElementType = revised as IfcElementType;
								if (thisElementType != null && revisedElementType != null)
								{
									revisedElementType.ElementType = thisElementType.ElementType;
								}
							}
						}
					}
				}
			}
			else
			{
				IfcRepresentationItem representationItem = this as IfcRepresentationItem, revisedItem = revised as IfcRepresentationItem;
				if (representationItem != null && revisedItem != null)
				{
					IfcStyledItem styledItem = representationItem.StyledByItem;
					if (styledItem != null)
						styledItem.Item = revisedItem;

					foreach (IfcShapeModel shapeModel in representationItem.mRepresents.ToList())
					{
						shapeModel.Items.Remove(representationItem);
						shapeModel.Items.Add(revisedItem);
					}
					IfcPresentationLayerAssignment layerAssignment = representationItem.mLayerAssignment;
					if (layerAssignment != null)
					{
						layerAssignment.AssignedItems.Remove(representationItem);
						layerAssignment.AssignedItems.Add(revisedItem);
					}
				}
			}
			mDatabase[revised.mIndex] = null;
			revised.mIndex = mIndex;
			mDatabase[mIndex] = revised;
		}
		
		public bool Dispose(bool children)
		{
			if (mDatabase == null)
				return true;
			if (mDatabase.IsDisposed())
				return true;
			return DisposeWorker(children);
		}
		protected virtual bool DisposeWorker(bool children)
		{
			mDatabase[mIndex] = null;
			return true;
		}
		internal virtual List<IBaseClassIfc> retrieveReference(IfcReference reference) { return (reference.InnerReference != null ? null : new List<IBaseClassIfc>() { }); }

		public static Type GetType(string classNameIfc)
		{
			string className = classNameIfc;
			if (string.Compare(className, "IfcBuildingElement", true) == 0)
				className = "IfcBuiltElement";
			else if (string.Compare(className, "IfcBuildingElementType", true) == 0)
				className = "IfcBuiltElementType";
			else if (string.Compare(className, "IfcBuildingSystem", true) == 0)
				className = "IfcBuiltSystem";
			return STEPEntity.GetType(className, "Ifc");	
		}
		public static BaseClassIfc Construct(string ifcClassName)
		{
			ConstructorInfo constructor = null;
			string className = ifcClassName;
			if (string.Compare(className, "IfcBuildingElement", true) == 0)
				className = "IfcBuildingElementProxy";
			if (string.Compare(className, "IfcDistanceExpression", true) == 0)
				className = "IfcPointByDistanceExpression";

			if (!mConstructors.TryGetValue(className, out constructor))
			{
				Type type = GetType(className);
				if (type == null)
					return null;
				if (type.IsAbstract)
				{
					if (string.Compare(className, "IfcProductRepresentation", true) == 0)
						return Construct("IfcProductDefinitionShape");
					if (string.Compare(className, "IfcParameterizedProfileDef", true) == 0)
						return Construct("IfcProfileDef");
					if (string.Compare(className, "IfcReinforcingElement", true) == 0)
						return Construct("IfcReinforcingBar");
					if (string.Compare(className, "IfcBuildingElementComponent", true) == 0)
						return Construct("IfcBuildingElementPart");
					if (string.Compare(className, "IfcFlowSegmentType", true) == 0)
						return Construct("IfcDistributionElementType");
					if (string.Compare(className, "IfcFlowTerminalType", true) == 0)
						return Construct("IfcDistributionElementType");
					else
						return null;
				}
				constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[] { }, null);
				mConstructors.TryAdd(className, constructor);
			}
			return constructor.Invoke(new object[] { }) as BaseClassIfc;
		}
		internal string formatLength(double length)
		{
			if (double.IsNaN(length))
				return "$";
			double tol = (mDatabase == null ? 1e-6 : mDatabase.Tolerance / 100);
			int digits = (mDatabase == null ? 5 : mDatabase.mLengthDigits);
			double result = Math.Round(length, digits);
			return ParserSTEP.DoubleToString(Math.Abs(result) < tol ? 0 : result);
		}
		internal static BaseClassIfc LineParser(string className, string str, ReleaseVersion release, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			BaseClassIfc result = Construct(className);	
			if(result == null)
				return null;
			int pos = 0;
			result.parse(str, ref pos, release, str.Length, dictionary);
			return result;
		}

		internal virtual bool isDuplicate(BaseClassIfc e, double tol) { return true; }

		internal class RepositoryAttributes
		{
			internal DateTime Created { get; set; }
			internal DateTime Modified { get; set; }

			internal RepositoryAttributes() { Created = DateTime.MinValue; Modified = DateTime.MinValue; }
			internal RepositoryAttributes(DateTime created, DateTime modified) { Created = created; Modified = modified; }
		}

		internal void setFolderAttributes(string folderPath)
		{
			IfcRoot root = this as IfcRoot;
			IfcOwnerHistory ownerHistory = (root == null ? null : root.OwnerHistory);
			if (ownerHistory == null)
				return;
			setFolderAttributes(folderPath, new RepositoryAttributes(ownerHistory.CreationDate, ownerHistory.LastModifiedDate));
		}
		internal void setFolderAttributes(string folderPath, RepositoryAttributes attributes)
		{
			try
			{
				DateTime created = attributes.Created, modified = attributes.Modified;
				if (created != DateTime.MinValue && created < Directory.GetCreationTime(folderPath))
				{
					Directory.SetCreationTime(folderPath, created);
					Directory.SetLastWriteTime(folderPath, created);
				}
				if (modified != DateTime.MinValue && modified > Directory.GetLastWriteTime(folderPath))
					Directory.SetLastWriteTime(folderPath, modified);
			}
			catch (Exception) { }
		}

		internal void setFileAttributes(string filePath)
		{
			IfcRoot root = this as IfcRoot;
			IfcOwnerHistory ownerHistory = (root == null ? null : root.OwnerHistory);
			if (ownerHistory == null)
				return;
			setFileAttributes(filePath, new RepositoryAttributes(ownerHistory.CreationDate, ownerHistory.LastModifiedDate));
		}
		internal void setFileAttributes(string filePath, RepositoryAttributes attributes)
		{
			try
			{
				DateTime created = attributes.Created, modified = attributes.Modified;
				if (created != DateTime.MinValue && created < File.GetCreationTime(filePath))
				{
					File.SetCreationTime(filePath, created);
					File.SetLastWriteTime(filePath, created);
				}
				if (modified != DateTime.MinValue && modified > File.GetLastWriteTime(filePath))
					File.SetLastWriteTime(filePath, modified);
			}
			catch (Exception) { }
		}
	}
	public partial interface IBaseClassIfc : ISTEPEntity
	{
		DatabaseIfc Database { get; }
	}
	public interface NamedObjectIfc : IBaseClassIfc { string Name { get; } } // GG interface
}
