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
	public partial class Qto_AirTerminalBaseQuantities : IfcElementQuantity
	{
		public double GrossWeight { set { mQuantities.Add(new IfcQuantityWeight(mDatabase, "GrossWeight", value).mIndex); } }
		public double Perimeter { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Perimeter", value).mIndex); } }
		public double TotalSurfaceArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "TotalSurfaceArea", value).mIndex); } }
		public Qto_AirTerminalBaseQuantities(IfcAirTerminal instance) : base(instance.mDatabase, "Qto_AirTerminalBaseQuantities") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Qto_AirTerminalBaseQuantities(IfcAirTerminalType type) : base(type.mDatabase, "Qto_AirTerminalBaseQuantities") { Description = type.Name; type.AddPropertySet(this); }
	}
	public partial class Qto_FootingBaseQuantities : IfcElementQuantity
	{
		public double Length { set { mQuantities.Add(new IfcQuantityLength(mDatabase,"Length",value).mIndex); } }
		public double Width { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Width", value).mIndex); } }
		public double Height { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Height", value).mIndex); } }
		public double CrossSectionArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "CrossSectionArea", value).mIndex); } }
		public double OuterSurfaceArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "OuterSurfaceArea", value).mIndex); } }
		public double GrossSurfaceArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "GrossSurfaceArea", value).mIndex); } }
		public double GrossVolume { set { mQuantities.Add(new IfcQuantityVolume(mDatabase, "GrossVolume", value).mIndex); } }
		public double NetVolume { set { mQuantities.Add(new IfcQuantityVolume(mDatabase, "NetVolume", value).mIndex); } }
		public double GrossWeight { set { mQuantities.Add(new IfcQuantityWeight(mDatabase, "GrossWeight", value).mIndex); } }
		public double NetWeight { set { mQuantities.Add(new IfcQuantityWeight(mDatabase, "NetWeight", value).mIndex); } }
		public Qto_FootingBaseQuantities(IfcFooting instance) : base(instance.mDatabase, "Qto_FootingBaseQuantities") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Qto_FootingBaseQuantities(IfcFootingType type) : base(type.mDatabase, "Qto_FootingBaseQuantities") { Description = type.Name; type.AddPropertySet(this); }
	}
	public partial class Qto_WindowBaseQuantities : IfcElementQuantity
	{
		public double Width { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Width", value).mIndex); } }
		public double Height { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Height", value).mIndex); } }
		public double Perimeter { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Perimeter", value).mIndex); } }
		public double Area { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "CrossSectionArea", value).mIndex); } }
		public Qto_WindowBaseQuantities(IfcWindow instance) : base(instance.mDatabase, "Qto_WindowBaseQuantities") { Description = instance.Name; DefinesOccurrence.Assign(instance); }
		public Qto_WindowBaseQuantities(IfcWindowType type) : base(type.mDatabase, "Qto_WindowBaseQuantities") { Description = type.Name; type.AddPropertySet(this); }
	}
}
