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
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeometryGym.STEP
{

	public partial interface ISTEPEntity
	{
		int Index { get; }
	}
	[Serializable]
	public partial class STEPEntity
	{
		[NonSerialized] internal int mIndex = 0;
		[NonSerialized] internal List<string> mComments = new List<string>();

		public int Index { get { return mIndex; } private set { mIndex = value; } }

		protected static ConcurrentDictionary<string, Type> mTypes = new ConcurrentDictionary<string, Type>();
		protected static ConcurrentDictionary<string, ConstructorInfo> mConstructors = new ConcurrentDictionary<string, ConstructorInfo>();

		internal STEPEntity() { initialize(); }
		internal virtual string KeyWord { get { return this.GetType().Name; } }

		protected virtual void initialize() { }
		public string StringSTEP()
		{
			string str = BuildStringSTEP();
			if (string.IsNullOrEmpty(str))
				return "";
			string comment = "";
			if (mComments.Count > 0)
			{
				foreach (string c in mComments)
					comment += "/* " + c + " */\r\n";
			}
			return comment + (mIndex > 0 ? "#" + mIndex + "= " : "") + KeyWord.ToUpper() + "(" + str.Substring(1) + ");";
		}
		public override string ToString() { return StringSTEP(); }
		protected virtual string BuildStringSTEP() { return ""; } 
		public void  AddComment(string comment) { mComments.Add(comment); }

	}
}
