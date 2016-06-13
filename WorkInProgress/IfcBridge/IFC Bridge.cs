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
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Drawing;
using GeometryGym.STEP;


namespace GeometryGym.Ifc
{
	public partial class IfcBridge : IfcBridgeStructureElement //IFC5
	{
		internal IfcBridgeStructureType mPredefinedType = IfcBridgeStructureType.ARCHED_BRIDGE;    //:	IfcBridgeStructureType;
		public IfcBridgeStructureType PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBridge() : base() { }
		internal IfcBridge(IfcBridge b) : base(b) { mPredefinedType = b.mPredefinedType; }
		internal IfcBridge(IfcSpatialStructureElement host, string name,IfcBridgeStructureIndicator indicator, IfcBridgeStructureType type) : base(host, name,indicator) { mPredefinedType = type; } 
		internal static void parseFields(IfcBridge b, List<string> arrFields, ref int ipos)
		{
			IfcBridgeStructureElement.parseFields(b, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s[0] == '.')
				b.mPredefinedType = (IfcBridgeStructureType)Enum.Parse(typeof(IfcBridgeStructureType), s.Replace(".", ""));
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }//mPredefinedType != .NOTDEFINED ? : ",$,")  }
		internal static IfcBridge Parse(string strDef) { IfcBridge b = new IfcBridge(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
	}
	public partial class IfcBridgeContactElement : IfcCivilElementPart //IFC5
	{
		internal IfcBridgeContactType mContactType = IfcBridgeContactType.CONNECTOR;//:	IfcBridgeContactType
		public IfcBridgeContactType ContactType { get { return mContactType; } set { mContactType = value; } }

		internal IfcBridgeContactElement() : base() { }
		internal IfcBridgeContactElement(IfcBridgeContactElement b) : base(b) { mContactType = b.mContactType; }
		internal IfcBridgeContactElement(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static IfcBridgeContactElement Parse(string strDef) { IfcBridgeContactElement p = new IfcBridgeContactElement(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcBridgeContactElement a, List<string> arrFields, ref int ipos)
		{
			IfcCivilElementPart.parseFields(a, arrFields, ref ipos);
			a.mContactType = (IfcBridgeContactType)Enum.Parse(typeof(IfcBridgeContactType), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",." + mContactType.ToString() + ".";
		}
	}
	public abstract partial class IfcBridgeElement : IfcCivilElement //IFC5 ABSTRACT SUPERTYPE OF (ONEOF (IfcBridgeSegment, IfcBridgePrismaticElement))
	{
		protected IfcBridgeElement() : base() { }
		protected IfcBridgeElement(IfcBridgeElement b) : base(b) {  }
		protected IfcBridgeElement(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host,p,r) { }
	}
	public partial class IfcBridgePart : IfcBridgeStructureElement //IFC5
	{
		internal IfcBridgeStructureElementType mStructureElementType = IfcBridgeStructureElementType.ARCH;    //:	IfcBridgeStructureElementType;
		internal IfcBridgeTechnologicalElementType mTechnoElementType = IfcBridgeTechnologicalElementType.BOW_STRING;    //:	IfcBridgeTechnologicalElementType
		public IfcBridgeStructureElementType StructureElementType { get { return mStructureElementType; } set { mStructureElementType = value; } }
		public IfcBridgeTechnologicalElementType TechnoElementType { get { return mTechnoElementType; } set { mTechnoElementType = value; } }

		internal IfcBridgePart() : base() { }
		internal IfcBridgePart(IfcBridgePart p) : base(p) { mStructureElementType = p.mStructureElementType; mTechnoElementType = p.mTechnoElementType; }
		internal IfcBridgePart(IfcSpatialStructureElement host, string name, IfcBridgeStructureIndicator indicator, IfcBridgeStructureElementType type, IfcBridgeTechnologicalElementType techno) : base(host, name, indicator) { mStructureElementType = type; mTechnoElementType = techno; }
		internal static void parseFields(IfcBridgePart b, List<string> arrFields, ref int ipos)
		{
			IfcBridgeStructureElement.parseFields(b, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s[0] == '.')
				b.mStructureElementType = (IfcBridgeStructureElementType)Enum.Parse(typeof(IfcBridgeStructureElementType), s.Replace(".", ""));
			s = arrFields[ipos++];
			if (s[0] == '.')
				b.mTechnoElementType = (IfcBridgeTechnologicalElementType)Enum.Parse(typeof(IfcBridgeTechnologicalElementType), s.Replace(".", ""));
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mStructureElementType.ToString() + ".,." + mTechnoElementType.ToString() + "."; }
		internal static IfcBridgePart Parse(string strDef) { IfcBridgePart b = new IfcBridgePart(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
	}
	public partial class IfcBridgePrismaticElement : IfcBridgeElement  //IFC5
	{
		internal IfcBridgePrismaticElementType mPredefinedType = IfcBridgePrismaticElementType.AUGET;// : OPTIONAL IfcBridgeSegmentType;
		public IfcBridgePrismaticElementType PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcBridgePrismaticElement() : base() { }
		internal IfcBridgePrismaticElement(IfcBridgePrismaticElement s) : base(s) { mPredefinedType = s.mPredefinedType; }
		public IfcBridgePrismaticElement(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r, IfcBridgePrismaticElementType type) : base(host, p, r)
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mDatabase.mRelease == ReleaseVersion.IFC4 || mDatabase.mRelease == ReleaseVersion.IFC4A1)
				throw new Exception(KeyWord + " only supported in IFC5!");
			mPredefinedType = type;
		}
		internal new static IfcBridgePrismaticElement Parse(string strDef) { IfcBridgePrismaticElement e = new IfcBridgePrismaticElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		internal static void parseFields(IfcBridgePrismaticElement e, List<string> arrFields, ref int ipos)
		{
			IfcBridgeElement.parseFields(e, arrFields, ref ipos);
			e.mPredefinedType = (IfcBridgePrismaticElementType)Enum.Parse(typeof(IfcBridgePrismaticElementType), arrFields[ipos++].Replace(".", ""));
		}

		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcBridgeSegment : IfcBridgeElement  //IFC5
	{
		internal IfcBridgeSegmentType mSegmentType = IfcBridgeSegmentType.CANTILEVER;//: IfcBridgeSegmentType;
		internal List<int> mSegmentParts = new List<int>();// : SET[0:?] OF IfcCivilElementPart;

		public IfcBridgeSegmentType SegmentType { get { return mSegmentType; } set { mSegmentType = value; } }

		internal IfcBridgeSegment() : base() { }
		internal IfcBridgeSegment(IfcBridgeSegment s) : base(s) { }
		public IfcBridgeSegment(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r, IfcBridgeSegmentType type, List<IfcCivilElementPart> parts) : base(host, p, r)
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mDatabase.mRelease == ReleaseVersion.IFC4 || mDatabase.mRelease == ReleaseVersion.IFC4A1)
				throw new Exception(KeyWord + " only supported in IFC5!");
			mSegmentType = type;
			mSegmentParts = parts.ConvertAll(x => x.mIndex);
		}
		internal new static IfcBridgeSegment Parse(string strDef) { IfcBridgeSegment e = new IfcBridgeSegment(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		internal static void parseFields(IfcBridgeSegment e, List<string> arrFields, ref int ipos)
		{
			IfcBridgeElement.parseFields(e, arrFields, ref ipos);
			e.mSegmentType = (IfcBridgeSegmentType)Enum.Parse(typeof(IfcBridgeSegmentType), arrFields[ipos++].Replace(".", ""));
			e.mSegmentParts = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}

		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + ",." + mSegmentType.ToString() + ".";
			if (mSegmentParts.Count == 0)
				return result + ",$";
			result += ",(#" + mSegmentParts[0];
			for (int icounter = 1; icounter < mSegmentParts.Count; icounter++)
				result += ",#" + mSegmentParts[icounter];
			return result + ")";	
		}
	}
	public partial class IfcBridgeSegmentPart : IfcCivilElementPart //IFC5
	{
		internal IfcBridgeSubPartType mSubPartType = IfcBridgeSubPartType.BRANCH_WALL;//:	IfcBridgeSubPartType
		internal IfcBridgeMechanicalRoleType mMechanicalRole = IfcBridgeMechanicalRoleType.COMPLETE;//:	IfcBridgeMechanicalRoleType

		public IfcBridgeSubPartType SubPartType { get { return mSubPartType; } set { mSubPartType = value; } }
		public IfcBridgeMechanicalRoleType MechanicalRole { get { return mMechanicalRole; } set { mMechanicalRole = value; } }

		internal IfcBridgeSegmentPart() : base() { }
		internal IfcBridgeSegmentPart(IfcBridgeSegmentPart b) : base(b) { mSubPartType = b.mSubPartType; mMechanicalRole = b.mMechanicalRole; }
		internal IfcBridgeSegmentPart(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static IfcBridgeSegmentPart Parse(string strDef) { IfcBridgeSegmentPart p = new IfcBridgeSegmentPart(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcBridgeSegmentPart a, List<string> arrFields, ref int ipos)
		{
			IfcCivilElementPart.parseFields(a, arrFields, ref ipos);
			a.mSubPartType = (IfcBridgeSubPartType)Enum.Parse(typeof(IfcBridgeSubPartType), arrFields[ipos++].Replace(".", ""));
			a.mMechanicalRole = (IfcBridgeMechanicalRoleType)Enum.Parse(typeof(IfcBridgeMechanicalRoleType), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + ",." + mSubPartType.ToString() + ".,." + mMechanicalRole.ToString() + ".";
		}
	}
	public abstract partial class IfcBridgeStructureElement : IfcCivilStructureElement //IFC5 ABSTRACT SUPERTYPE OF (ONEOF (IfcBridge, IfcBridgePart))
	{
		internal IfcBridgeStructureIndicator mStructureIndicator = IfcBridgeStructureIndicator.OTHER;    //: IfcBridgeStructureIndicator;
		public IfcBridgeStructureIndicator StructureIndicator { get { return mStructureIndicator; } set { mStructureIndicator = value; } }

		internal IfcBridgeStructureElement() : base() { }
		internal IfcBridgeStructureElement(IfcBridgeStructureElement e) : base(e) { mStructureIndicator = e.mStructureIndicator; }
		internal IfcBridgeStructureElement(IfcSpatialStructureElement host, string name, IfcBridgeStructureIndicator indicator) : base(host, name)
		{
			mStructureIndicator = indicator;
		}

		internal static void parseFields(IfcBridgeStructureElement e, List<string> arrFields, ref int ipos)
		{
			IfcCivilStructureElement.parseFields(e, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s[0] == '.')
				e.mStructureIndicator = (IfcBridgeStructureIndicator)Enum.Parse(typeof(IfcBridgeStructureIndicator), s.Replace(".", ""));
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mStructureIndicator.ToString() + "."; }
	}
	
}
