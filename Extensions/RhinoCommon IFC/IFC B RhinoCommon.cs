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
	public abstract partial class IfcBoundedCurve
	{
		public override Curve Curve
		{
			get
			{
				throw new NotImplementedException();
			}
		}
		
	}
	public partial class IfcBSplineCurve
	{
		protected IfcBSplineCurve(DatabaseIfc m, NurbsCurve nonRationalCurve, bool twoD)
			: this(m, nonRationalCurve.Degree, IfcBSplineCurveForm.UNSPECIFIED)
		{
			int ilast = nonRationalCurve.Points.Count - (nonRationalCurve.IsPeriodic ? mDegree : 0);
			if (twoD)
			{
				for (int icounter = 0; icounter < ilast; icounter++)
				{
					Point3d p3 = nonRationalCurve.Points[icounter].Location;
					mControlPointsList.Add(new IfcCartesianPoint(m, new Point2d(p3.X, p3.Y)).mIndex);
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
					mControlPointsList.Add(new IfcCartesianPoint(m, nonRationalCurve.Points[icounter].Location).mIndex);
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
		public IfcBSplineCurveWithKnots(DatabaseIfc db, int degree, List<Point3d> controlPoints, IfcBSplineCurveForm form, List<int> multiplicities, List<double> knots, IfcKnotType knotSpec) :
			base(degree, controlPoints.ConvertAll(x=>new IfcCartesianPoint(db, x)), form)
		{
			mMultiplicities.AddRange(multiplicities);
			mKnots.AddRange(knots);
		}
		internal IfcBSplineCurveWithKnots(DatabaseIfc m, NurbsCurve nc, bool twoD)
			: base(m, nc, twoD)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned)
				throw new Exception("Invalid Model View for IfcRationalBSplineCurveWithKnots : " + mDatabase.ModelView.ToString());
			ClosedCurve = nc.IsClosed ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE;
			adoptKnotsAndMultiplicities(nc);
		}
		public IfcBSplineCurveWithKnots(DatabaseIfc m, int degree, List<Point2d> controlPoints, IfcBSplineCurveForm form, List<int> multiplicities, List<double> knots, IfcKnotType knotSpec) :
			base(degree, controlPoints.ConvertAll(x => new IfcCartesianPoint(m, x)), form)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned)
				throw new Exception("Invalid Model View for IfcRationalBSplineCurveWithKnots : " + mDatabase.ModelView.ToString());
			mMultiplicities.AddRange(multiplicities);
			mKnots.AddRange(knots);
		}
		private void adoptKnotsAndMultiplicities(NurbsCurve nc)
		{ 
			double tol = (nc.Knots[nc.Knots.Count - 1] - nc.Knots[0]) / Math.Max(1000, nc.Knots.Count) / 1000;
			if (nc.IsPeriodic)
			{
				int kc = 1;
				if (nc.Knots[1] - nc.Knots[0] < tol)
					kc = 2;
				else
				{
					mKnots.Add(nc.Knots[0] - (nc.Knots[1] - nc.Knots[0]));
					mMultiplicities.Add(1);
				}
				double knot = nc.Knots[0];
				for (int icounter = 1; icounter < nc.Knots.Count; icounter++)
				{
					double t = nc.Knots[icounter];
					if ((t - knot) > tol)
					{
						mKnots.Add(knot);
						mMultiplicities.Add(kc);
						knot = t;
						kc = 1;
					}
					else
						kc++;
				}
				mKnots.Add(knot);
				if (kc > 1)
					mMultiplicities.Add(kc + 1);
				else
				{
					mMultiplicities.Add(1);
					mKnots.Add(knot + (knot - nc.Knots[nc.Knots.Count - 2]));
					mMultiplicities.Add(1);
				}
			}
			else
			{
				int kc = 2;
				double knot = nc.Knots[0];
				for (int icounter = 1; icounter < nc.Knots.Count; icounter++)
				{
					double t = nc.Knots[icounter];
					if ((t - knot) > tol)
					{
						mKnots.Add(knot);
						mMultiplicities.Add(kc);
						knot = t;
						kc = 1;
					}
					else
						kc++;
				}
				mKnots.Add(knot);
				mMultiplicities.Add(kc + 1);
			}
		}
	}

}
