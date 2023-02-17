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
	public partial class IfcActionRequest
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? ",'" + mIdentification + "'" : ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIdentification = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcActor
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mTheActor.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mTheActor = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorSelect;
		}
	}
	public partial class IfcActorRole
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return "." + (release < ReleaseVersion.IFC4 && mRole == IfcRoleEnum.COMMISSIONINGENGINEER ? "COMISSIONINGENGINEER" : mRole.ToString()) +
				(string.IsNullOrEmpty( mUserDefinedRole) ? ".,$," : ".,'" + ParserSTEP.Encode(mUserDefinedRole) + "',") + (string.IsNullOrEmpty(mDescription) ? "$" : "'" + ParserSTEP.Encode(mDescription) + "'"); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
			{
				if (string.Compare(s, "COMISSIONINGENGINEER", true) == 0)
					mRole = IfcRoleEnum.COMMISSIONINGENGINEER;
				else
					Enum.TryParse<IfcRoleEnum>( s.Replace(".", ""), out mRole);
			}
			mUserDefinedRole = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcActuator  
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcActuatorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release > ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcActuatorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcActuatorType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcActuatorTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcAddress
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return (mPurpose == IfcAddressTypeEnum.NOTDEFINED ? "$," : "." + mPurpose.ToString() + ".,") + 
				(string.IsNullOrEmpty(mDescription) ? "$," : "'" + ParserSTEP.Encode(mDescription) + "',") + 
				(string.IsNullOrEmpty(mUserDefinedPurpose) ? "$" : "'" + ParserSTEP.Encode(mUserDefinedPurpose) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAddressTypeEnum>(s.Replace(".", ""), true, out mPurpose);
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mUserDefinedPurpose = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcAdvancedBrep
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release)); }
	}
	public partial class IfcAdvancedBrepWithVoids
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mVoids.ConvertAll(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mVoids.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcClosedShell));
		}
	}
	public partial class IfcAdvancedFace
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4 ? "" : base.BuildStringSTEP(release)); }
	}
	public partial class IfcAirTerminal
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcAirTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcAirTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcAirTerminalBox
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcAirTerminalBoxTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcAirTerminalBoxTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcAirTerminalBoxType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAirTerminalBoxTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAirTerminalType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAirTerminalTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAirToAirHeatRecovery  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mPredefinedType == IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcAirToAirHeatRecoveryTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcAirToAirHeatRecoveryType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAirToAirHeatRecoveryTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAlarm  
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : (mPredefinedType == IfcAlarmTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcAlarmTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcAlarmType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAlarmTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAlignment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (mPredefinedType == IfcAlignmentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAlignmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAlignment2DCant
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(#" + string.Join(",#", mSegments.ConvertAll(x => x.StepId.ToString())) + ")" + "," +
			ParserSTEP.DoubleOptionalToString(mRailHeadDistance);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			Segments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcAlignment2DCantSegment));
			RailHeadDistance = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public abstract partial class IfcAlignment2DCantSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mStartDistAlong) + "," +
			ParserSTEP.DoubleOptionalToString(mHorizontalLength) + "," + ParserSTEP.DoubleOptionalToString(mStartCantLeft) + "," +
			ParserSTEP.DoubleOptionalToString(mStartCantRight); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			StartDistAlong = ParserSTEP.StripDouble(str, ref pos, len);
			HorizontalLength = ParserSTEP.StripDouble(str, ref pos, len);
			StartCantLeft = ParserSTEP.StripDouble(str, ref pos, len);
			StartCantRight = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcAlignment2DCantSegTransition
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mEndCantLeft) + "," + ParserSTEP.DoubleOptionalToString(mEndCantRight);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			EndCantLeft = ParserSTEP.StripDouble(str, ref pos, len);
			EndCantRight = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcAlignment2DCantSegTransitionNonLinear
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mStartRadius) + "," +
			ParserSTEP.DoubleOptionalToString(mEndRadius) + (mIsStartRadiusCCW ? ",.T." : ",.F.") +
			(mIsEndRadiusCCW ? ",.T." : ",.F.") + ",." + mTransitionCurveType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			StartRadius = ParserSTEP.StripDouble(str, ref pos, len);
			EndRadius = ParserSTEP.StripDouble(str, ref pos, len);
			IsStartRadiusCCW = ParserSTEP.StripBool(str, ref pos, len);
			IsEndRadiusCCW = ParserSTEP.StripBool(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcTransitionCurveType>(s.Replace(".", ""), true, out mTransitionCurveType);
		}
	}
	public partial class IfcAlignment2DHorizontal
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return StepOptionalLengthString(mStartDistAlong) + ",(" + string.Join(",", mSegments.ConvertAll(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mStartDistAlong = ParserSTEP.StripDouble(str, ref pos, len);
			mSegments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcAlignment2DHorizontalSegment));
		}
	}
	public partial class IfcAlignment2DHorizontalSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mCurveGeometry.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			CurveGeometry = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurveSegment2D;
		}
	}
	public abstract partial class IfcAlignment2DSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (mTangentialContinuity == IfcLogicalEnum.UNKNOWN ? "$," : (mTangentialContinuity == IfcLogicalEnum.TRUE ? ".T.," : ".F.,")) +
				(string.IsNullOrEmpty(mStartTag) ? "$," : "'" + ParserSTEP.Encode(mStartTag) + "',") + (string.IsNullOrEmpty(mEndTag) ? "$" : "'" + ParserSTEP.Encode(mEndTag) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mTangentialContinuity = ParserIfc.StripLogical(str, ref pos, len);
			mStartTag = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mEndTag = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcAlignment2DVerSegCircularArc
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + formatLength(mRadius) + "," + ParserSTEP.BoolToString(mIsConvex); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRadius = ParserSTEP.StripDouble(str, ref pos, len);
			mIsConvex = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcAlignment2DVerSegParabolicArc
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mParabolaConstant) + "," + ParserSTEP.BoolToString(mIsConvex); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mParabolaConstant = ParserSTEP.StripDouble(str, ref pos, len);
			mIsConvex = ParserSTEP.StripBool(str, ref pos, len);
		}
	}
	public partial class IfcAlignment2DVertical
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + string.Join(",", mSegments.ConvertAll(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary) { mSegments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcAlignment2DVerticalSegment)); }
	}
	public abstract partial class IfcAlignment2DVerticalSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + formatLength(mStartDistAlong) + "," + formatLength(mHorizontalLength) + "," + formatLength(mStartHeight) + "," + ParserSTEP.DoubleToString(mStartGradient); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mStartDistAlong = ParserSTEP.StripDouble(str, ref pos, len);
			mHorizontalLength = ParserSTEP.StripDouble(str, ref pos, len);
			mStartHeight = ParserSTEP.StripDouble(str, ref pos, len);
			mStartGradient = ParserSTEP.StripDouble(str, ref pos, len);
		}
	}
	public partial class IfcAlignmentCant
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mRailHeadDistance) +
				(release == ReleaseVersion.IFC4X3_RC2 ? (mCantSegments.Count == 0 ? ",$" : ",(" + string.Join(",", mCantSegments.ConvertAll(x => "#" + x.StepId)) + ")") : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			RailHeadDistance = ParserSTEP.StripDouble(str, ref pos, len);
			if(release <= ReleaseVersion.IFC4X3_RC3 && pos < len)
				mCantSegments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcAlignmentCantSegment));
		}
	}
	public partial class IfcAlignmentCantSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleOptionalToString(mStartDistAlong) + "," +
			ParserSTEP.DoubleOptionalToString(mHorizontalLength) + "," + ParserSTEP.DoubleToString(mStartCantLeft) + "," +
			ParserSTEP.DoubleOptionalToString(mEndCantLeft) + "," + ParserSTEP.DoubleToString(mStartCantRight) + "," +
			ParserSTEP.DoubleOptionalToString(mEndCantRight) + (release == ReleaseVersion.IFC4X3_RC3 ? ",$" : "") + 
			",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			StartDistAlong = ParserSTEP.StripDouble(str, ref pos, len);
			HorizontalLength = ParserSTEP.StripDouble(str, ref pos, len);
			StartCantLeft = ParserSTEP.StripDouble(str, ref pos, len);
			EndCantLeft = ParserSTEP.StripDouble(str, ref pos, len);
			StartCantRight = ParserSTEP.StripDouble(str, ref pos, len);
			EndCantRight = ParserSTEP.StripDouble(str, ref pos, len);
			if(release == ReleaseVersion.IFC4X3_RC3)
				ParserSTEP.StripDouble(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAlignmentCantSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAlignmentCurve
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return ParserSTEP.ObjToLinkString(mHorizontal) + "," + ParserSTEP.ObjToLinkString(mVertical) + "," + (string.IsNullOrEmpty(mTag) ? "$" : "'" + ParserSTEP.Encode(mTag) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			mHorizontal = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAlignment2DHorizontal;
			Vertical = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAlignment2DVertical;
			mTag = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcAlignmentHorizontal
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4X3_RC4 ? "," + ParserSTEP.DoubleOptionalToString(mStartDistAlong) : "") +
				(release == ReleaseVersion.IFC4X3_RC2 ? (mHorizontalSegments.Count == 0 ? ",$" : ",(" + string.Join(",", mHorizontalSegments.ConvertAll(x => "#" + x.StepId)) + ")") : ""); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if(release < ReleaseVersion.IFC4X3_RC4)
				StartDistAlong = ParserSTEP.StripDouble(str, ref pos, len);
			if(pos < len && release < ReleaseVersion.IFC4X3_RC3)
				mHorizontalSegments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcAlignmentHorizontalSegment));
		}
	}
	public partial class IfcAlignmentHorizontalSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mStartPoint.StepId + "," + ParserSTEP.DoubleToString(mStartDirection) + "," +
			formatLength(mStartRadiusOfCurvature) + "," +
			formatLength(mEndRadiusOfCurvature) + "," + formatLength(mSegmentLength) + "," +
			ParserSTEP.DoubleOptionalToString(mGravityCenterLineHeight) + ",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mStartPoint = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCartesianPoint;
			mStartDirection = ParserSTEP.StripDouble(str, ref pos, len);
			StartRadiusOfCurvature = ParserSTEP.StripDouble(str, ref pos, len);
			EndRadiusOfCurvature = ParserSTEP.StripDouble(str, ref pos, len);
			SegmentLength = ParserSTEP.StripDouble(str, ref pos, len);
			GravityCenterLineHeight = ParserSTEP.StripDouble(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAlignmentHorizontalSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public abstract partial class IfcAlignmentParameterSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (string.IsNullOrEmpty(mStartTag) ? "$" : "'" + ParserSTEP.Encode(mStartTag) + "'") +
			(string.IsNullOrEmpty(mEndTag) ? ",$" : ",'" + ParserSTEP.Encode(mEndTag) + "'");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			StartTag = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			EndTag = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
		}
	}
	public partial class IfcAlignmentSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + ",#" + mDesignParameters.StepId; 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			DesignParameters = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAlignmentParameterSegment;
		}
	}
	public partial class IfcAlignmentVertical
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (release == ReleaseVersion.IFC4X3_RC2 ? (mVerticalSegments.Count == 0 ? ",$" : ",(" + string.Join(",", mVerticalSegments.Select(x => "#" + x.StepId)) + ")") : "");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if(pos < len && release <= ReleaseVersion.IFC4X3_RC3)
				mVerticalSegments.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x => dictionary[x] as IfcAlignmentVerticalSegment));
		}
	}
	public partial class IfcAlignmentVerticalSegment
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + "," + formatLength(mStartDistAlong) + "," + formatLength(mHorizontalLength) + "," + 
				formatLength(mStartHeight) + "," + ParserSTEP.DoubleToString(mStartGradient) + "," + 
				ParserSTEP.DoubleToString(mEndGradient) + "," + formatLength(mRadiusOfCurvature) + ",." + mPredefinedType.ToString() + ".";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			StartDistAlong = ParserSTEP.StripDouble(str, ref pos, len);
			HorizontalLength = ParserSTEP.StripDouble(str, ref pos, len);
			StartHeight = ParserSTEP.StripDouble(str, ref pos, len);
			StartGradient = ParserSTEP.StripDouble(str, ref pos, len);
			EndGradient = ParserSTEP.StripDouble(str, ref pos, len);
			RadiusOfCurvature = ParserSTEP.StripDouble(str, ref pos, len);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAlignmentVerticalSegmentTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAnnotation
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4X3_RC1 ? "" : (mPredefinedType == IfcAnnotationTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release > ReleaseVersion.IFC4X2)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcAnnotationTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcAnnotationFillArea
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "#" + mOuterBoundary.StepId + 
				(mInnerBoundaries.Count == 0 ? ",$" : ",(" + string.Join(",", mInnerBoundaries.Select(x => "#" + x.StepId)) + ")");
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mOuterBoundary = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
			mInnerBoundaries.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=> dictionary[x] as IfcCurve));
		}
	}
	public partial class IfcAnnotationFillAreaOccurrence
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + (mFillStyleTarget == null ? ",$" : ",#" + mFillStyleTarget) +
				(mGlobalOrLocal == null ? ",$" : ",." + mGlobalOrLocal.Value.ToString() + "."); 
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mFillStyleTarget = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPoint;
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s != "$")
			{
				IfcGlobalOrLocalEnum val = IfcGlobalOrLocalEnum.GLOBAL_COORDS;
				if (Enum.TryParse<IfcGlobalOrLocalEnum>(s.Replace(".", ""), true, out val))
					mGlobalOrLocal = val;
			}
		}
	}
	public partial class IfcAnnotationSurface
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return "#" + mItem + "," + ParserSTEP.ObjToLinkString(mTextureCoordinates); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mItem = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcGeometricRepresentationItem;
			mTextureCoordinates = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcTextureCoordinate;
		}
	}
	public partial class IfcApplication
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (mApplicationDeveloper != null ? "#" + mApplicationDeveloper.StepId : "$") + ",'" + mVersion + "','" +
				(string.IsNullOrEmpty(mApplicationFullName) ? mDatabase.Factory.ApplicationFullName : mApplicationFullName) + "','" +
				(string.IsNullOrEmpty(mApplicationIdentifier) ? mDatabase.Factory.ApplicationIdentifier : mApplicationIdentifier) + "'";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			ApplicationDeveloper = dictionary[ParserSTEP.StripLink(str,ref pos, len)] as IfcOrganization;
			mVersion = ParserSTEP.StripString(str, ref pos, len);
			mApplicationFullName = ParserSTEP.StripString(str, ref pos, len);
			mApplicationIdentifier = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcAppliedValue
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			string result = (string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") + (string.IsNullOrEmpty( mDescription) ? "$," : "'" + ParserSTEP.Encode(mDescription) + "',");
			if (mAppliedValue == null)
				result += "$";
			else
			{
				IfcValue val = mAppliedValue as IfcValue;
				if (val != null)
					result += val.ToString();
				else
					result += ParserSTEP.LinkToString((mAppliedValue as BaseClassIfc).StepId);
			}
			result += (mUnitBasis == null ? ",$," :  ",#" + mUnitBasis.StepId + ",");
			if (release < ReleaseVersion.IFC4)
				return result +  (mSSApplicableDate == null ? ",$" : ",#" + mSSApplicableDate.StepId) + (mSSFixedUntilDate == null ? ",$" : ",#" + mSSFixedUntilDate.StepId);
			result += IfcDate.STEPAttribute(mApplicableDate) + "," + IfcDate.STEPAttribute(mFixedUntilDate);
			result += (string.IsNullOrEmpty(mCategory) ? ",$," : ",'" + ParserSTEP.Encode(mCategory) + "',") + (string.IsNullOrEmpty(mCondition) ? "$," : "'" + ParserSTEP.Encode(mCondition) + "',");
			return result + (mArithmeticOperator == IfcArithmeticOperatorEnum.NONE ? "$," : "." + mArithmeticOperator.ToString() + ".,")
				+ (mComponents.Count == 0 ? "$" : "(" + string.Join(",", mComponents.Select(x => "#" + x.StepId)) + ")"); ;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			string s = ParserSTEP.StripField(str, ref pos, len).Trim();
			if(s.StartsWith("IFC"))
				mAppliedValue = ParserIfc.parseValue(s);
			else
				mAppliedValue = dictionary[ParserSTEP.ParseLink(s)] as IfcAppliedValueSelect;
			mUnitBasis = dictionary[ ParserSTEP.StripLink(str, ref pos, len)] as IfcMeasureWithUnit;
			if(release < ReleaseVersion.IFC4)
			{
				mSSApplicableDate = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
				mSSFixedUntilDate = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDateTimeSelect;
			}
			else
			{
				mApplicableDate = IfcDate.ParseSTEP(ParserSTEP.StripString(str, ref pos, len));
				mFixedUntilDate = IfcDate.ParseSTEP(ParserSTEP.StripString(str, ref pos, len));
				mCategory = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mCondition = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcArithmeticOperatorEnum>(s.Replace(".", ""), true, out mArithmeticOperator);
				Components.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x => dictionary[x] as IfcAppliedValue));
			}
		}
	}
	public partial class IfcAppliedValueRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "#" + mComponentOfTotal.StepId + ",(" + 
				string.Join(",", mComponents.Select(x=> "#" + x.StepId))+ "),." + mArithmeticOperator.ToString() + ".," + mName + "," + mDescription;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			ComponentOfTotal = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcAppliedValue;
			Components.AddRange(ParserSTEP.StripListLink(str, ref pos, len).ConvertAll(x=>dictionary[x] as IfcAppliedValue));
			Enum.TryParse<IfcArithmeticOperatorEnum>(ParserSTEP.StripField(str, ref pos, len).Replace(".", ""), true, out mArithmeticOperator);
			mName = ParserSTEP.StripString(str, ref pos, len);
			mDescription = ParserSTEP.StripString(str, ref pos, len);
		}
	}
	public partial class IfcApproval
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			if(release < ReleaseVersion.IFC4)
				return (string.IsNullOrEmpty(mDescription) ? "$,#" : "'" + ParserSTEP.Encode(mDescription) + "',#") + 
					mApprovalDateTime.StepId + (string.IsNullOrEmpty(mStatus) ? ",$," : ",'" +  ParserSTEP.Encode(mStatus) + "',") +
					(string.IsNullOrEmpty(mLevel) ? "$," : "'" + ParserSTEP.Encode(mLevel) + "',") +
					(string.IsNullOrEmpty(mQualifier) ? "$," : "'" + ParserSTEP.Encode(mQualifier) + "',") +
					(string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") +
					(string.IsNullOrEmpty(mIdentifier) ? "$" : "'" + ParserSTEP.Encode(mIdentifier) + "'");
			return (string.IsNullOrEmpty(mIdentifier) ? "$," : "'" + ParserSTEP.Encode(mIdentifier) + "',") +
				(string.IsNullOrEmpty(mName) ? "$," : "'" + ParserSTEP.Encode(mName) + "',") +
				(string.IsNullOrEmpty(mDescription) ? "$," : "'" + ParserSTEP.Encode(mDescription) + "',") +
				IfcDate.STEPAttribute(mTimeOfApproval) +
				(string.IsNullOrEmpty(mStatus) ? ",$," : ",'" + ParserSTEP.Encode(mStatus) + "',") +
				(string.IsNullOrEmpty(mLevel) ? "$," : "'" + ParserSTEP.Encode(mLevel) + "',") +
				(string.IsNullOrEmpty(mQualifier) ? "$," : "'" + ParserSTEP.Encode(mQualifier) + "',") +
				(mRequestingApproval == null ? "$," : "#," + mRequestingApproval.StepId) + (mGivingApproval == null ? "$" : "#" + mGivingApproval.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			if (release < ReleaseVersion.IFC4)
			{
				mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mApprovalDateTime = dictionary[ParserSTEP.StripLink(str, ref pos, len)]  as IfcDateTimeSelect;
				mStatus = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mLevel = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mQualifier = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mIdentifier = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			}
			else
			{
				mIdentifier = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mTimeOfApproval = IfcDate.ParseSTEP(ParserSTEP.StripString(str, ref pos, len));
				mStatus = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mLevel = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mQualifier = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				RequestingApproval = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorSelect;
				GivingApproval = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorSelect;
			}
		}
	}
	public partial class IfcApprovalActorRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return "#" + mActor.StepId + ",#" + mApproval.StepId + ",#" + mRole.StepId;  }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mActor = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorSelect;
			mApproval = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcApproval;
			mRole = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorRole;
		}
	}
	public partial class IfcApprovalPropertyRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return "(" + String.Join(",", mApprovedProperties.Select(x=>"#" + x.StepId)) + "),#" + mApproval.StepId;
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			mApprovedProperties.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcProperty));
			mApproval = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcApproval;
		}
	}
	public partial class IfcApprovalRelationship
	{
		protected override string BuildStringSTEP(ReleaseVersion release) 
		{ 
			return base.BuildStringSTEP(release) + ",#" + mRelatedApproval.StepId + ",#" + mRelatingApproval.StepId + 
				(release < ReleaseVersion.IFC4 ? (string.IsNullOrEmpty(mDescription) ? ",$,'" : ",'" + ParserSTEP.Encode(mDescription) + "','") + ParserSTEP.Encode(mName)  + "'": ""); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRelatedApproval = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcApproval; 
			mRelatingApproval = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcApproval;
			if (release < ReleaseVersion.IFC4)
			{
				mDescription = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
				mName = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			}
		}
	}
	public partial class IfcArbitraryClosedProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mOuterCurve.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mOuterCurve = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCurve;
		}
	}
	public partial class IfcArbitraryOpenProfileDef
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",#" + mCurve.StepId; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mCurve = Database[ParserSTEP.StripLink(str, ref pos, len)] as IfcBoundedCurve;
		}
	}
	public partial class IfcArbitraryProfileDefWithVoids
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mInnerCurves.Count == 0)
				return base.BuildStringSTEP(release);
			return base.BuildStringSTEP(release) + ",(" + string.Join(",", mInnerCurves.Select(x => "#" + x.StepId)) + ")";
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mInnerCurves.AddRange(ParserSTEP.StripListLink(str, ref pos, len).Select(x=>dictionary[x] as IfcCurve));
		}
	}
	public partial class IfcArchElement
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return (release < ReleaseVersion.IFC4X4_DRAFT ? "" : base.BuildStringSTEP(release) + (mPredefinedType == IfcArchElementTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType + "."));
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcArchElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcArchElementType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return (release < ReleaseVersion.IFC4X4_DRAFT ? "" : base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcArchElementTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAsset
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			return base.BuildStringSTEP(release) + (string.IsNullOrEmpty(mIdentification) ? ",$," : ",'" + ParserSTEP.Encode(mIdentification) +
				(mOriginalValue == null ? "',$" : "',#" + mOriginalValue.StepId) + (mCurrentValue == null ? ",$" : ",#" + mCurrentValue.StepId) +
				(mTotalReplacementCost == null ? ",$" : ",#" + mTotalReplacementCost.StepId) + (mOwner == null ? ",$" : ",#" + mOwner.StepId) + 
				(mUser == null ? ",$" : ",#" + mUser.StepId) + (mResponsiblePerson == null ? ",$" : ",#" + mResponsiblePerson.StepId) +
				(mDatabase.Release < ReleaseVersion.IFC4 ? (mIncorporationDate == null ? ",$" : ",#" + mIncorporationDateSS.StepId) :
					IfcDate.STEPAttribute(mIncorporationDate))) + (mDepreciatedValue == null ? ",$" : ",#" + mDepreciatedValue.StepId);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mIdentification = ParserSTEP.Decode(ParserSTEP.StripString(str, ref pos, len));
			mOriginalValue = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCostValue;
			mCurrentValue = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCostValue;
			mTotalReplacementCost = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCostValue;
			mOwner = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorSelect;
			mUser = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcActorSelect;
			mResponsiblePerson = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcPerson;
			if (release < ReleaseVersion.IFC4)
				mIncorporationDateSS = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCalendarDate;
			else
				mIncorporationDate = IfcDate.ParseSTEP(ParserSTEP.StripString(str, ref pos, len));
			mDepreciatedValue = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcCostValue;
		}
	}
	public partial class IfcAsymmetricIShapeProfileDef 
	{
		protected override string BuildStringSTEP(ReleaseVersion release)
		{
			if (mDatabase.Release < ReleaseVersion.IFC4)
			{
				return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mBottomFlangeWidth) + "," + ParserSTEP.DoubleToString(mOverallDepth) + "," + 
					ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mBottomFlangeThickness) + "," + ParserSTEP.DoubleOptionalToString(mBottomFlangeFilletRadius) + "," +
					ParserSTEP.DoubleToString(mTopFlangeWidth) + "," + ParserSTEP.DoubleOptionalToString(mTopFlangeThickness) + "," +
					ParserSTEP.DoubleOptionalToString(mTopFlangeFilletRadius) +  "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY);
			}
			return base.BuildStringSTEP(release) + "," + ParserSTEP.DoubleToString(mBottomFlangeWidth) + "," + ParserSTEP.DoubleToString(mOverallDepth) + "," +
					ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mBottomFlangeThickness) + "," + ParserSTEP.DoubleOptionalToString(mBottomFlangeFilletRadius) + "," +
				ParserSTEP.DoubleToString(mTopFlangeWidth) + "," + ParserSTEP.DoubleOptionalToString(mTopFlangeThickness) + "," +
				ParserSTEP.DoubleOptionalToString(mTopFlangeFilletRadius) + "," + ParserSTEP.DoubleOptionalToString(mBottomFlangeEdgeRadius) + "," +
				ParserSTEP.DoubleOptionalToString(mBottomFlangeSlope) + "," + ParserSTEP.DoubleOptionalToString(mTopFlangeEdgeRadius) + "," +
				ParserSTEP.DoubleOptionalToString(mTopFlangeSlope);
		}
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release < ReleaseVersion.IFC4)
			{
				mBottomFlangeWidth = ParserSTEP.StripDouble(str, ref pos, len);
				mOverallDepth = ParserSTEP.StripDouble(str, ref pos, len);
				mWebThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeWidth = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mCentreOfGravityInY = ParserSTEP.StripDouble(str, ref pos, len);
			}
			else
			{
				mBottomFlangeWidth = ParserSTEP.StripDouble(str, ref pos, len);
				mOverallDepth = ParserSTEP.StripDouble(str, ref pos, len);
				mWebThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeWidth = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeThickness = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeFilletRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeEdgeRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mBottomFlangeSlope = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeEdgeRadius = ParserSTEP.StripDouble(str, ref pos, len);
				mTopFlangeSlope = ParserSTEP.StripDouble(str, ref pos, len);
			}
		}
	}
	public partial class IfcAudioVisualAppliance
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (release < ReleaseVersion.IFC4 ? "" : ( mPredefinedType == IfcAudioVisualApplianceTypeEnum.NOTDEFINED  ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			if (release != ReleaseVersion.IFC2x3)
			{
				string s = ParserSTEP.StripField(str, ref pos, len);
				if (s.StartsWith("."))
					Enum.TryParse<IfcAudioVisualApplianceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
			}
		}
	}
	public partial class IfcAudioVisualApplianceType
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + ",." + mPredefinedType.ToString() + "."; }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			string s = ParserSTEP.StripField(str, ref pos, len);
			if (s.StartsWith("."))
				Enum.TryParse<IfcAudioVisualApplianceTypeEnum>(s.Replace(".", ""), true, out mPredefinedType);
		}
	}
	public partial class IfcAxis1Placement
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + (mAxis == null ? ",$" : ",#" + mAxis.StepId); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxis = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
		}
	}
	public partial class IfcAxis2Placement2D
	{ 
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(mRefDirection); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mRefDirection = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
		}
	}
	public partial class IfcAxis2Placement3D
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(mAxis) + "," + ParserSTEP.ObjToLinkString(mRefDirection); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int,BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxis = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
			mRefDirection = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
		}
	}
	public partial class IfcAxis2PlacementLinear
	{
		protected override string BuildStringSTEP(ReleaseVersion release) { return base.BuildStringSTEP(release) + "," + ParserSTEP.ObjToLinkString(mAxis) + "," + ParserSTEP.ObjToLinkString(mRefDirection); }
		internal override void parse(string str, ref int pos, ReleaseVersion release, int len, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			base.parse(str, ref pos, release, len, dictionary);
			mAxis = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
			mRefDirection = dictionary[ParserSTEP.StripLink(str, ref pos, len)] as IfcDirection;
		}
	}
}
