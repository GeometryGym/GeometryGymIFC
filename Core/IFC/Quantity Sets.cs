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
		public Qto_AirTerminalBaseQuantities(IfcAirTerminal instance) : base(instance) { }
		public Qto_AirTerminalBaseQuantities(IfcAirTerminalType type) : base(type) { }
	}
	public partial class Qto_BuildingStoreyBaseQuantities : IfcElementQuantity
	{
		public double GrossHeight { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "GrossHeight", value).mIndex); } }
		public double NetHeight { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "NetHeight", value).mIndex); } }
		public double GrossPerimeter { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "GrossPerimeter", value).mIndex); } }
		public double GrossFloorArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "GrossFloorArea", value).mIndex); } }
		public double NetFloorArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "NetFloorArea", value).mIndex); } }
		public double GrossVolume { set { mQuantities.Add(new IfcQuantityVolume(mDatabase, "GrossVolume", value).mIndex); } }
		public double NetVolume { set { mQuantities.Add(new IfcQuantityVolume(mDatabase, "NetVolume", value).mIndex); } }
		public Qto_BuildingStoreyBaseQuantities(IfcBuildingStorey instance) : base(instance) { }
	}
	public partial class Qto_CoveringBaseQuantities : IfcElementQuantity
	{
		public double Width { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Width", value).mIndex); } }
		public double Area { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "Area", value).mIndex); } }
		public double NetArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "NetArea", value).mIndex); } }
		public Qto_CoveringBaseQuantities(IfcCovering instance) : base(instance) { }
		public Qto_CoveringBaseQuantities(IfcCoveringType type) : base(type) { }
	}
	public partial class Qto_DoorBaseQuantities : IfcElementQuantity
	{
		public double Width { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Width", value).mIndex); } }
		public double GrossArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "GrossArea", value).mIndex); } }
		public double Perimeter { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Perimeter", value).mIndex); } }
		public double Area { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "Area", value).mIndex); } }
		public Qto_DoorBaseQuantities(IfcDoor instance) : base(instance) { }
		public Qto_DoorBaseQuantities(IfcDoorType type) : base(type) { }
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
		public Qto_FootingBaseQuantities(IfcFooting instance) : base(instance) { }
		public Qto_FootingBaseQuantities(IfcFootingType type) : base(type) { }
	}
	public partial class Qto_ReinforcingElementBaseQuantities : IfcElementQuantity
	{
		public double Count { set { mQuantities.Add(new IfcQuantityCount(mDatabase, "Count", value).mIndex); } }
		public double Length { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Length", value).mIndex); } }
		public double Weight { set { mQuantities.Add(new IfcQuantityWeight(mDatabase, "Weight", value).mIndex); } }
		public Qto_ReinforcingElementBaseQuantities(IfcReinforcingElement instance) : base(instance) { }
	}
	public partial class Qto_SpaceBaseQuantities : IfcElementQuantity
	{
		public double Height { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Height", value).mIndex); } }
		public double FinishCeilingHeight { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "FinishCeilingHeight", value).mIndex); } }
		public double FinishFloorHeight { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "FinishFloorHeight", value).mIndex); } }
		public double GrossPerimeter { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "GrossPerimeter", value).mIndex); } }
		public double NetPerimeter { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "NetPerimeter", value).mIndex); } }
		public double GrossFloorArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "GrossFloorArea", value).mIndex); } }
		public double NetFloorArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "NetFloorArea", value).mIndex); } }
		public double GrossWallArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "GrossWallArea", value).mIndex); } }
		public double NetWallArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "NetWallArea", value).mIndex); } }
		public double GrossCeilingArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "GrossCeilingArea", value).mIndex); } }
		public double NetCeilingArea { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "NetCeilingArea", value).mIndex); } }
		public double GrossVolume { set { mQuantities.Add(new IfcQuantityVolume(mDatabase, "GrossVolume", value).mIndex); } }
		public double NetVolume { set { mQuantities.Add(new IfcQuantityVolume(mDatabase, "NetVolume", value).mIndex); } }
		public Qto_SpaceBaseQuantities(IfcSpace instance) : base(instance) { }
		public Qto_SpaceBaseQuantities(IfcSpaceType type) : base(type) { }
	}
	public partial class Qto_UnitaryEquipmentBaseQuantities : IfcElementQuantity
	{
		public double GrossWeight { set { mQuantities.Add(new IfcQuantityWeight(mDatabase, "GrossWeight", value).mIndex); } }
		public Qto_UnitaryEquipmentBaseQuantities(IfcUnitaryEquipment instance) : base(instance) { }
		public Qto_UnitaryEquipmentBaseQuantities(IfcUnitaryEquipmentType type) : base(type) { }
	}
	public partial class Qto_WindowBaseQuantities : IfcElementQuantity
	{
		public double Width { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Width", value).mIndex); } }
		public double Height { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Height", value).mIndex); } }
		public double Perimeter { set { mQuantities.Add(new IfcQuantityLength(mDatabase, "Perimeter", value).mIndex); } }
		public double Area { set { mQuantities.Add(new IfcQuantityArea(mDatabase, "Area", value).mIndex); } }
		public Qto_WindowBaseQuantities(IfcWindow instance) : base(instance) { }
		public Qto_WindowBaseQuantities(IfcWindowType type) : base(type) { }
	}
}
