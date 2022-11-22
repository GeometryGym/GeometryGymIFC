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
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using GeometryGym.STEP;
using System.Collections;
using GeometryGym.Ifc;

namespace GeometryGym.STEP
{
	public partial class DatabaseSTEP<T> : IEnumerable<T> where T : STEPEntity//, new()
	{
		public string FolderPath { get; set; }

		public STEPFileInformation OriginatingFileInformation = null;
	
		public List<string> Comments { get; } = new List<string>();

		internal string mFileName = "";
		public string FileName
		{
			get { return mFileName; }
			set { mFileName = value; if(!string.IsNullOrEmpty(value)) FolderPath = Path.GetDirectoryName(value); }
		}

		public DatabaseSTEP() { mNextBlank = 1; }

		private SortedDictionary<int, T> mObjects = new SortedDictionary<int, T>();
		public int LastKey() { return mObjects.Count == 0 ? 0 : mObjects.Last().Key; }

		public int NextObjectRecord { set { mNextBlank = value; } }
		public virtual T this[int stepId]
		{
			get
			{
				T result = null;
				mObjects.TryGetValue(stepId, out result);
				return result;
			}
			set
			{
				if (value == null)
				{
					if (mObjects.ContainsKey(stepId))
						mObjects.Remove(stepId);
					if (stepId < mNextBlank && stepId > 0)
						mNextBlank = stepId;
					return;
				}
				
				mObjects[stepId] = value;
				if (stepId == mNextBlank)
					mNextBlank = mNextBlank + 1;
				value.mStepId = stepId;
			}
		}
		internal void appendObject(T o) { this[NextBlank()] = o; }
		private int mNextBlank
		{
			get;
			set;
		} 
		internal int NextBlank()
		{
			while (mObjects.ContainsKey(mNextBlank))
				mNextBlank++;
			return mNextBlank;
		}
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return mObjects.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return mObjects.Values.GetEnumerator();
		}
		private SortedSet<string> mParsingErrors = new SortedSet<string>();
		private SortedSet<string> mParsingWarnings = new SortedSet<string>();
		internal void logParseError(string str) { string error = "XX Error " + str;  if (!mParsingErrors.Contains(error)) mParsingErrors.Add(error); }
		internal void logParseWarning(string str) { string warning = "!! Warning " + str; if (!mParsingWarnings.Contains(warning)) mParsingWarnings.Add(warning); }


		internal virtual bool processFileHeaderLine(string line)
		{
			
			string trimmedLine = ParserSTEP.StripComments(line).Trim();
			ParserSTEP.GetKeyWord(trimmedLine, out int stepID, out string keyword, out string def);
			int pos = 0, len = def.Length;
			if (string.Compare(keyword, "FILE_DESCRIPTION", true) == 0)
			{
				if (OriginatingFileInformation == null)
					OriginatingFileInformation = new STEPFileInformation();
				string field = ParserSTEP.StripField(def, ref pos, len);
				List<string> fields = ParserSTEP.SplitListStrings(field);
				OriginatingFileInformation.FileDescriptions.AddRange(fields.Select(x => ParserSTEP.Decode(x)));
				OriginatingFileInformation.FileImplementationLevel = ParserSTEP.StripString(def, ref pos, len);
				ProcessFileDescription();
				return true;
			}
			if (string.Compare(keyword, "FILE_NAME", true) == 0)
			{
				if (OriginatingFileInformation == null)
					OriginatingFileInformation = new STEPFileInformation();
				try
				{
					string str = ParserSTEP.StripString(def, ref pos, len);
					OriginatingFileInformation.FileName = ParserSTEP.Decode(str);
					str = ParserSTEP.StripString(def, ref pos, len);
					if (DateTime.TryParse(str, out DateTime dateTime))
						OriginatingFileInformation.TimeStamp = dateTime;
					str = ParserSTEP.StripField(def, ref pos, len);
					OriginatingFileInformation.Author.AddRange(ParserSTEP.SplitListStrings(str).Select(x=>ParserSTEP.Decode(x)));
					str = ParserSTEP.StripField(def, ref pos, len);
					OriginatingFileInformation.Organization.AddRange(ParserSTEP.SplitListStrings(str).Select(x=>ParserSTEP.Decode(x)));
					str = ParserSTEP.StripString(def, ref pos, len);
					OriginatingFileInformation.PreProcessorVersion = ParserSTEP.Decode(str);
					str = ParserSTEP.StripString(def, ref pos, len);
					OriginatingFileInformation.OriginatingSystem = ParserSTEP.Decode(str);
					str = ParserSTEP.StripString(def, ref pos, len);
					OriginatingFileInformation.Authorization = ParserSTEP.Decode(str);
				}
				catch { }
				return true;
			}
			return false;
		}
		internal virtual void ProcessFileDescription() { }
	}

	public class STEPFileInformation
	{
		public List<string> FileDescriptionViewDefinition { get; set; } = new List<string>();
		public List<string> FileDescriptionExchangeRequirements { get; set; } = new List<string>();

		public List<string> FileDescriptions { get; set; } = new List<string>();
		public string FileImplementationLevel { get; set; } = "";
		public string FileName { get; set; } = "";
		public DateTime TimeStamp { get; set; } = DateTime.MinValue;
		public List<string> Author { get; set; } = new List<string>();
		public List<string> Organization { get; set; } = new List<string>();
		public string PreProcessorVersion { get; set; } = "";
		public string OriginatingSystem { get; set; } = "";
		public string Authorization { get; set; } = ""; 
	}
}

 