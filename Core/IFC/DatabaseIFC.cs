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
using System.ComponentModel;
#if (!NOIFCZIP)
using System.IO.Compression;
#endif
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

using GeometryGym.STEP;
using System.Diagnostics;

#if (NET || !NOIFCJSON)
#if (NEWTONSOFT)
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonObject = Newtonsoft.Json.Linq.JObject;
using JsonArray = Newtonsoft.Json.Linq.JArray;
#else
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
#endif
#endif

namespace GeometryGym.Ifc
{
	public enum ReleaseVersion 
	{
		[Obsolete("Retired", false)] IFC2X, 
		[Obsolete("Retired", false)] IFC2x2, 
		IFC2x3, 
		[Obsolete("Retired", false)] IFC4, 
		[Obsolete("Withdrawn", false)] IFC4A1,  
		IFC4A2, 
		[Obsolete("Withdrawn", false)] IFC4X1, 
		[Obsolete("Withdrawn", false)] IFC4X2,
		[Obsolete("Obsolete", false)] IFC4X3_RC1,
		[Obsolete("Obsolete", false)] IFC4X3_RC2,
		[Obsolete("Obsolete", false)] IFC4X3_RC3,
		[Obsolete("Obsolete", false)] IFC4X3_RC4, 
		IFC4X3,
		IFC4X3_ADD2,
		/// <summary>
		/// Draft Release for IfcTunnel Deployment Testing, not to be used for Production
		/// </summary>
		[Description("Draft Release for IfcTunnel Deployment Testing, not to be used for Production")] IFC4X4_DRAFT

	}; // Alpha Releases IFC1.0, IFC1.5, IFC1.5.1, IFC2.0, 
	
