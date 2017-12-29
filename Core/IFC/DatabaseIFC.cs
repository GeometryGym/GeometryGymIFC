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
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{ 
	public enum ReleaseVersion {  IFC2x3, IFC4, IFC4A1, IFC4A2 };
	public enum ModelView { Ifc4Reference, Ifc4DesignTransfer, Ifc4NotAssigned,Ifc2x3Coordination, If2x3NotAssigned };

	public class Triple<T>
	{
		public T X { get; set; } = default(T);
		public T Y { get; set; } = default(T);
		public T Z { get; set; } = default(T);

		public Triple() { }
		public Triple(T x, T y, T z)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}
	public partial class DatabaseIfc : DatabaseSTEP<BaseClassIfc>
	{
		internal Guid id = Guid.NewGuid();
		
		internal ReleaseVersion mRelease = ReleaseVersion.IFC2x3;

		private FactoryIfc mFactory = null;
		public FactoryIfc Factory { get { return mFactory; } }

		public DatabaseIfc() : base() { mFactory = new FactoryIfc(this); }
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
			//mModelSIScale = 1 / GGYM.Units.mLengthConversion[(int) GGYM.GGYMRhino.GGRhino.ActiveUnits()];
			Tolerance = Rhino.RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
#endif
			//mFactory.mGeomRepContxt = new IfcGeometricRepresentationContext(this, 3, Tolerance) { ContextType = "Model" };
			if (generate)
				mFactory.initData();
		}
		public override BaseClassIfc this[int index]
		{
			set
			{
				IfcRoot root = this[index] as IfcRoot;
				if (root != null)
					mGlobalIDs.Remove(root.GlobalId);
				base[index] = value;
				if(value != null)
					value.mDatabase = this;
			}
		}
		internal ModelView mModelView = ModelView.If2x3NotAssigned;
		internal bool mAccuratePreview = false;
		internal string mFileName = "";
		public string FolderPath { get; private set; } = "";
		public string FileName { get { return mFileName; } set { mFileName = value; FolderPath = Path.GetDirectoryName(value); } }
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
		public double ScaleSI
		{
			get { return mModelSIScale; }
			set { mModelSIScale = value; }
		}
		private double mModelTolerance = 0.0001,mModelSIScale = 1;
		internal int mLengthDigits = 5;
		public IfcContext Context { get { return mContext; } }
		public IfcProject Project { get { return mContext as IfcProject; } }
		
		
		internal IfcContext mContext = null;
		
		internal HashSet<string> mGlobalIDs = new HashSet<string>();

		
		private string viewDefinition { get { return (mModelView == ModelView.Ifc2x3Coordination ? "CoordinationView_V2" : (mModelView == ModelView.Ifc4Reference ? "ReferenceView_V1" : (mModelView == ModelView.Ifc4DesignTransfer ? "DesignTransferView_V1" : "notYetAssigned"))); } }
		internal string getHeaderString(string fileName)
		{
			string hdr = "ISO-10303-21;\r\nHEADER;\r\nFILE_DESCRIPTION(('ViewDefinition [" + viewDefinition + "]'),'2;1');\r\n";

			hdr += "FILE_NAME(\r\n";
            hdr += "/* name */ '" + ParserIfc.ReplaceAe(ParserIfc.Encode(Path.GetFileName(fileName))) + "',\r\n";
            DateTime now = DateTime.Now;
			hdr += "/* time_stamp */ '" + now.Year + "-" + (now.Month < 10 ? "0" : "") + now.Month + "-" + (now.Day < 10 ? "0" : "") + now.Day + "T" + (now.Hour < 10 ? "0" : "") + now.Hour + ":" + (now.Minute < 10 ? "0" : "") + now.Minute + ":" + (now.Second < 10 ? "0" : "") + now.Second + "',\r\n";
            hdr += "/* author */ ('" + ParserIfc.ReplaceAe(System.Environment.UserName) + "'),\r\n";
            hdr += "/* organization */ ('" + ParserIfc.ReplaceAe(IfcOrganization.Organization) + "'),\r\n";
            hdr += "/* preprocessor_version */ '" + mFactory.ToolkitName  + "',\r\n";
            hdr += "/* originating_system */ '" + ParserIfc.ReplaceAe(mFactory.ApplicationFullName) + "',\r\n";
            hdr += "/* authorization */ 'None');\r\n\r\n";
			hdr += "FILE_SCHEMA (('" + (mRelease == ReleaseVersion.IFC2x3 ? "IFC2X3" : "IFC4") + "'));\r\n";
			hdr += "ENDSEC;\r\n";
			hdr += "\r\nDATA;";
			return hdr;
		}
		internal string getFooterString() { return "ENDSEC;\r\n\r\nEND-ISO-10303-21;\r\n\r\n"; } 
		public override string ToString()
		{
			string result = getHeaderString("") + "\r\n";
			foreach(BaseClassIfc e in this)
			{
				string str = e.ToString();
				if (str != "")
					result += str +"\r\n";
			}
			return result + getFooterString();
		}
	
		internal enum FormatIfc { JSON, STEP, XML }
		internal class FileStreamIfc
		{
			internal FormatIfc mFormat;
			internal TextReader mTextReader;
			internal FileStreamIfc(FormatIfc format, TextReader sr)
			{
				mFormat = format;
				mTextReader = sr;
			} 
		}
		private FormatIfc detectFormat(string fileName)
		{
			string lower = fileName.ToLower();
			if (lower.EndsWith("xml"))
				return FormatIfc.XML; 
			if (lower.EndsWith("json"))
				return FormatIfc.JSON;
			return FormatIfc.STEP;
		}
		internal FileStreamIfc getStreamReader(string fileName)
		{
			string ext = Path.GetExtension(fileName);
			FileName = fileName;
			FolderPath = Path.GetDirectoryName(fileName);
#if (!NOIFCZIP)
			if (fileName.ToLower().EndsWith("zip"))
			{
				System.IO.Compression.ZipArchive za = System.IO.Compression.ZipFile.OpenRead(fileName);
				if (za.Entries.Count != 1)
				{
					return null;
				}
				string filename = za.Entries[0].Name.ToLower();
				FormatIfc fformat = detectFormat(filename);
				StreamReader str = (fformat == FormatIfc.STEP ? new StreamReader(za.Entries[0].Open(), Encoding.GetEncoding("windows-1252")) :
					new StreamReader(za.Entries[0].Open()));
				return new FileStreamIfc(fformat,str);
			}
#endif
			FormatIfc format = detectFormat(fileName);
			StreamReader sr = format == FormatIfc.STEP ? new StreamReader(fileName, Encoding.GetEncoding("windows-1252")) :
				new StreamReader(fileName);
			return new FileStreamIfc(format, sr);
		}

		internal void ReadFile(string filename)
		{
			if(filename.ToLower().EndsWith("ifc"))
				importLines(File.ReadAllLines(filename));
			else
				ReadFile(getStreamReader(filename));
		}
		internal void ReadFile(FileStreamIfc fs)
		{
			if (fs == null)
				return;
			switch (fs.mFormat)
			{
				case FormatIfc.XML:
					ReadXMLStream(fs.mTextReader);
					break;
				case FormatIfc.JSON:
#if (NOIFCJSON)
					logError("IfcJSON not enabled!");
					return;
#else
					ReadJSONFile(fs.mTextReader);
					break;
#endif
				default:
					ReadFile(fs.mTextReader, 0);
					break;
			}
			
		}

		private IfcContext ReadFile(TextReader sr, int offset)
		{
			int i = sr.Peek();
			if (i < 0)
				return null;
			if (i == (int)'{')
			{
				ReadFile(new FileStreamIfc(FormatIfc.JSON, sr));
				return mContext;
			}
			else
			{
				
				List<string> lines = new List<string>();
				string strLine = sr.ReadLine();
				if (strLine == null)
					return null;
				while (strLine != null)
				{
					lines.Add(strLine);
					strLine = sr.ReadLine();
				}
			
				if (offset > 0)
				{
					int count = lines.Count; 
					for(int icounter = 0; icounter< count; icounter++)
						 lines[icounter] = ParserSTEP.offsetSTEPRecords(lines[icounter], offset);
				}
				sr.Close();
				return importLines(lines);
			}
			
		}
		private IfcContext importLines(IEnumerable<string> lines)
		{
			mRelease = ReleaseVersion.IFC2x3;
			bool ownerHistory = mFactory.Options.GenerateOwnerHistory;
			mFactory.Options.GenerateOwnerHistory = false;
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

			HashSet<string> toIgnore = new HashSet<string>() { "ISO-10303-21;", "HEADER;", "ENDSEC;", "DATA;", "END-ISO-10303-21;" };
			
			List<string> revised = new List<string>();
			int count = lines.Count();
			for(int icounter = 0; icounter < count; icounter++)
			{
				string strLine = lines.ElementAt(icounter);
				if (string.IsNullOrEmpty(strLine) || (strLine.Length < 20 && toIgnore.Contains(strLine.ToUpper())))
					continue;
				
				if(!strLine.EndsWith(");"))
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
						while(icounter+1 < count)
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
							if (strLine.EndsWith(");"))
								break;
						}
					}
				}
				char c = char.ToUpper(strLine[0]);
				if (c == 'F')
				{
					setFileLine(strLine);
					continue;
				}
				if (c == 'E')
					continue;
				revised.Add(strLine);
			}
			
