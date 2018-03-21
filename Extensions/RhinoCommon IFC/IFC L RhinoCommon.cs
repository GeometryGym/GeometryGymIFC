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
using System.Collections;
using System.Text;
using System.Reflection;
using System.IO;

using Rhino.Geometry;

namespace GeometryGym.Ifc
{
	public partial class IfcLine : IfcCurve
	{
		public override Curve Curve => new LineCurve(Line);
		public Line Line
		{
			get
			{
				Point3d pt = Pnt.Location;
				return new Line(pt, pt + Dir.Vector);
			}
		}
	}
	public partial class IfcLocalPlacement : IfcObjectPlacement
	{
		private Transform mtransform = Transform.Unset;
		public override Transform Transform
		{
			get
			{
				if (!mCalculated)
				{
					mCalculated = true;
					IfcObjectPlacement placementRelTo = PlacementRelTo;
					IfcAxis2Placement relativePlacement = RelativePlacement;
					if (placementRelTo == null || placementRelTo.isXYPlane)
					{
						if (relativePlacement == null || relativePlacement.IsXYPlane)
							mtransform = Transform.Identity;
						else
							mtransform = Transform.ChangeBasis(relativePlacement.Plane,Plane.WorldXY);
					}
					else
					{
						if (relativePlacement == null || relativePlacement.IsXYPlane)
							mtransform = placementRelTo.Transform;
						else
							mtransform = placementRelTo.Transform * Transform.ChangeBasis(relativePlacement.Plane, Plane.WorldXY);
					}
				}
				return mtransform;
			}
		}
	} 
}
