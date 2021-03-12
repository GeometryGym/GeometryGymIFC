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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GeometryGym.STEP;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GeometryGym.Ifc
{ 
	public enum ReleaseVersion {[Obsolete("RETIRED", false)] IFC2X, [Obsolete("RETIRED", false)] IFC2x2, IFC2x3, IFC4, IFC4A1, IFC4A2, IFC4X1, IFC4X2, IFC4X3_RC1, IFC4X3_RC2, IFC4X3_RC3 }; // Alpha Releases IFC1.0, IFC1.5, IFC1.5.1, IFC2.0, 
	public enum ModelView { Ifc4Reference, Ifc4DesignTransfer, Ifc4NotAssigned, Ifc2x3Coordination, Ifc2x3NotAssigned, Ifc4X1NotAssigned, Ifc4X2NotAssigned, Ifc4X3NotAssigned };

	public class Triple<T>
	{
		public T X { get; set; } 
		public T Y { get; set; }
		public T Z { get; set; } 
		public Triple() { X = default(T); Y = default(T); Z = default(T); }
		public Triple(T x, T y, T z)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}
	public enum FormatIfcSerialization
	{
		STEP,
		XML,
#if (!NOIFCJSON)
		JSON,
#endif
	};
	public partial class DatabaseIfc : DatabaseSTEP<BaseClassIfc>
	{
		internal string id = ParserIfc.EncodeGuid(Guid.NewGuid());

		private bool mIsDisposed = false;
		internal bool IsDisposed() { return mIsDisposed; }
		public void Dispose() { mIsDisposed = true; }

		internal ReleaseVersion mRelease = ReleaseVersion.IFC2x3;
		public FormatIfcSerialization Format { get; set; } 
		
		private FactoryIfc mFactory = null;
		public FactoryIfc Factory { get { return mFactory; } }

		public DatabaseIfc() : base() { mFactory = new FactoryIfc(this); Format = FormatIfcSerialization.STEP; }
		public DatabaseIfc(string fileName) : this() { new SerializationIfc(this).ReadFile(fileName); }
		public DatabaseIfc(TextReader stream) : this() { new SerializationIfc(this).ReadFile(stream); }
		public DatabaseIfc(ModelView view) : this(versionFromModelView(view), view) { }
		public DatabaseIfc(ReleaseVersion schema) : this(schema, schema < ReleaseVersion.IFC4 ? ModelView.Ifc2x3NotAssigned : ModelView.Ifc4NotAssigned) { }
		public DatabaseIfc(DatabaseIfc db) : this() { mRelease = db.mRelease; mModelView = db.mModelView; Tolerance = db.Tolerance; }
		[Obsolete]
		public DatabaseIfc(bool generate, ModelView view) : this(view) { if(generate) mFactory.initData(); }
		[Obsolete]
		public DatabaseIfc(bool generate, ReleaseVersion schema) : this(schema) { if (generate) mFactory.initData(); }
		private DatabaseIfc(ReleaseVersion schema, ModelView view) : this()
		{
			mRelease = schema;
			mModelView = view;
#if (RHINO || GH)
			//mModelSIScale = 1 / Utils.mLengthConversion[(int) GeometryGym.GGRhino.GGRhino.ActiveUnits()];
			Rhino.RhinoDoc doc = Rhino.RhinoDoc.ActiveDoc;
			if (doc != null)
			{
				Tolerance = doc.ModelAbsoluteTolerance;
				ToleranceAngleRadians = Math.Min(Math.PI/1800, doc.ModelAngleToleranceRadians);
			}
#endif
		}
		public override BaseClassIfc this[int index]
		{
			set
			{
				IfcRoot root = this[index] as IfcRoot;
				if (root != null)
				{
					BaseClassIfc obj = null;
					mDictionary.TryRemove(root.GlobalId, out obj);
				}
				root = value as IfcRoot;
				if(root != null && !string.IsNullOrEmpty(root.GlobalId))
				{
					if (mDictionary.ContainsKey(root.GlobalId))
						root.Guid = Guid.NewGuid();
					mDictionary.TryAdd(root.GlobalId, value);
				}
				base[index] = value;
				if(value != null)
					value.mDatabase = this;
			}
		}
		public virtual BaseClassIfc this[string globalID]
		{
			get
			{
				BaseClassIfc result = null;
				mDictionary.TryGetValue(globalID, out result);
				return result;
			}
		}
		internal ModelView mModelView = ModelView.Ifc2x3NotAssigned;
		
		internal bool mTimeInDays = false;
		public ReleaseVersion Release
		{ 
			get { return mRelease; }  
			set { mRelease = value; } 
		}
		public ModelView ModelView
		{
			get { return mModelView; }
			set { mModelView = value; }
		}
	
		private static ReleaseVersion versionFromModelView(ModelView modelView)
		{
			if (modelView == ModelView.Ifc2x3Coordination || modelView == ModelView.Ifc2x3NotAssigned)
				return ReleaseVersion.IFC2x3;
			if (modelView == ModelView.Ifc4X1NotAssigned)
				return ReleaseVersion.IFC4X1;
			if (modelView == ModelView.Ifc4X2NotAssigned)
				return ReleaseVersion.IFC4X2;
			if (modelView == ModelView.Ifc4X3NotAssigned)
				return ReleaseVersion.IFC4X3_RC3;
			return ReleaseVersion.IFC4A2;
		}
		public double Tolerance
		{
			get { return mModelTolerance; }
			set
			{
				if (!double.IsNaN(value))
				{
					mModelTolerance = value;
					mLengthDigits = Math.Max(2, -1 * (int)(Math.Log10(value) - 1));
				}
			}
		}
		public double ToleranceAngleRadians
		{
			get { return mModelToleranceAngleRadians; }
			set { mModelToleranceAngleRadians = value; }
		}
		public double ScaleSI
		{
			get
			{
				if(double.IsNaN(mModelSIScale))
				{
					IfcContext context = Context;
					if(context != null)
					{
						IfcUnitAssignment units = context.UnitsInContext;
						if (units != null)
						{

							IfcNamedUnit namedUnit = units[IfcUnitEnum.LENGTHUNIT];
							if(namedUnit != null)
								mModelSIScale = namedUnit.SIFactor;
						}
					}
					if (double.IsNaN(mModelSIScale))
						return 1;
				}
				return mModelSIScale;
			}
			internal set { mModelSIScale = value; }
		}
		internal double ScaleAngle()
		{
			if (mContext == null)
			{
				if (!Factory.Options.AngleUnitsInRadians)
					return Math.PI / 180;
			}
			else if (mContext.UnitsInContext != null)
				return mContext.UnitsInContext.ScaleSI(IfcUnitEnum.PLANEANGLEUNIT);
			return 1;
		}
		private double mModelTolerance = 0.0001, mModelSIScale = double.NaN, mModelToleranceAngleRadians = Math.PI / 1800;
		internal int mLengthDigits = 5;
		public IfcContext Context { get { return mContext; } }
		public IfcProject Project { get { return mContext as IfcProject; } }
		
		internal IfcContext mContext = null;
		
		internal ConcurrentDictionary<string, BaseClassIfc> mDictionary = new ConcurrentDictionary<string, BaseClassIfc>();
		
		private string viewDefinition { get { return (mModelView == ModelView.Ifc2x3Coordination ? "CoordinationView_V2" : (mModelView == ModelView.Ifc4Reference ? "ReferenceView_V1" : (mModelView == ModelView.Ifc4DesignTransfer ? "DesignTransferView_V1" : "notYetAssigned"))); } }
		
		public static DatabaseIfc ParseString(string str)
		{
			if (string.IsNullOrEmpty(str))
				return null;

			DatabaseIfc db = new DatabaseIfc(false, ReleaseVersion.IFC4);
#if (!NOIFCJSON)
			if (str.StartsWith("{"))
			{
				Newtonsoft.Json.Linq.JObject jobj = Newtonsoft.Json.Linq.JObject.Parse(str);
				if (str != null)
					db.ReadJSON(jobj);
			}
#endif
			if (str.StartsWith("<"))
			{
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(str);
				db.ReadXMLDoc(doc);
			}
			return db;
		}

		internal void postImport()
		{
			IfcContext context = Context;
			if (context != null)
			{
				context.initializeUnitsAndScales();
				Factory.IdentifyContexts(context.RepresentationContexts);
			}
		}

		public bool WriteFile(string filename)
		{
			StreamWriter sw = null;

			FolderPath = Path.GetDirectoryName(filename);
			string fn = Path.GetFileNameWithoutExtension(filename);
			char[] chars = Path.GetInvalidFileNameChars();
			foreach (char c in chars)
				fn = fn.Replace(c, '_');
			FileName = Path.Combine(FolderPath, fn + Path.GetExtension(filename));
			if (filename.EndsWith("xml"))
			{
				WriteXmlFile(FileName);
				return true;
			}
#if (!NOIFCJSON)
			else if (FileName.EndsWith("json"))
			{
				ToJSON(FileName);
				return true;
			}

#endif
#if (!NOIFCZIP)
			bool zip = FileName.EndsWith(".ifczip");
			System.IO.Compression.ZipArchive za = null;
			if (zip)
			{
				if (System.IO.File.Exists(FileName))
					System.IO.File.Delete(FileName);
				za = System.IO.Compression.ZipFile.Open(FileName, System.IO.Compression.ZipArchiveMode.Create);
				System.IO.Compression.ZipArchiveEntry zae = za.CreateEntry(System.IO.Path.GetFileNameWithoutExtension(FileName) + ".ifc");
				sw = new StreamWriter(zae.Open());
			}
			else
#endif
				sw = new StreamWriter(FileName);
			bool result = new SerializationIfcSTEP(this).WriteSTEP(sw, FileName);
#if (!NOIFCZIP)
			if (zip)
				za.Dispose();
#endif
			return result;
		}
		public string ToString(FormatIfcSerialization format)
		{
			if(format == FormatIfcSerialization.XML)
				return XmlString();
#if (!NOIFCJSON)
			if (format == FormatIfcSerialization.JSON)
				return JSON().ToString();
#endif
			StringWriter stringWriter = new StringWriter();
			if (new SerializationIfcSTEP(this).WriteSTEP(stringWriter, FileName))
				return stringWriter.ToString();
			return null;
		}
	}
	public class DuplicateMapping
	{
		private Dictionary<string, int> mDictionary = new Dictionary<string, int>();
		internal int FindExisting(BaseClassIfc obj)
		{
			if (obj == null || obj.mDatabase == null)
				return 0;
			int result = 0;
			mDictionary.TryGetValue(key(obj), out result);
			return result;
		}
		internal void AddObject(BaseClassIfc obj, int index) { mDictionary[key(obj)] = index; }
		private string key(BaseClassIfc obj) { return (obj.mDatabase == null ? "" : obj.mDatabase.id + "|") + obj.mIndex; }
		internal bool Remove(BaseClassIfc obj)
		{
			string k = key(obj);
			if (mDictionary.Remove(k))
				return true;
			return false;
		}
		internal void Clear() { mDictionary.Clear(); }
	}
	public partial class FactoryIfc
	{
		private DatabaseIfc mDatabase = null;
		internal FactoryIfc(DatabaseIfc db) { mDatabase = db; }

		public partial class GenerateOptions
		{
			public bool GenerateOwnerHistory { get; set; } 
			public bool AngleUnitsInRadians { get; set; }
			public GenerateOptions() { GenerateOwnerHistory = true; AngleUnitsInRadians = true; }
		}
		internal GenerateOptions mOptions = new GenerateOptions();
		public GenerateOptions Options { get { return mOptions; } }

		internal IfcElement ConstructElement(string className, IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation)
		{
			return ConstructElement(className, host, placement, representation, null);
		}
		internal IfcElement ConstructElement(string className, IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system)
		{
			IfcElement element = ConstructProduct(className, host, placement, representation, system) as IfcElement;
			if (element == null)
				element = new IfcBuildingElementProxy(host, placement, representation);
			return element;
		}
		internal IfcProduct ConstructProduct(string className, IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) { return ConstructProduct(className, host, placement, representation, null); }
		internal IfcProduct ConstructProduct(string className, IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system)
		{
			string str = className, definedType = "", enumName = "";
			ReleaseVersion release = mDatabase.Release;
			if (string.IsNullOrEmpty(str))
				return null;
			string[] fields = str.Split(".".ToCharArray());
			if (fields.Length > 1)
			{
				enumName = str = fields[0];
				definedType = fields[1];
			}
			if (str.EndsWith("Type"))
				str = str.Substring(0, str.Length - 4);
			if (str.EndsWith("TypeEnum"))
				str = str.Substring(0, str.Length - 8);
			Type type = BaseClassIfc.GetType(str);
			if (type == null)
				throw new Exception("XXX Unrecognized Ifc Type for " + className);
			if (release < ReleaseVersion.IFC4X3_RC1)
			{
				if (string.Compare(str, "IfcBuiltElement", true) == 0)
					type = typeof(IfcBuildingElementProxy);
				else if (typeof(IfcCourse).IsAssignableFrom(type) || typeof(IfcEarthworksElement).IsAssignableFrom(type))
					type = typeof(IfcBuildingElementProxy);
				else if (typeof(IfcEarthworksCut).IsAssignableFrom(type) || typeof(IfcGeotechnicalStratum).IsAssignableFrom(type))
					type = typeof(IfcBuildingElementProxy);
				else if (release < ReleaseVersion.IFC4X2)
				{
					if (typeof(IfcFacilityPart).IsAssignableFrom(type))
						type = typeof(IfcBuilding);
					else if (release < ReleaseVersion.IFC4X1)
					{
						if (type != typeof(IfcBuilding) && typeof(IfcFacility).IsAssignableFrom(type))
							type = host is IfcBuilding ? typeof(IfcBuilding) : typeof(IfcSite);
						else if (release < ReleaseVersion.IFC4)
						{
							if (typeof(IfcShadingDevice).IsAssignableFrom(type) || typeof(IfcGeographicElement).IsAssignableFrom(type) ||
								typeof(IfcCivilElement).IsAssignableFrom(type))
								type = typeof(IfcBuildingElementProxy);
						}
					}
				}
			}
			IfcProduct product = construct(type, host, placement, representation, system);
			if (product == null)
				throw new Exception("XXX Unrecognized Ifc Constructor for " + className);
			string resultClass = product.StepClassName;
			if (string.Compare(str, resultClass, true) != 0 && string.Compare(resultClass, "IfcFacilityPart",true) != 0)
				product.ObjectType = className;
			else if (!string.IsNullOrEmpty(definedType))
			{
				if (mDatabase.mRelease < ReleaseVersion.IFC4)
					product.ObjectType = definedType;
				else
				{
					type = product.GetType();
					PropertyInfo pi = type.GetProperty("PredefinedType");
					if (pi != null)
					{
						if (typeof(SelectEnum).IsAssignableFrom(pi.PropertyType))
						{
							MethodInfo method = pi.PropertyType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any,new Type[] { typeof(string) }, null);
							if (method != null)
							{
								if (!enumName.EndsWith("TypeEnum"))
									enumName += "TypeEnum";
								object o = method.Invoke(null, new object[] { enumName + "(." + definedType + ".)" });
								if (o != null)
									pi.SetValue(product, o);
							}
						}
						else
						{
							Type enumType = Type.GetType("GeometryGym.Ifc." + type.Name + "TypeEnum");
							if (enumType != null)
							{
								FieldInfo fi = enumType.GetField(definedType);
								if (fi == null)
								{
									product.ObjectType = definedType;
									fi = enumType.GetField("USERDEFINED");
								}
								if (fi != null)
								{
									int i = (int)fi.GetValue(enumType);
									object newEnumValue = Enum.ToObject(enumType, i);

									pi.SetValue(product, newEnumValue);
								}
								else
									product.ObjectType = definedType;
							}
							else
								product.ObjectType = definedType;
						}
					}
					else
						product.ObjectType = definedType;
				}
			}
			return product;
		}
		private IfcProduct construct(Type type, IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system)
		{
			if (host == null)
			{
				IfcProduct product = null;
				ConstructorInfo constructor = getConstructor(type, new[] { typeof(DatabaseIfc) });
				if(constructor == null)
				{
					constructor = getConstructor(type, new Type[] { });
					if (constructor == null)
						return null;
					product = constructor.Invoke(new object[] { }) as IfcProduct;
					mDatabase.appendObject(product);
				}
				else
					product = constructor.Invoke(new object[] { mDatabase }) as IfcProduct;

				product.ObjectPlacement = placement;
				product.Representation = representation;
				return product;
			}
			ConstructorInfo ctor = getConstructor(type, new[] { typeof(IfcObjectDefinition), typeof(IfcObjectPlacement), typeof(IfcProductDefinitionShape) });
			if (ctor == null)
			{
				ctor = getConstructor(type, new[] { typeof(IfcObjectDefinition), typeof(IfcObjectPlacement), typeof(IfcProductDefinitionShape), typeof(IfcDistributionSystem) });
				if (ctor == null)
				{
					IfcElement element = host as IfcElement;
					if (element != null)
					{
						ctor = getConstructor(type, new[] { typeof(IfcElement), typeof(IfcObjectPlacement), typeof(IfcProductDefinitionShape) });
						if(ctor != null)
							return ctor.Invoke(new object[] { element, placement, representation }) as IfcProduct;
					}
					return null;

				}
				else
					return ctor.Invoke(new object[] { host, placement, representation, system }) as IfcProduct;
			}
			else
				return ctor.Invoke(new object[] { host, placement, representation }) as IfcProduct;
		}
		private ConstructorInfo getConstructor(Type type, Type[] arguments)
		{
			BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			return type.GetConstructor(binding, null, arguments, null);
		}
		public BaseClassIfc Construct(string className)
		{
			BaseClassIfc result = BaseClassIfc.Construct(className);
			if (result == null)
				return null;
			if (mDatabase.Release < ReleaseVersion.IFC4 || Options.GenerateOwnerHistory)
			{
				IfcRoot root = result as IfcRoot;
				if (root != null)
					root.OwnerHistory = OwnerHistoryAdded;
			}
			mDatabase[mDatabase.NextBlank()] = result;
			return result; 
		}
		internal DuplicateMapping mDuplicateMapping = new DuplicateMapping();
		internal Dictionary<string, IfcProperty> mProperties = new Dictionary<string, IfcProperty>();
		internal Dictionary<string, IfcPropertySetDefinition> mPropertySets = new Dictionary<string, IfcPropertySetDefinition>();
		public BaseClassIfc Duplicate(IBaseClassIfc entity) { return Duplicate(entity as BaseClassIfc, new DuplicateOptions(mDatabase.Tolerance) { DuplicateDownstream = true }); }
		public void NominateDuplicate(IBaseClassIfc entity, IBaseClassIfc existingDuplicate) { mDuplicateMapping.AddObject(entity as BaseClassIfc, existingDuplicate.Index); }
		public IfcAxis2Placement3D DuplicateAxis(IfcAxis2Placement3D placement, DuplicateOptions options)
		{
			if (placement == null)
				return null;
			int index = mDuplicateMapping.FindExisting(placement);
			if (index > 0)
				return mDatabase[index] as IfcAxis2Placement3D;
			if (placement.IsXYPlane(mDatabase.Tolerance))
				return XYPlanePlacement;
			return Duplicate(placement, options) as IfcAxis2Placement3D;
		}
		public BaseClassIfc Duplicate(BaseClassIfc entity, DuplicateOptions options)
		{
			if (entity == null)
				return null;
			if (entity.mDatabase != null && entity.mDatabase.id == mDatabase.id)
				return entity;
			int index = mDuplicateMapping.FindExisting(entity);
			BaseClassIfc result = null;
			if (index > 0)
			{
				result = mDatabase[index];
				if (result != null)
					return result;
			}
			if (!string.IsNullOrEmpty(entity.mGlobalId))
			{
				if (mDatabase.mDictionary.TryGetValue(entity.mGlobalId, out result))
				{
					if (result != null)
					{
						mDuplicateMapping.AddObject(entity, result.mIndex);
						return result;
					}
				}
			}
			result = duplicateWorker(entity, options);
			if (result == null)
				return null;

			NominateDuplicate(entity, result);
			return result;
		}
		private BaseClassIfc duplicateWorker(BaseClassIfc entity, DuplicateOptions options)
		{
			BaseClassIfc result = null;
			if (mDatabase.Release < ReleaseVersion.IFC4X3_RC1)
			{
				if (mDatabase.Release < ReleaseVersion.IFC4X2)
				{
					if (mDatabase.Release < ReleaseVersion.IFC4X1)
					{
						if (mDatabase.Release < ReleaseVersion.IFC4)
						{
							IfcSpatialZone spatialZone = entity as IfcSpatialZone;
							if (spatialZone != null)
							{
								IfcSite site = new IfcSite(mDatabase, spatialZone, options);
								NominateDuplicate(entity, site);
								return site;
							}
						}
					}
				}
			}

			Type type = Type.GetType("GeometryGym.Ifc." + entity.GetType().Name, false, true);
			result = Duplicate(entity, type, options);
			if (result == null && entity is IfcProfileProperties)
				result = Duplicate(entity, typeof(IfcProfileProperties), options);

			return result;
		}
		private BaseClassIfc Duplicate(BaseClassIfc entity, Type type, DuplicateOptions options)
		{
			if (type != null)
			{
				Type[] types = new Type[] { typeof(DatabaseIfc), type, typeof(DuplicateOptions) };
				ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
				if (constructor != null)
					return constructor.Invoke(new object[] { this.mDatabase, entity, options }) as BaseClassIfc;
				types = new Type[] { typeof(DatabaseIfc), type, typeof(bool) };
				constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
				if (constructor != null)
					return constructor.Invoke(new object[] { this.mDatabase, entity, options.DuplicateDownstream }) as BaseClassIfc;
				types = new Type[] { typeof(DatabaseIfc), type };
				constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
				if (constructor != null)
					return constructor.Invoke(new object[] { this.mDatabase, entity }) as BaseClassIfc;
			}
			return null;
		}
		
		internal IfcProperty Duplicate(IfcProperty property)
		{
			int index = mDuplicateMapping.FindExisting(property);
			if (index > 0)
				return mDatabase[index] as IfcProperty;
			string stepString = dictionaryKey(property);
			IfcProperty result = null;
			if(!string.IsNullOrEmpty(stepString) && mProperties.TryGetValue(stepString, out result))
			{
				mDuplicateMapping.AddObject(property, result.StepId);
				return result;
			}
			result = Duplicate(property, new DuplicateOptions(mDatabase.Tolerance) { DuplicateDownstream = true }) as IfcProperty;
			mProperties[stepString] = result;
			return result;
		}
		internal IfcPropertySetDefinition DuplicatePropertySet(IfcPropertySetDefinition propertySet, DuplicateOptions options)
		{
			if (propertySet.Database != null && propertySet.Database.id == mDatabase.id)
				return propertySet;
			int index = mDuplicateMapping.FindExisting(propertySet);
			if (index > 0)
				return mDatabase[index] as IfcPropertySetDefinition;
			if(options.IgnoredPropertyNames.Count > 0)
			{
				IfcPropertySet pset = propertySet as IfcPropertySet;
				if(pset != null)
				{
					bool ignored = true;
					foreach (IfcProperty property in pset.HasProperties.Values)
					{
						if (!options.IgnoredPropertyNames.Contains(property.Name))
						{
							ignored = false;
							break;
						}
					}
					if (ignored)
						return null;
				}
			}
			IfcPropertySetDefinition result = Duplicate(propertySet, options) as IfcPropertySetDefinition, existing = null;
			string stepString = dictionaryKeyIgnoreId(result);
			if(!string.IsNullOrEmpty(stepString) && mPropertySets.TryGetValue(stepString, out existing))
			{
				if (result.StepId == existing.StepId)
					return result;
				mDuplicateMapping.AddObject(propertySet, existing.Index);
				result.Dispose(false);
				return existing;
			}
			mPropertySets[stepString] = result;
			return result;
		}

		private string dictionaryKey(BaseClassIfc obj)
		{
			string result = obj.STEPSerialization();
			if (string.IsNullOrEmpty(result))
				return null;
			return result.Substring(3);
		}
		private string dictionaryKeyIgnoreId(IfcRoot root)
		{
			string result = dictionaryKey(root);
			if (string.IsNullOrEmpty(result))
				return null;

			return result.Replace("'" + root.GlobalId + "',", "");
		}
		internal void initData()
		{
			initGeom();
			mSILength = new IfcSIUnit(mDatabase, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.NONE, IfcSIUnitName.METRE);
			mSIArea = new IfcSIUnit(mDatabase, IfcUnitEnum.AREAUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SQUARE_METRE);
			mSIVolume = new IfcSIUnit(mDatabase, IfcUnitEnum.VOLUMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.CUBIC_METRE);
		}
		internal void initGeom()
		{
			IfcCartesianPoint point = Origin;
			IfcDirection direction = XAxis;
			direction = YAxis;
			direction = ZAxis;
			IfcAxis2Placement3D pl = this.XYPlanePlacement;
			IfcAxis2Placement2D placement = Origin2dPlace;
		}

		private IfcCartesianPoint mOrigin = null, mWorldOrigin = null, mOrigin2d = null;
		//internal int mTempWorldCoordinatePlacement = 0;

		private IfcLocalPlacement mRootPlacement = null;
		internal IfcAxis2Placement3D mRootPlacementPlane = null;
		internal IfcAxis2Placement3D mPlacementPlaneXY;
		internal IfcAxis2Placement2D m2DPlaceOrigin;
		internal IfcCartesianTransformationOperator3D mTransformationPlaneXY;

		public IfcConversionBasedUnit ConversionUnit(IfcConversionBasedUnit.Common name)
		{
			IfcUnitAssignment assignment = (mDatabase.Context == null ? null : mDatabase.Context.UnitsInContext);
			string nameString = name.ToString().Replace("_", "");
			IfcNamedUnit unit = assignment == null ? null : assignment[IfcUnitEnum.LENGTHUNIT];
			if (unit == null)
				unit = SILength;
			if (name == IfcConversionBasedUnit.Common.inch)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.LENGTHUNIT, nameString, new IfcMeasureWithUnit(new IfcLengthMeasure(unit.SIFactor * 0.0254), unit));
			}
			if (name == IfcConversionBasedUnit.Common.foot)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.LENGTHUNIT, nameString, new IfcMeasureWithUnit(new IfcLengthMeasure(unit.SIFactor * IfcUnitAssignment.FeetToMetre), unit));
			}
			if (name == IfcConversionBasedUnit.Common.yard)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.LENGTHUNIT, nameString, new IfcMeasureWithUnit(new IfcLengthMeasure(unit.SIFactor * 0.914), unit));
			}
			if (name == IfcConversionBasedUnit.Common.mile)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.LENGTHUNIT, nameString, new IfcMeasureWithUnit(new IfcLengthMeasure(unit.SIFactor * 1609), unit));
			}

			unit = assignment == null ? null: assignment[IfcUnitEnum.AREAUNIT];
			if (unit == null)
				unit = SIArea;
			if (name == IfcConversionBasedUnit.Common.square_inch)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.AREAUNIT, nameString, new IfcMeasureWithUnit(new IfcAreaMeasure(unit.SIFactor * 0.0006452), unit));
			}
			if (name == IfcConversionBasedUnit.Common.square_foot)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.AREAUNIT, nameString, new IfcMeasureWithUnit(new IfcAreaMeasure(unit.SIFactor * 0.09290), unit));
			}
			if (name == IfcConversionBasedUnit.Common.square_yard)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.AREAUNIT, nameString, new IfcMeasureWithUnit(new IfcAreaMeasure(unit.SIFactor * 0.83612736), unit));
			}
			if (name == IfcConversionBasedUnit.Common.acre)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.AREAUNIT, nameString, new IfcMeasureWithUnit(new IfcAreaMeasure(unit.SIFactor * 4046.86), unit));
			}
			if (name == IfcConversionBasedUnit.Common.square_mile)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.AREAUNIT, nameString, new IfcMeasureWithUnit(new IfcAreaMeasure(unit.SIFactor * 2588881), unit));
			}

			unit = assignment == null ? null : assignment[IfcUnitEnum.VOLUMEUNIT];
			if (unit == null)
				unit = SIVolume;
			if (name == IfcConversionBasedUnit.Common.cubic_inch)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor * 0.00001639), unit));
			}
			if (name == IfcConversionBasedUnit.Common.cubic_foot)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor * 0.02832), unit));
			}
			if (name == IfcConversionBasedUnit.Common.cubic_yard)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor * 0.7636), unit));
			}
			if (name == IfcConversionBasedUnit.Common.litre)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor * 0.001), unit));
			}
			if (name == IfcConversionBasedUnit.Common.fluid_ounce_UK)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor * 0.0000284130625), unit));
			}
			if (name == IfcConversionBasedUnit.Common.fluid_ounce_US)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor * 0.00002957353), unit));
			}
			if (name == IfcConversionBasedUnit.Common.pint_UK)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor * 0.000568), unit));
			}
			if (name == IfcConversionBasedUnit.Common.pint_US)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor * 0.000473), unit));
			}
			if (name == IfcConversionBasedUnit.Common.gallon_UK)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor * 0.004546), unit));
			}
			if (name == IfcConversionBasedUnit.Common.gallon_UK)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor * 0.003785), unit));
			}

			unit = assignment == null ? null :assignment[IfcUnitEnum.PLANEANGLEUNIT];
			if (name == IfcConversionBasedUnit.Common.degree)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.PLANEANGLEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.RADIAN);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.PLANEANGLEUNIT, nameString, new IfcMeasureWithUnit(new IfcPlaneAngleMeasure(unit.SIFactor * Math.PI / 180.0), unit));
			}

			unit = assignment == null ? null : assignment[IfcUnitEnum.MASSUNIT];
			if (name == IfcConversionBasedUnit.Common.ounce)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.MASSUNIT, IfcSIPrefix.NONE, IfcSIUnitName.GRAM);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.MASSUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor * 28.35), unit));
			}
			if (name == IfcConversionBasedUnit.Common.pound)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.MASSUNIT, IfcSIPrefix.NONE, IfcSIUnitName.GRAM);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.MASSUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor * 454), unit));
			}
			if (name == IfcConversionBasedUnit.Common.ton_UK)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.MASSUNIT, IfcSIPrefix.NONE, IfcSIUnitName.GRAM);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.MASSUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor * 1016046.9088), unit));
			}
			if (name == IfcConversionBasedUnit.Common.ton_US)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.MASSUNIT, IfcSIPrefix.NONE, IfcSIUnitName.GRAM);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.MASSUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor * 907184.74), unit));
			}

			unit = assignment == null ? null : assignment[IfcUnitEnum.FORCEUNIT];
			if (name == IfcConversionBasedUnit.Common.lbf)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.FORCEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.NEWTON);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.FORCEUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor * 4.4482216153), unit));
			}
			if (name == IfcConversionBasedUnit.Common.kip)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.FORCEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.NEWTON);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.FORCEUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor * 4448.2216153), unit));
			}

			unit = assignment == null ? null : assignment[IfcUnitEnum.PRESSUREUNIT];
			if (name == IfcConversionBasedUnit.Common.kip)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.PRESSUREUNIT, IfcSIPrefix.NONE, IfcSIUnitName.PASCAL);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.FORCEUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor * 6894.7572932), unit));
			}
			if (name == IfcConversionBasedUnit.Common.ksi)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.PRESSUREUNIT, IfcSIPrefix.NONE, IfcSIUnitName.PASCAL);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.FORCEUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor * 6894757.2932), unit));
			}
			unit = assignment == null ? null : assignment[IfcUnitEnum.TIMEUNIT];
			if (name == IfcConversionBasedUnit.Common.minute)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.TIMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SECOND);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.TIMEUNIT, nameString, new IfcMeasureWithUnit(new IfcTimeMeasure(unit.SIFactor*60), unit));
			}
			if (name == IfcConversionBasedUnit.Common.hour)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.TIMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SECOND);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.TIMEUNIT, nameString, new IfcMeasureWithUnit(new IfcTimeMeasure(unit.SIFactor * 3600), unit));
			}
			if (name == IfcConversionBasedUnit.Common.day)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.TIMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SECOND);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.TIMEUNIT, nameString, new IfcMeasureWithUnit(new IfcTimeMeasure(unit.SIFactor * 86400), unit));
			}

			unit = assignment == null ? null : assignment[IfcUnitEnum.ENERGYUNIT];
			if (name == IfcConversionBasedUnit.Common.btu)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.ENERGYUNIT, IfcSIPrefix.NONE, IfcSIUnitName.JOULE);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.ENERGYUNIT, nameString, new IfcMeasureWithUnit(new IfcTimeMeasure(unit.SIFactor * 1055.056), unit));
			}
			return null;
		}
		public IfcUnit LengthUnit(IfcUnitAssignment.Length length)
		{
			if (length == IfcUnitAssignment.Length.Millimetre)
				return new IfcSIUnit(mDatabase, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.MILLI, IfcSIUnitName.METRE);
			if (length == IfcUnitAssignment.Length.Centimetre)
				return new IfcSIUnit(mDatabase, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.CENTI, IfcSIUnitName.METRE);
			if (length == IfcUnitAssignment.Length.Inch)
				return ConversionUnit(IfcConversionBasedUnit.Common.inch);
			if (length == IfcUnitAssignment.Length.Foot)
				return ConversionUnit(IfcConversionBasedUnit.Common.foot);
			return SILength;
		}
		internal IfcSIUnit mSILength, mSIArea, mSIVolume;
		public IfcSIUnit SILength { get { if(mSILength == null) mSILength = new IfcSIUnit(mDatabase, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.NONE, IfcSIUnitName.METRE); return mSILength; } }
		public IfcSIUnit SIArea { get { if(mSIArea == null) mSIArea = new IfcSIUnit(mDatabase, IfcUnitEnum.AREAUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SQUARE_METRE); return mSIArea; } }
		public IfcSIUnit SIVolume { get { if(mSIVolume == null) mSIVolume = new IfcSIUnit(mDatabase, IfcUnitEnum.VOLUMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.CUBIC_METRE); return mSIVolume; } }

		internal IfcDirection mXAxis, mYAxis, mZAxis, mNegXAxis, mNegYAxis, mNegZAxis;
		internal IfcLine mLineX2d = null;

		public IfcDirection Direction(double x, double y, double z)
		{
			if (double.IsNaN(z))
				return new IfcDirection(mDatabase, x, y);
			double length = Math.Sqrt(x * x + y * y + z * z), tol = mDatabase.Tolerance;
			double dx = x / length, dy = y / length, dz = z / length;
			if (Math.Abs(dx - 1) < tol)
				return XAxis;
			if (Math.Abs(dy - 1) < tol)
				return YAxis;
			if (Math.Abs(dz - 1) < tol)
				return ZAxis;
			if (Math.Abs(dx + 1) < tol)
				return XAxisNegative;
			if (Math.Abs(dy + 1) < tol)
				return YAxisNegative;
			if (Math.Abs(dz + 1) < tol)
				return ZAxisNegative;
			return new IfcDirection(mDatabase, x, y, z);
		}

		public IfcDirection XAxis { get { if (mXAxis == null) mXAxis = new IfcDirection(mDatabase, 1, 0, 0); return mXAxis; } }
		public IfcDirection YAxis { get { if (mYAxis == null) mYAxis = new IfcDirection(mDatabase, 0, 1, 0); return mYAxis; } }
		public IfcDirection ZAxis { get { if (mZAxis == null) mZAxis = new IfcDirection(mDatabase, 0, 0, 1); return mZAxis; } }
		public IfcDirection XAxisNegative { get { if (mNegXAxis == null) mNegXAxis = new IfcDirection(mDatabase, -1, 0, 0); return mNegXAxis; } }
		public IfcDirection YAxisNegative { get { if (mNegYAxis == null) mNegYAxis = new IfcDirection(mDatabase, 0, -1, 0); return mNegYAxis; } }
		public IfcDirection ZAxisNegative { get { if (mNegZAxis == null) mNegZAxis = new IfcDirection(mDatabase, 0, 0, -1); return mNegZAxis; } }

		public IfcLine LineX2d { get { if (mLineX2d == null) mLineX2d = new IfcLine(Origin2d, new IfcVector(new IfcDirection(mDatabase, 1, 0), 1));  return mLineX2d; } }

		partial void getApplicationFullName(ref string app);
		partial void getApplicationIdentifier(ref string app);
		partial void getApplicationDeveloper(ref string app);

		private string mApplicationFullName = "", mApplicationIdentifier = "", mApplicationDeveloper = "";
		public string ToolkitName
		{
			get
			{
				try
				{
					Assembly assembly = typeof(BaseClassIfc).Assembly;
					AssemblyName name = assembly.GetName();
					string date = String.Format("{0:s}", File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToUniversalTime());
					return name.Name + " v" + name.Version.ToString() + " by Geometry Gym Pty Ltd built " + date;
				}
				catch (Exception)
				{
					return "GeomGymIFC by Geometry Gym Pty Ltd";
				}
			}
		}
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
							Assembly assembly = Assembly.GetEntryAssembly();
							if (assembly == null)
								assembly = Assembly.GetCallingAssembly();
							if (assembly != null)
							{ 
								AssemblyName name = assembly.GetName();
								return name.Name + " v" + name.Version.ToString();
							}
						}
						catch (Exception) {  }
						return "Unknown Application";
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

		internal IfcApplication mApplication = null;
		public IfcApplication Application
		{
			get
			{
				if (mApplication == null)
					mApplication = new IfcApplication(mDatabase);
				return mApplication;
			}
			set
			{
				mApplication = value;
				if(string.IsNullOrEmpty(mApplicationFullName))
				{
					ApplicationFullName = mApplication.ApplicationFullName + (string.IsNullOrEmpty(mApplication.Version) ? "" : " " + mApplication.Version);
				}
			}
		}
		private IfcPersonAndOrganization mPersonOrganization = null;
		internal IfcPersonAndOrganization PersonOrganization
		{
			get
			{
				if (mPersonOrganization == null)
					mPersonOrganization = new IfcPersonAndOrganization(Person, Organization);
				return mPersonOrganization;
			}
			set { mPersonOrganization = value; }
		}
		internal string PersonName() { return System.Environment.UserName; }
		internal IfcPerson mPerson = null;
		internal IfcPerson Person
		{
			get
			{
				if (mPerson == null)
				{
					mPerson = new IfcPerson(mDatabase, PersonName(), "", "");
#if (IFCMODEL && !IFCIMPORTONLY && (RHINO || GH))
					string str = ggAssembly.mOptions.OwnerRole;
					if (!string.IsNullOrEmpty(str))
					{
						IfcRoleEnum role = IfcRoleEnum.NOTDEFINED;
						if (Enum.TryParse<IfcRoleEnum>(str, out role))
						{
							if (role != IfcRoleEnum.NOTDEFINED)
								mPerson.Roles.Add(new IfcActorRole(mDatabase, role, "", "", new List<int>()));
						}
						else
							mPerson.Roles.Add(new IfcActorRole(mDatabase, IfcRoleEnum.USERDEFINED, str, "", new List<int>()));
					}
#endif
				}
				return mPerson;
			}
			set { mPerson = value; }
		}
		internal IfcOrganization mOrganization = null;
		internal IfcOrganization Organization
		{
			get
			{
				if (mOrganization == null)
					mOrganization = new IfcOrganization(mDatabase, IfcOrganization.Organization);
				return mOrganization;
			}
			set
			{
				mOrganization = value;
			}
		}
		internal List<IfcOwnerHistory> mOwnerHistories = new List<IfcOwnerHistory>();
		public IfcOwnerHistory OwnerHistory(IfcChangeActionEnum action, IfcPersonAndOrganization owner, IfcApplication application, DateTime created)
		{
			return OwnerHistory(action, owner, application, null, null, action == IfcChangeActionEnum.NOTDEFINED || action == IfcChangeActionEnum.NOCHANGE ? DateTime.MinValue : created, created);	
		}
		public IfcOwnerHistory OwnerHistory(IfcChangeActionEnum action, IfcPersonAndOrganization owner, IfcApplication application, IfcPersonAndOrganization modifier, IfcApplication modApplication, DateTime modified, DateTime created)
		{
			if (!mOptions.GenerateOwnerHistory)
				return null;
			IfcOwnerHistory ownerHistory = new IfcOwnerHistory(owner, application, action);
			ownerHistory.LastModifyingUser = modifier;
			ownerHistory.LastModifyingApplication = modApplication;
			ownerHistory.CreationDate = created;
			ownerHistory.LastModifiedDate = modified;
			foreach (IfcOwnerHistory oh in mOwnerHistories)
			{
				if (oh.isDuplicate(ownerHistory, mDatabase.Tolerance))
				{
					ownerHistory.Dispose(false);
					return oh;
				}
			}
			mOwnerHistories.Add(ownerHistory);
			return ownerHistory;
		}
		public void AddOwnerHistory(IfcOwnerHistory ownerHistory)
		{
			foreach (IfcOwnerHistory oh in mOwnerHistories)
			{
				if (oh.isDuplicate(ownerHistory, mDatabase.Tolerance))
					return;
			}
			mOwnerHistories.Add(ownerHistory);
		}
		private IfcOwnerHistory mOwnerHistoryAdded = null;
		public IfcOwnerHistory OwnerHistoryAdded
		{
			get
			{
				if (mDatabase.Release > ReleaseVersion.IFC2x3 && (!mOptions.GenerateOwnerHistory || mDatabase.ModelView == ModelView.Ifc4Reference))
					return null;
				if(mOwnerHistoryAdded == null)
					mOwnerHistoryAdded = new IfcOwnerHistory(PersonOrganization, Application, IfcChangeActionEnum.ADDED);
				return mOwnerHistoryAdded;
			}
		}

		public IfcOwnerHistory OwnerHistory(IfcChangeActionEnum action, IfcRoot original, IfcRoot revision)
		{
			IfcOwnerHistory ownerHistory = (original == null ? null : original.OwnerHistory), revised = (revision == null ? null : revision.OwnerHistory);
			if (ownerHistory == null && original != null)
				return null;
			else if (ownerHistory != null && ownerHistory.mDatabase != mDatabase)
				ownerHistory = Duplicate(ownerHistory) as IfcOwnerHistory;
			IfcPersonAndOrganization owner = ownerHistory == null ? PersonOrganization : ownerHistory.OwningUser;
			IfcApplication owningApplication = ownerHistory == null ? Application : ownerHistory.OwningApplication;
			DateTime created = ownerHistory == null ? DateTime.UtcNow : ownerHistory.CreationDate;
			IfcPersonAndOrganization modifier = null;
			if (revised == null)
				modifier = (action == IfcChangeActionEnum.ADDED ? null : PersonOrganization);
			else
			{
				if (revised.OwningUser.mDatabase == mDatabase)
					modifier = revised.OwningUser;
				else
					modifier = Duplicate(revised.OwningUser) as IfcPersonAndOrganization;
			}
			IfcApplication application = null;
			if (revised == null)
				application = (action == IfcChangeActionEnum.ADDED ? null : Application);
			else
				application = revised.OwningApplication.mDatabase == mDatabase ? revised.OwningApplication : Duplicate(revised.OwningApplication) as IfcApplication;
			DateTime modified = (revised == null ? (action == IfcChangeActionEnum.ADDED ? DateTime.MinValue : DateTime.Now) : revised.CreationDate);
			return OwnerHistory(action, owner, owningApplication, modifier, application, modified, created);
		}

		internal Dictionary<IfcGeometricRepresentationContext.GeometricContextIdentifier, IfcGeometricRepresentationContext> mContexts = new Dictionary<IfcGeometricRepresentationContext.GeometricContextIdentifier, IfcGeometricRepresentationContext>();		
		public IfcGeometricRepresentationContext GeometricRepresentationContext(IfcGeometricRepresentationContext.GeometricContextIdentifier nature)
		{
			IfcGeometricRepresentationContext result = null;
			if (mContexts.TryGetValue(nature, out result))
				return result;
			string type = nature.ToString();
			int dimension = 3;

			result = new IfcGeometricRepresentationContext(mDatabase, dimension, mDatabase.Tolerance) { ContextType = type };
			IfcContext context = mDatabase.Context;
			if (context != null && !context.RepresentationContexts.Contains(result))
				context.RepresentationContexts.Add(result);
			mContexts.Add(nature, result);
			return result;
		}
		internal void IdentifyContexts(IEnumerable<IfcRepresentationContext> contexts)
		{
			if (mContexts.ContainsKey(IfcGeometricRepresentationContext.GeometricContextIdentifier.Model))
				return;
			foreach(IfcRepresentationContext context in contexts)
			{
				IfcGeometricRepresentationContext grc = context as IfcGeometricRepresentationContext;
				if(grc != null)
				{
					if (!mContexts.ContainsKey(IfcGeometricRepresentationContext.GeometricContextIdentifier.Model) && string.Compare(grc.ContextType, "Model", true) == 0)
						mContexts.Add(IfcGeometricRepresentationContext.GeometricContextIdentifier.Model, grc);
				}
			}
		}
		public IfcGeometricRepresentationSubContext SubContext(IfcGeometricRepresentationSubContext.SubContextIdentifier nature)
		{
			IfcGeometricRepresentationSubContext result = null;
			if (mSubContexts.TryGetValue(nature, out result))
				return result;
			string identifier = "Body";
			IfcGeometricProjectionEnum projection = IfcGeometricProjectionEnum.MODEL_VIEW;
			IfcGeometricRepresentationContext context = null;
			if (nature == IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis)
			{
				identifier = "Axis";
				projection = IfcGeometricProjectionEnum.GRAPH_VIEW;
			}
			//else if(nature == IfcGeometricRepresentationSubContext.SubContextIdentifier.PVI)
			//{
			//	identifier = "PVI";
			//	projection = IfcGeometricProjectionEnum.GRAPH_VIEW;
			//}
			else if (nature == IfcGeometricRepresentationSubContext.SubContextIdentifier.BoundingBox)
			{
				projection = IfcGeometricProjectionEnum.MODEL_VIEW;
				identifier = "Box";
			}
			else if (nature == IfcGeometricRepresentationSubContext.SubContextIdentifier.FootPrint)
			{
				identifier = "FootPrint";
			}
			else if (nature == IfcGeometricRepresentationSubContext.SubContextIdentifier.Outline)
			{
				identifier = "Outline";
			}
			else if (nature == IfcGeometricRepresentationSubContext.SubContextIdentifier.PlanSymbol3d)
			{
				projection = IfcGeometricProjectionEnum.PLAN_VIEW;
				identifier = "Annotation";
			}
			else if (nature == IfcGeometricRepresentationSubContext.SubContextIdentifier.PlanSymbol2d)
			{
				projection = IfcGeometricProjectionEnum.PLAN_VIEW;
				identifier = "Annotation";
				context = GeometricRepresentationContext(IfcGeometricRepresentationContext.GeometricContextIdentifier.Plan);
			}
			else if (nature == IfcGeometricRepresentationSubContext.SubContextIdentifier.Row)
			{
				projection = IfcGeometricProjectionEnum.GRAPH_VIEW;
				identifier = "Row";
			}
			if (context == null)
				context = GeometricRepresentationContext(IfcGeometricRepresentationContext.GeometricContextIdentifier.Model);
			result = new IfcGeometricRepresentationSubContext(context, projection ) { ContextIdentifier = identifier };
			mSubContexts.Add(nature, result);
			return result;
		}
		private Dictionary<IfcGeometricRepresentationSubContext.SubContextIdentifier, IfcGeometricRepresentationSubContext> mSubContexts = new Dictionary<IfcGeometricRepresentationSubContext.SubContextIdentifier, IfcGeometricRepresentationSubContext>();		
		public IfcCartesianPoint Origin
		{
			get
			{
				if (mOrigin == null)
					mOrigin = new IfcCartesianPoint(mDatabase, 0, 0, 0);
				return mOrigin;
			}
		}
		public IfcCartesianPoint Origin2d
		{
			get
			{
				if (mOrigin2d == null)
					mOrigin2d = new IfcCartesianPoint(mDatabase, 0, 0);
				return mOrigin2d;
			}
		}
		public IfcLocalPlacement RootPlacement
		{
			get
			{
				if (mRootPlacement == null)
					mRootPlacement = new IfcLocalPlacement(RootPlacementPlane);
				return mRootPlacement;
			}
		}
		internal IfcAxis2Placement3D RootPlacementPlane
		{
			get
			{
				if (mRootPlacementPlane == null)
					mRootPlacementPlane = new IfcAxis2Placement3D(new IfcCartesianPoint(mDatabase, 0, 0, 0), mZAxis, mXAxis);
				return mRootPlacementPlane;
			}
		}
		public IfcAxis2Placement3D XYPlanePlacement
		{
			get
			{
				if (mPlacementPlaneXY == null)
					mPlacementPlaneXY = new IfcAxis2Placement3D(Origin);
				return mPlacementPlaneXY;
			}
		}
		public IfcCartesianTransformationOperator3D XYPlaneTransformation
		{
			get
			{
				if (mTransformationPlaneXY == null)
					mTransformationPlaneXY = new IfcCartesianTransformationOperator3D(mDatabase);
				return mTransformationPlaneXY;
			}
		}
		public IfcAxis2Placement2D Origin2dPlace
		{
			get
			{
				if (m2DPlaceOrigin == null)
					m2DPlaceOrigin = new IfcAxis2Placement2D(new IfcCartesianPoint(mDatabase, 0, 0));
				return m2DPlaceOrigin;
			}
		}

		private Dictionary<string, IfcBoundaryNodeCondition> mBoundaryNodeConditions = new Dictionary<string, IfcBoundaryNodeCondition>();
		public IfcBoundaryNodeCondition FindOrCreateBoundaryNodeCondition(bool isPointRestraint, bool x, bool y, bool z, bool xx, bool yy, bool zz)
		{
			return FindOrCreateBoundaryNodeCondition(isPointRestraint, new Triple<IfcTranslationalStiffnessSelect>(new IfcTranslationalStiffnessSelect(x),new IfcTranslationalStiffnessSelect(y), new IfcTranslationalStiffnessSelect(z)),
				new Triple<IfcRotationalStiffnessSelect>(new  IfcRotationalStiffnessSelect(xx), new IfcRotationalStiffnessSelect(yy), new IfcRotationalStiffnessSelect(zz)));
		}
		public IfcBoundaryNodeCondition FindOrCreateBoundaryNodeCondition(bool isPointRestraint, Triple<IfcTranslationalStiffnessSelect> translational, Triple<IfcRotationalStiffnessSelect> rotational)
		{
			string name = "";
			bool partial = false;

			if (translational.X == null)
				name += isPointRestraint ? 'F' : 'T';
			else if (!translational.X.mRigid && translational.X.mStiffness != null)
				partial = true;
			else
				name += (translational.X.mRigid ? 'T' : 'F');
			if (translational.Y == null)
				name += isPointRestraint ? 'F' : 'T';
			else if (!translational.Y.mRigid && translational.Y.mStiffness != null)
				partial = true;
			else
				name += (translational.Y.mRigid ? 'T' : 'F');
			if (translational.Z == null)
				name += isPointRestraint ? 'F' : 'T';
			else if (!translational.Z.mRigid && translational.Z.mStiffness != null)
				partial = true;
			else
				name += (translational.Z.mRigid ? 'T' : 'F');
			if (rotational.X == null)
				name += isPointRestraint ? 'F' : 'T';
			else if (!rotational.X.mRigid && rotational.X.mStiffness != null)
				partial = true;
			else
				name += (rotational.X.mRigid ? 'T' : 'F');
			if (rotational.Y == null)
				name += isPointRestraint ? 'F' : 'T';
			else if (!rotational.Y.mRigid && rotational.Y.mStiffness != null)
				partial = true;
			else
				name += (rotational.Y.mRigid ? 'T' : 'F');
			if (rotational.Z == null)
				name += isPointRestraint ? 'F' : 'T';
			else if (!rotational.Z.mRigid && rotational.Z.mStiffness != null)
				partial = true;
			else
				name += (rotational.Z.mRigid ? 'T' : 'F');

			if (partial)
			{
#warning implement
				return new IfcBoundaryNodeCondition(mDatabase, "", translational.X, translational.Y, translational.Z, rotational.X, rotational.Y, rotational.Z);
			}
			if (mBoundaryNodeConditions.ContainsKey(name))
				return mBoundaryNodeConditions[name];
			IfcBoundaryNodeCondition result = new IfcBoundaryNodeCondition(mDatabase, name, translational.X, translational.Y, translational.Z, rotational.X, rotational.Y, rotational.Z);
			mBoundaryNodeConditions.Add(name, result);
			return result;
		}
	}

	public class DuplicateOptions
	{
		public bool DuplicateHost { get; set; } 
		public bool DuplicateDownstream { get; set; } 
		public double DeviationTolerance { get; set; }
		public IfcOwnerHistory OwnerHistory { get; set; }
		public bool DuplicateOwnerHistory { get; set; } = true;

		public HashSet<string> IgnoredPropertyNames = new HashSet<string>();

		public DuplicateOptions(double deviationTolerance) 
		{
			DeviationTolerance = deviationTolerance; 
			DuplicateHost = true;  
			DuplicateDownstream = true; 
		}
		public DuplicateOptions(DuplicateOptions options)
		{
			DuplicateHost = options.DuplicateHost;
			DuplicateDownstream = options.DuplicateDownstream;
			DeviationTolerance = options.DeviationTolerance;
			OwnerHistory = options.OwnerHistory;
			DuplicateOwnerHistory = options.DuplicateOwnerHistory;
		}
	}
	public class ApplicableFilter
	{
		internal string Filter { get; private set; }
		private List<ApplicableType> mApplicableTypes = new List<ApplicableType>();
		public ApplicableFilter(string filter)
		{
			mApplicableTypes = new List<ApplicableType>();
            Filter = filter;
			string[] fields = filter.Split(",;".ToCharArray());
			foreach (string str in fields)
			{
				if (string.IsNullOrEmpty(str))
					continue;
				string[] pair = str.Trim().Split("/".ToCharArray());
				string typename = (pair == null ? str.Trim() : pair[0]), predefined = pair == null || pair.Length < 2 ? "" : pair[0];
				Type type = BaseClassIfc.GetType(typename); 
				if (type == null)
					continue;
				mApplicableTypes.Add(new ApplicableType(type, predefined));
			}
		}

		public bool IsApplicable(IfcObjectDefinition obj)
		{
			return IsApplicable(obj.GetType(), obj.GetObjectDefinitionType());
		}
		public bool IsApplicable(Type typeIfc, string predefined)
		{
			foreach (ApplicableType t in mApplicableTypes)
			{
				if (t.isApplicable(typeIfc, predefined))
					return true;
			}
			return false;
		}
	}
	internal class ApplicableType
	{
		private Type mType = null;
		private string mPredefined = "";

		private HashSet<Type> mApplicable = new HashSet<Type>();
		private HashSet<Type> mNotApplicableTo = new HashSet<Type>();

		internal ApplicableType(Type type) { mType = type; mApplicable.Add(type); }
		internal ApplicableType(Type type, string predefined) : this(type) { mPredefined = predefined; }

		internal bool isApplicable(Type type)
		{
			if (mApplicable.Contains(type))
				return true;
			if (mNotApplicableTo.Contains(type))
				return false;
			if(type.IsSubclassOf(mType))
			{
				mApplicable.Add(type);
				return true;
			}
			mNotApplicableTo.Add(type);
			return false;
		}
		internal bool isApplicable(Type type, string predefined)
		{
			if (mApplicable.Contains(type))
				return (string.IsNullOrEmpty(mPredefined) || string.Compare(predefined, mPredefined, true) == 0);
			if (type.IsSubclassOf(mType))
			{
				mApplicable.Add(type);
				return (string.IsNullOrEmpty(mPredefined) || string.Compare(predefined, mPredefined, true) == 0);
			}
			return false;
		}

	}

	internal class SerializationIfc
	{
		protected DatabaseIfc mDatabase = null;
		protected CultureInfo mCachedCulture = null;
		private bool mCachedOwnerHistoryOption = false;

		internal SerializationIfc(DatabaseIfc db)
		{
			mDatabase = db;
		}

		internal class FileStreamIfc : IDisposable
		{
			internal FormatIfcSerialization mFormat;
			internal TextReader mTextReader;
			internal FileStreamIfc(FormatIfcSerialization format, TextReader sr)
			{
				mFormat = format;
				mTextReader = sr;
			}

			void IDisposable.Dispose()
			{
				if (mTextReader != null)
					mTextReader.Close();
			}
		}
		private FormatIfcSerialization detectFormat(string fileName)
		{
			string lower = fileName.ToLower();
			if (lower.EndsWith("xml"))
				return FormatIfcSerialization.XML;
#if (!NOIFCJSON)
			if (lower.EndsWith("json"))
				return FormatIfcSerialization.JSON;
#endif
			return FormatIfcSerialization.STEP;
		}
		internal FileStreamIfc getStreamReader(string fileName)
		{
			string ext = Path.GetExtension(fileName);
			mDatabase.FileName = fileName;
			mDatabase.FolderPath = Path.GetDirectoryName(fileName);
#if (!NOIFCZIP)
			if (fileName.ToLower().EndsWith("zip"))
			{
				System.IO.Compression.ZipArchive za = System.IO.Compression.ZipFile.OpenRead(fileName);
				if (za.Entries.Count != 1)
				{
					return null;
				}
				string filename = za.Entries[0].Name.ToLower();
				FormatIfcSerialization fformat = detectFormat(filename);
				StreamReader str = (fformat == FormatIfcSerialization.STEP ? new StreamReader(za.Entries[0].Open(), Encoding.GetEncoding("windows-1252")) :
					new StreamReader(za.Entries[0].Open()));
				return new FileStreamIfc(fformat, str);
			}
#endif
			FormatIfcSerialization format = detectFormat(fileName);
			StreamReader sr = format == FormatIfcSerialization.STEP ? new StreamReader(fileName, Encoding.GetEncoding("windows-1252")) :
				new StreamReader(fileName);
			return new FileStreamIfc(format, sr);
		}

		internal void ReadFile(string filename)
		{
			if (string.IsNullOrEmpty(filename))
				return;

			mDatabase.FolderPath = Path.GetDirectoryName(filename);
			if (filename.ToLower().EndsWith("ifc"))
				new SerializationIfcSTEP(mDatabase).ReadStepFile(filename);
			else
			{
				using (FileStreamIfc fileStream = getStreamReader(filename))
				{
					ReadFile(fileStream);
				}
			}
		}
		internal void ReadFile(FileStreamIfc fs)
		{
			if (fs == null)
				return;

			switch (fs.mFormat)
			{
				case FormatIfcSerialization.XML:
					initializeImport();
					mDatabase.ReadXMLStream(fs.mTextReader);
					finalizeImport();
					break;
#if (!NOIFCJSON)
				case FormatIfcSerialization.JSON:
					initializeImport();
					mDatabase.ReadJSONFile(fs.mTextReader);
					finalizeImport();
					break;
#endif
				default:
					ReadFile(fs.mTextReader);
					break;
			}
		}

		internal IfcContext ReadFile(TextReader sr)
		{
			int i = sr.Peek();
			if (i < 0)
				return null;
#if (!NOIFCJSON)
			if (i == (int)'{')
			{
				ReadFile(new FileStreamIfc(FormatIfcSerialization.JSON, sr));
				return mDatabase.Context;
			}
#endif
			if (i == (int)'<')
			{
				ReadFile(new FileStreamIfc(FormatIfcSerialization.XML, sr));
				return mDatabase.Context;
			}
			new SerializationIfcSTEP(mDatabase).ReadStepStream(sr);
			return mDatabase.Context;

		}
		protected void initializeImport()
		{
			mDatabase.Release = ReleaseVersion.IFC2x3;
			mCachedOwnerHistoryOption = mDatabase.Factory.Options.GenerateOwnerHistory;
			mDatabase.Factory.Options.GenerateOwnerHistory = false;
			mCachedCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
		}
		protected void finalizeImport()
		{
			Thread.CurrentThread.CurrentCulture = mCachedCulture;
			mDatabase.postImport();
			mDatabase.Factory.Options.GenerateOwnerHistory = mCachedOwnerHistoryOption;
		}
	}

	internal partial class SerializationIfcSTEP : SerializationIfc
	{
		private BlockingCollection<string> mDataLines = new BlockingCollection<string>();
		private ConcurrentBag<ConstructorClass> mConstructorsBag = new ConcurrentBag<ConstructorClass>();
		private ConcurrentBag<ConstructorClass> mPrimitiveConstructors = new ConcurrentBag<ConstructorClass>();
		private ConcurrentDictionary<int, BaseClassIfc> mDictionary = new ConcurrentDictionary<int, BaseClassIfc>();

		CancellationTokenSource mCancellationTokenSource = new CancellationTokenSource();

		private class ConstructorClass
		{
			internal BaseClassIfc Object { get; }
			private string DefinitionString { get; }
			internal ConstructorClass(BaseClassIfc obj, string definition) { Object = obj; DefinitionString = definition; }
	
			internal void ParseDefinition(ReleaseVersion release, ConcurrentDictionary<int, BaseClassIfc> dictionary)
			{
				int pos = 0;
				Object.parse(DefinitionString, ref pos, release, DefinitionString.Length, dictionary);
			}
		}

		protected Dictionary<string,string> mLinesToIgnore = new Dictionary<string, string>() {
			{ "ISO-10303-21;", "ISO-10303-21;" },
			{ "HEADER;", "HEADER" },
			{ "ENDSEC;", "ENDSEC;" },
			{ "DATA;", "DATA;" },
			{ "END-ISO-10303-21;", "END-ISO-10303-21;" } };

		internal SerializationIfcSTEP(DatabaseIfc db) : base(db) 
		{
			mDictionary[0] = null;
		}

		public void ReadStepFile(string stepFilePath)
		{
			initializeImport();
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Started Reading File");
			using (StreamReader reader = new StreamReader(stepFilePath))
			{
				string line = "";
				while((line = reader.ReadLine()) != null)
				{
					string str = line.Trim();
					if (str.ToUpper().StartsWith("DATA"))
						break;
					processFileHeaderLine(str);
				}
			}
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Started Reading Data");

			ParallelOptions parallelOptions = new ParallelOptions();
			parallelOptions.CancellationToken = mCancellationTokenSource.Token;
			parallelOptions.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

			try
			{
				Parallel.ForEach(File.ReadAllLines(stepFilePath), parallelOptions, x =>
				{
					processDataLine(x);
					parallelOptions.CancellationToken.ThrowIfCancellationRequested();
				});
			}
			catch (OperationCanceledException)
			{
				new SerializationIfcSTEP(mDatabase).importLines(File.ReadAllLines(stepFilePath));
				return;
			}
			processObjects();
		}
		public void ReadStepStream(TextReader stream)
		{
			initializeImport();
			string line = "";
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Started Reading File");
			while ((line = stream.ReadLine()) != null)
			{
				if (string.IsNullOrEmpty(line))
					continue;
				string str = line.Trim();
				if (string.IsNullOrEmpty(str))
					continue;
				char c = str.Last();
				while(c != ';')
				{
					line = stream.ReadLine();
					if (string.IsNullOrEmpty(line))
						continue;
					str += line.Trim();
					c = str.Last();
				}
				if (line.ToUpper().StartsWith("DATA"))
					break;
				processFileHeaderLine(str);
			}

			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Started Reading Data");
			List<string> lines = new List<string>();
			while ((line = stream.ReadLine()) != null)
			{
				string str = line.Trim();
				if (string.IsNullOrEmpty(str))
					continue;
				char c = str.Last();
				while (c != ';')
				{
					line = stream.ReadLine();
					if (string.IsNullOrEmpty(line))
						continue;
					str += line.Trim();
					c = str.Last();
				}
				if (line.ToUpper().StartsWith("ENDSEC"))
					break;
				lines.Add(str);
			}
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Completed Reading Data Lines");
			Parallel.ForEach(lines, x => processDataLine(x));
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Completed Process Data Lines");
		
			processObjects();
		}

		internal void importLines(IEnumerable<string> lines)
		{
			initializeImport();
			mPrimitiveConstructors = new ConcurrentBag<ConstructorClass>();
			List<string> revised = new List<string>();
			int count = lines.Count();
			for (int icounter = 0; icounter < count; icounter++)
			{
				string strLine = lines.ElementAt(icounter);
				if (string.IsNullOrEmpty(strLine) || (strLine.Length < 20 && mLinesToIgnore.ContainsKey(strLine.ToUpper())))
					continue;
				strLine = strLine.Trim();
				if (!strLine.EndsWith(";"))
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
							if (icounter + 1 < count)
							{
								str3 = lines.ElementAt(++icounter);
								if (string.IsNullOrEmpty(str3))
									continue;
								index2 = str3.IndexOf("*/");
							}
							else
								break;
						}
						strLine = str2;
						if (strLine != null && index2 + 2 < str3.Length)
							strLine += str3.Substring(index2 + 2);
					}
					strLine = strLine.Trim();
					if (string.IsNullOrEmpty(strLine))
						continue;
					if (!strLine.EndsWith(");"))
					{
						while (icounter + 1 < count)
						{
							string str = lines.ElementAt(++icounter);
							if (!string.IsNullOrEmpty(str))
							{
								index = str.IndexOf("/*");
								if (index >= 0)
								{
									string str2 = "", str3 = str;
									if (index > 0)
										str2 = str3.Substring(0, index);
									int index2 = str3.IndexOf("*/");
									while (index2 < 0)
									{
										if (icounter + 1 >= count)
											break;
										str3 = lines.ElementAt(++icounter);
										while (string.IsNullOrEmpty(str3))
										{
											if (icounter + 1 >= count)
												break;
											str3 = lines.ElementAt(++icounter);
										}
										index2 = str3.IndexOf("*/");
									}
									str = str2;
									if (!string.IsNullOrEmpty(str3) && index2 + 2 < str3.Length)
										str += str3.Substring(index2 + 2);
								}
								strLine += str;
								strLine.Trim();
							}
							if (strLine.EndsWith(";")) //);
								break;
						}
					}
				}
				if (isDataLineOrProcessFileLine(strLine))
					revised.Add(strLine);
			}

			Parallel.ForEach(revised, x => processDataLine(x));
			processObjects();
		}

		private void processObjects()
		{
			ReleaseVersion release = mDatabase.Release;

			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Processing Objects");
			Task taskPrimitive = Task.Run(() =>
			{
				Parallel.ForEach(mPrimitiveConstructors, x => x.ParseDefinition(release, mDictionary));
				Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Completed Primitive Constructors");
			});
		
			foreach (KeyValuePair<int, BaseClassIfc> pair in mDictionary)
				mDatabase[pair.Key] = pair.Value;
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Completed set database");

			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Started Processing Constructors");
			List<ConstructorClass> threadSafeConstructors = new List<ConstructorClass>();
			List<ConstructorClass> firstPass = new List<ConstructorClass>();
			List<ConstructorClass> secondPass = new List<ConstructorClass>();
			foreach (ConstructorClass obj in mConstructorsBag)
			{
				try
				{
					BaseClassIfc o = obj.Object;
					if (o is IfcPropertySet || o is IfcMaterialConstituentSet)
						secondPass.Add(obj);
					else if (o is IfcTessellatedFaceSet || o is IfcPolyLoop || o is IfcFacetedBrep)
						threadSafeConstructors.Add(obj);
					else
						firstPass.Add(obj);
				}
				catch (Exception x) { mDatabase.logParseError("XXX Error in line #" + obj.Object.Index + " " + obj.Object.StepClassName + " " + x.Message); }
			}
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Started Executing Constructors");
			Task taskThreadSafe = Task.Run(() =>
			{
				Parallel.ForEach(threadSafeConstructors, x => x.ParseDefinition(release, mDictionary));
				Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Completed ThreadSafe");
			});
			
			foreach(ConstructorClass obj in firstPass)
			{
				try
				{
					parseDefinition(obj, release);
				}
				catch (Exception x) { mDatabase.logParseError("XXX Error in line #" + obj.Object.Index + " " + obj.Object.StepClassName + " " + x.Message); }
			}
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Completed FirstPass");
			firstPass.Clear();
			foreach (ConstructorClass obj in secondPass)
			{
				try
				{
					parseDefinition(obj, release);
				}
				catch (Exception x) { mDatabase.logParseError("XXX Error in line #" + obj.Object.Index + " " + obj.Object.StepClassName + " " + x.Message); }
			}
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Completed SecondPass");
			secondPass.Clear();
			taskThreadSafe.Wait();
			threadSafeConstructors.Clear();
			taskPrimitive.Wait();
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Finalizing");
			finalizeImport();
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Finished");
		}
	
		private void parseDefinition(ConstructorClass constructor, ReleaseVersion release)
		{
			constructor.ParseDefinition(release, mDictionary);
			BaseClassIfc obj = constructor.Object;
			IfcApplication application = obj as IfcApplication;
			if (application != null)
			{
				IfcApplication ea = mDatabase.Factory.mApplication;
				if (ea != null && ea.mVersion == application.mVersion)
				{
					if (string.Compare(ea.ApplicationFullName, application.ApplicationFullName, true) == 0)
					{
						if (string.Compare(ea.mApplicationIdentifier, application.mApplicationIdentifier) == 0)
						{
							mDatabase[ea.mIndex] = null;
							mDatabase.Factory.mApplication = application;
						}
					}
				}
			}

			IfcGeometricRepresentationContext geometricRepresentationContext = obj as IfcGeometricRepresentationContext;
			if (geometricRepresentationContext != null)
				mDatabase.Tolerance = geometricRepresentationContext.mPrecision;
			IfcSIUnit unit = obj as IfcSIUnit;
			if (unit != null)
			{
				if (unit.Name == IfcSIUnitName.METRE && unit.Prefix == IfcSIPrefix.NONE)
					mDatabase.Factory.mSILength = unit;
				else if (unit.Name == IfcSIUnitName.SQUARE_METRE && unit.Prefix == IfcSIPrefix.NONE)
					mDatabase.Factory.mSIArea = unit;
				else if (unit.Name == IfcSIUnitName.CUBIC_METRE && unit.Prefix == IfcSIPrefix.NONE)
					mDatabase.Factory.mSIVolume = unit;
			}
		}

		private void processDataLine(string line)
		{
			if (string.IsNullOrEmpty(line))
				return;
			int stepID = 0;
			string kw = "", def = "";
			ParserSTEP.GetKeyWord(line, out stepID, out kw, out def);
			if (!string.IsNullOrEmpty(kw) && kw[0] == 'I')
			{
				if (line.Last() != ';')
					mCancellationTokenSource.Cancel();
				try
				{
					BaseClassIfc obj = BaseClassIfc.Construct(kw);
					if (obj != null)
					{
						obj.mIndex = stepID;
						mDictionary[stepID] = obj;
						ConstructorClass constructorClass = new ConstructorClass(obj, def);
						//Todo add more primitive classes
						if (obj is IfcCartesianPoint || obj is IfcCartesianPointList || obj is IfcDirection ||
							obj is IfcIndexedPolygonalFace  || def.IndexOf('#') < 0)
							mPrimitiveConstructors.Add(constructorClass);
						else
							mConstructorsBag.Add(constructorClass);

						IfcRoot root = obj as IfcRoot;
						if(root != null)
						{
							int pos = 0;
							root.GlobalId = ParserSTEP.StripString(def, ref pos, def.Length);
						}
					}
				}
				catch (Exception) { }
			}
		}

		protected bool isDataLineOrProcessFileLine(string trimmedLine)
		{
			char c = char.ToUpper(trimmedLine[0]);
			if (c == 'F')
			{
				processFileHeaderLine(trimmedLine);
				return false;
			}
			if (c == 'E' || c == 'H')
				return false;
			return c == '#';
		}
		internal bool processFileHeaderLine(string line)
		{
			string ts = line.Replace(" ", "");
			if (ts.StartsWith("FILE_SCHEMA(('IFC2X4", true, CultureInfo.CurrentCulture) ||
					ts.StartsWith("FILE_SCHEMA(('IFC4", true, CultureInfo.CurrentCulture))
			{
				if (ts.StartsWith("FILE_SCHEMA(('IFC4X1", true, CultureInfo.CurrentCulture))
					mDatabase.Release = ReleaseVersion.IFC4X1;
				else if (ts.StartsWith("FILE_SCHEMA(('IFC4X2", true, CultureInfo.CurrentCulture))
					mDatabase.Release = ReleaseVersion.IFC4X2;
				else if (ts.StartsWith("FILE_SCHEMA(('IFC4X3_RC1", true, CultureInfo.CurrentCulture) ||
					ts.StartsWith("FILE_SCHEMA(('IFC4X3_RC1", true, CultureInfo.CurrentCulture))
					mDatabase.Release = ReleaseVersion.IFC4X3_RC1;
				else if (ts.StartsWith("FILE_SCHEMA(('IFC4X3_RC2", true, CultureInfo.CurrentCulture))
					mDatabase.Release = ReleaseVersion.IFC4X3_RC2;
				else if (ts.StartsWith("FILE_SCHEMA(('IFC4X3_RC3", true, CultureInfo.CurrentCulture))
					mDatabase.Release = ReleaseVersion.IFC4X3_RC3;
				else
					mDatabase.Release = ReleaseVersion.IFC4;
				if (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mDatabase.ModelView == ModelView.Ifc2x3NotAssigned)
					mDatabase.ModelView = ModelView.Ifc4NotAssigned;
				return true;
			}
			if (ts.StartsWith("FILE_DESCRIPTION", true, System.Globalization.CultureInfo.CurrentCulture))
			{
				return true;
			}
			if (ts.StartsWith("FILE_NAME", true, System.Globalization.CultureInfo.CurrentCulture))
			{
				if (ts.Length > 12)
				{
					List<string> fields = ParserSTEP.SplitLineFields(ts.Substring(10, ts.Length - 12));
					mDatabase.PreviousApplication = fields.Count > 6 ? fields[5].Replace("'", "") : "";
				}
				return true;
			}
			return false;
		}

		public bool WriteSTEP(TextWriter sw, string filename)
		{
			mDatabase.FileName = filename;
			mCachedCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			sw.Write(getHeaderString(filename) + "\r\n");
			foreach (BaseClassIfc e in mDatabase)
			{
				if (e != null)
				{
					try
					{
						string str = e.StringSTEP();
						if (!string.IsNullOrEmpty(str))
							sw.WriteLine(str);
					}
					catch (Exception) { }
				}
			}
			sw.Write(getFooterString());
			sw.Close();
			Thread.CurrentThread.CurrentUICulture = mCachedCulture;

			return true;
		}
		internal string getHeaderString(string fileName)
		{
			string hdr = "ISO-10303-21;\r\nHEADER;\r\nFILE_DESCRIPTION(('ViewDefinition [" + mDatabase.ModelView + "]'),'2;1');\r\n";

			hdr += "FILE_NAME(\r\n";
			hdr += "/* name */ '" + ParserIfc.Encode(fileName) + "',\r\n";
			DateTime now = DateTime.Now;
			hdr += "/* time_stamp */ '" + now.Year + "-" + (now.Month < 10 ? "0" : "") + now.Month + "-" + (now.Day < 10 ? "0" : "") + now.Day + "T" + (now.Hour < 10 ? "0" : "") + now.Hour + ":" + (now.Minute < 10 ? "0" : "") + now.Minute + ":" + (now.Second < 10 ? "0" : "") + now.Second + "',\r\n";
			IfcPerson person = mDatabase.Factory.mPerson;
			string authorName = person == null ? mDatabase.Factory.PersonName() : person.Name;
			hdr += "/* author */ ('" + authorName + "'),\r\n";
			string organizationName = IfcOrganization.Organization;
			IfcOrganization organization = null;
			if (organization != null)
				organizationName = organization.Name; 
			hdr += "/* organization */ ('" + organizationName + "'),\r\n";
			hdr += "/* preprocessor_version */ '" + mDatabase.Factory.ToolkitName + "',\r\n";
			hdr += "/* originating_system */ '" + mDatabase.Factory.ApplicationFullName + "',\r\n";
			hdr += "/* authorization */ 'None');\r\n\r\n";
			string version = "IFC4";
			ReleaseVersion release = mDatabase.Release;
			if (release == ReleaseVersion.IFC2x3)
				version = "IFC2X3";
			else if (release == ReleaseVersion.IFC4X1)
				version = "IFC4X1";
			else if (release == ReleaseVersion.IFC4X2)
				version = "IFC4X2";
			else if (release == ReleaseVersion.IFC4X3_RC1)
				version = "IFC4X3_RC1";
			else if (release == ReleaseVersion.IFC4X3_RC2)
				version = "IFC4X3_RC2";
			else if (release == ReleaseVersion.IFC4X3_RC3)
				version = "IFC4X3_RC3";
			hdr += "FILE_SCHEMA (('" + version + "'));\r\n";
			hdr += "ENDSEC;\r\n";
			hdr += "\r\nDATA;";
			return hdr;
		}
		internal string getFooterString() { return "ENDSEC;\r\n\r\nEND-ISO-10303-21;\r\n\r\n"; }


	}
}

 