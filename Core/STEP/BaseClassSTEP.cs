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
		int StepId { get; }
		string StepClassName { get; }
	}
	[Serializable]
	public abstract partial class STEPEntity
	{
		[NonSerialized] internal int mStepId = 0;
		[NonSerialized] internal List<string> mComments = new List<string>();

		public int StepId { get { return mStepId; } private set { mStepId = value; } }

		protected static ConcurrentDictionary<string, Type> mSTEPTypes = new ConcurrentDictionary<string, Type>();
		protected static ConcurrentDictionary<string, ConstructorInfo> mConstructors = new ConcurrentDictionary<string, ConstructorInfo>();

		internal STEPEntity() { initialize(); }
		public virtual string StepClassName { get { return this.GetType().Name; } }

		protected virtual void initialize() { }
		public string StringSTEP()
		{
			string str = BuildStringSTEP();
			if (string.IsNullOrEmpty(str))
				return "";
			return StepLinePrefix() + str +  StepLineSuffix();
		}
		protected string StepLinePrefix()
		{
			string comment = "";
			if (mComments.Count > 0)
			{
				foreach (string c in mComments)
					comment += "/* " + c + " */\r\n";
			}
			return comment + (mStepId > 0 ? "#" + StepId + "= " : "") + StepClassName.ToUpper() + "(";
		}
		protected string StepLineSuffix()
		{
			return ");";
		}
		protected void WriteStepLineWorkerPrefix(TextWriter textWriter)
		{
			if (mComments.Count > 0)
			{
				foreach (string c in mComments)
					textWriter.WriteLine("/* " + c + " */)");
			}
			textWriter.Write("#" + StepId + "= " + StepClassName.ToUpper() + "(");
		}
		protected void WriteStepLineWorkerSuffix(TextWriter textWriter)
		{
			textWriter.WriteLine(");");
		}
		public string STEPSerialization()
		{
			string attributesString = BuildStringSTEP();
			if (string.IsNullOrEmpty(attributesString))
				return "";
			return StepClassName.ToUpper() + "(" + attributesString + ")"; 
		}
		public override string ToString() { return StringSTEP(); }
		protected abstract string BuildStringSTEP();// { return ""; } 
		public void  AddComment(string comment) { mComments.Add(comment); }

		
		internal static Type GetType(string classNameIfc, string nameSpace)
		{
			if (string.IsNullOrEmpty(classNameIfc))
				return null;
			Type type = null;
			string name = classNameIfc;
			string[] fields = classNameIfc.Split(".".ToCharArray());
			if (fields != null && fields.Length > 0)
				name = fields[0];
			if (!mSTEPTypes.TryGetValue(name, out type))
			{
				type = Type.GetType("GeometryGym." + nameSpace + "." + name, false, true);
				if (type != null)
					mSTEPTypes[name] = type;
			}
			return type;
		}
	}
}
