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
		public double GrossWeight { set { addQuantity(new IfcQuantityWeight(mDatabase, "GrossWeight", value)); } }
		public double Perimeter { set { addQuantity(new IfcQuantityLength(mDatabase, "Perimeter", value)); } }
		public double TotalSurfaceArea { set { addQuantity(new IfcQuantityArea(mDatabase, "TotalSurfaceArea", value)); } }
		public Qto_AirTerminalBaseQuantities(IfcAirTerminal instance) : base(instance) { }
		public Qto_AirTerminalBaseQuantities(IfcAirTerminalType type) : base(type) { }
	}
	public partial class Qto_BuildingStoreyBaseQuantities : IfcElementQuantity
	{
		public double GrossHeight { set { addQuantity(new IfcQuantityLength(mDatabase, "GrossHeight", value)); } }
		public double NetHeight { set { addQuantity(new IfcQuantityLength(mDatabase, "NetHeight", value)); } }
		public double GrossPerimeter { set { addQuantity(new IfcQuantityLength(mDatabase, "GrossPerimeter", value)); } }
		public double GrossFloorArea { set { addQuantity(new IfcQuantityArea(mDatabase, "GrossFloorArea", value)); } }
		public double NetFloorArea { set { addQuantity(new IfcQuantityArea(mDatabase, "NetFloorArea", value)); } }
		public double GrossVolume { set { addQuantity(new IfcQuantityVolume(mDatabase, "GrossVolume", value)); } }
		public double NetVolume { set { addQuantity(new IfcQuantityVolume(mDatabase, "NetVolume", value)); } }
		public Qto_BuildingStoreyBaseQuantities(IfcBuildingStorey instance) : base(instance) { }
	}
	public partial class Qto_CoveringBaseQuantities : IfcElementQuantity
	{
		public double Width { set { addQuantity(new IfcQuantityLength(mDatabase, "Width", value)); } }
		public double Area { set { addQuantity(new IfcQuantityArea(mDatabase, "Area", value)); } }
		public double NetArea { set { addQuantity(new IfcQuantityArea(mDatabase, "NetArea", value)); } }
		public Qto_CoveringBaseQuantities(IfcCovering instance) : base(instance) { }
		public Qto_CoveringBaseQuantities(IfcCoveringType type) : base(type) { }
	}
	public partial class Qto_DoorBaseQuantities : IfcElementQuantity
	{
		public double Width { set { addQuantity(new IfcQuantityLength(mDatabase, "Width", value)); } }
		public double GrossArea { set { addQuantity(new IfcQuantityArea(mDatabase, "GrossArea", value)); } }
		public double Perimeter { set { addQuantity(new IfcQuantityLength(mDatabase, "Perimeter", value)); } }
		public double Area { set { addQuantity(new IfcQuantityArea(mDatabase, "Area", value)); } }
		public Qto_DoorBaseQuantities(IfcDoor instance) : base(instance) { }
		public Qto_DoorBaseQuantities(IfcDoorType type) : base(type) { }
	}
	public partial class Qto_FootingBaseQuantities : IfcElementQuantity
	{
		public double Length { set { addQuantity(new IfcQuantityLength(mDatabase,"Length",value)); } }
		public double Width { set { addQuantity(new IfcQuantityLength(mDatabase, "Width", value)); } }
		public double Height { set { addQuantity(new IfcQuantityLength(mDatabase, "Height", value)); } }
		public double CrossSectionArea { set { addQuantity(new IfcQuantityArea(mDatabase, "CrossSectionArea", value)); } }
		public double OuterSurfaceArea { set { addQuantity(new IfcQuantityArea(mDatabase, "OuterSurfaceArea", value)); } }
		public double GrossSurfaceArea { set { addQuantity(new IfcQuantityArea(mDatabase, "GrossSurfaceArea", value)); } }
		public double GrossVolume { set { addQuantity(new IfcQuantityVolume(mDatabase, "GrossVolume", value)); } }
		public double NetVolume { set { addQuantity(new IfcQuantityVolume(mDatabase, "NetVolume", value)); } }
		public double GrossWeight { set { addQuantity(new IfcQuantityWeight(mDatabase, "GrossWeight", value)); } }
		public double NetWeight { set { addQuantity(new IfcQuantityWeight(mDatabase, "NetWeight", value)); } }
		public Qto_FootingBaseQuantities(IfcFooting instance) : base(instance) { }
		public Qto_FootingBaseQuantities(IfcFootingType type) : base(type) { }
	}
	public partial class Qto_ReinforcingElementBaseQuantities : IfcElementQuantity
	{
		public double Count { set { addQuantity(new IfcQuantityCount(mDatabase, "Count", value)); } }
		public double Length { set { addQuantity(new IfcQuantityLength(mDatabase, "Length", value)); } }
		public double Weight { set { addQuantity(new IfcQuantityWeight(mDatabase, "Weight", value)); } }
		public Qto_ReinforcingElementBaseQuantities(IfcReinforcingElement instance) : base(instance) { }
	}
	public partial class Qto_SpaceBaseQuantities : IfcElementQuantity
	{
		public double Height { set { addQuantity(new IfcQuantityLength(mDatabase, "Height", value)); } }
		public double FinishCeilingHeight { set { addQuantity(new IfcQuantityLength(mDatabase, "FinishCeilingHeight", value)); } }
		public double FinishFloorHeight { set { addQuantity(new IfcQuantityLength(mDatabase, "FinishFloorHeight", value)); } }
		public double GrossPerimeter { set { addQuantity(new IfcQuantityLength(mDatabase, "GrossPerimeter", value)); } }
		public double NetPerimeter { set { addQuantity(new IfcQuantityLength(mDatabase, "NetPerimeter", value)); } }
		public double GrossFloorArea { set { addQuantity(new IfcQuantityArea(mDatabase, "GrossFloorArea", value)); } }
		public double NetFloorArea { set { addQuantity(new IfcQuantityArea(mDatabase, "NetFloorArea", value)); } }
		public double GrossWallArea { set { addQuantity(new IfcQuantityArea(mDatabase, "GrossWallArea", value)); } }
		public double NetWallArea { set { addQuantity(new IfcQuantityArea(mDatabase, "NetWallArea", value)); } }
		public double GrossCeilingArea { set { addQuantity(new IfcQuantityArea(mDatabase, "GrossCeilingArea", value)); } }
		public double NetCeilingArea { set { addQuantity(new IfcQuantityArea(mDatabase, "NetCeilingArea", value)); } }
		public double GrossVolume { set { addQuantity(new IfcQuantityVolume(mDatabase, "GrossVolume", value)); } }
		public double NetVolume { set { addQuantity(new IfcQuantityVolume(mDatabase, "NetVolume", value)); } }
		public Qto_SpaceBaseQuantities(IfcSpace instance) : base(instance) { }
		public Qto_SpaceBaseQuantities(IfcSpaceType type) : base(type) { }
	}
	public partial class Qto_UnitaryEquipmentBaseQuantities : IfcElementQuantity
	{
		public double GrossWeight { set { addQuantity(new IfcQuantityWeight(mDatabase, "GrossWeight", value)); } }
		public Qto_UnitaryEquipmentBaseQuantities(IfcUnitaryEquipment instance) : base(instance) { }
		public Qto_UnitaryEquipmentBaseQuantities(IfcUnitaryEquipmentType type) : base(type) { }
	}
	public partial class Qto_WindowBaseQuantities : IfcElementQuantity
	{
		public double Width { set { addQuantity(new IfcQuantityLength(mDatabase, "Width", value)); } }
		public double Height { set { addQuantity(new IfcQuantityLength(mDatabase, "Height", value)); } }
		public double Perimeter { set { addQuantity(new IfcQuantityLength(mDatabase, "Perimeter", value)); } }
		public double Area { set { addQuantity(new IfcQuantityArea(mDatabase, "Area", value)); } }
		public Qto_WindowBaseQuantities(IfcWindow instance) : base(instance) { }
		public Qto_WindowBaseQuantities(IfcWindowType type) : base(type) { }
	}
}
