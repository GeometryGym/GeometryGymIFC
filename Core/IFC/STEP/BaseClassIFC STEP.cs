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
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;

using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class BaseClassIfc : STEPEntity, IBaseClassIfc
	{
		internal abstract void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary);
		protected override string BuildStringSTEP()
		{
			return BuildStringSTEP(mDatabase == null ? ReleaseVersion.IFC4A2 : mDatabase.Release);
		}
		protected abstract string BuildStringSTEP(ReleaseVersion release);

		protected string StepOptionalLengthString(double length)
		{
			if (double.IsNaN(length) || double.IsInfinity(length))
				return "$";
			return formatLength(length);
		}

		public string StringSTEP(ReleaseVersion release)
		{
			string str = BuildStringSTEP(release);
			if (string.IsNullOrEmpty(str))
				return "";
			return StepLinePrefix() + str + StepLineSuffix();
		}

		internal virtual void WriteStepLine(TextWriter textWriter, ReleaseVersion release)
		{
			try
			{
				string str = StringSTEP(release);
				if (!string.IsNullOrEmpty(str))
					textWriter.WriteLine(str);
			}
			catch (Exception) { }
		}
	}
	public partial class StiffnessSelect<T>
	{
		public override string ToString() { return (mStiffness == null ? "IFCBOOLEAN(" + ParserSTEP.BoolToString(mRigid) + ")" : mStiffness.ToString()); }
		protected void ParseValue(string str, ReleaseVersion version)
		{
			if (str.StartsWith("IFCBOOL"))
				mRigid = ((IfcBoolean)ParserIfc.parseSimpleValue(str)).Boolean;
			if (str.StartsWith("IFC"))
				mStiffness = ((T)ParserIfc.parseDerivedMeasureValue(str));
			if (str.StartsWith("."))
				mRigid = ParserSTEP.ParseBool(str);
			double d = ParserSTEP.ParseDouble(str), tol = 1e-9;
			if (!double.IsNaN(d))
			{
				if (version < ReleaseVersion.IFC4)
				{
					if (Math.Abs(d + 1) < tol)
					{
						mStiffness = new T();
						mStiffness.Measure = -1;
						mRigid = true;
					}
					else if (Math.Abs(d) < tol)
					{
						mStiffness = new T();
						mStiffness.Measure = 0;
						mRigid = false;
					}
				}
				else
				{
					mStiffness = new T();
					mStiffness.Measure = d;
				}
			}
		}
	}
}