	public enum ModelView 
	{ 
		Ifc4Reference,
		Ifc4DesignTransfer, 
		Ifc4NotAssigned, 
		Ifc2x3Coordination, 
		Ifc2x3NotAssigned,
		Ifc4X1NotAssigned,
		Ifc4X2NotAssigned,
		Ifc4X3NotAssigned,
		/// <summary>
		/// Draft Release for IfcTunnel Deployment Testing, not to be used for Production
		/// </summary>
		[Description("Draft Release for IfcTunnel Deployment Testing, not to be used for Production")]
		Ifc4X4NotAssigned
	};

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
#if (NET || !NOIFCJSON)
		JSON,
#endif
	};

	public delegate void SetProgressBarCallback(int percentDone);

	public partial class DatabaseIfc : DatabaseSTEP<BaseClassIfc>
	{
		internal string id { private set; get; }

		private bool mIsDisposed = false;
		internal bool IsDisposed() { return mIsDisposed; }
		public void Dispose() { mIsDisposed = true; }

		internal ReleaseVersion mRelease = ReleaseVersion.IFC2x3;
		public FormatIfcSerialization Format { get; set; }
		public string SourceFilePath { get; set; } = "";
		public string Authorization { get; set; } = "None";
		
		private FactoryIfc mFactory = null;
		public FactoryIfc Factory { get { return mFactory; } }

		public DatabaseIfc() : base() 
		{
			id = ParserIfc.EncodeGuid(Guid.NewGuid());
			mFactory = new FactoryIfc(this); Format = FormatIfcSerialization.STEP; 
		}
		public DatabaseIfc(string filePath) : this() 
		{
			SourceFilePath = filePath;
			new SerializationIfc(this).ReadFile(filePath);
		}
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
				BaseClassIfc existing = this[index];
				if (existing != null)
				{
					if (existing is IfcRoot existingRoot)
					{
						if(!string.IsNullOrEmpty(existingRoot.mGlobalId))
							mDictionary.TryRemove(existingRoot.GlobalId, out BaseClassIfc obj);
					}
					else if (existing is IfcClassificationReference existingClassificationReference)
						mClassificationReferences.Remove(existingClassificationReference);
					else if (existing is IfcClassification existingClassification)
						mClassifications.Add(existingClassification);
				}
				if (value is IfcRoot root)
				{
					if (!string.IsNullOrEmpty(root.mGlobalId))
					{
						mDictionary[root.GlobalId] = value;
					}
				}
				else if (value is IfcClassificationReference classificationReference)
					mClassificationReferences.Add(classificationReference);
				else if (value is IfcClassification classification)
					mClassifications.Add(classification);

			
				base[index] = value;
				if (value != null)
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
	
		internal static ReleaseVersion versionFromModelView(ModelView modelView)
		{
			if (modelView == ModelView.Ifc2x3Coordination || modelView == ModelView.Ifc2x3NotAssigned)
				return ReleaseVersion.IFC2x3;
			if (modelView == ModelView.Ifc4X1NotAssigned)
				return ReleaseVersion.IFC4X1;
			if (modelView == ModelView.Ifc4X2NotAssigned)
				return ReleaseVersion.IFC4X2;
			if (modelView == ModelView.Ifc4X3NotAssigned)
				return ReleaseVersion.IFC4X3_ADD2;
			if ((modelView == ModelView.Ifc4X4NotAssigned))
				return ReleaseVersion.IFC4X4_DRAFT;
			return ReleaseVersion.IFC4A2;
		}

		internal override void ProcessFileDescription()
		{
			base.ProcessFileDescription();
			foreach (string description in OriginatingFileInformation.FileDescriptions)
			{
				string modelView = description.ToLower();
				if (modelView.Contains("coordination"))
					ModelView = ModelView.Ifc2x3Coordination;
				else if (modelView.Contains("referenceview"))
					ModelView = ModelView.Ifc4Reference;
				else if (modelView.Contains("designtransfer"))
					ModelView = ModelView.Ifc4DesignTransfer;
			}
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
								mModelSIScale = namedUnit.SIFactor();
						}
					}
					if (double.IsNaN(mModelSIScale))
						return 1;
				}
				return mModelSIScale;
			}
			internal set { mModelSIScale = value; }
		}
		public double ScaleAngle()
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
		
		private IfcContext mContext = null;
		internal void SetContext(IfcContext context)
		{
			mContext = context;
		}
		
		internal ConcurrentDictionary<string, BaseClassIfc> mDictionary = new ConcurrentDictionary<string, BaseClassIfc>();
		internal HashSet<IfcClassificationReference> mClassificationReferences = new HashSet<IfcClassificationReference>();
		internal HashSet<IfcClassification> mClassifications = new HashSet<IfcClassification>();
		
		private string viewDefinition { get { return (mModelView == ModelView.Ifc2x3Coordination ? "CoordinationView_V2" : (mModelView == ModelView.Ifc4Reference ? "ReferenceView_V1" : (mModelView == ModelView.Ifc4DesignTransfer ? "DesignTransferView_V1" : "notYetAssigned"))); } }
		
		public static DatabaseIfc ParseString(string str)
		{
			if (string.IsNullOrEmpty(str))
				return null;

			DatabaseIfc db = new DatabaseIfc(ModelView.Ifc4NotAssigned);
			char startingChar = str[0];
			switch (startingChar)
			{
#if (!NOIFCJSON)
				case '{':
					{
						JsonObject jobj = JsonObject.Parse(str) as JsonObject;
						if (str != null)
							db.ReadJSON(jobj);
						break;
					}
#endif
				case '<':
					{
						System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
						doc.LoadXml(str);
						db.ReadXMLDoc(doc);
						break;
					}
				default:
					{
						new SerializationIfcSTEP(db).importLines(str.Split('\n'));
						break;
					}
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

		private string setFileInformation(string filePath)
		{
			FolderPath = Path.GetDirectoryName(filePath);
			string fileName = Path.GetFileNameWithoutExtension(filePath);
			char[] chars = Path.GetInvalidFileNameChars();
			foreach (char c in chars)
				fileName = fileName.Replace(c, '_');
			return FileName = Path.Combine(FolderPath, fileName + Path.GetExtension(filePath));
		}
		public bool WriteSTEPFile(string filePath, SetProgressBarCallback setProgressBarCallback)
		{
			string path = setFileInformation(filePath);
			bool result = new SerializationIfcSTEP(this).WriteSTEPFile(path, setProgressBarCallback);
			if (!result)
			{
				try
				{
					File.Delete(path);
				}
				catch { }
			}
			return result;
		}
		public bool WriteSTEPFile(string filePath, BackgroundWorker worker, DoWorkEventArgs e)
		{
			string path = setFileInformation(filePath);
			bool result = new SerializationIfcSTEP(this).WriteSTEPFile(path, worker, e);
			if (!result)
			{
				try
				{
					File.Delete(path);
				}
				catch { }
			}
			return result;
		}
#if(!NOIFCZIP)
		public bool WriteSTEPZipFile(string filePath, SetProgressBarCallback setProgressBarCallback)
		{
			string path = setFileInformation(filePath);
			ZipArchive za = null;
			if (File.Exists(path))
				File.Delete(path);
			za = ZipFile.Open(path, System.IO.Compression.ZipArchiveMode.Create);
			ZipArchiveEntry zae = za.CreateEntry(Path.GetFileNameWithoutExtension(path) + ".ifc");
			StreamWriter sw = new StreamWriter(zae.Open());
			bool result = new SerializationIfcSTEP(this).WriteSTEP(sw, path, setProgressBarCallback);
			sw.Close();

			za.Dispose();
			return true;
		}
#endif
		public bool WriteFile(string filePath)
		{
			StreamWriter sw = null;

			string path = setFileInformation(filePath);
		
			if (ExtensionHelper.ExtensionEquals(filePath, ".xml"))
			{
				WriteXmlFile(path);
				return true;
			}
			if (ExtensionHelper.ExtensionEquals(filePath, ".ifcxml"))
			{
				WriteXmlFile(path);
				return true;
			}
#if (NET || !NOIFCJSON)
			else if (ExtensionHelper.ExtensionEquals(filePath, ".json") || ExtensionHelper.ExtensionEquals(filePath, ".ifcjson"))
			{
				ToJSON(path);
				return true;
			}

#endif
			Encoding encoding = SerializationIfcSTEP.StepFileEncoding();
#if (!NOIFCZIP)
			bool zip = ExtensionHelper.ExtensionEquals(filePath, ".ifczip");
			ZipArchive za = null;
			if (zip)
			{
				if (File.Exists(path))
					File.Delete(path);
				za = ZipFile.Open(path, System.IO.Compression.ZipArchiveMode.Create);
				ZipArchiveEntry zae = za.CreateEntry(Path.GetFileNameWithoutExtension(path) + ".ifc");
				sw = new StreamWriter(zae.Open(), encoding);
			}
			else
#endif
				sw = new StreamWriter(FileName, false, encoding);
			bool result = new SerializationIfcSTEP(this).WriteSTEP(sw, path);
			sw.Close();
			
#if (!NOIFCZIP)
			if (zip)
				za.Dispose();
#endif
			if (!result)
			{
				try
				{
					File.Delete(path);
				}
				catch { }
			}
			return result;
		}
		public bool WriteStream(Stream stream, string filename)
		{
			if (ExtensionHelper.ExtensionEquals(filename, ".xml"))
			{
				return WriteXml(new XmlTextWriter(stream, Encoding.UTF8));
			}
#if (!NOIFCJSON)
			else if (ExtensionHelper.ExtensionEquals(filename, ".json"))
			{
				return this.ToJSON(filename) != null;
			}
#endif
#if (!NOIFCZIP)
			else if (ExtensionHelper.ExtensionEquals(filename, ".ifczip"))
			{
				var za = new System.IO.Compression.ZipArchive(stream, System.IO.Compression.ZipArchiveMode.Create, true);
				var zae = za.CreateEntry(Path.GetFileNameWithoutExtension(filename) + ".ifc");
				var sw = new StreamWriter(zae.Open());
				bool result = new SerializationIfcSTEP(this).WriteSTEP(sw, filename);
				sw.Close();
				za.Dispose();
				return result;
			}
#endif
			else
			{
				var sw = new StreamWriter(stream);
				bool result = new SerializationIfcSTEP(this).WriteSTEP(sw, filename);
				sw.Flush();
				return result;
			}
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
			{
				stringWriter.Close();
				return stringWriter.ToString();
			}
			return null;
		}
	}


	public class DuplicateMapping
	{
		private Dictionary<string, int> mDictionary = new Dictionary<string, int>();
		internal int FindExisting(BaseClassIfc obj)
		{
			if (obj == null || obj.mDatabase == null || string.IsNullOrEmpty(obj.mGlobalId))
				return 0;
			string keyValue = key(obj);
			mDictionary.TryGetValue(keyValue, out int result);
			return result;
		}
		internal void AddObject(BaseClassIfc obj, int index) 
		{
			if (string.IsNullOrEmpty(obj.mGlobalId))
				obj.setGlobalId(ParserIfc.EncodeGuid(Guid.NewGuid()));
			string objectKey = key(obj);
			mDictionary[objectKey] = index; 
		}

		private string key(BaseClassIfc obj) 
		{
			string key = (obj.mDatabase == null ? "" : obj.mDatabase.id + "|") + obj.mGlobalId;
			return key;
		}
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

		public IfcElement ConstructElement(string className, IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation)
		{
			return ConstructElement(className, host, placement, representation, null);
		}
		public IfcElement ConstructElement(string className, IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system)
		{
			IfcElement element = ConstructProduct(className, host, placement, representation, system) as IfcElement;
			if (element == null)
				element = new IfcBuildingElementProxy(host, placement, representation);
			return element;
		}
		public IfcProduct ConstructProduct(string className, IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) 
		{
			return ConstructProduct(className, host, placement, representation, null);
		}
		
		internal IfcProduct ConstructProduct(string className, IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system)
		{
			string str = className, predefinedTypeConstant = "";
			ReleaseVersion release = mDatabase.Release;
			if (string.IsNullOrEmpty(str))
				return null;
			str = ParserIfc.IdentifyIfcClass(str, out predefinedTypeConstant);
			Type type = BaseClassIfc.GetType(str);
			if (type == null)
				throw new Exception("XXX Unrecognized Ifc Type for " + className);
			
			if (release < ReleaseVersion.IFC4X4_DRAFT)
			{
				if (string.Compare(str, "IfcTunnel", true) == 0)
					type = typeof(IfcFacility);
				else if (string.Compare(str, "IfcTunnelPart", true) == 0)
					type = typeof(IfcFacilityPartCommon);
				else if (string.Compare(str, "IfcArchElement", true) == 0)
					type = typeof(IfcMember);
				else if (string.Compare(str, "IfcGroundReinforcementElement", true) == 0)
					type = typeof(IfcMember);
				else if (string.Compare(str, "IfcUndergroundExcavation", true) == 0)
					type = typeof(IfcEarthworksCut);
				if (release < ReleaseVersion.IFC4X3_RC1)
				{
					if (string.Compare(str, "IfcBuiltElement", true) == 0)
					{
						type = typeof(IfcBuildingElementProxy);
						str = "IfcBuildingElementProxy";
					}
					else if (release < ReleaseVersion.IFC4X2)
					{
						if (typeof(IfcFacility).IsAssignableFrom(type) && !typeof(IfcBuilding).IsAssignableFrom(type))
						{
							if (release < ReleaseVersion.IFC4X1)
								type = host is IfcBuilding ? typeof(IfcBuilding) : typeof(IfcSite);
						}
						else if (typeof(IfcFacilityPart).IsAssignableFrom(type))
							type = typeof(IfcSpace);
						else if (release > ReleaseVersion.IFC4X1)
						{
							if (typeof(IfcFacilityPart) == type)
								type = typeof(IfcFacilityPartCommon);
						}
					}
				}
			}
			VersionAddedAttribute versionAdded = type.GetCustomAttribute(typeof(VersionAddedAttribute)) as VersionAddedAttribute;
			if (versionAdded != null && versionAdded.Release > release && type.IsSubclassOf(typeof(IfcElement)))
				type = typeof(IfcBuildingElementProxy);
			if (type.IsAbstract && typeof(IfcElement).IsAssignableFrom(type))
			{
				type = typeof(IfcBuildingElementProxy);
			}
			IfcProduct product = construct(type, host, placement, representation, system);
			if (product == null)
				throw new Exception("XXX Unrecognized Ifc Constructor for " + className);
			string resultClass = product.StepClassName;
			if (string.Compare(str, resultClass, true) != 0 && string.Compare(resultClass, "IfcFacilityPart",true) != 0)
				product.ObjectType = className;
			else if (!string.IsNullOrEmpty(predefinedTypeConstant))
			{
				product.SetPredefinedType(predefinedTypeConstant);
			}
			return product;
		}
		internal IfcConstructionResource ConstructResource(string className)
		{
			string str = className, predefinedTypeConstant = "";
			ReleaseVersion release = mDatabase.Release;
			if (string.IsNullOrEmpty(str))
				return null;
			str = ParserIfc.IdentifyIfcClass(str, out predefinedTypeConstant);
			Type type = BaseClassIfc.GetType(str);
			if (type == null)
				throw new Exception("XXX Unrecognized Ifc Type for " + className);
			IfcConstructionResource resource = BaseClassIfc.Construct(str) as IfcConstructionResource;
			mDatabase.appendObject(resource);
			if (resource == null)
				throw new Exception("XXX Unrecognized Ifc Constructor for " + className);
			if (!string.IsNullOrEmpty(predefinedTypeConstant))
			{
				resource.SetPredefinedType(predefinedTypeConstant);
			}
			return resource;
		}
		private IfcProduct construct(Type type, IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, IfcDistributionSystem system)
		{
			if (host == null)
			{
				IfcProduct product = null;
				ConstructorInfo constructor = getConstructor(type, new[] { typeof(DatabaseIfc) });
				if(constructor != null)
					product = constructor.Invoke(new object[] { mDatabase }) as IfcProduct;
				else
				{ 
					constructor = getConstructor(type, new Type[] { });
					if (constructor == null)
						return null;
					product = constructor.Invoke(new object[] { }) as IfcProduct;
					mDatabase.appendObject(product);
				}

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
		internal Dictionary<string, IfcPropertySet> mPropertySets = new Dictionary<string, IfcPropertySet>();
		public T Duplicate<T>(T entity) where T : class, IBaseClassIfc
		{
			return Duplicate<T>(entity, new DuplicateOptions(mDatabase.Tolerance) { DuplicateDownstream = true }); 
		}
		public void NominateDuplicate(IBaseClassIfc entity, IBaseClassIfc existingDuplicate) 
		{
			mDuplicateMapping.AddObject(entity as BaseClassIfc, existingDuplicate.StepId);
		}
		public IfcAxis2Placement3D DuplicateAxis(IfcAxis2Placement3D placement, DuplicateOptions options)
		{
			if (placement == null)
				return null;
			int stepId = mDuplicateMapping.FindExisting(placement);
			if (stepId > 0)
				return mDatabase[stepId] as IfcAxis2Placement3D;
			if (placement.IsXYPlane(mDatabase.Tolerance))
				return XYPlanePlacement;
			return Duplicate<IfcAxis2Placement3D>(placement, options);
		}
		public IfcDirection DuplicateDirection(IfcDirection direction, DuplicateOptions options)
		{
			if (direction == null)
				return null;
			int stepId = mDuplicateMapping.FindExisting(direction);
			if (stepId > 0)
				return mDatabase[stepId] as IfcDirection;
			if (direction.DirectionRatios.Count == 2)
			{
				IfcDirection result = Direction(direction.DirectionRatios[0], direction.DirectionRatios[1]);
				mDuplicateMapping.AddObject(direction, result.StepId);
				return result;
			}
			else if (direction.DirectionRatios.Count == 3)
			{
				IfcDirection result = Direction(direction.DirectionRatios[0], direction.DirectionRatios[1], direction.DirectionRatios[2]);
				mDuplicateMapping.AddObject(direction, result.StepId);
				return result;
			}
			return Duplicate(direction, options);
		}
		public T Duplicate<T>(T entity, DuplicateOptions options) where T : class, IBaseClassIfc
		{
			if (entity == null)
				return null;

			BaseClassIfc obj = entity as BaseClassIfc;
			if (obj.Database != null && obj.Database.id == mDatabase.id)
				return entity;

			if(entity is IfcProduct product)
			{
				if (options.mProductsToExclude.Contains(product.GlobalId))
					return null;
			}
			
			T result = null;
			int index = mDuplicateMapping.FindExisting(obj);
			if (index > 0)
			{
				result = mDatabase[index] as T;
				if (result != null)
					return result;
			}

			result = obj.DuplicateCommon(mDatabase, options) as T;
			if (result != null)
				return result;

			if(entity is IfcClassification classification)
				result = findExistingClassification(classification) as T;
			else if(entity is IfcClassificationReference classificationReference)
				result = findExistingClassificationReference(classificationReference) as T;
			else if (entity is IfcRelAssociatesClassification associatesClassification)
			{
				if(associatesClassification.RelatingClassification is IfcClassification ifcClassification)
				{
					IfcClassification existing = findExistingClassification(ifcClassification);
					if(existing != null)
					{
						result = existing.HasReferences.OfType<T>().FirstOrDefault();
						if (result != null)
							return result;
					}
				}
				else if (associatesClassification.RelatingClassification is IfcClassificationReference ifcClassificationReference)
				{
					IfcClassificationReference existing = findExistingClassificationReference(ifcClassificationReference);
					if(existing != null)
					{
						result = existing.HasReferences.OfType<T>().FirstOrDefault();
						if (result != null)
							return result;
					}
				}
			}
			else if (entity is IfcRelAssignsToGroup assignsToGroup && assignsToGroup as IfcRelAssignsToGroupByFactor == null)
			{
				IfcGroup group = assignsToGroup.RelatingGroup;
				if(group != null)
				{
					IfcGroup existing = mDatabase[group.GlobalId] as IfcGroup;
					if(existing != null)
					{
						result = existing.IsGroupedBy.OfType<T>().FirstOrDefault();
						if (result != null)
							return result;
					}
				}
			}
		
			if (result != null)
				return result;
		
			if (!string.IsNullOrEmpty(obj.mGlobalId))
			{
				if (mDatabase.mDictionary.TryGetValue(obj.mGlobalId, out BaseClassIfc existing))
				{
					if (string.Compare(existing.StepClassName, entity.StepClassName, true) == 0)
					{
						result = existing as T;
						if (result != null)
						{
							mDuplicateMapping.AddObject(obj, result.StepId);
							return result;
						}
					}
				}
			}
			result = duplicateWorker(obj, options) as T;
			if (result == null)
				return null;

			NominateDuplicate(entity, result);
			return result;
		}
		private IfcClassification findExistingClassification(IfcClassification classification)
		{
			foreach (IfcClassification c in mDatabase.mClassifications)
			{
				if (c.isDuplicate(classification, 1e-5))
					return c;
			}
			return null;
		}
		private IfcClassificationReference findExistingClassificationReference(IfcClassificationReference classificationReference)
		{
			foreach(IfcClassificationReference r in mDatabase.mClassificationReferences)
			{
				if (r.isDuplicate(classificationReference, 1e-5))
					return r;
			}
			return null;
		}
		private T findExisting<T>(T obj) where T : BaseClassIfc
		{
			foreach (T existing in mDatabase.OfType<T>())
			{
				if (existing.isDuplicate(obj, 1e-5))
				{
					NominateDuplicate(obj, existing);
					return existing;
				}
			}
			return null;
		}
		internal BaseClassIfc duplicateWorker(BaseClassIfc entity, DuplicateOptions options)
		{
			BaseClassIfc result = null;
			if (entity.mDatabase == null || mDatabase.Release != entity.mDatabase.Release)
			{
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
			}
			string typeName = entity.GetType().Name;
			Type type = Type.GetType("GeometryGym.Ifc." + typeName, false, true);
			if (type == null)
			{
				typeName = entity.StepClassName;
				type = Type.GetType("GeometryGym.Ifc." + typeName, false, true);
				if (type == null)
					throw new Exception("Unrecongnized ifc type " + typeName);
			}
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
		public T DuplicateProperty<T>(T property) where T : IfcProperty { return duplicateProperty(property, new DuplicateOptions(mDatabase.Tolerance)) as T; }
		public T DuplicateProperty<T>(T property, DuplicateOptions options) where T : IfcProperty { return duplicateProperty(property, options) as T; }
		public IfcProperty DuplicateProperty(IfcProperty property) { return duplicateProperty(property, new DuplicateOptions(mDatabase.Tolerance)); }
		public IfcProperty DuplicateProperty(IfcProperty property, DuplicateOptions options) { return duplicateProperty(property, options); }
		private IfcProperty duplicateProperty(IfcProperty property, DuplicateOptions options)
		{
			int index = mDuplicateMapping.FindExisting(property);
			if (index > 0)
				return mDatabase[index] as IfcProperty;
			IfcPropertySingleValue psv = property as IfcPropertySingleValue;
			IfcValue val = null;
			if(psv != null && psv.Unit == null)
			{
				val = psv.NominalValue;
				if (val is IfcLengthMeasure lengthMeasure)
					psv.NominalValue = new IfcLengthMeasure(Math.Round(lengthMeasure.Measure, mDatabase.mLengthDigits));
				else if (val is IfcPositiveLengthMeasure positiveLengthMeasure)
					psv.NominalValue = new IfcPositiveLengthMeasure(Math.Round(positiveLengthMeasure.Measure, mDatabase.mLengthDigits));
				else if (val is IfcNonNegativeLengthMeasure nonNegativeLengthMeasure)
					psv.NominalValue = new IfcNonNegativeLengthMeasure(Math.Round(nonNegativeLengthMeasure.Measure, mDatabase.mLengthDigits));
				else
					psv = null;
			}
			string stepString = dictionaryKey(property);
			IfcProperty result = null;
			if(!string.IsNullOrEmpty(stepString) && mProperties.TryGetValue(stepString, out result))
			{
				mDuplicateMapping.AddObject(property, result.StepId);
				if (psv != null)
					psv.NominalValue = val; 
				return result;
			}
			result = Duplicate(property, new DuplicateOptions(options) { DuplicateDownstream = true });
			if (psv != null && val != null)
				psv.NominalValue = val; 
			mProperties[stepString] = result;
			return result;
		}
		internal IfcPropertySet DuplicatePropertySet(IfcPropertySet propertySet, DuplicateOptions options)
		{
			if (propertySet.Database != null && propertySet.Database.id == mDatabase.id)
			{
				if(options.CommonPropertySets)
					return propertySet;
			}
			if (options.CommonPropertySets)
			{
				int index = mDuplicateMapping.FindExisting(propertySet);
				if (index > 0)
					return mDatabase[index] as IfcPropertySet;
			}
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
			if (!options.CommonPropertySets && propertySet.Database != null && propertySet.Database.id == mDatabase.id)
				return new IfcPropertySet(mDatabase, propertySet, options);

			List<IfcProperty> properties = new List<IfcProperty>();
			foreach (IfcProperty property in propertySet.HasProperties.Values)
			{
				if (!options.IgnoredPropertyNames.Contains(property.Name))
					properties.Add(duplicateProperty(property, options));

			}
			if (options.CommonPropertySets)
			{
				
				string key = propertySet.Name + "," + propertySet.Description + "," + string.Join("|", properties.Select(x => x.StepId).OrderBy(x => x));
				IfcPropertySet existing = null;
				if (mPropertySets.TryGetValue(key, out existing))
					return existing;

				IfcPropertySet result = new IfcPropertySet(mDatabase, propertySet, properties, options);
				mDuplicateMapping.AddObject(propertySet, result.StepId);
				mPropertySets[key] = result;
				return result;
			}

			IfcPropertySet duplicate = new IfcPropertySet(mDatabase, propertySet, properties, options);
			mDuplicateMapping.AddObject(propertySet, duplicate.StepId);
			return duplicate;
			
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

		private Dictionary<IfcConversionBasedUnit.CommonUnitName, IfcConversionBasedUnit> mDictionaryConversionBasedUnits = new Dictionary<IfcConversionBasedUnit.CommonUnitName, IfcConversionBasedUnit>();
		private Dictionary<string, IfcSIUnit> mDictionarySIUnits = new Dictionary<string, IfcSIUnit>();
		private Dictionary<IfcUnitAssignment.Length, IfcUnit> mDictionaryLengthUnits = new Dictionary<IfcUnitAssignment.Length, IfcUnit>();

		public IfcSIUnit SIUnit(IfcUnitEnum unitEnum, IfcSIPrefix prefix, IfcSIUnitName name)
		{
			string key = unitEnum.ToString() + prefix.ToString() + name.ToString();
			if (mDictionarySIUnits.TryGetValue(key, out IfcSIUnit existing))
				return existing;
			IfcSIUnit unit = new IfcSIUnit(mDatabase, unitEnum, prefix, name);
			mDictionarySIUnits[key] = unit;
			return unit;
		}
		public IfcConversionBasedUnit ConversionUnit(IfcConversionBasedUnit.CommonUnitName name)
		{
			if (mDictionaryConversionBasedUnits.TryGetValue(name, out IfcConversionBasedUnit existing))
				return existing;
			IfcUnitAssignment assignment = (mDatabase.Context == null ? null : mDatabase.Context.UnitsInContext);
			string nameString = name.ToString().Replace("_", " ");
			IfcNamedUnit unit = assignment == null ? null : assignment[IfcUnitEnum.LENGTHUNIT];
			if (unit == null)
				unit = SILength;
			if (name == IfcConversionBasedUnit.CommonUnitName.inch)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.LENGTHUNIT, nameString, new IfcMeasureWithUnit(new IfcLengthMeasure(unit.SIFactor() * 0.0254), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.foot)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.LENGTHUNIT, nameString, new IfcMeasureWithUnit(new IfcLengthMeasure(unit.SIFactor() * IfcUnitAssignment.FeetToMetre), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.yard)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.LENGTHUNIT, nameString, new IfcMeasureWithUnit(new IfcLengthMeasure(unit.SIFactor() * 0.914), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.mile)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.LENGTHUNIT, nameString, new IfcMeasureWithUnit(new IfcLengthMeasure(unit.SIFactor() * 1609), unit));
			}

			unit = assignment == null ? null: assignment[IfcUnitEnum.AREAUNIT];
			if (unit == null)
				unit = SIArea;
			if (name == IfcConversionBasedUnit.CommonUnitName.square_inch)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.AREAUNIT, nameString, new IfcMeasureWithUnit(new IfcAreaMeasure(unit.SIFactor() * 0.0006452), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.square_foot)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.AREAUNIT, nameString, new IfcMeasureWithUnit(new IfcAreaMeasure(unit.SIFactor() * 0.09290), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.square_yard)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.AREAUNIT, nameString, new IfcMeasureWithUnit(new IfcAreaMeasure(unit.SIFactor() * 0.83612736), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.acre)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.AREAUNIT, nameString, new IfcMeasureWithUnit(new IfcAreaMeasure(unit.SIFactor() * 4046.86), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.square_mile)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.AREAUNIT, nameString, new IfcMeasureWithUnit(new IfcAreaMeasure(unit.SIFactor() * 2588881), unit));
			}

			unit = assignment == null ? null : assignment[IfcUnitEnum.VOLUMEUNIT];
			if (unit == null)
				unit = SIVolume;
			if (name == IfcConversionBasedUnit.CommonUnitName.cubic_inch)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor() * 0.00001639), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.cubic_foot)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor() * 0.02832), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.cubic_yard)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor() * 0.7636), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.litre)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor() * 0.001), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.fluid_ounce_UK)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor() * 0.0000284130625), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.fluid_ounce_US)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor() * 0.00002957353), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.pint_UK)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor() * 0.000568), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.pint_US)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor() * 0.000473), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.gallon_UK)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor() * 0.004546), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.gallon_UK)
			{
				IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
				if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
					return conversionBasedUnit;
				return new IfcConversionBasedUnit(IfcUnitEnum.VOLUMEUNIT, nameString, new IfcMeasureWithUnit(new IfcVolumeMeasure(unit.SIFactor() * 0.003785), unit));
			}

			unit = assignment == null ? null :assignment[IfcUnitEnum.PLANEANGLEUNIT];
			if (name == IfcConversionBasedUnit.CommonUnitName.degree)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.PLANEANGLEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.RADIAN);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.PLANEANGLEUNIT, nameString, new IfcMeasureWithUnit(new IfcPlaneAngleMeasure(unit.SIFactor() * Math.PI / 180.0), unit));
			}

			unit = assignment == null ? null : assignment[IfcUnitEnum.MASSUNIT];
			if (name == IfcConversionBasedUnit.CommonUnitName.ounce)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.MASSUNIT, IfcSIPrefix.KILO, IfcSIUnitName.GRAM);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.MASSUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor() * 0.02835), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.pound)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.MASSUNIT, IfcSIPrefix.KILO, IfcSIUnitName.GRAM);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.MASSUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor() * 0.453592), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.ton_UK)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.MASSUNIT, IfcSIPrefix.KILO, IfcSIUnitName.GRAM);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.MASSUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor() * 1016.0469088), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.ton_US)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.MASSUNIT, IfcSIPrefix.NONE, IfcSIUnitName.GRAM);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.MASSUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor() * 907.18474), unit));
			}

			unit = assignment == null ? null : assignment[IfcUnitEnum.FORCEUNIT];
			if (name == IfcConversionBasedUnit.CommonUnitName.lbf)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.FORCEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.NEWTON);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.FORCEUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor() * 4.4482216153), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.kip)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.FORCEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.NEWTON);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.FORCEUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor() * 4448.2216153), unit));
			}

			unit = assignment == null ? null : assignment[IfcUnitEnum.PRESSUREUNIT];
			if (name == IfcConversionBasedUnit.CommonUnitName.kip)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.PRESSUREUNIT, IfcSIPrefix.NONE, IfcSIUnitName.PASCAL);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.FORCEUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor() * 6894.7572932), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.ksi)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.PRESSUREUNIT, IfcSIPrefix.NONE, IfcSIUnitName.PASCAL);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.FORCEUNIT, nameString, new IfcMeasureWithUnit(new IfcMassMeasure(unit.SIFactor() * 6894757.2932), unit));
			}
			unit = assignment == null ? null : assignment[IfcUnitEnum.TIMEUNIT];
			if (name == IfcConversionBasedUnit.CommonUnitName.minute)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.TIMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SECOND);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.TIMEUNIT, nameString, new IfcMeasureWithUnit(new IfcTimeMeasure(unit.SIFactor() * 60), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.hour)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.TIMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SECOND);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.TIMEUNIT, nameString, new IfcMeasureWithUnit(new IfcTimeMeasure(unit.SIFactor() * 3600), unit));
			}
			if (name == IfcConversionBasedUnit.CommonUnitName.day)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.TIMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SECOND);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.TIMEUNIT, nameString, new IfcMeasureWithUnit(new IfcTimeMeasure(unit.SIFactor() * 86400), unit));
			}

			unit = assignment == null ? null : assignment[IfcUnitEnum.ENERGYUNIT];
			if (name == IfcConversionBasedUnit.CommonUnitName.btu)
			{
				if (unit == null)
					unit = new IfcSIUnit(mDatabase, IfcUnitEnum.ENERGYUNIT, IfcSIPrefix.NONE, IfcSIUnitName.JOULE);
				else
				{
					IfcConversionBasedUnit conversionBasedUnit = unit as IfcConversionBasedUnit;
					if (conversionBasedUnit != null && string.Compare(conversionBasedUnit.Name, nameString, true) == 0)
						return conversionBasedUnit;
				}
				return new IfcConversionBasedUnit(IfcUnitEnum.ENERGYUNIT, nameString, new IfcMeasureWithUnit(new IfcTimeMeasure(unit.SIFactor() * 1055.056), unit));
			}
			return null;
		}
		public IfcUnit LengthUnit(IfcUnitAssignment.Length length)
		{
			if (mDictionaryLengthUnits.TryGetValue(length, out IfcUnit existing))
				return existing;
			IfcUnit unit = null;
			if (length == IfcUnitAssignment.Length.Millimetre)
				unit = new IfcSIUnit(mDatabase, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.MILLI, IfcSIUnitName.METRE);
			else if (length == IfcUnitAssignment.Length.Centimetre)
				unit = new IfcSIUnit(mDatabase, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.CENTI, IfcSIUnitName.METRE);
			else if (length == IfcUnitAssignment.Length.Inch)
				unit = ConversionUnit(IfcConversionBasedUnit.CommonUnitName.inch);
			else if (length == IfcUnitAssignment.Length.Foot)
				unit = ConversionUnit(IfcConversionBasedUnit.CommonUnitName.foot);
			else 
				unit = SILength;
			mDictionaryLengthUnits[length] = unit;
			return unit;
		}
		internal IfcSIUnit mSILength, mSIArea, mSIVolume;
		public IfcSIUnit SILength { get { if(mSILength == null) mSILength = new IfcSIUnit(mDatabase, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.NONE, IfcSIUnitName.METRE); return mSILength; } }
		public IfcSIUnit SIArea { get { if(mSIArea == null) mSIArea = new IfcSIUnit(mDatabase, IfcUnitEnum.AREAUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SQUARE_METRE); return mSIArea; } }
		public IfcSIUnit SIVolume { get { if(mSIVolume == null) mSIVolume = new IfcSIUnit(mDatabase, IfcUnitEnum.VOLUMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.CUBIC_METRE); return mSIVolume; } }

		internal IfcDirection mXAxis, mYAxis, mZAxis, mNegXAxis, mNegYAxis, mNegZAxis;
		internal IfcDirection mXAxis2d, mYAxis2d, mNegXAxis2d, mNegYAxis2d;
		internal IfcLine mLineX2d = null;

		public IfcDirection Direction(double x, double y, double z)
		{
			if (double.IsNaN(z))
				return new IfcDirection(mDatabase, x, y);
			double length = Math.Sqrt(x * x + y * y + z * z), tol = 1e-8;
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

		public IfcDirection Direction(double x, double y)
		{
			double length = Math.Sqrt(x * x + y * y), tol = 1e-6;
			double dx = x / length, dy = y / length;
			if (Math.Abs(dx - 1) < tol)
				return XAxis2d;
			if (Math.Abs(dy - 1) < tol)
				return YAxis2d;
			if (Math.Abs(dx + 1) < tol)
				return XAxisNegative2d;
			if (Math.Abs(dy + 1) < tol)
				return YAxisNegative2d;
			return new IfcDirection(mDatabase, x, y);
		}
		public IfcDirection XAxis2d { get { if (mXAxis2d == null) mXAxis2d = new IfcDirection(mDatabase, 1, 0); return mXAxis2d; } }
		public IfcDirection YAxis2d { get { if (mYAxis2d == null) mYAxis2d = new IfcDirection(mDatabase, 0, 1); return mYAxis2d; } }
		public IfcDirection XAxisNegative2d { get { if (mNegXAxis2d == null) mNegXAxis2d = new IfcDirection(mDatabase, -1, 0); return mNegXAxis2d; } }
		public IfcDirection YAxisNegative2d { get { if (mNegYAxis2d == null) mNegYAxis2d = new IfcDirection(mDatabase, 0, -1); return mNegYAxis2d; } }
		public IfcLine LineX2d { get { if (mLineX2d == null) mLineX2d = new IfcLine(Origin2d, new IfcVector(new IfcDirection(mDatabase, 1, 0), 1));  return mLineX2d; } }

		partial void getApplicationFullName(ref string app);
		partial void getApplicationIdentifier(ref string app);
		partial void getApplicationDeveloper(ref string app);

		internal string mApplicationFullName = "", mApplicationIdentifier = "", mApplicationDeveloper = "", mApplicationVersion = "";
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
		public string ApplicationVersion
		{
			get
			{
				if (string.IsNullOrEmpty(mApplicationVersion))
				{
					return ToolkitName;
				}
				return mApplicationVersion;
			}
			set { mApplicationVersion = value; }
		}

		internal IfcApplication mApplication = null;
		public IfcApplication Application
		{
			get
			{
				if (mApplication == null)
					mApplication = IfcApplication.Construct(mDatabase);
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
		internal IfcPersonAndOrganization mPersonOrganization = null;
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
		internal string PersonIdentification() { return System.Environment.UserName; }
		internal IfcPerson mPerson = null;
		public IfcPerson Person
		{
			get
			{
				if (mPerson == null)
				{
					mPerson = new IfcPerson(mDatabase);
					if (mDatabase.Release > ReleaseVersion.IFC2x3)
						mPerson.Identification = PersonIdentification();
					else
						mPerson.FamilyName = PersonIdentification();
#if (IFCMODEL && !IFCIMPORTONLY && (RHINO || GH))
					string str = ggAssembly.mOptions.OwnerRole;
					if (!string.IsNullOrEmpty(str))
					{
						IfcRoleEnum role = IfcRoleEnum.NOTDEFINED;
						if (Enum.TryParse<IfcRoleEnum>(str, out role))
						{
							if (role != IfcRoleEnum.NOTDEFINED)
								mPerson.Roles.Add(new IfcActorRole(mDatabase) { Role = role });
						}
						else
							mPerson.Roles.Add(new IfcActorRole(mDatabase) { Role = IfcRoleEnum.USERDEFINED, UserDefinedRole = str });
					}
#endif
				}
				return mPerson;
			}
			set { mPerson = value; }
		}
		internal IfcOrganization mOrganization = null;
		public IfcOrganization Organization
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
		internal IfcOwnerHistory mOwnerHistoryAdded = null;
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
			DuplicateOptions options = new DuplicateOptions(mDatabase.Tolerance);
			if (ownerHistory == null && original != null)
				return null;
			else if (ownerHistory != null && ownerHistory.mDatabase != mDatabase)
				ownerHistory = Duplicate(ownerHistory, options);
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
				else if (ownerHistory != null)
				{
					if (revised.OwningUser.isDuplicate(ownerHistory.OwningUser))
						modifier = Duplicate(ownerHistory.OwningUser, options);
					else
						modifier = Duplicate(revised.OwningUser, options);
				}
				else
					modifier = Duplicate(revised.OwningUser, options);
			}
			IfcApplication application = null;
			if (revised == null)
				application = (action == IfcChangeActionEnum.ADDED ? null : Application);
			else if (ownerHistory != null)
			{
				if (revised.OwningApplication.isDuplicate(ownerHistory.OwningApplication))
					application = Duplicate(ownerHistory.OwningApplication, options);
				else
					application = Duplicate(revised.OwningApplication, options);
			}
			else
				application = Duplicate(revised.OwningApplication, options);
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
			IfcContext context = mDatabase.Context;
			if(context != null)
			{
				result = context.RepresentationContexts.OfType<IfcGeometricRepresentationContext>().Where(x => string.Compare(x.ContextType, type, true) == 0).FirstOrDefault();
				if (result != null)
					return result;
			}
			
			if(nature == IfcGeometricRepresentationContext.GeometricContextIdentifier.Plan)
			{
				dimension = 2;
			}

			result = new IfcGeometricRepresentationContext(mDatabase, dimension, mDatabase.Tolerance);
			result.ContextType = type;
			result.ContextIdentifier = dimension + "D";
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
			else if (nature == IfcGeometricRepresentationSubContext.SubContextIdentifier.Box)
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
			else if (nature == IfcGeometricRepresentationSubContext.SubContextIdentifier.Body_Fallback)
			{
				identifier = "Body-Fallback";
			}
			if (context == null)
				context = GeometricRepresentationContext(IfcGeometricRepresentationContext.GeometricContextIdentifier.Model);
			result = new IfcGeometricRepresentationSubContext(context, projection ) { ContextIdentifier = identifier };
			mSubContexts.Add(nature, result);
			return result;
		}
		internal Dictionary<IfcGeometricRepresentationSubContext.SubContextIdentifier, IfcGeometricRepresentationSubContext> mSubContexts = new Dictionary<IfcGeometricRepresentationSubContext.SubContextIdentifier, IfcGeometricRepresentationSubContext>();		
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

	public partial class DuplicateOptions
	{
		public bool DuplicateHost { get; set; } 
		public bool DuplicateDownstream { get; set; }
		public bool DuplicateProperties { get; set; } = true;
		public bool CommonPropertySets { get; set; } = true;
		public bool CommonGeometry { get; set; } = false;
		public bool DuplicateAssociations { get; set; } = true;
		public bool DuplicateRepresentation { get; set; } = true;
		public bool DuplicateAxisRepresentations { get; set; } = true;
		public bool DuplicatePresentationStyling { get; set; } = true;
		public double DeviationTolerance { get; set; }
		public IfcOwnerHistory OwnerHistory { get; set; }
		public bool DuplicateOwnerHistory { get; set; } = true;

		public HashSet<string> IgnoredPropertyNames = new HashSet<string>();
		internal DuplicateCommonDictionaries mCommonObjects = new DuplicateCommonDictionaries();
		internal HashSet<IfcSpatialElement> mSpatialElementsToDuplicate = new HashSet<IfcSpatialElement>();
		internal HashSet<string> mProductsToExclude = new HashSet<string>();

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
			DuplicateProperties = options.DuplicateProperties;
			CommonPropertySets = options.CommonPropertySets;
			CommonGeometry = options.CommonGeometry;
			DuplicateAssociations = options.DuplicateAssociations;
			DuplicateRepresentation = options.DuplicateRepresentation;
			DuplicateAxisRepresentations = options.DuplicateAxisRepresentations;
			DuplicatePresentationStyling = options.DuplicatePresentationStyling;
			DeviationTolerance = options.DeviationTolerance;
			OwnerHistory = options.OwnerHistory;
			DuplicateOwnerHistory = options.DuplicateOwnerHistory;

			foreach(string propertyName in options.IgnoredPropertyNames)
				IgnoredPropertyNames.Add(propertyName);

			mCommonObjects = options.mCommonObjects;
			mSpatialElementsToDuplicate = options.mSpatialElementsToDuplicate;
			mProductsToExclude = options.mProductsToExclude;
		}
	}
	internal class DuplicateCommonDictionaries
	{
		internal Dictionary<string, IfcCartesianPoint> mPoints = new Dictionary<string, IfcCartesianPoint>();
		internal Dictionary<string, IfcVector> mVectors = new Dictionary<string, IfcVector>();

		internal Dictionary<string, IfcLine> mLines = new Dictionary<string, IfcLine>();
		internal Dictionary<string, IfcCircle> mCircles = new Dictionary<string, IfcCircle>();
		internal Dictionary<string, IfcPolyline> mPolylines = new Dictionary<string, IfcPolyline>();
		internal Dictionary<string, IfcCompositeCurve> mCompositeCurves = new Dictionary<string, IfcCompositeCurve>();
		internal Dictionary<string, IfcCompositeCurveSegment> mCompositeCurveSegments = new Dictionary<string, IfcCompositeCurveSegment>();
		internal Dictionary<string, IfcTrimmedCurve> mTrimmedCurves = new Dictionary<string, IfcTrimmedCurve>();

		internal Dictionary<string, IfcPolyLoop> mLoops = new Dictionary<string, IfcPolyLoop>();
		internal Dictionary<string, IfcFaceBound> mFaceBounds = new Dictionary<string, IfcFaceBound>();
		internal Dictionary<string, IfcFace> mFaces = new Dictionary<string, IfcFace>();
		internal Dictionary<string, IfcConnectedFaceSet> mFaceSets = new Dictionary<string, IfcConnectedFaceSet>();

		internal Dictionary<string, IfcAxis2Placement3D> mPlacements3D = new Dictionary<string, IfcAxis2Placement3D>();
		internal Dictionary<string, IfcAxis2Placement2D> mPlacements2D = new Dictionary<string, IfcAxis2Placement2D>();

		internal Dictionary<string, IfcMaterialLayer> mMaterialLayers = new Dictionary<string, IfcMaterialLayer>();
		internal Dictionary<string, IfcMaterialLayerSet> mMaterialLayerSets = new Dictionary<string, IfcMaterialLayerSet>();
		internal Dictionary<string, IfcMaterialLayerSetUsage> mMaterialLayerSetUsages = new Dictionary<string, IfcMaterialLayerSetUsage>();

		internal Dictionary<string, IfcProfileDef> mProfiles = new Dictionary<string, IfcProfileDef>();

		internal Dictionary<string, IfcColourRgb> mColours = new Dictionary<string, IfcColourRgb>(); 
		internal Dictionary<string, IfcPresentationStyle> mPresentationStyles = new Dictionary<string, IfcPresentationStyle>(); 
		internal Dictionary<string, IfcPresentationStyleAssignment> mPresentationStyleAssignments = new Dictionary<string, IfcPresentationStyleAssignment>(); 
		internal Dictionary<string, IfcSurfaceStyleRendering> mSurfaceStyleRenderings = new Dictionary<string, IfcSurfaceStyleRendering>(); 
		internal Dictionary<string, IfcSurfaceStyleShading> mSurfaceStyleShadings = new Dictionary<string, IfcSurfaceStyleShading>(); 
		internal DuplicateCommonDictionaries() { }
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
				string[] pair = str.Trim().Split("/.".ToCharArray());
				string typename = (pair == null ? str.Trim() : pair[0]), predefined = pair == null || pair.Length < 2 ? "" : pair[1];
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
		public List<string> ApplicableEntities()
		{
			return mApplicableTypes.ConvertAll(x => x.ApplicableTypeString());
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

		internal string ApplicableTypeString()
		{
			return mType.Name + (string.IsNullOrEmpty(mPredefined) ? "" : "\\" + mPredefined);
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
			if (ExtensionHelper.ExtensionEquals(fileName, ".xml") || ExtensionHelper.ExtensionEquals(fileName, ".ifcxml"))
				return FormatIfcSerialization.XML;
#if (!NOIFCJSON)
			if (ExtensionHelper.ExtensionEquals(fileName, ".json") || ExtensionHelper.ExtensionEquals(fileName, "ifcjson"))
				return FormatIfcSerialization.JSON;
#endif
			return FormatIfcSerialization.STEP;
		}
		internal FileStreamIfc getStreamReader(string fileName)
		{
			mDatabase.FileName = fileName;
			mDatabase.FolderPath = Path.GetDirectoryName(fileName);
			
			StreamReader streamReader = null;
#if (!NOIFCZIP)
			if (ExtensionHelper.ExtensionEquals(fileName, ".zip") || ExtensionHelper.ExtensionEquals(fileName, ".ifczip"))
			{
				ZipArchive za = ZipFile.OpenRead(fileName);
				if (za.Entries.Count != 1)
				{
					return null;
				}
				string filename = za.Entries[0].Name.ToLower();
				FormatIfcSerialization fformat = detectFormat(filename);
				if (fformat == FormatIfcSerialization.STEP)
				{
					Encoding encoding = SerializationIfcSTEP.StepFileEncoding();
					streamReader = new StreamReader(za.Entries[0].Open(), encoding);
				}
				else
					streamReader = new StreamReader(za.Entries[0].Open());
				return new FileStreamIfc(fformat, streamReader);
			}
#endif
			FormatIfcSerialization format = detectFormat(fileName);
			if (format == FormatIfcSerialization.STEP)
			{
				Encoding encoding = SerializationIfcSTEP.StepFileEncoding();
				streamReader = new StreamReader(fileName, encoding);
			}
			else
				streamReader = new StreamReader(fileName);
			return new FileStreamIfc(format, streamReader);
		}

		internal void ReadFile(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
				return;

			mDatabase.FolderPath = Path.GetDirectoryName(filePath);
			if (ExtensionHelper.ExtensionEquals(filePath, ".ifc"))
				new SerializationIfcSTEP(mDatabase).ReadStepFile(filePath);
			else
			{
				using (FileStreamIfc fileStream = getStreamReader(filePath))
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
#if (NET || !NOIFCJSON)
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

		internal static Encoding StepFileEncoding()
		{
			return Encoding.ASCII;
		}
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

			public override string ToString()
			{
				return Object.StepClassName + " " + DefinitionString;
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

			var now = DateTime.Now;
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Started Reading File " + stepFilePath);

			using (StreamReader reader = new StreamReader(stepFilePath))
			{
				string line = "";
				while ((line = reader.ReadLine()) != null)
				{
					string str = line.Trim();
					if (string.IsNullOrEmpty(str))
						continue;
					if (str.ToUpper().StartsWith("DATA"))
						break;
					char c = str.Last();
					while (c != ';')
					{
						line = reader.ReadLine();
						if (line == null)
							break;
						str += line.Trim();
						c = str.Last();
					}
					processFileHeaderLine(str);
				}
			}
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Started Reading Data");

			ParallelOptions parallelOptions = new ParallelOptions();
			parallelOptions.CancellationToken = mCancellationTokenSource.Token;
			parallelOptions.MaxDegreeOfParallelism = System.Environment.ProcessorCount;

			bool aborted = false;
			try
			{
				Parallel.ForEach(File.ReadLines(stepFilePath), parallelOptions, x =>
				{
					processDataLine(x);
					parallelOptions.CancellationToken.ThrowIfCancellationRequested();
				});
			}
			catch (OperationCanceledException)
			{
				aborted = true;
			}
			if (aborted)
			{
				new SerializationIfcSTEP(mDatabase).importLines(File.ReadAllLines(stepFilePath));
				return;
			}
			var timespan = DateTime.Now.Subtract(now);
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - File Read duration " + timespan.TotalSeconds);

			processObjects();
			timespan = DateTime.Now.Subtract(now);
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Read duration " + timespan.TotalSeconds);
		}
		public void ReadStepStream(TextReader stream)
		{
			initializeImport();

			DateTime startTime = DateTime.Now;
			string line = "";
			Debug.WriteLine(startTime.ToString("HH:mm:ss:ffff") + " - Started Reading File");
			while ((line = stream.ReadLine()) != null)
			{
				if (string.IsNullOrEmpty(line))
					continue;
				string str = line.Trim();
				if (str.StartsWith("/*"))
					str = str.Substring(str.IndexOf("*/") + 2);
				if (string.IsNullOrEmpty(str))
					continue;
				char c = str.Last();
				while(c != ';')
				{
					line = stream.ReadLine();
					if (line == null)
						break;
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
				if (str.StartsWith("/*"))
					str = str.Substring(str.IndexOf("*/") + 2);
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
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Read Completed " + (DateTime.Now - startTime).ToString());
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
					if (o is IfcPropertySet || o is IfcMaterialConstituentSet || o is IfcPropertySetTemplate)
						secondPass.Add(obj);
					else if (o is IfcTessellatedFaceSet || o is IfcPolyLoop || o is IfcFacetedBrep)
						threadSafeConstructors.Add(obj);
					else
						firstPass.Add(obj);
				}
				catch (Exception x) { mDatabase.logParseError("XXX Error in line #" + obj.Object.StepId + " " + obj.Object.StepClassName + " " + x.Message); }
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
				catch (Exception x) { mDatabase.logParseError("XXX Error in line #" + obj.Object.StepId + " " + obj.Object.StepClassName + " " + x.Message); }
			}
			Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss:ffff") + " - Completed FirstPass");
			firstPass.Clear();
			foreach (ConstructorClass obj in secondPass)
			{
				try
				{
					parseDefinition(obj, release);
				}
				catch (Exception x) { mDatabase.logParseError("XXX Error in line #" + obj.Object.StepId + " " + obj.Object.StepClassName + " " + x.Message); }
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
							mDatabase[ea.StepId] = null;
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
						obj.mStepId = stepID;
						mDictionary[stepID] = obj;
						ConstructorClass constructorClass = new ConstructorClass(obj, def);
						if(obj is IfcTessellatedFaceSet || obj is IfcRelationship)
							mConstructorsBag.Add(constructorClass);
						else if (obj is IfcCartesianPoint || obj is IfcCartesianPointList || obj is IfcDirection ||
							obj is IfcIndexedPolygonalFace  || def.IndexOf('#') < 0)
							mPrimitiveConstructors.Add(constructorClass);
						else
							mConstructorsBag.Add(constructorClass);

						int pos = 0;
						if(obj is IfcRoot root)
							root.GlobalId = ParserSTEP.StripString(def, ref pos, def.Length);
						else if(obj is IfcProperty property)
							property.Name = ParserSTEP.Decode(ParserSTEP.StripString(def, ref pos, def.Length));
						else if(obj is IfcPropertyTemplate propertyTemplate)
							propertyTemplate.Name = ParserSTEP.Decode(ParserSTEP.StripString(def, ref pos, def.Length));
						else if(obj is IfcPhysicalQuantity quantity)
							quantity.Name = ParserSTEP.Decode(ParserSTEP.StripString(def, ref pos, def.Length));
						else if(obj is IfcMaterialConstituent materialConstituent)
							materialConstituent.Name = ParserSTEP.Decode(ParserSTEP.StripString(def, ref pos, def.Length));
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
			string trimmedLine = ParserSTEP.StripComments(line).Trim();
			ParserSTEP.GetKeyWord(trimmedLine, out int stepID, out string keyword, out string def);
			int pos = 0, len = def.Length;
			if (string.Compare(keyword, "FILE_SCHEMA", true) == 0)
			{
				string field = ParserSTEP.StripField(def, ref pos, len);
				List<string> fields = ParserSTEP.SplitListStrings(field);
				if (fields.Count > 0)
				{
					string schemaId = fields[0].Replace("'", "").Trim();
					if (Enum.TryParse<ReleaseVersion>(schemaId, true, out ReleaseVersion release))
						mDatabase.Release = release;
					else
					{
						if (schemaId.StartsWith("IFC2X4", true, CultureInfo.CurrentCulture))
							mDatabase.Release = ReleaseVersion.IFC4;
						else if (schemaId.StartsWith("IFC4X4", true, CultureInfo.CurrentCulture))
							mDatabase.Release = ReleaseVersion.IFC4X4_DRAFT;
						else
						{
							List<string> schemas = Enum.GetNames(typeof(ReleaseVersion)).Reverse().ToList();
							foreach (string schema in schemas)
							{
								if (schemaId.StartsWith(schema))
								{
									Enum.TryParse<ReleaseVersion>(schema, out release);
									mDatabase.Release = release;
									break;
								}
							}
						}
					}
				}

				if (mDatabase.Release > ReleaseVersion.IFC2x3)
				{
					if (mDatabase.Release == ReleaseVersion.IFC4X1)
						mDatabase.ModelView = ModelView.Ifc4X1NotAssigned;
					else if (mDatabase.Release == ReleaseVersion.IFC4X2)
						mDatabase.ModelView = ModelView.Ifc4X2NotAssigned;
					else if (mDatabase.Release > ReleaseVersion.IFC4X2)
					{
						mDatabase.ModelView = ModelView.Ifc4X3NotAssigned;
					}
					else if (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mDatabase.ModelView == ModelView.Ifc2x3NotAssigned)
						mDatabase.ModelView = ModelView.Ifc4NotAssigned;
				}
				return true;
			}
			
			return mDatabase.processFileHeaderLine(line);
		}


		public bool WriteSTEPFile(string filePath, SetProgressBarCallback progressBarCallback)
		{
			mDatabase.FileName = filePath;
			Encoding encoding = StepFileEncoding();
			StreamWriter sw = new StreamWriter(filePath, false, encoding);
			mCachedCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			sw.Write(getHeaderString(filePath) + "\r\n");
			int count = mDatabase.Count(), counter = 0, progress = 0;
			ReleaseVersion release = mDatabase.Release;
			progressBarCallback(0);

			foreach (BaseClassIfc c in mDatabase)
			{
				try
				{
					c.WriteStepLine(sw, release);
					int calculatedProgress = 100 * counter / count;
					if (calculatedProgress > progress)
					{
						progressBarCallback(++progress);
					}
				}
				catch (Exception) { }
				counter++;
			}
			sw.Write(getFooterString());
			Thread.CurrentThread.CurrentUICulture = mCachedCulture;
			progressBarCallback(100);
			sw.Close();
			return true;
		}
		public bool WriteSTEPFile(string filePath, BackgroundWorker worker, DoWorkEventArgs e)
		{
			mDatabase.FileName = filePath;
			Encoding encoding = StepFileEncoding();
			StreamWriter sw = new StreamWriter(filePath, false, encoding);
			mCachedCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			sw.Write(getHeaderString(filePath) + "\r\n");
			int count = mDatabase.Count(),counter = 0, progress = 0;
			ReleaseVersion release = mDatabase.Release;
			foreach (BaseClassIfc c in mDatabase)
			{
				if (c != null)
				{
					try
					{
						c.WriteStepLine(sw, release);
						int calculatedProgress = 100 * counter / count;
						if (calculatedProgress > progress)
						{
							worker.ReportProgress(++progress);
						}
					}
					catch (Exception) { }
				}
				if(worker.CancellationPending)
				{
					return false;
				}
				counter++;
			}
			sw.Write(getFooterString());
			Thread.CurrentThread.CurrentUICulture = mCachedCulture;
			worker.ReportProgress(100);
			sw.Close();
			return true;
		}
		public bool WriteSTEP(TextWriter textWriter, string filename, SetProgressBarCallback progressBarCallback)
		{
			mDatabase.FileName = filename;
			mCachedCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			textWriter.Write(getHeaderString(filename) + "\r\n");
			ReleaseVersion release = mDatabase.Release;
			int count = mDatabase.Count(), counter = 0, progress = 0;
			progressBarCallback(0);
			foreach (BaseClassIfc e in mDatabase)
			{
				if (e != null)
					e.WriteStepLine(textWriter, release);
				int calculatedProgress = 100 * counter / count;
				if (calculatedProgress > progress)
				{
					progressBarCallback(++progress);
				}
			}
			textWriter.Write(getFooterString());
			Thread.CurrentThread.CurrentUICulture = mCachedCulture;
			progressBarCallback(100);
			return true;
		}
		public bool WriteSTEP(TextWriter textWriter, string filename)
		{
			mDatabase.FileName = filename;
			mCachedCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			textWriter.Write(getHeaderString(filename) + "\r\n");
			ReleaseVersion release = mDatabase.Release;
			foreach (BaseClassIfc e in mDatabase)
			{
				if (e != null)
				{
					e.WriteStepLine(textWriter, release);
				}
			}
			textWriter.Write(getFooterString());
			Thread.CurrentThread.CurrentUICulture = mCachedCulture;
			return true;
		}
		public List<string> GetStepLines()
		{
			mCachedCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			List<string> result = getHeaderLines("");
			foreach (BaseClassIfc e in mDatabase)
			{
				if (e != null)
				{
					try
					{
						string str = e.StringSTEP();
						if (!string.IsNullOrEmpty(str))
							result.Add(str);
					}
					catch (Exception) { }
				}
			}
			result.AddRange(getFooterLines());
			Thread.CurrentThread.CurrentUICulture = mCachedCulture;
			return result;
		}
		internal string getHeaderString(string fileName)
		{
			return string.Join("\r\n", getHeaderLines(fileName));
		}
		internal List<string> getHeaderLines(string fileName)
		{
			List<string> lines = new List<string>();

			string modelView = mDatabase.ModelView.ToString();
			if (mDatabase.ModelView == ModelView.Ifc2x3Coordination)
				modelView = "CoordinationView_V2.0";
			else if (mDatabase.ModelView == ModelView.Ifc4Reference)
				modelView = "ReferenceView_V1.2";
			else if (mDatabase.ModelView == ModelView.Ifc4DesignTransfer)
				modelView = "DesignTransferView_V1.1";
			lines.Add("ISO-10303-21;\r\nHEADER;\r\nFILE_DESCRIPTION(('ViewDefinition [" + modelView + "]'),'2;1');");
			
			lines.Add("FILE_NAME(");
			lines.Add("/* name */ '" + ParserSTEP.Encode(fileName) + "',");
			DateTime now = DateTime.Now;
			lines.Add("/* time_stamp */ '" + now.Year + "-" + (now.Month < 10 ? "0" : "") + now.Month + "-" + (now.Day < 10 ? "0" : "") + now.Day + "T" + (now.Hour < 10 ? "0" : "") + now.Hour + ":" + (now.Minute < 10 ? "0" : "") + now.Minute + ":" + (now.Second < 10 ? "0" : "") + now.Second + "',");
			string authorName = "", organizationName = "", authorization = "None", originatingSystem = "", preprocessorVersion = "";
			var fileInformation = mDatabase.OriginatingFileInformation;
			if (fileInformation != null)
			{
				authorName = fileInformation.Author.FirstOrDefault();
				organizationName = fileInformation.Organization.FirstOrDefault();
				authorization = fileInformation.Authorization;
				originatingSystem = fileInformation.OriginatingSystem;
				preprocessorVersion = fileInformation.PreProcessorVersion;
			}
			if (!string.IsNullOrEmpty(mDatabase.Authorization))
				authorization = mDatabase.Authorization;
			if (string.IsNullOrEmpty(authorName))
			{
				IfcPerson person = mDatabase.Factory.mPerson;
				authorName = person == null ? mDatabase.Factory.PersonIdentification() : person.Name;
			}
			if(string.IsNullOrEmpty(organizationName))
			{
				organizationName = IfcOrganization.Organization;
				IfcOrganization organization = mDatabase.Factory.Organization;
				if (organization != null)
					organizationName = organization.Name; 
			}
			if(string.IsNullOrEmpty(originatingSystem))
			{
				originatingSystem = mDatabase.Factory.ApplicationFullName;
			}
			if(string.IsNullOrEmpty(preprocessorVersion))
			{
				preprocessorVersion = mDatabase.Factory.ApplicationVersion;
			}
			lines.Add("/* author */ ('" + ParserSTEP.Encode(authorName) + "'),");
			lines.Add("/* organization */ ('" + ParserSTEP.Encode(organizationName) + "'),");
			lines.Add("/* preprocessor_version */ '" + ParserSTEP.Encode(preprocessorVersion) + "',");
			lines.Add("/* originating_system */ '" + ParserSTEP.Encode(originatingSystem) + "',");

			lines.Add("/* authorization */ '" + ParserSTEP.Encode(authorization) + "'");
			if(mDatabase.Comments.Count > 0)
			{
				foreach (string comment in mDatabase.Comments)
				{
					if(!string.IsNullOrEmpty(comment))
						lines.Add("/* " + ParserSTEP.Encode(comment) + "*/");
				}
			}
			lines.Add(");");
			lines.Add("");
			ReleaseVersion release = mDatabase.Release;
			string version = release.ToString().ToUpper();
			if (release == ReleaseVersion.IFC4A1 || release == ReleaseVersion.IFC4A2)
				version = "IFC4";
			else if (release == ReleaseVersion.IFC4X4_DRAFT)
				version = "IFC4X4_B072BCCA";
			lines.Add("FILE_SCHEMA (('" + version + "'));");
			lines.Add("ENDSEC;");
			lines.Add("");
			lines.Add("DATA;");
			return lines;
		}
		internal string getFooterString() { return string.Join("\r\n", getFooterLines()); }
		internal List<string> getFooterLines() { return new List<string>() { "ENDSEC;", "", "END-ISO-10303-21;", "" }; }
	}

	public static class ExtensionHelper
	{
		public static bool ExtensionEquals(string fileName, string extension)
		{
			return fileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
		}
	}
}
