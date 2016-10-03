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
	public partial class IfcActionRequest : IfcControl
	{
		//internal string mRequestID;// : IfcIdentifier; IFC4 relocated
		internal IfcActionRequest(DatabaseIfc db, IfcActionRequest r) : base(db,r) { }
		internal IfcActionRequest() : base() { }
		internal static IfcActionRequest Parse(string strDef,ReleaseVersion schema) { IfcActionRequest r = new IfcActionRequest(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return r; }
		internal static void parseFields(IfcActionRequest r,List<string> arrFields, ref int ipos,ReleaseVersion schema) 
		{ 
			IfcControl.parseFields(r,arrFields, ref ipos,schema);
			if(schema == ReleaseVersion.IFC2x3)
				r.mIdentification = arrFields[ipos++].Replace("'","");
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ",'" + mIdentification + "'" : ""); }
	}
	public partial class IfcActor : IfcObject //	SUPERTYPE OF(IfcOccupant)
	{
		internal int mTheActor;//	 :	IfcActorSelect; 
		//INVERSE
	    internal List<IfcRelAssignsToActor> mIsActingUpon = new List<IfcRelAssignsToActor>();// : SET [0:?] OF IfcRelAssignsToActor FOR RelatingActor;

		public IfcActorSelect TheActor { get { return mDatabase[mTheActor] as IfcActorSelect; } set { mTheActor = value.Index; } }

		internal IfcActor() : base() { }
		internal IfcActor(DatabaseIfc db, IfcActor a) : base(db,a) { TheActor = db.Factory.Duplicate(a.mDatabase[ a.mTheActor]) as IfcActorSelect; }
		internal IfcActor(IfcActorSelect a) : base(a.Database)
		{
 			if(mDatabase.mModelView != ModelView.If2x3NotAssigned && mDatabase.mModelView != ModelView.Ifc4NotAssigned )
				throw new Exception("Invalid Model View for IfcActor : " + mDatabase.ModelView.ToString());
			mTheActor = a.Index; 
		}
		internal static IfcActor Parse(string strDef) { IfcActor a = new IfcActor(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcActor a, List<string> arrFields, ref int ipos) { IfcObject.parseFields(a, arrFields, ref ipos); a.mTheActor = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mTheActor); }
	}
	public partial class IfcActorRole : BaseClassIfc, IfcResourceObjectSelect
	{
		internal IfcRoleEnum mRole = IfcRoleEnum.NOTDEFINED;// : OPTIONAL IfcRoleEnum
		internal string mUserDefinedRole = "$";// : OPTIONAL IfcLabel
		internal string mDescription = "$";// : OPTIONAL IfcText; 
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public IfcRoleEnum Role { get { return mRole; } set { mRole = value; } }
		public string UserDefinedRole { get { return (mUserDefinedRole == "$" ? "" : ParserIfc.Decode(mUserDefinedRole)); } set { mUserDefinedRole = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcActorRole() : base() { }
		internal IfcActorRole(DatabaseIfc db, IfcActorRole r) : base(db,r) { mRole = r.mRole; mDescription = r.mDescription; mUserDefinedRole = r.mUserDefinedRole; }
		public IfcActorRole(DatabaseIfc db) : base(db) { }
		
		internal static void parseFields(IfcActorRole a,List<string> arrFields, ref int ipos)
		{ 
			string str = arrFields[ipos++];
			if (str != "$")
				a.mRole = (string.Compare(str, "COMISSIONINGENGINEER", true) == 0 ? IfcRoleEnum.COMMISSIONINGENGINEER : (IfcRoleEnum)Enum.Parse(typeof(IfcRoleEnum), str.Replace(".", "")));
			a.mUserDefinedRole = arrFields[ipos++].Replace("'","");
			a.mDescription = arrFields[ipos++].Replace("'",""); 
		}
		internal static IfcActorRole Parse(string strDef) { IfcActorRole a = new IfcActorRole(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + (mDatabase.mRelease == ReleaseVersion.IFC2x3 && mRole == IfcRoleEnum.COMMISSIONINGENGINEER ? "COMISSIONINGENGINEER" : mRole.ToString()) + (mUserDefinedRole == "$" ? ".,$," : ".,'" + mUserDefinedRole + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'") ; }
	}
	public interface IfcActorSelect : IBaseClassIfc {  }// IfcOrganization,  IfcPerson,  IfcPersonAndOrganization);
	public partial class IfcActuator : IfcDistributionControlElement //IFC4  
	{   
		internal IfcActuatorTypeEnum mPredefinedType = IfcActuatorTypeEnum.NOTDEFINED;
		public IfcActuatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcActuator() : base() { }
		internal IfcActuator(DatabaseIfc db, IfcActuator a) : base(db,a) { mPredefinedType = a.mPredefinedType; }
		public IfcActuator(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement,representation, system) { }

		internal static void parseFields(IfcActuator a, List<string> arrFields, ref int ipos)
		{ 
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcActuatorTypeEnum)Enum.Parse(typeof(IfcActuatorTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcActuator Parse(string strDef) { IfcActuator d = new IfcActuator(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcActuatorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcActuatorType : IfcDistributionControlElementType
	{
		internal IfcActuatorTypeEnum mPredefinedType = IfcActuatorTypeEnum.NOTDEFINED;// : IfcActuatorTypeEnum; 
		public IfcActuatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcActuatorType() : base() { }
		internal IfcActuatorType(DatabaseIfc db, IfcActuatorType t) : base(db, t)  { mPredefinedType = t.mPredefinedType; }
		internal IfcActuatorType(DatabaseIfc m, string name, IfcActuatorTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcActuatorType t,List<string> arrFields, ref int ipos) { IfcDistributionControlElementType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcActuatorTypeEnum)Enum.Parse(typeof(IfcActuatorTypeEnum),arrFields[ipos++].Replace(".","")); }
		internal new static IfcActuatorType Parse(string strDef) { IfcActuatorType t = new IfcActuatorType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract partial class IfcAddress : BaseClassIfc 	//ABSTRACT SUPERTYPE OF(ONEOF(IfcPostalAddress, IfcTelecomAddress));
	{
		internal IfcAddressTypeEnum mPurpose = IfcAddressTypeEnum.NOTDEFINED;// : OPTIONAL IfcAddressTypeEnum
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal string mUserDefinedPurpose = "$";// : OPTIONAL IfcLabel 

		public IfcAddressTypeEnum Purpose { get { return mPurpose; } set { mPurpose = value; } }
		public string Description { get { return mDescription == "$" ? "" : ParserIfc.Decode(mDescription); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string UserDefinedPurpose { get { return mUserDefinedPurpose == "$" ? "" : ParserIfc.Decode(mUserDefinedPurpose); } set { mUserDefinedPurpose = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		
		internal IfcAddress() : base() { }
		internal IfcAddress(DatabaseIfc db) : base(db) {  }
		protected IfcAddress(DatabaseIfc db, IfcAddress a) : base(db,a)
		{
			mPurpose = a.mPurpose;
			mDescription = a.mDescription;
			mUserDefinedPurpose = a.mUserDefinedPurpose;
		}
		internal static void parseFields(IfcAddress a, List<string> arrFields, ref int ipos)
		{
			string str = arrFields[ipos++];
			if (str != "$")
				a.mPurpose = (IfcAddressTypeEnum)Enum.Parse(typeof(IfcAddressTypeEnum), str.Replace(".", ""));
			a.mDescription = arrFields[ipos++].Replace("'", "");
			a.mUserDefinedPurpose = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mPurpose == IfcAddressTypeEnum.NOTDEFINED ? ",$," : ",." + mPurpose.ToString() + ".,") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + (mUserDefinedPurpose == "$" ? "$" : "'" + mUserDefinedPurpose + "'"); }
	}
	public partial class IfcAdvancedBrep : IfcManifoldSolidBrep // IFC4
	{
		internal IfcAdvancedBrep() : base() { }
		public IfcAdvancedBrep(List<IfcAdvancedFace> faces) : base(new IfcClosedShell(faces.ConvertAll(x => (IfcFace)x))) { }
		internal IfcAdvancedBrep(DatabaseIfc db, IfcAdvancedBrep b) : base(db,b) { }
		internal IfcAdvancedBrep(IfcClosedShell s) : base(s) { }

		internal static IfcAdvancedBrep Parse(string strDef) { IfcAdvancedBrep b = new IfcAdvancedBrep(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcAdvancedBrep b, List<string> arrFields, ref int ipos) { IfcManifoldSolidBrep.parseFields(b, arrFields, ref ipos); }
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP()); }
	}
	public partial class IfcAdvancedBrepWithVoids : IfcAdvancedBrep
	{
		private List<int> mVoids = new List<int>();// : SET [1:?] OF IfcClosedShell
		public List<IfcClosedShell> Voids { get { return mVoids.ConvertAll(x => mDatabase[x] as IfcClosedShell); } set { mVoids = value.ConvertAll(x => x.mIndex); } }

		internal IfcAdvancedBrepWithVoids() : base() { }
		internal IfcAdvancedBrepWithVoids(DatabaseIfc db, IfcAdvancedBrepWithVoids b) : base(db,b) { Voids = b.Voids.ConvertAll(x=> db.Factory.Duplicate(x) as IfcClosedShell); }

		internal new static IfcAdvancedBrepWithVoids Parse(string strDef) { IfcAdvancedBrepWithVoids b = new IfcAdvancedBrepWithVoids(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcAdvancedBrepWithVoids b, List<string> arrFields, ref int ipos) { IfcAdvancedBrep.parseFields(b, arrFields, ref ipos); b.mVoids = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(";
			if (mVoids.Count > 0)
			{
				str += ParserSTEP.LinkToString(mVoids[0]);
				for (int icounter = 1; icounter < mVoids.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mVoids[icounter]);
			}
			return str + ")";
		}
	}
	public partial class IfcAdvancedFace : IfcFaceSurface
	{
		internal IfcAdvancedFace() : base() { }
		internal IfcAdvancedFace(DatabaseIfc db, IfcAdvancedFace f) : base(db,f) { }
		public IfcAdvancedFace(IfcFaceOuterBound bound, IfcSurface f, bool sense) : base(bound, f, sense) { }
		public IfcAdvancedFace(List<IfcFaceBound> bounds, IfcSurface f, bool sense) : base(bounds, f, sense) { }
		public IfcAdvancedFace(IfcFaceOuterBound outer, IfcFaceBound inner, IfcSurface f, bool sense) : base(outer,inner, f, sense) { }
		internal new static IfcAdvancedFace Parse(string strDef) { IfcAdvancedFace f = new IfcAdvancedFace(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
		internal static void parseFields(IfcAdvancedFace f, List<string> arrFields, ref int ipos) { IfcFaceSurface.parseFields(f, arrFields, ref ipos); }
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP()); }
	}
	public partial class IfcAirTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcAirTerminalTypeEnum mPredefinedType = IfcAirTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcAirTerminalTypeEnum;
		public IfcAirTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirTerminal() : base() { }
		internal IfcAirTerminal(DatabaseIfc db, IfcAirTerminal t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcAirTerminal(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcAirTerminal s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos); 
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcAirTerminalTypeEnum)Enum.Parse(typeof(IfcAirTerminalTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcAirTerminal Parse(string strDef) { IfcAirTerminal s = new IfcAirTerminal(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcAirTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }

	}
	public partial class IfcAirTerminalBox : IfcFlowController //IFC4
	{
		internal IfcAirTerminalBoxTypeEnum mPredefinedType = IfcAirTerminalBoxTypeEnum.NOTDEFINED;// OPTIONAL : IfcAirTerminalBoxTypeEnum;
		public IfcAirTerminalBoxTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirTerminalBox() : base() { }
		internal IfcAirTerminalBox(DatabaseIfc db, IfcAirTerminalBox b) : base(db, b) { mPredefinedType = b.mPredefinedType; }
		public IfcAirTerminalBox(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcAirTerminalBox s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcAirTerminalBoxTypeEnum)Enum.Parse(typeof(IfcAirTerminalBoxTypeEnum), str);
		}
		internal new static IfcAirTerminalBox Parse(string strDef) { IfcAirTerminalBox s = new IfcAirTerminalBox(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcAirTerminalBoxTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcAirTerminalBoxType : IfcFlowControllerType
	{
		internal IfcAirTerminalBoxTypeEnum mPredefinedType = IfcAirTerminalBoxTypeEnum.NOTDEFINED;// : IfcAirTerminalBoxTypeEnum; 
		public IfcAirTerminalBoxTypeEnum PredefinedType { get { return mPredefinedType;} set { mPredefinedType = value; } }

		internal IfcAirTerminalBoxType() : base() { }
		internal IfcAirTerminalBoxType(DatabaseIfc db, IfcAirTerminalBoxType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcAirTerminalBoxType(DatabaseIfc m, string name, IfcAirTerminalBoxTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcAirTerminalBoxType  t,List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcAirTerminalBoxTypeEnum)Enum.Parse(typeof(IfcAirTerminalBoxTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcAirTerminalBoxType Parse(string strDef) { IfcAirTerminalBoxType t = new IfcAirTerminalBoxType(); int ipos = 0; parseFields(t,ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcAirTerminalType : IfcFlowTerminalType
	{
		internal IfcAirTerminalTypeEnum mPredefinedType = IfcAirTerminalTypeEnum.NOTDEFINED;// : IfcAirTerminalBoxTypeEnum; 
		public IfcAirTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirTerminalType() : base() { }
		internal IfcAirTerminalType(DatabaseIfc db, IfcAirTerminalType t) : base(db,t) { mPredefinedType = t.mPredefinedType; }
		public IfcAirTerminalType(DatabaseIfc m, string name, IfcAirTerminalTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcAirTerminalType t,List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcAirTerminalTypeEnum)Enum.Parse(typeof(IfcAirTerminalTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcAirTerminalType Parse(string strDef) { IfcAirTerminalType t = new IfcAirTerminalType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcAirToAirHeatRecovery : IfcEnergyConversionDevice //IFC4  
	{
		internal IfcAirToAirHeatRecoveryTypeEnum mPredefinedType = IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED;
		public IfcAirToAirHeatRecoveryTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirToAirHeatRecovery() : base() { }
		internal IfcAirToAirHeatRecovery(DatabaseIfc db, IfcAirToAirHeatRecovery a) : base(db, a) { mPredefinedType = a.mPredefinedType; }
		public IfcAirToAirHeatRecovery(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcAirToAirHeatRecovery a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcAirToAirHeatRecoveryTypeEnum)Enum.Parse(typeof(IfcAirToAirHeatRecoveryTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcAirToAirHeatRecovery Parse(string strDef) { IfcAirToAirHeatRecovery d = new IfcAirToAirHeatRecovery(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mPredefinedType == IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
	}
	public partial class IfcAirToAirHeatRecoveryType : IfcEnergyConversionDeviceType
	{
		internal IfcAirToAirHeatRecoveryTypeEnum mPredefinedType = IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED;// : IfcAirToAirHeatRecoveryTypeEnum; 
		public IfcAirToAirHeatRecoveryTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirToAirHeatRecoveryType() : base() { }
		internal IfcAirToAirHeatRecoveryType(DatabaseIfc db, IfcAirToAirHeatRecoveryType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcAirToAirHeatRecoveryType(DatabaseIfc m, string name, IfcAirToAirHeatRecoveryTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcAirToAirHeatRecoveryType t,List<string> arrFields, ref int ipos) {  IfcEnergyConversionDeviceType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcAirToAirHeatRecoveryTypeEnum)Enum.Parse(typeof(IfcAirToAirHeatRecoveryTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcAirToAirHeatRecoveryType Parse(string strDef) { IfcAirToAirHeatRecoveryType t = new IfcAirToAirHeatRecoveryType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcAlarm : IfcDistributionControlElement //IFC4  
	{
		internal IfcAlarmTypeEnum mPredefinedType = IfcAlarmTypeEnum.NOTDEFINED;
		public IfcAlarmTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAlarm() : base() { }
		internal IfcAlarm(DatabaseIfc db, IfcAlarm a) : base(db, a) { mPredefinedType = a.mPredefinedType; }
		public IfcAlarm(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcAlarm a, List<string> arrFields, ref int ipos) 
		{ 
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcAlarmTypeEnum)Enum.Parse(typeof(IfcAlarmTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcAlarm Parse(string strDef) { IfcAlarm a = new IfcAlarm(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcAlarmTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcAlarmType : IfcDistributionControlElementType
	{
		internal IfcAlarmTypeEnum mPredefinedType = IfcAlarmTypeEnum.NOTDEFINED;// : IfcAlarmTypeEnum; 
		public IfcAlarmTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAlarmType() : base() { }
		internal IfcAlarmType(DatabaseIfc db, IfcAlarmType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcAlarmType(DatabaseIfc m, string name, IfcAlarmTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcAlarmType t,List<string> arrFields, ref int ipos) { IfcDistributionControlElementType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcAlarmTypeEnum)Enum.Parse(typeof(IfcAlarmTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcAlarmType Parse(string strDef) { IfcAlarmType t = new IfcAlarmType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcAngularDimension : IfcDimensionCurveDirectedCallout //IFC4 depreceated
	{
		//internal IfcAngularDimension(IfcAngularDimension el) : base(el) { }
		internal IfcAngularDimension() : base() { }
		internal new static IfcAngularDimension Parse(string strDef) { IfcAngularDimension d = new IfcAngularDimension(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		internal static void parseFields(IfcAngularDimension d,List<string> arrFields, ref int ipos) { IfcDimensionCurveDirectedCallout.parseFields(d,arrFields, ref ipos); }
	}
	public partial class IfcAnnotation : IfcProduct
	{	 //INVERSE
		internal IfcRelContainedInSpatialStructure mContainedInStructure = null;
		internal IfcAnnotation() : base() { }
		internal IfcAnnotation(DatabaseIfc db, IfcAnnotation a) : base(db,a) { }
		public IfcAnnotation(DatabaseIfc db) : base(db) { }
		internal static IfcAnnotation Parse(string strDef) { int ipos = 0; IfcAnnotation a = new IfcAnnotation(); parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcAnnotation a, List<string> arrFields, ref int ipos)  { IfcProduct.parseFields(a,arrFields, ref ipos); }
	}
	public abstract partial class IfcAnnotationCurveOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		//protected IfcAnnotationCurveOccurrence(IfcAnnotationCurveOccurrence p) : base((IfcAnnotationOccurrence)p) { }
		protected IfcAnnotationCurveOccurrence() : base() { }
		protected static void parseFields(IfcAnnotationCurveOccurrence a,List<string> arrFields, ref int ipos) { IfcAnnotationOccurrence.parseFields(a,arrFields, ref ipos); }
	}
	public partial class IfcAnnotationFillArea : IfcGeometricRepresentationItem  
	{
		internal int mOuterBoundary;// : IfcCurve;
		internal List<int> mInnerBoundaries = new List<int>();// OPTIONAL SET [1:?] OF IfcCurve; 

		public IfcCurve OuterBoundary { get { return mDatabase[mOuterBoundary] as IfcCurve; } set { mOuterBoundary = value.mIndex; } }
		public List<IfcCurve> InnerBoundaries { get { return mInnerBoundaries.ConvertAll(x => mDatabase[x] as IfcCurve); } set { mInnerBoundaries = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }

		internal IfcAnnotationFillArea() : base() { }
		internal IfcAnnotationFillArea(DatabaseIfc db, IfcAnnotationFillArea a) : base(db,a) { OuterBoundary = db.Factory.Duplicate(a.OuterBoundary) as IfcCurve; InnerBoundaries = a.InnerBoundaries.ConvertAll(x=>db.Factory.Duplicate(x) as IfcCurve); }
		public IfcAnnotationFillArea(IfcCurve outerBoundary) : base(outerBoundary.mDatabase) { OuterBoundary = outerBoundary; }
		public IfcAnnotationFillArea(IfcCurve outerBoundary, List<IfcCurve> innerBoundaries) : this(outerBoundary) { InnerBoundaries = innerBoundaries; }
		internal static void parseFields(IfcAnnotationFillArea a, List<string> arrFields, ref int ipos)
		{
			IfcGeometricRepresentationItem.parseFields(a, arrFields, ref ipos);
			a.mOuterBoundary = ParserSTEP.ParseLink(arrFields[ipos++]);
			string str = arrFields[ipos++];
			if (str != "$")
				a.mInnerBoundaries = ParserSTEP.SplitListLinks(str);
		}
		internal static IfcAnnotationFillArea Parse(string strDef) { IfcAnnotationFillArea a = new IfcAnnotationFillArea(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mOuterBoundary);
			if (mInnerBoundaries.Count > 0)
			{
				str += ",(" + ParserSTEP.LinkToString(mInnerBoundaries[0]);
				for (int icounter = 1; icounter < mInnerBoundaries.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mInnerBoundaries[icounter]);
				return str + ")";
			}
			return str + ",$";
		}
 
	}
	public partial class IfcAnnotationFillAreaOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		internal int mFillStyleTarget;// : OPTIONAL IfcPoint;
		internal IfcGlobalOrLocalEnum  mGlobalOrLocal;// : OPTIONAL IfcGlobalOrLocalEnum; 
		internal IfcAnnotationFillAreaOccurrence(DatabaseIfc db, IfcAnnotationFillAreaOccurrence f) : base(db,f) { }
		internal IfcAnnotationFillAreaOccurrence() : base() { }
		internal new static IfcAnnotationFillAreaOccurrence Parse(string strDef) { IfcAnnotationFillAreaOccurrence a = new IfcAnnotationFillAreaOccurrence(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcAnnotationFillAreaOccurrence el,List<string> arrFields, ref int ipos)
		{ 
			IfcAnnotationOccurrence.parseFields(el,arrFields, ref ipos);
			el.mFillStyleTarget = ParserSTEP.ParseLink(arrFields[ipos++]);
			string str = arrFields[ipos++];
			if(str != "$")
				el.mGlobalOrLocal = (IfcGlobalOrLocalEnum)Enum.Parse(typeof(IfcGlobalOrLocalEnum),str.Replace(".","")); 
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() +"," + ParserSTEP.LinkToString(mFillStyleTarget) + ",." + mGlobalOrLocal.ToString() + "."; }
	}
	public abstract partial class IfcAnnotationOccurrence : IfcStyledItem //DEPRECEATED IFC4
	{
		protected IfcAnnotationOccurrence(DatabaseIfc db, IfcAnnotationOccurrence o) : base(db,o) { }
		protected IfcAnnotationOccurrence() : base() { }
		protected static void parseFields(IfcAnnotationOccurrence a,List<string> arrFields, ref int ipos) { IfcStyledItem.parseFields(a,arrFields, ref ipos); }
	}
	public partial class IfcAnnotationSurface : IfcGeometricRepresentationItem //DEPRECEATED IFC4
	{
		internal int mItem;// : IfcGeometricRepresentationItem;
		internal int mTextureCoordinates;// OPTIONAL IfcTextureCoordinate;

		public IfcGeometricRepresentationItem Item { get { return mDatabase[mItem] as IfcGeometricRepresentationItem; } set { mItem = value.mIndex; } }
		public IfcTextureCoordinate TextureCoordinates { get { return mDatabase[mTextureCoordinates] as IfcTextureCoordinate; } set { mTextureCoordinates = value.mIndex; } }

		internal IfcAnnotationSurface() : base() { } 
		internal IfcAnnotationSurface(DatabaseIfc db, IfcAnnotationSurface a) : base(db, a) { Item = db.Factory.Duplicate(a.Item) as IfcGeometricRepresentationItem; if(a.mTextureCoordinates > 0) TextureCoordinates = db.Factory.Duplicate(a.TextureCoordinates) as IfcTextureCoordinate; }
		internal static void parseFields(IfcAnnotationSurface s,List<string> arrFields, ref int ipos)
		{  
			IfcGeometricRepresentationItem.parseFields(s,arrFields, ref ipos);
			s.mItem = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mTextureCoordinates = ParserSTEP.ParseLink(arrFields[ipos++]); 
		}
		internal static IfcAnnotationSurface Parse(string strDef) { IfcAnnotationSurface a = new IfcAnnotationSurface(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mItem) + "," + ParserSTEP.LinkToString(mTextureCoordinates); }
	}
	public partial class IfcAnnotationSurfaceOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		internal IfcAnnotationSurfaceOccurrence(DatabaseIfc db, IfcAnnotationSurfaceOccurrence o) : base(db,o) { }
		internal IfcAnnotationSurfaceOccurrence() : base() { }
		internal new static IfcAnnotationSurfaceOccurrence Parse(string strDef) { IfcAnnotationSurfaceOccurrence a = new IfcAnnotationSurfaceOccurrence(); int ipos = 0; parseFields(a,ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcAnnotationSurfaceOccurrence a,List<string> arrFields, ref int ipos) { IfcAnnotationOccurrence.parseFields(a,arrFields, ref ipos); }
	}
	public partial class IfcAnnotationSymbolOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		//internal IfcAnnotationSymbolOccurrence(DatabaseIfc db, IfcAnnotationSymbolOccurrence o) : base(o) { }
		internal IfcAnnotationSymbolOccurrence() : base() { }
		internal new static IfcAnnotationSymbolOccurrence Parse(string strDef) { IfcAnnotationSymbolOccurrence a = new IfcAnnotationSymbolOccurrence(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcAnnotationSymbolOccurrence a,List<string> arrFields, ref int ipos) { IfcAnnotationOccurrence.parseFields(a,arrFields, ref ipos); }
	}
	public partial class IfcAnnotationTextOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		internal IfcAnnotationTextOccurrence() : base() { }
		internal IfcAnnotationTextOccurrence(DatabaseIfc db, IfcAnnotationTextOccurrence o) : base(db,o) { }
		internal new static IfcAnnotationTextOccurrence Parse(string strDef) { IfcAnnotationTextOccurrence a = new IfcAnnotationTextOccurrence(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcAnnotationTextOccurrence a,List<string> arrFields, ref int ipos) { IfcAnnotationOccurrence.parseFields(a,arrFields, ref ipos); }
	}
	public partial class IfcApplication : BaseClassIfc
	{
		internal int mApplicationDeveloper;// : IfcOrganization;
		internal string mVersion;// : IfcLabel;
		private string mApplicationFullName;// : IfcLabel;
		internal string mApplicationIdentifier;// : IfcIdentifier; 
		
		public IfcOrganization ApplicationDeveloper { get { return mDatabase[mApplicationDeveloper] as IfcOrganization; } set { mApplicationDeveloper = value.mIndex; } }
		public string Version { get { return mVersion; } set { mVersion = ParserIfc.Encode(value.Replace("'", "")); } }
		public string ApplicationFullName { get { return ParserIfc.Decode(mApplicationFullName); } set { mApplicationFullName =  ParserIfc.Encode(value.Replace("'", "")); } }
		public string ApplicationIdentifier { get { return ParserIfc.Decode(mApplicationIdentifier); } set { mApplicationIdentifier =  ParserIfc.Encode(value.Replace("'", "")); } }

		public override string Name { get { return ApplicationFullName; } set { ApplicationFullName = value; } }

		internal IfcApplication() : base() { }
		internal IfcApplication(DatabaseIfc db) : base(db)
		{
			IfcOrganization o = new IfcOrganization(db, "Geometry Gym Pty Ltd");
			mApplicationDeveloper = o.mIndex;
			try
			{
				mVersion =  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
			catch (Exception) { mVersion = "Unknown"; }
			mApplicationFullName = db.Factory.ApplicationFullName;
			mApplicationIdentifier = db.Factory.ApplicationIdentifier;

		}
		internal IfcApplication(DatabaseIfc db, IfcApplication a) : base(db,a)
		{
			ApplicationDeveloper = db.Factory.Duplicate(a.ApplicationDeveloper) as IfcOrganization;
			mVersion = a.mVersion;
			mApplicationFullName = a.mApplicationFullName;
			mApplicationIdentifier = a.mApplicationIdentifier;
		}
		public IfcApplication(IfcOrganization developer, string version, string fullName, string identifier) :base(developer.mDatabase) { ApplicationDeveloper = developer; Version = version; ApplicationFullName = fullName; ApplicationIdentifier = identifier; }
		internal static void parseFields(IfcApplication a, List<string> arrFields, ref int ipos)
		{
			a.mApplicationDeveloper = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mVersion = arrFields[ipos++].Replace("'", "");
			a.mApplicationFullName = arrFields[ipos++].Replace("'", "");
			a.mApplicationIdentifier = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mApplicationDeveloper) + ",'" + mVersion + "','" + 
				(string.IsNullOrEmpty(mApplicationFullName) ? mDatabase.Factory.ApplicationFullName : mApplicationFullName) + "','" + 
				(string.IsNullOrEmpty(mApplicationIdentifier) ? mDatabase.Factory.ApplicationIdentifier : mApplicationIdentifier) + "'";
		}
		internal static IfcApplication Parse(string strDef) { IfcApplication a = new IfcApplication(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
	}
	public partial class IfcAppliedValue : BaseClassIfc, IfcMetricValueSelect, IfcResourceObjectSelect// SUPERTYPE OF(IfcCostValue);
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal int mAppliedValueIndex = 0;// : OPTIONAL IfcAppliedValueSelect
		internal IfcValue mAppliedValueValue = null;
		internal int mUnitBasis;// : OPTIONAL IfcMeasureWithUnit;
		internal string mApplicableDate = "$";// : OPTIONAL IfcDateTimeSelect; 4 IfcDate
		internal string mFixedUntilDate = "$";// : OPTIONAL IfcDateTimeSelect; 4 IfcDate
		internal string mCategory = "$";// : OPTIONAL IfcLabel; IFC4
		internal string mCondition = "$";// : OPTIONAL IfcLabel; IFC4
		internal IfcArithmeticOperatorEnum mArithmeticOperator = IfcArithmeticOperatorEnum.NONE;//	 :	OPTIONAL IfcArithmeticOperatorEnum; IFC4 
		internal List<int> mComponents = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcAppliedValue; IFC4
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg
		internal List<IfcAppliedValue> mComponentFor = new List<IfcAppliedValue>(); //gg

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } } 
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcAppliedValueSelect AppliedValue { get { return mDatabase[mAppliedValueIndex] as IfcAppliedValueSelect; } set { mAppliedValueIndex = (value == null ? 0 : value.Index); } }
		public IfcValue Value { get { return mAppliedValueValue; } set { mAppliedValueValue = value; } }
		public IfcMeasureWithUnit UnitBasis { get { return mDatabase[mUnitBasis] as IfcMeasureWithUnit; } set { mUnitBasis = (value == null ? 0 : value.mIndex); } }
		public string Category { get { return (mCategory == "$" ? "" : ParserIfc.Decode(mCategory)); } set { mCategory = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Condition { get { return (mCondition == "$" ? "" : ParserIfc.Decode(mCondition)); } set { mCondition = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcArithmeticOperatorEnum ArithmeticOperator { get { return mArithmeticOperator; } set { mArithmeticOperator = value; } }
		public List<IfcAppliedValue> Components { get { return mComponents.ConvertAll(x => mDatabase[x] as IfcAppliedValue); } set { mComponents = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcAppliedValue() : base() { }
		public IfcAppliedValue(DatabaseIfc db) : base(db) { }
		public IfcAppliedValue(IfcAppliedValueSelect appliedValue) : base(appliedValue.Database) { AppliedValue = appliedValue; }
		public IfcAppliedValue(DatabaseIfc db, IfcValue value) : base(db) { Value = value; }
		public IfcAppliedValue(IfcAppliedValue component1, IfcArithmeticOperatorEnum op,IfcAppliedValue component2) : base(component1.mDatabase) { Components = new List<IfcAppliedValue>() { component1, component2 }; mArithmeticOperator = op; } 
		internal IfcAppliedValue(DatabaseIfc db, IfcAppliedValue v) : base(db,v)
		{
			mName = v.mName; mDescription = v.mDescription; mAppliedValueIndex = v.mAppliedValueIndex; mAppliedValueValue = v.mAppliedValueValue;
			UnitBasis = db.Factory.Duplicate(v.UnitBasis) as IfcMeasureWithUnit;
			mApplicableDate = v.mApplicableDate; mFixedUntilDate = v.mFixedUntilDate; mCategory = v.mCategory; mCondition = v.mCondition; mArithmeticOperator = v.mArithmeticOperator;
			Components = v.Components.ConvertAll(x=>db.Factory.Duplicate(x) as IfcAppliedValue);
		}
		internal static IfcAppliedValue Parse(string strDef, ReleaseVersion schema) { IfcAppliedValue v = new IfcAppliedValue(); int ipos = 0; parseFields(v, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return v; }
		internal static void parseFields(IfcAppliedValue a, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			a.mName = arrFields[ipos++].Replace("'", "");
			a.mDescription = arrFields[ipos++].Replace("'", "");
			string str = arrFields[ipos++];
			a.mAppliedValueValue = ParserIfc.parseValue(str);
			if (a.mAppliedValueValue == null)
				a.mAppliedValueIndex = ParserSTEP.ParseLink(str);
			a.mUnitBasis = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mApplicableDate = arrFields[ipos++];
			a.mFixedUntilDate = arrFields[ipos++];
			if (schema != ReleaseVersion.IFC2x3)
			{
				a.mCategory = arrFields[ipos++].Replace("'", "");
				a.mCondition = arrFields[ipos++].Replace("'", "");
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					a.mArithmeticOperator = (IfcArithmeticOperatorEnum)Enum.Parse(typeof(IfcArithmeticOperatorEnum), s.Replace(".", ""));
				a.mComponents = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			}
		}
		protected override string BuildStringSTEP()
		{
			string str = "$";
			if (mComponents.Count > 0)
			{
				str = "(" + ParserSTEP.LinkToString(mComponents[0]);
				for (int icounter = 1; icounter < mComponents.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mComponents[icounter]);
				str += ")";
			}
			return base.BuildStringSTEP() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + (mAppliedValueValue != null ? mAppliedValueValue.ToString() : ParserSTEP.LinkToString(mAppliedValueIndex)) + "," + ParserSTEP.LinkToString(mUnitBasis) + "," + mApplicableDate + "," + mFixedUntilDate +
				(mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mCategory == "$" ? ",$," : ",'" + mCategory + "',") + (mCondition == "$" ? "$," : "'" + mCondition + "',") + (mArithmeticOperator == IfcArithmeticOperatorEnum.NONE ? "$," : "." + mArithmeticOperator.ToString() + ".,") + str);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			foreach (IfcAppliedValue v in Components)
				v.mComponentFor.Add(this);
		}

		public override bool Destruct(bool children)
		{
			if (children)
			{
				if (mAppliedValueIndex > 0)
					mDatabase[mAppliedValueIndex].Destruct(children);
				for (int icounter = 0; icounter < mComponents.Count; icounter++)
				{
					BaseClassIfc bc = mDatabase[mComponents[icounter]];
					if (bc != null)
						bc.Destruct(true);
				}
			}
			return base.Destruct(children);
		}
	}
	public partial class IfcAppliedValueRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		internal int mComponentOfTotal;// : IfcAppliedValue;
		internal List< int> mComponents = new List<int>();// : SET [1:?] OF IfcAppliedValue;
		internal IfcArithmeticOperatorEnum mArithmeticOperator;// : IfcArithmeticOperatorEnum;
		internal string mName;// : OPTIONAL IfcLabel;
		internal string mDescription;// : OPTIONAL IfcText 
		internal IfcAppliedValueRelationship() : base() { }
		//internal IfcAppliedValueRelationship(IfcAppliedValueRelationship o) : base()
		//{
		//	mComponentOfTotal = o.mComponentOfTotal;
		//	mComponents = new List<int>(o.mComponents.ToArray());
		//	mArithmeticOperator = o.mArithmeticOperator;
		//	mName = o.mName;
		//	mDescription = o.mDescription;
		//}
		internal static void parseFields( IfcAppliedValueRelationship a,List<string> arrFields, ref int ipos)
		{ 
			a.mComponentOfTotal = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mComponents = ParserSTEP.SplitListLinks(arrFields[ipos++]); 
			a.mArithmeticOperator =( IfcArithmeticOperatorEnum)Enum.Parse(typeof(IfcArithmeticOperatorEnum), arrFields[ipos++].Replace(".",""));
			a.mName = arrFields[ipos++];
			a.mDescription = arrFields[ipos++]; 
		}
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mComponentOfTotal) + ",(" +
				ParserSTEP.LinkToString(mComponents[0]);
			for(int icounter = 1; icounter < mComponents.Count;icounter++)
				str += "," + ParserSTEP.LinkToString(mComponents[icounter]);
			return str + "),." + mArithmeticOperator.ToString() + ".," + mName + "," + mDescription; 
		}
		internal static IfcAppliedValueRelationship Parse(string strDef) { IfcAppliedValueRelationship a = new IfcAppliedValueRelationship(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
	}
	public interface IfcAppliedValueSelect : IBaseClassIfc  //	IfcMeasureWithUnit, IfcValue, IfcReference); IFC2x3 //IfcRatioMeasure, IfcMeasureWithUnit, IfcMonetaryMeasure); 
	{
		//List<IfcAppliedValue> AppliedValueFor { get; }
	}
	public partial class IfcApproval : BaseClassIfc, IfcResourceObjectSelect
	{
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal int mApprovalDateTime;// : IfcDateTimeSelect;
		internal string mApprovalStatus = "$";// : OPTIONAL IfcLabel;
		internal string mApprovalLevel = "$";// : OPTIONAL IfcLabel;
		internal string mApprovalQualifier = "$";// : OPTIONAL IfcText;
		internal string mName = "$";// :OPTIONAL IfcLabel;
		internal string mIdentifier = "$";// : OPTIONAL IfcIdentifier;
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg

		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcApproval() : base() { }
		//internal IfcApproval(IfcApproval o) : base()
		//{
		//	mDescription = o.mDescription;
		//	mApprovalDateTime = o.mApprovalDateTime;
		//	mApprovalStatus = o.mApprovalStatus;
		//	mApprovalLevel = o.mApprovalLevel;
		//	mApprovalQualifier = o.mApprovalQualifier;
		//	mName = o.mName;
		//	mIdentifier = o.mIdentifier;
		//}
		internal static void parseFields(IfcApproval a,List<string> arrFields, ref int ipos)
		{ 
			a.mDescription = arrFields[ipos++];
			a.mApprovalDateTime = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mApprovalStatus = arrFields[ipos++];
			a.mApprovalLevel = arrFields[ipos++];
			a.mApprovalQualifier = arrFields[ipos++];
			a.mName = arrFields[ipos++];
			a.mIdentifier = arrFields[ipos++]; 
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP()  + "," + mDescription + "," + ParserSTEP.LinkToString(mApprovalDateTime) + "," +  mApprovalStatus + "," + mApprovalLevel + "," + mApprovalQualifier + "," + mName + "," + mIdentifier;  }
		internal static IfcApproval Parse(string strDef) { IfcApproval a = new IfcApproval(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
	}
	public partial class IfcApprovalActorRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		internal int mActor;// : IfcActorSelect;
		internal int mApproval;// : IfcApproval;
		internal int mRole;// : IfcActorRole; 
		internal IfcApprovalActorRelationship() : base() { }
		//internal IfcApprovalActorRelationship(IfcApprovalActorRelationship o) : base() { mActor = o.mActor; mApproval = o.mApproval; mRole = o.mRole; }
		internal static void parseFields(IfcApprovalActorRelationship r,List<string> arrFields, ref int ipos) { r.mActor = ParserSTEP.ParseLink(arrFields[ipos++]); r.mApproval = ParserSTEP.ParseLink(arrFields[ipos++]); r.mRole = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mActor) + "," + ParserSTEP.LinkToString(mApproval) + "," + ParserSTEP.LinkToString(mRole);  }
		internal static IfcApprovalActorRelationship Parse(string strDef) { IfcApprovalActorRelationship r = new IfcApprovalActorRelationship(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
	}
	public partial class IfcApprovalPropertyRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		internal List<int> mApprovedProperties = new List<int>();// : SET [1:?] OF IfcProperty;
		internal int mApproval;// : IfcApproval; 
		internal IfcApprovalPropertyRelationship() : base() { }
		//internal IfcApprovalPropertyRelationship(IfcApprovalPropertyRelationship o) : base() { mApprovedProperties = new List<int>(o.mApprovedProperties.ToArray()); mApproval = o.mApproval; }
		internal static void parseFields(IfcApprovalPropertyRelationship r,List<string> arrFields, ref int ipos) {  r.mApprovedProperties = ParserSTEP.SplitListLinks(arrFields[ipos++]); r.mApproval = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mApprovedProperties[0]);
			for(int icounter = 1; icounter < mApprovedProperties.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mApprovedProperties[icounter]);
			str += ")," + ParserSTEP.LinkToString(mApproval);
			return str;
		}
		internal static IfcApprovalPropertyRelationship Parse(string strDef) { IfcApprovalPropertyRelationship r = new IfcApprovalPropertyRelationship(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
	}
	public partial class IfcApprovalRelationship : IfcResourceLevelRelationship //IFC4Change
	{
		internal int mRelatedApproval;// : IfcApproval;
		internal int mRelatingApproval;// : IfcApproval; 
		internal IfcApprovalRelationship() : base() { }
	//	internal IfcApprovalRelationship(IfcApprovalRelationship o) : base(o) { mRelatedApproval = o.mRelatedApproval; mRelatingApproval = o.mRelatingApproval;  }
		internal static void parseFields( IfcApprovalRelationship a,List<string> arrFields, ref int ipos,ReleaseVersion schema) 
		{
			IfcResourceLevelRelationship.parseFields(a,arrFields,ref ipos,schema);
			a.mRelatedApproval = ParserSTEP.ParseLink(arrFields[ipos++]); 
			a.mRelatingApproval = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (schema == ReleaseVersion.IFC2x3)
			{
				a.mDescription = arrFields[ipos++];
				a.mName = arrFields[ipos++];
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatedApproval) + "," + ParserSTEP.LinkToString(mRelatingApproval) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? (mDescription == "$" ? ",$,'" : ",'" + mDescription + "','") +  mName  + "'": ""); }
		internal static IfcApprovalRelationship Parse(string strDef,ReleaseVersion schema) { IfcApprovalRelationship a = new IfcApprovalRelationship(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
	}
	public partial class IfcArbitraryClosedProfileDef : IfcProfileDef //SUPERTYPE OF(IfcArbitraryProfileDefWithVoids)
	{
		private int mOuterCurve;// : IfcBoundedCurve
		public IfcBoundedCurve OuterCurve { get { return mDatabase[mOuterCurve] as IfcBoundedCurve; } set { mOuterCurve = value.mIndex; } }

		internal IfcArbitraryClosedProfileDef() : base() { }
		internal IfcArbitraryClosedProfileDef(DatabaseIfc db, IfcArbitraryClosedProfileDef p) : base(db, p) { OuterCurve = db.Factory.Duplicate(p.OuterCurve) as IfcBoundedCurve; }
		public IfcArbitraryClosedProfileDef(string name, IfcBoundedCurve boundedCurve) : base(boundedCurve.mDatabase,name) { mOuterCurve = boundedCurve.mIndex; }//if (string.Compare(getKW, mKW) == 0) mModel.mArbProfiles.Add(this); }

		internal new static IfcArbitraryClosedProfileDef Parse(string strDef) { IfcArbitraryClosedProfileDef p = new IfcArbitraryClosedProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcArbitraryClosedProfileDef p, List<string> arrFields, ref int ipos) { IfcProfileDef.parseFields(p, arrFields, ref ipos); p.mOuterCurve = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mOuterCurve); }

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			OuterCurve.changeSchema(schema);
		}
	}
	public partial class IfcArbitraryOpenProfileDef : IfcProfileDef //	SUPERTYPE OF(IfcCenterLineProfileDef)
	{
		private int mCurve;// : IfcBoundedCurve
		public IfcBoundedCurve Curve { get { return mDatabase[mCurve] as IfcBoundedCurve; } set { mCurve = value.mIndex; } }

		internal IfcArbitraryOpenProfileDef() : base() { }
		internal IfcArbitraryOpenProfileDef(DatabaseIfc db, IfcArbitraryOpenProfileDef p) : base(db, p) { Curve = db.Factory.Duplicate(p.Curve) as IfcBoundedCurve; }
		public IfcArbitraryOpenProfileDef(string name, IfcBoundedCurve boundedCurve) : base(boundedCurve.mDatabase,name) { mCurve = boundedCurve.mIndex; }
		
		internal new static IfcArbitraryOpenProfileDef Parse(string strDef) { IfcArbitraryOpenProfileDef p = new IfcArbitraryOpenProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcArbitraryOpenProfileDef p, List<string> arrFields, ref int ipos) { IfcProfileDef.parseFields(p, arrFields, ref ipos); p.mCurve = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mCurve); }
	}
	public partial class IfcArbitraryProfileDefWithVoids : IfcArbitraryClosedProfileDef
	{
		private List<int> mInnerCurves = new List<int>();// : SET [1:?] OF IfcCurve; 
		public List<IfcCurve> InnerCurves { get { return mInnerCurves.ConvertAll(x => mDatabase[x] as IfcCurve); } set { mInnerCurves = value.ConvertAll(x => x.mIndex); } }

		internal IfcArbitraryProfileDefWithVoids() : base() { }
		internal IfcArbitraryProfileDefWithVoids(DatabaseIfc db, IfcArbitraryProfileDefWithVoids p) : base(db, p) { InnerCurves = p.InnerCurves.ConvertAll(x => db.Factory.Duplicate(x) as IfcCurve); }
		public IfcArbitraryProfileDefWithVoids(string name, IfcBoundedCurve perim, IfcCurve inner) : base(name, perim) { mInnerCurves.Add(inner.mIndex); }
		public IfcArbitraryProfileDefWithVoids(string name, IfcBoundedCurve perim, List<IfcCurve> inner) : base(name, perim) { mInnerCurves = inner.ConvertAll(x => x.mIndex); }
		internal new static IfcArbitraryProfileDefWithVoids Parse(string strDef) { IfcArbitraryProfileDefWithVoids p = new IfcArbitraryProfileDefWithVoids(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcArbitraryProfileDefWithVoids p, List<string> arrFields, ref int ipos) { IfcArbitraryClosedProfileDef.parseFields(p, arrFields, ref ipos); p.mInnerCurves = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			if (mInnerCurves.Count == 0)
				return base.BuildStringSTEP();
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mInnerCurves[0]);
			for (int icounter = 1; icounter < mInnerCurves.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mInnerCurves[icounter]);
			return str + ")";
		}
	}
	public partial class IfcArcIndex : IfcSegmentIndexSelect
	{
		internal int mA, mB, mC;
		public IfcArcIndex(int a, int b, int c) { mA = a; mB = b; mC = c; }
		public override string ToString() { return "IFCARCINDEX((" + mA + "," + mB + "," + mC + "))"; }
	}
	public partial class IfcAsset : IfcGroup
	{
		internal string mAssetID;// : IfcIdentifier;
		internal int mOriginalValue;// : IfcCostValue;
		internal int mCurrentValue;// : IfcCostValue;
		internal int mTotalReplacementCost;// : IfcCostValue;
		internal int mOwner;// : IfcActorSelect;
		internal int mUser;// : IfcActorSelect;
		internal int mResponsiblePerson;// : IfcPerson;
		internal string mIncorporationDate = ""; // : IfcDate 
		internal int mIncorporationDateSS;// : IfcDate Ifc2x3 IfcCalendarDate;
		internal int mDepreciatedValue;// : IfcCostValue; 

		public string AssetID { get { return (mAssetID == "$" ? "" : ParserIfc.Decode(mAssetID)); } set { mAssetID = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcCostValue OriginalValue { get { return mDatabase[mOriginalValue] as IfcCostValue; } set { mOriginalValue = value.mIndex; } } 
		public IfcCostValue CurrentValue { get { return mDatabase[mCurrentValue] as IfcCostValue; } set { mCurrentValue = value.mIndex; } } 
		public IfcCostValue TotalReplacementCost { get { return mDatabase[mTotalReplacementCost] as IfcCostValue; } set { mTotalReplacementCost = value.mIndex; } } 
		public IfcActorSelect Owner { get { return mDatabase[mOwner] as IfcActorSelect; } set { mOwner = value.Index; } }
		public IfcActorSelect User { get { return mDatabase[mUser] as IfcActorSelect; } set { mUser = value.Index; } }
		public IfcPerson ResponsiblePerson { get { return mDatabase[mResponsiblePerson] as IfcPerson; } set { mResponsiblePerson = value.mIndex; } }
		//public  IncorporationDate
		public IfcCostValue DepreciatedValue { get { return mDatabase[mDepreciatedValue] as IfcCostValue; } set { mDepreciatedValue = value.mIndex; } } 

		
		internal IfcAsset() : base() { }
		internal IfcAsset(DatabaseIfc db, IfcAsset a) : base(db,a)
		{
			mAssetID = a.mAssetID;
			OriginalValue = db.Factory.Duplicate(a.OriginalValue) as IfcCostValue;
			CurrentValue = db.Factory.Duplicate(a.CurrentValue) as IfcCostValue;
			TotalReplacementCost = db.Factory.Duplicate(a.TotalReplacementCost) as IfcCostValue;
			Owner = db.Factory.Duplicate(a.mDatabase[a.mOwner]) as IfcActorSelect;
			User = db.Factory.Duplicate(a.mDatabase[a.mUser]) as IfcActorSelect;
			ResponsiblePerson = db.Factory.Duplicate(a.ResponsiblePerson) as IfcPerson;
			mIncorporationDate = a.mIncorporationDate;
			if(a.mIncorporationDateSS > 0)
				mIncorporationDateSS = db.Factory.Duplicate(a.mDatabase[ a.mIncorporationDateSS]).mIndex;

			DepreciatedValue =  db.Factory.Duplicate(a.DepreciatedValue) as IfcCostValue;
		}
		internal IfcAsset(DatabaseIfc m, string name) : base(m,name) { }
		internal new static IfcAsset Parse(string strDef, ReleaseVersion release) { IfcAsset a = new IfcAsset(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcAsset a,List<string> arrFields, ref int ipos, ReleaseVersion release)
		{ 
			IfcGroup.parseFields(a,arrFields, ref ipos);
			a.mAssetID = arrFields[ipos++].Replace("'","");
			a.mOriginalValue = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mCurrentValue = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mTotalReplacementCost = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mOwner = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mUser = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mResponsiblePerson = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (release == ReleaseVersion.IFC2x3)
				a.mIncorporationDateSS = ParserSTEP.ParseLink(arrFields[ipos++]);
			else
				a.mIncorporationDate = arrFields[ipos++];
			a.mDepreciatedValue = ParserSTEP.ParseLink(arrFields[ipos++]); 
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() +",'" + mAssetID + "'," + ParserSTEP.LinkToString(mOriginalValue) + "," +ParserSTEP.LinkToString(mCurrentValue) + "," + 
				ParserSTEP.LinkToString(mTotalReplacementCost) + "," +ParserSTEP.LinkToString(mOwner) + "," +
				ParserSTEP.LinkToString(mUser) + "," +ParserSTEP.LinkToString(mResponsiblePerson) + "," +
				(mDatabase.Release == ReleaseVersion.IFC2x3 ? ParserSTEP.LinkToString(mIncorporationDateSS) : mIncorporationDate ) + "," +ParserSTEP.LinkToString(mDepreciatedValue);
		}
	}
	public partial class IfcAsymmetricIShapeProfileDef : IfcParameterizedProfileDef // Ifc2x3 IfcIShapeProfileDef 
	{
		internal double mBottomFlangeWidth, mOverallDepth, mWebThickness, mBottomFlangeThickness;//	:	IfcPositiveLengthMeasure;
		internal double mBottomFlangeFilletRadius = double.NaN;//	:	OPTIONAL IfcNonNegativeLengthMeasure;
		internal double mTopFlangeWidth;// : IfcPositiveLengthMeasure;
		internal double mTopFlangeThickness = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mTopFlangeFilletRadius = double.NaN;// 	:	OPTIONAL IfcNonNegativeLengthMeasure;
		internal double mBottomFlangeEdgeRadius = double.NaN;//	:	OPTIONAL IfcNonNegativeLengthMeasure;
		internal double mBottomFlangeSlope = double.NaN;//	:	OPTIONAL IfcPlaneAngleMeasure;
		internal double mTopFlangeEdgeRadius = double.NaN;//	:	OPTIONAL IfcNonNegativeLengthMeasure;
		internal double mTopFlangeSlope = double.NaN;//:	OPTIONAL IfcPlaneAngleMeasure;
		internal double mCentreOfGravityInY;// : OPTIONAL IfcPositiveLengthMeasure IFC4 deleted
		internal IfcAsymmetricIShapeProfileDef() : base() { }
		internal IfcAsymmetricIShapeProfileDef(DatabaseIfc db, IfcAsymmetricIShapeProfileDef p) : base(db, p)
		{
			mBottomFlangeWidth = p.mBottomFlangeWidth;
			mOverallDepth = p.mOverallDepth;
			mWebThickness = p.mWebThickness;
			mBottomFlangeThickness = p.mBottomFlangeThickness;
			mBottomFlangeFilletRadius = p.mBottomFlangeFilletRadius;
			mTopFlangeWidth = p.mTopFlangeWidth;
			mTopFlangeThickness = p.mTopFlangeThickness;
			mTopFlangeFilletRadius = p.mTopFlangeFilletRadius;
			mBottomFlangeEdgeRadius = p.mBottomFlangeEdgeRadius;
			mBottomFlangeSlope = p.mBottomFlangeSlope;
			mTopFlangeEdgeRadius = p.mTopFlangeEdgeRadius;
			mTopFlangeSlope = p.mTopFlangeSlope;
			mCentreOfGravityInY = p.mCentreOfGravityInY;
		}
		internal static void parseFields(IfcAsymmetricIShapeProfileDef p, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos);
			if (schema == ReleaseVersion.IFC2x3)
			{
				p.mBottomFlangeWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mOverallDepth = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mWebThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mBottomFlangeThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mBottomFlangeFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mTopFlangeWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mTopFlangeThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mTopFlangeFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mCentreOfGravityInY = ParserSTEP.ParseDouble(arrFields[ipos++]);
			}
			else
			{
				p.mBottomFlangeWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mOverallDepth = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mWebThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mBottomFlangeThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mBottomFlangeFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mTopFlangeWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mTopFlangeThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mTopFlangeFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mBottomFlangeEdgeRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mBottomFlangeSlope = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mTopFlangeEdgeRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mTopFlangeSlope = ParserSTEP.ParseDouble(arrFields[ipos++]);
			}
		}
		internal static IfcAsymmetricIShapeProfileDef Parse(string strDef,ReleaseVersion schema) { IfcAsymmetricIShapeProfileDef p = new IfcAsymmetricIShapeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		
		protected override string BuildStringSTEP()
		{
			if (mDatabase.Release == ReleaseVersion.IFC2x3)
			{
				return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mBottomFlangeWidth) + "," + ParserSTEP.DoubleToString(mOverallDepth) + "," + 
					ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mBottomFlangeThickness) + "," + ParserSTEP.DoubleOptionalToString(mBottomFlangeFilletRadius) + "," +
					ParserSTEP.DoubleToString(mTopFlangeWidth) + "," + ParserSTEP.DoubleOptionalToString(mTopFlangeThickness) + "," +
					ParserSTEP.DoubleOptionalToString(mTopFlangeFilletRadius) +  "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY);
			}
			return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mBottomFlangeWidth) + "," + ParserSTEP.DoubleToString(mOverallDepth) + "," +
					ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mBottomFlangeThickness) + "," + ParserSTEP.DoubleOptionalToString(mBottomFlangeFilletRadius) + "," +
				ParserSTEP.DoubleToString(mTopFlangeWidth) + "," + ParserSTEP.DoubleOptionalToString(mTopFlangeThickness) + "," +
				ParserSTEP.DoubleOptionalToString(mTopFlangeFilletRadius) + "," + ParserSTEP.DoubleOptionalToString(mBottomFlangeEdgeRadius) + "," +
				ParserSTEP.DoubleOptionalToString(mBottomFlangeSlope) + "," + ParserSTEP.DoubleOptionalToString(mTopFlangeEdgeRadius) + "," +
				ParserSTEP.DoubleOptionalToString(mTopFlangeSlope);
		}
	}
	public partial class IfcAudioVisualAppliance : IfcFlowTerminal //IFC4
	{
		internal IfcAudioVisualApplianceTypeEnum mPredefinedType = IfcAudioVisualApplianceTypeEnum.NOTDEFINED;// OPTIONAL : IfcAudioVisualApplianceTypeEnum;
		public IfcAudioVisualApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAudioVisualAppliance() : base() { }
		internal IfcAudioVisualAppliance(DatabaseIfc db, IfcAudioVisualAppliance a) : base(db,a) { mPredefinedType = a.mPredefinedType; }
		internal static void parseFields(IfcAudioVisualAppliance s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcAudioVisualApplianceTypeEnum)Enum.Parse(typeof(IfcAudioVisualApplianceTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcAudioVisualAppliance Parse(string strDef) { IfcAudioVisualAppliance s = new IfcAudioVisualAppliance(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : ( mPredefinedType == IfcAudioVisualApplianceTypeEnum.NOTDEFINED  ? ",$" : ",." + mPredefinedType.ToString() + ".")); }

	}
	public partial class IfcAudioVisualApplianceType : IfcFlowTerminalType
	{
		internal IfcAudioVisualApplianceTypeEnum mPredefinedType = IfcAudioVisualApplianceTypeEnum.NOTDEFINED;// : IfcAudioVisualApplianceBoxTypeEnum; 
		public IfcAudioVisualApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAudioVisualApplianceType() : base() { }
		internal IfcAudioVisualApplianceType(DatabaseIfc db, IfcAudioVisualApplianceType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcAudioVisualApplianceType(DatabaseIfc m, string name, IfcAudioVisualApplianceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcAudioVisualApplianceType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcAudioVisualApplianceTypeEnum)Enum.Parse(typeof(IfcAudioVisualApplianceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcAudioVisualApplianceType Parse(string strDef) { IfcAudioVisualApplianceType t = new IfcAudioVisualApplianceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcAxis1Placement : IfcPlacement
	{
		private int mAxis;//  : OPTIONAL IfcDirection
		public IfcDirection Axis { get { return (mAxis > 0 ? mDatabase[mAxis] as IfcDirection : null); } set { mAxis = (value == null ? 0 : value.mIndex); } }
		
		internal IfcAxis1Placement() : base() { }
		public IfcAxis1Placement(DatabaseIfc db) : base(db) { }
		public IfcAxis1Placement(IfcCartesianPoint location) : base(location) { }
		public IfcAxis1Placement(IfcDirection axis) : base(axis.mDatabase) { Axis = axis; }
		public IfcAxis1Placement(IfcCartesianPoint location, IfcDirection axis) : base(location) { Axis = axis; }
		internal IfcAxis1Placement(DatabaseIfc db, IfcAxis1Placement p) : base(db,p) { if(p.mAxis > 0) Axis = db.Factory.Duplicate( p.Axis) as IfcDirection; }
 
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mAxis); }
		internal static IfcAxis1Placement Parse(string strDef) { IfcAxis1Placement p = new IfcAxis1Placement(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcAxis1Placement p, List<string> arrFields, ref int ipos) { IfcPlacement.parseFields(p, arrFields, ref ipos); p.mAxis = ParserSTEP.ParseLink(arrFields[ipos++]); }
	}
	public partial interface IfcAxis2Placement : IBaseClassIfc { } //SELECT ( IfcAxis2Placement2D, IfcAxis2Placement3D);
	public partial class IfcAxis2Placement2D : IfcPlacement, IfcAxis2Placement
	{ 
		private int mRefDirection;// : OPTIONAL IfcDirection;
		public IfcDirection RefDirection { get { return mDatabase[mRefDirection] as IfcDirection; } set { mRefDirection = value.mIndex; } }
		
		internal IfcAxis2Placement2D() : base() { }
		internal IfcAxis2Placement2D(DatabaseIfc db, IfcAxis2Placement2D p) : base(db, p)
		{
			if (p.mRefDirection > 0)
				RefDirection = db.Factory.Duplicate(p.RefDirection) as IfcDirection;
		}
		public IfcAxis2Placement2D(DatabaseIfc db) : base(db.Factory.Origin2d) { }
		public IfcAxis2Placement2D(IfcCartesianPoint location) : base(location) { }
		

		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRefDirection); }
		internal static IfcAxis2Placement2D Parse(string strDef) { IfcAxis2Placement2D p = new IfcAxis2Placement2D(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcAxis2Placement2D p, List<string> arrFields, ref int ipos) { IfcPlacement.parseFields(p, arrFields, ref ipos); p.mRefDirection = ParserSTEP.ParseLink(arrFields[ipos++]); }

		internal override bool isWorldXY { get { return base.isWorldXY && (mRefDirection == 0 || RefDirection.isXAxis); } }
	}
	public partial class IfcAxis2Placement3D : IfcPlacement, IfcAxis2Placement
	{
		private int mAxis;// : OPTIONAL IfcDirection;
		private int mRefDirection;// : OPTIONAL IfcDirection; 

		public IfcDirection Axis
		{
			get { return mDatabase[mAxis] as IfcDirection; }
			set
			{
				if (value == null)
					mAxis = 0;
				else
				{
					mAxis = value.mIndex;
					if (mRefDirection == 0)
						RefDirection = (Math.Abs(value.DirectionRatioX - 1) < 1e-3 ? mDatabase.Factory.YAxis : mDatabase.Factory.XAxis);
				}
			}
		}
		public IfcDirection RefDirection
		{
			get { return mDatabase[mRefDirection] as IfcDirection; }
			set
			{
				if (value == null)
					mRefDirection = 0;
				else
				{
					mRefDirection = value.mIndex;
					if (mAxis == 0)
						Axis = (Math.Abs(value.DirectionRatioZ - 1) < 1e-3 ? mDatabase.Factory.XAxis : mDatabase.Factory.ZAxis);
				}
			}
		}

		internal IfcAxis2Placement3D() : base() { }
		public IfcAxis2Placement3D(IfcCartesianPoint location) : base(location) { }
		public IfcAxis2Placement3D(IfcCartesianPoint location, IfcDirection axis, IfcDirection refDirection) : base(location) { mAxis = (axis == null ? 0 : axis.mIndex); mRefDirection = (refDirection == null ? 0 : refDirection.mIndex); }
		internal IfcAxis2Placement3D(DatabaseIfc db, IfcAxis2Placement3D p) : base(db, p)
		{
			if (p.mAxis > 0)
				Axis = db.Factory.Duplicate(p.Axis) as IfcDirection;
			if (p.mRefDirection > 0)
				RefDirection = db.Factory.Duplicate(p.RefDirection) as IfcDirection;
		}

		internal static IfcAxis2Placement3D Parse(string strDef) { IfcAxis2Placement3D p = new IfcAxis2Placement3D(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcAxis2Placement3D p, List<string> arrFields, ref int ipos) { IfcPlacement.parseFields(p, arrFields, ref ipos); p.mAxis = ParserSTEP.ParseLink(arrFields[ipos++]); p.mRefDirection = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mAxis > 0 ? "," + ParserSTEP.LinkToString(mAxis) : ",$") + (mRefDirection > 0 ? "," + ParserSTEP.LinkToString(mRefDirection) : ",$"); }

		internal override bool isWorldXY
		{
			get
			{
				if (mAxis > 0 && !Axis.isZAxis)
					return false;
				if (mRefDirection > 0 && !RefDirection.isXAxis)
					return false;
				return base.isWorldXY;
			}
		}
	}
}
