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
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO; 

using Rhino.Geometry;

namespace GeometryGym.Ifc
{
	public partial class IfcAlignmentHorizontal : IfcLinearElement
	{
		internal Plane planeAtLength(double length, double tol)
		{
			double distAlong = 0;
			List<IfcAlignmentHorizontalSegment> segments = HorizontalSegments.ToList();
			for (int icounter = 0; icounter < segments.Count; icounter++)
			{
				IfcAlignmentHorizontalSegment segment = segments[icounter];
				if (distAlong + segment.SegmentLength + tol > length)
				{
					Plane plane = segment.PlaneAtLength(length - distAlong, tol);
					if (plane.IsValid)
						return plane;

					return Plane.Unset;
				}
				distAlong += segment.SegmentLength;
			}
			return Plane.Unset;
		}
	}
	public partial class IfcAlignmentHorizontalSegment : IfcAlignmentParameterSegment
	{
		public Vector2d StartTangent2d()
		{
			Tuple<double, double> startTangent = StartTangent();
			return new Vector2d(startTangent.Item1, startTangent.Item2);
		}
		internal Vector3d StartTangent3d() { Tuple<double, double> startTangent = StartTangent(); return new Vector3d(startTangent.Item1, startTangent.Item2, 0); }
		public Plane Plane()
		{
			Point3d origin = StartPoint.Location;
			Vector3d startTangent = StartTangent3d();
			Vector3d yAxis = Vector3d.CrossProduct(Vector3d.ZAxis, startTangent);
			return new Plane(origin, startTangent, yAxis);
		}
		public Plane PlaneAtLength(double length, double tol)
		{
			Plane plane = Plane();
			if (mPredefinedType == IfcAlignmentHorizontalSegmentTypeEnum.LINE)
			{
				plane.Origin = plane.Origin + plane.XAxis * length;
				return plane;
			}
			else if (mPredefinedType == IfcAlignmentHorizontalSegmentTypeEnum.CIRCULARARC)
			{
				Point3d centre = plane.Origin + plane.YAxis * StartRadiusOfCurvature;
				double angle = length / Math.Abs(StartRadiusOfCurvature);
				plane.Rotate(angle, new Vector3d(0, 0, StartRadiusOfCurvature > 0 ? 1 : -1), centre);
				return plane;
			}
			else if (mPredefinedType == IfcAlignmentHorizontalSegmentTypeEnum.CLOTHOID)
			{

			}
			throw new NotImplementedException("Plane at length for " + PredefinedType + " not implemented yet!");
		}
	}
	public partial class IfcAlignmentVerticalSegment : IfcAlignmentParameterSegment
	{
		public double computeHeight(double distAlong)
		{
			if(PredefinedType == IfcAlignmentVerticalSegmentTypeEnum.CONSTANTGRADIENT)
				return StartHeight + StartGradient * (distAlong - StartDistAlong);
			throw new NotImplementedException("Computation of height for " + PredefinedType + " not implemented yet!");
		}
	}
	public partial class IfcAxis1Placement : IfcPlacement
	{
		internal Vector3d AxisVector { get { return (mAxis > 0 ? Axis.Vector3d : Vector3d.XAxis); } }
	}
	
	public partial class IfcAxis2Placement2D : IfcPlacement, IfcAxis2Placement
	{
		internal Vector3d DirectionVector { get { return (mRefDirection != null ? RefDirection.Vector3d : Vector3d.XAxis); } }

		internal IfcAxis2Placement2D(DatabaseIfc db, Point2d position, Vector2d dir) : base(db)
		{
			Location = new IfcCartesianPoint(db, position);
			if (dir.Length > 0 && new Vector3d(dir.X, dir.Y, 0).IsParallelTo(Vector3d.XAxis, Math.PI / 1800) != 1)
				RefDirection = new IfcDirection(db, dir);
		}
	
	}
	public partial class IfcAxis2Placement3D
	{
		public IfcAxis2Placement3D(DatabaseIfc db, Plane plane) : base(db)
		{
			Location = new IfcCartesianPoint(db, plane.Origin);
			double angTol = Math.PI / 1800;
			if (plane.ZAxis.IsParallelTo(Vector3d.ZAxis, angTol) != 1)
			{
				Axis = IfcDirection.convert(db, plane.ZAxis); 
				RefDirection = IfcDirection.convert(db, plane.XAxis);
			}
			else if (plane.XAxis.IsParallelTo(Vector3d.XAxis, angTol) != 1)
			{
				RefDirection = IfcDirection.convert(db, plane.XAxis);
				Axis = db.Factory.ZAxis;
			}
		}
	}
}