#if (MULTITHREAD)
			ConcurrentBag<BaseClassIfc> objects = new ConcurrentBag<BaseClassIfc>();
			ConcurrentBag<string> errors = new ConcurrentBag<string>();
			Parallel.ForEach(revised, new ParallelOptions { MaxDegreeOfParallelism = 4 }, line =>
			{
				try
				{
					BaseClassIfc obj = interpretLine(line);
					if (obj != null)
						objects.Add(obj);
				}
				catch (Exception x) { errors.Add("XXX Error in line " + line + " " + x.Message); }
			});
			BaseClassIfc last = objects.Last();
			if (last != null)
				setSize(last.mIndex);

			foreach (BaseClassIfc obj in objects)
				this[obj.mIndex] = obj;
			foreach (string error in errors)
				logError(error);
#else
			foreach (string line in revised)
			{
				try
				{
					BaseClassIfc obj = interpretLine(line);
					if (obj != null)
						this[obj.mIndex] = obj;
				}
				catch (Exception x) { logError("XXX Error in line " + line + " " + x.Message); }
			}
#endif

			Thread.CurrentThread.CurrentCulture = current;
			postImport();
			Factory.Options.GenerateOwnerHistory = ownerHistory;
			return mContext;
		}
		internal bool setFileLine(string line)
		{
			string ts = line.Trim().Replace(" ", "");
			if (ts.StartsWith("FILE_SCHEMA(('IFC2X4", true, CultureInfo.CurrentCulture) ||
					ts.StartsWith("FILE_SCHEMA(('IFC4", true, CultureInfo.CurrentCulture))
			{
				mRelease = ReleaseVersion.IFC4;
				if (mModelView == ModelView.Ifc2x3Coordination || mModelView == ModelView.If2x3NotAssigned)
					mModelView = ModelView.Ifc4NotAssigned;
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
					PreviousApplication = fields.Count > 6 ? fields[5].Replace("'", "") : "";
				}
				return true;
			}
			return false;
		}
		//partial void customPostImport();
		private void postImport() 
		{
			foreach(BaseClassIfc e in this)
			{
				try
				{
					e.postParseRelate();
				}
				catch(Exception) { }
			}
			if (mContext != null)
			{
				mContext.initializeUnitsAndScales();

				mFactory.IdentifyContexts(mContext.RepresentationContexts);

			}
			//	customPostImport();
		}
		internal BaseClassIfc interpretLine(string line)
		{
			BaseClassIfc result = ParserIfc.ParseLine(line, mRelease);
			if (result == null)
			{
				if (line.StartsWith("ISO"))
					return null;
				if (setFileLine(line))
					return null;
				int ifcID = 0;
				string kw = "", str = "";
				ParserSTEP.GetKeyWord(line, out ifcID, out kw, out str);
				if (string.IsNullOrEmpty(kw) || !kw.ToLower().StartsWith("ifc"))
					return null;

				result = new BaseClassIfc(ifcID, kw, str);
			}
			if (result == null)
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
							this[ea.mIndex] = null;
							mFactory.mApplication = application;
							//	mFactory.OwnerHistory(IfcChangeActionEnum.ADDED).mLastModifyingApplication = application.mIndex;
							//	if (mFactory.mOwnerHistories.ContainsKey(IfcChangeActionEnum.MODIFIED))
							//		mFactory.mOwnerHistories[IfcChangeActionEnum.MODIFIED].mLastModifyingApplication = application.mIndex;
						}
					}
				}
			}
			
			IfcGeometricRepresentationContext geometricRepresentationContext = result as IfcGeometricRepresentationContext;
			if (geometricRepresentationContext != null)
				Tolerance = geometricRepresentationContext.mPrecision;
			IfcSIUnit unit = result as IfcSIUnit;
			if (unit != null)
			{
				if (unit.Name == IfcSIUnitName.METRE && unit.Prefix == IfcSIPrefix.NONE)
					mFactory.mSILength = unit;
				else if (unit.Name == IfcSIUnitName.SQUARE_METRE && unit.Prefix == IfcSIPrefix.NONE)
					mFactory.mSIArea = unit;
				else if (unit.Name == IfcSIUnitName.CUBIC_METRE && unit.Prefix == IfcSIPrefix.NONE)
					mFactory.mSIVolume = unit;
			}
			return result;
		}

		public bool WriteFile(string filename)
		{
			StreamWriter sw = null;

			
			FolderPath = Path.GetDirectoryName(filename);
			string fn = Path.GetFileNameWithoutExtension(filename);
			char[] chars = Path.GetInvalidFileNameChars();
			foreach (char c in chars)
				fn = fn.Replace(c, '_');
			FileName = Path.Combine(FolderPath, fn  + Path.GetExtension(filename));
			if(filename.EndsWith("xml"))
			{
				WriteXMLFile(FileName);
				return true;
			}
#if (!NOIFCJSON)
			else if(FileName.EndsWith("json"))
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
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			sw.Write(getHeaderString(filename) + "\r\n");
			foreach(BaseClassIfc e in this)
			{
				if (e != null)
				{
					try
					{
						string str = e.ToString();
						if (!string.IsNullOrEmpty(str))
							sw.WriteLine(str);
					}
					catch(Exception) { }
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
			public bool AngleUnitsInRadians { get; set; } = true;	
		}
		internal GenerateOptions mOptions = new GenerateOptions();
		public GenerateOptions Options { get { return mOptions; } }

		internal Dictionary<int, int> mDuplicateMapping = new Dictionary<int, int>();
		internal BaseClassIfc Duplicate(BaseClassIfc entity) { return Duplicate(entity, true); }
		internal BaseClassIfc Duplicate(BaseClassIfc entity, bool downStream)
		{
			if (entity == null)
				return null;
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
			IfcAxis2Placement3D pl = this.XYPlanePlacement;
			//IfcAxis2Placement pl = this.WorldCoordinatePlacement;
			IfcAxis2Placement2D placement = Origin2dPlace;
		}

		private IfcCartesianPoint mOrigin = null, mWorldOrigin = null, mOrigin2d = null;
		//internal int mTempWorldCoordinatePlacement = 0;

		private IfcLocalPlacement mRootPlacement = null;
		internal IfcAxis2Placement3D mRootPlacementPlane = null;
		internal IfcAxis2Placement3D mPlacementPlaneXY;
		internal IfcAxis2Placement2D m2DPlaceOrigin;

		public IfcUnit LengthUnit(IfcUnitAssignment.Length length)
		{
			if (length == IfcUnitAssignment.Length.Millimetre)
				return new IfcSIUnit(mDatabase, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.MILLI, IfcSIUnitName.METRE);
			if (length == IfcUnitAssignment.Length.Centimetre)
				return new IfcSIUnit(mDatabase, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.CENTI, IfcSIUnitName.METRE);
			if (length == IfcUnitAssignment.Length.Inch)
			{
				IfcMeasureWithUnit mwu = new IfcMeasureWithUnit(new IfcLengthMeasure(0.0254), SILength);
				return new IfcConversionBasedUnit(IfcUnitEnum.LENGTHUNIT, "Inches", mwu);
			}
			if (length == IfcUnitAssignment.Length.Foot)
			{
				IfcMeasureWithUnit mwu = new IfcMeasureWithUnit(new IfcLengthMeasure(IfcUnitAssignment.FeetToMetre), SILength);
				return new IfcConversionBasedUnit(IfcUnitEnum.LENGTHUNIT, "Feet", mwu);
			}
			return SILength;
		}
		internal IfcSIUnit mSILength, mSIArea, mSIVolume;
		public IfcSIUnit SILength { get { if(mSILength == null) mSILength = new IfcSIUnit(mDatabase, IfcUnitEnum.LENGTHUNIT, IfcSIPrefix.NONE, IfcSIUnitName.METRE); return mSILength; } }
		public IfcSIUnit SIArea { get { if(mSIArea == null) mSIArea = new IfcSIUnit(mDatabase, IfcUnitEnum.AREAUNIT, IfcSIPrefix.NONE, IfcSIUnitName.SQUARE_METRE); return mSIArea; } }
		public IfcSIUnit SIVolume { get { if(mSIVolume == null) mSIVolume = new IfcSIUnit(mDatabase, IfcUnitEnum.VOLUMEUNIT, IfcSIPrefix.NONE, IfcSIUnitName.CUBIC_METRE); return mSIVolume; } }

		internal IfcDirection mXAxis, mYAxis, mZAxis, mNegXAxis, mNegYAxis, mNegZAxis;

		public IfcDirection XAxis { get { if (mXAxis == null) mXAxis = new IfcDirection(mDatabase, 1, 0, 0); return mXAxis; } }
		public IfcDirection YAxis { get { if (mYAxis == null) mYAxis = new IfcDirection(mDatabase, 0, 1, 0); return mYAxis; } }
		public IfcDirection ZAxis { get { if (mZAxis == null) mZAxis = new IfcDirection(mDatabase, 0, 0, 1); return mZAxis; } }
		public IfcDirection XAxisNegative { get { if (mNegXAxis == null) mNegXAxis = new IfcDirection(mDatabase, -1, 0, 0); return mNegXAxis; } }
		public IfcDirection YAxisNegative { get { if (mNegYAxis == null) mNegYAxis = new IfcDirection(mDatabase, 0, -1, 0); return mNegYAxis; } }
		public IfcDirection ZAxisNegative { get { if (mNegZAxis == null) mNegZAxis = new IfcDirection(mDatabase, 0, 0, -1); return mNegZAxis; } }

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
					string date = String.Format("{0:s}", System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToUniversalTime(), CultureInfo.InvariantCulture);
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
			set { mApplication = value; }
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
						mPerson.AddRole(new IfcActorRole(mDatabase, role, "", "", new List<int>()));
				}
				else
					mPerson.AddRole(new IfcActorRole(mDatabase, IfcRoleEnum.USERDEFINED, str, "", new List<int>()));
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
		internal Dictionary<IfcChangeActionEnum, IfcOwnerHistory> mOwnerHistories = new Dictionary<IfcChangeActionEnum, IfcOwnerHistory>();
		internal IfcOwnerHistory OwnerHistory(IfcChangeActionEnum changeAction)
		{
			if (mOwnerHistories.ContainsKey(changeAction))
				return mOwnerHistories[changeAction];
			IfcOwnerHistory result = new IfcOwnerHistory(PersonOrganization, Application, changeAction);
			mOwnerHistories.Add(changeAction, result);
			return result;
		}
		public void AddOwnerHistory(IfcOwnerHistory ownerHistory)
		{
			if (!mOwnerHistories.ContainsKey(ownerHistory.ChangeAction))
				mOwnerHistories.Add(ownerHistory.ChangeAction, ownerHistory);
		}

		public enum ContextIdentifier { Annotation, Model};
		internal Dictionary<ContextIdentifier, IfcGeometricRepresentationContext> mContexts = new Dictionary<ContextIdentifier, IfcGeometricRepresentationContext>();		
		public IfcGeometricRepresentationContext GeometricRepresentationContext(ContextIdentifier nature)
		{
			IfcGeometricRepresentationContext result = null;
			if (mContexts.TryGetValue(nature, out result))
				return result;
			string type = "Model";
			int dimension = 3;

			if (nature == ContextIdentifier.Annotation)
			{
				type = "Annotation";
			}
			
			result = new IfcGeometricRepresentationContext(mDatabase, dimension, mDatabase.Tolerance) { ContextType = type };
			IfcContext context = mDatabase.Context;
			if (context != null)
				context.addRepresentationContext(result);
			mContexts.Add(nature, result);
			return result;
		}
		internal void IdentifyContexts(IEnumerable< IfcRepresentationContext> contexts)
		{
			if (mContexts.ContainsKey(ContextIdentifier.Model))
				return;
			foreach(IfcRepresentationContext context in contexts)
			{
				IfcGeometricRepresentationContext grc = context as IfcGeometricRepresentationContext;
				if(grc != null)
				{
					if (string.Compare(grc.ContextType, "Model", true) == 0)
						mContexts.Add(ContextIdentifier.Model, grc);
				}
			}
		}
		public enum SubContextIdentifier { Axis, Body, BoundingBox, FootPrint, PlanSymbol3d, PlanSymbol2d };// Surface };
		public IfcGeometricRepresentationSubContext SubContext(SubContextIdentifier nature)
		{
			IfcGeometricRepresentationSubContext result = null;
			if (mSubContexts.TryGetValue(nature, out result))
				return result;
			string identifier = "Body";
			IfcGeometricProjectionEnum projection = IfcGeometricProjectionEnum.MODEL_VIEW;
			IfcGeometricRepresentationContext context = null;
			if (nature == SubContextIdentifier.Axis)
			{
				identifier = "Axis";
				projection = IfcGeometricProjectionEnum.GRAPH_VIEW;
			}
			else if (nature == SubContextIdentifier.BoundingBox)
			{
				projection = IfcGeometricProjectionEnum.MODEL_VIEW;
				identifier = "Box";
			}
			else if (nature == SubContextIdentifier.FootPrint)
			{
				identifier = "FootPrint";
			}
			else if (nature == SubContextIdentifier.PlanSymbol3d)
			{
				projection = IfcGeometricProjectionEnum.PLAN_VIEW;
				identifier = "Annotation";
			}
			else if (nature == SubContextIdentifier.PlanSymbol2d)
			{
				projection = IfcGeometricProjectionEnum.PLAN_VIEW;
				identifier = "Annotation";
				context = GeometricRepresentationContext(ContextIdentifier.Annotation);
			}
			if (context == null)
				context = GeometricRepresentationContext(ContextIdentifier.Model);
			result = new IfcGeometricRepresentationSubContext(context, projection ) { ContextIdentifier = identifier };
			mSubContexts.Add(nature, result);
			return result;
		}
		private Dictionary<SubContextIdentifier, IfcGeometricRepresentationSubContext> mSubContexts = new Dictionary<SubContextIdentifier, IfcGeometricRepresentationSubContext>();		
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
}

 