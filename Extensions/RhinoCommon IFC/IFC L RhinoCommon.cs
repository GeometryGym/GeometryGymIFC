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
		public override Curve Curve(double tol) { return new LineCurve(Line()); }
		public Line Line()
		{
			Point3d pt = Pnt.Location;
			return new Line(pt, pt + Dir.Vector);
		}
	}
	public partial class IfcLinearPlacement : IfcObjectPlacement
	{
		public override Transform Transform()
		{
			double tol = mDatabase == null ? 1e-5 : mDatabase.Tolerance;
			IfcDistanceExpression distanceExpression = Distance;
			IfcCurve curve = PlacementMeasuredAlong;
			Plane plane = Plane.Unset;
			if (curve is IfcAlignmentCurve alignmentCurve)
				plane = alignmentCurve.planeAt(distanceExpression, false, tol);

			if (plane.IsValid)
			{
				IfcOrientationExpression orientationExpression = Orientation;
				if (orientationExpression != null)
				{
					Vector3d x = orientationExpression.LateralAxisDirection.Vector3d, z = orientationExpression.VerticalAxisDirection.Vector3d;
					Vector3d y = Vector3d.CrossProduct(z, x);
					plane = new Plane(plane.Origin, x, y);
				}
				return Rhino.Geometry.Transform.ChangeBasis(plane, Plane.WorldXY);
			}

			if (CartesianPosition != null)
				return CartesianPosition.Transform();
			throw new Exception("Linear Placement Transform not supported yet!");
		}
	}
	public partial class IfcLocalPlacement : IfcObjectPlacement
	{
		public override Transform Transform()
		{
			IfcObjectPlacement placementRelTo = PlacementRelTo;
			IfcAxis2Placement relativePlacement = RelativePlacement;
			bool identityRelative = relativePlacement == null;
			if(relativePlacement != null)
			{
				if (mDatabase != null && relativePlacement.Plane.IsValid)
				{
					Plane plane = relativePlacement.Plane;
					if (plane.Origin.DistanceTo(Point3d.Origin) < mDatabase.Tolerance && plane.XAxis.IsParallelTo(Vector3d.XAxis) == 1 && plane.YAxis.IsParallelTo(Vector3d.YAxis) == 1)
						identityRelative = true;
				}
				else
					identityRelative = relativePlacement.IsXYPlane;
			}
			if (placementRelTo == null || placementRelTo.isXYPlane)
			{
				if (identityRelative)
					return Rhino.Geometry.Transform.Identity;
				else
					return Rhino.Geometry.Transform.ChangeBasis(relativePlacement.Plane,Plane.WorldXY);
			}
			if (identityRelative)
				return placementRelTo.Transform();
			return placementRelTo.Transform() * Rhino.Geometry.Transform.ChangeBasis(relativePlacement.Plane, Plane.WorldXY);
		}
	} 
}
