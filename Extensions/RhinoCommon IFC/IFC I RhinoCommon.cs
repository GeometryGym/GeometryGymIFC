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
	public partial class IfcIndexedPolyCurve
	{
		internal static IfcIndexedPolyCurve Convert(DatabaseIfc db, PolyCurve polycurve, bool twoD)
		{
			double tol = db.Tolerance;
			Curve[] segments = polycurve.Explode();
			PolyCurve pc = new PolyCurve();
			foreach (Curve s in segments)
			{
				if (s.IsLinear(tol))
					pc.Append(new Line(s.PointAtStart, s.PointAtEnd));
				else
				{
					Arc a = Arc.Unset;
					if (s.TryGetArc(out a, tol))
						pc.Append(a);
					else
						return null;
				}
			}
			List<IfcSegmentIndexSelect> segs = new List<IfcSegmentIndexSelect>();
			IfcCartesianPointList cpl = null;
			bool closed = pc.PointAtStart.DistanceTo(pc.PointAtEnd) < tol;
			if (twoD)
			{
				Point2d pt = new Point2d(pc.PointAtStart.X, pc.PointAtStart.Y);
				int pcounter = 1;
				List<Point2d> pts = new List<Point2d>();
				pts.Add(pt);
				IfcLineIndex li = null;
				for (int icounter = 0; icounter < pc.SegmentCount; icounter++)
				{
					Curve c = pc.SegmentCurve(icounter);
					if (c.IsLinear(tol) && c.PointAtStart.DistanceTo(c.PointAtEnd) < tol)
						continue;
					if (c.IsLinear(tol))
					{
						if (closed && icounter + 1 == segments.Length)
						{
							if (li != null)
								li.mIndices.Add(1);
							else
								li = new IfcLineIndex(pcounter, 1);
						}
						else
						{
							pts.Add(new Point2d(c.PointAtEnd.X, c.PointAtEnd.Y));
							if (li != null)
								li.mIndices.Add(++pcounter);
							else
								li = new IfcLineIndex(pcounter++, pcounter);
						}
					}
					else
					{
						if (li != null)
						{
							segs.Add(li);
							li = null;
						}
						Point3d tp = c.PointAt(c.Domain.Mid);
						pts.Add(new Point2d(tp.X, tp.Y));
						if (closed && icounter + 1 == segments.Length)
							segs.Add(new IfcArcIndex(pcounter++, pcounter++, 1));
						else
						{
							pts.Add(new Point2d(c.PointAtEnd.X, c.PointAtEnd.Y));
							segs.Add(new IfcArcIndex(pcounter++, pcounter++, pcounter));
						}
					}
				}
				if (li != null)
					segs.Add(li);
				cpl = new IfcCartesianPointList2D(db, pts.ToArray());
			}
			else
			{
				Point3d pt = pc.PointAtStart;
				int pcounter = 1;
				List<Point3d> pts = new List<Point3d>();
				pts.Add(pt);
				List<IfcSegmentIndexSelect> sis = new List<IfcSegmentIndexSelect>(segments.Length);
				IfcLineIndex li = null;
				for (int icounter = 0; icounter < pc.SegmentCount; icounter++)
				{
					Curve c = pc.SegmentCurve(icounter);
					if (c.IsLinear(tol) && c.PointAtStart.DistanceTo(c.PointAtEnd) < tol)
						continue;
					if (c.IsLinear(tol))
					{
						if (closed && icounter + 1 == segments.Length)
						{
							if (li != null)
								li.mIndices.Add(0);
							else
								li = new IfcLineIndex(pcounter, 0);
						}
						else
						{
							pts.Add(c.PointAtEnd);
							if (li != null)
								li.mIndices.Add(++pcounter);
							else
								li = new IfcLineIndex(pcounter++, pcounter);
						}
					}
					else
					{
						if (li != null)
						{
							segs.Add(li);
							li = null;
						}
						pts.Add(c.PointAt(c.Domain.Mid));
						if (closed && icounter + 1 == segments.Length)
							segs.Add(new IfcArcIndex(pcounter++, pcounter, 0));
						else
						{
							pts.Add(c.PointAtEnd);
							segs.Add(new IfcArcIndex(pcounter++, pcounter++, pcounter));
						}
					}
				}
				if (li != null)
					segs.Add(li);
				cpl = new IfcCartesianPointList3D(db, pts.ToArray());
			}
			return new IfcIndexedPolyCurve(cpl, segs) { };
		}
	}
}
