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
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcKerb : IfcBuiltElement
	{
		private bool mMountable = false; //: IfcBoolean;
		public bool Mountable { get { return mMountable; } set { mMountable = value; } }

		public IfcKerb() : base() { }
		public IfcKerb(DatabaseIfc db, bool mountable) : base(db) { Mountable = mountable; }
		public IfcKerb(DatabaseIfc db, IfcKerb kerb, DuplicateOptions options) : base(db, kerb, options) { Mountable = kerb.Mountable; }
		public IfcKerb(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcKerbType : IfcBuiltElementType
	{
		private bool mMountable = false; //: IfcBoolean;
		public bool Mountable { get { return mMountable; } set { mMountable = value; } }

		public IfcKerbType() : base() { }
		public IfcKerbType(DatabaseIfc db, IfcKerbType kerbType, DuplicateOptions options) : base(db, kerbType, options) { Mountable = kerbType.Mountable; }
		public IfcKerbType(DatabaseIfc db, string name, bool mountable)
			: base(db, name) { Mountable = mountable; }
	}
}
