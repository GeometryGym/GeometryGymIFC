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
	public enum ReleaseVersion {  IFC2x3, IFC4, IFC4A1, IFC4A2 };
	public enum ModelView { Ifc4Reference, Ifc4DesignTransfer, Ifc4NotAssigned,Ifc2x3Coordination, If2x3NotAssigned };
	public partial class DatabaseIfc
	{
		internal Guid id = Guid.NewGuid();
		private int mNextBlank = 1;
		internal int NextBlank
		{
			get
			{
				if (this[mNextBlank] == null)
					return mNextBlank;
				for(int icounter = mNextBlank; icounter < RecordCount; icounter++)
				{
					if(this[icounter] == null)
					{
						mNextBlank = icounter;
						return mNextBlank;
					}
				}
				mNextBlank = RecordCount;
				return mNextBlank;
			}
		}
		internal ReleaseVersion mRelease = ReleaseVersion.IFC2x3;

		private FactoryIfc mFactory = null;
		public FactoryIfc Factory { get { return mFactory; } }

		internal DatabaseIfc() : base() { mFactory = new FactoryIfc(this); }
		public DatabaseIfc(string fileName) : this() { ReadFile(fileName); }
		public DatabaseIfc(TextReader stream) : this() { ReadFile(stream, 0); }
		public DatabaseIfc(ModelView view) : this(true, view) { }
		public DatabaseIfc(DatabaseIfc db) : this() { mRelease = db.mRelease; mModelView = db.mModelView; Tolerance = db.Tolerance; }
		public DatabaseIfc(bool generate, ModelView view) : this(generate, view == ModelView.Ifc2x3Coordination || view == ModelView.If2x3NotAssigned ? ReleaseVersion.IFC2x3 : ReleaseVersion.IFC4A1, view) { }
		public DatabaseIfc(bool generate, ReleaseVersion schema) : this(generate, schema, schema == ReleaseVersion.IFC2x3 ? ModelView.If2x3NotAssigned : ModelView.Ifc4NotAssigned) { }
		private DatabaseIfc(bool generate, ReleaseVersion schema, ModelView view) : this()
		{
			mRelease = schema;
			mModelView = view;
#if (RHINO)
			mModelSIScale = 1 / GGYM.Units.mLengthConversion[(int) GGYM.GGYMRhino.GGRhino.ActiveUnits()];
			Tolerance = Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
#endif
			mFactory.mGeomRepContxt = new IfcGeometricRepresentationContext(this, 3, Tolerance) { ContextType = "Model" };
			if (generate)
				mFactory.initData();
		}

		internal ModelView mModelView = ModelView.If2x3NotAssigned;
		internal bool mAccuratePreview = false; 
		public string FolderPath { get; set; } = "";
		public string FileName { get; set; } = "";
		internal double mPlaneAngleToRadians = 1;
		internal bool mTimeInDays = false;
		public int NextObjectRecord { set { mNextBlank = value; } }
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
		public double Tolerance
		{
			get { return mModelTolerance; }
			set
			{
				if (!double.IsNaN(value))
				{
					mModelTolerance = Math.Min(0.0005 / mModelSIScale, value);
					mLengthDigits = Math.Max(2, -1 * (int)(Math.Log10(mModelTolerance) - 1));
				}
			}
		}
		public double ScaleSI
		{
			get { return mModelSIScale; }
			set { mModelSIScale = value; }
		}
		private double mModelTolerance = 0.0001,mModelSIScale = 1;
		internal int mLengthDigits = 5;
		public IfcContext Context { get { return mContext; } }
		public IfcProject Project { get { return mContext as IfcProject; } }
		private List<BaseClassIfc> mIfcObjects = new List<BaseClassIfc>() { new BaseClassIfc() }; 

		public int RecordCount { get { return mIfcObjects.Count; } }
		public BaseClassIfc this[int index]
		{
			get { return (index < mIfcObjects.Count ? mIfcObjects[index] : null); }
			set
			{
				if(value == null)
				{
					if (mIfcObjects.Count > index)
						mIfcObjects[index] = null;
					if (index < mNextBlank)
						mNextBlank = index;
					return;
				}
				if (mIfcObjects.Count <= index)
				{
					for (int ncounter = mIfcObjects.Count; ncounter <= index; ncounter++)
						mIfcObjects.Add(null);
				}
				mIfcObjects[index] = value;
				if (index == mNextBlank)
					mNextBlank = mNextBlank + 1;	
				value.mDatabase = this;
				value.mIndex = index;
			}
		}
		internal void appendObject(BaseClassIfc o) { this[NextBlank] = o; }	
		internal IfcContext mContext = null;
		
		internal HashSet<string> mGlobalIDs = new HashSet<string>();

		partial void printError(string str);
		internal void logError(string str) { printError(str); }
		
		private string viewDefinition { get { return (mModelView == ModelView.Ifc2x3Coordination ? "CoordinationView_V2" : (mModelView == ModelView.Ifc4Reference ? "ReferenceView_V1" : (mModelView == ModelView.Ifc4DesignTransfer ? "DesignTransferView_V1" : "notYetAssigned"))); } }
		internal string getHeaderString(string fileName)
		{
			string hdr = "ISO-10303-21;\r\nHEADER;\r\nFILE_DESCRIPTION(('ViewDefinition [" + viewDefinition + "]'),'2;1');\r\n";

			hdr += "FILE_NAME(\r\n";
			hdr += "/* name */ '" + ParserIfc.Encode(fileName.Replace("\\", "\\\\")) + "',\r\n";
			DateTime now = DateTime.Now;
			hdr += "/* time_stamp */ '" + now.Year + "-" + (now.Month < 10 ? "0" : "") + now.Month + "-" + (now.Day < 10 ? "0" : "") + now.Day + "T" + (now.Hour < 10 ? "0" : "") + now.Hour + ":" + (now.Minute < 10 ? "0" : "") + now.Minute + ":" + (now.Second < 10 ? "0" : "") + now.Second + "',\r\n";
			hdr += "/* author */ ('" + System.Environment.UserName + "'),\r\n";
			hdr += "/* organization */ ('" + IfcOrganization.Organization + "'),\r\n";
			hdr += "/* preprocessor_version */ 'GeomGymIFC by Geometry Gym Pty Ltd',\r\n";
			hdr += "/* originating_system */ '" + mFactory.ApplicationFullName + "',\r\n";

			hdr += "/* authorization */ 'None');\r\n\r\n";
			hdr += "FILE_SCHEMA (('" + (mRelease == ReleaseVersion.IFC2x3 ? "IFC2X3" : "IFC4") + "'));\r\n";
			hdr += "ENDSEC;\r\n";
			hdr += "\r\nDATA;";
			return hdr;
		}
		internal string getFooterString() { return "ENDSEC;\r\n\r\nEND-ISO-10303-21;\r\n\r\n"; } 
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
	
		internal enum FormatIfc { JSON, STEP, XML }
		internal class FileStreamIfc
		{
			internal FormatIfc mFormat;
			internal StreamReader mStreamReader;
			internal FileStreamIfc(FormatIfc format, StreamReader sr)
			{
				mFormat = format;
				mStreamReader = sr;
			} 
		}
		internal FileStreamIfc getStreamReader(string fileName)
		{
			string ext = Path.GetExtension(fileName);
			FileName = fileName;
			FolderPath = Path.GetDirectoryName(fileName);
			FormatIfc format = fileName.EndsWith("xml") ? FormatIfc.XML : FormatIfc.STEP;
			if (fileName.EndsWith("json"))
				format = FormatIfc.JSON;
#if (!NOIFCZIP)
			if (fileName.ToLower().EndsWith("zip"))
			{
				System.IO.Compression.ZipArchive za = System.IO.Compression.ZipFile.OpenRead(fileName);
				if (za.Entries.Count != 1)
				{
					return null;
				}
				string filename = za.Entries[0].Name.ToLower();
				format = filename.EndsWith("xml") ? FormatIfc.XML : FormatIfc.STEP;
				if (filename.EndsWith("json"))
					format = FormatIfc.JSON;
				StreamReader str = (format == FormatIfc.STEP ? new StreamReader(za.Entries[0].Open(), System.Text.Encoding.GetEncoding("windows-1252")) :
					new StreamReader(za.Entries[0].Open()));
				return new FileStreamIfc(format,str);
			}
#endif
			StreamReader sr = format == FormatIfc.STEP ? new StreamReader(fileName, System.Text.Encoding.GetEncoding("windows-1252")) :
				new StreamReader(fileName);
			return new FileStreamIfc(format, sr);
		}

		internal void ReadFile(string filename) { ReadFile(getStreamReader(filename)); }
		internal void ReadFile(FileStreamIfc fs)
		{
			if (fs == null)
				return;
			switch (fs.mFormat)
			{
				case FormatIfc.XML:
					ReadXMLStream(fs.mStreamReader);
					break;
				case FormatIfc.JSON:
#if (IFCJSON)
					ReadJSONFile(fs.mStreamReader);
					break;
#else
					logError("IfcJSON not enabled!");
					return;
#endif
				default:
					ReadFile(fs.mStreamReader, 0);
					break;
			}
			if (mContext != null)
			{
				mContext.initializeUnitsAndScales();

				if (mContext.mRepresentationContexts.Count > 0)
					mFactory.mGeomRepContxt = mIfcObjects[mContext.mRepresentationContexts[0]] as IfcGeometricRepresentationContext;
				
			}
		}
		private IfcContext ReadFile(TextReader sr, int offset)
		{
			mRelease = ReleaseVersion.IFC2x3;
			bool ownerHistory = mFactory.Options.GenerateOwnerHistory;
			mFactory.Options.GenerateOwnerHistory = false;
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
						InterpretLine(strLine);
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
						InterpretLine(strLine);
					}
					catch (Exception ex) { logError("XXX Error in line " + strLine + " " + ex.Message); }
					strLine = sr.ReadLine();
				}
			}
			sr.Close();
			Thread.CurrentThread.CurrentCulture = current;
			postImport();	
			Factory.Options.GenerateOwnerHistory = ownerHistory;
			return mContext;
		}

		//partial void customPostImport();
		private void postImport() 
		{
			//mWorldCoordinatePlacement = null;
			for(int icounter = 1; icounter < RecordCount; icounter++)
			{
				BaseClassIfc obj = this[icounter];
				if (obj == null)
					continue;
				try
				{
					obj.postParseRelate();
				}
				catch(Exception) { }
			}
			//	customPostImport();
		}
		internal BaseClassIfc InterpretLine(string line)
		{
			if (line.StartsWith("ISO"))
				return null;
			string ts = line.Trim().Replace(" ", "");
			if (ts.StartsWith("FILE_SCHEMA(('IFC2X4", true, System.Globalization.CultureInfo.CurrentCulture) ||
					ts.StartsWith("FILE_SCHEMA(('IFC4", true, System.Globalization.CultureInfo.CurrentCulture))
			{ 
				mRelease = ReleaseVersion.IFC4;
				return null;
			}
			BaseClassIfc result = ParserIfc.ParseLine(line, mRelease);
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
				IfcApplication ea = mFactory.mApplication;
				if (ea != null && ea.mVersion == application.mVersion)
				{
					if (string.Compare(ea.ApplicationFullName, application.ApplicationFullName, true) == 0)
					{
						if (string.Compare(ea.mApplicationIdentifier, application.mApplicationIdentifier) == 0)
						{
							mIfcObjects[ea.mIndex] = null;
							mFactory.mApplication = application;
							mFactory.OwnerHistory(IfcChangeActionEnum.ADDED).mLastModifyingApplication = application.mIndex;
							if (mFactory.mOwnerHistoryModify != null)
								mFactory.mOwnerHistoryModify.mLastModifyingApplication = application.mIndex;
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
					mFactory.mGeomRepContxt = geometricRepresentationContext;
				if (geometricRepresentationContext.mPrecision > 1e-6)
					Tolerance = geometricRepresentationContext.mPrecision;

			}
			IfcSIUnit unit = result as IfcSIUnit;
			if(unit != null)
			{
				if (unit.Name == IfcSIUnitName.METRE && unit.Prefix == IfcSIPrefix.NONE)
					mFactory.mSILength = unit;
				else if (unit.Name == IfcSIUnitName.SQUARE_METRE && unit.Prefix == IfcSIPrefix.NONE)
					mFactory.mSIArea = unit;
				else if (unit.Name == IfcSIUnitName.CUBIC_METRE && unit.Prefix == IfcSIPrefix.NONE)
					mFactory.mSIVolume = unit;
			}
			this[result.mIndex] = result;	
			
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
			if(filename.EndsWith("xml"))
			{
				WriteXMLFile(filename);
				return true;
			}
#if (IFCJSON)
			else if(filename.EndsWith("json"))
			{
				WriteJSONFile(filename);
				return true;
			}

#endif
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
#if (!NOIFCZIP)
			if (zip)
				za.Dispose();
#endif
			return true;
		}
	}

	public partial class FactoryIfc
	{
		private DatabaseIfc mDatabase = null;
		internal FactoryIfc(DatabaseIfc db) { mDatabase = db; }

		public partial class GenerateOptions
		{
			public bool GenerateOwnerHistory { get; set; } = true;
			
		}
		internal GenerateOptions mOptions = new GenerateOptions();
		public GenerateOptions Options { get { return mOptions; } }

		internal Dictionary<int, int> mDuplicateMapping = new Dictionary<int, int>();
		internal BaseClassIfc Duplicate(BaseClassIfc entity) { return Duplicate(entity, true); }
		internal BaseClassIfc Duplicate(BaseClassIfc entity, bool downStream)
		{
			if (mDuplicateMapping.ContainsKey(entity.mIndex))
				return mDatabase[mDuplicateMapping[entity.mIndex]];
			Type type = Type.GetType("GeometryGym.Ifc." + entity.GetType().Name, false, true);
			if (type != null)
			{
				Type[] types = new Type[] { typeof(DatabaseIfc), type, typeof(bool) };
				ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
				if (constructor != null)
					return constructor.Invoke(new object[] { this.mDatabase, entity, downStream }) as BaseClassIfc;
				types = new Type[] { typeof(DatabaseIfc), type };
				constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
				if (constructor != null)
					return constructor.Invoke(new object[] { this.mDatabase, entity }) as BaseClassIfc;
			}
			return null;
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
			IfcAxis2Placement3D pl = this.PlaneXYPlacement;
			//IfcAxis2Placement pl = this.WorldCoordinatePlacement;
			IfcAxis2Placement2D placement = Origin2dPlace;
		}

		private IfcCartesianPoint mOrigin = null, mWorldOrigin = null, mOrigin2d = null;
		//internal int mTempWorldCoordinatePlacement = 0;
		//private IfcAxis2Placement3D mWorldCoordinatePlacement;
		internal IfcAxis2Placement3D mPlacementPlaneXY;
		internal IfcAxis2Placement2D m2DPlaceOrigin;
		internal IfcSIUnit mSILength, mSIArea, mSIVolume;
		public IfcSIUnit SILength { get { if(mSILength == null) mSILength = new IfcSIUnit(mDatabase, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.NONE, IfcSIUnitName.METRE); return mSILength; } }
		public IfcSIUnit SIArea { get { if(mSIArea == null) mSIArea = new IfcSIUnit(mDatabase, IfcUnitEnum.AREAUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SQUARE_METRE); return mSIArea; } }
		public IfcSIUnit SIVolume { get { if(mSIVolume == null) mSIVolume = new IfcSIUnit(mDatabase, IfcUnitEnum.VOLUMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.CUBIC_METRE); return mSIVolume; } }

		internal IfcDirection mXAxis, mYAxis, mZAxis;

		public IfcDirection XAxis { get { if (mXAxis == null) mXAxis = new IfcDirection(mDatabase, 1, 0, 0); return mXAxis; } }
		public IfcDirection YAxis { get { if (mYAxis == null) mYAxis = new IfcDirection(mDatabase, 0, 1, 0); return mYAxis; } }
		public IfcDirection ZAxis { get { if (mZAxis == null) mZAxis = new IfcDirection(mDatabase, 0, 0, 1); return mZAxis; } }

		internal IfcGeometricRepresentationContext mGeomRepContxt;

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


		internal IfcApplication mApplication = null;
		internal IfcApplication Application
		{
			get
			{
				if (mApplication == null)
					mApplication = new IfcApplication(mDatabase);
				return mApplication;
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
		}
		private IfcPerson mPerson = null;
		internal IfcPerson Person
		{
			get
			{
				if (mPerson == null)
				{
					mPerson = new IfcPerson(mDatabase, System.Environment.UserName.Replace("'", ""), "", "");
#if (IFCMODEL && !IFCIMPORTONLY && (RHINO || GH))
			string str = GGYM.ggAssembly.mOptions.OwnerRole;
			if (!string.IsNullOrEmpty(str))
			{
				IfcRoleEnum role = IfcRoleEnum.NOTDEFINED;
				if (Enum.TryParse<IfcRoleEnum>(str, out role))
				{
					if (role != IfcRoleEnum.NOTDEFINED)
						mPerson.Roles = new List<IfcActorRole>() { new IfcActorRole(mDatabase, role, "", "", new List<int>()) };
				}
				else
					mPerson.Roles = new List<IfcActorRole>() { new IfcActorRole(mDatabase, IfcRoleEnum.USERDEFINED, str, "", new List<int>()) };
			}
#endif
				}
				return mPerson;
			}

		}
		private IfcOrganization mOrganization = null;
		internal IfcOrganization Organization
		{
			get
			{
				if (mOrganization == null)
					mOrganization = new IfcOrganization(mDatabase, IfcOrganization.Organization);
				return mOrganization;
			}

		}
		internal IfcOwnerHistory mOwnerHistoryCreate, mOwnerHistoryModify, mOwnerHistoryDelete;
		internal IfcOwnerHistory OwnerHistory(IfcChangeActionEnum changeAction)
		{
			if (changeAction == IfcChangeActionEnum.ADDED)
			{
				if (mOwnerHistoryCreate == null)
					mOwnerHistoryCreate = new IfcOwnerHistory(PersonOrganization, Application, IfcChangeActionEnum.ADDED);
				return mOwnerHistoryCreate;
			}
			if (changeAction == IfcChangeActionEnum.DELETED)
			{
				if (mOwnerHistoryDelete == null)
					mOwnerHistoryDelete = new IfcOwnerHistory(PersonOrganization, Application, IfcChangeActionEnum.DELETED);
				return mOwnerHistoryDelete;
			}
			if (changeAction == IfcChangeActionEnum.MODIFIED)
			{
				if (mOwnerHistoryModify == null)
					mOwnerHistoryModify = new IfcOwnerHistory(PersonOrganization, Application, IfcChangeActionEnum.MODIFIED);
				return mOwnerHistoryModify;
			}
			return null;
		}

		public enum SubContextIdentifier { Axis, Body, Surface, PlanSymbol };
		public IfcGeometricRepresentationSubContext SubContext(SubContextIdentifier nature)
		{
			if (nature == SubContextIdentifier.Axis)
			{
				if (mSubContxtAxis == null)
					mSubContxtAxis = new IfcGeometricRepresentationSubContext(mGeomRepContxt, IfcGeometricProjectionEnum.MODEL_VIEW) { ContextIdentifier = "Axis" };
				return mSubContxtAxis;
			}
			else if (nature == SubContextIdentifier.PlanSymbol)
			{
				if (mSubContxtPlanSymbol == null)
					mSubContxtPlanSymbol = new IfcGeometricRepresentationSubContext(mGeomRepContxt, IfcGeometricProjectionEnum.PLAN_VIEW) { ContextIdentifier = "Annotation" };
				return mSubContxtPlanSymbol;
			}
			if (mSubContxtBody == null)
				mSubContxtBody = new IfcGeometricRepresentationSubContext(mGeomRepContxt, IfcGeometricProjectionEnum.MODEL_VIEW) { ContextIdentifier = "Body" };
			return mSubContxtBody;

		}
		private IfcGeometricRepresentationSubContext mSubContxtBody = null, mSubContxtAxis = null, mSubContxtPlanSymbol = null;

		internal IfcAxis2Placement2D Origin2dPlace
		{
			get
			{
				if (m2DPlaceOrigin == null)
					m2DPlaceOrigin = new IfcAxis2Placement2D(new IfcCartesianPoint(mDatabase, 0, 0));
				return m2DPlaceOrigin;
			}
		}
		//internal IfcCartesianPoint WorldOrigin
		//{
		//	get
		//	{
		//		if(mWorldOrigin == null)
		//			mWorldOrigin = WorldCoordinatePlacement.Location;
		//		return mWorldOrigin;
		//	}
		//}
		internal IfcCartesianPoint Origin2d
		{
			get
			{
				if (mOrigin2d == null)
					mOrigin2d = new IfcCartesianPoint(mDatabase, 0, 0);
				return mOrigin2d;
			}
		}
		//internal IfcAxis2Placement3D WorldCoordinatePlacement
		//{
		//	get
		//	{
		//		if (mWorldCoordinatePlacement == null)
		//		{
		//			if (mContext != null)
		//			{
		//				List<IfcRepresentationContext> contexts = mContext.RepresentationContexts;
		//				foreach (IfcRepresentationContext context in contexts)
		//				{
		//					IfcGeometricRepresentationContext grc = context as IfcGeometricRepresentationContext;
		//					if (grc == null)
		//						continue;
		//					IfcAxis2Placement3D pl = grc.WorldCoordinateSystem as IfcAxis2Placement3D;
		//					if (pl != null)
		//					{
		//						mWorldCoordinatePlacement = pl;
		//						break;
		//					}
		//				}
		//			}
		//			if (mWorldCoordinatePlacement == null)
		//			{
		//				mWorldCoordinatePlacement = new IfcAxis2Placement3D(new IfcCartesianPoint(this, 0, 0, 0), mZAxis, mXAxis);
		//				if (mContext != null)
		//				{
		//					List<IfcRepresentationContext> contexts = mContext.RepresentationContexts;
		//					foreach (IfcRepresentationContext context in contexts)
		//					{
		//						IfcGeometricRepresentationContext grc = context as IfcGeometricRepresentationContext;
		//						if (grc != null)
		//						{
		//							grc.WorldCoordinateSystem = mWorldCoordinatePlacement;
		//							return mWorldCoordinatePlacement;
		//						}
		//					}
		//				}
		//			}
		//		}
		//		return mWorldCoordinatePlacement;
		//	}
		//}
		internal IfcAxis2Placement3D PlaneXYPlacement
		{
			get
			{
				if (mPlacementPlaneXY == null)
					mPlacementPlaneXY = new IfcAxis2Placement3D(Origin);
				return mPlacementPlaneXY;
			}
		}
		internal IfcCartesianPoint Origin
		{
			get
			{
				if (mOrigin == null)
					mOrigin = new IfcCartesianPoint(mDatabase, 0, 0, 0);
				return mOrigin;
			}
		}
		

	}
}

 