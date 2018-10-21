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
	public enum ReleaseVersion {[Obsolete("DEPRECEATED IFC4", false)] IFC2X, [Obsolete("DEPRECEATED IFC4", false)] IFC2x2, IFC2x3, IFC4, IFC4A1, IFC4A2, IFC4X1, IFC4X2 }; // Alpha Releases IFC1.0, IFC1.5, IFC1.5.1, IFC2.0, 
	public enum ModelView { Ifc4Reference, Ifc4DesignTransfer, Ifc4NotAssigned,Ifc2x3Coordination, Ifc2x3NotAssigned };

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
	public enum FormatIfcSerialization { STEP, XML, JSON };
	public partial class DatabaseIfc : DatabaseSTEP<BaseClassIfc>
	{
		internal string id = ParserIfc.EncodeGuid(Guid.NewGuid());

		private bool mIsDisposed = false;
		internal bool IsDisposed() { return mIsDisposed; }
		public void Dispose() { mIsDisposed = true; }

		internal ReleaseVersion mRelease = ReleaseVersion.IFC2x3;
		public FormatIfcSerialization Format { get; set; } = FormatIfcSerialization.STEP;
		
		private FactoryIfc mFactory = null;
		public FactoryIfc Factory { get { return mFactory; } }

		public DatabaseIfc() : base() { mFactory = new FactoryIfc(this); }
		public DatabaseIfc(string fileName) : this() { ReadFile(fileName); }
		public DatabaseIfc(TextReader stream) : this() { ReadFile(stream, 0); }
		public DatabaseIfc(ModelView view) : this(true, view) { }
		public DatabaseIfc(DatabaseIfc db) : this() { mRelease = db.mRelease; mModelView = db.mModelView; Tolerance = db.Tolerance; }
		public DatabaseIfc(bool generate, ModelView view) : this(generate, view == ModelView.Ifc2x3Coordination || view == ModelView.Ifc2x3NotAssigned ? ReleaseVersion.IFC2x3 : ReleaseVersion.IFC4A1, view) { }
		public DatabaseIfc(bool generate, ReleaseVersion schema) : this(generate, schema, schema < ReleaseVersion.IFC4 ? ModelView.Ifc2x3NotAssigned : ModelView.Ifc4NotAssigned) { }
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
				{
					BaseClassIfc obj = null;
					mDictionary.TryRemove(root.GlobalId, out obj);
				}
				root = value as IfcRoot;
				if(root != null)
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
		internal string mFileName = "";
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
		private double mModelTolerance = 0.0001,mModelSIScale = double.NaN;
		internal int mLengthDigits = 5;
		public IfcContext Context { get { return mContext; } }
		public IfcProject Project { get { return mContext as IfcProject; } }
		
		internal IfcContext mContext = null;
		
		internal ConcurrentDictionary<string, BaseClassIfc> mDictionary = new ConcurrentDictionary<string, BaseClassIfc>();

		
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
			hdr += "/* preprocessor_version */ '" + mFactory.ToolkitName  + "',\r\n";
			hdr += "/* originating_system */ '" + mFactory.ApplicationFullName + "',\r\n";
			hdr += "/* authorization */ 'None');\r\n\r\n";
			string version = "IFC4";
			if (mRelease == ReleaseVersion.IFC2x3)
				version = "IFC2X3";
			else if (mRelease == ReleaseVersion.IFC4X1)
				version = "IFC4X1";
			else if (mRelease == ReleaseVersion.IFC4X2)
				version = "IFC4X2";
			hdr += "FILE_SCHEMA (('" + version + "'));\r\n";
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
		public static DatabaseIfc ParseString(string str)
		{
			if (string.IsNullOrEmpty(str))
				return null;

			DatabaseIfc db = new DatabaseIfc(false, ReleaseVersion.IFC4);
#if (!NOIFCJSON)
			if(str.StartsWith("{"))
			{
				Newtonsoft.Json.Linq.JObject jobj = Newtonsoft.Json.Linq.JObject.Parse(str);
				if (str != null)
					db.ReadJSON(jobj);
			}
#endif
			if(str.StartsWith("<"))
			{
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(str);
				db.ReadXMLDoc(doc);
			}
			return db;
		}
	
		internal class FileStreamIfc
		{
			internal FormatIfcSerialization mFormat;
			internal TextReader mTextReader;
			internal FileStreamIfc(FormatIfcSerialization format, TextReader sr)
			{
				mFormat = format;
				mTextReader = sr;
			} 
		}
		private FormatIfcSerialization detectFormat(string fileName)
		{
			string lower = fileName.ToLower();
			if (lower.EndsWith("xml"))
				return FormatIfcSerialization.XML; 
			if (lower.EndsWith("json"))
				return FormatIfcSerialization.JSON;
			return FormatIfcSerialization.STEP;
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
				FormatIfcSerialization fformat = detectFormat(filename);
				StreamReader str = (fformat == FormatIfcSerialization.STEP ? new StreamReader(za.Entries[0].Open(), Encoding.GetEncoding("windows-1252")) :
					new StreamReader(za.Entries[0].Open()));
				return new FileStreamIfc(fformat,str);
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

			FolderPath = Path.GetDirectoryName(filename);
			if(filename.ToLower().EndsWith("ifc"))
				importLines(File.ReadAllLines(filename));
			else
				ReadFile(getStreamReader(filename));
		}
		internal void ReadFile(FileStreamIfc fs)
		{
			if (fs == null)
				return;
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

			switch (fs.mFormat)
			{
				case FormatIfcSerialization.XML:
					ReadXMLStream(fs.mTextReader);
					break;
				case FormatIfcSerialization.JSON:
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
			Thread.CurrentThread.CurrentCulture = current;
		}

		private IfcContext ReadFile(TextReader sr, int offset)
		{
			int i = sr.Peek();
			if (i < 0)
				return null;
			if (i == (int)'{')
			{
				ReadFile(new FileStreamIfc(FormatIfcSerialization.JSON, sr));
				return mContext;
			}
			else if (i == (int)'<')
			{
				ReadFile(new FileStreamIfc(FormatIfcSerialization.XML, sr));
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
				importLines(lines);
				return mContext;
			}
			
		}
		private class ConstructorClass
		{
			internal BaseClassIfc Object { get; }
			internal string DefinitionString { get; }
			internal ConstructorClass(BaseClassIfc obj, string definition) { Object = obj; DefinitionString = definition; }
		}
		internal void importLines(IEnumerable<string> lines)
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
				
				if(!strLine.EndsWith(";"))
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
							if (strLine.EndsWith(";")) //);
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

			ConcurrentDictionary<int, BaseClassIfc> dictionary = new ConcurrentDictionary<int, BaseClassIfc>();
			dictionary[0] = null;
#if (MULTITHREAD)
			ConcurrentBag<ConstructorClass> bag = new ConcurrentBag<ConstructorClass>();
#else
			List<ConstructorClass> bag = new List<ConstructorClass>();
#endif
			foreach(string str in revised)
			{
				int ifcID = 0;
				string kw = "", def  = "";
				ParserSTEP.GetKeyWord(str, out ifcID, out kw, out def);
				if(!string.IsNullOrEmpty(kw))
				{
					BaseClassIfc obj = BaseClassIfc.Construct(kw);
					if (obj != null)
					{
						dictionary[ifcID] = obj;
						bag.Add(new ConstructorClass(obj, def));
						this[ifcID] = obj;
					}
				}
			}
			ReleaseVersion release = Release;
#if (qsMULTITHREAD)
			ConcurrentBag<string> errors = new ConcurrentBag<string>();
			Parallel.ForEach(bag, new ParallelOptions { MaxDegreeOfParallelism = 4 }, obj =>
			{
				try
				{
					int pos = 0;
					string def = obj.DefinitionString;
					obj.Object.parse(def, ref pos,release, def.Length, dictionary);
				}
				catch (Exception x) { errors.Add("XXX Error in line #" + obj.Object.Index + " " + obj.Object.KeyWord + " " + x.Message); }
			});

			foreach (string error in errors)
				logError(error);
#else
			foreach (ConstructorClass obj in bag)
			{
				try
				{
					int pos = 0;
					string def = obj.DefinitionString;
					obj.Object.parse(def, ref pos, release, def.Length, dictionary);
				}
				catch (Exception x) { logError("XXX Error in line #" + obj.Object.Index + " " + obj.Object.KeyWord + " " + x.Message); }
			}
#endif
			Thread.CurrentThread.CurrentCulture = current;
			postParseRelate();
			postImport();
			Factory.Options.GenerateOwnerHistory = ownerHistory;
		}
		internal bool setFileLine(string line)
		{
			string ts = line.Trim().Replace(" ", "");
			if (ts.StartsWith("FILE_SCHEMA(('IFC2X4", true, CultureInfo.CurrentCulture) ||
					ts.StartsWith("FILE_SCHEMA(('IFC4", true, CultureInfo.CurrentCulture))
			{
				if(ts.StartsWith("FILE_SCHEMA(('IFC4X1", true, CultureInfo.CurrentCulture))
					mRelease = ReleaseVersion.IFC4X1;
				else if(ts.StartsWith("FILE_SCHEMA(('IFC4X2", true, CultureInfo.CurrentCulture))
					mRelease = ReleaseVersion.IFC4X2;
				else
					mRelease = ReleaseVersion.IFC4;
				if (mModelView == ModelView.Ifc2x3Coordination || mModelView == ModelView.Ifc2x3NotAssigned)
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
		private void postParseRelate()
		{
			foreach (BaseClassIfc e in this)
			{
				try
				{
					e.postParseRelate();
				}
				catch (Exception) { }
			}
		}
		private void postImport() 
		{
			if (mContext != null)
			{
				mContext.initializeUnitsAndScales();
				mFactory.IdentifyContexts(mContext.RepresentationContexts);
			}
			//	customPostImport();
		}
		internal BaseClassIfc interpretLine(string line, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			BaseClassIfc result = ParserIfc.ParseLine(line, mRelease, dictionary);
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

				return null;
			}
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
						string str = e.StringSTEP();
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
	public class DuplicateMapping
	{
		private Dictionary<string, int> mDictionary = new Dictionary<string, int>();
		internal int FindExisting(BaseClassIfc obj)
		{
			if (obj == null)
				return 0;
			int result = 0;
			mDictionary.TryGetValue(key(obj), out result);
			return result;
		}
		internal void AddObject(BaseClassIfc obj, int index) { mDictionary.Add(key(obj), index); }
		private string key(BaseClassIfc obj) { return obj.mDatabase.id + "|" + obj.mIndex; }
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
			public bool GenerateOwnerHistory { get; set; } = true;
			public bool AngleUnitsInRadians { get; set; } = true;
		}
		internal GenerateOptions mOptions = new GenerateOptions();
		public GenerateOptions Options { get { return mOptions; } }

		public BaseClassIfc Construct(string className)
		{
			BaseClassIfc result = BaseClassIfc.Construct(className);
			if (result == null)
				return null;
			mDatabase[mDatabase.NextBlank] = result;
			return result; 
		}
		internal DuplicateMapping mDuplicateMapping = new DuplicateMapping(); 
		public BaseClassIfc Duplicate(IBaseClassIfc entity) { return Duplicate(entity as BaseClassIfc, true); }
		public void NominateDuplicate(IBaseClassIfc entity, IBaseClassIfc existingDuplicate) { mDuplicateMapping.AddObject(entity as BaseClassIfc, existingDuplicate.Index); }
		internal BaseClassIfc Duplicate(BaseClassIfc entity, bool downStream)
		{
			if (entity == null)
				return null;
			int index = mDuplicateMapping.FindExisting(entity);
			if(index > 0)
				return mDatabase[index];
			if (!string.IsNullOrEmpty(entity.mGlobalId))
			{
				BaseClassIfc result = null;
				if (mDatabase.mDictionary.TryGetValue(entity.mGlobalId, out result))
				{
					if (result != null)
					{
						mDuplicateMapping.AddObject(entity, result.mIndex);
						return result;
					}
				}
			}
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
				types = new Type[] { typeof(DatabaseIfc), type, typeof(IfcOwnerHistory), typeof(bool) };
				constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
				if (constructor != null)
					return constructor.Invoke(new object[] { this.mDatabase, entity, null, downStream }) as BaseClassIfc;
				//return Duplicate(entity, downStream);
			}
			return null;
		}
		internal BaseClassIfc Duplicate(BaseClassIfc entity, IfcOwnerHistory ownerHistory, bool downStream)
		{
			if (entity == null)
				return null;
			int index = mDuplicateMapping.FindExisting(entity);
			if (index > 0)
				return mDatabase[index];
			if(!string.IsNullOrEmpty(entity.mGlobalId))
			{
				BaseClassIfc result = null;
				if(mDatabase.mDictionary.TryGetValue(entity.mGlobalId,out result))
				{
					if (result != null)
					{
						mDuplicateMapping.AddObject(entity, result.mIndex);
						return result;
					}
				}
			}
			Type type = Type.GetType("GeometryGym.Ifc." + entity.GetType().Name, false, true);
			if (type != null)
			{
				Type[] types = new Type[] { typeof(DatabaseIfc), type, typeof(IfcOwnerHistory), typeof(bool) };
				ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, types, null);
				if (constructor != null)
					return constructor.Invoke(new object[] { this.mDatabase, entity, ownerHistory, downStream}) as BaseClassIfc;
				return Duplicate(entity, downStream);	
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
					string date = String.Format("{0:s}", System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToUniversalTime());
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
						mPerson.Roles.Add(new IfcActorRole(mDatabase, role, "", "", new List<int>()));
				}
				else
					mPerson.Roles.Add(new IfcActorRole(mDatabase, IfcRoleEnum.USERDEFINED, str, "", new List<int>()));
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
		internal List<IfcOwnerHistory> mOwnerHistories = new List<IfcOwnerHistory>();
		public IfcOwnerHistory OwnerHistory(IfcChangeActionEnum action, IfcPersonAndOrganization owner, IfcApplication application, DateTime created)
		{
			return OwnerHistory(action, owner, application, null, null, DateTime.MinValue, created);	
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
				if (oh.isDuplicate(ownerHistory))
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
				if (oh.isDuplicate(ownerHistory))
					return;
			}
			mOwnerHistories.Add(ownerHistory);
		}
		private IfcOwnerHistory mOwnerHistoryAdded = null;
		public IfcOwnerHistory OwnerHistoryAdded
		{
			get
			{
				if (mDatabase.Release > ReleaseVersion.IFC2x3 && !mOptions.GenerateOwnerHistory)
					return null;
				if(mOwnerHistoryAdded == null)
					mOwnerHistoryAdded = new IfcOwnerHistory(PersonOrganization, Application, IfcChangeActionEnum.ADDED);
				return mOwnerHistoryAdded;
			}
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
}

 