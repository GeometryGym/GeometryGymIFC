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
	public partial class IfcTrimmedCurve
	{
		public IfcTrimmedCurve(IfcCartesianPoint s, IfcCartesianPoint e) : base(s.mDatabase)
		{
			BasisCurve = new IfcLine(s, new IfcVector(s.mDatabase,e.Location- s.Location));
			mTrim1 = new IfcTrimmingSelect(s);
			mTrim2 = new IfcTrimmingSelect(e);
			mMasterRepresentation = IfcTrimmingPreference.CARTESIAN;
			mSenseAgreement = true;
		}
		internal IfcTrimmedCurve(DatabaseIfc db, Arc a, bool twoD, IfcCartesianPoint optStrt, out IfcCartesianPoint end) : base(db)
		{
			Point3d o = a.Plane.Origin, s = a.StartPoint, e = a.EndPoint;
			Vector3d x = s - o;
			mSenseAgreement = true;
			if (optStrt == null)
				optStrt = twoD ? new IfcCartesianPoint(db, new Point2d(s.X, s.Y)) : new IfcCartesianPoint(db, s);
			end = twoD ? new IfcCartesianPoint(db, new Point2d(e.X, e.Y)) : new IfcCartesianPoint(db,e);
			double angleFactor = mDatabase.mContext.UnitsInContext.ScaleSI(IfcUnitEnum.PLANEANGLEUNIT);
			if (twoD)
			{
				if (a.Plane.ZAxis.Z < 0)
				{
					mSenseAgreement = false;
					x = e - o;
					IfcAxis2Placement2D ap = new IfcAxis2Placement2D(db, new Point2d(o.X, o.Y), new Vector2d(x.X, x.Y));
					BasisCurve = new IfcCircle(ap, a.Radius);
					mTrim1 = new IfcTrimmingSelect(a.Angle / angleFactor, optStrt);
					mTrim2 = new IfcTrimmingSelect(0, end);
				}
				else
				{
					IfcAxis2Placement2D ap = new IfcAxis2Placement2D(db, new Point2d(o.X, o.Y), new Vector2d(x.X, x.Y));
					BasisCurve = new IfcCircle(ap, a.Radius);
					mTrim1 = new IfcTrimmingSelect(0, optStrt);
					mTrim2 = new IfcTrimmingSelect(a.Angle / angleFactor, end);
				}
			}
			else
			{
				Vector3d y = Vector3d.CrossProduct(a.Plane.ZAxis, x);
				Plane pl = new Plane(o, x, y);
				IfcAxis2Placement3D ap = new IfcAxis2Placement3D(db, pl);
				BasisCurve = new IfcCircle(ap, a.Radius);
				mTrim1 = new IfcTrimmingSelect(0, optStrt);
				mTrim2 = new IfcTrimmingSelect(a.Angle / angleFactor, end);
			}
			mMasterRepresentation = IfcTrimmingPreference.PARAMETER;
		}
	}
}
