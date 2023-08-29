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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;


namespace GeometryGym.Ifc
{
	public partial class IfcBeam
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcBeamTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcBeamTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcBeamType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcBeamTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcBearing
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			(mPredefinedType == IfcBearingTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcBearingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcBearingType
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
			",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcBearingTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcBlobTexture
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{
			return base.BuildStringSTEP(release) + ",'" + ParserSTEP.Encode(mRasterFormat) + "','" + mRasterCode.ValueString + "'"; 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRasterFormat = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			string field = ParserSTEP.StripField(str, ref pos, len);
			mRasterCode = ParserSTEP.ParseBinaryString(field);
		}
	}
	public partial class IfcBlock
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mXLength) + "," + ParserSTEP.DoubleToString(mYLength) + "," + ParserSTEP.DoubleToString(mZLength); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mXLength = ParserSTEP.StripDouble(str, ref pos, len);
			mYLength = ParserSTEP.StripDouble(str, ref pos, len);
			mZLength = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcBoiler  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcBoilerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcBoilerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcBoilerType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcBoilerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcBooleanResult
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "." + mOperator.ToString() + ".,#" + mFirstOperand.StepId + ",#" + mSecondOperand.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			Enum.TryParse<IfcBooleanOperator>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mOperator);
			mFirstOperand = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcBooleanOperand;
			mSecondOperand = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcBooleanOperand;
		}
	}
	public partial class IfcBorehole
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4X4_DRAFT ? "" :  ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release >= ReleaseVersion.IFC4X4_DRAFT)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcBoreholeTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public abstract partial class IfcBoundaryCondition
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (string.IsNullOrEmpty(mName) ? "$" : "'" + ParserSTEP.Encode(mName) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcBoundaryEdgeCondition
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return base.BuildStringSTEP(release) + "," + (mLinearStiffnessByLengthX == null ? "$" : ParserSTEP.DoubleToString(mLinearStiffnessByLengthX.mStiffness.mValue)) + "," +
					(mLinearStiffnessByLengthY == null ? "$" : ParserSTEP.DoubleToString(mLinearStiffnessByLengthY.mStiffness.mValue)) + "," +
					(mLinearStiffnessByLengthZ == null ? "$" : ParserSTEP.DoubleToString(mLinearStiffnessByLengthZ.mStiffness.mValue)) + "," +
					(mRotationalStiffnessByLengthX == null ? "$" : ParserSTEP.DoubleToString(mRotationalStiffnessByLengthX.mStiffness.mValue)) + "," +
					(mRotationalStiffnessByLengthY == null ? "$" : ParserSTEP.DoubleToString(mRotationalStiffnessByLengthY.mStiffness.mValue)) + "," +
					(mRotationalStiffnessByLengthZ == null ? "$" : ParserSTEP.DoubleToString(mRotationalStiffnessByLengthZ.mStiffness.mValue));
			return base.BuildStringSTEP(release) + "," + (mLinearStiffnessByLengthX == null ? "$" : mLinearStiffnessByLengthX.ToString()) + "," +
				(mLinearStiffnessByLengthY == null ? "$" : mLinearStiffnessByLengthY.ToString()) + "," +
				(mLinearStiffnessByLengthZ == null ? "$" : mLinearStiffnessByLengthZ.ToString()) + "," +
				(mRotationalStiffnessByLengthX == null ? "$" : mRotationalStiffnessByLengthX.ToString()) + "," +
				(mRotationalStiffnessByLengthY == null ? "$" : mRotationalStiffnessByLengthY.ToString()) + "," +
				(mRotationalStiffnessByLengthZ == null ? "$" : mRotationalStiffnessByLengthZ.ToString());
		}

		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mLinearStiffnessByLengthX = IfcModulusOfTranslationalSubgradeReactionSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mLinearStiffnessByLengthY = IfcModulusOfTranslationalSubgradeReactionSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mLinearStiffnessByLengthZ = IfcModulusOfTranslationalSubgradeReactionSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mRotationalStiffnessByLengthX = IfcModulusOfRotationalSubgradeReactionSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mRotationalStiffnessByLengthY = IfcModulusOfRotationalSubgradeReactionSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mRotationalStiffnessByLengthZ = IfcModulusOfRotationalSubgradeReactionSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
		}
	}
	public partial class IfcBoundaryFaceCondition
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return base.BuildStringSTEP(release) + "," + (mTranslationalStiffnessByAreaX == null ? "$" : ParserSTEP.DoubleToString(mTranslationalStiffnessByAreaX.mStiffness.mValue)) + "," +
					(mTranslationalStiffnessByAreaY == null ? "$" : ParserSTEP.DoubleToString(mTranslationalStiffnessByAreaY.mStiffness.mValue)) + "," +
					(mTranslationalStiffnessByAreaZ == null ? "$" : ParserSTEP.DoubleToString(mTranslationalStiffnessByAreaZ.mStiffness.mValue));
			return base.BuildStringSTEP(release) + "," + (mTranslationalStiffnessByAreaX == null ? "$" : mTranslationalStiffnessByAreaX.ToString()) + "," +
				(mTranslationalStiffnessByAreaY == null ? "$" : mTranslationalStiffnessByAreaY.ToString()) + "," +
				(mTranslationalStiffnessByAreaZ == null ? "$" : mTranslationalStiffnessByAreaZ.ToString());
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mTranslationalStiffnessByAreaX = IfcModulusOfSubgradeReactionSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mTranslationalStiffnessByAreaY = IfcModulusOfSubgradeReactionSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mTranslationalStiffnessByAreaZ = IfcModulusOfSubgradeReactionSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
		}
	}
	public partial class IfcBoundaryNodeCondition 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4)
				return base.BuildStringSTEP(release) + "," + (mTranslationalStiffnessX == null ? "$" : ParserSTEP.DoubleToString(mTranslationalStiffnessX.mStiffness.mValue)) + "," +
					(mTranslationalStiffnessY == null ? "$" : ParserSTEP.DoubleToString(mTranslationalStiffnessY.mStiffness.mValue)) + "," +
					(mTranslationalStiffnessZ == null ? "$" : ParserSTEP.DoubleToString(mTranslationalStiffnessZ.mStiffness.mValue)) + "," +
					(mRotationalStiffnessX == null ? "$" : ParserSTEP.DoubleToString(mRotationalStiffnessX.mStiffness.mValue)) + "," +
					(mRotationalStiffnessY == null ? "$" : ParserSTEP.DoubleToString(mRotationalStiffnessY.mStiffness.mValue)) + "," +
					(mRotationalStiffnessZ == null ? "$" : ParserSTEP.DoubleToString(mRotationalStiffnessZ.mStiffness.mValue));
			return base.BuildStringSTEP(release) + "," + (mTranslationalStiffnessX == null ? "$" : mTranslationalStiffnessX.ToString()) + "," +
				(mTranslationalStiffnessY == null ? "$" : mTranslationalStiffnessY.ToString()) + "," +
				(mTranslationalStiffnessZ == null ? "$" : mTranslationalStiffnessZ.ToString()) + "," +
				(mRotationalStiffnessX == null ? "$" : mRotationalStiffnessX.ToString()) + "," +
				(mRotationalStiffnessY == null ? "$" : mRotationalStiffnessY.ToString()) + "," +
				(mRotationalStiffnessZ == null ? "$" : mRotationalStiffnessZ.ToString());
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mTranslationalStiffnessX = IfcTranslationalStiffnessSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mTranslationalStiffnessY = IfcTranslationalStiffnessSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mTranslationalStiffnessZ = IfcTranslationalStiffnessSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mRotationalStiffnessX = IfcRotationalStiffnessSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mRotationalStiffnessY = IfcRotationalStiffnessSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
			mRotationalStiffnessZ = IfcRotationalStiffnessSelect.Parse(ParserSTEP.StripField(str, ref pos, len), release);
		}
	}	
	public partial class IfcBoundaryNodeConditionWarping
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + mWarpingStiffness.ToString(); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mWarpingStiffness = IfcWarpingStiffnessSelect.Parse(ParserSTEP.StripField(str, ref pos, len));
		}
	}
	public partial class IfcBoundingBox
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mCorner.StepId + "," +  ParserSTEP.DoubleToString(mXDim) + "," + ParserSTEP.DoubleToString(mYDim) + "," + ParserSTEP.DoubleToString(mZDim); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mCorner = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianPoint;
			mXDim = ParserSTEP.StripDouble(str, ref pos, len);
			mYDim = ParserSTEP.StripDouble(str, ref pos, len);
			mZDim = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcBoxedHalfSpace
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mEnclosure.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary) { mEnclosure = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcBoundingBox; }
	}
	public partial class IfcBridge
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcBridgeTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcBridgePart
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (release < ReleaseVersion.IFC4X3 && release >= ReleaseVersion.IFC4X3_RC1)
				return base.BuildStringSTEP(release);
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcBridgePartTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4X3_RC1 || release >= ReleaseVersion.IFC4X3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
				{
					if (Enum.TryParse<IfcBridgePartTypeEnum>(s.Replace(".", ""), true, out IfcBridgePartTypeEnum partType))
						PredefinedType = partType;
				}
			}
		}
	}
	public abstract partial class IfcBSplineCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return mDegree + ",(#" + string.Join(",#", mControlPointsList.Select(x=>x.StepId.ToString())) +
				"),." + mCurveForm.ToString() + ".," + ParserIfc.LogicalToString(mClosedCurve) + "," + ParserIfc.LogicalToString(mSelfIntersect);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mDegree = int.Parse(ParserSTEP.StripField(str, ref pos, len));
			ControlPointsList.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcCartesianPoint));
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcBSplineCurveForm> (s.Replace(".", ""), true, out mCurveForm);
			mClosedCurve = ParserIfc.StripLogical(str, ref pos, len);
			mSelfIntersect = ParserIfc.StripLogical(str, ref pos, len);
		}
	}
	public partial class IfcBSplineCurveWithKnots
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mKnotMultiplicities) + "),("  +
				string.Join(",", mKnots.Select(x=> ParserSTEP.DoubleToString(x))) + "),." + mKnotSpec.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mKnotMultiplicities.AddRange(ParserSTEP.StripListInt(str, ref pos, len));
			mKnots.AddRange(ParserSTEP.StripListDouble(str, ref pos, len));
			if (!Enum.TryParse<IfcKnotType>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mKnotSpec))
				mKnotSpec = IfcKnotType.UNSPECIFIED;
		}
	}
	public abstract partial class IfcBSplineSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return mUDegree + "," + mVDegree + ",(" + string.Join(",", mControlPointsList.Select(x=> "(#" + string.Join(",#", x.Select(p=>p.StepId)) + ")")) + "),." + 
				mSurfaceForm.ToString() + ".," + ParserIfc.LogicalToString(mUClosed) + "," + ParserIfc.LogicalToString(mVClosed) + "," + ParserIfc.LogicalToString(mSelfIntersect);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mUDegree = int.Parse(ParserSTEP.StripField(str, ref pos, len));
			mVDegree = int.Parse(ParserSTEP.StripField(str, ref pos, len));
			foreach(List<int> ids in ParserSTEP.StripListListLink(str, ref pos, len))
				ControlPointsList.Add(new LIST<IfcCartesianPoint>(ids.Select(x => dictionary[x] as IfcCartesianPoint)));
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s[0] == '.')
				Enum.TryParse<IfcBSplineSurfaceForm>(s.Replace(".", ""), true, out mSurfaceForm);
			mUClosed = ParserIfc.StripLogical(str, ref pos, len);
			mVClosed = ParserIfc.StripLogical(str, ref pos, len);
			mSelfIntersect = ParserIfc.StripLogical(str, ref pos, len);
		}
	}
	public partial class IfcBSplineSurfaceWithKnots
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mUMultiplicities) + "),(" +
				string.Join(",", mVMultiplicities) + "),(" + string.Join(",", mUKnots.Select(x=>ParserSTEP.DoubleToString(x))) + "),(" +
				string.Join(",", mVKnots.Select(x=>ParserSTEP.DoubleToString(x))) + "),." + mKnotSpec.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mUMultiplicities = ParserSTEP.StripListInt(str, ref pos, len);
			mVMultiplicities = ParserSTEP.StripListInt(str, ref pos, len);
			mUKnots = ParserSTEP.StripListDouble(str, ref pos, len);
			mVKnots = ParserSTEP.StripListDouble(str, ref pos, len);
			if (!Enum.TryParse<IfcKnotType>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mKnotSpec))
				mKnotSpec = IfcKnotType.UNSPECIFIED;
		}
	}
	public partial class IfcBuilding
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mElevationOfRefHeight) + "," +
				ParserSTEP.DoubleOptionalToString(mElevationOfTerrain) + "," + ParserSTEP.ObjToLinkString(mBuildingAddress);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mElevationOfRefHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mElevationOfTerrain = ParserSTEP.StripDouble(str, ref pos, len);
			mBuildingAddress = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPostalAddress;
		}
	}
	public partial class IfcBuildingElementPart
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcBuildingElementPartTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcBuildingElementPartTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcBuildingElementPartType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcBuildingElementPartTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcBuildingElementProxy
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string str = (mPredefinedType == IfcBuildingElementProxyTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
			if (mPredefinedType != IfcBuildingElementProxyTypeEnum.NOTDEFINED && release < ReleaseVersion.IFC4)
			{
				if (mPredefinedType != IfcBuildingElementProxyTypeEnum.COMPLEX && mPredefinedType != IfcBuildingElementProxyTypeEnum.ELEMENT && mPredefinedType != IfcBuildingElementProxyTypeEnum.PARTIAL)
					str = ",$";
			}
			return base.BuildStringSTEP(release) + str;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcBuildingElementProxyTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcBuildingElementProxyType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcBuildingElementProxyTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcBuildingStorey
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + formatLength(mElevation); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mElevation = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcBuiltSystem
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) +
				(release < ReleaseVersion.IFC4 ? "" :
				(mPredefinedType == IfcBuiltSystemTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType + ".,") +
				(string.IsNullOrEmpty(mLongName) ? "$" : "'" + ParserSTEP.Encode(mLongName) + "'"));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release > ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcBuiltSystemTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
				mLongName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			}
		}
	}
	public partial class IfcBurner
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcBurnerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcBurnerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcBurnerType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcBurnerTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
}
