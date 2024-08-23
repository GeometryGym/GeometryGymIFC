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
	public partial class IfcBSplineCurve
	{
		protected IfcBSplineCurve(DatabaseIfc db, NurbsCurve nonRationalCurve, bool twoD)
			: this(db, nonRationalCurve.Degree)
		{
			int ilast = nonRationalCurve.Points.Count - (nonRationalCurve.IsPeriodic ? mDegree : 0);
			if (twoD)
			{
				for (int icounter = 0; icounter < ilast; icounter++)
				{
					Point3d p3 = nonRationalCurve.Points[icounter].Location;
					mControlPointsList.Add(new IfcCartesianPoint(db, new Point2d(p3.X, p3.Y)));
				}
				if (nonRationalCurve.IsPeriodic)
				{
					for (int icounter = 0; icounter < mDegree; icounter++)
						mControlPointsList.Add(mControlPointsList[icounter]);
				}
			}
			else
			{
				for (int icounter = 0; icounter < ilast; icounter++)
					mControlPointsList.Add(new IfcCartesianPoint(db, nonRationalCurve.Points[icounter].Location));
				if (nonRationalCurve.IsPeriodic)
				{
					for (int icounter = 0; icounter < mDegree; icounter++)
						mControlPointsList.Add(mControlPointsList[icounter]);
				}
			}
		}
	}
	public partial class IfcBSplineCurveWithKnots
	{
		public IfcBSplineCurveWithKnots(DatabaseIfc db, int degree, List<Point3d> controlPoints, List<int> multiplicities, List<double> knots, IfcKnotType knotSpec) :
			base(degree, controlPoints.ConvertAll(x=>new IfcCartesianPoint(db, x)))
		{
			mKnotMultiplicities.AddRange(multiplicities);
			mKnots.AddRange(knots);
		}
		internal IfcBSplineCurveWithKnots(DatabaseIfc db, NurbsCurve nc, bool twoD)
			: base(db, nc, twoD)
		{
			if (mDatabase.mModelView == ModelView.Ifc4Reference)
				throw new Exception("Invalid Model View for IfcBSplineCurveWithKnots : " + mDatabase.ModelView.ToString());
			ClosedCurve = nc.IsClosed ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE;
			adoptKnotsAndMultiplicities(nc);
		}
		public IfcBSplineCurveWithKnots(DatabaseIfc db, int degree, List<Point2d> controlPoints, IEnumerable<int> multiplicities, IEnumerable<double> knots, IfcKnotType knotSpec) :
			base(degree, controlPoints.ConvertAll(x => new IfcCartesianPoint(db, x)))
		{
			if (mDatabase.mModelView != ModelView.Ifc4Reference)
				throw new Exception("Invalid Model View for IfcBSplineCurveWithKnots : " + mDatabase.ModelView.ToString());
			mKnotMultiplicities.AddRange(multiplicities);
			mKnots.AddRange(knots);
		}
		private void adoptKnotsAndMultiplicities(NurbsCurve nc)
		{
			var knotList = nc.Knots;
			int count = knotList.Count;
			if (nc.IsPeriodic)
			{
				double knot = nc.Knots[0];
				mKnots.Add(knot - (nc.Knots[1] - knot));
				mKnotMultiplicities.Add(1);
				for(int u = 0; u < count; u++)
				{
					int multiplicity = knotList.KnotMultiplicity(u);
					mKnots.Add(knotList[u]);
					mKnotMultiplicities.Add(multiplicity);
					u += multiplicity - 1;
				}
				knot = nc.Knots[count - 1];
				mKnots.Add(knot + (knot - nc.Knots[count - 2]));
				mKnotMultiplicities.Add(1);
			}
			else
			{
				for (int u = 0; u < count; u++)
				{
					double knot = knotList[u];
					int multiplicity = knotList.KnotMultiplicity(u);
					bool superfluous = (u == 0 || u + multiplicity == count);
					mKnots.Add(knot);
					mKnotMultiplicities.Add(multiplicity + (superfluous ? 1 : 0));
					u += multiplicity - 1;
				}
			}
		}
	}

}
