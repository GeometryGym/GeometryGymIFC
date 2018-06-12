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
	public partial class IfcStructuralLoadLinearForce
	{
		public Vector3d LinearForce
		{
			get { return new Vector3d(mLinearForceX, mLinearForceY, mLinearForceZ); }
			set
			{
				if (value.IsValid)
				{
					mLinearForceX = value.X;
					mLinearForceY = value.Y;
					mLinearForceZ = value.Z;
				}
				else
					mLinearForceX = mLinearForceY = mLinearForceZ = 0;
			}
		}
		public Vector3d LinearMoment
		{
			get { return new Vector3d(mLinearMomentX, mLinearMomentY, mLinearMomentZ); }
			set
			{
				if (value.IsValid)
				{
					mLinearMomentX = value.X;
					mLinearMomentY = value.Y;
					mLinearMomentZ = value.Z;
				}
				else
					mLinearMomentX = mLinearMomentY = mLinearMomentZ = 0;
			}
		}
	}

	public partial class IfcStructuralLoadPlanarForce
	{
		public Vector3d PlanarForce
		{
			get { return new Vector3d(mPlanarForceX, mPlanarForceY, mPlanarForceZ); }
			set
			{
				if (value.IsValid)
				{
					mPlanarForceX = value.X;
					mPlanarForceY = value.Y;
					mPlanarForceZ = value.Z;
				}
				else
					mPlanarForceX = mPlanarForceY = mPlanarForceZ = 0;
			}
		}
	}
	public partial class IfcStructuralLoadSingleDisplacement
	{
		public Vector3d Displacement
		{
			get { return new Vector3d(mDisplacementX, mDisplacementY, mDisplacementZ); }
			set
			{
				if (value.IsValid)
				{
					mDisplacementX = value.X;
					mDisplacementY = value.Y;
					mDisplacementZ = value.Z;
				}
				else
					mDisplacementX = mDisplacementY = mDisplacementZ = 0;
			}
		}
		public Vector3d RotationalDisplacement
		{
			get { return new Vector3d(mRotationalDisplacementRX, mRotationalDisplacementRY, mRotationalDisplacementRZ); }
			set
			{
				if (value.IsValid)
				{
					mRotationalDisplacementRX = value.X;
					mRotationalDisplacementRY = value.Y;
					mRotationalDisplacementRZ = value.Z;
				}
				else
					mRotationalDisplacementRX = mRotationalDisplacementRY = mRotationalDisplacementRZ = 0;
			}
		}
	}
	public partial class IfcStructuralLoadSingleForce
	{
		public Vector3d Force
		{
			get { return new Vector3d(mForceX, mForceY, mForceZ); }
			set
			{
				if (value.IsValid)
				{
					mForceX = value.X;
					mForceY = value.Y;
					mForceZ = value.Z;
				}
				else
					mForceX = mForceY = mForceZ = 0;
			}
		}
		public Vector3d Moment
		{
			get { return new Vector3d(mMomentX, mMomentY, mMomentZ); }
			set
			{
				if (value.IsValid)
				{
					mMomentX = value.X;
					mMomentY = value.Y;
					mMomentZ = value.Z;
				}
				else
					mMomentX = mMomentY = mMomentZ = 0;
			}
		}
	}
	public partial class IfcSurfaceCurve : IfcCurve //IFC4 Add2
	{
		public override Curve Curve { get {  throw new NotImplementedException(); } }
	}
	public abstract partial class IfcSweptAreaSolid : IfcSolidModel  /*ABSTRACT SUPERTYPE OF (ONEOF (IfcExtrudedAreaSolid, IfcFixedReferenceSweptAreaSolid ,IfcRevolvedAreaSolid ,IfcSurfaceCurveSweptAreaSolid))*/
	{
		internal Transform PositionTransform { get { return (mPosition == 0 ? Transform.Identity : Position.Transform); } }
	}
}
