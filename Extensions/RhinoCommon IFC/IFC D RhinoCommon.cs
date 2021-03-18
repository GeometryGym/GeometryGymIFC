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
	public partial class IfcDirection : IfcGeometricRepresentationItem
	{
		public Vector3d Vector3d { get { return new Vector3d(mDirectionRatioX, mDirectionRatioY, double.IsNaN(mDirectionRatioZ) ? 0 : mDirectionRatioZ); } }
		public IfcDirection(DatabaseIfc db, Vector3d v) : base(db)
		{
			Vector3d unit = v;
			unit.Unitize();

			mDirectionRatioX = unit.X;
			mDirectionRatioY = unit.Y;
			mDirectionRatioZ = unit.Z;
		}
		public IfcDirection(DatabaseIfc db, Vector2d v) : base(db)
		{
			double len = v.Length;
			mDirectionRatioX = v.X / len;
			mDirectionRatioY = v.Y / len;
			mDirectionRatioZ = double.NaN;
		}

		internal static IfcDirection convert(DatabaseIfc db, Vector3d vector)
		{
			if (double.IsNaN(vector.Z))
				return new IfcDirection(db, vector.X, vector.Y);
			vector.Unitize();
			double tol = 1e-6;
			if (Math.Abs(vector.X - 1) < tol)
				return db.Factory.XAxis;
			if (Math.Abs(vector.Y - 1) < tol)
				return db.Factory.YAxis;
			if (Math.Abs(vector.Z - 1) < tol)
				return db.Factory.ZAxis;
			if (Math.Abs(vector.X + 1) < tol)
				return db.Factory.XAxisNegative;
			if (Math.Abs(vector.Y + 1) < tol)
				return db.Factory.YAxisNegative;
			if (Math.Abs(vector.Z + 1) < tol)
				return db.Factory.ZAxisNegative;
			return new IfcDirection(db, vector);
		}
	}
}
